using System;
using UnityEngine;

// Token: 0x02000272 RID: 626
public class GGSingleModeLadder : MonoBehaviour
{
	// Token: 0x060011CA RID: 4554 RVA: 0x000A25E2 File Offset: 0x000A09E2
	private void Awake()
	{
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x000A25E4 File Offset: 0x000A09E4
	private void Start()
	{
		this.climbDirection = this.ladderTop.transform.position - this.ladderBottom.transform.position;
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x000A2611 File Offset: 0x000A0A11
	public Vector3 ClimbDirection()
	{
		return this.climbDirection;
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x000A2619 File Offset: 0x000A0A19
	private void Update()
	{
	}

	// Token: 0x040014B2 RID: 5298
	public GameObject ladderBottom;

	// Token: 0x040014B3 RID: 5299
	public GameObject ladderTop;

	// Token: 0x040014B4 RID: 5300
	public Vector3 climbDirection = Vector3.zero;
}
