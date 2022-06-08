using System;
using ProtoBuf.Meta;

namespace ProtoBuf.Serializers
{
	// Token: 0x02000694 RID: 1684
	internal sealed class NetObjectSerializer : IProtoSerializer
	{
		// Token: 0x06003191 RID: 12689 RVA: 0x00161B2C File Offset: 0x0015FF2C
		public NetObjectSerializer(TypeModel model, Type type, int key, BclHelpers.NetObjectOptions options)
		{
			bool flag = (byte)(options & BclHelpers.NetObjectOptions.DynamicType) != 0;
			this.key = ((!flag) ? key : -1);
			this.type = ((!flag) ? type : model.MapType(typeof(object)));
			this.options = options;
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x00161B88 File Offset: 0x0015FF88
		public Type ExpectedType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x00161B90 File Offset: 0x0015FF90
		public bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x00161B93 File Offset: 0x0015FF93
		public bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x00161B96 File Offset: 0x0015FF96
		public object Read(object value, ProtoReader source)
		{
			return BclHelpers.ReadNetObject(value, source, this.key, (this.type != typeof(object)) ? this.type : null, this.options);
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x00161BCC File Offset: 0x0015FFCC
		public void Write(object value, ProtoWriter dest)
		{
			BclHelpers.WriteNetObject(value, dest, this.key, this.options);
		}

		// Token: 0x04002E87 RID: 11911
		private readonly int key;

		// Token: 0x04002E88 RID: 11912
		private readonly Type type;

		// Token: 0x04002E89 RID: 11913
		private readonly BclHelpers.NetObjectOptions options;
	}
}
