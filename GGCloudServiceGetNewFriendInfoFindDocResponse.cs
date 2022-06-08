using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;

// Token: 0x0200049B RID: 1179
public class GGCloudServiceGetNewFriendInfoFindDocResponse : App42CallBack
{
	// Token: 0x06002209 RID: 8713 RVA: 0x000FBCE0 File Offset: 0x000FA0E0
	public void OnSuccess(object obj)
	{
		GGCloudServiceKit.mInstance.GetNewFriendInfoAction_CallBack1((Storage)obj);
	}

	// Token: 0x0600220A RID: 8714 RVA: 0x000FBCF2 File Offset: 0x000FA0F2
	public void OnException(Exception ex)
	{
	}
}
