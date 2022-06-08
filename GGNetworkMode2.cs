using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004E5 RID: 1253
public class GGNetworkMode2 : MonoBehaviour
{
	// Token: 0x060022E1 RID: 8929 RVA: 0x001056E8 File Offset: 0x00103AE8
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

	// Token: 0x060022E2 RID: 8930 RVA: 0x00105729 File Offset: 0x00103B29
	private void Start()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		GGNetworkKit.mInstance.MessageOk += this.Event_MessageOK;
		this.InitMyNetworkCharacter();
	}

	// Token: 0x060022E3 RID: 8931 RVA: 0x00105757 File Offset: 0x00103B57
	private void OnDisable()
	{
		GGNetworkKit.mInstance.MessageOk -= this.Event_MessageOK;
	}

	// Token: 0x060022E4 RID: 8932 RVA: 0x00105770 File Offset: 0x00103B70
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
			bool flag = false;
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
				if (this.blueRankInfoList.IndexOf(this.myNetworkCharacter.mPlayerProperties) == 0)
				{
					flag = true;
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
				if (this.redRankInfoList.IndexOf(this.myNetworkCharacter.mPlayerProperties) == 0)
				{
					flag = true;
				}
			}
			if (this.myNetworkCharacter.mPlayerProperties.isWinner && GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Sport)
			{
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictory, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictory));
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictorySeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictorySeason));
				if (flag)
				{
					GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeMvp, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeMvp));
					GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeMvpSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeMvpSeason));
				}
			}
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInKillingCompetitionMode, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInKillingCompetitionMode));
			if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Sport)
			{
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeJoin, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeJoin));
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeJoinSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeJoinSeason));
			}
			int num = (int)(this.myNetworkCharacter.mPlayerProperties.killNum - this.myNetworkCharacter.mPlayerProperties.deadNum);
			if (num <= 0)
			{
				num = 0;
			}
			GrowthManagerKit.AddCoins(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tKillingCompetition, this.myNetworkCharacter.mPlayerProperties.rating) + num + 10);
			GrowthManagerKit.AddCharacterExp(GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tKillingCompetition, this.myNetworkCharacter.mPlayerProperties.rating));
			if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Sport)
			{
				GrowthManagerKit.AddHonorPoint(GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tKillingCompetition, this.myNetworkCharacter.mPlayerProperties.rating));
				int oncePlaySeasonScoreReward = GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tKillingCompetition, this.myNetworkCharacter.mPlayerProperties.rating, this.myNetworkCharacter.mPlayerProperties.isWinner);
				if (oncePlaySeasonScoreReward >= 0)
				{
					GrowthManagerKit.AddSeasonScore(oncePlaySeasonScoreReward);
				}
				else
				{
					GrowthManagerKit.SubSeasonScore(Math.Abs(oncePlaySeasonScoreReward));
				}
			}
			UIPauseDirector.mInstance.PushResultPanel();
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
		else if (message.messageType == GGMessageType.MessageKillingCompetitionTeamKillerIncrease)
		{
			if (message.messageContent.Team == GGTeamType.blue)
			{
				this.mGlobalInfo.modeInfo.blueKilling++;
			}
			else if (message.messageContent.Team == GGTeamType.red)
			{
				this.mGlobalInfo.modeInfo.redKilling++;
			}
		}
	}

	// Token: 0x060022E5 RID: 8933 RVA: 0x00105CD8 File Offset: 0x001040D8
	private IEnumerator RunRoundResultCalculate()
	{
		this.roundOverChkStatusFlag = 1;
		if (this.mGlobalInfo.modeInfo.redKilling >= this.mGlobalInfo.modeInfo.MAXKilling && this.mGlobalInfo.modeInfo.blueKilling >= this.mGlobalInfo.modeInfo.MAXKilling)
		{
			int num = UnityEngine.Random.Range(1, 3);
			if (num != 1)
			{
				if (num == 2)
				{
					this.CalcRoundResultForKillingCompetitionMode(GGTeamType.blue);
					this.winTeam = GGTeamType.blue;
				}
			}
			else
			{
				this.CalcRoundResultForKillingCompetitionMode(GGTeamType.red);
				this.winTeam = GGTeamType.red;
			}
		}
		else if (this.mGlobalInfo.modeInfo.redKilling >= this.mGlobalInfo.modeInfo.MAXKilling)
		{
			this.CalcRoundResultForKillingCompetitionMode(GGTeamType.red);
			this.winTeam = GGTeamType.red;
		}
		else if (this.mGlobalInfo.modeInfo.blueKilling >= this.mGlobalInfo.modeInfo.MAXKilling)
		{
			this.CalcRoundResultForKillingCompetitionMode(GGTeamType.blue);
			this.winTeam = GGTeamType.blue;
		}
		this.RatingBroadcastAndVerifyForKillingCompetitionMode();
		while (this.mAllplayersID.Count != 0)
		{
			yield return new WaitForSeconds(1f);
		}
		this.myNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
		this.myNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
		bool isMvp = false;
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
			if (this.blueRankInfoList.IndexOf(this.myNetworkCharacter.mPlayerProperties) == 0)
			{
				isMvp = true;
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
			if (this.redRankInfoList.IndexOf(this.myNetworkCharacter.mPlayerProperties) == 0)
			{
				isMvp = true;
			}
		}
		if (this.myNetworkCharacter.mPlayerProperties.isWinner && GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Sport)
		{
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictory, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictory));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictorySeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictorySeason));
			if (isMvp)
			{
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeMvp, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeMvp));
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeMvpSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeMvpSeason));
			}
		}
		GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInKillingCompetitionMode, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInKillingCompetitionMode));
		if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Sport)
		{
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeJoin, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeJoin));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeJoinSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeJoinSeason));
		}
		int coinAppend = (int)(this.myNetworkCharacter.mPlayerProperties.killNum - this.myNetworkCharacter.mPlayerProperties.deadNum);
		if (coinAppend <= 0)
		{
			coinAppend = 0;
		}
		GrowthManagerKit.AddCoins(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tKillingCompetition, this.myNetworkCharacter.mPlayerProperties.rating) + coinAppend + 10);
		GrowthManagerKit.AddCharacterExp(GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tKillingCompetition, this.myNetworkCharacter.mPlayerProperties.rating));
		if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Sport)
		{
			GrowthManagerKit.AddHonorPoint(GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tKillingCompetition, this.myNetworkCharacter.mPlayerProperties.rating));
			int oncePlaySeasonScoreReward = GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tKillingCompetition, this.myNetworkCharacter.mPlayerProperties.rating, this.myNetworkCharacter.mPlayerProperties.isWinner);
			if (oncePlaySeasonScoreReward >= 0)
			{
				GrowthManagerKit.AddSeasonScore(oncePlaySeasonScoreReward);
			}
			else
			{
				GrowthManagerKit.SubSeasonScore(Math.Abs(oncePlaySeasonScoreReward));
			}
		}
		UIPauseDirector.mInstance.PushResultPanel();
		this.roundOverChkStatusFlag = 2;
		GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().Reset();
		this.EnableNewRoundBroadcastAndVerifyForKillingCompetitionMode();
		while (this.mAllplayersID.Count != 0)
		{
			this.CheckEnableNewButtonForMasterClient();
			yield return new WaitForSeconds(1f);
		}
		GGNetworkKit.mInstance.EndCurrentRound();
		UIPauseDirector.mInstance.EnableNewRoundBtn();
		GGNetworkKit.mInstance.DisconnectFromRoom();
		this.roundOverChkStatusFlag = 3;
		yield break;
	}

	// Token: 0x060022E6 RID: 8934 RVA: 0x00105CF3 File Offset: 0x001040F3
	private void Update()
	{
		this.InitMyNetworkCharacter();
		this.ModeLogic();
	}

	// Token: 0x060022E7 RID: 8935 RVA: 0x00105D04 File Offset: 0x00104104
	public void ModeLogic()
	{
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
			if (this.mGlobalInfo == null)
			{
				return;
			}
			this.CalcBlueAndRedPlayerNum();
			if (!this.isInRoudResultCalculating && this.KillingCompetitionRoundOverChk())
			{
				base.StartCoroutine(this.RunRoundResultCalculate());
				this.isInRoudResultCalculating = true;
			}
		}
	}

	// Token: 0x060022E8 RID: 8936 RVA: 0x00105D74 File Offset: 0x00104174
	private void CalcBlueAndRedPlayerNum()
	{
		this.CalcPlayerNumTimeCount += Time.deltaTime;
		if (this.CalcPlayerNumTimeCount >= 1f)
		{
			this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerTotalNum = 0;
			this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerSurvivalNum = 0;
			this.mGlobalInfo.modeInfo.AllModeRedTeamPlayerTotalNum = 0;
			this.mGlobalInfo.modeInfo.AllModeRedTeamPlayerSurvivalNum = 0;
			Dictionary<int, GameObject> playerGameObjectList = GGNetworkKit.mInstance.GetPlayerGameObjectList();
			foreach (KeyValuePair<int, GameObject> keyValuePair in playerGameObjectList)
			{
				GameObject value = keyValuePair.Value;
				GGNetworkCharacter component = value.GetComponent<GGNetworkCharacter>();
				if (component.mPlayerProperties.team == GGTeamType.blue)
				{
					this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerTotalNum++;
					if (component.mCharacterWalkState != GGCharacterWalkState.Dead)
					{
						this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerSurvivalNum++;
					}
				}
				else if (component.mPlayerProperties.team == GGTeamType.red)
				{
					this.mGlobalInfo.modeInfo.AllModeRedTeamPlayerTotalNum++;
					if (component.mCharacterWalkState != GGCharacterWalkState.Dead)
					{
						this.mGlobalInfo.modeInfo.AllModeRedTeamPlayerSurvivalNum++;
					}
				}
			}
			this.CalcPlayerNumTimeCount = 0f;
		}
	}

	// Token: 0x060022E9 RID: 8937 RVA: 0x00105EF4 File Offset: 0x001042F4
	public void Reset()
	{
		base.gameObject.AddComponent<GGNetworkMode2>();
		UnityEngine.Object.DestroyImmediate(this);
	}

	// Token: 0x060022EA RID: 8938 RVA: 0x00105F08 File Offset: 0x00104308
	public bool KillingCompetitionRoundOverChk()
	{
		return this.mGlobalInfo.modeInfo.redKilling >= this.mGlobalInfo.modeInfo.MAXKilling || this.mGlobalInfo.modeInfo.blueKilling >= this.mGlobalInfo.modeInfo.MAXKilling;
	}

	// Token: 0x060022EB RID: 8939 RVA: 0x00105F64 File Offset: 0x00104364
	public void CalcRoundResultForKillingCompetitionMode(GGTeamType winTeam)
	{
		List<GGNetworkPlayerProperties> list = new List<GGNetworkPlayerProperties>(GGNetworkKit.mInstance.GetPlayerPropertiesList().ToArray());
		for (int i = 0; i < list.Count; i++)
		{
			list[i].participation = list[i].damageNum / 200 * 2 + list[i].bedamageNum / 200;
			list[i].KillingCompetitionScore = list[i].killNum * 3 - list[i].deadNum + list[i].headshotNum * 3 + list[i].maxKillNum * 5 + list[i].participation;
			list[i].KillingCompetitionScore = Math.Max(list[i].KillingCompetitionScore, 0);
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
						if (list[j].KillingCompetitionScore > this.blueRankInfoList[k].KillingCompetitionScore)
						{
							this.blueRankInfoList.Insert(k, list[j]);
							break;
						}
						if (list[j].KillingCompetitionScore == this.blueRankInfoList[k].KillingCompetitionScore && list[j].id > this.blueRankInfoList[k].id)
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
						if (list[j].KillingCompetitionScore > this.redRankInfoList[l].KillingCompetitionScore)
						{
							this.redRankInfoList.Insert(l, list[j]);
							break;
						}
						if (list[j].KillingCompetitionScore == this.redRankInfoList[l].KillingCompetitionScore && list[j].id > this.redRankInfoList[l].id)
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
					if (list[j].KillingCompetitionScore > list2[m].KillingCompetitionScore)
					{
						list2.Insert(m, list[j]);
						break;
					}
					if (list[j].KillingCompetitionScore == list2[m].KillingCompetitionScore && list[j].id > list2[m].id)
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
			GameValueForRatingCalc gameValueForRatingCalc = new GameValueForRatingCalc(GrowthGameModeTag.tKillingCompetition);
			gameValueForRatingCalc.mKCModeValue.DeadNum = (int)this.blueRankInfoList[n].deadNum;
			gameValueForRatingCalc.mKCModeValue.enemyTeamTotalPlayerNum = this.redRankInfoList.Count;
			gameValueForRatingCalc.mKCModeValue.isWinner = (winTeam == this.blueRankInfoList[n].team);
			gameValueForRatingCalc.mKCModeValue.killNum = (int)this.blueRankInfoList[n].killNum;
			gameValueForRatingCalc.mKCModeValue.myTeamTotalPlayerNum = this.blueRankInfoList.Count;
			gameValueForRatingCalc.mKCModeValue.rankInMyTeam = n + 1;
			gameValueForRatingCalc.allRankInfoList = list2;
			gameValueForRatingCalc.rankInAll = list2.IndexOf(this.blueRankInfoList[n]) + 1;
			gameValueForRatingCalc.score = (int)this.blueRankInfoList[n].KillingCompetitionScore;
			this.blueRankInfoList[n].rating = GrowthManagerKit.GetOncePlayRating(gameValueForRatingCalc);
			this.blueRankInfoList[n].isWinner = gameValueForRatingCalc.mKCModeValue.isWinner;
			int num = gameValueForRatingCalc.mKCModeValue.killNum - gameValueForRatingCalc.mKCModeValue.DeadNum;
			if (num <= 0)
			{
				num = 0;
			}
			this.blueRankInfoList[n].coinAdd = (short)(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tKillingCompetition, this.blueRankInfoList[n].rating) + num + 10);
			this.blueRankInfoList[n].expAdd = (short)GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tKillingCompetition, this.blueRankInfoList[n].rating);
			this.blueRankInfoList[n].honorPointAdd = (short)GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tKillingCompetition, this.blueRankInfoList[n].rating);
			this.blueRankInfoList[n].seasonScoreAdd = (short)GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tKillingCompetition, this.blueRankInfoList[n].rating, this.blueRankInfoList[n].isWinner);
		}
		for (int num2 = 0; num2 < this.redRankInfoList.Count; num2++)
		{
			GameValueForRatingCalc gameValueForRatingCalc2 = new GameValueForRatingCalc(GrowthGameModeTag.tKillingCompetition);
			gameValueForRatingCalc2.mKCModeValue.DeadNum = (int)this.redRankInfoList[num2].deadNum;
			gameValueForRatingCalc2.mKCModeValue.enemyTeamTotalPlayerNum = this.blueRankInfoList.Count;
			gameValueForRatingCalc2.mKCModeValue.isWinner = (winTeam == this.redRankInfoList[num2].team);
			gameValueForRatingCalc2.mKCModeValue.killNum = (int)this.redRankInfoList[num2].killNum;
			gameValueForRatingCalc2.mKCModeValue.myTeamTotalPlayerNum = this.redRankInfoList.Count;
			gameValueForRatingCalc2.mKCModeValue.rankInMyTeam = num2 + 1;
			gameValueForRatingCalc2.allRankInfoList = list2;
			gameValueForRatingCalc2.rankInAll = list2.IndexOf(this.redRankInfoList[num2]) + 1;
			gameValueForRatingCalc2.score = (int)this.redRankInfoList[num2].KillingCompetitionScore;
			this.redRankInfoList[num2].rating = GrowthManagerKit.GetOncePlayRating(gameValueForRatingCalc2);
			this.redRankInfoList[num2].isWinner = gameValueForRatingCalc2.mKCModeValue.isWinner;
			int num3 = gameValueForRatingCalc2.mKCModeValue.killNum - gameValueForRatingCalc2.mKCModeValue.DeadNum;
			if (num3 <= 0)
			{
				num3 = 0;
			}
			this.redRankInfoList[num2].coinAdd = (short)(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tKillingCompetition, this.redRankInfoList[num2].rating) + num3 + 10);
			this.redRankInfoList[num2].expAdd = (short)GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tKillingCompetition, this.redRankInfoList[num2].rating);
			this.redRankInfoList[num2].honorPointAdd = (short)GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tKillingCompetition, this.redRankInfoList[num2].rating);
			this.redRankInfoList[num2].seasonScoreAdd = (short)GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tKillingCompetition, this.redRankInfoList[num2].rating, this.redRankInfoList[num2].isWinner);
		}
	}

	// Token: 0x060022EC RID: 8940 RVA: 0x001067A8 File Offset: 0x00104BA8
	public void RatingBroadcastAndVerifyForKillingCompetitionMode()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageModeResult;
		ggmessage.messageContent = new GGMessageContent();
		if (this.blueRankInfoList == null)
		{
			this.blueRankInfoList = new List<GGNetworkPlayerProperties>();
		}
		if (this.redRankInfoList == null)
		{
			this.redRankInfoList = new List<GGNetworkPlayerProperties>();
		}
		ggmessage.messageContent.blueRankInfoList = this.blueRankInfoList;
		ggmessage.messageContent.redRankInfoList = this.redRankInfoList;
		GGNetworkManageGlobalInfo.mInstance.blueRankInfoList = this.blueRankInfoList;
		GGNetworkManageGlobalInfo.mInstance.redRankInfoList = this.redRankInfoList;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.Others);
		this.GetPlayerIdsSnapshot();
	}

	// Token: 0x060022ED RID: 8941 RVA: 0x00106850 File Offset: 0x00104C50
	public void EnableNewRoundBroadcastAndVerifyForKillingCompetitionMode()
	{
		this.GetPlayerIdsSnapshot();
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageNewRound;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.Others);
	}

	// Token: 0x060022EE RID: 8942 RVA: 0x0010687C File Offset: 0x00104C7C
	private void GetPlayerIdsSnapshot()
	{
		this.mAllplayersID.Clear();
		foreach (PhotonPlayer photonPlayer in GGNetworkKit.mInstance.GetPlayerList())
		{
			if (!photonPlayer.isLocal)
			{
				this.mAllplayersID.Add(photonPlayer.ID);
			}
		}
	}

	// Token: 0x060022EF RID: 8943 RVA: 0x001068D4 File Offset: 0x00104CD4
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
		int item = 0;
		bool flag = false;
		foreach (int num in this.mAllplayersID)
		{
			if (!this.mTmpAllplayersID.Contains(num))
			{
				flag = true;
				item = num;
				break;
			}
		}
		if (flag)
		{
			this.mAllplayersID.Remove(item);
		}
	}

	// Token: 0x04002393 RID: 9107
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x04002394 RID: 9108
	private bool mReceiveAllACK;

	// Token: 0x04002395 RID: 9109
	private List<int> mAllplayersID = new List<int>();

	// Token: 0x04002396 RID: 9110
	private List<int> mTmpAllplayersID = new List<int>();

	// Token: 0x04002397 RID: 9111
	private bool mReceiveAllActiveNewRound;

	// Token: 0x04002398 RID: 9112
	private int mACKResultNum;

	// Token: 0x04002399 RID: 9113
	private int mACkActiveNewRoundNum;

	// Token: 0x0400239A RID: 9114
	public readonly float SH_MODE_CUT_RATE = 1f;

	// Token: 0x0400239B RID: 9115
	public float shTimeCount;

	// Token: 0x0400239C RID: 9116
	private float CalcPlayerNumTimeCount;

	// Token: 0x0400239D RID: 9117
	private List<GGNetworkPlayerProperties> blueRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x0400239E RID: 9118
	private List<GGNetworkPlayerProperties> redRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x0400239F RID: 9119
	private GGTeamType winTeam = GGTeamType.Nil;

	// Token: 0x040023A0 RID: 9120
	private int roundOverChkStatusFlag;

	// Token: 0x040023A1 RID: 9121
	public bool isClacedResult;

	// Token: 0x040023A2 RID: 9122
	private GGNetworkCharacter myNetworkCharacter;

	// Token: 0x040023A3 RID: 9123
	public bool isInRoudResultCalculating;
}
