using System;
using RioLog;
using UnityEngine;

// Token: 0x0200047E RID: 1150
public class GGCloudServiceInteraction : MonoBehaviour
{
	// Token: 0x06002146 RID: 8518 RVA: 0x000F800C File Offset: 0x000F640C
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06002147 RID: 8519 RVA: 0x000F8014 File Offset: 0x000F6414
	private void Init()
	{
		this.InitEvent();
	}

	// Token: 0x06002148 RID: 8520 RVA: 0x000F801C File Offset: 0x000F641C
	private void InitEvent()
	{
		GGCloudServiceAdapter.mInstance.CSEvent += this.HandleCSEvent;
		GGCloudServiceKit.mInstance.CSErrorEvent += this.HandleCSErrorEvent;
	}

	// Token: 0x06002149 RID: 8521 RVA: 0x000F804C File Offset: 0x000F644C
	private void HandleCSErrorEvent(GGCloudServiceErrorEventType eventType)
	{
		string text = string.Empty;
		switch (eventType)
		{
		case GGCloudServiceErrorEventType.CreateUserNameExist:
			this.mGGCSEventType = GGCloudServiceErrorEventType.CreateUserNameExist;
			break;
		case GGCloudServiceErrorEventType.CreateUserNetworkNotConnect:
			text = "Network not connected!";
			if (UILoginNewDirector.mInstance != null)
			{
				UILoginNewDirector.mInstance.PopErrorTipPanel(text);
			}
			break;
		case GGCloudServiceErrorEventType.CreateUserSuccess:
			this.mGGCSEventType = GGCloudServiceErrorEventType.CreateUserSuccess;
			break;
		case GGCloudServiceErrorEventType.AutoCreateUserSuccess:
			this.mGGCSEventType = GGCloudServiceErrorEventType.AutoCreateUserSuccess;
			break;
		case GGCloudServiceErrorEventType.AutoCreateUserFail:
			this.mGGCSEventType = GGCloudServiceErrorEventType.AutoCreateUserFail;
			break;
		default:
			switch (eventType)
			{
			case GGCloudServiceErrorEventType.LoginUserPasswordNotMatch:
				this.mGGCSEventType = GGCloudServiceErrorEventType.LoginUserPasswordNotMatch;
				break;
			case GGCloudServiceErrorEventType.LoginNetworkNotConnect:
				this.mGGCSEventType = GGCloudServiceErrorEventType.LoginNetworkNotConnect;
				break;
			case GGCloudServiceErrorEventType.LoginSuccess:
				this.mGGCSEventType = GGCloudServiceErrorEventType.LoginSuccess;
				break;
			default:
				if (eventType != GGCloudServiceErrorEventType.AddFriendNotExist)
				{
					if (eventType != GGCloudServiceErrorEventType.CreateChatPrefab)
					{
						if (eventType != GGCloudServiceErrorEventType.LoadingPanelHide)
						{
							if (eventType != GGCloudServiceErrorEventType.LoadLogInScene)
							{
								if (eventType == GGCloudServiceErrorEventType.SlotTopPrizeFetch)
								{
									this.mGGCSEventType = GGCloudServiceErrorEventType.SlotTopPrizeFetch;
								}
							}
							else
							{
								this.mGGCSEventType = GGCloudServiceErrorEventType.LoadLogInScene;
							}
						}
						else
						{
							this.mGGCSEventType = GGCloudServiceErrorEventType.LoadingPanelHide;
						}
					}
					else
					{
						this.mGGCSEventType = GGCloudServiceErrorEventType.CreateChatPrefab;
					}
				}
				else
				{
					text = "Name you input not exists, please input again!";
				}
				break;
			}
			break;
		case GGCloudServiceErrorEventType.CreateRoleNameExist:
			this.mGGCSEventType = GGCloudServiceErrorEventType.CreateRoleNameExist;
			break;
		case GGCloudServiceErrorEventType.CreateRoleNetworkNotConnect:
			text = "Network not connected!";
			if (UILoginNewDirector.mInstance != null)
			{
				UILoginNewDirector.mInstance.PopErrorTipPanel(text);
			}
			break;
		}
		RioQerdoDebug.Log(text);
	}

	// Token: 0x0600214A RID: 8522 RVA: 0x000F81D4 File Offset: 0x000F65D4
	private void HandleCSEvent(GGCloudServiceEventType csevent)
	{
		if (csevent == GGCloudServiceEventType.HaveNewMessage)
		{
			string defaultUserName = UIUserDataController.GetDefaultUserName();
			GGCloudServiceKit.mInstance.GetOfficialMessage(defaultUserName);
			GGCloudServiceKit.mInstance.GetFriendRequest(defaultUserName);
			GGCloudServiceKit.mInstance.mNewMessageList.Clear();
		}
	}

