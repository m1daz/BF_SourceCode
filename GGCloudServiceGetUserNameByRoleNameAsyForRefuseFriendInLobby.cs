using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using RioLog;
using SimpleJSON;

// Token: 0x02000493 RID: 1171
public class GGCloudServiceGetUserNameByRoleNameAsyForRefuseFriendInLobbyResponse : App42CallBack
{
	// Token: 0x060021ED RID: 8685 RVA: 0x000FB798 File Offset: 0x000F9B98
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
				string friendUserName = jobject["UserName"];
				if (UIFriendSystemDirector.mInstance != null)
				{
					UIFriendSystemDirector.mInstance.RefuseBtnPressedCoroutine(friendUserName);
				}
			}
		}
	}

	// Token: 0x060021EE RID: 8686 RVA: 0x000FB83A File Offset: 0x000F9C3A
	public void OnException(Exception e)
	{
		this.result = e.ToString();
		RioQerdoDebug.Log("Exception is : " + e);
	}

	// Token: 0x04002250 RID: 8784
	private string result = string.Empty;
}
