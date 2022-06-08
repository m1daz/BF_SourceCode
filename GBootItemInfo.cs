using System;
using System.Collections.Generic;

// Token: 0x02000181 RID: 385
[Serializable]
public class GBootItemInfo
{
	// Token: 0x06000AD3 RID: 2771 RVA: 0x0004ED79 File Offset: 0x0004D179
	public void Enable()
	{
		UserDataController.EnableBoot(this.mName);
		this.RefreshInfo();
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0004ED8D File Offset: 0x0004D18D
	private void RefreshInfo()
	{
		this.mIsEnabled = true;
	}

	// Token: 0x04000A50 RID: 2640
	public GItemId mId;

	// Token: 0x04000A51 RID: 2641
	public string mName;

	// Token: 0x04000A52 RID: 2642
	public GItemPurchaseType mPurchasedType;

	// Token: 0x04000A53 RID: 2643
	public int mUnlockCLevel;

	// Token: 0x04000A54 RID: 2644
	public int mPrice;

	// Token: 0x04000A55 RID: 2645
	public int mFixPrice;

	// Token: 0x04000A56 RID: 2646
	public string mNameDisplay;

	// Token: 0x04000A57 RID: 2647
	public string mLogoSpriteName;

	// Token: 0x04000A58 RID: 2648
	public bool mIsEnabled;

	// Token: 0x04000A59 RID: 2649
	public string mDescription;

	// Token: 0x04000A5A RID: 2650
	public int mOffRate = 100;

	// Token: 0x04000A5B RID: 2651
	public string mOffRateDescription = string.Empty;

	// Token: 0x04000A5C RID: 2652
	public bool mSellable = true;

	// Token: 0x04000A5D RID: 2653
	public string mPurchaseTipsText = string.Empty;

	// Token: 0x04000A5E RID: 2654
	public List<EnchantmentDetails> mEnchantmentDetails = new List<EnchantmentDetails>();

	// Token: 0x04000A5F RID: 2655
	public string mEnchantmentDescription;
}
