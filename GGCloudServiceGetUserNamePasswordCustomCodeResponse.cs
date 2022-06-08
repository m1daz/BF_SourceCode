using System;
using com.shephertz.app42.paas.sdk.csharp;
using SimpleJSON;

// Token: 0x02000495 RID: 1173
public class GGCloudServiceGetUserNamePasswordCustomCodeResponse : App42CallBack
{
	// Token: 0x060021F4 RID: 8692 RVA: 0x000FB934 File Offset: 0x000F9D34
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
			if (UILoginNewDirector.mInstance != null)
			{
				UILoginNewDirector.mInstance.PopAutoRegisterSuccess(GGCloudServiceKit.mInstance.mUserNamePassword);
			}
			GGCloudServiceKit.mInstance.mNewUserName = text.ToLower();
		}
	}

	// Token: 0x060021F5 RID: 8693 RVA: 0x000FB9D3 File Offset: 0x000F9DD3
	public void OnException(Exception e)
	{
		GGCloudServiceAdapter.mInstance.ChangeToAuthServiceAPI();
		if (UILoginNewDirector.mInstance != null)
		{
			UILoginNewDirector.mInstance.PopErrorTipPanel("Auto Register Failed.");
		}
	}

	// Token: 0x060021F6 RID: 8694 RVA: 0x000FB9FE File Offset: 0x000F9DFE
	public string getResult()
	{
		return this.result;
	}

	// Token: 0x04002252 RID: 8786
	private string result = string.Empty;
}
