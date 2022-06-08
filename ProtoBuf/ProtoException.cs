using System;

namespace ProtoBuf
{
	// Token: 0x02000670 RID: 1648
	public class ProtoException : Exception
	{
		// Token: 0x06003038 RID: 12344 RVA: 0x0015CA29 File Offset: 0x0015AE29
		public ProtoException()
		{
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x0015CA31 File Offset: 0x0015AE31
		public ProtoException(string message) : base(message)
		{
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x0015CA3A File Offset: 0x0015AE3A
		public ProtoException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
