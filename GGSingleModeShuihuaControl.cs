using System;
using UnityEngine;

// Token: 0x02000276 RID: 630
public class GGSingleModeShuihuaControl : MonoBehaviour
{
	// Token: 0x060011E7 RID: 4583 RVA: 0x000A2AB8 File Offset: 0x000A0EB8
	private void Start()
	{
		this.originHeight = base.transform.position.y;
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x000A2AE0 File Offset: 0x000A0EE0
	private void Update()
	{
		if (GGSingleModePauseControl.mInstance.PauseState)
		{
			return;
		}
		this.shuihuaTime += Time.deltaTime;
		if (this.shuihuaTime > 10f && this.shuihuaTime <= 12f)
		{
			this.shuihuaDown = true;
		}
		if (this.shuihuaTime > 12f)
		{
			this.shuihuaDown = false;
			this.shuihuaTime = 0f;
		}
		if (this.shuihuaDown)
		{
			base.transform.position -= new Vector3(0f, 0.3f, 0f);
		}
		if (!this.shuihuaDown && base.transform.position.y < this.originHeight)
		{
			base.transform.position += new Vector3(0f, 0.3f, 0f);
		}
	}

	// Token: 0x040014CC RID: 5324
	private float shuihuaTime;

	// Token: 0x040014CD RID: 5325
	private bool shuihuaDown;

	// Token: 0x040014CE RID: 5326
	private float originHeight;
}
