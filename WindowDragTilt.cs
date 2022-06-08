using System;
using UnityEngine;

// Token: 0x02000558 RID: 1368
[AddComponentMenu("NGUI/Examples/Window Drag Tilt")]
public class WindowDragTilt : MonoBehaviour
{
	// Token: 0x06002644 RID: 9796 RVA: 0x0011BD40 File Offset: 0x0011A140
	private void OnEnable()
	{
		this.mTrans = base.transform;
		this.mLastPos = this.mTrans.position;
	}

	// Token: 0x06002645 RID: 9797 RVA: 0x0011BD60 File Offset: 0x0011A160
	private void Update()
	{
		Vector3 vector = this.mTrans.position - this.mLastPos;
		this.mLastPos = this.mTrans.position;
		this.mAngle += vector.x * this.degrees;
		this.mAngle = NGUIMath.SpringLerp(this.mAngle, 0f, 20f, Time.deltaTime);
		this.mTrans.localRotation = Quaternion.Euler(0f, 0f, -this.mAngle);
	}

	// Token: 0x040026FE RID: 9982
	public int updateOrder;

	// Token: 0x040026FF RID: 9983
	public float degrees = 30f;

	// Token: 0x04002700 RID: 9984
	private Vector3 mLastPos;

	// Token: 0x04002701 RID: 9985
	private Transform mTrans;

	// Token: 0x04002702 RID: 9986
	private float mAngle;
}
