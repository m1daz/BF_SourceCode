using System;
using UnityEngine;

// Token: 0x02000314 RID: 788
public class ActRotatorExample : MonoBehaviour
{
	// Token: 0x0600187D RID: 6269 RVA: 0x000CC3D2 File Offset: 0x000CA7D2
	private void Update()
	{
		base.transform.Rotate(this.speed * Time.deltaTime, this.speed * Time.deltaTime, this.speed * Time.deltaTime);
	}

	// Token: 0x04001B9F RID: 7071
	[Range(1f, 100f)]
	public float speed = 5f;
}
