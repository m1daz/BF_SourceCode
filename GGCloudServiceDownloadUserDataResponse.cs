using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp.upload;
using com.shephertz.app42.paas.sdk.csharp.user;
using GOG.Utility;
using RioLog;
using SimpleJSON;

// Token: 0x020004A4 RID: 1188
public class GGCloudServiceDownloadUserDataResponse : App42CallBack
{
	// Token: 0x06002224 RID: 8740 RVA: 0x000FC383 File Offset: 0x000FA783
	public GGCloudServiceDownloadUserDataResponse(DownloadDataType downloadtype)
	{
		this.mType = downloadtype;
	}

	// Token: 0x06002225 RID: 8741 RVA: 0x000FC3B4 File Offset: 0x000FA7B4
	public void OnSuccess(object obj)
	{
		switch (this.mType)
		{
		case DownloadDataType.Login:
			if (obj is User)
			{
				GGCloudServiceConstant.mInstance.mUser = (User)obj;
				if (GGCloudServiceConstant.mInstance.mUser.accountLocked)
				{
					if (UILoginNewDirector.mInstance != null)
					{
						UILoginNewDirector.mInstance.PopErrorTipPanel("Your account had been locked because you get gems by invalid ways!");
					}
					return;
				}
				if (UILoginNewDirector.mInstance != null)
				{
					UILoginNewDirector.mInstance.LoginSucessEvent();
				}
			}
			if (GGCloudServiceLoginProcessBar.mInstance != null)
			{
				if (GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Progress < 30)
				{
					GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update("Init...", "Init user data", 30, "Init data...");
				}
				else
				{
					GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update("Init...", "Init user data", GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Progress, "Init data...");
				}
			}
			break;
		case DownloadDataType.GetServerTime:
			if (obj is JObject)
			{
				JObject jobject = (JObject)obj;
				ACTUserDataManager.mInstance.mCurServerTime = jobject["SystemTime"];
				ACTUserDataManager.mInstance.bServerTimeReady = true;
			}
			if (GGCloudServiceLoginProcessBar.mInstance != null)
			{
				GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update("Init...", "Init user data", GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Progress, "Init data...");
			}
			break;
		case DownloadDataType.IsHaveRoleName:
			if (obj is Storage)
			{
				Storage storage = (Storage)obj;
				IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
				for (int i = 0; i < jsonDocList.Count; i++)
				{
					string jsonDoc = jsonDocList[i].GetJsonDoc();
					JObject jobject2 = JObject.Parse(jsonDoc);
					ACTUserDataManager.mInstance.mRoleName = jobject2["RoleName"];
				}
			}
			ACTUserDataManager.mInstance.mIsHaveRoleName = true;
			ACTUserDataManager.mInstance.bRoleNameReady = true;
			if (GGCloudServiceLoginProcessBar.mInstance != null)
			{
				GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update("Init...", "Init user data", GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Progress, "Init data...");
			}
			break;
		case DownloadDataType.GetUserData:
			if (obj is Storage)
			{
				Storage storage2 = (Storage)obj;
				ACTUserDataManager.mInstance.DownloadDataSequence2(storage2);
			}
			if (GGCloudServiceLoginProcessBar.mInstance != null)
			{
				GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update("Init...", "Init user data", GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Progress, "Init data...");
			}
			break;
		case DownloadDataType.GetCustomSkin:
		{
			Upload upload = (Upload)obj;
			IList<Upload.File> fileList = upload.GetFileList();
			string[] array = new string[fileList.Count];
			string[] array2 = new string[fileList.Count];
			for (int j = 0; j < fileList.Count; j++)
			{
				array[j] = fileList[j].GetUrl();
				array2[j] = fileList[j].GetName().Split(new char[]
				{
					'@'
				})[2];
			}
			if (fileList.Count == 0)
			{
				UIUserDataController.SetCustomSkinDownloadMark(false);
				ACTUserDataManager.mInstance.LoginSuccessHandle();
			}
			else if (ACTUserDataManager.mInstance != null)
			{
				ACTUserDataManager.mInstance.DownloadAllCustomSkins(array, array2);
			}
			break;
		}
		}
		if (ACTUserDataManager.mInstance.bServerTimeReady && ACTUserDataManager.mInstance.bRoleNameReady && ACTUserDataManager.mInstance.bUserDataReady)
		{
			ACTUserDataManager.mInstance.bServerTimeReady = false;
			ACTUserDataManager.mInstance.bRoleNameReady = false;
			ACTUserDataManager.mInstance.bUserDataReady = false;
			if (UIUserDataController.NeedDownloadCustomSkinData())
			{
				string curLoginUserName = ACTUserDataManager.curLoginUserName;
				GGCloudServiceAdapter.mInstance.mUploadService.GetAllFilesByUser(curLoginUserName, new GGCloudServiceDownloadUserDataResponse(DownloadDataType.GetCustomSkin));
			}
			else
			{
				ACTUserDataManager.mInstance.LoginSuccessHandle();
			}
			GGCloudServiceKit.mInstance.GetCustomLeaderBoardFromCloud();
		}
	}

