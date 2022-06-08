using System;
using UnityEngine;

// Token: 0x0200023E RID: 574
public class GGThunderChainForBoss1_4 : MonoBehaviour
{
	// Token: 0x06001043 RID: 4163 RVA: 0x0008ADB0 File Offset: 0x000891B0
	private void Start()
	{
		this.mlineRender = base.GetComponent<LineRenderer>();
		this.mBoxCollider = base.GetComponent<BoxCollider>();
	}

	// Token: 0x06001044 RID: 4164 RVA: 0x0008ADCA File Offset: 0x000891CA
	private void Update()
	{
		base.GetComponent<Renderer>().material.mainTextureOffset = this.scrollRate * Time.time;
		this.ThunderChain();
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x0008ADF4 File Offset: 0x000891F4
	private void ThunderChain()
	{
		Vector3 position = this.startTransform.position;
		Vector3 position2 = this.endTransform.position;
		this.mlineRender.SetPosition(0, position);
		this.mlineRender.SetPosition(1, position2);
		base.transform.LookAt(position2);
		this.mBoxCollider.center = new Vector3(0f, 0f, Vector3.Distance(position, position2) * 0.5f);
		this.mBoxCollider.size = new Vector3(0.5f, 0.5f, Vector3.Distance(position, position2));
	}

	// Token: 0x04001249 RID: 4681
	public Vector2 scrollRate = new Vector2(1f, 1f);

	// Token: 0x0400124A RID: 4682
	public Transform startTransform;

	// Token: 0x0400124B RID: 4683
	public Transform endTransform;

	// Token: 0x0400124C RID: 4684
	public LineRenderer mlineRender;

	// Token: 0x0400124D RID: 4685
	public BoxCollider mBoxCollider;
}
