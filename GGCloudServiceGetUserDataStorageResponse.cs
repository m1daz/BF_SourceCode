using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using RioLog;

// Token: 0x02000482 RID: 1154
public class GGCloudServiceGetUserDataStorageResponse : App42CallBack
{
	// Token: 0x060021B6 RID: 8630 RVA: 0x000FA288 File Offset: 0x000F8688
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
			RioQerdoDebug.Log(ex.ToString());
		}
	}

	// Token: 0x060021B7 RID: 8631 RVA: 0x000FA2E0 File Offset: 0x000F86E0
	public void OnException(Exception e)
	{
		this.result = e.ToString();
		RioQerdoDebug.Log("Exception is : " + e);
	}

	// Token: 0x0400223C RID: 8764
	private string result = string.Empty;
}
