using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000686 RID: 1670
	internal sealed class DoubleSerializer : IProtoSerializer
	{
		// Token: 0x06003138 RID: 12600 RVA: 0x0016092B File Offset: 0x0015ED2B
		public DoubleSerializer(TypeModel model)
		{
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x00160933 File Offset: 0x0015ED33
		public Type ExpectedType
		{
			get
			{
				return DoubleSerializer.expectedType;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x0016093A File Offset: 0x0015ED3A
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x0016093D File Offset: 0x0015ED3D
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x00160940 File Offset: 0x0015ED40
		public object Read(object value, ProtoReader source)
		{
			return source.ReadDouble();
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x0016094D File Offset: 0x0015ED4D
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteDouble((double)value, dest);
		}

		// Token: 0x04002E67 RID: 11879
		private static readonly Type expectedType = typeof(double);
	}
}
