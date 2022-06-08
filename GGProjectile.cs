using System;
using UnityEngine;

// Token: 0x0200021C RID: 540
public class GGProjectile : MonoBehaviour
{
	// Token: 0x06000E8F RID: 3727 RVA: 0x0007A0C1 File Offset: 0x000784C1
	private void Start()
	{
		if (this.destroyDelay > 0f)
		{
			base.Invoke("Kill", this.destroyDelay);
		}
		else
		{
			base.Invoke("Kill", this.timeOut);
		}
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x0007A0FA File Offset: 0x000784FA
	private void FixedUpdate()
	{
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x0007A0FC File Offset: 0x000784FC
	private void OnCollisionEnter(Collision collision)
	{
		this.contact = collision.contacts[0];
		this.rotation = Quaternion.FromToRotation(Vector3.up, this.contact.normal);
		if (this.destroyDelay > 0f)
		{
			return;
		}
		if (this.killCreateCount == 0)
		{
			this.Kill();
			this.killCreateCount = 1;
		}
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x0007A164 File Offset: 0x00078564
	private void Kill()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.explosion, base.transform.position, this.rotation);
		DetonatorForce component = gameObject.GetComponent<DetonatorForce>();
		if (component != null)
		{
			component.SetDetonatorShooter(this.shooter);
			component.ResetRadius(1f + this.RangeUpgradeValue);
			component.SetDetonatorDamage(new GGDamageEventArgs
			{
				damage = (short)this.DetonatorDamage,
				id = this.mutiplayerId,
				name = this.name,
				team = this.team,
				weaponType = (short)this.weapontype,
				shooterPositionX = this.shooterPositionX,
				shooterPositionY = this.shooterPositionY,
				shooterPositionZ = this.shooterPositionZ
			});
			component.SetShockWeapon(this.isShockWeapon);
		}
		ParticleEmitter componentInChildren = base.GetComponentInChildren<ParticleEmitter>();
		if (componentInChildren)
		{
			componentInChildren.emit = false;
		}
		base.transform.DetachChildren();
		UnityEngine.Object.Destroy(base.gameObject);
		if (this.objectsToDestroy.Length > 0)
		{
			for (int i = 0; i < this.objectsToDestroy.Length; i++)
			{
				UnityEngine.Object.Destroy(this.objectsToDestroy[i]);
			}
		}
	}

	// Token: 0x04000FD8 RID: 4056
	public GameObject explosion;

	// Token: 0x04000FD9 RID: 4057
	public float destroyDelay;

	// Token: 0x04000FDA RID: 4058
	public float timeOut = 3f;

	// Token: 0x04000FDB RID: 4059
	public GameObject[] objectsToDestroy;

	// Token: 0x04000FDC RID: 4060
	private ContactPoint contact;

	// Token: 0x04000FDD RID: 4061
	private Quaternion rotation;

	// Token: 0x04000FDE RID: 4062
	public string shooter = string.Empty;

	// Token: 0x04000FDF RID: 4063
	public float DetonatorDamage;

	// Token: 0x04000FE0 RID: 4064
	public int mutiplayerId = -1;

	// Token: 0x04000FE1 RID: 4065
	public int weapontype;

	// Token: 0x04000FE2 RID: 4066
	public new string name = string.Empty;

	// Token: 0x04000FE3 RID: 4067
	public float shooterPositionX;

	// Token: 0x04000FE4 RID: 4068
	public float shooterPositionY;

	// Token: 0x04000FE5 RID: 4069
	public float shooterPositionZ;

	// Token: 0x04000FE6 RID: 4070
	public GGTeamType team = GGTeamType.Nil;

	// Token: 0x04000FE7 RID: 4071
	public bool isPlused;

	// Token: 0x04000FE8 RID: 4072
	public float RangeUpgradeValue;

	// Token: 0x04000FE9 RID: 4073
	public bool isShockWeapon;

	// Token: 0x04000FEA RID: 4074
	private int killCreateCount;
}
