using System;
using UnityEngine;

// Token: 0x02000224 RID: 548
public class GGWeaponCrosshair : MonoBehaviour
{
	// Token: 0x06000EB1 RID: 3761 RVA: 0x0007AB9F File Offset: 0x00078F9F
	private void Awake()
	{
		this.weaponManager = GameObject.FindWithTag("WeaponManager").GetComponent<GGWeaponManager>();
		this.CrosshairSprite = GameObject.Find("Sprite(CrossHair)");
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x0007ABC8 File Offset: 0x00078FC8
	private void Update()
	{
		if (this.weaponManager && this.weaponManager.SelectedWeapon)
		{
			this.weaponScript = this.weaponManager.SelectedWeapon.GetComponent<GGWeaponScript>();
		}
		if ((double)Time.timeScale < 0.01)
		{
			return;
		}
		if (this.weaponScript)
		{
			if (this.weaponScript.aimed)
			{
				this.CrosshairSprite.SetActive(false);
			}
			else
			{
				this.CrosshairSprite.SetActive(true);
			}
		}
	}

	// Token: 0x04001012 RID: 4114
	private GGWeaponManager weaponManager;

	// Token: 0x04001013 RID: 4115
	private GGWeaponScript weaponScript;

	// Token: 0x04001014 RID: 4116
	private GameObject CrosshairSprite;
}
