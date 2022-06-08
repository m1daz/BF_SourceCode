using System;
using UnityEngine;

// Token: 0x02000448 RID: 1096
public class PinchRotationSample : SampleBase
{
	// Token: 0x06001FC2 RID: 8130 RVA: 0x000F0B97 File Offset: 0x000EEF97
	protected override string GetHelpText()
	{
		return "This sample demonstrates how to use the two-fingers Pinch and Rotation gesture events to control the scale and orientation of a rectangle on the screen\r\n\r\n- Pinch: move two fingers closer or further apart to change the scale of the rectangle\r\n- Rotation: twist two fingers in a circular motion to rotate the rectangle\r\n\r\n";
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x000F0B9E File Offset: 0x000EEF9E
	protected override void Start()
	{
		base.Start();
		base.UI.StatusText = "Use two fingers anywhere on the screen to rotate and scale the green object.";
		this.originalMaterial = this.target.GetComponent<Renderer>().sharedMaterial;
	}

	// Token: 0x06001FC4 RID: 8132 RVA: 0x000F0BCC File Offset: 0x000EEFCC
	private void OnEnable()
	{
		FingerGestures.OnRotationBegin += this.FingerGestures_OnRotationBegin;
		FingerGestures.OnRotationMove += this.FingerGestures_OnRotationMove;
		FingerGestures.OnRotationEnd += this.FingerGestures_OnRotationEnd;
		FingerGestures.OnPinchBegin += this.FingerGestures_OnPinchBegin;
		FingerGestures.OnPinchMove += this.FingerGestures_OnPinchMove;
		FingerGestures.OnPinchEnd += this.FingerGestures_OnPinchEnd;
	}

	// Token: 0x06001FC5 RID: 8133 RVA: 0x000F0C40 File Offset: 0x000EF040
	private void OnDisable()
	{
		FingerGestures.OnRotationBegin -= this.FingerGestures_OnRotationBegin;
		FingerGestures.OnRotationMove -= this.FingerGestures_OnRotationMove;
		FingerGestures.OnRotationEnd -= this.FingerGestures_OnRotationEnd;
		FingerGestures.OnPinchBegin -= this.FingerGestures_OnPinchBegin;
		FingerGestures.OnPinchMove -= this.FingerGestures_OnPinchMove;
		FingerGestures.OnPinchEnd -= this.FingerGestures_OnPinchEnd;
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x000F0CB3 File Offset: 0x000EF0B3
	// (set) Token: 0x06001FC7 RID: 8135 RVA: 0x000F0CBB File Offset: 0x000EF0BB
	private bool Rotating
	{
		get
		{
			return this.rotating;
		}
		set
		{
			if (this.rotating != value)
			{
				this.rotating = value;
				this.UpdateTargetMaterial();
			}
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x000F0CD6 File Offset: 0x000EF0D6
	public bool RotationAllowed
	{
		get
		{
			return this.inputMode == PinchRotationSample.InputMode.RotationOnly || this.inputMode == PinchRotationSample.InputMode.PinchAndRotation;
		}
	}

	// Token: 0x06001FC9 RID: 8137 RVA: 0x000F0CF0 File Offset: 0x000EF0F0
	private void FingerGestures_OnRotationBegin(Vector2 fingerPos1, Vector2 fingerPos2)
	{
		if (this.RotationAllowed)
		{
			base.UI.StatusText = "Rotation gesture started.";
			this.Rotating = true;
		}
	}

	// Token: 0x06001FCA RID: 8138 RVA: 0x000F0D14 File Offset: 0x000EF114
	private void FingerGestures_OnRotationMove(Vector2 fingerPos1, Vector2 fingerPos2, float rotationAngleDelta)
	{
		if (this.Rotating)
		{
			base.UI.StatusText = "Rotation updated by " + rotationAngleDelta + " degrees";
			this.target.Rotate(0f, 0f, rotationAngleDelta);
		}
	}

	// Token: 0x06001FCB RID: 8139 RVA: 0x000F0D62 File Offset: 0x000EF162
	private void FingerGestures_OnRotationEnd(Vector2 fingerPos1, Vector2 fingerPos2, float totalRotationAngle)
	{
		if (this.Rotating)
		{
			base.UI.StatusText = "Rotation gesture ended. Total rotation: " + totalRotationAngle;
			this.Rotating = false;
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x06001FCC RID: 8140 RVA: 0x000F0D91 File Offset: 0x000EF191
	// (set) Token: 0x06001FCD RID: 8141 RVA: 0x000F0D99 File Offset: 0x000EF199
	private bool Pinching
	{
		get
		{
			return this.pinching;
		}
		set
		{
			if (this.pinching != value)
			{
				this.pinching = value;
				this.UpdateTargetMaterial();
			}
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06001FCE RID: 8142 RVA: 0x000F0DB4 File Offset: 0x000EF1B4
	public bool PinchAllowed
	{
		get
		{
			return this.inputMode == PinchRotationSample.InputMode.PinchOnly || this.inputMode == PinchRotationSample.InputMode.PinchAndRotation;
		}
	}

	// Token: 0x06001FCF RID: 8143 RVA: 0x000F0DCD File Offset: 0x000EF1CD
	private void FingerGestures_OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
	{
		if (!this.PinchAllowed)
		{
			return;
		}
		this.Pinching = true;
	}

	// Token: 0x06001FD0 RID: 8144 RVA: 0x000F0DE2 File Offset: 0x000EF1E2
	private void FingerGestures_OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
	{
		if (this.Pinching)
		{
			this.target.transform.localScale += delta * this.pinchScaleFactor * Vector3.one;
		}
	}

	// Token: 0x06001FD1 RID: 8145 RVA: 0x000F0E1C File Offset: 0x000EF21C
	private void FingerGestures_OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
	{
		if (this.Pinching)
		{
			this.Pinching = false;
		}
	}

	// Token: 0x06001FD2 RID: 8146 RVA: 0x000F0E30 File Offset: 0x000EF230
	private void UpdateTargetMaterial()
	{
		Material sharedMaterial;
		if (this.pinching && this.rotating)
		{
			sharedMaterial = this.pinchAndRotationMaterial;
		}
		else if (this.pinching)
		{
			sharedMaterial = this.pinchMaterial;
		}
		else if (this.rotating)
		{
			sharedMaterial = this.rotationMaterial;
		}
		else
		{
			sharedMaterial = this.originalMaterial;
		}
		this.target.GetComponent<Renderer>().sharedMaterial = sharedMaterial;
	}

	// Token: 0x06001FD3 RID: 8147 RVA: 0x000F0EA8 File Offset: 0x000EF2A8
	private void OnGUI()
	{
		SampleUI.ApplyVirtualScreen();
		PinchRotationSample.InputMode inputMode = this.inputMode;
		string text;
		PinchRotationSample.InputMode inputMode2;
		if (inputMode != PinchRotationSample.InputMode.PinchOnly)
		{
			if (inputMode != PinchRotationSample.InputMode.RotationOnly)
			{
				text = "Pinch + Rotation";
				inputMode2 = PinchRotationSample.InputMode.PinchOnly;
			}
			else
			{
				text = "Rotation Only";
				inputMode2 = PinchRotationSample.InputMode.PinchAndRotation;
			}
		}
		else
		{
			text = "Pinch Only";
			inputMode2 = PinchRotationSample.InputMode.RotationOnly;
		}
		if (GUI.Button(this.inputModeButtonRect, text))
		{
			this.inputMode = inputMode2;
		}
	}

	// Token: 0x040020CB RID: 8395
	public Transform target;

	// Token: 0x040020CC RID: 8396
	public Material rotationMaterial;

	// Token: 0x040020CD RID: 8397
	public Material pinchMaterial;

	// Token: 0x040020CE RID: 8398
	public Material pinchAndRotationMaterial;

	// Token: 0x040020CF RID: 8399
	public float pinchScaleFactor = 0.02f;

	// Token: 0x040020D0 RID: 8400
	private Material originalMaterial;

	// Token: 0x040020D1 RID: 8401
	private PinchRotationSample.InputMode inputMode = PinchRotationSample.InputMode.PinchAndRotation;

	// Token: 0x040020D2 RID: 8402
	private bool rotating;

	// Token: 0x040020D3 RID: 8403
	private bool pinching;

	// Token: 0x040020D4 RID: 8404
	public Rect inputModeButtonRect;

	// Token: 0x02000449 RID: 1097
	public enum InputMode
	{
		// Token: 0x040020D6 RID: 8406
		PinchOnly,
		// Token: 0x040020D7 RID: 8407
		RotationOnly,
		// Token: 0x040020D8 RID: 8408
		PinchAndRotation
	}
}
