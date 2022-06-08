using System;

namespace ProtoBuf.Serializers
{
	// Token: 0x0200068F RID: 1679
	internal interface IProtoSerializer
	{
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06003170 RID: 12656
		Type ExpectedType { get; }

		// Token: 0x06003171 RID: 12657
		void Write(object value, ProtoWriter dest);

		// Token: 0x06003172 RID: 12658
		object Read(object value, ProtoReader source);

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06003173 RID: 12659
		bool RequiresOldValue { get; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06003174 RID: 12660
		bool ReturnsValue { get; }
	}
}
