using System;
using System.Collections.Generic;

// Token: 0x0200018D RID: 397
public class GameValueForRatingCalc
{
	// Token: 0x06000AE5 RID: 2789 RVA: 0x0004F18C File Offset: 0x0004D58C
	public GameValueForRatingCalc(GrowthGameModeTag mode)
	{
		this.mGameMode = mode;
		this.mShModeValue = default(GameValueForRatingCalc.StrongholdModeValue);
		this.mKCModeValue = default(GameValueForRatingCalc.KillingCompetitionModeValue);
		this.mEPModeValue = default(GameValueForRatingCalc.ExplosionModeValue);
		this.mMTModeValue = default(GameValueForRatingCalc.MutationModeValue);
		this.mKNModeValue = default(GameValueForRatingCalc.KnifeCompetitionModeValue);
		this.mHTModeValue = default(GameValueForRatingCalc.HuntingModeValue);
		this.allRankInfoList = new List<GGNetworkPlayerProperties>();
	}

	// Token: 0x04000AD2 RID: 2770
	public GrowthGameModeTag mGameMode;

	// Token: 0x04000AD3 RID: 2771
	public GameValueForRatingCalc.StrongholdModeValue mShModeValue;

	// Token: 0x04000AD4 RID: 2772
	public GameValueForRatingCalc.KillingCompetitionModeValue mKCModeValue;

	// Token: 0x04000AD5 RID: 2773
	public GameValueForRatingCalc.ExplosionModeValue mEPModeValue;

	// Token: 0x04000AD6 RID: 2774
	public GameValueForRatingCalc.MutationModeValue mMTModeValue;

	// Token: 0x04000AD7 RID: 2775
	public GameValueForRatingCalc.KnifeCompetitionModeValue mKNModeValue;

	// Token: 0x04000AD8 RID: 2776
	public GameValueForRatingCalc.HuntingModeValue mHTModeValue;

	// Token: 0x04000AD9 RID: 2777
	public List<GGNetworkPlayerProperties> allRankInfoList;

	// Token: 0x04000ADA RID: 2778
	public int rankInAll;

	// Token: 0x04000ADB RID: 2779
	public int score;

	// Token: 0x0200018E RID: 398
	public struct StrongholdModeValue
	{
		// Token: 0x04000ADC RID: 2780
		public int killNum;

		// Token: 0x04000ADD RID: 2781
		public int DeadNum;

		// Token: 0x04000ADE RID: 2782
		public bool isWinner;

		// Token: 0x04000ADF RID: 2783
		public int myTeamTotalPlayerNum;

		// Token: 0x04000AE0 RID: 2784
		public int enemyTeamTotalPlayerNum;

		// Token: 0x04000AE1 RID: 2785
		public int rankInMyTeam;
	}

	// Token: 0x0200018F RID: 399
	public struct KillingCompetitionModeValue
	{
		// Token: 0x04000AE2 RID: 2786
		public int killNum;

		// Token: 0x04000AE3 RID: 2787
		public int DeadNum;

		// Token: 0x04000AE4 RID: 2788
		public bool isWinner;

		// Token: 0x04000AE5 RID: 2789
		public int myTeamTotalPlayerNum;

		// Token: 0x04000AE6 RID: 2790
		public int enemyTeamTotalPlayerNum;

		// Token: 0x04000AE7 RID: 2791
		public int rankInMyTeam;
	}

	// Token: 0x02000190 RID: 400
	public struct ExplosionModeValue
	{
		// Token: 0x04000AE8 RID: 2792
		public int killNum;

		// Token: 0x04000AE9 RID: 2793
		public int DeadNum;

		// Token: 0x04000AEA RID: 2794
		public bool isWinner;

		// Token: 0x04000AEB RID: 2795
		public int myTeamTotalPlayerNum;

		// Token: 0x04000AEC RID: 2796
		public int enemyTeamTotalPlayerNum;

		// Token: 0x04000AED RID: 2797
		public int rankInMyTeam;
	}

	// Token: 0x02000191 RID: 401
	public struct MutationModeValue
	{
		// Token: 0x04000AEE RID: 2798
		public int score;

		// Token: 0x04000AEF RID: 2799
		public bool isWinner;

		// Token: 0x04000AF0 RID: 2800
		public int myTeamTotalPlayerNum;

		// Token: 0x04000AF1 RID: 2801
		public int enemyTeamTotalPlayerNum;

		// Token: 0x04000AF2 RID: 2802
		public int rankInMyTeam;
	}

	// Token: 0x02000192 RID: 402
	public struct KnifeCompetitionModeValue
	{
		// Token: 0x04000AF3 RID: 2803
		public int killNum;

		// Token: 0x04000AF4 RID: 2804
		public int DeadNum;

		// Token: 0x04000AF5 RID: 2805
		public bool isWinner;

		// Token: 0x04000AF6 RID: 2806
		public int myTeamTotalPlayerNum;

		// Token: 0x04000AF7 RID: 2807
		public int enemyTeamTotalPlayerNum;

		// Token: 0x04000AF8 RID: 2808
		public int rankInMyTeam;
	}

	// Token: 0x02000193 RID: 403
	public struct HuntingModeValue
	{
		// Token: 0x04000AF9 RID: 2809
		public int SenceID;

		// Token: 0x04000AFA RID: 2810
		public int RoundID;

		// Token: 0x04000AFB RID: 2811
		public int Difficulty;

		// Token: 0x04000AFC RID: 2812
		public int PassTime;

		// Token: 0x04000AFD RID: 2813
		public int DeadNum;

		// Token: 0x04000AFE RID: 2814
		public int MaxRoundNum;
	}
}
