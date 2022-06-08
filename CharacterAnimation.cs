using System;
using UnityEngine;

// Token: 0x020003F2 RID: 1010
public class CharacterAnimation : MonoBehaviour
{
	// Token: 0x06001E58 RID: 7768 RVA: 0x000E803F File Offset: 0x000E643F
	private void Start()
	{
		this.cc = base.GetComponentInChildren<CharacterController>();
		this.anim = base.GetComponentInChildren<Animation>();
	}

	// Token: 0x06001E59 RID: 7769 RVA: 0x000E805C File Offset: 0x000E645C
	private void LateUpdate()
	{
		if (this.cc.isGrounded && ETCInput.GetAxis("Vertical") != 0f)
		{
			this.anim.CrossFade("soldierRun");
		}
		if (this.cc.isGrounded && ETCInput.GetAxis("Vertical") == 0f && ETCInput.GetAxis("Horizontal") == 0f)
		{
			this.anim.CrossFade("soldierIdleRelaxed");
		}
		if (!this.cc.isGrounded)
		{
			this.anim.CrossFade("soldierFalling");
		}
		if (this.cc.isGrounded && ETCInput.GetAxis("Vertical") == 0f && ETCInput.GetAxis("Horizontal") > 0f)
		{
			this.anim.CrossFade("soldierSpinRight");
		}
		if (this.cc.isGrounded && ETCInput.GetAxis("Vertical") == 0f && ETCInput.GetAxis("Horizontal") < 0f)
		{
			this.anim.CrossFade("soldierSpinLeft");
		}
	}

	// Token: 0x04001F60 RID: 8032
	private CharacterController cc;

	// Token: 0x04001F61 RID: 8033
	private Animation anim;
}
