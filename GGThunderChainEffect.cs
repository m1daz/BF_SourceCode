using System;
using UnityEngine;

// Token: 0x0200023D RID: 573
public class GGThunderChainEffect : MonoBehaviour
{
	// Token: 0x0600103F RID: 4159 RVA: 0x0008AC06 File Offset: 0x00089006
	private void Start()
	{
		this.Laser();
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x0008AC10 File Offset: 0x00089010
	private void Update()
	{
		this.time += Time.deltaTime;
		this.alpha = Mathf.PingPong(Time.time * this.fadeSpeed, this.AlphaMax);
		this.alpha = Mathf.Max(this.alpha, 0.4f);
		this.laserSize = Mathf.PingPong(Time.time * this.enlargeSpeed, this.laserSizeMax);
		this.laserSize = Mathf.Max(this.laserSize, 0.1f);
		this.laser.SetWidth(this.laserSize, this.laserSize);
		this.laser.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(this.myColor.r, this.myColor.g, this.myColor.b, this.alpha));
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x0008ACF4 File Offset: 0x000890F4
	public void Laser()
	{
		this.laser.SetPosition(0, this.startPosition);
		this.laser.SetPosition(1, this.endPosition);
		this.lasBegin = this.startPosition;
		this.lasEnd = this.endPosition;
		if (this.normalizeUV)
		{
			float num = Vector3.Distance(this.lasBegin, this.lasEnd);
			base.GetComponent<Renderer>().materials[0].mainTextureScale = new Vector2(num / this.normalizeUvLength, base.GetComponent<Renderer>().materials[0].mainTextureScale.y);
		}
	}

	// Token: 0x04001236 RID: 4662
	public LineRenderer laser;

	// Token: 0x04001237 RID: 4663
	public float laserSize = 0.2f;

	// Token: 0x04001238 RID: 4664
	public float laserSizeMax = 0.3f;

	// Token: 0x04001239 RID: 4665
	public float fadeSpeed = 2.5f;

	// Token: 0x0400123A RID: 4666
	public float enlargeSpeed = 3f;

	// Token: 0x0400123B RID: 4667
	public float beginTintAlpha = 0.5f;

	// Token: 0x0400123C RID: 4668
	public float AlphaMax = 0.7f;

	// Token: 0x0400123D RID: 4669
	public Color myColor;

	// Token: 0x0400123E RID: 4670
	private float time;

	// Token: 0x0400123F RID: 4671
	private float alpha;

	// Token: 0x04001240 RID: 4672
	public bool normalizeUV = true;

	// Token: 0x04001241 RID: 4673
	public float normalizeUvLength = 4f;

	// Token: 0x04001242 RID: 4674
	public float maxRange = 200f;

	// Token: 0x04001243 RID: 4675
	private Vector3 lasBegin;

	// Token: 0x04001244 RID: 4676
	private Vector3 lasEnd;

	// Token: 0x04001245 RID: 4677
	public Transform beamFirePoint;

	// Token: 0x04001246 RID: 4678
	public bool IsLaser;

	// Token: 0x04001247 RID: 4679
	public Vector3 startPosition;

	// Token: 0x04001248 RID: 4680
	public Vector3 endPosition;
}
