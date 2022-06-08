using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000385 RID: 901
public class UIDrag : MonoBehaviour
{
	// Token: 0x06001BFE RID: 7166 RVA: 0x000DDAD4 File Offset: 0x000DBED4
	private void OnEnable()
	{
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchUp += this.On_TouchUp;
		EasyTouch.On_TouchStart2Fingers += this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers += this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers += this.On_TouchUp2Fingers;
	}

	// Token: 0x06001BFF RID: 7167 RVA: 0x000DDB48 File Offset: 0x000DBF48
	private void OnDestroy()
	{
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers -= this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers -= this.On_TouchUp2Fingers;
	}

	// Token: 0x06001C00 RID: 7168 RVA: 0x000DDBBC File Offset: 0x000DBFBC
	private void On_TouchStart(Gesture gesture)
	{
		if (gesture.isOverGui && this.drag && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)) && this.fingerId == -1)
		{
			this.fingerId = gesture.fingerIndex;
			base.transform.SetAsLastSibling();
		}
	}

	// Token: 0x06001C01 RID: 7169 RVA: 0x000DDC34 File Offset: 0x000DC034
	private void On_TouchDown(Gesture gesture)
	{
		if (this.fingerId == gesture.fingerIndex && this.drag && gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
		{
			base.transform.position += gesture.deltaPosition;
		}
	}

	// Token: 0x06001C02 RID: 7170 RVA: 0x000DDCBA File Offset: 0x000DC0BA
	private void On_TouchUp(Gesture gesture)
	{
		if (this.fingerId == gesture.fingerIndex)
		{
			this.fingerId = -1;
		}
	}

	// Token: 0x06001C03 RID: 7171 RVA: 0x000DDCD4 File Offset: 0x000DC0D4
	private void On_TouchStart2Fingers(Gesture gesture)
	{
		if (gesture.isOverGui && this.drag && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)) && this.fingerId == -1)
		{
			base.transform.SetAsLastSibling();
		}
	}

	// Token: 0x06001C04 RID: 7172 RVA: 0x000DDD40 File Offset: 0x000DC140
	private void On_TouchDown2Fingers(Gesture gesture)
	{
		if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
		{
			if (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform))
			{
				base.transform.position += gesture.deltaPosition;
			}
			this.drag = false;
		}
	}

	// Token: 0x06001C05 RID: 7173 RVA: 0x000DDDE4 File Offset: 0x000DC1E4
	private void On_TouchUp2Fingers(Gesture gesture)
	{
		if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
		{
			this.drag = true;
		}
	}

	// Token: 0x04001D9E RID: 7582
	private int fingerId = -1;

	// Token: 0x04001D9F RID: 7583
	private bool drag = true;
}
