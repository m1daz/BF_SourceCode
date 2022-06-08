using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x0200036B RID: 875
public class RTSCamera : MonoBehaviour
{
	// Token: 0x06001B4C RID: 6988 RVA: 0x000DBB0C File Offset: 0x000D9F0C
	private void OnEnable()
	{
		EasyTouch.On_Swipe += this.On_Swipe;
		EasyTouch.On_Drag += this.On_Drag;
		EasyTouch.On_Twist += this.On_Twist;
		EasyTouch.On_Pinch += this.On_Pinch;
	}

	// Token: 0x06001B4D RID: 6989 RVA: 0x000DBB5D File Offset: 0x000D9F5D
	private void On_Twist(Gesture gesture)
	{
		base.transform.Rotate(Vector3.up * gesture.twistAngle);
	}

	// Token: 0x06001B4E RID: 6990 RVA: 0x000DBB7A File Offset: 0x000D9F7A
	private void OnDestroy()
	{
		EasyTouch.On_Swipe -= this.On_Swipe;
		EasyTouch.On_Drag -= this.On_Drag;
		EasyTouch.On_Twist -= this.On_Twist;
	}

	// Token: 0x06001B4F RID: 6991 RVA: 0x000DBBAF File Offset: 0x000D9FAF
	private void On_Drag(Gesture gesture)
	{
		this.On_Swipe(gesture);
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x000DBBB8 File Offset: 0x000D9FB8
	private void On_Swipe(Gesture gesture)
	{
		base.transform.Translate(Vector3.left * gesture.deltaPosition.x / (float)Screen.width);
		base.transform.Translate(Vector3.back * gesture.deltaPosition.y / (float)Screen.height);
	}

	// Token: 0x06001B51 RID: 6993 RVA: 0x000DBC1B File Offset: 0x000DA01B
	private void On_Pinch(Gesture gesture)
	{
		Camera.main.fieldOfView += gesture.deltaPinch * Time.deltaTime;
	}

	// Token: 0x04001D76 RID: 7542
	private Vector3 delta;
}
