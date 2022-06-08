using System;
using UnityEngine;

// Token: 0x02000283 RID: 643
public class GGStrongholdHelicopter : MonoBehaviour
{
	// Token: 0x06001242 RID: 4674 RVA: 0x000A462B File Offset: 0x000A2A2B
	private void Start()
	{
		base.GetComponent<Rigidbody>().velocity = base.transform.TransformDirection(new Vector3(0f, 0f, UnityEngine.Random.Range(30f, 50f)));
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x000A4661 File Offset: 0x000A2A61
	private void Update()
	{
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			this.timeCount += Time.deltaTime;
			if (this.timeCount > 15f)
			{
				this.DestroyHelicopter();
			}
		}
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x000A469A File Offset: 0x000A2A9A
	private void DestroyHelicopter()
	{
		GGNetworkKit.mInstance.DestroySceneObjectRPC(base.gameObject);
	}

	// Token: 0x0400150F RID: 5391
	private float timeCount;
}
