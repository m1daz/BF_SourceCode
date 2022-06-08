using System;
using UnityEngine;

// Token: 0x02000282 RID: 642
public class GGSequenceFrameAnimation : MonoBehaviour
{
	// Token: 0x0600123D RID: 4669 RVA: 0x000A450B File Offset: 0x000A290B
	private void Start()
	{
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x000A450D File Offset: 0x000A290D
	private void Update()
	{
		this.DrawTextureAnimation();
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x000A4518 File Offset: 0x000A2918
	private void DrawTextureAnimation()
	{
		this.m_fTime_t += Time.deltaTime;
		if (this.m_fTime_t > this.m_fFps_t)
		{
			this.m_fTime_t = 0f;
			this.open = true;
		}
		if (this.m_texs != null && this.open)
		{
			this.m_fTime += Time.deltaTime;
			float num = 1f / this.m_fFps;
			if (this.m_fTime >= num)
			{
				this.m_fTime = 0f;
				this.T.mainTexture = this.m_texs[this.m_iCurFram];
				this.m_iCurFram++;
				if (this.m_iCurFram >= this.m_texs.Length)
				{
					this.m_iCurFram = 0;
					this.open = false;
					if (this.m_fFps_t > 0f)
					{
						base.Invoke("SetTextureNull", num);
					}
				}
			}
		}
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x000A4609 File Offset: 0x000A2A09
	private void SetTextureNull()
	{
		this.T.mainTexture = null;
		this.T.enabled = false;
	}

	// Token: 0x04001506 RID: 5382
	public Texture2D[] m_texs;

	// Token: 0x04001507 RID: 5383
	private int m_iCurFram;

	// Token: 0x04001508 RID: 5384
	private int m_iCurAnimation;

	// Token: 0x04001509 RID: 5385
	private float m_fTime;

	// Token: 0x0400150A RID: 5386
	private float m_fTime_t;

	// Token: 0x0400150B RID: 5387
	public float m_fFps = 4f;

	// Token: 0x0400150C RID: 5388
	public float m_fFps_t = 3f;

	// Token: 0x0400150D RID: 5389
	public bool open;

	// Token: 0x0400150E RID: 5390
	public UITexture T;
}
