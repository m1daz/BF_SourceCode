using System;
using ExitGames.Client.Photon;

// Token: 0x02000151 RID: 337
public static class TurnExtensions
{
	// Token: 0x060009DB RID: 2523 RVA: 0x00049C10 File Offset: 0x00048010
	public static void SetTurn(this Room room, int turn, bool setStartTime = false)
	{
		if (room == null || room.customProperties == null)
		{
			return;
		}
		Hashtable hashtable = new Hashtable();
		hashtable[TurnExtensions.TurnPropKey] = turn;
		if (setStartTime)
		{
			hashtable[TurnExtensions.TurnStartPropKey] = PhotonNetwork.ServerTimestamp;
		}
		room.SetCustomProperties(hashtable, null, false);
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x00049C6A File Offset: 0x0004806A
	public static int GetTurn(this RoomInfo room)
	{
		if (room == null || room.customProperties == null || !room.customProperties.ContainsKey(TurnExtensions.TurnPropKey))
		{
			return 0;
		}
		return (int)room.customProperties[TurnExtensions.TurnPropKey];
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x00049CA9 File Offset: 0x000480A9
	public static int GetTurnStart(this RoomInfo room)
	{
		if (room == null || room.customProperties == null || !room.customProperties.ContainsKey(TurnExtensions.TurnStartPropKey))
		{
			return 0;
		}
		return (int)room.customProperties[TurnExtensions.TurnStartPropKey];
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x00049CE8 File Offset: 0x000480E8
	public static int GetFinishedTurn(this PhotonPlayer player)
	{
		Room room = PhotonNetwork.room;
		if (room == null || room.customProperties == null || !room.customProperties.ContainsKey(TurnExtensions.TurnPropKey))
		{
			return 0;
		}
		string key = TurnExtensions.FinishedTurnPropKey + player.ID;
		return (int)room.customProperties[key];
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x00049D4C File Offset: 0x0004814C
	public static void SetFinishedTurn(this PhotonPlayer player, int turn)
	{
		Room room = PhotonNetwork.room;
		if (room == null || room.customProperties == null)
		{
			return;
		}
		string key = TurnExtensions.FinishedTurnPropKey + player.ID;
		Hashtable hashtable = new Hashtable();
		hashtable[key] = turn;
		room.SetCustomProperties(hashtable, null, false);
	}

	// Token: 0x040008B4 RID: 2228
	public static readonly string TurnPropKey = "Turn";

	// Token: 0x040008B5 RID: 2229
	public static readonly string TurnStartPropKey = "TStart";

	// Token: 0x040008B6 RID: 2230
	public static readonly string FinishedTurnPropKey = "FToA";
}
