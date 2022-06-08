using System;
using UnityEngine;

// Token: 0x02000563 RID: 1379
[AddComponentMenu("NGUI/Interaction/Button Offset")]
public class UIButtonOffset : MonoBehaviour
{
	// Token: 0x06002685 RID: 9861 RVA: 0x0011DB50 File Offset: 0x0011BF50
	private void Start()
	{
		if (!this.mStarted)
		{
			this.mStarted = true;
			if (this.tweenTarget == null)
			{
				this.tweenTarget = base.transform;
			}
			this.mPos = this.tweenTarget.localPosition;
		}
	}

	// Token: 0x06002686 RID: 9862 RVA: 0x0011DB9D File Offset: 0x0011BF9D
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x06002687 RID: 9863 RVA: 0x0011DBBC File Offset: 0x0011BFBC
	private void OnDisable()
	{
		if (this.mStarted && this.tweenTarget != null)
		{
			TweenPosition component = this.tweenTarget.GetComponent<TweenPosition>();
			if (component != null)
			{
				component.value = this.mPos;
				component.enabled = false;
			}
		}
	}

	// Token: 0x06002688 RID: 9864 RVA: 0x0011DC10 File Offset: 0x0011C010
	private void OnPress(bool isPressed)
	{
		this.mPressed = isPressed;
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isPressed) ? ((!UICamera.IsHighlighted(base.gameObject)) ? this.mPos : (this.mPos + this.hover)) : (this.mPos + this.pressed)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x06002689 RID: 9865 RVA: 0x0011DCA4 File Offset: 0x0011C0A4
	private void OnHover(bool isOver)
	{
		if (base.enabled)
		{
			if (!this.mStarted)
			{
				this.Start();
			}
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, (!isOver) ? this.mPos : (this.mPos + this.hover)).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600268A RID: 9866 RVA: 0x0011DD0B File Offset: 0x0011C10B
	private void OnDragOver()
	{
		if (this.mPressed)
		{
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, this.mPos + this.hover).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600268B RID: 9867 RVA: 0x0011DD45 File Offset: 0x0011C145
	private void OnDragOut()
	{
		if (this.mPressed)
		{
			TweenPosition.Begin(this.tweenTarget.gameObject, this.duration, this.mPos).method = UITweener.Method.EaseInOut;
		}
	}

	// Token: 0x0600268C RID: 9868 RVA: 0x0011DD74 File Offset: 0x0011C174
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x04002748 RID: 10056
	public Transform tweenTarget;

	// Token: 0x04002749 RID: 10057
	public Vector3 hover = Vector3.zero;

	// Token: 0x0400274A RID: 10058
	public Vector3 pressed = new Vector3(2f, -2f);

	// Token: 0x0400274B RID: 10059
	public float duration = 0.2f;

	// Token: 0x0400274C RID: 10060
	[NonSerialized]
	private Vector3 mPos;

	// Token: 0x0400274D RID: 10061
	[NonSerialized]
	private bool mStarted;

	// Token: 0x0400274E RID: 10062
	[NonSerialized]
	private bool mPressed;
}
