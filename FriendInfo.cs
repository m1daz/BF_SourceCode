using System;

// Token: 0x020000E9 RID: 233
public class FriendInfo
{
	// Token: 0x17000094 RID: 148
	// (get) Token: 0x060006BE RID: 1726 RVA: 0x0003994D File Offset: 0x00037D4D
	// (set) Token: 0x060006BF RID: 1727 RVA: 0x00039955 File Offset: 0x00037D55
	public string Name { get; protected internal set; }

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0003995E File Offset: 0x00037D5E
	// (set) Token: 0x060006C1 RID: 1729 RVA: 0x00039966 File Offset: 0x00037D66
	public bool IsOnline { get; protected internal set; }

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0003996F File Offset: 0x00037D6F
	// (set) Token: 0x060006C3 RID: 1731 RVA: 0x00039977 File Offset: 0x00037D77
	public string Room { get; protected internal set; }

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00039980 File Offset: 0x00037D80
	public bool IsInRoom
	{
		get
		{
			return this.IsOnline && !string.IsNullOrEmpty(this.Room);
		}
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x000399A0 File Offset: 0x00037DA0
	public override string ToString()
	{
		return string.Format("{0}\t is: {1}", this.Name, this.IsOnline ? ((!this.IsInRoom) ? "on master" : "playing") : "offline");
	}
}
