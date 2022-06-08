using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x02000112 RID: 274
internal class PhotonHandler : MonoBehaviour
{
	// Token: 0x06000802 RID: 2050 RVA: 0x000414E0 File Offset: 0x0003F8E0
	protected void Awake()
	{
		if (PhotonHandler.SP != null && PhotonHandler.SP != this && PhotonHandler.SP.gameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(PhotonHandler.SP.gameObject);
		}
		PhotonHandler.SP = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.updateInterval = 1000 / PhotonNetwork.sendRate;
		this.updateIntervalOnSerialize = 1000 / PhotonNetwork.sendRateOnSerialize;
		PhotonHandler.StartFallbackSendAckThread();
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x00041569 File Offset: 0x0003F969
	protected void OnLevelWasLoaded(int level)
	{
		PhotonNetwork.networkingPeer.NewSceneLoaded();
		PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced(SceneManagerHelper.ActiveSceneName);
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x00041584 File Offset: 0x0003F984
	protected void OnApplicationQuit()
	{
		PhotonHandler.AppQuits = true;
		PhotonHandler.StopFallbackSendAckThread();
		PhotonNetwork.Disconnect();
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00041598 File Offset: 0x0003F998
	protected void OnApplicationPause(bool pause)
	{
		if (PhotonNetwork.BackgroundTimeout > 0.1f)
		{
			if (PhotonHandler.timerToStopConnectionInBackground == null)
			{
				PhotonHandler.timerToStopConnectionInBackground = new Stopwatch();
			}
			PhotonHandler.timerToStopConnectionInBackground.Reset();
			if (pause)
			{
				PhotonHandler.timerToStopConnectionInBackground.Start();
			}
			else
			{
				PhotonHandler.timerToStopConnectionInBackground.Stop();
			}
		}
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x000415F1 File Offset: 0x0003F9F1
	protected void OnDestroy()
	{
		PhotonHandler.StopFallbackSendAckThread();
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x000415F8 File Offset: 0x0003F9F8
	protected void Update()
	{
		if (PhotonNetwork.networkingPeer == null)
		{
			UnityEngine.Debug.LogError("NetworkPeer broke!");
			return;
		}
		if (PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated || PhotonNetwork.connectionStateDetailed == ClientState.Disconnected || PhotonNetwork.offlineMode)
		{
			return;
		}
		if (!PhotonNetwork.isMessageQueueRunning)
		{
			return;
		}
		bool flag = true;
		while (PhotonNetwork.isMessageQueueRunning && flag)
		{
			flag = PhotonNetwork.networkingPeer.DispatchIncomingCommands();
		}
		int num = (int)(Time.realtimeSinceStartup * 1000f);
		if (PhotonNetwork.isMessageQueueRunning && num > this.nextSendTickCountOnSerialize)
		{
			PhotonNetwork.networkingPeer.RunViewUpdate();
			this.nextSendTickCountOnSerialize = num + this.updateIntervalOnSerialize;
			this.nextSendTickCount = 0;
		}
		num = (int)(Time.realtimeSinceStartup * 1000f);
		if (num > this.nextSendTickCount)
		{
			bool flag2 = true;
			while (PhotonNetwork.isMessageQueueRunning && flag2)
			{
				flag2 = PhotonNetwork.networkingPeer.SendOutgoingCommands();
			}
			this.nextSendTickCount = num + this.updateInterval;
		}
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x000416F4 File Offset: 0x0003FAF4
	protected void OnJoinedRoom()
	{
		PhotonNetwork.networkingPeer.LoadLevelIfSynced();
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x00041700 File Offset: 0x0003FB00
	protected void OnCreatedRoom()
	{
		PhotonNetwork.networkingPeer.SetLevelInPropsIfSynced(SceneManagerHelper.ActiveSceneName);
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00041711 File Offset: 0x0003FB11
	public static void StartFallbackSendAckThread()
	{
		if (PhotonHandler.sendThreadShouldRun)
		{
			return;
		}
		PhotonHandler.sendThreadShouldRun = true;
		if (PhotonHandler.<>f__mg$cache0 == null)
		{
			PhotonHandler.<>f__mg$cache0 = new Func<bool>(PhotonHandler.FallbackSendAckThread);
		}
		SupportClass.CallInBackground(PhotonHandler.<>f__mg$cache0);
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00041746 File Offset: 0x0003FB46
	public static void StopFallbackSendAckThread()
	{
		PhotonHandler.sendThreadShouldRun = false;
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00041750 File Offset: 0x0003FB50
	public static bool FallbackSendAckThread()
	{
		if (PhotonHandler.sendThreadShouldRun && PhotonNetwork.networkingPeer != null)
		{
			if (PhotonHandler.timerToStopConnectionInBackground != null && PhotonNetwork.BackgroundTimeout > 0.1f && (float)PhotonHandler.timerToStopConnectionInBackground.ElapsedMilliseconds > PhotonNetwork.BackgroundTimeout * 1000f)
			{
				if (PhotonNetwork.connected)
				{
					PhotonNetwork.Disconnect();
				}
				PhotonHandler.timerToStopConnectionInBackground.Stop();
				PhotonHandler.timerToStopConnectionInBackground.Reset();
				return PhotonHandler.sendThreadShouldRun;
			}
			if (PhotonNetwork.networkingPeer.ConnectionTime - PhotonNetwork.networkingPeer.LastSendOutgoingTime > 200)
			{
				PhotonNetwork.networkingPeer.SendAcksOnly();
			}
		}
		return PhotonHandler.sendThreadShouldRun;
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x0600080D RID: 2061 RVA: 0x00041800 File Offset: 0x0003FC00
	// (set) Token: 0x0600080E RID: 2062 RVA: 0x00041832 File Offset: 0x0003FC32
	internal static CloudRegionCode BestRegionCodeInPreferences
	{
		get
		{
			string @string = PlayerPrefs.GetString("PUNCloudBestRegion", string.Empty);
			if (!string.IsNullOrEmpty(@string))
			{
				return Region.Parse(@string);
			}
			return CloudRegionCode.none;
		}
		set
		{
			if (value == CloudRegionCode.none)
			{
				PlayerPrefs.DeleteKey("PUNCloudBestRegion");
			}
			else
			{
				PlayerPrefs.SetString("PUNCloudBestRegion", value.ToString());
			}
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00041861 File Offset: 0x0003FC61
	protected internal static void PingAvailableRegionsAndConnectToBest()
	{
		PhotonHandler.SP.StartCoroutine(PhotonHandler.SP.PingAvailableRegionsCoroutine(true));
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0004187C File Offset: 0x0003FC7C
	internal IEnumerator PingAvailableRegionsCoroutine(bool connectToBest)
	{
		PhotonHandler.BestRegionCodeCurrently = CloudRegionCode.none;
		while (PhotonNetwork.networkingPeer.AvailableRegions == null)
		{
			if (PhotonNetwork.connectionStateDetailed != ClientState.ConnectingToNameServer && PhotonNetwork.connectionStateDetailed != ClientState.ConnectedToNameServer)
			{
				UnityEngine.Debug.LogError("Call ConnectToNameServer to ping available regions.");
				yield break;
			}
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"Waiting for AvailableRegions. State: ",
				PhotonNetwork.connectionStateDetailed,
				" Server: ",
				PhotonNetwork.Server,
				" PhotonNetwork.networkingPeer.AvailableRegions ",
				PhotonNetwork.networkingPeer.AvailableRegions != null
			}));
			yield return new WaitForSeconds(0.25f);
		}
		if (PhotonNetwork.networkingPeer.AvailableRegions == null || PhotonNetwork.networkingPeer.AvailableRegions.Count == 0)
		{
			UnityEngine.Debug.LogError("No regions available. Are you sure your appid is valid and setup?");
			yield break;
		}
		PhotonPingManager pingManager = new PhotonPingManager();
		foreach (Region region in PhotonNetwork.networkingPeer.AvailableRegions)
		{
			PhotonHandler.SP.StartCoroutine(pingManager.PingSocket(region));
		}
		while (!pingManager.Done)
		{
			yield return new WaitForSeconds(0.1f);
		}
		Region best = pingManager.BestRegion;
		PhotonHandler.BestRegionCodeCurrently = best.Code;
		PhotonHandler.BestRegionCodeInPreferences = best.Code;
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"Found best region: ",
			best.Code,
			" ping: ",
			best.Ping,
			". Calling ConnectToRegionMaster() is: ",
			connectToBest
		}));
		if (connectToBest)
		{
			PhotonNetwork.networkingPeer.ConnectToRegionMaster(best.Code);
		}
		yield break;
	}

	// Token: 0x04000771 RID: 1905
	public static PhotonHandler SP;

	// Token: 0x04000772 RID: 1906
	public int updateInterval;

	// Token: 0x04000773 RID: 1907
	public int updateIntervalOnSerialize;

	// Token: 0x04000774 RID: 1908
	private int nextSendTickCount;

	// Token: 0x04000775 RID: 1909
	private int nextSendTickCountOnSerialize;

	// Token: 0x04000776 RID: 1910
	private static bool sendThreadShouldRun;

	// Token: 0x04000777 RID: 1911
	private static Stopwatch timerToStopConnectionInBackground;

	// Token: 0x04000778 RID: 1912
	protected internal static bool AppQuits;

	// Token: 0x04000779 RID: 1913
	protected internal static Type PingImplementation;

	// Token: 0x0400077A RID: 1914
	private const string PlayerPrefsKey = "PUNCloudBestRegion";

	// Token: 0x0400077B RID: 1915
	internal static CloudRegionCode BestRegionCodeCurrently = CloudRegionCode.none;

	// Token: 0x0400077C RID: 1916
	[CompilerGenerated]
	private static Func<bool> <>f__mg$cache0;
}
