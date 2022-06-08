using System;
using ProtoBuf;

// Token: 0x0200051A RID: 1306
[ProtoContract]
public class GGMessage
{
	// Token: 0x0400257B RID: 9595
	[ProtoMember(1, IsRequired = true)]
	public GGMessageType messageType;

	// Token: 0x0400257C RID: 9596
	[ProtoMember(2, IsRequired = true)]
	public GGMessageContent messageContent;
}
