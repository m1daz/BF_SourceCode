using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using com.shephertz.app42.paas.sdk.csharp.storage;
using GOG.Utility;
using RioLog;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Advertisements;

// Token: 0x0200047F RID: 1151
public class GGCloudServiceKit : MonoBehaviour
{
	// Token: 0x1400005C RID: 92
	// (add) Token: 0x0600214E RID: 8526 RVA: 0x000F86A0 File Offset: 0x000F6AA0
	// (remove) Token: 0x0600214F RID: 8527 RVA: 0x000F86D8 File Offset: 0x000F6AD8
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event CloudServiceErrorEventHandler CSErrorEvent;

	// Token: 0x06002150 RID: 8528 RVA: 0x000F870E File Offset: 0x000F6B0E
	private void Awake()
	{
		GGCloudServiceKit.mInstance = this;
	}

	// Token: 0x06002151 RID: 8529 RVA: 0x000F8716 File Offset: 0x000F6B16
	public List<CSFriendInfo> GetFriendInfomationList()
	{
		return new List<CSFriendInfo>(this.mFriendInfoList.ToArray());
	}

	// Token: 0x06002152 RID: 8530 RVA: 0x000F8728 File Offset: 0x000F6B28
	private void Start()
	{
		this.GeneratePublicK();
		try
		{
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x06002153 RID: 8531 RVA: 0x000F8758 File Offset: 0x000F6B58
	// (set) Token: 0x06002154 RID: 8532 RVA: 0x000F8760 File Offset: 0x000F6B60
	public double CurrentAllowance
	{
		get
		{
			return this.mCurrentAllowance;
		}
		set
		{
			this.mCurrentAllowance = value;
		}
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x06002155 RID: 8533 RVA: 0x000F8769 File Offset: 0x000F6B69
	// (set) Token: 0x06002156 RID: 8534 RVA: 0x000F8771 File Offset: 0x000F6B71
	public int MySeasonScoreRank
	{
		get
		{
			return this.mSeasonScoreRank;
		}
		set
		{
			this.mSeasonScoreRank = value;
		}
	}

	// Token: 0x06002157 RID: 8535 RVA: 0x000F877A File Offset: 0x000F6B7A
	private void GeneratePublicK()
	{
		GGCloudServiceKit.pkey = PV.mk1 + PV.Decrypt(PV.mk2);
	}

	// Token: 0x06002158 RID: 8536 RVA: 0x000F8795 File Offset: 0x000F6B95
	public void PreGetFriendSysHaveNewMessage(string username)
	{
		this.GetFriendRequest(username);
	}

	// Token: 0x06002159 RID: 8537 RVA: 0x000F87A0 File Offset: 0x000F6BA0
	private int SortCompareByOnline(CSFriendInfo finfo1, CSFriendInfo finfo2)
	{
		int result = 0;
		int num;
		if (finfo1.IsOnline)
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		int num2;
		if (finfo2.IsOnline)
		{
			num2 = 1;
		}
		else
		{
			num2 = 0;
		}
		if (num > num2)
		{
			result = -1;
		}
		else if (num < num2)
		{
			result = 1;
		}
		if (num == num2)
		{
			result = finfo1.Name.CompareTo(finfo2.Name);
		}
		return result;
	}

	// Token: 0x0600215A RID: 8538 RVA: 0x000F880C File Offset: 0x000F6C0C
	private void Update()
	{
		this.mOpTimeForSortFriends += Time.deltaTime;
		if (this.mOpTimeForSortFriends > GGCloudServiceKit.OPIntervalForSortFriends)
		{
			this.mOpTimeForSortFriends = 0f;
			this.mFriendInfoList.Sort(new Comparison<CSFriendInfo>(this.SortCompareByOnline));
		}
	}

	// Token: 0x0600215B RID: 8539 RVA: 0x000F8860 File Offset: 0x000F6C60
	public void SetGDPR(bool isGDPR)
	{
		MetaData metaData = new MetaData("gdpr");
		if (isGDPR)
		{
			metaData.Set("consent", "true");
		}
		else
		{
			metaData.Set("consent", "false");
		}
		Advertisement.SetMetaData(metaData);
	}

	// Token: 0x0600215C RID: 8540 RVA: 0x000F88AC File Offset: 0x000F6CAC
	public void AutoCreateUser()
	{
		this.mUserNamePassword = new CSUserNamePassword();
		string empty = string.Empty;
		string empty2 = string.Empty;
		GGCloudServiceAdapter.mInstance.ChangeToNotAuthServiceAPIRCC();
		GGCloudServiceKit.mInstance.GetUserNameAndPassword();
	}

	// Token: 0x0600215D RID: 8541 RVA: 0x000F88EC File Offset: 0x000F6CEC
	public void QuickEnterGame()
	{
		base.StartCoroutine(GGCloudServiceKit.mInstance.LoginTimeOut(60f));
		if (GGCloudServiceCreate.mInstance.goCloudService.GetComponent<GGCloudServiceLoginProcessBar>() == null)
		{
			GGCloudServiceCreate.mInstance.goCloudService.AddComponent<GGCloudServiceLoginProcessBar>();
		}
		GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update("Connecting...", "Connecting to server", GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Progress, "Connecting to server");
		UILoginNewDirector.mInstance.AutoEnterGameProcessBar();
		this.mUserNamePassword = new CSUserNamePassword();
		string empty = string.Empty;
		string empty2 = string.Empty;
		GGCloudServiceAdapter.mInstance.ChangeToNotAuthServiceAPIRCC();
		GGCloudServiceKit.mInstance.QuickEnterGameGetUserNameAndPassword();
	}

	// Token: 0x0600215E RID: 8542 RVA: 0x000F89A4 File Offset: 0x000F6DA4
	public void CreateUser(string userName, string password)
	{
		this.tuserName = userName;
		this.tpassword = password;
		string email = Guid.NewGuid().ToString() + "@gmail.com";
		GGCloudServiceAdapter.mInstance.CreateUser(userName, password, email, new GGCloudServiceCreateUserResponse());
	}

	// Token: 0x0600215F RID: 8543 RVA: 0x000F89F0 File Offset: 0x000F6DF0
	public void DeleteAll()
	{
		try
		{
			string defaultUserName = UIUserDataController.GetDefaultUserName();
			if (!string.IsNullOrEmpty(defaultUserName))
			{
				this.DeleteRoleName(defaultUserName);
				this.DeleteSkin(defaultUserName);
				this.DeleteGameData(defaultUserName);
				this.DeleteFriend(defaultUserName);
				this.DeleteUser(defaultUserName);
			}
		}
		catch (Exception ex)
		{
			RioQerdoDebug.Log("@@@@@@@@@@@@@@: delete user fail: " + ex.ToString());
		}
	}

	// Token: 0x06002160 RID: 8544 RVA: 0x000F8A64 File Offset: 0x000F6E64
	private void DeleteRoleName(string username)
	{
		string collectionName = "UserProfileCollection";
		string key = "UserName";
		GGCloudServiceAdapter.mInstance.mStorageService.DeleteDocumentsByKeyValue(GGCloudServiceConstant.mInstance.mDBName, collectionName, key, username, new GGCloudServiceUnKnownServiceResponse());
	}

	// Token: 0x06002161 RID: 8545 RVA: 0x000F8AA0 File Offset: 0x000F6EA0
	private void DeleteSkin(string username)
	{
		GGCloudServiceAdapter.mInstance.mUploadService.RemoveAllFilesByUser(username, new GGCloudServiceUnKnownServiceResponse());
	}

	// Token: 0x06002162 RID: 8546 RVA: 0x000F8AB8 File Offset: 0x000F6EB8
	private void DeleteGameData(string username)
	{
		string mUserDataCollectionName = GGCloudServiceConstant.mInstance.mUserDataCollectionName;
		string key = "UserName";
		GGCloudServiceAdapter.mInstance.mStorageService.DeleteDocumentsByKeyValue(GGCloudServiceConstant.mInstance.mDBName, mUserDataCollectionName, key, username, new GGCloudServiceUnKnownServiceResponse());
	}

	// Token: 0x06002163 RID: 8547 RVA: 0x000F8AFC File Offset: 0x000F6EFC
	private void DeleteFriend(string username)
	{
		string collectionName = "FriendCollection";
		string key = "UserName";
		GGCloudServiceAdapter.mInstance.mStorageService.DeleteDocumentsByKeyValue(GGCloudServiceConstant.mInstance.mDBName, collectionName, key, username, new GGCloudServiceUnKnownServiceResponse());
	}

	// Token: 0x06002164 RID: 8548 RVA: 0x000F8B38 File Offset: 0x000F6F38
	public void CreateUserAction_CreateUserException(Exception ex)
	{
		string text = ex.ToString();
		if (text.Contains("2001"))
		{
			if (this.CSErrorEvent != null)
			{
				this.CSErrorEvent(GGCloudServiceErrorEventType.CreateUserNameExist);
			}
		}
		else if (text.Contains("WebException") && this.CSErrorEvent != null)
		{
			this.CSErrorEvent(GGCloudServiceErrorEventType.CreateUserNetworkNotConnect);
		}
	}

	// Token: 0x06002165 RID: 8549 RVA: 0x000F8BA4 File Offset: 0x000F6FA4
	public void CreateUserAction_CreateUserOK()
	{
		GGCloudServiceKit.mInstance.mUserNamePassword.UserName = this.tuserName;
		GGCloudServiceKit.mInstance.mUserNamePassword.Password = this.tpassword;
		if (this.CSErrorEvent != null)
		{
			this.CSErrorEvent(GGCloudServiceErrorEventType.CreateUserSuccess);
		}
		GGCloudServiceKit.mInstance.mNewUserName = this.tuserName.ToLower();
	}

	// Token: 0x06002166 RID: 8550 RVA: 0x000F8C08 File Offset: 0x000F7008
	public void CreateRoleName(string userName, string roleName)
	{
		string collectionName = "UserProfileCollection";
		string key = "RoleName";
		RioQerdoDebug.Log("FindDocumentByKeyValue rolename");
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, collectionName, key, roleName, new GGCloudServiceAllUserDataResponse(AllUserDataType.IsRoleNameExist, userName, roleName));
	}

	// Token: 0x06002167 RID: 8551 RVA: 0x000F8C51 File Offset: 0x000F7051
	public void DisplayRoleNameToCreateIsExist()
	{
		if (this.CSErrorEvent != null)
		{
			this.CSErrorEvent(GGCloudServiceErrorEventType.CreateRoleNameExist);
		}
	}

	// Token: 0x06002168 RID: 8552 RVA: 0x000F8C6B File Offset: 0x000F706B
	public void DisplayCreateRoleNetworkNotConnect()
	{
		if (this.CSErrorEvent != null)
		{
			this.CSErrorEvent(GGCloudServiceErrorEventType.CreateRoleNetworkNotConnect);
		}
	}

	// Token: 0x06002169 RID: 8553 RVA: 0x000F8C88 File Offset: 0x000F7088
	public void InsertRoleNameForUser(string userName, string roleName)
	{
		string collectionName = "UserProfileCollection";
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("UserName", userName);
		jsonclass.Add("RoleName", roleName);
		GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, collectionName, jsonclass, new GGCloudServiceAllUserDataResponse(AllUserDataType.InsertRoleName));
	}

	// Token: 0x0600216A RID: 8554 RVA: 0x000F8CE4 File Offset: 0x000F70E4
	public void Authenticate(string userName, string password)
	{
		GGCloudServiceAdapter.mInstance.Authenticate(userName, password);
	}

	// Token: 0x0600216B RID: 8555 RVA: 0x000F8CF2 File Offset: 0x000F70F2
	public void Logout()
	{
		GGCloudServiceAdapter.mInstance.Logout();
	}

	// Token: 0x0600216C RID: 8556 RVA: 0x000F8CFE File Offset: 0x000F70FE
	public void SendFriendRequest(string userName, string buddyName, string message)
	{
		GGCloudServiceAdapter.mInstance.SendFriendRequest(userName, buddyName, message);
	}

	// Token: 0x0600216D RID: 8557 RVA: 0x000F8D0D File Offset: 0x000F710D
	public void GetFriendRequest(string userName)
	{
		GGCloudServiceAdapter.mInstance.GetFriendRequest(userName);
	}

	// Token: 0x0600216E RID: 8558 RVA: 0x000F8D1A File Offset: 0x000F711A
	public void AcceptFriendRequest(string userName, string buddyName)
	{
		GGCloudServiceAdapter.mInstance.AcceptFriendRequest(userName, buddyName);
	}

	// Token: 0x0600216F RID: 8559 RVA: 0x000F8D28 File Offset: 0x000F7128
	public void AcceptFriendRequest_CallBack(string userName, string buddyName)
	{
		this.UpdateUserNameRoleNameDic(userName);
		this.mFriendRequestList.Remove(buddyName);
		if (this.mFriendRequestKeyUserNameValueRoleNameDic.ContainsKey(buddyName))
		{
			this.mFriendRequestKeyUserNameValueRoleNameDic.Remove(buddyName);
		}
	}

	// Token: 0x06002170 RID: 8560 RVA: 0x000F8D5C File Offset: 0x000F715C
	public void RejectFriendRequest(string userName, string buddyName)
	{
		GGCloudServiceAdapter.mInstance.RejectFriendRequest(userName, buddyName);
		this.mFriendRequestList.Remove(buddyName);
		if (this.mFriendRequestKeyUserNameValueRoleNameDic.ContainsKey(buddyName))
		{
			this.mFriendRequestKeyUserNameValueRoleNameDic.Remove(buddyName);
		}
	}

	// Token: 0x06002171 RID: 8561 RVA: 0x000F8D98 File Offset: 0x000F7198
	public void StoreIAPRecord(string purchasedItem, bool bReceiptValidate, string receipt, string resultReceiptValidate)
	{
		string defaultUserName = UIUserDataController.GetDefaultUserName();
		string defaultRoleName = UIUserDataController.GetDefaultRoleName();
		CSParamter6 csparamter = new CSParamter6(defaultUserName, defaultRoleName, purchasedItem, bReceiptValidate.ToString(), receipt, resultReceiptValidate);
		Thread thread = new Thread(new ParameterizedThreadStart(this.StoreIAPRecordAction));
		thread.Start(csparamter);
	}

	// Token: 0x06002172 RID: 8562 RVA: 0x000F8DEC File Offset: 0x000F71EC
	private void StoreIAPRecordAction(object objectInfo)
	{
		CSParamter6 csparamter = (CSParamter6)objectInfo;
		string paramter = csparamter.paramter1;
		string paramter2 = csparamter.paramter2;
		string paramter3 = csparamter.paramter3;
		string paramter4 = csparamter.paramter4;
		string paramter5 = csparamter.paramter5;
		string paramter6 = csparamter.paramter6;
		string collectionName = "PRecordCollection";
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("UserName", paramter);
		jsonclass.Add("RoleName", paramter2);
		jsonclass.Add("PurchasedItem", paramter3);
		jsonclass.Add("ReceiptValidate", paramter4);
		jsonclass.Add("Receipt", paramter5);
		jsonclass.Add("ResultReceiptValidate", paramter6);
		try
		{
			GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, collectionName, jsonclass, null);
		}
		catch (Exception ex)
		{
			RioQerdoDebug.Log(ex.ToString());
		}
	}

