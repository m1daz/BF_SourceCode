using System;

// Token: 0x02000135 RID: 309
[Serializable]
public class PhotonTransformViewScaleModel
{
	// Token: 0x04000860 RID: 2144
	public bool SynchronizeEnabled;

	// Token: 0x04000861 RID: 2145
	public PhotonTransformViewScaleModel.InterpolateOptions InterpolateOption;

	// Token: 0x04000862 RID: 2146
	public float InterpolateMoveTowardsSpeed = 1f;

	// Token: 0x04000863 RID: 2147
	public float InterpolateLerpSpeed;

	// Token: 0x02000136 RID: 310
	public enum InterpolateOptions
	{
		// Token: 0x04000865 RID: 2149
		Disabled,
		// Token: 0x04000866 RID: 2150
		MoveTowards,
		// Token: 0x04000867 RID: 2151
		Lerp
	}
}
