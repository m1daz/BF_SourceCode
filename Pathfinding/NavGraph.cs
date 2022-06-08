using System;
using Pathfinding.Serialization.JsonFx;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000065 RID: 101
	public abstract class NavGraph
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00019018 File Offset: 0x00017418
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0001905D File Offset: 0x0001745D
		[JsonMember]
		public Pathfinding.Util.Guid guid
		{
			get
			{
				if (this._sguid == null || this._sguid.Length != 16)
				{
					this._sguid = Pathfinding.Util.Guid.NewGuid().ToByteArray();
				}
				return new Pathfinding.Util.Guid(this._sguid);
			}
			set
			{
				this._sguid = value.ToByteArray();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0001906C File Offset: 0x0001746C
		public Matrix4x4 inverseMatrix
		{
			get
			{
				return this.matrix.inverse;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001907C File Offset: 0x0001747C
		public virtual Node[] CreateNodes(int number)
		{
			Node[] array = new Node[number];
			for (int i = 0; i < number; i++)
			{
				array[i] = new Node();
				array[i].penalty = this.initialPenalty;
			}
			return array;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000190BC File Offset: 0x000174BC
		public virtual void RelocateNodes(Matrix4x4 oldMatrix, Matrix4x4 newMatrix)
		{
			if (this.nodes == null || this.nodes.Length == 0)
			{
				return;
			}
			Matrix4x4 inverse = oldMatrix.inverse;
			Matrix4x4 matrix4x = inverse * newMatrix;
			for (int i = 0; i < this.nodes.Length; i++)
			{
				this.nodes[i].position = (Int3)matrix4x.MultiplyPoint((Vector3)this.nodes[i].position);
			}
			this.matrix = newMatrix;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001913D File Offset: 0x0001753D
		public NNInfo GetNearest(Vector3 position)
		{
			return this.GetNearest(position, NNConstraint.None);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0001914B File Offset: 0x0001754B
		public NNInfo GetNearest(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint, null);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00019158 File Offset: 0x00017558
		public virtual NNInfo GetNearest(Vector3 position, NNConstraint constraint, Node hint)
		{
			if (this.nodes == null)
			{
				return default(NNInfo);
			}
			float num = (!constraint.constrainDistance) ? float.PositiveInfinity : AstarPath.active.maxNearestNodeDistanceSqr;
			float num2 = float.PositiveInfinity;
			Node node = null;
			float num3 = float.PositiveInfinity;
			Node node2 = null;
			for (int i = 0; i < this.nodes.Length; i++)
			{
				Node node3 = this.nodes[i];
				float sqrMagnitude = (position - (Vector3)node3.position).sqrMagnitude;
				if (sqrMagnitude < num2)
				{
					num2 = sqrMagnitude;
					node = node3;
				}
				if (sqrMagnitude < num3 && sqrMagnitude < num && constraint.Suitable(node3))
				{
					num3 = sqrMagnitude;
					node2 = node3;
				}
			}
			NNInfo result = new NNInfo(node);
			result.constrainedNode = node2;
			if (node2 != null)
			{
				result.constClampedPosition = (Vector3)node2.position;
			}
			else if (node != null)
			{
				result.constrainedNode = node;
				result.constClampedPosition = (Vector3)node.position;
			}
			return result;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00019277 File Offset: 0x00017677
		public virtual NNInfo GetNearestForce(Vector3 position, NNConstraint constraint)
		{
			return this.GetNearest(position, constraint);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00019281 File Offset: 0x00017681
		public virtual void Awake()
		{
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00019283 File Offset: 0x00017683
		public void SafeOnDestroy()
		{
			AstarPath.RegisterSafeUpdate(new OnVoidDelegate(this.OnDestroy), false);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00019298 File Offset: 0x00017698
		public virtual void OnDestroy()
		{
			this.nodes = null;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000192A4 File Offset: 0x000176A4
		public void ScanGraph()
		{
			if (AstarPath.OnPreScan != null)
			{
				AstarPath.OnPreScan(AstarPath.active);
			}
			if (AstarPath.OnGraphPreScan != null)
			{
				AstarPath.OnGraphPreScan(this);
			}
			this.Scan();
			if (AstarPath.OnGraphPostScan != null)
			{
				AstarPath.OnGraphPostScan(this);
			}
			if (AstarPath.OnPostScan != null)
			{
				AstarPath.OnPostScan(AstarPath.active);
			}
		}

		// Token: 0x06000349 RID: 841
		public abstract void Scan();

		// Token: 0x0600034A RID: 842 RVA: 0x00019314 File Offset: 0x00017714
		public virtual Color NodeColor(Node node, NodeRunData data)
		{
			Color result = AstarColor.NodeConnection;
			bool flag = false;
			if (node == null)
			{
				return AstarColor.NodeConnection;
			}
			GraphDebugMode debugMode = AstarPath.active.debugMode;
			if (debugMode != GraphDebugMode.Areas)
			{
				if (debugMode != GraphDebugMode.Penalty)
				{
					if (debugMode == GraphDebugMode.Tags)
					{
						result = Mathfx.IntToColor(node.tags, 0.5f);
						flag = true;
					}
				}
				else
				{
					result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, node.penalty / AstarPath.active.debugRoof);
					flag = true;
				}
			}
			else
			{
				result = AstarColor.GetAreaColor(node.area);
				flag = true;
			}
			if (!flag)
			{
				if (data == null)
				{
					return AstarColor.NodeConnection;
				}
				NodeRun nodeRun = node.GetNodeRun(data);
				if (nodeRun == null)
				{
					return AstarColor.NodeConnection;
				}
				GraphDebugMode debugMode2 = AstarPath.active.debugMode;
				if (debugMode2 != GraphDebugMode.G)
				{
					if (debugMode2 != GraphDebugMode.H)
					{
						if (debugMode2 == GraphDebugMode.F)
						{
							result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, nodeRun.f / AstarPath.active.debugRoof);
						}
					}
					else
					{
						result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, nodeRun.h / AstarPath.active.debugRoof);
					}
				}
				else
				{
					result = Color.Lerp(AstarColor.ConnectionLowLerp, AstarColor.ConnectionHighLerp, nodeRun.g / AstarPath.active.debugRoof);
				}
			}
			result.a *= 0.5f;
			return result;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001948D File Offset: 0x0001788D
		public virtual byte[] SerializeExtraInfo()
		{
			return null;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00019490 File Offset: 0x00017890
		public virtual void DeserializeExtraInfo(byte[] bytes)
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00019492 File Offset: 0x00017892
		public virtual void PostDeserialization()
		{
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00019494 File Offset: 0x00017894
		public bool InSearchTree(Node node, Path path)
		{
			if (path == null || path.runData == null)
			{
				return true;
			}
			NodeRun nodeRun = node.GetNodeRun(path.runData);
			return nodeRun.pathID == path.pathID;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000194D0 File Offset: 0x000178D0
		public virtual void OnDrawGizmos(bool drawNodes)
		{
			if (this.nodes == null || !drawNodes)
			{
				if (!Application.isPlaying)
				{
				}
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				Node node = this.nodes[i];
				if (node.connections != null)
				{
					Gizmos.color = this.NodeColor(node, AstarPath.active.debugPathData);
					if (AstarPath.active.showSearchTree && !this.InSearchTree(node, AstarPath.active.debugPath))
					{
						return;
					}
					if (AstarPath.active.showSearchTree && AstarPath.active.debugPathData != null && node.GetNodeRun(AstarPath.active.debugPathData).parent != null)
					{
						Gizmos.DrawLine((Vector3)node.position, (Vector3)node.GetNodeRun(AstarPath.active.debugPathData).parent.node.position);
					}
					else
					{
						for (int j = 0; j < node.connections.Length; j++)
						{
							Gizmos.DrawLine((Vector3)node.position, (Vector3)node.connections[j].position);
						}
					}
				}
			}
		}

		// Token: 0x040002A7 RID: 679
		public byte[] _sguid;

		// Token: 0x040002A8 RID: 680
		public AstarPath active;

		// Token: 0x040002A9 RID: 681
		[JsonMember]
		public uint initialPenalty;

		// Token: 0x040002AA RID: 682
		[JsonMember]
		public bool open;

		// Token: 0x040002AB RID: 683
		[JsonMember]
		public string name;

		// Token: 0x040002AC RID: 684
		[JsonMember]
		public bool drawGizmos = true;

		// Token: 0x040002AD RID: 685
		[JsonMember]
		public bool infoScreenOpen;

		// Token: 0x040002AE RID: 686
		public Node[] nodes;

		// Token: 0x040002AF RID: 687
		public Matrix4x4 matrix;
	}
}
