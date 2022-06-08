using System;

namespace Pathfinding
{
	// Token: 0x02000011 RID: 17
	public struct PathThreadInfo
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003F25 File Offset: 0x00002325
		public PathThreadInfo(int index, AstarPath astar, NodeRunData runData)
		{
			this.threadIndex = index;
			this.astar = astar;
			this.runData = runData;
			this._lock = new object();
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003F47 File Offset: 0x00002347
		public object Lock
		{
			get
			{
				return this._lock;
			}
		}

		// Token: 0x0400007C RID: 124
		public int threadIndex;

		// Token: 0x0400007D RID: 125
		public AstarPath astar;

		// Token: 0x0400007E RID: 126
		public NodeRunData runData;

		// Token: 0x0400007F RID: 127
		private object _lock;
	}
}
