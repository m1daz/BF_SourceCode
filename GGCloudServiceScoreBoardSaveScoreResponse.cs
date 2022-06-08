using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using RioLog;

// Token: 0x02000487 RID: 1159
public class GGCloudServiceScoreBoardSaveScoreResponse : App42CallBack
{
	// Token: 0x060021C7 RID: 8647 RVA: 0x000FAED4 File Offset: 0x000F92D4
	public void OnSuccess(object response)
	{
		Game game = (Game)response;
		for (int i = 0; i < game.GetScoreList().Count; i++)
		{
		}
	}

	// Token: 0x060021C8 RID: 8648 RVA: 0x000FAF04 File Offset: 0x000F9304
	public void OnException(Exception e)
	{
		RioQerdoDebug.Log("Exception : " + e);
	}
}
