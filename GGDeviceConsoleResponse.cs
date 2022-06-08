using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine;

// Token: 0x020004C0 RID: 1216
public class GGDeviceConsoleResponse : App42CallBack
{
	// Token: 0x0600224A RID: 8778 RVA: 0x000FD568 File Offset: 0x000FB968
	public void OnSuccess(object obj)
	{
		try
		{
			this.result = obj.ToString();
			if (obj is Storage)
			{
				Storage storage = (Storage)obj;
				IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
		}
	}

	// Token: 0x0600224B RID: 8779 RVA: 0x000FD5C0 File Offset: 0x000FB9C0
	public void OnException(Exception e)
	{
		this.result = e.ToString();
		Debug.Log("Exception is : " + e);
	}

	// Token: 0x040022D3 RID: 8915
	private string result = string.Empty;
}
