using System;
using UnityEngine;

// Token: 0x02000465 RID: 1125
public class SwooshTest : MonoBehaviour
{
	// Token: 0x06002095 RID: 8341 RVA: 0x000F3334 File Offset: 0x000F1734
	private void Start()
	{
		float num = this._animation.frameRate * this._animation.length;
		this._startN = (float)this._start / num;
		this._endN = (float)this._end / num;
		this._animationState = base.GetComponent<Animation>()[this._animation.name];
		this._trail.Emit = false;
	}

	// Token: 0x06002096 RID: 8342 RVA: 0x000F33A0 File Offset: 0x000F17A0
	private void Update()
	{
		this._time += this._animationState.normalizedTime - this._prevAnimTime;
		if (this._time > 1f || this._firstFrame)
		{
			if (!this._firstFrame)
			{
				this._time -= 1f;
			}
			this._firstFrame = false;
		}
		if (this._prevTime < this._startN && this._time >= this._startN)
		{
			this._trail.Emit = true;
		}
		else if (this._prevTime < this._endN && this._time >= this._endN)
		{
			this._trail.Emit = false;
		}
		this._prevTime = this._time;
		this._prevAnimTime = this._animationState.normalizedTime;
	}

	// Token: 0x04002157 RID: 8535
	[SerializeField]
	private AnimationClip _animation;

	// Token: 0x04002158 RID: 8536
	private AnimationState _animationState;

	// Token: 0x04002159 RID: 8537
	[SerializeField]
	private int _start;

	// Token: 0x0400215A RID: 8538
	[SerializeField]
	private int _end;

	// Token: 0x0400215B RID: 8539
	private float _startN;

	// Token: 0x0400215C RID: 8540
	private float _endN;

	// Token: 0x0400215D RID: 8541
	private float _time;

	// Token: 0x0400215E RID: 8542
	private float _prevTime;

	// Token: 0x0400215F RID: 8543
	private float _prevAnimTime;

	// Token: 0x04002160 RID: 8544
	[SerializeField]
	private MeleeWeaponTrail _trail;

	// Token: 0x04002161 RID: 8545
	private bool _firstFrame = true;
}
