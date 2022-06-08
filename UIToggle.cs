using System;
using System.Collections.Generic;
using AnimationOrTween;
using UnityEngine;

// Token: 0x0200059B RID: 1435
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Toggle")]
public class UIToggle : UIWidgetContainer
{
	// Token: 0x17000223 RID: 547
	// (get) Token: 0x06002824 RID: 10276 RVA: 0x001280B3 File Offset: 0x001264B3
	// (set) Token: 0x06002825 RID: 10277 RVA: 0x001280D4 File Offset: 0x001264D4
	public bool value
	{
		get
		{
			return (!this.mStarted) ? this.startsActive : this.mIsActive;
		}
		set
		{
			if (!this.mStarted)
			{
				this.startsActive = value;
			}
			else if (this.group == 0 || value || this.optionCanBeNone || !this.mStarted)
			{
				this.Set(value, true);
			}
		}
	}

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06002826 RID: 10278 RVA: 0x00128128 File Offset: 0x00126528
	public bool isColliderEnabled
	{
		get
		{
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x06002827 RID: 10279 RVA: 0x0012816B File Offset: 0x0012656B
	// (set) Token: 0x06002828 RID: 10280 RVA: 0x00128173 File Offset: 0x00126573
	[Obsolete("Use 'value' instead")]
	public bool isChecked
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

	// Token: 0x06002829 RID: 10281 RVA: 0x0012817C File Offset: 0x0012657C
	public static UIToggle GetActiveToggle(int group)
	{
		for (int i = 0; i < UIToggle.list.size; i++)
		{
			UIToggle uitoggle = UIToggle.list[i];
			if (uitoggle != null && uitoggle.group == group && uitoggle.mIsActive)
			{
				return uitoggle;
			}
		}
		return null;
	}

	// Token: 0x0600282A RID: 10282 RVA: 0x001281D6 File Offset: 0x001265D6
	private void OnEnable()
	{
		UIToggle.list.Add(this);
	}

	// Token: 0x0600282B RID: 10283 RVA: 0x001281E3 File Offset: 0x001265E3
	private void OnDisable()
	{
		UIToggle.list.Remove(this);
	}

	// Token: 0x0600282C RID: 10284 RVA: 0x001281F4 File Offset: 0x001265F4
	public void Start()
	{
		if (this.mStarted)
		{
			return;
		}
		if (this.startsChecked)
		{
			this.startsChecked = false;
			this.startsActive = true;
		}
		if (!Application.isPlaying)
		{
			if (this.checkSprite != null && this.activeSprite == null)
			{
				this.activeSprite = this.checkSprite;
				this.checkSprite = null;
			}
			if (this.checkAnimation != null && this.activeAnimation == null)
			{
				this.activeAnimation = this.checkAnimation;
				this.checkAnimation = null;
			}
			if (Application.isPlaying && this.activeSprite != null)
			{
				this.activeSprite.alpha = ((!this.invertSpriteState) ? ((!this.startsActive) ? 0f : 1f) : ((!this.startsActive) ? 1f : 0f));
			}
			if (EventDelegate.IsValid(this.onChange))
			{
				this.eventReceiver = null;
				this.functionName = null;
			}
		}
		else
		{
			this.mIsActive = !this.startsActive;
			this.mStarted = true;
			bool flag = this.instantTween;
			this.instantTween = true;
			this.Set(this.startsActive, true);
			this.instantTween = flag;
		}
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x0012835F File Offset: 0x0012675F
	private void OnClick()
	{
		if (base.enabled && this.isColliderEnabled && UICamera.currentTouchID != -2)
		{
			this.value = !this.value;
		}
	}

	// Token: 0x0600282E RID: 10286 RVA: 0x00128394 File Offset: 0x00126794
	public void Set(bool state, bool notify = true)
	{
		if (this.validator != null && !this.validator(state))
		{
			return;
		}
		if (!this.mStarted)
		{
			this.mIsActive = state;
			this.startsActive = state;
			if (this.activeSprite != null)
			{
				this.activeSprite.alpha = ((!this.invertSpriteState) ? ((!state) ? 0f : 1f) : ((!state) ? 1f : 0f));
			}
		}
		else if (this.mIsActive != state)
		{
			if (this.group != 0 && state)
			{
				int i = 0;
				int size = UIToggle.list.size;
				while (i < size)
				{
					UIToggle uitoggle = UIToggle.list[i];
					if (uitoggle != this && uitoggle.group == this.group)
					{
						uitoggle.Set(false, true);
					}
					if (UIToggle.list.size != size)
					{
						size = UIToggle.list.size;
						i = 0;
					}
					else
					{
						i++;
					}
				}
			}
			this.mIsActive = state;
			if (this.activeSprite != null)
			{
				if (this.instantTween || !NGUITools.GetActive(this))
				{
					this.activeSprite.alpha = ((!this.invertSpriteState) ? ((!this.mIsActive) ? 0f : 1f) : ((!this.mIsActive) ? 1f : 0f));
				}
				else
				{
					TweenAlpha.Begin(this.activeSprite.gameObject, 0.15f, (!this.invertSpriteState) ? ((!this.mIsActive) ? 0f : 1f) : ((!this.mIsActive) ? 1f : 0f), 0f);
				}
			}
			if (notify && UIToggle.current == null)
			{
				UIToggle uitoggle2 = UIToggle.current;
				UIToggle.current = this;
				if (EventDelegate.IsValid(this.onChange))
				{
					EventDelegate.Execute(this.onChange);
				}
				else if (this.eventReceiver != null && !string.IsNullOrEmpty(this.functionName))
				{
					this.eventReceiver.SendMessage(this.functionName, this.mIsActive, SendMessageOptions.DontRequireReceiver);
				}
				UIToggle.current = uitoggle2;
			}
			if (this.animator != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(this.animator, null, (!state) ? Direction.Reverse : Direction.Forward, EnableCondition.IgnoreDisabledState, DisableCondition.DoNotDisable);
				if (activeAnimation != null && (this.instantTween || !NGUITools.GetActive(this)))
				{
					activeAnimation.Finish();
				}
			}
			else if (this.activeAnimation != null)
			{
				ActiveAnimation activeAnimation2 = ActiveAnimation.Play(this.activeAnimation, null, (!state) ? Direction.Reverse : Direction.Forward, EnableCondition.IgnoreDisabledState, DisableCondition.DoNotDisable);
				if (activeAnimation2 != null && (this.instantTween || !NGUITools.GetActive(this)))
				{
					activeAnimation2.Finish();
				}
			}
			else if (this.tween != null)
			{
				bool active = NGUITools.GetActive(this);
				if (this.tween.tweenGroup != 0)
				{
					UITweener[] componentsInChildren = this.tween.GetComponentsInChildren<UITweener>(true);
					int j = 0;
					int num = componentsInChildren.Length;
					while (j < num)
					{
						UITweener uitweener = componentsInChildren[j];
						if (uitweener.tweenGroup == this.tween.tweenGroup)
						{
							uitweener.Play(state);
							if (this.instantTween || !active)
							{
								uitweener.tweenFactor = ((!state) ? 0f : 1f);
							}
						}
						j++;
					}
				}
				else
				{
					this.tween.Play(state);
					if (this.instantTween || !active)
					{
						this.tween.tweenFactor = ((!state) ? 0f : 1f);
					}
				}
			}
		}
	}

	// Token: 0x040028F4 RID: 10484
	public static BetterList<UIToggle> list = new BetterList<UIToggle>();

	// Token: 0x040028F5 RID: 10485
	public static UIToggle current;

	// Token: 0x040028F6 RID: 10486
	public int group;

	// Token: 0x040028F7 RID: 10487
	public UIWidget activeSprite;

	// Token: 0x040028F8 RID: 10488
	public bool invertSpriteState;

	// Token: 0x040028F9 RID: 10489
	public Animation activeAnimation;

	// Token: 0x040028FA RID: 10490
	public Animator animator;

	// Token: 0x040028FB RID: 10491
	public UITweener tween;

	// Token: 0x040028FC RID: 10492
	public bool startsActive;

	// Token: 0x040028FD RID: 10493
	public bool instantTween;

	// Token: 0x040028FE RID: 10494
	public bool optionCanBeNone;

	// Token: 0x040028FF RID: 10495
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x04002900 RID: 10496
	public UIToggle.Validate validator;

	// Token: 0x04002901 RID: 10497
	[HideInInspector]
	[SerializeField]
	private UISprite checkSprite;

	// Token: 0x04002902 RID: 10498
	[HideInInspector]
	[SerializeField]
	private Animation checkAnimation;

	// Token: 0x04002903 RID: 10499
	[HideInInspector]
	[SerializeField]
	private GameObject eventReceiver;

	// Token: 0x04002904 RID: 10500
	[HideInInspector]
	[SerializeField]
	private string functionName = "OnActivate";

	// Token: 0x04002905 RID: 10501
	[HideInInspector]
	[SerializeField]
	private bool startsChecked;

	// Token: 0x04002906 RID: 10502
	private bool mIsActive = true;

	// Token: 0x04002907 RID: 10503
	private bool mStarted;

	// Token: 0x0200059C RID: 1436
	// (Invoke) Token: 0x06002831 RID: 10289
	public delegate bool Validate(bool choice);
}
