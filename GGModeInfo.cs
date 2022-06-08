using System;
using ProtoBuf;

// Token: 0x02000523 RID: 1315
[ProtoContract]
public class GGModeInfo
{
	// Token: 0x0400259A RID: 9626
	[ProtoMember(1, IsRequired = true)]
	public int AllModeBlueTeamPlayerTotalNum;

	// Token: 0x0400259B RID: 9627
	[ProtoMember(2, IsRequired = true)]
	public int AllModeRedTeamPlayerTotalNum;

	// Token: 0x0400259C RID: 9628
	[ProtoMember(3, IsRequired = true)]
	public int AllModeBlueTeamPlayerSurvivalNum;

	// Token: 0x0400259D RID: 9629
	[ProtoMember(4, IsRequired = true)]
	public int AllModeRedTeamPlayerSurvivalNum;

	// Token: 0x0400259E RID: 9630
	[ProtoMember(5, IsRequired = true)]
	public int mRedResources;

	// Token: 0x0400259F RID: 9631
	[ProtoMember(6, IsRequired = true)]
	public int mBlueResources;

	// Token: 0x040025A0 RID: 9632
	[ProtoMember(7, IsRequired = true)]
	public int mMaxResources = 500;

	// Token: 0x040025A1 RID: 9633
	[ProtoMember(8, IsRequired = true)]
	public GGStrondholdState mStronghold1State = GGStrondholdState.unactivate;

	// Token: 0x040025A2 RID: 9634
	[ProtoMember(9, IsRequired = true)]
	public GGStrondholdState mStronghold2State = GGStrondholdState.unactivate;

	// Token: 0x040025A3 RID: 9635
	[ProtoMember(10, IsRequired = true)]
	public GGStrondholdState mStronghold3State = GGStrondholdState.unactivate;

	// Token: 0x040025A4 RID: 9636
	[ProtoMember(11, IsRequired = true)]
	public bool mStronghold1CD;

	// Token: 0x040025A5 RID: 9637
	[ProtoMember(12, IsRequired = true)]
	public bool mStronghold2CD;

	// Token: 0x040025A6 RID: 9638
	[ProtoMember(13, IsRequired = true)]
	public bool mStronghold3CD;

	// Token: 0x040025A7 RID: 9639
	[ProtoMember(14, IsRequired = true)]
	public int mStronghold1CDTimer;

	// Token: 0x040025A8 RID: 9640
	[ProtoMember(15, IsRequired = true)]
	public int mStronghold2CDTimer;

	// Token: 0x040025A9 RID: 9641
	[ProtoMember(16, IsRequired = true)]
	public int mStronghold3CDTimer;

	// Token: 0x040025AA RID: 9642
	[ProtoMember(17, IsRequired = true)]
	public int mStrongholdTimer;

	// Token: 0x040025AB RID: 9643
	[ProtoMember(18, IsRequired = true)]
	public bool IsStartStronghold;

	// Token: 0x040025AC RID: 9644
	[ProtoMember(19, IsRequired = true)]
	public bool isStartStrongholdTimer;

	// Token: 0x040025AD RID: 9645
	[ProtoMember(20, IsRequired = true)]
	public int StartStrongholdTimer = 5;

	// Token: 0x040025AE RID: 9646
	[ProtoMember(21, IsRequired = true)]
	public int blueKilling;

	// Token: 0x040025AF RID: 9647
	[ProtoMember(22, IsRequired = true)]
	public int redKilling;

	// Token: 0x040025B0 RID: 9648
	[ProtoMember(23, IsRequired = true)]
	public int MAXKilling = 40;

	// Token: 0x040025B1 RID: 9649
	[ProtoMember(24, IsRequired = true)]
	public bool IsTimerBombInstalled;

	// Token: 0x040025B2 RID: 9650
	[ProtoMember(25, IsRequired = true)]
	public int explosionTimer = 50;

	// Token: 0x040025B3 RID: 9651
	[ProtoMember(26, IsRequired = true)]
	public int totalTimer = 180;

	// Token: 0x040025B4 RID: 9652
	[ProtoMember(27, IsRequired = true)]
	public int bombPositionId;

	// Token: 0x040025B5 RID: 9653
	[ProtoMember(28, IsRequired = true)]
	public bool activeTimerBomb;

	// Token: 0x040025B6 RID: 9654
	[ProtoMember(29, IsRequired = true)]
	public bool isStartExplosionTimer;

	// Token: 0x040025B7 RID: 9655
	[ProtoMember(30, IsRequired = true)]
	public int StartExplosionTimer = 6;

