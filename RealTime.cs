using System;
using UnityEngine;

// Token: 0x020005C1 RID: 1473
public class RealTime : MonoBehaviour
{
	// Token: 0x1700024D RID: 589
	// (get) Token: 0x060029D8 RID: 10712 RVA: 0x00136C5D File Offset: 0x0013505D
	public static float time
	{
		get
		{
			return Time.unscaledTime;
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x060029D9 RID: 10713 RVA: 0x00136C64 File Offset: 0x00135064
	public static float deltaTime
	{
		get
		{
			return Time.unscaledDeltaTime;
		}
	}
}
