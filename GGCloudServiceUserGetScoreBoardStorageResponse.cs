using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using RioLog;
using SimpleJSON;

// Token: 0x02000486 RID: 1158
public class GGCloudServiceUserGetScoreBoardStorageResponse : App42CallBack
{
	// Token: 0x060021C2 RID: 8642 RVA: 0x000FA6BC File Offset: 0x000F8ABC
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
					string text = jobject["#DataType#"];
					switch (text)
					{
					case "#ExpLeaderBoard#":
						try
						{
							string text2 = jobject["LeaderBoard"];
							this.expThreshold = double.Parse(jobject["ExpThreshold"]);
							string[] array = text2.Split(new char[]
							{
								'&'
							});
							for (int j = 0; j < array.Length; j++)
							{
								string[] array2 = array[j].Split(new char[]
								{
									'@'
								});
								CSLeaderBoardInfo csleaderBoardInfo = new CSLeaderBoardInfo();
								csleaderBoardInfo.RoleName = array2[0];
								csleaderBoardInfo.rank = array2[1];
								csleaderBoardInfo.Level = array2[2];
								csleaderBoardInfo.TotalKillNum = array2[3];
								this.expLeaderBoardInfoList.Add(csleaderBoardInfo);
							}
						}
						catch (Exception ex)
						{
						}
						break;
					case "#TotalKillNumLeaderBoard#":
						try
						{
							string text3 = jobject["LeaderBoard"];
							this.expThreshold = double.Parse(jobject["ExpThreshold"]);
							string[] array3 = text3.Split(new char[]
							{
								'&'
							});
							for (int k = 0; k < array3.Length; k++)
							{
								string[] array4 = array3[k].Split(new char[]
								{
									'@'
								});
								CSLeaderBoardInfo csleaderBoardInfo2 = new CSLeaderBoardInfo();
								csleaderBoardInfo2.RoleName = array4[0];
								csleaderBoardInfo2.rank = array4[1];
								csleaderBoardInfo2.Level = array4[2];
								csleaderBoardInfo2.TotalKillNum = array4[3];
								this.totalKillNumLeaderBoardInfoList.Add(csleaderBoardInfo2);
							}
						}
						catch (Exception ex2)
						{
						}
						break;
					case "#ActivityAwardForNewUser#":
						try
						{
							if (GGCloudServiceKit.mInstance.mNewUserName == ACTUserDataManager.curLoginUserName)
							{
								GGCloudServiceKit.mInstance.mNewUserName = string.Empty;
								string a = jobject["open"];
								if (a == "true")
								{
									int gemsAdd = int.Parse(jobject["gems"]);
									UserDataController.AddGems(gemsAdd);
								}
							}
						}
						catch (Exception ex3)
						{
						}
						break;
					case "#CurrentSeason#":
						try
						{
							string text4 = jobject["SeasonName"];
							string seasonMark = GrowthManagerKit.GetSeasonMark();
							if (text4.Contains("season_"))
							{
								GGCloudServiceKit.mInstance.mSeasonExtraInfo.CurrentSeasonName = this.SeasonMarkToSeasonName(text4);
								if (text4 != seasonMark)
								{
									if (seasonMark.Contains("season_"))
									{
										GrowthManagerKit.SetSeasonMark(text4);
										GGCloudServiceKit.mInstance.SaveUserSeasonScore(text4, ACTUserDataManager.curLoginUserName);
									}
									else if (seasonMark == string.Empty)
									{
										GrowthManagerKit.SetSeasonMark(text4);
									}
								}
								else
								{
									GGCloudServiceKit.mInstance.SaveUserSeasonScore(seasonMark, ACTUserDataManager.curLoginUserName);
								}
								GGCloudServiceKit.mInstance.GetMySeasonRank(text4, ACTUserDataManager.curLoginUserName);
								GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, "AllSeason", "SeasonName", text4, new GGCloudServiceUserGetSeasonScoreStorageResponse());
							}
							else if (seasonMark.Contains("season_"))
							{
								GGCloudServiceKit.mInstance.SaveUserSeasonScore(seasonMark, ACTUserDataManager.curLoginUserName);
								GGCloudServiceKit.mInstance.mSeasonExtraInfo.CurrentSeasonName = this.SeasonMarkToSeasonName(seasonMark);
								GGCloudServiceKit.mInstance.GetMySeasonRank(seasonMark, ACTUserDataManager.curLoginUserName);
								GGCloudServiceAdapter.mInstance.mStorageService.FindDocumentByKeyValue(GGCloudServiceConstant.mInstance.mDBName, "AllSeason", "SeasonName", seasonMark, new GGCloudServiceUserGetSeasonScoreStorageResponse());
							}
						}
						catch (Exception ex4)
						{
							RioQerdoDebug.Log(ex4.ToString());
						}
						break;
					case "#Allowance#":
						try
						{
							float num2 = float.Parse(jobject["CurrentAllowance"]);
							GGCloudServiceKit.mInstance.CurrentAllowance = 1.0;
							string text5 = jobject["StartTime"];
							string mAllowanceEndTime = jobject["EndTime"];
							bool flag = jobject["open"] == "true";
							if (flag)
							{
								GGCloudServiceKit.mInstance.CurrentAllowance = (double)num2;
								ACTUserDataManager.mInstance.mbPublicDataAllowanceReady = true;
								ACTUserDataManager.mInstance.mAllowanceEndTime = mAllowanceEndTime;
							}
						}
						catch (Exception ex5)
						{
							RioQerdoDebug.Log(ex5.ToString());
						}
						break;
					case "#Hunting#":
						try
						{
							string a2 = jobject["open"];
							if (a2 == "false")
							{
								GGCloudServiceKit.mInstance.bHuntingModeOpen = false;
							}
						}
						catch (Exception ex6)
						{
						}
						break;
					case "#ADPromotion#":
						try
						{
							string a3 = jobject["open"];
							if (a3 == "true")
							{
								GGCloudServiceKit.mInstance.bADPromotionOpen = true;
							}
						}
						catch (Exception ex7)
						{
						}
						break;
					}
				}
			}
			GGCloudServiceKit.mInstance.mExpLeaderBoardInfoList.Clear();
			GGCloudServiceKit.mInstance.mExpLeaderBoardInfoList = this.expLeaderBoardInfoList;
			GGCloudServiceKit.mInstance.mTotalKillNumLeaderBoardInfoList.Clear();
			GGCloudServiceKit.mInstance.mTotalKillNumLeaderBoardInfoList = this.totalKillNumLeaderBoardInfoList;
			GGCloudServiceKit.mInstance.mExpThreshold = this.expThreshold;
		}
		catch (Exception ex8)
		{
			RioQerdoDebug.Log(ex8.ToString());
		}
	}

	// Token: 0x060021C3 RID: 8643 RVA: 0x000FADD0 File Offset: 0x000F91D0
	private string SeasonMarkToSeasonName(string seasonMark)
	{
		string[] array = seasonMark.Split(new char[]
		{
			'_'
		});
		return "S" + array[array.Length - 1];
	}

	// Token: 0x060021C4 RID: 8644 RVA: 0x000FAE00 File Offset: 0x000F9200
	private int SortCompareByTotalKillNum(CSLeaderBoardInfo LB1, CSLeaderBoardInfo LB2)
	{
		int num = 0;
		if (int.Parse(LB1.TotalKillNum) > int.Parse(LB2.TotalKillNum))
		{
			num = -1;
		}
		else if (int.Parse(LB1.TotalKillNum) < int.Parse(LB2.TotalKillNum))
		{
			num = 1;
		}
		else if (int.Parse(LB1.TotalKillNum) == int.Parse(LB2.TotalKillNum))
		{
			if (int.Parse(LB1.Exp) > int.Parse(LB2.Exp))
			{
				num = -1;
			}
			else if (int.Parse(LB1.Exp) < int.Parse(LB2.Exp))
			{
				num = 1;
			}
		}
		return num;
	}

	// Token: 0x060021C5 RID: 8645 RVA: 0x000FAEAE File Offset: 0x000F92AE
	public void OnException(Exception e)
	{
		this.result = e.ToString();
		RioQerdoDebug.Log("Exception is : " + e);
	}

	// Token: 0x04002244 RID: 8772
	private string result = string.Empty;

	// Token: 0x04002245 RID: 8773
	private List<CSLeaderBoardInfo> expLeaderBoardInfoList = new List<CSLeaderBoardInfo>();

	// Token: 0x04002246 RID: 8774
	private List<CSLeaderBoardInfo> totalKillNumLeaderBoardInfoList = new List<CSLeaderBoardInfo>();

	// Token: 0x04002247 RID: 8775
	private double expThreshold = 1.0;
}
