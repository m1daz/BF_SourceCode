using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020F RID: 527
public class GGBullet : MonoBehaviour
{
	// Token: 0x06000E5F RID: 3679 RVA: 0x000779A0 File Offset: 0x00075DA0
	private void Start()
	{
		this.newPos = base.transform.position;
		this.oldPos = this.newPos;
		this.velocity = (float)this.speed * base.transform.forward;
		if (GameObject.FindWithTag("Player") != null)
		{
			this.mainPlayerNetworkCharacter = GameObject.FindWithTag("Player").GetComponent<GGNetworkCharacter>();
		}
		else
		{
			this.shooter = string.Empty;
			this.mainPlayerNetworkCharacter = null;
		}
		UnityEngine.Object.Destroy(base.gameObject, this.life);
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x00077A3C File Offset: 0x00075E3C
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
			if (UIUserDataController.GetBulletHole() == 1 && this.impactHoles)
			{
				if (raycastHit.transform.tag == "Metal")
				{
					if (this.impactObjects.Count != 0)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[0], raycastHit.point, quaternion);
					}
				}
				else if (raycastHit.transform.tag == "Concrete")
				{
					if (this.impactObjects.Count != 0)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[1], raycastHit.point, quaternion);
					}
				}
				else if (raycastHit.transform.tag == "Dirt")
				{
					if (this.impactObjects.Count != 0)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[2], raycastHit.point, quaternion);
					}
				}
				else if (raycastHit.transform.tag == "Wood")
				{
					if (this.impactObjects.Count != 0)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[3], raycastHit.point, quaternion);
					}
				}
				else if (raycastHit.transform.tag == "Sand")
				{
					if (this.impactObjects.Count != 0)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[4], raycastHit.point, quaternion);
					}
				}
				else if (raycastHit.transform.tag == "Soft")
				{
					if (this.impactObjects.Count != 0)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[5], raycastHit.point, quaternion);
					}
				}
				else if (raycastHit.transform.tag == "EnemyHeadTag" || raycastHit.transform.tag == "EnemyBodyTag" || raycastHit.transform.tag == "EnemyFootTag" || raycastHit.transform.tag == "EnemyHeadAroundTag")
				{
					if (this.impactObjects.Count != 0 && this.shooter == "player" && this.mainPlayerNetworkCharacter.mPlayerProperties.team != raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[6], raycastHit.point, quaternion);
					}
				}
				else if (raycastHit.transform.tag == "EnemyTag")
				{
					if (this.impactObjects.Count != 0)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[6], raycastHit.point, quaternion);
					}
				}
				else if (raycastHit.transform.tag == "AI" && this.shooter == "player")
				{
					UnityEngine.Object.Instantiate<GameObject>(this.HuntingModeBulletEffect, raycastHit.point, quaternion);
				}
			}
			if (UIUserDataController.GetBulletHole() == 1 && this.knifeHoles)
			{
				if (raycastHit.transform.tag == "Metal" || raycastHit.transform.tag == "Concrete" || raycastHit.transform.tag == "Dirt" || raycastHit.transform.tag == "Wood" || raycastHit.transform.tag == "Sand" || raycastHit.transform.tag == "Soft")
				{
					if (this.impactObjects.Count != 0)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[0], raycastHit.point, quaternion * Quaternion.Euler(0f, 90f, 0f));
					}
				}
				else if (raycastHit.transform.tag == "EnemyHeadTag" || raycastHit.transform.tag == "EnemyBodyTag" || raycastHit.transform.tag == "EnemyFootTag" || raycastHit.transform.tag == "EnemyHeadAroundTag")
				{
					if (this.impactObjects.Count != 0 && this.shooter == "player" && this.mainPlayerNetworkCharacter.mPlayerProperties.team != raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team)
					{
						UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[1], raycastHit.point, quaternion);
					}
				}
				else if (raycastHit.transform.tag == "EnemyTag" && this.impactObjects.Count != 0)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[1], raycastHit.point, quaternion);
				}
			}
			if (this.zombieHandHoles && (raycastHit.transform.tag == "EnemyHeadTag" || raycastHit.transform.tag == "EnemyBodyTag" || raycastHit.transform.tag == "EnemyFootTag" || raycastHit.transform.tag == "EnemyHeadAroundTag") && this.impactObjects.Count != 0 && this.shooter == "player" && this.mainPlayerNetworkCharacter.mPlayerProperties.team != raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.impactObjects[0], raycastHit.point, quaternion);
			}
			if (this.isBurstBulletTrigger && this.impactHoles && this.shooter == "player")
			{
				UnityEngine.Object.Instantiate<GameObject>(this.BurstBulletEffect, raycastHit.point, quaternion);
			}
			if (this.isNightmareTrigger && this.impactHoles && this.shooter == "player")
			{
				UnityEngine.Object.Instantiate<GameObject>(this.NightmareEffect, raycastHit.point, quaternion);
			}
			if (raycastHit.transform.tag == "SingleModeEnemy")
			{
				if (this.shooter == "player")
				{
					raycastHit.transform.SendMessageUpwards("DamageToSingleEnemy", (int)this.bulletDamage, SendMessageOptions.DontRequireReceiver);
				}
			}
			else if (raycastHit.transform.tag == "Player")
			{
				raycastHit.transform.SendMessageUpwards("SinglePlayerDamage", (int)this.bulletDamage, SendMessageOptions.DontRequireReceiver);
			}
			else if (raycastHit.transform.tag == "EnemyHeadTag")
			{
				if (this.shooter == "player" && this.mainPlayerNetworkCharacter.mPlayerProperties.team != raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team)
				{
					GGDamageEventArgs ggdamageEventArgs = new GGDamageEventArgs();
					GGNetworkPlayerProperties mPlayerProperties = this.mainPlayerNetworkCharacter.mPlayerProperties;
					mPlayerProperties.damageNum += (short)(this.bulletDamage * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.DamagePlus].additionValue));
					if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
					{
						ggdamageEventArgs.damage = (short)(this.bulletDamage * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.DamagePlus].additionValue));
						if (raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.red)
						{
							GGMutationModeControl.mInstance.totalDamage += (int)ggdamageEventArgs.damage;
						}
						else if (raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.blue && this.mainPlayerNetworkCharacter.zombieSkinIndex == 4 && UnityEngine.Random.Range(0, 100) < 20)
						{
							GGMessage ggmessage = new GGMessage();
							ggmessage.messageType = GGMessageType.MessageNotifyMutationBlind;
							int ownerId = raycastHit.transform.root.GetComponent<PhotonView>().ownerId;
							GGNetworkKit.mInstance.SendMessage(ggmessage, ownerId);
						}
					}
					else if (this.weapontype != 46 && this.weapontype != 64)
					{
						ggdamageEventArgs.damage = 1000;
					}
					else
					{
						ggdamageEventArgs.damage = (short)(this.bulletDamage * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.DamagePlus].additionValue));
					}
					if (this.isNightmareTrigger)
					{
						GGMessage ggmessage2 = new GGMessage();
						ggmessage2.messageType = GGMessageType.MessageNotifyNightmare;
						int ownerId2 = raycastHit.transform.root.GetComponent<PhotonView>().ownerId;
						GGNetworkKit.mInstance.SendMessage(ggmessage2, ownerId2);
					}
					ggdamageEventArgs.id = this.mutiplayerId;
					ggdamageEventArgs.name = this.name;
					ggdamageEventArgs.team = this.team;
					ggdamageEventArgs.weaponType = (short)this.weapontype;
					ggdamageEventArgs.shooterPositionX = this.shooterPositionX;
					ggdamageEventArgs.shooterPositionY = this.shooterPositionY;
					ggdamageEventArgs.shooterPositionZ = this.shooterPositionZ;
					raycastHit.transform.SendMessageUpwards("PlayerDamaged", ggdamageEventArgs, SendMessageOptions.DontRequireReceiver);
				}
			}
			else if (raycastHit.transform.tag == "EnemyBodyTag")
			{
				if (this.shooter == "player" && this.mainPlayerNetworkCharacter.mPlayerProperties.team != raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team)
				{
					GGDamageEventArgs ggdamageEventArgs2 = new GGDamageEventArgs();
					ggdamageEventArgs2.damage = (short)(this.bulletDamage * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.DamagePlus].additionValue));
					GGNetworkPlayerProperties mPlayerProperties2 = this.mainPlayerNetworkCharacter.mPlayerProperties;
					mPlayerProperties2.damageNum += ggdamageEventArgs2.damage;
					if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
					{
						if (raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.red)
						{
							GGMutationModeControl.mInstance.totalDamage += (int)ggdamageEventArgs2.damage;
						}
						else if (raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.blue && this.mainPlayerNetworkCharacter.zombieSkinIndex == 4 && UnityEngine.Random.Range(0, 100) < 20)
						{
							GGMessage ggmessage3 = new GGMessage();
							ggmessage3.messageType = GGMessageType.MessageNotifyMutationBlind;
							int ownerId3 = raycastHit.transform.root.GetComponent<PhotonView>().ownerId;
							GGNetworkKit.mInstance.SendMessage(ggmessage3, ownerId3);
						}
					}
					if (this.isNightmareTrigger)
					{
						GGMessage ggmessage4 = new GGMessage();
						ggmessage4.messageType = GGMessageType.MessageNotifyNightmare;
						int ownerId4 = raycastHit.transform.root.GetComponent<PhotonView>().ownerId;
						GGNetworkKit.mInstance.SendMessage(ggmessage4, ownerId4);
					}
					ggdamageEventArgs2.id = this.mutiplayerId;
					ggdamageEventArgs2.name = this.name;
					ggdamageEventArgs2.team = this.team;
					ggdamageEventArgs2.weaponType = (short)this.weapontype;
					ggdamageEventArgs2.shooterPositionX = this.shooterPositionX;
					ggdamageEventArgs2.shooterPositionY = this.shooterPositionY;
					ggdamageEventArgs2.shooterPositionZ = this.shooterPositionZ;
					raycastHit.transform.SendMessageUpwards("PlayerDamaged", ggdamageEventArgs2, SendMessageOptions.DontRequireReceiver);
				}
			}
			else if (raycastHit.transform.tag == "EnemyHeadAroundTag")
			{
				if (this.shooter == "player" && this.mainPlayerNetworkCharacter.mPlayerProperties.team != raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team)
				{
					GGDamageEventArgs ggdamageEventArgs3 = new GGDamageEventArgs();
					ggdamageEventArgs3.damage = (short)(this.bulletDamage * (1.25f + GrowthManagerKit.EProperty().allDic[EnchantmentType.DamagePlus].additionValue));
					GGNetworkPlayerProperties mPlayerProperties3 = this.mainPlayerNetworkCharacter.mPlayerProperties;
					mPlayerProperties3.damageNum += ggdamageEventArgs3.damage;
					if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
					{
						if (raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.red)
						{
							GGMutationModeControl.mInstance.totalDamage += (int)ggdamageEventArgs3.damage;
						}
						else if (raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.blue && this.mainPlayerNetworkCharacter.zombieSkinIndex == 4 && UnityEngine.Random.Range(0, 100) < 20)
						{
							GGMessage ggmessage5 = new GGMessage();
							ggmessage5.messageType = GGMessageType.MessageNotifyMutationBlind;
							int ownerId5 = raycastHit.transform.root.GetComponent<PhotonView>().ownerId;
							GGNetworkKit.mInstance.SendMessage(ggmessage5, ownerId5);
						}
					}
					if (this.isNightmareTrigger)
					{
						GGMessage ggmessage6 = new GGMessage();
						ggmessage6.messageType = GGMessageType.MessageNotifyNightmare;
						int ownerId6 = raycastHit.transform.root.GetComponent<PhotonView>().ownerId;
						GGNetworkKit.mInstance.SendMessage(ggmessage6, ownerId6);
					}
					ggdamageEventArgs3.id = this.mutiplayerId;
					ggdamageEventArgs3.name = this.name;
					ggdamageEventArgs3.team = this.team;
					ggdamageEventArgs3.weaponType = (short)this.weapontype;
					ggdamageEventArgs3.shooterPositionX = this.shooterPositionX;
					ggdamageEventArgs3.shooterPositionY = this.shooterPositionY;
					ggdamageEventArgs3.shooterPositionZ = this.shooterPositionZ;
					raycastHit.transform.SendMessageUpwards("PlayerDamaged", ggdamageEventArgs3, SendMessageOptions.DontRequireReceiver);
				}
			}
			else if (raycastHit.transform.tag == "EnemyFootTag")
			{
				if (this.shooter == "player" && this.mainPlayerNetworkCharacter.mPlayerProperties.team != raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team)
				{
					GGDamageEventArgs ggdamageEventArgs4 = new GGDamageEventArgs();
					ggdamageEventArgs4.damage = (short)(this.bulletDamage * 0.7f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.DamagePlus].additionValue));
					GGNetworkPlayerProperties mPlayerProperties4 = this.mainPlayerNetworkCharacter.mPlayerProperties;
					mPlayerProperties4.damageNum += ggdamageEventArgs4.damage;
					if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
					{
						if (raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.red)
						{
							GGMutationModeControl.mInstance.totalDamage += (int)ggdamageEventArgs4.damage;
						}
						else if (raycastHit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.blue && this.mainPlayerNetworkCharacter.zombieSkinIndex == 4 && UnityEngine.Random.Range(0, 100) < 20)
						{
							GGMessage ggmessage7 = new GGMessage();
							ggmessage7.messageType = GGMessageType.MessageNotifyMutationBlind;
							int ownerId7 = raycastHit.transform.root.GetComponent<PhotonView>().ownerId;
							GGNetworkKit.mInstance.SendMessage(ggmessage7, ownerId7);
						}
					}
					if (this.isNightmareTrigger)
					{
						GGMessage ggmessage8 = new GGMessage();
						ggmessage8.messageType = GGMessageType.MessageNotifyNightmare;
						int ownerId8 = raycastHit.transform.root.GetComponent<PhotonView>().ownerId;
						GGNetworkKit.mInstance.SendMessage(ggmessage8, ownerId8);
					}
					ggdamageEventArgs4.id = this.mutiplayerId;
					ggdamageEventArgs4.name = this.name;
					ggdamageEventArgs4.team = this.team;
					ggdamageEventArgs4.weaponType = (short)this.weapontype;
					ggdamageEventArgs4.shooterPositionX = this.shooterPositionX;
					ggdamageEventArgs4.shooterPositionY = this.shooterPositionY;
					ggdamageEventArgs4.shooterPositionZ = this.shooterPositionZ;
					raycastHit.transform.SendMessageUpwards("PlayerDamaged", ggdamageEventArgs4, SendMessageOptions.DontRequireReceiver);
				}
			}
			else if (raycastHit.transform.name == "UIHelpTarget")
			{
				raycastHit.transform.GetComponent<Collider>().enabled = false;
				raycastHit.transform.Find("UIHelpTargetEffect").GetComponent<ParticleSystem>().Stop();
				UIHelpDirector.mInstance.ShotOneBullEye();
			}
			else if (raycastHit.transform.tag == "AI" && this.shooter == "player")
			{
				GGDamageEventArgs ggdamageEventArgs5 = new GGDamageEventArgs();
				ggdamageEventArgs5.damage = (short)(this.bulletDamage * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.DamagePlus].additionValue));
				UIHuntingModeDirector.mInstance.PopDamageTip((int)ggdamageEventArgs5.damage);
				GGNetworkPlayerProperties mPlayerProperties5 = this.mainPlayerNetworkCharacter.mPlayerProperties;
				mPlayerProperties5.damageNum += ggdamageEventArgs5.damage;
				raycastHit.transform.SendMessageUpwards("AIDamaged", ggdamageEventArgs5, SendMessageOptions.DontRequireReceiver);
			}
			UnityEngine.Object.Destroy(base.gameObject, 1f);
		}
		this.oldPos = base.transform.position;
		base.transform.position = this.newPos;
	}

	// Token: 0x04000F72 RID: 3954
	public int speed = 500;

	// Token: 0x04000F73 RID: 3955
	public float life = 3f;

	// Token: 0x04000F74 RID: 3956
	public int impactForce = 10;

	// Token: 0x04000F75 RID: 3957
	public bool impactHoles = true;

	// Token: 0x04000F76 RID: 3958
	public bool knifeHoles = true;

	// Token: 0x04000F77 RID: 3959
	public bool zombieHandHoles;

	// Token: 0x04000F78 RID: 3960
	public bool doDamage;

	// Token: 0x04000F79 RID: 3961
	public List<GameObject> impactObjects;

	// Token: 0x04000F7A RID: 3962
	private Vector3 velocity;

	// Token: 0x04000F7B RID: 3963
	private Vector3 newPos;

	// Token: 0x04000F7C RID: 3964
	private Vector3 oldPos;

	// Token: 0x04000F7D RID: 3965
	private bool hasHit;

	// Token: 0x04000F7E RID: 3966
	private Transform bloodParticleEffect;

	// Token: 0x04000F7F RID: 3967
	public GameObject BurstBulletEffect;

	// Token: 0x04000F80 RID: 3968
	public GameObject NightmareEffect;

	// Token: 0x04000F81 RID: 3969
	public GameObject HuntingModeBulletEffect;

	// Token: 0x04000F82 RID: 3970
	public string shooter = string.Empty;

	// Token: 0x04000F83 RID: 3971
	public float bulletDamage;

	// Token: 0x04000F84 RID: 3972
	public int mutiplayerId = -1;

	// Token: 0x04000F85 RID: 3973
	public int weapontype;

	// Token: 0x04000F86 RID: 3974
	public new string name = string.Empty;

	// Token: 0x04000F87 RID: 3975
	public float shooterPositionX;

	// Token: 0x04000F88 RID: 3976
	public float shooterPositionY;

	// Token: 0x04000F89 RID: 3977
	public float shooterPositionZ;

	// Token: 0x04000F8A RID: 3978
	public GGTeamType team = GGTeamType.Nil;

	// Token: 0x04000F8B RID: 3979
	public bool isBurstBulletTrigger;

	// Token: 0x04000F8C RID: 3980
	public bool isNightmareTrigger;

	// Token: 0x04000F8D RID: 3981
	private GGNetworkCharacter mainPlayerNetworkCharacter;
}
