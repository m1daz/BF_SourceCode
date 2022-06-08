using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B2 RID: 1714
public abstract class VCDPadBase : MonoBehaviour
{
	// Token: 0x0600328E RID: 12942 RVA: 0x00164B54 File Offset: 0x00162F54
	protected void AddInstance()
	{
		if (string.IsNullOrEmpty(this.vcName))
		{
			return;
		}
		if (VCDPadBase._instancesByVcName == null)
		{
			VCDPadBase._instancesByVcName = new Dictionary<string, VCDPadBase>();
		}
		while (VCDPadBase._instancesByVcName.ContainsKey(this.vcName))
		{
			if (VCDPadBase._instancesByVcName[this.vcName] == null)
			{
				VCDPadBase._instancesByVcName.Remove(this.vcName);
			}
			else
			{
				this.vcName += "_copy";
				Debug.LogWarning("Attempting to add instance with duplicate VCName!\nVCNames must be unique -- renaming this instance to " + this.vcName);
			}
		}
		VCDPadBase._instancesByVcName.Add(this.vcName, this);
	}

	// Token: 0x0600328F RID: 12943 RVA: 0x00164C0D File Offset: 0x0016300D
	public static VCDPadBase GetInstance(string vcName)
	{
		if (VCDPadBase._instancesByVcName == null || !VCDPadBase._instancesByVcName.ContainsKey(vcName))
		{
			return null;
		}
		return VCDPadBase._instancesByVcName[vcName];
	}

	// Token: 0x06003290 RID: 12944
	protected abstract void SetPressedGraphics(VCDPadBase.EDirection dir, bool pressed);

	// Token: 0x06003291 RID: 12945 RVA: 0x00164C36 File Offset: 0x00163036
	public bool Pressed(VCDPadBase.EDirection dir)
	{
		if (dir == VCDPadBase.EDirection.None)
		{
			return this._pressedField == 0;
		}
		return (this._pressedField & (int)dir) != 0;
	}

	// Token: 0x06003292 RID: 12946 RVA: 0x00164C56 File Offset: 0x00163056
	protected void Start()
	{
		this.Init();
	}

	// Token: 0x06003293 RID: 12947 RVA: 0x00164C60 File Offset: 0x00163060
	protected virtual bool Init()
	{
		base.useGUILayout = false;
		if (VCTouchController.Instance == null)
		{
			Debug.LogWarning("Cannot find VCTouchController!\nVirtualControls requires a gameObject which has VCTouchController component attached in scene. Adding one for you...");
			base.gameObject.AddComponent<VCTouchController>();
		}
		if (this.JoystickMode && !this.joystick.measureDeltaRelativeToCenter)
		{
			Debug.LogWarning("DPad in joystickMode may not function correctly when joystick's measureDeltaRelativeToCenter is not true.");
		}
		this.AddInstance();
		return true;
	}

	// Token: 0x06003294 RID: 12948 RVA: 0x00164CC6 File Offset: 0x001630C6
	protected virtual void Update()
	{
		if (this.JoystickMode)
		{
			this.UpdateStateJoystickMode();
		}
		else
		{
			this.UpdateStateNonJoystickMode();
		}
	}

	// Token: 0x06003295 RID: 12949 RVA: 0x00164CE4 File Offset: 0x001630E4
	protected virtual void UpdateStateJoystickMode()
	{
		this.SetPressed(VCDPadBase.EDirection.Right, this.joystick.AxisX > 0f && this.XAxisEnabled);
		this.SetPressed(VCDPadBase.EDirection.Left, this.joystick.AxisX < 0f && this.XAxisEnabled);
		this.SetPressed(VCDPadBase.EDirection.Up, this.joystick.AxisY > 0f && this.YAxisEnabled);
		this.SetPressed(VCDPadBase.EDirection.Down, this.joystick.AxisY < 0f && this.YAxisEnabled);
	}

	// Token: 0x06003296 RID: 12950 RVA: 0x00164D85 File Offset: 0x00163185
	protected virtual void UpdateStateNonJoystickMode()
	{
	}

	// Token: 0x06003297 RID: 12951 RVA: 0x00164D87 File Offset: 0x00163187
	protected virtual void SetPressed(VCDPadBase.EDirection dir, bool pressed)
	{
		if (this.Pressed(dir) == pressed)
		{
			return;
		}
		this.SetBitfield(dir, pressed);
		this.SetPressedGraphics(dir, pressed);
	}

