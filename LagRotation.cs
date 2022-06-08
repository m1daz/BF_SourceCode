using System;
using UnityEngine;

// Token: 0x0200054C RID: 1356
[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	// Token: 0x0600261F RID: 9759 RVA: 0x0011B2C4 File Offset: 0x001196C4
	public void OnRepositionEnd()
	{
		this.Interpolate(1000f);
	}

	// Token: 0x06002620 RID: 9760 RVA: 0x0011B2D4 File Offset: 0x001196D4
	private void Interpolate(float delta)
	{
		if (this.mTrans != null)
		{
			Transform parent = this.mTrans.parent;
			if (parent != null)
			{
				this.mAbsolute = Quaternion.Slerp(this.mAbsolute, parent.rotation * this.mRelative, delta * this.speed);
				this.mTrans.rotation = this.mAbsolute;
			}
		}
	}

	// Token: 0x06002621 RID: 9761 RVA: 0x0011B345 File Offset: 0x00119745
	private void Start()
	{
		this.mTrans = base.transform;
		this.mRelative = this.mTrans.localRotation;
		this.mAbsolute = this.mTrans.rotation;
	}

	// Token: 0x06002622 RID: 9762 RVA: 0x0011B375 File Offset: 0x00119775
	private void Update()
	{
		this.Interpolate((!this.ignoreTimeScale) ? Time.deltaTime : RealTime.deltaTime);
	}

	// Token: 0x040026DA RID: 9946
	public float speed = 10f;

	// Token: 0x040026DB RID: 9947
	public bool ignoreTimeScale;

	// Token: 0x040026DC RID: 9948
	private Transform mTrans;

	// Token: 0x040026DD RID: 9949
	private Quaternion mRelative;

	// Token: 0x040026DE RID: 9950
	private Quaternion mAbsolute;
}
