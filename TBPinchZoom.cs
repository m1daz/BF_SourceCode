using System;
using UnityEngine;

// Token: 0x02000453 RID: 1107
[RequireComponent(typeof(Camera))]
[AddComponentMenu("FingerGestures/Toolbox/Misc/Pinch-Zoom")]
public class TBPinchZoom : MonoBehaviour
{
	// Token: 0x17000179 RID: 377
	// (get) Token: 0x0600202E RID: 8238 RVA: 0x000F1FF2 File Offset: 0x000F03F2
	// (set) Token: 0x0600202F RID: 8239 RVA: 0x000F1FFA File Offset: 0x000F03FA
	public Vector3 DefaultPos
	{
		get
		{
			return this.defaultPos;
		}
		set
		{
			this.defaultPos = value;
		}
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06002030 RID: 8240 RVA: 0x000F2003 File Offset: 0x000F0403
	// (set) Token: 0x06002031 RID: 8241 RVA: 0x000F200B File Offset: 0x000F040B
	public float DefaultFov
	{
		get
		{
			return this.defaultFov;
		}
		set
		{
			this.defaultFov = value;
		}
	}

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06002032 RID: 8242 RVA: 0x000F2014 File Offset: 0x000F0414
	// (set) Token: 0x06002033 RID: 8243 RVA: 0x000F201C File Offset: 0x000F041C
	public float DefaultOrthoSize
	{
		get
		{
			return this.defaultOrthoSize;
		}
		set
		{
			this.defaultOrthoSize = value;
		}
	}

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06002034 RID: 8244 RVA: 0x000F2025 File Offset: 0x000F0425
	// (set) Token: 0x06002035 RID: 8245 RVA: 0x000F2030 File Offset: 0x000F0430
	public float ZoomAmount
	{
		get
		{
			return this.zoomAmount;
		}
		set
		{
			this.zoomAmount = Mathf.Clamp(value, this.minZoomAmount, this.maxZoomAmount);
			TBPinchZoom.ZoomMethod zoomMethod = this.zoomMethod;
			if (zoomMethod != TBPinchZoom.ZoomMethod.Position)
			{
				if (zoomMethod == TBPinchZoom.ZoomMethod.FOV)
				{
					if (base.GetComponent<Camera>().orthographic)
					{
						base.GetComponent<Camera>().orthographicSize = Mathf.Max(this.defaultOrthoSize - this.zoomAmount, 0.1f);
					}
					else
					{
						base.GetComponent<Camera>().fov = Mathf.Max(this.defaultFov - this.zoomAmount, 0.1f);
					}
				}
			}
			else
			{
				base.transform.position = this.defaultPos + this.zoomAmount * base.transform.forward;
			}
		}
	}

	// Token: 0x06002036 RID: 8246 RVA: 0x000F20FD File Offset: 0x000F04FD
	private void Start()
	{
		this.SetDefaults();
	}

	// Token: 0x06002037 RID: 8247 RVA: 0x000F2105 File Offset: 0x000F0505
	public void SetDefaults()
	{
		this.DefaultPos = base.transform.position;
		this.DefaultFov = base.GetComponent<Camera>().fov;
		this.DefaultOrthoSize = base.GetComponent<Camera>().orthographicSize;
	}

	// Token: 0x06002038 RID: 8248 RVA: 0x000F213A File Offset: 0x000F053A
	private void OnEnable()
	{
		FingerGestures.OnPinchMove += this.FingerGestures_OnPinchMove;
	}

	// Token: 0x06002039 RID: 8249 RVA: 0x000F214D File Offset: 0x000F054D
	private void OnDisable()
	{
		FingerGestures.OnPinchMove -= this.FingerGestures_OnPinchMove;
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x000F2160 File Offset: 0x000F0560
	private void FingerGestures_OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
	{
		this.ZoomAmount += this.zoomSpeed * delta;
	}

	// Token: 0x04002111 RID: 8465
	public TBPinchZoom.ZoomMethod zoomMethod;

	// Token: 0x04002112 RID: 8466
	public float zoomSpeed = 1.5f;

	// Token: 0x04002113 RID: 8467
	public float minZoomAmount;

	// Token: 0x04002114 RID: 8468
	public float maxZoomAmount = 50f;

	// Token: 0x04002115 RID: 8469
	private Vector3 defaultPos = Vector3.zero;

	// Token: 0x04002116 RID: 8470
	private float defaultFov;

	// Token: 0x04002117 RID: 8471
	private float defaultOrthoSize;

	// Token: 0x04002118 RID: 8472
	private float zoomAmount;

	// Token: 0x02000454 RID: 1108
	public enum ZoomMethod
	{
		// Token: 0x0400211A RID: 8474
		Position,
		// Token: 0x0400211B RID: 8475
		FOV
	}
}
