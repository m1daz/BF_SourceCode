using System;
using ExitGames.Client.Photon;
using UnityEngine;

// Token: 0x0200011F RID: 287
public class Room : RoomInfo
{
	// Token: 0x060008F6 RID: 2294 RVA: 0x00045DC8 File Offset: 0x000441C8
	internal Room(string roomName, RoomOptions options) : base(roomName, null)
	{
		if (options == null)
		{
			options = new RoomOptions();
		}
		this.visibleField = options.IsVisible;
		this.openField = options.IsOpen;
		this.maxPlayersField = options.MaxPlayers;
		this.autoCleanUpField = false;
		base.InternalCacheProperties(options.CustomRoomProperties);
		this.propertiesListedInLobby = options.CustomRoomPropertiesForLobby;
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00045E2D File Offset: 0x0004422D
	// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00045E35 File Offset: 0x00044235
	public new string name
	{
		get
		{
			return this.nameField;
		}
		internal set
		{
			this.nameField = value;
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00045E3E File Offset: 0x0004423E
	// (set) Token: 0x060008FA RID: 2298 RVA: 0x00045E48 File Offset: 0x00044248
	public new bool open
	{
		get
		{
			return this.openField;
		}
		set
		{
			if (!this.Equals(PhotonNetwork.room))
			{
				Debug.LogWarning("Can't set open when not in that room.");
			}
			if (value != this.openField && !PhotonNetwork.offlineMode)
			{
				PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(new Hashtable
				{
					{
						253,
						value
					}
				}, null, false);
			}
			this.openField = value;
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x060008FB RID: 2299 RVA: 0x00045EB6 File Offset: 0x000442B6
	// (set) Token: 0x060008FC RID: 2300 RVA: 0x00045EC0 File Offset: 0x000442C0
	public new bool visible
	{
		get
		{
			return this.visibleField;
		}
		set
		{
			if (!this.Equals(PhotonNetwork.room))
			{
				Debug.LogWarning("Can't set visible when not in that room.");
			}
			if (value != this.visibleField && !PhotonNetwork.offlineMode)
			{
				PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(new Hashtable
				{
					{
						254,
						value
					}
				}, null, false);
			}
			this.visibleField = value;
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x060008FD RID: 2301 RVA: 0x00045F2E File Offset: 0x0004432E
	// (set) Token: 0x060008FE RID: 2302 RVA: 0x00045F36 File Offset: 0x00044336
	public string[] propertiesListedInLobby { get; private set; }

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x060008FF RID: 2303 RVA: 0x00045F3F File Offset: 0x0004433F
	public bool autoCleanUp
	{
		get
		{
			return this.autoCleanUpField;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000900 RID: 2304 RVA: 0x00045F47 File Offset: 0x00044347
	// (set) Token: 0x06000901 RID: 2305 RVA: 0x00045F50 File Offset: 0x00044350
	public new int maxPlayers
	{
		get
		{
			return (int)this.maxPlayersField;
		}
		set
		{
			if (!this.Equals(PhotonNetwork.room))
			{
				Debug.LogWarning("Can't set MaxPlayers when not in that room.");
			}
			if (value > 255)
			{
				Debug.LogWarning("Can't set Room.MaxPlayers to: " + value + ". Using max value: 255.");
				value = 255;
			}
			if (value != (int)this.maxPlayersField && !PhotonNetwork.offlineMode)
			{
				PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(new Hashtable
				{
					{
						byte.MaxValue,
						(byte)value
					}
				}, null, false);
			}
			this.maxPlayersField = (byte)value;
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000902 RID: 2306 RVA: 0x00045FEC File Offset: 0x000443EC
	public new int playerCount
	{
		get
		{
			if (PhotonNetwork.playerList != null)
			{
				return PhotonNetwork.playerList.Length;
			}
			return 0;
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000903 RID: 2307 RVA: 0x00046001 File Offset: 0x00044401
	public string[] expectedUsers
	{
		get
		{
			return this.expectedUsersField;
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000904 RID: 2308 RVA: 0x00046009 File Offset: 0x00044409
	// (set) Token: 0x06000905 RID: 2309 RVA: 0x00046011 File Offset: 0x00044411
	protected internal int masterClientId
	{
		get
		{
			return this.masterClientIdField;
		}
		set
		{
			this.masterClientIdField = value;
		}
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x0004601C File Offset: 0x0004441C
	public void SetCustomProperties(Hashtable propertiesToSet, Hashtable expectedValues = null, bool webForward = false)
	{
		if (propertiesToSet == null)
		{
			return;
		}
		Hashtable hashtable = propertiesToSet.StripToStringKeys();
		Hashtable hashtable2 = expectedValues.StripToStringKeys();
		bool flag = hashtable2 == null || hashtable2.Count == 0;
		if (!PhotonNetwork.offlineMode)
		{
			PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(hashtable, hashtable2, webForward);
		}
		if (PhotonNetwork.offlineMode || flag)
		{
			base.InternalCacheProperties(hashtable);
			NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, new object[]
			{
				hashtable
			});
		}
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00046094 File Offset: 0x00044494
	public void SetPropertiesListedInLobby(string[] propsListedInLobby)
	{
		Hashtable hashtable = new Hashtable();
		hashtable[250] = propsListedInLobby;
		PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(hashtable, null, false);
		this.propertiesListedInLobby = propsListedInLobby;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x000460D0 File Offset: 0x000444D0
	public void ClearExpectedUsers()
	{
		Hashtable hashtable = new Hashtable();
		hashtable[247] = new string[0];
		Hashtable hashtable2 = new Hashtable();
		hashtable2[247] = this.expectedUsers;
		PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(hashtable, hashtable2, false);
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x00046124 File Offset: 0x00044524
	public override string ToString()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", new object[]
		{
			this.nameField,
			(!this.visibleField) ? "hidden" : "visible",
			(!this.openField) ? "closed" : "open",
			this.maxPlayersField,
			this.playerCount
		});
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x000461A0 File Offset: 0x000445A0
	public new string ToStringFull()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", new object[]
		{
			this.nameField,
			(!this.visibleField) ? "hidden" : "visible",
			(!this.openField) ? "closed" : "open",
			this.maxPlayersField,
			this.playerCount,
			base.customProperties.ToStringFull()
		});
	}
}
