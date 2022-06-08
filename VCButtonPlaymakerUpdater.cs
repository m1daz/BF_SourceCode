using System;
using UnityEngine;

// Token: 0x020006BD RID: 1725
public class VCButtonPlaymakerUpdater : MonoBehaviour
{
	// Token: 0x060032DB RID: 13019 RVA: 0x00166218 File Offset: 0x00164618
	private void Start()
	{
		if (this.button == null)
		{
			this.button = base.gameObject.GetComponent<VCButtonBase>();
			if (this.button == null)
			{
				VCUtils.DestroyWithError(base.gameObject, "You must specify a button for VCButtonPlaymakerUpdater to function.  Destroying this object.");
				return;
			}
		}
	}

	// Token: 0x060032DC RID: 13020 RVA: 0x00166269 File Offset: 0x00164669
	private void Update()
	{
		this.pressed = this.button.Pressed;
		this.forcePressed = this.button.ForcePressed;
		this.holdTime = this.button.HoldTime;
	}

	// Token: 0x04002F3A RID: 12090
	public VCButtonBase button;

	// Token: 0x04002F3B RID: 12091
	public bool pressed;

	// Token: 0x04002F3C RID: 12092
	public bool forcePressed;

	// Token: 0x04002F3D RID: 12093
	public float holdTime;
}
