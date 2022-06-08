using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Nodes;
using Pathfinding.Serialization.JsonFx;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000069 RID: 105
	[JsonOptIn]
	[Serializable]
	public class GridGraph : NavGraph, ISerializableGraph, IUpdatableGraph, IFunnelGraph, IRaycastableGraph, ISerializableObject
	{
		// Token: 0x06000359 RID: 857 RVA: 0x00019F20 File Offset: 0x00018320
		public GridGraph()
		{
			this.unclampedSize = new Vector2(10f, 10f);
			this.nodeSize = 1f;
			this.collision = new GraphCollision();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00019FDC File Offset: 0x000183DC
		public override Node[] CreateNodes(int number)
		{
			GridNode[] array = new GridNode[number];
			for (int i = 0; i < number; i++)
			{
				array[i] = new GridNode();
				array[i].penalty = this.initialPenalty;
			}
			this.nodes = array;
			return array;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001A020 File Offset: 0x00018420
		public override void OnDestroy()
		{
			base.OnDestroy();
			this.RemoveGridGraphFromStatic();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001A02E File Offset: 0x0001842E
		public void RemoveGridGraphFromStatic()
		{
			GridNode.RemoveGridGraph(this);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0001A036 File Offset: 0x00018436
		public virtual bool uniformWidhtDepthGrid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0001A039 File Offset: 0x00018439
		public bool useRaycastNormal
		{
			get
			{
				return Math.Abs(90f - this.maxSlope) > float.Epsilon;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001A053 File Offset: 0x00018453
		public void UpdateSizeFromWidthDepth()
		{
			this.unclampedSize = new Vector2((float)this.width, (float)this.depth) * this.nodeSize;
			this.GenerateMatrix();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001A080 File Offset: 0x00018480
		public void GenerateMatrix()
		{
			this.size = this.unclampedSize;
			this.size.x = this.size.x * Mathf.Sign(this.size.x);
			this.size.y = this.size.y * Mathf.Sign(this.size.y);
			this.nodeSize = Mathf.Clamp(this.nodeSize, this.size.x / 1024f, float.PositiveInfinity);
			this.nodeSize = Mathf.Clamp(this.nodeSize, this.size.y / 1024f, float.PositiveInfinity);
			this.size.x = ((this.size.x >= this.nodeSize) ? this.size.x : this.nodeSize);
			this.size.y = ((this.size.y >= this.nodeSize) ? this.size.y : this.nodeSize);
			this.boundsMatrix.SetTRS(this.center, Quaternion.Euler(this.rotation), new Vector3(this.aspectRatio, 1f, 1f));
			this.width = Mathf.FloorToInt(this.size.x / this.nodeSize);
			this.depth = Mathf.FloorToInt(this.size.y / this.nodeSize);
			if (Mathf.Approximately(this.size.x / this.nodeSize, (float)Mathf.CeilToInt(this.size.x / this.nodeSize)))
			{
				this.width = Mathf.CeilToInt(this.size.x / this.nodeSize);
			}
			if (Mathf.Approximately(this.size.y / this.nodeSize, (float)Mathf.CeilToInt(this.size.y / this.nodeSize)))
			{
				this.depth = Mathf.CeilToInt(this.size.y / this.nodeSize);
			}
			this.matrix.SetTRS(this.boundsMatrix.MultiplyPoint3x4(-new Vector3(this.size.x, 0f, this.size.y) * 0.5f), Quaternion.Euler(this.rotation), new Vector3(this.nodeSize * this.aspectRatio, 1f, this.nodeSize));
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001A314 File Offset: 0x00018714
		public override NNInfo GetNearest(Vector3 position, NNConstraint constraint, Node hint)
		{
			if (this.nodes == null || this.depth * this.width != this.nodes.Length)
			{
				return default(NNInfo);
			}
			position = base.inverseMatrix.MultiplyPoint3x4(position);
			float f = position.x - 0.5f;
			float f2 = position.z - 0.5f;
			int num = Mathf.Clamp(Mathf.RoundToInt(f), 0, this.width - 1);
			int num2 = Mathf.Clamp(Mathf.RoundToInt(f2), 0, this.depth - 1);
			NNInfo result = new NNInfo(this.nodes[num2 * this.width + num]);
			return result;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001A3C8 File Offset: 0x000187C8
		public override NNInfo GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			if (this.nodes == null || this.depth * this.width != this.nodes.Length)
			{
				return default(NNInfo);
			}
			Vector3 b = position;
			position = base.inverseMatrix.MultiplyPoint3x4(position);
			int num = Mathf.Clamp(Mathf.RoundToInt(position.x - 0.5f), 0, this.width - 1);
			int num2 = Mathf.Clamp(Mathf.RoundToInt(position.z - 0.5f), 0, this.depth - 1);
			Node node = this.nodes[num + num2 * this.width];
			Node node2 = null;
			float num3 = float.PositiveInfinity;
			int num4 = this.getNearestForceOverlap;
			if (constraint.Suitable(node))
			{
				node2 = node;
				num3 = ((Vector3)node2.position - b).sqrMagnitude;
			}
			if (node2 != null)
			{
				if (num4 == 0)
				{
					return new NNInfo(node2);
				}
				num4--;
			}
			float num5 = (!constraint.constrainDistance) ? float.PositiveInfinity : AstarPath.active.maxNearestNodeDistance;
			float num6 = num5 * num5;
			int num7 = 1;
			for (;;)
			{
				int i = num2 + num7;
				int num8 = i * this.width;
				if (this.nodeSize * (float)num7 > num5)
				{
					break;
				}
				bool flag = false;
				int j;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint.Suitable(this.nodes[j + num8]))
						{
							float sqrMagnitude = ((Vector3)this.nodes[j + num8].position - b).sqrMagnitude;
							if (sqrMagnitude < num3 && sqrMagnitude < num6)
							{
								num3 = sqrMagnitude;
								node2 = this.nodes[j + num8];
							}
						}
					}
				}
				i = num2 - num7;
				num8 = i * this.width;
				for (j = num - num7; j <= num + num7; j++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint.Suitable(this.nodes[j + num8]))
						{
							float sqrMagnitude2 = ((Vector3)this.nodes[j + num8].position - b).sqrMagnitude;
							if (sqrMagnitude2 < num3 && sqrMagnitude2 < num6)
							{
								num3 = sqrMagnitude2;
								node2 = this.nodes[j + num8];
							}
						}
					}
				}
				j = num - num7;
				i = num2 - num7 + 1;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint.Suitable(this.nodes[j + i * this.width]))
						{
							float sqrMagnitude3 = ((Vector3)this.nodes[j + i * this.width].position - b).sqrMagnitude;
							if (sqrMagnitude3 < num3 && sqrMagnitude3 < num6)
							{
								num3 = sqrMagnitude3;
								node2 = this.nodes[j + i * this.width];
							}
						}
					}
				}
				j = num + num7;
				for (i = num2 - num7 + 1; i <= num2 + num7 - 1; i++)
				{
					if (j >= 0 && i >= 0 && j < this.width && i < this.depth)
					{
						flag = true;
						if (constraint.Suitable(this.nodes[j + i * this.width]))
						{
							float sqrMagnitude4 = ((Vector3)this.nodes[j + i * this.width].position - b).sqrMagnitude;
							if (sqrMagnitude4 < num3 && sqrMagnitude4 < num6)
							{
								num3 = sqrMagnitude4;
								node2 = this.nodes[j + i * this.width];
							}
						}
					}
				}
				if (node2 != null)
				{
					if (num4 == 0)
					{
						goto Block_36;
					}
					num4--;
				}
				if (!flag)
				{
					goto Block_37;
				}
				num7++;
			}
			return new NNInfo(node2);
			Block_36:
			return new NNInfo(node2);
			Block_37:
			return new NNInfo(node2);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001A880 File Offset: 0x00018C80
		public virtual void SetUpOffsetsAndCosts()
		{
			this.neighbourOffsets = new int[]
			{
				-this.width,
				1,
				this.width,
				-1,
				-this.width + 1,
				this.width + 1,
				this.width - 1,
				-this.width - 1
			};
			int num = Mathf.RoundToInt(this.nodeSize * 1000f);
			int num2 = Mathf.RoundToInt(this.nodeSize * Mathf.Sqrt(2f) * 1000f);
			this.neighbourCosts = new int[]
			{
				num,
				num,
				num,
				num,
				num2,
				num2,
				num2,
				num2
			};
			this.neighbourXOffsets = new int[]
			{
				0,
				1,
				0,
				-1,
				1,
				1,
				-1,
				-1
			};
			this.neighbourZOffsets = new int[]
			{
				-1,
				0,
				1,
				0,
				-1,
				1,
				1,
				-1
			};
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001A96C File Offset: 0x00018D6C
		public override void Scan()
		{
			AstarPath.OnPostScan = (OnScanDelegate)Delegate.Combine(AstarPath.OnPostScan, new OnScanDelegate(this.OnPostScan));
			this.scans++;
			if (this.nodeSize <= 0f)
			{
				return;
			}
			this.GenerateMatrix();
			if (this.width > 1024 || this.depth > 1024)
			{
				Debug.LogError("One of the grid's sides is longer than 1024 nodes");
				return;
			}
			this.SetUpOffsetsAndCosts();
			int gridIndex = GridNode.SetGridGraph(this);
			this.nodes = this.CreateNodes(this.width * this.depth);
			GridNode[] array = this.nodes as GridNode[];
			if (this.collision == null)
			{
				this.collision = new GraphCollision();
			}
			this.collision.Initialize(this.matrix, this.nodeSize);
			this.textureData.Initialize();
			for (int i = 0; i < this.depth; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					GridNode gridNode = array[i * this.width + j];
					gridNode.SetIndex(i * this.width + j);
					this.UpdateNodePositionCollision(gridNode, j, i);
					this.textureData.Apply(gridNode, j, i);
					gridNode.SetGridIndex(gridIndex);
				}
			}
			for (int k = 0; k < this.depth; k++)
			{
				for (int l = 0; l < this.width; l++)
				{
					GridNode node = array[k * this.width + l];
					this.CalculateConnections(this.nodes, l, k, node);
				}
			}
			this.ErodeWalkableArea();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001AB20 File Offset: 0x00018F20
		public void UpdateNodePositionCollision(Node node, int x, int z)
		{
			node.position = (Int3)this.matrix.MultiplyPoint3x4(new Vector3((float)x + 0.5f, 0f, (float)z + 0.5f));
			bool flag = true;
			RaycastHit raycastHit;
			node.position = (Int3)this.collision.CheckHeight((Vector3)node.position, out raycastHit, out flag);
			node.penalty = this.initialPenalty;
			if (this.penaltyPosition)
			{
				node.penalty += (uint)Mathf.RoundToInt(((float)node.position.y - this.penaltyPositionOffset) * this.penaltyPositionFactor);
			}
			if (flag && this.useRaycastNormal && this.collision.heightCheck && raycastHit.normal != Vector3.zero)
			{
				float num = Vector3.Dot(raycastHit.normal.normalized, this.collision.up);
				if (this.penaltyAngle)
				{
					node.penalty += (uint)Mathf.RoundToInt((1f - num) * this.penaltyAngleFactor);
				}
				float num2 = Mathf.Cos(this.maxSlope * 0.017453292f);
				if (num < num2)
				{
					flag = false;
				}
			}
			if (flag)
			{
				node.walkable = this.collision.Check((Vector3)node.position);
			}
			else
			{
				node.walkable = flag;
			}
			node.Bit15 = node.walkable;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001ACA0 File Offset: 0x000190A0
		public virtual void ErodeWalkableArea()
		{
			this.ErodeWalkableArea(0, 0, this.width, this.depth);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001ACB8 File Offset: 0x000190B8
		public virtual void ErodeWalkableArea(int xmin, int zmin, int xmax, int zmax)
		{
			xmin = ((xmin >= 0) ? ((xmin <= this.width) ? xmin : this.width) : 0);
			xmax = ((xmax >= 0) ? ((xmax <= this.width) ? xmax : this.width) : 0);
			zmin = ((zmin >= 0) ? ((zmin <= this.depth) ? zmin : this.depth) : 0);
			zmax = ((zmax >= 0) ? ((zmax <= this.depth) ? zmax : this.depth) : 0);
			if (!this.erosionUseTags)
			{
				for (int i = 0; i < this.erodeIterations; i++)
				{
					for (int j = zmin; j < zmax; j++)
					{
						for (int k = xmin; k < xmax; k++)
						{
							GridNode gridNode = this.nodes[j * this.width + k] as GridNode;
							if (gridNode.walkable)
							{
								bool flag = false;
								for (int l = 0; l < 4; l++)
								{
									if (!gridNode.GetConnection(l))
									{
										flag = true;
										break;
									}
								}
								if (flag)
								{
									gridNode.walkable = false;
								}
							}
						}
					}
					for (int m = zmin; m < zmax; m++)
					{
						for (int n = xmin; n < xmax; n++)
						{
							GridNode node = this.nodes[m * this.width + n] as GridNode;
							this.CalculateConnections(this.nodes, n, m, node);
						}
					}
				}
			}
			else
			{
				if (this.erodeIterations + this.erosionFirstTag > 31)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Too few tags available for ",
						this.erodeIterations,
						" erode iterations and starting with tag ",
						this.erosionFirstTag,
						" (erodeIterations+erosionFirstTag > 31)"
					}));
					return;
				}
				if (this.erosionFirstTag <= 0)
				{
					Debug.LogError("First erosion tag must be greater or equal to 1");
					return;
				}
				for (int num = 0; num < this.erodeIterations; num++)
				{
					for (int num2 = zmin; num2 < zmax; num2++)
					{
						for (int num3 = xmin; num3 < xmax; num3++)
						{
							GridNode gridNode2 = this.nodes[num2 * this.width + num3] as GridNode;
							if (gridNode2.walkable && gridNode2.tags >= this.erosionFirstTag && gridNode2.tags < this.erosionFirstTag + num)
							{
								int index = gridNode2.GetIndex();
								for (int num4 = 0; num4 < 4; num4++)
								{
									if (gridNode2.GetConnection(num4))
									{
										int tags = this.nodes[index + this.neighbourOffsets[num4]].tags;
										if (tags > this.erosionFirstTag + num || tags < this.erosionFirstTag)
										{
											this.nodes[index + this.neighbourOffsets[num4]].tags = this.erosionFirstTag + num;
										}
									}
								}
							}
							else if (gridNode2.walkable && num == 0)
							{
								bool flag2 = false;
								for (int num5 = 0; num5 < 4; num5++)
								{
									if (!gridNode2.GetConnection(num5))
									{
										flag2 = true;
										break;
									}
								}
								if (flag2)
								{
									gridNode2.tags = this.erosionFirstTag + num;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001B054 File Offset: 0x00019454
		public virtual bool IsValidConnection(GridNode n1, GridNode n2)
		{
			return n1.walkable && n2.walkable && (this.maxClimb == 0f || (float)Mathf.Abs(n1.position[this.maxClimbAxis] - n2.position[this.maxClimbAxis]) <= this.maxClimb * 1000f);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001B0C8 File Offset: 0x000194C8
		public static void CalculateConnections(GridNode node)
		{
			GridGraph gridGraph = AstarData.GetGraph(node) as GridGraph;
			if (gridGraph != null)
			{
				int index = node.GetIndex();
				int x = index % gridGraph.width;
				int z = index / gridGraph.width;
				gridGraph.CalculateConnections(gridGraph.nodes, x, z, node);
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001B110 File Offset: 0x00019510
		public virtual void CalculateConnections(Node[] nodes, int x, int z, GridNode node)
		{
			node.flags &= -256;
			if (!node.walkable)
			{
				return;
			}
			int index = node.GetIndex();
			if (this.corners == null)
			{
				this.corners = new int[4];
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					this.corners[i] = 0;
				}
			}
			int j = 0;
			int num = 3;
			while (j < 4)
			{
				int num2 = x + this.neighbourXOffsets[j];
				int num3 = z + this.neighbourZOffsets[j];
				if (num2 >= 0 && num3 >= 0 && num2 < this.width && num3 < this.depth)
				{
					GridNode n = nodes[index + this.neighbourOffsets[j]] as GridNode;
					if (this.IsValidConnection(node, n))
					{
						node.SetConnectionRaw(j, 1);
						this.corners[j]++;
						this.corners[num]++;
					}
				}
				num = j;
				j++;
			}
			if (this.neighbours == NumNeighbours.Eight)
			{
				if (this.cutCorners)
				{
					for (int k = 0; k < 4; k++)
					{
						if (this.corners[k] >= 1)
						{
							int num4 = x + this.neighbourXOffsets[k + 4];
							int num5 = z + this.neighbourZOffsets[k + 4];
							if (num4 >= 0 && num5 >= 0 && num4 < this.width && num5 < this.depth)
							{
								GridNode n2 = nodes[index + this.neighbourOffsets[k + 4]] as GridNode;
								if (this.IsValidConnection(node, n2))
								{
									node.SetConnectionRaw(k + 4, 1);
								}
							}
						}
					}
				}
				else
				{
					for (int l = 0; l < 4; l++)
					{
						if (this.corners[l] == 2)
						{
							GridNode n3 = nodes[index + this.neighbourOffsets[l + 4]] as GridNode;
							if (this.IsValidConnection(node, n3))
							{
								node.SetConnectionRaw(l + 4, 1);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001B33C File Offset: 0x0001973C
		public void OnPostScan(AstarPath script)
		{
			AstarPath.OnPostScan = (OnScanDelegate)Delegate.Remove(AstarPath.OnPostScan, new OnScanDelegate(this.OnPostScan));
			if (!this.autoLinkGrids || this.autoLinkDistLimit <= 0f)
			{
				return;
			}
			int num = Mathf.RoundToInt(this.autoLinkDistLimit * 1000f);
			IEnumerator enumerator = script.astarData.FindGraphsOfType(typeof(GridGraph)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					GridGraph gridGraph = (GridGraph)obj;
					if (gridGraph != this && gridGraph.nodes != null && this.nodes != null)
					{
						for (int i = 0; i < this.width; i++)
						{
							Node node = this.nodes[i];
							Node node2 = gridGraph.GetNearest((Vector3)node.position).node;
							if (base.inverseMatrix.MultiplyPoint3x4((Vector3)node2.position).z <= 0f)
							{
								int costMagnitude = (node.position - node2.position).costMagnitude;
								if (costMagnitude <= num)
								{
									node.AddConnection(node2, costMagnitude);
									node2.AddConnection(node, costMagnitude);
								}
							}
						}
						for (int j = 0; j < this.depth; j++)
						{
							Node node3 = this.nodes[j * this.width];
							Node node4 = gridGraph.GetNearest((Vector3)node3.position).node;
							if (base.inverseMatrix.MultiplyPoint3x4((Vector3)node4.position).x <= 0f)
							{
								int costMagnitude2 = (node3.position - node4.position).costMagnitude;
								if (costMagnitude2 <= num)
								{
									node3.AddConnection(node4, costMagnitude2);
									node4.AddConnection(node3, costMagnitude2);
								}
							}
						}
						for (int k = 0; k < this.width; k++)
						{
							Node node5 = this.nodes[(this.depth - 1) * this.width + k];
							Node node6 = gridGraph.GetNearest((Vector3)node5.position).node;
							if (base.inverseMatrix.MultiplyPoint3x4((Vector3)node6.position).z >= (float)(this.depth - 1))
							{
								int costMagnitude3 = (node5.position - node6.position).costMagnitude;
								if (costMagnitude3 <= num)
								{
									node5.AddConnection(node6, costMagnitude3);
									node6.AddConnection(node5, costMagnitude3);
								}
							}
						}
						for (int l = 0; l < this.depth; l++)
						{
							Node node7 = this.nodes[l * this.width + this.width - 1];
							Node node8 = gridGraph.GetNearest((Vector3)node7.position).node;
							if (base.inverseMatrix.MultiplyPoint3x4((Vector3)node8.position).x >= (float)(this.width - 1))
							{
								int costMagnitude4 = (node7.position - node8.position).costMagnitude;
								if (costMagnitude4 <= num)
								{
									node7.AddConnection(node8, costMagnitude4);
									node8.AddConnection(node7, costMagnitude4);
								}
							}
						}
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001B730 File Offset: 0x00019B30
		public override void OnDrawGizmos(bool drawNodes)
		{
			Gizmos.matrix = this.boundsMatrix;
			Gizmos.color = Color.white;
			Gizmos.DrawWireCube(Vector3.zero, new Vector3(this.size.x, 0f, this.size.y));
			Gizmos.matrix = Matrix4x4.identity;
			if (!drawNodes)
			{
				return;
			}
			if (this.nodes == null || this.depth * this.width != this.nodes.Length)
			{
				return;
			}
			base.OnDrawGizmos(drawNodes);
			for (int i = 0; i < this.depth; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					GridNode gridNode = this.nodes[i * this.width + j] as GridNode;
					if (gridNode.walkable)
					{
						Gizmos.color = this.NodeColor(gridNode, AstarPath.active.debugPathData);
						if (AstarPath.active.showSearchTree && AstarPath.active.debugPathData != null)
						{
							if (base.InSearchTree(gridNode, AstarPath.active.debugPath))
							{
								NodeRun nodeRun = gridNode.GetNodeRun(AstarPath.active.debugPathData);
								NodeRun parent = nodeRun.parent;
								if (parent != null)
								{
									Gizmos.DrawLine((Vector3)gridNode.position, (Vector3)parent.node.position);
								}
							}
						}
						else
						{
							for (int k = 0; k < 8; k++)
							{
								if (gridNode.GetConnection(k))
								{
									GridNode gridNode2 = this.nodes[gridNode.GetIndex() + this.neighbourOffsets[k]] as GridNode;
									Gizmos.DrawLine((Vector3)gridNode.position, (Vector3)gridNode2.position);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001B8FC File Offset: 0x00019CFC
		public void GetBoundsMinMax(Bounds b, Matrix4x4 matrix, out Vector3 min, out Vector3 max)
		{
			Vector3[] array = new Vector3[]
			{
				matrix.MultiplyPoint3x4(b.center + new Vector3(b.extents.x, b.extents.y, b.extents.z)),
				matrix.MultiplyPoint3x4(b.center + new Vector3(b.extents.x, b.extents.y, -b.extents.z)),
				matrix.MultiplyPoint3x4(b.center + new Vector3(b.extents.x, -b.extents.y, b.extents.z)),
				matrix.MultiplyPoint3x4(b.center + new Vector3(b.extents.x, -b.extents.y, -b.extents.z)),
				matrix.MultiplyPoint3x4(b.center + new Vector3(-b.extents.x, b.extents.y, b.extents.z)),
				matrix.MultiplyPoint3x4(b.center + new Vector3(-b.extents.x, b.extents.y, -b.extents.z)),
				matrix.MultiplyPoint3x4(b.center + new Vector3(-b.extents.x, -b.extents.y, b.extents.z)),
				matrix.MultiplyPoint3x4(b.center + new Vector3(-b.extents.x, -b.extents.y, -b.extents.z))
			};
			min = array[0];
			max = array[0];
			for (int i = 1; i < 8; i++)
			{
				min = Vector3.Min(min, array[i]);
				max = Vector3.Max(max, array[i]);
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001BC32 File Offset: 0x0001A032
		public List<Node> GetNodesInArea(Bounds b)
		{
			return this.GetNodesInArea(b, null);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001BC3C File Offset: 0x0001A03C
		public List<Node> GetNodesInArea(GraphUpdateShape shape)
		{
			return this.GetNodesInArea(shape.GetBounds(), shape);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001BC4C File Offset: 0x0001A04C
		private List<Node> GetNodesInArea(Bounds b, GraphUpdateShape shape)
		{
			if (this.nodes == null || this.width * this.depth != this.nodes.Length)
			{
				return null;
			}
			List<Node> list = ListPool<Node>.Claim();
			Vector3 vector;
			Vector3 vector2;
			this.GetBoundsMinMax(b, base.inverseMatrix, out vector, out vector2);
			int xmin = Mathf.RoundToInt(vector.x - 0.5f);
			int xmax = Mathf.RoundToInt(vector2.x - 0.5f);
			int ymin = Mathf.RoundToInt(vector.z - 0.5f);
			int ymax = Mathf.RoundToInt(vector2.z - 0.5f);
			IntRect a = new IntRect(xmin, ymin, xmax, ymax);
			IntRect b2 = new IntRect(0, 0, this.width - 1, this.depth - 1);
			IntRect intRect = IntRect.Intersection(a, b2);
			for (int i = intRect.xmin; i <= intRect.xmax; i++)
			{
				for (int j = intRect.ymin; j <= intRect.ymax; j++)
				{
					int num = j * this.width + i;
					Node node = this.nodes[num];
					if (b.Contains((Vector3)node.position) && (shape == null || shape.Contains((Vector3)node.position)))
					{
						list.Add(node);
					}
				}
			}
			return list;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001BDB4 File Offset: 0x0001A1B4
		public void UpdateArea(GraphUpdateObject o)
		{
			if (this.nodes == null || this.nodes.Length != this.width * this.depth)
			{
				Debug.LogWarning("The Grid Graph is not scanned, cannot update area ");
				return;
			}
			Bounds b = o.bounds;
			Vector3 a;
			Vector3 a2;
			this.GetBoundsMinMax(b, base.inverseMatrix, out a, out a2);
			int xmin = Mathf.RoundToInt(a.x - 0.5f);
			int xmax = Mathf.RoundToInt(a2.x - 0.5f);
			int ymin = Mathf.RoundToInt(a.z - 0.5f);
			int ymax = Mathf.RoundToInt(a2.z - 0.5f);
			IntRect intRect = new IntRect(xmin, ymin, xmax, ymax);
			IntRect intRect2 = intRect;
			IntRect b2 = new IntRect(0, 0, this.width - 1, this.depth - 1);
			IntRect intRect3 = intRect;
			int num = (!o.updateErosion) ? 0 : this.erodeIterations;
			bool flag = o.updatePhysics || o.modifyWalkability;
			if (o.updatePhysics && !o.modifyWalkability && this.collision.collisionCheck)
			{
				Vector3 a3 = new Vector3(this.collision.diameter, 0f, this.collision.diameter) * 0.5f;
				a -= a3 * 1.02f;
				a2 += a3 * 1.02f;
				intRect3 = new IntRect(Mathf.RoundToInt(a.x - 0.5f), Mathf.RoundToInt(a.z - 0.5f), Mathf.RoundToInt(a2.x - 0.5f), Mathf.RoundToInt(a2.z - 0.5f));
				intRect2 = IntRect.Union(intRect3, intRect2);
			}
			if (flag || num > 0)
			{
				intRect2 = intRect2.Expand(num + 1);
			}
			IntRect intRect4 = IntRect.Intersection(intRect2, b2);
			for (int i = intRect4.xmin; i <= intRect4.xmax; i++)
			{
				for (int j = intRect4.ymin; j <= intRect4.ymax; j++)
				{
					o.WillUpdateNode(this.nodes[j * this.width + i]);
				}
			}
			if (o.updatePhysics && !o.modifyWalkability)
			{
				this.collision.Initialize(this.matrix, this.nodeSize);
				intRect4 = IntRect.Intersection(intRect3, b2);
				for (int k = intRect4.xmin; k <= intRect4.xmax; k++)
				{
					for (int l = intRect4.ymin; l <= intRect4.ymax; l++)
					{
						int num2 = l * this.width + k;
						GridNode node = this.nodes[num2] as GridNode;
						this.UpdateNodePositionCollision(node, k, l);
					}
				}
			}
			intRect4 = IntRect.Intersection(intRect, b2);
			for (int m = intRect4.xmin; m <= intRect4.xmax; m++)
			{
				for (int n = intRect4.ymin; n <= intRect4.ymax; n++)
				{
					int num3 = n * this.width + m;
					GridNode gridNode = this.nodes[num3] as GridNode;
					if (flag)
					{
						gridNode.walkable = gridNode.WalkableErosion;
						o.Apply(gridNode);
						gridNode.WalkableErosion = gridNode.walkable;
					}
					else
					{
						o.Apply(gridNode);
					}
				}
			}
			if (flag && num == 0)
			{
				intRect4 = IntRect.Intersection(intRect2, b2);
				for (int num4 = intRect4.xmin; num4 <= intRect4.xmax; num4++)
				{
					for (int num5 = intRect4.ymin; num5 <= intRect4.ymax; num5++)
					{
						int num6 = num5 * this.width + num4;
						GridNode node2 = this.nodes[num6] as GridNode;
						this.CalculateConnections(this.nodes, num4, num5, node2);
					}
				}
			}
			else if (flag && num > 0)
			{
				IntRect a4 = IntRect.Union(intRect, intRect3).Expand(num);
				IntRect a5 = a4.Expand(num);
				a4 = IntRect.Intersection(a4, b2);
				a5 = IntRect.Intersection(a5, b2);
				for (int num7 = a5.xmin; num7 <= a5.xmax; num7++)
				{
					for (int num8 = a5.ymin; num8 <= a5.ymax; num8++)
					{
						int num9 = num8 * this.width + num7;
						GridNode gridNode2 = this.nodes[num9] as GridNode;
						bool walkable = gridNode2.walkable;
						gridNode2.walkable = gridNode2.WalkableErosion;
						if (!a4.Contains(num7, num8))
						{
							gridNode2.Bit16 = walkable;
						}
					}
				}
				for (int num10 = a5.xmin; num10 <= a5.xmax; num10++)
				{
					for (int num11 = a5.ymin; num11 <= a5.ymax; num11++)
					{
						int num12 = num11 * this.width + num10;
						GridNode node3 = this.nodes[num12] as GridNode;
						this.CalculateConnections(this.nodes, num10, num11, node3);
					}
				}
				this.ErodeWalkableArea(a5.xmin, a5.ymin, a5.xmax + 1, a5.ymax + 1);
				for (int num13 = a5.xmin; num13 <= a5.xmax; num13++)
				{
					for (int num14 = a5.ymin; num14 <= a5.ymax; num14++)
					{
						if (!a4.Contains(num13, num14))
						{
							int num15 = num14 * this.width + num13;
							GridNode gridNode3 = this.nodes[num15] as GridNode;
							gridNode3.walkable = gridNode3.Bit16;
						}
					}
				}
				for (int num16 = a5.xmin; num16 <= a5.xmax; num16++)
				{
					for (int num17 = a5.ymin; num17 <= a5.ymax; num17++)
					{
						int num18 = num17 * this.width + num16;
						GridNode node4 = this.nodes[num18] as GridNode;
						this.CalculateConnections(this.nodes, num16, num17, node4);
					}
				}
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001C454 File Offset: 0x0001A854
		public void BuildFunnelCorridor(List<Node> path, int sIndex, int eIndex, List<Vector3> left, List<Vector3> right)
		{
			for (int i = sIndex; i < eIndex; i++)
			{
				GridNode n = path[i] as GridNode;
				GridNode n2 = path[i + 1] as GridNode;
				this.AddPortal(n, n2, left, right);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001C49B File Offset: 0x0001A89B
		public void AddPortal(Node n1, Node n2, List<Vector3> left, List<Vector3> right)
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001C4A0 File Offset: 0x0001A8A0
		public void AddPortal(GridNode n1, GridNode n2, List<Vector3> left, List<Vector3> right)
		{
			if (n1 == n2)
			{
				return;
			}
			int index = n1.GetIndex();
			int index2 = n2.GetIndex();
			int num = index % this.width;
			int num2 = index2 % this.width;
			int num3 = index / this.width;
			int num4 = index2 / this.width;
			Vector3 vector = (Vector3)n1.position;
			Vector3 vector2 = (Vector3)n2.position;
			int num5 = Mathf.Abs(num - num2);
			int num6 = Mathf.Abs(num3 - num4);
			if (num5 > 1 || num6 > 1)
			{
				left.Add(vector);
				right.Add(vector);
				left.Add(vector2);
				right.Add(vector2);
			}
			else if (num5 + num6 <= 1)
			{
				Vector3 vector3 = (vector2 - vector).normalized * this.nodeSize * 0.5f;
				Vector3 b = Vector3.Cross(vector3, Vector3.up).normalized * this.nodeSize * 0.5f;
				left.Add(vector + vector3 - b);
				right.Add(vector + vector3 + b);
			}
			else
			{
				Node node = this.nodes[num3 * this.width + num2];
				Node node2 = this.nodes[num4 * this.width + num];
				Node node3 = null;
				if (node.walkable)
				{
					node3 = node;
				}
				else if (node2.walkable)
				{
					node3 = node2;
				}
				if (node3 == null)
				{
					Vector3 item = (vector + vector2) * 0.5f;
					left.Add(item);
					right.Add(item);
				}
				else
				{
					this.AddPortal(n1, (GridNode)node3, left, right);
					this.AddPortal((GridNode)node3, n2, left, right);
				}
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001C688 File Offset: 0x0001AA88
		public bool Linecast(Vector3 _a, Vector3 _b)
		{
			GraphHitInfo graphHitInfo;
			return this.Linecast(_a, _b, null, out graphHitInfo);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001C6A0 File Offset: 0x0001AAA0
		public bool Linecast(Vector3 _a, Vector3 _b, Node hint)
		{
			GraphHitInfo graphHitInfo;
			return this.Linecast(_a, _b, hint, out graphHitInfo);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001C6B8 File Offset: 0x0001AAB8
		public bool Linecast(Vector3 _a, Vector3 _b, Node hint, out GraphHitInfo hit)
		{
			hit = default(GraphHitInfo);
			_a = base.inverseMatrix.MultiplyPoint3x4(_a);
			_a.x -= 0.5f;
			_a.z -= 0.5f;
			_b = base.inverseMatrix.MultiplyPoint3x4(_b);
			_b.x -= 0.5f;
			_b.z -= 0.5f;
			if (_a.x < -0.5f || _a.z < -0.5f || _a.x >= (float)this.width - 0.5f || _a.z >= (float)this.depth - 0.5f || _b.x < -0.5f || _b.z < -0.5f || _b.x >= (float)this.width - 0.5f || _b.z >= (float)this.depth - 0.5f)
			{
				Vector3 vector = new Vector3(-0.5f, 0f, -0.5f);
				Vector3 vector2 = new Vector3(-0.5f, 0f, (float)this.depth - 0.5f);
				Vector3 vector3 = new Vector3((float)this.width - 0.5f, 0f, (float)this.depth - 0.5f);
				Vector3 vector4 = new Vector3((float)this.width - 0.5f, 0f, -0.5f);
				int num = 0;
				bool flag = false;
				Vector3 vector5 = Polygon.SegmentIntersectionPoint(vector, vector2, _a, _b, out flag);
				if (flag)
				{
					num++;
					if (!Polygon.Left(vector, vector2, _a))
					{
						_a = vector5;
					}
					else
					{
						_b = vector5;
					}
				}
				vector5 = Polygon.SegmentIntersectionPoint(vector2, vector3, _a, _b, out flag);
				if (flag)
				{
					num++;
					if (!Polygon.Left(vector2, vector3, _a))
					{
						_a = vector5;
					}
					else
					{
						_b = vector5;
					}
				}
				vector5 = Polygon.SegmentIntersectionPoint(vector3, vector4, _a, _b, out flag);
				if (flag)
				{
					num++;
					if (!Polygon.Left(vector3, vector4, _a))
					{
						_a = vector5;
					}
					else
					{
						_b = vector5;
					}
				}
				vector5 = Polygon.SegmentIntersectionPoint(vector4, vector, _a, _b, out flag);
				if (flag)
				{
					num++;
					if (!Polygon.Left(vector4, vector, _a))
					{
						_a = vector5;
					}
					else
					{
						_b = vector5;
					}
				}
				if (num == 0)
				{
					return false;
				}
			}
			Vector3 a = _b - _a;
			float magnitude = a.magnitude;
			if (magnitude == 0f)
			{
				return false;
			}
			float num2 = 0.2f;
			float num3 = this.nodeSize * num2;
			num3 -= this.nodeSize * 0.02f;
			a = a / magnitude * num3;
			int num4 = (int)(magnitude / num3);
			Vector3 a2 = _a + a * this.nodeSize * 0.01f;
			for (int i = 0; i <= num4; i++)
			{
				Vector3 vector6 = a2 + a * (float)i;
				int num5 = Mathf.RoundToInt(vector6.x);
				int num6 = Mathf.RoundToInt(vector6.z);
				num5 = ((num5 >= 0) ? ((num5 < this.width) ? num5 : (this.width - 1)) : 0);
				num6 = ((num6 >= 0) ? ((num6 < this.depth) ? num6 : (this.depth - 1)) : 0);
				Node node = this.nodes[num6 * this.width + num5];
				if (!node.walkable)
				{
					if (i > 0)
					{
						hit.point = this.matrix.MultiplyPoint3x4(a2 + a * (float)(i - 1) + new Vector3(0.5f, 0f, 0.5f));
					}
					else
					{
						hit.point = this.matrix.MultiplyPoint3x4(_a + new Vector3(0.5f, 0f, 0.5f));
					}
					hit.success = false;
					hit.origin = this.matrix.MultiplyPoint3x4(_a + new Vector3(0.5f, 0f, 0.5f));
					hit.node = node;
					return true;
				}
				if (i > num4 - 1 && (Mathf.Abs(vector6.x - _b.x) <= 0.50001f || Mathf.Abs(vector6.z - _b.z) <= 0.50001f))
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001CB80 File Offset: 0x0001AF80
		public bool SnappedLinecast(Vector3 _a, Vector3 _b, Node hint, out GraphHitInfo hit)
		{
			hit = default(GraphHitInfo);
			Node node = base.GetNearest(_a, NNConstraint.None).node;
			Node node2 = base.GetNearest(_b, NNConstraint.None).node;
			_a = base.inverseMatrix.MultiplyPoint3x4((Vector3)node.position);
			_a.x -= 0.5f;
			_a.z -= 0.5f;
			_b = base.inverseMatrix.MultiplyPoint3x4((Vector3)node2.position);
			_b.x -= 0.5f;
			_b.z -= 0.5f;
			Int3 @int = new Int3(Mathf.RoundToInt(_a.x), Mathf.RoundToInt(_a.y), Mathf.RoundToInt(_a.z));
			Int3 int2 = new Int3(Mathf.RoundToInt(_b.x), Mathf.RoundToInt(_b.y), Mathf.RoundToInt(_b.z));
			hit.origin = (Vector3)@int;
			if (!this.nodes[@int.z * this.width + @int.x].walkable)
			{
				hit.node = this.nodes[@int.z * this.width + @int.x];
				hit.point = this.matrix.MultiplyPoint3x4(new Vector3((float)@int.x + 0.5f, 0f, (float)@int.z + 0.5f));
				hit.point.y = ((Vector3)hit.node.position).y;
				return true;
			}
			int num = Mathf.Abs(@int.x - int2.x);
			int num2 = Mathf.Abs(@int.z - int2.z);
			int num3;
			if (@int.x < int2.x)
			{
				num3 = 1;
			}
			else
			{
				num3 = -1;
			}
			int num4;
			if (@int.z < int2.z)
			{
				num4 = 1;
			}
			else
			{
				num4 = -1;
			}
			int num5 = num - num2;
			while (@int.x != int2.x || @int.z != int2.z)
			{
				int num6 = num5 * 2;
				int num7 = 0;
				Int3 int3 = @int;
				if (num6 > -num2)
				{
					num5 -= num2;
					num7 = num3;
					int3.x += num3;
				}
				if (num6 < num)
				{
					num5 += num;
					num7 += this.width * num4;
					int3.z += num4;
				}
				if (num7 == 0)
				{
					Debug.LogError("Offset is zero, this should not happen");
					return false;
				}
				int i = 0;
				while (i < this.neighbourOffsets.Length)
				{
					if (this.neighbourOffsets[i] == num7)
					{
						if (!this.CheckConnection(this.nodes[@int.z * this.width + @int.x] as GridNode, i))
						{
							hit.node = this.nodes[@int.z * this.width + @int.x];
							hit.point = this.matrix.MultiplyPoint3x4(new Vector3((float)@int.x + 0.5f, 0f, (float)@int.z + 0.5f));
							hit.point.y = ((Vector3)hit.node.position).y;
							return true;
						}
						if (!this.nodes[int3.z * this.width + int3.x].walkable)
						{
							hit.node = this.nodes[@int.z * this.width + @int.x];
							hit.point = this.matrix.MultiplyPoint3x4(new Vector3((float)@int.x + 0.5f, 0f, (float)@int.z + 0.5f));
							hit.point.y = ((Vector3)hit.node.position).y;
							return true;
						}
						@int = int3;
						break;
					}
					else
					{
						i++;
					}
				}
			}
			return false;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001CFFC File Offset: 0x0001B3FC
		public bool CheckConnection(GridNode node, int dir)
		{
			if (this.neighbours == NumNeighbours.Eight)
			{
				return node.GetConnection(dir);
			}
			int num = dir - 4 - 1 & 3;
			int num2 = dir - 4 + 1 & 3;
			if (!node.GetConnection(num) || !node.GetConnection(num2))
			{
				return false;
			}
			GridNode gridNode = this.nodes[node.GetIndex() + this.neighbourOffsets[num]] as GridNode;
			GridNode gridNode2 = this.nodes[node.GetIndex() + this.neighbourOffsets[num2]] as GridNode;
			return gridNode.walkable && gridNode2.walkable && gridNode2.GetConnection(num) && gridNode.GetConnection(num2);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001D0B4 File Offset: 0x0001B4B4
		public override void PostDeserialization()
		{
			this.GenerateMatrix();
			this.SetUpOffsetsAndCosts();
			if (this.nodes == null || this.nodes.Length == 0)
			{
				return;
			}
			if (this.width * this.depth != this.nodes.Length)
			{
				Debug.LogWarning("Node data did not match with bounds data. Probably a change to the bounds/width/depth data was made after scanning the graph just prior to saving it. Nodes will be discarded");
				this.nodes = new GridNode[0];
				return;
			}
			int gridIndex = GridNode.SetGridGraph(this);
			for (int i = 0; i < this.depth; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					GridNode gridNode = this.nodes[i * this.width + j] as GridNode;
					if (gridNode == null)
					{
						Debug.LogError("Deserialization Error : Couldn't cast the node to the appropriate type - GridGenerator. Check the CreateNodes function");
						return;
					}
					gridNode.SetIndex(i * this.width + j);
					gridNode.SetGridIndex(gridIndex);
				}
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001D18C File Offset: 0x0001B58C
		[Obsolete]
		public void SerializeNodes(Node[] nodes, AstarSerializer serializer)
		{
			this.GenerateMatrix();
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < nodes.Length; i++)
			{
				int y = nodes[i].position.y;
				num = ((y <= num) ? num : y);
				num2 = ((y >= num2) ? num2 : y);
			}
			int num3 = (num <= -num2) ? (-num2) : num;
			if (num3 <= 32767)
			{
				serializer.writerStream.Write(0);
				for (int j = 0; j < nodes.Length; j++)
				{
					serializer.writerStream.Write((short)base.inverseMatrix.MultiplyPoint3x4((Vector3)nodes[j].position).y);
				}
			}
			else
			{
				serializer.writerStream.Write(1);
				for (int k = 0; k < nodes.Length; k++)
				{
					serializer.writerStream.Write(base.inverseMatrix.MultiplyPoint3x4((Vector3)nodes[k].position).y);
				}
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001D2B4 File Offset: 0x0001B6B4
		[Obsolete]
		public void DeSerializeNodes(Node[] nodes, AstarSerializer serializer)
		{
			this.GenerateMatrix();
			this.SetUpOffsetsAndCosts();
			if (nodes == null || nodes.Length == 0)
			{
				return;
			}
			nodes = new GridNode[nodes.Length];
			int gridIndex = GridNode.SetGridGraph(this);
			int num = (int)serializer.readerStream.ReadByte();
			for (int i = 0; i < this.depth; i++)
			{
				for (int j = 0; j < this.width; j++)
				{
					GridNode gridNode = nodes[i * this.width + j] as GridNode;
					nodes[i * this.width + j] = gridNode;
					if (gridNode == null)
					{
						Debug.LogError("DeSerialization Error : Couldn't cast the node to the appropriate type - GridGenerator");
						return;
					}
					gridNode.SetIndex(i * this.width + j);
					gridNode.SetGridIndex(gridIndex);
					float y;
					if (num == 0)
					{
						y = (float)serializer.readerStream.ReadInt16();
					}
					else
					{
						y = (float)serializer.readerStream.ReadInt32();
					}
					gridNode.position = (Int3)this.matrix.MultiplyPoint3x4(new Vector3((float)j + 0.5f, y, (float)i + 0.5f));
				}
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001D3D0 File Offset: 0x0001B7D0
		public void SerializeSettings(AstarSerializer serializer)
		{
			serializer.mask -= AstarSerializer.SMask.SaveNodePositions;
			serializer.AddValue("unclampedSize", this.unclampedSize);
			serializer.AddValue("cutCorners", this.cutCorners);
			serializer.AddValue("neighbours", (int)this.neighbours);
			serializer.AddValue("center", this.center);
			serializer.AddValue("rotation", this.rotation);
			serializer.AddValue("nodeSize", this.nodeSize);
			serializer.AddValue("collision", (this.collision != null) ? this.collision : new GraphCollision());
			serializer.AddValue("maxClimb", this.maxClimb);
			serializer.AddValue("maxClimbAxis", this.maxClimbAxis);
			serializer.AddValue("maxSlope", this.maxSlope);
			serializer.AddValue("erodeIterations", this.erodeIterations);
			serializer.AddValue("penaltyAngle", this.penaltyAngle);
			serializer.AddValue("penaltyAngleFactor", this.penaltyAngleFactor);
			serializer.AddValue("penaltyPosition", this.penaltyPosition);
			serializer.AddValue("penaltyPositionOffset", this.penaltyPositionOffset);
			serializer.AddValue("penaltyPositionFactor", this.penaltyPositionFactor);
			serializer.AddValue("aspectRatio", this.aspectRatio);
			serializer.AddValue("textureData", this.textureData);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001D58C File Offset: 0x0001B98C
		public void DeSerializeSettings(AstarSerializer serializer)
		{
			this.unclampedSize = (Vector2)serializer.GetValue("unclampedSize", typeof(Vector2), null);
			this.cutCorners = (bool)serializer.GetValue("cutCorners", typeof(bool), null);
			this.neighbours = (NumNeighbours)serializer.GetValue("neighbours", typeof(int), null);
			this.rotation = (Vector3)serializer.GetValue("rotation", typeof(Vector3), null);
			this.nodeSize = (float)serializer.GetValue("nodeSize", typeof(float), null);
			this.collision = (GraphCollision)serializer.GetValue("collision", typeof(GraphCollision), null);
			this.center = (Vector3)serializer.GetValue("center", typeof(Vector3), null);
			this.maxClimb = (float)serializer.GetValue("maxClimb", typeof(float), null);
			this.maxClimbAxis = (int)serializer.GetValue("maxClimbAxis", typeof(int), 1);
			this.maxSlope = (float)serializer.GetValue("maxSlope", typeof(float), 90f);
			this.erodeIterations = (int)serializer.GetValue("erodeIterations", typeof(int), null);
			this.penaltyAngle = (bool)serializer.GetValue("penaltyAngle", typeof(bool), null);
			this.penaltyAngleFactor = (float)serializer.GetValue("penaltyAngleFactor", typeof(float), null);
			this.penaltyPosition = (bool)serializer.GetValue("penaltyPosition", typeof(bool), null);
			this.penaltyPositionOffset = (float)serializer.GetValue("penaltyPositionOffset", typeof(float), null);
			this.penaltyPositionFactor = (float)serializer.GetValue("penaltyPositionFactor", typeof(float), null);
			this.aspectRatio = (float)serializer.GetValue("aspectRatio", typeof(float), 1f);
			this.textureData = (serializer.GetValue("textureData", typeof(GridGraph.TextureData), null) as GridGraph.TextureData);
			if (this.textureData == null)
			{
				this.textureData = new GridGraph.TextureData();
			}
			this.GenerateMatrix();
			this.SetUpOffsetsAndCosts();
		}

		// Token: 0x040002CA RID: 714
		public int width;

		// Token: 0x040002CB RID: 715
		public int depth;

		// Token: 0x040002CC RID: 716
		[JsonMember]
		public float aspectRatio = 1f;

		// Token: 0x040002CD RID: 717
		[JsonMember]
		public Vector3 rotation;

		// Token: 0x040002CE RID: 718
		public Bounds bounds;

		// Token: 0x040002CF RID: 719
		[JsonMember]
		public Vector3 center;

		// Token: 0x040002D0 RID: 720
		[JsonMember]
		public Vector2 unclampedSize;

		// Token: 0x040002D1 RID: 721
		[JsonMember]
		public float nodeSize = 1f;

		// Token: 0x040002D2 RID: 722
		[JsonMember]
		public GraphCollision collision;

		// Token: 0x040002D3 RID: 723
		[JsonMember]
		public float maxClimb = 0.4f;

		// Token: 0x040002D4 RID: 724
		[JsonMember]
		public int maxClimbAxis = 1;

		// Token: 0x040002D5 RID: 725
		[JsonMember]
		public float maxSlope = 90f;

		// Token: 0x040002D6 RID: 726
		[JsonMember]
		public int erodeIterations;

		// Token: 0x040002D7 RID: 727
		[JsonMember]
		public bool erosionUseTags;

		// Token: 0x040002D8 RID: 728
		[JsonMember]
		public int erosionFirstTag = 1;

		// Token: 0x040002D9 RID: 729
		[JsonMember]
		public bool autoLinkGrids;

		// Token: 0x040002DA RID: 730
		[JsonMember]
		public float autoLinkDistLimit = 10f;

		// Token: 0x040002DB RID: 731
		[JsonMember]
		public NumNeighbours neighbours = NumNeighbours.Eight;

		// Token: 0x040002DC RID: 732
		[JsonMember]
		public bool cutCorners = true;

		// Token: 0x040002DD RID: 733
		[JsonMember]
		public float penaltyPositionOffset;

		// Token: 0x040002DE RID: 734
		[JsonMember]
		public bool penaltyPosition;

		// Token: 0x040002DF RID: 735
		[JsonMember]
		public float penaltyPositionFactor = 1f;

		// Token: 0x040002E0 RID: 736
		[JsonMember]
		public bool penaltyAngle;

		// Token: 0x040002E1 RID: 737
		[JsonMember]
		public float penaltyAngleFactor = 100f;

		// Token: 0x040002E2 RID: 738
		[JsonMember]
		public GridGraph.TextureData textureData = new GridGraph.TextureData();

		// Token: 0x040002E3 RID: 739
		public Vector2 size;

		// Token: 0x040002E4 RID: 740
		[NonSerialized]
		public int[] neighbourOffsets;

		// Token: 0x040002E5 RID: 741
		[NonSerialized]
		public int[] neighbourCosts;

		// Token: 0x040002E6 RID: 742
		[NonSerialized]
		public int[] neighbourXOffsets;

		// Token: 0x040002E7 RID: 743
		[NonSerialized]
		public int[] neighbourZOffsets;

		// Token: 0x040002E8 RID: 744
		public int getNearestForceOverlap = 2;

		// Token: 0x040002E9 RID: 745
		public Matrix4x4 boundsMatrix;

		// Token: 0x040002EA RID: 746
		public Matrix4x4 boundsMatrix2;

		// Token: 0x040002EB RID: 747
		public int scans;

		// Token: 0x040002EC RID: 748
		[NonSerialized]
		protected int[] corners;

		// Token: 0x0200006A RID: 106
		public class TextureData : ISerializableObject
		{
			// Token: 0x06000380 RID: 896 RVA: 0x0001D844 File Offset: 0x0001BC44
			public void Initialize()
			{
				if (this.enabled && this.source != null)
				{
					for (int i = 0; i < this.channels.Length; i++)
					{
						if (this.channels[i] != GridGraph.TextureData.ChannelUse.None)
						{
							try
							{
								this.data = this.source.GetPixels32();
							}
							catch (UnityException ex)
							{
								Debug.LogWarning(ex.ToString());
								this.data = null;
							}
							break;
						}
					}
				}
			}

			// Token: 0x06000381 RID: 897 RVA: 0x0001D8D8 File Offset: 0x0001BCD8
			public void Apply(Node node, int x, int z)
			{
				if (this.enabled && this.data != null && x < this.source.width && z < this.source.height)
				{
					Color32 color = this.data[z * this.source.width + x];
					if (this.channels[0] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.r, this.channels[0], this.factors[0]);
					}
					if (this.channels[1] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.g, this.channels[1], this.factors[1]);
					}
					if (this.channels[2] != GridGraph.TextureData.ChannelUse.None)
					{
						this.ApplyChannel(node, x, z, (int)color.b, this.channels[2], this.factors[2]);
					}
				}
			}

			// Token: 0x06000382 RID: 898 RVA: 0x0001D9C4 File Offset: 0x0001BDC4
			private void ApplyChannel(Node node, int x, int z, int value, GridGraph.TextureData.ChannelUse channelUse, float factor)
			{
				if (channelUse != GridGraph.TextureData.ChannelUse.Penalty)
				{
					if (channelUse != GridGraph.TextureData.ChannelUse.Position)
					{
						if (channelUse == GridGraph.TextureData.ChannelUse.WalkablePenalty)
						{
							if (value == 0)
							{
								node.walkable = false;
							}
							else
							{
								node.penalty += (uint)Mathf.RoundToInt((float)(value - 1) * factor);
							}
						}
					}
					else
					{
						node.position.y = Mathf.RoundToInt((float)value * factor * 1000f);
					}
				}
				else
				{
					node.penalty += (uint)Mathf.RoundToInt((float)value * factor);
				}
			}

			// Token: 0x06000383 RID: 899 RVA: 0x0001DA60 File Offset: 0x0001BE60
			public void SerializeSettings(AstarSerializer serializer)
			{
				serializer.AddUnityReferenceValue("source", this.source);
				serializer.AddValue("enabled", this.enabled);
				for (int i = 0; i < this.factors.Length; i++)
				{
					serializer.AddValue("factor" + i, this.factors[i]);
				}
				for (int j = 0; j < this.channels.Length; j++)
				{
					serializer.AddValue("channel" + j, (int)this.channels[j]);
				}
			}

			// Token: 0x06000384 RID: 900 RVA: 0x0001DB0C File Offset: 0x0001BF0C
			public void DeSerializeSettings(AstarSerializer serializer)
			{
				this.enabled = (bool)serializer.GetValue("enabled", typeof(bool), null);
				this.source = (Texture2D)serializer.GetUnityReferenceValue("source", typeof(Texture2D), null);
				this.factors = new float[3];
				for (int i = 0; i < this.factors.Length; i++)
				{
					this.factors[i] = (float)serializer.GetValue("factor" + i, typeof(float), 1f);
				}
				this.channels = new GridGraph.TextureData.ChannelUse[3];
				for (int j = 0; j < this.channels.Length; j++)
				{
					this.channels[j] = (GridGraph.TextureData.ChannelUse)serializer.GetValue("channel" + j, typeof(int), null);
				}
			}

			// Token: 0x040002ED RID: 749
			public bool enabled;

			// Token: 0x040002EE RID: 750
			public Texture2D source;

			// Token: 0x040002EF RID: 751
			public float[] factors = new float[3];

			// Token: 0x040002F0 RID: 752
			public GridGraph.TextureData.ChannelUse[] channels = new GridGraph.TextureData.ChannelUse[3];

			// Token: 0x040002F1 RID: 753
			private Color32[] data;

			// Token: 0x0200006B RID: 107
			public enum ChannelUse
			{
				// Token: 0x040002F3 RID: 755
				None,
				// Token: 0x040002F4 RID: 756
				Penalty,
				// Token: 0x040002F5 RID: 757
				Position,
				// Token: 0x040002F6 RID: 758
				WalkablePenalty
			}
		}
	}
}
