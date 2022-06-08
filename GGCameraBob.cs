using System;
using UnityEngine;

// Token: 0x02000212 RID: 530
public class GGCameraBob : MonoBehaviour
{
	// Token: 0x06000E69 RID: 3689 RVA: 0x000793BE File Offset: 0x000777BE
	private void Awake()
	{
		this.player = GameObject.FindWithTag("Player");
		this.midpoint = base.transform.localPosition;
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x000793E4 File Offset: 0x000777E4
	private void FixedUpdate()
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		if (Time.timeScale == 1f)
		{
			if (num != this.walkBobbingSpeed || num2 != this.runBobbingSpeed || num3 != this.idleBobbingSpeed)
			{
				num = this.walkBobbingSpeed;
				num2 = this.runBobbingSpeed;
				num3 = this.idleBobbingSpeed;
			}
		}
		else
		{
			num = this.walkBobbingSpeed * (Time.fixedDeltaTime / 0.02f);
			num2 = this.runBobbingSpeed * (Time.fixedDeltaTime / 0.02f);
			num3 = this.idleBobbingSpeed * (Time.fixedDeltaTime / 0.02f);
		}
		Vector3 zero = Vector3.zero;
		float num4 = Mathf.Sin(this.timer * 2f);
		float num5 = Mathf.Sin(this.timer);
		this.timer += this.bobbingSpeed;
		if (this.timer > 6.2831855f)
		{
			this.timer -= 6.2831855f;
		}
		if (num4 != 0f)
		{
			float num6 = num4 * this.BobbingAmount;
			float num7 = num5 * this.BobbingAmount;
			float num8 = Mathf.Clamp(1f, 0f, 1f);
			float num9 = num8 * num6;
			float num10 = num8 * num7;
		}
		else
		{
			zero = this.midpoint;
		}
		float num11 = 0f;
		num11 += Time.deltaTime * this.smooth;
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, zero, num11);
	}

	// Token: 0x04000F9F RID: 3999
	private float walkBobbingSpeed = 0.21f;

	// Token: 0x04000FA0 RID: 4000
	private float runBobbingSpeed = 0.35f;

	// Token: 0x04000FA1 RID: 4001
	private float idleBobbingSpeed = 0.1f;

	// Token: 0x04000FA2 RID: 4002
	private float bobbingAmount = 0.1f;

	// Token: 0x04000FA3 RID: 4003
	private float smooth = 1f;

	// Token: 0x04000FA4 RID: 4004
	private Vector3 midpoint;

	// Token: 0x04000FA5 RID: 4005
	private GameObject player;

	// Token: 0x04000FA6 RID: 4006
	private float timer;

	// Token: 0x04000FA7 RID: 4007
	private float bobbingSpeed;

	// Token: 0x04000FA8 RID: 4008
	private float BobbingAmount;
}
