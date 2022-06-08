using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000681 RID: 1665
	internal sealed class ByteSerializer : IProtoSerializer
	{
		// Token: 0x06003118 RID: 12568 RVA: 0x0016073F File Offset: 0x0015EB3F
		public ByteSerializer(TypeModel model)
		{
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x00160747 File Offset: 0x0015EB47
		public Type ExpectedType
		{
			get
			{
				return ByteSerializer.expectedType;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x0016074E File Offset: 0x0015EB4E
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x00160751 File Offset: 0x0015EB51
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x00160754 File Offset: 0x0015EB54
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteByte((byte)value, dest);
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x00160762 File Offset: 0x0015EB62
		public object Read(object value, ProtoReader source)
		{
			return source.ReadByte();
		}

		// Token: 0x04002E62 RID: 11874
		private static readonly Type expectedType = typeof(byte);
	}
}
