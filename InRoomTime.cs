using System;
using System.Collections;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class InRoomTime : MonoBehaviour
{
	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06000983 RID: 2435 RVA: 0x000484E4 File Offset: 0x000468E4
	public double RoomTime
	{
		get
		{
			uint roomTimestamp = (uint)this.RoomTimestamp;
			double num = roomTimestamp;
			return num / 1000.0;
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06000984 RID: 2436 RVA: 0x00048507 File Offset: 0x00046907
	public int RoomTimestamp
	{
		get
		{
			return (!PhotonNetwork.inRoom) ? 0 : (PhotonNetwork.ServerTimestamp - this.roomStartTimestamp);
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000985 RID: 2437 RVA: 0x00048525 File Offset: 0x00046925
	public bool IsRoomTimeSet
	{
		get
		{
			return PhotonNetwork.inRoom && PhotonNetwork.room.customProperties.ContainsKey("#rt");
		}
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00048548 File Offset: 0x00046948
	internal IEnumerator SetRoomStartTimestamp()
	{
		if (this.IsRoomTimeSet || !PhotonNetwork.isMasterClient)
		{
			yield break;
		}
		if (PhotonNetwork.ServerTimestamp == 0)
		{
			yield return 0;
		}
		ExitGames.Client.Photon.Hashtable startTimeProp = new ExitGames.Client.Photon.Hashtable();
		startTimeProp["#rt"] = PhotonNetwork.ServerTimestamp;
		PhotonNetwork.room.SetCustomProperties(startTimeProp, null, false);
		yield break;
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00048563 File Offset: 0x00046963
	public void OnJoinedRoom()
	{
		base.StartCoroutine("SetRoomStartTimestamp");
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x00048571 File Offset: 0x00046971
	public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
	{
		base.StartCoroutine("SetRoomStartTimestamp");
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0004857F File Offset: 0x0004697F
	public void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
	{
		if (propertiesThatChanged.ContainsKey("#rt"))
		{
			this.roomStartTimestamp = (int)propertiesThatChanged["#rt"];
		}
	}

	// Token: 0x04000882 RID: 2178
	private int roomStartTimestamp;

	// Token: 0x04000883 RID: 2179
	private const string StartTimeKey = "#rt";
}
