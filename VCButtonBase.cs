using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B0 RID: 1712
public abstract class VCButtonBase : VCCollideableObject
{
	// Token: 0x06003274 RID: 12916 RVA: 0x0016471C File Offset: 0x00162B1C
	protected void AddInstance()
	{
		if (string.IsNullOrEmpty(this.vcName))
		{
			return;
		}
		if (VCButtonBase._instancesByVcName == null)
		{
			VCButtonBase._instancesByVcName = new Dictionary<string, VCButtonBase>();
		}
		while (VCButtonBase._instancesByVcName.ContainsKey(this.vcName))
		{
			if (VCButtonBase._instancesByVcName[this.vcName] == null)
			{
				VCButtonBase._instancesByVcName.Remove(this.vcName);
			}
			else
			{
				this.vcName += "_copy";
				Debug.LogWarning("Attempting to add instance with duplicate VCName!\nVCNames must be unique -- renaming this instance to " + this.vcName);
			}
		}
		VCButtonBase._instancesByVcName.Add(this.vcName, this);
	}

	// Token: 0x06003275 RID: 12917 RVA: 0x001647D5 File Offset: 0x00162BD5
	public static VCButtonBase GetInstance(string vcName)
	{
		if (VCButtonBase._instancesByVcName == null || !VCButtonBase._instancesByVcName.ContainsKey(vcName))
		{
			return null;
		}
		return VCButtonBase._instancesByVcName[vcName];
	}

	// Token: 0x06003276 RID: 12918
	protected abstract void ShowPressedState(bool pressed);

	// Token: 0x06003277 RID: 12919
	protected abstract bool Colliding(VCTouchWrapper tw);

	// Token: 0x06003278 RID: 12920 RVA: 0x001647FE File Offset: 0x00162BFE
	public void ForceRelease()
	{
		this.Pressed = false;
	}

	// Token: 0x06003279 RID: 12921 RVA: 0x00164807 File Offset: 0x00162C07
	protected void Start()
	{
		this.Init();
	}

	// Token: 0x0600327A RID: 12922 RVA: 0x00164810 File Offset: 0x00162C10
	protected virtual bool Init()
	{
		base.useGUILayout = false;
		if (VCTouchController.Instance == null)
		{
			Debug.LogWarning("Cannot find VCTouchController!\nVirtualControls requires a gameObject which has VCTouchController component attached in scene. Adding one for you...");
			base.gameObject.AddComponent<VCTouchController>();
		}
		this._lastPressedState = true;
		this.Pressed = false;
		this.HoldTime = 0f;
		this.AddInstance();
		return true;
	}

	// Token: 0x0600327B RID: 12923 RVA: 0x0016486C File Offset: 0x00162C6C
	protected void Update()
	{
		this.PressBeganThisFrame = false;
		this.PressEndedThisFrame = false;
		if (!this.skipCollisionDetection)
		{
			if (this.Pressed)
			{
				if (this.PressEnded)
				{
					this.Pressed = false;
				}
				else
				{
					this.HoldTime += Time.deltaTime;
					if (this.OnHold != null)
					{
						this.OnHold(this);
					}
				}
			}
			else
			{
				for (int i = 0; i < VCTouchController.Instance.touches.Count; i++)
				{
					VCTouchWrapper vctouchWrapper = VCTouchController.Instance.touches[i];
					if (vctouchWrapper.Active && (!this.touchMustBeginOnCollider || vctouchWrapper.phase == TouchPhase.Began))
					{
						if (this.anyTouchActivatesControl || this.Colliding(vctouchWrapper))
						{
							this._touch = vctouchWrapper;
							this.Pressed = true;
						}
					}
				}
			}
		}
		this.UpdateVisibility();
	}

	// Token: 0x0600327C RID: 12924 RVA: 0x00164964 File Offset: 0x00162D64
	protected virtual void UpdateVisibility()
	{
		if (this.Pressed == this._lastPressedState)
		{
			return;
		}
		this._lastPressedState = this.Pressed;
		if (this.Pressed)
		{
			this.ShowPressedState(true);
		}
		else
		{
			this.ShowPressedState(false);
		}
	}

