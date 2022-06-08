using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class UINewStoreDecoCapePrefabInstiate : MonoBehaviour
{
	// Token: 0x06001706 RID: 5894 RVA: 0x000C3355 File Offset: 0x000C1755
	private void Start()
	{
		this.CapeInit();
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x000C335D File Offset: 0x000C175D
	private void Update()
	{
		if (!this.isCapeInstantiated)
		{
			return;
		}
		if (this.curSelectedCapeIndex != this.preSelectedCapeIndex)
		{
			this.preSelectedCapeIndex = this.curSelectedCapeIndex;
			this.RefreshCapeProperty();
			this.RefreshCapePurchase();
			this.CapeShowForCharacter();
		}
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x000C339C File Offset: 0x000C179C
	public void InstantiateCape()
	{
		if (!this.isCapeInstantiated)
		{
			this.capeEntityList = CapeManager.mInstance.myAllCapeEntityList;
			for (int i = 0; i < this.capeEntityList.Count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CapePrefab, this.CapePrefab.transform.position, this.CapePrefab.transform.rotation);
				gameObject.transform.parent = this.CapeObjParent.transform;
				gameObject.transform.localScale = this.CapePrefab.transform.localScale;
				gameObject.transform.localPosition = new Vector3((float)(i - 1) * 200f, 0f, 0f);
				gameObject.transform.rotation = this.CapePrefab.transform.rotation;
				gameObject.transform.Find("capeModel/capeTexture").GetComponent<UITexture>().mainTexture = this.capeTexture[i];
				gameObject.GetComponent<UINewStoreDecoCapePrefab>().index = i;
			}
			this.isCapeInstantiated = true;
		}
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x000C34B4 File Offset: 0x000C18B4
	public void RefreshCapeProperty()
	{
		this.curSelectedCapeItemInfo = this.capeEntityList[this.curSelectedCapeIndex];
		this.capeNameLabel.text = this.curSelectedCapeItemInfo.info.mNameDisplay;
		this.descriptionLabel.text = this.curSelectedCapeItemInfo.info.mDescription;
		this.enchantmentPropetyLabel.text = this.curSelectedCapeItemInfo.info.mEnchantmentDescription;
	}

	// Token: 0x0600170A RID: 5898 RVA: 0x000C352C File Offset: 0x000C192C
	public void RefreshCapePurchase()
	{
		this.curSelectedCapeItemInfo = this.capeEntityList[this.curSelectedCapeIndex];
		this.equipedLabel.SetActive(false);
		if (!this.curSelectedCapeItemInfo.info.mIsEnabled)
		{
			if (this.curSelectedCapeItemInfo.info.mSellable)
			{
				this.buyButton.SetActive(true);
				this.PurchaseTipLabel.text = string.Empty;
			}
			else
			{
				this.buyButton.SetActive(false);
				this.PurchaseTipLabel.text = this.curSelectedCapeItemInfo.info.mPurchaseTipsText;
			}
			this.equipButton.SetActive(false);
			this.unEquipButton.SetActive(false);
		}
		else
		{
			this.buyButton.SetActive(false);
			this.PurchaseTipLabel.text = string.Empty;
			if (this.curSelectedCapeItemInfo.isSetted)
			{
				this.unEquipButton.SetActive(true);
				this.equipButton.SetActive(false);
			}
			else
			{
				this.unEquipButton.SetActive(false);
				this.equipButton.SetActive(true);
			}
		}
		this.PriceOffSaleLabel.text = this.capeEntityList[this.curSelectedCapeIndex].info.mOffRateDescription;
		this.priceTypeSprite.spriteName = ((this.capeEntityList[this.curSelectedCapeIndex].info.mPurchasedType != GItemPurchaseType.CoinsPurchase) ? ((this.capeEntityList[this.curSelectedCapeIndex].info.mPurchasedType != GItemPurchaseType.GemPurchase) ? "Null" : "Gem") : "Coin");
		this.priceNumLabel.text = this.capeEntityList[this.curSelectedCapeIndex].info.mPrice.ToString();
	}

	// Token: 0x0600170B RID: 5899 RVA: 0x000C3708 File Offset: 0x000C1B08
	public void CapePurchase()
	{
		if (this.capeEntityList[this.curSelectedCapeIndex].info.mPurchasedType == GItemPurchaseType.CoinsPurchase)
		{
			if (GrowthManagerKit.GetCoins() >= this.capeEntityList[this.curSelectedCapeIndex].info.mPrice)
			{
				GrowthManagerKit.SubCoins(this.capeEntityList[this.curSelectedCapeIndex].info.mPrice);
				this.capeEntityList[this.curSelectedCapeIndex].info.Enable();
				this.RefreshCapePurchase();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
		else if (this.capeEntityList[this.curSelectedCapeIndex].info.mPurchasedType == GItemPurchaseType.GemPurchase)
		{
			if (GrowthManagerKit.GetGems() >= this.capeEntityList[this.curSelectedCapeIndex].info.mPrice)
			{
				GrowthManagerKit.SubGems(this.capeEntityList[this.curSelectedCapeIndex].info.mPrice);
				this.capeEntityList[this.curSelectedCapeIndex].info.Enable();
				this.RefreshCapePurchase();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x000C3847 File Offset: 0x000C1C47
	public void CapeEquip()
	{
		CapeManager.mInstance.SetCurSettedCape(this.curSelectedCapeIndex);
		this.curSettedCapeIndex = this.curSelectedCapeIndex + 1;
		this.unEquipButton.SetActive(true);
		this.equipButton.SetActive(false);
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x000C387F File Offset: 0x000C1C7F
	public void CapeUnEquip()
	{
		CapeManager.mInstance.UnSetCurSettedCape(this.curSelectedCapeIndex);
		this.curSettedCapeIndex = -1;
		this.unEquipButton.SetActive(false);
		this.equipButton.SetActive(true);
	}

	// Token: 0x0600170E RID: 5902 RVA: 0x000C38B0 File Offset: 0x000C1CB0
	public void CapeShowForCharacter()
	{
		if (this.curSelectedCapeIndex < this.capeEntityList.Count)
		{
			UINewStoreEquipForCharacter.mInstance.EquipCape(this.curSelectedCapeIndex + 1);
		}
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x000C38DC File Offset: 0x000C1CDC
	public void CapeInit()
	{
		if (CapeManager.mInstance.myCharacterCapeEntity == null)
		{
			UINewStoreEquipForCharacter.mInstance.EquipCape(-1);
			this.curSettedCapeIndex = -1;
		}
		else
		{
			UINewStoreEquipForCharacter.mInstance.EquipCape(CapeManager.mInstance.myCharacterCapeEntity.detail.modelIndex);
			this.curSettedCapeIndex = CapeManager.mInstance.myCharacterCapeEntity.detail.modelIndex;
		}
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x000C3947 File Offset: 0x000C1D47
	public void CapeDefaultEquipSet()
	{
		UINewStoreEquipForCharacter.mInstance.EquipCape(this.curSettedCapeIndex);
	}

	// Token: 0x040019D4 RID: 6612
	private List<CapeEntity> capeEntityList;

	// Token: 0x040019D5 RID: 6613
	public int curSelectedCapeIndex;

	// Token: 0x040019D6 RID: 6614
	private int preSelectedCapeIndex = -1;

	// Token: 0x040019D7 RID: 6615
	public GameObject CapePrefab;

	// Token: 0x040019D8 RID: 6616
	public GameObject CapeObjParent;

	// Token: 0x040019D9 RID: 6617
	public Texture[] capeTexture;

	// Token: 0x040019DA RID: 6618
	public GameObject NewStoreNullItemPrefabEnd;

	// Token: 0x040019DB RID: 6619
	public GameObject NewStoreNullItemPrefabStart;

	// Token: 0x040019DC RID: 6620
	public UILabel capeNameLabel;

	// Token: 0x040019DD RID: 6621
	public UILabel descriptionLabel;

	// Token: 0x040019DE RID: 6622
	public UILabel enchantmentPropetyLabel;

	// Token: 0x040019DF RID: 6623
	private CapeEntity curSelectedCapeItemInfo;

	// Token: 0x040019E0 RID: 6624
	public UISprite priceTypeSprite;

	// Token: 0x040019E1 RID: 6625
	public UILabel priceNumLabel;

	// Token: 0x040019E2 RID: 6626
	public GameObject equipButton;

	// Token: 0x040019E3 RID: 6627
	public GameObject unEquipButton;

	// Token: 0x040019E4 RID: 6628
	public GameObject equipedLabel;

	// Token: 0x040019E5 RID: 6629
	public GameObject buyButton;

	// Token: 0x040019E6 RID: 6630
	public UILabel PriceOffSaleLabel;

	// Token: 0x040019E7 RID: 6631
	public UILabel PurchaseTipLabel;

	// Token: 0x040019E8 RID: 6632
	private bool isCapeInstantiated;

	// Token: 0x040019E9 RID: 6633
	public int curSettedCapeIndex;
}
