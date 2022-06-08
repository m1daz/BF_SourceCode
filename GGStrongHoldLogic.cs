using System;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class GGStrongHoldLogic : MonoBehaviour
{
	// Token: 0x06001124 RID: 4388 RVA: 0x0009895F File Offset: 0x00096D5F
	private void Start()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.StrongHold)
		{
			this.isStrongholdMode = true;
			this.StartCountDownStronghold = UIPlayDirector.mInstance.startCountDownMutationObj;
		}
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x00098998 File Offset: 0x00096D98
	private void Update()
	{
		if (this.isStrongholdMode)
		{
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
			if (this.mGlobalInfo == null)
			{
				return;
			}
			this.Stronghold_1.holdState = this.mGlobalInfo.modeInfo.mStronghold1State;
			this.Stronghold_2.holdState = this.mGlobalInfo.modeInfo.mStronghold2State;
			this.Stronghold_3.holdState = this.mGlobalInfo.modeInfo.mStronghold3State;
			this.blueResource = this.mGlobalInfo.modeInfo.mBlueResources;
			this.redResource = this.mGlobalInfo.modeInfo.mRedResources;
			this.Stronghold_1.holdStateCD = this.mGlobalInfo.modeInfo.mStronghold1CD;
			this.Stronghold_2.holdStateCD = this.mGlobalInfo.modeInfo.mStronghold2CD;
			this.Stronghold_3.holdStateCD = this.mGlobalInfo.modeInfo.mStronghold3CD;
			if (this.isShow5sStrongholdTimer)
			{
				this.Show5sStrongholdTimer();
			}
			if (this.mGlobalInfo.modeInfo.isStartStrongholdTimer)
			{
				this.isShow5sStrongholdTimer = true;
			}
			else if (this.isShow5sStrongholdTimer)
			{
				this.isShow5sStrongholdTimer = false;
			}
			if (this.mGlobalInfo.modeInfo.StartStrongholdTimer == 5)
			{
				if (!this.StrongholdDoor.activeSelf)
				{
					this.StrongholdDoor.SetActive(true);
				}
			}
			else if (this.mGlobalInfo.modeInfo.StartStrongholdTimer == 0 && this.StrongholdDoor.activeSelf)
			{
				this.StrongholdDoor.SetActive(false);
			}
		}
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x00098B4C File Offset: 0x00096F4C
	private void Show5sStrongholdTimer()
	{
		if (this.mGlobalInfo.modeInfo.StartStrongholdTimer == 5)
		{
			this.StartCountDownStronghold.SetActive(true);
			this.StartCountDownStronghold.GetComponent<UILabel>().text = "5";
			base.GetComponent<AudioSource>().Play();
		}
		if (this.mGlobalInfo.modeInfo.StartStrongholdTimer == 4)
		{
			this.StartCountDownStronghold.GetComponent<UILabel>().text = "4";
		}
		if (this.mGlobalInfo.modeInfo.StartStrongholdTimer == 3)
		{
			this.StartCountDownStronghold.GetComponent<UILabel>().text = "3";
		}
		if (this.mGlobalInfo.modeInfo.StartStrongholdTimer == 2)
		{
			this.StartCountDownStronghold.GetComponent<UILabel>().text = "2";
		}
		if (this.mGlobalInfo.modeInfo.StartStrongholdTimer == 1)
		{
			this.StartCountDownStronghold.GetComponent<UILabel>().text = "1";
		}
		if (this.mGlobalInfo.modeInfo.StartStrongholdTimer == 0)
		{
			this.StartCountDownStronghold.GetComponent<UILabel>().text = " ";
			this.StartCountDownStronghold.SetActive(false);
			this.isShow5sStrongholdTimer = false;
		}
	}

	// Token: 0x040013AA RID: 5034
	public int blueResource;

	// Token: 0x040013AB RID: 5035
	public int redResource;

	// Token: 0x040013AC RID: 5036
	public GGStrongHold Stronghold_1;

	// Token: 0x040013AD RID: 5037
	public GGStrongHold Stronghold_2;

	// Token: 0x040013AE RID: 5038
	public GGStrongHold Stronghold_3;

	// Token: 0x040013AF RID: 5039
	private bool isStrongholdMode;

	// Token: 0x040013B0 RID: 5040
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x040013B1 RID: 5041
	private GameObject StartCountDownStronghold;

	// Token: 0x040013B2 RID: 5042
	private bool isShow5sStrongholdTimer;

	// Token: 0x040013B3 RID: 5043
	public GameObject StrongholdDoor;
}
