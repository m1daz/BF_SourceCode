using System;
using UnityEngine;

// Token: 0x020005E6 RID: 1510
[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
	// Token: 0x17000297 RID: 663
	// (get) Token: 0x06002B0D RID: 11021 RVA: 0x0013F7B5 File Offset: 0x0013DBB5
	// (set) Token: 0x06002B0E RID: 11022 RVA: 0x0013F7BD File Offset: 0x0013DBBD
	[Obsolete("Use 'value' instead")]
	public float alpha
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

	// Token: 0x06002B0F RID: 11023 RVA: 0x0013F7C8 File Offset: 0x0013DBC8
	private void Cache()
	{
		this.mCached = true;
		this.mRect = base.GetComponent<UIRect>();
		this.mSr = base.GetComponent<SpriteRenderer>();
		if (this.mRect == null && this.mSr == null)
		{
			this.mLight = base.GetComponent<Light>();
			if (this.mLight == null)
			{
				Renderer component = base.GetComponent<Renderer>();
				if (component != null)
				{
					this.mMat = component.material;
				}
				if (this.mMat == null)
				{
					this.mRect = base.GetComponentInChildren<UIRect>();
				}
			}
			else
			{
				this.mBaseIntensity = this.mLight.intensity;
			}
		}
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06002B10 RID: 11024 RVA: 0x0013F888 File Offset: 0x0013DC88
	// (set) Token: 0x06002B11 RID: 11025 RVA: 0x0013F918 File Offset: 0x0013DD18
	public float value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRect != null)
			{
				return this.mRect.alpha;
			}
			if (this.mSr != null)
			{
				return this.mSr.color.a;
			}
			return (!(this.mMat != null)) ? 1f : this.mMat.color.a;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mRect != null)
			{
				this.mRect.alpha = value;
			}
			else if (this.mSr != null)
			{
				Color color = this.mSr.color;
				color.a = value;
				this.mSr.color = color;
			}
			else if (this.mMat != null)
			{
				Color color2 = this.mMat.color;
				color2.a = value;
				this.mMat.color = color2;
			}
			else if (this.mLight != null)
			{
				this.mLight.intensity = this.mBaseIntensity * value;
			}
		}
	}

	// Token: 0x06002B12 RID: 11026 RVA: 0x0013F9E8 File Offset: 0x0013DDE8
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x06002B13 RID: 11027 RVA: 0x0013FA04 File Offset: 0x0013DE04
	public static TweenAlpha Begin(GameObject go, float duration, float alpha, float delay = 0f)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration, delay);
		tweenAlpha.from = tweenAlpha.value;
		tweenAlpha.to = alpha;
		if (duration <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.enabled = false;
		}
		return tweenAlpha;
	}

	// Token: 0x06002B14 RID: 11028 RVA: 0x0013FA4C File Offset: 0x0013DE4C
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06002B15 RID: 11029 RVA: 0x0013FA5A File Offset: 0x0013DE5A
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04002AC0 RID: 10944
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x04002AC1 RID: 10945
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x04002AC2 RID: 10946
	private bool mCached;

	// Token: 0x04002AC3 RID: 10947
	private UIRect mRect;

	// Token: 0x04002AC4 RID: 10948
	private Material mMat;

	// Token: 0x04002AC5 RID: 10949
	private Light mLight;

	// Token: 0x04002AC6 RID: 10950
	private SpriteRenderer mSr;

	// Token: 0x04002AC7 RID: 10951
	private float mBaseIntensity = 1f;
}
