using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class GGNetworkPlayerAnimationcontrolForMainmenu : MonoBehaviour
{
	// Token: 0x0600109A RID: 4250 RVA: 0x0008EFF4 File Offset: 0x0008D3F4
	private void Start()
	{
		this.animatorControl = base.GetComponent<Animator>();
		this.preWalkAction = GGCharacterWalkState.Idle;
		this.currentWalkAction = GGCharacterWalkState.Idle;
		this.preFireAction = GGCharacterFireState.Idle;
		this.currentFireAction = GGCharacterFireState.Idle;
		this.preWeaponIndex = 0;
		string[] allWeaponNameList = GrowthManagerKit.GetAllWeaponNameList();
		GWeaponItemInfo[] curEquippedWeaponItemInfoList = GrowthManagerKit.GetCurEquippedWeaponItemInfoList();
		if (curEquippedWeaponItemInfoList != null)
		{
			for (int i = 0; i < curEquippedWeaponItemInfoList.Length; i++)
			{
				if (!(curEquippedWeaponItemInfoList[i].mName != string.Empty))
				{
					break;
				}
				for (int j = 0; j < allWeaponNameList.Length; j++)
				{
					if (curEquippedWeaponItemInfoList[i].mName == allWeaponNameList[j])
					{
						this.WeaponIndexList.Add(j);
					}
				}
			}
		}
		else
		{
			this.WeaponIndexList.Add(8);
		}
		this.curWeaponIndex = this.WeaponIndexList[UnityEngine.Random.Range(0, this.WeaponIndexList.Count)] + 1;
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x0008F0E4 File Offset: 0x0008D4E4
	private void Update()
	{
		if (this.currentFireAction == GGCharacterFireState.Reload)
		{
			this.head.localEulerAngles = Vector3.Lerp(this.head.localEulerAngles, this.headEndLow.localEulerAngles, Time.deltaTime * 4f);
		}
		if (this.currentFireAction == GGCharacterFireState.Idle && this.i == 1)
		{
			this.head.localEulerAngles = Vector3.Lerp(this.head.localEulerAngles, this.headEndPri.localEulerAngles, Time.deltaTime * 4f);
		}
		if (Time.frameCount % 45 == 0 && this.currentFireAction == GGCharacterFireState.Fire)
		{
			this.currentFireAction = GGCharacterFireState.Idle;
		}
		if (Time.frameCount % 100 == 0 && this.currentFireAction == GGCharacterFireState.Reload)
		{
			this.currentFireAction = GGCharacterFireState.Idle;
			this.i = UnityEngine.Random.Range(1, 3);
		}
		if (Time.frameCount % 500 == 0 && this.currentFireAction == GGCharacterFireState.Idle)
		{
			this.currentFireAction = GGCharacterFireState.Reload;
		}
		if (Time.frameCount % 420 == 0 && this.currentFireAction == GGCharacterFireState.Idle)
		{
			this.currentFireAction = GGCharacterFireState.Fire;
		}
		if (Time.frameCount % 1400 == 0 && this.currentFireAction == GGCharacterFireState.Idle)
		{
			this.curWeaponIndex = this.WeaponIndexList[UnityEngine.Random.Range(0, this.WeaponIndexList.Count)] + 1;
		}
		if (this.preWeaponIndex != this.curWeaponIndex)
		{
			this.animatorControl.SetInteger("WeaponID", this.curWeaponIndex);
			this.mWeaponManagerForMainmenu.SwitchWeaponOnline(this.curWeaponIndex - 1);
			this.preWeaponIndex = this.curWeaponIndex;
		}
		if (this.preWalkAction != this.currentWalkAction)
		{
			if (this.currentWalkAction == GGCharacterWalkState.Walk)
			{
				this.animatorControl.SetFloat("speed", 1f);
			}
			else if (this.currentWalkAction == GGCharacterWalkState.Idle)
			{
				this.animatorControl.SetFloat("speed", 0f);
			}
			this.preWalkAction = this.currentWalkAction;
		}
		if (this.preFireAction != this.currentFireAction)
		{
			if (this.currentFireAction == GGCharacterFireState.Idle)
			{
				this.animatorControl.SetBool("fire", false);
			}
			else if (this.currentFireAction == GGCharacterFireState.Reload)
			{
				this.animatorControl.SetBool("reload", true);
			}
			else if (this.currentFireAction == GGCharacterFireState.Fire)
			{
				this.animatorControl.SetBool("fire", true);
			}
			this.preFireAction = this.currentFireAction;
		}
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x0008F36C File Offset: 0x0008D76C
	private void ReloadWhenAttack()
	{
		this.animatorControl.SetBool("reload", true);
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x0008F37F File Offset: 0x0008D77F
	private void ChangeWeaponIdToNull()
	{
		this.animatorControl.SetInteger("WeaponID", 0);
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x0008F392 File Offset: 0x0008D792
	private void AutoStopReload()
	{
		this.animatorControl.SetBool("reload", false);
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x0008F3A5 File Offset: 0x0008D7A5
	private void AutoStopFire()
	{
		this.animatorControl.SetBool("fire", false);
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x0008F3B8 File Offset: 0x0008D7B8
	private void DeadOver()
	{
		this.animatorControl.SetBool("dead", false);
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x0008F3CB File Offset: 0x0008D7CB
	private void LiveOver()
	{
		this.animatorControl.SetBool("live", false);
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0008F3DE File Offset: 0x0008D7DE
	private void STENClipShow()
	{
		this.STENClip.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x0008F3F1 File Offset: 0x0008D7F1
	private void STENClipDontShow()
	{
		this.STENClip.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0008F404 File Offset: 0x0008D804
	private void RPGMissileShow()
	{
		this.RPGMissile.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x0008F417 File Offset: 0x0008D817
	private void RPGMissileDontShow()
	{
		this.RPGMissile.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x040012E9 RID: 4841
	private Animator animatorControl;

	// Token: 0x040012EA RID: 4842
	private GGCharacterWalkState preWalkAction;

	// Token: 0x040012EB RID: 4843
	private GGCharacterWalkState currentWalkAction;

	// Token: 0x040012EC RID: 4844
	private GGCharacterFireState preFireAction;

	// Token: 0x040012ED RID: 4845
	private GGCharacterFireState currentFireAction;

	// Token: 0x040012EE RID: 4846
	private int preWeaponIndex;

	// Token: 0x040012EF RID: 4847
	private int curWeaponIndex;

	// Token: 0x040012F0 RID: 4848
	public WeaponManagerForMainmenu mWeaponManagerForMainmenu;

	// Token: 0x040012F1 RID: 4849
	public List<int> WeaponIndexList = new List<int>();

	// Token: 0x040012F2 RID: 4850
	public Transform headEndLow;

	// Token: 0x040012F3 RID: 4851
	public Transform headEndRight;

	// Token: 0x040012F4 RID: 4852
	public Transform headEndLeft;

	// Token: 0x040012F5 RID: 4853
	public Transform headEndPri;

	// Token: 0x040012F6 RID: 4854
	public Transform head;

	// Token: 0x040012F7 RID: 4855
	private int i = 1;

	// Token: 0x040012F8 RID: 4856
	public GameObject STENClip;

	// Token: 0x040012F9 RID: 4857
	public GameObject RPGMissile;
}
