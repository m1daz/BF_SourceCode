using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020006AE RID: 1710
public abstract class VCAnalogJoystickBase : VCCollideableObject
{
	// Token: 0x06003251 RID: 12881 RVA: 0x00163F10 File Offset: 0x00162310
	protected void AddInstance()
	{
		if (string.IsNullOrEmpty(this.vcName))
		{
			return;
		}
		if (VCAnalogJoystickBase._instancesByVcName == null)
		{
			VCAnalogJoystickBase._instancesByVcName = new Dictionary<string, VCAnalogJoystickBase>();
		}
		while (VCAnalogJoystickBase._instancesByVcName.ContainsKey(this.vcName))
		{
			if (VCAnalogJoystickBase._instancesByVcName[this.vcName] == null)
			{
				VCAnalogJoystickBase._instancesByVcName.Remove(this.vcName);
			}
			else
			{
				this.vcName += "_copy";
				Debug.LogWarning("Attempting to add instance with duplicate VCName!\nVCNames must be unique -- renaming this instance to " + this.vcName);
			}
		}
		VCAnalogJoystickBase._instancesByVcName.Add(this.vcName, this);
	}

	// Token: 0x06003252 RID: 12882 RVA: 0x00163FC9 File Offset: 0x001623C9
	public static VCAnalogJoystickBase GetInstance(string vcName)
	{
		if (VCAnalogJoystickBase._instancesByVcName == null || !VCAnalogJoystickBase._instancesByVcName.ContainsKey(vcName))
		{
			return null;
		}
		return VCAnalogJoystickBase._instancesByVcName[vcName];
	}

	// Token: 0x06003253 RID: 12883
	protected abstract void InitOriginValues();

	// Token: 0x06003254 RID: 12884
	protected abstract bool Colliding(VCTouchWrapper tw);

	// Token: 0x06003255 RID: 12885
	protected abstract void ProcessTouch(VCTouchWrapper tw);

	// Token: 0x06003256 RID: 12886
	protected abstract void SetVisible(bool visible, bool forceUpdate);

	// Token: 0x06003257 RID: 12887 RVA: 0x00163FF2 File Offset: 0x001623F2
	protected void Start()
	{
		this.Init();
	}

	// Token: 0x06003258 RID: 12888 RVA: 0x00163FFC File Offset: 0x001623FC
	protected virtual bool Init()
	{
		base.useGUILayout = false;
		if (VCTouchController.Instance == null)
		{
			Debug.LogWarning("Cannot find VCTouchController!\nVirtualControls requires a gameObject which has VCTouchController component attached in scene. Adding one for you...");
			VCUtils.AddTouchController(base.gameObject);
		}
		if (!this.movingPart)
		{
			VCUtils.DestroyWithError(base.gameObject, "movingPart is null, VCAnalogJoystick requires it to be assigned to a gameObject! Destroying this control.");
			return false;
		}
		if (this.RangeX <= 0f)
		{
			VCUtils.DestroyWithError(base.gameObject, "rangeMin must be less than rangeMax!  Destroying this control.");
			return false;
		}
		if (this.RangeY <= 0f)
		{
			VCUtils.DestroyWithError(base.gameObject, "rangeMin must be less than rangeMax!  Destroying this control.");
			return false;
		}
		if (this.basePart == null)
		{
			this.basePart = base.gameObject;
		}
		if (this.colliderObject == null)
		{
			this.colliderObject = this.movingPart;
		}
		this._deltaPixels = default(Vector2);
		this._dragDeltaMagnitudeMaxSq = this.dragDeltaMagnitudeMaxPixels * this.dragDeltaMagnitudeMaxPixels;
		base.InitCollider(this.colliderObject);
		this.InitOriginValues();
		this.TapCount = 0;
		this.AddInstance();
		return true;
	}

	// Token: 0x06003259 RID: 12889 RVA: 0x00164117 File Offset: 0x00162517
	protected virtual void SetPosition(GameObject go, Vector3 vec)
	{
		go.transform.position = vec;
	}

	// Token: 0x0600325A RID: 12890 RVA: 0x00164125 File Offset: 0x00162525
	protected virtual void LateUpdate()
	{
		if (this.useLateUpdate)
		{
			this.PerformUpdate();
		}
	}

	// Token: 0x0600325B RID: 12891 RVA: 0x00164138 File Offset: 0x00162538
	protected virtual void Update()
	{
		if (!this.useLateUpdate)
		{
			this.PerformUpdate();
		}
	}

