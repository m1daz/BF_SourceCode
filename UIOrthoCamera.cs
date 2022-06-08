using System;
using UnityEngine;

// Token: 0x0200062A RID: 1578
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("NGUI/UI/Orthographic Camera")]
public class UIOrthoCamera : MonoBehaviour
{
	// Token: 0x06002D81 RID: 11649 RVA: 0x0014D32E File Offset: 0x0014B72E
	private void Start()
	{
		this.mCam = base.GetComponent<Camera>();
		this.mTrans = base.transform;
		this.mCam.orthographic = true;
	}

	// Token: 0x06002D82 RID: 11650 RVA: 0x0014D354 File Offset: 0x0014B754
	private void Update()
	{
		float num = this.mCam.rect.yMin * (float)Screen.height;
		float num2 = this.mCam.rect.yMax * (float)Screen.height;
		float num3 = (num2 - num) * 0.5f * this.mTrans.lossyScale.y;
		if (!Mathf.Approximately(this.mCam.orthographicSize, num3))
		{
			this.mCam.orthographicSize = num3;
		}
	}

	// Token: 0x04002CA7 RID: 11431
	private Camera mCam;

	// Token: 0x04002CA8 RID: 11432
	private Transform mTrans;
}
