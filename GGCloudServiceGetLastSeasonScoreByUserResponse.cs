using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using RioLog;

// Token: 0x0200049E RID: 1182
public class GGCloudServiceGetLastSeasonScoreByUserResponse : App42CallBack
{
	// Token: 0x06002211 RID: 8721 RVA: 0x000FBD34 File Offset: 0x000FA134
	public GGCloudServiceGetLastSeasonScoreByUserResponse(double tseasonscore, string tgameName, string tusername)
	{
		this.seasonscore = tseasonscore;
		this.gameName = tgameName;
		this.userName = tusername;
	}

	// Token: 0x06002212 RID: 8722 RVA: 0x000FBD54 File Offset: 0x000FA154
	public void OnSuccess(object obj)
	{
		Game game = (Game)obj;
		int count = game.GetScoreList().Count;
		int num = 0;
		if (num < count)
		{
			string scoreId = game.GetScoreList()[num].GetScoreId();
			GGCloudServiceAdapter.mInstance.mScoreBoardService.EditScoreValueById(scoreId, this.seasonscore, new GGCloudServiceUnKnownServiceResponse());
		}
	}

	// Token: 0x06002213 RID: 8723 RVA: 0x000FBDB8 File Offset: 0x000FA1B8
	public void OnException(Exception ex)
	{
		try
		{
			GGCloudServiceAdapter.mInstance.mScoreBoardService.SaveUserScore(this.gameName, this.userName, this.seasonscore, new GGCloudServiceUnKnownServiceResponse());
		}
		catch (Exception ex2)
		{
			RioQerdoDebug.Log("ex2: " + ex2.ToString());
		}
	}

	// Token: 0x04002257 RID: 8791
	private double seasonscore;

	// Token: 0x04002258 RID: 8792
	private string gameName;

	// Token: 0x04002259 RID: 8793
	private string userName;
}
