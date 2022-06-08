using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x020006A4 RID: 1700
	internal sealed class UInt32Serializer : IProtoSerializer
	{
		// Token: 0x0600321A RID: 12826 RVA: 0x001630F3 File Offset: 0x001614F3
		public UInt32Serializer(TypeModel model)
		{
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x001630FB File Offset: 0x001614FB
		public Type ExpectedType
		{
			get
			{
				return UInt32Serializer.expectedType;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x00163102 File Offset: 0x00161502
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x00163105 File Offset: 0x00161505
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x00163108 File Offset: 0x00161508
		public object Read(object value, ProtoReader source)
		{
			return source.ReadUInt32();
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x00163115 File Offset: 0x00161515
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt32((uint)value, dest);
		}

		// Token: 0x04002EB3 RID: 11955
		private static readonly Type expectedType = typeof(uint);
	}
}
