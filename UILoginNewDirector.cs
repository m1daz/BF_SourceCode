using System;
using System.Collections;
using System.Collections.Generic;
using GOG.Utility;
using LitJson;
using UnityEngine;

// Token: 0x020002D1 RID: 721
public class UILoginNewDirector : MonoBehaviour
{
	// Token: 0x0600155F RID: 5471 RVA: 0x000B782A File Offset: 0x000B5C2A
	private void Awake()
	{
		if (UILoginNewDirector.mInstance == null)
		{
			UILoginNewDirector.mInstance = this;
		}
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x000B7842 File Offset: 0x000B5C42
	private void OnDestroy()
	{
		if (UILoginNewDirector.mInstance != null)
		{
			UILoginNewDirector.mInstance = null;
		}
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x000B785C File Offset: 0x000B5C5C
	private void Start()
	{
		this.NeedPopGDPRCanvas();
		base.Invoke("StartToHandle", 10f);
		this.InitLoginPage();
		this.backupTipLabel.gameObject.SetActive(false);
		this.backupBtn.gameObject.SetActive(false);
		if (UIUserDataController.GetFirstPlay() == 0)
		{
			this.QuickPlayBtn.SetActive(false);
		}
		else
		{
			this.QuickPlayBtn.SetActive(true);
		}
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x000B78CE File Offset: 0x000B5CCE
	private void Update()
	{
		this.ProcessLoginProgress();
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x000B78D8 File Offset: 0x000B5CD8
	public void InitLoginPage()
	{
		this.usernameCancelBtn.gameObject.SetActive(false);
		this.passwordCancelBtn.gameObject.SetActive(false);
		string curLoginUserName = UIUserDataController.GetCurLoginUserName();
		string curLoginPassword = UIUserDataController.GetCurLoginPassword();
		if (curLoginUserName != string.Empty)
		{
			this.usernameInput.value = curLoginUserName;
		}
		if (curLoginPassword != string.Empty)
		{
			if (UIUserDataController.GetRememberPwd() == 1)
			{
				this.passwordInput.value = curLoginPassword;
				this.rememberPwdToggle.value = true;
			}
			else
			{
				this.rememberPwdToggle.value = false;
			}
		}
		int usedAccountCount = UIUserDataController.GetUsedAccountCount();
		if (usedAccountCount > 0)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < usedAccountCount; i++)
			{
				list.Add(UIUserDataController.GetUsedAccount(i));
			}
			this.usernamePoplist.items = list;
		}
		else
		{
			this.usernamePoplist.gameObject.SetActive(false);
		}
		for (int j = 0; j < 2; j++)
		{
			this.tex[j] = (Resources.Load("UI/Images/General/LoadingTexture" + (j + 1).ToString()) as Texture);
		}
		this.versionTip.text = "V " + GameVersionController.GameVersion;
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x000B7A2B File Offset: 0x000B5E2B
	public void FirstLoadedLoginBtnPressed()
	{
		this.FirtLoadedNode.SetActive(false);
		this.loginNode.SetActive(true);
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x000B7A45 File Offset: 0x000B5E45
	public void RegisterBtnPressed()
	{
		this.loginNode.SetActive(false);
		this.FirtLoadedNode.SetActive(false);
		this.manualRegisterNode.SetActive(true);
		if (UIUserDataController.GetFirstPlay() == 0)
		{
			UnityAnalyticsIntegration.mInstance.NewUserPressQuickPlayOrRegister();
		}
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x000B7A80 File Offset: 0x000B5E80
	public void LoginBtnPressed()
	{
		if (this.usernameInput.value != string.Empty && this.passwordInput.value != string.Empty)
		{
			if (WordFilterInLogin.CheckString(this.usernameInput.value.ToLower()))
			{
				this.networkRequestType = UILoginNewDirector.UINetworkRequestType.Login;
				this.UINetworkRequest(this.networkRequestType);
			}
			else
			{
				this.UsernameOrPwdError();
			}
		}
		else
		{
			this.UsernameOrPwdError();
		}
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x000B7B04 File Offset: 0x000B5F04
	public void QuickEnterGameBtnPressed()
	{
		if (UIUserDataController.GetCurLoginUserName() != string.Empty && UIUserDataController.GetCurLoginPassword() != string.Empty)
		{
			GGCloudServiceCreate.mInstance.InitCloudServiceCreate();
		}
		else
		{
			GGCloudServiceKit.mInstance.QuickEnterGame();
			if (UIUserDataController.GetFirstPlay() == 0)
			{
				UnityAnalyticsIntegration.mInstance.NewUserPressQuickPlayOrRegister();
			}
		}
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x000B7B66 File Offset: 0x000B5F66
	public void UsernameCancelBtnPressed()
	{
		this.usernameInput.value = string.Empty;
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x000B7B78 File Offset: 0x000B5F78
	public void PwdCancelBtnPressed()
	{
		this.passwordInput.value = string.Empty;
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x000B7B8A File Offset: 0x000B5F8A
	public void UsernameTextboxClick()
	{
		if (this.passwordInput.value != string.Empty)
		{
		}
	}

	// Token: 0x0600156B RID: 5483 RVA: 0x000B7BAB File Offset: 0x000B5FAB
	public void PwdTextboxClick()
	{
		if (this.usernameInput.value != string.Empty)
		{
		}
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x000B7BCC File Offset: 0x000B5FCC
	public void UsernameTextboxChanged()
	{
		if (this.usernameInput.value != string.Empty)
		{
		}
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x000B7BED File Offset: 0x000B5FED
	public void PwdTextboxChanged()
	{
		if (this.passwordInput.value != string.Empty)
		{
		}
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x000B7C0E File Offset: 0x000B600E
	public void UsernameOrPwdError()
	{
		UITipController.mInstance.HideCurTip();
		this.PopErrorTipPanel("Username or password error.");
	}

	// Token: 0x0600156F RID: 5487 RVA: 0x000B7C25 File Offset: 0x000B6025
	public void RememberCheckboxChanged()
	{
		if (this.isFirstChangedForRememberToggle)
		{
			this.isFirstChangedForRememberToggle = false;
		}
		else if (this.rememberPwdToggle.value)
		{
			UIUserDataController.SetRememberPwd(1);
		}
		else
		{
			UIUserDataController.SetRememberPwd(0);
		}
	}

	// Token: 0x06001570 RID: 5488 RVA: 0x000B7C60 File Offset: 0x000B6060
	public void PopErrorTipPanel(string errorContent)
	{
		GGCloudServiceKit.mInstance.StopCoroutineLoginTimeOut();
		GOGPlayerPrefabs.SetString("sessionid", string.Empty);
		if (this.loginNode.activeSelf)
		{
			UITipController.mInstance.HideCurTip();
		}
		if (this.loginProgressBarNode.activeSelf)
		{
			this.loginProgressBarPanelIsVisible = false;
			this.loginProgressBarNode.SetActive(false);
		}
		EventDelegate btnEventName = new EventDelegate(this, "ErrorOkBtnPressed");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, errorContent, Color.white, "OK", string.Empty, btnEventName, null, null);
	}

	// Token: 0x06001571 RID: 5489 RVA: 0x000B7CED File Offset: 0x000B60ED
	public void AutoEnterGameProcessBar()
	{
		this.loginProgressBarPanelIsVisible = true;
		this.ShowLoginProgressBarPanel();
	}

	// Token: 0x06001572 RID: 5490 RVA: 0x000B7CFC File Offset: 0x000B60FC
	public void ErrorOkBtnPressed()
	{
		if (this.loginProgressBarNode.activeSelf)
		{
			this.loginProgressBarPanelIsVisible = false;
			this.loginProgressBarNode.SetActive(false);
		}
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x06001573 RID: 5491 RVA: 0x000B7D2B File Offset: 0x000B612B
	public void EnterGameDontNeedLogin(string SessionID)
	{
		GGCloudServiceKit.mInstance.LogIn(UIUserDataController.GetCurLoginUserName(), UIUserDataController.GetCurLoginPassword());
	}

	// Token: 0x06001574 RID: 5492 RVA: 0x000B7D44 File Offset: 0x000B6144
	public void LoginSucessEvent()
	{
		if (GGCloudServiceKit.mInstance.mUserNamePassword.UserName != string.Empty)
		{
			this.usernameInput.value = GGCloudServiceKit.mInstance.mUserNamePassword.UserName;
			this.passwordInput.value = GGCloudServiceKit.mInstance.mUserNamePassword.Password;
		}
		int usedAccountCount = UIUserDataController.GetUsedAccountCount();
		bool flag = false;
		if (usedAccountCount > 0)
		{
			for (int i = 0; i < usedAccountCount; i++)
			{
				if (UIUserDataController.GetUsedAccount(i) == this.usernameInput.value.ToLower())
				{
					flag = true;
					break;
				}
			}
			if (!flag && this.usernameInput.value != string.Empty)
			{
				UIUserDataController.SetUsedAccount(usedAccountCount, this.usernameInput.value.ToLower());
				UIUserDataController.SetUsedAccountPwd(usedAccountCount, this.passwordInput.value);
				UIUserDataController.SetUsedAccountCount(usedAccountCount + 1);
			}
		}
		else if (this.usernameInput.value != string.Empty)
		{
			UIUserDataController.SetUsedAccount(usedAccountCount, this.usernameInput.value.ToLower());
			UIUserDataController.SetUsedAccountPwd(usedAccountCount, this.passwordInput.value);
			UIUserDataController.SetUsedAccountCount(usedAccountCount + 1);
		}
		GGCloudServiceKit.mInstance.PreGetFriendSysHaveNewMessage(this.usernameInput.value.ToLower());
		if (GameObject.FindGameObjectWithTag("ACTUserDataManager") == null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.goACTUserDataPrefab, new Vector3(-1000f, -1000f, -1000f), Quaternion.identity);
			gameObject.name = "ACTUserDataManager";
			ACTUserDataManager.curLoginUserName = this.usernameInput.value.ToLower();
			ACTUserDataManager.curPassword = this.passwordInput.value;
			if ((ACTUserDataManager.curLoginUserName == string.Empty || ACTUserDataManager.curPassword == string.Empty) && GGCloudServiceKit.mInstance.mUserNamePassword.UserName != string.Empty)
			{
				ACTUserDataManager.curLoginUserName = GGCloudServiceKit.mInstance.mUserNamePassword.UserName;
				ACTUserDataManager.curPassword = GGCloudServiceKit.mInstance.mUserNamePassword.Password;
			}
			UIUserDataController.SetCurLoginUserName(ACTUserDataManager.curLoginUserName);
			UIUserDataController.SetCurLoginPassword(ACTUserDataManager.curPassword);
			ACTUserDataManager.preLoginUserName = UIUserDataController.GetPreUserName();
			ACTUserDataManager.mInstance.DownloadData();
		}
		else
		{
			ACTUserDataManager.curLoginUserName = this.usernameInput.value.ToLower();
			ACTUserDataManager.curPassword = this.passwordInput.value;
			if ((ACTUserDataManager.curLoginUserName == string.Empty || ACTUserDataManager.curPassword == string.Empty) && GGCloudServiceKit.mInstance.mUserNamePassword.UserName != string.Empty)
			{
				ACTUserDataManager.curLoginUserName = GGCloudServiceKit.mInstance.mUserNamePassword.UserName;
				ACTUserDataManager.curPassword = GGCloudServiceKit.mInstance.mUserNamePassword.Password;
			}
			UIUserDataController.SetCurLoginUserName(ACTUserDataManager.curLoginUserName);
			UIUserDataController.SetCurLoginPassword(ACTUserDataManager.curPassword);
			ACTUserDataManager.preLoginUserName = UIUserDataController.GetPreUserName();
			ACTUserDataManager.mInstance.DownloadData();
		}
	}

	// Token: 0x06001575 RID: 5493 RVA: 0x000B8064 File Offset: 0x000B6464
	public void LoginSucessEventSequence2()
	{
		GGCloudServiceConstant.mInstance.mUserName = this.usernameInput.value.ToLower();
		UIUserDataController.SetDefaultUserName(this.usernameInput.value.ToLower());
		UIUserDataController.SetDefaultPwd(this.passwordInput.value);
		UIUserDataController.SetPreUserName(this.usernameInput.value.ToLower());
		GGCloudServiceKit.mInstance.BindUserNameWithDeviceToken(UIUserDataController.GetDefaultUserName());
		GGCloudServiceKit.mInstance.StopCoroutineLoginTimeOut();
	}

	// Token: 0x06001576 RID: 5494 RVA: 0x000B80E0 File Offset: 0x000B64E0
	public void UsernamePoplistSelect()
	{
		if (UIPopupList.isOpen)
		{
			this.usernameInput.value = this.usernamePoplist.value;
			if (UIUserDataController.GetRememberPwd() == 1)
			{
				this.passwordInput.value = UIUserDataController.GetUsedAccountPwd(this.usernamePoplist.items.IndexOf(this.usernamePoplist.value));
			}
			else
			{
				this.passwordInput.value = string.Empty;
			}
		}
	}

	// Token: 0x06001577 RID: 5495 RVA: 0x000B8158 File Offset: 0x000B6558
	public void DeleteUsedAccountRecordBtnPressed()
	{
		EventDelegate btnEventName = new EventDelegate(this, "DeleteAccountYesBtnPressed");
		EventDelegate btnEventName2 = new EventDelegate(this, "DeleteAccountNoBtnPressed");
		UITipController.mInstance.SetTipData(UITipController.TipType.TwoButtonTip, "Do you want to delete account list?", Color.white, "YES", "NO", btnEventName, btnEventName2, null);
	}

	// Token: 0x06001578 RID: 5496 RVA: 0x000B81A0 File Offset: 0x000B65A0
	public void DeleteAccountYesBtnPressed()
	{
		int usedAccountCount = UIUserDataController.GetUsedAccountCount();
		if (usedAccountCount > 0)
		{
			for (int i = 0; i < usedAccountCount; i++)
			{
				UIUserDataController.SetUsedAccount(i, string.Empty);
				UIUserDataController.SetUsedAccountPwd(i, string.Empty);
			}
			UIUserDataController.SetUsedAccountCount(0);
			this.usernamePoplist.gameObject.SetActive(false);
		}
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x06001579 RID: 5497 RVA: 0x000B8203 File Offset: 0x000B6603
	public void DeleteAccountNoBtnPressed()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x0600157A RID: 5498 RVA: 0x000B8210 File Offset: 0x000B6610
	public void EmailBtnPressed()
	{
		string url = "mailto:riovoxfeedback@gmail.com?subject=FromBlockForceAndroid&body=";
		Application.OpenURL(url);
	}

	// Token: 0x0600157B RID: 5499 RVA: 0x000B8229 File Offset: 0x000B6629
	public void CreatRoleCancelBtnPressed()
	{
		this.creatRoleInput.value = string.Empty;
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x000B823B File Offset: 0x000B663B
	public void CreatRoleTextboxClick()
	{
		this.creatRoleInput.value = string.Empty;
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x000B8250 File Offset: 0x000B6650
	public void CreatRoleTextboxChanged()
	{
		if (this.creatRoleInput.value != string.Empty)
		{
			this.creatRoleCancelBtn.gameObject.SetActive(true);
			this.creatRoleInput.value = WordFilterInLogin.mInstance.FilterString(this.creatRoleInput.value);
			this.creatRoleInput.value = this.RemoveInvalidCharacter(this.creatRoleInput.value);
		}
		else
		{
			this.creatRoleCancelBtn.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x000B82DC File Offset: 0x000B66DC
	public string RemoveInvalidCharacter(string str)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < str.Length; i++)
		{
			if (str[i] < 'a' || str[i] > 'z')
			{
				if (str[i] < 'A' || str[i] > 'Z')
				{
					if (str[i] < '0' || str[i] > '9')
					{
						str = str.Replace(str[i], '\0');
						return str;
					}
				}
			}
		}
		return str;
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x000B837C File Offset: 0x000B677C
	public void CreatRoleBtnPressed()
	{
		if (UIUserDataController.GetFirstPlay() == 0)
		{
			UnityAnalyticsIntegration.mInstance.NewUserPressCreateRoleName();
		}
		UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Create role name ...", Color.white, null, null, null, null, null);
		if (this.creatRoleInput.value != string.Empty)
		{
			if (WordFilterInLogin.CheckString(this.creatRoleInput.value))
			{
				this.networkRequestType = UILoginNewDirector.UINetworkRequestType.CreatRole;
				this.UINetworkRequest(this.networkRequestType);
			}
			else
			{
				this.PopErrorTipPanel("Role Name Contains Illegal Characters!");
			}
		}
		else
		{
			this.PopErrorTipPanel("Role name can't be empty!");
		}
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x000B841C File Offset: 0x000B681C
	private void UINetworkRequest(UILoginNewDirector.UINetworkRequestType type)
	{
		switch (type)
		{
		case UILoginNewDirector.UINetworkRequestType.Login:
			GGCloudServiceKit.mInstance.LogIn(this.usernameInput.value.ToLower(), this.passwordInput.value);
			this.ShowLoginProgressBarPanel();
			this.loginProgressBarPanelIsVisible = true;
			break;
		case UILoginNewDirector.UINetworkRequestType.AutoRegister:
			GGCloudServiceKit.mInstance.AutoCreateUser();
			break;
		case UILoginNewDirector.UINetworkRequestType.ManualRegister:
			GGCloudServiceKit.mInstance.CreateUser(this.mrUsernameInput.value.ToLower(), this.mrPwdInput.value);
			break;
		case UILoginNewDirector.UINetworkRequestType.CreatRole:
			GGCloudServiceKit.mInstance.CreateRoleName(UIUserDataController.GetDefaultUserName(), this.creatRoleInput.value);
			break;
		}
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x000B84DA File Offset: 0x000B68DA
	public void CreateRoleNameSuccess()
	{
		this.creatRoleNode.SetActive(false);
		this.creatRoleSuccessLabel.text = this.creatRoleInput.value;
		UIUserDataController.SetDefaultRoleName(this.creatRoleInput.value);
		this.StartToPlay();
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x000B8514 File Offset: 0x000B6914
	public void PopManualRegisterSuccess()
	{
		this.mrUsernameInputValue = this.mrUsernameInput.value.ToLower();
		this.mrPasswordInputValue = this.mrPwdInput.value;
		this.mrSuccessTipLabel.text = this.mrUsernameInput.value.ToLower();
		this.mrSuccessTipNode.SetActive(true);
		this.manualRegisterNode.SetActive(false);
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x000B8588 File Offset: 0x000B6988
	public void AutoRegisterBtnPressed()
	{
		UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Register in process...", Color.white, null, null, null, null, null);
		this.networkRequestType = UILoginNewDirector.UINetworkRequestType.AutoRegister;
		this.UINetworkRequest(this.networkRequestType);
	}

	// Token: 0x06001584 RID: 5508 RVA: 0x000B85C2 File Offset: 0x000B69C2
	public void ManualRegisterBtnPressed()
	{
		this.registerSelectNode.SetActive(false);
		this.manualRegisterNode.SetActive(true);
	}

	// Token: 0x06001585 RID: 5509 RVA: 0x000B85DC File Offset: 0x000B69DC
	public void RegisterSelectCancelBtnPressed()
	{
		this.registerSelectNode.SetActive(false);
		this.FirtLoadedNode.SetActive(true);
	}

	// Token: 0x06001586 RID: 5510 RVA: 0x000B85F8 File Offset: 0x000B69F8
	public void AutoRegisterOkBtnPressed()
	{
		this.loginNode.SetActive(true);
		this.autoRegisterTipNode.SetActive(false);
		this.usernameInput.value = this.autoRegisterUsernameLabel.text;
		this.passwordInput.value = this.autoRegisterPasswordLabel.text;
	}

	// Token: 0x06001587 RID: 5511 RVA: 0x000B864C File Offset: 0x000B6A4C
	public void PopAutoRegisterSuccess(CSUserNamePassword value)
	{
		UITipController.mInstance.HideCurTip();
		this.autoRegisterTipNode.SetActive(true);
		this.registerSelectNode.SetActive(false);
		this.autoRegisterUsernameLabel.text = value.UserName;
		this.autoRegisterPasswordLabel.text = value.Password;
	}

	// Token: 0x06001588 RID: 5512 RVA: 0x000B869D File Offset: 0x000B6A9D
	public void StartToPlay()
	{
		if (UIUserDataController.GetFirstPlay() == 0)
		{
			UISceneManager.mInstance.LoadLevel("UIHelp");
		}
		else
		{
			UISceneManager.mInstance.LoadLevel("MainMenu");
		}
	}

	// Token: 0x06001589 RID: 5513 RVA: 0x000B86CC File Offset: 0x000B6ACC
	public void MRCancelBtnPressed()
	{
		this.mrUsernameInput.value = string.Empty;
		this.mrPwdInput.value = string.Empty;
		this.mrVerifyPwdInput.value = string.Empty;
		this.mrUsernameCancelBtn.gameObject.SetActive(false);
		this.mrPwdCancelBtn.gameObject.SetActive(false);
		this.mrVerifyPwdCancelBtn.gameObject.SetActive(false);
		this.manualRegisterNode.SetActive(false);
		this.FirtLoadedNode.SetActive(true);
		string defaultUserName = UIUserDataController.GetDefaultUserName();
		string defaultPwd = UIUserDataController.GetDefaultPwd();
		if (defaultUserName != string.Empty)
		{
			this.usernameInput.value = defaultUserName;
		}
		if (defaultPwd != string.Empty && UIUserDataController.GetRememberPwd() == 1)
		{
			this.passwordInput.value = defaultPwd;
			this.rememberPwdToggle.value = true;
		}
	}

	// Token: 0x0600158A RID: 5514 RVA: 0x000B87B0 File Offset: 0x000B6BB0
	public void MRSubmitBtnPressed()
	{
		if (this.mrUsernameInput.value != string.Empty && this.mrPwdInput.value != string.Empty && this.mrVerifyPwdInput.value != string.Empty)
		{
			if (WordFilterInLogin.CheckString(this.mrUsernameInput.value.ToLower()))
			{
				if (this.mrPwdInput.value != this.mrVerifyPwdInput.value)
				{
					this.PopErrorTipPanel("Passwords do not match!");
				}
				else
				{
					UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Register in process...", Color.white, null, null, null, null, null);
					this.networkRequestType = UILoginNewDirector.UINetworkRequestType.ManualRegister;
					this.UINetworkRequest(this.networkRequestType);
				}
			}
			else
			{
				this.PopErrorTipPanel("Username Contains Illegal Characters!");
			}
		}
		else
		{
			this.PopErrorTipPanel("Username or password error!");
		}
	}

	// Token: 0x0600158B RID: 5515 RVA: 0x000B88A2 File Offset: 0x000B6CA2
	public void MRUsernameCancelBtnPressed()
	{
		this.mrUsernameInput.value = string.Empty;
	}

	// Token: 0x0600158C RID: 5516 RVA: 0x000B88B4 File Offset: 0x000B6CB4
	public void MRPwdCancelBtnPressed()
	{
		this.mrPwdInput.value = string.Empty;
	}

	// Token: 0x0600158D RID: 5517 RVA: 0x000B88C6 File Offset: 0x000B6CC6
	public void MRVerifyPwdCancelBtnPressed()
	{
		this.mrVerifyPwdInput.value = string.Empty;
	}

	// Token: 0x0600158E RID: 5518 RVA: 0x000B88D8 File Offset: 0x000B6CD8
	public void MRSuccessOkBtnPressed()
	{
		this.mrSuccessTipNode.SetActive(false);
		this.loginNode.SetActive(true);
		string text = this.mrUsernameInputValue;
		string text2 = this.mrPasswordInputValue;
		if (text != string.Empty)
		{
			this.usernameInput.value = text;
		}
		if (text2 != string.Empty && UIUserDataController.GetRememberPwd() == 1)
		{
			this.passwordInput.value = text2;
			this.rememberPwdToggle.value = true;
		}
	}

	// Token: 0x0600158F RID: 5519 RVA: 0x000B895A File Offset: 0x000B6D5A
	public void MRUsernameTextboxClick()
	{
		this.mrUsernameInput.value = string.Empty;
	}

	// Token: 0x06001590 RID: 5520 RVA: 0x000B896C File Offset: 0x000B6D6C
	public void MRPwdTextboxClick()
	{
		this.mrPwdInput.value = string.Empty;
	}

	// Token: 0x06001591 RID: 5521 RVA: 0x000B897E File Offset: 0x000B6D7E
	public void MRVerifyPwdTextboxClick()
	{
		this.mrVerifyPwdInput.value = string.Empty;
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x000B8990 File Offset: 0x000B6D90
	public void MRUsernameTextboxChanged()
	{
		if (this.mrUsernameInput.value != string.Empty)
		{
			this.mrUsernameCancelBtn.gameObject.SetActive(true);
			this.mrUsernameInput.value = WordFilterInLogin.mInstance.FilterString(this.mrUsernameInput.value.ToLower());
			this.mrUsernameInput.value = this.RemoveInvalidCharacter(this.mrUsernameInput.value);
		}
		else
		{
			this.mrUsernameCancelBtn.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x000B8A20 File Offset: 0x000B6E20
	public void MRPwdTextboxChanged()
	{
		if (this.mrPwdInput.value != string.Empty)
		{
			this.mrPwdCancelBtn.gameObject.SetActive(true);
		}
		else
		{
			this.mrPwdCancelBtn.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x000B8A70 File Offset: 0x000B6E70
	public void MRVerifyPwdTextboxChanged()
	{
		if (this.mrVerifyPwdInput.value != string.Empty)
		{
			this.mrVerifyPwdCancelBtn.gameObject.SetActive(true);
		}
		else
		{
			this.mrVerifyPwdCancelBtn.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x000B8AC0 File Offset: 0x000B6EC0
	private void ProcessLoginProgress()
	{
		if (this.loginProgressBarPanelIsVisible)
		{
			this.TextureGif();
			if (GGCloudServiceLoginProcessBar.mInstance != null)
			{
				switch (GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.ResultStatus)
				{
				case ProgressStatus.Progressing:
					if (this.loginProgressBarTitleLabel.text != GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Title.ToUpper())
					{
						this.loginProgressBarTitleLabel.text = GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Title.ToUpper();
					}
					break;
				case ProgressStatus.Fail:
					this.loginProgressBarPanelIsVisible = false;
					this.loginProgressBarNode.SetActive(false);
					this.PopErrorTipPanel(GGCloudServiceLoginProcessBar.mInstance.mLoginProcessBarInfo.Result);
					break;
				case ProgressStatus.SuccessWithoutRoleName:
					this.loginProgressBarPanelIsVisible = false;
					this.loginProgressBarNode.SetActive(false);
					break;
				case ProgressStatus.Success:
					this.StartToPlay();
					break;
				}
			}
		}
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x000B8BBB File Offset: 0x000B6FBB
	private void ShowLoginProgressBarPanel()
	{
		this.loginProgressBarNode.SetActive(true);
		if (this.loadingBgTexture.mainTexture == null)
		{
			this.loadingBgTexture.mainTexture = (Resources.Load("UI/Images/General/LoadingBg") as Texture);
		}
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x000B8BFC File Offset: 0x000B6FFC
	private void TextureGif()
	{
		this.gifDeltaTime += Time.deltaTime;
		if (this.gifDeltaTime > 0.2f)
		{
			this.gifDeltaTime = 0f;
			this.textureIndex++;
			if (this.textureIndex > 2)
			{
				this.textureIndex = 1;
			}
			this.loadingTexture.mainTexture = this.tex[this.textureIndex - 1];
		}
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x000B8C71 File Offset: 0x000B7071
	public void LoginNodeCancelBtnPressed()
	{
		this.usernameInput.value = UIUserDataController.GetCurLoginUserName();
		this.passwordInput.value = UIUserDataController.GetCurLoginPassword();
		this.loginNode.SetActive(false);
		this.FirtLoadedNode.SetActive(true);
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x000B8CAC File Offset: 0x000B70AC
	public void GenerateRoleName()
	{
		this.creatRoleInput.value = GGCloudServiceGenerateName.GenerateSurname() + UnityEngine.Random.Range(0, 1000).ToString();
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x000B8CE7 File Offset: 0x000B70E7
	public void GDPRToggleValueChanged(UIToggle toggle)
	{
		UIUserDataController.SetGDPRToggleStatus(toggle.value);
		this.gdprOkBtn.isEnabled = toggle.value;
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x000B8D05 File Offset: 0x000B7105
	public void PrivacyBtnPressed()
	{
		Application.OpenURL("http://www.riovox.com/en/privacy/index.html");
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x000B8D11 File Offset: 0x000B7111
	public void TermsOfUseBtnPressed()
	{
		Application.OpenURL("http://www.riovox.com/en/terms/termsofuse-bf.html");
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x000B8D1D File Offset: 0x000B711D
	public void GDPROkBtnPressed()
	{
		this.isInit = false;
		if (!string.IsNullOrEmpty(this.gdprVersion))
		{
			UIUserDataController.SetGDPRVersion(this.gdprVersion);
		}
		this.gdprNode.SetActive(false);
		this.StartToHandle();
	}

	// Token: 0x0600159E RID: 5534 RVA: 0x000B8D53 File Offset: 0x000B7153
	private void GetGamePrivacyVersion(Action<string> callback)
	{
		base.StartCoroutine(this.InternalGetGamePrivacyVersion(callback));
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x000B8D64 File Offset: 0x000B7164
	private IEnumerator InternalGetGamePrivacyVersion(Action<string> callback)
	{
		WWW www = new WWW("http://www.riovox.com/config/games_privacy_version.txt");
		yield return www;
		if (string.IsNullOrEmpty(www.error) && !string.IsNullOrEmpty(www.text))
		{
			try
			{
				Dictionary<string, string> dictionary = JsonMapper.ToObject<Dictionary<string, string>>(www.text);
				string obj = null;
				dictionary.TryGetValue("BF", out obj);
				if (callback != null)
				{
					callback(obj);
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				if (callback != null)
				{
					callback(null);
				}
			}
		}
		else
		{
			Debug.LogError(www.error);
			if (callback != null)
			{
				callback(null);
			}
		}
		yield break;
	}

	// Token: 0x060015A0 RID: 5536 RVA: 0x000B8D7F File Offset: 0x000B717F
	private void NeedPopGDPRCanvas()
	{
		this.GetGamePrivacyVersion(delegate(string version)
		{
			if (string.IsNullOrEmpty(version))
			{
				this.needPopGDPRCanvas = false;
			}
			else
			{
				Debug.Log(version + "       " + UIUserDataController.GetGDPRVersion());
				if (version == UIUserDataController.GetGDPRVersion())
				{
					if (UIUserDataController.GetGDPRToggleStatus())
					{
						this.needPopGDPRCanvas = false;
					}
					else
					{
						this.needPopGDPRCanvas = true;
					}
				}
				else
				{
					this.gdprVersion = version;
					this.needPopGDPRCanvas = true;
				}
			}
			if (this.needPopGDPRCanvas)
			{
				this.isInit = true;
				this.gdprOkBtn.isEnabled = false;
				this.gdprNode.SetActive(true);
			}
			else
			{
				this.StartToHandle();
			}
		});
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x000B8D93 File Offset: 0x000B7193
	private void StartToHandle()
	{
		if (this.isInit)
		{
			return;
		}
		this.isInit = true;
		Debug.Log("aaaaaaaaaaaaaaa");
		this.FirtLoadedNode.SetActive(true);
		GGCloudServiceCreate.mInstance.BeforeCloudServiceCreate();
	}

	// Token: 0x04001839 RID: 6201
	public static UILoginNewDirector mInstance;

	// Token: 0x0400183A RID: 6202
	public UIInput usernameInput;

	// Token: 0x0400183B RID: 6203
	public UIInput passwordInput;

	// Token: 0x0400183C RID: 6204
	public UIButton usernameCancelBtn;

	// Token: 0x0400183D RID: 6205
	public UIButton passwordCancelBtn;

	// Token: 0x0400183E RID: 6206
	public UIToggle rememberPwdToggle;

	// Token: 0x0400183F RID: 6207
	public GameObject goACTUserDataPrefab;

	// Token: 0x04001840 RID: 6208
	public GameObject loginNode;

	// Token: 0x04001841 RID: 6209
	public GameObject m_ChartboostPrefab;

	// Token: 0x04001842 RID: 6210
	public GameObject QuickPlayBtn;

	// Token: 0x04001843 RID: 6211
	public GameObject FirtLoadedNode;

	// Token: 0x04001844 RID: 6212
	public UILabel versionTip;

	// Token: 0x04001845 RID: 6213
	private UILoginNewDirector.UINetworkRequestType networkRequestType;

	// Token: 0x04001846 RID: 6214
	private bool isFirstChangedForRememberToggle = true;

	// Token: 0x04001847 RID: 6215
	public UIPopupList usernamePoplist;

	// Token: 0x04001848 RID: 6216
	private bool isDropdownOpen;

	// Token: 0x04001849 RID: 6217
	public GameObject creatRoleNode;

	// Token: 0x0400184A RID: 6218
	public UIButton creatRoleCancelBtn;

	// Token: 0x0400184B RID: 6219
	public UIInput creatRoleInput;

	// Token: 0x0400184C RID: 6220
	public GameObject creatRoleSuccessNode;

	// Token: 0x0400184D RID: 6221
	public UILabel creatRoleSuccessLabel;

	// Token: 0x0400184E RID: 6222
	private string mrUsernameInputValue = string.Empty;

	// Token: 0x0400184F RID: 6223
	private string mrPasswordInputValue = string.Empty;

	// Token: 0x04001850 RID: 6224
	public GameObject registerSelectNode;

	// Token: 0x04001851 RID: 6225
	public GameObject autoRegisterTipNode;

	// Token: 0x04001852 RID: 6226
	public UILabel autoRegisterUsernameLabel;

	// Token: 0x04001853 RID: 6227
	public UILabel autoRegisterPasswordLabel;

	// Token: 0x04001854 RID: 6228
	public UILabel backupTipLabel;

	// Token: 0x04001855 RID: 6229
	public UIButton backupBtn;

	// Token: 0x04001856 RID: 6230
	public GameObject manualRegisterNode;

	// Token: 0x04001857 RID: 6231
	public UIInput mrUsernameInput;

	// Token: 0x04001858 RID: 6232
	public UIInput mrPwdInput;

	// Token: 0x04001859 RID: 6233
	public UIInput mrVerifyPwdInput;

	// Token: 0x0400185A RID: 6234
	public UIButton mrUsernameCancelBtn;

	// Token: 0x0400185B RID: 6235
	public UIButton mrPwdCancelBtn;

	// Token: 0x0400185C RID: 6236
	public UIButton mrVerifyPwdCancelBtn;

	// Token: 0x0400185D RID: 6237
	public GameObject mrSuccessTipNode;

	// Token: 0x0400185E RID: 6238
	public UILabel mrSuccessTipLabel;

	// Token: 0x0400185F RID: 6239
	public GameObject loginProgressBarNode;

	// Token: 0x04001860 RID: 6240
	public UILabel loginProgressBarTitleLabel;

	// Token: 0x04001861 RID: 6241
	public UITexture loadingTexture;

	// Token: 0x04001862 RID: 6242
	public UITexture loadingBgTexture;

	// Token: 0x04001863 RID: 6243
	private float gifDeltaTime;

	// Token: 0x04001864 RID: 6244
	private int textureIndex = 1;

	// Token: 0x04001865 RID: 6245
	private const int maxNum = 2;

	// Token: 0x04001866 RID: 6246
	private Texture[] tex = new Texture[2];

	// Token: 0x04001867 RID: 6247
	private bool loginProgressBarPanelIsVisible;

	// Token: 0x04001868 RID: 6248
	private const string ip = "52.26.193.247";

	// Token: 0x04001869 RID: 6249
	private const int port = 81;

	// Token: 0x0400186A RID: 6250
	public UIButton gdprOkBtn;

	// Token: 0x0400186B RID: 6251
	public GameObject gdprNode;

	// Token: 0x0400186C RID: 6252
	private string gdprVersion = string.Empty;

	// Token: 0x0400186D RID: 6253
	private bool isGDRPToggleOn;

	// Token: 0x0400186E RID: 6254
	private bool needPopGDPRCanvas;

	// Token: 0x0400186F RID: 6255
	private bool isInit;

	// Token: 0x020002D2 RID: 722
	private enum UINetworkRequestType
	{
		// Token: 0x04001871 RID: 6257
		Nil,
		// Token: 0x04001872 RID: 6258
		Login,
		// Token: 0x04001873 RID: 6259
		AutoRegister,
		// Token: 0x04001874 RID: 6260
		ManualRegister,
		// Token: 0x04001875 RID: 6261
		CreatRole
	}
}
