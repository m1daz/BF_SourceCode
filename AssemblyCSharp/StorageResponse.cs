using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x020004D9 RID: 1241
	public class StorageResponse : App42CallBack
	{
		// Token: 0x060022A9 RID: 8873 RVA: 0x001020F0 File Offset: 0x001004F0
		public void OnSuccess(object obj)
		{
			this.result = obj.ToString();
			if (obj is Storage)
			{
				Storage storage = (Storage)obj;
				Debug.Log("Storage Response : " + storage);
				IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
				for (int i = 0; i < storage.GetJsonDocList().Count; i++)
				{
					Debug.Log("ObjectId is : " + jsonDocList[i].GetDocId());
					Debug.Log("jsonDoc is : " + jsonDocList[i].GetJsonDoc());
				}
			}
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x00102184 File Offset: 0x00100584
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			Debug.Log("Exception is : " + e);
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x001021A2 File Offset: 0x001005A2
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x0400236F RID: 9071
		private string result = string.Empty;
	}
}
