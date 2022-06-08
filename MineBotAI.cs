using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
[RequireComponent(typeof(Seeker))]
public class MineBotAI : AIPath
{
	// Token: 0x0600032B RID: 811 RVA: 0x000189C0 File Offset: 0x00016DC0
	public new void Start()
	{
		this.anim["forward"].layer = 10;
		this.anim.Play("awake");
		this.anim.Play("forward");
		this.anim["awake"].wrapMode = WrapMode.Once;
		this.anim["awake"].speed = 0f;
		this.anim["awake"].normalizedTime = 1f;
		base.Start();
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00018A58 File Offset: 0x00016E58
	public override void OnTargetReached()
	{
		if (this.endOfPathEffect != null && Vector3.Distance(this.tr.position, this.lastTarget) > 1f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.endOfPathEffect, this.tr.position, this.tr.rotation);
			this.lastTarget = this.tr.position;
		}
	}

	// Token: 0x0600032D RID: 813 RVA: 0x00018AC9 File Offset: 0x00016EC9
	public override Vector3 GetFeetPosition()
	{
		return this.tr.position;
	}

	// Token: 0x0600032E RID: 814 RVA: 0x00018AD8 File Offset: 0x00016ED8
	protected new void Update()
	{
		Vector3 direction;
		if (this.canMove)
		{
			Vector3 vector = base.CalculateVelocity(this.GetFeetPosition());
			if (this.targetDirection != Vector3.zero)
			{
				this.RotateTowards(this.targetDirection);
			}
			if (vector.sqrMagnitude <= this.sleepVelocity * this.sleepVelocity)
			{
				vector = Vector3.zero;
			}
			if (this.navController != null)
			{
				this.navController.SimpleMove(this.GetFeetPosition(), vector);
			}
			else if (this.controller != null)
			{
				this.controller.SimpleMove(vector);
			}
			else
			{
				Debug.LogWarning("No NavmeshController or CharacterController attached to GameObject");
			}
			direction = this.controller.velocity;
		}
		else
		{
			direction = Vector3.zero;
		}
		Vector3 vector2 = this.tr.InverseTransformDirection(direction);
		if (direction.sqrMagnitude <= this.sleepVelocity * this.sleepVelocity)
		{
			this.anim.Blend("forward", 0f, 0.2f);
		}
		else
		{
			this.anim.Blend("forward", 1f, 0.2f);
			AnimationState animationState = this.anim["forward"];
			float z = vector2.z;
			animationState.speed = z * this.animationSpeed;
		}
	}

	// Token: 0x0400029A RID: 666
	public Animation anim;

	// Token: 0x0400029B RID: 667
	public float sleepVelocity = 0.4f;

	// Token: 0x0400029C RID: 668
	public float animationSpeed = 0.2f;

	// Token: 0x0400029D RID: 669
	public GameObject endOfPathEffect;

	// Token: 0x0400029E RID: 670
	protected Vector3 lastTarget;
}
