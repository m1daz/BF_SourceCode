using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000417 RID: 1047
public class ETCInput : MonoBehaviour
{
	// Token: 0x17000166 RID: 358
	// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x000EB38C File Offset: 0x000E978C
	public static ETCInput instance
	{
		get
		{
			if (!ETCInput._instance)
			{
				ETCInput._instance = (UnityEngine.Object.FindObjectOfType(typeof(ETCInput)) as ETCInput);
				if (!ETCInput._instance)
				{
					GameObject gameObject = new GameObject("InputManager");
					ETCInput._instance = gameObject.AddComponent<ETCInput>();
				}
			}
			return ETCInput._instance;
		}
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x000EB3EC File Offset: 0x000E97EC
	public void RegisterControl(ETCBase ctrl)
	{
		if (this.controls.ContainsKey(ctrl.name))
		{
			Debug.LogWarning("ETCInput control : " + ctrl.name + " already exists");
		}
		else
		{
			this.controls.Add(ctrl.name, ctrl);
			if (ctrl.GetType() == typeof(ETCJoystick))
			{
				this.RegisterAxis((ctrl as ETCJoystick).axisX);
				this.RegisterAxis((ctrl as ETCJoystick).axisY);
			}
			else if (ctrl.GetType() == typeof(ETCTouchPad))
			{
				this.RegisterAxis((ctrl as ETCTouchPad).axisX);
				this.RegisterAxis((ctrl as ETCTouchPad).axisY);
			}
			else if (ctrl.GetType() == typeof(ETCDPad))
			{
				this.RegisterAxis((ctrl as ETCDPad).axisX);
				this.RegisterAxis((ctrl as ETCDPad).axisY);
			}
			else if (ctrl.GetType() == typeof(ETCButton))
			{
				this.RegisterAxis((ctrl as ETCButton).axis);
			}
		}
	}

	// Token: 0x06001EB9 RID: 7865 RVA: 0x000EB51C File Offset: 0x000E991C
	public void UnRegisterControl(ETCBase ctrl)
	{
		if (this.controls.ContainsKey(ctrl.name) && ctrl.enabled)
		{
			this.controls.Remove(ctrl.name);
			if (ctrl.GetType() == typeof(ETCJoystick))
			{
				this.UnRegisterAxis((ctrl as ETCJoystick).axisX);
				this.UnRegisterAxis((ctrl as ETCJoystick).axisY);
			}
			else if (ctrl.GetType() == typeof(ETCTouchPad))
			{
				this.UnRegisterAxis((ctrl as ETCTouchPad).axisX);
				this.UnRegisterAxis((ctrl as ETCTouchPad).axisY);
			}
			else if (ctrl.GetType() == typeof(ETCDPad))
			{
				this.UnRegisterAxis((ctrl as ETCDPad).axisX);
				this.UnRegisterAxis((ctrl as ETCDPad).axisY);
			}
			else if (ctrl.GetType() == typeof(ETCButton))
			{
				this.UnRegisterAxis((ctrl as ETCButton).axis);
			}
		}
	}

	// Token: 0x06001EBA RID: 7866 RVA: 0x000EB636 File Offset: 0x000E9A36
	public void Create()
	{
	}

	// Token: 0x06001EBB RID: 7867 RVA: 0x000EB638 File Offset: 0x000E9A38
	public static void Register(ETCBase ctrl)
	{
		ETCInput.instance.RegisterControl(ctrl);
	}

	// Token: 0x06001EBC RID: 7868 RVA: 0x000EB645 File Offset: 0x000E9A45
	public static void UnRegister(ETCBase ctrl)
	{
		ETCInput.instance.UnRegisterControl(ctrl);
	}

	// Token: 0x06001EBD RID: 7869 RVA: 0x000EB654 File Offset: 0x000E9A54
	public static void SetControlVisible(string ctrlName, bool value)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			ETCInput.control.visible = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + ctrlName + " doesn't exist");
		}
	}

	// Token: 0x06001EBE RID: 7870 RVA: 0x000EB6A0 File Offset: 0x000E9AA0
	public static bool GetControlVisible(string ctrlName)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			return ETCInput.control.visible;
		}
		Debug.LogWarning("ETCInput : " + ctrlName + " doesn't exist");
		return false;
	}

	// Token: 0x06001EBF RID: 7871 RVA: 0x000EB6E0 File Offset: 0x000E9AE0
	public static void SetControlActivated(string ctrlName, bool value)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			ETCInput.control.activated = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + ctrlName + " doesn't exist");
		}
	}

	// Token: 0x06001EC0 RID: 7872 RVA: 0x000EB72C File Offset: 0x000E9B2C
	public static bool GetControlActivated(string ctrlName)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			return ETCInput.control.activated;
		}
		Debug.LogWarning("ETCInput : " + ctrlName + " doesn't exist");
		return false;
	}

	// Token: 0x06001EC1 RID: 7873 RVA: 0x000EB76C File Offset: 0x000E9B6C
	public static void SetControlSwipeIn(string ctrlName, bool value)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			ETCInput.control.isSwipeIn = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + ctrlName + " doesn't exist");
		}
	}

	// Token: 0x06001EC2 RID: 7874 RVA: 0x000EB7B8 File Offset: 0x000E9BB8
	public static bool GetControlSwipeIn(string ctrlName)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			return ETCInput.control.isSwipeIn;
		}
		Debug.LogWarning("ETCInput : " + ctrlName + " doesn't exist");
		return false;
	}

	// Token: 0x06001EC3 RID: 7875 RVA: 0x000EB7F8 File Offset: 0x000E9BF8
	public static void SetControlSwipeOut(string ctrlName, bool value)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			ETCInput.control.isSwipeOut = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + ctrlName + " doesn't exist");
		}
	}

	// Token: 0x06001EC4 RID: 7876 RVA: 0x000EB844 File Offset: 0x000E9C44
	public static bool GetControlSwipeOut(string ctrlName, bool value)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			return ETCInput.control.isSwipeOut;
		}
		Debug.LogWarning("ETCInput : " + ctrlName + " doesn't exist");
		return false;
	}

	// Token: 0x06001EC5 RID: 7877 RVA: 0x000EB884 File Offset: 0x000E9C84
	public static void SetDPadAxesCount(string ctrlName, ETCBase.DPadAxis value)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			ETCInput.control.dPadAxisCount = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + ctrlName + " doesn't exist");
		}
	}

	// Token: 0x06001EC6 RID: 7878 RVA: 0x000EB8D0 File Offset: 0x000E9CD0
	public static ETCBase.DPadAxis GetDPadAxesCount(string ctrlName)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			return ETCInput.control.dPadAxisCount;
		}
		Debug.LogWarning("ETCInput : " + ctrlName + " doesn't exist");
		return ETCBase.DPadAxis.Two_Axis;
	}

	// Token: 0x06001EC7 RID: 7879 RVA: 0x000EB910 File Offset: 0x000E9D10
	public static ETCJoystick GetControlJoystick(string ctrlName)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control) && ETCInput.control.GetType() == typeof(ETCJoystick))
		{
			return (ETCJoystick)ETCInput.control;
		}
		return null;
	}

	// Token: 0x06001EC8 RID: 7880 RVA: 0x000EB960 File Offset: 0x000E9D60
	public static ETCDPad GetControlDPad(string ctrlName)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control) && ETCInput.control.GetType() == typeof(ETCDPad))
		{
			return (ETCDPad)ETCInput.control;
		}
		return null;
	}

	// Token: 0x06001EC9 RID: 7881 RVA: 0x000EB9B0 File Offset: 0x000E9DB0
	public static ETCTouchPad GetControlTouchPad(string ctrlName)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control) && ETCInput.control.GetType() == typeof(ETCTouchPad))
		{
			return (ETCTouchPad)ETCInput.control;
		}
		return null;
	}

	// Token: 0x06001ECA RID: 7882 RVA: 0x000EBA00 File Offset: 0x000E9E00
	public static ETCButton GetControlButton(string ctrlName)
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control) && ETCInput.control.GetType() == typeof(ETCJoystick))
		{
			return (ETCButton)ETCInput.control;
		}
		return null;
	}

	// Token: 0x06001ECB RID: 7883 RVA: 0x000EBA50 File Offset: 0x000E9E50
	public static void SetControlSprite(string ctrlName, Sprite spr, Color color = default(Color))
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			Image component = ETCInput.control.GetComponent<Image>();
			if (component)
			{
				component.sprite = spr;
				component.color = color;
			}
		}
	}

	// Token: 0x06001ECC RID: 7884 RVA: 0x000EBA9C File Offset: 0x000E9E9C
	public static void SetJoystickThumbSprite(string ctrlName, Sprite spr, Color color = default(Color))
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control) && ETCInput.control.GetType() == typeof(ETCJoystick))
		{
			ETCJoystick etcjoystick = (ETCJoystick)ETCInput.control;
			if (etcjoystick)
			{
				Image component = etcjoystick.thumb.GetComponent<Image>();
				if (component)
				{
					component.sprite = spr;
					component.color = color;
				}
			}
		}
	}

	// Token: 0x06001ECD RID: 7885 RVA: 0x000EBB18 File Offset: 0x000E9F18
	public static void SetButtonSprite(string ctrlName, Sprite sprNormal, Sprite sprPress, Color color = default(Color))
	{
		if (ETCInput.instance.controls.TryGetValue(ctrlName, out ETCInput.control))
		{
			ETCButton component = ETCInput.control.GetComponent<ETCButton>();
			component.normalSprite = sprNormal;
			component.normalColor = color;
			component.pressedColor = color;
			component.pressedSprite = sprPress;
			ETCInput.SetControlSprite(ctrlName, sprNormal, color);
		}
	}

	// Token: 0x06001ECE RID: 7886 RVA: 0x000EBB70 File Offset: 0x000E9F70
	public static void SetAxisSpeed(string axisName, float speed)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.speed = speed;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001ECF RID: 7887 RVA: 0x000EBBBC File Offset: 0x000E9FBC
	public static void SetAxisGravity(string axisName, float gravity)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.gravity = gravity;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001ED0 RID: 7888 RVA: 0x000EBC08 File Offset: 0x000EA008
	public static void SetTurnMoveSpeed(string ctrlName, float speed)
	{
		ETCJoystick controlJoystick = ETCInput.GetControlJoystick(ctrlName);
		if (controlJoystick)
		{
			controlJoystick.tmSpeed = speed;
		}
	}

	// Token: 0x06001ED1 RID: 7889 RVA: 0x000EBC30 File Offset: 0x000EA030
	public static void ResetAxis(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.axisValue = 0f;
			ETCInput.axis.axisSpeedValue = 0f;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x000EBC90 File Offset: 0x000EA090
	public static void SetAxisEnabled(string axisName, bool value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.enable = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x000EBCDC File Offset: 0x000EA0DC
	public static bool GetAxisEnabled(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.enable;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001ED4 RID: 7892 RVA: 0x000EBD1C File Offset: 0x000EA11C
	public static void SetAxisInverted(string axisName, bool value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.invertedAxis = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001ED5 RID: 7893 RVA: 0x000EBD68 File Offset: 0x000EA168
	public static bool GetAxisInverted(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.invertedAxis;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001ED6 RID: 7894 RVA: 0x000EBDA8 File Offset: 0x000EA1A8
	public static void SetAxisDeadValue(string axisName, float value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.deadValue = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001ED7 RID: 7895 RVA: 0x000EBDF4 File Offset: 0x000EA1F4
	public static float GetAxisDeadValue(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.deadValue;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001ED8 RID: 7896 RVA: 0x000EBE40 File Offset: 0x000EA240
	public static void SetAxisSensitivity(string axisName, float value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.speed = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001ED9 RID: 7897 RVA: 0x000EBE8C File Offset: 0x000EA28C
	public static float GetAxisSensitivity(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.speed;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001EDA RID: 7898 RVA: 0x000EBED8 File Offset: 0x000EA2D8
	public static void SetAxisThreshold(string axisName, float value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.axisThreshold = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EDB RID: 7899 RVA: 0x000EBF24 File Offset: 0x000EA324
	public static float GetAxisThreshold(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisThreshold;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001EDC RID: 7900 RVA: 0x000EBF70 File Offset: 0x000EA370
	public static void SetAxisInertia(string axisName, bool value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.isEnertia = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EDD RID: 7901 RVA: 0x000EBFBC File Offset: 0x000EA3BC
	public static bool GetAxisInertia(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.isEnertia;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001EDE RID: 7902 RVA: 0x000EBFFC File Offset: 0x000EA3FC
	public static void SetAxisInertiaSpeed(string axisName, float value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.inertia = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EDF RID: 7903 RVA: 0x000EC048 File Offset: 0x000EA448
	public static float GetAxisInertiaSpeed(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.inertia;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x000EC094 File Offset: 0x000EA494
	public static void SetAxisInertiaThreshold(string axisName, float value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.inertiaThreshold = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EE1 RID: 7905 RVA: 0x000EC0E0 File Offset: 0x000EA4E0
	public static float GetAxisInertiaThreshold(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.inertiaThreshold;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x000EC12C File Offset: 0x000EA52C
	public static void SetAxisAutoStabilization(string axisName, bool value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.isAutoStab = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EE3 RID: 7907 RVA: 0x000EC178 File Offset: 0x000EA578
	public static bool GetAxisAutoStabilization(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.isAutoStab;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001EE4 RID: 7908 RVA: 0x000EC1B8 File Offset: 0x000EA5B8
	public static void SetAxisAutoStabilizationSpeed(string axisName, float value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.autoStabSpeed = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x000EC204 File Offset: 0x000EA604
	public static float GetAxisAutoStabilizationSpeed(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.autoStabSpeed;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x000EC250 File Offset: 0x000EA650
	public static void SetAxisAutoStabilizationThreshold(string axisName, float value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.autoStabThreshold = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EE7 RID: 7911 RVA: 0x000EC29C File Offset: 0x000EA69C
	public static float GetAxisAutoStabilizationThreshold(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.autoStabThreshold;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001EE8 RID: 7912 RVA: 0x000EC2E8 File Offset: 0x000EA6E8
	public static void SetAxisClampRotation(string axisName, bool value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.isClampRotation = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EE9 RID: 7913 RVA: 0x000EC334 File Offset: 0x000EA734
	public static bool GetAxisClampRotation(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.isClampRotation;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001EEA RID: 7914 RVA: 0x000EC374 File Offset: 0x000EA774
	public static void SetAxisClampRotationValue(string axisName, float min, float max)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.minAngle = min;
			ETCInput.axis.maxAngle = max;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x000EC3CC File Offset: 0x000EA7CC
	public static void SetAxisClampRotationMinValue(string axisName, float value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.minAngle = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EEC RID: 7916 RVA: 0x000EC418 File Offset: 0x000EA818
	public static void SetAxisClampRotationMaxValue(string axisName, float value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.maxAngle = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EED RID: 7917 RVA: 0x000EC464 File Offset: 0x000EA864
	public static float GetAxisClampRotationMinValue(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.minAngle;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001EEE RID: 7918 RVA: 0x000EC4B0 File Offset: 0x000EA8B0
	public static float GetAxisClampRotationMaxValue(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.maxAngle;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001EEF RID: 7919 RVA: 0x000EC4FC File Offset: 0x000EA8FC
	public static void SetAxisDirecTransform(string axisName, Transform value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.directTransform = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EF0 RID: 7920 RVA: 0x000EC548 File Offset: 0x000EA948
	public static Transform GetAxisDirectTransform(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.directTransform;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return null;
	}

	// Token: 0x06001EF1 RID: 7921 RVA: 0x000EC588 File Offset: 0x000EA988
	public static void SetAxisDirectAction(string axisName, ETCAxis.DirectAction value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.directAction = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EF2 RID: 7922 RVA: 0x000EC5D4 File Offset: 0x000EA9D4
	public static ETCAxis.DirectAction GetAxisDirectAction(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.directAction;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return ETCAxis.DirectAction.Rotate;
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x000EC614 File Offset: 0x000EAA14
	public static void SetAxisAffectedAxis(string axisName, ETCAxis.AxisInfluenced value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.axisInfluenced = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x000EC660 File Offset: 0x000EAA60
	public static ETCAxis.AxisInfluenced GetAxisAffectedAxis(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisInfluenced;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return ETCAxis.AxisInfluenced.X;
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x000EC6A0 File Offset: 0x000EAAA0
	public static void SetAxisOverTime(string axisName, bool value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.isValueOverTime = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EF6 RID: 7926 RVA: 0x000EC6EC File Offset: 0x000EAAEC
	public static bool GetAxisOverTime(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.isValueOverTime;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001EF7 RID: 7927 RVA: 0x000EC72C File Offset: 0x000EAB2C
	public static void SetAxisOverTimeStep(string axisName, float value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.overTimeStep = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x000EC778 File Offset: 0x000EAB78
	public static float GetAxisOverTimeStep(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.overTimeStep;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001EF9 RID: 7929 RVA: 0x000EC7C4 File Offset: 0x000EABC4
	public static void SetAxisOverTimeMaxValue(string axisName, float value)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			ETCInput.axis.maxOverTimeValue = value;
		}
		else
		{
			Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		}
	}

	// Token: 0x06001EFA RID: 7930 RVA: 0x000EC810 File Offset: 0x000EAC10
	public static float GetAxisOverTimeMaxValue(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.maxOverTimeValue;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x000EC85C File Offset: 0x000EAC5C
	public static float GetAxis(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisValue;
		}
		Debug.LogWarning("ETCInput : " + axisName + " doesn't exist");
		return 0f;
	}

	// Token: 0x06001EFC RID: 7932 RVA: 0x000EC8A8 File Offset: 0x000EACA8
	public static float GetAxisSpeed(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisSpeedValue;
		}
		Debug.LogWarning(axisName + " doesn't exist");
		return 0f;
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x000EC8E4 File Offset: 0x000EACE4
	public static bool GetAxisDownUp(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisState == ETCAxis.AxisState.DownUp;
		}
		Debug.LogWarning(axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x000EC930 File Offset: 0x000EAD30
	public static bool GetAxisDownDown(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisState == ETCAxis.AxisState.DownDown;
		}
		Debug.LogWarning(axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x000EC97C File Offset: 0x000EAD7C
	public static bool GetAxisDownRight(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisState == ETCAxis.AxisState.DownRight;
		}
		Debug.LogWarning(axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x000EC9C8 File Offset: 0x000EADC8
	public static bool GetAxisDownLeft(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisState == ETCAxis.AxisState.DownLeft;
		}
		Debug.LogWarning(axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001F01 RID: 7937 RVA: 0x000ECA14 File Offset: 0x000EAE14
	public static bool GetAxisPressedUp(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisState == ETCAxis.AxisState.PressUp;
		}
		Debug.LogWarning(axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x000ECA60 File Offset: 0x000EAE60
	public static bool GetAxisPressedDown(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisState == ETCAxis.AxisState.PressDown;
		}
		Debug.LogWarning(axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x000ECAB0 File Offset: 0x000EAEB0
	public static bool GetAxisPressedRight(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisState == ETCAxis.AxisState.PressRight;
		}
		Debug.LogWarning(axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x000ECB00 File Offset: 0x000EAF00
	public static bool GetAxisPressedLeft(string axisName)
	{
		if (ETCInput.instance.axes.TryGetValue(axisName, out ETCInput.axis))
		{
			return ETCInput.axis.axisState == ETCAxis.AxisState.PressLeft;
		}
		Debug.LogWarning(axisName + " doesn't exist");
		return false;
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x000ECB50 File Offset: 0x000EAF50
	public static bool GetButtonDown(string buttonName)
	{
		if (ETCInput.instance.axes.TryGetValue(buttonName, out ETCInput.axis))
		{
			return ETCInput.axis.axisState == ETCAxis.AxisState.Down;
		}
		Debug.LogWarning(buttonName + " doesn't exist");
		return false;
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x000ECB9C File Offset: 0x000EAF9C
	public static bool GetButton(string buttonName)
	{
		if (ETCInput.instance.axes.TryGetValue(buttonName, out ETCInput.axis))
		{
			return ETCInput.axis.axisState == ETCAxis.AxisState.Down || ETCInput.axis.axisState == ETCAxis.AxisState.Press;
		}
		Debug.LogWarning(buttonName + " doesn't exist");
		return false;
	}

	// Token: 0x06001F07 RID: 7943 RVA: 0x000ECBF8 File Offset: 0x000EAFF8
	public static bool GetButtonUp(string buttonName)
	{
		if (ETCInput.instance.axes.TryGetValue(buttonName, out ETCInput.axis))
		{
			return ETCInput.axis.axisState == ETCAxis.AxisState.Up;
		}
		Debug.LogWarning(buttonName + " doesn't exist");
		return false;
	}

	// Token: 0x06001F08 RID: 7944 RVA: 0x000ECC44 File Offset: 0x000EB044
	public static float GetButtonValue(string buttonName)
	{
		if (ETCInput.instance.axes.TryGetValue(buttonName, out ETCInput.axis))
		{
			return ETCInput.axis.axisValue;
		}
		Debug.LogWarning(buttonName + " doesn't exist");
		return -1f;
	}

	// Token: 0x06001F09 RID: 7945 RVA: 0x000ECC80 File Offset: 0x000EB080
	private void RegisterAxis(ETCAxis axis)
	{
		if (ETCInput.instance.axes.ContainsKey(axis.name))
		{
			Debug.LogWarning("ETCInput axis : " + axis.name + " already exists");
		}
		else
		{
			this.axes.Add(axis.name, axis);
		}
	}

	// Token: 0x06001F0A RID: 7946 RVA: 0x000ECCD8 File Offset: 0x000EB0D8
	private void UnRegisterAxis(ETCAxis axis)
	{
		if (ETCInput.instance.axes.ContainsKey(axis.name))
		{
			this.axes.Remove(axis.name);
		}
	}

	// Token: 0x04002027 RID: 8231
	public static ETCInput _instance;

	// Token: 0x04002028 RID: 8232
	private Dictionary<string, ETCAxis> axes = new Dictionary<string, ETCAxis>();

	// Token: 0x04002029 RID: 8233
	private Dictionary<string, ETCBase> controls = new Dictionary<string, ETCBase>();

	// Token: 0x0400202A RID: 8234
	private static ETCBase control;

	// Token: 0x0400202B RID: 8235
	private static ETCAxis axis;
}
