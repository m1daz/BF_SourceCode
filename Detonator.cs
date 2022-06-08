using System;
using UnityEngine;

// Token: 0x020001FD RID: 509
[AddComponentMenu("Detonator/Detonator")]
public class Detonator : MonoBehaviour
{
	// Token: 0x06000DDD RID: 3549 RVA: 0x00072F88 File Offset: 0x00071388
	private void Awake()
	{
		this.FillDefaultMaterials();
		this.components = base.GetComponents(typeof(DetonatorComponent));
		foreach (DetonatorComponent detonatorComponent in this.components)
		{
			if (detonatorComponent is DetonatorFireball)
			{
				this._fireball = (detonatorComponent as DetonatorFireball);
			}
			if (detonatorComponent is DetonatorSparks)
			{
				this._sparks = (detonatorComponent as DetonatorSparks);
			}
			if (detonatorComponent is DetonatorShockwave)
			{
				this._shockwave = (detonatorComponent as DetonatorShockwave);
			}
			if (detonatorComponent is DetonatorSmoke)
			{
				this._smoke = (detonatorComponent as DetonatorSmoke);
			}
			if (detonatorComponent is DetonatorGlow)
			{
				this._glow = (detonatorComponent as DetonatorGlow);
			}
			if (detonatorComponent is DetonatorLight)
			{
				this._light = (detonatorComponent as DetonatorLight);
			}
			if (detonatorComponent is DetonatorForce)
			{
				this._force = (detonatorComponent as DetonatorForce);
			}
			if (detonatorComponent is DetonatorHeatwave)
			{
				this._heatwave = (detonatorComponent as DetonatorHeatwave);
			}
		}
		if (!this._fireball && this.autoCreateFireball)
		{
			this._fireball = base.gameObject.AddComponent<DetonatorFireball>();
			this._fireball.Reset();
		}
		if (!this._smoke && this.autoCreateSmoke)
		{
			this._smoke = base.gameObject.AddComponent<DetonatorSmoke>();
			this._smoke.Reset();
		}
		if (!this._sparks && this.autoCreateSparks)
		{
			this._sparks = base.gameObject.AddComponent<DetonatorSparks>();
			this._sparks.Reset();
		}
		if (!this._shockwave && this.autoCreateShockwave)
		{
			this._shockwave = base.gameObject.AddComponent<DetonatorShockwave>();
			this._shockwave.Reset();
		}
		if (!this._glow && this.autoCreateGlow)
		{
			this._glow = base.gameObject.AddComponent<DetonatorGlow>();
			this._glow.Reset();
		}
		if (!this._light && this.autoCreateLight)
		{
			this._light = base.gameObject.AddComponent<DetonatorLight>();
			this._light.Reset();
		}
		if (!this._force && this.autoCreateForce)
		{
			this._force = base.gameObject.AddComponent<DetonatorForce>();
			this._force.Reset();
		}
		if (!this._heatwave && this.autoCreateHeatwave && SystemInfo.supportsImageEffects)
		{
			this._heatwave = base.gameObject.AddComponent<DetonatorHeatwave>();
			this._heatwave.Reset();
		}
		this.components = base.GetComponents(typeof(DetonatorComponent));
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x00073268 File Offset: 0x00071668
	private void FillDefaultMaterials()
	{
		if (!this.fireballAMaterial)
		{
			this.fireballAMaterial = Detonator.DefaultFireballAMaterial();
		}
		if (!this.fireballBMaterial)
		{
			this.fireballBMaterial = Detonator.DefaultFireballBMaterial();
		}
		if (!this.smokeAMaterial)
		{
			this.smokeAMaterial = Detonator.DefaultSmokeAMaterial();
		}
		if (!this.smokeBMaterial)
		{
			this.smokeBMaterial = Detonator.DefaultSmokeBMaterial();
		}
		if (!this.shockwaveMaterial)
		{
			this.shockwaveMaterial = Detonator.DefaultShockwaveMaterial();
		}
		if (!this.sparksMaterial)
		{
			this.sparksMaterial = Detonator.DefaultSparksMaterial();
		}
		if (!this.glowMaterial)
		{
			this.glowMaterial = Detonator.DefaultGlowMaterial();
		}
		if (!this.heatwaveMaterial)
		{
			this.heatwaveMaterial = Detonator.DefaultHeatwaveMaterial();
		}
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x0007334D File Offset: 0x0007174D
	private void Start()
	{
		if (this.explodeOnStart)
		{
			this.UpdateComponents();
			this.Explode();
		}
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x00073366 File Offset: 0x00071766
	private void Update()
	{
		if (this.destroyTime > 0f && this._lastExplosionTime + this.destroyTime <= Time.time)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x0007339C File Offset: 0x0007179C
	private void UpdateComponents()
	{
		if (this._firstComponentUpdate)
		{
			foreach (DetonatorComponent detonatorComponent in this.components)
			{
				detonatorComponent.Init();
				detonatorComponent.SetStartValues();
			}
			this._firstComponentUpdate = false;
		}
		if (!this._firstComponentUpdate)
		{
			foreach (DetonatorComponent detonatorComponent2 in this.components)
			{
				if (detonatorComponent2.detonatorControlled)
				{
					detonatorComponent2.size = detonatorComponent2.startSize * (this.size / Detonator._baseSize);
					detonatorComponent2.timeScale = this.duration / Detonator._baseDuration;
					detonatorComponent2.detail = detonatorComponent2.startDetail * this.detail;
					detonatorComponent2.force = detonatorComponent2.startForce * (this.size / Detonator._baseSize) + this.direction * (this.size / Detonator._baseSize);
					detonatorComponent2.velocity = detonatorComponent2.startVelocity * (this.size / Detonator._baseSize) + this.direction * (this.size / Detonator._baseSize);
					detonatorComponent2.color = Color.Lerp(detonatorComponent2.startColor, this.color, this.color.a);
				}
			}
		}
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x00073500 File Offset: 0x00071900
	public void Explode()
	{
		this._lastExplosionTime = Time.time;
		foreach (DetonatorComponent detonatorComponent in this.components)
		{
			this.UpdateComponents();
			detonatorComponent.Explode();
		}
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x00073548 File Offset: 0x00071948
	public void Reset()
	{
		this.size = 10f;
		this.color = Detonator._baseColor;
		this.duration = Detonator._baseDuration;
		this.FillDefaultMaterials();
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x00073574 File Offset: 0x00071974
	public static Material DefaultFireballAMaterial()
	{
		if (Detonator.defaultFireballAMaterial != null)
		{
			return Detonator.defaultFireballAMaterial;
		}
		Detonator.defaultFireballAMaterial = new Material(Shader.Find("Particles/Additive"));
		Detonator.defaultFireballAMaterial.name = "FireballA-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Fireball") as Texture2D;
		Detonator.defaultFireballAMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultFireballAMaterial.mainTexture = mainTexture;
		Detonator.defaultFireballAMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		return Detonator.defaultFireballAMaterial;
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x00073608 File Offset: 0x00071A08
	public static Material DefaultFireballBMaterial()
	{
		if (Detonator.defaultFireballBMaterial != null)
		{
			return Detonator.defaultFireballBMaterial;
		}
		Detonator.defaultFireballBMaterial = new Material(Shader.Find("Particles/Additive"));
		Detonator.defaultFireballBMaterial.name = "FireballB-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Fireball") as Texture2D;
		Detonator.defaultFireballBMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultFireballBMaterial.mainTexture = mainTexture;
		Detonator.defaultFireballBMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		Detonator.defaultFireballBMaterial.mainTextureOffset = new Vector2(0.5f, 0f);
		return Detonator.defaultFireballBMaterial;
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x000736B4 File Offset: 0x00071AB4
	public static Material DefaultSmokeAMaterial()
	{
		if (Detonator.defaultSmokeAMaterial != null)
		{
			return Detonator.defaultSmokeAMaterial;
		}
		Detonator.defaultSmokeAMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
		Detonator.defaultSmokeAMaterial.name = "SmokeA-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Smoke") as Texture2D;
		Detonator.defaultSmokeAMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultSmokeAMaterial.mainTexture = mainTexture;
		Detonator.defaultSmokeAMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		return Detonator.defaultSmokeAMaterial;
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x00073748 File Offset: 0x00071B48
	public static Material DefaultSmokeBMaterial()
	{
		if (Detonator.defaultSmokeBMaterial != null)
		{
			return Detonator.defaultSmokeBMaterial;
		}
		Detonator.defaultSmokeBMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
		Detonator.defaultSmokeBMaterial.name = "SmokeB-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Smoke") as Texture2D;
		Detonator.defaultSmokeBMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultSmokeBMaterial.mainTexture = mainTexture;
		Detonator.defaultSmokeBMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		Detonator.defaultSmokeBMaterial.mainTextureOffset = new Vector2(0.5f, 0f);
		return Detonator.defaultSmokeBMaterial;
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x000737F4 File Offset: 0x00071BF4
	public static Material DefaultSparksMaterial()
	{
		if (Detonator.defaultSparksMaterial != null)
		{
			return Detonator.defaultSparksMaterial;
		}
		Detonator.defaultSparksMaterial = new Material(Shader.Find("Particles/Additive"));
		Detonator.defaultSparksMaterial.name = "Sparks-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/GlowDot") as Texture2D;
		Detonator.defaultSparksMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultSparksMaterial.mainTexture = mainTexture;
		return Detonator.defaultSparksMaterial;
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x00073870 File Offset: 0x00071C70
	public static Material DefaultShockwaveMaterial()
	{
		if (Detonator.defaultShockwaveMaterial != null)
		{
			return Detonator.defaultShockwaveMaterial;
		}
		Detonator.defaultShockwaveMaterial = new Material(Shader.Find("Particles/Additive"));
		Detonator.defaultShockwaveMaterial.name = "Shockwave-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Shockwave") as Texture2D;
		Detonator.defaultShockwaveMaterial.SetColor("_TintColor", new Color(0.1f, 0.1f, 0.1f, 1f));
		Detonator.defaultShockwaveMaterial.mainTexture = mainTexture;
		return Detonator.defaultShockwaveMaterial;
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x00073900 File Offset: 0x00071D00
	public static Material DefaultGlowMaterial()
	{
		if (Detonator.defaultGlowMaterial != null)
		{
			return Detonator.defaultGlowMaterial;
		}
		Detonator.defaultGlowMaterial = new Material(Shader.Find("Particles/Additive"));
		Detonator.defaultGlowMaterial.name = "Glow-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Glow") as Texture2D;
		Detonator.defaultGlowMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultGlowMaterial.mainTexture = mainTexture;
		return Detonator.defaultGlowMaterial;
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x0007397C File Offset: 0x00071D7C
	public static Material DefaultHeatwaveMaterial()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			return null;
		}
		if (Detonator.defaultHeatwaveMaterial != null)
		{
			return Detonator.defaultHeatwaveMaterial;
		}
		Detonator.defaultHeatwaveMaterial = new Material(Shader.Find("HeatDistort"));
		Detonator.defaultHeatwaveMaterial.name = "Heatwave-Default";
		Texture2D value = Resources.Load("Detonator/Textures/Heatwave") as Texture2D;
		Detonator.defaultHeatwaveMaterial.SetTexture("_BumpMap", value);
		return Detonator.defaultHeatwaveMaterial;
	}

	// Token: 0x04000E71 RID: 3697
	private static float _baseSize = 30f;

	// Token: 0x04000E72 RID: 3698
	private static Color _baseColor = new Color(1f, 0.423f, 0f, 0.5f);

	// Token: 0x04000E73 RID: 3699
	private static float _baseDuration = 3f;

	// Token: 0x04000E74 RID: 3700
	public float size = 10f;

	// Token: 0x04000E75 RID: 3701
	public Color color = Detonator._baseColor;

	// Token: 0x04000E76 RID: 3702
	public bool explodeOnStart = true;

	// Token: 0x04000E77 RID: 3703
	public float duration = Detonator._baseDuration;

	// Token: 0x04000E78 RID: 3704
	public float detail = 1f;

	// Token: 0x04000E79 RID: 3705
	public float upwardsBias;

	// Token: 0x04000E7A RID: 3706
	public float destroyTime = 7f;

	// Token: 0x04000E7B RID: 3707
	public bool useWorldSpace = true;

	// Token: 0x04000E7C RID: 3708
	public Vector3 direction = Vector3.zero;

	// Token: 0x04000E7D RID: 3709
	public Material fireballAMaterial;

	// Token: 0x04000E7E RID: 3710
	public Material fireballBMaterial;

	// Token: 0x04000E7F RID: 3711
	public Material smokeAMaterial;

	// Token: 0x04000E80 RID: 3712
	public Material smokeBMaterial;

	// Token: 0x04000E81 RID: 3713
	public Material shockwaveMaterial;

	// Token: 0x04000E82 RID: 3714
	public Material sparksMaterial;

	// Token: 0x04000E83 RID: 3715
	public Material glowMaterial;

	// Token: 0x04000E84 RID: 3716
	public Material heatwaveMaterial;

	// Token: 0x04000E85 RID: 3717
	private Component[] components;

	// Token: 0x04000E86 RID: 3718
	private DetonatorFireball _fireball;

	// Token: 0x04000E87 RID: 3719
	private DetonatorSparks _sparks;

	// Token: 0x04000E88 RID: 3720
	private DetonatorShockwave _shockwave;

	// Token: 0x04000E89 RID: 3721
	private DetonatorSmoke _smoke;

	// Token: 0x04000E8A RID: 3722
	private DetonatorGlow _glow;

	// Token: 0x04000E8B RID: 3723
	private DetonatorLight _light;

	// Token: 0x04000E8C RID: 3724
	private DetonatorForce _force;

	// Token: 0x04000E8D RID: 3725
	private DetonatorHeatwave _heatwave;

	// Token: 0x04000E8E RID: 3726
	public bool autoCreateFireball = true;

	// Token: 0x04000E8F RID: 3727
	public bool autoCreateSparks = true;

	// Token: 0x04000E90 RID: 3728
	public bool autoCreateShockwave = true;

	// Token: 0x04000E91 RID: 3729
	public bool autoCreateSmoke = true;

	// Token: 0x04000E92 RID: 3730
	public bool autoCreateGlow = true;

	// Token: 0x04000E93 RID: 3731
	public bool autoCreateLight = true;

	// Token: 0x04000E94 RID: 3732
	public bool autoCreateForce = true;

	// Token: 0x04000E95 RID: 3733
	public bool autoCreateHeatwave;

	// Token: 0x04000E96 RID: 3734
	private float _lastExplosionTime = 1000f;

	// Token: 0x04000E97 RID: 3735
	private bool _firstComponentUpdate = true;

	// Token: 0x04000E98 RID: 3736
	private Component[] _subDetonators;

	// Token: 0x04000E99 RID: 3737
	public static Material defaultFireballAMaterial;

	// Token: 0x04000E9A RID: 3738
	public static Material defaultFireballBMaterial;

	// Token: 0x04000E9B RID: 3739
	public static Material defaultSmokeAMaterial;

	// Token: 0x04000E9C RID: 3740
	public static Material defaultSmokeBMaterial;

	// Token: 0x04000E9D RID: 3741
	public static Material defaultShockwaveMaterial;

	// Token: 0x04000E9E RID: 3742
	public static Material defaultSparksMaterial;

	// Token: 0x04000E9F RID: 3743
	public static Material defaultGlowMaterial;

	// Token: 0x04000EA0 RID: 3744
	public static Material defaultHeatwaveMaterial;
}
