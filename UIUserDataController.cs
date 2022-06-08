using System;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

// Token: 0x02000313 RID: 787
public class UIUserDataController : MonoBehaviour
{
	// Token: 0x0600181D RID: 6173 RVA: 0x000CB9D6 File Offset: 0x000C9DD6
	public static void SetHuntingDifficult(int difficult)
	{
		PlayerPrefs.SetInt("UI_HuntingDifficult", difficult);
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x000CB9E3 File Offset: 0x000C9DE3
	public static int GetHuntingDifficult()
	{
		return PlayerPrefs.GetInt("UI_HuntingDifficult", 0);
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x000CB9F0 File Offset: 0x000C9DF0
	public static void SetEntertainmentDefaultMode(int mode)
	{
		PlayerPrefs.SetInt("UI_ETDefaultMode", mode);
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x000CB9FD File Offset: 0x000C9DFD
	public static int GetEntertainmentDefaultMode()
	{
		return PlayerPrefs.GetInt("UI_ETDefaultMode", 0);
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x000CBA0A File Offset: 0x000C9E0A
	public static void SetDefaultMode(int mode)
	{
		PlayerPrefs.SetInt("UI_SportDefaultMode", mode);
	}

	// Token: 0x06001822 RID: 6178 RVA: 0x000CBA17 File Offset: 0x000C9E17
	public static int GetDefaultMode()
	{
		return PlayerPrefs.GetInt("UI_SportDefaultMode", 1);
	}

	// Token: 0x06001823 RID: 6179 RVA: 0x000CBA24 File Offset: 0x000C9E24
	public static void SetDefaultPlayerRange(int range)
	{
		PlayerPrefs.SetInt("UI_DefaultPlayerRange", range);
	}

	// Token: 0x06001824 RID: 6180 RVA: 0x000CBA31 File Offset: 0x000C9E31
	public static GGPlayersDisplayInterval GetDefaultPlayerRange()
	{
		return (GGPlayersDisplayInterval)PlayerPrefs.GetInt("UI_DefaultPlayerRange", 0);
	}

	// Token: 0x06001825 RID: 6181 RVA: 0x000CBA3E File Offset: 0x000C9E3E
	public static void SetDefaultServer(int sever)
	{
		PlayerPrefs.SetInt("UI_DefaultSever", sever);
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x000CBA4B File Offset: 0x000C9E4B
	public static GGServerRegion GetDefaultServer()
	{
		return (GGServerRegion)PlayerPrefs.GetInt("UI_DefaultSever", 255);
	}

	// Token: 0x06001827 RID: 6183 RVA: 0x000CBA5C File Offset: 0x000C9E5C
	public static void SetCreatDefaultPlayerNum(int value)
	{
		PlayerPrefs.SetInt("UI_CreatDefaultPlayerNum", value);
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x000CBA69 File Offset: 0x000C9E69
	public static int GetCreatDefaultPlayerNum()
	{
		return PlayerPrefs.GetInt("UI_CreatDefaultPlayerNum", 10);
	}

	// Token: 0x06001829 RID: 6185 RVA: 0x000CBA77 File Offset: 0x000C9E77
	public static void SetCreatDefaultRoomName(string value)
	{
		PlayerPrefs.SetString("UI_CreatDefaultRoomName", value);
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x000CBA84 File Offset: 0x000C9E84
	public static string GetCreatDefaultRoomName()
	{
		if (PlayerPrefs.GetString("UI_CreatDefaultRoomName", string.Empty) == string.Empty)
		{
			UIUserDataController.SetCreatDefaultRoomName("Room " + UnityEngine.Random.Range(100, 100000).ToString());
		}
		return PlayerPrefs.GetString("UI_CreatDefaultRoomName", string.Empty);
	}

	// Token: 0x0600182B RID: 6187 RVA: 0x000CBAE8 File Offset: 0x000C9EE8
	public static void SetPhrase(int index, string content)
	{
		if (content != null)
		{
			string key = "UIChatPhrase" + index.ToString();
			PlayerPrefs.SetString(key, content);
		}
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x000CBB1C File Offset: 0x000C9F1C
	public static string GetPhrase(int index)
	{
		string key = "UIChatPhrase" + index.ToString();
		return PlayerPrefs.GetString(key, UIUserDataController.defaultPhrase[index]);
	}

	// Token: 0x0600182D RID: 6189 RVA: 0x000CBB4E File Offset: 0x000C9F4E
	public static int GetSoundFX()
	{
		return PlayerPrefs.GetInt("Setting_SoundFX", 1);
	}

	// Token: 0x0600182E RID: 6190 RVA: 0x000CBB5B File Offset: 0x000C9F5B
	public static int GetMusic()
	{
		return PlayerPrefs.GetInt("Setting_Music", 1);
	}

	// Token: 0x0600182F RID: 6191 RVA: 0x000CBB68 File Offset: 0x000C9F68
	public static int GetLefty()
	{
		return PlayerPrefs.GetInt("Setting_Lefty", 1);
	}

	// Token: 0x06001830 RID: 6192 RVA: 0x000CBB75 File Offset: 0x000C9F75
	public static int GetBulletHole()
	{
		return PlayerPrefs.GetInt("Setting_BulletHole", 0);
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x000CBB82 File Offset: 0x000C9F82
	public static int GetShadow()
	{
		return PlayerPrefs.GetInt("Setting_Shadow", 1);
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x000CBB8F File Offset: 0x000C9F8F
	public static int GetSpark()
	{
		return PlayerPrefs.GetInt("Setting_Spark", 1);
	}

	// Token: 0x06001833 RID: 6195 RVA: 0x000CBB9C File Offset: 0x000C9F9C
	public static int GetSensitivity()
	{
		return PlayerPrefs.GetInt("Setting_Sensitivity", 20);
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x000CBBAA File Offset: 0x000C9FAA
	public static int GetQualityLevel()
	{
		return PlayerPrefs.GetInt("Setting_QualityLevel", 1);
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x000CBBB7 File Offset: 0x000C9FB7
	public static int GetPopVideoShareMenu()
	{
		return PlayerPrefs.GetInt("Setting_PopVideoShareMenu", 1);
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x000CBBC4 File Offset: 0x000C9FC4
	public static int GetSniperMode()
	{
		return PlayerPrefs.GetInt("Setting_SniperMode", 1);
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x000CBBD1 File Offset: 0x000C9FD1
	public static void SetSoundFX(int value)
	{
		PlayerPrefs.SetInt("Setting_SoundFX", value);
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x000CBBDE File Offset: 0x000C9FDE
	public static void SetMusic(int value)
	{
		PlayerPrefs.SetInt("Setting_Music", value);
	}

	// Token: 0x06001839 RID: 6201 RVA: 0x000CBBEB File Offset: 0x000C9FEB
	public static void SetLefty(int value)
	{
		PlayerPrefs.SetInt("Setting_Lefty", value);
	}

	// Token: 0x0600183A RID: 6202 RVA: 0x000CBBF8 File Offset: 0x000C9FF8
	public static void SetBulletHole(int value)
	{
		PlayerPrefs.SetInt("Setting_BulletHole", value);
	}

	// Token: 0x0600183B RID: 6203 RVA: 0x000CBC05 File Offset: 0x000CA005
	public static void SetShadow(int value)
	{
		PlayerPrefs.SetInt("Setting_Shadow", value);
	}

	// Token: 0x0600183C RID: 6204 RVA: 0x000CBC12 File Offset: 0x000CA012
	public static void SetSpark(int value)
	{
		PlayerPrefs.SetInt("Setting_Spark", value);
	}

	// Token: 0x0600183D RID: 6205 RVA: 0x000CBC1F File Offset: 0x000CA01F
	public static void SetSensitivity(int value)
	{
		PlayerPrefs.SetInt("Setting_Sensitivity", value);
	}

	// Token: 0x0600183E RID: 6206 RVA: 0x000CBC2C File Offset: 0x000CA02C
	public static void SetQualityLevel(int value)
	{
		PlayerPrefs.SetInt("Setting_QualityLevel", value);
	}

	// Token: 0x0600183F RID: 6207 RVA: 0x000CBC39 File Offset: 0x000CA039
	public static void SetPopVideoShareMenu(int value)
	{
		PlayerPrefs.SetInt("Setting_PopVideoShareMenu", value);
	}

	// Token: 0x06001840 RID: 6208 RVA: 0x000CBC46 File Offset: 0x000CA046
	public static void SetSniperMode(int value)
	{
		PlayerPrefs.SetInt("Setting_SniperMode", value);
	}

	// Token: 0x06001841 RID: 6209 RVA: 0x000CBC53 File Offset: 0x000CA053
	public static int GetThrowWeaponNum(string weaponName)
	{
		return GOGPlayerPrefabs.GetInt("UIThrowWeapon_" + weaponName, 0);
	}

	// Token: 0x06001842 RID: 6210 RVA: 0x000CBC66 File Offset: 0x000CA066
	public static void SetThrowWeaponNum(string weaponName, int num)
	{
		GOGPlayerPrefabs.SetInt("UIThrowWeapon_" + weaponName, num);
	}

	// Token: 0x06001843 RID: 6211 RVA: 0x000CBC79 File Offset: 0x000CA079
	public static void AddThrowWeaponNum(string weaponName, int num)
	{
		GOGPlayerPrefabs.SetInt("UIThrowWeapon_" + weaponName, UIUserDataController.GetThrowWeaponNum(weaponName) + num);
	}

	// Token: 0x06001844 RID: 6212 RVA: 0x000CBC93 File Offset: 0x000CA093
	public static void SubThrowWeaponNum(string weaponName, int num)
	{
		GOGPlayerPrefabs.SetInt("UIThrowWeapon_" + weaponName, UIUserDataController.GetThrowWeaponNum(weaponName) - num);
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x000CBCAD File Offset: 0x000CA0AD
	public static int GetQuickBarItemIndex()
	{
		return GOGPlayerPrefabs.GetInt("UIQuickBarItemIndex", 0);
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x000CBCBA File Offset: 0x000CA0BA
	public static void SetQuickBarItemIndex(int index)
	{
		GOGPlayerPrefabs.SetInt("UIQuickBarItemIndex", index);
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x000CBCC7 File Offset: 0x000CA0C7
	public static void SetCurPaletteColor(Color32 color)
	{
		PlayerPrefs.SetString("UICurPaletteColor", UIUserDataController.ColorToString(color));
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x000CBCD9 File Offset: 0x000CA0D9
	public static Color32 GetCurPaletteColor()
	{
		return UIUserDataController.StringToColor(PlayerPrefs.GetString("UICurPaletteColor", "0_0_255_255"));
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x000CBCEF File Offset: 0x000CA0EF
	public static void SetSavedColor(Color32 color, int index)
	{
		PlayerPrefs.SetString("UISavedColor_" + index.ToString(), UIUserDataController.ColorToString(color));
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x000CBD14 File Offset: 0x000CA114
	public static Color32 GetSavedColor(int index)
	{
		Color32 color = default(Color32);
		return UIUserDataController.StringToColor(PlayerPrefs.GetString("UISavedColor_" + index.ToString(), "251_249_249_255"));
	}

	// Token: 0x0600184B RID: 6219 RVA: 0x000CBD54 File Offset: 0x000CA154
	private static string ColorToString(Color32 color)
	{
		return string.Concat(new string[]
		{
			color.r.ToString(),
			"_",
			color.g.ToString(),
			"_",
			color.b.ToString(),
			"_",
			color.a.ToString()
		});
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x000CBDD8 File Offset: 0x000CA1D8
	public static Color32 StringToColor(string colorString)
	{
		Color32 result = default(Color32);
		string[] array = colorString.Split(new char[]
		{
			'_'
		});
		result.r = byte.Parse(array[0]);
		result.g = byte.Parse(array[1]);
		result.b = byte.Parse(array[2]);
		result.a = byte.Parse(array[3]);
		return result;
	}

	// Token: 0x0600184D RID: 6221 RVA: 0x000CBE3C File Offset: 0x000CA23C
	public static void SetCurLoginUserName(string value)
	{
		PlayerPrefs.SetString("UICurLoginUserName", value.ToLower());
	}

	// Token: 0x0600184E RID: 6222 RVA: 0x000CBE4E File Offset: 0x000CA24E
	public static string GetCurLoginUserName()
	{
		return PlayerPrefs.GetString("UICurLoginUserName", string.Empty);
	}

	// Token: 0x0600184F RID: 6223 RVA: 0x000CBE5F File Offset: 0x000CA25F
	public static void SetCurLoginPassword(string value)
	{
		PlayerPrefs.SetString("UICurLoginPassword", value.ToLower());
	}

	// Token: 0x06001850 RID: 6224 RVA: 0x000CBE71 File Offset: 0x000CA271
	public static string GetCurLoginPassword()
	{
		return PlayerPrefs.GetString("UICurLoginPassword", string.Empty);
	}

	// Token: 0x06001851 RID: 6225 RVA: 0x000CBE82 File Offset: 0x000CA282
	public static void SetDefaultUserName(string value)
	{
		PlayerPrefs.SetString("UIDefaultUsername", value.ToLower());
	}

	// Token: 0x06001852 RID: 6226 RVA: 0x000CBE94 File Offset: 0x000CA294
	public static void SetPreUserName(string value)
	{
		PlayerPrefs.SetString("UIPreUsername", value);
	}

	// Token: 0x06001853 RID: 6227 RVA: 0x000CBEA1 File Offset: 0x000CA2A1
	public static string GetPreUserName()
	{
		return PlayerPrefs.GetString("UIPreUsername", string.Empty);
	}

	// Token: 0x06001854 RID: 6228 RVA: 0x000CBEB2 File Offset: 0x000CA2B2
	public static void SetDefaultPwd(string value)
	{
		PlayerPrefs.SetString("UIDefaultPwd", value);
	}

	// Token: 0x06001855 RID: 6229 RVA: 0x000CBEBF File Offset: 0x000CA2BF
	public static void SetDefaultRoleName(string value)
	{
		PlayerPrefs.SetString("UIDefaultRoleName", value);
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x000CBECC File Offset: 0x000CA2CC
	public static string GetDefaultUserName()
	{
		return PlayerPrefs.GetString("UIDefaultUsername", string.Empty);
	}

	// Token: 0x06001857 RID: 6231 RVA: 0x000CBEDD File Offset: 0x000CA2DD
	public static string GetDefaultPwd()
	{
		return PlayerPrefs.GetString("UIDefaultPwd", string.Empty);
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x000CBEEE File Offset: 0x000CA2EE
	public static string GetDefaultRoleName()
	{
		return PlayerPrefs.GetString("UIDefaultRoleName", string.Empty);
	}

	// Token: 0x06001859 RID: 6233 RVA: 0x000CBEFF File Offset: 0x000CA2FF
	public static void SetRememberPwd(int value)
	{
		PlayerPrefs.SetInt("UIRememberPwd", value);
	}

	// Token: 0x0600185A RID: 6234 RVA: 0x000CBF0C File Offset: 0x000CA30C
	public static int GetRememberPwd()
	{
		return PlayerPrefs.GetInt("UIRememberPwd", 1);
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x000CBF19 File Offset: 0x000CA319
	public static void SetUsedAccountCount(int value)
	{
		PlayerPrefs.SetInt("UIUsedAccountCount", value);
	}

	// Token: 0x0600185C RID: 6236 RVA: 0x000CBF26 File Offset: 0x000CA326
	public static int GetUsedAccountCount()
	{
		return PlayerPrefs.GetInt("UIUsedAccountCount", 0);
	}

	// Token: 0x0600185D RID: 6237 RVA: 0x000CBF33 File Offset: 0x000CA333
	public static void SetUsedAccount(int index, string accountString)
	{
		PlayerPrefs.SetString("UIUsedAccount_" + index.ToString(), accountString);
	}

	// Token: 0x0600185E RID: 6238 RVA: 0x000CBF52 File Offset: 0x000CA352
	public static string GetUsedAccount(int index)
	{
		return PlayerPrefs.GetString("UIUsedAccount_" + index.ToString(), string.Empty);
	}

	// Token: 0x0600185F RID: 6239 RVA: 0x000CBF75 File Offset: 0x000CA375
	public static void SetUsedAccountPwd(int index, string accountPwdString)
	{
		PlayerPrefs.SetString("UIUsedAccountPwd_" + index.ToString(), accountPwdString);
	}

	// Token: 0x06001860 RID: 6240 RVA: 0x000CBF94 File Offset: 0x000CA394
	public static string GetUsedAccountPwd(int index)
	{
		return PlayerPrefs.GetString("UIUsedAccountPwd_" + index.ToString(), string.Empty);
	}

	// Token: 0x06001861 RID: 6241 RVA: 0x000CBFB8 File Offset: 0x000CA3B8
	public static List<string> GetUsedAccountList()
	{
		List<string> list = new List<string>();
		int usedAccountCount = UIUserDataController.GetUsedAccountCount();
		if (usedAccountCount > 0)
		{
			for (int i = 0; i < usedAccountCount; i++)
			{
				list.Add(UIUserDataController.GetUsedAccount(i));
			}
			for (int j = 0; j < usedAccountCount; j++)
			{
				list.Add(UIUserDataController.GetUsedAccountPwd(j));
			}
		}
		return list;
	}

	// Token: 0x06001862 RID: 6242 RVA: 0x000CC018 File Offset: 0x000CA418
	public static void SetUsedAccountList(List<string> list)
	{
		if (list != null && list.Count > 0)
		{
			UIUserDataController.SetUsedAccountCount(list.Count / 2);
			for (int i = 0; i < list.Count / 2; i++)
			{
				UIUserDataController.SetUsedAccount(i, list[i]);
			}
			for (int j = list.Count / 2; j < list.Count; j++)
			{
				UIUserDataController.SetUsedAccountPwd(j - list.Count / 2, list[j]);
			}
		}
		else
		{
			UIUserDataController.SetUsedAccountCount(0);
		}
	}

	// Token: 0x06001863 RID: 6243 RVA: 0x000CC0A9 File Offset: 0x000CA4A9
	public static void SetGrowthPackStatus(int index, int hasBought)
	{
		GOGPlayerPrefabs.SetInt("UIIPAGrowthPack_" + index.ToString(), hasBought);
	}

	// Token: 0x06001864 RID: 6244 RVA: 0x000CC0C8 File Offset: 0x000CA4C8
	public static int GetGrowthPackStatus(int index)
	{
		return GOGPlayerPrefabs.GetInt("UIIPAGrowthPack_" + index.ToString(), 0);
	}

	// Token: 0x06001865 RID: 6245 RVA: 0x000CC0E7 File Offset: 0x000CA4E7
	public static void SetCustomSkinDownloadMark(bool bMark)
	{
		if (bMark)
		{
			PlayerPrefs.SetInt("NeedDownloadCustomSkinFromServer", 1);
		}
		else
		{
			PlayerPrefs.SetInt("NeedDownloadCustomSkinFromServer", 0);
		}
	}

	// Token: 0x06001866 RID: 6246 RVA: 0x000CC10C File Offset: 0x000CA50C
	public static bool NeedDownloadCustomSkinData()
	{
		return PlayerPrefs.GetInt("NeedDownloadCustomSkinFromServer", 0).Equals(1);
	}

	// Token: 0x06001867 RID: 6247 RVA: 0x000CC12D File Offset: 0x000CA52D
	public static int GetLoginSuccessCount()
	{
		return GOGPlayerPrefabs.GetInt("UILoginCount" + GameVersionController.GameVersion, 0);
	}

	// Token: 0x06001868 RID: 6248 RVA: 0x000CC144 File Offset: 0x000CA544
	public static void SetLoginSuccessCount(int count)
	{
		GOGPlayerPrefabs.SetInt("UILoginCount" + GameVersionController.GameVersion, count);
	}

	// Token: 0x06001869 RID: 6249 RVA: 0x000CC15C File Offset: 0x000CA55C
	public static bool HasRatedInAppstore()
	{
		return GOGPlayerPrefabs.GetInt("UIHasRatedOurGame_" + GameVersionController.GameVersion, 0).Equals(1);
	}

	// Token: 0x0600186A RID: 6250 RVA: 0x000CC187 File Offset: 0x000CA587
	public static void VerifyAppstoreRating()
	{
		GOGPlayerPrefabs.SetInt("UIHasRatedOurGame_" + GameVersionController.GameVersion, 1);
	}

	// Token: 0x0600186B RID: 6251 RVA: 0x000CC1A0 File Offset: 0x000CA5A0
	public static bool HasDownloadRecommendApp()
	{
		return GOGPlayerPrefabs.GetInt("UIDownloadRecommendApp_" + GameVersionController.GameVersion, 0).Equals(1);
	}

	// Token: 0x0600186C RID: 6252 RVA: 0x000CC1CB File Offset: 0x000CA5CB
	public static void VerifyDownloadRecommendApp()
	{
		GOGPlayerPrefabs.SetInt("UIDownloadRecommendApp_" + GameVersionController.GameVersion, 1);
	}

	// Token: 0x0600186D RID: 6253 RVA: 0x000CC1E2 File Offset: 0x000CA5E2
	public static int GetFromLoginToHome()
	{
		return PlayerPrefs.GetInt("UIFromLoginToHome", 0);
	}

	// Token: 0x0600186E RID: 6254 RVA: 0x000CC1EF File Offset: 0x000CA5EF
	public static void SetFromLoginToHome(int count)
	{
		PlayerPrefs.SetInt("UIFromLoginToHome", count);
	}

	// Token: 0x0600186F RID: 6255 RVA: 0x000CC1FC File Offset: 0x000CA5FC
	public static int GetFirstPlay()
	{
		return PlayerPrefs.GetInt("UISetFirstPlay", 0);
	}

	// Token: 0x06001870 RID: 6256 RVA: 0x000CC209 File Offset: 0x000CA609
	public static void SetFirstPlay(int value)
	{
		PlayerPrefs.SetInt("UISetFirstPlay", value);
	}

	// Token: 0x06001871 RID: 6257 RVA: 0x000CC216 File Offset: 0x000CA616
	public static bool GetLatestVersionTag()
	{
		return PlayerPrefs.GetInt("LatestVersionTag_" + GameVersionController.GameVersion, 0) == 1;
	}

	// Token: 0x06001872 RID: 6258 RVA: 0x000CC23A File Offset: 0x000CA63A
	public static void SetLatestVersionTag()
	{
		PlayerPrefs.SetInt("LatestVersionTag_" + GameVersionController.GameVersion, 1);
	}

	// Token: 0x06001873 RID: 6259 RVA: 0x000CC251 File Offset: 0x000CA651
	public static bool GetIsFreeCoins()
	{
		return ObscuredPrefs.GetBool("Home_IsFreeCoins", false);
	}

	// Token: 0x06001874 RID: 6260 RVA: 0x000CC25E File Offset: 0x000CA65E
	public static void SetIsFreeCoins(bool isfreecoins)
	{
		ObscuredPrefs.SetBool("Home_IsFreeCoins", isfreecoins);
	}

	// Token: 0x06001875 RID: 6261 RVA: 0x000CC26C File Offset: 0x000CA66C
	public static float GetOffsaleFactor()
	{
		return (float)GGCloudServiceKit.mInstance.CurrentAllowance;
	}

	// Token: 0x06001876 RID: 6262 RVA: 0x000CC288 File Offset: 0x000CA688
	public static TimeSpan GetOffsaleTimeSpan()
	{
		return ACTUserDataManager.mInstance.GetAllowenceLeftTime();
	}

	// Token: 0x06001877 RID: 6263 RVA: 0x000CC2A1 File Offset: 0x000CA6A1
	public static string GetGDPRVersion()
	{
		return PlayerPrefs.GetString("GDPRVersion", "v0.0.0");
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x000CC2B2 File Offset: 0x000CA6B2
	public static void SetGDPRVersion(string versionArg)
	{
		PlayerPrefs.SetString("GDPRVersion", versionArg);
	}

	// Token: 0x06001879 RID: 6265 RVA: 0x000CC2C0 File Offset: 0x000CA6C0
	public static bool GetGDPRToggleStatus()
	{
		return PlayerPrefs.GetInt("GDPRToggleStatus", 0).Equals(1);
	}

	// Token: 0x0600187A RID: 6266 RVA: 0x000CC2E1 File Offset: 0x000CA6E1
	public static void SetGDPRToggleStatus(bool gDPRToggleStatusArg)
	{
		PlayerPrefs.SetInt("GDPRToggleStatus", (!gDPRToggleStatusArg) ? 0 : 1);
	}

	// Token: 0x04001B9C RID: 7068
	public static readonly string[] defaultPhrase = new string[]
	{
		"Go! Go! Go!",
		"Follow me!",
		"LOL!",
		"Wow, Cool!",
		"Rush!",
		"God like! ",
		"Madness!",
		"Help, please!",
		"I got a headshot!",
		"Can i be your friend?"
	};

	// Token: 0x04001B9D RID: 7069
	public static int[] allThrowWeaponPrice = new int[]
	{
		5,
		6,
		8,
		8,
		5,
		6
	};

	// Token: 0x04001B9E RID: 7070
	public static string[] allThrowWeapon = new string[]
	{
		"M67",
		"MilkBomb",
		"GingerbreadBomb",
		"SmokeBomb",
		"FlashBomb",
		"SnowmanBomb",
		"Null"
	};
}
