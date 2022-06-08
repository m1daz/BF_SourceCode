using System;
using UnityEngine;

// Token: 0x0200026D RID: 621
public class GGSingleModeDoorMove : MonoBehaviour
{
	// Token: 0x060011B5 RID: 4533 RVA: 0x000A1A88 File Offset: 0x0009FE88
	private void Start()
	{
		this.originHeight = base.transform.position.z;
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x000A1AB0 File Offset: 0x0009FEB0
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
			base.transform.position += new Vector3(0f, 0f, 0.05f);
		}
		if (!this.doorOpen && base.transform.position.z > this.originHeight)
		{
			base.transform.position -= new Vector3(0f, 0f, 0.05f);
		}
	}

	// Token: 0x04001496 RID: 5270
	private float doorOpenTime;

	// Token: 0x04001497 RID: 5271
	private bool doorOpen;

	// Token: 0x04001498 RID: 5272
	private float originHeight;
}
