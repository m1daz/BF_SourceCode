using System;
using System.Collections.Generic;

// Token: 0x0200017F RID: 383
[Serializable]
public class GHatItemInfo
{
	// Token: 0x06000ACD RID: 2765 RVA: 0x0004ECCF File Offset: 0x0004D0CF
	public void Enable()
	{
		UserDataController.EnableHat(this.mName);
		this.RefreshInfo();
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x0004ECE3 File Offset: 0x0004D0E3
	private void RefreshInfo()
	{
		this.mIsEnabled = true;
	}

	// Token: 0x04000A30 RID: 2608
	public GItemId mId;

	// Token: 0x04000A31 RID: 2609
	public string mName;

	// Token: 0x04000A32 RID: 2610
	public GItemPurchaseType mPurchasedType;

	// Token: 0x04000A33 RID: 2611
	public int mUnlockCLevel;

	// Token: 0x04000A34 RID: 2612
	public int mPrice;

	// Token: 0x04000A35 RID: 2613
	public int mFixPrice;

	// Token: 0x04000A36 RID: 2614
	public string mNameDisplay;

	// Token: 0x04000A37 RID: 2615
	public string mLogoSpriteName;

	// Token: 0x04000A38 RID: 2616
	public bool mIsEnabled;

	// Token: 0x04000A39 RID: 2617
	public string mDescription;

	// Token: 0x04000A3A RID: 2618
	public int mOffRate = 100;

	// Token: 0x04000A3B RID: 2619
	public string mOffRateDescription = string.Empty;

	// Token: 0x04000A3C RID: 2620
	public bool mSellable = true;

	// Token: 0x04000A3D RID: 2621
	public string mPurchaseTipsText = string.Empty;

	// Token: 0x04000A3E RID: 2622
	public List<EnchantmentDetails> mEnchantmentDetails = new List<EnchantmentDetails>();

	// Token: 0x04000A3F RID: 2623
	public string mEnchantmentDescription;
}
