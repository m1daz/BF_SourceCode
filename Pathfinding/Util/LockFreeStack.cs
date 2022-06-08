using System;
using System.Threading;

namespace Pathfinding.Util
{
	// Token: 0x020000C2 RID: 194
	public class LockFreeStack
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x00036B50 File Offset: 0x00034F50
		public void Push(Path p)
		{
			Path path;
			do
			{
				p.next = this.head;
				path = Interlocked.CompareExchange<Path>(ref this.head, p, p.next);
			}
			while (path != p.next);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00036B92 File Offset: 0x00034F92
		public Path PopAll()
		{
			return Interlocked.Exchange<Path>(ref this.head, null);
		}

		// Token: 0x040004C2 RID: 1218
		public Path head;
	}
}
