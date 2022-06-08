using System;
using UnityEngine;

// Token: 0x02000574 RID: 1396
[AddComponentMenu("NGUI/Interaction/Forward Events (Legacy)")]
public class UIForwardEvents : MonoBehaviour
{
	// Token: 0x060026FC RID: 9980 RVA: 0x001202B0 File Offset: 0x0011E6B0
	private void OnHover(bool isOver)
	{
		if (this.onHover && this.target != null)
		{
			this.target.SendMessage("OnHover", isOver, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060026FD RID: 9981 RVA: 0x001202E5 File Offset: 0x0011E6E5
	private void OnPress(bool pressed)
	{
		if (this.onPress && this.target != null)
		{
			this.target.SendMessage("OnPress", pressed, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060026FE RID: 9982 RVA: 0x0012031A File Offset: 0x0011E71A
	private void OnClick()
	{
		if (this.onClick && this.target != null)
		{
			this.target.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x060026FF RID: 9983 RVA: 0x00120349 File Offset: 0x0011E749
	private void OnDoubleClick()
	{
		if (this.onDoubleClick && this.target != null)
		{
			this.target.SendMessage("OnDoubleClick", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06002700 RID: 9984 RVA: 0x00120378 File Offset: 0x0011E778
	private void OnSelect(bool selected)
	{
		if (this.onSelect && this.target != null)
		{
			this.target.SendMessage("OnSelect", selected, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06002701 RID: 9985 RVA: 0x001203AD File Offset: 0x0011E7AD
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag && this.target != null)
		{
			this.target.SendMessage("OnDrag", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06002702 RID: 9986 RVA: 0x001203E2 File Offset: 0x0011E7E2
	private void OnDrop(GameObject go)
	{
		if (this.onDrop && this.target != null)
		{
			this.target.SendMessage("OnDrop", go, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06002703 RID: 9987 RVA: 0x00120412 File Offset: 0x0011E812
	private void OnSubmit()
	{
		if (this.onSubmit && this.target != null)
		{
			this.target.SendMessage("OnSubmit", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06002704 RID: 9988 RVA: 0x00120441 File Offset: 0x0011E841
	private void OnScroll(float delta)
	{
		if (this.onScroll && this.target != null)
		{
			this.target.SendMessage("OnScroll", delta, SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x040027C1 RID: 10177
	public GameObject target;

	// Token: 0x040027C2 RID: 10178
	public bool onHover;

	// Token: 0x040027C3 RID: 10179
	public bool onPress;

	// Token: 0x040027C4 RID: 10180
	public bool onClick;

	// Token: 0x040027C5 RID: 10181
	public bool onDoubleClick;

	// Token: 0x040027C6 RID: 10182
	public bool onSelect;

	// Token: 0x040027C7 RID: 10183
	public bool onDrag;

	// Token: 0x040027C8 RID: 10184
	public bool onDrop;

	// Token: 0x040027C9 RID: 10185
	public bool onSubmit;

	// Token: 0x040027CA RID: 10186
	public bool onScroll;
}
