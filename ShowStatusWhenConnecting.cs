using System;
using UnityEngine;

// Token: 0x02000155 RID: 341
public class ShowStatusWhenConnecting : MonoBehaviour
{
	// Token: 0x060009E9 RID: 2537 RVA: 0x0004A050 File Offset: 0x00048450
	private void OnGUI()
	{
		if (this.Skin != null)
		{
			GUI.skin = this.Skin;
		}
		float num = 400f;
		float num2 = 100f;
		Rect screenRect = new Rect(((float)Screen.width - num) / 2f, ((float)Screen.height - num2) / 2f, num, num2);
		GUILayout.BeginArea(screenRect, GUI.skin.box);
		GUILayout.Label("Connecting" + this.GetConnectingDots(), GUI.skin.customStyles[0], new GUILayoutOption[0]);
		GUILayout.Label("Status: " + PhotonNetwork.connectionStateDetailed, new GUILayoutOption[0]);
		GUILayout.EndArea();
		if (PhotonNetwork.inRoom)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0004A118 File Offset: 0x00048518
	private string GetConnectingDots()
	{
		string text = string.Empty;
		int num = Mathf.FloorToInt(Time.timeSinceLevelLoad * 3f % 4f);
		for (int i = 0; i < num; i++)
		{
			text += " .";
		}
		return text;
	}

	// Token: 0x040008BC RID: 2236
	public GUISkin Skin;
}
