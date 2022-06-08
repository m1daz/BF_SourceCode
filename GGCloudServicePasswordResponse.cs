using System;
using com.shephertz.app42.paas.sdk.csharp;
using RioLog;

// Token: 0x020004A3 RID: 1187
public class GGCloudServicePasswordResponse : App42CallBack
{
	// Token: 0x06002222 RID: 8738 RVA: 0x000FC355 File Offset: 0x000FA755
	public void OnSuccess(object response)
	{
		RioQerdoDebug.Log("password response: " + response.ToString());
	}

	// Token: 0x06002223 RID: 8739 RVA: 0x000FC36C File Offset: 0x000FA76C
	public void OnException(Exception e)
	{
		RioQerdoDebug.Log("exception: " + e.ToString());
	}
}
