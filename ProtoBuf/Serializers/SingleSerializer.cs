using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200069A RID: 1690
	internal sealed class SingleSerializer : IProtoSerializer
	{
		// Token: 0x060031BB RID: 12731 RVA: 0x00162091 File Offset: 0x00160491
		public SingleSerializer(TypeModel model)
		{
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x00162099 File Offset: 0x00160499
		public Type ExpectedType
		{
			get
			{
				return SingleSerializer.expectedType;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060031BD RID: 12733 RVA: 0x001620A0 File Offset: 0x001604A0
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x001620A3 File Offset: 0x001604A3
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x001620A6 File Offset: 0x001604A6
		public object Read(object value, ProtoReader source)
		{
			return source.ReadSingle();
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x001620B3 File Offset: 0x001604B3
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteSingle((float)value, dest);
		}

		// Token: 0x04002E93 RID: 11923
		private static readonly Type expectedType = typeof(float);
	}
}
