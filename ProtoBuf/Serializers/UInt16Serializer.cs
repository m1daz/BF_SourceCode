using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x020006A3 RID: 1699
	internal class UInt16Serializer : IProtoSerializer
	{
		// Token: 0x06003213 RID: 12819 RVA: 0x00160780 File Offset: 0x0015EB80
		public UInt16Serializer(TypeModel model)
		{
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06003214 RID: 12820 RVA: 0x00160788 File Offset: 0x0015EB88
		public virtual Type ExpectedType
		{
			get
			{
				return UInt16Serializer.expectedType;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06003215 RID: 12821 RVA: 0x0016078F File Offset: 0x0015EB8F
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06003216 RID: 12822 RVA: 0x00160792 File Offset: 0x0015EB92
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x00160795 File Offset: 0x0015EB95
		public virtual object Read(object value, ProtoReader source)
		{
			return source.ReadUInt16();
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x001607A2 File Offset: 0x0015EBA2
		public virtual void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteUInt16((ushort)value, dest);
		}

		// Token: 0x04002EB2 RID: 11954
		private static readonly Type expectedType = typeof(ushort);
	}
}
