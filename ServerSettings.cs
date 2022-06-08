using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x02000123 RID: 291
[Serializable]
public class ServerSettings : ScriptableObject
{
	// Token: 0x06000924 RID: 2340 RVA: 0x0004633D File Offset: 0x0004473D
	public void UseCloudBestRegion(string cloudAppid)
	{
		this.HostType = ServerSettings.HostingOption.BestRegion;
		this.AppID = cloudAppid;
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x0004634D File Offset: 0x0004474D
	public void UseCloud(string cloudAppid)
	{
		this.HostType = ServerSettings.HostingOption.PhotonCloud;
		this.AppID = cloudAppid;
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x0004635D File Offset: 0x0004475D
	public void UseCloud(string cloudAppid, CloudRegionCode code)
	{
		this.HostType = ServerSettings.HostingOption.PhotonCloud;
		this.AppID = cloudAppid;
		this.PreferredRegion = code;
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x00046374 File Offset: 0x00044774
	public void UseMyServer(string serverAddress, int serverPort, string application)
	{
		this.HostType = ServerSettings.HostingOption.SelfHosted;
		this.AppID = ((application == null) ? "master" : application);
		this.ServerAddress = serverAddress;
		this.ServerPort = serverPort;
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x000463A2 File Offset: 0x000447A2
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"ServerSettings: ",
			this.HostType,
			" ",
			this.ServerAddress
		});
	}

	// Token: 0x040007F9 RID: 2041
	public ServerSettings.HostingOption HostType;

	// Token: 0x040007FA RID: 2042
	public ConnectionProtocol Protocol;

	// Token: 0x040007FB RID: 2043
	public string ServerAddress = string.Empty;

	// Token: 0x040007FC RID: 2044
	public int ServerPort = 5055;

	// Token: 0x040007FD RID: 2045
	public string AppID = string.Empty;

	// Token: 0x040007FE RID: 2046
	public string VoiceAppID = string.Empty;

	// Token: 0x040007FF RID: 2047
	public CloudRegionCode PreferredRegion;

	// Token: 0x04000800 RID: 2048
	public CloudRegionFlag EnabledRegions = (CloudRegionFlag)(-1);

	// Token: 0x04000801 RID: 2049
	public bool JoinLobby;

	// Token: 0x04000802 RID: 2050
	public bool EnableLobbyStatistics;

	// Token: 0x04000803 RID: 2051
	public List<string> RpcList = new List<string>();

	// Token: 0x04000804 RID: 2052
	[HideInInspector]
	public bool DisableAutoOpenWizard;

	// Token: 0x02000124 RID: 292
	public enum HostingOption
	{
		// Token: 0x04000806 RID: 2054
		NotSet,
		// Token: 0x04000807 RID: 2055
		PhotonCloud,
		// Token: 0x04000808 RID: 2056
		SelfHosted,
		// Token: 0x04000809 RID: 2057
		OfflineMode,
		// Token: 0x0400080A RID: 2058
		BestRegion
	}
}
