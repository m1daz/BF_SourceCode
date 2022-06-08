using System;
using UnityEngine;

// Token: 0x02000287 RID: 647
public class IphoneXUIAdaptCenterNode : MonoBehaviour
{
	// Token: 0x0600124F RID: 4687 RVA: 0x000A481B File Offset: 0x000A2C1B
	private void Start()
	{
		if (Screen.width == 2436)
		{
			base.gameObject.transform.localScale = new Vector3(this.scaleSizeX, this.scaleSizeY, 1f);
		}
	}

	// Token: 0x04001516 RID: 5398
	public float scaleSizeX = 1f;

	// Token: 0x04001517 RID: 5399
	public float scaleSizeY = 1f;
}
