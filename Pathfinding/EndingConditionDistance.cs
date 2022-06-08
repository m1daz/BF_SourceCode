using System;

namespace Pathfinding
{
	// Token: 0x020000A8 RID: 168
	public class EndingConditionDistance : PathEndingCondition
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x00032A53 File Offset: 0x00030E53
		public EndingConditionDistance(Path p, int maxGScore) : base(p)
		{
			this.maxGScore = maxGScore;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00032A6B File Offset: 0x00030E6B
		public override bool TargetFound(NodeRun node)
		{
			return (ulong)node.g >= (ulong)((long)this.maxGScore);
		}

		// Token: 0x0400045B RID: 1115
		public int maxGScore = 100;
	}
}
