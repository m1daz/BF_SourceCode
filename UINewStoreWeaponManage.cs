using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200030C RID: 780
public class UINewStoreWeaponManage : MonoBehaviour
{
	// Token: 0x060017DD RID: 6109 RVA: 0x000C8D3C File Offset: 0x000C713C
	private void Awake()
	{
		for (int i = 0; i < this.allWeapons.Count; i++)
		{
			this.allWeapons[i].SetActive(false);
		}
	}

	// Token: 0x060017DE RID: 6110 RVA: 0x000C8D77 File Offset: 0x000C7177
	private void Update()
	{
		if ((double)Time.timeScale < 0.01)
		{
			return;
		}
	}

	// Token: 0x060017DF RID: 6111 RVA: 0x000C8D8E File Offset: 0x000C718E
	public void SwitchWeaponOnline(int tmpIndex)
	{
		this.SwitchWeapons(this.allWeapons[this.index], this.allWeapons[tmpIndex]);
		this.index = tmpIndex;
	}

	// Token: 0x060017E0 RID: 6112 RVA: 0x000C8DBC File Offset: 0x000C71BC
	public void SwitchWeapons(GameObject currentWeapon, GameObject nextWeapon)
	{
		currentWeapon.SetActive(false);
		nextWeapon.SetActive(true);
		if (nextWeapon.name == "DualPistol")
		{
			this.weaponBodyDualPistol_2.SetActive(true);
		}
		else
		{
			this.weaponBodyDualPistol_2.SetActive(false);
		}
	}

	// Token: 0x060017E1 RID: 6113 RVA: 0x000C8E0C File Offset: 0x000C720C
	public void WeaponUpgrade(int weaponIndex, int upgradeLv)
	{
		this.allWeaponsUpgradeEffectObj[weaponIndex].GetComponent<Renderer>().enabled = true;
		if (weaponIndex != 57)
		{
			this.allWeaponsUpgradeEffectObj[weaponIndex].GetComponent<Renderer>().material = this.upgradeMaterial[upgradeLv - 1];
		}
		else
		{
			this.allWeaponsUpgradeEffectObj[weaponIndex].GetComponent<Renderer>().material = (Resources.Load("Original Resources/Weapons/Materials/WeaponUpgradeLv_" + upgradeLv.ToString() + "_Nightmare") as Material);
		}
		Forcefield component = this.allWeaponsUpgradeEffectObj[weaponIndex].GetComponent<Forcefield>();
		component.enabled = false;
		component.mat = this.allWeaponsUpgradeEffectObj[weaponIndex].GetComponent<Renderer>().material;
		component.enabled = true;
		if (weaponIndex == 49)
		{
			this.weaponBodyUpgradeEffectObj_DualPistol_2.GetComponent<Renderer>().enabled = true;
			this.weaponBodyUpgradeEffectObj_DualPistol_2.GetComponent<Renderer>().material = this.upgradeMaterial[upgradeLv - 1];
			Forcefield component2 = this.weaponBodyUpgradeEffectObj_DualPistol_2.GetComponent<Forcefield>();
			component2.enabled = false;
			component2.mat = this.weaponBodyUpgradeEffectObj_DualPistol_2.GetComponent<Renderer>().material;
			component2.enabled = true;
		}
		else if (weaponIndex == 21)
		{
			this.weaponBodyUpgradeEffect_M134_2.GetComponent<Renderer>().enabled = true;
			this.weaponBodyUpgradeEffect_M134_2.GetComponent<Renderer>().material = this.upgradeMaterial[upgradeLv - 1];
			Forcefield component3 = this.weaponBodyUpgradeEffect_M134_2.GetComponent<Forcefield>();
			component3.enabled = false;
			component3.mat = this.weaponBodyUpgradeEffect_M134_2.GetComponent<Renderer>().material;
			component3.enabled = true;
		}
		else if (weaponIndex == 30)
		{
			this.weaponBodyUpgradeEffect_SM134_2.GetComponent<Renderer>().enabled = true;
			this.weaponBodyUpgradeEffect_SM134_2.GetComponent<Renderer>().material = this.upgradeMaterial[upgradeLv - 1];
			Forcefield component4 = this.weaponBodyUpgradeEffect_SM134_2.GetComponent<Forcefield>();
			component4.enabled = false;
			component4.mat = this.weaponBodyUpgradeEffect_SM134_2.GetComponent<Renderer>().material;
			component4.enabled = true;
		}
		else if (weaponIndex == 54)
		{
			this.weaponBodyUpgradeEffect_HM134_2.GetComponent<Renderer>().enabled = true;
			this.weaponBodyUpgradeEffect_HM134_2.GetComponent<Renderer>().material = this.upgradeMaterial[upgradeLv - 1];
			Forcefield component5 = this.weaponBodyUpgradeEffect_HM134_2.GetComponent<Forcefield>();
			component5.enabled = false;
			component5.mat = this.weaponBodyUpgradeEffect_HM134_2.GetComponent<Renderer>().material;
			component5.enabled = true;
		}
	}

	// Token: 0x04001B1F RID: 6943
	public List<GameObject> allWeapons;

	// Token: 0x04001B20 RID: 6944
	public List<GameObject> allWeaponsUpgradeEffectObj;

	// Token: 0x04001B21 RID: 6945
	public Material[] upgradeMaterial;

	// Token: 0x04001B22 RID: 6946
	private float SwitchTime = 0.5f;

	// Token: 0x04001B23 RID: 6947
	public int index;

	// Token: 0x04001B24 RID: 6948
	public GameObject weaponBodyDualPistol_2;

	// Token: 0x04001B25 RID: 6949
	public GameObject weaponBodyUpgradeEffectObj_DualPistol_2;

	// Token: 0x04001B26 RID: 6950
	public GameObject weaponBodyUpgradeEffect_M134_2;

	// Token: 0x04001B27 RID: 6951
	public GameObject weaponBodyUpgradeEffect_SM134_2;

	// Token: 0x04001B28 RID: 6952
	public GameObject weaponBodyUpgradeEffect_HM134_2;
}
