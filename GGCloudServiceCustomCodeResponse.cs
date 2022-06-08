using System;
using com.shephertz.app42.paas.sdk.csharp;
using RioLog;

// Token: 0x02000496 RID: 1174
public class GGCloudServiceCustomCodeResponse : App42CallBack
{
	// Token: 0x060021F8 RID: 8696 RVA: 0x000FBA0E File Offset: 0x000F9E0E
	public void OnSuccess(object obj)
	{
		RioQerdoDebug.Log("@@@@@@@OnSuccess: " + obj.ToString());
	}

	// Token: 0x060021F9 RID: 8697 RVA: 0x000FBA25 File Offset: 0x000F9E25
	public void OnException(Exception e)
	{
		RioQerdoDebug.Log("@@@@@@@@@@@@@@@@@@@@@@@ begin");
		RioQerdoDebug.Log("OnException: " + e.ToString());
		RioQerdoDebug.Log("@@@@@@@@@@@@@@@@@@@@@@@ end");
	}
}
