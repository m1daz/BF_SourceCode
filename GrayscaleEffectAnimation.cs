using System;
using UnityEngine;

// Token: 0x020001F4 RID: 500
public class GrayscaleEffectAnimation : MonoBehaviour
{
	// Token: 0x06000DBF RID: 3519 RVA: 0x00072305 File Offset: 0x00070705
	private void Start()
	{
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x00072308 File Offset: 0x00070708
	private void Update()
	{
		if (this.isGrayscaleEffect)
		{
			if (this.grayscaleEffect.rampOffset > 0f)
			{
				this.grayscaleEffect.rampOffset -= Time.deltaTime * 0.4f;
			}
		}
		else if (this.grayscaleEffect.rampOffset <= 0.4f)
		{
			this.grayscaleEffect.rampOffset += Time.deltaTime * 0.4f;
		}
		else
		{
			this.grayscaleEffect.enabled = false;
			base.enabled = false;
		}
	}

	// Token: 0x04000E31 RID: 3633
	public GrayscaleEffect grayscaleEffect;

	// Token: 0x04000E32 RID: 3634
	public bool isGrayscaleEffect;
}
