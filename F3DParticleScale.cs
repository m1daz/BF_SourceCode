using System;
using UnityEngine;

// Token: 0x0200046C RID: 1132
[ExecuteInEditMode]
public class F3DParticleScale : MonoBehaviour
{
	// Token: 0x060020D2 RID: 8402 RVA: 0x000F5836 File Offset: 0x000F3C36
	private void Start()
	{
		this.prevScale = this.ParticleScale;
	}

	// Token: 0x060020D3 RID: 8403 RVA: 0x000F5844 File Offset: 0x000F3C44
	private void ScaleShurikenSystems(float scaleFactor)
	{
	}

	// Token: 0x060020D4 RID: 8404 RVA: 0x000F5848 File Offset: 0x000F3C48
	private void ScaleTrailRenderers(float scaleFactor)
	{
		TrailRenderer[] componentsInChildren = base.GetComponentsInChildren<TrailRenderer>();
		foreach (TrailRenderer trailRenderer in componentsInChildren)
		{
			trailRenderer.startWidth *= scaleFactor;
			trailRenderer.endWidth *= scaleFactor;
		}
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x000F5892 File Offset: 0x000F3C92
	private void Update()
	{
	}

	// Token: 0x040021AD RID: 8621
	[Range(0f, 20f)]
	public float ParticleScale = 1f;

	// Token: 0x040021AE RID: 8622
	public bool ScaleGameobject = true;

	// Token: 0x040021AF RID: 8623
	private float prevScale;
}
