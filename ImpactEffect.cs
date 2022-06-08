using System;
using UnityEngine;

// Token: 0x020003EE RID: 1006
public class ImpactEffect : MonoBehaviour
{
	// Token: 0x06001E38 RID: 7736 RVA: 0x000E7DB2 File Offset: 0x000E61B2
	private void Start()
	{
		this.ps = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x000E7DC0 File Offset: 0x000E61C0
	private void Update()
	{
		if (!this.ps.IsAlive())
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04001F5C RID: 8028
	private ParticleSystem ps;
}
