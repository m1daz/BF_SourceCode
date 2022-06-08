using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000573 RID: 1395
[AddComponentMenu("NGUI/Interaction/Event Trigger")]
public class UIEventTrigger : MonoBehaviour
{
	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x060026F0 RID: 9968 RVA: 0x0012000C File Offset: 0x0011E40C
	public bool isColliderEnabled
	{
		get
		{
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x060026F1 RID: 9969 RVA: 0x00120050 File Offset: 0x0011E450
	private void OnHover(bool isOver)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (isOver)
		{
			EventDelegate.Execute(this.onHoverOver);
		}
		else
		{
			EventDelegate.Execute(this.onHoverOut);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x060026F2 RID: 9970 RVA: 0x001200A8 File Offset: 0x0011E4A8
	private void OnPress(bool pressed)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (pressed)
		{
			EventDelegate.Execute(this.onPress);
		}
		else
		{
			EventDelegate.Execute(this.onRelease);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x060026F3 RID: 9971 RVA: 0x00120100 File Offset: 0x0011E500
	private void OnSelect(bool selected)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		if (selected)
		{
			EventDelegate.Execute(this.onSelect);
		}
		else
		{
			EventDelegate.Execute(this.onDeselect);
		}
		UIEventTrigger.current = null;
	}

	// Token: 0x060026F4 RID: 9972 RVA: 0x00120156 File Offset: 0x0011E556
	private void OnClick()
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x060026F5 RID: 9973 RVA: 0x0012018B File Offset: 0x0011E58B
	private void OnDoubleClick()
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDoubleClick);
		UIEventTrigger.current = null;
	}

	// Token: 0x060026F6 RID: 9974 RVA: 0x001201C0 File Offset: 0x0011E5C0
	private void OnDragStart()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragStart);
		UIEventTrigger.current = null;
	}

	// Token: 0x060026F7 RID: 9975 RVA: 0x001201EA File Offset: 0x0011E5EA
	private void OnDragEnd()
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragEnd);
		UIEventTrigger.current = null;
	}

	// Token: 0x060026F8 RID: 9976 RVA: 0x00120214 File Offset: 0x0011E614
	private void OnDragOver(GameObject go)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOver);
		UIEventTrigger.current = null;
	}

	// Token: 0x060026F9 RID: 9977 RVA: 0x00120249 File Offset: 0x0011E649
	private void OnDragOut(GameObject go)
	{
		if (UIEventTrigger.current != null || !this.isColliderEnabled)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDragOut);
		UIEventTrigger.current = null;
	}

	// Token: 0x060026FA RID: 9978 RVA: 0x0012027E File Offset: 0x0011E67E
	private void OnDrag(Vector2 delta)
	{
		if (UIEventTrigger.current != null)
		{
			return;
		}
		UIEventTrigger.current = this;
		EventDelegate.Execute(this.onDrag);
		UIEventTrigger.current = null;
	}

	// Token: 0x040027B3 RID: 10163
	public static UIEventTrigger current;

	// Token: 0x040027B4 RID: 10164
	public List<EventDelegate> onHoverOver = new List<EventDelegate>();

	// Token: 0x040027B5 RID: 10165
	public List<EventDelegate> onHoverOut = new List<EventDelegate>();

	// Token: 0x040027B6 RID: 10166
	public List<EventDelegate> onPress = new List<EventDelegate>();

	// Token: 0x040027B7 RID: 10167
	public List<EventDelegate> onRelease = new List<EventDelegate>();

	// Token: 0x040027B8 RID: 10168
	public List<EventDelegate> onSelect = new List<EventDelegate>();

	// Token: 0x040027B9 RID: 10169
	public List<EventDelegate> onDeselect = new List<EventDelegate>();

	// Token: 0x040027BA RID: 10170
	public List<EventDelegate> onClick = new List<EventDelegate>();

	// Token: 0x040027BB RID: 10171
	public List<EventDelegate> onDoubleClick = new List<EventDelegate>();

	// Token: 0x040027BC RID: 10172
	public List<EventDelegate> onDragStart = new List<EventDelegate>();

	// Token: 0x040027BD RID: 10173
	public List<EventDelegate> onDragEnd = new List<EventDelegate>();

	// Token: 0x040027BE RID: 10174
	public List<EventDelegate> onDragOver = new List<EventDelegate>();

	// Token: 0x040027BF RID: 10175
	public List<EventDelegate> onDragOut = new List<EventDelegate>();

	// Token: 0x040027C0 RID: 10176
	public List<EventDelegate> onDrag = new List<EventDelegate>();
}
