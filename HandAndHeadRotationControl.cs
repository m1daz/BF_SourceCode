using System;
using UnityEngine;

// Token: 0x020001F5 RID: 501
public class HandAndHeadRotationControl : MonoBehaviour
{
	// Token: 0x06000DC2 RID: 3522 RVA: 0x000723A9 File Offset: 0x000707A9
	private void Start()
	{
		this.mNetworkCharacter = base.transform.root.GetComponent<GGNetworkCharacter>();
		this.originTransform = 0f;
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x000723CC File Offset: 0x000707CC
	private void Update()
	{
		if (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Dead)
		{
			return;
		}
		this.cameraTransform = this.mNetworkCharacter.cameraRotationX;
		if (this.cameraTransform >= 20f && this.cameraTransform < 90f)
		{
			this.headTransform.localEulerAngles = new Vector3(this.headTransform.localEulerAngles.x, 20f, this.headTransform.localEulerAngles.z);
			this.leftHandTransform.localEulerAngles = new Vector3(this.leftHandTransform.localEulerAngles.x, 20f, this.leftHandTransform.localEulerAngles.z);
			this.rightHandTransform.localEulerAngles = new Vector3(this.rightHandTransform.localEulerAngles.x, 20f, this.rightHandTransform.localEulerAngles.z);
		}
		else if (this.cameraTransform > 270f && this.cameraTransform <= 340f)
		{
			this.headTransform.localEulerAngles = new Vector3(this.headTransform.localEulerAngles.x, 340f, this.headTransform.localEulerAngles.z);
			this.leftHandTransform.localEulerAngles = new Vector3(this.leftHandTransform.localEulerAngles.x, this.cameraTransform, this.leftHandTransform.localEulerAngles.z);
			this.rightHandTransform.localEulerAngles = new Vector3(this.rightHandTransform.localEulerAngles.x, this.cameraTransform, this.rightHandTransform.localEulerAngles.z);
		}
		else
		{
			this.headTransform.localEulerAngles = new Vector3(this.headTransform.localEulerAngles.x, this.cameraTransform, this.headTransform.localEulerAngles.z);
			this.leftHandTransform.localEulerAngles = new Vector3(this.leftHandTransform.localEulerAngles.x, this.cameraTransform, this.leftHandTransform.localEulerAngles.z);
			this.rightHandTransform.localEulerAngles = new Vector3(this.rightHandTransform.localEulerAngles.x, this.cameraTransform, this.rightHandTransform.localEulerAngles.z);
		}
	}

	// Token: 0x04000E33 RID: 3635
	public Transform headTransform;

	// Token: 0x04000E34 RID: 3636
	public Transform leftHandTransform;

	// Token: 0x04000E35 RID: 3637
	public Transform rightHandTransform;

	// Token: 0x04000E36 RID: 3638
	private float cameraTransform;

	// Token: 0x04000E37 RID: 3639
	private float originTransform;

	// Token: 0x04000E38 RID: 3640
	private float target;

	// Token: 0x04000E39 RID: 3641
	private GGNetworkCharacter mNetworkCharacter;
}
