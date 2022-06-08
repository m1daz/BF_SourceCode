using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200000F RID: 15
	public class GraphUpdateObject
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00003D2D File Offset: 0x0000212D
		public GraphUpdateObject()
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003D55 File Offset: 0x00002155
		public GraphUpdateObject(Bounds b)
		{
			this.bounds = b;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003D84 File Offset: 0x00002184
		public virtual void WillUpdateNode(Node node)
		{
			if (this.trackChangedNodes && node != null)
			{
				if (this.changedNodes == null)
				{
					this.changedNodes = new List<Node>();
					this.backupData = new List<ulong>();
					this.backupPositionData = new List<Int3>();
				}
				this.changedNodes.Add(node);
				this.backupPositionData.Add(node.position);
				this.backupData.Add((ulong)node.penalty << 32 | (ulong)((long)node.flags));
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003E08 File Offset: 0x00002208
		public virtual void RevertFromBackup()
		{
			if (!this.trackChangedNodes)
			{
				throw new InvalidOperationException("Changed nodes have not been tracked, cannot revert from backup");
			}
			if (this.changedNodes == null)
			{
				return;
			}
			for (int i = 0; i < this.changedNodes.Count; i++)
			{
				this.changedNodes[i].penalty = (uint)(this.backupData[i] >> 32);
				this.changedNodes[i].flags = (int)(this.backupData[i] & (ulong)-1);
				this.changedNodes[i].position = this.backupPositionData[i];
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003EB8 File Offset: 0x000022B8
		public virtual void Apply(Node node)
		{
			if (this.shape == null || this.shape.Contains(node))
			{
				node.penalty = (uint)((ulong)node.penalty + (ulong)((long)this.addPenalty));
				if (this.modifyWalkability)
				{
					node.walkable = this.setWalkability;
				}
				if (this.modifyTag)
				{
					node.tags = this.setTag;
				}
			}
		}

		// Token: 0x0400006D RID: 109
		public Bounds bounds;

		// Token: 0x0400006E RID: 110
		public bool requiresFloodFill = true;

		// Token: 0x0400006F RID: 111
		public bool updatePhysics = true;

		// Token: 0x04000070 RID: 112
		public bool updateErosion = true;

		// Token: 0x04000071 RID: 113
		public NNConstraint nnConstraint = NNConstraint.None;

		// Token: 0x04000072 RID: 114
		public int addPenalty;

		// Token: 0x04000073 RID: 115
		public bool modifyWalkability;

		// Token: 0x04000074 RID: 116
		public bool setWalkability;

		// Token: 0x04000075 RID: 117
		public bool modifyTag;

		// Token: 0x04000076 RID: 118
		public int setTag;

		// Token: 0x04000077 RID: 119
		public bool trackChangedNodes;

		// Token: 0x04000078 RID: 120
		private List<Node> changedNodes;

		// Token: 0x04000079 RID: 121
		private List<ulong> backupData;

		// Token: 0x0400007A RID: 122
		private List<Int3> backupPositionData;

		// Token: 0x0400007B RID: 123
		public GraphUpdateShape shape;
	}
}
