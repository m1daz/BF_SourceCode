using System;
using UnityEngine;

// Token: 0x02000277 RID: 631
public class GGSingleModeSupplyManager : MonoBehaviour
{
	// Token: 0x060011EA RID: 4586 RVA: 0x000A2BE8 File Offset: 0x000A0FE8
	private void Awake()
	{
		GGSingleModeSupplyManager.mInstance = this;
		switch (PlayerPrefs.GetInt("SingleModeChapterOneDifficulty", 1))
		{
		case 1:
		{
			this.BloodPakNumBuyLimit = 1;
			this.BulletPakNumBuyLimit = 6;
			this.BloodPakPrices = new int[this.BloodPakNumBuyLimit];
			int[] bloodPakPrices = new int[]
			{
				300
			};
			this.BloodPakPrices = bloodPakPrices;
			this.BulletPakPrices = new int[this.BulletPakNumBuyLimit];
			int[] bulletPakPrices = new int[]
			{
				40,
				40,
				40,
				40,
				40,
				40
			};
			this.BulletPakPrices = bulletPakPrices;
			break;
		}
		case 2:
		{
			this.BloodPakNumBuyLimit = 2;
			this.BulletPakNumBuyLimit = 8;
			this.BloodPakPrices = new int[this.BloodPakNumBuyLimit];
			int[] bloodPakPrices2 = new int[]
			{
				300,
				300
			};
			this.BloodPakPrices = bloodPakPrices2;
			this.BulletPakPrices = new int[this.BulletPakNumBuyLimit];
			int[] bulletPakPrices2 = new int[]
			{
				40,
				40,
				40,
				40,
				40,
				40,
				40,
				40
			};
			this.BulletPakPrices = bulletPakPrices2;
			break;
		}
		case 3:
		{
			this.BloodPakNumBuyLimit = 3;
			this.BulletPakNumBuyLimit = 12;
			this.BloodPakPrices = new int[this.BloodPakNumBuyLimit];
			int[] bloodPakPrices3 = new int[]
			{
				300,
				300,
				300
			};
			this.BloodPakPrices = bloodPakPrices3;
			this.BulletPakPrices = new int[this.BulletPakNumBuyLimit];
			int[] bulletPakPrices3 = new int[]
			{
				40,
				40,
				40,
				40,
				40,
				40,
				40,
				40,
				40,
				40,
				40,
				40
			};
			this.BulletPakPrices = bulletPakPrices3;
			break;
		}
		}
		this.BloodPakNumBuyed = 0;
		this.BulletPakNumBuyed = 0;
		this.BloodPakNum = PlayerPrefs.GetInt("SingleModeChapterOneBloodPakNum", 0);
		this.BulletPakNum = PlayerPrefs.GetInt("SingleModeChapterOneBulletPakNum", 0);
		string loadedLevelName = Application.loadedLevelName;
		if (loadedLevelName != null)
		{
			if (!(loadedLevelName == "SingleMode_1"))
			{
				if (!(loadedLevelName == "SingleMode_2"))
				{
					if (!(loadedLevelName == "SingleMode_3"))
					{
						if (loadedLevelName == "SingleMode_4")
						{
							this.story = "Search the wandering pilot, start the helicopter \n\nwith his help. Be careful of jungle hunters.";
						}
					}
					else
					{
						this.story = "Search enough supplies storage in the bandit camp, \n\nacross the desert.";
					}
				}
				else
				{
					this.story = "Find the controlled crew, take control of the ship.";
				}
			}
			else
			{
				this.story = "Shake off the cops, and find way to escape the prison.";
			}
		}
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x000A2E30 File Offset: 0x000A1230
	private void OnDestroy()
	{
		GGSingleModeSupplyManager.mInstance = null;
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x000A2E38 File Offset: 0x000A1238
	private void Start()
	{
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x000A2E3A File Offset: 0x000A123A
	private void Update()
	{
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x000A2E3C File Offset: 0x000A123C
	public void BloodPakBuy()
	{
		PlayerPrefs.SetInt("SingleModeChapterOneBloodPakNum", PlayerPrefs.GetInt("SingleModeChapterOneBloodPakNum", 0) + 1);
		this.BloodPakNum = PlayerPrefs.GetInt("SingleModeChapterOneBloodPakNum", 0);
		this.BloodPakNumBuyed++;
		PlayerPrefs.SetInt("GameCoins", PlayerPrefs.GetInt("GameCoins") - 300);
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x000A2E9C File Offset: 0x000A129C
	public void BulletPakBuy()
	{
		PlayerPrefs.SetInt("SingleModeChapterOneBulletPakNum", PlayerPrefs.GetInt("SingleModeChapterOneBulletPakNum", 0) + 1);
		this.BulletPakNum = PlayerPrefs.GetInt("SingleModeChapterOneBulletPakNum", 0);
		this.BulletPakNumBuyed++;
		PlayerPrefs.SetInt("GameCoins", PlayerPrefs.GetInt("GameCoins") - 40);
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x000A2EF6 File Offset: 0x000A12F6
	public void BloodPakUse()
	{
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x000A2EF8 File Offset: 0x000A12F8
	public void BulletPakUse()
	{
		if (this.BulletPakNum > 0)
		{
			PlayerPrefs.SetInt("SingleModeChapterOneBulletPakNum", PlayerPrefs.GetInt("SingleModeChapterOneBulletPakNum", 0) - 1);
			this.BulletPakNum = PlayerPrefs.GetInt("SingleModeChapterOneBulletPakNum", 0);
			PlayerPrefs.SetInt("SingleModeAddBullet", 1);
		}
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x000A2F44 File Offset: 0x000A1344
	public void BloodPakGetInScene()
	{
		PlayerPrefs.SetInt("SingleModeChapterOneBloodPakNum", PlayerPrefs.GetInt("SingleModeChapterOneBloodPakNum", 0) + 1);
		this.BloodPakNum = PlayerPrefs.GetInt("SingleModeChapterOneBloodPakNum", 0);
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x000A2F6E File Offset: 0x000A136E
	public void BulletPakGetInScene()
	{
		PlayerPrefs.SetInt("SingleModeChapterOneBulletPakNum", PlayerPrefs.GetInt("SingleModeChapterOneBulletPakNum", 0) + 1);
		this.BulletPakNum = PlayerPrefs.GetInt("SingleModeChapterOneBulletPakNum", 0);
	}

	// Token: 0x040014CF RID: 5327
	public static GGSingleModeSupplyManager mInstance;

	// Token: 0x040014D0 RID: 5328
	public int BloodPakNum;

	// Token: 0x040014D1 RID: 5329
	public int BulletPakNum;

	// Token: 0x040014D2 RID: 5330
	public int BloodPakNumBuyLimit;

	// Token: 0x040014D3 RID: 5331
	public int BulletPakNumBuyLimit;

	// Token: 0x040014D4 RID: 5332
	public int BloodPakNumBuyed;

	// Token: 0x040014D5 RID: 5333
	public int BulletPakNumBuyed;

	// Token: 0x040014D6 RID: 5334
	public int[] BloodPakPrices;

	// Token: 0x040014D7 RID: 5335
	public int[] BulletPakPrices;

	// Token: 0x040014D8 RID: 5336
	public string story;
}
