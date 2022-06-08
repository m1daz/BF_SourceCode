using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class UINewStoreToolPotionPrefabInstiate : MonoBehaviour
{
	// Token: 0x060017AF RID: 6063 RVA: 0x000C7D28 File Offset: 0x000C6128
	private void Start()
	{
	}

	// Token: 0x060017B0 RID: 6064 RVA: 0x000C7D2A File Offset: 0x000C612A
	private void Update()
	{
		if (!this.isPotionInstantiated)
		{
			return;
		}
		if (this.curSelectedPotionIndex != this.preSelectedPotionIndex)
		{
			this.preSelectedPotionIndex = this.curSelectedPotionIndex;
			this.RefreshPotionProperty();
			this.RefreshPotionPurchase();
		}
	}

	// Token: 0x060017B1 RID: 6065 RVA: 0x000C7D64 File Offset: 0x000C6164
	public void InstantiatePotion()
	{
		if (!this.isPotionInstantiated)
		{
			this.potionNameList = GrowthManagerKit.GetAllMultiplayerBuffNameList();
			this.potionItemInfo = GrowthManagerKit.GetAllMultiplayerBuffItemInfo();
			int num = this.potionItemInfo.Length;
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.potionItemPrefab, this.potionItemPrefab.transform.position, this.potionItemPrefab.transform.rotation);
				gameObject.transform.parent = this.potionObjParent.transform;
				gameObject.transform.localScale = this.potionItemPrefab.transform.localScale;
				gameObject.transform.localPosition = new Vector3((float)(i - 1) * 200f, 0f, 0f);
				gameObject.transform.rotation = this.potionItemPrefab.transform.rotation;
				gameObject.transform.Find("potionModel/potionTexture").GetComponent<UITexture>().mainTexture = this.potionTexture[i];
				gameObject.GetComponent<UINewStoreToolPotionPrefab>().index = i;
			}
			this.isPotionInstantiated = true;
		}
	}

	// Token: 0x060017B2 RID: 6066 RVA: 0x000C7E80 File Offset: 0x000C6280
	public void RefreshPotionProperty()
	{
		this.curPotionItemInfo = this.potionItemInfo[this.curSelectedPotionIndex];
		this.potionNameLabel.text = this.curPotionItemInfo.mNameDisplay;
		this.potionDesLabel.text = this.curPotionItemInfo.mDescription;
		this.potionRestNumLabel.text = GrowthManagerKit.GetMultiplayerBuffItemInfoByName(this.curPotionItemInfo.mName).mExistNum.ToString();
	}

	// Token: 0x060017B3 RID: 6067 RVA: 0x000C7EF8 File Offset: 0x000C62F8
	public void RefreshPotionPurchase()
	{
		this.curPotionItemInfo = this.potionItemInfo[this.curSelectedPotionIndex];
		this.potionPriceTypeSprite.spriteName = ((this.curPotionItemInfo.mPurchasedType != GItemPurchaseType.CoinsPurchase) ? ((this.curPotionItemInfo.mPurchasedType != GItemPurchaseType.GemPurchase) ? "Null" : "Gem") : "Coin");
		this.potionPriceNumLabel.text = this.curPotionItemInfo.mBindingPrice.ToString();
		this.potionNumLabel.text = "X" + this.curPotionItemInfo.mBindingNum.ToString();
	}

	// Token: 0x060017B4 RID: 6068 RVA: 0x000C7FAC File Offset: 0x000C63AC
	public void PotionPurchase()
	{
		if (this.potionItemInfo[this.curSelectedPotionIndex].mPurchasedType == GItemPurchaseType.CoinsPurchase)
		{
			if (GrowthManagerKit.GetCoins() >= this.potionItemInfo[this.curSelectedPotionIndex].mBindingPrice)
			{
				GrowthManagerKit.SubCoins(this.potionItemInfo[this.curSelectedPotionIndex].mBindingPrice);
				this.potionItemInfo[this.curSelectedPotionIndex].AddBuffNum(this.potionItemInfo[this.curSelectedPotionIndex].mBindingNum);
				this.RefreshPotionProperty();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
		else if (this.potionItemInfo[this.curSelectedPotionIndex].mPurchasedType == GItemPurchaseType.GemPurchase)
		{
			if (GrowthManagerKit.GetGems() >= this.potionItemInfo[this.curSelectedPotionIndex].mBindingPrice)
			{
				GrowthManagerKit.SubGems(this.potionItemInfo[this.curSelectedPotionIndex].mBindingPrice);
				this.potionItemInfo[this.curSelectedPotionIndex].AddBuffNum(this.potionItemInfo[this.curSelectedPotionIndex].mBindingNum);
				this.RefreshPotionProperty();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
	}

	// Token: 0x04001AC8 RID: 6856
	public int curSelectedPotionIndex;

	// Token: 0x04001AC9 RID: 6857
	private int preSelectedPotionIndex = -1;

	// Token: 0x04001ACA RID: 6858
	private string[] potionNameList;

	// Token: 0x04001ACB RID: 6859
	private GMultiplayerBuffItemInfo[] potionItemInfo;

	// Token: 0x04001ACC RID: 6860
	public GameObject potionItemPrefab;

	// Token: 0x04001ACD RID: 6861
	public GameObject potionObjParent;

	// Token: 0x04001ACE RID: 6862
	private List<GameObject> potionItemObjList = new List<GameObject>();

	// Token: 0x04001ACF RID: 6863
	public Texture[] potionTexture;

	// Token: 0x04001AD0 RID: 6864
	public GameObject NewStoreNullItemPrefabEnd;

	// Token: 0x04001AD1 RID: 6865
	public GameObject NewStoreNullItemPrefabStart;

	// Token: 0x04001AD2 RID: 6866
	public UIToggle PotionUIToggle;

	// Token: 0x04001AD3 RID: 6867
	private GMultiplayerBuffItemInfo curPotionItemInfo;

	// Token: 0x04001AD4 RID: 6868
	public UILabel potionNameLabel;

	// Token: 0x04001AD5 RID: 6869
	public UILabel potionDesLabel;

	// Token: 0x04001AD6 RID: 6870
	public UILabel potionRestNumLabel;

	// Token: 0x04001AD7 RID: 6871
	public GameObject potionBuyBtn;

	// Token: 0x04001AD8 RID: 6872
	public UISprite potionPriceTypeSprite;

	// Token: 0x04001AD9 RID: 6873
	public UILabel potionPriceNumLabel;

	// Token: 0x04001ADA RID: 6874
	public UILabel potionOffRateLabel;

	// Token: 0x04001ADB RID: 6875
	public UILabel potionNumLabel;

	// Token: 0x04001ADC RID: 6876
	private bool isPotionInstantiated;
}
