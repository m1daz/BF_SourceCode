using System;
using UnityEngine;

// Token: 0x02000204 RID: 516
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Heatwave (Pro Only)")]
public class DetonatorHeatwave : DetonatorComponent
{
	// Token: 0x06000E22 RID: 3618 RVA: 0x00075BF6 File Offset: 0x00073FF6
	public override void Init()
	{
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x00075BF8 File Offset: 0x00073FF8
	private void Update()
	{
		if (this._delayedExplosionStarted)
		{
			this._explodeDelay -= Time.deltaTime;
			if (this._explodeDelay <= 0f)
			{
				this.Explode();
			}
		}
		if (this._heatwave)
		{
			this._heatwave.transform.rotation = Quaternion.FromToRotation(Vector3.up, Camera.main.transform.position - this._heatwave.transform.position);
			this._heatwave.transform.localPosition = this.localPosition + Vector3.forward * this.zOffset;
			this._elapsedTime += Time.deltaTime;
			this._normalizedTime = this._elapsedTime / this.duration;
			this.s = Mathf.Lerp(this._startSize, this._maxSize, this._normalizedTime);
			this._heatwave.GetComponent<Renderer>().material.SetFloat("_BumpAmt", (1f - this._normalizedTime) * this.distortion);
			this._heatwave.gameObject.transform.localScale = new Vector3(this.s, this.s, this.s);
			if (this._elapsedTime > this.duration)
			{
				UnityEngine.Object.Destroy(this._heatwave.gameObject);
			}
		}
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x00075D70 File Offset: 0x00074170
	public override void Explode()
	{
		if (SystemInfo.supportsImageEffects)
		{
			if (this.detailThreshold > this.detail || !this.on)
			{
				return;
			}
			if (!this._delayedExplosionStarted)
			{
				this._explodeDelay = this.explodeDelayMin + UnityEngine.Random.value * (this.explodeDelayMax - this.explodeDelayMin);
			}
			if (this._explodeDelay <= 0f)
			{
				this._startSize = 0f;
				this._maxSize = this.size * 10f;
				this._material = new Material(Shader.Find("HeatDistort"));
				this._heatwave = GameObject.CreatePrimitive(PrimitiveType.Plane);
				UnityEngine.Object.Destroy(this._heatwave.GetComponent(typeof(MeshCollider)));
				if (!this.heatwaveMaterial)
				{
					this.heatwaveMaterial = base.MyDetonator().heatwaveMaterial;
				}
				this._material.CopyPropertiesFromMaterial(this.heatwaveMaterial);
				this._heatwave.GetComponent<Renderer>().material = this._material;
				this._heatwave.transform.parent = base.transform;
				this._delayedExplosionStarted = false;
				this._explodeDelay = 0f;
			}
			else
			{
				this._delayedExplosionStarted = true;
			}
		}
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x00075EB3 File Offset: 0x000742B3
	public void Reset()
	{
		this.duration = this._baseDuration;
	}

	// Token: 0x04000F0E RID: 3854
	private GameObject _heatwave;

	// Token: 0x04000F0F RID: 3855
	private float s;

	// Token: 0x04000F10 RID: 3856
	private float _startSize;

	// Token: 0x04000F11 RID: 3857
	private float _maxSize;

	// Token: 0x04000F12 RID: 3858
	private float _baseDuration = 0.25f;

	// Token: 0x04000F13 RID: 3859
	private bool _delayedExplosionStarted;

	// Token: 0x04000F14 RID: 3860
	private float _explodeDelay;

	// Token: 0x04000F15 RID: 3861
	public float zOffset = 0.5f;

	// Token: 0x04000F16 RID: 3862
	public float distortion = 64f;

	// Token: 0x04000F17 RID: 3863
	private float _elapsedTime;

	// Token: 0x04000F18 RID: 3864
	private float _normalizedTime;

	// Token: 0x04000F19 RID: 3865
	public Material heatwaveMaterial;

	// Token: 0x04000F1A RID: 3866
	private Material _material;
}
