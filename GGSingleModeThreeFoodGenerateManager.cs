using System;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class GGSingleModeThreeFoodGenerateManager : MonoBehaviour
{
	// Token: 0x060011F8 RID: 4600 RVA: 0x000A3084 File Offset: 0x000A1484
	private void Start()
	{
		this.foodPakNum = 5;
		this.foodPakPositionNum = 8;
		this.foodPakGeneratePositions = new GameObject[this.foodPakPositionNum];
		for (int i = 0; i < this.foodPakPositionNum; i++)
		{
			this.foodPakGeneratePositions[i] = base.transform.Find("foodPakGeneratePosition" + (i + 1).ToString()).gameObject;
		}
		for (int j = 0; j < this.foodPakNum; j++)
		{
			int num = UnityEngine.Random.Range(0, this.foodPakPositionNum);
			UnityEngine.Object.Instantiate<GameObject>(this.foodPak, this.foodPakGeneratePositions[num].transform.position, this.foodPakGeneratePositions[num].transform.rotation);
		}
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x000A314E File Offset: 0x000A154E
	private void Update()
	{
	}

	// Token: 0x040014DC RID: 5340
	public GameObject foodPak;

	// Token: 0x040014DD RID: 5341
	private int foodPakNum;

	// Token: 0x040014DE RID: 5342
	private int foodPakPositionNum;

	// Token: 0x040014DF RID: 5343
	private GameObject[] foodPakGeneratePositions;
}
