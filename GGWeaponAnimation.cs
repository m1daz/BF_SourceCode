using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000223 RID: 547
public class GGWeaponAnimation : MonoBehaviour
{
	// Token: 0x06000EA9 RID: 3753 RVA: 0x0007A866 File Offset: 0x00078C66
	private void Awake()
	{
		this.A = base.GetComponent<Animation>();
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x0007A874 File Offset: 0x00078C74
	public void Fire(float fireTime)
	{
		if (!this.isPlayingTakeInAnimation)
		{
			this.A.Rewind(this.Shoot);
			this.A[this.Shoot].speed = this.A[this.Shoot].clip.length / fireTime;
			this.A.Play(this.Shoot);
		}
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x0007A8E4 File Offset: 0x00078CE4
	public void Fire_R(float fireTime)
	{
		if (!this.isPlayingTakeInAnimation)
		{
			this.A.Rewind(this.Shoot_R);
			this.A[this.Shoot_R].speed = this.A[this.Shoot_L].clip.length / fireTime;
			this.A.Play(this.Shoot_R);
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x0007A954 File Offset: 0x00078D54
	public void Fire_L(float fireTime)
	{
		if (!this.isPlayingTakeInAnimation)
		{
			this.A.Rewind(this.Shoot_L);
			this.A[this.Shoot_L].speed = this.A[this.Shoot_L].clip.length / fireTime;
			this.A.Play(this.Shoot_L);
		}
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x0007A9C4 File Offset: 0x00078DC4
	public void Reloading(float reloadTime)
	{
		if (!this.isPlayingTakeInAnimation)
		{
			this.A[this.Reload].speed = this.A[this.Reload].clip.length / reloadTime;
			this.A.Rewind(this.Reload);
			this.A.Play(this.Reload);
		}
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x0007AA34 File Offset: 0x00078E34
	public IEnumerator takeIn()
	{
		this.A.Play(this.TakeIn);
		this.isPlayingTakeInAnimation = true;
		yield return new WaitForSeconds(this.A[this.TakeIn].clip.length);
		this.isPlayingTakeInAnimation = false;
		yield break;
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x0007AA50 File Offset: 0x00078E50
	public void takeOut()
	{
		this.A.Rewind(this.TakeOut);
		this.A[this.TakeOut].speed = this.TakeInOutSpeed;
		this.A[this.TakeOut].time = 0f;
		this.A.CrossFade(this.TakeOut);
	}

	// Token: 0x04001005 RID: 4101
	public string Idle = "Idle";

	// Token: 0x04001006 RID: 4102
	public string Reload = "Reload";

	// Token: 0x04001007 RID: 4103
	public string Shoot = "Fire";

	// Token: 0x04001008 RID: 4104
	public string Shoot_R = "Fire_R";

	// Token: 0x04001009 RID: 4105
	public string Shoot_L = "Fire_L";

	// Token: 0x0400100A RID: 4106
	public string TakeIn = "TakeIn";

	// Token: 0x0400100B RID: 4107
	public string TakeOut = "TakeOut";

	// Token: 0x0400100C RID: 4108
	private float FireAnimationSpeed = 1f;

	// Token: 0x0400100D RID: 4109
	private float TakeInOutSpeed = 1f;

	// Token: 0x0400100E RID: 4110
	private string PlayThis;

	// Token: 0x0400100F RID: 4111
	private GameObject player;

	// Token: 0x04001010 RID: 4112
	private bool isPlayingTakeInAnimation;

	// Token: 0x04001011 RID: 4113
	private Animation A;
}
