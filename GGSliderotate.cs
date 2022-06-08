using System;
using UnityEngine;

// Token: 0x020001EF RID: 495
[AddComponentMenu("Camera-Control/Sliderotate")]
public class GGSliderotate : MonoBehaviour
{
	// Token: 0x06000DB4 RID: 3508 RVA: 0x0007137C File Offset: 0x0006F77C
	private void Update()
	{
		if (!this.isUIHelp && UIPauseDirector.mInstance.isCutControl)
		{
			return;
		}
		this.isRotating = false;
		this.isRotating1 = false;
		for (int i = 0; i < Input.touchCount; i++)
		{
			if (Input.GetTouch(i).phase == TouchPhase.Moved && Input.GetTouch(i).position.x >= (float)Screen.width * 0.4f && Input.GetTouch(i).position.y <= (float)Screen.height * 0.72f)
			{
				if (!this.isUIHelp)
				{
					if (this.mNetworkPlayerLogic.showInstallSchedule)
					{
						this.mNetworkPlayerLogic.OnInstallBtnReleased();
						this.mNetworkPlayerLogic.HideInstallBombButton();
					}
					else if (this.mNetworkPlayerLogic.showRemoveSchedule)
					{
						this.mNetworkPlayerLogic.OnUninstallBtnReleased();
						this.mNetworkPlayerLogic.HideUninstallBombButton();
					}
				}
				Touch touch = Input.GetTouch(i);
				if (i == 0)
				{
					if (this.preX == 0f && this.preY == 0f && this.curX == 0f && this.curY == 0f)
					{
						this.preX = (this.curX = touch.position.x);
						this.preY = (this.curY = touch.position.y);
					}
					else
					{
						this.preX = this.curX;
						this.curX = touch.position.x;
						this.preY = this.curY;
						this.curY = touch.position.y;
					}
					this.isRotating = true;
				}
				if (i == 1)
				{
					if (this.preX1 == 0f && this.preY1 == 0f && this.curX1 == 0f && this.curY1 == 0f)
					{
						this.preX1 = (this.curX1 = touch.position.x);
						this.preY1 = (this.curY1 = touch.position.y);
					}
					else
					{
						this.preX1 = this.curX1;
						this.curX1 = touch.position.x;
						this.preY1 = this.curY1;
						this.curY1 = touch.position.y;
					}
					this.isRotating1 = true;
				}
			}
		}
		if (this.axes == GGSliderotate.RotationAxes.MouseXAndY)
		{
			if (this.isRotating && !this.isRotating1)
			{
				float num = (this.curX - this.preX) * 0.1f;
				float num2 = (this.curY - this.preY) * 0.1f;
				this.rotationX = base.transform.localEulerAngles.y + num * this.sensitivityX;
				this.rotationY += num2 * this.sensitivityY;
				this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
				base.transform.localEulerAngles = new Vector3(0f, this.rotationX, 0f);
				this.cameratransform.localEulerAngles = new Vector3(-this.rotationY, 0f, 0f);
			}
			else if (!this.isRotating && this.isRotating1)
			{
				float num3 = (this.curX1 - this.preX1) * 0.1f;
				float num4 = (this.curY1 - this.preY1) * 0.1f;
				this.rotationX = base.transform.localEulerAngles.y + num3 * this.sensitivityX;
				this.rotationY += num4 * this.sensitivityY;
				this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
				base.transform.localEulerAngles = new Vector3(0f, this.rotationX, 0f);
				this.cameratransform.localEulerAngles = new Vector3(-this.rotationY, 0f, 0f);
			}
			else if (this.isRotating && this.isRotating1)
			{
				float num5 = (this.curX - this.preX + (this.curX1 - this.preX1)) * 0.1f;
				float num6 = (this.curY - this.preY + (this.curY1 - this.preY1)) * 0.1f;
				this.rotationX = base.transform.localEulerAngles.y + num5 * this.sensitivityX;
				this.rotationY += num6 * this.sensitivityY;
				this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
				base.transform.localEulerAngles = new Vector3(0f, this.rotationX, 0f);
				this.cameratransform.localEulerAngles = new Vector3(-this.rotationY, 0f, 0f);
			}
			else if (!this.isRotating && !this.isRotating1)
			{
				this.preX = (this.curX = (this.preY = (this.curY = 0f)));
				this.preX1 = (this.curX1 = (this.preY1 = (this.curY1 = 0f)));
			}
		}
		else if (this.axes == GGSliderotate.RotationAxes.MouseX)
		{
			if (this.touchCount <= 2)
			{
				this.preX = (this.curX = (this.preY = (this.curY = 0f)));
				this.isRotating = false;
				return;
			}
			this.isRotating = true;
			this.rotateCacheTime = 2;
			float num7 = (this.curX - this.preX) * 0.1f;
			float num8 = (this.curY - this.preY) * 0.1f;
			this.rotationX = base.transform.localEulerAngles.y + num7 * this.sensitivityX;
			this.rotationY += num8 * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			base.transform.localEulerAngles = new Vector3(-this.rotationY, this.rotationX, 0f);
		}
		else
		{
			this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
			this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
			base.transform.localEulerAngles = new Vector3(-this.rotationY, base.transform.localEulerAngles.y, 0f);
		}
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x00071AE0 File Offset: 0x0006FEE0
	private void Start()
	{
		if (Application.loadedLevelName == "UIHelp")
		{
			this.isUIHelp = true;
		}
		if (!this.isUIHelp)
		{
			this.mNetworkPlayerLogic = base.GetComponent<GGNetWorkPlayerlogic>();
		}
		this.cameratransform = base.transform.Find("LookObject").transform;
		if (Screen.height > 400)
		{
			this.sensitivity_count = 0.01f;
			this.sensitivity_min = 0.1f;
		}
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
		float num = (float)UIUserDataController.GetSensitivity() * this.sensitivity_count + this.sensitivity_min;
		this.sensitivityX = (this.sensitivityY = num);
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x00071BA0 File Offset: 0x0006FFA0
	public void Recoil(float recoilPower)
	{
		this.cameratransform.localEulerAngles -= new Vector3(recoilPower, 0f, 0f);
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x00071BC8 File Offset: 0x0006FFC8
	public void RecoilRecover(Vector3 DefaultAngle)
	{
		this.cameratransform.localEulerAngles += DefaultAngle;
		this.RecoilRecoverTarget = this.cameratransform.localEulerAngles;
	}

	// Token: 0x04000DF1 RID: 3569
	public GGSliderotate.RotationAxes axes;

	// Token: 0x04000DF2 RID: 3570
	public float sensitivityX = 3f;

	// Token: 0x04000DF3 RID: 3571
	public float sensitivityY = 3f;

	// Token: 0x04000DF4 RID: 3572
	public float minimumX = -360f;

	// Token: 0x04000DF5 RID: 3573
	public float maximumX = 360f;

	// Token: 0x04000DF6 RID: 3574
	private float minimumY = -50f;

	// Token: 0x04000DF7 RID: 3575
	private float maximumY = 50f;

	// Token: 0x04000DF8 RID: 3576
	public int movedcount;

	// Token: 0x04000DF9 RID: 3577
	private float rotationY;

	// Token: 0x04000DFA RID: 3578
	private float sensitivity_count = 0.04f;

	// Token: 0x04000DFB RID: 3579
	private float sensitivity_min = 0.8f;

	// Token: 0x04000DFC RID: 3580
	private int touchCount;

	// Token: 0x04000DFD RID: 3581
	public bool rotationDisabled;

	// Token: 0x04000DFE RID: 3582
	public bool isRotating;

	// Token: 0x04000DFF RID: 3583
	public bool isRotating1;

	// Token: 0x04000E00 RID: 3584
	private int rotateCacheTime;

	// Token: 0x04000E01 RID: 3585
	private float preX;

	// Token: 0x04000E02 RID: 3586
	private float curX;

	// Token: 0x04000E03 RID: 3587
	private float preY;

	// Token: 0x04000E04 RID: 3588
	private float curY;

	// Token: 0x04000E05 RID: 3589
	private float preX1;

	// Token: 0x04000E06 RID: 3590
	private float curX1;

	// Token: 0x04000E07 RID: 3591
	private float preY1;

	// Token: 0x04000E08 RID: 3592
	private float curY1;

	// Token: 0x04000E09 RID: 3593
	private float rotationX;

	// Token: 0x04000E0A RID: 3594
	public Transform cameratransform;

	// Token: 0x04000E0B RID: 3595
	private bool cannotRotate;

	// Token: 0x04000E0C RID: 3596
	private GGNetWorkPlayerlogic mNetworkPlayerLogic;

	// Token: 0x04000E0D RID: 3597
	private bool isUIHelp;

	// Token: 0x04000E0E RID: 3598
	private Vector3 RecoilRecoverTarget;

	// Token: 0x020001F0 RID: 496
	public enum RotationAxes
	{
		// Token: 0x04000E10 RID: 3600
		MouseXAndY,
		// Token: 0x04000E11 RID: 3601
		MouseX,
		// Token: 0x04000E12 RID: 3602
		MouseY
	}
}
