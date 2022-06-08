using System;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class EveryPlayDirector : MonoBehaviour
{
	// Token: 0x06000D1A RID: 3354 RVA: 0x0006D1E7 File Offset: 0x0006B5E7
	private void Awake()
	{
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0006D1E9 File Offset: 0x0006B5E9
	private void OnDestroy()
	{
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x0006D1EB File Offset: 0x0006B5EB
	private void RecordingStartedDelegate()
	{
		this.show_obj.SetActive(true);
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x0006D1F9 File Offset: 0x0006B5F9
	private void RecordingStoppedDelegate()
	{
		this.show_obj.SetActive(false);
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x0006D207 File Offset: 0x0006B607
	public void StartRecoding()
	{
		Debug.Log("recording was started!");
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x0006D213 File Offset: 0x0006B613
	public void StopRecording()
	{
		Debug.Log("recoding was ended!");
	}

	// Token: 0x06000D20 RID: 3360 RVA: 0x0006D21F File Offset: 0x0006B61F
	public void PlayLastRecording()
	{
		Debug.Log("playback the previously record!");
	}

	// Token: 0x06000D21 RID: 3361 RVA: 0x0006D22B File Offset: 0x0006B62B
	public void ShowSharingModal()
	{
		Debug.Log("show sharing view");
	}

	// Token: 0x04000D31 RID: 3377
	public GameObject show_obj;
}
