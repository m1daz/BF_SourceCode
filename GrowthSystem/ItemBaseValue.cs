using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrowthSystem
{
	// Token: 0x020001C9 RID: 457
	public class ItemBaseValue
	{
		// Token: 0x06000C0D RID: 3085 RVA: 0x0005607C File Offset: 0x0005447C
		public ItemBaseValue()
		{
			this.InitItemIdDict();
			this.InitWeaponRankDict();
			this.InitWeaponEnablePriceDict();
			this.InitWeaponToAdvancedPriceDict();
			this.InitWeaponToPerfectPriceDict();
			this.InitWeaponUnlockCLevelDict();
			this.InitArmorRankDict();
			this.InitArmorEnablePriceDict();
			this.InitArmorUnlockCLevelDict();
			this.InitGunPartsRankDict();
			this.InitGunPatsEnablePriceDict();
			this.InitGameAidUpgradePriceDict();
			this.InitInGameItemPriceDict();
			this.InitGameModeUnlockPriceDict();
			this.InitGameAidUnlockCLevelDict();
			this.InitInGameItemUnlockCLevelDict();
			this.InitGameModeUnlockCLevelDict();
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x000561D4 File Offset: 0x000545D4
		private void InitItemIdDict()
		{
			for (int i = 1; i <= 10; i++)
			{
				for (int j = 1; j <= 10; j++)
				{
					if (Type.GetType("GrowthSystem.GItemIdTag_3_" + i.ToString() + "_" + j.ToString()) != null)
					{
						int length = Enum.GetNames(Type.GetType("GrowthSystem.GItemIdTag_3_" + i.ToString() + "_" + j.ToString())).GetLength(0);
						for (int k = 0; k < length; k++)
						{
							string key = Enum.GetNames(Type.GetType("GrowthSystem.GItemIdTag_3_" + i.ToString() + "_" + j.ToString()))[k];
							int i_ = (int)Enum.GetValues(Type.GetType("GrowthSystem.GItemIdTag_3_" + i.ToString() + "_" + j.ToString())).GetValue(k);
							this.mItemDict_kName_vId.Add(key, new GItemId(i, j, i_, 0));
							if (i == 1)
							{
								this.mWeaponDict_kName_vType.Add(key, Enum.GetNames(Type.GetType("GrowthSystem.GItemIdTag_2_1"))[j - 1]);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0005633C File Offset: 0x0005473C
		private void InitItemNameDict()
		{
			for (int i = 1; i <= 10; i++)
			{
				for (int j = 1; j <= 10; j++)
				{
					if (Type.GetType("GrowthSystem.GItemIdTag_3_" + i.ToString() + "_" + j.ToString()) != null)
					{
						int length = Enum.GetNames(Type.GetType("GrowthSystem.GItemIdTag_3_" + i.ToString() + "_" + j.ToString())).GetLength(0);
						for (int k = 0; k < length; k++)
						{
							string value = Enum.GetNames(Type.GetType("GrowthSystem.GItemIdTag_3_" + i.ToString() + "_" + j.ToString()))[k];
							int i_ = (int)Enum.GetValues(Type.GetType("GrowthSystem.GItemIdTag_3_" + i.ToString() + "_" + j.ToString())).GetValue(k);
							this.mItemDict_kId_vName.Add(new GItemId(i, j, i_, 0), value);
						}
					}
				}
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0005647C File Offset: 0x0005487C
		private void InitWeaponRankDict()
		{
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 1, 1, 0), 1);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 1, 2, 0), 6);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 1, 3, 0), 15);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 2, 1, 0), 1);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 2, 2, 0), 3);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 2, 3, 0), 6);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 2, 4, 0), 9);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 2, 5, 0), 12);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 2, 6, 0), 15);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 3, 1, 0), 1);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 3, 2, 0), 2);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 3, 3, 0), 4);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 3, 4, 0), 7);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 3, 5, 0), 10);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 3, 6, 0), 12);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 3, 7, 0), 15);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 4, 1, 0), 3);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 4, 2, 0), 8);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 4, 3, 0), 13);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 5, 1, 0), 3);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 5, 2, 0), 5);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 5, 3, 0), 11);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 5, 4, 0), 14);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 6, 1, 0), 4);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 6, 2, 0), 9);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 6, 3, 0), 15);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 7, 1, 0), 2);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 7, 2, 0), 8);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 7, 3, 0), 11);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 8, 1, 0), 6);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 8, 2, 0), 10);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 8, 3, 0), 15);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 9, 1, 0), 2);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 9, 2, 0), 5);
			this.mWeaponDict_kId_vRank.Add(new GItemId(1, 9, 3, 0), 13);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0005677C File Offset: 0x00054B7C
		private void InitWeaponEnablePriceDict()
		{
			this.mWeaponDict_kRank_vEnablePrice.Add(1, 0);
			this.mWeaponDict_kRank_vEnablePrice.Add(2, 300);
			this.mWeaponDict_kRank_vEnablePrice.Add(3, 600);
			this.mWeaponDict_kRank_vEnablePrice.Add(4, 1000);
			this.mWeaponDict_kRank_vEnablePrice.Add(5, 1600);
			this.mWeaponDict_kRank_vEnablePrice.Add(6, 2200);
			this.mWeaponDict_kRank_vEnablePrice.Add(7, 2800);
			this.mWeaponDict_kRank_vEnablePrice.Add(8, 3600);
			this.mWeaponDict_kRank_vEnablePrice.Add(9, 4400);
			this.mWeaponDict_kRank_vEnablePrice.Add(10, 5200);
			this.mWeaponDict_kRank_vEnablePrice.Add(11, 6000);
			this.mWeaponDict_kRank_vEnablePrice.Add(12, 6800);
			this.mWeaponDict_kRank_vEnablePrice.Add(13, 7600);
			this.mWeaponDict_kRank_vEnablePrice.Add(14, 8200);
			this.mWeaponDict_kRank_vEnablePrice.Add(15, 10000);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0005688C File Offset: 0x00054C8C
		private void InitWeaponToAdvancedPriceDict()
		{
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(1, 100);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(2, 100);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(3, 200);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(4, 300);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(5, 500);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(6, 700);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(7, 900);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(8, 1200);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(9, 1400);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(10, 1700);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(11, 2000);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(12, 2200);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(13, 2500);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(14, 2700);
			this.mWeaponDict_kRank_vToAdvancedPrice.Add(15, 3500);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0005699C File Offset: 0x00054D9C
		private void InitWeaponToPerfectPriceDict()
		{
			this.mWeaponDict_kRank_vToPerfectPrice.Add(1, 300);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(2, 300);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(3, 600);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(4, 1000);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(5, 1600);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(6, 2200);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(7, 2800);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(8, 3600);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(9, 4400);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(10, 5200);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(11, 6000);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(12, 6800);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(13, 7600);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(14, 8200);
			this.mWeaponDict_kRank_vToPerfectPrice.Add(15, 10000);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00056AB0 File Offset: 0x00054EB0
		private void InitWeaponUnlockCLevelDict()
		{
			this.mWeaponDict_kRank_vUnlockCLevel.Add(1, 1);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(2, 3);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(3, 6);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(4, 10);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(5, 16);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(6, 22);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(7, 28);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(8, 36);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(9, 44);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(10, 52);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(11, 60);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(12, 68);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(13, 76);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(14, 82);
			this.mWeaponDict_kRank_vUnlockCLevel.Add(15, 100);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00056B94 File Offset: 0x00054F94
		private void InitArmorRankDict()
		{
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 1, 1, 0), 1);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 1, 2, 0), 2);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 1, 3, 0), 4);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 1, 4, 0), 6);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 1, 5, 0), 8);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 1, 6, 0), 9);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 1, 7, 0), 12);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 1, 8, 0), 15);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 2, 1, 0), 1);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 2, 2, 0), 3);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 2, 3, 0), 5);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 2, 4, 0), 10);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 2, 5, 0), 13);
			this.mArmorDict_kId_vRank.Add(new GItemId(2, 2, 6, 0), 15);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00056CD0 File Offset: 0x000550D0
		private void InitArmorEnablePriceDict()
		{
			this.mArmorDict_kRank_vEnablePrice.Add(1, 0);
			this.mArmorDict_kRank_vEnablePrice.Add(2, 300);
			this.mArmorDict_kRank_vEnablePrice.Add(3, 600);
			this.mArmorDict_kRank_vEnablePrice.Add(4, 1000);
			this.mArmorDict_kRank_vEnablePrice.Add(5, 1600);
			this.mArmorDict_kRank_vEnablePrice.Add(6, 2200);
			this.mArmorDict_kRank_vEnablePrice.Add(7, 2800);
			this.mArmorDict_kRank_vEnablePrice.Add(8, 3600);
			this.mArmorDict_kRank_vEnablePrice.Add(9, 4400);
			this.mArmorDict_kRank_vEnablePrice.Add(10, 5200);
			this.mArmorDict_kRank_vEnablePrice.Add(11, 6000);
			this.mArmorDict_kRank_vEnablePrice.Add(12, 6800);
			this.mArmorDict_kRank_vEnablePrice.Add(13, 7600);
			this.mArmorDict_kRank_vEnablePrice.Add(14, 8200);
			this.mArmorDict_kRank_vEnablePrice.Add(15, 10000);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00056DE0 File Offset: 0x000551E0
		private void InitArmorUnlockCLevelDict()
		{
			this.mArmorDict_kRank_vUnlockCLevel.Add(1, 1);
			this.mArmorDict_kRank_vUnlockCLevel.Add(2, 3);
			this.mArmorDict_kRank_vUnlockCLevel.Add(3, 6);
			this.mArmorDict_kRank_vUnlockCLevel.Add(4, 10);
			this.mArmorDict_kRank_vUnlockCLevel.Add(5, 16);
			this.mArmorDict_kRank_vUnlockCLevel.Add(6, 22);
			this.mArmorDict_kRank_vUnlockCLevel.Add(7, 28);
			this.mArmorDict_kRank_vUnlockCLevel.Add(8, 36);
			this.mArmorDict_kRank_vUnlockCLevel.Add(9, 44);
			this.mArmorDict_kRank_vUnlockCLevel.Add(10, 52);
			this.mArmorDict_kRank_vUnlockCLevel.Add(11, 60);
			this.mArmorDict_kRank_vUnlockCLevel.Add(12, 68);
			this.mArmorDict_kRank_vUnlockCLevel.Add(13, 76);
			this.mArmorDict_kRank_vUnlockCLevel.Add(14, 82);
			this.mArmorDict_kRank_vUnlockCLevel.Add(15, 100);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00056EC4 File Offset: 0x000552C4
		private void InitGunPartsRankDict()
		{
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 1, 0), 1);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 2, 0), 2);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 3, 0), 2);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 4, 0), 3);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 5, 0), 3);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 6, 0), 4);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 7, 0), 4);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 8, 0), 5);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 9, 0), 6);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 10, 0), 6);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 11, 0), 7);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 12, 0), 8);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 13, 0), 8);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 14, 0), 9);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 15, 0), 10);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 1, 16, 0), 10);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 1, 0), 1);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 2, 0), 2);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 3, 0), 2);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 4, 0), 3);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 5, 0), 4);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 6, 0), 5);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 7, 0), 6);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 8, 0), 7);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 9, 0), 8);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 10, 0), 9);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 11, 0), 10);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 2, 12, 0), 10);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 3, 1, 0), 1);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 3, 2, 0), 2);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 3, 3, 0), 3);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 3, 4, 0), 4);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 3, 5, 0), 5);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 3, 6, 0), 6);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 3, 7, 0), 7);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 3, 8, 0), 8);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 3, 9, 0), 9);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 3, 10, 0), 10);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 4, 1, 0), 1);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 4, 2, 0), 2);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 4, 3, 0), 3);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 4, 4, 0), 4);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 4, 5, 0), 5);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 4, 6, 0), 6);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 4, 7, 0), 7);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 4, 8, 0), 8);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 4, 9, 0), 9);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 4, 10, 0), 10);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 5, 1, 0), 1);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 5, 2, 0), 2);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 5, 3, 0), 3);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 5, 4, 0), 4);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 5, 5, 0), 5);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 5, 6, 0), 7);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 5, 7, 0), 8);
			this.mGunPartsDict_kId_vRank.Add(new GItemId(3, 5, 8, 0), 10);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00057384 File Offset: 0x00055784
		private void InitGunPatsEnablePriceDict()
		{
			this.mGunPatsDict_kRank_vEnablePrice.Add(1, 0);
			this.mGunPatsDict_kRank_vEnablePrice.Add(2, 200);
			this.mGunPatsDict_kRank_vEnablePrice.Add(3, 350);
			this.mGunPatsDict_kRank_vEnablePrice.Add(4, 600);
			this.mGunPatsDict_kRank_vEnablePrice.Add(5, 900);
			this.mGunPatsDict_kRank_vEnablePrice.Add(6, 1200);
			this.mGunPatsDict_kRank_vEnablePrice.Add(7, 1500);
			this.mGunPatsDict_kRank_vEnablePrice.Add(8, 2000);
			this.mGunPatsDict_kRank_vEnablePrice.Add(9, 2500);
			this.mGunPatsDict_kRank_vEnablePrice.Add(10, 4000);
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0005743C File Offset: 0x0005583C
		private void InitGameAidUpgradePriceDict()
		{
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 1, 1, 0), 0);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 1, 2, 0), 1000);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 1, 3, 0), 3000);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 1, 4, 0), 4000);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 1, 5, 0), 8000);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 1, 6, 0), 10000);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 2, 1, 0), 0);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 2, 2, 0), 800);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 2, 3, 0), 1500);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 2, 4, 0), 2000);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 2, 5, 0), 2500);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 2, 6, 0), 4500);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 3, 1, 0), 0);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 3, 2, 0), 500);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 3, 3, 0), 1000);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 3, 4, 0), 1500);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 3, 5, 0), 2000);
			this.mGameAidDict_kId_vUpgradePrice.Add(new GItemId(4, 3, 6, 0), 4000);
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x000575FF File Offset: 0x000559FF
		private void InitInGameItemPriceDict()
		{
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00057604 File Offset: 0x00055A04
		private void InitGameModeUnlockPriceDict()
		{
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 2, 1, 0), 0);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 2, 2, 0), 2000);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 2, 3, 0), 4000);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 2, 4, 0), 6000);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 3, 1, 0), 0);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 3, 2, 0), 1000);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 3, 3, 0), 2000);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 3, 11, 0), 2000);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 3, 12, 0), 4000);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 4, 1, 0), 0);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 4, 2, 0), 500);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 4, 3, 0), 1000);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 4, 4, 0), 2000);
			this.mGameModeUnlockDict_kId_vPrice.Add(new GItemId(6, 7, 1, 0), 500);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00057768 File Offset: 0x00055B68
		private void InitGameAidUnlockCLevelDict()
		{
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 1, 1, 0), 1);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 1, 2, 0), 10);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 1, 3, 0), 28);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 1, 4, 0), 52);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 1, 5, 0), 82);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 1, 6, 0), 100);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 2, 1, 0), 1);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 2, 2, 0), 10);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 2, 3, 0), 28);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 2, 4, 0), 52);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 2, 5, 0), 82);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 2, 6, 0), 100);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 3, 1, 0), 1);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 3, 2, 0), 10);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 3, 3, 0), 28);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 3, 4, 0), 52);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 3, 5, 0), 82);
			this.mGameAidDict_kId_vUnlockCLevel.Add(new GItemId(4, 3, 6, 0), 100);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000578FE File Offset: 0x00055CFE
		private void InitInGameItemUnlockCLevelDict()
		{
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00057900 File Offset: 0x00055D00
		private void InitGameModeUnlockCLevelDict()
		{
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 2, 1, 0), 1);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 2, 2, 0), 10);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 2, 3, 0), 28);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 2, 4, 0), 52);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 3, 1, 0), 1);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 3, 2, 0), 6);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 3, 3, 0), 22);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 3, 11, 0), 10);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 3, 12, 0), 36);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 4, 1, 0), 1);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 4, 2, 0), 10);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 4, 3, 0), 28);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 4, 4, 0), 52);
			this.mGameModeUnlockDict_kId_vUnlockCLevel.Add(new GItemId(6, 7, 1, 0), 10);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00057A40 File Offset: 0x00055E40
		private void InitUnlockItemsDict()
		{
			for (int i = 1; i <= 100; i++)
			{
				this.mUnlockItemsDict_kCLevel_vIds.Add(i, new List<GItemId>());
			}
			for (int j = 1; j <= 10; j++)
			{
				for (int k = 1; k <= 10; k++)
				{
					if (Type.GetType("GItemIdTag_3_" + j.ToString() + "_" + k.ToString()) != null)
					{
						int length = Enum.GetNames(Type.GetType("GItemIdTag_3_" + j.ToString() + "_" + k.ToString())).GetLength(0);
						for (int l = 0; l < length; l++)
						{
							int i_ = (int)Enum.GetValues(Type.GetType("GItemIdTag_3_" + j.ToString() + "_" + k.ToString())).GetValue(l);
							GItemId gitemId = new GItemId(j, k, i_, 0);
							Debug.Log(string.Concat(new object[]
							{
								gitemId.mId_1,
								"_",
								gitemId.mId_2,
								"_",
								gitemId.mId_3
							}));
							int itemUnlockCLevel = this.GetItemUnlockCLevel(gitemId);
							this.mUnlockItemsDict_kCLevel_vIds[itemUnlockCLevel].Add(gitemId);
						}
					}
				}
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00057BD4 File Offset: 0x00055FD4
		public int GetItemEnablePrice(GItemId id)
		{
			int result = -1;
			switch (id.mId_1)
			{
			case 1:
			{
				int key = this.mWeaponDict_kId_vRank[id];
				result = this.mWeaponDict_kRank_vEnablePrice[key];
				break;
			}
			case 2:
			{
				int key = this.mArmorDict_kId_vRank[id];
				result = this.mArmorDict_kRank_vEnablePrice[key];
				break;
			}
			case 3:
			{
				int key = this.mGunPartsDict_kId_vRank[id];
				result = this.mGunPatsDict_kRank_vEnablePrice[key];
				break;
			}
			case 4:
				result = this.mGameAidDict_kId_vUpgradePrice[id];
				break;
			case 5:
				result = this.mInGameItemDict_kId_vPrice[id];
				break;
			case 6:
				result = this.mGameModeUnlockDict_kId_vPrice[id];
				break;
			}
			return result;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00057CAC File Offset: 0x000560AC
		public int GetWeaponUpgradePrice(GItemId id, int toLevel)
		{
			int result = -1;
			int key = this.mWeaponDict_kId_vRank[id];
			if (toLevel != 1)
			{
				if (toLevel == 2)
				{
					result = this.mWeaponDict_kRank_vToPerfectPrice[key];
				}
			}
			else
			{
				result = this.mWeaponDict_kRank_vToAdvancedPrice[key];
			}
			return result;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00057D08 File Offset: 0x00056108
		public int GetConsumableItemPrice(GItemId id)
		{
			return this.mInGameItemDict_kId_vPrice[id];
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00057D28 File Offset: 0x00056128
		private int GetItemUnlockCLevel(GItemId id)
		{
			int result = -1;
			switch (id.mId_1)
			{
			case 1:
			{
				int key = this.mWeaponDict_kId_vRank[id];
				result = this.mWeaponDict_kRank_vUnlockCLevel[key];
				break;
			}
			case 2:
			{
				int key = this.mArmorDict_kId_vRank[id];
				result = this.mArmorDict_kRank_vUnlockCLevel[key];
				break;
			}
			case 4:
				result = this.mGameAidDict_kId_vUnlockCLevel[id];
				break;
			case 5:
				result = this.mInGameItemDict_kId_vUnlockCLevel[id];
				break;
			case 6:
				result = this.mGameModeUnlockDict_kId_vUnlockCLevel[id];
				break;
			}
			return result;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00057DE4 File Offset: 0x000561E4
		public List<GItemId> GetUnlockItems(int cLevel)
		{
			return this.mUnlockItemsDict_kCLevel_vIds[cLevel];
		}

		// Token: 0x04000CDC RID: 3292
		public Dictionary<string, GItemId> mItemDict_kName_vId = new Dictionary<string, GItemId>();

		// Token: 0x04000CDD RID: 3293
		public Dictionary<GItemId, string> mItemDict_kId_vName = new Dictionary<GItemId, string>();

		// Token: 0x04000CDE RID: 3294
		public Dictionary<string, string> mWeaponDict_kName_vType = new Dictionary<string, string>();

		// Token: 0x04000CDF RID: 3295
		public Dictionary<GItemId, int> mWeaponDict_kId_vRank = new Dictionary<GItemId, int>();

		// Token: 0x04000CE0 RID: 3296
		public Dictionary<GItemId, int> mArmorDict_kId_vRank = new Dictionary<GItemId, int>();

		// Token: 0x04000CE1 RID: 3297
		public Dictionary<GItemId, int> mGunPartsDict_kId_vRank = new Dictionary<GItemId, int>();

		// Token: 0x04000CE2 RID: 3298
		public Dictionary<int, int> mWeaponDict_kRank_vEnablePrice = new Dictionary<int, int>();

		// Token: 0x04000CE3 RID: 3299
		public Dictionary<int, int> mWeaponDict_kRank_vToAdvancedPrice = new Dictionary<int, int>();

		// Token: 0x04000CE4 RID: 3300
		public Dictionary<int, int> mWeaponDict_kRank_vToPerfectPrice = new Dictionary<int, int>();

		// Token: 0x04000CE5 RID: 3301
		public Dictionary<int, int> mArmorDict_kRank_vEnablePrice = new Dictionary<int, int>();

		// Token: 0x04000CE6 RID: 3302
		public Dictionary<int, int> mGunPatsDict_kRank_vEnablePrice = new Dictionary<int, int>();

		// Token: 0x04000CE7 RID: 3303
		public Dictionary<int, int> mWeaponDict_kRank_vUnlockCLevel = new Dictionary<int, int>();

		// Token: 0x04000CE8 RID: 3304
		public Dictionary<int, int> mArmorDict_kRank_vUnlockCLevel = new Dictionary<int, int>();

		// Token: 0x04000CE9 RID: 3305
		public Dictionary<GItemId, int> mGameAidDict_kId_vUpgradePrice = new Dictionary<GItemId, int>();

		// Token: 0x04000CEA RID: 3306
		public Dictionary<GItemId, int> mInGameItemDict_kId_vPrice = new Dictionary<GItemId, int>();

		// Token: 0x04000CEB RID: 3307
		public Dictionary<GItemId, int> mGameModeUnlockDict_kId_vPrice = new Dictionary<GItemId, int>();

		// Token: 0x04000CEC RID: 3308
		public Dictionary<GItemId, int> mGameAidDict_kId_vUnlockCLevel = new Dictionary<GItemId, int>();

		// Token: 0x04000CED RID: 3309
		public Dictionary<GItemId, int> mInGameItemDict_kId_vUnlockCLevel = new Dictionary<GItemId, int>();

		// Token: 0x04000CEE RID: 3310
		public Dictionary<GItemId, int> mGameModeUnlockDict_kId_vUnlockCLevel = new Dictionary<GItemId, int>();

		// Token: 0x04000CEF RID: 3311
		public Dictionary<int, List<GItemId>> mUnlockItemsDict_kCLevel_vIds = new Dictionary<int, List<GItemId>>();
	}
}
