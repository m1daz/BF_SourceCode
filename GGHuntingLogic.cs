using System;
using UnityEngine;

// Token: 0x02000242 RID: 578
public class GGHuntingLogic : MonoBehaviour
{
	// Token: 0x06001053 RID: 4179 RVA: 0x0008BA6C File Offset: 0x00089E6C
	private void Start()
	{
		GGHuntingLogic.mInstance = this;
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting)
		{
			this.isHuntingMode = true;
			this.StartCountDownHunting = UIPlayDirector.mInstance.startCountDownMutationObj;
			this.HuntingModeRoundNum = UIModeDirector.mInstance.explosionCurRoundLabel;
		}
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x0008BAC6 File Offset: 0x00089EC6
	private void OnDestroy()
	{
		GGHuntingLogic.mInstance = null;
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x0008BAD0 File Offset: 0x00089ED0
	private void Update()
	{
		if (this.isHuntingMode)
		{
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
			if (this.mGlobalInfo == null)
			{
				return;
			}
			if (this.isShow5sHuntingTimer)
			{
				this.Show5sHuntingTimer();
			}
			if (this.mGlobalInfo.modeInfo.isStartHuntingTimer)
			{
				this.isShow5sHuntingTimer = true;
			}
			else if (this.isShow5sHuntingTimer)
			{
				this.isShow5sHuntingTimer = false;
			}
		}
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x0008BB50 File Offset: 0x00089F50
	private void Show5sHuntingTimer()
	{
		if (this.mGlobalInfo.modeInfo.StartHuntingTimer == 5)
		{
			this.StartCountDownHunting.SetActive(true);
			this.StartCountDownHunting.GetComponent<UILabel>().text = "5";
			UIModeDirector.mInstance.countdownBgObj.SetActive(true);
			string roundContent = "ROUND " + this.mGlobalInfo.modeInfo.HuntingRoundNum.ToString();
			UIModeDirector.mInstance.ShowExplosionCurRoundLabel(roundContent);
			base.GetComponent<AudioSource>().volume = 1f;
			base.GetComponent<AudioSource>().clip = this.ReadyAudioClip;
			base.GetComponent<AudioSource>().Play();
		}
		if (this.mGlobalInfo.modeInfo.StartHuntingTimer == 4)
		{
			this.StartCountDownHunting.GetComponent<UILabel>().text = "4";
		}
		if (this.mGlobalInfo.modeInfo.StartHuntingTimer == 3)
		{
			UIModeDirector.mInstance.HideExplosionCurRoundLabel();
			this.StartCountDownHunting.GetComponent<UILabel>().text = "3";
		}
		if (this.mGlobalInfo.modeInfo.StartHuntingTimer == 2)
		{
			this.StartCountDownHunting.GetComponent<UILabel>().text = "2";
		}
		if (this.mGlobalInfo.modeInfo.StartHuntingTimer == 1)
		{
			this.StartCountDownHunting.GetComponent<UILabel>().text = "1";
		}
		if (this.mGlobalInfo.modeInfo.StartHuntingTimer == 0)
		{
			UIModeDirector.mInstance.countdownBgObj.SetActive(false);
			this.StartCountDownHunting.GetComponent<UILabel>().text = " ";
			this.StartCountDownHunting.SetActive(false);
			this.isShow5sHuntingTimer = false;
		}
	}

	// Token: 0x0400126D RID: 4717
	public static GGHuntingLogic mInstance;

	// Token: 0x0400126E RID: 4718
	private bool isHuntingMode;

	// Token: 0x0400126F RID: 4719
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x04001270 RID: 4720
	private GameObject StartCountDownHunting;

	// Token: 0x04001271 RID: 4721
	private bool isShow5sHuntingTimer;

	// Token: 0x04001272 RID: 4722
	private UILabel HuntingModeRoundNum;

	// Token: 0x04001273 RID: 4723
	public AudioClip ReadyAudioClip;

	// Token: 0x04001274 RID: 4724
	private int preHuntingTimer = 50;
}
