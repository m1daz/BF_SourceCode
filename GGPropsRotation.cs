using System;
using UnityEngine;

// Token: 0x020001ED RID: 493
public class GGPropsRotation : MonoBehaviour
{
	// Token: 0x06000DAA RID: 3498 RVA: 0x00071209 File Offset: 0x0006F609
	private void Start()
	{
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x0007120B File Offset: 0x0006F60B
	private void Update()
	{
		base.transform.Rotate(0f, this.rotateSpeed * Time.deltaTime, 0f, Space.Self);
	}

	// Token: 0x04000DEE RID: 3566
	private float rotateSpeed = 190f;
}
