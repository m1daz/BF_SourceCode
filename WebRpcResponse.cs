using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

// Token: 0x02000111 RID: 273
public class WebRpcResponse
{
	// Token: 0x060007F7 RID: 2039 RVA: 0x000413B8 File Offset: 0x0003F7B8
	public WebRpcResponse(OperationResponse response)
	{
		object obj;
		response.Parameters.TryGetValue(209, out obj);
		this.Name = (obj as string);
		response.Parameters.TryGetValue(207, out obj);
		this.ReturnCode = ((obj == null) ? -1 : ((int)((byte)obj)));
		response.Parameters.TryGetValue(208, out obj);
		this.Parameters = (obj as Dictionary<string, object>);
		response.Parameters.TryGetValue(206, out obj);
		this.DebugMessage = (obj as string);
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00041453 File Offset: 0x0003F853
	// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0004145B File Offset: 0x0003F85B
	public string Name { get; private set; }

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x060007FA RID: 2042 RVA: 0x00041464 File Offset: 0x0003F864
	// (set) Token: 0x060007FB RID: 2043 RVA: 0x0004146C File Offset: 0x0003F86C
	public int ReturnCode { get; private set; }

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x060007FC RID: 2044 RVA: 0x00041475 File Offset: 0x0003F875
	// (set) Token: 0x060007FD RID: 2045 RVA: 0x0004147D File Offset: 0x0003F87D
	public string DebugMessage { get; private set; }

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x060007FE RID: 2046 RVA: 0x00041486 File Offset: 0x0003F886
	// (set) Token: 0x060007FF RID: 2047 RVA: 0x0004148E File Offset: 0x0003F88E
	public Dictionary<string, object> Parameters { get; private set; }

	// Token: 0x06000800 RID: 2048 RVA: 0x00041497 File Offset: 0x0003F897
	public string ToStringFull()
	{
		return string.Format("{0}={2}: {1} \"{3}\"", new object[]
		{
			this.Name,
			SupportClass.DictionaryToString(this.Parameters),
			this.ReturnCode,
			this.DebugMessage
		});
	}
}
