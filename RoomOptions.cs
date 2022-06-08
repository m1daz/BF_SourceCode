using System;
using ExitGames.Client.Photon;

// Token: 0x020000FA RID: 250
public class RoomOptions
{
	// Token: 0x17000099 RID: 153
	// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0003A971 File Offset: 0x00038D71
	// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0003A979 File Offset: 0x00038D79
	public bool IsVisible
	{
		get
		{
			return this.isVisibleField;
		}
		set
		{
			this.isVisibleField = value;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0003A982 File Offset: 0x00038D82
	// (set) Token: 0x060006EA RID: 1770 RVA: 0x0003A98A File Offset: 0x00038D8A
	public bool IsOpen
	{
		get
		{
			return this.isOpenField;
		}
		set
		{
			this.isOpenField = value;
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x060006EB RID: 1771 RVA: 0x0003A993 File Offset: 0x00038D93
	// (set) Token: 0x060006EC RID: 1772 RVA: 0x0003A99B File Offset: 0x00038D9B
	public bool CleanupCacheOnLeave
	{
		get
		{
			return this.cleanupCacheOnLeaveField;
		}
		set
		{
			this.cleanupCacheOnLeaveField = value;
		}
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x060006ED RID: 1773 RVA: 0x0003A9A4 File Offset: 0x00038DA4
	public bool SuppressRoomEvents
	{
		get
		{
			return this.suppressRoomEventsField;
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x060006EE RID: 1774 RVA: 0x0003A9AC File Offset: 0x00038DAC
	// (set) Token: 0x060006EF RID: 1775 RVA: 0x0003A9B4 File Offset: 0x00038DB4
	public bool PublishUserId
	{
		get
		{
			return this.publishUserIdField;
		}
		set
		{
			this.publishUserIdField = value;
		}
	}

	// Token: 0x1700009E RID: 158
	// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0003A9BD File Offset: 0x00038DBD
	// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0003A9C5 File Offset: 0x00038DC5
	[Obsolete("Use property with uppercase naming instead.")]
	public bool isVisible
	{
		get
		{
			return this.isVisibleField;
		}
		set
		{
			this.isVisibleField = value;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0003A9CE File Offset: 0x00038DCE
	// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0003A9D6 File Offset: 0x00038DD6
	[Obsolete("Use property with uppercase naming instead.")]
	public bool isOpen
	{
		get
		{
			return this.isOpenField;
		}
		set
		{
			this.isOpenField = value;
		}
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0003A9DF File Offset: 0x00038DDF
	// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0003A9E7 File Offset: 0x00038DE7
	[Obsolete("Use property with uppercase naming instead.")]
	public byte maxPlayers
	{
		get
		{
			return this.MaxPlayers;
		}
		set
		{
			this.MaxPlayers = value;
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0003A9F0 File Offset: 0x00038DF0
	// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0003A9F8 File Offset: 0x00038DF8
	[Obsolete("Use property with uppercase naming instead.")]
	public bool cleanupCacheOnLeave
	{
		get
		{
			return this.cleanupCacheOnLeaveField;
		}
		set
		{
			this.cleanupCacheOnLeaveField = value;
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0003AA01 File Offset: 0x00038E01
	// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0003AA09 File Offset: 0x00038E09
	[Obsolete("Use property with uppercase naming instead.")]
	public Hashtable customRoomProperties
	{
		get
		{
			return this.CustomRoomProperties;
		}
		set
		{
			this.CustomRoomProperties = value;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x060006FA RID: 1786 RVA: 0x0003AA12 File Offset: 0x00038E12
	// (set) Token: 0x060006FB RID: 1787 RVA: 0x0003AA1A File Offset: 0x00038E1A
	[Obsolete("Use property with uppercase naming instead.")]
	public string[] customRoomPropertiesForLobby
	{
		get
		{
			return this.CustomRoomPropertiesForLobby;
		}
		set
		{
			this.CustomRoomPropertiesForLobby = value;
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x060006FC RID: 1788 RVA: 0x0003AA23 File Offset: 0x00038E23
	// (set) Token: 0x060006FD RID: 1789 RVA: 0x0003AA2B File Offset: 0x00038E2B
	[Obsolete("Use property with uppercase naming instead.")]
	public string[] plugins
	{
		get
		{
			return this.Plugins;
		}
		set
		{
			this.Plugins = value;
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x060006FE RID: 1790 RVA: 0x0003AA34 File Offset: 0x00038E34
	[Obsolete("Use property with uppercase naming instead.")]
	public bool suppressRoomEvents
	{
		get
		{
			return this.suppressRoomEventsField;
		}
	}

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x060006FF RID: 1791 RVA: 0x0003AA3C File Offset: 0x00038E3C
	// (set) Token: 0x06000700 RID: 1792 RVA: 0x0003AA44 File Offset: 0x00038E44
	[Obsolete("Use property with uppercase naming instead.")]
	public bool publishUserId
	{
		get
		{
			return this.publishUserIdField;
		}
		set
		{
			this.publishUserIdField = value;
		}
	}

	// Token: 0x040006C5 RID: 1733
	private bool isVisibleField = true;

	// Token: 0x040006C6 RID: 1734
	private bool isOpenField = true;

	// Token: 0x040006C7 RID: 1735
	public byte MaxPlayers;

	// Token: 0x040006C8 RID: 1736
	public int PlayerTtl;

	// Token: 0x040006C9 RID: 1737
	public int EmptyRoomTtl;

	// Token: 0x040006CA RID: 1738
	private bool cleanupCacheOnLeaveField = PhotonNetwork.autoCleanUpPlayerObjects;

	// Token: 0x040006CB RID: 1739
	public Hashtable CustomRoomProperties;

	// Token: 0x040006CC RID: 1740
	public string[] CustomRoomPropertiesForLobby = new string[0];

	// Token: 0x040006CD RID: 1741
	public string[] Plugins;

	// Token: 0x040006CE RID: 1742
	private bool suppressRoomEventsField;

	// Token: 0x040006CF RID: 1743
	private bool publishUserIdField;
}
