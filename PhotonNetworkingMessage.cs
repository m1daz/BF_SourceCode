using System;

// Token: 0x020000DF RID: 223
public enum PhotonNetworkingMessage
{
	// Token: 0x040005B8 RID: 1464
	OnConnectedToPhoton,
	// Token: 0x040005B9 RID: 1465
	OnLeftRoom,
	// Token: 0x040005BA RID: 1466
	OnMasterClientSwitched,
	// Token: 0x040005BB RID: 1467
	OnPhotonCreateRoomFailed,
	// Token: 0x040005BC RID: 1468
	OnPhotonJoinRoomFailed,
	// Token: 0x040005BD RID: 1469
	OnCreatedRoom,
	// Token: 0x040005BE RID: 1470
	OnJoinedLobby,
	// Token: 0x040005BF RID: 1471
	OnLeftLobby,
	// Token: 0x040005C0 RID: 1472
	OnDisconnectedFromPhoton,
	// Token: 0x040005C1 RID: 1473
	OnConnectionFail,
	// Token: 0x040005C2 RID: 1474
	OnFailedToConnectToPhoton,
	// Token: 0x040005C3 RID: 1475
	OnReceivedRoomListUpdate,
	// Token: 0x040005C4 RID: 1476
	OnJoinedRoom,
	// Token: 0x040005C5 RID: 1477
	OnPhotonPlayerConnected,
	// Token: 0x040005C6 RID: 1478
	OnPhotonPlayerDisconnected,
	// Token: 0x040005C7 RID: 1479
	OnPhotonRandomJoinFailed,
	// Token: 0x040005C8 RID: 1480
	OnConnectedToMaster,
	// Token: 0x040005C9 RID: 1481
	OnPhotonSerializeView,
	// Token: 0x040005CA RID: 1482
	OnPhotonInstantiate,
	// Token: 0x040005CB RID: 1483
	OnPhotonMaxCccuReached,
	// Token: 0x040005CC RID: 1484
	OnPhotonCustomRoomPropertiesChanged,
	// Token: 0x040005CD RID: 1485
	OnPhotonPlayerPropertiesChanged,
	// Token: 0x040005CE RID: 1486
	OnUpdatedFriendList,
	// Token: 0x040005CF RID: 1487
	OnCustomAuthenticationFailed,
	// Token: 0x040005D0 RID: 1488
	OnCustomAuthenticationResponse,
	// Token: 0x040005D1 RID: 1489
	OnWebRpcResponse,
	// Token: 0x040005D2 RID: 1490
	OnOwnershipRequest,
	// Token: 0x040005D3 RID: 1491
	OnLobbyStatisticsUpdate
}
