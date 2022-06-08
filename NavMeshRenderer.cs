using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
[ExecuteInEditMode]
public class NavMeshRenderer : MonoBehaviour
{
	// Token: 0x0600043F RID: 1087 RVA: 0x00026AA4 File Offset: 0x00024EA4
	public string SomeFunction()
	{
		return this.lastLevel;
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00026AAC File Offset: 0x00024EAC
	private void Update()
	{
	}

	// Token: 0x04000359 RID: 857
	private string lastLevel = string.Empty;
}
