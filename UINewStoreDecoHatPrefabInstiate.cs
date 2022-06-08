using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002ED RID: 749
public class UINewStoreDecoHatPrefabInstiate : MonoBehaviour
{
	// Token: 0x06001715 RID: 5909 RVA: 0x000C3B81 File Offset: 0x000C1F81
	private void Start()
	{
		this.HatInit();
	}

	// Token: 0x06001716 RID: 5910 RVA: 0x000C3B89 File Offset: 0x000C1F89
	private void Update()
	{
		if (!this.isHatInstantiated)
		{
			return;
		}
		if (this.curSelectedHatIndex != this.preSelectedHatIndex)
		{
			this.preSelectedHatIndex = this.curSelectedHatIndex;
			this.RefreshHatProperty();
			this.RefreshHatPurchase();
			this.HatShowForCharacter();
		}
	}

	// Token: 0x06001717 RID: 5911 RVA: 0x000C3BC8 File Offset: 0x000C1FC8
	public void InstantiateHat()
	{
		if (!this.isHatInstantiated)
		{
			this.hatEntityList = HatManager.mInstance.myAllHatEntityList;
			for (int i = 0; i < this.hatEntityList.Count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.HatPrefab, this.HatPrefab.transform.position, this.HatPrefab.transform.rotation);
				gameObject.transform.parent = this.HatObjParent.transform;
				gameObject.transform.localScale = this.HatPrefab.transform.localScale;
				gameObject.transform.localPosition = new Vector3((float)(i - 1) * 200f, 0f, 0f);
				gameObject.transform.rotation = this.HatPrefab.transform.rotation;
				gameObject.transform.Find("hatModel/hatTexture").GetComponent<UITexture>().mainTexture = this.hatTexture[i];
				gameObject.GetComponent<UINewStoreDecoHatPrefab>().index = i;
			}
			this.isHatInstantiated = true;
		}
	}

	// Token: 0x06001718 RID: 5912 RVA: 0x000C3CE0 File Offset: 0x000C20E0
	public void RefreshHatProperty()
	{
		this.curSelectedHatItemInfo = this.hatEntityList[this.curSelectedHatIndex];
		this.hatNameLabel.text = this.curSelectedHatItemInfo.info.mNameDisplay;
		this.descriptionLabel.text = this.curSelectedHatItemInfo.info.mDescription;
		this.enchantmentPropetyLabel.text = this.curSelectedHatItemInfo.info.mEnchantmentDescription;
	}

	// Token: 0x06001719 RID: 5913 RVA: 0x000C3D58 File Offset: 0x000C2158
	public void RefreshHatPurchase()
	{
		this.curSelectedHatItemInfo = this.hatEntityList[this.curSelectedHatIndex];
		this.equipedLabel.SetActive(false);
		if (!this.curSelectedHatItemInfo.info.mIsEnabled)
		{
			if (this.curSelectedHatItemInfo.info.mSellable)
			{
				this.buyButton.SetActive(true);
				this.PurchaseTipLabel.text = string.Empty;
			}
			else
			{
				this.buyButton.SetActive(false);
				this.PurchaseTipLabel.text = this.curSelectedHatItemInfo.info.mPurchaseTipsText;
			}
			this.equipButton.SetActive(false);
			this.unEquipButton.SetActive(false);
		}
		else
		{
			this.buyButton.SetActive(false);
			this.PurchaseTipLabel.text = string.Empty;
			if (this.curSelectedHatItemInfo.isSetted)
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
		this.PriceOffSaleLabel.text = this.hatEntityList[this.curSelectedHatIndex].info.mOffRateDescription;
		this.priceTypeSprite.spriteName = ((this.hatEntityList[this.curSelectedHatIndex].info.mPurchasedType != GItemPurchaseType.CoinsPurchase) ? ((this.hatEntityList[this.curSelectedHatIndex].info.mPurchasedType != GItemPurchaseType.GemPurchase) ? "Null" : "Gem") : "Coin");
		this.priceNumLabel.text = this.hatEntityList[this.curSelectedHatIndex].info.mPrice.ToString();
	}

	// Token: 0x0600171A RID: 5914 RVA: 0x000C3F34 File Offset: 0x000C2334
	public void HatPurchase()
	{
		if (this.hatEntityList[this.curSelectedHatIndex].info.mPurchasedType == GItemPurchaseType.CoinsPurchase)
		{
			if (GrowthManagerKit.GetCoins() >= this.hatEntityList[this.curSelectedHatIndex].info.mPrice)
			{
				GrowthManagerKit.SubCoins(this.hatEntityList[this.curSelectedHatIndex].info.mPrice);
				this.hatEntityList[this.curSelectedHatIndex].info.Enable();
				this.RefreshHatPurchase();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
		else if (this.hatEntityList[this.curSelectedHatIndex].info.mPurchasedType == GItemPurchaseType.GemPurchase)
		{
			if (GrowthManagerKit.GetGems() >= this.hatEntityList[this.curSelectedHatIndex].info.mPrice)
			{
				GrowthManagerKit.SubGems(this.hatEntityList[this.curSelectedHatIndex].info.mPrice);
				this.hatEntityList[this.curSelectedHatIndex].info.Enable();
				this.RefreshHatPurchase();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
	}

	// Token: 0x0600171B RID: 5915 RVA: 0x000C4073 File Offset: 0x000C2473
	public void HatEquip()
	{
		HatManager.mInstance.SetCurSettedHat(this.curSelectedHatIndex);
		this.curSettedHatIndex = this.curSelectedHatIndex + 1;
		this.unEquipButton.SetActive(true);
		this.equipButton.SetActive(false);
	}

	// Token: 0x0600171C RID: 5916 RVA: 0x000C40AB File Offset: 0x000C24AB
	public void HatUnEquip()
	{
		HatManager.mInstance.UnSetCurSettedHat(this.curSelectedHatIndex);
		this.curSettedHatIndex = -1;
		this.unEquipButton.SetActive(false);
		this.equipButton.SetActive(true);
	}

	// Token: 0x0600171D RID: 5917 RVA: 0x000C40DC File Offset: 0x000C24DC
	public void HatShowForCharacter()
	{
		if (this.curSelectedHatIndex < this.hatEntityList.Count)
		{
			UINewStoreEquipForCharacter.mInstance.EquipHat(this.curSelectedHatIndex + 1);
		}
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x000C4108 File Offset: 0x000C2508
	public void HatInit()
	{
		if (HatManager.mInstance.myCharacterHatEntity == null)
		{
			UINewStoreEquipForCharacter.mInstance.EquipHat(-1);
			this.curSettedHatIndex = -1;
		}
		else
		{
			UINewStoreEquipForCharacter.mInstance.EquipHat(HatManager.mInstance.myCharacterHatEntity.detail.modelIndex);
			this.curSettedHatIndex = HatManager.mInstance.myCharacterHatEntity.detail.modelIndex;
		}
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x000C4173 File Offset: 0x000C2573
	public void HatDefaultEquipSet()
	{
		UINewStoreEquipForCharacter.mInstance.EquipHat(this.curSettedHatIndex);
	}

	// Token: 0x040019EF RID: 6639
	private List<HatEntity> hatEntityList;

	// Token: 0x040019F0 RID: 6640
	public int curSelectedHatIndex;

	// Token: 0x040019F1 RID: 6641
	private int preSelectedHatIndex = -1;

	// Token: 0x040019F2 RID: 6642
	public GameObject HatPrefab;

	// Token: 0x040019F3 RID: 6643
	public GameObject HatObjParent;

	// Token: 0x040019F4 RID: 6644
	public Texture[] hatTexture;

	// Token: 0x040019F5 RID: 6645
	public GameObject NewStoreNullItemPrefabEnd;

	// Token: 0x040019F6 RID: 6646
	public GameObject NewStoreNullItemPrefabStart;

	// Token: 0x040019F7 RID: 6647
	public UILabel descriptionLabel;

	// Token: 0x040019F8 RID: 6648
	public UILabel enchantmentPropetyLabel;

	// Token: 0x040019F9 RID: 6649
	public UILabel hatNameLabel;

	// Token: 0x040019FA RID: 6650
	private HatEntity curSelectedHatItemInfo;

	// Token: 0x040019FB RID: 6651
	public UISprite priceTypeSprite;

	// Token: 0x040019FC RID: 6652
	public UILabel priceNumLabel;

	// Token: 0x040019FD RID: 6653
	public GameObject equipButton;

	// Token: 0x040019FE RID: 6654
	public GameObject unEquipButton;

	// Token: 0x040019FF RID: 6655
	public GameObject equipedLabel;

	// Token: 0x04001A00 RID: 6656
	public GameObject buyButton;

	// Token: 0x04001A01 RID: 6657
	public UILabel PriceOffSaleLabel;

	// Token: 0x04001A02 RID: 6658
	public UILabel PurchaseTipLabel;

	// Token: 0x04001A03 RID: 6659
	private bool isHatInstantiated;

	// Token: 0x04001A04 RID: 6660
	public int curSettedHatIndex;
}
