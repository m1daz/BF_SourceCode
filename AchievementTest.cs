using System;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.achievement;
using UnityEngine;

// Token: 0x020004C3 RID: 1219
public class AchievementTest : MonoBehaviour
{
	// Token: 0x06002254 RID: 8788 RVA: 0x000FD79C File Offset: 0x000FBB9C
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x06002255 RID: 8789 RVA: 0x000FD7BF File Offset: 0x000FBBBF
	private void Update()
	{
	}

	// Token: 0x06002256 RID: 8790 RVA: 0x000FD7C4 File Offset: 0x000FBBC4
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.GetResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1100f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "Create Achievement"))
		{
			App42Log.SetDebug(true);
			this.achievementService = this.sp.BuildAchievementService();
			this.achievementService.CreateAchievement(this.cons.achievementName, this.cons.description, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "Earn Achievement"))
		{
			App42Log.SetDebug(true);
			this.achievementService = this.sp.BuildAchievementService();
			this.achievementService.EarnAchievement(this.cons.userName, this.cons.achievementName, this.cons.gameName, this.cons.description, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "GetAll Achievements ForUser"))
		{
			App42Log.SetDebug(true);
			this.achievementService = this.sp.BuildAchievementService();
			this.achievementService.GetAllAchievementsForUser(this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "GetAll Achievements ForUserInGame"))
		{
			App42Log.SetDebug(true);
			this.achievementService = this.sp.BuildAchievementService();
			this.achievementService.GetAllAchievementsForUserInGame(this.cons.userName, this.cons.gameName, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 200f, 200f, 30f), "GetAll Achievements"))
		{
			App42Log.SetDebug(true);
			this.achievementService = this.sp.BuildAchievementService();
			this.achievementService.GetAllAchievements(this.callBack);
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "Get Achievement ByName"))
		{
			App42Log.SetDebug(true);
			this.achievementService = this.sp.BuildAchievementService();
			this.achievementService.GetAchievementByName(this.cons.achievementName, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 250f, 200f, 30f), "GetUsers Achievement"))
		{
			App42Log.SetDebug(true);
			this.achievementService = this.sp.BuildAchievementService();
			this.achievementService.GetUsersAchievement(this.cons.achievementName, this.cons.gameName, this.callBack);
		}
	}

	// Token: 0x040022D5 RID: 8917
	private Constant cons = new Constant();

	// Token: 0x040022D6 RID: 8918
	private ServiceAPI sp;

	// Token: 0x040022D7 RID: 8919
	private AchievementService achievementService;

	// Token: 0x040022D8 RID: 8920
	public string success;

	// Token: 0x040022D9 RID: 8921
	private AchievementResponse callBack = new AchievementResponse();
}
