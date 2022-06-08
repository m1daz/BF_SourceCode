using System;
using Photon;
using UnityEngine;

// Token: 0x0200013F RID: 319
[RequireComponent(typeof(PhotonView))]
public class OnAwakeUsePhotonView : Photon.MonoBehaviour
{
	// Token: 0x06000991 RID: 2449 RVA: 0x00048945 File Offset: 0x00046D45
	private void Awake()
	{
		if (!base.photonView.isMine)
		{
			return;
		}
		base.photonView.RPC("OnAwakeRPC", PhotonTargets.All, new object[0]);
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0004896F File Offset: 0x00046D6F
	private void Start()
	{
		if (!base.photonView.isMine)
		{
			return;
		}
		base.photonView.RPC("OnAwakeRPC", PhotonTargets.All, new object[]
		{
			1
		});
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x000489A2 File Offset: 0x00046DA2
	[PunRPC]
	public void OnAwakeRPC()
	{
		Debug.Log("RPC: 'OnAwakeRPC' PhotonView: " + base.photonView);
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x000489B9 File Offset: 0x00046DB9
	[PunRPC]
	public void OnAwakeRPC(byte myParameter)
	{
		Debug.Log(string.Concat(new object[]
		{
			"RPC: 'OnAwakeRPC' Parameter: ",
			myParameter,
			" PhotonView: ",
			base.photonView
		}));
	}
}
