using System;
using UnityEngine;

// Token: 0x020001E4 RID: 484
public class FPSMouseLook : MonoBehaviour
{
	// Token: 0x06000D80 RID: 3456 RVA: 0x000700F3 File Offset: 0x0006E4F3
	private void Awake()
	{
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x000700F8 File Offset: 0x0006E4F8
	private void Start()
	{
		this.mNetworkCharacter = base.GetComponent<GGNetworkCharacter>();
		this.LookObject = base.transform.Find("LookObject").transform;
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x00070148 File Offset: 0x0006E548
	private void Update()
	{
		if (Time.timeScale < 0.01f)
		{
			return;
		}
		if (this.axes == FPSMouseLook.RotationAxes.MouseXAndY)
		{
			float y = base.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * this.sensitivityX;
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			this.LookObject.localEulerAngles = new Vector3(-this.rotationY, 0f, 0f);
			base.transform.localEulerAngles = new Vector3(0f, y, 0f);
		}
		else if (this.axes == FPSMouseLook.RotationAxes.MouseX)
		{
			base.transform.Rotate(0f, Input.GetAxis("Mouse X") * this.sensitivityX, 0f);
		}
		else
		{
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			base.transform.localEulerAngles = new Vector3(-this.rotationY, base.transform.localEulerAngles.y, 0f);
		}
	}

	// Token: 0x04000D9D RID: 3485
	private FPSMouseLook.RotationAxes axes;

	// Token: 0x04000D9E RID: 3486
	private float sensitivity = 4f;

	// Token: 0x04000D9F RID: 3487
	private float aimSensitivity = 2f;

	// Token: 0x04000DA0 RID: 3488
	private float sensitivityX = 5f;

	// Token: 0x04000DA1 RID: 3489
	private float sensitivityY = 5f;

	// Token: 0x04000DA2 RID: 3490
	private float minimumX = -360f;

	// Token: 0x04000DA3 RID: 3491
	private float maximumX = 360f;

	// Token: 0x04000DA4 RID: 3492
	private float minimumY = -40f;

	// Token: 0x04000DA5 RID: 3493
	private float maximumY = 35f;

	// Token: 0x04000DA6 RID: 3494
	private float rotationY;

	// Token: 0x04000DA7 RID: 3495
	private Transform LookObject;

	// Token: 0x04000DA8 RID: 3496
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x020001E5 RID: 485
	private enum RotationAxes
	{
		// Token: 0x04000DAA RID: 3498
		MouseXAndY,
		// Token: 0x04000DAB RID: 3499
		MouseX,
		// Token: 0x04000DAC RID: 3500
		MouseY
	}
}
