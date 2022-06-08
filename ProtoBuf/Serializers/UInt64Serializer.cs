using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x020006A5 RID: 1701
	internal sealed class UInt64Serializer : IProtoSerializer
	{
		// Token: 0x06003221 RID: 12833 RVA: 0x00163134 File Offset: 0x00161534
		public UInt64Serializer(TypeModel model)
		{
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x0016313C File Offset: 0x0016153C
		public Type ExpectedType
		{
			get
			{
				return UInt64Serializer.expectedType;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x00163143 File Offset: 0x00161543
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x00163146 File Offset: 0x00161546
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x00163149 File Offset: 0x00161549
		public object Read(object value, ProtoReader source)
		{
			return source.ReadUInt64();
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x00163156 File Offset: 0x00161556
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt64((ulong)value, dest);
		}

		// Token: 0x04002EB4 RID: 11956
		private static readonly Type expectedType = typeof(ulong);
	}
}
