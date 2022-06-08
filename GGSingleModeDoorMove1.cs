using System;
using UnityEngine;

// Token: 0x0200026E RID: 622
public class GGSingleModeDoorMove1 : MonoBehaviour
{
	// Token: 0x060011B8 RID: 4536 RVA: 0x000A1BB8 File Offset: 0x0009FFB8
	private void Start()
	{
		this.originHeight = base.transform.position.x;
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x000A1BE0 File Offset: 0x0009FFE0
	private void Update()
	{
		if (GGSingleModePauseControl.mInstance.PauseState)
		{
			return;
		}
		this.doorOpenTime += Time.deltaTime;
		if (this.doorOpenTime > 20f && this.doorOpenTime <= 23f)
		{
			this.doorOpen = true;
		}
		if (this.doorOpenTime > 23f)
		{
			this.doorOpen = false;
			this.doorOpenTime = 0f;
		}
		if (this.doorOpen)
		{
			base.transform.position += new Vector3(0.05f, 0f, 0f);
		}
		if (!this.doorOpen && base.transform.position.x > this.originHeight)
		{
			base.transform.position -= new Vector3(0.05f, 0f, 0f);
		}
	}

	// Token: 0x04001499 RID: 5273
	private float doorOpenTime;

	// Token: 0x0400149A RID: 5274
	private bool doorOpen;

	// Token: 0x0400149B RID: 5275
	private float originHeight;
}
