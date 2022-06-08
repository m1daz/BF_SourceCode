using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.buddy;
using com.shephertz.app42.paas.sdk.csharp.customcode;
using com.shephertz.app42.paas.sdk.csharp.game;
using com.shephertz.app42.paas.sdk.csharp.log;
using com.shephertz.app42.paas.sdk.csharp.pushNotification;
using com.shephertz.app42.paas.sdk.csharp.session;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp.upload;
using com.shephertz.app42.paas.sdk.csharp.user;
using RioLog;
using SimpleJSON;
using UnityEngine;

// Token: 0x02000470 RID: 1136
public class GGCloudServiceAdapter : MonoBehaviour
{
	// Token: 0x1400005B RID: 91
	// (add) Token: 0x060020F0 RID: 8432 RVA: 0x000F61D8 File Offset: 0x000F45D8
	// (remove) Token: 0x060020F1 RID: 8433 RVA: 0x000F6210 File Offset: 0x000F4610
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event CloudServiceEventHandler CSEvent;

	// Token: 0x060020F2 RID: 8434
	[DllImport("__Internal")]
	public static extern void registerForRemoteNotifications();

	// Token: 0x060020F3 RID: 8435
	[DllImport("__Internal")]
	public static extern void setListenerGameObject(string listenerName);

