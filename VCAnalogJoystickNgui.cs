using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006BA RID: 1722
public class VCAnalogJoystickNgui : VCAnalogJoystickBase
{
	// Token: 0x060032CB RID: 13003 RVA: 0x00165CEC File Offset: 0x001640EC
	protected override bool Init()
	{
		if (!VCPluginSettings.NguiEnabled(base.gameObject))
		{
			return false;
		}
		if (!base.Init())
		{
			return false;
		}
		if (this._collider == null)
		{
			VCUtils.DestroyWithError(base.gameObject, "No collider attached to colliderGameObject!  Destroying this gameObject.");
			return false;
		}
		this._movingPartSprite = this.movingPart.GetComponent<UISprite>();
		if (this._movingPartSprite == null)
		{
			this._movingPartSprite = this.movingPart.GetComponentInChildren<UISprite>();
		}
		return true;
	}

	// Token: 0x060032CC RID: 13004 RVA: 0x00165D70 File Offset: 0x00164170
	protected override void InitOriginValues()
	{
		this._touchOrigin = this._colliderCamera.WorldToViewportPoint(this.movingPart.transform.position);
		this._touchOriginScreen = this._colliderCamera.WorldToScreenPoint(this.movingPart.transform.position);
		this._movingPartOrigin = this.movingPart.transform.localPosition;
	}

	// Token: 0x060032CD RID: 13005 RVA: 0x00165DDA File Offset: 0x001641DA
	protected override bool Colliding(VCTouchWrapper tw)
	{
		return tw.Active && base.AABBContains(tw.position);
	}

	// Token: 0x060032CE RID: 13006 RVA: 0x00165DF8 File Offset: 0x001641F8
	protected override void ProcessTouch(VCTouchWrapper tw)
	{
		if (this.measureDeltaRelativeToCenter)
		{
			this._touchOrigin = this.movingPart.transform.position;
			this._touchOriginScreen = this._colliderCamera.WorldToScreenPoint(this.movingPart.transform.position);
		}
		else
		{
			this._touchOrigin = this._colliderCamera.ScreenToWorldPoint(tw.position);
			this._touchOriginScreen.x = tw.position.x;
			this._touchOriginScreen.y = tw.position.y;
		}
		if (this.positionAtTouchLocation)
		{
			float z = this.movingPart.transform.localPosition.z;
			this.basePart.transform.position = this._touchOrigin;
			this.movingPart.transform.position = this._touchOrigin;
			this._movingPartOrigin.Set(this.movingPart.transform.localPosition.x, this.movingPart.transform.localPosition.y, z);
		}
	}

	// Token: 0x060032CF RID: 13007 RVA: 0x00165F28 File Offset: 0x00164328
	protected override void SetVisible(bool visible, bool forceUpdate)
	{
		if (!forceUpdate && this._visible == visible)
		{
			return;
		}
		if (this._visibleBehaviourComponents == null)
		{
			this._visibleBehaviourComponents = new List<Behaviour>(this.basePart.GetComponentsInChildren<UISprite>());
		}
		foreach (Behaviour behaviour in this._visibleBehaviourComponents)
		{
			behaviour.enabled = visible;
		}
		this._movingPartVisible = (visible && !this.hideMovingPart);
		this._movingPartSprite.enabled = this._movingPartVisible;
		this._visible = visible;
	}

	// Token: 0x060032D0 RID: 13008 RVA: 0x00165FE8 File Offset: 0x001643E8
	protected override void SetPosition(GameObject go, Vector3 vec)
	{
		go.transform.localPosition = new Vector3(vec.x, vec.y, go.transform.localPosition.z);
	}

	// Token: 0x060032D1 RID: 13009 RVA: 0x00166026 File Offset: 0x00164426
	protected override void UpdateDelta()
	{
		base.UpdateDelta();
	}

	// Token: 0x04002F35 RID: 12085
	protected UISprite _movingPartSprite;
}
