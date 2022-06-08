using System;
using UnityEngine;

// Token: 0x02000278 RID: 632
public class GGSingleModeThreeBombGenerateManager : MonoBehaviour
{
	// Token: 0x060011F5 RID: 4597 RVA: 0x000A2FA0 File Offset: 0x000A13A0
	private void Start()
	{
		this.bombPakPositionNum = 20;
		this.bombPakGeneratePositions = new GameObject[this.bombPakPositionNum];
		for (int i = 0; i < this.bombPakPositionNum; i++)
		{
			this.bombPakGeneratePositions[i] = base.transform.Find("bombGeneratePosition" + (i + 1).ToString()).gameObject;
		}
	}

	// Token: 0x060011F6 RID: 4598 RVA: 0x000A3010 File Offset: 0x000A1410
	private void Update()
	{
		if (GGSingleModePauseControl.mInstance.PauseState)
		{
			return;
		}
		if (Time.frameCount % 400 == 0)
		{
			int num = UnityEngine.Random.Range(0, this.bombPakPositionNum);
			UnityEngine.Object.Instantiate<GameObject>(this.bombPak, this.bombPakGeneratePositions[num].transform.position, this.bombPakGeneratePositions[num].transform.rotation);
		}
	}

	// Token: 0x040014D9 RID: 5337
	public GameObject bombPak;

	// Token: 0x040014DA RID: 5338
	private int bombPakPositionNum;

	// Token: 0x040014DB RID: 5339
	private GameObject[] bombPakGeneratePositions;
}
