using System;
using UnityEngine;

// Token: 0x020006C6 RID: 1734
public class CameraFacingBillboard : MonoBehaviour
{
	// Token: 0x06003302 RID: 13058 RVA: 0x001668D8 File Offset: 0x00164CD8
	public Vector3 GetAxis(CameraFacingBillboard.Axis refAxis)
	{
		switch (refAxis)
		{
		case CameraFacingBillboard.Axis.down:
			return Vector3.down;
		case CameraFacingBillboard.Axis.left:
			return Vector3.left;
		case CameraFacingBillboard.Axis.right:
			return Vector3.right;
		case CameraFacingBillboard.Axis.forward:
			return Vector3.forward;
		case CameraFacingBillboard.Axis.back:
			return Vector3.back;
		default:
			return Vector3.up;
		}
	}

	// Token: 0x06003303 RID: 13059 RVA: 0x00166929 File Offset: 0x00164D29
	private void Awake()
	{
		if (!this.referenceCamera)
		{
			this.referenceCamera = Camera.main;
		}
	}

	// Token: 0x06003304 RID: 13060 RVA: 0x00166948 File Offset: 0x00164D48
	private void Update()
	{
		Vector3 worldPosition = base.transform.position + this.referenceCamera.transform.rotation * ((!this.reverseFace) ? Vector3.back : Vector3.forward);
		Vector3 worldUp = this.referenceCamera.transform.rotation * this.GetAxis(this.axis);
		base.transform.LookAt(worldPosition, worldUp);
	}

	// Token: 0x04002F57 RID: 12119
	private Camera referenceCamera;

	// Token: 0x04002F58 RID: 12120
	public bool reverseFace;

	// Token: 0x04002F59 RID: 12121
	public CameraFacingBillboard.Axis axis;

	// Token: 0x020006C7 RID: 1735
	public enum Axis
	{
		// Token: 0x04002F5B RID: 12123
		up,
		// Token: 0x04002F5C RID: 12124
		down,
		// Token: 0x04002F5D RID: 12125
		left,
		// Token: 0x04002F5E RID: 12126
		right,
		// Token: 0x04002F5F RID: 12127
		forward,
		// Token: 0x04002F60 RID: 12128
		back
	}
}
