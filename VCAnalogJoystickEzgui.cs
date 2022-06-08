using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B4 RID: 1716
public class VCAnalogJoystickEzgui : VCAnalogJoystickBase
{
	// Token: 0x060032A0 RID: 12960 RVA: 0x00164E40 File Offset: 0x00163240
	protected override bool Init()
	{
		if (!VCPluginSettings.EzguiEnabled(base.gameObject))
		{
			return false;
		}
		if (!base.Init())
		{
			return false;
		}
		if (this.colliderObject == this.movingPart)
		{
			Debug.LogWarning("VCAnalogJoystickEzgui may not behave properly when the colliderObject is the same as the movingPart! You should add a Collider to a gameObject independent from the EZGUI UI components.");
		}
		this._movingPartBehaviorComponent = this.GetEzguiBehavior(this.movingPart);
		if (this._movingPartBehaviorComponent == null)
		{
			VCUtils.DestroyWithError(base.gameObject, "Cannot find a SimpleSprite or UIButton component on movingPart.  Destroying this control.");
			return false;
		}
		if (this._collider == null)
		{
			VCUtils.DestroyWithError(base.gameObject, "No collider attached to colliderGameObject!  Destroying this control.");
			return false;
		}
		return true;
	}

	// Token: 0x060032A1 RID: 12961 RVA: 0x00164EE8 File Offset: 0x001632E8
	protected override void InitOriginValues()
	{
		this._touchOrigin = this._colliderCamera.WorldToViewportPoint(this.movingPart.transform.position);
		this._touchOriginScreen = this._colliderCamera.WorldToScreenPoint(this.movingPart.transform.position);
		this._movingPartOrigin = this.movingPart.transform.localPosition;
	}

	// Token: 0x060032A2 RID: 12962 RVA: 0x00164F52 File Offset: 0x00163352
	protected override bool Colliding(VCTouchWrapper tw)
	{
		return tw.Active && base.AABBContains(tw.position);
	}

	// Token: 0x060032A3 RID: 12963 RVA: 0x00164F70 File Offset: 0x00163370
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
			this._touchOrigin.Set(this._touchOrigin.x, this._touchOrigin.y, this.basePart.transform.position.z);
			this._touchOriginScreen.x = tw.position.x;
			this._touchOriginScreen.y = tw.position.y;
		}
		if (this.positionAtTouchLocation)
		{
			float z = this.movingPart.transform.localPosition.z;
			this.basePart.transform.localPosition = this._touchOrigin;
			this.movingPart.transform.position = this._touchOrigin;
			this._movingPartOrigin.Set(this.movingPart.transform.localPosition.x, this.movingPart.transform.localPosition.y, z);
		}
	}

	// Token: 0x060032A4 RID: 12964 RVA: 0x001650D8 File Offset: 0x001634D8
	protected override void SetVisible(bool visible, bool forceUpdate)
	{
		if (!forceUpdate && this._visible == visible)
		{
			return;
		}
		if (this._visibleBehaviourComponents == null)
		{
			this._visibleBehaviourComponents = new List<Behaviour>(this.basePart.GetComponentsInChildren<SimpleSprite>());
			this._visibleBehaviourComponents.AddRange(this.basePart.GetComponentsInChildren<UIButton>());
		}
		foreach (Behaviour behaviour in this._visibleBehaviourComponents)
		{
			(behaviour as SpriteRoot).Hide(!visible);
		}
		this._movingPartVisible = (visible && !this.hideMovingPart);
		(this._movingPartBehaviorComponent as SpriteRoot).Hide(!this._movingPartVisible);
		this._visible = visible;
	}

	// Token: 0x060032A5 RID: 12965 RVA: 0x001651C0 File Offset: 0x001635C0
	protected override void SetPosition(GameObject go, Vector3 vec)
	{
		go.transform.localPosition = new Vector3(vec.x, vec.y, go.transform.localPosition.z);
	}

	// Token: 0x060032A6 RID: 12966 RVA: 0x00165200 File Offset: 0x00163600
	protected Behaviour GetEzguiBehavior(GameObject go)
	{
		if (go == null)
		{
			return null;
		}
		Behaviour component = go.GetComponent<SimpleSprite>();
		if (component == null)
		{
			component = go.GetComponent<UIButton>();
		}
		return component;
	}

	// Token: 0x04002F22 RID: 12066
	protected Behaviour _movingPartBehaviorComponent;
}
