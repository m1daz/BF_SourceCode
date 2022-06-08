using System;
using UnityEngine;

// Token: 0x02000310 RID: 784
public class UINewStoreWeaponWindowEvent : MonoBehaviour
{
	// Token: 0x06001814 RID: 6164 RVA: 0x000CB76F File Offset: 0x000C9B6F
	private void Start()
	{
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x000CB771 File Offset: 0x000C9B71
	private void Update()
	{
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x000CB774 File Offset: 0x000C9B74
	private void OnClick()
	{
		if (UINewStoreWeaponWindowDirector.mInstance == null)
		{
			return;
		}
		switch (this.btnName)
		{
		case UINewStoreWeaponWindowEvent.ButtonName.MeeleButton:
			UINewStoreWeaponWindowDirector.mInstance.MeeleBtnPressed();
			break;
		case UINewStoreWeaponWindowEvent.ButtonName.DeagleButton:
			UINewStoreWeaponWindowDirector.mInstance.DeagleBtnPressed();
			break;
		case UINewStoreWeaponWindowEvent.ButtonName.RifleButton:
			UINewStoreWeaponWindowDirector.mInstance.RifleBtnPressed();
			break;
		case UINewStoreWeaponWindowEvent.ButtonName.MachineGunButton:
			UINewStoreWeaponWindowDirector.mInstance.MachineGunBtnPressed();
			break;
		case UINewStoreWeaponWindowEvent.ButtonName.GrenadeButton:
			UINewStoreWeaponWindowDirector.mInstance.GrenadeBtnPressed();
			break;
		case UINewStoreWeaponWindowEvent.ButtonName.SniperButton:
			UINewStoreWeaponWindowDirector.mInstance.SniperBtnPressed();
			break;
		case UINewStoreWeaponWindowEvent.ButtonName.SpecialButton:
			UINewStoreWeaponWindowDirector.mInstance.SpecialBtnPressed();
			break;
		}
	}

	// Token: 0x04001B92 RID: 7058
	public UINewStoreWeaponWindowEvent.ButtonName btnName;

	// Token: 0x02000311 RID: 785
	public enum ButtonName
	{
		// Token: 0x04001B94 RID: 7060
		Nil,
		// Token: 0x04001B95 RID: 7061
		MeeleButton,
		// Token: 0x04001B96 RID: 7062
		DeagleButton,
		// Token: 0x04001B97 RID: 7063
		RifleButton,
		// Token: 0x04001B98 RID: 7064
		MachineGunButton,
		// Token: 0x04001B99 RID: 7065
		GrenadeButton,
		// Token: 0x04001B9A RID: 7066
		SniperButton,
		// Token: 0x04001B9B RID: 7067
		SpecialButton
	}
}
