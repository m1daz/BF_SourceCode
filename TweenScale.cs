using System;
using UnityEngine;

// Token: 0x020005F2 RID: 1522
[AddComponentMenu("NGUI/Tween/Tween Scale")]
public class TweenScale : UITweener
{
	// Token: 0x170002AB RID: 683
	// (get) Token: 0x06002B74 RID: 11124 RVA: 0x00140C81 File Offset: 0x0013F081
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

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x06002B75 RID: 11125 RVA: 0x00140CA6 File Offset: 0x0013F0A6
	// (set) Token: 0x06002B76 RID: 11126 RVA: 0x00140CB3 File Offset: 0x0013F0B3
	public Vector3 value
	{
		get
		{
			return this.cachedTransform.localScale;
		}
		set
		{
			this.cachedTransform.localScale = value;
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06002B77 RID: 11127 RVA: 0x00140CC1 File Offset: 0x0013F0C1
	// (set) Token: 0x06002B78 RID: 11128 RVA: 0x00140CC9 File Offset: 0x0013F0C9
	[Obsolete("Use 'value' instead")]
	public Vector3 scale
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

	// Token: 0x06002B79 RID: 11129 RVA: 0x00140CD4 File Offset: 0x0013F0D4
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
		if (this.updateTable)
		{
			if (this.mTable == null)
			{
				this.mTable = NGUITools.FindInParents<UITable>(base.gameObject);
				if (this.mTable == null)
				{
					this.updateTable = false;
					return;
				}
			}
			this.mTable.repositionNow = true;
		}
	}

	// Token: 0x06002B7A RID: 11130 RVA: 0x00140D5C File Offset: 0x0013F15C
	public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
	{
		TweenScale tweenScale = UITweener.Begin<TweenScale>(go, duration, 0f);
		tweenScale.from = tweenScale.value;
		tweenScale.to = scale;
		if (duration <= 0f)
		{
			tweenScale.Sample(1f, true);
			tweenScale.enabled = false;
		}
		return tweenScale;
	}

	// Token: 0x06002B7B RID: 11131 RVA: 0x00140DA8 File Offset: 0x0013F1A8
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06002B7C RID: 11132 RVA: 0x00140DB6 File Offset: 0x0013F1B6
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06002B7D RID: 11133 RVA: 0x00140DC4 File Offset: 0x0013F1C4
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06002B7E RID: 11134 RVA: 0x00140DD2 File Offset: 0x0013F1D2
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04002AFE RID: 11006
	public Vector3 from = Vector3.one;

	// Token: 0x04002AFF RID: 11007
	public Vector3 to = Vector3.one;

	// Token: 0x04002B00 RID: 11008
	public bool updateTable;

	// Token: 0x04002B01 RID: 11009
	private Transform mTrans;

	// Token: 0x04002B02 RID: 11010
	private UITable mTable;
}
