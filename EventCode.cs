using System;

// Token: 0x020000F2 RID: 242
public class EventCode
{
	// Token: 0x04000644 RID: 1604
	public const byte GameList = 230;

	// Token: 0x04000645 RID: 1605
	public const byte GameListUpdate = 229;

	// Token: 0x04000646 RID: 1606
	public const byte QueueState = 228;

	// Token: 0x04000647 RID: 1607
	public const byte Match = 227;

	// Token: 0x04000648 RID: 1608
	public const byte AppStats = 226;

	// Token: 0x04000649 RID: 1609
	public const byte LobbyStats = 224;

	// Token: 0x0400064A RID: 1610
	[Obsolete("TCP routing was removed after becoming obsolete.")]
	public const byte AzureNodeInfo = 210;

	// Token: 0x0400064B RID: 1611
	public const byte Join = 255;

	// Token: 0x0400064C RID: 1612
	public const byte Leave = 254;

	// Token: 0x0400064D RID: 1613
	public const byte PropertiesChanged = 253;

	// Token: 0x0400064E RID: 1614
	[Obsolete("Use PropertiesChanged now.")]
	public const byte SetProperties = 253;

	// Token: 0x0400064F RID: 1615
	public const byte ErrorInfo = 251;

	// Token: 0x04000650 RID: 1616
	public const byte CacheSliceChanged = 250;
}
