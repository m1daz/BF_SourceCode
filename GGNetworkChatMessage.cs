using System;
using ProtoBuf;

// Token: 0x020004F1 RID: 1265
[ProtoContract]
public class GGNetworkChatMessage
{
	// Token: 0x060023EC RID: 9196 RVA: 0x001128BC File Offset: 0x00110CBC
	public GGNetworkChatMessage()
	{
		this.receiverTeam = GGTeamType.Nil;
		this.receiverName = string.Empty;
		this.chatmessagesubtype = GGChatMessageSubType.Public;
	}

	// Token: 0x04002471 RID: 9329
	[ProtoMember(1, IsRequired = true)]
	public bool displayed;

	// Token: 0x04002472 RID: 9330
	[ProtoMember(2, IsRequired = true)]
	public GGTeamType team;

	// Token: 0x04002473 RID: 9331
	[ProtoMember(3, IsRequired = true)]
	public string name;

	// Token: 0x04002474 RID: 9332
	[ProtoMember(4, IsRequired = true)]
	public string content;

	// Token: 0x04002475 RID: 9333
	[ProtoMember(5, IsRequired = true)]
	public GGTeamType receiverTeam;

	// Token: 0x04002476 RID: 9334
	[ProtoMember(6, IsRequired = true)]
	public string receiverName;

	// Token: 0x04002477 RID: 9335
	[ProtoMember(7, IsRequired = true)]
	public GGChatMessageSubType chatmessagesubtype;
}
