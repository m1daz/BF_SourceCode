using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200028C RID: 652
public class UIFriendSystemDirector : MonoBehaviour
{
	// Token: 0x06001270 RID: 4720 RVA: 0x000A517A File Offset: 0x000A357A
	private void Awake()
	{
		if (UIFriendSystemDirector.mInstance == null)
		{
			UIFriendSystemDirector.mInstance = this;
		}
	}

	// Token: 0x06001271 RID: 4721 RVA: 0x000A5192 File Offset: 0x000A3592
	private void OnDestroy()
	{
		if (UIFriendSystemDirector.mInstance != null)
		{
			UIFriendSystemDirector.mInstance = null;
		}
	}

	// Token: 0x06001272 RID: 4722 RVA: 0x000A51AA File Offset: 0x000A35AA
	private void Start()
	{
		this.curUsername = UIUserDataController.GetDefaultUserName();
		this.curPassword = UIUserDataController.GetDefaultPwd();
		this.RefreshBroadcastBtn();
		this.textList.Clear();
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x000A51D3 File Offset: 0x000A35D3
	private void Update()
	{
		if (this.friendNode.activeSelf)
		{
			this.RefreshFriendNodeTimer();
		}
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x000A51EC File Offset: 0x000A35EC
	private void RefreshFriendNodeTimer()
	{
		this.friendNodeTimerTime += Time.deltaTime;
		if (this.friendNodeTimerTime > 0.5f)
		{
			this.friendNodeTimerTime = 0f;
			if (GGCloudServiceKit.mInstance.mIsFriendInfoListReady)
			{
				List<CSFriendInfo> mFriendInfoList = GGCloudServiceKit.mInstance.mFriendInfoList;
				if (mFriendInfoList.Count > 0)
				{
					this.preListNum = this.allFriendList.Count;
					this.allFriendList.Clear();
					for (int i = 0; i < mFriendInfoList.Count; i++)
					{
						this.allFriendList.Add(mFriendInfoList[i]);
					}
					if (mFriendInfoList.Count > this.preListNum)
					{
						this.CreatFriendList();
					}
					this.RefreshFriendListUI();
				}
				if (this.loadingSprite.gameObject.activeSelf)
				{
					this.loadingSprite.gameObject.SetActive(false);
				}
			}
			this.RefreshBroadcastBtn();
		}
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x000A52DC File Offset: 0x000A36DC
	public void BackBtnPressed()
	{
		UIHomeDirector.mInstance.BackToRootNode(this.friendNode);
		if (GGCloudServiceKit.mInstance.mFriendRequestList.Count > 0)
		{
			UIHomeDirector.mInstance.haveNewMsgSprite.gameObject.SetActive(true);
		}
		else
		{
			UIHomeDirector.mInstance.haveNewMsgSprite.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x000A533D File Offset: 0x000A373D
	public void OpenFriendSysBtnPressed()
	{
		GGCloudServiceKit.mInstance.GetAllFriendInfoList(UIUserDataController.GetDefaultUserName());
		GGCloudServiceKit.mInstance.GetOfficialMessage(UIUserDataController.GetDefaultUserName());
		GGCloudServiceKit.mInstance.GetFriendRequest(UIUserDataController.GetDefaultUserName());
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x000A536C File Offset: 0x000A376C
	private void CreatFriendList()
	{
		List<CSFriendInfo> mFriendInfoList = GGCloudServiceKit.mInstance.mFriendInfoList;
		int num = mFriendInfoList.Count - this.preListNum;
		if (num > 0)
		{
			for (int i = 0; i < mFriendInfoList.Count; i++)
			{
				if (i >= this.preListNum)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.friendItemPrefab);
					gameObject.transform.parent = this.friendListScrollView.transform;
					gameObject.transform.localPosition = new Vector3(0f, (float)(-(float)i) * 90f - 180f, 0f);
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					if (i == 0)
					{
						gameObject.GetComponent<UIFriendInfoItemPrefab>().toggle.value = true;
					}
					this.friendItemObjList.Add(gameObject);
				}
				if (this.loadingSprite.gameObject.activeSelf)
				{
					this.loadingSprite.gameObject.SetActive(false);
				}
			}
			this.friendListScrollView.GetComponent<UIScrollView>().ResetPosition();
		}
	}

	// Token: 0x06001278 RID: 4728 RVA: 0x000A5484 File Offset: 0x000A3884
	private void RefreshFriendListUI()
	{
		if (this.allFriendList.Count > 0)
		{
			this.onlineNum = 0;
			for (int i = 0; i < this.allFriendList.Count; i++)
			{
				this.friendItemObjList[i].GetComponent<UIFriendInfoItemPrefab>().ReadData(this.allFriendList[i], i);
				if (this.allFriendList[i].IsOnline)
				{
					this.onlineNum++;
				}
				if (this.curSelectFriendName == this.allFriendList[i].RoleName)
				{
					this.curFriendIndex = i;
					this.friendItemObjList[i].GetComponent<UIFriendInfoItemPrefab>().toggle.value = true;
				}
			}
			this.friendNumLabel.text = string.Concat(new string[]
			{
				"(",
				this.onlineNum.ToString(),
				"/",
				this.allFriendList.Count.ToString(),
				")"
			});
			if (this.allFriendList[this.curFriendIndex].messageList.Count > this.preMsgNum)
			{
				for (int j = this.preMsgNum; j < this.allFriendList[this.curFriendIndex].messageList.Count; j++)
				{
					string text = this.allFriendList[this.curFriendIndex].messageList[j].sender + " : " + this.allFriendList[this.curFriendIndex].messageList[j].content;
					this.textList.Add(text);
				}
				this.allFriendList[this.curFriendIndex].ClearNewMessage();
				this.preMsgNum = this.allFriendList[this.curFriendIndex].messageList.Count;
			}
			this.FriendSelected(this.curFriendIndex);
		}
	}

	// Token: 0x06001279 RID: 4729 RVA: 0x000A56A4 File Offset: 0x000A3AA4
	public void FriendSelected(int index)
	{
		this.curFriendIndex = index;
		this.curSelectFriendName = this.allFriendList[index].RoleName;
		this.textList.Clear();
		if (this.allFriendList.Count > 0)
		{
			this.preMsgNum = this.allFriendList[index].messageList.Count;
			for (int i = 0; i < this.preMsgNum; i++)
			{
				string text = this.allFriendList[index].messageList[i].sender + " : " + this.allFriendList[index].messageList[i].content;
				this.textList.Add(text);
			}
			this.allFriendList[this.curFriendIndex].ClearNewMessage();
			if (this.allFriendList[this.curFriendIndex].IsInRoom)
			{
				this.roomInfoNode.SetActive(true);
				string path = string.Empty;
				if (this.mHuntingDicKeyMapNameValueIndex.ContainsKey(this.allFriendList[this.curFriendIndex].mapName))
				{
					int num = this.mHuntingDicKeyMapNameValueIndex[this.allFriendList[this.curFriendIndex].mapName];
					path = "UI/Images/Maps/Hunting/Map_" + num.ToString() + "_" + this.mHuntingMapNameSuffix[num - 1];
				}
				else
				{
					int num = this.mDicKeyMapNameValueIndex[this.allFriendList[this.curFriendIndex].mapName];
					path = "UI/Images/Maps/Map_" + num.ToString() + "_" + this.mMapNameSuffix[num - 1];
				}
				this.mapTexture.mainTexture = (Resources.Load(path) as Texture);
				this.roomNameLabel.text = string.Concat(new string[]
				{
					this.allFriendList[this.curFriendIndex].Room,
					"(",
					this.allFriendList[this.curFriendIndex].playersCount,
					"/",
					this.allFriendList[this.curFriendIndex].maxplayersCount,
					")"
				});
				this.modeSprite.spriteName = "FriendModeLogo_" + this.allFriendList[this.curFriendIndex].modeName;
				if (this.allFriendList[this.curFriendIndex].encrytion)
				{
					this.lockSprite.gameObject.SetActive(true);
				}
				else
				{
					this.lockSprite.gameObject.SetActive(false);
				}
			}
			else
			{
				this.roomInfoNode.SetActive(false);
			}
		}
	}

	// Token: 0x0600127A RID: 4730 RVA: 0x000A597C File Offset: 0x000A3D7C
	public void FriendChatInputSubmit()
	{
		this.chatInput.isSelected = false;
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x000A598C File Offset: 0x000A3D8C
	public void SendMsgBtnPressed()
	{
		if (this.chatInput.value != string.Empty)
		{
			string message = WordFilter.mInstance.FilterString(this.chatInput.value);
			if (this.allFriendList.Count > 0 && this.allFriendList[this.curFriendIndex] != null)
			{
				GGNetworkChatSys.mInstance.SendPrivateMessage(this.allFriendList[this.curFriendIndex].RoleName, message);
				this.chatInput.value = string.Empty;
			}
		}
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x000A5A24 File Offset: 0x000A3E24
	public void JoinInBtnPressed()
	{
		GGServerRegion currentServerRegion = GGNetworkKit.mInstance.GetCurrentServerRegion();
		if (currentServerRegion == this.allFriendList[this.curFriendIndex].region)
		{
			if (!this.allFriendList[this.curFriendIndex].open)
			{
				EventDelegate btnEventName = new EventDelegate(this, "HideCurTip");
				UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "Room Is Not Joinable!", Color.white, "OK", string.Empty, btnEventName, null, null);
			}
			else if (this.allFriendList[this.curFriendIndex].encrytion)
			{
				this.PopPasswordInputPanel();
			}
			else
			{
				UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, null, null, null, null, null);
				GGNetworkKit.mInstance.JoinFriendRoom(this.allFriendList[this.curFriendIndex].Room, this.allFriendList[this.curFriendIndex].region, this.allFriendList[this.curFriendIndex].modeName);
			}
		}
		else
		{
			this.PopChangeServerTipPanel();
		}
	}

	// Token: 0x0600127D RID: 4733 RVA: 0x000A5B40 File Offset: 0x000A3F40
	private void PopPasswordInputPanel()
	{
		EventDelegate btnEventName = new EventDelegate(this, "HideCurTip");
		EventDelegate btnEventName2 = new EventDelegate(this, "PasswordJoinBtnPressed");
		UITipController.mInstance.SetTipData(UITipController.TipType.InputPasswordTip, "Input Password.", Color.white, "Cancel", "Join", btnEventName, btnEventName2, null);
	}

	// Token: 0x0600127E RID: 4734 RVA: 0x000A5B88 File Offset: 0x000A3F88
	private void PopChangeServerTipPanel()
	{
		string tipContent = "Change server region to " + this.allFriendList[this.curFriendIndex].region.ToString().ToUpper() + "?";
		EventDelegate btnEventName = new EventDelegate(this, "ChangeServerYesBtnPressed");
		EventDelegate btnEventName2 = new EventDelegate(this, "ChangeServerNoBtnPressed");
		UITipController.mInstance.SetTipData(UITipController.TipType.TwoButtonTip, tipContent, Color.white, "YES", "NO", btnEventName, btnEventName2, null);
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x000A5C04 File Offset: 0x000A4004
	public void ChangeServerYesBtnPressed()
	{
		UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, null, null, null, null, null);
		if (this.allFriendList[this.curFriendIndex].encrytion)
		{
			this.PopPasswordInputPanel();
		}
		else
		{
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, null, null, null, null, null);
			GGNetworkKit.mInstance.JoinFriendRoom(this.allFriendList[this.curFriendIndex].Room, this.allFriendList[this.curFriendIndex].region, this.allFriendList[this.curFriendIndex].modeName);
		}
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x000A5CB7 File Offset: 0x000A40B7
	public void ChangeServerNoBtnPressed()
	{
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x000A5CBC File Offset: 0x000A40BC
	public void PasswordJoinBtnPressed()
	{
		if (UITipController.mInstance.passwordInput.value == this.allFriendList[this.curFriendIndex].password)
		{
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, null, null, null, null, null);
			GGNetworkKit.mInstance.JoinFriendRoom(this.allFriendList[this.curFriendIndex].Room, this.allFriendList[this.curFriendIndex].region, this.allFriendList[this.curFriendIndex].modeName);
		}
		else
		{
			EventDelegate btnEventName = new EventDelegate(this, "HideCurTip");
			EventDelegate btnEventName2 = new EventDelegate(this, "PasswordJoinBtnPressed");
			UITipController.mInstance.SetTipData(UITipController.TipType.InputPasswordTip, "Password Error!", Color.red, "Cancel", "Join", btnEventName, btnEventName2, null);
		}
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x000A5D9D File Offset: 0x000A419D
	public void PasswordCancelBtnPressed()
	{
		UITipController.mInstance.HideCurTip();
		UITipController.mInstance.passwordInput.value = "Input password";
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x000A5DC0 File Offset: 0x000A41C0
	public void JoinroomFailed()
	{
		EventDelegate btnEventName = new EventDelegate(this, "HideCurTip");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "The room is not exist or full.", Color.white, "OK", string.Empty, btnEventName, null, null);
	}

	// Token: 0x06001284 RID: 4740 RVA: 0x000A5DFC File Offset: 0x000A41FC
	private void RefreshBroadcastBtn()
	{
		this.userAddRequestList = GGCloudServiceKit.mInstance.mFriendRequestList;
		if (this.userAddRequestList.Count > 0)
		{
			if (!this.newBroadcastSprite.gameObject.activeSelf)
			{
				this.newBroadcastSprite.gameObject.SetActive(true);
			}
		}
		else if (this.newBroadcastSprite.gameObject.activeSelf)
		{
			this.newBroadcastSprite.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001285 RID: 4741 RVA: 0x000A5E7C File Offset: 0x000A427C
	public void BroadcastBtnPressed()
	{
		this.RefreshBroadcastBtn();
		this.curBroadIndex = 0;
		if (this.userAddRequestList.Count > 0)
		{
			this.roleAddRequestList.Clear();
			for (int i = 0; i < this.userAddRequestList.Count; i++)
			{
				string text = string.Empty;
				if (GGCloudServiceKit.mInstance.mFriendRequestKeyUserNameValueRoleNameDic.ContainsKey(this.userAddRequestList[i]))
				{
					text = GGCloudServiceKit.mInstance.mFriendRequestKeyUserNameValueRoleNameDic[this.userAddRequestList[i]];
				}
				if (text != string.Empty)
				{
					this.roleAddRequestList.Add(text);
				}
			}
			this.PopAddRequestTip();
		}
	}

	// Token: 0x06001286 RID: 4742 RVA: 0x000A5F38 File Offset: 0x000A4338
	private void PopAddRequestTip()
	{
		if (this.roleAddRequestList.Count > 0 && this.curBroadIndex < this.roleAddRequestList.Count)
		{
			string tipContent = this.roleAddRequestList[this.curBroadIndex] + " want to be your friend.";
			EventDelegate btnEventName = new EventDelegate(this, "RefuseBtnPressed");
			EventDelegate btnEventName2 = new EventDelegate(this, "AcceptBtnPressed");
			UITipController.mInstance.SetTipData(UITipController.TipType.TwoButtonPlusTip, tipContent, Color.white, "Refuse", "Accept", btnEventName, btnEventName2, null);
		}
	}

	// Token: 0x06001287 RID: 4743 RVA: 0x000A5FC0 File Offset: 0x000A43C0
	public void AcceptBtnPressedCoroutine(string friendUserName, string friendRoleName)
	{
		object obj = this.mLockAcceptFriendRequest;
		lock (obj)
		{
			if (friendUserName != string.Empty)
			{
				GGCloudServiceKit.mInstance.AcceptFriendRequest(this.curUsername, friendUserName);
				GGNetworkChatSys.mInstance.SendFriendAcceptMessage(friendRoleName);
			}
		}
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x000A6024 File Offset: 0x000A4424
	public void AcceptBtnPressed()
	{
		GGCloudServiceKit.mInstance.GetUserNameByRoleNameAsyForAcceptFriendInLobby(this.roleAddRequestList[this.curBroadIndex]);
		if (this.curBroadIndex < this.roleAddRequestList.Count - 1)
		{
			this.curBroadIndex++;
			this.PopAddRequestTip();
		}
		else
		{
			UITipController.mInstance.HideCurTip();
			this.newBroadcastSprite.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001289 RID: 4745 RVA: 0x000A6098 File Offset: 0x000A4498
	public void RefuseBtnPressedCoroutine(string friendUserName)
	{
		object obj = this.mLockRefuseFriendRequest;
		lock (obj)
		{
			if (friendUserName != string.Empty)
			{
				GGCloudServiceKit.mInstance.RejectFriendRequest(this.curUsername, friendUserName);
			}
		}
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x000A60F0 File Offset: 0x000A44F0
	public void RefuseBtnPressed()
	{
		GGCloudServiceKit.mInstance.GetUserNameByRoleNameAsyForRefuseFriendInLobby(this.roleAddRequestList[this.curBroadIndex]);
		if (this.curBroadIndex < this.roleAddRequestList.Count - 1)
		{
			this.curBroadIndex++;
			this.PopAddRequestTip();
		}
		else
		{
			UITipController.mInstance.HideCurTip();
			this.newBroadcastSprite.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x000A6164 File Offset: 0x000A4564
	public void AddFriendBtnPressed()
	{
		string tipContent = "Input name to add a friend.";
		EventDelegate btnEventName = new EventDelegate(this, "HideCurTip");
		EventDelegate btnEventName2 = new EventDelegate(this, "AddRequestSendBtnPressed");
		UITipController.mInstance.SetTipData(UITipController.TipType.InputTip, tipContent, Color.white, "Cancel", "Add", btnEventName, btnEventName2, null);
	}

	// Token: 0x0600128C RID: 4748 RVA: 0x000A61AD File Offset: 0x000A45AD
	public void AddFriendClick()
	{
	}

	// Token: 0x0600128D RID: 4749 RVA: 0x000A61B0 File Offset: 0x000A45B0
	public void AddFriendSubmit()
	{
		if (UITipController.mInstance.input.value != string.Empty)
		{
			UITipController.mInstance.input.value = WordFilter.mInstance.FilterString(UITipController.mInstance.input.value);
		}
	}

	// Token: 0x0600128E RID: 4750 RVA: 0x000A6204 File Offset: 0x000A4604
	public void AddRequestSendBtnPressedLaterRoleNameNotExist()
	{
		EventDelegate btnEventName = new EventDelegate(this, "AddFriendBtnPressed");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "Role name does not exist!", Color.white, "OK", string.Empty, btnEventName, null, null);
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x000A6240 File Offset: 0x000A4640
	public void AddRequestSendBtnPressedLater(string friendUserName)
	{
		object obj = this.mLockAddFriendRequest;
		lock (obj)
		{
			List<CSFriendInfo> mFriendInfoList = GGCloudServiceKit.mInstance.mFriendInfoList;
			if (mFriendInfoList.Count > 0)
			{
				for (int i = 0; i < mFriendInfoList.Count; i++)
				{
					if (UITipController.mInstance.input.value == GGCloudServiceKit.mInstance.mUserNameRoleNameDic[mFriendInfoList[i].Name])
					{
						EventDelegate btnEventName = new EventDelegate(this, "AddFriendBtnPressed");
						UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "The friend had been added!", Color.white, "OK", string.Empty, btnEventName, null, null);
						return;
					}
				}
			}
			GGCloudServiceKit.mInstance.AddFriendRequest(UIUserDataController.GetDefaultUserName(), friendUserName, UIUserDataController.GetDefaultRoleName());
			GGNetworkChatSys.mInstance.SendFriendRequestMessage(UITipController.mInstance.input.value);
			EventDelegate btnEventName2 = new EventDelegate(this, "AddFriendBtnPressed");
			UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "Request sent successfully! Please wait for confirmation.", Color.white, "OK", string.Empty, btnEventName2, null, null);
		}
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x000A636C File Offset: 0x000A476C
	public void AddRequestSendBtnPressed()
	{
		if (UITipController.mInstance.input.value != string.Empty)
		{
			UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Loading...", Color.white, null, null, null, null, null);
			GGCloudServiceKit.mInstance.GetUserNameByRoleNameAsyForAddFriendInLobby(UITipController.mInstance.input.value);
		}
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x000A63CA File Offset: 0x000A47CA
	private void HideCurTip()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x04001527 RID: 5415
	public static UIFriendSystemDirector mInstance;

	// Token: 0x04001528 RID: 5416
	public GameObject friendNode;

	// Token: 0x04001529 RID: 5417
	public GameObject roomInfoNode;

	// Token: 0x0400152A RID: 5418
	public UILabel friendNumLabel;

	// Token: 0x0400152B RID: 5419
	public GameObject friendItemPrefab;

	// Token: 0x0400152C RID: 5420
	public GameObject friendListScrollView;

	// Token: 0x0400152D RID: 5421
	private List<GameObject> friendItemObjList = new List<GameObject>();

	// Token: 0x0400152E RID: 5422
	private List<CSFriendInfo> allFriendList = new List<CSFriendInfo>();

	// Token: 0x0400152F RID: 5423
	public UITextList textList;

	// Token: 0x04001530 RID: 5424
	public UIInput chatInput;

	// Token: 0x04001531 RID: 5425
	private int curFriendIndex;

	// Token: 0x04001532 RID: 5426
	private string curSelectFriendName = string.Empty;

	// Token: 0x04001533 RID: 5427
	private int onlineNum;

	// Token: 0x04001534 RID: 5428
	private string curUsername = string.Empty;

	// Token: 0x04001535 RID: 5429
	private string curPassword = string.Empty;

	// Token: 0x04001536 RID: 5430
	public UISprite loadingSprite;

	// Token: 0x04001537 RID: 5431
	private string[] mMapNameSuffix = new string[]
	{
		"Platform",
		"Warehouse",
		"Snow",
		"Dusty",
		"KnifeFactory",
		"DesertA",
		"DesertB",
		"ChessHero",
		"MazeVillage",
		"ChristmasTown",
		"FloatingCity",
		"Macross",
		"FantasyCastle",
		"ChristmasHouse",
		"Trench",
		"Desert",
		"Exhibition",
		"ShippingPort",
		"LavaCellar"
	};

	// Token: 0x04001538 RID: 5432
	private Dictionary<string, int> mDicKeyMapNameValueIndex = new Dictionary<string, int>
	{
		{
			"DEATH PLATFORM",
			1
		},
		{
			"WAREHOUSE",
			2
		},
		{
			"SNOW",
			3
		},
		{
			"DUSTY",
			4
		},
		{
			"KNIFE FACTORY",
			5
		},
		{
			"DESERT A",
			6
		},
		{
			"DESERT B",
			7
		},
		{
			"CHESS HERO",
			8
		},
		{
			"MAZE VILLAGE",
			9
		},
		{
			"CHRISTMAS TOWN",
			10
		},
		{
			"FLOATING CITY",
			11
		},
		{
			"MACROSS",
			12
		},
		{
			"FANTASY CASTLE",
			13
		},
		{
			"CHRISTMAS HOUSE",
			14
		},
		{
			"STORM TRENCH",
			15
		},
		{
			"DESERT",
			16
		},
		{
			"EXHIBITION",
			17
		},
		{
			"SHIPPING PORT",
			18
		},
		{
			"LAVA CELLAR",
			19
		}
	};

	// Token: 0x04001539 RID: 5433
	private string[] mHuntingMapNameArray = new string[]
	{
		"MECHANICAL SQUARE"
	};

	// Token: 0x0400153A RID: 5434
	private Dictionary<string, int> mHuntingDicKeyMapNameValueIndex = new Dictionary<string, int>
	{
		{
			"MECHANICAL SQUARE",
			1
		}
	};

	// Token: 0x0400153B RID: 5435
	private string[] mHuntingMapNameSuffix = new string[]
	{
		"MechanicalSquare"
	};

	// Token: 0x0400153C RID: 5436
	private float friendNodeTimerTime;

	// Token: 0x0400153D RID: 5437
	private int preListNum;

	// Token: 0x0400153E RID: 5438
	public UITexture mapTexture;

	// Token: 0x0400153F RID: 5439
	public UILabel roomNameLabel;

	// Token: 0x04001540 RID: 5440
	public UISprite modeSprite;

	// Token: 0x04001541 RID: 5441
	public UISprite lockSprite;

	// Token: 0x04001542 RID: 5442
	private int preMsgNum;

	// Token: 0x04001543 RID: 5443
	public UISprite newBroadcastSprite;

	// Token: 0x04001544 RID: 5444
	private List<string> userAddRequestList;

	// Token: 0x04001545 RID: 5445
	private List<string> roleAddRequestList = new List<string>();

	// Token: 0x04001546 RID: 5446
	private int curBroadIndex;

	// Token: 0x04001547 RID: 5447
	private object mLockAcceptFriendRequest = new object();

	// Token: 0x04001548 RID: 5448
	private object mLockRefuseFriendRequest = new object();

	// Token: 0x04001549 RID: 5449
	private object mLockAddFriendRequest = new object();

	// Token: 0x0400154A RID: 5450
	private bool isSuccess;
}
