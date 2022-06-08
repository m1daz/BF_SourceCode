using System;
using UnityEngine;

// Token: 0x020005DA RID: 1498
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Snapshot Point")]
public class UISnapshotPoint : MonoBehaviour
{
	// Token: 0x06002AA1 RID: 10913 RVA: 0x0013E9BF File Offset: 0x0013CDBF
	private void Start()
	{
		if (base.tag != "EditorOnly")
		{
			base.tag = "EditorOnly";
		}
	}

	// Token: 0x04002A79 RID: 10873
	public bool isOrthographic = true;

	// Token: 0x04002A7A RID: 10874
	public float nearClip = -100f;

	// Token: 0x04002A7B RID: 10875
	public float farClip = 100f;

	// Token: 0x04002A7C RID: 10876
	[Range(10f, 80f)]
	public int fieldOfView = 35;

	// Token: 0x04002A7D RID: 10877
	public float orthoSize = 30f;

	// Token: 0x04002A7E RID: 10878
	public Texture2D thumbnail;
}
