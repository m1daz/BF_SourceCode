using System;

// Token: 0x0200047B RID: 1147
public enum GGCloudServiceErrorEventType
{
	// Token: 0x040021F6 RID: 8694
	NetworkNotConnect,
	// Token: 0x040021F7 RID: 8695
	LoginUserPasswordNotMatch,
	// Token: 0x040021F8 RID: 8696
	LoginNetworkNotConnect,
	// Token: 0x040021F9 RID: 8697
	LoginSuccess,
	// Token: 0x040021FA RID: 8698
	CreateUserNameExist = 10,
	// Token: 0x040021FB RID: 8699
	CreateUserNetworkNotConnect,
	// Token: 0x040021FC RID: 8700
	CreateUserSuccess,
	// Token: 0x040021FD RID: 8701
	AutoCreateUserSuccess,
	// Token: 0x040021FE RID: 8702
	AutoCreateUserFail,
	// Token: 0x040021FF RID: 8703
	CreateRoleNameExist = 20,
	// Token: 0x04002200 RID: 8704
	CreateRoleNetworkNotConnect,
	// Token: 0x04002201 RID: 8705
	AddFriendNotExist = 30,
	// Token: 0x04002202 RID: 8706
	CreateChatPrefab = 40,
	// Token: 0x04002203 RID: 8707
	LoadingPanelHide = 50,
	// Token: 0x04002204 RID: 8708
	LoadLogInScene = 60,
	// Token: 0x04002205 RID: 8709
	SlotTopPrizeFetch = 70,
	// Token: 0x04002206 RID: 8710
	NULL = 255
}
