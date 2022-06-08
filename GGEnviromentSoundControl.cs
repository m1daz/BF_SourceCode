using System;
using UnityEngine;

// Token: 0x0200027F RID: 639
public class GGEnviromentSoundControl : MonoBehaviour
{
	// Token: 0x06001234 RID: 4660 RVA: 0x000A442E File Offset: 0x000A282E
	private void Start()
	{
		this.EnviromentSound = base.GetComponent<AudioSource>();
		this.EnviromentSound.loop = this.isloop;
		if (this.isloop)
		{
			this.EnviromentSound.Play();
		}
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x000A4464 File Offset: 0x000A2864
	private void Update()
	{
		if (!this.isloop)
		{
			this.looptime += Time.deltaTime;
			if (this.looptime > this.timeCount)
			{
				this.EnviromentSound.Play();
				this.looptime = 0f;
			}
		}
	}

	// Token: 0x04001502 RID: 5378
	private float looptime;

	// Token: 0x04001503 RID: 5379
	public float timeCount = 30f;

	// Token: 0x04001504 RID: 5380
	private AudioSource EnviromentSound;

	// Token: 0x04001505 RID: 5381
	public bool isloop;
}
