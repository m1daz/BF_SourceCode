using System;
using UnityEngine;

// Token: 0x02000550 RID: 1360
[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : MonoBehaviour
{
	// Token: 0x0600262B RID: 9771 RVA: 0x0011B4DC File Offset: 0x001198DC
	private void Start()
	{
		this.mTrans = base.transform;
		this.mStart = this.mTrans.localRotation;
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x0011B4FC File Offset: 0x001198FC
	private void Update()
	{
		float deltaTime = RealTime.deltaTime;
		Vector3 vector = UICamera.lastEventPosition;
		float num = (float)Screen.width * 0.5f;
		float num2 = (float)Screen.height * 0.5f;
		if (this.range < 0.1f)
		{
			this.range = 0.1f;
		}
		float x = Mathf.Clamp((vector.x - num) / num / this.range, -1f, 1f);
		float y = Mathf.Clamp((vector.y - num2) / num2 / this.range, -1f, 1f);
		this.mRot = Vector2.Lerp(this.mRot, new Vector2(x, y), deltaTime * 5f);
		this.mTrans.localRotation = this.mStart * Quaternion.Euler(-this.mRot.y * this.degrees.y, this.mRot.x * this.degrees.x, 0f);
	}

	// Token: 0x040026E4 RID: 9956
	public Vector2 degrees = new Vector2(5f, 3f);

	// Token: 0x040026E5 RID: 9957
	public float range = 1f;

	// Token: 0x040026E6 RID: 9958
	private Transform mTrans;

	// Token: 0x040026E7 RID: 9959
	private Quaternion mStart;

	// Token: 0x040026E8 RID: 9960
	private Vector2 mRot = Vector2.zero;
}
