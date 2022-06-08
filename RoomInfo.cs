using System;
using ExitGames.Client.Photon;

// Token: 0x02000120 RID: 288
public class RoomInfo
{
	// Token: 0x0600090B RID: 2315 RVA: 0x000459EF File Offset: 0x00043DEF
	protected internal RoomInfo(string roomName, Hashtable properties)
	{
		this.InternalCacheProperties(properties);
		this.nameField = roomName;
	}

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x0600090C RID: 2316 RVA: 0x00045A29 File Offset: 0x00043E29
	// (set) Token: 0x0600090D RID: 2317 RVA: 0x00045A31 File Offset: 0x00043E31
	public bool removedFromList { get; internal set; }

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x0600090E RID: 2318 RVA: 0x00045A3A File Offset: 0x00043E3A
	// (set) Token: 0x0600090F RID: 2319 RVA: 0x00045A42 File Offset: 0x00043E42
	protected internal bool serverSideMasterClient { get; private set; }

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06000910 RID: 2320 RVA: 0x00045A4B File Offset: 0x00043E4B
	public Hashtable customProperties
	{
		get
		{
			return this.customPropertiesField;
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06000911 RID: 2321 RVA: 0x00045A53 File Offset: 0x00043E53
	public string name
	{
		get
		{
			return this.nameField;
		}
	}

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x06000912 RID: 2322 RVA: 0x00045A5B File Offset: 0x00043E5B
	// (set) Token: 0x06000913 RID: 2323 RVA: 0x00045A63 File Offset: 0x00043E63
	public int playerCount { get; private set; }

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06000914 RID: 2324 RVA: 0x00045A6C File Offset: 0x00043E6C
	// (set) Token: 0x06000915 RID: 2325 RVA: 0x00045A74 File Offset: 0x00043E74
	public bool isLocalClientInside { get; set; }

	// Token: 0x1700011F RID: 287
	// (get) Token: 0x06000916 RID: 2326 RVA: 0x00045A7D File Offset: 0x00043E7D
	public byte maxPlayers
	{
		get
		{
			return this.maxPlayersField;
		}
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06000917 RID: 2327 RVA: 0x00045A85 File Offset: 0x00043E85
	public bool open
	{
		get
		{
			return this.openField;
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06000918 RID: 2328 RVA: 0x00045A8D File Offset: 0x00043E8D
	public bool visible
	{
		get
		{
			return this.visibleField;
		}
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00045A98 File Offset: 0x00043E98
	public override bool Equals(object other)
	{
		RoomInfo roomInfo = other as RoomInfo;
		return roomInfo != null && this.name.Equals(roomInfo.nameField);
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x00045AC6 File Offset: 0x00043EC6
	public override int GetHashCode()
	{
		return this.nameField.GetHashCode();
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00045AD4 File Offset: 0x00043ED4
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

	// Token: 0x0600091C RID: 2332 RVA: 0x00045B50 File Offset: 0x00043F50
	public string ToStringFull()
	{
		return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", new object[]
		{
			this.nameField,
			(!this.visibleField) ? "hidden" : "visible",
			(!this.openField) ? "closed" : "open",
			this.maxPlayersField,
			this.playerCount,
			this.customPropertiesField.ToStringFull()
		});
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00045BDC File Offset: 0x00043FDC
	protected internal void InternalCacheProperties(Hashtable propertiesToCache)
	{
		if (propertiesToCache == null || propertiesToCache.Count == 0 || this.customPropertiesField.Equals(propertiesToCache))
		{
			return;
		}
		if (propertiesToCache.ContainsKey(251))
		{
			this.removedFromList = (bool)propertiesToCache[251];
			if (this.removedFromList)
			{
				return;
			}
		}
		if (propertiesToCache.ContainsKey(255))
		{
			this.maxPlayersField = (byte)propertiesToCache[byte.MaxValue];
		}
		if (propertiesToCache.ContainsKey(253))
		{
			this.openField = (bool)propertiesToCache[253];
		}
		if (propertiesToCache.ContainsKey(254))
		{
			this.visibleField = (bool)propertiesToCache[254];
		}
		if (propertiesToCache.ContainsKey(252))
		{
			this.playerCount = (int)((byte)propertiesToCache[252]);
		}
		if (propertiesToCache.ContainsKey(249))
		{
			this.autoCleanUpField = (bool)propertiesToCache[249];
		}
		if (propertiesToCache.ContainsKey(248))
		{
			this.serverSideMasterClient = true;
			bool flag = this.masterClientIdField != 0;
			this.masterClientIdField = (int)propertiesToCache[248];
			if (flag)
			{
				PhotonNetwork.networkingPeer.UpdateMasterClient();
			}
		}
		if (propertiesToCache.ContainsKey(247))
		{
			this.expectedUsersField = (string[])propertiesToCache[247];
		}
		this.customPropertiesField.MergeStringKeys(propertiesToCache);
	}

	// Token: 0x040007EB RID: 2027
	private Hashtable customPropertiesField = new Hashtable();

	// Token: 0x040007EC RID: 2028
	protected byte maxPlayersField;

	// Token: 0x040007ED RID: 2029
	protected string[] expectedUsersField;

	// Token: 0x040007EE RID: 2030
	protected bool openField = true;

	// Token: 0x040007EF RID: 2031
	protected bool visibleField = true;

	// Token: 0x040007F0 RID: 2032
	protected bool autoCleanUpField = PhotonNetwork.autoCleanUpPlayerObjects;

	// Token: 0x040007F1 RID: 2033
	protected string nameField;

	// Token: 0x040007F2 RID: 2034
	protected internal int masterClientIdField;
}
