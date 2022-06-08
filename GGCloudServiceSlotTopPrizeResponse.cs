using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using SimpleJSON;

// Token: 0x0200048D RID: 1165
public class GGCloudServiceSlotTopPrizeResponse : App42CallBack
{
	// Token: 0x060021D9 RID: 8665 RVA: 0x000FB0C4 File Offset: 0x000F94C4
	public void OnSuccess(object response)
	{
		List<CSSlotTopPrizeInfo> list = new List<CSSlotTopPrizeInfo>();
		JObject jobject = response as JObject;
		string json = jobject["StorageStrResponse"];
		Storage storage = new StorageResponseBuilder().BuildResponse(json);
		IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
		for (int i = 0; i < storage.GetJsonDocList().Count; i++)
		{
			string jsonDoc = jsonDocList[i].GetJsonDoc();
			JObject jobject2 = JObject.Parse(jsonDoc);
			string roleName = jobject2["RoleName"];
			string prizeName = jobject2["PrizeName"];
			string prizeInfo = jobject2["PrizeInfo"];
			list.Add(new CSSlotTopPrizeInfo
			{
				RoleName = roleName,
				PrizeName = prizeName,
				PrizeInfo = prizeInfo
			});
		}
		foreach (CSSlotTopPrizeInfo csslotTopPrizeInfo in list)
		{
		}
		GGCloudServiceKit.mInstance.mGetTopPrize = true;
		GGCloudServiceKit.mInstance.mTopPrizeList.Clear();
		GGCloudServiceKit.mInstance.mTopPrizeList = list;
		if (UICasinoDirector.mInstance != null)
		{
			UICasinoDirector.mInstance.ShowLuckyList(GGCloudServiceKit.mInstance.mTopPrizeList);
		}
	}

	// Token: 0x060021DA RID: 8666 RVA: 0x000FB230 File Offset: 0x000F9630
	public void OnException(Exception e)
	{
		GGCloudServiceKit.mInstance.mTopPrizeList.Clear();
		GGCloudServiceKit.mInstance.mTopPrizeList = new List<CSSlotTopPrizeInfo>();
		if (UICasinoDirector.mInstance != null)
		{
			UICasinoDirector.mInstance.ShowLuckyList(GGCloudServiceKit.mInstance.mTopPrizeList);
		}
	}
}
