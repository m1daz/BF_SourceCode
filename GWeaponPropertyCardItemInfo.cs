using System;

// Token: 0x02000197 RID: 407
[Serializable]
public class GWeaponPropertyCardItemInfo
{
	// Token: 0x06000AEA RID: 2794 RVA: 0x0004F374 File Offset: 0x0004D774
	public GWeaponPropertyCardItemInfo(string propertyName, int lv)
	{
		this.mPropertyName = propertyName;
		this.mLv = lv;
		this.mPurchasedType = GItemPurchaseType.CoinsPurchase;
		this.mExistNum = GrowthManagerKit.GetWeaponPropertyCardNum(propertyName, lv);
		this.mNameDisplay = propertyName.ToUpper() + " CARD LV." + lv.ToString();
		this.mLogoSpriteName = propertyName + "_CardLogo_" + lv.ToString();
		this.mDescription = "UPGRADE " + propertyName.ToUpper() + " PROPERTY.";
		if (lv == 1)
		{
			this.mSellable = true;
		}
		else
		{
			this.mDescription = "UPGRADE " + propertyName.ToUpper() + " PROPERTY. GET IT FROM SLOTS OR HUNTING MODE!";
			this.mSellable = false;
		}
		this.mSinglePrice = ((lv != 1) ? ((lv != 2) ? 5000 : 2000) : 500);
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x0004F488 File Offset: 0x0004D888
	public bool Buy(int num)
	{
		if (this.mPurchasedType == GItemPurchaseType.CoinsPurchase)
		{
			if (GrowthManagerKit.GetCoins() >= this.mSinglePrice * num)
			{
				GrowthManagerKit.SubCoins(this.mSinglePrice * num);
				GrowthManagerKit.AddWeaponPropertyCardNum(this.mPropertyName, this.mLv, num);
				return true;
			}
			return false;
		}
		else
		{
			if (this.mPurchasedType != GItemPurchaseType.GemPurchase)
			{
				return false;
			}
			if (GrowthManagerKit.GetGems() >= this.mSinglePrice * num)
			{
				GrowthManagerKit.SubGems(this.mSinglePrice * num);
				GrowthManagerKit.AddWeaponPropertyCardNum(this.mPropertyName, this.mLv, num);
				return true;
			}
			return false;
		}
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x0004F519 File Offset: 0x0004D919
	private void Refresh()
	{
		this.mExistNum = GrowthManagerKit.GetWeaponPropertyCardNum(this.mPropertyName, this.mLv);
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0004F532 File Offset: 0x0004D932
	public void AddCardNum(int num)
	{
		GrowthManagerKit.AddWeaponPropertyCardNum(this.mPropertyName, this.mLv, num);
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0004F546 File Offset: 0x0004D946
	public void SubCardNum(int num)
	{
		GrowthManagerKit.SubWeaponPropertyCardNum(this.mPropertyName, this.mLv, num);
	}

	// Token: 0x04000B1F RID: 2847
	public GItemId mId;

	// Token: 0x04000B20 RID: 2848
	public string mPropertyName;

	// Token: 0x04000B21 RID: 2849
	public int mLv;

	// Token: 0x04000B22 RID: 2850
	public bool mSellable;

	// Token: 0x04000B23 RID: 2851
	public GItemPurchaseType mPurchasedType;

	// Token: 0x04000B24 RID: 2852
	public int mExistNum;

	// Token: 0x04000B25 RID: 2853
	public string mNameDisplay;

	// Token: 0x04000B26 RID: 2854
	public string mLogoSpriteName;

	// Token: 0x04000B27 RID: 2855
	public int mSinglePrice;

	// Token: 0x04000B28 RID: 2856
	public int mOffRate = 100;

	// Token: 0x04000B29 RID: 2857
	public string mOffRateDescription = string.Empty;

	// Token: 0x04000B2A RID: 2858
	public string mDescription = string.Empty;
}
