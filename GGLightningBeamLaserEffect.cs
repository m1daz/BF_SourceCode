using System;
using UnityEngine;

// Token: 0x02000219 RID: 537
public class GGLightningBeamLaserEffect : MonoBehaviour
{
	// Token: 0x06000E81 RID: 3713 RVA: 0x00079B56 File Offset: 0x00077F56
	private void Start()
	{
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x00079B58 File Offset: 0x00077F58
	private void Update()
	{
		if (this.IsLaser)
		{
			this.Laser();
			this.time += Time.deltaTime;
			this.alpha = Mathf.PingPong(Time.time * this.fadeSpeed, this.AlphaMax);
			this.alpha = Mathf.Max(this.alpha, 0.4f);
			this.laserSize = Mathf.PingPong(Time.time * this.enlargeSpeed, this.laserSizeMax);
			this.laserSize = Mathf.Max(this.laserSize, 0.1f);
			this.laser.SetWidth(this.laserSize, this.laserSize);
			this.laser.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(this.myColor.r, this.myColor.g, this.myColor.b, this.alpha));
		}
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x00079C4C File Offset: 0x0007804C
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
		if (this.normalizeUV)
		{
			float num = Vector3.Distance(this.lasBegin, this.lasEnd);
			base.GetComponent<Renderer>().materials[0].mainTextureScale = new Vector2(num / this.normalizeUvLength, base.GetComponent<Renderer>().materials[0].mainTextureScale.y);
		}
	}

	// Token: 0x04000FBC RID: 4028
	public LineRenderer laser;

	// Token: 0x04000FBD RID: 4029
	public float laserSize = 0.2f;

	// Token: 0x04000FBE RID: 4030
	public float laserSizeMax = 0.3f;

	// Token: 0x04000FBF RID: 4031
	public float fadeSpeed = 2.5f;

	// Token: 0x04000FC0 RID: 4032
	public float enlargeSpeed = 3f;

	// Token: 0x04000FC1 RID: 4033
	public float beginTintAlpha = 0.5f;

	// Token: 0x04000FC2 RID: 4034
	public float AlphaMax = 0.7f;

	// Token: 0x04000FC3 RID: 4035
	public Color myColor;

	// Token: 0x04000FC4 RID: 4036
	private float time;

	// Token: 0x04000FC5 RID: 4037
	private float alpha;

	// Token: 0x04000FC6 RID: 4038
	public bool normalizeUV = true;

	// Token: 0x04000FC7 RID: 4039
	public float normalizeUvLength = 4f;

	// Token: 0x04000FC8 RID: 4040
	public float maxRange = 200f;

	// Token: 0x04000FC9 RID: 4041
	private Vector3 lasBegin;

	// Token: 0x04000FCA RID: 4042
	private Vector3 lasEnd;

	// Token: 0x04000FCB RID: 4043
	public Transform beamFirePoint;

	// Token: 0x04000FCC RID: 4044
	public bool IsLaser;
}
