using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using RioLog;

// Token: 0x020004A1 RID: 1185
public class GGCloudServiceUploadDataActionResponse : App42CallBack
{
	// Token: 0x0600221B RID: 8731 RVA: 0x000FC081 File Offset: 0x000FA481
	public GGCloudServiceUploadDataActionResponse(UploadDataActionType type)
	{
		this.mUploadDataType = type;
	}

	// Token: 0x0600221C RID: 8732 RVA: 0x000FC090 File Offset: 0x000FA490
	public void OnSuccess(object response)
	{
		UploadDataActionType uploadDataActionType = this.mUploadDataType;
		if (uploadDataActionType != UploadDataActionType.FindDoc)
		{
			if (uploadDataActionType == UploadDataActionType.InsertDoc)
			{
				if (response is Storage)
				{
					Storage storage = (Storage)response;
					IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
					for (int i = 0; i < storage.GetJsonDocList().Count; i++)
					{
						string docId = jsonDocList[i].GetDocId();
						HashSet<ACL> hashSet = new HashSet<ACL>();
						hashSet.Add(new ACL("PUBLIC", Permission.READ));
						GGCloudServiceAdapter.mInstance.mStorageService.RevokeAccessOnDoc(GGCloudServiceConstant.mInstance.mDBName, GGCloudServiceConstant.mInstance.mUserDataCollectionName, docId, hashSet, new GGCloudServiceUploadDataActionResponse(UploadDataActionType.RevokeAccessDoc));
					}
				}
			}
		}
		else
		{
			ACTUserDataManager.mInstance.UploadDataActionSeq_UpdateDoc();
		}
	}

	// Token: 0x0600221D RID: 8733 RVA: 0x000FC158 File Offset: 0x000FA558
	public void OnException(Exception e)
	{
		string text = e.ToString();
		RioQerdoDebug.Log("exception: " + e.ToString());
		UploadDataActionType uploadDataActionType = this.mUploadDataType;
		if (uploadDataActionType != UploadDataActionType.FindDoc)
		{
			if (uploadDataActionType != UploadDataActionType.InsertDoc)
			{
			}
		}
		else if (text.Contains("2601"))
		{
			ACTUserDataManager.mInstance.UploadDataActionSeq_InsertDoc();
		}
	}

	// Token: 0x0400225F RID: 8799
	public UploadDataActionType mUploadDataType;
}
