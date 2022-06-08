using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class Laserscope : MonoBehaviour
{
	// Token: 0x06000A72 RID: 2674 RVA: 0x0004BD31 File Offset: 0x0004A131
	private void Start()
	{
		this.lRenderer = base.gameObject.GetComponent<LineRenderer>();
		this.aniTime = 0f;
		this.ChoseNewAnimationTargetCoroutine();
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0004BD58 File Offset: 0x0004A158
	private IEnumerator ChoseNewAnimationTargetCoroutine()
	{
		for (;;)
		{
			this.aniDir = this.aniDir * 0.9f + UnityEngine.Random.Range(0.5f, 1.5f) * 0.1f;
			this.minWidth = this.minWidth * 0.8f + UnityEngine.Random.Range(0.1f, 1f) * 0.2f;
			yield return new WaitForSeconds(1f + UnityEngine.Random.value * 2f - 1f);
		}
		yield break;
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0004BD74 File Offset: 0x0004A174
	private void Update()
	{
		base.GetComponent<Renderer>().material.mainTextureOffset += new Vector2(Time.deltaTime * this.aniDir * this.scrollSpeed, 0f);
		base.GetComponent<Renderer>().material.SetTextureOffset("_NoiseTex", new Vector2(-Time.time * this.aniDir * this.scrollSpeed, 0f));
		this.lRenderer.SetWidth(this.maxWidth, this.maxWidth);
		if (Physics.Raycast(base.transform.position, base.transform.forward, out this.hitInfo))
		{
			if (this.hitInfo.transform)
			{
				this.lRenderer.SetPosition(1, this.hitInfo.distance * Vector3.forward);
				base.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.1f * this.hitInfo.distance, 0f);
				if (this.pointer)
				{
					this.pointer.GetComponent<Renderer>().enabled = true;
					this.pointer.transform.position = this.hitInfo.point + (base.transform.position - this.hitInfo.point) * 0.005f;
					this.pointer.transform.rotation = Quaternion.LookRotation(this.hitInfo.normal, base.transform.up);
					this.pointer.transform.eulerAngles = new Vector3(0f, this.pointer.transform.eulerAngles.y, this.pointer.transform.eulerAngles.z);
				}
			}
			else
			{
				if (this.pointer)
				{
					this.pointer.GetComponent<Renderer>().enabled = false;
				}
				float num = 200f;
				this.lRenderer.SetPosition(1, num * Vector3.forward);
				base.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.1f * num, 0f);
			}
		}
	}

	// Token: 0x04000946 RID: 2374
	public float scrollSpeed = 0.5f;

	// Token: 0x04000947 RID: 2375
	public float pulseSpeed = 1.5f;

	// Token: 0x04000948 RID: 2376
	public float noiseSize = 1f;

	// Token: 0x04000949 RID: 2377
	public float maxWidth = 0.5f;

	// Token: 0x0400094A RID: 2378
	public float minWidth = 0.2f;

	// Token: 0x0400094B RID: 2379
	public GameObject pointer;

	// Token: 0x0400094C RID: 2380
	private LineRenderer lRenderer;

	// Token: 0x0400094D RID: 2381
	private float aniTime;

	// Token: 0x0400094E RID: 2382
	private float aniDir = 1f;

	// Token: 0x0400094F RID: 2383
	private RaycastHit hitInfo;
}