	// Token: 0x0600325C RID: 12892 RVA: 0x0016414C File Offset: 0x0016254C
	protected void PerformUpdate()
	{
		if (Time.time - this._tapTime >= this.tapCountResetTime)
		{
			this.TapCount = 0;
		}
		if (this.Dragging)
		{
			this.UpdateDelta();
			Vector3 vec = new Vector3(this._movingPartOrigin.x + this._movingPartOffset.x, this._movingPartOrigin.y + this._movingPartOffset.y, this.movingPart.transform.position.z);
			this.SetPosition(this.movingPart, vec);
			if (this.stopDraggingOnMoveOut && !this.Colliding(this._touch))
			{
				this.StopDragging();
				return;
			}
		}
		else
		{
			if (this._wasDragging)
			{
				this.StopDragging();
			}
			for (int i = 0; i < VCTouchController.Instance.touches.Count; i++)
			{
				VCTouchWrapper vctouchWrapper = VCTouchController.Instance.touches[i];
				if (vctouchWrapper.phase == TouchPhase.Began)
				{
					if (this.anyTouchActivatesControl && this.SetTouch(vctouchWrapper))
					{
						break;
					}
					if (this.positionAtTouchLocation)
					{
						Vector2 vector = new Vector2(vctouchWrapper.position.x / (float)Screen.width, vctouchWrapper.position.y / (float)Screen.height);
						if (vector.x < this.positionAtTouchLocationAreaMin.x || vector.x > this.positionAtTouchLocationAreaMax.x)
						{
							goto IL_1D0;
						}
						if (vector.y < this.positionAtTouchLocationAreaMin.y || vector.y > this.positionAtTouchLocationAreaMax.y)
						{
							goto IL_1D0;
						}
						if (this.SetTouch(vctouchWrapper))
						{
							break;
						}
					}
					if (this.Colliding(vctouchWrapper) && this.SetTouch(vctouchWrapper))
					{
						break;
					}
				}
				IL_1D0:;
			}
			if (this._touch != null)
			{
				this.ProcessTouch(this._touch);
			}
		}
		this.SetVisible(this.visibleWhenNotActive || this.Dragging, this._movingPartVisible == this.hideMovingPart);
	}

	// Token: 0x0600325D RID: 12893 RVA: 0x00164384 File Offset: 0x00162784
	protected virtual void UpdateDelta()
	{
		this._deltaPixels.x = (this._touch.position.x - this._touchOriginScreen.x) * this.dragScaleFactor.x;
		this._deltaPixels.y = (this._touch.position.y - this._touchOriginScreen.y) * this.dragScaleFactor.y;
		if (this._deltaPixels.sqrMagnitude > this._dragDeltaMagnitudeMaxSq)
		{
			this._deltaPixels = this._deltaPixels.normalized * this.dragDeltaMagnitudeMaxPixels;
		}
		this._movingPartOffset.x = this._deltaPixels.x;
		this._movingPartOffset.y = this._deltaPixels.y;
	}

	// Token: 0x0600325E RID: 12894 RVA: 0x00164458 File Offset: 0x00162858
	protected bool SetTouch(VCTouchWrapper tw)
	{
		if (this.requireExclusiveTouch && VCAnalogJoystickBase.touchesInUse.Any((VCTouchWrapper x) => x.fingerId == tw.fingerId))
		{
			return false;
		}
		this._touch = tw;
		this.TapCount++;
		if (this.TapCount == 1)
		{
			this._tapTime = Time.time;
		}
		else if (this.TapCount > 0 && this.TapCount % 2 == 0 && this.OnDoubleTap != null)
		{
			this.OnDoubleTap(this);
		}
		VCAnalogJoystickBase.touchesInUse.Add(tw);
		this._wasDragging = true;
		return true;
	}

	// Token: 0x0600325F RID: 12895 RVA: 0x00164518 File Offset: 0x00162918
	protected void StopDragging()
	{
		this.SetPosition(this.movingPart, this._movingPartOrigin);
		this._deltaPixels = Vector2.zero;
		this._wasDragging = false;
		VCAnalogJoystickBase.touchesInUse.Remove(this._touch);
		this._touch = null;
	}

