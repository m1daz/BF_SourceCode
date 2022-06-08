using System;

namespace ExitGames.Client.Photon.Chat
{
	// Token: 0x02000167 RID: 359
	public interface IChatClientListener
	{
		// Token: 0x06000A68 RID: 2664
		void DebugReturn(DebugLevel level, string message);

		// Token: 0x06000A69 RID: 2665
		void OnDisconnected();

		// Token: 0x06000A6A RID: 2666
		void OnConnected();

		// Token: 0x06000A6B RID: 2667
		void OnChatStateChange(ChatState state);

		// Token: 0x06000A6C RID: 2668
		void OnGetMessages(string channelName, string[] senders, object[] messages);

		// Token: 0x06000A6D RID: 2669
		void OnPrivateMessage(string sender, object message, string channelName);

		// Token: 0x06000A6E RID: 2670
		void OnSubscribed(string[] channels, bool[] results);

		// Token: 0x06000A6F RID: 2671
		void OnUnsubscribed(string[] channels);

		// Token: 0x06000A70 RID: 2672
		void OnStatusUpdate(string user, int status, bool gotMessage, object message);
	}
}
