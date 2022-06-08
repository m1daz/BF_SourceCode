using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000475 RID: 1141
public class GGCloudServiceDemo : MonoBehaviour
{
	// Token: 0x0600212D RID: 8493 RVA: 0x000F7022 File Offset: 0x000F5422
	private void Start()
	{
		this.mOurSendMessage = "Message";
	}

	// Token: 0x0600212E RID: 8494 RVA: 0x000F7030 File Offset: 0x000F5430
	public void OnGUI()
	{
		GUI.TextArea(new Rect(250f, 400f, 200f, 175f), this.mAllMessage);
		GUILayout.Label("Username: ", new GUILayoutOption[]
		{
			GUILayout.Width(80f)
		});
		GGCloudServiceConstant.mInstance.mUserName = GUILayout.TextField(GGCloudServiceConstant.mInstance.mUserName, new GUILayoutOption[0]);
		GUILayout.Label("Password: ", new GUILayoutOption[]
		{
			GUILayout.Width(80f)
		});
		this.mPassword = GUILayout.TextField(this.mPassword, new GUILayoutOption[0]);
		GUILayout.Label("Email: ", new GUILayoutOption[]
		{
			GUILayout.Width(80f)
		});
		GGCloudServiceConstant.mInstance.mEmailId = GUILayout.TextField(GGCloudServiceConstant.mInstance.mEmailId, new GUILayoutOption[0]);
		GUILayout.Label("msg: ", new GUILayoutOption[]
		{
			GUILayout.Width(80f)
		});
		this.mOurSendMessage = GUILayout.TextField(this.mOurSendMessage, new GUILayoutOption[0]);
		GUILayout.Label("add friend: ", new GUILayoutOption[]
		{
			GUILayout.Width(80f)
		});
		this.mRequestFriendName = GUILayout.TextField(this.mRequestFriendName, new GUILayoutOption[0]);
		GUILayout.Label("friend name: ", new GUILayoutOption[]
		{
			GUILayout.Width(80f)
		});
		this.mFriendName = GUILayout.TextField(this.mFriendName, new GUILayoutOption[0]);
		if (GUI.Button(new Rect(250f, 0f, 200f, 30f), "Sign Up"))
		{
			try
			{
				GGCloudServiceKit.mInstance.CreateUser(GGCloudServiceConstant.mInstance.mUserName, this.mPassword);
				Debug.Log("Sign up success!");
			}
			catch (Exception arg)
			{
				Debug.Log("Sign up exception: " + arg);
			}
		}
		if (GUI.Button(new Rect(250f, 50f, 200f, 30f), "Login In"))
		{
			GGCloudServiceKit.mInstance.LogIn(GGCloudServiceConstant.mInstance.mUserName, this.mPassword);
		}
		if (GUI.Button(new Rect(250f, 100f, 200f, 30f), "Log out"))
		{
			GGCloudServiceKit.mInstance.Logout();
			GGCloudServiceConstant.mInstance.mUserName = string.Empty;
			GGCloudServiceConstant.mInstance.mSessionId = string.Empty;
			GGCloudServiceConstant.mInstance.mEmailId = string.Empty;
			GGCloudServiceConstant.mInstance.mAllFriendsList.Clear();
		}
		if (GUI.Button(new Rect(250f, 150f, 200f, 30f), "Send Public Notice"))
		{
			GGCloudServiceKit.mInstance.SendPublicNotice("FirstOnline", "Thank you for surport!");
		}
		if (GUI.Button(new Rect(250f, 200f, 200f, 30f), "Get Session"))
		{
		}
		if (GUI.Button(new Rect(250f, 250f, 200f, 30f), "Add Friend Request"))
		{
			GGCloudServiceKit.mInstance.AddFriendRequest(GGCloudServiceConstant.mInstance.mUserName, this.mRequestFriendName, GGCloudServiceConstant.mInstance.mUserName + " request to add friend!");
		}
		GUI.Window(0, this.rectRequestWindow, new GUI.WindowFunction(this.RequestWindowFunc), "Request window");
		if (GUI.Button(new Rect(250f, 300f, 200f, 30f), "Get Friend Request"))
		{
			GGCloudServiceKit.mInstance.GetFriendRequest(GGCloudServiceConstant.mInstance.mUserName);
		}
		if (GUI.Button(new Rect(250f, 350f, 200f, 30f), "Update Friends"))
		{
			List<CSFriendInfo> list = new List<CSFriendInfo>();
			foreach (CSFriendInfo csfriendInfo in list)
			{
				Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
				Debug.Log(string.Concat(new object[]
				{
					csfriendInfo.Name,
					", ",
					csfriendInfo.IsOnline,
					", ",
					csfriendInfo.IsInRoom,
					", ",
					csfriendInfo.Room,
					", ",
					csfriendInfo.NewMessageNum
				}));
				foreach (CSMessage csmessage in csfriendInfo.messageList)
				{
					Debug.Log(string.Concat(new string[]
					{
						csmessage.sender,
						" to ",
						csmessage.receiver,
						" : ",
						csmessage.content
					}));
				}
			}
		}
		if (GUI.Button(new Rect(500f, 0f, 200f, 30f), "Create UserName Collection"))
		{
			GGCloudServiceKit.mInstance.CreateCacheUserName();
		}
		if (GUI.Button(new Rect(500f, 50f, 200f, 30f), "Create email"))
		{
			GGCloudServiceKit.mInstance.CreateCacheEmail();
		}
		if (GUI.Button(new Rect(500f, 350f, 200f, 30f), "create role name"))
		{
			GGCloudServiceKit.mInstance.CreateRoleName("379368292", "riovox-292");
		}
		if (GUI.Button(new Rect(500f, 100f, 200f, 30f), "Send Offical Message"))
		{
			GGCloudServiceKit.mInstance.SendOfficialMessageToAll("This is offical message");
		}
		if (GUI.Button(new Rect(500f, 150f, 200f, 30f), "Get Offical Message"))
		{
		}
		if (GUI.Button(new Rect(500f, 200f, 200f, 30f), "Get UserName"))
		{
		}
		if (GUI.Button(new Rect(500f, 250f, 200f, 30f), "denounce"))
		{
		}
		if (GUI.Button(new Rect(500f, 300f, 200f, 30f), "Get accused User"))
		{
			List<CSAccused> accusedList = GGCloudServiceKit.mInstance.GetAccusedList();
			foreach (CSAccused csaccused in accusedList)
			{
				Debug.Log(string.Concat(new object[]
				{
					"accused: ",
					csaccused.name,
					", times: ",
					csaccused.times
				}));
			}
			List<CSAccused> prosecutorList = GGCloudServiceKit.mInstance.GetProsecutorList();
			foreach (CSAccused csaccused2 in prosecutorList)
			{
				Debug.Log(string.Concat(new object[]
				{
					"prosecutor: ",
					csaccused2.name,
					", times: ",
					csaccused2.times
				}));
			}
		}
		if (PhotonNetwork.Friends != null)
		{
			foreach (FriendInfo friendInfo in PhotonNetwork.Friends)
			{
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.Label(friendInfo.ToString(), new GUILayoutOption[0]);
				if (friendInfo.IsOnline)
				{
					Debug.Log("...................................................");
				}
				if (GUILayout.Button("Send Msg", new GUILayoutOption[0]))
				{
					Debug.Log("send " + this.mOurSendMessage + " to " + friendInfo.Name);
				}
				if (!friendInfo.IsInRoom || GUILayout.Button("join", new GUILayoutOption[0]))
				{
				}
				GUILayout.EndHorizontal();
			}
		}
	}

