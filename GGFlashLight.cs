using System;
using UnityEngine;

// Token: 0x02000215 RID: 533
public class GGFlashLight : MonoBehaviour
{
	// Token: 0x06000E73 RID: 3699 RVA: 0x00079776 File Offset: 0x00077B76
	private void Start()
	{
		if (this.turnOn)
		{
			this.flashLight.enabled = true;
		}
		else
		{
			this.flashLight.enabled = false;
		}
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x000797A0 File Offset: 0x00077BA0
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			this.turnOn = !this.turnOn;
			this.flashLightOnOff();
		}
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x000797C4 File Offset: 0x00077BC4
	private void flashLightOnOff()
	{
		base.GetComponent<AudioSource>().clip = this.OnOffAudio;
		base.GetComponent<AudioSource>().Play();
		if (this.turnOn)
		{
			this.flashLight.enabled = true;
		}
		else
		{
			this.flashLight.enabled = false;
		}
	}

	// Token: 0x04000FAE RID: 4014
	private bool turnOn;

	// Token: 0x04000FAF RID: 4015
	private Light flashLight;

	// Token: 0x04000FB0 RID: 4016
	private AudioClip OnOffAudio;
}
