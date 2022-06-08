using System;
using System.Collections.Generic;
using Photon;
using UnityEngine;

// Token: 0x02000524 RID: 1316
public class GGNetworkGlobalInfo : Photon.MonoBehaviour
{
	// Token: 0x06002495 RID: 9365 RVA: 0x001132CC File Offset: 0x001116CC
	public void Reset()
	{
		this.modeInfo = null;
		this.modeInfo = new GGModeInfo();
		if (GGNetworkKit.mInstance.GetMaxPlayers() >= 1 && GGNetworkKit.mInstance.GetMaxPlayers() <= 4)
		{
			this.modeInfo.MAXKilling = 40;
		}
		else if (GGNetworkKit.mInstance.GetMaxPlayers() >= 5 && GGNetworkKit.mInstance.GetMaxPlayers() <= 8)
		{
			this.modeInfo.MAXKilling = 80;
		}
		else if (GGNetworkKit.mInstance.GetMaxPlayers() >= 9 && GGNetworkKit.mInstance.GetMaxPlayers() <= 12)
		{
			this.modeInfo.MAXKilling = 120;
		}
		else if (GGNetworkKit.mInstance.GetMaxPlayers() >= 13 && GGNetworkKit.mInstance.GetMaxPlayers() <= 16)
		{
			this.modeInfo.MAXKilling = 160;
		}
		else if (GGNetworkKit.mInstance.GetMaxPlayers() >= 17 && GGNetworkKit.mInstance.GetMaxPlayers() <= 20)
		{
			this.modeInfo.MAXKilling = 200;
		}
		else
		{
			this.modeInfo.MAXKilling = 200;
		}
	}

	// Token: 0x06002496 RID: 9366 RVA: 0x00113401 File Offset: 0x00111801
	private void Awake()
	{
		this.modeInfo = new GGModeInfo();
		this.mElevatorPositionCollection = new GGVector3List();
		this.mElevatorPositionCollection.vector3List = new List<GGVector3>();
	}

