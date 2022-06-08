using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000459 RID: 1113
[AddComponentMenu("FingerGestures/Toolbox/FingerDown")]
public class TBFingerDown : TBComponent
{
	// Token: 0x14000056 RID: 86
	// (add) Token: 0x0600205A RID: 8282 RVA: 0x000F25E8 File Offset: 0x000F09E8
	// (remove) Token: 0x0600205B RID: 8283 RVA: 0x000F2620 File Offset: 0x000F0A20
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event TBComponent.EventHandler<TBFingerDown> OnFingerDown;

	// Token: 0x0600205C RID: 8284 RVA: 0x000F2656 File Offset: 0x000F0A56
	public bool RaiseFingerDown(int fingerIndex, Vector2 fingerPos)
	{
		base.FingerIndex = fingerIndex;
		base.FingerPos = fingerPos;
		if (this.OnFingerDown != null)
		{
			this.OnFingerDown(this);
		}
		base.Send(this.message);
		return true;
	}

	// Token: 0x04002129 RID: 8489
	public TBComponent.Message message = new TBComponent.Message("OnFingerDown");
}
