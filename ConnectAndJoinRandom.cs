using System;
using Photon;
using UnityEngine;

// Token: 0x02000137 RID: 311
public class ConnectAndJoinRandom : Photon.MonoBehaviour
{
	// Token: 0x06000963 RID: 2403 RVA: 0x00047BFA File Offset: 0x00045FFA
	public virtual void Start()
	{
		PhotonNetwork.autoJoinLobby = false;
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x00047C04 File Offset: 0x00046004
	public virtual void Update()
	{
		if (this.ConnectInUpdate && this.AutoConnect && !PhotonNetwork.connected)
		{
			Debug.Log("Update() was called by Unity. Scene is loaded. Let's connect to the Photon Master Server. Calling: PhotonNetwork.ConnectUsingSettings();");
			this.ConnectInUpdate = false;
			PhotonNetwork.ConnectUsingSettings(this.Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
		}
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x00047C67 File Offset: 0x00046067
	public virtual void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
		PhotonNetwork.JoinRandomRoom();
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x00047C79 File Offset: 0x00046079
	public virtual void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
		PhotonNetwork.JoinRandomRoom();
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00047C8C File Offset: 0x0004608C
	public virtual void OnPhotonRandomJoinFailed()
	{
		Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");
		PhotonNetwork.CreateRoom(null, new RoomOptions
		{
			MaxPlayers = 4
		}, null);
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00047CB9 File Offset: 0x000460B9
	public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.LogError("Cause: " + cause);
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x00047CD0 File Offset: 0x000460D0
	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");
	}

	// Token: 0x04000868 RID: 2152
	public bool AutoConnect = true;

	// Token: 0x04000869 RID: 2153
	public byte Version = 1;

	// Token: 0x0400086A RID: 2154
	private bool ConnectInUpdate = true;
}
