using System;

namespace ProtoBuf.Meta
{
	// Token: 0x0200065E RID: 1630
	public sealed class LockContentedEventArgs : EventArgs
	{
		// Token: 0x06002F86 RID: 12166 RVA: 0x0015B609 File Offset: 0x00159A09
		internal LockContentedEventArgs(string ownerStackTrace)
		{
			this.ownerStackTrace = ownerStackTrace;
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06002F87 RID: 12167 RVA: 0x0015B618 File Offset: 0x00159A18
		public string OwnerStackTrace
		{
			get
			{
				return this.ownerStackTrace;
			}
		}

		// Token: 0x04002DCE RID: 11726
		private readonly string ownerStackTrace;
	}
}
