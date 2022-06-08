using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200068A RID: 1674
	internal sealed class GuidSerializer : IProtoSerializer
	{
		// Token: 0x0600314F RID: 12623 RVA: 0x00160DB4 File Offset: 0x0015F1B4
		public GuidSerializer(TypeModel model)
		{
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x00160DBC File Offset: 0x0015F1BC
		public Type ExpectedType
		{
			get
			{
				return GuidSerializer.expectedType;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x00160DC3 File Offset: 0x0015F1C3
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x00160DC6 File Offset: 0x0015F1C6
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x00160DC9 File Offset: 0x0015F1C9
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteGuid((Guid)value, dest);
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x00160DD7 File Offset: 0x0015F1D7
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadGuid(source);
		}

		// Token: 0x04002E6F RID: 11887
		private static readonly Type expectedType = typeof(Guid);
	}
}
