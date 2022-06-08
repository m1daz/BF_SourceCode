using System;
using UnityEngine;

// Token: 0x020002B8 RID: 696
public class UITouchMonitor : MonoBehaviour
{
	// Token: 0x06001453 RID: 5203 RVA: 0x000B131C File Offset: 0x000AF71C
	private void Start()
	{
	}

	// Token: 0x06001454 RID: 5204 RVA: 0x000B1320 File Offset: 0x000AF720
	private void OnPress()
	{
		switch (this.uiName)
		{
		case UITouchMonitor.UIName.Fire:
			if (this.isFirePressing)
			{
				this.isFirePressing = false;
				UIPlayDirector.mInstance.FireOnPress();
			}
			else
			{
				this.isFirePressing = true;
				UIPlayDirector.mInstance.FireOnRelease();
			}
			break;
		case UITouchMonitor.UIName.InstallBomb:
			if (this.isPressing)
			{
				this.isPressing = false;
				UIPlayDirector.mInstance.InstallBtnPressed();
			}
			else
			{
				this.isPressing = true;
				UIPlayDirector.mInstance.InstallBtnReleased();
			}
			break;
		case UITouchMonitor.UIName.UninstallBomb:
			if (this.isPressing)
			{
				this.isPressing = false;
				UIPlayDirector.mInstance.UninstallBtnPressed();
			}
			else
			{
				this.isPressing = true;
				UIPlayDirector.mInstance.UninstallBtnReleased();
			}
			break;
		}
	}

	// Token: 0x0400173D RID: 5949
	public UITouchMonitor.UIName uiName;

	// Token: 0x0400173E RID: 5950
	public bool isFirePressing = true;

	// Token: 0x0400173F RID: 5951
	public bool isPressing = true;

	// Token: 0x020002B9 RID: 697
	public enum UIName
	{
		// Token: 0x04001741 RID: 5953
		Nil,
		// Token: 0x04001742 RID: 5954
		Fire,
		// Token: 0x04001743 RID: 5955
		InstallBomb,
		// Token: 0x04001744 RID: 5956
		UninstallBomb
	}
}
