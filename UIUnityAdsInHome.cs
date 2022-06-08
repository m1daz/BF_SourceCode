using System;
using UnityEngine;
using UnityEngine.Advertisements;

// Token: 0x02000288 RID: 648
public class UIUnityAdsInHome : MonoBehaviour
{
	// Token: 0x06001251 RID: 4689 RVA: 0x000A4865 File Offset: 0x000A2C65
	private void Start()
	{
		this.adsBtn.SetActive(false);
		if (UserDataController.GetEveryDayFreeADSIndex() < 15)
		{
			this.adsActive = true;
		}
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x000A4886 File Offset: 0x000A2C86
	private void Update()
	{
		if (this.adsActive)
		{
			if (Advertisement.IsReady())
			{
				this.adsBtn.SetActive(true);
			}
		}
		else
		{
			this.adsBtn.SetActive(false);
		}
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x000A48BC File Offset: 0x000A2CBC
	public void AdsBtnPressed()
	{
		EventDelegate btnEventName = new EventDelegate(this, "WatchVideo");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonCloseTip, "Get reward by watching video.", Color.white, "Get Now!", null, btnEventName, null, null);
		this.adsBtn.SetActive(false);
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x000A4900 File Offset: 0x000A2D00
	public void WatchVideo()
	{
		if (string.IsNullOrEmpty(this.zoneId))
		{
			this.zoneId = null;
		}
		ShowOptions showOptions = new ShowOptions();
		showOptions.resultCallback = new Action<ShowResult>(this.HandleShowResult);
		Advertisement.Show(this.zoneId, showOptions);
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x000A4952 File Offset: 0x000A2D52
	private void HandleShowResult(ShowResult result)
	{
		if (result != ShowResult.Finished)
		{
			if (result != ShowResult.Skipped)
			{
				if (result == ShowResult.Failed)
				{
					this.VideoPlayFail();
				}
			}
			else
			{
				this.VideoPlayFail();
			}
		}
		else
		{
			this.VideoPlaySucess();
		}
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x000A4990 File Offset: 0x000A2D90
	private void VideoPlaySucess()
	{
		EventDelegate btnEventName = new EventDelegate(this, "HideCurTip");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "Congratulations on getting 1 gem!", Color.white, "OK", string.Empty, btnEventName, null, null);
		GrowthManagerKit.AddGems(1);
		UserDataController.SetEveryDayFreeADSIndex();
		if (UserDataController.GetEveryDayFreeADSIndex() == 15)
		{
			this.adsActive = false;
		}
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x000A49EC File Offset: 0x000A2DEC
	private void VideoPlayFail()
	{
		EventDelegate btnEventName = new EventDelegate(this, "HideCurTip");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "Sorry! It's failed to play a video.", Color.red, "OK", string.Empty, btnEventName, null, null);
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x000A4A27 File Offset: 0x000A2E27
	private void HideCurTip()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x04001518 RID: 5400
	public GameObject adsBtn;

	// Token: 0x04001519 RID: 5401
	private bool adsActive;

	// Token: 0x0400151A RID: 5402
	private string zoneId = "rewardedVideo";
}
