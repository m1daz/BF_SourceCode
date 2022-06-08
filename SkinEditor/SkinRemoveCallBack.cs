using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using UnityEngine;

namespace SkinEditor
{
	// Token: 0x02000359 RID: 857
	public class SkinRemoveCallBack : App42CallBack
	{
		// Token: 0x06001AD8 RID: 6872 RVA: 0x000D8118 File Offset: 0x000D6518
		public SkinRemoveCallBack(string curTexFileName, bool needUploadNewFile)
		{
			this._curTexFileName = curTexFileName;
			this._needUploadNewFile = needUploadNewFile;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000D813C File Offset: 0x000D653C
		public void OnSuccess(object response)
		{
			App42Response app42Response = (App42Response)response;
			if (this._needUploadNewFile)
			{
				GGCloudServiceAdapter.mInstance.mUploadService.UploadFileForUser(UIUserDataController.GetDefaultRoleName() + "@Private@" + this._curTexFileName, UIUserDataController.GetDefaultUserName(), SkinIOController.GetCustomSkinFolderPath() + this._curTexFileName, UploadFileType.IMAGE, "Custom Skin", new SkinUploadCallBack());
			}
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x000D81A4 File Offset: 0x000D65A4
		public void OnException(Exception e)
		{
			Debug.Log("Exception : " + e);
			if (this._needUploadNewFile)
			{
				GGCloudServiceAdapter.mInstance.mUploadService.UploadFileForUser(UIUserDataController.GetDefaultRoleName() + "@Private@" + this._curTexFileName, UIUserDataController.GetDefaultUserName(), SkinIOController.GetCustomSkinFolderPath() + this._curTexFileName, UploadFileType.IMAGE, "Custom Skin", new SkinUploadCallBack());
			}
		}

		// Token: 0x04001D22 RID: 7458
		public string _curTexFileName = string.Empty;

		// Token: 0x04001D23 RID: 7459
		public bool _needUploadNewFile;
	}
}
