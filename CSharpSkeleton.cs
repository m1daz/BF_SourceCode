using System;
using UnityEngine;

// Token: 0x02000450 RID: 1104
public class CSharpSkeleton : MonoBehaviour
{
	// Token: 0x06001FF4 RID: 8180 RVA: 0x000F15F8 File Offset: 0x000EF9F8
	private void OnEnable()
	{
		FingerGestures.OnFingerDown += this.FingerGestures_OnFingerDown;
		FingerGestures.OnFingerStationaryBegin += this.FingerGestures_OnFingerStationaryBegin;
		FingerGestures.OnFingerStationary += this.FingerGestures_OnFingerStationary;
		FingerGestures.OnFingerStationaryEnd += this.FingerGestures_OnFingerStationaryEnd;
		FingerGestures.OnFingerMoveBegin += this.FingerGestures_OnFingerMoveBegin;
		FingerGestures.OnFingerMove += this.FingerGestures_OnFingerMove;
		FingerGestures.OnFingerMoveEnd += this.FingerGestures_OnFingerMoveEnd;
		FingerGestures.OnFingerUp += this.FingerGestures_OnFingerUp;
		FingerGestures.OnFingerLongPress += this.FingerGestures_OnFingerLongPress;
		FingerGestures.OnFingerTap += this.FingerGestures_OnFingerTap;
		FingerGestures.OnFingerSwipe += this.FingerGestures_OnFingerSwipe;
		FingerGestures.OnFingerDragBegin += this.FingerGestures_OnFingerDragBegin;
		FingerGestures.OnFingerDragMove += this.FingerGestures_OnFingerDragMove;
		FingerGestures.OnFingerDragEnd += this.FingerGestures_OnFingerDragEnd;
		FingerGestures.OnLongPress += this.FingerGestures_OnLongPress;
		FingerGestures.OnTap += this.FingerGestures_OnTap;
		FingerGestures.OnSwipe += this.FingerGestures_OnSwipe;
		FingerGestures.OnDragBegin += this.FingerGestures_OnDragBegin;
		FingerGestures.OnDragMove += this.FingerGestures_OnDragMove;
		FingerGestures.OnDragEnd += this.FingerGestures_OnDragEnd;
		FingerGestures.OnPinchBegin += this.FingerGestures_OnPinchBegin;
		FingerGestures.OnPinchMove += this.FingerGestures_OnPinchMove;
		FingerGestures.OnPinchEnd += this.FingerGestures_OnPinchEnd;
		FingerGestures.OnRotationBegin += this.FingerGestures_OnRotationBegin;
		FingerGestures.OnRotationMove += this.FingerGestures_OnRotationMove;
		FingerGestures.OnRotationEnd += this.FingerGestures_OnRotationEnd;
		FingerGestures.OnTwoFingerLongPress += this.FingerGestures_OnTwoFingerLongPress;
		FingerGestures.OnTwoFingerTap += this.FingerGestures_OnTwoFingerTap;
		FingerGestures.OnTwoFingerSwipe += this.FingerGestures_OnTwoFingerSwipe;
		FingerGestures.OnTwoFingerDragBegin += this.FingerGestures_OnTwoFingerDragBegin;
		FingerGestures.OnTwoFingerDragMove += this.FingerGestures_OnTwoFingerDragMove;
		FingerGestures.OnTwoFingerDragEnd += this.FingerGestures_OnTwoFingerDragEnd;
	}

