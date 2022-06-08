using System;
using UnityEngine;

// Token: 0x02000564 RID: 1380
[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
	// Token: 0x0600268E RID: 9870 RVA: 0x0011DDC4 File Offset: 0x0011C1C4
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mRot = this.tweenTarget.localRotation;
		}
	}

	// Token: 0x0600268F RID: 9871 RVA: 0x0011DE11 File Offset: 0x0011C211
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06002690 RID: 9872 RVA: 0x0011DE30 File Offset: 0x0011C230
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenRotation component = this.tweenTarget.GetComponent<TweenRotation>();
			if (component != null)
			{
				component.value = this.mRot;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06002691 RID: 9873 RVA: 0x0011DE84 File Offset: 0x0011C284
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))) : (this.mRot * Quaternion.Euler(this.pressed))).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06002692 RID: 9874 RVA: 0x0011DF1C File Offset: 0x0011C31C
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenRotation.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mRot : (this.mRot * Quaternion.Euler(this.hover))).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06002693 RID: 9875 RVA: 0x0011DF88 File Offset: 0x0011C388
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x0400274F RID: 10063
	public Transform tweenTarget;

	// Token: 0x04002750 RID: 10064
	public Vector3 hover = Vector3.zero;

	// Token: 0x04002751 RID: 10065
	public Vector3 pressed = Vector3.zero;

	// Token: 0x04002752 RID: 10066
	public float duration = 0.2f;

	// Token: 0x04002753 RID: 10067
	private Quaternion mRot;

	// Token: 0x04002754 RID: 10068
	private bool mStarted;
}
