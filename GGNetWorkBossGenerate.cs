using System;
using UnityEngine;

// Token: 0x020004E1 RID: 1249
public class GGNetWorkBossGenerate : MonoBehaviour
{
	// Token: 0x060022C6 RID: 8902 RVA: 0x0010396F File Offset: 0x00101D6F
	private void Awake()
	{
		GGNetWorkBossGenerate.mInstance = this;
	}

	// Token: 0x060022C7 RID: 8903 RVA: 0x00103977 File Offset: 0x00101D77
	private void Start()
	{
	}

	// Token: 0x060022C8 RID: 8904 RVA: 0x00103979 File Offset: 0x00101D79
	private void Update()
	{
	}

	// Token: 0x060022C9 RID: 8905 RVA: 0x0010397C File Offset: 0x00101D7C
	public void BossGenerate(int SenceId, int RoundId)
	{
		if (SenceId == 1)
		{
			if (RoundId < 4)
			{
				string objectName = "HuntingModeBoss" + SenceId.ToString() + "_" + RoundId.ToString();
				GGNetworkKit.mInstance.CreateSeceneObject(objectName, new Vector3(0f, 0.9f, 0f), Quaternion.identity);
			}
			else if (RoundId == 4)
			{
				string objectName2 = string.Concat(new string[]
				{
					"HuntingModeBoss",
					SenceId.ToString(),
					"_",
					RoundId.ToString(),
					"_L"
				});
				GGNetworkKit.mInstance.CreateSeceneObject(objectName2, new Vector3(0f, 0.9f, -26.6f), Quaternion.identity);
				string objectName3 = string.Concat(new string[]
				{
					"HuntingModeBoss",
					SenceId.ToString(),
					"_",
					RoundId.ToString(),
					"_R"
				});
				GGNetworkKit.mInstance.CreateSeceneObject(objectName3, new Vector3(0f, 0.9f, 23f), Quaternion.identity);
			}
		}
	}

	// Token: 0x060022CA RID: 8906 RVA: 0x00103AC1 File Offset: 0x00101EC1
	private void OnDestroy()
	{
		GGNetWorkBossGenerate.mInstance = null;
	}

	// Token: 0x0400238F RID: 9103
	public static GGNetWorkBossGenerate mInstance;
}
