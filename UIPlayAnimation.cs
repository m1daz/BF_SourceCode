using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200057F RID: 1407
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Play Animation")]
public class UIPlayAnimation : MonoBehaviour
{
	// Token: 0x170001FD RID: 509
	// (get) Token: 0x06002748 RID: 10056 RVA: 0x001213EE File Offset: 0x0011F7EE
	private bool dualState
	{
		get
		{
			return this.trigger == Trigger.OnPress || this.trigger == Trigger.OnHover;
		}
	}

	// Token: 0x06002749 RID: 10057 RVA: 0x00121408 File Offset: 0x0011F808
	private void Awake()
	{
		UIButton component = base.GetComponent<UIButton>();
		if (component != null)
		{
			this.dragHighlight = component.dragHighlight;
		}
		if (this.eventReceiver != null && EventDelegate.IsValid(this.onFinished))
		{
			this.eventReceiver = null;
			this.callWhenFinished = null;
		}
	}

	// Token: 0x0600274A RID: 10058 RVA: 0x00121464 File Offset: 0x0011F864
	private void Start()
	{
		this.mStarted = true;
		if (this.target == null && this.animator == null)
		{
			this.animator = base.GetComponentInChildren<Animator>();
		}
		if (this.animator != null)
		{
			if (this.animator.enabled)
			{
				this.animator.enabled = false;
			}
			return;
		}
		if (this.target == null)
		{
			this.target = base.GetComponentInChildren<Animation>();
		}
		if (this.target != null && this.target.enabled)
		{
			this.target.enabled = false;
		}
	}

