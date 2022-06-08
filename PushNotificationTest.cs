using System;
using System.Collections.Generic;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.pushNotification;
using UnityEngine;

// Token: 0x020004CE RID: 1230
public class PushNotificationTest : MonoBehaviour
{
	// Token: 0x0600227D RID: 8829 RVA: 0x000FF8A2 File Offset: 0x000FDCA2
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x0600227E RID: 8830 RVA: 0x000FF8C5 File Offset: 0x000FDCC5
	private void Update()
	{
	}

	// Token: 0x0600227F RID: 8831 RVA: 0x000FF8C8 File Offset: 0x000FDCC8
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1300f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "Create Channel ForApp"))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			this.pushNotificationService.CreateChannelForApp(this.cons.channelName, this.cons.description, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "Store Device Token"))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			this.pushNotificationService.StoreDeviceToken(this.cons.userName, this.cons.deviceToken, "<Enter_your-device_type>", this.callBack);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "Subscribe To Channel "))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			this.pushNotificationService.SubscribeToChannel(this.cons.channelName, this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "Send Push Message ToChannel "))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			this.pushNotificationService.SendPushMessageToChannel(this.cons.channelName, this.cons.message, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 200f, 200f, 30f), "SendPush Message To User"))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			this.pushNotificationService.SendPushMessageToUser(this.cons.userName, this.cons.message, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "Send PushMessage ToAll"))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			this.pushNotificationService.SendPushMessageToAll(this.cons.message, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 250f, 200f, 30f), "Send PushMessage ToAll ByType"))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			this.pushNotificationService.SendPushMessageToAllByType(this.cons.message, "<Enter_your-device_type>", this.callBack);
		}
		if (GUI.Button(new Rect(470f, 250f, 200f, 30f), "Unsubscribe Device To Channel"))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			this.pushNotificationService.UnsubscribeDeviceToChannel(this.cons.userName, this.cons.channelName, this.cons.deviceToken, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 250f, 200f, 30f), "Unsubscribe From Channel"))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			this.pushNotificationService.UnsubscribeFromChannel(this.cons.channelName, this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 250f, 240f, 30f), "Subscribe Channel With DeviceToken"))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			this.pushNotificationService.SubscribeToChannel(this.cons.userName, this.cons.channelName, this.cons.deviceToken, "<Enter_your-device_type>", this.callBack);
		}
		if (GUI.Button(new Rect(50f, 300f, 200f, 30f), "Delete Device Token"))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			this.pushNotificationService.DeleteDeviceToken(this.cons.userName, this.cons.deviceToken, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 300f, 240f, 30f), "Send PushMessage ToGroup"))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			IList<string> list = new List<string>();
			list.Add(this.cons.userName);
			list.Add(this.cons.userName1);
			this.pushNotificationService.SendPushMessageToGroup(this.cons.message, list, this.callBack);
		}
		if (GUI.Button(new Rect(520f, 300f, 200f, 30f), "Schedule Message To User"))
		{
			this.pushNotificationService = this.sp.BuildPushNotificationService();
			DateTime expiryDate = DateTime.UtcNow.AddDays(1.0);
			this.pushNotificationService.ScheduleMessageToUser(this.cons.userName, this.cons.message, expiryDate, this.callBack);
		}
	}

	// Token: 0x0400232F RID: 9007
	private Constant cons = new Constant();

	// Token: 0x04002330 RID: 9008
	private PushNotificationResponse callBack = new PushNotificationResponse();

	// Token: 0x04002331 RID: 9009
	private ServiceAPI sp;

	// Token: 0x04002332 RID: 9010
	private PushNotificationService pushNotificationService;

	// Token: 0x04002333 RID: 9011
	public string password = "password";

	// Token: 0x04002334 RID: 9012
	public int max = 2;

	// Token: 0x04002335 RID: 9013
	public int offSet = 1;

	// Token: 0x04002336 RID: 9014
	public string success;

	// Token: 0x04002337 RID: 9015
	public string box;
}
