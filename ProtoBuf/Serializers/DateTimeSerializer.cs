using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000683 RID: 1667
	internal sealed class DateTimeSerializer : IProtoSerializer
	{
		// Token: 0x06003124 RID: 12580 RVA: 0x001607FD File Offset: 0x0015EBFD
		public DateTimeSerializer(TypeModel model)
		{
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06003125 RID: 12581 RVA: 0x00160805 File Offset: 0x0015EC05
		public Type ExpectedType
		{
			get
			{
				return DateTimeSerializer.expectedType;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x0016080C File Offset: 0x0015EC0C
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x0016080F File Offset: 0x0015EC0F
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x00160812 File Offset: 0x0015EC12
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadDateTime(source);
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x0016081F File Offset: 0x0015EC1F
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteDateTime((DateTime)value, dest);
		}

		// Token: 0x04002E64 RID: 11876
		private static readonly Type expectedType = typeof(DateTime);
	}
}
