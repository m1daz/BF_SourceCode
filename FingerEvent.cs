using System;
using UnityEngine;

// Token: 0x02000439 RID: 1081
public class FingerEvent : MonoBehaviour
{
	// Token: 0x06001F54 RID: 8020 RVA: 0x000EEE30 File Offset: 0x000ED230
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

	// Token: 0x06001F55 RID: 8021 RVA: 0x000EEEF8 File Offset: 0x000ED2F8
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

	// Token: 0x06001F56 RID: 8022 RVA: 0x000EEFC0 File Offset: 0x000ED3C0
	private void OnFingerDown(int fingerIndex, Vector2 fingerPos)
	{
		base.transform.position = this.GetWorldPos(fingerPos);
		Debug.Log(" OnFingerDown =" + fingerPos);
	}

	// Token: 0x06001F57 RID: 8023 RVA: 0x000EEFE9 File Offset: 0x000ED3E9
	private void OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
	{
		Debug.Log(" OnFingerUp =" + fingerPos);
	}

	// Token: 0x06001F58 RID: 8024 RVA: 0x000EF000 File Offset: 0x000ED400
	private void OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnFingerDragBegin fingerIndex =",
			fingerIndex,
			" fingerPos =",
			fingerPos,
			"startPos =",
			startPos
		}));
	}

	// Token: 0x06001F59 RID: 8025 RVA: 0x000EF050 File Offset: 0x000ED450
	private void OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnFingerDragEnd fingerIndex =",
			fingerIndex,
			" fingerPos =",
			fingerPos
		}));
	}

	// Token: 0x06001F5A RID: 8026 RVA: 0x000EF084 File Offset: 0x000ED484
	private void OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
	{
		base.transform.position = this.GetWorldPos(fingerPos);
		Debug.Log(" OnFingerDragMove =" + fingerPos);
	}

	// Token: 0x06001F5B RID: 8027 RVA: 0x000EF0AD File Offset: 0x000ED4AD
	private void OnFingerSwipe(int fingerIndex, Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnFingerSwipe ",
			direction,
			" with finger ",
			fingerIndex
		}));
	}

	// Token: 0x06001F5C RID: 8028 RVA: 0x000EF0E1 File Offset: 0x000ED4E1
	private void OnFingerTap(int fingerIndex, Vector2 fingerPos, int tapCount)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnFingerTap ",
			tapCount,
			" times with finger ",
			fingerIndex
		}));
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x000EF115 File Offset: 0x000ED515
	private void OnFingerStationaryBegin(int fingerIndex, Vector2 fingerPos)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnFingerStationaryBegin ",
			fingerPos,
			" times with finger ",
			fingerIndex
		}));
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x000EF149 File Offset: 0x000ED549
	private void OnFingerStationary(int fingerIndex, Vector2 fingerPos, float elapsedTime)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnFingerStationary ",
			fingerPos,
			" times with finger ",
			fingerIndex
		}));
	}

	// Token: 0x06001F5F RID: 8031 RVA: 0x000EF17D File Offset: 0x000ED57D
	private void OnFingerStationaryEnd(int fingerIndex, Vector2 fingerPos, float elapsedTime)
	{
		Debug.Log(string.Concat(new object[]
		{
			"OnFingerStationaryEnd ",
			fingerPos,
			" times with finger ",
			fingerIndex
		}));
	}

	// Token: 0x06001F60 RID: 8032 RVA: 0x000EF1B1 File Offset: 0x000ED5B1
	private void OnFingerLongPress(int fingerIndex, Vector2 fingerPos)
	{
		Debug.Log("OnFingerLongPress " + fingerPos);
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x000EF1C8 File Offset: 0x000ED5C8
	private Vector3 GetWorldPos(Vector2 screenPos)
	{
		Camera main = Camera.main;
		return main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(base.transform.position.z - main.transform.position.z)));
	}
}
