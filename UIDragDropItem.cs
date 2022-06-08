using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200056B RID: 1387
[AddComponentMenu("NGUI/Interaction/Drag and Drop Item")]
public class UIDragDropItem : MonoBehaviour
{
	// Token: 0x060026B3 RID: 9907 RVA: 0x0011A7E5 File Offset: 0x00118BE5
	protected virtual void Awake()
	{
		this.mTrans = base.transform;
		this.mCollider = base.gameObject.GetComponent<Collider>();
		this.mCollider2D = base.gameObject.GetComponent<Collider2D>();
	}

	// Token: 0x060026B4 RID: 9908 RVA: 0x0011A815 File Offset: 0x00118C15
	protected virtual void OnEnable()
	{
	}

	// Token: 0x060026B5 RID: 9909 RVA: 0x0011A817 File Offset: 0x00118C17
	protected virtual void OnDisable()
	{
		if (this.mDragging)
		{
			this.StopDragging(UICamera.hoveredObject);
		}
	}

	// Token: 0x060026B6 RID: 9910 RVA: 0x0011A82F File Offset: 0x00118C2F
	protected virtual void Start()
	{
		this.mButton = base.GetComponent<UIButton>();
		this.mDragScrollView = base.GetComponent<UIDragScrollView>();
	}

