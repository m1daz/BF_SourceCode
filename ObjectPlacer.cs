using System;
using Pathfinding;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class ObjectPlacer : MonoBehaviour
{
	// Token: 0x06000332 RID: 818 RVA: 0x00018DCB File Offset: 0x000171CB
	private void Start()
	{
	}

	// Token: 0x06000333 RID: 819 RVA: 0x00018DCD File Offset: 0x000171CD
	private void Update()
	{
		if (Input.GetKeyDown("p"))
		{
			this.PlaceObject();
		}
		if (Input.GetKeyDown("r"))
		{
			this.RemoveObject();
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00018DFC File Offset: 0x000171FC
	public void PlaceObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity))
		{
			Vector3 point = raycastHit.point;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.go, point, Quaternion.identity);
			Bounds bounds = gameObject.GetComponent<Collider>().bounds;
			GraphUpdateObject ob = new GraphUpdateObject(bounds);
			AstarPath.active.UpdateGraphs(ob);
			if (this.direct)
			{
				AstarPath.active.FlushGraphUpdates();
			}
		}
	}

	// Token: 0x06000335 RID: 821 RVA: 0x00018E7C File Offset: 0x0001727C
	public void RemoveObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, float.PositiveInfinity))
		{
			if (raycastHit.collider.isTrigger || raycastHit.transform.gameObject.name == "Ground")
			{
				return;
			}
			Bounds bounds = raycastHit.collider.bounds;
			UnityEngine.Object.Destroy(raycastHit.collider);
			UnityEngine.Object.Destroy(raycastHit.collider.gameObject);
			GraphUpdateObject ob = new GraphUpdateObject(bounds);
			AstarPath.active.UpdateGraphs(ob, 0f);
			if (this.direct)
			{
				AstarPath.active.FlushGraphUpdates();
			}
		}
	}

	// Token: 0x040002A1 RID: 673
	public GameObject go;

	// Token: 0x040002A2 RID: 674
	public bool direct;
}
