using System;
using UnityEngine;

// Token: 0x020003F4 RID: 1012
public static class ComponentExtensions
{
	// Token: 0x06001E5D RID: 7773 RVA: 0x000E8275 File Offset: 0x000E6675
	public static RectTransform rectTransform(this Component cp)
	{
		return cp.transform as RectTransform;
	}

	// Token: 0x06001E5E RID: 7774 RVA: 0x000E8282 File Offset: 0x000E6682
	public static float Remap(this float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}
