using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using SimpleJSON;

// Token: 0x02000488 RID: 1160
public class GGCloudServiceGetRoleNameByUserNameResponse : App42CallBack
{
	// Token: 0x060021CA RID: 8650 RVA: 0x000FAF20 File Offset: 0x000F9320
	public void OnSuccess(object response)
	{
		Storage storage = (Storage)response;
		IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
		for (int i = 0; i < jsonDocList.Count; i++)
		{
			string jsonDoc = jsonDocList[i].GetJsonDoc();
			JObject jobject = JObject.Parse(jsonDoc);
			string text = jobject["RoleName"];
		}
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x000FAF7A File Offset: 0x000F937A
	public void OnException(Exception e)
	{
	}
}
