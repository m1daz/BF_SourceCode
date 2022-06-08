using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using RioLog;
using SimpleJSON;

// Token: 0x02000491 RID: 1169
public class GGCloudServiceGetUserNameByRoleNameAsyForAddFriendInGameResponse : App42CallBack
{
	// Token: 0x060021E7 RID: 8679 RVA: 0x000FB5DC File Offset: 0x000F99DC
	public void OnSuccess(object obj)
	{
		this.result = obj.ToString();
		if (obj is Storage)
		{
			Storage storage = (Storage)obj;
			RioQerdoDebug.Log("Storage Response : " + storage);
			IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
			for (int i = 0; i < storage.GetJsonDocList().Count; i++)
			{
				string jsonDoc = jsonDocList[i].GetJsonDoc();
				JObject jobject = JObject.Parse(jsonDoc);
				string friendusername = jobject["UserName"];
				if (UIPauseDirector.mInstance != null)
				{
					UIPauseDirector.mInstance.AddFriendBtnPressedCoroutine(friendusername);
				}
			}
		}
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x000FB67E File Offset: 0x000F9A7E
	public void OnException(Exception e)
	{
		this.result = e.ToString();
		RioQerdoDebug.Log("Exception is : " + e);
	}

	// Token: 0x0400224E RID: 8782
	private string result = string.Empty;
}
