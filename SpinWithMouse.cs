using System;
using UnityEngine;

// Token: 0x02000554 RID: 1364
[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class SpinWithMouse : MonoBehaviour
{
	// Token: 0x06002638 RID: 9784 RVA: 0x0011B9AD File Offset: 0x00119DAD
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06002639 RID: 9785 RVA: 0x0011B9BC File Offset: 0x00119DBC
	private void OnDrag(Vector2 delta)
	{
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		if (this.target != null)
		{
			this.target.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.target.localRotation;
		}
		else
		{
			this.mTrans.localRotation = Quaternion.Euler(0f, -0.5f * delta.x * this.speed, 0f) * this.mTrans.localRotation;
		}
	}

	// Token: 0x040026F3 RID: 9971
	public Transform target;

	// Token: 0x040026F4 RID: 9972
	public float speed = 1f;

	// Token: 0x040026F5 RID: 9973
	private Transform mTrans;
}
