using System;
using UnityEngine;

// Token: 0x0200021B RID: 539
public class GGLightningBeamUVScroller : MonoBehaviour
{
	// Token: 0x06000E8A RID: 3722 RVA: 0x0007A02A File Offset: 0x0007842A
	private void Start()
	{
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x0007A02C File Offset: 0x0007842C
	private void Update()
	{
		base.GetComponent<Renderer>().materials[this.matNumber].mainTextureOffset += new Vector2(this.velocityX * Time.deltaTime, this.velocityY * Time.deltaTime);
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x0007A078 File Offset: 0x00078478
	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x0007A081 File Offset: 0x00078481
	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	// Token: 0x04000FD5 RID: 4053
	public float velocityY;

	// Token: 0x04000FD6 RID: 4054
	public float velocityX = -3f;

	// Token: 0x04000FD7 RID: 4055
	public int matNumber;
}
