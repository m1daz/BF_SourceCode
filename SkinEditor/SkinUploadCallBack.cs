using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using UnityEngine;

namespace SkinEditor
{
	// Token: 0x02000358 RID: 856
	public class SkinUploadCallBack : App42CallBack
	{
		// Token: 0x06001AD6 RID: 6870 RVA: 0x000D80D4 File Offset: 0x000D64D4
		public void OnSuccess(object response)
		{
			Upload upload = (Upload)response;
			IList<Upload.File> fileList = upload.GetFileList();
			for (int i = 0; i < fileList.Count; i++)
			{
			}
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000D8106 File Offset: 0x000D6506
		public void OnException(Exception e)
		{
			Debug.Log("Exception : " + e);
		}
	}
}
