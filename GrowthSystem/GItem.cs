using System;

namespace GrowthSystem
{
	// Token: 0x020001A3 RID: 419
	public class GItem
	{
		// Token: 0x06000C0C RID: 3084 RVA: 0x00056056 File Offset: 0x00054456
		public GItem(GItemId id, int price, GItemPurchaseType purchasedType, int unlockLevel)
		{
			this.mId = id;
			this.mPrice = price;
			this.mPurchasedType = purchasedType;
			this.mUnlockLevel = unlockLevel;
		}

		// Token: 0x04000B72 RID: 2930
		public GItemId mId;

		// Token: 0x04000B73 RID: 2931
		public int mPrice;

		// Token: 0x04000B74 RID: 2932
		public GItemPurchaseType mPurchasedType;

		// Token: 0x04000B75 RID: 2933
		public int mUnlockLevel;
	}
}