	// Token: 0x060026B7 RID: 9911 RVA: 0x0011A84C File Offset: 0x00118C4C
	protected virtual void OnPress(bool isPressed)
	{
		if (!this.interactable || UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (isPressed)
		{
			if (!this.mPressed)
			{
				this.mTouch = UICamera.currentTouch;
				this.mDragStartTime = RealTime.time + this.pressAndHoldDelay;
				this.mPressed = true;
			}
		}
		else if (this.mPressed && this.mTouch == UICamera.currentTouch)
		{
			this.mPressed = false;
			this.mTouch = null;
		}
	}

	// Token: 0x060026B8 RID: 9912 RVA: 0x0011A8E0 File Offset: 0x00118CE0
	protected virtual void Update()
	{
		if (this.restriction == UIDragDropItem.Restriction.PressAndHold && this.mPressed && !this.mDragging && this.mDragStartTime < RealTime.time)
		{
			this.StartDragging();
		}
	}

	// Token: 0x060026B9 RID: 9913 RVA: 0x0011A91C File Offset: 0x00118D1C
	protected virtual void OnDragStart()
	{
		if (!this.interactable)
		{
			return;
		}
		if (!base.enabled || this.mTouch != UICamera.currentTouch)
		{
			return;
		}
		if (this.restriction != UIDragDropItem.Restriction.None)
		{
			if (this.restriction == UIDragDropItem.Restriction.Horizontal)
			{
				Vector2 totalDelta = this.mTouch.totalDelta;
				if (Mathf.Abs(totalDelta.x) < Mathf.Abs(totalDelta.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragDropItem.Restriction.Vertical)
			{
				Vector2 totalDelta2 = this.mTouch.totalDelta;
				if (Mathf.Abs(totalDelta2.x) > Mathf.Abs(totalDelta2.y))
				{
					return;
				}
			}
			else if (this.restriction == UIDragDropItem.Restriction.PressAndHold)
			{
				return;
			}
		}
		this.StartDragging();
	}

	// Token: 0x060026BA RID: 9914 RVA: 0x0011A9E8 File Offset: 0x00118DE8
	public virtual void StartDragging()
	{
		if (!this.interactable)
		{
			return;
		}
		if (!this.mDragging)
		{
			if (this.cloneOnDrag)
			{
				this.mPressed = false;
				GameObject gameObject = base.transform.parent.gameObject.AddChild(base.gameObject);
				gameObject.transform.localPosition = base.transform.localPosition;
				gameObject.transform.localRotation = base.transform.localRotation;
				gameObject.transform.localScale = base.transform.localScale;
				UIButtonColor component = gameObject.GetComponent<UIButtonColor>();
				if (component != null)
				{
					component.defaultColor = base.GetComponent<UIButtonColor>().defaultColor;
				}
				if (this.mTouch != null && this.mTouch.pressed == base.gameObject)
				{
					this.mTouch.current = gameObject;
					this.mTouch.pressed = gameObject;
					this.mTouch.dragged = gameObject;
					this.mTouch.last = gameObject;
				}
				UIDragDropItem component2 = gameObject.GetComponent<UIDragDropItem>();
				component2.mTouch = this.mTouch;
				component2.mPressed = true;
				component2.mDragging = true;
				component2.Start();
				component2.OnClone(base.gameObject);
				component2.OnDragDropStart();
				if (UICamera.currentTouch == null)
				{
					UICamera.currentTouch = this.mTouch;
				}
				this.mTouch = null;
				UICamera.Notify(base.gameObject, "OnPress", false);
				UICamera.Notify(base.gameObject, "OnHover", false);
			}
			else
			{
				this.mDragging = true;
				this.OnDragDropStart();
			}
		}
	}

	// Token: 0x060026BB RID: 9915 RVA: 0x0011AB89 File Offset: 0x00118F89
	protected virtual void OnClone(GameObject original)
	{
	}

	// Token: 0x060026BC RID: 9916 RVA: 0x0011AB8C File Offset: 0x00118F8C
	protected virtual void OnDrag(Vector2 delta)
	{
		if (!this.interactable)
		{
			return;
		}
		if (!this.mDragging || !base.enabled || this.mTouch != UICamera.currentTouch)
		{
			return;
		}
		if (this.mRoot != null)
		{
			this.OnDragDropMove(delta * this.mRoot.pixelSizeAdjustment);
		}
		else
		{
			this.OnDragDropMove(delta);
		}
	}

	// Token: 0x060026BD RID: 9917 RVA: 0x0011AC00 File Offset: 0x00119000
	protected virtual void OnDragEnd()
	{
		if (!this.interactable)
		{
			return;
		}
		if (!base.enabled || this.mTouch != UICamera.currentTouch)
		{
			return;
		}
		this.StopDragging(UICamera.hoveredObject);
	}

	// Token: 0x060026BE RID: 9918 RVA: 0x0011AC35 File Offset: 0x00119035
	public void StopDragging(GameObject go = null)
	{
		if (this.mDragging)
		{
			this.mDragging = false;
			this.OnDragDropRelease(go);
		}
	}

	// Token: 0x060026BF RID: 9919 RVA: 0x0011AC50 File Offset: 0x00119050
	protected virtual void OnDragDropStart()
	{
		if (!UIDragDropItem.draggedItems.Contains(this))
		{
			UIDragDropItem.draggedItems.Add(this);
		}
		if (this.mDragScrollView != null)
		{
			this.mDragScrollView.enabled = false;
		}
		if (this.mButton != null)
		{
			this.mButton.isEnabled = false;
		}
		else if (this.mCollider != null)
		{
			this.mCollider.enabled = false;
		}
		else if (this.mCollider2D != null)
		{
			this.mCollider2D.enabled = false;
		}
		this.mParent = this.mTrans.parent;
		this.mRoot = NGUITools.FindInParents<UIRoot>(this.mParent);
		this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
		this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
		if (UIDragDropRoot.root != null)
		{
			this.mTrans.parent = UIDragDropRoot.root;
		}
		Vector3 localPosition = this.mTrans.localPosition;
		localPosition.z = 0f;
		this.mTrans.localPosition = localPosition;
		TweenPosition component = base.GetComponent<TweenPosition>();
		if (component != null)
		{
			component.enabled = false;
		}
		SpringPosition component2 = base.GetComponent<SpringPosition>();
		if (component2 != null)
		{
			component2.enabled = false;
		}
		NGUITools.MarkParentAsChanged(base.gameObject);
		if (this.mTable != null)
		{
			this.mTable.repositionNow = true;
		}
		if (this.mGrid != null)
		{
			this.mGrid.repositionNow = true;
		}
	}

	// Token: 0x060026C0 RID: 9920 RVA: 0x0011ADF7 File Offset: 0x001191F7
	protected virtual void OnDragDropMove(Vector2 delta)
	{
		this.mTrans.localPosition += this.mTrans.InverseTransformDirection(delta);
	}

	// Token: 0x060026C1 RID: 9921 RVA: 0x0011AE20 File Offset: 0x00119220
	protected virtual void OnDragDropRelease(GameObject surface)
	{
		if (!this.cloneOnDrag)
		{
			UIDragScrollView[] componentsInChildren = base.GetComponentsInChildren<UIDragScrollView>();
			foreach (UIDragScrollView uidragScrollView in componentsInChildren)
			{
				uidragScrollView.scrollView = null;
			}
			if (this.mButton != null)
			{
				this.mButton.isEnabled = true;
			}
			else if (this.mCollider != null)
			{
				this.mCollider.enabled = true;
			}
			else if (this.mCollider2D != null)
			{
				this.mCollider2D.enabled = true;
			}
			UIDragDropContainer uidragDropContainer = (!surface) ? null : NGUITools.FindInParents<UIDragDropContainer>(surface);
			if (uidragDropContainer != null)
			{
				this.mTrans.parent = ((!(uidragDropContainer.reparentTarget != null)) ? uidragDropContainer.transform : uidragDropContainer.reparentTarget);
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.z = 0f;
				this.mTrans.localPosition = localPosition;
			}
			else
			{
				this.mTrans.parent = this.mParent;
			}
			this.mParent = this.mTrans.parent;
			this.mGrid = NGUITools.FindInParents<UIGrid>(this.mParent);
			this.mTable = NGUITools.FindInParents<UITable>(this.mParent);
			if (this.mDragScrollView != null)
			{
				base.Invoke("EnableDragScrollView", 0.001f);
			}
			NGUITools.MarkParentAsChanged(base.gameObject);
			if (this.mTable != null)
			{
				this.mTable.repositionNow = true;
			}
			if (this.mGrid != null)
			{
				this.mGrid.repositionNow = true;
			}
		}
		this.OnDragDropEnd();
		if (this.cloneOnDrag)
		{
			this.DestroySelf();
		}
	}

	// Token: 0x060026C2 RID: 9922 RVA: 0x0011B003 File Offset: 0x00119403
	protected virtual void DestroySelf()
	{
		NGUITools.Destroy(base.gameObject);
	}

	// Token: 0x060026C3 RID: 9923 RVA: 0x0011B010 File Offset: 0x00119410
	protected virtual void OnDragDropEnd()
	{
		UIDragDropItem.draggedItems.Remove(this);
	}

	// Token: 0x060026C4 RID: 9924 RVA: 0x0011B01E File Offset: 0x0011941E
	protected void EnableDragScrollView()
	{
		if (this.mDragScrollView != null)
		{
			this.mDragScrollView.enabled = true;
		}
	}

	// Token: 0x060026C5 RID: 9925 RVA: 0x0011B03D File Offset: 0x0011943D
	protected void OnApplicationFocus(bool focus)
	{
		if (!focus)
		{
			this.StopDragging(null);
		}
	}

	// Token: 0x04002763 RID: 10083
	public UIDragDropItem.Restriction restriction;

	// Token: 0x04002764 RID: 10084
	public bool cloneOnDrag;

	// Token: 0x04002765 RID: 10085
	[HideInInspector]
	public float pressAndHoldDelay = 1f;

	// Token: 0x04002766 RID: 10086
	public bool interactable = true;

	// Token: 0x04002767 RID: 10087
	[NonSerialized]
	protected Transform mTrans;

	// Token: 0x04002768 RID: 10088
	[NonSerialized]
	protected Transform mParent;

	// Token: 0x04002769 RID: 10089
	[NonSerialized]
	protected Collider mCollider;

	// Token: 0x0400276A RID: 10090
	[NonSerialized]
	protected Collider2D mCollider2D;

	// Token: 0x0400276B RID: 10091
	[NonSerialized]
	protected UIButton mButton;

	// Token: 0x0400276C RID: 10092
	[NonSerialized]
	protected UIRoot mRoot;

	// Token: 0x0400276D RID: 10093
	[NonSerialized]
	protected UIGrid mGrid;

	// Token: 0x0400276E RID: 10094
	[NonSerialized]
	protected UITable mTable;

	// Token: 0x0400276F RID: 10095
	[NonSerialized]
	protected float mDragStartTime;

	// Token: 0x04002770 RID: 10096
	[NonSerialized]
	protected UIDragScrollView mDragScrollView;

	// Token: 0x04002771 RID: 10097
	[NonSerialized]
	protected bool mPressed;

	// Token: 0x04002772 RID: 10098
	[NonSerialized]
	protected bool mDragging;

	// Token: 0x04002773 RID: 10099
	[NonSerialized]
	protected UICamera.MouseOrTouch mTouch;

	// Token: 0x04002774 RID: 10100
	public static List<UIDragDropItem> draggedItems = new List<UIDragDropItem>();

	// Token: 0x0200056C RID: 1388
	[DoNotObfuscateNGUI]
	public enum Restriction
	{
		// Token: 0x04002776 RID: 10102
		None,
		// Token: 0x04002777 RID: 10103
		Horizontal,
		// Token: 0x04002778 RID: 10104
		Vertical,
		// Token: 0x04002779 RID: 10105
		PressAndHold
	}
}
