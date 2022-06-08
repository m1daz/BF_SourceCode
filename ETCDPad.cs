using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000408 RID: 1032
public class ETCDPad : ETCBase, IDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	// Token: 0x06001E9D RID: 7837 RVA: 0x000EA7A8 File Offset: 0x000E8BA8
	public ETCDPad()
	{
		this.axisX = new ETCAxis("Horizontal");
		this.axisY = new ETCAxis("Vertical");
		this._visible = true;
		this._activated = true;
		this.dPadAxisCount = ETCBase.DPadAxis.Two_Axis;
		this.tmpAxis = Vector2.zero;
		this.showPSInspector = true;
		this.showSpriteInspector = false;
		this.showBehaviourInspector = false;
		this.showEventInspector = false;
		this.isOnDrag = false;
		this.isOnTouch = false;
		this.axisX.unityAxis = "Horizontal";
		this.axisY.unityAxis = "Vertical";
		this.enableKeySimulation = true;
	}

	// Token: 0x06001E9E RID: 7838 RVA: 0x000EA858 File Offset: 0x000E8C58
	public override void Start()
	{
		base.Start();
		this.tmpAxis = Vector2.zero;
		this.OldTmpAxis = Vector2.zero;
		this.axisX.InitAxis();
		this.axisY.InitAxis();
		if (this.allowSimulationStandalone && this.enableKeySimulation && !Application.isEditor)
		{
			this.SetVisible(this.visibleOnStandalone);
		}
	}

	// Token: 0x06001E9F RID: 7839 RVA: 0x000EA8C3 File Offset: 0x000E8CC3
	protected override void UpdateControlState()
	{
		this.UpdateDPad();
	}

	// Token: 0x06001EA0 RID: 7840 RVA: 0x000EA8CB File Offset: 0x000E8CCB
	protected override void DoActionBeforeEndOfFrame()
	{
		this.axisX.DoGravity();
		this.axisY.DoGravity();
	}

	// Token: 0x06001EA1 RID: 7841 RVA: 0x000EA8E4 File Offset: 0x000E8CE4
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this._activated && !this.isOnTouch)
		{
			this.onTouchStart.Invoke();
			this.GetTouchDirection(eventData.position, eventData.pressEventCamera);
			this.isOnTouch = true;
			this.isOnDrag = true;
			this.pointId = eventData.pointerId;
		}
	}

	// Token: 0x06001EA2 RID: 7842 RVA: 0x000EA93E File Offset: 0x000E8D3E
	public void OnDrag(PointerEventData eventData)
	{
		if (this._activated && this.pointId == eventData.pointerId)
		{
			this.isOnTouch = true;
			this.isOnDrag = true;
			this.GetTouchDirection(eventData.position, eventData.pressEventCamera);
		}
	}

	// Token: 0x06001EA3 RID: 7843 RVA: 0x000EA97C File Offset: 0x000E8D7C
	public void OnPointerUp(PointerEventData eventData)
	{
		if (this.pointId == eventData.pointerId)
		{
			this.isOnTouch = false;
			this.isOnDrag = false;
			this.tmpAxis = Vector2.zero;
			this.OldTmpAxis = Vector2.zero;
			this.axisX.axisState = ETCAxis.AxisState.None;
			this.axisY.axisState = ETCAxis.AxisState.None;
			if (!this.axisX.isEnertia && !this.axisY.isEnertia)
			{
				this.axisX.ResetAxis();
				this.axisY.ResetAxis();
				this.onMoveEnd.Invoke();
			}
			this.pointId = -1;
			this.onTouchUp.Invoke();
		}
	}

	// Token: 0x06001EA4 RID: 7844 RVA: 0x000EAA2C File Offset: 0x000E8E2C
	private void UpdateDPad()
	{
		if (this.enableKeySimulation && !this.isOnTouch && this._activated && this._visible)
		{
			float axis = Input.GetAxis(this.axisX.unityAxis);
			float axis2 = Input.GetAxis(this.axisY.unityAxis);
			this.isOnDrag = false;
			this.tmpAxis = Vector2.zero;
			if (axis != 0f)
			{
				this.isOnDrag = true;
				this.tmpAxis = new Vector2(1f * Mathf.Sign(axis), this.tmpAxis.y);
			}
			if (axis2 != 0f)
			{
				this.isOnDrag = true;
				this.tmpAxis = new Vector2(this.tmpAxis.x, 1f * Mathf.Sign(axis2));
			}
		}
		this.OldTmpAxis.x = this.axisX.axisValue;
		this.OldTmpAxis.y = this.axisY.axisValue;
		this.axisX.UpdateAxis(this.tmpAxis.x, this.isOnDrag, ETCBase.ControlType.DPad, true);
		this.axisY.UpdateAxis(this.tmpAxis.y, this.isOnDrag, ETCBase.ControlType.DPad, true);
		if ((this.axisX.axisValue != 0f || this.axisY.axisValue != 0f) && this.OldTmpAxis == Vector2.zero)
		{
			this.onMoveStart.Invoke();
		}
		if (this.axisX.axisValue != 0f || this.axisY.axisValue != 0f)
		{
			if (this.axisX.actionOn == ETCAxis.ActionOn.Down && (this.axisX.axisState == ETCAxis.AxisState.DownLeft || this.axisX.axisState == ETCAxis.AxisState.DownRight))
			{
				this.axisX.DoDirectAction();
			}
			else if (this.axisX.actionOn == ETCAxis.ActionOn.Press)
			{
				this.axisX.DoDirectAction();
			}
			if (this.axisY.actionOn == ETCAxis.ActionOn.Down && (this.axisY.axisState == ETCAxis.AxisState.DownUp || this.axisY.axisState == ETCAxis.AxisState.DownDown))
			{
				this.axisY.DoDirectAction();
			}
			else if (this.axisY.actionOn == ETCAxis.ActionOn.Press)
			{
				this.axisY.DoDirectAction();
			}
			this.onMove.Invoke(new Vector2(this.axisX.axisValue, this.axisY.axisValue));
			this.onMoveSpeed.Invoke(new Vector2(this.axisX.axisSpeedValue, this.axisY.axisSpeedValue));
		}
		else if (this.axisX.axisValue == 0f && this.axisY.axisValue == 0f && this.OldTmpAxis != Vector2.zero)
		{
			this.onMoveEnd.Invoke();
		}
		float num = 1f;
		if (this.axisX.invertedAxis)
		{
			num = -1f;
		}
		if (this.OldTmpAxis.x == 0f && Mathf.Abs(this.axisX.axisValue) > 0f)
		{
			if (this.axisX.axisValue * num > 0f)
			{
				this.axisX.axisState = ETCAxis.AxisState.DownRight;
				this.OnDownRight.Invoke();
			}
			else if (this.axisX.axisValue * num < 0f)
			{
				this.axisX.axisState = ETCAxis.AxisState.DownLeft;
				this.OnDownLeft.Invoke();
			}
			else
			{
				this.axisX.axisState = ETCAxis.AxisState.None;
			}
		}
		else if (this.axisX.axisState != ETCAxis.AxisState.None)
		{
			if (this.axisX.axisValue * num > 0f)
			{
				this.axisX.axisState = ETCAxis.AxisState.PressRight;
				this.OnPressRight.Invoke();
			}
			else if (this.axisX.axisValue * num < 0f)
			{
				this.axisX.axisState = ETCAxis.AxisState.PressLeft;
				this.OnPressLeft.Invoke();
			}
			else
			{
				this.axisX.axisState = ETCAxis.AxisState.None;
			}
		}
		num = 1f;
		if (this.axisY.invertedAxis)
		{
			num = -1f;
		}
		if (this.OldTmpAxis.y == 0f && Mathf.Abs(this.axisY.axisValue) > 0f)
		{
			if (this.axisY.axisValue * num > 0f)
			{
				this.axisY.axisState = ETCAxis.AxisState.DownUp;
				this.OnDownUp.Invoke();
			}
			else if (this.axisY.axisValue * num < 0f)
			{
				this.axisY.axisState = ETCAxis.AxisState.DownDown;
				this.OnDownDown.Invoke();
			}
			else
			{
				this.axisY.axisState = ETCAxis.AxisState.None;
			}
		}
		else if (this.axisY.axisState != ETCAxis.AxisState.None)
		{
			if (this.axisY.axisValue * num > 0f)
			{
				this.axisY.axisState = ETCAxis.AxisState.PressUp;
				this.OnPressUp.Invoke();
			}
			else if (this.axisY.axisValue * num < 0f)
			{
				this.axisY.axisState = ETCAxis.AxisState.PressDown;
				this.OnPressDown.Invoke();
			}
			else
			{
				this.axisY.axisState = ETCAxis.AxisState.None;
			}
		}
	}

	// Token: 0x06001EA5 RID: 7845 RVA: 0x000EAFC0 File Offset: 0x000E93C0
	protected override void SetVisible(bool forceUnvisible = false)
	{
		bool visible = this._visible;
		if (!base.visible)
		{
			visible = base.visible;
		}
		base.GetComponent<Image>().enabled = visible;
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x000EAFF4 File Offset: 0x000E93F4
	protected override void SetActivated()
	{
		if (!this._activated)
		{
			this.isOnTouch = false;
			this.isOnDrag = false;
			this.tmpAxis = Vector2.zero;
			this.OldTmpAxis = Vector2.zero;
			this.axisX.axisState = ETCAxis.AxisState.None;
			this.axisY.axisState = ETCAxis.AxisState.None;
			if (!this.axisX.isEnertia && !this.axisY.isEnertia)
			{
				this.axisX.ResetAxis();
				this.axisY.ResetAxis();
			}
			this.pointId = -1;
		}
	}

	// Token: 0x06001EA7 RID: 7847 RVA: 0x000EB088 File Offset: 0x000E9488
	private void GetTouchDirection(Vector2 position, Camera cam)
	{
		Vector2 vector;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(this.cachedRectTransform, position, cam, out vector);
		Vector2 vector2 = this.rectTransform().sizeDelta / this.buttonSizeCoef;
		this.tmpAxis = Vector2.zero;
		if ((vector.x < -vector2.x / 2f && vector.y > -vector2.y / 2f && vector.y < vector2.y / 2f && this.dPadAxisCount == ETCBase.DPadAxis.Two_Axis) || (this.dPadAxisCount == ETCBase.DPadAxis.Four_Axis && vector.x < -vector2.x / 2f))
		{
			this.tmpAxis.x = -1f;
		}
		if ((vector.x > vector2.x / 2f && vector.y > -vector2.y / 2f && vector.y < vector2.y / 2f && this.dPadAxisCount == ETCBase.DPadAxis.Two_Axis) || (this.dPadAxisCount == ETCBase.DPadAxis.Four_Axis && vector.x > vector2.x / 2f))
		{
			this.tmpAxis.x = 1f;
		}
		if ((vector.y > vector2.y / 2f && vector.x > -vector2.x / 2f && vector.x < vector2.x / 2f && this.dPadAxisCount == ETCBase.DPadAxis.Two_Axis) || (this.dPadAxisCount == ETCBase.DPadAxis.Four_Axis && vector.y > vector2.y / 2f))
		{
			this.tmpAxis.y = 1f;
		}
		if ((vector.y < -vector2.y / 2f && vector.x > -vector2.x / 2f && vector.x < vector2.x / 2f && this.dPadAxisCount == ETCBase.DPadAxis.Two_Axis) || (this.dPadAxisCount == ETCBase.DPadAxis.Four_Axis && vector.y < -vector2.y / 2f))
		{
			this.tmpAxis.y = -1f;
		}
	}

	// Token: 0x0400200E RID: 8206
	[SerializeField]
	public ETCDPad.OnMoveStartHandler onMoveStart;

	// Token: 0x0400200F RID: 8207
	[SerializeField]
	public ETCDPad.OnMoveHandler onMove;

	// Token: 0x04002010 RID: 8208
	[SerializeField]
	public ETCDPad.OnMoveSpeedHandler onMoveSpeed;

	// Token: 0x04002011 RID: 8209
	[SerializeField]
	public ETCDPad.OnMoveEndHandler onMoveEnd;

	// Token: 0x04002012 RID: 8210
	[SerializeField]
	public ETCDPad.OnTouchStartHandler onTouchStart;

	// Token: 0x04002013 RID: 8211
	[SerializeField]
	public ETCDPad.OnTouchUPHandler onTouchUp;

	// Token: 0x04002014 RID: 8212
	[SerializeField]
	public ETCDPad.OnDownUpHandler OnDownUp;

	// Token: 0x04002015 RID: 8213
	[SerializeField]
	public ETCDPad.OnDownDownHandler OnDownDown;

	// Token: 0x04002016 RID: 8214
	[SerializeField]
	public ETCDPad.OnDownLeftHandler OnDownLeft;

	// Token: 0x04002017 RID: 8215
	[SerializeField]
	public ETCDPad.OnDownRightHandler OnDownRight;

	// Token: 0x04002018 RID: 8216
	[SerializeField]
	public ETCDPad.OnDownUpHandler OnPressUp;

	// Token: 0x04002019 RID: 8217
	[SerializeField]
	public ETCDPad.OnDownDownHandler OnPressDown;

	// Token: 0x0400201A RID: 8218
	[SerializeField]
	public ETCDPad.OnDownLeftHandler OnPressLeft;

	// Token: 0x0400201B RID: 8219
	[SerializeField]
	public ETCDPad.OnDownRightHandler OnPressRight;

	// Token: 0x0400201C RID: 8220
	public ETCAxis axisX;

	// Token: 0x0400201D RID: 8221
	public ETCAxis axisY;

	// Token: 0x0400201E RID: 8222
	public Sprite normalSprite;

	// Token: 0x0400201F RID: 8223
	public Color normalColor;

	// Token: 0x04002020 RID: 8224
	public Sprite pressedSprite;

	// Token: 0x04002021 RID: 8225
	public Color pressedColor;

	// Token: 0x04002022 RID: 8226
	private Vector2 tmpAxis;

	// Token: 0x04002023 RID: 8227
	private Vector2 OldTmpAxis;

	// Token: 0x04002024 RID: 8228
	private bool isOnTouch;

	// Token: 0x04002025 RID: 8229
	private Image cachedImage;

	// Token: 0x04002026 RID: 8230
	public float buttonSizeCoef = 3f;

	// Token: 0x02000409 RID: 1033
	[Serializable]
	public class OnMoveStartHandler : UnityEvent
	{
	}

	// Token: 0x0200040A RID: 1034
	[Serializable]
	public class OnMoveHandler : UnityEvent<Vector2>
	{
	}

	// Token: 0x0200040B RID: 1035
	[Serializable]
	public class OnMoveSpeedHandler : UnityEvent<Vector2>
	{
	}

	// Token: 0x0200040C RID: 1036
	[Serializable]
	public class OnMoveEndHandler : UnityEvent
	{
	}

	// Token: 0x0200040D RID: 1037
	[Serializable]
	public class OnTouchStartHandler : UnityEvent
	{
	}

	// Token: 0x0200040E RID: 1038
	[Serializable]
	public class OnTouchUPHandler : UnityEvent
	{
	}

	// Token: 0x0200040F RID: 1039
	[Serializable]
	public class OnDownUpHandler : UnityEvent
	{
	}

	// Token: 0x02000410 RID: 1040
	[Serializable]
	public class OnDownDownHandler : UnityEvent
	{
	}

	// Token: 0x02000411 RID: 1041
	[Serializable]
	public class OnDownLeftHandler : UnityEvent
	{
	}

	// Token: 0x02000412 RID: 1042
	[Serializable]
	public class OnDownRightHandler : UnityEvent
	{
	}

	// Token: 0x02000413 RID: 1043
	[Serializable]
	public class OnPressUpHandler : UnityEvent
	{
	}

	// Token: 0x02000414 RID: 1044
	[Serializable]
	public class OnPressDownHandler : UnityEvent
	{
	}

	// Token: 0x02000415 RID: 1045
	[Serializable]
	public class OnPressLeftHandler : UnityEvent
	{
	}

	// Token: 0x02000416 RID: 1046
	[Serializable]
	public class OnPressRightHandler : UnityEvent
	{
	}
}