	// Token: 0x06003260 RID: 12896 RVA: 0x00164558 File Offset: 0x00162958
	protected float RangeAdjust(float val, float min, float max)
	{
		float num = max - min;
		float num2 = Mathf.Abs(val);
		if (num2 < min)
		{
			return 0f;
		}
		if (num2 > max)
		{
			return 1f * VCUtils.GetSign(val);
		}
		return (num2 - min) / num * VCUtils.GetSign(val);
	}

	// Token: 0x1700046C RID: 1132
	// (get) Token: 0x06003261 RID: 12897 RVA: 0x0016459D File Offset: 0x0016299D
	public float AxisX
	{
		get
		{
			return this.RangeAdjust(this._deltaPixels.x / this.dragDeltaMagnitudeMaxPixels, this.rangeMin.x, this.rangeMax.x);
		}
	}

	// Token: 0x1700046D RID: 1133
	// (get) Token: 0x06003262 RID: 12898 RVA: 0x001645CD File Offset: 0x001629CD
	public float AxisY
	{
		get
		{
			return this.RangeAdjust(this._deltaPixels.y / this.dragDeltaMagnitudeMaxPixels, this.rangeMin.y, this.rangeMax.y);
		}
	}

	// Token: 0x1700046E RID: 1134
	// (get) Token: 0x06003263 RID: 12899 RVA: 0x001645FD File Offset: 0x001629FD
	public float AxisXRaw
	{
		get
		{
			return this._deltaPixels.x / this.dragDeltaMagnitudeMaxPixels;
		}
	}

	// Token: 0x1700046F RID: 1135
	// (get) Token: 0x06003264 RID: 12900 RVA: 0x00164611 File Offset: 0x00162A11
	public float AxisYRaw
	{
		get
		{
			return this._deltaPixels.y / this.dragDeltaMagnitudeMaxPixels;
		}
	}

	// Token: 0x17000470 RID: 1136
	// (get) Token: 0x06003265 RID: 12901 RVA: 0x00164625 File Offset: 0x00162A25
	public float MagnitudeSqr
	{
		get
		{
			return this.AxisX * this.AxisX + this.AxisY * this.AxisY;
		}
	}

	// Token: 0x17000471 RID: 1137
	// (get) Token: 0x06003266 RID: 12902 RVA: 0x00164642 File Offset: 0x00162A42
	public float AngleRadians
	{
		get
		{
			return Mathf.Atan2(this.AxisY, this.AxisX);
		}
	}

	// Token: 0x17000472 RID: 1138
	// (get) Token: 0x06003267 RID: 12903 RVA: 0x00164655 File Offset: 0x00162A55
	public float AngleDegrees
	{
		get
		{
			return 57.295776f * Mathf.Atan2(this.AxisY, this.AxisX);
		}
	}

	// Token: 0x17000473 RID: 1139
	// (get) Token: 0x06003268 RID: 12904 RVA: 0x0016466E File Offset: 0x00162A6E
	public bool Dragging
	{
		get
		{
			return this._touch != null && this._touch.Active;
		}
	}

	// Token: 0x17000474 RID: 1140
	// (get) Token: 0x06003269 RID: 12905 RVA: 0x00164689 File Offset: 0x00162A89
	// (set) Token: 0x0600326A RID: 12906 RVA: 0x00164691 File Offset: 0x00162A91
	public int TapCount { get; private set; }

	// Token: 0x17000475 RID: 1141
	// (get) Token: 0x0600326B RID: 12907 RVA: 0x0016469A File Offset: 0x00162A9A
	public VCTouchWrapper TouchWrapper
	{
		get
		{
			return this._touch;
		}
	}

	// Token: 0x17000476 RID: 1142
	// (get) Token: 0x0600326C RID: 12908 RVA: 0x001646A2 File Offset: 0x00162AA2
	protected float RangeX
	{
		get
		{
			return this.rangeMax.x - this.rangeMin.x;
		}
	}

	// Token: 0x17000477 RID: 1143
	// (get) Token: 0x0600326D RID: 12909 RVA: 0x001646BB File Offset: 0x00162ABB
	protected float RangeY
	{
		get
		{
			return this.rangeMax.y - this.rangeMin.y;
		}
	}

	// Token: 0x04002ED1 RID: 11985
	public string vcName;

	// Token: 0x04002ED2 RID: 11986
	protected static Dictionary<string, VCAnalogJoystickBase> _instancesByVcName;

