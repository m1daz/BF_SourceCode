using System;
using UnityEngine;

// Token: 0x02000201 RID: 513
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Force")]
public class DetonatorForce : DetonatorComponent
{
	// Token: 0x06000E08 RID: 3592 RVA: 0x00074FA0 File Offset: 0x000733A0
	public void SetDetonatorShooter(string shooterId)
	{
		this.shooter = shooterId;
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x00074FA9 File Offset: 0x000733A9
	public void SetDetonatorDamage(GGDamageEventArgs mDamageEventArgs)
	{
		this.DamageEventArgs = mDamageEventArgs;
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x00074FB2 File Offset: 0x000733B2
	public void ResetRadius(float pluseRadius)
	{
		this.radius *= pluseRadius;
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x00074FC2 File Offset: 0x000733C2
	public void SetShockWeapon(bool isShockWeapon)
	{
		this.shockWeapon = isShockWeapon;
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x00074FCB File Offset: 0x000733CB
	public override void Init()
	{
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x00074FD0 File Offset: 0x000733D0
	private void Awake()
	{
		if (GameObject.FindWithTag("Player") != null)
		{
			this.mainPlayerNetworkCharacter = GameObject.FindWithTag("Player").GetComponent<GGNetworkCharacter>();
		}
		else
		{
			this.shooter = string.Empty;
			this.mainPlayerNetworkCharacter = null;
		}
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x0007501E File Offset: 0x0007341E
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

	// Token: 0x06000E0F RID: 3599 RVA: 0x00075054 File Offset: 0x00073454
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
					float num = UnityEngine.Random.Range(this.MinDamage, this.MaxDamage) * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.ExplosionDamagePlus].additionValue);
					Vector3 a = collider.GetComponent<Collider>().ClosestPointOnBounds(this._explosionPosition);
					float num2 = Vector3.Distance(a, this._explosionPosition);
					short num3 = (short)((1f - Mathf.Clamp01(num2 / this.radius)) * num);
					if (collider.GetComponent<Collider>().gameObject.tag == "SingleModeEnemy")
					{
						if (this.shooter == "player")
						{
							collider.transform.SendMessageUpwards("DamageToSingleEnemy", num3, SendMessageOptions.DontRequireReceiver);
						}
					}
					else if (collider.GetComponent<Collider>().gameObject.tag == "EnemyHeadTag")
					{
						if (this.shooter == "player" && this.mainPlayerNetworkCharacter.mPlayerProperties.team != collider.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team)
						{
							this.DamageEventArgs.damage = num3;
							collider.transform.SendMessageUpwards("PlayerDamaged", this.DamageEventArgs, SendMessageOptions.DontRequireReceiver);
							if (this.shockWeapon)
							{
								int id = collider.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.id;
								GGMessage ggmessage = new GGMessage();
								ggmessage.messageType = GGMessageType.MessagePlayerAutoMove;
								ggmessage.messageContent = new GGMessageContent();
								Vector3 vector = new Vector3(collider.transform.position.x - base.transform.position.x, 0f, collider.transform.position.z - base.transform.position.z);
								Vector3 normalized = vector.normalized;
								ggmessage.messageContent.X = normalized.x * 6f;
								ggmessage.messageContent.Y = 4f;
								ggmessage.messageContent.Z = normalized.z * 6f;
								GGNetworkKit.mInstance.SendMessage(ggmessage, id);
							}
						}
						if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation && collider.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.red)
						{
							GGMutationModeControl.mInstance.totalDamage += (int)num3;
						}
					}
					else if (collider.transform.tag == "Player")
					{
						if (this.shooter == "player")
						{
							this.DamageEventArgs.damage = num3;
							collider.transform.SendMessageUpwards("Event_Damage", this.DamageEventArgs, SendMessageOptions.DontRequireReceiver);
							if (this.shockWeapon)
							{
								Vector3 vector2 = new Vector3(collider.transform.position.x - base.transform.position.x, 0f, collider.transform.position.z - base.transform.position.z);
								Vector3 normalized2 = vector2.normalized;
								collider.transform.SendMessageUpwards("AutoMove", normalized2 * 6f + new Vector3(0f, 4f, 0f), SendMessageOptions.DontRequireReceiver);
							}
						}
						collider.transform.SendMessageUpwards("SinglePlayerDamage", num3, SendMessageOptions.DontRequireReceiver);
					}
					else if (collider.transform.tag == "AI" && this.shooter == "player")
					{
						this.DamageEventArgs.damage = num3;
						UIHuntingModeDirector.mInstance.PopDamageTip((int)this.DamageEventArgs.damage);
						GGNetworkPlayerProperties mPlayerProperties = this.mainPlayerNetworkCharacter.mPlayerProperties;
						mPlayerProperties.damageNum += this.DamageEventArgs.damage;
						collider.transform.SendMessageUpwards("AIDamaged", this.DamageEventArgs, SendMessageOptions.DontRequireReceiver);
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

	// Token: 0x06000E10 RID: 3600 RVA: 0x0007555F File Offset: 0x0007395F
	public void Reset()
	{
		this.radius = this._baseRadius;
		this.power = this._basePower;
	}

	// Token: 0x04000EE5 RID: 3813
	private float _baseRadius = 2f;

	// Token: 0x04000EE6 RID: 3814
	private float _basePower = 160f;

	// Token: 0x04000EE7 RID: 3815
	private float _scaledRange;

	// Token: 0x04000EE8 RID: 3816
	private float _scaledIntensity;

	// Token: 0x04000EE9 RID: 3817
	private bool _delayedExplosionStarted;

	// Token: 0x04000EEA RID: 3818
	private float _explodeDelay;

	// Token: 0x04000EEB RID: 3819
	public float radius = 13f;

	// Token: 0x04000EEC RID: 3820
	public float MinDamage = 85f;

	// Token: 0x04000EED RID: 3821
	public float MaxDamage = 102f;

	// Token: 0x04000EEE RID: 3822
	public float power;

	// Token: 0x04000EEF RID: 3823
	public GameObject fireObject;

	// Token: 0x04000EF0 RID: 3824
	public float fireObjectLife;

	// Token: 0x04000EF1 RID: 3825
	private Collider[] _colliders;

	// Token: 0x04000EF2 RID: 3826
	private GameObject _tempFireObject;

	// Token: 0x04000EF3 RID: 3827
	public bool shockWeapon;

	// Token: 0x04000EF4 RID: 3828
	public string shooter = string.Empty;

	// Token: 0x04000EF5 RID: 3829
	public bool isPluse;

	// Token: 0x04000EF6 RID: 3830
	private GGNetworkCharacter mainPlayerNetworkCharacter;

	// Token: 0x04000EF7 RID: 3831
	public GGDamageEventArgs DamageEventArgs = new GGDamageEventArgs();

	// Token: 0x04000EF8 RID: 3832
	private Vector3 _explosionPosition;
}
