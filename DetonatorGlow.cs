using System;
using UnityEngine;

// Token: 0x02000203 RID: 515
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Glow")]
public class DetonatorGlow : DetonatorComponent
{
	// Token: 0x06000E1A RID: 3610 RVA: 0x000757E6 File Offset: 0x00073BE6
	public override void Init()
	{
		this.FillMaterials(false);
		this.BuildGlow();
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x000757F5 File Offset: 0x00073BF5
	public void FillMaterials(bool wipe)
	{
		if (!this.glowMaterial || wipe)
		{
			this.glowMaterial = base.MyDetonator().glowMaterial;
		}
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x00075820 File Offset: 0x00073C20
	public void BuildGlow()
	{
		this._glow = new GameObject("Glow");
		this._glowEmitter = this._glow.AddComponent<DetonatorBurstEmitter>();
		this._glow.transform.parent = base.transform;
		this._glow.transform.localPosition = this.localPosition;
		this._glowEmitter.material = this.glowMaterial;
		this._glowEmitter.exponentialGrowth = false;
		this._glowEmitter.useExplicitColorAnimation = true;
		this._glowEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x000758BC File Offset: 0x00073CBC
	public void UpdateGlow()
	{
		this._glow.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._glowEmitter.color = this.color;
		this._glowEmitter.duration = this.duration;
		this._glowEmitter.timeScale = this.timeScale;
		this._glowEmitter.count = 1f;
		this._glowEmitter.particleSize = 65f;
		this._glowEmitter.sizeVariation = 0f;
		this._glowEmitter.velocity = new Vector3(0f, 0f, 0f);
		this._glowEmitter.startRadius = 0f;
		this._glowEmitter.sizeGrow = 0f;
		this._glowEmitter.size = this.size;
		this._glowEmitter.explodeDelayMin = this.explodeDelayMin;
		this._glowEmitter.explodeDelayMax = this.explodeDelayMax;
		Color color = Color.Lerp(this.color, new Color(0.5f, 0.1f, 0.1f, 1f), 0.5f);
		color.a = 0.9f;
		Color color2 = Color.Lerp(this.color, new Color(0.6f, 0.3f, 0.3f, 1f), 0.5f);
		color2.a = 0.8f;
		Color color3 = Color.Lerp(this.color, new Color(0.7f, 0.3f, 0.3f, 1f), 0.5f);
		color3.a = 0.5f;
		Color color4 = Color.Lerp(this.color, new Color(0.4f, 0.3f, 0.4f, 1f), 0.5f);
		color4.a = 0.2f;
		Color color5 = new Color(0.1f, 0.1f, 0.4f, 0f);
		this._glowEmitter.colorAnimation[0] = color;
		this._glowEmitter.colorAnimation[1] = color2;
		this._glowEmitter.colorAnimation[2] = color3;
		this._glowEmitter.colorAnimation[3] = color4;
		this._glowEmitter.colorAnimation[4] = color5;
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x00075B37 File Offset: 0x00073F37
	private void Update()
	{
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x00075B3C File Offset: 0x00073F3C
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

	// Token: 0x06000E20 RID: 3616 RVA: 0x00075B9D File Offset: 0x00073F9D
	public override void Explode()
	{
		if (this.detailThreshold > this.detail)
		{
			return;
		}
		if (this.on)
		{
			this.UpdateGlow();
			this._glowEmitter.Explode();
		}
	}

	// Token: 0x04000F06 RID: 3846
	private float _baseSize = 1f;

	// Token: 0x04000F07 RID: 3847
	private float _baseDuration = 3f;

	// Token: 0x04000F08 RID: 3848
	private Vector3 _baseVelocity = new Vector3(0f, 0f, 0f);

	// Token: 0x04000F09 RID: 3849
	private Color _baseColor = Color.black;

	// Token: 0x04000F0A RID: 3850
	private float _scaledDuration;

	// Token: 0x04000F0B RID: 3851
	private GameObject _glow;

	// Token: 0x04000F0C RID: 3852
	private DetonatorBurstEmitter _glowEmitter;

	// Token: 0x04000F0D RID: 3853
	public Material glowMaterial;
}
