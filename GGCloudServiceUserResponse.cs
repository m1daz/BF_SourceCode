using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using RioLog;

// Token: 0x0200048E RID: 1166
public class GGCloudServiceUserResponse : App42CallBack
{
	// Token: 0x060021DC RID: 8668 RVA: 0x000FB294 File Offset: 0x000F9694
	public void OnSuccess(object user)
	{
		try
		{
			if (user is User)
			{
				User user2 = (User)user;
				this.result = user2.ToString();
				GGCloudServiceConstant.mInstance.mSessionId = user2.GetSessionId();
				RioQerdoDebug.Log("UserName : " + user2.GetUserName());
				RioQerdoDebug.Log("EmailId : " + user2.GetEmail());
				User.Profile profile = user2.GetProfile();
				if (profile != null)
				{
					RioQerdoDebug.Log("FIRST NAME" + profile.GetFirstName());
					RioQerdoDebug.Log("SEX" + profile.GetSex());
					RioQerdoDebug.Log("LAST NAME" + profile.GetLastName());
				}
			}
			else
			{
				IList<User> list = (IList<User>)user;
				this.result = list[0].ToString();
				RioQerdoDebug.Log("UserName : " + list[0].GetUserName());
				RioQerdoDebug.Log("EmailId : " + list[0].GetEmail());
			}
		}
		catch (App42Exception ex)
		{
			this.result = ex.ToString();
			RioQerdoDebug.Log("App42Exception : " + ex);
		}
	}

	// Token: 0x060021DD RID: 8669 RVA: 0x000FB3D0 File Offset: 0x000F97D0
	public void OnException(Exception e)
	{
		this.result = e.ToString();
		RioQerdoDebug.Log("Exception : " + e);
	}

	// Token: 0x060021DE RID: 8670 RVA: 0x000FB3EE File Offset: 0x000F97EE
	public string getResult()
	{
		return this.result;
	}

	// Token: 0x0400224B RID: 8779
	private string result = string.Empty;
}
