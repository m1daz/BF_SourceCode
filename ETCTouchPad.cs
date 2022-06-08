using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200042A RID: 1066
[Serializable]
public class ETCTouchPad : ETCBase, IBeginDragHandler, IDragHandler, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IEventSystemHandler
{
	// Token: 0x06001F36 RID: 7990 RVA: 0x000EE250 File Offset: 0x000EC650
	public ETCTouchPad()
	{
		this.axisX = new ETCAxis("Horizontal");
		this.axisX.speed = 1f;
		this.axisY = new ETCAxis("Vertical");
		this.axisY.speed = 1f;
		this._visible = true;
		this._activated = true;
		this.showPSInspector = true;
		this.showSpriteInspector = false;
		this.showBehaviourInspector = false;
		this.showEventInspector = false;
		this.tmpAxis = Vector2.zero;
		this.isOnDrag = false;
		this.isOnTouch = false;
		this.axisX.unityAxis = "Horizontal";
		this.axisY.unityAxis = "Vertical";
		this.enableKeySimulation = true;
		this.enableKeySimulation = false;
		this.isOut = false;
		this.axisX.axisState = ETCAxis.AxisState.None;
		this.useFixedUpdate = false;
		this.isDPI = false;
	}

	// Token: 0x06001F37 RID: 7991 RVA: 0x000EE335 File Offset: 0x000EC735
	protected override void Awake()
	{
		base.Awake();
		this.cachedVisible = this._visible;
		this.cachedImage = base.GetComponent<Image>();
	}

	// Token: 0x06001F38 RID: 7992 RVA: 0x000EE358 File Offset: 0x000EC758
	public override void OnEnable()
	{
		base.OnEnable();
		if (!this.cachedVisible)
		{
			this.cachedImage.color = new Color(0f, 0f, 0f, 0f);
		}
		if (this.allowSimulationStandalone && this.enableKeySimulation && !Application.isEditor)
		{
			this.SetVisible(this.visibleOnStandalone);
		}
	}

	// Token: 0x06001F39 RID: 7993 RVA: 0x000EE3C6 File Offset: 0x000EC7C6
	public override void Start()
	{
		base.Start();
		this.tmpAxis = Vector2.zero;
		this.OldTmpAxis = Vector2.zero;
		this.axisX.InitAxis();
		this.axisY.InitAxis();
	}

	// Token: 0x06001F3A RID: 7994 RVA: 0x000EE3FA File Offset: 0x000EC7FA
	protected override void UpdateControlState()
	{
		this.UpdateTouchPad();
	}

	// Token: 0x06001F3B RID: 7995 RVA: 0x000EE402 File Offset: 0x000EC802
	protected override void DoActionBeforeEndOfFrame()
	{
		this.axisX.DoGravity();
		this.axisY.DoGravity();
	}

