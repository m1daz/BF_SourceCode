using System;
using UnityEngine;

// Token: 0x02000211 RID: 529
public class GGBulletPickUp : MonoBehaviour
{
	// Token: 0x06000E65 RID: 3685 RVA: 0x00078E5A File Offset: 0x0007725A
	private void Awake()
	{
		this.weaponManager = GameObject.FindWithTag("WeaponManager").GetComponent<GGWeaponManager>();
		this.AudioProcX = base.transform.Find("Audio").gameObject;
	}

	// Token: 0x06000E66 RID: 3686 RVA: 0x00078E8C File Offset: 0x0007728C
	private void OnTriggerEnter(Collider other)
	{
		if (this.weaponManager.SelectedWeapon)
		{
			this.weaponscript = this.weaponManager.SelectedWeapon.GetComponent<GGWeaponScript>();
		}
		if (!this.weaponscript)
		{
			return;
		}
		if (other.gameObject.tag == "Player")
		{
			if (this.AudioProcX != null)
			{
				this.AudioProcX.GetComponent<AudioSource>().Play();
			}
			string weaponName = this.weaponscript.weaponName;
			switch (weaponName)
			{
			case "Deagle":
				this.weaponscript._MachineGunclips += 10;
				this.weaponscript.isReload = false;
				break;
			case "G36K":
				this.weaponscript._MachineGunclips += 40;
				this.weaponscript.isReload = false;
				break;
			case "GLOCK21":
				this.weaponscript._MachineGunclips += 20;
				this.weaponscript.isReload = false;
				break;
			case "M67":
				this.weaponscript._GrenadeLauncherammoCount += 5;
				this.weaponscript.isReload = false;
				break;
			case "M87T":
				this.weaponscript._ShotGunclips += 10;
				this.weaponscript.isReload = false;
				break;
			case "MP5KA4":
				this.weaponscript._MachineGunclips += 40;
				this.weaponscript.isReload = false;
				break;
			case "MP5KA5":
				this.weaponscript._MachineGunclips += 50;
				this.weaponscript.isReload = false;
				break;
			case "RPG":
				this.weaponscript._GrenadeLauncherammoCount += 5;
				this.weaponscript.isReload = false;
				break;
			case "Blaser R93":
				this.weaponscript._MachineGunclips += 5;
				this.weaponscript.isReload = false;
				break;
			case "STW-25":
				this.weaponscript._MachineGunclips += 50;
				this.weaponscript.isReload = false;
				break;
			case "UZI":
				this.weaponscript._MachineGunclips += 60;
				this.weaponscript.isReload = false;
				break;
			case "M249":
				this.weaponscript._MachineGunclips += 100;
				this.weaponscript.isReload = false;
				break;
			case "MilkBomb":
				this.weaponscript._GrenadeLauncherammoCount += 5;
				this.weaponscript.isReload = false;
				break;
			case "CandyRifle":
				this.weaponscript._MachineGunclips += 50;
				this.weaponscript.isReload = false;
				break;
			case "ChristmasSniper":
				this.weaponscript._MachineGunclips += 50;
				this.weaponscript.isReload = false;
				break;
			case "SantaGun":
				this.weaponscript._MachineGunclips += 50;
				this.weaponscript.isReload = false;
				break;
			case "GingerbreadBomb":
				this.weaponscript._GrenadeLauncherammoCount += 5;
				this.weaponscript.isReload = false;
				break;
			case "AUG":
				this.weaponscript._MachineGunclips += 40;
				this.weaponscript.isReload = false;
				break;
			case "M3":
				this.weaponscript._ShotGunclips += 7;
				this.weaponscript.isReload = false;
				break;
			}
			base.transform.position -= new Vector3(0f, -20f, 0f);
		}
	}

	// Token: 0x06000E67 RID: 3687 RVA: 0x0007937D File Offset: 0x0007777D
	private void Update()
	{
	}

	// Token: 0x04000F97 RID: 3991
	private int bulletsLeft;

	// Token: 0x04000F98 RID: 3992
	private int clips;

	// Token: 0x04000F99 RID: 3993
	private GGWeaponScript weaponscript;

	// Token: 0x04000F9A RID: 3994
	private GGWeaponManager weaponManager;

	// Token: 0x04000F9B RID: 3995
	private GGWeaponScript currentWeapon;

	// Token: 0x04000F9C RID: 3996
	private float color;

	// Token: 0x04000F9D RID: 3997
	private GameObject AudioProcX;
}
