using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200069E RID: 1694
	internal sealed class SystemTypeSerializer : IProtoSerializer
	{
		// Token: 0x060031DF RID: 12767 RVA: 0x00162484 File Offset: 0x00160884
		public SystemTypeSerializer(TypeModel model)
		{
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060031E0 RID: 12768 RVA: 0x0016248C File Offset: 0x0016088C
		public Type ExpectedType
		{
			get
			{
				return SystemTypeSerializer.expectedType;
			}
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x00162493 File Offset: 0x00160893
		void IProtoSerializer.Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteType((Type)value, dest);
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x001624A1 File Offset: 0x001608A1
		object IProtoSerializer.Read(object value, ProtoReader source)
		{
			return source.ReadType();
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x001624A9 File Offset: 0x001608A9
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060031E4 RID: 12772 RVA: 0x001624AC File Offset: 0x001608AC
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002E9E RID: 11934
		private static readonly Type expectedType = typeof(Type);
	}
}
