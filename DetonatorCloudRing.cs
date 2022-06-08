using System;
using UnityEngine;

// Token: 0x020001FF RID: 511
[RequireComponent(typeof(Detonator))]
public class DetonatorCloudRing : DetonatorComponent
{
	// Token: 0x06000DF6 RID: 3574 RVA: 0x00074270 File Offset: 0x00072670
	public override void Init()
	{
		this.FillMaterials(false);
		this.BuildCloudRing();
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x0007427F File Offset: 0x0007267F
	public void FillMaterials(bool wipe)
	{
		if (!this.cloudRingMaterial || wipe)
		{
			this.cloudRingMaterial = base.MyDetonator().smokeBMaterial;
		}
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x000742A8 File Offset: 0x000726A8
	public void BuildCloudRing()
	{
		this._cloudRing = new GameObject("CloudRing");
		this._cloudRingEmitter = this._cloudRing.AddComponent<DetonatorBurstEmitter>();
		this._cloudRing.transform.parent = base.transform;
		this._cloudRing.transform.localPosition = this.localPosition;
		this._cloudRingEmitter.material = this.cloudRingMaterial;
		this._cloudRingEmitter.useExplicitColorAnimation = true;
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x00074320 File Offset: 0x00072720
	public void UpdateCloudRing()
	{
		this._cloudRing.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._cloudRingEmitter.color = this.color;
		this._cloudRingEmitter.duration = this.duration;
		this._cloudRingEmitter.durationVariation = this.duration / 4f;
		this._cloudRingEmitter.count = (float)((int)(this.detail * 50f));
		this._cloudRingEmitter.particleSize = 10f;
		this._cloudRingEmitter.sizeVariation = 2f;
		this._cloudRingEmitter.velocity = this.velocity;
		this._cloudRingEmitter.startRadius = 3f;
		this._cloudRingEmitter.size = this.size;
		this._cloudRingEmitter.force = this.force;
		this._cloudRingEmitter.explodeDelayMin = this.explodeDelayMin;
		this._cloudRingEmitter.explodeDelayMax = this.explodeDelayMax;
		Color color = Color.Lerp(this.color, new Color(0.2f, 0.2f, 0.2f, 0.6f), 0.5f);
		Color color2 = new Color(0.2f, 0.2f, 0.2f, 0.5f);
		Color color3 = new Color(0.2f, 0.2f, 0.2f, 0.3f);
		Color color4 = new Color(0.2f, 0.2f, 0.2f, 0f);
		this._cloudRingEmitter.colorAnimation[0] = color;
		this._cloudRingEmitter.colorAnimation[1] = color2;
		this._cloudRingEmitter.colorAnimation[2] = color2;
		this._cloudRingEmitter.colorAnimation[3] = color3;
		this._cloudRingEmitter.colorAnimation[4] = color4;
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x00074524 File Offset: 0x00072924
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
		this.force = this._baseForce;
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x00074591 File Offset: 0x00072991
	public override void Explode()
	{
		if (this.on)
		{
			this.UpdateCloudRing();
			this._cloudRingEmitter.Explode();
		}
	}

	// Token: 0x04000ECC RID: 3788
	private float _baseSize = 1f;

	// Token: 0x04000ECD RID: 3789
	private float _baseDuration = 5f;

	// Token: 0x04000ECE RID: 3790
	private Vector3 _baseVelocity = new Vector3(155f, 5f, 155f);

	// Token: 0x04000ECF RID: 3791
	private Color _baseColor = Color.white;

	// Token: 0x04000ED0 RID: 3792
	private Vector3 _baseForce = new Vector3(0.162f, 2.56f, 0f);

	// Token: 0x04000ED1 RID: 3793
	private GameObject _cloudRing;

	// Token: 0x04000ED2 RID: 3794
	private DetonatorBurstEmitter _cloudRingEmitter;

	// Token: 0x04000ED3 RID: 3795
	public Material cloudRingMaterial;
}
