using System;
using UnityEngine;

// Token: 0x0200043F RID: 1087
public class DragTrail : MonoBehaviour
{
	// Token: 0x06001F8E RID: 8078 RVA: 0x000EFEC0 File Offset: 0x000EE2C0
	private void Start()
	{
		this.lineRenderer = UnityEngine.Object.Instantiate<LineRenderer>(this.lineRendererPrefab, base.transform.position, base.transform.rotation);
		this.lineRenderer.transform.parent = base.transform;
		this.lineRenderer.enabled = false;
	}

	// Token: 0x06001F8F RID: 8079 RVA: 0x000EFF18 File Offset: 0x000EE318
	private void OnDragBegin()
	{
		this.lineRenderer.enabled = true;
		this.lineRenderer.SetPosition(0, base.transform.position);
		this.lineRenderer.SetPosition(1, base.transform.position);
		this.lineRenderer.SetWidth(0.01f, base.transform.localScale.x);
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x000EFF82 File Offset: 0x000EE382
	private void OnDragEnd()
	{
		this.lineRenderer.enabled = false;
	}

	// Token: 0x06001F91 RID: 8081 RVA: 0x000EFF90 File Offset: 0x000EE390
	private void Update()
	{
		if (this.lineRenderer.enabled)
		{
			this.lineRenderer.SetPosition(1, base.transform.position);
		}
	}

	// Token: 0x0400209B RID: 8347
	public LineRenderer lineRendererPrefab;

	// Token: 0x0400209C RID: 8348
	private LineRenderer lineRenderer;
}
