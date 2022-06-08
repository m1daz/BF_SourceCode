using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp.buddy;
using com.shephertz.app42.paas.sdk.csharp.user;
using UnityEngine;

// Token: 0x02000473 RID: 1139
public class GGCloudServiceConstant : MonoBehaviour
{
	// Token: 0x06002123 RID: 8483 RVA: 0x000F6D57 File Offset: 0x000F5157
	private void Awake()
	{
		GGCloudServiceConstant.mInstance = this;
	}

	// Token: 0x040021CF RID: 8655
	public static GGCloudServiceConstant mInstance;

	// Token: 0x040021D0 RID: 8656
	public string mApiKey = "_2b9e6b15b00ed75bb5b7a3fbd8ed6daaa47e8a37f83cb82905b37343ced5ee70";

	// Token: 0x040021D1 RID: 8657
	public string mSecretKey = "6459dd8fc383756fbaaa215854188f2117695b446c760c06284bbf37c3989508";

	// Token: 0x040021D2 RID: 8658
	[HideInInspector]
	public string mUserName = string.Empty;

	// Token: 0x040021D3 RID: 8659
	public string mEmailId = string.Empty;

	// Token: 0x040021D4 RID: 8660
	[HideInInspector]
	public string mSessionId;

	// Token: 0x040021D5 RID: 8661
	public string mGroupName;

	// Token: 0x040021D6 RID: 8662
	public string mDBName = "PLAYER";

	// Token: 0x040021D7 RID: 8663
	public string mCollectionName;

	// Token: 0x040021D8 RID: 8664
	public string mNewMessageCollectionName;

	// Token: 0x040021D9 RID: 8665
	public string mNewFriendInfoCollectionName;

	// Token: 0x040021DA RID: 8666
	public string mUserDataCollectionName;

	// Token: 0x040021DB RID: 8667
	public string mPublicNotice;

	// Token: 0x040021DC RID: 8668
	public IList<Buddy> mBuddyRequestList = new List<Buddy>();

	// Token: 0x040021DD RID: 8669
	public IList<Buddy> mBuddyMessageList = new List<Buddy>();

	// Token: 0x040021DE RID: 8670
	public IList<Buddy> mAllFriendsList = new List<Buddy>();

	// Token: 0x040021DF RID: 8671
	public IList<Buddy> mAllGroupList = new List<Buddy>();

	// Token: 0x040021E0 RID: 8672
	public IList<Buddy> mAlFriendsInGroupList = new List<Buddy>();

	// Token: 0x040021E1 RID: 8673
	public User mUser = new User();

	// Token: 0x040021E2 RID: 8674
	public Buddy mBuddy = new Buddy();
}
