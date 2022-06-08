using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.pushNotification;

// Token: 0x020004C1 RID: 1217
public class PushResponse : App42CallBack
{
	// Token: 0x0600224D RID: 8781 RVA: 0x000FD5E8 File Offset: 0x000FB9E8
	public void OnSuccess(object response)
	{
		if (response is PushNotification)
		{
			PushNotification pushNotification = (PushNotification)response;
		}
	}

	// Token: 0x0600224E RID: 8782 RVA: 0x000FD607 File Offset: 0x000FBA07
	public void OnException(Exception e)
	{
	}
}
