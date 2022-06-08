using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;

// Token: 0x02000489 RID: 1161
public class GGCloudServiceUpdateUserNameFindDocResponse : App42CallBack
{
	// Token: 0x060021CC RID: 8652 RVA: 0x000FAF7C File Offset: 0x000F937C
	public GGCloudServiceUpdateUserNameFindDocResponse(string tusername)
	{
		this.userName = tusername;
	}

	// Token: 0x060021CD RID: 8653 RVA: 0x000FAF96 File Offset: 0x000F9396
	public void OnSuccess(object response)
	{
		GGCloudServiceKit.mInstance.UpdateUserNameRoleNameDic_CallBack((Storage)response, this.userName);
	}

	// Token: 0x060021CE RID: 8654 RVA: 0x000FAFAE File Offset: 0x000F93AE
	public void OnException(Exception e)
	{
		GGCloudServiceKit.mInstance.UpdateUserNameRoleNameDic_CallBack(null, this.userName);
	}

	// Token: 0x04002249 RID: 8777
	private string userName = string.Empty;
}
