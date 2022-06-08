using System;
using UnityEngine;

// Token: 0x02000442 RID: 1090
[RequireComponent(typeof(SampleUI))]
public class SampleBase : MonoBehaviour
{
	// Token: 0x06001F9A RID: 8090 RVA: 0x000EF228 File Offset: 0x000ED628
	protected virtual string GetHelpText()
	{
		return string.Empty;
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06001F9B RID: 8091 RVA: 0x000EF22F File Offset: 0x000ED62F
	public SampleUI UI
	{
		get
		{
			return this.ui;
		}
	}

	// Token: 0x06001F9C RID: 8092 RVA: 0x000EF237 File Offset: 0x000ED637
	protected virtual void Awake()
	{
		this.ui = base.GetComponent<SampleUI>();
	}

	// Token: 0x06001F9D RID: 8093 RVA: 0x000EF245 File Offset: 0x000ED645
	protected virtual void Start()
	{
		this.ui.helpText = this.GetHelpText();
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x000EF258 File Offset: 0x000ED658
	public static Vector3 GetWorldPos(Vector2 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		float distance = -ray.origin.z / ray.direction.z;
		return ray.GetPoint(distance);
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x000EF2A0 File Offset: 0x000ED6A0
	public static GameObject PickObject(Vector2 screenPos)
	{
		Ray ray = Camera.main.ScreenPointToRay(screenPos);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit))
		{
			return raycastHit.collider.gameObject;
		}
		return null;
	}

	// Token: 0x040020A3 RID: 8355
	private SampleUI ui;
}
