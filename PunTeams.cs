using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014C RID: 332
public class PunTeams : MonoBehaviour
{
	// Token: 0x060009C1 RID: 2497 RVA: 0x00049740 File Offset: 0x00047B40
	public void Start()
	{
		PunTeams.PlayersPerTeam = new Dictionary<PunTeams.Team, List<PhotonPlayer>>();
		Array values = Enum.GetValues(typeof(PunTeams.Team));
		IEnumerator enumerator = values.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				PunTeams.PlayersPerTeam[(PunTeams.Team)obj] = new List<PhotonPlayer>();
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x000497C4 File Offset: 0x00047BC4
	public void OnJoinedRoom()
	{
		this.UpdateTeams();
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x000497CC File Offset: 0x00047BCC
	public void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
	{
		this.UpdateTeams();
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x000497D4 File Offset: 0x00047BD4
	public void UpdateTeams()
	{
		Array values = Enum.GetValues(typeof(PunTeams.Team));
		IEnumerator enumerator = values.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				PunTeams.PlayersPerTeam[(PunTeams.Team)obj].Clear();
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
		{
			PhotonPlayer photonPlayer = PhotonNetwork.playerList[i];
			PunTeams.Team team = photonPlayer.GetTeam();
			PunTeams.PlayersPerTeam[team].Add(photonPlayer);
		}
	}

	// Token: 0x040008A6 RID: 2214
	public static Dictionary<PunTeams.Team, List<PhotonPlayer>> PlayersPerTeam;

	// Token: 0x040008A7 RID: 2215
	public const string TeamPlayerProp = "team";

	// Token: 0x0200014D RID: 333
	public enum Team : byte
	{
		// Token: 0x040008A9 RID: 2217
		none,
		// Token: 0x040008AA RID: 2218
		noteam,
		// Token: 0x040008AB RID: 2219
		cop,
		// Token: 0x040008AC RID: 2220
		robber
	}
}
