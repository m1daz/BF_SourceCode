using System;
using UnityEngine;

// Token: 0x0200052D RID: 1325
public class WFX_Demo_DeleteAfterDelay : MonoBehaviour
{
	// Token: 0x060025AB RID: 9643 RVA: 0x00118158 File Offset: 0x00116558
	private void Update()
	{
		this.delay -= Time.deltaTime;
		if (this.delay < 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002649 RID: 9801
	public float delay = 1f;
}
