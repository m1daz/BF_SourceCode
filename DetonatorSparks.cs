using System;
using UnityEngine;

// Token: 0x02000209 RID: 521
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Sparks")]
public class DetonatorSparks : DetonatorComponent
{
	// Token: 0x06000E41 RID: 3649 RVA: 0x00076CD1 File Offset: 0x000750D1
	public override void Init()
	{
		this.FillMaterials(false);
		this.BuildSparks();
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x00076CE0 File Offset: 0x000750E0
	public void FillMaterials(bool wipe)
	{
		if (!this.sparksMaterial || wipe)
		{
			this.sparksMaterial = base.MyDetonator().sparksMaterial;
		}
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x00076D0C File Offset: 0x0007510C
	public void BuildSparks()
	{
		this._sparks = new GameObject("Sparks");
		this._sparksEmitter = this._sparks.AddComponent<DetonatorBurstEmitter>();
		this._sparks.transform.parent = base.transform;
		this._sparks.transform.localPosition = this.localPosition;
		this._sparks.transform.localRotation = Quaternion.identity;
		this._sparksEmitter.material = this.sparksMaterial;
		this._sparksEmitter.force = Physics.gravity / 3f;
		this._sparksEmitter.useExplicitColorAnimation = false;
		this._sparksEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
		this._sparksEmitter.upwardsBias = base.MyDetonator().upwardsBias;
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x00076DE0 File Offset: 0x000751E0
	public void UpdateSparks()
	{
		this._scaledDuration = this.duration * this.timeScale;
		this._sparksEmitter.color = this.color;
		this._sparksEmitter.duration = this._scaledDuration / 4f;
		this._sparksEmitter.durationVariation = this._scaledDuration;
		this._sparksEmitter.count = (float)((int)(this.detail * 50f));
		this._sparksEmitter.particleSize = 0.5f;
		this._sparksEmitter.sizeVariation = 0.25f;
		if (this._sparksEmitter.upwardsBias > 0f)
		{
			this._sparksEmitter.velocity = new Vector3(this.velocity.x / Mathf.Log(this._sparksEmitter.upwardsBias), this.velocity.y * Mathf.Log(this._sparksEmitter.upwardsBias), this.velocity.z / Mathf.Log(this._sparksEmitter.upwardsBias));
		}
		else
		{
			this._sparksEmitter.velocity = this.velocity;
		}
		this._sparksEmitter.startRadius = 0f;
		this._sparksEmitter.size = this.size;
		this._sparksEmitter.explodeDelayMin = this.explodeDelayMin;
		this._sparksEmitter.explodeDelayMax = this.explodeDelayMax;
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x00076F44 File Offset: 0x00075344
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

	// Token: 0x06000E46 RID: 3654 RVA: 0x00076FB1 File Offset: 0x000753B1
	public override void Explode()
	{
		if (this.on)
		{
			this.UpdateSparks();
			this._sparksEmitter.Explode();
		}
	}

	// Token: 0x04000F42 RID: 3906
	private float _baseSize = 1f;

	// Token: 0x04000F43 RID: 3907
	private float _baseDuration = 4f;

	// Token: 0x04000F44 RID: 3908
	private Vector3 _baseVelocity = new Vector3(155f, 155f, 155f);

	// Token: 0x04000F45 RID: 3909
	private Color _baseColor = Color.white;

	// Token: 0x04000F46 RID: 3910
	private Vector3 _baseForce = Physics.gravity;

	// Token: 0x04000F47 RID: 3911
	private float _scaledDuration;

	// Token: 0x04000F48 RID: 3912
	private GameObject _sparks;

	// Token: 0x04000F49 RID: 3913
	private DetonatorBurstEmitter _sparksEmitter;

	// Token: 0x04000F4A RID: 3914
	public Material sparksMaterial;
}
