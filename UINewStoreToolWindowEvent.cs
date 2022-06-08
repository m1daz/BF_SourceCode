using System;
using UnityEngine;

// Token: 0x02000309 RID: 777
public class UINewStoreToolWindowEvent : MonoBehaviour
{
	// Token: 0x060017D5 RID: 6101 RVA: 0x000C8C7F File Offset: 0x000C707F
	private void Start()
	{
	}

	// Token: 0x060017D6 RID: 6102 RVA: 0x000C8C81 File Offset: 0x000C7081
	private void Update()
	{
	}

	// Token: 0x060017D7 RID: 6103 RVA: 0x000C8C84 File Offset: 0x000C7084
	private void OnClick()
	{
		if (UINewStoreToolWindowDirector.mInstance == null)
		{
			return;
		}
		switch (this.btnName)
		{
		case UINewStoreToolWindowEvent.ButtonName.PotionButton:
			UINewStoreToolWindowDirector.mInstance.PotionBtnPressed();
			break;
		case UINewStoreToolWindowEvent.ButtonName.KeyButton:
			UINewStoreToolWindowDirector.mInstance.KeyBtnPressed();
			break;
		case UINewStoreToolWindowEvent.ButtonName.FireworkButton:
			UINewStoreToolWindowDirector.mInstance.FireworkBtnPressed();
			break;
		case UINewStoreToolWindowEvent.ButtonName.StaffButton:
			UINewStoreToolWindowDirector.mInstance.StaffBtnPressed();
			break;
		case UINewStoreToolWindowEvent.ButtonName.TicketButton:
			UINewStoreToolWindowDirector.mInstance.TicketBtnPressed();
			break;
		}
	}

	// Token: 0x04001B16 RID: 6934
	public UINewStoreToolWindowEvent.ButtonName btnName;

	// Token: 0x0200030A RID: 778
	public enum ButtonName
	{
		// Token: 0x04001B18 RID: 6936
		Nil,
		// Token: 0x04001B19 RID: 6937
		PotionButton,
		// Token: 0x04001B1A RID: 6938
		KeyButton,
		// Token: 0x04001B1B RID: 6939
		FireworkButton,
		// Token: 0x04001B1C RID: 6940
		StaffButton,
		// Token: 0x04001B1D RID: 6941
		TicketButton
	}
}
