using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EF RID: 751
public class UINewStoreDecoShoePrefabInstiate : MonoBehaviour
{
	// Token: 0x06001724 RID: 5924 RVA: 0x000C43AD File Offset: 0x000C27AD
	private void Start()
	{
		this.BootInit();
	}

	// Token: 0x06001725 RID: 5925 RVA: 0x000C43B5 File Offset: 0x000C27B5
	private void Update()
	{
		if (!this.isbootInstantiated)
		{
			return;
		}
		if (this.curSelectedBootIndex != this.preSelectedBootIndex)
		{
			this.preSelectedBootIndex = this.curSelectedBootIndex;
			this.RefreshBootProperty();
			this.RefreshBootPurchase();
			this.BootShowForCharacter();
		}
	}

	// Token: 0x06001726 RID: 5926 RVA: 0x000C43F4 File Offset: 0x000C27F4
	public void InstantiateBoot()
	{
		if (!this.isbootInstantiated)
		{
			this.BootEntityList = BootManager.mInstance.myAllBootEntityList;
			for (int i = 0; i < this.BootEntityList.Count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bootPrefab, this.bootPrefab.transform.position, this.bootPrefab.transform.rotation);
				gameObject.transform.parent = this.bootObjParent.transform;
				gameObject.transform.localScale = this.bootPrefab.transform.localScale;
				gameObject.transform.localPosition = new Vector3((float)(i - 1) * 200f, 0f, 0f);
				gameObject.transform.rotation = this.bootPrefab.transform.rotation;
				gameObject.transform.Find("shoeModel/shoeTexture").GetComponent<UITexture>().mainTexture = this.bootTexture[i];
				gameObject.GetComponent<UINewStoreDecoShoePrefab>().index = i;
			}
			this.isbootInstantiated = true;
		}
	}

	// Token: 0x06001727 RID: 5927 RVA: 0x000C450C File Offset: 0x000C290C
	public void RefreshBootProperty()
	{
		this.curSelectedbootItemInfo = this.BootEntityList[this.curSelectedBootIndex];
		this.bootNameLabel.text = this.curSelectedbootItemInfo.info.mNameDisplay;
		this.descriptionLabel.text = this.curSelectedbootItemInfo.info.mDescription;
		this.enchantmentPropetyLabel.text = this.curSelectedbootItemInfo.info.mEnchantmentDescription;
	}

	// Token: 0x06001728 RID: 5928 RVA: 0x000C4584 File Offset: 0x000C2984
	public void RefreshBootPurchase()
	{
		this.curSelectedbootItemInfo = this.BootEntityList[this.curSelectedBootIndex];
		this.equipedLabel.SetActive(false);
		if (!this.curSelectedbootItemInfo.info.mIsEnabled)
		{
			if (this.curSelectedbootItemInfo.info.mSellable)
			{
				this.buyButton.SetActive(true);
				this.PurchaseTipLabel.text = string.Empty;
			}
			else
			{
				this.buyButton.SetActive(false);
				this.PurchaseTipLabel.text = this.curSelectedbootItemInfo.info.mPurchaseTipsText;
			}
			this.equipButton.SetActive(false);
			this.unEquipButton.SetActive(false);
		}
		else
		{
			this.buyButton.SetActive(false);
			this.PurchaseTipLabel.text = string.Empty;
			if (this.curSelectedbootItemInfo.isSetted)
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
		this.PriceOffSaleLabel.text = this.BootEntityList[this.curSelectedBootIndex].info.mOffRateDescription;
		this.priceTypeSprite.spriteName = ((this.BootEntityList[this.curSelectedBootIndex].info.mPurchasedType != GItemPurchaseType.CoinsPurchase) ? ((this.BootEntityList[this.curSelectedBootIndex].info.mPurchasedType != GItemPurchaseType.GemPurchase) ? "Null" : "Gem") : "Coin");
		this.priceNumLabel.text = this.BootEntityList[this.curSelectedBootIndex].info.mPrice.ToString();
	}

	// Token: 0x06001729 RID: 5929 RVA: 0x000C4760 File Offset: 0x000C2B60
	public void BootPurchase()
	{
		if (this.BootEntityList[this.curSelectedBootIndex].info.mPurchasedType == GItemPurchaseType.CoinsPurchase)
		{
			if (GrowthManagerKit.GetCoins() >= this.BootEntityList[this.curSelectedBootIndex].info.mPrice)
			{
				GrowthManagerKit.SubCoins(this.BootEntityList[this.curSelectedBootIndex].info.mPrice);
				this.BootEntityList[this.curSelectedBootIndex].info.Enable();
				this.RefreshBootPurchase();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
		else if (this.BootEntityList[this.curSelectedBootIndex].info.mPurchasedType == GItemPurchaseType.GemPurchase)
		{
			if (GrowthManagerKit.GetGems() >= this.BootEntityList[this.curSelectedBootIndex].info.mPrice)
			{
				GrowthManagerKit.SubGems(this.BootEntityList[this.curSelectedBootIndex].info.mPrice);
				this.BootEntityList[this.curSelectedBootIndex].info.Enable();
				this.RefreshBootPurchase();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
	}

	// Token: 0x0600172A RID: 5930 RVA: 0x000C489F File Offset: 0x000C2C9F
	public void BootEquip()
	{
		BootManager.mInstance.SetCurSettedBoot(this.curSelectedBootIndex);
		this.curSettedBootIndex = this.curSelectedBootIndex + 1;
		this.unEquipButton.SetActive(true);
		this.equipButton.SetActive(false);
	}

	// Token: 0x0600172B RID: 5931 RVA: 0x000C48D7 File Offset: 0x000C2CD7
	public void BootUnEquip()
	{
		BootManager.mInstance.UnSetCurSettedBoot(this.curSelectedBootIndex);
		this.curSettedBootIndex = -1;
		this.unEquipButton.SetActive(false);
		this.equipButton.SetActive(true);
	}

	// Token: 0x0600172C RID: 5932 RVA: 0x000C4908 File Offset: 0x000C2D08
	public void BootShowForCharacter()
	{
		if (this.curSelectedBootIndex < this.BootEntityList.Count)
		{
			UINewStoreEquipForCharacter.mInstance.EquipBoot(this.curSelectedBootIndex + 1);
		}
	}

	// Token: 0x0600172D RID: 5933 RVA: 0x000C4934 File Offset: 0x000C2D34
	public void BootInit()
	{
		if (BootManager.mInstance.myCharacterBootEntity == null)
		{
			UINewStoreEquipForCharacter.mInstance.EquipBoot(-1);
			this.curSettedBootIndex = -1;
		}
		else
		{
			UINewStoreEquipForCharacter.mInstance.EquipBoot(BootManager.mInstance.myCharacterBootEntity.detail.modelIndex);
			this.curSettedBootIndex = BootManager.mInstance.myCharacterBootEntity.detail.modelIndex;
		}
	}

	// Token: 0x0600172E RID: 5934 RVA: 0x000C499F File Offset: 0x000C2D9F
	public void BootDefaultEquipSet()
	{
		UINewStoreEquipForCharacter.mInstance.EquipBoot(this.curSettedBootIndex);
	}

	// Token: 0x04001A0A RID: 6666
	private List<BootEntity> BootEntityList;

	// Token: 0x04001A0B RID: 6667
	public int curSelectedBootIndex;

	// Token: 0x04001A0C RID: 6668
	private int preSelectedBootIndex = -1;

	// Token: 0x04001A0D RID: 6669
	public GameObject bootPrefab;

	// Token: 0x04001A0E RID: 6670
	public GameObject bootObjParent;

	// Token: 0x04001A0F RID: 6671
	public Texture[] bootTexture;

	// Token: 0x04001A10 RID: 6672
	public UILabel descriptionLabel;

	// Token: 0x04001A11 RID: 6673
	public UILabel enchantmentPropetyLabel;

	// Token: 0x04001A12 RID: 6674
	public UILabel bootNameLabel;

	// Token: 0x04001A13 RID: 6675
	private BootEntity curSelectedbootItemInfo;

	// Token: 0x04001A14 RID: 6676
	public UISprite priceTypeSprite;

	// Token: 0x04001A15 RID: 6677
	public UILabel priceNumLabel;

	// Token: 0x04001A16 RID: 6678
	public GameObject equipButton;

	// Token: 0x04001A17 RID: 6679
	public GameObject unEquipButton;

	// Token: 0x04001A18 RID: 6680
	public GameObject equipedLabel;

	// Token: 0x04001A19 RID: 6681
	public GameObject buyButton;

	// Token: 0x04001A1A RID: 6682
	public UILabel PriceOffSaleLabel;

	// Token: 0x04001A1B RID: 6683
	public UILabel PurchaseTipLabel;

	// Token: 0x04001A1C RID: 6684
	private bool isbootInstantiated;

	// Token: 0x04001A1D RID: 6685
	public int curSettedBootIndex;
}
