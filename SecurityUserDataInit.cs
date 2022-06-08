using System;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class SecurityUserDataInit : MonoBehaviour
{
	// Token: 0x06000C71 RID: 3185 RVA: 0x0005DB31 File Offset: 0x0005BF31
	private void Start()
	{
		GOGPlayerPrefabs.SetSecretKey("RsQgWX" + SystemInfo.deviceUniqueIdentifier);
	}

	// Token: 0x06000C72 RID: 3186 RVA: 0x0005DB47 File Offset: 0x0005BF47
	private void Update()
	{
	}
}
