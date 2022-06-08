using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000684 RID: 1668
	internal sealed class DecimalSerializer : IProtoSerializer
	{
		// Token: 0x0600312B RID: 12587 RVA: 0x0016083E File Offset: 0x0015EC3E
		public DecimalSerializer(TypeModel model)
		{
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x00160846 File Offset: 0x0015EC46
		public Type ExpectedType
		{
			get
			{
				return DecimalSerializer.expectedType;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x0016084D File Offset: 0x0015EC4D
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x00160850 File Offset: 0x0015EC50
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x00160853 File Offset: 0x0015EC53
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadDecimal(source);
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x00160860 File Offset: 0x0015EC60
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteDecimal((decimal)value, dest);
		}

		// Token: 0x04002E65 RID: 11877
		private static readonly Type expectedType = typeof(decimal);
	}
}
