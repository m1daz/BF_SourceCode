using System;
using UnityEngine;

// Token: 0x0200026F RID: 623
public class GGSingleModeEggshellGenerateManager : MonoBehaviour
{
	// Token: 0x060011BB RID: 4539 RVA: 0x000A1CE8 File Offset: 0x000A00E8
	private void Start()
	{
		string loadedLevelName = Application.loadedLevelName;
		if (loadedLevelName != null)
		{
			if (!(loadedLevelName == "SingleMode_1"))
			{
				if (!(loadedLevelName == "SingleMode_2"))
				{
					if (!(loadedLevelName == "SingleMode_3"))
					{
						if (!(loadedLevelName == "SingleMode_4"))
						{
							if (loadedLevelName == "SingleMode_5")
							{
								this.eggshellPakNum = 1;
								this.eggshellPakPositionNum = 6;
							}
						}
						else
						{
							this.eggshellPakNum = 1;
							if (GGSingleEnemyManager.mInstance.Difficulty == 3)
							{
								this.eggshellPakNum = 2;
							}
							this.eggshellPakPositionNum = 6;
						}
					}
					else
					{
						this.eggshellPakNum = 1;
						if (GGSingleEnemyManager.mInstance.Difficulty == 3)
						{
							this.eggshellPakNum = 2;
						}
						this.eggshellPakPositionNum = 6;
					}
				}
				else
				{
					this.eggshellPakNum = 1;
					if (GGSingleEnemyManager.mInstance.Difficulty == 3)
					{
						this.eggshellPakNum = 2;
					}
					this.eggshellPakPositionNum = 6;
				}
			}
			else
			{
				this.eggshellPakNum = 0;
				this.eggshellPakPositionNum = 4;
			}
		}
		this.eggshellPakGeneratePositions = new GameObject[this.eggshellPakPositionNum];
		for (int i = 0; i < this.eggshellPakPositionNum; i++)
		{
			this.eggshellPakGeneratePositions[i] = base.transform.Find("eggshellPakGeneratePosition" + (i + 1).ToString()).gameObject;
		}
		for (int j = 0; j < this.eggshellPakNum; j++)
		{
			int num = UnityEngine.Random.Range(0, this.eggshellPakPositionNum);
			UnityEngine.Object.Instantiate<GameObject>(this.coinPak, this.eggshellPakGeneratePositions[num].transform.position, this.eggshellPakGeneratePositions[num].transform.rotation);
		}
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x000A1EB1 File Offset: 0x000A02B1
	private void Update()
	{
	}

	// Token: 0x0400149C RID: 5276
	public GameObject coinPak;

	// Token: 0x0400149D RID: 5277
	public GameObject jewelPak;

	// Token: 0x0400149E RID: 5278
	private int eggshellPakNum;

	// Token: 0x0400149F RID: 5279
	private int eggshellPakPositionNum;

	// Token: 0x040014A0 RID: 5280
	private GameObject[] eggshellPakGeneratePositions;
}
