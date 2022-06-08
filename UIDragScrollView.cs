using System;
using UnityEngine;

// Token: 0x02000572 RID: 1394
[AddComponentMenu("NGUI/Interaction/Drag Scroll View")]
public class UIDragScrollView : MonoBehaviour
{
	// Token: 0x060026E7 RID: 9959 RVA: 0x0011FCE0 File Offset: 0x0011E0E0
	private void OnEnable()
	{
		this.mTrans = base.transform;
		if (this.scrollView == null && this.draggablePanel != null)
		{
			this.scrollView = this.draggablePanel;
			this.draggablePanel = null;
		}
		if (this.mStarted && (this.mAutoFind || this.mScroll == null))
		{
			this.FindScrollView();
		}
	}

	// Token: 0x060026E8 RID: 9960 RVA: 0x0011FD5B File Offset: 0x0011E15B
	private void Start()
	{
		this.mStarted = true;
		this.FindScrollView();
	}

	// Token: 0x060026E9 RID: 9961 RVA: 0x0011FD6C File Offset: 0x0011E16C
	private void FindScrollView()
	{
		UIScrollView uiscrollView = NGUITools.FindInParents<UIScrollView>(this.mTrans);
		if (this.scrollView == null || (this.mAutoFind && uiscrollView != this.scrollView))
		{
			this.scrollView = uiscrollView;
			this.mAutoFind = true;
		}
		else if (this.scrollView == uiscrollView)
		{
			this.mAutoFind = true;
		}
		this.mScroll = this.scrollView;
	}

	// Token: 0x060026EA RID: 9962 RVA: 0x0011FDEC File Offset: 0x0011E1EC
	private void OnDisable()
	{
		if (this.mPressed && this.mScroll != null && this.mScroll.GetComponentInChildren<UIWrapContent>() == null)
		{
			this.mScroll.Press(false);
			this.mScroll = null;
		}
	}

	// Token: 0x060026EB RID: 9963 RVA: 0x0011FE40 File Offset: 0x0011E240
	private void OnPress(bool pressed)
	{
		this.mPressed = pressed;
		if (this.mAutoFind && this.mScroll != this.scrollView)
		{
			this.mScroll = this.scrollView;
			this.mAutoFind = false;
		}
		if (this.scrollView && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.scrollView.Press(pressed);
			if (!pressed && this.mAutoFind)
			{
				this.scrollView = NGUITools.FindInParents<UIScrollView>(this.mTrans);
				this.mScroll = this.scrollView;
			}
		}
	}

	// Token: 0x060026EC RID: 9964 RVA: 0x0011FEED File Offset: 0x0011E2ED
	private void OnDrag(Vector2 delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Drag();
		}
	}

	// Token: 0x060026ED RID: 9965 RVA: 0x0011FF15 File Offset: 0x0011E315
	private void OnScroll(float delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.Scroll(delta);
		}
	}

	// Token: 0x060026EE RID: 9966 RVA: 0x0011FF3E File Offset: 0x0011E33E
	public void OnPan(Vector2 delta)
	{
		if (this.scrollView && NGUITools.GetActive(this))
		{
			this.scrollView.OnPan(delta);
		}
	}

	// Token: 0x040027AC RID: 10156
	public UIScrollView scrollView;

	// Token: 0x040027AD RID: 10157
	[HideInInspector]
	[SerializeField]
	private UIScrollView draggablePanel;

	// Token: 0x040027AE RID: 10158
	private Transform mTrans;

	// Token: 0x040027AF RID: 10159
	private UIScrollView mScroll;

	// Token: 0x040027B0 RID: 10160
	private bool mAutoFind;

	// Token: 0x040027B1 RID: 10161
	private bool mStarted;

	// Token: 0x040027B2 RID: 10162
	[NonSerialized]
	private bool mPressed;
}
