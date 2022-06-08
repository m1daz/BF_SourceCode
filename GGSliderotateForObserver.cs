using System;
using UnityEngine;

// Token: 0x020001F1 RID: 497
[AddComponentMenu("Camera-Control/GGSliderotateForObserver")]
public class GGSliderotateForObserver : MonoBehaviour
{
	// Token: 0x06000DB9 RID: 3513 RVA: 0x00071C4C File Offset: 0x0007004C
	private void Update()
	{
		if (UIPauseDirector.mInstance.isCutControl)
		{
			return;
		}
		this.isRotating = false;
		this.isRotating1 = false;
		for (int i = 0; i < Input.touchCount; i++)
		{
			if (Input.GetTouch(i).phase == TouchPhase.Moved && Input.GetTouch(i).position.y <= (float)Screen.height * 0.8f)
			{
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
		if (this.axes == GGSliderotateForObserver.RotationAxes.MouseXAndY)
		{
			if (this.isRotating && !this.isRotating1)
			{
				float num = (this.curX - this.preX) * 0.1f;
				float num2 = (this.curY - this.preY) * 0.1f;
				this.rotationX = base.transform.localEulerAngles.y + num * this.sensitivityX;
				this.rotationY += num2 * this.sensitivityY;
				this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
				base.transform.localEulerAngles = new Vector3(-this.rotationY, this.rotationX, 0f);
			}
			else if (!this.isRotating && this.isRotating1)
			{
				float num3 = (this.curX1 - this.preX1) * 0.1f;
				float num4 = (this.curY1 - this.preY1) * 0.1f;
				this.rotationX = base.transform.localEulerAngles.y + num3 * this.sensitivityX;
				this.rotationY += num4 * this.sensitivityY;
				this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
				base.transform.localEulerAngles = new Vector3(-this.rotationY, this.rotationX, 0f);
			}
			else if (this.isRotating && this.isRotating1)
			{
				float num5 = (this.curX - this.preX + (this.curX1 - this.preX1)) * 0.1f;
				float num6 = (this.curY - this.preY + (this.curY1 - this.preY1)) * 0.1f;
				this.rotationX = base.transform.localEulerAngles.y + num5 * this.sensitivityX;
				this.rotationY += num6 * this.sensitivityY;
				this.rotationY = Mathf.Clamp(this.rotationY, this.minimumY, this.maximumY);
				base.transform.localEulerAngles = new Vector3(-this.rotationY, this.rotationX, 0f);
			}
			else if (!this.isRotating && !this.isRotating1)
			{
				this.preX = (this.curX = (this.preY = (this.curY = 0f)));
				this.preX1 = (this.curX1 = (this.preY1 = (this.curY1 = 0f)));
			}
		}
		else if (this.axes == GGSliderotateForObserver.RotationAxes.MouseX)
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

	// Token: 0x06000DBA RID: 3514 RVA: 0x000722C2 File Offset: 0x000706C2
	private void Start()
	{
	}

	// Token: 0x04000E13 RID: 3603
	public GGSliderotateForObserver.RotationAxes axes;

	// Token: 0x04000E14 RID: 3604
	public float sensitivityX = 2f;

	// Token: 0x04000E15 RID: 3605
	public float sensitivityY = 2f;

	// Token: 0x04000E16 RID: 3606
	public float minimumX = -360f;

	// Token: 0x04000E17 RID: 3607
	public float maximumX = 360f;

	// Token: 0x04000E18 RID: 3608
	private float minimumY = -20f;

	// Token: 0x04000E19 RID: 3609
	private float maximumY = 20f;

	// Token: 0x04000E1A RID: 3610
	public int movedcount;

	// Token: 0x04000E1B RID: 3611
	private float rotationY;

	// Token: 0x04000E1C RID: 3612
	private int touchCount;

	// Token: 0x04000E1D RID: 3613
	public bool rotationDisabled;

	// Token: 0x04000E1E RID: 3614
	public bool isRotating;

	// Token: 0x04000E1F RID: 3615
	public bool isRotating1;

	// Token: 0x04000E20 RID: 3616
	private int rotateCacheTime;

	// Token: 0x04000E21 RID: 3617
	private float preX;

	// Token: 0x04000E22 RID: 3618
	private float curX;

	// Token: 0x04000E23 RID: 3619
	private float preY;

	// Token: 0x04000E24 RID: 3620
	private float curY;

	// Token: 0x04000E25 RID: 3621
	private float preX1;

	// Token: 0x04000E26 RID: 3622
	private float curX1;

	// Token: 0x04000E27 RID: 3623
	private float preY1;

	// Token: 0x04000E28 RID: 3624
	private float curY1;

	// Token: 0x04000E29 RID: 3625
	private float rotationX;

	// Token: 0x04000E2A RID: 3626
	private Transform cameratransform;

	// Token: 0x04000E2B RID: 3627
	private bool cannotRotate;

	// Token: 0x020001F2 RID: 498
	public enum RotationAxes
	{
		// Token: 0x04000E2D RID: 3629
		MouseXAndY,
		// Token: 0x04000E2E RID: 3630
		MouseX,
		// Token: 0x04000E2F RID: 3631
		MouseY
	}
}
