using System;
using Photon;
using UnityEngine;

// Token: 0x0200013E RID: 318
[RequireComponent(typeof(PhotonView))]
public class MoveByKeys : Photon.MonoBehaviour
{
	// Token: 0x0600098E RID: 2446 RVA: 0x0004876B File Offset: 0x00046B6B
	public void Start()
	{
		this.isSprite = (base.GetComponent<SpriteRenderer>() != null);
		this.body2d = base.GetComponent<Rigidbody2D>();
		this.body = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00048798 File Offset: 0x00046B98
	public void FixedUpdate()
	{
		if (!base.photonView.isMine)
		{
			return;
		}
		if (Input.GetAxisRaw("Horizontal") < -0.1f || Input.GetAxisRaw("Horizontal") > 0.1f)
		{
			base.transform.position += Vector3.right * (this.Speed * Time.deltaTime) * Input.GetAxisRaw("Horizontal");
		}
		if (this.jumpingTime <= 0f)
		{
			if ((this.body != null || this.body2d != null) && Input.GetKey(KeyCode.Space))
			{
				this.jumpingTime = this.JumpTimeout;
				Vector2 vector = Vector2.up * this.JumpForce;
				if (this.body2d != null)
				{
					this.body2d.AddForce(vector);
				}
				else if (this.body != null)
				{
					this.body.AddForce(vector);
				}
			}
		}
		else
		{
			this.jumpingTime -= Time.deltaTime;
		}
		if (!this.isSprite && (Input.GetAxisRaw("Vertical") < -0.1f || Input.GetAxisRaw("Vertical") > 0.1f))
		{
			base.transform.position += Vector3.forward * (this.Speed * Time.deltaTime) * Input.GetAxisRaw("Vertical");
		}
	}

	// Token: 0x04000885 RID: 2181
	public float Speed = 10f;

	// Token: 0x04000886 RID: 2182
	public float JumpForce = 200f;

	// Token: 0x04000887 RID: 2183
	public float JumpTimeout = 0.5f;

	// Token: 0x04000888 RID: 2184
	private bool isSprite;

	// Token: 0x04000889 RID: 2185
	private float jumpingTime;

	// Token: 0x0400088A RID: 2186
	private Rigidbody body;

	// Token: 0x0400088B RID: 2187
	private Rigidbody2D body2d;
}
