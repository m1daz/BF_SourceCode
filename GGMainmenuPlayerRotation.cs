using System;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class GGMainmenuPlayerRotation : MonoBehaviour
{
	// Token: 0x06001058 RID: 4184 RVA: 0x0008BD17 File Offset: 0x0008A117
	private void Start()
	{
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x0008BD1C File Offset: 0x0008A11C
	private void Update()
	{
		this.curTime += Time.deltaTime;
		if (this.curTime >= this.rotationtime && this.curTime < this.rotationtime * 2f)
		{
			if (this.i == 0)
			{
				base.transform.localEulerAngles = Vector3.Lerp(base.transform.localEulerAngles, this.end1.localEulerAngles, 2f * Time.deltaTime);
			}
			else if (this.i == 1)
			{
				base.transform.localEulerAngles = Vector3.Lerp(base.transform.localEulerAngles, this.end2.localEulerAngles, 2f * Time.deltaTime);
			}
			else if (this.i == 2)
			{
				base.transform.localEulerAngles = Vector3.Lerp(base.transform.localEulerAngles, this.end3.localEulerAngles, 2f * Time.deltaTime);
			}
		}
		else if (this.curTime >= this.rotationtime * 2f)
		{
			this.curTime = 0f;
			this.rotationtime = UnityEngine.Random.Range(4f, 8f);
			this.i = UnityEngine.Random.Range(0, 3);
		}
	}

	// Token: 0x04001275 RID: 4725
	private float rotationtime = 6f;

	// Token: 0x04001276 RID: 4726
	private float curTime;

	// Token: 0x04001277 RID: 4727
	public Transform end1;

	// Token: 0x04001278 RID: 4728
	public Transform end2;

	// Token: 0x04001279 RID: 4729
	public Transform end3;

	// Token: 0x0400127A RID: 4730
	private int i;
}
