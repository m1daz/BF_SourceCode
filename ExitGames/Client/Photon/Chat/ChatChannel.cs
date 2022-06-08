using System;
using System.Collections.Generic;
using System.Text;

namespace ExitGames.Client.Photon.Chat
{
	// Token: 0x0200015A RID: 346
	public class ChatChannel
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0004A5C7 File Offset: 0x000489C7
		public ChatChannel(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0004A5EC File Offset: 0x000489EC
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0004A5F4 File Offset: 0x000489F4
		public bool IsPrivate { get; protected internal set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0004A5FD File Offset: 0x000489FD
		public int MessageCount
		{
			get
			{
				return this.Messages.Count;
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0004A60A File Offset: 0x00048A0A
		public void Add(string sender, object message)
		{
			this.Senders.Add(sender);
			this.Messages.Add(message);
			this.TruncateMessages();
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0004A62A File Offset: 0x00048A2A
		public void Add(string[] senders, object[] messages)
		{
			this.Senders.AddRange(senders);
			this.Messages.AddRange(messages);
			this.TruncateMessages();
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0004A64C File Offset: 0x00048A4C
		public void TruncateMessages()
		{
			if (this.MessageLimit <= 0 || this.Messages.Count <= this.MessageLimit)
			{
				return;
			}
			int count = this.Messages.Count - this.MessageLimit;
			this.Senders.RemoveRange(0, count);
			this.Messages.RemoveRange(0, count);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0004A6A9 File Offset: 0x00048AA9
		public void ClearMessages()
		{
			this.Senders.Clear();
			this.Messages.Clear();
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0004A6C4 File Offset: 0x00048AC4
		public string ToStringMessages()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.Messages.Count; i++)
			{
				stringBuilder.AppendLine(string.Format("{0}: {1}", this.Senders[i], this.Messages[i]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040008C6 RID: 2246
		public readonly string Name;

		// Token: 0x040008C7 RID: 2247
		public readonly List<string> Senders = new List<string>();

		// Token: 0x040008C8 RID: 2248
		public readonly List<object> Messages = new List<object>();

		// Token: 0x040008C9 RID: 2249
		public int MessageLimit;
	}
}
