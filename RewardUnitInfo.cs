using System;

// Token: 0x02000186 RID: 390
public class RewardUnitInfo
{
	// Token: 0x04000AB2 RID: 2738
	public bool canGotReward;

	// Token: 0x04000AB3 RID: 2739
	public int rewardCoins;

	// Token: 0x04000AB4 RID: 2740
	public string rewardHonorName = string.Empty;

	// Token: 0x04000AB5 RID: 2741
	public string rewardDescription = string.Empty;

	// Token: 0x04000AB6 RID: 2742
	public int progressTargetValue;

	// Token: 0x04000AB7 RID: 2743
	public int progressCurValue;

	// Token: 0x04000AB8 RID: 2744
	public int progressRate;

	// Token: 0x04000AB9 RID: 2745
	public int rewardLv = 1;

	// Token: 0x04000ABA RID: 2746
	public string rewardLvStr = "1";

	// Token: 0x04000ABB RID: 2747
	public string rewardStrValue = string.Empty;

	// Token: 0x04000ABC RID: 2748
	public string spriteName = string.Empty;

	// Token: 0x04000ABD RID: 2749
	public int iNum;

	// Token: 0x04000ABE RID: 2750
	public string sNum = string.Empty;

	// Token: 0x04000ABF RID: 2751
	public RewardUnitInfo.RewardUnitDisplayInfo[] displayInfoList;

	// Token: 0x02000187 RID: 391
	public struct RewardUnitDisplayInfo
	{
		// Token: 0x04000AC0 RID: 2752
		public string spriteName;

		// Token: 0x04000AC1 RID: 2753
		public string sNum;
	}
}