	// Token: 0x06001FF5 RID: 8181 RVA: 0x000F1828 File Offset: 0x000EFC28
	private void OnDisable()
	{
		FingerGestures.OnFingerDown -= this.FingerGestures_OnFingerDown;
		FingerGestures.OnFingerStationaryBegin -= this.FingerGestures_OnFingerStationaryBegin;
		FingerGestures.OnFingerStationary -= this.FingerGestures_OnFingerStationary;
		FingerGestures.OnFingerStationaryEnd -= this.FingerGestures_OnFingerStationaryEnd;
		FingerGestures.OnFingerMoveBegin -= this.FingerGestures_OnFingerMoveBegin;
		FingerGestures.OnFingerMove -= this.FingerGestures_OnFingerMove;
		FingerGestures.OnFingerMoveEnd -= this.FingerGestures_OnFingerMoveEnd;
		FingerGestures.OnFingerUp -= this.FingerGestures_OnFingerUp;
		FingerGestures.OnFingerLongPress -= this.FingerGestures_OnFingerLongPress;
		FingerGestures.OnFingerTap -= this.FingerGestures_OnFingerTap;
		FingerGestures.OnFingerSwipe -= this.FingerGestures_OnFingerSwipe;
		FingerGestures.OnFingerDragBegin -= this.FingerGestures_OnFingerDragBegin;
		FingerGestures.OnFingerDragMove -= this.FingerGestures_OnFingerDragMove;
		FingerGestures.OnFingerDragEnd -= this.FingerGestures_OnFingerDragEnd;
		FingerGestures.OnLongPress -= this.FingerGestures_OnLongPress;
		FingerGestures.OnTap -= this.FingerGestures_OnTap;
		FingerGestures.OnSwipe -= this.FingerGestures_OnSwipe;
		FingerGestures.OnDragBegin -= this.FingerGestures_OnDragBegin;
		FingerGestures.OnDragMove -= this.FingerGestures_OnDragMove;
		FingerGestures.OnDragEnd -= this.FingerGestures_OnDragEnd;
		FingerGestures.OnPinchBegin -= this.FingerGestures_OnPinchBegin;
		FingerGestures.OnPinchMove -= this.FingerGestures_OnPinchMove;
		FingerGestures.OnPinchEnd -= this.FingerGestures_OnPinchEnd;
		FingerGestures.OnRotationBegin -= this.FingerGestures_OnRotationBegin;
		FingerGestures.OnRotationMove -= this.FingerGestures_OnRotationMove;
		FingerGestures.OnRotationEnd -= this.FingerGestures_OnRotationEnd;
		FingerGestures.OnTwoFingerLongPress -= this.FingerGestures_OnTwoFingerLongPress;
		FingerGestures.OnTwoFingerTap -= this.FingerGestures_OnTwoFingerTap;
		FingerGestures.OnTwoFingerSwipe -= this.FingerGestures_OnTwoFingerSwipe;
		FingerGestures.OnTwoFingerDragBegin -= this.FingerGestures_OnTwoFingerDragBegin;
		FingerGestures.OnTwoFingerDragMove -= this.FingerGestures_OnTwoFingerDragMove;
		FingerGestures.OnTwoFingerDragEnd -= this.FingerGestures_OnTwoFingerDragEnd;
	}

