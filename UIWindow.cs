using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000384 RID: 900
public class UIWindow : MonoBehaviour, IDragHandler, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06001BFB RID: 7163 RVA: 0x000DDA8E File Offset: 0x000DBE8E
	public void OnDrag(PointerEventData eventData)
	{
		base.transform.position += eventData.delta;
	}

	// Token: 0x06001BFC RID: 7164 RVA: 0x000DDAB1 File Offset: 0x000DBEB1
	public void OnPointerDown(PointerEventData eventData)
	{
		base.transform.SetAsLastSibling();
	}
}
