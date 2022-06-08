using System;
using UnityEngine;

// Token: 0x0200036E RID: 878
public class ThirdPersonCamera : MonoBehaviour
{
	// Token: 0x06001B5C RID: 7004 RVA: 0x000DBE47 File Offset: 0x000DA247
	private void Start()
	{
		this.follow = GameObject.FindWithTag("Player").transform;
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x000DBE60 File Offset: 0x000DA260
	private void LateUpdate()
	{
		this.targetPosition = this.follow.position + Vector3.up * this.distanceUp - this.follow.forward * this.distanceAway;
		base.transform.position = Vector3.Lerp(base.transform.position, this.targetPosition, Time.deltaTime * this.smooth);
		base.transform.LookAt(this.follow);
	}

	// Token: 0x04001D7D RID: 7549
	public float distanceAway;

	// Token: 0x04001D7E RID: 7550
	public float distanceUp;

	// Token: 0x04001D7F RID: 7551
	public float smooth;

	// Token: 0x04001D80 RID: 7552
	private GameObject hovercraft;

	// Token: 0x04001D81 RID: 7553
	private Vector3 targetPosition;

	// Token: 0x04001D82 RID: 7554
	private Transform follow;
}
