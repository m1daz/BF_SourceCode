using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000373 RID: 883
public class DragMe : MonoBehaviour
{
	// Token: 0x06001B74 RID: 7028 RVA: 0x000DC302 File Offset: 0x000DA702
	private void OnEnable()
	{
		EasyTouch.On_Drag += this.On_Drag;
		EasyTouch.On_DragStart += this.On_DragStart;
		EasyTouch.On_DragEnd += this.On_DragEnd;
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x000DC337 File Offset: 0x000DA737
	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B76 RID: 7030 RVA: 0x000DC33F File Offset: 0x000DA73F
	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x000DC347 File Offset: 0x000DA747
	private void UnsubscribeEvent()
	{
		EasyTouch.On_Drag -= this.On_Drag;
		EasyTouch.On_DragStart -= this.On_DragStart;
		EasyTouch.On_DragEnd -= this.On_DragEnd;
	}

	// Token: 0x06001B78 RID: 7032 RVA: 0x000DC37C File Offset: 0x000DA77C
	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x000DC3A8 File Offset: 0x000DA7A8
	private void On_DragStart(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.fingerIndex = gesture.fingerIndex;
			this.RandomColor();
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			this.deltaPosition = touchToWorldPoint - base.transform.position;
		}
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x000DC40C File Offset: 0x000DA80C
	private void On_Drag(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject && this.fingerIndex == gesture.fingerIndex)
		{
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			base.transform.position = touchToWorldPoint - this.deltaPosition;
			float swipeOrDragAngle = gesture.GetSwipeOrDragAngle();
			this.textMesh.text = swipeOrDragAngle.ToString("f2") + " / " + gesture.swipe.ToString();
		}
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x000DC4A8 File Offset: 0x000DA8A8
	private void On_DragEnd(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Drag me";
		}
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x000DC4F8 File Offset: 0x000DA8F8
	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	// Token: 0x04001D87 RID: 7559
	private TextMesh textMesh;

	// Token: 0x04001D88 RID: 7560
	private Color startColor;

	// Token: 0x04001D89 RID: 7561
	private Vector3 deltaPosition;

	// Token: 0x04001D8A RID: 7562
	private int fingerIndex;
}
