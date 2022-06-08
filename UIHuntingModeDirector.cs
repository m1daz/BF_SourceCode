using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000291 RID: 657
public class UIHuntingModeDirector : MonoBehaviour
{
	// Token: 0x060012C9 RID: 4809 RVA: 0x000A8700 File Offset: 0x000A6B00
	private void Awake()
	{
		UIHuntingModeDirector.mInstance = this;
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x000A8708 File Offset: 0x000A6B08
	private void OnDestroy()
	{
		if (UIHuntingModeDirector.mInstance != null)
		{
			UIHuntingModeDirector.mInstance = null;
		}
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x000A8720 File Offset: 0x000A6B20
	private void Start()
	{
		this.modeType = GGNetworkKit.mInstance.GetGameMode();
		if (this.modeType == GGModeType.Hunting)
		{
			this.Init();
		}
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x000A8744 File Offset: 0x000A6B44
	private void Update()
	{
		if (this.modeType == GGModeType.Hunting)
		{
			this.RespawnCountdownTimer();
			this.UpdateModeInfoNode();
		}
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x000A8760 File Offset: 0x000A6B60
	private void Init()
	{
		this.InitModeInfoNode();
		for (int i = 0; i < this.starSprite.Length; i++)
		{
			this.starSprite[i].GetComponent<TweenAlpha>().enabled = false;
			this.starSprite[i].GetComponent<TweenScale>().enabled = false;
		}
		for (int j = 0; j < this.lotteryItemBg.Length; j++)
		{
			this.lotteryItemBg[j].GetComponent<TweenRotation>().enabled = false;
			this.lotteryItemBg2[j].GetComponent<TweenAlpha>().enabled = false;
			this.lotteryItemLogo[j].GetComponent<TweenAlpha>().enabled = false;
		}
		for (int k = 0; k < this.lotteryItemBg.Length; k++)
		{
			this.lotteryItemNumLabel[k].text = string.Empty;
		}
		this.respawnTimeLabel.text = string.Empty;
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x000A883F File Offset: 0x000A6C3F
	public void QuitBtnPressed()
	{
		Application.LoadLevel("UILobby");
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x000A884B File Offset: 0x000A6C4B
	private void InitModeInfoNode()
	{
		this.ResetDeadLimitedText(this.curDeadLimitedNum);
		this.modeLogo.spriteName = "LobbyModeLogoHunting";
	}

	// Token: 0x060012D0 RID: 4816 RVA: 0x000A886C File Offset: 0x000A6C6C
	private void UpdateModeInfoNode()
	{
		this.modeInfoTempTime += Time.deltaTime;
		if (this.modeInfoTempTime > 0.5f)
		{
			this.modeInfoTempTime = 0f;
			this.modeInfo = GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().modeInfo;
			this.modeInfoTimeLabel.text = (this.modeInfo.HuntingTimer / 60).ToString() + " : " + ((this.modeInfo.HuntingTimer % 60 >= 10) ? (this.modeInfo.HuntingTimer % 60).ToString() : ("0" + (this.modeInfo.HuntingTimer % 60).ToString()));
			this.bossBloodSprite.fillAmount = this.modeInfo.huntingprocess;
		}
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x000A8963 File Offset: 0x000A6D63
	public void ResetDeadLimitedText(int numArg)
	{
		this.curDeadLimitedNum = numArg;
		this.deadLimitedLabel.text = "Dead Limit : " + this.curDeadLimitedNum.ToString();
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x000A8994 File Offset: 0x000A6D94
	public void PushHuntingResultNode(bool isWin, HuntingRewardInfo rewardInfoArg)
	{
		if (UIGameStoreDirector.mInstance.storeNode.activeSelf)
		{
			UIGameStoreDirector.mInstance.storeNode.SetActive(false);
		}
		this.rewardInfo = rewardInfoArg;
		this.huntingResultNode.SetActive(true);
		this.huntingResultChildNode.GetComponent<TweenScale>().ResetToBeginning();
		this.huntingResultChildNode.GetComponent<TweenScale>().Play();
		if (isWin)
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.clips[0]);
			this.victoryNode.SetActive(true);
			this.defeatedNode.SetActive(false);
			this.generalRewardItemList = this.rewardInfo.generalItems;
			this.lotteryRewardItemList = this.rewardInfo.lotteryItems;
			this.curRestCount = this.rewardInfo.maxValidLotteryCount;
			this.curRestFreeCount = this.rewardInfo.maxFreeLotteryCount;
			this.RefreshGeneralNode();
			if (this.rewardInfo.isLotteryRound)
			{
				this.quitBtn.SetActive(true);
				this.lotteryNode.SetActive(true);
				this.RefreshLotteryNode();
			}
			else
			{
				this.lotteryNode.SetActive(false);
				this.quitBtn.SetActive(false);
			}
		}
		else
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.clips[1]);
			this.quitBtn.SetActive(true);
			this.recommmendedItemList = HuntingRewardInfo.GetRecommendItems();
			this.victoryNode.SetActive(false);
			this.defeatedNode.SetActive(true);
			this.RefreshDefeatedNode();
		}
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x000A8B0C File Offset: 0x000A6F0C
	public void HideHuntingResultNode()
	{
		this.huntingResultNode.SetActive(false);
		for (int i = 0; i < this.lotteryItemBg.Length; i++)
		{
			this.lotteryItemBg[i].GetComponent<TweenRotation>().ResetToBeginning();
			this.lotteryItemBg2[i].GetComponent<TweenAlpha>().ResetToBeginning();
			this.lotteryItemLogo[i].GetComponent<TweenAlpha>().ResetToBeginning();
			this.lotteryItemNumLabel[i].text = string.Empty;
			this.lotteryItemBtn[i].enabled = true;
		}
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x000A8B94 File Offset: 0x000A6F94
	private void RefreshDefeatedNode()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (GameObject item in this.recommmendedItemObjList)
		{
			list.Add(item);
		}
		foreach (GameObject gameObject in list)
		{
			this.recommmendedItemObjList.Remove(gameObject);
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
		for (int i = 0; i < this.recommmendedItemList.Count; i++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.recommmendedItemPrefab);
			gameObject2.transform.parent = this.recommmendedScrollView.transform;
			gameObject2.transform.localPosition = new Vector3(-330f + 190f * (float)i, 0f, 0f);
			gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject2.GetComponentInChildren<UITexture>().mainTexture = (Resources.Load("UI/Images/SlotLogo/" + this.recommmendedItemList[i].spriteName) as Texture);
			this.recommmendedItemObjList.Add(gameObject2);
		}
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x000A8D1C File Offset: 0x000A711C
	private void RefreshGeneralNode()
	{
		int starRating = this.rewardInfo.starRating;
		for (int i = 0; i < this.starSprite.Length; i++)
		{
			if (i < starRating)
			{
				this.starSprite[i].GetComponent<TweenAlpha>().ResetToBeginning();
				this.starSprite[i].GetComponent<TweenAlpha>().Play();
				this.starSprite[i].GetComponent<TweenScale>().ResetToBeginning();
				this.starSprite[i].GetComponent<TweenScale>().Play();
			}
			else
			{
				this.starSprite[i].GetComponent<TweenAlpha>().ResetToBeginning();
				this.starSprite[i].GetComponent<TweenScale>().ResetToBeginning();
			}
		}
		if (this.rewardInfo.isLotteryRound)
		{
			int num = 4;
			for (int j = 0; j < num; j++)
			{
				if (j < this.generalRewardItemList.Count)
				{
					this.generalItemLabel[j].gameObject.SetActive(true);
					this.generalItemLabel[j].text = " + " + this.generalRewardItemList[j].number.ToString();
					this.generalItemLogo[j].mainTexture = (Resources.Load("UI/Images/SlotLogo/" + this.generalRewardItemList[j].spriteName) as Texture);
				}
				else
				{
					this.generalItemLabel[j].gameObject.SetActive(false);
				}
			}
		}
		else
		{
			int num = 3;
			if (this.generalRewardItemList.Count > 0)
			{
				this.generalItemLabel[3].gameObject.SetActive(true);
				this.generalItemLabel[3].text = " + " + this.generalRewardItemList[this.generalRewardItemList.Count - 1].number.ToString();
				this.generalItemLogo[3].mainTexture = (Resources.Load("UI/Images/SlotLogo/" + this.generalRewardItemList[this.generalRewardItemList.Count - 1].spriteName) as Texture);
			}
			for (int k = 0; k < num; k++)
			{
				if (k < this.generalRewardItemList.Count - 1)
				{
					this.generalItemLabel[k].gameObject.SetActive(true);
					this.generalItemLabel[k].text = " + " + this.generalRewardItemList[k].number.ToString();
					this.generalItemLogo[k].mainTexture = (Resources.Load("UI/Images/SlotLogo/" + this.generalRewardItemList[k].spriteName) as Texture);
				}
				else
				{
					this.generalItemLabel[k].gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060012D6 RID: 4822 RVA: 0x000A9010 File Offset: 0x000A7410
	private void RefreshLotteryNode()
	{
		this.lotteryTimesLabel.text = "Lottery Times : " + this.rewardInfo.maxValidLotteryCount.ToString();
		this.lotteryFreeTimesLabel.text = "Free Times : " + this.rewardInfo.maxFreeLotteryCount.ToString();
		this.lotteryPriceNumLabel.text = " X " + this.rewardInfo.paidLotteryPrice.ToString();
		if (this.rewardInfo.paidLotteryPurchaseType == GItemPurchaseType.CoinsPurchase)
		{
			this.lotteryPriceLogo.spriteName = "Coin";
		}
		else if (this.rewardInfo.paidLotteryPurchaseType == GItemPurchaseType.GemPurchase)
		{
			this.lotteryPriceLogo.spriteName = "Gem";
		}
		else if (this.rewardInfo.paidLotteryPurchaseType == GItemPurchaseType.HonorPointPurchase)
		{
			this.lotteryPriceLogo.spriteName = "HonorPointLogo";
		}
		for (int i = 0; i < this.lotteryItemLogo.Length; i++)
		{
			this.lotteryItemBtn[i].enabled = true;
			this.lotteryItemNumLabel[i].text = string.Empty;
			this.lotteryItemBg[i].GetComponent<TweenRotation>().ResetToBeginning();
			this.lotteryItemBg2[i].GetComponent<TweenAlpha>().ResetToBeginning();
			this.lotteryItemLogo[i].GetComponent<TweenAlpha>().ResetToBeginning();
		}
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x000A9184 File Offset: 0x000A7584
	public void LotteryBtnPressed(int indexArg)
	{
		int num = indexArg - 1;
		if (this.curRestCount > 0)
		{
			int num2 = 0;
			string str = string.Empty;
			if (this.rewardInfo.paidLotteryPurchaseType == GItemPurchaseType.CoinsPurchase)
			{
				num2 = GrowthManagerKit.GetCoins();
				str = "Coins";
			}
			else if (this.rewardInfo.paidLotteryPurchaseType == GItemPurchaseType.GemPurchase)
			{
				num2 = GrowthManagerKit.GetGems();
				str = "Gems";
			}
			else if (this.rewardInfo.paidLotteryPurchaseType == GItemPurchaseType.HonorPointPurchase)
			{
				num2 = GrowthManagerKit.GetHonorPoint();
				str = "Honor Point";
			}
			if (this.curRestFreeCount > 0)
			{
				this.lotteryItemLogo[num].mainTexture = (Resources.Load("UI/Images/SlotLogo/" + this.lotteryRewardItemList[this.curLotteryIndex].spriteName) as Texture);
				this.rewardInfo.SteppedReceiveLotteryReward();
				if (this.curRestFreeCount > 0)
				{
					this.curRestFreeCount--;
				}
				this.curRestCount--;
				this.lotteryTimesLabel.text = "Lottery Times : " + this.curRestCount.ToString();
				this.lotteryFreeTimesLabel.text = "Free Times : " + this.curRestFreeCount.ToString();
				this.lotteryItemBg[num].GetComponent<TweenRotation>().ResetToBeginning();
				this.lotteryItemBg[num].GetComponent<TweenRotation>().Play();
				this.lotteryItemBg2[num].GetComponent<TweenAlpha>().ResetToBeginning();
				this.lotteryItemBg2[num].GetComponent<TweenAlpha>().Play();
				this.lotteryItemLogo[num].GetComponent<TweenAlpha>().ResetToBeginning();
				this.lotteryItemLogo[num].GetComponent<TweenAlpha>().Play();
				this.lotteryItemNumLabel[num].text = this.lotteryRewardItemList[this.curLotteryIndex].number.ToString();
				this.lotteryItemBtn[num].enabled = false;
				this.curLotteryIndex++;
			}
			else if (num2 < this.rewardInfo.paidLotteryPrice)
			{
				this.lotteryLackTipLabel.text = str + " Lack!";
				this.ShowSubCoinLabel(this.lotteryLackTipLabel.gameObject, null, 1f);
			}
			else
			{
				this.lotteryItemLogo[num].mainTexture = (Resources.Load("UI/Images/SlotLogo/" + this.lotteryRewardItemList[this.curLotteryIndex].spriteName) as Texture);
				this.rewardInfo.SteppedReceiveLotteryReward();
				this.curRestCount--;
				this.lotteryTimesLabel.text = "Lottery Times : " + this.curRestCount.ToString();
				this.lotteryFreeTimesLabel.text = "Free Times : " + this.curRestFreeCount.ToString();
				this.lotteryItemBg[num].GetComponent<TweenRotation>().ResetToBeginning();
				this.lotteryItemBg[num].GetComponent<TweenRotation>().Play();
				this.lotteryItemBg2[num].GetComponent<TweenAlpha>().ResetToBeginning();
				this.lotteryItemBg2[num].GetComponent<TweenAlpha>().Play();
				this.lotteryItemLogo[num].GetComponent<TweenAlpha>().ResetToBeginning();
				this.lotteryItemLogo[num].GetComponent<TweenAlpha>().Play();
				this.lotteryItemNumLabel[num].text = this.lotteryRewardItemList[this.curLotteryIndex].number.ToString();
				this.lotteryItemBtn[num].enabled = false;
				this.curLotteryIndex++;
			}
		}
		else
		{
			this.lotteryLackTipLabel.text = "Lottery Times Lack!";
			this.ShowSubCoinLabel(this.lotteryLackTipLabel.gameObject, null, 1f);
		}
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x060012D8 RID: 4824 RVA: 0x000A9557 File Offset: 0x000A7957
	// (set) Token: 0x060012D9 RID: 4825 RVA: 0x000A955F File Offset: 0x000A795F
	public int respawnPrice
	{
		get
		{
			return this._respawnPrice;
		}
		set
		{
			GameProtecter.mInstance.SetEncryptVariable(ref this._respawnPrice, ref this.StrEncryptrespawnPrice, value);
		}
	}

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x060012DA RID: 4826 RVA: 0x000A9578 File Offset: 0x000A7978
	// (remove) Token: 0x060012DB RID: 4827 RVA: 0x000A95AC File Offset: 0x000A79AC
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHuntingModeDirector.UIRespawnEventHandler OnUIRespawn;

	// Token: 0x060012DC RID: 4828 RVA: 0x000A95E0 File Offset: 0x000A79E0
	public void GenUIRespawnEvent()
	{
		if (UIHuntingModeDirector.OnUIRespawn != null)
		{
			UIHuntingModeDirector.OnUIRespawn();
		}
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x000A95F8 File Offset: 0x000A79F8
	public void ShowRespawnNode(int time, int price)
	{
		this.respawnNode.SetActive(true);
		if (price > 0)
		{
			this.respawnPrice = price;
			this.respawnPriceLabel.text = " X " + this.respawnPrice.ToString();
		}
		this.respawnTimeCycle = time;
		this.tempRespawnTimeCycle = time;
		this.isNeedCountdown = true;
		this.tempTime = 1f;
		if (this.curDeadLimitedNum > 0)
		{
			this.curDeadLimitedNum--;
			this.deadLimitedLabel.text = "Dead Limit : " + this.curDeadLimitedNum.ToString();
		}
	}

	// Token: 0x060012DE RID: 4830 RVA: 0x000A96A8 File Offset: 0x000A7AA8
	public void RespawnBtnPressed()
	{
		if (GrowthManagerKit.GetGems() >= this.respawnPrice)
		{
			GrowthManagerKit.SubGems(this.respawnPrice);
			this.GenUIRespawnEvent();
			this.OnRespawnEvent();
		}
		else
		{
			this.ShowSubCoinLabel(this.respawnTipLabel.gameObject, null, 1f);
		}
	}

	// Token: 0x060012DF RID: 4831 RVA: 0x000A96F9 File Offset: 0x000A7AF9
	private void OnRespawnEvent()
	{
		this.isNeedCountdown = false;
		this.respawnNode.SetActive(false);
		this.respawnTimeLabel.text = string.Empty;
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x000A9720 File Offset: 0x000A7B20
	private void RespawnCountdownTimer()
	{
		if (this.isNeedCountdown)
		{
			this.tempTime += Time.deltaTime;
			if (this.tempTime > 1f)
			{
				this.tempRespawnTimeCycle--;
				this.tempTime = 0f;
				if (this.tempRespawnTimeCycle < 1)
				{
					this.OnRespawnEvent();
				}
				else
				{
					this.respawnTimeLabel.text = this.tempRespawnTimeCycle.ToString();
				}
			}
		}
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x000A97A8 File Offset: 0x000A7BA8
	public void PopDamageTip(int damageNum)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.damageTipPrefab);
		gameObject.transform.parent = this.middleNode.transform;
		float x = 1f * (float)UnityEngine.Random.Range(-100, 100);
		float y = 1f * (float)UnityEngine.Random.Range(160, 260);
		gameObject.transform.localPosition = new Vector3(x, y, 0f);
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		int fontSize = UnityEngine.Random.Range(16, 32);
		gameObject.GetComponent<UILabel>().fontSize = fontSize;
		gameObject.GetComponent<UILabel>().text = "- " + damageNum.ToString();
		TweenAlpha.Begin(gameObject, 1f, 0f, 0f);
		TweenPosition.Begin(gameObject, 1f, gameObject.transform.localPosition + new Vector3(0f, 100f, 0f));
		TweenScale.Begin(gameObject, 1f, new Vector3(2f, 2f, 1f));
		UnityEngine.Object.Destroy(gameObject, 2f);
	}

	// Token: 0x060012E2 RID: 4834 RVA: 0x000A98DE File Offset: 0x000A7CDE
	private void ShowSubCoinLabel(GameObject coinLabel, GameObject hiddenObject, float time)
	{
		if (hiddenObject != null)
		{
			hiddenObject.SetActive(false);
		}
		coinLabel.SetActive(true);
		base.StartCoroutine(this.RecoverShowState(coinLabel, hiddenObject, time));
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x000A990C File Offset: 0x000A7D0C
	private IEnumerator RecoverShowState(GameObject coinLabel, GameObject hiddenObject, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		if (hiddenObject != null)
		{
			hiddenObject.SetActive(true);
		}
		coinLabel.SetActive(false);
		yield break;
	}

	// Token: 0x04001596 RID: 5526
	public static UIHuntingModeDirector mInstance;

	// Token: 0x04001597 RID: 5527
	private GGModeType modeType = GGModeType.Other;

	// Token: 0x04001598 RID: 5528
	public AudioClip[] clips;

	// Token: 0x04001599 RID: 5529
	public UISprite bossBloodSprite;

	// Token: 0x0400159A RID: 5530
	public UISprite modeLogo;

	// Token: 0x0400159B RID: 5531
	public UILabel modeInfoTimeLabel;

	// Token: 0x0400159C RID: 5532
	public UILabel deadLimitedLabel;

	// Token: 0x0400159D RID: 5533
	private GGModeInfo modeInfo;

	// Token: 0x0400159E RID: 5534
	private float modeInfoTempTime;

	// Token: 0x0400159F RID: 5535
	private int curDeadLimitedNum = 6;

	// Token: 0x040015A0 RID: 5536
	public GameObject huntingResultNode;

	// Token: 0x040015A1 RID: 5537
	public GameObject huntingResultChildNode;

	// Token: 0x040015A2 RID: 5538
	public GameObject victoryNode;

	// Token: 0x040015A3 RID: 5539
	public GameObject defeatedNode;

	// Token: 0x040015A4 RID: 5540
	public GameObject quitBtn;

	// Token: 0x040015A5 RID: 5541
	private HuntingRewardInfo rewardInfo;

	// Token: 0x040015A6 RID: 5542
	private List<HuntingRewardInfo.SingleReward> generalRewardItemList = new List<HuntingRewardInfo.SingleReward>();

	// Token: 0x040015A7 RID: 5543
	private List<HuntingRewardInfo.SingleReward> lotteryRewardItemList = new List<HuntingRewardInfo.SingleReward>();

	// Token: 0x040015A8 RID: 5544
	public UIScrollView recommmendedScrollView;

	// Token: 0x040015A9 RID: 5545
	public GameObject recommmendedItemPrefab;

	// Token: 0x040015AA RID: 5546
	private List<HuntingRewardInfo.RecommendItem> recommmendedItemList = new List<HuntingRewardInfo.RecommendItem>();

	// Token: 0x040015AB RID: 5547
	private List<GameObject> recommmendedItemObjList = new List<GameObject>();

	// Token: 0x040015AC RID: 5548
	public UISprite[] starSprite;

	// Token: 0x040015AD RID: 5549
	public UITexture[] generalItemLogo;

	// Token: 0x040015AE RID: 5550
	public UILabel[] generalItemLabel;

	// Token: 0x040015AF RID: 5551
	public GameObject lotteryNode;

	// Token: 0x040015B0 RID: 5552
	public UILabel lotteryLackTipLabel;

	// Token: 0x040015B1 RID: 5553
	public UIButton[] lotteryItemBtn;

	// Token: 0x040015B2 RID: 5554
	public UISprite[] lotteryItemBg;

	// Token: 0x040015B3 RID: 5555
	public UISprite[] lotteryItemBg2;

	// Token: 0x040015B4 RID: 5556
	public UITexture[] lotteryItemLogo;

	// Token: 0x040015B5 RID: 5557
	public UILabel[] lotteryItemNumLabel;

	// Token: 0x040015B6 RID: 5558
	public UILabel lotteryTimesLabel;

	// Token: 0x040015B7 RID: 5559
	public UILabel lotteryFreeTimesLabel;

	// Token: 0x040015B8 RID: 5560
	public UILabel lotteryPriceNumLabel;

	// Token: 0x040015B9 RID: 5561
	public UISprite lotteryPriceLogo;

	// Token: 0x040015BA RID: 5562
	private int curRestCount;

	// Token: 0x040015BB RID: 5563
	private int curRestFreeCount;

	// Token: 0x040015BC RID: 5564
	private int curLotteryIndex;

	// Token: 0x040015BD RID: 5565
	public GameObject respawnNode;

	// Token: 0x040015BE RID: 5566
	public UILabel respawnTimeLabel;

	// Token: 0x040015BF RID: 5567
	public UILabel respawnPriceLabel;

	// Token: 0x040015C0 RID: 5568
	public UILabel respawnTipLabel;

	// Token: 0x040015C1 RID: 5569
	private int _respawnPrice;

	// Token: 0x040015C2 RID: 5570
	public string StrEncryptrespawnPrice = string.Empty;

	// Token: 0x040015C3 RID: 5571
	private int respawnTimeCycle;

	// Token: 0x040015C4 RID: 5572
	private bool isNeedCountdown;

	// Token: 0x040015C5 RID: 5573
	private float tempTime;

	// Token: 0x040015C6 RID: 5574
	private int tempRespawnTimeCycle;

	// Token: 0x040015C8 RID: 5576
	public GameObject damageTipPrefab;

	// Token: 0x040015C9 RID: 5577
	public GameObject middleNode;

	// Token: 0x02000292 RID: 658
	// (Invoke) Token: 0x060012E5 RID: 4837
	public delegate void UIRespawnEventHandler();
}