	// Token: 0x17000478 RID: 1144
	// (get) Token: 0x0600327D RID: 12925 RVA: 0x001649A2 File Offset: 0x00162DA2
	// (set) Token: 0x0600327E RID: 12926 RVA: 0x001649AC File Offset: 0x00162DAC
	public bool Pressed
	{
		get
		{
			return this._pressed;
		}
		private set
		{
			if (this._pressed == value)
			{
				return;
			}
			this._pressed = value;
			if (this._pressed)
			{
				if (this.OnPress != null)
				{
					this.OnPress(this);
				}
				this.PressBeganThisFrame = true;
			}
			else
			{
				if (this.OnRelease != null)
				{
					this.OnRelease(this);
				}
				this.PressEndedThisFrame = true;
				this.HoldTime = 0f;
				this._touch = null;
			}
		}
	}

	// Token: 0x17000479 RID: 1145
	// (get) Token: 0x0600327F RID: 12927 RVA: 0x00164A2B File Offset: 0x00162E2B
	// (set) Token: 0x06003280 RID: 12928 RVA: 0x00164A33 File Offset: 0x00162E33
	public bool ForcePressed
	{
		get
		{
			return this._forcePressed;
		}
		set
		{
			this._forcePressed = value;
			if (this._forcePressed)
			{
				this.Pressed = true;
			}
			else
			{
				this.Pressed = !this.PressEnded;
			}
		}
	}

	// Token: 0x1700047A RID: 1146
	// (get) Token: 0x06003281 RID: 12929 RVA: 0x00164A62 File Offset: 0x00162E62
	// (set) Token: 0x06003282 RID: 12930 RVA: 0x00164A6A File Offset: 0x00162E6A
	public float HoldTime { get; private set; }

	// Token: 0x1700047B RID: 1147
	// (get) Token: 0x06003283 RID: 12931 RVA: 0x00164A73 File Offset: 0x00162E73
	public VCTouchWrapper TouchWrapper
	{
		get
		{
			return this._touch;
		}
	}

	// Token: 0x1700047C RID: 1148
	// (get) Token: 0x06003284 RID: 12932 RVA: 0x00164A7B File Offset: 0x00162E7B
	// (set) Token: 0x06003285 RID: 12933 RVA: 0x00164A83 File Offset: 0x00162E83
	public bool PressBeganThisFrame { get; private set; }

	// Token: 0x1700047D RID: 1149
	// (get) Token: 0x06003286 RID: 12934 RVA: 0x00164A8C File Offset: 0x00162E8C
	// (set) Token: 0x06003287 RID: 12935 RVA: 0x00164A94 File Offset: 0x00162E94
	public bool PressEndedThisFrame { get; private set; }

	// Token: 0x1700047E RID: 1150
	// (get) Token: 0x06003288 RID: 12936 RVA: 0x00164AA0 File Offset: 0x00162EA0
	protected bool PressEnded
	{
		get
		{
			return !this.ForcePressed && (this._touch == null || !this._touch.Active || (!this.anyTouchActivatesControl && this.releaseOnMoveOut && !this.Colliding(this._touch)));
		}
	}

	// Token: 0x04002EFC RID: 12028
	public string vcName;

	// Token: 0x04002EFD RID: 12029
	protected static Dictionary<string, VCButtonBase> _instancesByVcName;

	// Token: 0x04002EFE RID: 12030
	public bool touchMustBeginOnCollider = true;

	// Token: 0x04002EFF RID: 12031
	public bool releaseOnMoveOut = true;

	// Token: 0x04002F00 RID: 12032
	public bool anyTouchActivatesControl;

	// Token: 0x04002F01 RID: 12033
	public bool skipCollisionDetection;

	// Token: 0x04002F02 RID: 12034
	public bool debugKeyEnabled;

	// Token: 0x04002F03 RID: 12035
	public KeyCode debugTouchKey = KeyCode.A;

	// Token: 0x04002F04 RID: 12036
	public bool debugTouchKeyTogglesPress;

	// Token: 0x04002F05 RID: 12037
	protected bool _visible;

	// Token: 0x04002F06 RID: 12038
	protected bool _pressed;

	// Token: 0x04002F07 RID: 12039
	protected bool _forcePressed;

	// Token: 0x04002F08 RID: 12040
	protected bool _lastPressedState;

	// Token: 0x04002F09 RID: 12041
	protected VCTouchWrapper _touch;

	// Token: 0x04002F0A RID: 12042
	public VCButtonBase.VCButtonDelegate OnHold;

	// Token: 0x04002F0B RID: 12043
	public VCButtonBase.VCButtonDelegate OnPress;

	// Token: 0x04002F0C RID: 12044
	public VCButtonBase.VCButtonDelegate OnRelease;

	// Token: 0x020006B1 RID: 1713
	// (Invoke) Token: 0x0600328A RID: 12938
	public delegate void VCButtonDelegate(VCButtonBase button);
}
