using System;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.session;
using UnityEngine;

// Token: 0x020004D6 RID: 1238
public class SessionTest : MonoBehaviour
{
	// Token: 0x0600229D RID: 8861 RVA: 0x001015DC File Offset: 0x000FF9DC
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x0600229E RID: 8862 RVA: 0x001015FF File Offset: 0x000FF9FF
	private void Update()
	{
	}

	// Token: 0x0600229F RID: 8863 RVA: 0x00101604 File Offset: 0x000FFA04
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1300f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "GetSession"))
		{
			this.sessionService = this.sp.BuildSessionService();
			this.sessionService.GetSession(this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "GetSessionIsCreate"))
		{
			this.sessionService = this.sp.BuildSessionService();
			this.sessionService.GetSession(this.cons.userName, this.cons.isCreate, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "Invalidate"))
		{
			this.sessionService = this.sp.BuildSessionService();
			this.sessionService.Invalidate(this.cons.sessionId, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "SetAttribute"))
		{
			this.sessionService = this.sp.BuildSessionService();
			this.sessionService.SetAttribute(this.cons.sessionId, this.cons.attributeName, this.cons.attributeValue, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 200f, 200f, 30f), "GetAttribute"))
		{
			this.sessionService = this.sp.BuildSessionService();
			this.sessionService.GetAttribute(this.cons.sessionId, this.cons.attributeName, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "GetAllAttributes"))
		{
			this.sessionService = this.sp.BuildSessionService();
			this.sessionService.GetAllAttributes(this.cons.sessionId, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 250f, 200f, 30f), "RemoveAttribute"))
		{
			this.sessionService = this.sp.BuildSessionService();
			this.sessionService.RemoveAttribute(this.cons.sessionId, this.cons.attributeName, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 250f, 200f, 30f), "RemoveAllAttributes"))
		{
			this.sessionService = this.sp.BuildSessionService();
			this.sessionService.RemoveAllAttributes(this.cons.sessionId, this.callBack);
		}
	}

	// Token: 0x04002356 RID: 9046
	private Constant cons = new Constant();

	// Token: 0x04002357 RID: 9047
	private ServiceAPI sp;

	// Token: 0x04002358 RID: 9048
	private SessionService sessionService;

	// Token: 0x04002359 RID: 9049
	public string sessionId = string.Empty;

	// Token: 0x0400235A RID: 9050
	public string success;

	// Token: 0x0400235B RID: 9051
	private SessionResponse callBack = new SessionResponse();
}
