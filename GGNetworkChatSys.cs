using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Chat;
using RioLog;
using UnityEngine;

// Token: 0x0200052B RID: 1323
public class GGNetworkChatSys : MonoBehaviour, IChatClientListener
{
	// Token: 0x0600258A RID: 9610 RVA: 0x00116685 File Offset: 0x00114A85
	private void Awake()
	{
		GGNetworkChatSys.mInstance = this;
	}

	// Token: 0x0600258B RID: 9611 RVA: 0x00116690 File Offset: 0x00114A90
	private void Start()
	{
		this.Init();
		if (GGNetworkChatSys.mInstanceRef == null)
		{
			GGNetworkChatSys.mInstanceRef = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
		Application.runInBackground = true;
		this.chatClient = new ChatClient(this, ConnectionProtocol.Udp);
		this.gameVersion = "gog_ios_" + GameVersionController.PhotonGameVersion;
		this.chatClient.Connect(this.ChatAppId, this.gameVersion, new ExitGames.Client.Photon.Chat.AuthenticationValues(this.RoleName));
	}

	// Token: 0x0600258C RID: 9612 RVA: 0x0011671F File Offset: 0x00114B1F
	private void Init()
	{
		this.ChatAppId = "9f325d14-1311-43dd-a23e-7216d9baa416";
		this.RoleName = UIUserDataController.GetDefaultRoleName();
		this.ChannelsToJoinOnConnect = new string[]
		{
			"Region"
		};
		this.HistoryLengthToFetch = 0;
	}

	// Token: 0x0600258D RID: 9613 RVA: 0x00116754 File Offset: 0x00114B54
	private void Update()
	{
		if (Application.loadedLevelName == "UILogin")
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
			return;
		}
		if (this.chatClient != null)
		{
			this.chatClient.Service();
		}
		this.mLastUpdateRoomStatusTime += Time.deltaTime;
		if (this.mLastUpdateRoomStatusTime > 3f)
		{
			this.mLastUpdateRoomStatusTime = 0f;
			if (PhotonNetwork.room == null && this.chatClient.State == ChatState.ConnectedToFrontEnd)
			{
				string str = "1";
				if (PhotonNetwork.player != null)
				{
					try
					{
						str = PhotonNetwork.player.customProperties["rank"].ToString();
					}
					catch (Exception ex)
					{
					}
				}
				string str2 = ((int)GGNetworkKit.mInstance.GetCurrentServerRegion()).ToString();
				string message = str + ";" + str2;
				this.chatClient.SetOnlineStatus(2, message);
			}
			if (this.mAllFriendRoleNameListPreCount != -1 && this.mAllFriendRoleNameListPreCount < GGCloudServiceKit.mInstance.mAllFriendRoleNameList.Count)
			{
				this.chatClient.RemoveFriends(this.mAllFriendRoleNameListPre);
				this.chatClient.AddFriends(GGCloudServiceKit.mInstance.mAllFriendRoleNameList.ToArray());
				this.mAllFriendRoleNameListPreCount = GGCloudServiceKit.mInstance.mAllFriendRoleNameList.Count;
				this.mAllFriendRoleNameListPre = new string[this.mAllFriendRoleNameListPreCount];
				GGCloudServiceKit.mInstance.mAllFriendRoleNameList.ToArray().CopyTo(this.mAllFriendRoleNameListPre, 0);
			}
		}
		this.mLastUpdateRoomStatusTime2 += Time.deltaTime;
		if (this.mLastUpdateRoomStatusTime2 > 0.5f)
		{
			this.mLastUpdateRoomStatusTime2 = 0f;
			if (this.chatClient.State == ChatState.Disconnected)
			{
				this.chatClient.Disconnect();
				this.chatClient = null;
				this.chatClient = new ChatClient(this, ConnectionProtocol.Udp);
				this.chatClient.Connect(this.ChatAppId, this.gameVersion, new ExitGames.Client.Photon.Chat.AuthenticationValues(this.RoleName));
			}
		}
	}

