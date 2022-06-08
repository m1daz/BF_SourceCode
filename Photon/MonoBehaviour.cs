using System;
using UnityEngine;

namespace Photon
{
	// Token: 0x0200010A RID: 266
	public class MonoBehaviour : MonoBehaviour
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00040D62 File Offset: 0x0003F162
		public PhotonView photonView
		{
			get
			{
				if (this.pvCache == null)
				{
					this.pvCache = PhotonView.Get(this);
				}
				return this.pvCache;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00040D87 File Offset: 0x0003F187
		[Obsolete("Use a photonView")]
		public PhotonView networkView
		{
			get
			{
				Debug.LogWarning("Why are you still using networkView? should be PhotonView?");
				return PhotonView.Get(this);
			}
		}

		// Token: 0x04000759 RID: 1881
		private PhotonView pvCache;
	}
}
