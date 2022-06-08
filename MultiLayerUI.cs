using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000367 RID: 871
public class MultiLayerUI : MonoBehaviour
{
	// Token: 0x06001B37 RID: 6967 RVA: 0x000DB821 File Offset: 0x000D9C21
	public void SetAutoSelect(bool value)
	{
		EasyTouch.SetEnableAutoSelect(value);
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x000DB829 File Offset: 0x000D9C29
	public void SetAutoUpdate(bool value)
	{
		EasyTouch.SetAutoUpdatePickedObject(value);
	}

	// Token: 0x06001B39 RID: 6969 RVA: 0x000DB834 File Offset: 0x000D9C34
	public void Layer1(bool value)
	{
		LayerMask mask = EasyTouch.Get3DPickableLayer();
		if (value)
		{
			mask |= 256;
		}
		else
		{
			mask = ~mask;
			mask = ~(mask | 256);
		}
		EasyTouch.Set3DPickableLayer(mask);
	}

	// Token: 0x06001B3A RID: 6970 RVA: 0x000DB88C File Offset: 0x000D9C8C
	public void Layer2(bool value)
	{
		LayerMask mask = EasyTouch.Get3DPickableLayer();
		if (value)
		{
			mask |= 512;
		}
		else
		{
			mask = ~mask;
			mask = ~(mask | 512);
		}
		EasyTouch.Set3DPickableLayer(mask);
	}

	// Token: 0x06001B3B RID: 6971 RVA: 0x000DB8E4 File Offset: 0x000D9CE4
	public void Layer3(bool value)
	{
		LayerMask mask = EasyTouch.Get3DPickableLayer();
		if (value)
		{
			mask |= 1024;
		}
		else
		{
			mask = ~mask;
			mask = ~(mask | 1024);
		}
		EasyTouch.Set3DPickableLayer(mask);
	}
}
