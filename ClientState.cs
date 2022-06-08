using System;

// Token: 0x02000102 RID: 258
public enum ClientState
{
	// Token: 0x040006F3 RID: 1779
	Uninitialized,
	// Token: 0x040006F4 RID: 1780
	PeerCreated,
	// Token: 0x040006F5 RID: 1781
	Queued,
	// Token: 0x040006F6 RID: 1782
	Authenticated,
	// Token: 0x040006F7 RID: 1783
	JoinedLobby,
	// Token: 0x040006F8 RID: 1784
	DisconnectingFromMasterserver,
	// Token: 0x040006F9 RID: 1785
	ConnectingToGameserver,
	// Token: 0x040006FA RID: 1786
	ConnectedToGameserver,
	// Token: 0x040006FB RID: 1787
	Joining,
	// Token: 0x040006FC RID: 1788
	Joined,
	// Token: 0x040006FD RID: 1789
	Leaving,
	// Token: 0x040006FE RID: 1790
	DisconnectingFromGameserver,
	// Token: 0x040006FF RID: 1791
	ConnectingToMasterserver,
	// Token: 0x04000700 RID: 1792
	QueuedComingFromGameserver,
	// Token: 0x04000701 RID: 1793
	Disconnecting,
	// Token: 0x04000702 RID: 1794
	Disconnected,
	// Token: 0x04000703 RID: 1795
	ConnectedToMaster,
	// Token: 0x04000704 RID: 1796
	ConnectingToNameServer,
	// Token: 0x04000705 RID: 1797
	ConnectedToNameServer,
	// Token: 0x04000706 RID: 1798
	DisconnectingFromNameServer,
	// Token: 0x04000707 RID: 1799
	Authenticating
}
