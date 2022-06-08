using System;

namespace Pathfinding
{
	// Token: 0x020000B3 RID: 179
	public class ABPathEndingCondition : PathEndingCondition
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x00034B96 File Offset: 0x00032F96
		public ABPathEndingCondition(ABPath p)
		{
			if (p == null)
			{
				throw new ArgumentNullException("Please supply a non-null path");
			}
			this.abPath = p;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00034BB6 File Offset: 0x00032FB6
		public override bool TargetFound(NodeRun node)
		{
			return node.node == this.abPath.endNode;
		}

		// Token: 0x04000484 RID: 1156
		protected ABPath abPath;
	}
}
