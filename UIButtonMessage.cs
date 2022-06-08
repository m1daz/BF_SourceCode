using System;
using UnityEngine;

// Token: 0x02000561 RID: 1377
[AddComponentMenu("NGUI/Interaction/Button Message (Legacy)")]
public class UIButtonMessage : MonoBehaviour
{
	// Token: 0x0600267C RID: 9852 RVA: 0x0011D982 File Offset: 0x0011BD82
	private void Start()
	{
		this.mStarted = true;
	}

	// Token: 0x0600267D RID: 9853 RVA: 0x0011D98B File Offset: 0x0011BD8B
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnHover(UICamera.IsHighlighted(base.gameObject));
		}
	}

	// Token: 0x0600267E RID: 9854 RVA: 0x0011D9A9 File Offset: 0x0011BDA9
	private void OnHover(bool isOver)
	{
		if (base.enabled && ((isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOver) || (!isOver && this.trigger == UIButtonMessage.Trigger.OnMouseOut)))
		{
			this.Send();
		}
	}

	// Token: 0x0600267F RID: 9855 RVA: 0x0011D9E0 File Offset: 0x0011BDE0
	private void OnPress(bool isPressed)
	{
		if (base.enabled && ((isPressed && this.trigger == UIButtonMessage.Trigger.OnPress) || (!isPressed && this.trigger == UIButtonMessage.Trigger.OnRelease)))
		{
			this.Send();
		}
	}

	// Token: 0x06002680 RID: 9856 RVA: 0x0011DA17 File Offset: 0x0011BE17
	private void OnSelect(bool isSelected)
	{
		if (base.enabled && (!isSelected || UICamera.currentScheme == UICamera.ControlScheme.Controller))
		{
			this.OnHover(isSelected);
		}
	}

	// Token: 0x06002681 RID: 9857 RVA: 0x0011DA3C File Offset: 0x0011BE3C
	private void OnClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnClick)
		{
			this.Send();
		}
	}

	// Token: 0x06002682 RID: 9858 RVA: 0x0011DA5A File Offset: 0x0011BE5A
	private void OnDoubleClick()
	{
		if (base.enabled && this.trigger == UIButtonMessage.Trigger.OnDoubleClick)
		{
			this.Send();
		}
	}

	// Token: 0x06002683 RID: 9859 RVA: 0x0011DA7C File Offset: 0x0011BE7C
	private void Send()
	{
		if (string.IsNullOrEmpty(this.functionName))
		{
			return;
		}
		if (this.target == null)
		{
			this.target = base.gameObject;
		}
		if (this.includeChildren)
		{
			Transform[] componentsInChildren = this.target.GetComponentsInChildren<Transform>();
			int i = 0;
			int num = componentsInChildren.Length;
			while (i < num)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
				i++;
			}
		}
		else
		{
			this.target.SendMessage(this.functionName, base.gameObject, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x0400273C RID: 10044
	public GameObject target;

	// Token: 0x0400273D RID: 10045
	public string functionName;

	// Token: 0x0400273E RID: 10046
	public UIButtonMessage.Trigger trigger;

	// Token: 0x0400273F RID: 10047
	public bool includeChildren;

	// Token: 0x04002740 RID: 10048
	private bool mStarted;

	// Token: 0x02000562 RID: 1378
	[DoNotObfuscateNGUI]
	public enum Trigger
	{
		// Token: 0x04002742 RID: 10050
		OnClick,
		// Token: 0x04002743 RID: 10051
		OnMouseOver,
		// Token: 0x04002744 RID: 10052
		OnMouseOut,
		// Token: 0x04002745 RID: 10053
		OnPress,
		// Token: 0x04002746 RID: 10054
		OnRelease,
		// Token: 0x04002747 RID: 10055
		OnDoubleClick
	}
}
