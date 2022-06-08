using System;

namespace GrowthSystem
{
	// Token: 0x020001CD RID: 461
	public class RewardCalcer
	{
		// Token: 0x06000C30 RID: 3120 RVA: 0x00057E3C File Offset: 0x0005623C
		public int GetLevelUpCoinsReward(int curLevel)
		{
			int num = 0;
			for (int i = 0; i < GrowthBaseValue.mGBV_BaseLevelLayoutMatrix.Length; i++)
			{
				num += GrowthBaseValue.mGBV_BaseLevelLayoutMatrix[i];
				if (curLevel <= num)
				{
					return GrowthBaseValue.mGBV_BaseLevelUpCoinsRewardMatrix[i] * 2;
				}
			}
			return 0;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00057E80 File Offset: 0x00056280
		public int GetSNSShareAfterLevelUpCoinsReward(int curLevel)
		{
			int num = 0;
			for (int i = 0; i < GrowthBaseValue.mGBV_BaseLevelLayoutMatrix.Length; i++)
			{
				num += GrowthBaseValue.mGBV_BaseLevelLayoutMatrix[i];
				if (curLevel <= num)
				{
					return GrowthBaseValue.mGBV_BaseLevelUpCoinsRewardMatrix[i] / 2;
				}
			}
			return 0;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00057EC3 File Offset: 0x000562C3
		public int GetAppstoreRatingCoinsReward()
		{
			return 300;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00057ECA File Offset: 0x000562CA
		public int GetSNSShareCoinsReward()
		{
			return 20;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00057ED0 File Offset: 0x000562D0
		public RewardUnitInfo GetRewardUnitInfo(FightingStatisticsTag tag)
		{
			RewardUnitInfo rewardUnitInfo = new RewardUnitInfo();
			switch (tag)
			{
			case FightingStatisticsTag.tTotalKillInWorldwideMultiplayer:
			{
				int i;
				for (i = 0; i < GrowthBaseValue.mGBV_WWMTotalKillTarget.Length; i++)
				{
					if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInWorldwideMultiplayer) < GrowthBaseValue.mGBV_WWMTotalKillTarget[i])
					{
						break;
					}
					if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tTotalKillInWorldwideMultiplayer, i + 1))
					{
						rewardUnitInfo.canGotReward = true;
						rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_WWMTotalKillTarget[i];
						rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInWorldwideMultiplayer);
						rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_WWMTotalKillReward[i];
						rewardUnitInfo.spriteName = "Coins_SlotLogo";
						rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
						rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardDescription = "Worldwide total killing number.";
						rewardUnitInfo.rewardHonorName = "Global Killer";
						rewardUnitInfo.progressRate = 100;
						rewardUnitInfo.rewardLv = i + 1;
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						return rewardUnitInfo;
					}
				}
				i = Math.Min(i, GrowthBaseValue.mGBV_WWMTotalKillTarget.Length - 1);
				rewardUnitInfo.rewardLv = i + 1;
				if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_WWMTotalKillTarget.Length)
				{
					rewardUnitInfo.rewardLvStr = "Max";
				}
				else
				{
					rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
				}
				rewardUnitInfo.canGotReward = false;
				rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_WWMTotalKillTarget[i];
				rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInWorldwideMultiplayer);
				if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
				{
					rewardUnitInfo.progressRate = 100;
				}
				else
				{
					rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
				}
				rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_WWMTotalKillReward[i];
				rewardUnitInfo.spriteName = "Coins_SlotLogo";
				rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
				rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardDescription = "Worldwide total killing number.";
				rewardUnitInfo.rewardHonorName = "Global Killer";
				break;
			}
			case FightingStatisticsTag.tTotalKillInLocalWifiMultiplayer:
			{
				int i;
				for (i = 0; i < GrowthBaseValue.mGBV_LWMTotalKillTarget.Length; i++)
				{
					if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInLocalWifiMultiplayer) < GrowthBaseValue.mGBV_LWMTotalKillTarget[i])
					{
						break;
					}
					if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tTotalKillInLocalWifiMultiplayer, i + 1))
					{
						rewardUnitInfo.canGotReward = true;
						rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_LWMTotalKillTarget[i];
						rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInLocalWifiMultiplayer);
						rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_LWMTotalKillReward[i];
						rewardUnitInfo.spriteName = "Coins_SlotLogo";
						rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
						rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardDescription = "Local WiFi total killing number.";
						rewardUnitInfo.rewardHonorName = "Local Killer";
						rewardUnitInfo.progressRate = 100;
						rewardUnitInfo.rewardLv = i + 1;
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						return rewardUnitInfo;
					}
				}
				i = Math.Min(i, GrowthBaseValue.mGBV_LWMTotalKillTarget.Length - 1);
				rewardUnitInfo.rewardLv = i + 1;
				if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_LWMTotalKillTarget.Length)
				{
					rewardUnitInfo.rewardLvStr = "Max";
				}
				else
				{
					rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
				}
				rewardUnitInfo.canGotReward = false;
				rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_LWMTotalKillTarget[i];
				rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInLocalWifiMultiplayer);
				if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
				{
					rewardUnitInfo.progressRate = 100;
				}
				else
				{
					rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
				}
				rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_LWMTotalKillReward[i];
				rewardUnitInfo.spriteName = "Coins_SlotLogo";
				rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
				rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardDescription = "Local WiFi total killing number.";
				rewardUnitInfo.rewardHonorName = "Local Killer";
				break;
			}
			case FightingStatisticsTag.tTotalHeadshotKill:
			{
				int i;
				for (i = 0; i < GrowthBaseValue.mGBV_TotalHeadshotKillTarget.Length; i++)
				{
					if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalHeadshotKill) < GrowthBaseValue.mGBV_TotalHeadshotKillTarget[i])
					{
						break;
					}
					if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tTotalHeadshotKill, i + 1))
					{
						rewardUnitInfo.canGotReward = true;
						rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalHeadshotKillTarget[i];
						rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalHeadshotKill);
						rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalHeadshotKillReward[i];
						rewardUnitInfo.spriteName = "Coins_SlotLogo";
						rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
						rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardDescription = "Headshot killing number.";
						rewardUnitInfo.rewardHonorName = "Headshot Killer";
						rewardUnitInfo.progressRate = 100;
						rewardUnitInfo.rewardLv = i + 1;
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						return rewardUnitInfo;
					}
				}
				i = Math.Min(i, GrowthBaseValue.mGBV_TotalHeadshotKillTarget.Length - 1);
				rewardUnitInfo.rewardLv = i + 1;
				if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_TotalHeadshotKillTarget.Length)
				{
					rewardUnitInfo.rewardLvStr = "Max";
				}
				else
				{
					rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
				}
				rewardUnitInfo.canGotReward = false;
				rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalHeadshotKillTarget[i];
				rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalHeadshotKill);
				if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
				{
					rewardUnitInfo.progressRate = 100;
				}
				else
				{
					rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
				}
				rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalHeadshotKillReward[i];
				rewardUnitInfo.spriteName = "Coins_SlotLogo";
				rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
				rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardDescription = "Headshot killing number.";
				rewardUnitInfo.rewardHonorName = "Headshot Killer";
				break;
			}
			case FightingStatisticsTag.tTotalTwoKill:
				break;
			case FightingStatisticsTag.tTotalFourKill:
				break;
			case FightingStatisticsTag.tTotalSixKill:
				break;
			case FightingStatisticsTag.tTotalEightKill:
				break;
			case FightingStatisticsTag.tTotalGoldLikeKill:
			{
				int i;
				for (i = 0; i < GrowthBaseValue.mGBV_TotalGoldLikeKillTarget.Length; i++)
				{
					if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKill) < GrowthBaseValue.mGBV_TotalGoldLikeKillTarget[i])
					{
						break;
					}
					if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tTotalGoldLikeKill, i + 1))
					{
						rewardUnitInfo.canGotReward = true;
						rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalGoldLikeKillTarget[i];
						rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKill);
						rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalGoldLikeKillReward[i];
						rewardUnitInfo.spriteName = "Coins_SlotLogo";
						rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
						rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardDescription = "God Like killing number.";
						rewardUnitInfo.rewardHonorName = "God Like Killer";
						rewardUnitInfo.progressRate = 100;
						rewardUnitInfo.rewardLv = i + 1;
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						return rewardUnitInfo;
					}
				}
				i = Math.Min(i, GrowthBaseValue.mGBV_TotalGoldLikeKillTarget.Length - 1);
				rewardUnitInfo.rewardLv = i + 1;
				if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_TotalGoldLikeKillTarget.Length)
				{
					rewardUnitInfo.rewardLvStr = "Max";
				}
				else
				{
					rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
				}
				rewardUnitInfo.canGotReward = false;
				rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalGoldLikeKillTarget[i];
				rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKill);
				if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
				{
					rewardUnitInfo.progressRate = 100;
				}
				else
				{
					rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
				}
				rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalGoldLikeKillReward[i];
				rewardUnitInfo.spriteName = "Coins_SlotLogo";
				rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
				rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardDescription = "God Like killing number.";
				rewardUnitInfo.rewardHonorName = "God Like Killer";
				break;
			}
			case FightingStatisticsTag.tMaxDeadOneRound:
			{
				int i;
				for (i = 0; i < GrowthBaseValue.mGBV_MaxDeadOneRoundTarget.Length; i++)
				{
					if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tMaxDeadOneRound) < GrowthBaseValue.mGBV_MaxDeadOneRoundTarget[i])
					{
						break;
					}
					if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tMaxDeadOneRound, i + 1))
					{
						rewardUnitInfo.canGotReward = true;
						rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_MaxDeadOneRoundTarget[i];
						rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tMaxDeadOneRound);
						rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_MaxDeadOneRoundReward[i];
						rewardUnitInfo.spriteName = "Coins_SlotLogo";
						rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
						rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardDescription = "Max dead numbers in one room.";
						rewardUnitInfo.rewardHonorName = "Bravest Loser";
						rewardUnitInfo.progressRate = 100;
						rewardUnitInfo.rewardLv = i + 1;
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						return rewardUnitInfo;
					}
				}
				i = Math.Min(i, GrowthBaseValue.mGBV_MaxDeadOneRoundTarget.Length - 1);
				rewardUnitInfo.rewardLv = i + 1;
				if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_MaxDeadOneRoundTarget.Length)
				{
					rewardUnitInfo.rewardLvStr = "Max";
				}
				else
				{
					rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
				}
				rewardUnitInfo.canGotReward = false;
				rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_MaxDeadOneRoundTarget[i];
				rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tMaxDeadOneRound);
				if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
				{
					rewardUnitInfo.progressRate = 100;
				}
				else
				{
					rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
				}
				rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_MaxDeadOneRoundReward[i];
				rewardUnitInfo.spriteName = "Coins_SlotLogo";
				rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
				rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardDescription = "Max dead numbers in one room.";
				rewardUnitInfo.rewardHonorName = "Bravest Loser";
				break;
			}
			case FightingStatisticsTag.tTotalStrongholdModeVictory:
			{
				int i;
				for (i = 0; i < GrowthBaseValue.mGBV_TotalStrongholdModeVictoryTarget.Length; i++)
				{
					if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictory) < GrowthBaseValue.mGBV_TotalStrongholdModeVictoryTarget[i])
					{
						break;
					}
					if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tTotalStrongholdModeVictory, i + 1))
					{
						rewardUnitInfo.canGotReward = true;
						rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalStrongholdModeVictoryTarget[i];
						rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictory);
						rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalStrongholdModeVictoryReward[i];
						rewardUnitInfo.spriteName = "Coins_SlotLogo";
						rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
						rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardDescription = "Stronghold mode victory number";
						rewardUnitInfo.rewardHonorName = "Stronghold Master";
						rewardUnitInfo.progressRate = 100;
						rewardUnitInfo.rewardLv = i + 1;
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						return rewardUnitInfo;
					}
				}
				i = Math.Min(i, GrowthBaseValue.mGBV_TotalStrongholdModeVictoryTarget.Length - 1);
				rewardUnitInfo.rewardLv = i + 1;
				if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_TotalStrongholdModeVictoryTarget.Length)
				{
					rewardUnitInfo.rewardLvStr = "Max";
				}
				else
				{
					rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
				}
				rewardUnitInfo.canGotReward = false;
				rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalStrongholdModeVictoryTarget[i];
				rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictory);
				if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
				{
					rewardUnitInfo.progressRate = 100;
				}
				else
				{
					rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
				}
				rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalStrongholdModeVictoryReward[i];
				rewardUnitInfo.spriteName = "Coins_SlotLogo";
				rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
				rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardDescription = "Stronghold mode victory number";
				rewardUnitInfo.rewardHonorName = "Stronghold Master";
				break;
			}
			case FightingStatisticsTag.tLevelUp:
			{
				int i;
				for (i = 0; i < 100; i++)
				{
					if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tLevelUp) < i + 1)
					{
						break;
					}
					if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tLevelUp, i + 1) && i + 1 != 100)
					{
						rewardUnitInfo.canGotReward = true;
						rewardUnitInfo.progressTargetValue = i + 1;
						rewardUnitInfo.progressCurValue = i + 1;
						rewardUnitInfo.rewardCoins = this.GetLevelUpCoinsReward(i + 1);
						rewardUnitInfo.spriteName = "Coins_SlotLogo";
						rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
						rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardDescription = "Level Up reward.";
						rewardUnitInfo.rewardHonorName = "Level Up";
						rewardUnitInfo.progressRate = 100;
						rewardUnitInfo.rewardLv = i + 1;
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						return rewardUnitInfo;
					}
				}
				i = Math.Min(i, 99);
				rewardUnitInfo.rewardLv = i + 1;
				if (rewardUnitInfo.rewardLv == 100)
				{
					rewardUnitInfo.rewardLvStr = "Max";
				}
				else
				{
					rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
				}
				rewardUnitInfo.canGotReward = false;
				rewardUnitInfo.progressTargetValue = i + 1;
				rewardUnitInfo.progressCurValue = i;
				if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
				{
					rewardUnitInfo.progressRate = 100;
				}
				else
				{
					rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
				}
				rewardUnitInfo.rewardCoins = this.GetLevelUpCoinsReward(i + 1);
				rewardUnitInfo.spriteName = "Coins_SlotLogo";
				rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
				rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardDescription = "Level Up reward.";
				rewardUnitInfo.rewardHonorName = "Level Up";
				break;
			}
			case FightingStatisticsTag.tTotalKillingCompetitionModeVictory:
			{
				int i;
				for (i = 0; i < GrowthBaseValue.mGBV_TotalKillingCompetitionModeVictoryTarget.Length; i++)
				{
					if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictory) < GrowthBaseValue.mGBV_TotalKillingCompetitionModeVictoryTarget[i])
					{
						break;
					}
					if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tTotalKillingCompetitionModeVictory, i + 1))
					{
						rewardUnitInfo.canGotReward = true;
						rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalKillingCompetitionModeVictoryTarget[i];
						rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictory);
						rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalKillingCompetitionModeVictoryReward[i];
						rewardUnitInfo.spriteName = "Coins_SlotLogo";
						rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
						rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardDescription = "Killing Competition mode victory number.";
						rewardUnitInfo.rewardHonorName = "Killing Competition Master";
						rewardUnitInfo.progressRate = 100;
						rewardUnitInfo.rewardLv = i + 1;
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						return rewardUnitInfo;
					}
				}
				i = Math.Min(i, GrowthBaseValue.mGBV_TotalKillingCompetitionModeVictoryTarget.Length - 1);
				rewardUnitInfo.rewardLv = i + 1;
				if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_TotalKillingCompetitionModeVictoryTarget.Length)
				{
					rewardUnitInfo.rewardLvStr = "Max";
				}
				else
				{
					rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
				}
				rewardUnitInfo.canGotReward = false;
				rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalKillingCompetitionModeVictoryTarget[i];
				rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictory);
				if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
				{
					rewardUnitInfo.progressRate = 100;
				}
				else
				{
					rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
				}
				rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalKillingCompetitionModeVictoryReward[i];
				rewardUnitInfo.spriteName = "Coins_SlotLogo";
				rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
				rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardDescription = "Killing Competition mode victory number.";
				rewardUnitInfo.rewardHonorName = "Killing Competition Master";
				break;
			}
			case FightingStatisticsTag.tTotalExplosionModeVictory:
			{
				int i;
				for (i = 0; i < GrowthBaseValue.mGBV_TotalExplosionModeVictoryTarget.Length; i++)
				{
					if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictory) < GrowthBaseValue.mGBV_TotalExplosionModeVictoryTarget[i])
					{
						break;
					}
					if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tTotalExplosionModeVictory, i + 1))
					{
						rewardUnitInfo.canGotReward = true;
						rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalExplosionModeVictoryTarget[i];
						rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictory);
						rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalExplosionModeVictoryReward[i];
						rewardUnitInfo.spriteName = "Coins_SlotLogo";
						rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
						rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardDescription = "Explosion mode victory number.";
						rewardUnitInfo.rewardHonorName = "Explosion Master";
						rewardUnitInfo.progressRate = 100;
						rewardUnitInfo.rewardLv = i + 1;
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						return rewardUnitInfo;
					}
				}
				i = Math.Min(i, GrowthBaseValue.mGBV_TotalExplosionModeVictoryTarget.Length - 1);
				rewardUnitInfo.rewardLv = i + 1;
				if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_TotalExplosionModeVictoryTarget.Length)
				{
					rewardUnitInfo.rewardLvStr = "Max";
				}
				else
				{
					rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
				}
				rewardUnitInfo.canGotReward = false;
				rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalExplosionModeVictoryTarget[i];
				rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictory);
				if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
				{
					rewardUnitInfo.progressRate = 100;
				}
				else
				{
					rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
				}
				rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalExplosionModeVictoryReward[i];
				rewardUnitInfo.spriteName = "Coins_SlotLogo";
				rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
				rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardDescription = "Explosion mode victory number.";
				rewardUnitInfo.rewardHonorName = "Explosion Master";
				break;
			}
			case FightingStatisticsTag.tTotalMutationModeVictory:
			{
				int i;
				for (i = 0; i < GrowthBaseValue.mGBV_TotalMutationModeVictoryTarget.Length; i++)
				{
					if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalMutationModeVictory) < GrowthBaseValue.mGBV_TotalMutationModeVictoryTarget[i])
					{
						break;
					}
					if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tTotalMutationModeVictory, i + 1))
					{
						rewardUnitInfo.canGotReward = true;
						rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalMutationModeVictoryTarget[i];
						rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalMutationModeVictory);
						rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalMutationModeVictoryReward[i];
						rewardUnitInfo.spriteName = "Coins_SlotLogo";
						rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
						rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardDescription = "Mutation mode victory number.";
						rewardUnitInfo.rewardHonorName = "Mutation Master";
						rewardUnitInfo.progressRate = 100;
						rewardUnitInfo.rewardLv = i + 1;
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						return rewardUnitInfo;
					}
				}
				i = Math.Min(i, GrowthBaseValue.mGBV_TotalMutationModeVictoryTarget.Length - 1);
				rewardUnitInfo.rewardLv = i + 1;
				if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_TotalMutationModeVictoryTarget.Length)
				{
					rewardUnitInfo.rewardLvStr = "Max";
				}
				else
				{
					rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
				}
				rewardUnitInfo.canGotReward = false;
				rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_TotalMutationModeVictoryTarget[i];
				rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalMutationModeVictory);
				if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
				{
					rewardUnitInfo.progressRate = 100;
				}
				else
				{
					rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
				}
				rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_TotalMutationModeVictoryReward[i];
				rewardUnitInfo.spriteName = "Coins_SlotLogo";
				rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
				rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardStrValue = "Base@Coins@" + rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardDescription = "Mutation mode victory number.";
				rewardUnitInfo.rewardHonorName = "Mutation Master";
				break;
			}
			case FightingStatisticsTag.tLvGrowthGift:
			{
				int i;
				for (i = 0; i < GrowthBaseValue.mGBV_LvGrowthGiftTarget.Length; i++)
				{
					if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tLvGrowthGift) < GrowthBaseValue.mGBV_LvGrowthGiftTarget[i])
					{
						break;
					}
					if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tLvGrowthGift, i + 1))
					{
						rewardUnitInfo.canGotReward = true;
						rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_LvGrowthGiftTarget[i];
						rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tLvGrowthGift);
						rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_LvGrowthGiftReward[i];
						rewardUnitInfo.spriteName = "Gems_SlotLogo";
						rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
						rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardStrValue = "Base@Gems@" + rewardUnitInfo.iNum.ToString();
						rewardUnitInfo.rewardDescription = "Receive gift when you reached the special level.";
						rewardUnitInfo.rewardHonorName = "Growth Gift";
						rewardUnitInfo.progressRate = 100;
						rewardUnitInfo.rewardLv = i + 1;
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						return rewardUnitInfo;
					}
				}
				i = Math.Min(i, GrowthBaseValue.mGBV_LvGrowthGiftTarget.Length - 1);
				rewardUnitInfo.rewardLv = i + 1;
				if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_LvGrowthGiftTarget.Length)
				{
					rewardUnitInfo.rewardLvStr = "Max";
				}
				else
				{
					rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
				}
				rewardUnitInfo.canGotReward = false;
				rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_LvGrowthGiftTarget[i];
				rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tLvGrowthGift);
				if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
				{
					rewardUnitInfo.progressRate = 100;
				}
				else
				{
					rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
				}
				rewardUnitInfo.rewardCoins = GrowthBaseValue.mGBV_LvGrowthGiftReward[i];
				rewardUnitInfo.spriteName = "Gems_SlotLogo";
				rewardUnitInfo.iNum = rewardUnitInfo.rewardCoins;
				rewardUnitInfo.sNum = rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardStrValue = "Base@Gems@" + rewardUnitInfo.iNum.ToString();
				rewardUnitInfo.rewardDescription = "Receive gift when you reached the special level.";
				rewardUnitInfo.rewardHonorName = "Growth Gift";
				break;
			}
			default:
				switch (tag)
				{
				case FightingStatisticsTag.tDailyKillInDeathMatchMode:
				{
					int i;
					for (i = 0; i < GrowthBaseValue.mGBV_DailyKillInDeathMatchModeTarget.Length; i++)
					{
						if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyKillInDeathMatchMode) < GrowthBaseValue.mGBV_DailyKillInDeathMatchModeTarget[i])
						{
							break;
						}
						if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tDailyKillInDeathMatchMode, i + 1))
						{
							rewardUnitInfo.canGotReward = true;
							rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyKillInDeathMatchModeTarget[i];
							rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyKillInDeathMatchMode);
							rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyKillInDeathMatchModeReward[i];
							rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[1] + "_SlotLogo";
							rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2];
							rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2]);
							rewardUnitInfo.rewardDescription = "Daily killing number in death match mode.";
							rewardUnitInfo.rewardHonorName = "Daily - Death Match";
							rewardUnitInfo.progressRate = 100;
							rewardUnitInfo.rewardLv = i + 1;
							rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
							return rewardUnitInfo;
						}
					}
					i = Math.Min(i, GrowthBaseValue.mGBV_DailyKillInDeathMatchModeTarget.Length - 1);
					rewardUnitInfo.rewardLv = i + 1;
					if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_DailyKillInDeathMatchModeTarget.Length)
					{
						rewardUnitInfo.rewardLvStr = "Max";
					}
					else
					{
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
					}
					rewardUnitInfo.canGotReward = false;
					rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyKillInDeathMatchModeTarget[i];
					rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyKillInDeathMatchMode);
					if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
					{
						rewardUnitInfo.progressRate = 100;
					}
					else
					{
						rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
					}
					rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyKillInDeathMatchModeReward[i];
					rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[1] + "_SlotLogo";
					rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[2];
					rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[2]);
					rewardUnitInfo.rewardDescription = "Daily killing number in death match mode.";
					rewardUnitInfo.rewardHonorName = "Daily - Death Match";
					break;
				}
				case FightingStatisticsTag.tDailyJoinInStrongholdMode:
				{
					int i;
					for (i = 0; i < GrowthBaseValue.mGBV_DailyJoinInStrongholdModeTarget.Length; i++)
					{
						if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInStrongholdMode) < GrowthBaseValue.mGBV_DailyJoinInStrongholdModeTarget[i])
						{
							break;
						}
						if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tDailyJoinInStrongholdMode, i + 1))
						{
							rewardUnitInfo.canGotReward = true;
							rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyJoinInStrongholdModeTarget[i];
							rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInStrongholdMode);
							rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyJoinInStrongholdModeReward[i];
							rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[1] + "_SlotLogo";
							rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2];
							rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2]);
							rewardUnitInfo.rewardDescription = "Daily stronghold mode complete number.";
							rewardUnitInfo.rewardHonorName = "Daily - Stronghold";
							rewardUnitInfo.progressRate = 100;
							rewardUnitInfo.rewardLv = i + 1;
							rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
							return rewardUnitInfo;
						}
					}
					i = Math.Min(i, GrowthBaseValue.mGBV_DailyJoinInStrongholdModeTarget.Length - 1);
					rewardUnitInfo.rewardLv = i + 1;
					if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_DailyJoinInStrongholdModeTarget.Length)
					{
						rewardUnitInfo.rewardLvStr = "Max";
					}
					else
					{
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
					}
					rewardUnitInfo.canGotReward = false;
					rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyJoinInStrongholdModeTarget[i];
					rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInStrongholdMode);
					if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
					{
						rewardUnitInfo.progressRate = 100;
					}
					else
					{
						rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
					}
					rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyJoinInStrongholdModeReward[i];
					rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[1] + "_SlotLogo";
					rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[2];
					rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[2]);
					rewardUnitInfo.rewardDescription = "Daily stronghold mode complete number.";
					rewardUnitInfo.rewardHonorName = "Daily - Stronghold";
					break;
				}
				case FightingStatisticsTag.tDailyJoinInKillingCompetitionMode:
				{
					int i;
					for (i = 0; i < GrowthBaseValue.mGBV_DailyJoinInKillingCompetitionModeTarget.Length; i++)
					{
						if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInKillingCompetitionMode) < GrowthBaseValue.mGBV_DailyJoinInKillingCompetitionModeTarget[i])
						{
							break;
						}
						if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tDailyJoinInKillingCompetitionMode, i + 1))
						{
							rewardUnitInfo.canGotReward = true;
							rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyJoinInKillingCompetitionModeTarget[i];
							rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInKillingCompetitionMode);
							rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyJoinInKillingCompetitionModeReward[i];
							rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[1] + "_SlotLogo";
							rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2];
							rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2]);
							rewardUnitInfo.rewardDescription = "Daily killing competition mode complete number.";
							rewardUnitInfo.rewardHonorName = "Daily - Killing Competition";
							rewardUnitInfo.progressRate = 100;
							rewardUnitInfo.rewardLv = i + 1;
							rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
							return rewardUnitInfo;
						}
					}
					i = Math.Min(i, GrowthBaseValue.mGBV_DailyJoinInKillingCompetitionModeTarget.Length - 1);
					rewardUnitInfo.rewardLv = i + 1;
					if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_DailyJoinInKillingCompetitionModeTarget.Length)
					{
						rewardUnitInfo.rewardLvStr = "Max";
					}
					else
					{
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
					}
					rewardUnitInfo.canGotReward = false;
					rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyJoinInKillingCompetitionModeTarget[i];
					rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInKillingCompetitionMode);
					if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
					{
						rewardUnitInfo.progressRate = 100;
					}
					else
					{
						rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
					}
					rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyJoinInKillingCompetitionModeReward[i];
					rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[1] + "_SlotLogo";
					rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[2];
					rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[2]);
					rewardUnitInfo.rewardDescription = "Daily killing competition mode complete number.";
					rewardUnitInfo.rewardHonorName = "Daily - Killing Competition";
					break;
				}
				case FightingStatisticsTag.tDailyJoinInExplosionMode:
				{
					int i;
					for (i = 0; i < GrowthBaseValue.mGBV_DailyJoinInExplosionModeTarget.Length; i++)
					{
						if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInExplosionMode) < GrowthBaseValue.mGBV_DailyJoinInExplosionModeTarget[i])
						{
							break;
						}
						if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tDailyJoinInExplosionMode, i + 1))
						{
							rewardUnitInfo.canGotReward = true;
							rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyJoinInExplosionModeTarget[i];
							rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInExplosionMode);
							rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyJoinInExplosionModeReward[i];
							rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[1] + "_SlotLogo";
							rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2];
							rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2]);
							rewardUnitInfo.rewardDescription = "Daily explosion mode complete number.";
							rewardUnitInfo.rewardHonorName = "Daily - Explosion";
							rewardUnitInfo.progressRate = 100;
							rewardUnitInfo.rewardLv = i + 1;
							rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
							return rewardUnitInfo;
						}
					}
					i = Math.Min(i, GrowthBaseValue.mGBV_DailyJoinInExplosionModeTarget.Length - 1);
					rewardUnitInfo.rewardLv = i + 1;
					if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_DailyJoinInExplosionModeTarget.Length)
					{
						rewardUnitInfo.rewardLvStr = "Max";
					}
					else
					{
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
					}
					rewardUnitInfo.canGotReward = false;
					rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyJoinInExplosionModeTarget[i];
					rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInExplosionMode);
					if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
					{
						rewardUnitInfo.progressRate = 100;
					}
					else
					{
						rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
					}
					rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyJoinInExplosionModeReward[i];
					rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[1] + "_SlotLogo";
					rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[2];
					rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[2]);
					rewardUnitInfo.rewardDescription = "Daily explosion mode complete number.";
					rewardUnitInfo.rewardHonorName = "Daily - Explosion";
					break;
				}
				case FightingStatisticsTag.tDailyJoinInMutationMode:
				{
					int i;
					for (i = 0; i < GrowthBaseValue.mGBV_DailyJoinInMutationModeTarget.Length; i++)
					{
						if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInMutationMode) < GrowthBaseValue.mGBV_DailyJoinInMutationModeTarget[i])
						{
							break;
						}
						if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tDailyJoinInMutationMode, i + 1))
						{
							rewardUnitInfo.canGotReward = true;
							rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyJoinInMutationModeTarget[i];
							rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInMutationMode);
							rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyJoinInMutationModeReward[i];
							rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[1] + "_SlotLogo";
							rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2];
							rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2]);
							rewardUnitInfo.rewardDescription = "Daily mutation mode complete number.";
							rewardUnitInfo.rewardHonorName = "Daily - Mutation";
							rewardUnitInfo.progressRate = 100;
							rewardUnitInfo.rewardLv = i + 1;
							rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
							return rewardUnitInfo;
						}
					}
					i = Math.Min(i, GrowthBaseValue.mGBV_DailyJoinInMutationModeTarget.Length - 1);
					rewardUnitInfo.rewardLv = i + 1;
					if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_DailyJoinInMutationModeTarget.Length)
					{
						rewardUnitInfo.rewardLvStr = "Max";
					}
					else
					{
						rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
					}
					rewardUnitInfo.canGotReward = false;
					rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyJoinInMutationModeTarget[i];
					rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyJoinInMutationMode);
					if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
					{
						rewardUnitInfo.progressRate = 100;
					}
					else
					{
						rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
					}
					rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyJoinInMutationModeReward[i];
					rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[1] + "_SlotLogo";
					rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[2];
					rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
					{
						'@'
					})[2]);
					rewardUnitInfo.rewardDescription = "Daily mutation mode complete number.";
					rewardUnitInfo.rewardHonorName = "Daily - Mutation";
					break;
				}
				default:
					if (tag != FightingStatisticsTag.tDailyLoginInSevenDays)
					{
						if (tag == FightingStatisticsTag.tDailyVideoShare)
						{
							int i;
							for (i = 0; i < GrowthBaseValue.mGBV_DailyVideoShareTarget.Length; i++)
							{
								if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyVideoShare) < GrowthBaseValue.mGBV_DailyVideoShareTarget[i])
								{
									break;
								}
								if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tDailyVideoShare, i + 1))
								{
									rewardUnitInfo.canGotReward = true;
									rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyVideoShareTarget[i];
									rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyVideoShare);
									rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyVideoShareReward[i];
									rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
									{
										'@'
									})[1] + "_SlotLogo";
									rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
									{
										'@'
									})[2];
									rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
									{
										'@'
									})[2]);
									rewardUnitInfo.rewardDescription = "Daily video sharing complete number.";
									rewardUnitInfo.rewardHonorName = "Daily - Video Sharing";
									rewardUnitInfo.progressRate = 100;
									rewardUnitInfo.rewardLv = i + 1;
									rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
									return rewardUnitInfo;
								}
							}
							i = Math.Min(i, GrowthBaseValue.mGBV_DailyVideoShareTarget.Length - 1);
							rewardUnitInfo.rewardLv = i + 1;
							if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_DailyVideoShareTarget.Length)
							{
								rewardUnitInfo.rewardLvStr = "Max";
							}
							else
							{
								rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
							}
							rewardUnitInfo.canGotReward = false;
							rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyVideoShareTarget[i];
							rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyVideoShare);
							if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
							{
								rewardUnitInfo.progressRate = 100;
							}
							else
							{
								rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
							}
							rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyVideoShareReward[i];
							rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[1] + "_SlotLogo";
							rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2];
							rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
							{
								'@'
							})[2]);
							rewardUnitInfo.rewardDescription = "Daily video sharing complete number.";
							rewardUnitInfo.rewardHonorName = "Daily - Video Sharing";
						}
					}
					else
					{
						int i;
						for (i = 0; i < GrowthBaseValue.mGBV_DailyLoginInSevenDaysTarget.Length; i++)
						{
							if (UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyLoginInSevenDays) < GrowthBaseValue.mGBV_DailyLoginInSevenDaysTarget[i])
							{
								break;
							}
							if (!UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tDailyLoginInSevenDays, i + 1))
							{
								rewardUnitInfo.canGotReward = true;
								rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyLoginInSevenDaysTarget[i];
								rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyLoginInSevenDays);
								rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyLoginInSevenDaysReward[i];
								rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
								{
									'@'
								})[1] + "_SlotLogo";
								rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
								{
									'@'
								})[2];
								rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
								{
									'@'
								})[2]);
								rewardUnitInfo.rewardDescription = "Continuous login days: get reward everyday.";
								rewardUnitInfo.rewardHonorName = "Continuous Login";
								rewardUnitInfo.progressRate = 100;
								rewardUnitInfo.rewardLv = i + 1;
								rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
								return rewardUnitInfo;
							}
						}
						i = Math.Min(i, GrowthBaseValue.mGBV_DailyLoginInSevenDaysTarget.Length - 1);
						rewardUnitInfo.rewardLv = i + 1;
						if (rewardUnitInfo.rewardLv == GrowthBaseValue.mGBV_DailyLoginInSevenDaysTarget.Length)
						{
							rewardUnitInfo.rewardLvStr = "Max";
						}
						else
						{
							rewardUnitInfo.rewardLvStr = rewardUnitInfo.rewardLv.ToString();
						}
						rewardUnitInfo.canGotReward = false;
						rewardUnitInfo.progressTargetValue = GrowthBaseValue.mGBV_DailyLoginInSevenDaysTarget[i];
						rewardUnitInfo.progressCurValue = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tDailyLoginInSevenDays);
						if (rewardUnitInfo.progressCurValue >= rewardUnitInfo.progressTargetValue)
						{
							rewardUnitInfo.progressRate = 100;
						}
						else
						{
							rewardUnitInfo.progressRate = rewardUnitInfo.progressCurValue * 100 / rewardUnitInfo.progressTargetValue;
						}
						rewardUnitInfo.rewardStrValue = GrowthBaseValue.mGBV_DailyLoginInSevenDaysReward[i];
						rewardUnitInfo.spriteName = rewardUnitInfo.rewardStrValue.Split(new char[]
						{
							'@'
						})[1] + "_SlotLogo";
						rewardUnitInfo.sNum = rewardUnitInfo.rewardStrValue.Split(new char[]
						{
							'@'
						})[2];
						rewardUnitInfo.iNum = int.Parse(rewardUnitInfo.rewardStrValue.Split(new char[]
						{
							'@'
						})[2]);
						rewardUnitInfo.rewardDescription = "Continuous login days: get reward everyday.";
						rewardUnitInfo.rewardHonorName = "Continuous Login";
						rewardUnitInfo.displayInfoList = new RewardUnitInfo.RewardUnitDisplayInfo[GrowthBaseValue.mGBV_DailyLoginInSevenDaysReward.Length];
						for (i = 0; i < rewardUnitInfo.displayInfoList.Length; i++)
						{
							string text = GrowthBaseValue.mGBV_DailyLoginInSevenDaysReward[i];
							rewardUnitInfo.displayInfoList[i].spriteName = text.Split(new char[]
							{
								'@'
							})[1] + "_SlotLogo";
							rewardUnitInfo.displayInfoList[i].sNum = text.Split(new char[]
							{
								'@'
							})[2];
						}
					}
					break;
				}
				break;
			}
			return rewardUnitInfo;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0005A770 File Offset: 0x00058B70
		public void ResetDailyRewardRecord(FightingStatisticsTag tag)
		{
			if (!tag.ToString().Contains("Daily"))
			{
				return;
			}
			switch (tag)
			{
			case FightingStatisticsTag.tDailyKillInDeathMatchMode:
				for (int i = 1; i <= GrowthBaseValue.mGBV_DailyKillInDeathMatchModeTarget.Length; i++)
				{
					UserDataController.ClearFightingStatisticsRewardGotRecord(tag, i);
					UserDataController.SetFightingStatisticsValue(tag, 0);
				}
				break;
			case FightingStatisticsTag.tDailyJoinInStrongholdMode:
				for (int i = 1; i <= GrowthBaseValue.mGBV_DailyJoinInStrongholdModeTarget.Length; i++)
				{
					UserDataController.ClearFightingStatisticsRewardGotRecord(tag, i);
					UserDataController.SetFightingStatisticsValue(tag, 0);
				}
				break;
			case FightingStatisticsTag.tDailyJoinInKillingCompetitionMode:
				for (int i = 1; i <= GrowthBaseValue.mGBV_DailyJoinInKillingCompetitionModeTarget.Length; i++)
				{
					UserDataController.ClearFightingStatisticsRewardGotRecord(tag, i);
					UserDataController.SetFightingStatisticsValue(tag, 0);
				}
				break;
			case FightingStatisticsTag.tDailyJoinInExplosionMode:
				for (int i = 1; i <= GrowthBaseValue.mGBV_DailyJoinInExplosionModeTarget.Length; i++)
				{
					UserDataController.ClearFightingStatisticsRewardGotRecord(tag, i);
					UserDataController.SetFightingStatisticsValue(tag, 0);
				}
				break;
			case FightingStatisticsTag.tDailyJoinInMutationMode:
				for (int i = 1; i <= GrowthBaseValue.mGBV_DailyJoinInMutationModeTarget.Length; i++)
				{
					UserDataController.ClearFightingStatisticsRewardGotRecord(tag, i);
					UserDataController.SetFightingStatisticsValue(tag, 0);
				}
				break;
			default:
				if (tag != FightingStatisticsTag.tDailyLoginInSevenDays)
				{
					if (tag == FightingStatisticsTag.tDailyVideoShare)
					{
						for (int i = 1; i <= GrowthBaseValue.mGBV_DailyVideoShareTarget.Length; i++)
						{
							UserDataController.ClearFightingStatisticsRewardGotRecord(tag, i);
							UserDataController.SetFightingStatisticsValue(tag, 0);
						}
					}
				}
				else
				{
					for (int i = 1; i <= GrowthBaseValue.mGBV_DailyLoginInSevenDaysTarget.Length; i++)
					{
						UserDataController.ClearFightingStatisticsRewardGotRecord(tag, i);
						UserDataController.SetFightingStatisticsValue(tag, 1);
					}
				}
				break;
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0005A900 File Offset: 0x00058D00
		public GrowthGameRatingTag GetOncePlayRating(GameValueForRatingCalc gameValue)
		{
			GrowthGameRatingTag[] array = new GrowthGameRatingTag[30];
			int num = 0;
			if (gameValue.mGameMode == GrowthGameModeTag.tStronghold)
			{
				num = gameValue.mShModeValue.enemyTeamTotalPlayerNum + gameValue.mShModeValue.myTeamTotalPlayerNum;
			}
			else if (gameValue.mGameMode == GrowthGameModeTag.tKillingCompetition)
			{
				num = gameValue.mKCModeValue.enemyTeamTotalPlayerNum + gameValue.mKCModeValue.myTeamTotalPlayerNum;
			}
			else if (gameValue.mGameMode == GrowthGameModeTag.tExplosion)
			{
				num = gameValue.mEPModeValue.enemyTeamTotalPlayerNum + gameValue.mEPModeValue.myTeamTotalPlayerNum;
			}
			else if (gameValue.mGameMode == GrowthGameModeTag.tMutation)
			{
				num = gameValue.mMTModeValue.enemyTeamTotalPlayerNum + gameValue.mMTModeValue.myTeamTotalPlayerNum;
			}
			else if (gameValue.mGameMode == GrowthGameModeTag.tKnifeCompetition)
			{
				num = gameValue.mKNModeValue.enemyTeamTotalPlayerNum + gameValue.mKNModeValue.myTeamTotalPlayerNum;
			}
			if (num <= 2)
			{
				array[0] = GrowthGameRatingTag.RatingTag_E;
				array[1] = GrowthGameRatingTag.RatingTag_F;
				for (int i = 2; i < array.Length; i++)
				{
					array[i] = GrowthGameRatingTag.RatingTag_F;
				}
			}
			else if (num <= 4)
			{
				array[0] = GrowthGameRatingTag.RatingTag_D;
				array[1] = GrowthGameRatingTag.RatingTag_E;
				array[2] = GrowthGameRatingTag.RatingTag_F;
				array[3] = GrowthGameRatingTag.RatingTag_F;
				for (int j = 4; j < array.Length; j++)
				{
					array[j] = GrowthGameRatingTag.RatingTag_F;
				}
			}
			else if (num <= 6)
			{
				array[0] = GrowthGameRatingTag.RatingTag_B;
				array[1] = GrowthGameRatingTag.RatingTag_C;
				array[2] = GrowthGameRatingTag.RatingTag_D;
				array[3] = GrowthGameRatingTag.RatingTag_E;
				array[4] = GrowthGameRatingTag.RatingTag_F;
				array[5] = GrowthGameRatingTag.RatingTag_F;
				for (int k = 6; k < array.Length; k++)
				{
					array[k] = GrowthGameRatingTag.RatingTag_F;
				}
			}
			else if (num <= 8)
			{
				array[0] = GrowthGameRatingTag.RatingTag_A;
				array[1] = GrowthGameRatingTag.RatingTag_B;
				array[2] = GrowthGameRatingTag.RatingTag_C;
				array[3] = GrowthGameRatingTag.RatingTag_C;
				array[4] = GrowthGameRatingTag.RatingTag_D;
				array[5] = GrowthGameRatingTag.RatingTag_E;
				array[6] = GrowthGameRatingTag.RatingTag_F;
				array[7] = GrowthGameRatingTag.RatingTag_F;
				for (int l = 8; l < array.Length; l++)
				{
					array[l] = GrowthGameRatingTag.RatingTag_F;
				}
			}
			else if (num <= 10)
			{
				if (gameValue.mGameMode == GrowthGameModeTag.tExplosion)
				{
					array[0] = GrowthGameRatingTag.RatingTag_S;
					array[1] = GrowthGameRatingTag.RatingTag_A;
					array[2] = GrowthGameRatingTag.RatingTag_B;
					array[3] = GrowthGameRatingTag.RatingTag_B;
					array[4] = GrowthGameRatingTag.RatingTag_C;
					array[5] = GrowthGameRatingTag.RatingTag_C;
					array[6] = GrowthGameRatingTag.RatingTag_D;
					array[7] = GrowthGameRatingTag.RatingTag_D;
					array[8] = GrowthGameRatingTag.RatingTag_E;
					array[9] = GrowthGameRatingTag.RatingTag_F;
					for (int m = 10; m < array.Length; m++)
					{
						array[m] = GrowthGameRatingTag.RatingTag_F;
					}
				}
				else
				{
					array[0] = GrowthGameRatingTag.RatingTag_A;
					array[1] = GrowthGameRatingTag.RatingTag_B;
					array[2] = GrowthGameRatingTag.RatingTag_B;
					array[3] = GrowthGameRatingTag.RatingTag_C;
					array[4] = GrowthGameRatingTag.RatingTag_C;
					array[5] = GrowthGameRatingTag.RatingTag_D;
					array[6] = GrowthGameRatingTag.RatingTag_D;
					array[7] = GrowthGameRatingTag.RatingTag_E;
					array[8] = GrowthGameRatingTag.RatingTag_F;
					array[9] = GrowthGameRatingTag.RatingTag_F;
					for (int n = 10; n < array.Length; n++)
					{
						array[n] = GrowthGameRatingTag.RatingTag_F;
					}
				}
			}
			else if (num <= 12)
			{
				array[0] = GrowthGameRatingTag.RatingTag_S;
				array[1] = GrowthGameRatingTag.RatingTag_A;
				array[2] = GrowthGameRatingTag.RatingTag_B;
				array[3] = GrowthGameRatingTag.RatingTag_B;
				array[4] = GrowthGameRatingTag.RatingTag_B;
				array[5] = GrowthGameRatingTag.RatingTag_C;
				array[6] = GrowthGameRatingTag.RatingTag_C;
				array[7] = GrowthGameRatingTag.RatingTag_D;
				array[8] = GrowthGameRatingTag.RatingTag_D;
				array[9] = GrowthGameRatingTag.RatingTag_E;
				array[10] = GrowthGameRatingTag.RatingTag_F;
				array[11] = GrowthGameRatingTag.RatingTag_F;
				for (int num2 = 12; num2 < array.Length; num2++)
				{
					array[num2] = GrowthGameRatingTag.RatingTag_F;
				}
			}
			else if (num <= 16)
			{
				array[0] = GrowthGameRatingTag.RatingTag_S;
				array[1] = GrowthGameRatingTag.RatingTag_A;
				array[2] = GrowthGameRatingTag.RatingTag_A;
				array[3] = GrowthGameRatingTag.RatingTag_A;
				array[4] = GrowthGameRatingTag.RatingTag_B;
				array[5] = GrowthGameRatingTag.RatingTag_B;
				array[6] = GrowthGameRatingTag.RatingTag_C;
				array[7] = GrowthGameRatingTag.RatingTag_C;
				array[8] = GrowthGameRatingTag.RatingTag_C;
				array[9] = GrowthGameRatingTag.RatingTag_D;
				array[10] = GrowthGameRatingTag.RatingTag_D;
				array[11] = GrowthGameRatingTag.RatingTag_E;
				array[12] = GrowthGameRatingTag.RatingTag_E;
				array[13] = GrowthGameRatingTag.RatingTag_F;
				array[14] = GrowthGameRatingTag.RatingTag_F;
				array[15] = GrowthGameRatingTag.RatingTag_F;
				for (int num3 = 16; num3 < array.Length; num3++)
				{
					array[num3] = GrowthGameRatingTag.RatingTag_F;
				}
			}
			else if (num <= 30)
			{
				array[0] = GrowthGameRatingTag.RatingTag_S;
				array[1] = GrowthGameRatingTag.RatingTag_S;
				array[2] = GrowthGameRatingTag.RatingTag_A;
				array[3] = GrowthGameRatingTag.RatingTag_A;
				array[4] = GrowthGameRatingTag.RatingTag_A;
				array[5] = GrowthGameRatingTag.RatingTag_B;
				array[6] = GrowthGameRatingTag.RatingTag_B;
				array[7] = GrowthGameRatingTag.RatingTag_B;
				array[8] = GrowthGameRatingTag.RatingTag_C;
				array[9] = GrowthGameRatingTag.RatingTag_C;
				array[10] = GrowthGameRatingTag.RatingTag_C;
				array[11] = GrowthGameRatingTag.RatingTag_D;
				array[12] = GrowthGameRatingTag.RatingTag_D;
				array[13] = GrowthGameRatingTag.RatingTag_E;
				array[14] = GrowthGameRatingTag.RatingTag_E;
				array[15] = GrowthGameRatingTag.RatingTag_E;
				array[16] = GrowthGameRatingTag.RatingTag_F;
				array[17] = GrowthGameRatingTag.RatingTag_F;
				array[18] = GrowthGameRatingTag.RatingTag_F;
				array[19] = GrowthGameRatingTag.RatingTag_F;
				for (int num4 = 20; num4 < array.Length; num4++)
				{
					array[num4] = GrowthGameRatingTag.RatingTag_F;
				}
			}
			if (gameValue.mGameMode == GrowthGameModeTag.tStronghold)
			{
				if (gameValue.score <= 0)
				{
					return array[array.Length - 1];
				}
				if (gameValue.mShModeValue.isWinner && gameValue.mShModeValue.rankInMyTeam == 1)
				{
					return array[0];
				}
				if (gameValue.rankInAll <= array.Length && gameValue.rankInAll >= 1)
				{
					return array[gameValue.rankInAll - 1];
				}
				return array[array.Length - 1];
			}
			else if (gameValue.mGameMode == GrowthGameModeTag.tKillingCompetition)
			{
				if (gameValue.score <= 0)
				{
					return array[array.Length - 1];
				}
				if (gameValue.mKCModeValue.isWinner && gameValue.mKCModeValue.rankInMyTeam == 1)
				{
					return array[0];
				}
				if (gameValue.rankInAll <= array.Length && gameValue.rankInAll >= 1)
				{
					return array[gameValue.rankInAll - 1];
				}
				return array[array.Length - 1];
			}
			else if (gameValue.mGameMode == GrowthGameModeTag.tExplosion)
			{
				if (gameValue.score <= 0)
				{
					return array[array.Length - 1];
				}
				if (gameValue.mEPModeValue.isWinner && gameValue.mEPModeValue.rankInMyTeam == 1)
				{
					return array[0];
				}
				if (gameValue.rankInAll <= array.Length && gameValue.rankInAll >= 1)
				{
					return array[gameValue.rankInAll - 1];
				}
				return array[array.Length - 1];
			}
			else if (gameValue.mGameMode == GrowthGameModeTag.tMutation)
			{
				if (gameValue.score <= 0)
				{
					return array[array.Length - 1];
				}
				if (gameValue.mMTModeValue.isWinner && gameValue.mMTModeValue.rankInMyTeam == 1)
				{
					return array[0];
				}
				if (gameValue.rankInAll <= array.Length && gameValue.rankInAll >= 1)
				{
					return array[gameValue.rankInAll - 1];
				}
				return array[array.Length - 1];
			}
			else if (gameValue.mGameMode == GrowthGameModeTag.tKnifeCompetition)
			{
				if (gameValue.score <= 0)
				{
					return array[array.Length - 1];
				}
				if (gameValue.mKNModeValue.isWinner && gameValue.mKNModeValue.rankInMyTeam == 1)
				{
					return array[0];
				}
				if (gameValue.rankInAll <= array.Length && gameValue.rankInAll >= 1)
				{
					return array[gameValue.rankInAll - 1];
				}
				return array[array.Length - 1];
			}
			else
			{
				if (gameValue.mGameMode != GrowthGameModeTag.tHunting)
				{
					return GrowthGameRatingTag.RatingTag_Nil;
				}
				if (gameValue.mHTModeValue.PassTime <= 240)
				{
					return GrowthGameRatingTag.RatingTag_S;
				}
				if (gameValue.mHTModeValue.PassTime <= 300)
				{
					return GrowthGameRatingTag.RatingTag_A;
				}
				return GrowthGameRatingTag.RatingTag_B;
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0005AF40 File Offset: 0x00059340
		public int GetOncePlaySeasonScoreReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating, bool isWinner)
		{
			int num = 0;
			int num2 = 0;
			switch (gameMode)
			{
			case GrowthGameModeTag.tTeamDeathMatch:
				num2 = GrowthBaseValue.mGBV_ModeValueForSeasonScore[0];
				break;
			case GrowthGameModeTag.tStronghold:
				num2 = GrowthBaseValue.mGBV_ModeValueForSeasonScore[1];
				break;
			case GrowthGameModeTag.tKillingCompetition:
				num2 = GrowthBaseValue.mGBV_ModeValueForSeasonScore[2];
				break;
			case GrowthGameModeTag.tExplosion:
				num2 = GrowthBaseValue.mGBV_ModeValueForSeasonScore[3];
				break;
			case GrowthGameModeTag.tMutation:
				num2 = GrowthBaseValue.mGBV_ModeValueForSeasonScore[4];
				break;
			case GrowthGameModeTag.tKnifeCompetition:
				num2 = GrowthBaseValue.mGBV_ModeValueForSeasonScore[5];
				break;
			}
			switch (gameRating)
			{
			case GrowthGameRatingTag.RatingTag_Nil:
				num = ((!isWinner) ? GrowthBaseValue.mGBV_RatingValueForSeasonScoreLoser[0] : GrowthBaseValue.mGBV_RatingValueForSeasonScoreWinner[0]);
				break;
			case GrowthGameRatingTag.RatingTag_F:
				num = ((!isWinner) ? GrowthBaseValue.mGBV_RatingValueForSeasonScoreLoser[1] : GrowthBaseValue.mGBV_RatingValueForSeasonScoreWinner[1]);
				break;
			case GrowthGameRatingTag.RatingTag_E:
				num = ((!isWinner) ? GrowthBaseValue.mGBV_RatingValueForSeasonScoreLoser[2] : GrowthBaseValue.mGBV_RatingValueForSeasonScoreWinner[2]);
				break;
			case GrowthGameRatingTag.RatingTag_D:
				num = ((!isWinner) ? GrowthBaseValue.mGBV_RatingValueForSeasonScoreLoser[3] : GrowthBaseValue.mGBV_RatingValueForSeasonScoreWinner[3]);
				break;
			case GrowthGameRatingTag.RatingTag_C:
				num = ((!isWinner) ? GrowthBaseValue.mGBV_RatingValueForSeasonScoreLoser[4] : GrowthBaseValue.mGBV_RatingValueForSeasonScoreWinner[4]);
				break;
			case GrowthGameRatingTag.RatingTag_B:
				num = ((!isWinner) ? GrowthBaseValue.mGBV_RatingValueForSeasonScoreLoser[5] : GrowthBaseValue.mGBV_RatingValueForSeasonScoreWinner[5]);
				break;
			case GrowthGameRatingTag.RatingTag_A:
				num = ((!isWinner) ? GrowthBaseValue.mGBV_RatingValueForSeasonScoreLoser[6] : GrowthBaseValue.mGBV_RatingValueForSeasonScoreWinner[6]);
				break;
			case GrowthGameRatingTag.RatingTag_S:
				num = ((!isWinner) ? GrowthBaseValue.mGBV_RatingValueForSeasonScoreLoser[7] : GrowthBaseValue.mGBV_RatingValueForSeasonScoreWinner[7]);
				break;
			}
			return num * num2;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0005B0F4 File Offset: 0x000594F4
		public int GetOncePlayHonorPointReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating)
		{
			int num = 0;
			int num2 = 0;
			switch (gameMode)
			{
			case GrowthGameModeTag.tTeamDeathMatch:
				num2 = GrowthBaseValue.mGBV_ModeValueForHonorPoint[0];
				break;
			case GrowthGameModeTag.tStronghold:
				num2 = GrowthBaseValue.mGBV_ModeValueForHonorPoint[1];
				break;
			case GrowthGameModeTag.tKillingCompetition:
				num2 = GrowthBaseValue.mGBV_ModeValueForHonorPoint[2];
				break;
			case GrowthGameModeTag.tExplosion:
				num2 = GrowthBaseValue.mGBV_ModeValueForHonorPoint[3];
				break;
			case GrowthGameModeTag.tMutation:
				num2 = GrowthBaseValue.mGBV_ModeValueForHonorPoint[4];
				break;
			case GrowthGameModeTag.tKnifeCompetition:
				num2 = GrowthBaseValue.mGBV_ModeValueForHonorPoint[5];
				break;
			}
			switch (gameRating)
			{
			case GrowthGameRatingTag.RatingTag_Nil:
				num = GrowthBaseValue.mGBV_RatingValueForHonorPoint[0];
				break;
			case GrowthGameRatingTag.RatingTag_F:
				num = GrowthBaseValue.mGBV_RatingValueForHonorPoint[1];
				break;
			case GrowthGameRatingTag.RatingTag_E:
				num = GrowthBaseValue.mGBV_RatingValueForHonorPoint[2];
				break;
			case GrowthGameRatingTag.RatingTag_D:
				num = GrowthBaseValue.mGBV_RatingValueForHonorPoint[3];
				break;
			case GrowthGameRatingTag.RatingTag_C:
				num = GrowthBaseValue.mGBV_RatingValueForHonorPoint[4];
				break;
			case GrowthGameRatingTag.RatingTag_B:
				num = GrowthBaseValue.mGBV_RatingValueForHonorPoint[5];
				break;
			case GrowthGameRatingTag.RatingTag_A:
				num = GrowthBaseValue.mGBV_RatingValueForHonorPoint[6];
				break;
			case GrowthGameRatingTag.RatingTag_S:
				num = GrowthBaseValue.mGBV_RatingValueForHonorPoint[7];
				break;
			}
			return num * num2;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0005B218 File Offset: 0x00059618
		public int GetOncePlayExpReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating)
		{
			int num = 0;
			int num2 = 0;
			switch (gameMode)
			{
			case GrowthGameModeTag.tTeamDeathMatch:
				num2 = GrowthBaseValue.mGBV_ModeValueForExp[0];
				break;
			case GrowthGameModeTag.tStronghold:
				num2 = GrowthBaseValue.mGBV_ModeValueForExp[1];
				break;
			case GrowthGameModeTag.tKillingCompetition:
				num2 = GrowthBaseValue.mGBV_ModeValueForExp[2];
				break;
			case GrowthGameModeTag.tExplosion:
				num2 = GrowthBaseValue.mGBV_ModeValueForExp[3];
				break;
			case GrowthGameModeTag.tMutation:
				num2 = GrowthBaseValue.mGBV_ModeValueForExp[4];
				break;
			case GrowthGameModeTag.tKnifeCompetition:
				num2 = GrowthBaseValue.mGBV_ModeValueForExp[5];
				break;
			case GrowthGameModeTag.tHunting:
				num2 = GrowthBaseValue.mGBV_ModeValueForExp[6];
				break;
			}
			switch (gameRating)
			{
			case GrowthGameRatingTag.RatingTag_Nil:
				num = GrowthBaseValue.mGBV_RatingValueForExp[0];
				break;
			case GrowthGameRatingTag.RatingTag_F:
				num = GrowthBaseValue.mGBV_RatingValueForExp[1];
				break;
			case GrowthGameRatingTag.RatingTag_E:
				num = GrowthBaseValue.mGBV_RatingValueForExp[2];
				break;
			case GrowthGameRatingTag.RatingTag_D:
				num = GrowthBaseValue.mGBV_RatingValueForExp[3];
				break;
			case GrowthGameRatingTag.RatingTag_C:
				num = GrowthBaseValue.mGBV_RatingValueForExp[4];
				break;
			case GrowthGameRatingTag.RatingTag_B:
				num = GrowthBaseValue.mGBV_RatingValueForExp[5];
				break;
			case GrowthGameRatingTag.RatingTag_A:
				num = GrowthBaseValue.mGBV_RatingValueForExp[6];
				break;
			case GrowthGameRatingTag.RatingTag_S:
				num = GrowthBaseValue.mGBV_RatingValueForExp[7];
				break;
			}
			return num * num2;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0005B34C File Offset: 0x0005974C
		public int GetOncePlayCoinsReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating)
		{
			int num = 0;
			int num2 = 0;
			switch (gameMode)
			{
			case GrowthGameModeTag.tTeamDeathMatch:
				num2 = GrowthBaseValue.mGBV_ModeValueForCoin[0];
				break;
			case GrowthGameModeTag.tStronghold:
				num2 = GrowthBaseValue.mGBV_ModeValueForCoin[1];
				break;
			case GrowthGameModeTag.tKillingCompetition:
				num2 = GrowthBaseValue.mGBV_ModeValueForCoin[2];
				break;
			case GrowthGameModeTag.tExplosion:
				num2 = GrowthBaseValue.mGBV_ModeValueForCoin[3];
				break;
			case GrowthGameModeTag.tMutation:
				num2 = GrowthBaseValue.mGBV_ModeValueForCoin[4];
				break;
			case GrowthGameModeTag.tKnifeCompetition:
				num2 = GrowthBaseValue.mGBV_ModeValueForCoin[5];
				break;
			case GrowthGameModeTag.tHunting:
				num2 = GrowthBaseValue.mGBV_ModeValueForCoin[6];
				break;
			}
			switch (gameRating)
			{
			case GrowthGameRatingTag.RatingTag_Nil:
				num = GrowthBaseValue.mGBV_RatingValueForCoin[0];
				break;
			case GrowthGameRatingTag.RatingTag_F:
				num = GrowthBaseValue.mGBV_RatingValueForCoin[1];
				break;
			case GrowthGameRatingTag.RatingTag_E:
				num = GrowthBaseValue.mGBV_RatingValueForCoin[2];
				break;
			case GrowthGameRatingTag.RatingTag_D:
				num = GrowthBaseValue.mGBV_RatingValueForCoin[3];
				break;
			case GrowthGameRatingTag.RatingTag_C:
				num = GrowthBaseValue.mGBV_RatingValueForCoin[4];
				break;
			case GrowthGameRatingTag.RatingTag_B:
				num = GrowthBaseValue.mGBV_RatingValueForCoin[5];
				break;
			case GrowthGameRatingTag.RatingTag_A:
				num = GrowthBaseValue.mGBV_RatingValueForCoin[6];
				break;
			case GrowthGameRatingTag.RatingTag_S:
				num = GrowthBaseValue.mGBV_RatingValueForCoin[7];
				break;
			}
			return (int)((float)(num * num2) * 1f);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0005B488 File Offset: 0x00059888
		public HuntingRewardInfo GetHuntingModeItemReward(GameValueForRatingCalc gameValue, GrowthGameRatingTag gameRating)
		{
			HuntingRewardInfo huntingRewardInfo = new HuntingRewardInfo(gameValue.mHTModeValue.SenceID, gameValue.mHTModeValue.Difficulty, gameValue.mHTModeValue.RoundID, gameValue.mHTModeValue.MaxRoundNum, gameRating);
			huntingRewardInfo.GenGeneralReward();
			huntingRewardInfo.GenLotteryReward();
			return huntingRewardInfo;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0005B4D5 File Offset: 0x000598D5
		public int GetCoins()
		{
			return UserDataController.GetCoins();
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0005B4DC File Offset: 0x000598DC
		public void AddCoins(int coinsAdd)
		{
			if (UserDataController.GetCoins() + coinsAdd >= 99999999)
			{
				UserDataController.SetCoins(99999999);
			}
			else
			{
				UserDataController.AddCoins(coinsAdd);
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0005B504 File Offset: 0x00059904
		public bool SubCoins(int coinsSub)
		{
			if (UserDataController.GetCoins() < coinsSub)
			{
				return false;
			}
			UserDataController.SubCoins(coinsSub);
			return true;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0005B51A File Offset: 0x0005991A
		public int GetHonorPoint()
		{
			return UserDataController.GetHonorPoint();
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0005B521 File Offset: 0x00059921
		public void AddHonorPoint(int addNum)
		{
			UserDataController.AddHonorPoint(addNum);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0005B529 File Offset: 0x00059929
		public bool SubHonorPoint(int subNum)
		{
			if (UserDataController.GetHonorPoint() < subNum)
			{
				return false;
			}
			UserDataController.SubHonorPoint(subNum);
			return true;
		}
	}
}
