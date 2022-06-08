using System;
using UnityEngine;

// Token: 0x02000250 RID: 592
public class GGNetworkWeaponpart : MonoBehaviour
{
	// Token: 0x06001119 RID: 4377 RVA: 0x00097D81 File Offset: 0x00096181
	private void Start()
	{
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x00097D83 File Offset: 0x00096183
	private void Update()
	{
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x00097D85 File Offset: 0x00096185
	public void WeaponPartShow(bool b)
	{
		base.GetComponent<Renderer>().enabled = b;
	}
}
