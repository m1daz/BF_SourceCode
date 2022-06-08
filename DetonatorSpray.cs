using System;
using UnityEngine;

// Token: 0x0200020A RID: 522
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Object Spray")]
public class DetonatorSpray : DetonatorComponent
{
	// Token: 0x06000E48 RID: 3656 RVA: 0x00076FF5 File Offset: 0x000753F5
	public override void Init()
	{
	}

	// Token: 0x06000E49 RID: 3657 RVA: 0x00076FF7 File Offset: 0x000753F7
	private void Update()
	{
		if (this._delayedExplosionStarted)
		{
			this._explodeDelay -= Time.deltaTime;
			if (this._explodeDelay <= 0f)
			{
				this.Explode();
			}
		}
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x0007702C File Offset: 0x0007542C
	public override void Explode()
	{
		if (!this._delayedExplosionStarted)
		{
			this._explodeDelay = this.explodeDelayMin + UnityEngine.Random.value * (this.explodeDelayMax - this.explodeDelayMin);
		}
		if (this._explodeDelay <= 0f)
		{
			int num = (int)(this.detail * (float)this.count);
			for (int i = 0; i < num; i++)
			{
				Vector3 b = UnityEngine.Random.onUnitSphere * (this.startingRadius * this.size);
				Vector3 b2 = new Vector3(this.velocity.x * this.size, this.velocity.y * this.size, this.velocity.z * this.size);
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.sprayObject, base.transform.position + b, base.transform.rotation);
				gameObject.transform.parent = base.transform;
				this._tmpScale = this.minScale + UnityEngine.Random.value * (this.maxScale - this.minScale);
				this._tmpScale *= this.size;
				gameObject.transform.localScale = new Vector3(this._tmpScale, this._tmpScale, this._tmpScale);
				gameObject.GetComponent<Rigidbody>().velocity = Vector3.Scale(b.normalized, b2);
				UnityEngine.Object.Destroy(gameObject, this.duration * this.timeScale);
				this._delayedExplosionStarted = false;
				this._explodeDelay = 0f;
			}
		}
		else
		{
			this._delayedExplosionStarted = true;
		}
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x000771C7 File Offset: 0x000755C7
	public void Reset()
	{
		this.velocity = new Vector3(15f, 15f, 15f);
	}

	// Token: 0x04000F4B RID: 3915
	public GameObject sprayObject;

	// Token: 0x04000F4C RID: 3916
	public int count = 10;

	// Token: 0x04000F4D RID: 3917
	public float startingRadius;

	// Token: 0x04000F4E RID: 3918
	public float minScale = 1f;

	// Token: 0x04000F4F RID: 3919
	public float maxScale = 1f;

	// Token: 0x04000F50 RID: 3920
	private bool _delayedExplosionStarted;

	// Token: 0x04000F51 RID: 3921
	private float _explodeDelay;

	// Token: 0x04000F52 RID: 3922
	private Vector3 _explosionPosition;

	// Token: 0x04000F53 RID: 3923
	private float _tmpScale;
}
