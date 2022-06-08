using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
[RequireComponent(typeof(LocalAvoidance))]
public class LocalAvoidanceMover : MonoBehaviour
{
	// Token: 0x06000328 RID: 808 RVA: 0x00018915 File Offset: 0x00016D15
	private void Start()
	{
		this.targetPoint = base.transform.forward * this.targetPointDist + base.transform.position;
		this.controller = base.GetComponent<LocalAvoidance>();
	}

	// Token: 0x06000329 RID: 809 RVA: 0x00018950 File Offset: 0x00016D50
	private void Update()
	{
		if (this.controller != null)
		{
			this.controller.SimpleMove((this.targetPoint - base.transform.position).normalized * this.speed);
		}
	}

	// Token: 0x04000296 RID: 662
	public float targetPointDist = 10f;

	// Token: 0x04000297 RID: 663
	public float speed = 2f;

	// Token: 0x04000298 RID: 664
	private Vector3 targetPoint;

	// Token: 0x04000299 RID: 665
	private LocalAvoidance controller;
}
