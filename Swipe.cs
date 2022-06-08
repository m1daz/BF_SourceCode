using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000375 RID: 885
public class Swipe : MonoBehaviour
{
	// Token: 0x06001B88 RID: 7048 RVA: 0x000DC6F4 File Offset: 0x000DAAF4
	private void OnEnable()
	{
		EasyTouch.On_SwipeStart += this.On_SwipeStart;
		EasyTouch.On_Swipe += this.On_Swipe;
		EasyTouch.On_SwipeEnd += this.On_SwipeEnd;
	}

	// Token: 0x06001B89 RID: 7049 RVA: 0x000DC729 File Offset: 0x000DAB29
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x000DC731 File Offset: 0x000DAB31
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x000DC739 File Offset: 0x000DAB39
	private void UnsubscribeEvent()
	{
		EasyTouch.On_SwipeStart -= this.On_SwipeStart;
		EasyTouch.On_Swipe -= this.On_Swipe;
		EasyTouch.On_SwipeEnd -= this.On_SwipeEnd;
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x000DC76E File Offset: 0x000DAB6E
	private void On_SwipeStart(Gesture gesture)
	{
		this.swipeText.text = "You start a swipe";
	}

	// Token: 0x06001B8D RID: 7053 RVA: 0x000DC780 File Offset: 0x000DAB80
	private void On_Swipe(Gesture gesture)
	{
		Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(5f);
		this.trail.transform.position = touchToWorldPoint;
	}

	// Token: 0x06001B8E RID: 7054 RVA: 0x000DC7AC File Offset: 0x000DABAC
	private void On_SwipeEnd(Gesture gesture)
	{
		float swipeOrDragAngle = gesture.GetSwipeOrDragAngle();
		this.swipeText.text = string.Concat(new object[]
		{
			"Last swipe : ",
			gesture.swipe.ToString(),
			" /  vector : ",
			gesture.swipeVector.normalized,
			" / angle : ",
			swipeOrDragAngle.ToString("f2")
		});
	}

	// Token: 0x04001D8D RID: 7565
	public GameObject trail;

	// Token: 0x04001D8E RID: 7566
	public Text swipeText;
}
