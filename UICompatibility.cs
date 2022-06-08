using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000383 RID: 899
public class UICompatibility : MonoBehaviour
{
	// Token: 0x06001BF9 RID: 7161 RVA: 0x000DDA7E File Offset: 0x000DBE7E
	public void SetCompatibility(bool value)
	{
		EasyTouch.SetUICompatibily(value);
	}
}
