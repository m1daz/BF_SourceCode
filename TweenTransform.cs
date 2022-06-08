using System;
using UnityEngine;

// Token: 0x020005F3 RID: 1523
[AddComponentMenu("NGUI/Tween/Tween Transform")]
public class TweenTransform : UITweener
{
	// Token: 0x06002B80 RID: 11136 RVA: 0x00140DE8 File Offset: 0x0013F1E8
	protected override void OnUpdate(float factor, bool isFinished)
	{
		if (this.to != null)
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
				this.mPos = this.mTrans.position;
				this.mRot = this.mTrans.rotation;
				this.mScale = this.mTrans.localScale;
			}
			if (this.from != null)
			{
				this.mTrans.position = this.from.position * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.from.localScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.from.rotation, this.to.rotation, factor);
			}
			else
			{
				this.mTrans.position = this.mPos * (1f - factor) + this.to.position * factor;
				this.mTrans.localScale = this.mScale * (1f - factor) + this.to.localScale * factor;
				this.mTrans.rotation = Quaternion.Slerp(this.mRot, this.to.rotation, factor);
			}
			if (this.parentWhenFinished && isFinished)
			{
				this.mTrans.parent = this.to;
			}
		}
	}

	// Token: 0x06002B81 RID: 11137 RVA: 0x00140FAD File Offset: 0x0013F3AD
	public static TweenTransform Begin(GameObject go, float duration, Transform to)
	{
		return TweenTransform.Begin(go, duration, null, to);
	}

	// Token: 0x06002B82 RID: 11138 RVA: 0x00140FB8 File Offset: 0x0013F3B8
	public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{
		TweenTransform tweenTransform = UITweener.Begin<TweenTransform>(go, duration, 0f);
		tweenTransform.from = from;
		tweenTransform.to = to;
		if (duration <= 0f)
		{
			tweenTransform.Sample(1f, true);
			tweenTransform.enabled = false;
		}
		return tweenTransform;
	}

	// Token: 0x04002B03 RID: 11011
	public Transform from;

	// Token: 0x04002B04 RID: 11012
	public Transform to;

	// Token: 0x04002B05 RID: 11013
	public bool parentWhenFinished;

	// Token: 0x04002B06 RID: 11014
	private Transform mTrans;

	// Token: 0x04002B07 RID: 11015
	private Vector3 mPos;

	// Token: 0x04002B08 RID: 11016
	private Quaternion mRot;

	// Token: 0x04002B09 RID: 11017
	private Vector3 mScale;
}
