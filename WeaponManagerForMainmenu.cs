using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000256 RID: 598
public class WeaponManagerForMainmenu : MonoBehaviour
{
	// Token: 0x06001136 RID: 4406 RVA: 0x000995D8 File Offset: 0x000979D8
	private void Awake()
	{
		for (int i = 0; i < this.allWeapons.Count; i++)
		{
			this.allWeapons[i].SetActiveRecursively(false);
		}
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x00099613 File Offset: 0x00097A13
	private void Update()
	{
		if ((double)Time.timeScale < 0.01)
		{
			return;
		}
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x0009962A File Offset: 0x00097A2A
	public void SwitchWeaponOnline(int tmpIndex)
	{
		this.SwitchWeapons(this.allWeapons[this.index], this.allWeapons[tmpIndex]);
		this.index = tmpIndex;
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x00099658 File Offset: 0x00097A58
	public void SwitchWeapons(GameObject currentWeapon, GameObject nextWeapon)
	{
		currentWeapon.SetActiveRecursively(false);
		nextWeapon.SetActiveRecursively(true);
		if (nextWeapon.name == "DualPistol")
		{
			this.weaponBodyDualPistol_2.SetActive(true);
		}
		else
		{
			this.weaponBodyDualPistol_2.SetActive(false);
		}
	}

	// Token: 0x040013D7 RID: 5079
	public List<GameObject> allWeapons;

	// Token: 0x040013D8 RID: 5080
	private float SwitchTime = 0.5f;

	// Token: 0x040013D9 RID: 5081
	public int index;

	// Token: 0x040013DA RID: 5082
	public GameObject weaponBodyDualPistol_2;
}
