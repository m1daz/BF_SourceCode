using System;
using UnityEngine;

// Token: 0x02000218 RID: 536
public class GGLightningBeamLaserControl : MonoBehaviour
{
	// Token: 0x06000E7D RID: 3709 RVA: 0x00079990 File Offset: 0x00077D90
	private void Start()
	{
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x00079992 File Offset: 0x00077D92
	private void Update()
	{
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x00079994 File Offset: 0x00077D94
	public void MuzzleAndImpact()
	{
		if (this.muzzleEffect)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.muzzleEffect, base.transform.position, base.transform.rotation);
			gameObject.transform.parent = base.transform.parent;
			if (this.beamFirePoint == null)
			{
				this.beamFirePoint = base.transform.root.Find("LookObject/Main Camera").transform;
			}
			Vector3 direction = this.beamFirePoint.TransformDirection(Vector3.forward);
			RaycastHit raycastHit;
			if (Physics.Raycast(this.beamFirePoint.position, direction, out raycastHit))
			{
				if (raycastHit.transform.root.gameObject.name != "ExampleCharacter(Clone)")
				{
					if (this.impactEffect)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.impactEffect, raycastHit.point, raycastHit.transform.rotation);
					}
				}
				else if (this.hitPlayerImpactEffect)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.hitPlayerImpactEffect, raycastHit.point, raycastHit.transform.rotation);
					gameObject2.transform.parent = raycastHit.transform.root;
				}
			}
		}
	}

	// Token: 0x04000FB8 RID: 4024
	public GameObject impactEffect;

	// Token: 0x04000FB9 RID: 4025
	public GameObject hitPlayerImpactEffect;

	// Token: 0x04000FBA RID: 4026
	public GameObject muzzleEffect;

	// Token: 0x04000FBB RID: 4027
	public Transform beamFirePoint;
}
