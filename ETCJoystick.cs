using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000418 RID: 1048
[Serializable]
public class ETCJoystick : ETCBase, IPointerEnterHandler, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	// Token: 0x06001F0C RID: 7948 RVA: 0x000ECD08 File Offset: 0x000EB108
	public ETCJoystick()
	{
		this.joystickType = ETCJoystick.JoystickType.Static;
		this.allowJoystickOverTouchPad = false;
		this.radiusBase = ETCJoystick.RadiusBase.Width;
		this.axisX = new ETCAxis("Horizontal");
		this.axisY = new ETCAxis("Vertical");
		this._visible = true;
		this._activated = true;
		this.joystickArea = ETCJoystick.JoystickArea.FullScreen;
		this.isDynamicActif = false;
		this.isOnDrag = false;
		this.isOnTouch = false;
		this.axisX.unityAxis = "Horizontal";
		this.axisY.unityAxis = "Vertical";
		this.enableKeySimulation = true;
		this.isNoReturnThumb = false;
		this.showPSInspector = false;
		this.showAxesInspector = false;
		this.showEventInspector = false;
		this.showSpriteInspector = false;
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x06001F0D RID: 7949 RVA: 0x000ECDCF File Offset: 0x000EB1CF
	// (set) Token: 0x06001F0E RID: 7950 RVA: 0x000ECDD7 File Offset: 0x000EB1D7
	public bool IsNoReturnThumb
	{
		get
		{
			return this.isNoReturnThumb;
		}
		set
		{
			this.isNoReturnThumb = value;
		}
	}

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06001F0F RID: 7951 RVA: 0x000ECDE0 File Offset: 0x000EB1E0
	// (set) Token: 0x06001F10 RID: 7952 RVA: 0x000ECDE8 File Offset: 0x000EB1E8
	public bool IsNoOffsetThumb
	{
		get
		{
			return this.isNoOffsetThumb;
		}
		set
		{
			this.isNoOffsetThumb = value;
		}
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x000ECDF4 File Offset: 0x000EB1F4
	protected override void Awake()
	{
		base.Awake();
		if (this.joystickType == ETCJoystick.JoystickType.Dynamic)
		{
			this.rectTransform().anchorMin = new Vector2(0.5f, 0.5f);
			this.rectTransform().anchorMax = new Vector2(0.5f, 0.5f);
			this.rectTransform().SetAsLastSibling();
			base.visible = false;
		}
		if (this.allowSimulationStandalone && this.enableKeySimulation && !Application.isEditor && this.joystickType != ETCJoystick.JoystickType.Dynamic)
		{
			this.SetVisible(this.visibleOnStandalone);
		}
	}

	// Token: 0x06001F12 RID: 7954 RVA: 0x000ECE90 File Offset: 0x000EB290
	public override void Start()
	{
		this.axisX.InitAxis();
		this.axisY.InitAxis();
		if (this.enableCamera)
		{
			this.InitCameraLookAt();
		}
		this.tmpAxis = Vector2.zero;
		this.OldTmpAxis = Vector2.zero;
		this.noReturnPosition = this.thumb.position;
		this.pointId = -1;
		if (this.joystickType == ETCJoystick.JoystickType.Dynamic)
		{
			base.visible = false;
		}
		base.Start();
		if (this.enableCamera && this.cameraMode == ETCBase.CameraMode.SmoothFollow && this.cameraTransform && this.cameraLookAt)
		{
			this.cameraTransform.position = this.cameraLookAt.TransformPoint(new Vector3(0f, this.followHeight, -this.followDistance));
			this.cameraTransform.LookAt(this.cameraLookAt);
		}
		if (this.enableCamera && this.cameraMode == ETCBase.CameraMode.Follow && this.cameraTransform && this.cameraLookAt)
		{
			this.cameraTransform.position = this.cameraLookAt.position + this.followOffset;
			this.cameraTransform.LookAt(this.cameraLookAt.position);
		}
	}

	// Token: 0x06001F13 RID: 7955 RVA: 0x000ECFF4 File Offset: 0x000EB3F4
	public override void Update()
	{
		base.Update();
		if (this.joystickType == ETCJoystick.JoystickType.Dynamic && !this._visible && this._activated)
		{
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			if (this.isTouchOverJoystickArea(ref zero, ref zero2))
			{
				GameObject firstUIElement = base.GetFirstUIElement(zero2);
				if (firstUIElement == null || (this.allowJoystickOverTouchPad && firstUIElement.GetComponent<ETCTouchPad>()) || (firstUIElement != null && firstUIElement.GetComponent<ETCArea>()))
				{
					this.cachedRectTransform.anchoredPosition = zero;
					base.visible = true;
				}
			}
		}
		if (this.joystickType == ETCJoystick.JoystickType.Dynamic && this._visible && this.GetTouchCount() == 0)
		{
			base.visible = false;
		}
	}

	// Token: 0x06001F14 RID: 7956 RVA: 0x000ED0C9 File Offset: 0x000EB4C9
	public override void LateUpdate()
	{
		if (this.enableCamera && !this.cameraLookAt)
		{
			this.InitCameraLookAt();
		}
		base.LateUpdate();
	}

	// Token: 0x06001F15 RID: 7957 RVA: 0x000ED0F4 File Offset: 0x000EB4F4
	private void InitCameraLookAt()
	{
		if (this.cameraTargetMode == ETCBase.CameraTargetMode.FromDirectActionAxisX)
		{
			this.cameraLookAt = this.axisX.directTransform;
		}
		else if (this.cameraTargetMode == ETCBase.CameraTargetMode.FromDirectActionAxisY)
		{
			this.cameraLookAt = this.axisY.directTransform;
			if (this.isTurnAndMove)
			{
				this.cameraLookAt = this.axisX.directTransform;
			}
		}
		else if (this.cameraTargetMode == ETCBase.CameraTargetMode.LinkOnTag)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag(this.camTargetTag);
			if (gameObject)
			{
				this.cameraLookAt = gameObject.transform;
			}
		}
		if (this.cameraLookAt)
		{
			this.cameraLookAtCC = this.cameraLookAt.GetComponent<CharacterController>();
		}
	}

	// Token: 0x06001F16 RID: 7958 RVA: 0x000ED1B1 File Offset: 0x000EB5B1
	protected override void UpdateControlState()
	{
		if (this._visible)
		{
			this.UpdateJoystick();
		}
		else if (this.joystickType == ETCJoystick.JoystickType.Dynamic)
		{
			this.OnUp(false);
		}
	}

	// Token: 0x06001F17 RID: 7959 RVA: 0x000ED1DC File Offset: 0x000EB5DC
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.joystickType == ETCJoystick.JoystickType.Dynamic && !this.isDynamicActif && this._activated && this.pointId == -1)
		{
			eventData.pointerDrag = base.gameObject;
			eventData.pointerPress = base.gameObject;
			this.isDynamicActif = true;
			this.pointId = eventData.pointerId;
		}
		if (this.joystickType == ETCJoystick.JoystickType.Dynamic && !eventData.eligibleForClick)
		{
			this.OnPointerUp(eventData);
		}
	}

	// Token: 0x06001F18 RID: 7960 RVA: 0x000ED25E File Offset: 0x000EB65E
	public void OnPointerDown(PointerEventData eventData)
	{
		this.onTouchStart.Invoke();
		this.pointId = eventData.pointerId;
		if (this.isNoOffsetThumb)
		{
			this.OnDrag(eventData);
		}
	}

	// Token: 0x06001F19 RID: 7961 RVA: 0x000ED289 File Offset: 0x000EB689
	public void OnBeginDrag(PointerEventData eventData)
	{
	}

	// Token: 0x06001F1A RID: 7962 RVA: 0x000ED28C File Offset: 0x000EB68C
	public void OnDrag(PointerEventData eventData)
	{
		if (this.pointId == eventData.pointerId)
		{
			this.isOnDrag = true;
			this.isOnTouch = true;
			float radius = this.GetRadius();
			if (!this.isNoReturnThumb)
			{
				this.thumbPosition = eventData.position - eventData.pressPosition;
			}
			else
			{
				this.thumbPosition = (eventData.position - this.noReturnPosition) / this.cachedRootCanvas.rectTransform().localScale.x + this.noReturnOffset;
			}
			if (this.isNoOffsetThumb)
			{
				this.thumbPosition = (eventData.position - this.cachedRectTransform.position) / this.cachedRootCanvas.rectTransform().localScale.x;
			}
			this.thumbPosition.x = (float)Mathf.FloorToInt(this.thumbPosition.x);
			this.thumbPosition.y = (float)Mathf.FloorToInt(this.thumbPosition.y);
			if (!this.axisX.enable)
			{
				this.thumbPosition.x = 0f;
			}
			if (!this.axisY.enable)
			{
				this.thumbPosition.y = 0f;
			}
			if (this.thumbPosition.magnitude > radius)
			{
				if (!this.isNoReturnThumb)
				{
					this.thumbPosition = this.thumbPosition.normalized * radius;
				}
				else
				{
					this.thumbPosition = this.thumbPosition.normalized * radius;
				}
			}
			this.thumb.anchoredPosition = this.thumbPosition;
		}
	}

	// Token: 0x06001F1B RID: 7963 RVA: 0x000ED446 File Offset: 0x000EB846
	public void OnPointerUp(PointerEventData eventData)
	{
		if (this.pointId == eventData.pointerId)
		{
			this.OnUp(true);
		}
	}

	// Token: 0x06001F1C RID: 7964 RVA: 0x000ED460 File Offset: 0x000EB860
	private void OnUp(bool real = true)
	{
		this.isOnDrag = false;
		this.isOnTouch = false;
		if (this.isNoReturnThumb)
		{
			this.noReturnPosition = this.thumb.position;
			this.noReturnOffset = this.thumbPosition;
		}
		if (!this.isNoReturnThumb)
		{
			this.thumbPosition = Vector2.zero;
			this.thumb.anchoredPosition = Vector2.zero;
			this.axisX.axisState = ETCAxis.AxisState.None;
			this.axisY.axisState = ETCAxis.AxisState.None;
		}
		if (!this.axisX.isEnertia && !this.axisY.isEnertia)
		{
			this.axisX.ResetAxis();
			this.axisY.ResetAxis();
			this.tmpAxis = Vector2.zero;
			this.OldTmpAxis = Vector2.zero;
			if (real)
			{
				this.onMoveEnd.Invoke();
			}
		}
		if (this.joystickType == ETCJoystick.JoystickType.Dynamic)
		{
			base.visible = false;
			this.isDynamicActif = false;
		}
		this.pointId = -1;
		if (real)
		{
			this.onTouchUp.Invoke();
		}
	}

	// Token: 0x06001F1D RID: 7965 RVA: 0x000ED574 File Offset: 0x000EB974
	protected override void DoActionBeforeEndOfFrame()
	{
		this.axisX.DoGravity();
		this.axisY.DoGravity();
	}

	// Token: 0x06001F1E RID: 7966 RVA: 0x000ED58C File Offset: 0x000EB98C
	private void UpdateJoystick()
	{
		if (this.enableKeySimulation && !this.isOnTouch && this._activated && this._visible)
		{
			float axis = Input.GetAxis(this.axisX.unityAxis);
			float axis2 = Input.GetAxis(this.axisY.unityAxis);
			if (!this.isNoReturnThumb)
			{
				this.thumb.localPosition = Vector2.zero;
			}
			this.isOnDrag = false;
			if (axis != 0f)
			{
				this.isOnDrag = true;
				this.thumb.localPosition = new Vector2(this.GetRadius() * axis, this.thumb.localPosition.y);
			}
			if (axis2 != 0f)
			{
				this.isOnDrag = true;
				this.thumb.localPosition = new Vector2(this.thumb.localPosition.x, this.GetRadius() * axis2);
			}
			this.thumbPosition = this.thumb.localPosition;
		}
		this.OldTmpAxis.x = this.axisX.axisValue;
		this.OldTmpAxis.y = this.axisY.axisValue;
		this.tmpAxis = this.thumbPosition / this.GetRadius();
		this.axisX.UpdateAxis(this.tmpAxis.x, this.isOnDrag, ETCBase.ControlType.Joystick, true);
		this.axisY.UpdateAxis(this.tmpAxis.y, this.isOnDrag, ETCBase.ControlType.Joystick, true);
		if ((this.axisX.axisValue != 0f || this.axisY.axisValue != 0f) && this.OldTmpAxis == Vector2.zero)
		{
			this.onMoveStart.Invoke();
		}
		if (this.axisX.axisValue != 0f || this.axisY.axisValue != 0f)
		{
			if (!this.isTurnAndMove)
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
			}
			else
			{
				this.DoTurnAndMove();
			}
			this.onMove.Invoke(new Vector2(this.axisX.axisValue, this.axisY.axisValue));
			this.onMoveSpeed.Invoke(new Vector2(this.axisX.axisSpeedValue, this.axisY.axisSpeedValue));
		}
		else if (this.axisX.axisValue == 0f && this.axisY.axisValue == 0f && this.OldTmpAxis != Vector2.zero)
		{
			this.onMoveEnd.Invoke();
		}
		if (!this.isTurnAndMove)
		{
			if (this.axisX.axisValue == 0f && this.axisX.directCharacterController && !this.axisX.directCharacterController.isGrounded && this.axisX.isLockinJump)
			{
				this.axisX.DoDirectAction();
			}
			if (this.axisY.axisValue == 0f && this.axisY.directCharacterController && !this.axisY.directCharacterController.isGrounded && this.axisY.isLockinJump)
			{
				this.axisY.DoDirectAction();
			}
		}
		else if (this.axisX.axisValue == 0f && this.axisY.axisValue == 0f && this.axisX.directCharacterController && !this.axisX.directCharacterController.isGrounded && this.tmLockInJump)
		{
			this.DoTurnAndMove();
		}
		float num = 1f;
		if (this.axisX.invertedAxis)
		{
			num = -1f;
		}
		if (Mathf.Abs(this.OldTmpAxis.x) < this.axisX.axisThreshold && Mathf.Abs(this.axisX.axisValue) >= this.axisX.axisThreshold)
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
		if (Mathf.Abs(this.OldTmpAxis.y) < this.axisY.axisThreshold && Mathf.Abs(this.axisY.axisValue) >= this.axisY.axisThreshold)
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

	// Token: 0x06001F1F RID: 7967 RVA: 0x000EDCEC File Offset: 0x000EC0EC
	private bool isTouchOverJoystickArea(ref Vector2 localPosition, ref Vector2 screenPosition)
	{
		bool flag = false;
		bool flag2 = false;
		screenPosition = Vector2.zero;
		int touchCount = this.GetTouchCount();
		int num = 0;
		while (num < touchCount && !flag)
		{
			if (Input.GetTouch(num).phase == TouchPhase.Began)
			{
				screenPosition = Input.GetTouch(num).position;
				flag2 = true;
			}
			if (flag2 && this.isScreenPointOverArea(screenPosition, ref localPosition))
			{
				flag = true;
			}
			num++;
		}
		return flag;
	}

	// Token: 0x06001F20 RID: 7968 RVA: 0x000EDD70 File Offset: 0x000EC170
	private bool isScreenPointOverArea(Vector2 screenPosition, ref Vector2 localPosition)
	{
		bool result = false;
		if (this.joystickArea != ETCJoystick.JoystickArea.UserDefined)
		{
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.cachedRootCanvas.rectTransform(), screenPosition, null, out localPosition))
			{
				switch (this.joystickArea)
				{
				case ETCJoystick.JoystickArea.FullScreen:
					result = true;
					break;
				case ETCJoystick.JoystickArea.Left:
					if (localPosition.x < 0f)
					{
						result = true;
					}
					break;
				case ETCJoystick.JoystickArea.Right:
					if (localPosition.x > 0f)
					{
						result = true;
					}
					break;
				case ETCJoystick.JoystickArea.Top:
					if (localPosition.y > 0f)
					{
						result = true;
					}
					break;
				case ETCJoystick.JoystickArea.Bottom:
					if (localPosition.y < 0f)
					{
						result = true;
					}
					break;
				case ETCJoystick.JoystickArea.TopLeft:
					if (localPosition.y > 0f && localPosition.x < 0f)
					{
						result = true;
					}
					break;
				case ETCJoystick.JoystickArea.TopRight:
					if (localPosition.y > 0f && localPosition.x > 0f)
					{
						result = true;
					}
					break;
				case ETCJoystick.JoystickArea.BottomLeft:
					if (localPosition.y < 0f && localPosition.x < 0f)
					{
						result = true;
					}
					break;
				case ETCJoystick.JoystickArea.BottomRight:
					if (localPosition.y < 0f && localPosition.x > 0f)
					{
						result = true;
					}
					break;
				}
			}
		}
		else if (RectTransformUtility.RectangleContainsScreenPoint(this.userArea, screenPosition, this.cachedRootCanvas.worldCamera))
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.cachedRootCanvas.rectTransform(), screenPosition, this.cachedRootCanvas.worldCamera, out localPosition);
			result = true;
		}
		return result;
	}

	// Token: 0x06001F21 RID: 7969 RVA: 0x000EDF1B File Offset: 0x000EC31B
	private int GetTouchCount()
	{
		return Input.touchCount;
	}

	// Token: 0x06001F22 RID: 7970 RVA: 0x000EDF24 File Offset: 0x000EC324
	public float GetRadius()
	{
		float result = 0f;
		ETCJoystick.RadiusBase radiusBase = this.radiusBase;
		if (radiusBase != ETCJoystick.RadiusBase.Width)
		{
			if (radiusBase != ETCJoystick.RadiusBase.Height)
			{
				if (radiusBase == ETCJoystick.RadiusBase.UserDefined)
				{
					result = this.radiusBaseValue;
				}
			}
			else
			{
				result = this.cachedRectTransform.sizeDelta.y * 0.5f;
			}
		}
		else
		{
			result = this.cachedRectTransform.sizeDelta.x * 0.5f;
		}
		return result;
	}

	// Token: 0x06001F23 RID: 7971 RVA: 0x000EDFA2 File Offset: 0x000EC3A2
	protected override void SetActivated()
	{
		base.GetComponent<CanvasGroup>().blocksRaycasts = this._activated;
		if (!this._activated)
		{
			this.OnUp(false);
		}
	}

	// Token: 0x06001F24 RID: 7972 RVA: 0x000EDFC8 File Offset: 0x000EC3C8
	protected override void SetVisible(bool visible = true)
	{
		bool enabled = this._visible;
		if (!visible)
		{
			enabled = visible;
		}
		base.GetComponent<Image>().enabled = enabled;
		this.thumb.GetComponent<Image>().enabled = enabled;
		base.GetComponent<CanvasGroup>().blocksRaycasts = this._activated;
	}

	// Token: 0x06001F25 RID: 7973 RVA: 0x000EE014 File Offset: 0x000EC414
	private void DoTurnAndMove()
	{
		float num = Mathf.Atan2(this.axisX.axisValue, this.axisY.axisValue) * 57.29578f;
		AnimationCurve animationCurve = this.tmMoveCurve;
		Vector2 vector = new Vector2(this.axisX.axisValue, this.axisY.axisValue);
		float d = animationCurve.Evaluate(vector.magnitude) * this.tmSpeed;
		if (this.axisX.directTransform != null)
		{
			this.axisX.directTransform.rotation = Quaternion.Euler(new Vector3(0f, num + this.tmAdditionnalRotation, 0f));
			if (this.axisX.directCharacterController != null)
			{
				if (this.axisX.directCharacterController.isGrounded || !this.tmLockInJump)
				{
					Vector3 a = this.axisX.directCharacterController.transform.TransformDirection(Vector3.forward) * d;
					this.axisX.directCharacterController.Move(a * Time.deltaTime);
					this.tmLastMove = a;
				}
				else
				{
					this.axisX.directCharacterController.Move(this.tmLastMove * Time.deltaTime);
				}
			}
			else
			{
				this.axisX.directTransform.Translate(Vector3.forward * d * Time.deltaTime, Space.Self);
			}
		}
	}

	// Token: 0x06001F26 RID: 7974 RVA: 0x000EE187 File Offset: 0x000EC587
	public void InitCurve()
	{
		this.axisX.InitDeadCurve();
		this.axisY.InitDeadCurve();
		this.InitTurnMoveCurve();
	}

	// Token: 0x06001F27 RID: 7975 RVA: 0x000EE1A5 File Offset: 0x000EC5A5
	public void InitTurnMoveCurve()
	{
		this.tmMoveCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		this.tmMoveCurve.postWrapMode = WrapMode.PingPong;
		this.tmMoveCurve.preWrapMode = WrapMode.PingPong;
	}

	// Token: 0x0400202C RID: 8236
	[SerializeField]
	public ETCJoystick.OnMoveStartHandler onMoveStart;

	// Token: 0x0400202D RID: 8237
	[SerializeField]
	public ETCJoystick.OnMoveHandler onMove;

	// Token: 0x0400202E RID: 8238
	[SerializeField]
	public ETCJoystick.OnMoveSpeedHandler onMoveSpeed;

	// Token: 0x0400202F RID: 8239
	[SerializeField]
	public ETCJoystick.OnMoveEndHandler onMoveEnd;

	// Token: 0x04002030 RID: 8240
	[SerializeField]
	public ETCJoystick.OnTouchStartHandler onTouchStart;

	// Token: 0x04002031 RID: 8241
	[SerializeField]
	public ETCJoystick.OnTouchUpHandler onTouchUp;

	// Token: 0x04002032 RID: 8242
	[SerializeField]
	public ETCJoystick.OnDownUpHandler OnDownUp;

	// Token: 0x04002033 RID: 8243
	[SerializeField]
	public ETCJoystick.OnDownDownHandler OnDownDown;

	// Token: 0x04002034 RID: 8244
	[SerializeField]
	public ETCJoystick.OnDownLeftHandler OnDownLeft;

	// Token: 0x04002035 RID: 8245
	[SerializeField]
	public ETCJoystick.OnDownRightHandler OnDownRight;

	// Token: 0x04002036 RID: 8246
	[SerializeField]
	public ETCJoystick.OnDownUpHandler OnPressUp;

	// Token: 0x04002037 RID: 8247
	[SerializeField]
	public ETCJoystick.OnDownDownHandler OnPressDown;

	// Token: 0x04002038 RID: 8248
	[SerializeField]
	public ETCJoystick.OnDownLeftHandler OnPressLeft;

	// Token: 0x04002039 RID: 8249
	[SerializeField]
	public ETCJoystick.OnDownRightHandler OnPressRight;

	// Token: 0x0400203A RID: 8250
	public ETCJoystick.JoystickType joystickType;

	// Token: 0x0400203B RID: 8251
	public bool allowJoystickOverTouchPad;

	// Token: 0x0400203C RID: 8252
	public ETCJoystick.RadiusBase radiusBase;

	// Token: 0x0400203D RID: 8253
	public float radiusBaseValue;

	// Token: 0x0400203E RID: 8254
	public ETCAxis axisX;

	// Token: 0x0400203F RID: 8255
	public ETCAxis axisY;

	// Token: 0x04002040 RID: 8256
	public RectTransform thumb;

	// Token: 0x04002041 RID: 8257
	public ETCJoystick.JoystickArea joystickArea;

	// Token: 0x04002042 RID: 8258
	public RectTransform userArea;

	// Token: 0x04002043 RID: 8259
	public bool isTurnAndMove;

	// Token: 0x04002044 RID: 8260
	public float tmSpeed = 10f;

	// Token: 0x04002045 RID: 8261
	public float tmAdditionnalRotation;

	// Token: 0x04002046 RID: 8262
	public AnimationCurve tmMoveCurve;

	// Token: 0x04002047 RID: 8263
	public bool tmLockInJump;

	// Token: 0x04002048 RID: 8264
	private Vector3 tmLastMove;

	// Token: 0x04002049 RID: 8265
	private Vector2 thumbPosition;

	// Token: 0x0400204A RID: 8266
	private bool isDynamicActif;

	// Token: 0x0400204B RID: 8267
	private Vector2 tmpAxis;

	// Token: 0x0400204C RID: 8268
	private Vector2 OldTmpAxis;

	// Token: 0x0400204D RID: 8269
	private bool isOnTouch;

	// Token: 0x0400204E RID: 8270
	[SerializeField]
	private bool isNoReturnThumb;

	// Token: 0x0400204F RID: 8271
	private Vector2 noReturnPosition;

	// Token: 0x04002050 RID: 8272
	private Vector2 noReturnOffset;

	// Token: 0x04002051 RID: 8273
	[SerializeField]
	private bool isNoOffsetThumb;

	// Token: 0x02000419 RID: 1049
	[Serializable]
	public class OnMoveStartHandler : UnityEvent
	{
	}

	// Token: 0x0200041A RID: 1050
	[Serializable]
	public class OnMoveSpeedHandler : UnityEvent<Vector2>
	{
	}

	// Token: 0x0200041B RID: 1051
	[Serializable]
	public class OnMoveHandler : UnityEvent<Vector2>
	{
	}

	// Token: 0x0200041C RID: 1052
	[Serializable]
	public class OnMoveEndHandler : UnityEvent
	{
	}

	// Token: 0x0200041D RID: 1053
	[Serializable]
	public class OnTouchStartHandler : UnityEvent
	{
	}

	// Token: 0x0200041E RID: 1054
	[Serializable]
	public class OnTouchUpHandler : UnityEvent
	{
	}

	// Token: 0x0200041F RID: 1055
	[Serializable]
	public class OnDownUpHandler : UnityEvent
	{
	}

	// Token: 0x02000420 RID: 1056
	[Serializable]
	public class OnDownDownHandler : UnityEvent
	{
	}

	// Token: 0x02000421 RID: 1057
	[Serializable]
	public class OnDownLeftHandler : UnityEvent
	{
	}

	// Token: 0x02000422 RID: 1058
	[Serializable]
	public class OnDownRightHandler : UnityEvent
	{
	}

	// Token: 0x02000423 RID: 1059
	[Serializable]
	public class OnPressUpHandler : UnityEvent
	{
	}

	// Token: 0x02000424 RID: 1060
	[Serializable]
	public class OnPressDownHandler : UnityEvent
	{
	}

	// Token: 0x02000425 RID: 1061
	[Serializable]
	public class OnPressLeftHandler : UnityEvent
	{
	}

	// Token: 0x02000426 RID: 1062
	[Serializable]
	public class OnPressRightHandler : UnityEvent
	{
	}

	// Token: 0x02000427 RID: 1063
	public enum JoystickArea
	{
		// Token: 0x04002053 RID: 8275
		UserDefined,
		// Token: 0x04002054 RID: 8276
		FullScreen,
		// Token: 0x04002055 RID: 8277
		Left,
		// Token: 0x04002056 RID: 8278
		Right,
		// Token: 0x04002057 RID: 8279
		Top,
		// Token: 0x04002058 RID: 8280
		Bottom,
		// Token: 0x04002059 RID: 8281
		TopLeft,
		// Token: 0x0400205A RID: 8282
		TopRight,
		// Token: 0x0400205B RID: 8283
		BottomLeft,
		// Token: 0x0400205C RID: 8284
		BottomRight
	}

	// Token: 0x02000428 RID: 1064
	public enum JoystickType
	{
		// Token: 0x0400205E RID: 8286
		Dynamic,
		// Token: 0x0400205F RID: 8287
		Static
	}

	// Token: 0x02000429 RID: 1065
	public enum RadiusBase
	{
		// Token: 0x04002061 RID: 8289
		Width,
		// Token: 0x04002062 RID: 8290
		Height,
		// Token: 0x04002063 RID: 8291
		UserDefined
	}
}
