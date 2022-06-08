using System;
using UnityEngine;

// Token: 0x020001F3 RID: 499
public class GGTijiguangRotation : MonoBehaviour
{
	// Token: 0x06000DBC RID: 3516 RVA: 0x000722D7 File Offset: 0x000706D7
	private void Start()
	{
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x000722D9 File Offset: 0x000706D9
	private void Update()
	{
		base.transform.Rotate(0f, this.rotateSpeed * Time.deltaTime, 0f, Space.Self);
	}

	// Token: 0x04000E30 RID: 3632
	private float rotateSpeed = 5f;
}
