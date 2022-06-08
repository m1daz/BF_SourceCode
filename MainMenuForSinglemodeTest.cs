using System;
using UnityEngine;

// Token: 0x020000C9 RID: 201
public class MainMenuForSinglemodeTest : MonoBehaviour
{
	// Token: 0x060005FA RID: 1530 RVA: 0x00037195 File Offset: 0x00035595
	private void Start()
	{
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x00037197 File Offset: 0x00035597
	private void Update()
	{
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x0003719C File Offset: 0x0003559C
	private void OnGUI()
	{
		if (GUI.Button(new Rect(100f, 150f, 50f, 50f), "Add Gems"))
		{
			GrowthManagerKit.AddGems(500);
		}
		if (GUI.Button(new Rect(100f, 50f, 50f, 50f), "GetAllWeapons"))
		{
			string[] allWeaponNameList = GrowthManagerKit.GetAllWeaponNameList();
			for (int i = 0; i < allWeaponNameList.Length; i++)
			{
				GrowthManagerKit.GetWeaponItemInfoByName(allWeaponNameList[i]).AddWeaponTime(918000f, GWeaponRechargeType.WeaponTime);
			}
		}
		if (GUI.Button(new Rect(100f, 100f, 50f, 50f), "AddAllCards"))
		{
			GWeaponPropertyCardItemInfo[] allWeaponPropertyCardItemInfo = GrowthManagerKit.GetAllWeaponPropertyCardItemInfo();
			for (int j = 0; j < allWeaponPropertyCardItemInfo.Length; j++)
			{
				allWeaponPropertyCardItemInfo[j].AddCardNum(100);
			}
		}
		if (GUI.Button(new Rect(100f, 200f, 50f, 50f), "ResetTickets"))
		{
			GrowthManagerKit.SetHuntingTickets(0);
		}
		if (GUI.Button(new Rect(100f, 250f, 50f, 50f), "GM Coming"))
		{
			GrowthManagerKit.AddCustomEProperty(SceneEnchantmentProps.HCustomDamagePlus, 500f);
		}
	}
}
