using System;
using System.Collections;
using UnityEngine;

// Token: 0x020001EA RID: 490
public class GGLadderPlayer : MonoBehaviour
{
	// Token: 0x06000D9B RID: 3483 RVA: 0x00070B08 File Offset: 0x0006EF08
	private void Start()
	{
		this.mainCamera = GameObject.FindWithTag("MainCamera");
		this.controller = base.GetComponent<CharacterController>();
		this.landingPads = new ArrayList();
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x00070B34 File Offset: 0x0006EF34
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.gameObject);
		if (other.tag == "Ladder")
		{
			this.LatchLadder(other.gameObject, other);
			this.trigger = true;
			this.ladderName = other.gameObject.name;
		}
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x00070B86 File Offset: 0x0006EF86
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Ladder" && this.ladderName == other.gameObject.name)
		{
			this.UnlatchLadder();
			this.trigger = false;
		}
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x00070BC5 File Offset: 0x0006EFC5
	private void LatchLadder(GameObject latchedLadder, Collider collisionWaypoint)
	{
		this.currentLadder = latchedLadder.GetComponent<GGLadder>();
		this.latchedToLadder = true;
		this.climbDirection = this.currentLadder.ClimbDirection();
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x00070BEB File Offset: 0x0006EFEB
	private void UnlatchLadder()
	{
		this.latchedToLadder = false;
		this.currentLadder = null;
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x00070BFB File Offset: 0x0006EFFB
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

	// Token: 0x06000DA1 RID: 3489 RVA: 0x00070C20 File Offset: 0x0006F020
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

	// Token: 0x04000DC8 RID: 3528
	public float climbSpeed = 6f;

	// Token: 0x04000DC9 RID: 3529
	public float climbDownThreshold = -0.4f;

	// Token: 0x04000DCA RID: 3530
	public Vector3 climbDirection = Vector3.zero;

	// Token: 0x04000DCB RID: 3531
	private Vector3 lateralMove = Vector3.zero;

	// Token: 0x04000DCC RID: 3532
	private Vector3 forwardMove = Vector3.zero;

	// Token: 0x04000DCD RID: 3533
	private Vector3 ladderMovement = Vector3.zero;

	// Token: 0x04000DCE RID: 3534
	private GGLadder currentLadder;

	// Token: 0x04000DCF RID: 3535
	public bool latchedToLadder;

	// Token: 0x04000DD0 RID: 3536
	public bool inLandingPad;

	// Token: 0x04000DD1 RID: 3537
	private GameObject mainCamera;

	// Token: 0x04000DD2 RID: 3538
	private CharacterController controller;

	// Token: 0x04000DD3 RID: 3539
	private ArrayList landingPads;

	// Token: 0x04000DD4 RID: 3540
	private bool trigger;

	// Token: 0x04000DD5 RID: 3541
	private string ladderName = string.Empty;
}
