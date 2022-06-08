using System;
using System.Text;

namespace Pathfinding
{
	// Token: 0x02000032 RID: 50
	public class NodeRunData
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000C4E3 File Offset: 0x0000A8E3
		public StringBuilder DebugStringBuilder
		{
			get
			{
				return this.stringBuilder;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000C4EB File Offset: 0x0000A8EB
		public void Initialize(Path p)
		{
			this.path = p;
			this.pathID = p.pathID;
			this.open.Clear();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000C50C File Offset: 0x0000A90C
		public void ClearPathIDs()
		{
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i] != null)
				{
					this.nodes[i].pathID = 0;
				}
			}
		}

		// Token: 0x0400016C RID: 364
		public Path path;

		// Token: 0x0400016D RID: 365
		public ushort pathID;

		// Token: 0x0400016E RID: 366
		public NodeRun[] nodes;

		// Token: 0x0400016F RID: 367
		public BinaryHeapM open = new BinaryHeapM(512);

		// Token: 0x04000170 RID: 368
		private StringBuilder stringBuilder = new StringBuilder();
	}
}
