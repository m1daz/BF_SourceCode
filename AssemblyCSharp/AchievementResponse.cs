using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.achievement;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x020004C2 RID: 1218
	public class AchievementResponse : App42CallBack
	{
		// Token: 0x06002250 RID: 8784 RVA: 0x000FD61C File Offset: 0x000FBA1C
		public void OnSuccess(object response)
		{
			if (response is Achievement)
			{
				Achievement achievement = (Achievement)response;
				Debug.Log("userName  is : " + achievement.GetUserName());
				Debug.Log("achievementName is : " + achievement.GetName());
				Debug.Log("gameName is : " + achievement.GetGameName());
				Debug.Log("AchievedOn is : " + achievement.GetAchievedOn());
				Debug.Log("Description is : " + achievement.GetDescription());
			}
			else
			{
				IList<Achievement> list = (IList<Achievement>)response;
				for (int i = 0; i < list.Count; i++)
				{
					Debug.Log("userName  is : " + list[i].GetUserName());
					Debug.Log("achievementName is : " + list[i].GetName());
					Debug.Log("gameName is : " + list[i].GetGameName());
					Debug.Log("AchievedOn is : " + list[i].GetAchievedOn());
					Debug.Log("Description is : " + list[i].GetDescription());
				}
			}
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000FD758 File Offset: 0x000FBB58
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			Debug.Log("Exception : " + e);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000FD776 File Offset: 0x000FBB76
		public string GetResult()
		{
			return this.result;
		}

		// Token: 0x040022D4 RID: 8916
		private string result = string.Empty;
	}
}
