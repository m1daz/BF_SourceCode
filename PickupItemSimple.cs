using System;
using Photon;
using UnityEngine;

// Token: 0x02000147 RID: 327
[RequireComponent(typeof(PhotonView))]
public class PickupItemSimple : Photon.MonoBehaviour
{
	// Token: 0x060009AE RID: 2478 RVA: 0x0004909C File Offset: 0x0004749C
	public void OnTriggerEnter(Collider other)
	{
		PhotonView component = other.GetComponent<PhotonView>();
		if (this.PickupOnCollide && component != null && component.isMine)
		{
			this.Pickup();
		}
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x000490D8 File Offset: 0x000474D8
	public void Pickup()
	{
		if (this.SentPickup)
		{
			return;
		}
		this.SentPickup = true;
		base.photonView.RPC("PunPickupSimple", PhotonTargets.AllViaServer, new object[0]);
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x00049104 File Offset: 0x00047504
	[PunRPC]
	public void PunPickupSimple(PhotonMessageInfo msgInfo)
	{
		if (!this.SentPickup || !msgInfo.sender.isLocal || base.gameObject.GetActive())
		{
		}
		this.SentPickup = false;
		if (!base.gameObject.GetActive())
		{
			Debug.Log("Ignored PU RPC, cause item is inactive. " + base.gameObject);
			return;
		}
		double num = PhotonNetwork.time - msgInfo.timestamp;
		float num2 = this.SecondsBeforeRespawn - (float)num;
		if (num2 > 0f)
		{
			base.gameObject.SetActive(false);
			base.Invoke("RespawnAfter", num2);
		}
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x000491AA File Offset: 0x000475AA
	public void RespawnAfter()
	{
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x040008A0 RID: 2208
	public float SecondsBeforeRespawn = 2f;

	// Token: 0x040008A1 RID: 2209
	public bool PickupOnCollide;

	// Token: 0x040008A2 RID: 2210
	public bool SentPickup;
}
