using System;
using System.Collections.Generic;
using GrowthSystem;
using UnityEngine;

// Token: 0x020001D3 RID: 467
public class UserDataController : MonoBehaviour
{
	// Token: 0x06000C74 RID: 3188 RVA: 0x0005DB54 File Offset: 0x0005BF54
	public static void FirstLoadSetting()
	{
		GOGPlayerPrefabs.SetString("GOGLastDeviceID", SystemInfo.deviceUniqueIdentifier);
		GOGPlayerPrefabs.Save();
		if (GOGPlayerPrefabs.GetInt("GOGFirstLoadGameSetting_V_1_0_0", 0) == 0)
		{
			UserDataController.EnableSkin("Skin_1");
			UserDataController.EnableSkin("Skin_2");
			UserDataController.EnableSkin("Skin_3");
			UserDataController.EnableSkin("Skin_4");
			UserDataController.SetWeaponTimeRest("BallisticKnife", 918000f);
			UserDataController.SetWeaponTimeRest("GLOCK21", 918000f);
			UserDataController.SetWeaponTimeRest("MP5KA5", 918000f);
			UserDataController.SetWeaponTimeRest("M67", 918000f);
			UserDataController.SetWeaponTimeRest("MilkBomb", 918000f);
			UserDataController.SetWeaponTimeRest("GingerbreadBomb", 918000f);
			UIUserDataController.SetThrowWeaponNum("M67", 0);
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_1", "BallisticKnife");
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_2", "GLOCK21");
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_3", "MP5KA5");
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_4", string.Empty);
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_5", string.Empty);
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_6", string.Empty);
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_7", string.Empty);
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_8", string.Empty);
			GOGPlayerPrefabs.SetString("CurSettedSkinName", "Skin_" + UnityEngine.Random.Range(1, 5).ToString());
			GOGPlayerPrefabs.SetInt("GOGFirstLoadGameSetting_V_1_0_0", 1);
			if (GameVersionController.isCopsNRobbers2)
			{
				UserDataController.SetCoins(1000);
				UserDataController.SetWeaponTimeRest("AK47", 86400f);
				UserDataController.SetWeaponTimeRest("M4", 86400f);
			}
		}
		if (GOGPlayerPrefabs.GetInt("GOGFirstLoadGameSetting_V_1_0_2", 0) == 0)
		{
			UserDataController.AddCoins(488);
			GOGPlayerPrefabs.SetInt("GOGFirstLoadGameSetting_V_1_0_2", 1);
		}
		if (GOGPlayerPrefabs.GetInt("GOGFirstLoadGameSetting_V_1_0_4", 0) == 0)
		{
			UserDataController.SetWeaponTimeRest("SmokeBomb", 918000f);
			UserDataController.SetWeaponTimeRest("FlashBomb", 918000f);
			GOGPlayerPrefabs.SetInt("GOGFirstLoadGameSetting_V_1_0_4", 1);
		}
		if (GOGPlayerPrefabs.GetInt("GOGFirstLoadGameSetting_V_1_2_0", 0) == 0)
		{
			UserDataController.SetWeaponTimeRest("SnowmanBomb", 918000f);
			GOGPlayerPrefabs.SetInt("GOGFirstLoadGameSetting_V_1_2_0", 1);
		}
		if (GOGPlayerPrefabs.GetInt("GOGFLGS_V_2_0_1", 0) == 0)
		{
			for (int i = 1; i <= GrowthBaseValue.mGBV_TotalKillingCompetitionModeVictoryTarget.Length; i++)
			{
				UserDataController.ClearFightingStatisticsRewardGotRecord(FightingStatisticsTag.tTotalKillingCompetitionModeVictory, i);
			}
			UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillingCompetitionModeVictory, 0);
			for (int j = 1; j <= GrowthBaseValue.mGBV_TotalExplosionModeVictoryTarget.Length; j++)
			{
				UserDataController.ClearFightingStatisticsRewardGotRecord(FightingStatisticsTag.tTotalExplosionModeVictory, j);
			}
			UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalExplosionModeVictory, 0);
			for (int k = 1; k <= GrowthBaseValue.mGBV_TotalStrongholdModeVictoryTarget.Length; k++)
			{
				UserDataController.ClearFightingStatisticsRewardGotRecord(FightingStatisticsTag.tTotalStrongholdModeVictory, k);
			}
			UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tTotalStrongholdModeVictory, 0);
			GOGPlayerPrefabs.SetInt("GOGFLGS_V_2_0_1", 1);
		}
		if (GOGPlayerPrefabs.GetInt("GOGFLGS_V_2_0_4", 0) == 0)
		{
			string[] allWeaponNameList = GrowthManagerKit.GetAllWeaponNameList();
			GWeaponItemInfo[] array = new GWeaponItemInfo[allWeaponNameList.Length];
			for (int l = 0; l < allWeaponNameList.Length; l++)
			{
				array[l] = GrowthManagerKit.GetWeaponItemInfoByName(allWeaponNameList[l]);
				if (array[l].mIsPlused)
				{
					int timeFillPrice = array[l].GetTimeFillPrice(1, GWeaponRechargeType.WeaponPlusTime);
					int num = (int)(array[l].mWeaponPlusTimeRest / 3600f);
					UserDataController.AddGems(timeFillPrice * num);
					UserDataController.SetWeaponPlusTimeRest(array[l].mName, 0f);
				}
			}
			GOGPlayerPrefabs.SetInt("GOGFLGS_V_2_0_4", 1);
		}
		if (GOGPlayerPrefabs.GetInt("GOGFLGS_V_2_1_1", 0) == 0)
		{
			int num2 = UserDataController.GetCurWeaponEquipLimitedNum() - 4;
			if (num2 > 0)
			{
				UserDataController.AddGems(num2 * 100);
			}
			UserDataController.SetCurWeaponEquipLimitedNum(6);
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_1", "BallisticKnife");
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_2", "GLOCK21");
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_3", "MP5KA5");
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_4", string.Empty);
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_5", string.Empty);
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_6", string.Empty);
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_7", string.Empty);
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_8", string.Empty);
			GOGPlayerPrefabs.SetInt("GOGFLGS_V_2_1_1", 1);
		}
		if (PlayerPrefs.GetInt("FLAF_V_" + GameVersionController.GameVersion, 0) == 0)
		{
			AndroidFileTransfer.UnZipFileOnAndroid();
			PlayerPrefs.SetInt("FLAF_V_" + GameVersionController.GameVersion, 1);
		}
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x0005DF9A File Offset: 0x0005C39A
	public static string GetSeasonMark()
	{
		return GOGPlayerPrefabs.GetString("SeasonMark", string.Empty);
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x0005DFAB File Offset: 0x0005C3AB
	public static void SetSeasonMark(string newMark)
	{
		GOGPlayerPrefabs.SetString("SeasonMark", newMark);
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x0005DFB8 File Offset: 0x0005C3B8
	public static void SetRoleName(string name)
	{
		GOGPlayerPrefabs.SetString("GOGRoleName", name);
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x0005DFC5 File Offset: 0x0005C3C5
	public static void VerifyAllOldVersionSkinsUploaded()
	{
		GOGPlayerPrefabs.SetInt("UploadedOldCustomSkins_V_1_0_4", 1);
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x0005DFD4 File Offset: 0x0005C3D4
	public static bool NeedUploadAllOldSkins()
	{
		return GOGPlayerPrefabs.GetInt("UploadedOldCustomSkins_V_1_0_4", 0).Equals(0);
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x0005DFF8 File Offset: 0x0005C3F8
	public static bool HasVersionTips(string version)
	{
		return GOGPlayerPrefabs.GetInt("Tips_V_" + version, 0).Equals(0);
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x0005E020 File Offset: 0x0005C420
	public static void VerifyVersionTips(string version)
	{
		if (version != null)
		{
			if (version == "1_0_2")
			{
				bool flag = GOGPlayerPrefabs.GetInt("Tips_V_1_0_2", 0).Equals(0);
				if (flag)
				{
					GOGPlayerPrefabs.SetInt("Tips_V_1_0_2", 1);
					UserDataController.AddCoins(488);
					UserDataController.SetCurGiftBoxTotal(UserDataController.GetCurGiftBoxTotal() + 3);
				}
				return;
			}
			if (version == "1_1_0")
			{
				bool flag = GOGPlayerPrefabs.GetInt("Tips_V_1_1_0", 0).Equals(0);
				if (flag)
				{
					GOGPlayerPrefabs.SetInt("Tips_V_1_1_0", 1);
					UserDataController.AddGems(30);
				}
				return;
			}
		}
		GOGPlayerPrefabs.SetInt("Tips_V_" + version, 1);
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x0005E0E3 File Offset: 0x0005C4E3
	public static int GetHolidayCurrency()
	{
		return GOGPlayerPrefabs.GetInt("CSocks2014", 0);
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x0005E0F0 File Offset: 0x0005C4F0
	public static void SetHolidayCurrency(int num)
	{
		GOGPlayerPrefabs.SetInt("CSocks2014", num);
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x0005E0FD File Offset: 0x0005C4FD
	public static void AddHolidayCurrency(int addNum)
	{
		GOGPlayerPrefabs.SetInt("CSocks2014", UserDataController.GetHolidayCurrency() + addNum);
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x0005E110 File Offset: 0x0005C510
	public static void SubHolidayCurrency(int subNum)
	{
		GOGPlayerPrefabs.SetInt("CSocks2014", UserDataController.GetHolidayCurrency() - subNum);
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x0005E123 File Offset: 0x0005C523
	public static void AddHolidayRechargeRecord(int gemsRechargeNum)
	{
		GOGPlayerPrefabs.SetInt("CRecharge2015", UserDataController.GetHolidayRechargeRecord() + gemsRechargeNum);
	}

	// Token: 0x06000C81 RID: 3201 RVA: 0x0005E136 File Offset: 0x0005C536
	public static int GetHolidayRechargeRecord()
	{
		return GOGPlayerPrefabs.GetInt("CRecharge2015", 0);
	}

	// Token: 0x06000C82 RID: 3202 RVA: 0x0005E143 File Offset: 0x0005C543
	public static void VerifyHolidayRechargeRewardGet(int rewardLv)
	{
		GOGPlayerPrefabs.SetInt("CRecharge2015_Get_" + rewardLv.ToString(), 1);
	}

	// Token: 0x06000C83 RID: 3203 RVA: 0x0005E164 File Offset: 0x0005C564
	public static bool HasEverGotHolidayRechargeReward(int rewardLv)
	{
		return GOGPlayerPrefabs.GetInt("CRecharge2015_Get_" + rewardLv.ToString(), 0).Equals(1);
	}

	// Token: 0x06000C84 RID: 3204 RVA: 0x0005E197 File Offset: 0x0005C597
	public static bool CanGetHolidayRechargeReward(int rewardLv)
	{
		return (rewardLv >= 1 || rewardLv <= UserDataController.HolidayRechargeRewardTarget.Length) && UserDataController.GetHolidayRechargeRecord() >= UserDataController.HolidayRechargeRewardTarget[rewardLv - 1];
	}

	// Token: 0x06000C85 RID: 3205 RVA: 0x0005E1C9 File Offset: 0x0005C5C9
	public static int GetHuntingTickets()
	{
		return GOGPlayerPrefabs.GetInt("hTicket", 3);
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x0005E1D6 File Offset: 0x0005C5D6
	public static void SetHuntingTickets(int newNum)
	{
		GOGPlayerPrefabs.SetInt("hTicket", newNum);
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0005E1E3 File Offset: 0x0005C5E3
	public static void AddHuntingTickets(int addNum)
	{
		GOGPlayerPrefabs.SetInt("hTicket", UserDataController.GetHuntingTickets() + addNum);
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0005E1F6 File Offset: 0x0005C5F6
	public static void SubHuntingTickets(int subNum)
	{
		if (subNum < 0)
		{
			return;
		}
		GOGPlayerPrefabs.SetInt("hTicket", Math.Max(0, UserDataController.GetHuntingTickets() - subNum));
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x0005E217 File Offset: 0x0005C617
	public static int GetMultiplayerBuffNum(string name)
	{
		return GOGPlayerPrefabs.GetInt("BuffPotionNum_" + name, 0);
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x0005E22A File Offset: 0x0005C62A
	public static void SetMultiplayerBuffNum(string name, int newNum)
	{
		GOGPlayerPrefabs.SetInt("BuffPotionNum_" + name, newNum);
	}

	// Token: 0x06000C8B RID: 3211 RVA: 0x0005E240 File Offset: 0x0005C640
	public static GMultiplayerBuffItemInfo GetMultiplayerBuffItemInfo(string name, GItemId id)
	{
		GMultiplayerBuffItemInfo gmultiplayerBuffItemInfo = new GMultiplayerBuffItemInfo();
		gmultiplayerBuffItemInfo.mName = name;
		gmultiplayerBuffItemInfo.mId = id;
		gmultiplayerBuffItemInfo.mExistNum = UserDataController.GetMultiplayerBuffNum(name);
		if (name != null)
		{
			if (name == "SpeedPlusBuff")
			{
				gmultiplayerBuffItemInfo.mType = BuffTypeInMultiplayer.SpeedPlusBuff;
				gmultiplayerBuffItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				gmultiplayerBuffItemInfo.mMaxEffectTime_S = 300f;
				gmultiplayerBuffItemInfo.mBaseMaxEffectTime_S = gmultiplayerBuffItemInfo.mMaxEffectTime_S;
				gmultiplayerBuffItemInfo.mUnlockCLevel = 1;
				gmultiplayerBuffItemInfo.mBindingNum = 3;
				gmultiplayerBuffItemInfo.mOffRate = 100;
				gmultiplayerBuffItemInfo.mOffRateDescription = string.Empty;
				gmultiplayerBuffItemInfo.mBindingPrice = 5 * gmultiplayerBuffItemInfo.mOffRate / 100;
				gmultiplayerBuffItemInfo.mNameDisplay = "Speed Plus Potion";
				gmultiplayerBuffItemInfo.mLogoSpriteName = "buff_logo_1_SpeedPlusBuff";
				gmultiplayerBuffItemInfo.mDescription = "Increase move speed by 20% for 5 minutes.";
				gmultiplayerBuffItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewPotionEnchantmentDetails(EnchantmentType.SpeedPlus, true, 0.2f, 1f, 0f, 0f, gmultiplayerBuffItemInfo.mMaxEffectTime_S));
				goto IL_50C;
			}
			if (name == "HpPlusBuff")
			{
				gmultiplayerBuffItemInfo.mType = BuffTypeInMultiplayer.HpPlusBuff;
				gmultiplayerBuffItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				gmultiplayerBuffItemInfo.mMaxEffectTime_S = 300f;
				gmultiplayerBuffItemInfo.mBaseMaxEffectTime_S = gmultiplayerBuffItemInfo.mMaxEffectTime_S;
				gmultiplayerBuffItemInfo.mUnlockCLevel = 1;
				gmultiplayerBuffItemInfo.mBindingNum = 3;
				gmultiplayerBuffItemInfo.mOffRate = 100;
				gmultiplayerBuffItemInfo.mOffRateDescription = string.Empty;
				gmultiplayerBuffItemInfo.mBindingPrice = 8 * gmultiplayerBuffItemInfo.mOffRate / 100;
				gmultiplayerBuffItemInfo.mNameDisplay = "Hp Plus Potion";
				gmultiplayerBuffItemInfo.mLogoSpriteName = "buff_logo_2_HpPlusBuff";
				gmultiplayerBuffItemInfo.mDescription = "Increase hp value by 50% for 5 minutes.";
				gmultiplayerBuffItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewPotionEnchantmentDetails(EnchantmentType.HpPlus, true, 0.5f, 1f, 0f, 0f, gmultiplayerBuffItemInfo.mMaxEffectTime_S));
				goto IL_50C;
			}
			if (name == "CoinsX2Buff")
			{
				gmultiplayerBuffItemInfo.mType = BuffTypeInMultiplayer.CoinsX2Buff;
				gmultiplayerBuffItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				gmultiplayerBuffItemInfo.mMaxEffectTime_S = 600f;
				gmultiplayerBuffItemInfo.mBaseMaxEffectTime_S = gmultiplayerBuffItemInfo.mMaxEffectTime_S;
				gmultiplayerBuffItemInfo.mUnlockCLevel = 1;
				gmultiplayerBuffItemInfo.mBindingNum = 3;
				gmultiplayerBuffItemInfo.mOffRate = 100;
				gmultiplayerBuffItemInfo.mOffRateDescription = string.Empty;
				gmultiplayerBuffItemInfo.mBindingPrice = 10 * gmultiplayerBuffItemInfo.mOffRate / 100;
				gmultiplayerBuffItemInfo.mNameDisplay = "Coins Plus Potion";
				gmultiplayerBuffItemInfo.mLogoSpriteName = "buff_logo_3_CoinsX2Buff";
				gmultiplayerBuffItemInfo.mDescription = "xxx";
				gmultiplayerBuffItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewPotionEnchantmentDetails(EnchantmentType.KillingCoinDouble, true, 1f, 1f, 0f, 0f, gmultiplayerBuffItemInfo.mMaxEffectTime_S));
				goto IL_50C;
			}
			if (name == "ExpX2Buff")
			{
				gmultiplayerBuffItemInfo.mType = BuffTypeInMultiplayer.ExpX2Buff;
				gmultiplayerBuffItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				gmultiplayerBuffItemInfo.mMaxEffectTime_S = 600f;
				gmultiplayerBuffItemInfo.mBaseMaxEffectTime_S = gmultiplayerBuffItemInfo.mMaxEffectTime_S;
				gmultiplayerBuffItemInfo.mUnlockCLevel = 1;
				gmultiplayerBuffItemInfo.mBindingNum = 3;
				gmultiplayerBuffItemInfo.mOffRate = 100;
				gmultiplayerBuffItemInfo.mOffRateDescription = string.Empty;
				gmultiplayerBuffItemInfo.mBindingPrice = 10 * gmultiplayerBuffItemInfo.mOffRate / 100;
				gmultiplayerBuffItemInfo.mNameDisplay = "Exp Plus Potion";
				gmultiplayerBuffItemInfo.mLogoSpriteName = "buff_logo_4_ExpX2Buff";
				gmultiplayerBuffItemInfo.mDescription = "Increase exp gained by 100% for 10 minutes.";
				gmultiplayerBuffItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewPotionEnchantmentDetails(EnchantmentType.KillingExpDouble, true, 1f, 1f, 0f, 0f, gmultiplayerBuffItemInfo.mMaxEffectTime_S));
				goto IL_50C;
			}
			if (name == "DamagePlusBuff")
			{
				gmultiplayerBuffItemInfo.mType = BuffTypeInMultiplayer.DamagePlusBuff;
				gmultiplayerBuffItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				gmultiplayerBuffItemInfo.mMaxEffectTime_S = 300f;
				gmultiplayerBuffItemInfo.mBaseMaxEffectTime_S = gmultiplayerBuffItemInfo.mMaxEffectTime_S;
				gmultiplayerBuffItemInfo.mUnlockCLevel = 1;
				gmultiplayerBuffItemInfo.mBindingNum = 3;
				gmultiplayerBuffItemInfo.mOffRate = 100;
				gmultiplayerBuffItemInfo.mOffRateDescription = string.Empty;
				gmultiplayerBuffItemInfo.mBindingPrice = 10 * gmultiplayerBuffItemInfo.mOffRate / 100;
				gmultiplayerBuffItemInfo.mNameDisplay = "Damage Plus Potion";
				gmultiplayerBuffItemInfo.mLogoSpriteName = "buff_logo_5_DamagePlusBuff";
				gmultiplayerBuffItemInfo.mDescription = "Increase the damage of all your weapons by 20% for 5 minutes.";
				gmultiplayerBuffItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewPotionEnchantmentDetails(EnchantmentType.DamagePlus, true, 0.2f, 1f, 0f, 0f, gmultiplayerBuffItemInfo.mMaxEffectTime_S));
				goto IL_50C;
			}
			if (name == "HpRecoveryBuff")
			{
				gmultiplayerBuffItemInfo.mType = BuffTypeInMultiplayer.HpRecoveryBuff;
				gmultiplayerBuffItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				gmultiplayerBuffItemInfo.mMaxEffectTime_S = 300f;
				gmultiplayerBuffItemInfo.mBaseMaxEffectTime_S = gmultiplayerBuffItemInfo.mMaxEffectTime_S;
				gmultiplayerBuffItemInfo.mUnlockCLevel = 1;
				gmultiplayerBuffItemInfo.mBindingNum = 3;
				gmultiplayerBuffItemInfo.mOffRate = 100;
				gmultiplayerBuffItemInfo.mOffRateDescription = string.Empty;
				gmultiplayerBuffItemInfo.mBindingPrice = 10 * gmultiplayerBuffItemInfo.mOffRate / 100;
				gmultiplayerBuffItemInfo.mNameDisplay = "Regeneration Potion";
				gmultiplayerBuffItemInfo.mLogoSpriteName = "buff_logo_6_HpRecoveryBuff";
				gmultiplayerBuffItemInfo.mDescription = "Give you 10% hp recovery per 5 seconds for 5 minutes.";
				gmultiplayerBuffItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewPotionEnchantmentDetails(EnchantmentType.HpRecovery, true, 0.1f, 1f, 5f, 0f, gmultiplayerBuffItemInfo.mMaxEffectTime_S));
				goto IL_50C;
			}
		}
		gmultiplayerBuffItemInfo.mType = BuffTypeInMultiplayer.SpeedPlusBuff;
		gmultiplayerBuffItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
		gmultiplayerBuffItemInfo.mMaxEffectTime_S = 300f;
		gmultiplayerBuffItemInfo.mBaseMaxEffectTime_S = gmultiplayerBuffItemInfo.mMaxEffectTime_S;
		gmultiplayerBuffItemInfo.mUnlockCLevel = 1;
		gmultiplayerBuffItemInfo.mBindingNum = 3;
		gmultiplayerBuffItemInfo.mOffRate = 100;
		gmultiplayerBuffItemInfo.mOffRateDescription = string.Empty;
		gmultiplayerBuffItemInfo.mBindingPrice = 1000 * gmultiplayerBuffItemInfo.mOffRate / 100;
		gmultiplayerBuffItemInfo.mNameDisplay = "Error Potion";
		gmultiplayerBuffItemInfo.mLogoSpriteName = "buff_logo_1_SpeedPlusBuff";
		gmultiplayerBuffItemInfo.mDescription = "xxx";
		IL_50C:
		gmultiplayerBuffItemInfo.mDescription = gmultiplayerBuffItemInfo.mDescription.ToUpper();
		gmultiplayerBuffItemInfo.mNameDisplay = gmultiplayerBuffItemInfo.mNameDisplay.ToUpper();
		return gmultiplayerBuffItemInfo;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x0005E77C File Offset: 0x0005CB7C
	public static string GetCurSettedHatName()
	{
		return GOGPlayerPrefabs.GetString("CurSettedHatName", "Hat_Null");
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x0005E78D File Offset: 0x0005CB8D
	public static void SetCurSettedHat(string name)
	{
		GOGPlayerPrefabs.SetString("CurSettedHatName", name);
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x0005E79C File Offset: 0x0005CB9C
	public static string[] GetUserAllEnabledHatName()
	{
		List<string> list = new List<string>();
		GItemId id = new GItemId(1, 1, 1, 1);
		for (int i = 0; i < UserDataController.AllHatNameList.Length; i++)
		{
			if (UserDataController.GetHatItemInfo(UserDataController.AllHatNameList[i], id).mIsEnabled)
			{
				list.Add(UserDataController.AllHatNameList[i]);
			}
		}
		string[] array = new string[list.Count];
		list.CopyTo(array);
		list.Clear();
		return array;
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x0005E810 File Offset: 0x0005CC10
	public static GHatItemInfo GetHatItemInfo(string name, GItemId id)
	{
		GHatItemInfo ghatItemInfo = new GHatItemInfo();
		ghatItemInfo.mName = name;
		ghatItemInfo.mId = id;
		if (GOGPlayerPrefabs.GetInt("HatEnabled_" + name.ToString(), 0) == 0)
		{
			ghatItemInfo.mIsEnabled = false;
		}
		else
		{
			ghatItemInfo.mIsEnabled = true;
		}
		switch (name)
		{
		case "Hat_1":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 150;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Royal Crown";
			ghatItemInfo.mLogoSpriteName = "Hat_1";
			ghatItemInfo.mDescription = "It's the symbol of power.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamageReducation, true, 0.15f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.HeadshotProtectRate, true, 0.1f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Damage Reducation  [+ 15%]\nHeadshot Protect  [+ 10%]";
			goto IL_ABC;
		case "Hat_2":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 75;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Cowboy Hat";
			ghatItemInfo.mLogoSpriteName = "Hat_2";
			ghatItemInfo.mDescription = "It's very comfortable.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.AccuracyPlus, true, 0.3f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Accuracy  [+ 30%]";
			goto IL_ABC;
		case "Hat_3":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 75;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Skull Mask";
			ghatItemInfo.mLogoSpriteName = "Hat_3";
			ghatItemInfo.mDescription = "It comes from the dark side.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamagePlus, true, 0.15f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Damage  [+ 15%]";
			goto IL_ABC;
		case "Hat_4":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 60;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Magic Hat";
			ghatItemInfo.mLogoSpriteName = "Hat_4";
			ghatItemInfo.mDescription = "The magician's hat.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.ExplosionDamagePlus, true, 0.3f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Explosion Damage  [+ 30%]";
			goto IL_ABC;
		case "Hat_5":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 50;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Football Hero";
			ghatItemInfo.mLogoSpriteName = "Hat_5";
			ghatItemInfo.mDescription = "You will be the football hero.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.ExplosionDamageReducation, true, 0.4f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Explosion Damage Reducation  [+ 40%]";
			goto IL_ABC;
		case "Hat_6":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 75;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Halloween Hat";
			ghatItemInfo.mLogoSpriteName = "Hat_6";
			ghatItemInfo.mDescription = "Special hat for halloween.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamageReducation, true, 0.1f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.HpPlus, true, 0.1f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Damage Reducation  [+ 10%]\nHp  [+ 10%]";
			goto IL_ABC;
		case "Hat_7":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 60;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Evil Mask";
			ghatItemInfo.mLogoSpriteName = "Hat_7";
			ghatItemInfo.mDescription = "It's full of the forces of evil.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.AccuracyPlus, true, 0.1f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamagePlus, true, 0.05f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Accuracy  [+ 10%]\nDamage  [+ 5%]";
			goto IL_ABC;
		case "Hat_8":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 60;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Ghost Mask";
			ghatItemInfo.mLogoSpriteName = "Hat_8";
			ghatItemInfo.mDescription = "It's full of the forces of dark side.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.HeadshotProtectRate, true, 0.1f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.HpPlus, true, 0.05f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Headshot Protect  [+ 10%]\nHp  [+ 5%]";
			goto IL_ABC;
		case "Hat_9":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 80;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Santa Hat";
			ghatItemInfo.mLogoSpriteName = "Hat_9";
			ghatItemInfo.mDescription = "[Christmas Only]\nGift from Santa Claus.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamagePlus, true, 0.15f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.HeadshotProtectRate, true, 0.15f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Damage  [+ 15%]\nHeadshot Protect  [+ 15%]";
			goto IL_ABC;
		case "Hat_10":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 60;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Hero Mask";
			ghatItemInfo.mLogoSpriteName = "Hat_10";
			ghatItemInfo.mDescription = "It's full of the forces of dark side.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.ExplosionDamagePlus, true, 0.1f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamagePlus, true, 0.05f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Damage  [+ 5%]\nExplosion Damage  [+ 10%]";
			goto IL_ABC;
		case "Hat_11":
			ghatItemInfo.mSellable = false;
			ghatItemInfo.mPurchaseTipsText = "*Cannot Get*\n- Out Of Date-";
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 60;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Chirstmas Gift Hat";
			ghatItemInfo.mLogoSpriteName = "Hat_11";
			ghatItemInfo.mDescription = "Keep continuous login to get it!";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.HpPlus, true, 0.05f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Hp  [+ 5%]";
			goto IL_ABC;
		case "Hat_12":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 75;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Forest Glass";
			ghatItemInfo.mLogoSpriteName = "Hat_12";
			ghatItemInfo.mDescription = "Guardian of the forest.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.AccuracyPlus, true, 0.05f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamageReducation, true, 0.1f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Accuracy  [+ 5%]\nDamage Reducation  [+ 10%]";
			goto IL_ABC;
		case "Hat_13":
			ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			ghatItemInfo.mUnlockCLevel = 1;
			ghatItemInfo.mOffRate = 100;
			ghatItemInfo.mOffRateDescription = string.Empty;
			ghatItemInfo.mPrice = 90;
			ghatItemInfo.mPrice = ghatItemInfo.mPrice * ghatItemInfo.mOffRate / 100 / 5 * 5;
			ghatItemInfo.mNameDisplay = "Bat Mask";
			ghatItemInfo.mLogoSpriteName = "Hat_13";
			ghatItemInfo.mDescription = "Not afraid of evil.";
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.HeadshotProtectRate, true, 0.2f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamagePlus, true, 0.05f, 1f, 0f, 0f));
			ghatItemInfo.mEnchantmentDescription = "Headshot Protect  [+ 20%]\nDamage  [+ 5%]";
			goto IL_ABC;
		}
		ghatItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
		ghatItemInfo.mUnlockCLevel = 1;
		ghatItemInfo.mPrice = 150;
		ghatItemInfo.mNameDisplay = name;
		ghatItemInfo.mDescription = string.Empty;
		ghatItemInfo.mEnchantmentDescription = string.Empty;
		IL_ABC:
		ghatItemInfo.mDescription = ghatItemInfo.mDescription.ToUpper();
		ghatItemInfo.mNameDisplay = ghatItemInfo.mNameDisplay.ToUpper();
		ghatItemInfo.mEnchantmentDescription = ghatItemInfo.mEnchantmentDescription.ToUpper();
		ghatItemInfo.mPurchaseTipsText = ghatItemInfo.mPurchaseTipsText.ToUpper();
		ghatItemInfo.mFixPrice = ghatItemInfo.mPrice / 2;
		return ghatItemInfo;
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x0005F32C File Offset: 0x0005D72C
	public static bool EnableHat(string name)
	{
		GOGPlayerPrefabs.SetInt("HatEnabled_" + name.ToString(), 1);
		return true;
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x0005F345 File Offset: 0x0005D745
	public static string GetCurSettedCapeName()
	{
		return GOGPlayerPrefabs.GetString("CurSettedCapeName", "Cape_Null");
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x0005F356 File Offset: 0x0005D756
	public static void SetCurSettedCape(string name)
	{
		GOGPlayerPrefabs.SetString("CurSettedCapeName", name);
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x0005F364 File Offset: 0x0005D764
	public static string[] GetUserAllEnabledCapeName()
	{
		List<string> list = new List<string>();
		GItemId id = new GItemId(1, 1, 1, 1);
		for (int i = 0; i < UserDataController.AllCapeNameList.Length; i++)
		{
			if (UserDataController.GetCapeItemInfo(UserDataController.AllCapeNameList[i], id).mIsEnabled)
			{
				list.Add(UserDataController.AllCapeNameList[i]);
			}
		}
		string[] array = new string[list.Count];
		list.CopyTo(array);
		list.Clear();
		return array;
	}

	// Token: 0x06000C94 RID: 3220 RVA: 0x0005F3D8 File Offset: 0x0005D7D8
	public static GCapeItemInfo GetCapeItemInfo(string name, GItemId id)
	{
		GCapeItemInfo gcapeItemInfo = new GCapeItemInfo();
		gcapeItemInfo.mName = name;
		gcapeItemInfo.mId = id;
		if (GOGPlayerPrefabs.GetInt("CapeEnabled_" + name.ToString(), 0) == 0)
		{
			gcapeItemInfo.mIsEnabled = false;
		}
		else
		{
			gcapeItemInfo.mIsEnabled = true;
		}
		switch (name)
		{
		case "Cape_1":
			gcapeItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			gcapeItemInfo.mUnlockCLevel = 1;
			gcapeItemInfo.mOffRate = 100;
			gcapeItemInfo.mOffRateDescription = string.Empty;
			gcapeItemInfo.mPrice = 100;
			gcapeItemInfo.mPrice = gcapeItemInfo.mPrice * gcapeItemInfo.mOffRate / 100 / 5 * 5;
			gcapeItemInfo.mNameDisplay = "Lava Sword";
			gcapeItemInfo.mLogoSpriteName = "Cape_1";
			gcapeItemInfo.mDescription = "It's the symbol of power.";
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamagePlus, true, 0.1f, 1f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.HpRecovery, true, 0.05f, 1f, 5f, 0f));
			gcapeItemInfo.mEnchantmentDescription = "Damage  [+ 10%]\nHp Recovery  [+ 5% Per 5 Seconds]";
			goto IL_87C;
		case "Cape_2":
			gcapeItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			gcapeItemInfo.mUnlockCLevel = 1;
			gcapeItemInfo.mOffRate = 100;
			gcapeItemInfo.mOffRateDescription = string.Empty;
			gcapeItemInfo.mPrice = 60;
			gcapeItemInfo.mPrice = gcapeItemInfo.mPrice * gcapeItemInfo.mOffRate / 100 / 5 * 5;
			gcapeItemInfo.mNameDisplay = "Skull Cape";
			gcapeItemInfo.mLogoSpriteName = "Cape_2";
			gcapeItemInfo.mDescription = "It comes from the dark side.";
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.KillingInvisible, true, 1f, 0.1f, 0f, 5f));
			gcapeItemInfo.mEnchantmentDescription = "Invisible  [Killing Trigger: 10%]";
			goto IL_87C;
		case "Cape_3":
			gcapeItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			gcapeItemInfo.mUnlockCLevel = 1;
			gcapeItemInfo.mOffRate = 100;
			gcapeItemInfo.mOffRateDescription = string.Empty;
			gcapeItemInfo.mPrice = 50;
			gcapeItemInfo.mPrice = gcapeItemInfo.mPrice * gcapeItemInfo.mOffRate / 100 / 5 * 5;
			gcapeItemInfo.mNameDisplay = "Knight Soul";
			gcapeItemInfo.mLogoSpriteName = "Cape_3";
			gcapeItemInfo.mDescription = "It's the symbol of justice.";
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.KillingHpFullRecovery, true, 1f, 0.2f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDescription = "Full Hp Recovery  [Killing Trigger: 20%]";
			goto IL_87C;
		case "Cape_4":
			gcapeItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			gcapeItemInfo.mUnlockCLevel = 1;
			gcapeItemInfo.mOffRate = 100;
			gcapeItemInfo.mOffRateDescription = string.Empty;
			gcapeItemInfo.mPrice = 50;
			gcapeItemInfo.mPrice = gcapeItemInfo.mPrice * gcapeItemInfo.mOffRate / 100 / 5 * 5;
			gcapeItemInfo.mNameDisplay = "Craft Cape";
			gcapeItemInfo.mLogoSpriteName = "Cape_4";
			gcapeItemInfo.mDescription = "Create your own world.";
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.KillingExpDoubleTrigger, true, 1f, 0.2f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.KillingCoinDoubleTrigger, true, 1f, 0.2f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDescription = "Exp x 2  [Killing Trigger: 20%]\nCoins x 2  [Killing Trigger: 20%]";
			goto IL_87C;
		case "Cape_5":
			gcapeItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			gcapeItemInfo.mUnlockCLevel = 1;
			gcapeItemInfo.mOffRate = 100;
			gcapeItemInfo.mOffRateDescription = string.Empty;
			gcapeItemInfo.mPrice = 60;
			gcapeItemInfo.mPrice = gcapeItemInfo.mPrice * gcapeItemInfo.mOffRate / 100 / 5 * 5;
			gcapeItemInfo.mNameDisplay = "Monster Cape";
			gcapeItemInfo.mLogoSpriteName = "Cape_5";
			gcapeItemInfo.mDescription = "It comes from the spirit world.";
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.PotionTimePlus, true, 0.2f, 1f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDescription = "Potion Time  [+ 20%]";
			goto IL_87C;
		case "Cape_6":
			gcapeItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			gcapeItemInfo.mUnlockCLevel = 1;
			gcapeItemInfo.mOffRate = 100;
			gcapeItemInfo.mOffRateDescription = string.Empty;
			gcapeItemInfo.mPrice = 75;
			gcapeItemInfo.mPrice = gcapeItemInfo.mPrice * gcapeItemInfo.mOffRate / 100 / 5 * 5;
			gcapeItemInfo.mNameDisplay = "Halloween Cape";
			gcapeItemInfo.mLogoSpriteName = "Cape_6";
			gcapeItemInfo.mDescription = "Special cape for halloween.";
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamagePlus, true, 0.05f, 1f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamageReducation, true, 0.05f, 1f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDescription = "Damage  [+ 5%]\nDamage Reducation  [+ 5%]";
			goto IL_87C;
		case "Cape_7":
			gcapeItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			gcapeItemInfo.mUnlockCLevel = 1;
			gcapeItemInfo.mOffRate = 100;
			gcapeItemInfo.mOffRateDescription = string.Empty;
			gcapeItemInfo.mPrice = 70;
			gcapeItemInfo.mPrice = gcapeItemInfo.mPrice * gcapeItemInfo.mOffRate / 100 / 5 * 5;
			gcapeItemInfo.mNameDisplay = "Santa Cape";
			gcapeItemInfo.mLogoSpriteName = "Cape_7";
			gcapeItemInfo.mDescription = "[Christmas Only]\nGift from Santa Claus.";
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamagePlus, true, 0.1f, 1f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamageReducation, true, 0.15f, 1f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDescription = "Damage  [+ 10%]\nDamage Reducation  [+ 15%]";
			goto IL_87C;
		case "Cape_8":
			gcapeItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			gcapeItemInfo.mUnlockCLevel = 1;
			gcapeItemInfo.mOffRate = 100;
			gcapeItemInfo.mOffRateDescription = string.Empty;
			gcapeItemInfo.mPrice = 60;
			gcapeItemInfo.mPrice = gcapeItemInfo.mPrice * gcapeItemInfo.mOffRate / 100 / 5 * 5;
			gcapeItemInfo.mNameDisplay = "Skyline";
			gcapeItemInfo.mLogoSpriteName = "Cape_8";
			gcapeItemInfo.mDescription = "Quiet & Focus";
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.AccuracyPlus, true, 0.1f, 1f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamageReducation, true, 0.1f, 1f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDescription = "Accuracy  [+ 10%]\nDamage Reducation  [+ 10%]";
			goto IL_87C;
		case "Cape_9":
			gcapeItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			gcapeItemInfo.mUnlockCLevel = 1;
			gcapeItemInfo.mOffRate = 100;
			gcapeItemInfo.mOffRateDescription = string.Empty;
			gcapeItemInfo.mPrice = 60;
			gcapeItemInfo.mPrice = gcapeItemInfo.mPrice * gcapeItemInfo.mOffRate / 100 / 5 * 5;
			gcapeItemInfo.mNameDisplay = "Forest Cape";
			gcapeItemInfo.mLogoSpriteName = "Cape_9";
			gcapeItemInfo.mDescription = "Feel like a mushroom on the back.";
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.HpRecovery, true, 0.05f, 1f, 5f, 0f));
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamageReducation, true, 0.1f, 1f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDescription = "Hp Recovery  [+ 5% Per 5 Seconds]\nDamage Reducation  [+ 10%]";
			goto IL_87C;
		case "Cape_10":
			gcapeItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
			gcapeItemInfo.mUnlockCLevel = 1;
			gcapeItemInfo.mOffRate = 100;
			gcapeItemInfo.mOffRateDescription = string.Empty;
			gcapeItemInfo.mPrice = 80;
			gcapeItemInfo.mPrice = gcapeItemInfo.mPrice * gcapeItemInfo.mOffRate / 100 / 5 * 5;
			gcapeItemInfo.mNameDisplay = "Bat Cape";
			gcapeItemInfo.mLogoSpriteName = "Cape_10";
			gcapeItemInfo.mDescription = "Experience flying in the sky.";
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.AccuracyPlus, true, 0.1f, 1f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.JumpPlus, true, 0.05f, 1f, 0f, 0f));
			gcapeItemInfo.mEnchantmentDescription = "Accuracy  [+ 10%]\nJump  [+ 5%]";
			goto IL_87C;
		}
		gcapeItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
		gcapeItemInfo.mUnlockCLevel = 1;
		gcapeItemInfo.mPrice = 150;
		gcapeItemInfo.mNameDisplay = name;
		gcapeItemInfo.mDescription = string.Empty;
		gcapeItemInfo.mEnchantmentDescription = string.Empty;
		IL_87C:
		gcapeItemInfo.mDescription = gcapeItemInfo.mDescription.ToUpper();
		gcapeItemInfo.mNameDisplay = gcapeItemInfo.mNameDisplay.ToUpper();
		gcapeItemInfo.mEnchantmentDescription = gcapeItemInfo.mEnchantmentDescription.ToUpper();
		gcapeItemInfo.mPurchaseTipsText = gcapeItemInfo.mPurchaseTipsText.ToUpper();
		gcapeItemInfo.mFixPrice = gcapeItemInfo.mPrice / 2;
		return gcapeItemInfo;
	}

	// Token: 0x06000C95 RID: 3221 RVA: 0x0005FCB4 File Offset: 0x0005E0B4
	public static bool EnableCape(string name)
	{
		GOGPlayerPrefabs.SetInt("CapeEnabled_" + name.ToString(), 1);
		return true;
	}

	// Token: 0x06000C96 RID: 3222 RVA: 0x0005FCCD File Offset: 0x0005E0CD
	public static string GetCurSettedBootName()
	{
		return GOGPlayerPrefabs.GetString("CurSettedBootName", "Boot_Null");
	}

	// Token: 0x06000C97 RID: 3223 RVA: 0x0005FCDE File Offset: 0x0005E0DE
	public static void SetCurSettedBoot(string name)
	{
		GOGPlayerPrefabs.SetString("CurSettedBootName", name);
	}

	// Token: 0x06000C98 RID: 3224 RVA: 0x0005FCEC File Offset: 0x0005E0EC
	public static string[] GetUserAllEnabledBootName()
	{
		List<string> list = new List<string>();
		GItemId id = new GItemId(1, 1, 1, 1);
		for (int i = 0; i < UserDataController.AllBootNameList.Length; i++)
		{
			if (UserDataController.GetBootItemInfo(UserDataController.AllBootNameList[i], id).mIsEnabled)
			{
				list.Add(UserDataController.AllBootNameList[i]);
			}
		}
		string[] array = new string[list.Count];
		list.CopyTo(array);
		list.Clear();
		return array;
	}

	// Token: 0x06000C99 RID: 3225 RVA: 0x0005FD60 File Offset: 0x0005E160
	public static GBootItemInfo GetBootItemInfo(string name, GItemId id)
	{
		GBootItemInfo gbootItemInfo = new GBootItemInfo();
		gbootItemInfo.mName = name;
		gbootItemInfo.mId = id;
		if (GOGPlayerPrefabs.GetInt("BootEnabled_" + name.ToString(), 0) == 0)
		{
			gbootItemInfo.mIsEnabled = false;
		}
		else
		{
			gbootItemInfo.mIsEnabled = true;
		}
		if (name != null)
		{
			if (name == "Boot_1")
			{
				gbootItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				gbootItemInfo.mUnlockCLevel = 1;
				gbootItemInfo.mOffRate = 100;
				gbootItemInfo.mOffRateDescription = string.Empty;
				gbootItemInfo.mPrice = 100;
				gbootItemInfo.mPrice = gbootItemInfo.mPrice * gbootItemInfo.mOffRate / 100 / 5 * 5;
				gbootItemInfo.mNameDisplay = "Royal Boot";
				gbootItemInfo.mLogoSpriteName = "Boot_1";
				gbootItemInfo.mDescription = "It's the symbol of power.";
				gbootItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.JumpPlus, true, 0.1f, 1f, 0f, 0f));
				gbootItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.SpeedPlus, true, 0.1f, 1f, 0f, 0f));
				gbootItemInfo.mEnchantmentDescription = "Jump  [+ 10%]\nSpeed  [+ 10%]";
				goto IL_454;
			}
			if (name == "Boot_2")
			{
				gbootItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				gbootItemInfo.mUnlockCLevel = 1;
				gbootItemInfo.mOffRate = 100;
				gbootItemInfo.mOffRateDescription = string.Empty;
				gbootItemInfo.mPrice = 75;
				gbootItemInfo.mPrice = gbootItemInfo.mPrice * gbootItemInfo.mOffRate / 100 / 5 * 5;
				gbootItemInfo.mNameDisplay = "Skull Boot";
				gbootItemInfo.mLogoSpriteName = "Boot_2";
				gbootItemInfo.mDescription = "It comes from the dark side.";
				gbootItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.SpeedPlus, true, 0.1f, 1f, 0f, 0f));
				gbootItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamagePlus, true, 0.05f, 1f, 0f, 0f));
				gbootItemInfo.mEnchantmentDescription = "Speed  [+ 10%]\nDamage  [+ 5%]";
				goto IL_454;
			}
			if (name == "Boot_3")
			{
				gbootItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				gbootItemInfo.mUnlockCLevel = 1;
				gbootItemInfo.mOffRate = 100;
				gbootItemInfo.mOffRateDescription = string.Empty;
				gbootItemInfo.mPrice = 90;
				gbootItemInfo.mPrice = gbootItemInfo.mPrice * gbootItemInfo.mOffRate / 100 / 5 * 5;
				gbootItemInfo.mNameDisplay = "Santa Boot";
				gbootItemInfo.mLogoSpriteName = "Boot_3";
				gbootItemInfo.mDescription = "[Christmas Only]\nGift from Santa Claus.";
				gbootItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.JumpPlus, true, 0.25f, 1f, 0f, 0f));
				gbootItemInfo.mEnchantmentDescription = "Jump  [+ 25%]";
				goto IL_454;
			}
			if (name == "Boot_4")
			{
				gbootItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				gbootItemInfo.mUnlockCLevel = 1;
				gbootItemInfo.mOffRate = 100;
				gbootItemInfo.mOffRateDescription = string.Empty;
				gbootItemInfo.mPrice = 80;
				gbootItemInfo.mPrice = gbootItemInfo.mPrice * gbootItemInfo.mOffRate / 100 / 5 * 5;
				gbootItemInfo.mNameDisplay = "Forest Boot";
				gbootItemInfo.mLogoSpriteName = "Boot_4";
				gbootItemInfo.mDescription = "Comfortable and toughness.";
				gbootItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.SpeedPlus, true, 0.06f, 1f, 0f, 0f));
				gbootItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.DamageReducation, true, 0.08f, 1f, 0f, 0f));
				gbootItemInfo.mEnchantmentDescription = "Speed  [+ 6%]\nDamage Reducation [+ 8%]";
				goto IL_454;
			}
			if (name == "Boot_5")
			{
				gbootItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				gbootItemInfo.mUnlockCLevel = 1;
				gbootItemInfo.mOffRate = 100;
				gbootItemInfo.mOffRateDescription = string.Empty;
				gbootItemInfo.mPrice = 120;
				gbootItemInfo.mPrice = gbootItemInfo.mPrice * gbootItemInfo.mOffRate / 100 / 5 * 5;
				gbootItemInfo.mNameDisplay = "Bat Boot";
				gbootItemInfo.mLogoSpriteName = "Boot_5";
				gbootItemInfo.mDescription = "Fast speed like wind.";
				gbootItemInfo.mEnchantmentDetails.Add(EnchantmentDetails.NewEquipPropsEnchantmentDetails(EnchantmentType.SpeedPlus, true, 0.15f, 1f, 0f, 0f));
				gbootItemInfo.mEnchantmentDescription = "Speed  [+ 15%]";
				goto IL_454;
			}
		}
		gbootItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
		gbootItemInfo.mUnlockCLevel = 1;
		gbootItemInfo.mPrice = 150;
		gbootItemInfo.mNameDisplay = name;
		gbootItemInfo.mDescription = string.Empty;
		gbootItemInfo.mEnchantmentDescription = string.Empty;
		IL_454:
		gbootItemInfo.mDescription = gbootItemInfo.mDescription.ToUpper();
		gbootItemInfo.mNameDisplay = gbootItemInfo.mNameDisplay.ToUpper();
		gbootItemInfo.mEnchantmentDescription = gbootItemInfo.mEnchantmentDescription.ToUpper();
		gbootItemInfo.mPurchaseTipsText = gbootItemInfo.mPurchaseTipsText.ToUpper();
		gbootItemInfo.mFixPrice = gbootItemInfo.mPrice / 2;
		return gbootItemInfo;
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x00060214 File Offset: 0x0005E614
	public static bool EnableBoot(string name)
	{
		GOGPlayerPrefabs.SetInt("BootEnabled_" + name.ToString(), 1);
		return true;
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x00060230 File Offset: 0x0005E630
	public static float GetCurAutoGiftTimeInterval()
	{
		float result;
		switch (GOGPlayerPrefabs.GetInt("AutoGiftGetCount", 0))
		{
		case 0:
			result = 300f;
			break;
		case 1:
			result = 900f;
			break;
		case 2:
			result = 1800f;
			break;
		case 3:
			result = 3600f;
			break;
		case 4:
			result = 5400f;
			break;
		default:
			result = 7200f;
			break;
		}
		return result;
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x000602B1 File Offset: 0x0005E6B1
	public static float GetCurAutoGiftTimeRest()
	{
		return GOGPlayerPrefabs.GetFloat("CurAutoGiftTimeRest", 300f);
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x000602C2 File Offset: 0x0005E6C2
	public static void SetCurAutoGiftTimeRest(float tValue)
	{
		GOGPlayerPrefabs.SetFloat("CurAutoGiftTimeRest", tValue);
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x000602D0 File Offset: 0x0005E6D0
	public static void CutCurAutoGiftTimeRest(float tValue)
	{
		float num = GOGPlayerPrefabs.GetFloat("CurAutoGiftTimeRest", 300f) - tValue;
		if (num <= 0f)
		{
			num = 0f;
		}
		GOGPlayerPrefabs.SetFloat("CurAutoGiftTimeRest", num);
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x0006030B File Offset: 0x0005E70B
	public static bool CanGetAutoGift()
	{
		return UserDataController.GetCurAutoGiftTimeRest() <= 0f;
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x00060320 File Offset: 0x0005E720
	public static int GetRamdonCoinsForAutoGift()
	{
		int num = UnityEngine.Random.Range(1, 101);
		int num2;
		if (num <= 100 && num > 99)
		{
			num2 = UnityEngine.Random.Range(20, 31);
		}
		else if (num <= 99 && num > 97)
		{
			num2 = UnityEngine.Random.Range(10, 21);
		}
		else if (num <= 97 && num > 80)
		{
			num2 = UnityEngine.Random.Range(5, 11);
		}
		else
		{
			num2 = UnityEngine.Random.Range(1, 6);
		}
		return num2 * 10;
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x000603A0 File Offset: 0x0005E7A0
	public static AutoGiftInfo RecevieOneGift()
	{
		AutoGiftInfo autoGiftInfo = new AutoGiftInfo();
		switch (GOGPlayerPrefabs.GetInt("AutoGiftGetCount", 0))
		{
		case 0:
			autoGiftInfo.gType = AutoGiftType.GiftBox;
			autoGiftInfo.num = 1;
			autoGiftInfo.spriteName = "GiftBoxBig";
			break;
		case 1:
		{
			int num = UnityEngine.Random.Range(1, 101);
			if (num > 50)
			{
				autoGiftInfo.gType = AutoGiftType.GiftBox;
				autoGiftInfo.num = 1;
				autoGiftInfo.spriteName = "GiftBoxBig";
			}
			else
			{
				autoGiftInfo.gType = AutoGiftType.Coins;
				autoGiftInfo.num = UserDataController.GetRamdonCoinsForAutoGift();
				autoGiftInfo.spriteName = "GiftCoinBig";
			}
			break;
		}
		case 2:
		{
			int num = UnityEngine.Random.Range(1, 101);
			if (num > 50)
			{
				autoGiftInfo.gType = AutoGiftType.GiftBox;
				autoGiftInfo.num = 1;
				autoGiftInfo.spriteName = "GiftBoxBig";
			}
			else
			{
				autoGiftInfo.gType = AutoGiftType.Coins;
				autoGiftInfo.num = UserDataController.GetRamdonCoinsForAutoGift();
				autoGiftInfo.spriteName = "GiftCoinBig";
			}
			break;
		}
		case 3:
		{
			int num = UnityEngine.Random.Range(1, 101);
			if (num > 70)
			{
				autoGiftInfo.gType = AutoGiftType.GiftBox;
				autoGiftInfo.num = 1;
				autoGiftInfo.spriteName = "GiftBoxBig";
			}
			else
			{
				autoGiftInfo.gType = AutoGiftType.Coins;
				autoGiftInfo.num = UserDataController.GetRamdonCoinsForAutoGift();
				autoGiftInfo.spriteName = "GiftCoinBig";
			}
			break;
		}
		case 4:
		{
			int num = UnityEngine.Random.Range(1, 101);
			if (num > 80)
			{
				autoGiftInfo.gType = AutoGiftType.GiftBox;
				autoGiftInfo.num = 1;
				autoGiftInfo.spriteName = "GiftBoxBig";
			}
			else
			{
				autoGiftInfo.gType = AutoGiftType.Coins;
				autoGiftInfo.num = UserDataController.GetRamdonCoinsForAutoGift();
				autoGiftInfo.spriteName = "GiftCoinBig";
			}
			break;
		}
		default:
		{
			int num = UnityEngine.Random.Range(1, 101);
			if (num > 90)
			{
				autoGiftInfo.gType = AutoGiftType.GiftBox;
				autoGiftInfo.num = 1;
				autoGiftInfo.spriteName = "GiftBoxBig";
			}
			else
			{
				autoGiftInfo.gType = AutoGiftType.Coins;
				autoGiftInfo.num = UserDataController.GetRamdonCoinsForAutoGift();
				autoGiftInfo.spriteName = "GiftCoinBig";
			}
			break;
		}
		}
		GOGPlayerPrefabs.SetInt("AutoGiftGetCount", GOGPlayerPrefabs.GetInt("AutoGiftGetCount", 0) + 1);
		UserDataController.SetCurAutoGiftTimeRest(UserDataController.GetCurAutoGiftTimeInterval());
		if (autoGiftInfo.gType == AutoGiftType.GiftBox)
		{
			UserDataController.SetCurGiftBoxTotal(UserDataController.GetCurGiftBoxTotal() + 1);
		}
		else
		{
			UserDataController.AddCoins(autoGiftInfo.num);
		}
		return autoGiftInfo;
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x000605DD File Offset: 0x0005E9DD
	public static int GetCurGiftBoxTotal()
	{
		return GOGPlayerPrefabs.GetInt("CurGiftBoxTotal", 1);
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x000605EA File Offset: 0x0005E9EA
	public static void SetCurGiftBoxTotal(int newNum)
	{
		GOGPlayerPrefabs.SetInt("CurGiftBoxTotal", newNum);
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x000605F8 File Offset: 0x0005E9F8
	public static bool HasBuyTheGiftPack()
	{
		return GOGPlayerPrefabs.GetInt("HasBuyTheGiftPack", 0).Equals(1);
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x00060619 File Offset: 0x0005EA19
	public static void VerifyTheGiftPackPurchase()
	{
		GOGPlayerPrefabs.SetInt("HasBuyTheGiftPack", 1);
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00060626 File Offset: 0x0005EA26
	public static int GetGiftBoxSlotsPlayCount()
	{
		return GOGPlayerPrefabs.GetInt("GiftBoxSlotsPlayCount", 0);
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00060633 File Offset: 0x0005EA33
	public static void AddGiftBoxSlotsPlayCount()
	{
		GOGPlayerPrefabs.SetInt("GiftBoxSlotsPlayCount", GOGPlayerPrefabs.GetInt("GiftBoxSlotsPlayCount", 0) + 1);
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x0006064C File Offset: 0x0005EA4C
	public static void StatisticsChkPerLogin()
	{
		UserDataController.AddLoginCount();
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00060653 File Offset: 0x0005EA53
	public static int GetLoginCount()
	{
		return GOGPlayerPrefabs.GetInt("LoginCount_" + UserDataController.GetStrVersion(), 0);
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x0006066A File Offset: 0x0005EA6A
	public static void AddLoginCount()
	{
		GOGPlayerPrefabs.SetInt("LoginCount_" + UserDataController.GetStrVersion(), GOGPlayerPrefabs.GetInt("LoginCount_" + UserDataController.GetStrVersion(), 0) + 1);
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00060698 File Offset: 0x0005EA98
	public static bool HasRatedInAppstore()
	{
		return GOGPlayerPrefabs.GetInt("HasRatedOurGame_" + UserDataController.GetStrVersion(), 0).Equals(1);
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x000606C3 File Offset: 0x0005EAC3
	public static void VerifyAppstoreRating()
	{
		GOGPlayerPrefabs.SetInt("HasRatedOurGame_" + UserDataController.GetStrVersion(), 1);
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x000606DC File Offset: 0x0005EADC
	public static bool IsFirstUseApp()
	{
		return GOGPlayerPrefabs.GetInt("IsFirstUseApp_", 0).Equals(0);
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x000606FD File Offset: 0x0005EAFD
	public static void SetIsFirstUseApp()
	{
		GOGPlayerPrefabs.SetInt("IsFirstUseApp_", 1);
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x0006070A File Offset: 0x0005EB0A
	public static void ResetAllSkinData()
	{
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x0006070C File Offset: 0x0005EB0C
	public static void ResetAllWeaponData()
	{
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x0006070E File Offset: 0x0005EB0E
	public static void ResetWeaponEuqiped()
	{
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00060710 File Offset: 0x0005EB10
	public static void ResetExpAndCoins()
	{
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x00060712 File Offset: 0x0005EB12
	public static void ResetAllData()
	{
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00060714 File Offset: 0x0005EB14
	public static string GetCurSettedArmorName()
	{
		return GOGPlayerPrefabs.GetString("CurSettedArmorName", "BodyArmor_1");
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x00060725 File Offset: 0x0005EB25
	public static void SetCurSettedArmor(string name)
	{
		GOGPlayerPrefabs.SetString("CurSettedArmorName", name);
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00060732 File Offset: 0x0005EB32
	public static void SetArmorAutoSupplyTime(string armorName, float second)
	{
		GOGPlayerPrefabs.SetFloat("AutoSupplyTimeRest" + armorName, second);
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00060745 File Offset: 0x0005EB45
	public static float GetArmorAutoSupplyTime(string armorName)
	{
		return GOGPlayerPrefabs.GetFloat("AutoSupplyTimeRest" + armorName, 0f);
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x0006075C File Offset: 0x0005EB5C
	public static GArmorItemInfo GetArmorItemInfo(string name, GItemId id)
	{
		GArmorItemInfo garmorItemInfo = new GArmorItemInfo();
		garmorItemInfo.mName = name;
		garmorItemInfo.mId = id;
		garmorItemInfo.mAutoSupplyTimeRest = UserDataController.GetArmorAutoSupplyTime(name);
		if (UserDataController.GetCurSettedArmorName() == name)
		{
			garmorItemInfo.mIsEquipped = true;
		}
		if (name != null)
		{
			if (name == "BodyArmor_1")
			{
				if (GOGPlayerPrefabs.GetInt("BodyArmor_1", 1) == 0)
				{
					garmorItemInfo.mIsEnabled = false;
				}
				else
				{
					garmorItemInfo.mIsEnabled = true;
				}
				garmorItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				garmorItemInfo.mUnlockCLevel = 1;
				garmorItemInfo.mSinglePrice = 2;
				garmorItemInfo.mCoinsPriceInGame = 10;
				garmorItemInfo.mNameDisplay = "Body Armor";
				garmorItemInfo.mInGameLogoSpriteNameFg = "Armor_Body_F";
				garmorItemInfo.mInGameLogoSpriteNameBg = "Armor_Body_B";
				garmorItemInfo.mInGameLogoSpriteName = "ArmorBodyLogo";
				garmorItemInfo.mHeadshotIgnoreRate = new float[2];
				garmorItemInfo.mBodyDamageDefendRate = new float[]
				{
					0.15f,
					0.3f
				};
				garmorItemInfo.mDescription = "Body Damage Reducation: 15% - 30%";
				goto IL_289;
			}
			if (name == "HeadArmor_1")
			{
				if (GOGPlayerPrefabs.GetInt("HeadArmor_1", 1) == 0)
				{
					garmorItemInfo.mIsEnabled = false;
				}
				else
				{
					garmorItemInfo.mIsEnabled = true;
				}
				garmorItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				garmorItemInfo.mUnlockCLevel = 1;
				garmorItemInfo.mSinglePrice = 2;
				garmorItemInfo.mCoinsPriceInGame = 10;
				garmorItemInfo.mNameDisplay = "Head Armor";
				garmorItemInfo.mInGameLogoSpriteNameFg = "Armor_Head_F";
				garmorItemInfo.mInGameLogoSpriteNameBg = "Armor_Head_B";
				garmorItemInfo.mInGameLogoSpriteName = "ArmorHeadLogo";
				garmorItemInfo.mHeadshotIgnoreRate = new float[]
				{
					0.3f,
					0.5f
				};
				garmorItemInfo.mBodyDamageDefendRate = new float[2];
				garmorItemInfo.mDescription = "Headshot Protect Rate: 30% - 50%";
				goto IL_289;
			}
			if (name == "HeadNBodyArmor_1")
			{
				if (GOGPlayerPrefabs.GetInt("HeadNBodyArmor_1", 1) == 0)
				{
					garmorItemInfo.mIsEnabled = false;
				}
				else
				{
					garmorItemInfo.mIsEnabled = true;
				}
				garmorItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
				garmorItemInfo.mUnlockCLevel = 1;
				garmorItemInfo.mSinglePrice = 3;
				garmorItemInfo.mCoinsPriceInGame = 15;
				garmorItemInfo.mNameDisplay = "Head&Body Armor";
				garmorItemInfo.mInGameLogoSpriteNameFg = "Armor_HeadNBody_F";
				garmorItemInfo.mInGameLogoSpriteNameBg = "Armor_HeadNBody_B";
				garmorItemInfo.mInGameLogoSpriteName = "ArmorHeadBodyLogo";
				garmorItemInfo.mHeadshotIgnoreRate = new float[]
				{
					0.15f,
					0.3f
				};
				garmorItemInfo.mBodyDamageDefendRate = new float[]
				{
					0.1f,
					0.2f
				};
				garmorItemInfo.mDescription = "Headshot Protect Rate: 15% - 30%\nBody Damage Reducation: 10% - 20%";
				goto IL_289;
			}
		}
		garmorItemInfo.mNameDisplay = "Undefined Armor";
		garmorItemInfo.mDescription = string.Empty;
		IL_289:
		garmorItemInfo.mDescription = garmorItemInfo.mDescription.ToUpper();
		garmorItemInfo.mNameDisplay = garmorItemInfo.mNameDisplay.ToUpper();
		return garmorItemInfo;
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x00060A18 File Offset: 0x0005EE18
	public static int GetLockedPalettePrice(int paletteIndex)
	{
		int result = 0;
		if (paletteIndex != 3)
		{
			if (paletteIndex != 4)
			{
				if (paletteIndex == 5)
				{
					result = 800;
				}
			}
			else
			{
				result = 500;
			}
		}
		else
		{
			result = 300;
		}
		return result;
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x00060A63 File Offset: 0x0005EE63
	public static string GetCurSettedSkinName()
	{
		return GOGPlayerPrefabs.GetString("CurSettedSkinName", "Skin_1");
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x00060A74 File Offset: 0x0005EE74
	public static void SetCurSettedSkin(string name)
	{
		GOGPlayerPrefabs.SetString("CurSettedSkinName", name);
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x00060A81 File Offset: 0x0005EE81
	public static void SetSkinSharedMark(string name, bool mark)
	{
		PlayerPrefs.SetInt("HasSharedSkin_" + name, (!mark) ? 0 : 1);
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x00060AA0 File Offset: 0x0005EEA0
	public static bool HasSkinShared(string name)
	{
		return PlayerPrefs.GetInt("HasSharedSkin_" + name, 0).Equals(1);
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x00060AC8 File Offset: 0x0005EEC8
	public static string[] GetUserAllEnabledSkinName()
	{
		List<string> list = new List<string>();
		GItemId id = new GItemId(1, 1, 1, 1);
		for (int i = 0; i < UserDataController.AllSkinNameList.Length; i++)
		{
			if (UserDataController.GetSkinItemInfo(UserDataController.AllSkinNameList[i], id).mIsEnabled)
			{
				list.Add(UserDataController.AllSkinNameList[i]);
			}
		}
		string[] array = new string[list.Count];
		list.CopyTo(array);
		list.Clear();
		return array;
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x00060B3C File Offset: 0x0005EF3C
	public static GSkinItemInfo GetSkinItemInfo(string name, GItemId id)
	{
		GSkinItemInfo gskinItemInfo = new GSkinItemInfo();
		gskinItemInfo.mName = name;
		if (GOGPlayerPrefabs.GetInt("SkinEnabled_" + name.ToString(), 0) == 0)
		{
			gskinItemInfo.mIsEnabled = false;
		}
		else
		{
			gskinItemInfo.mIsEnabled = true;
		}
		gskinItemInfo.mDescription = "Pick your favorite skin, to be the most personality.";
		switch (name)
		{
		case "Skin_1":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 0;
			gskinItemInfo.mNameDisplay = "Officer";
			gskinItemInfo.mLogoSpriteName = "Skin_1";
			gskinItemInfo.mHeadMaterialName = "Skin_1_1";
			gskinItemInfo.mBodyMaterialName = "Skin_1_2";
			gskinItemInfo.mHandMaterialName = "Skin_1_2";
			goto IL_15DF;
		case "Skin_2":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 0;
			gskinItemInfo.mNameDisplay = "Aviatrix";
			gskinItemInfo.mLogoSpriteName = "Skin_2";
			gskinItemInfo.mHeadMaterialName = "Skin_2_1";
			gskinItemInfo.mBodyMaterialName = "Skin_2_2";
			gskinItemInfo.mHandMaterialName = "Skin_2_2";
			goto IL_15DF;
		case "Skin_3":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 0;
			gskinItemInfo.mNameDisplay = "Bank Robber";
			gskinItemInfo.mLogoSpriteName = "Skin_3";
			gskinItemInfo.mHeadMaterialName = "Skin_3_1";
			gskinItemInfo.mBodyMaterialName = "Skin_3_2";
			gskinItemInfo.mHandMaterialName = "Skin_3_2";
			goto IL_15DF;
		case "Skin_4":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 0;
			gskinItemInfo.mNameDisplay = "Police Woman";
			gskinItemInfo.mLogoSpriteName = "Skin_4";
			gskinItemInfo.mHeadMaterialName = "Skin_4_1";
			gskinItemInfo.mBodyMaterialName = "Skin_4_2";
			gskinItemInfo.mHandMaterialName = "Skin_4_2";
			goto IL_15DF;
		case "Skin_5":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Special Forces";
			gskinItemInfo.mLogoSpriteName = "Skin_5";
			gskinItemInfo.mHeadMaterialName = "Skin_5_1";
			gskinItemInfo.mBodyMaterialName = "Skin_5_2";
			gskinItemInfo.mHandMaterialName = "Skin_5_2";
			goto IL_15DF;
		case "Skin_6":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Special Forces";
			gskinItemInfo.mLogoSpriteName = "Skin_6";
			gskinItemInfo.mHeadMaterialName = "Skin_6_1";
			gskinItemInfo.mBodyMaterialName = "Skin_6_2";
			gskinItemInfo.mHandMaterialName = "Skin_6_2";
			goto IL_15DF;
		case "Skin_7":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 500;
			gskinItemInfo.mNameDisplay = "Soldier";
			gskinItemInfo.mLogoSpriteName = "Skin_7";
			gskinItemInfo.mHeadMaterialName = "Skin_7_1";
			gskinItemInfo.mBodyMaterialName = "Skin_7_2";
			gskinItemInfo.mHandMaterialName = "Skin_7_2";
			goto IL_15DF;
		case "Skin_8":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 500;
			gskinItemInfo.mNameDisplay = "Soldier";
			gskinItemInfo.mLogoSpriteName = "Skin_8";
			gskinItemInfo.mHeadMaterialName = "Skin_8_1";
			gskinItemInfo.mBodyMaterialName = "Skin_8_2";
			gskinItemInfo.mHandMaterialName = "Skin_8_2";
			goto IL_15DF;
		case "Skin_9":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 500;
			gskinItemInfo.mNameDisplay = "Soldier";
			gskinItemInfo.mLogoSpriteName = "Skin_9";
			gskinItemInfo.mHeadMaterialName = "Skin_9_1";
			gskinItemInfo.mBodyMaterialName = "Skin_9_2";
			gskinItemInfo.mHandMaterialName = "Skin_9_2";
			goto IL_15DF;
		case "Skin_10":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 500;
			gskinItemInfo.mNameDisplay = "Soldier";
			gskinItemInfo.mLogoSpriteName = "Skin_10";
			gskinItemInfo.mHeadMaterialName = "Skin_10_1";
			gskinItemInfo.mBodyMaterialName = "Skin_10_2";
			gskinItemInfo.mHandMaterialName = "Skin_10_2";
			goto IL_15DF;
		case "Skin_11":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 500;
			gskinItemInfo.mNameDisplay = "Soldier";
			gskinItemInfo.mLogoSpriteName = "Skin_11";
			gskinItemInfo.mHeadMaterialName = "Skin_11_1";
			gskinItemInfo.mBodyMaterialName = "Skin_11_2";
			gskinItemInfo.mHandMaterialName = "Skin_11_2";
			goto IL_15DF;
		case "Skin_12":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 500;
			gskinItemInfo.mNameDisplay = "Soldier";
			gskinItemInfo.mLogoSpriteName = "Skin_12";
			gskinItemInfo.mHeadMaterialName = "Skin_12_1";
			gskinItemInfo.mBodyMaterialName = "Skin_12_2";
			gskinItemInfo.mHandMaterialName = "Skin_12_2";
			goto IL_15DF;
		case "Skin_13":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Royal Guard";
			gskinItemInfo.mLogoSpriteName = "Skin_13";
			gskinItemInfo.mHeadMaterialName = "Skin_13_1";
			gskinItemInfo.mBodyMaterialName = "Skin_13_2";
			gskinItemInfo.mHandMaterialName = "Skin_13_2";
			goto IL_15DF;
		case "Skin_14":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Royal Guard";
			gskinItemInfo.mLogoSpriteName = "Skin_14";
			gskinItemInfo.mHeadMaterialName = "Skin_14_1";
			gskinItemInfo.mBodyMaterialName = "Skin_14_2";
			gskinItemInfo.mHandMaterialName = "Skin_14_2";
			goto IL_15DF;
		case "Skin_15":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Bishop";
			gskinItemInfo.mLogoSpriteName = "Skin_15";
			gskinItemInfo.mHeadMaterialName = "Skin_15_1";
			gskinItemInfo.mBodyMaterialName = "Skin_15_2";
			gskinItemInfo.mHandMaterialName = "Skin_15_2";
			goto IL_15DF;
		case "Skin_16":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Death";
			gskinItemInfo.mLogoSpriteName = "Skin_16";
			gskinItemInfo.mHeadMaterialName = "Skin_16_1";
			gskinItemInfo.mBodyMaterialName = "Skin_16_2";
			gskinItemInfo.mHandMaterialName = "Skin_16_2";
			goto IL_15DF;
		case "Skin_17":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Robot Cop";
			gskinItemInfo.mLogoSpriteName = "Skin_17";
			gskinItemInfo.mHeadMaterialName = "Skin_17_1";
			gskinItemInfo.mBodyMaterialName = "Skin_17_2";
			gskinItemInfo.mHandMaterialName = "Skin_17_2";
			goto IL_15DF;
		case "Skin_18":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Butcher";
			gskinItemInfo.mLogoSpriteName = "Skin_18";
			gskinItemInfo.mHeadMaterialName = "Skin_18_1";
			gskinItemInfo.mBodyMaterialName = "Skin_18_2";
			gskinItemInfo.mHandMaterialName = "Skin_18_2";
			goto IL_15DF;
		case "Skin_19":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Nurse";
			gskinItemInfo.mLogoSpriteName = "Skin_19";
			gskinItemInfo.mHeadMaterialName = "Skin_19_1";
			gskinItemInfo.mBodyMaterialName = "Skin_19_2";
			gskinItemInfo.mHandMaterialName = "Skin_19_2";
			goto IL_15DF;
		case "Skin_20":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Rainbow Messenger";
			gskinItemInfo.mLogoSpriteName = "Skin_20";
			gskinItemInfo.mHeadMaterialName = "Skin_20_1";
			gskinItemInfo.mBodyMaterialName = "Skin_20_2";
			gskinItemInfo.mHandMaterialName = "Skin_20_2";
			goto IL_15DF;
		case "Skin_21":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Mr. Panda";
			gskinItemInfo.mLogoSpriteName = "Skin_21";
			gskinItemInfo.mHeadMaterialName = "Skin_21_1";
			gskinItemInfo.mBodyMaterialName = "Skin_21_2";
			gskinItemInfo.mHandMaterialName = "Skin_21_2";
			goto IL_15DF;
		case "Skin_22":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Ghast Girl";
			gskinItemInfo.mLogoSpriteName = "Skin_22";
			gskinItemInfo.mHeadMaterialName = "Skin_22_1";
			gskinItemInfo.mBodyMaterialName = "Skin_22_2";
			gskinItemInfo.mHandMaterialName = "Skin_22_2";
			goto IL_15DF;
		case "Skin_23":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Rain Girl";
			gskinItemInfo.mLogoSpriteName = "Skin_23";
			gskinItemInfo.mHeadMaterialName = "Skin_23_1";
			gskinItemInfo.mBodyMaterialName = "Skin_23_2";
			gskinItemInfo.mHandMaterialName = "Skin_23_2";
			goto IL_15DF;
		case "Skin_24":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Betrayer";
			gskinItemInfo.mLogoSpriteName = "Skin_24";
			gskinItemInfo.mHeadMaterialName = "Skin_24_1";
			gskinItemInfo.mBodyMaterialName = "Skin_24_2";
			gskinItemInfo.mHandMaterialName = "Skin_24_2";
			goto IL_15DF;
		case "Skin_25":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Lizard Explorer";
			gskinItemInfo.mLogoSpriteName = "Skin_25";
			gskinItemInfo.mHeadMaterialName = "Skin_25_1";
			gskinItemInfo.mBodyMaterialName = "Skin_25_2";
			gskinItemInfo.mHandMaterialName = "Skin_25_2";
			goto IL_15DF;
		case "Skin_26":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Astronaut";
			gskinItemInfo.mLogoSpriteName = "Skin_26";
			gskinItemInfo.mHeadMaterialName = "Skin_26_1";
			gskinItemInfo.mBodyMaterialName = "Skin_26_2";
			gskinItemInfo.mHandMaterialName = "Skin_26_2";
			goto IL_15DF;
		case "Skin_27":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Frozen Girl";
			gskinItemInfo.mLogoSpriteName = "Skin_27";
			gskinItemInfo.mHeadMaterialName = "Skin_27_1";
			gskinItemInfo.mBodyMaterialName = "Skin_27_2";
			gskinItemInfo.mHandMaterialName = "Skin_27_2";
			goto IL_15DF;
		case "Skin_28":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Lava Knight";
			gskinItemInfo.mLogoSpriteName = "Skin_28";
			gskinItemInfo.mHeadMaterialName = "Skin_28_1";
			gskinItemInfo.mBodyMaterialName = "Skin_28_2";
			gskinItemInfo.mHandMaterialName = "Skin_28_2";
			goto IL_15DF;
		case "Skin_29":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Red Fox";
			gskinItemInfo.mLogoSpriteName = "Skin_29";
			gskinItemInfo.mHeadMaterialName = "Skin_29_1";
			gskinItemInfo.mBodyMaterialName = "Skin_29_2";
			gskinItemInfo.mHandMaterialName = "Skin_29_2";
			goto IL_15DF;
		case "Skin_30":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Scientist";
			gskinItemInfo.mLogoSpriteName = "Skin_30";
			gskinItemInfo.mHeadMaterialName = "Skin_30_1";
			gskinItemInfo.mBodyMaterialName = "Skin_30_2";
			gskinItemInfo.mHandMaterialName = "Skin_30_2";
			goto IL_15DF;
		case "Skin_31":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Magician Girl";
			gskinItemInfo.mLogoSpriteName = "Skin_31";
			gskinItemInfo.mHeadMaterialName = "Skin_31_1";
			gskinItemInfo.mBodyMaterialName = "Skin_31_2";
			gskinItemInfo.mHandMaterialName = "Skin_31_2";
			goto IL_15DF;
		case "Skin_32":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Dragon Girl";
			gskinItemInfo.mLogoSpriteName = "Skin_32";
			gskinItemInfo.mHeadMaterialName = "Skin_32_1";
			gskinItemInfo.mBodyMaterialName = "Skin_32_2";
			gskinItemInfo.mHandMaterialName = "Skin_32_2";
			goto IL_15DF;
		case "Skin_33":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Space Soldier";
			gskinItemInfo.mLogoSpriteName = "Skin_33";
			gskinItemInfo.mHeadMaterialName = "Skin_33_1";
			gskinItemInfo.mBodyMaterialName = "Skin_33_2";
			gskinItemInfo.mHandMaterialName = "Skin_33_2";
			goto IL_15DF;
		case "Skin_34":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Medusa";
			gskinItemInfo.mLogoSpriteName = "Skin_34";
			gskinItemInfo.mHeadMaterialName = "Skin_34_1";
			gskinItemInfo.mBodyMaterialName = "Skin_34_2";
			gskinItemInfo.mHandMaterialName = "Skin_34_2";
			goto IL_15DF;
		case "Skin_35":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Sandworm";
			gskinItemInfo.mLogoSpriteName = "Skin_35";
			gskinItemInfo.mHeadMaterialName = "Skin_35_1";
			gskinItemInfo.mBodyMaterialName = "Skin_35_2";
			gskinItemInfo.mHandMaterialName = "Skin_35_2";
			goto IL_15DF;
		case "Skin_36":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Mask Ninja";
			gskinItemInfo.mLogoSpriteName = "Skin_36";
			gskinItemInfo.mHeadMaterialName = "Skin_36_1";
			gskinItemInfo.mBodyMaterialName = "Skin_36_2";
			gskinItemInfo.mHandMaterialName = "Skin_36_2";
			goto IL_15DF;
		case "Skin_37":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Red Robot";
			gskinItemInfo.mLogoSpriteName = "Skin_37";
			gskinItemInfo.mHeadMaterialName = "Skin_37_1";
			gskinItemInfo.mBodyMaterialName = "Skin_37_2";
			gskinItemInfo.mHandMaterialName = "Skin_37_2";
			goto IL_15DF;
		case "Skin_38":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Skull Pirate";
			gskinItemInfo.mLogoSpriteName = "Skin_38";
			gskinItemInfo.mHeadMaterialName = "Skin_38_1";
			gskinItemInfo.mBodyMaterialName = "Skin_38_2";
			gskinItemInfo.mHandMaterialName = "Skin_38_2";
			goto IL_15DF;
		case "Skin_39":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Gumi";
			gskinItemInfo.mLogoSpriteName = "Skin_39";
			gskinItemInfo.mHeadMaterialName = "Skin_39_1";
			gskinItemInfo.mBodyMaterialName = "Skin_39_2";
			gskinItemInfo.mHandMaterialName = "Skin_39_2";
			goto IL_15DF;
		case "Skin_40":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Halloween Man";
			gskinItemInfo.mLogoSpriteName = "Skin_40";
			gskinItemInfo.mHeadMaterialName = "Skin_40_1";
			gskinItemInfo.mBodyMaterialName = "Skin_40_2";
			gskinItemInfo.mHandMaterialName = "Skin_38_2";
			goto IL_15DF;
		case "Skin_41":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1000;
			gskinItemInfo.mNameDisplay = "Evil King";
			gskinItemInfo.mLogoSpriteName = "Skin_41";
			gskinItemInfo.mHeadMaterialName = "Skin_41_1";
			gskinItemInfo.mBodyMaterialName = "Skin_41_2";
			gskinItemInfo.mHandMaterialName = "Skin_41_2";
			goto IL_15DF;
		case "Skin_42":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Faceless Slayer";
			gskinItemInfo.mLogoSpriteName = "Skin_42";
			gskinItemInfo.mHeadMaterialName = "Skin_42_1";
			gskinItemInfo.mBodyMaterialName = "Skin_42_2";
			gskinItemInfo.mHandMaterialName = "Skin_42_2";
			goto IL_15DF;
		case "Skin_43":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Santa Claus";
			gskinItemInfo.mLogoSpriteName = "Skin_43";
			gskinItemInfo.mHeadMaterialName = "Skin_43_1";
			gskinItemInfo.mBodyMaterialName = "Skin_43_2";
			gskinItemInfo.mHandMaterialName = "Skin_43_2";
			goto IL_15DF;
		case "Skin_44":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Snowman";
			gskinItemInfo.mLogoSpriteName = "Skin_44";
			gskinItemInfo.mHeadMaterialName = "Skin_44_1";
			gskinItemInfo.mBodyMaterialName = "Skin_44_2";
			gskinItemInfo.mHandMaterialName = "Skin_44_2";
			goto IL_15DF;
		case "Skin_45":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Iceman";
			gskinItemInfo.mLogoSpriteName = "Skin_45";
			gskinItemInfo.mHeadMaterialName = "Skin_45_1";
			gskinItemInfo.mBodyMaterialName = "Skin_45_2";
			gskinItemInfo.mHandMaterialName = "Skin_45_2";
			goto IL_15DF;
		case "Skin_46":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Reindeer Girl";
			gskinItemInfo.mLogoSpriteName = "Skin_46";
			gskinItemInfo.mHeadMaterialName = "Skin_46_1";
			gskinItemInfo.mBodyMaterialName = "Skin_46_2";
			gskinItemInfo.mHandMaterialName = "Skin_46_2";
			goto IL_15DF;
		case "Skin_47":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Gingerbread Man";
			gskinItemInfo.mLogoSpriteName = "Skin_47";
			gskinItemInfo.mHeadMaterialName = "Skin_47_1";
			gskinItemInfo.mBodyMaterialName = "Skin_47_2";
			gskinItemInfo.mHandMaterialName = "Skin_47_2";
			goto IL_15DF;
		case "Skin_48":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Christmas Sweater";
			gskinItemInfo.mLogoSpriteName = "Skin_48";
			gskinItemInfo.mHeadMaterialName = "Skin_48_1";
			gskinItemInfo.mBodyMaterialName = "Skin_48_2";
			gskinItemInfo.mHandMaterialName = "Skin_48_2";
			goto IL_15DF;
		case "Skin_49":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Chess King";
			gskinItemInfo.mLogoSpriteName = "Skin_49";
			gskinItemInfo.mHeadMaterialName = "Skin_49_1";
			gskinItemInfo.mBodyMaterialName = "Skin_49_2";
			gskinItemInfo.mHandMaterialName = "Skin_49_2";
			goto IL_15DF;
		case "Skin_50":
			gskinItemInfo.mId = id;
			gskinItemInfo.mPurchasedType = GItemPurchaseType.CoinsPurchase;
			gskinItemInfo.mUnlockCLevel = 1;
			gskinItemInfo.mPrice = 1500;
			gskinItemInfo.mNameDisplay = "Chess Queen";
			gskinItemInfo.mLogoSpriteName = "Skin_50";
			gskinItemInfo.mHeadMaterialName = "Skin_50_1";
			gskinItemInfo.mBodyMaterialName = "Skin_50_2";
			gskinItemInfo.mHandMaterialName = "Skin_50_2";
			goto IL_15DF;
		}
		gskinItemInfo.mId = id;
		gskinItemInfo.mPurchasedType = GItemPurchaseType.GemPurchase;
		gskinItemInfo.mUnlockCLevel = 1;
		gskinItemInfo.mPrice = 20;
		gskinItemInfo.mNameDisplay = name;
		IL_15DF:
		gskinItemInfo.mDescription = gskinItemInfo.mDescription.ToUpper();
		gskinItemInfo.mNameDisplay = gskinItemInfo.mNameDisplay.ToUpper();
		gskinItemInfo.mPurchaseTipsText = gskinItemInfo.mPurchaseTipsText.ToUpper();
		gskinItemInfo.mFixPrice = 0;
		return gskinItemInfo;
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00062164 File Offset: 0x00060564
	public static bool EnableSkin(string name)
	{
		bool result = true;
		GOGPlayerPrefabs.SetInt("SkinEnabled_" + name.ToString(), 1);
		return result;
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x0006218A File Offset: 0x0006058A
	public static void DisableSkin(string name)
	{
		GOGPlayerPrefabs.SetInt("SkinEnabled_" + name.ToString(), 0);
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x000621A2 File Offset: 0x000605A2
	public static void SetWeaponTimeRest(string weaponName, float second)
	{
		GOGPlayerPrefabs.SetFloat("GMWeaponTimeRest_" + weaponName, second);
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x000621B5 File Offset: 0x000605B5
	public static float GetWeaponTimeRest(string weaponName)
	{
		return GOGPlayerPrefabs.GetFloat("GMWeaponTimeRest_" + weaponName, 0f);
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x000621CC File Offset: 0x000605CC
	public static void SetWeaponPlusTimeRest(string weaponName, float second)
	{
		GOGPlayerPrefabs.SetFloat("GMWeaponPlusTimeRest_" + weaponName, second);
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x000621DF File Offset: 0x000605DF
	public static float GetWeaponPlusTimeRest(string weaponName)
	{
		return GOGPlayerPrefabs.GetFloat("GMWeaponPlusTimeRest_" + weaponName, 0f);
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x000621F6 File Offset: 0x000605F6
	public static void SetWeaponPropertyLv(string weaponName, string propertyName, int newLv)
	{
		GOGPlayerPrefabs.SetInt(weaponName + "_" + propertyName + "_Lv", newLv);
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x0006220F File Offset: 0x0006060F
	public static int GetWeaponPropertyLv(string weaponName, string propertyName)
	{
		return GOGPlayerPrefabs.GetInt(weaponName + "_" + propertyName + "_Lv", 0);
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x00062228 File Offset: 0x00060628
	public static void SetWeaponPropertyCardNum(string propertyName, int lv, int newNum)
	{
		GOGPlayerPrefabs.SetInt(propertyName + "_Card_" + lv.ToString(), newNum);
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00062248 File Offset: 0x00060648
	public static int GetWeaponPropertyCardNum(string propertyName, int lv)
	{
		return GOGPlayerPrefabs.GetInt(propertyName + "_Card_" + lv.ToString(), 0);
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x00062268 File Offset: 0x00060668
	public static void AddWeaponPropertyCardNum(string propertyName, int lv, int addNum)
	{
		UserDataController.SetWeaponPropertyCardNum(propertyName, lv, UserDataController.GetWeaponPropertyCardNum(propertyName, lv) + addNum);
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0006227A File Offset: 0x0006067A
	public static void SubWeaponPropertyCardNum(string propertyName, int lv, int subNum)
	{
		UserDataController.SetWeaponPropertyCardNum(propertyName, lv, UserDataController.GetWeaponPropertyCardNum(propertyName, lv) - subNum);
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0006228C File Offset: 0x0006068C
	public static bool IsWeaponChipUnlocked(string weaponName, int chipIndex, int maxChips)
	{
		string text = string.Empty;
		for (int i = 0; i < maxChips; i++)
		{
			if (i == 0)
			{
				text += "0";
			}
			else
			{
				text += "_0";
			}
		}
		string @string = GOGPlayerPrefabs.GetString(weaponName + "_Chips", text);
		string[] array = @string.Split(new char[]
		{
			'_'
		});
		return array[chipIndex - 1].Equals("1");
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x00062308 File Offset: 0x00060708
	public static void UnlockWeaponChip(string weaponName, int chipIndex, int maxChips)
	{
		string text = string.Empty;
		for (int i = 0; i < maxChips; i++)
		{
			if (i == 0)
			{
				text += "0";
			}
			else
			{
				text += "_0";
			}
		}
		string @string = GOGPlayerPrefabs.GetString(weaponName + "_Chips", text);
		string[] array = @string.Split(new char[]
		{
			'_'
		});
		array[chipIndex - 1] = "1";
		string newValue = string.Join("_", array);
		GOGPlayerPrefabs.SetString(weaponName + "_Chips", newValue);
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x000623A0 File Offset: 0x000607A0
	public static void ProcessOneWeaponEquipTap(string tapedWeaponName)
	{
		string[] curEquippedWeaponNameListForStore = UserDataController.GetCurEquippedWeaponNameListForStore();
		int curWeaponEquipLimitedNum = UserDataController.GetCurWeaponEquipLimitedNum();
		GItemId itemId = GrowthManagerKit.GetItemId(tapedWeaponName);
		for (int i = 0; i < curEquippedWeaponNameListForStore.Length; i++)
		{
			if (tapedWeaponName == curEquippedWeaponNameListForStore[i])
			{
				GOGPlayerPrefabs.SetString("CurWeaponEquiped_" + (i + 1).ToString(), string.Empty);
				return;
			}
		}
		if (itemId.mId_2 == 1)
		{
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_1", tapedWeaponName);
		}
		else if (itemId.mId_2 == 2)
		{
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_2", tapedWeaponName);
		}
		else if (itemId.mId_2 == 3)
		{
			if (itemId.mId_3 == 18)
			{
				GOGPlayerPrefabs.SetString("CurWeaponEquiped_6", tapedWeaponName);
			}
			else
			{
				GOGPlayerPrefabs.SetString("CurWeaponEquiped_4", tapedWeaponName);
			}
		}
		else if (itemId.mId_2 == 4)
		{
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_2", tapedWeaponName);
		}
		else if (itemId.mId_2 == 5)
		{
			if (itemId.mId_3 == 6 || itemId.mId_3 == 8)
			{
				GOGPlayerPrefabs.SetString("CurWeaponEquiped_6", tapedWeaponName);
			}
			else
			{
				GOGPlayerPrefabs.SetString("CurWeaponEquiped_3", tapedWeaponName);
			}
		}
		else if (itemId.mId_2 == 6)
		{
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_5", tapedWeaponName);
		}
		else if (itemId.mId_2 == 7)
		{
			if (itemId.mId_3 == 6)
			{
				GOGPlayerPrefabs.SetString("CurWeaponEquiped_6", tapedWeaponName);
			}
			else
			{
				GOGPlayerPrefabs.SetString("CurWeaponEquiped_3", tapedWeaponName);
			}
		}
		else if (itemId.mId_2 == 8)
		{
			GOGPlayerPrefabs.SetString("CurWeaponEquiped_6", tapedWeaponName);
		}
		else if (itemId.mId_2 == 9)
		{
		}
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x00062558 File Offset: 0x00060958
	public static int GetCurWeaponEquipLimitedUnlockLevel()
	{
		switch (GOGPlayerPrefabs.GetInt("CurWeaponEquipLimitedNum", 4))
		{
		case 4:
			return 15;
		case 5:
			return 40;
		case 6:
			return 70;
		case 7:
			return 100;
		default:
			return 100;
		}
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0006259C File Offset: 0x0006099C
	public static int GetCurWeaponEquipLimitedNum()
	{
		switch (GOGPlayerPrefabs.GetInt("CurWeaponEquipLimitedNum", 4))
		{
		case 4:
			if (UserDataController.GetCharacterLevel() >= 15)
			{
			}
			break;
		case 5:
			if (UserDataController.GetCharacterLevel() >= 40)
			{
			}
			break;
		case 6:
			if (UserDataController.GetCharacterLevel() >= 70)
			{
			}
			break;
		case 7:
			if (UserDataController.GetCharacterLevel() >= 100)
			{
			}
			break;
		}
		return GOGPlayerPrefabs.GetInt("CurWeaponEquipLimitedNum", 4);
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x00062621 File Offset: 0x00060A21
	public static void SetCurWeaponEquipLimitedNum(int num)
	{
		GOGPlayerPrefabs.SetInt("CurWeaponEquipLimitedNum", num);
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x00062630 File Offset: 0x00060A30
	public static string[] GetCurEquippedWeaponNameListForStore()
	{
		List<string> list = new List<string>();
		int curWeaponEquipLimitedNum = UserDataController.GetCurWeaponEquipLimitedNum();
		string item = string.Empty;
		for (int i = 0; i < curWeaponEquipLimitedNum; i++)
		{
			item = GOGPlayerPrefabs.GetString("CurWeaponEquiped_" + (i + 1).ToString(), string.Empty);
			list.Add(item);
		}
		string[] array = new string[list.Count];
		list.CopyTo(array);
		list.Clear();
		return array;
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x000626AC File Offset: 0x00060AAC
	public static Dictionary<int, string> GetWeaponNameDic()
	{
		Dictionary<int, string> dictionary = new Dictionary<int, string>();
		for (int i = 0; i < UserDataController.AllWeaponNameList.Length; i++)
		{
			dictionary.Add(i + 1, UserDataController.AllWeaponNameList[i]);
		}
		return dictionary;
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x000626E8 File Offset: 0x00060AE8
	public static string[] GetCurEquippedWeaponNameList()
	{
		List<string> list = new List<string>();
		int curWeaponEquipLimitedNum = UserDataController.GetCurWeaponEquipLimitedNum();
		string text = string.Empty;
		for (int i = 0; i < curWeaponEquipLimitedNum; i++)
		{
			text = GOGPlayerPrefabs.GetString("CurWeaponEquiped_" + (i + 1).ToString(), string.Empty);
			if (text != string.Empty)
			{
				list.Add(text);
			}
		}
		string[] array = new string[list.Count];
		list.CopyTo(array);
		list.Clear();
		return array;
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x00062774 File Offset: 0x00060B74
	public static bool UpgradeWeapon(string name, int upNum)
	{
		bool result = true;
		string key = name + "_Level";
		GOGPlayerPrefabs.SetInt(key, GOGPlayerPrefabs.GetInt(key, 0) + upNum);
		return result;
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x000627A0 File Offset: 0x00060BA0
	public static GWeaponItemInfo GetWeaponItemInfo(string name, GItemId id, bool isSearchForUpgrade, int fixedLv)
	{
		GWeaponItemInfo gweaponItemInfo = new GWeaponItemInfo();
		string[] curEquippedWeaponNameList = UserDataController.GetCurEquippedWeaponNameList();
		for (int i = 0; i < curEquippedWeaponNameList.Length; i++)
		{
			if (name == curEquippedWeaponNameList[i])
			{
				gweaponItemInfo.mIsEquipped = true;
				break;
			}
		}
		gweaponItemInfo.mName = name;
		gweaponItemInfo.mWeaponTimeRest = UserDataController.GetWeaponTimeRest(name);
		gweaponItemInfo.mWeaponPlusTimeRest = UserDataController.GetWeaponPlusTimeRest(name);
		int num;
		if (gweaponItemInfo.mWeaponPlusTimeRest > 0f)
		{
			num = 2;
		}
		else if (gweaponItemInfo.mWeaponTimeRest > 0f)
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		if (isSearchForUpgrade)
		{
			num++;
		}
		if (fixedLv != -1)
		{
			num = fixedLv;
		}
		gweaponItemInfo.mCurWeaponLevel = num;
		gweaponItemInfo.mMaxWeaponLevel = 2;
		gweaponItemInfo.mIsEnabled = (gweaponItemInfo.mWeaponTimeRest > 0f);
		gweaponItemInfo.mIsPlused = (gweaponItemInfo.mWeaponPlusTimeRest > 0f);
		gweaponItemInfo.mId = id;
		gweaponItemInfo.mUnlockCLevel = 1;
		if (gweaponItemInfo.mCurWeaponLevel == 1 || gweaponItemInfo.mCurWeaponLevel == 0)
		{
			gweaponItemInfo.mLogoSpriteName = string.Concat(new string[]
			{
				"GItemLogo_",
				id.mId_1.ToString(),
				"_",
				id.mId_2.ToString(),
				"_",
				id.mId_3.ToString(),
				"_0"
			});
		}
		else
		{
			gweaponItemInfo.mLogoSpriteName = string.Concat(new string[]
			{
				"GItemLogo_",
				id.mId_1.ToString(),
				"_",
				id.mId_2.ToString(),
				"_",
				id.mId_3.ToString(),
				"_0"
			});
		}
		if (gweaponItemInfo.mWeaponTimeRest > 720000f)
		{
			gweaponItemInfo.mIsNoLimitedUse = true;
			gweaponItemInfo.mCanUpgrade = true;
		}
		else
		{
			gweaponItemInfo.mIsNoLimitedUse = false;
			gweaponItemInfo.mCanUpgrade = false;
		}
		gweaponItemInfo.mClipPrice = 0;
		switch (name)
		{
		case "BallisticKnife":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Ballistic Knife";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 1;
			gweaponItemInfo.mMoveSpeed = 6.7f;
			gweaponItemInfo.mMinDamage = 12f;
			gweaponItemInfo.mMaxDamage = 30f;
			gweaponItemInfo.mNoLimitedUsePrice = 30;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 8f);
				gweaponItemInfo.mFeatureDescription = "Regular melee weapon.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 8f);
				gweaponItemInfo.mFeatureDescription = "Regular melee weapon.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 8f);
				gweaponItemInfo.mFeatureDescription = "Regular melee weapon.";
				break;
			}
			goto IL_8ACD;
		case "HonorKnife":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Brave Knife";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mIsHonorItem = true;
			gweaponItemInfo.mWeaponId = 43;
			gweaponItemInfo.mMoveSpeed = 6.7f;
			gweaponItemInfo.mMinDamage = 30f;
			gweaponItemInfo.mMaxDamage = 60f;
			gweaponItemInfo.mNoLimitedUsePrice = 500;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = 3;
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 8f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nThe warriors need it.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 8f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nThe warriors need it.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 4.5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 8f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nThe warriors need it.";
				break;
			}
			goto IL_8ACD;
		case "DesertEagle":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Desert Eagle";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 2;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mMinDamage = 30f;
			gweaponItemInfo.mMaxDamage = 45f;
			gweaponItemInfo.mNoLimitedUsePrice = 45;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 1.11f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "Classic pistol, great power.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 1.11f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "Classic pistol, great power.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 1.11f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "Classic pistol, great power.";
				break;
			}
			goto IL_8ACD;
		case "AK47":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "AK47";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 3;
			gweaponItemInfo.mMoveSpeed = 4.5f;
			gweaponItemInfo.mMinDamage = 16f;
			gweaponItemInfo.mMaxDamage = 30f;
			gweaponItemInfo.mNoLimitedUsePrice = 90;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "High damage, widely used.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "High damage, widely used.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.3f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "High damage, widely used.";
				break;
			}
			goto IL_8ACD;
		case "HonorAK47":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Golden AK47";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mIsHonorItem = true;
			gweaponItemInfo.mWeaponId = 44;
			gweaponItemInfo.mMoveSpeed = 4.5f;
			gweaponItemInfo.mMinDamage = 16f;
			gweaponItemInfo.mMaxDamage = 32f;
			gweaponItemInfo.mNoLimitedUsePrice = 1500;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = 3;
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.8f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden AK47, the symbol of ability.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.8f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden AK47, the symbol of ability.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.8f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden AK47, the symbol of ability.";
				break;
			}
			goto IL_8ACD;
		case "M4":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "M4";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 4;
			gweaponItemInfo.mMoveSpeed = 5.1f;
			gweaponItemInfo.mMinDamage = 18f;
			gweaponItemInfo.mMaxDamage = 26f;
			gweaponItemInfo.mNoLimitedUsePrice = 75;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.2f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6.2f);
				gweaponItemInfo.mFeatureDescription = "More balanced performance.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.2f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6.2f);
				gweaponItemInfo.mFeatureDescription = "More balanced performance.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.2f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 6.2f);
				gweaponItemInfo.mFeatureDescription = "More balanced performance.";
				break;
			}
			goto IL_8ACD;
		case "HonorM4":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Golden M4";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mIsHonorItem = true;
			gweaponItemInfo.mWeaponId = 45;
			gweaponItemInfo.mMoveSpeed = 5.1f;
			gweaponItemInfo.mMinDamage = 18f;
			gweaponItemInfo.mMaxDamage = 28f;
			gweaponItemInfo.mNoLimitedUsePrice = 1000;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = 3;
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.6f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6.2f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden M4, the symbol of ability.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.6f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6.2f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden M4, the symbol of ability.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.6f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 6.2f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden M4, the symbol of ability.";
				break;
			}
			goto IL_8ACD;
		case "M87T":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "M87T";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 5;
			gweaponItemInfo.mMoveSpeed = 4.75f;
			gweaponItemInfo.mMinDamage = 9f;
			gweaponItemInfo.mMaxDamage = 13f;
			gweaponItemInfo.mNoLimitedUsePrice = 60;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.83f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.8f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Moderate power, high fire rate.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.83f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.8f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Moderate power, high fire rate.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 5.5f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 0.83f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.8f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Moderate power, high fire rate.";
				break;
			}
			goto IL_8ACD;
		case "AWP":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "AWP";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 6;
			gweaponItemInfo.mMoveSpeed = 4.2f;
			gweaponItemInfo.mMinDamage = 210f;
			gweaponItemInfo.mMaxDamage = 215f;
			gweaponItemInfo.mNoLimitedUsePrice = 90;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Aim"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployCurProperty("Aim", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "Balanced performance.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployCurProperty("Aim", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "Balanced performance.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 10f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployNextProperty("Aim", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "Balanced performance.";
				break;
			}
			goto IL_8ACD;
		case "HonorAWP":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "Golden AWP";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mIsHonorItem = true;
			gweaponItemInfo.mWeaponId = 54;
			gweaponItemInfo.mMoveSpeed = 4.2f;
			gweaponItemInfo.mMinDamage = 210f;
			gweaponItemInfo.mMaxDamage = 215f;
			gweaponItemInfo.mNoLimitedUsePrice = 1500;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Aim"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployCurProperty("Aim", 3.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden AWP, the symbol of ability.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployCurProperty("Aim", 3.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden AWP, the symbol of ability.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 10f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployNextProperty("Aim", 3.3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden AWP, the symbol of ability.";
				break;
			}
			goto IL_8ACD;
		case "RPG":
			gweaponItemInfo.mClipPrice = 40;
			gweaponItemInfo.mNameDisplay = "RPG";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 7;
			gweaponItemInfo.mMoveSpeed = 4f;
			gweaponItemInfo.mNoLimitedUsePrice = 225;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Range"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.23f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "Heavy weapon, mass destruction.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.23f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "Heavy weapon, mass destruction.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 10f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 0.23f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployNextProperty("Range", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "Heavy weapon, mass destruction.";
				break;
			}
			goto IL_8ACD;
		case "M67":
			gweaponItemInfo.mClipPrice = 10;
			gweaponItemInfo.mNameDisplay = "M67";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 8;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mIsEnabled = true;
			gweaponItemInfo.mIsNoLimitedUse = true;
			gweaponItemInfo.mCanUpgrade = false;
			gweaponItemInfo.mNoLimitedUsePrice = 0;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(null);
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 1.67f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Regular grenade, high explosive delay.<5 coins per grenade>";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 1.67f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Regular grenade, high explosive delay.<5 coins per grenade>";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployNextProperty("Blast Speed", 1.67f);
				gweaponItemInfo.AddNDeployNextProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Regular grenade, high explosive delay.<5 coins per grenade>";
				break;
			}
			goto IL_8ACD;
		case "GLOCK21":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "GLOCK 21";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 9;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mMinDamage = 15f;
			gweaponItemInfo.mMaxDamage = 20f;
			gweaponItemInfo.mNoLimitedUsePrice = 30;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 1.66f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 1.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 8.2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, great stability.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 1.66f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 1.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 8.2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, great stability.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 1.75f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 1.66f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 1.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 8.2f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, great stability.";
				break;
			}
			goto IL_8ACD;
		case "MP5KA5":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "MP5KA5";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 10;
			gweaponItemInfo.mMoveSpeed = 6f;
			gweaponItemInfo.mMinDamage = 10f;
			gweaponItemInfo.mMaxDamage = 20f;
			gweaponItemInfo.mNoLimitedUsePrice = 75;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "Great accuracy, good overall performance.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "Great accuracy, good overall performance.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 1.5f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 8.13f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 7.5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "Great accuracy, good overall performance.";
				break;
			}
			goto IL_8ACD;
		case "UZI":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "UZI";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 11;
			gweaponItemInfo.mMoveSpeed = 6.25f;
			gweaponItemInfo.mMinDamage = 8f;
			gweaponItemInfo.mMaxDamage = 18f;
			gweaponItemInfo.mNoLimitedUsePrice = 60;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, light and fast reload.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, light and fast reload.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 1.3f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 6.6f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, light and fast reload.";
				break;
			}
			goto IL_8ACD;
		case "G36K":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "G36K";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 12;
			gweaponItemInfo.mMoveSpeed = 4.75f;
			gweaponItemInfo.mMinDamage = 18f;
			gweaponItemInfo.mMaxDamage = 35f;
			gweaponItemInfo.mNoLimitedUsePrice = 105;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.65f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Excellent overall performance, moderate accuracy.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.65f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Excellent overall performance, moderate accuracy.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.65f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Excellent overall performance, moderate accuracy.";
				break;
			}
			goto IL_8ACD;
		case "M249":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "M249";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 13;
			gweaponItemInfo.mMoveSpeed = 4f;
			gweaponItemInfo.mMinDamage = 40f;
			gweaponItemInfo.mMaxDamage = 50f;
			gweaponItemInfo.mNoLimitedUsePrice = 135;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Move",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 4.16f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 3.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "Moderate fire rate, strong endurance, great power.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 4.16f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 3.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "Moderate fire rate, strong endurance, great power.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 4.5f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 4.16f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 3.3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "Moderate fire rate, strong endurance, great power.";
				break;
			}
			goto IL_8ACD;
		case "MilkBomb":
			gweaponItemInfo.mClipPrice = 10;
			gweaponItemInfo.mNameDisplay = "Milk Bomb";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 14;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mIsEnabled = true;
			gweaponItemInfo.mIsNoLimitedUse = true;
			gweaponItemInfo.mCanUpgrade = false;
			gweaponItemInfo.mNoLimitedUsePrice = 0;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(null);
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 5f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Special grenade, low explosive delay.<6 coins per grenade>";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 5f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Special grenade, low explosive delay.<6 coins per grenade>";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployNextProperty("Blast Speed", 5f);
				gweaponItemInfo.AddNDeployNextProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Special grenade, low explosive delay.<6 coins per grenade>";
				break;
			}
			goto IL_8ACD;
		case "CandyRifle":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Candy Rifle";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 15;
			gweaponItemInfo.mMoveSpeed = 5f;
			gweaponItemInfo.mMinDamage = 13f;
			gweaponItemInfo.mMaxDamage = 28f;
			gweaponItemInfo.mNoLimitedUsePrice = 180;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "Candy Rifle..";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "Candy Rifle..";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 5.5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "Candy Rifle..";
				break;
			}
			goto IL_8ACD;
		case "ChristmasSniper":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "Christmas Sniper";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 16;
			gweaponItemInfo.mMoveSpeed = 4.2f;
			gweaponItemInfo.mMinDamage = 210f;
			gweaponItemInfo.mMaxDamage = 215f;
			gweaponItemInfo.mNoLimitedUsePrice = 330;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Aim"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.46f);
				gweaponItemInfo.AddNDeployCurProperty("Aim", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "Christmas Sniper..";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.46f);
				gweaponItemInfo.AddNDeployCurProperty("Aim", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "Christmas Sniper..";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 10f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.46f);
				gweaponItemInfo.AddNDeployNextProperty("Aim", 6f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "Christmas Sniper..";
				break;
			}
			goto IL_8ACD;
		case "GingerbreadBomb":
			gweaponItemInfo.mClipPrice = 10;
			gweaponItemInfo.mNameDisplay = "Bear Bomb";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 17;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mIsEnabled = true;
			gweaponItemInfo.mIsNoLimitedUse = true;
			gweaponItemInfo.mCanUpgrade = false;
			gweaponItemInfo.mNoLimitedUsePrice = 0;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(null);
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 2.5f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Special grenade, low explosive delay.<8 coins per grenade>";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 2.5f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Special grenade, low explosive delay.<8 coins per grenade>";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployNextProperty("Blast Speed", 2.5f);
				gweaponItemInfo.AddNDeployNextProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Special grenade, low explosive delay.<8 coins per grenade>";
				break;
			}
			goto IL_8ACD;
		case "GingerbreadKnife":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Gingerbread Knife";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 18;
			gweaponItemInfo.mMoveSpeed = 6.7f;
			gweaponItemInfo.mMinDamage = 16f;
			gweaponItemInfo.mMaxDamage = 35f;
			gweaponItemInfo.mNoLimitedUsePrice = 45;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 8f);
				gweaponItemInfo.mFeatureDescription = "Gingerbread Knife..";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 8f);
				gweaponItemInfo.mFeatureDescription = "Gingerbread Knife..";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 8f);
				gweaponItemInfo.mFeatureDescription = "Gingerbread Knife..";
				break;
			}
			goto IL_8ACD;
		case "SantaGun":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "FN 2000";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 19;
			gweaponItemInfo.mMoveSpeed = 4.5f;
			gweaponItemInfo.mMinDamage = 14f;
			gweaponItemInfo.mMaxDamage = 20f;
			gweaponItemInfo.mNoLimitedUsePrice = 90;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.2f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, look gorgeous.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.2f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, look gorgeous.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 1.7f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.2f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, look gorgeous.";
				break;
			}
			goto IL_8ACD;
		case "AUG":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "AUG";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 20;
			gweaponItemInfo.mMoveSpeed = 5f;
			gweaponItemInfo.mMinDamage = 20f;
			gweaponItemInfo.mMaxDamage = 40f;
			gweaponItemInfo.mNoLimitedUsePrice = 105;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "Excellent overall performance, light, moderate accuracy.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "Excellent overall performance, light, moderate accuracy.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "Excellent overall performance, light, moderate accuracy.";
				break;
			}
			goto IL_8ACD;
		case "M3":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "M3";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 21;
			gweaponItemInfo.mMoveSpeed = 4.75f;
			gweaponItemInfo.mMinDamage = 8f;
			gweaponItemInfo.mMaxDamage = 13f;
			gweaponItemInfo.mNoLimitedUsePrice = 90;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 8.4f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Great power, low fire rate.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 8.4f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Great power, low fire rate.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 8.4f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Great power, low fire rate.";
				break;
			}
			goto IL_8ACD;
		case "M134":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "M134";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 22;
			gweaponItemInfo.mMoveSpeed = 4f;
			gweaponItemInfo.mMinDamage = 30f;
			gweaponItemInfo.mMaxDamage = 45f;
			gweaponItemInfo.mNoLimitedUsePrice = 165;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Move",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 2.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, strong endurance, moderate power.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 2.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, strong endurance, moderate power.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 2.6f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, strong endurance, moderate power.";
				break;
			}
			goto IL_8ACD;
		case "HonorM134":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "Golden M134";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mIsHonorItem = true;
			gweaponItemInfo.mWeaponId = 55;
			gweaponItemInfo.mMoveSpeed = 4.2f;
			gweaponItemInfo.mMinDamage = 30f;
			gweaponItemInfo.mMaxDamage = 45f;
			gweaponItemInfo.mNoLimitedUsePrice = 1000;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Move",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 2.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden M134, the symbol of ability.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 2.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden M134, the symbol of ability.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 2.6f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "[Honor Weapon]\nGolden M134, the symbol of ability.";
				break;
			}
			goto IL_8ACD;
		case "StenMarkV":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Sten Mark V";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 23;
			gweaponItemInfo.mMoveSpeed = 6f;
			gweaponItemInfo.mMinDamage = 8f;
			gweaponItemInfo.mMaxDamage = 20f;
			gweaponItemInfo.mNoLimitedUsePrice = 90;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.41f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, light, great accuracy.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.41f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, light, great accuracy.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 1.41f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 8.13f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, light, great accuracy.";
				break;
			}
			goto IL_8ACD;
		case "FlashBomb":
			gweaponItemInfo.mClipPrice = 10;
			gweaponItemInfo.mNameDisplay = "Flash-1";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 25;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mIsEnabled = true;
			gweaponItemInfo.mIsNoLimitedUse = true;
			gweaponItemInfo.mCanUpgrade = false;
			gweaponItemInfo.mNoLimitedUsePrice = 0;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(null);
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 2.5f);
				gweaponItemInfo.mFeatureDescription = "Regular flash bomb.<5 coins per grenade>";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 2.5f);
				gweaponItemInfo.mFeatureDescription = "Regular flash bomb.<5 coins per grenade>";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Blast Speed", 2.5f);
				gweaponItemInfo.mFeatureDescription = "Regular flash bomb.<5 coins per grenade>";
				break;
			}
			goto IL_8ACD;
		case "SmokeBomb":
			gweaponItemInfo.mClipPrice = 10;
			gweaponItemInfo.mNameDisplay = "Smoke-1";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 24;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mIsEnabled = true;
			gweaponItemInfo.mIsNoLimitedUse = true;
			gweaponItemInfo.mCanUpgrade = false;
			gweaponItemInfo.mNoLimitedUsePrice = 0;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(null);
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 1.25f);
				gweaponItemInfo.mFeatureDescription = "Regular smoke bomb.<8 coins per grenade>";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 1.25f);
				gweaponItemInfo.mFeatureDescription = "Regular smoke bomb.<8 coins per grenade>";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Blast Speed", 1.25f);
				gweaponItemInfo.mFeatureDescription = "Regular smoke bomb.<8 coins per grenade>";
				break;
			}
			goto IL_8ACD;
		case "LaserR7":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Laser R-7";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 26;
			gweaponItemInfo.mMoveSpeed = 4.75f;
			gweaponItemInfo.mMinDamage = 20f;
			gweaponItemInfo.mMaxDamage = 40f;
			gweaponItemInfo.mNoLimitedUsePrice = 135;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "First futuristic gun.Excellent overall performance.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "First futuristic gun.Excellent overall performance.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 6.6f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "First futuristic gun.Excellent overall performance.";
				break;
			}
			goto IL_8ACD;
		case "Shovel":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Shovel";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 27;
			gweaponItemInfo.mMoveSpeed = 6.5f;
			gweaponItemInfo.mMinDamage = 45f;
			gweaponItemInfo.mMaxDamage = 50f;
			gweaponItemInfo.mNoLimitedUsePrice = 30;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.8f);
				gweaponItemInfo.mFeatureDescription = "Special melee weapon.Slower but more powerful.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.8f);
				gweaponItemInfo.mFeatureDescription = "Special melee weapon.Slower but more powerful.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.8f);
				gweaponItemInfo.mFeatureDescription = "Special melee weapon.Slower but more powerful.";
				break;
			}
			goto IL_8ACD;
		case "ZombieHand":
			gweaponItemInfo.mNameDisplay = "ZombieHand";
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mFeatureDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 28;
			gweaponItemInfo.mMoveSpeed = 7f;
			gweaponItemInfo.mMinDamage = 60f;
			gweaponItemInfo.mMaxDamage = 80f;
			goto IL_8ACD;
		case "Firelock":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Firelock";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 29;
			gweaponItemInfo.mMoveSpeed = 5.5f;
			gweaponItemInfo.mMinDamage = 40f;
			gweaponItemInfo.mMaxDamage = 70f;
			gweaponItemInfo.mNoLimitedUsePrice = 45;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.83f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6.6f);
				gweaponItemInfo.mFeatureDescription = "Classic weapon, slower but more powerful.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.83f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6.6f);
				gweaponItemInfo.mFeatureDescription = "Classic weapon, slower but more powerful.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 5.5f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 0.83f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 6.6f);
				gweaponItemInfo.mFeatureDescription = "Classic weapon, slower but more powerful.";
				break;
			}
			goto IL_8ACD;
		case "HalloweenGun":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "Halloween Gun";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 30;
			gweaponItemInfo.mMoveSpeed = 4.2f;
			gweaponItemInfo.mMinDamage = 35f;
			gweaponItemInfo.mMaxDamage = 50f;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mNoLimitedUsePrice = 120;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Move",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4.25f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 5.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 3.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "Special machine gun for halloween.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4.25f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 5.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 3.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "Special machine gun for halloween.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 4.25f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 5.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 3.3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "Special machine gun for halloween.";
				break;
			}
			goto IL_8ACD;
		case "SM134":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "S-M134";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 31;
			gweaponItemInfo.mMoveSpeed = 4f;
			gweaponItemInfo.mMinDamage = 30f;
			gweaponItemInfo.mMaxDamage = 45f;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mNoLimitedUsePrice = 165;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Move",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 2.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nHigh fire rate, strong endurance, moderate power.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 2.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nHigh fire rate, strong endurance, moderate power.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 2.6f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 4.8f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nHigh fire rate, strong endurance, moderate power.";
				break;
			}
			goto IL_8ACD;
		case "SM4":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "S-M4";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 32;
			gweaponItemInfo.mMoveSpeed = 5.1f;
			gweaponItemInfo.mMinDamage = 18f;
			gweaponItemInfo.mMaxDamage = 26f;
			gweaponItemInfo.mNoLimitedUsePrice = 75;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.2f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6.2f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nMore balanced performance.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.2f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6.2f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nMore balanced performance.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.2f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 6.2f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nMore balanced performance.";
				break;
			}
			goto IL_8ACD;
		case "SG36K":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "S-G36K";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 33;
			gweaponItemInfo.mMoveSpeed = 4.75f;
			gweaponItemInfo.mMinDamage = 18f;
			gweaponItemInfo.mMaxDamage = 35f;
			gweaponItemInfo.mNoLimitedUsePrice = 105;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.65f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nExcellent overall performance, moderate accuracy.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.65f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nExcellent overall performance, moderate accuracy.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.65f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nExcellent overall performance, moderate accuracy.";
				break;
			}
			goto IL_8ACD;
		case "SAUG":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "S-AUG";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 34;
			gweaponItemInfo.mMoveSpeed = 5f;
			gweaponItemInfo.mMinDamage = 20f;
			gweaponItemInfo.mMaxDamage = 40f;
			gweaponItemInfo.mNoLimitedUsePrice = 105;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nExcellent overall performance, light, moderate accuracy.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nExcellent overall performance, light, moderate accuracy.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nExcellent overall performance, light, moderate accuracy.";
				break;
			}
			goto IL_8ACD;
		case "SAK47":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "S-AK47";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 35;
			gweaponItemInfo.mMoveSpeed = 4.5f;
			gweaponItemInfo.mMinDamage = 16f;
			gweaponItemInfo.mMaxDamage = 30f;
			gweaponItemInfo.mNoLimitedUsePrice = 150;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nHigh damage, widely used.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nHigh damage, widely used.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.3f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 5.3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nHigh damage, widely used.";
				break;
			}
			goto IL_8ACD;
		case "SDesertEagle":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "SDesert Eagle";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 36;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mMinDamage = 30f;
			gweaponItemInfo.mMaxDamage = 45f;
			gweaponItemInfo.mNoLimitedUsePrice = 90;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 1.11f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nClassic pistol, great power.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 1.11f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nClassic pistol, great power.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 1.11f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nClassic pistol, great power.";
				break;
			}
			goto IL_8ACD;
		case "SantaGun2014":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Santa Gun";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 37;
			gweaponItemInfo.mMoveSpeed = 4.75f;
			gweaponItemInfo.mMinDamage = 25f;
			gweaponItemInfo.mMaxDamage = 35f;
			gweaponItemInfo.mNoLimitedUsePrice = 150;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nGift from Santa Claus.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nGift from Santa Claus.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nGift from Santa Claus.";
				break;
			}
			goto IL_8ACD;
		case "ThunderX6":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "Thunder X6";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 38;
			gweaponItemInfo.mMoveSpeed = 4.5f;
			gweaponItemInfo.mMinDamage = 210f;
			gweaponItemInfo.mMaxDamage = 215f;
			gweaponItemInfo.mNoLimitedUsePrice = 150;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Aim"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.46f);
				gweaponItemInfo.AddNDeployCurProperty("Aim", 3.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Futuristic Sniper Rifle, Aim x 1.2.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.46f);
				gweaponItemInfo.AddNDeployCurProperty("Aim", 3.6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Futuristic Sniper Rifle, Aim x 1.2.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 10f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 0.41f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.46f);
				gweaponItemInfo.AddNDeployNextProperty("Aim", 3.6f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Futuristic Sniper Rifle, Aim x 1.2.";
				break;
			}
			goto IL_8ACD;
		case "MK5":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "MK5";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 39;
			gweaponItemInfo.mMoveSpeed = 6.25f;
			gweaponItemInfo.mMinDamage = 8f;
			gweaponItemInfo.mMaxDamage = 18f;
			gweaponItemInfo.mNoLimitedUsePrice = 75;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "Compact size, high flexibility.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "Compact size, high flexibility.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 1.5f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "Compact size, high flexibility.";
				break;
			}
			goto IL_8ACD;
		case "BurstRG2":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "Burst RG2";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 40;
			gweaponItemInfo.mMoveSpeed = 4.5f;
			gweaponItemInfo.mMinDamage = 25f;
			gweaponItemInfo.mMaxDamage = 50f;
			gweaponItemInfo.mNoLimitedUsePrice = 135;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Move",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 3.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Futuristic Machinegun. Light and powerful.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 3.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Futuristic Machinegun. Light and powerful.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 6.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 3.3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Futuristic Machinegun. Light and powerful.";
				break;
			}
			goto IL_8ACD;
		case "SnowmanBomb":
			gweaponItemInfo.mClipPrice = 10;
			gweaponItemInfo.mNameDisplay = "Snowman Bomb";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 41;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mIsEnabled = true;
			gweaponItemInfo.mIsNoLimitedUse = true;
			gweaponItemInfo.mCanUpgrade = false;
			gweaponItemInfo.mNoLimitedUsePrice = 0;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(null);
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 5f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Special grenade for Christmas, low explosive delay.<6 coins per grenade>";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployCurProperty("Blast Speed", 5f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Special grenade for Christmas, low explosive delay.<6 coins per grenade>";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 9.35f);
				gweaponItemInfo.AddNDeployNextProperty("Blast Speed", 5f);
				gweaponItemInfo.AddNDeployNextProperty("Range", 3f);
				gweaponItemInfo.mFeatureDescription = "Special grenade for Christmas, low explosive delay.<6 coins per grenade>";
				break;
			}
			goto IL_8ACD;
		case "CandyHammer":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Candy Hammer";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 42;
			gweaponItemInfo.mMoveSpeed = 6f;
			gweaponItemInfo.mMinDamage = 50f;
			gweaponItemInfo.mMaxDamage = 80f;
			gweaponItemInfo.mNoLimitedUsePrice = 180;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 7f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nSpecial Hammer from Santa.Slower but more powerful.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 7f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nSpecial Hammer from Santa.Slower but more powerful.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 7f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "[Christmas Only]\nSpecial Hammer from Santa.Slower but more powerful.";
				break;
			}
			goto IL_8ACD;
		case "TeslaP1":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Tesla P1";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 46;
			gweaponItemInfo.mMoveSpeed = 6f;
			gweaponItemInfo.mMinDamage = 20f;
			gweaponItemInfo.mMaxDamage = 40f;
			gweaponItemInfo.mNoLimitedUsePrice = 120;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Energy"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Energy", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "High and stable electric damage, best accuracy but no headshot.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Energy", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "High and stable electric damage, best accuracy but no headshot.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployNextProperty("Energy", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 10f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.3f);
				gweaponItemInfo.mFeatureDescription = "High and stable electric damage, best accuracy but no headshot.";
				break;
			}
			goto IL_8ACD;
		case "MiniCannon":
			gweaponItemInfo.mClipPrice = 20;
			gweaponItemInfo.mNameDisplay = "Mini Cannon";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 48;
			gweaponItemInfo.mMoveSpeed = 4.2f;
			gweaponItemInfo.mNoLimitedUsePrice = 75;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Range"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 8.25f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.23f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 4.2f);
				gweaponItemInfo.mFeatureDescription = "Mini heavy weapon, convenient to carry.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 8.25f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 0.23f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 4.2f);
				gweaponItemInfo.mFeatureDescription = "Mini heavy weapon, convenient to carry.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 8.25f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 0.23f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployNextProperty("Range", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 4.2f);
				gweaponItemInfo.mFeatureDescription = "Mini heavy weapon, convenient to carry.";
				break;
			}
			goto IL_8ACD;
		case "M1":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "M1";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 47;
			gweaponItemInfo.mMoveSpeed = 4.5f;
			gweaponItemInfo.mMinDamage = 20f;
			gweaponItemInfo.mMaxDamage = 28f;
			gweaponItemInfo.mNoLimitedUsePrice = 60;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.4f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Balanced performance, stable damage.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.4f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Balanced performance, stable damage.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.4f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Balanced performance, stable damage.";
				break;
			}
			goto IL_8ACD;
		case "M29":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Fantasy M29";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 49;
			gweaponItemInfo.mMoveSpeed = 4.5f;
			gweaponItemInfo.mMinDamage = 24f;
			gweaponItemInfo.mMaxDamage = 34f;
			gweaponItemInfo.mNoLimitedUsePrice = 120;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.9f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Fantasy rifle, great performance.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.9f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Fantasy rifle, great performance.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.9f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 2.67f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 4.5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Fantasy rifle, great performance.";
				break;
			}
			goto IL_8ACD;
		case "DualPistol":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Dual Pistol";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 50;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mMinDamage = 15f;
			gweaponItemInfo.mMaxDamage = 30f;
			gweaponItemInfo.mNoLimitedUsePrice = 60;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.25f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 2.22f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 1.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "Epic pistol.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.25f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 2.22f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 1.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "Epic pistol.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.25f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 2.22f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 1.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 7.2f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "Epic pistol.";
				break;
			}
			goto IL_8ACD;
		case "FreddyGun":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Freddy Gun";
			gweaponItemInfo.mWeaponId = 51;
			gweaponItemInfo.mMoveSpeed = 4.75f;
			gweaponItemInfo.mMinDamage = 21f;
			gweaponItemInfo.mMaxDamage = 33f;
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mSellable = false;
			gweaponItemInfo.mIsCollection = true;
			gweaponItemInfo.mNoLimitedUsePrice = 200;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			gweaponItemInfo.DeployChipsStates();
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Special weapon, Collect chips by hunting to active it!";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Special weapon, Collect chips by hunting to active it!";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.7f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 6.06f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 6f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Special weapon, Collect chips by hunting to active it!";
				break;
			}
			goto IL_8ACD;
		case "ImpulseGun":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "Impulse Dragon";
			gweaponItemInfo.mWeaponId = 52;
			gweaponItemInfo.mMoveSpeed = 4.5f;
			gweaponItemInfo.mMinDamage = 25f;
			gweaponItemInfo.mMaxDamage = 40f;
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mSellable = false;
			gweaponItemInfo.mIsCollection = true;
			gweaponItemInfo.mNoLimitedUsePrice = 200;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Move",
				"Clip"
			});
			gweaponItemInfo.DeployChipsStates();
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Special weapon, Collect chips by hunting to active it!";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Special weapon, Collect chips by hunting to active it!";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 3.75f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 10f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.3f);
				gweaponItemInfo.mFeatureDescription = "Special weapon, Collect chips by hunting to active it!";
				break;
			}
			goto IL_8ACD;
		case "Assault":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Assault";
			gweaponItemInfo.mWeaponId = 53;
			gweaponItemInfo.mMoveSpeed = 6.25f;
			gweaponItemInfo.mMinDamage = 15f;
			gweaponItemInfo.mMaxDamage = 25f;
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mSellable = false;
			gweaponItemInfo.mIsCollection = true;
			gweaponItemInfo.mNoLimitedUsePrice = 200;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			gweaponItemInfo.DeployChipsStates();
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "Special weapon, Collect chips by hunting to active it!";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "Special weapon, Collect chips by hunting to active it!";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 8.43f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 7.16f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "Special weapon, Collect chips by hunting to active it!";
				break;
			}
			goto IL_8ACD;
		case "ShadowSnake":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Shadow Snake";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 56;
			gweaponItemInfo.mMoveSpeed = 6.35f;
			gweaponItemInfo.mMinDamage = 35f;
			gweaponItemInfo.mMaxDamage = 56f;
			gweaponItemInfo.mNoLimitedUsePrice = 50;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5.1f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "Hide in shadow.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5.1f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "Hide in shadow.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 5.1f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "Hide in shadow.";
				break;
			}
			goto IL_8ACD;
		case "Digger":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Digger";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 57;
			gweaponItemInfo.mMoveSpeed = 4.75f;
			gweaponItemInfo.mMinDamage = 18f;
			gweaponItemInfo.mMaxDamage = 34f;
			gweaponItemInfo.mNoLimitedUsePrice = 105;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			gweaponItemInfo.DeployChipsStates();
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.8f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Destroy the defense effectively.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.8f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Destroy the defense effectively.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.8f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.7f);
				gweaponItemInfo.mFeatureDescription = "Destroy the defense effectively.";
				break;
			}
			goto IL_8ACD;
		case "Nightmare":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Nightmare";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 58;
			gweaponItemInfo.mMoveSpeed = 5f;
			gweaponItemInfo.mMinDamage = 18f;
			gweaponItemInfo.mMaxDamage = 24f;
			gweaponItemInfo.mNoLimitedUsePrice = 105;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			gweaponItemInfo.DeployChipsStates();
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.1f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "Make enemy into nightmare.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.1f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "Make enemy into nightmare.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.1f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.13f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "Make enemy into nightmare.";
				break;
			}
			goto IL_8ACD;
		case "SweetMemory":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Sweet Memory";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 59;
			gweaponItemInfo.mMoveSpeed = 6f;
			gweaponItemInfo.mMinDamage = 40f;
			gweaponItemInfo.mMaxDamage = 55f;
			gweaponItemInfo.mNoLimitedUsePrice = 90;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.2f);
				gweaponItemInfo.mFeatureDescription = "A stick full of candy, sweet when beat.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5.3f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.2f);
				gweaponItemInfo.mFeatureDescription = "A stick full of candy, sweet when beat.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 5.3f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.2f);
				gweaponItemInfo.mFeatureDescription = "A stick full of candy, sweet when beat.";
				break;
			}
			goto IL_8ACD;
		case "Shark":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Shark";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 60;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mMinDamage = 20f;
			gweaponItemInfo.mMaxDamage = 31f;
			gweaponItemInfo.mNoLimitedUsePrice = 90;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 1.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "The handle covered with shark's skin, comfortable but fierce.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.5f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 1.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "The handle covered with shark's skin, comfortable but fierce.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.5f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 1.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 7f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7f);
				gweaponItemInfo.mFeatureDescription = "The handle covered with shark's skin, comfortable but fierce.";
				break;
			}
			goto IL_8ACD;
		case "DeathHunter":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 8 : 0);
			gweaponItemInfo.mNameDisplay = "Death Hunter";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 61;
			gweaponItemInfo.mMoveSpeed = 4.6f;
			gweaponItemInfo.mMinDamage = 98f;
			gweaponItemInfo.mMaxDamage = 125f;
			gweaponItemInfo.mNoLimitedUsePrice = 120;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Aim"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 9.8f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.25f);
				gweaponItemInfo.AddNDeployCurProperty("Aim", 2.8f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.5f);
				gweaponItemInfo.mFeatureDescription = "High firerate sniper, able to shoot 5 times fast!";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 9.8f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.25f);
				gweaponItemInfo.AddNDeployCurProperty("Aim", 2.8f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.5f);
				gweaponItemInfo.mFeatureDescription = "High firerate sniper, able to shoot 5 times fast!";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 9.8f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.25f);
				gweaponItemInfo.AddNDeployNextProperty("Aim", 2.8f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.5f);
				gweaponItemInfo.mFeatureDescription = "High firerate sniper, able to shoot 5 times fast!";
				break;
			}
			goto IL_8ACD;
		case "NuclearEmitter":
			gweaponItemInfo.mClipPrice = 15;
			gweaponItemInfo.mNameDisplay = "Nuclear Emitter";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 62;
			gweaponItemInfo.mMoveSpeed = 4.5f;
			gweaponItemInfo.mNoLimitedUsePrice = 250;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Range"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 1f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.2f);
				gweaponItemInfo.mFeatureDescription = "Great power and area damage, light but fire at close range.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 3f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployCurProperty("Range", 1f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.2f);
				gweaponItemInfo.mFeatureDescription = "Great power and area damage, light but fire at close range.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 7f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 3f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 0.33f);
				gweaponItemInfo.AddNDeployNextProperty("Range", 1f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.2f);
				gweaponItemInfo.mFeatureDescription = "Great power and area damage, light but fire at close range.";
				break;
			}
			goto IL_8ACD;
		case "Flower":
			gweaponItemInfo.mClipPrice = -1;
			gweaponItemInfo.mNameDisplay = "Flower";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 63;
			gweaponItemInfo.mMoveSpeed = 6.3f;
			gweaponItemInfo.mMinDamage = 40f;
			gweaponItemInfo.mMaxDamage = 50f;
			gweaponItemInfo.mNoLimitedUsePrice = 100;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "[Valentine's day]\nBlooming roses, sending out romantic flavor.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "[Valentine's day]\nBlooming roses, sending out romantic flavor.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.5f);
				gweaponItemInfo.mFeatureDescription = "[Valentine's day]\nBlooming roses, sending out romantic flavor.";
				break;
			}
			goto IL_8ACD;
		case "Flamethrower":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Flamethrower";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
			gweaponItemInfo.mWeaponId = 64;
			gweaponItemInfo.mMoveSpeed = 5.8f;
			gweaponItemInfo.mMinDamage = 24f;
			gweaponItemInfo.mMaxDamage = 30f;
			gweaponItemInfo.mNoLimitedUsePrice = 120;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Energy"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.8f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.6f);
				gweaponItemInfo.AddNDeployCurProperty("Energy", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "[Valentine's day]\nLove like flame, too hot to breathe.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.8f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 6.6f);
				gweaponItemInfo.AddNDeployCurProperty("Energy", 6f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 10f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "[Valentine's day]\nLove like flame, too hot to breathe.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.8f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 6.6f);
				gweaponItemInfo.AddNDeployNextProperty("Energy", 6f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 10f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 6f);
				gweaponItemInfo.mFeatureDescription = "[Valentine's day]\nLove like flame, too hot to breathe.";
				break;
			}
			goto IL_8ACD;
		case "AlienRifle":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Alien Rifle";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 65;
			gweaponItemInfo.mMoveSpeed = 5.5f;
			gweaponItemInfo.mMinDamage = 20f;
			gweaponItemInfo.mMaxDamage = 34f;
			gweaponItemInfo.mNoLimitedUsePrice = 110;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.8f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.5f);
				gweaponItemInfo.mFeatureDescription = "High damage, widely used.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.8f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.5f);
				gweaponItemInfo.mFeatureDescription = "High damage, widely used.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.7f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.55f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 5.8f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.5f);
				gweaponItemInfo.mFeatureDescription = "High damage, widely used.";
				break;
			}
			goto IL_8ACD;
		case "AlienSMG":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Alien SMG";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 66;
			gweaponItemInfo.mMoveSpeed = 7.2f;
			gweaponItemInfo.mMinDamage = 12f;
			gweaponItemInfo.mMaxDamage = 22f;
			gweaponItemInfo.mNoLimitedUsePrice = 90;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.22f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.22f);
				gweaponItemInfo.mFeatureDescription = "High firerate, move fast.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 1.7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 8.22f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 7f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 7.22f);
				gweaponItemInfo.mFeatureDescription = "High firerate, move fast.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 1.7f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 8.22f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 4f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 7f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 7.22f);
				gweaponItemInfo.mFeatureDescription = "High firerate, move fast.";
				break;
			}
			goto IL_8ACD;
		case "HeroRifle":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Hero Rifle";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 67;
			gweaponItemInfo.mMoveSpeed = 5.4f;
			gweaponItemInfo.mMinDamage = 24f;
			gweaponItemInfo.mMaxDamage = 30f;
			gweaponItemInfo.mNoLimitedUsePrice = 115;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.73f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.4f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.4f);
				gweaponItemInfo.mFeatureDescription = "Balanced performance, stable damage.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 2.7f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.73f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 5.4f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5.4f);
				gweaponItemInfo.mFeatureDescription = "Balanced performance, stable damage.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 2.7f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.73f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 3.33f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 5.4f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5.4f);
				gweaponItemInfo.mFeatureDescription = "Balanced performance, stable damage.";
				break;
			}
			goto IL_8ACD;
		case "RoyaleGun":
			gweaponItemInfo.mClipPrice = ((UserDataController.GetCharacterLevel() >= 5) ? 3 : 0);
			gweaponItemInfo.mNameDisplay = "Royale Gun";
			gweaponItemInfo.mOffRate = 100;
			gweaponItemInfo.mOffRateDescription = string.Empty;
			gweaponItemInfo.mWeaponId = 68;
			gweaponItemInfo.mMoveSpeed = 5f;
			gweaponItemInfo.mMinDamage = 35f;
			gweaponItemInfo.mMaxDamage = 49f;
			gweaponItemInfo.mNoLimitedUsePrice = 130;
			gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
			gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
			gweaponItemInfo.DeployPropertyUpgradeConfig(new string[]
			{
				"Power",
				"Accuracy",
				"Clip"
			});
			switch (num)
			{
			case 0:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4.22f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.25f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.66f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 2.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, strong endurance, great power.";
				break;
			case 1:
				gweaponItemInfo.AddNDeployCurProperty("Power", 4.22f);
				gweaponItemInfo.AddNDeployCurProperty("Fire Rate", 5.25f);
				gweaponItemInfo.AddNDeployCurProperty("Clip", 6.66f);
				gweaponItemInfo.AddNDeployCurProperty("Accuracy", 2.5f);
				gweaponItemInfo.AddNDeployCurProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, strong endurance, great power.";
				break;
			case 2:
				gweaponItemInfo.AddNDeployNextProperty("Power", 4.22f);
				gweaponItemInfo.AddNDeployNextProperty("Fire Rate", 5.25f);
				gweaponItemInfo.AddNDeployNextProperty("Clip", 6.66f);
				gweaponItemInfo.AddNDeployNextProperty("Accuracy", 2.5f);
				gweaponItemInfo.AddNDeployNextProperty("Move", 5f);
				gweaponItemInfo.mFeatureDescription = "High fire rate, strong endurance, great power.";
				break;
			}
			goto IL_8ACD;
		}
		gweaponItemInfo.mClipPrice = 0;
		gweaponItemInfo.mNameDisplay = "Undefined";
		gweaponItemInfo.mOffRate = 100;
		gweaponItemInfo.mOffRateDescription = string.Empty;
		gweaponItemInfo.mIsOnlyUnlimitedBuy = true;
		gweaponItemInfo.mCanUpgrade = false;
		gweaponItemInfo.mNoLimitedUsePrice = 30;
		gweaponItemInfo.mSinglePriceOfTimeFill = gweaponItemInfo.mNoLimitedUsePrice * 1000 * 6 / 5 / 15 / 72 / 10 * 10;
		gweaponItemInfo.mSinglePriceOfPlusTimeFill = ((gweaponItemInfo.mSinglePriceOfTimeFill >= 66) ? (gweaponItemInfo.mSinglePriceOfTimeFill / 66) : 1);
		switch (num)
		{
		case 0:
			gweaponItemInfo.AddNDeployCurProperty("Power", 5f);
			gweaponItemInfo.AddNDeployCurProperty("Move", 7.8f);
			gweaponItemInfo.mFeatureDescription = "Undefined.";
			break;
		case 1:
			gweaponItemInfo.AddNDeployCurProperty("Power", 5f);
			gweaponItemInfo.AddNDeployCurProperty("Move", 7.8f);
			gweaponItemInfo.mFeatureDescription = "Undefined.";
			break;
		case 2:
			gweaponItemInfo.AddNDeployCurProperty("Power", 5f);
			gweaponItemInfo.AddNDeployCurProperty("Move", 7.8f);
			gweaponItemInfo.mFeatureDescription = "Undefined.";
			break;
		}
		gweaponItemInfo.mFeatureDescription = string.Empty;
		IL_8ACD:
		gweaponItemInfo.mOffRateDescription = gweaponItemInfo.mOffRateDescription.ToUpper();
		gweaponItemInfo.mNameDisplay = gweaponItemInfo.mNameDisplay.ToUpper();
		gweaponItemInfo.mFeatureDescription = gweaponItemInfo.mFeatureDescription.ToUpper();
		gweaponItemInfo.mPurchaseTipsText = gweaponItemInfo.mPurchaseTipsText.ToUpper();
		return gweaponItemInfo;
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0006B2BF File Offset: 0x000696BF
	public static void SetCurLoginDateTime(string newValue)
	{
		GOGPlayerPrefabs.SetString("CurrentLoginDateTime", newValue);
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0006B2CC File Offset: 0x000696CC
	public static string GetCurLoginDateTime()
	{
		return GOGPlayerPrefabs.GetString("CurrentLoginDateTime", "19881001000000");
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0006B2DD File Offset: 0x000696DD
	public static void SetPreLoginDateTime(string newValue)
	{
		GOGPlayerPrefabs.SetString("PreLoginDateTime", newValue);
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x0006B2EA File Offset: 0x000696EA
	public static string GetPreLoginDateTime()
	{
		return GOGPlayerPrefabs.GetString("PreLoginDateTime", "19881001000000");
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0006B2FC File Offset: 0x000696FC
	public static void SetFightingStatisticsValue(FightingStatisticsTag tag, int newValue)
	{
		switch (tag)
		{
		case FightingStatisticsTag.tTotalKillInWorldwideMultiplayer:
			GOGPlayerPrefabs.SetInt("WorldwideMultiplayerKill", newValue);
			break;
		case FightingStatisticsTag.tTotalKillInLocalWifiMultiplayer:
			GOGPlayerPrefabs.SetInt("LocalMultiplayerKill", newValue);
			break;
		case FightingStatisticsTag.tTotalHeadshotKill:
			GOGPlayerPrefabs.SetInt("TotalHeadshotKill", newValue);
			break;
		case FightingStatisticsTag.tTotalTwoKill:
			GOGPlayerPrefabs.SetInt("TotalTwoKill", newValue);
			break;
		case FightingStatisticsTag.tTotalFourKill:
			GOGPlayerPrefabs.SetInt("TotalFourKill", newValue);
			break;
		case FightingStatisticsTag.tTotalSixKill:
			GOGPlayerPrefabs.SetInt("TotalSixKill", newValue);
			break;
		case FightingStatisticsTag.tTotalEightKill:
			GOGPlayerPrefabs.SetInt("TotalEightKill", newValue);
			break;
		case FightingStatisticsTag.tTotalGoldLikeKill:
			GOGPlayerPrefabs.SetInt("TotalGoldLikeKill", newValue);
			break;
		case FightingStatisticsTag.tMaxDeadOneRound:
			GOGPlayerPrefabs.SetInt("MaxDeadOneRound", newValue);
			break;
		case FightingStatisticsTag.tTotalStrongholdModeVictory:
			GOGPlayerPrefabs.SetInt("TotalStrongholdModeVictory", newValue);
			break;
		case FightingStatisticsTag.tLevelUp:
			break;
		case FightingStatisticsTag.tTotalKillingCompetitionModeVictory:
			GOGPlayerPrefabs.SetInt("TotalKillingCompetitionModeVictory", newValue);
			break;
		case FightingStatisticsTag.tTotalExplosionModeVictory:
			GOGPlayerPrefabs.SetInt("TotalExplosionModeVictory", newValue);
			break;
		case FightingStatisticsTag.tTotalMutationModeVictory:
			GOGPlayerPrefabs.SetInt("TotalMutationModeVictory", newValue);
			break;
		case FightingStatisticsTag.tLvGrowthGift:
			break;
		default:
			switch (tag)
			{
			case FightingStatisticsTag.tDailyKillInDeathMatchMode:
				GOGPlayerPrefabs.SetInt("DailyKillInDeathMatchMode", newValue);
				break;
			case FightingStatisticsTag.tDailyJoinInStrongholdMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInStrongholdMode", newValue);
				break;
			case FightingStatisticsTag.tDailyJoinInKillingCompetitionMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInKillingCompetitionMode", newValue);
				break;
			case FightingStatisticsTag.tDailyJoinInExplosionMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInExplosionMode", newValue);
				break;
			case FightingStatisticsTag.tDailyJoinInMutationMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInMutationMode", newValue);
				break;
			default:
				switch (tag)
				{
				case FightingStatisticsTag.tTotalKillingCompetitionModeJoin:
					GOGPlayerPrefabs.SetInt("TotalKillingCompetitionModeJoin", newValue);
					break;
				case FightingStatisticsTag.tTotalExplosionModeJoin:
					GOGPlayerPrefabs.SetInt("TotalExplosionModeJoin", newValue);
					break;
				case FightingStatisticsTag.tTotalStrongholdModeJoin:
					GOGPlayerPrefabs.SetInt("TotalStrongholdModeJoin", newValue);
					break;
				default:
					switch (tag)
					{
					case FightingStatisticsTag.tTotalKillingCompetitionModeMvp:
						GOGPlayerPrefabs.SetInt("TotalKillingCompetitionModeMvp", newValue);
						break;
					case FightingStatisticsTag.tTotalExplosionModeMvp:
						GOGPlayerPrefabs.SetInt("TotalExplosionModeMvp", newValue);
						break;
					case FightingStatisticsTag.tTotalStrongholdModeMvp:
						GOGPlayerPrefabs.SetInt("TotalStrongholdModeMvp", newValue);
						break;
					default:
						switch (tag)
						{
						case FightingStatisticsTag.tTotalKillingCompetitionModeJoinSeason:
							GOGPlayerPrefabs.SetInt("TotalKillingCompetitionModeJoinSeason", newValue);
							break;
						case FightingStatisticsTag.tTotalExplosionModeJoinSeason:
							GOGPlayerPrefabs.SetInt("TotalExplosionModeJoinSeason", newValue);
							break;
						case FightingStatisticsTag.tTotalStrongholdModeJoinSeason:
							GOGPlayerPrefabs.SetInt("TotalStrongholdModeJoinSeason", newValue);
							break;
						default:
							switch (tag)
							{
							case FightingStatisticsTag.tTotalKillingCompetitionModeVictorySeason:
								GOGPlayerPrefabs.SetInt("TotalKillingCompetitionModeVictorySeason", newValue);
								break;
							case FightingStatisticsTag.tTotalExplosionModeVictorySeason:
								GOGPlayerPrefabs.SetInt("TotalExplosionModeVictorySeason", newValue);
								break;
							case FightingStatisticsTag.tTotalStrongholdModeVictorySeason:
								GOGPlayerPrefabs.SetInt("TotalStrongholdModeVictorySeason", newValue);
								break;
							default:
								switch (tag)
								{
								case FightingStatisticsTag.tTotalKillingCompetitionModeMvpSeason:
									GOGPlayerPrefabs.SetInt("TotalKillingCompetitionModeMvpSeason", newValue);
									break;
								case FightingStatisticsTag.tTotalExplosionModeMvpSeason:
									GOGPlayerPrefabs.SetInt("TotalExplosionModeMvpSeason", newValue);
									break;
								case FightingStatisticsTag.tTotalStrongholdModeMvpSeason:
									GOGPlayerPrefabs.SetInt("TotalStrongholdModeMvpSeason", newValue);
									break;
								default:
									switch (tag)
									{
									case FightingStatisticsTag.tTotalKillInWorldwideMultiplayerSeason:
										GOGPlayerPrefabs.SetInt("TotalKillInWorldwideMultiplayerSeason", newValue);
										break;
									case FightingStatisticsTag.tTotalHeadshotKillSeason:
										GOGPlayerPrefabs.SetInt("TotalHeadshotKillSeason", newValue);
										break;
									case FightingStatisticsTag.tTotalGoldLikeKillSeason:
										GOGPlayerPrefabs.SetInt("TotalGoldLikeKillSeason", newValue);
										break;
									default:
										if (tag != FightingStatisticsTag.tSeasonScore)
										{
											if (tag != FightingStatisticsTag.tSeasonRank)
											{
												if (tag != FightingStatisticsTag.tDailyLoginInSevenDays)
												{
													if (tag == FightingStatisticsTag.tDailyVideoShare)
													{
														GOGPlayerPrefabs.SetInt("DailyVideoShare", newValue);
													}
												}
												else
												{
													GOGPlayerPrefabs.SetInt("DailyLoginInSevenDays", newValue);
												}
											}
											else
											{
												GOGPlayerPrefabs.SetInt("SeasonRank", newValue);
											}
										}
										else
										{
											GOGPlayerPrefabs.SetInt("SeasonScore", newValue);
										}
										break;
									}
									break;
								}
								break;
							}
							break;
						}
						break;
					}
					break;
				}
				break;
			}
			break;
		}
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0006B6A8 File Offset: 0x00069AA8
	public static int GetFightingStatisticsValue(FightingStatisticsTag tag)
	{
		int result = 0;
		switch (tag)
		{
		case FightingStatisticsTag.tTotalKillInWorldwideMultiplayer:
			result = GOGPlayerPrefabs.GetInt("WorldwideMultiplayerKill", 0);
			break;
		case FightingStatisticsTag.tTotalKillInLocalWifiMultiplayer:
			result = GOGPlayerPrefabs.GetInt("LocalMultiplayerKill", 0);
			break;
		case FightingStatisticsTag.tTotalHeadshotKill:
			result = GOGPlayerPrefabs.GetInt("TotalHeadshotKill", 0);
			break;
		case FightingStatisticsTag.tTotalTwoKill:
			result = GOGPlayerPrefabs.GetInt("TotalTwoKill", 0);
			break;
		case FightingStatisticsTag.tTotalFourKill:
			result = GOGPlayerPrefabs.GetInt("TotalFourKill", 0);
			break;
		case FightingStatisticsTag.tTotalSixKill:
			result = GOGPlayerPrefabs.GetInt("TotalSixKill", 0);
			break;
		case FightingStatisticsTag.tTotalEightKill:
			result = GOGPlayerPrefabs.GetInt("TotalEightKill", 0);
			break;
		case FightingStatisticsTag.tTotalGoldLikeKill:
			result = GOGPlayerPrefabs.GetInt("TotalGoldLikeKill", 0);
			break;
		case FightingStatisticsTag.tMaxDeadOneRound:
			result = GOGPlayerPrefabs.GetInt("MaxDeadOneRound", 0);
			break;
		case FightingStatisticsTag.tTotalStrongholdModeVictory:
			result = GOGPlayerPrefabs.GetInt("TotalStrongholdModeVictory", 0);
			break;
		case FightingStatisticsTag.tLevelUp:
			result = UserDataController.GetCharacterLevel();
			break;
		case FightingStatisticsTag.tTotalKillingCompetitionModeVictory:
			result = GOGPlayerPrefabs.GetInt("TotalKillingCompetitionModeVictory", 0);
			break;
		case FightingStatisticsTag.tTotalExplosionModeVictory:
			result = GOGPlayerPrefabs.GetInt("TotalExplosionModeVictory", 0);
			break;
		case FightingStatisticsTag.tTotalMutationModeVictory:
			result = GOGPlayerPrefabs.GetInt("TotalMutationModeVictory", 0);
			break;
		case FightingStatisticsTag.tLvGrowthGift:
			result = UserDataController.GetCharacterLevel();
			break;
		default:
			switch (tag)
			{
			case FightingStatisticsTag.tDailyKillInDeathMatchMode:
				result = GOGPlayerPrefabs.GetInt("DailyKillInDeathMatchMode", 0);
				break;
			case FightingStatisticsTag.tDailyJoinInStrongholdMode:
				result = GOGPlayerPrefabs.GetInt("DailyJoinInStrongholdMode", 0);
				break;
			case FightingStatisticsTag.tDailyJoinInKillingCompetitionMode:
				result = GOGPlayerPrefabs.GetInt("DailyJoinInKillingCompetitionMode", 0);
				break;
			case FightingStatisticsTag.tDailyJoinInExplosionMode:
				result = GOGPlayerPrefabs.GetInt("DailyJoinInExplosionMode", 0);
				break;
			case FightingStatisticsTag.tDailyJoinInMutationMode:
				result = GOGPlayerPrefabs.GetInt("DailyJoinInMutationMode", 0);
				break;
			default:
				switch (tag)
				{
				case FightingStatisticsTag.tTotalKillingCompetitionModeJoin:
					result = GOGPlayerPrefabs.GetInt("TotalKillingCompetitionModeJoin", 0);
					break;
				case FightingStatisticsTag.tTotalExplosionModeJoin:
					result = GOGPlayerPrefabs.GetInt("TotalExplosionModeJoin", 0);
					break;
				case FightingStatisticsTag.tTotalStrongholdModeJoin:
					result = GOGPlayerPrefabs.GetInt("TotalStrongholdModeJoin", 0);
					break;
				default:
					switch (tag)
					{
					case FightingStatisticsTag.tTotalKillingCompetitionModeMvp:
						result = GOGPlayerPrefabs.GetInt("TotalKillingCompetitionModeMvp", 0);
						break;
					case FightingStatisticsTag.tTotalExplosionModeMvp:
						result = GOGPlayerPrefabs.GetInt("TotalExplosionModeMvp", 0);
						break;
					case FightingStatisticsTag.tTotalStrongholdModeMvp:
						result = GOGPlayerPrefabs.GetInt("TotalStrongholdModeMvp", 0);
						break;
					default:
						switch (tag)
						{
						case FightingStatisticsTag.tTotalKillingCompetitionModeJoinSeason:
							result = GOGPlayerPrefabs.GetInt("TotalKillingCompetitionModeJoinSeason", 0);
							break;
						case FightingStatisticsTag.tTotalExplosionModeJoinSeason:
							result = GOGPlayerPrefabs.GetInt("TotalExplosionModeJoinSeason", 0);
							break;
						case FightingStatisticsTag.tTotalStrongholdModeJoinSeason:
							result = GOGPlayerPrefabs.GetInt("TotalStrongholdModeJoinSeason", 0);
							break;
						default:
							switch (tag)
							{
							case FightingStatisticsTag.tTotalKillingCompetitionModeVictorySeason:
								result = GOGPlayerPrefabs.GetInt("TotalKillingCompetitionModeVictorySeason", 0);
								break;
							case FightingStatisticsTag.tTotalExplosionModeVictorySeason:
								result = GOGPlayerPrefabs.GetInt("TotalExplosionModeVictorySeason", 0);
								break;
							case FightingStatisticsTag.tTotalStrongholdModeVictorySeason:
								result = GOGPlayerPrefabs.GetInt("TotalStrongholdModeVictorySeason", 0);
								break;
							default:
								switch (tag)
								{
								case FightingStatisticsTag.tTotalKillingCompetitionModeMvpSeason:
									result = GOGPlayerPrefabs.GetInt("TotalKillingCompetitionModeMvpSeason", 0);
									break;
								case FightingStatisticsTag.tTotalExplosionModeMvpSeason:
									result = GOGPlayerPrefabs.GetInt("TotalExplosionModeMvpSeason", 0);
									break;
								case FightingStatisticsTag.tTotalStrongholdModeMvpSeason:
									result = GOGPlayerPrefabs.GetInt("TotalStrongholdModeMvpSeason", 0);
									break;
								default:
									switch (tag)
									{
									case FightingStatisticsTag.tTotalKillInWorldwideMultiplayerSeason:
										result = GOGPlayerPrefabs.GetInt("TotalKillInWorldwideMultiplayerSeason", 0);
										break;
									case FightingStatisticsTag.tTotalHeadshotKillSeason:
										result = GOGPlayerPrefabs.GetInt("TotalHeadshotKillSeason", 0);
										break;
									case FightingStatisticsTag.tTotalGoldLikeKillSeason:
										result = GOGPlayerPrefabs.GetInt("TotalGoldLikeKillSeason", 0);
										break;
									default:
										if (tag != FightingStatisticsTag.tSeasonScore)
										{
											if (tag != FightingStatisticsTag.tSeasonRank)
											{
												if (tag != FightingStatisticsTag.tDailyLoginInSevenDays)
												{
													if (tag == FightingStatisticsTag.tDailyVideoShare)
													{
														result = GOGPlayerPrefabs.GetInt("DailyVideoShare", 0);
													}
												}
												else
												{
													result = GOGPlayerPrefabs.GetInt("DailyLoginInSevenDays", 1);
												}
											}
											else
											{
												result = GOGPlayerPrefabs.GetInt("SeasonRank", 0);
											}
										}
										else
										{
											result = GOGPlayerPrefabs.GetInt("SeasonScore", 0);
										}
										break;
									}
									break;
								}
								break;
							}
							break;
						}
						break;
					}
					break;
				}
				break;
			}
			break;
		}
		return result;
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0006BA8C File Offset: 0x00069E8C
	public static void VerifyFightingStatisticsRewardGot(FightingStatisticsTag tag, int gotLv)
	{
		switch (tag)
		{
		case FightingStatisticsTag.tTotalKillInWorldwideMultiplayer:
			GOGPlayerPrefabs.SetInt("WorldwideMultiplayerKill_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tTotalKillInLocalWifiMultiplayer:
			GOGPlayerPrefabs.SetInt("LocalMultiplayerKill_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tTotalHeadshotKill:
			GOGPlayerPrefabs.SetInt("TotalHeadshotKill_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tTotalTwoKill:
			GOGPlayerPrefabs.SetInt("TotalTwoKill_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tTotalFourKill:
			GOGPlayerPrefabs.SetInt("TotalFourKill_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tTotalSixKill:
			GOGPlayerPrefabs.SetInt("TotalSixKill_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tTotalEightKill:
			GOGPlayerPrefabs.SetInt("TotalEightKill_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tTotalGoldLikeKill:
			GOGPlayerPrefabs.SetInt("TotalGoldLikeKill_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tMaxDeadOneRound:
			GOGPlayerPrefabs.SetInt("MaxDeadOneRound_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tTotalStrongholdModeVictory:
			GOGPlayerPrefabs.SetInt("TotalStrongholdModeVictory_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tLevelUp:
			GOGPlayerPrefabs.SetInt("CharacterLevel_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tTotalKillingCompetitionModeVictory:
			GOGPlayerPrefabs.SetInt("TotalKillingCompetitionModeVictory_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tTotalExplosionModeVictory:
			GOGPlayerPrefabs.SetInt("TotalExplosionModeVictory_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tTotalMutationModeVictory:
			GOGPlayerPrefabs.SetInt("TotalMutationModeVictory_Got_Lv_" + gotLv.ToString(), 1);
			break;
		case FightingStatisticsTag.tLvGrowthGift:
			GOGPlayerPrefabs.SetInt("LvGrowthGift_Got_Lv_" + gotLv.ToString(), 1);
			break;
		default:
			switch (tag)
			{
			case FightingStatisticsTag.tDailyKillInDeathMatchMode:
				GOGPlayerPrefabs.SetInt("DailyKillInDeathMatchMode_Got_Lv_" + gotLv.ToString(), 1);
				break;
			case FightingStatisticsTag.tDailyJoinInStrongholdMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInStrongholdMode_Got_Lv_" + gotLv.ToString(), 1);
				break;
			case FightingStatisticsTag.tDailyJoinInKillingCompetitionMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInKillingCompetitionMode_Got_Lv_" + gotLv.ToString(), 1);
				break;
			case FightingStatisticsTag.tDailyJoinInExplosionMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInExplosionMode_Got_Lv_" + gotLv.ToString(), 1);
				break;
			case FightingStatisticsTag.tDailyJoinInMutationMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInMutationMode_Got_Lv_" + gotLv.ToString(), 1);
				break;
			default:
				if (tag != FightingStatisticsTag.tDailyLoginInSevenDays)
				{
					if (tag == FightingStatisticsTag.tDailyVideoShare)
					{
						GOGPlayerPrefabs.SetInt("DailyVideoShare_Got_Lv_" + gotLv.ToString(), 1);
					}
				}
				else
				{
					GOGPlayerPrefabs.SetInt("DailyLoginInSevenDays_Got_Lv_" + gotLv.ToString(), 1);
				}
				break;
			}
			break;
		}
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0006BE00 File Offset: 0x0006A200
	public static void ClearFightingStatisticsRewardGotRecord(FightingStatisticsTag tag, int gotLv)
	{
		switch (tag)
		{
		case FightingStatisticsTag.tTotalKillInWorldwideMultiplayer:
			GOGPlayerPrefabs.SetInt("WorldwideMultiplayerKill_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tTotalKillInLocalWifiMultiplayer:
			GOGPlayerPrefabs.SetInt("LocalMultiplayerKill_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tTotalHeadshotKill:
			GOGPlayerPrefabs.SetInt("TotalHeadshotKill_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tTotalTwoKill:
			GOGPlayerPrefabs.SetInt("TotalTwoKill_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tTotalFourKill:
			GOGPlayerPrefabs.SetInt("TotalFourKill_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tTotalSixKill:
			GOGPlayerPrefabs.SetInt("TotalSixKill_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tTotalEightKill:
			GOGPlayerPrefabs.SetInt("TotalEightKill_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tTotalGoldLikeKill:
			GOGPlayerPrefabs.SetInt("TotalGoldLikeKill_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tMaxDeadOneRound:
			GOGPlayerPrefabs.SetInt("MaxDeadOneRound_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tTotalStrongholdModeVictory:
			GOGPlayerPrefabs.SetInt("TotalStrongholdModeVictory_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tLevelUp:
			GOGPlayerPrefabs.SetInt("CharacterLevel_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tTotalKillingCompetitionModeVictory:
			GOGPlayerPrefabs.SetInt("TotalKillingCompetitionModeVictory_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tTotalExplosionModeVictory:
			GOGPlayerPrefabs.SetInt("TotalExplosionModeVictory_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tTotalMutationModeVictory:
			GOGPlayerPrefabs.SetInt("TotalMutationModeVictory_Got_Lv_" + gotLv.ToString(), 0);
			break;
		case FightingStatisticsTag.tLvGrowthGift:
			GOGPlayerPrefabs.SetInt("LvGrowthGift_Got_Lv_" + gotLv.ToString(), 0);
			break;
		default:
			switch (tag)
			{
			case FightingStatisticsTag.tDailyKillInDeathMatchMode:
				GOGPlayerPrefabs.SetInt("DailyKillInDeathMatchMode_Got_Lv_" + gotLv.ToString(), 0);
				break;
			case FightingStatisticsTag.tDailyJoinInStrongholdMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInStrongholdMode_Got_Lv_" + gotLv.ToString(), 0);
				break;
			case FightingStatisticsTag.tDailyJoinInKillingCompetitionMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInKillingCompetitionMode_Got_Lv_" + gotLv.ToString(), 0);
				break;
			case FightingStatisticsTag.tDailyJoinInExplosionMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInExplosionMode_Got_Lv_" + gotLv.ToString(), 0);
				break;
			case FightingStatisticsTag.tDailyJoinInMutationMode:
				GOGPlayerPrefabs.SetInt("DailyJoinInMutationMode_Got_Lv_" + gotLv.ToString(), 0);
				break;
			default:
				if (tag != FightingStatisticsTag.tDailyLoginInSevenDays)
				{
					if (tag == FightingStatisticsTag.tDailyVideoShare)
					{
						GOGPlayerPrefabs.SetInt("DailyVideoShare_Got_Lv_" + gotLv.ToString(), 0);
					}
				}
				else
				{
					GOGPlayerPrefabs.SetInt("DailyLoginInSevenDays_Got_Lv_" + gotLv.ToString(), 0);
				}
				break;
			}
			break;
		}
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0006C174 File Offset: 0x0006A574
	public static bool HasEverGotFightingStatisticsReward(FightingStatisticsTag tag, int gotLv)
	{
		bool result = false;
		switch (tag)
		{
		case FightingStatisticsTag.tTotalKillInWorldwideMultiplayer:
			result = !GOGPlayerPrefabs.GetInt("WorldwideMultiplayerKill_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tTotalKillInLocalWifiMultiplayer:
			result = !GOGPlayerPrefabs.GetInt("LocalMultiplayerKill_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tTotalHeadshotKill:
			result = !GOGPlayerPrefabs.GetInt("TotalHeadshotKill_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tTotalTwoKill:
			result = !GOGPlayerPrefabs.GetInt("TotalTwoKill_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tTotalFourKill:
			result = !GOGPlayerPrefabs.GetInt("TotalFourKill_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tTotalSixKill:
			result = !GOGPlayerPrefabs.GetInt("TotalSixKill_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tTotalEightKill:
			result = !GOGPlayerPrefabs.GetInt("TotalEightKill_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tTotalGoldLikeKill:
			result = !GOGPlayerPrefabs.GetInt("TotalGoldLikeKill_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tMaxDeadOneRound:
			result = !GOGPlayerPrefabs.GetInt("MaxDeadOneRound_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tTotalStrongholdModeVictory:
			result = !GOGPlayerPrefabs.GetInt("TotalStrongholdModeVictory_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tLevelUp:
			result = !GOGPlayerPrefabs.GetInt("CharacterLevel_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tTotalKillingCompetitionModeVictory:
			result = !GOGPlayerPrefabs.GetInt("TotalKillingCompetitionModeVictory_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tTotalExplosionModeVictory:
			result = !GOGPlayerPrefabs.GetInt("TotalExplosionModeVictory_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tTotalMutationModeVictory:
			result = !GOGPlayerPrefabs.GetInt("TotalMutationModeVictory_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		case FightingStatisticsTag.tLvGrowthGift:
			result = !GOGPlayerPrefabs.GetInt("LvGrowthGift_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
			break;
		default:
			switch (tag)
			{
			case FightingStatisticsTag.tDailyKillInDeathMatchMode:
				result = !GOGPlayerPrefabs.GetInt("DailyKillInDeathMatchMode_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
				break;
			case FightingStatisticsTag.tDailyJoinInStrongholdMode:
				result = !GOGPlayerPrefabs.GetInt("DailyJoinInStrongholdMode_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
				break;
			case FightingStatisticsTag.tDailyJoinInKillingCompetitionMode:
				result = !GOGPlayerPrefabs.GetInt("DailyJoinInKillingCompetitionMode_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
				break;
			case FightingStatisticsTag.tDailyJoinInExplosionMode:
				result = !GOGPlayerPrefabs.GetInt("DailyJoinInExplosionMode_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
				break;
			case FightingStatisticsTag.tDailyJoinInMutationMode:
				result = !GOGPlayerPrefabs.GetInt("DailyJoinInMutationMode_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
				break;
			default:
				if (tag != FightingStatisticsTag.tDailyLoginInSevenDays)
				{
					if (tag == FightingStatisticsTag.tDailyVideoShare)
					{
						result = !GOGPlayerPrefabs.GetInt("DailyVideoShare_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
					}
				}
				else
				{
					result = !GOGPlayerPrefabs.GetInt("DailyLoginInSevenDays_Got_Lv_" + gotLv.ToString(), 0).Equals(0);
				}
				break;
			}
			break;
		}
		return result;
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x0006C61C File Offset: 0x0006AA1C
	public static MsgType GetChatMessageSendType()
	{
		return (MsgType)GOGPlayerPrefabs.GetInt("ChatMessageSendType", 1);
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0006C629 File Offset: 0x0006AA29
	public static void SwitchChatMessageSendType()
	{
		if (GOGPlayerPrefabs.GetInt("ChatMessageSendType", 1) == 1)
		{
			GOGPlayerPrefabs.SetInt("ChatMessageSendType", 2);
		}
		else
		{
			GOGPlayerPrefabs.SetInt("ChatMessageSendType", 1);
		}
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0006C658 File Offset: 0x0006AA58
	public static bool IsSystemMessageInChatClosed()
	{
		return GOGPlayerPrefabs.GetInt("IsCloseSystemMessageInChat", 1).Equals(0);
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x0006C679 File Offset: 0x0006AA79
	public static void SwitchSystemMessageInChat()
	{
		if (GOGPlayerPrefabs.GetInt("IsCloseSystemMessageInChat", 1) == 1)
		{
			GOGPlayerPrefabs.SetInt("IsCloseSystemMessageInChat", 0);
		}
		else
		{
			GOGPlayerPrefabs.SetInt("IsCloseSystemMessageInChat", 1);
		}
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0006C6A7 File Offset: 0x0006AAA7
	public static MsgType GetChatMessageReceiveType()
	{
		return (MsgType)GOGPlayerPrefabs.GetInt("ChatMessageReceiveType", 1);
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0006C6B4 File Offset: 0x0006AAB4
	public static void SwitchChatMessageReceiveType()
	{
		if (GOGPlayerPrefabs.GetInt("ChatMessageReceiveType", 1) == 1)
		{
			GOGPlayerPrefabs.SetInt("ChatMessageReceiveType", 2);
		}
		else
		{
			GOGPlayerPrefabs.SetInt("ChatMessageReceiveType", 1);
		}
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0006C6E4 File Offset: 0x0006AAE4
	public static void SetMyNickName(string name)
	{
		if (name.Length > 16)
		{
			name = name.Remove(16);
		}
		string[] array = name.Split(new char[]
		{
			'_'
		});
		if (array.Length > 0)
		{
			name = string.Empty;
			for (int i = 0; i < array.Length; i++)
			{
				name += array[i];
			}
		}
		GOGPlayerPrefabs.SetString("LocalMultiplayerNickName", name);
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0006C754 File Offset: 0x0006AB54
	public static string GetMyNickName()
	{
		return GOGPlayerPrefabs.GetString("LocalMultiplayerNickName", "New Player");
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x0006C765 File Offset: 0x0006AB65
	public static void SetMyRoomName(string name)
	{
		if (name.Length > 27)
		{
			name = name.Remove(27);
		}
		GOGPlayerPrefabs.SetString("MultiplayerRoomName", name);
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0006C789 File Offset: 0x0006AB89
	public static string GetMyRoomName()
	{
		return GOGPlayerPrefabs.GetString("MultiplayerRoomName", string.Empty);
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x0006C79A File Offset: 0x0006AB9A
	public static int GetMyRoomNumberSetting()
	{
		return GOGPlayerPrefabs.GetInt("MultiplayerRoomNumberSetting", 8);
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0006C7A7 File Offset: 0x0006ABA7
	public static void SetMyRoomNumberSetting(int num)
	{
		if (num > 12)
		{
			num = 12;
		}
		if (num < 2)
		{
			num = 2;
		}
		GOGPlayerPrefabs.SetInt("MultiplayerRoomNumberSetting", num);
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0006C7CA File Offset: 0x0006ABCA
	public static string GetStrVersion()
	{
		return UserDataController.CNR_CUR_VERSION;
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0006C7D1 File Offset: 0x0006ABD1
	public static int GetCharacterLevel()
	{
		return GOGPlayerPrefabs.GetInt("CharacterLevel", 1);
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x0006C7DE File Offset: 0x0006ABDE
	public static void SetCharacterLevel(int level)
	{
		GOGPlayerPrefabs.SetInt("CharacterLevel", level);
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x0006C7EB File Offset: 0x0006ABEB
	public static int GetCharacterExp()
	{
		return GOGPlayerPrefabs.GetInt("CharacterExp", 0);
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x0006C7F8 File Offset: 0x0006ABF8
	public static void SetCharacterExp(int exp)
	{
		GOGPlayerPrefabs.SetInt("CharacterExp", exp);
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x0006C805 File Offset: 0x0006AC05
	public static void AddCharacterExp(int expAdd)
	{
		GOGPlayerPrefabs.SetInt("CharacterExp", UserDataController.GetCharacterExp() + expAdd);
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x0006C818 File Offset: 0x0006AC18
	public static int GetHonorPoint()
	{
		return GOGPlayerPrefabs.GetInt("HonorPoint", 0);
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x0006C825 File Offset: 0x0006AC25
	public static void SetHonorPoint(int newValue)
	{
		GOGPlayerPrefabs.SetInt("HonorPoint", newValue);
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0006C832 File Offset: 0x0006AC32
	public static void AddHonorPoint(int addNum)
	{
		GOGPlayerPrefabs.SetInt("HonorPoint", UserDataController.GetHonorPoint() + addNum);
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0006C845 File Offset: 0x0006AC45
	public static void SubHonorPoint(int subNum)
	{
		if (subNum < 0)
		{
			return;
		}
		GOGPlayerPrefabs.SetInt("HonorPoint", UserDataController.GetHonorPoint() - subNum);
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0006C860 File Offset: 0x0006AC60
	public static int GetCoins()
	{
		return GOGPlayerPrefabs.GetInt("GameCoins", 0);
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x0006C86D File Offset: 0x0006AC6D
	public static void SetCoins(int coins)
	{
		GOGPlayerPrefabs.SetInt("GameCoins", coins);
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x0006C87A File Offset: 0x0006AC7A
	public static void AddCoins(int coinsAdd)
	{
		GOGPlayerPrefabs.SetInt("GameCoins", UserDataController.GetCoins() + coinsAdd);
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x0006C88D File Offset: 0x0006AC8D
	public static void SubCoins(int coinsSub)
	{
		if (coinsSub < 0)
		{
			return;
		}
		GOGPlayerPrefabs.SetInt("GameCoins", UserDataController.GetCoins() - coinsSub);
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x0006C8A8 File Offset: 0x0006ACA8
	public static int GetGems()
	{
		return GOGPlayerPrefabs.GetInt("GameGems", 0);
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x0006C8B5 File Offset: 0x0006ACB5
	public static void SetGems(int gems)
	{
		GOGPlayerPrefabs.SetInt("GameGems", gems);
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0006C8C4 File Offset: 0x0006ACC4
	public static void AddGems(int gemsAdd)
	{
		int newValue = GOGPlayerPrefabs.GetInt("GameGems", 0) + gemsAdd;
		GOGPlayerPrefabs.SetInt("GameGems", newValue);
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x0006C8EA File Offset: 0x0006ACEA
	public static void SubGems(int gemsSub)
	{
		if (gemsSub < 0)
		{
			return;
		}
		GOGPlayerPrefabs.SetInt("GameGems", UserDataController.GetGems() - gemsSub);
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x0006C908 File Offset: 0x0006AD08
	public static int GetUnlockPaletteIndex()
	{
		return GOGPlayerPrefabs.GetInt("UnlockPaletteIndex", 2);
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0006C922 File Offset: 0x0006AD22
	public static void SetUnlockPaletteIndex(int paletteIndex)
	{
		GOGPlayerPrefabs.SetInt("UnlockPaletteIndex", paletteIndex);
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0006C930 File Offset: 0x0006AD30
	public static string GetRandomTips()
	{
		int num;
		if (UnityEngine.Random.Range(1, 101) >= 30)
		{
			num = 12;
		}
		else
		{
			num = UnityEngine.Random.Range(2, 12);
		}
		string result = string.Empty;
		switch (num)
		{
		case 1:
			result = "Ammo supply(except RPG) is free if your level is lower than 5.";
			break;
		case 2:
			result = "Choose Weapon & Skin & Hat & Cape and equip them in store scene.";
			break;
		case 3:
			result = "Weapon plus feature will make your weapon more powerful.";
			break;
		case 4:
			result = "You can get all the props in game from casino scene.";
			break;
		case 5:
			result = "Select region nearby to keep low network latency.";
			break;
		case 6:
			result = "Tap the \"+\" sign to add friends when fighting.";
			break;
		case 7:
			result = "Send mail to us to submit bugs.";
			break;
		case 8:
			result = "Continuous login to win more daily reward.";
			break;
		case 9:
			result = "Season statistics is only for competition gameplay.";
			break;
		case 10:
			result = "Season Score: winner's score will increase, loser's score will decrease.";
			break;
		case 11:
			result = "Get honor point in competition gameplay.";
			break;
		case 12:
			result = "Only permanent weapon can upgrade!";
			break;
		default:
			result = "Enjoy yourself!";
			break;
		}
		return result;
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0006CA30 File Offset: 0x0006AE30
	public static bool IsSoundEffectEnabled()
	{
		return GOGPlayerPrefabs.GetInt("IsSoundEffectEnabled", 1).Equals(1);
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0006CA51 File Offset: 0x0006AE51
	public static void SwitchSoundEffectSetting()
	{
		if (GOGPlayerPrefabs.GetInt("IsSoundEffectEnabled", 1) == 1)
		{
			GOGPlayerPrefabs.SetInt("IsSoundEffectEnabled", 0);
		}
		else
		{
			GOGPlayerPrefabs.SetInt("IsSoundEffectEnabled", 1);
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0006CA7F File Offset: 0x0006AE7F
	public static void SetIsShowApp()
	{
		PlayerPrefs.SetInt("IsShowApp_", 1);
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0006CA8C File Offset: 0x0006AE8C
	public static void SetIsCloseApp()
	{
		PlayerPrefs.SetInt("IsShowApp_", 0);
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0006CA9C File Offset: 0x0006AE9C
	public static bool IsShowApp()
	{
		return PlayerPrefs.GetInt("IsShowApp_", 0).Equals(1);
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0006CABD File Offset: 0x0006AEBD
	public static void SetEveryDayFreeADSDateTime(string newValue)
	{
		GOGPlayerPrefabs.SetString("EveryDayFreeADSDateTime", newValue);
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0006CACA File Offset: 0x0006AECA
	public static string GetEveryDayFreeADSDateTime()
	{
		return GOGPlayerPrefabs.GetString("EveryDayFreeADSDateTime", "19881001000000");
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0006CADB File Offset: 0x0006AEDB
	public static void SetSlotFreeADSDateTime(string newValue)
	{
		GOGPlayerPrefabs.SetString("SlotFreeADSDateTime", newValue);
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0006CAE8 File Offset: 0x0006AEE8
	public static string GetSlotFreeADSDateTime()
	{
		return GOGPlayerPrefabs.GetString("SlotFreeADSDateTime", "19881001000000");
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0006CAF9 File Offset: 0x0006AEF9
	public static void SetEveryDayFreeADSIndex()
	{
		GOGPlayerPrefabs.SetInt("EveryDayFreeADSIndex", UserDataController.GetEveryDayFreeADSIndex() + 1);
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0006CB0C File Offset: 0x0006AF0C
	public static void SetEveryDayFreeADSIndexZero()
	{
		GOGPlayerPrefabs.SetInt("EveryDayFreeADSIndex", 0);
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0006CB19 File Offset: 0x0006AF19
	public static int GetEveryDayFreeADSIndex()
	{
		return GOGPlayerPrefabs.GetInt("EveryDayFreeADSIndex", 0);
	}

	// Token: 0x04000D18 RID: 3352
	public static readonly string[] AllWeaponNameList = new string[]
	{
		"BallisticKnife",
		"DesertEagle",
		"AK47",
		"M4",
		"M87T",
		"AWP",
		"RPG",
		"M67",
		"GLOCK21",
		"MP5KA5",
		"UZI",
		"G36K",
		"M249",
		"MilkBomb",
		"CandyRifle",
		"ChristmasSniper",
		"GingerbreadBomb",
		"GingerbreadKnife",
		"SantaGun",
		"AUG",
		"M3",
		"M134",
		"StenMarkV",
		"SmokeBomb",
		"FlashBomb",
		"LaserR7",
		"Shovel",
		"ZombieHand",
		"Firelock",
		"HalloweenGun",
		"SM134",
		"SM4",
		"SG36K",
		"SAUG",
		"SAK47",
		"SDesertEagle",
		"SantaGun2014",
		"ThunderX6",
		"MK5",
		"BurstRG2",
		"SnowmanBomb",
		"CandyHammer",
		"HonorKnife",
		"HonorAK47",
		"HonorM4",
		"TeslaP1",
		"M1",
		"MiniCannon",
		"M29",
		"DualPistol",
		"FreddyGun",
		"ImpulseGun",
		"Assault",
		"HonorAWP",
		"HonorM134",
		"ShadowSnake",
		"Digger",
		"Nightmare",
		"SweetMemory",
		"Shark",
		"DeathHunter",
		"NuclearEmitter",
		"Flower",
		"Flamethrower",
		"AlienRifle",
		"AlienSMG",
		"HeroRifle",
		"RoyaleGun"
	};

	// Token: 0x04000D19 RID: 3353
	public static readonly string[] AllSkinNameList = new string[]
	{
		"Skin_1",
		"Skin_2",
		"Skin_3",
		"Skin_4",
		"Skin_5",
		"Skin_6",
		"Skin_7",
		"Skin_8",
		"Skin_9",
		"Skin_10",
		"Skin_11",
		"Skin_12",
		"Skin_13",
		"Skin_14",
		"Skin_15",
		"Skin_16",
		"Skin_17",
		"Skin_18",
		"Skin_19",
		"Skin_20",
		"Skin_21",
		"Skin_22",
		"Skin_23",
		"Skin_24",
		"Skin_25",
		"Skin_26",
		"Skin_27",
		"Skin_28",
		"Skin_29",
		"Skin_30",
		"Skin_31",
		"Skin_32",
		"Skin_33",
		"Skin_34",
		"Skin_35",
		"Skin_36",
		"Skin_37",
		"Skin_38",
		"Skin_39",
		"Skin_40",
		"Skin_41",
		"Skin_42",
		"Skin_43",
		"Skin_44",
		"Skin_45",
		"Skin_46",
		"Skin_47",
		"Skin_48",
		"Skin_49",
		"Skin_50"
	};

	// Token: 0x04000D1A RID: 3354
	public static readonly string[] AllArmorNameList = new string[]
	{
		"BodyArmor_1",
		"HeadArmor_1",
		"HeadNBodyArmor_1"
	};

	// Token: 0x04000D1B RID: 3355
	public static readonly string[] AllHatNameList = new string[]
	{
		"Hat_1",
		"Hat_2",
		"Hat_3",
		"Hat_4",
		"Hat_5",
		"Hat_6",
		"Hat_7",
		"Hat_8",
		"Hat_9",
		"Hat_10",
		"Hat_11",
		"Hat_12",
		"Hat_13"
	};

	// Token: 0x04000D1C RID: 3356
	public static readonly string[] AllCapeNameList = new string[]
	{
		"Cape_1",
		"Cape_2",
		"Cape_3",
		"Cape_4",
		"Cape_5",
		"Cape_6",
		"Cape_7",
		"Cape_8",
		"Cape_9",
		"Cape_10"
	};

	// Token: 0x04000D1D RID: 3357
	public static readonly string[] AllBootNameList = new string[]
	{
		"Boot_1",
		"Boot_2",
		"Boot_3",
		"Boot_4",
		"Boot_5"
	};

	// Token: 0x04000D1E RID: 3358
	public static readonly string[] AllMultiplayerBuffNameList = new string[]
	{
		"SpeedPlusBuff",
		"HpPlusBuff",
		"CoinsX2Buff",
		"ExpX2Buff",
		"DamagePlusBuff",
		"HpRecoveryBuff"
	};

	// Token: 0x04000D1F RID: 3359
	public static readonly string[] AllUpEnabledWeaponPropertyList = new string[]
	{
		"Power",
		"Accuracy",
		"Clip",
		"Move",
		"Energy",
		"Range",
		"Aim"
	};

	// Token: 0x04000D20 RID: 3360
	public static int[] HolidayRechargeRewardTarget = new int[]
	{
		30,
		100,
		200,
		500,
		1000,
		2000
	};

	// Token: 0x04000D21 RID: 3361
	public static readonly string CNR_CUR_VERSION = "gog_v1_0_0";
}
