using System;
using UnityEngine;

// Token: 0x0200012E RID: 302
[Serializable]
public class PhotonTransformViewPositionModel
{
	// Token: 0x0400083B RID: 2107
	public bool SynchronizeEnabled;

	// Token: 0x0400083C RID: 2108
	public bool TeleportEnabled = true;

	// Token: 0x0400083D RID: 2109
	public float TeleportIfDistanceGreaterThan = 3f;

	// Token: 0x0400083E RID: 2110
	public PhotonTransformViewPositionModel.InterpolateOptions InterpolateOption = PhotonTransformViewPositionModel.InterpolateOptions.EstimatedSpeed;

	// Token: 0x0400083F RID: 2111
	public float InterpolateMoveTowardsSpeed = 1f;

	// Token: 0x04000840 RID: 2112
	public float InterpolateLerpSpeed = 1f;

	// Token: 0x04000841 RID: 2113
	public float InterpolateMoveTowardsAcceleration = 2f;

	// Token: 0x04000842 RID: 2114
	public float InterpolateMoveTowardsDeceleration = 2f;

	// Token: 0x04000843 RID: 2115
	public AnimationCurve InterpolateSpeedCurve = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(-1f, 0f, 0f, float.PositiveInfinity),
		new Keyframe(0f, 1f, 0f, 0f),
		new Keyframe(1f, 1f, 0f, 1f),
		new Keyframe(4f, 4f, 1f, 0f)
	});

	// Token: 0x04000844 RID: 2116
	public PhotonTransformViewPositionModel.ExtrapolateOptions ExtrapolateOption;

	// Token: 0x04000845 RID: 2117
	public float ExtrapolateSpeed = 1f;

	// Token: 0x04000846 RID: 2118
	public bool ExtrapolateIncludingRoundTripTime = true;

	// Token: 0x04000847 RID: 2119
	public int ExtrapolateNumberOfStoredPositions = 1;

	// Token: 0x04000848 RID: 2120
	public bool DrawErrorGizmo = true;

	// Token: 0x0200012F RID: 303
	public enum InterpolateOptions
	{
		// Token: 0x0400084A RID: 2122
		Disabled,
		// Token: 0x0400084B RID: 2123
		FixedSpeed,
		// Token: 0x0400084C RID: 2124
		EstimatedSpeed,
		// Token: 0x0400084D RID: 2125
		SynchronizeValues,
		// Token: 0x0400084E RID: 2126
		Lerp
	}

	// Token: 0x02000130 RID: 304
	public enum ExtrapolateOptions
	{
		// Token: 0x04000850 RID: 2128
		Disabled,
		// Token: 0x04000851 RID: 2129
		SynchronizeValues,
		// Token: 0x04000852 RID: 2130
		EstimateSpeedAndTurn,
		// Token: 0x04000853 RID: 2131
		FixedSpeed
	}
}
