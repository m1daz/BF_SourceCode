using System;
using UnityEngine;

// Token: 0x020002F8 RID: 760
public class UINewStoreFingerEvent : MonoBehaviour
{
	// Token: 0x0600176D RID: 5997 RVA: 0x000C61BC File Offset: 0x000C45BC
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

	// Token: 0x0600176E RID: 5998 RVA: 0x000C6284 File Offset: 0x000C4684
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

	// Token: 0x0600176F RID: 5999 RVA: 0x000C634C File Offset: 0x000C474C
	private void OnFingerDown(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x06001770 RID: 6000 RVA: 0x000C634E File Offset: 0x000C474E
	private void OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
	{
	}

	// Token: 0x06001771 RID: 6001 RVA: 0x000C6350 File Offset: 0x000C4750
	private void OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
	{
		this.prePos = fingerPos;
	}

	// Token: 0x06001772 RID: 6002 RVA: 0x000C6359 File Offset: 0x000C4759
	private void OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x06001773 RID: 6003 RVA: 0x000C635C File Offset: 0x000C475C
	private void OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
	{
		if (fingerPos.x < (float)Screen.width * 0.75f)
		{
			return;
		}
		base.transform.eulerAngles += new Vector3(0f, -(fingerPos.x - this.prePos.x) * 0.5f, 0f);
		this.prePos = fingerPos;
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x000C63C8 File Offset: 0x000C47C8
	private void OnFingerSwipe(int fingerIndex, Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity)
	{
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x000C63CA File Offset: 0x000C47CA
	private void OnFingerTap(int fingerIndex, Vector2 fingerPos, int tapCount)
	{
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x000C63CC File Offset: 0x000C47CC
	private void OnFingerStationaryBegin(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x06001777 RID: 6007 RVA: 0x000C63CE File Offset: 0x000C47CE
	private void OnFingerStationary(int fingerIndex, Vector2 fingerPos, float elapsedTime)
	{
	}

	// Token: 0x06001778 RID: 6008 RVA: 0x000C63D0 File Offset: 0x000C47D0
	private void OnFingerStationaryEnd(int fingerIndex, Vector2 fingerPos, float elapsedTime)
	{
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x000C63D2 File Offset: 0x000C47D2
	private void OnFingerLongPress(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x0600177A RID: 6010 RVA: 0x000C63D4 File Offset: 0x000C47D4
	private Vector3 GetWorldPos(Vector2 screenPos)
	{
		Camera main = Camera.main;
		return main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(base.transform.position.z - main.transform.position.z)));
	}

	// Token: 0x04001A71 RID: 6769
	private Vector2 prePos;
}
