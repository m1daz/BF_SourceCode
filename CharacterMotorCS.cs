using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001DE RID: 478
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Character/Character Motor (CSharp)")]
public class CharacterMotorCS : MonoBehaviour
{
	// Token: 0x06000D5D RID: 3421 RVA: 0x0006EADC File Offset: 0x0006CEDC
	private void Awake()
	{
		this.controller = base.GetComponent<CharacterController>();
		this.mNetworkCharacter = base.GetComponent<GGNetworkCharacter>();
		this.mNetworkPlayerLogic = base.GetComponent<GGNetWorkPlayerlogic>();
		this.movingPlatform.enabled = false;
		this.tr = base.transform;
		UIPlayDirector.OnJumpStart += this.OnJumpStart;
		UIPlayDirector.OnJumpEnd += this.OnJumpEnd;
		UIHelpDirector.OnHelpJump += this.OnHelpJump;
		this.footStep = base.transform.Find("Audio/PlayerFoot").GetComponent<AudioSource>();
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0006EB73 File Offset: 0x0006CF73
	private void Start()
	{
		if (Application.loadedLevelName == "UIHelp")
		{
			this.isUIHelp = true;
		}
		if (!this.isUIHelp && GGNetworkKit.mInstance.GetGameMode() == GGModeType.Explosion)
		{
			this.isExplosionMode = true;
		}
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x0006EBB4 File Offset: 0x0006CFB4
	private void UpdateFunction()
	{
		if (!this.isUIHelp && this.isExplosionMode && GGExplosionModeTimerBombLogic.mInstance.cannotControlJoystick)
		{
			return;
		}
		Vector3 vector = this.movement.velocity;
		vector = this.ApplyInputVelocityChange(vector);
		if (!this.isUIHelp && (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Dead || UIPauseDirector.mInstance.isCutControl || !this.canMove))
		{
			vector = Vector3.zero;
		}
		vector = this.ApplyGravityAndJumping(vector);
		Vector3 vector2 = Vector3.zero;
		if (this.MoveWithPlatform())
		{
			Vector3 a = this.movingPlatform.activePlatform.TransformPoint(this.movingPlatform.activeLocalPoint);
			vector2 = a - this.movingPlatform.activeGlobalPoint;
			if (vector2 != Vector3.zero)
			{
				this.controller.Move(vector2);
			}
			Quaternion lhs = this.movingPlatform.activePlatform.rotation * this.movingPlatform.activeLocalRotation;
			float y = (lhs * Quaternion.Inverse(this.movingPlatform.activeGlobalRotation)).eulerAngles.y;
			if (y != 0f)
			{
			}
		}
		Vector3 position = this.tr.position;
		vector.y = Mathf.Min(vector.y, 6.2f);
		Vector3 vector3 = vector * Time.deltaTime;
		float stepOffset = this.controller.stepOffset;
		Vector3 vector4 = new Vector3(vector3.x, 0f, vector3.z);
		float d = Mathf.Max(stepOffset, vector4.magnitude);
		if (this.grounded)
		{
			vector3 -= d * Vector3.up;
		}
		this.movingPlatform.hitPlatform = null;
		this.groundNormal = Vector3.zero;
		this.movement.collisionFlags = this.controller.Move(vector3);
		this.movement.lastHitPoint = this.movement.hitPoint;
		this.lastGroundNormal = this.groundNormal;
		if (this.movingPlatform.enabled && this.movingPlatform.activePlatform != this.movingPlatform.hitPlatform && this.movingPlatform.hitPlatform != null)
		{
			this.movingPlatform.activePlatform = this.movingPlatform.hitPlatform;
			this.movingPlatform.lastMatrix = this.movingPlatform.hitPlatform.localToWorldMatrix;
			this.movingPlatform.newPlatform = true;
		}
		Vector3 vector5 = new Vector3(vector.x, 0f, vector.z);
		this.movement.velocity = (this.tr.position - position) / Time.deltaTime;
		Vector3 lhs2 = new Vector3(this.movement.velocity.x, 0f, this.movement.velocity.z);
		if (vector5 == Vector3.zero)
		{
			this.movement.velocity = new Vector3(0f, this.movement.velocity.y, 0f);
			if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
			{
				this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
			}
		}
		else
		{
			float value = Vector3.Dot(lhs2, vector5) / vector5.sqrMagnitude;
			this.movement.velocity = vector5 * Mathf.Clamp01(value) + this.movement.velocity.y * Vector3.up;
			if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
			{
				this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Walk;
			}
		}
		this.curJumpIncreseVar = GrowthManagerKit.EProperty().allDic[EnchantmentType.JumpPlus].additionValue;
		if (this.preJumpIncreseVar != this.curJumpIncreseVar)
		{
			this.jumping.baseHeight = 1.2f * (1f + this.curJumpIncreseVar);
			this.preJumpIncreseVar = this.curJumpIncreseVar;
		}
		if (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Walk)
		{
			this.walkTime += Time.deltaTime;
			if (this.walkTime >= this.footStepFreq)
			{
				this.footStep.Play();
				this.walkTime = 0f;
			}
		}
		if (this.movement.velocity.y < vector.y - 0.001f)
		{
			if (this.movement.velocity.y < 0f)
			{
				this.movement.velocity.y = vector.y;
			}
			else
			{
				this.jumping.holdingJumpButton = false;
			}
		}
		if (this.grounded && !this.IsGroundedTest())
		{
			this.grounded = false;
			if (this.movingPlatform.enabled && (this.movingPlatform.movementTransfer == CharacterMotorCS.MovementTransferOnJump.InitTransfer || this.movingPlatform.movementTransfer == CharacterMotorCS.MovementTransferOnJump.PermaTransfer))
			{
				this.movement.frameVelocity = this.movingPlatform.platformVelocity;
				this.movement.velocity += this.movingPlatform.platformVelocity;
			}
			base.SendMessage("OnFall", SendMessageOptions.DontRequireReceiver);
			this.tr.position += d * Vector3.up;
		}
		else if (!this.grounded && this.IsGroundedTest())
		{
			this.grounded = true;
			this.jumping.jumping = false;
			this.SubtractNewPlatformVelocity();
			base.SendMessage("OnLand", SendMessageOptions.DontRequireReceiver);
		}
		if (this.MoveWithPlatform())
		{
			this.movingPlatform.activeGlobalPoint = this.tr.position + Vector3.up * (this.controller.center.y - this.controller.height * 0.5f + this.controller.radius);
			this.movingPlatform.activeLocalPoint = this.movingPlatform.activePlatform.InverseTransformPoint(this.movingPlatform.activeGlobalPoint);
			this.movingPlatform.activeGlobalRotation = this.tr.rotation;
			this.movingPlatform.activeLocalRotation = Quaternion.Inverse(this.movingPlatform.activePlatform.rotation) * this.movingPlatform.activeGlobalRotation;
		}
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0006F244 File Offset: 0x0006D644
	private void Update()
	{
		if (this.movingPlatform.enabled)
		{
			if (this.movingPlatform.activePlatform != null)
			{
				if (!this.movingPlatform.newPlatform)
				{
					this.movingPlatform.platformVelocity = (this.movingPlatform.activePlatform.localToWorldMatrix.MultiplyPoint3x4(this.movingPlatform.activeLocalPoint) - this.movingPlatform.lastMatrix.MultiplyPoint3x4(this.movingPlatform.activeLocalPoint)) / Time.deltaTime;
				}
				this.movingPlatform.lastMatrix = this.movingPlatform.activePlatform.localToWorldMatrix;
				this.movingPlatform.newPlatform = false;
			}
			else
			{
				this.movingPlatform.platformVelocity = Vector3.zero;
			}
		}
		this.UpdateFunction();
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0006F324 File Offset: 0x0006D724
	private Vector3 ApplyInputVelocityChange(Vector3 velocity)
	{
		if (!this.canControl)
		{
			this.inputMoveDirection = Vector3.zero;
		}
		Vector3 vector2;
		if (this.grounded && this.TooSteep())
		{
			Vector3 vector = new Vector3(this.groundNormal.x, 0f, this.groundNormal.z);
			vector2 = vector.normalized;
			Vector3 vector3 = Vector3.Project(this.inputMoveDirection, vector2);
			vector2 = vector2 + vector3 * this.sliding.speedControl + (this.inputMoveDirection - vector3) * this.sliding.sidewaysControl;
			vector2 *= this.sliding.slidingSpeed;
		}
		else
		{
			vector2 = this.GetDesiredHorizontalVelocity();
		}
		if (this.movingPlatform.enabled && this.movingPlatform.movementTransfer == CharacterMotorCS.MovementTransferOnJump.PermaTransfer)
		{
			vector2 += this.movement.frameVelocity;
			vector2.y = 0f;
		}
		if (!this.grounded)
		{
			velocity.y = 0f;
		}
		float num = this.GetMaxAcceleration(this.grounded) * Time.deltaTime;
		Vector3 b = vector2 - velocity;
		if (b.sqrMagnitude > num * num)
		{
			b = b.normalized * num;
		}
		if (this.grounded || this.canControl)
		{
			velocity += b;
		}
		if (this.grounded)
		{
			velocity.y = Mathf.Min(velocity.y, 0f);
		}
		return velocity;
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x0006F4C4 File Offset: 0x0006D8C4
	private Vector3 ApplyGravityAndJumping(Vector3 velocity)
	{
		if (!this.inputJump || !this.canControl)
		{
			this.jumping.holdingJumpButton = false;
			this.jumping.lastButtonDownTime = -100f;
		}
		if (this.inputJump && this.jumping.lastButtonDownTime < 0f && this.canControl)
		{
			this.jumping.lastButtonDownTime = Time.time;
		}
		if (this.grounded)
		{
			velocity.y = Mathf.Min(0f, velocity.y) - this.movement.gravity * Time.deltaTime;
		}
		else
		{
			velocity.y = this.movement.velocity.y - this.movement.gravity * Time.deltaTime;
			if (this.jumping.jumping && this.jumping.holdingJumpButton && Time.time < this.jumping.lastStartTime + this.jumping.extraHeight / this.CalculateJumpVerticalSpeed(this.jumping.baseHeight))
			{
				velocity += this.jumping.jumpDir * this.movement.gravity * Time.deltaTime;
			}
			velocity.y = Mathf.Max(velocity.y, -this.movement.maxFallSpeed);
		}
		if (this.grounded)
		{
			if (this.jumping.enabled && this.canControl && (double)(Time.time - this.jumping.lastButtonDownTime) < 0.0001)
			{
				this.grounded = false;
				this.jumping.jumping = true;
				this.jumping.lastStartTime = Time.time;
				this.jumping.lastButtonDownTime = -100f;
				this.jumping.holdingJumpButton = true;
				if (this.TooSteep())
				{
					this.jumping.jumpDir = Vector3.Slerp(Vector3.up, this.groundNormal, this.jumping.steepPerpAmount);
				}
				else
				{
					this.jumping.jumpDir = Vector3.Slerp(Vector3.up, this.groundNormal, this.jumping.perpAmount);
				}
				velocity.y = 0f;
				velocity += this.jumping.jumpDir * this.CalculateJumpVerticalSpeed(this.jumping.baseHeight);
				if (this.movingPlatform.enabled && (this.movingPlatform.movementTransfer == CharacterMotorCS.MovementTransferOnJump.InitTransfer || this.movingPlatform.movementTransfer == CharacterMotorCS.MovementTransferOnJump.PermaTransfer))
				{
					this.movement.frameVelocity = this.movingPlatform.platformVelocity;
					velocity += this.movingPlatform.platformVelocity;
				}
				base.SendMessage("OnJump", SendMessageOptions.DontRequireReceiver);
			}
			else
			{
				this.jumping.holdingJumpButton = false;
			}
		}
		return velocity;
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x0006F7D0 File Offset: 0x0006DBD0
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.normal.y > 0f && hit.normal.y > this.groundNormal.y && hit.moveDirection.y < 0f)
		{
			if ((double)(hit.point - this.movement.lastHitPoint).sqrMagnitude > 0.001 || this.lastGroundNormal == Vector3.zero)
			{
				this.groundNormal = hit.normal;
			}
			else
			{
				this.groundNormal = this.lastGroundNormal;
			}
			this.movingPlatform.hitPlatform = hit.collider.transform;
			this.movement.hitPoint = hit.point;
			this.movement.frameVelocity = Vector3.zero;
		}
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x0006F8C4 File Offset: 0x0006DCC4
	private IEnumerator SubtractNewPlatformVelocity()
	{
		if (this.movingPlatform.enabled && (this.movingPlatform.movementTransfer == CharacterMotorCS.MovementTransferOnJump.InitTransfer || this.movingPlatform.movementTransfer == CharacterMotorCS.MovementTransferOnJump.PermaTransfer))
		{
			if (this.movingPlatform.newPlatform)
			{
				Transform platform = this.movingPlatform.activePlatform;
				yield return new WaitForFixedUpdate();
				yield return new WaitForFixedUpdate();
				if (this.grounded && platform == this.movingPlatform.activePlatform)
				{
					yield break;
				}
			}
			this.movement.velocity -= this.movingPlatform.platformVelocity;
		}
		yield break;
	}

	// Token: 0x06000D65 RID: 3429 RVA: 0x0006F8E0 File Offset: 0x0006DCE0
	private bool MoveWithPlatform()
	{
		return this.movingPlatform.enabled && (this.grounded || this.movingPlatform.movementTransfer == CharacterMotorCS.MovementTransferOnJump.PermaLocked) && this.movingPlatform.activePlatform != null;
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x0006F930 File Offset: 0x0006DD30
	private Vector3 GetDesiredHorizontalVelocity()
	{
		Vector3 vector = this.tr.InverseTransformDirection(this.inputMoveDirection);
		float num = this.MaxSpeedInDirection(vector);
		if (this.grounded)
		{
			float time = Mathf.Asin(this.movement.velocity.normalized.y) * 57.29578f;
			num *= this.movement.slopeSpeedMultiplier.Evaluate(time);
		}
		return this.tr.TransformDirection(vector * num);
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0006F9AC File Offset: 0x0006DDAC
	private Vector3 AdjustGroundVelocityToNormal(Vector3 hVelocity, Vector3 groundNormal)
	{
		Vector3 lhs = Vector3.Cross(Vector3.up, hVelocity);
		return Vector3.Cross(lhs, groundNormal).normalized * hVelocity.magnitude;
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x0006F9E0 File Offset: 0x0006DDE0
	private bool IsGroundedTest()
	{
		return (double)this.groundNormal.y > 0.01;
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0006F9F9 File Offset: 0x0006DDF9
	private float GetMaxAcceleration(bool grounded)
	{
		if (grounded)
		{
			return this.movement.maxGroundAcceleration;
		}
		return this.movement.maxAirAcceleration;
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x0006FA18 File Offset: 0x0006DE18
	private float CalculateJumpVerticalSpeed(float targetJumpHeight)
	{
		return Mathf.Sqrt(2f * targetJumpHeight * this.movement.gravity);
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0006FA32 File Offset: 0x0006DE32
	private bool IsJumping()
	{
		return this.jumping.jumping;
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x0006FA3F File Offset: 0x0006DE3F
	private bool IsSliding()
	{
		return this.grounded && this.sliding.enabled && this.TooSteep();
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x0006FA65 File Offset: 0x0006DE65
	private bool IsTouchingCeiling()
	{
		return (this.movement.collisionFlags & CollisionFlags.Above) != CollisionFlags.None;
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0006FA7A File Offset: 0x0006DE7A
	private bool IsGrounded()
	{
		return this.grounded;
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x0006FA82 File Offset: 0x0006DE82
	private bool TooSteep()
	{
		return this.groundNormal.y <= Mathf.Cos(this.controller.slopeLimit * 0.017453292f);
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x0006FAAA File Offset: 0x0006DEAA
	private Vector3 GetDirection()
	{
		return this.inputMoveDirection;
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0006FAB2 File Offset: 0x0006DEB2
	private void SetControllable(bool controllable)
	{
		this.canControl = controllable;
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x0006FABC File Offset: 0x0006DEBC
	private float MaxSpeedInDirection(Vector3 desiredMovementDirection)
	{
		if (desiredMovementDirection == Vector3.zero)
		{
			return 0f;
		}
		float num = ((desiredMovementDirection.z <= 0f) ? this.movement.maxBackwardsSpeed : this.movement.maxForwardSpeed) / this.movement.maxSidewaysSpeed;
		Vector3 vector = new Vector3(desiredMovementDirection.x, 0f, desiredMovementDirection.z / num);
		Vector3 normalized = vector.normalized;
		Vector3 vector2 = new Vector3(normalized.x, 0f, normalized.z * num);
		return vector2.magnitude * this.movement.maxSidewaysSpeed;
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x0006FB6D File Offset: 0x0006DF6D
	private void SetVelocity(Vector3 velocity)
	{
		this.grounded = false;
		this.movement.velocity = velocity;
		this.movement.frameVelocity = Vector3.zero;
		base.SendMessage("OnExternalVelocity");
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x0006FBA0 File Offset: 0x0006DFA0
	private void OnJumpStart()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (this.mNetworkPlayerLogic.showInstallSchedule)
			{
				this.mNetworkPlayerLogic.OnInstallBtnReleased();
				this.mNetworkPlayerLogic.HideInstallBombButton();
			}
			else if (this.mNetworkPlayerLogic.showRemoveSchedule)
			{
				this.mNetworkPlayerLogic.OnUninstallBtnReleased();
				this.mNetworkPlayerLogic.HideUninstallBombButton();
			}
			this.inputJump = true;
			base.StartCoroutine(this.JumpEnd());
		}
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x0006FC23 File Offset: 0x0006E023
	private void OnHelpJump()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.inputJump = true;
			base.StartCoroutine(this.JumpEnd());
		}
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x0006FC4A File Offset: 0x0006E04A
	private void OnJumpEnd()
	{
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x0006FC4C File Offset: 0x0006E04C
	private IEnumerator JumpEnd()
	{
		yield return new WaitForSeconds(0.0001f);
		this.inputJump = false;
		yield break;
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x0006FC67 File Offset: 0x0006E067
	private void OnDisable()
	{
		UIPlayDirector.OnJumpStart -= this.OnJumpStart;
		UIPlayDirector.OnJumpEnd -= this.OnJumpEnd;
		UIHelpDirector.OnHelpJump -= this.OnHelpJump;
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x0006FC9C File Offset: 0x0006E09C
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "MovingObject")
		{
			this.movingPlatform.enabled = true;
		}
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0006FCC4 File Offset: 0x0006E0C4
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "MovingObject")
		{
			this.movingPlatform.enabled = false;
		}
	}

	// Token: 0x04000D5A RID: 3418
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x04000D5B RID: 3419
	private GGNetWorkPlayerlogic mNetworkPlayerLogic;

	// Token: 0x04000D5C RID: 3420
	public bool canControl = true;

	// Token: 0x04000D5D RID: 3421
	public bool canMove = true;

	// Token: 0x04000D5E RID: 3422
	public bool isExplosionMode;

	// Token: 0x04000D5F RID: 3423
	public bool useFixedUpdate = true;

	// Token: 0x04000D60 RID: 3424
	private AudioSource footStep;

	// Token: 0x04000D61 RID: 3425
	private float walkTime;

	// Token: 0x04000D62 RID: 3426
	private float preJumpIncreseVar;

	// Token: 0x04000D63 RID: 3427
	private float curJumpIncreseVar;

	// Token: 0x04000D64 RID: 3428
	[NonSerialized]
	public Vector3 inputMoveDirection = Vector3.zero;

	// Token: 0x04000D65 RID: 3429
	[NonSerialized]
	public bool inputJump;

	// Token: 0x04000D66 RID: 3430
	public bool inputSprint;

	// Token: 0x04000D67 RID: 3431
	public float footStepFreq = 0.45f;

	// Token: 0x04000D68 RID: 3432
	public CharacterMotorCS.CharacterMotorMovement movement = new CharacterMotorCS.CharacterMotorMovement();

	// Token: 0x04000D69 RID: 3433
	public CharacterMotorCS.CharacterMotorJumping jumping = new CharacterMotorCS.CharacterMotorJumping();

	// Token: 0x04000D6A RID: 3434
	public CharacterMotorCS.CharacterMotorMovingPlatform movingPlatform = new CharacterMotorCS.CharacterMotorMovingPlatform();

	// Token: 0x04000D6B RID: 3435
	public CharacterMotorCS.CharacterMotorSliding sliding = new CharacterMotorCS.CharacterMotorSliding();

	// Token: 0x04000D6C RID: 3436
	[NonSerialized]
	public bool grounded = true;

	// Token: 0x04000D6D RID: 3437
	[NonSerialized]
	public Vector3 groundNormal = Vector3.zero;

	// Token: 0x04000D6E RID: 3438
	private Vector3 lastGroundNormal = Vector3.zero;

	// Token: 0x04000D6F RID: 3439
	private Transform tr;

	// Token: 0x04000D70 RID: 3440
	public CharacterController controller;

	// Token: 0x04000D71 RID: 3441
	private bool isUIHelp;

	// Token: 0x020001DF RID: 479
	[Serializable]
	public class CharacterMotorMovement
	{
		// Token: 0x04000D72 RID: 3442
		public float maxForwardSpeed = 3f;

		// Token: 0x04000D73 RID: 3443
		public float maxSidewaysSpeed = 3f;

		// Token: 0x04000D74 RID: 3444
		public float maxBackwardsSpeed = 3f;

		// Token: 0x04000D75 RID: 3445
		public AnimationCurve slopeSpeedMultiplier = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(-90f, 1f),
			new Keyframe(0f, 1f),
			new Keyframe(90f, 0f)
		});

		// Token: 0x04000D76 RID: 3446
		public float maxGroundAcceleration = 30f;

		// Token: 0x04000D77 RID: 3447
		public float maxAirAcceleration = 20f;

		// Token: 0x04000D78 RID: 3448
		public float gravity = 9.81f;

		// Token: 0x04000D79 RID: 3449
		public float maxFallSpeed = 20f;

		// Token: 0x04000D7A RID: 3450
		[NonSerialized]
		public CollisionFlags collisionFlags;

		// Token: 0x04000D7B RID: 3451
		[NonSerialized]
		public Vector3 velocity;

		// Token: 0x04000D7C RID: 3452
		[NonSerialized]
		public Vector3 frameVelocity = Vector3.zero;

		// Token: 0x04000D7D RID: 3453
		[NonSerialized]
		public Vector3 hitPoint = Vector3.zero;

		// Token: 0x04000D7E RID: 3454
		[NonSerialized]
		public Vector3 lastHitPoint = new Vector3(float.PositiveInfinity, 0f, 0f);
	}

	// Token: 0x020001E0 RID: 480
	public enum MovementTransferOnJump
	{
		// Token: 0x04000D80 RID: 3456
		None,
		// Token: 0x04000D81 RID: 3457
		InitTransfer,
		// Token: 0x04000D82 RID: 3458
		PermaTransfer,
		// Token: 0x04000D83 RID: 3459
		PermaLocked
	}

	// Token: 0x020001E1 RID: 481
	[Serializable]
	public class CharacterMotorJumping
	{
		// Token: 0x04000D84 RID: 3460
		public bool enabled = true;

		// Token: 0x04000D85 RID: 3461
		public float baseHeight = 1f;

		// Token: 0x04000D86 RID: 3462
		public float extraHeight = 4.1f;

		// Token: 0x04000D87 RID: 3463
		public float perpAmount;

		// Token: 0x04000D88 RID: 3464
		public float steepPerpAmount = 0.5f;

		// Token: 0x04000D89 RID: 3465
		[NonSerialized]
		public bool jumping;

		// Token: 0x04000D8A RID: 3466
		[NonSerialized]
		public bool holdingJumpButton;

		// Token: 0x04000D8B RID: 3467
		[NonSerialized]
		public float lastStartTime;

		// Token: 0x04000D8C RID: 3468
		[NonSerialized]
		public float lastButtonDownTime = -100f;

		// Token: 0x04000D8D RID: 3469
		[NonSerialized]
		public Vector3 jumpDir = Vector3.up;
	}

	// Token: 0x020001E2 RID: 482
	[Serializable]
	public class CharacterMotorMovingPlatform
	{
		// Token: 0x04000D8E RID: 3470
		public bool enabled = true;

		// Token: 0x04000D8F RID: 3471
		public CharacterMotorCS.MovementTransferOnJump movementTransfer = CharacterMotorCS.MovementTransferOnJump.PermaTransfer;

		// Token: 0x04000D90 RID: 3472
		[NonSerialized]
		public Transform hitPlatform;

		// Token: 0x04000D91 RID: 3473
		[NonSerialized]
		public Transform activePlatform;

		// Token: 0x04000D92 RID: 3474
		[NonSerialized]
		public Vector3 activeLocalPoint;

		// Token: 0x04000D93 RID: 3475
		[NonSerialized]
		public Vector3 activeGlobalPoint;

		// Token: 0x04000D94 RID: 3476
		[NonSerialized]
		public Quaternion activeLocalRotation;

		// Token: 0x04000D95 RID: 3477
		[NonSerialized]
		public Quaternion activeGlobalRotation;

		// Token: 0x04000D96 RID: 3478
		[NonSerialized]
		public Matrix4x4 lastMatrix;

		// Token: 0x04000D97 RID: 3479
		[NonSerialized]
		public Vector3 platformVelocity;

		// Token: 0x04000D98 RID: 3480
		[NonSerialized]
		public bool newPlatform;
	}

	// Token: 0x020001E3 RID: 483
	[Serializable]
	public class CharacterMotorSliding
	{
		// Token: 0x04000D99 RID: 3481
		public bool enabled = true;

		// Token: 0x04000D9A RID: 3482
		public float slidingSpeed = 15f;

		// Token: 0x04000D9B RID: 3483
		public float sidewaysControl = 1f;

		// Token: 0x04000D9C RID: 3484
		public float speedControl = 0.4f;
	}
}
