using System;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class TargetMover : MonoBehaviour
{
	// Token: 0x06000337 RID: 823 RVA: 0x00018F3B File Offset: 0x0001733B
	public void Start()
	{
		this.cam = Camera.main;
	}

	// Token: 0x06000338 RID: 824 RVA: 0x00018F48 File Offset: 0x00017348
	public void OnGUI()
	{
		if (this.onlyOnDoubleClick && this.cam != null && Event.current.type == EventType.MouseDown && Event.current.clickCount == 2)
		{
			this.UpdateTargetPosition();
		}
	}

	// Token: 0x06000339 RID: 825 RVA: 0x00018F96 File Offset: 0x00017396
	private void Update()
	{
		if (!this.onlyOnDoubleClick && this.cam != null)
		{
			this.UpdateTargetPosition();
		}
	}

	// Token: 0x0600033A RID: 826 RVA: 0x00018FBC File Offset: 0x000173BC
	public void UpdateTargetPosition()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(this.cam.ScreenPointToRay(Input.mousePosition), out raycastHit, float.PositiveInfinity, this.mask))
		{
			this.target.position = raycastHit.point;
		}
	}

	// Token: 0x040002A3 RID: 675
	public LayerMask mask;

	// Token: 0x040002A4 RID: 676
	public Transform target;

	// Token: 0x040002A5 RID: 677
	public bool onlyOnDoubleClick;

	// Token: 0x040002A6 RID: 678
	private Camera cam;
}