	// Token: 0x06002226 RID: 8742 RVA: 0x000FC7C0 File Offset: 0x000FABC0
	public void OnException(Exception e)
	{
		string text = e.ToString();
		RioQerdoDebug.Log("^^^^^^^^^^^^^^^^^^^^^^^exception: " + text);
		RioQerdoDebug.Log(this.mType);
		switch (this.mType)
		{
		case DownloadDataType.Login:
			if (text.Contains("2002"))
			{
				if (GGCloudServiceLoginProcessBar.mInstance != null)
				{
					GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("Username or password error!", ProgressStatus.Fail);
				}
				if (UILoginNewDirector.mInstance != null)
				{
					UILoginNewDirector.mInstance.PopErrorTipPanel("Username or password error!");
				}
			}
			else if (text.Contains("WebException") || text.Contains("Please check your network connection"))
			{
				if (GGCloudServiceLoginProcessBar.mInstance != null)
				{
					GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("NetWork not connected!", ProgressStatus.Fail);
				}
				if (UILoginNewDirector.mInstance != null)
				{
					UILoginNewDirector.mInstance.PopErrorTipPanel("NetWork not connected!");
				}
			}
			else if (GGCloudServiceLoginProcessBar.mInstance != null)
			{
				GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("Fail to connect to server!", ProgressStatus.Fail);
			}
			break;
		case DownloadDataType.GetServerTime:
			RioQerdoDebug.Log("Exception: GetServerTime: " + text);
			ACTUserDataManager.mInstance.mCurServerTime = string.Empty;
			ACTUserDataManager.mInstance.bServerTimeReady = true;
			break;
		case DownloadDataType.IsHaveRoleName:
			if (text.Contains("WebException") || text.Contains("Please check your network connection"))
			{
				if (GGCloudServiceLoginProcessBar.mInstance != null)
				{
					GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("NetWork not connected!", ProgressStatus.Fail);
				}
				if (UILoginNewDirector.mInstance != null)
				{
					UILoginNewDirector.mInstance.PopErrorTipPanel("NetWork not connected!");
				}
			}
			else if (text.Contains("2601"))
			{
				if (ACTUserDataManager.mInstance.bServerTimeReady && ACTUserDataManager.mInstance.bUserDataReady)
				{
					ACTUserDataManager.mInstance.bServerTimeReady = false;
					ACTUserDataManager.mInstance.bRoleNameReady = false;
					ACTUserDataManager.mInstance.bUserDataReady = false;
					UIUserDataController.SetCustomSkinDownloadMark(false);
					ACTUserDataManager.mInstance.DownloadDataSequence4();
					if (ACTUserDataManager.mInstance.mIsHaveRoleName)
					{
						if (GGCloudServiceLoginProcessBar.mInstance != null)
						{
							GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update(100);
							GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("Init data success!", ProgressStatus.Success);
						}
					}
					else if (GGCloudServiceLoginProcessBar.mInstance != null)
					{
						GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update(100);
						GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("Init data success, don't have role name!", ProgressStatus.SuccessWithoutRoleName);
					}
					GGCloudServiceKit.mInstance.GetCustomLeaderBoardFromCloud();
				}
			}
			else if (GGCloudServiceLoginProcessBar.mInstance != null)
			{
				GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("Fail to connect to server!", ProgressStatus.Fail);
			}
			ACTUserDataManager.mInstance.mIsHaveRoleName = false;
			ACTUserDataManager.mInstance.bRoleNameReady = true;
			break;
		case DownloadDataType.GetUserData:
			if (text.Contains("2601"))
			{
				ACTUserDataManager.mInstance.DownloadDataSequence2NotHaveUserData();
			}
			else if (text.Contains("WebException") || text.Contains("Please check your network connection"))
			{
				if (GGCloudServiceLoginProcessBar.mInstance != null)
				{
					GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("NetWork not connected!", ProgressStatus.Fail);
				}
				if (UILoginNewDirector.mInstance != null)
				{
					UILoginNewDirector.mInstance.PopErrorTipPanel("NetWork not connected!");
				}
			}
			else if (GGCloudServiceLoginProcessBar.mInstance != null)
			{
				GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("Fail to connect to server!", ProgressStatus.Fail);
			}
			break;
		case DownloadDataType.GetCustomSkin:
		{
			App42Exception ex = (App42Exception)e;
			int appErrorCode = ex.GetAppErrorCode();
			int httpErrorCode = ex.GetHttpErrorCode();
			if (appErrorCode == 2102)
			{
				UIUserDataController.SetCustomSkinDownloadMark(false);
			}
			else if (appErrorCode != 1401)
			{
				if (appErrorCode == 1500)
				{
				}
			}
			ACTUserDataManager.mInstance.LoginSuccessHandle();
			break;
		}
		}
	}

	// Token: 0x04002260 RID: 8800
	public DownloadDataType mType;

	// Token: 0x04002261 RID: 8801
	public string mCollectionName = string.Empty;

	// Token: 0x04002262 RID: 8802
	public string mKey = string.Empty;

	// Token: 0x04002263 RID: 8803
	public string mValve = string.Empty;
}
