using System;
using UnityEngine;

// Token: 0x02000208 RID: 520
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Sound")]
public class DetonatorSound : DetonatorComponent
{
	// Token: 0x06000E3C RID: 3644 RVA: 0x00076B0A File Offset: 0x00074F0A
	public override void Init()
	{
		this._soundComponent = base.gameObject.AddComponent<AudioSource>();
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x00076B20 File Offset: 0x00074F20
	private void Update()
	{
		this._soundComponent.pitch = Time.timeScale;
		if (this._delayedExplosionStarted)
		{
			this._explodeDelay -= Time.deltaTime;
			if (this._explodeDelay <= 0f)
			{
				this.Explode();
			}
		}
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x00076B70 File Offset: 0x00074F70
	public override void Explode()
	{
		if (this.detailThreshold > this.detail)
		{
			return;
		}
		if (!this._delayedExplosionStarted)
		{
			this._explodeDelay = this.explodeDelayMin + UnityEngine.Random.value * (this.explodeDelayMax - this.explodeDelayMin);
		}
		if (this._explodeDelay <= 0f)
		{
			if (Vector3.Distance(Camera.main.transform.position, base.transform.position) < this.distanceThreshold)
			{
				this._idx = (int)(UnityEngine.Random.value * (float)this.nearSounds.Length);
				this._soundComponent.PlayOneShot(this.nearSounds[this._idx]);
			}
			else
			{
				this._idx = (int)(UnityEngine.Random.value * (float)this.farSounds.Length);
				this._soundComponent.PlayOneShot(this.farSounds[this._idx]);
			}
			this._delayedExplosionStarted = false;
			this._explodeDelay = 0f;
		}
		else
		{
			this._delayedExplosionStarted = true;
		}
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x00076C73 File Offset: 0x00075073
	public void Reset()
	{
	}

	// Token: 0x04000F38 RID: 3896
	public AudioClip[] nearSounds;

	// Token: 0x04000F39 RID: 3897
	public AudioClip[] farSounds;

	// Token: 0x04000F3A RID: 3898
	public float distanceThreshold = 50f;

	// Token: 0x04000F3B RID: 3899
	public float minVolume = 0.4f;

	// Token: 0x04000F3C RID: 3900
	public float maxVolume = 1f;

	// Token: 0x04000F3D RID: 3901
	public float rolloffFactor = 0.5f;

	// Token: 0x04000F3E RID: 3902
	private AudioSource _soundComponent;

	// Token: 0x04000F3F RID: 3903
	private bool _delayedExplosionStarted;

	// Token: 0x04000F40 RID: 3904
	private float _explodeDelay;

	// Token: 0x04000F41 RID: 3905
	private int _idx;
}
