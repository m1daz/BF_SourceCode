using System;
using UnityEngine;

// Token: 0x0200021A RID: 538
public class GGLightningBeamLaserEffect_ImpluseGun : MonoBehaviour
{
	// Token: 0x06000E85 RID: 3717 RVA: 0x00079DDE File Offset: 0x000781DE
	private void Start()
	{
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x00079DE0 File Offset: 0x000781E0
	private void Update()
	{
		if (this.IsLaser)
		{
			base.GetComponent<Renderer>().material.mainTextureOffset = this.scrollRate * Time.time;
			this.Laser();
		}
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x00079E14 File Offset: 0x00078214
	public void Impact()
	{
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
					UnityEngine.Object.Instantiate<Transform>(this.impactEffect, raycastHit.point, raycastHit.transform.rotation);
				}
			}
			else if (this.impactEffect)
			{
				Transform transform = UnityEngine.Object.Instantiate<Transform>(this.impactEffect, raycastHit.point, raycastHit.transform.rotation);
			}
		}
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x00079F04 File Offset: 0x00078304
	public void Laser()
	{
		if (this.beamFirePoint == null)
		{
			this.beamFirePoint = base.transform.root.Find("LookObject/Main Camera").transform;
		}
		Vector3 direction = this.beamFirePoint.TransformDirection(Vector3.forward);
		RaycastHit raycastHit;
		if (Physics.Raycast(this.beamFirePoint.position, direction, out raycastHit))
		{
			this.laser.SetPosition(0, base.transform.position);
			this.laser.SetPosition(1, raycastHit.point);
			this.lasBegin = base.transform.position;
			this.lasEnd = raycastHit.point;
		}
		else
		{
			this.laser.SetPosition(0, base.transform.position);
			Vector3 position = this.beamFirePoint.position + direction.normalized * this.maxRange;
			this.laser.SetPosition(1, position);
			this.lasBegin = base.transform.position;
			this.lasEnd = position;
		}
	}

	// Token: 0x04000FCD RID: 4045
	public Transform beamFirePoint;

	// Token: 0x04000FCE RID: 4046
	public Transform impactEffect;

	// Token: 0x04000FCF RID: 4047
	public Vector2 scrollRate = new Vector2(1f, 1f);

	// Token: 0x04000FD0 RID: 4048
	public bool IsLaser;

	// Token: 0x04000FD1 RID: 4049
	public LineRenderer laser;

	// Token: 0x04000FD2 RID: 4050
	public float maxRange = 200f;

	// Token: 0x04000FD3 RID: 4051
	private Vector3 lasBegin;

	// Token: 0x04000FD4 RID: 4052
	private Vector3 lasEnd;
}
