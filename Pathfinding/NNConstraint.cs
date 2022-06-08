using System;

namespace Pathfinding
{
	// Token: 0x02000009 RID: 9
	public class NNConstraint
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00003AAB File Offset: 0x00001EAB
		public virtual bool SuitableGraph(int graphIndex, NavGraph graph)
		{
			return (this.graphMask >> graphIndex & 1) != 0;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003AC0 File Offset: 0x00001EC0
		public virtual bool Suitable(Node node)
		{
			return (!this.constrainWalkability || node.walkable == this.walkable) && (!this.constrainArea || this.area < 0 || node.area == this.area) && (!this.constrainTags || (this.tags >> node.tags & 1) != 0);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003B3A File Offset: 0x00001F3A
		public static NNConstraint Default
		{
			get
			{
				return new NNConstraint();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00003B44 File Offset: 0x00001F44
		public static NNConstraint None
		{
			get
			{
				return new NNConstraint
				{
					constrainWalkability = false,
					constrainArea = false,
					constrainTags = false,
					constrainDistance = false,
					graphMask = -1
				};
			}
		}

		// Token: 0x0400005D RID: 93
		public int graphMask = -1;

		// Token: 0x0400005E RID: 94
		public bool constrainArea;

		// Token: 0x0400005F RID: 95
		public int area = -1;

		// Token: 0x04000060 RID: 96
		public bool constrainWalkability = true;

		// Token: 0x04000061 RID: 97
		public bool walkable = true;

		// Token: 0x04000062 RID: 98
		public bool constrainTags = true;

		// Token: 0x04000063 RID: 99
		public int tags = -1;

		// Token: 0x04000064 RID: 100
		public bool constrainDistance = true;
	}
}
