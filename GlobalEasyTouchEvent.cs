using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000382 RID: 898
public class GlobalEasyTouchEvent : MonoBehaviour
{
	// Token: 0x06001BF2 RID: 7154 RVA: 0x000DD8D8 File Offset: 0x000DBCD8
	private void OnEnable()
	{
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
		EasyTouch.On_OverUIElement += this.On_OverUIElement;
		EasyTouch.On_UIElementTouchUp += this.On_UIElementTouchUp;
	}

	// Token: 0x06001BF3 RID: 7155 RVA: 0x000DD92C File Offset: 0x000DBD2C
	private void OnDestroy()
	{
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_OverUIElement -= this.On_OverUIElement;
		EasyTouch.On_UIElementTouchUp -= this.On_UIElementTouchUp;
	}

	// Token: 0x06001BF4 RID: 7156 RVA: 0x000DD980 File Offset: 0x000DBD80
	private void On_TouchDown(Gesture gesture)
	{
		this.statText.transform.SetAsFirstSibling();
		if (gesture.pickedUIElement != null)
		{
			this.statText.text = "You touch UI Element : " + gesture.pickedUIElement.name + " (from gesture event)";
		}
		if (!gesture.isOverGui && gesture.pickedObject == null)
		{
			this.statText.text = "You touch an empty area";
		}
		if (gesture.pickedObject != null && !gesture.isOverGui)
		{
			this.statText.text = "You touch a 3D Object";
		}
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x000DDA2B File Offset: 0x000DBE2B
	private void On_OverUIElement(Gesture gesture)
	{
		this.statText.text = "You touch UI Element : " + gesture.pickedUIElement.name + " (from On_OverUIElement event)";
	}

	// Token: 0x06001BF6 RID: 7158 RVA: 0x000DDA52 File Offset: 0x000DBE52
	private void On_UIElementTouchUp(Gesture gesture)
	{
		this.statText.text = string.Empty;
	}

	// Token: 0x06001BF7 RID: 7159 RVA: 0x000DDA64 File Offset: 0x000DBE64
	private void On_TouchUp(Gesture gesture)
	{
		this.statText.text = string.Empty;
	}

	// Token: 0x04001D9D RID: 7581
	public Text statText;
}
