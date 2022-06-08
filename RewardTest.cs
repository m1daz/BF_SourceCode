using System;
using System.Collections.Generic;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.reward;
using UnityEngine;

// Token: 0x020004D2 RID: 1234
public class RewardTest : MonoBehaviour
{
	// Token: 0x0600228D RID: 8845 RVA: 0x001007CC File Offset: 0x000FEBCC
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x0600228E RID: 8846 RVA: 0x001007EF File Offset: 0x000FEBEF
	private void Update()
	{
	}

	// Token: 0x0600228F RID: 8847 RVA: 0x001007F4 File Offset: 0x000FEBF4
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1300f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "CreateReward"))
		{
			this.rewardService = this.sp.BuildRewardService();
			this.rewardService.CreateReward(this.cons.rewardName, this.rewardDescription, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "GetAllRewards"))
		{
			this.rewardService = this.sp.BuildRewardService();
			this.rewardService.GetAllRewards(this.callBack);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "GetRewardByName"))
		{
			this.rewardService = this.sp.BuildRewardService();
			this.rewardService.GetRewardByName(this.cons.rewardName, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "EarnRewards"))
		{
			this.rewardService = this.sp.BuildRewardService();
			this.rewardService.EarnRewards(this.cons.gameName, this.cons.userName, this.cons.rewardName, this.rewardPoints1, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 200f, 200f, 30f), "RedeemRewards"))
		{
			this.rewardService = this.sp.BuildRewardService();
			this.rewardService.RedeemRewards(this.cons.gameName, this.cons.userName, this.cons.rewardName, this.rewardPoints1, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "GetGameRewardPointsForUser"))
		{
			this.rewardService = this.sp.BuildRewardService();
			this.rewardService.GetGameRewardPointsForUser(this.cons.gameName, this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 250f, 200f, 30f), "GetAllRewardsByPaging"))
		{
			this.rewardService = this.sp.BuildRewardService();
			this.rewardService.GetAllRewards(this.max, this.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 250f, 200f, 30f), "GetAllRewardsCount"))
		{
			this.rewardService = this.sp.BuildRewardService();
			this.rewardService.GetAllRewardsCount(this.callBack);
		}
		if (GUI.Button(new Rect(680f, 250f, 200f, 30f), "GetTopNRewardEarners"))
		{
			this.rewardService = this.sp.BuildRewardService();
			this.rewardService.GetTopNRewardEarners(this.cons.gameName, this.cons.rewardName, this.max, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 250f, 200f, 30f), "GetAllRewardsByUser"))
		{
			this.rewardService = this.sp.BuildRewardService();
			this.rewardService.GetAllRewardsByUser(this.cons.userName, this.cons.rewardName, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 300f, 200f, 30f), "GetTopNRewardEarnersByGroup"))
		{
			this.rewardService = this.sp.BuildRewardService();
			IList<string> list = new List<string>();
			list.Add(this.cons.userName);
			list.Add(this.cons.userName1);
			this.rewardService.GetTopNRewardEarnersByGroup(this.cons.gameName, this.cons.rewardName, list, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 300f, 200f, 30f), "GetUserRankingOnReward"))
		{
			this.rewardService = this.sp.BuildRewardService();
			this.rewardService.GetUserRankingOnReward(this.cons.gameName, this.cons.rewardName, this.cons.userName, this.callBack);
		}
	}

	// Token: 0x04002340 RID: 9024
	private ServiceAPI sp;

	// Token: 0x04002341 RID: 9025
	private RewardService rewardService;

	// Token: 0x04002342 RID: 9026
	private Constant cons = new Constant();

	// Token: 0x04002343 RID: 9027
	public string rewardDescription = "REWARD DESCRIPTION";

	// Token: 0x04002344 RID: 9028
	public double rewardPoints = 10000.0;

	// Token: 0x04002345 RID: 9029
	public double rewardPoints1 = 2000.0;

	// Token: 0x04002346 RID: 9030
	public int max = 2;

	// Token: 0x04002347 RID: 9031
	public int offSet = 1;

	// Token: 0x04002348 RID: 9032
	public string success;

	// Token: 0x04002349 RID: 9033
	public string box;

	// Token: 0x0400234A RID: 9034
	private RewardResponse callBack = new RewardResponse();
}
