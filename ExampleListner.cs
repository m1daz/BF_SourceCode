using System;
using UnionAssets.FLE;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class ExampleListner : MonoBehaviour
{
	// Token: 0x0600069D RID: 1693 RVA: 0x00038E8B File Offset: 0x0003728B
	private void Start()
	{
		EventButtonExample.instance.addEventListener("click", new EventHandlerFunction(this.onButtonClick));
		EventButtonExample.instance.addEventListener("click", new DataEventHandlerFunction(this.onButtonClickData));
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x00038EC3 File Offset: 0x000372C3
	private void onButtonClick()
	{
		this.count++;
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x00038ED4 File Offset: 0x000372D4
	private void onButtonClickData(CEvent e)
	{
		Debug.Log("================================");
		Debug.Log("onButtonClickData");
		Debug.Log("dispatcher: " + e.dispatcher.ToString());
		Debug.Log("event data: " + e.data.ToString());
		Debug.Log("event name: " + e.name.ToString());
		Debug.Log("================================");
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x00038F50 File Offset: 0x00037350
	private void OnGUI()
	{
		GUI.Label(new Rect(0f, 0f, 200f, 200f), this.label + this.count.ToString(), this.style);
	}

	// Token: 0x04000597 RID: 1431
	public GUIStyle style;

	// Token: 0x04000598 RID: 1432
	private string label = "Click's: ";

	// Token: 0x04000599 RID: 1433
	private int count;
}
