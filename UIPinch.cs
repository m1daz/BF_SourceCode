using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000386 RID: 902
public class UIPinch : MonoBehaviour
{
	// Token: 0x06001C07 RID: 7175 RVA: 0x000DDE3C File Offset: 0x000DC23C
	public void OnEnable()
	{
		EasyTouch.On_Pinch += this.On_Pinch;
	}

	// Token: 0x06001C08 RID: 7176 RVA: 0x000DDE4F File Offset: 0x000DC24F
	public void OnDestroy()
	{
		EasyTouch.On_Pinch -= this.On_Pinch;
	}

	// Token: 0x06001C09 RID: 7177 RVA: 0x000DDE64 File Offset: 0x000DC264
	private void On_Pinch(Gesture gesture)
	{
		if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
		{
			base.transform.localScale = new Vector3(base.transform.localScale.x + gesture.deltaPinch * Time.deltaTime, base.transform.localScale.y + gesture.deltaPinch * Time.deltaTime, base.transform.localScale.z);
		}
	}
}