	// Token: 0x0600258E RID: 9614 RVA: 0x00116970 File Offset: 0x00114D70
	public void DisconnectChatSys()
	{
		if (this.chatClient != null)
		{
			this.chatClient.Disconnect();
		}
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x00116988 File Offset: 0x00114D88
	public void SendPrivateMessage(string target, object message)
	{
		RioQerdoDebug.Log(target);
		RioQerdoDebug.Log(message.ToString());
		this.chatClient.SendPrivateMessage(target, message);
	}

	// Token: 0x06002590 RID: 9616 RVA: 0x001169A9 File Offset: 0x00114DA9
	public void SendFriendRequestMessage(string target)
	{
		this.chatClient.SendPrivateMessage(target, UIUserDataController.GetDefaultUserName() + "_" + UIUserDataController.GetDefaultRoleName() + "_AddFriendRequest");
	}

	// Token: 0x06002591 RID: 9617 RVA: 0x001169D1 File Offset: 0x00114DD1
	public void SendFriendAcceptMessage(string target)
	{
		this.chatClient.SendPrivateMessage(target, UIUserDataController.GetDefaultUserName() + "_" + UIUserDataController.GetDefaultRoleName() + "_AcceptFriendRequest");
	}

	// Token: 0x06002592 RID: 9618 RVA: 0x001169F9 File Offset: 0x00114DF9
	public void SendPublishMessage(string chanelName, object message)
	{
		this.chatClient.PublishMessage(chanelName, message);
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x00116A09 File Offset: 0x00114E09
	public void OnApplicationQuit()
	{
		if (this.chatClient != null)
		{
			this.chatClient.Disconnect();
		}
	}

	// Token: 0x06002594 RID: 9620 RVA: 0x00116A24 File Offset: 0x00114E24
	public void OnConnected()
	{
		if (this.ChannelsToJoinOnConnect != null && this.ChannelsToJoinOnConnect.Length > 0)
		{
			this.chatClient.Subscribe(this.ChannelsToJoinOnConnect, this.HistoryLengthToFetch);
		}
		this.chatClient.AddFriends(GGCloudServiceKit.mInstance.mAllFriendRoleNameList.ToArray());
		this.mAllFriendRoleNameListPreCount = GGCloudServiceKit.mInstance.mAllFriendRoleNameList.Count;
		this.mAllFriendRoleNameListPre = new string[this.mAllFriendRoleNameListPreCount];
		GGCloudServiceKit.mInstance.mAllFriendRoleNameList.ToArray().CopyTo(this.mAllFriendRoleNameListPre, 0);
		string str = "1";
		if (PhotonNetwork.player != null)
		{
			try
			{
				str = PhotonNetwork.player.customProperties["rank"].ToString();
			}
			catch (Exception ex)
			{
				str = "1";
			}
		}
		string str2 = ((int)GGNetworkKit.mInstance.GetCurrentServerRegion()).ToString();
		string msg = str + ";" + str2;
		RioQerdoDebug.Log(msg);
		this.chatClient.SetOnlineStatus(2);
	}

	// Token: 0x06002595 RID: 9621 RVA: 0x00116B44 File Offset: 0x00114F44
	public void OnDisconnected()
	{
		RioQerdoDebug.Log("OnDisconnected");
	}

	// Token: 0x06002596 RID: 9622 RVA: 0x00116B50 File Offset: 0x00114F50
	public void OnChatStateChange(ChatState state)
	{
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x00116B52 File Offset: 0x00114F52
	public void OnSubscribed(string[] channels, bool[] results)
	{
		RioQerdoDebug.Log("OnSubscribed");
	}

	// Token: 0x06002598 RID: 9624 RVA: 0x00116B5E File Offset: 0x00114F5E
	public void OnUnsubscribed(string[] channels)
	{
		RioQerdoDebug.Log("OnUnsubscribed");
	}

	// Token: 0x06002599 RID: 9625 RVA: 0x00116B6A File Offset: 0x00114F6A
	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		RioQerdoDebug.Log("OnGetMessages");
	}

	// Token: 0x0600259A RID: 9626 RVA: 0x00116B76 File Offset: 0x00114F76
	public void DebugReturn(DebugLevel level, string message)
	{
		RioQerdoDebug.Log(message);
	}

	// Token: 0x0600259B RID: 9627 RVA: 0x00116B80 File Offset: 0x00114F80
	public void OnPrivateMessage(string sender, object message, string channelName)
	{
		if (message != null)
		{
			if (message.ToString().Contains("AddFriendRequest"))
			{
				if (sender != UIUserDataController.GetDefaultRoleName())
				{
					if (!GGCloudServiceKit.mInstance.mFriendRequestList.Contains(message.ToString().Split(new char[]
					{
						'_'
					})[0]))
					{
						GGCloudServiceKit.mInstance.mFriendRequestList.Add(message.ToString().Split(new char[]
						{
							'_'
						})[0]);
					}
					if (!GGCloudServiceKit.mInstance.mFriendRequestKeyUserNameValueRoleNameDic.ContainsKey(message.ToString().Split(new char[]
					{
						'_'
					})[0]))
					{
						GGCloudServiceKit.mInstance.mFriendRequestKeyUserNameValueRoleNameDic.Add(message.ToString().Split(new char[]
						{
							'_'
						})[0], message.ToString().Split(new char[]
						{
							'_'
						})[1]);
					}
				}
				return;
			}
			if (message.ToString().Contains("AcceptFriendRequest"))
			{
				if (sender != UIUserDataController.GetDefaultRoleName())
				{
					RioQerdoDebug.Log("AcceptFriendRequest----1");
					string text = message.ToString().Split(new char[]
					{
						'_'
					})[0];
					string text2 = message.ToString().Split(new char[]
					{
						'_'
					})[1];
					if (!GGCloudServiceKit.mInstance.mAllFriendNameList.Contains(text) && !GGCloudServiceKit.mInstance.mAllFriendRoleNameList.Contains(text2))
					{
						RioQerdoDebug.Log("AcceptFriendRequest----2");
						GGCloudServiceKit.mInstance.mAllFriendNameList.Add(text);
						GGCloudServiceKit.mInstance.mAllFriendRoleNameList.Add(text2);
						if (!GGCloudServiceKit.mInstance.mUserNameRoleNameDic.ContainsKey(text))
						{
							GGCloudServiceKit.mInstance.mUserNameRoleNameDic.Add(text, text2);
						}
						if (!GGCloudServiceKit.mInstance.mRoleNameUserNameDic.ContainsKey(text2))
						{
							GGCloudServiceKit.mInstance.mRoleNameUserNameDic.Add(text2, text);
						}
						CSFriendInfo csfriendInfo = new CSFriendInfo();
						csfriendInfo.Name = text;
						csfriendInfo.RoleName = text2;
						csfriendInfo.IsOnline = false;
						csfriendInfo.IsInRoom = false;
						csfriendInfo.Room = string.Empty;
						csfriendInfo.messageList = new List<CSMessage>();
						RioQerdoDebug.Log("AcceptFriendRequest----3: " + GGCloudServiceKit.mInstance.mFriendInfoList.Count);
						GGCloudServiceKit.mInstance.mFriendInfoList.Add(csfriendInfo);
						RioQerdoDebug.Log("AcceptFriendRequest----4: " + GGCloudServiceKit.mInstance.mFriendInfoList.Count);
						GGCloudServiceKit.mInstance.GetNewFriendInfo(UIUserDataController.GetDefaultUserName());
					}
				}
				return;
			}
		}
		List<CSFriendInfo> mFriendInfoList = GGCloudServiceKit.mInstance.mFriendInfoList;
		int count = mFriendInfoList.Count;
		ChatChannel chatChannel = this.chatClient.PrivateChannels[channelName];
		int count2 = chatChannel.Messages.Count;
		string[] array = channelName.Split(new char[]
		{
			':'
		});
		RioQerdoDebug.Log(array[0] + "   " + array[1]);
		for (int i = 0; i < count; i++)
		{
			if (mFriendInfoList[i].RoleName != UIUserDataController.GetDefaultRoleName() && (mFriendInfoList[i].RoleName == array[0] || mFriendInfoList[i].RoleName == array[1]))
			{
				mFriendInfoList[i].messageList.Clear();
				for (int j = 0; j < count2; j++)
				{
					CSMessage csmessage = new CSMessage();
					csmessage.sender = chatChannel.Senders[j];
					csmessage.content = chatChannel.Messages[j].ToString();
					if (!csmessage.content.Contains("AcceptFriendRequest") && !csmessage.content.Contains("AddFriendRequest"))
					{
						mFriendInfoList[i].messageList.Add(csmessage);
						mFriendInfoList[i].IsHaveNewMessage = true;
					}
				}
			}
		}
		foreach (CSFriendInfo csfriendInfo2 in mFriendInfoList)
		{
			foreach (CSMessage csmessage2 in csfriendInfo2.messageList)
			{
				RioQerdoDebug.Log(csmessage2.sender + " : " + csmessage2.content);
			}
		}
	}

	// Token: 0x0600259C RID: 9628 RVA: 0x00117038 File Offset: 0x00115438
	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		RioQerdoDebug.Log(string.Concat(new object[]
		{
			"OnStatusUpdate: ",
			message,
			", ",
			status
		}));
		List<CSFriendInfo> mFriendInfoList = GGCloudServiceKit.mInstance.mFriendInfoList;
		int count = mFriendInfoList.Count;
		for (int i = 0; i < count; i++)
		{
			if (mFriendInfoList[i].RoleName == user)
			{
				switch (status)
				{
				case 0:
					mFriendInfoList[i].IsOnline = false;
					mFriendInfoList[i].IsInRoom = false;
					break;
				case 1:
					mFriendInfoList[i].IsOnline = false;
					mFriendInfoList[i].IsInRoom = false;
					break;
				case 2:
					mFriendInfoList[i].IsOnline = true;
					mFriendInfoList[i].IsInRoom = false;
					if (message != null)
					{
						string[] array = message.ToString().Split(new char[]
						{
							';'
						});
						mFriendInfoList[i].rank = array[0];
						mFriendInfoList[i].region = (GGServerRegion)int.Parse(array[1]);
					}
					break;
				case 3:
					mFriendInfoList[i].IsOnline = false;
					mFriendInfoList[i].IsInRoom = false;
					break;
				case 4:
					mFriendInfoList[i].IsOnline = false;
					mFriendInfoList[i].IsInRoom = false;
					break;
				case 5:
					mFriendInfoList[i].IsOnline = false;
					mFriendInfoList[i].IsInRoom = false;
					break;
				case 6:
					mFriendInfoList[i].IsOnline = true;
					mFriendInfoList[i].IsInRoom = true;
					if (message != null)
					{
						string[] array = message.ToString().Split(new char[]
						{
							';'
						});
						mFriendInfoList[i].Room = array[0];
						mFriendInfoList[i].mapName = array[1];
						mFriendInfoList[i].modeName = array[2];
						mFriendInfoList[i].playersCount = array[3];
						mFriendInfoList[i].maxplayersCount = array[4];
						mFriendInfoList[i].rank = array[5];
						mFriendInfoList[i].region = (GGServerRegion)int.Parse(array[6]);
						if (array[7].ToLower() == "true")
						{
							mFriendInfoList[i].encrytion = true;
						}
						else
						{
							mFriendInfoList[i].encrytion = false;
						}
						mFriendInfoList[i].password = array[8];
						if (array[9].ToLower() == "true")
						{
							mFriendInfoList[i].open = true;
						}
						else
						{
							mFriendInfoList[i].open = false;
						}
					}
					break;
				}
				break;
			}
		}
	}

	// Token: 0x04002623 RID: 9763
	public static GGNetworkChatSys mInstance;

	// Token: 0x04002624 RID: 9764
	private static GGNetworkChatSys mInstanceRef;

	// Token: 0x04002625 RID: 9765
	public ChatClient chatClient;

	// Token: 0x04002626 RID: 9766
	private string ChatAppId;

	// Token: 0x04002627 RID: 9767
	private string RoleName;

	// Token: 0x04002628 RID: 9768
	private string[] ChannelsToJoinOnConnect;

	// Token: 0x04002629 RID: 9769
	private int HistoryLengthToFetch;

	// Token: 0x0400262A RID: 9770
	private float mLastUpdateRoomStatusTime;

	// Token: 0x0400262B RID: 9771
	private float mLastUpdateRoomStatusTime2;

	// Token: 0x0400262C RID: 9772
	private int mAllFriendRoleNameListPreCount = -1;

	// Token: 0x0400262D RID: 9773
	public string[] mAllFriendRoleNameListPre;

	// Token: 0x0400262E RID: 9774
	private string gameVersion;
}
