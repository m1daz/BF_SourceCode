using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.buddy;

// Token: 0x02000498 RID: 1176
public class GGCloudServiceGetFriendRequestResponse : App42CallBack
{
	// Token: 0x060021FE RID: 8702 RVA: 0x000FBA64 File Offset: 0x000F9E64
	public void OnSuccess(object buddy)
	{
		try
		{
			if (buddy is Buddy)
			{
				IList<Buddy> list = new List<Buddy>();
				Buddy item = (Buddy)buddy;
				list.Add(item);
				GGCloudServiceAdapter.mInstance.GetFriendRequest_buddylist(list);
			}
			else
			{
				IList<Buddy> buddyList = (IList<Buddy>)buddy;
				GGCloudServiceAdapter.mInstance.GetFriendRequest_buddylist(buddyList);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060021FF RID: 8703 RVA: 0x000FBAD0 File Offset: 0x000F9ED0
	public void OnException(Exception e)
	{
	}
}
