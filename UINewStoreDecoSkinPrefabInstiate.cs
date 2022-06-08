using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F1 RID: 753
public class UINewStoreDecoSkinPrefabInstiate : MonoBehaviour
{
	// Token: 0x06001733 RID: 5939 RVA: 0x000C4BD9 File Offset: 0x000C2FD9
	private void Start()
	{
		this.SkinInit();
	}

	// Token: 0x06001734 RID: 5940 RVA: 0x000C4BE1 File Offset: 0x000C2FE1
	private void Update()
	{
		if (!this.isSkinInstantiated)
		{
			return;
		}
		if (this.curSelectedSkinIndex != this.preSelectedSkinIndex)
		{
			this.preSelectedSkinIndex = this.curSelectedSkinIndex;
			this.RefreshSkinProperty();
			this.RefreshSkinPurchase();
			this.SkinShowForCharacter();
		}
	}

	// Token: 0x06001735 RID: 5941 RVA: 0x000C4C20 File Offset: 0x000C3020
	public void InstantiateSkin()
	{
		if (!this.isSkinInstantiated)
		{
			while (this.NewStoreSkinItem.Count > 0)
			{
				for (int i = 0; i < this.NewStoreSkinItem.Count; i++)
				{
					UnityEngine.Object.DestroyImmediate(this.NewStoreSkinItem[i]);
				}
				this.NewStoreSkinItem.Clear();
			}
			this.skinEntityList = SkinManager.mInstance.myAllSkinEntityList;
			for (int j = 0; j < this.skinEntityList.Count; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SkinPrefab, this.SkinPrefab.transform.position, this.SkinPrefab.transform.rotation);
				gameObject.transform.parent = this.SkinObjParent.transform;
				gameObject.transform.localScale = this.SkinPrefab.transform.localScale;
				gameObject.transform.localPosition = new Vector3((float)(j - 1) * 200f, 0f, 0f);
				gameObject.transform.rotation = this.SkinPrefab.transform.rotation;
				gameObject.transform.Find("skinModel/player_model").GetComponent<Renderer>().material.mainTexture = this.skinEntityList[j].tex;
				gameObject.GetComponent<UINewStoreDecoSkinPrefab>().index = j;
				this.NewStoreSkinItem.Add(gameObject);
			}
			this.isSkinInstantiated = true;
		}
	}

	// Token: 0x06001736 RID: 5942 RVA: 0x000C4DA0 File Offset: 0x000C31A0
	public void RefreshSkinProperty()
	{
		this.curSelectedSkinItemInfo = this.skinEntityList[this.curSelectedSkinIndex];
		this.skinNameLabel.text = this.curSelectedSkinItemInfo.info.mNameDisplay;
		this.descriptionLabel.text = this.curSelectedSkinItemInfo.info.mDescription;
		this.enchantmentPropetyLabel.text = string.Empty;
	}

	// Token: 0x06001737 RID: 5943 RVA: 0x000C4E0C File Offset: 0x000C320C
	public void RefreshSkinPurchase()
	{
		this.curSelectedSkinItemInfo = this.skinEntityList[this.curSelectedSkinIndex];
		this.unEquipButton.SetActive(false);
		if (!this.curSelectedSkinItemInfo.info.mIsEnabled)
		{
			if (this.curSelectedSkinItemInfo.info.mSellable)
			{
				this.buyButton.SetActive(true);
				this.PurchaseTipLabel.text = string.Empty;
			}
			else
			{
				this.buyButton.SetActive(false);
				this.PurchaseTipLabel.text = this.curSelectedSkinItemInfo.info.mPurchaseTipsText;
			}
			this.equipButton.SetActive(false);
			this.equipedLabel.SetActive(false);
		}
		else
		{
			this.buyButton.SetActive(false);
			this.PurchaseTipLabel.text = string.Empty;
			if (this.curSelectedSkinItemInfo.isSetted)
			{
				this.equipedLabel.SetActive(true);
				this.equipButton.SetActive(false);
			}
			else
			{
				this.equipedLabel.SetActive(false);
				this.equipButton.SetActive(true);
			}
		}
		if (this.curSelectedSkinIndex >= SkinManager.mInstance.mySystemSkinEntityList.Count)
		{
			this.editButton.SetActive(true);
			this.deleteButton.SetActive(true);
			if (SkinManager.mInstance.CanShare(this.curSelectedSkinItemInfo))
			{
				this.shareButton.SetActive(true);
			}
			else
			{
				this.shareButton.SetActive(false);
			}
		}
		else
		{
			this.editButton.SetActive(false);
			this.deleteButton.SetActive(false);
			this.shareButton.SetActive(false);
		}
		this.PriceOffSaleLabel.text = this.skinEntityList[this.curSelectedSkinIndex].info.mOffRateDescription;
		this.priceTypeSprite.spriteName = ((this.skinEntityList[this.curSelectedSkinIndex].info.mPurchasedType != GItemPurchaseType.CoinsPurchase) ? ((this.skinEntityList[this.curSelectedSkinIndex].info.mPurchasedType != GItemPurchaseType.GemPurchase) ? "Null" : "Gem") : "Coin");
		this.priceNumLabel.text = this.skinEntityList[this.curSelectedSkinIndex].info.mPrice.ToString();
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x000C5078 File Offset: 0x000C3478
	public void SkinPurchase()
	{
		if (this.skinEntityList[this.curSelectedSkinIndex].info.mPurchasedType == GItemPurchaseType.CoinsPurchase)
		{
			if (GrowthManagerKit.GetCoins() >= this.skinEntityList[this.curSelectedSkinIndex].info.mPrice)
			{
				GrowthManagerKit.SubCoins(this.skinEntityList[this.curSelectedSkinIndex].info.mPrice);
				this.skinEntityList[this.curSelectedSkinIndex].info.Enable();
				this.RefreshSkinPurchase();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
		else if (this.skinEntityList[this.curSelectedSkinIndex].info.mPurchasedType == GItemPurchaseType.GemPurchase)
		{
			if (GrowthManagerKit.GetGems() >= this.skinEntityList[this.curSelectedSkinIndex].info.mPrice)
			{
				GrowthManagerKit.SubGems(this.skinEntityList[this.curSelectedSkinIndex].info.mPrice);
				this.skinEntityList[this.curSelectedSkinIndex].info.Enable();
				this.RefreshSkinPurchase();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
	}

	// Token: 0x06001739 RID: 5945 RVA: 0x000C51B7 File Offset: 0x000C35B7
	public void SkinEquip()
	{
		SkinManager.mInstance.SetCurSettedSkin(this.curSelectedSkinIndex);
		this.curSettedSkinIndex = this.curSelectedSkinIndex;
		this.equipedLabel.SetActive(true);
		this.equipButton.SetActive(false);
	}

	// Token: 0x0600173A RID: 5946 RVA: 0x000C51ED File Offset: 0x000C35ED
	public void SkinShowForCharacter()
	{
		UINewStoreEquipForCharacter.mInstance.EquipSkin(this.skinEntityList[this.curSelectedSkinIndex].tex);
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x000C5210 File Offset: 0x000C3610
	public void SkinInit()
	{
		if (SkinManager.mInstance.myCharacterSkinEntity != null)
		{
			UINewStoreEquipForCharacter.mInstance.EquipSkin(SkinManager.mInstance.myCharacterSkinEntity.tex);
		}
		for (int i = 0; i < SkinManager.mInstance.myAllSkinEntityList.Count; i++)
		{
			if (SkinManager.mInstance.myCharacterSkinEntity.name == SkinManager.mInstance.myAllSkinEntityList[i].name)
			{
				this.curSettedSkinIndex = i;
				break;
			}
		}
	}

	// Token: 0x0600173C RID: 5948 RVA: 0x000C529F File Offset: 0x000C369F
	public void SkinDefaultEquipSet()
	{
		UINewStoreEquipForCharacter.mInstance.EquipSkin(SkinManager.mInstance.myAllSkinEntityList[this.curSettedSkinIndex].tex);
	}

	// Token: 0x0600173D RID: 5949 RVA: 0x000C52C5 File Offset: 0x000C36C5
	public void CustomSkinCreate()
	{
		if (SkinManager.mInstance.myCustomSkinEntityList.Count >= 30)
		{
			UINewStoreBasicWindowDirector.mInstance.TipReachCustomSkinLimit();
		}
		else
		{
			SkinManager.mInstance.LoadEditorForNewSkin();
		}
	}

	// Token: 0x0600173E RID: 5950 RVA: 0x000C52F6 File Offset: 0x000C36F6
	public void CustomSkinFix()
	{
		SkinManager.mInstance.LoadEditorForExistSkin(this.skinEntityList[this.curSelectedSkinIndex].name);
	}

	// Token: 0x0600173F RID: 5951 RVA: 0x000C5318 File Offset: 0x000C3718
	public void CustomSkinDelete()
	{
		UINewStoreBasicWindowDirector.mInstance.TipDeleteCustomSkin();
	}

	// Token: 0x06001740 RID: 5952 RVA: 0x000C5324 File Offset: 0x000C3724
	public void DeleteSkin()
	{
		if (this.skinEntityList[this.curSelectedSkinIndex].isSetted)
		{
			SkinManager.mInstance.SetCurSettedSkin(0);
			this.curSettedSkinIndex = 0;
		}
		SkinManager.mInstance.DeleteSkin(this.curSelectedSkinIndex);
		UnityEngine.Object.DestroyImmediate(this.NewStoreSkinItem[this.curSelectedSkinIndex]);
		this.NewStoreSkinItem.Remove(this.NewStoreSkinItem[this.curSelectedSkinIndex]);
		for (int i = this.curSelectedSkinIndex; i < this.NewStoreSkinItem.Count; i++)
		{
			this.NewStoreSkinItem[i].transform.localPosition -= new Vector3(200f, 0f, 0f);
			this.NewStoreSkinItem[i].GetComponent<UINewStoreDecoSkinPrefab>().index--;
		}
		if (this.curSelectedSkinIndex == this.NewStoreSkinItem.Count)
		{
			this.curSelectedSkinIndex--;
		}
		this.RefreshSkinProperty();
		this.RefreshSkinPurchase();
		this.SkinShowForCharacter();
	}

	// Token: 0x06001741 RID: 5953 RVA: 0x000C544B File Offset: 0x000C384B
	public void ShareSkin()
	{
		this.shareButton.SetActive(false);
		SkinManager.mInstance.UploadCustomSkin(this.curSelectedSkinItemInfo);
	}

	// Token: 0x06001742 RID: 5954 RVA: 0x000C5469 File Offset: 0x000C3869
	public void BackToSkinUIToggle()
	{
		this.SkinUIToggle.value = true;
	}

	// Token: 0x04001A23 RID: 6691
	private List<SkinEntity> skinEntityList;

	// Token: 0x04001A24 RID: 6692
	public int curSelectedSkinIndex;

	// Token: 0x04001A25 RID: 6693
	private int preSelectedSkinIndex = -1;

	// Token: 0x04001A26 RID: 6694
	public GameObject SkinPrefab;

	// Token: 0x04001A27 RID: 6695
	public GameObject SkinObjParent;

	// Token: 0x04001A28 RID: 6696
	public GameObject NewStoreNullItemPrefabEnd;

	// Token: 0x04001A29 RID: 6697
	public GameObject NewStoreNullItemPrefabStart;

	// Token: 0x04001A2A RID: 6698
	public List<GameObject> NewStoreSkinItem;

	// Token: 0x04001A2B RID: 6699
	public UIToggle SkinUIToggle;

	// Token: 0x04001A2C RID: 6700
	public UILabel skinNameLabel;

	// Token: 0x04001A2D RID: 6701
	public UILabel descriptionLabel;

	// Token: 0x04001A2E RID: 6702
	public UILabel enchantmentPropetyLabel;

	// Token: 0x04001A2F RID: 6703
	private SkinEntity curSelectedSkinItemInfo;

	// Token: 0x04001A30 RID: 6704
	public UISprite priceTypeSprite;

	// Token: 0x04001A31 RID: 6705
	public UILabel priceNumLabel;

	// Token: 0x04001A32 RID: 6706
	public GameObject equipButton;

	// Token: 0x04001A33 RID: 6707
	public GameObject unEquipButton;

	// Token: 0x04001A34 RID: 6708
	public GameObject equipedLabel;

	// Token: 0x04001A35 RID: 6709
	public GameObject buyButton;

	// Token: 0x04001A36 RID: 6710
	public GameObject editButton;

	// Token: 0x04001A37 RID: 6711
	public GameObject deleteButton;

	// Token: 0x04001A38 RID: 6712
	public GameObject shareButton;

	// Token: 0x04001A39 RID: 6713
	public UILabel PriceOffSaleLabel;

	// Token: 0x04001A3A RID: 6714
	public UILabel PurchaseTipLabel;

	// Token: 0x04001A3B RID: 6715
	public bool isSkinInstantiated;

	// Token: 0x04001A3C RID: 6716
	public int curSettedSkinIndex;
}
