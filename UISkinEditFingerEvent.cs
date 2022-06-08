using System;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class UISkinEditFingerEvent : MonoBehaviour
{
	// Token: 0x060016A3 RID: 5795 RVA: 0x000C13EC File Offset: 0x000BF7EC
	private void OnEnable()
	{
		FingerGestures.OnFingerDown += this.OnFingerDown;
		FingerGestures.OnFingerUp += this.OnFingerUp;
		FingerGestures.OnFingerDragBegin += this.OnFingerDragBegin;
		FingerGestures.OnFingerDragMove += this.OnFingerDragMove;
		FingerGestures.OnFingerDragEnd += this.OnFingerDragEnd;
		FingerGestures.OnFingerSwipe += this.OnFingerSwipe;
		FingerGestures.OnFingerTap += this.OnFingerTap;
		FingerGestures.OnFingerStationaryBegin += this.OnFingerStationaryBegin;
		FingerGestures.OnFingerStationary += this.OnFingerStationary;
		FingerGestures.OnFingerStationaryEnd += this.OnFingerStationaryEnd;
		FingerGestures.OnFingerLongPress += this.OnFingerLongPress;
	}

	// Token: 0x060016A4 RID: 5796 RVA: 0x000C14B4 File Offset: 0x000BF8B4
	private void OnDisable()
	{
		FingerGestures.OnFingerDown -= this.OnFingerDown;
		FingerGestures.OnFingerUp -= this.OnFingerUp;
		FingerGestures.OnFingerDragBegin -= this.OnFingerDragBegin;
		FingerGestures.OnFingerDragMove -= this.OnFingerDragMove;
		FingerGestures.OnFingerDragEnd -= this.OnFingerDragEnd;
		FingerGestures.OnFingerSwipe -= this.OnFingerSwipe;
		FingerGestures.OnFingerTap -= this.OnFingerTap;
		FingerGestures.OnFingerStationaryBegin -= this.OnFingerStationaryBegin;
		FingerGestures.OnFingerStationary -= this.OnFingerStationary;
		FingerGestures.OnFingerStationaryEnd -= this.OnFingerStationaryEnd;
		FingerGestures.OnFingerLongPress -= this.OnFingerLongPress;
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x000C157C File Offset: 0x000BF97C
	private void OnFingerDown(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x000C157E File Offset: 0x000BF97E
	private void OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
	{
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x000C1580 File Offset: 0x000BF980
	private void OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
	{
		this.prePos = fingerPos;
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x000C1589 File Offset: 0x000BF989
	private void OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x000C158C File Offset: 0x000BF98C
	private void OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
	{
		base.transform.eulerAngles += new Vector3(0f, -(fingerPos.x - this.prePos.x) * 0.5f, 0f);
		this.prePos = fingerPos;
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x000C15DF File Offset: 0x000BF9DF
	private void OnFingerSwipe(int fingerIndex, Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity)
	{
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x000C15E1 File Offset: 0x000BF9E1
	private void OnFingerTap(int fingerIndex, Vector2 fingerPos, int tapCount)
	{
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x000C15E3 File Offset: 0x000BF9E3
	private void OnFingerStationaryBegin(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x000C15E5 File Offset: 0x000BF9E5
	private void OnFingerStationary(int fingerIndex, Vector2 fingerPos, float elapsedTime)
	{
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x000C15E7 File Offset: 0x000BF9E7
	private void OnFingerStationaryEnd(int fingerIndex, Vector2 fingerPos, float elapsedTime)
	{
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x000C15E9 File Offset: 0x000BF9E9
	private void OnFingerLongPress(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x000C15EC File Offset: 0x000BF9EC
	private Vector3 GetWorldPos(Vector2 screenPos)
	{
		Camera main = Camera.main;
		return main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(base.transform.position.z - main.transform.position.z)));
	}

	// Token: 0x04001978 RID: 6520
	private Vector2 prePos;
}
