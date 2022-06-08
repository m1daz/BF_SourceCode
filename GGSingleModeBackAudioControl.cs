using System;
using UnityEngine;

// Token: 0x02000269 RID: 617
public class GGSingleModeBackAudioControl : MonoBehaviour
{
	// Token: 0x060011A6 RID: 4518 RVA: 0x000A168C File Offset: 0x0009FA8C
	private void Awake()
	{
		this.backAudioSource = base.GetComponent<AudioSource>();
		string loadedLevelName = Application.loadedLevelName;
		if (loadedLevelName != null)
		{
			if (!(loadedLevelName == "SingleMode_1"))
			{
				if (!(loadedLevelName == "SingleMode_2"))
				{
					if (!(loadedLevelName == "SingleMode_3"))
					{
						if (loadedLevelName == "SingleMode_4")
						{
							this.backAudioSource.clip = this.backclips[3];
						}
					}
					else
					{
						this.backAudioSource.clip = this.backclips[2];
					}
				}
				else
				{
					this.backAudioSource.clip = this.backclips[1];
				}
			}
			else
			{
				this.backAudioSource.clip = this.backclips[0];
			}
		}
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x000A175B File Offset: 0x0009FB5B
	private void Start()
	{
		this.backAudioSource.loop = true;
		this.backAudioSource.Play();
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x000A1774 File Offset: 0x0009FB74
	private void Update()
	{
	}

	// Token: 0x0400148C RID: 5260
	public AudioClip[] backclips;

	// Token: 0x0400148D RID: 5261
	private AudioSource backAudioSource;
}
