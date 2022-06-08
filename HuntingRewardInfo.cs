using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200019B RID: 411
public class HuntingRewardInfo
{
	// Token: 0x06000AF2 RID: 2802 RVA: 0x0004F574 File Offset: 0x0004D974
	public HuntingRewardInfo()
	{
		this.strItems = new List<string>();
		this.generalItems = new List<HuntingRewardInfo.SingleReward>();
		this.lotteryItems = new List<HuntingRewardInfo.SingleReward>();
		this.lotteryCount = 0;
		this.isLotteryRound = false;
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x000501E4 File Offset: 0x0004E5E4
	public HuntingRewardInfo(int sceneId, int difficulty, int roundId, int maxRoundNum, GrowthGameRatingTag rating)
	{
		this.strItems = new List<string>();
		this.generalItems = new List<HuntingRewardInfo.SingleReward>();
		this.lotteryItems = new List<HuntingRewardInfo.SingleReward>();
		this.lotteryCount = 0;
		this.isLotteryRound = false;
		this.rating = rating;
		this.sceneId = sceneId;
		this.difficulty = difficulty;
		this.roundId = roundId;
		this.maxRoundNum = maxRoundNum;
		this.starRating = ((rating != GrowthGameRatingTag.RatingTag_S) ? ((rating != GrowthGameRatingTag.RatingTag_A) ? ((rating != GrowthGameRatingTag.RatingTag_B) ? 0 : 1) : 2) : 3);
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x00050EAC File Offset: 0x0004F2AC
	public static List<HuntingRewardInfo.RecommendItem> GetRecommendItems()
	{
		int num = UnityEngine.Random.Range(1, 3);
		string[] array;
		if (num != 1)
		{
			if (num != 2)
			{
				array = new string[]
				{
					"Hat_1_SlotLogo",
					"Boot_1_SlotLogo",
					"Cape_1_SlotLogo",
					"M134_SlotLogo"
				};
			}
			else
			{
				array = new string[]
				{
					"Hat_3_SlotLogo",
					"Boot_2_SlotLogo",
					"Cape_2_SlotLogo",
					"BurstRG2_SlotLogo"
				};
			}
		}
		else
		{
			array = new string[]
			{
				"Hat_1_SlotLogo",
				"Boot_1_SlotLogo",
				"Cape_1_SlotLogo",
				"M134_SlotLogo"
			};
		}
		List<HuntingRewardInfo.RecommendItem> list = new List<HuntingRewardInfo.RecommendItem>();
		for (int i = 0; i < array.Length; i++)
		{
			list.Add(new HuntingRewardInfo.RecommendItem(array[i]));
		}
		return list;
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x00050F84 File Offset: 0x0004F384
	public void SteppedReceiveLotteryReward()
	{
		if (this.lotteryCount < this.maxValidLotteryCount)
		{
			if (this.lotteryCount >= this.maxFreeLotteryCount)
			{
				GrowthManagerKit.SubGems(this.paidLotteryPrice);
			}
			this.ReceiveItemReward(this.lotteryItems[this.lotteryCount].configStr);
			this.lotteryCount++;
		}
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x00050FEC File Offset: 0x0004F3EC
	public void GenLotteryReward()
	{
		if (this.roundId >= this.maxRoundNum)
		{
			this.isLotteryRound = true;
			this.lotteryCount = 0;
			if (this.rating == GrowthGameRatingTag.RatingTag_S)
			{
				string[] array = new string[this.maxValidLotteryCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.itemList_Lottery_S[UnityEngine.Random.Range(0, this.itemList_Lottery_S.Length)];
					array[i] = this.FixItemConfig(array[i]);
					this.lotteryItems.Add(new HuntingRewardInfo.SingleReward(this.GetItemSpriteName(array[i]), this.GetItemNum(array[i]), array[i]));
				}
			}
			else if (this.rating == GrowthGameRatingTag.RatingTag_A)
			{
				string[] array2 = new string[this.maxValidLotteryCount];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = this.itemList_Lottery_A[UnityEngine.Random.Range(0, this.itemList_Lottery_A.Length)];
					array2[j] = this.FixItemConfig(array2[j]);
					this.lotteryItems.Add(new HuntingRewardInfo.SingleReward(this.GetItemSpriteName(array2[j]), this.GetItemNum(array2[j]), array2[j]));
				}
			}
			else if (this.rating == GrowthGameRatingTag.RatingTag_B)
			{
				string[] array3 = new string[this.maxValidLotteryCount];
				for (int k = 0; k < array3.Length; k++)
				{
					array3[k] = this.itemList_Lottery_B[UnityEngine.Random.Range(0, this.itemList_Lottery_B.Length)];
					array3[k] = this.FixItemConfig(array3[k]);
					this.lotteryItems.Add(new HuntingRewardInfo.SingleReward(this.GetItemSpriteName(array3[k]), this.GetItemNum(array3[k]), array3[k]));
				}
			}
		}
		else
		{
			this.isLotteryRound = false;
		}
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x000511A0 File Offset: 0x0004F5A0
	public void GenGeneralReward()
	{
		if (this.difficulty == 1)
		{
			this.coin = GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tHunting, this.rating);
			this.exp = GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tHunting, this.rating);
			this.generalItems.Add(new HuntingRewardInfo.SingleReward("Coins_SlotLogo", this.coin));
			this.generalItems.Add(new HuntingRewardInfo.SingleReward("Exp_SlotLogo", this.exp));
			GrowthManagerKit.AddCoins(this.coin);
			GrowthManagerKit.AddCharacterExp(this.exp);
			if (this.rating == GrowthGameRatingTag.RatingTag_S)
			{
				if (this.roundId < this.maxRoundNum)
				{
					string text = this.itemList_S[UnityEngine.Random.Range(0, this.itemList_S.Length)];
					text = this.FixItemConfig(text);
					this.generalItems.Add(new HuntingRewardInfo.SingleReward(this.GetItemSpriteName(text), this.GetItemNum(text), text));
					this.ReceiveItemReward(text);
				}
			}
			else if (this.rating == GrowthGameRatingTag.RatingTag_A)
			{
				if (this.roundId < this.maxRoundNum)
				{
					string text2 = this.itemList_S[UnityEngine.Random.Range(0, this.itemList_A.Length)];
					text2 = this.FixItemConfig(text2);
					this.generalItems.Add(new HuntingRewardInfo.SingleReward(this.GetItemSpriteName(text2), this.GetItemNum(text2), text2));
					this.ReceiveItemReward(text2);
				}
			}
			else if (this.rating == GrowthGameRatingTag.RatingTag_B && this.roundId < this.maxRoundNum)
			{
				string text3 = this.itemList_S[UnityEngine.Random.Range(0, this.itemList_B.Length)];
				text3 = this.FixItemConfig(text3);
				this.generalItems.Add(new HuntingRewardInfo.SingleReward(this.GetItemSpriteName(text3), this.GetItemNum(text3), text3));
				this.ReceiveItemReward(text3);
			}
		}
		else
		{
			this.coin = GrowthManagerKit.GetOncePlayCoinsReward(GrowthGameModeTag.tHunting, this.rating) * 2;
			this.exp = GrowthManagerKit.GetOncePlayExpReward(GrowthGameModeTag.tHunting, this.rating) * 2;
			this.generalItems.Add(new HuntingRewardInfo.SingleReward("Coins_SlotLogo", this.coin));
			this.generalItems.Add(new HuntingRewardInfo.SingleReward("Exp_SlotLogo", this.exp));
			GrowthManagerKit.AddCoins(this.coin);
			GrowthManagerKit.AddCharacterExp(this.exp);
			if (this.rating == GrowthGameRatingTag.RatingTag_S)
			{
				this.gem = 3;
				this.generalItems.Add(new HuntingRewardInfo.SingleReward("Gems_SlotLogo", this.gem));
				GrowthManagerKit.AddGems(this.gem);
				if (this.roundId < this.maxRoundNum)
				{
					string text4 = this.itemList_S[UnityEngine.Random.Range(0, this.itemList_S.Length)];
					text4 = this.FixItemConfig(text4);
					this.generalItems.Add(new HuntingRewardInfo.SingleReward(this.GetItemSpriteName(text4), this.GetItemNum(text4), text4));
					this.ReceiveItemReward(text4);
				}
			}
			else if (this.rating == GrowthGameRatingTag.RatingTag_A)
			{
				this.gem = 2;
				this.generalItems.Add(new HuntingRewardInfo.SingleReward("Gems_SlotLogo", this.gem));
				GrowthManagerKit.AddGems(this.gem);
				if (this.roundId < this.maxRoundNum)
				{
					string text5 = this.itemList_S[UnityEngine.Random.Range(0, this.itemList_A.Length)];
					text5 = this.FixItemConfig(text5);
					this.generalItems.Add(new HuntingRewardInfo.SingleReward(this.GetItemSpriteName(text5), this.GetItemNum(text5), text5));
					this.ReceiveItemReward(text5);
				}
			}
			else if (this.rating == GrowthGameRatingTag.RatingTag_B)
			{
				this.gem = 1;
				this.generalItems.Add(new HuntingRewardInfo.SingleReward("Gems_SlotLogo", this.gem));
				GrowthManagerKit.AddGems(this.gem);
				if (this.roundId < this.maxRoundNum)
				{
					string text6 = this.itemList_S[UnityEngine.Random.Range(0, this.itemList_B.Length)];
					text6 = this.FixItemConfig(text6);
					this.generalItems.Add(new HuntingRewardInfo.SingleReward(this.GetItemSpriteName(text6), this.GetItemNum(text6), text6));
					this.ReceiveItemReward(text6);
				}
			}
		}
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x00051594 File Offset: 0x0004F994
	private string FixItemConfig(string itemConfigStr)
	{
		string result = itemConfigStr;
		if (itemConfigStr.Contains("WeaponChip"))
		{
			string name = itemConfigStr.Split(new char[]
			{
				'@'
			})[1].Split(new char[]
			{
				'_'
			})[0];
			int chipIndex = int.Parse(itemConfigStr.Split(new char[]
			{
				'@'
			})[1].Split(new char[]
			{
				'_'
			})[2]);
			if (GrowthManagerKit.GetWeaponItemInfoByName(name).IsChipUnlocked(chipIndex))
			{
				string[] array = new string[]
				{
					"PropertyCard@Power_1@1",
					"PropertyCard@Accuracy_1@1",
					"PropertyCard@Clip_1@1",
					"PropertyCard@Move_1@1",
					"PropertyCard@Energy_1@1",
					"PropertyCard@Range_1@1",
					"PropertyCard@Aim_1@1"
				};
				result = array[UnityEngine.Random.Range(0, array.Length)];
			}
		}
		return result;
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x00051664 File Offset: 0x0004FA64
	private string GetItemSpriteName(string configStr)
	{
		string[] array = configStr.Split(new char[]
		{
			'@'
		});
		string text = array[0];
		string text2 = array[1];
		int num = int.Parse(array[2]);
		string result = string.Empty;
		if (text != null)
		{
			if (!(text == "Base"))
			{
				if (!(text == "Potion"))
				{
					if (!(text == "PropertyCard"))
					{
						if (text == "WeaponChip")
						{
							result = text2 + "_SlotLogo";
						}
					}
					else
					{
						result = text2.Insert(text2.IndexOf('_'), "_SlotLogo");
					}
				}
				else
				{
					result = text2 + "_SlotLogo";
				}
			}
			else
			{
				result = text2 + "_SlotLogo";
			}
		}
		return result;
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x00051740 File Offset: 0x0004FB40
	private int GetItemNum(string configStr)
	{
		string[] array = configStr.Split(new char[]
		{
			'@'
		});
		return int.Parse(array[2]);
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x0005176C File Offset: 0x0004FB6C
	private void ReceiveItemReward(string configStr)
	{
		string[] array = configStr.Split(new char[]
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
					if (!(text == "PropertyCard"))
					{
						if (text == "WeaponChip")
						{
							GrowthManagerKit.GetWeaponItemInfoByName(text2.Split(new char[]
							{
								'_'
							})[0]).UnlockChip(int.Parse(text2.Split(new char[]
							{
								'_'
							})[2]));
						}
					}
					else
					{
						GrowthManagerKit.AddWeaponPropertyCardNum(text2.Split(new char[]
						{
							'_'
						})[0], int.Parse(text2.Split(new char[]
						{
							'_'
						})[1]), num);
					}
				}
				else
				{
					GrowthManagerKit.GetMultiplayerBuffItemInfoByName(text2).AddBuffNum(num);
				}
			}
			else if (text2 != null)
			{
				if (!(text2 == "Coins"))
				{
					if (!(text2 == "GiftBox"))
					{
						if (!(text2 == "Gems"))
						{
							if (text2 == "HuntingTicket")
							{
								GrowthManagerKit.AddHuntingTickets(num);
							}
						}
						else
						{
							GrowthManagerKit.AddGems(num);
						}
					}
					else
					{
						GrowthManagerKit.AddGiftBox(num);
					}
				}
				else
				{
					GrowthManagerKit.AddCoins(num);
				}
			}
		}
	}

	// Token: 0x04000B3C RID: 2876
	private readonly string[] itemList_S = new string[]
	{
		"Base@GiftBox@1",
		"Base@HuntingTicket@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_2@1",
		"PropertyCard@Accuracy_2@1",
		"PropertyCard@Clip_2@1",
		"PropertyCard@Move_2@1",
		"PropertyCard@Energy_2@1",
		"PropertyCard@Range_2@1",
		"PropertyCard@Aim_2@1",
		"PropertyCard@Power_2@1",
		"PropertyCard@Accuracy_2@1",
		"PropertyCard@Clip_2@1",
		"PropertyCard@Move_2@1",
		"PropertyCard@Energy_2@1",
		"PropertyCard@Range_2@1",
		"PropertyCard@Aim_2@1",
		"PropertyCard@Power_3@1",
		"PropertyCard@Accuracy_3@1",
		"PropertyCard@Clip_3@1",
		"PropertyCard@Move_3@1",
		"PropertyCard@Energy_3@1",
		"PropertyCard@Range_3@1",
		"PropertyCard@Aim_3@1",
		"WeaponChip@FreddyGun_Chip_1@1",
		"WeaponChip@FreddyGun_Chip_2@1",
		"WeaponChip@FreddyGun_Chip_3@1",
		"WeaponChip@FreddyGun_Chip_4@1",
		"WeaponChip@ImpulseGun_Chip_1@1",
		"WeaponChip@ImpulseGun_Chip_2@1",
		"WeaponChip@ImpulseGun_Chip_3@1",
		"WeaponChip@ImpulseGun_Chip_4@1",
		"WeaponChip@Assault_Chip_1@1",
		"WeaponChip@Assault_Chip_2@1",
		"WeaponChip@Assault_Chip_3@1",
		"WeaponChip@Assault_Chip_4@1",
		"Potion@SpeedPlusBuff@2",
		"Potion@HpPlusBuff@2",
		"Potion@ExpX2Buff@2",
		"Potion@DamagePlusBuff@2",
		"Potion@HpRecoveryBuff@2"
	};

	// Token: 0x04000B3D RID: 2877
	private readonly string[] itemList_A = new string[]
	{
		"Base@GiftBox@1",
		"Base@HuntingTicket@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_2@1",
		"PropertyCard@Accuracy_2@1",
		"PropertyCard@Clip_2@1",
		"PropertyCard@Move_2@1",
		"PropertyCard@Energy_2@1",
		"PropertyCard@Range_2@1",
		"PropertyCard@Aim_2@1",
		"PropertyCard@Power_2@1",
		"PropertyCard@Accuracy_2@1",
		"PropertyCard@Clip_2@1",
		"PropertyCard@Move_2@1",
		"PropertyCard@Energy_2@1",
		"PropertyCard@Range_2@1",
		"PropertyCard@Aim_2@1",
		"PropertyCard@Power_3@1",
		"PropertyCard@Accuracy_3@1",
		"PropertyCard@Clip_3@1",
		"PropertyCard@Move_3@1",
		"PropertyCard@Energy_3@1",
		"PropertyCard@Range_3@1",
		"PropertyCard@Aim_3@1",
		"WeaponChip@FreddyGun_Chip_1@1",
		"WeaponChip@FreddyGun_Chip_2@1",
		"WeaponChip@FreddyGun_Chip_3@1",
		"WeaponChip@ImpulseGun_Chip_1@1",
		"WeaponChip@ImpulseGun_Chip_2@1",
		"WeaponChip@ImpulseGun_Chip_3@1",
		"WeaponChip@Assault_Chip_1@1",
		"WeaponChip@Assault_Chip_2@1",
		"WeaponChip@Assault_Chip_3@1",
		"Potion@SpeedPlusBuff@1",
		"Potion@HpPlusBuff@1",
		"Potion@ExpX2Buff@1",
		"Potion@DamagePlusBuff@1",
		"Potion@HpRecoveryBuff@1",
		"Potion@SpeedPlusBuff@1",
		"Potion@HpPlusBuff@1",
		"Potion@ExpX2Buff@1",
		"Potion@DamagePlusBuff@1",
		"Potion@HpRecoveryBuff@1"
	};

	// Token: 0x04000B3E RID: 2878
	private readonly string[] itemList_B = new string[]
	{
		"Base@GiftBox@1",
		"Base@HuntingTicket@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_2@1",
		"PropertyCard@Accuracy_2@1",
		"PropertyCard@Clip_2@1",
		"PropertyCard@Move_2@1",
		"PropertyCard@Energy_2@1",
		"PropertyCard@Range_2@1",
		"PropertyCard@Aim_2@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"WeaponChip@FreddyGun_Chip_1@1",
		"WeaponChip@FreddyGun_Chip_2@1",
		"WeaponChip@FreddyGun_Chip_3@1",
		"WeaponChip@ImpulseGun_Chip_1@1",
		"WeaponChip@ImpulseGun_Chip_2@1",
		"WeaponChip@ImpulseGun_Chip_3@1",
		"WeaponChip@Assault_Chip_1@1",
		"WeaponChip@Assault_Chip_2@1",
		"WeaponChip@Assault_Chip_3@1",
		"Potion@SpeedPlusBuff@1",
		"Potion@HpPlusBuff@1",
		"Potion@ExpX2Buff@1",
		"Potion@DamagePlusBuff@1",
		"Potion@HpRecoveryBuff@1",
		"Potion@SpeedPlusBuff@1",
		"Potion@HpPlusBuff@1",
		"Potion@ExpX2Buff@1",
		"Potion@DamagePlusBuff@1",
		"Potion@HpRecoveryBuff@1",
		"Potion@SpeedPlusBuff@1",
		"Potion@HpPlusBuff@1",
		"Potion@ExpX2Buff@1",
		"Potion@DamagePlusBuff@1",
		"Potion@HpRecoveryBuff@1"
	};

	// Token: 0x04000B3F RID: 2879
	private readonly string[] itemList_Lottery_S = new string[]
	{
		"Base@GiftBox@1",
		"Base@HuntingTicket@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_2@1",
		"PropertyCard@Accuracy_2@1",
		"PropertyCard@Clip_2@1",
		"PropertyCard@Move_2@1",
		"PropertyCard@Energy_2@1",
		"PropertyCard@Range_2@1",
		"PropertyCard@Aim_2@1",
		"PropertyCard@Power_3@1",
		"PropertyCard@Accuracy_3@1",
		"PropertyCard@Clip_3@1",
		"PropertyCard@Move_3@1",
		"PropertyCard@Energy_3@1",
		"PropertyCard@Range_3@1",
		"PropertyCard@Aim_3@1",
		"WeaponChip@FreddyGun_Chip_1@1",
		"WeaponChip@FreddyGun_Chip_2@1",
		"WeaponChip@FreddyGun_Chip_3@1",
		"WeaponChip@FreddyGun_Chip_4@1",
		"WeaponChip@ImpulseGun_Chip_1@1",
		"WeaponChip@ImpulseGun_Chip_2@1",
		"WeaponChip@ImpulseGun_Chip_3@1",
		"WeaponChip@ImpulseGun_Chip_4@1",
		"WeaponChip@Assault_Chip_1@1",
		"WeaponChip@Assault_Chip_2@1",
		"WeaponChip@Assault_Chip_3@1",
		"WeaponChip@Assault_Chip_4@1",
		"Potion@SpeedPlusBuff@2",
		"Potion@HpPlusBuff@2",
		"Potion@ExpX2Buff@2",
		"Potion@DamagePlusBuff@2",
		"Potion@HpRecoveryBuff@2",
		"Potion@SpeedPlusBuff@3",
		"Potion@HpPlusBuff@3",
		"Potion@ExpX2Buff@3",
		"Potion@DamagePlusBuff@3",
		"Potion@HpRecoveryBuff@3"
	};

	// Token: 0x04000B40 RID: 2880
	private readonly string[] itemList_Lottery_A = new string[]
	{
		"Base@GiftBox@1",
		"Base@HuntingTicket@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_2@1",
		"PropertyCard@Accuracy_2@1",
		"PropertyCard@Clip_2@1",
		"PropertyCard@Move_2@1",
		"PropertyCard@Energy_2@1",
		"PropertyCard@Range_2@1",
		"PropertyCard@Aim_2@1",
		"PropertyCard@Power_2@1",
		"PropertyCard@Accuracy_2@1",
		"PropertyCard@Clip_2@1",
		"PropertyCard@Move_2@1",
		"PropertyCard@Energy_2@1",
		"PropertyCard@Range_2@1",
		"PropertyCard@Aim_2@1",
		"PropertyCard@Power_3@1",
		"PropertyCard@Accuracy_3@1",
		"PropertyCard@Clip_3@1",
		"PropertyCard@Move_3@1",
		"PropertyCard@Energy_3@1",
		"PropertyCard@Range_3@1",
		"PropertyCard@Aim_3@1",
		"WeaponChip@FreddyGun_Chip_1@1",
		"WeaponChip@FreddyGun_Chip_2@1",
		"WeaponChip@FreddyGun_Chip_3@1",
		"WeaponChip@FreddyGun_Chip_4@1",
		"WeaponChip@ImpulseGun_Chip_1@1",
		"WeaponChip@ImpulseGun_Chip_2@1",
		"WeaponChip@ImpulseGun_Chip_3@1",
		"WeaponChip@ImpulseGun_Chip_4@1",
		"WeaponChip@Assault_Chip_1@1",
		"WeaponChip@Assault_Chip_2@1",
		"WeaponChip@Assault_Chip_3@1",
		"WeaponChip@Assault_Chip_4@1",
		"Potion@SpeedPlusBuff@2",
		"Potion@HpPlusBuff@2",
		"Potion@ExpX2Buff@2",
		"Potion@DamagePlusBuff@2",
		"Potion@HpRecoveryBuff@2",
		"Potion@SpeedPlusBuff@3",
		"Potion@HpPlusBuff@3",
		"Potion@ExpX2Buff@3",
		"Potion@DamagePlusBuff@3",
		"Potion@HpRecoveryBuff@3"
	};

	// Token: 0x04000B41 RID: 2881
	private readonly string[] itemList_Lottery_B = new string[]
	{
		"Base@GiftBox@1",
		"Base@HuntingTicket@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_1@1",
		"PropertyCard@Accuracy_1@1",
		"PropertyCard@Clip_1@1",
		"PropertyCard@Move_1@1",
		"PropertyCard@Energy_1@1",
		"PropertyCard@Range_1@1",
		"PropertyCard@Aim_1@1",
		"PropertyCard@Power_2@1",
		"PropertyCard@Accuracy_2@1",
		"PropertyCard@Clip_2@1",
		"PropertyCard@Move_2@1",
		"PropertyCard@Energy_2@1",
		"PropertyCard@Range_2@1",
		"PropertyCard@Aim_2@1",
		"PropertyCard@Power_2@1",
		"PropertyCard@Accuracy_2@1",
		"PropertyCard@Clip_2@1",
		"PropertyCard@Move_2@1",
		"PropertyCard@Energy_2@1",
		"PropertyCard@Range_2@1",
		"PropertyCard@Aim_2@1",
		"PropertyCard@Power_3@1",
		"PropertyCard@Accuracy_3@1",
		"PropertyCard@Clip_3@1",
		"PropertyCard@Move_3@1",
		"PropertyCard@Energy_3@1",
		"PropertyCard@Range_3@1",
		"PropertyCard@Aim_3@1",
		"WeaponChip@FreddyGun_Chip_1@1",
		"WeaponChip@FreddyGun_Chip_2@1",
		"WeaponChip@FreddyGun_Chip_3@1",
		"WeaponChip@FreddyGun_Chip_4@1",
		"WeaponChip@ImpulseGun_Chip_1@1",
		"WeaponChip@ImpulseGun_Chip_2@1",
		"WeaponChip@ImpulseGun_Chip_3@1",
		"WeaponChip@ImpulseGun_Chip_4@1",
		"WeaponChip@Assault_Chip_1@1",
		"WeaponChip@Assault_Chip_2@1",
		"WeaponChip@Assault_Chip_3@1",
		"WeaponChip@Assault_Chip_4@1",
		"Potion@SpeedPlusBuff@1",
		"Potion@HpPlusBuff@1",
		"Potion@ExpX2Buff@1",
		"Potion@DamagePlusBuff@1",
		"Potion@HpRecoveryBuff@1",
		"Potion@SpeedPlusBuff@2",
		"Potion@HpPlusBuff@2",
		"Potion@ExpX2Buff@2",
		"Potion@DamagePlusBuff@2",
		"Potion@HpRecoveryBuff@2",
		"Potion@SpeedPlusBuff@2",
		"Potion@HpPlusBuff@2",
		"Potion@ExpX2Buff@2",
		"Potion@DamagePlusBuff@2",
		"Potion@HpRecoveryBuff@2"
	};

	// Token: 0x04000B42 RID: 2882
	private int coin;

	// Token: 0x04000B43 RID: 2883
	private int exp;

	// Token: 0x04000B44 RID: 2884
	private int gem;

	// Token: 0x04000B45 RID: 2885
	private int lotteryCount;

	// Token: 0x04000B46 RID: 2886
	public bool isLotteryRound;

	// Token: 0x04000B47 RID: 2887
	public List<HuntingRewardInfo.SingleReward> lotteryItems;

	// Token: 0x04000B48 RID: 2888
	public readonly int maxValidLotteryCount = 3;

	// Token: 0x04000B49 RID: 2889
	public readonly int maxFreeLotteryCount = 2;

	// Token: 0x04000B4A RID: 2890
	public readonly int maxPaidLotteryCount = 1;

	// Token: 0x04000B4B RID: 2891
	public readonly int paidLotteryPrice = 5;

	// Token: 0x04000B4C RID: 2892
	public readonly GItemPurchaseType paidLotteryPurchaseType = GItemPurchaseType.GemPurchase;

	// Token: 0x04000B4D RID: 2893
	private List<string> strItems;

	// Token: 0x04000B4E RID: 2894
	public List<HuntingRewardInfo.SingleReward> generalItems;

	// Token: 0x04000B4F RID: 2895
	private GrowthGameRatingTag rating;

	// Token: 0x04000B50 RID: 2896
	private int sceneId;

	// Token: 0x04000B51 RID: 2897
	private int difficulty;

	// Token: 0x04000B52 RID: 2898
	private int roundId;

	// Token: 0x04000B53 RID: 2899
	private int maxRoundNum = 4;

	// Token: 0x04000B54 RID: 2900
	public int starRating;

	// Token: 0x0200019C RID: 412
	public struct SingleReward
	{
		// Token: 0x06000AFC RID: 2812 RVA: 0x000518EE File Offset: 0x0004FCEE
		public SingleReward(string spriteName, int number)
		{
			this.spriteName = spriteName;
			this.number = number;
			this.configStr = string.Empty;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00051909 File Offset: 0x0004FD09
		public SingleReward(string spriteName, int number, string configStr)
		{
			this.spriteName = spriteName;
			this.number = number;
			this.configStr = configStr;
		}

		// Token: 0x04000B55 RID: 2901
		public string spriteName;

		// Token: 0x04000B56 RID: 2902
		public int number;

		// Token: 0x04000B57 RID: 2903
		public string configStr;
	}

	// Token: 0x0200019D RID: 413
	public struct RecommendItem
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x00051920 File Offset: 0x0004FD20
		public RecommendItem(string spriteName)
		{
			this.spriteName = spriteName;
		}

		// Token: 0x04000B58 RID: 2904
		public string spriteName;
	}
}
