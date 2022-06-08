using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200037E RID: 894
public class TwoSwipe : MonoBehaviour
{
	// Token: 0x06001BD3 RID: 7123 RVA: 0x000DD3D4 File Offset: 0x000DB7D4
	private void OnEnable()
	{
		EasyTouch.On_SwipeStart2Fingers += this.On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers += this.On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers += this.On_SwipeEnd2Fingers;
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x000DD409 File Offset: 0x000DB809
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BD5 RID: 7125 RVA: 0x000DD411 File Offset: 0x000DB811
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x000DD419 File Offset: 0x000DB819
	private void UnsubscribeEvent()
	{
		EasyTouch.On_SwipeStart2Fingers -= this.On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers -= this.On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers -= this.On_SwipeEnd2Fingers;
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x000DD44E File Offset: 0x000DB84E
	private void On_SwipeStart2Fingers(Gesture gesture)
	{
		this.swipeData.text = "You start a swipe";
	}

	// Token: 0x06001BD8 RID: 7128 RVA: 0x000DD460 File Offset: 0x000DB860
	private void On_Swipe2Fingers(Gesture gesture)
	{
		Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(5f);
		this.trail.transform.position = touchToWorldPoint;
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x000DD48C File Offset: 0x000DB88C
	private void On_SwipeEnd2Fingers(Gesture gesture)
	{
		float swipeOrDragAngle = gesture.GetSwipeOrDragAngle();
		this.swipeData.text = string.Concat(new object[]
		{
			"Last swipe : ",
			gesture.swipe.ToString(),
			" /  vector : ",
			gesture.swipeVector.normalized,
			" / angle : ",
			swipeOrDragAngle.ToString("f2")
		});
	}

	// Token: 0x04001D98 RID: 7576
	public GameObject trail;

	// Token: 0x04001D99 RID: 7577
	public Text swipeData;
}
