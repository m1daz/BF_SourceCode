using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x0200037C RID: 892
public class TwoDragMe : MonoBehaviour
{
	// Token: 0x06001BBD RID: 7101 RVA: 0x000DCF78 File Offset: 0x000DB378
	private void OnEnable()
	{
		EasyTouch.On_DragStart2Fingers += this.On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers += this.On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers += this.On_DragEnd2Fingers;
		EasyTouch.On_Cancel2Fingers += this.On_Cancel2Fingers;
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x000DCFC9 File Offset: 0x000DB3C9
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x000DCFD1 File Offset: 0x000DB3D1
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x000DCFDC File Offset: 0x000DB3DC
	private void UnsubscribeEvent()
	{
		EasyTouch.On_DragStart2Fingers -= this.On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers -= this.On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers -= this.On_DragEnd2Fingers;
		EasyTouch.On_Cancel2Fingers -= this.On_Cancel2Fingers;
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x000DD02D File Offset: 0x000DB42D
	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x000DD058 File Offset: 0x000DB458
	private void On_DragStart2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			this.deltaPosition = touchToWorldPoint - base.transform.position;
		}
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x000DD0B0 File Offset: 0x000DB4B0
	private void On_Drag2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			base.transform.position = touchToWorldPoint - this.deltaPosition;
			float swipeOrDragAngle = gesture.GetSwipeOrDragAngle();
			this.textMesh.text = swipeOrDragAngle.ToString("f2") + " / " + gesture.swipe.ToString();
		}
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x000DD13C File Offset: 0x000DB53C
	private void On_DragEnd2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Drag me";
		}
	}

	// Token: 0x06001BC5 RID: 7109 RVA: 0x000DD18A File Offset: 0x000DB58A
	private void On_Cancel2Fingers(Gesture gesture)
	{
		this.On_DragEnd2Fingers(gesture);
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x000DD194 File Offset: 0x000DB594
	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	// Token: 0x04001D93 RID: 7571
	private TextMesh textMesh;

	// Token: 0x04001D94 RID: 7572
	private Vector3 deltaPosition;

	// Token: 0x04001D95 RID: 7573
	private Color startColor;
}
