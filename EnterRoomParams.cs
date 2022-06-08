using System;
using ExitGames.Client.Photon;

// Token: 0x020000EE RID: 238
internal class EnterRoomParams
{
	// Token: 0x04000612 RID: 1554
	public string RoomName;

	// Token: 0x04000613 RID: 1555
	public RoomOptions RoomOptions;

	// Token: 0x04000614 RID: 1556
	public TypedLobby Lobby;

	// Token: 0x04000615 RID: 1557
	public Hashtable PlayerProperties;

	// Token: 0x04000616 RID: 1558
	public bool OnGameServer = true;

	// Token: 0x04000617 RID: 1559
	public bool CreateIfNotExists;

	// Token: 0x04000618 RID: 1560
	public bool RejoinOnly;

	// Token: 0x04000619 RID: 1561
	public string[] ExpectedUsers;

	// Token: 0x0400061A RID: 1562
	public string AppVersion;
}
