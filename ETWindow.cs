using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000381 RID: 897
public class ETWindow : MonoBehaviour
{
	// Token: 0x06001BED RID: 7149 RVA: 0x000DD7AC File Offset: 0x000DBBAC
	private void OnEnable()
	{
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchStart += this.On_TouchStart;
	}

	// Token: 0x06001BEE RID: 7150 RVA: 0x000DD7D0 File Offset: 0x000DBBD0
	private void OnDestroy()
	{
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchStart -= this.On_TouchStart;
	}

	// Token: 0x06001BEF RID: 7151 RVA: 0x000DD7F4 File Offset: 0x000DBBF4
	private void On_TouchStart(Gesture gesture)
	{
		this.drag = false;
		if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
		{
			base.transform.SetAsLastSibling();
			this.drag = true;
		}
	}

	// Token: 0x06001BF0 RID: 7152 RVA: 0x000DD858 File Offset: 0x000DBC58
	private void On_TouchDown(Gesture gesture)
	{
		if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)) && this.drag)
		{
			base.transform.position += gesture.deltaPosition;
		}
	}

	// Token: 0x04001D9C RID: 7580
	private bool drag;
}
