using System;

// Token: 0x020000FE RID: 254
public class TypedLobbyInfo : TypedLobby
{
	// Token: 0x06000709 RID: 1801 RVA: 0x0003AAE0 File Offset: 0x00038EE0
	public override string ToString()
	{
		return string.Format("TypedLobbyInfo '{0}'[{1}] rooms: {2} players: {3}", new object[]
		{
			this.Name,
			this.Type,
			this.RoomCount,
			this.PlayerCount
		});
	}

	// Token: 0x040006DF RID: 1759
	public int PlayerCount;

	// Token: 0x040006E0 RID: 1760
	public int RoomCount;
}
