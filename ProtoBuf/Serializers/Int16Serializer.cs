using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200068C RID: 1676
	internal sealed class Int16Serializer : IProtoSerializer
	{
		// Token: 0x0600315B RID: 12635 RVA: 0x001619A4 File Offset: 0x0015FDA4
		public Int16Serializer(TypeModel model)
		{
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x001619AC File Offset: 0x0015FDAC
		public Type ExpectedType
		{
			get
			{
				return Int16Serializer.expectedType;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x001619B3 File Offset: 0x0015FDB3
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x001619B6 File Offset: 0x0015FDB6
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x001619B9 File Offset: 0x0015FDB9
		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt16();
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x001619C6 File Offset: 0x0015FDC6
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt16((short)value, dest);
		}

		// Token: 0x04002E74 RID: 11892
		private static readonly Type expectedType = typeof(short);
	}
}
