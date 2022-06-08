using System;
using UnityEngine;

// Token: 0x020001EB RID: 491
public class GGModelRatationForX : MonoBehaviour
{
	// Token: 0x06000DA3 RID: 3491 RVA: 0x00070CC8 File Offset: 0x0006F0C8
	private void Start()
	{
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x00070CCA File Offset: 0x0006F0CA
	private void Update()
	{
		base.transform.Rotate(this.rotateSpeed * Time.deltaTime, 0f, 0f, Space.Self);
	}

	// Token: 0x04000DD6 RID: 3542
	private float rotateSpeed = 120f;
}
