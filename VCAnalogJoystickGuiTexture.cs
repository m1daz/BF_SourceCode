using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B8 RID: 1720
public class VCAnalogJoystickGuiTexture : VCAnalogJoystickBase
{
	// Token: 0x060032C0 RID: 12992 RVA: 0x00165862 File Offset: 0x00163C62
	protected override bool Init()
	{
		if (!base.Init())
		{
			return false;
		}
		this._movingPartGuiTexture = this.movingPart.GetComponent<GUITexture>();
		return true;
	}

	// Token: 0x060032C1 RID: 12993 RVA: 0x00165884 File Offset: 0x00163C84
	protected override void InitOriginValues()
	{
		this._touchOrigin = this.movingPart.transform.position;
		this._touchOriginScreen = new Vector2(this._touchOrigin.x * (float)Screen.width, this._touchOrigin.y * (float)Screen.height);
		this._movingPartOrigin = this.movingPart.transform.position;
	}

	// Token: 0x060032C2 RID: 12994 RVA: 0x001658EC File Offset: 0x00163CEC
	protected override void UpdateDelta()
	{
		base.UpdateDelta();
		this._movingPartOffset.x = this._deltaPixels.x / (float)Screen.width;
		this._movingPartOffset.y = this._deltaPixels.y / (float)Screen.height;
	}

	// Token: 0x060032C3 RID: 12995 RVA: 0x0016593C File Offset: 0x00163D3C
	protected override bool Colliding(VCTouchWrapper tw)
	{
		if (!tw.Active)
		{
			return false;
		}
		if (this._collider != null)
		{
			return base.AABBContains(tw.position);
		}
		Rect screenRect = this._movingPartGuiTexture.GetScreenRect();
		VCUtils.ScaleRect(ref screenRect, this.hitRectScale);
		return screenRect.Contains(tw.position);
	}

	// Token: 0x060032C4 RID: 12996 RVA: 0x0016599C File Offset: 0x00163D9C
	protected override void ProcessTouch(VCTouchWrapper tw)
	{
		if (this.measureDeltaRelativeToCenter)
		{
			this._touchOrigin = this.movingPart.transform.position;
			this._touchOriginScreen.x = this._touchOrigin.x * (float)Screen.width;
			this._touchOriginScreen.y = this._touchOrigin.y * (float)Screen.height;
		}
		else
		{
			this._touchOrigin.x = tw.position.x / (float)Screen.width;
			this._touchOrigin.y = tw.position.y / (float)Screen.height;
			this._touchOrigin.z = this.movingPart.transform.position.z;
			this._touchOriginScreen.x = tw.position.x;
			this._touchOriginScreen.y = tw.position.y;
		}
		if (this.positionAtTouchLocation)
		{
			this._movingPartOrigin = this._touchOrigin;
			this.basePart.transform.position = new Vector3(this._touchOrigin.x, this._touchOrigin.y, this.basePart.transform.position.z);
		}
	}

	// Token: 0x060032C5 RID: 12997 RVA: 0x00165AEC File Offset: 0x00163EEC
	protected override void SetVisible(bool visible, bool forceUpdate)
	{
		if (!forceUpdate && this._visible == visible)
		{
			return;
		}
		if (this._visibleBehaviourComponents == null)
		{
			this._visibleBehaviourComponents = new List<Behaviour>(this.basePart.GetComponentsInChildren<GUITexture>());
		}
		foreach (Behaviour behaviour in this._visibleBehaviourComponents)
		{
			behaviour.enabled = visible;
		}
		this._movingPartVisible = (visible && !this.hideMovingPart);
		this.movingPart.GetComponent<GUITexture>().enabled = this._movingPartVisible;
		this._visible = visible;
	}

	// Token: 0x04002F31 RID: 12081
	public Vector2 hitRectScale = new Vector2(1f, 1f);

	// Token: 0x04002F32 RID: 12082
	protected GUITexture _movingPartGuiTexture;
}
