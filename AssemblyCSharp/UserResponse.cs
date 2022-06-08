using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x020004DB RID: 1243
	public class UserResponse : App42CallBack
	{
		// Token: 0x060022B1 RID: 8881 RVA: 0x00102A64 File Offset: 0x00100E64
		public void OnSuccess(object user)
		{
			try
			{
				if (user is User)
				{
					User user2 = (User)user;
					this.result = user2.ToString();
					Debug.Log("User Session Id: " + user2.GetSessionId());
					Debug.Log("UserName : " + user2.GetUserName());
					Debug.Log("EmailId : " + user2.GetEmail());
					User.Profile profile = user2.GetProfile();
					if (profile != null)
					{
						Debug.Log("FIRST NAME" + profile.GetFirstName());
						Debug.Log("SEX" + profile.GetSex());
						Debug.Log("LAST NAME" + profile.GetLastName());
					}
				}
				else
				{
					IList<User> list = (IList<User>)user;
					this.result = list[0].ToString();
					Debug.Log("UserName : " + list[0].GetUserName());
					Debug.Log("EmailId : " + list[0].GetEmail());
				}
			}
			catch (App42Exception ex)
			{
				this.result = ex.ToString();
				Debug.Log("App42Exception : " + ex);
			}
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x00102BB4 File Offset: 0x00100FB4
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			Debug.Log("Exception : " + e);
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x00102BD2 File Offset: 0x00100FD2
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x04002379 RID: 9081
		private string result = string.Empty;
	}
}
