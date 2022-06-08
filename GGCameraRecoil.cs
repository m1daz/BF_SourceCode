using System;
using UnityEngine;

// Token: 0x02000213 RID: 531
public class GGCameraRecoil : MonoBehaviour
{
	// Token: 0x06000E6C RID: 3692 RVA: 0x00079581 File Offset: 0x00077981
	private void Start()
	{
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x00079583 File Offset: 0x00077983
	private void Update()
	{
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x00079585 File Offset: 0x00077985
	public void CameraRecoil(float RecoilPower)
	{
		base.transform.localEulerAngles -= new Vector3(RecoilPower, 0f, 0f);
	}
}
