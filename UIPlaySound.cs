using System;
using UnityEngine;

// Token: 0x02000580 RID: 1408
[AddComponentMenu("NGUI/Interaction/Play Sound")]
public class UIPlaySound : MonoBehaviour
{
	// Token: 0x170001FE RID: 510
	// (get) Token: 0x0600275D RID: 10077 RVA: 0x00121AEC File Offset: 0x0011FEEC
	private bool canPlay
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			UIButton component = base.GetComponent<UIButton>();
			return component == null || component.isEnabled;
		}
	}

	// Token: 0x0600275E RID: 10078 RVA: 0x00121B22 File Offset: 0x0011FF22
	private void OnEnable()
	{
		if (this.trigger == UIPlaySound.Trigger.OnEnable)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x00121B48 File Offset: 0x0011FF48
	private void OnDisable()
	{
		if (this.trigger == UIPlaySound.Trigger.OnDisable)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06002760 RID: 10080 RVA: 0x00121B70 File Offset: 0x0011FF70
	private void OnHover(bool isOver)
	{
		if (this.trigger == UIPlaySound.Trigger.OnMouseOver)
		{
			if (this.mIsOver == isOver)
			{
				return;
			}
			this.mIsOver = isOver;
		}
		if (this.canPlay && ((isOver && this.trigger == UIPlaySound.Trigger.OnMouseOver) || (!isOver && this.trigger == UIPlaySound.Trigger.OnMouseOut)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06002761 RID: 10081 RVA: 0x00121BE4 File Offset: 0x0011FFE4
	private void OnPress(bool isPressed)
	{
		if (this.trigger == UIPlaySound.Trigger.OnPress)
		{
			if (this.mIsOver == isPressed)
			{
				return;
			}
			this.mIsOver = isPressed;
		}
		if (this.canPlay && ((isPressed && this.trigger == UIPlaySound.Trigger.OnPress) || (!isPressed && this.trigger == UIPlaySound.Trigger.OnRelease)))
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06002762 RID: 10082 RVA: 0x00121C58 File Offset: 0x00120058
	private void OnClick()
	{
		if (this.canPlay && this.trigger == UIPlaySound.Trigger.OnClick)
		{
			NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
		}
	}

	// Token: 0x06002763 RID: 10083 RVA: 0x00121C88 File Offset: 0x00120088
	private void OnSelect(bool isSelected)
	{
		if (this.canPlay && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x06002764 RID: 10084 RVA: 0x00121CAD File Offset: 0x001200AD
	public void Play()
	{
		NGUITools.PlaySound(this.audioClip, this.volume, this.pitch);
	}

	// Token: 0x0400281E RID: 10270
	public AudioClip audioClip;

	// Token: 0x0400281F RID: 10271
	public UIPlaySound.Trigger trigger;

	// Token: 0x04002820 RID: 10272
	[Range(0f, 1f)]
	public float volume = 1f;

	// Token: 0x04002821 RID: 10273
	[Range(0f, 2f)]
	public float pitch = 1f;

	// Token: 0x04002822 RID: 10274
	private bool mIsOver;

	// Token: 0x02000581 RID: 1409
	[DoNotObfuscateNGUI]
	public enum Trigger
	{
		// Token: 0x04002824 RID: 10276
		OnClick,
		// Token: 0x04002825 RID: 10277
		OnMouseOver,
		// Token: 0x04002826 RID: 10278
		OnMouseOut,
		// Token: 0x04002827 RID: 10279
		OnPress,
		// Token: 0x04002828 RID: 10280
		OnRelease,
		// Token: 0x04002829 RID: 10281
		Custom,
		// Token: 0x0400282A RID: 10282
		OnEnable,
		// Token: 0x0400282B RID: 10283
		OnDisable
	}
}
