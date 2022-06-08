using System;
using UnityEngine;

// Token: 0x02000153 RID: 339
public class ServerTime : MonoBehaviour
{
	// Token: 0x060009E4 RID: 2532 RVA: 0x00049DE8 File Offset: 0x000481E8
	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect((float)(Screen.width / 2 - 100), 0f, 200f, 30f));
		GUILayout.Label(string.Format("Time Offset: {0}", PhotonNetwork.ServerTimestamp - Environment.TickCount), new GUILayoutOption[0]);
		if (GUILayout.Button("fetch", new GUILayoutOption[0]))
		{
			PhotonNetwork.FetchServerTimestamp();
		}
		GUILayout.EndArea();
	}
}