	// Token: 0x040025B8 RID: 9656
	[ProtoMember(31, IsRequired = true)]
	public bool IsStartExplosion;

	// Token: 0x040025B9 RID: 9657
	[ProtoMember(32, IsRequired = true)]
	public int ExplosionModeNewPlayerJoinGameTimer = 60;

	// Token: 0x040025BA RID: 9658
	[ProtoMember(33, IsRequired = true)]
	public int RoundNum = 1;

	// Token: 0x040025BB RID: 9659
	[ProtoMember(34, IsRequired = true)]
	public int BlueLivePlayerNum = -1;

	// Token: 0x040025BC RID: 9660
	[ProtoMember(35, IsRequired = true)]
	public int RedLivePlayerNum = -1;

	// Token: 0x040025BD RID: 9661
	[ProtoMember(36, IsRequired = true)]
	public bool IsTimerBombUninstall;

	// Token: 0x040025BE RID: 9662
	[ProtoMember(37, IsRequired = true)]
	public bool singleRoundResultCalc;

	// Token: 0x040025BF RID: 9663
	[ProtoMember(38, IsRequired = true)]
	public float TimerBombPositionX;

	// Token: 0x040025C0 RID: 9664
	[ProtoMember(39, IsRequired = true)]
	public float TimerBombPositionY = -200f;

	// Token: 0x040025C1 RID: 9665
	[ProtoMember(40, IsRequired = true)]
	public float TimerBombPositionZ;

	// Token: 0x040025C2 RID: 9666
	[ProtoMember(41, IsRequired = true)]
	public int RedTeamWinNum;

	// Token: 0x040025C3 RID: 9667
	[ProtoMember(42, IsRequired = true)]
	public int BlueTeamWinNum;

	// Token: 0x040025C4 RID: 9668
	[ProtoMember(43, IsRequired = true)]
	public int survivalTimer = 600;

	// Token: 0x040025C5 RID: 9669
	[ProtoMember(44, IsRequired = true)]
	public int humanNum = 1;

	// Token: 0x040025C6 RID: 9670
	[ProtoMember(45, IsRequired = true)]
	public int zombieNum;

	// Token: 0x040025C7 RID: 9671
	[ProtoMember(46, IsRequired = true)]
	public bool isStartMutation;

	// Token: 0x040025C8 RID: 9672
	[ProtoMember(47, IsRequired = true)]
	public bool isStartMutationTimer;

	// Token: 0x040025C9 RID: 9673
	[ProtoMember(48, IsRequired = true)]
	public int MutationTimer = 5;

	// Token: 0x040025CA RID: 9674
	[ProtoMember(49, IsRequired = true)]
	public bool isStartTranslate;

	// Token: 0x040025CB RID: 9675
	[ProtoMember(50, IsRequired = true)]
	public int TranslateTimer = 3;

	// Token: 0x040025CC RID: 9676
	[ProtoMember(51, IsRequired = true)]
	public bool isGotoGameScene;

	// Token: 0x040025CD RID: 9677
	[ProtoMember(52, IsRequired = true)]
	public bool isStartHuntingTimer;

	// Token: 0x040025CE RID: 9678
	[ProtoMember(53, IsRequired = true)]
	public bool IsStartHunting;

	// Token: 0x040025CF RID: 9679
	[ProtoMember(54, IsRequired = true)]
	public int StartHuntingTimer = 5;

	// Token: 0x040025D0 RID: 9680
	[ProtoMember(55, IsRequired = true)]
	public bool IsBossKilled;

	// Token: 0x040025D1 RID: 9681
	[ProtoMember(56, IsRequired = true)]
	public int HuntingRoundNum = 1;

	// Token: 0x040025D2 RID: 9682
	[ProtoMember(57, IsRequired = true)]
	public int HuntingTimer = 360;

	// Token: 0x040025D3 RID: 9683
	[ProtoMember(58, IsRequired = true)]
	public int RewardTimer = 6;

	// Token: 0x040025D4 RID: 9684
	[ProtoMember(59, IsRequired = true)]
	public bool huntingModeSingleRoundResultCalc;

	// Token: 0x040025D5 RID: 9685
	[ProtoMember(60, IsRequired = true)]
	public bool isStartRewardCal;

	// Token: 0x040025D6 RID: 9686
	[ProtoMember(61, IsRequired = true)]
	public float huntingprocess1 = 1f;

	// Token: 0x040025D7 RID: 9687
	[ProtoMember(62, IsRequired = true)]
	public float huntingprocess2;

	// Token: 0x040025D8 RID: 9688
	[ProtoMember(63, IsRequired = true)]
	public float huntingprocess = 1f;
}
