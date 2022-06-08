using System;

namespace GrowthSystem
{
	// Token: 0x02000171 RID: 369
	public class GrowthBaseValue
	{
		// Token: 0x0400095A RID: 2394
		public const int mGBV_MinCharacterLevel = 1;

		// Token: 0x0400095B RID: 2395
		public const int mGBV_MaxCharacterLevel = 100;

		// Token: 0x0400095C RID: 2396
		public const int mGBV_MinCoinsNum = 0;

		// Token: 0x0400095D RID: 2397
		public const int mGBV_MaxCoinsNum = 99999999;

		// Token: 0x0400095E RID: 2398
		public const int mGBV_MaxWeaponEquipNum = 8;

		// Token: 0x0400095F RID: 2399
		public static readonly int[] mGBV_BaseExpMatrix = new int[]
		{
			10,
			12,
			14,
			0,
			16,
			18,
			20,
			0,
			88,
			100,
			116,
			0,
			136,
			160,
			188,
			220,
			320,
			370,
			375,
			2000
		};

		// Token: 0x04000960 RID: 2400
		public static readonly int[] mGBV_BaseLevelLayoutMatrix = new int[]
		{
			1,
			3,
			4,
			0,
			6,
			6,
			6,
			0,
			8,
			8,
			8,
			0,
			8,
			8,
			8,
			6,
			6,
			6,
			6,
			1
		};

		// Token: 0x04000961 RID: 2401
		public const int mGBV_BaseExpFactor = 20;

		// Token: 0x04000962 RID: 2402
		public static readonly int[] mGBV_BaseLevelUpCoinsRewardMatrix = new int[]
		{
			10,
			12,
			14,
			0,
			16,
			18,
			20,
			0,
			22,
			25,
			29,
			0,
			34,
			40,
			47,
			55,
			64,
			74,
			75,
			400
		};

		// Token: 0x04000963 RID: 2403
		public const int mGBV_BaseLevelUpCoinsRewardFactor = 2;

		// Token: 0x04000964 RID: 2404
		public static readonly int[] mGBV_RatingValueForCoin = new int[]
		{
			0,
			1,
			2,
			3,
			5,
			8,
			12,
			20
		};

		// Token: 0x04000965 RID: 2405
		public static readonly int[] mGBV_RatingValueForExp = new int[]
		{
			0,
			1,
			2,
			3,
			5,
			8,
			12,
			20
		};

		// Token: 0x04000966 RID: 2406
		public static readonly int[] mGBV_RatingValueForHonorPoint = new int[]
		{
			0,
			1,
			2,
			4,
			6,
			8,
			12,
			20
		};

		// Token: 0x04000967 RID: 2407
		public static readonly int[] mGBV_RatingValueForSeasonScoreWinner = new int[]
		{
			0,
			1,
			2,
			4,
			8,
			10,
			15,
			20
		};

		// Token: 0x04000968 RID: 2408
		public static readonly int[] mGBV_RatingValueForSeasonScoreLoser = new int[]
		{
			0,
			-10,
			-8,
			-4,
			-2,
			-1,
			0,
			0
		};

		// Token: 0x04000969 RID: 2409
		public static readonly int[] mGBV_ModeValueForCoin = new int[]
		{
			0,
			2,
			2,
			2,
			2,
			2,
			2
		};

		// Token: 0x0400096A RID: 2410
		public static readonly int[] mGBV_ModeValueForExp = new int[]
		{
			0,
			2,
			2,
			1,
			1,
			1,
			2
		};

		// Token: 0x0400096B RID: 2411
		public static readonly int[] mGBV_ModeValueForHonorPoint = new int[]
		{
			0,
			1,
			1,
			1,
			0,
			0
		};

		// Token: 0x0400096C RID: 2412
		public static readonly int[] mGBV_ModeValueForSeasonScore = new int[]
		{
			0,
			1,
			1,
			1,
			0,
			0
		};

		// Token: 0x0400096D RID: 2413
		public const float mGBV_BaseCoinRewardFactor = 1f;

		// Token: 0x0400096E RID: 2414
		public const int mGBV_BaseExpRewardFactor = 1;

		// Token: 0x0400096F RID: 2415
		public const int mGBV_BaseHonorPointRewardFactor = 1;

		// Token: 0x04000970 RID: 2416
		public const int mGBV_BaseSeasonScoreRewardFactor = 1;

