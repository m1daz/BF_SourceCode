using System;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.email;
using UnityEngine;

// Token: 0x020004CA RID: 1226
public class EmailTest : MonoBehaviour
{
	// Token: 0x0600226D RID: 8813 RVA: 0x000FEA18 File Offset: 0x000FCE18
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x0600226E RID: 8814 RVA: 0x000FEA3B File Offset: 0x000FCE3B
	private void Update()
	{
	}

	// Token: 0x0600226F RID: 8815 RVA: 0x000FEA40 File Offset: 0x000FCE40
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1300f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "CreateMailConfiguration"))
		{
			App42Log.SetDebug(true);
			this.emailService = this.sp.BuildEmailService();
			this.emailService.CreateMailConfiguration(this.cons.emailHost, this.cons.emailPort, this.cons.mailId, this.cons.emailPassword, this.cons.isSSL, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "RemoveEmailConfiguration"))
		{
			this.emailService = this.sp.BuildEmailService();
			this.emailService.RemoveEmailConfiguration(this.cons.mailId, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "GetEmailConfigurations"))
		{
			this.emailService = this.sp.BuildEmailService();
			this.emailService.GetEmailConfigurations(this.callBack);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "SendMail"))
		{
			this.emailService = this.sp.BuildEmailService();
			this.emailService.SendMail(this.cons.sendTo, this.cons.sendSubject, this.cons.sendMsg, this.cons.mailId, EmailMIME.PLAIN_TEXT_MIME_TYPE, this.callBack);
		}
	}

	// Token: 0x04002319 RID: 8985
	private Constant cons = new Constant();

	// Token: 0x0400231A RID: 8986
	private ServiceAPI sp;

	// Token: 0x0400231B RID: 8987
	private EmailService emailService;

	// Token: 0x0400231C RID: 8988
	public string success;

	// Token: 0x0400231D RID: 8989
	private EmailResponse callBack = new EmailResponse();
}
