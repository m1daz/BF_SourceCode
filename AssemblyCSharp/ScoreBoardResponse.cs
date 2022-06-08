using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x020004D3 RID: 1235
	public class ScoreBoardResponse : App42CallBack
	{
		// Token: 0x06002291 RID: 8849 RVA: 0x00100CF8 File Offset: 0x000FF0F8
		public void OnSuccess(object obj)
		{
			if (obj is Game)
			{
				Game game = (Game)obj;
				this.result = game.ToString();
				Debug.Log("GameName : " + game.GetName());
				if (game.GetScoreList() != null)
				{
					IList<Game.Score> scoreList = game.GetScoreList();
					for (int i = 0; i < scoreList.Count; i++)
					{
						Debug.Log("UserName is  : " + scoreList[i].GetUserName());
						Debug.Log("CreatedOn is  : " + scoreList[i].GetCreatedOn());
						Debug.Log("ScoreId is  : " + scoreList[i].GetScoreId());
						Debug.Log("Value is  : " + scoreList[i].GetValue());
					}
				}
			}
			else
			{
				IList<Game> list = (IList<Game>)obj;
				this.result = list.ToString();
				for (int j = 0; j < list.Count; j++)
				{
					Debug.Log("GameName is   : " + list[j].GetName());
				}
			}
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x00100E26 File Offset: 0x000FF226
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			Debug.Log("EXCEPTION : " + e);
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x00100E44 File Offset: 0x000FF244
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x0400234B RID: 9035
		private string result = string.Empty;
	}
}
