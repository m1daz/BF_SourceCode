using System;

namespace GrowthSystem
{
	// Token: 0x02000170 RID: 368
	public class ExpCalcer
	{
		// Token: 0x06000A8C RID: 2700 RVA: 0x0004C26C File Offset: 0x0004A66C
		public int GetCharacterCurLevelUpExpNeed(int curLevel)
		{
			int num = 0;
			for (int i = 0; i < GrowthBaseValue.mGBV_BaseLevelLayoutMatrix.Length; i++)
			{
				num += GrowthBaseValue.mGBV_BaseLevelLayoutMatrix[i];
				if (curLevel <= num)
				{
					return GrowthBaseValue.mGBV_BaseExpMatrix[i] * 20;
				}
			}
			return 99999999;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0004C2B4 File Offset: 0x0004A6B4
		public int GetCharacterCurLevelExpExist(int curLevel)
		{
			int characterExp = UserDataController.GetCharacterExp();
			int num = 1;
			int num2 = 0;
			for (int i = 0; i < GrowthBaseValue.mGBV_BaseLevelLayoutMatrix.Length; i++)
			{
				int j = 0;
				while (j < GrowthBaseValue.mGBV_BaseLevelLayoutMatrix[i])
				{
					if (num == curLevel)
					{
						if (characterExp - num2 <= 0)
						{
							if (characterExp < num2)
							{
								UserDataController.SetCharacterExp(num2);
							}
							return 0;
						}
						return characterExp - num2;
					}
					else
					{
						num++;
						num2 += GrowthBaseValue.mGBV_BaseExpMatrix[i] * 20;
						j++;
					}
				}
			}
			return 0;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0004C334 File Offset: 0x0004A734
		public int AddCharacterExpAndChkLevelUp(int expAdd)
		{
			UserDataController.AddCharacterExp(expAdd);
			int num = UserDataController.GetCharacterLevel();
			int characterCurLevelUpExpNeed = this.GetCharacterCurLevelUpExpNeed(num);
			int characterCurLevelExpExist = this.GetCharacterCurLevelExpExist(num);
			int num2 = 0;
			while (characterCurLevelExpExist >= characterCurLevelUpExpNeed && num < 100)
			{
				UserDataController.SetCharacterLevel(num + 1);
				num++;
				characterCurLevelUpExpNeed = this.GetCharacterCurLevelUpExpNeed(num);
				characterCurLevelExpExist = this.GetCharacterCurLevelExpExist(num);
				num2++;
			}
			return num2;
		}
	}
}
