using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000273 RID: 627
public class GGSingleModeLadderPlayer : MonoBehaviour
{
	// Token: 0x060011CF RID: 4559 RVA: 0x000A267C File Offset: 0x000A0A7C
	private void Start()
	{
		this.mainCamera = GameObject.FindWithTag("MainCamera");
		this.controller = base.GetComponent<CharacterController>();
		this.landingPads = new ArrayList();
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x000A26A5 File Offset: 0x000A0AA5
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ladder")
		{
			this.LatchLadder(other.gameObject, other);
			this.trigger = true;
			this.ladderName = other.gameObject.name;
		}
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x000A26E1 File Offset: 0x000A0AE1
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Ladder" && this.ladderName == other.gameObject.name)
		{
			this.UnlatchLadder();
			this.trigger = false;
		}
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x000A2720 File Offset: 0x000A0B20
	private void LatchLadder(GameObject latchedLadder, Collider collisionWaypoint)
	{
		this.currentLadder = latchedLadder.GetComponent<GGSingleModeLadder>();
		this.latchedToLadder = true;
		this.climbDirection = this.currentLadder.ClimbDirection();
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x000A2746 File Offset: 0x000A0B46
	private void UnlatchLadder()
	{
		this.latchedToLadder = false;
		this.currentLadder = null;
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x000A2756 File Offset: 0x000A0B56
	private void FixedUpdate()
	{
		if (!this.latchedToLadder)
		{
			return;
		}
		if (Input.GetButton("Jump"))
		{
			this.UnlatchLadder();
			return;
		}
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x000A277C File Offset: 0x000A0B7C
	private void RaycastCheck()
	{
		CharacterController component = base.GetComponent<CharacterController>();
		Vector3 vector = base.transform.position + component.center + Vector3.up * (-component.height * 0.5f);
		Vector3 point = vector + Vector3.up * component.height;
		RaycastHit raycastHit;
		if (!Physics.CapsuleCast(vector, point, component.radius, base.transform.forward, out raycastHit, component.radius))
		{
			this.UnlatchLadder();
			this.trigger = false;
		}
	}

	// Token: 0x040014B5 RID: 5301
	public float climbSpeed = 6f;

	// Token: 0x040014B6 RID: 5302
	public float climbDownThreshold = -0.4f;

	// Token: 0x040014B7 RID: 5303
	public Vector3 climbDirection = Vector3.zero;

	// Token: 0x040014B8 RID: 5304
	private Vector3 lateralMove = Vector3.zero;

	// Token: 0x040014B9 RID: 5305
	private Vector3 forwardMove = Vector3.zero;

	// Token: 0x040014BA RID: 5306
	private Vector3 ladderMovement = Vector3.zero;

	// Token: 0x040014BB RID: 5307
	private GGSingleModeLadder currentLadder;

	// Token: 0x040014BC RID: 5308
	public bool latchedToLadder;

	// Token: 0x040014BD RID: 5309
	public bool inLandingPad;

	// Token: 0x040014BE RID: 5310
	private GameObject mainCamera;

	// Token: 0x040014BF RID: 5311
	private CharacterController controller;

	// Token: 0x040014C0 RID: 5312
	private ArrayList landingPads;

	// Token: 0x040014C1 RID: 5313
	private bool trigger;

	// Token: 0x040014C2 RID: 5314
	private string ladderName = string.Empty;
}
