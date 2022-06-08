using System;
using System.Collections.Generic;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using UnityEngine;

// Token: 0x020004DC RID: 1244
public class UserTest : MonoBehaviour
{
	// Token: 0x060022B5 RID: 8885 RVA: 0x00102C11 File Offset: 0x00101011
	private void Start()
	{
		this.sp = new ServiceAPI(this.cons.apiKey, this.cons.secretKey);
	}

	// Token: 0x060022B6 RID: 8886 RVA: 0x00102C34 File Offset: 0x00101034
	private void OnGUI()
	{
		if (Time.time % 2f < 1f)
		{
			this.success = this.callBack.getResult();
		}
		GUI.TextArea(new Rect(10f, 5f, 1300f, 175f), this.success);
		if (GUI.Button(new Rect(50f, 200f, 200f, 30f), "Create User"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.CreateUser(this.cons.userName, this.password, this.cons.emailId, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 200f, 200f, 30f), "Get User"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.GetUser(this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 200f, 200f, 30f), "Get All Users"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.GetAllUsers(this.callBack);
		}
		if (GUI.Button(new Rect(680f, 200f, 200f, 30f), "Update Email"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.UpdateEmail(this.cons.userName, this.cons.emailId, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 200f, 200f, 30f), "Delete User"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.DeleteUser(this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 250f, 200f, 30f), "Authenticate User"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.Authenticate(this.cons.userName, this.password, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 250f, 200f, 30f), "Change UserPassword"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.ChangeUserPassword(this.cons.userName, this.password, "newPassWord", this.callBack);
		}
		if (GUI.Button(new Rect(470f, 250f, 200f, 30f), "Lock User"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.LockUser(this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 250f, 200f, 30f), "Unlock User"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.UnlockUser(this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 250f, 200f, 30f), "Get LockedUsers"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.GetLockedUsers(this.callBack);
		}
		if (GUI.Button(new Rect(50f, 300f, 200f, 30f), "GetAllUsersByPaging"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.GetAllUsers(this.max, this.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(260f, 300f, 200f, 30f), "GetAllUsersCount"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.GetAllUsersCount(this.callBack);
		}
		if (GUI.Button(new Rect(680f, 300f, 200f, 30f), "ResetUserPassword"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.ResetUserPassword(this.cons.userName, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 300f, 200f, 30f), "Log out"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.Logout(this.cons.sessionId, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 350f, 200f, 30f), "GetLockedUsersCount"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.GetLockedUsersCount(this.callBack);
		}
		if (GUI.Button(new Rect(260f, 350f, 200f, 30f), "GetUserByEmailId"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.GetUserByEmailId(this.cons.updateEmailId, this.callBack);
		}
		if (GUI.Button(new Rect(470f, 350f, 200f, 30f), "GetLockedUsersByPaging"))
		{
			this.userService = this.sp.BuildUserService();
			this.userService.GetLockedUsers(this.max, this.offSet, this.callBack);
		}
		if (GUI.Button(new Rect(680f, 350f, 200f, 30f), "CreateOrUpdateProfile"))
		{
			this.userService = this.sp.BuildUserService();
			User.Profile profile = new User.Profile(this.createUserObj);
			profile.SetCountry("India");
			profile.SetCity("GGN");
			profile.SetFirstName("Akshay");
			profile.SetLastName("Mishra");
			profile.SetHomeLandLine("1234567890");
			profile.SetMobile("12345678900987654321");
			profile.SetOfficeLandLine("0987654321");
			profile.SetSex(UserGender.MALE);
			profile.SetState("UP");
			this.userService.CreateOrUpdateProfile(this.createUserObj, this.callBack);
		}
		if (GUI.Button(new Rect(890f, 350f, 200f, 30f), "CreateUserWithRole"))
		{
			this.userService = this.sp.BuildUserService();
			IList<string> list = new List<string>();
			list.Add("Admin");
			list.Add("Manager");
			list.Add("Programmer");
			list.Add("Tester");
			this.userService.CreateUser(this.cons.userName, this.password, this.cons.emailId, list, this.callBack);
		}
		if (GUI.Button(new Rect(50f, 400f, 200f, 30f), "AssignRoles"))
		{
			this.userService = this.sp.BuildUserService();
			IList<string> list2 = new List<string>();
			list2.Add("Designer");
			list2.Add("Architect");
			this.userService.AssignRoles(this.cons.userName, list2, this.callBack);
		}
	}

	// Token: 0x0400237A RID: 9082
	private Constant cons = new Constant();

	// Token: 0x0400237B RID: 9083
	private ServiceAPI sp;

	// Token: 0x0400237C RID: 9084
	private UserService userService;

	// Token: 0x0400237D RID: 9085
	private User createUserObj;

	// Token: 0x0400237E RID: 9086
	public string password = "password";

	// Token: 0x0400237F RID: 9087
	public int max = 2;

	// Token: 0x04002380 RID: 9088
	public int offSet = 1;

	// Token: 0x04002381 RID: 9089
	public string success;

	// Token: 0x04002382 RID: 9090
	public string box;

	// Token: 0x04002383 RID: 9091
	private UserResponse callBack = new UserResponse();
}
