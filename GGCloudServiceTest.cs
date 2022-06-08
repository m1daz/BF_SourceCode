using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

// Token: 0x020004A5 RID: 1189
public class GGCloudServiceTest : MonoBehaviour
{
	// Token: 0x06002228 RID: 8744 RVA: 0x000FCC03 File Offset: 0x000FB003
	private void Awake()
	{
		GGCloudServiceTest.mInstance = this;
	}

	// Token: 0x06002229 RID: 8745 RVA: 0x000FCC0B File Offset: 0x000FB00B
	private void Start()
	{
	}

	// Token: 0x0600222A RID: 8746 RVA: 0x000FCC0D File Offset: 0x000FB00D
	private void Update()
	{
	}

	// Token: 0x0600222B RID: 8747 RVA: 0x000FCC10 File Offset: 0x000FB010
	private void OnGUI()
	{
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "Create User"))
		{
			try
			{
				GGCloudServiceConstant.mInstance.mUser = null;
				GGCloudServiceKit.mInstance.CreateUser(GGCloudServiceConstant.mInstance.mUserName, this.mPassword);
				Debug.Log("Create success!");
			}
			catch (Exception arg)
			{
				Debug.Log("exception: " + arg);
			}
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "Login In"))
		{
			GGCloudServiceConstant.mInstance.mUserName = "379368233";
			try
			{
				GGCloudServiceAdapter.mInstance.mUserService.Authenticate(GGCloudServiceConstant.mInstance.mUserName, this.mPassword, null);
				GGCloudServiceConstant.mInstance.mSessionId = GGCloudServiceConstant.mInstance.mUser.sessionId;
				Debug.Log("Login in success!");
			}
			catch (Exception arg2)
			{
				Debug.Log("exception: " + arg2);
			}
		}
		if (GUI.Button(new Rect(50f, 300f, 200f, 30f), "Log out"))
		{
			GGCloudServiceKit.mInstance.Logout();
			GGCloudServiceConstant.mInstance.mUserName = string.Empty;
			GGCloudServiceConstant.mInstance.mSessionId = string.Empty;
		}
		if (GUI.Button(new Rect(300f, 50f, 200f, 30f), "SendFriendRequest"))
		{
			GGCloudServiceKit.mInstance.SendFriendRequest(GGCloudServiceConstant.mInstance.mUserName, this.mBuddyName, this.mMessage);
		}
		if (GUI.Button(new Rect(300f, 100f, 200f, 30f), "GetFriendRequest"))
		{
		}
		if (GUI.Button(new Rect(300f, 150f, 200f, 30f), "Accept Request List"))
		{
			for (int i = 0; i < GGCloudServiceConstant.mInstance.mBuddyRequestList.Count; i++)
			{
				GGCloudServiceConstant.mInstance.mBuddy = null;
				GGCloudServiceKit.mInstance.AcceptFriendRequest(GGCloudServiceConstant.mInstance.mUserName, GGCloudServiceConstant.mInstance.mBuddyRequestList[i].GetBuddyName());
			}
			GGCloudServiceConstant.mInstance.mBuddyRequestList.Clear();
		}
		if (GUI.Button(new Rect(300f, 200f, 200f, 30f), "Reject Request"))
		{
			for (int j = 0; j < GGCloudServiceConstant.mInstance.mBuddyRequestList.Count; j++)
			{
				GGCloudServiceConstant.mInstance.mBuddy = null;
				GGCloudServiceKit.mInstance.RejectFriendRequest(GGCloudServiceConstant.mInstance.mUserName, GGCloudServiceConstant.mInstance.mBuddyRequestList[j].GetBuddyName());
			}
			GGCloudServiceConstant.mInstance.mBuddyRequestList.Clear();
		}
		if (GUI.Button(new Rect(300f, 250f, 200f, 30f), "Get all friends"))
		{
		}
		if (GUI.Button(new Rect(300f, 300f, 200f, 30f), "CreateGroupByUser"))
		{
		}
		if (GUI.Button(new Rect(300f, 350f, 200f, 30f), "AddFriendToGroup"))
		{
		}
		if (GUI.Button(new Rect(300f, 400f, 200f, 30f), "GetAllGroups"))
		{
		}
		if (GUI.Button(new Rect(300f, 450f, 200f, 30f), "GetAllFriendsInGroup"))
		{
		}
		if (GUI.Button(new Rect(520f, 50f, 200f, 30f), "SendMessageToGroup"))
		{
		}
		if (GUI.Button(new Rect(520f, 100f, 200f, 30f), "Display all the friends"))
		{
			try
			{
				IList<GGCloudServiceGroupFriends> groupsFriendsList = GGCloudServiceBuddyManager.mInstance.GetGroupsFriendsList();
				for (int k = 0; k < groupsFriendsList.Count; k++)
				{
					Debug.Log("group Name: " + groupsFriendsList[k].mGroup.GetGroupName());
					Debug.Log("ownername Name: " + groupsFriendsList[k].mGroup.GetUserName());
					for (int l = 0; l < groupsFriendsList[k].mGroupFriendsList.Count; l++)
					{
						Debug.Log("------Friend" + l.ToString() + " " + groupsFriendsList[k].mGroupFriendsList[l].GetBuddyName());
					}
				}
				Debug.Log("Display all friends success!");
			}
			catch (Exception arg3)
			{
				Debug.Log("Exception: " + arg3);
			}
		}
		if (GUI.Button(new Rect(520f, 150f, 200f, 30f), "SendMessageToFriend"))
		{
		}
		if (GUI.Button(new Rect(520f, 200f, 200f, 30f), "SendMessageToFriends"))
		{
		}
		if (GUI.Button(new Rect(520f, 250f, 200f, 30f), "GetAllMessages"))
		{
		}
		if (GUI.Button(new Rect(520f, 300f, 200f, 30f), "GetAllMessagesFromBuddy"))
		{
		}
		if (GUI.Button(new Rect(520f, 350f, 200f, 30f), "GetAllMessagesFromGroup"))
		{
		}
		if (GUI.Button(new Rect(520f, 400f, 200f, 30f), "InsertUserProfile"))
		{
			UserProfile obj = new UserProfile();
			string json = JsonMapper.ToJson(obj);
			GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument("PLAYERPROFILE", "379368237_newmessage", json, null);
		}
		if (GUI.Button(new Rect(520f, 450f, 200f, 30f), "DeserializeUserProfile"))
		{
		}
	}

	// Token: 0x04002264 RID: 8804
	public static GGCloudServiceTest mInstance;

	// Token: 0x04002265 RID: 8805
	private string mPassword = "password";

	// Token: 0x04002266 RID: 8806
	private string mBuddyName = "379368239";

	// Token: 0x04002267 RID: 8807
	private string mMessage = "Hi, guys!";
}
