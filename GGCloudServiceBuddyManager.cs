using System;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp.buddy;
using UnityEngine;

// Token: 0x02000472 RID: 1138
public class GGCloudServiceBuddyManager : MonoBehaviour
{
	// Token: 0x0600211E RID: 8478 RVA: 0x000F6CB0 File Offset: 0x000F50B0
	private void Awake()
	{
		GGCloudServiceBuddyManager.mInstance = this;
	}

	// Token: 0x0600211F RID: 8479 RVA: 0x000F6CB8 File Offset: 0x000F50B8
	private void Start()
	{
	}

	// Token: 0x06002120 RID: 8480 RVA: 0x000F6CBA File Offset: 0x000F50BA
	private void Update()
	{
	}

	// Token: 0x06002121 RID: 8481 RVA: 0x000F6CBC File Offset: 0x000F50BC
	public IList<GGCloudServiceGroupFriends> GetGroupsFriendsList()
	{
		return null;
	}

	// Token: 0x040021CB RID: 8651
	public static GGCloudServiceBuddyManager mInstance;

	// Token: 0x040021CC RID: 8652
	public IList<Buddy> mFriendsList = new List<Buddy>();

	// Token: 0x040021CD RID: 8653
	public IList<GGCloudServiceGroupFriends> mGroupsFriendsList = new List<GGCloudServiceGroupFriends>();

	// Token: 0x040021CE RID: 8654
	private IList<Buddy> mGroupsList = new List<Buddy>();
}
