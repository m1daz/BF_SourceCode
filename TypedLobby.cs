using System;

// Token: 0x020000FD RID: 253
public class TypedLobby
{
	// Token: 0x06000703 RID: 1795 RVA: 0x0003AA61 File Offset: 0x00038E61
	public TypedLobby()
	{
		this.Name = string.Empty;
		this.Type = LobbyType.Default;
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0003AA7B File Offset: 0x00038E7B
	public TypedLobby(string name, LobbyType type)
	{
		this.Name = name;
		this.Type = type;
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000705 RID: 1797 RVA: 0x0003AA91 File Offset: 0x00038E91
	public bool IsDefault
	{
		get
		{
			return this.Type == LobbyType.Default && string.IsNullOrEmpty(this.Name);
		}
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0003AAAC File Offset: 0x00038EAC
	public override string ToString()
	{
		return string.Format("lobby '{0}'[{1}]", this.Name, this.Type);
	}

	// Token: 0x040006DC RID: 1756
	public string Name;

	// Token: 0x040006DD RID: 1757
	public LobbyType Type;

	// Token: 0x040006DE RID: 1758
	public static readonly TypedLobby Default = new TypedLobby();
}
