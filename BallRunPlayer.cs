using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

// Token: 0x0200036D RID: 877
public class BallRunPlayer : MonoBehaviour
{
	// Token: 0x06001B54 RID: 6996 RVA: 0x000DBC4A File Offset: 0x000DA04A
	private void OnEnable()
	{
		EasyTouch.On_SwipeEnd += this.On_SwipeEnd;
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x000DBC5D File Offset: 0x000DA05D
	private void OnDestroy()
	{
		EasyTouch.On_SwipeEnd -= this.On_SwipeEnd;
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x000DBC70 File Offset: 0x000DA070
	private void Start()
	{
		this.characterController = base.GetComponent<CharacterController>();
		this.startPosition = base.transform.position;
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x000DBC90 File Offset: 0x000DA090
	private void Update()
	{
		if (this.start)
		{
			this.moveDirection = base.transform.TransformDirection(Vector3.forward) * 10f * Time.deltaTime;
			this.moveDirection.y = this.moveDirection.y - 9.81f * Time.deltaTime;
			if (this.isJump)
			{
				this.moveDirection.y = 8f;
				this.isJump = false;
			}
			this.characterController.Move(this.moveDirection);
			this.ballModel.Rotate(Vector3.right * 400f * Time.deltaTime);
		}
		if ((double)base.transform.position.y < 0.5)
		{
			this.start = false;
			base.transform.position = this.startPosition;
		}
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x000DBD81 File Offset: 0x000DA181
	private void OnCollision()
	{
		Debug.Log("ok");
	}

	// Token: 0x06001B59 RID: 7001 RVA: 0x000DBD90 File Offset: 0x000DA190
	private void On_SwipeEnd(Gesture gesture)
	{
		if (this.start)
		{
			switch (gesture.swipe)
			{
			case EasyTouch.SwipeDirection.Left:
			case EasyTouch.SwipeDirection.UpLeft:
			case EasyTouch.SwipeDirection.DownLeft:
				base.transform.Rotate(Vector3.up * -90f);
				break;
			case EasyTouch.SwipeDirection.Right:
			case EasyTouch.SwipeDirection.UpRight:
			case EasyTouch.SwipeDirection.DownRight:
				base.transform.Rotate(Vector3.up * 90f);
				break;
			case EasyTouch.SwipeDirection.Up:
				if (this.characterController.isGrounded)
				{
					this.isJump = true;
				}
				break;
			}
		}
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x000DBE36 File Offset: 0x000DA236
	public void StartGame()
	{
		this.start = true;
	}

	// Token: 0x04001D77 RID: 7543
	public Transform ballModel;

	// Token: 0x04001D78 RID: 7544
	private bool start;

	// Token: 0x04001D79 RID: 7545
	private Vector3 moveDirection;

	// Token: 0x04001D7A RID: 7546
	private CharacterController characterController;

	// Token: 0x04001D7B RID: 7547
	private Vector3 startPosition;

	// Token: 0x04001D7C RID: 7548
	private bool isJump;
}
