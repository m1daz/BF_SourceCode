using System;
using UnityEngine;

// Token: 0x0200020C RID: 524
public class FlashBombExplosion : MonoBehaviour
{
	// Token: 0x06000E53 RID: 3667 RVA: 0x000772B7 File Offset: 0x000756B7
	private void Start()
	{
		if (GameObject.FindWithTag("Player") != null)
		{
			this.mainPlayerCamera = GameObject.FindWithTag("Player").transform.Find("LookObject/Main Camera").GetComponent<Camera>();
		}
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x000772F4 File Offset: 0x000756F4
	private void Update()
	{
		if (this.isRender && !this.hasRaycast)
		{
			this.hasRaycast = true;
			float num = Vector3.Distance(base.transform.position, this.mainPlayerCamera.gameObject.transform.position);
			if (num < 100f)
			{
				Vector3 vector = base.transform.position + new Vector3(0f, 0.2f, 0f);
				if (Physics.Raycast(vector, this.mainPlayerCamera.gameObject.transform.position - vector, out this.hit, 100f, -21) && this.hit.collider.transform.root.tag.Equals("Player"))
				{
					this.hit.collider.transform.root.gameObject.SendMessage("FlashBombRender", num, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x000773FB File Offset: 0x000757FB
	private void OnWillRenderObject()
	{
		if (this.mainPlayerCamera != null)
		{
			this.isRender = true;
		}
	}

	// Token: 0x04000F56 RID: 3926
	private Camera mainPlayerCamera;

	// Token: 0x04000F57 RID: 3927
	private RaycastHit hit;

	// Token: 0x04000F58 RID: 3928
	private bool isRender;

	// Token: 0x04000F59 RID: 3929
	private bool hasRaycast;
}
