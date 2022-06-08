using System;
using UnityEngine;

// Token: 0x020001FC RID: 508
public abstract class DetonatorComponent : MonoBehaviour
{
	// Token: 0x06000DD8 RID: 3544
	public abstract void Explode();

	// Token: 0x06000DD9 RID: 3545
	public abstract void Init();

	// Token: 0x06000DDA RID: 3546 RVA: 0x00072E5C File Offset: 0x0007125C
	public void SetStartValues()
	{
		this.startSize = this.size;
		this.startForce = this.force;
		this.startVelocity = this.velocity;
		this.startDuration = this.duration;
		this.startDetail = this.detail;
		this.startColor = this.color;
		this.startLocalPosition = this.localPosition;
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x00072EC0 File Offset: 0x000712C0
	public Detonator MyDetonator()
	{
		return base.GetComponent("Detonator") as Detonator;
	}

	// Token: 0x04000E5D RID: 3677
	public bool on = true;

	// Token: 0x04000E5E RID: 3678
	public bool detonatorControlled = true;

	// Token: 0x04000E5F RID: 3679
	[HideInInspector]
	public float startSize = 1f;

	// Token: 0x04000E60 RID: 3680
	public float size = 1f;

	// Token: 0x04000E61 RID: 3681
	public float explodeDelayMin;

	// Token: 0x04000E62 RID: 3682
	public float explodeDelayMax;

	// Token: 0x04000E63 RID: 3683
	[HideInInspector]
	public float startDuration = 2f;

	// Token: 0x04000E64 RID: 3684
	public float duration = 2f;

	// Token: 0x04000E65 RID: 3685
	[HideInInspector]
	public float timeScale = 1f;

	// Token: 0x04000E66 RID: 3686
	[HideInInspector]
	public float startDetail = 1f;

	// Token: 0x04000E67 RID: 3687
	public float detail = 1f;

	// Token: 0x04000E68 RID: 3688
	[HideInInspector]
	public Color startColor = Color.white;

	// Token: 0x04000E69 RID: 3689
	public Color color = Color.white;

	// Token: 0x04000E6A RID: 3690
	[HideInInspector]
	public Vector3 startLocalPosition = Vector3.zero;

	// Token: 0x04000E6B RID: 3691
	public Vector3 localPosition = Vector3.zero;

	// Token: 0x04000E6C RID: 3692
	[HideInInspector]
	public Vector3 startForce = Vector3.zero;

	// Token: 0x04000E6D RID: 3693
	public Vector3 force = Vector3.zero;

	// Token: 0x04000E6E RID: 3694
	[HideInInspector]
	public Vector3 startVelocity = Vector3.zero;

	// Token: 0x04000E6F RID: 3695
	public Vector3 velocity = Vector3.zero;

	// Token: 0x04000E70 RID: 3696
	public float detailThreshold;
}
