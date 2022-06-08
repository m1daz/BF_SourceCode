using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000290 RID: 656
public class UIGameStoreDirector : MonoBehaviour
{
	// Token: 0x060012B4 RID: 4788 RVA: 0x000A76FD File Offset: 0x000A5AFD
	private void Awake()
	{
		UIGameStoreDirector.mInstance = this;
	}

	// Token: 0x060012B5 RID: 4789 RVA: 0x000A7705 File Offset: 0x000A5B05
	private void OnDestroy()
	{
		if (UIGameStoreDirector.mInstance != null)
		{
			UIGameStoreDirector.mInstance = null;
		}
	}

	// Token: 0x060012B6 RID: 4790 RVA: 0x000A7720 File Offset: 0x000A5B20
	private void Start()
	{
		this.InitIAP();
		this.coinNum = GrowthManagerKit.GetCoins();
		this.gemNum = GrowthManagerKit.GetGems();
		this.giftboxNum = GrowthManagerKit.GetCurGiftBoxTotal();
		this.honorPointNum = GrowthManagerKit.GetHonorPoint();
		this.coinLabel.text = this.coinNum.ToString();
		this.gemLabel.text = this.gemNum.ToString();
		this.giftboxLabel.text = this.giftboxNum.ToString();
		this.honorPointLabel.text = this.honorPointNum.ToString();
	}

