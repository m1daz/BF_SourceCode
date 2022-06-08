using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000575 RID: 1397
[AddComponentMenu("NGUI/Interaction/Grid")]
public class UIGrid : UIWidgetContainer
{
	// Token: 0x170001F8 RID: 504
	// (set) Token: 0x06002706 RID: 9990 RVA: 0x00120494 File Offset: 0x0011E894
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

	// Token: 0x06002707 RID: 9991 RVA: 0x001204AC File Offset: 0x0011E8AC
	public List<Transform> GetChildList()
	{
		Transform transform = base.transform;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!this.hideInactive || (child && child.gameObject.activeSelf))
			{
				list.Add(child);
			}
		}
		if (this.sorting != UIGrid.Sorting.None && this.arrangement != UIGrid.Arrangement.CellSnap)
		{
			if (this.sorting == UIGrid.Sorting.Alphabetic)
			{
				List<Transform> list2 = list;
				if (UIGrid.<>f__mg$cache0 == null)
				{
					UIGrid.<>f__mg$cache0 = new Comparison<Transform>(UIGrid.SortByName);
				}
				list2.Sort(UIGrid.<>f__mg$cache0);
			}
			else if (this.sorting == UIGrid.Sorting.Horizontal)
			{
				List<Transform> list3 = list;
				if (UIGrid.<>f__mg$cache1 == null)
				{
					UIGrid.<>f__mg$cache1 = new Comparison<Transform>(UIGrid.SortHorizontal);
				}
				list3.Sort(UIGrid.<>f__mg$cache1);
			}
			else if (this.sorting == UIGrid.Sorting.Vertical)
			{
				List<Transform> list4 = list;
				if (UIGrid.<>f__mg$cache2 == null)
				{
					UIGrid.<>f__mg$cache2 = new Comparison<Transform>(UIGrid.SortVertical);
				}
				list4.Sort(UIGrid.<>f__mg$cache2);
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

	// Token: 0x06002708 RID: 9992 RVA: 0x001205EC File Offset: 0x0011E9EC
	public Transform GetChild(int index)
	{
		List<Transform> childList = this.GetChildList();
		return (index >= childList.Count) ? null : childList[index];
	}

	// Token: 0x06002709 RID: 9993 RVA: 0x00120619 File Offset: 0x0011EA19
	public int GetIndex(Transform trans)
	{
		return this.GetChildList().IndexOf(trans);
	}

	// Token: 0x0600270A RID: 9994 RVA: 0x00120627 File Offset: 0x0011EA27
	[Obsolete("Use gameObject.AddChild or transform.parent = gridTransform")]
	public void AddChild(Transform trans)
	{
		if (trans != null)
		{
			trans.parent = base.transform;
			this.ResetPosition(this.GetChildList());
		}
	}

	// Token: 0x0600270B RID: 9995 RVA: 0x0012064D File Offset: 0x0011EA4D
	[Obsolete("Use gameObject.AddChild or transform.parent = gridTransform")]
	public void AddChild(Transform trans, bool sort)
	{
		if (trans != null)
		{
			trans.parent = base.transform;
			this.ResetPosition(this.GetChildList());
		}
	}

	// Token: 0x0600270C RID: 9996 RVA: 0x00120674 File Offset: 0x0011EA74
	public bool RemoveChild(Transform t)
	{
		List<Transform> childList = this.GetChildList();
		if (childList.Remove(t))
		{
			this.ResetPosition(childList);
			return true;
		}
		return false;
	}

	// Token: 0x0600270D RID: 9997 RVA: 0x0012069E File Offset: 0x0011EA9E
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x0600270E RID: 9998 RVA: 0x001206B8 File Offset: 0x0011EAB8
	protected virtual void Start()
	{
		if (!this.mInitDone)
		{
			this.Init();
		}
		bool flag = this.animateSmoothly;
		this.animateSmoothly = false;
		this.Reposition();
		this.animateSmoothly = flag;
		base.enabled = false;
	}

	// Token: 0x0600270F RID: 9999 RVA: 0x001206F8 File Offset: 0x0011EAF8
	protected virtual void Update()
	{
		this.Reposition();
		base.enabled = false;
	}

	// Token: 0x06002710 RID: 10000 RVA: 0x00120707 File Offset: 0x0011EB07
	private void OnValidate()
	{
		if (!Application.isPlaying && NGUITools.GetActive(this))
		{
			this.Reposition();
		}
	}

	// Token: 0x06002711 RID: 10001 RVA: 0x00120724 File Offset: 0x0011EB24
	public static int SortByName(Transform a, Transform b)
	{
		return string.Compare(a.name, b.name);
	}

	// Token: 0x06002712 RID: 10002 RVA: 0x00120738 File Offset: 0x0011EB38
	public static int SortHorizontal(Transform a, Transform b)
	{
		return a.localPosition.x.CompareTo(b.localPosition.x);
	}

	// Token: 0x06002713 RID: 10003 RVA: 0x00120768 File Offset: 0x0011EB68
	public static int SortVertical(Transform a, Transform b)
	{
		return b.localPosition.y.CompareTo(a.localPosition.y);
	}

	// Token: 0x06002714 RID: 10004 RVA: 0x00120796 File Offset: 0x0011EB96
	protected virtual void Sort(List<Transform> list)
	{
	}

	// Token: 0x06002715 RID: 10005 RVA: 0x00120798 File Offset: 0x0011EB98
	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(base.gameObject))
		{
			this.Init();
		}
		if (this.sorted)
		{
			this.sorted = false;
			if (this.sorting == UIGrid.Sorting.None)
			{
				this.sorting = UIGrid.Sorting.Alphabetic;
			}
			NGUITools.SetDirty(this, "last change");
		}
		List<Transform> childList = this.GetChildList();
		this.ResetPosition(childList);
		if (this.keepWithinPanel)
		{
			this.ConstrainWithinPanel();
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	// Token: 0x06002716 RID: 10006 RVA: 0x00120834 File Offset: 0x0011EC34
	public void ConstrainWithinPanel()
	{
		if (this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(base.transform, true);
			UIScrollView component = this.mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
	}

	// Token: 0x06002717 RID: 10007 RVA: 0x00120884 File Offset: 0x0011EC84
	protected virtual void ResetPosition(List<Transform> list)
	{
		this.mReposition = false;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		Transform transform = base.transform;
		int i = 0;
		int count = list.Count;
		while (i < count)
		{
			Transform transform2 = list[i];
			Vector3 vector = transform2.localPosition;
			float z = vector.z;
			if (this.arrangement == UIGrid.Arrangement.CellSnap)
			{
				if (this.cellWidth > 0f)
				{
					vector.x = Mathf.Round(vector.x / this.cellWidth) * this.cellWidth;
				}
				if (this.cellHeight > 0f)
				{
					vector.y = Mathf.Round(vector.y / this.cellHeight) * this.cellHeight;
				}
			}
			else
			{
				vector = ((this.arrangement != UIGrid.Arrangement.Horizontal) ? new Vector3(this.cellWidth * (float)num2, -this.cellHeight * (float)num, z) : new Vector3(this.cellWidth * (float)num, -this.cellHeight * (float)num2, z));
			}
			if (this.animateSmoothly && Application.isPlaying && (this.pivot != UIWidget.Pivot.TopLeft || Vector3.SqrMagnitude(transform2.localPosition - vector) >= 0.0001f))
			{
				SpringPosition springPosition = SpringPosition.Begin(transform2.gameObject, vector, 15f);
				springPosition.updateScrollView = true;
				springPosition.ignoreTimeScale = true;
			}
			else
			{
				transform2.localPosition = vector;
			}
			num3 = Mathf.Max(num3, num);
			num4 = Mathf.Max(num4, num2);
			if (++num >= this.maxPerLine && this.maxPerLine > 0)
			{
				num = 0;
				num2++;
			}
			i++;
		}
		if (this.pivot != UIWidget.Pivot.TopLeft)
		{
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.pivot);
			float num5;
			float num6;
			if (this.arrangement == UIGrid.Arrangement.Horizontal)
			{
				num5 = Mathf.Lerp(0f, (float)num3 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num4) * this.cellHeight, 0f, pivotOffset.y);
			}
			else
			{
				num5 = Mathf.Lerp(0f, (float)num4 * this.cellWidth, pivotOffset.x);
				num6 = Mathf.Lerp((float)(-(float)num3) * this.cellHeight, 0f, pivotOffset.y);
			}
			foreach (Transform transform3 in list)
			{
				SpringPosition component = transform3.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
					SpringPosition springPosition2 = component;
					springPosition2.target.x = springPosition2.target.x - num5;
					SpringPosition springPosition3 = component;
					springPosition3.target.y = springPosition3.target.y - num6;
					component.enabled = true;
				}
				else
				{
					Vector3 localPosition = transform3.localPosition;
					localPosition.x -= num5;
					localPosition.y -= num6;
					transform3.localPosition = localPosition;
				}
			}
		}
	}

	// Token: 0x040027CB RID: 10187
	public UIGrid.Arrangement arrangement;

	// Token: 0x040027CC RID: 10188
	public UIGrid.Sorting sorting;

	// Token: 0x040027CD RID: 10189
	public UIWidget.Pivot pivot;

	// Token: 0x040027CE RID: 10190
	public int maxPerLine;

	// Token: 0x040027CF RID: 10191
	public float cellWidth = 200f;

	// Token: 0x040027D0 RID: 10192
	public float cellHeight = 200f;

	// Token: 0x040027D1 RID: 10193
	public bool animateSmoothly;

	// Token: 0x040027D2 RID: 10194
	public bool hideInactive;

	// Token: 0x040027D3 RID: 10195
	public bool keepWithinPanel;

	// Token: 0x040027D4 RID: 10196
	public UIGrid.OnReposition onReposition;

	// Token: 0x040027D5 RID: 10197
	public Comparison<Transform> onCustomSort;

	// Token: 0x040027D6 RID: 10198
	[HideInInspector]
	[SerializeField]
	private bool sorted;

	// Token: 0x040027D7 RID: 10199
	protected bool mReposition;

	// Token: 0x040027D8 RID: 10200
	protected UIPanel mPanel;

	// Token: 0x040027D9 RID: 10201
	protected bool mInitDone;

	// Token: 0x040027DA RID: 10202
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache0;

	// Token: 0x040027DB RID: 10203
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache1;

	// Token: 0x040027DC RID: 10204
	[CompilerGenerated]
	private static Comparison<Transform> <>f__mg$cache2;

	// Token: 0x02000576 RID: 1398
	// (Invoke) Token: 0x06002719 RID: 10009
	public delegate void OnReposition();

	// Token: 0x02000577 RID: 1399
	[DoNotObfuscateNGUI]
	public enum Arrangement
	{
		// Token: 0x040027DE RID: 10206
		Horizontal,
		// Token: 0x040027DF RID: 10207
		Vertical,
		// Token: 0x040027E0 RID: 10208
		CellSnap
	}

	// Token: 0x02000578 RID: 1400
	[DoNotObfuscateNGUI]
	public enum Sorting
	{
		// Token: 0x040027E2 RID: 10210
		None,
		// Token: 0x040027E3 RID: 10211
		Alphabetic,
		// Token: 0x040027E4 RID: 10212
		Horizontal,
		// Token: 0x040027E5 RID: 10213
		Vertical,
		// Token: 0x040027E6 RID: 10214
		Custom
	}
}