	// Token: 0x0600274B RID: 10059 RVA: 0x00121520 File Offset: 0x0011F920
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if (this.trigger == Trigger.OnPress || this.trigger == Trigger.OnPressTrue)
			{
				this.mActivated = (UICamera.currentTouch.pressed == base.gameObject);
			}
			if (this.trigger == Trigger.OnHover || this.trigger == Trigger.OnHoverTrue)
			{
				this.mActivated = (UICamera.currentTouch.current == base.gameObject);
			}
		}
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Add(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x0600274C RID: 10060 RVA: 0x001215E4 File Offset: 0x0011F9E4
	private void OnDisable()
	{
		UIToggle component = base.GetComponent<UIToggle>();
		if (component != null)
		{
			EventDelegate.Remove(component.onChange, new EventDelegate.Callback(this.OnToggle));
		}
	}

	// Token: 0x0600274D RID: 10061 RVA: 0x0012161C File Offset: 0x0011FA1C
	private void OnHover(bool isOver)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnHover || (this.trigger == Trigger.OnHoverTrue && isOver) || (this.trigger == Trigger.OnHoverFalse && !isOver))
		{
			this.Play(isOver, this.dualState);
		}
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x00121674 File Offset: 0x0011FA74
	private void OnPress(bool isPressed)
	{
		if (!base.enabled)
		{
			return;
		}
		if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (this.trigger == Trigger.OnPress || (this.trigger == Trigger.OnPressTrue && isPressed) || (this.trigger == Trigger.OnPressFalse && !isPressed))
		{
			this.Play(isPressed, this.dualState);
		}
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x001216E3 File Offset: 0x0011FAE3
	private void OnClick()
	{
		if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (base.enabled && this.trigger == Trigger.OnClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x0012171C File Offset: 0x0011FB1C
	private void OnDoubleClick()
	{
		if (UICamera.currentTouchID == -2 || UICamera.currentTouchID == -3)
		{
			return;
		}
		if (base.enabled && this.trigger == Trigger.OnDoubleClick)
		{
			this.Play(true, false);
		}
	}

	// Token: 0x06002751 RID: 10065 RVA: 0x00121758 File Offset: 0x0011FB58
	private void OnSelect(bool isSelected)
	{
		if (!base.enabled)
		{
			return;
		}
		if (this.trigger == Trigger.OnSelect || (this.trigger == Trigger.OnSelectTrue && isSelected) || (this.trigger == Trigger.OnSelectFalse && !isSelected))
		{
			this.Play(isSelected, this.dualState);
		}
	}

	// Token: 0x06002752 RID: 10066 RVA: 0x001217B4 File Offset: 0x0011FBB4
	private void OnToggle()
	{
		if (!base.enabled || UIToggle.current == null)
		{
			return;
		}
		if (this.trigger == Trigger.OnActivate || (this.trigger == Trigger.OnActivateTrue && UIToggle.current.value) || (this.trigger == Trigger.OnActivateFalse && !UIToggle.current.value))
		{
			this.Play(UIToggle.current.value, this.dualState);
		}
	}

	// Token: 0x06002753 RID: 10067 RVA: 0x00121838 File Offset: 0x0011FC38
	private void OnDragOver()
	{
		if (base.enabled && this.dualState)
		{
			if (UICamera.currentTouch.dragged == base.gameObject)
			{
				this.Play(true, true);
			}
			else if (this.dragHighlight && this.trigger == Trigger.OnPress)
			{
				this.Play(true, true);
			}
		}
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x001218A1 File Offset: 0x0011FCA1
	private void OnDragOut()
	{
		if (base.enabled && this.dualState && UICamera.hoveredObject != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x06002755 RID: 10069 RVA: 0x001218D6 File Offset: 0x0011FCD6
	private void OnDrop(GameObject go)
	{
		if (base.enabled && this.trigger == Trigger.OnPress && UICamera.currentTouch.dragged != base.gameObject)
		{
			this.Play(false, true);
		}
	}

	// Token: 0x06002756 RID: 10070 RVA: 0x00121911 File Offset: 0x0011FD11
	public void Play(bool forward)
	{
		this.Play(forward, true);
	}

	// Token: 0x06002757 RID: 10071 RVA: 0x0012191C File Offset: 0x0011FD1C
	public void Play(bool forward, bool onlyIfDifferent)
	{
		if (this.target || this.animator)
		{
			if (onlyIfDifferent)
			{
				if (this.mActivated == forward)
				{
					return;
				}
				this.mActivated = forward;
			}
			if (this.clearSelection && UICamera.selectedObject == base.gameObject)
			{
				UICamera.selectedObject = null;
			}
			int num = (int)(-(int)this.playDirection);
			Direction direction = (Direction)((!forward) ? num : ((int)this.playDirection));
			ActiveAnimation activeAnimation = (!this.target) ? ActiveAnimation.Play(this.animator, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished) : ActiveAnimation.Play(this.target, this.clipName, direction, this.ifDisabledOnPlay, this.disableWhenFinished);
			if (activeAnimation != null)
			{
				if (this.resetOnPlay)
				{
					activeAnimation.Reset();
				}
				for (int i = 0; i < this.onFinished.Count; i++)
				{
					EventDelegate.Add(activeAnimation.onFinished, new EventDelegate.Callback(this.OnFinished), true);
				}
			}
		}
	}

	// Token: 0x06002758 RID: 10072 RVA: 0x00121A48 File Offset: 0x0011FE48
	public void PlayForward()
	{
		this.Play(true);
	}

	// Token: 0x06002759 RID: 10073 RVA: 0x00121A51 File Offset: 0x0011FE51
	public void PlayReverse()
	{
		this.Play(false);
	}

	// Token: 0x0600275A RID: 10074 RVA: 0x00121A5C File Offset: 0x0011FE5C
	private void OnFinished()
	{
		if (UIPlayAnimation.current == null)
		{
			UIPlayAnimation.current = this;
			EventDelegate.Execute(this.onFinished);
			if (this.eventReceiver != null && !string.IsNullOrEmpty(this.callWhenFinished))
			{
				this.eventReceiver.SendMessage(this.callWhenFinished, SendMessageOptions.DontRequireReceiver);
			}
			this.eventReceiver = null;
			UIPlayAnimation.current = null;
		}
	}

	// Token: 0x0400280E RID: 10254
	public static UIPlayAnimation current;

	// Token: 0x0400280F RID: 10255
	public Animation target;

	// Token: 0x04002810 RID: 10256
	public Animator animator;

	// Token: 0x04002811 RID: 10257
	public string clipName;

	// Token: 0x04002812 RID: 10258
	public Trigger trigger;

	// Token: 0x04002813 RID: 10259
	public Direction playDirection = Direction.Forward;

	// Token: 0x04002814 RID: 10260
	public bool resetOnPlay;

	// Token: 0x04002815 RID: 10261
	public bool clearSelection;

	// Token: 0x04002816 RID: 10262
	public EnableCondition ifDisabledOnPlay;

	// Token: 0x04002817 RID: 10263
	public DisableCondition disableWhenFinished;

	// Token: 0x04002818 RID: 10264
	public List<EventDelegate> onFinished = new List<EventDelegate>();

	// Token: 0x04002819 RID: 10265
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x0400281A RID: 10266
	[HideInInspector]
	[SerializeField]
	private string callWhenFinished;

	// Token: 0x0400281B RID: 10267
	private bool mStarted;

	// Token: 0x0400281C RID: 10268
	private bool mActivated;

	// Token: 0x0400281D RID: 10269
	private bool dragHighlight;
}
