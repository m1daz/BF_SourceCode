using System;
using UnityEngine;

// Token: 0x020005F4 RID: 1524
[RequireComponent(typeof(AudioSource))]
[AddComponentMenu("NGUI/Tween/Tween Volume")]
public class TweenVolume : UITweener
{
	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06002B84 RID: 11140 RVA: 0x00141020 File Offset: 0x0013F420
	public AudioSource audioSource
	{
		get
		{
			if (this.mSource == null)
			{
				this.mSource = base.GetComponent<AudioSource>();
				if (this.mSource == null)
				{
					this.mSource = base.GetComponent<AudioSource>();
					if (this.mSource == null)
					{
						Debug.LogError("TweenVolume needs an AudioSource to work with", this);
						base.enabled = false;
					}
				}
			}
			return this.mSource;
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06002B85 RID: 11141 RVA: 0x00141090 File Offset: 0x0013F490
	// (set) Token: 0x06002B86 RID: 11142 RVA: 0x00141098 File Offset: 0x0013F498
	[Obsolete("Use 'value' instead")]
	public float volume
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06002B87 RID: 11143 RVA: 0x001410A1 File Offset: 0x0013F4A1
	// (set) Token: 0x06002B88 RID: 11144 RVA: 0x001410C9 File Offset: 0x0013F4C9
	public float value
	{
		get
		{
			return (!(this.audioSource != null)) ? 0f : this.mSource.volume;
		}
		set
		{
			if (this.audioSource != null)
			{
				this.mSource.volume = value;
			}
		}
	}

	// Token: 0x06002B89 RID: 11145 RVA: 0x001410E8 File Offset: 0x0013F4E8
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
		this.mSource.enabled = (this.mSource.volume > 0.01f);
	}

	// Token: 0x06002B8A RID: 11146 RVA: 0x00141124 File Offset: 0x0013F524
	public static TweenVolume Begin(GameObject go, float duration, float targetVolume)
	{
		TweenVolume tweenVolume = UITweener.Begin<TweenVolume>(go, duration, 0f);
		tweenVolume.from = tweenVolume.value;
		tweenVolume.to = targetVolume;
		if (targetVolume > 0f)
		{
			AudioSource audioSource = tweenVolume.audioSource;
			audioSource.enabled = true;
			audioSource.Play();
		}
		return tweenVolume;
	}

	// Token: 0x06002B8B RID: 11147 RVA: 0x00141171 File Offset: 0x0013F571
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06002B8C RID: 11148 RVA: 0x0014117F File Offset: 0x0013F57F
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04002B0A RID: 11018
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x04002B0B RID: 11019
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x04002B0C RID: 11020
	private AudioSource mSource;
}
