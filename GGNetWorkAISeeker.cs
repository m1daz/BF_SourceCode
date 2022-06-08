using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200022C RID: 556
public class GGNetWorkAISeeker : MonoBehaviour
{
	// Token: 0x06000F32 RID: 3890 RVA: 0x00081BD6 File Offset: 0x0007FFD6
	private void Start()
	{
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x00081BD8 File Offset: 0x0007FFD8
	private void Update()
	{
		if (GGNetworkKit.mInstance.IsMasterClient() && this.target != null)
		{
			this.mNavMeshAgent.SetDestination(this.target.transform.position);
		}
	}

	// Token: 0x040010F7 RID: 4343
	public NavMeshAgent mNavMeshAgent;

	// Token: 0x040010F8 RID: 4344
	public GameObject target;
}
