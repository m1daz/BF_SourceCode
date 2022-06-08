using System;
using UnityEngine;

// Token: 0x02000139 RID: 313
public class InputToEvent : MonoBehaviour
{
	// Token: 0x17000122 RID: 290
	// (get) Token: 0x0600096D RID: 2413 RVA: 0x00047DE4 File Offset: 0x000461E4
	// (set) Token: 0x0600096E RID: 2414 RVA: 0x00047DEB File Offset: 0x000461EB
	public static GameObject goPointedAt { get; private set; }

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x0600096F RID: 2415 RVA: 0x00047DF3 File Offset: 0x000461F3
	public Vector2 DragVector
	{
		get
		{
			return (!this.Dragging) ? Vector2.zero : (this.currentPos - this.pressedPosition);
		}
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x00047E1B File Offset: 0x0004621B
	private void Start()
	{
		this.m_Camera = base.GetComponent<Camera>();
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00047E2C File Offset: 0x0004622C
	private void Update()
	{
		if (this.DetectPointedAtGameObject)
		{
			InputToEvent.goPointedAt = this.RaycastObject(Input.mousePosition);
		}
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			this.currentPos = touch.position;
			if (touch.phase == TouchPhase.Began)
			{
				this.Press(touch.position);
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				this.Release(touch.position);
			}
			return;
		}
		this.currentPos = Input.mousePosition;
		if (Input.GetMouseButtonDown(0))
		{
			this.Press(Input.mousePosition);
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.Release(Input.mousePosition);
		}
		if (Input.GetMouseButtonDown(1))
		{
			this.pressedPosition = Input.mousePosition;
			this.lastGo = this.RaycastObject(this.pressedPosition);
			if (this.lastGo != null)
			{
				this.lastGo.SendMessage("OnPressRight", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x00047F46 File Offset: 0x00046346
	private void Press(Vector2 screenPos)
	{
		this.pressedPosition = screenPos;
		this.Dragging = true;
		this.lastGo = this.RaycastObject(screenPos);
		if (this.lastGo != null)
		{
			this.lastGo.SendMessage("OnPress", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00047F88 File Offset: 0x00046388
	private void Release(Vector2 screenPos)
	{
		if (this.lastGo != null)
		{
			GameObject x = this.RaycastObject(screenPos);
			if (x == this.lastGo)
			{
				this.lastGo.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
			}
			this.lastGo.SendMessage("OnRelease", SendMessageOptions.DontRequireReceiver);
			this.lastGo = null;
		}
		this.pressedPosition = Vector2.zero;
		this.Dragging = false;
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00047FFC File Offset: 0x000463FC
	private GameObject RaycastObject(Vector2 screenPos)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(this.m_Camera.ScreenPointToRay(screenPos), out raycastHit, 200f))
		{
			InputToEvent.inputHitPos = raycastHit.point;
			return raycastHit.collider.gameObject;
		}
		return null;
	}

	// Token: 0x0400086E RID: 2158
	private GameObject lastGo;

	// Token: 0x0400086F RID: 2159
	public static Vector3 inputHitPos;

	// Token: 0x04000870 RID: 2160
	public bool DetectPointedAtGameObject;

	// Token: 0x04000872 RID: 2162
	private Vector2 pressedPosition = Vector2.zero;

	// Token: 0x04000873 RID: 2163
	private Vector2 currentPos = Vector2.zero;

	// Token: 0x04000874 RID: 2164
	public bool Dragging;

	// Token: 0x04000875 RID: 2165
	private Camera m_Camera;
}
