using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.email;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x020004C9 RID: 1225
	public class EmailResponse : App42CallBack
	{
		// Token: 0x06002269 RID: 8809 RVA: 0x000FE8D4 File Offset: 0x000FCCD4
		public void OnSuccess(object email)
		{
			if (email is Email)
			{
				Email email2 = (Email)email;
				this.result = email2.ToString();
				Debug.Log("GetBody:  " + email2.GetBody());
				Debug.Log("GetFrom : " + email2.GetFrom());
				Debug.Log("GetTo :  " + email2.GetTo());
				Debug.Log("GetConfigList : " + email2.GetConfigList());
				for (int i = 0; i < email2.GetConfigList().Count; i++)
				{
					Debug.Log("EmailId is :  " + email2.GetConfigList()[i].GetEmailId());
					Debug.Log("GetHost :  " + email2.GetConfigList()[i].GetHost());
					Debug.Log("GetPort :  " + email2.GetConfigList()[i].GetPort());
				}
			}
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000FE9D4 File Offset: 0x000FCDD4
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			Debug.Log("Exception : " + e);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000FE9F2 File Offset: 0x000FCDF2
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x04002318 RID: 8984
		private string result = string.Empty;
	}
}
