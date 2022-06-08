using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000530 RID: 1328
[RequireComponent(typeof(ParticleSystem))]
public class WFX_AutoDestructShuriken : MonoBehaviour
{
	// Token: 0x060025B1 RID: 9649 RVA: 0x001182C4 File Offset: 0x001166C4
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, this.lifeTime);
	}

	// Token: 0x060025B2 RID: 9650 RVA: 0x001182D8 File Offset: 0x001166D8
	private IEnumerator CheckIfAlive()
	{
		yield return new WaitForSeconds(2f);
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x0400264D RID: 9805
	public bool OnlyDeactivate;

	// Token: 0x0400264E RID: 9806
	public float lifeTime = 2f;
}
