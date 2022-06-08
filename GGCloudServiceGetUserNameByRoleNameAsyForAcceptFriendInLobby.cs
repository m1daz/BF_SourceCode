using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using RioLog;
using SimpleJSON;

// Token: 0x02000492 RID: 1170
public class GGCloudServiceGetUserNameByRoleNameAsyForAcceptFriendInLobbyResponse : App42CallBack
{
	// Token: 0x060021EA RID: 8682 RVA: 0x000FB6B0 File Offset: 0x000F9AB0
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
				string friendRoleName = jobject["RoleName"];
				if (UIFriendSystemDirector.mInstance != null)
				{
					UIFriendSystemDirector.mInstance.AcceptBtnPressedCoroutine(friendUserName, friendRoleName);
				}
			}
		}
	}

	// Token: 0x060021EB RID: 8683 RVA: 0x000FB767 File Offset: 0x000F9B67
	public void OnException(Exception e)
	{
		this.result = e.ToString();
		RioQerdoDebug.Log("Exception is : " + e);
	}

	// Token: 0x0400224F RID: 8783
	private string result = string.Empty;
}
