using System;
using UnityEngine;

// Token: 0x02000205 RID: 517
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Light")]
public class DetonatorLight : DetonatorComponent
{
	// Token: 0x06000E27 RID: 3623 RVA: 0x00075EEC File Offset: 0x000742EC
	public override void Init()
	{
		this._light = new GameObject("Light");
		this._light.transform.parent = base.transform;
		this._light.transform.localPosition = this.localPosition;
		this._lightComponent = this._light.AddComponent<Light>();
		this._lightComponent.type = LightType.Point;
		this._lightComponent.enabled = false;
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x00075F60 File Offset: 0x00074360
	private void Update()
	{
		if (this._explodeTime + this._scaledDuration > Time.time && this._lightComponent.intensity > 0f)
		{
			this._reduceAmount = this.intensity * (Time.deltaTime / this._scaledDuration);
			this._lightComponent.intensity -= this._reduceAmount;
		}
		else if (this._lightComponent)
		{
			this._lightComponent.enabled = false;
		}
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x00075FEC File Offset: 0x000743EC
	public override void Explode()
	{
		if (this.detailThreshold > this.detail)
		{
			return;
		}
		this._lightComponent.color = this.color;
		this._lightComponent.range = this.size * 50f;
		this._scaledDuration = this.duration * this.timeScale;
		this._lightComponent.enabled = true;
		this._lightComponent.intensity = this.intensity;
		this._explodeTime = Time.time;
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x0007606E File Offset: 0x0007446E
	public void Reset()
	{
		this.color = this._baseColor;
		this.intensity = this._baseIntensity;
	}

	// Token: 0x04000F1B RID: 3867
	private float _baseIntensity = 1f;

	// Token: 0x04000F1C RID: 3868
	private Color _baseColor = Color.white;

	// Token: 0x04000F1D RID: 3869
	private float _scaledDuration;

	// Token: 0x04000F1E RID: 3870
	private float _explodeTime = -1000f;

	// Token: 0x04000F1F RID: 3871
	private GameObject _light;

	// Token: 0x04000F20 RID: 3872
	private Light _lightComponent;

	// Token: 0x04000F21 RID: 3873
	public float intensity;

	// Token: 0x04000F22 RID: 3874
	private float _reduceAmount;
}
