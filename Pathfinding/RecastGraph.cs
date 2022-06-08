using System;
using System.Collections.Generic;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000077 RID: 119
	[JsonOptIn]
	[Serializable]
	public class RecastGraph : NavGraph, INavmesh, ISerializableGraph, IRaycastableGraph, IFunnelGraph, IUpdatableGraph, ISerializableObject
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x00024760 File Offset: 0x00022B60
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0002479D File Offset: 0x00022B9D
		public Bounds forcedBounds
		{
			get
			{
				return new Bounds(this.forcedBoundsCenter, this.forcedBoundsSize);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x000247B0 File Offset: 0x00022BB0
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x000247B8 File Offset: 0x00022BB8
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x000247C1 File Offset: 0x00022BC1
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x000247C9 File Offset: 0x00022BC9
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000247D4 File Offset: 0x00022BD4
		public Vector3[] vectorVertices
		{
			get
			{
				if (this._vectorVertices != null && this._vectorVertices.Length == this.vertices.Length)
				{
					return this._vectorVertices;
				}
				if (this.vertices == null)
				{
					return null;
				}
				this._vectorVertices = new Vector3[this.vertices.Length];
				for (int i = 0; i < this._vectorVertices.Length; i++)
				{
					this._vectorVertices[i] = (Vector3)this.vertices[i];
				}
				return this._vectorVertices;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00024870 File Offset: 0x00022C70
		public void SnapForceBoundsToScene()
		{
			List<MeshFilter> sceneMeshes = this.GetSceneMeshes();
			if (sceneMeshes.Count == 0)
			{
				return;
			}
			Bounds bounds = default(Bounds);
			for (int i = 0; i < sceneMeshes.Count; i++)
			{
				if (sceneMeshes[i].GetComponent<Renderer>() != null)
				{
					bounds = sceneMeshes[i].GetComponent<Renderer>().bounds;
					break;
				}
			}
			for (int j = 0; j < sceneMeshes.Count; j++)
			{
				if (sceneMeshes[j].GetComponent<Renderer>() != null)
				{
					bounds.Encapsulate(sceneMeshes[j].GetComponent<Renderer>().bounds);
				}
			}
			this.forcedBoundsCenter = bounds.center;
			this.forcedBoundsSize = bounds.size;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00024940 File Offset: 0x00022D40
		public List<MeshFilter> GetSceneMeshes()
		{
			MeshFilter[] array = UnityEngine.Object.FindObjectsOfType(typeof(MeshFilter)) as MeshFilter[];
			List<MeshFilter> list = new List<MeshFilter>(array.Length / 3);
			foreach (MeshFilter meshFilter in array)
			{
				if (meshFilter.GetComponent<Renderer>() != null && meshFilter.GetComponent<Renderer>().enabled && ((1 << meshFilter.gameObject.layer & this.mask) == 1 << meshFilter.gameObject.layer || this.tagMask.Contains(meshFilter.tag)))
				{
					list.Add(meshFilter);
				}
			}
			List<RecastGraph.SceneMesh> list2 = new List<RecastGraph.SceneMesh>();
			HashSet<Mesh> hashSet = new HashSet<Mesh>();
			foreach (MeshFilter meshFilter2 in list)
			{
				if (meshFilter2.GetComponent<Renderer>().isPartOfStaticBatch)
				{
					Mesh sharedMesh = meshFilter2.sharedMesh;
					if (!hashSet.Contains(sharedMesh))
					{
						hashSet.Add(sharedMesh);
						list2.Add(new RecastGraph.SceneMesh
						{
							mesh = sharedMesh,
							bounds = meshFilter2.GetComponent<Renderer>().bounds,
							matrix = Matrix4x4.identity
						});
					}
					else
					{
						RecastGraph.SceneMesh sceneMesh = default(RecastGraph.SceneMesh);
						for (int j = 0; j < list2.Count; j++)
						{
							if (list2[j].mesh == sharedMesh)
							{
								sceneMesh = list2[j];
								break;
							}
						}
						sceneMesh.bounds.Encapsulate(meshFilter2.GetComponent<Renderer>().bounds);
					}
				}
				else if (meshFilter2.GetComponent<Renderer>().bounds.Intersects(this.forcedBounds))
				{
					Mesh sharedMesh2 = meshFilter2.sharedMesh;
					list2.Add(new RecastGraph.SceneMesh
					{
						matrix = meshFilter2.GetComponent<Renderer>().localToWorldMatrix,
						mesh = sharedMesh2,
						bounds = meshFilter2.GetComponent<Renderer>().bounds
					});
				}
			}
			return list;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00024B9C File Offset: 0x00022F9C
		public void UpdateArea(GraphUpdateObject guo)
		{
			NavMeshGraph.UpdateArea(guo, this);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00024BA5 File Offset: 0x00022FA5
		public override NNInfo GetNearest(Vector3 position, NNConstraint constraint, Node hint)
		{
			return NavMeshGraph.GetNearest(this, this.nodes, position, constraint, this.accurateNearestNode);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00024BBB File Offset: 0x00022FBB
		public override NNInfo GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return NavMeshGraph.GetNearestForce(this.nodes, this.vertices, position, constraint, this.accurateNearestNode);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00024BD6 File Offset: 0x00022FD6
		public void BuildFunnelCorridor(List<Node> path, int startIndex, int endIndex, List<Vector3> left, List<Vector3> right)
		{
			NavMeshGraph.BuildFunnelCorridor(this, path, startIndex, endIndex, left, right);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00024BE5 File Offset: 0x00022FE5
		public void AddPortal(Node n1, Node n2, List<Vector3> left, List<Vector3> right)
		{
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00024BE7 File Offset: 0x00022FE7
		public static string GetRecastPath()
		{
			return Application.dataPath + "/Recast/recast";
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00024BF8 File Offset: 0x00022FF8
		public override void Scan()
		{
			if (this.useCRecast)
			{
				this.ScanCRecast();
			}
			else
			{
				MeshFilter[] filters;
				RecastGraph.ExtraMesh[] extraMeshes;
				if (!this.CollectMeshes(out filters, out extraMeshes))
				{
					this.nodes = new Node[0];
					return;
				}
				Voxelize voxelize = new Voxelize(this.cellHeight, this.cellSize, this.walkableClimb, this.walkableHeight, this.maxSlope);
				voxelize.maxEdgeLength = this.maxEdgeLength;
				voxelize.forcedBounds = this.forcedBounds;
				voxelize.includeOutOfBounds = this.includeOutOfBounds;
				voxelize.VoxelizeMesh(filters, extraMeshes);
				voxelize.ErodeWalkableArea(Mathf.CeilToInt(2f * this.characterRadius / this.cellSize));
				voxelize.BuildDistanceField();
				voxelize.BuildRegions();
				VoxelContourSet cset = new VoxelContourSet();
				voxelize.BuildContours(this.contourMaxError, 1, cset, Voxelize.RC_CONTOUR_TESS_WALL_EDGES);
				VoxelMesh voxelMesh;
				voxelize.BuildPolyMesh(cset, 3, out voxelMesh);
				Vector3[] array = new Vector3[voxelMesh.verts.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (Vector3)voxelMesh.verts[i];
				}
				this.matrix = Matrix4x4.TRS(voxelize.voxelOffset, Quaternion.identity, 1000f * Voxelize.CellScale);
				NavMeshGraph.GenerateNodes(this, array, voxelMesh.tris, out this._vectorVertices, out this._vertices);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00024D64 File Offset: 0x00023164
		public void ScanCRecast()
		{
			Debug.LogError("The C++ version of recast can only be used in editor or osx standalone mode, I'm sure it cannot be used in the webplayer, but other platforms are not tested yet\nIf you are in the Unity Editor, try switching Platform to OSX Standalone just when scanning, scanned graphs can be cached to enable them to be used in a webplayer");
			this._vectorVertices = new Vector3[0];
			this._vertices = new Int3[0];
			this.nodes = new Node[0];
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00024D94 File Offset: 0x00023194
		public void CollectTreeMeshes(List<RecastGraph.ExtraMesh> extraMeshes, Terrain terrain)
		{
			TerrainData terrainData = terrain.terrainData;
			for (int i = 0; i < terrainData.treeInstances.Length; i++)
			{
				TreeInstance treeInstance = terrainData.treeInstances[i];
				TreePrototype treePrototype = terrainData.treePrototypes[treeInstance.prototypeIndex];
				if (treePrototype.prefab.GetComponent<Collider>() == null)
				{
					Bounds b = new Bounds(terrain.transform.position + Vector3.Scale(treeInstance.position, terrainData.size), new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale));
					Matrix4x4 matrix = Matrix4x4.TRS(terrain.transform.position + Vector3.Scale(treeInstance.position, terrainData.size), Quaternion.identity, new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale) * 0.5f);
					RecastGraph.ExtraMesh item = new RecastGraph.ExtraMesh(this.BoxColliderVerts, this.BoxColliderTris, b, matrix);
					extraMeshes.Add(item);
				}
				else
				{
					Vector3 pos = terrain.transform.position + Vector3.Scale(treeInstance.position, terrainData.size);
					Vector3 s = new Vector3(treeInstance.widthScale, treeInstance.heightScale, treeInstance.widthScale);
					RecastGraph.ExtraMesh item2 = this.RasterizeCollider(treePrototype.prefab.GetComponent<Collider>(), Matrix4x4.TRS(pos, Quaternion.identity, s));
					if (item2.vertices != null)
					{
						item2.RecalculateBounds();
						extraMeshes.Add(item2);
					}
				}
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00024F30 File Offset: 0x00023330
		public bool CollectMeshes(out MeshFilter[] filters, out RecastGraph.ExtraMesh[] extraMeshes)
		{
			List<MeshFilter> list;
			if (this.rasterizeMeshes)
			{
				list = this.GetSceneMeshes();
			}
			else
			{
				list = new List<MeshFilter>();
			}
			List<RecastGraph.ExtraMesh> list2 = new List<RecastGraph.ExtraMesh>();
			Terrain[] array = UnityEngine.Object.FindObjectsOfType(typeof(Terrain)) as Terrain[];
			if (this.rasterizeTerrain && array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					TerrainData terrainData = array[i].terrainData;
					if (!(terrainData == null))
					{
						Vector3 position = array[i].GetPosition();
						Vector3 center = position + terrainData.size * 0.5f;
						Bounds b = new Bounds(center, terrainData.size);
						if (b.Intersects(this.forcedBounds))
						{
							float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapWidth, terrainData.heightmapHeight);
							this.terrainSampleSize = ((this.terrainSampleSize >= 1) ? this.terrainSampleSize : 1);
							int num = terrainData.heightmapWidth / this.terrainSampleSize;
							int num2 = terrainData.heightmapHeight / this.terrainSampleSize;
							Vector3[] array2 = new Vector3[num * num2];
							Vector3 heightmapScale = terrainData.heightmapScale;
							float y = terrainData.size.y;
							int num3 = 0;
							for (int j = 0; j < num2; j++)
							{
								int num4 = 0;
								for (int k = 0; k < num; k++)
								{
									array2[j * num + k] = new Vector3((float)num3 * heightmapScale.z, heights[num4, num3] * y, (float)num4 * heightmapScale.x) + position;
									num4 += this.terrainSampleSize;
								}
								num3 += this.terrainSampleSize;
							}
							int[] array3 = new int[(num - 1) * (num2 - 1) * 2 * 3];
							int num5 = 0;
							for (int l = 0; l < num2 - 1; l++)
							{
								for (int m = 0; m < num - 1; m++)
								{
									array3[num5] = l * num + m;
									array3[num5 + 1] = l * num + m + 1;
									array3[num5 + 2] = (l + 1) * num + m + 1;
									num5 += 3;
									array3[num5] = l * num + m;
									array3[num5 + 1] = (l + 1) * num + m + 1;
									array3[num5 + 2] = (l + 1) * num + m;
									num5 += 3;
								}
							}
							list2.Add(new RecastGraph.ExtraMesh(array2, array3, b));
							if (this.rasterizeTrees)
							{
								this.CollectTreeMeshes(list2, array[i]);
							}
						}
					}
				}
			}
			if (this.rasterizeColliders)
			{
				Collider[] array4 = UnityEngine.Object.FindObjectsOfType(typeof(Collider)) as Collider[];
				foreach (Collider collider in array4)
				{
					if ((1 << collider.gameObject.layer & this.mask) == 1 << collider.gameObject.layer && collider.enabled)
					{
						RecastGraph.ExtraMesh item = this.RasterizeCollider(collider);
						if (item.vertices != null)
						{
							list2.Add(item);
						}
					}
				}
				this.capsuleCache.Clear();
			}
			if (list.Count == 0 && list2.Count == 0)
			{
				Debug.LogWarning("No MeshFilters where found contained in the layers specified by the 'mask' variable");
				filters = null;
				extraMeshes = null;
				return false;
			}
			filters = list.ToArray();
			extraMeshes = list2.ToArray();
			return true;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000252D3 File Offset: 0x000236D3
		public RecastGraph.ExtraMesh RasterizeCollider(Collider col)
		{
			return this.RasterizeCollider(col, col.transform.localToWorldMatrix);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000252E8 File Offset: 0x000236E8
		public RecastGraph.ExtraMesh RasterizeCollider(Collider col, Matrix4x4 localToWorldMatrix)
		{
			if (col is BoxCollider)
			{
				BoxCollider boxCollider = col as BoxCollider;
				Matrix4x4 matrix4x = Matrix4x4.TRS(boxCollider.center, Quaternion.identity, boxCollider.size * 0.5f);
				matrix4x = localToWorldMatrix * matrix4x;
				Bounds bounds = boxCollider.bounds;
				RecastGraph.ExtraMesh result = new RecastGraph.ExtraMesh(this.BoxColliderVerts, this.BoxColliderTris, bounds, matrix4x);
				return result;
			}
			if (col is SphereCollider || col is CapsuleCollider)
			{
				SphereCollider sphereCollider = col as SphereCollider;
				CapsuleCollider capsuleCollider = col as CapsuleCollider;
				float num = (!(sphereCollider != null)) ? capsuleCollider.radius : sphereCollider.radius;
				float num2 = (!(sphereCollider != null)) ? (capsuleCollider.height * 0.5f / num - 1f) : 0f;
				Matrix4x4 matrix4x2 = Matrix4x4.TRS((!(sphereCollider != null)) ? capsuleCollider.center : sphereCollider.center, Quaternion.identity, Vector3.one * num);
				matrix4x2 = localToWorldMatrix * matrix4x2;
				int num3 = Mathf.Max(4, Mathf.RoundToInt(this.colliderRasterizeDetail * Mathf.Sqrt(matrix4x2.MultiplyVector(Vector3.one).magnitude)));
				if (num3 > 100)
				{
					Debug.LogWarning("Very large detail for some collider meshes. Consider decreasing Collider Rasterize Detail (RecastGraph)");
				}
				int num4 = num3;
				RecastGraph.CapsuleCache capsuleCache = null;
				for (int i = 0; i < this.capsuleCache.Count; i++)
				{
					RecastGraph.CapsuleCache capsuleCache2 = this.capsuleCache[i];
					if (capsuleCache2.rows == num3 && Mathf.Approximately(capsuleCache2.height, num2))
					{
						capsuleCache = capsuleCache2;
					}
				}
				Vector3[] array;
				if (capsuleCache == null)
				{
					array = new Vector3[num3 * num4 + 2];
					List<int> list = new List<int>();
					array[array.Length - 1] = Vector3.up;
					for (int j = 0; j < num3; j++)
					{
						for (int k = 0; k < num4; k++)
						{
							array[k + j * num4] = new Vector3(Mathf.Cos((float)k * 3.1415927f * 2f / (float)num4) * Mathf.Sin((float)j * 3.1415927f / (float)(num3 - 1)), Mathf.Cos((float)j * 3.1415927f / (float)(num3 - 1)) + ((j >= num3 / 2) ? (-num2) : num2), Mathf.Sin((float)k * 3.1415927f * 2f / (float)num4) * Mathf.Sin((float)j * 3.1415927f / (float)(num3 - 1)));
						}
					}
					array[array.Length - 2] = Vector3.down;
					int l = 0;
					int num5 = num4 - 1;
					while (l < num4)
					{
						list.Add(array.Length - 1);
						list.Add(0 * num4 + num5);
						list.Add(0 * num4 + l);
						num5 = l++;
					}
					for (int m = 1; m < num3; m++)
					{
						int n = 0;
						int num6 = num4 - 1;
						while (n < num4)
						{
							list.Add(m * num4 + n);
							list.Add(m * num4 + num6);
							list.Add((m - 1) * num4 + n);
							list.Add((m - 1) * num4 + num6);
							list.Add((m - 1) * num4 + n);
							list.Add(m * num4 + num6);
							num6 = n++;
						}
					}
					int num7 = 0;
					int num8 = num4 - 1;
					while (num7 < num4)
					{
						list.Add(array.Length - 2);
						list.Add((num3 - 1) * num4 + num8);
						list.Add((num3 - 1) * num4 + num7);
						num8 = num7++;
					}
					capsuleCache = new RecastGraph.CapsuleCache();
					capsuleCache.rows = num3;
					capsuleCache.height = num2;
					capsuleCache.verts = array;
					capsuleCache.tris = list.ToArray();
					this.capsuleCache.Add(capsuleCache);
				}
				array = capsuleCache.verts;
				int[] tris = capsuleCache.tris;
				Bounds bounds2 = col.bounds;
				RecastGraph.ExtraMesh result2 = new RecastGraph.ExtraMesh(array, tris, bounds2, matrix4x2);
				return result2;
			}
			if (col is MeshCollider)
			{
				MeshCollider meshCollider = col as MeshCollider;
				RecastGraph.ExtraMesh result3 = new RecastGraph.ExtraMesh(meshCollider.sharedMesh.vertices, meshCollider.sharedMesh.triangles, meshCollider.bounds, localToWorldMatrix);
				return result3;
			}
			return default(RecastGraph.ExtraMesh);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00025798 File Offset: 0x00023B98
		public bool Linecast(Vector3 origin, Vector3 end)
		{
			return this.Linecast(origin, end, base.GetNearest(origin, NNConstraint.None).node);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000257C1 File Offset: 0x00023BC1
		public bool Linecast(Vector3 origin, Vector3 end, Node hint, out GraphHitInfo hit)
		{
			return NavMeshGraph.Linecast(this, origin, end, hint, false, 0f, out hit);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000257D4 File Offset: 0x00023BD4
		public bool Linecast(Vector3 origin, Vector3 end, Node hint)
		{
			GraphHitInfo graphHitInfo;
			return NavMeshGraph.Linecast(this, origin, end, hint, false, 0f, out graphHitInfo);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000257F4 File Offset: 0x00023BF4
		public void Sort(Int3[] a)
		{
			bool flag = true;
			while (flag)
			{
				flag = false;
				for (int i = 0; i < a.Length - 1; i++)
				{
					if (a[i].x > a[i + 1].x || (a[i].x == a[i + 1].x && (a[i].y > a[i + 1].y || (a[i].y == a[i + 1].y && a[i].z > a[i + 1].z))))
					{
						Int3 @int = a[i];
						a[i] = a[i + 1];
						a[i + 1] = @int;
						flag = true;
					}
				}
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000258FC File Offset: 0x00023CFC
		public override void OnDrawGizmos(bool drawNodes)
		{
			if (!drawNodes)
			{
				return;
			}
			if (this.bbTree != null)
			{
				this.bbTree.OnDrawGizmos();
			}
			Gizmos.DrawWireCube(this.forcedBounds.center, this.forcedBounds.size);
			if (this.nodes == null)
			{
			}
			if (this.nodes == null)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				MeshNode meshNode = (MeshNode)this.nodes[i];
				if (AstarPath.active.debugPathData != null && AstarPath.active.showSearchTree && meshNode.GetNodeRun(AstarPath.active.debugPathData).parent != null)
				{
					Gizmos.color = this.NodeColor(meshNode, AstarPath.active.debugPathData);
					Gizmos.DrawLine((Vector3)meshNode.position, (Vector3)meshNode.GetNodeRun(AstarPath.active.debugPathData).parent.node.position);
				}
				if (this.showMeshOutline)
				{
					Gizmos.color = this.NodeColor(meshNode, AstarPath.active.debugPathData);
					Gizmos.DrawLine((Vector3)this.vertices[meshNode.v1], (Vector3)this.vertices[meshNode.v2]);
					Gizmos.DrawLine((Vector3)this.vertices[meshNode.v2], (Vector3)this.vertices[meshNode.v3]);
					Gizmos.DrawLine((Vector3)this.vertices[meshNode.v3], (Vector3)this.vertices[meshNode.v1]);
				}
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00025ADE File Offset: 0x00023EDE
		public override byte[] SerializeExtraInfo()
		{
			return NavMeshGraph.SerializeMeshNodes(this, this.nodes);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00025AEC File Offset: 0x00023EEC
		public override void DeserializeExtraInfo(byte[] bytes)
		{
			NavMeshGraph.DeserializeMeshNodes(this, this.nodes, bytes);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00025AFB File Offset: 0x00023EFB
		public void SerializeNodes(Node[] nodes, AstarSerializer serializer)
		{
			NavMeshGraph.SerializeMeshNodes(this, nodes, serializer);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00025B05 File Offset: 0x00023F05
		public void DeSerializeNodes(Node[] nodes, AstarSerializer serializer)
		{
			NavMeshGraph.DeSerializeMeshNodes(this, nodes, serializer);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00025B10 File Offset: 0x00023F10
		public void SerializeSettings(AstarSerializer serializer)
		{
			serializer.AddValue("contourMaxError", this.contourMaxError);
			serializer.AddValue("cellSize", this.cellSize);
			serializer.AddValue("cellHeight", this.cellHeight);
			serializer.AddValue("walkableHeight", this.walkableHeight);
			serializer.AddValue("walkableClimb", this.walkableClimb);
			serializer.AddValue("maxSlope", this.maxSlope);
			serializer.AddValue("maxEdgeLength", this.maxEdgeLength);
			serializer.AddValue("forcedBoundsCenter", this.forcedBoundsCenter);
			serializer.AddValue("forcedBoundsSize", this.forcedBoundsSize);
			serializer.AddValue("mask", this.mask.value);
			serializer.AddValue("showMeshOutline", this.showMeshOutline);
			serializer.AddValue("includeOutOfBounds", this.includeOutOfBounds);
			serializer.AddValue("regionMinSize", this.regionMinSize);
			serializer.AddValue("characterRadius", this.characterRadius);
			serializer.AddValue("useCRecast", this.useCRecast);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00025C6C File Offset: 0x0002406C
		public void DeSerializeSettings(AstarSerializer serializer)
		{
			this.contourMaxError = (float)serializer.GetValue("contourMaxError", typeof(float), null);
			this.cellSize = (float)serializer.GetValue("cellSize", typeof(float), null);
			this.cellHeight = (float)serializer.GetValue("cellHeight", typeof(float), null);
			this.walkableHeight = (float)serializer.GetValue("walkableHeight", typeof(float), null);
			this.walkableClimb = (float)serializer.GetValue("walkableClimb", typeof(float), null);
			this.maxSlope = (float)serializer.GetValue("maxSlope", typeof(float), null);
			this.maxEdgeLength = (float)serializer.GetValue("maxEdgeLength", typeof(float), null);
			this.forcedBoundsCenter = (Vector3)serializer.GetValue("forcedBoundsCenter", typeof(Vector3), null);
			this.forcedBoundsSize = (Vector3)serializer.GetValue("forcedBoundsSize", typeof(Vector3), null);
			this.mask.value = (int)serializer.GetValue("mask", typeof(int), null);
			this.showMeshOutline = (bool)serializer.GetValue("showMeshOutline", typeof(bool), null);
			this.includeOutOfBounds = (bool)serializer.GetValue("includeOutOfBounds", typeof(bool), null);
			this.regionMinSize = (int)serializer.GetValue("regionMinSize", typeof(int), null);
			this.characterRadius = (float)serializer.GetValue("characterRadius", typeof(float), null);
			this.useCRecast = (bool)serializer.GetValue("useCRecast", typeof(bool), null);
		}

		// Token: 0x04000329 RID: 809
		[JsonMember]
		public float characterRadius = 0.5f;

		// Token: 0x0400032A RID: 810
		[JsonMember]
		public float contourMaxError = 2f;

		// Token: 0x0400032B RID: 811
		[JsonMember]
		public float cellSize = 0.5f;

		// Token: 0x0400032C RID: 812
		[JsonMember]
		public float cellHeight = 0.4f;

		// Token: 0x0400032D RID: 813
		[JsonMember]
		public float walkableHeight = 2f;

		// Token: 0x0400032E RID: 814
		[JsonMember]
		public float walkableClimb = 0.5f;

		// Token: 0x0400032F RID: 815
		[JsonMember]
		public float maxSlope = 30f;

		// Token: 0x04000330 RID: 816
		[JsonMember]
		public float maxEdgeLength = 20f;

		// Token: 0x04000331 RID: 817
		[JsonMember]
		public int regionMinSize = 8;

		// Token: 0x04000332 RID: 818
		[JsonMember]
		public bool useCRecast;

		// Token: 0x04000333 RID: 819
		[JsonMember]
		public bool rasterizeColliders;

		// Token: 0x04000334 RID: 820
		[JsonMember]
		public bool rasterizeMeshes = true;

		// Token: 0x04000335 RID: 821
		[JsonMember]
		public bool rasterizeTerrain = true;

		// Token: 0x04000336 RID: 822
		public bool rasterizeTrees = true;

		// Token: 0x04000337 RID: 823
		[JsonMember]
		public float colliderRasterizeDetail = 10f;

		// Token: 0x04000338 RID: 824
		[JsonMember]
		public bool includeOutOfBounds;

		// Token: 0x04000339 RID: 825
		[JsonMember]
		public Vector3 forcedBoundsCenter;

		// Token: 0x0400033A RID: 826
		[JsonMember]
		public Vector3 forcedBoundsSize = new Vector3(100f, 40f, 100f);

		// Token: 0x0400033B RID: 827
		[JsonMember]
		public LayerMask mask = -1;

		// Token: 0x0400033C RID: 828
		[JsonMember]
		public List<string> tagMask = new List<string>();

		// Token: 0x0400033D RID: 829
		[JsonMember]
		public bool showMeshOutline;

		// Token: 0x0400033E RID: 830
		[JsonMember]
		public int terrainSampleSize = 3;

		// Token: 0x0400033F RID: 831
		[JsonMember]
		public bool accurateNearestNode = true;

		// Token: 0x04000340 RID: 832
		private BBTree _bbTree;

		// Token: 0x04000341 RID: 833
		private Int3[] _vertices;

		// Token: 0x04000342 RID: 834
		private Vector3[] _vectorVertices;

		// Token: 0x04000343 RID: 835
		private readonly int[] BoxColliderTris = new int[]
		{
			0,
			1,
			2,
			0,
			2,
			3,
			6,
			5,
			4,
			7,
			6,
			4,
			0,
			5,
			1,
			0,
			4,
			5,
			1,
			6,
			2,
			1,
			5,
			6,
			2,
			7,
			3,
			2,
			6,
			7,
			3,
			4,
			0,
			3,
			7,
			4
		};

		// Token: 0x04000344 RID: 836
		private readonly Vector3[] BoxColliderVerts = new Vector3[]
		{
			new Vector3(-1f, -1f, -1f),
			new Vector3(1f, -1f, -1f),
			new Vector3(1f, -1f, 1f),
			new Vector3(-1f, -1f, 1f),
			new Vector3(-1f, 1f, -1f),
			new Vector3(1f, 1f, -1f),
			new Vector3(1f, 1f, 1f),
			new Vector3(-1f, 1f, 1f)
		};

		// Token: 0x04000345 RID: 837
		private List<RecastGraph.CapsuleCache> capsuleCache = new List<RecastGraph.CapsuleCache>();

		// Token: 0x02000078 RID: 120
		public struct SceneMesh
		{
			// Token: 0x04000346 RID: 838
			public Mesh mesh;

			// Token: 0x04000347 RID: 839
			public Matrix4x4 matrix;

			// Token: 0x04000348 RID: 840
			public Bounds bounds;
		}

		// Token: 0x02000079 RID: 121
		public struct ExtraMesh
		{
			// Token: 0x06000423 RID: 1059 RVA: 0x00025E6D File Offset: 0x0002426D
			public ExtraMesh(Vector3[] v, int[] t, Bounds b)
			{
				this.matrix = Matrix4x4.identity;
				this.vertices = v;
				this.triangles = t;
				this.bounds = b;
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x00025E8F File Offset: 0x0002428F
			public ExtraMesh(Vector3[] v, int[] t, Bounds b, Matrix4x4 matrix)
			{
				this.matrix = matrix;
				this.vertices = v;
				this.triangles = t;
				this.bounds = b;
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x00025EB0 File Offset: 0x000242B0
			public void RecalculateBounds()
			{
				Bounds bounds = new Bounds(this.matrix.MultiplyPoint3x4(this.vertices[0]), Vector3.zero);
				for (int i = 1; i < this.vertices.Length; i++)
				{
					bounds.Encapsulate(this.matrix.MultiplyPoint3x4(this.vertices[i]));
				}
				this.bounds = bounds;
			}

			// Token: 0x04000349 RID: 841
			public Vector3[] vertices;

			// Token: 0x0400034A RID: 842
			public int[] triangles;

			// Token: 0x0400034B RID: 843
			public Bounds bounds;

			// Token: 0x0400034C RID: 844
			public Matrix4x4 matrix;
		}

		// Token: 0x0200007A RID: 122
		private class CapsuleCache
		{
			// Token: 0x0400034D RID: 845
			public int rows;

			// Token: 0x0400034E RID: 846
			public float height;

			// Token: 0x0400034F RID: 847
			public Vector3[] verts;

			// Token: 0x04000350 RID: 848
			public int[] tris;
		}

		// Token: 0x0200007B RID: 123
		public struct Int2
		{
			// Token: 0x06000427 RID: 1063 RVA: 0x00025F30 File Offset: 0x00024330
			public Int2(int _x, int _z)
			{
				this.x = _x;
				this.z = _z;
			}

			// Token: 0x04000351 RID: 849
			public int x;

			// Token: 0x04000352 RID: 850
			public int z;
		}
	}
}
