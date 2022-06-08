using System;
using UnityEngine;

// Token: 0x0200026B RID: 619
public class GGSingleModeBulletAndBloodPakGenerate : MonoBehaviour
{
	// Token: 0x060011AE RID: 4526 RVA: 0x000A17BC File Offset: 0x0009FBBC
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
								this.bulletPakNum = 4;
								this.bulletPakPositionNum = 8;
								this.bloodPakNum = 1;
								this.bloodPakPositionNum = 4;
							}
						}
						else
						{
							this.bulletPakNum = 4;
							this.bulletPakPositionNum = 8;
							this.bloodPakNum = 1;
							this.bloodPakPositionNum = 4;
						}
					}
					else
					{
						this.bulletPakNum = 4;
						this.bulletPakPositionNum = 8;
						this.bloodPakNum = 1;
						this.bloodPakPositionNum = 4;
					}
				}
				else
				{
					this.bulletPakNum = 4;
					this.bulletPakPositionNum = 8;
					this.bloodPakNum = 1;
					this.bloodPakPositionNum = 4;
				}
			}
			else
			{
				this.bulletPakNum = 4;
				this.bulletPakPositionNum = 8;
				this.bloodPakNum = 1;
				this.bloodPakPositionNum = 4;
			}
		}
		this.bulletPakGeneratePositions = new GameObject[this.bulletPakPositionNum];
		for (int i = 0; i < this.bulletPakPositionNum; i++)
		{
			this.bulletPakGeneratePositions[i] = base.transform.Find("bulletPakGeneratePosition" + (i + 1).ToString()).gameObject;
		}
		this.bloodPakGeneratePositions = new GameObject[this.bloodPakPositionNum];
		for (int j = 0; j < this.bloodPakPositionNum; j++)
		{
			this.bloodPakGeneratePositions[j] = base.transform.Find("bloodPakGeneratePosition" + (j + 1).ToString()).gameObject;
		}
		for (int k = 0; k < this.bulletPakNum; k++)
		{
			int num = UnityEngine.Random.Range(0, this.bulletPakPositionNum);
			UnityEngine.Object.Instantiate<GameObject>(this.bulletPak, this.bulletPakGeneratePositions[num].transform.position, this.bulletPakGeneratePositions[num].transform.rotation);
		}
		for (int l = 0; l < this.bloodPakNum; l++)
		{
			int num2 = UnityEngine.Random.Range(0, this.bloodPakPositionNum);
			UnityEngine.Object.Instantiate<GameObject>(this.bloodPak, this.bloodPakGeneratePositions[num2].transform.position, this.bloodPakGeneratePositions[num2].transform.rotation);
		}
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x000A1A41 File Offset: 0x0009FE41
	private void Update()
	{
	}

	// Token: 0x0400148E RID: 5262
	public GameObject bloodPak;

	// Token: 0x0400148F RID: 5263
	public GameObject bulletPak;

	// Token: 0x04001490 RID: 5264
	private int bulletPakNum;

	// Token: 0x04001491 RID: 5265
	private int bulletPakPositionNum;

	// Token: 0x04001492 RID: 5266
	private GameObject[] bulletPakGeneratePositions;

	// Token: 0x04001493 RID: 5267
	private int bloodPakNum;

	// Token: 0x04001494 RID: 5268
	private int bloodPakPositionNum;

	// Token: 0x04001495 RID: 5269
	private GameObject[] bloodPakGeneratePositions;
}
