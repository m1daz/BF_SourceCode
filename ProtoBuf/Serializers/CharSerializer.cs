using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000682 RID: 1666
	internal sealed class CharSerializer : UInt16Serializer
	{
		// Token: 0x0600311F RID: 12575 RVA: 0x001607C1 File Offset: 0x0015EBC1
		public CharSerializer(TypeModel model) : base(model)
		{
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x001607CA File Offset: 0x0015EBCA
		public override Type ExpectedType
		{
			get
			{
				return CharSerializer.expectedType;
			}
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x001607D1 File Offset: 0x0015EBD1
		public override void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt16((ushort)((char)value), dest);
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x001607DF File Offset: 0x0015EBDF
		public override object Read(object value, ProtoReader source)
		{
			return (char)source.ReadUInt16();
		}

		// Token: 0x04002E63 RID: 11875
		private static readonly Type expectedType = typeof(char);
	}
}
