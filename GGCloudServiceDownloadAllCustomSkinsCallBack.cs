using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using GOG.Utility;
using RioLog;

// Token: 0x020004A2 RID: 1186
public class GGCloudServiceDownloadAllCustomSkinsCallBack : App42CallBack
{
	// Token: 0x0600221F RID: 8735 RVA: 0x000FC1C8 File Offset: 0x000FA5C8
	public void OnSuccess(object response)
	{
		Upload upload = (Upload)response;
		IList<Upload.File> fileList = upload.GetFileList();
		string[] array = new string[fileList.Count];
		string[] array2 = new string[fileList.Count];
		for (int i = 0; i < fileList.Count; i++)
		{
			array[i] = fileList[i].GetUrl();
			array2[i] = fileList[i].GetName().Split(new char[]
			{
				'@'
			})[2];
		}
		if (fileList.Count == 0)
		{
			UIUserDataController.SetCustomSkinDownloadMark(false);
			if (GGCloudServiceLoginProcessBar.mInstance != null)
			{
				GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update(100);
				GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("Init data success!", ProgressStatus.Success);
			}
		}
		else if (ACTUserDataManager.mInstance != null)
		{
			ACTUserDataManager.mInstance.DownloadAllCustomSkins(array, array2);
		}
	}

	// Token: 0x06002220 RID: 8736 RVA: 0x000FC2B4 File Offset: 0x000FA6B4
	public void OnException(Exception e)
	{
		RioQerdoDebug.Log("Exception : " + e);
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
		if (GGCloudServiceLoginProcessBar.mInstance != null)
		{
			GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Update(100);
			GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.EndProgress("Init data success!", ProgressStatus.Success);
		}
	}
}
