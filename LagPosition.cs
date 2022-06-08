using System;
using UnityEngine;

// Token: 0x0200054B RID: 1355
public class LagPosition : MonoBehaviour
{
	// Token: 0x06002617 RID: 9751 RVA: 0x0011B13C File Offset: 0x0011953C
	public void OnRepositionEnd()
	{
		this.Interpolate(1000f);
	}

	// Token: 0x06002618 RID: 9752 RVA: 0x0011B14C File Offset: 0x0011954C
	private void Interpolate(float delta)
	{
		Transform parent = this.mTrans.parent;
		if (parent != null)
		{
			Vector3 vector = parent.position + parent.rotation * this.mRelative;
			this.mAbsolute.x = Mathf.Lerp(this.mAbsolute.x, vector.x, Mathf.Clamp01(delta * this.speed.x));
			this.mAbsolute.y = Mathf.Lerp(this.mAbsolute.y, vector.y, Mathf.Clamp01(delta * this.speed.y));
			this.mAbsolute.z = Mathf.Lerp(this.mAbsolute.z, vector.z, Mathf.Clamp01(delta * this.speed.z));
			this.mTrans.position = this.mAbsolute;
		}
	}

	// Token: 0x06002619 RID: 9753 RVA: 0x0011B23B File Offset: 0x0011963B
	private void Awake()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600261A RID: 9754 RVA: 0x0011B249 File Offset: 0x00119649
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.ResetPosition();
		}
	}

	// Token: 0x0600261B RID: 9755 RVA: 0x0011B25C File Offset: 0x0011965C
	private void Start()
	{
		this.mStarted = true;
		this.ResetPosition();
	}

	// Token: 0x0600261C RID: 9756 RVA: 0x0011B26B File Offset: 0x0011966B
	public void ResetPosition()
	{
		this.mAbsolute = this.mTrans.position;
		this.mRelative = this.mTrans.localPosition;
	}

	// Token: 0x0600261D RID: 9757 RVA: 0x0011B28F File Offset: 0x0011968F
	private void Update()
	{
		this.Interpolate((!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime);
	}

	// Token: 0x040026D4 RID: 9940
	public Vector3 speed = new Vector3(10f, 10f, 10f);

	// Token: 0x040026D5 RID: 9941
	public bool ignoreTimeScale;

	// Token: 0x040026D6 RID: 9942
	private Transform mTrans;

	// Token: 0x040026D7 RID: 9943
	private Vector3 mRelative;

	// Token: 0x040026D8 RID: 9944
	private Vector3 mAbsolute;

	// Token: 0x040026D9 RID: 9945
	private bool mStarted;
}
