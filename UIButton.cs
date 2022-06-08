using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200055C RID: 1372
[AddComponentMenu("NGUI/Interaction/Button")]
public class UIButton : UIButtonColor
{
	// Token: 0x170001EE RID: 494
	// (get) Token: 0x06002655 RID: 9813 RVA: 0x0011CB24 File Offset: 0x0011AF24
	// (set) Token: 0x06002656 RID: 9814 RVA: 0x0011CB80 File Offset: 0x0011AF80
	public override bool isEnabled
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			Collider component = base.gameObject.GetComponent<Collider>();
			if (component && component.enabled)
			{
				return true;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 && component2.enabled;
		}
		set
		{
			if (this.isEnabled != value)
			{
				Collider component = base.gameObject.GetComponent<Collider>();
				if (component != null)
				{
					component.enabled = value;
					UIButton[] components = base.GetComponents<UIButton>();
					foreach (UIButton uibutton in components)
					{
						uibutton.SetState((!value) ? UIButtonColor.State.Disabled : UIButtonColor.State.Normal, false);
					}
				}
				else
				{
					Collider2D component2 = base.GetComponent<Collider2D>();
					if (component2 != null)
					{
						component2.enabled = value;
						UIButton[] components2 = base.GetComponents<UIButton>();
						foreach (UIButton uibutton2 in components2)
						{
							uibutton2.SetState((!value) ? UIButtonColor.State.Disabled : UIButtonColor.State.Normal, false);
						}
					}
					else
					{
						base.enabled = value;
					}
				}
			}
		}
	}

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06002657 RID: 9815 RVA: 0x0011CC61 File Offset: 0x0011B061
	// (set) Token: 0x06002658 RID: 9816 RVA: 0x0011CC7C File Offset: 0x0011B07C
	public string normalSprite
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mNormalSprite;
		}
		set
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.mSprite != null && !string.IsNullOrEmpty(this.mNormalSprite) && this.mNormalSprite == this.mSprite.spriteName)
			{
				this.mNormalSprite = value;
				this.SetSprite(value);
				NGUITools.SetDirty(this.mSprite, "last change");
			}
			else
			{
				this.mNormalSprite = value;
				if (this.mState == UIButtonColor.State.Normal)
				{
					this.SetSprite(value);
				}
			}
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x06002659 RID: 9817 RVA: 0x0011CD12 File Offset: 0x0011B112
	// (set) Token: 0x0600265A RID: 9818 RVA: 0x0011CD2C File Offset: 0x0011B12C
	public Sprite normalSprite2D
	{
		get
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			return this.mNormalSprite2D;
		}
		set
		{
			if (!this.mInitDone)
			{
				this.OnInit();
			}
			if (this.mSprite2D != null && this.mNormalSprite2D == this.mSprite2D.sprite2D)
			{
				this.mNormalSprite2D = value;
				this.SetSprite(value);
				NGUITools.SetDirty(this.mSprite, "last change");
			}
			else
			{
				this.mNormalSprite2D = value;
				if (this.mState == UIButtonColor.State.Normal)
				{
					this.SetSprite(value);
				}
			}
		}
	}

	// Token: 0x0600265B RID: 9819 RVA: 0x0011CDB4 File Offset: 0x0011B1B4
	protected override void OnInit()
	{
		base.OnInit();
		this.mSprite = (this.mWidget as UISprite);
		this.mSprite2D = (this.mWidget as UI2DSprite);
		if (this.mSprite != null)
		{
			this.mNormalSprite = this.mSprite.spriteName;
		}
		if (this.mSprite2D != null)
		{
			this.mNormalSprite2D = this.mSprite2D.sprite2D;
		}
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x0011CE2D File Offset: 0x0011B22D
	protected override void OnEnable()
	{
		if (this.isEnabled)
		{
			if (this.mInitDone)
			{
				this.OnHover(UICamera.hoveredObject == base.gameObject);
			}
		}
		else
		{
			this.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	// Token: 0x0600265D RID: 9821 RVA: 0x0011CE68 File Offset: 0x0011B268
	protected override void OnDragOver()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOver();
		}
	}

	// Token: 0x0600265E RID: 9822 RVA: 0x0011CEA0 File Offset: 0x0011B2A0
	protected override void OnDragOut()
	{
		if (this.isEnabled && (this.dragHighlight || UICamera.currentTouch.pressed == base.gameObject))
		{
			base.OnDragOut();
		}
	}

	// Token: 0x0600265F RID: 9823 RVA: 0x0011CED8 File Offset: 0x0011B2D8
	protected virtual void OnClick()
	{
		if (UIButton.current == null && this.isEnabled && UICamera.currentTouchID != -2 && UICamera.currentTouchID != -3)
		{
			UIButton.current = this;
			EventDelegate.Execute(this.onClick);
			UIButton.current = null;
		}
	}

	// Token: 0x06002660 RID: 9824 RVA: 0x0011CF30 File Offset: 0x0011B330
	public override void SetState(UIButtonColor.State state, bool immediate)
	{
		base.SetState(state, immediate);
		if (this.mSprite != null)
		{
			switch (state)
			{
			case UIButtonColor.State.Normal:
				this.SetSprite(this.mNormalSprite);
				break;
			case UIButtonColor.State.Hover:
				this.SetSprite((!string.IsNullOrEmpty(this.hoverSprite)) ? this.hoverSprite : this.mNormalSprite);
				break;
			case UIButtonColor.State.Pressed:
				this.SetSprite(this.pressedSprite);
				break;
			case UIButtonColor.State.Disabled:
				this.SetSprite(this.disabledSprite);
				break;
			}
		}
		else if (this.mSprite2D != null)
		{
			switch (state)
			{
			case UIButtonColor.State.Normal:
				this.SetSprite(this.mNormalSprite2D);
				break;
			case UIButtonColor.State.Hover:
				this.SetSprite((!(this.hoverSprite2D == null)) ? this.hoverSprite2D : this.mNormalSprite2D);
				break;
			case UIButtonColor.State.Pressed:
				this.SetSprite(this.pressedSprite2D);
				break;
			case UIButtonColor.State.Disabled:
				this.SetSprite(this.disabledSprite2D);
				break;
			}
		}
	}

	// Token: 0x06002661 RID: 9825 RVA: 0x0011D064 File Offset: 0x0011B464
	protected void SetSprite(string sp)
	{
		if (this.mSprite != null && !string.IsNullOrEmpty(sp) && this.mSprite.spriteName != sp)
		{
			this.mSprite.spriteName = sp;
			if (this.pixelSnap)
			{
				this.mSprite.MakePixelPerfect();
			}
		}
	}

	// Token: 0x06002662 RID: 9826 RVA: 0x0011D0C8 File Offset: 0x0011B4C8
	protected void SetSprite(Sprite sp)
	{
		if (sp != null && this.mSprite2D != null && this.mSprite2D.sprite2D != sp)
		{
			this.mSprite2D.sprite2D = sp;
			if (this.pixelSnap)
			{
				this.mSprite2D.MakePixelPerfect();
			}
		}
	}

	// Token: 0x04002718 RID: 10008
	public static UIButton current;

	// Token: 0x04002719 RID: 10009
	public bool dragHighlight;

	// Token: 0x0400271A RID: 10010
	public string hoverSprite;

	// Token: 0x0400271B RID: 10011
	public string pressedSprite;

	// Token: 0x0400271C RID: 10012
	public string disabledSprite;

	// Token: 0x0400271D RID: 10013
	public Sprite hoverSprite2D;

	// Token: 0x0400271E RID: 10014
	public Sprite pressedSprite2D;

	// Token: 0x0400271F RID: 10015
	public Sprite disabledSprite2D;

	// Token: 0x04002720 RID: 10016
	public bool pixelSnap;

	// Token: 0x04002721 RID: 10017
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x04002722 RID: 10018
	[NonSerialized]
	private UISprite mSprite;

	// Token: 0x04002723 RID: 10019
	[NonSerialized]
	private UI2DSprite mSprite2D;

	// Token: 0x04002724 RID: 10020
	[NonSerialized]
	private string mNormalSprite;

	// Token: 0x04002725 RID: 10021
	[NonSerialized]
	private Sprite mNormalSprite2D;
}
