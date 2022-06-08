using System;
using System.Collections.Generic;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.buddy;
using com.shephertz.app42.paas.sdk.csharp.geo;
using UnityEngine;

// Token: 0x020004C5 RID: 1221
public class BuddyTest : MonoBehaviour
{
	// Token: 0x0600225C RID: 8796 RVA: 0x000FDD85 File Offset: 0x000FC185
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x0600225D RID: 8797 RVA: 0x000FDDA8 File Offset: 0x000FC1A8
	private void Update()
	{
	}

	// Token: 0x0600225E RID: 8798 RVA: 0x000FDDAC File Offset: 0x000FC1AC
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callback.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1300f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "SendFriendRequest"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.SendFriendRequest(this.userName, this.buddyName, this.message, this.callback);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "GetFriendRequest"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.GetFriendRequest(this.buddyName, this.callback);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "AcceptFriendRequest"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.AcceptFriendRequest(this.userName, this.buddyName, this.callback);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "RejectFriendRequest"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.RejectFriendRequest(this.userName, this.buddyName, this.callback);
		}
		if (GUI.Button(new Rect(890f, 200f, 200f, 30f), "GetAllFriends"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.GetAllFriends(this.userName, this.callback);
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "CreateGroupByUser"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.CreateGroupByUser(this.userName, this.groupName, this.callback);
		}
		if (GUI.Button(new Rect(260f, 250f, 200f, 30f), "AddFriendToGroup"))
		{
			IList<string> list = new List<string>();
			list.Add("379368234");
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.AddFriendToGroup(this.userName, this.groupName, list, this.callback);
		}
		if (GUI.Button(new Rect(470f, 250f, 200f, 30f), "BlockUser"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.BlockUser(this.userName, this.buddyName, this.callback);
		}
		if (GUI.Button(new Rect(680f, 250f, 200f, 30f), "UnblockUser"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.UnblockUser(this.userName, this.buddyName, this.callback);
		}
		if (GUI.Button(new Rect(890f, 250f, 200f, 30f), "BlockFriendRequest"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.BlockFriendRequest(this.userName, this.buddyName, this.callback);
		}
		if (GUI.Button(new Rect(50f, 300f, 200f, 30f), "GetAllGroups"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.GetAllGroups(this.userName, this.callback);
		}
		if (GUI.Button(new Rect(260f, 300f, 200f, 30f), "GetAllFriendsInGroup"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.GetAllFriendsInGroup(this.userName, this.ownerName, this.groupName, this.callback);
		}
		if (GUI.Button(new Rect(470f, 300f, 200f, 30f), "CheckedInGeoLocation"))
		{
			GeoPoint geoPoint = new GeoPoint();
			geoPoint.SetLng(28.4091958);
			geoPoint.SetLat(77.04781120000007);
			geoPoint.SetMarker("Himalya");
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.CheckedInGeoLocation(this.userName, geoPoint, this.callback);
		}
		if (GUI.Button(new Rect(680f, 300f, 200f, 30f), "GetFriendsByLocation"))
		{
			double latitude = 77.04781120000007;
			double longitude = 28.4091958;
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.GetFriendsByLocation(this.userName, latitude, longitude, this.maxDistance, 5, this.callback);
		}
		if (GUI.Button(new Rect(890f, 300f, 200f, 30f), "SendMessageToGroup"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.SendMessageToGroup(this.userName, this.ownerName, this.groupName, this.message, this.callback);
		}
		if (GUI.Button(new Rect(50f, 350f, 200f, 30f), "SendMessageToFriend"))
		{
			Debug.Log(string.Concat(new string[]
			{
				"userName: ",
				this.userName,
				" buddyName: ",
				this.buddyName,
				" message: ",
				this.message
			}));
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.SendMessageToFriend(this.userName, this.buddyName, this.message, this.callback);
		}
		if (GUI.Button(new Rect(260f, 350f, 200f, 30f), "SendMessageToFriends"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.SendMessageToFriends(this.userName, this.message, this.callback);
		}
		if (GUI.Button(new Rect(470f, 350f, 200f, 30f), "GetAllMessages"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.GetAllMessages("379368234", this.callback);
		}
		if (GUI.Button(new Rect(680f, 350f, 200f, 30f), "GetAllMessagesFromBuddy"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.GetAllMessagesFromBuddy(this.userName, this.buddyName, this.callback);
		}
		if (GUI.Button(new Rect(890f, 350f, 200f, 30f), "GetAllMessagesFromGroup"))
		{
			this.buddyService = this.sp.BuildBuddyService();
			this.buddyService.GetAllMessagesFromGroup(this.userName, this.ownerName, this.groupName, this.callback);
		}
	}

	// Token: 0x040022DB RID: 8923
	private Constant cons = new Constant();

	// Token: 0x040022DC RID: 8924
	private ServiceAPI sp;

	// Token: 0x040022DD RID: 8925
	private BuddyService buddyService;

	// Token: 0x040022DE RID: 8926
	private BuddyResponse callback = new BuddyResponse();

	// Token: 0x040022DF RID: 8927
	public string userName = "379368233";

	// Token: 0x040022E0 RID: 8928
	public string buddyName = "379368237";

	// Token: 0x040022E1 RID: 8929
	public string buddyName1 = "Aks0";

	// Token: 0x040022E2 RID: 8930
	public string buddyName2 = "Juno0";

	// Token: 0x040022E3 RID: 8931
	public string message = "nice to meet you!";

	// Token: 0x040022E4 RID: 8932
	public string groupName = "379368233Group";

	// Token: 0x040022E5 RID: 8933
	public string ownerName = "379368233";

	// Token: 0x040022E6 RID: 8934
	public double maxDistance = 1000.0;

	// Token: 0x040022E7 RID: 8935
	public string success;
}
