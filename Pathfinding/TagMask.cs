using System;

namespace Pathfinding
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public class TagMask
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00003CB9 File Offset: 0x000020B9
		public TagMask()
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003CC1 File Offset: 0x000020C1
		public TagMask(int change, int set)
		{
			this.tagsChange = change;
			this.tagsSet = set;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003CD8 File Offset: 0x000020D8
		public void SetValues(object boxedTagMask)
		{
			TagMask tagMask = (TagMask)boxedTagMask;
			this.tagsChange = tagMask.tagsChange;
			this.tagsSet = tagMask.tagsSet;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003D04 File Offset: 0x00002104
		public override string ToString()
		{
			return string.Empty + Convert.ToString(this.tagsChange, 2) + "\n" + Convert.ToString(this.tagsSet, 2);
		}

		// Token: 0x0400006B RID: 107
		public int tagsChange;

		// Token: 0x0400006C RID: 108
		public int tagsSet;
	}
}