	// Token: 0x0600214B RID: 8523 RVA: 0x000F8224 File Offset: 0x000F6624
	private void Update()
	{
		GGCloudServiceErrorEventType ggcloudServiceErrorEventType = this.mGGCSEventType;
		switch (ggcloudServiceErrorEventType)
		{
		case GGCloudServiceErrorEventType.CreateUserNameExist:
		{
			string errorContent = "User name already exists, please modify and resubmit!";
			if (UILoginNewDirector.mInstance != null)
			{
				UILoginNewDirector.mInstance.PopErrorTipPanel(errorContent);
			}
			this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
			break;
		}
		default:
			switch (ggcloudServiceErrorEventType)
			{
			case GGCloudServiceErrorEventType.LoginUserPasswordNotMatch:
			{
				string errorContent2 = "Username or password error!";
				if (UILoginNewDirector.mInstance != null)
				{
					UILoginNewDirector.mInstance.PopErrorTipPanel(errorContent2);
				}
				this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
				GGCloudServiceKit.mInstance.StopCoroutineLoginTimeOut();
				break;
			}
			case GGCloudServiceErrorEventType.LoginNetworkNotConnect:
			{
				string errorContent3 = "Network not connected!";
				if (UILoginNewDirector.mInstance != null)
				{
					UILoginNewDirector.mInstance.PopErrorTipPanel(errorContent3);
				}
				this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
				GGCloudServiceKit.mInstance.StopCoroutineLoginTimeOut();
				break;
			}
			case GGCloudServiceErrorEventType.LoginSuccess:
				if (UILoginNewDirector.mInstance != null)
				{
					UILoginNewDirector.mInstance.LoginSucessEvent();
				}
				this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
				break;
			default:
				if (ggcloudServiceErrorEventType != GGCloudServiceErrorEventType.CreateRoleNameExist)
				{
					if (ggcloudServiceErrorEventType != GGCloudServiceErrorEventType.CreateChatPrefab)
					{
						if (ggcloudServiceErrorEventType != GGCloudServiceErrorEventType.LoadingPanelHide)
						{
							if (ggcloudServiceErrorEventType != GGCloudServiceErrorEventType.LoadLogInScene)
							{
								if (ggcloudServiceErrorEventType != GGCloudServiceErrorEventType.SlotTopPrizeFetch)
								{
									if (ggcloudServiceErrorEventType != GGCloudServiceErrorEventType.NULL)
									{
									}
								}
								else
								{
									if (UICasinoDirector.mInstance != null)
									{
										UICasinoDirector.mInstance.ShowLuckyList(GGCloudServiceKit.mInstance.mTopPrizeList);
									}
									this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
								}
							}
							else
							{
								if (UISceneManager.mInstance != null)
								{
									UISceneManager.mInstance.LoadLevel("UILogin");
								}
								this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
							}
						}
						else
						{
							if (UILoginNewDirector.mInstance != null)
							{
								UITipController.mInstance.HideCurTip();
							}
							this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
						}
					}
					else
					{
						if (GameObject.FindGameObjectWithTag("GGNetworkChatSys") == null)
						{
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.goChatPrefab, new Vector3(-1000f, -1000f, -1000f), Quaternion.identity);
						}
						this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
					}
				}
				else
				{
					string errorContent4 = "Role name exists!";
					if (UILoginNewDirector.mInstance != null)
					{
						UILoginNewDirector.mInstance.PopErrorTipPanel(errorContent4);
					}
					this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
				}
				break;
			}
			break;
		case GGCloudServiceErrorEventType.CreateUserSuccess:
			UITipController.mInstance.HideCurTip();
			if (UILoginNewDirector.mInstance != null)
			{
				UILoginNewDirector.mInstance.manualRegisterNode.SetActive(false);
			}
			GGCloudServiceCreate.mInstance.AutoQuickEnterGame(GGCloudServiceKit.mInstance.mUserNamePassword.UserName, GGCloudServiceKit.mInstance.mUserNamePassword.Password);
			this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
			break;
		case GGCloudServiceErrorEventType.AutoCreateUserSuccess:
			if (UILoginNewDirector.mInstance != null)
			{
				UILoginNewDirector.mInstance.PopAutoRegisterSuccess(GGCloudServiceKit.mInstance.mUserNamePassword);
			}
			this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
			break;
		case GGCloudServiceErrorEventType.AutoCreateUserFail:
			if (UILoginNewDirector.mInstance != null)
			{
				UILoginNewDirector.mInstance.PopErrorTipPanel("Auto Register Failed.");
			}
			this.mGGCSEventType = GGCloudServiceErrorEventType.NULL;
			break;
		}
	}

	// Token: 0x0600214C RID: 8524 RVA: 0x000F854C File Offset: 0x000F694C
	private void OnDisable()
	{
		GGCloudServiceAdapter.mInstance.CSEvent -= this.HandleCSEvent;
		GGCloudServiceKit.mInstance.CSErrorEvent -= this.HandleCSErrorEvent;
	}

	// Token: 0x0400220B RID: 8715
	public GameObject goChatPrefab;

	// Token: 0x0400220C RID: 8716
	private GGCloudServiceErrorEventType mGGCSEventType = GGCloudServiceErrorEventType.NULL;

	// Token: 0x0400220D RID: 8717
	private GGCloudServiceEventType mGGCSAdapterEventType = GGCloudServiceEventType.NULL;
}
