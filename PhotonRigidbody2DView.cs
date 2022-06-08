using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu("Photon Networking/Photon Rigidbody 2D View")]
public class PhotonRigidbody2DView : MonoBehaviour, IPunObservable
{
	// Token: 0x0600093F RID: 2367 RVA: 0x00046FDB File Offset: 0x000453DB
	private void Awake()
	{
		this.m_Body = base.GetComponent<Rigidbody2D>();
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x00046FEC File Offset: 0x000453EC
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			if (this.m_SynchronizeVelocity)
			{
				stream.SendNext(this.m_Body.velocity);
			}
			if (this.m_SynchronizeAngularVelocity)
			{
				stream.SendNext(this.m_Body.angularVelocity);
			}
		}
		else
		{
			if (this.m_SynchronizeVelocity)
			{
				this.m_Body.velocity = (Vector2)stream.ReceiveNext();
			}
			if (this.m_SynchronizeAngularVelocity)
			{
				this.m_Body.angularVelocity = (float)stream.ReceiveNext();
			}
		}
	}

	// Token: 0x04000824 RID: 2084
	[SerializeField]
	private bool m_SynchronizeVelocity = true;

	// Token: 0x04000825 RID: 2085
	[SerializeField]
	private bool m_SynchronizeAngularVelocity = true;

	// Token: 0x04000826 RID: 2086
	private Rigidbody2D m_Body;
}
