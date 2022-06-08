using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200030E RID: 782
public class UINewStoreWeaponPrefabInstiate : MonoBehaviour
{
	// Token: 0x060017E6 RID: 6118 RVA: 0x000C93D6 File Offset: 0x000C77D6
	private void Awake()
	{
		if (UINewStoreWeaponPrefabInstiate.mInstance == null)
		{
			UINewStoreWeaponPrefabInstiate.mInstance = this;
		}
	}

	// Token: 0x060017E7 RID: 6119 RVA: 0x000C93EE File Offset: 0x000C77EE
	private void OnDestroy()
	{
		if (UINewStoreWeaponPrefabInstiate.mInstance != null)
		{
			UINewStoreWeaponPrefabInstiate.mInstance = null;
		}
	}

	// Token: 0x060017E8 RID: 6120 RVA: 0x000C9406 File Offset: 0x000C7806
	private void Start()
	{
		this.InitData();
		this.WeaponEquipInit();
	}

	// Token: 0x060017E9 RID: 6121 RVA: 0x000C9414 File Offset: 0x000C7814
	private void Update()
	{
		if (this.newWeaponPrefab.Count == 0)
		{
			return;
		}
		if (this.curWeaponCategoryIndex != this.preWeaponCategoryIndex)
		{
			this.preWeaponCategoryIndex = this.curWeaponCategoryIndex;
			base.GetComponent<UIScrollView>().transform.localPosition = new Vector3(0f, 0f, 0f);
			base.GetComponent<UIPanel>().clipOffset = new Vector2(0f, 0f);
			this.curSelectedWeaponIndex = 0;
			this.RefreshWeaponProperty();
			this.RefreshWeaponPurchase();
			this.WeaponShowForCharacter();
		}
		else if (this.curSelectedWeaponIndex != this.preSelectedWeaponIndex)
		{
			this.preSelectedWeaponIndex = this.curSelectedWeaponIndex;
			this.RefreshWeaponProperty();
			this.RefreshWeaponPurchase();
			this.WeaponShowForCharacter();
		}
	}

	// Token: 0x060017EA RID: 6122 RVA: 0x000C94DC File Offset: 0x000C78DC
	public void InitData()
	{
		for (int i = 0; i < this.WeaponCategoryNum; i++)
		{
			this.weaponItemInfoCategoryList[i] = new List<GWeaponItemInfo>();
		}
		this.ReadWeaponData();
	}

	// Token: 0x060017EB RID: 6123 RVA: 0x000C9514 File Offset: 0x000C7914
	public void ReadWeaponData()
	{
		this.weaponNameList = GrowthManagerKit.GetAllWeaponNameList();
		if (this.weaponNameList.Length > 0)
		{
			this.weaponItemInfo = new GWeaponItemInfo[this.weaponNameList.Length];
			for (int i = 0; i < this.weaponNameList.Length; i++)
			{
				this.weaponItemInfo[i] = GrowthManagerKit.GetWeaponItemInfoByName(this.weaponNameList[i]);
			}
		}
		this.ClassifyWeaponInfo(this.weaponItemInfo);
	}