	// Token: 0x04002ED3 RID: 11987
	public static List<VCTouchWrapper> touchesInUse = new List<VCTouchWrapper>();

	// Token: 0x04002ED4 RID: 11988
	public GameObject movingPart;

	// Token: 0x04002ED5 RID: 11989
	public GameObject basePart;

	// Token: 0x04002ED6 RID: 11990
	public GameObject colliderObject;

	// Token: 0x04002ED7 RID: 11991
	public bool visibleWhenNotActive = true;

	// Token: 0x04002ED8 RID: 11992
	public bool positionAtTouchLocation;

	// Token: 0x04002ED9 RID: 11993
	public Vector2 positionAtTouchLocationAreaMin = new Vector2(0f, 0f);

	// Token: 0x04002EDA RID: 11994
	public Vector2 positionAtTouchLocationAreaMax = new Vector2(1f, 1f);

	// Token: 0x04002EDB RID: 11995
	public bool stopDraggingOnMoveOut;

	// Token: 0x04002EDC RID: 11996
	public bool anyTouchActivatesControl;

	// Token: 0x04002EDD RID: 11997
	public bool measureDeltaRelativeToCenter;

	// Token: 0x04002EDE RID: 11998
	public bool hideMovingPart;

	// Token: 0x04002EDF RID: 11999
	public bool useLateUpdate;

	// Token: 0x04002EE0 RID: 12000
	public bool requireExclusiveTouch;

	// Token: 0x04002EE1 RID: 12001
	public float dragDeltaMagnitudeMaxPixels = 50f;

	// Token: 0x04002EE2 RID: 12002
	public float tapCountResetTime = 0.2f;

	// Token: 0x04002EE3 RID: 12003
	public Vector2 dragScaleFactor = new Vector2(1f, 1f);

	// Token: 0x04002EE4 RID: 12004
	public Vector2 rangeMin = new Vector2(0f, 0f);

	// Token: 0x04002EE5 RID: 12005
	public Vector2 rangeMax = new Vector2(1f, 1f);

	// Token: 0x04002EE6 RID: 12006
	public bool debugKeysEnabled;

	// Token: 0x04002EE7 RID: 12007
	public float debugTouchMovementSpeedPixels = 100f;

	// Token: 0x04002EE8 RID: 12008
	public KeyCode debugTouchKey = KeyCode.LeftControl;

	// Token: 0x04002EE9 RID: 12009
	public bool debugTouchKeyTogglesTouch;

	// Token: 0x04002EEA RID: 12010
	public KeyCode debugLeftKey = KeyCode.LeftArrow;

	// Token: 0x04002EEB RID: 12011
	public KeyCode debugRightKey = KeyCode.RightArrow;

	// Token: 0x04002EEC RID: 12012
	public KeyCode debugUpKey = KeyCode.UpArrow;

	// Token: 0x04002EED RID: 12013
	public KeyCode debugDownKey = KeyCode.DownArrow;

	// Token: 0x04002EEE RID: 12014
	protected float _dragDeltaMagnitudeMaxSq;

	// Token: 0x04002EEF RID: 12015
	protected Vector3 _movingPartOrigin;

	// Token: 0x04002EF0 RID: 12016
	protected Vector3 _touchOrigin;

	// Token: 0x04002EF1 RID: 12017
	protected Vector2 _touchOriginScreen;

	// Token: 0x04002EF2 RID: 12018
	protected bool _wasDragging;

	// Token: 0x04002EF3 RID: 12019
	protected Vector2 _deltaPixels;

	// Token: 0x04002EF4 RID: 12020
	protected Vector2 _movingPartOffset;

	// Token: 0x04002EF5 RID: 12021
	protected bool _visible = true;

	// Token: 0x04002EF6 RID: 12022
	protected bool _movingPartVisible = true;

	// Token: 0x04002EF7 RID: 12023
	protected List<Behaviour> _visibleBehaviourComponents;

	// Token: 0x04002EF8 RID: 12024
	protected VCTouchWrapper _touch;

	// Token: 0x04002EF9 RID: 12025
	public VCAnalogJoystickBase.VCJoystickDelegate OnDoubleTap;

	// Token: 0x04002EFA RID: 12026
	private float _tapTime;

	// Token: 0x020006AF RID: 1711
	// (Invoke) Token: 0x06003270 RID: 12912
	public delegate void VCJoystickDelegate(VCAnalogJoystickBase joystick);
}
