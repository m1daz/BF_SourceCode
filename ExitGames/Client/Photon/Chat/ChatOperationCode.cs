using System;

namespace ExitGames.Client.Photon.Chat
{
	// Token: 0x0200015E RID: 350
	public class ChatOperationCode
	{
		// Token: 0x040008F1 RID: 2289
		public const byte Authenticate = 230;

		// Token: 0x040008F2 RID: 2290
		public const byte Subscribe = 0;

		// Token: 0x040008F3 RID: 2291
		public const byte Unsubscribe = 1;

		// Token: 0x040008F4 RID: 2292
		public const byte Publish = 2;

		// Token: 0x040008F5 RID: 2293
		public const byte SendPrivate = 3;

		// Token: 0x040008F6 RID: 2294
		public const byte ChannelHistory = 4;

		// Token: 0x040008F7 RID: 2295
		public const byte UpdateStatus = 5;

		// Token: 0x040008F8 RID: 2296
		public const byte AddFriends = 6;

		// Token: 0x040008F9 RID: 2297
		public const byte RemoveFriends = 7;
	}
}
