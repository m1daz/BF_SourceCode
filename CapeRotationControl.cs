using System;
using UnityEngine;

// Token: 0x020001DD RID: 477
public class CapeRotationControl : MonoBehaviour
{
	// Token: 0x06000D5A RID: 3418 RVA: 0x0006E930 File Offset: 0x0006CD30
	private void Start()
	{
		base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, 349f, base.transform.localEulerAngles.z);
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0006E978 File Offset: 0x0006CD78
	private void Update()
	{
		this.tempTime += Time.deltaTime;
		if (this.tempTime > this.rotationTime)
		{
			this.tempAngle = UnityEngine.Random.Range(this.minAngle, this.maxAngle);
			this.rotationTime = UnityEngine.Random.Range(0.5f, 1.5f);
			this.tempTime = 0f;
			this.speed = UnityEngine.Random.Range(0.5f, 2f);
		}
		this.curAngle = Mathf.Lerp(this.curAngle, this.tempAngle, Time.deltaTime * this.speed);
		base.transform.localEulerAngles = new Vector3(base.transform.localEulerAngles.x, this.curAngle, base.transform.localEulerAngles.z);
	}

	// Token: 0x04000D53 RID: 3411
	private float maxAngle = 357f;

	// Token: 0x04000D54 RID: 3412
	private float minAngle = 325f;

	// Token: 0x04000D55 RID: 3413
	private float tempTime;

	// Token: 0x04000D56 RID: 3414
	private float rotationTime = 4f;

	// Token: 0x04000D57 RID: 3415
	private float tempAngle = 349f;

	// Token: 0x04000D58 RID: 3416
	private float curAngle = 349f;

	// Token: 0x04000D59 RID: 3417
	private float speed = 1f;
}