	// Token: 0x060020F4 RID: 8436 RVA: 0x000F6246 File Offset: 0x000F4646
	public static bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}

	// Token: 0x060020F5 RID: 8437 RVA: 0x000F624C File Offset: 0x000F464C
	private void Awake()
	{
		GGCloudServiceAdapter.mInstance = this;
		if (GGCloudServiceAdapter.<>f__mg$cache0 == null)
		{
			GGCloudServiceAdapter.<>f__mg$cache0 = new RemoteCertificateValidationCallback(GGCloudServiceAdapter.Validator);
		}
		ServicePointManager.ServerCertificateValidationCallback = GGCloudServiceAdapter.<>f__mg$cache0;
		this.mSp = new ServiceAPI("f85070c06309be56c3b45e34810fd2e4e31f5c994ccdc34dfd62dd1b096cd533", "1094e5d9c234ad253ba1fbb59f263192511b1f1705a7d05864470bf2ec26efbb");
		App42API.SetBaseURL("http://ns.api.shephertz.com/cloud/");
		this.mUserService = this.mSp.BuildUserService();
		this.mBuddyService = this.mSp.BuildBuddyService();
		this.mStorageService = this.mSp.BuildStorageService();
		this.mPushNotificationService = this.mSp.BuildPushNotificationService();
		this.mCustomCodeService = this.mSp.BuildCustomCodeService();
		this.mSessionService = this.mSp.BuildSessionService();
		this.mLogService = this.mSp.BuildLogService();
		this.mScoreBoardService = this.mSp.BuildScoreBoardService();
		this.mUploadService = this.mSp.BuildUploadService();
	}

	// Token: 0x060020F6 RID: 8438 RVA: 0x000F6339 File Offset: 0x000F4739
	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		this.SetGameObjectListener();
		GGCloudServiceCreate.mInstance.InitCloudServiceCreate();
	}

	// Token: 0x060020F7 RID: 8439 RVA: 0x000F6358 File Offset: 0x000F4758
	public void ChangeToNotAuthServiceAPIRCC()
	{
		this.mSp = new ServiceAPI("695f4352ba4e359357a36c417e08ca62224b9293c25e297207491a392dea3411", "007c21c2481bc5e6189b1dbeda41af854c0ff0b1dd591fcf329e096e04785618");
		App42API.SetBaseURL("http://ns.api.shephertz.com/cloud/");
		this.mUserService = this.mSp.BuildUserService();
		this.mBuddyService = this.mSp.BuildBuddyService();
		this.mStorageService = this.mSp.BuildStorageService();
		this.mPushNotificationService = this.mSp.BuildPushNotificationService();
		this.mCustomCodeService = this.mSp.BuildCustomCodeService();
		this.mSessionService = this.mSp.BuildSessionService();
		this.mLogService = this.mSp.BuildLogService();
		this.mScoreBoardService = this.mSp.BuildScoreBoardService();
		this.mUploadService = this.mSp.BuildUploadService();
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x000F6420 File Offset: 0x000F4820
	public void ChangeToNotAuthServiceAPIRData()
	{
		this.mSp = new ServiceAPI("27e8df6a053cfc086d2b09f646b5d9801e4c1552343233b7794cb20cf498628f", "d51b1a756591d0071cbf6e8729da793aaa4630635b86affc598c569acabcb92f");
		App42API.SetBaseURL("http://ns.api.shephertz.com/cloud/");
		this.mUserService = this.mSp.BuildUserService();
		this.mBuddyService = this.mSp.BuildBuddyService();
		this.mStorageService = this.mSp.BuildStorageService();
		this.mPushNotificationService = this.mSp.BuildPushNotificationService();
		this.mCustomCodeService = this.mSp.BuildCustomCodeService();
		this.mSessionService = this.mSp.BuildSessionService();
		this.mLogService = this.mSp.BuildLogService();
		this.mScoreBoardService = this.mSp.BuildScoreBoardService();
		this.mUploadService = this.mSp.BuildUploadService();
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x000F64E8 File Offset: 0x000F48E8
	public void ChangeToAuthServiceAPI()
	{
		this.mSp = new ServiceAPI("f85070c06309be56c3b45e34810fd2e4e31f5c994ccdc34dfd62dd1b096cd533", "1094e5d9c234ad253ba1fbb59f263192511b1f1705a7d05864470bf2ec26efbb");
		App42API.SetBaseURL("http://ns.api.shephertz.com/cloud/");
		this.mUserService = this.mSp.BuildUserService();
		this.mBuddyService = this.mSp.BuildBuddyService();
		this.mStorageService = this.mSp.BuildStorageService();
		this.mPushNotificationService = this.mSp.BuildPushNotificationService();
		this.mCustomCodeService = this.mSp.BuildCustomCodeService();
		this.mSessionService = this.mSp.BuildSessionService();
		this.mLogService = this.mSp.BuildLogService();
		this.mScoreBoardService = this.mSp.BuildScoreBoardService();
		this.mUploadService = this.mSp.BuildUploadService();
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x000F65AD File Offset: 0x000F49AD
	private void Update()
	{
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x000F65AF File Offset: 0x000F49AF
	public void CreateUser(string userName, string password, string email, App42CallBack createusercallback)
	{
		this.mUserService.CreateUser(userName, password, email, createusercallback);
	}

	// Token: 0x060020FC RID: 8444 RVA: 0x000F65C1 File Offset: 0x000F49C1
	public void Authenticate(string userName, string password)
	{
	}

	// Token: 0x060020FD RID: 8445 RVA: 0x000F65C3 File Offset: 0x000F49C3
	public void Logout()
	{
		this.mUserService.Logout(GGCloudServiceConstant.mInstance.mSessionId, new GGCloudServiceLogoutResponse());
	}

	// Token: 0x060020FE RID: 8446 RVA: 0x000F65DF File Offset: 0x000F49DF
	public void SendFriendRequest(string userName, string buddyName, string message)
	{
		RioQerdoDebug.Log("userName: " + userName);
		RioQerdoDebug.Log("buddyName: " + buddyName);
		this.mBuddyService.SendFriendRequest(userName, buddyName, message, this.mBuddyResponseCallBack);
	}

	// Token: 0x060020FF RID: 8447 RVA: 0x000F6618 File Offset: 0x000F4A18
	public void GetFriendRequest(string userName)
	{
		List<string> list = new List<string>();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		this.mBuddyService.GetFriendRequest(userName, new GGCloudServiceGetFriendRequestResponse());
	}

	// Token: 0x06002100 RID: 8448 RVA: 0x000F6644 File Offset: 0x000F4A44
	public void GetFriendRequest_buddylist(IList<Buddy> buddyList)
	{
		List<string> list = new List<string>();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (Buddy buddy in buddyList)
		{
			list.Add(buddy.GetBuddyName());
			string message = buddy.GetMessage();
			if (!message.Contains("request add friend!"))
			{
				dictionary.Add(buddy.GetBuddyName(), buddy.GetMessage());
			}
		}
		GGCloudServiceKit.mInstance.mFriendRequestList = list;
		GGCloudServiceKit.mInstance.mFriendRequestKeyUserNameValueRoleNameDic = dictionary;
	}

	// Token: 0x06002101 RID: 8449 RVA: 0x000F66F0 File Offset: 0x000F4AF0
	public void AcceptFriendRequest_FindDoc()
	{
	}

	// Token: 0x06002102 RID: 8450 RVA: 0x000F66F4 File Offset: 0x000F4AF4
	public void AcceptFriendRequest(string userName, string buddyName)
	{
		this.mBuddyService.AcceptFriendRequest(userName, buddyName, new GGCloudServiceAcceptFriendRequestResponse(AcceptFriendRequestType.AcceptFriendRequest));
		string collectionName = "UserProfileCollection";
		string key = "UserName";
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, collectionName, key, buddyName, new GGCloudServiceAcceptFriendRequestResponse(AcceptFriendRequestType.FindDoc, userName, buddyName));
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("UserName", buddyName);
		jsonclass.Add("NewFriendUserName", userName);
		string mNewFriendInfoCollectionName = GGCloudServiceConstant.mInstance.mNewFriendInfoCollectionName;
		GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, mNewFriendInfoCollectionName, jsonclass, new GGCloudServiceAcceptFriendRequestResponse(AcceptFriendRequestType.InsertDocNewFriend, buddyName));
	}

	// Token: 0x06002103 RID: 8451 RVA: 0x000F67AB File Offset: 0x000F4BAB
	public void RejectFriendRequest(string userName, string buddyName)
	{
		this.mBuddyService.RejectFriendRequest(userName, buddyName, this.mBuddyResponseCallBack);
	}

	// Token: 0x06002104 RID: 8452 RVA: 0x000F67C0 File Offset: 0x000F4BC0
	public void GetUserNameAndPassword()
	{
		JSONClass jsonBody = new JSONClass();
		this.mCustomCodeService.RunJavaCode("GetUserName7", jsonBody, new GGCloudServiceGetUserNamePasswordCustomCodeResponse());
	}

	// Token: 0x06002105 RID: 8453 RVA: 0x000F67EC File Offset: 0x000F4BEC
	public void QuickEnterGameGetUserNameAndPassword()
	{
		JSONClass jsonBody = new JSONClass();
		this.mCustomCodeService.RunJavaCode("GetUserName7", jsonBody, new GGCloudServiceQuickEnterGameGetUserNamePasswordResponse());
	}

	// Token: 0x06002106 RID: 8454 RVA: 0x000F6818 File Offset: 0x000F4C18
	public void LUser(string userName)
	{
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("username", userName);
		this.mCustomCodeService.RunJavaCode("DoLUser1", jsonclass, new GGCloudServiceCustomCodeResponse());
	}

	// Token: 0x06002107 RID: 8455 RVA: 0x000F6854 File Offset: 0x000F4C54
	public void UploadUserData(Dictionary<string, object> dataCachetmp, string tmpUserName)
	{
		string key = "UserName";
		if (!dataCachetmp.ContainsKey("UserName"))
		{
			dataCachetmp.Add(key, tmpUserName);
		}
		string mUserDataCollectionName = GGCloudServiceConstant.mInstance.mUserDataCollectionName;
		if (ACTUserDataManager.mInstance.mClickLogOut)
		{
			this.mStorageService.UpdateDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, mUserDataCollectionName, key, tmpUserName, dataCachetmp, new GGCloudServiceCustomCodeResponse());
		}
		else
		{
			this.mStorageService.UpdateDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, mUserDataCollectionName, key, tmpUserName, dataCachetmp, new GGCloudServiceCustomCodeResponse());
		}
	}

	// Token: 0x06002108 RID: 8456 RVA: 0x000F68DC File Offset: 0x000F4CDC
	private void SetGameObjectListener()
	{
	}

	// Token: 0x06002109 RID: 8457 RVA: 0x000F68DE File Offset: 0x000F4CDE
	public void BindUserNameWithDeviceToken(string tmpUserName)
	{
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x000F68E0 File Offset: 0x000F4CE0
	private void onDidRegisterForRemoteNotificationsWithDeviceToken(string deviceToken)
	{
		RioQerdoDebug.Log("@@@@@@@@@@@@@@@@@@@@@@@@@@@deviceToken: " + deviceToken);
		if (deviceToken != null && deviceToken.Length != 0)
		{
			PlayerPrefs.SetString("GGCloudServiceDeviceTokenToBind", deviceToken);
		}
	}

	// Token: 0x0600210B RID: 8459 RVA: 0x000F690E File Offset: 0x000F4D0E
	private void registerDeviceTokenToApp42PushNotificationService(string devToken, string userName)
	{
		RioQerdoDebug.Log("registerDeviceTokenToApp42PushNotificationService:");
		PlayerPrefs.SetString("GGCloudServiceLastSubscribeDeviceTokenUserName", userName);
	}

	// Token: 0x0600210C RID: 8460 RVA: 0x000F6925 File Offset: 0x000F4D25
	private void onDidFailToRegisterForRemoteNotificcallBackationsWithError(string error)
	{
		RioQerdoDebug.Log(error);
	}

	// Token: 0x0600210D RID: 8461 RVA: 0x000F692D File Offset: 0x000F4D2D
	private void onPushNotificationsReceived(string pushMessageString)
	{
		RioQerdoDebug.Log("onPushNotificationsReceived: " + pushMessageString);
		if (this.CSEvent != null)
		{
			this.CSEvent(GGCloudServiceEventType.HaveNewMessage);
		}
	}

	// Token: 0x0600210E RID: 8462 RVA: 0x000F6958 File Offset: 0x000F4D58
	public void SendPushToUser(string userName, string message)
	{
		this.mPushNotificationService.SendPushMessageToUser(userName, message, this.callBack);
		string message2 = "{'badge':'increment','sound':'default'}";
		this.mPushNotificationService.SendPushMessageToUser(userName, message2, this.callBack);
	}

	// Token: 0x0600210F RID: 8463 RVA: 0x000F6994 File Offset: 0x000F4D94
	public void SendPushMessageToAll(string message)
	{
		this.mPushNotificationService.SendPushMessageToAll(message, this.callBack);
		string message2 = "{'badge':'increment','sound':'default'}";
		this.mPushNotificationService.SendPushMessageToAll(message2, this.callBack);
	}

	// Token: 0x06002110 RID: 8464 RVA: 0x000F69CB File Offset: 0x000F4DCB
	public void DeleteDeviceToken(string userName, string devToken)
	{
	}

	// Token: 0x06002111 RID: 8465 RVA: 0x000F69CD File Offset: 0x000F4DCD
	public void DeleteDeviceTokenAsy(string userName, string devToken)
	{
	}

	// Token: 0x06002112 RID: 8466 RVA: 0x000F69CF File Offset: 0x000F4DCF
	public void UpdatePushBadgeforUser(string userName, int numbers)
	{
		this.mPushNotificationService.UpdatePushBadgeforUser(userName, numbers, this.callBack);
	}

	// Token: 0x06002113 RID: 8467 RVA: 0x000F69E4 File Offset: 0x000F4DE4
	public void GetFilesByType(string uploadfiletype, int max, int offset, App42CallBack callback)
	{
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("uploadfiletype", uploadfiletype);
		jsonclass.Add("max", max);
		jsonclass.Add("offset", offset);
		this.mCustomCodeService.RunJavaCode("GetFileByType", jsonclass, callback);
	}

	// Token: 0x06002114 RID: 8468 RVA: 0x000F6A40 File Offset: 0x000F4E40
	public void GetSlotTopPrize(int max, int offset, App42CallBack callback)
	{
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("max", max);
		jsonclass.Add("offset", offset);
		jsonclass.Add("gameversion", GameVersionController.GameVersion);
		this.mCustomCodeService.RunJavaCode("GetSlotTopPrize2", jsonclass, callback);
	}

	// Token: 0x06002115 RID: 8469 RVA: 0x000F6A9C File Offset: 0x000F4E9C
	public void UploadSlotTopPrize(string rolename, string prizename, string prizeAddtionalInfo)
	{
		string collectionName = "SlotTopPrizeCollection" + GameVersionController.GameVersion;
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("RoleName", rolename);
		jsonclass.Add("PrizeName", prizename);
		jsonclass.Add("PrizeInfo", prizeAddtionalInfo);
		GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, collectionName, jsonclass, new GGCloudServiceGetUserDataStorageResponse());
	}

	// Token: 0x06002116 RID: 8470 RVA: 0x000F6B12 File Offset: 0x000F4F12
	public void UpdateEmail(string userName, string emailId)
	{
		this.mUserService.UpdateEmail(userName, emailId, new GGCloudServicePasswordResponse());
	}

	// Token: 0x06002117 RID: 8471 RVA: 0x000F6B28 File Offset: 0x000F4F28
	public void ResetUserPassword(string userName)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("emailVerification", "true");
		this.mUserService.SetOtherMetaHeaders(dictionary);
		this.mUserService.ResetUserPassword(userName, new GGCloudServicePasswordResponse());
	}

	// Token: 0x06002118 RID: 8472 RVA: 0x000F6B68 File Offset: 0x000F4F68
	public void GetMySeasonRank(string gamename, string userName, App42CallBack callBack)
	{
		this.mScoreBoardService.GetUserRanking(gamename, userName, callBack);
	}

	// Token: 0x06002119 RID: 8473 RVA: 0x000F6B78 File Offset: 0x000F4F78
	public void AddBlackList(string userName, string roleName)
	{
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("UserName", userName);
		jsonclass.Add("RoleName", roleName);
		jsonclass.Add("Action", "modify the user data");
		GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, "Blacklist", jsonclass, new GGCloudServiceGetUserDataStorageResponse());
	}

	// Token: 0x0600211A RID: 8474 RVA: 0x000F6BE8 File Offset: 0x000F4FE8
	public void AddBlackListToDelete(string userName, string roleName, string action)
	{
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("UserName", userName);
		jsonclass.Add("RoleName", roleName);
		jsonclass.Add("Action", action);
		GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, "BlacklistToDelete", jsonclass, new GGCloudServiceGetUserDataStorageResponse());
	}

	// Token: 0x0600211B RID: 8475 RVA: 0x000F6C52 File Offset: 0x000F5052
	public void DeleteUser(string username)
	{
		GGCloudServiceAdapter.mInstance.mUserService.DeleteUser(username, new GGCloudServiceUnKnownServiceResponse());
	}

	// Token: 0x040021B9 RID: 8633
	public static GGCloudServiceAdapter mInstance;

	// Token: 0x040021BA RID: 8634
	public ServiceAPI mSp;

	// Token: 0x040021BB RID: 8635
	public UserService mUserService;

	// Token: 0x040021BC RID: 8636
	public BuddyService mBuddyService;

	// Token: 0x040021BD RID: 8637
	public StorageService mStorageService;

	// Token: 0x040021BE RID: 8638
	public PushNotificationService mPushNotificationService;

	// Token: 0x040021BF RID: 8639
	public CustomCodeService mCustomCodeService;

	// Token: 0x040021C0 RID: 8640
	public SessionService mSessionService;

	// Token: 0x040021C1 RID: 8641
	public LogService mLogService;

	// Token: 0x040021C2 RID: 8642
	public ScoreBoardService mScoreBoardService;

	// Token: 0x040021C3 RID: 8643
	public UploadService mUploadService;

	// Token: 0x040021C4 RID: 8644
	public GGCloudServiceUserResponse mUserResponseCallBack = new GGCloudServiceUserResponse();

	// Token: 0x040021C5 RID: 8645
	private GGCloudServiceBuddyResponse mBuddyResponseCallBack = new GGCloudServiceBuddyResponse();

	// Token: 0x040021C7 RID: 8647
	private PushResponse callBack = new PushResponse();

	// Token: 0x040021C8 RID: 8648
	[CompilerGenerated]
	private static RemoteCertificateValidationCallback <>f__mg$cache0;
}
