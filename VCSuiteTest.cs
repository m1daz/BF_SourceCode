using System;
using UnityEngine;

// Token: 0x020006AD RID: 1709
public class VCSuiteTest : MonoBehaviour
{
	// Token: 0x0600324D RID: 12877 RVA: 0x001638EF File Offset: 0x00161CEF
	private void Start()
	{
	}

	// Token: 0x0600324E RID: 12878 RVA: 0x001638F4 File Offset: 0x00161CF4
	private void Update()
	{
		if (this.cubeContainer)
		{
			VCDPadBase instance = VCDPadBase.GetInstance("dpad");
			if (instance)
			{
				if (instance.Left)
				{
					this.cubeContainer.transform.Translate(-this.moveSpeed * Time.deltaTime, 0f, 0f);
				}
				if (instance.Right)
				{
					this.cubeContainer.transform.Translate(this.moveSpeed * Time.deltaTime, 0f, 0f);
				}
				if (instance.Up)
				{
					this.cubeContainer.transform.Translate(0f, this.moveSpeed * Time.deltaTime, 0f);
				}
				if (instance.Down)
				{
					this.cubeContainer.transform.Translate(0f, -this.moveSpeed * Time.deltaTime, 0f);
				}
			}
			VCAnalogJoystickBase instance2 = VCAnalogJoystickBase.GetInstance("stick");
			if (instance2 != null)
			{
				this.cubeContainer.transform.Translate(this.moveSpeed * Time.deltaTime * instance2.AxisX, this.moveSpeed * Time.deltaTime * instance2.AxisY, 0f);
			}
		}
		if (this.cube)
		{
			this.cube.transform.RotateAroundLocal(new Vector3(1f, 1f, 0f), Time.deltaTime);
		}
		VCButtonBase instance3 = VCButtonBase.GetInstance("A");
		if (instance3 != null && this.cubeContainer)
		{
			ParticleSystem componentInChildren = this.cubeContainer.GetComponentInChildren<ParticleSystem>();
			if (componentInChildren != null)
			{
				componentInChildren.emissionRate = instance3.HoldTime * 50f;
			}
			VCAnalogJoystickBase instance4 = VCAnalogJoystickBase.GetInstance("stick");
			if (instance4 != null && componentInChildren != null && instance4.TapCount > 1)
			{
				componentInChildren.emissionRate = 150f;
			}
		}
		if (instance3 != null)
		{
			if (instance3.PressBeganThisFrame)
			{
				Debug.Log("Press began on frame " + Time.frameCount);
			}
			if (instance3.PressEndedThisFrame)
			{
				Debug.Log("Press ended on frame " + Time.frameCount);
			}
		}
	}

	// Token: 0x0600324F RID: 12879 RVA: 0x00163B5C File Offset: 0x00161F5C
	private void OnGUI()
	{
		if (VCAnalogJoystickBase.GetInstance("stick") != null)
		{
			GUI.Label(new Rect(10f, 10f, 300f, 20f), string.Concat(new object[]
			{
				"Joystick Axes: ",
				VCAnalogJoystickBase.GetInstance("stick").AxisX,
				" ",
				VCAnalogJoystickBase.GetInstance("stick").AxisY
			}));
		}
		if (VCButtonBase.GetInstance("A") != null)
		{
			GUI.Label(new Rect(10f, 30f, 300f, 20f), "Button Hold (s): " + VCButtonBase.GetInstance("A").HoldTime.ToString());
		}
		VCDPadBase instance = VCDPadBase.GetInstance("dpad");
		if (instance != null)
		{
			string text = "DPad: ";
			if (instance.Left)
			{
				text += "Left ";
			}
			if (instance.Right)
			{
				text += "Right ";
			}
			if (instance.Up)
			{
				text += "Up ";
			}
			if (instance.Down)
			{
				text += "Down ";
			}
			if (instance.Pressed(VCDPadBase.EDirection.None))
			{
				text += "(No Direction)";
			}
			GUI.Label(new Rect(10f, 50f, 300f, 20f), text);
		}
		GUI.Label(new Rect(10f, 70f, 300f, 20f), "Move cube using controls");
		GUI.Label(new Rect(10f, 90f, 300f, 20f), "Double tap joystick / Press A for particles");
	}

	// Token: 0x04002ECE RID: 11982
	public GameObject cube;

	// Token: 0x04002ECF RID: 11983
	public GameObject cubeContainer;

	// Token: 0x04002ED0 RID: 11984
	public float moveSpeed = 5f;
}
