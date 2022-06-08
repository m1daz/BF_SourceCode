using System;
using UnityEngine;

// Token: 0x020005CE RID: 1486
[AddComponentMenu("NGUI/Internal/Event Listener")]
public class UIEventListener : MonoBehaviour
{
	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06002A37 RID: 10807 RVA: 0x0013E340 File Offset: 0x0013C740
	private bool isColliderEnabled
	{
		get
		{
			if (!this.needsActiveCollider)
			{
				return true;
			}
			Collider component = base.GetComponent<Collider>();
			if (component != null)
			{
				return component.enabled;
			}
			Collider2D component2 = base.GetComponent<Collider2D>();
			return component2 != null && component2.enabled;
		}
	}

	// Token: 0x06002A38 RID: 10808 RVA: 0x0013E390 File Offset: 0x0013C790
	private void OnSubmit()
	{
		if (this.isColliderEnabled && this.onSubmit != null)
		{
			this.onSubmit(base.gameObject);
		}
	}

	// Token: 0x06002A39 RID: 10809 RVA: 0x0013E3B9 File Offset: 0x0013C7B9
	private void OnClick()
	{
		if (this.isColliderEnabled && this.onClick != null)
		{
			this.onClick(base.gameObject);
		}
	}

	// Token: 0x06002A3A RID: 10810 RVA: 0x0013E3E2 File Offset: 0x0013C7E2
	private void OnDoubleClick()
	{
		if (this.isColliderEnabled && this.onDoubleClick != null)
		{
			this.onDoubleClick(base.gameObject);
		}
	}

	// Token: 0x06002A3B RID: 10811 RVA: 0x0013E40B File Offset: 0x0013C80B
	private void OnHover(bool isOver)
	{
		if (this.isColliderEnabled && this.onHover != null)
		{
			this.onHover(base.gameObject, isOver);
		}
	}

	// Token: 0x06002A3C RID: 10812 RVA: 0x0013E435 File Offset: 0x0013C835
	private void OnPress(bool isPressed)
	{
		if (this.isColliderEnabled && this.onPress != null)
		{
			this.onPress(base.gameObject, isPressed);
		}
	}

	// Token: 0x06002A3D RID: 10813 RVA: 0x0013E45F File Offset: 0x0013C85F
	private void OnSelect(bool selected)
	{
		if (this.isColliderEnabled && this.onSelect != null)
		{
			this.onSelect(base.gameObject, selected);
		}
	}

	// Token: 0x06002A3E RID: 10814 RVA: 0x0013E489 File Offset: 0x0013C889
	private void OnScroll(float delta)
	{
		if (this.isColliderEnabled && this.onScroll != null)
		{
			this.onScroll(base.gameObject, delta);
		}
	}

	// Token: 0x06002A3F RID: 10815 RVA: 0x0013E4B3 File Offset: 0x0013C8B3
	private void OnDragStart()
	{
		if (this.onDragStart != null)
		{
			this.onDragStart(base.gameObject);
		}
	}

	// Token: 0x06002A40 RID: 10816 RVA: 0x0013E4D1 File Offset: 0x0013C8D1
	private void OnDrag(Vector2 delta)
	{
		if (this.onDrag != null)
		{
			this.onDrag(base.gameObject, delta);
		}
	}

	// Token: 0x06002A41 RID: 10817 RVA: 0x0013E4F0 File Offset: 0x0013C8F0
	private void OnDragOver()
	{
		if (this.isColliderEnabled && this.onDragOver != null)
		{
			this.onDragOver(base.gameObject);
		}
	}

	// Token: 0x06002A42 RID: 10818 RVA: 0x0013E519 File Offset: 0x0013C919
	private void OnDragOut()
	{
		if (this.isColliderEnabled && this.onDragOut != null)
		{
			this.onDragOut(base.gameObject);
		}
	}

	// Token: 0x06002A43 RID: 10819 RVA: 0x0013E542 File Offset: 0x0013C942
	private void OnDragEnd()
	{
		if (this.onDragEnd != null)
		{
			this.onDragEnd(base.gameObject);
		}
	}

