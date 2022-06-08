using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace GrowthSystem
{
	// Token: 0x0200019F RID: 415
	public class GrowthManger : MonoBehaviour
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000BA4 RID: 2980 RVA: 0x00054E00 File Offset: 0x00053200
		// (remove) Token: 0x06000BA5 RID: 2981 RVA: 0x00054E34 File Offset: 0x00053234
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event GrowthManger.CharacterLevelUpEventHandler OnCharacterLevelUp;

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00054E68 File Offset: 0x00053268
		public void GenCharacterLevelUpEvent(int curLevel)
		{
			if (GrowthManger.OnCharacterLevelUp != null)
			{
				GrowthManger.OnCharacterLevelUp(curLevel);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000BA7 RID: 2983 RVA: 0x00054E80 File Offset: 0x00053280
		// (remove) Token: 0x06000BA8 RID: 2984 RVA: 0x00054EB4 File Offset: 0x000532B4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event GrowthManger.GrowthPromptEventHandler OnGrowthPrompt;

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00054EE8 File Offset: 0x000532E8
		public void GenGrowthPromptEvent(GrowthPrometType type, int num, string description)
		{
			if (GrowthManger.OnGrowthPrompt != null)
			{
				GrowthManger.OnGrowthPrompt(type, num, description);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000BAA RID: 2986 RVA: 0x00054F04 File Offset: 0x00053304
		// (remove) Token: 0x06000BAB RID: 2987 RVA: 0x00054F38 File Offset: 0x00053338
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event GrowthManger.ScenePropsInvalidEventHandler OnScenePropsInvalid;

		// Token: 0x06000BAC RID: 2988 RVA: 0x00054F6C File Offset: 0x0005336C
		public void GenScenePropsInvalidEvent(SceneEnchantmentProps type)
		{
			if (GrowthManger.OnScenePropsInvalid != null)
			{
				GrowthManger.OnScenePropsInvalid(type);
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00054F83 File Offset: 0x00053383
		public int GetCharacterLevel()
		{
			return UserDataController.GetCharacterLevel();
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00054F8A File Offset: 0x0005338A
		public int GetCharacterExpTotal()
		{
			return UserDataController.GetCharacterExp();
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00054F91 File Offset: 0x00053391
		public int GetCharacterCurLevelUpExpNeed(int curLevel)
		{
			return this.mExpCalcer.GetCharacterCurLevelUpExpNeed(curLevel);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00054F9F File Offset: 0x0005339F
		public int GetCharacterCurLevelExpExist(int curLevel)
		{
			return this.mExpCalcer.GetCharacterCurLevelExpExist(curLevel);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00054FB0 File Offset: 0x000533B0
		public void AddCharacterExp(int expAdd)
		{
			int num = this.mExpCalcer.AddCharacterExpAndChkLevelUp(expAdd);
			int num2 = this.GetCharacterLevel() - num;
			for (int i = 0; i < num; i++)
			{
				this.GenCharacterLevelUpEvent(num2);
				num2++;
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00054FF0 File Offset: 0x000533F0
		public int GetHonorPoint()
		{
			return this.mRewardCalcer.GetHonorPoint();
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00055000 File Offset: 0x00053400
		public bool SubHonorPoint(int subNum)
		{
			this.SetDataDisplayRefreshFlag(true);
			bool flag = this.mRewardCalcer.SubHonorPoint(subNum);
			if (flag)
			{
			}
			return flag;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00055028 File Offset: 0x00053428
		public void AddHonorPoint(int addNum)
		{
			this.SetDataDisplayRefreshFlag(true);
			this.mRewardCalcer.AddHonorPoint(addNum);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0005503D File Offset: 0x0005343D
		public int GetCoins()
		{
			return this.mRewardCalcer.GetCoins();
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0005504C File Offset: 0x0005344C
		public bool SubCoins(int coinsSub)
		{
			this.SetDataDisplayRefreshFlag(true);
			bool flag = this.mRewardCalcer.SubCoins(coinsSub);
			if (flag)
			{
				base.GetComponent<AudioSource>().PlayOneShot(this.coinsOut);
			}
			return flag;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00055085 File Offset: 0x00053485
		public void AddCoins(int coinsAdd)
		{
			this.SetDataDisplayRefreshFlag(true);
			base.GetComponent<AudioSource>().PlayOneShot(this.coinsIn);
			this.mRewardCalcer.AddCoins(coinsAdd);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000550AB File Offset: 0x000534AB
		public int GetGems()
		{
			return UserDataController.GetGems();
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000550B2 File Offset: 0x000534B2
		public void AddGems(int gemsAdd)
		{
			this.SetDataDisplayRefreshFlag(true);
			base.GetComponent<AudioSource>().PlayOneShot(this.gemsIn);
			UserDataController.AddGems(gemsAdd);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x000550D2 File Offset: 0x000534D2
		public bool SubGems(int gemsSub)
		{
			this.SetDataDisplayRefreshFlag(true);
			if (UserDataController.GetGems() < gemsSub)
			{
				return false;
			}
			UserDataController.SubGems(gemsSub);
			return true;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000550EF File Offset: 0x000534EF
		public int GetLevelUpCoinsReward(int curLevel)
		{
			return this.mRewardCalcer.GetLevelUpCoinsReward(curLevel);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000550FD File Offset: 0x000534FD
		public int GetSNSShareAfterLevelUpCoinsReward(int curLevel)
		{
			return this.mRewardCalcer.GetSNSShareAfterLevelUpCoinsReward(curLevel);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0005510B File Offset: 0x0005350B
		public int GetSNSShareCoinsReward()
		{
			return this.mRewardCalcer.GetSNSShareCoinsReward();
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00055118 File Offset: 0x00053518
		public int GetAppstoreRatingCoinsReward()
		{
			return this.mRewardCalcer.GetAppstoreRatingCoinsReward();
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00055125 File Offset: 0x00053525
		public GrowthGameRatingTag GetOncePlayRating(GameValueForRatingCalc gameValue)
		{
			return this.mRewardCalcer.GetOncePlayRating(gameValue);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00055133 File Offset: 0x00053533
		public int GetOncePlayExpReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating)
		{
			return this.mRewardCalcer.GetOncePlayExpReward(gameMode, gameRating);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00055142 File Offset: 0x00053542
		public int GetOncePlayHonorPointReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating)
		{
			return this.mRewardCalcer.GetOncePlayHonorPointReward(gameMode, gameRating);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00055151 File Offset: 0x00053551
		public int GetOncePlaySeasonScoreReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating, bool isWinner)
		{
			return this.mRewardCalcer.GetOncePlaySeasonScoreReward(gameMode, gameRating, isWinner);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00055161 File Offset: 0x00053561
		public int GetOncePlayCoinsReward(GrowthGameModeTag gameMode, GrowthGameRatingTag gameRating)
		{
			return this.mRewardCalcer.GetOncePlayCoinsReward(gameMode, gameRating);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00055170 File Offset: 0x00053570
		public RewardUnitInfo GetRewardUnitInfo(FightingStatisticsTag tag)
		{
			return this.mRewardCalcer.GetRewardUnitInfo(tag);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0005517E File Offset: 0x0005357E
		public void ResetDailyRewardRecord(FightingStatisticsTag tag)
		{
			this.mRewardCalcer.ResetDailyRewardRecord(tag);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0005518C File Offset: 0x0005358C
		public void ReceiveReward(FightingStatisticsTag tag)
		{
			RewardUnitInfo rewardUnitInfo = this.GetRewardUnitInfo(tag);
			if (rewardUnitInfo.canGotReward)
			{
				string[] array = rewardUnitInfo.rewardStrValue.Split(new char[]
				{
					'@'
				});
				string text = array[0];
				string text2 = array[1];
				int num = int.Parse(array[2]);
				if (text != null)
				{
					if (!(text == "Base"))
					{
						if (!(text == "Potion"))
						{
							if (!(text == "Armor"))
							{
							}
						}
						else
						{
							GMultiplayerBuffItemInfo multiplayerBuffItemInfoByName = this.GetMultiplayerBuffItemInfoByName(text2);
							multiplayerBuffItemInfoByName.AddBuffNum(num);
						}
					}
					else if (text2 != null)
					{
						if (!(text2 == "Coins"))
						{
							if (!(text2 == "GiftBox"))
							{
								if (text2 == "Gems")
								{
									this.AddGems(num);
								}
							}
							else
							{
								this.SetDataDisplayRefreshFlag(true);
								UserDataController.SetCurGiftBoxTotal(UserDataController.GetCurGiftBoxTotal() + num);
							}
						}
						else
						{
							this.AddCoins(num);
						}
					}
				}
				UserDataController.VerifyFightingStatisticsRewardGot(tag, rewardUnitInfo.rewardLv);
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000552B5 File Offset: 0x000536B5
		public HuntingRewardInfo GetHuntingModeItemReward(GameValueForRatingCalc gameValue, GrowthGameRatingTag gameRating)
		{
			return this.mRewardCalcer.GetHuntingModeItemReward(gameValue, gameRating);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x000552C4 File Offset: 0x000536C4
		public GItemId GetItemId(string name)
		{
			return this.mItemBaseValue.mItemDict_kName_vId[name];
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000552D8 File Offset: 0x000536D8
		public GArmorItemInfo GetArmorItemInfoByName(string name)
		{
			GItemId id = this.mItemBaseValue.mItemDict_kName_vId[name];
			return UserDataController.GetArmorItemInfo(name, id);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000552FE File Offset: 0x000536FE
		public GArmorItemInfo GetCurSettedArmorInfo()
		{
			return this.GetArmorItemInfoByName(UserDataController.GetCurSettedArmorName());
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0005530B File Offset: 0x0005370B
		public void SetCurSettedArmor(string name)
		{
			UserDataController.SetCurSettedArmor(name);
			this.curSettedArmorInfo = this.GetArmorItemInfoByName(UserDataController.GetCurSettedArmorName());
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00055324 File Offset: 0x00053724
		public GWeaponItemInfo GetWeaponItemInfoByName(string name)
		{
			GItemId id = this.mItemBaseValue.mItemDict_kName_vId[name];
			GWeaponItemInfo weaponItemInfo = UserDataController.GetWeaponItemInfo(name, id, false, -1);
			weaponItemInfo.mGunType = this.mItemBaseValue.mWeaponDict_kName_vType[name];
			return weaponItemInfo;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00055368 File Offset: 0x00053768
		public GWeaponItemInfo GetUpgradeWeaponItemInfoByName(string name)
		{
			GItemId id = this.mItemBaseValue.mItemDict_kName_vId[name];
			GWeaponItemInfo weaponItemInfo = UserDataController.GetWeaponItemInfo(name, id, true, -1);
			weaponItemInfo.mGunType = this.mItemBaseValue.mWeaponDict_kName_vType[name];
			return weaponItemInfo;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000553AC File Offset: 0x000537AC
		public GWeaponItemInfo[] GetComparedWeaponItemInfoListByName(string name)
		{
			GWeaponItemInfo[] array = new GWeaponItemInfo[2];
			GItemId id = this.mItemBaseValue.mItemDict_kName_vId[name];
			array[0] = UserDataController.GetWeaponItemInfo(name, id, false, 1);
			array[1] = UserDataController.GetWeaponItemInfo(name, id, false, 2);
			array[0].mGunType = this.mItemBaseValue.mWeaponDict_kName_vType[name];
			array[1].mGunType = this.mItemBaseValue.mWeaponDict_kName_vType[name];
			return array;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00055420 File Offset: 0x00053820
		public bool UpgradeWeaponByName(string name)
		{
			GWeaponItemInfo weaponItemInfoByName = this.GetWeaponItemInfoByName(name);
			int mPrice = weaponItemInfoByName.mPrice;
			if (weaponItemInfoByName.mPurchasedType == GItemPurchaseType.CoinsPurchase)
			{
				if (mPrice > UserDataController.GetCoins())
				{
					return false;
				}
				if (UserDataController.UpgradeWeapon(name, 1))
				{
					this.SubCoins(mPrice);
					return true;
				}
				return false;
			}
			else
			{
				if (weaponItemInfoByName.mPurchasedType != GItemPurchaseType.GemPurchase)
				{
					return false;
				}
				if (mPrice > UserDataController.GetGems())
				{
					return false;
				}
				if (UserDataController.UpgradeWeapon(name, 1))
				{
					this.SubGems(mPrice);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000554A0 File Offset: 0x000538A0
		public GSkinItemInfo GetSkinItemInfo(string name)
		{
			GItemId id = this.mItemBaseValue.mItemDict_kName_vId[name];
			return UserDataController.GetSkinItemInfo(name, id);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000554C6 File Offset: 0x000538C6
		public GSkinItemInfo GetCustomSkinItemInfo(string name)
		{
			return UserDataController.GetSkinItemInfo(name, new GItemId(7, 1, 9999, 0));
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000554DC File Offset: 0x000538DC
		public bool EnableSkin(string name)
		{
			GSkinItemInfo skinItemInfo = this.GetSkinItemInfo(name);
			int mPrice = skinItemInfo.mPrice;
			if (skinItemInfo.mPurchasedType == GItemPurchaseType.CoinsPurchase)
			{
				if (mPrice > UserDataController.GetCoins())
				{
					return false;
				}
				if (UserDataController.EnableSkin(name))
				{
					this.SubCoins(mPrice);
					return true;
				}
				return false;
			}
			else
			{
				if (skinItemInfo.mPurchasedType != GItemPurchaseType.GemPurchase)
				{
					return false;
				}
				if (mPrice > UserDataController.GetGems())
				{
					return false;
				}
				if (UserDataController.EnableSkin(name))
				{
					this.SubGems(mPrice);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00055558 File Offset: 0x00053958
		public void DisableSkin(string name)
		{
			UserDataController.DisableSkin(name);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00055560 File Offset: 0x00053960
		public string GetCurSettedSkinName()
		{
			return UserDataController.GetCurSettedSkinName();
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00055567 File Offset: 0x00053967
		public void SetCurSettedSkin(string name)
		{
			UserDataController.SetCurSettedSkin(name);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0005556F File Offset: 0x0005396F
		public string[] GetUserAllEnabledSkinName()
		{
			return UserDataController.GetUserAllEnabledSkinName();
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00055578 File Offset: 0x00053978
		public GHatItemInfo GetHatItemInfo(string name)
		{
			GItemId id = this.mItemBaseValue.mItemDict_kName_vId[name];
			return UserDataController.GetHatItemInfo(name, id);
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0005559E File Offset: 0x0005399E
		public GHatItemInfo GetCustomHatItemInfo(string name)
		{
			return UserDataController.GetHatItemInfo(name, new GItemId(7, 2, 9999, 0));
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000555B4 File Offset: 0x000539B4
		public bool EnableHat(string name)
		{
			GHatItemInfo hatItemInfo = this.GetHatItemInfo(name);
			int mPrice = hatItemInfo.mPrice;
			if (hatItemInfo.mPurchasedType == GItemPurchaseType.CoinsPurchase)
			{
				if (mPrice > UserDataController.GetCoins())
				{
					return false;
				}
				if (UserDataController.EnableHat(name))
				{
					this.SubCoins(mPrice);
					return true;
				}
				return false;
			}
			else
			{
				if (hatItemInfo.mPurchasedType != GItemPurchaseType.GemPurchase)
				{
					return false;
				}
				if (mPrice > UserDataController.GetGems())
				{
					return false;
				}
				if (UserDataController.EnableHat(name))
				{
					this.SubGems(mPrice);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00055630 File Offset: 0x00053A30
		public string GetCurSettedHatName()
		{
			return UserDataController.GetCurSettedHatName();
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00055637 File Offset: 0x00053A37
		public void SetCurSettedHat(string name)
		{
			UserDataController.SetCurSettedHat(name);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0005563F File Offset: 0x00053A3F
		public string[] GetUserAllEnabledHatName()
		{
			return UserDataController.GetUserAllEnabledHatName();
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00055648 File Offset: 0x00053A48
		public GCapeItemInfo GetCapeItemInfo(string name)
		{
			GItemId id = this.mItemBaseValue.mItemDict_kName_vId[name];
			return UserDataController.GetCapeItemInfo(name, id);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0005566E File Offset: 0x00053A6E
		public GCapeItemInfo GetCustomCapeItemInfo(string name)
		{
			return UserDataController.GetCapeItemInfo(name, new GItemId(7, 3, 9999, 0));
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00055684 File Offset: 0x00053A84
		public bool EnableCape(string name)
		{
			GCapeItemInfo capeItemInfo = this.GetCapeItemInfo(name);
			int mPrice = capeItemInfo.mPrice;
			if (capeItemInfo.mPurchasedType == GItemPurchaseType.CoinsPurchase)
			{
				if (mPrice > UserDataController.GetCoins())
				{
					return false;
				}
				if (UserDataController.EnableCape(name))
				{
					this.SubCoins(mPrice);
					return true;
				}
				return false;
			}
			else
			{
				if (capeItemInfo.mPurchasedType != GItemPurchaseType.GemPurchase)
				{
					return false;
				}
				if (mPrice > UserDataController.GetGems())
				{
					return false;
				}
				if (UserDataController.EnableCape(name))
				{
					this.SubGems(mPrice);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00055700 File Offset: 0x00053B00
		public string GetCurSettedCapeName()
		{
			return UserDataController.GetCurSettedCapeName();
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00055707 File Offset: 0x00053B07
		public void SetCurSettedCape(string name)
		{
			UserDataController.SetCurSettedCape(name);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0005570F File Offset: 0x00053B0F
		public string[] GetUserAllEnabledCapeName()
		{
			return UserDataController.GetUserAllEnabledCapeName();
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00055718 File Offset: 0x00053B18
		public GBootItemInfo GetBootItemInfo(string name)
		{
			GItemId id = this.mItemBaseValue.mItemDict_kName_vId[name];
			return UserDataController.GetBootItemInfo(name, id);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0005573E File Offset: 0x00053B3E
		public GBootItemInfo GetCustomBootItemInfo(string name)
		{
			return UserDataController.GetBootItemInfo(name, new GItemId(7, 4, 9999, 0));
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00055754 File Offset: 0x00053B54
		public bool EnableBoot(string name)
		{
			GBootItemInfo bootItemInfo = this.GetBootItemInfo(name);
			int mPrice = bootItemInfo.mPrice;
			if (bootItemInfo.mPurchasedType == GItemPurchaseType.CoinsPurchase)
			{
				if (mPrice > UserDataController.GetCoins())
				{
					return false;
				}
				if (UserDataController.EnableBoot(name))
				{
					this.SubCoins(mPrice);
					return true;
				}
				return false;
			}
			else
			{
				if (bootItemInfo.mPurchasedType != GItemPurchaseType.GemPurchase)
				{
					return false;
				}
				if (mPrice > UserDataController.GetGems())
				{
					return false;
				}
				if (UserDataController.EnableBoot(name))
				{
					this.SubGems(mPrice);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000557D0 File Offset: 0x00053BD0
		public string GetCurSettedBootName()
		{
			return UserDataController.GetCurSettedBootName();
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000557D7 File Offset: 0x00053BD7
		public void SetCurSettedBoot(string name)
		{
			UserDataController.SetCurSettedBoot(name);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000557DF File Offset: 0x00053BDF
		public string[] GetUserAllEnabledBootName()
		{
			return UserDataController.GetUserAllEnabledBootName();
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000557E6 File Offset: 0x00053BE6
		public Dictionary<int, string> GetWeaponNameDic()
		{
			return UserDataController.GetWeaponNameDic();
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000557ED File Offset: 0x00053BED
		public string[] GetCurEquippedWeaponNameList()
		{
			return UserDataController.GetCurEquippedWeaponNameList();
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000557F4 File Offset: 0x00053BF4
		public string[] GetCurEquippedWeaponNameListForStore()
		{
			return UserDataController.GetCurEquippedWeaponNameListForStore();
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000557FB File Offset: 0x00053BFB
		public int GetCurWeaponEquipLimitedNum()
		{
			return UserDataController.GetCurWeaponEquipLimitedNum();
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00055802 File Offset: 0x00053C02
		public void SetCurWeaponEquipLimitedNum(int num)
		{
			UserDataController.SetCurWeaponEquipLimitedNum(num);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0005580A File Offset: 0x00053C0A
		public void ProcessOneWeaponEquipTap(string tapedWeaponName)
		{
			UserDataController.ProcessOneWeaponEquipTap(tapedWeaponName);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00055812 File Offset: 0x00053C12
		public int GetCurWeaponEquipLimitedUnlockLevel()
		{
			return UserDataController.GetCurWeaponEquipLimitedUnlockLevel();
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00055819 File Offset: 0x00053C19
		public void SetFightingStatisticsValue(FightingStatisticsTag tag, int newValue)
		{
			UserDataController.SetFightingStatisticsValue(tag, newValue);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00055822 File Offset: 0x00053C22
		public int GetFightingStatisticsValue(FightingStatisticsTag tag)
		{
			return UserDataController.GetFightingStatisticsValue(tag);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0005582A File Offset: 0x00053C2A
		public int GetMultiplayerBuffNum(string name)
		{
			return UserDataController.GetMultiplayerBuffNum(name);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00055832 File Offset: 0x00053C32
		public void SetMultiplayerBuffNum(string name, int newNum)
		{
			if (newNum > UserDataController.GetMultiplayerBuffNum(name))
			{
				base.GetComponent<AudioSource>().PlayOneShot(this.buffIn);
			}
			UserDataController.SetMultiplayerBuffNum(name, newNum);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00055858 File Offset: 0x00053C58
		public void SetMultiplayerBuffNum(string name, int newNum, bool ignoreSoundEffect)
		{
			if (newNum > UserDataController.GetMultiplayerBuffNum(name) && !ignoreSoundEffect)
			{
				base.GetComponent<AudioSource>().PlayOneShot(this.buffIn);
			}
			UserDataController.SetMultiplayerBuffNum(name, newNum);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00055884 File Offset: 0x00053C84
		public GMultiplayerBuffItemInfo GetMultiplayerBuffItemInfoByName(string name)
		{
			GItemId id = this.mItemBaseValue.mItemDict_kName_vId[name];
			return UserDataController.GetMultiplayerBuffItemInfo(name, id);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x000558AA File Offset: 0x00053CAA
		public void ClearAllBuffEffect()
		{
			this.playerEnchantmentProperty.ClearPotionAddition();
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000558B8 File Offset: 0x00053CB8
		private void Awake()
		{
			this.mExpCalcer = new ExpCalcer();
			this.mRewardCalcer = new RewardCalcer();
			this.mItemBaseValue = new ItemBaseValue();
			GrowthManger.mInstance = this;
			this.DailyRefreshADSData();
			UserDataController.FirstLoadSetting();
			this.mExpCalcer.AddCharacterExpAndChkLevelUp(0);
			UserDataController.SetCharacterLevel(UserDataController.GetCharacterLevel());
			UserDataController.SetGems(UserDataController.GetGems());
			UserDataController.SetCoins(UserDataController.GetCoins());
			UserDataController.SetFightingStatisticsValue(FightingStatisticsTag.tSeasonScore, UserDataController.GetFightingStatisticsValue(FightingStatisticsTag.tSeasonScore));
			UserDataController.SetHonorPoint(UserDataController.GetHonorPoint());
			UserDataController.SetSeasonMark(UserDataController.GetSeasonMark());
			this.mSystemMsgList = new List<string>();
			if (GameObject.Find(base.gameObject.name + "_old") != null)
			{
				List<string> list = GameObject.Find(base.gameObject.name + "_old").GetComponent<GrowthManger>().mSystemMsgList;
				if (list.Count > 0)
				{
					string[] array = new string[list.Count];
					list.CopyTo(array);
					for (int i = 0; i < array.Length; i++)
					{
						this.mSystemMsgList.Add(array[i]);
					}
				}
				UnityEngine.Object.Destroy(GameObject.Find(base.gameObject.name + "_old"));
			}
			else
			{
				UserDataController.StatisticsChkPerLogin();
				string preLoginDateTime = UserDataController.GetPreLoginDateTime();
				string curLoginDateTime = UserDataController.GetCurLoginDateTime();
				DateTime dateTime = new DateTime(int.Parse(preLoginDateTime.Substring(0, 4)), int.Parse(preLoginDateTime.Substring(4, 2)), int.Parse(preLoginDateTime.Substring(6, 2)), int.Parse(preLoginDateTime.Substring(8, 2)), int.Parse(preLoginDateTime.Substring(10, 2)), int.Parse(preLoginDateTime.Substring(12, 2)));
				DateTime dateTime2 = new DateTime(int.Parse(curLoginDateTime.Substring(0, 4)), int.Parse(curLoginDateTime.Substring(4, 2)), int.Parse(curLoginDateTime.Substring(6, 2)), int.Parse(curLoginDateTime.Substring(8, 2)), int.Parse(curLoginDateTime.Substring(10, 2)), int.Parse(curLoginDateTime.Substring(12, 2)));
				if (!dateTime2.Date.Equals(dateTime.Date))
				{
					this.ResetDailyRewardRecord(FightingStatisticsTag.tDailyKillInDeathMatchMode);
					this.ResetDailyRewardRecord(FightingStatisticsTag.tDailyJoinInStrongholdMode);
					this.ResetDailyRewardRecord(FightingStatisticsTag.tDailyJoinInKillingCompetitionMode);
					this.ResetDailyRewardRecord(FightingStatisticsTag.tDailyJoinInExplosionMode);
					this.ResetDailyRewardRecord(FightingStatisticsTag.tDailyJoinInMutationMode);
					this.ResetDailyRewardRecord(FightingStatisticsTag.tDailyVideoShare);
					if (UserDataController.GetHuntingTickets() < 3)
					{
						UserDataController.SetHuntingTickets(3);
					}
					if (!dateTime.AddDays(1.0).Date.Equals(dateTime2.Date))
					{
						this.ResetDailyRewardRecord(FightingStatisticsTag.tDailyLoginInSevenDays);
						this.SetFightingStatisticsValue(FightingStatisticsTag.tDailyLoginInSevenDays, 1);
					}
					else
					{
						int fightingStatisticsValue = this.GetFightingStatisticsValue(FightingStatisticsTag.tDailyLoginInSevenDays);
						if (fightingStatisticsValue >= 7)
						{
							if (UserDataController.HasEverGotFightingStatisticsReward(FightingStatisticsTag.tDailyLoginInSevenDays, 7))
							{
								this.ResetDailyRewardRecord(FightingStatisticsTag.tDailyLoginInSevenDays);
								this.SetFightingStatisticsValue(FightingStatisticsTag.tDailyLoginInSevenDays, 1);
							}
						}
						else
						{
							this.SetFightingStatisticsValue(FightingStatisticsTag.tDailyLoginInSevenDays, fightingStatisticsValue + 1);
						}
					}
				}
			}
			base.gameObject.name = base.gameObject.name + "_old";
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			UserDataController.SetRoleName(UIUserDataController.GetDefaultRoleName());
			this.mSystemMsgList.Add(UserDataController.GetRandomTips());
			this.playerEnchantmentProperty = new PlayerEnchantmentProperty();
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00055BFB File Offset: 0x00053FFB
		private void OnDestroy()
		{
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00055BFD File Offset: 0x00053FFD
		public bool NeedRefreshDataDisplay()
		{
			return this.needRefreshDataDisplay;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00055C05 File Offset: 0x00054005
		public void SetDataDisplayRefreshFlag(bool b)
		{
			this.needRefreshDataDisplay = b;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00055C10 File Offset: 0x00054010
		private void Start()
		{
			this.allWeaponItemInfoList = new List<GWeaponItemInfo>();
			for (int i = 0; i < UserDataController.AllWeaponNameList.Length; i++)
			{
				this.allWeaponItemInfoList.Add(this.GetWeaponItemInfoByName(UserDataController.AllWeaponNameList[i]));
			}
			this.allEquippedWeaponItemInfoList = new List<GWeaponItemInfo>();
			string[] curEquippedWeaponNameList = UserDataController.GetCurEquippedWeaponNameList();
			for (int j = 0; j < curEquippedWeaponNameList.Length; j++)
			{
				this.allEquippedWeaponItemInfoList.Add(this.GetWeaponItemInfoByName(curEquippedWeaponNameList[j]));
			}
			this.allArmorItemInfoList = new List<GArmorItemInfo>();
			for (int k = 0; k < UserDataController.AllArmorNameList.Length; k++)
			{
				this.allArmorItemInfoList.Add(this.GetArmorItemInfoByName(UserDataController.AllArmorNameList[k]));
			}
			this.curSettedArmorInfo = this.GetCurSettedArmorInfo();
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00055CD8 File Offset: 0x000540D8
		private void Update()
		{
			if (Application.loadedLevelName == "UILogin")
			{
				UnityEngine.Object.DestroyImmediate(base.gameObject);
				return;
			}
			this.timeTickCount += Time.deltaTime;
			if (this.timeTickCount >= this.DEALTA_COUNT)
			{
				this.playerEnchantmentProperty.Update(this.timeTickCount);
				if (Application.loadedLevelName.Contains("MGameScene"))
				{
					bool flag = false;
					foreach (GWeaponItemInfo gweaponItemInfo in this.allEquippedWeaponItemInfoList)
					{
						if (gweaponItemInfo.GetCachedWeaponTimeRest(GWeaponRechargeType.WeaponTime) <= 0f)
						{
							gweaponItemInfo.WriteDataFromCacheToDisk();
							this.ProcessOneWeaponEquipTap(gweaponItemInfo.mName);
							this.mSystemMsgList.Add(gweaponItemInfo.mNameDisplay + " is out of date! Perhaps the permanent purchase is better!");
							flag = true;
						}
						if (!gweaponItemInfo.mIsNoLimitedUse)
						{
							gweaponItemInfo.CachedWeaponTimeDecreaseUpdate(this.timeTickCount, GWeaponRechargeType.WeaponTime);
						}
						gweaponItemInfo.CachedWeaponTimeDecreaseUpdate(this.timeTickCount, GWeaponRechargeType.WeaponPlusTime);
					}
					if (flag)
					{
						this.allEquippedWeaponItemInfoList.Clear();
						string[] curEquippedWeaponNameList = UserDataController.GetCurEquippedWeaponNameList();
						for (int i = 0; i < curEquippedWeaponNameList.Length; i++)
						{
							this.allEquippedWeaponItemInfoList.Add(this.GetWeaponItemInfoByName(curEquippedWeaponNameList[i]));
						}
					}
					this.curSettedArmorInfo.CachedAutoSupplyTimeDecreaseUpdate(this.timeTickCount);
				}
				else
				{
					this.curSettedArmorInfo.WriteDataFromCacheToDisk();
					foreach (GWeaponItemInfo gweaponItemInfo2 in this.allEquippedWeaponItemInfoList)
					{
						gweaponItemInfo2.WriteDataFromCacheToDisk();
					}
				}
				this.timeTickCount = 0f;
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00055EBC File Offset: 0x000542BC
		private void OnDisable()
		{
			this.curSettedArmorInfo.WriteDataFromCacheToDisk();
			foreach (GWeaponItemInfo gweaponItemInfo in this.allEquippedWeaponItemInfoList)
			{
				gweaponItemInfo.WriteDataFromCacheToDisk();
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00055F24 File Offset: 0x00054324
		private void OnApplicationPause(bool pauseStatus)
		{
			if (this.curSettedArmorInfo != null)
			{
				this.curSettedArmorInfo.WriteDataFromCacheToDisk();
			}
			if (this.allEquippedWeaponItemInfoList != null)
			{
				foreach (GWeaponItemInfo gweaponItemInfo in this.allEquippedWeaponItemInfoList)
				{
					gweaponItemInfo.WriteDataFromCacheToDisk();
				}
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00055FA0 File Offset: 0x000543A0
		private void DailyRefreshADSData()
		{
			string everyDayFreeADSDateTime = UserDataController.GetEveryDayFreeADSDateTime();
			DateTime dateTime = new DateTime(int.Parse(everyDayFreeADSDateTime.Substring(0, 4)), int.Parse(everyDayFreeADSDateTime.Substring(4, 2)), int.Parse(everyDayFreeADSDateTime.Substring(6, 2)), int.Parse(everyDayFreeADSDateTime.Substring(8, 2)), int.Parse(everyDayFreeADSDateTime.Substring(10, 2)), int.Parse(everyDayFreeADSDateTime.Substring(12, 2)));
			if (dateTime.Date.CompareTo(UnbiasedTime.Instance.Now().Date) != 0)
			{
				UserDataController.SetEveryDayFreeADSIndexZero();
				UserDataController.SetEveryDayFreeADSDateTime(DateTime.Now.Date.ToString("yyyyMMddHHmmss"));
			}
		}

		// Token: 0x04000B59 RID: 2905
		public static GrowthManger mInstance;

		// Token: 0x04000B5A RID: 2906
		public ExpCalcer mExpCalcer;

		// Token: 0x04000B5B RID: 2907
		public RewardCalcer mRewardCalcer;

		// Token: 0x04000B5C RID: 2908
		public ItemBaseValue mItemBaseValue;

		// Token: 0x04000B5D RID: 2909
		public AudioClip coinsIn;

		// Token: 0x04000B5E RID: 2910
		public AudioClip coinsOut;

		// Token: 0x04000B5F RID: 2911
		public AudioClip gemsIn;

		// Token: 0x04000B60 RID: 2912
		public AudioClip gemsOut;

		// Token: 0x04000B61 RID: 2913
		public AudioClip buffIn;

		// Token: 0x04000B62 RID: 2914
		public AudioClip buffOut;

		// Token: 0x04000B63 RID: 2915
		public AudioClip giftBoxIn;

		// Token: 0x04000B64 RID: 2916
		public AudioClip giftBoxOut;

		// Token: 0x04000B65 RID: 2917
		public List<string> mSystemMsgList;

		// Token: 0x04000B69 RID: 2921
		public List<float> mBuffRestTimeListInMultiplayer;

		// Token: 0x04000B6A RID: 2922
		public PlayerEnchantmentProperty playerEnchantmentProperty;

		// Token: 0x04000B6B RID: 2923
		private bool needRefreshDataDisplay;

		// Token: 0x04000B6C RID: 2924
		private List<GWeaponItemInfo> allWeaponItemInfoList;

		// Token: 0x04000B6D RID: 2925
		private List<GWeaponItemInfo> allEquippedWeaponItemInfoList;

		// Token: 0x04000B6E RID: 2926
		private List<GArmorItemInfo> allArmorItemInfoList;

		// Token: 0x04000B6F RID: 2927
		private readonly float DEALTA_COUNT = 1f;

		// Token: 0x04000B70 RID: 2928
		private float timeTickCount;

		// Token: 0x04000B71 RID: 2929
		private GArmorItemInfo curSettedArmorInfo;

		// Token: 0x020001A0 RID: 416
		// (Invoke) Token: 0x06000C01 RID: 3073
		public delegate void CharacterLevelUpEventHandler(int curLevel);

		// Token: 0x020001A1 RID: 417
		// (Invoke) Token: 0x06000C05 RID: 3077
		public delegate void GrowthPromptEventHandler(GrowthPrometType type, int num, string description);

		// Token: 0x020001A2 RID: 418
		// (Invoke) Token: 0x06000C09 RID: 3081
		public delegate void ScenePropsInvalidEventHandler(SceneEnchantmentProps type);
	}
}
