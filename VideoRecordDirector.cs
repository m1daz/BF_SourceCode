using System;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class VideoRecordDirector : MonoBehaviour
{
	// Token: 0x06000D23 RID: 3363 RVA: 0x0006D23F File Offset: 0x0006B63F
	private void Awake()
	{
		VideoRecordDirector.mInstance = this;
		if (VideoRecordDirector.GameProtecterRef == null)
		{
			VideoRecordDirector.GameProtecterRef = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.RegisterCallbacks();
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}

	// Token: 0x06000D24 RID: 3364 RVA: 0x0006D27E File Offset: 0x0006B67E
	private void Start()
	{
	}

	// Token: 0x06000D25 RID: 3365 RVA: 0x0006D280 File Offset: 0x0006B680
	private void RegisterCallbacks()
	{
	}

	// Token: 0x06000D26 RID: 3366 RVA: 0x0006D282 File Offset: 0x0006B682
	private void RecordingStartedDelegate()
	{
		Debug.Log("start record");
	}

	// Token: 0x06000D27 RID: 3367 RVA: 0x0006D28E File Offset: 0x0006B68E
	private void RecordingStoppedDelegate()
	{
		Debug.Log("stop record");
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x0006D29A File Offset: 0x0006B69A
	private void OnDestroy()
	{
		this.UnregisterCallbacks();
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0006D2A2 File Offset: 0x0006B6A2
	private void UnregisterCallbacks()
	{
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x0006D2A4 File Offset: 0x0006B6A4
	public static bool IsEnabled()
	{
		return false;
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0006D2A7 File Offset: 0x0006B6A7
	public static bool IsRecording()
	{
		return false;
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0006D2AA File Offset: 0x0006B6AA
	public static void StartRecording()
	{
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0006D2AC File Offset: 0x0006B6AC
	public static void StopRecording()
	{
		VideoRecordDirector.hasRecordedInThisLaunch = true;
		VideoRecordDirector.isNewVideoExistInList = true;
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0006D2BA File Offset: 0x0006B6BA
	public static void ShowVideoRecordInfo()
	{
		if (VideoRecordDirector.isNewVideoExistInList)
		{
			VideoRecordDirector.PlayLastRecording();
		}
		else
		{
			VideoRecordDirector.Show();
		}
		VideoRecordDirector.isNewVideoExistInList = false;
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0006D2DB File Offset: 0x0006B6DB
	public static void PlayLastRecording()
	{
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0006D2DD File Offset: 0x0006B6DD
	public static void Show()
	{
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0006D2DF File Offset: 0x0006B6DF
	private void UploadDidCompleteDelegate(int videoID)
	{
		GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tDailyVideoShare, GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tDailyVideoShare) + 1);
		if (UIRewardNewDirector.mInstance != null)
		{
			UIRewardNewDirector.mInstance.RefershDailyRewardScrollVeiw();
		}
	}

	// Token: 0x04000D32 RID: 3378
	public static VideoRecordDirector mInstance;

	// Token: 0x04000D33 RID: 3379
	private static VideoRecordDirector GameProtecterRef;

	// Token: 0x04000D34 RID: 3380
	public static bool isNewVideoExistInList;

	// Token: 0x04000D35 RID: 3381
	public static bool hasRecordedInThisLaunch;
}
