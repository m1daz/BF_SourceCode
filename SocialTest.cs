using System;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.social;
using UnityEngine;

// Token: 0x020004D8 RID: 1240
public class SocialTest : MonoBehaviour
{
	// Token: 0x060022A5 RID: 8869 RVA: 0x00101C24 File Offset: 0x00100024
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x060022A6 RID: 8870 RVA: 0x00101C47 File Offset: 0x00100047
	private void Update()
	{
	}

	// Token: 0x060022A7 RID: 8871 RVA: 0x00101C4C File Offset: 0x0010004C
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1300f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "LinkUserFacebookAccount"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.LinkUserFacebookAccount(this.userName, this.fbAccessToken, this.appId, this.appSecret, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "LinkUserFacebookAccountAcsTkn"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.LinkUserFacebookAccount(this.userName, this.fbAccessToken, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "UpdateFacebookStatus"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.UpdateFacebookStatus(this.userName, this.status, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "LinkUserTwitterAccount"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.LinkUserTwitterAccount(this.userName, this.accessToken, this.accessTokenSecret, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 200f, 200f, 30f), "LinkUserTwitterAccount"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.LinkUserTwitterAccount(this.userName, this.accessToken, this.accessTokenSecret, this.consumerKey, this.consumerSecret, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "UpdateTwitterStatus"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.UpdateTwitterStatus(this.userName, this.status, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 250f, 200f, 30f), "LinkUserLinkedInAccount"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.LinkUserLinkedInAccount(this.userName, this.linkedinAccessToken, this.linkedinAccessTokenSecret, this.linkedinApiKey, this.linkedinSecretKey, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 250f, 200f, 30f), "LinkUserLinkedInAccount"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.LinkUserLinkedInAccount(this.userName, this.accessToken, this.accessTokenSecret, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 250f, 200f, 30f), "UpdateLinkedInStatus"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.UpdateLinkedInStatus(this.userName, this.status, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 250f, 200f, 30f), "UpdateSocialStatusForAll"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.UpdateSocialStatusForAll(this.userName, this.status, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 300f, 200f, 30f), "GetFacebookFriendsFromLinkUser"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.GetFacebookFriendsFromLinkUser(this.userName, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 300f, 200f, 30f), "GetFacebookFriendsFromAccessToken"))
		{
			this.socialService = this.sp.BuildSocialService();
			this.socialService.GetFacebookFriendsFromAccessToken(this.accessToken, this.callBack);
		}
	}

	// Token: 0x0400235D RID: 9053
	private Constant cons = new Constant();

	// Token: 0x0400235E RID: 9054
	private SocialResponse callBack = new SocialResponse();

	// Token: 0x0400235F RID: 9055
	private ServiceAPI sp;

	// Token: 0x04002360 RID: 9056
	private SocialService socialService;

	// Token: 0x04002361 RID: 9057
	public string userName = "<Name of the user>";

	// Token: 0x04002362 RID: 9058
	public string status = "<Status that has to update on wall>";

	// Token: 0x04002363 RID: 9059
	public string appId = "<facebook_app_id>";

	// Token: 0x04002364 RID: 9060
	public string appSecret = "<facebook_app_secret>";

	// Token: 0x04002365 RID: 9061
	public string fbAccessToken = "<facebook_access_token>";

	// Token: 0x04002366 RID: 9062
	public string consumerKey = "<twitter_consumer_key>";

	// Token: 0x04002367 RID: 9063
	public string consumerSecret = "<twitter_consumer_secret>";

	// Token: 0x04002368 RID: 9064
	public string accessToken = "<twitter_access_token>";

	// Token: 0x04002369 RID: 9065
	public string accessTokenSecret = "<twitter_access_token_secret>";

	// Token: 0x0400236A RID: 9066
	public string linkedinApiKey = "<linkedin_api_key>";

	// Token: 0x0400236B RID: 9067
	public string linkedinSecretKey = "<linkedin_secret_key>";

	// Token: 0x0400236C RID: 9068
	public string linkedinAccessToken = "<linkedin_access_token>";

	// Token: 0x0400236D RID: 9069
	public string linkedinAccessTokenSecret = "<linkedin_access_token_secret>";

	// Token: 0x0400236E RID: 9070
	public string success;
}
