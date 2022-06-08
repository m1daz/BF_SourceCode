using System;
using UnityEngine;

// Token: 0x020006B9 RID: 1721
public class VCButtonGuiTexture : VCButtonWithBehaviours
{
	// Token: 0x060032C7 RID: 12999 RVA: 0x00165BD4 File Offset: 0x00163FD4
	protected override bool Init()
	{
		this._requireCollider = false;
		if (!base.Init())
		{
			return false;
		}
		if (this._collider == null)
		{
			this._colliderGuiTexture = this.colliderObject.GetComponent<GUITexture>();
			if (this._colliderGuiTexture == null)
			{
				VCUtils.DestroyWithError(base.gameObject, "There is no Collider attached to colliderObject, as well as no GUITexture, attach one or the other.  Destroying this control.");
				return false;
			}
		}
		return true;
	}

	// Token: 0x060032C8 RID: 13000 RVA: 0x00165C3C File Offset: 0x0016403C
	protected override void InitBehaviours()
	{
		if (this.upStateObject != null)
		{
			this._upBehaviour = this.upStateObject.GetComponent<GUITexture>();
		}
		if (this.pressedStateObject != null)
		{
			this._pressedBehavior = this.pressedStateObject.GetComponent<GUITexture>();
		}
	}

	// Token: 0x060032C9 RID: 13001 RVA: 0x00165C90 File Offset: 0x00164090
	protected override bool Colliding(VCTouchWrapper tw)
	{
		if (this._collider != null)
		{
			return base.AABBContains(tw.position);
		}
		Rect screenRect = this._colliderGuiTexture.GetScreenRect();
		VCUtils.ScaleRect(ref screenRect, this.hitRectScale);
		return screenRect.Contains(tw.position);
	}

	// Token: 0x04002F33 RID: 12083
	public Vector2 hitRectScale = new Vector2(1f, 1f);

	// Token: 0x04002F34 RID: 12084
	protected GUITexture _colliderGuiTexture;
}
