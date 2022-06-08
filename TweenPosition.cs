using System;
using UnityEngine;

// Token: 0x020005F0 RID: 1520
[AddComponentMenu("NGUI/Tween/Tween Position")]
public class TweenPosition : UITweener
{
	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x06002B5A RID: 11098 RVA: 0x001408A0 File Offset: 0x0013ECA0
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x06002B5B RID: 11099 RVA: 0x001408C5 File Offset: 0x0013ECC5
	// (set) Token: 0x06002B5C RID: 11100 RVA: 0x001408CD File Offset: 0x0013ECCD
	[Obsolete("Use 'value' instead")]
	public Vector3 position
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

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06002B5D RID: 11101 RVA: 0x001408D6 File Offset: 0x0013ECD6
	// (set) Token: 0x06002B5E RID: 11102 RVA: 0x00140900 File Offset: 0x0013ED00
	public Vector3 value
	{
		get
		{
			return (!this.worldSpace) ? this.cachedTransform.localPosition : this.cachedTransform.position;
		}
		set
		{
			if (this.mRect == null || !this.mRect.isAnchored || this.worldSpace)
			{
				if (this.worldSpace)
				{
					this.cachedTransform.position = value;
				}
				else
				{
					this.cachedTransform.localPosition = value;
				}
			}
			else
			{
				value -= this.cachedTransform.localPosition;
				NGUIMath.MoveRect(this.mRect, value.x, value.y);
			}
		}
	}

	// Token: 0x06002B5F RID: 11103 RVA: 0x00140992 File Offset: 0x0013ED92
	private void Awake()
	{
		this.mRect = base.GetComponent<UIRect>();
	}

	// Token: 0x06002B60 RID: 11104 RVA: 0x001409A0 File Offset: 0x0013EDA0
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06002B61 RID: 11105 RVA: 0x001409CC File Offset: 0x0013EDCC
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration, 0f);
		tweenPosition.from = tweenPosition.value;
		tweenPosition.to = pos;
		if (duration <= 0f)
		{
			tweenPosition.Sample(1f, true);
			tweenPosition.enabled = false;
		}
		return tweenPosition;
	}

	// Token: 0x06002B62 RID: 11106 RVA: 0x00140A18 File Offset: 0x0013EE18
	public static TweenPosition Begin(GameObject go, float duration, Vector3 pos, bool worldSpace)
	{
		TweenPosition tweenPosition = UITweener.Begin<TweenPosition>(go, duration, 0f);
		tweenPosition.worldSpace = worldSpace;
		tweenPosition.from = tweenPosition.value;
		tweenPosition.to = pos;
		if (duration <= 0f)
		{
			tweenPosition.Sample(1f, true);
			tweenPosition.enabled = false;
		}
		return tweenPosition;
	}

	// Token: 0x06002B63 RID: 11107 RVA: 0x00140A6B File Offset: 0x0013EE6B
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06002B64 RID: 11108 RVA: 0x00140A79 File Offset: 0x0013EE79
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06002B65 RID: 11109 RVA: 0x00140A87 File Offset: 0x0013EE87
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06002B66 RID: 11110 RVA: 0x00140A95 File Offset: 0x0013EE95
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04002AF5 RID: 10997
	public Vector3 from;

	// Token: 0x04002AF6 RID: 10998
	public Vector3 to;

	// Token: 0x04002AF7 RID: 10999
	[HideInInspector]
	public bool worldSpace;

	// Token: 0x04002AF8 RID: 11000
	private Transform mTrans;

	// Token: 0x04002AF9 RID: 11001
	private UIRect mRect;
}
