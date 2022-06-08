using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

// Token: 0x02000108 RID: 264
public interface IPunCallbacks
{
	// Token: 0x0600079F RID: 1951
	void OnConnectedToPhoton();

	// Token: 0x060007A0 RID: 1952
	void OnLeftRoom();

	// Token: 0x060007A1 RID: 1953
	void OnMasterClientSwitched(PhotonPlayer newMasterClient);

	// Token: 0x060007A2 RID: 1954
	void OnPhotonCreateRoomFailed(object[] codeAndMsg);

	// Token: 0x060007A3 RID: 1955
	void OnPhotonJoinRoomFailed(object[] codeAndMsg);

	// Token: 0x060007A4 RID: 1956
	void OnCreatedRoom();

	// Token: 0x060007A5 RID: 1957
	void OnJoinedLobby();

	// Token: 0x060007A6 RID: 1958
	void OnLeftLobby();

	// Token: 0x060007A7 RID: 1959
	void OnFailedToConnectToPhoton(DisconnectCause cause);

	// Token: 0x060007A8 RID: 1960
	void OnConnectionFail(DisconnectCause cause);

	// Token: 0x060007A9 RID: 1961
	void OnDisconnectedFromPhoton();

	// Token: 0x060007AA RID: 1962
	void OnPhotonInstantiate(PhotonMessageInfo info);

	// Token: 0x060007AB RID: 1963
	void OnReceivedRoomListUpdate();

	// Token: 0x060007AC RID: 1964
	void OnJoinedRoom();

	// Token: 0x060007AD RID: 1965
	void OnPhotonPlayerConnected(PhotonPlayer newPlayer);

	// Token: 0x060007AE RID: 1966
	void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer);

	// Token: 0x060007AF RID: 1967
	void OnPhotonRandomJoinFailed(object[] codeAndMsg);

	// Token: 0x060007B0 RID: 1968
	void OnConnectedToMaster();

	// Token: 0x060007B1 RID: 1969
	void OnPhotonMaxCccuReached();

	// Token: 0x060007B2 RID: 1970
	void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged);

	// Token: 0x060007B3 RID: 1971
	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps);

	// Token: 0x060007B4 RID: 1972
	void OnUpdatedFriendList();

	// Token: 0x060007B5 RID: 1973
	void OnCustomAuthenticationFailed(string debugMessage);

	// Token: 0x060007B6 RID: 1974
	void OnCustomAuthenticationResponse(Dictionary<string, object> data);

	// Token: 0x060007B7 RID: 1975
	void OnWebRpcResponse(OperationResponse response);

	// Token: 0x060007B8 RID: 1976
	void OnOwnershipRequest(object[] viewAndPlayer);

	// Token: 0x060007B9 RID: 1977
	void OnLobbyStatisticsUpdate();
}
