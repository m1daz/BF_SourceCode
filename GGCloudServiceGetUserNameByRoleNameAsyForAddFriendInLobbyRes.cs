using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using RioLog;
using SimpleJSON;

// Token: 0x02000490 RID: 1168
public class GGCloudServiceGetUserNameByRoleNameAsyForAddFriendInLobbyResponse : App42CallBack
{
	// Token: 0x060021E4 RID: 8676 RVA: 0x000FB4EC File Offset: 0x000F98EC
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
					UIFriendSystemDirector.mInstance.AddRequestSendBtnPressedLater(friendUserName);
				}
			}
		}
	}

	// Token: 0x060021E5 RID: 8677 RVA: 0x000FB58E File Offset: 0x000F998E
	public void OnException(Exception e)
	{
		this.result = e.ToString();
		if (UIFriendSystemDirector.mInstance != null)
		{
			UIFriendSystemDirector.mInstance.AddRequestSendBtnPressedLaterRoleNameNotExist();
		}
		RioQerdoDebug.Log("Exception is : " + e);
	}

	// Token: 0x0400224D RID: 8781
	private string result = string.Empty;
}
