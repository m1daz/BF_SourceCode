using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002CA RID: 714
public class UIFestivalDirector : MonoBehaviour
{
	// Token: 0x060014EC RID: 5356 RVA: 0x000B3AE3 File Offset: 0x000B1EE3
	private void Awake()
	{
		UIFestivalDirector.mInstance = this;
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x000B3AEB File Offset: 0x000B1EEB
	private void OnDestroy()
	{
		if (UIFestivalDirector.mInstance != null)
		{
			UIFestivalDirector.mInstance = null;
		}
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x000B3B03 File Offset: 0x000B1F03
	public void Init()
	{
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x000B3B08 File Offset: 0x000B1F08
	private void Start()
	{
		this.rootCandyLabel.text = " X " + GrowthManagerKit.GetHolidayCurrency().ToString();
		this.randomItemNumLabel.text = string.Empty;
		this.randomItemLogoTextrue.mainTexture = (Resources.Load(this.path + "RandomItemLogo") as Texture);
		this.RefreshUiData();
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000B3B78 File Offset: 0x000B1F78
	private void Update()
	{
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x000B3B7C File Offset: 0x000B1F7C
	private void RefreshUiData()
	{
		this.topCandyLabel.text = GrowthManagerKit.GetHolidayCurrency().ToString();
		GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName("SM4");
		if (weaponItemInfoByName.mIsEnabled)
		{
			this.exchangeBtn[0].isEnabled = false;
			this.buyBtn[0].isEnabled = false;
		}
		else
		{
			this.exchangeBtn[0].isEnabled = true;
			this.buyBtn[0].isEnabled = true;
		}
		GSkinItemInfo skinItemInfoByName = GrowthManagerKit.GetSkinItemInfoByName("Skin_43");
		if (skinItemInfoByName.mIsEnabled)
		{
			this.exchangeBtn[1].isEnabled = false;
			this.buyBtn[1].isEnabled = false;
		}
		else
		{
			this.exchangeBtn[1].isEnabled = true;
			this.buyBtn[1].isEnabled = true;
		}
		GCapeItemInfo capeItemInfoByName = GrowthManagerKit.GetCapeItemInfoByName("Cape_8");
		if (capeItemInfoByName.mIsEnabled)
		{
			this.exchangeBtn[2].isEnabled = false;
			this.buyBtn[2].isEnabled = false;
		}
		else
		{
			this.exchangeBtn[2].isEnabled = true;
			this.buyBtn[2].isEnabled = true;
		}
		GWeaponItemInfo weaponItemInfoByName2 = GrowthManagerKit.GetWeaponItemInfoByName("SG36K");
		if (weaponItemInfoByName2.mIsEnabled)
		{
			this.exchangeBtn[3].isEnabled = false;
			this.buyBtn[3].isEnabled = false;
		}
		else
		{
			this.exchangeBtn[3].isEnabled = true;
			this.buyBtn[3].isEnabled = true;
		}
		GWeaponItemInfo weaponItemInfoByName3 = GrowthManagerKit.GetWeaponItemInfoByName("SAUG");
		if (weaponItemInfoByName3.mIsEnabled)
		{
			this.exchangeBtn[4].isEnabled = false;
			this.buyBtn[4].isEnabled = false;
		}
		else
		{
			this.exchangeBtn[4].isEnabled = true;
			this.buyBtn[4].isEnabled = true;
		}
		GSkinItemInfo skinItemInfoByName2 = GrowthManagerKit.GetSkinItemInfoByName("Skin_44");
		if (skinItemInfoByName2.mIsEnabled)
		{
			this.exchangeBtn[5].isEnabled = false;
			this.buyBtn[5].isEnabled = false;
		}
		else
		{
			this.exchangeBtn[5].isEnabled = true;
			this.buyBtn[5].isEnabled = true;
		}
		GSkinItemInfo skinItemInfoByName3 = GrowthManagerKit.GetSkinItemInfoByName("Skin_45");
		if (skinItemInfoByName3.mIsEnabled)
		{
			this.exchangeBtn[6].isEnabled = false;
			this.buyBtn[6].isEnabled = false;
		}
		else
		{
			this.exchangeBtn[6].isEnabled = true;
			this.buyBtn[6].isEnabled = true;
		}
		GSkinItemInfo skinItemInfoByName4 = GrowthManagerKit.GetSkinItemInfoByName("Skin_46");
		if (skinItemInfoByName4.mIsEnabled)
		{
			this.exchangeBtn[7].isEnabled = false;
			this.buyBtn[7].isEnabled = false;
		}
		else
		{
			this.exchangeBtn[7].isEnabled = true;
			this.buyBtn[7].isEnabled = true;
		}
		GSkinItemInfo skinItemInfoByName5 = GrowthManagerKit.GetSkinItemInfoByName("Skin_47");
		if (skinItemInfoByName5.mIsEnabled)
		{
			this.exchangeBtn[8].isEnabled = false;
			this.buyBtn[8].isEnabled = false;
		}
		else
		{
			this.exchangeBtn[8].isEnabled = true;
			this.buyBtn[8].isEnabled = true;
		}
		GSkinItemInfo skinItemInfoByName6 = GrowthManagerKit.GetSkinItemInfoByName("Skin_48");
		if (skinItemInfoByName6.mIsEnabled)
		{
			this.exchangeBtn[9].isEnabled = false;
			this.buyBtn[9].isEnabled = false;
		}
		else
		{
			this.exchangeBtn[9].isEnabled = true;
			this.buyBtn[9].isEnabled = true;
		}
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x000B3EF8 File Offset: 0x000B22F8
	public void BackBtnPressed()
	{
		base.StopAllCoroutines();
		this.rootCandyLabel.text = " X " + GrowthManagerKit.GetHolidayCurrency().ToString();
		this.RecoverRandomItem();
		this.candyExchangeNumIndex = 1;
		this.coinNumLabel.text = this.candyExchangeNumList[this.candyExchangeNumIndex].ToString();
		this.candyNumLabel.text = this.candyExchangeNumList[this.candyExchangeNumIndex].ToString();
		this.RefreshUiData();
		UIHomeDirector.mInstance.BackToRootNode(UIHomeDirector.mInstance.festivalNode);
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x000B3FA8 File Offset: 0x000B23A8
	public void ExchangeBtnPressed(int index)
	{
		int holidayCurrency = GrowthManagerKit.GetHolidayCurrency();
		switch (index)
		{
		case 1:
		{
			int num = 700;
			if (num > holidayCurrency)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "SOCKS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubHolidayCurrency(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "EXCHANGE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName("SM4");
				weaponItemInfoByName.AddWeaponTime(918000f, GWeaponRechargeType.WeaponTime);
				this.RefreshUiData();
			}
			break;
		}
		case 2:
		{
			int num = 200;
			if (num > holidayCurrency)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "SOCKS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubHolidayCurrency(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "EXCHANGE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName = GrowthManagerKit.GetSkinItemInfoByName("Skin_43");
				skinItemInfoByName.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 3:
		{
			int num = 500;
			if (num > holidayCurrency)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "SOCKS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubHolidayCurrency(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "EXCHANGE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GCapeItemInfo capeItemInfoByName = GrowthManagerKit.GetCapeItemInfoByName("Cape_8");
				capeItemInfoByName.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 4:
		{
			int num = 1000;
			if (num > holidayCurrency)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "SOCKS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubHolidayCurrency(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "EXCHANGE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GWeaponItemInfo weaponItemInfoByName2 = GrowthManagerKit.GetWeaponItemInfoByName("SG36K");
				weaponItemInfoByName2.AddWeaponTime(918000f, GWeaponRechargeType.WeaponTime);
				this.RefreshUiData();
			}
			break;
		}
		case 5:
		{
			int num = 1000;
			if (num > holidayCurrency)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "SOCKS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubHolidayCurrency(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "EXCHANGE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GWeaponItemInfo weaponItemInfoByName3 = GrowthManagerKit.GetWeaponItemInfoByName("SAUG");
				weaponItemInfoByName3.AddWeaponTime(918000f, GWeaponRechargeType.WeaponTime);
				this.RefreshUiData();
			}
			break;
		}
		case 6:
		{
			int num = 200;
			if (num > holidayCurrency)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "SOCKS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubHolidayCurrency(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "EXCHANGE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName2 = GrowthManagerKit.GetSkinItemInfoByName("Skin_44");
				skinItemInfoByName2.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 7:
		{
			int num = 200;
			if (num > holidayCurrency)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "SOCKS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubHolidayCurrency(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "EXCHANGE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName3 = GrowthManagerKit.GetSkinItemInfoByName("Skin_45");
				skinItemInfoByName3.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 8:
		{
			int num = 200;
			if (num > holidayCurrency)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "SOCKS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubHolidayCurrency(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "EXCHANGE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName4 = GrowthManagerKit.GetSkinItemInfoByName("Skin_46");
				skinItemInfoByName4.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 9:
		{
			int num = 200;
			if (num > holidayCurrency)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "SOCKS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubHolidayCurrency(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "EXCHANGE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName5 = GrowthManagerKit.GetSkinItemInfoByName("Skin_47");
				skinItemInfoByName5.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 10:
		{
			int num = 200;
			if (num > holidayCurrency)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "SOCKS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubHolidayCurrency(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "EXCHANGE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName6 = GrowthManagerKit.GetSkinItemInfoByName("Skin_48");
				skinItemInfoByName6.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 11:
		{
			int num = 10;
			if (num > holidayCurrency)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "SOCKS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubHolidayCurrency(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "EXCHANGE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				HolidaySlotsItemInfo holidaySlotsResultInfo = GrowthManagerKit.GetHolidaySlotsResultInfo();
				if (holidaySlotsResultInfo.num > 0)
				{
					this.randomItemNumLabel.text = holidaySlotsResultInfo.num.ToString();
				}
				this.randomItemLogoTextrue.mainTexture = (Resources.Load(this.path + holidaySlotsResultInfo.spriteName) as Texture);
				this.RefreshUiData();
			}
			break;
		}
		}
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x000B475C File Offset: 0x000B2B5C
	public void BuyBtnPressed(int index)
	{
		int gems = GrowthManagerKit.GetGems();
		int coins = GrowthManagerKit.GetCoins();
		switch (index)
		{
		case 1:
		{
			int num = 75;
			if (num > gems)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "GEMS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubGems(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "PURCHASE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName("SM4");
				weaponItemInfoByName.AddWeaponTime(918000f, GWeaponRechargeType.WeaponTime);
				this.RefreshUiData();
			}
			break;
		}
		case 2:
		{
			int num = 1500;
			if (num > coins)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "COINS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubCoins(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "PURCHASE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName = GrowthManagerKit.GetSkinItemInfoByName("Skin_43");
				skinItemInfoByName.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 3:
		{
			int num = 60;
			if (num > gems)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "GEMS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubGems(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "PURCHASE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GCapeItemInfo capeItemInfoByName = GrowthManagerKit.GetCapeItemInfoByName("Cape_8");
				capeItemInfoByName.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 4:
		{
			int num = 105;
			if (num > gems)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "GEMS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubGems(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "PURCHASE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GWeaponItemInfo weaponItemInfoByName2 = GrowthManagerKit.GetWeaponItemInfoByName("SG36K");
				weaponItemInfoByName2.AddWeaponTime(918000f, GWeaponRechargeType.WeaponTime);
				this.RefreshUiData();
			}
			break;
		}
		case 5:
		{
			int num = 105;
			if (num > gems)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "GEMS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubGems(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "PURCHASE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GWeaponItemInfo weaponItemInfoByName3 = GrowthManagerKit.GetWeaponItemInfoByName("SAUG");
				weaponItemInfoByName3.AddWeaponTime(918000f, GWeaponRechargeType.WeaponTime);
				this.RefreshUiData();
			}
			break;
		}
		case 6:
		{
			int num = 1500;
			if (num > coins)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "COINS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubCoins(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "PURCHASE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName2 = GrowthManagerKit.GetSkinItemInfoByName("Skin_44");
				skinItemInfoByName2.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 7:
		{
			int num = 1500;
			if (num > coins)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "COINS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubCoins(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "PURCHASE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName3 = GrowthManagerKit.GetSkinItemInfoByName("Skin_45");
				skinItemInfoByName3.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 8:
		{
			int num = 1500;
			if (num > coins)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "COINS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubCoins(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "PURCHASE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName4 = GrowthManagerKit.GetSkinItemInfoByName("Skin_46");
				skinItemInfoByName4.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 9:
		{
			int num = 1500;
			if (num > coins)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "COINS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubCoins(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "PURCHASE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName5 = GrowthManagerKit.GetSkinItemInfoByName("Skin_47");
				skinItemInfoByName5.Enable();
				this.RefreshUiData();
			}
			break;
		}
		case 10:
		{
			int num = 1500;
			if (num > coins)
			{
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "COINS LACK !";
				this.purchaseStatusLabel.color = Color.red;
			}
			else
			{
				GrowthManagerKit.SubCoins(num);
				this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
				this.purchaseStatusLabel.text = "PURCHASE SUCCESS !";
				this.purchaseStatusLabel.color = Color.green;
				GSkinItemInfo skinItemInfoByName6 = GrowthManagerKit.GetSkinItemInfoByName("Skin_48");
				skinItemInfoByName6.Enable();
				this.RefreshUiData();
			}
			break;
		}
		}
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x000B4E2C File Offset: 0x000B322C
	public void CandyNumRightBtnPressed()
	{
		this.candyExchangeNumIndex++;
		if (this.candyExchangeNumIndex > 3)
		{
			this.candyExchangeNumIndex = 0;
		}
		this.coinNumLabel.text = this.candyExchangeNumList[this.candyExchangeNumIndex].ToString();
		this.candyNumLabel.text = this.candyExchangeNumList[this.candyExchangeNumIndex].ToString();
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x000B4EA8 File Offset: 0x000B32A8
	public void CandyNumLeftBtnPressed()
	{
		this.candyExchangeNumIndex--;
		if (this.candyExchangeNumIndex < 0)
		{
			this.candyExchangeNumIndex = 3;
		}
		this.coinNumLabel.text = this.candyExchangeNumList[this.candyExchangeNumIndex].ToString();
		this.candyNumLabel.text = this.candyExchangeNumList[this.candyExchangeNumIndex].ToString();
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x000B4F24 File Offset: 0x000B3324
	public void CandyExchangeBtnPressed()
	{
		int holidayCurrency = GrowthManagerKit.GetHolidayCurrency();
		if (this.candyExchangeNumList[this.candyExchangeNumIndex] > holidayCurrency)
		{
			this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
			this.purchaseStatusLabel.text = "SOCKS LACK !";
			this.purchaseStatusLabel.color = Color.red;
		}
		else
		{
			GrowthManagerKit.SubHolidayCurrency(this.candyExchangeNumList[this.candyExchangeNumIndex]);
			GrowthManagerKit.AddCoins(this.candyExchangeNumList[this.candyExchangeNumIndex]);
			this.ShowSubCoinLabel(this.purchaseStatusLabel.gameObject, null, 2f);
			this.purchaseStatusLabel.text = "PURCHASE SUCCESS !";
			this.purchaseStatusLabel.color = Color.green;
			this.RefreshUiData();
		}
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x000B4FE7 File Offset: 0x000B33E7
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

	// Token: 0x060014F9 RID: 5369 RVA: 0x000B5028 File Offset: 0x000B3428
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
		if (this.randomItemLogoTextrue.mainTexture.name != "RandomItemLogo")
		{
			yield return new WaitForSeconds(1f);
			this.RecoverRandomItem();
		}
		yield break;
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x000B5058 File Offset: 0x000B3458
	private void RecoverRandomItem()
	{
		this.randomItemLogoTextrue.mainTexture = (Resources.Load(this.path + "RandomItemLogo") as Texture);
		this.randomItemNumLabel.text = string.Empty;
	}

	// Token: 0x040017A3 RID: 6051
	public static UIFestivalDirector mInstance;

	// Token: 0x040017A4 RID: 6052
	public UILabel rootCandyLabel;

	// Token: 0x040017A5 RID: 6053
	public UILabel purchaseStatusLabel;

	// Token: 0x040017A6 RID: 6054
	public UITexture randomItemLogoTextrue;

	// Token: 0x040017A7 RID: 6055
	public UILabel randomItemNumLabel;

	// Token: 0x040017A8 RID: 6056
	public UIButton randomItemExchangeBtn;

	// Token: 0x040017A9 RID: 6057
	public UILabel candyNumLabel;

	// Token: 0x040017AA RID: 6058
	public UILabel coinNumLabel;

	// Token: 0x040017AB RID: 6059
	private int candyExchangeNumIndex;

	// Token: 0x040017AC RID: 6060
	private int[] candyExchangeNumList = new int[]
	{
		1,
		10,
		100,
		1000
	};

	// Token: 0x040017AD RID: 6061
	public UILabel topCandyLabel;

	// Token: 0x040017AE RID: 6062
	public UIButton[] exchangeBtn = new UIButton[10];

	// Token: 0x040017AF RID: 6063
	public UIButton[] buyBtn = new UIButton[10];

	// Token: 0x040017B0 RID: 6064
	private string path = "UI/Images/SlotLogo/";
}
