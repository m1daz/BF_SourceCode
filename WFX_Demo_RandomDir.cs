using System;
using UnityEngine;

// Token: 0x0200052E RID: 1326
public class WFX_Demo_RandomDir : MonoBehaviour
{
	// Token: 0x060025AD RID: 9645 RVA: 0x001181C4 File Offset: 0x001165C4
	private void Awake()
	{
		base.transform.eulerAngles = new Vector3(UnityEngine.Random.Range(this.min.x, this.max.x), UnityEngine.Random.Range(this.min.y, this.max.y), UnityEngine.Random.Range(this.min.z, this.max.z));
	}

	// Token: 0x0400264A RID: 9802
	public Vector3 min = new Vector3(0f, 0f, 0f);

	// Token: 0x0400264B RID: 9803
	public Vector3 max = new Vector3(0f, 360f, 0f);
}
