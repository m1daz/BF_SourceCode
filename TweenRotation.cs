using System;
using UnityEngine;

// Token: 0x020005F1 RID: 1521
[AddComponentMenu("NGUI/Tween/Tween Rotation")]
public class TweenRotation : UITweener
{
	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x06002B68 RID: 11112 RVA: 0x00140AAB File Offset: 0x0013EEAB
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x06002B69 RID: 11113 RVA: 0x00140AD0 File Offset: 0x0013EED0
	// (set) Token: 0x06002B6A RID: 11114 RVA: 0x00140AD8 File Offset: 0x0013EED8
	[Obsolete("Use 'value' instead")]
	public Quaternion rotation
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x06002B6B RID: 11115 RVA: 0x00140AE1 File Offset: 0x0013EEE1
	// (set) Token: 0x06002B6C RID: 11116 RVA: 0x00140AEE File Offset: 0x0013EEEE
	public Quaternion value
	{
		get
		{
			return this.cachedTransform.localRotation;
		}
		set
		{
			this.cachedTransform.localRotation = value;
		}
	}

	// Token: 0x06002B6D RID: 11117 RVA: 0x00140AFC File Offset: 0x0013EEFC
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = ((!this.quaternionLerp) ? Quaternion.Euler(new Vector3(Mathf.Lerp(this.from.x, this.to.x, factor), Mathf.Lerp(this.from.y, this.to.y, factor), Mathf.Lerp(this.from.z, this.to.z, factor))) : Quaternion.Slerp(Quaternion.Euler(this.from), Quaternion.Euler(this.to), factor));
	}

	// Token: 0x06002B6E RID: 11118 RVA: 0x00140B9C File Offset: 0x0013EF9C
	public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		TweenRotation tweenRotation = UITweener.Begin<TweenRotation>(go, duration, 0f);
		tweenRotation.from = tweenRotation.value.eulerAngles;
		tweenRotation.to = rot.eulerAngles;
		if (duration <= 0f)
		{
			tweenRotation.Sample(1f, true);
			tweenRotation.enabled = false;
		}
		return tweenRotation;
	}

	// Token: 0x06002B6F RID: 11119 RVA: 0x00140BF8 File Offset: 0x0013EFF8
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value.eulerAngles;
	}

	// Token: 0x06002B70 RID: 11120 RVA: 0x00140C1C File Offset: 0x0013F01C
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value.eulerAngles;
	}

	// Token: 0x06002B71 RID: 11121 RVA: 0x00140C3D File Offset: 0x0013F03D
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = Quaternion.Euler(this.from);
	}

	// Token: 0x06002B72 RID: 11122 RVA: 0x00140C50 File Offset: 0x0013F050
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = Quaternion.Euler(this.to);
	}

	// Token: 0x04002AFA RID: 11002
	public Vector3 from;

	// Token: 0x04002AFB RID: 11003
	public Vector3 to;

	// Token: 0x04002AFC RID: 11004
	public bool quaternionLerp;

	// Token: 0x04002AFD RID: 11005
	private Transform mTrans;
}
