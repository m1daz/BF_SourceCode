using System;
using System.Collections.Generic;
using System.IO;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000073 RID: 115
	[JsonOptIn]
	[Serializable]
	public class NavMeshGraph : NavGraph, INavmesh, ISerializableGraph, IUpdatableGraph, IFunnelGraph, IRaycastableGraph, ISerializableObject
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x000209D8 File Offset: 0x0001EDD8
		public override Node[] CreateNodes(int number)
		{
			MeshNode[] array = new MeshNode[number];
			for (int i = 0; i < number; i++)
			{
				array[i] = new MeshNode();
				array[i].penalty = this.initialPenalty;
			}
			return array;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00020A15 File Offset: 0x0001EE15
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00020A1D File Offset: 0x0001EE1D
		public BBTree bbTree
		{
			get
			{
				return this._bbTree;
			}
			set
			{
				this._bbTree = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00020A26 File Offset: 0x0001EE26
		// (set) Token: 0x060003BA RID: 954 RVA: 0x00020A2E File Offset: 0x0001EE2E
		public Int3[] vertices
		{
			get
			{
				return this._vertices;
			}
			set
			{
				this._vertices = value;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00020A37 File Offset: 0x0001EE37
		public void GenerateMatrix()
		{
			this.matrix = Matrix4x4.TRS(this.offset, Quaternion.Euler(this.rotation), new Vector3(this.scale, this.scale, this.scale));
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00020A6C File Offset: 0x0001EE6C
		public override void RelocateNodes(Matrix4x4 oldMatrix, Matrix4x4 newMatrix)
		{
			if (this.vertices == null || this.vertices.Length == 0 || this.originalVertices == null || this.originalVertices.Length != this.vertices.Length)
			{
				return;
			}
			for (int i = 0; i < this.vertices.Length; i++)
			{
				this.vertices[i] = (Int3)newMatrix.MultiplyPoint3x4(this.originalVertices[i]);
			}
			for (int j = 0; j < this.nodes.Length; j++)
			{
				MeshNode meshNode = (MeshNode)this.nodes[j];
				meshNode.position = (this.vertices[meshNode.v1] + this.vertices[meshNode.v2] + this.vertices[meshNode.v3]) / 3f;
				if (meshNode.connections != null)
				{
					for (int k = 0; k < meshNode.connections.Length; k++)
					{
						meshNode.connectionCosts[k] = (meshNode.position - meshNode.connections[k].position).costMagnitude;
					}
				}
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00020BCC File Offset: 0x0001EFCC
		public static NNInfo GetNearest(INavmesh graph, Node[] nodes, Vector3 position, NNConstraint constraint, bool accurateNearestNode)
		{
			if (nodes == null || nodes.Length == 0)
			{
				Debug.LogError("NavGraph hasn't been generated yet or does not contain any nodes");
				return default(NNInfo);
			}
			if (constraint == null)
			{
				constraint = NNConstraint.None;
			}
			Int3[] vertices = graph.vertices;
			if (graph.bbTree == null)
			{
				return NavMeshGraph.GetNearestForce(nodes, vertices, position, constraint, accurateNearestNode);
			}
			float num = (graph.bbTree.root.rect.width + graph.bbTree.root.rect.height) * 0.5f * 0.02f;
			NNInfo result = graph.bbTree.QueryCircle(position, num, constraint);
			if (result.node == null)
			{
				for (int i = 1; i <= 8; i++)
				{
					result = graph.bbTree.QueryCircle(position, (float)(i * i) * num, constraint);
					if (result.node != null || (float)((i - 1) * (i - 1)) * num > AstarPath.active.maxNearestNodeDistance * 2f)
					{
						break;
					}
				}
			}
			if (result.node != null)
			{
				result.clampedPosition = NavMeshGraph.ClosestPointOnNode(result.node as MeshNode, vertices, position);
			}
			if (result.constrainedNode != null)
			{
				if (constraint.constrainDistance && ((Vector3)result.constrainedNode.position - position).sqrMagnitude > AstarPath.active.maxNearestNodeDistanceSqr)
				{
					result.constrainedNode = null;
				}
				else
				{
					result.constClampedPosition = NavMeshGraph.ClosestPointOnNode(result.constrainedNode as MeshNode, vertices, position);
				}
			}
			return result;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00020D6B File Offset: 0x0001F16B
		public override NNInfo GetNearest(Vector3 position, NNConstraint constraint, Node hint)
		{
			return NavMeshGraph.GetNearest(this, this.nodes, position, constraint, this.accurateNearestNode);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00020D81 File Offset: 0x0001F181
		public override NNInfo GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return NavMeshGraph.GetNearestForce(this.nodes, this.vertices, position, constraint, this.accurateNearestNode);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00020D9C File Offset: 0x0001F19C
		public static NNInfo GetNearestForce(Node[] nodes, Int3[] vertices, Vector3 position, NNConstraint constraint, bool accurateNearestNode)
		{
			NNInfo nearestForceBoth = NavMeshGraph.GetNearestForceBoth(nodes, vertices, position, constraint, accurateNearestNode);
			nearestForceBoth.node = nearestForceBoth.constrainedNode;
			nearestForceBoth.clampedPosition = nearestForceBoth.constClampedPosition;
			return nearestForceBoth;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00020DD4 File Offset: 0x0001F1D4
		public static NNInfo GetNearestForceBoth(Node[] nodes, Int3[] vertices, Vector3 position, NNConstraint constraint, bool accurateNearestNode)
		{
			Int3 @int = (Int3)position;
			float num = -1f;
			Node node = null;
			float num2 = -1f;
			Node node2 = null;
			float num3 = (!constraint.constrainDistance) ? float.PositiveInfinity : AstarPath.active.maxNearestNodeDistanceSqr;
			if (nodes == null || nodes.Length == 0)
			{
				return default(NNInfo);
			}
			for (int i = 0; i < nodes.Length; i++)
			{
				MeshNode meshNode = nodes[i] as MeshNode;
				if (accurateNearestNode)
				{
					Vector3 b = Polygon.ClosestPointOnTriangle((Vector3)vertices[meshNode.v1], (Vector3)vertices[meshNode.v2], (Vector3)vertices[meshNode.v3], position);
					float sqrMagnitude = ((Vector3)@int - b).sqrMagnitude;
					if (node == null || sqrMagnitude < num)
					{
						num = sqrMagnitude;
						node = meshNode;
					}
					if (sqrMagnitude < num3 && constraint.Suitable(meshNode) && (node2 == null || sqrMagnitude < num2))
					{
						num2 = sqrMagnitude;
						node2 = meshNode;
					}
				}
				else if (!Polygon.IsClockwise(vertices[meshNode.v1], vertices[meshNode.v2], @int) || !Polygon.IsClockwise(vertices[meshNode.v2], vertices[meshNode.v3], @int) || !Polygon.IsClockwise(vertices[meshNode.v3], vertices[meshNode.v1], @int))
				{
					float sqrMagnitude2 = (meshNode.position - @int).sqrMagnitude;
					if (node == null || sqrMagnitude2 < num)
					{
						num = sqrMagnitude2;
						node = meshNode;
					}
					if (sqrMagnitude2 < num3 && constraint.Suitable(meshNode) && (node2 == null || sqrMagnitude2 < num2))
					{
						num2 = sqrMagnitude2;
						node2 = meshNode;
					}
				}
				else
				{
					int num4 = Mathfx.Abs(meshNode.position.y - @int.y);
					if (node == null || (float)num4 < num)
					{
						num = (float)num4;
						node = meshNode;
					}
					if ((float)num4 < num3 && constraint.Suitable(meshNode) && (node2 == null || (float)num4 < num2))
					{
						num2 = (float)num4;
						node2 = meshNode;
					}
				}
			}
			NNInfo result = new NNInfo(node);
			if (result.node != null)
			{
				MeshNode meshNode2 = result.node as MeshNode;
				Vector3 clampedPosition = Polygon.ClosestPointOnTriangle((Vector3)vertices[meshNode2.v1], (Vector3)vertices[meshNode2.v2], (Vector3)vertices[meshNode2.v3], position);
				result.clampedPosition = clampedPosition;
			}
			result.constrainedNode = node2;
			if (result.constrainedNode != null)
			{
				MeshNode meshNode3 = result.constrainedNode as MeshNode;
				Vector3 constClampedPosition = Polygon.ClosestPointOnTriangle((Vector3)vertices[meshNode3.v1], (Vector3)vertices[meshNode3.v2], (Vector3)vertices[meshNode3.v3], position);
				result.constClampedPosition = constClampedPosition;
			}
			return result;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00021144 File Offset: 0x0001F544
		public void BuildFunnelCorridor(List<Node> path, int startIndex, int endIndex, List<Vector3> left, List<Vector3> right)
		{
			NavMeshGraph.BuildFunnelCorridor(this, path, startIndex, endIndex, left, right);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00021154 File Offset: 0x0001F554
		public static void BuildFunnelCorridor(INavmesh graph, List<Node> path, int startIndex, int endIndex, List<Vector3> left, List<Vector3> right)
		{
			if (graph == null)
			{
				Debug.LogError("Couldn't cast graph to the appropriate type (graph isn't a Navmesh type graph, it doesn't implement the INavmesh interface)");
				return;
			}
			Int3[] vertices = graph.vertices;
			int num = -1;
			int num2 = -1;
			for (int i = startIndex; i < endIndex; i++)
			{
				MeshNode meshNode = path[i] as MeshNode;
				MeshNode meshNode2 = path[i + 1] as MeshNode;
				bool flag = false;
				int num3 = -1;
				int num4 = -1;
				for (int j = 0; j < 3; j++)
				{
					int vertexIndex = meshNode.GetVertexIndex(j);
					for (int k = 0; k < 3; k++)
					{
						int vertexIndex2 = meshNode2.GetVertexIndex(k);
						if (vertexIndex == vertexIndex2)
						{
							if (flag)
							{
								num4 = vertexIndex2;
								break;
							}
							num3 = vertexIndex2;
							flag = true;
						}
					}
				}
				if (num3 == -1 || num4 == -1)
				{
					left.Add((Vector3)meshNode.position);
					right.Add((Vector3)meshNode.position);
					left.Add((Vector3)meshNode2.position);
					right.Add((Vector3)meshNode2.position);
					num = num3;
					num2 = num4;
				}
				else if (num3 == num)
				{
					left.Add((Vector3)vertices[num3]);
					right.Add((Vector3)vertices[num4]);
					num = num3;
					num2 = num4;
				}
				else if (num3 == num2)
				{
					left.Add((Vector3)vertices[num4]);
					right.Add((Vector3)vertices[num3]);
					num = num4;
					num2 = num3;
				}
				else if (num4 == num)
				{
					left.Add((Vector3)vertices[num4]);
					right.Add((Vector3)vertices[num3]);
					num = num4;
					num2 = num3;
				}
				else
				{
					left.Add((Vector3)vertices[num3]);
					right.Add((Vector3)vertices[num4]);
					num = num3;
					num2 = num4;
				}
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00021386 File Offset: 0x0001F786
		public void AddPortal(Node n1, Node n2, List<Vector3> left, List<Vector3> right)
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00021388 File Offset: 0x0001F788
		public bool Linecast(Vector3 origin, Vector3 end)
		{
			return this.Linecast(origin, end, base.GetNearest(origin, NNConstraint.None).node);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000213B1 File Offset: 0x0001F7B1
		public bool Linecast(Vector3 origin, Vector3 end, Node hint, out GraphHitInfo hit)
		{
			return NavMeshGraph.Linecast(this, origin, end, hint, false, 0f, out hit);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000213C4 File Offset: 0x0001F7C4
		public bool Linecast(Vector3 origin, Vector3 end, Node hint)
		{
			GraphHitInfo graphHitInfo;
			return NavMeshGraph.Linecast(this, origin, end, hint, false, 0f, out graphHitInfo);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000213E4 File Offset: 0x0001F7E4
		public static bool Linecast(INavmesh graph, Vector3 tmp_origin, Vector3 tmp_end, Node hint, bool thick, float thickness, out GraphHitInfo hit)
		{
			Int3 @int = (Int3)tmp_end;
			Int3 int2 = (Int3)tmp_origin;
			if (thickness <= 0f)
			{
				thick = false;
			}
			hit = default(GraphHitInfo);
			MeshNode meshNode = hint as MeshNode;
			if (meshNode == null)
			{
				Debug.LogError("NavMeshGenerator:Linecast: The 'hint' must be a MeshNode");
				return true;
			}
			if (int2 == @int)
			{
				hit.node = meshNode;
				return false;
			}
			Int3[] vertices = graph.vertices;
			int2 = (Int3)meshNode.ClosestPoint((Vector3)int2, vertices);
			hit.origin = (Vector3)int2;
			Vector3 normalized = ((Vector3)(@int - int2)).normalized;
			Int3 int3 = (Int3)(normalized * 1f);
			Int3 int4 = int2;
			int num = 0;
			int[] array = new int[3];
			string str = string.Empty;
			MeshNode meshNode2 = null;
			if (!meshNode.walkable)
			{
				str += " Node is unwalkable";
				hit.point = (Vector3)int2;
				hit.tangentOrigin = (Vector3)int2;
				return true;
			}
			int num4;
			int num8;
			float num9;
			Vector3 vector;
			MeshNode meshNode3;
			for (;;)
			{
				num++;
				if (int4 == @int)
				{
					break;
				}
				if (NavMeshGraph.ContainsPoint(meshNode, (Vector3)@int, vertices))
				{
					goto Block_6;
				}
				if (num > 200)
				{
					goto Block_7;
				}
				for (int i = 0; i < 3; i++)
				{
					if (vertices[meshNode[i]] == int4)
					{
						array[i] = 0;
					}
					else
					{
						int num2 = Int3.Dot(int3, (vertices[meshNode[i]] - int4).NormalizeTo(1000));
						array[i] = num2;
					}
				}
				int num3 = 0;
				if (array[1] > array[num3])
				{
					num3 = 1;
				}
				if (array[2] > array[num3])
				{
					num3 = 2;
				}
				num4 = meshNode[num3];
				if (num == 70 || num == 71)
				{
					string text = string.Concat(new object[]
					{
						"Count ",
						num,
						" ",
						meshNode.position,
						"\n"
					});
					for (int j = 0; j < array.Length; j++)
					{
						text = text + array[j].ToString("0.00") + "\n";
					}
					Debug.Log(text);
				}
				int num5 = 0;
				long num6 = Polygon.TriangleArea2(int4, int4 + int3, vertices[num4]);
				if (num6 == 0L)
				{
					int num7 = -1;
					for (int k = 0; k < 3; k++)
					{
						if (num3 != k && (num7 == -1 || array[k] > array[num7]))
						{
							num7 = k;
						}
					}
					num8 = meshNode[num7];
				}
				else if (num6 < 0L)
				{
					num5 = ((num3 - 1 >= 0) ? (num3 - 1) : 2);
					num8 = meshNode[num5];
				}
				else
				{
					num5 = ((num3 + 1 <= 2) ? (num3 + 1) : 0);
					num8 = meshNode[num5];
				}
				bool flag = true;
				if (thick)
				{
					num9 = Polygon.IntersectionFactor((Vector3)vertices[num4], (Vector3)vertices[num8], (Vector3)int4, (Vector3)@int);
					if (num9 < 0f || num9 > 1f)
					{
						goto IL_3E5;
					}
					Vector3 a = (Vector3)(vertices[num8] - vertices[num4]);
					vector = (Vector3)vertices[num4] + a * num9;
					float magnitude = a.magnitude;
					num9 *= magnitude;
					if (num9 - thickness < 0f)
					{
						goto Block_24;
					}
					if (num9 + thickness > magnitude)
					{
						goto Block_25;
					}
				}
				else
				{
					float num10 = Polygon.IntersectionFactor((Vector3)vertices[num4], (Vector3)vertices[num8], (Vector3)int4, (Vector3)@int);
					if (num10 == -1f)
					{
						flag = false;
					}
					num10 = Mathf.Clamp01(num10);
					vector = (Vector3)vertices[num4] + (Vector3)(vertices[num8] - vertices[num4]) * num10;
					if (!flag)
					{
						if ((vertices[num4] - int4).sqrMagnitude >= (@int - int4).sqrMagnitude)
						{
							goto Block_28;
						}
						num5 = ((num3 == 0 || num5 == 0) ? ((num3 == 1 || num5 == 1) ? 2 : 1) : 0);
						num8 = meshNode[num5];
						vector = (Vector3)vertices[num4];
						flag = true;
						str = "Colinear - Continuing";
					}
					float num11 = Mathfx.NearestPointFactor((Vector3)int2, (Vector3)@int, vector);
					if (num11 > 1f)
					{
						flag = false;
					}
				}
				meshNode3 = null;
				bool flag2 = false;
				for (int l = 0; l < meshNode.connections.Length; l++)
				{
					MeshNode meshNode4 = meshNode.connections[l] as MeshNode;
					if (meshNode4 != null && meshNode4 != meshNode)
					{
						int num12 = 0;
						for (int m = 0; m < 3; m++)
						{
							if (meshNode4[m] == num4 || meshNode4[m] == num8)
							{
								num12++;
							}
						}
						if (num12 == 2)
						{
							if (meshNode4 == meshNode2)
							{
								str += "Other == previous node\n";
								flag2 = true;
								break;
							}
							meshNode3 = meshNode4;
							break;
						}
					}
				}
				if (flag2)
				{
					goto Block_39;
				}
				if (meshNode3 == null || !flag || !meshNode3.walkable)
				{
					goto IL_712;
				}
				meshNode2 = meshNode;
				meshNode = meshNode3;
				int4 = (Int3)vector;
			}
			str += " Current == End";
			Block_6:
			goto IL_792;
			Block_7:
			Debug.DrawRay((Vector3)int2, normalized * 100f, Color.cyan);
			Debug.DrawRay((Vector3)int4, normalized * 100f, Color.cyan);
			Debug.LogError("Possible infinite loop, intersected > 200 nodes");
			Debug.Break();
			return true;
			IL_3E5:
			Debug.LogError("This should not happen");
			hit.point = ((num9 >= 0f) ? ((Vector3)vertices[num8]) : ((Vector3)vertices[num4]));
			return true;
			Block_24:
			hit.point = (Vector3)vertices[num4];
			return true;
			Block_25:
			hit.point = (Vector3)vertices[num8];
			return true;
			Block_28:
			vector = (Vector3)@int;
			Block_39:
			goto IL_792;
			IL_712:
			if (meshNode3 == null)
			{
				str += "No next node (wall)";
			}
			hit.tangentOrigin = (Vector3)vertices[num4];
			hit.tangent = (Vector3)(vertices[num8] - vertices[num4]);
			hit.point = vector;
			hit.node = meshNode;
			return true;
			IL_792:
			hit.node = meshNode;
			return false;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00021B8C File Offset: 0x0001FF8C
		public void UpdateArea(GraphUpdateObject o)
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00021B90 File Offset: 0x0001FF90
		public static void UpdateArea(GraphUpdateObject o, NavGraph graph)
		{
			INavmesh navmesh = graph as INavmesh;
			if (navmesh == null)
			{
				Debug.LogError("Update Area on NavMesh must be called with a graph implementing INavmesh");
				return;
			}
			if (graph.nodes == null || graph.nodes.Length == 0)
			{
				Debug.LogError("NavGraph hasn't been generated yet or does not contain any nodes");
				return;
			}
			Bounds bounds = o.bounds;
			Rect rect = Rect.MinMaxRect(bounds.min.x, bounds.min.z, bounds.max.x, bounds.max.z);
			Vector3 vector = new Vector3(rect.xMin, 0f, rect.yMin);
			Vector3 vector2 = new Vector3(rect.xMin, 0f, rect.yMax);
			Vector3 vector3 = new Vector3(rect.xMax, 0f, rect.yMin);
			Vector3 vector4 = new Vector3(rect.xMax, 0f, rect.yMax);
			for (int i = 0; i < graph.nodes.Length; i++)
			{
				MeshNode meshNode = graph.nodes[i] as MeshNode;
				bool flag = false;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				for (int j = 0; j < 3; j++)
				{
					Vector3 vector5 = (Vector3)navmesh.vertices[meshNode[j]];
					Vector2 point = new Vector2(vector5.x, vector5.z);
					if (rect.Contains(point))
					{
						flag = true;
						break;
					}
					if (vector5.x < rect.xMin)
					{
						num++;
					}
					if (vector5.x > rect.xMax)
					{
						num2++;
					}
					if (vector5.z < rect.yMin)
					{
						num3++;
					}
					if (vector5.z > rect.yMax)
					{
						num4++;
					}
				}
				if (flag || (num != 3 && num2 != 3 && num3 != 3 && num4 != 3))
				{
					for (int k = 0; k < 3; k++)
					{
						int i2 = (k <= 1) ? (k + 1) : 0;
						Vector3 start = (Vector3)navmesh.vertices[meshNode[k]];
						Vector3 end = (Vector3)navmesh.vertices[meshNode[i2]];
						if (Polygon.Intersects(vector, vector2, start, end))
						{
							flag = true;
							break;
						}
						if (Polygon.Intersects(vector, vector3, start, end))
						{
							flag = true;
							break;
						}
						if (Polygon.Intersects(vector3, vector4, start, end))
						{
							flag = true;
							break;
						}
						if (Polygon.Intersects(vector4, vector2, start, end))
						{
							flag = true;
							break;
						}
					}
					if (!flag && NavMeshGraph.ContainsPoint(meshNode, vector, navmesh.vertices))
					{
						flag = true;
					}
					if (!flag && NavMeshGraph.ContainsPoint(meshNode, vector2, navmesh.vertices))
					{
						flag = true;
					}
					if (!flag && NavMeshGraph.ContainsPoint(meshNode, vector3, navmesh.vertices))
					{
						flag = true;
					}
					if (!flag && NavMeshGraph.ContainsPoint(meshNode, vector4, navmesh.vertices))
					{
						flag = true;
					}
					if (flag)
					{
						o.WillUpdateNode(meshNode);
						o.Apply(meshNode);
					}
				}
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00021F20 File Offset: 0x00020320
		public static Vector3 ClosestPointOnNode(MeshNode node, Int3[] vertices, Vector3 pos)
		{
			return Polygon.ClosestPointOnTriangle((Vector3)vertices[node[0]], (Vector3)vertices[node[1]], (Vector3)vertices[node[2]], pos);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00021F78 File Offset: 0x00020378
		public bool ContainsPoint(MeshNode node, Vector3 pos)
		{
			return Polygon.IsClockwise((Vector3)this.vertices[node.v1], (Vector3)this.vertices[node.v2], pos) && Polygon.IsClockwise((Vector3)this.vertices[node.v2], (Vector3)this.vertices[node.v3], pos) && Polygon.IsClockwise((Vector3)this.vertices[node.v3], (Vector3)this.vertices[node.v1], pos);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0002204C File Offset: 0x0002044C
		public static bool ContainsPoint(MeshNode node, Vector3 pos, Int3[] vertices)
		{
			if (!Polygon.IsClockwiseMargin((Vector3)vertices[node.v1], (Vector3)vertices[node.v2], (Vector3)vertices[node.v3]))
			{
				Debug.LogError("Noes!");
			}
			return Polygon.IsClockwiseMargin((Vector3)vertices[node.v1], (Vector3)vertices[node.v2], pos) && Polygon.IsClockwiseMargin((Vector3)vertices[node.v2], (Vector3)vertices[node.v3], pos) && Polygon.IsClockwiseMargin((Vector3)vertices[node.v3], (Vector3)vertices[node.v1], pos);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00022158 File Offset: 0x00020558
		public void Scan(string objMeshPath)
		{
			Mesh x = ObjImporter.ImportFile(objMeshPath);
			if (x == null)
			{
				Debug.LogError("Couldn't read .obj file at '" + objMeshPath + "'");
				return;
			}
			this.sourceMesh = x;
			this.Scan();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0002219C File Offset: 0x0002059C
		public override void Scan()
		{
			if (this.sourceMesh == null)
			{
				return;
			}
			this.GenerateMatrix();
			Vector3[] vertices = this.sourceMesh.vertices;
			this.triangles = this.sourceMesh.triangles;
			NavMeshGraph.GenerateNodes(this, vertices, this.triangles, out this.originalVertices, out this._vertices);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000221F8 File Offset: 0x000205F8
		public static void GenerateNodes(NavGraph graph, Vector3[] vectorVertices, int[] triangles, out Vector3[] originalVertices, out Int3[] vertices)
		{
			if (!(graph is INavmesh))
			{
				Debug.LogError("The specified graph does not implement interface 'INavmesh'");
				originalVertices = vectorVertices;
				vertices = new Int3[0];
				graph.nodes = graph.CreateNodes(0);
				return;
			}
			if (vectorVertices.Length == 0 || triangles.Length == 0)
			{
				originalVertices = vectorVertices;
				vertices = new Int3[0];
				graph.nodes = graph.CreateNodes(0);
				return;
			}
			vertices = new Int3[vectorVertices.Length];
			int num = 0;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = (Int3)graph.matrix.MultiplyPoint(vectorVertices[i]);
			}
			Dictionary<Int3, int> dictionary = new Dictionary<Int3, int>();
			int[] array = new int[vertices.Length];
			for (int j = 0; j < vertices.Length - 1; j++)
			{
				if (!dictionary.ContainsKey(vertices[j]))
				{
					array[num] = j;
					dictionary.Add(vertices[j], num);
					num++;
				}
			}
			array[num] = vertices.Length - 1;
			if (!dictionary.ContainsKey(vertices[array[num]]))
			{
				dictionary.Add(vertices[array[num]], num);
				num++;
			}
			for (int k = 0; k < triangles.Length; k++)
			{
				Int3 key = vertices[triangles[k]];
				triangles[k] = dictionary[key];
			}
			Int3[] array2 = vertices;
			vertices = new Int3[num];
			originalVertices = new Vector3[num];
			for (int l = 0; l < num; l++)
			{
				vertices[l] = array2[array[l]];
				originalVertices[l] = (Vector3)vertices[l];
			}
			Node[] array3 = graph.CreateNodes(triangles.Length / 3);
			graph.nodes = array3;
			for (int m = 0; m < array3.Length; m++)
			{
				MeshNode meshNode = (MeshNode)array3[m];
				meshNode.walkable = true;
				meshNode.position = (vertices[triangles[m * 3]] + vertices[triangles[m * 3 + 1]] + vertices[triangles[m * 3 + 2]]) / 3f;
				meshNode.v1 = triangles[m * 3];
				meshNode.v2 = triangles[m * 3 + 1];
				meshNode.v3 = triangles[m * 3 + 2];
				if (!Polygon.IsClockwise(vertices[meshNode.v1], vertices[meshNode.v2], vertices[meshNode.v3]))
				{
					int v = meshNode.v1;
					meshNode.v1 = meshNode.v3;
					meshNode.v3 = v;
				}
				if (Polygon.IsColinear(vertices[meshNode.v1], vertices[meshNode.v2], vertices[meshNode.v3]))
				{
					Debug.DrawLine((Vector3)vertices[meshNode.v1], (Vector3)vertices[meshNode.v2], Color.red);
					Debug.DrawLine((Vector3)vertices[meshNode.v2], (Vector3)vertices[meshNode.v3], Color.red);
					Debug.DrawLine((Vector3)vertices[meshNode.v3], (Vector3)vertices[meshNode.v1], Color.red);
				}
				array3[m] = meshNode;
			}
			List<Node> list = new List<Node>();
			List<int> list2 = new List<int>();
			int num2 = 0;
			for (int n = 0; n < triangles.Length; n += 3)
			{
				list.Clear();
				list2.Clear();
				Node node = array3[n / 3];
				for (int num3 = 0; num3 < triangles.Length; num3 += 3)
				{
					if (num3 != n)
					{
						int num4 = 0;
						if (triangles[num3] == triangles[n])
						{
							num4++;
						}
						if (triangles[num3 + 1] == triangles[n])
						{
							num4++;
						}
						if (triangles[num3 + 2] == triangles[n])
						{
							num4++;
						}
						if (triangles[num3] == triangles[n + 1])
						{
							num4++;
						}
						if (triangles[num3 + 1] == triangles[n + 1])
						{
							num4++;
						}
						if (triangles[num3 + 2] == triangles[n + 1])
						{
							num4++;
						}
						if (triangles[num3] == triangles[n + 2])
						{
							num4++;
						}
						if (triangles[num3 + 1] == triangles[n + 2])
						{
							num4++;
						}
						if (triangles[num3 + 2] == triangles[n + 2])
						{
							num4++;
						}
						if (num4 >= 3)
						{
							num2++;
							Debug.DrawLine((Vector3)vertices[triangles[num3]], (Vector3)vertices[triangles[num3 + 1]], Color.red);
							Debug.DrawLine((Vector3)vertices[triangles[num3]], (Vector3)vertices[triangles[num3 + 2]], Color.red);
							Debug.DrawLine((Vector3)vertices[triangles[num3 + 2]], (Vector3)vertices[triangles[num3 + 1]], Color.red);
						}
						if (num4 == 2)
						{
							Node node2 = array3[num3 / 3];
							list.Add(node2);
							list2.Add(Mathf.RoundToInt((float)(node.position - node2.position).costMagnitude));
						}
					}
				}
				node.connections = list.ToArray();
				node.connectionCosts = list2.ToArray();
			}
			if (num2 > 0)
			{
				Debug.LogError("One or more triangles are identical to other triangles, this is not a good thing to have in a navmesh\nIncreasing the scale of the mesh might help\nNumber of triangles with error: " + num2 + "\n");
			}
			NavMeshGraph.RebuildBBTree(graph);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00022890 File Offset: 0x00020C90
		public static void RebuildBBTree(NavGraph graph)
		{
			INavmesh navmesh = graph as INavmesh;
			BBTree bbtree = new BBTree(graph as INavmesh);
			for (int i = 0; i < graph.nodes.Length; i++)
			{
				bbtree.Insert(graph.nodes[i] as MeshNode);
			}
			navmesh.bbTree = bbtree;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000228E4 File Offset: 0x00020CE4
		public void PostProcess()
		{
			int num = UnityEngine.Random.Range(0, this.nodes.Length);
			Node node = this.nodes[num];
			NavGraph navGraph;
			if (AstarPath.active.astarData.GetGraphIndex(this) == 0)
			{
				navGraph = AstarPath.active.graphs[1];
			}
			else
			{
				navGraph = AstarPath.active.graphs[0];
			}
			num = UnityEngine.Random.Range(0, navGraph.nodes.Length);
			List<Node> list = new List<Node>();
			List<int> list2 = new List<int>();
			list.AddRange(node.connections);
			list2.AddRange(node.connectionCosts);
			Node node2 = navGraph.nodes[num];
			list.Add(node2);
			list2.Add((node.position - node2.position).costMagnitude);
			node.connections = list.ToArray();
			node.connectionCosts = list2.ToArray();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000229C4 File Offset: 0x00020DC4
		public void Sort(Vector3[] a)
		{
			bool flag = true;
			while (flag)
			{
				flag = false;
				for (int i = 0; i < a.Length - 1; i++)
				{
					if (a[i].x > a[i + 1].x || (a[i].x == a[i + 1].x && (a[i].y > a[i + 1].y || (a[i].y == a[i + 1].y && a[i].z > a[i + 1].z))))
					{
						Vector3 vector = a[i];
						a[i] = a[i + 1];
						a[i + 1] = vector;
						flag = true;
					}
				}
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00022ACC File Offset: 0x00020ECC
		public override void OnDrawGizmos(bool drawNodes)
		{
			if (!drawNodes)
			{
				return;
			}
			Matrix4x4 matrix = this.matrix;
			this.GenerateMatrix();
			if (this.nodes == null)
			{
				this.Scan();
			}
			if (this.nodes == null)
			{
				return;
			}
			if (matrix != this.matrix)
			{
				this.RelocateNodes(matrix, this.matrix);
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				MeshNode meshNode = (MeshNode)this.nodes[i];
				Gizmos.color = this.NodeColor(meshNode, AstarPath.active.debugPathData);
				if (meshNode.walkable)
				{
					if (AstarPath.active.showSearchTree && AstarPath.active.debugPathData != null && meshNode.GetNodeRun(AstarPath.active.debugPathData).parent != null)
					{
						Gizmos.DrawLine((Vector3)meshNode.position, (Vector3)meshNode.GetNodeRun(AstarPath.active.debugPathData).parent.node.position);
					}
					else
					{
						for (int j = 0; j < meshNode.connections.Length; j++)
						{
							Gizmos.DrawLine((Vector3)meshNode.position, (Vector3)meshNode.connections[j].position);
						}
					}
					Gizmos.color = AstarColor.MeshEdgeColor;
				}
				else
				{
					Gizmos.color = Color.red;
				}
				Gizmos.DrawLine((Vector3)this.vertices[meshNode.v1], (Vector3)this.vertices[meshNode.v2]);
				Gizmos.DrawLine((Vector3)this.vertices[meshNode.v2], (Vector3)this.vertices[meshNode.v3]);
				Gizmos.DrawLine((Vector3)this.vertices[meshNode.v3], (Vector3)this.vertices[meshNode.v1]);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00022CE6 File Offset: 0x000210E6
		public override byte[] SerializeExtraInfo()
		{
			return NavMeshGraph.SerializeMeshNodes(this, this.nodes);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00022CF4 File Offset: 0x000210F4
		public override void DeserializeExtraInfo(byte[] bytes)
		{
			NavMeshGraph.DeserializeMeshNodes(this, this.nodes, bytes);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00022D04 File Offset: 0x00021104
		public static void DeserializeMeshNodes(INavmesh graph, Node[] nodes, byte[] bytes)
		{
			MemoryStream input = new MemoryStream(bytes);
			BinaryReader binaryReader = new BinaryReader(input);
			for (int i = 0; i < nodes.Length; i++)
			{
				MeshNode meshNode = nodes[i] as MeshNode;
				if (meshNode == null)
				{
					Debug.LogError("Serialization Error : Couldn't cast the node to the appropriate type - NavMeshGenerator");
					return;
				}
				meshNode.v1 = binaryReader.ReadInt32();
				meshNode.v2 = binaryReader.ReadInt32();
				meshNode.v3 = binaryReader.ReadInt32();
			}
			int num = binaryReader.ReadInt32();
			graph.vertices = new Int3[num];
			for (int j = 0; j < num; j++)
			{
				int x = binaryReader.ReadInt32();
				int y = binaryReader.ReadInt32();
				int z = binaryReader.ReadInt32();
				graph.vertices[j] = new Int3(x, y, z);
			}
			NavMeshGraph.RebuildBBTree(graph as NavGraph);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00022DE0 File Offset: 0x000211E0
		public static byte[] SerializeMeshNodes(INavmesh graph, Node[] nodes)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			for (int i = 0; i < nodes.Length; i++)
			{
				MeshNode meshNode = nodes[i] as MeshNode;
				if (meshNode == null)
				{
					Debug.LogError("Serialization Error : Couldn't cast the node to the appropriate type - NavMeshGenerator. Omitting node data.");
					return null;
				}
				binaryWriter.Write(meshNode.v1);
				binaryWriter.Write(meshNode.v2);
				binaryWriter.Write(meshNode.v3);
			}
			Int3[] array = graph.vertices;
			if (array == null)
			{
				array = new Int3[0];
			}
			binaryWriter.Write(array.Length);
			for (int j = 0; j < array.Length; j++)
			{
				binaryWriter.Write(array[j].x);
				binaryWriter.Write(array[j].y);
				binaryWriter.Write(array[j].z);
			}
			binaryWriter.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00022ED0 File Offset: 0x000212D0
		public static void SerializeMeshNodes(INavmesh graph, Node[] nodes, AstarSerializer serializer)
		{
			BinaryWriter writerStream = serializer.writerStream;
			for (int i = 0; i < nodes.Length; i++)
			{
				MeshNode meshNode = nodes[i] as MeshNode;
				if (meshNode == null)
				{
					Debug.LogError("Serialization Error : Couldn't cast the node to the appropriate type - NavMeshGenerator");
					return;
				}
				writerStream.Write(meshNode.v1);
				writerStream.Write(meshNode.v2);
				writerStream.Write(meshNode.v3);
			}
			Int3[] array = graph.vertices;
			if (array == null)
			{
				array = new Int3[0];
			}
			writerStream.Write(array.Length);
			for (int j = 0; j < array.Length; j++)
			{
				writerStream.Write(array[j].x);
				writerStream.Write(array[j].y);
				writerStream.Write(array[j].z);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00022FA4 File Offset: 0x000213A4
		public static void DeSerializeMeshNodes(INavmesh graph, Node[] nodes, AstarSerializer serializer)
		{
			BinaryReader readerStream = serializer.readerStream;
			for (int i = 0; i < nodes.Length; i++)
			{
				MeshNode meshNode = nodes[i] as MeshNode;
				if (meshNode == null)
				{
					Debug.LogError("Serialization Error : Couldn't cast the node to the appropriate type - NavMeshGenerator");
					return;
				}
				meshNode.v1 = readerStream.ReadInt32();
				meshNode.v2 = readerStream.ReadInt32();
				meshNode.v3 = readerStream.ReadInt32();
			}
			int num = readerStream.ReadInt32();
			graph.vertices = new Int3[num];
			for (int j = 0; j < num; j++)
			{
				int x = readerStream.ReadInt32();
				int y = readerStream.ReadInt32();
				int z = readerStream.ReadInt32();
				graph.vertices[j] = new Int3(x, y, z);
			}
			NavMeshGraph.RebuildBBTree(graph as NavGraph);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00023073 File Offset: 0x00021473
		public void SerializeNodes(Node[] nodes, AstarSerializer serializer)
		{
			NavMeshGraph.SerializeMeshNodes(this, nodes, serializer);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0002307D File Offset: 0x0002147D
		public void DeSerializeNodes(Node[] nodes, AstarSerializer serializer)
		{
			NavMeshGraph.DeSerializeMeshNodes(this, nodes, serializer);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00023088 File Offset: 0x00021488
		public void SerializeSettings(AstarSerializer serializer)
		{
			BinaryWriter writerStream = serializer.writerStream;
			serializer.AddValue("offset", this.offset);
			serializer.AddValue("rotation", this.rotation);
			serializer.AddValue("scale", this.scale);
			if (this.sourceMesh != null)
			{
				Vector3[] vertices = this.sourceMesh.vertices;
				int[] array = this.sourceMesh.triangles;
				writerStream.Write(vertices.Length);
				writerStream.Write(array.Length);
				for (int i = 0; i < vertices.Length; i++)
				{
					writerStream.Write(vertices[i].x);
					writerStream.Write(vertices[i].y);
					writerStream.Write(vertices[i].z);
				}
				for (int j = 0; j < array.Length; j++)
				{
					writerStream.Write(array[j]);
				}
			}
			else
			{
				writerStream.Write(0);
				writerStream.Write(0);
			}
			serializer.AddUnityReferenceValue("sourceMesh", this.sourceMesh);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000231AC File Offset: 0x000215AC
		public void DeSerializeSettings(AstarSerializer serializer)
		{
			BinaryReader readerStream = serializer.readerStream;
			this.offset = (Vector3)serializer.GetValue("offset", typeof(Vector3), null);
			this.rotation = (Vector3)serializer.GetValue("rotation", typeof(Vector3), null);
			this.scale = (float)serializer.GetValue("scale", typeof(float), null);
			this.GenerateMatrix();
			Vector3[] array = new Vector3[readerStream.ReadInt32()];
			int[] array2 = new int[readerStream.ReadInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new Vector3(readerStream.ReadSingle(), readerStream.ReadSingle(), readerStream.ReadSingle());
			}
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = readerStream.ReadInt32();
			}
			this.sourceMesh = (serializer.GetUnityReferenceValue("sourceMesh", typeof(Mesh), null) as Mesh);
			if (Application.isPlaying)
			{
				this.sourceMesh = new Mesh();
				this.sourceMesh.name = "NavGraph Mesh";
				this.sourceMesh.vertices = array;
				this.sourceMesh.triangles = array2;
			}
		}

		// Token: 0x0400030F RID: 783
		[JsonMember]
		public Mesh sourceMesh;

		// Token: 0x04000310 RID: 784
		[JsonMember]
		public Vector3 offset;

		// Token: 0x04000311 RID: 785
		[JsonMember]
		public Vector3 rotation;

		// Token: 0x04000312 RID: 786
		[JsonMember]
		public float scale = 1f;

		// Token: 0x04000313 RID: 787
		[JsonMember]
		public bool accurateNearestNode = true;

		// Token: 0x04000314 RID: 788
		private BBTree _bbTree;

		// Token: 0x04000315 RID: 789
		[NonSerialized]
		private Int3[] _vertices;

		// Token: 0x04000316 RID: 790
		[NonSerialized]
		private Vector3[] originalVertices;

		// Token: 0x04000317 RID: 791
		private Matrix4x4 _originalMatrix;

		// Token: 0x04000318 RID: 792
		[NonSerialized]
		public int[] triangles;
	}
}
