using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HedgehogTeam.EasyTouch
{
	// Token: 0x020003B1 RID: 945
	public class EasyTouch : MonoBehaviour
	{
		// Token: 0x06001CA5 RID: 7333 RVA: 0x000E12BC File Offset: 0x000DF6BC
		public EasyTouch()
		{
			this.enable = true;
			this.allowUIDetection = true;
			this.enableUIMode = true;
			this.autoUpdatePickedUI = false;
			this.enabledNGuiMode = false;
			this.nGUICameras = new List<Camera>();
			this.autoSelect = true;
			this.touchCameras = new List<ECamera>();
			this.pickableLayers3D = 1;
			this.enable2D = false;
			this.pickableLayers2D = 1;
			this.gesturePriority = EasyTouch.GesturePriority.Tap;
			this.StationaryTolerance = 15f;
			this.longTapTime = 1f;
			this.doubleTapDetection = EasyTouch.DoubleTapDetection.BySystem;
			this.doubleTapTime = 0.3f;
			this.swipeTolerance = 0.85f;
			this.alwaysSendSwipe = false;
			this.enable2FingersGesture = true;
			this.twoFingerPickMethod = EasyTouch.TwoFingerPickMethod.Finger;
			this.enable2FingersSwipe = true;
			this.enablePinch = true;
			this.minPinchLength = 0f;
			this.enableTwist = true;
			this.minTwistAngle = 0f;
			this.enableSimulation = true;
			this.twistKey = KeyCode.LeftAlt;
			this.swipeKey = KeyCode.LeftControl;
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06001CA6 RID: 7334 RVA: 0x000E1430 File Offset: 0x000DF830
		// (remove) Token: 0x06001CA7 RID: 7335 RVA: 0x000E1464 File Offset: 0x000DF864
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.TouchCancelHandler On_Cancel;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06001CA8 RID: 7336 RVA: 0x000E1498 File Offset: 0x000DF898
		// (remove) Token: 0x06001CA9 RID: 7337 RVA: 0x000E14CC File Offset: 0x000DF8CC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.Cancel2FingersHandler On_Cancel2Fingers;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06001CAA RID: 7338 RVA: 0x000E1500 File Offset: 0x000DF900
		// (remove) Token: 0x06001CAB RID: 7339 RVA: 0x000E1534 File Offset: 0x000DF934
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.TouchStartHandler On_TouchStart;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06001CAC RID: 7340 RVA: 0x000E1568 File Offset: 0x000DF968
		// (remove) Token: 0x06001CAD RID: 7341 RVA: 0x000E159C File Offset: 0x000DF99C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.TouchDownHandler On_TouchDown;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06001CAE RID: 7342 RVA: 0x000E15D0 File Offset: 0x000DF9D0
		// (remove) Token: 0x06001CAF RID: 7343 RVA: 0x000E1604 File Offset: 0x000DFA04
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.TouchUpHandler On_TouchUp;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06001CB0 RID: 7344 RVA: 0x000E1638 File Offset: 0x000DFA38
		// (remove) Token: 0x06001CB1 RID: 7345 RVA: 0x000E166C File Offset: 0x000DFA6C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.SimpleTapHandler On_SimpleTap;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06001CB2 RID: 7346 RVA: 0x000E16A0 File Offset: 0x000DFAA0
		// (remove) Token: 0x06001CB3 RID: 7347 RVA: 0x000E16D4 File Offset: 0x000DFAD4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.DoubleTapHandler On_DoubleTap;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06001CB4 RID: 7348 RVA: 0x000E1708 File Offset: 0x000DFB08
		// (remove) Token: 0x06001CB5 RID: 7349 RVA: 0x000E173C File Offset: 0x000DFB3C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.LongTapStartHandler On_LongTapStart;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06001CB6 RID: 7350 RVA: 0x000E1770 File Offset: 0x000DFB70
		// (remove) Token: 0x06001CB7 RID: 7351 RVA: 0x000E17A4 File Offset: 0x000DFBA4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.LongTapHandler On_LongTap;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06001CB8 RID: 7352 RVA: 0x000E17D8 File Offset: 0x000DFBD8
		// (remove) Token: 0x06001CB9 RID: 7353 RVA: 0x000E180C File Offset: 0x000DFC0C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.LongTapEndHandler On_LongTapEnd;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06001CBA RID: 7354 RVA: 0x000E1840 File Offset: 0x000DFC40
		// (remove) Token: 0x06001CBB RID: 7355 RVA: 0x000E1874 File Offset: 0x000DFC74
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.DragStartHandler On_DragStart;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06001CBC RID: 7356 RVA: 0x000E18A8 File Offset: 0x000DFCA8
		// (remove) Token: 0x06001CBD RID: 7357 RVA: 0x000E18DC File Offset: 0x000DFCDC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.DragHandler On_Drag;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06001CBE RID: 7358 RVA: 0x000E1910 File Offset: 0x000DFD10
		// (remove) Token: 0x06001CBF RID: 7359 RVA: 0x000E1944 File Offset: 0x000DFD44
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.DragEndHandler On_DragEnd;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06001CC0 RID: 7360 RVA: 0x000E1978 File Offset: 0x000DFD78
		// (remove) Token: 0x06001CC1 RID: 7361 RVA: 0x000E19AC File Offset: 0x000DFDAC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.SwipeStartHandler On_SwipeStart;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06001CC2 RID: 7362 RVA: 0x000E19E0 File Offset: 0x000DFDE0
		// (remove) Token: 0x06001CC3 RID: 7363 RVA: 0x000E1A14 File Offset: 0x000DFE14
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.SwipeHandler On_Swipe;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06001CC4 RID: 7364 RVA: 0x000E1A48 File Offset: 0x000DFE48
		// (remove) Token: 0x06001CC5 RID: 7365 RVA: 0x000E1A7C File Offset: 0x000DFE7C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.SwipeEndHandler On_SwipeEnd;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06001CC6 RID: 7366 RVA: 0x000E1AB0 File Offset: 0x000DFEB0
		// (remove) Token: 0x06001CC7 RID: 7367 RVA: 0x000E1AE4 File Offset: 0x000DFEE4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.TouchStart2FingersHandler On_TouchStart2Fingers;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06001CC8 RID: 7368 RVA: 0x000E1B18 File Offset: 0x000DFF18
		// (remove) Token: 0x06001CC9 RID: 7369 RVA: 0x000E1B4C File Offset: 0x000DFF4C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.TouchDown2FingersHandler On_TouchDown2Fingers;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06001CCA RID: 7370 RVA: 0x000E1B80 File Offset: 0x000DFF80
		// (remove) Token: 0x06001CCB RID: 7371 RVA: 0x000E1BB4 File Offset: 0x000DFFB4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.TouchUp2FingersHandler On_TouchUp2Fingers;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06001CCC RID: 7372 RVA: 0x000E1BE8 File Offset: 0x000DFFE8
		// (remove) Token: 0x06001CCD RID: 7373 RVA: 0x000E1C1C File Offset: 0x000E001C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.SimpleTap2FingersHandler On_SimpleTap2Fingers;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06001CCE RID: 7374 RVA: 0x000E1C50 File Offset: 0x000E0050
		// (remove) Token: 0x06001CCF RID: 7375 RVA: 0x000E1C84 File Offset: 0x000E0084
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.DoubleTap2FingersHandler On_DoubleTap2Fingers;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06001CD0 RID: 7376 RVA: 0x000E1CB8 File Offset: 0x000E00B8
		// (remove) Token: 0x06001CD1 RID: 7377 RVA: 0x000E1CEC File Offset: 0x000E00EC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.LongTapStart2FingersHandler On_LongTapStart2Fingers;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06001CD2 RID: 7378 RVA: 0x000E1D20 File Offset: 0x000E0120
		// (remove) Token: 0x06001CD3 RID: 7379 RVA: 0x000E1D54 File Offset: 0x000E0154
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.LongTap2FingersHandler On_LongTap2Fingers;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06001CD4 RID: 7380 RVA: 0x000E1D88 File Offset: 0x000E0188
		// (remove) Token: 0x06001CD5 RID: 7381 RVA: 0x000E1DBC File Offset: 0x000E01BC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.LongTapEnd2FingersHandler On_LongTapEnd2Fingers;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06001CD6 RID: 7382 RVA: 0x000E1DF0 File Offset: 0x000E01F0
		// (remove) Token: 0x06001CD7 RID: 7383 RVA: 0x000E1E24 File Offset: 0x000E0224
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.TwistHandler On_Twist;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06001CD8 RID: 7384 RVA: 0x000E1E58 File Offset: 0x000E0258
		// (remove) Token: 0x06001CD9 RID: 7385 RVA: 0x000E1E8C File Offset: 0x000E028C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.TwistEndHandler On_TwistEnd;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06001CDA RID: 7386 RVA: 0x000E1EC0 File Offset: 0x000E02C0
		// (remove) Token: 0x06001CDB RID: 7387 RVA: 0x000E1EF4 File Offset: 0x000E02F4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.PinchHandler On_Pinch;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06001CDC RID: 7388 RVA: 0x000E1F28 File Offset: 0x000E0328
		// (remove) Token: 0x06001CDD RID: 7389 RVA: 0x000E1F5C File Offset: 0x000E035C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.PinchInHandler On_PinchIn;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06001CDE RID: 7390 RVA: 0x000E1F90 File Offset: 0x000E0390
		// (remove) Token: 0x06001CDF RID: 7391 RVA: 0x000E1FC4 File Offset: 0x000E03C4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.PinchOutHandler On_PinchOut;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06001CE0 RID: 7392 RVA: 0x000E1FF8 File Offset: 0x000E03F8
		// (remove) Token: 0x06001CE1 RID: 7393 RVA: 0x000E202C File Offset: 0x000E042C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.PinchEndHandler On_PinchEnd;

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06001CE2 RID: 7394 RVA: 0x000E2060 File Offset: 0x000E0460
		// (remove) Token: 0x06001CE3 RID: 7395 RVA: 0x000E2094 File Offset: 0x000E0494
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.DragStart2FingersHandler On_DragStart2Fingers;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06001CE4 RID: 7396 RVA: 0x000E20C8 File Offset: 0x000E04C8
		// (remove) Token: 0x06001CE5 RID: 7397 RVA: 0x000E20FC File Offset: 0x000E04FC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.Drag2FingersHandler On_Drag2Fingers;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06001CE6 RID: 7398 RVA: 0x000E2130 File Offset: 0x000E0530
		// (remove) Token: 0x06001CE7 RID: 7399 RVA: 0x000E2164 File Offset: 0x000E0564
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.DragEnd2FingersHandler On_DragEnd2Fingers;

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06001CE8 RID: 7400 RVA: 0x000E2198 File Offset: 0x000E0598
		// (remove) Token: 0x06001CE9 RID: 7401 RVA: 0x000E21CC File Offset: 0x000E05CC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.SwipeStart2FingersHandler On_SwipeStart2Fingers;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06001CEA RID: 7402 RVA: 0x000E2200 File Offset: 0x000E0600
		// (remove) Token: 0x06001CEB RID: 7403 RVA: 0x000E2234 File Offset: 0x000E0634
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.Swipe2FingersHandler On_Swipe2Fingers;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06001CEC RID: 7404 RVA: 0x000E2268 File Offset: 0x000E0668
		// (remove) Token: 0x06001CED RID: 7405 RVA: 0x000E229C File Offset: 0x000E069C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.SwipeEnd2FingersHandler On_SwipeEnd2Fingers;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06001CEE RID: 7406 RVA: 0x000E22D0 File Offset: 0x000E06D0
		// (remove) Token: 0x06001CEF RID: 7407 RVA: 0x000E2304 File Offset: 0x000E0704
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.EasyTouchIsReadyHandler On_EasyTouchIsReady;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06001CF0 RID: 7408 RVA: 0x000E2338 File Offset: 0x000E0738
		// (remove) Token: 0x06001CF1 RID: 7409 RVA: 0x000E236C File Offset: 0x000E076C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.OverUIElementHandler On_OverUIElement;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06001CF2 RID: 7410 RVA: 0x000E23A0 File Offset: 0x000E07A0
		// (remove) Token: 0x06001CF3 RID: 7411 RVA: 0x000E23D4 File Offset: 0x000E07D4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EasyTouch.UIElementTouchUpHandler On_UIElementTouchUp;

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x000E2408 File Offset: 0x000E0808
		public static EasyTouch instance
		{
			get
			{
				if (!EasyTouch._instance)
				{
					EasyTouch._instance = (UnityEngine.Object.FindObjectOfType(typeof(EasyTouch)) as EasyTouch);
					if (!EasyTouch._instance)
					{
						GameObject gameObject = new GameObject("Easytouch");
						EasyTouch._instance = gameObject.AddComponent<EasyTouch>();
					}
				}
				return EasyTouch._instance;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x000E2467 File Offset: 0x000E0867
		public static Gesture current
		{
			get
			{
				return EasyTouch.instance._currentGesture;
			}
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x000E2473 File Offset: 0x000E0873
		private void OnEnable()
		{
			if (Application.isPlaying && Application.isEditor)
			{
				this.Init();
			}
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000E248F File Offset: 0x000E088F
		private void Awake()
		{
			this.Init();
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000E2498 File Offset: 0x000E0898
		private void Start()
		{
			for (int i = 0; i < 100; i++)
			{
				this.singleDoubleTap[i] = new EasyTouch.DoubleTap();
			}
			int num = this.touchCameras.FindIndex((ECamera c) => c.camera == Camera.main);
			if (num < 0)
			{
				this.touchCameras.Add(new ECamera(Camera.main, false));
			}
			if (EasyTouch.On_EasyTouchIsReady != null)
			{
				EasyTouch.On_EasyTouchIsReady();
			}
			this._currentGestures.Add(new Gesture());
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x000E252F File Offset: 0x000E092F
		private void Init()
		{
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x000E2531 File Offset: 0x000E0931
		private void OnDrawGizmos()
		{
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x000E2534 File Offset: 0x000E0934
		private void Update()
		{
			if (this.enable && EasyTouch.instance == this)
			{
				if (Application.isPlaying && Input.touchCount > 0)
				{
					this.enableRemote = true;
				}
				if (Application.isPlaying && Input.touchCount == 0)
				{
					this.enableRemote = false;
				}
				int num = this.input.TouchCount();
				if (this.oldTouchCount == 2 && num != 2 && num > 0)
				{
					this.CreateGesture2Finger(EasyTouch.EvtType.On_Cancel2Fingers, Vector2.zero, Vector2.zero, Vector2.zero, 0f, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, 0f);
				}
				this.UpdateTouches(true, num);
				this.twoFinger.oldPickedObject = this.twoFinger.pickedObject;
				if (this.enable2FingersGesture && num == 2)
				{
					this.TwoFinger();
				}
				for (int i = 0; i < 100; i++)
				{
					if (this.fingers[i] != null)
					{
						this.OneFinger(i);
					}
				}
				this.oldTouchCount = num;
			}
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000E2654 File Offset: 0x000E0A54
		private void LateUpdate()
		{
			if (this._currentGestures.Count > 1)
			{
				this._currentGestures.RemoveAt(0);
			}
			else if (this._currentGestures.Count == 0)
			{
				this._currentGestures.Add(new Gesture());
			}
			else
			{
				this._currentGestures[0] = new Gesture();
			}
			this._currentGesture = this._currentGestures[0];
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000E26CC File Offset: 0x000E0ACC
		private void UpdateTouches(bool realTouch, int touchCount)
		{
			this.fingers.CopyTo(this.tmpArray, 0);
			if (realTouch || this.enableRemote)
			{
				this.ResetTouches();
				for (int i = 0; i < touchCount; i++)
				{
					Touch touch = Input.GetTouch(i);
					int num = 0;
					while (num < 100 && this.fingers[i] == null)
					{
						if (this.tmpArray[num] != null && this.tmpArray[num].fingerIndex == touch.fingerId)
						{
							this.fingers[i] = this.tmpArray[num];
						}
						num++;
					}
					if (this.fingers[i] == null)
					{
						this.fingers[i] = new Finger();
						this.fingers[i].fingerIndex = touch.fingerId;
						this.fingers[i].gesture = EasyTouch.GestureType.None;
						this.fingers[i].phase = TouchPhase.Began;
					}
					else
					{
						this.fingers[i].phase = touch.phase;
					}
					if (this.fingers[i].phase != TouchPhase.Began)
					{
						this.fingers[i].deltaPosition = touch.position - this.fingers[i].position;
					}
					else
					{
						this.fingers[i].deltaPosition = Vector2.zero;
					}
					this.fingers[i].position = touch.position;
					this.fingers[i].tapCount = touch.tapCount;
					this.fingers[i].deltaTime = touch.deltaTime;
					this.fingers[i].touchCount = touchCount;
				}
			}
			else
			{
				for (int j = 0; j < touchCount; j++)
				{
					this.fingers[j] = this.input.GetMouseTouch(j, this.fingers[j]);
					this.fingers[j].touchCount = touchCount;
				}
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000E28B0 File Offset: 0x000E0CB0
		private void ResetTouches()
		{
			for (int i = 0; i < 100; i++)
			{
				this.fingers[i] = null;
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000E28DC File Offset: 0x000E0CDC
		private void OneFinger(int fingerIndex)
		{
			if (this.fingers[fingerIndex].gesture == EasyTouch.GestureType.None)
			{
				if (!this.singleDoubleTap[fingerIndex].inDoubleTap)
				{
					this.singleDoubleTap[fingerIndex].inDoubleTap = true;
					this.singleDoubleTap[fingerIndex].time = 0f;
					this.singleDoubleTap[fingerIndex].count = 1;
				}
				this.fingers[fingerIndex].startTimeAction = Time.realtimeSinceStartup;
				this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Acquisition;
				this.fingers[fingerIndex].startPosition = this.fingers[fingerIndex].position;
				if (this.autoSelect && this.GetPickedGameObject(this.fingers[fingerIndex], false))
				{
					this.fingers[fingerIndex].pickedObject = this.pickedObject.pickedObj;
					this.fingers[fingerIndex].isGuiCamera = this.pickedObject.isGUI;
					this.fingers[fingerIndex].pickedCamera = this.pickedObject.pickedCamera;
				}
				if (this.allowUIDetection)
				{
					this.fingers[fingerIndex].isOverGui = this.IsScreenPositionOverUI(this.fingers[fingerIndex].position);
					this.fingers[fingerIndex].pickedUIElement = this.GetFirstUIElementFromCache();
				}
				this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_TouchStart, this.fingers[fingerIndex], EasyTouch.SwipeDirection.None, 0f, Vector2.zero);
			}
			if (this.singleDoubleTap[fingerIndex].inDoubleTap)
			{
				this.singleDoubleTap[fingerIndex].time += Time.deltaTime;
			}
			this.fingers[fingerIndex].actionTime = Time.realtimeSinceStartup - this.fingers[fingerIndex].startTimeAction;
			if (this.fingers[fingerIndex].phase == TouchPhase.Canceled)
			{
				this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Cancel;
			}
			if (this.fingers[fingerIndex].phase != TouchPhase.Ended && this.fingers[fingerIndex].phase != TouchPhase.Canceled)
			{
				if (this.fingers[fingerIndex].phase == TouchPhase.Stationary && this.fingers[fingerIndex].actionTime >= this.longTapTime && this.fingers[fingerIndex].gesture == EasyTouch.GestureType.Acquisition)
				{
					this.fingers[fingerIndex].gesture = EasyTouch.GestureType.LongTap;
					this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_LongTapStart, this.fingers[fingerIndex], EasyTouch.SwipeDirection.None, 0f, Vector2.zero);
				}
				if (((this.fingers[fingerIndex].gesture == EasyTouch.GestureType.Acquisition || this.fingers[fingerIndex].gesture == EasyTouch.GestureType.LongTap) && this.fingers[fingerIndex].phase == TouchPhase.Moved && this.gesturePriority == EasyTouch.GesturePriority.Slips) || ((this.fingers[fingerIndex].gesture == EasyTouch.GestureType.Acquisition || this.fingers[fingerIndex].gesture == EasyTouch.GestureType.LongTap) && !this.FingerInTolerance(this.fingers[fingerIndex]) && this.gesturePriority == EasyTouch.GesturePriority.Tap))
				{
					if (this.fingers[fingerIndex].gesture == EasyTouch.GestureType.LongTap)
					{
						this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Cancel;
						this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_LongTapEnd, this.fingers[fingerIndex], EasyTouch.SwipeDirection.None, 0f, Vector2.zero);
						this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Acquisition;
					}
					else
					{
						this.fingers[fingerIndex].oldSwipeType = EasyTouch.SwipeDirection.None;
						if (this.fingers[fingerIndex].pickedObject)
						{
							this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Drag;
							this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_DragStart, this.fingers[fingerIndex], EasyTouch.SwipeDirection.None, 0f, Vector2.zero);
							if (this.alwaysSendSwipe)
							{
								this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_SwipeStart, this.fingers[fingerIndex], EasyTouch.SwipeDirection.None, 0f, Vector2.zero);
							}
						}
						else
						{
							this.fingers[fingerIndex].gesture = EasyTouch.GestureType.Swipe;
							this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_SwipeStart, this.fingers[fingerIndex], EasyTouch.SwipeDirection.None, 0f, Vector2.zero);
						}
					}
				}
				EasyTouch.EvtType evtType = EasyTouch.EvtType.None;
				EasyTouch.GestureType gesture = this.fingers[fingerIndex].gesture;
				if (gesture != EasyTouch.GestureType.LongTap)
				{
					if (gesture != EasyTouch.GestureType.Drag)
					{
						if (gesture == EasyTouch.GestureType.Swipe)
						{
							evtType = EasyTouch.EvtType.On_Swipe;
						}
					}
					else
					{
						evtType = EasyTouch.EvtType.On_Drag;
					}
				}
				else
				{
					evtType = EasyTouch.EvtType.On_LongTap;
				}
				EasyTouch.SwipeDirection swipe = this.GetSwipe(new Vector2(0f, 0f), this.fingers[fingerIndex].deltaPosition);
				if (evtType != EasyTouch.EvtType.None)
				{
					this.fingers[fingerIndex].oldSwipeType = swipe;
					this.CreateGesture(fingerIndex, evtType, this.fingers[fingerIndex], swipe, 0f, this.fingers[fingerIndex].deltaPosition);
					if (evtType == EasyTouch.EvtType.On_Drag && this.alwaysSendSwipe)
					{
						this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_Swipe, this.fingers[fingerIndex], swipe, 0f, this.fingers[fingerIndex].deltaPosition);
					}
				}
				this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_TouchDown, this.fingers[fingerIndex], swipe, 0f, this.fingers[fingerIndex].deltaPosition);
			}
			else
			{
				switch (this.fingers[fingerIndex].gesture)
				{
				case EasyTouch.GestureType.Drag:
					this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_DragEnd, this.fingers[fingerIndex], this.GetSwipe(this.fingers[fingerIndex].startPosition, this.fingers[fingerIndex].position), (this.fingers[fingerIndex].startPosition - this.fingers[fingerIndex].position).magnitude, this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition);
					if (this.alwaysSendSwipe)
					{
						this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_SwipeEnd, this.fingers[fingerIndex], this.GetSwipe(this.fingers[fingerIndex].startPosition, this.fingers[fingerIndex].position), (this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition).magnitude, this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition);
					}
					break;
				case EasyTouch.GestureType.Swipe:
					this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_SwipeEnd, this.fingers[fingerIndex], this.GetSwipe(this.fingers[fingerIndex].startPosition, this.fingers[fingerIndex].position), (this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition).magnitude, this.fingers[fingerIndex].position - this.fingers[fingerIndex].startPosition);
					break;
				case EasyTouch.GestureType.LongTap:
					this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_LongTapEnd, this.fingers[fingerIndex], EasyTouch.SwipeDirection.None, 0f, Vector2.zero);
					break;
				case EasyTouch.GestureType.Cancel:
					this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_Cancel, this.fingers[fingerIndex], EasyTouch.SwipeDirection.None, 0f, Vector2.zero);
					break;
				case EasyTouch.GestureType.Acquisition:
					if (this.doubleTapDetection == EasyTouch.DoubleTapDetection.BySystem)
					{
						if (this.FingerInTolerance(this.fingers[fingerIndex]))
						{
							if (this.fingers[fingerIndex].tapCount < 2)
							{
								this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_SimpleTap, this.fingers[fingerIndex], EasyTouch.SwipeDirection.None, 0f, Vector2.zero);
							}
							else
							{
								this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_DoubleTap, this.fingers[fingerIndex], EasyTouch.SwipeDirection.None, 0f, Vector2.zero);
							}
						}
					}
					else if (!this.singleDoubleTap[fingerIndex].inWait)
					{
						this.singleDoubleTap[fingerIndex].finger = this.fingers[fingerIndex];
						base.StartCoroutine(this.SingleOrDouble(fingerIndex));
					}
					else
					{
						this.singleDoubleTap[fingerIndex].count++;
					}
					break;
				}
				this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_TouchUp, this.fingers[fingerIndex], EasyTouch.SwipeDirection.None, 0f, Vector2.zero);
				this.fingers[fingerIndex] = null;
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x000E308C File Offset: 0x000E148C
		private IEnumerator SingleOrDouble(int fingerIndex)
		{
			this.singleDoubleTap[fingerIndex].inWait = true;
			float time2Wait = this.doubleTapTime - this.singleDoubleTap[fingerIndex].finger.actionTime;
			if (time2Wait < 0f)
			{
				time2Wait = 0f;
			}
			yield return new WaitForSeconds(time2Wait);
			if (this.singleDoubleTap[fingerIndex].count < 2)
			{
				this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_SimpleTap, this.singleDoubleTap[fingerIndex].finger, EasyTouch.SwipeDirection.None, 0f, this.singleDoubleTap[fingerIndex].finger.deltaPosition);
			}
			else
			{
				this.CreateGesture(fingerIndex, EasyTouch.EvtType.On_DoubleTap, this.singleDoubleTap[fingerIndex].finger, EasyTouch.SwipeDirection.None, 0f, this.singleDoubleTap[fingerIndex].finger.deltaPosition);
			}
			this.singleDoubleTap[fingerIndex].Stop();
			base.StopCoroutine("SingleOrDouble");
			yield break;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x000E30B0 File Offset: 0x000E14B0
		private void CreateGesture(int touchIndex, EasyTouch.EvtType message, Finger finger, EasyTouch.SwipeDirection swipe, float swipeLength, Vector2 swipeVector)
		{
			bool flag = true;
			if (this.autoUpdatePickedUI && this.allowUIDetection)
			{
				finger.isOverGui = this.IsScreenPositionOverUI(finger.position);
				finger.pickedUIElement = this.GetFirstUIElementFromCache();
			}
			if (this.enabledNGuiMode && message == EasyTouch.EvtType.On_TouchStart)
			{
				finger.isOverGui = (finger.isOverGui || this.IsTouchOverNGui(finger.position, false));
			}
			if (this.enableUIMode || this.enabledNGuiMode)
			{
				flag = !finger.isOverGui;
			}
			Gesture gesture = finger.GetGesture();
			if (this.autoUpdatePickedObject && this.autoSelect && message != EasyTouch.EvtType.On_Drag && message != EasyTouch.EvtType.On_DragEnd && message != EasyTouch.EvtType.On_DragStart)
			{
				if (this.GetPickedGameObject(finger, false))
				{
					gesture.pickedObject = this.pickedObject.pickedObj;
					gesture.pickedCamera = this.pickedObject.pickedCamera;
					gesture.isGuiCamera = this.pickedObject.isGUI;
				}
				else
				{
					gesture.pickedObject = null;
					gesture.pickedCamera = null;
					gesture.isGuiCamera = false;
				}
			}
			gesture.swipe = swipe;
			gesture.swipeLength = swipeLength;
			gesture.swipeVector = swipeVector;
			gesture.deltaPinch = 0f;
			gesture.twistAngle = 0f;
			if (flag)
			{
				this.RaiseEvent(message, gesture);
			}
			else if (finger.isOverGui)
			{
				if (message == EasyTouch.EvtType.On_TouchUp)
				{
					this.RaiseEvent(EasyTouch.EvtType.On_UIElementTouchUp, gesture);
				}
				else
				{
					this.RaiseEvent(EasyTouch.EvtType.On_OverUIElement, gesture);
				}
			}
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000E3240 File Offset: 0x000E1640
		private void TwoFinger()
		{
			bool flag = false;
			if (this.twoFinger.currentGesture == EasyTouch.GestureType.None)
			{
				if (!this.singleDoubleTap[99].inDoubleTap)
				{
					this.singleDoubleTap[99].inDoubleTap = true;
					this.singleDoubleTap[99].time = 0f;
					this.singleDoubleTap[99].count = 1;
				}
				this.twoFinger.finger0 = this.GetTwoFinger(-1);
				this.twoFinger.finger1 = this.GetTwoFinger(this.twoFinger.finger0);
				this.twoFinger.startTimeAction = Time.realtimeSinceStartup;
				this.twoFinger.currentGesture = EasyTouch.GestureType.Acquisition;
				this.fingers[this.twoFinger.finger0].startPosition = this.fingers[this.twoFinger.finger0].position;
				this.fingers[this.twoFinger.finger1].startPosition = this.fingers[this.twoFinger.finger1].position;
				this.fingers[this.twoFinger.finger0].oldPosition = this.fingers[this.twoFinger.finger0].position;
				this.fingers[this.twoFinger.finger1].oldPosition = this.fingers[this.twoFinger.finger1].position;
				this.twoFinger.oldFingerDistance = Mathf.Abs(Vector2.Distance(this.fingers[this.twoFinger.finger0].position, this.fingers[this.twoFinger.finger1].position));
				this.twoFinger.startPosition = new Vector2((this.fingers[this.twoFinger.finger0].position.x + this.fingers[this.twoFinger.finger1].position.x) / 2f, (this.fingers[this.twoFinger.finger0].position.y + this.fingers[this.twoFinger.finger1].position.y) / 2f);
				this.twoFinger.position = this.twoFinger.startPosition;
				this.twoFinger.oldStartPosition = this.twoFinger.startPosition;
				this.twoFinger.deltaPosition = Vector2.zero;
				this.twoFinger.startDistance = this.twoFinger.oldFingerDistance;
				if (this.autoSelect)
				{
					if (this.GetTwoFingerPickedObject())
					{
						this.twoFinger.pickedObject = this.pickedObject.pickedObj;
						this.twoFinger.pickedCamera = this.pickedObject.pickedCamera;
						this.twoFinger.isGuiCamera = this.pickedObject.isGUI;
					}
					else
					{
						this.twoFinger.ClearPickedObjectData();
					}
				}
				if (this.allowUIDetection)
				{
					if (this.GetTwoFingerPickedUIElement())
					{
						this.twoFinger.pickedUIElement = this.pickedObject.pickedObj;
						this.twoFinger.isOverGui = true;
					}
					else
					{
						this.twoFinger.ClearPickedUIData();
					}
				}
				this.CreateGesture2Finger(EasyTouch.EvtType.On_TouchStart2Fingers, this.twoFinger.startPosition, this.twoFinger.startPosition, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, this.twoFinger.oldFingerDistance);
			}
			if (this.singleDoubleTap[99].inDoubleTap)
			{
				this.singleDoubleTap[99].time += Time.deltaTime;
			}
			this.twoFinger.timeSinceStartAction = Time.realtimeSinceStartup - this.twoFinger.startTimeAction;
			this.twoFinger.position = new Vector2((this.fingers[this.twoFinger.finger0].position.x + this.fingers[this.twoFinger.finger1].position.x) / 2f, (this.fingers[this.twoFinger.finger0].position.y + this.fingers[this.twoFinger.finger1].position.y) / 2f);
			this.twoFinger.deltaPosition = this.twoFinger.position - this.twoFinger.oldStartPosition;
			this.twoFinger.fingerDistance = Mathf.Abs(Vector2.Distance(this.fingers[this.twoFinger.finger0].position, this.fingers[this.twoFinger.finger1].position));
			if (this.fingers[this.twoFinger.finger0].phase == TouchPhase.Canceled || this.fingers[this.twoFinger.finger1].phase == TouchPhase.Canceled)
			{
				this.twoFinger.currentGesture = EasyTouch.GestureType.Cancel;
			}
			if (this.fingers[this.twoFinger.finger0].phase != TouchPhase.Ended && this.fingers[this.twoFinger.finger1].phase != TouchPhase.Ended && this.twoFinger.currentGesture != EasyTouch.GestureType.Cancel)
			{
				if (this.twoFinger.currentGesture == EasyTouch.GestureType.Acquisition && this.twoFinger.timeSinceStartAction >= this.longTapTime && this.FingerInTolerance(this.fingers[this.twoFinger.finger0]) && this.FingerInTolerance(this.fingers[this.twoFinger.finger1]))
				{
					this.twoFinger.currentGesture = EasyTouch.GestureType.LongTap;
					this.CreateGesture2Finger(EasyTouch.EvtType.On_LongTapStart2Fingers, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, this.twoFinger.fingerDistance);
				}
				if (((!this.FingerInTolerance(this.fingers[this.twoFinger.finger0]) || !this.FingerInTolerance(this.fingers[this.twoFinger.finger1])) && this.gesturePriority == EasyTouch.GesturePriority.Tap) || ((this.fingers[this.twoFinger.finger0].phase == TouchPhase.Moved || this.fingers[this.twoFinger.finger1].phase == TouchPhase.Moved) && this.gesturePriority == EasyTouch.GesturePriority.Slips))
				{
					flag = true;
				}
				if (flag && this.twoFinger.currentGesture != EasyTouch.GestureType.Tap)
				{
					Vector2 currentDistance = this.fingers[this.twoFinger.finger0].position - this.fingers[this.twoFinger.finger1].position;
					Vector2 previousDistance = this.fingers[this.twoFinger.finger0].oldPosition - this.fingers[this.twoFinger.finger1].oldPosition;
					float currentDelta = currentDistance.magnitude - previousDistance.magnitude;
					if (this.enable2FingersSwipe)
					{
						float num = Vector2.Dot(this.fingers[this.twoFinger.finger0].deltaPosition.normalized, this.fingers[this.twoFinger.finger1].deltaPosition.normalized);
						if (num > 0f)
						{
							if (this.twoFinger.oldGesture == EasyTouch.GestureType.LongTap)
							{
								this.CreateStateEnd2Fingers(this.twoFinger.currentGesture, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, false, this.twoFinger.fingerDistance, 0f, 0f);
								this.twoFinger.startTimeAction = Time.realtimeSinceStartup;
							}
							if (this.twoFinger.pickedObject && !this.twoFinger.dragStart && !this.alwaysSendSwipe)
							{
								this.twoFinger.currentGesture = EasyTouch.GestureType.Drag;
								this.CreateGesture2Finger(EasyTouch.EvtType.On_DragStart2Fingers, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, this.twoFinger.fingerDistance);
								this.CreateGesture2Finger(EasyTouch.EvtType.On_SwipeStart2Fingers, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, this.twoFinger.fingerDistance);
								this.twoFinger.dragStart = true;
							}
							else if (!this.twoFinger.pickedObject && !this.twoFinger.swipeStart)
							{
								this.twoFinger.currentGesture = EasyTouch.GestureType.Swipe;
								this.CreateGesture2Finger(EasyTouch.EvtType.On_SwipeStart2Fingers, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, this.twoFinger.fingerDistance);
								this.twoFinger.swipeStart = true;
							}
						}
						else if (num < 0f)
						{
							this.twoFinger.dragStart = false;
							this.twoFinger.swipeStart = false;
						}
						if (this.twoFinger.dragStart)
						{
							this.CreateGesture2Finger(EasyTouch.EvtType.On_Drag2Fingers, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, this.GetSwipe(this.twoFinger.oldStartPosition, this.twoFinger.position), 0f, this.twoFinger.deltaPosition, 0f, 0f, this.twoFinger.fingerDistance);
							this.CreateGesture2Finger(EasyTouch.EvtType.On_Swipe2Fingers, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, this.GetSwipe(this.twoFinger.oldStartPosition, this.twoFinger.position), 0f, this.twoFinger.deltaPosition, 0f, 0f, this.twoFinger.fingerDistance);
						}
						if (this.twoFinger.swipeStart)
						{
							this.CreateGesture2Finger(EasyTouch.EvtType.On_Swipe2Fingers, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, this.GetSwipe(this.twoFinger.oldStartPosition, this.twoFinger.position), 0f, this.twoFinger.deltaPosition, 0f, 0f, this.twoFinger.fingerDistance);
						}
					}
					this.DetectPinch(currentDelta);
					this.DetecTwist(previousDistance, currentDistance, currentDelta);
				}
				else if (this.twoFinger.currentGesture == EasyTouch.GestureType.LongTap)
				{
					this.CreateGesture2Finger(EasyTouch.EvtType.On_LongTap2Fingers, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, this.twoFinger.fingerDistance);
				}
				this.CreateGesture2Finger(EasyTouch.EvtType.On_TouchDown2Fingers, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, this.GetSwipe(this.twoFinger.oldStartPosition, this.twoFinger.position), 0f, this.twoFinger.deltaPosition, 0f, 0f, this.twoFinger.fingerDistance);
				this.fingers[this.twoFinger.finger0].oldPosition = this.fingers[this.twoFinger.finger0].position;
				this.fingers[this.twoFinger.finger1].oldPosition = this.fingers[this.twoFinger.finger1].position;
				this.twoFinger.oldFingerDistance = this.twoFinger.fingerDistance;
				this.twoFinger.oldStartPosition = this.twoFinger.position;
				this.twoFinger.oldGesture = this.twoFinger.currentGesture;
			}
			else if (this.twoFinger.currentGesture != EasyTouch.GestureType.Acquisition && this.twoFinger.currentGesture != EasyTouch.GestureType.Tap)
			{
				this.CreateStateEnd2Fingers(this.twoFinger.currentGesture, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, true, this.twoFinger.fingerDistance, 0f, 0f);
				this.twoFinger.currentGesture = EasyTouch.GestureType.None;
				this.twoFinger.pickedObject = null;
				this.twoFinger.swipeStart = false;
				this.twoFinger.dragStart = false;
			}
			else
			{
				this.twoFinger.currentGesture = EasyTouch.GestureType.Tap;
				this.CreateStateEnd2Fingers(this.twoFinger.currentGesture, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, true, this.twoFinger.fingerDistance, 0f, 0f);
			}
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000E400C File Offset: 0x000E240C
		private void DetectPinch(float currentDelta)
		{
			if (this.enablePinch)
			{
				if ((Mathf.Abs(this.twoFinger.fingerDistance - this.twoFinger.startDistance) >= this.minPinchLength && this.twoFinger.currentGesture != EasyTouch.GestureType.Pinch) || this.twoFinger.currentGesture == EasyTouch.GestureType.Pinch)
				{
					if (currentDelta != 0f && this.twoFinger.oldGesture == EasyTouch.GestureType.LongTap)
					{
						this.CreateStateEnd2Fingers(this.twoFinger.currentGesture, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, false, this.twoFinger.fingerDistance, 0f, 0f);
						this.twoFinger.startTimeAction = Time.realtimeSinceStartup;
					}
					this.twoFinger.currentGesture = EasyTouch.GestureType.Pinch;
					if (currentDelta > 0f)
					{
						this.CreateGesture2Finger(EasyTouch.EvtType.On_PinchOut, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, this.GetSwipe(this.twoFinger.startPosition, this.twoFinger.position), 0f, Vector2.zero, 0f, Mathf.Abs(this.twoFinger.fingerDistance - this.twoFinger.oldFingerDistance), this.twoFinger.fingerDistance);
					}
					if (currentDelta < 0f)
					{
						this.CreateGesture2Finger(EasyTouch.EvtType.On_PinchIn, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, this.GetSwipe(this.twoFinger.startPosition, this.twoFinger.position), 0f, Vector2.zero, 0f, Mathf.Abs(this.twoFinger.fingerDistance - this.twoFinger.oldFingerDistance), this.twoFinger.fingerDistance);
					}
					if (currentDelta < 0f || currentDelta > 0f)
					{
						this.CreateGesture2Finger(EasyTouch.EvtType.On_Pinch, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, this.GetSwipe(this.twoFinger.startPosition, this.twoFinger.position), 0f, Vector2.zero, 0f, currentDelta, this.twoFinger.fingerDistance);
					}
				}
				this.twoFinger.lastPinch = ((currentDelta <= 0f) ? this.twoFinger.lastPinch : currentDelta);
			}
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000E42C4 File Offset: 0x000E26C4
		private void DetecTwist(Vector2 previousDistance, Vector2 currentDistance, float currentDelta)
		{
			if (this.enableTwist)
			{
				float num = Vector2.Angle(previousDistance, currentDistance);
				if (previousDistance == currentDistance)
				{
					num = 0f;
				}
				if ((Mathf.Abs(num) >= this.minTwistAngle && this.twoFinger.currentGesture != EasyTouch.GestureType.Twist) || this.twoFinger.currentGesture == EasyTouch.GestureType.Twist)
				{
					if (this.twoFinger.oldGesture == EasyTouch.GestureType.LongTap)
					{
						this.CreateStateEnd2Fingers(this.twoFinger.currentGesture, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, false, this.twoFinger.fingerDistance, 0f, 0f);
						this.twoFinger.startTimeAction = Time.realtimeSinceStartup;
					}
					this.twoFinger.currentGesture = EasyTouch.GestureType.Twist;
					if (num != 0f)
					{
						num *= Mathf.Sign(Vector3.Cross(previousDistance, currentDistance).z);
					}
					this.CreateGesture2Finger(EasyTouch.EvtType.On_Twist, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, num, 0f, this.twoFinger.fingerDistance);
				}
				this.twoFinger.lastTwistAngle = ((num == 0f) ? this.twoFinger.lastTwistAngle : num);
			}
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000E444C File Offset: 0x000E284C
		private void CreateStateEnd2Fingers(EasyTouch.GestureType gesture, Vector2 startPosition, Vector2 position, Vector2 deltaPosition, float time, bool realEnd, float fingerDistance, float twist = 0f, float pinch = 0f)
		{
			switch (gesture)
			{
			case EasyTouch.GestureType.LongTap:
				this.CreateGesture2Finger(EasyTouch.EvtType.On_LongTapEnd2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
				goto IL_1D2;
			case EasyTouch.GestureType.Pinch:
				this.CreateGesture2Finger(EasyTouch.EvtType.On_PinchEnd, startPosition, position, deltaPosition, time, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, this.twoFinger.lastPinch, fingerDistance);
				goto IL_1D2;
			case EasyTouch.GestureType.Twist:
				this.CreateGesture2Finger(EasyTouch.EvtType.On_TwistEnd, startPosition, position, deltaPosition, time, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, this.twoFinger.lastTwistAngle, 0f, fingerDistance);
				goto IL_1D2;
			default:
				if (gesture != EasyTouch.GestureType.Tap)
				{
					goto IL_1D2;
				}
				break;
			case EasyTouch.GestureType.Acquisition:
				break;
			}
			if (this.doubleTapDetection == EasyTouch.DoubleTapDetection.BySystem)
			{
				if (this.fingers[this.twoFinger.finger0].tapCount < 2 && this.fingers[this.twoFinger.finger1].tapCount < 2)
				{
					this.CreateGesture2Finger(EasyTouch.EvtType.On_SimpleTap2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
				}
				else
				{
					this.CreateGesture2Finger(EasyTouch.EvtType.On_DoubleTap2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
				}
				this.twoFinger.currentGesture = EasyTouch.GestureType.None;
				this.twoFinger.pickedObject = null;
				this.twoFinger.swipeStart = false;
				this.twoFinger.dragStart = false;
				this.singleDoubleTap[99].Stop();
				base.StopCoroutine("SingleOrDouble2Fingers");
			}
			else if (!this.singleDoubleTap[99].inWait)
			{
				base.StartCoroutine("SingleOrDouble2Fingers");
			}
			else
			{
				this.singleDoubleTap[99].count++;
			}
			IL_1D2:
			if (realEnd)
			{
				if (this.twoFinger.dragStart)
				{
					this.CreateGesture2Finger(EasyTouch.EvtType.On_DragEnd2Fingers, startPosition, position, deltaPosition, time, this.GetSwipe(startPosition, position), (position - startPosition).magnitude, position - startPosition, 0f, 0f, fingerDistance);
				}
				if (this.twoFinger.swipeStart)
				{
					this.CreateGesture2Finger(EasyTouch.EvtType.On_SwipeEnd2Fingers, startPosition, position, deltaPosition, time, this.GetSwipe(startPosition, position), (position - startPosition).magnitude, position - startPosition, 0f, 0f, fingerDistance);
				}
				this.CreateGesture2Finger(EasyTouch.EvtType.On_TouchUp2Fingers, startPosition, position, deltaPosition, time, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, fingerDistance);
			}
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x000E46E8 File Offset: 0x000E2AE8
		private IEnumerator SingleOrDouble2Fingers()
		{
			this.singleDoubleTap[99].inWait = true;
			yield return new WaitForSeconds(this.doubleTapTime);
			if (this.singleDoubleTap[99].count < 2)
			{
				this.CreateGesture2Finger(EasyTouch.EvtType.On_SimpleTap2Fingers, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, this.twoFinger.fingerDistance);
			}
			else
			{
				this.CreateGesture2Finger(EasyTouch.EvtType.On_DoubleTap2Fingers, this.twoFinger.startPosition, this.twoFinger.position, this.twoFinger.deltaPosition, this.twoFinger.timeSinceStartAction, EasyTouch.SwipeDirection.None, 0f, Vector2.zero, 0f, 0f, this.twoFinger.fingerDistance);
			}
			this.twoFinger.currentGesture = EasyTouch.GestureType.None;
			this.twoFinger.pickedObject = null;
			this.twoFinger.swipeStart = false;
			this.twoFinger.dragStart = false;
			this.singleDoubleTap[99].Stop();
			base.StopCoroutine("SingleOrDouble2Fingers");
			yield break;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000E4704 File Offset: 0x000E2B04
		private void CreateGesture2Finger(EasyTouch.EvtType message, Vector2 startPosition, Vector2 position, Vector2 deltaPosition, float actionTime, EasyTouch.SwipeDirection swipe, float swipeLength, Vector2 swipeVector, float twist, float pinch, float twoDistance)
		{
			bool flag = true;
			Gesture gesture = new Gesture();
			gesture.isOverGui = false;
			if (this.enabledNGuiMode && message == EasyTouch.EvtType.On_TouchStart2Fingers)
			{
				gesture.isOverGui = (gesture.isOverGui || (this.IsTouchOverNGui(this.twoFinger.position, false) && this.IsTouchOverNGui(this.twoFinger.position, false)));
			}
			gesture.touchCount = 2;
			gesture.fingerIndex = -1;
			gesture.startPosition = startPosition;
			gesture.position = position;
			gesture.deltaPosition = deltaPosition;
			gesture.actionTime = actionTime;
			gesture.deltaTime = Time.deltaTime;
			gesture.swipe = swipe;
			gesture.swipeLength = swipeLength;
			gesture.swipeVector = swipeVector;
			gesture.deltaPinch = pinch;
			gesture.twistAngle = twist;
			gesture.twoFingerDistance = twoDistance;
			gesture.pickedObject = this.twoFinger.pickedObject;
			gesture.pickedCamera = this.twoFinger.pickedCamera;
			gesture.isGuiCamera = this.twoFinger.isGuiCamera;
			if (this.autoUpdatePickedObject && message != EasyTouch.EvtType.On_Drag && message != EasyTouch.EvtType.On_DragEnd && message != EasyTouch.EvtType.On_Twist && message != EasyTouch.EvtType.On_TwistEnd && message != EasyTouch.EvtType.On_Pinch && message != EasyTouch.EvtType.On_PinchEnd && message != EasyTouch.EvtType.On_PinchIn && message != EasyTouch.EvtType.On_PinchOut)
			{
				if (this.GetTwoFingerPickedObject())
				{
					gesture.pickedObject = this.pickedObject.pickedObj;
					gesture.pickedCamera = this.pickedObject.pickedCamera;
					gesture.isGuiCamera = this.pickedObject.isGUI;
				}
				else
				{
					this.twoFinger.ClearPickedObjectData();
				}
			}
			gesture.pickedUIElement = this.twoFinger.pickedUIElement;
			gesture.isOverGui = this.twoFinger.isOverGui;
			if (this.allowUIDetection && this.autoUpdatePickedUI && message != EasyTouch.EvtType.On_Drag && message != EasyTouch.EvtType.On_DragEnd && message != EasyTouch.EvtType.On_Twist && message != EasyTouch.EvtType.On_TwistEnd && message != EasyTouch.EvtType.On_Pinch && message != EasyTouch.EvtType.On_PinchEnd && message != EasyTouch.EvtType.On_PinchIn && message != EasyTouch.EvtType.On_PinchOut && message == EasyTouch.EvtType.On_SimpleTap2Fingers)
			{
				if (this.GetTwoFingerPickedUIElement())
				{
					gesture.pickedUIElement = this.pickedObject.pickedObj;
					gesture.isOverGui = true;
				}
				else
				{
					this.twoFinger.ClearPickedUIData();
				}
			}
			if (this.enableUIMode || (this.enabledNGuiMode && this.allowUIDetection))
			{
				flag = !gesture.isOverGui;
			}
			if (flag)
			{
				this.RaiseEvent(message, gesture);
			}
			else if (gesture.isOverGui)
			{
				if (message == EasyTouch.EvtType.On_TouchUp2Fingers)
				{
					this.RaiseEvent(EasyTouch.EvtType.On_UIElementTouchUp, gesture);
				}
				else
				{
					this.RaiseEvent(EasyTouch.EvtType.On_OverUIElement, gesture);
				}
			}
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000E49C4 File Offset: 0x000E2DC4
		private int GetTwoFinger(int index)
		{
			int num = index + 1;
			bool flag = false;
			while (num < 10 && !flag)
			{
				if (this.fingers[num] != null && num >= index)
				{
					flag = true;
				}
				num++;
			}
			return num - 1;
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x000E4A0C File Offset: 0x000E2E0C
		private bool GetTwoFingerPickedObject()
		{
			bool result = false;
			if (this.twoFingerPickMethod == EasyTouch.TwoFingerPickMethod.Finger)
			{
				if (this.GetPickedGameObject(this.fingers[this.twoFinger.finger0], false))
				{
					GameObject pickedObj = this.pickedObject.pickedObj;
					if (this.GetPickedGameObject(this.fingers[this.twoFinger.finger1], false) && pickedObj == this.pickedObject.pickedObj)
					{
						result = true;
					}
				}
			}
			else if (this.GetPickedGameObject(this.fingers[this.twoFinger.finger0], true))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000E4AAC File Offset: 0x000E2EAC
		private bool GetTwoFingerPickedUIElement()
		{
			bool result = false;
			if (this.fingers[this.twoFinger.finger0] == null)
			{
				return false;
			}
			if (this.twoFingerPickMethod == EasyTouch.TwoFingerPickMethod.Finger)
			{
				if (this.IsScreenPositionOverUI(this.fingers[this.twoFinger.finger0].position))
				{
					GameObject firstUIElementFromCache = this.GetFirstUIElementFromCache();
					if (this.IsScreenPositionOverUI(this.fingers[this.twoFinger.finger1].position))
					{
						GameObject firstUIElementFromCache2 = this.GetFirstUIElementFromCache();
						if (firstUIElementFromCache2 == firstUIElementFromCache || firstUIElementFromCache2.transform.IsChildOf(firstUIElementFromCache.transform) || firstUIElementFromCache.transform.IsChildOf(firstUIElementFromCache2.transform))
						{
							this.pickedObject.pickedObj = firstUIElementFromCache;
							this.pickedObject.isGUI = true;
							result = true;
						}
					}
				}
			}
			else if (this.IsScreenPositionOverUI(this.twoFinger.position))
			{
				this.pickedObject.pickedObj = this.GetFirstUIElementFromCache();
				this.pickedObject.isGUI = true;
				result = true;
			}
			return result;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000E4BC0 File Offset: 0x000E2FC0
		private void RaiseEvent(EasyTouch.EvtType evnt, Gesture gesture)
		{
			gesture.type = evnt;
			switch (evnt)
			{
			case EasyTouch.EvtType.On_TouchStart:
				if (EasyTouch.On_TouchStart != null)
				{
					EasyTouch.On_TouchStart(gesture);
				}
				break;
			case EasyTouch.EvtType.On_TouchDown:
				if (EasyTouch.On_TouchDown != null)
				{
					EasyTouch.On_TouchDown(gesture);
				}
				break;
			case EasyTouch.EvtType.On_TouchUp:
				if (EasyTouch.On_TouchUp != null)
				{
					EasyTouch.On_TouchUp(gesture);
				}
				break;
			case EasyTouch.EvtType.On_SimpleTap:
				if (EasyTouch.On_SimpleTap != null)
				{
					EasyTouch.On_SimpleTap(gesture);
				}
				break;
			case EasyTouch.EvtType.On_DoubleTap:
				if (EasyTouch.On_DoubleTap != null)
				{
					EasyTouch.On_DoubleTap(gesture);
				}
				break;
			case EasyTouch.EvtType.On_LongTapStart:
				if (EasyTouch.On_LongTapStart != null)
				{
					EasyTouch.On_LongTapStart(gesture);
				}
				break;
			case EasyTouch.EvtType.On_LongTap:
				if (EasyTouch.On_LongTap != null)
				{
					EasyTouch.On_LongTap(gesture);
				}
				break;
			case EasyTouch.EvtType.On_LongTapEnd:
				if (EasyTouch.On_LongTapEnd != null)
				{
					EasyTouch.On_LongTapEnd(gesture);
				}
				break;
			case EasyTouch.EvtType.On_DragStart:
				if (EasyTouch.On_DragStart != null)
				{
					EasyTouch.On_DragStart(gesture);
				}
				break;
			case EasyTouch.EvtType.On_Drag:
				if (EasyTouch.On_Drag != null)
				{
					EasyTouch.On_Drag(gesture);
				}
				break;
			case EasyTouch.EvtType.On_DragEnd:
				if (EasyTouch.On_DragEnd != null)
				{
					EasyTouch.On_DragEnd(gesture);
				}
				break;
			case EasyTouch.EvtType.On_SwipeStart:
				if (EasyTouch.On_SwipeStart != null)
				{
					EasyTouch.On_SwipeStart(gesture);
				}
				break;
			case EasyTouch.EvtType.On_Swipe:
				if (EasyTouch.On_Swipe != null)
				{
					EasyTouch.On_Swipe(gesture);
				}
				break;
			case EasyTouch.EvtType.On_SwipeEnd:
				if (EasyTouch.On_SwipeEnd != null)
				{
					EasyTouch.On_SwipeEnd(gesture);
				}
				break;
			case EasyTouch.EvtType.On_TouchStart2Fingers:
				if (EasyTouch.On_TouchStart2Fingers != null)
				{
					EasyTouch.On_TouchStart2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_TouchDown2Fingers:
				if (EasyTouch.On_TouchDown2Fingers != null)
				{
					EasyTouch.On_TouchDown2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_TouchUp2Fingers:
				if (EasyTouch.On_TouchUp2Fingers != null)
				{
					EasyTouch.On_TouchUp2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_SimpleTap2Fingers:
				if (EasyTouch.On_SimpleTap2Fingers != null)
				{
					EasyTouch.On_SimpleTap2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_DoubleTap2Fingers:
				if (EasyTouch.On_DoubleTap2Fingers != null)
				{
					EasyTouch.On_DoubleTap2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_LongTapStart2Fingers:
				if (EasyTouch.On_LongTapStart2Fingers != null)
				{
					EasyTouch.On_LongTapStart2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_LongTap2Fingers:
				if (EasyTouch.On_LongTap2Fingers != null)
				{
					EasyTouch.On_LongTap2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_LongTapEnd2Fingers:
				if (EasyTouch.On_LongTapEnd2Fingers != null)
				{
					EasyTouch.On_LongTapEnd2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_Twist:
				if (EasyTouch.On_Twist != null)
				{
					EasyTouch.On_Twist(gesture);
				}
				break;
			case EasyTouch.EvtType.On_TwistEnd:
				if (EasyTouch.On_TwistEnd != null)
				{
					EasyTouch.On_TwistEnd(gesture);
				}
				break;
			case EasyTouch.EvtType.On_Pinch:
				if (EasyTouch.On_Pinch != null)
				{
					EasyTouch.On_Pinch(gesture);
				}
				break;
			case EasyTouch.EvtType.On_PinchIn:
				if (EasyTouch.On_PinchIn != null)
				{
					EasyTouch.On_PinchIn(gesture);
				}
				break;
			case EasyTouch.EvtType.On_PinchOut:
				if (EasyTouch.On_PinchOut != null)
				{
					EasyTouch.On_PinchOut(gesture);
				}
				break;
			case EasyTouch.EvtType.On_PinchEnd:
				if (EasyTouch.On_PinchEnd != null)
				{
					EasyTouch.On_PinchEnd(gesture);
				}
				break;
			case EasyTouch.EvtType.On_DragStart2Fingers:
				if (EasyTouch.On_DragStart2Fingers != null)
				{
					EasyTouch.On_DragStart2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_Drag2Fingers:
				if (EasyTouch.On_Drag2Fingers != null)
				{
					EasyTouch.On_Drag2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_DragEnd2Fingers:
				if (EasyTouch.On_DragEnd2Fingers != null)
				{
					EasyTouch.On_DragEnd2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_SwipeStart2Fingers:
				if (EasyTouch.On_SwipeStart2Fingers != null)
				{
					EasyTouch.On_SwipeStart2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_Swipe2Fingers:
				if (EasyTouch.On_Swipe2Fingers != null)
				{
					EasyTouch.On_Swipe2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_SwipeEnd2Fingers:
				if (EasyTouch.On_SwipeEnd2Fingers != null)
				{
					EasyTouch.On_SwipeEnd2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_Cancel:
				if (EasyTouch.On_Cancel != null)
				{
					EasyTouch.On_Cancel(gesture);
				}
				break;
			case EasyTouch.EvtType.On_Cancel2Fingers:
				if (EasyTouch.On_Cancel2Fingers != null)
				{
					EasyTouch.On_Cancel2Fingers(gesture);
				}
				break;
			case EasyTouch.EvtType.On_OverUIElement:
				if (EasyTouch.On_OverUIElement != null)
				{
					EasyTouch.On_OverUIElement(gesture);
				}
				break;
			case EasyTouch.EvtType.On_UIElementTouchUp:
				if (EasyTouch.On_UIElementTouchUp != null)
				{
					EasyTouch.On_UIElementTouchUp(gesture);
				}
				break;
			}
			int num = this._currentGestures.FindIndex((Gesture obj) => obj.type == gesture.type && obj.fingerIndex == gesture.fingerIndex);
			if (num > -1)
			{
				this._currentGestures[num].touchCount = gesture.touchCount;
				this._currentGestures[num].position = gesture.position;
				this._currentGestures[num].actionTime = gesture.actionTime;
				this._currentGestures[num].pickedCamera = gesture.pickedCamera;
				this._currentGestures[num].pickedObject = gesture.pickedObject;
				this._currentGestures[num].pickedUIElement = gesture.pickedUIElement;
				this._currentGestures[num].isOverGui = gesture.isOverGui;
				this._currentGestures[num].isGuiCamera = gesture.isGuiCamera;
				this._currentGestures[num].deltaPinch += gesture.deltaPinch;
				this._currentGestures[num].deltaPosition += gesture.deltaPosition;
				this._currentGestures[num].deltaTime += gesture.deltaTime;
				this._currentGestures[num].twistAngle += gesture.twistAngle;
			}
			if (num == -1)
			{
				this._currentGestures.Add((Gesture)gesture.Clone());
				if (this._currentGestures.Count > 0)
				{
					this._currentGesture = this._currentGestures[0];
				}
			}
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000E5300 File Offset: 0x000E3700
		private bool GetPickedGameObject(Finger finger, bool isTowFinger = false)
		{
			if (finger == null && !isTowFinger)
			{
				return false;
			}
			this.pickedObject.isGUI = false;
			this.pickedObject.pickedObj = null;
			this.pickedObject.pickedCamera = null;
			if (this.touchCameras.Count > 0)
			{
				for (int i = 0; i < this.touchCameras.Count; i++)
				{
					if (this.touchCameras[i].camera != null && this.touchCameras[i].camera.enabled)
					{
						Vector2 position = Vector2.zero;
						if (!isTowFinger)
						{
							position = finger.position;
						}
						else
						{
							position = this.twoFinger.position;
						}
						if (this.GetGameObjectAt(position, this.touchCameras[i].camera, this.touchCameras[i].guiCamera))
						{
							return true;
						}
					}
				}
			}
			else
			{
				UnityEngine.Debug.LogWarning("No camera is assigned to EasyTouch");
			}
			return false;
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000E5408 File Offset: 0x000E3808
		private bool GetGameObjectAt(Vector2 position, Camera cam, bool isGuiCam)
		{
			Ray ray = cam.ScreenPointToRay(position);
			if (this.enable2D)
			{
				LayerMask mask = this.pickableLayers2D;
				RaycastHit2D[] array = new RaycastHit2D[1];
				if (Physics2D.GetRayIntersectionNonAlloc(ray, array, float.PositiveInfinity, mask) > 0)
				{
					this.pickedObject.pickedCamera = cam;
					this.pickedObject.isGUI = isGuiCam;
					this.pickedObject.pickedObj = array[0].collider.gameObject;
					return true;
				}
			}
			LayerMask mask2 = this.pickableLayers3D;
			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, 3.4028235E+38f, mask2))
			{
				this.pickedObject.pickedCamera = cam;
				this.pickedObject.isGUI = isGuiCam;
				this.pickedObject.pickedObj = raycastHit.collider.gameObject;
				return true;
			}
			return false;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000E54DC File Offset: 0x000E38DC
		private EasyTouch.SwipeDirection GetSwipe(Vector2 start, Vector2 end)
		{
			Vector2 normalized = (end - start).normalized;
			if (Vector2.Dot(normalized, Vector2.up) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeDirection.Up;
			}
			if (Vector2.Dot(normalized, -Vector2.up) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeDirection.Down;
			}
			if (Vector2.Dot(normalized, Vector2.right) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeDirection.Right;
			}
			if (Vector2.Dot(normalized, -Vector2.right) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeDirection.Left;
			}
			Vector2 lhs = normalized;
			Vector2 vector = new Vector2(0.5f, 0.5f);
			if (Vector2.Dot(lhs, vector.normalized) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeDirection.UpRight;
			}
			Vector2 lhs2 = normalized;
			Vector2 vector2 = new Vector2(0.5f, -0.5f);
			if (Vector2.Dot(lhs2, vector2.normalized) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeDirection.DownRight;
			}
			Vector2 lhs3 = normalized;
			Vector2 vector3 = new Vector2(-0.5f, 0.5f);
			if (Vector2.Dot(lhs3, vector3.normalized) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeDirection.UpLeft;
			}
			Vector2 lhs4 = normalized;
			Vector2 vector4 = new Vector2(-0.5f, -0.5f);
			if (Vector2.Dot(lhs4, vector4.normalized) >= this.swipeTolerance)
			{
				return EasyTouch.SwipeDirection.DownLeft;
			}
			return EasyTouch.SwipeDirection.Other;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x000E5614 File Offset: 0x000E3A14
		private bool FingerInTolerance(Finger finger)
		{
			return (finger.position - finger.startPosition).sqrMagnitude <= this.StationaryTolerance * this.StationaryTolerance;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000E5650 File Offset: 0x000E3A50
		private bool IsTouchOverNGui(Vector2 position, bool isTwoFingers = false)
		{
			bool flag = false;
			if (this.enabledNGuiMode)
			{
				LayerMask mask = this.nGUILayers;
				int num = 0;
				while (!flag && num < this.nGUICameras.Count)
				{
					Vector2 v = Vector2.zero;
					if (!isTwoFingers)
					{
						v = position;
					}
					else
					{
						v = this.twoFinger.position;
					}
					Ray ray = this.nGUICameras[num].ScreenPointToRay(v);
					RaycastHit raycastHit;
					flag = Physics.Raycast(ray, out raycastHit, float.MaxValue, mask);
					num++;
				}
			}
			return flag;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000E56E8 File Offset: 0x000E3AE8
		private Finger GetFinger(int finderId)
		{
			int num = 0;
			Finger finger = null;
			while (num < 10 && finger == null)
			{
				if (this.fingers[num] != null && this.fingers[num].fingerIndex == finderId)
				{
					finger = this.fingers[num];
				}
				num++;
			}
			return finger;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000E573C File Offset: 0x000E3B3C
		private bool IsScreenPositionOverUI(Vector2 position)
		{
			this.uiEventSystem = EventSystem.current;
			if (this.uiEventSystem != null)
			{
				this.uiPointerEventData = new PointerEventData(this.uiEventSystem);
				this.uiPointerEventData.position = position;
				this.uiEventSystem.RaycastAll(this.uiPointerEventData, this.uiRaycastResultCache);
				return this.uiRaycastResultCache.Count > 0;
			}
			return false;
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000E57B0 File Offset: 0x000E3BB0
		private GameObject GetFirstUIElementFromCache()
		{
			if (this.uiRaycastResultCache.Count > 0)
			{
				return this.uiRaycastResultCache[0].gameObject;
			}
			return null;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000E57E4 File Offset: 0x000E3BE4
		private GameObject GetFirstUIElement(Vector2 position)
		{
			if (this.IsScreenPositionOverUI(position))
			{
				return this.GetFirstUIElementFromCache();
			}
			return null;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000E57FC File Offset: 0x000E3BFC
		public static bool IsFingerOverUIElement(int fingerIndex)
		{
			if (EasyTouch.instance != null)
			{
				Finger finger = EasyTouch.instance.GetFinger(fingerIndex);
				return finger != null && EasyTouch.instance.IsScreenPositionOverUI(finger.position);
			}
			return false;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x000E5840 File Offset: 0x000E3C40
		public static GameObject GetCurrentPickedUIElement(int fingerIndex, bool isTwoFinger)
		{
			if (!(EasyTouch.instance != null))
			{
				return null;
			}
			Finger finger = EasyTouch.instance.GetFinger(fingerIndex);
			if (finger != null || isTwoFinger)
			{
				Vector2 position = Vector2.zero;
				if (!isTwoFinger)
				{
					position = finger.position;
				}
				else
				{
					position = EasyTouch.instance.twoFinger.position;
				}
				return EasyTouch.instance.GetFirstUIElement(position);
			}
			return null;
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000E58AC File Offset: 0x000E3CAC
		public static GameObject GetCurrentPickedObject(int fingerIndex, bool isTwoFinger)
		{
			if (!(EasyTouch.instance != null))
			{
				return null;
			}
			Finger finger = EasyTouch.instance.GetFinger(fingerIndex);
			if ((finger != null || isTwoFinger) && EasyTouch.instance.GetPickedGameObject(finger, isTwoFinger))
			{
				return EasyTouch.instance.pickedObject.pickedObj;
			}
			return null;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000E5908 File Offset: 0x000E3D08
		public static GameObject GetGameObjectAt(Vector2 position, bool isTwoFinger = false)
		{
			if (EasyTouch.instance != null)
			{
				if (isTwoFinger)
				{
					position = EasyTouch.instance.twoFinger.position;
				}
				if (EasyTouch.instance.touchCameras.Count > 0)
				{
					int i = 0;
					while (i < EasyTouch.instance.touchCameras.Count)
					{
						if (EasyTouch.instance.touchCameras[i].camera != null && EasyTouch.instance.touchCameras[i].camera.enabled)
						{
							if (EasyTouch.instance.GetGameObjectAt(position, EasyTouch.instance.touchCameras[i].camera, EasyTouch.instance.touchCameras[i].guiCamera))
							{
								return EasyTouch.instance.pickedObject.pickedObj;
							}
							return null;
						}
						else
						{
							i++;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000E59FD File Offset: 0x000E3DFD
		public static int GetTouchCount()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.input.TouchCount();
			}
			return 0;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000E5A1F File Offset: 0x000E3E1F
		public static void ResetTouch(int fingerIndex)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.GetFinger(fingerIndex).gesture = EasyTouch.GestureType.None;
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000E5A41 File Offset: 0x000E3E41
		public static void SetEnabled(bool enable)
		{
			EasyTouch.instance.enable = enable;
			if (enable)
			{
				EasyTouch.instance.ResetTouches();
			}
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000E5A5E File Offset: 0x000E3E5E
		public static bool GetEnabled()
		{
			return EasyTouch.instance && EasyTouch.instance.enable;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000E5A7B File Offset: 0x000E3E7B
		public static void SetEnableUIDetection(bool enable)
		{
			if (EasyTouch.instance != null)
			{
				EasyTouch.instance.allowUIDetection = enable;
			}
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x000E5A98 File Offset: 0x000E3E98
		public static bool GetEnableUIDetection()
		{
			return EasyTouch.instance && EasyTouch.instance.allowUIDetection;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x000E5AB5 File Offset: 0x000E3EB5
		public static void SetUICompatibily(bool value)
		{
			if (EasyTouch.instance != null)
			{
				EasyTouch.instance.enableUIMode = value;
			}
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x000E5AD2 File Offset: 0x000E3ED2
		public static bool GetUIComptability()
		{
			return EasyTouch.instance != null && EasyTouch.instance.enableUIMode;
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x000E5AF0 File Offset: 0x000E3EF0
		public static void SetAutoUpdateUI(bool value)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.autoUpdatePickedUI = value;
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x000E5B0C File Offset: 0x000E3F0C
		public static bool GetAutoUpdateUI()
		{
			return EasyTouch.instance && EasyTouch.instance.autoUpdatePickedUI;
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x000E5B29 File Offset: 0x000E3F29
		public static void SetNGUICompatibility(bool value)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.enabledNGuiMode = value;
			}
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x000E5B45 File Offset: 0x000E3F45
		public static bool GetNGUICompatibility()
		{
			return EasyTouch.instance && EasyTouch.instance.enabledNGuiMode;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x000E5B62 File Offset: 0x000E3F62
		public static void SetEnableAutoSelect(bool value)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.autoSelect = value;
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x000E5B7E File Offset: 0x000E3F7E
		public static bool GetEnableAutoSelect()
		{
			return EasyTouch.instance && EasyTouch.instance.autoSelect;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000E5B9B File Offset: 0x000E3F9B
		public static void SetAutoUpdatePickedObject(bool value)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.autoUpdatePickedObject = value;
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000E5BB7 File Offset: 0x000E3FB7
		public static bool GetAutoUpdatePickedObject()
		{
			return EasyTouch.instance && EasyTouch.instance.autoUpdatePickedObject;
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000E5BD4 File Offset: 0x000E3FD4
		public static void Set3DPickableLayer(LayerMask mask)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.pickableLayers3D = mask;
			}
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x000E5BF0 File Offset: 0x000E3FF0
		public static LayerMask Get3DPickableLayer()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.pickableLayers3D;
			}
			return LayerMask.GetMask(new string[]
			{
				"Default"
			});
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000E5C24 File Offset: 0x000E4024
		public static void AddCamera(Camera cam, bool guiCam = false)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.touchCameras.Add(new ECamera(cam, guiCam));
			}
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x000E5C4C File Offset: 0x000E404C
		public static void RemoveCamera(Camera cam)
		{
			if (EasyTouch.instance)
			{
				int num = EasyTouch.instance.touchCameras.FindIndex((ECamera c) => c.camera == cam);
				if (num > -1)
				{
					EasyTouch.instance.touchCameras[num] = null;
					EasyTouch.instance.touchCameras.RemoveAt(num);
				}
			}
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x000E5CB9 File Offset: 0x000E40B9
		public static Camera GetCamera(int index = 0)
		{
			if (!EasyTouch.instance)
			{
				return null;
			}
			if (index < EasyTouch.instance.touchCameras.Count)
			{
				return EasyTouch.instance.touchCameras[index].camera;
			}
			return null;
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x000E5CF8 File Offset: 0x000E40F8
		public static void SetEnable2DCollider(bool value)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.enable2D = value;
			}
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x000E5D14 File Offset: 0x000E4114
		public static bool GetEnable2DCollider()
		{
			return EasyTouch.instance && EasyTouch.instance.enable2D;
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x000E5D31 File Offset: 0x000E4131
		public static void Set2DPickableLayer(LayerMask mask)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.pickableLayers2D = mask;
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x000E5D4D File Offset: 0x000E414D
		public static LayerMask Get2DPickableLayer()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.pickableLayers2D;
			}
			return LayerMask.GetMask(new string[]
			{
				"Default"
			});
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000E5D81 File Offset: 0x000E4181
		public static void SetGesturePriority(EasyTouch.GesturePriority value)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.gesturePriority = value;
			}
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000E5D9D File Offset: 0x000E419D
		public static EasyTouch.GesturePriority GetGesturePriority()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.gesturePriority;
			}
			return EasyTouch.GesturePriority.Tap;
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000E5DBA File Offset: 0x000E41BA
		public static void SetStationaryTolerance(float tolerance)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.StationaryTolerance = tolerance;
			}
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x000E5DD6 File Offset: 0x000E41D6
		public static float GetStationaryTolerance()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.StationaryTolerance;
			}
			return -1f;
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x000E5DF7 File Offset: 0x000E41F7
		public static void SetLongTapTime(float time)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.longTapTime = time;
			}
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x000E5E13 File Offset: 0x000E4213
		public static float GetlongTapTime()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.longTapTime;
			}
			return -1f;
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x000E5E34 File Offset: 0x000E4234
		public static void SetDoubleTapTime(float time)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.doubleTapTime = time;
			}
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x000E5E50 File Offset: 0x000E4250
		public static float GetDoubleTapTime()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.doubleTapTime;
			}
			return -1f;
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x000E5E71 File Offset: 0x000E4271
		public static void SetDoubleTapMethod(EasyTouch.DoubleTapDetection detection)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.doubleTapDetection = detection;
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x000E5E8D File Offset: 0x000E428D
		public static EasyTouch.DoubleTapDetection GetDoubleTapMethod()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.doubleTapDetection;
			}
			return EasyTouch.DoubleTapDetection.BySystem;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x000E5EAA File Offset: 0x000E42AA
		public static void SetSwipeTolerance(float tolerance)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.swipeTolerance = tolerance;
			}
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x000E5EC6 File Offset: 0x000E42C6
		public static float GetSwipeTolerance()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.swipeTolerance;
			}
			return -1f;
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x000E5EE7 File Offset: 0x000E42E7
		public static void SetEnable2FingersGesture(bool enable)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.enable2FingersGesture = enable;
			}
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x000E5F03 File Offset: 0x000E4303
		public static bool GetEnable2FingersGesture()
		{
			return EasyTouch.instance && EasyTouch.instance.enable2FingersGesture;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x000E5F20 File Offset: 0x000E4320
		public static void SetTwoFingerPickMethod(EasyTouch.TwoFingerPickMethod pickMethod)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.twoFingerPickMethod = pickMethod;
			}
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x000E5F3C File Offset: 0x000E433C
		public static EasyTouch.TwoFingerPickMethod GetTwoFingerPickMethod()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.twoFingerPickMethod;
			}
			return EasyTouch.TwoFingerPickMethod.Finger;
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x000E5F59 File Offset: 0x000E4359
		public static void SetEnablePinch(bool enable)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.enablePinch = enable;
			}
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x000E5F75 File Offset: 0x000E4375
		public static bool GetEnablePinch()
		{
			return EasyTouch.instance && EasyTouch.instance.enablePinch;
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x000E5F92 File Offset: 0x000E4392
		public static void SetMinPinchLength(float length)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.minPinchLength = length;
			}
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x000E5FAE File Offset: 0x000E43AE
		public static float GetMinPinchLength()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.minPinchLength;
			}
			return -1f;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x000E5FCF File Offset: 0x000E43CF
		public static void SetEnableTwist(bool enable)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.enableTwist = enable;
			}
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x000E5FEB File Offset: 0x000E43EB
		public static bool GetEnableTwist()
		{
			return EasyTouch.instance && EasyTouch.instance.enableTwist;
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x000E6008 File Offset: 0x000E4408
		public static void SetMinTwistAngle(float angle)
		{
			if (EasyTouch.instance)
			{
				EasyTouch.instance.minTwistAngle = angle;
			}
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x000E6024 File Offset: 0x000E4424
		public static float GetMinTwistAngle()
		{
			if (EasyTouch.instance)
			{
				return EasyTouch.instance.minTwistAngle;
			}
			return -1f;
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x000E6045 File Offset: 0x000E4445
		public static bool GetSecondeFingerSimulation()
		{
			return EasyTouch.instance != null && EasyTouch.instance.enableSimulation;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x000E6063 File Offset: 0x000E4463
		public static void SetSecondFingerSimulation(bool value)
		{
			if (EasyTouch.instance != null)
			{
				EasyTouch.instance.enableSimulation = value;
			}
		}

		// Token: 0x04001E72 RID: 7794
		private static EasyTouch _instance;

		// Token: 0x04001E73 RID: 7795
		private Gesture _currentGesture = new Gesture();

		// Token: 0x04001E74 RID: 7796
		private List<Gesture> _currentGestures = new List<Gesture>();

		// Token: 0x04001E75 RID: 7797
		public bool enable;

		// Token: 0x04001E76 RID: 7798
		public bool enableRemote;

		// Token: 0x04001E77 RID: 7799
		public EasyTouch.GesturePriority gesturePriority;

		// Token: 0x04001E78 RID: 7800
		public float StationaryTolerance;

		// Token: 0x04001E79 RID: 7801
		public float longTapTime;

		// Token: 0x04001E7A RID: 7802
		public float swipeTolerance;

		// Token: 0x04001E7B RID: 7803
		public float minPinchLength;

		// Token: 0x04001E7C RID: 7804
		public float minTwistAngle;

		// Token: 0x04001E7D RID: 7805
		public EasyTouch.DoubleTapDetection doubleTapDetection;

		// Token: 0x04001E7E RID: 7806
		public float doubleTapTime;

		// Token: 0x04001E7F RID: 7807
		public bool alwaysSendSwipe;

		// Token: 0x04001E80 RID: 7808
		public bool enable2FingersGesture;

		// Token: 0x04001E81 RID: 7809
		public bool enableTwist;

		// Token: 0x04001E82 RID: 7810
		public bool enablePinch;

		// Token: 0x04001E83 RID: 7811
		public bool enable2FingersSwipe;

		// Token: 0x04001E84 RID: 7812
		public EasyTouch.TwoFingerPickMethod twoFingerPickMethod;

		// Token: 0x04001E85 RID: 7813
		public List<ECamera> touchCameras;

		// Token: 0x04001E86 RID: 7814
		public bool autoSelect;

		// Token: 0x04001E87 RID: 7815
		public LayerMask pickableLayers3D;

		// Token: 0x04001E88 RID: 7816
		public bool enable2D;

		// Token: 0x04001E89 RID: 7817
		public LayerMask pickableLayers2D;

		// Token: 0x04001E8A RID: 7818
		public bool autoUpdatePickedObject;

		// Token: 0x04001E8B RID: 7819
		public bool allowUIDetection;

		// Token: 0x04001E8C RID: 7820
		public bool enableUIMode;

		// Token: 0x04001E8D RID: 7821
		public bool autoUpdatePickedUI;

		// Token: 0x04001E8E RID: 7822
		public bool enabledNGuiMode;

		// Token: 0x04001E8F RID: 7823
		public LayerMask nGUILayers;

		// Token: 0x04001E90 RID: 7824
		public List<Camera> nGUICameras;

		// Token: 0x04001E91 RID: 7825
		public bool enableSimulation;

		// Token: 0x04001E92 RID: 7826
		public KeyCode twistKey;

		// Token: 0x04001E93 RID: 7827
		public KeyCode swipeKey;

		// Token: 0x04001E94 RID: 7828
		public bool showGuiInspector;

		// Token: 0x04001E95 RID: 7829
		public bool showSelectInspector;

		// Token: 0x04001E96 RID: 7830
		public bool showGestureInspector;

		// Token: 0x04001E97 RID: 7831
		public bool showTwoFingerInspector;

		// Token: 0x04001E98 RID: 7832
		public bool showSecondFingerInspector;

		// Token: 0x04001E99 RID: 7833
		private EasyTouchInput input = new EasyTouchInput();

		// Token: 0x04001E9A RID: 7834
		private Finger[] fingers = new Finger[100];

		// Token: 0x04001E9B RID: 7835
		public Texture secondFingerTexture;

		// Token: 0x04001E9C RID: 7836
		private TwoFingerGesture twoFinger = new TwoFingerGesture();

		// Token: 0x04001E9D RID: 7837
		private int oldTouchCount;

		// Token: 0x04001E9E RID: 7838
		private EasyTouch.DoubleTap[] singleDoubleTap = new EasyTouch.DoubleTap[100];

		// Token: 0x04001E9F RID: 7839
		private Finger[] tmpArray = new Finger[100];

		// Token: 0x04001EA0 RID: 7840
		private EasyTouch.PickedObject pickedObject = new EasyTouch.PickedObject();

		// Token: 0x04001EA1 RID: 7841
		private List<RaycastResult> uiRaycastResultCache = new List<RaycastResult>();

		// Token: 0x04001EA2 RID: 7842
		private PointerEventData uiPointerEventData;

		// Token: 0x04001EA3 RID: 7843
		private EventSystem uiEventSystem;

		// Token: 0x020003B2 RID: 946
		[Serializable]
		private class DoubleTap
		{
			// Token: 0x06001D4E RID: 7502 RVA: 0x000E609A File Offset: 0x000E449A
			public void Stop()
			{
				this.inDoubleTap = false;
				this.inWait = false;
				this.time = 0f;
				this.count = 0;
			}

			// Token: 0x04001EA5 RID: 7845
			public bool inDoubleTap;

			// Token: 0x04001EA6 RID: 7846
			public bool inWait;

			// Token: 0x04001EA7 RID: 7847
			public float time;

			// Token: 0x04001EA8 RID: 7848
			public int count;

			// Token: 0x04001EA9 RID: 7849
			public Finger finger;
		}

		// Token: 0x020003B3 RID: 947
		private class PickedObject
		{
			// Token: 0x04001EAA RID: 7850
			public GameObject pickedObj;

			// Token: 0x04001EAB RID: 7851
			public Camera pickedCamera;

			// Token: 0x04001EAC RID: 7852
			public bool isGUI;
		}

		// Token: 0x020003B4 RID: 948
		// (Invoke) Token: 0x06001D51 RID: 7505
		public delegate void TouchCancelHandler(Gesture gesture);

		// Token: 0x020003B5 RID: 949
		// (Invoke) Token: 0x06001D55 RID: 7509
		public delegate void Cancel2FingersHandler(Gesture gesture);

		// Token: 0x020003B6 RID: 950
		// (Invoke) Token: 0x06001D59 RID: 7513
		public delegate void TouchStartHandler(Gesture gesture);

		// Token: 0x020003B7 RID: 951
		// (Invoke) Token: 0x06001D5D RID: 7517
		public delegate void TouchDownHandler(Gesture gesture);

		// Token: 0x020003B8 RID: 952
		// (Invoke) Token: 0x06001D61 RID: 7521
		public delegate void TouchUpHandler(Gesture gesture);

		// Token: 0x020003B9 RID: 953
		// (Invoke) Token: 0x06001D65 RID: 7525
		public delegate void SimpleTapHandler(Gesture gesture);

		// Token: 0x020003BA RID: 954
		// (Invoke) Token: 0x06001D69 RID: 7529
		public delegate void DoubleTapHandler(Gesture gesture);

		// Token: 0x020003BB RID: 955
		// (Invoke) Token: 0x06001D6D RID: 7533
		public delegate void LongTapStartHandler(Gesture gesture);

		// Token: 0x020003BC RID: 956
		// (Invoke) Token: 0x06001D71 RID: 7537
		public delegate void LongTapHandler(Gesture gesture);

		// Token: 0x020003BD RID: 957
		// (Invoke) Token: 0x06001D75 RID: 7541
		public delegate void LongTapEndHandler(Gesture gesture);

		// Token: 0x020003BE RID: 958
		// (Invoke) Token: 0x06001D79 RID: 7545
		public delegate void DragStartHandler(Gesture gesture);

		// Token: 0x020003BF RID: 959
		// (Invoke) Token: 0x06001D7D RID: 7549
		public delegate void DragHandler(Gesture gesture);

		// Token: 0x020003C0 RID: 960
		// (Invoke) Token: 0x06001D81 RID: 7553
		public delegate void DragEndHandler(Gesture gesture);

		// Token: 0x020003C1 RID: 961
		// (Invoke) Token: 0x06001D85 RID: 7557
		public delegate void SwipeStartHandler(Gesture gesture);

		// Token: 0x020003C2 RID: 962
		// (Invoke) Token: 0x06001D89 RID: 7561
		public delegate void SwipeHandler(Gesture gesture);

		// Token: 0x020003C3 RID: 963
		// (Invoke) Token: 0x06001D8D RID: 7565
		public delegate void SwipeEndHandler(Gesture gesture);

		// Token: 0x020003C4 RID: 964
		// (Invoke) Token: 0x06001D91 RID: 7569
		public delegate void TouchStart2FingersHandler(Gesture gesture);

		// Token: 0x020003C5 RID: 965
		// (Invoke) Token: 0x06001D95 RID: 7573
		public delegate void TouchDown2FingersHandler(Gesture gesture);

		// Token: 0x020003C6 RID: 966
		// (Invoke) Token: 0x06001D99 RID: 7577
		public delegate void TouchUp2FingersHandler(Gesture gesture);

		// Token: 0x020003C7 RID: 967
		// (Invoke) Token: 0x06001D9D RID: 7581
		public delegate void SimpleTap2FingersHandler(Gesture gesture);

		// Token: 0x020003C8 RID: 968
		// (Invoke) Token: 0x06001DA1 RID: 7585
		public delegate void DoubleTap2FingersHandler(Gesture gesture);

		// Token: 0x020003C9 RID: 969
		// (Invoke) Token: 0x06001DA5 RID: 7589
		public delegate void LongTapStart2FingersHandler(Gesture gesture);

		// Token: 0x020003CA RID: 970
		// (Invoke) Token: 0x06001DA9 RID: 7593
		public delegate void LongTap2FingersHandler(Gesture gesture);

		// Token: 0x020003CB RID: 971
		// (Invoke) Token: 0x06001DAD RID: 7597
		public delegate void LongTapEnd2FingersHandler(Gesture gesture);

		// Token: 0x020003CC RID: 972
		// (Invoke) Token: 0x06001DB1 RID: 7601
		public delegate void TwistHandler(Gesture gesture);

		// Token: 0x020003CD RID: 973
		// (Invoke) Token: 0x06001DB5 RID: 7605
		public delegate void TwistEndHandler(Gesture gesture);

		// Token: 0x020003CE RID: 974
		// (Invoke) Token: 0x06001DB9 RID: 7609
		public delegate void PinchInHandler(Gesture gesture);

		// Token: 0x020003CF RID: 975
		// (Invoke) Token: 0x06001DBD RID: 7613
		public delegate void PinchOutHandler(Gesture gesture);

		// Token: 0x020003D0 RID: 976
		// (Invoke) Token: 0x06001DC1 RID: 7617
		public delegate void PinchEndHandler(Gesture gesture);

		// Token: 0x020003D1 RID: 977
		// (Invoke) Token: 0x06001DC5 RID: 7621
		public delegate void PinchHandler(Gesture gesture);

		// Token: 0x020003D2 RID: 978
		// (Invoke) Token: 0x06001DC9 RID: 7625
		public delegate void DragStart2FingersHandler(Gesture gesture);

		// Token: 0x020003D3 RID: 979
		// (Invoke) Token: 0x06001DCD RID: 7629
		public delegate void Drag2FingersHandler(Gesture gesture);

		// Token: 0x020003D4 RID: 980
		// (Invoke) Token: 0x06001DD1 RID: 7633
		public delegate void DragEnd2FingersHandler(Gesture gesture);

		// Token: 0x020003D5 RID: 981
		// (Invoke) Token: 0x06001DD5 RID: 7637
		public delegate void SwipeStart2FingersHandler(Gesture gesture);

		// Token: 0x020003D6 RID: 982
		// (Invoke) Token: 0x06001DD9 RID: 7641
		public delegate void Swipe2FingersHandler(Gesture gesture);

		// Token: 0x020003D7 RID: 983
		// (Invoke) Token: 0x06001DDD RID: 7645
		public delegate void SwipeEnd2FingersHandler(Gesture gesture);

		// Token: 0x020003D8 RID: 984
		// (Invoke) Token: 0x06001DE1 RID: 7649
		public delegate void EasyTouchIsReadyHandler();

		// Token: 0x020003D9 RID: 985
		// (Invoke) Token: 0x06001DE5 RID: 7653
		public delegate void OverUIElementHandler(Gesture gesture);

		// Token: 0x020003DA RID: 986
		// (Invoke) Token: 0x06001DE9 RID: 7657
		public delegate void UIElementTouchUpHandler(Gesture gesture);

		// Token: 0x020003DB RID: 987
		public enum GesturePriority
		{
			// Token: 0x04001EAE RID: 7854
			Tap,
			// Token: 0x04001EAF RID: 7855
			Slips
		}

		// Token: 0x020003DC RID: 988
		public enum DoubleTapDetection
		{
			// Token: 0x04001EB1 RID: 7857
			BySystem,
			// Token: 0x04001EB2 RID: 7858
			ByTime
		}

		// Token: 0x020003DD RID: 989
		public enum GestureType
		{
			// Token: 0x04001EB4 RID: 7860
			Tap,
			// Token: 0x04001EB5 RID: 7861
			Drag,
			// Token: 0x04001EB6 RID: 7862
			Swipe,
			// Token: 0x04001EB7 RID: 7863
			None,
			// Token: 0x04001EB8 RID: 7864
			LongTap,
			// Token: 0x04001EB9 RID: 7865
			Pinch,
			// Token: 0x04001EBA RID: 7866
			Twist,
			// Token: 0x04001EBB RID: 7867
			Cancel,
			// Token: 0x04001EBC RID: 7868
			Acquisition
		}

		// Token: 0x020003DE RID: 990
		public enum SwipeDirection
		{
			// Token: 0x04001EBE RID: 7870
			None,
			// Token: 0x04001EBF RID: 7871
			Left,
			// Token: 0x04001EC0 RID: 7872
			Right,
			// Token: 0x04001EC1 RID: 7873
			Up,
			// Token: 0x04001EC2 RID: 7874
			Down,
			// Token: 0x04001EC3 RID: 7875
			UpLeft,
			// Token: 0x04001EC4 RID: 7876
			UpRight,
			// Token: 0x04001EC5 RID: 7877
			DownLeft,
			// Token: 0x04001EC6 RID: 7878
			DownRight,
			// Token: 0x04001EC7 RID: 7879
			Other,
			// Token: 0x04001EC8 RID: 7880
			All
		}

		// Token: 0x020003DF RID: 991
		public enum TwoFingerPickMethod
		{
			// Token: 0x04001ECA RID: 7882
			Finger,
			// Token: 0x04001ECB RID: 7883
			Average
		}

		// Token: 0x020003E0 RID: 992
		public enum EvtType
		{
			// Token: 0x04001ECD RID: 7885
			None,
			// Token: 0x04001ECE RID: 7886
			On_TouchStart,
			// Token: 0x04001ECF RID: 7887
			On_TouchDown,
			// Token: 0x04001ED0 RID: 7888
			On_TouchUp,
			// Token: 0x04001ED1 RID: 7889
			On_SimpleTap,
			// Token: 0x04001ED2 RID: 7890
			On_DoubleTap,
			// Token: 0x04001ED3 RID: 7891
			On_LongTapStart,
			// Token: 0x04001ED4 RID: 7892
			On_LongTap,
			// Token: 0x04001ED5 RID: 7893
			On_LongTapEnd,
			// Token: 0x04001ED6 RID: 7894
			On_DragStart,
			// Token: 0x04001ED7 RID: 7895
			On_Drag,
			// Token: 0x04001ED8 RID: 7896
			On_DragEnd,
			// Token: 0x04001ED9 RID: 7897
			On_SwipeStart,
			// Token: 0x04001EDA RID: 7898
			On_Swipe,
			// Token: 0x04001EDB RID: 7899
			On_SwipeEnd,
			// Token: 0x04001EDC RID: 7900
			On_TouchStart2Fingers,
			// Token: 0x04001EDD RID: 7901
			On_TouchDown2Fingers,
			// Token: 0x04001EDE RID: 7902
			On_TouchUp2Fingers,
			// Token: 0x04001EDF RID: 7903
			On_SimpleTap2Fingers,
			// Token: 0x04001EE0 RID: 7904
			On_DoubleTap2Fingers,
			// Token: 0x04001EE1 RID: 7905
			On_LongTapStart2Fingers,
			// Token: 0x04001EE2 RID: 7906
			On_LongTap2Fingers,
			// Token: 0x04001EE3 RID: 7907
			On_LongTapEnd2Fingers,
			// Token: 0x04001EE4 RID: 7908
			On_Twist,
			// Token: 0x04001EE5 RID: 7909
			On_TwistEnd,
			// Token: 0x04001EE6 RID: 7910
			On_Pinch,
			// Token: 0x04001EE7 RID: 7911
			On_PinchIn,
			// Token: 0x04001EE8 RID: 7912
			On_PinchOut,
			// Token: 0x04001EE9 RID: 7913
			On_PinchEnd,
			// Token: 0x04001EEA RID: 7914
			On_DragStart2Fingers,
			// Token: 0x04001EEB RID: 7915
			On_Drag2Fingers,
			// Token: 0x04001EEC RID: 7916
			On_DragEnd2Fingers,
			// Token: 0x04001EED RID: 7917
			On_SwipeStart2Fingers,
			// Token: 0x04001EEE RID: 7918
			On_Swipe2Fingers,
			// Token: 0x04001EEF RID: 7919
			On_SwipeEnd2Fingers,
			// Token: 0x04001EF0 RID: 7920
			On_EasyTouchIsReady,
			// Token: 0x04001EF1 RID: 7921
			On_Cancel,
			// Token: 0x04001EF2 RID: 7922
			On_Cancel2Fingers,
			// Token: 0x04001EF3 RID: 7923
			On_OverUIElement,
			// Token: 0x04001EF4 RID: 7924
			On_UIElementTouchUp
		}
	}
}