	// Token: 0x060017EC RID: 6124 RVA: 0x000C9588 File Offset: 0x000C7988
	public void RefreshWeaponDisplay()
	{
		while (this.newWeaponPrefab.Count > 0)
		{
			for (int i = 0; i < this.newWeaponPrefab.Count; i++)
			{
				UnityEngine.Object.DestroyImmediate(this.newWeaponPrefab[i]);
			}
			this.newWeaponPrefab.Clear();
		}
		if (this.weaponItemInfoCategoryList[this.curWeaponCategoryIndex].Count > 0)
		{
			for (int j = 0; j < this.weaponItemInfoCategoryList[this.curWeaponCategoryIndex].Count; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.weaponPrefab, this.weaponPrefab.transform.position, this.weaponPrefab.transform.rotation);
				gameObject.transform.parent = this.weaponObjParent.transform;
				gameObject.transform.localScale = this.weaponPrefab.transform.localScale;
				gameObject.transform.localPosition = new Vector3((float)j * 200f, 0f, 0f);
				gameObject.transform.rotation = this.weaponPrefab.transform.rotation;
				if (this.curWeaponCategoryIndex == 0)
				{
					gameObject.transform.Find("Texture(Weapon)").GetComponent<UITexture>().mainTexture = this.meeleTexture[j];
				}
				else if (this.curWeaponCategoryIndex == 1)
				{
					gameObject.transform.Find("Texture(Weapon)").GetComponent<UITexture>().mainTexture = this.deagleTexture[j];
				}
				else if (this.curWeaponCategoryIndex == 2)
				{
					gameObject.transform.Find("Texture(Weapon)").GetComponent<UITexture>().mainTexture = this.machineTexture[j];
				}
				else if (this.curWeaponCategoryIndex == 3)
				{
					gameObject.transform.Find("Texture(Weapon)").GetComponent<UITexture>().mainTexture = this.rifleTexture[j];
				}
				else if (this.curWeaponCategoryIndex == 4)
				{
					gameObject.transform.Find("Texture(Weapon)").GetComponent<UITexture>().mainTexture = this.sniperTexture[j];
				}
				else if (this.curWeaponCategoryIndex == 5)
				{
					gameObject.transform.Find("Texture(Weapon)").GetComponent<UITexture>().mainTexture = this.specialTexture[j];
				}
				else if (this.curWeaponCategoryIndex == 6)
				{
					gameObject.transform.Find("Texture(Weapon)").GetComponent<UITexture>().mainTexture = this.grenadeTexture[j];
				}
				if (this.weaponItemInfoCategoryList[this.curWeaponCategoryIndex][j] != null)
				{
					gameObject.GetComponent<UINewStoreWeaponPrefab>().index = j;
				}
				this.newWeaponPrefab.Add(gameObject);
			}
		}
	}

	// Token: 0x060017ED RID: 6125 RVA: 0x000C984C File Offset: 0x000C7C4C
	private void ClassifyWeaponInfo(GWeaponItemInfo[] info)
	{
		if (info.Length > 0)
		{
			int i = 0;
			while (i < info.Length)
			{
				string mGunType = info[i].mGunType;
				switch (mGunType)
				{
				case "Knife":
					if (info[i].mName != "GingerbreadKnife" && info[i].mName != "ZombieHand")
					{
						this.weaponItemInfoCategoryList[0].Add(info[i]);
					}
					break;
				case "Pistol":
					this.weaponItemInfoCategoryList[1].Add(info[i]);
					break;
				case "Rifle":
					if (info[i].mName != "CandyRifle")
					{
						if (info[i].mName == "Nightmare")
						{
							this.weaponItemInfoCategoryList[5].Add(info[i]);
						}
						else
						{
							this.weaponItemInfoCategoryList[3].Add(info[i]);
						}
					}
					break;
				case "ShotGun":
					this.weaponItemInfoCategoryList[1].Add(info[i]);
					break;
				case "SubmachineGun":
					if (info[i].mName == "TeslaP1" || info[i].mName == "Flamethrower")
					{
						this.weaponItemInfoCategoryList[5].Add(info[i]);
					}
					else
					{
						this.weaponItemInfoCategoryList[2].Add(info[i]);
					}
					break;
				case "SniperRifle":
					if (info[i].mName != "ChristmasSniper")
					{
						this.weaponItemInfoCategoryList[4].Add(info[i]);
					}
					break;
				case "MachineGun":
					if (info[i].mName != "ImpulseGun")
					{
						this.weaponItemInfoCategoryList[2].Add(info[i]);
					}
					else
					{
						this.weaponItemInfoCategoryList[5].Add(info[i]);
					}
					break;
				case "Bazooka":
					this.weaponItemInfoCategoryList[5].Add(info[i]);
					break;
				case "Thrown":
					this.weaponItemInfoCategoryList[6].Add(info[i]);
					break;
				}
				IL_2A1:
				i++;
				continue;
				goto IL_2A1;
			}
		}
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x000C9B08 File Offset: 0x000C7F08
	public void RefreshWeaponProperty()
	{
		this.curSelectedWeaponItemInfo = this.weaponItemInfoCategoryList[this.curWeaponCategoryIndex][this.curSelectedWeaponIndex];
		this.curSelectedWeaponItemInfo = GrowthManagerKit.GetWeaponItemInfoByName(this.curSelectedWeaponItemInfo.mName);
		this.SelectWeaponPurchaseUnlimit();
		this.propertyDic = this.curSelectedWeaponItemInfo.mPropertyList;
		if (this.propertyDic.Count < 6)
		{
			int num = 0;
			foreach (KeyValuePair<string, float> keyValuePair in this.propertyDic)
			{
				if (!this.propertyNameSprite[num].enabled)
				{
					this.propertyNameSprite[num].enabled = true;
					this.propertyNameLabel[num].enabled = true;
					this.propertyVauleSprite[num].enabled = true;
					this.propertyVauleSpriteBg[num].enabled = true;
					this.propertyVauleNumLabel[num].enabled = true;
					this.propertyLvSprite[num].enabled = true;
				}
				int propertyCurLv = this.curSelectedWeaponItemInfo.GetPropertyCurLv(keyValuePair.Key);
				this.propertyLvSprite[num].spriteName = this.StarSpriteName[propertyCurLv];
				this.propertyNameLabel[num].text = keyValuePair.Key.ToUpper();
				this.propertyVauleSprite[num].fillAmount = keyValuePair.Value * 0.1f;
				this.propertyVauleNumLabel[num].text = ((int)(keyValuePair.Value * 10f)).ToString();
				num++;
			}
			for (int i = num; i < 6; i++)
			{
				this.propertyNameSprite[i].enabled = false;
				this.propertyNameLabel[i].enabled = false;
				this.propertyVauleSprite[i].enabled = false;
				this.propertyVauleSpriteBg[i].enabled = false;
				this.propertyVauleNumLabel[i].enabled = false;
				this.propertyLvSprite[i].enabled = false;
			}
		}
		this.weaponNameLabel.text = this.curSelectedWeaponItemInfo.mNameDisplay;
		this.descriptionLabel.text = this.curSelectedWeaponItemInfo.mFeatureDescription;
		if (this.curSelectedWeaponItemInfo.mIsNoLimitedUse)
		{
			this.restTimeLabel.text = "REST TIME : UNLIMIT";
		}
		else
		{
			this.restTimeLabel.text = "REST TIME : " + UIToolFunctionController.ParseTimeSeconds((int)this.curSelectedWeaponItemInfo.mWeaponTimeRest, 0);
		}
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x000C9DA0 File Offset: 0x000C81A0
	public void RefreshWeaponPurchase()
	{
		this.curSelectedWeaponItemInfo = this.weaponItemInfoCategoryList[this.curWeaponCategoryIndex][this.curSelectedWeaponIndex];
		this.curSelectedWeaponItemInfo = GrowthManagerKit.GetWeaponItemInfoByName(this.curSelectedWeaponItemInfo.mName);
		this.weaponBuyTimeOffRateLabel.text = this.curSelectedWeaponItemInfo.mOffRateDescription;
		if (this.curSelectedWeaponItemInfo.mGunType == "Thrown" || this.curSelectedWeaponItemInfo.mIsNoLimitedUse || !this.curSelectedWeaponItemInfo.mSellable)
		{
			this.priceRegion.SetActive(false);
			if (!this.curSelectedWeaponItemInfo.mSellable)
			{
				this.PurchaseTipLabel.text = this.curSelectedWeaponItemInfo.mPurchaseTipsText;
			}
			else
			{
				this.PurchaseTipLabel.text = string.Empty;
			}
		}
		else
		{
			this.priceRegion.SetActive(true);
			this.PurchaseTipLabel.text = string.Empty;
		}
		if (this.curSelectedWeaponItemInfo.mIsOnlyUnlimitedBuy)
		{
			this.TimeBuyRegion.SetActive(false);
		}
		else
		{
			this.TimeBuyRegion.SetActive(true);
		}
		if (this.curSelectedWeaponItemInfo.mGunType != "Thrown")
		{
			if (!this.curSelectedWeaponItemInfo.mIsEnabled)
			{
				this.equipButton.SetActive(false);
				this.unequipButton.SetActive(false);
				this.plusButton.SetActive(false);
			}
			else
			{
				if (this.curSelectedWeaponItemInfo.mIsEquipped)
				{
					this.unequipButton.SetActive(true);
					this.equipButton.SetActive(false);
				}
				else
				{
					this.equipButton.SetActive(true);
					this.unequipButton.SetActive(false);
				}
				if (this.curSelectedWeaponItemInfo.mCanUpgrade)
				{
					this.plusButton.SetActive(true);
				}
				else
				{
					this.plusButton.SetActive(false);
				}
			}
			if (this.curSelectedWeaponItemInfo.mIsCollection)
			{
				this.collectionChipsRegion.SetActive(true);
				string[] mChipsSpriteName = this.curSelectedWeaponItemInfo.mChipsSpriteName;
				for (int i = 0; i < this.collectionChipsTexture.Length; i++)
				{
					this.collectionChipsTexture[i].mainTexture = (Resources.Load("UI/Images/SlotLogo/" + mChipsSpriteName[i]) as Texture);
				}
			}
			else
			{
				this.collectionChipsRegion.SetActive(false);
			}
		}
		else
		{
			if (UIUserDataController.allThrowWeapon[UIUserDataController.GetQuickBarItemIndex()] == this.curSelectedWeaponItemInfo.mName)
			{
				this.equipButton.SetActive(false);
				this.unequipButton.SetActive(true);
			}
			else
			{
				this.equipButton.SetActive(true);
				this.unequipButton.SetActive(false);
			}
			this.plusButton.SetActive(false);
			this.collectionChipsRegion.SetActive(false);
		}
	}

	// Token: 0x060017F0 RID: 6128 RVA: 0x000CA074 File Offset: 0x000C8474
	public void SelectWeaponPurchase3H()
	{
		int num = 1;
		this.weaponSelectedIndex = num;
		GItemPurchaseType timeFillPurchaseType = this.curSelectedWeaponItemInfo.GetTimeFillPurchaseType(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime);
		this.priceType.spriteName = ((timeFillPurchaseType != GItemPurchaseType.CoinsPurchase) ? ((timeFillPurchaseType != GItemPurchaseType.GemPurchase) ? "HonorPoint" : "Gem") : "Coin");
		this.priceNumLabel.text = this.curSelectedWeaponItemInfo.GetTimeFillPrice(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime).ToString();
		this.purchasedTime = (float)(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num] * 3600);
		this.priceNum = this.curSelectedWeaponItemInfo.GetTimeFillPrice(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime);
	}

	// Token: 0x060017F1 RID: 6129 RVA: 0x000CA140 File Offset: 0x000C8540
	public void SelectWeaponPurchase6H()
	{
		int num = 2;
		this.weaponSelectedIndex = num;
		GItemPurchaseType timeFillPurchaseType = this.curSelectedWeaponItemInfo.GetTimeFillPurchaseType(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime);
		this.priceType.spriteName = ((timeFillPurchaseType != GItemPurchaseType.CoinsPurchase) ? ((timeFillPurchaseType != GItemPurchaseType.GemPurchase) ? "HonorPoint" : "Gem") : "Coin");
		this.priceNumLabel.text = this.curSelectedWeaponItemInfo.GetTimeFillPrice(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime).ToString();
		this.purchasedTime = (float)(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num] * 3600);
		this.priceNum = this.curSelectedWeaponItemInfo.GetTimeFillPrice(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime);
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x000CA20C File Offset: 0x000C860C
	public void SelectWeaponPurchase24H()
	{
		int num = 5;
		this.weaponSelectedIndex = num;
		GItemPurchaseType timeFillPurchaseType = this.curSelectedWeaponItemInfo.GetTimeFillPurchaseType(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime);
		this.priceType.spriteName = ((timeFillPurchaseType != GItemPurchaseType.CoinsPurchase) ? ((timeFillPurchaseType != GItemPurchaseType.GemPurchase) ? "HonorPoint" : "Gem") : "Coin");
		this.priceNumLabel.text = this.curSelectedWeaponItemInfo.GetTimeFillPrice(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime).ToString();
		this.purchasedTime = (float)(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num] * 3600);
		this.priceNum = this.curSelectedWeaponItemInfo.GetTimeFillPrice(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime);
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x000CA2D8 File Offset: 0x000C86D8
	public void SelectWeaponPurchaseUnlimit()
	{
		int num = 0;
		this.weaponSelectedIndex = num;
		GItemPurchaseType timeFillPurchaseType = this.curSelectedWeaponItemInfo.GetTimeFillPurchaseType(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime);
		this.priceType.spriteName = ((timeFillPurchaseType != GItemPurchaseType.CoinsPurchase) ? ((timeFillPurchaseType != GItemPurchaseType.GemPurchase) ? "HonorPoint" : "Gem") : "Coin");
		this.priceNumLabel.text = this.curSelectedWeaponItemInfo.GetTimeFillPrice(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime).ToString();
		this.purchasedTime = (float)(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num] * 3600);
		this.priceNum = this.curSelectedWeaponItemInfo.GetTimeFillPrice(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[num], GWeaponRechargeType.WeaponTime);
		this.unlimitUIToggle.value = true;
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x000CA3B0 File Offset: 0x000C87B0
	public void WeaponPurchase()
	{
		this.priceNum = this.curSelectedWeaponItemInfo.GetTimeFillPrice(this.curSelectedWeaponItemInfo.mWeaponTimeFillLevel[this.weaponSelectedIndex], GWeaponRechargeType.WeaponTime);
		if (this.priceType.spriteName == "Gem")
		{
			if (GrowthManagerKit.GetGems() >= this.priceNum)
			{
				GrowthManagerKit.SubGems(this.priceNum);
				this.curSelectedWeaponItemInfo.AddWeaponTime(this.purchasedTime, GWeaponRechargeType.WeaponTime);
				if (this.curSelectedWeaponItemInfo.mIsNoLimitedUse)
				{
					this.restTimeLabel.text = "REST TIME : UNLIMIT";
				}
				else
				{
					this.restTimeLabel.text = "REST TIME : " + UIToolFunctionController.ParseTimeSeconds((int)this.curSelectedWeaponItemInfo.mWeaponTimeRest, 0);
				}
				this.RefreshWeaponPurchase();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
		else if (this.priceType.spriteName == "Coin")
		{
			if (GrowthManagerKit.GetCoins() >= this.priceNum)
			{
				GrowthManagerKit.SubCoins(this.priceNum);
				this.curSelectedWeaponItemInfo.AddWeaponTime(this.purchasedTime, GWeaponRechargeType.WeaponTime);
				if (this.curSelectedWeaponItemInfo.mIsNoLimitedUse)
				{
					this.restTimeLabel.text = "REST TIME : UNLIMIT";
				}
				else
				{
					this.restTimeLabel.text = "REST TIME : " + UIToolFunctionController.ParseTimeSeconds((int)this.curSelectedWeaponItemInfo.mWeaponTimeRest, 0);
				}
				this.RefreshWeaponPurchase();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
		else if (this.priceType.spriteName == "HonorPoint")
		{
			if (GrowthManagerKit.GetHonorPoint() >= this.priceNum)
			{
				GrowthManagerKit.SubHonorPoint(this.priceNum);
				this.curSelectedWeaponItemInfo.AddWeaponTime(this.purchasedTime, GWeaponRechargeType.WeaponTime);
				if (this.curSelectedWeaponItemInfo.mIsNoLimitedUse)
				{
					this.restTimeLabel.text = "REST TIME : UNLIMIT";
				}
				else
				{
					this.restTimeLabel.text = "REST TIME : " + UIToolFunctionController.ParseTimeSeconds((int)this.curSelectedWeaponItemInfo.mWeaponTimeRest, 0);
				}
				this.RefreshWeaponPurchase();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipHonorPointLack();
			}
		}
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x000CA5E8 File Offset: 0x000C89E8
	public void WeaponPlus()
	{
		this.WeaponWindow.SetActive(false);
		this.WeaponPlusWindow.SetActive(true);
		this.WeaponPlusOnePropertyWindow.SetActive(false);
		this.WeaponPlusPropertyWindow.SetActive(true);
		if (this.curWeaponCategoryIndex == 0)
		{
			this.TextureWeaponPlus.GetComponent<UITexture>().mainTexture = this.meeleTexture[this.curSelectedWeaponIndex];
		}
		else if (this.curWeaponCategoryIndex == 1)
		{
			this.TextureWeaponPlus.GetComponent<UITexture>().mainTexture = this.deagleTexture[this.curSelectedWeaponIndex];
		}
		else if (this.curWeaponCategoryIndex == 2)
		{
			this.TextureWeaponPlus.GetComponent<UITexture>().mainTexture = this.machineTexture[this.curSelectedWeaponIndex];
		}
		else if (this.curWeaponCategoryIndex == 3)
		{
			this.TextureWeaponPlus.GetComponent<UITexture>().mainTexture = this.rifleTexture[this.curSelectedWeaponIndex];
		}
		else if (this.curWeaponCategoryIndex == 4)
		{
			this.TextureWeaponPlus.GetComponent<UITexture>().mainTexture = this.sniperTexture[this.curSelectedWeaponIndex];
		}
		else if (this.curWeaponCategoryIndex == 5)
		{
			this.TextureWeaponPlus.GetComponent<UITexture>().mainTexture = this.specialTexture[this.curSelectedWeaponIndex];
		}
		else if (this.curWeaponCategoryIndex == 6)
		{
			this.TextureWeaponPlus.GetComponent<UITexture>().mainTexture = this.grenadeTexture[this.curSelectedWeaponIndex];
		}
		this.RefreshWeaponPlusProperty();
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x000CA768 File Offset: 0x000C8B68
	public void RefreshWeaponPlusProperty()
	{
		this.plusWeaponItemInfo = GrowthManagerKit.GetComparedWeaponItemInfoListByName(this.curSelectedWeaponItemInfo.mName);
		this.plusWeaponNameLabel.text = this.curSelectedWeaponItemInfo.mName;
		this.propertyDic = this.plusWeaponItemInfo[0].mPropertyList;
		this.plusPropertyDic = this.plusWeaponItemInfo[1].mPropertyList;
		if (this.propertyDic.Count < 6)
		{
			int num = 0;
			foreach (KeyValuePair<string, float> keyValuePair in this.propertyDic)
			{
				if (!this.propertyNameSpriteBeforePlus[num].enabled)
				{
					this.propertyNameSpriteBeforePlus[num].enabled = true;
					this.propertyNameLabelBeforePlus[num].enabled = true;
					this.propertyVauleSpriteBeforePlus[num].enabled = true;
					this.propertyVauleSpriteBgBeforePlus[num].enabled = true;
					this.propertyVauleNumLabelBeforePlus[num].enabled = true;
					this.propertyLvSpriteBeforePlus[num].enabled = true;
				}
				this.propertyNameLabelBeforePlus[num].text = keyValuePair.Key.ToUpper();
				this.propertyVauleSpriteBeforePlus[num].fillAmount = keyValuePair.Value * 0.1f;
				this.propertyValue[num] = keyValuePair.Value;
				this.propertyVauleNumLabelBeforePlus[num].text = ((int)(keyValuePair.Value * 10f)).ToString();
				int propertyCurLv = this.plusWeaponItemInfo[0].GetPropertyCurLv(keyValuePair.Key);
				this.propertyLvSpriteBeforePlus[num].spriteName = this.StarSpriteName[propertyCurLv];
				num++;
			}
			for (int i = num; i < 6; i++)
			{
				this.propertyNameSpriteBeforePlus[i].enabled = false;
				this.propertyNameLabelBeforePlus[i].enabled = false;
				this.propertyVauleSpriteBeforePlus[i].enabled = false;
				this.propertyVauleSpriteBgBeforePlus[i].enabled = false;
				this.propertyVauleNumLabelBeforePlus[i].enabled = false;
				this.propertyLvSpriteBeforePlus[i].enabled = false;
			}
		}
		if (this.plusPropertyDic.Count < 6)
		{
			int num2 = 0;
			foreach (KeyValuePair<string, float> keyValuePair2 in this.plusPropertyDic)
			{
				if (!this.propertyNameSpriteAfterPlus[num2].enabled)
				{
					this.propertyNameSpriteAfterPlus[num2].enabled = true;
					this.propertyNameLabelAfterPlus[num2].enabled = true;
					this.propertyVauleSpriteAfterPlus[num2].enabled = true;
					this.propertyVauleSpriteBgAfterPlus[num2].enabled = true;
					this.propertyVauleNumLabelAfterPlus[num2].enabled = true;
				}
				this.propertyAddButton[num2].gameObject.SetActive(false);
				this.propertyNameLabelAfterPlus[num2].text = keyValuePair2.Key.ToUpper();
				if (Mathf.Abs(this.propertyValue[num2] - keyValuePair2.Value) > 0.02f)
				{
					this.propertyNameSpriteAfterPlus[num2].color = Color.green;
					this.propertyAddButton[num2].gameObject.SetActive(true);
				}
				else
				{
					this.propertyNameSpriteAfterPlus[num2].color = Color.white;
				}
				this.propertyVauleNumLabelAfterPlus[num2].text = ((int)(keyValuePair2.Value * 10f)).ToString();
				this.propertyVauleSpriteAfterPlus[num2].fillAmount = keyValuePair2.Value * 0.1f;
				this.propertyAddButton[num2].gameObject.name = keyValuePair2.Key;
				num2++;
			}
			for (int j = num2; j < 6; j++)
			{
				this.propertyNameSpriteAfterPlus[j].enabled = false;
				this.propertyNameLabelAfterPlus[j].enabled = false;
				this.propertyVauleSpriteAfterPlus[j].enabled = false;
				this.propertyVauleSpriteBgAfterPlus[j].enabled = false;
				this.propertyVauleNumLabelAfterPlus[j].enabled = false;
				this.propertyAddButton[j].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x000CABCC File Offset: 0x000C8FCC
	public void SelectWeaponPlusOneProperty()
	{
		this.buttonName = UIButton.current.name;
		this.WeaponPlusOnePropertyWindow.SetActive(true);
		this.WeaponPlusPropertyWindow.SetActive(false);
		this.onePropertyNameLabel.text = this.buttonName.ToUpper() + " LV:";
		this.onePropertyLvOriginLabel.text = this.curSelectedWeaponItemInfo.GetPropertyCurLv(this.buttonName).ToString();
		this.onePropertyLvNextLabel.text = (this.curSelectedWeaponItemInfo.GetPropertyCurLv(this.buttonName) + 1).ToString();
		GWeaponItemInfo.UpgradeConditionsSet[] upgradeConditions = this.curSelectedWeaponItemInfo.GetUpgradeConditions(this.buttonName);
		this.staffSprite.mainTexture = (Resources.Load("UI/Images/UINewStoreLogo/" + upgradeConditions[0].conditions[0].typeSpriteName) as Texture2D);
		this.plusOnePropertyCoinsPriceLabel.text = upgradeConditions[0].conditions[1].costNum.ToString();
		this.plusCoinPriceNum = int.Parse(this.plusOnePropertyCoinsPriceLabel.text);
		this.plusOnePropertyGemPriceLabel.text = upgradeConditions[1].conditions[1].costNum.ToString();
		this.plusCoinPriceNum = int.Parse(this.plusOnePropertyGemPriceLabel.text);
		this.plusOnePropertyCoinsSuccessRateLabel.text = (upgradeConditions[0].successRate * 100f).ToString() + "%";
		this.plusOnePropertyGemSuccessRateLabel.text = (upgradeConditions[1].successRate * 100f).ToString() + "%";
		if (!upgradeConditions[0].isConditionsReady)
		{
			this.PlusOnePropertyButtonUseCoins.isEnabled = false;
		}
		else
		{
			this.PlusOnePropertyButtonUseCoins.isEnabled = true;
		}
		if (!upgradeConditions[1].isConditionsReady)
		{
			this.PlusOnePropertyButtonUseGems.isEnabled = false;
		}
		else
		{
			this.PlusOnePropertyButtonUseGems.isEnabled = true;
		}
	}

	// Token: 0x060017F8 RID: 6136 RVA: 0x000CAE0C File Offset: 0x000C920C
	public void RefreshWeaponPlusOneProperty()
	{
		this.onePropertyNameLabel.text = this.buttonName.ToUpper() + " LV:";
		this.onePropertyLvOriginLabel.text = this.curSelectedWeaponItemInfo.GetPropertyCurLv(this.buttonName).ToString();
		this.onePropertyLvNextLabel.text = (this.curSelectedWeaponItemInfo.GetPropertyCurLv(this.buttonName) + 1).ToString();
		GWeaponItemInfo.UpgradeConditionsSet[] upgradeConditions = this.curSelectedWeaponItemInfo.GetUpgradeConditions(this.buttonName);
		this.staffSprite.mainTexture = (Resources.Load("UI/Images/UINewStoreLogo/" + upgradeConditions[0].conditions[0].typeSpriteName) as Texture2D);
		this.plusOnePropertyCoinsPriceLabel.text = upgradeConditions[0].conditions[1].costNum.ToString();
		this.plusCoinPriceNum = int.Parse(this.plusOnePropertyCoinsPriceLabel.text);
		this.plusOnePropertyGemPriceLabel.text = upgradeConditions[1].conditions[1].costNum.ToString();
		this.plusCoinPriceNum = int.Parse(this.plusOnePropertyGemPriceLabel.text);
		this.plusOnePropertyCoinsSuccessRateLabel.text = (upgradeConditions[0].successRate * 100f).ToString() + "%";
		this.plusOnePropertyGemSuccessRateLabel.text = (upgradeConditions[1].successRate * 100f).ToString() + "%";
		if (!upgradeConditions[0].isConditionsReady)
		{
			this.PlusOnePropertyButtonUseCoins.isEnabled = false;
		}
		else
		{
			this.PlusOnePropertyButtonUseCoins.isEnabled = true;
		}
		if (!upgradeConditions[1].isConditionsReady)
		{
			this.PlusOnePropertyButtonUseGems.isEnabled = false;
		}
		else
		{
			this.PlusOnePropertyButtonUseGems.isEnabled = true;
		}
	}

	// Token: 0x060017F9 RID: 6137 RVA: 0x000CB024 File Offset: 0x000C9424
	public void WeaponPlusPurchaseUseCoin()
	{
		this.isUpgradeProcess_COIN = true;
		this.plusResultProcess.gameObject.SetActive(true);
		this.plusResultMask.SetActive(true);
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x000CB04C File Offset: 0x000C944C
	public bool JudgeUpgradeUseCoinResult()
	{
		bool result = false;
		int mModelLv = this.curSelectedWeaponItemInfo.mModelLv;
		if (this.curSelectedWeaponItemInfo.UpgradeProperty(this.buttonName, GItemPurchaseType.CoinsPurchase))
		{
			this.plusResultSuccessful.gameObject.SetActive(true);
			if (this.curSelectedWeaponItemInfo.mModelLv != mModelLv)
			{
				this.WeaponUpgradeShowForCharacter(this.curSelectedWeaponItemInfo.mModelLv);
			}
			result = true;
		}
		else
		{
			this.RefreshWeaponPlusOneProperty();
			this.plusResultFailed.gameObject.SetActive(true);
		}
		return result;
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x000CB0D0 File Offset: 0x000C94D0
	public void WeaponPlusPurchaseUseGem()
	{
		this.isUpgradeProcess_GEM = true;
		this.plusResultProcess.gameObject.SetActive(true);
		this.plusResultMask.SetActive(true);
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x000CB0F8 File Offset: 0x000C94F8
	public bool JudgeUpgradeUseGemResult()
	{
		bool result = false;
		int mModelLv = this.curSelectedWeaponItemInfo.mModelLv;
		if (this.curSelectedWeaponItemInfo.UpgradeProperty(this.buttonName, GItemPurchaseType.GemPurchase))
		{
			this.plusResultSuccessful.gameObject.SetActive(true);
			if (this.curSelectedWeaponItemInfo.mModelLv != mModelLv)
			{
				this.WeaponUpgradeShowForCharacter(this.curSelectedWeaponItemInfo.mModelLv);
			}
			result = true;
		}
		else
		{
			this.RefreshWeaponPlusOneProperty();
			this.plusResultFailed.gameObject.SetActive(true);
		}
		return result;
	}

	// Token: 0x060017FD RID: 6141 RVA: 0x000CB17C File Offset: 0x000C957C
	public void WeaponPlusCancel()
	{
		this.WeaponWindow.SetActive(true);
		this.WeaponPlusWindow.SetActive(false);
		this.RefreshWeaponProperty();
	}

	// Token: 0x060017FE RID: 6142 RVA: 0x000CB19C File Offset: 0x000C959C
	public void WeaponPlusOnePropertyCancel()
	{
		this.WeaponPlusOnePropertyWindow.SetActive(false);
		this.WeaponPlusPropertyWindow.SetActive(true);
		this.RefreshWeaponPlusProperty();
	}

	// Token: 0x060017FF RID: 6143 RVA: 0x000CB1BC File Offset: 0x000C95BC
	public void WeaponPlusWindowDontShow()
	{
		this.WeaponWindow.SetActive(true);
		this.WeaponPlusWindow.SetActive(false);
	}

	// Token: 0x06001800 RID: 6144 RVA: 0x000CB1D8 File Offset: 0x000C95D8
	public void WeaponEquipInit()
	{
		this.weaponItemInfoEquiped = GrowthManagerKit.GetCurEquippedWeaponItemInfoListForStore();
		int num = 0;
		if (this.weaponItemInfoEquiped != null)
		{
			num = this.weaponItemInfoEquiped.Length;
		}
		for (int i = 0; i < num; i++)
		{
			this.weaponEquipSpriteForCharacter[i].spriteName = ((this.weaponItemInfoEquiped[i] != null) ? this.weaponItemInfoEquiped[i].mLogoSpriteName : ("WeaponSlotDefault_" + (i + 1).ToString()));
		}
		for (int j = num; j < GrowthManagerKit.GetCurWeaponEquipLimitedNum(); j++)
		{
			this.weaponEquipSpriteForCharacter[j].spriteName = "Null";
		}
		if (UIUserDataController.GetQuickBarItemIndex() == 6)
		{
			this.grenadeEquipSpriteForCharacter.spriteName = "Null";
		}
		else
		{
			this.grenadeEquipSpriteForCharacter.spriteName = GrowthManagerKit.GetWeaponItemInfoByName(UIUserDataController.allThrowWeapon[UIUserDataController.GetQuickBarItemIndex()]).mLogoSpriteName;
		}
	}

	// Token: 0x06001801 RID: 6145 RVA: 0x000CB2C8 File Offset: 0x000C96C8
	public void WeaponEquip()
	{
		if (this.curSelectedWeaponItemInfo.mGunType != "Thrown")
		{
			GrowthManagerKit.ProcessOneWeaponEquipTap(this.curSelectedWeaponItemInfo.mName);
			this.WeaponEquipInit();
		}
		else
		{
			for (int i = 0; i < UIUserDataController.allThrowWeapon.Length; i++)
			{
				if (this.curSelectedWeaponItemInfo.mName == UIUserDataController.allThrowWeapon[i])
				{
					UIUserDataController.SetQuickBarItemIndex(i);
					break;
				}
			}
			this.grenadeEquipSpriteForCharacter.spriteName = this.curSelectedWeaponItemInfo.mLogoSpriteName;
		}
		this.equipButton.SetActive(false);
		this.unequipButton.SetActive(true);
	}

	// Token: 0x06001802 RID: 6146 RVA: 0x000CB378 File Offset: 0x000C9778
	public void WeaponUnequip()
	{
		if (this.curSelectedWeaponItemInfo.mGunType != "Thrown")
		{
			GrowthManagerKit.ProcessOneWeaponEquipTap(this.curSelectedWeaponItemInfo.mName);
			this.WeaponEquipInit();
		}
		else
		{
			UIUserDataController.SetQuickBarItemIndex(6);
			this.grenadeEquipSpriteForCharacter.spriteName = "Null";
		}
		this.equipButton.SetActive(true);
		this.unequipButton.SetActive(false);
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x000CB3E8 File Offset: 0x000C97E8
	public void WeaponUninstall(int weaponButtonIndex)
	{
		this.weaponItemInfoEquiped = GrowthManagerKit.GetCurEquippedWeaponItemInfoListForStore();
		int num = 0;
		if (this.weaponItemInfoEquiped != null)
		{
			num = this.weaponItemInfoEquiped.Length;
		}
		if (weaponButtonIndex < num)
		{
			if (this.weaponItemInfoEquiped[weaponButtonIndex] != null)
			{
				if (this.weaponItemInfoEquiped[weaponButtonIndex].mName == this.curSelectedWeaponItemInfo.mName)
				{
					this.equipButton.SetActive(true);
					this.unequipButton.SetActive(false);
				}
				GrowthManagerKit.ProcessOneWeaponEquipTap(this.weaponItemInfoEquiped[weaponButtonIndex].mName);
			}
			this.WeaponEquipInit();
		}
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x000CB47C File Offset: 0x000C987C
	public void WeaponShowForCharacter()
	{
		int num = 0;
		for (int i = 0; i < this.weaponNameList.Length; i++)
		{
			if (this.curSelectedWeaponItemInfo.mName == this.weaponNameList[i])
			{
				num = i;
				break;
			}
		}
		UINewStoreAnimationForCharacter.mInstance.SetCurWeaponIndex(num);
		int mModelLv = this.curSelectedWeaponItemInfo.mModelLv;
		if (mModelLv > 0)
		{
			UINewStoreAnimationForCharacter.mInstance.SetCurWeaponUpgradeIndex(num, mModelLv);
		}
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x000CB4F4 File Offset: 0x000C98F4
	public void WeaponUpgradeShowForCharacter(int upgradelV)
	{
		int weaponindex = 0;
		for (int i = 0; i < this.weaponNameList.Length; i++)
		{
			if (this.curSelectedWeaponItemInfo.mName == this.weaponNameList[i])
			{
				weaponindex = i;
				break;
			}
		}
		UINewStoreAnimationForCharacter.mInstance.SetCurWeaponUpgradeIndex(weaponindex, upgradelV);
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x000CB54C File Offset: 0x000C994C
	public void BackToMeeleUIToggle()
	{
		this.MeeleUIToggle.value = true;
	}

	// Token: 0x04001B2E RID: 6958
	public static UINewStoreWeaponPrefabInstiate mInstance;

	// Token: 0x04001B2F RID: 6959
	public int curWeaponCategoryIndex;

	// Token: 0x04001B30 RID: 6960
	public int preWeaponCategoryIndex = -1;

	// Token: 0x04001B31 RID: 6961
	public int WeaponCategoryNum = 7;

	// Token: 0x04001B32 RID: 6962
	public List<GWeaponItemInfo>[] weaponItemInfoCategoryList = new List<GWeaponItemInfo>[7];

	// Token: 0x04001B33 RID: 6963
	private string[] weaponNameList;

	// Token: 0x04001B34 RID: 6964
	private GWeaponItemInfo[] weaponItemInfo;

	// Token: 0x04001B35 RID: 6965
	public GameObject weaponPrefab;

	// Token: 0x04001B36 RID: 6966
	public GameObject weaponObjParent;

	// Token: 0x04001B37 RID: 6967
	public List<GameObject> newWeaponPrefab = new List<GameObject>();

	// Token: 0x04001B38 RID: 6968
	public int curSelectedWeaponIndex;

	// Token: 0x04001B39 RID: 6969
	public int preSelectedWeaponIndex = -1;

	// Token: 0x04001B3A RID: 6970
	private GWeaponItemInfo curSelectedWeaponItemInfo;

	// Token: 0x04001B3B RID: 6971
	public Texture[] meeleTexture;

	// Token: 0x04001B3C RID: 6972
	public Texture[] deagleTexture;

	// Token: 0x04001B3D RID: 6973
	public Texture[] rifleTexture;

	// Token: 0x04001B3E RID: 6974
	public Texture[] machineTexture;

	// Token: 0x04001B3F RID: 6975
	public Texture[] sniperTexture;

	// Token: 0x04001B40 RID: 6976
	public Texture[] specialTexture;

	// Token: 0x04001B41 RID: 6977
	public Texture[] grenadeTexture;

	// Token: 0x04001B42 RID: 6978
	public GameObject NewStoreNullItemPrefabEnd;

	// Token: 0x04001B43 RID: 6979
	public GameObject NewStoreNullItemPrefabStart;

	// Token: 0x04001B44 RID: 6980
	public UIToggle MeeleUIToggle;

	// Token: 0x04001B45 RID: 6981
	public UISprite[] propertyVauleSprite = new UISprite[6];

	// Token: 0x04001B46 RID: 6982
	public UISprite[] propertyVauleSpriteBg = new UISprite[6];

	// Token: 0x04001B47 RID: 6983
	public UISprite[] propertyNameSprite = new UISprite[6];

	// Token: 0x04001B48 RID: 6984
	public UILabel[] propertyNameLabel = new UILabel[6];

	// Token: 0x04001B49 RID: 6985
	public UILabel[] propertyVauleNumLabel = new UILabel[6];

	// Token: 0x04001B4A RID: 6986
	public UISprite[] propertyLvSprite = new UISprite[6];

	// Token: 0x04001B4B RID: 6987
	private Dictionary<string, float> propertyDic;

	// Token: 0x04001B4C RID: 6988
	private string[] priceDropdownItems = new string[GWeaponItemInfo.mWeaponTimeFillLevelNum];

	// Token: 0x04001B4D RID: 6989
	public UILabel weaponNameLabel;

	// Token: 0x04001B4E RID: 6990
	public UILabel descriptionLabel;

	// Token: 0x04001B4F RID: 6991
	public UILabel weaponBuyTimeOffRateLabel;

	// Token: 0x04001B50 RID: 6992
	private int restTime;

	// Token: 0x04001B51 RID: 6993
	private int plusTime;

	// Token: 0x04001B52 RID: 6994
	public UILabel restTimeLabel;

	// Token: 0x04001B53 RID: 6995
	public string[] StarSpriteName;

	// Token: 0x04001B54 RID: 6996
	public GameObject WeaponWindow;

	// Token: 0x04001B55 RID: 6997
	public GameObject priceRegion;

	// Token: 0x04001B56 RID: 6998
	public GameObject TimeBuyRegion;

	// Token: 0x04001B57 RID: 6999
	public GameObject plusButton;

	// Token: 0x04001B58 RID: 7000
	public GameObject equipButton;

	// Token: 0x04001B59 RID: 7001
	public GameObject unequipButton;

	// Token: 0x04001B5A RID: 7002
	public UILabel priceNumLabel;

	// Token: 0x04001B5B RID: 7003
	public UISprite priceType;

	// Token: 0x04001B5C RID: 7004
	private int priceNum;

	// Token: 0x04001B5D RID: 7005
	private float purchasedTime;

	// Token: 0x04001B5E RID: 7006
	public UIToggle unlimitUIToggle;

	// Token: 0x04001B5F RID: 7007
	public UILabel PurchaseTipLabel;

	// Token: 0x04001B60 RID: 7008
	private int weaponSelectedIndex;

	// Token: 0x04001B61 RID: 7009
	public GameObject collectionChipsRegion;

	// Token: 0x04001B62 RID: 7010
	public UITexture[] collectionChipsTexture;

	// Token: 0x04001B63 RID: 7011
	public GameObject WeaponPlusWindow;

	// Token: 0x04001B64 RID: 7012
	public GameObject WeaponPlusPropertyWindow;

	// Token: 0x04001B65 RID: 7013
	public GameObject TextureWeaponPlus;

	// Token: 0x04001B66 RID: 7014
	private GWeaponItemInfo[] plusWeaponItemInfo = new GWeaponItemInfo[2];

	// Token: 0x04001B67 RID: 7015
	private Dictionary<string, float> plusPropertyDic;

	// Token: 0x04001B68 RID: 7016
	public UISprite[] propertyVauleSpriteBeforePlus = new UISprite[6];

	// Token: 0x04001B69 RID: 7017
	public UISprite[] propertyVauleSpriteBgBeforePlus = new UISprite[6];

	// Token: 0x04001B6A RID: 7018
	public UISprite[] propertyNameSpriteBeforePlus = new UISprite[6];

	// Token: 0x04001B6B RID: 7019
	public UISprite[] propertyVauleSpriteAfterPlus = new UISprite[6];

	// Token: 0x04001B6C RID: 7020
	public UILabel[] propertyVauleNumLabelBeforePlus = new UILabel[6];

	// Token: 0x04001B6D RID: 7021
	public UISprite[] propertyLvSpriteBeforePlus = new UISprite[6];

	// Token: 0x04001B6E RID: 7022
	public UISprite[] propertyVauleSpriteBgAfterPlus = new UISprite[6];

	// Token: 0x04001B6F RID: 7023
	public UISprite[] propertyNameSpriteAfterPlus = new UISprite[6];

	// Token: 0x04001B70 RID: 7024
	public UILabel[] propertyNameLabelBeforePlus = new UILabel[6];

	// Token: 0x04001B71 RID: 7025
	public UILabel[] propertyNameLabelAfterPlus = new UILabel[6];

	// Token: 0x04001B72 RID: 7026
	public UILabel[] propertyVauleNumLabelAfterPlus = new UILabel[6];

	// Token: 0x04001B73 RID: 7027
	public UIButton[] propertyAddButton = new UIButton[6];

	// Token: 0x04001B74 RID: 7028
	private float[] propertyValue = new float[6];

	// Token: 0x04001B75 RID: 7029
	public UILabel plusWeaponNameLabel;

	// Token: 0x04001B76 RID: 7030
	private string buttonName = string.Empty;

	// Token: 0x04001B77 RID: 7031
	public GameObject WeaponPlusOnePropertyWindow;

	// Token: 0x04001B78 RID: 7032
	public UILabel onePropertyNameLabel;

	// Token: 0x04001B79 RID: 7033
	public UILabel onePropertyLvOriginLabel;

	// Token: 0x04001B7A RID: 7034
	public UILabel onePropertyLvNextLabel;

	// Token: 0x04001B7B RID: 7035
	public UITexture staffSprite;

	// Token: 0x04001B7C RID: 7036
	public UILabel plusOnePropertyCoinsPriceLabel;

	// Token: 0x04001B7D RID: 7037
	public UILabel plusOnePropertyGemPriceLabel;

	// Token: 0x04001B7E RID: 7038
	public UILabel plusOnePropertyCoinsSuccessRateLabel;

	// Token: 0x04001B7F RID: 7039
	public UILabel plusOnePropertyGemSuccessRateLabel;

	// Token: 0x04001B80 RID: 7040
	public UIButton PlusOnePropertyButtonUseCoins;

	// Token: 0x04001B81 RID: 7041
	public UIButton PlusOnePropertyButtonUseGems;

	// Token: 0x04001B82 RID: 7042
	public UINewSotreWeaponUpgrade mUINewSotreWeaponUpgrade;

	// Token: 0x04001B83 RID: 7043
	public UISprite plusResultProcess;

	// Token: 0x04001B84 RID: 7044
	public UILabel plusResultSuccessful;

	// Token: 0x04001B85 RID: 7045
	public UILabel plusResultFailed;

	// Token: 0x04001B86 RID: 7046
	public GameObject plusResultSuccessfulEffect;

	// Token: 0x04001B87 RID: 7047
	public GameObject plusResultMask;

	// Token: 0x04001B88 RID: 7048
	private int plusCoinPriceNum;

	// Token: 0x04001B89 RID: 7049
	private int plusGemPriceNum;

	// Token: 0x04001B8A RID: 7050
	public bool isUpgradeProcess_COIN;

	// Token: 0x04001B8B RID: 7051
	public bool isUpgradeProcess_GEM;

	// Token: 0x04001B8C RID: 7052
	public UISprite grenadeEquipSpriteForCharacter;

	// Token: 0x04001B8D RID: 7053
	public UISprite[] weaponEquipSpriteForCharacter;

	// Token: 0x04001B8E RID: 7054
	private GWeaponItemInfo[] weaponItemInfoEquiped;
}
