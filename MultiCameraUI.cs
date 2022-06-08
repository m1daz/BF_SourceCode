using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000369 RID: 873
public class MultiCameraUI : MonoBehaviour
{
	// Token: 0x06001B42 RID: 6978 RVA: 0x000DB9F4 File Offset: 0x000D9DF4
	public void AddCamera2(bool value)
	{
		this.AddCamera(this.cam2, value);
	}

	// Token: 0x06001B43 RID: 6979 RVA: 0x000DBA03 File Offset: 0x000D9E03
	public void AddCamera3(bool value)
	{
		this.AddCamera(this.cam3, value);
	}

	// Token: 0x06001B44 RID: 6980 RVA: 0x000DBA12 File Offset: 0x000D9E12
	public void AddCamera(Camera cam, bool value)
	{
		if (value)
		{
			EasyTouch.AddCamera(cam, false);
		}
		else
		{
			EasyTouch.RemoveCamera(cam);
		}
	}

	// Token: 0x04001D73 RID: 7539
	public Camera cam2;

	// Token: 0x04001D74 RID: 7540
	public Camera cam3;
}