	// Token: 0x060012B7 RID: 4791 RVA: 0x000A77CF File Offset: 0x000A5BCF
	private void Update()
	{
		this.UpdateCoinBar();
		this.UpdateOffsaleTime();
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x000A77E0 File Offset: 0x000A5BE0
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

	// Token: 0x060012B9 RID: 4793 RVA: 0x000A7920 File Offset: 0x000A5D20
	private void UpdateCoinBar()
	{
		if (this.storeNode.activeSelf && GrowthManagerKit.NeedRefreshDataDisplay())
		{
			if (this.coinNum != GrowthManagerKit.GetCoins())
			{
				TweenScale.Begin(this.coinLabel.gameObject, 0.15f, new Vector3(1.5f, 1.5f, 1f));
				if (this.coinNum < GrowthManagerKit.GetCoins())
				{
					TweenColor.Begin(this.coinLabel.gameObject, 0.15f, Color.green);
				}
				else if (this.coinNum > GrowthManagerKit.GetCoins())
				{
					TweenColor.Begin(this.coinLabel.gameObject, 0.15f, Color.red);
				}
				base.StartCoroutine(this.TweenFinish(this.coinLabel));
				this.coinNum = GrowthManagerKit.GetCoins();
				this.coinLabel.text = this.coinNum.ToString();
			}
			if (this.gemNum != GrowthManagerKit.GetGems())
			{
				TweenScale.Begin(this.gemLabel.gameObject, 0.15f, new Vector3(1.5f, 1.5f, 1f));
				if (this.gemNum < GrowthManagerKit.GetGems())
				{
					TweenColor.Begin(this.gemLabel.gameObject, 0.15f, Color.green);
				}
				else if (this.gemNum > GrowthManagerKit.GetGems())
				{
					TweenColor.Begin(this.gemLabel.gameObject, 0.15f, Color.red);
				}
				base.StartCoroutine(this.TweenFinish(this.gemLabel));
				this.gemNum = GrowthManagerKit.GetGems();
				this.gemLabel.text = this.gemNum.ToString();
			}
			if (this.giftboxNum != GrowthManagerKit.GetCurGiftBoxTotal())
			{
				TweenScale.Begin(this.giftboxLabel.gameObject, 0.15f, new Vector3(1.5f, 1.5f, 1f));
				if (this.giftboxNum < GrowthManagerKit.GetCurGiftBoxTotal())
				{
					TweenColor.Begin(this.giftboxLabel.gameObject, 0.15f, Color.green);
				}
				else if (this.giftboxNum > GrowthManagerKit.GetCurGiftBoxTotal())
				{
					TweenColor.Begin(this.giftboxLabel.gameObject, 0.15f, Color.red);
				}
				base.StartCoroutine(this.TweenFinish(this.giftboxLabel));
				this.giftboxNum = GrowthManagerKit.GetCurGiftBoxTotal();
				this.giftboxLabel.text = this.giftboxNum.ToString();
			}
			GrowthManagerKit.SetDataDisplayRefreshFlag(false);
		}
	}

	// Token: 0x060012BA RID: 4794 RVA: 0x000A7BBA File Offset: 0x000A5FBA
	public void BackBtnPressed()
	{
		this.storeNode.SetActive(false);
		UIPauseDirector.mInstance.isCutControl = false;
	}

	// Token: 0x060012BB RID: 4795 RVA: 0x000A7BD4 File Offset: 0x000A5FD4
	private IEnumerator TweenFinish(UILabel label)
	{
		yield return new WaitForSeconds(0.2f);
		label.color = Color.white;
		label.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		yield break;
	}

	// Token: 0x060012BC RID: 4796 RVA: 0x000A7BEF File Offset: 0x000A5FEF
	public void ToggleValueChanged()
	{
	}

	// Token: 0x060012BD RID: 4797 RVA: 0x000A7BF4 File Offset: 0x000A5FF4
	private void InitIAP()
	{
		this.purchaseScrollView.ResetPosition();
		this.exchangeScrollView.ResetPosition();
		this.puchaseStatusLabel.gameObject.SetActive(false);
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

	// Token: 0x060012BE RID: 4798 RVA: 0x000A7E98 File Offset: 0x000A6298
	public void PurchaseSuccess()
	{
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, this.swipeTipLabel.gameObject, 2f);
		this.puchaseStatusLabel.text = "PURCHASE SUCCESS !";
		this.puchaseStatusLabel.color = Color.green;
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060012BF RID: 4799 RVA: 0x000A7EF0 File Offset: 0x000A62F0
	public void PurchaseFail()
	{
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, this.swipeTipLabel.gameObject, 2f);
		this.puchaseStatusLabel.text = "PURCHASE FAIL !";
		this.puchaseStatusLabel.color = Color.red;
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060012C0 RID: 4800 RVA: 0x000A7F48 File Offset: 0x000A6348
	public void PurchaseCancel()
	{
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, this.swipeTipLabel.gameObject, 2f);
		this.puchaseStatusLabel.text = "PURCHASE CANCEL !";
		this.puchaseStatusLabel.color = Color.white;
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060012C1 RID: 4801 RVA: 0x000A7FA0 File Offset: 0x000A63A0
	private void ExchangeSuccess()
	{
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, this.swipeTipLabel.gameObject, 2f);
		this.puchaseStatusLabel.text = "EXCHANGE SUCCESS !";
		this.puchaseStatusLabel.color = Color.green;
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060012C2 RID: 4802 RVA: 0x000A7FF8 File Offset: 0x000A63F8
	private void ExchangeFail()
	{
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, this.swipeTipLabel.gameObject, 2f);
		this.puchaseStatusLabel.text = "GEMS LACK !";
		this.puchaseStatusLabel.color = Color.red;
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060012C3 RID: 4803 RVA: 0x000A8050 File Offset: 0x000A6450
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
		this.ShowSubCoinLabel(this.puchaseStatusLabel.gameObject, this.swipeTipLabel.gameObject, 2f);
		this.puchaseStatusLabel.color = Color.red;
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060012C4 RID: 4804 RVA: 0x000A80E8 File Offset: 0x000A64E8
	public void BuyBtnPressed(int index)
	{
		ACTUserDataManager.IsBuyingInGame = true;
		UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Purchase in process...", Color.white, null, null, null, null, null);
		switch (index)
		{
		case 1:
			Purchaser.mInstance.BuyItem("coinspack5000");
			break;
		case 2:
			Purchaser.mInstance.BuyItem("gemspack15");
			break;
		case 3:
			Purchaser.mInstance.BuyItem("gemspack30");
			break;
		case 4:
			Purchaser.mInstance.BuyItem("gemspack80");
			break;
		case 5:
			Purchaser.mInstance.BuyItem("gemspack165");
			break;
		case 6:
			Purchaser.mInstance.BuyItem("gemspack335");
			break;
		case 7:
			Purchaser.mInstance.BuyItem("gemspack850");
			break;
		}
	}

	// Token: 0x060012C5 RID: 4805 RVA: 0x000A81D0 File Offset: 0x000A65D0
	public void ExchangeBtnPressed(int index)
	{
		UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Exchange in process...", Color.white, null, null, null, null, null);
		switch (index)
		{
		case 1:
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
		case 2:
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
		case 3:
			if (GrowthManagerKit.GetCharacterLevel() >= 1)
			{
				if (GrowthManagerKit.GetGems() >= 15)
				{
					GrowthManagerKit.SubGems(15);
					GrowthManagerKit.AddCoins(900);
					GrowthManagerKit.AddGiftBox(4);
					this.ExchangeSuccess();
					this.growthPackBtn[0].isEnabled = false;
					this.growthPackBtn[0].transform.parent.GetComponent<BoxCollider>().enabled = true;
					UIUserDataController.SetGrowthPackStatus(1, 1);
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
		case 4:
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
		case 5:
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
		case 6:
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
		case 7:
			if (GrowthManagerKit.GetCharacterLevel() >= 5)
			{
				if (GrowthManagerKit.GetGems() >= 30)
				{
					GrowthManagerKit.SubGems(30);
					GrowthManagerKit.AddCoins(1800);
					GrowthManagerKit.AddGiftBox(6);
					this.ExchangeSuccess();
					this.growthPackBtn[1].isEnabled = false;
					this.growthPackBtn[1].transform.parent.GetComponent<BoxCollider>().enabled = true;
					UIUserDataController.SetGrowthPackStatus(2, 1);
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
		case 8:
			if (GrowthManagerKit.GetCharacterLevel() >= 10)
			{
				if (GrowthManagerKit.GetGems() >= 45)
				{
					GrowthManagerKit.SubGems(45);
					GrowthManagerKit.AddCoins(3000);
					GrowthManagerKit.AddGiftBox(8);
					this.ExchangeSuccess();
					this.growthPackBtn[2].isEnabled = false;
					this.growthPackBtn[2].transform.parent.GetComponent<BoxCollider>().enabled = true;
					UIUserDataController.SetGrowthPackStatus(3, 1);
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
		}
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x000A84AB File Offset: 0x000A68AB
	private void ShowSubCoinLabel(GameObject coinLabel, GameObject hiddenObject, float time)
	{
		base.StopAllCoroutines();
		if (hiddenObject != null)
		{
			hiddenObject.SetActive(false);
		}
		if (coinLabel != null)
		{
			coinLabel.SetActive(true);
		}
		base.StartCoroutine(this.RecoverShowState(coinLabel, hiddenObject, time));
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x000A84EC File Offset: 0x000A68EC
	private IEnumerator RecoverShowState(GameObject coinLabel, GameObject hiddenObject, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		if (hiddenObject != null)
		{
			hiddenObject.SetActive(true);
		}
		if (coinLabel != null)
		{
			coinLabel.SetActive(false);
		}
		yield break;
	}

	// Token: 0x04001580 RID: 5504
	public static UIGameStoreDirector mInstance;

	// Token: 0x04001581 RID: 5505
	private float timeCount;

	// Token: 0x04001582 RID: 5506
	private UILabel TimeTipLabel;

	// Token: 0x04001583 RID: 5507
	private int coinNum;

	// Token: 0x04001584 RID: 5508
	private int gemNum;

	// Token: 0x04001585 RID: 5509
	private int giftboxNum;

	// Token: 0x04001586 RID: 5510
	private int honorPointNum;

	// Token: 0x04001587 RID: 5511
	public UILabel coinLabel;

	// Token: 0x04001588 RID: 5512
	public UILabel gemLabel;

	// Token: 0x04001589 RID: 5513
	public UILabel giftboxLabel;

	// Token: 0x0400158A RID: 5514
	public UILabel honorPointLabel;

	// Token: 0x0400158B RID: 5515
	public GameObject storeNode;

	// Token: 0x0400158C RID: 5516
	public GameObject coinNode;

	// Token: 0x0400158D RID: 5517
	public UILabel puchaseStatusLabel;

	// Token: 0x0400158E RID: 5518
	public UILabel swipeTipLabel;

	// Token: 0x0400158F RID: 5519
	public UIButton[] growthPackBtn;

	// Token: 0x04001590 RID: 5520
	public UISprite[] growthPackSprite;

	// Token: 0x04001591 RID: 5521
	public UILabel[] growthPackLabel;

	// Token: 0x04001592 RID: 5522
	public UIScrollView purchaseScrollView;

	// Token: 0x04001593 RID: 5523
	public UIScrollView exchangeScrollView;

	// Token: 0x04001594 RID: 5524
	public GameObject Festive_OffsaleTipPanel;

	// Token: 0x04001595 RID: 5525
	public GameObject[] OffsaleTipLabel;
}
