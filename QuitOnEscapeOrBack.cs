using System;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class QuitOnEscapeOrBack : MonoBehaviour
{
	// Token: 0x060009E2 RID: 2530 RVA: 0x00049DCB File Offset: 0x000481CB
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
