using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using RioLog;
using SimpleJSON;

// Token: 0x02000484 RID: 1156
public class GGCloudServiceUserGetSeasonScoreStorageResponse : App42CallBack
{
	// Token: 0x060021BC RID: 8636 RVA: 0x000FA3FC File Offset: 0x000F87FC
	public void OnSuccess(object obj)
	{
		try
		{
			this.result = obj.ToString();
			if (obj is Storage)
			{
				Storage storage = (Storage)obj;
				IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
				for (int i = 0; i < jsonDocList.Count; i++)
				{
					string jsonDoc = jsonDocList[i].GetJsonDoc();
					JObject jobject = JObject.Parse(jsonDoc);
					this.SeasonScoreThreshold = double.Parse(jobject["SeasonScoreThreshold"]);
					this.SeasonStartTime = jobject["StartTime"];
					this.SeasonEndTime = jobject["EndTime"];
					this.SeasonTip = jobject["SeasonTip"];
					string text = jobject["LeaderBoard"];
					string[] array = text.Split(new char[]
					{
						'&'
					});
					for (int j = 0; j < array.Length; j++)
					{
						string[] array2 = array[j].Split(new char[]
						{
							'@'
						});
						CSSeasonScoreBoardInfo csseasonScoreBoardInfo = new CSSeasonScoreBoardInfo();
						csseasonScoreBoardInfo.Level = array2[0];
						csseasonScoreBoardInfo.RoleName = array2[1];
						csseasonScoreBoardInfo.ScoreRank = array2[2];
						csseasonScoreBoardInfo.Score = array2[3];
						this.seasonScordBoardLeaderBoardInfoList.Add(csseasonScoreBoardInfo);
					}
				}
			}
			GGCloudServiceKit.mInstance.mSeasonScoreBoardInfoList = this.seasonScordBoardLeaderBoardInfoList;
			GGCloudServiceKit.mInstance.mSeasonExtraInfo.SeasonStartTime = this.SeasonStartTime;
			GGCloudServiceKit.mInstance.mSeasonExtraInfo.SeasonEndTime = this.SeasonEndTime;
			GGCloudServiceKit.mInstance.mSeasonExtraInfo.SeasonTip = this.SeasonTip;
			GGCloudServiceKit.mInstance.mSeasonScoreThreshold = this.SeasonScoreThreshold;
		}
		catch (Exception ex)
		{
			RioQerdoDebug.Log("Exception 1 is : " + ex.ToString());
		}
	}

	// Token: 0x060021BD RID: 8637 RVA: 0x000FA5F0 File Offset: 0x000F89F0
	public void OnException(Exception e)
	{
		RioQerdoDebug.Log("Exception 2 is : " + e.ToString());
	}

	// Token: 0x0400223E RID: 8766
	private string result = string.Empty;

	// Token: 0x0400223F RID: 8767
	private List<CSSeasonScoreBoardInfo> seasonScordBoardLeaderBoardInfoList = new List<CSSeasonScoreBoardInfo>();

	// Token: 0x04002240 RID: 8768
	private double SeasonScoreThreshold = 1.0;

	// Token: 0x04002241 RID: 8769
	private string SeasonStartTime = string.Empty;

	// Token: 0x04002242 RID: 8770
	private string SeasonEndTime = string.Empty;

	// Token: 0x04002243 RID: 8771
	private string SeasonTip = string.Empty;
}
