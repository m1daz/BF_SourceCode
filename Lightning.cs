using System;
using UnityEngine;

// Token: 0x020006C8 RID: 1736
public class Lightning : MonoBehaviour
{
	// Token: 0x06003306 RID: 13062 RVA: 0x001669E1 File Offset: 0x00164DE1
	private void Update()
	{
		base.GetComponent<Renderer>().material.mainTextureOffset = this.scrollRate * Time.time;
	}

	// Token: 0x04002F61 RID: 12129
	public Vector2 scrollRate = new Vector2(1f, 1f);
}
