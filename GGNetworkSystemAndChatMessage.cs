using System;

// Token: 0x020004F3 RID: 1267
public class GGNetworkSystemAndChatMessage
{
	// Token: 0x060023EE RID: 9198 RVA: 0x001128E5 File Offset: 0x00110CE5
	public GGNetworkSystemAndChatMessage()
	{
		this.receiverTeam = GGTeamType.Nil;
		this.receiverName = string.Empty;
		this.chatmessagesubtype = GGChatMessageSubType.Public;
	}

	// Token: 0x0400247B RID: 9339
	public GGChatMessageType chatMesaageType;

	// Token: 0x0400247C RID: 9340
	public bool displayed;

	// Token: 0x0400247D RID: 9341
	public GGColor color;

	// Token: 0x0400247E RID: 9342
	public GGTeamType team;

	// Token: 0x0400247F RID: 9343
	public string name;

	// Token: 0x04002480 RID: 9344
	public string content;

	// Token: 0x04002481 RID: 9345
	public GGTeamType receiverTeam;

	// Token: 0x04002482 RID: 9346
	public string receiverName;

	// Token: 0x04002483 RID: 9347
	public GGChatMessageSubType chatmessagesubtype;
}
