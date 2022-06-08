using System;
using UnityEngine;

// Token: 0x020006BE RID: 1726
public class VCDPadPlaymakerUpdater : MonoBehaviour
{
	// Token: 0x060032DE RID: 13022 RVA: 0x001662A8 File Offset: 0x001646A8
	private void Start()
	{
		if (this.dpad == null)
		{
			this.dpad = base.gameObject.GetComponent<VCDPadBase>();
			if (this.dpad == null)
			{
				VCUtils.DestroyWithError(base.gameObject, "You must specify a button for VCDPadPlaymakerUpdater to function.  Destroying this object.");
				return;
			}
		}
	}

	// Token: 0x060032DF RID: 13023 RVA: 0x001662FC File Offset: 0x001646FC
	private void Update()
	{
		this.up = this.dpad.Up;
		this.down = this.dpad.Down;
		this.left = this.dpad.Left;
		this.right = this.dpad.Right;
	}

	// Token: 0x04002F3E RID: 12094
	public VCDPadBase dpad;

	// Token: 0x04002F3F RID: 12095
	public bool up;

	// Token: 0x04002F40 RID: 12096
	public bool down;

	// Token: 0x04002F41 RID: 12097
	public bool left;

	// Token: 0x04002F42 RID: 12098
	public bool right;
}
