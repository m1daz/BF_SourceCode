using System;
using UnionAssets.FLE;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class EventButtonExample : EventDispatcher
{
	// Token: 0x06000699 RID: 1689 RVA: 0x00038E08 File Offset: 0x00037208
	private void Awake()
	{
		EventButtonExample.instance = this;
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x00038E10 File Offset: 0x00037210
	private void OnGUI()
	{
		Rect position = new Rect(((float)Screen.width - this.w) / 2f, ((float)Screen.height - this.h) / 2f, this.w, this.h);
		if (GUI.Button(position, "click me"))
		{
			base.dispatch("click", "hello");
		}
	}

	// Token: 0x04000594 RID: 1428
	public static EventButtonExample instance;

	// Token: 0x04000595 RID: 1429
	public float w = 150f;

	// Token: 0x04000596 RID: 1430
	public float h = 50f;
}
