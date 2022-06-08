using System;

// Token: 0x020000EF RID: 239
public class ErrorCode
{
	// Token: 0x0400061B RID: 1563
	public const int Ok = 0;

	// Token: 0x0400061C RID: 1564
	public const int OperationNotAllowedInCurrentState = -3;

	// Token: 0x0400061D RID: 1565
	[Obsolete("Use InvalidOperation.")]
	public const int InvalidOperationCode = -2;

	// Token: 0x0400061E RID: 1566
	public const int InvalidOperation = -2;

	// Token: 0x0400061F RID: 1567
	public const int InternalServerError = -1;

	// Token: 0x04000620 RID: 1568
	public const int InvalidAuthentication = 32767;

	// Token: 0x04000621 RID: 1569
	public const int GameIdAlreadyExists = 32766;

	// Token: 0x04000622 RID: 1570
	public const int GameFull = 32765;

	// Token: 0x04000623 RID: 1571
	public const int GameClosed = 32764;

	// Token: 0x04000624 RID: 1572
	[Obsolete("No longer used, cause random matchmaking is no longer a process.")]
	public const int AlreadyMatched = 32763;

	// Token: 0x04000625 RID: 1573
	public const int ServerFull = 32762;

	// Token: 0x04000626 RID: 1574
	public const int UserBlocked = 32761;

	// Token: 0x04000627 RID: 1575
	public const int NoRandomMatchFound = 32760;

	// Token: 0x04000628 RID: 1576
	public const int GameDoesNotExist = 32758;

	// Token: 0x04000629 RID: 1577
	public const int MaxCcuReached = 32757;

	// Token: 0x0400062A RID: 1578
	public const int InvalidRegion = 32756;

	// Token: 0x0400062B RID: 1579
	public const int CustomAuthenticationFailed = 32755;

	// Token: 0x0400062C RID: 1580
	public const int AuthenticationTicketExpired = 32753;

	// Token: 0x0400062D RID: 1581
	public const int PluginReportedError = 32752;

	// Token: 0x0400062E RID: 1582
	public const int PluginMismatch = 32751;

	// Token: 0x0400062F RID: 1583
	public const int JoinFailedPeerAlreadyJoined = 32750;

	// Token: 0x04000630 RID: 1584
	public const int JoinFailedFoundInactiveJoiner = 32749;

	// Token: 0x04000631 RID: 1585
	public const int JoinFailedWithRejoinerNotFound = 32748;

	// Token: 0x04000632 RID: 1586
	public const int JoinFailedFoundExcludedUserId = 32747;

	// Token: 0x04000633 RID: 1587
	public const int JoinFailedFoundActiveJoiner = 32746;

	// Token: 0x04000634 RID: 1588
	public const int HttpLimitReached = 32745;

	// Token: 0x04000635 RID: 1589
	public const int ExternalHttpCallFailed = 32744;

	// Token: 0x04000636 RID: 1590
	public const int SlotError = 32742;

	// Token: 0x04000637 RID: 1591
	public const int InvalidEncryptionParameters = 32741;
}
