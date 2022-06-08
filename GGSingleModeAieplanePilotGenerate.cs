using System;
using UnityEngine;

// Token: 0x02000268 RID: 616
public class GGSingleModeAieplanePilotGenerate : MonoBehaviour
{
	// Token: 0x060011A3 RID: 4515 RVA: 0x000A1620 File Offset: 0x0009FA20
	private void Start()
	{
		int num = UnityEngine.Random.Range(1, 5);
		this.SingleModeAieplanePilotGeneratePoaition = base.transform.Find("SingleModeAieplanePilotGeneratePoaition" + num.ToString());
		UnityEngine.Object.Instantiate<GameObject>(this.SingleModeAieplanePilot, this.SingleModeAieplanePilotGeneratePoaition.position, this.SingleModeAieplanePilotGeneratePoaition.rotation);
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x000A167F File Offset: 0x0009FA7F
	private void Update()
	{
	}

	// Token: 0x0400148A RID: 5258
	public GameObject SingleModeAieplanePilot;

	// Token: 0x0400148B RID: 5259
	private Transform SingleModeAieplanePilotGeneratePoaition;
}
