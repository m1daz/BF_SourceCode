using System;
using UnityEngine;

// Token: 0x02000200 RID: 512
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Fireball")]
public class DetonatorFireball : DetonatorComponent
{
	// Token: 0x06000DFD RID: 3581 RVA: 0x0007460D File Offset: 0x00072A0D
	public override void Init()
	{
		this.FillMaterials(false);
		this.BuildFireballA();
		this.BuildFireballB();
		this.BuildFireShadow();
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x00074628 File Offset: 0x00072A28
	public void FillMaterials(bool wipe)
	{
		if (!this.fireballAMaterial || wipe)
		{
			this.fireballAMaterial = base.MyDetonator().fireballAMaterial;
		}
		if (!this.fireballBMaterial || wipe)
		{
			this.fireballBMaterial = base.MyDetonator().fireballBMaterial;
		}
		if (!this.fireShadowMaterial || wipe)
		{
			if ((double)UnityEngine.Random.value > 0.5)
			{
				this.fireShadowMaterial = base.MyDetonator().smokeAMaterial;
			}
			else
			{
				this.fireShadowMaterial = base.MyDetonator().smokeBMaterial;
			}
		}
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x000746D4 File Offset: 0x00072AD4
	public void BuildFireballA()
	{
		this._fireballA = new GameObject("FireballA");
		this._fireballAEmitter = this._fireballA.AddComponent<DetonatorBurstEmitter>();
		this._fireballA.transform.parent = base.transform;
		this._fireballA.transform.localRotation = Quaternion.identity;
		this._fireballAEmitter.material = this.fireballAMaterial;
		this._fireballAEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
		this._fireballAEmitter.upwardsBias = base.MyDetonator().upwardsBias;
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x0007476C File Offset: 0x00072B6C
	public void UpdateFireballA()
	{
		this._fireballA.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._fireballAEmitter.color = this.color;
		this._fireballAEmitter.duration = this.duration * 0.5f;
		this._fireballAEmitter.durationVariation = this.duration * 0.5f;
		this._fireballAEmitter.count = 2f;
		this._fireballAEmitter.timeScale = this.timeScale;
		this._fireballAEmitter.detail = this.detail;
		this._fireballAEmitter.particleSize = 14f;
		this._fireballAEmitter.sizeVariation = 3f;
		this._fireballAEmitter.velocity = this.velocity;
		this._fireballAEmitter.startRadius = 4f;
		this._fireballAEmitter.size = this.size;
		this._fireballAEmitter.useExplicitColorAnimation = true;
		Color b = new Color(1f, 1f, 1f, 0.5f);
		Color b2 = new Color(0.6f, 0.15f, 0.15f, 0.3f);
		Color color = new Color(0.1f, 0.2f, 0.45f, 0f);
		this._fireballAEmitter.colorAnimation[0] = Color.Lerp(this.color, b, 0.8f);
		this._fireballAEmitter.colorAnimation[1] = Color.Lerp(this.color, b, 0.5f);
		this._fireballAEmitter.colorAnimation[2] = this.color;
		this._fireballAEmitter.colorAnimation[3] = Color.Lerp(this.color, b2, 0.7f);
		this._fireballAEmitter.colorAnimation[4] = color;
		this._fireballAEmitter.explodeDelayMin = this.explodeDelayMin;
		this._fireballAEmitter.explodeDelayMax = this.explodeDelayMax;
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x00074998 File Offset: 0x00072D98
	public void BuildFireballB()
	{
		this._fireballB = new GameObject("FireballB");
		this._fireballBEmitter = this._fireballB.AddComponent<DetonatorBurstEmitter>();
		this._fireballB.transform.parent = base.transform;
		this._fireballB.transform.localRotation = Quaternion.identity;
		this._fireballBEmitter.material = this.fireballBMaterial;
		this._fireballBEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
		this._fireballBEmitter.upwardsBias = base.MyDetonator().upwardsBias;
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x00074A30 File Offset: 0x00072E30
	public void UpdateFireballB()
	{
		this._fireballB.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._fireballBEmitter.color = this.color;
		this._fireballBEmitter.duration = this.duration * 0.5f;
		this._fireballBEmitter.durationVariation = this.duration * 0.5f;
		this._fireballBEmitter.count = 2f;
		this._fireballBEmitter.timeScale = this.timeScale;
		this._fireballBEmitter.detail = this.detail;
		this._fireballBEmitter.particleSize = 10f;
		this._fireballBEmitter.sizeVariation = 6f;
		this._fireballBEmitter.velocity = this.velocity;
		this._fireballBEmitter.startRadius = 4f;
		this._fireballBEmitter.size = this.size;
		this._fireballBEmitter.useExplicitColorAnimation = true;
		Color b = new Color(1f, 1f, 1f, 0.5f);
		Color b2 = new Color(0.6f, 0.15f, 0.15f, 0.3f);
		Color color = new Color(0.1f, 0.2f, 0.45f, 0f);
		this._fireballBEmitter.colorAnimation[0] = Color.Lerp(this.color, b, 0.8f);
		this._fireballBEmitter.colorAnimation[1] = Color.Lerp(this.color, b, 0.5f);
		this._fireballBEmitter.colorAnimation[2] = this.color;
		this._fireballBEmitter.colorAnimation[3] = Color.Lerp(this.color, b2, 0.7f);
		this._fireballBEmitter.colorAnimation[4] = color;
		this._fireballBEmitter.explodeDelayMin = this.explodeDelayMin;
		this._fireballBEmitter.explodeDelayMax = this.explodeDelayMax;
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x00074C5C File Offset: 0x0007305C
	public void BuildFireShadow()
	{
		this._fireShadow = new GameObject("FireShadow");
		this._fireShadowEmitter = this._fireShadow.AddComponent<DetonatorBurstEmitter>();
		this._fireShadow.transform.parent = base.transform;
		this._fireShadow.transform.localRotation = Quaternion.identity;
		this._fireShadowEmitter.material = this.fireShadowMaterial;
		this._fireShadowEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
		this._fireShadowEmitter.upwardsBias = base.MyDetonator().upwardsBias;
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x00074CF4 File Offset: 0x000730F4
	public void UpdateFireShadow()
	{
		this._fireShadow.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._fireShadow.transform.LookAt(Camera.main.transform);
		this._fireShadow.transform.localPosition = -(Vector3.forward * 1f);
		this._fireShadowEmitter.color = new Color(0.1f, 0.1f, 0.1f, 0.6f);
		this._fireShadowEmitter.duration = this.duration * 0.5f;
		this._fireShadowEmitter.durationVariation = this.duration * 0.5f;
		this._fireShadowEmitter.timeScale = this.timeScale;
		this._fireShadowEmitter.detail = 1f;
		this._fireShadowEmitter.particleSize = 13f;
		this._fireShadowEmitter.velocity = this.velocity;
		this._fireShadowEmitter.sizeVariation = 1f;
		this._fireShadowEmitter.count = 4f;
		this._fireShadowEmitter.startRadius = 6f;
		this._fireShadowEmitter.size = this.size;
		this._fireShadowEmitter.explodeDelayMin = this.explodeDelayMin;
		this._fireShadowEmitter.explodeDelayMax = this.explodeDelayMax;
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x00074E68 File Offset: 0x00073268
	public void Reset()
	{
		this.FillMaterials(true);
		this.on = true;
		this.size = this._baseSize;
		this.duration = this._baseDuration;
		this.explodeDelayMin = 0f;
		this.explodeDelayMax = 0f;
		this.color = this._baseColor;
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x00074EC0 File Offset: 0x000732C0
	public override void Explode()
	{
		if (this.detailThreshold > this.detail)
		{
			return;
		}
		if (this.on)
		{
			this.UpdateFireballA();
			this.UpdateFireballB();
			this.UpdateFireShadow();
			if (this.drawFireballA)
			{
				this._fireballAEmitter.Explode();
			}
			if (this.drawFireballB)
			{
				this._fireballBEmitter.Explode();
			}
			if (this.drawFireShadow)
			{
				this._fireShadowEmitter.Explode();
			}
		}
	}

	// Token: 0x04000ED4 RID: 3796
	private float _baseSize = 1f;

	// Token: 0x04000ED5 RID: 3797
	private float _baseDuration = 3f;

	// Token: 0x04000ED6 RID: 3798
	private Color _baseColor = new Color(1f, 0.423f, 0f, 0.5f);

	// Token: 0x04000ED7 RID: 3799
	private float _scaledDuration;

	// Token: 0x04000ED8 RID: 3800
	private GameObject _fireballA;

	// Token: 0x04000ED9 RID: 3801
	private DetonatorBurstEmitter _fireballAEmitter;

	// Token: 0x04000EDA RID: 3802
	public Material fireballAMaterial;

	// Token: 0x04000EDB RID: 3803
	private GameObject _fireballB;

	// Token: 0x04000EDC RID: 3804
	private DetonatorBurstEmitter _fireballBEmitter;

	// Token: 0x04000EDD RID: 3805
	public Material fireballBMaterial;

	// Token: 0x04000EDE RID: 3806
	private GameObject _fireShadow;

	// Token: 0x04000EDF RID: 3807
	private DetonatorBurstEmitter _fireShadowEmitter;

	// Token: 0x04000EE0 RID: 3808
	public Material fireShadowMaterial;

	// Token: 0x04000EE1 RID: 3809
	public bool drawFireballA = true;

	// Token: 0x04000EE2 RID: 3810
	public bool drawFireballB = true;

	// Token: 0x04000EE3 RID: 3811
	public bool drawFireShadow = true;

	// Token: 0x04000EE4 RID: 3812
	private Color _detailAdjustedColor;
}
