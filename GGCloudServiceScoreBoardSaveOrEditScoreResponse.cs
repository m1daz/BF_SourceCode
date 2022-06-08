using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;

// Token: 0x0200048B RID: 1163
public class GGCloudServiceScoreBoardSaveOrEditScoreResponse : App42CallBack
{
	// Token: 0x060021D3 RID: 8659 RVA: 0x000FB010 File Offset: 0x000F9410
	public void OnSuccess(object response)
	{
		Game game = (Game)response;
		int num = 0;
		if (num < game.GetScoreList().Count)
		{
			string scoreId = game.GetScoreList()[num].GetScoreId();
			double gameScore = (double)GrowthManagerKit.GetCharacterExpTotal();
			GGCloudServiceKit.mInstance.EditScoreValueById(scoreId, gameScore);
			GGCloudServiceKit.mInstance.LoadLogInScene();
		}
	}

	// Token: 0x060021D4 RID: 8660 RVA: 0x000FB074 File Offset: 0x000F9474
	public void OnException(Exception e)
	{
		double gameScore = (double)GrowthManagerKit.GetCharacterExpTotal();
		GGCloudServiceKit.mInstance.SaveScore(gameScore);
		GGCloudServiceKit.mInstance.LoadLogInScene();
	}
}
