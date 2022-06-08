using System;
using UnityEngine;

// Token: 0x02000216 RID: 534
public class GGGearAnimation : MonoBehaviour
{
	// Token: 0x06000E77 RID: 3703 RVA: 0x00079828 File Offset: 0x00077C28
	private void Awake()
	{
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x0007982C File Offset: 0x00077C2C
	private void GearUse()
	{
		base.GetComponent<Animation>().Rewind(this.Use);
		base.GetComponent<Animation>()[this.Use].speed = base.GetComponent<Animation>()[this.Use].clip.length / 3f;
		base.GetComponent<Animation>().Play(this.Use);
	}

	// Token: 0x04000FB1 RID: 4017
	public string Use = "Use";
}
