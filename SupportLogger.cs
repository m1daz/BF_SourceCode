using System;
using UnityEngine;

// Token: 0x02000157 RID: 343
public class SupportLogger : MonoBehaviour
{
	// Token: 0x060009F0 RID: 2544 RVA: 0x0004A2B8 File Offset: 0x000486B8
	public void Start()
	{
		GameObject gameObject = GameObject.Find("PunSupportLogger");
		if (gameObject == null)
		{
			gameObject = new GameObject("PunSupportLogger");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			SupportLogging supportLogging = gameObject.AddComponent<SupportLogging>();
			supportLogging.LogTrafficStats = this.LogTrafficStats;
		}
	}

	// Token: 0x040008C0 RID: 2240
	public bool LogTrafficStats = true;
}