	// Token: 0x06002A44 RID: 10820 RVA: 0x0013E560 File Offset: 0x0013C960
	private void OnDrop(GameObject go)
	{
		if (this.isColliderEnabled && this.onDrop != null)
		{
			this.onDrop(base.gameObject, go);
		}
	}

	// Token: 0x06002A45 RID: 10821 RVA: 0x0013E58A File Offset: 0x0013C98A
	private void OnKey(KeyCode key)
	{
		if (this.isColliderEnabled && this.onKey != null)
		{
			this.onKey(base.gameObject, key);
		}
	}

	// Token: 0x06002A46 RID: 10822 RVA: 0x0013E5B4 File Offset: 0x0013C9B4
	private void OnTooltip(bool show)
	{
		if (this.isColliderEnabled && this.onTooltip != null)
		{
			this.onTooltip(base.gameObject, show);
		}
	}

	// Token: 0x06002A47 RID: 10823 RVA: 0x0013E5E0 File Offset: 0x0013C9E0
	public void Clear()
	{
		this.onSubmit = null;
		this.onClick = null;
		this.onDoubleClick = null;
		this.onHover = null;
		this.onPress = null;
		this.onSelect = null;
		this.onScroll = null;
		this.onDragStart = null;
		this.onDrag = null;
		this.onDragOver = null;
		this.onDragOut = null;
		this.onDragEnd = null;
		this.onDrop = null;
		this.onKey = null;
		this.onTooltip = null;
	}

	// Token: 0x06002A48 RID: 10824 RVA: 0x0013E658 File Offset: 0x0013CA58
	public static UIEventListener Get(GameObject go)
	{
		UIEventListener uieventListener = go.GetComponent<UIEventListener>();
		if (uieventListener == null)
		{
			uieventListener = go.AddComponent<UIEventListener>();
		}
		return uieventListener;
	}

	// Token: 0x04002A44 RID: 10820
	public object parameter;

	// Token: 0x04002A45 RID: 10821
	public UIEventListener.VoidDelegate onSubmit;

	// Token: 0x04002A46 RID: 10822
	public UIEventListener.VoidDelegate onClick;

	// Token: 0x04002A47 RID: 10823
	public UIEventListener.VoidDelegate onDoubleClick;

	// Token: 0x04002A48 RID: 10824
	public UIEventListener.BoolDelegate onHover;

	// Token: 0x04002A49 RID: 10825
	public UIEventListener.BoolDelegate onPress;

	// Token: 0x04002A4A RID: 10826
	public UIEventListener.BoolDelegate onSelect;

	// Token: 0x04002A4B RID: 10827
	public UIEventListener.FloatDelegate onScroll;

	// Token: 0x04002A4C RID: 10828
	public UIEventListener.VoidDelegate onDragStart;

	// Token: 0x04002A4D RID: 10829
	public UIEventListener.VectorDelegate onDrag;

	// Token: 0x04002A4E RID: 10830
	public UIEventListener.VoidDelegate onDragOver;

	// Token: 0x04002A4F RID: 10831
	public UIEventListener.VoidDelegate onDragOut;

	// Token: 0x04002A50 RID: 10832
	public UIEventListener.VoidDelegate onDragEnd;

	// Token: 0x04002A51 RID: 10833
	public UIEventListener.ObjectDelegate onDrop;

	// Token: 0x04002A52 RID: 10834
	public UIEventListener.KeyCodeDelegate onKey;

	// Token: 0x04002A53 RID: 10835
	public UIEventListener.BoolDelegate onTooltip;

	// Token: 0x04002A54 RID: 10836
	public bool needsActiveCollider = true;

	// Token: 0x020005CF RID: 1487
	// (Invoke) Token: 0x06002A4A RID: 10826
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x020005D0 RID: 1488
	// (Invoke) Token: 0x06002A4E RID: 10830
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x020005D1 RID: 1489
	// (Invoke) Token: 0x06002A52 RID: 10834
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x020005D2 RID: 1490
	// (Invoke) Token: 0x06002A56 RID: 10838
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x020005D3 RID: 1491
	// (Invoke) Token: 0x06002A5A RID: 10842
	public delegate void ObjectDelegate(GameObject go, GameObject obj);

	// Token: 0x020005D4 RID: 1492
	// (Invoke) Token: 0x06002A5E RID: 10846
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);
}
