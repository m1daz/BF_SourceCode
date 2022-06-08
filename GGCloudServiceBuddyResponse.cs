using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.buddy;
using RioLog;

// Token: 0x0200048F RID: 1167
public class GGCloudServiceBuddyResponse : App42CallBack
{
	// Token: 0x060021E0 RID: 8672 RVA: 0x000FB40C File Offset: 0x000F980C
	public void OnSuccess(object buddy)
	{
		this.result = buddy.ToString();
		try
		{
			if (buddy is Buddy)
			{
				Buddy buddy2 = (Buddy)buddy;
				this.result = buddy2.ToString();
			}
			else
			{
				IList<Buddy> list = (IList<Buddy>)buddy;
				this.result = list[0].ToString();
				for (int i = 0; i < list.Count; i++)
				{
				}
			}
		}
		catch (App42Exception ex)
		{
			this.result = ex.ToString();
			RioQerdoDebug.Log("App42Exception : " + ex);
		}
	}

	// Token: 0x060021E1 RID: 8673 RVA: 0x000FB4B0 File Offset: 0x000F98B0
	public void OnException(Exception e)
	{
		this.result = e.ToString();
		RioQerdoDebug.Log("Exception : " + e);
	}

	// Token: 0x060021E2 RID: 8674 RVA: 0x000FB4CE File Offset: 0x000F98CE
	public string getResult()
	{
		return this.result;
	}

	// Token: 0x0400224C RID: 8780
	private string result = string.Empty;
}
