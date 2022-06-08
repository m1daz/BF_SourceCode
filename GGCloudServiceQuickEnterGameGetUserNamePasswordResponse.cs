using System;
using com.shephertz.app42.paas.sdk.csharp;
using SimpleJSON;

// Token: 0x02000494 RID: 1172
public class GGCloudServiceQuickEnterGameGetUserNamePasswordResponse : App42CallBack
{
	// Token: 0x060021F0 RID: 8688 RVA: 0x000FB86C File Offset: 0x000F9C6C
	public void OnSuccess(object response)
	{
		if (response is JObject)
		{
			JObject jobject = (JObject)response;
			string text = jobject["UserName"];
			string password = jobject["Password"];
			GGCloudServiceKit.mInstance.mUserNamePassword.UserName = text;
			GGCloudServiceKit.mInstance.mUserNamePassword.Password = password;
			GGCloudServiceAdapter.mInstance.ChangeToAuthServiceAPI();
			GGCloudServiceCreate.mInstance.AutoQuickEnterGame(text, password);
			GGCloudServiceKit.mInstance.mNewUserName = text;
		}
	}

	// Token: 0x060021F1 RID: 8689 RVA: 0x000FB8EE File Offset: 0x000F9CEE
	public void OnException(Exception e)
	{
		GGCloudServiceAdapter.mInstance.ChangeToAuthServiceAPI();
		if (UILoginNewDirector.mInstance != null)
		{
			UILoginNewDirector.mInstance.PopErrorTipPanel("Register failed.");
		}
	}

	// Token: 0x060021F2 RID: 8690 RVA: 0x000FB919 File Offset: 0x000F9D19
	public string getResult()
	{
		return this.result;
	}

	// Token: 0x04002251 RID: 8785
	private string result = string.Empty;
}
