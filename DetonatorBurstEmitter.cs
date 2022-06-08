using System;
using UnityEngine;

// Token: 0x020001FE RID: 510
public class DetonatorBurstEmitter : DetonatorComponent
{
	// Token: 0x06000DEE RID: 3566 RVA: 0x00073AE7 File Offset: 0x00071EE7
	public override void Init()
	{
		MonoBehaviour.print("UNUSED");
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x00073AF4 File Offset: 0x00071EF4
	public void Awake()
	{
		this._particleEmitter = base.gameObject.AddComponent<EllipsoidParticleEmitter>();
		this._particleRenderer = base.gameObject.AddComponent<ParticleRenderer>();
		this._particleAnimator = base.gameObject.AddComponent<ParticleAnimator>();
		this._particleEmitter.hideFlags = HideFlags.HideAndDontSave;
		this._particleRenderer.hideFlags = HideFlags.HideAndDontSave;
		this._particleAnimator.hideFlags = HideFlags.HideAndDontSave;
		this._particleAnimator.damping = this._baseDamping;
		this._particleEmitter.emit = false;
		this._particleRenderer.maxParticleSize = this.maxScreenSize;
		this._particleRenderer.material = this.material;
		this._particleRenderer.material.color = Color.white;
		this._particleAnimator.sizeGrow = this.sizeGrow;
		if (this.explodeOnAwake)
		{
			this.Explode();
		}
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x00073BD4 File Offset: 0x00071FD4
	private void Update()
	{
		if (this.exponentialGrowth)
		{
			float num = Time.time - this._emitTime;
			float num2 = this.SizeFunction(num - DetonatorBurstEmitter.epsilon);
			float num3 = this.SizeFunction(num);
			float num4 = (num3 / num2 - 1f) / DetonatorBurstEmitter.epsilon;
			this._particleAnimator.sizeGrow = num4;
		}
		else
		{
			this._particleAnimator.sizeGrow = this.sizeGrow;
		}
		if (this._delayedExplosionStarted)
		{
			this._explodeDelay -= Time.deltaTime;
			if (this._explodeDelay <= 0f)
			{
				this.Explode();
			}
		}
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x00073C74 File Offset: 0x00072074
	private float SizeFunction(float elapsedTime)
	{
		float num = 1f - 1f / (1f + elapsedTime * this.speed);
		return this.initFraction + (1f - this.initFraction) * num;
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x00073CB1 File Offset: 0x000720B1
	public void Reset()
	{
		this.size = this._baseSize;
		this.color = this._baseColor;
		this.damping = this._baseDamping;
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x00073CD8 File Offset: 0x000720D8
	public override void Explode()
	{
		if (this.on)
		{
			this._particleEmitter.useWorldSpace = this.useWorldSpace;
			this._scaledDuration = this.timeScale * this.duration;
			this._scaledDurationVariation = this.timeScale * this.durationVariation;
			this._scaledStartRadius = this.size * this.startRadius;
			this._particleRenderer.particleRenderMode = this.renderMode;
			if (!this._delayedExplosionStarted)
			{
				this._explodeDelay = this.explodeDelayMin + UnityEngine.Random.value * (this.explodeDelayMax - this.explodeDelayMin);
			}
			if (this._explodeDelay <= 0f)
			{
				Color[] array = this._particleAnimator.colorAnimation;
				if (this.useExplicitColorAnimation)
				{
					array[0] = this.colorAnimation[0];
					array[1] = this.colorAnimation[1];
					array[2] = this.colorAnimation[2];
					array[3] = this.colorAnimation[3];
					array[4] = this.colorAnimation[4];
				}
				else
				{
					array[0] = new Color(this.color.r, this.color.g, this.color.b, this.color.a * 0.7f);
					array[1] = new Color(this.color.r, this.color.g, this.color.b, this.color.a * 1f);
					array[2] = new Color(this.color.r, this.color.g, this.color.b, this.color.a * 0.5f);
					array[3] = new Color(this.color.r, this.color.g, this.color.b, this.color.a * 0.3f);
					array[4] = new Color(this.color.r, this.color.g, this.color.b, this.color.a * 0f);
				}
				this._particleAnimator.colorAnimation = array;
				this._particleRenderer.material = this.material;
				this._particleAnimator.force = this.force;
				this._tmpCount = this.count * this.detail;
				if (this._tmpCount < 1f)
				{
					this._tmpCount = 1f;
				}
				if (this._particleEmitter.useWorldSpace)
				{
					this._thisPos = base.gameObject.transform.position;
				}
				else
				{
					this._thisPos = new Vector3(0f, 0f, 0f);
				}
				int num = 1;
				while ((float)num <= this._tmpCount)
				{
					this._tmpPos = Vector3.Scale(UnityEngine.Random.insideUnitSphere, new Vector3(this._scaledStartRadius, this._scaledStartRadius, this._scaledStartRadius));
					this._tmpPos = this._thisPos + this._tmpPos;
					this._tmpDir = Vector3.Scale(UnityEngine.Random.insideUnitSphere, new Vector3(this.velocity.x, this.velocity.y, this.velocity.z));
					this._tmpDir.y = this._tmpDir.y + 2f * (Mathf.Abs(this._tmpDir.y) * this.upwardsBias);
					if (this.randomRotation)
					{
						this._randomizedRotation = UnityEngine.Random.Range(-1f, 1f);
						this._tmpAngularVelocity = UnityEngine.Random.Range(-1f, 1f) * this.angularVelocity;
					}
					else
					{
						this._randomizedRotation = 0f;
						this._tmpAngularVelocity = this.angularVelocity;
					}
					this._tmpDir = Vector3.Scale(this._tmpDir, new Vector3(this.size, this.size, this.size));
					this._tmpParticleSize = this.size * (this.particleSize + UnityEngine.Random.value * this.sizeVariation);
					this._tmpDuration = this._scaledDuration + UnityEngine.Random.value * this._scaledDurationVariation;
					this._particleEmitter.Emit(this._tmpPos, this._tmpDir, this._tmpParticleSize, this._tmpDuration, this.color, this._randomizedRotation, this._tmpAngularVelocity);
					num++;
				}
				this._emitTime = Time.time;
				this._delayedExplosionStarted = false;
				this._explodeDelay = 0f;
			}
			else
			{
				this._delayedExplosionStarted = true;
			}
		}
	}

	// Token: 0x04000EA1 RID: 3745
	private ParticleEmitter _particleEmitter;

	// Token: 0x04000EA2 RID: 3746
	private ParticleRenderer _particleRenderer;

	// Token: 0x04000EA3 RID: 3747
	private ParticleAnimator _particleAnimator;

	// Token: 0x04000EA4 RID: 3748
	private float _baseDamping = 0.1300004f;

	// Token: 0x04000EA5 RID: 3749
	private float _baseSize = 1f;

	// Token: 0x04000EA6 RID: 3750
	private Color _baseColor = Color.white;

	// Token: 0x04000EA7 RID: 3751
	public float damping = 1f;

	// Token: 0x04000EA8 RID: 3752
	public float startRadius = 1f;

	// Token: 0x04000EA9 RID: 3753
	public float maxScreenSize = 2f;

	// Token: 0x04000EAA RID: 3754
	public bool explodeOnAwake;

	// Token: 0x04000EAB RID: 3755
	public bool oneShot = true;

	// Token: 0x04000EAC RID: 3756
	public float sizeVariation;

	// Token: 0x04000EAD RID: 3757
	public float particleSize = 1f;

	// Token: 0x04000EAE RID: 3758
	public float count = 1f;

	// Token: 0x04000EAF RID: 3759
	public float sizeGrow = 20f;

	// Token: 0x04000EB0 RID: 3760
	public bool exponentialGrowth = true;

	// Token: 0x04000EB1 RID: 3761
	public float durationVariation;

	// Token: 0x04000EB2 RID: 3762
	public bool useWorldSpace = true;

	// Token: 0x04000EB3 RID: 3763
	public float upwardsBias;

	// Token: 0x04000EB4 RID: 3764
	public float angularVelocity = 20f;

	// Token: 0x04000EB5 RID: 3765
	public bool randomRotation = true;

	// Token: 0x04000EB6 RID: 3766
	public ParticleRenderMode renderMode;

	// Token: 0x04000EB7 RID: 3767
	public bool useExplicitColorAnimation;

	// Token: 0x04000EB8 RID: 3768
	public Color[] colorAnimation = new Color[5];

	// Token: 0x04000EB9 RID: 3769
	private bool _delayedExplosionStarted;

	// Token: 0x04000EBA RID: 3770
	private float _explodeDelay;

	// Token: 0x04000EBB RID: 3771
	public Material material;

	// Token: 0x04000EBC RID: 3772
	private float _emitTime;

	// Token: 0x04000EBD RID: 3773
	private float speed = 3f;

	// Token: 0x04000EBE RID: 3774
	private float initFraction = 0.1f;

	// Token: 0x04000EBF RID: 3775
	private static float epsilon = 0.01f;

	// Token: 0x04000EC0 RID: 3776
	private float _tmpParticleSize;

	// Token: 0x04000EC1 RID: 3777
	private Vector3 _tmpPos;

	// Token: 0x04000EC2 RID: 3778
	private Vector3 _tmpDir;

	// Token: 0x04000EC3 RID: 3779
	private Vector3 _thisPos;

	// Token: 0x04000EC4 RID: 3780
	private float _tmpDuration;

	// Token: 0x04000EC5 RID: 3781
	private float _tmpCount;

	// Token: 0x04000EC6 RID: 3782
	private float _scaledDuration;

	// Token: 0x04000EC7 RID: 3783
	private float _scaledDurationVariation;

	// Token: 0x04000EC8 RID: 3784
	private float _scaledStartRadius;

	// Token: 0x04000EC9 RID: 3785
	private float _scaledColor;

	// Token: 0x04000ECA RID: 3786
	private float _randomizedRotation;

	// Token: 0x04000ECB RID: 3787
	private float _tmpAngularVelocity;
}
