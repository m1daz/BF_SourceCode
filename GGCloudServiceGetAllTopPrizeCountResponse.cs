using System;
using com.shephertz.app42.paas.sdk.csharp;
using RioLog;

// Token: 0x02000483 RID: 1155
public class GGCloudServiceGetAllTopPrizeCountResponse : App42CallBack
{
	// Token: 0x060021B8 RID: 8632 RVA: 0x000FA2FE File Offset: 0x000F86FE
	public GGCloudServiceGetAllTopPrizeCountResponse(int prizenum)
	{
		this.mPrizeNum = prizenum;
	}

	// Token: 0x060021B9 RID: 8633 RVA: 0x000FA318 File Offset: 0x000F8718
	public void OnSuccess(object obj)
	{
		try
		{
			App42Response app42Response = (App42Response)obj;
			int totalRecords = app42Response.GetTotalRecords();
			int max = this.mPrizeNum;
			int num = totalRecords - this.mPrizeNum;
			if (num < 0)
			{
				num = 0;
			}
			GGCloudServiceAdapter.mInstance.GetSlotTopPrize(max, num, new GGCloudServiceSlotTopPrizeResponse());
		}
		catch (Exception ex)
		{
			RioQerdoDebug.Log(ex.ToString());
		}
	}

	// Token: 0x060021BA RID: 8634 RVA: 0x000FA388 File Offset: 0x000F8788
	public void OnException(Exception e)
	{
		RioQerdoDebug.Log("Exception is : " + e.ToString());
	}

	// Token: 0x0400223D RID: 8765
	public int mPrizeNum = 20;
}
