using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000526 RID: 1318
public class GGNetworkManageGlobalInfo : MonoBehaviour
{
	// Token: 0x0600252B RID: 9515 RVA: 0x00115C08 File Offset: 0x00114008
	private void Awake()
	{
		GGNetworkManageGlobalInfo.mInstance = this;
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			GGNetworkKit.mInstance.CreateSeceneObject("GlobalInfoManager", new Vector3(-100f, -100f, -100f), Quaternion.identity);
			this.mGlobalInfo = GameObject.FindWithTag("GlobalInfoManager").GetComponent<GGNetworkGlobalInfo>();
			this.mModeInfo = this.mGlobalInfo.modeInfo;
		}
	}

	// Token: 0x0600252C RID: 9516 RVA: 0x00115C78 File Offset: 0x00114078
	public GGNetworkGlobalInfo GetGlobalInfo()
	{
		if (this.mGlobalInfo == null)
		{
			GameObject gameObject = GameObject.FindWithTag("GlobalInfoManager");
			if (gameObject != null)
			{
				this.mGlobalInfo = gameObject.GetComponent<GGNetworkGlobalInfo>();
				this.mModeInfo = this.mGlobalInfo.modeInfo;
			}
		}
		return this.mGlobalInfo;
	}

	// Token: 0x0600252D RID: 9517 RVA: 0x00115CD0 File Offset: 0x001140D0
	public void InitGlobalInfo()
	{
	}

	// Token: 0x0600252E RID: 9518 RVA: 0x00115CD2 File Offset: 0x001140D2
	private void Start()
	{
	}

	// Token: 0x0600252F RID: 9519 RVA: 0x00115CD4 File Offset: 0x001140D4
	public void DoModeLogic(GGNetworkMode mode)
	{
	}

	// Token: 0x06002530 RID: 9520 RVA: 0x00115CD6 File Offset: 0x001140D6
	private void Update()
	{
	}

	// Token: 0x040025F4 RID: 9716
	public static GGNetworkManageGlobalInfo mInstance;

	// Token: 0x040025F5 RID: 9717
	public List<GGNetworkPlayerProperties> blueRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x040025F6 RID: 9718
	public List<GGNetworkPlayerProperties> redRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x040025F7 RID: 9719
	public GGModeInfo mModeInfo;

	// Token: 0x040025F8 RID: 9720
	public GGNetworkGlobalInfo mGlobalInfo;
}
