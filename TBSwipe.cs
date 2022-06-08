using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200045E RID: 1118
[AddComponentMenu("FingerGestures/Toolbox/Swipe")]
public class TBSwipe : TBComponent
{
	// Token: 0x14000059 RID: 89
	// (add) Token: 0x06002077 RID: 8311 RVA: 0x000F2E58 File Offset: 0x000F1258
	// (remove) Token: 0x06002078 RID: 8312 RVA: 0x000F2E90 File Offset: 0x000F1290
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event TBComponent.EventHandler<TBSwipe> OnSwipe;

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x06002079 RID: 8313 RVA: 0x000F2EC6 File Offset: 0x000F12C6
	// (set) Token: 0x0600207A RID: 8314 RVA: 0x000F2ECE File Offset: 0x000F12CE
	public FingerGestures.SwipeDirection Direction
	{
		get
		{
			return this.direction;
		}
		protected set
		{
			this.direction = value;
		}
	}

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x0600207B RID: 8315 RVA: 0x000F2ED7 File Offset: 0x000F12D7
	// (set) Token: 0x0600207C RID: 8316 RVA: 0x000F2EDF File Offset: 0x000F12DF
	public float Velocity
	{
		get
		{
			return this.velocity;
		}
		protected set
		{
			this.velocity = value;
		}
	}

	// Token: 0x0600207D RID: 8317 RVA: 0x000F2EE8 File Offset: 0x000F12E8
	public bool IsValid(FingerGestures.SwipeDirection direction)
	{
		if (direction == FingerGestures.SwipeDirection.Left)
		{
			return this.swipeLeft;
		}
		if (direction == FingerGestures.SwipeDirection.Right)
		{
			return this.swipeRight;
		}
		if (direction == FingerGestures.SwipeDirection.Up)
		{
			return this.swipeUp;
		}
		return direction == FingerGestures.SwipeDirection.Down && this.swipeDown;
	}

	// Token: 0x0600207E RID: 8318 RVA: 0x000F2F23 File Offset: 0x000F1323
	private TBComponent.Message GetMessageForSwipeDirection(FingerGestures.SwipeDirection direction)
	{
		if (direction == FingerGestures.SwipeDirection.Left)
		{
			return this.swipeLeftMessage;
		}
		if (direction == FingerGestures.SwipeDirection.Right)
		{
			return this.swipeRightMessage;
		}
		if (direction == FingerGestures.SwipeDirection.Up)
		{
			return this.swipeUpMessage;
		}
		return this.swipeDownMessage;
	}

	// Token: 0x0600207F RID: 8319 RVA: 0x000F2F58 File Offset: 0x000F1358
	public bool RaiseSwipe(int fingerIndex, Vector2 fingerPos, FingerGestures.SwipeDirection direction, float velocity)
	{
		if (velocity < this.minVelocity)
		{
			return false;
		}
		if (!this.IsValid(direction))
		{
			return false;
		}
		base.FingerIndex = fingerIndex;
		base.FingerPos = fingerPos;
		this.Direction = direction;
		this.Velocity = velocity;
		if (this.OnSwipe != null)
		{
			this.OnSwipe(this);
		}
		base.Send(this.swipeMessage);
		base.Send(this.GetMessageForSwipeDirection(direction));
		return true;
	}

	// Token: 0x04002141 RID: 8513
	public bool swipeLeft = true;

	// Token: 0x04002142 RID: 8514
	public bool swipeRight = true;

	// Token: 0x04002143 RID: 8515
	public bool swipeUp = true;

	// Token: 0x04002144 RID: 8516
	public bool swipeDown = true;

	// Token: 0x04002145 RID: 8517
	public float minVelocity;

	// Token: 0x04002146 RID: 8518
	public TBComponent.Message swipeMessage = new TBComponent.Message("OnSwipe");

	// Token: 0x04002147 RID: 8519
	public TBComponent.Message swipeLeftMessage = new TBComponent.Message("OnSwipeLeft", false);

	// Token: 0x04002148 RID: 8520
	public TBComponent.Message swipeRightMessage = new TBComponent.Message("OnSwipeRight", false);

	// Token: 0x04002149 RID: 8521
	public TBComponent.Message swipeUpMessage = new TBComponent.Message("OnSwipeUp", false);

	// Token: 0x0400214A RID: 8522
	public TBComponent.Message swipeDownMessage = new TBComponent.Message("OnSwipeDown", false);

	// Token: 0x0400214C RID: 8524
	private FingerGestures.SwipeDirection direction;

	// Token: 0x0400214D RID: 8525
	private float velocity;
}
