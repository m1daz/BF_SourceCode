using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200017B RID: 379
public class GWeaponItemInfo
{
	// Token: 0x06000AAD RID: 2733 RVA: 0x0004D764 File Offset: 0x0004BB64
	public GWeaponItemInfo()
	{
		this.mNameDisplay = "Undefined Weapon";
		this.mPropertyList = new Dictionary<string, float>();
		this.mUpgradeEnabledPropertyList = new string[0];
		this.mPropertyCanUpgradeList = new Dictionary<string, bool>();
		this.mPropertyLvList = new Dictionary<string, int>();
		this.mPropertyNextLvList = new Dictionary<string, int>();
		if (GWeaponItemInfo.mPropertyUpgradeAdditionValueList == null)
		{
			GWeaponItemInfo.mPropertyUpgradeAdditionValueList = new Dictionary<string, float[]>();
			GWeaponItemInfo.mPropertyUpgradeCoinPriceList = new Dictionary<string, float[]>();
			GWeaponItemInfo.mPropertyUpgradeGemPriceList = new Dictionary<string, float[]>();
			GWeaponItemInfo.mPropertyUpgradeCoinSuccessRateList = new Dictionary<string, float[]>();
			GWeaponItemInfo.mPropertyUpgradeGemSuccessRateList = new Dictionary<string, float[]>();
			GWeaponItemInfo.mPropertyUpgradeAdditionValueList.Add("Power", new float[]
			{
				0.08f,
				0.16f,
				0.28f
			});
			GWeaponItemInfo.mPropertyUpgradeAdditionValueList.Add("Accuracy", new float[]
			{
				0.1f,
				0.2f,
				0.4f
			});
			GWeaponItemInfo.mPropertyUpgradeAdditionValueList.Add("Clip", new float[]
			{
				0.1f,
				0.2f,
				0.4f
			});
			GWeaponItemInfo.mPropertyUpgradeAdditionValueList.Add("Move", new float[]
			{
				0.025f,
				0.05f,
				0.1f
			});
			GWeaponItemInfo.mPropertyUpgradeAdditionValueList.Add("Energy", new float[]
			{
				0.2f,
				0.4f,
				0.8f
			});
			GWeaponItemInfo.mPropertyUpgradeAdditionValueList.Add("Range", new float[]
			{
				0.1f,
				0.2f,
				0.4f
			});
			GWeaponItemInfo.mPropertyUpgradeAdditionValueList.Add("Aim", new float[]
			{
				0.1f,
				0.2f,
				0.4f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinPriceList.Add("Power", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinPriceList.Add("Accuracy", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinPriceList.Add("Clip", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinPriceList.Add("Move", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinPriceList.Add("Energy", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinPriceList.Add("Range", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinPriceList.Add("Aim", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeGemPriceList.Add("Power", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeGemPriceList.Add("Accuracy", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeGemPriceList.Add("Clip", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeGemPriceList.Add("Move", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeGemPriceList.Add("Energy", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeGemPriceList.Add("Range", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeGemPriceList.Add("Aim", new float[]
			{
				0.1f,
				0.1f,
				0.2f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinSuccessRateList.Add("Power", new float[]
			{
				0.8f,
				0.5f,
				0.3f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinSuccessRateList.Add("Accuracy", new float[]
			{
				0.8f,
				0.5f,
				0.3f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinSuccessRateList.Add("Clip", new float[]
			{
				0.8f,
				0.5f,
				0.3f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinSuccessRateList.Add("Move", new float[]
			{
				0.8f,
				0.5f,
				0.3f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinSuccessRateList.Add("Energy", new float[]
			{
				0.8f,
				0.5f,
				0.3f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinSuccessRateList.Add("Range", new float[]
			{
				0.8f,
				0.5f,
				0.3f
			});
			GWeaponItemInfo.mPropertyUpgradeCoinSuccessRateList.Add("Aim", new float[]
			{
				0.8f,
				0.5f,
				0.3f
			});
			GWeaponItemInfo.mPropertyUpgradeGemSuccessRateList.Add("Power", new float[]
			{
				1f,
				1f,
				0.8f
			});
			GWeaponItemInfo.mPropertyUpgradeGemSuccessRateList.Add("Accuracy", new float[]
			{
				1f,
				1f,
				0.8f
			});
			GWeaponItemInfo.mPropertyUpgradeGemSuccessRateList.Add("Clip", new float[]
			{
				1f,
				1f,
				0.8f
			});
			GWeaponItemInfo.mPropertyUpgradeGemSuccessRateList.Add("Move", new float[]
			{
				1f,
				1f,
				0.8f
			});
			GWeaponItemInfo.mPropertyUpgradeGemSuccessRateList.Add("Energy", new float[]
			{
				1f,
				1f,
				0.8f
			});
			GWeaponItemInfo.mPropertyUpgradeGemSuccessRateList.Add("Range", new float[]
			{
				1f,
				1f,
				0.8f
			});
			GWeaponItemInfo.mPropertyUpgradeGemSuccessRateList.Add("Aim", new float[]
			{
				1f,
				1f,
				0.8f
			});
		}
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0004DCD5 File Offset: 0x0004C0D5
	public bool IsChipUnlocked(int chipIndex)
	{
		return UserDataController.IsWeaponChipUnlocked(this.mName, chipIndex, this.maxChips);
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x0004DCE9 File Offset: 0x0004C0E9
	public void UnlockChip(int chipIndex)
	{
		if (!this.mIsCollection)
		{
			return;
		}
		UserDataController.UnlockWeaponChip(this.mName, chipIndex, this.maxChips);
	}

	// Token: 0x06000AB0 RID: 2736 RVA: 0x0004DD0C File Offset: 0x0004C10C
	public void DeployChipsStates()
	{
		if (!this.mIsCollection)
		{
			return;
		}
		this.mChipsSpriteName = new string[this.maxChips];
		int num = 0;
		for (int i = 0; i < this.maxChips; i++)
		{
			bool flag = GrowthManagerKit.IsWeaponChipUnlocked(this.mName, i + 1, this.maxChips);
			if (flag)
			{
				this.mChipsSpriteName[i] = string.Format("{0}_Chip_{1}_SlotLogo", this.mName, i + 1);
				num++;
			}
			else
			{
				this.mChipsSpriteName[i] = string.Format("{0}_Chip_{1}_Locked_SlotLogo", this.mName, i + 1);
			}
		}
		if (num >= this.maxChips && !this.mIsNoLimitedUse)
		{
			this.AddWeaponTime(918000f, GWeaponRechargeType.WeaponTime);
		}
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0004DDD6 File Offset: 0x0004C1D6
	public int GetPropertyMaxLv(string propertyName)
	{
		return GWeaponItemInfo.mPropertyUpgradeAdditionValueList[propertyName].Length;
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0004DDE5 File Offset: 0x0004C1E5
	public int GetPropertyCurLv(string propertyName)
	{
		return (!this.mPropertyLvList.ContainsKey(propertyName)) ? 0 : this.mPropertyLvList[propertyName];
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0004DE0C File Offset: 0x0004C20C
	public float GetPropertyAdditionValue(string propertyName)
	{
		if (this.mPropertyLvList.ContainsKey(propertyName))
		{
			float[] array = new float[]
			{
				1f,
				1f,
				1f
			};
			if (propertyName == "Power")
			{
				if (this.mId.mId_2 == 3)
				{
					array = new float[]
					{
						0.7f,
						0.7f,
						0.7f
					};
				}
				else if (this.mId.mId_2 == 5)
				{
					if (this.mId.mId_3 == 6)
					{
						array = new float[]
						{
							0.35f,
							0.35f,
							0.35f
						};
					}
				}
				else if (this.mId.mId_2 == 4)
				{
					array = new float[]
					{
						0.5f,
						0.5f,
						0.5f
					};
				}
				else if (this.mId.mId_2 == 7)
				{
					array = new float[]
					{
						0.7f,
						0.7f,
						0.7f
					};
				}
			}
			return (this.mPropertyLvList[propertyName] != 0) ? (array[this.mPropertyLvList[propertyName] - 1] * GWeaponItemInfo.mPropertyUpgradeAdditionValueList[propertyName][this.mPropertyLvList[propertyName] - 1]) : 0f;
		}
		return 0f;
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0004DF44 File Offset: 0x0004C344
	public float GetPropertyNextAdditionValue(string propertyName)
	{
		if (this.mPropertyNextLvList.ContainsKey(propertyName))
		{
			float[] array = new float[]
			{
				1f,
				1f,
				1f
			};
			if (propertyName == "Power")
			{
				if (this.mId.mId_2 == 3)
				{
					array = new float[]
					{
						0.7f,
						0.7f,
						0.7f
					};
				}
				else if (this.mId.mId_2 == 5)
				{
					if (this.mId.mId_3 == 6)
					{
						array = new float[]
						{
							0.35f,
							0.35f,
							0.35f
						};
					}
				}
				else if (this.mId.mId_2 == 4)
				{
					array = new float[]
					{
						0.5f,
						0.5f,
						0.5f
					};
				}
				else if (this.mId.mId_2 == 7)
				{
					array = new float[]
					{
						0.7f,
						0.7f,
						0.7f
					};
				}
			}
			return (this.mPropertyNextLvList[propertyName] != 0) ? (array[this.mPropertyNextLvList[propertyName] - 1] * GWeaponItemInfo.mPropertyUpgradeAdditionValueList[propertyName][this.mPropertyNextLvList[propertyName] - 1]) : 0f;
		}
		return 0f;
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x0004E07C File Offset: 0x0004C47C
	public GWeaponItemInfo.UpgradeConditionsSet[] GetUpgradeConditions(string propertyName)
	{
		GWeaponItemInfo.UpgradeConditionsSet[] array = new GWeaponItemInfo.UpgradeConditionsSet[2];
		int num = this.mPropertyLvList[propertyName];
		int num2 = 1;
		bool flag = UserDataController.GetWeaponPropertyCardNum(propertyName, num + 1) >= num2;
		GWeaponItemInfo.UpgradeCondition upgradeCondition = new GWeaponItemInfo.UpgradeCondition(propertyName + "_CardLogo_" + (num + 1).ToString(), num2, (!flag) ? "UpgradeReady_No" : "UpgradeReady_Yes");
		array[0].isConditionsReady = flag;
		num = this.mPropertyLvList[propertyName];
		if (!this.mIsHonorItem)
		{
			num2 = (int)((float)(this.mNoLimitedUsePrice * 60) * GWeaponItemInfo.mPropertyUpgradeCoinPriceList[propertyName][num]);
		}
		else
		{
			num2 = (int)(12000f * GWeaponItemInfo.mPropertyUpgradeCoinPriceList[propertyName][num]);
		}
		flag = (UserDataController.GetCoins() >= num2);
		GWeaponItemInfo.UpgradeCondition upgradeCondition2 = new GWeaponItemInfo.UpgradeCondition("Coin", num2, (!flag) ? "UpgradeReady_No" : "UpgradeReady_Yes");
		array[0].isConditionsReady = (array[0].isConditionsReady && flag);
		array[0].successRate = GWeaponItemInfo.mPropertyUpgradeCoinSuccessRateList[propertyName][num];
		array[0].conditions = new GWeaponItemInfo.UpgradeCondition[2];
		array[0].conditions[0] = upgradeCondition;
		array[0].conditions[1] = upgradeCondition2;
		num = this.mPropertyLvList[propertyName];
		num2 = 1;
		flag = (UserDataController.GetWeaponPropertyCardNum(propertyName, num + 1) >= num2);
		GWeaponItemInfo.UpgradeCondition upgradeCondition3 = new GWeaponItemInfo.UpgradeCondition(propertyName + "_CardLogo_" + (num + 1).ToString(), num2, (!flag) ? "UpgradeReady_No" : "UpgradeReady_Yes");
		array[1].isConditionsReady = flag;
		num = this.mPropertyLvList[propertyName];
		if (!this.mIsHonorItem)
		{
			num2 = (int)((float)this.mNoLimitedUsePrice * GWeaponItemInfo.mPropertyUpgradeGemPriceList[propertyName][num]);
		}
		else
		{
			num2 = (int)(200f * GWeaponItemInfo.mPropertyUpgradeGemPriceList[propertyName][num]);
		}
		flag = (UserDataController.GetGems() >= num2);
		GWeaponItemInfo.UpgradeCondition upgradeCondition4 = new GWeaponItemInfo.UpgradeCondition("Gem", num2, (!flag) ? "UpgradeReady_No" : "UpgradeReady_Yes");
		array[1].isConditionsReady = (array[1].isConditionsReady && flag);
		array[1].successRate = GWeaponItemInfo.mPropertyUpgradeGemSuccessRateList[propertyName][num];
		array[1].conditions = new GWeaponItemInfo.UpgradeCondition[2];
		array[1].conditions[0] = upgradeCondition3;
		array[1].conditions[1] = upgradeCondition4;
		return array;
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x0004E34C File Offset: 0x0004C74C
	public bool UpgradeProperty(string propertyName, GItemPurchaseType purchaseType)
	{
		if (!this.mPropertyCanUpgradeList[propertyName])
		{
			return false;
		}
		float num = 0f;
		int num2 = this.mPropertyLvList[propertyName];
		if (num2 >= 3)
		{
			return false;
		}
		if (purchaseType == GItemPurchaseType.CoinsPurchase)
		{
			num = GWeaponItemInfo.mPropertyUpgradeCoinSuccessRateList[propertyName][this.mPropertyLvList[propertyName]];
			int num3;
			if (!this.mIsHonorItem)
			{
				num3 = (int)((float)(this.mNoLimitedUsePrice * 60) * GWeaponItemInfo.mPropertyUpgradeCoinPriceList[propertyName][num2]);
			}
			else
			{
				num3 = (int)(12000f * GWeaponItemInfo.mPropertyUpgradeCoinPriceList[propertyName][num2]);
			}
			GrowthManagerKit.SubCoins(num3);
		}
		else if (purchaseType == GItemPurchaseType.GemPurchase)
		{
			num = GWeaponItemInfo.mPropertyUpgradeGemSuccessRateList[propertyName][this.mPropertyLvList[propertyName]];
			int num3;
			if (!this.mIsHonorItem)
			{
				num3 = (int)((float)this.mNoLimitedUsePrice * GWeaponItemInfo.mPropertyUpgradeGemPriceList[propertyName][num2]);
			}
			else
			{
				num3 = (int)(200f * GWeaponItemInfo.mPropertyUpgradeGemPriceList[propertyName][num2]);
			}
			GrowthManagerKit.SubGems(num3);
		}
		GrowthManagerKit.SubWeaponPropertyCardNum(propertyName, num2 + 1, 1);
		bool flag = UnityEngine.Random.Range(0f, 1f) <= num;
		if (flag)
		{
			UserDataController.SetWeaponPropertyLv(this.mName, propertyName, this.mPropertyNextLvList[propertyName]);
			this.RefreshPropertyUpgradeConfig();
		}
		return flag;
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x0004E49C File Offset: 0x0004C89C
	private void RefreshPropertyUpgradeConfig()
	{
		if (this.mUpgradeEnabledPropertyList == null)
		{
			return;
		}
		this.mCurPropertySumLv = 0;
		this.mModelLv = 0;
		for (int i = 0; i < this.mUpgradeEnabledPropertyList.Length; i++)
		{
			string text = this.mUpgradeEnabledPropertyList[i];
			this.mPropertyLvList[text] = UserDataController.GetWeaponPropertyLv(this.mName, text);
			this.mPropertyNextLvList[text] = Math.Min(GWeaponItemInfo.mPropertyUpgradeAdditionValueList[text].Length, this.mPropertyLvList[text] + 1);
			this.mPropertyCanUpgradeList[text] = (this.mPropertyNextLvList[text] > this.mPropertyLvList[text]);
			this.mCurPropertySumLv += this.mPropertyLvList[text];
			if (i == 0)
			{
				this.mModelLv = this.mPropertyLvList[text];
			}
			else
			{
				this.mModelLv = Math.Min(this.mPropertyLvList[text], this.mModelLv);
			}
		}
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0004E5A4 File Offset: 0x0004C9A4
	public void DeployPropertyUpgradeConfig(string[] propertyNameList)
	{
		if (propertyNameList == null)
		{
			return;
		}
		this.mUpgradeEnabledPropertyList = propertyNameList;
		this.mCurPropertySumLv = 0;
		this.mConstPropertySumLv = 0;
		this.mModelLv = 0;
		for (int i = 0; i < propertyNameList.Length; i++)
		{
			string text = propertyNameList[i];
			this.mPropertyLvList.Add(text, UserDataController.GetWeaponPropertyLv(this.mName, text));
			this.mPropertyNextLvList.Add(text, Math.Min(GWeaponItemInfo.mPropertyUpgradeAdditionValueList[text].Length, this.mPropertyLvList[text] + 1));
			this.mPropertyCanUpgradeList.Add(text, this.mPropertyNextLvList[text] > this.mPropertyLvList[text]);
			this.mCurPropertySumLv += this.mPropertyLvList[text];
			this.mConstPropertySumLv += GWeaponItemInfo.mPropertyUpgradeAdditionValueList[text].Length;
			if (i == 0)
			{
				this.mModelLv = this.mPropertyLvList[text];
			}
			else
			{
				this.mModelLv = Math.Min(this.mPropertyLvList[text], this.mModelLv);
			}
		}
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0004E6C3 File Offset: 0x0004CAC3
	private float GetCurAdditionValue(string propertyName)
	{
		return this.GetPropertyAdditionValue(propertyName);
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0004E6CC File Offset: 0x0004CACC
	private float GetNextAdditionValue(string propertyName)
	{
		return this.GetPropertyNextAdditionValue(propertyName);
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0004E6D8 File Offset: 0x0004CAD8
	public void AddNDeployCurProperty(string propertyName, float displayValue)
	{
		if (this.mPropertyLvList.ContainsKey(propertyName))
		{
			if (propertyName == "Accuracy")
			{
				this.mPropertyList.Add(propertyName, displayValue + (10.1f - displayValue) * this.GetCurAdditionValue(propertyName));
			}
			else
			{
				this.mPropertyList.Add(propertyName, displayValue * (1f + this.GetCurAdditionValue(propertyName)));
			}
		}
		else
		{
			this.mPropertyList.Add(propertyName, displayValue);
		}
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x0004E758 File Offset: 0x0004CB58
	public void AddNDeployNextProperty(string propertyName, float displayValue)
	{
		if (this.mPropertyLvList.ContainsKey(propertyName))
		{
			if (propertyName == "Accuracy")
			{
				this.mPropertyList.Add(propertyName, displayValue + (10.1f - displayValue) * this.GetNextAdditionValue(propertyName));
			}
			else
			{
				this.mPropertyList.Add(propertyName, displayValue * (1f + this.GetNextAdditionValue(propertyName)));
			}
		}
		else
		{
			this.mPropertyList.Add(propertyName, displayValue);
		}
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x0004E7D8 File Offset: 0x0004CBD8
	public int GetTimeFillPrice(int hour, GWeaponRechargeType rType)
	{
		int num = 1;
		int result;
		if (rType == GWeaponRechargeType.WeaponTime)
		{
			if (this.GetTimeFillPurchaseType(hour, rType) == GItemPurchaseType.CoinsPurchase)
			{
				result = hour * num * this.mSinglePriceOfTimeFill / 10 * 10;
			}
			else if (this.GetTimeFillPurchaseType(hour, rType) == GItemPurchaseType.GemPurchase)
			{
				result = this.mNoLimitedUsePrice * this.mOffRate / 100 / 5 * 5;
			}
			else if (this.GetTimeFillPurchaseType(hour, rType) == GItemPurchaseType.HonorPointPurchase)
			{
				result = this.mNoLimitedUsePrice * this.mOffRate / 100 / 5 * 5;
			}
			else
			{
				result = 2555555;
			}
		}
		else if (rType == GWeaponRechargeType.WeaponPlusTime)
		{
			if (this.GetTimeFillPurchaseType(hour, rType) == GItemPurchaseType.CoinsPurchase)
			{
				result = hour * num * this.mSinglePriceOfPlusTimeFill / 10 * 10;
			}
			else if (this.GetTimeFillPurchaseType(hour, rType) == GItemPurchaseType.GemPurchase)
			{
				result = hour * num * this.mSinglePriceOfPlusTimeFill;
			}
			else
			{
				result = 2555555;
			}
		}
		else
		{
			result = 2555555;
		}
		return result;
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x0004E8C7 File Offset: 0x0004CCC7
	public GItemPurchaseType GetTimeFillPurchaseType(int hour, GWeaponRechargeType rType)
	{
		if (rType == GWeaponRechargeType.WeaponTime)
		{
			if (this.mIsHonorItem)
			{
				return GItemPurchaseType.HonorPointPurchase;
			}
			if (hour == 255)
			{
				return GItemPurchaseType.GemPurchase;
			}
			return GItemPurchaseType.CoinsPurchase;
		}
		else
		{
			if (rType == GWeaponRechargeType.WeaponPlusTime)
			{
				return GItemPurchaseType.GemPurchase;
			}
			return GItemPurchaseType.CoinsPurchase;
		}
	}

	// Token: 0x06000ABF RID: 2751 RVA: 0x0004E8F8 File Offset: 0x0004CCF8
	public void AddWeaponTime(float secondAdd, GWeaponRechargeType rType)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllWeaponNameList.Length; i++)
		{
			if (UserDataController.AllWeaponNameList[i] == this.mName)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			if (rType == GWeaponRechargeType.WeaponTime)
			{
				this.mWeaponTimeRest = UserDataController.GetWeaponTimeRest(this.mName) + secondAdd;
				UserDataController.SetWeaponTimeRest(this.mName, this.mWeaponTimeRest);
				if (this.mWeaponTimeRest > 0f)
				{
					this.mIsEnabled = true;
					if (this.mWeaponTimeRest > 720000f)
					{
						this.mIsNoLimitedUse = true;
					}
				}
			}
			else if (rType == GWeaponRechargeType.WeaponPlusTime)
			{
				this.mWeaponPlusTimeRest = UserDataController.GetWeaponPlusTimeRest(this.mName) + secondAdd;
				UserDataController.SetWeaponPlusTimeRest(this.mName, this.mWeaponPlusTimeRest);
				if (this.mWeaponPlusTimeRest > 0f)
				{
					this.mIsPlused = true;
				}
			}
		}
	}

	// Token: 0x06000AC0 RID: 2752 RVA: 0x0004E9E8 File Offset: 0x0004CDE8
	public void WeaponTimeDecreaseUpdate(float deltaTime, GWeaponRechargeType rType)
	{
		if (rType == GWeaponRechargeType.WeaponTime)
		{
			float num = UserDataController.GetWeaponTimeRest(this.mName);
			if (num <= 0f)
			{
				this.mWeaponTimeRest = 0f;
				this.mIsEnabled = false;
			}
			else
			{
				this.mWeaponTimeRest = Mathf.Max(num - deltaTime, 0f);
				UserDataController.SetWeaponTimeRest(this.mName, this.mWeaponTimeRest);
				this.mIsEnabled = true;
			}
		}
		else if (rType == GWeaponRechargeType.WeaponPlusTime)
		{
			float num = UserDataController.GetWeaponPlusTimeRest(this.mName);
			if (num <= 0f)
			{
				this.mWeaponPlusTimeRest = 0f;
			}
			else
			{
				this.mWeaponPlusTimeRest = Mathf.Max(num - deltaTime, 0f);
				UserDataController.SetWeaponPlusTimeRest(this.mName, this.mWeaponPlusTimeRest);
			}
		}
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x0004EAB2 File Offset: 0x0004CEB2
	public float GetWeaponTimeRest(GWeaponRechargeType rType)
	{
		if (rType == GWeaponRechargeType.WeaponTime)
		{
			return UserDataController.GetWeaponTimeRest(this.mName);
		}
		if (rType == GWeaponRechargeType.WeaponPlusTime)
		{
			return UserDataController.GetWeaponPlusTimeRest(this.mName);
		}
		return -255f;
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x0004EAE0 File Offset: 0x0004CEE0
	public float GetCachedWeaponTimeRest(GWeaponRechargeType rType)
	{
		if (!this.mHasLoadedUserDataToCache)
		{
			this.mWeaponTimeRest = UserDataController.GetWeaponTimeRest(this.mName);
			this.mWeaponPlusTimeRest = UserDataController.GetWeaponPlusTimeRest(this.mName);
			this.mHasLoadedUserDataToCache = true;
		}
		if (rType == GWeaponRechargeType.WeaponTime)
		{
			return this.mWeaponTimeRest;
		}
		if (rType == GWeaponRechargeType.WeaponPlusTime)
		{
			return this.mWeaponPlusTimeRest;
		}
		return -255f;
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x0004EB44 File Offset: 0x0004CF44
	public void CachedWeaponTimeDecreaseUpdate(float deltaTime, GWeaponRechargeType rType)
	{
		float cachedWeaponTimeRest = this.GetCachedWeaponTimeRest(rType);
		if (rType == GWeaponRechargeType.WeaponTime)
		{
			if (cachedWeaponTimeRest <= 0f)
			{
				this.mIsEnabled = false;
			}
			else
			{
				this.mNeedWriteDataToDisk = true;
				this.mWeaponTimeRest = Mathf.Max(cachedWeaponTimeRest - deltaTime, 0f);
				this.mIsEnabled = true;
			}
		}
		else if (rType == GWeaponRechargeType.WeaponPlusTime && cachedWeaponTimeRest > 0f)
		{
			this.mNeedWriteDataToDisk = true;
			this.mWeaponPlusTimeRest = Mathf.Max(cachedWeaponTimeRest - deltaTime, 0f);
		}
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x0004EBC9 File Offset: 0x0004CFC9
	public void WriteDataFromCacheToDisk()
	{
		if (this.mNeedWriteDataToDisk)
		{
			UserDataController.SetWeaponTimeRest(this.mName, this.mWeaponTimeRest);
			UserDataController.SetWeaponPlusTimeRest(this.mName, this.mWeaponPlusTimeRest);
			this.mNeedWriteDataToDisk = false;
			this.mHasLoadedUserDataToCache = false;
		}
	}

	// Token: 0x040009E2 RID: 2530
	public static Dictionary<string, float[]> mPropertyUpgradeAdditionValueList;

	// Token: 0x040009E3 RID: 2531
	public static Dictionary<string, float[]> mPropertyUpgradeCoinPriceList;

	// Token: 0x040009E4 RID: 2532
	public static Dictionary<string, float[]> mPropertyUpgradeGemPriceList;

	// Token: 0x040009E5 RID: 2533
	public static Dictionary<string, float[]> mPropertyUpgradeCoinSuccessRateList;

	// Token: 0x040009E6 RID: 2534
	public static Dictionary<string, float[]> mPropertyUpgradeGemSuccessRateList;

	// Token: 0x040009E7 RID: 2535
	public string mName;

	// Token: 0x040009E8 RID: 2536
	public GItemId mId;

	// Token: 0x040009E9 RID: 2537
	public GItemPurchaseType mPurchasedType;

	// Token: 0x040009EA RID: 2538
	public int mUnlockCLevel;

	// Token: 0x040009EB RID: 2539
	public int mCurWeaponLevel;

	// Token: 0x040009EC RID: 2540
	public int mMaxWeaponLevel;

	// Token: 0x040009ED RID: 2541
	public int mWeaponId;

	// Token: 0x040009EE RID: 2542
	public float mMoveSpeed;

	// Token: 0x040009EF RID: 2543
	public float mMinDamage;

	// Token: 0x040009F0 RID: 2544
	public float mMaxDamage;

	// Token: 0x040009F1 RID: 2545
	public int mPrice;

	// Token: 0x040009F2 RID: 2546
	public string mNameDisplay;

	// Token: 0x040009F3 RID: 2547
	public string mLogoSpriteName;

	// Token: 0x040009F4 RID: 2548
	public bool mCanUpgrade;

	// Token: 0x040009F5 RID: 2549
	public bool mIsEnabled;

	// Token: 0x040009F6 RID: 2550
	public bool mIsPlused;

	// Token: 0x040009F7 RID: 2551
	public bool mIsEquipped;

	// Token: 0x040009F8 RID: 2552
	public bool mIsNoLimitedUse;

	// Token: 0x040009F9 RID: 2553
	public int mNoLimitedUsePrice = 1000;

	// Token: 0x040009FA RID: 2554
	public int mClipPrice = 5;

	// Token: 0x040009FB RID: 2555
	public string mGunType;

	// Token: 0x040009FC RID: 2556
	public Dictionary<string, float> mPropertyList;

	// Token: 0x040009FD RID: 2557
	private string[] mUpgradeEnabledPropertyList;

	// Token: 0x040009FE RID: 2558
	public Dictionary<string, bool> mPropertyCanUpgradeList;

	// Token: 0x040009FF RID: 2559
	public Dictionary<string, int> mPropertyLvList;

	// Token: 0x04000A00 RID: 2560
	public Dictionary<string, int> mPropertyNextLvList;

	// Token: 0x04000A01 RID: 2561
	public string mFeatureDescription;

	// Token: 0x04000A02 RID: 2562
	public bool mCanBeAutoUnlocked;

	// Token: 0x04000A03 RID: 2563
	public int mAutoUnlockLevel = 1;

	// Token: 0x04000A04 RID: 2564
	public int mOffRate = 100;

	// Token: 0x04000A05 RID: 2565
	public string mOffRateDescription = string.Empty;

	// Token: 0x04000A06 RID: 2566
	public static int mWeaponTimeFillLevelNum = 6;

	// Token: 0x04000A07 RID: 2567
	public int[] mWeaponTimeFillLevel = new int[]
	{
		255,
		3,
		6,
		12,
		18,
		24
	};

	// Token: 0x04000A08 RID: 2568
	public int mSinglePriceOfTimeFill;

	// Token: 0x04000A09 RID: 2569
	public float mWeaponTimeRest;

	// Token: 0x04000A0A RID: 2570
	public bool mIsOnlyUnlimitedBuy;

	// Token: 0x04000A0B RID: 2571
	public bool mIsHonorItem;

	// Token: 0x04000A0C RID: 2572
	public bool mSellable = true;

	// Token: 0x04000A0D RID: 2573
	public string mPurchaseTipsText = string.Empty;

	// Token: 0x04000A0E RID: 2574
	public bool mIsCollection;

	// Token: 0x04000A0F RID: 2575
	public string[] mChipsSpriteName = new string[4];

	// Token: 0x04000A10 RID: 2576
	private readonly int maxChips = 4;

	// Token: 0x04000A11 RID: 2577
	public int[] mWeaponPlusTimeFillLevel = new int[]
	{
		3,
		6,
		12,
		18,
		24
	};

	// Token: 0x04000A12 RID: 2578
	public int mSinglePriceOfPlusTimeFill;

	// Token: 0x04000A13 RID: 2579
	public float mWeaponPlusTimeRest;

	// Token: 0x04000A14 RID: 2580
	public int mCurPropertySumLv;

	// Token: 0x04000A15 RID: 2581
	public int mConstPropertySumLv;

	// Token: 0x04000A16 RID: 2582
	public int mModelLv;

	// Token: 0x04000A17 RID: 2583
	private bool mHasLoadedUserDataToCache;

	// Token: 0x04000A18 RID: 2584
	private bool mNeedWriteDataToDisk;

	// Token: 0x0200017C RID: 380
	public struct UpgradeCondition
	{
		// Token: 0x06000AC6 RID: 2758 RVA: 0x0004EC0E File Offset: 0x0004D00E
		public UpgradeCondition(string typeSpriteName, int costNum, string isPreparedSpriteName)
		{
			this.typeSpriteName = typeSpriteName;
			this.costNum = costNum;
			this.isPreparedSpriteName = isPreparedSpriteName;
		}

		// Token: 0x04000A19 RID: 2585
		public string typeSpriteName;

		// Token: 0x04000A1A RID: 2586
		public int costNum;

		// Token: 0x04000A1B RID: 2587
		public string isPreparedSpriteName;
	}

	// Token: 0x0200017D RID: 381
	public struct UpgradeConditionsSet
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x0004EC25 File Offset: 0x0004D025
		public UpgradeConditionsSet(GWeaponItemInfo.UpgradeCondition[] conditions)
		{
			this.conditions = conditions;
			this.isConditionsReady = false;
			this.successRate = 0f;
		}

		// Token: 0x04000A1C RID: 2588
		public GWeaponItemInfo.UpgradeCondition[] conditions;

		// Token: 0x04000A1D RID: 2589
		public bool isConditionsReady;

		// Token: 0x04000A1E RID: 2590
		public float successRate;
	}
}
