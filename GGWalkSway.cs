using System;
using UnityEngine;

// Token: 0x02000222 RID: 546
public class GGWalkSway : MonoBehaviour
{
	// Token: 0x06000EA5 RID: 3749 RVA: 0x0007A5AD File Offset: 0x000789AD
	private void Awake()
	{
		this.midpoint = base.transform.localPosition;
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x0007A5C0 File Offset: 0x000789C0
	private void Start()
	{
		this.mNetworkCharacter = base.transform.root.GetComponent<GGNetworkCharacter>();
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x0007A5D8 File Offset: 0x000789D8
	private void FixedUpdate()
	{
		Vector3 zero = Vector3.zero;
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
			zero.y = this.midpoint.y + num9;
			zero.x = this.midpoint.x + num10;
		}
		else
		{
			zero = this.midpoint;
		}
		if (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Walk)
		{
			this.bobbingSpeed = num;
			this.BobbingAmount = this.bobbingAmount * 1f;
		}
		if (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Idle)
		{
			this.bobbingSpeed = num3;
			this.BobbingAmount = this.bobbingAmount * 0.3f;
		}
		float num11 = 0f;
		num11 += Time.deltaTime * this.smooth;
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, zero, num11);
	}

	// Token: 0x04000FFB RID: 4091
	public float walkBobbingSpeed = 0.14f;

	// Token: 0x04000FFC RID: 4092
	public float runBobbingSpeed = 0.35f;

	// Token: 0x04000FFD RID: 4093
	public float idleBobbingSpeed = 0.1f;

	// Token: 0x04000FFE RID: 4094
	public float bobbingAmount = 0.06f;

	// Token: 0x04000FFF RID: 4095
	public float smooth = 1f;

	// Token: 0x04001000 RID: 4096
	private Vector3 midpoint;

	// Token: 0x04001001 RID: 4097
	private float timer;

	// Token: 0x04001002 RID: 4098
	private float bobbingSpeed;

	// Token: 0x04001003 RID: 4099
	private float BobbingAmount;

	// Token: 0x04001004 RID: 4100
	private GGNetworkCharacter mNetworkCharacter;
}
