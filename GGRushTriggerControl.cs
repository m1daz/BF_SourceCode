using System;
using UnityEngine;

// Token: 0x0200025D RID: 605
public class GGRushTriggerControl : MonoBehaviour
{
	// Token: 0x06001171 RID: 4465 RVA: 0x0009BA99 File Offset: 0x00099E99
	private void Start()
	{
		this.GGsingleEnemyLogic = base.transform.root.GetComponent<GGSingleEnemyLogic>();
		this.GGsingleEnemyAI = base.transform.root.GetComponent<GGSingleEnemyAI>();
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x0009BAC7 File Offset: 0x00099EC7
	private void Update()
	{
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x0009BACC File Offset: 0x00099ECC
	private void OnTriggerEnter(Collider other)
	{
		if (this.GGsingleEnemyLogic.enemyType != EnemyType.sniperEnemy && other.gameObject.tag == "RushTrigger")
		{
			this.GGsingleEnemyLogic.enemyAction = EnemyAction.rush;
			this.GGsingleEnemyLogic.rushTime = 0f;
			this.GGsingleEnemyAI.canSearch = true;
		}
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x0009BB2C File Offset: 0x00099F2C
	private void OnTriggerExit(Collider other)
	{
		if (this.GGsingleEnemyLogic.enemyType == EnemyType.sniperEnemy || other.gameObject.tag == "RushTrigger")
		{
		}
	}

	// Token: 0x0400140B RID: 5131
	private GGSingleEnemyLogic GGsingleEnemyLogic;

	// Token: 0x0400140C RID: 5132
	private GGSingleEnemyAI GGsingleEnemyAI;
}
