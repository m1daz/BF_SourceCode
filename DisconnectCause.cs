using System;

// Token: 0x02000104 RID: 260
public enum DisconnectCause
{
	// Token: 0x0400070E RID: 1806
	DisconnectByServerUserLimit = 1042,
	// Token: 0x0400070F RID: 1807
	ExceptionOnConnect = 1023,
	// Token: 0x04000710 RID: 1808
	DisconnectByServerTimeout = 1041,
	// Token: 0x04000711 RID: 1809
	DisconnectByServerLogic = 1043,
	// Token: 0x04000712 RID: 1810
	Exception = 1026,
	// Token: 0x04000713 RID: 1811
	InvalidAuthentication = 32767,
	// Token: 0x04000714 RID: 1812
	MaxCcuReached = 32757,
	// Token: 0x04000715 RID: 1813
	InvalidRegion = 32756,
	// Token: 0x04000716 RID: 1814
	SecurityExceptionOnConnect = 1022,
	// Token: 0x04000717 RID: 1815
	DisconnectByClientTimeout = 1040,
	// Token: 0x04000718 RID: 1816
	InternalReceiveException = 1039,
	// Token: 0x04000719 RID: 1817
	AuthenticationTicketExpired = 32753
}
