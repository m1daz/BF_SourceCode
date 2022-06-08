using System;
using ProtoBuf;

// Token: 0x020004F2 RID: 1266
[ProtoContract]
public class GGNetworkSystemMessage
{
	// Token: 0x04002478 RID: 9336
	[ProtoMember(1, IsRequired = true)]
	public bool displayed;

	// Token: 0x04002479 RID: 9337
	[ProtoMember(2, IsRequired = true)]
	public GGColor color;

	// Token: 0x0400247A RID: 9338
	[ProtoMember(3, IsRequired = true)]
	public string content;
}
