using System;
using UnityEngine;

// Token: 0x020005EA RID: 1514
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Height")]
public class TweenHeight : UITweener
{
	// Token: 0x1700029F RID: 671
	// (get) Token: 0x06002B37 RID: 11063 RVA: 0x0013FF7C File Offset: 0x0013E37C
	public UIWidget cachedWidget
	{
		get
		{
			if (this.mWidget == null)
			{
				this.mWidget = base.GetComponent<UIWidget>();
			}
			return this.mWidget;
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x06002B38 RID: 11064 RVA: 0x0013FFA1 File Offset: 0x0013E3A1
	// (set) Token: 0x06002B39 RID: 11065 RVA: 0x0013FFA9 File Offset: 0x0013E3A9
	[Obsolete("Use 'value' instead")]
	public int height
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

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x06002B3A RID: 11066 RVA: 0x0013FFB2 File Offset: 0x0013E3B2
	// (set) Token: 0x06002B3B RID: 11067 RVA: 0x0013FFBF File Offset: 0x0013E3BF
	public int value
	{
		get
		{
			return this.cachedWidget.height;
		}
		set
		{
			this.cachedWidget.height = value;
		}
	}

	// Token: 0x06002B3C RID: 11068 RVA: 0x0013FFD0 File Offset: 0x0013E3D0
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.RoundToInt((float)this.from * (1f - factor) + (float)this.to * factor);
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

	// Token: 0x06002B3D RID: 11069 RVA: 0x00140054 File Offset: 0x0013E454
	public static TweenHeight Begin(UIWidget widget, float duration, int height)
	{
		TweenHeight tweenHeight = UITweener.Begin<TweenHeight>(widget.gameObject, duration, 0f);
		tweenHeight.from = widget.height;
		tweenHeight.to = height;
		if (duration <= 0f)
		{
			tweenHeight.Sample(1f, true);
			tweenHeight.enabled = false;
		}
		return tweenHeight;
	}

	// Token: 0x06002B3E RID: 11070 RVA: 0x001400A5 File Offset: 0x0013E4A5
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06002B3F RID: 11071 RVA: 0x001400B3 File Offset: 0x0013E4B3
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06002B40 RID: 11072 RVA: 0x001400C1 File Offset: 0x0013E4C1
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06002B41 RID: 11073 RVA: 0x001400CF File Offset: 0x0013E4CF
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04002AD6 RID: 10966
	public int from = 100;

	// Token: 0x04002AD7 RID: 10967
	public int to = 100;

	// Token: 0x04002AD8 RID: 10968
	public bool updateTable;

	// Token: 0x04002AD9 RID: 10969
	private UIWidget mWidget;

	// Token: 0x04002ADA RID: 10970
	private UITable mTable;
}
