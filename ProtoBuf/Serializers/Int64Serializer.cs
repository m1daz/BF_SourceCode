using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200068E RID: 1678
	internal sealed class Int64Serializer : IProtoSerializer
	{
		// Token: 0x06003169 RID: 12649 RVA: 0x00161A26 File Offset: 0x0015FE26
		public Int64Serializer(TypeModel model)
		{
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x00161A2E File Offset: 0x0015FE2E
		public Type ExpectedType
		{
			get
			{
				return Int64Serializer.expectedType;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x00161A35 File Offset: 0x0015FE35
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x00161A38 File Offset: 0x0015FE38
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x00161A3B File Offset: 0x0015FE3B
		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt64();
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x00161A48 File Offset: 0x0015FE48
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt64((long)value, dest);
		}

		// Token: 0x04002E76 RID: 11894
		private static readonly Type expectedType = typeof(long);
	}
}
