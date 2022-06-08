using System;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;

// Token: 0x0200049C RID: 1180
public class GGCloudServiceFindUserProfileResponse : App42CallBack
{
	// Token: 0x0600220B RID: 8715 RVA: 0x000FBCF4 File Offset: 0x000FA0F4
	public GGCloudServiceFindUserProfileResponse(string tnewFriendUserName)
	{
		this.newFriendUserName = tnewFriendUserName;
	}

	// Token: 0x0600220C RID: 8716 RVA: 0x000FBD0E File Offset: 0x000FA10E
	public void OnSuccess(object obj)
	{
		GGCloudServiceKit.mInstance.GetNewFriendInfoAction_CallBack2((Storage)obj, this.newFriendUserName);
	}

	// Token: 0x0600220D RID: 8717 RVA: 0x000FBD26 File Offset: 0x000FA126
	public void OnException(Exception ex)
	{
	}

	// Token: 0x04002256 RID: 8790
	public string newFriendUserName = string.Empty;
}
