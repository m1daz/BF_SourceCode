using System;
using System.Text;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class SupportLogging : MonoBehaviour
{
	// Token: 0x060009F2 RID: 2546 RVA: 0x0004A308 File Offset: 0x00048708
	public void Start()
	{
		if (this.LogTrafficStats)
		{
			base.InvokeRepeating("LogStats", 10f, 10f);
		}
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0004A32A File Offset: 0x0004872A
	protected void OnApplicationPause(bool pause)
	{
		Debug.Log(string.Concat(new object[]
		{
			"SupportLogger OnApplicationPause: ",
			pause,
			" connected: ",
			PhotonNetwork.connected
		}));
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x0004A362 File Offset: 0x00048762
	public void OnApplicationQuit()
	{
		base.CancelInvoke();
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x0004A36A File Offset: 0x0004876A
	public void LogStats()
	{
		if (this.LogTrafficStats)
		{
			Debug.Log("SupportLogger " + PhotonNetwork.NetworkStatisticsToString());
		}
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x0004A38C File Offset: 0x0004878C
	private void LogBasics()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("SupportLogger Info: PUN {0}: ", "1.78");
		stringBuilder.AppendFormat("AppID: {0}*** GameVersion: {1} ", PhotonNetwork.networkingPeer.AppId.Substring(0, 8), PhotonNetwork.networkingPeer.AppVersion);
		stringBuilder.AppendFormat("Server: {0}. Region: {1} ", PhotonNetwork.ServerAddress, PhotonNetwork.networkingPeer.CloudRegion);
		stringBuilder.AppendFormat("HostType: {0} ", PhotonNetwork.PhotonServerSettings.HostType);
		Debug.Log(stringBuilder.ToString());
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x0004A41D File Offset: 0x0004881D
	public void OnConnectedToPhoton()
	{
		Debug.Log("SupportLogger OnConnectedToPhoton().");
		this.LogBasics();
		if (this.LogTrafficStats)
		{
			PhotonNetwork.NetworkStatisticsEnabled = true;
		}
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x0004A440 File Offset: 0x00048840
	public void OnFailedToConnectToPhoton(DisconnectCause cause)
	{
		Debug.Log("SupportLogger OnFailedToConnectToPhoton(" + cause + ").");
		this.LogBasics();
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x0004A462 File Offset: 0x00048862
	public void OnJoinedLobby()
	{
		Debug.Log("SupportLogger OnJoinedLobby(" + PhotonNetwork.lobby + ").");
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x0004A480 File Offset: 0x00048880
	public void OnJoinedRoom()
	{
		Debug.Log(string.Concat(new object[]
		{
			"SupportLogger OnJoinedRoom(",
			PhotonNetwork.room,
			"). ",
			PhotonNetwork.lobby,
			" GameServer:",
			PhotonNetwork.ServerAddress
		}));
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x0004A4D0 File Offset: 0x000488D0
	public void OnCreatedRoom()
	{
		Debug.Log(string.Concat(new object[]
		{
			"SupportLogger OnCreatedRoom(",
			PhotonNetwork.room,
			"). ",
			PhotonNetwork.lobby,
			" GameServer:",
			PhotonNetwork.ServerAddress
		}));
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x0004A51D File Offset: 0x0004891D
	public void OnLeftRoom()
	{
		Debug.Log("SupportLogger OnLeftRoom().");
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x0004A529 File Offset: 0x00048929
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("SupportLogger OnDisconnectedFromPhoton().");
	}

	// Token: 0x040008C1 RID: 2241
	public bool LogTrafficStats;
}
