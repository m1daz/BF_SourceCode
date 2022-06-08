using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using RioLog;

// Token: 0x02000485 RID: 1157
public class GGCloudServiceOurSeasonRankResponse : App42CallBack
{
	// Token: 0x060021BF RID: 8639 RVA: 0x000FA610 File Offset: 0x000F8A10
	public void OnSuccess(object response)
	{
		Game game = (Game)response;
		for (int i = 0; i < game.GetScoreList().Count; i++)
		{
			GGCloudServiceKit.mInstance.MySeasonScoreRank = int.Parse(game.GetScoreList()[i].GetRank());
			GrowthManagerKit.SetSeasonRank(GGCloudServiceKit.mInstance.MySeasonScoreRank);
		}
	}

	// Token: 0x060021C0 RID: 8640 RVA: 0x000FA66F File Offset: 0x000F8A6F
	public void OnException(Exception e)
	{
		RioQerdoDebug.Log("Exception : " + e);
	}
}
