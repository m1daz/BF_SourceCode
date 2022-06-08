using System;
using System.Collections.Generic;
using GrowthSystem;
using UnityEngine;

// Token: 0x0200019E RID: 414
public class GrowthManagerKit : MonoBehaviour
{
	// Token: 0x06000B00 RID: 2816 RVA: 0x00051931 File Offset: 0x0004FD31
	public static void GenGrowthPromptEvent(GrowthPrometType type, int num, string description)
	{
		GrowthManger.mInstance.GenGrowthPromptEvent(type, num, description);
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x00051940 File Offset: 0x0004FD40
	public static void GenScenePropsInvalidEvent(SceneEnchantmentProps type)
	{
		GrowthManger.mInstance.GenScenePropsInvalidEvent(type);
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x0005194D File Offset: 0x0004FD4D
	public static bool HasVersionTips(string version)
	{
		return UserDataController.HasVersionTips(version);
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x00051955 File Offset: 0x0004FD55
	public static void VerifyVersionTips(string version)
	{
		UserDataController.VerifyVersionTips(version);
		GrowthManger.mInstance.SetDataDisplayRefreshFlag(true);
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x00051968 File Offset: 0x0004FD68
	public static int GetCharacterLevel()
	{
		return GrowthManger.mInstance.GetCharacterLevel();
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x00051974 File Offset: 0x0004FD74
	public static int GetCharacterExpTotal()
	{
		return GrowthManger.mInstance.GetCharacterExpTotal();
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x00051980 File Offset: 0x0004FD80
	public static int GetCharacterCurLevelUpExpNeed(int curLevel)
	{
		return GrowthManger.mInstance.GetCharacterCurLevelUpExpNeed(curLevel);
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x0005198D File Offset: 0x0004FD8D
	public static int GetCharacterCurLevelExpExist(int curLevel)
	{
		return GrowthManger.mInstance.GetCharacterCurLevelExpExist(curLevel);
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x0005199A File Offset: 0x0004FD9A
	public static void AddCharacterExp(int expAdd)
	{
		GrowthManger.mInstance.AddCharacterExp(expAdd);
		GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.ExpAdd, expAdd, string.Empty);
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x000519B3 File Offset: 0x0004FDB3
	public static int GetHonorPoint()
	{
		return GrowthManger.mInstance.GetHonorPoint();
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x000519BF File Offset: 0x0004FDBF
	public static bool SubHonorPoint(int subNum)
	{
		return GrowthManger.mInstance.SubHonorPoint(subNum);
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x000519CC File Offset: 0x0004FDCC
	public static void AddHonorPoint(int addNum)
	{
		GrowthManger.mInstance.AddHonorPoint(addNum);
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x000519D9 File Offset: 0x0004FDD9
	public static int GetCoins()
	{
		return GrowthManger.mInstance.GetCoins();
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x000519E5 File Offset: 0x0004FDE5
	public static bool SubCoins(int coinsSub)
	{
		return GrowthManger.mInstance.SubCoins(coinsSub);
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x000519F2 File Offset: 0x0004FDF2
	public static void AddCoins(int coinsAdd)
	{
		GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.CoinsAdd, coinsAdd, string.Empty);
		GrowthManger.mInstance.AddCoins(coinsAdd);
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00051A0B File Offset: 0x0004FE0B
	public static int GetHuntingTickets()
	{
		return UserDataController.GetHuntingTickets();
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x00051A12 File Offset: 0x0004FE12
	public static void AddHuntingTickets(int addNum)
	{
		UserDataController.AddHuntingTickets(addNum);
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x00051A1A File Offset: 0x0004FE1A
	public static void SubHuntingTickets(int subNum)
	{
		UserDataController.SubHuntingTickets(subNum);
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x00051A22 File Offset: 0x0004FE22
	public static void SetHuntingTickets(int newNum)
	{
		UserDataController.SetHuntingTickets(newNum);
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x00051A2A File Offset: 0x0004FE2A
	public static int GetUnlockPaletteIndex()
	{
		return UserDataController.GetUnlockPaletteIndex();
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x00051A31 File Offset: 0x0004FE31
	public static void SetUnlockPaletteIndex(int paletteIndex)
	{
		UserDataController.SetUnlockPaletteIndex(paletteIndex);
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x00051A39 File Offset: 0x0004FE39
	public static int GetPaletteUnlockPrice(int paletteIndex)
	{
		return UserDataController.GetLockedPalettePrice(paletteIndex);
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00051A41 File Offset: 0x0004FE41
	public static int GetGems()
	{
		return GrowthManger.mInstance.GetGems();
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00051A4D File Offset: 0x0004FE4D
	public static bool SubGems(int gemsSub)
	{
		return GrowthManger.mInstance.SubGems(gemsSub);
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x00051A5A File Offset: 0x0004FE5A
	public static void AddGems(int gemsAdd)
	{
		GrowthManger.mInstance.AddGems(gemsAdd);
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x00051A67 File Offset: 0x0004FE67
	public static int GetLevelUpCoinsReward(int curLevel)
	{
		return GrowthManger.mInstance.GetLevelUpCoinsReward(curLevel);
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x00051A74 File Offset: 0x0004FE74
	public int GetSNSShareCoinsReward()
	{
		return GrowthManger.mInstance.GetSNSShareCoinsReward();
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x00051A80 File Offset: 0x0004FE80
	public static int GetAppstoreRatingCoinsReward()
	{
		return GrowthManger.mInstance.GetAppstoreRatingCoinsReward();
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x00051A8C File Offset: 0x0004FE8C
	public static GrowthGameRatingTag GetOncePlayRating(GameValueForRatingCalc gameValue)
	{
		return GrowthManger.mInstance.GetOncePlayRating(gameValue);
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x00051A99 File Offset: 0x0004FE99
	public static int GetOncePlayExpReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating)
	{
		return GrowthManger.mInstance.GetOncePlayExpReward(gameMode, gameRating);
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x00051AA7 File Offset: 0x0004FEA7
	public static int GetOncePlayHonorPointReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating)
	{
		return GrowthManger.mInstance.GetOncePlayHonorPointReward(gameMode, gameRating);
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x00051AB5 File Offset: 0x0004FEB5
	public static int GetOncePlaySeasonScoreReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating, bool isWinner)
	{
		return GrowthManger.mInstance.GetOncePlaySeasonScoreReward(gameMode, gameRating, isWinner);
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x00051AC4 File Offset: 0x0004FEC4
	public static int GetOncePlayCoinsReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating)
	{
		return GrowthManger.mInstance.GetOncePlayCoinsReward(gameMode, gameRating);
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00051AD2 File Offset: 0x0004FED2
	public static HuntingRewardInfo GetHuntingModeItemReward(GameValueForRatingCalc gameValue, GrowthGameRatingTag gameRating)
	{
		return GrowthManger.mInstance.GetHuntingModeItemReward(gameValue, gameRating);
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00051AE0 File Offset: 0x0004FEE0
	public static void ReceiveReward(FightingStatisticsTag tag)
	{
		GrowthManger.mInstance.ReceiveReward(tag);
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x00051AED File Offset: 0x0004FEED
	public static RewardUnitInfo GetRewardUnitInfo(FightingStatisticsTag tag)
	{
		return GrowthManger.mInstance.GetRewardUnitInfo(tag);
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x00051AFC File Offset: 0x0004FEFC
	public static List<FightingStatisticsTag> GetAllRewardTypeList()
	{
		return new List<FightingStatisticsTag>
		{
			FightingStatisticsTag.tLevelUp,
			FightingStatisticsTag.tTotalKillInWorldwideMultiplayer,
			FightingStatisticsTag.tTotalHeadshotKill,
			FightingStatisticsTag.tTotalGoldLikeKill,
			FightingStatisticsTag.tTotalStrongholdModeVictory,
			FightingStatisticsTag.tTotalKillingCompetitionModeVictory,
			FightingStatisticsTag.tTotalExplosionModeVictory,
			FightingStatisticsTag.tLvGrowthGift,
			FightingStatisticsTag.tDailyLoginInSevenDays,
			FightingStatisticsTag.tDailyJoinInKillingCompetitionMode,
			FightingStatisticsTag.tDailyJoinInStrongholdMode,
			FightingStatisticsTag.tDailyJoinInExplosionMode,
			FightingStatisticsTag.tDailyKillInDeathMatchMode,
			FightingStatisticsTag.tDailyJoinInMutationMode,
			FightingStatisticsTag.tDailyVideoShare
		};
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x00051B88 File Offset: 0x0004FF88
	public static List<FightingStatisticsTag> GetDailyRewardTypeList()
	{
		return new List<FightingStatisticsTag>
		{
			FightingStatisticsTag.tDailyLoginInSevenDays,
			FightingStatisticsTag.tDailyJoinInKillingCompetitionMode,
			FightingStatisticsTag.tDailyJoinInStrongholdMode,
			FightingStatisticsTag.tDailyJoinInExplosionMode,
			FightingStatisticsTag.tDailyKillInDeathMatchMode,
			FightingStatisticsTag.tDailyJoinInMutationMode,
			FightingStatisticsTag.tDailyVideoShare
		};
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x00051BD4 File Offset: 0x0004FFD4
	public static List<FightingStatisticsTag> GetGeneralRewardTypeList()
	{
		return new List<FightingStatisticsTag>
		{
			FightingStatisticsTag.tLvGrowthGift,
			FightingStatisticsTag.tLevelUp,
			FightingStatisticsTag.tTotalKillInWorldwideMultiplayer,
			FightingStatisticsTag.tTotalHeadshotKill,
			FightingStatisticsTag.tTotalGoldLikeKill,
			FightingStatisticsTag.tTotalStrongholdModeVictory,
			FightingStatisticsTag.tTotalKillingCompetitionModeVictory,
			FightingStatisticsTag.tTotalExplosionModeVictory
		};
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x00051C28 File Offset: 0x00050028
	public static bool IsNewRewardEnabled()
	{
		bool result = false;
		List<FightingStatisticsTag> allRewardTypeList = GrowthManagerKit.GetAllRewardTypeList();
		foreach (FightingStatisticsTag tag in allRewardTypeList)
		{
			RewardUnitInfo rewardUnitInfo = GrowthManagerKit.GetRewardUnitInfo(tag);
			if (rewardUnitInfo.canGotReward)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x00051C9C File Offset: 0x0005009C
	public static bool NeedRefreshDataDisplay()
	{
		return GrowthManger.mInstance.NeedRefreshDataDisplay();
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x00051CA8 File Offset: 0x000500A8
	public static void SetDataDisplayRefreshFlag(bool b)
	{
		GrowthManger.mInstance.SetDataDisplayRefreshFlag(b);
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x00051CB5 File Offset: 0x000500B5
	public static GItemId GetItemId(string name)
	{
		return GrowthManger.mInstance.GetItemId(name);
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x00051CC2 File Offset: 0x000500C2
	public static string[] GetAllWeaponNameList()
	{
		return UserDataController.AllWeaponNameList;
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x00051CCC File Offset: 0x000500CC
	public static GWeaponItemInfo GetWeaponItemInfoByName(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllWeaponNameList.Length; i++)
		{
			if (UserDataController.AllWeaponNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			return GrowthManger.mInstance.GetWeaponItemInfoByName(name);
		}
		return null;
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x00051D20 File Offset: 0x00050120
	public static GWeaponItemInfo GetUpgradeWeaponItemInfoByName(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllWeaponNameList.Length; i++)
		{
			if (UserDataController.AllWeaponNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			return GrowthManger.mInstance.GetUpgradeWeaponItemInfoByName(name);
		}
		return null;
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x00051D74 File Offset: 0x00050174
	public static GWeaponItemInfo[] GetComparedWeaponItemInfoListByName(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllWeaponNameList.Length; i++)
		{
			if (UserDataController.AllWeaponNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			return GrowthManger.mInstance.GetComparedWeaponItemInfoListByName(name);
		}
		return null;
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x00051DC7 File Offset: 0x000501C7
	public static bool UpgradeWeaponByName(string name)
	{
		return GrowthManger.mInstance.UpgradeWeaponByName(name);
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00051DD4 File Offset: 0x000501D4
	public static bool EnableWeaponByName(string name)
	{
		return GrowthManger.mInstance.UpgradeWeaponByName(name);
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x00051DE1 File Offset: 0x000501E1
	public static void SetSkinSharedMark(string name, bool mark)
	{
		UserDataController.SetSkinSharedMark(name, mark);
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x00051DEA File Offset: 0x000501EA
	public static bool HasSkinShared(string name)
	{
		return UserDataController.HasSkinShared(name);
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x00051DF2 File Offset: 0x000501F2
	public static string[] GetAllSkinNameList()
	{
		return UserDataController.AllSkinNameList;
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x00051DFC File Offset: 0x000501FC
	public static GSkinItemInfo GetSkinItemInfoByName(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllSkinNameList.Length; i++)
		{
			if (UserDataController.AllSkinNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			return GrowthManger.mInstance.GetSkinItemInfo(name);
		}
		return GrowthManger.mInstance.GetCustomSkinItemInfo(name);
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00051E59 File Offset: 0x00050259
	public static bool EnableSkinByName(string name)
	{
		return GrowthManger.mInstance.EnableSkin(name);
	}

	// Token: 0x06000B36 RID: 2870 RVA: 0x00051E66 File Offset: 0x00050266
	public static void DisableSkinByName(string name)
	{
		GrowthManger.mInstance.DisableSkin(name);
	}

	// Token: 0x06000B37 RID: 2871 RVA: 0x00051E73 File Offset: 0x00050273
	public static GSkinItemInfo GetCurSettedSkinInfo()
	{
		return GrowthManagerKit.GetSkinItemInfoByName(GrowthManger.mInstance.GetCurSettedSkinName());
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x00051E84 File Offset: 0x00050284
	public static bool SetCurSettedSkin(string name)
	{
		GrowthManger.mInstance.SetCurSettedSkin(name);
		bool flag = true;
		for (int i = 0; i < UserDataController.AllSkinNameList.Length; i++)
		{
			if (UserDataController.AllSkinNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			GrowthManger.mInstance.SetCurSettedSkin(name);
			return true;
		}
		GrowthManger.mInstance.SetCurSettedSkin(name);
		return true;
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x00051EEE File Offset: 0x000502EE
	public static string[] GetUserAllEnabledSkinName()
	{
		return GrowthManger.mInstance.GetUserAllEnabledSkinName();
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x00051EFC File Offset: 0x000502FC
	public static GSkinItemInfo[] GetUserAllEnabledSkinItemInfo()
	{
		string[] userAllEnabledSkinName = GrowthManagerKit.GetUserAllEnabledSkinName();
		if (userAllEnabledSkinName.Length <= 0)
		{
			return null;
		}
		GSkinItemInfo[] array = new GSkinItemInfo[userAllEnabledSkinName.Length];
		for (int i = 0; i < userAllEnabledSkinName.Length; i++)
		{
			array[i] = GrowthManagerKit.GetSkinItemInfoByName(userAllEnabledSkinName[i]);
		}
		return array;
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00051F43 File Offset: 0x00050343
	public static string[] GetAllHatNameList()
	{
		return UserDataController.AllHatNameList;
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x00051F4C File Offset: 0x0005034C
	public static GHatItemInfo GetHatItemInfoByName(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllHatNameList.Length; i++)
		{
			if (UserDataController.AllHatNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			return GrowthManger.mInstance.GetHatItemInfo(name);
		}
		return GrowthManger.mInstance.GetCustomHatItemInfo(name);
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00051FA9 File Offset: 0x000503A9
	public static bool EnableHatByName(string name)
	{
		return GrowthManger.mInstance.EnableHat(name);
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x00051FB6 File Offset: 0x000503B6
	public static GHatItemInfo GetCurSettedHatInfo()
	{
		return GrowthManagerKit.GetHatItemInfoByName(GrowthManger.mInstance.GetCurSettedHatName());
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x00051FC8 File Offset: 0x000503C8
	public static bool SetCurSettedHat(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllHatNameList.Length; i++)
		{
			if (UserDataController.AllHatNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			GrowthManger.mInstance.SetCurSettedHat(name);
			return true;
		}
		GrowthManger.mInstance.SetCurSettedHat(name);
		return true;
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00052027 File Offset: 0x00050427
	public static string[] GetUserAllEnabledHatName()
	{
		return GrowthManger.mInstance.GetUserAllEnabledHatName();
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x00052034 File Offset: 0x00050434
	public static GHatItemInfo[] GetUserAllEnabledHatItemInfo()
	{
		string[] userAllEnabledHatName = GrowthManagerKit.GetUserAllEnabledHatName();
		if (userAllEnabledHatName.Length <= 0)
		{
			return null;
		}
		GHatItemInfo[] array = new GHatItemInfo[userAllEnabledHatName.Length];
		for (int i = 0; i < userAllEnabledHatName.Length; i++)
		{
			array[i] = GrowthManagerKit.GetHatItemInfoByName(userAllEnabledHatName[i]);
		}
		return array;
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x0005207B File Offset: 0x0005047B
	public static string[] GetAllCapeNameList()
	{
		return UserDataController.AllCapeNameList;
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x00052084 File Offset: 0x00050484
	public static GCapeItemInfo GetCapeItemInfoByName(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllCapeNameList.Length; i++)
		{
			if (UserDataController.AllCapeNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			return GrowthManger.mInstance.GetCapeItemInfo(name);
		}
		return GrowthManger.mInstance.GetCustomCapeItemInfo(name);
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x000520E1 File Offset: 0x000504E1
	public static bool EnableCapeByName(string name)
	{
		return GrowthManger.mInstance.EnableCape(name);
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x000520EE File Offset: 0x000504EE
	public static GCapeItemInfo GetCurSettedCapeInfo()
	{
		return GrowthManagerKit.GetCapeItemInfoByName(GrowthManger.mInstance.GetCurSettedCapeName());
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x00052100 File Offset: 0x00050500
	public static bool SetCurSettedCape(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllCapeNameList.Length; i++)
		{
			if (UserDataController.AllCapeNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			GrowthManger.mInstance.SetCurSettedCape(name);
			return true;
		}
		GrowthManger.mInstance.SetCurSettedCape(name);
		return true;
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x0005215F File Offset: 0x0005055F
	public static string[] GetUserAllEnabledCapeName()
	{
		return GrowthManger.mInstance.GetUserAllEnabledCapeName();
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x0005216C File Offset: 0x0005056C
	public static GCapeItemInfo[] GetUserAllEnabledCapeItemInfo()
	{
		string[] userAllEnabledCapeName = GrowthManagerKit.GetUserAllEnabledCapeName();
		if (userAllEnabledCapeName.Length <= 0)
		{
			return null;
		}
		GCapeItemInfo[] array = new GCapeItemInfo[userAllEnabledCapeName.Length];
		for (int i = 0; i < userAllEnabledCapeName.Length; i++)
		{
			array[i] = GrowthManagerKit.GetCapeItemInfoByName(userAllEnabledCapeName[i]);
		}
		return array;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x000521B3 File Offset: 0x000505B3
	public static string[] GetAllBootNameList()
	{
		return UserDataController.AllBootNameList;
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x000521BC File Offset: 0x000505BC
	public static GBootItemInfo GetBootItemInfoByName(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllBootNameList.Length; i++)
		{
			if (UserDataController.AllBootNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			return GrowthManger.mInstance.GetBootItemInfo(name);
		}
		return GrowthManger.mInstance.GetCustomBootItemInfo(name);
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x00052219 File Offset: 0x00050619
	public static bool EnableBootByName(string name)
	{
		return GrowthManger.mInstance.EnableBoot(name);
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x00052226 File Offset: 0x00050626
	public static GBootItemInfo GetCurSettedBootInfo()
	{
		return GrowthManagerKit.GetBootItemInfoByName(GrowthManger.mInstance.GetCurSettedBootName());
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00052238 File Offset: 0x00050638
	public static bool SetCurSettedBoot(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllBootNameList.Length; i++)
		{
			if (UserDataController.AllBootNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			GrowthManger.mInstance.SetCurSettedBoot(name);
			return true;
		}
		GrowthManger.mInstance.SetCurSettedBoot(name);
		return true;
	}

	// Token: 0x06000B4E RID: 2894 RVA: 0x00052297 File Offset: 0x00050697
	public static string[] GetUserAllEnabledBootName()
	{
		return GrowthManger.mInstance.GetUserAllEnabledBootName();
	}

	// Token: 0x06000B4F RID: 2895 RVA: 0x000522A4 File Offset: 0x000506A4
	public static GBootItemInfo[] GetUserAllEnabledBootItemInfo()
	{
		string[] userAllEnabledBootName = GrowthManagerKit.GetUserAllEnabledBootName();
		if (userAllEnabledBootName.Length <= 0)
		{
			return null;
		}
		GBootItemInfo[] array = new GBootItemInfo[userAllEnabledBootName.Length];
		for (int i = 0; i < userAllEnabledBootName.Length; i++)
		{
			array[i] = GrowthManagerKit.GetBootItemInfoByName(userAllEnabledBootName[i]);
		}
		return array;
	}

	// Token: 0x06000B50 RID: 2896 RVA: 0x000522EB File Offset: 0x000506EB
	public static string[] GetAllUpEnabledWeaponPropertyNameList()
	{
		return UserDataController.AllUpEnabledWeaponPropertyList;
	}

	// Token: 0x06000B51 RID: 2897 RVA: 0x000522F4 File Offset: 0x000506F4
	public static GWeaponPropertyCardItemInfo[] GetAllWeaponPropertyCardItemInfo()
	{
		string[] allUpEnabledWeaponPropertyNameList = GrowthManagerKit.GetAllUpEnabledWeaponPropertyNameList();
		GWeaponPropertyCardItemInfo[] array = new GWeaponPropertyCardItemInfo[allUpEnabledWeaponPropertyNameList.Length * 3];
		int num = 0;
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < allUpEnabledWeaponPropertyNameList.Length; j++)
			{
				array[j + i * allUpEnabledWeaponPropertyNameList.Length] = new GWeaponPropertyCardItemInfo(allUpEnabledWeaponPropertyNameList[j], i + 1);
				num++;
			}
		}
		return array;
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x0005235C File Offset: 0x0005075C
	public static void SetMyNickName(string name)
	{
		string myNickName = name;
		if (WordFilter.mInstance != null)
		{
			myNickName = WordFilter.mInstance.FilterString(name);
		}
		UserDataController.SetMyNickName(myNickName);
	}

	// Token: 0x06000B53 RID: 2899 RVA: 0x0005238D File Offset: 0x0005078D
	public static string GetMyNickName()
	{
		return UserDataController.GetMyNickName();
	}

	// Token: 0x06000B54 RID: 2900 RVA: 0x00052394 File Offset: 0x00050794
	public static void SetMyRoomName(string name)
	{
		string myRoomName = name;
		if (WordFilter.mInstance != null)
		{
			myRoomName = WordFilter.mInstance.FilterString(name);
		}
		UserDataController.SetMyRoomName(myRoomName);
	}

	// Token: 0x06000B55 RID: 2901 RVA: 0x000523C5 File Offset: 0x000507C5
	public static string GetMyRoomName()
	{
		return UserDataController.GetMyRoomName();
	}

	// Token: 0x06000B56 RID: 2902 RVA: 0x000523CC File Offset: 0x000507CC
	public static Dictionary<int, string> GetWeaponNameDic()
	{
		return GrowthManger.mInstance.GetWeaponNameDic();
	}

	// Token: 0x06000B57 RID: 2903 RVA: 0x000523D8 File Offset: 0x000507D8
	public static string[] GetCurEquippedWeaponNameList()
	{
		return GrowthManger.mInstance.GetCurEquippedWeaponNameList();
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x000523E4 File Offset: 0x000507E4
	public static string[] GetCurEquippedWeaponNameListForStore()
	{
		return GrowthManger.mInstance.GetCurEquippedWeaponNameListForStore();
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x000523F0 File Offset: 0x000507F0
	public static GWeaponItemInfo[] GetCurEquippedWeaponItemInfoListForStore()
	{
		string[] curEquippedWeaponNameListForStore = GrowthManagerKit.GetCurEquippedWeaponNameListForStore();
		if (curEquippedWeaponNameListForStore.Length <= 0)
		{
			return null;
		}
		GWeaponItemInfo[] array = new GWeaponItemInfo[curEquippedWeaponNameListForStore.Length];
		for (int i = 0; i < curEquippedWeaponNameListForStore.Length; i++)
		{
			if (curEquippedWeaponNameListForStore[i] == string.Empty)
			{
				array[i] = null;
			}
			else
			{
				array[i] = GrowthManagerKit.GetWeaponItemInfoByName(curEquippedWeaponNameListForStore[i]);
			}
		}
		return array;
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x00052454 File Offset: 0x00050854
	public static GWeaponItemInfo[] GetCurEquippedWeaponItemInfoList()
	{
		string[] curEquippedWeaponNameList = GrowthManagerKit.GetCurEquippedWeaponNameList();
		if (curEquippedWeaponNameList.Length <= 0)
		{
			return null;
		}
		GWeaponItemInfo[] array = new GWeaponItemInfo[curEquippedWeaponNameList.Length];
		for (int i = 0; i < curEquippedWeaponNameList.Length; i++)
		{
			array[i] = GrowthManagerKit.GetWeaponItemInfoByName(curEquippedWeaponNameList[i]);
		}
		return array;
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x0005249B File Offset: 0x0005089B
	public static int GetCurWeaponEquipLimitedNum()
	{
		return GrowthManger.mInstance.GetCurWeaponEquipLimitedNum();
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x000524A7 File Offset: 0x000508A7
	public static void SetCurWeaponEquipLimitedNum(int num)
	{
		GrowthManger.mInstance.SetCurWeaponEquipLimitedNum(num);
	}

	// Token: 0x06000B5D RID: 2909 RVA: 0x000524B4 File Offset: 0x000508B4
	public static int GetMaxWeaponEquipNum()
	{
		return 8;
	}

	// Token: 0x06000B5E RID: 2910 RVA: 0x000524B8 File Offset: 0x000508B8
	public static GWeaponItemInfo[] GetUserAllEnabledWeaponItemInfo()
	{
		string[] allWeaponNameList = GrowthManagerKit.GetAllWeaponNameList();
		List<GWeaponItemInfo> list = new List<GWeaponItemInfo>();
		for (int i = 0; i < allWeaponNameList.Length; i++)
		{
			GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(allWeaponNameList[i]);
			if (weaponItemInfoByName.mIsEnabled)
			{
				list.Add(weaponItemInfoByName);
			}
		}
		GWeaponItemInfo[] array = new GWeaponItemInfo[list.Count];
		list.CopyTo(array);
		list.Clear();
		return array;
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x00052520 File Offset: 0x00050920
	public static int GetWeaponEquipLimitUpgradePrice()
	{
		int result;
		switch (GrowthManagerKit.GetCurWeaponEquipLimitedNum())
		{
		case 4:
			result = 100;
			break;
		case 5:
			result = 100;
			break;
		case 6:
			result = 100;
			break;
		case 7:
			result = 100;
			break;
		default:
			result = 0;
			break;
		}
		return result;
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x0005257C File Offset: 0x0005097C
	public static bool UpgradeWeaponEquipLimitedNum()
	{
		int curWeaponEquipLimitedNum = GrowthManagerKit.GetCurWeaponEquipLimitedNum();
		bool result = false;
		if (curWeaponEquipLimitedNum < GrowthManagerKit.GetMaxWeaponEquipNum())
		{
			int weaponEquipLimitUpgradePrice = GrowthManagerKit.GetWeaponEquipLimitUpgradePrice();
			if (GrowthManagerKit.SubGems(weaponEquipLimitUpgradePrice))
			{
				result = true;
				GrowthManagerKit.SetCurWeaponEquipLimitedNum(curWeaponEquipLimitedNum + 1);
			}
		}
		return result;
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x000525B8 File Offset: 0x000509B8
	public static void ProcessOneWeaponEquipTap(string tapedWeaponName)
	{
		GrowthManger.mInstance.ProcessOneWeaponEquipTap(tapedWeaponName);
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x000525C5 File Offset: 0x000509C5
	public static int GetCurWeaponEquipLimitedUnlockLevel()
	{
		return GrowthManger.mInstance.GetCurWeaponEquipLimitedUnlockLevel();
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x000525D4 File Offset: 0x000509D4
	public static GArmorItemInfo GetArmorItemInfoByName(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllArmorNameList.Length; i++)
		{
			if (UserDataController.AllArmorNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			return GrowthManger.mInstance.GetArmorItemInfoByName(name);
		}
		return null;
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x00052627 File Offset: 0x00050A27
	public static GArmorItemInfo GetCurSettedArmorInfo()
	{
		return GrowthManger.mInstance.GetCurSettedArmorInfo();
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x00052634 File Offset: 0x00050A34
	public static void SetCurSettedArmor(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllArmorNameList.Length; i++)
		{
			if (UserDataController.AllArmorNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			GrowthManger.mInstance.SetCurSettedArmor(name);
		}
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x0005268A File Offset: 0x00050A8A
	public static void SetFightingStatisticsValue(FightingStatisticsTag tag, int newValue)
	{
		GrowthManger.mInstance.SetFightingStatisticsValue(tag, newValue);
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x00052698 File Offset: 0x00050A98
	public static int GetFightingStatisticsValue(FightingStatisticsTag tag)
	{
		return GrowthManger.mInstance.GetFightingStatisticsValue(tag);
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x000526A5 File Offset: 0x00050AA5
	public static void ResetDailyRewardRecord(FightingStatisticsTag tag)
	{
		GrowthManger.mInstance.ResetDailyRewardRecord(tag);
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x000526B2 File Offset: 0x00050AB2
	public static float GetCurAutoGiftTimeRest()
	{
		return UserDataController.GetCurAutoGiftTimeRest();
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x000526B9 File Offset: 0x00050AB9
	public static bool CanGetAutoGift()
	{
		return UserDataController.CanGetAutoGift();
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x000526C0 File Offset: 0x00050AC0
	public static AutoGiftInfo RecevieOneGift()
	{
		return UserDataController.RecevieOneGift();
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x000526C7 File Offset: 0x00050AC7
	public static float GetAutoGiftProgressFillAmount()
	{
		return UserDataController.GetCurAutoGiftTimeRest() / UserDataController.GetCurAutoGiftTimeInterval();
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x000526D4 File Offset: 0x00050AD4
	public static int GetCurGiftBoxTotal()
	{
		return UserDataController.GetCurGiftBoxTotal();
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x000526DB File Offset: 0x00050ADB
	public static void AddGiftBox(int addNum)
	{
		GrowthManger.mInstance.SetDataDisplayRefreshFlag(true);
		GrowthManger.mInstance.GetComponent<AudioSource>().PlayOneShot(GrowthManger.mInstance.giftBoxIn);
		UserDataController.SetCurGiftBoxTotal(UserDataController.GetCurGiftBoxTotal() + addNum);
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00052710 File Offset: 0x00050B10
	public static bool SubOneGiftBox()
	{
		GrowthManger.mInstance.SetDataDisplayRefreshFlag(true);
		int curGiftBoxTotal = UserDataController.GetCurGiftBoxTotal();
		if (curGiftBoxTotal < 1)
		{
			return false;
		}
		UserDataController.SetCurGiftBoxTotal(curGiftBoxTotal - 1);
		return true;
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00052740 File Offset: 0x00050B40
	public static void VerifyForGiftPackPurchase()
	{
		UserDataController.VerifyTheGiftPackPurchase();
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x00052747 File Offset: 0x00050B47
	public static bool HasBuyTheGiftPack()
	{
		return UserDataController.HasBuyTheGiftPack();
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x00052750 File Offset: 0x00050B50
	public static int GetGiftBoxSlotsLuckyValue()
	{
		if (UserDataController.GetGiftBoxSlotsPlayCount() <= 2)
		{
			return 30;
		}
		if (UserDataController.GetGiftBoxSlotsPlayCount() <= 4)
		{
			return 20;
		}
		if (UserDataController.GetGiftBoxSlotsPlayCount() <= 6)
		{
			return 20;
		}
		if (UserDataController.GetGiftBoxSlotsPlayCount() <= 8)
		{
			return 10;
		}
		if (UserDataController.GetGiftBoxSlotsPlayCount() % 5 == 0)
		{
			return 10;
		}
		return 0;
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x000527A8 File Offset: 0x00050BA8
	public static GiftBoxSlotsTableInfo GetGiftBoxSlotsResultInfo(int tableSize)
	{
		string[] allWeaponNameList = UserDataController.AllWeaponNameList;
		string[] array = new string[allWeaponNameList.Length - 20];
		int num = 0;
		for (int i = 0; i < allWeaponNameList.Length; i++)
		{
			if (!(allWeaponNameList[i] == "CandyRifle") && !(allWeaponNameList[i] == "ChristmasSniper") && !(allWeaponNameList[i] == "GingerbreadKnife") && !(allWeaponNameList[i] == "ZombieHand") && !(allWeaponNameList[i] == "SAK47") && !(allWeaponNameList[i] == "SDesertEagle") && !(allWeaponNameList[i] == "SantaGun2014") && !(allWeaponNameList[i] == "CandyHammer") && !(allWeaponNameList[i] == "SM134") && !(allWeaponNameList[i] == "SM4") && !(allWeaponNameList[i] == "SG36K") && !(allWeaponNameList[i] == "SAUG") && !(allWeaponNameList[i] == "HonorAK47") && !(allWeaponNameList[i] == "HonorM4") && !(allWeaponNameList[i] == "HonorKnife") && !(allWeaponNameList[i] == "HonorAWP") && !(allWeaponNameList[i] == "HonorM134") && !(allWeaponNameList[i] == "FreddyGun") && !(allWeaponNameList[i] == "ImpulseGun") && !(allWeaponNameList[i] == "Assault"))
			{
				array[num] = allWeaponNameList[i];
				num++;
			}
		}
		string[] array2 = array;
		string[] allSkinNameList = UserDataController.AllSkinNameList;
		string[] allCapeNameList = UserDataController.AllCapeNameList;
		string[] allHatNameList = UserDataController.AllHatNameList;
		string[] allBootNameList = UserDataController.AllBootNameList;
		string[] allMultiplayerBuffNameList = GrowthManagerKit.GetAllMultiplayerBuffNameList();
		string[] allArmorNameList = UserDataController.AllArmorNameList;
		string[] allUpEnabledWeaponPropertyList = UserDataController.AllUpEnabledWeaponPropertyList;
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		List<string> list4 = new List<string>();
		List<string> list5 = new List<string>();
		List<string> list6 = new List<string>();
		List<string> list7 = new List<string>();
		List<string> list8 = new List<string>();
		int giftBoxSlotsLuckyValue = GrowthManagerKit.GetGiftBoxSlotsLuckyValue();
		UserDataController.AddGiftBoxSlotsPlayCount();
		for (int j = 0; j < array2.Length; j++)
		{
			if (!GrowthManagerKit.GetWeaponItemInfoByName(array2[j]).mIsNoLimitedUse)
			{
				list.Add(array2[j]);
			}
		}
		for (int j = 0; j < allSkinNameList.Length; j++)
		{
			if (!GrowthManagerKit.GetSkinItemInfoByName(allSkinNameList[j]).mIsEnabled)
			{
				list2.Add(allSkinNameList[j]);
			}
		}
		for (int j = 0; j < allCapeNameList.Length; j++)
		{
			if (!GrowthManagerKit.GetCapeItemInfoByName(allCapeNameList[j]).mIsEnabled)
			{
				if (!(allCapeNameList[j] == "Cape_7"))
				{
					list3.Add(allCapeNameList[j]);
				}
			}
		}
		for (int j = 0; j < allHatNameList.Length; j++)
		{
			if (!GrowthManagerKit.GetHatItemInfoByName(allHatNameList[j]).mIsEnabled)
			{
				if (!(allHatNameList[j] == "Hat_9") && !(allHatNameList[j] == "Hat_11"))
				{
					list4.Add(allHatNameList[j]);
				}
			}
		}
		for (int j = 0; j < allBootNameList.Length; j++)
		{
			if (!GrowthManagerKit.GetBootItemInfoByName(allBootNameList[j]).mIsEnabled)
			{
				if (!(allBootNameList[j] == "Boot_3"))
				{
					list5.Add(allBootNameList[j]);
				}
			}
		}
		for (int j = 0; j < allMultiplayerBuffNameList.Length; j++)
		{
			list6.Add(allMultiplayerBuffNameList[j]);
		}
		for (int j = 0; j < allArmorNameList.Length; j++)
		{
			list7.Add(allArmorNameList[j]);
		}
		for (int j = 0; j < allUpEnabledWeaponPropertyList.Length; j++)
		{
			list8.Add(allUpEnabledWeaponPropertyList[j]);
		}
		GiftBoxSlotsTableInfo giftBoxSlotsTableInfo = new GiftBoxSlotsTableInfo();
		giftBoxSlotsTableInfo.resultIndex = UnityEngine.Random.Range(0, tableSize);
		GiftBoxSlotsItemInfo giftBoxSlotsItemInfo = new GiftBoxSlotsItemInfo();
		int num2 = UnityEngine.Random.Range(1, 1001);
		if (num2 > 990 - giftBoxSlotsLuckyValue)
		{
			if (list4.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, list4.Count);
				giftBoxSlotsItemInfo.Num = 1;
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = list4[index] + "_SlotLogo";
				GHatItemInfo hatItemInfoByName = GrowthManagerKit.GetHatItemInfoByName(list4[index]);
				giftBoxSlotsItemInfo.shareName = "Hat - " + GrowthManagerKit.GetHatItemInfoByName(list4[index]).mNameDisplay;
				giftBoxSlotsItemInfo.isSpecialAward = true;
				GGCloudServiceKit.mInstance.UploadSlotTopPrize(giftBoxSlotsItemInfo.spriteName, string.Empty);
				hatItemInfoByName.Enable();
				list4.RemoveAt(index);
			}
			else
			{
				giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(10, 31) * 10;
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = "Coins_SlotLogo";
				if (UnityEngine.Random.Range(1, 101) <= 2)
				{
					giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(20, 51) * 10;
				}
				else if (UnityEngine.Random.Range(1, 201) <= 1)
				{
					giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(50, 100) * 10;
				}
				giftBoxSlotsItemInfo.shareName = "Coins x " + giftBoxSlotsItemInfo.Num.ToString();
				UserDataController.AddCoins(giftBoxSlotsItemInfo.Num);
				GrowthManger.mInstance.SetDataDisplayRefreshFlag(true);
			}
		}
		else if (num2 > 980 - giftBoxSlotsLuckyValue * 2 && num2 <= 990 - giftBoxSlotsLuckyValue)
		{
			if (list3.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, list3.Count);
				giftBoxSlotsItemInfo.Num = 1;
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = list3[index] + "_SlotLogo";
				GCapeItemInfo capeItemInfoByName = GrowthManagerKit.GetCapeItemInfoByName(list3[index]);
				giftBoxSlotsItemInfo.shareName = "Cape - " + GrowthManagerKit.GetCapeItemInfoByName(list3[index]).mNameDisplay;
				giftBoxSlotsItemInfo.isSpecialAward = true;
				GGCloudServiceKit.mInstance.UploadSlotTopPrize(giftBoxSlotsItemInfo.spriteName, string.Empty);
				capeItemInfoByName.Enable();
				list3.RemoveAt(index);
			}
			else
			{
				giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(10, 31) * 10;
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = "Coins_SlotLogo";
				if (UnityEngine.Random.Range(1, 101) <= 2)
				{
					giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(20, 51) * 10;
				}
				else if (UnityEngine.Random.Range(1, 201) <= 1)
				{
					giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(50, 100) * 10;
				}
				giftBoxSlotsItemInfo.shareName = "Coins x " + giftBoxSlotsItemInfo.Num.ToString();
				UserDataController.AddCoins(giftBoxSlotsItemInfo.Num);
				GrowthManger.mInstance.SetDataDisplayRefreshFlag(true);
			}
		}
		else if (num2 > 970 - giftBoxSlotsLuckyValue * 3 && num2 <= 980 - giftBoxSlotsLuckyValue * 2)
		{
			if (list5.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, list5.Count);
				giftBoxSlotsItemInfo.Num = 1;
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = list5[index] + "_SlotLogo";
				GBootItemInfo bootItemInfoByName = GrowthManagerKit.GetBootItemInfoByName(list5[index]);
				giftBoxSlotsItemInfo.shareName = "Boot - " + GrowthManagerKit.GetBootItemInfoByName(list5[index]).mNameDisplay;
				giftBoxSlotsItemInfo.isSpecialAward = true;
				GGCloudServiceKit.mInstance.UploadSlotTopPrize(giftBoxSlotsItemInfo.spriteName, string.Empty);
				bootItemInfoByName.Enable();
				list5.RemoveAt(index);
			}
			else
			{
				giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(10, 31) * 10;
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = "Coins_SlotLogo";
				if (UnityEngine.Random.Range(1, 101) <= 2)
				{
					giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(20, 51) * 10;
				}
				else if (UnityEngine.Random.Range(1, 201) <= 1)
				{
					giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(50, 100) * 10;
				}
				giftBoxSlotsItemInfo.shareName = "Coins x " + giftBoxSlotsItemInfo.Num.ToString();
				UserDataController.AddCoins(giftBoxSlotsItemInfo.Num);
				GrowthManger.mInstance.SetDataDisplayRefreshFlag(true);
			}
		}
		else if (num2 > 780 - giftBoxSlotsLuckyValue * 4 && num2 <= 970 - giftBoxSlotsLuckyValue * 3)
		{
			if (list.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, list.Count);
				giftBoxSlotsItemInfo.Num = 1;
				giftBoxSlotsItemInfo.hTimeNum = ((UnityEngine.Random.Range(1, 101) <= 10) ? 255 : ((UnityEngine.Random.Range(1, 11) <= 3) ? UnityEngine.Random.Range(1, 13) : UnityEngine.Random.Range(2, 5)));
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = list[index] + "_SlotLogo";
				GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(list[index]);
				if (weaponItemInfoByName.mIsOnlyUnlimitedBuy)
				{
					if (UnityEngine.Random.Range(1, 101) > 10)
					{
						giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(10, 31) * 10;
						giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
						giftBoxSlotsItemInfo.spriteName = "Coins_SlotLogo";
						if (UnityEngine.Random.Range(1, 101) <= 2)
						{
							giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(20, 51) * 10;
						}
						else if (UnityEngine.Random.Range(1, 201) <= 1)
						{
							giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(50, 100) * 10;
						}
						giftBoxSlotsItemInfo.shareName = "Coins x " + giftBoxSlotsItemInfo.Num.ToString();
						UserDataController.AddCoins(giftBoxSlotsItemInfo.Num);
						GrowthManger.mInstance.SetDataDisplayRefreshFlag(true);
					}
					else
					{
						giftBoxSlotsItemInfo.hTimeNum = 255;
						giftBoxSlotsItemInfo.shareName = "Weapon - " + weaponItemInfoByName.mNameDisplay + ": unlimited use!";
						GGCloudServiceKit.mInstance.UploadSlotTopPrize(giftBoxSlotsItemInfo.spriteName, "Unlimited");
						giftBoxSlotsItemInfo.isSpecialAward = true;
						weaponItemInfoByName.AddWeaponTime(3600f * (float)giftBoxSlotsItemInfo.hTimeNum, GWeaponRechargeType.WeaponTime);
						list.RemoveAt(index);
					}
				}
				else
				{
					if (giftBoxSlotsItemInfo.hTimeNum == 255)
					{
						giftBoxSlotsItemInfo.shareName = "Weapon - " + weaponItemInfoByName.mNameDisplay + ": unlimited use!";
						GGCloudServiceKit.mInstance.UploadSlotTopPrize(giftBoxSlotsItemInfo.spriteName, "Unlimited");
					}
					else
					{
						giftBoxSlotsItemInfo.shareName = string.Concat(new string[]
						{
							"Weapon - ",
							weaponItemInfoByName.mNameDisplay,
							": time + ",
							giftBoxSlotsItemInfo.hTimeNum.ToString(),
							"h"
						});
					}
					if (giftBoxSlotsItemInfo.hTimeNum > 6)
					{
						giftBoxSlotsItemInfo.isSpecialAward = true;
					}
					weaponItemInfoByName.AddWeaponTime(3600f * (float)giftBoxSlotsItemInfo.hTimeNum, GWeaponRechargeType.WeaponTime);
					list.RemoveAt(index);
				}
			}
			else
			{
				giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(10, 31) * 10;
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = "Coins_SlotLogo";
				if (UnityEngine.Random.Range(1, 101) <= 2)
				{
					giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(20, 51) * 10;
				}
				else if (UnityEngine.Random.Range(1, 201) <= 1)
				{
					giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(50, 100) * 10;
				}
				giftBoxSlotsItemInfo.shareName = "Coins x " + giftBoxSlotsItemInfo.Num.ToString();
				UserDataController.AddCoins(giftBoxSlotsItemInfo.Num);
				GrowthManger.mInstance.SetDataDisplayRefreshFlag(true);
			}
		}
		else if (num2 > 750 - giftBoxSlotsLuckyValue * 5 && num2 <= 780 - giftBoxSlotsLuckyValue * 4)
		{
			if (list2.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, list2.Count);
				giftBoxSlotsItemInfo.Num = 1;
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = list2[index] + "_SlotLogo";
				GSkinItemInfo skinItemInfoByName = GrowthManagerKit.GetSkinItemInfoByName(list2[index]);
				giftBoxSlotsItemInfo.shareName = "Skin - " + skinItemInfoByName.mNameDisplay;
				giftBoxSlotsItemInfo.isSpecialAward = true;
				skinItemInfoByName.Enable();
				list2.RemoveAt(index);
			}
			else
			{
				giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(10, 31) * 10;
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = "Coins_SlotLogo";
				if (UnityEngine.Random.Range(1, 101) <= 2)
				{
					giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(20, 51) * 10;
				}
				else if (UnityEngine.Random.Range(1, 201) <= 1)
				{
					giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(50, 100) * 10;
				}
				giftBoxSlotsItemInfo.shareName = "Coins x " + giftBoxSlotsItemInfo.Num.ToString();
				UserDataController.AddCoins(giftBoxSlotsItemInfo.Num);
				GrowthManger.mInstance.SetDataDisplayRefreshFlag(true);
			}
		}
		else if (num2 > 650 - giftBoxSlotsLuckyValue * 6 && num2 <= 750 - giftBoxSlotsLuckyValue * 5)
		{
			if (UnityEngine.Random.Range(1, 201) <= 4 + giftBoxSlotsLuckyValue)
			{
				giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(2, 6) * 5;
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = "Gems_SlotLogo";
			}
			else if (UnityEngine.Random.Range(1, 201) <= 4 + giftBoxSlotsLuckyValue)
			{
				giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(5, 100);
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = "Gems_SlotLogo";
			}
			else
			{
				giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(1, 10);
				giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
				giftBoxSlotsItemInfo.spriteName = "Gems_SlotLogo";
			}
			giftBoxSlotsItemInfo.shareName = "Gems x " + giftBoxSlotsItemInfo.Num.ToString();
			if (giftBoxSlotsItemInfo.Num >= 20)
			{
				giftBoxSlotsItemInfo.isSpecialAward = true;
			}
			if (giftBoxSlotsItemInfo.Num >= 30)
			{
				GGCloudServiceKit.mInstance.UploadSlotTopPrize(giftBoxSlotsItemInfo.spriteName, "x " + giftBoxSlotsItemInfo.Num.ToString());
			}
			UserDataController.AddGems(giftBoxSlotsItemInfo.Num);
			GrowthManger.mInstance.SetDataDisplayRefreshFlag(true);
		}
		else if (num2 > 500 - giftBoxSlotsLuckyValue * 7 && num2 <= 650 - giftBoxSlotsLuckyValue * 6)
		{
			int index = UnityEngine.Random.Range(0, list6.Count);
			giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(1, 6);
			giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
			giftBoxSlotsItemInfo.spriteName = list6[index] + "_SlotLogo";
			GMultiplayerBuffItemInfo multiplayerBuffItemInfoByName = GrowthManagerKit.GetMultiplayerBuffItemInfoByName(list6[index]);
			giftBoxSlotsItemInfo.shareName = multiplayerBuffItemInfoByName.mNameDisplay + " x " + giftBoxSlotsItemInfo.Num.ToString();
			if (giftBoxSlotsItemInfo.Num > 3)
			{
				giftBoxSlotsItemInfo.isSpecialAward = true;
			}
			multiplayerBuffItemInfoByName.AddBuffNum(giftBoxSlotsItemInfo.Num, true);
			list6.RemoveAt(index);
		}
		else if (num2 > 350 - giftBoxSlotsLuckyValue * 8 && num2 <= 500 - giftBoxSlotsLuckyValue * 7)
		{
			int index = UnityEngine.Random.Range(0, list7.Count);
			giftBoxSlotsItemInfo.Num = 1;
			giftBoxSlotsItemInfo.hTimeNum = ((UnityEngine.Random.Range(1, 11) <= 2) ? UnityEngine.Random.Range(1, 13) : UnityEngine.Random.Range(1, 4));
			giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
			giftBoxSlotsItemInfo.spriteName = list7[index] + "_SlotLogo";
			GArmorItemInfo armorItemInfoByName = GrowthManagerKit.GetArmorItemInfoByName(list7[index]);
			giftBoxSlotsItemInfo.shareName = armorItemInfoByName.mNameDisplay + " time + " + giftBoxSlotsItemInfo.hTimeNum.ToString() + "h";
			if (giftBoxSlotsItemInfo.hTimeNum > 3)
			{
				giftBoxSlotsItemInfo.isSpecialAward = true;
			}
			armorItemInfoByName.AddAutoSupplyTime(3600f * (float)giftBoxSlotsItemInfo.hTimeNum);
			list7.RemoveAt(index);
		}
		else if (num2 > 200 - giftBoxSlotsLuckyValue * 9 && num2 <= 350 - giftBoxSlotsLuckyValue * 8)
		{
			int index = UnityEngine.Random.Range(0, list8.Count);
			int num3 = UnityEngine.Random.Range(0, 100);
			int num4 = (num3 > 70) ? ((num3 <= 70 || num3 > 90) ? 3 : 2) : 1;
			giftBoxSlotsItemInfo.Num = 1;
			giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
			giftBoxSlotsItemInfo.spriteName = list8[index] + "_SlotLogo_" + num4.ToString();
			giftBoxSlotsItemInfo.shareName = string.Concat(new string[]
			{
				list8[index],
				" Card Lv-",
				num4.ToString(),
				" x ",
				giftBoxSlotsItemInfo.Num.ToString()
			});
			if (num4 > 1)
			{
				giftBoxSlotsItemInfo.isSpecialAward = true;
				if (num4 == 3)
				{
					GGCloudServiceKit.mInstance.UploadSlotTopPrize(giftBoxSlotsItemInfo.spriteName, "x " + giftBoxSlotsItemInfo.Num.ToString());
				}
			}
			GrowthManagerKit.AddWeaponPropertyCardNum(list8[index], num4, 1);
			list8.RemoveAt(index);
		}
		else
		{
			giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(10, 31) * 10;
			giftBoxSlotsItemInfo.index = giftBoxSlotsTableInfo.resultIndex;
			giftBoxSlotsItemInfo.spriteName = "Coins_SlotLogo";
			if (UnityEngine.Random.Range(1, 101) <= 2 + giftBoxSlotsLuckyValue)
			{
				giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(20, 51) * 10;
				giftBoxSlotsItemInfo.isSpecialAward = false;
			}
			else if (UnityEngine.Random.Range(1, 201) <= 1 + giftBoxSlotsLuckyValue)
			{
				giftBoxSlotsItemInfo.Num = UnityEngine.Random.Range(50, 100) * 10;
				giftBoxSlotsItemInfo.isSpecialAward = true;
			}
			giftBoxSlotsItemInfo.shareName = "Coins x " + giftBoxSlotsItemInfo.Num.ToString();
			UserDataController.AddCoins(giftBoxSlotsItemInfo.Num);
			GrowthManger.mInstance.SetDataDisplayRefreshFlag(true);
		}
		for (int j = 0; j < tableSize; j++)
		{
			if (j == giftBoxSlotsTableInfo.resultIndex)
			{
				giftBoxSlotsTableInfo.itemList.Add(giftBoxSlotsItemInfo);
			}
			else
			{
				GiftBoxSlotsItemInfo giftBoxSlotsItemInfo2 = new GiftBoxSlotsItemInfo();
				num2 = UnityEngine.Random.Range(1, 1001);
				if (num2 > 950)
				{
					if (list4.Count > 0)
					{
						int index = UnityEngine.Random.Range(0, list4.Count);
						giftBoxSlotsItemInfo2.Num = 1;
						giftBoxSlotsItemInfo2.index = j;
						giftBoxSlotsItemInfo2.spriteName = list4[index] + "_SlotLogo";
						list4.RemoveAt(index);
					}
					else
					{
						giftBoxSlotsItemInfo2.Num = UnityEngine.Random.Range(10, 101) * 10;
						giftBoxSlotsItemInfo2.index = j;
						giftBoxSlotsItemInfo2.spriteName = "Coins_SlotLogo";
					}
				}
				else if (num2 > 900 && num2 <= 950)
				{
					if (list3.Count > 0)
					{
						int index = UnityEngine.Random.Range(0, list3.Count);
						giftBoxSlotsItemInfo2.Num = 1;
						giftBoxSlotsItemInfo2.index = j;
						giftBoxSlotsItemInfo2.spriteName = list3[index] + "_SlotLogo";
						list3.RemoveAt(index);
					}
					else
					{
						giftBoxSlotsItemInfo2.Num = UnityEngine.Random.Range(10, 101) * 10;
						giftBoxSlotsItemInfo2.index = j;
						giftBoxSlotsItemInfo2.spriteName = "Coins_SlotLogo";
					}
				}
				else if (num2 > 850 && num2 <= 900)
				{
					if (list5.Count > 0)
					{
						int index = UnityEngine.Random.Range(0, list5.Count);
						giftBoxSlotsItemInfo2.Num = 1;
						giftBoxSlotsItemInfo2.index = j;
						giftBoxSlotsItemInfo2.spriteName = list5[index] + "_SlotLogo";
						list5.RemoveAt(index);
					}
					else
					{
						giftBoxSlotsItemInfo2.Num = UnityEngine.Random.Range(10, 101) * 10;
						giftBoxSlotsItemInfo2.index = j;
						giftBoxSlotsItemInfo2.spriteName = "Coins_SlotLogo";
					}
				}
				else if (num2 > 650 && num2 <= 850)
				{
					if (list.Count > 0)
					{
						int index = UnityEngine.Random.Range(0, list.Count);
						giftBoxSlotsItemInfo2.Num = 1;
						giftBoxSlotsItemInfo2.index = j;
						giftBoxSlotsItemInfo2.spriteName = list[index] + "_SlotLogo";
						list.RemoveAt(index);
					}
					else
					{
						giftBoxSlotsItemInfo2.Num = UnityEngine.Random.Range(10, 101) * 10;
						giftBoxSlotsItemInfo2.index = j;
						giftBoxSlotsItemInfo2.spriteName = "Coins_SlotLogo";
					}
				}
				else if (num2 > 450 && num2 <= 650)
				{
					if (list2.Count > 0)
					{
						int index = UnityEngine.Random.Range(0, list2.Count);
						giftBoxSlotsItemInfo2.Num = 1;
						giftBoxSlotsItemInfo2.index = j;
						giftBoxSlotsItemInfo2.spriteName = list2[index] + "_SlotLogo";
						list2.RemoveAt(index);
					}
					else
					{
						giftBoxSlotsItemInfo2.Num = UnityEngine.Random.Range(10, 101) * 10;
						giftBoxSlotsItemInfo2.index = j;
						giftBoxSlotsItemInfo2.spriteName = "Coins_SlotLogo";
					}
				}
				else if (num2 > 350 && num2 <= 450)
				{
					giftBoxSlotsItemInfo2.Num = UnityEngine.Random.Range(1, 51);
					giftBoxSlotsItemInfo2.index = j;
					giftBoxSlotsItemInfo2.spriteName = "Gems_SlotLogo";
				}
				else if (num2 > 250 && num2 <= 350)
				{
					int index = UnityEngine.Random.Range(0, list6.Count);
					giftBoxSlotsItemInfo2.Num = UnityEngine.Random.Range(1, 6);
					giftBoxSlotsItemInfo2.index = j;
					giftBoxSlotsItemInfo2.spriteName = list6[index] + "_SlotLogo";
				}
				else if (num2 > 100 && num2 <= 250)
				{
					int index = UnityEngine.Random.Range(0, list8.Count);
					giftBoxSlotsItemInfo2.Num = 1;
					giftBoxSlotsItemInfo2.index = j;
					int num5 = UnityEngine.Random.Range(1, 4);
					giftBoxSlotsItemInfo2.spriteName = list8[index] + "_SlotLogo_" + num5.ToString();
				}
				else if (num2 > 50 && num2 <= 100)
				{
					int index = UnityEngine.Random.Range(0, list7.Count);
					giftBoxSlotsItemInfo2.Num = 1;
					giftBoxSlotsItemInfo2.index = j;
					giftBoxSlotsItemInfo2.spriteName = list7[index] + "_SlotLogo";
				}
				else
				{
					giftBoxSlotsItemInfo2.Num = UnityEngine.Random.Range(10, 101) * 10;
					giftBoxSlotsItemInfo2.index = j;
					giftBoxSlotsItemInfo2.spriteName = "Coins_SlotLogo";
				}
				giftBoxSlotsTableInfo.itemList.Add(giftBoxSlotsItemInfo2);
			}
		}
		GrowthManagerKit.SubOneGiftBox();
		return giftBoxSlotsTableInfo;
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x00053F82 File Offset: 0x00052382
	public static int GetHolidayCurrency()
	{
		return UserDataController.GetHolidayCurrency();
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x00053F89 File Offset: 0x00052389
	public static void SetHolidayCurrency(int num)
	{
		UserDataController.SetHolidayCurrency(num);
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x00053F91 File Offset: 0x00052391
	public static void AddHolidayCurrency(int addNum)
	{
		UserDataController.AddHolidayCurrency(addNum);
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x00053F99 File Offset: 0x00052399
	public static void SubHolidayCurrency(int subNum)
	{
		UserDataController.SubHolidayCurrency(subNum);
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x00053FA4 File Offset: 0x000523A4
	public static HolidaySlotsItemInfo GetHolidaySlotsResultInfo()
	{
		HolidaySlotsItemInfo holidaySlotsItemInfo = new HolidaySlotsItemInfo();
		int num = UnityEngine.Random.Range(1, 1001);
		int[] array = new int[]
		{
			5,
			20
		};
		int[] array2 = new int[]
		{
			1,
			4
		};
		string[] array3 = new string[]
		{
			"SM4",
			"SG36K",
			"SAUG"
		};
		string[] array4 = new string[]
		{
			"Skin_43",
			"Skin_44",
			"Skin_45",
			"Skin_46",
			"Skin_47",
			"Skin_48"
		};
		string[] array5 = new string[]
		{
			"Cape_8"
		};
		string[] array6 = new string[0];
		string[] allMultiplayerBuffNameList = GrowthManagerKit.GetAllMultiplayerBuffNameList();
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		List<string> list4 = new List<string>();
		for (int i = 0; i < array3.Length; i++)
		{
			if (!GrowthManagerKit.GetWeaponItemInfoByName(array3[i]).mIsNoLimitedUse)
			{
				list.Add(array3[i]);
			}
		}
		for (int i = 0; i < array4.Length; i++)
		{
			if (!GrowthManagerKit.GetSkinItemInfoByName(array4[i]).mIsEnabled)
			{
				list2.Add(array4[i]);
			}
		}
		for (int i = 0; i < array5.Length; i++)
		{
			if (!GrowthManagerKit.GetCapeItemInfoByName(array5[i]).mIsEnabled)
			{
				list3.Add(array5[i]);
			}
		}
		for (int i = 0; i < array6.Length; i++)
		{
			if (!GrowthManagerKit.GetHatItemInfoByName(array6[i]).mIsEnabled)
			{
				list4.Add(array6[i]);
			}
		}
		if (num <= 10)
		{
			if (list.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, list.Count);
				GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(list[index]);
				weaponItemInfoByName.AddWeaponTime(918000f, GWeaponRechargeType.WeaponTime);
				holidaySlotsItemInfo.spriteName = weaponItemInfoByName.mName + "_SlotLogo";
				holidaySlotsItemInfo.num = 1;
			}
			else
			{
				holidaySlotsItemInfo.spriteName = "Coins_SlotLogo";
				holidaySlotsItemInfo.num = UnityEngine.Random.Range(array[0], array[1]);
				GrowthManagerKit.AddCoins(holidaySlotsItemInfo.num);
			}
		}
		else if (num <= 15)
		{
			if (list4.Count > 0)
			{
				int index2 = UnityEngine.Random.Range(0, list4.Count);
				GHatItemInfo hatItemInfoByName = GrowthManagerKit.GetHatItemInfoByName(list4[index2]);
				hatItemInfoByName.Enable();
				holidaySlotsItemInfo.spriteName = hatItemInfoByName.mName + "_SlotLogo";
				holidaySlotsItemInfo.num = 1;
			}
			else
			{
				holidaySlotsItemInfo.spriteName = "Coins_SlotLogo";
				holidaySlotsItemInfo.num = UnityEngine.Random.Range(array[0], array[1]);
				GrowthManagerKit.AddCoins(holidaySlotsItemInfo.num);
			}
		}
		else if (num <= 20)
		{
			if (list3.Count > 0)
			{
				int index3 = UnityEngine.Random.Range(0, list3.Count);
				GCapeItemInfo capeItemInfoByName = GrowthManagerKit.GetCapeItemInfoByName(list3[index3]);
				capeItemInfoByName.Enable();
				holidaySlotsItemInfo.spriteName = capeItemInfoByName.mName + "_SlotLogo";
				holidaySlotsItemInfo.num = 1;
			}
			else
			{
				holidaySlotsItemInfo.spriteName = "Coins_SlotLogo";
				holidaySlotsItemInfo.num = UnityEngine.Random.Range(array[0], array[1]);
				GrowthManagerKit.AddCoins(holidaySlotsItemInfo.num);
			}
		}
		else if (num <= 40)
		{
			GrowthManagerKit.AddGiftBox(1);
			holidaySlotsItemInfo.spriteName = "GiftBox_SlotLogo";
			holidaySlotsItemInfo.num = 1;
		}
		else if (num <= 60)
		{
			holidaySlotsItemInfo.spriteName = "Gems_SlotLogo";
			holidaySlotsItemInfo.num = UnityEngine.Random.Range(array2[0], array2[1]);
			if (UnityEngine.Random.Range(1, 101) <= 1)
			{
				holidaySlotsItemInfo.num *= 5;
			}
			GrowthManagerKit.AddGems(holidaySlotsItemInfo.num);
		}
		else if (num <= 80)
		{
			if (list2.Count > 0)
			{
				int index4 = UnityEngine.Random.Range(0, list2.Count);
				GSkinItemInfo skinItemInfoByName = GrowthManagerKit.GetSkinItemInfoByName(list2[index4]);
				skinItemInfoByName.Enable();
				holidaySlotsItemInfo.spriteName = skinItemInfoByName.mName + "_SlotLogo";
				holidaySlotsItemInfo.num = 1;
			}
			else
			{
				holidaySlotsItemInfo.spriteName = "Coins_SlotLogo";
				holidaySlotsItemInfo.num = UnityEngine.Random.Range(array[0], array[1]);
				GrowthManagerKit.AddCoins(holidaySlotsItemInfo.num);
			}
		}
		else if (num <= 160)
		{
			int num2 = UnityEngine.Random.Range(0, allMultiplayerBuffNameList.Length);
			GMultiplayerBuffItemInfo multiplayerBuffItemInfoByName = GrowthManagerKit.GetMultiplayerBuffItemInfoByName(allMultiplayerBuffNameList[num2]);
			multiplayerBuffItemInfoByName.AddBuffNum(1);
			holidaySlotsItemInfo.spriteName = allMultiplayerBuffNameList[num2] + "_SlotLogo";
			holidaySlotsItemInfo.num = 1;
		}
		else
		{
			holidaySlotsItemInfo.spriteName = "Coins_SlotLogo";
			holidaySlotsItemInfo.num = UnityEngine.Random.Range(array[0], array[1]);
			if (UnityEngine.Random.Range(1, 101) <= 1)
			{
				holidaySlotsItemInfo.num *= 10;
			}
			GrowthManagerKit.AddCoins(holidaySlotsItemInfo.num);
		}
		return holidaySlotsItemInfo;
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x00054490 File Offset: 0x00052890
	public static int[] HolidayRechargeRewardTarget()
	{
		return UserDataController.HolidayRechargeRewardTarget;
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00054497 File Offset: 0x00052897
	public static void AddHolidayRechargeRecord(int gemsRechargeNum)
	{
		UserDataController.AddHolidayRechargeRecord(gemsRechargeNum);
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x0005449F File Offset: 0x0005289F
	public static int GetHolidayRechargeRecord()
	{
		return UserDataController.GetHolidayRechargeRecord();
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x000544A6 File Offset: 0x000528A6
	public static bool HasEverGotHolidayRechargeReward(int rewardLv)
	{
		return UserDataController.HasEverGotHolidayRechargeReward(rewardLv);
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x000544AE File Offset: 0x000528AE
	public static bool CanGetHolidayRechargeReward(int rewardLv)
	{
		return UserDataController.CanGetHolidayRechargeReward(rewardLv);
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x000544B8 File Offset: 0x000528B8
	public static void ReceiveHolidayRechargeReward(int rewardLv)
	{
		switch (rewardLv)
		{
		case 1:
			GrowthManagerKit.AddGems(10);
			GrowthManagerKit.AddGiftBox(1);
			GrowthManagerKit.AddWeaponPropertyCardNum("Power", 2, 3);
			UserDataController.VerifyHolidayRechargeRewardGet(rewardLv);
			break;
		case 2:
			GrowthManagerKit.AddGems(25);
			GrowthManagerKit.AddGiftBox(3);
			GrowthManagerKit.AddWeaponPropertyCardNum("Clip", 2, 5);
			UserDataController.VerifyHolidayRechargeRewardGet(rewardLv);
			break;
		case 3:
			GrowthManagerKit.AddGems(50);
			GrowthManagerKit.AddGiftBox(5);
			GrowthManagerKit.GetWeaponItemInfoByName("Shark").AddWeaponTime(918000f, GWeaponRechargeType.WeaponTime);
			UserDataController.VerifyHolidayRechargeRewardGet(rewardLv);
			break;
		case 4:
			GrowthManagerKit.AddGems(150);
			GrowthManagerKit.AddGiftBox(10);
			GrowthManagerKit.AddWeaponPropertyCardNum("Clip", 3, 5);
			UserDataController.VerifyHolidayRechargeRewardGet(rewardLv);
			break;
		case 5:
			GrowthManagerKit.AddGems(300);
			GrowthManagerKit.AddGiftBox(20);
			GrowthManagerKit.AddWeaponPropertyCardNum("Power", 3, 5);
			UserDataController.VerifyHolidayRechargeRewardGet(rewardLv);
			break;
		case 6:
			GrowthManagerKit.AddGems(500);
			GrowthManagerKit.AddGiftBox(30);
			GrowthManagerKit.GetWeaponItemInfoByName("NuclearEmitter").AddWeaponTime(918000f, GWeaponRechargeType.WeaponTime);
			UserDataController.VerifyHolidayRechargeRewardGet(rewardLv);
			break;
		}
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x000545E8 File Offset: 0x000529E8
	public static GMultiplayerBuffItemInfo GetMultiplayerBuffItemInfoByName(string name)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllMultiplayerBuffNameList.Length; i++)
		{
			if (UserDataController.AllMultiplayerBuffNameList[i] == name)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			return GrowthManger.mInstance.GetMultiplayerBuffItemInfoByName(name);
		}
		return null;
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x0005463C File Offset: 0x00052A3C
	public static string[] GetAllMultiplayerBuffNameList()
	{
		string[] array = new string[UserDataController.AllMultiplayerBuffNameList.Length - 1];
		int num = 0;
		for (int i = 0; i < UserDataController.AllMultiplayerBuffNameList.Length; i++)
		{
			if (!(UserDataController.AllMultiplayerBuffNameList[i] == "CoinsX2Buff"))
			{
				array[num] = UserDataController.AllMultiplayerBuffNameList[i];
				num++;
			}
		}
		return array;
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x0005469C File Offset: 0x00052A9C
	public static GMultiplayerBuffItemInfo[] GetAllMultiplayerBuffItemInfo()
	{
		string[] allMultiplayerBuffNameList = GrowthManagerKit.GetAllMultiplayerBuffNameList();
		GMultiplayerBuffItemInfo[] array = new GMultiplayerBuffItemInfo[allMultiplayerBuffNameList.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = GrowthManagerKit.GetMultiplayerBuffItemInfoByName(allMultiplayerBuffNameList[i]);
		}
		return array;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x000546D8 File Offset: 0x00052AD8
	public static float GetBuffValueInMultiplayer(BuffTypeInMultiplayer bType)
	{
		float result;
		switch (bType)
		{
		case BuffTypeInMultiplayer.SpeedPlusBuff:
			if (GrowthManger.mInstance.mBuffRestTimeListInMultiplayer[1] > 0f)
			{
				result = 0.1f;
			}
			else
			{
				result = 0f;
			}
			break;
		case BuffTypeInMultiplayer.HpPlusBuff:
			if (GrowthManger.mInstance.mBuffRestTimeListInMultiplayer[2] > 0f)
			{
				result = 0.5f;
			}
			else
			{
				result = 0f;
			}
			break;
		case BuffTypeInMultiplayer.CoinsX2Buff:
			if (GrowthManger.mInstance.mBuffRestTimeListInMultiplayer[3] > 0f)
			{
				result = 1f;
			}
			else
			{
				result = 0f;
			}
			break;
		case BuffTypeInMultiplayer.ExpX2Buff:
			if (GrowthManger.mInstance.mBuffRestTimeListInMultiplayer[4] > 0f)
			{
				result = 1f;
			}
			else
			{
				result = 0f;
			}
			break;
		case BuffTypeInMultiplayer.DamagePlusBuff:
			if (GrowthManger.mInstance.mBuffRestTimeListInMultiplayer[5] > 0f)
			{
				result = 0.2f;
			}
			else
			{
				result = 0f;
			}
			break;
		case BuffTypeInMultiplayer.HpRecoveryBuff:
			if (GrowthManger.mInstance.mBuffRestTimeListInMultiplayer[6] > 0f)
			{
				result = 1f;
			}
			else
			{
				result = 0f;
			}
			break;
		default:
			result = 0f;
			break;
		}
		return result;
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0005483C File Offset: 0x00052C3C
	public static void ClearAllBuffEffect()
	{
		GrowthManger.mInstance.ClearAllBuffEffect();
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x00054848 File Offset: 0x00052C48
	public static List<string> SystemMsgList()
	{
		return GrowthManger.mInstance.mSystemMsgList;
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x00054854 File Offset: 0x00052C54
	public static void PushSystemMsg(string msg)
	{
		GrowthManger.mInstance.mSystemMsgList.Add(msg);
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x00054868 File Offset: 0x00052C68
	public static string PopSystemMsg()
	{
		string result = string.Empty;
		if (GrowthManger.mInstance.mSystemMsgList.Count > 0)
		{
			result = GrowthManger.mInstance.mSystemMsgList[0].ToString();
			GrowthManger.mInstance.mSystemMsgList.RemoveAt(0);
		}
		return result;
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x000548B8 File Offset: 0x00052CB8
	public static List<string> PopAllSystemMsg()
	{
		if (GrowthManger.mInstance.mSystemMsgList.Count > 0)
		{
			string[] array = new string[GrowthManger.mInstance.mSystemMsgList.Count];
			GrowthManger.mInstance.mSystemMsgList.CopyTo(array);
			GrowthManger.mInstance.mSystemMsgList.Clear();
			List<string> list = new List<string>();
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(array[i]);
			}
			return list;
		}
		return new List<string>();
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x00054938 File Offset: 0x00052D38
	public static PlayerEnchantmentProperty EProperty()
	{
		return GrowthManger.mInstance.playerEnchantmentProperty;
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x00054944 File Offset: 0x00052D44
	public static void AddEProperty(SceneEnchantmentProps propsType)
	{
		GrowthManger.mInstance.playerEnchantmentProperty.AddScenePropsProperty(propsType);
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x00054956 File Offset: 0x00052D56
	public static void AddCustomEProperty(SceneEnchantmentProps propType, float additionValue, float validTimeRest)
	{
		GrowthManger.mInstance.playerEnchantmentProperty.AddCustomScenePropsProperty(propType, additionValue, validTimeRest);
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0005496A File Offset: 0x00052D6A
	public static void AddCustomEProperty(SceneEnchantmentProps propType, float additionValue)
	{
		GrowthManger.mInstance.playerEnchantmentProperty.AddCustomScenePropsProperty(propType, additionValue);
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x0005497D File Offset: 0x00052D7D
	public static void RemoveEProperty(SceneEnchantmentProps propsType)
	{
		GrowthManger.mInstance.playerEnchantmentProperty.RemoveScenePropsProperty(propsType);
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x0005498F File Offset: 0x00052D8F
	public static void InitEProperty()
	{
		GrowthManger.mInstance.playerEnchantmentProperty.ClearAllAddition();
		GrowthManger.mInstance.playerEnchantmentProperty.Init();
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x000549AF File Offset: 0x00052DAF
	public static void ClearPotionEProperty()
	{
		GrowthManger.mInstance.playerEnchantmentProperty.ClearPotionAddition();
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x000549C0 File Offset: 0x00052DC0
	public static void ClearScenePropsEProperty()
	{
		GrowthManger.mInstance.playerEnchantmentProperty.ClearScenePropsAddition();
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x000549D1 File Offset: 0x00052DD1
	public static void ClearEquipPropsEProperty()
	{
		GrowthManger.mInstance.playerEnchantmentProperty.ClearEquipAddition();
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x000549E2 File Offset: 0x00052DE2
	public static void ClearAllEProperty()
	{
		GrowthManger.mInstance.playerEnchantmentProperty.ClearAllAddition();
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x000549F3 File Offset: 0x00052DF3
	public static List<EnchantmentDetails> EnabledTickedEPropertyList()
	{
		return GrowthManger.mInstance.playerEnchantmentProperty.tickedAdditionList;
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x00054A04 File Offset: 0x00052E04
	public static CareerStatValuesSeason GetCareerStatValuesOfSeason()
	{
		CareerStatValuesSeason careerStatValuesSeason = new CareerStatValuesSeason();
		careerStatValuesSeason.totalKillingCompetitionModeJoinCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeJoinSeason);
		careerStatValuesSeason.totalExplosionModeJoinCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeJoinSeason);
		careerStatValuesSeason.totalStrongholdModeJoinCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeJoinSeason);
		careerStatValuesSeason.totalKillingCompetitionModeVictoryCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictorySeason);
		careerStatValuesSeason.totalExplosionModeVictoryCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictorySeason);
		careerStatValuesSeason.totalStrongholdModeVictoryCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictorySeason);
		careerStatValuesSeason.killingCompetitionModeVictoryRate = ((careerStatValuesSeason.totalKillingCompetitionModeJoinCount <= 0) ? 0 : (100 * careerStatValuesSeason.totalKillingCompetitionModeVictoryCount / careerStatValuesSeason.totalKillingCompetitionModeJoinCount));
		careerStatValuesSeason.explosionModeVictoryRate = ((careerStatValuesSeason.totalExplosionModeJoinCount <= 0) ? 0 : (100 * careerStatValuesSeason.totalExplosionModeVictoryCount / careerStatValuesSeason.totalExplosionModeJoinCount));
		careerStatValuesSeason.strongholdModeVictoryRate = ((careerStatValuesSeason.totalStrongholdModeJoinCount <= 0) ? 0 : (100 * careerStatValuesSeason.totalStrongholdModeVictoryCount / careerStatValuesSeason.totalStrongholdModeJoinCount));
		careerStatValuesSeason.totalKillingCompetitionModeMvpCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeMvpSeason);
		careerStatValuesSeason.totalExplosionModeMvpCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeMvpSeason);
		careerStatValuesSeason.totalStrongholdModeMvpCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeMvpSeason);
		careerStatValuesSeason.totalKilling = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInWorldwideMultiplayerSeason);
		careerStatValuesSeason.totalHeadshotKilling = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalHeadshotKillSeason);
		careerStatValuesSeason.totalGodLikeKilling = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKillSeason);
		careerStatValuesSeason.seasonScore = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tSeasonScore);
		careerStatValuesSeason.seasonRank = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tSeasonRank);
		return careerStatValuesSeason;
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x00054B6C File Offset: 0x00052F6C
	public static CareerStatValuesAll GetCareerStatValuesOfAll()
	{
		CareerStatValuesAll careerStatValuesAll = new CareerStatValuesAll();
		careerStatValuesAll.totalKillingCompetitionModeJoinCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeJoin);
		careerStatValuesAll.totalExplosionModeJoinCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeJoin);
		careerStatValuesAll.totalStrongholdModeJoinCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeJoin);
		careerStatValuesAll.totalKillingCompetitionModeVictoryCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictory);
		careerStatValuesAll.totalExplosionModeVictoryCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictory);
		careerStatValuesAll.totalStrongholdModeVictoryCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictory);
		careerStatValuesAll.killingCompetitionModeVictoryRate = ((careerStatValuesAll.totalKillingCompetitionModeJoinCount <= 0) ? 0 : (100 * careerStatValuesAll.totalKillingCompetitionModeVictoryCount / careerStatValuesAll.totalKillingCompetitionModeJoinCount));
		careerStatValuesAll.explosionModeVictoryRate = ((careerStatValuesAll.totalExplosionModeJoinCount <= 0) ? 0 : (100 * careerStatValuesAll.totalExplosionModeVictoryCount / careerStatValuesAll.totalExplosionModeJoinCount));
		careerStatValuesAll.strongholdModeVictoryRate = ((careerStatValuesAll.totalStrongholdModeJoinCount <= 0) ? 0 : (100 * careerStatValuesAll.totalStrongholdModeVictoryCount / careerStatValuesAll.totalStrongholdModeJoinCount));
		careerStatValuesAll.totalKillingCompetitionModeMvpCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeMvp);
		careerStatValuesAll.totalExplosionModeMvpCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeMvp);
		careerStatValuesAll.totalStrongholdModeMvpCount = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeMvp);
		careerStatValuesAll.totalKilling = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInWorldwideMultiplayer);
		careerStatValuesAll.totalHeadshotKilling = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalHeadshotKill);
		careerStatValuesAll.totalGodLikeKilling = UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKill);
		return careerStatValuesAll;
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x00054C94 File Offset: 0x00053094
	public static void ClearCareerStatValuesOfSeason()
	{
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeJoinSeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeJoinSeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeJoinSeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictorySeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictorySeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictorySeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeMvpSeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeMvpSeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeMvpSeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInWorldwideMultiplayerSeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalHeadshotKillSeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKillSeason, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tSeasonScore, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tSeasonRank, 0);
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00054D38 File Offset: 0x00053138
	public static void AddSeasonScore(int addNum)
	{
		int fightingStatisticsValue = GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tSeasonScore);
		int newValue = Math.Max(fightingStatisticsValue + addNum, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tSeasonScore, newValue);
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x00054D60 File Offset: 0x00053160
	public static void SubSeasonScore(int subNum)
	{
		int fightingStatisticsValue = GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tSeasonScore);
		int newValue = Math.Max(fightingStatisticsValue - subNum, 0);
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tSeasonScore, newValue);
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x00054D87 File Offset: 0x00053187
	public static int GetSeasonScore()
	{
		return GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tSeasonScore);
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x00054D90 File Offset: 0x00053190
	public static string GetSeasonMark()
	{
		return UserDataController.GetSeasonMark();
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x00054D97 File Offset: 0x00053197
	public static void SetSeasonMark(string newMark)
	{
		UserDataController.SetSeasonMark(newMark);
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x00054D9F File Offset: 0x0005319F
	public static int GetSeasonRank()
	{
		return GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tSeasonRank);
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x00054DA8 File Offset: 0x000531A8
	public static void SetSeasonRank(int newRank)
	{
		UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tSeasonRank, newRank);
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x00054DB2 File Offset: 0x000531B2
	public static void SetWeaponPropertyCardNum(string propertyName, int lv, int newNum)
	{
		UserDataController.SetWeaponPropertyCardNum(propertyName, lv, newNum);
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00054DBC File Offset: 0x000531BC
	public static int GetWeaponPropertyCardNum(string propertyName, int lv)
	{
		return UserDataController.GetWeaponPropertyCardNum(propertyName, lv);
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x00054DC5 File Offset: 0x000531C5
	public static void AddWeaponPropertyCardNum(string propertyName, int lv, int addNum)
	{
		UserDataController.AddWeaponPropertyCardNum(propertyName, lv, addNum);
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x00054DCF File Offset: 0x000531CF
	public static void SubWeaponPropertyCardNum(string propertyName, int lv, int subNum)
	{
		UserDataController.SubWeaponPropertyCardNum(propertyName, lv, subNum);
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00054DD9 File Offset: 0x000531D9
	public static bool IsWeaponChipUnlocked(string weaponName, int chipIndex, int maxChips)
	{
		return UserDataController.IsWeaponChipUnlocked(weaponName, chipIndex, maxChips);
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x00054DE3 File Offset: 0x000531E3
	public static void UnlockWeaponChip(string weaponName, int chipIndex, int maxChips)
	{
		UserDataController.UnlockWeaponChip(weaponName, chipIndex, maxChips);
	}
}
