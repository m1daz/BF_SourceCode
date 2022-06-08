using System;

namespace Pathfinding
{
	// Token: 0x0200000C RID: 12
	public struct Progress
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00003CA9 File Offset: 0x000020A9
		public Progress(float p, string d)
		{
			this.progress = p;
			this.description = d;
		}

		// Token: 0x04000069 RID: 105
		public float progress;

		// Token: 0x0400006A RID: 106
		public string description;
	}
}
