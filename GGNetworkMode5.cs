using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004E8 RID: 1256
public class GGNetworkMode5 : MonoBehaviour
{
	// Token: 0x0600232C RID: 9004 RVA: 0x0010BB27 File Offset: 0x00109F27
	private void Awake()
	{
	}

	// Token: 0x0600232D RID: 9005 RVA: 0x0010BB2C File Offset: 0x00109F2C
	private void InitMyNetworkCharacter()
	{
		if (this.myNetworkCharacter == null)
		{
			GameObject gameObject = GameObject.FindWithTag("Player");
			if (gameObject != null)
			{
				this.myNetworkCharacter = gameObject.GetComponent<GGNetworkCharacter>();
			}
		}
	}

	// Token: 0x0600232E RID: 9006 RVA: 0x0010BB6D File Offset: 0x00109F6D
	private void Start()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		GGNetworkKit.mInstance.MessageOk += this.Event_MessageOK;
		UIPlayDirector.OnStartMutationMode += this.OnStartMutationMode;
		this.InitMyNetworkCharacter();
	}

	// Token: 0x0600232F RID: 9007 RVA: 0x0010BBAC File Offset: 0x00109FAC
	private void OnDisable()
	{
		GGNetworkKit.mInstance.MessageOk -= this.Event_MessageOK;
		UIPlayDirector.OnStartMutationMode -= this.OnStartMutationMode;
	}

	// Token: 0x06002330 RID: 9008 RVA: 0x0010BBD8 File Offset: 0x00109FD8
	private void Event_MessageOK(GGMessage message)
	{
		if (message.messageType == GGMessageType.MessageModeResult)
		{
			if (this.myNetworkCharacter == null)
			{
				return;
			}
			GGMessage ggmessage = new GGMessage();
			ggmessage.messageType = GGMessageType.MessageACKModeResult;
			ggmessage.messageContent = new GGMessageContent();
			ggmessage.messageContent.ID = this.myNetworkCharacter.mPlayerProperties.id;
			GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
			this.myNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
			this.myNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
			this.blueRankInfoList = message.messageContent.blueRankInfoList;
			this.redRankInfoList = message.messageContent.redRankInfoList;
			if (this.blueRankInfoList == null)
			{
				this.blueRankInfoList = new List<GGNetworkPlayerProperties>();
			}
			if (this.redRankInfoList == null)
			{
				this.redRankInfoList = new List<GGNetworkPlayerProperties>();
			}
			GGNetworkManageGlobalInfo.mInstance.blueRankInfoList = this.blueRankInfoList;
			GGNetworkManageGlobalInfo.mInstance.redRankInfoList = this.redRankInfoList;
			if (this.myNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
			{
				foreach (GGNetworkPlayerProperties ggnetworkPlayerProperties in this.blueRankInfoList)
				{
					if (ggnetworkPlayerProperties.id == this.myNetworkCharacter.mPlayerProperties.id)
					{
						this.myNetworkCharacter.mPlayerProperties = ggnetworkPlayerProperties;
						break;
					}
				}
			}
			else
			{
				foreach (GGNetworkPlayerProperties ggnetworkPlayerProperties2 in this.redRankInfoList)
				{
					if (ggnetworkPlayerProperties2.id == this.myNetworkCharacter.mPlayerProperties.id)
					{
						this.myNetworkCharacter.mPlayerProperties = ggnetworkPlayerProperties2;
						break;
					}
				}
			}
			if (this.myNetworkCharacter.mPlayerProperties.isWinner)
			{
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalMutationModeVictory, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalMutationModeVictory));
			}
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInMutationMode, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInMutationMode));
			int num = 0;
			if (num <= 0)
			{
				num = 0;
			}
			if (!this.isClacedResult)
			{
				GrowthManagerKit.AddCoins(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tMutation, this.myNetworkCharacter.mPlayerProperties.rating) + num);
				GrowthManagerKit.AddCharacterExp(GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tMutation, this.myNetworkCharacter.mPlayerProperties.rating));
				UIPauseDirector.mInstance.PushResultPanel();
				this.isClacedResult = true;
			}
		}
		else if (message.messageType == GGMessageType.MessageNewRound)
		{
			if (this.myNetworkCharacter == null)
			{
				return;
			}
			GGMessage ggmessage2 = new GGMessage();
			ggmessage2.messageType = GGMessageType.MessageACKActiveNewRound;
			ggmessage2.messageContent = new GGMessageContent();
			ggmessage2.messageContent.ID = this.myNetworkCharacter.mPlayerProperties.id;
			GGNetworkKit.mInstance.SendMessage(ggmessage2, GGTarget.MasterClient);
			GGNetworkKit.mInstance.EndCurrentRound();
			UIPauseDirector.mInstance.EnableNewRoundBtn();
			GGNetworkKit.mInstance.DisconnectFromRoom();
		}
		else if (message.messageType == GGMessageType.MessageACKModeResult)
		{
			if (this.mAllplayersID.Contains(message.messageContent.ID))
			{
				this.mAllplayersID.Remove(message.messageContent.ID);
			}
			if (this.mAllplayersID.Count == 0)
			{
			}
		}
		else if (message.messageType == GGMessageType.MessageACKActiveNewRound)
		{
			if (this.mAllplayersID.Contains(message.messageContent.ID))
			{
				this.mAllplayersID.Remove(message.messageContent.ID);
			}
			if (this.mAllplayersID.Count == 0)
			{
			}
		}
	}

	// Token: 0x06002331 RID: 9009 RVA: 0x0010BF94 File Offset: 0x0010A394
	private IEnumerator RunRoundResultCalculate()
	{
		this.roundOverChkStatusFlag = 1;
		if (this.mGlobalInfo.modeInfo.humanNum == 0)
		{
			this.CalcRoundResultForMutationMode(GGTeamType.red);
			this.winTeam = GGTeamType.red;
		}
		else if (this.mGlobalInfo.modeInfo.survivalTimer == 0)
		{
			this.CalcRoundResultForMutationMode(GGTeamType.blue);
			this.winTeam = GGTeamType.blue;
		}
		this.RatingBroadcastAndVerifyForMutationMode();
		int[] oldAllplayersID = this.mAllplayersID.ToArray();
		float oldTime = Time.time;
		while (this.mAllplayersID.Count != 0)
		{
			if (Time.time - oldTime > 10f)
			{
				foreach (int item in oldAllplayersID)
				{
					if (this.mAllplayersID.Contains(item))
					{
						this.mAllplayersID.Remove(item);
					}
				}
			}
			this.CheckResultForMasterClient();
			yield return new WaitForSeconds(1f);
		}
		this.myNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
		this.myNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
		if (this.myNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
		{
			foreach (GGNetworkPlayerProperties ggnetworkPlayerProperties in this.blueRankInfoList)
			{
				if (ggnetworkPlayerProperties.id == this.myNetworkCharacter.mPlayerProperties.id)
				{
					this.myNetworkCharacter.mPlayerProperties = ggnetworkPlayerProperties;
					break;
				}
			}
		}
		else
		{
			foreach (GGNetworkPlayerProperties ggnetworkPlayerProperties2 in this.redRankInfoList)
			{
				if (ggnetworkPlayerProperties2.id == this.myNetworkCharacter.mPlayerProperties.id)
				{
					this.myNetworkCharacter.mPlayerProperties = ggnetworkPlayerProperties2;
					break;
				}
			}
		}
		if (this.myNetworkCharacter.mPlayerProperties.isWinner)
		{
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalMutationModeVictory, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalMutationModeVictory));
		}
		GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInMutationMode, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInMutationMode));
		int coinAppend = 0;
		if (coinAppend <= 0)
		{
			coinAppend = 0;
		}
		GrowthManagerKit.AddCoins(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tMutation, this.myNetworkCharacter.mPlayerProperties.rating) + coinAppend);
		GrowthManagerKit.AddCharacterExp(GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tMutation, this.myNetworkCharacter.mPlayerProperties.rating));
		UIPauseDirector.mInstance.PushResultPanel();
		this.isClacedResult = true;
		this.roundOverChkStatusFlag = 2;
		GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().Reset();
		this.EnableNewRoundBroadcastAndVerifyForMutationMode();
		int[] oldAllplayersID2 = this.mAllplayersID.ToArray();
		float oldTime2 = Time.time;
		while (this.mAllplayersID.Count != 0)
		{
			if (Time.time - oldTime2 > 10f)
			{
				foreach (int item2 in oldAllplayersID2)
				{
					if (this.mAllplayersID.Contains(item2))
					{
						this.mAllplayersID.Remove(item2);
					}
				}
			}
			this.CheckEnableNewButtonForMasterClient();
			yield return new WaitForSeconds(1f);
		}
		GGNetworkKit.mInstance.EndCurrentRound();
		UIPauseDirector.mInstance.EnableNewRoundBtn();
		GGNetworkKit.mInstance.DisconnectFromRoom();
		this.roundOverChkStatusFlag = 3;
		yield break;
	}

	// Token: 0x06002332 RID: 9010 RVA: 0x0010BFB0 File Offset: 0x0010A3B0
	public void ModeLogic()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
		if (this.mGlobalInfo == null)
		{
			return;
		}
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			if (!this.MutationRoundOverChk())
			{
				this.AutoStartMutationTimer += Time.deltaTime;
				if (this.AutoStartMutationTimer > 1f)
				{
					this.CalcHumanAndZombieNum();
					this.TimerCheck();
					this.ActiveStartMutationButton();
					if (this.mGlobalInfo.modeInfo.zombieNum == 0 && this.mGlobalInfo.modeInfo.isGotoGameScene && this.mGlobalInfo.modeInfo.humanNum > 4)
					{
						this.AutoStartMutation();
					}
					this.AutoStartMutationTimer = 0f;
				}
				if (this.mGlobalInfo.modeInfo.isStartMutationTimer)
				{
					this.CutDown5s();
				}
				if (this.mGlobalInfo.modeInfo.isStartTranslate)
				{
					this.CutDown3s();
				}
			}
			else if (!this.isInRoudResultCalculating)
			{
				base.StartCoroutine(this.RunRoundResultCalculate());
				this.isInRoudResultCalculating = true;
			}
		}
		this.PlayerSurvivalTimeCheck();
	}

	// Token: 0x06002333 RID: 9011 RVA: 0x0010C0E0 File Offset: 0x0010A4E0
	private void CutDown5s()
	{
		this.MutationTimer += Time.deltaTime;
		if (this.MutationTimer >= 1f)
		{
			if (this.mGlobalInfo.modeInfo.MutationTimer > 0)
			{
				this.mGlobalInfo.modeInfo.MutationTimer--;
				this.MutationTimer = 0f;
			}
			if (this.mGlobalInfo.modeInfo.MutationTimer == 0)
			{
				this.mGlobalInfo.modeInfo.isStartMutationTimer = false;
				GGMessage ggmessage = new GGMessage();
				ggmessage.messageType = GGMessageType.MessageMutationModePlayerCanAttack;
				GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
				this.SendStartMutationMessage();
				this.mGlobalInfo.modeInfo.isStartTranslate = true;
			}
		}
	}

	// Token: 0x06002334 RID: 9012 RVA: 0x0010C1A0 File Offset: 0x0010A5A0
	private void CutDown3s()
	{
		this.TranslateTimer += Time.deltaTime;
		if (this.TranslateTimer >= 1f)
		{
			if (this.mGlobalInfo.modeInfo.TranslateTimer > 0)
			{
				this.mGlobalInfo.modeInfo.TranslateTimer--;
			}
			if (this.mGlobalInfo.modeInfo.TranslateTimer == 0)
			{
				this.mGlobalInfo.modeInfo.isStartTranslate = false;
				this.TranslateToGameScene();
			}
			this.TranslateTimer = 0f;
		}
	}

	// Token: 0x06002335 RID: 9013 RVA: 0x0010C234 File Offset: 0x0010A634
	private void TranslateToGameScene()
	{
		this.mGlobalInfo.modeInfo.isGotoGameScene = true;
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageMutationModePlayerTranslate;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
	}

	// Token: 0x06002336 RID: 9014 RVA: 0x0010C26C File Offset: 0x0010A66C
	public void ActiveStartMutationButton()
	{
		if (this.mGlobalInfo.modeInfo.humanNum > 9 && !this.mGlobalInfo.modeInfo.isStartMutation)
		{
			this.StartMutation();
		}
		if (!this.mGlobalInfo.modeInfo.isStartMutation)
		{
			this.ShowStartMutationButton();
		}
		if (this.mGlobalInfo.modeInfo.humanNum > 4 && !this.mGlobalInfo.modeInfo.isStartMutation)
		{
			this.EnableStartMutationButton();
		}
	}

	// Token: 0x06002337 RID: 9015 RVA: 0x0010C2F7 File Offset: 0x0010A6F7
	public void ShowStartMutationButton()
	{
		UIModeDirector.mInstance.ShowStartBtn();
	}

	// Token: 0x06002338 RID: 9016 RVA: 0x0010C303 File Offset: 0x0010A703
	public void EnableStartMutationButton()
	{
		UIModeDirector.mInstance.EnableStartBtn();
	}

	// Token: 0x06002339 RID: 9017 RVA: 0x0010C30F File Offset: 0x0010A70F
	public void HideStartMutationButton()
	{
		UIModeDirector.mInstance.StartBtnPressed();
		UIModeDirector.mInstance.startBtn.isEnabled = false;
	}

	// Token: 0x0600233A RID: 9018 RVA: 0x0010C32B File Offset: 0x0010A72B
	private void OnStartMutationMode()
	{
		this.StartMutation();
	}

	// Token: 0x0600233B RID: 9019 RVA: 0x0010C333 File Offset: 0x0010A733
	private void StartMutation()
	{
		this.mGlobalInfo.modeInfo.isStartMutation = true;
		this.mGlobalInfo.modeInfo.isStartMutationTimer = true;
		this.HideStartMutationButton();
	}

	// Token: 0x0600233C RID: 9020 RVA: 0x0010C35D File Offset: 0x0010A75D
	public void AutoStartMutation()
	{
		this.SendStartMutationMessage();
	}

	// Token: 0x0600233D RID: 9021 RVA: 0x0010C368 File Offset: 0x0010A768
	public void SendStartMutationMessage()
	{
		List<GGNetworkPlayerProperties> playerPropertiesList = GGNetworkKit.mInstance.GetPlayerPropertiesList();
		int index = UnityEngine.Random.Range(0, playerPropertiesList.Count);
		int id = playerPropertiesList[index].id;
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessagePlayerStartMutation;
		GGNetworkKit.mInstance.SendMessage(ggmessage, id);
	}

	// Token: 0x0600233E RID: 9022 RVA: 0x0010C3B4 File Offset: 0x0010A7B4
	public void Reset()
	{
		GGNetworkKit.mInstance.SetRoomJoinableStatus(true);
		base.gameObject.AddComponent<GGNetworkMode5>();
		UnityEngine.Object.DestroyImmediate(this);
	}

	// Token: 0x0600233F RID: 9023 RVA: 0x0010C3D4 File Offset: 0x0010A7D4
	private void TimerCheck()
	{
		if (this.mGlobalInfo.modeInfo.survivalTimer > 0 && this.mGlobalInfo.modeInfo.isGotoGameScene)
		{
			this.mGlobalInfo.modeInfo.survivalTimer--;
		}
		if (this.mGlobalInfo.modeInfo.survivalTimer == 60)
		{
			GGNetworkKit.mInstance.SetRoomJoinableStatus(false);
		}
	}

	// Token: 0x06002340 RID: 9024 RVA: 0x0010C448 File Offset: 0x0010A848
	private void PlayerSurvivalTimeCheck()
	{
		if (this.mGlobalInfo.modeInfo.isGotoGameScene && this.myNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
		{
			this.SurvivalTimeCount += Time.deltaTime;
			if (this.SurvivalTimeCount >= this.MT_MODE_SURVIVAL_CUT_RATE)
			{
				GGMutationModeControl mInstance = GGMutationModeControl.mInstance;
				mInstance.score += 50;
				GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.ScoreInMutation, 50, string.Empty);
				this.myNetworkCharacter.mPlayerProperties.MutationModeScore = GGMutationModeControl.mInstance.score;
				this.SurvivalTimeCount = 0f;
			}
		}
	}

	// Token: 0x06002341 RID: 9025 RVA: 0x0010C4E9 File Offset: 0x0010A8E9
	private void CalcHumanAndZombieNum()
	{
		this.mGlobalInfo.modeInfo.humanNum = GGNetworkKit.mInstance.GetBluePlayerPropertiesList().Count;
		this.mGlobalInfo.modeInfo.zombieNum = GGNetworkKit.mInstance.GetRedPlayerPropertiesList().Count;
	}

	// Token: 0x06002342 RID: 9026 RVA: 0x0010C52C File Offset: 0x0010A92C
	public bool MutationRoundOverChk()
	{
		return this.mGlobalInfo.modeInfo.isGotoGameScene && (this.mGlobalInfo.modeInfo.humanNum == 0 || this.mGlobalInfo.modeInfo.survivalTimer == 0);
	}

	// Token: 0x06002343 RID: 9027 RVA: 0x0010C57B File Offset: 0x0010A97B
	private void Update()
	{
		this.InitMyNetworkCharacter();
		this.ModeLogic();
	}

	// Token: 0x06002344 RID: 9028 RVA: 0x0010C58C File Offset: 0x0010A98C
	public void CalcRoundResultForMutationMode(GGTeamType winTeam)
	{
		List<GGNetworkPlayerProperties> list = new List<GGNetworkPlayerProperties>(GGNetworkKit.mInstance.GetPlayerPropertiesList().ToArray());
		for (int i = 0; i < list.Count; i++)
		{
			list[i].participation = list[i].damageNum / 200 * 2 + list[i].bedamageNum / 200;
		}
		List<GGNetworkPlayerProperties> list2 = new List<GGNetworkPlayerProperties>();
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j].team == GGTeamType.blue)
			{
				if (this.blueRankInfoList.Count == 0)
				{
					this.blueRankInfoList.Add(list[j]);
				}
				else
				{
					int count = this.blueRankInfoList.Count;
					for (int k = 0; k < count; k++)
					{
						if (list[j].MutationModeScore > this.blueRankInfoList[k].MutationModeScore)
						{
							this.blueRankInfoList.Insert(k, list[j]);
							break;
						}
						if (list[j].MutationModeScore == this.blueRankInfoList[k].MutationModeScore && list[j].id > this.blueRankInfoList[k].id)
						{
							this.blueRankInfoList.Insert(k, list[j]);
							break;
						}
						if (k == count - 1)
						{
							this.blueRankInfoList.Add(list[j]);
						}
					}
				}
			}
			else if (list[j].team == GGTeamType.red)
			{
				if (this.redRankInfoList.Count == 0)
				{
					this.redRankInfoList.Add(list[j]);
				}
				else
				{
					int count2 = this.redRankInfoList.Count;
					for (int l = 0; l < count2; l++)
					{
						if (list[j].MutationModeScore > this.redRankInfoList[l].MutationModeScore)
						{
							this.redRankInfoList.Insert(l, list[j]);
							break;
						}
						if (list[j].MutationModeScore == this.redRankInfoList[l].MutationModeScore && list[j].id > this.redRankInfoList[l].id)
						{
							this.redRankInfoList.Insert(l, list[j]);
							break;
						}
						if (l == count2 - 1)
						{
							this.redRankInfoList.Add(list[j]);
						}
					}
				}
			}
			if (list2.Count == 0)
			{
				list2.Add(list[j]);
			}
			else
			{
				int count3 = list2.Count;
				for (int m = 0; m < count3; m++)
				{
					if (list[j].MutationModeScore > list2[m].MutationModeScore)
					{
						list2.Insert(m, list[j]);
						break;
					}
					if (list[j].MutationModeScore == list2[m].MutationModeScore && list[j].id > list2[m].id)
					{
						list2.Insert(m, list[j]);
						break;
					}
					if (m == count3 - 1)
					{
						list2.Add(list[j]);
					}
				}
			}
		}
		for (int n = 0; n < this.blueRankInfoList.Count; n++)
		{
			GameValueForRatingCalc gameValueForRatingCalc = new GameValueForRatingCalc(GrowthGameModeTag.tMutation);
			gameValueForRatingCalc.mMTModeValue.score = (int)this.blueRankInfoList[n].MutationModeScore;
			gameValueForRatingCalc.mMTModeValue.enemyTeamTotalPlayerNum = this.redRankInfoList.Count;
			gameValueForRatingCalc.mMTModeValue.isWinner = (winTeam == this.blueRankInfoList[n].team);
			gameValueForRatingCalc.mMTModeValue.myTeamTotalPlayerNum = this.blueRankInfoList.Count;
			gameValueForRatingCalc.mMTModeValue.rankInMyTeam = n + 1;
			gameValueForRatingCalc.allRankInfoList = list2;
			gameValueForRatingCalc.rankInAll = list2.IndexOf(this.blueRankInfoList[n]) + 1;
			gameValueForRatingCalc.score = (int)this.blueRankInfoList[n].MutationModeScore;
			this.blueRankInfoList[n].rating = GrowthManagerKit.GetOncePlayRating(gameValueForRatingCalc);
			this.blueRankInfoList[n].isWinner = gameValueForRatingCalc.mMTModeValue.isWinner;
			int num = 0;
			if (num <= 0)
			{
				num = 0;
			}
			this.blueRankInfoList[n].coinAdd = (short)(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tMutation, this.blueRankInfoList[n].rating) + num);
			this.blueRankInfoList[n].expAdd = (short)GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tMutation, this.blueRankInfoList[n].rating);
		}
		for (int num2 = 0; num2 < this.redRankInfoList.Count; num2++)
		{
			GameValueForRatingCalc gameValueForRatingCalc2 = new GameValueForRatingCalc(GrowthGameModeTag.tMutation);
			gameValueForRatingCalc2.mMTModeValue.score = (int)this.redRankInfoList[num2].MutationModeScore;
			gameValueForRatingCalc2.mMTModeValue.enemyTeamTotalPlayerNum = this.blueRankInfoList.Count;
			gameValueForRatingCalc2.mMTModeValue.isWinner = (winTeam == this.redRankInfoList[num2].team);
			gameValueForRatingCalc2.mMTModeValue.myTeamTotalPlayerNum = this.redRankInfoList.Count;
			gameValueForRatingCalc2.mMTModeValue.rankInMyTeam = num2 + 1;
			gameValueForRatingCalc2.allRankInfoList = list2;
			gameValueForRatingCalc2.rankInAll = list2.IndexOf(this.redRankInfoList[num2]) + 1;
			gameValueForRatingCalc2.score = (int)this.redRankInfoList[num2].MutationModeScore;
			this.redRankInfoList[num2].rating = GrowthManagerKit.GetOncePlayRating(gameValueForRatingCalc2);
			this.redRankInfoList[num2].isWinner = gameValueForRatingCalc2.mMTModeValue.isWinner;
			int num3 = 0;
			if (num3 <= 0)
			{
				num3 = 0;
			}
			this.redRankInfoList[num2].coinAdd = (short)(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tMutation, this.redRankInfoList[num2].rating) + num3);
			this.redRankInfoList[num2].expAdd = (short)GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tMutation, this.redRankInfoList[num2].rating);
		}
	}

	// Token: 0x06002345 RID: 9029 RVA: 0x0010CC1C File Offset: 0x0010B01C
	public void RatingBroadcastAndVerifyForMutationMode()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageModeResult;
		ggmessage.messageContent = new GGMessageContent();
		ggmessage.messageContent.blueRankInfoList = this.blueRankInfoList;
		ggmessage.messageContent.redRankInfoList = this.redRankInfoList;
		GGNetworkManageGlobalInfo.mInstance.blueRankInfoList = this.blueRankInfoList;
		GGNetworkManageGlobalInfo.mInstance.redRankInfoList = this.redRankInfoList;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.Others);
		this.GetPlayerIdsSnapshot();
	}

	// Token: 0x06002346 RID: 9030 RVA: 0x0010CC98 File Offset: 0x0010B098
	public void EnableNewRoundBroadcastAndVerifyForMutationMode()
	{
		this.GetPlayerIdsSnapshot();
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageNewRound;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.Others);
	}

	// Token: 0x06002347 RID: 9031 RVA: 0x0010CCC4 File Offset: 0x0010B0C4
	private void GetPlayerIdsSnapshot()
	{
		this.mAllplayersID.Clear();
		foreach (PhotonPlayer photonPlayer in GGNetworkKit.mInstance.GetPlayerList())
		{
			if (!photonPlayer.isMasterClient)
			{
				this.mAllplayersID.Add(photonPlayer.ID);
			}
		}
	}

	// Token: 0x06002348 RID: 9032 RVA: 0x0010CD1C File Offset: 0x0010B11C
	private void CheckResultForMasterClient()
	{
		this.mTmpAllplayersID.Clear();
		foreach (PhotonPlayer photonPlayer in GGNetworkKit.mInstance.GetPlayerList())
		{
			if (!photonPlayer.isMasterClient)
			{
				this.mTmpAllplayersID.Add(photonPlayer.ID);
			}
		}
		List<int> list = new List<int>();
		foreach (int item in this.mAllplayersID)
		{
			if (!this.mTmpAllplayersID.Contains(item))
			{
				list.Add(item);
			}
		}
		if (list.Count > 0)
		{
			foreach (int item2 in list)
			{
				this.mAllplayersID.Remove(item2);
			}
		}
	}

	// Token: 0x06002349 RID: 9033 RVA: 0x0010CE38 File Offset: 0x0010B238
	private void CheckEnableNewButtonForMasterClient()
	{
		this.mTmpAllplayersID.Clear();
		foreach (PhotonPlayer photonPlayer in GGNetworkKit.mInstance.GetPlayerList())
		{
			if (!photonPlayer.isMasterClient)
			{
				this.mTmpAllplayersID.Add(photonPlayer.ID);
			}
		}
		List<int> list = new List<int>();
		foreach (int item in this.mAllplayersID)
		{
			if (!this.mTmpAllplayersID.Contains(item))
			{
				list.Add(item);
			}
		}
		if (list.Count > 0)
		{
			foreach (int item2 in list)
			{
				this.mAllplayersID.Remove(item2);
			}
		}
	}

	// Token: 0x040023E1 RID: 9185
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x040023E2 RID: 9186
	private bool mReceiveAllACK;

	// Token: 0x040023E3 RID: 9187
	private List<int> mAllplayersID = new List<int>();

	// Token: 0x040023E4 RID: 9188
	private List<int> mTmpAllplayersID = new List<int>();

	// Token: 0x040023E5 RID: 9189
	private bool mReceiveAllActiveNewRound;

	// Token: 0x040023E6 RID: 9190
	private int mACKResultNum;

	// Token: 0x040023E7 RID: 9191
	private int mACkActiveNewRoundNum;

	// Token: 0x040023E8 RID: 9192
	public readonly float MT_MODE_CUT_RATE = 1f;

	// Token: 0x040023E9 RID: 9193
	public float MutationTimeCount;

	// Token: 0x040023EA RID: 9194
	public readonly float MT_MODE_CUT_RATE_STARTMUTATION = 5f;

	// Token: 0x040023EB RID: 9195
	public float StartMutationTimeCount;

	// Token: 0x040023EC RID: 9196
	public readonly float MT_MODE_SURVIVAL_CUT_RATE = 30f;

	// Token: 0x040023ED RID: 9197
	public float SurvivalTimeCount;

	// Token: 0x040023EE RID: 9198
	private List<GGNetworkPlayerProperties> blueRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x040023EF RID: 9199
	private List<GGNetworkPlayerProperties> redRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x040023F0 RID: 9200
	private GGTeamType winTeam = GGTeamType.Nil;

	// Token: 0x040023F1 RID: 9201
	private int roundOverChkStatusFlag;

	// Token: 0x040023F2 RID: 9202
	public bool isClacedResult;

	// Token: 0x040023F3 RID: 9203
	private GGNetworkCharacter myNetworkCharacter;

	// Token: 0x040023F4 RID: 9204
	public bool isShowStartMutationButton;

	// Token: 0x040023F5 RID: 9205
	public bool isEnableStartMutationButton;

	// Token: 0x040023F6 RID: 9206
	private bool preisStartMutation;

	// Token: 0x040023F7 RID: 9207
	public DateTime lastDateTime;

	// Token: 0x040023F8 RID: 9208
	private bool isStartMutationTimer;

	// Token: 0x040023F9 RID: 9209
	private float MutationTimer;

	// Token: 0x040023FA RID: 9210
	private float TranslateTimer;

	// Token: 0x040023FB RID: 9211
	private float AutoStartMutationTimer;

	// Token: 0x040023FC RID: 9212
	public bool isInRoudResultCalculating;
}
