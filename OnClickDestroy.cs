using System;
using System.Collections;
using Photon;
using UnityEngine;

// Token: 0x02000140 RID: 320
[RequireComponent(typeof(PhotonView))]
public class OnClickDestroy : Photon.MonoBehaviour
{
	// Token: 0x06000996 RID: 2454 RVA: 0x000489F5 File Offset: 0x00046DF5
	public void OnClick()
	{
		if (!this.DestroyByRpc)
		{
			PhotonNetwork.Destroy(base.gameObject);
		}
		else
		{
			base.photonView.RPC("DestroyRpc", PhotonTargets.AllBuffered, new object[0]);
		}
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x00048A2C File Offset: 0x00046E2C
	[PunRPC]
	public IEnumerator DestroyRpc()
	{
		UnityEngine.Object.Destroy(base.gameObject);
		yield return 0;
		PhotonNetwork.UnAllocateViewID(base.photonView.viewID);
		yield break;
	}

	// Token: 0x0400088C RID: 2188
	public bool DestroyByRpc;
}
