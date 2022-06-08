using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.pushNotification;
using UnityEngine;

namespace AssemblyCSharp
{
	// Token: 0x020004CD RID: 1229
	public class PushNotificationResponse : App42CallBack
	{
		// Token: 0x06002279 RID: 8825 RVA: 0x000FF768 File Offset: 0x000FDB68
		public void OnSuccess(object response)
		{
			this.result = response.ToString();
			if (response is PushNotification)
			{
				PushNotification pushNotification = (PushNotification)response;
				Debug.Log("UserName : " + pushNotification.GetUserName());
				Debug.Log("Expiery : " + pushNotification.GetExpiry());
				Debug.Log("DeviceToken : " + pushNotification.GetDeviceToken());
				Debug.Log("pushNotification : " + pushNotification.GetMessage());
				Debug.Log("pushNotification : " + pushNotification.GetStrResponse());
				Debug.Log("pushNotification : " + pushNotification.GetTotalRecords());
				Debug.Log("pushNotification : " + pushNotification.GetType());
				Debug.Log("pushNotification : " + pushNotification.GetChannelList());
			}
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x000FF845 File Offset: 0x000FDC45
		public void OnException(Exception e)
		{
			this.result = e.ToString();
			Debug.Log("Exception : " + e);
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x000FF863 File Offset: 0x000FDC63
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x0400232E RID: 9006
		private string result = string.Empty;
	}
}
