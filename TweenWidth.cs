using System;
using UnityEngine;

// Token: 0x020005F5 RID: 1525
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/Tween/Tween Width")]
public class TweenWidth : UITweener
{
	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x06002B8E RID: 11150 RVA: 0x001411A5 File Offset: 0x0013F5A5
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

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06002B8F RID: 11151 RVA: 0x001411CA File Offset: 0x0013F5CA
	// (set) Token: 0x06002B90 RID: 11152 RVA: 0x001411D2 File Offset: 0x0013F5D2
	[Obsolete("Use 'value' instead")]
	public int width
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

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x06002B91 RID: 11153 RVA: 0x001411DB File Offset: 0x0013F5DB
	// (set) Token: 0x06002B92 RID: 11154 RVA: 0x001411E8 File Offset: 0x0013F5E8
	public int value
	{
		get
		{
			return this.cachedWidget.width;
		}
		set
		{
			this.cachedWidget.width = value;
		}
	}

	// Token: 0x06002B93 RID: 11155 RVA: 0x001411F8 File Offset: 0x0013F5F8
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

	// Token: 0x06002B94 RID: 11156 RVA: 0x0014127C File Offset: 0x0013F67C
	public static TweenWidth Begin(UIWidget widget, float duration, int width)
	{
		TweenWidth tweenWidth = UITweener.Begin<TweenWidth>(widget.gameObject, duration, 0f);
		tweenWidth.from = widget.width;
		tweenWidth.to = width;
		if (duration <= 0f)
		{
			tweenWidth.Sample(1f, true);
			tweenWidth.enabled = false;
		}
		return tweenWidth;
	}

	// Token: 0x06002B95 RID: 11157 RVA: 0x001412CD File Offset: 0x0013F6CD
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06002B96 RID: 11158 RVA: 0x001412DB File Offset: 0x0013F6DB
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06002B97 RID: 11159 RVA: 0x001412E9 File Offset: 0x0013F6E9
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06002B98 RID: 11160 RVA: 0x001412F7 File Offset: 0x0013F6F7
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04002B0D RID: 11021
	public int from = 100;

	// Token: 0x04002B0E RID: 11022
	public int to = 100;

	// Token: 0x04002B0F RID: 11023
	public bool updateTable;

	// Token: 0x04002B10 RID: 11024
	private UIWidget mWidget;

	// Token: 0x04002B11 RID: 11025
	private UITable mTable;
}
