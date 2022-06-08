using System;
using UnityEngine;

// Token: 0x0200043A RID: 1082
public class AdvancedPinchRotationSample : SampleBase
{
	// Token: 0x06001F63 RID: 8035 RVA: 0x000EF2EC File Offset: 0x000ED6EC
	protected override void Start()
	{
		base.Start();
		base.UI.StatusText = "Use two fingers anywhere on the screen to rotate and scale the green object.";
		this.originalMaterial = this.target.GetComponent<Renderer>().sharedMaterial;
		this.pinchGesture.OnStateChanged += this.Gesture_OnStateChanged;
		this.pinchGesture.OnPinchMove += this.OnPinchMove;
		this.pinchGesture.SetCanBeginDelegate(new GestureRecognizer.CanBeginDelegate(this.CanBeginPinch));
		this.rotationGesture.OnStateChanged += this.Gesture_OnStateChanged;
		this.rotationGesture.OnRotationMove += this.OnRotationMove;
		this.rotationGesture.SetCanBeginDelegate(new GestureRecognizer.CanBeginDelegate(this.CanBeginRotation));
	}

	// Token: 0x06001F64 RID: 8036 RVA: 0x000EF3AF File Offset: 0x000ED7AF
	private bool CanBeginRotation(GestureRecognizer gr, FingerGestures.IFingerList touches)
	{
		return !this.pinchGesture.IsActive;
	}

	// Token: 0x06001F65 RID: 8037 RVA: 0x000EF3BF File Offset: 0x000ED7BF
	private bool CanBeginPinch(GestureRecognizer gr, FingerGestures.IFingerList touches)
	{
		return !this.rotationGesture.IsActive;
	}

	// Token: 0x06001F66 RID: 8038 RVA: 0x000EF3D0 File Offset: 0x000ED7D0
	private void Gesture_OnStateChanged(GestureRecognizer source)
	{
		if (source.PreviousState == GestureRecognizer.GestureState.Ready && source.State == GestureRecognizer.GestureState.InProgress)
		{
			base.UI.StatusText = source + " gesture started";
		}
		else if (source.PreviousState == GestureRecognizer.GestureState.InProgress)
		{
			if (source.State == GestureRecognizer.GestureState.Failed)
			{
				base.UI.StatusText = source + " gesture failed";
			}
			else if (source.State == GestureRecognizer.GestureState.Recognized)
			{
				base.UI.StatusText = source + " gesture ended";
			}
		}
		this.UpdateTargetMaterial();
	}

	// Token: 0x06001F67 RID: 8039 RVA: 0x000EF46C File Offset: 0x000ED86C
	private void OnPinchMove(PinchGestureRecognizer source)
	{
		base.UI.StatusText = "Pinch updated by " + source.Delta + " degrees";
		this.target.transform.localScale += source.Delta * this.pinchScaleFactor * Vector3.one;
	}

	// Token: 0x06001F68 RID: 8040 RVA: 0x000EF4D0 File Offset: 0x000ED8D0
	private void OnRotationMove(RotationGestureRecognizer source)
	{
		base.UI.StatusText = "Rotation updated by " + source.RotationDelta + " degrees";
		this.target.Rotate(0f, 0f, source.RotationDelta);
	}

	// Token: 0x06001F69 RID: 8041 RVA: 0x000EF51D File Offset: 0x000ED91D
	protected override string GetHelpText()
	{
		return "This sample demonstrates advanced use of the GestureRecognizer classes for Pinch and Rotation";
	}

	// Token: 0x06001F6A RID: 8042 RVA: 0x000EF524 File Offset: 0x000ED924
	private void UpdateTargetMaterial()
	{
		Material sharedMaterial;
		if (this.pinchGesture.IsActive && this.rotationGesture.IsActive)
		{
			sharedMaterial = this.pinchAndRotationMaterial;
		}
		else if (this.pinchGesture.IsActive)
		{
			sharedMaterial = this.pinchMaterial;
		}
		else if (this.rotationGesture.IsActive)
		{
			sharedMaterial = this.rotationMaterial;
		}
		else
		{
			sharedMaterial = this.originalMaterial;
		}
		this.target.GetComponent<Renderer>().sharedMaterial = sharedMaterial;
	}

	// Token: 0x0400207C RID: 8316
	public PinchGestureRecognizer pinchGesture;

	// Token: 0x0400207D RID: 8317
	public RotationGestureRecognizer rotationGesture;

	// Token: 0x0400207E RID: 8318
	public Transform target;

	// Token: 0x0400207F RID: 8319
	public Material rotationMaterial;

	// Token: 0x04002080 RID: 8320
	public Material pinchMaterial;

	// Token: 0x04002081 RID: 8321
	public Material pinchAndRotationMaterial;

	// Token: 0x04002082 RID: 8322
	public float pinchScaleFactor = 0.02f;

	// Token: 0x04002083 RID: 8323
	private Material originalMaterial;
}
