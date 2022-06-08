using System;
using UnityEngine;

// Token: 0x02000266 RID: 614
public class GGSingleEnemyManager : MonoBehaviour
{
	// Token: 0x0600119A RID: 4506 RVA: 0x0009DFAA File Offset: 0x0009C3AA
	private void Awake()
	{
		GGSingleEnemyManager.mInstance = this;
		this.Difficulty = PlayerPrefs.GetInt("SingleModeChapterOneDifficulty", 1);
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x0009DFC3 File Offset: 0x0009C3C3
	private void OnDestroy()
	{
		GGSingleEnemyManager.mInstance = null;
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x0009DFCC File Offset: 0x0009C3CC
	private void Start()
	{
		this.playerRushTrigger = GameObject.FindWithTag("Player").transform.Find("rushTrigger").gameObject;
		switch (this.Difficulty)
		{
		case 2:
			this.playerRushTrigger.GetComponent<BoxCollider>().size *= 1.11f;
			break;
		case 3:
			this.playerRushTrigger.GetComponent<BoxCollider>().size *= 1.22f;
			break;
		}
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
								this.currentSenceEnemyNumber = 40;
								this.singleModeEnemyArrange = new int[this.currentSenceEnemyNumber][];
								int[] array = new int[]
								{
									1,
									1,
									1,
									1,
									1,
									2,
									1,
									1,
									1,
									2,
									1,
									1,
									1,
									2,
									1,
									1,
									1,
									1,
									1,
									2,
									2,
									1,
									1,
									1,
									1,
									1,
									2,
									2,
									2,
									3,
									1,
									1,
									1,
									1,
									2,
									1,
									2,
									1,
									2,
									1
								};
								int[] array2 = new int[]
								{
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									2,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									1,
									2,
									1,
									1,
									1,
									1,
									1,
									2,
									1,
									1,
									1,
									1,
									1
								};
								for (int i = 0; i < this.currentSenceEnemyNumber; i++)
								{
									this.singleModeEnemyArrange[i] = new int[2];
								}
								for (int j = 0; j < this.currentSenceEnemyNumber; j++)
								{
									this.singleModeEnemyArrange[j][0] = array[j];
									this.singleModeEnemyArrange[j][1] = array2[j];
								}
							}
						}
						else
						{
							this.currentSenceEnemyNumber = 82;
							this.singleModeEnemyArrange = new int[this.currentSenceEnemyNumber][];
							int[] array3 = new int[]
							{
								1,
								1,
								1,
								1,
								3,
								2,
								1,
								1,
								1,
								2,
								1,
								1,
								1,
								2,
								1,
								1,
								1,
								1,
								1,
								2,
								2,
								1,
								1,
								3,
								2,
								1,
								3,
								2,
								2,
								1,
								1,
								1,
								3,
								1,
								2,
								1,
								2,
								1,
								2,
								3,
								1,
								1,
								1,
								2,
								2,
								1,
								2,
								3,
								3,
								3,
								3,
								1,
								1,
								1,
								2,
								2,
								1,
								1,
								2,
								1,
								1,
								2,
								1,
								1,
								1,
								1,
								1,
								2,
								1,
								2,
								2,
								2,
								2,
								2,
								1,
								1,
								2,
								2,
								1,
								2,
								2,
								2
							};
							int[] array4 = new int[]
							{
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								2,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								2,
								1,
								1,
								1,
								1,
								1,
								2,
								1,
								1,
								1,
								1,
								1,
								1,
								2,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								3,
								2,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								2,
								1,
								1,
								1,
								1,
								1,
								1,
								1,
								2,
								1,
								1,
								1,
								1,
								1,
								1,
								2,
								1,
								1,
								1,
								2,
								2,
								3
							};
							for (int k = 0; k < this.currentSenceEnemyNumber; k++)
							{
								this.singleModeEnemyArrange[k] = new int[2];
							}
							for (int l = 0; l < this.currentSenceEnemyNumber; l++)
							{
								this.singleModeEnemyArrange[l][0] = array3[l];
								this.singleModeEnemyArrange[l][1] = array4[l];
							}
						}
					}
					else
					{
						this.currentSenceEnemyNumber = 96;
						this.singleModeEnemyArrange = new int[this.currentSenceEnemyNumber][];
						int[] array5 = new int[]
						{
							1,
							2,
							1,
							1,
							1,
							2,
							4,
							1,
							4,
							2,
							1,
							1,
							4,
							2,
							2,
							1,
							3,
							4,
							3,
							2,
							2,
							1,
							4,
							1,
							3,
							1,
							3,
							4,
							2,
							3,
							1,
							1,
							1,
							4,
							2,
							3,
							2,
							4,
							2,
							1,
							2,
							4,
							1,
							4,
							2,
							3,
							1,
							4,
							2,
							2,
							1,
							1,
							4,
							2,
							2,
							2,
							1,
							4,
							3,
							4,
							1,
							2,
							4,
							2,
							3,
							1,
							1,
							1,
							4,
							2,
							1,
							2,
							3,
							2,
							1,
							2,
							1,
							4,
							1,
							4,
							1,
							2,
							4,
							2,
							2,
							1,
							2,
							1,
							4,
							3,
							1,
							2,
							4,
							2,
							2,
							4
						};
						int[] array6 = new int[]
						{
							1,
							1,
							1,
							1,
							1,
							1,
							1,
							2,
							1,
							1,
							1,
							1,
							3,
							2,
							1,
							1,
							1,
							1,
							1,
							2,
							1,
							2,
							1,
							3,
							1,
							1,
							1,
							1,
							2,
							1,
							1,
							1,
							2,
							1,
							2,
							1,
							1,
							3,
							1,
							1,
							1,
							1,
							2,
							1,
							1,
							1,
							1,
							1,
							1,
							1,
							1,
							1,
							1,
							2,
							3,
							1,
							1,
							2,
							1,
							1,
							1,
							1,
							1,
							2,
							1,
							1,
							2,
							1,
							1,
							2,
							1,
							1,
							1,
							1,
							1,
							1,
							2,
							3,
							2,
							1,
							1,
							1,
							1,
							1,
							1,
							1,
							1,
							1,
							1,
							2,
							1,
							1,
							1,
							1,
							2,
							3
						};
						for (int m = 0; m < this.currentSenceEnemyNumber; m++)
						{
							this.singleModeEnemyArrange[m] = new int[2];
						}
						for (int n = 0; n < this.currentSenceEnemyNumber; n++)
						{
							this.singleModeEnemyArrange[n][0] = array5[n];
							this.singleModeEnemyArrange[n][1] = array6[n];
						}
					}
				}
				else
				{
					this.currentSenceEnemyNumber = 75;
					this.singleModeEnemyArrange = new int[this.currentSenceEnemyNumber][];
					int[] array7 = new int[]
					{
						1,
						1,
						2,
						2,
						1,
						2,
						1,
						1,
						3,
						3,
						3,
						3,
						3,
						3,
						3,
						3,
						1,
						1,
						1,
						2,
						2,
						1,
						1,
						1,
						2,
						1,
						2,
						2,
						2,
						1,
						1,
						3,
						1,
						1,
						2,
						1,
						2,
						1,
						2,
						1,
						1,
						1,
						1,
						2,
						1,
						2,
						1,
						1,
						2,
						3,
						3,
						1,
						1,
						2,
						2,
						2,
						2,
						1,
						1,
						1,
						1,
						2,
						2,
						1,
						2,
						1,
						1,
						1,
						2,
						1,
						2,
						1,
						2,
						2,
						2
					};
					int[] array8 = new int[]
					{
						1,
						1,
						1,
						3,
						1,
						2,
						1,
						2,
						1,
						2,
						1,
						1,
						1,
						2,
						1,
						1,
						1,
						2,
						1,
						2,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						2,
						1,
						1,
						1,
						1,
						1,
						2,
						1,
						2,
						1,
						1,
						1,
						1,
						1,
						1,
						2,
						1,
						1,
						1,
						1,
						1,
						1,
						2,
						1,
						1,
						2,
						1,
						1,
						1,
						1,
						2,
						1,
						1,
						1,
						1,
						1,
						1,
						1,
						2,
						1,
						1,
						1,
						1,
						1,
						2,
						2,
						3
					};
					for (int num = 0; num < this.currentSenceEnemyNumber; num++)
					{
						this.singleModeEnemyArrange[num] = new int[2];
					}
					for (int num2 = 0; num2 < this.currentSenceEnemyNumber; num2++)
					{
						this.singleModeEnemyArrange[num2][0] = array7[num2];
						this.singleModeEnemyArrange[num2][1] = array8[num2];
					}
				}
			}
			else
			{
				this.currentSenceEnemyNumber = 75;
				this.singleModeEnemyArrange = new int[this.currentSenceEnemyNumber][];
				int[] array9 = new int[]
				{
					1,
					1,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					1,
					1,
					2,
					2,
					1,
					1,
					1,
					1,
					1,
					2,
					2,
					2,
					1,
					1,
					2,
					2,
					2,
					2,
					1,
					2,
					1,
					2,
					3,
					3,
					2,
					1,
					2,
					1,
					2,
					2,
					1,
					1,
					1,
					2,
					2,
					2,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					1,
					2,
					3,
					1,
					2,
					1,
					2
				};
				int[] array10 = new int[]
				{
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					2,
					1,
					2,
					1,
					1,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					1,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					1,
					1,
					1,
					2,
					1,
					1,
					1,
					2,
					1,
					2,
					2
				};
				for (int num3 = 0; num3 < this.currentSenceEnemyNumber; num3++)
				{
					this.singleModeEnemyArrange[num3] = new int[2];
				}
				for (int num4 = 0; num4 < this.currentSenceEnemyNumber; num4++)
				{
					this.singleModeEnemyArrange[num4][0] = array9[num4];
					this.singleModeEnemyArrange[num4][1] = array10[num4];
				}
			}
		}
		this.generatePosition = new GameObject[this.currentSenceEnemyNumber];
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x0009E43B File Offset: 0x0009C83B
	private void Update()
	{
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x0009E440 File Offset: 0x0009C840
	private void GenerateEnemyByTrigger(int triggerId)
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
								this.triggerNum = 5;
								this.GenerateEnemyArrange = new int[this.triggerNum];
								int[] generateEnemyArrange = new int[]
								{
									20,
									25,
									35,
									45,
									50
								};
								this.GenerateEnemyArrange = generateEnemyArrange;
							}
						}
						else
						{
							this.triggerNum = 5;
							this.GenerateEnemyArrange = new int[this.triggerNum];
							int[] generateEnemyArrange2 = new int[]
							{
								13,
								31,
								51,
								67,
								82
							};
							this.GenerateEnemyArrange = generateEnemyArrange2;
						}
					}
					else
					{
						this.triggerNum = 5;
						this.GenerateEnemyArrange = new int[this.triggerNum];
						int[] generateEnemyArrange3 = new int[]
						{
							20,
							46,
							59,
							73,
							96
						};
						this.GenerateEnemyArrange = generateEnemyArrange3;
					}
				}
				else
				{
					this.triggerNum = 4;
					this.GenerateEnemyArrange = new int[this.triggerNum];
					int[] generateEnemyArrange4 = new int[]
					{
						16,
						51,
						60,
						75
					};
					this.GenerateEnemyArrange = generateEnemyArrange4;
				}
			}
			else
			{
				this.triggerNum = 5;
				this.GenerateEnemyArrange = new int[this.triggerNum];
				int[] generateEnemyArrange5 = new int[]
				{
					25,
					41,
					51,
					64,
					75
				};
				this.GenerateEnemyArrange = generateEnemyArrange5;
			}
		}
		switch (triggerId)
		{
		case 1:
			for (int i = 0; i < this.GenerateEnemyArrange[0]; i++)
			{
				this.generatePosition[i] = GameObject.Find("generatePosition" + (i + 1).ToString());
				int num = this.singleModeEnemyArrange[i][0];
				int num2 = this.singleModeEnemyArrange[i][1];
				switch (num)
				{
				case 1:
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.knifeEnemy, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject.GetComponent<GGSingleEnemyLogic>().enemyId = i;
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject2.name = "enemyTempGenerateTransform" + i;
					GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject3.name = "enemyTempAttackTransform" + i;
					GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject4.name = "enemyTempRushTransform" + i;
					GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject5.name = "enemyTempPatrolTransform" + i;
					switch (num2)
					{
					case 1:
						gameObject.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 2:
				{
					GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(this.gunEnemy, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject6.GetComponent<GGSingleEnemyLogic>().enemyId = i;
					GameObject gameObject7 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject7.name = "enemyTempGenerateTransform" + i;
					GameObject gameObject8 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject8.name = "enemyTempAttackTransform" + i;
					GameObject gameObject9 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject9.name = "enemyTempRushTransform" + i;
					GameObject gameObject10 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject10.name = "enemyTempPatrolTransform" + i;
					switch (num2)
					{
					case 1:
						gameObject6.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject6.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject6.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 3:
				{
					GameObject gameObject11 = UnityEngine.Object.Instantiate<GameObject>(this.snipeEnemy, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject11.GetComponent<GGSingleEnemyLogic>().enemyId = i;
					GameObject gameObject12 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject12.name = "enemyTempGenerateTransform" + i;
					GameObject gameObject13 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject13.name = "enemyTempAttackTransform" + i;
					GameObject gameObject14 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject14.name = "enemyTempRushTransform" + i;
					GameObject gameObject15 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject15.name = "enemyTempPatrolTransform" + i;
					switch (num2)
					{
					case 1:
						gameObject11.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject11.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject11.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 4:
				{
					GameObject gameObject16 = UnityEngine.Object.Instantiate<GameObject>(this.grenadeEnemy, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject16.GetComponent<GGSingleEnemyLogic>().enemyId = i;
					GameObject gameObject17 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject17.name = "enemyTempGenerateTransform" + i;
					GameObject gameObject18 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject18.name = "enemyTempAttackTransform" + i;
					GameObject gameObject19 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject19.name = "enemyTempRushTransform" + i;
					GameObject gameObject20 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[i].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[i].transform.rotation);
					gameObject20.name = "enemyTempPatrolTransform" + i;
					switch (num2)
					{
					case 1:
						gameObject16.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject16.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject16.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				}
			}
			break;
		case 2:
			for (int j = this.GenerateEnemyArrange[0]; j < this.GenerateEnemyArrange[1]; j++)
			{
				this.generatePosition[j] = GameObject.Find("generatePosition" + (j + 1).ToString());
				int num3 = this.singleModeEnemyArrange[j][0];
				int num4 = this.singleModeEnemyArrange[j][1];
				switch (num3)
				{
				case 1:
				{
					GameObject gameObject21 = UnityEngine.Object.Instantiate<GameObject>(this.knifeEnemy, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject21.GetComponent<GGSingleEnemyLogic>().enemyId = j;
					GameObject gameObject22 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject22.name = "enemyTempGenerateTransform" + j;
					GameObject gameObject23 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject23.name = "enemyTempAttackTransform" + j;
					GameObject gameObject24 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject24.name = "enemyTempRushTransform" + j;
					GameObject gameObject25 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject25.name = "enemyTempPatrolTransform" + j;
					switch (num4)
					{
					case 1:
						gameObject21.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject21.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject21.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 2:
				{
					GameObject gameObject26 = UnityEngine.Object.Instantiate<GameObject>(this.gunEnemy, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject26.GetComponent<GGSingleEnemyLogic>().enemyId = j;
					GameObject gameObject27 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject27.name = "enemyTempGenerateTransform" + j;
					GameObject gameObject28 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject28.name = "enemyTempAttackTransform" + j;
					GameObject gameObject29 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject29.name = "enemyTempRushTransform" + j;
					GameObject gameObject30 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject30.name = "enemyTempPatrolTransform" + j;
					switch (num4)
					{
					case 1:
						gameObject26.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject26.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject26.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 3:
				{
					GameObject gameObject31 = UnityEngine.Object.Instantiate<GameObject>(this.snipeEnemy, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject31.GetComponent<GGSingleEnemyLogic>().enemyId = j;
					GameObject gameObject32 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject32.name = "enemyTempGenerateTransform" + j;
					GameObject gameObject33 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject33.name = "enemyTempAttackTransform" + j;
					GameObject gameObject34 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject34.name = "enemyTempRushTransform" + j;
					GameObject gameObject35 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject35.name = "enemyTempPatrolTransform" + j;
					switch (num4)
					{
					case 1:
						gameObject31.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject31.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject31.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 4:
				{
					GameObject gameObject36 = UnityEngine.Object.Instantiate<GameObject>(this.grenadeEnemy, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject36.GetComponent<GGSingleEnemyLogic>().enemyId = j;
					GameObject gameObject37 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject37.name = "enemyTempGenerateTransform" + j;
					GameObject gameObject38 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject38.name = "enemyTempAttackTransform" + j;
					GameObject gameObject39 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject39.name = "enemyTempRushTransform" + j;
					GameObject gameObject40 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[j].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[j].transform.rotation);
					gameObject40.name = "enemyTempPatrolTransform" + j;
					switch (num4)
					{
					case 1:
						gameObject36.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject36.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject36.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				}
			}
			break;
		case 3:
			for (int k = this.GenerateEnemyArrange[1]; k < this.GenerateEnemyArrange[2]; k++)
			{
				this.generatePosition[k] = GameObject.Find("generatePosition" + (k + 1).ToString());
				int num5 = this.singleModeEnemyArrange[k][0];
				int num6 = this.singleModeEnemyArrange[k][1];
				switch (num5)
				{
				case 1:
				{
					GameObject gameObject41 = UnityEngine.Object.Instantiate<GameObject>(this.knifeEnemy, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject41.GetComponent<GGSingleEnemyLogic>().enemyId = k;
					GameObject gameObject42 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject42.name = "enemyTempGenerateTransform" + k;
					GameObject gameObject43 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject43.name = "enemyTempAttackTransform" + k;
					GameObject gameObject44 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject44.name = "enemyTempRushTransform" + k;
					GameObject gameObject45 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject45.name = "enemyTempPatrolTransform" + k;
					switch (num6)
					{
					case 1:
						gameObject41.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject41.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject41.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 2:
				{
					GameObject gameObject46 = UnityEngine.Object.Instantiate<GameObject>(this.gunEnemy, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject46.GetComponent<GGSingleEnemyLogic>().enemyId = k;
					GameObject gameObject47 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject47.name = "enemyTempGenerateTransform" + k;
					GameObject gameObject48 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject48.name = "enemyTempAttackTransform" + k;
					GameObject gameObject49 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject49.name = "enemyTempRushTransform" + k;
					GameObject gameObject50 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject50.name = "enemyTempPatrolTransform" + k;
					switch (num6)
					{
					case 1:
						gameObject46.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject46.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject46.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 3:
				{
					GameObject gameObject51 = UnityEngine.Object.Instantiate<GameObject>(this.snipeEnemy, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject51.GetComponent<GGSingleEnemyLogic>().enemyId = k;
					GameObject gameObject52 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject52.name = "enemyTempGenerateTransform" + k;
					GameObject gameObject53 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject53.name = "enemyTempAttackTransform" + k;
					GameObject gameObject54 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject54.name = "enemyTempRushTransform" + k;
					GameObject gameObject55 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject55.name = "enemyTempPatrolTransform" + k;
					switch (num6)
					{
					case 1:
						gameObject51.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject51.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject51.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 4:
				{
					GameObject gameObject56 = UnityEngine.Object.Instantiate<GameObject>(this.grenadeEnemy, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject56.GetComponent<GGSingleEnemyLogic>().enemyId = k;
					GameObject gameObject57 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject57.name = "enemyTempGenerateTransform" + k;
					GameObject gameObject58 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject58.name = "enemyTempAttackTransform" + k;
					GameObject gameObject59 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject59.name = "enemyTempRushTransform" + k;
					GameObject gameObject60 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[k].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[k].transform.rotation);
					gameObject60.name = "enemyTempPatrolTransform" + k;
					switch (num6)
					{
					case 1:
						gameObject56.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject56.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject56.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				}
			}
			break;
		case 4:
			for (int l = this.GenerateEnemyArrange[2]; l < this.GenerateEnemyArrange[3]; l++)
			{
				this.generatePosition[l] = GameObject.Find("generatePosition" + (l + 1).ToString());
				int num7 = this.singleModeEnemyArrange[l][0];
				int num8 = this.singleModeEnemyArrange[l][1];
				switch (num7)
				{
				case 1:
				{
					GameObject gameObject61 = UnityEngine.Object.Instantiate<GameObject>(this.knifeEnemy, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject61.GetComponent<GGSingleEnemyLogic>().enemyId = l;
					GameObject gameObject62 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject62.name = "enemyTempGenerateTransform" + l;
					GameObject gameObject63 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject63.name = "enemyTempAttackTransform" + l;
					GameObject gameObject64 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject64.name = "enemyTempRushTransform" + l;
					GameObject gameObject65 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject65.name = "enemyTempPatrolTransform" + l;
					switch (num8)
					{
					case 1:
						gameObject61.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject61.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject61.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 2:
				{
					GameObject gameObject66 = UnityEngine.Object.Instantiate<GameObject>(this.gunEnemy, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject66.GetComponent<GGSingleEnemyLogic>().enemyId = l;
					GameObject gameObject67 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject67.name = "enemyTempGenerateTransform" + l;
					GameObject gameObject68 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject68.name = "enemyTempAttackTransform" + l;
					GameObject gameObject69 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject69.name = "enemyTempRushTransform" + l;
					GameObject gameObject70 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject70.name = "enemyTempPatrolTransform" + l;
					switch (num8)
					{
					case 1:
						gameObject66.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject66.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject66.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 3:
				{
					GameObject gameObject71 = UnityEngine.Object.Instantiate<GameObject>(this.snipeEnemy, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject71.GetComponent<GGSingleEnemyLogic>().enemyId = l;
					GameObject gameObject72 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject72.name = "enemyTempGenerateTransform" + l;
					GameObject gameObject73 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject73.name = "enemyTempAttackTransform" + l;
					GameObject gameObject74 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject74.name = "enemyTempRushTransform" + l;
					GameObject gameObject75 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject75.name = "enemyTempPatrolTransform" + l;
					switch (num8)
					{
					case 1:
						gameObject71.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject71.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject71.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 4:
				{
					GameObject gameObject76 = UnityEngine.Object.Instantiate<GameObject>(this.grenadeEnemy, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject76.GetComponent<GGSingleEnemyLogic>().enemyId = l;
					GameObject gameObject77 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject77.name = "enemyTempGenerateTransform" + l;
					GameObject gameObject78 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject78.name = "enemyTempAttackTransform" + l;
					GameObject gameObject79 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject79.name = "enemyTempRushTransform" + l;
					GameObject gameObject80 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[l].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[l].transform.rotation);
					gameObject80.name = "enemyTempPatrolTransform" + l;
					switch (num8)
					{
					case 1:
						gameObject76.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject76.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject76.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				}
			}
			break;
		case 5:
			for (int m = this.GenerateEnemyArrange[3]; m < this.GenerateEnemyArrange[4]; m++)
			{
				this.generatePosition[m] = GameObject.Find("generatePosition" + (m + 1).ToString());
				int num9 = this.singleModeEnemyArrange[m][0];
				int num10 = this.singleModeEnemyArrange[m][1];
				switch (num9)
				{
				case 1:
				{
					GameObject gameObject81 = UnityEngine.Object.Instantiate<GameObject>(this.knifeEnemy, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject81.GetComponent<GGSingleEnemyLogic>().enemyId = m;
					GameObject gameObject82 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject82.name = "enemyTempGenerateTransform" + m;
					GameObject gameObject83 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject83.name = "enemyTempAttackTransform" + m;
					GameObject gameObject84 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject84.name = "enemyTempRushTransform" + m;
					GameObject gameObject85 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject85.name = "enemyTempPatrolTransform" + m;
					switch (num10)
					{
					case 1:
						gameObject81.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject81.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject81.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 2:
				{
					GameObject gameObject86 = UnityEngine.Object.Instantiate<GameObject>(this.gunEnemy, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject86.GetComponent<GGSingleEnemyLogic>().enemyId = m;
					GameObject gameObject87 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject87.name = "enemyTempGenerateTransform" + m;
					GameObject gameObject88 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject88.name = "enemyTempAttackTransform" + m;
					GameObject gameObject89 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject89.name = "enemyTempRushTransform" + m;
					GameObject gameObject90 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject90.name = "enemyTempPatrolTransform" + m;
					switch (num10)
					{
					case 1:
						gameObject86.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject86.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject86.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 3:
				{
					GameObject gameObject91 = UnityEngine.Object.Instantiate<GameObject>(this.snipeEnemy, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject91.GetComponent<GGSingleEnemyLogic>().enemyId = m;
					GameObject gameObject92 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject92.name = "enemyTempGenerateTransform" + m;
					GameObject gameObject93 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject93.name = "enemyTempAttackTransform" + m;
					GameObject gameObject94 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject94.name = "enemyTempRushTransform" + m;
					GameObject gameObject95 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject95.name = "enemyTempPatrolTransform" + m;
					switch (num10)
					{
					case 1:
						gameObject91.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject91.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject91.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				case 4:
				{
					GameObject gameObject96 = UnityEngine.Object.Instantiate<GameObject>(this.grenadeEnemy, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject96.GetComponent<GGSingleEnemyLogic>().enemyId = m;
					GameObject gameObject97 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject97.name = "enemyTempGenerateTransform" + m;
					GameObject gameObject98 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject98.name = "enemyTempAttackTransform" + m;
					GameObject gameObject99 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject99.name = "enemyTempRushTransform" + m;
					GameObject gameObject100 = UnityEngine.Object.Instantiate<GameObject>(this.tempPositionObj, this.generatePosition[m].transform.position + new Vector3(0f, 0.7f, 0f), this.generatePosition[m].transform.rotation);
					gameObject100.name = "enemyTempPatrolTransform" + m;
					switch (num10)
					{
					case 1:
						gameObject96.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.custom;
						break;
					case 2:
						gameObject96.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.elite;
						break;
					case 3:
						gameObject96.GetComponent<GGSingleEnemyLogic>().enemyLv = EnemyLv.boss;
						break;
					}
					break;
				}
				}
			}
			break;
		}
	}

	// Token: 0x0400146E RID: 5230
	public static GGSingleEnemyManager mInstance;

	// Token: 0x0400146F RID: 5231
	public int Difficulty = 1;

	// Token: 0x04001470 RID: 5232
	public GameObject knifeEnemy;

	// Token: 0x04001471 RID: 5233
	public GameObject gunEnemy;

	// Token: 0x04001472 RID: 5234
	public GameObject snipeEnemy;

	// Token: 0x04001473 RID: 5235
	public GameObject grenadeEnemy;

	// Token: 0x04001474 RID: 5236
	public GameObject tempPositionObj;

	// Token: 0x04001475 RID: 5237
	private int currentSenceEnemyNumber;

	// Token: 0x04001476 RID: 5238
	private GameObject[] generatePosition;

	// Token: 0x04001477 RID: 5239
	public int[][] singleModeEnemyArrange;

	// Token: 0x04001478 RID: 5240
	public int[] singleModeOneEnemyTypeArrange;

	// Token: 0x04001479 RID: 5241
	public int[] singleModeOneEnemyLvArrange;

	// Token: 0x0400147A RID: 5242
	public int[] singleModeTwoEnemyTypeArrange;

	// Token: 0x0400147B RID: 5243
	public int[] singleModeTwoEnemyLvArrange;

	// Token: 0x0400147C RID: 5244
	public int[] singleModeThreeEnemyTypeArrange;

	// Token: 0x0400147D RID: 5245
	public int[] singleModeThreeEnemyLvArrange;

	// Token: 0x0400147E RID: 5246
	public int[] singleModeFourEnemyTypeArrange;

	// Token: 0x0400147F RID: 5247
	public int[] singleModeFourEnemyLvArrange;

	// Token: 0x04001480 RID: 5248
	public int[] singleModeFiveEnemyTypeArrange;

	// Token: 0x04001481 RID: 5249
	public int[] singleModeFiveEnemyLvArrange;

	// Token: 0x04001482 RID: 5250
	public int triggerNum;

	// Token: 0x04001483 RID: 5251
	public int[] GenerateEnemyArrange;

	// Token: 0x04001484 RID: 5252
	public int[] GenerateEnemyArrangeOne;

	// Token: 0x04001485 RID: 5253
	public int[] GenerateEnemyArrangeTwo;

	// Token: 0x04001486 RID: 5254
	public int[] GenerateEnemyArrangeThree;

	// Token: 0x04001487 RID: 5255
	public int[] GenerateEnemyArrangeFour;

	// Token: 0x04001488 RID: 5256
	public int[] GenerateEnemyArrangeFive;

	// Token: 0x04001489 RID: 5257
	private GameObject playerRushTrigger;
}
