using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000600 RID: 1536
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Event System (UICamera)")]
[RequireComponent(typeof(Camera))]
public class UICamera : MonoBehaviour
{
	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x06002BF0 RID: 11248 RVA: 0x00143563 File Offset: 0x00141963
	[Obsolete("Use new OnDragStart / OnDragOver / OnDragOut / OnDragEnd events instead")]
	public bool stickyPress
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170002CA RID: 714
	// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x00143566 File Offset: 0x00141966
	// (set) Token: 0x06002BF2 RID: 11250 RVA: 0x0014357D File Offset: 0x0014197D
	public static bool disableController
	{
		get
		{
			return UICamera.mDisableController && !UIPopupList.isOpen;
		}
		set
		{
			UICamera.mDisableController = value;
		}
	}

	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x00143585 File Offset: 0x00141985
	// (set) Token: 0x06002BF4 RID: 11252 RVA: 0x0014358C File Offset: 0x0014198C
	[Obsolete("Use lastEventPosition instead. It handles controller input properly.")]
	public static Vector2 lastTouchPosition
	{
		get
		{
			return UICamera.mLastPos;
		}
		set
		{
			UICamera.mLastPos = value;
		}
	}

	// Token: 0x170002CC RID: 716
	// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x00143594 File Offset: 0x00141994
	// (set) Token: 0x06002BF6 RID: 11254 RVA: 0x001435F0 File Offset: 0x001419F0
	public static Vector2 lastEventPosition
	{
		get
		{
			UICamera.ControlScheme currentScheme = UICamera.currentScheme;
			if (currentScheme == UICamera.ControlScheme.Controller)
			{
				GameObject hoveredObject = UICamera.hoveredObject;
				if (hoveredObject != null)
				{
					Bounds bounds = NGUIMath.CalculateAbsoluteWidgetBounds(hoveredObject.transform);
					Camera camera = NGUITools.FindCameraForLayer(hoveredObject.layer);
					return camera.WorldToScreenPoint(bounds.center);
				}
			}
			return UICamera.mLastPos;
		}
		set
		{
			UICamera.mLastPos = value;
		}
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x001435F8 File Offset: 0x001419F8
	public static UICamera first
	{
		get
		{
			if (UICamera.list == null || UICamera.list.size == 0)
			{
				return null;
			}
			return UICamera.list[0];
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x00143620 File Offset: 0x00141A20
	// (set) Token: 0x06002BF9 RID: 11257 RVA: 0x001436B4 File Offset: 0x00141AB4
	public static UICamera.ControlScheme currentScheme
	{
		get
		{
			if (UICamera.mCurrentKey == KeyCode.None)
			{
				return UICamera.ControlScheme.Touch;
			}
			if (UICamera.mCurrentKey >= KeyCode.JoystickButton0)
			{
				return UICamera.ControlScheme.Controller;
			}
			if (!(UICamera.current != null))
			{
				return UICamera.ControlScheme.Mouse;
			}
			if (UICamera.mLastScheme == UICamera.ControlScheme.Controller && (UICamera.mCurrentKey == UICamera.current.submitKey0 || UICamera.mCurrentKey == UICamera.current.submitKey1))
			{
				return UICamera.ControlScheme.Controller;
			}
			if (UICamera.current.useMouse)
			{
				return UICamera.ControlScheme.Mouse;
			}
			if (UICamera.current.useTouch)
			{
				return UICamera.ControlScheme.Touch;
			}
			return UICamera.ControlScheme.Controller;
		}
		set
		{
			if (UICamera.mLastScheme != value)
			{
				if (value == UICamera.ControlScheme.Mouse)
				{
					UICamera.currentKey = KeyCode.Mouse0;
				}
				else if (value == UICamera.ControlScheme.Controller)
				{
					UICamera.currentKey = KeyCode.JoystickButton0;
				}
				else if (value == UICamera.ControlScheme.Touch)
				{
					UICamera.currentKey = KeyCode.None;
				}
				else
				{
					UICamera.currentKey = KeyCode.Alpha0;
				}
				UICamera.mLastScheme = value;
			}
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x06002BFA RID: 11258 RVA: 0x00143716 File Offset: 0x00141B16
	// (set) Token: 0x06002BFB RID: 11259 RVA: 0x00143720 File Offset: 0x00141B20
	public static KeyCode currentKey
	{
		get
		{
			return UICamera.mCurrentKey;
		}
		set
		{
			if (UICamera.mCurrentKey != value)
			{
				UICamera.ControlScheme controlScheme = UICamera.mLastScheme;
				UICamera.mCurrentKey = value;
				UICamera.mLastScheme = UICamera.currentScheme;
				if (controlScheme != UICamera.mLastScheme)
				{
					UICamera.HideTooltip();
					if (UICamera.mLastScheme == UICamera.ControlScheme.Mouse)
					{
						Cursor.lockState = CursorLockMode.None;
						Cursor.visible = true;
					}
					else if (UICamera.current != null && UICamera.current.autoHideCursor)
					{
						Cursor.visible = false;
						Cursor.lockState = CursorLockMode.Locked;
						UICamera.mMouse[0].ignoreDelta = 2;
					}
					if (UICamera.onSchemeChange != null)
					{
						UICamera.onSchemeChange();
					}
				}
			}
		}
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x06002BFC RID: 11260 RVA: 0x001437C8 File Offset: 0x00141BC8
	public static Ray currentRay
	{
		get
		{
			return (!(UICamera.currentCamera != null) || UICamera.currentTouch == null) ? default(Ray) : UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
		}
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06002BFD RID: 11261 RVA: 0x00143816 File Offset: 0x00141C16
	public static bool inputHasFocus
	{
		get
		{
			return UICamera.mInputFocus && UICamera.mSelected && UICamera.mSelected.activeInHierarchy;
		}
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06002BFE RID: 11262 RVA: 0x00143843 File Offset: 0x00141C43
	// (set) Token: 0x06002BFF RID: 11263 RVA: 0x0014384A File Offset: 0x00141C4A
	[Obsolete("Use delegates instead such as UICamera.onClick, UICamera.onHover, etc.")]
	public static GameObject genericEventHandler
	{
		get
		{
			return UICamera.mGenericHandler;
		}
		set
		{
			UICamera.mGenericHandler = value;
		}
	}

	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06002C00 RID: 11264 RVA: 0x00143852 File Offset: 0x00141C52
	public static UICamera.MouseOrTouch mouse0
	{
		get
		{
			return UICamera.mMouse[0];
		}
	}

	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06002C01 RID: 11265 RVA: 0x0014385B File Offset: 0x00141C5B
	public static UICamera.MouseOrTouch mouse1
	{
		get
		{
			return UICamera.mMouse[1];
		}
	}

	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06002C02 RID: 11266 RVA: 0x00143864 File Offset: 0x00141C64
	public static UICamera.MouseOrTouch mouse2
	{
		get
		{
			return UICamera.mMouse[2];
		}
	}

	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06002C03 RID: 11267 RVA: 0x0014386D File Offset: 0x00141C6D
	private bool handlesEvents
	{
		get
		{
			return UICamera.eventHandler == this;
		}
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06002C04 RID: 11268 RVA: 0x0014387A File Offset: 0x00141C7A
	public Camera cachedCamera
	{
		get
		{
			if (this.mCam == null)
			{
				this.mCam = base.GetComponent<Camera>();
			}
			return this.mCam;
		}
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06002C05 RID: 11269 RVA: 0x0014389F File Offset: 0x00141C9F
	// (set) Token: 0x06002C06 RID: 11270 RVA: 0x001438A6 File Offset: 0x00141CA6
	public static GameObject tooltipObject
	{
		get
		{
			return UICamera.mTooltip;
		}
		set
		{
			UICamera.ShowTooltip(value);
		}
	}

	// Token: 0x06002C07 RID: 11271 RVA: 0x001438AF File Offset: 0x00141CAF
	public static bool IsPartOfUI(GameObject go)
	{
		return !(go == null) && !(go == UICamera.fallThrough) && NGUITools.FindInParents<UIRoot>(go) != null;
	}

	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06002C08 RID: 11272 RVA: 0x001438DC File Offset: 0x00141CDC
	public static bool isOverUI
	{
		get
		{
			int frameCount = Time.frameCount;
			if (UICamera.mLastOverCheck != frameCount)
			{
				UICamera.mLastOverCheck = frameCount;
				if (UICamera.currentTouch != null)
				{
					if (UICamera.currentTouch.pressed != null)
					{
						UICamera.mLastOverResult = UICamera.IsPartOfUI(UICamera.currentTouch.pressed);
						return UICamera.mLastOverResult;
					}
					UICamera.mLastOverResult = UICamera.IsPartOfUI(UICamera.currentTouch.current);
					return UICamera.mLastOverResult;
				}
				else
				{
					int i = 0;
					int count = UICamera.activeTouches.Count;
					while (i < count)
					{
						UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[i];
						if (UICamera.IsPartOfUI(mouseOrTouch.pressed))
						{
							UICamera.mLastOverResult = true;
							return UICamera.mLastOverResult;
						}
						i++;
					}
					for (int j = 0; j < 3; j++)
					{
						UICamera.MouseOrTouch mouseOrTouch2 = UICamera.mMouse[j];
						if (UICamera.IsPartOfUI((!(mouseOrTouch2.pressed != null)) ? mouseOrTouch2.current : mouseOrTouch2.pressed))
						{
							UICamera.mLastOverResult = true;
							return UICamera.mLastOverResult;
						}
					}
					UICamera.mLastOverResult = UICamera.IsPartOfUI(UICamera.controller.pressed);
				}
			}
			return UICamera.mLastOverResult;
		}
	}

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x06002C09 RID: 11273 RVA: 0x00143A0C File Offset: 0x00141E0C
	public static bool uiHasFocus
	{
		get
		{
			int frameCount = Time.frameCount;
			if (UICamera.mLastFocusCheck != frameCount)
			{
				UICamera.mLastFocusCheck = frameCount;
				if (UICamera.inputHasFocus)
				{
					UICamera.mLastFocusResult = true;
					return UICamera.mLastFocusResult;
				}
				if (UICamera.currentTouch != null)
				{
					UICamera.mLastFocusResult = UICamera.currentTouch.isOverUI;
					return UICamera.mLastFocusResult;
				}
				int i = 0;
				int count = UICamera.activeTouches.Count;
				while (i < count)
				{
					UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[i];
					if (UICamera.IsPartOfUI(mouseOrTouch.pressed))
					{
						UICamera.mLastFocusResult = true;
						return UICamera.mLastFocusResult;
					}
					i++;
				}
				for (int j = 0; j < 3; j++)
				{
					UICamera.MouseOrTouch mouseOrTouch2 = UICamera.mMouse[j];
					if (UICamera.IsPartOfUI(mouseOrTouch2.pressed) || UICamera.IsPartOfUI(mouseOrTouch2.current))
					{
						UICamera.mLastFocusResult = true;
						return UICamera.mLastFocusResult;
					}
				}
				UICamera.mLastFocusResult = UICamera.IsPartOfUI(UICamera.controller.pressed);
			}
			return UICamera.mLastFocusResult;
		}
	}

	// Token: 0x170002DB RID: 731
	// (get) Token: 0x06002C0A RID: 11274 RVA: 0x00143B14 File Offset: 0x00141F14
	public static bool interactingWithUI
	{
		get
		{
			int frameCount = Time.frameCount;
			if (UICamera.mLastInteractionCheck != frameCount)
			{
				UICamera.mLastInteractionCheck = frameCount;
				if (UICamera.inputHasFocus)
				{
					UICamera.mLastInteractionResult = true;
					return UICamera.mLastInteractionResult;
				}
				int i = 0;
				int count = UICamera.activeTouches.Count;
				while (i < count)
				{
					UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[i];
					if (UICamera.IsPartOfUI(mouseOrTouch.pressed))
					{
						UICamera.mLastInteractionResult = true;
						return UICamera.mLastInteractionResult;
					}
					i++;
				}
				for (int j = 0; j < 3; j++)
				{
					UICamera.MouseOrTouch mouseOrTouch2 = UICamera.mMouse[j];
					if (UICamera.IsPartOfUI(mouseOrTouch2.pressed))
					{
						UICamera.mLastInteractionResult = true;
						return UICamera.mLastInteractionResult;
					}
				}
				UICamera.mLastInteractionResult = UICamera.IsPartOfUI(UICamera.controller.pressed);
			}
			return UICamera.mLastInteractionResult;
		}
	}

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x06002C0B RID: 11275 RVA: 0x00143BEC File Offset: 0x00141FEC
	// (set) Token: 0x06002C0C RID: 11276 RVA: 0x00143C54 File Offset: 0x00142054
	public static GameObject hoveredObject
	{
		get
		{
			if (UICamera.currentTouch != null && (UICamera.currentScheme != UICamera.ControlScheme.Mouse || UICamera.currentTouch.dragStarted))
			{
				return UICamera.currentTouch.current;
			}
			if (UICamera.mHover && UICamera.mHover.activeInHierarchy)
			{
				return UICamera.mHover;
			}
			UICamera.mHover = null;
			return null;
		}
		set
		{
			if (UICamera.mHover == value)
			{
				return;
			}
			bool flag = false;
			UICamera uicamera = UICamera.current;
			if (UICamera.currentTouch == null)
			{
				flag = true;
				UICamera.currentTouchID = -100;
				UICamera.currentTouch = UICamera.controller;
			}
			UICamera.ShowTooltip(null);
			if (UICamera.mSelected && UICamera.currentScheme == UICamera.ControlScheme.Controller)
			{
				UICamera.Notify(UICamera.mSelected, "OnSelect", false);
				if (UICamera.onSelect != null)
				{
					UICamera.onSelect(UICamera.mSelected, false);
				}
				UICamera.mSelected = null;
			}
			if (UICamera.mHover)
			{
				UICamera.Notify(UICamera.mHover, "OnHover", false);
				if (UICamera.onHover != null)
				{
					UICamera.onHover(UICamera.mHover, false);
				}
			}
			UICamera.mHover = value;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
			if (UICamera.mHover)
			{
				if (UICamera.mHover != UICamera.controller.current && UICamera.mHover.GetComponent<UIKeyNavigation>() != null)
				{
					UICamera.controller.current = UICamera.mHover;
				}
				if (flag)
				{
					UICamera uicamera2 = (!(UICamera.mHover != null)) ? UICamera.list[0] : UICamera.FindCameraForLayer(UICamera.mHover.layer);
					if (uicamera2 != null)
					{
						UICamera.current = uicamera2;
						UICamera.currentCamera = uicamera2.cachedCamera;
					}
				}
				if (UICamera.onHover != null)
				{
					UICamera.onHover(UICamera.mHover, true);
				}
				UICamera.Notify(UICamera.mHover, "OnHover", true);
			}
			if (flag)
			{
				UICamera.current = uicamera;
				UICamera.currentCamera = ((!(uicamera != null)) ? null : uicamera.cachedCamera);
				UICamera.currentTouch = null;
				UICamera.currentTouchID = -100;
			}
		}
	}

	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06002C0D RID: 11277 RVA: 0x00143E40 File Offset: 0x00142240
	// (set) Token: 0x06002C0E RID: 11278 RVA: 0x00143FA8 File Offset: 0x001423A8
	public static GameObject controllerNavigationObject
	{
		get
		{
			if (UICamera.controller.current && UICamera.controller.current.activeInHierarchy)
			{
				return UICamera.controller.current;
			}
			if (UICamera.currentScheme == UICamera.ControlScheme.Controller && UICamera.current != null && UICamera.current.useController && !UICamera.ignoreControllerInput && UIKeyNavigation.list.size > 0)
			{
				for (int i = 0; i < UIKeyNavigation.list.size; i++)
				{
					UIKeyNavigation uikeyNavigation = UIKeyNavigation.list[i];
					if (uikeyNavigation && uikeyNavigation.constraint != UIKeyNavigation.Constraint.Explicit && uikeyNavigation.startsSelected)
					{
						UICamera.hoveredObject = uikeyNavigation.gameObject;
						UICamera.controller.current = UICamera.mHover;
						return UICamera.mHover;
					}
				}
				if (UICamera.mHover == null)
				{
					for (int j = 0; j < UIKeyNavigation.list.size; j++)
					{
						UIKeyNavigation uikeyNavigation2 = UIKeyNavigation.list[j];
						if (uikeyNavigation2 && uikeyNavigation2.constraint != UIKeyNavigation.Constraint.Explicit)
						{
							UICamera.hoveredObject = uikeyNavigation2.gameObject;
							UICamera.controller.current = UICamera.mHover;
							return UICamera.mHover;
						}
					}
				}
			}
			UICamera.controller.current = null;
			return null;
		}
		set
		{
			if (UICamera.controller.current != value && UICamera.controller.current)
			{
				UICamera.Notify(UICamera.controller.current, "OnHover", false);
				if (UICamera.onHover != null)
				{
					UICamera.onHover(UICamera.controller.current, false);
				}
				UICamera.controller.current = null;
			}
			UICamera.hoveredObject = value;
		}
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06002C0F RID: 11279 RVA: 0x00144028 File Offset: 0x00142428
	// (set) Token: 0x06002C10 RID: 11280 RVA: 0x00144058 File Offset: 0x00142458
	public static GameObject selectedObject
	{
		get
		{
			if (UICamera.mSelected && UICamera.mSelected.activeInHierarchy)
			{
				return UICamera.mSelected;
			}
			UICamera.mSelected = null;
			return null;
		}
		set
		{
			if (UICamera.mSelected == value)
			{
				UICamera.hoveredObject = value;
				UICamera.controller.current = value;
				return;
			}
			UICamera.ShowTooltip(null);
			bool flag = false;
			UICamera uicamera = UICamera.current;
			if (UICamera.currentTouch == null)
			{
				flag = true;
				UICamera.currentTouchID = -100;
				UICamera.currentTouch = UICamera.controller;
			}
			UICamera.mInputFocus = false;
			if (UICamera.mSelected)
			{
				UICamera.Notify(UICamera.mSelected, "OnSelect", false);
				if (UICamera.onSelect != null)
				{
					UICamera.onSelect(UICamera.mSelected, false);
				}
			}
			UICamera.mSelected = value;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
			if (value != null)
			{
				UIKeyNavigation component = value.GetComponent<UIKeyNavigation>();
				if (component != null)
				{
					UICamera.controller.current = value;
				}
			}
			if (UICamera.mSelected && flag)
			{
				UICamera uicamera2 = (!(UICamera.mSelected != null)) ? UICamera.list[0] : UICamera.FindCameraForLayer(UICamera.mSelected.layer);
				if (uicamera2 != null)
				{
					UICamera.current = uicamera2;
					UICamera.currentCamera = uicamera2.cachedCamera;
				}
			}
			if (UICamera.mSelected)
			{
				UICamera.mInputFocus = (UICamera.mSelected.activeInHierarchy && UICamera.mSelected.GetComponent<UIInput>() != null);
				if (UICamera.onSelect != null)
				{
					UICamera.onSelect(UICamera.mSelected, true);
				}
				UICamera.Notify(UICamera.mSelected, "OnSelect", true);
			}
			if (flag)
			{
				UICamera.current = uicamera;
				UICamera.currentCamera = ((!(uicamera != null)) ? null : uicamera.cachedCamera);
				UICamera.currentTouch = null;
				UICamera.currentTouchID = -100;
			}
		}
	}

	// Token: 0x06002C11 RID: 11281 RVA: 0x00144230 File Offset: 0x00142630
	public static bool IsPressed(GameObject go)
	{
		for (int i = 0; i < 3; i++)
		{
			if (UICamera.mMouse[i].pressed == go)
			{
				return true;
			}
		}
		int j = 0;
		int count = UICamera.activeTouches.Count;
		while (j < count)
		{
			UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[j];
			if (mouseOrTouch.pressed == go)
			{
				return true;
			}
			j++;
		}
		return UICamera.controller.pressed == go;
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06002C12 RID: 11282 RVA: 0x001442BC File Offset: 0x001426BC
	[Obsolete("Use either 'CountInputSources()' or 'activeTouches.Count'")]
	public static int touchCount
	{
		get
		{
			return UICamera.CountInputSources();
		}
	}

	// Token: 0x06002C13 RID: 11283 RVA: 0x001442C4 File Offset: 0x001426C4
	public static int CountInputSources()
	{
		int num = 0;
		int i = 0;
		int count = UICamera.activeTouches.Count;
		while (i < count)
		{
			UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[i];
			if (mouseOrTouch.pressed != null)
			{
				num++;
			}
			i++;
		}
		for (int j = 0; j < UICamera.mMouse.Length; j++)
		{
			if (UICamera.mMouse[j].pressed != null)
			{
				num++;
			}
		}
		if (UICamera.controller.pressed != null)
		{
			num++;
		}
		return num;
	}

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06002C14 RID: 11284 RVA: 0x00144364 File Offset: 0x00142764
	public static int dragCount
	{
		get
		{
			int num = 0;
			int i = 0;
			int count = UICamera.activeTouches.Count;
			while (i < count)
			{
				UICamera.MouseOrTouch mouseOrTouch = UICamera.activeTouches[i];
				if (mouseOrTouch.dragged != null)
				{
					num++;
				}
				i++;
			}
			for (int j = 0; j < UICamera.mMouse.Length; j++)
			{
				if (UICamera.mMouse[j].dragged != null)
				{
					num++;
				}
			}
			if (UICamera.controller.dragged != null)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06002C15 RID: 11285 RVA: 0x00144404 File Offset: 0x00142804
	public static Camera mainCamera
	{
		get
		{
			UICamera eventHandler = UICamera.eventHandler;
			return (!(eventHandler != null)) ? null : eventHandler.cachedCamera;
		}
	}

	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06002C16 RID: 11286 RVA: 0x00144430 File Offset: 0x00142830
	public static UICamera eventHandler
	{
		get
		{
			for (int i = 0; i < UICamera.list.size; i++)
			{
				UICamera uicamera = UICamera.list.buffer[i];
				if (!(uicamera == null) && uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
				{
					return uicamera;
				}
			}
			return null;
		}
	}

	// Token: 0x06002C17 RID: 11287 RVA: 0x00144494 File Offset: 0x00142894
	private static int CompareFunc(UICamera a, UICamera b)
	{
		if (a.cachedCamera.depth < b.cachedCamera.depth)
		{
			return 1;
		}
		if (a.cachedCamera.depth > b.cachedCamera.depth)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06002C18 RID: 11288 RVA: 0x001444D4 File Offset: 0x001428D4
	private static Rigidbody FindRootRigidbody(Transform trans)
	{
		while (trans != null)
		{
			if (trans.GetComponent<UIPanel>() != null)
			{
				break;
			}
			Rigidbody component = trans.GetComponent<Rigidbody>();
			if (component != null)
			{
				return component;
			}
			trans = trans.parent;
		}
		return null;
	}

	// Token: 0x06002C19 RID: 11289 RVA: 0x00144528 File Offset: 0x00142928
	private static Rigidbody2D FindRootRigidbody2D(Transform trans)
	{
		while (trans != null)
		{
			if (trans.GetComponent<UIPanel>() != null)
			{
				break;
			}
			Rigidbody2D component = trans.GetComponent<Rigidbody2D>();
			if (component != null)
			{
				return component;
			}
			trans = trans.parent;
		}
		return null;
	}

	// Token: 0x06002C1A RID: 11290 RVA: 0x0014457C File Offset: 0x0014297C
	public static void Raycast(UICamera.MouseOrTouch touch)
	{
		if (!UICamera.Raycast(touch.pos))
		{
			UICamera.mRayHitObject = UICamera.fallThrough;
		}
		if (UICamera.mRayHitObject == null)
		{
			UICamera.mRayHitObject = UICamera.mGenericHandler;
		}
		touch.last = touch.current;
		touch.current = UICamera.mRayHitObject;
		UICamera.mLastPos = touch.pos;
	}

	// Token: 0x06002C1B RID: 11291 RVA: 0x001445E4 File Offset: 0x001429E4
	public static bool Raycast(Vector3 inPos)
	{
		for (int i = 0; i < UICamera.list.size; i++)
		{
			UICamera uicamera = UICamera.list.buffer[i];
			if (uicamera.enabled && NGUITools.GetActive(uicamera.gameObject))
			{
				UICamera.currentCamera = uicamera.cachedCamera;
				if (UICamera.currentCamera.targetDisplay == 0)
				{
					Vector3 vector = UICamera.currentCamera.ScreenToViewportPoint(inPos);
					if (!float.IsNaN(vector.x) && !float.IsNaN(vector.y))
					{
						if (vector.x >= 0f && vector.x <= 1f && vector.y >= 0f && vector.y <= 1f)
						{
							Ray ray = UICamera.currentCamera.ScreenPointToRay(inPos);
							int layerMask = UICamera.currentCamera.cullingMask & uicamera.eventReceiverMask;
							float num = (uicamera.rangeDistance <= 0f) ? (UICamera.currentCamera.farClipPlane - UICamera.currentCamera.nearClipPlane) : uicamera.rangeDistance;
							if (uicamera.eventType == UICamera.EventType.World_3D)
							{
								UICamera.lastWorldRay = ray;
								if (Physics.Raycast(ray, out UICamera.lastHit, num, layerMask, QueryTriggerInteraction.Ignore))
								{
									UICamera.lastWorldPosition = UICamera.lastHit.point;
									UICamera.mRayHitObject = UICamera.lastHit.collider.gameObject;
									if (!uicamera.eventsGoToColliders)
									{
										Rigidbody componentInParent = UICamera.mRayHitObject.gameObject.GetComponentInParent<Rigidbody>();
										if (componentInParent != null)
										{
											UICamera.mRayHitObject = componentInParent.gameObject;
										}
									}
									return true;
								}
							}
							else if (uicamera.eventType == UICamera.EventType.UI_3D)
							{
								if (UICamera.mRayHits == null)
								{
									UICamera.mRayHits = new RaycastHit[50];
								}
								int num2 = Physics.RaycastNonAlloc(ray, UICamera.mRayHits, num, layerMask, QueryTriggerInteraction.Collide);
								if (num2 > 1)
								{
									int j = 0;
									while (j < num2)
									{
										GameObject gameObject = UICamera.mRayHits[j].collider.gameObject;
										UIWidget component = gameObject.GetComponent<UIWidget>();
										if (component != null)
										{
											if (component.isVisible)
											{
												if (component.hitCheck == null || component.hitCheck(UICamera.mRayHits[j].point))
												{
													goto IL_291;
												}
											}
										}
										else
										{
											UIRect uirect = NGUITools.FindInParents<UIRect>(gameObject);
											if (!(uirect != null) || uirect.finalAlpha >= 0.001f)
											{
												goto IL_291;
											}
										}
										IL_31B:
										j++;
										continue;
										IL_291:
										UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject);
										if (UICamera.mHit.depth != 2147483647)
										{
											UICamera.mHit.hit = UICamera.mRayHits[j];
											UICamera.mHit.point = UICamera.mRayHits[j].point;
											UICamera.mHit.go = UICamera.mRayHits[j].collider.gameObject;
											UICamera.mHits.Add(UICamera.mHit);
											goto IL_31B;
										}
										goto IL_31B;
									}
									UICamera.mHits.Sort((UICamera.DepthEntry r1, UICamera.DepthEntry r2) => r2.depth.CompareTo(r1.depth));
									for (int k = 0; k < UICamera.mHits.size; k++)
									{
										if (UICamera.IsVisible(ref UICamera.mHits.buffer[k]))
										{
											UICamera.lastHit = UICamera.mHits[k].hit;
											UICamera.mRayHitObject = UICamera.mHits[k].go;
											UICamera.lastWorldRay = ray;
											UICamera.lastWorldPosition = UICamera.mHits[k].point;
											UICamera.mHits.Clear();
											return true;
										}
									}
									UICamera.mHits.Clear();
								}
								else if (num2 == 1)
								{
									GameObject gameObject2 = UICamera.mRayHits[0].collider.gameObject;
									UIWidget component2 = gameObject2.GetComponent<UIWidget>();
									if (component2 != null)
									{
										if (!component2.isVisible)
										{
											goto IL_851;
										}
										if (component2.hitCheck != null && !component2.hitCheck(UICamera.mRayHits[0].point))
										{
											goto IL_851;
										}
									}
									else
									{
										UIRect uirect2 = NGUITools.FindInParents<UIRect>(gameObject2);
										if (uirect2 != null && uirect2.finalAlpha < 0.001f)
										{
											goto IL_851;
										}
									}
									if (UICamera.IsVisible(UICamera.mRayHits[0].point, UICamera.mRayHits[0].collider.gameObject))
									{
										UICamera.lastHit = UICamera.mRayHits[0];
										UICamera.lastWorldRay = ray;
										UICamera.lastWorldPosition = UICamera.mRayHits[0].point;
										UICamera.mRayHitObject = UICamera.lastHit.collider.gameObject;
										return true;
									}
								}
							}
							else if (uicamera.eventType == UICamera.EventType.World_2D)
							{
								if (UICamera.m2DPlane.Raycast(ray, out num))
								{
									Vector3 point = ray.GetPoint(num);
									Collider2D collider2D = Physics2D.OverlapPoint(point, layerMask);
									if (collider2D)
									{
										UICamera.lastWorldPosition = point;
										UICamera.mRayHitObject = collider2D.gameObject;
										if (!uicamera.eventsGoToColliders)
										{
											Rigidbody2D rigidbody2D = UICamera.FindRootRigidbody2D(UICamera.mRayHitObject.transform);
											if (rigidbody2D != null)
											{
												UICamera.mRayHitObject = rigidbody2D.gameObject;
											}
										}
										return true;
									}
								}
							}
							else if (uicamera.eventType == UICamera.EventType.UI_2D)
							{
								if (UICamera.m2DPlane.Raycast(ray, out num))
								{
									UICamera.lastWorldPosition = ray.GetPoint(num);
									if (UICamera.mOverlap == null)
									{
										UICamera.mOverlap = new Collider2D[50];
									}
									int num3 = Physics2D.OverlapPointNonAlloc(UICamera.lastWorldPosition, UICamera.mOverlap, layerMask);
									if (num3 > 1)
									{
										int l = 0;
										while (l < num3)
										{
											GameObject gameObject3 = UICamera.mOverlap[l].gameObject;
											UIWidget component3 = gameObject3.GetComponent<UIWidget>();
											if (component3 != null)
											{
												if (component3.isVisible)
												{
													if (component3.hitCheck == null || component3.hitCheck(UICamera.lastWorldPosition))
													{
														goto IL_6A9;
													}
												}
											}
											else
											{
												UIRect uirect3 = NGUITools.FindInParents<UIRect>(gameObject3);
												if (!(uirect3 != null) || uirect3.finalAlpha >= 0.001f)
												{
													goto IL_6A9;
												}
											}
											IL_6F8:
											l++;
											continue;
											IL_6A9:
											UICamera.mHit.depth = NGUITools.CalculateRaycastDepth(gameObject3);
											if (UICamera.mHit.depth != 2147483647)
											{
												UICamera.mHit.go = gameObject3;
												UICamera.mHit.point = UICamera.lastWorldPosition;
												UICamera.mHits.Add(UICamera.mHit);
												goto IL_6F8;
											}
											goto IL_6F8;
										}
										UICamera.mHits.Sort((UICamera.DepthEntry r1, UICamera.DepthEntry r2) => r2.depth.CompareTo(r1.depth));
										for (int m = 0; m < UICamera.mHits.size; m++)
										{
											if (UICamera.IsVisible(ref UICamera.mHits.buffer[m]))
											{
												UICamera.mRayHitObject = UICamera.mHits[m].go;
												UICamera.mHits.Clear();
												return true;
											}
										}
										UICamera.mHits.Clear();
									}
									else if (num3 == 1)
									{
										GameObject gameObject4 = UICamera.mOverlap[0].gameObject;
										UIWidget component4 = gameObject4.GetComponent<UIWidget>();
										if (component4 != null)
										{
											if (!component4.isVisible)
											{
												goto IL_851;
											}
											if (component4.hitCheck != null && !component4.hitCheck(UICamera.lastWorldPosition))
											{
												goto IL_851;
											}
										}
										else
										{
											UIRect uirect4 = NGUITools.FindInParents<UIRect>(gameObject4);
											if (uirect4 != null && uirect4.finalAlpha < 0.001f)
											{
												goto IL_851;
											}
										}
										if (UICamera.IsVisible(UICamera.lastWorldPosition, gameObject4))
										{
											UICamera.mRayHitObject = gameObject4;
											return true;
										}
									}
								}
							}
						}
					}
				}
			}
			IL_851:;
		}
		return false;
	}

	// Token: 0x06002C1C RID: 11292 RVA: 0x00144E58 File Offset: 0x00143258
	private static bool IsVisible(Vector3 worldPoint, GameObject go)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(go);
		while (uipanel != null)
		{
			if (!uipanel.IsVisible(worldPoint))
			{
				return false;
			}
			uipanel = uipanel.parentPanel;
		}
		return true;
	}

	// Token: 0x06002C1D RID: 11293 RVA: 0x00144E94 File Offset: 0x00143294
	private static bool IsVisible(ref UICamera.DepthEntry de)
	{
		UIPanel uipanel = NGUITools.FindInParents<UIPanel>(de.go);
		while (uipanel != null)
		{
			if (!uipanel.IsVisible(de.point))
			{
				return false;
			}
			uipanel = uipanel.parentPanel;
		}
		return true;
	}

	// Token: 0x06002C1E RID: 11294 RVA: 0x00144ED9 File Offset: 0x001432D9
	public static bool IsHighlighted(GameObject go)
	{
		return UICamera.hoveredObject == go;
	}

	// Token: 0x06002C1F RID: 11295 RVA: 0x00144EE8 File Offset: 0x001432E8
	public static UICamera FindCameraForLayer(int layer)
	{
		int num = 1 << layer;
		for (int i = 0; i < UICamera.list.size; i++)
		{
			UICamera uicamera = UICamera.list.buffer[i];
			Camera cachedCamera = uicamera.cachedCamera;
			if (cachedCamera != null && (cachedCamera.cullingMask & num) != 0)
			{
				return uicamera;
			}
		}
		return null;
	}

	// Token: 0x06002C20 RID: 11296 RVA: 0x00144F47 File Offset: 0x00143347
	private static int GetDirection(KeyCode up, KeyCode down)
	{
		if (UICamera.GetKeyDown(up))
		{
			UICamera.currentKey = up;
			return 1;
		}
		if (UICamera.GetKeyDown(down))
		{
			UICamera.currentKey = down;
			return -1;
		}
		return 0;
	}

	// Token: 0x06002C21 RID: 11297 RVA: 0x00144F7C File Offset: 0x0014337C
	private static int GetDirection(KeyCode up0, KeyCode up1, KeyCode down0, KeyCode down1)
	{
		if (UICamera.GetKeyDown(up0))
		{
			UICamera.currentKey = up0;
			return 1;
		}
		if (UICamera.GetKeyDown(up1))
		{
			UICamera.currentKey = up1;
			return 1;
		}
		if (UICamera.GetKeyDown(down0))
		{
			UICamera.currentKey = down0;
			return -1;
		}
		if (UICamera.GetKeyDown(down1))
		{
			UICamera.currentKey = down1;
			return -1;
		}
		return 0;
	}

	// Token: 0x06002C22 RID: 11298 RVA: 0x00144FEC File Offset: 0x001433EC
	private static int GetDirection(string axis)
	{
		float time = RealTime.time;
		if (UICamera.mNextEvent < time && !string.IsNullOrEmpty(axis))
		{
			float num = UICamera.GetAxis(axis);
			if (num > 0.75f)
			{
				UICamera.currentKey = KeyCode.JoystickButton0;
				UICamera.mNextEvent = time + 0.25f;
				return 1;
			}
			if (num < -0.75f)
			{
				UICamera.currentKey = KeyCode.JoystickButton0;
				UICamera.mNextEvent = time + 0.25f;
				return -1;
			}
		}
		return 0;
	}

	// Token: 0x06002C23 RID: 11299 RVA: 0x00145068 File Offset: 0x00143468
	public static void Notify(GameObject go, string funcName, object obj)
	{
		if (UICamera.mNotifying > 10)
		{
			return;
		}
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller && UIPopupList.isOpen && UIPopupList.current.source == go && UIPopupList.isOpen)
		{
			go = UIPopupList.current.gameObject;
		}
		if (go && go.activeInHierarchy)
		{
			UICamera.mNotifying++;
			go.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
			if (UICamera.mGenericHandler != null && UICamera.mGenericHandler != go)
			{
				UICamera.mGenericHandler.SendMessage(funcName, obj, SendMessageOptions.DontRequireReceiver);
			}
			UICamera.mNotifying--;
		}
	}

	// Token: 0x06002C24 RID: 11300 RVA: 0x00145128 File Offset: 0x00143528
	private void Awake()
	{
		UICamera.mWidth = Screen.width;
		UICamera.mHeight = Screen.height;
		UICamera.currentScheme = UICamera.ControlScheme.Touch;
		UICamera.mMouse[0].pos = Input.mousePosition;
		for (int i = 1; i < 3; i++)
		{
			UICamera.mMouse[i].pos = UICamera.mMouse[0].pos;
			UICamera.mMouse[i].lastPos = UICamera.mMouse[0].pos;
		}
		UICamera.mLastPos = UICamera.mMouse[0].pos;
	}

	// Token: 0x06002C25 RID: 11301 RVA: 0x001451B8 File Offset: 0x001435B8
	private void OnEnable()
	{
		UICamera.list.Add(this);
		BetterList<UICamera> betterList = UICamera.list;
		if (UICamera.<>f__mg$cache0 == null)
		{
			UICamera.<>f__mg$cache0 = new BetterList<UICamera>.CompareFunc(UICamera.CompareFunc);
		}
		betterList.Sort(UICamera.<>f__mg$cache0);
	}

	// Token: 0x06002C26 RID: 11302 RVA: 0x001451EC File Offset: 0x001435EC
	private void OnDisable()
	{
		UICamera.list.Remove(this);
	}

	// Token: 0x06002C27 RID: 11303 RVA: 0x001451FC File Offset: 0x001435FC
	private void Start()
	{
		BetterList<UICamera> betterList = UICamera.list;
		if (UICamera.<>f__mg$cache1 == null)
		{
			UICamera.<>f__mg$cache1 = new BetterList<UICamera>.CompareFunc(UICamera.CompareFunc);
		}
		betterList.Sort(UICamera.<>f__mg$cache1);
		if (this.eventType != UICamera.EventType.World_3D && this.cachedCamera.transparencySortMode != TransparencySortMode.Orthographic)
		{
			this.cachedCamera.transparencySortMode = TransparencySortMode.Orthographic;
		}
		if (Application.isPlaying)
		{
			if (UICamera.fallThrough == null)
			{
				UIRoot uiroot = NGUITools.FindInParents<UIRoot>(base.gameObject);
				UICamera.fallThrough = ((!(uiroot != null)) ? base.gameObject : uiroot.gameObject);
			}
			this.cachedCamera.eventMask = 0;
		}
	}

	// Token: 0x06002C28 RID: 11304 RVA: 0x001452AC File Offset: 0x001436AC
	[ContextMenu("Start ignoring events")]
	private void StartIgnoring()
	{
		UICamera.ignoreAllEvents = true;
	}

	// Token: 0x06002C29 RID: 11305 RVA: 0x001452B4 File Offset: 0x001436B4
	[ContextMenu("Stop ignoring events")]
	private void StopIgnoring()
	{
		UICamera.ignoreAllEvents = false;
	}

	// Token: 0x06002C2A RID: 11306 RVA: 0x001452BC File Offset: 0x001436BC
	private void Update()
	{
		if (UICamera.ignoreAllEvents)
		{
			return;
		}
		if (!this.handlesEvents)
		{
			return;
		}
		if (this.processEventsIn == UICamera.ProcessEventsIn.Update)
		{
			this.ProcessEvents();
		}
	}

	// Token: 0x06002C2B RID: 11307 RVA: 0x001452E8 File Offset: 0x001436E8
	private void LateUpdate()
	{
		if (!this.handlesEvents)
		{
			return;
		}
		if (this.processEventsIn == UICamera.ProcessEventsIn.LateUpdate)
		{
			this.ProcessEvents();
		}
		int width = Screen.width;
		int height = Screen.height;
		if (width != UICamera.mWidth || height != UICamera.mHeight)
		{
			UICamera.mWidth = width;
			UICamera.mHeight = height;
			UIRoot.Broadcast("UpdateAnchors");
			if (UICamera.onScreenResize != null)
			{
				UICamera.onScreenResize();
			}
		}
	}

	// Token: 0x06002C2C RID: 11308 RVA: 0x00145360 File Offset: 0x00143760
	private void ProcessEvents()
	{
		UICamera.current = this;
		NGUIDebug.debugRaycast = this.debug;
		if (this.useTouch)
		{
			this.ProcessTouches();
		}
		else if (this.useMouse)
		{
			this.ProcessMouse();
		}
		if (UICamera.onCustomInput != null)
		{
			UICamera.onCustomInput();
		}
		if ((this.useKeyboard || this.useController) && !UICamera.disableController && !UICamera.ignoreControllerInput)
		{
			this.ProcessOthers();
		}
		if (this.useMouse && UICamera.mHover != null)
		{
			float num = string.IsNullOrEmpty(this.scrollAxisName) ? 0f : UICamera.GetAxis(this.scrollAxisName);
			if (num != 0f)
			{
				if (UICamera.onScroll != null)
				{
					UICamera.onScroll(UICamera.mHover, num);
				}
				UICamera.Notify(UICamera.mHover, "OnScroll", num);
			}
			if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.showTooltips && UICamera.mTooltipTime != 0f && !UIPopupList.isOpen && UICamera.mMouse[0].dragged == null && (UICamera.mTooltipTime < RealTime.time || UICamera.GetKey(KeyCode.LeftShift) || UICamera.GetKey(KeyCode.RightShift)))
			{
				UICamera.currentTouch = UICamera.mMouse[0];
				UICamera.currentTouchID = -1;
				UICamera.ShowTooltip(UICamera.mHover);
			}
		}
		if (UICamera.mTooltip != null && !NGUITools.GetActive(UICamera.mTooltip))
		{
			UICamera.ShowTooltip(null);
		}
		UICamera.current = null;
		UICamera.currentTouchID = -100;
	}

	// Token: 0x06002C2D RID: 11309 RVA: 0x00145534 File Offset: 0x00143934
	public void ProcessMouse()
	{
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < 3; i++)
		{
			if (Input.GetMouseButtonDown(i))
			{
				UICamera.currentKey = KeyCode.Mouse0 + i;
				flag2 = true;
				flag = true;
			}
			else if (Input.GetMouseButton(i))
			{
				UICamera.currentKey = KeyCode.Mouse0 + i;
				flag = true;
			}
		}
		if (UICamera.currentScheme == UICamera.ControlScheme.Touch && UICamera.activeTouches.Count > 0)
		{
			return;
		}
		UICamera.currentTouch = UICamera.mMouse[0];
		Vector2 vector = Input.mousePosition;
		if (UICamera.currentTouch.ignoreDelta == 0)
		{
			UICamera.currentTouch.delta = vector - UICamera.currentTouch.pos;
		}
		else
		{
			UICamera.currentTouch.ignoreDelta--;
			UICamera.currentTouch.delta.x = 0f;
			UICamera.currentTouch.delta.y = 0f;
		}
		float sqrMagnitude = UICamera.currentTouch.delta.sqrMagnitude;
		UICamera.currentTouch.pos = vector;
		UICamera.mLastPos = vector;
		bool flag3 = false;
		if (UICamera.currentScheme != UICamera.ControlScheme.Mouse)
		{
			if (sqrMagnitude < 0.001f)
			{
				return;
			}
			UICamera.currentKey = KeyCode.Mouse0;
			flag3 = true;
		}
		else if (sqrMagnitude > 0.001f)
		{
			flag3 = true;
		}
		for (int j = 1; j < 3; j++)
		{
			UICamera.mMouse[j].pos = UICamera.currentTouch.pos;
			UICamera.mMouse[j].delta = UICamera.currentTouch.delta;
		}
		if (flag || flag3 || this.mNextRaycast < RealTime.time)
		{
			this.mNextRaycast = RealTime.time + 0.02f;
			UICamera.Raycast(UICamera.currentTouch);
			if (flag)
			{
				flag3 = true;
				for (int k = 0; k < 3; k++)
				{
					UICamera.mMouse[k].current = UICamera.currentTouch.current;
				}
			}
			else if (UICamera.mMouse[0].current != UICamera.currentTouch.current)
			{
				UICamera.currentKey = KeyCode.Mouse0;
				flag3 = true;
				for (int l = 0; l < 3; l++)
				{
					UICamera.mMouse[l].current = UICamera.currentTouch.current;
				}
			}
		}
		bool flag4 = UICamera.currentTouch.last != UICamera.currentTouch.current;
		bool flag5 = UICamera.currentTouch.pressed != null;
		if (!flag5 && flag3)
		{
			UICamera.hoveredObject = UICamera.currentTouch.current;
		}
		UICamera.currentTouchID = -1;
		if (flag4)
		{
			UICamera.currentKey = KeyCode.Mouse0;
		}
		if (!flag && flag3 && (!this.stickyTooltip || flag4))
		{
			if (UICamera.mTooltipTime != 0f)
			{
				UICamera.mTooltipTime = Time.unscaledTime + this.tooltipDelay;
			}
			else if (UICamera.mTooltip != null)
			{
				UICamera.ShowTooltip(null);
			}
		}
		if (flag3 && UICamera.onMouseMove != null)
		{
			UICamera.onMouseMove(UICamera.currentTouch.delta);
			UICamera.currentTouch = null;
		}
		if (flag4 && (flag2 || (flag5 && !flag)))
		{
			UICamera.hoveredObject = null;
		}
		for (int m = 0; m < 3; m++)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(m);
			bool mouseButtonUp = Input.GetMouseButtonUp(m);
			if (mouseButtonDown || mouseButtonUp)
			{
				UICamera.currentKey = KeyCode.Mouse0 + m;
			}
			UICamera.currentTouch = UICamera.mMouse[m];
			UICamera.currentTouchID = -1 - m;
			UICamera.currentKey = KeyCode.Mouse0 + m;
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
				UICamera.currentTouch.pressTime = RealTime.time;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			this.ProcessTouch(mouseButtonDown, mouseButtonUp);
		}
		if (!flag && flag4)
		{
			UICamera.currentTouch = UICamera.mMouse[0];
			UICamera.mTooltipTime = Time.unscaledTime + this.tooltipDelay;
			UICamera.currentTouchID = -1;
			UICamera.currentKey = KeyCode.Mouse0;
			UICamera.hoveredObject = UICamera.currentTouch.current;
		}
		UICamera.currentTouch = null;
		UICamera.mMouse[0].last = UICamera.mMouse[0].current;
		for (int n = 1; n < 3; n++)
		{
			UICamera.mMouse[n].last = UICamera.mMouse[0].last;
		}
	}

	// Token: 0x06002C2E RID: 11310 RVA: 0x001459F4 File Offset: 0x00143DF4
	public void ProcessTouches()
	{
		int num = (UICamera.GetInputTouchCount != null) ? UICamera.GetInputTouchCount() : Input.touchCount;
		for (int i = 0; i < num; i++)
		{
			TouchPhase phase;
			int fingerId;
			Vector2 position;
			int tapCount;
			if (UICamera.GetInputTouch == null)
			{
				UnityEngine.Touch touch = Input.GetTouch(i);
				phase = touch.phase;
				fingerId = touch.fingerId;
				position = touch.position;
				tapCount = touch.tapCount;
			}
			else
			{
				UICamera.Touch touch2 = UICamera.GetInputTouch(i);
				phase = touch2.phase;
				fingerId = touch2.fingerId;
				position = touch2.position;
				tapCount = touch2.tapCount;
			}
			UICamera.currentTouchID = ((!this.allowMultiTouch) ? 1 : fingerId);
			UICamera.currentTouch = UICamera.GetTouch(UICamera.currentTouchID, true);
			bool flag = phase == TouchPhase.Began || UICamera.currentTouch.touchBegan;
			bool flag2 = phase == TouchPhase.Canceled || phase == TouchPhase.Ended;
			UICamera.currentTouch.delta = position - UICamera.currentTouch.pos;
			UICamera.currentTouch.pos = position;
			UICamera.currentKey = KeyCode.None;
			UICamera.Raycast(UICamera.currentTouch);
			if (flag)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			if (tapCount > 1)
			{
				UICamera.currentTouch.clickTime = RealTime.time;
			}
			this.ProcessTouch(flag, flag2);
			if (flag2)
			{
				UICamera.RemoveTouch(UICamera.currentTouchID);
			}
			UICamera.currentTouch.touchBegan = false;
			UICamera.currentTouch.last = null;
			UICamera.currentTouch = null;
			if (!this.allowMultiTouch)
			{
				break;
			}
		}
		if (num == 0)
		{
			if (UICamera.mUsingTouchEvents)
			{
				UICamera.mUsingTouchEvents = false;
				return;
			}
			if (this.useMouse)
			{
				this.ProcessMouse();
			}
		}
		else
		{
			UICamera.mUsingTouchEvents = true;
		}
	}

	// Token: 0x06002C2F RID: 11311 RVA: 0x00145BF8 File Offset: 0x00143FF8
	private void ProcessFakeTouches()
	{
		bool mouseButtonDown = Input.GetMouseButtonDown(0);
		bool mouseButtonUp = Input.GetMouseButtonUp(0);
		bool mouseButton = Input.GetMouseButton(0);
		if (mouseButtonDown || mouseButtonUp || mouseButton)
		{
			UICamera.currentTouchID = 1;
			UICamera.currentTouch = UICamera.mMouse[0];
			UICamera.currentTouch.touchBegan = mouseButtonDown;
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressTime = RealTime.time;
				UICamera.activeTouches.Add(UICamera.currentTouch);
			}
			Vector2 vector = Input.mousePosition;
			UICamera.currentTouch.delta = vector - UICamera.currentTouch.pos;
			UICamera.currentTouch.pos = vector;
			UICamera.Raycast(UICamera.currentTouch);
			if (mouseButtonDown)
			{
				UICamera.currentTouch.pressedCam = UICamera.currentCamera;
			}
			else if (UICamera.currentTouch.pressed != null)
			{
				UICamera.currentCamera = UICamera.currentTouch.pressedCam;
			}
			UICamera.currentKey = KeyCode.None;
			this.ProcessTouch(mouseButtonDown, mouseButtonUp);
			if (mouseButtonUp)
			{
				UICamera.activeTouches.Remove(UICamera.currentTouch);
			}
			UICamera.currentTouch.last = null;
			UICamera.currentTouch = null;
		}
	}

	// Token: 0x06002C30 RID: 11312 RVA: 0x00145D1C File Offset: 0x0014411C
	public void ProcessOthers()
	{
		UICamera.currentTouchID = -100;
		UICamera.currentTouch = UICamera.controller;
		bool flag = false;
		bool flag2 = false;
		if (this.submitKey0 != KeyCode.None && UICamera.GetKeyDown(this.submitKey0))
		{
			UICamera.currentKey = this.submitKey0;
			flag = true;
		}
		else if (this.submitKey1 != KeyCode.None && UICamera.GetKeyDown(this.submitKey1))
		{
			UICamera.currentKey = this.submitKey1;
			flag = true;
		}
		else if ((this.submitKey0 == KeyCode.Return || this.submitKey1 == KeyCode.Return) && UICamera.GetKeyDown(KeyCode.KeypadEnter))
		{
			UICamera.currentKey = this.submitKey0;
			flag = true;
		}
		if (this.submitKey0 != KeyCode.None && UICamera.GetKeyUp(this.submitKey0))
		{
			UICamera.currentKey = this.submitKey0;
			flag2 = true;
		}
		else if (this.submitKey1 != KeyCode.None && UICamera.GetKeyUp(this.submitKey1))
		{
			UICamera.currentKey = this.submitKey1;
			flag2 = true;
		}
		else if ((this.submitKey0 == KeyCode.Return || this.submitKey1 == KeyCode.Return) && UICamera.GetKeyUp(KeyCode.KeypadEnter))
		{
			UICamera.currentKey = this.submitKey0;
			flag2 = true;
		}
		if (flag)
		{
			UICamera.currentTouch.pressTime = RealTime.time;
		}
		if ((flag || flag2) && UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			UICamera.currentTouch.current = UICamera.controllerNavigationObject;
			this.ProcessTouch(flag, flag2);
			UICamera.currentTouch.last = UICamera.currentTouch.current;
		}
		KeyCode keyCode = KeyCode.None;
		if (this.useController && !UICamera.ignoreControllerInput)
		{
			if (!UICamera.disableController && UICamera.currentScheme == UICamera.ControlScheme.Controller && (UICamera.currentTouch.current == null || !UICamera.currentTouch.current.activeInHierarchy))
			{
				UICamera.currentTouch.current = UICamera.controllerNavigationObject;
			}
			if (!string.IsNullOrEmpty(this.verticalAxisName))
			{
				int direction = UICamera.GetDirection(this.verticalAxisName);
				if (direction != 0)
				{
					UICamera.ShowTooltip(null);
					UICamera.currentScheme = UICamera.ControlScheme.Controller;
					UICamera.currentTouch.current = UICamera.controllerNavigationObject;
					if (UICamera.currentTouch.current != null)
					{
						keyCode = ((direction <= 0) ? KeyCode.DownArrow : KeyCode.UpArrow);
						if (UICamera.onNavigate != null)
						{
							UICamera.onNavigate(UICamera.currentTouch.current, keyCode);
						}
						UICamera.Notify(UICamera.currentTouch.current, "OnNavigate", keyCode);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.horizontalAxisName))
			{
				int direction2 = UICamera.GetDirection(this.horizontalAxisName);
				if (direction2 != 0)
				{
					UICamera.ShowTooltip(null);
					UICamera.currentScheme = UICamera.ControlScheme.Controller;
					UICamera.currentTouch.current = UICamera.controllerNavigationObject;
					if (UICamera.currentTouch.current != null)
					{
						keyCode = ((direction2 <= 0) ? KeyCode.LeftArrow : KeyCode.RightArrow);
						if (UICamera.onNavigate != null)
						{
							UICamera.onNavigate(UICamera.currentTouch.current, keyCode);
						}
						UICamera.Notify(UICamera.currentTouch.current, "OnNavigate", keyCode);
					}
				}
			}
			float num = string.IsNullOrEmpty(this.horizontalPanAxisName) ? 0f : UICamera.GetAxis(this.horizontalPanAxisName);
			float num2 = string.IsNullOrEmpty(this.verticalPanAxisName) ? 0f : UICamera.GetAxis(this.verticalPanAxisName);
			if (num != 0f || num2 != 0f)
			{
				UICamera.ShowTooltip(null);
				UICamera.currentScheme = UICamera.ControlScheme.Controller;
				UICamera.currentTouch.current = UICamera.controllerNavigationObject;
				if (UICamera.currentTouch.current != null)
				{
					Vector2 vector = new Vector2(num, num2);
					vector *= Time.unscaledDeltaTime;
					if (UICamera.onPan != null)
					{
						UICamera.onPan(UICamera.currentTouch.current, vector);
					}
					UICamera.Notify(UICamera.currentTouch.current, "OnPan", vector);
				}
			}
		}
		if ((UICamera.GetAnyKeyDown == null) ? Input.anyKeyDown : UICamera.GetAnyKeyDown())
		{
			int i = 0;
			int num3 = NGUITools.keys.Length;
			while (i < num3)
			{
				KeyCode keyCode2 = NGUITools.keys[i];
				if (keyCode != keyCode2)
				{
					if (UICamera.GetKeyDown(keyCode2))
					{
						if (this.useKeyboard || keyCode2 >= KeyCode.Mouse0)
						{
							if ((this.useController && !UICamera.ignoreControllerInput) || keyCode2 < KeyCode.JoystickButton0)
							{
								if (this.useMouse || keyCode2 < KeyCode.Mouse0 || keyCode2 > KeyCode.Mouse6)
								{
									UICamera.currentKey = keyCode2;
									if (UICamera.onKey != null)
									{
										UICamera.onKey(UICamera.currentTouch.current, keyCode2);
									}
									UICamera.Notify(UICamera.currentTouch.current, "OnKey", keyCode2);
								}
							}
						}
					}
				}
				i++;
			}
		}
		UICamera.currentTouch = null;
	}

	// Token: 0x06002C31 RID: 11313 RVA: 0x00146298 File Offset: 0x00144698
	private void ProcessPress(bool pressed, float click, float drag)
	{
		if (pressed)
		{
			if (UICamera.mTooltip != null)
			{
				UICamera.ShowTooltip(null);
			}
			UICamera.mTooltipTime = Time.unscaledTime + this.tooltipDelay;
			UICamera.currentTouch.pressStarted = true;
			if (UICamera.onPress != null && UICamera.currentTouch.pressed)
			{
				UICamera.onPress(UICamera.currentTouch.pressed, false);
			}
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
			if (UICamera.currentScheme == UICamera.ControlScheme.Mouse && UICamera.hoveredObject == null && UICamera.currentTouch.current != null)
			{
				UICamera.hoveredObject = UICamera.currentTouch.current;
			}
			UICamera.currentTouch.pressed = UICamera.currentTouch.current;
			UICamera.currentTouch.dragged = UICamera.currentTouch.current;
			UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
			UICamera.currentTouch.totalDelta = Vector2.zero;
			UICamera.currentTouch.dragStarted = false;
			if (UICamera.onPress != null && UICamera.currentTouch.pressed)
			{
				UICamera.onPress(UICamera.currentTouch.pressed, true);
			}
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", true);
			if (UICamera.mSelected != UICamera.currentTouch.pressed)
			{
				UICamera.mInputFocus = false;
				if (UICamera.mSelected)
				{
					UICamera.Notify(UICamera.mSelected, "OnSelect", false);
					if (UICamera.onSelect != null)
					{
						UICamera.onSelect(UICamera.mSelected, false);
					}
				}
				UICamera.mSelected = UICamera.currentTouch.pressed;
				if (UICamera.currentTouch.pressed != null)
				{
					UIKeyNavigation component = UICamera.currentTouch.pressed.GetComponent<UIKeyNavigation>();
					if (component != null)
					{
						UICamera.controller.current = UICamera.currentTouch.pressed;
					}
				}
				if (UICamera.mSelected)
				{
					UICamera.mInputFocus = (UICamera.mSelected.activeInHierarchy && UICamera.mSelected.GetComponent<UIInput>() != null);
					if (UICamera.onSelect != null)
					{
						UICamera.onSelect(UICamera.mSelected, true);
					}
					UICamera.Notify(UICamera.mSelected, "OnSelect", true);
				}
			}
		}
		else if (UICamera.currentTouch.pressed != null && (UICamera.currentTouch.delta.sqrMagnitude != 0f || UICamera.currentTouch.current != UICamera.currentTouch.last))
		{
			UICamera.currentTouch.totalDelta += UICamera.currentTouch.delta;
			float sqrMagnitude = UICamera.currentTouch.totalDelta.sqrMagnitude;
			bool flag = false;
			if (!UICamera.currentTouch.dragStarted && UICamera.currentTouch.last != UICamera.currentTouch.current)
			{
				UICamera.currentTouch.dragStarted = true;
				UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
				UICamera.isDragging = true;
				if (UICamera.onDragStart != null)
				{
					UICamera.onDragStart(UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", null);
				if (UICamera.onDragOver != null)
				{
					UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.last, "OnDragOver", UICamera.currentTouch.dragged);
				UICamera.isDragging = false;
			}
			else if (!UICamera.currentTouch.dragStarted && drag < sqrMagnitude)
			{
				flag = true;
				UICamera.currentTouch.dragStarted = true;
				UICamera.currentTouch.delta = UICamera.currentTouch.totalDelta;
			}
			if (UICamera.currentTouch.dragStarted)
			{
				if (UICamera.mTooltip != null)
				{
					UICamera.ShowTooltip(null);
				}
				UICamera.isDragging = true;
				bool flag2 = UICamera.currentTouch.clickNotification == UICamera.ClickNotification.None;
				if (flag)
				{
					if (UICamera.onDragStart != null)
					{
						UICamera.onDragStart(UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.dragged, "OnDragStart", null);
					if (UICamera.onDragOver != null)
					{
						UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", UICamera.currentTouch.dragged);
				}
				else if (UICamera.currentTouch.last != UICamera.currentTouch.current)
				{
					if (UICamera.onDragOut != null)
					{
						UICamera.onDragOut(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", UICamera.currentTouch.dragged);
					if (UICamera.onDragOver != null)
					{
						UICamera.onDragOver(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
					}
					UICamera.Notify(UICamera.currentTouch.current, "OnDragOver", UICamera.currentTouch.dragged);
				}
				if (UICamera.onDrag != null)
				{
					UICamera.onDrag(UICamera.currentTouch.dragged, UICamera.currentTouch.delta);
				}
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDrag", UICamera.currentTouch.delta);
				UICamera.currentTouch.last = UICamera.currentTouch.current;
				UICamera.isDragging = false;
				if (flag2)
				{
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				}
				else if (UICamera.currentTouch.clickNotification == UICamera.ClickNotification.BasedOnDelta && click < sqrMagnitude)
				{
					UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				}
			}
		}
	}

	// Token: 0x06002C32 RID: 11314 RVA: 0x001468B0 File Offset: 0x00144CB0
	private void ProcessRelease(bool isMouse, float drag)
	{
		if (UICamera.currentTouch == null)
		{
			return;
		}
		UICamera.currentTouch.pressStarted = false;
		if (UICamera.currentTouch.pressed != null)
		{
			if (UICamera.currentTouch.dragStarted)
			{
				if (UICamera.onDragOut != null)
				{
					UICamera.onDragOut(UICamera.currentTouch.last, UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.last, "OnDragOut", UICamera.currentTouch.dragged);
				if (UICamera.onDragEnd != null)
				{
					UICamera.onDragEnd(UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.dragged, "OnDragEnd", null);
			}
			if (UICamera.onPress != null)
			{
				UICamera.onPress(UICamera.currentTouch.pressed, false);
			}
			UICamera.Notify(UICamera.currentTouch.pressed, "OnPress", false);
			if (isMouse)
			{
				bool flag = this.HasCollider(UICamera.currentTouch.pressed);
				if (flag)
				{
					if (UICamera.mHover == UICamera.currentTouch.current)
					{
						if (UICamera.onHover != null)
						{
							UICamera.onHover(UICamera.currentTouch.current, true);
						}
						UICamera.Notify(UICamera.currentTouch.current, "OnHover", true);
					}
					else
					{
						UICamera.hoveredObject = UICamera.currentTouch.current;
					}
				}
			}
			if (UICamera.currentTouch.dragged == UICamera.currentTouch.current || (UICamera.currentScheme != UICamera.ControlScheme.Controller && UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.totalDelta.sqrMagnitude < drag))
			{
				if (UICamera.currentTouch.clickNotification != UICamera.ClickNotification.None && UICamera.currentTouch.pressed == UICamera.currentTouch.current)
				{
					UICamera.ShowTooltip(null);
					float time = RealTime.time;
					if (UICamera.onClick != null)
					{
						UICamera.onClick(UICamera.currentTouch.pressed);
					}
					UICamera.Notify(UICamera.currentTouch.pressed, "OnClick", null);
					if (UICamera.currentTouch.clickTime + 0.35f > time)
					{
						if (UICamera.onDoubleClick != null)
						{
							UICamera.onDoubleClick(UICamera.currentTouch.pressed);
						}
						UICamera.Notify(UICamera.currentTouch.pressed, "OnDoubleClick", null);
					}
					UICamera.currentTouch.clickTime = time;
				}
			}
			else if (UICamera.currentTouch.dragStarted)
			{
				if (UICamera.onDrop != null)
				{
					UICamera.onDrop(UICamera.currentTouch.current, UICamera.currentTouch.dragged);
				}
				UICamera.Notify(UICamera.currentTouch.current, "OnDrop", UICamera.currentTouch.dragged);
			}
		}
		UICamera.currentTouch.dragStarted = false;
		UICamera.currentTouch.pressed = null;
		UICamera.currentTouch.dragged = null;
	}

	// Token: 0x06002C33 RID: 11315 RVA: 0x00146BB8 File Offset: 0x00144FB8
	private bool HasCollider(GameObject go)
	{
		if (go == null)
		{
			return false;
		}
		Collider component = go.GetComponent<Collider>();
		if (component != null)
		{
			return component.enabled;
		}
		Collider2D component2 = go.GetComponent<Collider2D>();
		return component2 != null && component2.enabled;
	}

	// Token: 0x06002C34 RID: 11316 RVA: 0x00146C0C File Offset: 0x0014500C
	public void ProcessTouch(bool pressed, bool released)
	{
		if (released)
		{
			UICamera.mTooltipTime = 0f;
		}
		bool flag = UICamera.currentScheme == UICamera.ControlScheme.Mouse;
		float num = (!flag) ? this.touchDragThreshold : this.mouseDragThreshold;
		float num2 = (!flag) ? this.touchClickThreshold : this.mouseClickThreshold;
		num *= num;
		num2 *= num2;
		if (UICamera.currentTouch.pressed != null)
		{
			if (released)
			{
				this.ProcessRelease(flag, num);
			}
			this.ProcessPress(pressed, num2, num);
			if (this.tooltipDelay != 0f && UICamera.currentTouch.deltaTime > this.tooltipDelay && UICamera.currentTouch.pressed == UICamera.currentTouch.current && UICamera.mTooltipTime != 0f && !UICamera.currentTouch.dragStarted)
			{
				UICamera.mTooltipTime = 0f;
				UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
				if (this.longPressTooltip)
				{
					UICamera.ShowTooltip(UICamera.currentTouch.pressed);
				}
				UICamera.Notify(UICamera.currentTouch.current, "OnLongPress", null);
			}
		}
		else if (flag || pressed || released)
		{
			this.ProcessPress(pressed, num2, num);
			if (released)
			{
				this.ProcessRelease(flag, num);
			}
		}
	}

	// Token: 0x06002C35 RID: 11317 RVA: 0x00146D6A File Offset: 0x0014516A
	public static void CancelNextTooltip()
	{
		UICamera.mTooltipTime = 0f;
	}

	// Token: 0x06002C36 RID: 11318 RVA: 0x00146D78 File Offset: 0x00145178
	public static bool ShowTooltip(GameObject go)
	{
		if (UICamera.mTooltip != go)
		{
			if (UICamera.mTooltip != null)
			{
				if (UICamera.onTooltip != null)
				{
					UICamera.onTooltip(UICamera.mTooltip, false);
				}
				UICamera.Notify(UICamera.mTooltip, "OnTooltip", false);
			}
			UICamera.mTooltip = go;
			UICamera.mTooltipTime = 0f;
			if (UICamera.mTooltip != null)
			{
				if (UICamera.onTooltip != null)
				{
					UICamera.onTooltip(UICamera.mTooltip, true);
				}
				UICamera.Notify(UICamera.mTooltip, "OnTooltip", true);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06002C37 RID: 11319 RVA: 0x00146E26 File Offset: 0x00145226
	public static bool HideTooltip()
	{
		return UICamera.ShowTooltip(null);
	}

	// Token: 0x04002B6A RID: 11114
	public static BetterList<UICamera> list = new BetterList<UICamera>();

	// Token: 0x04002B6B RID: 11115
	public static UICamera.GetKeyStateFunc GetKeyDown = (KeyCode key) => (key < KeyCode.JoystickButton0 || !UICamera.ignoreControllerInput) && Input.GetKeyDown(key);

	// Token: 0x04002B6C RID: 11116
	public static UICamera.GetKeyStateFunc GetKeyUp = (KeyCode key) => (key < KeyCode.JoystickButton0 || !UICamera.ignoreControllerInput) && Input.GetKeyUp(key);

	// Token: 0x04002B6D RID: 11117
	public static UICamera.GetKeyStateFunc GetKey = (KeyCode key) => (key < KeyCode.JoystickButton0 || !UICamera.ignoreControllerInput) && Input.GetKey(key);

	// Token: 0x04002B6E RID: 11118
	public static UICamera.GetAxisFunc GetAxis = delegate(string axis)
	{
		if (UICamera.ignoreControllerInput)
		{
			return 0f;
		}
		return Input.GetAxis(axis);
	};

	// Token: 0x04002B6F RID: 11119
	public static UICamera.GetAnyKeyFunc GetAnyKeyDown;

	// Token: 0x04002B70 RID: 11120
	public static UICamera.GetMouseDelegate GetMouse = (int button) => UICamera.mMouse[button];

	// Token: 0x04002B71 RID: 11121
	public static UICamera.GetTouchDelegate GetTouch = delegate(int id, bool createIfMissing)
	{
		if (id < 0)
		{
			return UICamera.GetMouse(-id - 1);
		}
		int i = 0;
		int count = UICamera.mTouchIDs.Count;
		while (i < count)
		{
			if (UICamera.mTouchIDs[i] == id)
			{
				return UICamera.activeTouches[i];
			}
			i++;
		}
		if (createIfMissing)
		{
			UICamera.MouseOrTouch mouseOrTouch = new UICamera.MouseOrTouch();
			mouseOrTouch.pressTime = RealTime.time;
			mouseOrTouch.touchBegan = true;
			UICamera.activeTouches.Add(mouseOrTouch);
			UICamera.mTouchIDs.Add(id);
			return mouseOrTouch;
		}
		return null;
	};

	// Token: 0x04002B72 RID: 11122
	public static UICamera.RemoveTouchDelegate RemoveTouch = delegate(int id)
	{
		int i = 0;
		int count = UICamera.mTouchIDs.Count;
		while (i < count)
		{
			if (UICamera.mTouchIDs[i] == id)
			{
				UICamera.mTouchIDs.RemoveAt(i);
				UICamera.activeTouches.RemoveAt(i);
				return;
			}
			i++;
		}
	};

	// Token: 0x04002B73 RID: 11123
	public static UICamera.OnScreenResize onScreenResize;

	// Token: 0x04002B74 RID: 11124
	public UICamera.EventType eventType = UICamera.EventType.UI_3D;

	// Token: 0x04002B75 RID: 11125
	public bool eventsGoToColliders;

	// Token: 0x04002B76 RID: 11126
	public LayerMask eventReceiverMask = -1;

	// Token: 0x04002B77 RID: 11127
	public UICamera.ProcessEventsIn processEventsIn;

	// Token: 0x04002B78 RID: 11128
	public bool debug;

	// Token: 0x04002B79 RID: 11129
	public bool useMouse = true;

	// Token: 0x04002B7A RID: 11130
	public bool useTouch = true;

	// Token: 0x04002B7B RID: 11131
	public bool allowMultiTouch = true;

	// Token: 0x04002B7C RID: 11132
	public bool useKeyboard = true;

	// Token: 0x04002B7D RID: 11133
	public bool useController = true;

	// Token: 0x04002B7E RID: 11134
	public bool stickyTooltip = true;

	// Token: 0x04002B7F RID: 11135
	public float tooltipDelay = 1f;

	// Token: 0x04002B80 RID: 11136
	public bool longPressTooltip;

	// Token: 0x04002B81 RID: 11137
	public float mouseDragThreshold = 4f;

	// Token: 0x04002B82 RID: 11138
	public float mouseClickThreshold = 10f;

	// Token: 0x04002B83 RID: 11139
	public float touchDragThreshold = 40f;

	// Token: 0x04002B84 RID: 11140
	public float touchClickThreshold = 40f;

	// Token: 0x04002B85 RID: 11141
	public float rangeDistance = -1f;

	// Token: 0x04002B86 RID: 11142
	public string horizontalAxisName = "Horizontal";

	// Token: 0x04002B87 RID: 11143
	public string verticalAxisName = "Vertical";

	// Token: 0x04002B88 RID: 11144
	public string horizontalPanAxisName;

	// Token: 0x04002B89 RID: 11145
	public string verticalPanAxisName;

	// Token: 0x04002B8A RID: 11146
	public string scrollAxisName = "Mouse ScrollWheel";

	// Token: 0x04002B8B RID: 11147
	[Tooltip("If enabled, command-click will result in a right-click event on OSX")]
	public bool commandClick = true;

	// Token: 0x04002B8C RID: 11148
	public KeyCode submitKey0 = KeyCode.Return;

	// Token: 0x04002B8D RID: 11149
	public KeyCode submitKey1 = KeyCode.JoystickButton0;

	// Token: 0x04002B8E RID: 11150
	public KeyCode cancelKey0 = KeyCode.Escape;

	// Token: 0x04002B8F RID: 11151
	public KeyCode cancelKey1 = KeyCode.JoystickButton1;

	// Token: 0x04002B90 RID: 11152
	public bool autoHideCursor = true;

	// Token: 0x04002B91 RID: 11153
	public static UICamera.OnCustomInput onCustomInput;

	// Token: 0x04002B92 RID: 11154
	public static bool showTooltips = true;

	// Token: 0x04002B93 RID: 11155
	public static bool ignoreAllEvents = false;

	// Token: 0x04002B94 RID: 11156
	public static bool ignoreControllerInput = false;

	// Token: 0x04002B95 RID: 11157
	private static bool mDisableController = false;

	// Token: 0x04002B96 RID: 11158
	private static Vector2 mLastPos = Vector2.zero;

	// Token: 0x04002B97 RID: 11159
	public static Vector3 lastWorldPosition = Vector3.zero;

	// Token: 0x04002B98 RID: 11160
	public static Ray lastWorldRay = default(Ray);

	// Token: 0x04002B99 RID: 11161
	public static RaycastHit lastHit;

	// Token: 0x04002B9A RID: 11162
	public static UICamera current = null;

	// Token: 0x04002B9B RID: 11163
	public static Camera currentCamera = null;

	// Token: 0x04002B9C RID: 11164
	public static UICamera.OnSchemeChange onSchemeChange;

	// Token: 0x04002B9D RID: 11165
	private static UICamera.ControlScheme mLastScheme = UICamera.ControlScheme.Mouse;

	// Token: 0x04002B9E RID: 11166
	public static int currentTouchID = -100;

	// Token: 0x04002B9F RID: 11167
	private static KeyCode mCurrentKey = KeyCode.Alpha0;

	// Token: 0x04002BA0 RID: 11168
	public static UICamera.MouseOrTouch currentTouch = null;

	// Token: 0x04002BA1 RID: 11169
	private static bool mInputFocus = false;

	// Token: 0x04002BA2 RID: 11170
	private static GameObject mGenericHandler;

	// Token: 0x04002BA3 RID: 11171
	public static GameObject fallThrough;

	// Token: 0x04002BA4 RID: 11172
	public static UICamera.VoidDelegate onClick;

	// Token: 0x04002BA5 RID: 11173
	public static UICamera.VoidDelegate onDoubleClick;

	// Token: 0x04002BA6 RID: 11174
	public static UICamera.BoolDelegate onHover;

	// Token: 0x04002BA7 RID: 11175
	public static UICamera.BoolDelegate onPress;

	// Token: 0x04002BA8 RID: 11176
	public static UICamera.BoolDelegate onSelect;

	// Token: 0x04002BA9 RID: 11177
	public static UICamera.FloatDelegate onScroll;

	// Token: 0x04002BAA RID: 11178
	public static UICamera.VectorDelegate onDrag;

	// Token: 0x04002BAB RID: 11179
	public static UICamera.VoidDelegate onDragStart;

	// Token: 0x04002BAC RID: 11180
	public static UICamera.ObjectDelegate onDragOver;

	// Token: 0x04002BAD RID: 11181
	public static UICamera.ObjectDelegate onDragOut;

	// Token: 0x04002BAE RID: 11182
	public static UICamera.VoidDelegate onDragEnd;

	// Token: 0x04002BAF RID: 11183
	public static UICamera.ObjectDelegate onDrop;

	// Token: 0x04002BB0 RID: 11184
	public static UICamera.KeyCodeDelegate onKey;

	// Token: 0x04002BB1 RID: 11185
	public static UICamera.KeyCodeDelegate onNavigate;

	// Token: 0x04002BB2 RID: 11186
	public static UICamera.VectorDelegate onPan;

	// Token: 0x04002BB3 RID: 11187
	public static UICamera.BoolDelegate onTooltip;

	// Token: 0x04002BB4 RID: 11188
	public static UICamera.MoveDelegate onMouseMove;

	// Token: 0x04002BB5 RID: 11189
	private static UICamera.MouseOrTouch[] mMouse = new UICamera.MouseOrTouch[]
	{
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch(),
		new UICamera.MouseOrTouch()
	};

	// Token: 0x04002BB6 RID: 11190
	public static UICamera.MouseOrTouch controller = new UICamera.MouseOrTouch();

	// Token: 0x04002BB7 RID: 11191
	public static List<UICamera.MouseOrTouch> activeTouches = new List<UICamera.MouseOrTouch>();

	// Token: 0x04002BB8 RID: 11192
	private static List<int> mTouchIDs = new List<int>();

	// Token: 0x04002BB9 RID: 11193
	private static int mWidth = 0;

	// Token: 0x04002BBA RID: 11194
	private static int mHeight = 0;

	// Token: 0x04002BBB RID: 11195
	private static GameObject mTooltip = null;

	// Token: 0x04002BBC RID: 11196
	private Camera mCam;

	// Token: 0x04002BBD RID: 11197
	private static float mTooltipTime = 0f;

	// Token: 0x04002BBE RID: 11198
	private float mNextRaycast;

	// Token: 0x04002BBF RID: 11199
	public static bool isDragging = false;

	// Token: 0x04002BC0 RID: 11200
	private static int mLastInteractionCheck = -1;

	// Token: 0x04002BC1 RID: 11201
	private static bool mLastInteractionResult = false;

	// Token: 0x04002BC2 RID: 11202
	private static int mLastFocusCheck = -1;

	// Token: 0x04002BC3 RID: 11203
	private static bool mLastFocusResult = false;

	// Token: 0x04002BC4 RID: 11204
	private static int mLastOverCheck = -1;

	// Token: 0x04002BC5 RID: 11205
	private static bool mLastOverResult = false;

	// Token: 0x04002BC6 RID: 11206
	private static GameObject mRayHitObject;

	// Token: 0x04002BC7 RID: 11207
	private static GameObject mHover;

	// Token: 0x04002BC8 RID: 11208
	private static GameObject mSelected;

	// Token: 0x04002BC9 RID: 11209
	private static UICamera.DepthEntry mHit = default(UICamera.DepthEntry);

	// Token: 0x04002BCA RID: 11210
	private static BetterList<UICamera.DepthEntry> mHits = new BetterList<UICamera.DepthEntry>();

	// Token: 0x04002BCB RID: 11211
	private static RaycastHit[] mRayHits;

	// Token: 0x04002BCC RID: 11212
	private static Collider2D[] mOverlap;

	// Token: 0x04002BCD RID: 11213
	private static Plane m2DPlane = new Plane(Vector3.back, 0f);

	// Token: 0x04002BCE RID: 11214
	private static float mNextEvent = 0f;

	// Token: 0x04002BCF RID: 11215
	private static int mNotifying = 0;

	// Token: 0x04002BD0 RID: 11216
	private static bool mUsingTouchEvents = true;

	// Token: 0x04002BD1 RID: 11217
	public static UICamera.GetTouchCountCallback GetInputTouchCount;

	// Token: 0x04002BD2 RID: 11218
	public static UICamera.GetTouchCallback GetInputTouch;

	// Token: 0x04002BD5 RID: 11221
	[CompilerGenerated]
	private static BetterList<UICamera>.CompareFunc <>f__mg$cache0;

	// Token: 0x04002BD6 RID: 11222
	[CompilerGenerated]
	private static BetterList<UICamera>.CompareFunc <>f__mg$cache1;

	// Token: 0x02000601 RID: 1537
	[DoNotObfuscateNGUI]
	public enum ControlScheme
	{
		// Token: 0x04002BD8 RID: 11224
		Mouse,
		// Token: 0x04002BD9 RID: 11225
		Touch,
		// Token: 0x04002BDA RID: 11226
		Controller
	}

	// Token: 0x02000602 RID: 1538
	[DoNotObfuscateNGUI]
	public enum ClickNotification
	{
		// Token: 0x04002BDC RID: 11228
		None,
		// Token: 0x04002BDD RID: 11229
		Always,
		// Token: 0x04002BDE RID: 11230
		BasedOnDelta
	}

	// Token: 0x02000603 RID: 1539
	public class MouseOrTouch
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x00147194 File Offset: 0x00145594
		public float deltaTime
		{
			get
			{
				return RealTime.time - this.pressTime;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06002C44 RID: 11332 RVA: 0x001471A2 File Offset: 0x001455A2
		public bool isOverUI
		{
			get
			{
				return this.current != null && this.current != UICamera.fallThrough && NGUITools.FindInParents<UIRoot>(this.current) != null;
			}
		}

		// Token: 0x04002BDF RID: 11231
		public KeyCode key;

		// Token: 0x04002BE0 RID: 11232
		public Vector2 pos;

		// Token: 0x04002BE1 RID: 11233
		public Vector2 lastPos;

		// Token: 0x04002BE2 RID: 11234
		public Vector2 delta;

		// Token: 0x04002BE3 RID: 11235
		public Vector2 totalDelta;

		// Token: 0x04002BE4 RID: 11236
		public Camera pressedCam;

		// Token: 0x04002BE5 RID: 11237
		public GameObject last;

		// Token: 0x04002BE6 RID: 11238
		public GameObject current;

		// Token: 0x04002BE7 RID: 11239
		public GameObject pressed;

		// Token: 0x04002BE8 RID: 11240
		public GameObject dragged;

		// Token: 0x04002BE9 RID: 11241
		public float pressTime;

		// Token: 0x04002BEA RID: 11242
		public float clickTime;

		// Token: 0x04002BEB RID: 11243
		public UICamera.ClickNotification clickNotification = UICamera.ClickNotification.Always;

		// Token: 0x04002BEC RID: 11244
		public bool touchBegan = true;

		// Token: 0x04002BED RID: 11245
		public bool pressStarted;

		// Token: 0x04002BEE RID: 11246
		public bool dragStarted;

		// Token: 0x04002BEF RID: 11247
		public int ignoreDelta;
	}

	// Token: 0x02000604 RID: 1540
	[DoNotObfuscateNGUI]
	public enum EventType
	{
		// Token: 0x04002BF1 RID: 11249
		World_3D,
		// Token: 0x04002BF2 RID: 11250
		UI_3D,
		// Token: 0x04002BF3 RID: 11251
		World_2D,
		// Token: 0x04002BF4 RID: 11252
		UI_2D
	}

	// Token: 0x02000605 RID: 1541
	// (Invoke) Token: 0x06002C46 RID: 11334
	public delegate bool GetKeyStateFunc(KeyCode key);

	// Token: 0x02000606 RID: 1542
	// (Invoke) Token: 0x06002C4A RID: 11338
	public delegate float GetAxisFunc(string name);

	// Token: 0x02000607 RID: 1543
	// (Invoke) Token: 0x06002C4E RID: 11342
	public delegate bool GetAnyKeyFunc();

	// Token: 0x02000608 RID: 1544
	// (Invoke) Token: 0x06002C52 RID: 11346
	public delegate UICamera.MouseOrTouch GetMouseDelegate(int button);

	// Token: 0x02000609 RID: 1545
	// (Invoke) Token: 0x06002C56 RID: 11350
	public delegate UICamera.MouseOrTouch GetTouchDelegate(int id, bool createIfMissing);

	// Token: 0x0200060A RID: 1546
	// (Invoke) Token: 0x06002C5A RID: 11354
	public delegate void RemoveTouchDelegate(int id);

	// Token: 0x0200060B RID: 1547
	// (Invoke) Token: 0x06002C5E RID: 11358
	public delegate void OnScreenResize();

	// Token: 0x0200060C RID: 1548
	[DoNotObfuscateNGUI]
	public enum ProcessEventsIn
	{
		// Token: 0x04002BF6 RID: 11254
		Update,
		// Token: 0x04002BF7 RID: 11255
		LateUpdate
	}

	// Token: 0x0200060D RID: 1549
	// (Invoke) Token: 0x06002C62 RID: 11362
	public delegate void OnCustomInput();

	// Token: 0x0200060E RID: 1550
	// (Invoke) Token: 0x06002C66 RID: 11366
	public delegate void OnSchemeChange();

	// Token: 0x0200060F RID: 1551
	// (Invoke) Token: 0x06002C6A RID: 11370
	public delegate void MoveDelegate(Vector2 delta);

	// Token: 0x02000610 RID: 1552
	// (Invoke) Token: 0x06002C6E RID: 11374
	public delegate void VoidDelegate(GameObject go);

	// Token: 0x02000611 RID: 1553
	// (Invoke) Token: 0x06002C72 RID: 11378
	public delegate void BoolDelegate(GameObject go, bool state);

	// Token: 0x02000612 RID: 1554
	// (Invoke) Token: 0x06002C76 RID: 11382
	public delegate void FloatDelegate(GameObject go, float delta);

	// Token: 0x02000613 RID: 1555
	// (Invoke) Token: 0x06002C7A RID: 11386
	public delegate void VectorDelegate(GameObject go, Vector2 delta);

	// Token: 0x02000614 RID: 1556
	// (Invoke) Token: 0x06002C7E RID: 11390
	public delegate void ObjectDelegate(GameObject go, GameObject obj);

	// Token: 0x02000615 RID: 1557
	// (Invoke) Token: 0x06002C82 RID: 11394
	public delegate void KeyCodeDelegate(GameObject go, KeyCode key);

	// Token: 0x02000616 RID: 1558
	private struct DepthEntry
	{
		// Token: 0x04002BF8 RID: 11256
		public int depth;

		// Token: 0x04002BF9 RID: 11257
		public RaycastHit hit;

		// Token: 0x04002BFA RID: 11258
		public Vector3 point;

		// Token: 0x04002BFB RID: 11259
		public GameObject go;
	}

	// Token: 0x02000617 RID: 1559
	public class Touch
	{
		// Token: 0x04002BFC RID: 11260
		public int fingerId;

		// Token: 0x04002BFD RID: 11261
		public TouchPhase phase;

		// Token: 0x04002BFE RID: 11262
		public Vector2 position;

		// Token: 0x04002BFF RID: 11263
		public int tapCount;
	}

	// Token: 0x02000618 RID: 1560
	// (Invoke) Token: 0x06002C87 RID: 11399
	public delegate int GetTouchCountCallback();

	// Token: 0x02000619 RID: 1561
	// (Invoke) Token: 0x06002C8B RID: 11403
	public delegate UICamera.Touch GetTouchCallback(int index);
}
