using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002FB RID: 763
public class UINewStoreGemAndCoinPurchaseWindowDirector : MonoBehaviour
{
	// Token: 0x06001780 RID: 6016 RVA: 0x000C6452 File Offset: 0x000C4852
	private void Awake()
	{
		if (UINewStoreGemAndCoinPurchaseWindowDirector.mInstance == null)
		{
			UINewStoreGemAndCoinPurchaseWindowDirector.mInstance = this;
		}
	}

	// Token: 0x06001781 RID: 6017 RVA: 0x000C646A File Offset: 0x000C486A
	private void OnDestroy()
	{
		if (UINewStoreGemAndCoinPurchaseWindowDirector.mInstance != null)
		{
			UINewStoreGemAndCoinPurchaseWindowDirector.mInstance = null;
		}
	}

	// Token: 0x06001782 RID: 6018 RVA: 0x000C6482 File Offset: 0x000C4882
	private void Start()
	{
		this.InitIAP();
	}

	// Token: 0x06001783 RID: 6019 RVA: 0x000C648A File Offset: 0x000C488A
	private void Update()
	{
		this.UpdateOffsaleTime();
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x000C6494 File Offset: 0x000C4894
	private void UpdateOffsaleTime()
	{
		this.timeCount += Time.deltaTime;
		if (this.timeCount > 1f)
		{
			this.timeCount = 0f;
			TimeSpan offsaleTimeSpan = UIUserDataController.GetOffsaleTimeSpan();
			if (offsaleTimeSpan.TotalSeconds > 0.0)
			{
				if (UIUserDataController.GetOffsaleFactor() > 1f)
				{
					this.TimeTipLabel.text = string.Concat(new string[]
					{
						(offsaleTimeSpan.Days * 24 + offsaleTimeSpan.Hours).ToString(),
						"  :  ",
						offsaleTimeSpan.Minutes.ToString(),
						"  :  ",
						offsaleTimeSpan.Seconds.ToString()
					});
				}
			}
			else
			{
				if (this.Festive_OffsaleTipPanel.activeSelf)
				{
					this.Festive_OffsaleTipPanel.SetActive(false);
				}
				for (int i = 0; i < this.OffsaleTipLabel.Length; i++)
				{
					if (this.OffsaleTipLabel[i].activeSelf)
					{
						this.OffsaleTipLabel[i].SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x06001785 RID: 6021 RVA: 0x000C65D4 File Offset: 0x000C49D4
	private void InitIAP()
	{
		if (UIUserDataController.GetGrowthPackStatus(1) == 1)
		{
			this.growthPackSprite[0].enabled = false;
			this.growthPackLabel[0].text = "UNAVAILABLE";
			this.growthPackLabel[0].transform.localPosition = new Vector3(0f, 0f, 0f);
			this.growthPackBtn[0].isEnabled = false;
			this.growthPackBtn[0].transform.parent.GetComponent<BoxCollider>().enabled = true;
		}
		if (UIUserDataController.GetGrowthPackStatus(2) == 1)
		{
			this.growthPackSprite[1].enabled = false;
			this.growthPackLabel[1].text = "UNAVAILABLE";
			this.growthPackLabel[1].transform.localPosition = new Vector3(0f, 0f, 0f);
			this.growthPackBtn[1].isEnabled = false;
			this.growthPackBtn[1].transform.parent.GetComponent<BoxCollider>().enabled = true;
		}
		if (UIUserDataController.GetGrowthPackStatus(3) == 1)
		{
			this.growthPackSprite[2].enabled = false;
			this.growthPackLabel[2].text = "UNAVAILABLE";
			this.growthPackLabel[2].transform.localPosition = new Vector3(0f, 0f, 0f);
			this.growthPackBtn[2].isEnabled = false;
			this.growthPackBtn[2].transform.parent.GetComponent<BoxCollider>().enabled = true;
		}
		TimeSpan offsaleTimeSpan = UIUserDataController.GetOffsaleTimeSpan();
		if (offsaleTimeSpan.TotalSeconds > 0.0 && UIUserDataController.GetOffsaleFactor() > 1f)
		{
			this.Festive_OffsaleTipPanel.SetActive(true);
			for (int i = 0; i < this.OffsaleTipLabel.Length; i++)
			{
				this.OffsaleTipLabel[i].SetActive(true);
			}
			this.TimeTipLabel = this.Festive_OffsaleTipPanel.transform.Find("TimerTip").GetComponent<UILabel>();
			this.TimeTipLabel.text = string.Concat(new string[]
			{
				(offsaleTimeSpan.Days * 24 + offsaleTimeSpan.Hours).ToString(),
				"  :  ",
				offsaleTimeSpan.Minutes.ToString(),
				"  :  ",
				offsaleTimeSpan.Seconds.ToString()
			});
		}
	}

	// Token: 0x06001786 RID: 6022 RVA: 0x000C6850 File Offset: 0x000C4C50
	public void BuyBtnPressed(UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName item)
	{
		switch (item)
		{
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.CoinItem:
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Purchase in process...", Color.white, null, null, null, null, null);
			Purchaser.mInstance.BuyItem("coinspack5000");
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.GemItem1:
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Purchase in process...", Color.white, null, null, null, null, null);
			Purchaser.mInstance.BuyItem("gemspack15");
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.GemItem2:
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Purchase in process...", Color.white, null, null, null, null, null);
			Purchaser.mInstance.BuyItem("gemspack30");
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.GemItem3:
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Purchase in process...", Color.white, null, null, null, null, null);
			Purchaser.mInstance.BuyItem("gemspack80");
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.GemItem4:
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Purchase in process...", Color.white, null, null, null, null, null);
			Purchaser.mInstance.BuyItem("gemspack165");
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.GemItem5:
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Purchase in process...", Color.white, null, null, null, null, null);
			Purchaser.mInstance.BuyItem("gemspack335");
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.GemItem6:
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Purchase in process...", Color.white, null, null, null, null, null);
			Purchaser.mInstance.BuyItem("gemspack850");
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.GrowthPack1:
			if (GrowthManagerKit.GetCharacterLevel() >= 1)
			{
				if (GrowthManagerKit.GetGems() >= 15)
				{
					GrowthManagerKit.SubGems(15);
					GrowthManagerKit.AddCoins(900);
					GrowthManagerKit.AddGiftBox(4);
					this.ExchangeSuccess();
					UIUserDataController.SetGrowthPackStatus(1, 1);
					this.InitIAP();
				}
				else
				{
					this.ExchangeFail();
				}
			}
			else
			{
				this.ExchangeLevelFail(1);
			}
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.GrowthPack2:
			if (GrowthManagerKit.GetCharacterLevel() >= 5)
			{
				if (GrowthManagerKit.GetGems() >= 30)
				{
					GrowthManagerKit.SubGems(30);
					GrowthManagerKit.AddCoins(1800);
					GrowthManagerKit.AddGiftBox(6);
					this.ExchangeSuccess();
					UIUserDataController.SetGrowthPackStatus(2, 1);
					this.InitIAP();
				}
				else
				{
					this.ExchangeFail();
				}
			}
			else
			{
				this.ExchangeLevelFail(2);
			}
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.GrowthPack3:
			if (GrowthManagerKit.GetCharacterLevel() >= 10)
			{
				if (GrowthManagerKit.GetGems() >= 45)
				{
					GrowthManagerKit.SubGems(45);
					GrowthManagerKit.AddCoins(3000);
					GrowthManagerKit.AddGiftBox(8);
					this.ExchangeSuccess();
					UIUserDataController.SetGrowthPackStatus(3, 1);
					this.InitIAP();
				}
				else
				{
					this.ExchangeFail();
				}
			}
			else
			{
				this.ExchangeLevelFail(3);
			}
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.CoinExchange1:
			if (GrowthManagerKit.GetGems() > 0)
			{
				GrowthManagerKit.SubGems(1);
				GrowthManagerKit.AddCoins(60);
				this.ExchangeSuccess();
			}
			else
			{
				this.ExchangeFail();
			}
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.CoinExchange2:
			if (GrowthManagerKit.GetGems() >= 10)
			{
				GrowthManagerKit.SubGems(10);
				GrowthManagerKit.AddCoins(600);
				this.ExchangeSuccess();
			}
			else
			{
				this.ExchangeFail();
			}
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.CoinExchange3:
			if (GrowthManagerKit.GetGems() >= 100)
			{
				GrowthManagerKit.SubGems(100);
				GrowthManagerKit.AddCoins(6000);
				this.ExchangeSuccess();
			}
			else
			{
				this.ExchangeFail();
			}
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.GiftBoxSmall:
			if (GrowthManagerKit.GetGems() >= 15)
			{
				GrowthManagerKit.SubGems(15);
				GrowthManagerKit.AddGiftBox(3);
				this.ExchangeSuccess();
			}
			else
			{
				this.ExchangeFail();
			}
			break;
		case UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName.GiftBoxBig:
			if (GrowthManagerKit.GetGems() >= 45)
			{
				GrowthManagerKit.SubGems(45);
				GrowthManagerKit.AddGiftBox(10);
				this.ExchangeSuccess();
			}
			else
			{
				this.ExchangeFail();
			}
			break;
		}
	}

	// Token: 0x06001787 RID: 6023 RVA: 0x000C6C0C File Offset: 0x000C500C
	public void PurchaseSuccess()
	{
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, null, 2f);
		this.puchaseStatusLabel.text = "PURCHASE SUCCESS !";
		this.puchaseStatusLabel.color = Color.green;
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x06001788 RID: 6024 RVA: 0x000C6C5C File Offset: 0x000C505C
	public void PurchaseFail()
	{
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, null, 2f);
		this.puchaseStatusLabel.text = "PURCHASE FAIL !";
		this.puchaseStatusLabel.color = Color.red;
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x06001789 RID: 6025 RVA: 0x000C6CAC File Offset: 0x000C50AC
	public void PurchaseCancel()
	{
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, null, 2f);
		this.puchaseStatusLabel.text = "PURCHASE CANCEL !";
		this.puchaseStatusLabel.color = Color.white;
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x0600178A RID: 6026 RVA: 0x000C6CFA File Offset: 0x000C50FA
	private void ExchangeSuccess()
	{
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, null, 2f);
		this.puchaseStatusLabel.text = "EXCHANGE SUCCESS !";
		this.puchaseStatusLabel.color = Color.green;
	}

	// Token: 0x0600178B RID: 6027 RVA: 0x000C6D33 File Offset: 0x000C5133
	private void ExchangeFail()
	{
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, null, 2f);
		this.puchaseStatusLabel.text = "GEMS LACK !";
		this.puchaseStatusLabel.color = Color.red;
	}

	// Token: 0x0600178C RID: 6028 RVA: 0x000C6D6C File Offset: 0x000C516C
	private void ExchangeLevelFail(int index)
	{
		if (index == 1)
		{
			this.puchaseStatusLabel.text = "NEED LV.1 !";
		}
		else if (index == 2)
		{
			this.puchaseStatusLabel.text = "NEED LV.5 !";
		}
		else if (index == 3)
		{
			this.puchaseStatusLabel.text = "NEED LV.10 !";
		}
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, null, 2f);
		this.puchaseStatusLabel.color = Color.red;
	}

	// Token: 0x0600178D RID: 6029 RVA: 0x000C6DEF File Offset: 0x000C51EF
	private void ShowSubCoinLabel(GameObject puchaseStatusLabel, GameObject hiddenObject, float time)
	{
		base.StopAllCoroutines();
		if (hiddenObject != null)
		{
			hiddenObject.SetActive(false);
		}
		if (puchaseStatusLabel != null)
		{
			puchaseStatusLabel.SetActive(true);
		}
		base.StartCoroutine(this.RecoverShowState(puchaseStatusLabel, hiddenObject, time));
	}

	// Token: 0x0600178E RID: 6030 RVA: 0x000C6E30 File Offset: 0x000C5230
	private IEnumerator RecoverShowState(GameObject puchaseStatusLabel, GameObject hiddenObject, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		if (hiddenObject != null)
		{
			hiddenObject.SetActive(true);
		}
		if (puchaseStatusLabel != null)
		{
			puchaseStatusLabel.SetActive(false);
		}
		yield break;
	}

	// Token: 0x0600178F RID: 6031 RVA: 0x000C6E5C File Offset: 0x000C525C
	public void PurchaseBtnPressed()
	{
		this.purchaseScrollView.SetActive(true);
		this.exchangeScrollView.SetActive(false);
		if (UIUserDataController.GetOffsaleTimeSpan().TotalSeconds > 0.0 && UIUserDataController.GetOffsaleFactor() > 1f)
		{
			this.Festive_OffsaleTipPanel.SetActive(true);
		}
	}

	// Token: 0x06001790 RID: 6032 RVA: 0x000C6EB7 File Offset: 0x000C52B7
	public void ExchangeBtnPressed()
	{
		this.purchaseScrollView.SetActive(false);
		this.exchangeScrollView.SetActive(true);
		this.Festive_OffsaleTipPanel.SetActive(false);
	}

	// Token: 0x04001A84 RID: 6788
	public static UINewStoreGemAndCoinPurchaseWindowDirector mInstance;

	// Token: 0x04001A85 RID: 6789
	public UILabel puchaseStatusLabel;

	// Token: 0x04001A86 RID: 6790
	public UIButton[] growthPackBtn;

	// Token: 0x04001A87 RID: 6791
	public UISprite[] growthPackSprite;

	// Token: 0x04001A88 RID: 6792
	public UILabel[] growthPackLabel;

	// Token: 0x04001A89 RID: 6793
	public GameObject purchaseScrollView;

	// Token: 0x04001A8A RID: 6794
	public GameObject exchangeScrollView;

	// Token: 0x04001A8B RID: 6795
	public GameObject Festive_OffsaleTipPanel;

	// Token: 0x04001A8C RID: 6796
	public GameObject[] OffsaleTipLabel;

	// Token: 0x04001A8D RID: 6797
	private float timeCount;

	// Token: 0x04001A8E RID: 6798
	private UILabel TimeTipLabel;
}
