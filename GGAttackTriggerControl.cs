using System;
using UnityEngine;

// Token: 0x0200025A RID: 602
public class GGAttackTriggerControl : MonoBehaviour
{
	// Token: 0x06001156 RID: 4438 RVA: 0x0009AB10 File Offset: 0x00098F10
	private void Start()
	{
		this.GGsingleEnemyLogic = base.transform.root.GetComponent<GGSingleEnemyLogic>();
		this.GGsingleEnemyAI = base.transform.root.GetComponent<GGSingleEnemyAI>();
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x0009AB3E File Offset: 0x00098F3E
	private void Update()
	{
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x0009AB40 File Offset: 0x00098F40
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			this.GGsingleEnemyLogic.enemyAction = EnemyAction.attack;
			this.GGsingleEnemyLogic.attackTime = 0f;
			this.GGsingleEnemyAI.canSearch = false;
		}
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x0009AB90 File Offset: 0x00098F90
	private void OnTriggerExit(Collider other)
	{
		if (this.GGsingleEnemyLogic.enemyType != EnemyType.sniperEnemy && other.gameObject.tag == "Player")
		{
			this.GGsingleEnemyLogic.enemyAction = EnemyAction.rush;
			this.GGsingleEnemyLogic.rushTime = 0f;
			this.GGsingleEnemyAI.canSearch = true;
		}
	}

	// Token: 0x04001405 RID: 5125
	private GGSingleEnemyLogic GGsingleEnemyLogic;

	// Token: 0x04001406 RID: 5126
	private GGSingleEnemyAI GGsingleEnemyAI;
}
