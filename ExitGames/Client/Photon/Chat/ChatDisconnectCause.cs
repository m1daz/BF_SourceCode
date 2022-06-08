using System;

namespace ExitGames.Client.Photon.Chat
{
	// Token: 0x0200015C RID: 348
	public enum ChatDisconnectCause
	{
		// Token: 0x040008DF RID: 2271
		None,
		// Token: 0x040008E0 RID: 2272
		DisconnectByServerUserLimit,
		// Token: 0x040008E1 RID: 2273
		ExceptionOnConnect,
		// Token: 0x040008E2 RID: 2274
		DisconnectByServer,
		// Token: 0x040008E3 RID: 2275
		TimeoutDisconnect,
		// Token: 0x040008E4 RID: 2276
		Exception,
		// Token: 0x040008E5 RID: 2277
		InvalidAuthentication,
		// Token: 0x040008E6 RID: 2278
		MaxCcuReached,
		// Token: 0x040008E7 RID: 2279
		InvalidRegion,
		// Token: 0x040008E8 RID: 2280
		OperationNotAllowedInCurrentState,
		// Token: 0x040008E9 RID: 2281
		CustomAuthenticationFailed
	}
}
