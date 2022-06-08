using System;

namespace ExitGames.Client.Photon.Chat
{
	// Token: 0x02000164 RID: 356
	public class ErrorCode
	{
		// Token: 0x04000923 RID: 2339
		public const int Ok = 0;

		// Token: 0x04000924 RID: 2340
		public const int OperationNotAllowedInCurrentState = -3;

		// Token: 0x04000925 RID: 2341
		public const int InvalidOperationCode = -2;

		// Token: 0x04000926 RID: 2342
		public const int InternalServerError = -1;

		// Token: 0x04000927 RID: 2343
		public const int InvalidAuthentication = 32767;

		// Token: 0x04000928 RID: 2344
		public const int GameIdAlreadyExists = 32766;

		// Token: 0x04000929 RID: 2345
		public const int GameFull = 32765;

		// Token: 0x0400092A RID: 2346
		public const int GameClosed = 32764;

		// Token: 0x0400092B RID: 2347
		public const int ServerFull = 32762;

		// Token: 0x0400092C RID: 2348
		public const int UserBlocked = 32761;

		// Token: 0x0400092D RID: 2349
		public const int NoRandomMatchFound = 32760;

		// Token: 0x0400092E RID: 2350
		public const int GameDoesNotExist = 32758;

		// Token: 0x0400092F RID: 2351
		public const int MaxCcuReached = 32757;

		// Token: 0x04000930 RID: 2352
		public const int InvalidRegion = 32756;

		// Token: 0x04000931 RID: 2353
		public const int CustomAuthenticationFailed = 32755;
	}
}
