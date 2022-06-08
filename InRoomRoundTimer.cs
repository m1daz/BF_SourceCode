using System;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x0200013B RID: 315
public class InRoomRoundTimer : MonoBehaviour
{
	// Token: 0x0600097C RID: 2428 RVA: 0x0004832C File Offset: 0x0004672C
	private void StartRoundNow()
	{
		if (PhotonNetwork.time < 9.999999747378752E-05)
		{
			this.startRoundWhenTimeIsSynced = true;
			return;
		}
		this.startRoundWhenTimeIsSynced = false;
		Hashtable hashtable = new Hashtable();
		hashtable["st"] = PhotonNetwork.time;
		PhotonNetwork.room.SetCustomProperties(hashtable, null, false);
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x00048383 File Offset: 0x00046783
	public void OnJoinedRoom()
	{
		if (PhotonNetwork.isMasterClient)
		{
			this.StartRoundNow();
		}
		else
		{
			Debug.Log("StartTime already set: " + PhotonNetwork.room.customProperties.ContainsKey("st"));
		}
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x000483C2 File Offset: 0x000467C2
	public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
	{
		if (propertiesThatChanged.ContainsKey("st"))
		{
			this.StartTime = (double)propertiesThatChanged["st"];
		}
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x000483EA File Offset: 0x000467EA
	public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
	{
		if (!PhotonNetwork.room.customProperties.ContainsKey("st"))
		{
			Debug.Log("The new master starts a new round, cause we didn't start yet.");
			this.StartRoundNow();
		}
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00048415 File Offset: 0x00046815
	private void Update()
	{
		if (this.startRoundWhenTimeIsSynced)
		{
			this.StartRoundNow();
		}
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00048428 File Offset: 0x00046828
	public void OnGUI()
	{
		double num = PhotonNetwork.time - this.StartTime;
		double num2 = (double)this.SecondsPerTurn - num % (double)this.SecondsPerTurn;
		int num3 = (int)(num / (double)this.SecondsPerTurn);
		GUILayout.BeginArea(this.TextPos);
		GUILayout.Label(string.Format("elapsed: {0:0.000}", num), new GUILayoutOption[0]);
		GUILayout.Label(string.Format("remaining: {0:0.000}", num2), new GUILayoutOption[0]);
		GUILayout.Label(string.Format("turn: {0:0}", num3), new GUILayoutOption[0]);
		if (GUILayout.Button("new round", new GUILayoutOption[0]))
		{
			this.StartRoundNow();
		}
		GUILayout.EndArea();
	}

	// Token: 0x0400087D RID: 2173
	public int SecondsPerTurn = 5;

	// Token: 0x0400087E RID: 2174
	public double StartTime;

	// Token: 0x0400087F RID: 2175
	public Rect TextPos = new Rect(0f, 80f, 150f, 300f);

	// Token: 0x04000880 RID: 2176
	private bool startRoundWhenTimeIsSynced;

	// Token: 0x04000881 RID: 2177
	private const string StartTimeKey = "st";
}
