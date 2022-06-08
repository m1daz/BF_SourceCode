using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200068D RID: 1677
	internal sealed class Int32Serializer : IProtoSerializer
	{
		// Token: 0x06003162 RID: 12642 RVA: 0x001619E5 File Offset: 0x0015FDE5
		public Int32Serializer(TypeModel model)
		{
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x001619ED File Offset: 0x0015FDED
		public Type ExpectedType
		{
			get
			{
				return Int32Serializer.expectedType;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x001619F4 File Offset: 0x0015FDF4
		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x001619F7 File Offset: 0x0015FDF7
		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x001619FA File Offset: 0x0015FDFA
		public object Read(object value, ProtoReader source)
		{
			return source.ReadInt32();
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x00161A07 File Offset: 0x0015FE07
		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteInt32((int)value, dest);
		}

		// Token: 0x04002E75 RID: 11893
		private static readonly Type expectedType = typeof(int);
	}
}
