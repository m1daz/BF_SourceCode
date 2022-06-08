using System;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public static class GameObjectExtensions
{
	// Token: 0x060006BC RID: 1724 RVA: 0x0003993D File Offset: 0x00037D3D
	public static bool GetActive(this GameObject target)
	{
		return target.activeInHierarchy;
	}
}
