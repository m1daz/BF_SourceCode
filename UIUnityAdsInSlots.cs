using System;
using UnityEngine;
using UnityEngine.Advertisements;

// Token: 0x02000289 RID: 649
public class UIUnityAdsInSlots : MonoBehaviour
{
	// Token: 0x0600125A RID: 4698 RVA: 0x000A4A48 File Offset: 0x000A2E48
	private void Start()
	{
		this.adsBtn.SetActive(false);
		string slotFreeADSDateTime = UserDataController.GetSlotFreeADSDateTime();
		DateTime dateTime = new DateTime(int.Parse(slotFreeADSDateTime.Substring(0, 4)), int.Parse(slotFreeADSDateTime.Substring(4, 2)), int.Parse(slotFreeADSDateTime.Substring(6, 2)), int.Parse(slotFreeADSDateTime.Substring(8, 2)), int.Parse(slotFreeADSDateTime.Substring(10, 2)), int.Parse(slotFreeADSDateTime.Substring(12, 2)));
		if (dateTime.Date.CompareTo(UnbiasedTime.Instance.Now().Date) != 0)
		{
			this.adsActive = true;
		}
		else
		{
			this.adsActive = false;
		}
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x000A4AF7 File Offset: 0x000A2EF7
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

	// Token: 0x0600125C RID: 4700 RVA: 0x000A4B2C File Offset: 0x000A2F2C
	public void AdsBtnPressed()
	{
		EventDelegate btnEventName = new EventDelegate(this, "WatchVideo");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonCloseTip, "Spin once free by watching video.", Color.white, "Watch Now", null, btnEventName, null, null);
		this.adsBtn.SetActive(false);
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x000A4B70 File Offset: 0x000A2F70
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

	// Token: 0x0600125E RID: 4702 RVA: 0x000A4BC2 File Offset: 0x000A2FC2
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

	// Token: 0x0600125F RID: 4703 RVA: 0x000A4C00 File Offset: 0x000A3000
	private void VideoPlaySucess()
	{
		EventDelegate eventDelegate = new EventDelegate(this, "HideCurTip");
		UserDataController.SetSlotFreeADSDateTime(DateTime.Now.Date.ToString("yyyyMMddHHmmss"));
		this.adsBtn.SetActive(false);
		this.adsActive = false;
		GrowthManagerKit.AddGiftBox(1);
		UICasinoDirector.mInstance.SpinOnceBtnPressed();
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x000A4C5C File Offset: 0x000A305C
	private void VideoPlayFail()
	{
		EventDelegate btnEventName = new EventDelegate(this, "HideCurTip");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "Sorry! It's failed to play a video.", Color.red, "OK", string.Empty, btnEventName, null, null);
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x000A4C97 File Offset: 0x000A3097
	private void HideCurTip()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x0400151B RID: 5403
	public GameObject adsBtn;

	// Token: 0x0400151C RID: 5404
	private bool adsActive;

	// Token: 0x0400151D RID: 5405
	private string zoneId = "slots";
}
