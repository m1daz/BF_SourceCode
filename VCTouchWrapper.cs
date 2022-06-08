using System;
using UnityEngine;

// Token: 0x020006C4 RID: 1732
public class VCTouchWrapper
{
	// Token: 0x060032F3 RID: 13043 RVA: 0x00166698 File Offset: 0x00164A98
	public VCTouchWrapper()
	{
		this.visited = false;
		this.position = default(Vector2);
		this.deltaPosition = default(Vector2);
		this.phase = TouchPhase.Canceled;
		this.fingerId = -1;
	}

	// Token: 0x060032F4 RID: 13044 RVA: 0x001666E0 File Offset: 0x00164AE0
	public VCTouchWrapper(Vector2 position)
	{
		this.visited = true;
		this.position = position;
		this.deltaPosition = default(Vector2);
		this.fingerId = 0;
		this.phase = TouchPhase.Began;
	}

	// Token: 0x060032F5 RID: 13045 RVA: 0x0016671E File Offset: 0x00164B1E
	public VCTouchWrapper(Touch touch)
	{
		this.Set(touch);
	}

	// Token: 0x060032F6 RID: 13046 RVA: 0x0016672D File Offset: 0x00164B2D
	public void Reset()
	{
		this.visited = false;
		this.position = Vector2.zero;
		this.deltaPosition = Vector2.zero;
		this.phase = TouchPhase.Ended;
		this.fingerId = -1;
		this.debugTouch = false;
	}

	// Token: 0x1700048A RID: 1162
	// (get) Token: 0x060032F7 RID: 13047 RVA: 0x00166761 File Offset: 0x00164B61
	public bool Active
	{
		get
		{
			return this.phase == TouchPhase.Began || this.phase == TouchPhase.Moved || this.phase == TouchPhase.Stationary;
		}
	}

	// Token: 0x060032F8 RID: 13048 RVA: 0x00166786 File Offset: 0x00164B86
	public void Set(Touch touch)
	{
		this.visited = true;
		this.position = touch.position;
		this.deltaPosition = touch.deltaPosition;
		this.phase = touch.phase;
		this.fingerId = touch.fingerId;
	}

	// Token: 0x04002F51 RID: 12113
	public int fingerId;

	// Token: 0x04002F52 RID: 12114
	public TouchPhase phase;

	// Token: 0x04002F53 RID: 12115
	public Vector2 position;

	// Token: 0x04002F54 RID: 12116
	public Vector2 deltaPosition;

	// Token: 0x04002F55 RID: 12117
	public bool visited;

	// Token: 0x04002F56 RID: 12118
	public bool debugTouch;
}
