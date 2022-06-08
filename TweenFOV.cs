using System;
using UnityEngine;

// Token: 0x020005E9 RID: 1513
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Field of View")]
public class TweenFOV : UITweener
{
	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06002B2B RID: 11051 RVA: 0x0013FE6E File Offset: 0x0013E26E
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.GetComponent<Camera>();
			}
			return this.mCam;
		}
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06002B2C RID: 11052 RVA: 0x0013FE93 File Offset: 0x0013E293
	// (set) Token: 0x06002B2D RID: 11053 RVA: 0x0013FE9B File Offset: 0x0013E29B
	[Obsolete("Use 'value' instead")]
	public float fov
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

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06002B2E RID: 11054 RVA: 0x0013FEA4 File Offset: 0x0013E2A4
	// (set) Token: 0x06002B2F RID: 11055 RVA: 0x0013FEB1 File Offset: 0x0013E2B1
	public float value
	{
		get
		{
			return this.cachedCamera.fieldOfView;
		}
		set
		{
			this.cachedCamera.fieldOfView = value;
		}
	}

	// Token: 0x06002B30 RID: 11056 RVA: 0x0013FEBF File Offset: 0x0013E2BF
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06002B31 RID: 11057 RVA: 0x0013FEE0 File Offset: 0x0013E2E0
	public static TweenFOV Begin(GameObject go, float duration, float to)
	{
		TweenFOV tweenFOV = UITweener.Begin<TweenFOV>(go, duration, 0f);
		tweenFOV.from = tweenFOV.value;
		tweenFOV.to = to;
		if (duration <= 0f)
		{
			tweenFOV.Sample(1f, true);
			tweenFOV.enabled = false;
		}
		return tweenFOV;
	}

	// Token: 0x06002B32 RID: 11058 RVA: 0x0013FF2C File Offset: 0x0013E32C
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06002B33 RID: 11059 RVA: 0x0013FF3A File Offset: 0x0013E33A
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x06002B34 RID: 11060 RVA: 0x0013FF48 File Offset: 0x0013E348
	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = this.from;
	}

	// Token: 0x06002B35 RID: 11061 RVA: 0x0013FF56 File Offset: 0x0013E356
	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = this.to;
	}

	// Token: 0x04002AD3 RID: 10963
	public float from = 45f;

	// Token: 0x04002AD4 RID: 10964
	public float to = 45f;

	// Token: 0x04002AD5 RID: 10965
	private Camera mCam;
}
