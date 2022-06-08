using System;
using UnityEngine;

// Token: 0x02000220 RID: 544
public class GGWaitForDestroy : MonoBehaviour
{
	// Token: 0x06000E9F RID: 3743 RVA: 0x0007A4E9 File Offset: 0x000788E9
	private void Awake()
	{
		UnityEngine.Object.Destroy(base.gameObject, this.lifeTime);
	}

	// Token: 0x04000FF8 RID: 4088
	public float lifeTime = 2f;
}