	// Token: 0x06001FF6 RID: 8182 RVA: 0x000F1A55 File Offset: 0x000EFE55
	private void FingerGestures_OnFingerDown(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x06001FF7 RID: 8183 RVA: 0x000F1A57 File Offset: 0x000EFE57
	private void FingerGestures_OnFingerMoveBegin(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x06001FF8 RID: 8184 RVA: 0x000F1A59 File Offset: 0x000EFE59
	private void FingerGestures_OnFingerMove(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x06001FF9 RID: 8185 RVA: 0x000F1A5B File Offset: 0x000EFE5B
	private void FingerGestures_OnFingerMoveEnd(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x06001FFA RID: 8186 RVA: 0x000F1A5D File Offset: 0x000EFE5D
	private void FingerGestures_OnFingerStationaryBegin(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x06001FFB RID: 8187 RVA: 0x000F1A5F File Offset: 0x000EFE5F
	private void FingerGestures_OnFingerStationary(int fingerIndex, Vector2 fingerPos, float elapsedTime)
	{
	}

	// Token: 0x06001FFC RID: 8188 RVA: 0x000F1A61 File Offset: 0x000EFE61
	private void FingerGestures_OnFingerStationaryEnd(int fingerIndex, Vector2 fingerPos, float elapsedTime)
	{
	}

	// Token: 0x06001FFD RID: 8189 RVA: 0x000F1A63 File Offset: 0x000EFE63
	private void FingerGestures_OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
	{
	}

	// Token: 0x06001FFE RID: 8190 RVA: 0x000F1A65 File Offset: 0x000EFE65
	private void FingerGestures_OnFingerLongPress(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x06001FFF RID: 8191 RVA: 0x000F1A67 File Offset: 0x000EFE67
	private void FingerGestures_OnFingerTap(int fingerIndex, Vector2 fingerPos, int tapCount)
	{
	}

	// Token: 0x06002000 RID: 8192 RVA: 0x000F1A69 File Offset: 0x000EFE69
	private void FingerGestures_OnFingerSwipe(int fingerIndex, Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity)
	{
	}

	// Token: 0x06002001 RID: 8193 RVA: 0x000F1A6B File Offset: 0x000EFE6B
	private void FingerGestures_OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
	{
	}

	// Token: 0x06002002 RID: 8194 RVA: 0x000F1A6D File Offset: 0x000EFE6D
	private void FingerGestures_OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
	{
	}

	// Token: 0x06002003 RID: 8195 RVA: 0x000F1A6F File Offset: 0x000EFE6F
	private void FingerGestures_OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
	{
	}

	// Token: 0x06002004 RID: 8196 RVA: 0x000F1A71 File Offset: 0x000EFE71
	private void FingerGestures_OnLongPress(Vector2 fingerPos)
	{
	}

	// Token: 0x06002005 RID: 8197 RVA: 0x000F1A73 File Offset: 0x000EFE73
	private void FingerGestures_OnTap(Vector2 fingerPos, int tapCount)
	{
	}

	// Token: 0x06002006 RID: 8198 RVA: 0x000F1A75 File Offset: 0x000EFE75
	private void FingerGestures_OnSwipe(Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity)
	{
	}

	// Token: 0x06002007 RID: 8199 RVA: 0x000F1A77 File Offset: 0x000EFE77
	private void FingerGestures_OnDragBegin(Vector2 fingerPos, Vector2 startPos)
	{
	}

	// Token: 0x06002008 RID: 8200 RVA: 0x000F1A79 File Offset: 0x000EFE79
	private void FingerGestures_OnDragMove(Vector2 fingerPos, Vector2 delta)
	{
	}

	// Token: 0x06002009 RID: 8201 RVA: 0x000F1A7B File Offset: 0x000EFE7B
	private void FingerGestures_OnDragEnd(Vector2 fingerPos)
	{
	}

	// Token: 0x0600200A RID: 8202 RVA: 0x000F1A7D File Offset: 0x000EFE7D
	private void FingerGestures_OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
	{
	}

	// Token: 0x0600200B RID: 8203 RVA: 0x000F1A7F File Offset: 0x000EFE7F
	private void FingerGestures_OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
	{
	}

	// Token: 0x0600200C RID: 8204 RVA: 0x000F1A81 File Offset: 0x000EFE81
	private void FingerGestures_OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
	{
	}

	// Token: 0x0600200D RID: 8205 RVA: 0x000F1A83 File Offset: 0x000EFE83
	private void FingerGestures_OnRotationBegin(Vector2 fingerPos1, Vector2 fingerPos2)
	{
	}

	// Token: 0x0600200E RID: 8206 RVA: 0x000F1A85 File Offset: 0x000EFE85
	private void FingerGestures_OnRotationMove(Vector2 fingerPos1, Vector2 fingerPos2, float rotationAngleDelta)
	{
	}

	// Token: 0x0600200F RID: 8207 RVA: 0x000F1A87 File Offset: 0x000EFE87
	private void FingerGestures_OnRotationEnd(Vector2 fingerPos1, Vector2 fingerPos2, float totalRotationAngle)
	{
	}

	// Token: 0x06002010 RID: 8208 RVA: 0x000F1A89 File Offset: 0x000EFE89
	private void FingerGestures_OnTwoFingerLongPress(Vector2 fingerPos)
	{
	}

	// Token: 0x06002011 RID: 8209 RVA: 0x000F1A8B File Offset: 0x000EFE8B
	private void FingerGestures_OnTwoFingerTap(Vector2 fingerPos, int tapCount)
	{
	}

	// Token: 0x06002012 RID: 8210 RVA: 0x000F1A8D File Offset: 0x000EFE8D
	private void FingerGestures_OnTwoFingerSwipe(Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity)
	{
	}

	// Token: 0x06002013 RID: 8211 RVA: 0x000F1A8F File Offset: 0x000EFE8F
	private void FingerGestures_OnTwoFingerDragBegin(Vector2 fingerPos, Vector2 startPos)
	{
	}

	// Token: 0x06002014 RID: 8212 RVA: 0x000F1A91 File Offset: 0x000EFE91
	private void FingerGestures_OnTwoFingerDragMove(Vector2 fingerPos, Vector2 delta)
	{
	}

	// Token: 0x06002015 RID: 8213 RVA: 0x000F1A93 File Offset: 0x000EFE93
	private void FingerGestures_OnTwoFingerDragEnd(Vector2 fingerPos)
	{
	}
}
