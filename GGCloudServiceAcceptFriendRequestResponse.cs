using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using SimpleJSON;

// Token: 0x02000499 RID: 1177
public class GGCloudServiceAcceptFriendRequestResponse : App42CallBack
{
	// Token: 0x06002200 RID: 8704 RVA: 0x000FBAD2 File Offset: 0x000F9ED2
	public GGCloudServiceAcceptFriendRequestResponse(AcceptFriendRequestType acceptFriendRequestType)
	{
		this.mAcceptFriendRequestType = acceptFriendRequestType;
	}

	// Token: 0x06002201 RID: 8705 RVA: 0x000FBAE1 File Offset: 0x000F9EE1
	public GGCloudServiceAcceptFriendRequestResponse(AcceptFriendRequestType acceptFriendRequestType, string username)
	{
		this.UserName = username;
		this.mAcceptFriendRequestType = acceptFriendRequestType;
	}

	// Token: 0x06002202 RID: 8706 RVA: 0x000FBAF7 File Offset: 0x000F9EF7
	public GGCloudServiceAcceptFriendRequestResponse(AcceptFriendRequestType acceptFriendRequestType, string username, string buddyname)
	{
		this.UserName = username;
		this.BuddyName = buddyname;
		this.mAcceptFriendRequestType = acceptFriendRequestType;
	}

	// Token: 0x06002203 RID: 8707 RVA: 0x000FBB14 File Offset: 0x000F9F14
	public void OnSuccess(object obj)
	{
		Storage storage = (Storage)obj;
		AcceptFriendRequestType acceptFriendRequestType = this.mAcceptFriendRequestType;
		if (acceptFriendRequestType != AcceptFriendRequestType.FindDoc)
		{
			if (acceptFriendRequestType != AcceptFriendRequestType.InsertDocNewFriend)
			{
				if (acceptFriendRequestType == AcceptFriendRequestType.InsertDoc)
				{
					GGCloudServiceKit.mInstance.AcceptFriendRequest_CallBack(this.UserName, this.BuddyName);
				}
			}
			else
			{
				IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
				for (int i = 0; i < jsonDocList.Count; i++)
				{
					string docId = jsonDocList[i].GetDocId();
					HashSet<ACL> hashSet = new HashSet<ACL>();
					hashSet.Add(new ACL(this.UserName, Permission.READ));
					hashSet.Add(new ACL(this.UserName, Permission.WRITE));
					GGCloudServiceAdapter.mInstance.mStorageService.GrantAccessOnDoc(GGCloudServiceConstant.mInstance.mDBName, GGCloudServiceConstant.mInstance.mNewFriendInfoCollectionName, docId, hashSet, null);
				}
			}
		}
		else
		{
			IList<Storage.JSONDocument> jsonDocList2 = storage.GetJsonDocList();
			string s = string.Empty;
			for (int j = 0; j < jsonDocList2.Count; j++)
			{
				string jsonDoc = jsonDocList2[j].GetJsonDoc();
				JObject jobject = JObject.Parse(jsonDoc);
				s = jobject["RoleName"];
			}
			JSONClass jsonclass = new JSONClass();
			jsonclass.Add("UserName", this.UserName);
			jsonclass.Add("RoleName", s);
			GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, "FriendCollection", jsonclass, new GGCloudServiceAcceptFriendRequestResponse(AcceptFriendRequestType.InsertDoc, this.UserName, this.BuddyName));
		}
	}

	// Token: 0x06002204 RID: 8708 RVA: 0x000FBCB5 File Offset: 0x000FA0B5
	public void OnException(Exception e)
	{
	}

	// Token: 0x04002253 RID: 8787
	public AcceptFriendRequestType mAcceptFriendRequestType;

	// Token: 0x04002254 RID: 8788
	public string UserName;

	// Token: 0x04002255 RID: 8789
	public string BuddyName;
}
