using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000368 RID: 872
public class MultiCameraTouch : MonoBehaviour
{
	// Token: 0x06001B3D RID: 6973 RVA: 0x000DB942 File Offset: 0x000D9D42
	private void OnEnable()
	{
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
	}

	// Token: 0x06001B3E RID: 6974 RVA: 0x000DB966 File Offset: 0x000D9D66
	private void OnDestroy()
	{
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
	}

	// Token: 0x06001B3F RID: 6975 RVA: 0x000DB98C File Offset: 0x000D9D8C
	private void On_TouchDown(Gesture gesture)
	{
		if (gesture.pickedObject != null)
		{
			this.label.text = "You touch : " + gesture.pickedObject.name + " on camera : " + gesture.pickedCamera.name;
		}
	}

	// Token: 0x06001B40 RID: 6976 RVA: 0x000DB9DA File Offset: 0x000D9DDA
	private void On_TouchUp(Gesture gesture)
	{
		this.label.text = string.Empty;
	}

	// Token: 0x04001D72 RID: 7538
	public Text label;
}
