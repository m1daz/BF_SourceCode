using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200045F RID: 1119
[AddComponentMenu("FingerGestures/Toolbox/Tap")]
public class TBTap : TBComponent
{
	// Token: 0x1400005A RID: 90
	// (add) Token: 0x06002081 RID: 8321 RVA: 0x000F2FF4 File Offset: 0x000F13F4
	// (remove) Token: 0x06002082 RID: 8322 RVA: 0x000F302C File Offset: 0x000F142C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event TBComponent.EventHandler<TBTap> OnTap;

	// Token: 0x06002083 RID: 8323 RVA: 0x000F3064 File Offset: 0x000F1464
	public bool RaiseTap(int fingerIndex, Vector2 fingerPos, int tapCount)
	{
		if (tapCount != this.tapCount)
		{
			return false;
		}
		base.FingerIndex = fingerIndex;
		base.FingerPos = fingerPos;
		if (this.OnTap != null)
		{
			this.OnTap(this);
		}
		base.Send(this.message);
		return true;
	}

	// Token: 0x0400214E RID: 8526
	public int tapCount = 1;

	// Token: 0x0400214F RID: 8527
	public TBComponent.Message message = new TBComponent.Message("OnTap");
}
