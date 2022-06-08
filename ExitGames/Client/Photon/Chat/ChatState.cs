using System;

namespace ExitGames.Client.Photon.Chat
{
	// Token: 0x02000165 RID: 357
	public enum ChatState
	{
		// Token: 0x04000933 RID: 2355
		Uninitialized,
		// Token: 0x04000934 RID: 2356
		ConnectingToNameServer,
		// Token: 0x04000935 RID: 2357
		ConnectedToNameServer,
		// Token: 0x04000936 RID: 2358
		Authenticating,
		// Token: 0x04000937 RID: 2359
		Authenticated,
		// Token: 0x04000938 RID: 2360
		DisconnectingFromNameServer,
		// Token: 0x04000939 RID: 2361
		ConnectingToFrontEnd,
		// Token: 0x0400093A RID: 2362
		ConnectedToFrontEnd,
		// Token: 0x0400093B RID: 2363
		DisconnectingFromFrontEnd,
		// Token: 0x0400093C RID: 2364
		QueuedComingFromFrontEnd,
		// Token: 0x0400093D RID: 2365
		Disconnecting,
		// Token: 0x0400093E RID: 2366
		Disconnected
	}
}
