using System;
using ExitGames.Client.Photon;

// Token: 0x020000ED RID: 237
internal class OpJoinRandomRoomParams
{
	// Token: 0x0400060B RID: 1547
	public Hashtable ExpectedCustomRoomProperties;

	// Token: 0x0400060C RID: 1548
	public byte ExpectedMaxPlayers;

	// Token: 0x0400060D RID: 1549
	public MatchmakingMode MatchingType;

	// Token: 0x0400060E RID: 1550
	public TypedLobby TypedLobby;

	// Token: 0x0400060F RID: 1551
	public string SqlLobbyFilter;

	// Token: 0x04000610 RID: 1552
	public string[] ExpectedUsers;

	// Token: 0x04000611 RID: 1553
	public string AppVersion;
}
