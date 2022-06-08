using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004E6 RID: 1254
public class GGNetworkMode3 : MonoBehaviour
{
	// Token: 0x060022F1 RID: 8945 RVA: 0x00107038 File Offset: 0x00105438
	private void Awake()
	{
	}

	// Token: 0x060022F2 RID: 8946 RVA: 0x0010703C File Offset: 0x0010543C
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

	// Token: 0x060022F3 RID: 8947 RVA: 0x0010707D File Offset: 0x0010547D
	private void Start()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		GGNetworkKit.mInstance.MessageOk += this.Event_MessageOK;
		UIPlayDirector.OnStartStrongholdMode += this.OnStartStrongholdMode;
		this.InitMyNetworkCharacter();
	}

	// Token: 0x060022F4 RID: 8948 RVA: 0x001070BC File Offset: 0x001054BC
	private void OnDisable()
	{
		GGNetworkKit.mInstance.MessageOk -= this.Event_MessageOK;
		UIPlayDirector.OnStartStrongholdMode -= this.OnStartStrongholdMode;
	}

	// Token: 0x060022F5 RID: 8949 RVA: 0x001070E8 File Offset: 0x001054E8
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
			if (this.myNetworkCharacter.mPlayerProperties.isWinner)
			{
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictory, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictory));
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictorySeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictorySeason));
				if (flag)
				{
					GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeMvp, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeMvp));
					GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeMvpSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeMvpSeason));
				}
			}
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInStrongholdMode, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInStrongholdMode));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeJoin, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeJoin));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeJoinSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeJoinSeason));
			int num = (int)(this.myNetworkCharacter.mPlayerProperties.killNum - this.myNetworkCharacter.mPlayerProperties.deadNum);
			if (num <= 0)
			{
				num = 0;
			}
			if (!this.isClacedResult)
			{
				GrowthManagerKit.AddCoins(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tStronghold, this.myNetworkCharacter.mPlayerProperties.rating) + num + 10);
				GrowthManagerKit.AddCharacterExp(GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tStronghold, this.myNetworkCharacter.mPlayerProperties.rating));
				GrowthManagerKit.AddHonorPoint(GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tStronghold, this.myNetworkCharacter.mPlayerProperties.rating));
				int oncePlaySeasonScoreReward = GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tStronghold, this.myNetworkCharacter.mPlayerProperties.rating, this.myNetworkCharacter.mPlayerProperties.isWinner);
				if (oncePlaySeasonScoreReward >= 0)
				{
					GrowthManagerKit.AddSeasonScore(oncePlaySeasonScoreReward);
				}
				else
				{
					GrowthManagerKit.SubSeasonScore(Math.Abs(oncePlaySeasonScoreReward));
				}
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
			Debug.Log("GGMessageType.MessageACKModeResult ");
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
		else if (message.messageType == GGMessageType.MessageStrongholdTeamKillerIncrease)
		{
			if (message.messageContent.Team == GGTeamType.blue)
			{
				this.mGlobalInfo.modeInfo.mBlueResources++;
			}
			else if (message.messageContent.Team == GGTeamType.red)
			{
				this.mGlobalInfo.modeInfo.mRedResources++;
			}
		}
	}

	// Token: 0x060022F6 RID: 8950 RVA: 0x0010763C File Offset: 0x00105A3C
	private IEnumerator RunRoundResultCalculate()
	{
		this.roundOverChkStatusFlag = 1;
		if (this.mGlobalInfo.modeInfo.mBlueResources >= this.mGlobalInfo.modeInfo.mMaxResources && this.mGlobalInfo.modeInfo.mRedResources >= this.mGlobalInfo.modeInfo.mMaxResources)
		{
			int num = UnityEngine.Random.Range(1, 3);
			if (num != 1)
			{
				if (num == 2)
				{
					this.CalcRoundResultForStrongholdMode(GGTeamType.blue);
					this.winTeam = GGTeamType.blue;
				}
			}
			else
			{
				this.CalcRoundResultForStrongholdMode(GGTeamType.red);
				this.winTeam = GGTeamType.red;
			}
		}
		else if (this.mGlobalInfo.modeInfo.mBlueResources >= this.mGlobalInfo.modeInfo.mMaxResources)
		{
			this.CalcRoundResultForStrongholdMode(GGTeamType.blue);
			this.winTeam = GGTeamType.blue;
		}
		else if (this.mGlobalInfo.modeInfo.mRedResources >= this.mGlobalInfo.modeInfo.mMaxResources)
		{
			this.CalcRoundResultForStrongholdMode(GGTeamType.red);
			this.winTeam = GGTeamType.blue;
		}
		if (!this.isClacedResult)
		{
			this.RatingBroadcastAndVerifyForStrongholdMode();
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
			if (this.myNetworkCharacter.mPlayerProperties.isWinner)
			{
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictory, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictory));
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictorySeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictorySeason));
				if (isMvp)
				{
					GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeMvp, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeMvp));
					GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeMvpSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeMvpSeason));
				}
			}
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInStrongholdMode, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInStrongholdMode));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeJoin, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeJoin));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeJoinSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeJoinSeason));
			int coinAppend = (int)(this.myNetworkCharacter.mPlayerProperties.killNum - this.myNetworkCharacter.mPlayerProperties.deadNum);
			if (coinAppend <= 0)
			{
				coinAppend = 0;
			}
			GrowthManagerKit.AddCoins(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tStronghold, this.myNetworkCharacter.mPlayerProperties.rating) + coinAppend + 10);
			GrowthManagerKit.AddCharacterExp(GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tStronghold, this.myNetworkCharacter.mPlayerProperties.rating));
			GrowthManagerKit.AddHonorPoint(GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tStronghold, this.myNetworkCharacter.mPlayerProperties.rating));
			int seasonScore = GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tStronghold, this.myNetworkCharacter.mPlayerProperties.rating, this.myNetworkCharacter.mPlayerProperties.isWinner);
			if (seasonScore >= 0)
			{
				GrowthManagerKit.AddSeasonScore(seasonScore);
			}
			else
			{
				GrowthManagerKit.SubSeasonScore(Math.Abs(seasonScore));
			}
			UIPauseDirector.mInstance.PushResultPanel();
			this.isClacedResult = true;
		}
		this.roundOverChkStatusFlag = 2;
		GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().Reset();
		this.EnableNewRoundBroadcastAndVerifyForStrongholdMode();
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

	// Token: 0x060022F7 RID: 8951 RVA: 0x00107658 File Offset: 0x00105A58
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
			this.ActiveStartStrongholdButton();
			if (this.mGlobalInfo.modeInfo.IsStartStronghold)
			{
				this.ResourceCheck();
				this.CDTimerCount();
				this.GenerateHelicopter();
				if (this.mGlobalInfo.modeInfo.isStartStrongholdTimer)
				{
					this.CutDown5s();
				}
			}
			if (!this.isInRoudResultCalculating && this.StrongholdRoundOverChk())
			{
				this.mGlobalInfo.modeInfo.IsStartStronghold = false;
				base.StartCoroutine(this.RunRoundResultCalculate());
				this.isInRoudResultCalculating = true;
			}
		}
	}

	// Token: 0x060022F8 RID: 8952 RVA: 0x00107720 File Offset: 0x00105B20
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

	// Token: 0x060022F9 RID: 8953 RVA: 0x001078A0 File Offset: 0x00105CA0
	private void GenerateHelicopter()
	{
		if (Time.frameCount % 2500 == this.tmpHelicopterTimer)
		{
			int num = UnityEngine.Random.Range(1, 3);
			if (num == 1)
			{
				GGNetworkKit.mInstance.CreateSeceneObject("HelicopterPrefab", new Vector3((UnityEngine.Random.Range(0, 2) != 0) ? UnityEngine.Random.Range(200f, 120f) : UnityEngine.Random.Range(-100f, -120f), UnityEngine.Random.Range(25f, 40f), (UnityEngine.Random.Range(0, 2) != 0) ? UnityEngine.Random.Range(100f, 120f) : UnityEngine.Random.Range(-100f, -120f)), Quaternion.Euler(new Vector3(0f, UnityEngine.Random.Range(0f, 360f))));
			}
			else if (num == 2)
			{
				GGNetworkKit.mInstance.CreateSeceneObject("HelicopterPrefab", new Vector3((UnityEngine.Random.Range(0, 2) != 0) ? UnityEngine.Random.Range(200f, 120f) : UnityEngine.Random.Range(-100f, -120f), UnityEngine.Random.Range(25f, 40f), (UnityEngine.Random.Range(0, 2) != 0) ? UnityEngine.Random.Range(100f, 120f) : UnityEngine.Random.Range(-100f, -120f)), Quaternion.Euler(new Vector3(0f, UnityEngine.Random.Range(0f, 360f))));
				GGNetworkKit.mInstance.CreateSeceneObject("HelicopterPrefab", new Vector3((UnityEngine.Random.Range(0, 2) != 0) ? UnityEngine.Random.Range(200f, 120f) : UnityEngine.Random.Range(-100f, -120f), UnityEngine.Random.Range(25f, 40f), (UnityEngine.Random.Range(0, 2) != 0) ? UnityEngine.Random.Range(100f, 120f) : UnityEngine.Random.Range(-100f, -120f)), Quaternion.Euler(new Vector3(0f, UnityEngine.Random.Range(0f, 360f))));
			}
			this.tmpHelicopterTimer = UnityEngine.Random.Range(1500, 2100);
		}
	}

	// Token: 0x060022FA RID: 8954 RVA: 0x00107AD0 File Offset: 0x00105ED0
	private void CutDown5s()
	{
		this.CutDown5sTimer += Time.deltaTime;
		if (this.CutDown5sTimer >= 1f)
		{
			if (this.mGlobalInfo.modeInfo.StartStrongholdTimer > 0)
			{
				this.mGlobalInfo.modeInfo.StartStrongholdTimer--;
				this.CutDown5sTimer = 0f;
			}
			if (this.mGlobalInfo.modeInfo.StartStrongholdTimer == 0)
			{
				this.mGlobalInfo.modeInfo.isStartStrongholdTimer = false;
			}
		}
	}

	// Token: 0x060022FB RID: 8955 RVA: 0x00107B60 File Offset: 0x00105F60
	public void ActiveStartStrongholdButton()
	{
		this.StartStrongholdTimeCount += Time.deltaTime;
		if (this.StartStrongholdTimeCount >= 3f)
		{
			if (!this.isShowStartStrongholdButton && !this.mGlobalInfo.modeInfo.IsStartStronghold)
			{
				this.isShowStartStrongholdButton = true;
			}
			if (!this.isEnableStartStrongholdButton && GGNetworkKit.mInstance.GetPlayerPropertiesList().Count >= 4 && !this.mGlobalInfo.modeInfo.IsStartStronghold)
			{
				this.StartStronghold();
				this.isEnableStartStrongholdButton = true;
			}
			this.StartStrongholdTimeCount = 0f;
		}
	}

	// Token: 0x060022FC RID: 8956 RVA: 0x00107C03 File Offset: 0x00106003
	public void ShowStartStrongholdButton()
	{
		UIModeDirector.mInstance.ShowStartBtn();
	}

	// Token: 0x060022FD RID: 8957 RVA: 0x00107C0F File Offset: 0x0010600F
	public void EnableStartStrongholdButton()
	{
		UIModeDirector.mInstance.EnableStartBtn();
	}

	// Token: 0x060022FE RID: 8958 RVA: 0x00107C1B File Offset: 0x0010601B
	private void OnStartStrongholdMode()
	{
		this.StartStronghold();
	}

	// Token: 0x060022FF RID: 8959 RVA: 0x00107C23 File Offset: 0x00106023
	private void StartStronghold()
	{
		this.mGlobalInfo.modeInfo.IsStartStronghold = true;
		this.mGlobalInfo.modeInfo.isStartStrongholdTimer = true;
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x00107C47 File Offset: 0x00106047
	public void Reset()
	{
		base.gameObject.AddComponent<GGNetworkMode3>();
		UnityEngine.Object.DestroyImmediate(this);
	}

	// Token: 0x06002301 RID: 8961 RVA: 0x00107C5C File Offset: 0x0010605C
	private void ResourceCheck()
	{
		this.shTimeCount += Time.deltaTime;
		if (this.shTimeCount >= this.SH_MODE_CUT_RATE)
		{
			this.mGlobalInfo.modeInfo.mStrongholdTimer++;
			this.shTimeCount = 0f;
			if (this.mGlobalInfo.modeInfo.mStrongholdTimer == 20)
			{
				this.mGlobalInfo.modeInfo.mStronghold1State = GGStrondholdState.activate;
				this.mGlobalInfo.modeInfo.mStronghold2State = GGStrondholdState.activate;
				this.mGlobalInfo.modeInfo.mStronghold3State = GGStrondholdState.activate;
				GGNetworkSystemMessage ggnetworkSystemMessage = new GGNetworkSystemMessage();
				ggnetworkSystemMessage.content = " Stronghold activate !";
				GGNetworkChat.mInstance.SystemMessage(ggnetworkSystemMessage);
			}
			if (this.mGlobalInfo.modeInfo.mStrongholdTimer == 120)
			{
				if (this.mGlobalInfo.modeInfo.mStronghold1State == GGStrondholdState.BlueOccupation)
				{
					if (this.mGlobalInfo.modeInfo.mBlueResources < this.mGlobalInfo.modeInfo.mMaxResources)
					{
						this.mGlobalInfo.modeInfo.mBlueResources += 50;
						if (this.mGlobalInfo.modeInfo.mBlueResources > this.mGlobalInfo.modeInfo.mMaxResources)
						{
							this.mGlobalInfo.modeInfo.mBlueResources = this.mGlobalInfo.modeInfo.mMaxResources;
						}
					}
				}
				else if (this.mGlobalInfo.modeInfo.mStronghold1State == GGStrondholdState.RedOccupation && this.mGlobalInfo.modeInfo.mRedResources < this.mGlobalInfo.modeInfo.mMaxResources)
				{
					this.mGlobalInfo.modeInfo.mRedResources += 50;
					if (this.mGlobalInfo.modeInfo.mRedResources > this.mGlobalInfo.modeInfo.mMaxResources)
					{
						this.mGlobalInfo.modeInfo.mRedResources = this.mGlobalInfo.modeInfo.mMaxResources;
					}
				}
				if (this.mGlobalInfo.modeInfo.mStronghold2State == GGStrondholdState.BlueOccupation)
				{
					if (this.mGlobalInfo.modeInfo.mBlueResources < this.mGlobalInfo.modeInfo.mMaxResources)
					{
						this.mGlobalInfo.modeInfo.mBlueResources += 50;
						if (this.mGlobalInfo.modeInfo.mBlueResources > this.mGlobalInfo.modeInfo.mMaxResources)
						{
							this.mGlobalInfo.modeInfo.mBlueResources = this.mGlobalInfo.modeInfo.mMaxResources;
						}
					}
				}
				else if (this.mGlobalInfo.modeInfo.mStronghold2State == GGStrondholdState.RedOccupation && this.mGlobalInfo.modeInfo.mRedResources < this.mGlobalInfo.modeInfo.mMaxResources)
				{
					this.mGlobalInfo.modeInfo.mRedResources += 50;
					if (this.mGlobalInfo.modeInfo.mRedResources > this.mGlobalInfo.modeInfo.mMaxResources)
					{
						this.mGlobalInfo.modeInfo.mRedResources = this.mGlobalInfo.modeInfo.mMaxResources;
					}
				}
				if (this.mGlobalInfo.modeInfo.mStronghold3State == GGStrondholdState.BlueOccupation)
				{
					if (this.mGlobalInfo.modeInfo.mBlueResources < this.mGlobalInfo.modeInfo.mMaxResources)
					{
						this.mGlobalInfo.modeInfo.mBlueResources += 50;
						if (this.mGlobalInfo.modeInfo.mBlueResources > this.mGlobalInfo.modeInfo.mMaxResources)
						{
							this.mGlobalInfo.modeInfo.mBlueResources = this.mGlobalInfo.modeInfo.mMaxResources;
						}
					}
				}
				else if (this.mGlobalInfo.modeInfo.mStronghold3State == GGStrondholdState.RedOccupation && this.mGlobalInfo.modeInfo.mRedResources < this.mGlobalInfo.modeInfo.mMaxResources)
				{
					this.mGlobalInfo.modeInfo.mRedResources += 50;
					if (this.mGlobalInfo.modeInfo.mRedResources > this.mGlobalInfo.modeInfo.mMaxResources)
					{
						this.mGlobalInfo.modeInfo.mRedResources = this.mGlobalInfo.modeInfo.mMaxResources;
					}
				}
				this.mGlobalInfo.modeInfo.mStrongholdTimer = 0;
				this.mGlobalInfo.modeInfo.mStronghold1State = GGStrondholdState.unactivate;
				this.mGlobalInfo.modeInfo.mStronghold2State = GGStrondholdState.unactivate;
				this.mGlobalInfo.modeInfo.mStronghold3State = GGStrondholdState.unactivate;
				GGNetworkSystemMessage ggnetworkSystemMessage2 = new GGNetworkSystemMessage();
				ggnetworkSystemMessage2.content = " Stronghold de-activate !";
				GGNetworkChat.mInstance.SystemMessage(ggnetworkSystemMessage2);
			}
		}
	}

	// Token: 0x06002302 RID: 8962 RVA: 0x00108130 File Offset: 0x00106530
	public void CDTimerCount()
	{
		if (this.mGlobalInfo.modeInfo.mStronghold1CD)
		{
			this.sh1CDTimeCount += Time.deltaTime;
			if (this.sh1CDTimeCount >= 1f)
			{
				this.mGlobalInfo.modeInfo.mStronghold1CDTimer++;
				this.sh1CDTimeCount = 0f;
			}
			if (this.mGlobalInfo.modeInfo.mStronghold1CDTimer == 5)
			{
				this.mGlobalInfo.modeInfo.mStronghold1CD = false;
				this.mGlobalInfo.modeInfo.mStronghold1CDTimer = 0;
			}
		}
		if (this.mGlobalInfo.modeInfo.mStronghold2CD)
		{
			this.sh2CDTimeCount += Time.deltaTime;
			if (this.sh2CDTimeCount >= 1f)
			{
				this.mGlobalInfo.modeInfo.mStronghold2CDTimer++;
				this.sh2CDTimeCount = 0f;
			}
			if (this.mGlobalInfo.modeInfo.mStronghold2CDTimer == 5)
			{
				this.mGlobalInfo.modeInfo.mStronghold2CD = false;
				this.mGlobalInfo.modeInfo.mStronghold2CDTimer = 0;
			}
		}
		if (this.mGlobalInfo.modeInfo.mStronghold3CD)
		{
			this.sh3CDTimeCount += Time.deltaTime;
			if (this.sh3CDTimeCount >= 1f)
			{
				this.mGlobalInfo.modeInfo.mStronghold3CDTimer++;
				this.sh3CDTimeCount = 0f;
			}
			if (this.mGlobalInfo.modeInfo.mStronghold3CDTimer == 5)
			{
				this.mGlobalInfo.modeInfo.mStronghold3CD = false;
				this.mGlobalInfo.modeInfo.mStronghold3CDTimer = 0;
			}
		}
	}

	// Token: 0x06002303 RID: 8963 RVA: 0x001082F4 File Offset: 0x001066F4
	public bool StrongholdRoundOverChk()
	{
		return this.mGlobalInfo.modeInfo.mRedResources >= this.mGlobalInfo.modeInfo.mMaxResources || this.mGlobalInfo.modeInfo.mBlueResources >= this.mGlobalInfo.modeInfo.mMaxResources;
	}

	// Token: 0x06002304 RID: 8964 RVA: 0x0010834E File Offset: 0x0010674E
	private void Update()
	{
		this.InitMyNetworkCharacter();
		this.ModeLogic();
	}

	// Token: 0x06002305 RID: 8965 RVA: 0x0010835C File Offset: 0x0010675C
	public void CalcRoundResultForStrongholdMode(GGTeamType winTeam)
	{
		List<GGNetworkPlayerProperties> list = new List<GGNetworkPlayerProperties>(GGNetworkKit.mInstance.GetPlayerPropertiesList().ToArray());
		for (int i = 0; i < list.Count; i++)
		{
			list[i].participation = list[i].damageNum / 200 * 2 + list[i].bedamageNum / 200;
			list[i].StrongholdScore = list[i].killNum * 3 - list[i].deadNum + list[i].headshotNum * 3 + list[i].maxKillNum * 5 + list[i].participation + list[i].strongholdGetNum * 10;
			list[i].StrongholdScore = Math.Max(list[i].StrongholdScore, 0);
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
						if (list[j].StrongholdScore > this.blueRankInfoList[k].StrongholdScore)
						{
							this.blueRankInfoList.Insert(k, list[j]);
							break;
						}
						if (list[j].StrongholdScore == this.blueRankInfoList[k].StrongholdScore && list[j].id > this.blueRankInfoList[k].id)
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
						if (list[j].StrongholdScore > this.redRankInfoList[l].StrongholdScore)
						{
							this.redRankInfoList.Insert(l, list[j]);
							break;
						}
						if (list[j].StrongholdScore == this.redRankInfoList[l].StrongholdScore && list[j].id > this.redRankInfoList[l].id)
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
					if (list[j].StrongholdScore > list2[m].StrongholdScore)
					{
						list2.Insert(m, list[j]);
						break;
					}
					if (list[j].StrongholdScore == list2[m].StrongholdScore && list[j].id > list2[m].id)
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
			GameValueForRatingCalc gameValueForRatingCalc = new GameValueForRatingCalc(GrowthGameModeTag.tStronghold);
			gameValueForRatingCalc.mShModeValue.DeadNum = (int)this.blueRankInfoList[n].deadNum;
			gameValueForRatingCalc.mShModeValue.enemyTeamTotalPlayerNum = this.redRankInfoList.Count;
			gameValueForRatingCalc.mShModeValue.isWinner = (winTeam == this.blueRankInfoList[n].team);
			gameValueForRatingCalc.mShModeValue.killNum = (int)this.blueRankInfoList[n].killNum;
			gameValueForRatingCalc.mShModeValue.myTeamTotalPlayerNum = this.blueRankInfoList.Count;
			gameValueForRatingCalc.mShModeValue.rankInMyTeam = n + 1;
			gameValueForRatingCalc.allRankInfoList = list2;
			gameValueForRatingCalc.rankInAll = list2.IndexOf(this.blueRankInfoList[n]) + 1;
			gameValueForRatingCalc.score = (int)this.blueRankInfoList[n].StrongholdScore;
			this.blueRankInfoList[n].rating = GrowthManagerKit.GetOncePlayRating(gameValueForRatingCalc);
			this.blueRankInfoList[n].isWinner = gameValueForRatingCalc.mShModeValue.isWinner;
			int num = gameValueForRatingCalc.mShModeValue.killNum - gameValueForRatingCalc.mShModeValue.DeadNum;
			if (num <= 0)
			{
				num = 0;
			}
			this.blueRankInfoList[n].coinAdd = (short)(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tStronghold, this.blueRankInfoList[n].rating) + num + 10);
			this.blueRankInfoList[n].expAdd = (short)GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tStronghold, this.blueRankInfoList[n].rating);
			this.blueRankInfoList[n].honorPointAdd = (short)GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tStronghold, this.blueRankInfoList[n].rating);
			this.blueRankInfoList[n].seasonScoreAdd = (short)GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tStronghold, this.blueRankInfoList[n].rating, this.blueRankInfoList[n].isWinner);
		}
		for (int num2 = 0; num2 < this.redRankInfoList.Count; num2++)
		{
			GameValueForRatingCalc gameValueForRatingCalc2 = new GameValueForRatingCalc(GrowthGameModeTag.tStronghold);
			gameValueForRatingCalc2.mShModeValue.DeadNum = (int)this.redRankInfoList[num2].deadNum;
			gameValueForRatingCalc2.mShModeValue.enemyTeamTotalPlayerNum = this.blueRankInfoList.Count;
			gameValueForRatingCalc2.mShModeValue.isWinner = (winTeam == this.redRankInfoList[num2].team);
			gameValueForRatingCalc2.mShModeValue.killNum = (int)this.redRankInfoList[num2].killNum;
			gameValueForRatingCalc2.mShModeValue.myTeamTotalPlayerNum = this.redRankInfoList.Count;
			gameValueForRatingCalc2.mShModeValue.rankInMyTeam = num2 + 1;
			gameValueForRatingCalc2.allRankInfoList = list2;
			gameValueForRatingCalc2.rankInAll = list2.IndexOf(this.redRankInfoList[num2]) + 1;
			gameValueForRatingCalc2.score = (int)this.redRankInfoList[num2].StrongholdScore;
			this.redRankInfoList[num2].rating = GrowthManagerKit.GetOncePlayRating(gameValueForRatingCalc2);
			this.redRankInfoList[num2].isWinner = gameValueForRatingCalc2.mShModeValue.isWinner;
			int num3 = gameValueForRatingCalc2.mShModeValue.killNum - gameValueForRatingCalc2.mShModeValue.DeadNum;
			if (num3 <= 0)
			{
				num3 = 0;
			}
			this.redRankInfoList[num2].coinAdd = (short)(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tStronghold, this.redRankInfoList[num2].rating) + num3 + 10);
			this.redRankInfoList[num2].expAdd = (short)GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tStronghold, this.redRankInfoList[num2].rating);
			this.redRankInfoList[num2].honorPointAdd = (short)GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tStronghold, this.redRankInfoList[num2].rating);
			this.redRankInfoList[num2].seasonScoreAdd = (short)GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tStronghold, this.redRankInfoList[num2].rating, this.redRankInfoList[num2].isWinner);
		}
	}

	// Token: 0x06002306 RID: 8966 RVA: 0x00108BB0 File Offset: 0x00106FB0
	public void RatingBroadcastAndVerifyForStrongholdMode()
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

	// Token: 0x06002307 RID: 8967 RVA: 0x00108C58 File Offset: 0x00107058
	public void EnableNewRoundBroadcastAndVerifyForStrongholdMode()
	{
		this.GetPlayerIdsSnapshot();
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageNewRound;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.Others);
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x00108C84 File Offset: 0x00107084
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

	// Token: 0x06002309 RID: 8969 RVA: 0x00108CDC File Offset: 0x001070DC
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

	// Token: 0x0600230A RID: 8970 RVA: 0x00108DF8 File Offset: 0x001071F8
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

	// Token: 0x040023A4 RID: 9124
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x040023A5 RID: 9125
	private bool mReceiveAllACK;

	// Token: 0x040023A6 RID: 9126
	private List<int> mAllplayersID = new List<int>();

	// Token: 0x040023A7 RID: 9127
	private List<int> mTmpAllplayersID = new List<int>();

	// Token: 0x040023A8 RID: 9128
	private bool mReceiveAllActiveNewRound;

	// Token: 0x040023A9 RID: 9129
	private int mACKResultNum;

	// Token: 0x040023AA RID: 9130
	private int mACkActiveNewRoundNum;

	// Token: 0x040023AB RID: 9131
	public readonly float SH_MODE_CUT_RATE = 1f;

	// Token: 0x040023AC RID: 9132
	public float shTimeCount;

	// Token: 0x040023AD RID: 9133
	private float CalcPlayerNumTimeCount;

	// Token: 0x040023AE RID: 9134
	private List<GGNetworkPlayerProperties> blueRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x040023AF RID: 9135
	private List<GGNetworkPlayerProperties> redRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x040023B0 RID: 9136
	private GGTeamType winTeam = GGTeamType.Nil;

	// Token: 0x040023B1 RID: 9137
	private int roundOverChkStatusFlag;

	// Token: 0x040023B2 RID: 9138
	public bool isClacedResult;

	// Token: 0x040023B3 RID: 9139
	private float sh1CDTimeCount;

	// Token: 0x040023B4 RID: 9140
	private float sh2CDTimeCount;

	// Token: 0x040023B5 RID: 9141
	private float sh3CDTimeCount;

	// Token: 0x040023B6 RID: 9142
	private float StartStrongholdTimeCount;

	// Token: 0x040023B7 RID: 9143
	private bool isShowStartStrongholdButton;

	// Token: 0x040023B8 RID: 9144
	private bool isEnableStartStrongholdButton;

	// Token: 0x040023B9 RID: 9145
	private float CutDown5sTimer;

	// Token: 0x040023BA RID: 9146
	private int tmpHelicopterTimer = 1800;

	// Token: 0x040023BB RID: 9147
	private GGNetworkCharacter myNetworkCharacter;

	// Token: 0x040023BC RID: 9148
	public bool isInRoudResultCalculating;
}
