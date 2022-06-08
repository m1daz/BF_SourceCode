using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using RioLog;

// Token: 0x020004A0 RID: 1184
public class GGCloudServiceAllUserDataResponse : App42CallBack
{
	// Token: 0x06002217 RID: 8727 RVA: 0x000FBEE8 File Offset: 0x000FA2E8
	public GGCloudServiceAllUserDataResponse(AllUserDataType userdatatype)
	{
		this.mType = userdatatype;
	}

	// Token: 0x06002218 RID: 8728 RVA: 0x000FBF0D File Offset: 0x000FA30D
	public GGCloudServiceAllUserDataResponse(AllUserDataType userdatatype, string username, string rolename)
	{
		this.mType = userdatatype;
		this.mUserName = username;
		this.mRoleName = rolename;
	}

	// Token: 0x06002219 RID: 8729 RVA: 0x000FBF40 File Offset: 0x000FA340
	public void OnSuccess(object obj)
	{
		RioQerdoDebug.Log("OnSuccess: " + obj.ToString());
		AllUserDataType allUserDataType = this.mType;
		if (allUserDataType != AllUserDataType.IsRoleNameExist)
		{
			if (allUserDataType != AllUserDataType.InsertRoleName)
			{
				if (allUserDataType == AllUserDataType.IsUserOnline)
				{
					if (obj is Storage)
					{
						Storage storage = (Storage)obj;
						ACTUserDataManager.mInstance.UserIsOnlineOrNot(storage);
					}
				}
			}
			else if (UILoginNewDirector.mInstance != null)
			{
				UILoginNewDirector.mInstance.CreateRoleNameSuccess();
			}
		}
		else
		{
			GGCloudServiceKit.mInstance.DisplayRoleNameToCreateIsExist();
		}
	}

	// Token: 0x0600221A RID: 8730 RVA: 0x000FBFD4 File Offset: 0x000FA3D4
	public void OnException(Exception e)
	{
		RioQerdoDebug.Log("Exception is : " + e);
		string text = e.ToString();
		AllUserDataType allUserDataType = this.mType;
		if (allUserDataType != AllUserDataType.IsRoleNameExist)
		{
			if (allUserDataType != AllUserDataType.InsertRoleName)
			{
				if (allUserDataType == AllUserDataType.IsUserOnline)
				{
					RioQerdoDebug.Log("OnException: LoadLevel(UILogin)");
					UISceneManager.mInstance.LoadLevel("UILogin");
				}
			}
			else if (text.Contains("WebException"))
			{
				GGCloudServiceKit.mInstance.DisplayCreateRoleNetworkNotConnect();
			}
		}
		else if (text.Contains("2601"))
		{
			GGCloudServiceKit.mInstance.InsertRoleNameForUser(this.mUserName, this.mRoleName);
		}
	}

	// Token: 0x0400225C RID: 8796
	public AllUserDataType mType;

	// Token: 0x0400225D RID: 8797
	public string mUserName = string.Empty;

	// Token: 0x0400225E RID: 8798
	public string mRoleName = string.Empty;
}
