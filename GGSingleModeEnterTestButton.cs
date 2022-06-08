using System;
using UnityEngine;

// Token: 0x02000270 RID: 624
public class GGSingleModeEnterTestButton : MonoBehaviour
{
	// Token: 0x060011BE RID: 4542 RVA: 0x000A1EBB File Offset: 0x000A02BB
	private void Start()
	{
	}

	// Token: 0x060011BF RID: 4543 RVA: 0x000A1EBD File Offset: 0x000A02BD
	private void Update()
	{
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x000A1EC0 File Offset: 0x000A02C0
	private void OnGUI()
	{
		if (GUI.Button(new Rect((float)Screen.width * 0.2f, (float)Screen.height * 0.2f, 100f, 100f), "Single1"))
		{
			Application.LoadLevel("SingleMode_1");
		}
		if (GUI.Button(new Rect((float)Screen.width * 0.4f, (float)Screen.height * 0.2f, 100f, 100f), "Single2"))
		{
			Application.LoadLevel("SingleMode_2");
		}
		if (GUI.Button(new Rect((float)Screen.width * 0.6f, (float)Screen.height * 0.2f, 100f, 100f), "Single3"))
		{
			Application.LoadLevel("SingleMode_3");
		}
		if (GUI.Button(new Rect((float)Screen.width * 0.8f, (float)Screen.height * 0.2f, 100f, 100f), "Single4"))
		{
			Application.LoadLevel("SingleMode_4");
		}
		if (GUI.Button(new Rect((float)Screen.width * 0.2f, (float)Screen.height * 0.4f, 100f, 100f), "diff1"))
		{
			PlayerPrefs.SetInt("SingleModeChapterOneDifficulty", 1);
		}
		if (GUI.Button(new Rect((float)Screen.width * 0.2f, (float)Screen.height * 0.6f, 100f, 100f), "diff2"))
		{
			PlayerPrefs.SetInt("SingleModeChapterOneDifficulty", 2);
		}
		if (GUI.Button(new Rect((float)Screen.width * 0.2f, (float)Screen.height * 0.8f, 100f, 100f), "diff3"))
		{
			PlayerPrefs.SetInt("SingleModeChapterOneDifficulty", 3);
		}
	}
}
