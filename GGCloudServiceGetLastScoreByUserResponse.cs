using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;

// Token: 0x0200049F RID: 1183
public class GGCloudServiceGetLastScoreByUserResponse : App42CallBack
{
	// Token: 0x06002214 RID: 8724 RVA: 0x000FBE1C File Offset: 0x000FA21C
	public GGCloudServiceGetLastScoreByUserResponse(string tgameName, double tTotalCharacterExp)
	{
		this.gameName = tgameName;
		this.TotalCharacterExp = tTotalCharacterExp;
	}

	// Token: 0x06002215 RID: 8725 RVA: 0x000FBE34 File Offset: 0x000FA234
	public void OnSuccess(object obj)
	{
		Game game = (Game)obj;
		int count = game.GetScoreList().Count;
		int num = 0;
		if (num < count)
		{
			string scoreId = game.GetScoreList()[num].GetScoreId();
			GGCloudServiceAdapter.mInstance.mScoreBoardService.EditScoreValueById(scoreId, this.TotalCharacterExp, new GGCloudServiceUnKnownServiceResponse());
		}
	}

	// Token: 0x06002216 RID: 8726 RVA: 0x000FBE98 File Offset: 0x000FA298
	public void OnException(Exception ex)
	{
		try
		{
			GGCloudServiceAdapter.mInstance.mScoreBoardService.SaveUserScore(this.gameName, UIUserDataController.GetDefaultUserName(), this.TotalCharacterExp, new GGCloudServiceUnKnownServiceResponse());
		}
		catch (Exception ex2)
		{
		}
	}

	// Token: 0x0400225A RID: 8794
	private string gameName;

	// Token: 0x0400225B RID: 8795
	private double TotalCharacterExp;
}
