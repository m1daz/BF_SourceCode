using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000265 RID: 613
public class GGSingleEnemyLogic : MonoBehaviour
{
	// Token: 0x0600118B RID: 4491 RVA: 0x0009C060 File Offset: 0x0009A460
	private void Start()
	{
		this.GGsingleEnemyAI = base.GetComponent<GGSingleEnemyAI>();
		this.bDead = false;
		this.ChangeWeaponSpeedOver = false;
		this.tempGenerateTransform = GameObject.Find("enemyTempGenerateTransform" + this.enemyId).transform;
		this.tempGenerateTransform.position = base.transform.position;
		this.tempPatrolTransform = GameObject.Find("enemyTempPatrolTransform" + this.enemyId).transform;
		this.tempRushTransform = GameObject.Find("enemyTempRushTransform" + this.enemyId).transform;
		this.tempAttackTransform = GameObject.Find("enemyTempAttackTransform" + this.enemyId).transform;
		this.playerTransform = GameObject.FindWithTag("Player").transform;
		this.currentDifficulty = GGSingleEnemyManager.mInstance.Difficulty;
		this.Blood = 130f;
		this.patrolSpeed = 1.5f;
		this.AttackMoveSpeed = 2.5f;
		this.RushSpeed = 3.5f;
		this.EscapeSpeed = 4f;
		this.DamageRate = 0.2f;
		this.attackRotateSpeed = 0.06f;
		this.attackDuringTime = 1.2f;
		this.attackErrorAngle = 3f;
		this.callProbability = 0.2f;
		this.escapeProbability = 0.2f;
		this.attackRange = 12f;
		this.rushRange = 30f;
		this.AttackSpeed = 1.8f;
		this.coinsGet = 1f;
		this.expGet = 1f;
		this.rewardPointGet = 1f;
		if (this.currentDifficulty == 1)
		{
			if (this.enemyLv == EnemyLv.custom)
			{
				this.currentDifficultyIndex = 1.4f;
			}
			else if (this.enemyLv == EnemyLv.elite)
			{
				this.currentDifficultyIndex = 1.6f;
			}
			else if (this.enemyLv == EnemyLv.boss)
			{
				this.currentDifficultyIndex = 1.8f;
			}
		}
		else if (this.currentDifficulty == 2)
		{
			if (this.enemyLv == EnemyLv.custom)
			{
				this.currentDifficultyIndex = 1.6f;
			}
			else if (this.enemyLv == EnemyLv.elite)
			{
				this.currentDifficultyIndex = 1.8f;
			}
			else if (this.enemyLv == EnemyLv.boss)
			{
				this.currentDifficultyIndex = 2f;
			}
		}
		else if (this.currentDifficulty == 3)
		{
			if (this.enemyLv == EnemyLv.custom)
			{
				this.currentDifficultyIndex = 1.8f;
			}
			else if (this.enemyLv == EnemyLv.elite)
			{
				this.currentDifficultyIndex = 2f;
			}
			else if (this.enemyLv == EnemyLv.boss)
			{
				this.currentDifficultyIndex = 2.2f;
			}
		}
		this.Blood *= this.currentDifficultyIndex;
		this.AttackMoveSpeed *= this.currentDifficultyIndex;
		this.RushSpeed *= this.currentDifficultyIndex;
		this.EscapeSpeed *= this.currentDifficultyIndex;
		this.DamageRate *= this.currentDifficultyIndex;
		this.attackRotateSpeed *= this.currentDifficultyIndex;
		this.attackDuringTime *= this.currentDifficultyIndex;
		this.attackErrorAngle /= this.currentDifficultyIndex;
		this.callProbability *= this.currentDifficultyIndex;
		this.escapeProbability *= this.currentDifficultyIndex;
		this.attackRange *= this.currentDifficultyIndex;
		this.rushRange *= this.currentDifficultyIndex;
		this.AttackSpeed /= this.currentDifficultyIndex;
		this.audioEnemyDie = base.GetComponent<AudioSource>();
		switch (this.enemyType)
		{
		case EnemyType.knifeEnemy:
		{
			int[] array = new int[]
			{
				0,
				17
			};
			int num = UnityEngine.Random.Range(0, 2);
			int num2 = array[num];
			base.BroadcastMessage("SwitchWeaponSingleEnemy", num2, SendMessageOptions.DontRequireReceiver);
			base.transform.Find("Player_1_sinkmesh").SendMessage("SingleEnemyTakeFirstWeapon", num2 + 1, SendMessageOptions.DontRequireReceiver);
			break;
		}
		case EnemyType.gunEnemy:
		{
			int[] array2 = new int[]
			{
				1,
				2,
				3,
				4,
				8,
				9,
				10,
				11,
				12,
				14,
				18,
				19,
				20
			};
			int num3 = UnityEngine.Random.Range(0, 13);
			int num4 = array2[num3];
			base.BroadcastMessage("SwitchWeaponSingleEnemy", num4, SendMessageOptions.DontRequireReceiver);
			base.transform.Find("Player_1_sinkmesh").SendMessage("SingleEnemyTakeFirstWeapon", num4 + 1, SendMessageOptions.DontRequireReceiver);
			break;
		}
		case EnemyType.sniperEnemy:
		{
			int[] array3 = new int[]
			{
				5,
				15
			};
			int num5 = UnityEngine.Random.Range(0, 2);
			int num6 = array3[num5];
			base.BroadcastMessage("SwitchWeaponSingleEnemy", num6, SendMessageOptions.DontRequireReceiver);
			base.transform.Find("Player_1_sinkmesh").SendMessage("SingleEnemyTakeFirstWeapon", num6 + 1, SendMessageOptions.DontRequireReceiver);
			break;
		}
		case EnemyType.granadeEnemy:
		{
			int[] array4 = new int[]
			{
				7,
				13,
				16
			};
			int num7 = UnityEngine.Random.Range(0, 3);
			int num8 = array4[num7];
			base.BroadcastMessage("SwitchWeaponSingleEnemy", num8, SendMessageOptions.DontRequireReceiver);
			base.transform.Find("Player_1_sinkmesh").SendMessage("SingleEnemyTakeFirstWeapon", num8 + 1, SendMessageOptions.DontRequireReceiver);
			break;
		}
		}
		switch (this.enemyType)
		{
		case EnemyType.knifeEnemy:
			base.transform.Find("Triggers/attackTrigger").localScale = new Vector3(this.attackRange * 0.12f, this.attackRange * 0.3f, this.attackRange * 0.12f);
			break;
		case EnemyType.gunEnemy:
			base.transform.Find("Triggers/attackTrigger").localScale = new Vector3(this.attackRange, this.attackRange * 0.3f, this.attackRange);
			break;
		case EnemyType.sniperEnemy:
			base.transform.Find("Triggers/attackTrigger").localScale = new Vector3(this.attackRange * 4f, this.attackRange * 4f, this.attackRange * 4f);
			break;
		case EnemyType.granadeEnemy:
			base.transform.Find("Triggers/attackTrigger").localScale = new Vector3(this.attackRange * 1f, this.attackRange * 1.5f, this.attackRange * 1f);
			break;
		}
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x0009C6EC File Offset: 0x0009AAEC
	private void Update()
	{
		if (GGSingleModePauseControl.mInstance != null && GGSingleModePauseControl.mInstance.PauseState)
		{
			return;
		}
		if (this.bDead)
		{
			return;
		}
		if (this.enemyAction == EnemyAction.idle)
		{
			this.IdleLogic();
		}
		else if (this.enemyAction == EnemyAction.patrol)
		{
			this.PatrolLogic();
		}
		else if (this.enemyAction == EnemyAction.attack)
		{
			this.AttackLogic();
			if (!this.ChangeWeaponSpeedOver)
			{
				base.BroadcastMessage("ChangeWeaponSpeed", this.AttackSpeed, SendMessageOptions.DontRequireReceiver);
				this.ChangeWeaponSpeedOver = true;
			}
		}
		else if (this.enemyAction == EnemyAction.rush)
		{
			this.RushLogic();
		}
		else if (this.enemyAction == EnemyAction.warning)
		{
			this.WarningLogic();
		}
		else if (this.enemyAction == EnemyAction.search)
		{
			this.SearchLogic();
		}
		else if (this.enemyAction == EnemyAction.escape)
		{
			this.EscapeLogic();
		}
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x0009C7EC File Offset: 0x0009ABEC
	private void IdleLogic()
	{
		if (this.idleTime == 0f)
		{
			this.GGsingleEnemyAI.target = this.tempGenerateTransform;
			this.GGsingleEnemyAI.canSearch = false;
			this.sniperAniamtionPlayed = false;
		}
		this.idleTime += Time.deltaTime;
		if (this.idleTime >= 5f)
		{
			int num = UnityEngine.Random.Range(0, 10);
			if (num < 5)
			{
				this.enemyAction = EnemyAction.idle;
			}
			else
			{
				this.enemyAction = EnemyAction.patrol;
				this.GGsingleEnemyAI.canSearch = true;
			}
			this.idleTime = 0f;
		}
		if (Time.frameCount % 15 == 0 && Physics.Raycast(base.transform.position + new Vector3(0f, 1.2f, 0f), this.playerTransform.position - base.transform.position, out this.hitInfo, 30f, -21) && this.hitInfo.collider.gameObject.tag.Equals("Player"))
		{
			this.enemyAction = EnemyAction.rush;
			this.rushTime = 0f;
		}
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x0009C924 File Offset: 0x0009AD24
	private void PatrolLogic()
	{
		switch (this.enemyType)
		{
		case EnemyType.knifeEnemy:
			if (this.patrolTime == 0f)
			{
				this.tempPatrolTransform.position = this.tempGenerateTransform.position + new Vector3(UnityEngine.Random.Range(-5f, 5f), 0f, UnityEngine.Random.Range(-5f, 5f));
				this.GGsingleEnemyAI.target = this.tempPatrolTransform;
				this.GGsingleEnemyAI.speed = this.patrolSpeed;
			}
			this.patrolTime += Time.deltaTime;
			if (this.patrolTime >= 4f)
			{
				int num = UnityEngine.Random.Range(0, 10);
				if (num < 5)
				{
					this.enemyAction = EnemyAction.idle;
				}
				else
				{
					this.enemyAction = EnemyAction.patrol;
				}
				this.patrolTime = 0f;
			}
			break;
		case EnemyType.gunEnemy:
			if (this.patrolTime == 0f)
			{
				this.tempPatrolTransform.position = this.tempGenerateTransform.position + new Vector3(UnityEngine.Random.Range(-5f, 5f), 0f, UnityEngine.Random.Range(-5f, 5f));
				this.GGsingleEnemyAI.target = this.tempPatrolTransform;
				this.GGsingleEnemyAI.speed = this.patrolSpeed;
			}
			this.patrolTime += Time.deltaTime;
			if (this.patrolTime >= 5f)
			{
				int num2 = UnityEngine.Random.Range(0, 10);
				if (num2 < 5)
				{
					this.enemyAction = EnemyAction.idle;
				}
				else
				{
					this.enemyAction = EnemyAction.patrol;
				}
				this.patrolTime = 0f;
			}
			break;
		case EnemyType.sniperEnemy:
			if (this.patrolTime == 0f)
			{
			}
			this.patrolTime += Time.deltaTime;
			if (this.patrolTime >= 5f)
			{
				int num3 = UnityEngine.Random.Range(0, 10);
				if (num3 < 2)
				{
					this.enemyAction = EnemyAction.idle;
				}
				else
				{
					this.enemyAction = EnemyAction.patrol;
				}
				this.patrolTime = 0f;
			}
			break;
		case EnemyType.granadeEnemy:
			if (this.patrolTime == 0f)
			{
				this.tempPatrolTransform.position = this.tempGenerateTransform.position + new Vector3(UnityEngine.Random.Range(-15f, 15f), 0f, UnityEngine.Random.Range(-15f, 15f));
				this.GGsingleEnemyAI.target = this.tempPatrolTransform;
				this.GGsingleEnemyAI.speed = this.patrolSpeed;
			}
			this.patrolTime += Time.deltaTime;
			if (this.patrolTime >= 4f)
			{
				int num4 = UnityEngine.Random.Range(0, 10);
				if (num4 < 5)
				{
					this.enemyAction = EnemyAction.idle;
				}
				else
				{
					this.enemyAction = EnemyAction.patrol;
				}
				this.patrolTime = 0f;
			}
			break;
		}
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x0009CC20 File Offset: 0x0009B020
	private void RushLogic()
	{
		switch (this.enemyType)
		{
		case EnemyType.knifeEnemy:
			if (this.rushTime == 0f)
			{
				this.GGsingleEnemyAI.canSearch = true;
				this.GGsingleEnemyAI.target = this.playerTransform;
				this.GGsingleEnemyAI.speed = this.RushSpeed;
			}
			this.rushTime += Time.deltaTime;
			if (Time.frameCount % 15 == 0 && Physics.Raycast(base.transform.position + new Vector3(0f, 1.2f, 0f), this.playerTransform.position - base.transform.position, out this.hitInfo, this.attackRange * 0.1f, -21) && this.hitInfo.collider.gameObject.tag.Equals("Player"))
			{
				this.enemyAction = EnemyAction.attack;
				this.attackTime = 0f;
			}
			if (this.rushTime > 30f)
			{
				this.rushTime = 0f;
				this.enemyAction = EnemyAction.escape;
				this.escapeTime = 0f;
			}
			break;
		case EnemyType.gunEnemy:
			if (this.rushTime == 0f)
			{
				this.GGsingleEnemyAI.canSearch = true;
				this.GGsingleEnemyAI.target = this.playerTransform;
				this.GGsingleEnemyAI.speed = this.RushSpeed;
				base.BroadcastMessage("StopFireInSingleMode", SendMessageOptions.DontRequireReceiver);
			}
			this.rushTime += Time.deltaTime;
			if (Time.frameCount % 15 == 0 && Physics.Raycast(base.transform.position + new Vector3(0f, 1.2f, 0f), this.playerTransform.position - base.transform.position, out this.hitInfo, this.attackRange, -21) && this.hitInfo.collider.gameObject.tag.Equals("Player"))
			{
				this.enemyAction = EnemyAction.attack;
				this.attackTime = 0f;
			}
			if (this.rushTime > 25f)
			{
				this.rushTime = 0f;
				this.enemyAction = EnemyAction.escape;
				this.escapeTime = 0f;
			}
			break;
		case EnemyType.sniperEnemy:
			this.sniperAniamtionPlayed = false;
			break;
		case EnemyType.granadeEnemy:
			if (this.rushTime == 0f)
			{
				this.GGsingleEnemyAI.canSearch = true;
				this.GGsingleEnemyAI.speed = this.RushSpeed;
			}
			this.rushTime += Time.deltaTime;
			if (this.rushTime > 2f)
			{
				this.tempRushTransform.position = base.transform.position + new Vector3(UnityEngine.Random.Range(-15f, 15f), 0f, UnityEngine.Random.Range(-15f, 15f));
				this.GGsingleEnemyAI.target = this.tempRushTransform;
				this.rushTime = 0f;
			}
			if (Time.frameCount % 15 == 0 && Physics.Raycast(base.transform.position + new Vector3(0f, 1.2f, 0f), this.playerTransform.position - base.transform.position, out this.hitInfo, this.attackRange * 1.5f, -21) && this.hitInfo.collider.gameObject.tag.Equals("Player"))
			{
				this.enemyAction = EnemyAction.attack;
				this.attackTime = 0f;
			}
			if (this.rushTime > 14f)
			{
				this.rushTime = 0f;
				this.enemyAction = EnemyAction.escape;
				this.escapeTime = 0f;
			}
			break;
		}
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x0009D02C File Offset: 0x0009B42C
	private void AttackLogic()
	{
		switch (this.enemyType)
		{
		case EnemyType.knifeEnemy:
			if (this.attackTime == 0f)
			{
				this.rushTime = 0f;
				this.GGsingleEnemyAI.canSearch = false;
				this.GGsingleEnemyAI.speed = this.AttackMoveSpeed;
				base.StartCoroutine(this.FireForOneSecond(this.attackDuringTime * 0.5f / this.currentDifficultyIndex));
			}
			if (this.attackTime > this.attackDuringTime * 0.3f / this.currentDifficultyIndex && this.attackTime < this.attackDuringTime * 0.3f / this.currentDifficultyIndex + Time.deltaTime)
			{
				base.BroadcastMessage("FireInSingleMode", SendMessageOptions.DontRequireReceiver);
			}
			this.attackTime += Time.deltaTime;
			if (this.attackTime > 2f / this.currentDifficultyIndex)
			{
				this.attackTime = 0f;
			}
			if (this.isAttackState)
			{
				Quaternion b = Quaternion.LookRotation(this.playerTransform.position - base.transform.position, Vector3.up);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, this.attackRotateSpeed * 2f * this.currentDifficultyIndex);
			}
			break;
		case EnemyType.gunEnemy:
			if (this.attackTime == 0f)
			{
				this.rushTime = 0f;
				this.GGsingleEnemyAI.canSearch = false;
				this.GGsingleEnemyAI.speed = this.AttackMoveSpeed;
				base.StartCoroutine(this.FireForOneSecond(this.attackDuringTime * this.currentDifficultyIndex));
			}
			this.attackTime += Time.deltaTime;
			if (this.attackTime > 2.5f * this.currentDifficultyIndex)
			{
				if (Physics.Raycast(base.transform.position + new Vector3(0f, 1.2f, 0f), this.playerTransform.position - base.transform.position, out this.hitInfo, 30f, -21))
				{
					if (this.hitInfo.collider.gameObject.tag.Equals("Player"))
					{
						this.enemyAction = EnemyAction.attack;
					}
					else
					{
						this.enemyAction = EnemyAction.warning;
						this.GGsingleEnemyAI.canSearch = true;
					}
				}
				this.attackTime = 0f;
			}
			if (this.isAttackState)
			{
				Quaternion b2 = Quaternion.LookRotation(this.playerTransform.position - base.transform.position, Vector3.up);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b2, this.attackRotateSpeed * this.currentDifficultyIndex);
			}
			break;
		case EnemyType.sniperEnemy:
			if (this.attackTime == 0f)
			{
				this.rushTime = 0f;
				this.sniperAniamtionPlayed = true;
				this.GGsingleEnemyAI.canSearch = false;
				this.GGsingleEnemyAI.speed = this.AttackMoveSpeed;
				base.StartCoroutine(this.FireForOneSecond(this.attackDuringTime * this.currentDifficultyIndex));
			}
			this.attackTime += Time.deltaTime;
			if (this.attackTime > 6f)
			{
				this.attackTime = 0f;
			}
			if (this.isAttackState)
			{
				Quaternion b3 = Quaternion.LookRotation(this.playerTransform.position - base.transform.position, Vector3.up);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b3, this.attackRotateSpeed * 2f * this.currentDifficultyIndex);
			}
			break;
		case EnemyType.granadeEnemy:
			if (this.attackTime == 0f)
			{
				this.rushTime = 0f;
				this.GGsingleEnemyAI.canSearch = false;
				this.GGsingleEnemyAI.speed = this.AttackMoveSpeed;
				base.StartCoroutine(this.FireForOneSecond(this.attackDuringTime));
			}
			this.attackTime += Time.deltaTime;
			if (this.attackTime > 5f / this.currentDifficultyIndex)
			{
				if (Physics.Raycast(base.transform.position + new Vector3(0f, 1.2f, 0f), this.playerTransform.position - base.transform.position, out this.hitInfo, 30f, -21))
				{
					if (this.hitInfo.collider.gameObject.tag.Equals("Player"))
					{
						this.GGsingleEnemyAI.canSearch = false;
						base.StartCoroutine(this.FireForOneSecond(this.attackDuringTime));
					}
					else
					{
						this.enemyAction = EnemyAction.warning;
						this.GGsingleEnemyAI.canSearch = true;
					}
				}
				this.attackTime = 0f;
			}
			if (this.isAttackState)
			{
				Quaternion b4 = Quaternion.LookRotation(this.playerTransform.position - base.transform.position, Vector3.up);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b4, this.attackRotateSpeed * 3f * this.currentDifficultyIndex);
			}
			break;
		}
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x0009D5A0 File Offset: 0x0009B9A0
	private void WarningLogic()
	{
		if (this.enemyType != EnemyType.granadeEnemy && this.enemyType != EnemyType.sniperEnemy)
		{
			if (this.warningTime == 0f)
			{
				this.GGsingleEnemyAI.canSearch = false;
			}
			this.warningTime += Time.deltaTime;
			if (this.warningTime > 0.5f)
			{
				if (Physics.Raycast(base.transform.position + new Vector3(0f, 1.2f, 0f), this.playerTransform.position - base.transform.position, out this.hitInfo, 50f, -21))
				{
					if (this.hitInfo.collider.gameObject.tag.Equals("Player"))
					{
						this.enemyAction = EnemyAction.attack;
					}
					else
					{
						int num = UnityEngine.Random.Range(0, 10);
						if (num < 3)
						{
							this.enemyAction = EnemyAction.warning;
						}
						else
						{
							this.enemyAction = EnemyAction.search;
							this.GGsingleEnemyAI.canSearch = true;
						}
					}
				}
				this.warningTime = 0f;
			}
		}
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x0009D6C4 File Offset: 0x0009BAC4
	private void SearchLogic()
	{
		if (this.enemyType == EnemyType.knifeEnemy)
		{
			if (this.searchTime == 0f)
			{
				this.GGsingleEnemyAI.target = this.playerTransform;
			}
		}
		else if (this.enemyType == EnemyType.gunEnemy)
		{
			if (this.searchTime == 0f)
			{
				this.GGsingleEnemyAI.target = this.playerTransform;
			}
		}
		else if (this.enemyType == EnemyType.sniperEnemy)
		{
			if (this.searchTime == 0f)
			{
				this.GGsingleEnemyAI.target = this.generatePosition;
			}
		}
		else if (this.enemyType == EnemyType.sniperEnemy && this.searchTime == 0f)
		{
			this.GGsingleEnemyAI.target = this.playerTransform;
		}
		this.searchTime += Time.deltaTime;
		if (this.searchTime > 0.5f)
		{
			if (Physics.Raycast(base.transform.position + new Vector3(0f, 1.2f, 0f), this.playerTransform.position - base.transform.position, out this.hitInfo, 50f, -21))
			{
				if (this.hitInfo.collider.gameObject.tag.Equals("Player"))
				{
					this.enemyAction = EnemyAction.attack;
				}
				else
				{
					int num = UnityEngine.Random.Range(0, 10);
					if (num < 3)
					{
						this.enemyAction = EnemyAction.warning;
					}
					else
					{
						this.enemyAction = EnemyAction.search;
					}
				}
			}
			this.searchTime = 0f;
		}
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x0009D86C File Offset: 0x0009BC6C
	private void EscapeLogic()
	{
		if (this.escapeTime == 0f)
		{
			this.GGsingleEnemyAI.target = this.tempGenerateTransform;
			this.GGsingleEnemyAI.speed = this.EscapeSpeed;
		}
		this.escapeTime += Time.deltaTime;
		if (Vector3.Distance(base.transform.position, this.tempGenerateTransform.position) < 2f)
		{
			this.escapeTime = 0f;
			this.enemyAction = EnemyAction.idle;
		}
		if (this.escapeTime > 15f)
		{
			this.enemyAction = EnemyAction.rush;
			this.escapeTime = 0f;
		}
		if (this.escapeTime > 2f && this.escapeTime < 2f + Time.deltaTime)
		{
			float num = UnityEngine.Random.Range(0f, 1f);
			if (num < this.callProbability)
			{
				Collider[] array = Physics.OverlapSphere(base.transform.position, 40f);
				foreach (Collider collider in array)
				{
					if (collider.gameObject.tag.Equals("singleEnemy") && (collider.gameObject.GetComponent<GGSingleEnemyLogic>().enemyAction == EnemyAction.idle || collider.gameObject.GetComponent<GGSingleEnemyLogic>().enemyAction == EnemyAction.patrol))
					{
						collider.gameObject.GetComponent<GGSingleEnemyLogic>().enemyAction = EnemyAction.rush;
					}
				}
			}
		}
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x0009D9E4 File Offset: 0x0009BDE4
	private IEnumerator FireForOneSecond(float delayTime)
	{
		this.isAttackState = true;
		base.BroadcastMessage("FireInSingleMode", SendMessageOptions.DontRequireReceiver);
		yield return new WaitForSeconds(delayTime);
		base.BroadcastMessage("StopFireInSingleMode", SendMessageOptions.DontRequireReceiver);
		if (UnityEngine.Random.Range(0, 3) == 0)
		{
			base.BroadcastMessage("ReloadWhenAttack", SendMessageOptions.DontRequireReceiver);
		}
		switch (this.enemyType)
		{
		case EnemyType.knifeEnemy:
			this.tempAttackTransform.position = base.transform.position + new Vector3(UnityEngine.Random.Range(-2f, 2f), 0f, UnityEngine.Random.Range(-2f, 2f));
			break;
		case EnemyType.gunEnemy:
			this.tempAttackTransform.position = base.transform.position + new Vector3(UnityEngine.Random.Range(-6f, 6f), 0f, UnityEngine.Random.Range(-6f, 6f));
			break;
		case EnemyType.sniperEnemy:
			this.tempAttackTransform.position = base.transform.position;
			break;
		case EnemyType.granadeEnemy:
			this.tempAttackTransform.position = base.transform.position + new Vector3(UnityEngine.Random.Range(-15f, 15f), 0f, UnityEngine.Random.Range(-15f, 15f));
			break;
		}
		if (this.enemyAction == EnemyAction.attack && this.enemyType != EnemyType.sniperEnemy)
		{
			this.GGsingleEnemyAI.target = this.tempAttackTransform;
		}
		if (this.enemyType != EnemyType.sniperEnemy)
		{
			this.GGsingleEnemyAI.canSearch = true;
		}
		this.isAttackState = false;
		yield break;
	}

	// Token: 0x06001195 RID: 4501 RVA: 0x0009DA08 File Offset: 0x0009BE08
	public void DamageToSingleEnemy(int bulletDamage)
	{
		if (this.Blood > 0f)
		{
			this.playBulletHitAudio();
			this.Blood -= (float)bulletDamage;
			if (this.enemyAction == EnemyAction.idle || this.enemyAction == EnemyAction.patrol)
			{
				this.enemyAction = EnemyAction.rush;
			}
			if (this.Blood <= 60f && UnityEngine.Random.Range(0, 5) == 0)
			{
				this.enemyAction = EnemyAction.escape;
				this.escapeTime = 0f;
			}
			if (this.Blood <= 0f)
			{
				if (this.enemyLv == EnemyLv.custom)
				{
					PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + this.currentDifficulty);
					PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + this.currentDifficulty);
				}
				else if (this.enemyLv == EnemyLv.elite)
				{
					PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + this.currentDifficulty * 2);
					PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + this.currentDifficulty * 2);
				}
				else if (this.enemyLv == EnemyLv.boss)
				{
					PlayerPrefs.SetInt("SingleModeChapterOneCurrentPoint", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0) + this.currentDifficulty * 3);
					PlayerPrefs.SetInt("SingleModeChapterOneTotalPoint", PlayerPrefs.GetInt("SingleModeChapterOneTotalPoint", 0) + this.currentDifficulty * 3);
					if (Application.loadedLevelName == "SingleMode_2")
					{
						if (this.enemyId == 74)
						{
							this.playerTransform.gameObject.BroadcastMessage("RescueCrew", SendMessageOptions.DontRequireReceiver);
						}
						if (this.enemyId == 3)
						{
							this.playerTransform.gameObject.BroadcastMessage("BombDestroy", SendMessageOptions.DontRequireReceiver);
						}
					}
				}
				this.bDead = true;
				UnityEngine.Object.Destroy(base.transform.GetComponent<GGSingleEnemyAI>());
				base.transform.GetComponent<BoxCollider>().enabled = false;
				base.transform.GetComponent<CharacterController>().enabled = false;
				this.playEnemyDieAudio();
				base.StartCoroutine(this.waitForSecondsToDestory(3));
			}
		}
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x0009DC1B File Offset: 0x0009C01B
	public void playEnemyDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x0009DC28 File Offset: 0x0009C028
	public void playBulletHitAudio()
	{
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x0009DC2C File Offset: 0x0009C02C
	private IEnumerator waitForSecondsToDestory(int seconds)
	{
		base.gameObject.GetComponent<CharacterController>().enabled = false;
		yield return new WaitForSeconds((float)seconds);
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x04001432 RID: 5170
	public float Blood;

	// Token: 0x04001433 RID: 5171
	public float patrolSpeed;

	// Token: 0x04001434 RID: 5172
	public float AttackMoveSpeed;

	// Token: 0x04001435 RID: 5173
	public float RushSpeed;

	// Token: 0x04001436 RID: 5174
	public float EscapeSpeed;

	// Token: 0x04001437 RID: 5175
	public float DamageRate;

	// Token: 0x04001438 RID: 5176
	private float attackRotateSpeed;

	// Token: 0x04001439 RID: 5177
	private float attackDuringTime;

	// Token: 0x0400143A RID: 5178
	private float attackErrorAngle;

	// Token: 0x0400143B RID: 5179
	private float callProbability;

	// Token: 0x0400143C RID: 5180
	private float escapeProbability;

	// Token: 0x0400143D RID: 5181
	private float attackRange;

	// Token: 0x0400143E RID: 5182
	private float rushRange;

	// Token: 0x0400143F RID: 5183
	private float AttackSpeed;

	// Token: 0x04001440 RID: 5184
	private float coinsGet;

	// Token: 0x04001441 RID: 5185
	private float expGet;

	// Token: 0x04001442 RID: 5186
	private float rewardPointGet;

	// Token: 0x04001443 RID: 5187
	public string EnemyName;

	// Token: 0x04001444 RID: 5188
	public Material[] LvOneSkinBodyMaterial;

	// Token: 0x04001445 RID: 5189
	public Material[] LvOneSkinHatMaterial;

	// Token: 0x04001446 RID: 5190
	public Material[] LvTwoSkinBodyMaterial;

	// Token: 0x04001447 RID: 5191
	public Material[] LvTwoSkinHatMaterial;

	// Token: 0x04001448 RID: 5192
	public Material[] LvThreeSkinBodyMaterial;

	// Token: 0x04001449 RID: 5193
	public Material[] LvThreeSkinHatMaterial;

	// Token: 0x0400144A RID: 5194
	public Material[] LvFourSkinBodyMaterial;

	// Token: 0x0400144B RID: 5195
	public Material[] LvFourSkinHatMaterial;

	// Token: 0x0400144C RID: 5196
	public Material[] LvFiveSkinBodyMaterial;

	// Token: 0x0400144D RID: 5197
	public Material[] LvFiveSkinHatMaterial;

	// Token: 0x0400144E RID: 5198
	public Material EnemyMaterial;

	// Token: 0x0400144F RID: 5199
	public AudioClip[] AllClip;

	// Token: 0x04001450 RID: 5200
	private Animation BodyAnimation;

	// Token: 0x04001451 RID: 5201
	private Animation HandAnimation;

	// Token: 0x04001452 RID: 5202
	public EnemyLv enemyLv;

	// Token: 0x04001453 RID: 5203
	public EnemyType enemyType;

	// Token: 0x04001454 RID: 5204
	public EnemyAction enemyAction;

	// Token: 0x04001455 RID: 5205
	public int enemyId;

	// Token: 0x04001456 RID: 5206
	public bool bDead;

	// Token: 0x04001457 RID: 5207
	private Transform generatePosition;

	// Token: 0x04001458 RID: 5208
	private Transform playerTransform;

	// Token: 0x04001459 RID: 5209
	private string senceName;

	// Token: 0x0400145A RID: 5210
	private int currentDifficulty;

	// Token: 0x0400145B RID: 5211
	private float currentDifficultyIndex;

	// Token: 0x0400145C RID: 5212
	public float idleTime;

	// Token: 0x0400145D RID: 5213
	public float patrolTime;

	// Token: 0x0400145E RID: 5214
	public float rushTime;

	// Token: 0x0400145F RID: 5215
	public float attackTime;

	// Token: 0x04001460 RID: 5216
	public float warningTime;

	// Token: 0x04001461 RID: 5217
	public float searchTime;

	// Token: 0x04001462 RID: 5218
	public float escapeTime;

	// Token: 0x04001463 RID: 5219
	private GGSingleEnemyAI GGsingleEnemyAI;

	// Token: 0x04001464 RID: 5220
	public RaycastHit hitInfo;

	// Token: 0x04001465 RID: 5221
	private bool isAttackState;

	// Token: 0x04001466 RID: 5222
	private Transform handtransform;

	// Token: 0x04001467 RID: 5223
	private Transform tempGenerateTransform;

	// Token: 0x04001468 RID: 5224
	private Transform tempPatrolTransform;

	// Token: 0x04001469 RID: 5225
	private Transform tempRushTransform;

	// Token: 0x0400146A RID: 5226
	private Transform tempAttackTransform;

	// Token: 0x0400146B RID: 5227
	private bool ChangeWeaponSpeedOver;

	// Token: 0x0400146C RID: 5228
	private bool sniperAniamtionPlayed;

	// Token: 0x0400146D RID: 5229
	private AudioSource audioEnemyDie;
}
