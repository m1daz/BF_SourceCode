using System;
using UnityEngine;

// Token: 0x02000451 RID: 1105
[AddComponentMenu("FingerGestures/Toolbox/Misc/DragOrbit")]
public class TBDragOrbit : MonoBehaviour
{
	// Token: 0x17000171 RID: 369
	// (get) Token: 0x06002017 RID: 8215 RVA: 0x000F1B6C File Offset: 0x000EFF6C
	public float Distance
	{
		get
		{
			return this.distance;
		}
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x06002018 RID: 8216 RVA: 0x000F1B74 File Offset: 0x000EFF74
	// (set) Token: 0x06002019 RID: 8217 RVA: 0x000F1B7C File Offset: 0x000EFF7C
	public float IdealDistance
	{
		get
		{
			return this.idealDistance;
		}
		set
		{
			this.idealDistance = Mathf.Clamp(value, this.minDistance, this.maxDistance);
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x0600201A RID: 8218 RVA: 0x000F1B96 File Offset: 0x000EFF96
	public float Yaw
	{
		get
		{
			return this.yaw;
		}
	}

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x0600201B RID: 8219 RVA: 0x000F1B9E File Offset: 0x000EFF9E
	// (set) Token: 0x0600201C RID: 8220 RVA: 0x000F1BA6 File Offset: 0x000EFFA6
	public float IdealYaw
	{
		get
		{
			return this.idealYaw;
		}
		set
		{
			this.idealYaw = value;
		}
	}

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x0600201D RID: 8221 RVA: 0x000F1BAF File Offset: 0x000EFFAF
	public float Pitch
	{
		get
		{
			return this.pitch;
		}
	}

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x0600201E RID: 8222 RVA: 0x000F1BB7 File Offset: 0x000EFFB7
	// (set) Token: 0x0600201F RID: 8223 RVA: 0x000F1BBF File Offset: 0x000EFFBF
	public float IdealPitch
	{
		get
		{
			return this.idealPitch;
		}
		set
		{
			this.idealPitch = ((!this.clampPitchAngle) ? value : TBDragOrbit.ClampAngle(value, this.minPitch, this.maxPitch));
		}
	}

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06002020 RID: 8224 RVA: 0x000F1BEA File Offset: 0x000EFFEA
	// (set) Token: 0x06002021 RID: 8225 RVA: 0x000F1BF2 File Offset: 0x000EFFF2
	public Vector3 IdealPanOffset
	{
		get
		{
			return this.idealPanOffset;
		}
		set
		{
			this.idealPanOffset = value;
		}
	}

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x06002022 RID: 8226 RVA: 0x000F1BFB File Offset: 0x000EFFFB
	public Vector3 PanOffset
	{
		get
		{
			return this.panOffset;
		}
	}

	// Token: 0x06002023 RID: 8227 RVA: 0x000F1C04 File Offset: 0x000F0004
	private void Start()
	{
		if (!this.panningPlane)
		{
			this.panningPlane = base.transform;
		}
		Vector3 eulerAngles = base.transform.eulerAngles;
		float num = this.initialDistance;
		this.IdealDistance = num;
		this.distance = num;
		num = eulerAngles.y;
		this.IdealYaw = num;
		this.yaw = num;
		num = eulerAngles.x;
		this.IdealPitch = num;
		this.pitch = num;
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().freezeRotation = true;
		}
		this.Apply();
	}

	// Token: 0x06002024 RID: 8228 RVA: 0x000F1C9C File Offset: 0x000F009C
	private void OnEnable()
	{
		FingerGestures.OnDragMove += this.FingerGestures_OnDragMove;
		FingerGestures.OnPinchMove += this.FingerGestures_OnPinchMove;
		FingerGestures.OnTwoFingerDragMove += this.FingerGestures_OnTwoFingerDragMove;
	}

	// Token: 0x06002025 RID: 8229 RVA: 0x000F1CD1 File Offset: 0x000F00D1
	private void OnDisable()
	{
		FingerGestures.OnDragMove -= this.FingerGestures_OnDragMove;
		FingerGestures.OnPinchMove -= this.FingerGestures_OnPinchMove;
		FingerGestures.OnTwoFingerDragMove -= this.FingerGestures_OnTwoFingerDragMove;
	}

	// Token: 0x06002026 RID: 8230 RVA: 0x000F1D08 File Offset: 0x000F0108
	private void FingerGestures_OnDragMove(Vector2 fingerPos, Vector2 delta)
	{
		if (Time.time - this.lastPanTime < 0.25f)
		{
			return;
		}
		if (this.target)
		{
			this.IdealYaw += delta.x * this.yawSensitivity * 0.02f;
			this.IdealPitch -= delta.y * this.pitchSensitivity * 0.02f;
		}
	}

	// Token: 0x06002027 RID: 8231 RVA: 0x000F1D7E File Offset: 0x000F017E
	private void FingerGestures_OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
	{
		if (this.allowPinchZoom)
		{
			this.IdealDistance -= delta * this.pinchZoomSensitivity;
		}
	}

	// Token: 0x06002028 RID: 8232 RVA: 0x000F1DA0 File Offset: 0x000F01A0
	private void FingerGestures_OnTwoFingerDragMove(Vector2 fingerPos, Vector2 delta)
	{
		if (this.allowPanning)
		{
			Vector3 b = -0.02f * this.panningSensitivity * (this.panningPlane.right * delta.x + this.panningPlane.up * delta.y);
			if (this.invertPanningDirections)
			{
				this.IdealPanOffset -= b;
			}
			else
			{
				this.IdealPanOffset += b;
			}
			this.lastPanTime = Time.time;
		}
	}

	// Token: 0x06002029 RID: 8233 RVA: 0x000F1E3C File Offset: 0x000F023C
	private void Apply()
	{
		if (this.smoothMotion)
		{
			this.distance = Mathf.Lerp(this.distance, this.IdealDistance, Time.deltaTime * this.smoothZoomSpeed);
			this.yaw = Mathf.Lerp(this.yaw, this.IdealYaw, Time.deltaTime * this.smoothOrbitSpeed);
			this.pitch = Mathf.Lerp(this.pitch, this.IdealPitch, Time.deltaTime * this.smoothOrbitSpeed);
		}
		else
		{
			this.distance = this.IdealDistance;
			this.yaw = this.IdealYaw;
			this.pitch = this.IdealPitch;
		}
		if (this.smoothPanning)
		{
			this.panOffset = Vector3.Lerp(this.panOffset, this.idealPanOffset, Time.deltaTime * this.smoothPanningSpeed);
		}
		else
		{
			this.panOffset = this.idealPanOffset;
		}
		base.transform.rotation = Quaternion.Euler(this.pitch, this.yaw, 0f);
		base.transform.position = this.target.position + this.panOffset - this.distance * base.transform.forward;
	}

	// Token: 0x0600202A RID: 8234 RVA: 0x000F1F82 File Offset: 0x000F0382
	private void LateUpdate()
	{
		this.Apply();
	}

	// Token: 0x0600202B RID: 8235 RVA: 0x000F1F8A File Offset: 0x000F038A
	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x0600202C RID: 8236 RVA: 0x000F1FBC File Offset: 0x000F03BC
	public void ResetPanning()
	{
		this.IdealPanOffset = Vector3.zero;
	}

	// Token: 0x040020F0 RID: 8432
	public Transform target;

	// Token: 0x040020F1 RID: 8433
	public float initialDistance = 10f;

	// Token: 0x040020F2 RID: 8434
	public float minDistance = 1f;

	// Token: 0x040020F3 RID: 8435
	public float maxDistance = 20f;

	// Token: 0x040020F4 RID: 8436
	public float yawSensitivity = 80f;

	// Token: 0x040020F5 RID: 8437
	public float pitchSensitivity = 80f;

	// Token: 0x040020F6 RID: 8438
	public bool clampPitchAngle = true;

	// Token: 0x040020F7 RID: 8439
	public float minPitch = -20f;

	// Token: 0x040020F8 RID: 8440
	public float maxPitch = 80f;

	// Token: 0x040020F9 RID: 8441
	public bool allowPinchZoom = true;

	// Token: 0x040020FA RID: 8442
	public float pinchZoomSensitivity = 2f;

	// Token: 0x040020FB RID: 8443
	public bool smoothMotion = true;

	// Token: 0x040020FC RID: 8444
	public float smoothZoomSpeed = 3f;

	// Token: 0x040020FD RID: 8445
	public float smoothOrbitSpeed = 4f;

	// Token: 0x040020FE RID: 8446
	public bool allowPanning;

	// Token: 0x040020FF RID: 8447
	public bool invertPanningDirections;

	// Token: 0x04002100 RID: 8448
	public float panningSensitivity = 1f;

	// Token: 0x04002101 RID: 8449
	public Transform panningPlane;

	// Token: 0x04002102 RID: 8450
	public bool smoothPanning = true;

	// Token: 0x04002103 RID: 8451
	public float smoothPanningSpeed = 8f;

	// Token: 0x04002104 RID: 8452
	private float lastPanTime;

	// Token: 0x04002105 RID: 8453
	private float distance = 10f;

	// Token: 0x04002106 RID: 8454
	private float yaw;

	// Token: 0x04002107 RID: 8455
	private float pitch;

	// Token: 0x04002108 RID: 8456
	private float idealDistance;

	// Token: 0x04002109 RID: 8457
	private float idealYaw;

	// Token: 0x0400210A RID: 8458
	private float idealPitch;

	// Token: 0x0400210B RID: 8459
	private Vector3 idealPanOffset = Vector3.zero;

	// Token: 0x0400210C RID: 8460
	private Vector3 panOffset = Vector3.zero;

	// Token: 0x02000452 RID: 1106
	public enum PanMode
	{
		// Token: 0x0400210E RID: 8462
		Disabled,
		// Token: 0x0400210F RID: 8463
		OneFinger,
		// Token: 0x04002110 RID: 8464
		TwoFingers
	}
}
