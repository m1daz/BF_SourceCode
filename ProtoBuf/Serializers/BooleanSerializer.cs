using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000680 RID: 1664
	internal sealed class BooleanSerializer : IProtoSerializer
	{
		// Token: 0x06003111 RID: 12561 RVA: 0x001606FE File Offset: 0x0015EAFE
		public BooleanSerializer(TypeModel model)
		{
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x00160706 File Offset: 0x0015EB06
		public Type ExpectedType
		{
			get
			{
				return BooleanSerializer.expectedType;
			}
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x0016070D File Offset: 0x0015EB0D
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteBoolean((bool)value, dest);
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x0016071B File Offset: 0x0015EB1B
		public object Read(object value, ProtoReader source)
		{
			return source.ReadBoolean();
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x00160728 File Offset: 0x0015EB28
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x0016072B File Offset: 0x0015EB2B
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002E61 RID: 11873
		private static readonly Type expectedType = typeof(bool);
	}
}
