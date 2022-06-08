using System;
using com.shephertz.app42.paas.sdk.csharp;

// Token: 0x0200049A RID: 1178
public class GGCloudServiceCreateUserResponse : App42CallBack
{
	// Token: 0x06002206 RID: 8710 RVA: 0x000FBCBF File Offset: 0x000FA0BF
	public void OnSuccess(object obj)
	{
		GGCloudServiceKit.mInstance.CreateUserAction_CreateUserOK();
	}

	// Token: 0x06002207 RID: 8711 RVA: 0x000FBCCB File Offset: 0x000FA0CB
	public void OnException(Exception ex)
	{
		GGCloudServiceKit.mInstance.CreateUserAction_CreateUserException(ex);
	}
}
