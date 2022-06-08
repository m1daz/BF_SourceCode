using System;
using System.Collections.Generic;

// Token: 0x020004A8 RID: 1192
[Serializable]
public class CSFriendInfo
{
	// Token: 0x06002231 RID: 8753 RVA: 0x000FD290 File Offset: 0x000FB690
	public void ClearNewMessage()
	{
		this.IsHaveNewMessage = false;
	}

	// Token: 0x04002268 RID: 8808
	public string Name;

	// Token: 0x04002269 RID: 8809
	public string RoleName;

	// Token: 0x0400226A RID: 8810
	public bool IsOnline;

	// Token: 0x0400226B RID: 8811
	public string Room;

	// Token: 0x0400226C RID: 8812
	public bool IsInRoom;

	// Token: 0x0400226D RID: 8813
	public int NewMessageNum;

	// Token: 0x0400226E RID: 8814
	public bool IsInviteMe;

	// Token: 0x0400226F RID: 8815
	public List<CSMessage> messageList;

	// Token: 0x04002270 RID: 8816
	public bool IsHaveNewMessage;

	// Token: 0x04002271 RID: 8817
	public string mapName;

	// Token: 0x04002272 RID: 8818
	public string modeName;

	// Token: 0x04002273 RID: 8819
	public string playersCount;

	// Token: 0x04002274 RID: 8820
	public string maxplayersCount;

	// Token: 0x04002275 RID: 8821
	public GGServerRegion region;

	// Token: 0x04002276 RID: 8822
	public bool encrytion;

	// Token: 0x04002277 RID: 8823
	public string password;

	// Token: 0x04002278 RID: 8824
	public bool open;

	// Token: 0x04002279 RID: 8825
	public string rank;
}
