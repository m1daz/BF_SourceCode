using System;
using UnityEngine;

// Token: 0x020006B7 RID: 1719
public class VCDPadWithButtons : VCDPadBase
{
	// Token: 0x17000484 RID: 1156
	// (get) Token: 0x060032B3 RID: 12979 RVA: 0x0016552A File Offset: 0x0016392A
	// (set) Token: 0x060032B4 RID: 12980 RVA: 0x00165532 File Offset: 0x00163932
	public VCButtonBase LeftButton { get; private set; }

	// Token: 0x17000485 RID: 1157
	// (get) Token: 0x060032B5 RID: 12981 RVA: 0x0016553B File Offset: 0x0016393B
	// (set) Token: 0x060032B6 RID: 12982 RVA: 0x00165543 File Offset: 0x00163943
	public VCButtonBase RightButton { get; private set; }

	// Token: 0x17000486 RID: 1158
	// (get) Token: 0x060032B7 RID: 12983 RVA: 0x0016554C File Offset: 0x0016394C
	// (set) Token: 0x060032B8 RID: 12984 RVA: 0x00165554 File Offset: 0x00163954
	public VCButtonBase UpButton { get; private set; }

	// Token: 0x17000487 RID: 1159
	// (get) Token: 0x060032B9 RID: 12985 RVA: 0x0016555D File Offset: 0x0016395D
	// (set) Token: 0x060032BA RID: 12986 RVA: 0x00165565 File Offset: 0x00163965
	public VCButtonBase DownButton { get; private set; }

	// Token: 0x060032BB RID: 12987 RVA: 0x00165570 File Offset: 0x00163970
	protected override bool Init()
	{
		if (!base.Init())
		{
			return false;
		}
		if (this.leftVCButtonObject)
		{
			this.LeftButton = this.leftVCButtonObject.GetComponent<VCButtonBase>();
		}
		if (this.rightVCButtonObject)
		{
			this.RightButton = this.rightVCButtonObject.GetComponent<VCButtonBase>();
		}
		if (this.upVCButtonObject)
		{
			this.UpButton = this.upVCButtonObject.GetComponent<VCButtonBase>();
		}
		if (this.downVCButtonObject)
		{
			this.DownButton = this.downVCButtonObject.GetComponent<VCButtonBase>();
		}
		this.SetPressedGraphics(VCDPadBase.EDirection.Left, false);
		this.SetPressedGraphics(VCDPadBase.EDirection.Right, false);
		this.SetPressedGraphics(VCDPadBase.EDirection.Up, false);
		this.SetPressedGraphics(VCDPadBase.EDirection.Down, false);
		if (base.JoystickMode)
		{
			if (this.LeftButton && !this.LeftButton.skipCollisionDetection)
			{
				Debug.LogWarning("When DPad is in JoystickMode, Buttons should have skipCollisionDetection set to true.  Setting it automatically for LeftButton");
				this.LeftButton.skipCollisionDetection = true;
			}
			if (this.RightButton && !this.RightButton.skipCollisionDetection)
			{
				Debug.LogWarning("When DPad is in JoystickMode, Buttons should have skipCollisionDetection set to true.  Setting it automatically for RightButton");
				this.RightButton.skipCollisionDetection = true;
			}
			if (this.DownButton && !this.DownButton.skipCollisionDetection)
			{
				Debug.LogWarning("When DPad is in JoystickMode, Buttons should have skipCollisionDetection set to true.  Setting it automatically for DownButton");
				this.DownButton.skipCollisionDetection = true;
			}
			if (this.UpButton && !this.UpButton.skipCollisionDetection)
			{
				Debug.LogWarning("When DPad is in JoystickMode, Buttons should have skipCollisionDetection set to true.  Setting it automatically for UpButton");
				this.UpButton.skipCollisionDetection = true;
			}
		}
		return true;
	}

	// Token: 0x060032BC RID: 12988 RVA: 0x00165714 File Offset: 0x00163B14
	protected override void SetPressedGraphics(VCDPadBase.EDirection dir, bool pressed)
	{
		if (!base.JoystickMode)
		{
			return;
		}
		if (dir == VCDPadBase.EDirection.Left && this.LeftButton != null)
		{
			this.LeftButton.ForcePressed = pressed;
		}
		if (dir == VCDPadBase.EDirection.Right && this.RightButton != null)
		{
			this.RightButton.ForcePressed = pressed;
		}
		if (dir == VCDPadBase.EDirection.Up && this.UpButton != null)
		{
			this.UpButton.ForcePressed = pressed;
		}
		if (dir == VCDPadBase.EDirection.Down && this.DownButton != null)
		{
			this.DownButton.ForcePressed = pressed;
		}
	}

	// Token: 0x060032BD RID: 12989 RVA: 0x001657C0 File Offset: 0x00163BC0
	protected override void UpdateStateNonJoystickMode()
	{
		if (this.XAxisEnabled)
		{
			this.SetPressed(VCDPadBase.EDirection.Left, this.ButtonExistsAndIsPressed(this.LeftButton));
			this.SetPressed(VCDPadBase.EDirection.Right, this.ButtonExistsAndIsPressed(this.RightButton));
		}
		if (this.YAxisEnabled)
		{
			this.SetPressed(VCDPadBase.EDirection.Up, this.ButtonExistsAndIsPressed(this.UpButton));
			this.SetPressed(VCDPadBase.EDirection.Down, this.ButtonExistsAndIsPressed(this.DownButton));
		}
	}

	// Token: 0x060032BE RID: 12990 RVA: 0x0016582F File Offset: 0x00163C2F
	protected bool ButtonExistsAndIsPressed(VCButtonBase button)
	{
		return !(button == null) && button.Pressed;
	}

	// Token: 0x04002F29 RID: 12073
	public GameObject leftVCButtonObject;

	// Token: 0x04002F2A RID: 12074
	public GameObject rightVCButtonObject;

	// Token: 0x04002F2B RID: 12075
	public GameObject upVCButtonObject;

	// Token: 0x04002F2C RID: 12076
	public GameObject downVCButtonObject;
}
