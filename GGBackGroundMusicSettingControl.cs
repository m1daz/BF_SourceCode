using System;
using UnityEngine;

// Token: 0x020001E6 RID: 486
public class GGBackGroundMusicSettingControl : MonoBehaviour
{
	// Token: 0x06000D84 RID: 3460 RVA: 0x000702BB File Offset: 0x0006E6BB
	private void Start()
	{
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x000702BD File Offset: 0x0006E6BD
	private void Update()
	{
		if (this.preSetting != PlayerPrefs.GetInt("Setting_Music", 1))
		{
			this.SetMusic();
			this.preSetting = PlayerPrefs.GetInt("Setting_Music", 1);
		}
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x000702EC File Offset: 0x0006E6EC
	public void SetMusic()
	{
		if (PlayerPrefs.GetInt("Setting_Music", 1) == 0)
		{
			if (base.GetComponent<AudioSource>() != null)
			{
				base.GetComponent<AudioSource>().Stop();
			}
		}
		else if (PlayerPrefs.GetInt("Setting_Music", 1) == 1 && base.GetComponent<AudioSource>() != null)
		{
			base.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x04000DAD RID: 3501
	private int preSetting;
}
