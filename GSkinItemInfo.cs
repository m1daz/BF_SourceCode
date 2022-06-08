using System;

// Token: 0x0200017E RID: 382
[Serializable]
public class GSkinItemInfo
{
	// Token: 0x06000AC9 RID: 2761 RVA: 0x0004EC6D File Offset: 0x0004D06D
	public void Enable()
	{
		UserDataController.EnableSkin(this.mName);
		this.RefreshInfo();
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0004EC81 File Offset: 0x0004D081
	public void Disable()
	{
		UserDataController.DisableSkin(this.mName);
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x0004EC8E File Offset: 0x0004D08E
	private void RefreshInfo()
	{
		this.mIsEnabled = true;
	}

	// Token: 0x04000A1F RID: 2591
	public GItemId mId;

	// Token: 0x04000A20 RID: 2592
	public string mName;

	// Token: 0x04000A21 RID: 2593
	public GItemPurchaseType mPurchasedType;

	// Token: 0x04000A22 RID: 2594
	public int mUnlockCLevel;

	// Token: 0x04000A23 RID: 2595
	public int mPrice;

	// Token: 0x04000A24 RID: 2596
	public int mFixPrice;

	// Token: 0x04000A25 RID: 2597
	public string mNameDisplay;

	// Token: 0x04000A26 RID: 2598
	public string mLogoSpriteName;

	// Token: 0x04000A27 RID: 2599
	public bool mIsEnabled;

	// Token: 0x04000A28 RID: 2600
	public string mHeadMaterialName;

	// Token: 0x04000A29 RID: 2601
	public string mBodyMaterialName;

	// Token: 0x04000A2A RID: 2602
	public string mHandMaterialName;

	// Token: 0x04000A2B RID: 2603
	public string mDescription;

	// Token: 0x04000A2C RID: 2604
	public int mOffRate = 100;

	// Token: 0x04000A2D RID: 2605
	public string mOffRateDescription = string.Empty;

	// Token: 0x04000A2E RID: 2606
	public bool mSellable = true;

	// Token: 0x04000A2F RID: 2607
	public string mPurchaseTipsText = string.Empty;
}
