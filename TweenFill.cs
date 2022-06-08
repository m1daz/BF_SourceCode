using System;
using UnityEngine;

// Token: 0x020005E8 RID: 1512
[RequireComponent(typeof(UIBasicSprite))]
[AddComponentMenu("NGUI/Tween/Tween Fill")]
public class TweenFill : UITweener
{
	// Token: 0x06002B23 RID: 11043 RVA: 0x0013FD52 File Offset: 0x0013E152
	private void Cache()
	{
		this.mCached = true;
		this.mSprite = base.GetComponent<UISprite>();
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06002B24 RID: 11044 RVA: 0x0013FD67 File Offset: 0x0013E167
	// (set) Token: 0x06002B25 RID: 11045 RVA: 0x0013FD9C File Offset: 0x0013E19C
	public float value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mSprite != null)
			{
				return this.mSprite.fillAmount;
			}
			return 0f;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mSprite != null)
			{
				this.mSprite.fillAmount = value;
			}
		}
	}

	// Token: 0x06002B26 RID: 11046 RVA: 0x0013FDCC File Offset: 0x0013E1CC
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	// Token: 0x06002B27 RID: 11047 RVA: 0x0013FDE8 File Offset: 0x0013E1E8
	public static TweenFill Begin(GameObject go, float duration, float fill)
	{
		TweenFill tweenFill = UITweener.Begin<TweenFill>(go, duration, 0f);
		tweenFill.from = tweenFill.value;
		tweenFill.to = fill;
		if (duration <= 0f)
		{
			tweenFill.Sample(1f, true);
			tweenFill.enabled = false;
		}
		return tweenFill;
	}

	// Token: 0x06002B28 RID: 11048 RVA: 0x0013FE34 File Offset: 0x0013E234
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06002B29 RID: 11049 RVA: 0x0013FE42 File Offset: 0x0013E242
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04002ACF RID: 10959
	[Range(0f, 1f)]
	public float from = 1f;

	// Token: 0x04002AD0 RID: 10960
	[Range(0f, 1f)]
	public float to = 1f;

	// Token: 0x04002AD1 RID: 10961
	private bool mCached;

	// Token: 0x04002AD2 RID: 10962
	private UIBasicSprite mSprite;
}
