using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200028E RID: 654
public class UIChatSystemDirector : MonoBehaviour
{
	// Token: 0x06001297 RID: 4759 RVA: 0x000A6501 File Offset: 0x000A4901
	private void Awake()
	{
		if (UIChatSystemDirector.mInstance == null)
		{
			UIChatSystemDirector.mInstance = this;
		}
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x000A6519 File Offset: 0x000A4919
	private void OnDestroy()
	{
		if (UIChatSystemDirector.mInstance != null)
		{
			UIChatSystemDirector.mInstance = null;
		}
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x000A6531 File Offset: 0x000A4931
	private void Start()
	{
		this.InitUI();
	}

	// Token: 0x0600129A RID: 4762 RVA: 0x000A6539 File Offset: 0x000A4939
	private void Update()
	{
		this.RefreshAllMessage();
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x000A6544 File Offset: 0x000A4944
	private void InitUI()
	{
		for (int i = 0; i < 8; i++)
		{
			this.phraseLabel[i].text = UIUserDataController.GetPhrase(i);
			this.phraseInput[i].label.text = UIUserDataController.GetPhrase(i);
		}
		this.killMsgLabel.text = string.Empty;
		this.sysChatMsgLabel.text = string.Empty;
		this.chatMsgLabel.text = string.Empty;
		TweenAlpha.Begin(this.playerNameListNode, 0.1f, 0f, 0f);
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x000A65D9 File Offset: 0x000A49D9
	public void CloseBtnPressed()
	{
		this.chatNode.SetActive(false);
		UIPauseDirector.mInstance.isCutControl = false;
	}

	// Token: 0x0600129D RID: 4765 RVA: 0x000A65F4 File Offset: 0x000A49F4
	public void SendMsgBtnPressed()
	{
		if (!string.IsNullOrEmpty(this.chatInput.label.text))
		{
			this.PackageAndSendChatSysMessage(this.chatInput.label.text);
			this.chatInput.label.text = string.Empty;
		}
	}

	// Token: 0x0600129E RID: 4766 RVA: 0x000A6646 File Offset: 0x000A4A46
	public void PhraseBtnPressed(int index)
	{
		this.PhraseTextSubmit();
		this.curPhraseIndex = index - 1;
		this.PackageAndSendChatSysMessage(UIUserDataController.GetPhrase(this.curPhraseIndex));
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x000A6668 File Offset: 0x000A4A68
	private void PackageAndSendChatSysMessage(string msgContent)
	{
		if (this.chatObjType == UIChatSystemDirector.UIChatObjectType.All)
		{
			this.ggChatMessage.content = this.FilterString(msgContent);
			this.ggChatMessage.chatmessagesubtype = GGChatMessageSubType.Public;
			this.ggChatMessage.displayed = false;
			this.ggChatMessage.name = GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetworkCharacter>().mPlayerProperties.name;
			this.ggChatMessage.team = GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetworkCharacter>().mPlayerProperties.team;
			GGNetworkChat.mInstance.ChatMessage(this.ggChatMessage, -1);
		}
		else if (this.chatObjType == UIChatSystemDirector.UIChatObjectType.Team)
		{
			this.ggChatMessage.content = this.FilterString(msgContent);
			this.ggChatMessage.chatmessagesubtype = GGChatMessageSubType.Team;
			this.ggChatMessage.displayed = false;
			this.ggChatMessage.name = GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetworkCharacter>().mPlayerProperties.name;
			this.ggChatMessage.team = GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetworkCharacter>().mPlayerProperties.team;
			this.ggChatMessage.receiverTeam = this.ggChatMessage.team;
			GGNetworkChat.mInstance.ChatMessage(this.ggChatMessage, -1);
		}
		else if (this.chatObjType == UIChatSystemDirector.UIChatObjectType.Private && this.otherPlayerPropertiesList.Count > 0)
		{
			this.ggChatMessage.content = this.FilterString(msgContent);
			this.ggChatMessage.chatmessagesubtype = GGChatMessageSubType.Private;
			this.ggChatMessage.displayed = false;
			this.ggChatMessage.name = GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetworkCharacter>().mPlayerProperties.name;
			this.ggChatMessage.team = GGNetworkKit.mInstance.GetMainPlayer().GetComponent<GGNetworkCharacter>().mPlayerProperties.team;
			this.ggChatMessage.receiverName = this.otherPlayerPropertiesList[this.curPrivateChatIndex].name;
			this.ggChatMessage.receiverTeam = this.otherPlayerPropertiesList[this.curPrivateChatIndex].team;
			GGNetworkChat.mInstance.ChatMessage(this.ggChatMessage, this.otherPlayerPropertiesList[this.curPrivateChatIndex].id);
		}
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x000A68AC File Offset: 0x000A4CAC
	private string ParseAndShowChatSysMessage(GGNetworkSystemAndChatMessage newMsg)
	{
		string result = string.Empty;
		if (newMsg != null)
		{
			if (newMsg.chatMesaageType == GGChatMessageType.ChatMessage)
			{
				if (newMsg.chatmessagesubtype == GGChatMessageSubType.Public)
				{
					string colorCode;
					if (newMsg.team == GGTeamType.blue)
					{
						colorCode = UIToolFunctionController.GetColorCode(GGColor.Blue);
					}
					else
					{
						colorCode = UIToolFunctionController.GetColorCode(GGColor.Red);
					}
					result = string.Concat(new string[]
					{
						"[A] ",
						colorCode,
						newMsg.name,
						"[-] : ",
						newMsg.content
					});
				}
				else if (newMsg.chatmessagesubtype == GGChatMessageSubType.Team)
				{
					if (newMsg.receiverTeam == GGNetworkKit.mInstance.GetManagePlayerProperties().GetMainPlayerProperty().team)
					{
						string colorCode;
						if (newMsg.team == GGTeamType.blue)
						{
							colorCode = UIToolFunctionController.GetColorCode(GGColor.Blue);
						}
						else
						{
							colorCode = UIToolFunctionController.GetColorCode(GGColor.Red);
						}
						result = string.Concat(new string[]
						{
							UIToolFunctionController.GetColorCode(GGColor.Green),
							"[T] [-]",
							colorCode,
							newMsg.name,
							"[-] : ",
							newMsg.content
						});
					}
				}
				else if (newMsg.chatmessagesubtype == GGChatMessageSubType.Private)
				{
					string colorCode;
					if (newMsg.team == GGTeamType.blue)
					{
						colorCode = UIToolFunctionController.GetColorCode(GGColor.Blue);
					}
					else
					{
						colorCode = UIToolFunctionController.GetColorCode(GGColor.Red);
					}
					string colorCode2;
					if (newMsg.receiverTeam == GGTeamType.blue)
					{
						colorCode2 = UIToolFunctionController.GetColorCode(GGColor.Blue);
					}
					else
					{
						colorCode2 = UIToolFunctionController.GetColorCode(GGColor.Red);
					}
					string text = (!(newMsg.name == UIUserDataController.GetDefaultRoleName())) ? newMsg.name : "You";
					string text2 = (!(newMsg.receiverName == UIUserDataController.GetDefaultRoleName())) ? newMsg.receiverName : "You";
					result = string.Concat(new string[]
					{
						UIToolFunctionController.GetColorCode(GGColor.Purple),
						"[P] [-]",
						colorCode,
						text,
						"[-] To ",
						colorCode2,
						text2,
						"[-] : ",
						newMsg.content
					});
				}
			}
			else
			{
				result = newMsg.content;
			}
		}
		return result;
	}

	// Token: 0x060012A1 RID: 4769 RVA: 0x000A6AA4 File Offset: 0x000A4EA4
	public void OnSubmit()
	{
		this.chatInput.isSelected = false;
	}

	// Token: 0x060012A2 RID: 4770 RVA: 0x000A6AB4 File Offset: 0x000A4EB4
	private void RefreshAllMessage()
	{
		this.deltaTime += Time.deltaTime;
		for (int i = 0; i < this.sysChatMsgLiveTimeList.Count; i++)
		{
			List<float> list;
			int index;
			(list = this.sysChatMsgLiveTimeList)[index = i] = list[index] + Time.deltaTime;
		}
		if (this.deltaTime > 0.2f)
		{
			this.deltaTime = 0f;
			this.RefreshSysChatMessage();
		}
		this.deltaTimeKillInfo += Time.deltaTime;
		for (int j = 0; j < this.killInfoLiveTimeList.Count; j++)
		{
			List<float> list;
			int index2;
			(list = this.killInfoLiveTimeList)[index2 = j] = list[index2] + Time.deltaTime;
		}
		if (this.deltaTimeKillInfo > this.killInfoRefreshCycleTime)
		{
			this.deltaTimeKillInfo = 0f;
			this.RefreshKillMessage();
		}
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x000A6B9C File Offset: 0x000A4F9C
	private void RefreshKillMessage()
	{
		string empty = string.Empty;
		string spriteName = string.Empty;
		string empty2 = string.Empty;
		float item = 0f;
		for (int i = 0; i < this.killMessageShowList.Count; i++)
		{
			if (this.killInfoLiveTimeList[i] > 2f)
			{
				this.killInfoLiveTimeList.RemoveAt(i);
				this.killMessageShowList.RemoveAt(i);
				i = 0;
			}
		}
		GGNetworkKillMessage ggnetworkKillMessage = GGNetworkChat.mInstance.PopKillMessage();
		if (ggnetworkKillMessage != null)
		{
			if (this.killMessageShowList.Count < 6)
			{
				this.killMessageShowList.Add(ggnetworkKillMessage);
				this.killInfoLiveTimeList.Add(item);
			}
			else
			{
				this.killMessageShowList.Add(ggnetworkKillMessage);
				this.killInfoLiveTimeList.Add(item);
				this.killMessageShowList.RemoveAt(0);
				this.killInfoLiveTimeList.RemoveAt(0);
			}
		}
		for (int j = 0; j < this.killMessageShowList.Count; j++)
		{
			string colorCode;
			if (this.killMessageShowList[j].killerTeam == GGTeamType.blue)
			{
				colorCode = UIToolFunctionController.GetColorCode(GGColor.Blue);
			}
			else if (this.killMessageShowList[j].killerTeam == GGTeamType.red)
			{
				colorCode = UIToolFunctionController.GetColorCode(GGColor.Red);
			}
			else
			{
				colorCode = UIToolFunctionController.GetColorCode(GGColor.White);
			}
			string colorCode2;
			if (this.killMessageShowList[j].theDeadTeam == GGTeamType.blue)
			{
				colorCode2 = UIToolFunctionController.GetColorCode(GGColor.Blue);
			}
			else
			{
				colorCode2 = UIToolFunctionController.GetColorCode(GGColor.Red);
			}
			spriteName = this.GetWeaponLogoName(this.killMessageShowList[j].gun);
			if (!this.killerLabel[j].gameObject.activeSelf)
			{
				this.killerLabel[j].gameObject.SetActive(true);
			}
			this.killerLabel[j].text = colorCode + this.killMessageShowList[j].killer + "[-]";
			this.weaponSprite[j].spriteName = spriteName;
			if (this.killMessageShowList[j].headShot)
			{
				this.headshotSprite[j].gameObject.SetActive(true);
				this.deadLabel[j].text = "     " + colorCode2 + this.killMessageShowList[j].theDead + "[-]";
			}
			else
			{
				this.headshotSprite[j].gameObject.SetActive(false);
				this.deadLabel[j].text = colorCode2 + this.killMessageShowList[j].theDead + "[-]";
			}
		}
		for (int k = this.killMessageShowList.Count; k < 6; k++)
		{
			if (this.killerLabel[k].gameObject.activeSelf)
			{
				this.killerLabel[k].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x000A6E90 File Offset: 0x000A5290
	private void RefreshSysChatMessage()
	{
		float item = 0f;
		for (int i = 0; i < this.sysChatMessageShowList.Count; i++)
		{
			if (this.sysChatMsgLiveTimeList[i] > 4f)
			{
				this.sysChatMsgLiveTimeList.RemoveAt(i);
				this.sysChatMessageShowList.RemoveAt(i);
				i = 0;
			}
		}
		GGNetworkSystemAndChatMessage ggnetworkSystemAndChatMessage = GGNetworkChat.mInstance.PopSystemAndChatMessage();
		if (ggnetworkSystemAndChatMessage != null)
		{
			if (this.sysChatMessageShowList.Count < 2)
			{
				this.sysChatMessageShowList.Add(ggnetworkSystemAndChatMessage);
				this.sysChatMsgLiveTimeList.Add(item);
			}
			else
			{
				this.sysChatMessageShowList.Add(ggnetworkSystemAndChatMessage);
				this.sysChatMsgLiveTimeList.Add(item);
				this.sysChatMessageShowList.RemoveAt(0);
				this.sysChatMsgLiveTimeList.RemoveAt(0);
			}
		}
		string text = string.Empty;
		if (this.sysChatMessageShowList.Count > 0)
		{
			for (int j = 0; j < this.sysChatMessageShowList.Count; j++)
			{
				if (text != string.Empty)
				{
					text += "\n";
				}
				text += this.ParseAndShowChatSysMessage(this.sysChatMessageShowList[j]);
			}
		}
		this.sysChatMsgLabel.text = text;
		if (ggnetworkSystemAndChatMessage != null && ggnetworkSystemAndChatMessage.chatMesaageType == GGChatMessageType.ChatMessage)
		{
			string text2 = this.ParseAndShowChatSysMessage(ggnetworkSystemAndChatMessage);
			if (text2 != string.Empty)
			{
				this.textList.Add(text2);
			}
		}
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x000A7018 File Offset: 0x000A5418
	public void EditToggleValueChanged(int index)
	{
		if (!this.hasInitUI)
		{
			this.hasInitUI = true;
			return;
		}
		if (this.phraseToggle[index - 1].value)
		{
			if (this.curPhraseIndex != index - 1)
			{
				this.PhraseTextSubmit();
			}
			this.curPhraseIndex = index - 1;
			this.phraseInput[this.curPhraseIndex].gameObject.SetActive(true);
			this.phraseInput[this.curPhraseIndex].label.text = this.phraseLabel[this.curPhraseIndex].text;
			this.phraseLabel[this.curPhraseIndex].gameObject.SetActive(false);
		}
		else
		{
			this.phraseLabel[this.curPhraseIndex].text = this.phraseInput[this.curPhraseIndex].label.text;
			this.phraseInput[this.curPhraseIndex].gameObject.SetActive(false);
			this.phraseLabel[this.curPhraseIndex].gameObject.SetActive(true);
			UIUserDataController.SetPhrase(this.curPhraseIndex, this.phraseLabel[this.curPhraseIndex].text);
			this.curPhraseIndex = index - 1;
		}
	}

	// Token: 0x060012A6 RID: 4774 RVA: 0x000A7146 File Offset: 0x000A5546
	public void PhraseTextClick()
	{
		this.phraseInput[this.curPhraseIndex].label.text = null;
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x000A7160 File Offset: 0x000A5560
	public void PhraseTextSubmit()
	{
		this.phraseToggle[this.curPhraseIndex].value = false;
		this.phraseLabel[this.curPhraseIndex].text = this.phraseInput[this.curPhraseIndex].label.text;
		this.phraseInput[this.curPhraseIndex].gameObject.SetActive(false);
		this.phraseLabel[this.curPhraseIndex].gameObject.SetActive(true);
		UIUserDataController.SetPhrase(this.curPhraseIndex, this.phraseLabel[this.curPhraseIndex].text);
	}

	// Token: 0x060012A8 RID: 4776 RVA: 0x000A71F8 File Offset: 0x000A55F8
	public string GetWeaponLogoName(int index)
	{
		string result = string.Empty;
		string[] allWeaponNameList = GrowthManagerKit.GetAllWeaponNameList();
		if (index <= allWeaponNameList.Length)
		{
			result = "KillInfo_" + allWeaponNameList[index - 1];
		}
		return result;
	}

	// Token: 0x060012A9 RID: 4777 RVA: 0x000A722C File Offset: 0x000A562C
	public string FilterString(string orStr)
	{
		return WordFilter.mInstance.FilterString(orStr);
	}

	// Token: 0x060012AA RID: 4778 RVA: 0x000A7248 File Offset: 0x000A5648
	private string MyReplace(string strSource, string strRe, string strTo)
	{
		string text = strSource.ToLower();
		string value = strRe.ToLower();
		int num = text.IndexOf(value);
		if (num != -1)
		{
			strSource = strSource.Substring(0, num) + strTo + this.MyReplace(strSource.Substring(num + strRe.Length), strRe, strTo);
		}
		return strSource;
	}

	// Token: 0x060012AB RID: 4779 RVA: 0x000A7299 File Offset: 0x000A5699
	public void PrivateChatBtnPressed()
	{
		if (this.playerNameListNode.activeSelf)
		{
			this.HidePrivateNameListNode();
		}
		else
		{
			this.ShowPrivateNameListNode();
		}
	}

	// Token: 0x060012AC RID: 4780 RVA: 0x000A72BC File Offset: 0x000A56BC
	private void ShowPrivateNameListNode()
	{
		this.DestroyPrivateNameObjList();
		this.otherPlayerPropertiesList = GGNetworkKit.mInstance.GetOtherPlayerPropertiesList();
		if (this.otherPlayerPropertiesList.Count > 0)
		{
			for (int i = 0; i < this.otherPlayerPropertiesList.Count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.otherPlayerNamePrefab);
				gameObject.transform.parent = this.playerNameListNode.transform;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.transform.localPosition = new Vector3(-150f + (float)(165 * (i % 4)), -140f + (float)(70 * (i / 4)), 0f);
				gameObject.GetComponent<UIChatOtherPlayerNamePrefab>().index = i;
				gameObject.GetComponent<UIChatOtherPlayerNamePrefab>().label.text = this.otherPlayerPropertiesList[i].name;
				if (i == this.curPrivateChatIndex)
				{
					gameObject.GetComponent<UIChatOtherPlayerNamePrefab>().toggle.value = true;
				}
				this.otherPlayerNameObjList.Add(gameObject);
			}
			this.otherPlayerNameListBg.SetRect(-237f, -180f, 670f, 80f + (float)(70 * ((this.otherPlayerPropertiesList.Count - 1) / 4)));
			this.playerNameListNode.SetActive(true);
			this.playerNameListNode.transform.localPosition = new Vector3(0f, -1f * this.otherPlayerNameListBg.localSize.y, 0f);
			TweenPosition.Begin(this.playerNameListNode, 0.1f, new Vector3(0f, 0f, 0f));
			TweenAlpha.Begin(this.playerNameListNode, 0.3f, 1f, 0f);
		}
	}

	// Token: 0x060012AD RID: 4781 RVA: 0x000A748C File Offset: 0x000A588C
	public void HidePrivateNameListNode()
	{
		if (this.playerNameListNode.activeSelf)
		{
			TweenPosition.Begin(this.playerNameListNode, 0.1f, new Vector3(0f, -80f, 0f));
			TweenAlpha.Begin(this.playerNameListNode, 0.1f, 0f, 0f);
			base.StartCoroutine(this.OnFinishByPlayerNameListNodeHide(0.1f));
		}
	}

	// Token: 0x060012AE RID: 4782 RVA: 0x000A74FC File Offset: 0x000A58FC
	private IEnumerator OnFinishByPlayerNameListNodeHide(float time)
	{
		yield return new WaitForSeconds(time);
		this.playerNameListNode.SetActive(false);
		yield break;
	}

	// Token: 0x060012AF RID: 4783 RVA: 0x000A7520 File Offset: 0x000A5920
	private void DestroyPrivateNameObjList()
	{
		if (this.otherPlayerNameObjList.Count > 0)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < this.otherPlayerNameObjList.Count; i++)
			{
				list.Add(this.otherPlayerNameObjList[i]);
			}
			foreach (GameObject gameObject in list)
			{
				this.otherPlayerNameObjList.Remove(gameObject);
				UnityEngine.Object.DestroyImmediate(gameObject);
			}
			this.otherPlayerNameObjList.Clear();
		}
	}

	// Token: 0x060012B0 RID: 4784 RVA: 0x000A75D4 File Offset: 0x000A59D4
	public void ChatToAllToggleValueChanged()
	{
		if (this.chatToAllToggle.value)
		{
			this.chatObjType = UIChatSystemDirector.UIChatObjectType.All;
			this.HidePrivateNameListNode();
			this.privateChatBtn.isEnabled = false;
		}
	}

	// Token: 0x060012B1 RID: 4785 RVA: 0x000A75FF File Offset: 0x000A59FF
	public void ChatToTeamToggleValueChanged()
	{
		if (this.chatToTeamToggle.value)
		{
			this.chatObjType = UIChatSystemDirector.UIChatObjectType.Team;
			this.HidePrivateNameListNode();
			this.privateChatBtn.isEnabled = false;
		}
	}

	// Token: 0x060012B2 RID: 4786 RVA: 0x000A762A File Offset: 0x000A5A2A
	public void ChatToPrivateToggleValueChanged()
	{
		if (this.chatToPrivateToggle.value)
		{
			this.chatObjType = UIChatSystemDirector.UIChatObjectType.Private;
			this.ShowPrivateNameListNode();
			this.privateChatBtn.isEnabled = true;
		}
	}

	// Token: 0x0400154E RID: 5454
	public static UIChatSystemDirector mInstance;

	// Token: 0x0400154F RID: 5455
	public bool isFirstOpen = true;

	// Token: 0x04001550 RID: 5456
	public UILabel[] phraseLabel = new UILabel[8];

	// Token: 0x04001551 RID: 5457
	public UIInput[] phraseInput = new UIInput[8];

	// Token: 0x04001552 RID: 5458
	public UIToggle[] phraseToggle = new UIToggle[8];

	// Token: 0x04001553 RID: 5459
	public UILabel killMsgLabel;

	// Token: 0x04001554 RID: 5460
	public UILabel sysChatMsgLabel;

	// Token: 0x04001555 RID: 5461
	public UILabel chatMsgLabel;

	// Token: 0x04001556 RID: 5462
	private bool hasInitUI;

	// Token: 0x04001557 RID: 5463
	public GameObject chatNode;

	// Token: 0x04001558 RID: 5464
	public UITextList textList;

	// Token: 0x04001559 RID: 5465
	public UIInput chatInput;

	// Token: 0x0400155A RID: 5466
	private int curPhraseIndex;

	// Token: 0x0400155B RID: 5467
	private const float refreshCycleTime = 0.2f;

	// Token: 0x0400155C RID: 5468
	private float killInfoRefreshCycleTime = 0.2f;

	// Token: 0x0400155D RID: 5469
	private const float chatHistoryRefreshCycleTime = 0.2f;

	// Token: 0x0400155E RID: 5470
	private float deltaTime;

	// Token: 0x0400155F RID: 5471
	private float deltaTimeKillInfo;

	// Token: 0x04001560 RID: 5472
	private float deltaTimeChatHistory;

	// Token: 0x04001561 RID: 5473
	private const int killInfoNum = 6;

	// Token: 0x04001562 RID: 5474
	private const int sysChatNum = 2;

	// Token: 0x04001563 RID: 5475
	private List<float> killInfoLiveTimeList = new List<float>();

	// Token: 0x04001564 RID: 5476
	private List<float> sysChatMsgLiveTimeList = new List<float>();

	// Token: 0x04001565 RID: 5477
	private List<GGNetworkChatMessage> chatMessageList = new List<GGNetworkChatMessage>();

	// Token: 0x04001566 RID: 5478
	private List<GGNetworkSystemAndChatMessage> sysChatMessageList = new List<GGNetworkSystemAndChatMessage>();

	// Token: 0x04001567 RID: 5479
	private List<GGNetworkKillMessage> killMessageList = new List<GGNetworkKillMessage>();

	// Token: 0x04001568 RID: 5480
	private List<GGNetworkChatMessage> chatMessageShowList = new List<GGNetworkChatMessage>();

	// Token: 0x04001569 RID: 5481
	private List<GGNetworkSystemAndChatMessage> sysChatMessageShowList = new List<GGNetworkSystemAndChatMessage>();

	// Token: 0x0400156A RID: 5482
	public GGNetworkSystemAndChatMessage testMsg = new GGNetworkSystemAndChatMessage();

	// Token: 0x0400156B RID: 5483
	private List<GGNetworkKillMessage> killMessageShowList = new List<GGNetworkKillMessage>();

	// Token: 0x0400156C RID: 5484
	private GGNetworkChatMessage ggChatMessage = new GGNetworkChatMessage();

	// Token: 0x0400156D RID: 5485
	public UILabel[] killerLabel = new UILabel[6];

	// Token: 0x0400156E RID: 5486
	public UILabel[] deadLabel = new UILabel[6];

	// Token: 0x0400156F RID: 5487
	public UISprite[] weaponSprite = new UISprite[6];

	// Token: 0x04001570 RID: 5488
	public UISprite[] headshotSprite = new UISprite[6];

	// Token: 0x04001571 RID: 5489
	public List<GGNetworkPlayerProperties> otherPlayerPropertiesList = new List<GGNetworkPlayerProperties>();

	// Token: 0x04001572 RID: 5490
	public UIToggle chatToAllToggle;

	// Token: 0x04001573 RID: 5491
	public UIToggle chatToTeamToggle;

	// Token: 0x04001574 RID: 5492
	public UIToggle chatToPrivateToggle;

	// Token: 0x04001575 RID: 5493
	public UIButton privateChatBtn;

	// Token: 0x04001576 RID: 5494
	public GameObject playerNameListNode;

	// Token: 0x04001577 RID: 5495
	public GameObject otherPlayerNamePrefab;

	// Token: 0x04001578 RID: 5496
	public List<GameObject> otherPlayerNameObjList = new List<GameObject>();

	// Token: 0x04001579 RID: 5497
	public UISprite otherPlayerNameListBg;

	// Token: 0x0400157A RID: 5498
	public int curPrivateChatIndex;

	// Token: 0x0400157B RID: 5499
	private UIChatSystemDirector.UIChatObjectType chatObjType;

	// Token: 0x0200028F RID: 655
	private enum UIChatObjectType
	{
		// Token: 0x0400157D RID: 5501
		All,
		// Token: 0x0400157E RID: 5502
		Team,
		// Token: 0x0400157F RID: 5503
		Private
	}
}
