using System;
using Photon;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class SmoothSyncMovement : Photon.MonoBehaviour, IPunObservable
{
	// Token: 0x060009EC RID: 2540 RVA: 0x0004A18A File Offset: 0x0004858A
	public void Awake()
	{
		if (base.photonView == null || base.photonView.observed != this)
		{
			Debug.LogWarning(this + " is not observed by this object's photonView! OnPhotonSerializeView() in this class won't be used.");
		}
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0004A1C4 File Offset: 0x000485C4
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(base.transform.position);
			stream.SendNext(base.transform.rotation);
		}
		else
		{
			this.correctPlayerPos = (Vector3)stream.ReceiveNext();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0004A230 File Offset: 0x00048630
	public void Update()
	{
		if (!base.photonView.isMine)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.correctPlayerPos, Time.deltaTime * this.SmoothingDelay);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.correctPlayerRot, Time.deltaTime * this.SmoothingDelay);
		}
	}

	// Token: 0x040008BD RID: 2237
	public float SmoothingDelay = 5f;

	// Token: 0x040008BE RID: 2238
	private Vector3 correctPlayerPos = Vector3.zero;

	// Token: 0x040008BF RID: 2239
	private Quaternion correctPlayerRot = Quaternion.identity;
}
