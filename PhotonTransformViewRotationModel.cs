using System;

// Token: 0x02000132 RID: 306
[Serializable]
public class PhotonTransformViewRotationModel
{
	// Token: 0x04000856 RID: 2134
	public bool SynchronizeEnabled;

	// Token: 0x04000857 RID: 2135
	public PhotonTransformViewRotationModel.InterpolateOptions InterpolateOption = PhotonTransformViewRotationModel.InterpolateOptions.RotateTowards;

	// Token: 0x04000858 RID: 2136
	public float InterpolateRotateTowardsSpeed = 180f;

	// Token: 0x04000859 RID: 2137
	public float InterpolateLerpSpeed = 5f;

	// Token: 0x02000133 RID: 307
	public enum InterpolateOptions
	{
		// Token: 0x0400085B RID: 2139
		Disabled,
		// Token: 0x0400085C RID: 2140
		RotateTowards,
		// Token: 0x0400085D RID: 2141
		Lerp
	}
}
