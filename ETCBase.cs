using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020003FD RID: 1021
[Serializable]
public abstract class ETCBase : MonoBehaviour
{
	// Token: 0x17000162 RID: 354
	// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000E96D9 File Offset: 0x000E7AD9
	// (set) Token: 0x06001E73 RID: 7795 RVA: 0x000E96E1 File Offset: 0x000E7AE1
	public ETCBase.RectAnchor anchor
	{
		get
		{
			return this._anchor;
		}
		set
		{
			if (value != this._anchor)
			{
				this._anchor = value;
				this.SetAnchorPosition();
			}
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x06001E74 RID: 7796 RVA: 0x000E96FC File Offset: 0x000E7AFC
	// (set) Token: 0x06001E75 RID: 7797 RVA: 0x000E9704 File Offset: 0x000E7B04
	public Vector2 anchorOffet
	{
		get
		{
			return this._anchorOffet;
		}
		set
		{
			if (value != this._anchorOffet)
			{
				this._anchorOffet = value;
				this.SetAnchorPosition();
			}
		}
	}

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06001E76 RID: 7798 RVA: 0x000E9724 File Offset: 0x000E7B24
	// (set) Token: 0x06001E77 RID: 7799 RVA: 0x000E972C File Offset: 0x000E7B2C
	public bool visible
	{
		get
		{
			return this._visible;
		}
		set
		{
			if (value != this._visible)
			{
				this._visible = value;
				this.SetVisible(true);
			}
		}
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x06001E78 RID: 7800 RVA: 0x000E9748 File Offset: 0x000E7B48
	// (set) Token: 0x06001E79 RID: 7801 RVA: 0x000E9750 File Offset: 0x000E7B50
	public bool activated
	{
		get
		{
			return this._activated;
		}
		set
		{
			if (value != this._activated)
			{
				this._activated = value;
				this.SetActivated();
			}
		}
	}

	// Token: 0x06001E7A RID: 7802 RVA: 0x000E976C File Offset: 0x000E7B6C
	protected virtual void Awake()
	{
		this.cachedRectTransform = (base.transform as RectTransform);
		this.cachedRootCanvas = base.transform.parent.GetComponent<Canvas>();
		if (!this.allowSimulationStandalone)
		{
			this.enableKeySimulation = false;
		}
		this.visibleAtStart = this._visible;
		this.activatedAtStart = this._activated;
		if (!this.isUnregisterAtDisable)
		{
			ETCInput.instance.RegisterControl(this);
		}
	}

	// Token: 0x06001E7B RID: 7803 RVA: 0x000E97E0 File Offset: 0x000E7BE0
	public virtual void Start()
	{
		if (this.enableCamera && this.autoLinkTagCam)
		{
			this.cameraTransform = null;
			GameObject gameObject = GameObject.FindGameObjectWithTag(this.autoCamTag);
			if (gameObject)
			{
				this.cameraTransform = gameObject.transform;
			}
		}
	}

	// Token: 0x06001E7C RID: 7804 RVA: 0x000E982D File Offset: 0x000E7C2D
	public virtual void OnEnable()
	{
		if (this.isUnregisterAtDisable)
		{
			ETCInput.instance.RegisterControl(this);
		}
		this.visible = this.visibleAtStart;
		this.activated = this.activatedAtStart;
	}

	// Token: 0x06001E7D RID: 7805 RVA: 0x000E9860 File Offset: 0x000E7C60
	private void OnDisable()
	{
		if (ETCInput._instance && this.isUnregisterAtDisable)
		{
			ETCInput.instance.UnRegisterControl(this);
		}
		this.visibleAtStart = this._visible;
		this.activated = this._activated;
		this.visible = false;
		this.activated = false;
	}

	// Token: 0x06001E7E RID: 7806 RVA: 0x000E98B8 File Offset: 0x000E7CB8
	private void OnDestroy()
	{
		if (ETCInput._instance)
		{
			ETCInput.instance.UnRegisterControl(this);
		}
	}

	// Token: 0x06001E7F RID: 7807 RVA: 0x000E98D4 File Offset: 0x000E7CD4
	public virtual void Update()
	{
		if (!this.useFixedUpdate)
		{
			base.StartCoroutine("UpdateVirtualControl");
		}
	}

	// Token: 0x06001E80 RID: 7808 RVA: 0x000E98ED File Offset: 0x000E7CED
	public virtual void FixedUpdate()
	{
		if (this.useFixedUpdate)
		{
			base.StartCoroutine("FixedUpdateVirtualControl");
		}
	}

	// Token: 0x06001E81 RID: 7809 RVA: 0x000E9908 File Offset: 0x000E7D08
	public virtual void LateUpdate()
	{
		if (this.enableCamera)
		{
			if (this.autoLinkTagCam && this.cameraTransform == null)
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag(this.autoCamTag);
				if (gameObject)
				{
					this.cameraTransform = gameObject.transform;
				}
			}
			ETCBase.CameraMode cameraMode = this.cameraMode;
			if (cameraMode != ETCBase.CameraMode.Follow)
			{
				if (cameraMode == ETCBase.CameraMode.SmoothFollow)
				{
					this.CameraSmoothFollow();
				}
			}
			else
			{
				this.CameraFollow();
			}
		}
	}

	// Token: 0x06001E82 RID: 7810 RVA: 0x000E998E File Offset: 0x000E7D8E
	protected virtual void UpdateControlState()
	{
	}

	// Token: 0x06001E83 RID: 7811 RVA: 0x000E9990 File Offset: 0x000E7D90
	protected virtual void SetVisible(bool forceUnvisible = true)
	{
	}

	// Token: 0x06001E84 RID: 7812 RVA: 0x000E9992 File Offset: 0x000E7D92
	protected virtual void SetActivated()
	{
	}

	// Token: 0x06001E85 RID: 7813 RVA: 0x000E9994 File Offset: 0x000E7D94
	public void SetAnchorPosition()
	{
		switch (this._anchor)
		{
		case ETCBase.RectAnchor.BottomLeft:
			this.rectTransform().anchorMin = new Vector2(0f, 0f);
			this.rectTransform().anchorMax = new Vector2(0f, 0f);
			this.rectTransform().anchoredPosition = new Vector2(this.rectTransform().sizeDelta.x / 2f + this._anchorOffet.x, this.rectTransform().sizeDelta.y / 2f + this._anchorOffet.y);
			break;
		case ETCBase.RectAnchor.BottomCenter:
			this.rectTransform().anchorMin = new Vector2(0.5f, 0f);
			this.rectTransform().anchorMax = new Vector2(0.5f, 0f);
			this.rectTransform().anchoredPosition = new Vector2(this._anchorOffet.x, this.rectTransform().sizeDelta.y / 2f + this._anchorOffet.y);
			break;
		case ETCBase.RectAnchor.BottonRight:
			this.rectTransform().anchorMin = new Vector2(1f, 0f);
			this.rectTransform().anchorMax = new Vector2(1f, 0f);
			this.rectTransform().anchoredPosition = new Vector2(-this.rectTransform().sizeDelta.x / 2f - this._anchorOffet.x, this.rectTransform().sizeDelta.y / 2f + this._anchorOffet.y);
			break;
		case ETCBase.RectAnchor.CenterLeft:
			this.rectTransform().anchorMin = new Vector2(0f, 0.5f);
			this.rectTransform().anchorMax = new Vector2(0f, 0.5f);
			this.rectTransform().anchoredPosition = new Vector2(this.rectTransform().sizeDelta.x / 2f + this._anchorOffet.x, this._anchorOffet.y);
			break;
		case ETCBase.RectAnchor.Center:
			this.rectTransform().anchorMin = new Vector2(0.5f, 0.5f);
			this.rectTransform().anchorMax = new Vector2(0.5f, 0.5f);
			this.rectTransform().anchoredPosition = new Vector2(this._anchorOffet.x, this._anchorOffet.y);
			break;
		case ETCBase.RectAnchor.CenterRight:
			this.rectTransform().anchorMin = new Vector2(1f, 0.5f);
			this.rectTransform().anchorMax = new Vector2(1f, 0.5f);
			this.rectTransform().anchoredPosition = new Vector2(-this.rectTransform().sizeDelta.x / 2f - this._anchorOffet.x, this._anchorOffet.y);
			break;
		case ETCBase.RectAnchor.TopLeft:
			this.rectTransform().anchorMin = new Vector2(0f, 1f);
			this.rectTransform().anchorMax = new Vector2(0f, 1f);
			this.rectTransform().anchoredPosition = new Vector2(this.rectTransform().sizeDelta.x / 2f + this._anchorOffet.x, -this.rectTransform().sizeDelta.y / 2f - this._anchorOffet.y);
			break;
		case ETCBase.RectAnchor.TopCenter:
			this.rectTransform().anchorMin = new Vector2(0.5f, 1f);
			this.rectTransform().anchorMax = new Vector2(0.5f, 1f);
			this.rectTransform().anchoredPosition = new Vector2(this._anchorOffet.x, -this.rectTransform().sizeDelta.y / 2f - this._anchorOffet.y);
			break;
		case ETCBase.RectAnchor.TopRight:
			this.rectTransform().anchorMin = new Vector2(1f, 1f);
			this.rectTransform().anchorMax = new Vector2(1f, 1f);
			this.rectTransform().anchoredPosition = new Vector2(-this.rectTransform().sizeDelta.x / 2f - this._anchorOffet.x, -this.rectTransform().sizeDelta.y / 2f - this._anchorOffet.y);
			break;
		}
	}

	// Token: 0x06001E86 RID: 7814 RVA: 0x000E9E78 File Offset: 0x000E8278
	protected GameObject GetFirstUIElement(Vector2 position)
	{
		this.uiEventSystem = EventSystem.current;
		if (!(this.uiEventSystem != null))
		{
			return null;
		}
		this.uiPointerEventData = new PointerEventData(this.uiEventSystem);
		this.uiPointerEventData.position = position;
		this.uiEventSystem.RaycastAll(this.uiPointerEventData, this.uiRaycastResultCache);
		if (this.uiRaycastResultCache.Count > 0)
		{
			return this.uiRaycastResultCache[0].gameObject;
		}
		return null;
	}

	// Token: 0x06001E87 RID: 7815 RVA: 0x000E9F00 File Offset: 0x000E8300
	protected void CameraSmoothFollow()
	{
		if (!this.cameraTransform || !this.cameraLookAt)
		{
			return;
		}
		float y = this.cameraLookAt.eulerAngles.y;
		float b = this.cameraLookAt.position.y + this.followHeight;
		float num = this.cameraTransform.eulerAngles.y;
		float num2 = this.cameraTransform.position.y;
		num = Mathf.LerpAngle(num, y, this.followRotationDamping * Time.deltaTime);
		num2 = Mathf.Lerp(num2, b, this.followHeightDamping * Time.deltaTime);
		Quaternion rotation = Quaternion.Euler(0f, num, 0f);
		Vector3 vector = this.cameraLookAt.position;
		vector -= rotation * Vector3.forward * this.followDistance;
		vector = new Vector3(vector.x, num2, vector.z);
		RaycastHit raycastHit;
		if (this.enableWallDetection && Physics.Linecast(new Vector3(this.cameraLookAt.position.x, this.cameraLookAt.position.y + 1f, this.cameraLookAt.position.z), vector, out raycastHit))
		{
			vector = new Vector3(raycastHit.point.x, num2, raycastHit.point.z);
		}
		this.cameraTransform.position = vector;
		this.cameraTransform.LookAt(this.cameraLookAt);
	}

	// Token: 0x06001E88 RID: 7816 RVA: 0x000EA0B4 File Offset: 0x000E84B4
	protected void CameraFollow()
	{
		if (!this.cameraTransform || !this.cameraLookAt)
		{
			return;
		}
		Vector3 b = this.followOffset;
		this.cameraTransform.position = this.cameraLookAt.position + b;
		this.cameraTransform.LookAt(this.cameraLookAt.position);
	}

	// Token: 0x06001E89 RID: 7817 RVA: 0x000EA11C File Offset: 0x000E851C
	private IEnumerator UpdateVirtualControl()
	{
		this.DoActionBeforeEndOfFrame();
		yield return new WaitForEndOfFrame();
		this.UpdateControlState();
		yield break;
	}

	// Token: 0x06001E8A RID: 7818 RVA: 0x000EA138 File Offset: 0x000E8538
	private IEnumerator FixedUpdateVirtualControl()
	{
		this.DoActionBeforeEndOfFrame();
		yield return new WaitForFixedUpdate();
		this.UpdateControlState();
		yield break;
	}

	// Token: 0x06001E8B RID: 7819 RVA: 0x000EA153 File Offset: 0x000E8553
	protected virtual void DoActionBeforeEndOfFrame()
	{
	}

	// Token: 0x04001FB8 RID: 8120
	protected RectTransform cachedRectTransform;

	// Token: 0x04001FB9 RID: 8121
	protected Canvas cachedRootCanvas;

	// Token: 0x04001FBA RID: 8122
	public bool isUnregisterAtDisable;

	// Token: 0x04001FBB RID: 8123
	private bool visibleAtStart = true;

	// Token: 0x04001FBC RID: 8124
	private bool activatedAtStart = true;

	// Token: 0x04001FBD RID: 8125
	[SerializeField]
	protected ETCBase.RectAnchor _anchor;

	// Token: 0x04001FBE RID: 8126
	[SerializeField]
	protected Vector2 _anchorOffet;

	// Token: 0x04001FBF RID: 8127
	[SerializeField]
	protected bool _visible;

	// Token: 0x04001FC0 RID: 8128
	[SerializeField]
	protected bool _activated;

	// Token: 0x04001FC1 RID: 8129
	public bool enableCamera;

	// Token: 0x04001FC2 RID: 8130
	public ETCBase.CameraMode cameraMode;

	// Token: 0x04001FC3 RID: 8131
	public string camTargetTag = "Player";

	// Token: 0x04001FC4 RID: 8132
	public bool autoLinkTagCam = true;

	// Token: 0x04001FC5 RID: 8133
	public string autoCamTag = "MainCamera";

	// Token: 0x04001FC6 RID: 8134
	public Transform cameraTransform;

	// Token: 0x04001FC7 RID: 8135
	public ETCBase.CameraTargetMode cameraTargetMode;

	// Token: 0x04001FC8 RID: 8136
	public bool enableWallDetection;

	// Token: 0x04001FC9 RID: 8137
	public LayerMask wallLayer = 0;

	// Token: 0x04001FCA RID: 8138
	public Transform cameraLookAt;

	// Token: 0x04001FCB RID: 8139
	protected CharacterController cameraLookAtCC;

	// Token: 0x04001FCC RID: 8140
	public Vector3 followOffset = new Vector3(0f, 6f, -6f);

	// Token: 0x04001FCD RID: 8141
	public float followDistance = 10f;

	// Token: 0x04001FCE RID: 8142
	public float followHeight = 5f;

	// Token: 0x04001FCF RID: 8143
	public float followRotationDamping = 5f;

	// Token: 0x04001FD0 RID: 8144
	public float followHeightDamping = 5f;

	// Token: 0x04001FD1 RID: 8145
	public int pointId = -1;

	// Token: 0x04001FD2 RID: 8146
	public bool enableKeySimulation;

	// Token: 0x04001FD3 RID: 8147
	public bool allowSimulationStandalone;

	// Token: 0x04001FD4 RID: 8148
	public bool visibleOnStandalone = true;

	// Token: 0x04001FD5 RID: 8149
	public ETCBase.DPadAxis dPadAxisCount;

	// Token: 0x04001FD6 RID: 8150
	public bool useFixedUpdate;

	// Token: 0x04001FD7 RID: 8151
	private List<RaycastResult> uiRaycastResultCache = new List<RaycastResult>();

	// Token: 0x04001FD8 RID: 8152
	private PointerEventData uiPointerEventData;

	// Token: 0x04001FD9 RID: 8153
	private EventSystem uiEventSystem;

	// Token: 0x04001FDA RID: 8154
	public bool isOnDrag;

	// Token: 0x04001FDB RID: 8155
	public bool isSwipeIn;

	// Token: 0x04001FDC RID: 8156
	public bool isSwipeOut;

	// Token: 0x04001FDD RID: 8157
	public bool showPSInspector;

	// Token: 0x04001FDE RID: 8158
	public bool showSpriteInspector;

	// Token: 0x04001FDF RID: 8159
	public bool showEventInspector;

	// Token: 0x04001FE0 RID: 8160
	public bool showBehaviourInspector;

	// Token: 0x04001FE1 RID: 8161
	public bool showAxesInspector;

	// Token: 0x04001FE2 RID: 8162
	public bool showTouchEventInspector;

	// Token: 0x04001FE3 RID: 8163
	public bool showDownEventInspector;

	// Token: 0x04001FE4 RID: 8164
	public bool showPressEventInspector;

	// Token: 0x04001FE5 RID: 8165
	public bool showCameraInspector;

	// Token: 0x020003FE RID: 1022
	public enum ControlType
	{
		// Token: 0x04001FE7 RID: 8167
		Joystick,
		// Token: 0x04001FE8 RID: 8168
		TouchPad,
		// Token: 0x04001FE9 RID: 8169
		DPad,
		// Token: 0x04001FEA RID: 8170
		Button
	}

	// Token: 0x020003FF RID: 1023
	public enum RectAnchor
	{
		// Token: 0x04001FEC RID: 8172
		UserDefined,
		// Token: 0x04001FED RID: 8173
		BottomLeft,
		// Token: 0x04001FEE RID: 8174
		BottomCenter,
		// Token: 0x04001FEF RID: 8175
		BottonRight,
		// Token: 0x04001FF0 RID: 8176
		CenterLeft,
		// Token: 0x04001FF1 RID: 8177
		Center,
		// Token: 0x04001FF2 RID: 8178
		CenterRight,
		// Token: 0x04001FF3 RID: 8179
		TopLeft,
		// Token: 0x04001FF4 RID: 8180
		TopCenter,
		// Token: 0x04001FF5 RID: 8181
		TopRight
	}

	// Token: 0x02000400 RID: 1024
	public enum DPadAxis
	{
		// Token: 0x04001FF7 RID: 8183
		Two_Axis,
		// Token: 0x04001FF8 RID: 8184
		Four_Axis
	}

	// Token: 0x02000401 RID: 1025
	public enum CameraMode
	{
		// Token: 0x04001FFA RID: 8186
		Follow,
		// Token: 0x04001FFB RID: 8187
		SmoothFollow
	}

	// Token: 0x02000402 RID: 1026
	public enum CameraTargetMode
	{
		// Token: 0x04001FFD RID: 8189
		UserDefined,
		// Token: 0x04001FFE RID: 8190
		LinkOnTag,
		// Token: 0x04001FFF RID: 8191
		FromDirectActionAxisX,
		// Token: 0x04002000 RID: 8192
		FromDirectActionAxisY
	}
}
