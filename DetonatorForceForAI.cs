using System;
using UnityEngine;

// Token: 0x02000202 RID: 514
[RequireComponent(typeof(Detonator))]
public class DetonatorForceForAI : DetonatorComponent
{
	// Token: 0x06000E12 RID: 3602 RVA: 0x000755D1 File Offset: 0x000739D1
	public void SetDetonatorDamage(GGDamageEventArgs mDamageEventArgs)
	{
		this.DamageEventArgs = mDamageEventArgs;
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x000755DA File Offset: 0x000739DA
	public void ResetRadius(float pluseRadius)
	{
		this.radius *= pluseRadius;
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x000755EA File Offset: 0x000739EA
	public override void Init()
	{
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x000755EC File Offset: 0x000739EC
	private void Awake()
	{
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x000755EE File Offset: 0x000739EE
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

	// Token: 0x06000E17 RID: 3607 RVA: 0x00075624 File Offset: 0x00073A24
	public override void Explode()
	{
		if (!this.on)
		{
			return;
		}
		if (this.detailThreshold > this.detail)
		{
			return;
		}
		if (!this._delayedExplosionStarted)
		{
			this._explodeDelay = this.explodeDelayMin + UnityEngine.Random.value * (this.explodeDelayMax - this.explodeDelayMin);
		}
		if (this._explodeDelay <= 0f)
		{
			this._explosionPosition = base.transform.position;
			this._colliders = Physics.OverlapSphere(this._explosionPosition, this.radius);
			foreach (Collider collider in this._colliders)
			{
				if (collider)
				{
					float num = UnityEngine.Random.Range(this.MinDamage, this.MaxDamage);
					Vector3 a = collider.GetComponent<Collider>().ClosestPointOnBounds(this._explosionPosition);
					float num2 = Vector3.Distance(a, this._explosionPosition);
					short damage = (short)((1f - Mathf.Clamp01(num2 / this.radius)) * num);
					if (collider.transform.tag == "Player")
					{
						this.DamageEventArgs.damage = damage;
						collider.transform.SendMessageUpwards("Event_Damage", this.DamageEventArgs, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			this._delayedExplosionStarted = false;
			this._explodeDelay = 0f;
		}
		else
		{
			this._delayedExplosionStarted = true;
		}
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x0007578A File Offset: 0x00073B8A
	public void Reset()
	{
		this.radius = this._baseRadius;
	}

	// Token: 0x04000EF9 RID: 3833
	private float _baseRadius = 2f;

	// Token: 0x04000EFA RID: 3834
	private float _basePower = 160f;

	// Token: 0x04000EFB RID: 3835
	private float _scaledRange;

	// Token: 0x04000EFC RID: 3836
	private float _scaledIntensity;

	// Token: 0x04000EFD RID: 3837
	private bool _delayedExplosionStarted;

	// Token: 0x04000EFE RID: 3838
	private float _explodeDelay;

	// Token: 0x04000EFF RID: 3839
	public float radius = 5f;

	// Token: 0x04000F00 RID: 3840
	public float MinDamage = 40f;

	// Token: 0x04000F01 RID: 3841
	public float MaxDamage = 50f;

	// Token: 0x04000F02 RID: 3842
	private Collider[] _colliders;

	// Token: 0x04000F03 RID: 3843
	private GameObject _tempFireObject;

	// Token: 0x04000F04 RID: 3844
	public GGDamageEventArgs DamageEventArgs = new GGDamageEventArgs();

	// Token: 0x04000F05 RID: 3845
	private Vector3 _explosionPosition;
}
