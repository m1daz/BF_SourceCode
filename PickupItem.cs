using System;
using System.Collections.Generic;
using Photon;
using UnityEngine;

// Token: 0x02000146 RID: 326
[RequireComponent(typeof(PhotonView))]
public class PickupItem : Photon.MonoBehaviour, IPunObservable
{
	// Token: 0x17000127 RID: 295
	// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00048D60 File Offset: 0x00047160
	public int ViewID
	{
		get
		{
			return base.photonView.viewID;
		}
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x00048D70 File Offset: 0x00047170
	public void OnTriggerEnter(Collider other)
	{
		PhotonView component = other.GetComponent<PhotonView>();
		if (this.PickupOnTrigger && component != null && component.isMine)
		{
			this.Pickup();
		}
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00048DAC File Offset: 0x000471AC
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting && this.SecondsBeforeRespawn <= 0f)
		{
			stream.SendNext(base.gameObject.transform.position);
		}
		else
		{
			Vector3 position = (Vector3)stream.ReceiveNext();
			base.gameObject.transform.position = position;
		}
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00048E11 File Offset: 0x00047211
	public void Pickup()
	{
		if (this.SentPickup)
		{
			return;
		}
		this.SentPickup = true;
		base.photonView.RPC("PunPickup", PhotonTargets.AllViaServer, new object[0]);
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x00048E3D File Offset: 0x0004723D
	public void Drop()
	{
		if (this.PickupIsMine)
		{
			base.photonView.RPC("PunRespawn", PhotonTargets.AllViaServer, new object[0]);
		}
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x00048E61 File Offset: 0x00047261
	public void Drop(Vector3 newPosition)
	{
		if (this.PickupIsMine)
		{
			base.photonView.RPC("PunRespawn", PhotonTargets.AllViaServer, new object[]
			{
				newPosition
			});
		}
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x00048E90 File Offset: 0x00047290
	[PunRPC]
	public void PunPickup(PhotonMessageInfo msgInfo)
	{
		if (msgInfo.sender.isLocal)
		{
			this.SentPickup = false;
		}
		if (!base.gameObject.GetActive())
		{
			Debug.Log(string.Concat(new object[]
			{
				"Ignored PU RPC, cause item is inactive. ",
				base.gameObject,
				" SecondsBeforeRespawn: ",
				this.SecondsBeforeRespawn,
				" TimeOfRespawn: ",
				this.TimeOfRespawn,
				" respawn in future: ",
				this.TimeOfRespawn > PhotonNetwork.time
			}));
			return;
		}
		this.PickupIsMine = msgInfo.sender.isLocal;
		if (this.OnPickedUpCall != null)
		{
			this.OnPickedUpCall.SendMessage("OnPickedUp", this);
		}
		if (this.SecondsBeforeRespawn <= 0f)
		{
			this.PickedUp(0f);
		}
		else
		{
			double num = PhotonNetwork.time - msgInfo.timestamp;
			double num2 = (double)this.SecondsBeforeRespawn - num;
			if (num2 > 0.0)
			{
				this.PickedUp((float)num2);
			}
		}
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00048FB4 File Offset: 0x000473B4
	internal void PickedUp(float timeUntilRespawn)
	{
		base.gameObject.SetActive(false);
		PickupItem.DisabledPickupItems.Add(this);
		this.TimeOfRespawn = 0.0;
		if (timeUntilRespawn > 0f)
		{
			this.TimeOfRespawn = PhotonNetwork.time + (double)timeUntilRespawn;
			base.Invoke("PunRespawn", timeUntilRespawn);
		}
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0004900D File Offset: 0x0004740D
	[PunRPC]
	internal void PunRespawn(Vector3 pos)
	{
		Debug.Log("PunRespawn with Position.");
		this.PunRespawn();
		base.gameObject.transform.position = pos;
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x00049030 File Offset: 0x00047430
	[PunRPC]
	internal void PunRespawn()
	{
		PickupItem.DisabledPickupItems.Remove(this);
		this.TimeOfRespawn = 0.0;
		this.PickupIsMine = false;
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x04000899 RID: 2201
	public float SecondsBeforeRespawn = 2f;

	// Token: 0x0400089A RID: 2202
	public bool PickupOnTrigger;

	// Token: 0x0400089B RID: 2203
	public bool PickupIsMine;

	// Token: 0x0400089C RID: 2204
	public UnityEngine.MonoBehaviour OnPickedUpCall;

	// Token: 0x0400089D RID: 2205
	public bool SentPickup;

	// Token: 0x0400089E RID: 2206
	public double TimeOfRespawn;

	// Token: 0x0400089F RID: 2207
	public static HashSet<PickupItem> DisabledPickupItems = new HashSet<PickupItem>();
}
