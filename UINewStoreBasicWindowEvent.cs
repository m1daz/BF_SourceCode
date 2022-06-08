using System;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class UINewStoreBasicWindowEvent : MonoBehaviour
{
	// Token: 0x060016F9 RID: 5881 RVA: 0x000C2B93 File Offset: 0x000C0F93
	private void Start()
	{
	}

	// Token: 0x060016FA RID: 5882 RVA: 0x000C2B95 File Offset: 0x000C0F95
	private void Update()
	{
	}

	// Token: 0x060016FB RID: 5883 RVA: 0x000C2B98 File Offset: 0x000C0F98
	private void OnClick()
	{
		if (UINewStoreBasicWindowDirector.mInstance == null)
		{
			return;
		}
		switch (this.btnName)
		{
		case UINewStoreBasicWindowEvent.StoreButtonName.WeaponButton:
			UINewStoreBasicWindowDirector.mInstance.WeaponBtnPressed();
			break;
		case UINewStoreBasicWindowEvent.StoreButtonName.ArmorButton:
			UINewStoreBasicWindowDirector.mInstance.ArmorBtnPressed();
			break;
		case UINewStoreBasicWindowEvent.StoreButtonName.DecoButton:
			UINewStoreBasicWindowDirector.mInstance.DecoBtnPressed();
			break;
		case UINewStoreBasicWindowEvent.StoreButtonName.ToolButton:
			UINewStoreBasicWindowDirector.mInstance.ToolBtnPressed();
			break;
		case UINewStoreBasicWindowEvent.StoreButtonName.CoinButton:
			UINewStoreBasicWindowDirector.mInstance.CoinBtnPressed();
			break;
		}
	}

	// Token: 0x040019BF RID: 6591
	public UINewStoreBasicWindowEvent.StoreButtonName btnName;

	// Token: 0x020002E8 RID: 744
	public enum StoreButtonName
	{
		// Token: 0x040019C1 RID: 6593
		Nil,
		// Token: 0x040019C2 RID: 6594
		WeaponButton,
		// Token: 0x040019C3 RID: 6595
		ArmorButton,
		// Token: 0x040019C4 RID: 6596
		DecoButton,
		// Token: 0x040019C5 RID: 6597
		ToolButton,
		// Token: 0x040019C6 RID: 6598
		CoinButton
	}
}
