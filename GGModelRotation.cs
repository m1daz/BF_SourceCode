using System;
using UnityEngine;

// Token: 0x02000281 RID: 641
public class GGModelRotation : MonoBehaviour
{
	// Token: 0x0600123A RID: 4666 RVA: 0x000A44C9 File Offset: 0x000A28C9
	private void Start()
	{
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x000A44CB File Offset: 0x000A28CB
	private void Update()
	{
		base.transform.Rotate(0f, 0f, 60f * Time.deltaTime);
	}
}
