using System;
using UnityEngine;

// Token: 0x0200030F RID: 783
public class UINewStoreWeaponWindowDirector : MonoBehaviour
{
	// Token: 0x06001808 RID: 6152 RVA: 0x000CB562 File Offset: 0x000C9962
	private void Awake()
	{
		if (UINewStoreWeaponWindowDirector.mInstance == null)
		{
			UINewStoreWeaponWindowDirector.mInstance = this;
		}
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x000CB57A File Offset: 0x000C997A
	private void OnDestroy()
	{
		if (UINewStoreWeaponWindowDirector.mInstance != null)
		{
			UINewStoreWeaponWindowDirector.mInstance = null;
		}
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x000CB592 File Offset: 0x000C9992
	private void Start()
	{
	}

	// Token: 0x0600180B RID: 6155 RVA: 0x000CB594 File Offset: 0x000C9994
	private void Update()
	{
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x000CB598 File Offset: 0x000C9998
	public void MeeleBtnPressed()
	{
		this.weaponScrollViewInstiate.curWeaponCategoryIndex = 0;
		if (this.weaponScrollViewInstiate.curWeaponCategoryIndex != this.weaponScrollViewInstiate.preWeaponCategoryIndex)
		{
			this.weaponScrollViewInstiate.RefreshWeaponDisplay();
		}
		this.weaponScrollViewInstiate.WeaponPlusWindowDontShow();
		this.weaponScrollViewInstiate.BackToMeeleUIToggle();
	}

	// Token: 0x0600180D RID: 6157 RVA: 0x000CB5ED File Offset: 0x000C99ED
	public void DeagleBtnPressed()
	{
		this.weaponScrollViewInstiate.curWeaponCategoryIndex = 1;
		if (this.weaponScrollViewInstiate.curWeaponCategoryIndex != this.weaponScrollViewInstiate.preWeaponCategoryIndex)
		{
			this.weaponScrollViewInstiate.RefreshWeaponDisplay();
		}
		this.weaponScrollViewInstiate.WeaponPlusWindowDontShow();
	}

	// Token: 0x0600180E RID: 6158 RVA: 0x000CB62C File Offset: 0x000C9A2C
	public void MachineGunBtnPressed()
	{
		this.weaponScrollViewInstiate.curWeaponCategoryIndex = 2;
		if (this.weaponScrollViewInstiate.curWeaponCategoryIndex != this.weaponScrollViewInstiate.preWeaponCategoryIndex)
		{
			this.weaponScrollViewInstiate.RefreshWeaponDisplay();
		}
		this.weaponScrollViewInstiate.WeaponPlusWindowDontShow();
	}

	// Token: 0x0600180F RID: 6159 RVA: 0x000CB66B File Offset: 0x000C9A6B
	public void RifleBtnPressed()
	{
		this.weaponScrollViewInstiate.curWeaponCategoryIndex = 3;
		if (this.weaponScrollViewInstiate.curWeaponCategoryIndex != this.weaponScrollViewInstiate.preWeaponCategoryIndex)
		{
			this.weaponScrollViewInstiate.RefreshWeaponDisplay();
		}
		this.weaponScrollViewInstiate.WeaponPlusWindowDontShow();
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x000CB6AA File Offset: 0x000C9AAA
	public void SniperBtnPressed()
	{
		this.weaponScrollViewInstiate.curWeaponCategoryIndex = 4;
		if (this.weaponScrollViewInstiate.curWeaponCategoryIndex != this.weaponScrollViewInstiate.preWeaponCategoryIndex)
		{
			this.weaponScrollViewInstiate.RefreshWeaponDisplay();
		}
		this.weaponScrollViewInstiate.WeaponPlusWindowDontShow();
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x000CB6E9 File Offset: 0x000C9AE9
	public void SpecialBtnPressed()
	{
		this.weaponScrollViewInstiate.curWeaponCategoryIndex = 5;
		if (this.weaponScrollViewInstiate.curWeaponCategoryIndex != this.weaponScrollViewInstiate.preWeaponCategoryIndex)
		{
			this.weaponScrollViewInstiate.RefreshWeaponDisplay();
		}
		this.weaponScrollViewInstiate.WeaponPlusWindowDontShow();
	}

	// Token: 0x06001812 RID: 6162 RVA: 0x000CB728 File Offset: 0x000C9B28
	public void GrenadeBtnPressed()
	{
		this.weaponScrollViewInstiate.curWeaponCategoryIndex = 6;
		if (this.weaponScrollViewInstiate.curWeaponCategoryIndex != this.weaponScrollViewInstiate.preWeaponCategoryIndex)
		{
			this.weaponScrollViewInstiate.RefreshWeaponDisplay();
		}
		this.weaponScrollViewInstiate.WeaponPlusWindowDontShow();
	}

	// Token: 0x04001B90 RID: 7056
	public static UINewStoreWeaponWindowDirector mInstance;

	// Token: 0x04001B91 RID: 7057
	public UINewStoreWeaponPrefabInstiate weaponScrollViewInstiate;
}
