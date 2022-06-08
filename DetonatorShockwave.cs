using System;
using UnityEngine;

// Token: 0x02000206 RID: 518
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Shockwave")]
public class DetonatorShockwave : DetonatorComponent
{
	// Token: 0x06000E2C RID: 3628 RVA: 0x000760D6 File Offset: 0x000744D6
	public override void Init()
	{
		this.FillMaterials(false);
		this.BuildShockwave();
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x000760E5 File Offset: 0x000744E5
	public void FillMaterials(bool wipe)
	{
		if (!this.shockwaveMaterial || wipe)
		{
			this.shockwaveMaterial = base.MyDetonator().shockwaveMaterial;
		}
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x00076110 File Offset: 0x00074510
	public void BuildShockwave()
	{
		this._shockwave = new GameObject("Shockwave");
		this._shockwaveEmitter = this._shockwave.AddComponent<DetonatorBurstEmitter>();
		this._shockwave.transform.parent = base.transform;
		this._shockwave.transform.localRotation = Quaternion.identity;
		this._shockwave.transform.localPosition = this.localPosition;
		this._shockwaveEmitter.material = this.shockwaveMaterial;
		this._shockwaveEmitter.exponentialGrowth = false;
		this._shockwaveEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x000761B4 File Offset: 0x000745B4
	public void UpdateShockwave()
	{
		this._shockwave.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._shockwaveEmitter.color = this.color;
		this._shockwaveEmitter.duration = this.duration;
		this._shockwaveEmitter.durationVariation = this.duration * 0.1f;
		this._shockwaveEmitter.count = 1f;
		this._shockwaveEmitter.detail = 1f;
		this._shockwaveEmitter.particleSize = 25f;
		this._shockwaveEmitter.sizeVariation = 0f;
		this._shockwaveEmitter.velocity = new Vector3(0f, 0f, 0f);
		this._shockwaveEmitter.startRadius = 0f;
		this._shockwaveEmitter.sizeGrow = 202f;
		this._shockwaveEmitter.size = this.size;
		this._shockwaveEmitter.explodeDelayMin = this.explodeDelayMin;
		this._shockwaveEmitter.explodeDelayMax = this.explodeDelayMax;
		this._shockwaveEmitter.renderMode = this.renderMode;
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x000762F0 File Offset: 0x000746F0
	public void Reset()
	{
		this.FillMaterials(true);
		this.on = true;
		this.size = this._baseSize;
		this.duration = this._baseDuration;
		this.explodeDelayMin = 0f;
		this.explodeDelayMax = 0f;
		this.color = this._baseColor;
		this.velocity = this._baseVelocity;
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x00076351 File Offset: 0x00074751
	public override void Explode()
	{
		if (this.on)
		{
			this.UpdateShockwave();
			this._shockwaveEmitter.Explode();
		}
	}

	// Token: 0x04000F23 RID: 3875
	private float _baseSize = 1f;

	// Token: 0x04000F24 RID: 3876
	private float _baseDuration = 0.25f;

	// Token: 0x04000F25 RID: 3877
	private Vector3 _baseVelocity = new Vector3(0f, 0f, 0f);

	// Token: 0x04000F26 RID: 3878
	private Color _baseColor = Color.white;

	// Token: 0x04000F27 RID: 3879
	private GameObject _shockwave;

	// Token: 0x04000F28 RID: 3880
	private DetonatorBurstEmitter _shockwaveEmitter;

	// Token: 0x04000F29 RID: 3881
	public Material shockwaveMaterial;

	// Token: 0x04000F2A RID: 3882
	public ParticleRenderMode renderMode;
}
