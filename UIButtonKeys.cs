using System;
using UnityEngine;

// Token: 0x02000560 RID: 1376
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Interaction/Button Keys (Legacy)")]
public class UIButtonKeys : UIKeyNavigation
{
	// Token: 0x06002679 RID: 9849 RVA: 0x0011D803 File Offset: 0x0011BC03
	protected override void OnEnable()
	{
		this.Upgrade();
		base.OnEnable();
	}

	// Token: 0x0600267A RID: 9850 RVA: 0x0011D814 File Offset: 0x0011BC14
	public void Upgrade()
	{
		if (this.onClick == null && this.selectOnClick != null)
		{
			this.onClick = this.selectOnClick.gameObject;
			this.selectOnClick = null;
			NGUITools.SetDirty(this, "last change");
		}
		if (this.onLeft == null && this.selectOnLeft != null)
		{
			this.onLeft = this.selectOnLeft.gameObject;
			this.selectOnLeft = null;
			NGUITools.SetDirty(this, "last change");
		}
		if (this.onRight == null && this.selectOnRight != null)
		{
			this.onRight = this.selectOnRight.gameObject;
			this.selectOnRight = null;
			NGUITools.SetDirty(this, "last change");
		}
		if (this.onUp == null && this.selectOnUp != null)
		{
			this.onUp = this.selectOnUp.gameObject;
			this.selectOnUp = null;
			NGUITools.SetDirty(this, "last change");
		}
		if (this.onDown == null && this.selectOnDown != null)
		{
			this.onDown = this.selectOnDown.gameObject;
			this.selectOnDown = null;
			NGUITools.SetDirty(this, "last change");
		}
	}

	// Token: 0x04002737 RID: 10039
	public UIButtonKeys selectOnClick;

	// Token: 0x04002738 RID: 10040
	public UIButtonKeys selectOnUp;

	// Token: 0x04002739 RID: 10041
	public UIButtonKeys selectOnDown;

	// Token: 0x0400273A RID: 10042
	public UIButtonKeys selectOnLeft;

	// Token: 0x0400273B RID: 10043
	public UIButtonKeys selectOnRight;
}