		// Token: 0x04000971 RID: 2417
		public const int mGBV_AppstoreRatingCoinsRewardNum = 300;

		// Token: 0x04000972 RID: 2418
		public const int mGBV_SNSShareCoinsRewardNum = 20;

		// Token: 0x04000973 RID: 2419
		public static readonly int[] mGBV_LWMTotalKillTarget = new int[]
		{
			100,
			500,
			1000,
			2000,
			3000,
			4000,
			5000,
			10000,
			15000,
			20000,
			25000,
			30000,
			35000,
			40000,
			45000,
			50000
		};

		// Token: 0x04000974 RID: 2420
		public static readonly int[] mGBV_LWMTotalKillReward = new int[]
		{
			100,
			500,
			500,
			1000,
			1000,
			1000,
			1000,
			2000,
			2000,
			2000,
			2000,
			2000,
			2000,
			2000,
			2000,
			2000
		};

		// Token: 0x04000975 RID: 2421
		public static readonly int[] mGBV_WWMTotalKillTarget = new int[]
		{
			100,
			500,
			1000,
			2000,
			3000,
			4000,
			5000,
			10000,
			15000,
			20000,
			25000,
			30000,
			35000,
			40000,
			45000,
			50000
		};

		// Token: 0x04000976 RID: 2422
		public static readonly int[] mGBV_WWMTotalKillReward = new int[]
		{
			100,
			500,
			500,
			1000,
			1000,
			1000,
			1000,
			2000,
			2000,
			2000,
			2000,
			2000,
			2000,
			2000,
			2000,
			2000
		};

		// Token: 0x04000977 RID: 2423
		public static readonly int[] mGBV_TotalHeadshotKillTarget = new int[]
		{
			10,
			100,
			200,
			300,
			400,
			500,
			1000,
			2000,
			3000,
			4000,
			5000,
			10000
		};

		// Token: 0x04000978 RID: 2424
		public static readonly int[] mGBV_TotalHeadshotKillReward = new int[]
		{
			50,
			100,
			200,
			300,
			400,
			500,
			1000,
			2000,
			2000,
			2000,
			2000,
			2000
		};

		// Token: 0x04000979 RID: 2425
		public static readonly int[] mGBV_TotalGoldLikeKillTarget = new int[]
		{
			10,
			50,
			100,
			200,
			300,
			400,
			500,
			1000,
			2000,
			3000,
			4000,
			5000
		};

		// Token: 0x0400097A RID: 2426
		public static readonly int[] mGBV_TotalGoldLikeKillReward = new int[]
		{
			50,
			100,
			200,
			400,
			600,
			800,
			1000,
			2000,
			4000,
			4000,
			4000,
			4000
		};

		// Token: 0x0400097B RID: 2427
		public static readonly int[] mGBV_MaxDeadOneRoundTarget = new int[]
		{
			200
		};

		// Token: 0x0400097C RID: 2428
		public static readonly int[] mGBV_MaxDeadOneRoundReward = new int[]
		{
			500
		};

		// Token: 0x0400097D RID: 2429
		public static readonly int[] mGBV_TotalStrongholdModeVictoryTarget = new int[]
		{
			10,
			20,
			30,
			40,
			50,
			100,
			200,
			500,
			1000,
			2000,
			5000
		};

		// Token: 0x0400097E RID: 2430
		public static readonly int[] mGBV_TotalStrongholdModeVictoryReward = new int[]
		{
			100,
			200,
			300,
			400,
			500,
			1000,
			2000,
			5000,
			5000,
			5000,
			10000
		};

		// Token: 0x0400097F RID: 2431
		public static readonly int[] mGBV_TotalKillingCompetitionModeVictoryTarget = new int[]
		{
			10,
			20,
			30,
			40,
			50,
			100,
			200,
			500,
			1000,
			2000,
			5000
		};

		// Token: 0x04000980 RID: 2432
		public static readonly int[] mGBV_TotalKillingCompetitionModeVictoryReward = new int[]
		{
			100,
			200,
			300,
			400,
			500,
			1000,
			2000,
			5000,
			5000,
			5000,
			10000
		};

		// Token: 0x04000981 RID: 2433
		public static readonly int[] mGBV_TotalExplosionModeVictoryTarget = new int[]
		{
			10,
			20,
			30,
			40,
			50,
			100,
			200,
			500,
			1000,
			2000,
			5000
		};

