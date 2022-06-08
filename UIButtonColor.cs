using System;
using UnityEngine;

// Token: 0x0200055E RID: 1374
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Color")]
public class UIButtonColor : UIWidgetContainer
{
	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x06002666 RID: 9830 RVA: 0x0011C59F File Offset: 0x0011A99F
	// (set) Token: 0x06002667 RID: 9831 RVA: 0x0011C5A7 File Offset: 0x0011A9A7
	public UIButtonColor.State state
	{
		get
		{
			return this.mState;
		}
		set
		{
			this.SetState(value, false);
		}
	}

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x06002668 RID: 9832 RVA: 0x0011C5B1 File Offset: 0x0011A9B1
	// (set) Token: 0x06002669 RID: 9833 RVA: 0x0011C5CC File Offset: 0x0011A9CC
	public Color defaultColor
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mDefaultColor;
		}
		set
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			this.mDefaultColor = value;
			UIButtonColor.State state = this.mState;
			this.mState = UIButtonColor.State.Disabled;
			this.SetState(state, false);
		}
	}

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x0600266A RID: 9834 RVA: 0x0011C607 File Offset: 0x0011AA07
	// (set) Token: 0x0600266B RID: 9835 RVA: 0x0011C60F File Offset: 0x0011AA0F
	public virtual bool isEnabled
	{
		get
		{
			return base.enabled;
		}
		set
		{
			base.enabled = value;
		}
	}

	// Token: 0x0600266C RID: 9836 RVA: 0x0011C618 File Offset: 0x0011AA18
	public void ResetDefaultColor()
	{
		this.defaultColor = this.mStartingColor;
	}

	// Token: 0x0600266D RID: 9837 RVA: 0x0011C626 File Offset: 0x0011AA26
	public void CacheDefaultColor()
	{
		if (!this.mInitDone)
		{
			this.OnInit();
		}
	}

	// Token: 0x0600266E RID: 9838 RVA: 0x0011C639 File Offset: 0x0011AA39
	private void Start()
	{
		if (!this.mInitDone)
		{
			this.OnInit();
		}
		if (!this.isEnabled)
		{
			this.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	// Token: 0x0600266F RID: 9839 RVA: 0x0011C660 File Offset: 0x0011AA60
	protected virtual void OnInit()
	{
		this.mInitDone = true;
		if (this.tweenTarget == null && !Application.isPlaying)
		{
			this.tweenTarget = base.gameObject;
		}
		if (this.tweenTarget != null)
		{
			this.mWidget = this.tweenTarget.GetComponent<UIWidget>();
		}
		if (this.mWidget != null)
		{
			this.mDefaultColor = this.mWidget.color;
			this.mStartingColor = this.mDefaultColor;
		}
		else if (this.tweenTarget != null)
		{
			Renderer component = this.tweenTarget.GetComponent<Renderer>();
			if (component != null)
			{
				this.mDefaultColor = ((!Application.isPlaying) ? component.sharedMaterial.color : component.material.color);
				this.mStartingColor = this.mDefaultColor;
			}
			else
			{
				Light component2 = this.tweenTarget.GetComponent<Light>();
				if (component2 != null)
				{
					this.mDefaultColor = component2.color;
					this.mStartingColor = this.mDefaultColor;
				}
				else
				{
					this.tweenTarget = null;
					this.mInitDone = false;
				}
			}
		}
	}

	// Token: 0x06002670 RID: 9840 RVA: 0x0011C798 File Offset: 0x0011AB98
	protected virtual void OnEnable()
	{
		if (this.mInitDone)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
		if (UICamera.currentTouch != null)
		{
			if (UICamera.currentTouch.pressed == base.gameObject)
			{
				this.OnPress(true);
			}
			else if (UICamera.currentTouch.current == base.gameObject)
			{
				this.OnHover(true);
			}
		}
	}

	// Token: 0x06002671 RID: 9841 RVA: 0x0011C814 File Offset: 0x0011AC14
	protected virtual void OnDisable()
	{
		if (this.mInitDone && this.mState != UIButtonColor.State.Normal)
		{
			this.SetState(UIButtonColor.State.Normal, true);
			if (this.tweenTarget != null)
			{
				TweenColor component = this.tweenTarget.GetComponent<TweenColor>();
				if (component != null)
				{
					component.value = this.mDefaultColor;
					component.enabled = false;
				}
			}
		}
	}

	// Token: 0x06002672 RID: 9842 RVA: 0x0011C87C File Offset: 0x0011AC7C
	protected virtual void OnHover(bool isOver)
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState((!isOver) ? UIButtonColor.State.Normal : UIButtonColor.State.Hover, false);
			}
		}
	}

	// Token: 0x06002673 RID: 9843 RVA: 0x0011C8CC File Offset: 0x0011ACCC
	protected virtual void OnPress(bool isPressed)
	{
		if (this.isEnabled && UICamera.currentTouch != null)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				if (isPressed)
				{
					this.SetState(UIButtonColor.State.Pressed, false);
				}
				else if (UICamera.currentTouch.current == base.gameObject)
				{
					if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
					{
						this.SetState(UIButtonColor.State.Hover, false);
					}
					else if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.hoveredObject == base.gameObject)
					{
						this.SetState(UIButtonColor.State.Hover, false);
					}
					else
					{
						this.SetState(UIButtonColor.State.Normal, false);
					}
				}
				else
				{
					this.SetState(UIButtonColor.State.Normal, false);
				}
			}
		}
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x0011C996 File Offset: 0x0011AD96
	protected virtual void OnDragOver()
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(UIButtonColor.State.Pressed, false);
			}
		}
	}

	// Token: 0x06002675 RID: 9845 RVA: 0x0011C9CD File Offset: 0x0011ADCD
	protected virtual void OnDragOut()
	{
		if (this.isEnabled)
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.tweenTarget != null)
			{
				this.SetState(UIButtonColor.State.Normal, false);
			}
		}
	}

	// Token: 0x06002676 RID: 9846 RVA: 0x0011CA04 File Offset: 0x0011AE04
	public virtual void SetState(UIButtonColor.State state, bool instant)
	{
		if (!this.mInitDone)
		{
			this.mInitDone = true;
			this.OnInit();
		}
		if (this.mState != state)
		{
			this.mState = state;
			this.UpdateColor(instant);
		}
	}

	// Token: 0x06002677 RID: 9847 RVA: 0x0011CA38 File Offset: 0x0011AE38
	public void UpdateColor(bool instant)
	{
		if (this.tweenTarget != null)
		{
			TweenColor tweenColor;
			switch (this.mState)
			{
			case UIButtonColor.State.Hover:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.hover);
				break;
			case UIButtonColor.State.Pressed:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.pressed);
				break;
			case UIButtonColor.State.Disabled:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.disabledColor);
				break;
			default:
				tweenColor = TweenColor.Begin(this.tweenTarget, this.duration, this.mDefaultColor);
				break;
			}
			if (instant && tweenColor != null)
			{
				tweenColor.value = tweenColor.to;
				tweenColor.enabled = false;
			}
		}
	}

	// Token: 0x04002728 RID: 10024
	public GameObject tweenTarget;

	// Token: 0x04002729 RID: 10025
	public Color hover = new Color(0.88235295f, 0.78431374f, 0.5882353f, 1f);

	// Token: 0x0400272A RID: 10026
	public Color pressed = new Color(0.7176471f, 0.6392157f, 0.48235294f, 1f);

	// Token: 0x0400272B RID: 10027
	public Color disabledColor = Color.grey;

	// Token: 0x0400272C RID: 10028
	public float duration = 0.2f;

	// Token: 0x0400272D RID: 10029
	[NonSerialized]
	protected Color mStartingColor;

	// Token: 0x0400272E RID: 10030
	[NonSerialized]
	protected Color mDefaultColor;

	// Token: 0x0400272F RID: 10031
	[NonSerialized]
	protected bool mInitDone;

	// Token: 0x04002730 RID: 10032
	[NonSerialized]
	protected UIWidget mWidget;

	// Token: 0x04002731 RID: 10033
	[NonSerialized]
	protected UIButtonColor.State mState;

	// Token: 0x0200055F RID: 1375
	[DoNotObfuscateNGUI]
	public enum State
	{
		// Token: 0x04002733 RID: 10035
		Normal,
		// Token: 0x04002734 RID: 10036
		Hover,
		// Token: 0x04002735 RID: 10037
		Pressed,
		// Token: 0x04002736 RID: 10038
		Disabled
	}
}
