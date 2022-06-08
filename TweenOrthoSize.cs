using System;
using UnityEngine;

// Token: 0x020005EF RID: 1519
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/Tween/Tween Orthographic Size")]
public class TweenOrthoSize : UITweener
{
	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x06002B50 RID: 11088 RVA: 0x001407BF File Offset: 0x0013EBBF
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

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x06002B51 RID: 11089 RVA: 0x001407E4 File Offset: 0x0013EBE4
	// (set) Token: 0x06002B52 RID: 11090 RVA: 0x001407EC File Offset: 0x0013EBEC
	[Obsolete("Use 'value' instead")]
	public float orthoSize
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

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x06002B53 RID: 11091 RVA: 0x001407F5 File Offset: 0x0013EBF5
	// (set) Token: 0x06002B54 RID: 11092 RVA: 0x00140802 File Offset: 0x0013EC02
	public float value
	{
		get
		{
			return this.cachedCamera.orthographicSize;
		}
		set
		{
			this.cachedCamera.orthographicSize = value;
		}
	}

	// Token: 0x06002B55 RID: 11093 RVA: 0x00140810 File Offset: 0x0013EC10
	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06002B56 RID: 11094 RVA: 0x00140830 File Offset: 0x0013EC30
	public static TweenOrthoSize Begin(GameObject go, float duration, float to)
	{
		TweenOrthoSize tweenOrthoSize = UITweener.Begin<TweenOrthoSize>(go, duration, 0f);
		tweenOrthoSize.from = tweenOrthoSize.value;
		tweenOrthoSize.to = to;
		if (duration <= 0f)
		{
			tweenOrthoSize.Sample(1f, true);
			tweenOrthoSize.enabled = false;
		}
		return tweenOrthoSize;
	}

	// Token: 0x06002B57 RID: 11095 RVA: 0x0014087C File Offset: 0x0013EC7C
	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	// Token: 0x06002B58 RID: 11096 RVA: 0x0014088A File Offset: 0x0013EC8A
	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}

	// Token: 0x04002AF2 RID: 10994
	public float from = 1f;

	// Token: 0x04002AF3 RID: 10995
	public float to = 1f;

	// Token: 0x04002AF4 RID: 10996
	private Camera mCam;
}
