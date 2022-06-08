using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004E9 RID: 1257
public class GGNetworkMode6 : MonoBehaviour
{
	// Token: 0x0600234B RID: 9035 RVA: 0x0010D480 File Offset: 0x0010B880
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

	// Token: 0x0600234C RID: 9036 RVA: 0x0010D4C1 File Offset: 0x0010B8C1
	private void Start()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		GGNetworkKit.mInstance.MessageOk += this.Event_MessageOK;
		this.InitMyNetworkCharacter();
	}

	// Token: 0x0600234D RID: 9037 RVA: 0x0010D4EF File Offset: 0x0010B8EF
	private void OnDisable()
	{
		GGNetworkKit.mInstance.MessageOk -= this.Event_MessageOK;
	}

	// Token: 0x0600234E RID: 9038 RVA: 0x0010D508 File Offset: 0x0010B908
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
				if (this.blueRankInfoList.IndexOf(this.myNetworkCharacter.mPlayerProperties) == 0)
				{
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
				}
			}
			int num = (int)(this.myNetworkCharacter.mPlayerProperties.killNum - this.myNetworkCharacter.mPlayerProperties.deadNum);
			if (num <= 0)
			{
				num = 0;
			}
			GrowthManagerKit.AddCoins(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tKnifeCompetition, this.myNetworkCharacter.mPlayerProperties.rating) + num + 10);
			GrowthManagerKit.AddCharacterExp(GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tKnifeCompetition, this.myNetworkCharacter.mPlayerProperties.rating));
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
		else if (message.messageType == GGMessageType.MessageKnifeCompetitionTeamKillerIncrease)
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

	// Token: 0x0600234F RID: 9039 RVA: 0x0010D944 File Offset: 0x0010BD44
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
					this.CalcRoundResultForKnifeCompetitionMode(GGTeamType.blue);
					this.winTeam = GGTeamType.blue;
				}
			}
			else
			{
				this.CalcRoundResultForKnifeCompetitionMode(GGTeamType.red);
				this.winTeam = GGTeamType.red;
			}
		}
		else if (this.mGlobalInfo.modeInfo.redKilling >= this.mGlobalInfo.modeInfo.MAXKilling)
		{
			this.CalcRoundResultForKnifeCompetitionMode(GGTeamType.red);
			this.winTeam = GGTeamType.red;
		}
		else if (this.mGlobalInfo.modeInfo.blueKilling >= this.mGlobalInfo.modeInfo.MAXKilling)
		{
			this.CalcRoundResultForKnifeCompetitionMode(GGTeamType.blue);
			this.winTeam = GGTeamType.blue;
		}
		this.RatingBroadcastAndVerifyForKnifeCompetitionMode();
		while (this.mAllplayersID.Count != 0)
		{
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
			if (this.blueRankInfoList.IndexOf(this.myNetworkCharacter.mPlayerProperties) == 0)
			{
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
			}
		}
		int coinAppend = (int)(this.myNetworkCharacter.mPlayerProperties.killNum - this.myNetworkCharacter.mPlayerProperties.deadNum);
		if (coinAppend <= 0)
		{
			coinAppend = 0;
		}
		GrowthManagerKit.AddCoins(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tKnifeCompetition, this.myNetworkCharacter.mPlayerProperties.rating) + coinAppend + 10);
		GrowthManagerKit.AddCharacterExp(GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tKnifeCompetition, this.myNetworkCharacter.mPlayerProperties.rating));
		UIPauseDirector.mInstance.PushResultPanel();
		this.roundOverChkStatusFlag = 2;
		GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().Reset();
		this.EnableNewRoundBroadcastAndVerifyForKnifeCompetitionMode();
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

	// Token: 0x06002350 RID: 9040 RVA: 0x0010D95F File Offset: 0x0010BD5F
	private void Update()
	{
		this.InitMyNetworkCharacter();
		this.ModeLogic();
	}

	// Token: 0x06002351 RID: 9041 RVA: 0x0010D970 File Offset: 0x0010BD70
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
			if (!this.isInRoudResultCalculating && this.KnifeCompetitionRoundOverChk())
			{
				base.StartCoroutine(this.RunRoundResultCalculate());
				this.isInRoudResultCalculating = true;
			}
		}
	}

	// Token: 0x06002352 RID: 9042 RVA: 0x0010D9E0 File Offset: 0x0010BDE0
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

	// Token: 0x06002353 RID: 9043 RVA: 0x0010DB60 File Offset: 0x0010BF60
	public void Reset()
	{
		base.gameObject.AddComponent<GGNetworkMode6>();
		UnityEngine.Object.DestroyImmediate(this);
	}

	// Token: 0x06002354 RID: 9044 RVA: 0x0010DB74 File Offset: 0x0010BF74
	public bool KnifeCompetitionRoundOverChk()
	{
		return this.mGlobalInfo.modeInfo.redKilling >= this.mGlobalInfo.modeInfo.MAXKilling || this.mGlobalInfo.modeInfo.blueKilling >= this.mGlobalInfo.modeInfo.MAXKilling;
	}

	// Token: 0x06002355 RID: 9045 RVA: 0x0010DBD0 File Offset: 0x0010BFD0
	public void CalcRoundResultForKnifeCompetitionMode(GGTeamType winTeam)
	{
		List<GGNetworkPlayerProperties> list = new List<GGNetworkPlayerProperties>(GGNetworkKit.mInstance.GetPlayerPropertiesList().ToArray());
		for (int i = 0; i < list.Count; i++)
		{
			list[i].participation = list[i].damageNum / 200 * 2 + list[i].bedamageNum / 200;
			list[i].KnifeCompetitionScore = list[i].killNum * 3 - list[i].deadNum + list[i].headshotNum * 3 + list[i].maxKillNum * 5 + list[i].participation;
			list[i].KnifeCompetitionScore = Math.Max(list[i].KnifeCompetitionScore, 0);
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
						if (list[j].KnifeCompetitionScore > this.blueRankInfoList[k].KnifeCompetitionScore)
						{
							this.blueRankInfoList.Insert(k, list[j]);
							break;
						}
						if (list[j].KnifeCompetitionScore == this.blueRankInfoList[k].KnifeCompetitionScore && list[j].id > this.blueRankInfoList[k].id)
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
						if (list[j].KnifeCompetitionScore > this.redRankInfoList[l].KnifeCompetitionScore)
						{
							this.redRankInfoList.Insert(l, list[j]);
							break;
						}
						if (list[j].KnifeCompetitionScore == this.redRankInfoList[l].KnifeCompetitionScore && list[j].id > this.redRankInfoList[l].id)
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
					if (list[j].KnifeCompetitionScore > list2[m].KnifeCompetitionScore)
					{
						list2.Insert(m, list[j]);
						break;
					}
					if (list[j].KnifeCompetitionScore == list2[m].KnifeCompetitionScore && list[j].id > list2[m].id)
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
			GameValueForRatingCalc gameValueForRatingCalc = new GameValueForRatingCalc(GrowthGameModeTag.tKnifeCompetition);
			gameValueForRatingCalc.mKCModeValue.DeadNum = (int)this.blueRankInfoList[n].deadNum;
			gameValueForRatingCalc.mKCModeValue.enemyTeamTotalPlayerNum = this.redRankInfoList.Count;
			gameValueForRatingCalc.mKCModeValue.isWinner = (winTeam == this.blueRankInfoList[n].team);
			gameValueForRatingCalc.mKCModeValue.killNum = (int)this.blueRankInfoList[n].killNum;
			gameValueForRatingCalc.mKCModeValue.myTeamTotalPlayerNum = this.blueRankInfoList.Count;
			gameValueForRatingCalc.mKCModeValue.rankInMyTeam = n + 1;
			gameValueForRatingCalc.allRankInfoList = list2;
			gameValueForRatingCalc.rankInAll = list2.IndexOf(this.blueRankInfoList[n]) + 1;
			gameValueForRatingCalc.score = (int)this.blueRankInfoList[n].KnifeCompetitionScore;
			this.blueRankInfoList[n].rating = GrowthManagerKit.GetOncePlayRating(gameValueForRatingCalc);
			this.blueRankInfoList[n].isWinner = gameValueForRatingCalc.mKCModeValue.isWinner;
			int num = gameValueForRatingCalc.mKCModeValue.killNum - gameValueForRatingCalc.mKCModeValue.DeadNum;
			if (num <= 0)
			{
				num = 0;
			}
			this.blueRankInfoList[n].coinAdd = (short)(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tKnifeCompetition, this.blueRankInfoList[n].rating) + num + 10);
			this.blueRankInfoList[n].expAdd = (short)GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tKnifeCompetition, this.blueRankInfoList[n].rating);
			this.blueRankInfoList[n].honorPointAdd = (short)GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tKnifeCompetition, this.blueRankInfoList[n].rating);
			this.blueRankInfoList[n].seasonScoreAdd = (short)GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tKnifeCompetition, this.blueRankInfoList[n].rating, this.blueRankInfoList[n].isWinner);
		}
		for (int num2 = 0; num2 < this.redRankInfoList.Count; num2++)
		{
			GameValueForRatingCalc gameValueForRatingCalc2 = new GameValueForRatingCalc(GrowthGameModeTag.tKnifeCompetition);
			gameValueForRatingCalc2.mKCModeValue.DeadNum = (int)this.redRankInfoList[num2].deadNum;
			gameValueForRatingCalc2.mKCModeValue.enemyTeamTotalPlayerNum = this.blueRankInfoList.Count;
			gameValueForRatingCalc2.mKCModeValue.isWinner = (winTeam == this.redRankInfoList[num2].team);
			gameValueForRatingCalc2.mKCModeValue.killNum = (int)this.redRankInfoList[num2].killNum;
			gameValueForRatingCalc2.mKCModeValue.myTeamTotalPlayerNum = this.redRankInfoList.Count;
			gameValueForRatingCalc2.mKCModeValue.rankInMyTeam = num2 + 1;
			gameValueForRatingCalc2.allRankInfoList = list2;
			gameValueForRatingCalc2.rankInAll = list2.IndexOf(this.redRankInfoList[num2]) + 1;
			gameValueForRatingCalc2.score = (int)this.redRankInfoList[num2].KnifeCompetitionScore;
			this.redRankInfoList[num2].rating = GrowthManagerKit.GetOncePlayRating(gameValueForRatingCalc2);
			this.redRankInfoList[num2].isWinner = gameValueForRatingCalc2.mKCModeValue.isWinner;
			int num3 = gameValueForRatingCalc2.mKCModeValue.killNum - gameValueForRatingCalc2.mKCModeValue.DeadNum;
			if (num3 <= 0)
			{
				num3 = 0;
			}
			this.redRankInfoList[num2].coinAdd = (short)(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tKnifeCompetition, this.redRankInfoList[num2].rating) + num3 + 10);
			this.redRankInfoList[num2].expAdd = (short)GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tKnifeCompetition, this.redRankInfoList[num2].rating);
			this.redRankInfoList[num2].honorPointAdd = (short)GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tKnifeCompetition, this.redRankInfoList[num2].rating);
			this.redRankInfoList[num2].seasonScoreAdd = (short)GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tKnifeCompetition, this.redRankInfoList[num2].rating, this.redRankInfoList[num2].isWinner);
		}
	}

	// Token: 0x06002356 RID: 9046 RVA: 0x0010E414 File Offset: 0x0010C814
	public void RatingBroadcastAndVerifyForKnifeCompetitionMode()
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

	// Token: 0x06002357 RID: 9047 RVA: 0x0010E4BC File Offset: 0x0010C8BC
	public void EnableNewRoundBroadcastAndVerifyForKnifeCompetitionMode()
	{
		this.GetPlayerIdsSnapshot();
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageNewRound;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.Others);
	}

	// Token: 0x06002358 RID: 9048 RVA: 0x0010E4E8 File Offset: 0x0010C8E8
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

	// Token: 0x06002359 RID: 9049 RVA: 0x0010E540 File Offset: 0x0010C940
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

	// Token: 0x040023FD RID: 9213
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x040023FE RID: 9214
	private bool mReceiveAllACK;

	// Token: 0x040023FF RID: 9215
	private List<int> mAllplayersID = new List<int>();

	// Token: 0x04002400 RID: 9216
	private List<int> mTmpAllplayersID = new List<int>();

	// Token: 0x04002401 RID: 9217
	private bool mReceiveAllActiveNewRound;

	// Token: 0x04002402 RID: 9218
	private int mACKResultNum;

	// Token: 0x04002403 RID: 9219
	private int mACkActiveNewRoundNum;

	// Token: 0x04002404 RID: 9220
	public readonly float KC_MODE_CUT_RATE = 1f;

	// Token: 0x04002405 RID: 9221
	public float kcTimeCount;

	// Token: 0x04002406 RID: 9222
	private float CalcPlayerNumTimeCount;

	// Token: 0x04002407 RID: 9223
	private List<GGNetworkPlayerProperties> blueRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x04002408 RID: 9224
	private List<GGNetworkPlayerProperties> redRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x04002409 RID: 9225
	private GGTeamType winTeam = GGTeamType.Nil;

	// Token: 0x0400240A RID: 9226
	private int roundOverChkStatusFlag;

	// Token: 0x0400240B RID: 9227
	public bool isClacedResult;

	// Token: 0x0400240C RID: 9228
	private GGNetworkCharacter myNetworkCharacter;

	// Token: 0x0400240D RID: 9229
	public bool isInRoudResultCalculating;
}
