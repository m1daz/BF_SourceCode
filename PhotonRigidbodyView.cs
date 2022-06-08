using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Rigidbody))]
[AddComponentMenu("Photon Networking/Photon Rigidbody View")]
public class PhotonRigidbodyView : MonoBehaviour, IPunObservable
{
	// Token: 0x06000942 RID: 2370 RVA: 0x000470A3 File Offset: 0x000454A3
	private void Awake()
	{
		this.m_Body = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x000470B4 File Offset: 0x000454B4
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
				this.m_Body.velocity = (Vector3)stream.ReceiveNext();
			}
			if (this.m_SynchronizeAngularVelocity)
			{
				this.m_Body.angularVelocity = (Vector3)stream.ReceiveNext();
			}
		}
	}

	// Token: 0x04000827 RID: 2087
	[SerializeField]
	private bool m_SynchronizeVelocity = true;

	// Token: 0x04000828 RID: 2088
	[SerializeField]
	private bool m_SynchronizeAngularVelocity = true;

	// Token: 0x04000829 RID: 2089
	private Rigidbody m_Body;
}
