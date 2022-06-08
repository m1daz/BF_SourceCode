using System;
using UnityEngine;

// Token: 0x02000145 RID: 325
public class OnStartDelete : MonoBehaviour
{
	// Token: 0x060009A0 RID: 2464 RVA: 0x00048D40 File Offset: 0x00047140
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
