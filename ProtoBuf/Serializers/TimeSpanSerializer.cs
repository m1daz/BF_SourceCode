using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x020006A0 RID: 1696
	internal sealed class TimeSpanSerializer : IProtoSerializer
	{
		// Token: 0x060031F1 RID: 12785 RVA: 0x00162616 File Offset: 0x00160A16
		public TimeSpanSerializer(TypeModel model)
		{
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x0016261E File Offset: 0x00160A1E
		public Type ExpectedType
		{
			get
			{
				return TimeSpanSerializer.expectedType;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x00162625 File Offset: 0x00160A25
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x00162628 File Offset: 0x00160A28
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x0016262B File Offset: 0x00160A2B
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadTimeSpan(source);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x00162638 File Offset: 0x00160A38
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteTimeSpan((TimeSpan)value, dest);
		}

		// Token: 0x04002EA2 RID: 11938
		private static readonly Type expectedType = typeof(TimeSpan);
	}
}
