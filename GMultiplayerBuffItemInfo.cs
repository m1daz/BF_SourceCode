using System;
using System.Collections.Generic;
using GrowthSystem;

// Token: 0x02000196 RID: 406
[Serializable]
public class GMultiplayerBuffItemInfo
{
	// Token: 0x06000AE7 RID: 2791 RVA: 0x0004F240 File Offset: 0x0004D640
	public bool UseBuff()
	{
		int multiplayerBuffNum = GrowthManger.mInstance.GetMultiplayerBuffNum(this.mName);
		if (multiplayerBuffNum >= 1)
		{
			this.mMaxEffectTime_S = this.mBaseMaxEffectTime_S * (1f + GrowthManger.mInstance.playerEnchantmentProperty.allDic[EnchantmentType.PotionTimePlus].additionValue);
			if (this.mEnchantmentDetails.Count > 0)
			{
				foreach (EnchantmentDetails enchantmentDetails in this.mEnchantmentDetails)
				{
					enchantmentDetails.validTimeRest = this.mMaxEffectTime_S;
					GrowthManger.mInstance.playerEnchantmentProperty.AddProperty(EnchantmentOriginType.PotionAddition, enchantmentDetails);
				}
			}
			GrowthManger.mInstance.SetMultiplayerBuffNum(this.mName, multiplayerBuffNum - 1);
			this.mExistNum = multiplayerBuffNum - 1;
			return true;
		}
		return false;
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x0004F328 File Offset: 0x0004D728
	public void AddBuffNum(int num)
	{
		GrowthManger.mInstance.SetMultiplayerBuffNum(this.mName, GrowthManger.mInstance.GetMultiplayerBuffNum(this.mName) + num);
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x0004F34C File Offset: 0x0004D74C
	public void AddBuffNum(int num, bool ignoreSoundEffect)
	{
		GrowthManger.mInstance.SetMultiplayerBuffNum(this.mName, GrowthManger.mInstance.GetMultiplayerBuffNum(this.mName) + num, ignoreSoundEffect);
	}

	// Token: 0x04000B0F RID: 2831
	public GItemId mId;

	// Token: 0x04000B10 RID: 2832
	public string mName;

	// Token: 0x04000B11 RID: 2833
	public int mUnlockCLevel;

	// Token: 0x04000B12 RID: 2834
	public GItemPurchaseType mPurchasedType;

	// Token: 0x04000B13 RID: 2835
	public BuffTypeInMultiplayer mType;

	// Token: 0x04000B14 RID: 2836
	public int mBindingPrice;

	// Token: 0x04000B15 RID: 2837
	public int mBindingNum;

	// Token: 0x04000B16 RID: 2838
	public int mExistNum;

	// Token: 0x04000B17 RID: 2839
	public string mNameDisplay;

	// Token: 0x04000B18 RID: 2840
	public string mLogoSpriteName;

	// Token: 0x04000B19 RID: 2841
	public float mBaseMaxEffectTime_S;

	// Token: 0x04000B1A RID: 2842
	public float mMaxEffectTime_S;

	// Token: 0x04000B1B RID: 2843
	public int mOffRate = 100;

	// Token: 0x04000B1C RID: 2844
	public string mOffRateDescription = string.Empty;

	// Token: 0x04000B1D RID: 2845
	public string mDescription = string.Empty;

	// Token: 0x04000B1E RID: 2846
	public List<EnchantmentDetails> mEnchantmentDetails = new List<EnchantmentDetails>();
}