	// Token: 0x06002497 RID: 9367 RVA: 0x0011342C File Offset: 0x0011182C
	private void Start()
	{
		if (GGNetworkKit.mInstance.GetMaxPlayers() >= 1 && GGNetworkKit.mInstance.GetMaxPlayers() <= 4)
		{
			this.modeInfo.MAXKilling = 40;
		}
		else if (GGNetworkKit.mInstance.GetMaxPlayers() >= 5 && GGNetworkKit.mInstance.GetMaxPlayers() <= 8)
		{
			this.modeInfo.MAXKilling = 80;
		}
		else if (GGNetworkKit.mInstance.GetMaxPlayers() >= 9 && GGNetworkKit.mInstance.GetMaxPlayers() <= 12)
		{
			this.modeInfo.MAXKilling = 120;
		}
		else if (GGNetworkKit.mInstance.GetMaxPlayers() >= 13 && GGNetworkKit.mInstance.GetMaxPlayers() <= 16)
		{
			this.modeInfo.MAXKilling = 160;
		}
		else if (GGNetworkKit.mInstance.GetMaxPlayers() >= 17 && GGNetworkKit.mInstance.GetMaxPlayers() <= 20)
		{
			this.modeInfo.MAXKilling = 200;
		}
		else
		{
			this.modeInfo.MAXKilling = 200;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag("MovingObject");
		if (array != null && GGNetworkKit.mInstance.IsMasterClient())
		{
			for (int i = 0; i < array.Length; i++)
			{
				GGVector3 ggvector = new GGVector3(array[i].transform.position.x, array[i].transform.position.y, array[i].transform.position.z);
				ggvector.X = array[i].transform.position.x;
				ggvector.Y = array[i].transform.position.y;
				ggvector.Z = array[i].transform.position.z;
				ggvector.ID = array[i].GetComponent<MovingObjectControl>().ID;
				ggvector.curTime = array[i].GetComponent<MovingObjectControl>().movingObjectTime;
				this.mElevatorPositionCollection.vector3List.Add(ggvector);
			}
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.KillingCompetition)
		{
			this.isKillingCompetitionMode = true;
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Explosion)
		{
			this.isExplosionMode = true;
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.StrongHold)
		{
			this.isStrongHoldMode = true;
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			this.isMutationMode = true;
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.KnifeCompetition)
		{
			this.isKnifeCompetitionMode = true;
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting)
		{
			this.isHuntingMode = true;
		}
	}

	// Token: 0x06002498 RID: 9368 RVA: 0x001136F8 File Offset: 0x00111AF8
	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext((short)this.modeInfo.AllModeBlueTeamPlayerTotalNum);
			stream.SendNext((short)this.modeInfo.AllModeRedTeamPlayerTotalNum);
			stream.SendNext((short)this.modeInfo.AllModeBlueTeamPlayerSurvivalNum);
			stream.SendNext((short)this.modeInfo.AllModeRedTeamPlayerSurvivalNum);
			if (this.isStrongHoldMode)
			{
				stream.SendNext((short)this.modeInfo.mRedResources);
				stream.SendNext((short)this.modeInfo.mBlueResources);
				stream.SendNext((short)this.modeInfo.mMaxResources);
				stream.SendNext(this.modeInfo.mStronghold1State);
				stream.SendNext(this.modeInfo.mStronghold2State);
				stream.SendNext(this.modeInfo.mStronghold3State);
				stream.SendNext(this.modeInfo.mStronghold1CD);
				stream.SendNext(this.modeInfo.mStronghold2CD);
				stream.SendNext(this.modeInfo.mStronghold3CD);
				stream.SendNext((byte)this.modeInfo.mStronghold1CDTimer);
				stream.SendNext((byte)this.modeInfo.mStronghold2CDTimer);
				stream.SendNext((byte)this.modeInfo.mStronghold3CDTimer);
				stream.SendNext((short)this.modeInfo.mStrongholdTimer);
				stream.SendNext(this.modeInfo.IsStartStronghold);
				stream.SendNext(this.modeInfo.isStartStrongholdTimer);
				stream.SendNext((byte)this.modeInfo.StartStrongholdTimer);
			}
			else if (this.isKillingCompetitionMode || this.isKnifeCompetitionMode)
			{
				stream.SendNext((short)this.modeInfo.blueKilling);
				stream.SendNext((short)this.modeInfo.redKilling);
				stream.SendNext((short)this.modeInfo.MAXKilling);
			}
			else if (this.isExplosionMode)
			{
				stream.SendNext(this.modeInfo.IsTimerBombInstalled);
				stream.SendNext((short)this.modeInfo.explosionTimer);
				stream.SendNext((short)this.modeInfo.totalTimer);
				stream.SendNext((byte)this.modeInfo.bombPositionId);
				stream.SendNext(this.modeInfo.activeTimerBomb);
				stream.SendNext(this.modeInfo.isStartExplosionTimer);
				stream.SendNext((short)this.modeInfo.StartExplosionTimer);
				stream.SendNext(this.modeInfo.IsStartExplosion);
				stream.SendNext((short)this.modeInfo.ExplosionModeNewPlayerJoinGameTimer);
				stream.SendNext((short)this.modeInfo.RoundNum);
				stream.SendNext((byte)this.modeInfo.BlueLivePlayerNum);
				stream.SendNext((byte)this.modeInfo.RedLivePlayerNum);
				stream.SendNext(this.modeInfo.IsTimerBombUninstall);
				stream.SendNext(this.modeInfo.singleRoundResultCalc);
				stream.SendNext(this.modeInfo.TimerBombPositionX);
				stream.SendNext(this.modeInfo.TimerBombPositionY);
				stream.SendNext(this.modeInfo.TimerBombPositionZ);
				stream.SendNext((byte)this.modeInfo.RedTeamWinNum);
				stream.SendNext((byte)this.modeInfo.BlueTeamWinNum);
			}
			else if (this.isMutationMode)
			{
				stream.SendNext((short)this.modeInfo.survivalTimer);
				stream.SendNext((byte)this.modeInfo.humanNum);
				stream.SendNext((byte)this.modeInfo.zombieNum);
				stream.SendNext(this.modeInfo.isStartMutation);
				stream.SendNext(this.modeInfo.isStartMutationTimer);
				stream.SendNext((short)this.modeInfo.MutationTimer);
				stream.SendNext(this.modeInfo.isStartTranslate);
				stream.SendNext((short)this.modeInfo.TranslateTimer);
				stream.SendNext(this.modeInfo.isGotoGameScene);
			}
			else if (this.isHuntingMode)
			{
				stream.SendNext(this.modeInfo.isStartHuntingTimer);
				stream.SendNext(this.modeInfo.IsStartHunting);
				stream.SendNext((short)this.modeInfo.StartHuntingTimer);
				stream.SendNext(this.modeInfo.IsBossKilled);
				stream.SendNext((short)this.modeInfo.HuntingRoundNum);
				stream.SendNext((short)this.modeInfo.HuntingTimer);
				stream.SendNext((short)this.modeInfo.RewardTimer);
				stream.SendNext(this.modeInfo.huntingModeSingleRoundResultCalc);
				stream.SendNext(this.modeInfo.isStartRewardCal);
				stream.SendNext(this.modeInfo.huntingprocess1);
				stream.SendNext(this.modeInfo.huntingprocess2);
				stream.SendNext(this.modeInfo.huntingprocess);
			}
		}
		else
		{
			this.modeInfo.AllModeBlueTeamPlayerTotalNum = (int)((short)stream.ReceiveNext());
			this.modeInfo.AllModeRedTeamPlayerTotalNum = (int)((short)stream.ReceiveNext());
			this.modeInfo.AllModeBlueTeamPlayerSurvivalNum = (int)((short)stream.ReceiveNext());
			this.modeInfo.AllModeRedTeamPlayerSurvivalNum = (int)((short)stream.ReceiveNext());
			if (this.isStrongHoldMode)
			{
				this.modeInfo.mRedResources = (int)((short)stream.ReceiveNext());
				this.modeInfo.mBlueResources = (int)((short)stream.ReceiveNext());
				this.modeInfo.mMaxResources = (int)((short)stream.ReceiveNext());
				this.modeInfo.mStronghold1State = (GGStrondholdState)stream.ReceiveNext();
				this.modeInfo.mStronghold2State = (GGStrondholdState)stream.ReceiveNext();
				this.modeInfo.mStronghold3State = (GGStrondholdState)stream.ReceiveNext();
				this.modeInfo.mStronghold1CD = (bool)stream.ReceiveNext();
				this.modeInfo.mStronghold2CD = (bool)stream.ReceiveNext();
				this.modeInfo.mStronghold3CD = (bool)stream.ReceiveNext();
				this.modeInfo.mStronghold1CDTimer = (int)((byte)stream.ReceiveNext());
				this.modeInfo.mStronghold2CDTimer = (int)((byte)stream.ReceiveNext());
				this.modeInfo.mStronghold3CDTimer = (int)((byte)stream.ReceiveNext());
				this.modeInfo.mStrongholdTimer = (int)((short)stream.ReceiveNext());
				this.modeInfo.IsStartStronghold = (bool)stream.ReceiveNext();
				this.modeInfo.isStartStrongholdTimer = (bool)stream.ReceiveNext();
				this.modeInfo.StartStrongholdTimer = (int)((byte)stream.ReceiveNext());
			}
			else if (this.isKillingCompetitionMode || this.isKnifeCompetitionMode)
			{
				this.modeInfo.blueKilling = (int)((short)stream.ReceiveNext());
				this.modeInfo.redKilling = (int)((short)stream.ReceiveNext());
				this.modeInfo.MAXKilling = (int)((short)stream.ReceiveNext());
			}
			else if (this.isExplosionMode)
			{
				this.modeInfo.IsTimerBombInstalled = (bool)stream.ReceiveNext();
				this.modeInfo.explosionTimer = (int)((short)stream.ReceiveNext());
				this.modeInfo.totalTimer = (int)((short)stream.ReceiveNext());
				this.modeInfo.bombPositionId = (int)((byte)stream.ReceiveNext());
				this.modeInfo.activeTimerBomb = (bool)stream.ReceiveNext();
				this.modeInfo.isStartExplosionTimer = (bool)stream.ReceiveNext();
				this.modeInfo.StartExplosionTimer = (int)((short)stream.ReceiveNext());
				this.modeInfo.IsStartExplosion = (bool)stream.ReceiveNext();
				this.modeInfo.ExplosionModeNewPlayerJoinGameTimer = (int)((short)stream.ReceiveNext());
				this.modeInfo.RoundNum = (int)((short)stream.ReceiveNext());
				this.modeInfo.BlueLivePlayerNum = (int)((byte)stream.ReceiveNext());
				this.modeInfo.RedLivePlayerNum = (int)((byte)stream.ReceiveNext());
				this.modeInfo.IsTimerBombUninstall = (bool)stream.ReceiveNext();
				this.modeInfo.singleRoundResultCalc = (bool)stream.ReceiveNext();
				this.modeInfo.TimerBombPositionX = (float)stream.ReceiveNext();
				this.modeInfo.TimerBombPositionY = (float)stream.ReceiveNext();
				this.modeInfo.TimerBombPositionZ = (float)stream.ReceiveNext();
				this.modeInfo.RedTeamWinNum = (int)((byte)stream.ReceiveNext());
				this.modeInfo.BlueTeamWinNum = (int)((byte)stream.ReceiveNext());
			}
			else if (this.isMutationMode)
			{
				this.modeInfo.survivalTimer = (int)((short)stream.ReceiveNext());
				this.modeInfo.humanNum = (int)((byte)stream.ReceiveNext());
				this.modeInfo.zombieNum = (int)((byte)stream.ReceiveNext());
				this.modeInfo.isStartMutation = (bool)stream.ReceiveNext();
				this.modeInfo.isStartMutationTimer = (bool)stream.ReceiveNext();
				this.modeInfo.MutationTimer = (int)((short)stream.ReceiveNext());
				this.modeInfo.isStartTranslate = (bool)stream.ReceiveNext();
				this.modeInfo.TranslateTimer = (int)((short)stream.ReceiveNext());
				this.modeInfo.isGotoGameScene = (bool)stream.ReceiveNext();
			}
			else if (this.isHuntingMode)
			{
				this.modeInfo.isStartHuntingTimer = (bool)stream.ReceiveNext();
				this.modeInfo.IsStartHunting = (bool)stream.ReceiveNext();
				this.modeInfo.StartHuntingTimer = (int)((short)stream.ReceiveNext());
				this.modeInfo.IsBossKilled = (bool)stream.ReceiveNext();
				this.modeInfo.HuntingRoundNum = (int)((short)stream.ReceiveNext());
				this.modeInfo.HuntingTimer = (int)((short)stream.ReceiveNext());
				this.modeInfo.RewardTimer = (int)((short)stream.ReceiveNext());
				this.modeInfo.huntingModeSingleRoundResultCalc = (bool)stream.ReceiveNext();
				this.modeInfo.isStartRewardCal = (bool)stream.ReceiveNext();
				this.modeInfo.huntingprocess1 = (float)stream.ReceiveNext();
				this.modeInfo.huntingprocess2 = (float)stream.ReceiveNext();
				this.modeInfo.huntingprocess = (float)stream.ReceiveNext();
			}
		}
	}

	// Token: 0x06002499 RID: 9369 RVA: 0x001142B7 File Offset: 0x001126B7
	private void Update()
	{
	}

	// Token: 0x040025D9 RID: 9689
	public GGModeInfo modeInfo;

	// Token: 0x040025DA RID: 9690
	public GGVector3List mElevatorPositionCollection;

	// Token: 0x040025DB RID: 9691
	private bool isKillingCompetitionMode;

	// Token: 0x040025DC RID: 9692
	private bool isExplosionMode;

	// Token: 0x040025DD RID: 9693
	private bool isStrongHoldMode;

	// Token: 0x040025DE RID: 9694
	private bool isMutationMode;

	// Token: 0x040025DF RID: 9695
	private bool isKnifeCompetitionMode;

	// Token: 0x040025E0 RID: 9696
	private bool isHuntingMode;
}
