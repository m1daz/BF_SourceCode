using System;
using UnityEngine;

// Token: 0x020002F9 RID: 761
public class UINewStoreGemAndCoinPurchaseButtonEvent : MonoBehaviour
{
	// Token: 0x0600177C RID: 6012 RVA: 0x000C6434 File Offset: 0x000C4834
	private void Start()
	{
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x000C6436 File Offset: 0x000C4836
	private void Update()
	{
	}

	// Token: 0x0600177E RID: 6014 RVA: 0x000C6438 File Offset: 0x000C4838
	private void OnClick()
	{
		UINewStoreGemAndCoinPurchaseWindowDirector.mInstance.BuyBtnPressed(this.btnName);
	}

	// Token: 0x04001A72 RID: 6770
	public UINewStoreGemAndCoinPurchaseButtonEvent.ButtonName btnName;

	// Token: 0x020002FA RID: 762
	public enum ButtonName
	{
		// Token: 0x04001A74 RID: 6772
		Nil,
		// Token: 0x04001A75 RID: 6773
		CoinItem,
		// Token: 0x04001A76 RID: 6774
		GemItem1,
		// Token: 0x04001A77 RID: 6775
		GemItem2,
		// Token: 0x04001A78 RID: 6776
		GemItem3,
		// Token: 0x04001A79 RID: 6777
		GemItem4,
		// Token: 0x04001A7A RID: 6778
		GemItem5,
		// Token: 0x04001A7B RID: 6779
		GemItem6,
		// Token: 0x04001A7C RID: 6780
		GrowthPack1 = 11,
		// Token: 0x04001A7D RID: 6781
		GrowthPack2,
		// Token: 0x04001A7E RID: 6782
		GrowthPack3,
		// Token: 0x04001A7F RID: 6783
		CoinExchange1,
		// Token: 0x04001A80 RID: 6784
		CoinExchange2,
		// Token: 0x04001A81 RID: 6785
		CoinExchange3,
		// Token: 0x04001A82 RID: 6786
		GiftBoxSmall,
		// Token: 0x04001A83 RID: 6787
		GiftBoxBig
	}
}
