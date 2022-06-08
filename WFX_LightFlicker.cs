using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000532 RID: 1330
[RequireComponent(typeof(Light))]
public class WFX_LightFlicker : MonoBehaviour
{
	// Token: 0x060025B9 RID: 9657 RVA: 0x00118732 File Offset: 0x00116B32
	private void Start()
	{
		this.timer = this.time;
		base.StartCoroutine("Flicker");
	}

	// Token: 0x060025BA RID: 9658 RVA: 0x0011874C File Offset: 0x00116B4C
	private IEnumerator Flicker()
	{
		for (;;)
		{
			base.GetComponent<Light>().enabled = !base.GetComponent<Light>().enabled;
			do
			{
				this.timer -= Time.deltaTime;
				yield return null;
			}
			while (this.timer > 0f);
			this.timer = this.time;
		}
		yield break;
	}

	// Token: 0x04002659 RID: 9817
	public float time = 0.05f;

	// Token: 0x0400265A RID: 9818
	private float timer;
}