		// Token: 0x04000982 RID: 2434
		public static readonly int[] mGBV_TotalExplosionModeVictoryReward = new int[]
		{
			100,
			200,
			300,
			400,
			500,
			1000,
			2000,
			5000,
			5000,
			5000,
			10000
		};

		// Token: 0x04000983 RID: 2435
		public static readonly int[] mGBV_TotalMutationModeVictoryTarget = new int[]
		{
			10,
			20,
			30,
			40,
			50,
			100,
			200,
			500,
			1000,
			2000,
			5000
		};

		// Token: 0x04000984 RID: 2436
		public static readonly int[] mGBV_TotalMutationModeVictoryReward = new int[]
		{
			100,
			200,
			300,
			400,
			500,
			1000,
			2000,
			5000,
			5000,
			5000,
			10000
		};

		// Token: 0x04000985 RID: 2437
		public static readonly int[] mGBV_LvGrowthGiftTarget = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			10,
			15,
			20,
			25,
			30,
			35,
			40,
			45,
			50
		};

		// Token: 0x04000986 RID: 2438
		public static readonly int[] mGBV_LvGrowthGiftReward = new int[]
		{
			5,
			5,
			5,
			5,
			5,
			20,
			20,
			20,
			30,
			30,
			40,
			40,
			50,
			50
		};

		// Token: 0x04000987 RID: 2439
		public static readonly int[] mGBV_DailyLoginInSevenDaysTarget = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7
		};

		// Token: 0x04000988 RID: 2440
		public static readonly string[] mGBV_DailyLoginInSevenDaysReward = new string[]
		{
			"Base@Coins@150",
			"Base@Coins@200",
			"Base@GiftBox@1",
			"Base@Coins@250",
			"Base@Coins@300",
			"Base@Coins@350",
			"Base@GiftBox@1"
		};

		// Token: 0x04000989 RID: 2441
		public static readonly int[] mGBV_DailyKillInDeathMatchModeTarget = new int[]
		{
			10,
			50,
			100
		};

		// Token: 0x0400098A RID: 2442
		public static readonly string[] mGBV_DailyKillInDeathMatchModeReward = new string[]
		{
			"Base@Coins@50",
			"Base@Coins@100",
			"Base@Coins@100"
		};

		// Token: 0x0400098B RID: 2443
		public static readonly int[] mGBV_DailyJoinInStrongholdModeTarget = new int[]
		{
			1,
			3,
			6
		};

		// Token: 0x0400098C RID: 2444
		public static readonly string[] mGBV_DailyJoinInStrongholdModeReward = new string[]
		{
			"Base@Gems@2",
			"Base@Gems@2",
			"Base@Gems@2"
		};

		// Token: 0x0400098D RID: 2445
		public static readonly int[] mGBV_DailyJoinInKillingCompetitionModeTarget = new int[]
		{
			1,
			3,
			6
		};

		// Token: 0x0400098E RID: 2446
		public static readonly string[] mGBV_DailyJoinInKillingCompetitionModeReward = new string[]
		{
			"Base@Coins@50",
			"Base@Coins@100",
			"Base@Coins@100"
		};

		// Token: 0x0400098F RID: 2447
		public static readonly int[] mGBV_DailyJoinInExplosionModeTarget = new int[]
		{
			1,
			3,
			6
		};

		// Token: 0x04000990 RID: 2448
		public static readonly string[] mGBV_DailyJoinInExplosionModeReward = new string[]
		{
			"Base@Gems@2",
			"Base@Gems@2",
			"Base@Gems@2"
		};

		// Token: 0x04000991 RID: 2449
		public static readonly int[] mGBV_DailyJoinInMutationModeTarget = new int[]
		{
			1,
			3,
			6
		};

		// Token: 0x04000992 RID: 2450
		public static readonly string[] mGBV_DailyJoinInMutationModeReward = new string[]
		{
			"Potion@DamagePlusBuff@1",
			"Potion@HpPlusBuff@2",
			"Potion@SpeedPlusBuff@2"
		};

		// Token: 0x04000993 RID: 2451
		public static readonly int[] mGBV_DailyVideoShareTarget = new int[]
		{
			1,
			3
		};

		// Token: 0x04000994 RID: 2452
		public static readonly string[] mGBV_DailyVideoShareReward = new string[]
		{
			"Base@Gems@2",
			"Base@Gems@3\t"
		};
	}
}
