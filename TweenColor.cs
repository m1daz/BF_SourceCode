using System;
using UnityEngine;

// Token: 0x020005E7 RID: 1511
[AddComponentMenu("NGUI/Tween/Tween Color")]
public class TweenColor : UITweener
{
	// Token: 0x06002B17 RID: 11031 RVA: 0x0013FA88 File Offset: 0x0013DE88
	private void Cache()
	{
		this.mCached = true;
		this.mWidget = base.GetComponent<UIWidget>();
		if (this.mWidget != null)
		{
			return;
		}
		this.mSr = base.GetComponent<SpriteRenderer>();
		if (this.mSr != null)
		{
			return;
		}
		Renderer component = base.GetComponent<Renderer>();
		if (component != null)
		{
			this.mMat = component.material;
			return;
		}
		this.mLight = base.GetComponent<Light>();
		if (this.mLight == null)
		{
			this.mWidget = base.GetComponentInChildren<UIWidget>();
		}
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06002B18 RID: 11032 RVA: 0x0013FB21 File Offset: 0x0013DF21
	// (set) Token: 0x06002B19 RID: 11033 RVA: 0x0013FB29 File Offset: 0x0013DF29
	[Obsolete("Use 'value' instead")]
	public Color color
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

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06002B1A RID: 11034 RVA: 0x0013FB34 File Offset: 0x0013DF34
	// (set) Token: 0x06002B1B RID: 11035 RVA: 0x0013FBCC File Offset: 0x0013DFCC
	public Color value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				return this.mWidget.color;
			}
			if (this.mMat != null)
			{
				return this.mMat.color;
			}
			if (this.mSr != null)
			{
				return this.mSr.color;
			}
			if (this.mLight != null)
			{
				return this.mLight.color;
			}
			return Color.black;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mWidget != null)
			{
				this.mWidget.color = value;
			}
			else if (this.mMat != null)
			{
				this.mMat.color = value;
			}
			else if (this.mSr != null)
			{
				this.mSr.color = value;
			}
			else if (this.mLight != null)
			{
				this.mLight.color = value;
				this.mLight.enabled = (value.r + value.g + value.b > 0.01f);
			}
		}
	}

	// Token: 0x06002B1C RID: 11036 RVA: 0x0013FC96 File Offset: 0x0013E096
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Color.Lerp(this.from, this.to, factor);
	}

	// Token: 0x06002B1D RID: 11037 RVA: 0x0013FCB0 File Offset: 0x0013E0B0
	public static TweenColor Begin(GameObject go, float duration, Color color)
	{
		TweenColor tweenColor = UITweener.Begin<TweenColor>(go, duration, 0f);
		tweenColor.from = tweenColor.value;
		tweenColor.to = color;
		if (duration <= 0f)
		{
			tweenColor.Sample(1f, true);
			tweenColor.enabled = false;
		}
		return tweenColor;
	}

	// Token: 0x06002B1E RID: 11038 RVA: 0x0013FCFC File Offset: 0x0013E0FC
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06002B1F RID: 11039 RVA: 0x0013FD0A File Offset: 0x0013E10A
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06002B20 RID: 11040 RVA: 0x0013FD18 File Offset: 0x0013E118
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06002B21 RID: 11041 RVA: 0x0013FD26 File Offset: 0x0013E126
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04002AC8 RID: 10952
	public Color from = Color.white;

	// Token: 0x04002AC9 RID: 10953
	public Color to = Color.white;

	// Token: 0x04002ACA RID: 10954
	private bool mCached;

	// Token: 0x04002ACB RID: 10955
	private UIWidget mWidget;

	// Token: 0x04002ACC RID: 10956
	private Material mMat;

	// Token: 0x04002ACD RID: 10957
	private Light mLight;

	// Token: 0x04002ACE RID: 10958
	private SpriteRenderer mSr;
}
