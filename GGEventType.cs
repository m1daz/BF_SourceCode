using System;

// Token: 0x02000501 RID: 1281
public enum GGEventType
{
	// Token: 0x040024FE RID: 9470
	EventSkinReceive,
	// Token: 0x040024FF RID: 9471
	EventTeamChange,
	// Token: 0x04002500 RID: 9472
	EventOnJoinLobby,
	// Token: 0x04002501 RID: 9473
	EventNewRound,
	// Token: 0x04002502 RID: 9474
	EventEndCurrentRound,
	// Token: 0x04002503 RID: 9475
	EventHatReceive,
	// Token: 0x04002504 RID: 9476
	EventCapeReceive,
	// Token: 0x04002505 RID: 9477
	EventWeaponPropertiesReceive,
	// Token: 0x04002506 RID: 9478
	EventBootReceive,
	// Token: 0x04002507 RID: 9479
	EventPlayerSeasonInfo,
	// Token: 0x04002508 RID: 9480
	EventNoConnection = 1000,
	// Token: 0x04002509 RID: 9481
	RoomCreateSameNameRoom,
	// Token: 0x0400250A RID: 9482
	RoomJoinRoomNotExistOrFull,
	// Token: 0x0400250B RID: 9483
	RoomJoinPasswordNotCorrect,
	// Token: 0x0400250C RID: 9484
	RoomJoinOrCreateNoConnection,
	// Token: 0x0400250D RID: 9485
	EventDisconnectedToConnected,
	// Token: 0x0400250E RID: 9486
	RandomJoinRoomNotAvailableRoom,
	// Token: 0x0400250F RID: 9487
	FirstJoinDynamicSwitchPlayerTeam,
	// Token: 0x04002510 RID: 9488
	RoomToLobbyWhenNotClickNewRound,
	// Token: 0x04002511 RID: 9489
	RoomCreatedSuccess,
	// Token: 0x04002512 RID: 9490
	LoadingPanelHide = 2000,
	// Token: 0x04002513 RID: 9491
	EventPlayerLeave
}
