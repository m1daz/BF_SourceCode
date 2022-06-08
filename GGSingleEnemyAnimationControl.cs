using System;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class GGSingleEnemyAnimationControl : MonoBehaviour
{
	// Token: 0x06001180 RID: 4480 RVA: 0x0009BE66 File Offset: 0x0009A266
	private void Awake()
	{
		this.animatorControl = base.GetComponent<Animator>();
		this.GGsingleEnemyLogic = base.transform.root.GetComponent<GGSingleEnemyLogic>();
		this.preAction = EnemyAction.idle;
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x0009BE94 File Offset: 0x0009A294
	private void Update()
	{
		this.currentAction = this.GGsingleEnemyLogic.enemyAction;
		if (this.preAction != this.currentAction)
		{
			if ((this.currentAction == EnemyAction.patrol || this.currentAction == EnemyAction.rush || this.currentAction == EnemyAction.search || this.currentAction == EnemyAction.escape) && this.GGsingleEnemyLogic.enemyType != EnemyType.sniperEnemy)
			{
				this.animatorControl.SetFloat("speed", 1f);
			}
			if (this.currentAction == EnemyAction.idle || this.currentAction == EnemyAction.attack || this.currentAction == EnemyAction.warning)
			{
				this.animatorControl.SetFloat("speed", 0f);
			}
			if (this.currentAction == EnemyAction.attack)
			{
				this.animatorControl.SetBool("fire", true);
			}
			if (this.preAction == EnemyAction.attack)
			{
				this.animatorControl.SetBool("fire", false);
			}
			this.preAction = this.currentAction;
		}
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x0009BF97 File Offset: 0x0009A397
	private void ChangeWeaponIdToNull()
	{
		this.animatorControl.SetInteger("WeaponID", 0);
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x0009BFAA File Offset: 0x0009A3AA
	private void AutoStopReload()
	{
		this.animatorControl.SetBool("reload", false);
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x0009BFBD File Offset: 0x0009A3BD
	private void AutoStopFire()
	{
		this.animatorControl.SetBool("fire", false);
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x0009BFD0 File Offset: 0x0009A3D0
	private void DeadOver()
	{
		this.animatorControl.SetBool("dead", false);
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x0009BFE3 File Offset: 0x0009A3E3
	private void LiveOver()
	{
		this.animatorControl.SetBool("live", false);
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x0009BFF6 File Offset: 0x0009A3F6
	private void EnemyDead()
	{
		this.animatorControl.SetFloat("speed", 0f);
		this.animatorControl.SetBool("fire", false);
		this.animatorControl.SetBool("dead", true);
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x0009C02F File Offset: 0x0009A42F
	private void ReloadWhenAttack()
	{
		this.animatorControl.SetBool("reload", true);
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x0009C042 File Offset: 0x0009A442
	private void SingleEnemyTakeFirstWeapon(int weaponId)
	{
		this.animatorControl.SetInteger("WeaponID", weaponId);
	}

	// Token: 0x04001415 RID: 5141
	private Animator animatorControl;

	// Token: 0x04001416 RID: 5142
	private GGSingleEnemyLogic GGsingleEnemyLogic;

	// Token: 0x04001417 RID: 5143
	private EnemyAction preAction;

	// Token: 0x04001418 RID: 5144
	private EnemyAction currentAction;
}
