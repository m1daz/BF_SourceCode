using System;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x0200014E RID: 334
public static class TeamExtensions
{
	// Token: 0x060009C5 RID: 2501 RVA: 0x00049890 File Offset: 0x00047C90
	public static PunTeams.Team GetTeam(this PhotonPlayer player)
	{
		object obj;
		if (player.customProperties.TryGetValue("team", out obj))
		{
			return (PunTeams.Team)obj;
		}
		return PunTeams.Team.noteam;
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x000498BC File Offset: 0x00047CBC
	public static void SetTeam(this PhotonPlayer player, PunTeams.Team team)
	{
		if (!PhotonNetwork.connectedAndReady)
		{
			Debug.LogWarning("JoinTeam was called in state: " + PhotonNetwork.connectionStateDetailed + ". Not connectedAndReady.");
			return;
		}
		PunTeams.Team team2 = player.GetTeam();
		if (team2 != team)
		{
			player.SetCustomProperties(new Hashtable
			{
				{
					"team",
					(byte)team
				}
			}, null, false);
		}
	}
}
