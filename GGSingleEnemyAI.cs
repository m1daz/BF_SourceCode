using System;
using UnityEngine;

// Token: 0x0200025F RID: 607
[RequireComponent(typeof(Seeker))]
public class GGSingleEnemyAI : AIPath
{
	// Token: 0x0600117B RID: 4475 RVA: 0x0009BC2C File Offset: 0x0009A02C
	public new void Start()
	{
		this.enemyAnimation = base.transform.Find("EnemyAnimation");
		this.GGsingleEnemyLogic = base.GetComponent<GGSingleEnemyLogic>();
		this.target = base.transform;
		this.GGsingleEnemyLogic.enemyAction = EnemyAction.idle;
		base.Start();
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x0009BC7C File Offset: 0x0009A07C
	public override void OnTargetReached()
	{
		if (this.endOfPathEffect != null && Vector3.Distance(this.tr.position, this.lastTarget) > 1f)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.endOfPathEffect, this.tr.position, this.tr.rotation);
			this.lastTarget = this.tr.position;
		}
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x0009BCED File Offset: 0x0009A0ED
	public override Vector3 GetFeetPosition()
	{
		return this.tr.position;
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x0009BCFC File Offset: 0x0009A0FC
	protected new void Update()
	{
		Vector3 direction;
		if (this.canMove && this.canSearch)
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
				if (this.canSearch)
				{
					if (this.controller.isGrounded)
					{
						this.controller.Move(vector * Time.deltaTime);
					}
					else
					{
						this.controller.Move(vector * Time.deltaTime + Physics.gravity * Time.deltaTime);
					}
				}
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
		}
	}

	// Token: 0x0400140E RID: 5134
	public float sleepVelocity = 0.4f;

	// Token: 0x0400140F RID: 5135
	public float animationSpeed = 0.2f;

	// Token: 0x04001410 RID: 5136
	public GameObject endOfPathEffect;

	// Token: 0x04001411 RID: 5137
	public Transform enemyAnimation;

	// Token: 0x04001412 RID: 5138
	private GGSingleEnemyLogic GGsingleEnemyLogic;

	// Token: 0x04001413 RID: 5139
	private float idleTime;

	// Token: 0x04001414 RID: 5140
	protected Vector3 lastTarget;
}
