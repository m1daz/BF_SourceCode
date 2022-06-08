using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.social;

namespace AssemblyCSharp
{
	// Token: 0x020004D7 RID: 1239
	public class SocialResponse : App42CallBack
	{
		// Token: 0x060022A1 RID: 8865 RVA: 0x00101950 File Offset: 0x000FFD50
		public void OnSuccess(object social)
		{
			this.result = social.ToString();
			if (social is Social)
			{
				Social social2 = (Social)social;
				if (social2.GetFacebookAccessToken() != null)
				{
					App42Log.Console("AccessToken : " + social2.GetFacebookAccessToken());
					App42Log.Console("BuddyName : " + social2.GetFacebookAppId());
					App42Log.Console("BuddyName : " + social2.GetFacebookAppSecret());
					if (social2.GetFriendList() != null)
					{
						IList<Social.Friends> friendList = social2.GetFriendList();
						for (int i = 0; i < friendList.Count; i++)
						{
							App42Log.Console("Friends Name is  : " + friendList[i].GetName());
							App42Log.Console("Friends Id is : " + friendList[i].GetId());
							App42Log.Console("Friends AppInstalled is : " + friendList[i].GetInstalled());
							App42Log.Console("Friends Image is : " + friendList[i].GetPicture());
						}
					}
				}
				else if (social2.GetTwitterAccessToken() != null)
				{
					App42Log.Console("AccessToken is : " + social2.GetTwitterAccessToken());
					App42Log.Console("AccessTokenSecret is : " + social2.GetTwitterAccessTokenSecret());
					App42Log.Console("TwitterConsumerKey is : " + social2.GetTwitterConsumerKey());
					App42Log.Console("TwitterConsumerSecret is : " + social2.GetTwitterConsumerSecret());
				}
				else if (social2.GetLinkedinAccessToken() != null)
				{
					App42Log.Console("AccessToken is : " + social2.GetLinkedinAccessToken());
					App42Log.Console("AccessTokenSecret is : " + social2.GetLinkedinAccessTokenSecret());
					App42Log.Console("LinkedInApiKey is : " + social2.GetLinkedinApiKey());
					App42Log.Console("LinkedinSecretKey is : " + social2.GetLinkedinSecretKey());
				}
				else
				{
					App42Log.Console("UserName is : " + social2.GetUserName());
					App42Log.Console("Staus is : " + social2.GetStatus());
				}
			}
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x00101B56 File Offset: 0x000FFF56
		public void OnException(Exception e)
		{
			this.result = e.ToString();
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x00101B64 File Offset: 0x000FFF64
		public string getResult()
		{
			return this.result;
		}

		// Token: 0x0400235C RID: 9052
		private string result = string.Empty;
	}
}
