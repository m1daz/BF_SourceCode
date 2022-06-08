using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;

// Token: 0x0200048A RID: 1162
public class GGCloudServiceGetOfficialMessageFindDocResponse : App42CallBack
{
	// Token: 0x060021CF RID: 8655 RVA: 0x000FAFC1 File Offset: 0x000F93C1
	public GGCloudServiceGetOfficialMessageFindDocResponse(string tusername)
	{
		this.userName = tusername;
	}

	// Token: 0x060021D0 RID: 8656 RVA: 0x000FAFDB File Offset: 0x000F93DB
	public void OnSuccess(object response)
	{
		GGCloudServiceKit.mInstance.GetOfficialMessageAction_CallBack((Storage)response, this.userName);
	}

	// Token: 0x060021D1 RID: 8657 RVA: 0x000FAFF3 File Offset: 0x000F93F3
	public void OnException(Exception e)
	{
		GGCloudServiceKit.mInstance.GetOfficialMessageAction_CallBack(null, this.userName);
	}

	// Token: 0x0400224A RID: 8778
	private string userName = string.Empty;
}
