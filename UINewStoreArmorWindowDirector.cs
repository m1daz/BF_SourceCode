using System;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class UINewStoreArmorWindowDirector : MonoBehaviour
{
	// Token: 0x060016D9 RID: 5849 RVA: 0x000C21DC File Offset: 0x000C05DC
	private void Start()
	{
		this.ArmorInit();
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x000C21E4 File Offset: 0x000C05E4
	private void Update()
	{
		if (this.curSelectedArmorIndex != this.preSelectedArmorIndex)
		{
			this.preSelectedArmorIndex = this.curSelectedArmorIndex;
			this.RefreshArmorProperty();
			this.RefreshArmorPurchase();
		}
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x000C2210 File Offset: 0x000C0610
	public void RefreshArmorProperty()
	{
		this.curArmorInfo = GrowthManagerKit.GetArmorItemInfoByName(UserDataController.AllArmorNameList[this.curSelectedArmorIndex]);
		this.ArmorNameLabel.text = this.curArmorInfo.mNameDisplay;
		this.DesLabel.text = this.curArmorInfo.mDescription;
		this.armorFillTimeLevel = this.curArmorInfo.mAutoSupplyTimeFillLevel;
		this.RestTimeLabel.text = UIToolFunctionController.ParseTimeSeconds((int)this.curArmorInfo.mAutoSupplyTimeRest, 0);
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x000C2290 File Offset: 0x000C0690
	public void RefreshArmorPurchase()
	{
		if (this.curArmorInfo.mIsEquipped)
		{
			this.equipButton.SetActive(false);
			this.equipedLabel.SetActive(true);
		}
		else
		{
			this.equipButton.SetActive(true);
			this.equipedLabel.SetActive(false);
		}
		this.ArmorTimeSelect3H();
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x000C22E8 File Offset: 0x000C06E8
	public void ArmorTimeSelect3H()
	{
		this.armorPriceType.spriteName = this.purchaseType;
		this.armorPurchasedTime = 0;
		this.armorPriceNum = this.curArmorInfo.GetTimeFillPrice(this.armorFillTimeLevel[0]);
		this.armorPriceNumLabel.text = this.armorPriceNum.ToString();
		this.ArmorUIToggle.value = true;
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x000C2350 File Offset: 0x000C0750
	public void ArmorTimeSelect6H()
	{
		this.armorPriceType.spriteName = this.purchaseType;
		this.armorPurchasedTime = 1;
		this.armorPriceNum = this.curArmorInfo.GetTimeFillPrice(this.armorFillTimeLevel[1]);
		this.armorPriceNumLabel.text = this.armorPriceNum.ToString();
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x000C23AC File Offset: 0x000C07AC
	public void ArmorTimeSelect24H()
	{
		this.armorPriceType.spriteName = this.purchaseType;
		this.armorPurchasedTime = 4;
		this.armorPriceNum = this.curArmorInfo.GetTimeFillPrice(this.armorFillTimeLevel[4]);
		this.armorPriceNumLabel.text = this.armorPriceNum.ToString();
	}

	// Token: 0x060016E0 RID: 5856 RVA: 0x000C2408 File Offset: 0x000C0808
	public void ArmorPurchase()
	{
		if (this.curArmorInfo.mAutoSupplyTimeRest < 172800f)
		{
			if (this.purchaseType == "Gem")
			{
				if (GrowthManagerKit.GetGems() >= this.armorPriceNum)
				{
					GrowthManagerKit.SubGems(this.armorPriceNum);
					this.curArmorInfo.AddAutoSupplyTime((float)(this.armorFillTimeLevel[this.armorPurchasedTime] * 3600));
					this.RefreshArmorProperty();
				}
				else
				{
					UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
				}
			}
			else if (this.purchaseType == "Coin")
			{
				if (GrowthManagerKit.GetCoins() >= this.armorPriceNum)
				{
					GrowthManagerKit.SubCoins(this.armorPriceNum);
					this.curArmorInfo.AddAutoSupplyTime((float)(this.armorFillTimeLevel[this.armorPurchasedTime] * 3600));
					this.RefreshArmorProperty();
				}
				else
				{
					UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
				}
			}
		}
	}

	// Token: 0x060016E1 RID: 5857 RVA: 0x000C2500 File Offset: 0x000C0900
	public void ArmorEquip()
	{
		GrowthManagerKit.SetCurSettedArmor(this.curArmorInfo.mName);
		this.equipButton.SetActive(false);
		this.equipedLabel.SetActive(true);
		this.armorEquipForCharacter.GetComponent<UITexture>().mainTexture = this.allArmorEquipForCharacter[this.curSelectedArmorIndex];
	}

	// Token: 0x060016E2 RID: 5858 RVA: 0x000C2554 File Offset: 0x000C0954
	public void ArmorInit()
	{
		if (GrowthManagerKit.GetCurSettedArmorInfo().mName == "BodyArmor_1")
		{
			this.armorEquipForCharacter.GetComponent<UITexture>().mainTexture = this.allArmorEquipForCharacter[0];
		}
		else if (GrowthManagerKit.GetCurSettedArmorInfo().mName == "HeadArmor_1")
		{
			this.armorEquipForCharacter.GetComponent<UITexture>().mainTexture = this.allArmorEquipForCharacter[1];
		}
		else if (GrowthManagerKit.GetCurSettedArmorInfo().mName == "HeadNBodyArmor_1")
		{
			this.armorEquipForCharacter.GetComponent<UITexture>().mainTexture = this.allArmorEquipForCharacter[2];
		}
	}

	// Token: 0x0400199E RID: 6558
	public int curSelectedArmorIndex;

	// Token: 0x0400199F RID: 6559
	private int preSelectedArmorIndex = -1;

	// Token: 0x040019A0 RID: 6560
	public UILabel scaleOffLabel;

	// Token: 0x040019A1 RID: 6561
	public UIToggle ArmorUIToggle;

	// Token: 0x040019A2 RID: 6562
	private GArmorItemInfo curArmorInfo;

	// Token: 0x040019A3 RID: 6563
	public UILabel ArmorNameLabel;

	// Token: 0x040019A4 RID: 6564
	public UILabel DesLabel;

	// Token: 0x040019A5 RID: 6565
	public UILabel RestTimeLabel;

	// Token: 0x040019A6 RID: 6566
	private int[] armorFillTimeLevel;

	// Token: 0x040019A7 RID: 6567
	private const int limitToBuyTime = 172800;

	// Token: 0x040019A8 RID: 6568
	private int restTime;

	// Token: 0x040019A9 RID: 6569
	public UISprite armorPriceType;

	// Token: 0x040019AA RID: 6570
	public UILabel armorPriceNumLabel;

	// Token: 0x040019AB RID: 6571
	private int armorPurchasedTime;

	// Token: 0x040019AC RID: 6572
	private int armorPriceNum;

	// Token: 0x040019AD RID: 6573
	private string purchaseType = "Gem";

	// Token: 0x040019AE RID: 6574
	public GameObject equipButton;

	// Token: 0x040019AF RID: 6575
	public GameObject equipedLabel;

	// Token: 0x040019B0 RID: 6576
	public GameObject armorEquipForCharacter;

	// Token: 0x040019B1 RID: 6577
	public Texture[] allArmorEquipForCharacter;
}
