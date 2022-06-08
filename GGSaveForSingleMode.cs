using System;
using UnityEngine;

// Token: 0x0200025E RID: 606
public class GGSaveForSingleMode : MonoBehaviour
{
	// Token: 0x06001176 RID: 4470 RVA: 0x0009BB61 File Offset: 0x00099F61
	private void Awake()
	{
		GGSaveForSingleMode.mInstance = this;
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x0009BB69 File Offset: 0x00099F69
	private void OnDestroy()
	{
		GGSaveForSingleMode.mInstance = null;
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x0009BB71 File Offset: 0x00099F71
	private void Update()
	{
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x0009BB74 File Offset: 0x00099F74
	public void Save()
	{
		PlayerPrefs.SetInt("SingleModeChapterOneSavedApply", 1);
		PlayerPrefs.SetInt("SingleModeChapterOneBulletAndBloodPakStoreShow", 1);
		PlayerPrefs.SetInt("SingleModeChapterOneDifficultySaved", GGSingleEnemyManager.mInstance.Difficulty);
		PlayerPrefs.SetInt("SingleModeChapterOneBloodPakNumSaved", GGSingleModeSupplyManager.mInstance.BloodPakNum);
		PlayerPrefs.SetInt("SingleModeChapterOneBulletPakNumSaved", GGSingleModeSupplyManager.mInstance.BulletPakNum);
		PlayerPrefs.SetString("SingleModeChapterOneBarrierLvSaved", Application.loadedLevelName);
		PlayerPrefs.SetInt("SingleModeChapterOneCurrentPointSaved", PlayerPrefs.GetInt("SingleModeChapterOneCurrentPoint", 0));
		PlayerPrefs.SetInt("SingleModeRespawnLimitSaved", PlayerPrefs.GetInt("SingleModeRespawnLimit", 5));
	}

	// Token: 0x0400140D RID: 5133
	public static GGSaveForSingleMode mInstance;
}