	// Token: 0x06001F3C RID: 7996 RVA: 0x000EE41C File Offset: 0x000EC81C
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.isSwipeIn && this.axisX.axisState == ETCAxis.AxisState.None && this._activated && !this.isOnTouch)
		{
			if (eventData.pointerDrag != null && eventData.pointerDrag != base.gameObject)
			{
				this.previousDargObject = eventData.pointerDrag;
			}
			else if (eventData.pointerPress != null && eventData.pointerPress != base.gameObject)
			{
				this.previousDargObject = eventData.pointerPress;
			}
			eventData.pointerDrag = base.gameObject;
			eventData.pointerPress = base.gameObject;
			this.OnPointerDown(eventData);
		}
	}

	// Token: 0x06001F3D RID: 7997 RVA: 0x000EE4E4 File Offset: 0x000EC8E4
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (this.pointId == eventData.pointerId)
		{
			this.onMoveStart.Invoke();
		}
	}

	// Token: 0x06001F3E RID: 7998 RVA: 0x000EE504 File Offset: 0x000EC904
	public void OnDrag(PointerEventData eventData)
	{
		if (base.activated && !this.isOut && this.pointId == eventData.pointerId)
		{
			this.isOnTouch = true;
			this.isOnDrag = true;
			if (this.isDPI)
			{
				this.tmpAxis = new Vector2(eventData.delta.x / Screen.dpi * 100f, eventData.delta.y / Screen.dpi * 100f);
			}
			else
			{
				this.tmpAxis = new Vector2(eventData.delta.x, eventData.delta.y);
			}
			if (!this.axisX.enable)
			{
				this.tmpAxis.x = 0f;
			}
			if (!this.axisY.enable)
			{
				this.tmpAxis.y = 0f;
			}
		}
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x000EE5FC File Offset: 0x000EC9FC
	public void OnPointerDown(PointerEventData eventData)
	{
		if (this._activated && !this.isOnTouch)
		{
			this.axisX.axisState = ETCAxis.AxisState.Down;
			this.tmpAxis = eventData.delta;
			this.isOut = false;
			this.isOnTouch = true;
			this.pointId = eventData.pointerId;
			this.onTouchStart.Invoke();
		}
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x000EE65C File Offset: 0x000ECA5C
	public void OnPointerUp(PointerEventData eventData)
	{
		if (this.pointId == eventData.pointerId)
		{
			this.isOnDrag = false;
			this.isOnTouch = false;
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
			this.onTouchUp.Invoke();
			if (this.previousDargObject)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(this.previousDargObject, eventData, ExecuteEvents.pointerUpHandler);
				this.previousDargObject = null;
			}
			this.pointId = -1;
		}
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x000EE732 File Offset: 0x000ECB32
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.pointId == eventData.pointerId && !this.isSwipeOut)
		{
			this.isOut = true;
			this.OnPointerUp(eventData);
		}
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x000EE760 File Offset: 0x000ECB60
	private void UpdateTouchPad()
	{
		if (this.enableKeySimulation && !this.isOnTouch && this._activated && this._visible)
		{
			this.isOnDrag = false;
			this.tmpAxis = Vector2.zero;
			float axis = Input.GetAxis(this.axisX.unityAxis);
			float axis2 = Input.GetAxis(this.axisY.unityAxis);
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
		this.tmpAxis = Vector2.zero;
	}

	// Token: 0x06001F43 RID: 8003 RVA: 0x000EECB4 File Offset: 0x000ED0B4
	protected override void SetVisible(bool forceUnvisible = false)
	{
		if (Application.isPlaying)
		{
			if (!this._visible)
			{
				this.cachedImage.color = new Color(0f, 0f, 0f, 0f);
			}
			else
			{
				this.cachedImage.color = new Color(1f, 1f, 1f, 1f);
			}
		}
	}

	// Token: 0x06001F44 RID: 8004 RVA: 0x000EED24 File Offset: 0x000ED124
	protected override void SetActivated()
	{
		if (!this._activated)
		{
			this.isOnDrag = false;
			this.isOnTouch = false;
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

	// Token: 0x04002064 RID: 8292
	[SerializeField]
	public ETCTouchPad.OnMoveStartHandler onMoveStart;

	// Token: 0x04002065 RID: 8293
	[SerializeField]
	public ETCTouchPad.OnMoveHandler onMove;

	// Token: 0x04002066 RID: 8294
	[SerializeField]
	public ETCTouchPad.OnMoveSpeedHandler onMoveSpeed;

	// Token: 0x04002067 RID: 8295
	[SerializeField]
	public ETCTouchPad.OnMoveEndHandler onMoveEnd;

	// Token: 0x04002068 RID: 8296
	[SerializeField]
	public ETCTouchPad.OnTouchStartHandler onTouchStart;

	// Token: 0x04002069 RID: 8297
	[SerializeField]
	public ETCTouchPad.OnTouchUPHandler onTouchUp;

	// Token: 0x0400206A RID: 8298
	[SerializeField]
	public ETCTouchPad.OnDownUpHandler OnDownUp;

	// Token: 0x0400206B RID: 8299
	[SerializeField]
	public ETCTouchPad.OnDownDownHandler OnDownDown;

	// Token: 0x0400206C RID: 8300
	[SerializeField]
	public ETCTouchPad.OnDownLeftHandler OnDownLeft;

	// Token: 0x0400206D RID: 8301
	[SerializeField]
	public ETCTouchPad.OnDownRightHandler OnDownRight;

	// Token: 0x0400206E RID: 8302
	[SerializeField]
	public ETCTouchPad.OnDownUpHandler OnPressUp;

	// Token: 0x0400206F RID: 8303
	[SerializeField]
	public ETCTouchPad.OnDownDownHandler OnPressDown;

	// Token: 0x04002070 RID: 8304
	[SerializeField]
	public ETCTouchPad.OnDownLeftHandler OnPressLeft;

	// Token: 0x04002071 RID: 8305
	[SerializeField]
	public ETCTouchPad.OnDownRightHandler OnPressRight;

	// Token: 0x04002072 RID: 8306
	public ETCAxis axisX;

	// Token: 0x04002073 RID: 8307
	public ETCAxis axisY;

	// Token: 0x04002074 RID: 8308
	public bool isDPI;

	// Token: 0x04002075 RID: 8309
	private Image cachedImage;

	// Token: 0x04002076 RID: 8310
	private Vector2 tmpAxis;

	// Token: 0x04002077 RID: 8311
	private Vector2 OldTmpAxis;

	// Token: 0x04002078 RID: 8312
	private GameObject previousDargObject;

	// Token: 0x04002079 RID: 8313
	private bool isOut;

	// Token: 0x0400207A RID: 8314
	private bool isOnTouch;

	// Token: 0x0400207B RID: 8315
	private bool cachedVisible;

	// Token: 0x0200042B RID: 1067
	[Serializable]
	public class OnMoveStartHandler : UnityEvent
	{
	}

	// Token: 0x0200042C RID: 1068
	[Serializable]
	public class OnMoveHandler : UnityEvent<Vector2>
	{
	}

	// Token: 0x0200042D RID: 1069
	[Serializable]
	public class OnMoveSpeedHandler : UnityEvent<Vector2>
	{
	}

	// Token: 0x0200042E RID: 1070
	[Serializable]
	public class OnMoveEndHandler : UnityEvent
	{
	}

	// Token: 0x0200042F RID: 1071
	[Serializable]
	public class OnTouchStartHandler : UnityEvent
	{
	}

	// Token: 0x02000430 RID: 1072
	[Serializable]
	public class OnTouchUPHandler : UnityEvent
	{
	}

	// Token: 0x02000431 RID: 1073
	[Serializable]
	public class OnDownUpHandler : UnityEvent
	{
	}

	// Token: 0x02000432 RID: 1074
	[Serializable]
	public class OnDownDownHandler : UnityEvent
	{
	}

	// Token: 0x02000433 RID: 1075
	[Serializable]
	public class OnDownLeftHandler : UnityEvent
	{
	}

	// Token: 0x02000434 RID: 1076
	[Serializable]
	public class OnDownRightHandler : UnityEvent
	{
	}

	// Token: 0x02000435 RID: 1077
	[Serializable]
	public class OnPressUpHandler : UnityEvent
	{
	}

	// Token: 0x02000436 RID: 1078
	[Serializable]
	public class OnPressDownHandler : UnityEvent
	{
	}

	// Token: 0x02000437 RID: 1079
	[Serializable]
	public class OnPressLeftHandler : UnityEvent
	{
	}

	// Token: 0x02000438 RID: 1080
	[Serializable]
	public class OnPressRightHandler : UnityEvent
	{
	}
}
