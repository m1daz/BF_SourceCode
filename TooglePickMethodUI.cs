using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000379 RID: 889
public class TooglePickMethodUI : MonoBehaviour
{
	// Token: 0x06001BAA RID: 7082 RVA: 0x000DCCE8 File Offset: 0x000DB0E8
	public void SetPickMethod2Finger(bool value)
	{
		if (value)
		{
			EasyTouch.SetTwoFingerPickMethod(EasyTouch.TwoFingerPickMethod.Finger);
		}
	}

	// Token: 0x06001BAB RID: 7083 RVA: 0x000DCCF6 File Offset: 0x000DB0F6
	public void SetPickMethod2Averager(bool value)
	{
		if (value)
		{
			EasyTouch.SetTwoFingerPickMethod(EasyTouch.TwoFingerPickMethod.Average);
		}
	}
}
