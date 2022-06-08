using System;
using com.shephertz.app42.paas.sdk.csharp;
using RioLog;

// Token: 0x0200048C RID: 1164
public class GGCloudServiceScoreBoardEditScoreValueByIdResponse : App42CallBack
{
	// Token: 0x060021D6 RID: 8662 RVA: 0x000FB0A5 File Offset: 0x000F94A5
	public void OnSuccess(object response)
	{
	}

	// Token: 0x060021D7 RID: 8663 RVA: 0x000FB0A7 File Offset: 0x000F94A7
	public void OnException(Exception e)
	{
		RioQerdoDebug.Log("Exception : " + e);
	}
}
