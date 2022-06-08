using System;
using UnityEngine;

// Token: 0x02000274 RID: 628
public class GGSingleModePauseControl : MonoBehaviour
{
	// Token: 0x060011D7 RID: 4567 RVA: 0x000A2819 File Offset: 0x000A0C19
	private void OnDestroy()
	{
		GGSingleModePauseControl.mInstance = null;
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x000A2821 File Offset: 0x000A0C21
	private void Awake()
	{
		GGSingleModePauseControl.mInstance = this;
		this.PauseState = false;
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x000A2830 File Offset: 0x000A0C30
	private void OnDisable()
	{
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x000A2832 File Offset: 0x000A0C32
	private void OnGamePaused()
	{
		this.PauseState = true;
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x000A283B File Offset: 0x000A0C3B
	private void OnGameResume()
	{
		this.PauseState = false;
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x000A2844 File Offset: 0x000A0C44
	private void OnSaveNQuit()
	{
		GGSaveForSingleMode.mInstance.Save();
		Application.LoadLevel("MainMenu");
	}

	// Token: 0x040014C3 RID: 5315
	public static GGSingleModePauseControl mInstance;

	// Token: 0x040014C4 RID: 5316
	public bool PauseState;
}
