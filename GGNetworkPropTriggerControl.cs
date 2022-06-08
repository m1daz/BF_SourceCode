using System;
using UnityEngine;

// Token: 0x0200024F RID: 591
public class GGNetworkPropTriggerControl : MonoBehaviour
{
	// Token: 0x06001116 RID: 4374 RVA: 0x00097D0C File Offset: 0x0009610C
	private void Start()
	{
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x00097D10 File Offset: 0x00096110
	private void Update()
	{
		if (!this.canTrigger)
		{
			this.TriggerDelayTime += Time.deltaTime;
			if (this.TriggerDelayTime > this.TriggerDelay)
			{
				this.canTrigger = true;
				base.GetComponent<BoxCollider>().isTrigger = true;
				base.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = true;
			}
		}
	}

	// Token: 0x0400138A RID: 5002
	private float TriggerDelay = 3f;

	// Token: 0x0400138B RID: 5003
	private float TriggerDelayTime;

	// Token: 0x0400138C RID: 5004
	private bool canTrigger;
}
