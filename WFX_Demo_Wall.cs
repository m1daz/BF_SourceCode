using System;
using UnityEngine;

// Token: 0x0200052F RID: 1327
public class WFX_Demo_Wall : MonoBehaviour
{
	// Token: 0x060025AF RID: 9647 RVA: 0x0011823C File Offset: 0x0011663C
	private void OnMouseDown()
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (base.GetComponent<Collider>().Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 9999f))
		{
			GameObject gameObject = this.demo.spawnParticle();
			gameObject.transform.position = raycastHit.point;
			gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.forward, raycastHit.normal);
		}
	}

	// Token: 0x0400264C RID: 9804
	public WFX_Demo demo;
}
