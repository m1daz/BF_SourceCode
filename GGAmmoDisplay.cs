using System;
using UnityEngine;

// Token: 0x0200020E RID: 526
public class GGAmmoDisplay : MonoBehaviour
{
	// Token: 0x06000E5C RID: 3676 RVA: 0x000776DF File Offset: 0x00075ADF
	private void Awake()
	{
		this.weaponManager = GameObject.FindWithTag("WeaponManager").GetComponent<GGWeaponManager>();
		this.NG_UI = GameObject.Find("UI Root (3D)");
	}

	// Token: 0x06000E5D RID: 3677 RVA: 0x00077708 File Offset: 0x00075B08
	private void Update()
	{
		if (this.weaponManager.SelectedWeapon)
		{
			this.weaponscript = this.weaponManager.SelectedWeapon.GetComponent<GGWeaponScript>();
		}
		if (!this.weaponscript)
		{
			return;
		}
		if (this.weaponscript.GunType == gunType.MachineGun)
		{
			this.bulletsLeft = this.weaponscript._MachineGunbulletsLeft;
			this.clips = this.weaponscript._MachineGunclips;
		}
		if (this.weaponscript.GunType == gunType.ShotGun)
		{
			this.bulletsLeft = this.weaponscript._ShotGunbulletsLeft;
			this.clips = this.weaponscript._ShotGunclips;
		}
		if (this.weaponscript.GunType == gunType.Bazooka)
		{
			this.clips = this.weaponscript._GrenadeLauncherammoCount;
		}
		if (this.currentWeapon != this.weaponManager.SelectedWeapon)
		{
			this.color = Mathf.Lerp(this.color, 0.3f, Time.deltaTime * 20f);
			if (this.color < 0.32f)
			{
				this.currentWeapon = this.weaponManager.SelectedWeapon;
			}
		}
		if (this.weaponscript)
		{
			if (this.weaponscript.GunType != gunType.Knife)
			{
				if (this.weaponscript.GunType == gunType.Bazooka)
				{
					this.NG_UI.SendMessage("receiveBullets", this.clips.ToString(), SendMessageOptions.DontRequireReceiver);
				}
				else if (this.weaponscript.weaponName == "Deagle" || this.weaponscript.weaponName == "GLOCK21")
				{
					this.NG_UI.SendMessage("receiveBullets", this.bulletsLeft + " | NA", SendMessageOptions.DontRequireReceiver);
				}
				else
				{
					this.NG_UI.SendMessage("receiveBullets", this.bulletsLeft + " | " + this.clips, SendMessageOptions.DontRequireReceiver);
				}
			}
			else
			{
				this.NG_UI.SendMessage("receiveBullets", "NA", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x04000F6A RID: 3946
	private bool display = true;

	// Token: 0x04000F6B RID: 3947
	private int bulletsLeft;

	// Token: 0x04000F6C RID: 3948
	private int clips;

	// Token: 0x04000F6D RID: 3949
	private GGWeaponScript weaponscript;

	// Token: 0x04000F6E RID: 3950
	private GGWeaponManager weaponManager;

	// Token: 0x04000F6F RID: 3951
	private GGWeaponScript currentWeapon;

	// Token: 0x04000F70 RID: 3952
	private float color;

	// Token: 0x04000F71 RID: 3953
	private GameObject NG_UI;
}
