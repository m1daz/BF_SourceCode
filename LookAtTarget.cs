using System;
using UnityEngine;

// Token: 0x0200054E RID: 1358
[AddComponentMenu("NGUI/Examples/Look At Target")]
public class LookAtTarget : MonoBehaviour
{
	// Token: 0x06002626 RID: 9766 RVA: 0x0011B3CF File Offset: 0x001197CF
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06002627 RID: 9767 RVA: 0x0011B3E0 File Offset: 0x001197E0
	private void LateUpdate()
	{
		if (this.target != null)
		{
			Vector3 forward = this.target.position - this.mTrans.position;
			float magnitude = forward.magnitude;
			if (magnitude > 0.001f)
			{
				Quaternion b = Quaternion.LookRotation(forward);
				this.mTrans.rotation = Quaternion.Slerp(this.mTrans.rotation, b, Mathf.Clamp01(this.speed * Time.deltaTime));
			}
		}
	}

	// Token: 0x040026E0 RID: 9952
	public int level;

	// Token: 0x040026E1 RID: 9953
	public Transform target;

	// Token: 0x040026E2 RID: 9954
	public float speed = 8f;

	// Token: 0x040026E3 RID: 9955
	private Transform mTrans;
}
