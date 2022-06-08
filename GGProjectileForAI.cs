using System;
using UnityEngine;

// Token: 0x0200021D RID: 541
public class GGProjectileForAI : MonoBehaviour
{
	// Token: 0x06000E94 RID: 3732 RVA: 0x0007A2B5 File Offset: 0x000786B5
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

	// Token: 0x06000E95 RID: 3733 RVA: 0x0007A2EE File Offset: 0x000786EE
	private void FixedUpdate()
	{
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x0007A2F0 File Offset: 0x000786F0
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

	// Token: 0x06000E97 RID: 3735 RVA: 0x0007A358 File Offset: 0x00078758
	private void Kill()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.explosion, base.transform.position, this.rotation);
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

	// Token: 0x04000FEB RID: 4075
	public GameObject explosion;

	// Token: 0x04000FEC RID: 4076
	public float destroyDelay;

	// Token: 0x04000FED RID: 4077
	public float timeOut = 3f;

	// Token: 0x04000FEE RID: 4078
	public GameObject[] objectsToDestroy;

	// Token: 0x04000FEF RID: 4079
	private ContactPoint contact;

	// Token: 0x04000FF0 RID: 4080
	private Quaternion rotation;

	// Token: 0x04000FF1 RID: 4081
	public float DetonatorDamage;

	// Token: 0x04000FF2 RID: 4082
	public bool isPlused;

	// Token: 0x04000FF3 RID: 4083
	public float RangeUpgradeValue;

	// Token: 0x04000FF4 RID: 4084
	private int killCreateCount;
}
