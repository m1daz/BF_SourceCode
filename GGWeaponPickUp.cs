using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000226 RID: 550
public class GGWeaponPickUp : MonoBehaviour
{
	// Token: 0x06000EE1 RID: 3809 RVA: 0x0007D247 File Offset: 0x0007B647
	private void Awake()
	{
		this.weapManager = GameObject.FindWithTag("WeaponManager").GetComponent<GGWeaponManager>();
		this.controller = base.GetComponent<CharacterController>();
		this.prevHeight = this.controller.height;
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x0007D27B File Offset: 0x0007B67B
	private void Update()
	{
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x0007D27D File Offset: 0x0007B67D
	private void OnTriggerStay(Collider weapon)
	{
		if (weapon.gameObject.tag == "PickUp")
		{
			this.WeaponToPick = weapon.gameObject;
		}
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x0007D2A5 File Offset: 0x0007B6A5
	private void OnTriggerExit(Collider weapon)
	{
		if (weapon.gameObject.tag == "PickUp")
		{
			this.WeaponToPick = null;
		}
	}

	// Token: 0x04001044 RID: 4164
	private GUISkin guiStyle;

	// Token: 0x04001045 RID: 4165
	private GGWeaponPickUp.PickUpStyle pickUpStyle;

	// Token: 0x04001046 RID: 4166
	private int pickAmmoMultiply = 1;

	// Token: 0x04001047 RID: 4167
	private int reserveAmmoLimit = 3;

	// Token: 0x04001048 RID: 4168
	private float throwForce = 500f;

	// Token: 0x04001049 RID: 4169
	private Transform spawnObject;

	// Token: 0x0400104A RID: 4170
	private int actionsToDisplay = 5;

	// Token: 0x0400104B RID: 4171
	private float messageTimeOut = 5f;

	// Token: 0x0400104C RID: 4172
	public List<GameObject> weapons;

	// Token: 0x0400104D RID: 4173
	public List<GGWeaponScript> playerWeapons;

	// Token: 0x0400104E RID: 4174
	private List<string> actionsList;

	// Token: 0x0400104F RID: 4175
	private List<float> timer;

	// Token: 0x04001050 RID: 4176
	private string weapName;

	// Token: 0x04001051 RID: 4177
	private GameObject weaponToThrow;

	// Token: 0x04001052 RID: 4178
	private GGWeaponScript newWeapon;

	// Token: 0x04001053 RID: 4179
	private GameObject WeaponToPick;

	// Token: 0x04001054 RID: 4180
	private GGWeaponManager weapManager;

	// Token: 0x04001055 RID: 4181
	private float color;

	// Token: 0x04001056 RID: 4182
	private string text;

	// Token: 0x04001057 RID: 4183
	private CharacterController controller;

	// Token: 0x04001058 RID: 4184
	private float prevHeight;

	// Token: 0x02000227 RID: 551
	private enum PickUpStyle
	{
		// Token: 0x0400105A RID: 4186
		Replace,
		// Token: 0x0400105B RID: 4187
		Add
	}
}