	// Token: 0x06003298 RID: 12952 RVA: 0x00164DA7 File Offset: 0x001631A7
	protected void SetBitfield(VCDPadBase.EDirection dir, bool pressed)
	{
		this._pressedField &= (int)(~(int)dir);
		if (pressed)
		{
			this._pressedField |= (int)dir;
		}
	}

	// Token: 0x06003299 RID: 12953 RVA: 0x00164DCC File Offset: 0x001631CC
	protected VCDPadBase.EDirection GetOpposite(VCDPadBase.EDirection dir)
	{
		switch (dir)
		{
		case VCDPadBase.EDirection.Up:
			return VCDPadBase.EDirection.Down;
		case VCDPadBase.EDirection.Down:
			return VCDPadBase.EDirection.Up;
		case VCDPadBase.EDirection.Left:
			return VCDPadBase.EDirection.Right;
		case VCDPadBase.EDirection.Right:
			return VCDPadBase.EDirection.Left;
		}
		return VCDPadBase.EDirection.None;
	}

	// Token: 0x1700047F RID: 1151
	// (get) Token: 0x0600329A RID: 12954 RVA: 0x00164E04 File Offset: 0x00163204
	public bool Up
	{
		get
		{
			return this.Pressed(VCDPadBase.EDirection.Up);
		}
	}

	// Token: 0x17000480 RID: 1152
	// (get) Token: 0x0600329B RID: 12955 RVA: 0x00164E0D File Offset: 0x0016320D
	public bool Down
	{
		get
		{
			return this.Pressed(VCDPadBase.EDirection.Down);
		}
	}

	// Token: 0x17000481 RID: 1153
	// (get) Token: 0x0600329C RID: 12956 RVA: 0x00164E16 File Offset: 0x00163216
	public bool Left
	{
		get
		{
			return this.Pressed(VCDPadBase.EDirection.Left);
		}
	}

	// Token: 0x17000482 RID: 1154
	// (get) Token: 0x0600329D RID: 12957 RVA: 0x00164E1F File Offset: 0x0016321F
	public bool Right
	{
		get
		{
			return this.Pressed(VCDPadBase.EDirection.Right);
		}
	}

	// Token: 0x17000483 RID: 1155
	// (get) Token: 0x0600329E RID: 12958 RVA: 0x00164E28 File Offset: 0x00163228
	protected bool JoystickMode
	{
		get
		{
			return this.joystick != null;
		}
	}

	// Token: 0x04002F10 RID: 12048
	public string vcName;

	// Token: 0x04002F11 RID: 12049
	private static Dictionary<string, VCDPadBase> _instancesByVcName;

	// Token: 0x04002F12 RID: 12050
	public VCAnalogJoystickBase joystick;

	// Token: 0x04002F13 RID: 12051
	public bool XAxisEnabled = true;

	// Token: 0x04002F14 RID: 12052
	public bool YAxisEnabled = true;

	// Token: 0x04002F15 RID: 12053
	public bool debugKeysEnabled;

	// Token: 0x04002F16 RID: 12054
	public bool debugTouchKeysTogglesPress;

	// Token: 0x04002F17 RID: 12055
	public KeyCode debugLeftKey = KeyCode.Keypad4;

	// Token: 0x04002F18 RID: 12056
	public KeyCode debugRightKey = KeyCode.Keypad6;

	// Token: 0x04002F19 RID: 12057
	public KeyCode debugUpKey = KeyCode.Keypad8;

	// Token: 0x04002F1A RID: 12058
	public KeyCode debugDownKey = KeyCode.Keypad5;

	// Token: 0x04002F1B RID: 12059
	protected int _pressedField;

	// Token: 0x020006B3 RID: 1715
	public enum EDirection
	{
		// Token: 0x04002F1D RID: 12061
		None,
		// Token: 0x04002F1E RID: 12062
		Up,
		// Token: 0x04002F1F RID: 12063
		Down,
		// Token: 0x04002F20 RID: 12064
		Left = 4,
		// Token: 0x04002F21 RID: 12065
		Right = 8
	}
}
