using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000366 RID: 870
public class MultiLayerTouch : MonoBehaviour
{
	// Token: 0x06001B32 RID: 6962 RVA: 0x000DB66A File Offset: 0x000D9A6A
	private void OnEnable()
	{
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
	}

	// Token: 0x06001B33 RID: 6963 RVA: 0x000DB68E File Offset: 0x000D9A8E
	private void OnDestroy()
	{
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
	}

	// Token: 0x06001B34 RID: 6964 RVA: 0x000DB6B4 File Offset: 0x000D9AB4
	private void On_TouchDown(Gesture gesture)
	{
		if (gesture.pickedObject != null)
		{
			if (!EasyTouch.GetAutoUpdatePickedObject())
			{
				this.label.text = string.Concat(new object[]
				{
					"Picked object from event : ",
					gesture.pickedObject.name,
					" : ",
					gesture.position
				});
			}
			else
			{
				this.label.text = string.Concat(new object[]
				{
					"Picked object from event : ",
					gesture.pickedObject.name,
					" : ",
					gesture.position
				});
			}
		}
		else if (!EasyTouch.GetAutoUpdatePickedObject())
		{
			this.label.text = "Picked object from event :  none";
		}
		else
		{
			this.label.text = "Picked object from event : none";
		}
		this.label2.text = string.Empty;
		if (!EasyTouch.GetAutoUpdatePickedObject())
		{
			GameObject currentPickedObject = gesture.GetCurrentPickedObject(false);
			if (currentPickedObject != null)
			{
				this.label2.text = "Picked object from GetCurrentPickedObject : " + currentPickedObject.name;
			}
			else
			{
				this.label2.text = "Picked object from GetCurrentPickedObject : none";
			}
		}
	}

	// Token: 0x06001B35 RID: 6965 RVA: 0x000DB7F7 File Offset: 0x000D9BF7
	private void On_TouchUp(Gesture gesture)
	{
		this.label.text = string.Empty;
		this.label2.text = string.Empty;
	}

	// Token: 0x04001D70 RID: 7536
	public Text label;

	// Token: 0x04001D71 RID: 7537
	public Text label2;
}
