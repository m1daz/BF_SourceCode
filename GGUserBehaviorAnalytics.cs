using System;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine;

// Token: 0x020004DE RID: 1246
public class GGUserBehaviorAnalytics : MonoBehaviour
{
	// Token: 0x060022B8 RID: 8888 RVA: 0x00103406 File Offset: 0x00101806
	public static void SetUBAEvent(string moduleName, string eventName)
	{
		GGCloudServiceAdapter.mInstance.mLogService.SetEvent(eventName, new GGUserBehaviorAnalytics.UnityCallBackRyan());
	}

	// Token: 0x060022B9 RID: 8889 RVA: 0x00103420 File Offset: 0x00101820
	private void OnGUI()
	{
		if (GUI.Button(new Rect(0f, 100f, 100f, 100f), "IntoShop"))
		{
			GGUserBehaviorAnalytics.SetUBAEvent("HomeShopping", "IntoShop");
		}
		if (GUI.Button(new Rect(0f, 200f, 100f, 100f), "IntoIAP"))
		{
			GGUserBehaviorAnalytics.SetUBAEvent("HomeShopping", "IntoIAP");
		}
		if (GUI.Button(new Rect(0f, 300f, 100f, 100f), "PurchaseIAP"))
		{
			GGUserBehaviorAnalytics.SetUBAEvent("HomeShopping", "PurchaseIAP");
		}
		if (GUI.Button(new Rect(0f, 400f, 100f, 100f), "PurchaseSuccessful"))
		{
			GGUserBehaviorAnalytics.SetUBAEvent("HomeShopping", "PurchaseSuccessful");
		}
	}

	// Token: 0x020004DF RID: 1247
	public class UnityCallBackRyan : App42CallBack
	{
		// Token: 0x060022BB RID: 8891 RVA: 0x00103511 File Offset: 0x00101911
		public void OnSuccess(object response)
		{
			Debug.Log("Success : " + response);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00103523 File Offset: 0x00101923
		public void OnException(Exception e)
		{
			Debug.Log("Exception : " + e);
		}
	}
}
