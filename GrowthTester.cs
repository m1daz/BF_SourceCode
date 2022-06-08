using System;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class GrowthTester : MonoBehaviour
{
	// Token: 0x060005F2 RID: 1522 RVA: 0x0003709F File Offset: 0x0003549F
	private void Start()
	{
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x000370A1 File Offset: 0x000354A1
	private void Update()
	{
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x000370A4 File Offset: 0x000354A4
	private void OnGUI()
	{
		if (GUI.Button(new Rect(100f, 100f, 100f, 100f), "WeaponItem"))
		{
			GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName("AUG");
			Debug.Log(weaponItemInfoByName.mGunType);
		}
	}
}
