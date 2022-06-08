using System;
using UnityEngine;

// Token: 0x02000210 RID: 528
public class GGBulletForAI : MonoBehaviour
{
	// Token: 0x06000E62 RID: 3682 RVA: 0x00078CD4 File Offset: 0x000770D4
	private void Start()
	{
		this.newPos = base.transform.position;
		this.oldPos = this.newPos;
		this.velocity = (float)this.speed * base.transform.forward;
		UnityEngine.Object.Destroy(base.gameObject, this.life);
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x00078D2C File Offset: 0x0007712C
	private void Update()
	{
		if (this.hasHit)
		{
			return;
		}
		this.newPos += this.velocity * Time.deltaTime * 10f;
		Vector3 direction = this.newPos - this.oldPos;
		float magnitude = direction.magnitude;
		RaycastHit raycastHit;
		if (magnitude > 0f && Physics.Raycast(this.oldPos, direction, out raycastHit, magnitude, 19))
		{
			this.newPos = raycastHit.point;
			this.hasHit = true;
			Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, raycastHit.normal);
			if (raycastHit.transform.tag == "Player")
			{
				GGDamageEventArgs ggdamageEventArgs = new GGDamageEventArgs();
				ggdamageEventArgs.damage = (short)UnityEngine.Random.Range(this.bulletMinDamage, this.bulletMaxDamage);
				raycastHit.transform.SendMessageUpwards("Event_Damage", ggdamageEventArgs, SendMessageOptions.DontRequireReceiver);
			}
			UnityEngine.Object.Destroy(base.gameObject, 1f);
		}
		this.oldPos = base.transform.position;
		base.transform.position = this.newPos;
	}

	// Token: 0x04000F8E RID: 3982
	public int speed = 20;

	// Token: 0x04000F8F RID: 3983
	public float life = 3f;

	// Token: 0x04000F90 RID: 3984
	private Vector3 velocity;

	// Token: 0x04000F91 RID: 3985
	private Vector3 newPos;

	// Token: 0x04000F92 RID: 3986
	private Vector3 oldPos;

	// Token: 0x04000F93 RID: 3987
	private bool hasHit;

	// Token: 0x04000F94 RID: 3988
	public float bulletMinDamage = 5f;

	// Token: 0x04000F95 RID: 3989
	public float bulletMaxDamage = 20f;

	// Token: 0x04000F96 RID: 3990
	public bool isBurstBulletTrigger;
}
