using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000699 RID: 1689
	internal sealed class SByteSerializer : IProtoSerializer
	{
		// Token: 0x060031B4 RID: 12724 RVA: 0x00162050 File Offset: 0x00160450
		public SByteSerializer(TypeModel model)
		{
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060031B5 RID: 12725 RVA: 0x00162058 File Offset: 0x00160458
		public Type ExpectedType
		{
			get
			{
				return SByteSerializer.expectedType;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x0016205F File Offset: 0x0016045F
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060031B7 RID: 12727 RVA: 0x00162062 File Offset: 0x00160462
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x00162065 File Offset: 0x00160465
		public object Read(object value, ProtoReader source)
		{
			return source.ReadSByte();
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x00162072 File Offset: 0x00160472
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteSByte((sbyte)value, dest);
		}

		// Token: 0x04002E92 RID: 11922
		private static readonly Type expectedType = typeof(sbyte);
	}
}
