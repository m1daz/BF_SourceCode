using System;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.log;
using UnityEngine;

// Token: 0x020004CC RID: 1228
public class LogTest : MonoBehaviour
{
	// Token: 0x06002275 RID: 8821 RVA: 0x000FEDE2 File Offset: 0x000FD1E2
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x06002276 RID: 8822 RVA: 0x000FEE05 File Offset: 0x000FD205
	private void Update()
	{
	}

	// Token: 0x06002277 RID: 8823 RVA: 0x000FEE08 File Offset: 0x000FD208
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1300f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "Info"))
		{
			App42Log.SetDebug(true);
			this.logService = this.sp.BuildLogService();
			this.logService.Info(this.msg, this.module, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "Debug"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.Debug(this.msg, this.module, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "Fatal"))
		{
			App42Log.SetDebug(true);
			this.logService = this.sp.BuildLogService();
			this.logService.Fatal(this.msg, this.module, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "Error"))
		{
			App42Log.SetDebug(true);
			this.logService = this.sp.BuildLogService();
			this.logService.Error(this.msg, this.module, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 200f, 200f, 30f), "FetchLogsByModule"))
		{
			App42Log.SetDebug(true);
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByModule(this.module, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "FetchLogsByModuleAndText"))
		{
			App42Log.SetDebug(true);
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByModuleAndText(this.module, this.text, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 250f, 200f, 30f), "SetEvent"))
		{
			App42Log.SetDebug(true);
			this.logService = this.sp.BuildLogService();
			this.logService.SetEvent(this.eventName, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 250f, 200f, 30f), "FetchLogsByInfo"))
		{
			App42Log.SetDebug(true);
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByInfo(this.callBack);
		}
		if (GUI.Button(new Rect(680f, 250f, 200f, 30f), "FetchLogsByDebug"))
		{
			App42Log.SetDebug(true);
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByDebug(this.callBack);
		}
		if (GUI.Button(new Rect(890f, 250f, 200f, 30f), "FetchLogsByError"))
		{
			App42Log.SetDebug(true);
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByError(this.callBack);
		}
		if (GUI.Button(new Rect(50f, 300f, 200f, 30f), "FetchLogsByFatal"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByFatal(this.callBack);
		}
		if (GUI.Button(new Rect(260f, 300f, 200f, 30f), "FetchLogByDateRange"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogByDateRange(this.startDate, this.endDate, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 300f, 200f, 30f), "FetchLogsByInfoByPaging"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByInfo(this.max, this.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 300f, 200f, 30f), "FetchLogsByDebugByPaging"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByDebug(this.max, this.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 300f, 200f, 30f), "FetchLogsByErrorByPaging"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByError(this.max, this.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 350f, 200f, 30f), "FetchLogsByFatalByPaging"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByFatal(this.max, this.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 350f, 200f, 30f), "FetchLogsByModuleByPaging"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByModule(this.module, this.max, this.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 350f, 200f, 30f), "FetchLogsByModuleAndTextByPaging"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsByModuleAndText(this.module, this.text, this.max, this.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 350f, 200f, 30f), "FetchLogByDateRangeByPaging"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogByDateRange(this.startDate, this.endDate, this.max, this.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 350f, 200f, 30f), "FetchLogsCountByModule"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsCountByModule(this.module, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 400f, 200f, 30f), "FetchLogsCountByModuleAndText"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsCountByModuleAndText(this.module, this.text, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 400f, 200f, 30f), "FetchLogsCountByInfo"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsCountByInfo(this.callBack);
		}
		if (GUI.Button(new Rect(470f, 400f, 200f, 30f), "FetchLogsCountByDebug"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsCountByDebug(this.callBack);
		}
		if (GUI.Button(new Rect(680f, 400f, 200f, 30f), "FetchLogsCountByError"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsCountByError(this.callBack);
		}
		if (GUI.Button(new Rect(890f, 400f, 200f, 30f), "FetchLogsCountByFatal"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogsCountByFatal(this.callBack);
		}
		if (GUI.Button(new Rect(50f, 450f, 200f, 30f), "FetchLogCountByDateRange"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.FetchLogCountByDateRange(this.startDate, this.endDate, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 450f, 200f, 30f), "SetEvent"))
		{
			this.logService = this.sp.BuildLogService();
			this.logService.SetEvent(this.module, this.eventName, this.callBack);
		}
	}

	// Token: 0x0400231F RID: 8991
	private Constant cons = new Constant();

	// Token: 0x04002320 RID: 8992
	private LogResponse callBack = new LogResponse();

	// Token: 0x04002321 RID: 8993
	private ServiceAPI sp;

	// Token: 0x04002322 RID: 8994
	private LogService logService;

	// Token: 0x04002323 RID: 8995
	public string userName = "Aks" + DateTime.Now.Ticks;

	// Token: 0x04002324 RID: 8996
	public string module = "LogModule";

	// Token: 0x04002325 RID: 8997
	public string eventName = "LogEvent";

	// Token: 0x04002326 RID: 8998
	public string text = "LogTEXT";

	// Token: 0x04002327 RID: 8999
	public string level = "LogLEVEL";

	// Token: 0x04002328 RID: 9000
	public string msg = "Hi I M Using App42 Log For Unity3D";

	// Token: 0x04002329 RID: 9001
	public DateTime startDate = DateTime.Now;

	// Token: 0x0400232A RID: 9002
	public DateTime endDate = DateTime.Now;

	// Token: 0x0400232B RID: 9003
	public int max = 2;

	// Token: 0x0400232C RID: 9004
	public int offSet = 1;

	// Token: 0x0400232D RID: 9005
	public string success;
}
