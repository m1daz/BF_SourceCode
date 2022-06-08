using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x0200037A RID: 890
public class TwistMe : MonoBehaviour
{
	// Token: 0x06001BAD RID: 7085 RVA: 0x000DCD0C File Offset: 0x000DB10C
	private void OnEnable()
	{
		EasyTouch.On_TouchStart2Fingers += this.On_TouchStart2Fingers;
		EasyTouch.On_Twist += this.On_Twist;
		EasyTouch.On_TwistEnd += this.On_TwistEnd;
		EasyTouch.On_Cancel2Fingers += this.On_Cancel2Fingers;
	}

	// Token: 0x06001BAE RID: 7086 RVA: 0x000DCD5D File Offset: 0x000DB15D
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BAF RID: 7087 RVA: 0x000DCD65 File Offset: 0x000DB165
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BB0 RID: 7088 RVA: 0x000DCD70 File Offset: 0x000DB170
	private void UnsubscribeEvent()
	{
		EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
		EasyTouch.On_Twist -= this.On_Twist;
		EasyTouch.On_TwistEnd -= this.On_TwistEnd;
		EasyTouch.On_Cancel2Fingers -= this.On_Cancel2Fingers;
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x000DCDC1 File Offset: 0x000DB1C1
	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
	}

	// Token: 0x06001BB2 RID: 7090 RVA: 0x000DCDCF File Offset: 0x000DB1CF
	private void On_TouchStart2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			EasyTouch.SetEnableTwist(true);
			EasyTouch.SetEnablePinch(false);
		}
	}

	// Token: 0x06001BB3 RID: 7091 RVA: 0x000DCDF4 File Offset: 0x000DB1F4
	private void On_Twist(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.transform.Rotate(new Vector3(0f, 0f, gesture.twistAngle));
			this.textMesh.text = "Delta angle : " + gesture.twistAngle.ToString();
		}
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x000DCE5D File Offset: 0x000DB25D
	private void On_TwistEnd(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			EasyTouch.SetEnablePinch(true);
			base.transform.rotation = Quaternion.identity;
			this.textMesh.text = "Twist me";
		}
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x000DCE9B File Offset: 0x000DB29B
	private void On_Cancel2Fingers(Gesture gesture)
	{
		EasyTouch.SetEnablePinch(true);
		base.transform.rotation = Quaternion.identity;
		this.textMesh.text = "Twist me";
	}

	// Token: 0x04001D92 RID: 7570
	private TextMesh textMesh;
}
