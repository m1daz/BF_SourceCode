using System;
using UnityEngine;

// Token: 0x020001F7 RID: 503
[AddComponentMenu("Camera-Control/Mobile Mouse Look")]
public class MobileMouseLook : MonoBehaviour
{
	// Token: 0x06000DCA RID: 3530 RVA: 0x00072874 File Offset: 0x00070C74
	private void Update()
	{
		if (this.axes == MobileMouseLook.RotationAxes.MouseXAndY)
		{
			float y = base.transform.localEulerAngles.y + this.mouseInput.AxisX * this.sensitivityX;
			this.rotationY += this.mouseInput.AxisY * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			this.LookObject.localEulerAngles = new Vector3(-this.rotationY, 0f, 0f);
			base.transform.localEulerAngles = new Vector3(0f, y, 0f);
		}
		else if (this.axes == MobileMouseLook.RotationAxes.MouseX)
		{
			base.transform.Rotate(0f, this.mouseInput.AxisX * this.sensitivityX, 0f);
		}
		else
		{
			this.rotationY += this.mouseInput.AxisY * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			base.transform.localEulerAngles = new Vector3(-this.rotationY, base.transform.localEulerAngles.y, 0f);
		}
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x000729D4 File Offset: 0x00070DD4
	private void Start()
	{
		this.mNetworkCharacter = base.GetComponent<GGNetworkCharacter>();
		this.LookObject = base.transform.Find("LookObject").transform;
		this.mouseInput = VCAnalogJoystickBase.GetInstance("RightJoystick");
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	// Token: 0x04000E3F RID: 3647
	public MobileMouseLook.RotationAxes axes;

	// Token: 0x04000E40 RID: 3648
	public float sensitivityX = 3f;

	// Token: 0x04000E41 RID: 3649
	public float sensitivityY = 3f;

	// Token: 0x04000E42 RID: 3650
	private float minimumX = -360f;

	// Token: 0x04000E43 RID: 3651
	private float maximumX = 360f;

	// Token: 0x04000E44 RID: 3652
	private float minimumY = -50f;

	// Token: 0x04000E45 RID: 3653
	private float maximumY = 50f;

	// Token: 0x04000E46 RID: 3654
	private float rotationY;

	// Token: 0x04000E47 RID: 3655
	private Transform LookObject;

	// Token: 0x04000E48 RID: 3656
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x04000E49 RID: 3657
	private VCAnalogJoystickBase mouseInput;

	// Token: 0x020001F8 RID: 504
	public enum RotationAxes
	{
		// Token: 0x04000E4B RID: 3659
		MouseXAndY,
		// Token: 0x04000E4C RID: 3660
		MouseX,
		// Token: 0x04000E4D RID: 3661
		MouseY
	}
}
