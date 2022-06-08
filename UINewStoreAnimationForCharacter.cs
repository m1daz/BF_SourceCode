using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002E3 RID: 739
public class UINewStoreAnimationForCharacter : MonoBehaviour
{
	// Token: 0x060016B8 RID: 5816 RVA: 0x000C1AEF File Offset: 0x000BFEEF
	private void Awake()
	{
		if (UINewStoreAnimationForCharacter.mInstance == null)
		{
			UINewStoreAnimationForCharacter.mInstance = this;
		}
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x000C1B07 File Offset: 0x000BFF07
	private void OnDestroy()
	{
		if (UINewStoreAnimationForCharacter.mInstance != null)
		{
			UINewStoreAnimationForCharacter.mInstance = null;
		}
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x000C1B1F File Offset: 0x000BFF1F
	private void Start()
	{
		this.animatorControl = base.GetComponent<Animator>();
		this.preWalkAction = GGCharacterWalkState.Idle;
		this.currentWalkAction = GGCharacterWalkState.Idle;
		this.preFireAction = GGCharacterFireState.Idle;
		this.currentFireAction = GGCharacterFireState.Idle;
		this.preWeaponIndex = -1;
		this.curWeaponIndex = 0;
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x000C1B58 File Offset: 0x000BFF58
	private void Update()
	{
		this.headRotationDelay += Time.deltaTime;
		if (this.headRotationDelay >= this.headRotationDelay_Count)
		{
			this.p = UnityEngine.Random.Range(0, 3);
			this.q = UnityEngine.Random.Range(0, 4);
			this.headRotationDelay = 0f;
			this.headRotationDelay_Count = UnityEngine.Random.Range(2f, 3f);
		}
		if (this.p == 0)
		{
			this.head.localEulerAngles = Vector3.Lerp(this.head.localEulerAngles, this.headEndLow.localEulerAngles, Time.deltaTime * 4f);
		}
		else if (this.p == 1)
		{
			this.head.localEulerAngles = Vector3.Lerp(this.head.localEulerAngles, this.headEndPri.localEulerAngles, Time.deltaTime * 4f);
		}
		this.idleDelay += Time.deltaTime;
		if (this.idleDelay >= this.idleDelay_Count)
		{
			this.idleDelay = 0f;
			this.idleDelay_Count = UnityEngine.Random.Range(1f, 3f);
			int num = UnityEngine.Random.Range(1, 4);
			this.currentFireAction = ((num != 1) ? ((num != 2) ? GGCharacterFireState.Idle : GGCharacterFireState.Reload) : GGCharacterFireState.Fire);
		}
		if (this.currentFireAction == GGCharacterFireState.Fire)
		{
			base.StartCoroutine(this.StopFire());
		}
		if (this.preWeaponIndex != this.curWeaponIndex)
		{
			this.animatorControl.SetInteger("WeaponID", this.curWeaponIndex + 1);
			this.mWeaponManagerForUIStore.SwitchWeaponOnline(this.curWeaponIndex);
			this.preWeaponIndex = this.curWeaponIndex;
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

	// Token: 0x060016BC RID: 5820 RVA: 0x000C1D88 File Offset: 0x000C0188
	public void SetCurWeaponIndex(int index)
	{
		this.currentFireAction = GGCharacterFireState.Idle;
		this.curWeaponIndex = index;
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x000C1D98 File Offset: 0x000C0198
	public void SetCurWeaponUpgradeIndex(int weaponindex, int upgradeLv)
	{
		this.mWeaponManagerForUIStore.WeaponUpgrade(weaponindex, upgradeLv);
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x000C1DA7 File Offset: 0x000C01A7
	private void ReloadWhenAttack()
	{
		this.animatorControl.SetBool("reload", true);
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x000C1DBA File Offset: 0x000C01BA
	private void ChangeWeaponIdToNull()
	{
		this.animatorControl.SetInteger("WeaponID", 0);
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x000C1DCD File Offset: 0x000C01CD
	private void AutoStopReload()
	{
		this.animatorControl.SetBool("reload", false);
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x000C1DE0 File Offset: 0x000C01E0
	private void AutoStopFire()
	{
		this.animatorControl.SetBool("fire", false);
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x000C1DF4 File Offset: 0x000C01F4
	private IEnumerator StopFire()
	{
		yield return new WaitForSeconds(0.3f);
		this.currentFireAction = GGCharacterFireState.Idle;
		yield break;
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x000C1E0F File Offset: 0x000C020F
	private void DeadOver()
	{
		this.animatorControl.SetBool("dead", false);
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x000C1E22 File Offset: 0x000C0222
	private void LiveOver()
	{
		this.animatorControl.SetBool("live", false);
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x000C1E35 File Offset: 0x000C0235
	private void RPGMissileShow()
	{
		this.RPGMissile.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x060016C6 RID: 5830 RVA: 0x000C1E48 File Offset: 0x000C0248
	private void RPGMissileDontShow()
	{
		this.RPGMissile.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060016C7 RID: 5831 RVA: 0x000C1E5B File Offset: 0x000C025B
	private void M67Show()
	{
		this.M67.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x000C1E6E File Offset: 0x000C026E
	private void M67DontShow()
	{
		this.M67.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x000C1E81 File Offset: 0x000C0281
	private void MilkBombShow()
	{
		this.MilkBomb.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x000C1E94 File Offset: 0x000C0294
	private void MilkBombDontShow()
	{
		this.MilkBomb.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x000C1EA7 File Offset: 0x000C02A7
	private void GingerBreadBombShow()
	{
		this.GingerBreadBomb.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x000C1EBA File Offset: 0x000C02BA
	private void GingerBreadBombDontShow()
	{
		this.GingerBreadBomb.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x000C1ECD File Offset: 0x000C02CD
	private void STENClipShow()
	{
		this.STENClip.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x000C1EE0 File Offset: 0x000C02E0
	private void STENClipDontShow()
	{
		this.STENClip.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x000C1EF3 File Offset: 0x000C02F3
	private void SmokeBombShow()
	{
		this.SmokeBomb.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x060016D0 RID: 5840 RVA: 0x000C1F06 File Offset: 0x000C0306
	private void SmokeBombDontShow()
	{
		this.SmokeBomb.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x000C1F19 File Offset: 0x000C0319
	private void FlashBombShow()
	{
		this.FlashBomb.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x000C1F2C File Offset: 0x000C032C
	private void FlashBombDontShow()
	{
		this.FlashBomb.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x000C1F3F File Offset: 0x000C033F
	private void SnowmanBombShow()
	{
		this.SnowmanBomb.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x000C1F52 File Offset: 0x000C0352
	private void SnowmanBombDontShow()
	{
		this.SnowmanBomb.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x0400197B RID: 6523
	public static UINewStoreAnimationForCharacter mInstance;

	// Token: 0x0400197C RID: 6524
	private Animator animatorControl;

	// Token: 0x0400197D RID: 6525
	private GGCharacterWalkState preWalkAction;

	// Token: 0x0400197E RID: 6526
	private GGCharacterWalkState currentWalkAction;

	// Token: 0x0400197F RID: 6527
	private GGCharacterFireState preFireAction;

	// Token: 0x04001980 RID: 6528
	private GGCharacterFireState currentFireAction;

	// Token: 0x04001981 RID: 6529
	private int preWeaponIndex;

	// Token: 0x04001982 RID: 6530
	private int curWeaponIndex;

	// Token: 0x04001983 RID: 6531
	public UINewStoreWeaponManage mWeaponManagerForUIStore;

	// Token: 0x04001984 RID: 6532
	public Transform headEndLow;

	// Token: 0x04001985 RID: 6533
	public Transform headEndHigh;

	// Token: 0x04001986 RID: 6534
	public Transform headEndRight1;

	// Token: 0x04001987 RID: 6535
	public Transform headEndLeft1;

	// Token: 0x04001988 RID: 6536
	public Transform headEndRight2;

	// Token: 0x04001989 RID: 6537
	public Transform headEndLeft2;

	// Token: 0x0400198A RID: 6538
	public Transform headEndPri;

	// Token: 0x0400198B RID: 6539
	public Transform head;

	// Token: 0x0400198C RID: 6540
	private int p = 1;

	// Token: 0x0400198D RID: 6541
	private int q = 1;

	// Token: 0x0400198E RID: 6542
	private float idleDelay;

	// Token: 0x0400198F RID: 6543
	private float idleDelay_Count = 3f;

	// Token: 0x04001990 RID: 6544
	private float headRotationDelay;

	// Token: 0x04001991 RID: 6545
	private float headRotationDelay_Count = 3f;

	// Token: 0x04001992 RID: 6546
	public GameObject RPGMissile;

	// Token: 0x04001993 RID: 6547
	public GameObject M67;

	// Token: 0x04001994 RID: 6548
	public GameObject MilkBomb;

	// Token: 0x04001995 RID: 6549
	public GameObject GingerBreadBomb;

	// Token: 0x04001996 RID: 6550
	public GameObject STENClip;

	// Token: 0x04001997 RID: 6551
	public GameObject SmokeBomb;

	// Token: 0x04001998 RID: 6552
	public GameObject FlashBomb;

	// Token: 0x04001999 RID: 6553
	public GameObject SnowmanBomb;
}