	// Token: 0x06002173 RID: 8563 RVA: 0x000F8EF8 File Offset: 0x000F72F8
	public void GetUserNameByRoleNameAsyForAddFriendInLobby(string roleName)
	{
		string collectionName = "UserProfileCollection";
		string key = "RoleName";
		RioQerdoDebug.Log("rolename: " + roleName);
		GGCloudServiceGetUserNameByRoleNameAsyForAddFriendInLobbyResponse callBack = new GGCloudServiceGetUserNameByRoleNameAsyForAddFriendInLobbyResponse();
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, collectionName, key, roleName, callBack);
	}

	// Token: 0x06002174 RID: 8564 RVA: 0x000F8F48 File Offset: 0x000F7348
	public void GetUserNameByRoleNameAsyForAddFriendInGame(string roleName)
	{
		string collectionName = "UserProfileCollection";
		string key = "RoleName";
		RioQerdoDebug.Log("rolename: " + roleName);
		GGCloudServiceGetUserNameByRoleNameAsyForAddFriendInGameResponse callBack = new GGCloudServiceGetUserNameByRoleNameAsyForAddFriendInGameResponse();
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, collectionName, key, roleName, callBack);
	}

	// Token: 0x06002175 RID: 8565 RVA: 0x000F8F98 File Offset: 0x000F7398
	public void GetUserNameByRoleNameAsyForAcceptFriendInLobby(string roleName)
	{
		string collectionName = "UserProfileCollection";
		string key = "RoleName";
		RioQerdoDebug.Log("rolename: " + roleName);
		GGCloudServiceGetUserNameByRoleNameAsyForAcceptFriendInLobbyResponse callBack = new GGCloudServiceGetUserNameByRoleNameAsyForAcceptFriendInLobbyResponse();
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, collectionName, key, roleName, callBack);
	}

	// Token: 0x06002176 RID: 8566 RVA: 0x000F8FE8 File Offset: 0x000F73E8
	public void GetUserNameByRoleNameAsyForRefuseFriendInLobby(string roleName)
	{
		string collectionName = "UserProfileCollection";
		string key = "RoleName";
		RioQerdoDebug.Log("rolename: " + roleName);
		GGCloudServiceGetUserNameByRoleNameAsyForRefuseFriendInLobbyResponse callBack = new GGCloudServiceGetUserNameByRoleNameAsyForRefuseFriendInLobbyResponse();
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, collectionName, key, roleName, callBack);
	}

	// Token: 0x06002177 RID: 8567 RVA: 0x000F9038 File Offset: 0x000F7438
	public void GetRoleNameByUserName(string userName)
	{
		string collectionName = "UserProfileCollection";
		string key = "UserName";
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, collectionName, key, userName, new GGCloudServiceGetRoleNameByUserNameResponse());
	}

	// Token: 0x06002178 RID: 8568 RVA: 0x000F9074 File Offset: 0x000F7474
	public void SendPushToUser(string userName, string message)
	{
		GGCloudServiceAdapter.mInstance.SendPushToUser(userName, message);
	}

	// Token: 0x06002179 RID: 8569 RVA: 0x000F9082 File Offset: 0x000F7482
	public void SetBadgeNumber(string userName, int num)
	{
		GGCloudServiceAdapter.mInstance.UpdatePushBadgeforUser(userName, num);
	}

	// Token: 0x0600217A RID: 8570 RVA: 0x000F9090 File Offset: 0x000F7490
	public void SendPushMessageToAll(string message)
	{
		GGCloudServiceAdapter.mInstance.SendPushMessageToAll(message);
	}

	// Token: 0x0600217B RID: 8571 RVA: 0x000F909D File Offset: 0x000F749D
	public void BindUserNameWithDeviceToken(string tmpUserName)
	{
		GGCloudServiceAdapter.mInstance.BindUserNameWithDeviceToken(tmpUserName);
	}

	// Token: 0x0600217C RID: 8572 RVA: 0x000F90AC File Offset: 0x000F74AC
	public void LogIn(string userName, string password)
	{
		base.StartCoroutine(this.LoginTimeOut(60f));
		if (base.gameObject.GetComponent<GGCloudServiceLoginProcessBar>() == null)
		{
			base.gameObject.AddComponent<GGCloudServiceLoginProcessBar>();
		}
		if (GGCloudServiceLoginProcessBar.mInstance != null)
		{
			GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update("Authenticate...", "Authenticate the user", GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Progress, "Authenticate the user");
		}
		GGCloudServiceAdapter.mInstance.mUserService.Authenticate(userName, password, new GGCloudServiceDownloadUserDataResponse(DownloadDataType.Login));
	}

	// Token: 0x0600217D RID: 8573 RVA: 0x000F9141 File Offset: 0x000F7541
	public void RemoveLoginProcessBar()
	{
		UnityEngine.Object.Destroy(base.gameObject.GetComponent<GGCloudServiceLoginProcessBar>());
	}

	// Token: 0x0600217E RID: 8574 RVA: 0x000F9154 File Offset: 0x000F7554
	public IEnumerator LoginTimeOut(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		if (GGCloudServiceLoginProcessBar.mInstance != null)
		{
			GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("Login Time Out!", ProgressStatus.Fail);
		}
		if (this.CSErrorEvent != null)
		{
			this.CSErrorEvent(GGCloudServiceErrorEventType.LoadingPanelHide);
		}
		yield break;
	}

	// Token: 0x0600217F RID: 8575 RVA: 0x000F9176 File Offset: 0x000F7576
	public void StopCoroutineLoginTimeOut()
	{
		base.StopAllCoroutines();
	}

	// Token: 0x06002180 RID: 8576 RVA: 0x000F9180 File Offset: 0x000F7580
	public void UpdateUserNameRoleNameDic(string userName)
	{
		string collectionName = "FriendCollection";
		try
		{
			Query query = QueryBuilder.SetLoggedInUser(userName);
			GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentsByQuery(GGCloudServiceConstant.mInstance.mDBName, collectionName, query, new GGCloudServiceUpdateUserNameFindDocResponse(userName));
		}
		catch (Exception ex)
		{
			RioQerdoDebug.Log(ex.ToString());
		}
	}

	// Token: 0x06002181 RID: 8577 RVA: 0x000F91E4 File Offset: 0x000F75E4
	public void UpdateUserNameRoleNameDic_CallBack(Storage storage, string userName)
	{
		if (storage != null)
		{
			IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
			this.mUserNameRoleNameDic.Clear();
			this.mRoleNameUserNameDic.Clear();
			for (int i = 0; i < jsonDocList.Count; i++)
			{
				string jsonDoc = jsonDocList[i].GetJsonDoc();
				JObject jobject = JObject.Parse(jsonDoc);
				string text = jobject["UserName"];
				string text2 = jobject["RoleName"];
				if (!this.mUserNameRoleNameDic.ContainsKey(text))
				{
					this.mUserNameRoleNameDic.Add(text, text2);
				}
				if (!this.mRoleNameUserNameDic.ContainsKey(text2))
				{
					this.mRoleNameUserNameDic.Add(text2, text);
				}
				if (!this.mAllFriendNameList.Contains(text) && !this.mAllFriendRoleNameList.Contains(text2))
				{
					this.mAllFriendNameList.Add(text);
					this.mAllFriendRoleNameList.Add(text2);
					CSFriendInfo csfriendInfo = new CSFriendInfo();
					csfriendInfo.Name = text;
					csfriendInfo.RoleName = text2;
					csfriendInfo.IsOnline = false;
					csfriendInfo.IsInRoom = false;
					csfriendInfo.Room = string.Empty;
					csfriendInfo.messageList = new List<CSMessage>();
					this.mFriendInfoList.Add(csfriendInfo);
				}
			}
			string defaultRoleName = UIUserDataController.GetDefaultRoleName();
			if (!this.mUserNameRoleNameDic.ContainsKey(userName))
			{
				this.mUserNameRoleNameDic.Add(userName, defaultRoleName);
			}
			if (!this.mRoleNameUserNameDic.ContainsKey(defaultRoleName))
			{
				this.mRoleNameUserNameDic.Add(defaultRoleName, userName);
			}
		}
		this.mIsFriendInfoListReady = true;
	}

	// Token: 0x06002182 RID: 8578 RVA: 0x000F9384 File Offset: 0x000F7784
	public List<CSMessage> GetFriendHistoryMessage(string userName, string friendName)
	{
		Dictionary<CSHistoryMessageKey, List<CSMessage>> historyMessage = GGCloudServiceHistoryMessage.mInstance.GetHistoryMessage(userName);
		CSHistoryMessageKey key = default(CSHistoryMessageKey);
		int num = string.Compare(userName, friendName);
		if (num < 0)
		{
			key.name1 = userName;
			key.name2 = friendName;
		}
		else
		{
			key.name1 = friendName;
			key.name2 = userName;
		}
		if (historyMessage.ContainsKey(key))
		{
			return historyMessage[key];
		}
		return new List<CSMessage>();
	}

	// Token: 0x06002183 RID: 8579 RVA: 0x000F93F4 File Offset: 0x000F77F4
	public List<CSMessage> GetFriendHistoryMessage(string userName, string friendName, Dictionary<CSHistoryMessageKey, List<CSMessage>> historyMessageDic)
	{
		CSHistoryMessageKey key = default(CSHistoryMessageKey);
		int num = string.Compare(userName, friendName);
		if (num < 0)
		{
			key.name1 = userName;
			key.name2 = friendName;
		}
		else
		{
			key.name1 = friendName;
			key.name2 = userName;
		}
		if (historyMessageDic.ContainsKey(key))
		{
			return historyMessageDic[key];
		}
		return new List<CSMessage>();
	}

	// Token: 0x06002184 RID: 8580 RVA: 0x000F9456 File Offset: 0x000F7856
	public void CleanLocalNotification()
	{
	}

	// Token: 0x06002185 RID: 8581 RVA: 0x000F9458 File Offset: 0x000F7858
	public int GetServerBadgeNumber()
	{
		return 0;
	}

	// Token: 0x06002186 RID: 8582 RVA: 0x000F9468 File Offset: 0x000F7868
	public void GetAllFriendInfoList(string userName)
	{
		this.mFriendInfoList.Clear();
		int serverBadgeNumber = this.GetServerBadgeNumber();
		this.CleanLocalNotification();
		this.SetBadgeNumber(userName, 0);
		this.GetAllFriendInfoListAction(userName);
	}

	// Token: 0x06002187 RID: 8583 RVA: 0x000F949C File Offset: 0x000F789C
	public void GetAllFriendInfoListAction(string userName)
	{
		this.mFriendInfoList.Clear();
		this.mAllFriendRoleNameList.Clear();
		this.mAllFriendNameList.Clear();
		this.GetNewFriendInfo(userName);
		this.UpdateUserNameRoleNameDic(userName);
	}

	// Token: 0x06002188 RID: 8584 RVA: 0x000F94D0 File Offset: 0x000F78D0
	public void AddFriendRequest(string userName, string requestFriendName, string message)
	{
		try
		{
			this.SendFriendRequest(userName, requestFriendName, message);
		}
		catch (Exception ex)
		{
			if (this.CSErrorEvent != null)
			{
				this.CSErrorEvent(GGCloudServiceErrorEventType.AddFriendNotExist);
			}
		}
	}

	// Token: 0x06002189 RID: 8585 RVA: 0x000F9528 File Offset: 0x000F7928
	public void GetOfficialMessage(string userName)
	{
		this.GetOfficialMessageAction(userName);
	}

	// Token: 0x0600218A RID: 8586 RVA: 0x000F9534 File Offset: 0x000F7934
	public void GetOfficialMessageAction(string userName)
	{
		string collectionName = "officalmessage";
		try
		{
			GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, collectionName, "officalmessage", "officalmessage", new GGCloudServiceGetOfficialMessageFindDocResponse(userName));
		}
		catch (Exception ex)
		{
			RioQerdoDebug.Log("no system message");
		}
	}

	// Token: 0x0600218B RID: 8587 RVA: 0x000F9598 File Offset: 0x000F7998
	public void GetOfficialMessageAction_CallBack(Storage storage, string userName)
	{
		List<string> list = new List<string>();
		if (storage != null)
		{
			IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
			bool flag = false;
			for (int i = 0; i < jsonDocList.Count; i++)
			{
				string jsonDoc = jsonDocList[i].GetJsonDoc();
				JObject jobject = JObject.Parse(jsonDoc);
				string a = jobject["receiver"];
				string text = jobject["content"];
				if (a == userName || a == "all")
				{
					list.Add(text);
				}
				if (text.Contains("your friend request!"))
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.UpdateUserNameRoleNameDic(userName);
			}
		}
		this.mOfficalMessageList.Clear();
		this.mOfficalMessageList = list;
	}

	// Token: 0x0600218C RID: 8588 RVA: 0x000F9666 File Offset: 0x000F7A66
	public void GetNewFriendInfo(string userName)
	{
		this.GetNewFriendInfoAction(userName);
	}

	// Token: 0x0600218D RID: 8589 RVA: 0x000F9670 File Offset: 0x000F7A70
	public void GetNewFriendInfoAction(string userName)
	{
		string mNewFriendInfoCollectionName = GGCloudServiceConstant.mInstance.mNewFriendInfoCollectionName;
		string key = "UserName";
		try
		{
			GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, mNewFriendInfoCollectionName, key, userName, new GGCloudServiceGetNewFriendInfoFindDocResponse());
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x0600218E RID: 8590 RVA: 0x000F96CC File Offset: 0x000F7ACC
	public void GetNewFriendInfoAction_CallBack1(Storage storage)
	{
		string mNewFriendInfoCollectionName = GGCloudServiceConstant.mInstance.mNewFriendInfoCollectionName;
		IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
		for (int i = 0; i < jsonDocList.Count; i++)
		{
			string docId = jsonDocList[i].GetDocId();
			GGCloudServiceAdapter.mInstance.mStorageService.DeleteDocumentById(GGCloudServiceConstant.mInstance.mDBName, mNewFriendInfoCollectionName, docId, new GGCloudServiceUnKnownServiceResponse());
		}
		IList<Storage.JSONDocument> jsonDocList2 = storage.GetJsonDocList();
		string text = string.Empty;
		for (int j = 0; j < jsonDocList2.Count; j++)
		{
			string jsonDoc = jsonDocList2[j].GetJsonDoc();
			JObject jobject = JObject.Parse(jsonDoc);
			text = jobject["NewFriendUserName"];
		}
		string collectionName = "UserProfileCollection";
		string key = "UserName";
		string value = text;
		try
		{
			GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, collectionName, key, value, new GGCloudServiceFindUserProfileResponse(text));
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x0600218F RID: 8591 RVA: 0x000F97DC File Offset: 0x000F7BDC
	public void GetNewFriendInfoAction_CallBack2(Storage storage, string newFriendUserName)
	{
		IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
		string s = string.Empty;
		for (int i = 0; i < jsonDocList.Count; i++)
		{
			string jsonDoc = jsonDocList[i].GetJsonDoc();
			JObject jobject = JObject.Parse(jsonDoc);
			s = jobject["RoleName"];
		}
		string collectionName = "FriendCollection";
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("UserName", newFriendUserName);
		jsonclass.Add("RoleName", s);
		try
		{
			GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, collectionName, jsonclass, new GGCloudServiceUnKnownServiceResponse());
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06002190 RID: 8592 RVA: 0x000F98A4 File Offset: 0x000F7CA4
	public void GetUserNameAndPassword()
	{
		GGCloudServiceAdapter.mInstance.GetUserNameAndPassword();
	}

	// Token: 0x06002191 RID: 8593 RVA: 0x000F98B0 File Offset: 0x000F7CB0
	public void QuickEnterGameGetUserNameAndPassword()
	{
		GGCloudServiceAdapter.mInstance.QuickEnterGameGetUserNameAndPassword();
	}

	// Token: 0x06002192 RID: 8594 RVA: 0x000F98BC File Offset: 0x000F7CBC
	public void LUser(string userName)
	{
		GGCloudServiceAdapter.mInstance.LUser(userName);
	}

	// Token: 0x06002193 RID: 8595 RVA: 0x000F98CC File Offset: 0x000F7CCC
	public void CreateCacheUserName()
	{
		List<JSONClass> list = new List<JSONClass>();
		string collectionName = "CacheUserNameCollection";
		string text = string.Empty;
		string text2 = string.Empty;
		string email = string.Empty;
		RioQerdoDebug.Log("1111");
		for (int i = 102900000; i <= 103900000; i++)
		{
			try
			{
				text = i.ToString();
				text2 = UnityEngine.Random.Range(10000000, 99999999).ToString();
				email = Guid.NewGuid().ToString() + "@gmail.com";
				GGCloudServiceAdapter.mInstance.CreateUser(text, text2, email, new GGCloudServiceUnKnownServiceResponse());
				list.Add(new JSONClass
				{
					{
						"UserName",
						text
					},
					{
						"Password",
						text2
					}
				});
				if (i % 100 == 0)
				{
					GGCloudServiceAdapter.mInstance.ChangeToNotAuthServiceAPIRData();
					foreach (JSONClass json in list)
					{
						try
						{
							GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, collectionName, json, null);
						}
						catch (Exception ex)
						{
						}
					}
					list.Clear();
					GGCloudServiceAdapter.mInstance.ChangeToAuthServiceAPI();
				}
			}
			catch (Exception ex2)
			{
				RioQerdoDebug.Log(ex2.ToString());
			}
		}
	}

	// Token: 0x06002194 RID: 8596 RVA: 0x000F9A9C File Offset: 0x000F7E9C
	public void CreateCacheEmail()
	{
	}

	// Token: 0x06002195 RID: 8597 RVA: 0x000F9AA0 File Offset: 0x000F7EA0
	public void SendOfficialMessageToAll(string message)
	{
		string collectionName = "officalmessage";
		this.SendPushMessageToAll(message);
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("officalmessage", "officalmessage");
		jsonclass.Add("receiver", "all");
		jsonclass.Add("content", message);
		GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, collectionName, jsonclass, null);
	}

	// Token: 0x06002196 RID: 8598 RVA: 0x000F9B18 File Offset: 0x000F7F18
	public void SendPublicNotice(string subject, string message)
	{
		string mPublicNotice = GGCloudServiceConstant.mInstance.mPublicNotice;
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("publicnotice", "publicnotice");
		jsonclass.Add("subject", subject);
		jsonclass.Add("content", message);
		GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, mPublicNotice, jsonclass, null);
	}

	// Token: 0x06002197 RID: 8599 RVA: 0x000F9B8C File Offset: 0x000F7F8C
	public void InsertAcceptRejectFriendMessage(string userName, string message)
	{
		string collectionName = "AcceptRejectMessageCollection";
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("receiver", userName);
		jsonclass.Add("content", message);
		GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, collectionName, jsonclass, null);
	}

	// Token: 0x06002198 RID: 8600 RVA: 0x000F9BE4 File Offset: 0x000F7FE4
	public void SendOfficialMessageToUser(string userName, string message)
	{
		string collectionName = "officalmessage";
		JSONClass jsonclass = new JSONClass();
		jsonclass.Add("officalmessage", "officalmessage");
		jsonclass.Add("receiver", userName);
		jsonclass.Add("content", message);
		GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, collectionName, jsonclass, null);
	}

	// Token: 0x06002199 RID: 8601 RVA: 0x000F9C50 File Offset: 0x000F8050
	public List<CSAccused> GetProsecutorList()
	{
		return null;
	}

	// Token: 0x0600219A RID: 8602 RVA: 0x000F9C53 File Offset: 0x000F8053
	public List<CSAccused> GetAccusedList()
	{
		return null;
	}

	// Token: 0x0600219B RID: 8603 RVA: 0x000F9C56 File Offset: 0x000F8056
	public void LoadLogInScene()
	{
		if (this.CSErrorEvent != null)
		{
			this.CSErrorEvent(GGCloudServiceErrorEventType.LoadLogInScene);
		}
	}

	// Token: 0x0600219C RID: 8604 RVA: 0x000F9C70 File Offset: 0x000F8070
	public void IsUserOnline(string userName)
	{
		string mUserDataCollectionName = GGCloudServiceConstant.mInstance.mUserDataCollectionName;
		string key = "UserName";
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, mUserDataCollectionName, key, userName, new GGCloudServiceAllUserDataResponse(AllUserDataType.IsUserOnline));
	}

	// Token: 0x0600219D RID: 8605 RVA: 0x000F9CB4 File Offset: 0x000F80B4
	public void SaveUserScore()
	{
		string text = "LeaderBoard";
		double num = (double)GrowthManagerKit.GetCharacterExpTotal();
		if (num > GGCloudServiceKit.mInstance.mExpThreshold && num > 1000.0)
		{
			GGCloudServiceAdapter.mInstance.mScoreBoardService.GetLastScoreByUser(text, UIUserDataController.GetDefaultUserName(), new GGCloudServiceGetLastScoreByUserResponse(text, num));
		}
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x000F9D0C File Offset: 0x000F810C
	public void SaveUserSeasonScore(string gameName, string userName)
	{
		double num = (double)GrowthManagerKit.GetCareerStatValuesOfSeason().seasonScore;
		if (num >= this.mSeasonScoreThreshold)
		{
			GGCloudServiceAdapter.mInstance.mScoreBoardService.GetLastScoreByUser(gameName, userName, new GGCloudServiceGetLastSeasonScoreByUserResponse(num, gameName, userName));
		}
	}

	// Token: 0x0600219F RID: 8607 RVA: 0x000F9D4C File Offset: 0x000F814C
	public void GetCustomLeaderBoardFromCloud()
	{
		GGCloudServiceUserGetScoreBoardStorageResponse callBack = new GGCloudServiceUserGetScoreBoardStorageResponse();
		GGCloudServiceAdapter.mInstance.mStorageService.FindAllDocuments("PLAYER", "PublicDataCollection", 25, 0, callBack);
	}

	// Token: 0x060021A0 RID: 8608 RVA: 0x000F9D7C File Offset: 0x000F817C
	public void SaveOrEditScore(string userName)
	{
		GGCloudServiceScoreBoardSaveOrEditScoreResponse callBack = new GGCloudServiceScoreBoardSaveOrEditScoreResponse();
		GGCloudServiceAdapter.mInstance.mScoreBoardService.GetLastScoreByUser("LeaderBoard", userName, callBack);
	}

	// Token: 0x060021A1 RID: 8609 RVA: 0x000F9DA8 File Offset: 0x000F81A8
	public void SaveScore(double gameScore)
	{
		GGCloudServiceScoreBoardSaveScoreResponse callBack = new GGCloudServiceScoreBoardSaveScoreResponse();
		GGCloudServiceAdapter.mInstance.mScoreBoardService.SaveUserScore("LeaderBoard", UIUserDataController.GetDefaultUserName(), gameScore, callBack);
	}

	// Token: 0x060021A2 RID: 8610 RVA: 0x000F9DD8 File Offset: 0x000F81D8
	public void EditScoreValueById(string scoreID, double gameScore)
	{
		GGCloudServiceScoreBoardEditScoreValueByIdResponse callBack = new GGCloudServiceScoreBoardEditScoreValueByIdResponse();
		GGCloudServiceAdapter.mInstance.mScoreBoardService.EditScoreValueById(scoreID, gameScore, callBack);
	}

	// Token: 0x060021A3 RID: 8611 RVA: 0x000F9E00 File Offset: 0x000F8200
	public void GetSlotTopPrize(int prizeNum)
	{
		if (!this.mGetTopPrize)
		{
			string collectionName = "SlotTopPrizeCollection" + GameVersionController.GameVersion;
			GGCloudServiceGetAllTopPrizeCountResponse callBack = new GGCloudServiceGetAllTopPrizeCountResponse(prizeNum);
			GGCloudServiceAdapter.mInstance.mStorageService.FindAllDocumentsCount(GGCloudServiceConstant.mInstance.mDBName, collectionName, callBack);
		}
		else if (this.CSErrorEvent != null)
		{
			this.CSErrorEvent(GGCloudServiceErrorEventType.SlotTopPrizeFetch);
		}
	}

	// Token: 0x060021A4 RID: 8612 RVA: 0x000F9E68 File Offset: 0x000F8268
	public void UploadSlotTopPrize(string prize, string prizeinfo)
	{
		string defaultRoleName = UIUserDataController.GetDefaultRoleName();
		GGCloudServiceAdapter.mInstance.UploadSlotTopPrize(defaultRoleName, prize, prizeinfo);
	}

	// Token: 0x060021A5 RID: 8613 RVA: 0x000F9E88 File Offset: 0x000F8288
	public void GetMySeasonRank(string gamename, string userName)
	{
		GGCloudServiceAdapter.mInstance.GetMySeasonRank(gamename, userName, new GGCloudServiceOurSeasonRankResponse());
	}

	// Token: 0x060021A6 RID: 8614 RVA: 0x000F9E9C File Offset: 0x000F829C
	public void AddBlackList()
	{
		string defaultUserName = UIUserDataController.GetDefaultUserName();
		string defaultRoleName = UIUserDataController.GetDefaultRoleName();
		GGCloudServiceAdapter.mInstance.AddBlackList(defaultUserName, defaultRoleName);
	}

	// Token: 0x060021A7 RID: 8615 RVA: 0x000F9EC4 File Offset: 0x000F82C4
	public void AddBlackListToDelete(string reason)
	{
		string defaultUserName = UIUserDataController.GetDefaultUserName();
		string defaultRoleName = UIUserDataController.GetDefaultRoleName();
		GGCloudServiceAdapter.mInstance.AddBlackListToDelete(defaultUserName, defaultRoleName, reason);
	}

	// Token: 0x060021A8 RID: 8616 RVA: 0x000F9EEA File Offset: 0x000F82EA
	public void DeleteUser(string username)
	{
		GGCloudServiceAdapter.mInstance.mUserService.DeleteUser(username, new GGCloudServiceUnKnownServiceResponse());
	}

	// Token: 0x0400220E RID: 8718
	public static GGCloudServiceKit mInstance;

	// Token: 0x0400220F RID: 8719
	public static string EK = "yeah";

	// Token: 0x04002210 RID: 8720
	public Dictionary<string, string> mUserNameRoleNameDic = new Dictionary<string, string>();

	// Token: 0x04002211 RID: 8721
	public Dictionary<string, string> mRoleNameUserNameDic = new Dictionary<string, string>();

	// Token: 0x04002212 RID: 8722
	public List<string> mAllFriendNameList = new List<string>();

	// Token: 0x04002213 RID: 8723
	public List<string> mAllFriendRoleNameList = new List<string>();

	// Token: 0x04002214 RID: 8724
	public List<CSFriendInfo> mFriendInfoList = new List<CSFriendInfo>();

	// Token: 0x04002215 RID: 8725
	public bool mIsFriendInfoListReady;

	// Token: 0x04002216 RID: 8726
	public List<CSMessage> mNewMessageList = new List<CSMessage>();

	// Token: 0x04002217 RID: 8727
	public List<string> mOfficalMessageList = new List<string>();

	// Token: 0x04002218 RID: 8728
	public List<string> mFriendRequestList = new List<string>();

	// Token: 0x04002219 RID: 8729
	public Dictionary<string, string> mFriendRequestKeyUserNameValueRoleNameDic = new Dictionary<string, string>();

	// Token: 0x0400221A RID: 8730
	public List<CSLeaderBoardInfo> mExpLeaderBoardInfoList = new List<CSLeaderBoardInfo>();

	// Token: 0x0400221B RID: 8731
	public List<CSLeaderBoardInfo> mTotalKillNumLeaderBoardInfoList = new List<CSLeaderBoardInfo>();

	// Token: 0x0400221C RID: 8732
	public double mExpThreshold = 1.0;

	// Token: 0x0400221D RID: 8733
	public List<CSSeasonScoreBoardInfo> mSeasonScoreBoardInfoList = new List<CSSeasonScoreBoardInfo>();

	// Token: 0x0400221E RID: 8734
	private int mSeasonScoreRank;

	// Token: 0x0400221F RID: 8735
	public double mSeasonScoreThreshold = -0.10000000149011612;

	// Token: 0x04002220 RID: 8736
	public CSSeasonExtraInfo mSeasonExtraInfo = new CSSeasonExtraInfo();

	// Token: 0x04002221 RID: 8737
	private double mCurrentAllowance = 1.0;

	// Token: 0x04002222 RID: 8738
	public bool mGetTopPrize;

	// Token: 0x04002223 RID: 8739
	public List<CSSlotTopPrizeInfo> mTopPrizeList = new List<CSSlotTopPrizeInfo>();

	// Token: 0x04002224 RID: 8740
	public List<string> mHadSendFriendRequestRoleName = new List<string>();

	// Token: 0x04002226 RID: 8742
	private float mOpTimeForFindFriends;

	// Token: 0x04002227 RID: 8743
	private static float OPIntervalForFindFriends = 10f;

	// Token: 0x04002228 RID: 8744
	private float mOpTimeForUpdateStatus;

	// Token: 0x04002229 RID: 8745
	private static float OPIntervalForUpdateStatus = 3f;

	// Token: 0x0400222A RID: 8746
	private float mOpTimeForSortFriends;

	// Token: 0x0400222B RID: 8747
	private static float OPIntervalForSortFriends = 2f;

	// Token: 0x0400222C RID: 8748
	public CSUserNamePassword mUserNamePassword = new CSUserNamePassword();

	// Token: 0x0400222D RID: 8749
	public bool bHuntingModeOpen = true;

	// Token: 0x0400222E RID: 8750
	public bool bADPromotionOpen;

	// Token: 0x0400222F RID: 8751
	public string mNewUserName = string.Empty;

	// Token: 0x04002230 RID: 8752
	public string mLoginException = string.Empty;

	// Token: 0x04002231 RID: 8753
	public static string pkey = "G3TVJ9aqwoe9YkZHZ0+7sfS4gaEhLuCxHVcqTel+IW0pAt5299WTchn1DBA6A/irmc9Gt80xuJ6DCCyyER0nee9lij7Jh+6nV44X+hy5qrRdCcrFti1ehQrVbbxHgNmnR6RKBteXY3q0Mqc7WgyA33rmYi1/";

	// Token: 0x04002232 RID: 8754
	private string tuserName = string.Empty;

	// Token: 0x04002233 RID: 8755
	private string tpassword = string.Empty;
}
