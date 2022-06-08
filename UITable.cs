using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000597 RID: 1431
[AddComponentMenu("NGUI/Interaction/Table")]
public class UITable : UIWidgetContainer
{
	// Token: 0x17000222 RID: 546
	// (set) Token: 0x06002816 RID: 10262 RVA: 0x001278D6 File Offset: 0x00125CD6
	public bool repositionNow
	{
		set
		{
			if (value)
			{
				this.mReposition = true;
				base.enabled = true;
			}
		}
	}

	// Token: 0x06002817 RID: 10263 RVA: 0x001278EC File Offset: 0x00125CEC
	public List<Transform> GetChildList()
	{
		Transform transform = base.transform;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!this.hideInactive || (child && NGUITools.GetActive(child.gameObject)))
			{
				list.Add(child);
			}
		}
		if (this.sorting != UITable.Sorting.None)
		{
			if (this.sorting == UITable.Sorting.Alphabetic)
			{
				List<Transform> list2 = list;
				if (UITable.<>f__mg$cache0 == null)
				{
					UITable.<>f__mg$cache0 = new Comparison<Transform>(UIGrid.SortByName);
				}
				list2.Sort(UITable.<>f__mg$cache0);
			}
			else if (this.sorting == UITable.Sorting.Horizontal)
			{
				List<Transform> list3 = list;
				if (UITable.<>f__mg$cache1 == null)
				{
					UITable.<>f__mg$cache1 = new Comparison<Transform>(UIGrid.SortHorizontal);
				}
				list3.Sort(UITable.<>f__mg$cache1);
			}
			else if (this.sorting == UITable.Sorting.Vertical)
			{
				List<Transform> list4 = list;
				if (UITable.<>f__mg$cache2 == null)
				{
					UITable.<>f__mg$cache2 = new Comparison<Transform>(UIGrid.SortVertical);
				}
				list4.Sort(UITable.<>f__mg$cache2);
			}
			else if (this.onCustomSort != null)
			{
				list.Sort(this.onCustomSort);
			}
			else
			{
				this.Sort(list);
			}
		}
		return list;
	}

	// Token: 0x06002818 RID: 10264 RVA: 0x00127A1D File Offset: 0x00125E1D
	protected virtual void Sort(List<Transform> list)
	{
		if (UITable.<>f__mg$cache3 == null)
		{
			UITable.<>f__mg$cache3 = new Comparison<Transform>(UIGrid.SortByName);
		}
		list.Sort(UITable.<>f__mg$cache3);
	}

	// Token: 0x06002819 RID: 10265 RVA: 0x00127A42 File Offset: 0x00125E42
	protected virtual void Start()
	{
		this.Init();
		this.Reposition();
		base.enabled = false;
	}

	// Token: 0x0600281A RID: 10266 RVA: 0x00127A57 File Offset: 0x00125E57
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x0600281B RID: 10267 RVA: 0x00127A71 File Offset: 0x00125E71
	protected virtual void LateUpdate()
	{
		if (this.mReposition)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	// Token: 0x0600281C RID: 10268 RVA: 0x00127A8B File Offset: 0x00125E8B
	private void OnValidate()
	{
		if (!Application.isPlaying && NGUITools.GetActive(this))
		{
			this.Reposition();
		}
	}

	// Token: 0x0600281D RID: 10269 RVA: 0x00127AA8 File Offset: 0x00125EA8
	protected void RepositionVariableSize(List<Transform> children)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = (this.columns <= 0) ? 1 : (children.Count / this.columns + 1);
		int num4 = (this.columns <= 0) ? children.Count : this.columns;
		Bounds[,] array = new Bounds[num3, num4];
		Bounds[] array2 = new Bounds[num4];
		Bounds[] array3 = new Bounds[num3];
		int num5 = 0;
		int num6 = 0;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Transform transform = children[i];
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform, !this.hideInactive);
			Vector3 localScale = transform.localScale;
			bounds.min = Vector3.Scale(bounds.min, localScale);
			bounds.max = Vector3.Scale(bounds.max, localScale);
			array[num6, num5] = bounds;
			array2[num5].Encapsulate(bounds);
			array3[num6].Encapsulate(bounds);
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
			}
			i++;
		}
		num5 = 0;
		num6 = 0;
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.cellAlignment);
		int j = 0;
		int count2 = children.Count;
		while (j < count2)
		{
			Transform transform2 = children[j];
			Bounds bounds2 = array[num6, num5];
			Bounds bounds3 = array2[num5];
			Bounds bounds4 = array3[num6];
			Vector3 localPosition = transform2.localPosition;
			localPosition.x = num + bounds2.extents.x - bounds2.center.x;
			localPosition.x -= Mathf.Lerp(0f, bounds2.max.x - bounds2.min.x - bounds3.max.x + bounds3.min.x, pivotOffset.x) - this.padding.x;
			if (this.direction == UITable.Direction.Down)
			{
				localPosition.y = -num2 - bounds2.extents.y - bounds2.center.y;
				localPosition.y += Mathf.Lerp(bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y, 0f, pivotOffset.y) - this.padding.y;
			}
			else
			{
				localPosition.y = num2 + bounds2.extents.y - bounds2.center.y;
				localPosition.y -= Mathf.Lerp(0f, bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y, pivotOffset.y) - this.padding.y;
			}
			num += bounds3.size.x + this.padding.x * 2f;
			transform2.localPosition = localPosition;
			if (++num5 >= this.columns && this.columns > 0)
			{
				num5 = 0;
				num6++;
				num = 0f;
				num2 += bounds4.size.y + this.padding.y * 2f;
			}
			j++;
		}
		if (this.pivot != UIWidget.Pivot.TopLeft)
		{
			pivotOffset = NGUIMath.GetPivotOffset(this.pivot);
			Bounds bounds5 = NGUIMath.CalculateRelativeWidgetBounds(base.transform);
			float num7 = Mathf.Lerp(0f, bounds5.size.x, pivotOffset.x);
			float num8 = Mathf.Lerp(-bounds5.size.y, 0f, pivotOffset.y);
			Transform transform3 = base.transform;
			for (int k = 0; k < transform3.childCount; k++)
			{
				Transform child = transform3.GetChild(k);
				SpringPosition component = child.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
					SpringPosition springPosition = component;
					springPosition.target.x = springPosition.target.x - num7;
					SpringPosition springPosition2 = component;
					springPosition2.target.y = springPosition2.target.y - num8;
					component.enabled = true;
				}
				else
				{
					Vector3 localPosition2 = child.localPosition;
					localPosition2.x -= num7;
					localPosition2.y -= num8;
					child.localPosition = localPosition2;
				}
			}
		}
	}

	// Token: 0x0600281E RID: 10270 RVA: 0x00127FD4 File Offset: 0x001263D4
	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(this))
		{
			this.Init();
		}
		this.mReposition = false;
		Transform transform = base.transform;
		List<Transform> childList = this.GetChildList();
		if (childList.Count > 0)
		{
			this.RepositionVariableSize(childList);
		}
		if (this.keepWithinPanel && this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(transform, true);
			UIScrollView component = this.mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	// Token: 0x040028DA RID: 10458
	public int columns;

	// Token: 0x040028DB RID: 10459
	public UITable.Direction direction;

	// Token: 0x040028DC RID: 10460
	public UITable.Sorting sorting;

	// Token: 0x040028DD RID: 10461
	public UIWidget.Pivot pivot;

	// Token: 0x040028DE RID: 10462
	public UIWidget.Pivot cellAlignment;

	// Token: 0x040028DF RID: 10463
	public bool hideInactive = true;

	// Token: 0x040028E0 RID: 10464
	public bool keepWithinPanel;

	// Token: 0x040028E1 RID: 10465
	public Vector2 padding = Vector2.zero;

	// Token: 0x040028E2 RID: 10466
	public UITable.OnReposition onReposition;

	// Token: 0x040028E3 RID: 10467
	public Comparison<Transform> onCustomSort;

	// Token: 0x040028E4 RID: 10468
	protected UIPanel mPanel;

	// Token: 0x040028E5 RID: 10469
	protected bool mInitDone;

	// Token: 0x040028E6 RID: 10470
	protected bool mReposition;

	// Token: 0x040028E7 RID: 10471
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache0;

	// Token: 0x040028E8 RID: 10472
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache1;

	// Token: 0x040028E9 RID: 10473
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache2;

	// Token: 0x040028EA RID: 10474
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache3;

	// Token: 0x02000598 RID: 1432
	// (Invoke) Token: 0x06002820 RID: 10272
	public delegate void OnReposition();

	// Token: 0x02000599 RID: 1433
	[DoNotObfuscateNGUI]
	public enum Direction
	{
		// Token: 0x040028EC RID: 10476
		Down,
		// Token: 0x040028ED RID: 10477
		Up
	}

	// Token: 0x0200059A RID: 1434
	[DoNotObfuscateNGUI]
	public enum Sorting
	{
		// Token: 0x040028EF RID: 10479
		None,
		// Token: 0x040028F0 RID: 10480
		Alphabetic,
		// Token: 0x040028F1 RID: 10481
		Horizontal,
		// Token: 0x040028F2 RID: 10482
		Vertical,
		// Token: 0x040028F3 RID: 10483
		Custom
	}
}
