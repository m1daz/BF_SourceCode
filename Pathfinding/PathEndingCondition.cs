using System;

namespace Pathfinding
{
	// Token: 0x020000B2 RID: 178
	public class PathEndingCondition
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x00032A28 File Offset: 0x00030E28
		protected PathEndingCondition()
		{
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00032A30 File Offset: 0x00030E30
		public PathEndingCondition(Path p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("Please supply a non-null path");
			}
			this.p = p;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00032A50 File Offset: 0x00030E50
		public virtual bool TargetFound(NodeRun node)
		{
			return true;
		}

		// Token: 0x04000483 RID: 1155
		protected Path p;
	}
}
