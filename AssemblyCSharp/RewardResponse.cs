using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.reward;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x020004D1 RID: 1233
	public class RewardResponse : App42CallBack
	{
		// Token: 0x06002289 RID: 8841 RVA: 0x0010064C File Offset: 0x000FEA4C
		public void OnSuccess(object obj)
		{
			try
			{
				if (obj is Reward)
				{
					Reward reward = (Reward)obj;
					this.result = reward.ToString();
					Debug.Log("GetName : " + reward.GetName());
					Debug.Log("GetGameName : " + reward.GetGameName());
				}
				else
				{
					IList<Reward> list = (IList<Reward>)obj;
					this.result = list[0].ToString();
					Debug.Log("RewardResponse  " + list[0]);
					Debug.Log("GetName : " + list[0].GetName());
					Debug.Log("GetGameName : " + list[0].GetGameName());
				}
			}
			catch (App42Exception ex)
			{
				this.result = ex.ToString();
				Debug.Log("App42Exception : " + ex);
			}
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x00100744 File Offset: 0x000FEB44
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			Debug.Log("EXCEPTION  :  " + e);
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x00100762 File Offset: 0x000FEB62
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x0400233F RID: 9023
		private string result = string.Empty;
	}
}
