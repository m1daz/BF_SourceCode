using System;
using Photon;
using UnityEngine;

// Token: 0x02000138 RID: 312
[RequireComponent(typeof(PhotonView))]
public class HighlightOwnedGameObj : Photon.MonoBehaviour
{
	// Token: 0x0600096B RID: 2411 RVA: 0x00047CF0 File Offset: 0x000460F0
	private void Update()
	{
		if (base.photonView.isMine)
		{
			if (this.markerTransform == null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PointerPrefab);
				gameObject.transform.parent = base.gameObject.transform;
				this.markerTransform = gameObject.transform;
			}
			Vector3 position = base.gameObject.transform.position;
			this.markerTransform.position = new Vector3(position.x, position.y + this.Offset, position.z);
			this.markerTransform.rotation = Quaternion.identity;
		}
		else if (this.markerTransform != null)
		{
			UnityEngine.Object.Destroy(this.markerTransform.gameObject);
			this.markerTransform = null;
		}
	}

	// Token: 0x0400086B RID: 2155
	public GameObject PointerPrefab;

	// Token: 0x0400086C RID: 2156
	public float Offset = 0.5f;

	// Token: 0x0400086D RID: 2157
	private Transform markerTransform;
}
