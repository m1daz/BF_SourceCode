using System;
using UnityEngine;

// Token: 0x020003F3 RID: 1011
public class CharacterAnimationDungeon : MonoBehaviour
{
	// Token: 0x06001E5B RID: 7771 RVA: 0x000E819D File Offset: 0x000E659D
	private void Start()
	{
		this.cc = base.GetComponentInChildren<CharacterController>();
		this.anim = base.GetComponentInChildren<Animation>();
	}

	// Token: 0x06001E5C RID: 7772 RVA: 0x000E81B8 File Offset: 0x000E65B8
	private void LateUpdate()
	{
		if (this.cc.isGrounded && (ETCInput.GetAxis("Vertical") != 0f || ETCInput.GetAxis("Horizontal") != 0f))
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
	}

	// Token: 0x04001F62 RID: 8034
	private CharacterController cc;

	// Token: 0x04001F63 RID: 8035
	private Animation anim;
}
