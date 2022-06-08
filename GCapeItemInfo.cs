using System;
using System.Collections.Generic;

// Token: 0x02000180 RID: 384
[Serializable]
public class GCapeItemInfo
{
	// Token: 0x06000AD0 RID: 2768 RVA: 0x0004ED24 File Offset: 0x0004D124
	public void Enable()
	{
		UserDataController.EnableCape(this.mName);
		this.RefreshInfo();
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x0004ED38 File Offset: 0x0004D138
	private void RefreshInfo()
	{
		this.mIsEnabled = true;
	}

	// Token: 0x04000A40 RID: 2624
	public GItemId mId;

	// Token: 0x04000A41 RID: 2625
	public string mName;

	// Token: 0x04000A42 RID: 2626
	public GItemPurchaseType mPurchasedType;

	// Token: 0x04000A43 RID: 2627
	public int mUnlockCLevel;

	// Token: 0x04000A44 RID: 2628
	public int mPrice;

	// Token: 0x04000A45 RID: 2629
	public int mFixPrice;

	// Token: 0x04000A46 RID: 2630
	public string mNameDisplay;

	// Token: 0x04000A47 RID: 2631
	public string mLogoSpriteName;

	// Token: 0x04000A48 RID: 2632
	public bool mIsEnabled;

	// Token: 0x04000A49 RID: 2633
	public string mDescription;

	// Token: 0x04000A4A RID: 2634
	public int mOffRate = 100;

	// Token: 0x04000A4B RID: 2635
	public string mOffRateDescription = string.Empty;

	// Token: 0x04000A4C RID: 2636
	public bool mSellable = true;

	// Token: 0x04000A4D RID: 2637
	public string mPurchaseTipsText = string.Empty;

	// Token: 0x04000A4E RID: 2638
	public List<EnchantmentDetails> mEnchantmentDetails = new List<EnchantmentDetails>();

	// Token: 0x04000A4F RID: 2639
	public string mEnchantmentDescription;
}
