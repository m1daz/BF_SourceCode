using System;
using UnityEngine;

// Token: 0x020002FC RID: 764
public class UINewStorePurchaseWindowEvent : MonoBehaviour
{
	// Token: 0x06001792 RID: 6034 RVA: 0x000C6FAE File Offset: 0x000C53AE
	private void Start()
	{
	}

	// Token: 0x06001793 RID: 6035 RVA: 0x000C6FB0 File Offset: 0x000C53B0
	private void Update()
	{
	}

	// Token: 0x06001794 RID: 6036 RVA: 0x000C6FB4 File Offset: 0x000C53B4
	private void OnClick()
	{
		if (UINewStoreGemAndCoinPurchaseWindowDirector.mInstance == null)
		{
			return;
		}
		UINewStorePurchaseWindowEvent.ButtonName buttonName = this.btnName;
		if (buttonName != UINewStorePurchaseWindowEvent.ButtonName.PurchaseButton)
		{
			if (buttonName == UINewStorePurchaseWindowEvent.ButtonName.ExchangeButton)
			{
				UINewStoreGemAndCoinPurchaseWindowDirector.mInstance.ExchangeBtnPressed();
			}
		}
		else
		{
			UINewStoreGemAndCoinPurchaseWindowDirector.mInstance.PurchaseBtnPressed();
		}
	}

	// Token: 0x04001A8F RID: 6799
	public UINewStorePurchaseWindowEvent.ButtonName btnName;

	// Token: 0x020002FD RID: 765
	public enum ButtonName
	{
		// Token: 0x04001A91 RID: 6801
		Nil,
		// Token: 0x04001A92 RID: 6802
		PurchaseButton,
		// Token: 0x04001A93 RID: 6803
		ExchangeButton
	}
}
