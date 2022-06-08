using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004E7 RID: 1255
public class GGNetworkMode4 : MonoBehaviour
{
	// Token: 0x0600230C RID: 8972 RVA: 0x001096D5 File Offset: 0x00107AD5
	private void Awake()
	{
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x001096D8 File Offset: 0x00107AD8
	private void InitMyNetworkCharacter()
	{
		if (this.myNetworkCharacter == null)
		{
			GameObject gameObject = GameObject.FindWithTag("Player");
			if (gameObject != null)
			{
				this.myNetworkCharacter = gameObject.GetComponent<GGNetworkCharacter>();
				this.myMainCamera = gameObject.transform.Find("LookObject/Main Camera");
			}
		}
	}

	// Token: 0x0600230E RID: 8974 RVA: 0x00109730 File Offset: 0x00107B30
	private void Start()
	{
		this.bombPositionNum = GameObject.FindWithTag("TimerBombPosition").transform.childCount;
		this.TimerBombSound = GameObject.FindWithTag("TimerBombPosition").GetComponent<AudioSource>();
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		GGNetworkKit.mInstance.MessageOk += this.Event_MessageOK;
		this.explosionRoundOver1 = false;
		this.explosionRoundOver2 = false;
		this.InitMyNetworkCharacter();
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x001097A6 File Offset: 0x00107BA6
	private void OnDisable()
	{
		GGNetworkKit.mInstance.MessageOk -= this.Event_MessageOK;
	}

	// Token: 0x06002310 RID: 8976 RVA: 0x001097C0 File Offset: 0x00107BC0
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
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictory, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictory));
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictorySeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictorySeason));
				if (flag)
				{
					GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeMvp, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeMvp));
					GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeMvpSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeMvpSeason));
				}
			}
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInExplosionMode, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInExplosionMode));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeJoin, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeJoin));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeJoinSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeJoinSeason));
			int num = (int)(this.myNetworkCharacter.mPlayerProperties.killNum - this.myNetworkCharacter.mPlayerProperties.deadNum);
			if (num <= 0)
			{
				num = 0;
			}
			if (!this.isClacedResult)
			{
				GrowthManagerKit.AddCoins(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tExplosion, this.myNetworkCharacter.mPlayerProperties.rating) + num);
				GrowthManagerKit.AddCharacterExp(GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tExplosion, this.myNetworkCharacter.mPlayerProperties.rating));
				GrowthManagerKit.AddHonorPoint(GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tExplosion, this.myNetworkCharacter.mPlayerProperties.rating));
				int oncePlaySeasonScoreReward = GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tExplosion, this.myNetworkCharacter.mPlayerProperties.rating, this.myNetworkCharacter.mPlayerProperties.isWinner);
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
	}

	// Token: 0x06002311 RID: 8977 RVA: 0x00109CB0 File Offset: 0x001080B0
	private IEnumerator RunRoundResultCalculate()
	{
		this.roundOverChkStatusFlag = 1;
		if (this.mGlobalInfo.modeInfo.RedTeamWinNum == this.teamWinRoundNum || GGNetworkKit.mInstance.GetBluePlayerPropertiesList().Count == 0)
		{
			this.CalcRoundResultForExplosionMode(GGTeamType.red);
			this.winTeam = GGTeamType.red;
		}
		if (this.mGlobalInfo.modeInfo.BlueTeamWinNum == this.teamWinRoundNum || GGNetworkKit.mInstance.GetRedPlayerPropertiesList().Count == 0)
		{
			this.CalcRoundResultForExplosionMode(GGTeamType.blue);
			this.winTeam = GGTeamType.blue;
		}
		this.RatingBroadcastAndVerifyForExplosionMode();
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
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictory, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictory));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictorySeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictorySeason));
			if (isMvp)
			{
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeMvp, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeMvp));
				GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeMvpSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeMvpSeason));
			}
		}
		GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInExplosionMode, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInExplosionMode));
		GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeJoin, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeJoin));
		GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeJoinSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeJoinSeason));
		int coinAppend = (int)(this.myNetworkCharacter.mPlayerProperties.killNum - this.myNetworkCharacter.mPlayerProperties.deadNum);
		if (coinAppend <= 0)
		{
			coinAppend = 0;
		}
		GrowthManagerKit.AddCoins(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tExplosion, this.myNetworkCharacter.mPlayerProperties.rating) + coinAppend);
		GrowthManagerKit.AddCharacterExp(GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tExplosion, this.myNetworkCharacter.mPlayerProperties.rating));
		GrowthManagerKit.AddHonorPoint(GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tExplosion, this.myNetworkCharacter.mPlayerProperties.rating));
		int seasonScore = GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tExplosion, this.myNetworkCharacter.mPlayerProperties.rating, this.myNetworkCharacter.mPlayerProperties.isWinner);
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
		this.roundOverChkStatusFlag = 2;
		GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().Reset();
		this.EnableNewRoundBroadcastAndVerifyForExplosionMode();
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

	// Token: 0x06002312 RID: 8978 RVA: 0x00109CCC File Offset: 0x001080CC
	public void ModeLogic()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
		if (this.mGlobalInfo == null)
		{
			return;
		}
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			this.CalcBlueAndRedPlayerNum();
			if (!this.mGlobalInfo.modeInfo.singleRoundResultCalc)
			{
				if (this.mGlobalInfo.modeInfo.RoundNum > 0)
				{
					if (!this.ExplosionNextRoundChk())
					{
						this.ActiveStartExplosionButton();
						if (this.mGlobalInfo.modeInfo.isStartExplosionTimer)
						{
							this.CutDown5s();
						}
						if (this.mGlobalInfo.modeInfo.IsStartExplosion)
						{
							this.mGlobalInfo.modeInfo.BlueLivePlayerNum = this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerSurvivalNum;
							this.mGlobalInfo.modeInfo.RedLivePlayerNum = this.mGlobalInfo.modeInfo.AllModeRedTeamPlayerSurvivalNum;
							this.TimerCheck();
						}
					}
					else
					{
						this.mGlobalInfo.modeInfo.singleRoundResultCalc = true;
						if (this.mGlobalInfo.modeInfo.singleRoundResultCalc)
						{
							if (this.mGlobalInfo.modeInfo.BlueLivePlayerNum == 0 || this.mGlobalInfo.modeInfo.explosionTimer == 0)
							{
								this.mGlobalInfo.modeInfo.RedTeamWinNum++;
								this.SendSingleRoundWinMessage(2);
							}
							else if (this.mGlobalInfo.modeInfo.IsTimerBombUninstall || (!this.mGlobalInfo.modeInfo.IsTimerBombInstalled && this.mGlobalInfo.modeInfo.RedLivePlayerNum == 0) || this.mGlobalInfo.modeInfo.totalTimer == 0)
							{
								this.mGlobalInfo.modeInfo.BlueTeamWinNum++;
								this.SendSingleRoundWinMessage(1);
							}
						}
					}
				}
			}
			else if (this.mGlobalInfo.modeInfo.singleRoundResultCalc)
			{
				this.nextRoundTimeCount += Time.deltaTime;
				if (this.nextRoundTimeCount >= 3f)
				{
					this.nextRoundTimeCount = 0f;
					this.mGlobalInfo.modeInfo.singleRoundResultCalc = false;
					this.ResetExplosionSingleRoundData();
					GameObject[] array = GameObject.FindGameObjectsWithTag("ExplosionModeTimerBomb");
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							GGNetworkKit.mInstance.DestroySceneObjectRPC(array[i]);
						}
					}
					if (!this.ExplosionAllRoundOverChk())
					{
						this.mGlobalInfo.modeInfo.RoundNum++;
						if (this.mGlobalInfo.modeInfo.RoundNum <= this.maxRoundNum)
						{
							this.SendNextRoundMessage();
						}
					}
					else
					{
						this.mGlobalInfo.modeInfo.RoundNum = 0;
						base.StartCoroutine(this.RunRoundResultCalculate());
						this.isInRoudResultCalculating = true;
					}
				}
			}
		}
	}

	// Token: 0x06002313 RID: 8979 RVA: 0x00109FB8 File Offset: 0x001083B8
	private void CalcBlueAndRedPlayerNum()
	{
		this.CalcPlayerNumTimeCount += Time.deltaTime;
		if (this.CalcPlayerNumTimeCount >= 1f)
		{
			List<GGNetworkPlayerProperties> bluePlayerPropertiesList = GGNetworkKit.mInstance.GetBluePlayerPropertiesList();
			if (bluePlayerPropertiesList != null)
			{
				this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerSurvivalNum = bluePlayerPropertiesList.Count;
				this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerTotalNum = bluePlayerPropertiesList.Count;
				for (int i = 0; i < bluePlayerPropertiesList.Count; i++)
				{
					if (bluePlayerPropertiesList[i].isObserver)
					{
						this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerSurvivalNum--;
					}
				}
			}
			else
			{
				this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerSurvivalNum = 0;
			}
			List<GGNetworkPlayerProperties> redPlayerPropertiesList = GGNetworkKit.mInstance.GetRedPlayerPropertiesList();
			if (redPlayerPropertiesList != null)
			{
				this.mGlobalInfo.modeInfo.AllModeRedTeamPlayerSurvivalNum = redPlayerPropertiesList.Count;
				this.mGlobalInfo.modeInfo.AllModeRedTeamPlayerTotalNum = redPlayerPropertiesList.Count;
				for (int j = 0; j < redPlayerPropertiesList.Count; j++)
				{
					if (redPlayerPropertiesList[j].isObserver)
					{
						this.mGlobalInfo.modeInfo.AllModeRedTeamPlayerSurvivalNum--;
					}
				}
			}
			else
			{
				this.mGlobalInfo.modeInfo.AllModeRedTeamPlayerSurvivalNum = 0;
			}
			this.CalcPlayerNumTimeCount = 0f;
		}
	}

	// Token: 0x06002314 RID: 8980 RVA: 0x0010A118 File Offset: 0x00108518
	private void CutDown5s()
	{
		this.CutDown5sTimer += Time.deltaTime;
		if (this.CutDown5sTimer >= 1f)
		{
			if (this.mGlobalInfo.modeInfo.StartExplosionTimer > 0)
			{
				this.mGlobalInfo.modeInfo.StartExplosionTimer--;
				this.CutDown5sTimer = 0f;
			}
			if (this.mGlobalInfo.modeInfo.StartExplosionTimer == 0)
			{
				this.mGlobalInfo.modeInfo.isStartExplosionTimer = false;
				this.SendActiveTimerBombMessage();
			}
		}
	}

	// Token: 0x06002315 RID: 8981 RVA: 0x0010A1AC File Offset: 0x001085AC
	public void SendActiveTimerBombMessage()
	{
		List<GGNetworkPlayerProperties> redPlayerPropertiesList = GGNetworkKit.mInstance.GetRedPlayerPropertiesList();
		int index = UnityEngine.Random.Range(0, redPlayerPropertiesList.Count);
		int id = redPlayerPropertiesList[index].id;
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessagePlayerActiveTimerBomb;
		GGNetworkKit.mInstance.SendMessage(ggmessage, id);
	}

	// Token: 0x06002316 RID: 8982 RVA: 0x0010A1F8 File Offset: 0x001085F8
	public void ActiveStartExplosionButton()
	{
		if (!this.isShowStartExplosionButton && !this.mGlobalInfo.modeInfo.IsStartExplosion)
		{
			this.isShowStartExplosionButton = true;
		}
		if (!this.isEnableStartExplosionButton && !this.mGlobalInfo.modeInfo.IsStartExplosion)
		{
			if (this.mGlobalInfo.modeInfo.RoundNum == 1)
			{
				if (this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerSurvivalNum >= 2 && this.mGlobalInfo.modeInfo.AllModeRedTeamPlayerSurvivalNum >= 2)
				{
					this.isEnableStartExplosionButton = true;
					this.StartExplosion();
				}
			}
			else if (this.mGlobalInfo.modeInfo.RoundNum <= this.maxRoundNum && this.mGlobalInfo.modeInfo.RoundNum > 1 && this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerSurvivalNum >= 1 && this.mGlobalInfo.modeInfo.AllModeRedTeamPlayerSurvivalNum >= 1)
			{
				this.isEnableStartExplosionButton = true;
				this.StartExplosion();
			}
		}
	}

	// Token: 0x06002317 RID: 8983 RVA: 0x0010A30A File Offset: 0x0010870A
	public void ShowStartExplosionButton()
	{
		UIModeDirector.mInstance.ShowStartBtn();
	}

	// Token: 0x06002318 RID: 8984 RVA: 0x0010A316 File Offset: 0x00108716
	public void EnableStartExplosionButton()
	{
		UIModeDirector.mInstance.EnableStartBtn();
	}

	// Token: 0x06002319 RID: 8985 RVA: 0x0010A322 File Offset: 0x00108722
	private void OnStartExplosionMode()
	{
		this.StartExplosion();
	}

	// Token: 0x0600231A RID: 8986 RVA: 0x0010A32A File Offset: 0x0010872A
	private void StartExplosion()
	{
		this.mGlobalInfo.modeInfo.IsStartExplosion = true;
		this.mGlobalInfo.modeInfo.isStartExplosionTimer = true;
	}

	// Token: 0x0600231B RID: 8987 RVA: 0x0010A350 File Offset: 0x00108750
	private void ResetExplosionSingleRoundData()
	{
		this.isEnableStartExplosionButton = false;
		this.mGlobalInfo.modeInfo.IsTimerBombInstalled = false;
		this.mGlobalInfo.modeInfo.explosionTimer = 50;
		this.mGlobalInfo.modeInfo.totalTimer = 180;
		this.mGlobalInfo.modeInfo.bombPositionId = 0;
		this.mGlobalInfo.modeInfo.activeTimerBomb = false;
		this.mGlobalInfo.modeInfo.isStartExplosionTimer = false;
		this.mGlobalInfo.modeInfo.StartExplosionTimer = 6;
		this.mGlobalInfo.modeInfo.IsStartExplosion = false;
		this.mGlobalInfo.modeInfo.ExplosionModeNewPlayerJoinGameTimer = 20;
		this.mGlobalInfo.modeInfo.BlueLivePlayerNum = -1;
		this.mGlobalInfo.modeInfo.RedLivePlayerNum = -1;
		this.mGlobalInfo.modeInfo.IsTimerBombUninstall = false;
		this.mGlobalInfo.modeInfo.TimerBombPositionX = 0f;
		this.mGlobalInfo.modeInfo.TimerBombPositionY = -200f;
		this.mGlobalInfo.modeInfo.TimerBombPositionZ = 0f;
	}

	// Token: 0x0600231C RID: 8988 RVA: 0x0010A475 File Offset: 0x00108875
	public void Reset()
	{
		base.gameObject.AddComponent<GGNetworkMode4>();
		UnityEngine.Object.DestroyImmediate(this);
	}

	// Token: 0x0600231D RID: 8989 RVA: 0x0010A48C File Offset: 0x0010888C
	private void TimerCheck()
	{
		this.ExplosionTimeCount += Time.deltaTime;
		if (this.ExplosionTimeCount >= this.EP_MODE_CUT_RATE)
		{
			if (this.mGlobalInfo.modeInfo.IsTimerBombInstalled && this.mGlobalInfo.modeInfo.explosionTimer > 0)
			{
				this.mGlobalInfo.modeInfo.explosionTimer--;
			}
			if (this.mGlobalInfo.modeInfo.totalTimer > 0)
			{
				this.mGlobalInfo.modeInfo.totalTimer--;
			}
			if (this.mGlobalInfo.modeInfo.ExplosionModeNewPlayerJoinGameTimer > 0)
			{
				this.mGlobalInfo.modeInfo.ExplosionModeNewPlayerJoinGameTimer--;
			}
			this.ExplosionTimeCount = 0f;
		}
	}

	// Token: 0x0600231E RID: 8990 RVA: 0x0010A568 File Offset: 0x00108968
	public void TimerBombExplosionCheck()
	{
		if (this.mGlobalInfo.modeInfo.explosionTimer > 0 && this.mGlobalInfo.modeInfo.totalTimer > 0)
		{
			this.flag = true;
		}
		if (!this.flag)
		{
			return;
		}
		if (!this.explosionRoundOver1)
		{
			if (this.mGlobalInfo.modeInfo.explosionTimer == 0)
			{
				this.explosionRoundOver1 = true;
				if (this.myNetworkCharacter != null)
				{
					this.myMainCamera.GetComponent<Animation>().Play("cameraShake");
				}
				base.StartCoroutine(this.SetExplosionRoundOver(2));
			}
			else if (this.mGlobalInfo.modeInfo.totalTimer == 0)
			{
				this.explosionRoundOver1 = true;
				this.explosionRoundOver2 = true;
			}
		}
	}

	// Token: 0x0600231F RID: 8991 RVA: 0x0010A638 File Offset: 0x00108A38
	private IEnumerator SetExplosionRoundOver(int delay)
	{
		yield return new WaitForSeconds((float)delay);
		this.explosionRoundOver2 = true;
		yield break;
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x0010A65C File Offset: 0x00108A5C
	public bool ExplosionNextRoundChk()
	{
		return this.mGlobalInfo.modeInfo.IsStartExplosion && (this.mGlobalInfo.modeInfo.BlueLivePlayerNum == 0 || this.mGlobalInfo.modeInfo.explosionTimer == 0 || this.mGlobalInfo.modeInfo.IsTimerBombUninstall || (!this.mGlobalInfo.modeInfo.IsTimerBombInstalled && this.mGlobalInfo.modeInfo.RedLivePlayerNum == 0) || this.mGlobalInfo.modeInfo.totalTimer == 0);
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x0010A708 File Offset: 0x00108B08
	public bool ExplosionAllRoundOverChk()
	{
		return (this.mGlobalInfo.modeInfo.RedTeamWinNum == this.teamWinRoundNum || this.mGlobalInfo.modeInfo.BlueTeamWinNum == this.teamWinRoundNum || GGNetworkKit.mInstance.GetBluePlayerPropertiesList().Count == 0 || GGNetworkKit.mInstance.GetRedPlayerPropertiesList().Count == 0) && !this.isInRoudResultCalculating;
	}

	// Token: 0x06002322 RID: 8994 RVA: 0x0010A781 File Offset: 0x00108B81
	private void Update()
	{
		this.InitMyNetworkCharacter();
		this.ModeLogic();
	}

	// Token: 0x06002323 RID: 8995 RVA: 0x0010A790 File Offset: 0x00108B90
	public void CalcRoundResultForExplosionMode(GGTeamType winTeam)
	{
		List<GGNetworkPlayerProperties> list = new List<GGNetworkPlayerProperties>(GGNetworkKit.mInstance.GetPlayerPropertiesList().ToArray());
		for (int i = 0; i < list.Count; i++)
		{
			list[i].participation = list[i].damageNum / 200 * 2 + list[i].bedamageNum / 200;
			list[i].ExplosionScore = list[i].killNum * 3 - list[i].deadNum + list[i].headshotNum * 3 + list[i].maxKillNum * 5 + list[i].participation + list[i].installbombNum * 10 + list[i].unInstallbombNum * 10;
			list[i].ExplosionScore = Math.Max(list[i].ExplosionScore, 0);
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
						if (list[j].ExplosionScore > this.blueRankInfoList[k].ExplosionScore)
						{
							this.blueRankInfoList.Insert(k, list[j]);
							break;
						}
						if (list[j].ExplosionScore == this.blueRankInfoList[k].ExplosionScore && list[j].id > this.blueRankInfoList[k].id)
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
						if (list[j].ExplosionScore > this.redRankInfoList[l].ExplosionScore)
						{
							this.redRankInfoList.Insert(l, list[j]);
							break;
						}
						if (list[j].ExplosionScore == this.redRankInfoList[l].ExplosionScore && list[j].id > this.redRankInfoList[l].id)
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
					if (list[j].ExplosionScore > list2[m].ExplosionScore)
					{
						list2.Insert(m, list[j]);
						break;
					}
					if (list[j].ExplosionScore == list2[m].ExplosionScore && list[j].id > list2[m].id)
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
			GameValueForRatingCalc gameValueForRatingCalc = new GameValueForRatingCalc(GrowthGameModeTag.tExplosion);
			gameValueForRatingCalc.mEPModeValue.DeadNum = (int)this.blueRankInfoList[n].deadNum;
			gameValueForRatingCalc.mEPModeValue.enemyTeamTotalPlayerNum = this.redRankInfoList.Count;
			gameValueForRatingCalc.mEPModeValue.isWinner = (winTeam == this.blueRankInfoList[n].team);
			gameValueForRatingCalc.mEPModeValue.killNum = (int)this.blueRankInfoList[n].killNum;
			gameValueForRatingCalc.mEPModeValue.myTeamTotalPlayerNum = this.blueRankInfoList.Count;
			gameValueForRatingCalc.mEPModeValue.rankInMyTeam = n + 1;
			gameValueForRatingCalc.allRankInfoList = list2;
			gameValueForRatingCalc.rankInAll = list2.IndexOf(this.blueRankInfoList[n]) + 1;
			gameValueForRatingCalc.score = (int)this.blueRankInfoList[n].ExplosionScore;
			this.blueRankInfoList[n].rating = GrowthManagerKit.GetOncePlayRating(gameValueForRatingCalc);
			this.blueRankInfoList[n].isWinner = gameValueForRatingCalc.mEPModeValue.isWinner;
			int num = gameValueForRatingCalc.mEPModeValue.killNum - gameValueForRatingCalc.mEPModeValue.DeadNum;
			if (num <= 0)
			{
				num = 0;
			}
			this.blueRankInfoList[n].coinAdd = (short)(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tExplosion, this.blueRankInfoList[n].rating) + num);
			this.blueRankInfoList[n].expAdd = (short)GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tExplosion, this.blueRankInfoList[n].rating);
			this.blueRankInfoList[n].honorPointAdd = (short)GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tExplosion, this.blueRankInfoList[n].rating);
			this.blueRankInfoList[n].seasonScoreAdd = (short)GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tExplosion, this.blueRankInfoList[n].rating, this.blueRankInfoList[n].isWinner);
		}
		for (int num2 = 0; num2 < this.redRankInfoList.Count; num2++)
		{
			GameValueForRatingCalc gameValueForRatingCalc2 = new GameValueForRatingCalc(GrowthGameModeTag.tExplosion);
			gameValueForRatingCalc2.mEPModeValue.DeadNum = (int)this.redRankInfoList[num2].deadNum;
			gameValueForRatingCalc2.mEPModeValue.enemyTeamTotalPlayerNum = this.blueRankInfoList.Count;
			gameValueForRatingCalc2.mEPModeValue.isWinner = (winTeam == this.redRankInfoList[num2].team);
			gameValueForRatingCalc2.mEPModeValue.killNum = (int)this.redRankInfoList[num2].killNum;
			gameValueForRatingCalc2.mEPModeValue.myTeamTotalPlayerNum = this.redRankInfoList.Count;
			gameValueForRatingCalc2.mEPModeValue.rankInMyTeam = num2 + 1;
			gameValueForRatingCalc2.allRankInfoList = list2;
			gameValueForRatingCalc2.rankInAll = list2.IndexOf(this.redRankInfoList[num2]) + 1;
			gameValueForRatingCalc2.score = (int)this.redRankInfoList[num2].ExplosionScore;
			this.redRankInfoList[num2].rating = GrowthManagerKit.GetOncePlayRating(gameValueForRatingCalc2);
			this.redRankInfoList[num2].isWinner = gameValueForRatingCalc2.mEPModeValue.isWinner;
			int num3 = gameValueForRatingCalc2.mEPModeValue.killNum - gameValueForRatingCalc2.mEPModeValue.DeadNum;
			if (num3 <= 0)
			{
				num3 = 0;
			}
			this.redRankInfoList[num2].coinAdd = (short)(GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tExplosion, this.redRankInfoList[num2].rating) + num3);
			this.redRankInfoList[num2].expAdd = (short)GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tExplosion, this.redRankInfoList[num2].rating);
			this.redRankInfoList[num2].honorPointAdd = (short)GrowthManagerKit.GetOncePlayHonorPointReward(GrowthGameModeTag.tExplosion, this.redRankInfoList[num2].rating);
			this.redRankInfoList[num2].seasonScoreAdd = (short)GrowthManagerKit.GetOncePlaySeasonScoreReward(GrowthGameModeTag.tExplosion, this.redRankInfoList[num2].rating, this.redRankInfoList[num2].isWinner);
		}
	}

	// Token: 0x06002324 RID: 8996 RVA: 0x0010AFEC File Offset: 0x001093EC
	public void RatingBroadcastAndVerifyForExplosionMode()
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

	// Token: 0x06002325 RID: 8997 RVA: 0x0010B068 File Offset: 0x00109468
	public void EnableNewRoundBroadcastAndVerifyForExplosionMode()
	{
		this.GetPlayerIdsSnapshot();
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageNewRound;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.Others);
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x0010B094 File Offset: 0x00109494
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

	// Token: 0x06002327 RID: 8999 RVA: 0x0010B0EC File Offset: 0x001094EC
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

	// Token: 0x06002328 RID: 9000 RVA: 0x0010B208 File Offset: 0x00109608
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

	// Token: 0x06002329 RID: 9001 RVA: 0x0010B324 File Offset: 0x00109724
	public void SendNextRoundMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageExplosionModeNextRoundStart;
		ggmessage.messageContent = new GGMessageContent();
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
	}

	// Token: 0x0600232A RID: 9002 RVA: 0x0010B358 File Offset: 0x00109758
	public void SendSingleRoundWinMessage(int winTeamIndex)
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageExplosionModeSingleRoundWin;
		ggmessage.messageContent = new GGMessageContent();
		ggmessage.messageContent.ID = winTeamIndex;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
	}

	// Token: 0x040023BD RID: 9149
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x040023BE RID: 9150
	private bool mReceiveAllACK;

	// Token: 0x040023BF RID: 9151
	private List<int> mAllplayersID = new List<int>();

	// Token: 0x040023C0 RID: 9152
	private List<int> mTmpAllplayersID = new List<int>();

	// Token: 0x040023C1 RID: 9153
	private bool mReceiveAllActiveNewRound;

	// Token: 0x040023C2 RID: 9154
	private int mACKResultNum;

	// Token: 0x040023C3 RID: 9155
	private int mACkActiveNewRoundNum;

	// Token: 0x040023C4 RID: 9156
	public readonly float EP_MODE_CUT_RATE = 1f;

	// Token: 0x040023C5 RID: 9157
	public float ExplosionTimeCount;

	// Token: 0x040023C6 RID: 9158
	public readonly float EP_MODE_CUT_RATE_BLUETEAM = 5f;

	// Token: 0x040023C7 RID: 9159
	public float ExplosionTimeCountBlueTeam;

	// Token: 0x040023C8 RID: 9160
	private float nextRoundTimeCount;

	// Token: 0x040023C9 RID: 9161
	private float CalcPlayerNumTimeCount;

	// Token: 0x040023CA RID: 9162
	private float AllRoundOverTimeCount;

	// Token: 0x040023CB RID: 9163
	private int maxRoundNum = 9;

	// Token: 0x040023CC RID: 9164
	private int teamWinRoundNum = 5;

	// Token: 0x040023CD RID: 9165
	private List<GGNetworkPlayerProperties> blueRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x040023CE RID: 9166
	private List<GGNetworkPlayerProperties> redRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x040023CF RID: 9167
	private GGTeamType winTeam = GGTeamType.Nil;

	// Token: 0x040023D0 RID: 9168
	private int roundOverChkStatusFlag;

	// Token: 0x040023D1 RID: 9169
	public bool isClacedResult;

	// Token: 0x040023D2 RID: 9170
	private float StartExplosionTimeCount;

	// Token: 0x040023D3 RID: 9171
	private bool isShowStartExplosionButton;

	// Token: 0x040023D4 RID: 9172
	private bool isEnableStartExplosionButton;

	// Token: 0x040023D5 RID: 9173
	private float CutDown5sTimer;

	// Token: 0x040023D6 RID: 9174
	private int PreBluePlayerNum = -1;

	// Token: 0x040023D7 RID: 9175
	private int PreRedPlayerNum = -1;

	// Token: 0x040023D8 RID: 9176
	private GGNetworkCharacter myNetworkCharacter;

	// Token: 0x040023D9 RID: 9177
	private Transform myMainCamera;

	// Token: 0x040023DA RID: 9178
	private int bombPositionNum;

	// Token: 0x040023DB RID: 9179
	private bool explosionRoundOver1;

	// Token: 0x040023DC RID: 9180
	private bool explosionRoundOver2;

	// Token: 0x040023DD RID: 9181
	private AudioSource TimerBombSound;

	// Token: 0x040023DE RID: 9182
	public AnimationClip cameraShakeClip;

	// Token: 0x040023DF RID: 9183
	public bool isInRoudResultCalculating;

	// Token: 0x040023E0 RID: 9184
	private bool flag;
}
