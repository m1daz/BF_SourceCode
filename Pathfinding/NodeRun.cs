using System;

namespace Pathfinding
{
	// Token: 0x02000033 RID: 51
	public class NodeRun
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000C555 File Offset: 0x0000A955
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000C55D File Offset: 0x0000A95D
		public Node node { get; set; }

		// Token: 0x06000196 RID: 406 RVA: 0x0000C566 File Offset: 0x0000A966
		public void Reset()
		{
			this.g = 0U;
			this.h = 0U;
			this.node = null;
			this.pathID = 0;
			this.cost = 0U;
			this.parent = null;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000C592 File Offset: 0x0000A992
		public uint f
		{
			get
			{
				return this.g + this.h;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C5A1 File Offset: 0x0000A9A1
		public void Link(Node node, int index)
		{
			this.node = node;
			if (index != node.GetNodeIndex())
			{
				throw new Exception("Node indices out of sync when creating NodeRun data (node index != specified index)");
			}
		}

		// Token: 0x04000171 RID: 369
		public uint g;

		// Token: 0x04000172 RID: 370
		public uint h;

		// Token: 0x04000174 RID: 372
		public ushort pathID;

		// Token: 0x04000175 RID: 373
		public uint cost;

		// Token: 0x04000176 RID: 374
		public NodeRun parent;
	}
}
