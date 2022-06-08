using System;
using UnityEngine;

// Token: 0x020002CC RID: 716
public class UIHomeDirector : MonoBehaviour
{
	// Token: 0x06001506 RID: 5382 RVA: 0x000B5338 File Offset: 0x000B3738
	private void Awake()
	{
		if (UIHomeDirector.mInstance == null)
		{
			UIHomeDirector.mInstance = this;
		}
		if (GameObject.Find("FirstStartController") == null)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "FirstStartController";
			gameObject.AddComponent<FirstStartController>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x000B538E File Offset: 0x000B378E
	private void OnDestroy()
	{
		if (UIHomeDirector.mInstance != null)
		{
			UIHomeDirector.mInstance = null;
		}
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x000B53A8 File Offset: 0x000B37A8
	private void Start()
	{
		if (UserDataController.IsShowApp())
		{
			UserDataController.SetIsCloseApp();
		}
		else if (UnityEngine.Random.Range(1, 10) < 5)
		{
		}
		this.InitNoticeNode();
		if (UIUserDataController.HasRatedInAppstore())
		{
			this.rateNode.SetActive(false);
		}
		else if (UIUserDataController.GetLoginSuccessCount() > 5 && UnityEngine.Random.Range(1, 11) != 1)
		{
			this.rootNode.SetActive(false);
			this.coinBarNode.SetActive(false);
			this.rateNode.SetActive(true);
		}
		else
		{
			this.rateNode.SetActive(false);
		}
		if (!this.noticeNode.activeSelf && !this.rateNode.activeSelf)
		{
			this.rootNode.SetActive(true);
			this.coinBarNode.SetActive(true);
		}
		this.tipBgSprite.gameObject.SetActive(false);
		this.tipLabel.gameObject.SetActive(false);
		this.tipLabel.text = string.Empty;
		this.InitTopBar();
		if (GGCloudServiceKit.mInstance.mFriendRequestList.Count > 0)
		{
			this.haveNewMsgSprite.gameObject.SetActive(true);
		}
		else
		{
			this.haveNewMsgSprite.gameObject.SetActive(false);
		}
		this.RefreshNewRewardSprite();
		this.RefreshStoreOffsaleSprite();
		this.VideoRecordInit();
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x000B5504 File Offset: 0x000B3904
	private void Update()
	{
		this.UpdateCoinBar();
		this.SysTipTimer();
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x000B5512 File Offset: 0x000B3912
	public void MultiPlayerBtnPressed()
	{
		this.ShowLoadingPanel();
		UISceneManager.mInstance.LoadLevel("UILobby");
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x000B5529 File Offset: 0x000B3929
	public void CasinoBtnPressed()
	{
		this.rootNode.SetActive(false);
		this.casinoNode.SetActive(true);
		UICasinoDirector.mInstance.GetLuckyListData();
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x000B554D File Offset: 0x000B394D
	public void ShareBtnPressed()
	{
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x000B554F File Offset: 0x000B394F
	public void StoreBtnPressed()
	{
		this.ShowLoadingPanel();
		UISceneManager.mInstance.LoadLevel("UINewStore");
	}

	// Token: 0x0600150E RID: 5390 RVA: 0x000B5566 File Offset: 0x000B3966
	public void EulaBtnPressed()
	{
		this.rootNode.SetActive(false);
		this.gdprNode.SetActive(true);
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x000B5580 File Offset: 0x000B3980
	public void EulaBackBtnPressed()
	{
		this.GDPRToggle.value = true;
		this.rootNode.SetActive(true);
		this.gdprNode.SetActive(false);
	}

	// Token: 0x06001510 RID: 5392 RVA: 0x000B55A6 File Offset: 0x000B39A6
	public void SettingBtnPressed()
	{
		this.rootNode.SetActive(false);
		this.settingNode.SetActive(true);
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x000B55C0 File Offset: 0x000B39C0
	public void SettingBackBtnPressed()
	{
		this.rootNode.SetActive(true);
		this.settingNode.SetActive(false);
		UISettingNewDirector.mInstance.BackBtnPressed();
	}

	// Token: 0x06001512 RID: 5394 RVA: 0x000B55E4 File Offset: 0x000B39E4
	public void HelpBtnPressed()
	{
		UISceneManager.mInstance.LoadLevel("UIHelp");
	}

	// Token: 0x06001513 RID: 5395 RVA: 0x000B55F8 File Offset: 0x000B39F8
	public void LeaderboardsBtnPressed()
	{
		this.rootNode.SetActive(false);
		this.leaderboardNode.SetActive(true);
		if (UILeaderboardsDirector.mInstance.needRefresh)
		{
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, null, null, null, null, null);
		}
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x000B5646 File Offset: 0x000B3A46
	public void SharePlatformBtnPressed()
	{
		this.ShowLoadingPanel();
		UISceneManager.mInstance.LoadLevel("UISharePlatform");
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x000B565D File Offset: 0x000B3A5D
	public void RewardBtnPressed()
	{
		this.rootNode.SetActive(false);
		this.rewardNode.SetActive(true);
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x000B5677 File Offset: 0x000B3A77
	public void FestivalBtnPressed()
	{
		this.rootNode.SetActive(false);
		this.festivalNode.SetActive(true);
	}

	// Token: 0x06001517 RID: 5399 RVA: 0x000B5691 File Offset: 0x000B3A91
	public void ProfileBtnPressed()
	{
		this.rootNode.SetActive(false);
		this.coinBarNode.SetActive(false);
		this.profileNode.SetActive(true);
		UIHomeProfileDirector.mInstance.ShowProfileNodeAnimaiton();
		UIHomeProfileDirector.mInstance.RefreshData();
	}

	// Token: 0x06001518 RID: 5400 RVA: 0x000B56CB File Offset: 0x000B3ACB
	public void RechageRewardBtnPressed()
	{
		this.rootNode.SetActive(false);
		this.rechargeRewardNode.SetActive(true);
		UIRechargeRewardDirector.mInstance.RefreshUI();
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x000B56EF File Offset: 0x000B3AEF
	public void FriendBtnPressed()
	{
		this.rootNode.SetActive(false);
		this.coinBarNode.SetActive(false);
		this.friendNode.SetActive(true);
		UIFriendSystemDirector.mInstance.OpenFriendSysBtnPressed();
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x000B571F File Offset: 0x000B3B1F
	public void VideoRecordInit()
	{
		if (!VideoRecordDirector.IsEnabled())
		{
			this.videoRecordButton.SetActive(false);
		}
		else
		{
			this.UpdateRecordFlag();
		}
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x000B5742 File Offset: 0x000B3B42
	public void UpdateRecordFlag()
	{
		if (VideoRecordDirector.isNewVideoExistInList)
		{
			this.newVideoTip.SetActive(true);
		}
		else
		{
			this.newVideoTip.SetActive(false);
		}
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x000B576B File Offset: 0x000B3B6B
	public void VideoRecordBtnPressed()
	{
		VideoRecordDirector.ShowVideoRecordInfo();
		this.UpdateRecordFlag();
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x000B5778 File Offset: 0x000B3B78
	public void LogoutBtnPressed()
	{
		EventDelegate btnEventName = new EventDelegate(this, "LogoutContinueBtnPressed");
		EventDelegate btnEventName2 = new EventDelegate(this, "LogoutCancelBtnPressed");
		UITipController.mInstance.SetTipData(UITipController.TipType.TwoButtonTip, "Do you want to logout?", Color.white, "YES", "NO", btnEventName, btnEventName2, null);
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x000B57BF File Offset: 0x000B3BBF
	public void LogoutContinueBtnPressed()
	{
		GOGPlayerPrefabs.SetInt("IsLastPlayCrashed", 0);
		GOGPlayerPrefabs.SetString("LastPlayCrashedDeviceID", string.Empty);
		ACTUserDataManager.mInstance.mClickLogOut = true;
		ACTUserDataManager.mInstance.UploadDataSwitchScene(true, "UILogin");
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x000B57F6 File Offset: 0x000B3BF6
	public void LogoutCancelBtnPressed()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x000B5802 File Offset: 0x000B3C02
	public void BackToRootNode(GameObject hideObject)
	{
		if (hideObject != null)
		{
			hideObject.SetActive(false);
		}
		this.rootNode.SetActive(true);
		this.coinBarNode.SetActive(true);
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x000B582F File Offset: 0x000B3C2F
	public void RefreshNewRewardSprite()
	{
		if (GrowthManagerKit.IsNewRewardEnabled())
		{
			this.haveNewRewardSprite.gameObject.SetActive(true);
		}
		else
		{
			this.haveNewRewardSprite.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x000B5864 File Offset: 0x000B3C64
	public void RefreshStoreOffsaleSprite()
	{
		if (UIUserDataController.GetOffsaleTimeSpan().TotalSeconds > 0.0 && UIUserDataController.GetOffsaleFactor() > 1f)
		{
			this.haveStoreOffsaleSprite.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x000B58AC File Offset: 0x000B3CAC
	private void InitNoticeNode()
	{
		if (GrowthManagerKit.HasVersionTips(GameVersionController.GameVersion))
		{
			if (this.curNoticeIndex > 0)
			{
				this.noticeNode.SetActive(true);
				this.noticeTexture.mainTexture = (Resources.Load("UI/Images/Notice/Notice_" + this.curNoticeIndex.ToString()) as Texture);
			}
		}
		else
		{
			this.noticeNode.SetActive(false);
		}
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x000B5924 File Offset: 0x000B3D24
	public void NoticeOkBtnPressed()
	{
		this.curNoticeIndex++;
		if (this.curNoticeIndex <= UIHomeDirector.noticeNumber)
		{
			this.noticeTexture.mainTexture = (Resources.Load("UI/Images/Notice/Notice_" + this.curNoticeIndex.ToString()) as Texture);
		}
		else
		{
			if (GameVersionController.GameVersion == "2_1_5")
			{
				this.GetFestiveGiftButton.SetActive(true);
			}
			GrowthManagerKit.VerifyVersionTips(GameVersionController.GameVersion);
			this.noticeNode.SetActive(false);
		}
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x000B59BA File Offset: 0x000B3DBA
	public void GetFestiveGift()
	{
		GrowthManagerKit.AddGems(50);
		this.GetFestiveGiftButton.SetActive(false);
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x000B59D0 File Offset: 0x000B3DD0
	public void NoticeNextBtnPressed()
	{
		this.curNoticeIndex++;
		if (this.curNoticeIndex < UIHomeDirector.noticeNumber - 1)
		{
			this.noticeOkBtn.gameObject.SetActive(false);
			this.noticeNextBtn.gameObject.SetActive(true);
			this.noticeLastBtn.gameObject.SetActive(true);
		}
		else
		{
			this.noticeOkBtn.gameObject.SetActive(true);
			this.noticeLastBtn.gameObject.SetActive(true);
			this.noticeNextBtn.gameObject.SetActive(false);
		}
		this.noticeTexture.mainTexture = (Resources.Load("UI/Images/Notice/Notice_" + (this.curNoticeIndex + 1).ToString()) as Texture);
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x000B5AA0 File Offset: 0x000B3EA0
	public void NoticeLastBtnPressed()
	{
		this.curNoticeIndex--;
		if (this.curNoticeIndex > 0)
		{
			this.noticeOkBtn.gameObject.SetActive(false);
			this.noticeNextBtn.gameObject.SetActive(true);
			this.noticeLastBtn.gameObject.SetActive(true);
		}
		else
		{
			this.noticeOkBtn.gameObject.SetActive(false);
			this.noticeLastBtn.gameObject.SetActive(false);
			this.noticeNextBtn.gameObject.SetActive(true);
		}
		this.noticeTexture.mainTexture = (Resources.Load("UI/Images/Notice/Notice_" + (this.curNoticeIndex + 1).ToString()) as Texture);
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x000B5B68 File Offset: 0x000B3F68
	public void EmailBtnPressed()
	{
		string url = "mailto:riovoxfeedback@gmail.com?subject=FromBlockForceAndroid&body=";
		Application.OpenURL(url);
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x000B5B84 File Offset: 0x000B3F84
	private void InitTopBar()
	{
		int characterLevel = GrowthManagerKit.GetCharacterLevel();
		int characterCurLevelExpExist = GrowthManagerKit.GetCharacterCurLevelExpExist(characterLevel);
		int characterCurLevelUpExpNeed = GrowthManagerKit.GetCharacterCurLevelUpExpNeed(characterLevel);
		this.rankSprite.mainTexture = (Resources.Load("UI/NGUI_SpriteSheets/Rank/Rank_" + characterLevel.ToString()) as Texture);
		this.rankLevelLabel.text = GrowthManagerKit.GetCharacterLevel().ToString();
		this.experienceNumLabel.text = characterCurLevelExpExist.ToString() + "/" + characterCurLevelUpExpNeed.ToString();
		this.exprienceSprite.fillAmount = (float)characterCurLevelExpExist * 100f / (float)characterCurLevelUpExpNeed * 0.01f;
		this.roleNameLabel.text = UIUserDataController.GetDefaultRoleName();
		this.coinNum = GrowthManagerKit.GetCoins();
		this.coinLabel.text = this.coinNum.ToString();
		this.gemNum = GrowthManagerKit.GetGems();
		this.gemLabel.text = this.gemNum.ToString();
		this.giftboxNum = GrowthManagerKit.GetCurGiftBoxTotal();
		this.giftboxLabel.text = this.giftboxNum.ToString();
		this.honorPointNum = GrowthManagerKit.GetHonorPoint();
		this.honorPointLabel.text = this.honorPointNum.ToString();
		if (this.giftboxNum > 0)
		{
			this.giftboxSlotBgSprite.gameObject.SetActive(true);
			this.giftboxSlotLabel.gameObject.SetActive(true);
			if (this.SlotCanGift())
			{
				this.giftboxSlotLabel.text = (this.giftboxNum + 1).ToString();
			}
			else
			{
				this.giftboxSlotLabel.text = this.giftboxNum.ToString();
			}
		}
		else if (this.SlotCanGift())
		{
			this.giftboxSlotBgSprite.gameObject.SetActive(true);
			this.giftboxSlotLabel.gameObject.SetActive(true);
			this.giftboxSlotLabel.text = "1";
		}
		else
		{
			this.giftboxSlotBgSprite.gameObject.SetActive(false);
			this.giftboxSlotLabel.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x000B5DCC File Offset: 0x000B41CC
	public void UpdateGiftTips()
	{
		this.giftboxNum = GrowthManagerKit.GetCurGiftBoxTotal();
		if (this.giftboxNum > 0)
		{
			this.giftboxSlotBgSprite.gameObject.SetActive(true);
			this.giftboxSlotLabel.gameObject.SetActive(true);
			if (this.SlotCanGift())
			{
				this.giftboxSlotLabel.text = (this.giftboxNum + 1).ToString();
			}
			else
			{
				this.giftboxSlotLabel.text = this.giftboxNum.ToString();
			}
		}
		else if (this.SlotCanGift())
		{
			this.giftboxSlotBgSprite.gameObject.SetActive(true);
			this.giftboxSlotLabel.gameObject.SetActive(true);
			this.giftboxSlotLabel.text = "1";
		}
		else
		{
			this.giftboxSlotBgSprite.gameObject.SetActive(false);
			this.giftboxSlotLabel.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x000B5EC8 File Offset: 0x000B42C8
	private bool SlotCanGift()
	{
		string slotFreeADSDateTime = UserDataController.GetSlotFreeADSDateTime();
		DateTime dateTime = new DateTime(int.Parse(slotFreeADSDateTime.Substring(0, 4)), int.Parse(slotFreeADSDateTime.Substring(4, 2)), int.Parse(slotFreeADSDateTime.Substring(6, 2)), int.Parse(slotFreeADSDateTime.Substring(8, 2)), int.Parse(slotFreeADSDateTime.Substring(10, 2)), int.Parse(slotFreeADSDateTime.Substring(12, 2)));
		return dateTime.Date.CompareTo(UnbiasedTime.Instance.Now().Date) != 0;
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x000B5F5C File Offset: 0x000B435C
	public void UpdateCoinBar()
	{
		if (this.casinoNode.activeSelf && !UICasinoDirector.mInstance.casinoCanRefreshCoinbar)
		{
			return;
		}
		if (GrowthManagerKit.NeedRefreshDataDisplay())
		{
			if (this.coinNum != GrowthManagerKit.GetCoins())
			{
				this.coinNum = GrowthManagerKit.GetCoins();
				this.coinLabel.text = this.coinNum.ToString();
			}
			if (this.gemNum != GrowthManagerKit.GetGems())
			{
				this.gemNum = GrowthManagerKit.GetGems();
				this.gemLabel.text = this.gemNum.ToString();
			}
			if (this.giftboxNum != GrowthManagerKit.GetCurGiftBoxTotal() || Time.frameCount % 10 == 0)
			{
				this.giftboxNum = GrowthManagerKit.GetCurGiftBoxTotal();
				this.giftboxLabel.text = this.giftboxNum.ToString();
				if (this.giftboxNum > 0)
				{
					this.giftboxSlotBgSprite.gameObject.SetActive(true);
					this.giftboxSlotLabel.gameObject.SetActive(true);
					if (this.SlotCanGift())
					{
						this.giftboxSlotLabel.text = (this.giftboxNum + 1).ToString();
					}
					else
					{
						this.giftboxSlotLabel.text = this.giftboxNum.ToString();
					}
				}
				else if (this.SlotCanGift())
				{
					this.giftboxSlotBgSprite.gameObject.SetActive(true);
					this.giftboxSlotLabel.gameObject.SetActive(true);
					this.giftboxSlotLabel.text = "1";
				}
				else
				{
					this.giftboxSlotBgSprite.gameObject.SetActive(false);
					this.giftboxSlotLabel.gameObject.SetActive(false);
				}
			}
			if (this.honorPointNum != GrowthManagerKit.GetHonorPoint())
			{
				this.honorPointNum = GrowthManagerKit.GetHonorPoint();
				this.honorPointLabel.text = this.honorPointNum.ToString();
			}
			GrowthManagerKit.SetDataDisplayRefreshFlag(false);
		}
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x000B6168 File Offset: 0x000B4568
	private void PopSysTip()
	{
		string text = GrowthManagerKit.PopSystemMsg();
		if (text == string.Empty)
		{
			this.tipBgSprite.gameObject.SetActive(false);
			this.tipLabel.gameObject.SetActive(false);
		}
		else
		{
			this.tipBgSprite.gameObject.SetActive(true);
			this.tipLabel.gameObject.SetActive(true);
			this.tipLabel.text = "[00EE00]Tips : [-] " + text;
		}
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x000B61EA File Offset: 0x000B45EA
	private void SysTipTimer()
	{
		this.sysTipTime += Time.deltaTime;
		if (this.sysTipTime > 10f)
		{
			this.sysTipTime = 0f;
			this.PopSysTip();
		}
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x000B621F File Offset: 0x000B461F
	public void RatingNowBtnPressed()
	{
		this.rootNode.SetActive(true);
		this.coinBarNode.SetActive(true);
		this.rateNode.SetActive(false);
		UIUserDataController.VerifyAppstoreRating();
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x000B624A File Offset: 0x000B464A
	public void RemindLaterBtnPressed()
	{
		this.rootNode.SetActive(true);
		this.coinBarNode.SetActive(true);
		this.rateNode.SetActive(false);
		UIUserDataController.SetLoginSuccessCount(0);
	}

	// Token: 0x06001531 RID: 5425 RVA: 0x000B6276 File Offset: 0x000B4676
	public void NeverRemindBtnPressed()
	{
		this.rootNode.SetActive(true);
		this.coinBarNode.SetActive(true);
		this.rateNode.SetActive(false);
		UIUserDataController.VerifyAppstoreRating();
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x000B62A1 File Offset: 0x000B46A1
	private void ShowLoadingPanel()
	{
		this.loadingBgTexture.mainTexture = (Resources.Load("UI/Images/General/LoadingBg") as Texture);
		this.loadingNode.SetActive(true);
		this.startGif = true;
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x000B62D0 File Offset: 0x000B46D0
	private void TextureGif()
	{
		if (this.startGif)
		{
			this.gifDeltaTime += Time.deltaTime;
			if (this.gifDeltaTime > 0.2f)
			{
				this.gifDeltaTime = 0f;
				this.textureIndex++;
				if (this.textureIndex > 2)
				{
					this.textureIndex = 1;
				}
				if (this.tex[this.textureIndex - 1] == null)
				{
					this.tex[this.textureIndex - 1] = (Resources.Load("UI/Images/General/LoadingTexture" + this.textureIndex.ToString()) as Texture);
				}
				this.loadingTexture.mainTexture = this.tex[this.textureIndex - 1];
			}
		}
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x000B639E File Offset: 0x000B479E
	public void FacebookBtnPressed()
	{
		Application.OpenURL("https://www.facebook.com/riovoxofficial/");
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x000B63AA File Offset: 0x000B47AA
	public void TwitterBtnPressed()
	{
		Application.OpenURL("https://twitter.com/riovox");
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x000B63B6 File Offset: 0x000B47B6
	public void GDPRToggleValueChanged(UIToggle toggle)
	{
		UIUserDataController.SetGDPRToggleStatus(toggle.value);
		this.gdprCancelConsentBtn.isEnabled = !toggle.value;
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x000B63D7 File Offset: 0x000B47D7
	public void PrivacyBtnPressed()
	{
		Application.OpenURL("http://www.riovox.com/en/privacy/index.html");
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x000B63E3 File Offset: 0x000B47E3
	public void TermsOfUseBtnPressed()
	{
		Application.OpenURL("http://www.riovox.com/en/terms/termsofuse-bf.html");
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x000B63EF File Offset: 0x000B47EF
	public void GDPRCancelConsentBtnPressed()
	{
		UIUserDataController.SetGDPRToggleStatus(false);
		UISceneManager.mInstance.LoadLevel("UILogin");
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x000B6406 File Offset: 0x000B4806
	public void OnProcessPrivacyDataBtnPressed()
	{
		this.processPrivacyDataNode.SetActive(true);
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x000B6414 File Offset: 0x000B4814
	public void OnProcessGameDataBtnPressed()
	{
		this.processPrivacyDataNode.SetActive(false);
		this.processGameDataNode.SetActive(true);
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x000B642E File Offset: 0x000B482E
	public void OnCloseProcessPrivacyDataBtnPressed()
	{
		this.processPrivacyDataNode.SetActive(false);
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x000B643C File Offset: 0x000B483C
	public void OnCloseProcessGameDataBtnPressed()
	{
		this.processGameDataNode.SetActive(false);
		this.processPrivacyDataNode.SetActive(true);
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x000B6456 File Offset: 0x000B4856
	public void OnProcessUnityDataBtnPressed()
	{
		DataPrivacyUnityDirector.Instance.OpenDataPrivacyUrl();
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x000B6462 File Offset: 0x000B4862
	public void OnDeleteGameDataBtnPressed()
	{
		GGCloudServiceKit.mInstance.DeleteAll();
		PlayerPrefs.DeleteAll();
		UISceneManager.mInstance.LoadLevel("UILogin");
	}

	// Token: 0x040017B6 RID: 6070
	public static UIHomeDirector mInstance;

	// Token: 0x040017B7 RID: 6071
	public GameObject coinBarNode;

	// Token: 0x040017B8 RID: 6072
	public GameObject rootNode;

	// Token: 0x040017B9 RID: 6073
	public GameObject rateNode;

	// Token: 0x040017BA RID: 6074
	public GameObject settingNode;

	// Token: 0x040017BB RID: 6075
	public GameObject leaderboardNode;

	// Token: 0x040017BC RID: 6076
	public GameObject friendNode;

	// Token: 0x040017BD RID: 6077
	public GameObject helpNode;

	// Token: 0x040017BE RID: 6078
	public GameObject casinoNode;

	// Token: 0x040017BF RID: 6079
	public GameObject rewardNode;

	// Token: 0x040017C0 RID: 6080
	public GameObject festivalNode;

	// Token: 0x040017C1 RID: 6081
	public GameObject rechargeRewardNode;

	// Token: 0x040017C2 RID: 6082
	public GameObject freeGemsNode;

	// Token: 0x040017C3 RID: 6083
	public GameObject profileNode;

	// Token: 0x040017C4 RID: 6084
	public GameObject[] recommendTexture;

	// Token: 0x040017C5 RID: 6085
	private int recommendIndex;

	// Token: 0x040017C6 RID: 6086
	public UISprite haveNewMsgSprite;

	// Token: 0x040017C7 RID: 6087
	public UISprite haveNewRewardSprite;

	// Token: 0x040017C8 RID: 6088
	public UISprite haveStoreOffsaleSprite;

	// Token: 0x040017C9 RID: 6089
	public AudioSource mainmenuBgAudio;

	// Token: 0x040017CA RID: 6090
	public GameObject videoRecordButton;

	// Token: 0x040017CB RID: 6091
	public GameObject newVideoTip;

	// Token: 0x040017CC RID: 6092
	public GameObject noticeNode;

	// Token: 0x040017CD RID: 6093
	public UIButton noticeOkBtn;

	// Token: 0x040017CE RID: 6094
	public UIButton noticeNextBtn;

	// Token: 0x040017CF RID: 6095
	public UIButton noticeLastBtn;

	// Token: 0x040017D0 RID: 6096
	private static int noticeNumber = 1;

	// Token: 0x040017D1 RID: 6097
	public UITexture noticeTexture;

	// Token: 0x040017D2 RID: 6098
	private int curNoticeIndex;

	// Token: 0x040017D3 RID: 6099
	public GameObject GetFestiveGiftButton;

	// Token: 0x040017D4 RID: 6100
	public Transform FestiveGiftTargetPosition;

	// Token: 0x040017D5 RID: 6101
	public UITexture rankSprite;

	// Token: 0x040017D6 RID: 6102
	public UILabel rankLevelLabel;

	// Token: 0x040017D7 RID: 6103
	public UILabel experienceNumLabel;

	// Token: 0x040017D8 RID: 6104
	public UISprite exprienceSprite;

	// Token: 0x040017D9 RID: 6105
	public UILabel roleNameLabel;

	// Token: 0x040017DA RID: 6106
	public UILabel coinLabel;

	// Token: 0x040017DB RID: 6107
	public UILabel gemLabel;

	// Token: 0x040017DC RID: 6108
	public UILabel giftboxLabel;

	// Token: 0x040017DD RID: 6109
	public UILabel honorPointLabel;

	// Token: 0x040017DE RID: 6110
	public UILabel giftboxSlotLabel;

	// Token: 0x040017DF RID: 6111
	public UISprite giftboxSlotBgSprite;

	// Token: 0x040017E0 RID: 6112
	private int coinNum;

	// Token: 0x040017E1 RID: 6113
	private int gemNum;

	// Token: 0x040017E2 RID: 6114
	private int giftboxNum;

	// Token: 0x040017E3 RID: 6115
	private int honorPointNum;

	// Token: 0x040017E4 RID: 6116
	public UILabel tipLabel;

	// Token: 0x040017E5 RID: 6117
	public UISprite tipBgSprite;

	// Token: 0x040017E6 RID: 6118
	private float sysTipTime = 9f;

	// Token: 0x040017E7 RID: 6119
	public GameObject loadingNode;

	// Token: 0x040017E8 RID: 6120
	public UITexture loadingBgTexture;

	// Token: 0x040017E9 RID: 6121
	public UITexture loadingTexture;

	// Token: 0x040017EA RID: 6122
	private float gifDeltaTime;

	// Token: 0x040017EB RID: 6123
	private int textureIndex = 1;

	// Token: 0x040017EC RID: 6124
	private const int maxNum = 2;

	// Token: 0x040017ED RID: 6125
	private Texture[] tex = new Texture[2];

	// Token: 0x040017EE RID: 6126
	private bool startGif;

	// Token: 0x040017EF RID: 6127
	public UIButton gdprCancelConsentBtn;

	// Token: 0x040017F0 RID: 6128
	public GameObject gdprNode;

	// Token: 0x040017F1 RID: 6129
	public UIToggle GDPRToggle;

	// Token: 0x040017F2 RID: 6130
	public GameObject processPrivacyDataNode;

	// Token: 0x040017F3 RID: 6131
	public GameObject processGameDataNode;
}
