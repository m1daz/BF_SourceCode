using System;
using UnityEngine;

// Token: 0x020006A9 RID: 1705
public class Shellcasing : MonoBehaviour
{
	// Token: 0x06003231 RID: 12849 RVA: 0x00163208 File Offset: 0x00161608
	private void Start()
	{
		base.Invoke("Kill", this.delayTime);
	}

	// Token: 0x06003232 RID: 12850 RVA: 0x0016321B File Offset: 0x0016161B
	private void Kill()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x04002EC0 RID: 11968
	public float delayTime = 0.05f;
}
