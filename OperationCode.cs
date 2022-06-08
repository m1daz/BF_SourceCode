using System;

// Token: 0x020000F4 RID: 244
public class OperationCode
{
	// Token: 0x04000693 RID: 1683
	[Obsolete("Exchanging encrpytion keys is done internally in the lib now. Don't expect this operation-result.")]
	public const byte ExchangeKeysForEncryption = 250;

	// Token: 0x04000694 RID: 1684
	public const byte Join = 255;

	// Token: 0x04000695 RID: 1685
	public const byte AuthenticateOnce = 231;

	// Token: 0x04000696 RID: 1686
	public const byte Authenticate = 230;

	// Token: 0x04000697 RID: 1687
	public const byte JoinLobby = 229;

	// Token: 0x04000698 RID: 1688
	public const byte LeaveLobby = 228;

	// Token: 0x04000699 RID: 1689
	public const byte CreateGame = 227;

	// Token: 0x0400069A RID: 1690
	public const byte JoinGame = 226;

	// Token: 0x0400069B RID: 1691
	public const byte JoinRandomGame = 225;

	// Token: 0x0400069C RID: 1692
	public const byte Leave = 254;

	// Token: 0x0400069D RID: 1693
	public const byte RaiseEvent = 253;

	// Token: 0x0400069E RID: 1694
	public const byte SetProperties = 252;

	// Token: 0x0400069F RID: 1695
	public const byte GetProperties = 251;

	// Token: 0x040006A0 RID: 1696
	public const byte ChangeGroups = 248;

	// Token: 0x040006A1 RID: 1697
	public const byte FindFriends = 222;

	// Token: 0x040006A2 RID: 1698
	public const byte GetLobbyStats = 221;

	// Token: 0x040006A3 RID: 1699
	public const byte GetRegions = 220;

	// Token: 0x040006A4 RID: 1700
	public const byte WebRpc = 219;

	// Token: 0x040006A5 RID: 1701
	public const byte ServerSettings = 218;
}
