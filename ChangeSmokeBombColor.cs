using System;
using UnityEngine;

// Token: 0x020001FB RID: 507
public class ChangeSmokeBombColor : MonoBehaviour
{
	// Token: 0x06000DD5 RID: 3541 RVA: 0x00072D0C File Offset: 0x0007110C
	private void Start()
	{
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x00072D10 File Offset: 0x00071110
	private void Update()
	{
		this.smoketime += Time.deltaTime;
		if (this.smoketime >= this.smokeBombDurationTime && base.GetComponent<ParticleSystem>().startColor.a > 0.02f)
		{
			base.GetComponent<ParticleSystem>().startColor -= new Color(0f, 0f, 0f, Time.deltaTime * 0.2f);
		}
	}

	// Token: 0x04000E5B RID: 3675
	private float smoketime;

	// Token: 0x04000E5C RID: 3676
	private float smokeBombDurationTime = 25f;
}
