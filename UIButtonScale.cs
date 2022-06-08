using System;
using UnityEngine;

// Token: 0x02000565 RID: 1381
[AddComponentMenu("NGUI/Interaction/Button Scale")]
public class UIButtonScale : MonoBehaviour
{
	// Token: 0x06002695 RID: 9877 RVA: 0x0011E004 File Offset: 0x0011C404
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mScale = this.tweenTarget.localScale;
		}
	}

	// Token: 0x06002696 RID: 9878 RVA: 0x0011E051 File Offset: 0x0011C451
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06002697 RID: 9879 RVA: 0x0011E070 File Offset: 0x0011C470
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenScale component = this.tweenTarget.GetComponent<TweenScale>();
			if (component != null)
			{
				component.value = this.mScale;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06002698 RID: 9880 RVA: 0x0011E0C4 File Offset: 0x0011C4C4
	private void OnPress(bool isPressed)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mScale : Vector3.Scale(this.mScale, this.hover)) : Vector3.Scale(this.mScale, this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06002699 RID: 9881 RVA: 0x0011E154 File Offset: 0x0011C554
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenScale.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mScale : Vector3.Scale(this.mScale, this.hover)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600269A RID: 9882 RVA: 0x0011E1BB File Offset: 0x0011C5BB
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x04002755 RID: 10069
	public Transform tweenTarget;

	// Token: 0x04002756 RID: 10070
	public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);

	// Token: 0x04002757 RID: 10071
	public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);

	// Token: 0x04002758 RID: 10072
	public float duration = 0.2f;

	// Token: 0x04002759 RID: 10073
	private Vector3 mScale;

	// Token: 0x0400275A RID: 10074
	private bool mStarted;
}
