using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000458 RID: 1112
[AddComponentMenu("FingerGestures/Toolbox/Drag")]
public class TBDrag : TBComponent
{
	// Token: 0x14000053 RID: 83
	// (add) Token: 0x0600204A RID: 8266 RVA: 0x000F22B8 File Offset: 0x000F06B8
	// (remove) Token: 0x0600204B RID: 8267 RVA: 0x000F22F0 File Offset: 0x000F06F0
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event TBComponent.EventHandler<TBDrag> OnDragBegin;

	// Token: 0x14000054 RID: 84
	// (add) Token: 0x0600204C RID: 8268 RVA: 0x000F2328 File Offset: 0x000F0728
	// (remove) Token: 0x0600204D RID: 8269 RVA: 0x000F2360 File Offset: 0x000F0760
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event TBComponent.EventHandler<TBDrag> OnDragMove;

	// Token: 0x14000055 RID: 85
	// (add) Token: 0x0600204E RID: 8270 RVA: 0x000F2398 File Offset: 0x000F0798
	// (remove) Token: 0x0600204F RID: 8271 RVA: 0x000F23D0 File Offset: 0x000F07D0
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event TBComponent.EventHandler<TBDrag> OnDragEnd;

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06002050 RID: 8272 RVA: 0x000F2406 File Offset: 0x000F0806
	// (set) Token: 0x06002051 RID: 8273 RVA: 0x000F2410 File Offset: 0x000F0810
	public bool Dragging
	{
		get
		{
			return this.dragging;
		}
		private set
		{
			if (this.dragging != value)
			{
				this.dragging = value;
				if (this.dragging)
				{
					FingerGestures.OnFingerDragMove += this.FingerGestures_OnDragMove;
					FingerGestures.OnFingerDragEnd += this.FingerGestures_OnDragEnd;
				}
				else
				{
					FingerGestures.OnFingerDragMove -= this.FingerGestures_OnDragMove;
					FingerGestures.OnFingerDragEnd -= this.FingerGestures_OnDragEnd;
				}
			}
		}
	}

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06002052 RID: 8274 RVA: 0x000F2484 File Offset: 0x000F0884
	// (set) Token: 0x06002053 RID: 8275 RVA: 0x000F248C File Offset: 0x000F088C
	public Vector2 MoveDelta
	{
		get
		{
			return this.moveDelta;
		}
		private set
		{
			this.moveDelta = value;
		}
	}

	// Token: 0x06002054 RID: 8276 RVA: 0x000F2498 File Offset: 0x000F0898
	public bool BeginDrag(int fingerIndex, Vector2 fingerPos)
	{
		if (this.Dragging)
		{
			return false;
		}
		base.FingerIndex = fingerIndex;
		base.FingerPos = fingerPos;
		this.Dragging = true;
		if (this.OnDragBegin != null)
		{
			this.OnDragBegin(this);
		}
		base.Send(this.dragBeginMessage);
		return true;
	}

	// Token: 0x06002055 RID: 8277 RVA: 0x000F24EC File Offset: 0x000F08EC
	public bool EndDrag()
	{
		if (!this.Dragging)
		{
			return false;
		}
		if (this.OnDragEnd != null)
		{
			this.OnDragEnd(this);
		}
		base.Send(this.dragEndMessage);
		this.Dragging = false;
		base.FingerIndex = -1;
		return true;
	}

	// Token: 0x06002056 RID: 8278 RVA: 0x000F253C File Offset: 0x000F093C
	private void FingerGestures_OnDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
	{
		if (this.Dragging && base.FingerIndex == fingerIndex)
		{
			base.FingerPos = fingerPos;
			this.MoveDelta = delta;
			if (this.OnDragMove != null)
			{
				this.OnDragMove(this);
			}
			base.Send(this.dragMoveMessage);
		}
	}

	// Token: 0x06002057 RID: 8279 RVA: 0x000F2592 File Offset: 0x000F0992
	private void FingerGestures_OnDragEnd(int fingerIndex, Vector2 fingerPos)
	{
		if (this.Dragging && base.FingerIndex == fingerIndex)
		{
			base.FingerPos = fingerPos;
			this.EndDrag();
		}
	}

	// Token: 0x06002058 RID: 8280 RVA: 0x000F25B9 File Offset: 0x000F09B9
	private void OnDisable()
	{
		if (this.Dragging)
		{
			this.EndDrag();
		}
	}

	// Token: 0x04002121 RID: 8481
	public TBComponent.Message dragBeginMessage = new TBComponent.Message("OnDragBegin");

	// Token: 0x04002122 RID: 8482
	public TBComponent.Message dragMoveMessage = new TBComponent.Message("OnDragMove", false);

	// Token: 0x04002123 RID: 8483
	public TBComponent.Message dragEndMessage = new TBComponent.Message("OnDragEnd");

	// Token: 0x04002127 RID: 8487
	private bool dragging;

	// Token: 0x04002128 RID: 8488
	private Vector2 moveDelta;
}
