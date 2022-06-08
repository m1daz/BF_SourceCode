using System;
using UnityEngine;

// Token: 0x020006BC RID: 1724
public class VCAnalogJoystickPlaymakerUpdater : MonoBehaviour
{
	// Token: 0x060032D8 RID: 13016 RVA: 0x00166144 File Offset: 0x00164544
	private void Start()
	{
		if (this.joystick == null)
		{
			this.joystick = base.gameObject.GetComponent<VCAnalogJoystickBase>();
			if (this.joystick == null)
			{
				VCUtils.DestroyWithError(base.gameObject, "You must specify a joystick for VCAnalogJoystickPlaymakerUpdater to function.  Destroying this object.");
				return;
			}
		}
	}

	// Token: 0x060032D9 RID: 13017 RVA: 0x00166198 File Offset: 0x00164598
	private void Update()
	{
		this.axis.x = this.joystick.AxisX;
		this.axis.y = this.joystick.AxisY;
		this.axisRaw.x = this.joystick.AxisXRaw;
		this.axisRaw.y = this.joystick.AxisYRaw;
		this.tapCount = this.joystick.TapCount;
	}

	// Token: 0x04002F36 RID: 12086
	public VCAnalogJoystickBase joystick;

	// Token: 0x04002F37 RID: 12087
	public Vector3 axis;

	// Token: 0x04002F38 RID: 12088
	public Vector3 axisRaw;

	// Token: 0x04002F39 RID: 12089
	public int tapCount;
}
