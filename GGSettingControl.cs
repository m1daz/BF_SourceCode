using System;
using UnityEngine;

// Token: 0x020001EE RID: 494
public class GGSettingControl : MonoBehaviour
{
	// Token: 0x06000DAD RID: 3501 RVA: 0x00071237 File Offset: 0x0006F637
	private void Start()
	{
		GGSettingControl.mInstance = this;
		this.mGGSliderotate = base.GetComponent<GGSliderotate>();
		this.InitSetting();
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x00071251 File Offset: 0x0006F651
	private void Update()
	{
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x00071253 File Offset: 0x0006F653
	private void OnDestroy()
	{
		if (GGSettingControl.mInstance != null)
		{
			GGSettingControl.mInstance = null;
		}
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x0007126B File Offset: 0x0006F66B
	private void InitSetting()
	{
		this.SetSensitivity();
		this.SetSound();
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x0007127C File Offset: 0x0006F67C
	public void SetSensitivity()
	{
		if (this.mGGSliderotate != null)
		{
			this.mGGSliderotate.sensitivityX = 0.05f * (float)UIUserDataController.GetSensitivity() + 1f;
			this.mGGSliderotate.sensitivityY = 0.05f * (float)UIUserDataController.GetSensitivity() + 1f;
		}
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x000712D4 File Offset: 0x0006F6D4
	public void SetSound()
	{
		if (PlayerPrefs.GetInt("Setting_SoundFX", 1) == 0)
		{
			AudioListener.volume = 0f;
		}
		else if (PlayerPrefs.GetInt("Setting_SoundFX", 1) == 1)
		{
			AudioListener.volume = 1f;
		}
	}

	// Token: 0x04000DEF RID: 3567
	public static GGSettingControl mInstance;

	// Token: 0x04000DF0 RID: 3568
	private GGSliderotate mGGSliderotate;
}
