using System;
using UnityEngine;

// Token: 0x020002F5 RID: 757
public class UINewStoreDecoWindowEvent : MonoBehaviour
{
	// Token: 0x06001760 RID: 5984 RVA: 0x000C5F42 File Offset: 0x000C4342
	private void Start()
	{
	}

	// Token: 0x06001761 RID: 5985 RVA: 0x000C5F44 File Offset: 0x000C4344
	private void Update()
	{
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x000C5F48 File Offset: 0x000C4348
	private void OnClick()
	{
		if (UINewStoreDecoWindowDirector.mInstance == null)
		{
			return;
		}
		switch (this.btnName)
		{
		case UINewStoreDecoWindowEvent.ButtonName.SkinButton:
			UINewStoreDecoWindowDirector.mInstance.SkinBtnPressed();
			break;
		case UINewStoreDecoWindowEvent.ButtonName.HatButton:
			UINewStoreDecoWindowDirector.mInstance.HatBtnPressed();
			break;
		case UINewStoreDecoWindowEvent.ButtonName.CapeButton:
			UINewStoreDecoWindowDirector.mInstance.CapeBtnPressed();
			break;
		case UINewStoreDecoWindowEvent.ButtonName.ShoeButton:
			UINewStoreDecoWindowDirector.mInstance.ShoeBtnPressed();
			break;
		case UINewStoreDecoWindowEvent.ButtonName.SkinShareButton:
			UINewStoreDecoWindowDirector.mInstance.SkinShareBtnPressed();
			break;
		}
	}

	// Token: 0x04001A62 RID: 6754
	public UINewStoreDecoWindowEvent.ButtonName btnName;

	// Token: 0x020002F6 RID: 758
	public enum ButtonName
	{
		// Token: 0x04001A64 RID: 6756
		Nil,
		// Token: 0x04001A65 RID: 6757
		SkinButton,
		// Token: 0x04001A66 RID: 6758
		HatButton,
		// Token: 0x04001A67 RID: 6759
		CapeButton,
		// Token: 0x04001A68 RID: 6760
		ShoeButton,
		// Token: 0x04001A69 RID: 6761
		SkinShareButton
	}
}
