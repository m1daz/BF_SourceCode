using System;
using System.Collections.Generic;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using UnityEngine;

// Token: 0x020004D4 RID: 1236
public class ScoreBoardTest : MonoBehaviour
{
	// Token: 0x06002295 RID: 8853 RVA: 0x00100EA1 File Offset: 0x000FF2A1
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x06002296 RID: 8854 RVA: 0x00100EC4 File Offset: 0x000FF2C4
	private void Update()
	{
	}

	// Token: 0x06002297 RID: 8855 RVA: 0x00100EC8 File Offset: 0x000FF2C8
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1300f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "SaveUserScore"))
		{
			App42Log.SetDebug(true);
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			this.scoreBoardService.SaveUserScore(this.cons.gameName, this.cons.userName, this.userScore, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "GetScoresByUser"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			this.scoreBoardService.GetScoresByUser(this.cons.gameName, this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "GetHighestScoreByUser"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			this.scoreBoardService.GetHighestScoreByUser(this.cons.gameName, this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "GetLowestScoreByUser"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			this.scoreBoardService.GetLowestScoreByUser(this.cons.gameName, this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 200f, 200f, 30f), "GetTopRankings"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			this.scoreBoardService.GetTopRankings(this.cons.gameName, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "GetAverageScoreByUser"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			this.scoreBoardService.GetAverageScoreByUser(this.cons.gameName, this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 250f, 200f, 30f), "GetLastScoreByUser"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			this.scoreBoardService.GetLastScoreByUser(this.cons.gameName, this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 250f, 200f, 30f), "GetTopRankings"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			this.scoreBoardService.GetTopRankings(this.cons.gameName, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 250f, 200f, 30f), "GetTopNRankings"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			this.scoreBoardService.GetTopNRankings(this.cons.gameName, this.max, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 250f, 200f, 30f), "GetTopNRankers"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			this.scoreBoardService.GetTopNRankers(this.cons.gameName, this.max, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 300f, 200f, 30f), "GetTopRankingsByGroup"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			IList<string> list = new List<string>();
			list.Add(this.cons.userName);
			list.Add(this.cons.userName1);
			this.scoreBoardService.GetTopRankingsByGroup(this.cons.gameName, list, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 300f, 200f, 30f), "GetTopNRankersByGroup"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			IList<string> list2 = new List<string>();
			list2.Add(this.cons.userName);
			list2.Add(this.cons.userName1);
			this.scoreBoardService.GetTopNRankersByGroup(this.cons.gameName, list2, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 300f, 200f, 30f), "EditScoreValueById"))
		{
			this.scoreBoardService = this.sp.BuildScoreBoardService();
			IList<string> list3 = new List<string>();
			list3.Add(this.cons.userName);
			list3.Add(this.cons.userName1);
			this.scoreBoardService.EditScoreValueById(this.cons.scoreId, this.userScore, this.callBack);
		}
	}

	// Token: 0x0400234C RID: 9036
	private ServiceAPI sp;

	// Token: 0x0400234D RID: 9037
	private ScoreBoardService scoreBoardService;

	// Token: 0x0400234E RID: 9038
	private Constant cons = new Constant();

	// Token: 0x0400234F RID: 9039
	public double userScore = 1000.0;

	// Token: 0x04002350 RID: 9040
	public double Score1 = 2000.0;

	// Token: 0x04002351 RID: 9041
	public int max = 2;

	// Token: 0x04002352 RID: 9042
	public int offSet = 1;

	// Token: 0x04002353 RID: 9043
	public string success;

	// Token: 0x04002354 RID: 9044
	private ScoreBoardResponse callBack = new ScoreBoardResponse();
}
