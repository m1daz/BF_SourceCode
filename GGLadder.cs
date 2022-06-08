using System;
using UnityEngine;

// Token: 0x020001E9 RID: 489
public class GGLadder : MonoBehaviour
{
	// Token: 0x06000D96 RID: 3478 RVA: 0x00070A5E File Offset: 0x0006EE5E
	private void Awake()
	{
		this.myMeshRender = base.gameObject.GetComponent<MeshRenderer>();
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x00070A71 File Offset: 0x0006EE71
	private void Start()
	{
		this.climbDirection = this.ladderTop.transform.position - this.ladderBottom.transform.position;
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x00070A9E File Offset: 0x0006EE9E
	public Vector3 ClimbDirection()
	{
		return this.climbDirection;
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x00070AA6 File Offset: 0x0006EEA6
	private void Update()
	{
	}

	// Token: 0x04000DC4 RID: 3524
	public GameObject ladderBottom;

	// Token: 0x04000DC5 RID: 3525
	public GameObject ladderTop;

	// Token: 0x04000DC6 RID: 3526
	public MeshRenderer myMeshRender;

	// Token: 0x04000DC7 RID: 3527
	public Vector3 climbDirection = Vector3.zero;
}
