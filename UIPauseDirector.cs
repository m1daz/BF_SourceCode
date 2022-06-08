using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000297 RID: 663
public class UIPauseDirector : MonoBehaviour
{
	// Token: 0x14000005 RID: 5
	// (add) Token: 0x0600131C RID: 4892 RVA: 0x000ABD64 File Offset: 0x000AA164
	// (remove) Token: 0x0600131D RID: 4893 RVA: 0x000ABD98 File Offset: 0x000AA198
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPauseDirector.PauseStartEventHandler OnPauseStart;

	// Token: 0x0600131E RID: 4894 RVA: 0x000ABDCC File Offset: 0x000AA1CC
	public void GenPauseStartEvent()
	{
		if (UIPauseDirector.OnPauseStart != null)
		{
			UIPauseDirector.OnPauseStart();
		}
	}

	// Token: 0x0600131F RID: 4895 RVA: 0x000ABDE2 File Offset: 0x000AA1E2
	private void Awake()
	{
		if (UIPauseDirector.mInstance == null)
		{
			UIPauseDirector.mInstance = this;
		}
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x000ABDFA File Offset: 0x000AA1FA
	private void OnDestroy()
	{
		if (UIPauseDirector.mInstance != null)
		{
			UIPauseDirector.mInstance = null;
		}
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x000ABE12 File Offset: 0x000AA212
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x000ABE1A File Offset: 0x000AA21A
	private void Update()
	{
		this.CountdownTimer();
		this.QuitCountdownTimer();
		this.PlayerInfoListRefreshTimer();
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x000ABE30 File Offset: 0x000AA230
	public void Init()
	{
		this.quitCountdownLabel.text = "(60)";
		this.resultNode.SetActive(false);
		this.startBtn.isEnabled = false;
		this.roomNameLabel.text = GGNetworkKit.mInstance.GetRoomName().ToUpper();
		this.bluePlayerNumLabel.text = string.Empty;
		this.redPlayerNumLabel.text = string.Empty;
		this.blueResultPlayerNumLabel.text = string.Empty;
		this.redResultPlayerNumLabel.text = string.Empty;
		this.InitCountdownNode();
		if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Sport)
		{
			UIPlayDirector.mInstance.buffToggle.gameObject.SetActive(false);
			UIPlayDirector.mInstance.buffBarNode.SetActive(false);
		}
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x000ABEFC File Offset: 0x000AA2FC
	public void ResumeBtnPressed()
	{
		if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Sport)
		{
			this.blueTeamBtn.gameObject.SetActive(false);
			this.redTeamBtn.gameObject.SetActive(false);
			UIPlayDirector.mInstance.buffToggle.gameObject.SetActive(false);
			UIPlayDirector.mInstance.buffBarNode.SetActive(false);
		}
		this.lobbyToggle.value = true;
		this.settingToggle.value = false;
		this.pauseNode.SetActive(false);
		this.isCutControl = false;
		UISettingNewDirector.mInstance.BackBtnPressed();
	}

	// Token: 0x06001325 RID: 4901 RVA: 0x000ABF94 File Offset: 0x000AA394
	public void NewGameBtnPressed()
	{
		UISceneManager.mInstance.LoadLevel("UILobby");
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x000ABFA8 File Offset: 0x000AA3A8
	public void ShowLobbyNode()
	{
		this.isCutControl = true;
		if (this.startBtn.gameObject.activeSelf)
		{
			this.startBtn.isEnabled = true;
		}
		this.DestroyPlayerInfoPrefabList();
		GGTeamType ggteamType;
		if (GameObject.FindWithTag("Player") == null)
		{
			ggteamType = ((UnityEngine.Random.Range(0, 1001) <= 500) ? GGTeamType.red : GGTeamType.blue);
		}
		else
		{
			ggteamType = GGNetworkKit.mInstance.GetManagePlayerProperties().GetMainPlayerProperty().team;
		}
		if (ggteamType == GGTeamType.red)
		{
			this.isBlueTeam = false;
			this.blueTeamSelectLogo.gameObject.SetActive(false);
			this.redTeamSelectLogo.gameObject.SetActive(true);
			UIPlayDirector.mInstance.bloodBg.color = new Color(1f, 0.26666668f, 0.26666668f, 1f);
			UIPlayDirector.mInstance.armorBg.color = new Color(1f, 0.26666668f, 0.26666668f, 1f);
		}
		else
		{
			this.isBlueTeam = true;
			this.blueTeamSelectLogo.gameObject.SetActive(true);
			this.redTeamSelectLogo.gameObject.SetActive(false);
			UIPlayDirector.mInstance.bloodBg.color = new Color(0.37254903f, 0.6117647f, 0.9764706f, 1f);
			UIPlayDirector.mInstance.armorBg.color = new Color(0.37254903f, 0.6117647f, 0.9764706f, 1f);
		}
		this.bluePlayerPropertiesList = GGNetworkKit.mInstance.GetBluePlayerPropertiesList();
		this.redPlayerPropertiesList = GGNetworkKit.mInstance.GetRedPlayerPropertiesList();
		this.bluePlayerNum = this.bluePlayerPropertiesList.Count;
		this.redPlayerNum = this.redPlayerPropertiesList.Count;
		int num = this.bluePlayerNum + this.redPlayerNum;
		this.roomNameLabel.text = string.Concat(new string[]
		{
			GGNetworkKit.mInstance.GetRoomName().ToUpper(),
			" (",
			num.ToString(),
			"/",
			GGNetworkKit.mInstance.GetMaxPlayers().ToString().ToUpper(),
			")"
		});
		this.bluePlayerNumLabel.text = this.bluePlayerNum.ToString();
		this.redPlayerNumLabel.text = this.redPlayerNum.ToString();
		this.RefreshPlayerInfoList();
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x000AC22D File Offset: 0x000AA62D
	public void SendAddRequest(string roleName)
	{
		GGCloudServiceKit.mInstance.GetUserNameByRoleNameAsyForAddFriendInGame(roleName);
		if (GGNetworkChatSys.mInstance != null)
		{
			GGNetworkChatSys.mInstance.SendFriendRequestMessage(roleName);
		}
		GGCloudServiceKit.mInstance.mHadSendFriendRequestRoleName.Add(roleName);
	}

	// Token: 0x06001328 RID: 4904 RVA: 0x000AC268 File Offset: 0x000AA668
	public void LobbyToggleValueChanged()
	{
		if (this.lobbyToggle.value)
		{
			if (!this.lobbyNode.activeSelf)
			{
				this.lobbyNode.SetActive(true);
			}
			if (this.settingNode.activeSelf)
			{
				this.settingNode.SetActive(false);
			}
		}
	}

	// Token: 0x06001329 RID: 4905 RVA: 0x000AC2C0 File Offset: 0x000AA6C0
	public void SettingToggleValueChanged()
	{
		if (this.settingToggle.value)
		{
			if (!this.settingNode.activeSelf)
			{
				this.settingNode.SetActive(true);
			}
			if (this.lobbyNode.activeSelf)
			{
				this.lobbyNode.SetActive(false);
			}
		}
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x000AC318 File Offset: 0x000AA718
	public void BlueTeamBtnPressed()
	{
		if (!this.isBlueTeam && GGNetworkKit.mInstance.GetManagePlayerProperties().GetMainPlayerProperty().team == GGTeamType.red)
		{
			bool flag = GGNetworkKit.mInstance.SwitchPlayerTeam();
			if (flag)
			{
				this.ShowLobbyNode();
			}
			else
			{
				this.changeTeamErrorLabel.text = "Too Many Blue Team Members!";
				base.StartCoroutine(this.HideChangeTeamErrorTip());
			}
		}
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x000AC384 File Offset: 0x000AA784
	public void RedTeamBtnPressed()
	{
		if (this.isBlueTeam && GGNetworkKit.mInstance.GetManagePlayerProperties().GetMainPlayerProperty().team == GGTeamType.blue)
		{
			bool flag = GGNetworkKit.mInstance.SwitchPlayerTeam();
			if (flag)
			{
				this.ShowLobbyNode();
			}
			else
			{
				this.changeTeamErrorLabel.text = "Too Many Red Team Members!";
				base.StartCoroutine(this.HideChangeTeamErrorTip());
			}
		}
	}

	// Token: 0x0600132C RID: 4908 RVA: 0x000AC3F0 File Offset: 0x000AA7F0
	private IEnumerator HideChangeTeamErrorTip()
	{
		yield return new WaitForSeconds(1.2f);
		this.changeTeamErrorLabel.text = string.Empty;
		yield break;
	}

	// Token: 0x0600132D RID: 4909 RVA: 0x000AC40C File Offset: 0x000AA80C
	public void NewRoundBtnPressed()
	{
		this.startQuitCountdown = false;
		this.quitCountdownValue = 60;
		this.quitCountdownLabel.text = "(60)";
		this.resultNode.SetActive(false);
		this.pauseNode.SetActive(true);
		UIPlayDirector.mInstance.PauseBtnPressed();
		if (UIModeDirector.mInstance.modeType == GGModeType.Mutation || UIModeDirector.mInstance.modeType == GGModeType.Hunting)
		{
			this.blueTeamBtn.gameObject.SetActive(false);
			this.redTeamBtn.gameObject.SetActive(false);
		}
		else
		{
			this.blueTeamBtn.gameObject.SetActive(true);
			this.redTeamBtn.gameObject.SetActive(true);
		}
		this.newGameBtn.gameObject.SetActive(false);
		this.resumeBtn.gameObject.SetActive(false);
		this.startBtn.gameObject.SetActive(true);
		this.startGameBgTexture.enabled = true;
		this.DestroyPlayerInfoResultPrefabList();
		GGNetworkKit.mInstance.NewRound();
		UIPlayDirector.mInstance.Reset();
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x000AC51C File Offset: 0x000AA91C
	public void StartBtnPressed()
	{
		if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Sport)
		{
			this.blueTeamBtn.gameObject.SetActive(false);
			this.redTeamBtn.gameObject.SetActive(false);
			UIPlayDirector.mInstance.buffToggle.gameObject.SetActive(false);
			UIPlayDirector.mInstance.buffBarNode.SetActive(false);
		}
		this.startBtn.gameObject.SetActive(false);
		this.startGameBgTexture.enabled = false;
		this.resumeBtn.gameObject.SetActive(true);
		this.newGameBtn.gameObject.SetActive(true);
		this.lobbyToggle.value = true;
		this.settingToggle.value = false;
		this.pauseNode.SetActive(false);
		this.isCutControl = false;
		UISettingNewDirector.mInstance.BackBtnPressed();
		this.GenPauseStartEvent();
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x000AC5FC File Offset: 0x000AA9FC
	private void DestroyPlayerInfoPrefabList()
	{
		this.tempList.Clear();
		for (int i = 0; i < this.bluePlayerInfoList.Count; i++)
		{
			this.tempList.Add(this.bluePlayerInfoList[i]);
		}
		foreach (GameObject gameObject in this.tempList)
		{
			this.bluePlayerInfoList.Remove(gameObject);
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
		this.tempList.Clear();
		for (int j = 0; j < this.redPlayerInfoList.Count; j++)
		{
			this.tempList.Add(this.redPlayerInfoList[j]);
		}
		foreach (GameObject gameObject2 in this.tempList)
		{
			this.redPlayerInfoList.Remove(gameObject2);
			UnityEngine.Object.DestroyImmediate(gameObject2);
		}
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x000AC73C File Offset: 0x000AAB3C
	private void DestroyPlayerInfoResultPrefabList()
	{
		this.tempList.Clear();
		for (int i = 0; i < this.playerResultInfoList.Count; i++)
		{
			this.tempList.Add(this.playerResultInfoList[i]);
		}
		foreach (GameObject gameObject in this.tempList)
		{
			this.playerResultInfoList.Remove(gameObject);
			UnityEngine.Object.DestroyImmediate(gameObject);
		}
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x000AC7E4 File Offset: 0x000AABE4
	public void PushResultPanel()
	{
		if (this.resultNode.activeSelf)
		{
			return;
		}
		UIPlayDirector.mInstance.chatNode.SetActive(false);
		UIPlayDirector.mInstance.storeNode.SetActive(false);
		this.isCutControl = true;
		if (this.pauseNode.activeSelf)
		{
			UIOtherPlayerInfoDirector.mInstance.CloseBtnPressed();
			this.pauseNode.SetActive(false);
		}
		base.GetComponent<AudioSource>().PlayOneShot(this.clips[0]);
		this.resultNode.SetActive(true);
		this.resultExceptBgNode.GetComponent<TweenScale>().ResetToBeginning();
		this.resultExceptBgNode.GetComponent<TweenScale>().Play();
		this.newRoundBtn.isEnabled = false;
		this.quitBtn.isEnabled = false;
		base.StartCoroutine(this.EnableQuitBtnCoroutine(20f));
		this.DestroyPlayerInfoResultPrefabList();
		this.blueResultList = GGNetworkKit.mInstance.GetResultBlueRankInfoList();
		this.redResultList = GGNetworkKit.mInstance.GetResultRedRankInfoList();
		this.bluePlayerNum = this.blueResultList.Count;
		this.redPlayerNum = this.redResultList.Count;
		if (UIModeDirector.mInstance.modeType == GGModeType.Mutation)
		{
			this.blueWinnerLabel.gameObject.SetActive(false);
			this.redWinnerLabel.gameObject.SetActive(false);
		}
		else if (this.blueResultList.Count > 0)
		{
			if (this.blueResultList[0].isWinner)
			{
				this.blueWinnerLabel.gameObject.SetActive(true);
				this.redWinnerLabel.gameObject.SetActive(false);
			}
			else
			{
				this.blueWinnerLabel.gameObject.SetActive(false);
				this.redWinnerLabel.gameObject.SetActive(true);
			}
		}
		this.RefreshResultPlayerList();
		if (UIModeDirector.mInstance.resultSportTopNode.activeSelf)
		{
			this.selfInfoToggle.value = true;
			this.mvpInfoToggle.value = false;
			this.DetailToggleValueChanged();
		}
		this.blueResultPlayerNumLabel.text = this.bluePlayerNum.ToString();
		this.redResultPlayerNumLabel.text = this.redPlayerNum.ToString();
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x000ACA18 File Offset: 0x000AAE18
	private void RefreshSportModeInfo(GGNetworkPlayerProperties playerInfo)
	{
		if (playerInfo != null)
		{
			this.sportRankSprite.spriteName = "Rank_" + playerInfo.rank.ToString();
			this.sportRankLabel.text = "- " + playerInfo.rank.ToString() + " -";
			this.sportNickNameLabel.text = playerInfo.name;
			if (UIModeDirector.mInstance.modeType == GGModeType.StrongHold)
			{
				this.sportOccupyLabel.text = playerInfo.strongholdGetNum.ToString();
			}
			else if (UIModeDirector.mInstance.modeType == GGModeType.Explosion)
			{
				this.sportOccupyLabel.text = playerInfo.installbombNum.ToString() + "/" + playerInfo.unInstallbombNum.ToString();
			}
			else
			{
				this.sportOccupyLabel.text = string.Empty;
			}
			this.sportCoinLabel.text = "+" + playerInfo.coinAdd.ToString();
			this.sportHonorLabel.text = "+" + playerInfo.honorPointAdd.ToString();
			this.sportExpLabel.text = "Exp + " + playerInfo.expAdd.ToString();
			this.sportSeasonScoreLabel.text = ((playerInfo.seasonScoreAdd >= 0) ? (" + " + playerInfo.seasonScoreAdd.ToString()) : (" - " + Math.Abs(playerInfo.seasonScoreAdd).ToString()));
			this.sportKillDeadLabel.text = playerInfo.killNum.ToString() + " / " + playerInfo.deadNum.ToString();
			this.sportHeadshotLabel.text = playerInfo.headshotNum.ToString();
			this.sportContinuousKillLabel.text = playerInfo.maxKillNum.ToString();
			this.sportParticipationLabel.text = playerInfo.participation.ToString();
		}
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x000ACCA6 File Offset: 0x000AB0A6
	public void DetailToggleValueChanged()
	{
		if (this.selfInfoToggle.value)
		{
			this.sportResultTopBg1.color = this.bgColor1;
			this.sportResultTopBg2.color = this.bgColor1;
			this.RefreshSportModeInfo(this.selfPlayerProperties);
		}
	}

	// Token: 0x06001334 RID: 4916 RVA: 0x000ACCE6 File Offset: 0x000AB0E6
	public void MvpDetailToggleValueChanged()
	{
		if (this.mvpInfoToggle.value)
		{
			this.sportResultTopBg1.color = this.bgColor2;
			this.sportResultTopBg2.color = this.bgColor2;
			this.RefreshSportModeInfo(this.mvpPlayerProperties);
		}
	}

	// Token: 0x06001335 RID: 4917 RVA: 0x000ACD28 File Offset: 0x000AB128
	private void RefreshPlayerInfoList()
	{
		List<string> mAllFriendRoleNameList = GGCloudServiceKit.mInstance.mAllFriendRoleNameList;
		for (int i = 0; i < this.bluePlayerPropertiesList.Count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.playerInfoPrefab);
			gameObject.transform.parent = this.blueScrollView.transform;
			gameObject.transform.localPosition = new Vector3(-152f, 60f - (float)i * 60f, 0f);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			bool isFriend = mAllFriendRoleNameList.Contains(this.bluePlayerPropertiesList[i].name) || GGCloudServiceKit.mInstance.mHadSendFriendRequestRoleName.Contains(this.bluePlayerPropertiesList[i].name);
			gameObject.GetComponent<UIPlayerInfoListPrefab>().SetInfoValue(i, this.myBlueColor, this.bluePlayerPropertiesList[i], isFriend);
			this.bluePlayerInfoList.Add(gameObject);
		}
		for (int j = 0; j < this.redPlayerPropertiesList.Count; j++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.playerInfoPrefab);
			gameObject2.transform.parent = this.redScrollView.transform;
			gameObject2.transform.localPosition = new Vector3(-152f, 60f - (float)j * 60f, 0f);
			gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
			bool isFriend2 = mAllFriendRoleNameList.Contains(this.redPlayerPropertiesList[j].name) || GGCloudServiceKit.mInstance.mHadSendFriendRequestRoleName.Contains(this.redPlayerPropertiesList[j].name);
			gameObject2.GetComponent<UIPlayerInfoListPrefab>().SetInfoValue(j, this.myRedColor, this.redPlayerPropertiesList[j], isFriend2);
			this.redPlayerInfoList.Add(gameObject2);
		}
	}

	// Token: 0x06001336 RID: 4918 RVA: 0x000ACF5C File Offset: 0x000AB35C
	private void PlayerInfoListRefreshTimer()
	{
		if (this.pauseNode.activeSelf)
		{
			this.playerInfoListRefreshTime += Time.deltaTime;
			if (this.playerInfoListRefreshTime > 0.2f)
			{
				this.playerInfoListRefreshTime = 0f;
				this.bluePlayerPropertiesList = GGNetworkKit.mInstance.GetBluePlayerPropertiesList();
				this.redPlayerPropertiesList = GGNetworkKit.mInstance.GetRedPlayerPropertiesList();
				this.bluePlayerNum = this.bluePlayerPropertiesList.Count;
				this.redPlayerNum = this.redPlayerPropertiesList.Count;
				int num = this.bluePlayerNum + this.redPlayerNum;
				this.roomNameLabel.text = string.Concat(new string[]
				{
					GGNetworkKit.mInstance.GetRoomName().ToUpper(),
					" (",
					num.ToString(),
					"/",
					GGNetworkKit.mInstance.GetMaxPlayers().ToString().ToUpper(),
					")"
				});
				this.bluePlayerNumLabel.text = this.bluePlayerNum.ToString();
				this.redPlayerNumLabel.text = this.redPlayerNum.ToString();
				List<string> mAllFriendRoleNameList = GGCloudServiceKit.mInstance.mAllFriendRoleNameList;
				if (this.bluePlayerNum < this.bluePlayerInfoList.Count)
				{
					this.tempList.Clear();
					for (int i = this.bluePlayerNum; i < this.bluePlayerInfoList.Count; i++)
					{
						this.tempList.Add(this.bluePlayerInfoList[i]);
					}
					foreach (GameObject gameObject in this.tempList)
					{
						this.bluePlayerInfoList.Remove(gameObject);
						UnityEngine.Object.DestroyImmediate(gameObject);
					}
					if (this.bluePlayerInfoList.Count > 0)
					{
						for (int j = 0; j < this.bluePlayerInfoList.Count; j++)
						{
							bool isFriend = mAllFriendRoleNameList.Contains(this.bluePlayerPropertiesList[j].name) || GGCloudServiceKit.mInstance.mHadSendFriendRequestRoleName.Contains(this.bluePlayerPropertiesList[j].name);
							this.bluePlayerInfoList[j].GetComponent<UIPlayerInfoListPrefab>().SetInfoValue(j, this.myBlueColor, this.bluePlayerPropertiesList[j], isFriend);
						}
					}
				}
				else
				{
					for (int k = this.bluePlayerInfoList.Count; k < this.bluePlayerNum; k++)
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.playerInfoPrefab);
						gameObject2.transform.parent = this.blueScrollView.transform;
						gameObject2.transform.localPosition = new Vector3(-152f, 60f - (float)k * 60f, 0f);
						gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
						this.bluePlayerInfoList.Add(gameObject2);
					}
					for (int l = 0; l < this.bluePlayerNum; l++)
					{
						bool isFriend2 = mAllFriendRoleNameList.Contains(this.bluePlayerPropertiesList[l].name) || GGCloudServiceKit.mInstance.mHadSendFriendRequestRoleName.Contains(this.bluePlayerPropertiesList[l].name);
						this.bluePlayerInfoList[l].GetComponent<UIPlayerInfoListPrefab>().SetInfoValue(l, this.myBlueColor, this.bluePlayerPropertiesList[l], isFriend2);
					}
				}
				if (this.redPlayerNum < this.redPlayerInfoList.Count)
				{
					this.tempList.Clear();
					for (int m = this.redPlayerNum; m < this.redPlayerInfoList.Count; m++)
					{
						this.tempList.Add(this.redPlayerInfoList[m]);
					}
					foreach (GameObject gameObject3 in this.tempList)
					{
						this.redPlayerInfoList.Remove(gameObject3);
						UnityEngine.Object.DestroyImmediate(gameObject3);
					}
					if (this.redPlayerInfoList.Count > 0)
					{
						for (int n = 0; n < this.redPlayerInfoList.Count; n++)
						{
							bool isFriend3 = mAllFriendRoleNameList.Contains(this.redPlayerPropertiesList[n].name) || GGCloudServiceKit.mInstance.mHadSendFriendRequestRoleName.Contains(this.redPlayerPropertiesList[n].name);
							this.redPlayerInfoList[n].GetComponent<UIPlayerInfoListPrefab>().SetInfoValue(n, this.myRedColor, this.redPlayerPropertiesList[n], isFriend3);
						}
					}
				}
				else
				{
					for (int num2 = this.redPlayerInfoList.Count; num2 < this.redPlayerNum; num2++)
					{
						GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.playerInfoPrefab);
						gameObject4.transform.parent = this.redScrollView.transform;
						gameObject4.transform.localPosition = new Vector3(-152f, 60f - (float)num2 * 60f, 0f);
						gameObject4.transform.localScale = new Vector3(1f, 1f, 1f);
						this.redPlayerInfoList.Add(gameObject4);
					}
					for (int num3 = 0; num3 < this.redPlayerNum; num3++)
					{
						bool isFriend4 = mAllFriendRoleNameList.Contains(this.redPlayerPropertiesList[num3].name) || GGCloudServiceKit.mInstance.mHadSendFriendRequestRoleName.Contains(this.redPlayerPropertiesList[num3].name);
						this.redPlayerInfoList[num3].GetComponent<UIPlayerInfoListPrefab>().SetInfoValue(num3, this.myRedColor, this.redPlayerPropertiesList[num3], isFriend4);
					}
				}
			}
		}
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x000AD618 File Offset: 0x000ABA18
	private void RefreshResultPlayerList()
	{
		for (int i = 0; i < this.blueResultList.Count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.playerInfoResultPrefab);
			gameObject.transform.parent = this.blueResultScrollView.transform;
			gameObject.transform.localPosition = new Vector3(-152f, 60f - (float)i * 60f, 0f);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.GetComponent<UIPlayerResultInfoListPrefab>().SetInfoValue(i, this.myBlueColor, this.blueResultList[i]);
			this.playerResultInfoList.Add(gameObject);
			if (this.blueResultList[i].name == UIUserDataController.GetDefaultRoleName())
			{
				this.selfPlayerProperties = this.blueResultList[i];
				if (this.blueResultList[i].isWinner)
				{
					this.mvpPlayerProperties = this.blueResultList[0];
				}
				else
				{
					this.mvpPlayerProperties = this.redResultList[0];
				}
			}
		}
		for (int j = 0; j < this.redResultList.Count; j++)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.playerInfoResultPrefab);
			gameObject2.transform.parent = this.redResultScrollView.transform;
			gameObject2.transform.localPosition = new Vector3(-152f, 60f - (float)j * 60f, 0f);
			gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject2.GetComponent<UIPlayerResultInfoListPrefab>().SetInfoValue(j, this.myRedColor, this.redResultList[j]);
			this.playerResultInfoList.Add(gameObject2);
			if (this.redResultList[j].name == UIUserDataController.GetDefaultRoleName())
			{
				this.selfPlayerProperties = this.redResultList[j];
				if (this.redResultList[j].isWinner)
				{
					this.mvpPlayerProperties = this.redResultList[0];
				}
				else
				{
					this.mvpPlayerProperties = this.blueResultList[0];
				}
			}
		}
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x000AD868 File Offset: 0x000ABC68
	private IEnumerator EnableQuitBtnCoroutine(float time)
	{
		yield return new WaitForSeconds(time);
		this.EnableQuitBtn();
		yield break;
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x000AD88A File Offset: 0x000ABC8A
	private void EnableQuitBtn()
	{
		if (!this.quitBtn.isEnabled)
		{
			this.quitBtn.isEnabled = true;
		}
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x000AD8A8 File Offset: 0x000ABCA8
	public void EnableNewRoundBtn()
	{
		if (!this.newRoundBtn.isEnabled)
		{
			this.newRoundBtn.isEnabled = true;
		}
		this.EnableQuitBtn();
		this.startQuitCountdown = true;
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x000AD8D4 File Offset: 0x000ABCD4
	private void QuitCountdownTimer()
	{
		if (this.startQuitCountdown)
		{
			this.quitCountdownTime += Time.deltaTime;
			if (this.quitCountdownTime > 1f && this.quitCountdownValue > 0)
			{
				this.quitCountdownTime = 0f;
				this.quitCountdownValue--;
				this.quitCountdownLabel.text = "(" + (this.quitCountdownValue - 1).ToString() + ")";
			}
		}
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x000AD964 File Offset: 0x000ABD64
	public void PushDisconnectPanel()
	{
		EventDelegate btnEventName = new EventDelegate(this, "RejoinRoomBtnPressed");
		EventDelegate btnEventName2 = new EventDelegate(this, "QuitBtnPressed");
		UITipController.mInstance.SetTipData(UITipController.TipType.TwoButtonBlackBgTip, "Disconnected!", Color.white, "Rejoin", "Quit", btnEventName, btnEventName2, null);
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x000AD9AB File Offset: 0x000ABDAB
	private void QuitBtnPressed()
	{
		Application.LoadLevel("UILobby");
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x000AD9B7 File Offset: 0x000ABDB7
	private void RejoinRoomBtnPressed()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.goSwitchSceneInfo, new Vector3(0f, 0f, 0f), Quaternion.identity);
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x000AD9DE File Offset: 0x000ABDDE
	private void InitCountdownNode()
	{
		this.countdownNode.SetActive(false);
		this.lastDateTime = DateTime.Now;
	}

	// Token: 0x06001340 RID: 4928 RVA: 0x000AD9F8 File Offset: 0x000ABDF8
	private void CountdownAfterDied()
	{
		if (!this.isCountdownPanelVisible)
		{
			this.isCountdownPanelVisible = true;
			this.countdownNode.SetActive(true);
			this.lastDateTime = DateTime.Now;
		}
		DateTime now = DateTime.Now;
		TimeSpan ts = new TimeSpan(this.lastDateTime.Ticks);
		TimeSpan timeSpan = new TimeSpan(now.Ticks);
		TimeSpan timeSpan2 = timeSpan.Subtract(ts).Duration();
		if (timeSpan2.Seconds >= 5 && timeSpan2.TotalSeconds > 5.199999809265137)
		{
			this.DisvisibleCountdownPanel();
			return;
		}
		if (timeSpan2.Seconds == 0 && !this.isFive)
		{
			this.countdownLabel.text = "5";
			this.isFive = true;
			this.countdownLabel.GetComponent<TweenScale>().ResetToBeginning();
			this.countdownLabel.GetComponent<TweenScale>().Play();
		}
		else if (timeSpan2.Seconds == 1 && !this.isFour)
		{
			this.countdownLabel.text = "4";
			this.isFour = true;
			this.countdownLabel.GetComponent<TweenScale>().ResetToBeginning();
			this.countdownLabel.GetComponent<TweenScale>().Play();
		}
		else if (timeSpan2.Seconds == 2 && !this.isThree)
		{
			this.countdownLabel.text = "3";
			this.isThree = true;
			this.countdownLabel.GetComponent<TweenScale>().ResetToBeginning();
			this.countdownLabel.GetComponent<TweenScale>().Play();
		}
		else if (timeSpan2.Seconds == 3 && !this.isTwo)
		{
			this.countdownLabel.text = "2";
			this.isTwo = true;
			this.countdownLabel.GetComponent<TweenScale>().ResetToBeginning();
			this.countdownLabel.GetComponent<TweenScale>().Play();
		}
		else if (timeSpan2.Seconds == 4 && !this.isOne)
		{
			this.countdownLabel.text = "1";
			this.isOne = true;
			this.countdownLabel.GetComponent<TweenScale>().ResetToBeginning();
			this.countdownLabel.GetComponent<TweenScale>().Play();
		}
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x000ADC2D File Offset: 0x000AC02D
	private void DisvisibleCountdownPanel()
	{
		this.countdownNode.SetActive(false);
		this.isCountdownPanelVisible = false;
		this.isOne = false;
		this.isTwo = false;
		this.isThree = false;
		this.isFour = false;
		this.isFive = false;
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x000ADC65 File Offset: 0x000AC065
	private void CountdownTimer()
	{
		if (UIModeDirector.mInstance.modeType == GGModeType.Explosion || UIModeDirector.mInstance.modeType == GGModeType.Hunting)
		{
			return;
		}
		if (this.isDead || this.isCountdownPanelVisible)
		{
			this.CountdownAfterDied();
		}
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x000ADCA4 File Offset: 0x000AC0A4
	public void AddFriendBtnPressedCoroutine(string friendusername)
	{
		object obj = this.mLockAddFriendInGameRequest;
		lock (obj)
		{
			GGCloudServiceKit.mInstance.AddFriendRequest(UIUserDataController.GetDefaultUserName(), friendusername, UIUserDataController.GetDefaultRoleName());
		}
	}

	// Token: 0x04001646 RID: 5702
	public static UIPauseDirector mInstance;

	// Token: 0x04001647 RID: 5703
	public GameObject pauseNode;

	// Token: 0x04001648 RID: 5704
	public AudioClip[] clips;

	// Token: 0x04001649 RID: 5705
	public GameObject joystickNode;

	// Token: 0x0400164A RID: 5706
	public GameObject resultNode;

	// Token: 0x0400164B RID: 5707
	private Color myBlueColor = new Color(0f, 0.3254902f, 0.8235294f, 1f);

	// Token: 0x0400164C RID: 5708
	private Color myRedColor = new Color(0.77254903f, 0f, 0f, 1f);

	// Token: 0x0400164D RID: 5709
	public Color myGrayColor = new Color(0.3372549f, 0.3372549f, 0.3372549f, 1f);

	// Token: 0x0400164E RID: 5710
	private Color bgColor1 = new Color(0.36078432f, 0.36078432f, 0.36078432f, 1f);

	// Token: 0x0400164F RID: 5711
	private Color bgColor2 = new Color(0.7137255f, 0.4f, 0f, 1f);

	// Token: 0x04001650 RID: 5712
	public UIScrollView blueScrollView;

	// Token: 0x04001651 RID: 5713
	public UIScrollView redScrollView;

	// Token: 0x04001652 RID: 5714
	public UILabel roomNameLabel;

	// Token: 0x04001653 RID: 5715
	public UILabel bluePlayerNumLabel;

	// Token: 0x04001654 RID: 5716
	public UILabel redPlayerNumLabel;

	// Token: 0x04001655 RID: 5717
	public GameObject playerInfoPrefab;

	// Token: 0x04001656 RID: 5718
	private List<GameObject> bluePlayerInfoList = new List<GameObject>();

	// Token: 0x04001657 RID: 5719
	private List<GameObject> redPlayerInfoList = new List<GameObject>();

	// Token: 0x04001658 RID: 5720
	private List<GameObject> tempList = new List<GameObject>();

	// Token: 0x04001659 RID: 5721
	private int bluePlayerNum;

	// Token: 0x0400165A RID: 5722
	private int redPlayerNum;

	// Token: 0x0400165B RID: 5723
	public UIButton startBtn;

	// Token: 0x0400165C RID: 5724
	public UIButton resumeBtn;

	// Token: 0x0400165D RID: 5725
	public UIButton newGameBtn;

	// Token: 0x0400165E RID: 5726
	public UILabel changeTeamErrorLabel;

	// Token: 0x0400165F RID: 5727
	public UITexture startGameBgTexture;

	// Token: 0x04001660 RID: 5728
	public GameObject resultExceptBgNode;

	// Token: 0x04001661 RID: 5729
	public UIScrollView blueResultScrollView;

	// Token: 0x04001662 RID: 5730
	public UIScrollView redResultScrollView;

	// Token: 0x04001663 RID: 5731
	public UIButton newRoundBtn;

	// Token: 0x04001664 RID: 5732
	public UIButton quitBtn;

	// Token: 0x04001665 RID: 5733
	public UILabel quitCountdownLabel;

	// Token: 0x04001666 RID: 5734
	private float quitCountdownTime;

	// Token: 0x04001667 RID: 5735
	private int quitCountdownValue = 60;

	// Token: 0x04001668 RID: 5736
	private bool startQuitCountdown;

	// Token: 0x04001669 RID: 5737
	public GameObject playerInfoResultPrefab;

	// Token: 0x0400166A RID: 5738
	private List<GameObject> playerResultInfoList = new List<GameObject>();

	// Token: 0x0400166B RID: 5739
	private List<GGNetworkPlayerProperties> bluePlayerPropertiesList = new List<GGNetworkPlayerProperties>();

	// Token: 0x0400166C RID: 5740
	private List<GGNetworkPlayerProperties> redPlayerPropertiesList = new List<GGNetworkPlayerProperties>();

	// Token: 0x0400166D RID: 5741
	private List<GGNetworkPlayerProperties> blueResultList = new List<GGNetworkPlayerProperties>();

	// Token: 0x0400166E RID: 5742
	private List<GGNetworkPlayerProperties> redResultList = new List<GGNetworkPlayerProperties>();

	// Token: 0x0400166F RID: 5743
	private GGNetworkPlayerProperties selfPlayerProperties;

	// Token: 0x04001670 RID: 5744
	private GGNetworkPlayerProperties mvpPlayerProperties;

	// Token: 0x04001671 RID: 5745
	public UILabel blueWinnerLabel;

	// Token: 0x04001672 RID: 5746
	public UILabel redWinnerLabel;

	// Token: 0x04001673 RID: 5747
	public UILabel blueResultPlayerNumLabel;

	// Token: 0x04001674 RID: 5748
	public UILabel redResultPlayerNumLabel;

	// Token: 0x04001675 RID: 5749
	public GameObject goSwitchSceneInfo;

	// Token: 0x04001676 RID: 5750
	public bool isCutControl = true;

	// Token: 0x04001678 RID: 5752
	public UIToggle settingToggle;

	// Token: 0x04001679 RID: 5753
	public UIToggle lobbyToggle;

	// Token: 0x0400167A RID: 5754
	public GameObject lobbyNode;

	// Token: 0x0400167B RID: 5755
	public GameObject settingNode;

	// Token: 0x0400167C RID: 5756
	public UISprite blueTeamSelectLogo;

	// Token: 0x0400167D RID: 5757
	public UISprite redTeamSelectLogo;

	// Token: 0x0400167E RID: 5758
	public UIButton blueTeamBtn;

	// Token: 0x0400167F RID: 5759
	public UIButton redTeamBtn;

	// Token: 0x04001680 RID: 5760
	private bool isBlueTeam = true;

	// Token: 0x04001681 RID: 5761
	public UISprite sportRankSprite;

	// Token: 0x04001682 RID: 5762
	public UILabel sportRankLabel;

	// Token: 0x04001683 RID: 5763
	public UILabel sportNickNameLabel;

	// Token: 0x04001684 RID: 5764
	public UILabel sportCoinLabel;

	// Token: 0x04001685 RID: 5765
	public UILabel sportHonorLabel;

	// Token: 0x04001686 RID: 5766
	public UILabel sportExpLabel;

	// Token: 0x04001687 RID: 5767
	public UILabel sportSeasonScoreLabel;

	// Token: 0x04001688 RID: 5768
	public UILabel sportKillDeadLabel;

	// Token: 0x04001689 RID: 5769
	public UILabel sportHeadshotLabel;

	// Token: 0x0400168A RID: 5770
	public UILabel sportContinuousKillLabel;

	// Token: 0x0400168B RID: 5771
	public UILabel sportParticipationLabel;

	// Token: 0x0400168C RID: 5772
	public UILabel sportOccupyLabel;

	// Token: 0x0400168D RID: 5773
	public UISprite sportResultTopBg1;

	// Token: 0x0400168E RID: 5774
	public UISprite sportResultTopBg2;

	// Token: 0x0400168F RID: 5775
	public UIToggle selfInfoToggle;

	// Token: 0x04001690 RID: 5776
	public UIToggle mvpInfoToggle;

	// Token: 0x04001691 RID: 5777
	private float playerInfoListRefreshTime;

	// Token: 0x04001692 RID: 5778
	private DateTime lastDateTime;

	// Token: 0x04001693 RID: 5779
	private bool isCountdownPanelVisible;

	// Token: 0x04001694 RID: 5780
	public bool isDead;

	// Token: 0x04001695 RID: 5781
	public GameObject countdownNode;

	// Token: 0x04001696 RID: 5782
	public UILabel countdownLabel;

	// Token: 0x04001697 RID: 5783
	private bool isOne;

	// Token: 0x04001698 RID: 5784
	private bool isTwo;

	// Token: 0x04001699 RID: 5785
	private bool isThree;

	// Token: 0x0400169A RID: 5786
	private bool isFour;

	// Token: 0x0400169B RID: 5787
	private bool isFive;

	// Token: 0x0400169C RID: 5788
	private object mLockAddFriendInGameRequest = new object();

	// Token: 0x02000298 RID: 664
	// (Invoke) Token: 0x06001345 RID: 4933
	public delegate void PauseStartEventHandler();
}
