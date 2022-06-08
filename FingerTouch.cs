using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class FingerTouch : MonoBehaviour
{
	// Token: 0x06001B61 RID: 7009 RVA: 0x000DBF1C File Offset: 0x000DA31C
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchUp += this.On_TouchUp;
		EasyTouch.On_Swipe += this.On_Swipe;
		EasyTouch.On_Drag += this.On_Drag;
		EasyTouch.On_DoubleTap += this.On_DoubleTap;
		this.textMesh = base.GetComponentInChildren<TextMesh>();
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x000DBF8C File Offset: 0x000DA38C
	private void OnDestroy()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_Swipe -= this.On_Swipe;
		EasyTouch.On_Drag -= this.On_Drag;
		EasyTouch.On_DoubleTap -= this.On_DoubleTap;
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x000DBFF0 File Offset: 0x000DA3F0
	private void On_Drag(Gesture gesture)
	{
		if (gesture.pickedObject.transform.IsChildOf(base.gameObject.transform) && this.fingerId == gesture.fingerIndex)
		{
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			base.transform.position = touchToWorldPoint - this.deltaPosition;
		}
	}

	// Token: 0x06001B64 RID: 7012 RVA: 0x000DC05C File Offset: 0x000DA45C
	private void On_Swipe(Gesture gesture)
	{
		if (this.fingerId == gesture.fingerIndex)
		{
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(base.transform.position);
			base.transform.position = touchToWorldPoint - this.deltaPosition;
		}
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x000DC0A4 File Offset: 0x000DA4A4
	private void On_TouchStart(Gesture gesture)
	{
		if (gesture.pickedObject != null && gesture.pickedObject.transform.IsChildOf(base.gameObject.transform))
		{
			this.fingerId = gesture.fingerIndex;
			this.textMesh.text = this.fingerId.ToString();
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			this.deltaPosition = touchToWorldPoint - base.transform.position;
		}
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x000DC138 File Offset: 0x000DA538
	private void On_TouchUp(Gesture gesture)
	{
		if (gesture.fingerIndex == this.fingerId)
		{
			this.fingerId = -1;
			this.textMesh.text = string.Empty;
		}
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x000DC162 File Offset: 0x000DA562
	public void InitTouch(int ind)
	{
		this.fingerId = ind;
		this.textMesh.text = this.fingerId.ToString();
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x000DC188 File Offset: 0x000DA588
	private void On_DoubleTap(Gesture gesture)
	{
		if (gesture.pickedObject != null && gesture.pickedObject.transform.IsChildOf(base.gameObject.transform))
		{
			UnityEngine.Object.DestroyImmediate(base.transform.gameObject);
		}
	}

	// Token: 0x04001D83 RID: 7555
	private TextMesh textMesh;

	// Token: 0x04001D84 RID: 7556
	public Vector3 deltaPosition = Vector2.zero;

	// Token: 0x04001D85 RID: 7557
	public int fingerId = -1;
}
