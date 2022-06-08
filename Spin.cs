using System;
using UnityEngine;

// Token: 0x02000553 RID: 1363
[AddComponentMenu("NGUI/Examples/Spin")]
public class Spin : MonoBehaviour
{
	// Token: 0x06002633 RID: 9779 RVA: 0x0011B8B9 File Offset: 0x00119CB9
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRb = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06002634 RID: 9780 RVA: 0x0011B8D3 File Offset: 0x00119CD3
	private void Update()
	{
		if (this.mRb == null)
		{
			this.ApplyDelta((!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime);
		}
	}

	// Token: 0x06002635 RID: 9781 RVA: 0x0011B906 File Offset: 0x00119D06
	private void FixedUpdate()
	{
		if (this.mRb != null)
		{
			this.ApplyDelta(Time.deltaTime);
		}
	}

	// Token: 0x06002636 RID: 9782 RVA: 0x0011B924 File Offset: 0x00119D24
	public void ApplyDelta(float delta)
	{
		delta *= 360f;
		Quaternion rhs = Quaternion.Euler(this.rotationsPerSecond * delta);
		if (this.mRb == null)
		{
			this.mTrans.rotation = this.mTrans.rotation * rhs;
		}
		else
		{
			this.mRb.MoveRotation(this.mRb.rotation * rhs);
		}
	}

	// Token: 0x040026EF RID: 9967
	public Vector3 rotationsPerSecond = new Vector3(0f, 0.1f, 0f);

	// Token: 0x040026F0 RID: 9968
	public bool ignoreTimeScale;

	// Token: 0x040026F1 RID: 9969
	private Rigidbody mRb;

	// Token: 0x040026F2 RID: 9970
	private Transform mTrans;
}
