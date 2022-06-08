using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using com.shephertz.app42.paas.sdk.csharp.storage;
using GOG.Utility;
using SimpleJSON;
using SkinEditor;
using UnityEngine;

// Token: 0x020001CE RID: 462
public class ACTUserDataManager : MonoBehaviour
{
	// Token: 0x06000C43 RID: 3139 RVA: 0x0005B5AB File Offset: 0x000599AB
	private void OnApplicationQuit()
	{
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x0005B5AD File Offset: 0x000599AD
	private void OnApplicationFocus(bool pauseStatus)
	{
		if (pauseStatus)
		{
		}
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x0005B5B8 File Offset: 0x000599B8
	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			bool flag = true;
			if (UIGameStoreDirector.mInstance != null && UIGameStoreDirector.mInstance.storeNode.activeSelf && ACTUserDataManager.IsBuyingInGame)
			{
				flag = false;
			}
			if (UINewStoreBasicWindowDirector.mInstance != null && UINewStoreBasicWindowDirector.mInstance.Windows[4].activeSelf)
			{
				flag = false;
			}
			if (flag)
			{
				if (ACTUserDataManager.mInstance != null && Application.loadedLevelName != "UILogin")
				{
					GOGPlayerPrefabs.SetInt("IsLastPlayCrashed", 0);
					GOGPlayerPrefabs.SetString("LastPlayCrashedDeviceID", string.Empty);
					ACTUserDataManager.mInstance.UploadDataSwitchScene(true, "MainMenu");
				}
				if (GGNetworkAdapter.mInstance != null)
				{
					GGNetworkAdapter.mInstance.DisconnectPhotonNetwork();
				}
				if (GGNetworkChatSys.mInstance != null)
				{
					GGNetworkChatSys.mInstance.DisconnectChatSys();
				}
			}
		}
		else
		{
			bool flag2 = true;
			if (UIGameStoreDirector.mInstance != null && UIGameStoreDirector.mInstance.storeNode.activeSelf && ACTUserDataManager.IsBuyingInGame)
			{
				flag2 = false;
			}
			if (UINewStoreBasicWindowDirector.mInstance != null && UINewStoreBasicWindowDirector.mInstance.Windows[4].activeSelf)
			{
				flag2 = false;
			}
			if (flag2 && Application.loadedLevelName != "UILogin")
			{
				GGCloudServiceKit.mInstance.IsUserOnline(UIUserDataController.GetDefaultUserName());
			}
		}
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x0005B73D File Offset: 0x00059B3D
	public static void OnApplicationEnterForeground()
	{
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x0005B73F File Offset: 0x00059B3F
	public static void OnApplicationEnterBackground()
	{
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x0005B741 File Offset: 0x00059B41
	private void OnDestroy()
	{
		GOGPlayerPrefabs.SetInt("IsLastPlayCrashed", 0);
		GOGPlayerPrefabs.SetString("LastPlayCrashedDeviceID", string.Empty);
		ACTUserDataManager.mInstance = null;
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x0005B763 File Offset: 0x00059B63
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
		ACTUserDataManager.mInstance = this;
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x0005B771 File Offset: 0x00059B71
	private void Start()
	{
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x0005B774 File Offset: 0x00059B74
	public void UserIsOnlineOrNot(Storage storage)
	{
		if (storage != null)
		{
			IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
			int num = 0;
			if (num < jsonDocList.Count)
			{
				string jsonDoc = jsonDocList[num].GetJsonDoc();
				JObject jobject = JObject.Parse(jsonDoc);
				JSONClass asObject = jobject.AsObject;
				Dictionary<string, JObject> dictionary = new Dictionary<string, JObject>();
				dictionary = asObject.m_Dict;
				if (dictionary.ContainsKey("#1#_IsInLogin"))
				{
					string a = dictionary["#1#_IsInLogin"];
					string a2 = dictionary["#2#_GOGLastDeviceID"];
					if (a == "0")
					{
						if (UISceneManager.mInstance != null)
						{
							Debug.Log("success stronline 0: LoadLevel(MainMenu)");
							if (Application.loadedLevelName != "MainMenu")
							{
								UISceneManager.mInstance.LoadLevel("MainMenu");
							}
							GOGPlayerPrefabs.SetInt("IsLastPlayCrashed", 1);
							GOGPlayerPrefabs.SetString("LastPlayCrashedDeviceID", SystemInfo.deviceUniqueIdentifier);
							GOGPlayerPrefabs.Save();
							GOGPlayerPrefabs.SetInt("IsInLogin", 1);
							GOGPlayerPrefabs.SetString("GOGLastDeviceID", SystemInfo.deviceUniqueIdentifier);
							this.UploadDataSwitchScene(false, string.Empty);
						}
					}
					else if (a == "1")
					{
						if (a2 == SystemInfo.deviceUniqueIdentifier)
						{
							if (UISceneManager.mInstance != null)
							{
								Debug.Log("success stronline 0: LoadLevel(MainMenu)");
								if (Application.loadedLevelName != "MainMenu")
								{
									UISceneManager.mInstance.LoadLevel("MainMenu");
								}
								GOGPlayerPrefabs.SetInt("IsLastPlayCrashed", 1);
								GOGPlayerPrefabs.SetString("LastPlayCrashedDeviceID", SystemInfo.deviceUniqueIdentifier);
								GOGPlayerPrefabs.Save();
								GOGPlayerPrefabs.SetInt("IsInLogin", 1);
								GOGPlayerPrefabs.SetString("GOGLastDeviceID", SystemInfo.deviceUniqueIdentifier);
								this.UploadDataSwitchScene(false, string.Empty);
							}
						}
						else if (UISceneManager.mInstance != null)
						{
							Debug.Log("success stronline 1: LoadLevel(UILogin)");
							UISceneManager.mInstance.LoadLevel("UILogin");
						}
					}
				}
			}
		}
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x0005B977 File Offset: 0x00059D77
	public void Reset()
	{
		this.mCloudUserDataExist = false;
		this.mIsHaveRoleName = false;
		this.mRoleName = string.Empty;
		this.mCurServerTime = string.Empty;
		this.mClickLogOut = false;
		this.mSaveScore = false;
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x0005B9AC File Offset: 0x00059DAC
	public void UploadDataSwitchScene(bool isLogout, string scenename)
	{
		if (isLogout)
		{
			try
			{
				GOGPlayerPrefabs.SetInt("IsInLogin", 0);
				this.dataCachetmp.Clear();
				Debug.Log("@@@@@@@@: UploadDataSwitchScene dataCachetmp begin add ");
				foreach (KeyValuePair<string, object> keyValuePair in this.dataCache)
				{
					this.dataCachetmp.Add(keyValuePair.Key, keyValuePair.Value);
				}
				Debug.Log("@@@@@@@@: UploadDataSwitchScene dataCachetmp end add ");
				GGCloudServiceAdapter.mInstance.UploadUserData(this.dataCachetmp, ACTUserDataManager.curLoginUserName);
				Debug.Log("@@@@@@@@: UploadUserData end ");
				if (Application.loadedLevelName != "MainMenu" || ACTUserDataManager.mInstance.mClickLogOut)
				{
					Application.LoadLevel(scenename);
				}
			}
			catch (Exception ex)
			{
				Debug.Log("@@@@@@@@: UploadDataSwitchScene " + ex.ToString());
			}
		}
		else
		{
			this.UploadDataAction();
		}
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0005BACC File Offset: 0x00059ECC
	public void UploadData(bool isLogout)
	{
		if (isLogout)
		{
			GOGPlayerPrefabs.SetInt("IsInLogin", 0);
			this.dataCachetmp.Clear();
			foreach (KeyValuePair<string, object> keyValuePair in this.dataCache)
			{
				this.dataCachetmp.Add(keyValuePair.Key, keyValuePair.Value);
			}
			double num = (double)GrowthManagerKit.GetCharacterExpTotal();
			if (num > GGCloudServiceKit.mInstance.mExpThreshold)
			{
				this.dataCachetmp.Add("savescore", "true");
			}
			GGCloudServiceAdapter.mInstance.UploadUserData(this.dataCachetmp, ACTUserDataManager.curLoginUserName);
			GGCloudServiceKit.mInstance.LoadLogInScene();
		}
		else
		{
			this.UploadDataAction();
		}
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x0005BBAC File Offset: 0x00059FAC
	public void UploadDataAction()
	{
		this.dataCachetmp.Clear();
		string text = ACTUserDataManager.curLoginUserName;
		string mUserDataCollectionName = GGCloudServiceConstant.mInstance.mUserDataCollectionName;
		string key = "UserName";
		string value = text;
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, mUserDataCollectionName, key, value, new GGCloudServiceUploadDataActionResponse(UploadDataActionType.FindDoc));
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0005BC04 File Offset: 0x0005A004
	public void UploadDataActionSeq_InsertDoc()
	{
		string text = ACTUserDataManager.curLoginUserName;
		string mUserDataCollectionName = GGCloudServiceConstant.mInstance.mUserDataCollectionName;
		string text2 = "UserName";
		string text3 = text;
		JSONClass jsonclass = new JSONClass();
		foreach (KeyValuePair<string, object> keyValuePair in this.dataCache)
		{
			string key = keyValuePair.Key;
			string s = (string)keyValuePair.Value;
			jsonclass.Add(key, s);
		}
		this.dataCache.Add(text2, text3);
		jsonclass.Add(text2, text3);
		GGCloudServiceAdapter.mInstance.mStorageService.InsertJSONDocument(GGCloudServiceConstant.mInstance.mDBName, mUserDataCollectionName, jsonclass, new GGCloudServiceUploadDataActionResponse(UploadDataActionType.InsertDoc));
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x0005BCE0 File Offset: 0x0005A0E0
	public void UploadDataActionSeq_UpdateDoc()
	{
		this.dataCachetmp.Clear();
		string text = ACTUserDataManager.curLoginUserName;
		string mUserDataCollectionName = GGCloudServiceConstant.mInstance.mUserDataCollectionName;
		string key = "UserName";
		string value = text;
		try
		{
			foreach (KeyValuePair<string, object> keyValuePair in this.dataCache)
			{
				if (keyValuePair.Value == string.Empty)
				{
					this.dataCachetmp.Add(keyValuePair.Key, GOGPlayerPrefabs.DATA_TAG_STRING_NULL);
				}
				else
				{
					this.dataCachetmp.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			if (!this.dataCachetmp.ContainsKey("UserName"))
			{
				this.dataCachetmp.Add(key, value);
			}
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
			return;
		}
		GGCloudServiceAdapter.mInstance.mStorageService.UpdateDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, mUserDataCollectionName, key, value, this.dataCachetmp, new GGCloudServiceUploadDataActionResponse(UploadDataActionType.UpdateDoc));
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x0005BE10 File Offset: 0x0005A210
	private bool IsNotVailidLogOut()
	{
		if (this.dataCache.ContainsKey(GOGPlayerPrefabs.DATA_TAG_STRING + "_CurrentLoginDateTime"))
		{
			string s = this.dataCache[GOGPlayerPrefabs.DATA_TAG_STRING + "_CurrentLoginDateTime"].ToString();
			if (this.mCurServerTime != string.Empty)
			{
				try
				{
					DateTime d = DateTime.ParseExact(s, "yyyyMMddHHmmss", null);
					DateTime d2 = DateTime.ParseExact(this.mCurServerTime, "yyyyMMddHHmmss", null);
					double totalHours = (d2 - d).TotalHours;
					Debug.Log("spanHours: " + totalHours);
					if (totalHours >= 4.0)
					{
						return true;
					}
					return false;
				}
				catch (Exception ex)
				{
					Debug.Log(ex.ToString());
				}
				return false;
			}
		}
		return false;
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x0005BEFC File Offset: 0x0005A2FC
	private bool IsInLogin()
	{
		bool result = false;
		if (this.dataCache.ContainsKey(GOGPlayerPrefabs.DATA_TAG_INT + "_IsInLogin"))
		{
			string s = this.dataCache[GOGPlayerPrefabs.DATA_TAG_INT + "_IsInLogin"].ToString();
			if (int.Parse(s) == 1)
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0005BF5C File Offset: 0x0005A35C
	public void DownloadData()
	{
		this.dataCache.Clear();
		string mUserDataCollectionName = GGCloudServiceConstant.mInstance.mUserDataCollectionName;
		string text = ACTUserDataManager.curLoginUserName;
		string key = "UserName";
		string value = text;
		JSONClass jsonBody = new JSONClass();
		GGCloudServiceAdapter.mInstance.mCustomCodeService.RunJavaCode("GetServerTime24", jsonBody, new GGCloudServiceDownloadUserDataResponse(DownloadDataType.GetServerTime));
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, mUserDataCollectionName, key, value, new GGCloudServiceDownloadUserDataResponse(DownloadDataType.GetUserData));
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x0005BFD4 File Offset: 0x0005A3D4
	public void LoginSucessEventSequence2()
	{
		GGCloudServiceConstant.mInstance.mUserName = ACTUserDataManager.curLoginUserName;
		UIUserDataController.SetDefaultUserName(ACTUserDataManager.curLoginUserName);
		UIUserDataController.SetDefaultPwd(ACTUserDataManager.curPassword);
		UIUserDataController.SetPreUserName(ACTUserDataManager.curLoginUserName);
		GGCloudServiceKit.mInstance.BindUserNameWithDeviceToken(UIUserDataController.GetDefaultUserName());
		GGCloudServiceKit.mInstance.StopCoroutineLoginTimeOut();
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x0005C028 File Offset: 0x0005A428
	public void DownloadDataSequence4()
	{
		UserDataController.SetPreLoginDateTime(UserDataController.GetCurLoginDateTime());
		UserDataController.SetCurLoginDateTime(this.mCurServerTime);
		this.LoginSucessEventSequence2();
		if (this.mIsHaveRoleName)
		{
			UIUserDataController.SetDefaultRoleName(this.mRoleName);
			UISceneManager.mInstance.LoadLevel("MainMenu");
			this.UploadDataSwitchScene(false, string.Empty);
		}
		else
		{
			UITipController.mInstance.HideCurTip();
			UILoginNewDirector.mInstance.loginNode.SetActive(false);
			UILoginNewDirector.mInstance.FirtLoadedNode.SetActive(false);
			UILoginNewDirector.mInstance.creatRoleNode.SetActive(true);
			UILoginNewDirector.mInstance.GenerateRoleName();
			if (UIUserDataController.GetFirstPlay() == 0)
			{
				UnityAnalyticsIntegration.mInstance.NewUserLoadedCreateRoleName();
			}
			this.UploadDataSwitchScene(false, string.Empty);
		}
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x0005C0EC File Offset: 0x0005A4EC
	public void DownloadDataSequence2NotHaveUserData()
	{
		bool flag = false;
		string a = string.Empty;
		if (this.dataCache.ContainsKey(GOGPlayerPrefabs.DATA_TAG_STRING + "_LastPlayCrashedDeviceID"))
		{
			a = this.dataCache[GOGPlayerPrefabs.DATA_TAG_STRING + "_LastPlayCrashedDeviceID"].ToString();
		}
		if (this.dataCache.ContainsKey(GOGPlayerPrefabs.DATA_TAG_INT + "_IsLastPlayCrashed"))
		{
			string s = this.dataCache[GOGPlayerPrefabs.DATA_TAG_INT + "_IsLastPlayCrashed"].ToString();
			if (int.Parse(s) == 1)
			{
				flag = true;
			}
		}
		if (this.IsInLogin())
		{
			if (!flag || !(a == SystemInfo.deviceUniqueIdentifier))
			{
				if (!this.IsNotVailidLogOut())
				{
					Debug.Log("Back");
					if (UILoginNewDirector.mInstance != null)
					{
						UILoginNewDirector.mInstance.PopErrorTipPanel("Your account login at another device last time, please logout at that device and retry!");
					}
					return;
				}
			}
		}
		bool flag2 = false;
		if (ACTUserDataManager.preLoginUserName != ACTUserDataManager.curLoginUserName)
		{
			flag2 = true;
			string @string = PlayerPrefs.GetString("GGCloudServiceDeviceTokenToBind", string.Empty);
			if (@string != string.Empty)
			{
			}
		}
		if (this.dataCache.ContainsKey(GOGPlayerPrefabs.DATA_TAG_STRING + "_GOGLastDeviceID") && this.dataCache[GOGPlayerPrefabs.DATA_TAG_STRING + "_GOGLastDeviceID"].ToString() != SystemInfo.deviceUniqueIdentifier)
		{
			flag2 = true;
		}
		if (flag2)
		{
			Debug.Log("DeleteAll");
			List<string> usedAccountList = UIUserDataController.GetUsedAccountList();
			GOGPlayerPrefabs.DeleteAll();
			SkinIOController.DeleteAllCustomSkin();
			UIUserDataController.SetCustomSkinDownloadMark(true);
			UIUserDataController.SetUsedAccountList(usedAccountList);
			usedAccountList.Clear();
			List<string> list = new List<string>(this.dataCache.Keys);
			foreach (string text in list)
			{
				string text2 = text;
				string text3 = this.dataCache[text].ToString();
				try
				{
					if (text2.Contains(GOGPlayerPrefabs.DATA_TAG_INT))
					{
						GOGPlayerPrefabs.SetInt(text2.Remove(0, GOGPlayerPrefabs.DATA_TAG_INT.Length + 1), int.Parse(text3));
					}
					else if (text2.Contains(GOGPlayerPrefabs.DATA_TAG_STRING))
					{
						if (text3.Equals(GOGPlayerPrefabs.DATA_TAG_STRING_NULL))
						{
							GOGPlayerPrefabs.SetString(text2.Remove(0, GOGPlayerPrefabs.DATA_TAG_STRING.Length + 1), string.Empty);
						}
						else
						{
							GOGPlayerPrefabs.SetString(text2.Remove(0, GOGPlayerPrefabs.DATA_TAG_STRING.Length + 1), text3);
						}
					}
					else if (text2.Contains(GOGPlayerPrefabs.DATA_TAG_FLOAT))
					{
						GOGPlayerPrefabs.SetFloat(text2.Remove(0, GOGPlayerPrefabs.DATA_TAG_FLOAT.Length + 1), float.Parse(text3));
					}
				}
				catch (Exception message)
				{
					Debug.Log(message);
				}
			}
			list.Clear();
			UIUserDataController.SetPreUserName(ACTUserDataManager.curLoginUserName);
			ACTUserDataManager.preLoginUserName = ACTUserDataManager.curLoginUserName;
			UIUserDataController.SetCurLoginUserName(ACTUserDataManager.curLoginUserName);
			UIUserDataController.SetCurLoginPassword(ACTUserDataManager.curPassword);
		}
		GOGPlayerPrefabs.SetString("sessionid", GGCloudServiceConstant.mInstance.mSessionId);
		GOGPlayerPrefabs.SetInt("IsLastPlayCrashed", 1);
		GOGPlayerPrefabs.SetString("LastPlayCrashedDeviceID", SystemInfo.deviceUniqueIdentifier);
		GOGPlayerPrefabs.Save();
		GOGPlayerPrefabs.SetInt("IsInLogin", 1);
		GOGPlayerPrefabs.SetString("GOGLastDeviceID", SystemInfo.deviceUniqueIdentifier);
		this.bUserDataReady = true;
		string key = "UserName";
		string value = ACTUserDataManager.curLoginUserName;
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, "UserProfileCollection", key, value, new GGCloudServiceDownloadUserDataResponse(DownloadDataType.IsHaveRoleName));
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x0005C4E0 File Offset: 0x0005A8E0
	public void DownloadDataSequence2(Storage storage)
	{
		string a = string.Empty;
		this.mCloudUserDataExist = true;
		if (storage != null)
		{
			IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
			for (int i = 0; i < jsonDocList.Count; i++)
			{
				string jsonDoc = jsonDocList[i].GetJsonDoc();
				JObject jobject = JObject.Parse(jsonDoc);
				JSONClass asObject = jobject.AsObject;
				Dictionary<string, JObject> dictionary = new Dictionary<string, JObject>();
				dictionary = asObject.m_Dict;
				foreach (KeyValuePair<string, JObject> keyValuePair in dictionary)
				{
					string key = keyValuePair.Key;
					string text = keyValuePair.Value;
					if (key == "#2#_GOGRoleName" && text != "#NULL#")
					{
						a = text;
					}
					if (text != string.Empty)
					{
						try
						{
							if (!this.dataCache.ContainsKey(key))
							{
								this.dataCache.Add(key, text);
							}
							else
							{
								this.dataCache[key] = text;
							}
						}
						catch (Exception ex)
						{
							Debug.Log("dataCache: " + key);
						}
					}
				}
			}
		}
		bool flag = false;
		string a2 = string.Empty;
		if (this.dataCache.ContainsKey(GOGPlayerPrefabs.DATA_TAG_STRING + "_LastPlayCrashedDeviceID"))
		{
			a2 = this.dataCache[GOGPlayerPrefabs.DATA_TAG_STRING + "_LastPlayCrashedDeviceID"].ToString();
		}
		if (this.dataCache.ContainsKey(GOGPlayerPrefabs.DATA_TAG_INT + "_IsLastPlayCrashed"))
		{
			string s = this.dataCache[GOGPlayerPrefabs.DATA_TAG_INT + "_IsLastPlayCrashed"].ToString();
			if (int.Parse(s) == 1)
			{
				flag = true;
			}
		}
		if (this.IsInLogin())
		{
			if (!flag || !(a2 == SystemInfo.deviceUniqueIdentifier))
			{
				if (!this.IsNotVailidLogOut())
				{
					if (UILoginNewDirector.mInstance != null)
					{
						UILoginNewDirector.mInstance.PopErrorTipPanel("Your account login at another device last time, please logout at that device and retry!");
					}
					return;
				}
			}
		}
		bool flag2 = false;
		if (ACTUserDataManager.preLoginUserName != ACTUserDataManager.curLoginUserName)
		{
			flag2 = true;
			string @string = PlayerPrefs.GetString("GGCloudServiceDeviceTokenToBind", string.Empty);
			if (@string != string.Empty)
			{
			}
		}
		if (this.dataCache.ContainsKey(GOGPlayerPrefabs.DATA_TAG_STRING + "_GOGLastDeviceID") && this.dataCache[GOGPlayerPrefabs.DATA_TAG_STRING + "_GOGLastDeviceID"].ToString() != SystemInfo.deviceUniqueIdentifier)
		{
			flag2 = true;
		}
		if (flag2)
		{
			Debug.Log("DeleteAll");
			List<string> usedAccountList = UIUserDataController.GetUsedAccountList();
			GOGPlayerPrefabs.DeleteAll();
			SkinIOController.DeleteAllCustomSkin();
			UIUserDataController.SetCustomSkinDownloadMark(true);
			UIUserDataController.SetUsedAccountList(usedAccountList);
			usedAccountList.Clear();
			List<string> list = new List<string>(this.dataCache.Keys);
			foreach (string text2 in list)
			{
				string text3 = text2;
				string text4 = this.dataCache[text2].ToString();
				try
				{
					if (text3.Contains(GOGPlayerPrefabs.DATA_TAG_INT))
					{
						GOGPlayerPrefabs.SetInt(text3.Remove(0, GOGPlayerPrefabs.DATA_TAG_INT.Length + 1), int.Parse(text4));
					}
					else if (text3.Contains(GOGPlayerPrefabs.DATA_TAG_STRING))
					{
						if (text4.Equals(GOGPlayerPrefabs.DATA_TAG_STRING_NULL))
						{
							GOGPlayerPrefabs.SetString(text3.Remove(0, GOGPlayerPrefabs.DATA_TAG_STRING.Length + 1), string.Empty);
						}
						else
						{
							GOGPlayerPrefabs.SetString(text3.Remove(0, GOGPlayerPrefabs.DATA_TAG_STRING.Length + 1), text4);
						}
					}
					else if (text3.Contains(GOGPlayerPrefabs.DATA_TAG_FLOAT))
					{
						GOGPlayerPrefabs.SetFloat(text3.Remove(0, GOGPlayerPrefabs.DATA_TAG_FLOAT.Length + 1), float.Parse(text4));
					}
				}
				catch (Exception message)
				{
					Debug.Log(message);
				}
			}
			list.Clear();
			UIUserDataController.SetPreUserName(ACTUserDataManager.curLoginUserName);
			ACTUserDataManager.preLoginUserName = ACTUserDataManager.curLoginUserName;
			UIUserDataController.SetCurLoginUserName(ACTUserDataManager.curLoginUserName);
			UIUserDataController.SetCurLoginPassword(ACTUserDataManager.curPassword);
		}
		GOGPlayerPrefabs.SetString("sessionid", GGCloudServiceConstant.mInstance.mSessionId);
		GOGPlayerPrefabs.SetInt("IsLastPlayCrashed", 1);
		GOGPlayerPrefabs.SetString("LastPlayCrashedDeviceID", SystemInfo.deviceUniqueIdentifier);
		GOGPlayerPrefabs.Save();
		GOGPlayerPrefabs.SetInt("IsInLogin", 1);
		GOGPlayerPrefabs.SetString("GOGLastDeviceID", SystemInfo.deviceUniqueIdentifier);
		this.bUserDataReady = true;
		if (a == string.Empty)
		{
			string key2 = "UserName";
			string value = ACTUserDataManager.curLoginUserName;
			GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, "UserProfileCollection", key2, value, new GGCloudServiceDownloadUserDataResponse(DownloadDataType.IsHaveRoleName));
		}
		else
		{
			this.bRoleNameReady = true;
			this.mIsHaveRoleName = true;
			this.mRoleName = a;
		}
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x0005CA64 File Offset: 0x0005AE64
	public void DownloadDataSequence3()
	{
		string text = ACTUserDataManager.curLoginUserName;
		string key = "UserName";
		string value = text;
		GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, "UserProfileCollection", key, value, new GGCloudServiceDownloadUserDataResponse(DownloadDataType.GetRoleName));
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x0005CAA8 File Offset: 0x0005AEA8
	public TimeSpan GetAllowenceLeftTime()
	{
		int num = (int)(this.mAllowanceDateTimeLeft.TotalSeconds - this.allowencetime);
		if (num <= 0)
		{
			num = 0;
		}
		TimeSpan result = new TimeSpan(0, 0, 0, num);
		return result;
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x0005CAE0 File Offset: 0x0005AEE0
	private void Update()
	{
		if (this.mbPublicDataAllowanceReady && this.mCurServerTime != string.Empty)
		{
			DateTime dateTime;
			DateTime dateTime2;
			if (!this.bGetTimeLeft && DateTime.TryParseExact(this.mCurServerTime, "yyyyMMddHHmmss", null, DateTimeStyles.None, out dateTime) && DateTime.TryParseExact(this.mAllowanceEndTime, "yyyyMMddHHmmss", null, DateTimeStyles.None, out dateTime2))
			{
				this.bGetTimeLeft = true;
				this.mAllowanceDateTimeLeft = dateTime2 - dateTime;
				if (Time.frameCount % 200 == 0)
				{
					Debug.Log("dtCurServerTime: " + dateTime);
					Debug.Log("dtAllowenceEndTime: " + dateTime2);
				}
			}
			this.allowencetime += (double)Time.deltaTime;
			if (Time.frameCount % 200 == 0)
			{
				this.GetAllowenceLeftTime();
			}
		}
		if (!this.mSaveScore && Application.loadedLevelName == "MainMenu")
		{
			base.StartCoroutine(this.SaveUserDataCoroutine());
			this.mSaveScore = true;
		}
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0005CBF8 File Offset: 0x0005AFF8
	public void LoginSuccessHandle()
	{
		this.DownloadDataSequence4();
		if (this.mIsHaveRoleName)
		{
			if (GGCloudServiceLoginProcessBar.mInstance != null)
			{
				GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update(100);
				GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("Login success!", ProgressStatus.Success);
			}
		}
		else if (GGCloudServiceLoginProcessBar.mInstance != null)
		{
			GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update(100);
			GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("Login success, but no role name!", ProgressStatus.SuccessWithoutRoleName);
		}
		GGCloudServiceKit.mInstance.StopCoroutineLoginTimeOut();
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x0005CC94 File Offset: 0x0005B094
	public IEnumerator SaveUserDataCoroutine()
	{
		yield return new WaitForSeconds(10f);
		GGCloudServiceKit.mInstance.SaveUserScore();
		yield break;
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x0005CCA8 File Offset: 0x0005B0A8
	public void DownloadAllCustomSkins(string[] skinUrlList, string[] skinNameList)
	{
		base.StartCoroutine(this.DownloadAllCustomSkinsCoroutine(skinUrlList, skinNameList));
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x0005CCBC File Offset: 0x0005B0BC
	public IEnumerator DownloadAllCustomSkinsCoroutine(string[] skinUrlList, string[] skinNameList)
	{
		if (GGCloudServiceLoginProcessBar.mInstance != null)
		{
			GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update("Download custom data 0%", "Download custom data", GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Progress, "Download custom data");
		}
		for (int i = 0; i < skinUrlList.Length; i++)
		{
			WWW www = new WWW(skinUrlList[i]);
			yield return www;
			SkinIOController.SaveSkin(skinNameList[i], www.bytes);
			int customItemsOpProgress = (i + 1) * 100 / skinUrlList.Length;
			if (GGCloudServiceLoginProcessBar.mInstance != null)
			{
				GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update("Download custom data " + customItemsOpProgress.ToString() + "%", "Download custom data", GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Progress, "Download custom data");
			}
		}
		yield return new WaitForSeconds(0.1f);
		UIUserDataController.SetCustomSkinDownloadMark(false);
		ACTUserDataManager.mInstance.LoginSuccessHandle();
		yield break;
	}

	// Token: 0x04000CF2 RID: 3314
	public static ACTUserDataManager mInstance;

	// Token: 0x04000CF3 RID: 3315
	public static string curLoginUserName = string.Empty;

	// Token: 0x04000CF4 RID: 3316
	public static string curPassword = string.Empty;

	// Token: 0x04000CF5 RID: 3317
	public static string preLoginUserName;

	// Token: 0x04000CF6 RID: 3318
	public string mCurServerTime = string.Empty;

	// Token: 0x04000CF7 RID: 3319
	public bool mIsHaveRoleName;

	// Token: 0x04000CF8 RID: 3320
	public string mRoleName = string.Empty;

	// Token: 0x04000CF9 RID: 3321
	public bool mCloudUserDataExist;

	// Token: 0x04000CFA RID: 3322
	public bool mClickLogOut;

	// Token: 0x04000CFB RID: 3323
	public bool mSaveScore;

	// Token: 0x04000CFC RID: 3324
	public bool mbPublicDataAllowanceReady;

	// Token: 0x04000CFD RID: 3325
	public string mAllowanceEndTime = string.Empty;

	// Token: 0x04000CFE RID: 3326
	public TimeSpan mAllowanceDateTimeLeft;

	// Token: 0x04000CFF RID: 3327
	private object mLockUploadData = new object();

	// Token: 0x04000D00 RID: 3328
	private object mLockServerTime = new object();

	// Token: 0x04000D01 RID: 3329
	private object mLockIsHaveRoleName = new object();

	// Token: 0x04000D02 RID: 3330
	public Dictionary<string, object> dataCache = new Dictionary<string, object>();

	// Token: 0x04000D03 RID: 3331
	public Dictionary<string, object> dataCachetmp = new Dictionary<string, object>();

	// Token: 0x04000D04 RID: 3332
	public static bool IsBuyingInGame;

	// Token: 0x04000D05 RID: 3333
	private int pauseCount;

	// Token: 0x04000D06 RID: 3334
	public bool bUserDataReady;

	// Token: 0x04000D07 RID: 3335
	public bool bServerTimeReady;

	// Token: 0x04000D08 RID: 3336
	public bool bRoleNameReady;

	// Token: 0x04000D09 RID: 3337
	private bool bGetTimeLeft;

	// Token: 0x04000D0A RID: 3338
	private double allowencetime;
}
