using System;
using UnityEngine;

// Token: 0x02000217 RID: 535
public class GGLaserEffect : MonoBehaviour
{
	// Token: 0x06000E7A RID: 3706 RVA: 0x000798B0 File Offset: 0x00077CB0
	private void Start()
	{
		this.newPos = base.transform.position;
		this.oldPos = this.newPos;
		this.velocity = (float)this.speed * base.transform.forward;
		UnityEngine.Object.Destroy(base.gameObject, this.life);
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x00079908 File Offset: 0x00077D08
	private void Update()
	{
		if (this.hasHit)
		{
			return;
		}
		this.newPos += this.velocity * Time.deltaTime * 10f;
		float magnitude = (this.newPos - this.oldPos).magnitude;
		this.oldPos = base.transform.position;
		base.transform.position = this.newPos;
	}

	// Token: 0x04000FB2 RID: 4018
	private int speed = 50;

	// Token: 0x04000FB3 RID: 4019
	private float life = 0.1f;

	// Token: 0x04000FB4 RID: 4020
	private Vector3 velocity;

	// Token: 0x04000FB5 RID: 4021
	private Vector3 newPos;

	// Token: 0x04000FB6 RID: 4022
	private Vector3 oldPos;

	// Token: 0x04000FB7 RID: 4023
	private bool hasHit;
}
