using System;
using UnityEngine;

// Token: 0x020001F9 RID: 505
public class ObserverCameraControl : MonoBehaviour
{
	// Token: 0x06000DCD RID: 3533 RVA: 0x00072A8D File Offset: 0x00070E8D
	private void Start()
	{
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x00072A90 File Offset: 0x00070E90
	private void LateUpdate()
	{
		this.closer = false;
		Vector3 normalized = (base.transform.position - this.target.position).normalized;
		RaycastHit raycastHit;
		if (Physics.Raycast(this.target.position, normalized, out raycastHit))
		{
			if (Vector3.Distance(raycastHit.point, this.target.position) < this.distance)
			{
				this.closer = true;
			}
			else
			{
				this.closer = false;
			}
		}
		if (this.closer)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, raycastHit.point, 0.5f);
		}
		else
		{
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, this.originPosition, 0.5f);
		}
	}

	// Token: 0x04000E4E RID: 3662
	public Transform target;

	// Token: 0x04000E4F RID: 3663
	public float distance = 4f;

	// Token: 0x04000E50 RID: 3664
	private bool closer;

	// Token: 0x04000E51 RID: 3665
	private Vector3 originPosition = new Vector3(0f, 0f, -4f);

	// Token: 0x04000E52 RID: 3666
	public float height = 5f;

	// Token: 0x04000E53 RID: 3667
	public float heightDamping = 2f;

	// Token: 0x04000E54 RID: 3668
	public float rotationDamping = 3f;
}
