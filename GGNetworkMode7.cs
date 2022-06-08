using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004EA RID: 1258
public class GGNetworkMode7 : MonoBehaviour
{
	// Token: 0x0600235B RID: 9051 RVA: 0x0010EB63 File Offset: 0x0010CF63
	private void Awake()
	{
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x0010EB68 File Offset: 0x0010CF68
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

	// Token: 0x0600235D RID: 9053 RVA: 0x0010EBA9 File Offset: 0x0010CFA9
	private void Start()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		GGNetworkKit.mInstance.MessageOk += this.Event_MessageOK;
		UIPlayDirector.OnStartHuntingMode += this.OnStartHuntingMode;
		this.InitMyNetworkCharacter();
	}

	// Token: 0x0600235E RID: 9054 RVA: 0x0010EBE8 File Offset: 0x0010CFE8
	private void OnDisable()
	{
		GGNetworkKit.mInstance.MessageOk -= this.Event_MessageOK;
		UIPlayDirector.OnStartHuntingMode -= this.OnStartHuntingMode;
	}

	// Token: 0x0600235F RID: 9055 RVA: 0x0010EC14 File Offset: 0x0010D014
	private void Event_MessageOK(GGMessage message)
	{
		if (message.messageType == GGMessageType.MessageHuntingModeSingleRoundWin)
		{
			GGNetWorkPlayerlogic component = this.myNetworkCharacter.gameObject.GetComponent<GGNetWorkPlayerlogic>();
			component.HuntingModeRandomPositionInWaitingRoom();
			component.HuntingModeDeadCount = 0;
			UIHuntingModeDirector.mInstance.ResetDeadLimitedText(component.HuntingModeMaxDeadCount);
			component.DisableAcidRainEffect();
			GrowthManagerKit.ClearScenePropsEProperty();
			this.myNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
			this.myNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
			GameObject[] array = GameObject.FindGameObjectsWithTag("HuntingModeLocalProp");
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					UnityEngine.Object.DestroyImmediate(array[i]);
				}
			}
			this.RewardCal();
			if (this.mGlobalInfo.modeInfo.HuntingRoundNum == 2)
			{
				GrowthManagerKit.SubHuntingTickets(1);
			}
			if (this.mGlobalInfo.modeInfo.HuntingRoundNum <= this.maxRoundNum - 1 && this.receivedRewardCount < 4)
			{
				UIHuntingModeDirector.mInstance.PushHuntingResultNode(true, this.hRewardInfo);
				base.StartCoroutine(this.HideResultPanel(6f));
			}
			else if (this.mGlobalInfo.modeInfo.HuntingRoundNum == this.maxRoundNum || this.receivedRewardCount >= 4)
			{
				UIHuntingModeDirector.mInstance.PushHuntingResultNode(true, this.hRewardInfo);
			}
		}
		if (message.messageType == GGMessageType.MessageHuntingModeSingleRoundLose)
		{
			GGNetWorkPlayerlogic component2 = this.myNetworkCharacter.gameObject.GetComponent<GGNetWorkPlayerlogic>();
			component2.HuntingModeRandomPositionInWaitingRoom();
			component2.StopCoroutine("waitForGeneratePlayer");
			component2.HuntingModeDeadCount = 0;
			UIHuntingModeDirector.mInstance.ResetDeadLimitedText(component2.HuntingModeMaxDeadCount);
			component2.RainFallEffect.SetActive(false);
			GameObject[] array2 = GameObject.FindGameObjectsWithTag("HuntingModeLocalProp");
			if (array2 != null)
			{
				for (int j = 0; j < array2.Length; j++)
				{
					UnityEngine.Object.DestroyImmediate(array2[j]);
				}
			}
			this.myNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
			this.myNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
			this.mGlobalInfo.modeInfo.HuntingRoundNum = this.maxRoundNum;
			UIHuntingModeDirector.mInstance.PushHuntingResultNode(false, this.hRewardInfo);
		}
		if (message.messageType == GGMessageType.MessageHuntingModeBossKilled)
		{
			this.mGlobalInfo.modeInfo.IsBossKilled = true;
		}
	}

	// Token: 0x06002360 RID: 9056 RVA: 0x0010EE48 File Offset: 0x0010D248
	private IEnumerator HideResultPanel(float delay)
	{
		yield return new WaitForSeconds(delay);
		UIHuntingModeDirector.mInstance.HideHuntingResultNode();
		yield break;
	}

	// Token: 0x06002361 RID: 9057 RVA: 0x0010EE63 File Offset: 0x0010D263
	private void RunRoundResultCalculate()
	{
		if (this.mGlobalInfo.modeInfo.IsBossKilled)
		{
			this.CalcRoundResultForHuntingMode(GGTeamType.blue);
		}
		else
		{
			this.CalcRoundResultForHuntingMode(GGTeamType.red);
		}
	}

	// Token: 0x06002362 RID: 9058 RVA: 0x0010EE90 File Offset: 0x0010D290
	public void ModeLogic()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
		if (this.mGlobalInfo == null)
		{
			return;
		}
		if (this.mGlobalInfo.modeInfo.HuntingRoundNum == this.maxRoundNum)
		{
			return;
		}
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			this.CalcBlueAndRedPlayerNum();
			this.CheckHuntingModeProcess();
			if (!this.mGlobalInfo.modeInfo.huntingModeSingleRoundResultCalc)
			{
				if (this.mGlobalInfo.modeInfo.HuntingRoundNum > 0)
				{
					if (!this.HuntingNextRoundChk())
					{
						this.ActiveStartHuntingButton();
						if (this.mGlobalInfo.modeInfo.isStartHuntingTimer)
						{
							this.CutDown5s();
						}
						if (this.mGlobalInfo.modeInfo.IsStartHunting)
						{
							this.HuntingTimerCheck();
						}
					}
					else
					{
						this.mGlobalInfo.modeInfo.huntingModeSingleRoundResultCalc = true;
					}
				}
			}
			else if (!this.mGlobalInfo.modeInfo.isStartRewardCal)
			{
				this.nextRoundTimeCount += Time.deltaTime;
				if (this.nextRoundTimeCount >= 4f)
				{
					this.nextRoundTimeCount = 0f;
					this.mGlobalInfo.modeInfo.HuntingRoundNum++;
					GameObject[] array = GameObject.FindGameObjectsWithTag("HuntingModeBoss");
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							GGNetworkKit.mInstance.DestorySceneObject(array[i]);
						}
					}
					GameObject[] array2 = GameObject.FindGameObjectsWithTag("HuntingModeSummon");
					if (array2 != null)
					{
						for (int j = 0; j < array2.Length; j++)
						{
							GGNetworkKit.mInstance.DestorySceneObject(array2[j]);
						}
					}
					GameObject[] array3 = GameObject.FindGameObjectsWithTag("HuntingModeProp");
					if (array3 != null)
					{
						for (int k = 0; k < array3.Length; k++)
						{
							GGNetworkKit.mInstance.DestorySceneObject(array3[k]);
						}
					}
					this.RunRoundResultCalculate();
					this.mGlobalInfo.modeInfo.isStartRewardCal = true;
				}
			}
			else
			{
				this.RewardTimerCheck();
			}
		}
	}

	// Token: 0x06002363 RID: 9059 RVA: 0x0010F0A0 File Offset: 0x0010D4A0
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
			}
			else
			{
				this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerSurvivalNum = 0;
			}
			this.CalcPlayerNumTimeCount = 0f;
		}
	}

	// Token: 0x06002364 RID: 9060 RVA: 0x0010F130 File Offset: 0x0010D530
	private void CheckHuntingModeProcess()
	{
		this.CheckProcessTimeCount += Time.deltaTime;
		if (this.CheckProcessTimeCount >= 1f)
		{
			this.mGlobalInfo.modeInfo.huntingprocess = this.mGlobalInfo.modeInfo.huntingprocess1 + this.mGlobalInfo.modeInfo.huntingprocess2;
			this.CheckProcessTimeCount = 0f;
		}
	}

	// Token: 0x06002365 RID: 9061 RVA: 0x0010F19C File Offset: 0x0010D59C
	private void CutDown5s()
	{
		this.CutDown5sTimer += Time.deltaTime;
		if (this.CutDown5sTimer >= 1f)
		{
			if (this.mGlobalInfo.modeInfo.StartHuntingTimer > 0)
			{
				this.mGlobalInfo.modeInfo.StartHuntingTimer--;
				this.CutDown5sTimer = 0f;
			}
			if (this.mGlobalInfo.modeInfo.StartHuntingTimer == 0)
			{
				this.mGlobalInfo.modeInfo.isStartHuntingTimer = false;
				this.BossGenerate();
				this.TranslateToGameScene();
			}
		}
	}

	// Token: 0x06002366 RID: 9062 RVA: 0x0010F238 File Offset: 0x0010D638
	public void BossGenerate()
	{
		int senceId = 1;
		if (Application.loadedLevelName == "HGameSence_1")
		{
			senceId = 1;
		}
		int huntingRoundNum = this.mGlobalInfo.modeInfo.HuntingRoundNum;
		GGNetWorkBossGenerate.mInstance.BossGenerate(senceId, huntingRoundNum);
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x0010F27C File Offset: 0x0010D67C
	private void TranslateToGameScene()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageHuntingModePlayerMoveOut;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x0010F2A8 File Offset: 0x0010D6A8
	public void ActiveStartHuntingButton()
	{
		if (!this.isShowStartHuntingButton && !this.mGlobalInfo.modeInfo.IsStartHunting)
		{
			this.isShowStartHuntingButton = true;
		}
		if (!this.isEnableStartHuntingButton && !this.mGlobalInfo.modeInfo.IsStartHunting)
		{
			if (this.mGlobalInfo.modeInfo.HuntingRoundNum == 1)
			{
				if (this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerSurvivalNum >= 1)
				{
					this.ShowStartHuntingButton();
					this.EnableStartHuntingButton();
				}
			}
			else if (this.mGlobalInfo.modeInfo.HuntingRoundNum < this.maxRoundNum && this.mGlobalInfo.modeInfo.HuntingRoundNum > 1 && this.mGlobalInfo.modeInfo.AllModeBlueTeamPlayerSurvivalNum >= 1)
			{
				this.StartHunting();
			}
		}
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x0010F386 File Offset: 0x0010D786
	public void ShowStartHuntingButton()
	{
		UIModeDirector.mInstance.ShowStartBtn();
	}

	// Token: 0x0600236A RID: 9066 RVA: 0x0010F392 File Offset: 0x0010D792
	public void EnableStartHuntingButton()
	{
		UIModeDirector.mInstance.EnableStartBtn();
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x0010F39E File Offset: 0x0010D79E
	private void OnStartHuntingMode()
	{
		this.StartHunting();
	}

	// Token: 0x0600236C RID: 9068 RVA: 0x0010F3A6 File Offset: 0x0010D7A6
	private void StartHunting()
	{
		this.mGlobalInfo.modeInfo.IsStartHunting = true;
		this.mGlobalInfo.modeInfo.isStartHuntingTimer = true;
		GGNetworkKit.mInstance.SetRoomJoinableStatus(false);
	}

	// Token: 0x0600236D RID: 9069 RVA: 0x0010F3D5 File Offset: 0x0010D7D5
	private void StartRewardGet()
	{
		this.mGlobalInfo.modeInfo.isStartRewardCal = true;
	}

	// Token: 0x0600236E RID: 9070 RVA: 0x0010F3E8 File Offset: 0x0010D7E8
	private void ResetHuntingSingleRoundData()
	{
		this.mGlobalInfo.modeInfo.HuntingTimer = 360;
		this.mGlobalInfo.modeInfo.isStartHuntingTimer = false;
		this.mGlobalInfo.modeInfo.StartHuntingTimer = 5;
		this.mGlobalInfo.modeInfo.IsStartHunting = false;
		this.mGlobalInfo.modeInfo.IsBossKilled = false;
		this.mGlobalInfo.modeInfo.isStartRewardCal = false;
		this.mGlobalInfo.modeInfo.RewardTimer = 6;
		this.mGlobalInfo.modeInfo.huntingprocess1 = 0f;
		this.mGlobalInfo.modeInfo.huntingprocess2 = 0f;
	}

	// Token: 0x0600236F RID: 9071 RVA: 0x0010F49A File Offset: 0x0010D89A
	public void Reset()
	{
		base.gameObject.AddComponent<GGNetworkMode7>();
		UnityEngine.Object.DestroyImmediate(this);
	}

	// Token: 0x06002370 RID: 9072 RVA: 0x0010F4B0 File Offset: 0x0010D8B0
	private void HuntingTimerCheck()
	{
		this.HuntingTimeCount += Time.deltaTime;
		if (this.HuntingTimeCount >= this.HU_MODE_CUT_RATE)
		{
			if (!this.mGlobalInfo.modeInfo.IsBossKilled && this.mGlobalInfo.modeInfo.HuntingTimer > 0)
			{
				this.mGlobalInfo.modeInfo.HuntingTimer--;
			}
			this.HuntingTimeCount = 0f;
		}
	}

	// Token: 0x06002371 RID: 9073 RVA: 0x0010F530 File Offset: 0x0010D930
	private void RewardTimerCheck()
	{
		this.RewardTimeCount += Time.deltaTime;
		if (this.RewardTimeCount >= 1f)
		{
			if (this.mGlobalInfo.modeInfo.RewardTimer > 0)
			{
				this.mGlobalInfo.modeInfo.RewardTimer--;
				this.RewardTimeCount = 0f;
			}
			if (this.mGlobalInfo.modeInfo.RewardTimer == 0)
			{
				this.mGlobalInfo.modeInfo.isStartRewardCal = false;
				this.ResetHuntingSingleRoundData();
				this.mGlobalInfo.modeInfo.huntingModeSingleRoundResultCalc = false;
			}
		}
	}

	// Token: 0x06002372 RID: 9074 RVA: 0x0010F5D8 File Offset: 0x0010D9D8
	public bool HuntingNextRoundChk()
	{
		return this.mGlobalInfo.modeInfo.IsStartHunting && (this.mGlobalInfo.modeInfo.HuntingTimer == 0 || this.mGlobalInfo.modeInfo.IsBossKilled);
	}

	// Token: 0x06002373 RID: 9075 RVA: 0x0010F627 File Offset: 0x0010DA27
	private void Update()
	{
		this.InitMyNetworkCharacter();
		this.ModeLogic();
	}

	// Token: 0x06002374 RID: 9076 RVA: 0x0010F635 File Offset: 0x0010DA35
	public void CalcRoundResultForHuntingMode(GGTeamType winTeam)
	{
		if (winTeam == GGTeamType.blue)
		{
			this.SendSingleRoundWinMessage();
		}
		else
		{
			this.SendSingleRoundLoseMessage();
		}
	}

	// Token: 0x06002375 RID: 9077 RVA: 0x0010F650 File Offset: 0x0010DA50
	public void RewardCal()
	{
		GameValueForRatingCalc gameValueForRatingCalc = new GameValueForRatingCalc(GrowthGameModeTag.tHunting);
		gameValueForRatingCalc.mHTModeValue.SenceID = 1;
		gameValueForRatingCalc.mHTModeValue.RoundID = this.mGlobalInfo.modeInfo.HuntingRoundNum;
		gameValueForRatingCalc.mHTModeValue.Difficulty = GGNetWorkAIDifficultyControl.mInstance.difficultySet;
		gameValueForRatingCalc.mHTModeValue.PassTime = 360 - this.mGlobalInfo.modeInfo.HuntingTimer;
		gameValueForRatingCalc.mHTModeValue.MaxRoundNum = this.maxRoundNum;
		this.receivedRewardCount++;
		if (this.receivedRewardCount >= 4)
		{
			gameValueForRatingCalc.mHTModeValue.RoundID = gameValueForRatingCalc.mHTModeValue.MaxRoundNum;
		}
		gameValueForRatingCalc.mHTModeValue.DeadNum = (int)this.myNetworkCharacter.mPlayerProperties.deadNum;
		gameValueForRatingCalc.mHTModeValue.MaxRoundNum = this.maxRoundNum;
		this.Reward_Rating = GrowthManagerKit.GetOncePlayRating(gameValueForRatingCalc);
		this.Reward_coinAdd = GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tHunting, this.Reward_Rating);
		this.Reward_expAdd = GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tHunting, this.Reward_Rating);
		this.hRewardInfo = GrowthManagerKit.GetHuntingModeItemReward(gameValueForRatingCalc, this.Reward_Rating);
	}

	// Token: 0x06002376 RID: 9078 RVA: 0x0010F770 File Offset: 0x0010DB70
	public void SendNextRoundMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageHuntingModeNextRoundStart;
		ggmessage.messageContent = new GGMessageContent();
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
	}

	// Token: 0x06002377 RID: 9079 RVA: 0x0010F7A8 File Offset: 0x0010DBA8
	public void SendSingleRoundWinMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageHuntingModeSingleRoundWin;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
	}

	// Token: 0x06002378 RID: 9080 RVA: 0x0010F7D4 File Offset: 0x0010DBD4
	public void SendSingleRoundLoseMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageHuntingModeSingleRoundLose;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
	}

	// Token: 0x0400240E RID: 9230
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x0400240F RID: 9231
	private bool mReceiveAllACK;

	// Token: 0x04002410 RID: 9232
	private List<int> mAllplayersID = new List<int>();

	// Token: 0x04002411 RID: 9233
	private List<int> mTmpAllplayersID = new List<int>();

	// Token: 0x04002412 RID: 9234
	private bool mReceiveAllActiveNewRound;

	// Token: 0x04002413 RID: 9235
	private int mACKResultNum;

	// Token: 0x04002414 RID: 9236
	private int mACkActiveNewRoundNum;

	// Token: 0x04002415 RID: 9237
	public readonly float HU_MODE_CUT_RATE = 1f;

	// Token: 0x04002416 RID: 9238
	public float HuntingTimeCount;

	// Token: 0x04002417 RID: 9239
	public float RewardTimeCount;

	// Token: 0x04002418 RID: 9240
	public readonly float HU_MODE_CUT_RATE_BLUETEAM = 5f;

	// Token: 0x04002419 RID: 9241
	public float HuntingTimeCountBlueTeam;

	// Token: 0x0400241A RID: 9242
	private float nextRoundTimeCount;

	// Token: 0x0400241B RID: 9243
	private float CalcPlayerNumTimeCount;

	// Token: 0x0400241C RID: 9244
	private float AllRoundOverTimeCount;

	// Token: 0x0400241D RID: 9245
	private float CheckProcessTimeCount;

	// Token: 0x0400241E RID: 9246
	private List<GGNetworkPlayerProperties> blueRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x0400241F RID: 9247
	private List<GGNetworkPlayerProperties> redRankInfoList = new List<GGNetworkPlayerProperties>();

	// Token: 0x04002420 RID: 9248
	private GGTeamType winTeam = GGTeamType.Nil;

	// Token: 0x04002421 RID: 9249
	private int roundOverChkStatusFlag;

	// Token: 0x04002422 RID: 9250
	public bool isClacedResult;

	// Token: 0x04002423 RID: 9251
	private float StartHuntingTimeCount;

	// Token: 0x04002424 RID: 9252
	private bool isShowStartHuntingButton;

	// Token: 0x04002425 RID: 9253
	private bool isEnableStartHuntingButton;

	// Token: 0x04002426 RID: 9254
	private float CutDown5sTimer;

	// Token: 0x04002427 RID: 9255
	private GGNetworkCharacter myNetworkCharacter;

	// Token: 0x04002428 RID: 9256
	private GrowthGameRatingTag Reward_Rating;

	// Token: 0x04002429 RID: 9257
	private int Reward_coinAdd;

	// Token: 0x0400242A RID: 9258
	private int Reward_expAdd;

	// Token: 0x0400242B RID: 9259
	private int Reward_StaffIndex;

	// Token: 0x0400242C RID: 9260
	private HuntingRewardInfo hRewardInfo;

	// Token: 0x0400242D RID: 9261
	private int maxRoundNum = 5;

	// Token: 0x0400242E RID: 9262
	private int receivedRewardCount;

	// Token: 0x0400242F RID: 9263
	public bool isInRoudResultCalculating;
}
