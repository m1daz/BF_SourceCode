using System;
using UnityEngine;

// Token: 0x02000182 RID: 386
public class GArmorItemInfo
{
	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0004EDED File Offset: 0x0004D1ED
	// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0004EDF5 File Offset: 0x0004D1F5
	public int mDurabilityInGame
	{
		get
		{
			return this._mDurabilityInGame;
		}
		set
		{
			GameProtecter.mInstance.SetEncryptVariable(ref this._mDurabilityInGame, ref this._mEncryptDurabilityInGame, value);
		}
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0004EE10 File Offset: 0x0004D210
	public int GetTimeFillPrice(int hour)
	{
		int num = 1;
		int num2;
		switch (hour)
		{
		case 3:
			num2 = 100;
			break;
		default:
			if (hour != 12)
			{
				if (hour != 18)
				{
					if (hour != 24)
					{
						num2 = 100;
					}
					else
					{
						num2 = 100;
					}
				}
				else
				{
					num2 = 100;
				}
			}
			else
			{
				num2 = 100;
			}
			break;
		case 6:
			num2 = 100;
			break;
		}
		return hour * num * this.mSinglePrice * num2 / 100;
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0004EE98 File Offset: 0x0004D298
	public string GetTimeFillOffSaleRateStr(int hour)
	{
		int num;
		switch (hour)
		{
		case 3:
			num = 100;
			break;
		default:
			if (hour != 12)
			{
				if (hour != 18)
				{
					if (hour != 24)
					{
						num = 100;
					}
					else
					{
						num = 100;
					}
				}
				else
				{
					num = 100;
				}
			}
			else
			{
				num = 100;
			}
			break;
		case 6:
			num = 100;
			break;
		}
		if (num == 100)
		{
			return string.Empty;
		}
		return "<" + (100 - num).ToString() + "% OFF>";
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x0004EF3C File Offset: 0x0004D33C
	public void AddAutoSupplyTime(float secondAdd)
	{
		bool flag = true;
		for (int i = 0; i < UserDataController.AllArmorNameList.Length; i++)
		{
			if (UserDataController.AllArmorNameList[i] == this.mName)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			this.mAutoSupplyTimeRest = UserDataController.GetArmorAutoSupplyTime(this.mName) + secondAdd;
			UserDataController.SetArmorAutoSupplyTime(this.mName, this.mAutoSupplyTimeRest);
		}
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x0004EFB0 File Offset: 0x0004D3B0
	public void AutoSupplyTimeDecreaseUpdate(float deltaTime)
	{
		float armorAutoSupplyTime = UserDataController.GetArmorAutoSupplyTime(this.mName);
		if (armorAutoSupplyTime <= 0f)
		{
			this.mAutoSupplyTimeRest = 0f;
			this.mIsInAutoSupplyStatus = false;
		}
		else
		{
			this.mAutoSupplyTimeRest = Mathf.Max(armorAutoSupplyTime - deltaTime, 0f);
			UserDataController.SetArmorAutoSupplyTime(this.mName, this.mAutoSupplyTimeRest);
			this.mIsInAutoSupplyStatus = true;
		}
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x0004F016 File Offset: 0x0004D416
	public float GetAutoSupplyTimeRest()
	{
		return UserDataController.GetArmorAutoSupplyTime(this.mName);
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x0004F023 File Offset: 0x0004D423
	public float GetCachedAutoSupplyTimeRest()
	{
		if (!this.mHasLoadedUserDataToCache)
		{
			this.mAutoSupplyTimeRest = UserDataController.GetArmorAutoSupplyTime(this.mName);
			this.mHasLoadedUserDataToCache = true;
		}
		return this.mAutoSupplyTimeRest;
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x0004F050 File Offset: 0x0004D450
	public void CachedAutoSupplyTimeDecreaseUpdate(float deltaTime)
	{
		float cachedAutoSupplyTimeRest = this.GetCachedAutoSupplyTimeRest();
		if (cachedAutoSupplyTimeRest <= 0f)
		{
			this.mIsInAutoSupplyStatus = false;
		}
		else
		{
			this.mNeedWriteDataToDisk = true;
			this.mAutoSupplyTimeRest = Mathf.Max(cachedAutoSupplyTimeRest - deltaTime, 0f);
			this.mIsInAutoSupplyStatus = true;
		}
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x0004F09C File Offset: 0x0004D49C
	public void WriteDataFromCacheToDisk()
	{
		if (this.mNeedWriteDataToDisk)
		{
			UserDataController.SetArmorAutoSupplyTime(this.mName, this.mAutoSupplyTimeRest);
			this.mNeedWriteDataToDisk = false;
			this.mHasLoadedUserDataToCache = false;
		}
	}

	// Token: 0x04000A60 RID: 2656
	public GItemId mId;

	// Token: 0x04000A61 RID: 2657
	public string mName;

	// Token: 0x04000A62 RID: 2658
	public GItemPurchaseType mPurchasedType;

	// Token: 0x04000A63 RID: 2659
	public int mUnlockCLevel;

	// Token: 0x04000A64 RID: 2660
	public int mSinglePrice;

	// Token: 0x04000A65 RID: 2661
	public int mCoinsPriceInGame;

	// Token: 0x04000A66 RID: 2662
	public string mNameDisplay;

	// Token: 0x04000A67 RID: 2663
	public string mInGameLogoSpriteNameFg;

	// Token: 0x04000A68 RID: 2664
	public string mInGameLogoSpriteNameBg;

	// Token: 0x04000A69 RID: 2665
	public string mInGameLogoSpriteName;

	// Token: 0x04000A6A RID: 2666
	public bool mIsEnabled;

	// Token: 0x04000A6B RID: 2667
	public bool mIsEquipped;

	// Token: 0x04000A6C RID: 2668
	public float mAutoSupplyTimeRest;

	// Token: 0x04000A6D RID: 2669
	public string mDescription;

	// Token: 0x04000A6E RID: 2670
	public float[] mHeadshotIgnoreRate = new float[2];

	// Token: 0x04000A6F RID: 2671
	public float[] mBodyDamageDefendRate = new float[2];

	// Token: 0x04000A70 RID: 2672
	public string _mEncryptDurabilityInGame = string.Empty;

	// Token: 0x04000A71 RID: 2673
	public int _mDurabilityInGame = 100;

	// Token: 0x04000A72 RID: 2674
	public bool mIsInAutoSupplyStatus;

	// Token: 0x04000A73 RID: 2675
	public int[] mAutoSupplyTimeFillLevel = new int[]
	{
		3,
		6,
		12,
		18,
		24
	};

	// Token: 0x04000A74 RID: 2676
	private bool mHasLoadedUserDataToCache;

	// Token: 0x04000A75 RID: 2677
	private bool mNeedWriteDataToDisk;
}
