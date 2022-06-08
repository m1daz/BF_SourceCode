using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200027B RID: 635
public class WeaponManagerForSingleEnemy : MonoBehaviour
{
	// Token: 0x0600120D RID: 4621 RVA: 0x000A36FC File Offset: 0x000A1AFC
	private void Awake()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActiveRecursively(false);
		}
		for (int j = 0; j < this.allWeapons.Count; j++)
		{
			this.allWeapons[j].gameObject.SetActiveRecursively(false);
		}
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x000A376F File Offset: 0x000A1B6F
	private void Update()
	{
		if ((double)Time.timeScale < 0.01)
		{
			return;
		}
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x000A3788 File Offset: 0x000A1B88
	public void SwitchWeaponSingleEnemy(int tmpIndex)
	{
		if (this.allWeapons.Count < 2)
		{
			return;
		}
		this.SwitchWeapons(this.allWeapons[this.index].gameObject, this.allWeapons[tmpIndex].gameObject);
		this.index = tmpIndex;
		WeaponSynForSingleModeEnemy weaponSynForSingleModeEnemy = this.allWeapons[tmpIndex];
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x000A37E8 File Offset: 0x000A1BE8
	public void SwitchWeapons(GameObject currentWeapon, GameObject nextWeapon)
	{
		currentWeapon.SetActiveRecursively(false);
		nextWeapon.SetActiveRecursively(true);
	}

	// Token: 0x040014E9 RID: 5353
	public List<WeaponSynForSingleModeEnemy> allWeapons;

	// Token: 0x040014EA RID: 5354
	private float SwitchTime = 0.5f;

	// Token: 0x040014EB RID: 5355
	public int index;

	// Token: 0x040014EC RID: 5356
	private bool canSwitch;
}
