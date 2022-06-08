using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200045D RID: 1117
[AddComponentMenu("FingerGestures/Toolbox/LongPress")]
public class TBLongPress : TBComponent
{
	// Token: 0x14000058 RID: 88
	// (add) Token: 0x06002073 RID: 8307 RVA: 0x000F2D30 File Offset: 0x000F1130
	// (remove) Token: 0x06002074 RID: 8308 RVA: 0x000F2D68 File Offset: 0x000F1168
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event TBComponent.EventHandler<TBLongPress> OnLongPress;

	// Token: 0x06002075 RID: 8309 RVA: 0x000F2D9E File Offset: 0x000F119E
	public bool RaiseLongPress(int fingerIndex, Vector2 fingerPos)
	{
		base.FingerIndex = fingerIndex;
		base.FingerPos = fingerPos;
		if (this.OnLongPress != null)
		{
			this.OnLongPress(this);
		}
		base.Send(this.message);
		return true;
	}

	// Token: 0x0400213F RID: 8511
	public TBComponent.Message message = new TBComponent.Message("OnLongPress");
}