	// Token: 0x0600212F RID: 8495 RVA: 0x000F78F8 File Offset: 0x000F5CF8
	private void RequestWindowFunc(int windowID)
	{
		for (int i = 0; i < this.mFriendRequestList.Count; i++)
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			GUILayout.Label(this.mFriendRequestList[i], new GUILayoutOption[0]);
			if (GUILayout.Button("Accept", new GUILayoutOption[0]))
			{
				GGCloudServiceKit.mInstance.AcceptFriendRequest(GGCloudServiceConstant.mInstance.mUserName, this.mFriendRequestList[i]);
			}
			if (GUILayout.Button("Reject", new GUILayoutOption[0]))
			{
				GGCloudServiceKit.mInstance.RejectFriendRequest(GGCloudServiceConstant.mInstance.mUserName, this.mFriendRequestList[i]);
			}
			GUILayout.EndHorizontal();
		}
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x000F79B2 File Offset: 0x000F5DB2
	private void Update()
	{
	}

	// Token: 0x040021E6 RID: 8678
	private Vector2 scrollPos = Vector2.zero;

	// Token: 0x040021E7 RID: 8679
	private string mPassword = string.Empty;

	// Token: 0x040021E8 RID: 8680
	private string mAllMessage = string.Empty;

	// Token: 0x040021E9 RID: 8681
	private string mAllOneFriendMessage = string.Empty;

	// Token: 0x040021EA RID: 8682
	private string mRequestFriendName = string.Empty;

	// Token: 0x040021EB RID: 8683
	private string mOurSendMessage = string.Empty;

	// Token: 0x040021EC RID: 8684
	private Rect rectRequestWindow = new Rect(450f, 400f, 200f, 175f);

	// Token: 0x040021ED RID: 8685
	private string mFriendName = string.Empty;

	// Token: 0x040021EE RID: 8686
	private string[] mAllFriendNameList;

	// Token: 0x040021EF RID: 8687
	private List<string> mFriendRequestList = new List<string>();
}
