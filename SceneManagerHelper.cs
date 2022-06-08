using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
public class SceneManagerHelper
{
	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x060007F5 RID: 2037 RVA: 0x000413A9 File Offset: 0x0003F7A9
	public static string ActiveSceneName
	{
		get
		{
			return Application.loadedLevelName;
		}
	}

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000413B0 File Offset: 0x0003F7B0
	public static int ActiveSceneBuildIndex
	{
		get
		{
			return Application.loadedLevel;
		}
	}
}
