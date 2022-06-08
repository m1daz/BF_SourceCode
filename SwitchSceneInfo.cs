using System;
using UnityEngine;

// Token: 0x020004EE RID: 1262
public class SwitchSceneInfo : MonoBehaviour
{
	// Token: 0x0600238C RID: 9100 RVA: 0x001107FE File Offset: 0x0010EBFE
	private void Awake()
	{
		this.mRoomName = GGNetworkKit.mInstance.mCurRoomName;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x0600238D RID: 9101 RVA: 0x0011081B File Offset: 0x0010EC1B
	private void Start()
	{
	}

	// Token: 0x0600238E RID: 9102 RVA: 0x0011081D File Offset: 0x0010EC1D
	private void Update()
	{
		if (!this.bExcute)
		{
			this.bExcute = true;
			Application.LoadLevel("DisconnectionReJoinScene");
		}
	}

	// Token: 0x0400244D RID: 9293
	public string mRoomName = string.Empty;

	// Token: 0x0400244E RID: 9294
	private bool bExcute;
}
