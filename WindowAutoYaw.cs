using System;
using UnityEngine;

// Token: 0x02000557 RID: 1367
[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
	// Token: 0x06002640 RID: 9792 RVA: 0x0011BC7A File Offset: 0x0011A07A
	private void OnDisable()
	{
		this.mTrans.localRotation = Quaternion.identity;
	}

	// Token: 0x06002641 RID: 9793 RVA: 0x0011BC8C File Offset: 0x0011A08C
	private void OnEnable()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mTrans = base.transform;
	}

	// Token: 0x06002642 RID: 9794 RVA: 0x0011BCC4 File Offset: 0x0011A0C4
	private void Update()
	{
		if (this.uiCamera != null)
		{
			Vector3 vector = this.uiCamera.WorldToViewportPoint(this.mTrans.position);
			this.mTrans.localRotation = Quaternion.Euler(0f, (vector.x * 2f - 1f) * this.yawAmount, 0f);
		}
	}

	// Token: 0x040026FA RID: 9978
	public int updateOrder;

	// Token: 0x040026FB RID: 9979
	public Camera uiCamera;

	// Token: 0x040026FC RID: 9980
	public float yawAmount = 20f;

	// Token: 0x040026FD RID: 9981
	private Transform mTrans;
}
