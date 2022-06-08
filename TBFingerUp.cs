using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200045A RID: 1114
[AddComponentMenu("FingerGestures/Toolbox/FingerUp")]
public class TBFingerUp : TBComponent
{
	// Token: 0x14000057 RID: 87
	// (add) Token: 0x0600205E RID: 8286 RVA: 0x000F26A4 File Offset: 0x000F0AA4
	// (remove) Token: 0x0600205F RID: 8287 RVA: 0x000F26DC File Offset: 0x000F0ADC
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event TBComponent.EventHandler<TBFingerUp> OnFingerUp;

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x06002060 RID: 8288 RVA: 0x000F2712 File Offset: 0x000F0B12
	// (set) Token: 0x06002061 RID: 8289 RVA: 0x000F271A File Offset: 0x000F0B1A
	public float TimeHeldDown
	{
		get
		{
			return this.timeHeldDown;
		}
		private set
		{
			this.timeHeldDown = value;
		}
	}

	// Token: 0x06002062 RID: 8290 RVA: 0x000F2723 File Offset: 0x000F0B23
	public bool RaiseFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
	{
		base.FingerIndex = fingerIndex;
		base.FingerPos = fingerPos;
		this.TimeHeldDown = timeHeldDown;
		if (this.OnFingerUp != null)
		{
			this.OnFingerUp(this);
		}
		base.Send(this.message);
		return true;
	}

	// Token: 0x0400212B RID: 8491
	public TBComponent.Message message = new TBComponent.Message("OnFingerUp");

	// Token: 0x0400212D RID: 8493
	private float timeHeldDown;
}
