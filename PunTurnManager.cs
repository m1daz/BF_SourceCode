using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon;
using UnityEngine;

// Token: 0x0200014F RID: 335
public class PunTurnManager : PunBehaviour
{
	// Token: 0x17000128 RID: 296
	// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0004993E File Offset: 0x00047D3E
	// (set) Token: 0x060009C9 RID: 2505 RVA: 0x0004994A File Offset: 0x00047D4A
	public int Turn
	{
		get
		{
			return PhotonNetwork.room.GetTurn();
		}
		private set
		{
			this._isOverCallProcessed = false;
			PhotonNetwork.room.SetTurn(value, true);
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x060009CA RID: 2506 RVA: 0x0004995F File Offset: 0x00047D5F
	public float ElapsedTimeInTurn
	{
		get
		{
			return (float)(PhotonNetwork.ServerTimestamp - PhotonNetwork.room.GetTurnStart()) / 1000f;
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x060009CB RID: 2507 RVA: 0x00049978 File Offset: 0x00047D78
	public float RemainingSecondsInTurn
	{
		get
		{
			return Mathf.Max(0f, this.TurnDuration - this.ElapsedTimeInTurn);
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x060009CC RID: 2508 RVA: 0x00049991 File Offset: 0x00047D91
	public bool IsCompletedByAll
	{
		get
		{
			return PhotonNetwork.room != null && this.Turn > 0 && this.finishedPlayers.Count == PhotonNetwork.room.playerCount;
		}
	}

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x060009CD RID: 2509 RVA: 0x000499C3 File Offset: 0x00047DC3
	public bool IsFinishedByMe
	{
		get
		{
			return this.finishedPlayers.Contains(PhotonNetwork.player);
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x060009CE RID: 2510 RVA: 0x000499D5 File Offset: 0x00047DD5
	public bool IsOver
	{
		get
		{
			return this.RemainingSecondsInTurn <= 0f;
		}
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x000499E7 File Offset: 0x00047DE7
	private void Start()
	{
		PhotonNetwork.OnEventCall = new PhotonNetwork.EventCallback(this.OnEvent);
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x000499FA File Offset: 0x00047DFA
	private void Update()
	{
		if (this.Turn > 0 && this.IsOver && !this._isOverCallProcessed)
		{
			this._isOverCallProcessed = true;
			this.TurnManagerListener.OnTurnTimeEnds(this.Turn);
		}
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x00049A36 File Offset: 0x00047E36
	public void BeginTurn()
	{
		this.Turn++;
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x00049A48 File Offset: 0x00047E48
	public void SendMove(object move, bool finished)
	{
		if (this.IsFinishedByMe)
		{
			Debug.LogWarning("Can't SendMove. Turn is finished by this player.");
			return;
		}
		Hashtable hashtable = new Hashtable();
		hashtable.Add("turn", this.Turn);
		hashtable.Add("move", move);
		byte eventCode = (!finished) ? 1 : 2;
		PhotonNetwork.RaiseEvent(eventCode, hashtable, true, new RaiseEventOptions
		{
			CachingOption = EventCaching.AddToRoomCache
		});
		if (finished)
		{
			PhotonNetwork.player.SetFinishedTurn(this.Turn);
		}
		this.OnEvent(eventCode, hashtable, PhotonNetwork.player.ID);
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x00049AE0 File Offset: 0x00047EE0
	public bool GetPlayerFinishedTurn(PhotonPlayer player)
	{
		return player != null && this.finishedPlayers != null && this.finishedPlayers.Contains(player);
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x00049B08 File Offset: 0x00047F08
	public void OnEvent(byte eventCode, object content, int senderId)
	{
		PhotonPlayer photonPlayer = PhotonPlayer.Find(senderId);
		if (eventCode != 1)
		{
			if (eventCode == 2)
			{
				Hashtable hashtable = content as Hashtable;
				int num = (int)hashtable["turn"];
				object move = hashtable["move"];
				if (num == this.Turn)
				{
					this.finishedPlayers.Add(photonPlayer);
					this.TurnManagerListener.OnPlayerFinished(photonPlayer, num, move);
				}
				if (this.IsCompletedByAll)
				{
					this.TurnManagerListener.OnTurnCompleted(this.Turn);
				}
			}
		}
		else
		{
			Hashtable hashtable2 = content as Hashtable;
			int turn = (int)hashtable2["turn"];
			object move2 = hashtable2["move"];
			this.TurnManagerListener.OnPlayerMove(photonPlayer, turn, move2);
		}
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x00049BDA File Offset: 0x00047FDA
	public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
	{
		if (propertiesThatChanged.ContainsKey("Turn"))
		{
			this._isOverCallProcessed = false;
			this.finishedPlayers.Clear();
			this.TurnManagerListener.OnTurnBegins(this.Turn);
		}
	}

	// Token: 0x040008AD RID: 2221
	public float TurnDuration = 20f;

	// Token: 0x040008AE RID: 2222
	public IPunTurnManagerCallbacks TurnManagerListener;

	// Token: 0x040008AF RID: 2223
	private readonly HashSet<PhotonPlayer> finishedPlayers = new HashSet<PhotonPlayer>();

	// Token: 0x040008B0 RID: 2224
	public const byte TurnManagerEventOffset = 0;

	// Token: 0x040008B1 RID: 2225
	public const byte EvMove = 1;

	// Token: 0x040008B2 RID: 2226
	public const byte EvFinalMove = 2;

	// Token: 0x040008B3 RID: 2227
	private bool _isOverCallProcessed;
}
