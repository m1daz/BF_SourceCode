using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000246 RID: 582
public class GGNetWorkMillorPlayerLogic : MonoBehaviour
{
	// Token: 0x06001066 RID: 4198 RVA: 0x0008C2B7 File Offset: 0x0008A6B7
	private void Awake()
	{
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x0008C2BC File Offset: 0x0008A6BC
	private void Start()
	{
		this.isGetName = false;
		this.playerViewiD = base.GetComponent<PhotonView>().viewID;
		this.mNetworkCharacter = base.GetComponent<GGNetworkCharacter>();
		this.animator = base.transform.Find("Player_1_sinkmesh").GetComponent<Animator>();
		if (GameObject.FindWithTag("Player") != null)
		{
			this.mainPlayerNetworkCharacter = GameObject.FindWithTag("Player").GetComponent<GGNetworkCharacter>();
		}
		this.bloodbar = base.transform.Find("Player_1_sinkmesh/BloodBar").gameObject;
		this.nameColor = base.transform.Find("Player_1_sinkmesh/BloodBar/Name").gameObject;
		this.rank = base.transform.Find("Player_1_sinkmesh/BloodBar/Name/Lv").gameObject;
		this.ZombieIcon = base.transform.Find("Player_1_sinkmesh/BloodBar/ZombieIcon").gameObject;
		this.TimerBombIcon = base.transform.Find("Player_1_sinkmesh/BloodBar/TimerBombIcon").gameObject;
		this.TimerBombIcon1 = base.transform.Find("Player_1_sinkmesh/BloodBar/TimerBombIcon1").gameObject;
		this.footStep = base.transform.Find("Audio/PlayerFoot").GetComponent<AudioSource>();
		this.enemyDie = base.transform.Find("Audio/EnemyDieAudio").GetComponent<AudioSource>();
		this.zombieDie = base.transform.Find("Audio/zombieDie").GetComponent<AudioSource>();
		this.zombieMutation = base.transform.Find("Audio/zombieMutation").GetComponent<AudioSource>();
		this.zombieSound = base.transform.Find("Audio/zombieSound").GetComponent<AudioSource>();
		this.installBombSound = base.transform.Find("Audio/InstallBombSound").GetComponent<AudioSource>();
		this.TeamIcon = base.transform.Find("Player_1_sinkmesh/TeamIcon").gameObject;
		this.zombieSoundTimerCount = UnityEngine.Random.Range(20f, 60f);
		this.preState = GGCharacterWalkState.Idle;
		this.WeaponManager_Multiplayer = this.WeaponManagerForMultiplayer.GetComponent<WeaponManagerForMultiplayer>();
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x0008C4C0 File Offset: 0x0008A8C0
	private void Update()
	{
		if (this.mainPlayerNetworkCharacter == null)
		{
			if (Time.frameCount % 16 != 0 || !(GameObject.FindWithTag("Player") != null))
			{
				return;
			}
			this.mainPlayerNetworkCharacter = GameObject.FindWithTag("Player").GetComponent<GGNetworkCharacter>();
		}
		if (!this.isGetName && this.mNetworkCharacter.mPlayerProperties.isDataValid)
		{
			this.nameColor.GetComponent<TextMesh>().text = this.mNetworkCharacter.mPlayerProperties.name;
			this.isGetName = true;
		}
		if (this.WeaponManager_Multiplayer.IsWeaponInstantiateFinish && this.preWeaponType != this.mNetworkCharacter.mWeaponType)
		{
			this.preWeaponType = this.mNetworkCharacter.mWeaponType;
			this.WeaponSwitch(this.mNetworkCharacter.mWeaponType);
		}
		if (this.preWeaponAnimationId != this.mNetworkCharacter.mWeaponType)
		{
			this.preWeaponAnimationId = this.mNetworkCharacter.mWeaponType;
			this.animator.SetInteger("WeaponID", this.mNetworkCharacter.mWeaponType);
		}
		if (this.preGearType != this.mNetworkCharacter.mGearType)
		{
			this.GearSwitch(this.mNetworkCharacter.mGearType);
			this.preGearType = this.mNetworkCharacter.mGearType;
		}
		if (this.preRank != (int)this.mNetworkCharacter.mPlayerProperties.rank)
		{
			this.preRank = (int)this.mNetworkCharacter.mPlayerProperties.rank;
			this.rank.GetComponent<Renderer>().material = new Material(Shader.Find("Particles/Alpha Blended"));
			this.rank.GetComponent<Renderer>().material.name = "RankOfNetworkPlayer" + this.mNetworkCharacter.mPlayerProperties.id.ToString();
			this.rank.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0f);
			this.rank.GetComponent<Renderer>().material.mainTexture = (Resources.Load("UI/Images/RankLogoEx/RankEx_" + this.mNetworkCharacter.mPlayerProperties.rank.ToString(), typeof(Texture)) as Texture);
		}
		if (this.mNetworkCharacter.mPlayerProperties.team != this.mainPlayerNetworkCharacter.mPlayerProperties.team)
		{
			if (!this.nameColor.GetComponent<Renderer>().material.name.Contains(this.nameMaterials[1].name))
			{
				this.nameColor.GetComponent<Renderer>().material = this.nameMaterials[1];
			}
			if (!this.TeamIcon.GetComponent<Renderer>().material.name.Contains(this.teamMaterials[1].name))
			{
				this.TeamIcon.GetComponent<Renderer>().material = this.teamMaterials[1];
			}
		}
		else
		{
			if (!this.nameColor.GetComponent<Renderer>().material.name.Contains(this.nameMaterials[0].name))
			{
				this.nameColor.GetComponent<Renderer>().material = this.nameMaterials[0];
			}
			if (!this.TeamIcon.GetComponent<Renderer>().material.name.Contains(this.teamMaterials[0].name))
			{
				this.TeamIcon.GetComponent<Renderer>().material = this.teamMaterials[0];
			}
		}
		if (this.scanTime > 0f)
		{
			this.scanTime -= Time.deltaTime;
			if (!this.bloodbar.activeSelf)
			{
				this.bloodbar.SetActive(true);
			}
		}
		else if (this.scaned)
		{
			if (this.bloodbar.activeSelf)
			{
				this.bloodbar.SetActive(false);
			}
			this.scaned = false;
		}
		if (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Walk)
		{
			this.walkTime += Time.deltaTime;
			if (this.walkTime >= 0.45f)
			{
				this.footStep.Play();
				this.walkTime = 0f;
			}
		}
		if (this.preState != this.mNetworkCharacter.mCharacterWalkState)
		{
			if (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Dead)
			{
				if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
				{
					if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
					{
						this.zombieDie.Play();
					}
					else
					{
						this.enemyDie.Play();
					}
				}
				else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Explosion)
				{
					for (int i = 0; i < this.BloodBarObj.Length; i++)
					{
						this.BloodBarObj[i].layer = LayerMask.NameToLayer("InvisibleObj");
					}
					this.enemyDie.Play();
				}
				else
				{
					this.enemyDie.Play();
				}
			}
			else if (this.preState == GGCharacterWalkState.Dead && GGNetworkKit.mInstance.GetGameMode() == GGModeType.Explosion)
			{
				for (int j = 0; j < this.BloodBarObj.Length; j++)
				{
					this.BloodBarObj[j].layer = LayerMask.NameToLayer("Default");
				}
			}
			this.preState = this.mNetworkCharacter.mCharacterWalkState;
		}
		if (this.PreRespawn_DamageImmune != this.mNetworkCharacter.mPlayerProperties.DamageImmuneWhenRespawn)
		{
			if (this.mNetworkCharacter.mPlayerProperties.DamageImmuneWhenRespawn)
			{
				this.DamageImmuneEffect.SetActive(true);
			}
			else if (!this.mNetworkCharacter.mPlayerProperties.DamageImmuneWhenRespawn)
			{
				this.DamageImmuneEffect.SetActive(false);
			}
			this.PreRespawn_DamageImmune = this.mNetworkCharacter.mPlayerProperties.DamageImmuneWhenRespawn;
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			this.zombieSoundTimer += Time.deltaTime;
			if (this.zombieSoundTimer > this.zombieSoundTimerCount)
			{
				if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
				{
					this.zombieSound.clip = this.zombieSoundClip[UnityEngine.Random.Range(0, 4)];
					this.zombieSound.Play();
				}
				this.zombieSoundTimer = 0f;
				this.zombieSoundTimerCount = UnityEngine.Random.Range(5f, 60f);
			}
		}
		if (this.preZombieLv != this.mNetworkCharacter.mZombieLv)
		{
			if (this.preZombieLv == 0)
			{
				this.ZombieIcon.GetComponent<Renderer>().enabled = true;
				this.animator.SetBool("zombie", true);
				base.transform.Find("Player_1_sinkmesh/Player").SendMessage("MutationModeSetCapeAndHat", SendMessageOptions.DontRequireReceiver);
				if (this.mNetworkCharacter.mZombieLv == 6)
				{
					this.zombieMutation.Play();
				}
			}
			this.preZombieLv = this.mNetworkCharacter.mZombieLv;
		}
		if (this.preZombieType != this.mNetworkCharacter.zombieSkinIndex)
		{
			if (this.mNetworkCharacter.zombieSkinIndex == 2)
			{
				this.HPGenerateEffectShow();
			}
			base.transform.Find("Player_1_sinkmesh/Player").SendMessage("MutationModeSetSkin", this.mNetworkCharacter.zombieSkinIndex - 1, SendMessageOptions.DontRequireReceiver);
			this.preZombieType = this.mNetworkCharacter.zombieSkinIndex;
		}
		if (this.preCommonProp_AttackEnhance != (int)this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.AttackEnhance)
		{
			if (this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.AttackEnhance == 1)
			{
				this.AddBufferLogo("AttackEnhance");
			}
			else if (this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.AttackEnhance == 0)
			{
				this.RemoveBufferLogo("AttackEnhance");
			}
			this.preCommonProp_AttackEnhance = (int)this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.AttackEnhance;
		}
		if (this.preCommonProp_ArmorEnhance != (int)this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.ArmorEnhance)
		{
			if (this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.ArmorEnhance == 1)
			{
				this.AddBufferLogo("ArmorEnhance");
			}
			else if (this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.ArmorEnhance == 0)
			{
				this.RemoveBufferLogo("ArmorEnhance");
			}
			this.preCommonProp_ArmorEnhance = (int)this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.ArmorEnhance;
		}
		if (this.preCommonProp_SpeedEnhance != (int)this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.SpeedEnhance)
		{
			if (this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.SpeedEnhance == 1)
			{
				this.AddBufferLogo("SpeedEnhance");
			}
			else if (this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.SpeedEnhance == 0)
			{
				this.RemoveBufferLogo("SpeedEnhance");
			}
			this.preCommonProp_SpeedEnhance = (int)this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.SpeedEnhance;
		}
		if (this.preCommonProp_JumpEnhance != (int)this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.JumpEnhance)
		{
			if (this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.JumpEnhance == 1)
			{
				this.AddBufferLogo("JumpEnhance");
			}
			else if (this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.JumpEnhance == 0)
			{
				this.RemoveBufferLogo("JumpEnhance");
			}
			this.preCommonProp_JumpEnhance = (int)this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.JumpEnhance;
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			if (this.preMutationProp_BurstBullet != (int)this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.BurstBullet)
			{
				if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.BurstBullet == 1)
				{
					this.AddBufferLogo("BurstBullet");
				}
				else if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.BurstBullet == 0)
				{
					this.RemoveBufferLogo("BurstBullet");
				}
				this.preMutationProp_BurstBullet = (int)this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.BurstBullet;
			}
			if (this.preMutationProp_DamageImmune != (int)this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.DamageImmune)
			{
				if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.DamageImmune == 1)
				{
					this.AddBufferLogo("DamageImmune");
					this.DamageImmuneEffect.SetActive(true);
				}
				else if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.DamageImmune == 0)
				{
					this.RemoveBufferLogo("DamageImmune");
					this.DamageImmuneEffect.SetActive(false);
				}
				this.preMutationProp_DamageImmune = (int)this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.DamageImmune;
			}
			if (this.preMutationProp_Antivenom != (int)this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.Antivenom)
			{
				if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.Antivenom == 1)
				{
					this.AddBufferLogo("Antivenom");
				}
				else if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.Antivenom == 0)
				{
					this.RemoveBufferLogo("Antivenom");
				}
				this.preMutationProp_Antivenom = (int)this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.Antivenom;
			}
			if (this.preMutationProp_SpeedTrap != (int)this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.SpeedTrap)
			{
				if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.SpeedTrap == 1)
				{
					this.AddBufferLogo("SpeedTrap");
				}
				else if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.SpeedTrap == 0)
				{
					this.RemoveBufferLogo("SpeedTrap");
				}
				this.preMutationProp_SpeedTrap = (int)this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.SpeedTrap;
			}
			if (this.PreDeco_SelfInvisible != (int)this.mNetworkCharacter.mPlayerProperties.DecorationSkill.SelfInvisible || this.preMutationProp_InvisiblePotion != (int)this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.InvisiblePotion)
			{
				if ((this.PreDeco_SelfInvisible == 0 || this.preMutationProp_InvisiblePotion == 0) && (this.mNetworkCharacter.mPlayerProperties.DecorationSkill.SelfInvisible == 1 || this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.InvisiblePotion == 1))
				{
					this.InvisibleEffectShow();
					this.InvisibleEffectSoundPlay();
				}
				if ((this.PreDeco_SelfInvisible == 1 || this.preMutationProp_InvisiblePotion == 1) && (this.mNetworkCharacter.mPlayerProperties.DecorationSkill.SelfInvisible == 0 || this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.InvisiblePotion == 0))
				{
					this.InvisibleEffectDisappear();
				}
				this.PreDeco_SelfInvisible = (int)this.mNetworkCharacter.mPlayerProperties.DecorationSkill.SelfInvisible;
				this.preMutationProp_InvisiblePotion = (int)this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.InvisiblePotion;
			}
			if (this.preZombieSkill_SelfExplosion != (int)this.mNetworkCharacter.mPlayerProperties.MutationSkill.SelfExplosion)
			{
				if (this.mNetworkCharacter.mPlayerProperties.MutationSkill.SelfExplosion == 1)
				{
					this.SelfExplosionEffectShow();
				}
				this.preZombieSkill_SelfExplosion = (int)this.mNetworkCharacter.mPlayerProperties.MutationSkill.SelfExplosion;
			}
			if (this.preZombieSkill_Blind != (int)this.mNetworkCharacter.mPlayerProperties.MutationSkill.Blind)
			{
				if (this.mNetworkCharacter.mPlayerProperties.MutationSkill.Blind == 1)
				{
					this.BlindEffectShow();
				}
				this.preZombieSkill_Blind = (int)this.mNetworkCharacter.mPlayerProperties.MutationSkill.Blind;
			}
			if (this.preZombieSkill_ActiveHorror != (int)this.mNetworkCharacter.mPlayerProperties.MutationSkill.ActiveHorror)
			{
				if (this.mNetworkCharacter.mPlayerProperties.MutationSkill.ActiveHorror == 1)
				{
					this.ActiveHorrorEffectShow();
				}
				else
				{
					this.ActiveHorrorEffectDisappear();
				}
				this.preZombieSkill_ActiveHorror = (int)this.mNetworkCharacter.mPlayerProperties.MutationSkill.ActiveHorror;
			}
			if (this.preZombieSkill_PassiveHorror != (int)this.mNetworkCharacter.mPlayerProperties.MutationSkill.PassiveHorror)
			{
				if (this.mNetworkCharacter.mPlayerProperties.MutationSkill.PassiveHorror == 1)
				{
					this.PassiveHorrorEffectShow();
				}
				else
				{
					this.PassiveHorrorEffectDisappear();
				}
				this.preZombieSkill_PassiveHorror = (int)this.mNetworkCharacter.mPlayerProperties.MutationSkill.PassiveHorror;
			}
		}
		else if (this.PreDeco_SelfInvisible != (int)this.mNetworkCharacter.mPlayerProperties.DecorationSkill.SelfInvisible)
		{
			if (this.mNetworkCharacter.mPlayerProperties.DecorationSkill.SelfInvisible == 1)
			{
				this.InvisibleEffectShow();
				this.InvisibleEffectSoundPlay();
			}
			else
			{
				this.InvisibleEffectDisappear();
			}
			this.PreDeco_SelfInvisible = (int)this.mNetworkCharacter.mPlayerProperties.DecorationSkill.SelfInvisible;
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Explosion && this.pre_isTakeTimerBomb != this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb)
		{
			if (this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb)
			{
				this.TakeTimerBombShow();
			}
			else
			{
				this.TakeTimerBombDisappear();
			}
			this.pre_isTakeTimerBomb = this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb;
		}
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x0008D448 File Offset: 0x0008B848
	private void PlayerDamaged(GGDamageEventArgs mdamageEventArgs)
	{
		GGNetworkKit.mInstance.DamageToPlayer(mdamageEventArgs, base.GetComponent<PhotonView>());
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x0008D45B File Offset: 0x0008B85B
	private void WeaponSwitch(int weaponType)
	{
		this.WeaponManagerForMultiplayer.SendMessage("SwitchWeaponOnline", weaponType, SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x0008D474 File Offset: 0x0008B874
	private void GearSwitch(int gearType)
	{
		if (gearType > 0)
		{
			this.GearManager[gearType - 1].SetActive(true);
			IEnumerator enumerator = this.WeaponManagerForMultiplayer.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					transform.gameObject.layer = LayerMask.NameToLayer("InvisibleObj");
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			Forcefield[] componentsInChildren = this.WeaponManagerForMultiplayer.GetComponentsInChildren<Forcefield>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = LayerMask.NameToLayer("InvisibleObj");
				componentsInChildren[i].transform.parent.gameObject.layer = LayerMask.NameToLayer("InvisibleObj");
			}
			if (this.LeftHandWeaponObj.activeSelf)
			{
				this.LeftHandWeaponObj.layer = LayerMask.NameToLayer("InvisibleObj");
				Forcefield componentInChildren = this.LeftHandWeaponObj.GetComponentInChildren<Forcefield>();
				componentInChildren.gameObject.layer = LayerMask.NameToLayer("InvisibleObj");
			}
			this.installBombSound.Play();
			this.animator.SetInteger("GearID", gearType);
		}
		else
		{
			for (int j = 0; j < this.GearManager.Length; j++)
			{
				this.GearManager[j].SetActive(false);
			}
			IEnumerator enumerator2 = this.WeaponManagerForMultiplayer.transform.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					Transform transform2 = (Transform)obj2;
					transform2.gameObject.layer = LayerMask.NameToLayer("NetworkPlayer");
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
			Forcefield[] componentsInChildren2 = this.WeaponManagerForMultiplayer.GetComponentsInChildren<Forcefield>();
			for (int k = 0; k < componentsInChildren2.Length; k++)
			{
				componentsInChildren2[k].gameObject.layer = LayerMask.NameToLayer("NetworkPlayer");
				componentsInChildren2[k].transform.parent.gameObject.layer = LayerMask.NameToLayer("NetworkPlayer");
			}
			if (this.LeftHandWeaponObj.activeSelf)
			{
				this.LeftHandWeaponObj.layer = LayerMask.NameToLayer("NetworkPlayer");
				Forcefield componentInChildren2 = this.LeftHandWeaponObj.GetComponentInChildren<Forcefield>();
				componentInChildren2.gameObject.layer = LayerMask.NameToLayer("NetworkPlayer");
			}
			this.installBombSound.Stop();
			this.animator.SetInteger("GearID", 0);
			this.animator.SetInteger("WeaponID", this.mNetworkCharacter.mWeaponType);
		}
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x0008D748 File Offset: 0x0008BB48
	private void AddBufferLogo(string bufferName)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.propBufferLogo, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 0f));
		gameObject.name = bufferName;
		for (int i = 0; i < this.propBufferLogoMaterials.Length; i++)
		{
			if (this.propBufferLogoMaterials[i].name == bufferName)
			{
				gameObject.GetComponent<Renderer>().material = this.propBufferLogoMaterials[i];
				break;
			}
		}
		gameObject.transform.parent = this.propBufferLogoParent.transform;
		gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		gameObject.transform.localPosition = new Vector3(1.7f - 1.1f * (float)this.propBufferLogoList.Count, 0f, 0f);
		gameObject.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
		this.propBufferLogoList.Add(gameObject);
	}

	// Token: 0x0600106D RID: 4205 RVA: 0x0008D878 File Offset: 0x0008BC78
	private void RemoveBufferLogo(string bufferName)
	{
		for (int i = 0; i < this.propBufferLogoList.Count; i++)
		{
			if (this.propBufferLogoList[i].name == bufferName)
			{
				UnityEngine.Object.DestroyImmediate(this.propBufferLogoList[i]);
				this.propBufferLogoList.RemoveAt(i);
				break;
			}
		}
		for (int j = 0; j < this.propBufferLogoList.Count; j++)
		{
			this.propBufferLogoList[j].transform.localPosition = new Vector3(1.7f - 1.1f * (float)j, 0f, 0f);
		}
	}

	// Token: 0x0600106E RID: 4206 RVA: 0x0008D92E File Offset: 0x0008BD2E
	private void TakeTimerBombShow()
	{
		this.TimerBombIcon.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x0600106F RID: 4207 RVA: 0x0008D941 File Offset: 0x0008BD41
	private void TakeTimerBombDisappear()
	{
		this.TimerBombIcon.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x0008D954 File Offset: 0x0008BD54
	private void InvisibleEffectShow()
	{
		for (int i = 0; i < this.BloodBarObj.Length; i++)
		{
			this.BloodBarObj[i].layer = LayerMask.NameToLayer("InvisibleObj");
		}
		this.BufferLogoObj.SetActive(false);
		this.TeamIcon.SetActive(false);
		IEnumerator enumerator = this.WeaponManagerForMultiplayer.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (!transform.gameObject.activeSelf)
				{
					transform.gameObject.SetActive(true);
					Transform[] componentsInChildren = transform.gameObject.GetComponentsInChildren<Transform>();
					for (int j = 0; j < componentsInChildren.Length; j++)
					{
						if (!componentsInChildren[j].gameObject.name.Contains("MuzzleFlash"))
						{
							componentsInChildren[j].gameObject.layer = LayerMask.NameToLayer("InvisibleObj");
						}
					}
					transform.gameObject.SetActive(false);
				}
				else
				{
					Transform[] componentsInChildren2 = transform.gameObject.GetComponentsInChildren<Transform>();
					for (int k = 0; k < componentsInChildren2.Length; k++)
					{
						if (!componentsInChildren2[k].gameObject.name.Contains("MuzzleFlash"))
						{
							componentsInChildren2[k].gameObject.layer = LayerMask.NameToLayer("InvisibleObj");
						}
					}
					transform.gameObject.SetActive(false);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		if (this.LeftHandWeaponObj.activeSelf)
		{
			this.LeftHandWeaponObj.layer = LayerMask.NameToLayer("InvisibleObj");
			Forcefield componentInChildren = this.LeftHandWeaponObj.GetComponentInChildren<Forcefield>();
			componentInChildren.gameObject.layer = LayerMask.NameToLayer("InvisibleObj");
		}
		this.HatObj.SetActive(false);
		this.CapeObj.SetActive(false);
		this.LeftShoeObj.SetActive(false);
		this.RightShoeObj.SetActive(false);
		this.SkinObj.layer = LayerMask.NameToLayer("InvisibleObj");
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x0008DB8C File Offset: 0x0008BF8C
	private void InvisibleEffectDisappear()
	{
		for (int i = 0; i < this.BloodBarObj.Length; i++)
		{
			this.BloodBarObj[i].layer = LayerMask.NameToLayer("Default");
		}
		this.BufferLogoObj.SetActive(true);
		this.TeamIcon.SetActive(true);
		IEnumerator enumerator = this.WeaponManagerForMultiplayer.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (!transform.gameObject.activeSelf)
				{
					transform.gameObject.SetActive(true);
					Transform[] componentsInChildren = transform.gameObject.GetComponentsInChildren<Transform>();
					for (int j = 0; j < componentsInChildren.Length; j++)
					{
						if (!componentsInChildren[j].gameObject.name.Contains("MuzzleFlash"))
						{
							componentsInChildren[j].gameObject.layer = LayerMask.NameToLayer("NetworkPlayer");
						}
					}
					transform.gameObject.SetActive(false);
				}
				else
				{
					Transform[] componentsInChildren2 = transform.gameObject.GetComponentsInChildren<Transform>();
					for (int k = 0; k < componentsInChildren2.Length; k++)
					{
						if (!componentsInChildren2[k].gameObject.name.Contains("MuzzleFlash"))
						{
							componentsInChildren2[k].gameObject.layer = LayerMask.NameToLayer("NetworkPlayer");
						}
					}
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		if (this.LeftHandWeaponObj.activeSelf)
		{
			this.LeftHandWeaponObj.layer = LayerMask.NameToLayer("NetworkPlayer");
			Forcefield componentInChildren = this.LeftHandWeaponObj.GetComponentInChildren<Forcefield>();
			componentInChildren.gameObject.layer = LayerMask.NameToLayer("NetworkPlayer");
		}
		this.HatObj.SetActive(true);
		this.CapeObj.SetActive(true);
		this.LeftShoeObj.SetActive(true);
		this.RightShoeObj.SetActive(true);
		this.SkinObj.layer = LayerMask.NameToLayer("NetworkPlayer");
	}

	// Token: 0x06001072 RID: 4210 RVA: 0x0008DDB8 File Offset: 0x0008C1B8
	private void DamageImmuneEffectShow()
	{
		this.DamageImmuneEffect.SetActive(true);
		this.DamageImmuneEffect.GetComponent<ParticleSystem>().Play();
	}

	// Token: 0x06001073 RID: 4211 RVA: 0x0008DDD6 File Offset: 0x0008C1D6
	private void HPGenerateEffectShow()
	{
		this.HPGenerateEffect.SetActive(true);
		this.HPGenerateEffect.GetComponent<ParticleSystem>().Play();
	}

	// Token: 0x06001074 RID: 4212 RVA: 0x0008DDF4 File Offset: 0x0008C1F4
	private void SelfExplosionEffectShow()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.SelfExplosionEffect, base.transform.position - new Vector3(0f, 0.5f, 0f), new Quaternion(0f, 0f, 0f, 0f));
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x0008DE4C File Offset: 0x0008C24C
	private void BlindEffectShow()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.BlindEffect, base.transform.position + new Vector3(0f, 2f, 0f), new Quaternion(0f, 0f, 0f, 0f));
		gameObject.transform.parent = base.transform;
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x0008DEB3 File Offset: 0x0008C2B3
	private void ActiveHorrorEffectShow()
	{
		if (!this.ActiveHorrorEffect.activeSelf)
		{
			this.ActiveHorrorEffect.SetActive(true);
		}
		this.ActiveHorrorEffect.GetComponent<ParticleSystem>().Play();
		this.ActiveHorrorEffectSound.Play();
	}

	// Token: 0x06001077 RID: 4215 RVA: 0x0008DEEC File Offset: 0x0008C2EC
	private void ActiveHorrorEffectDisappear()
	{
		this.ActiveHorrorEffect.GetComponent<ParticleSystem>().Stop();
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x0008DEFE File Offset: 0x0008C2FE
	private void PassiveHorrorEffectShow()
	{
		if (!this.PassiveHorrorEffect.activeSelf)
		{
			this.PassiveHorrorEffect.SetActive(true);
		}
		this.PassiveHorrorEffect.GetComponent<ParticleSystem>().Play();
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x0008DF2C File Offset: 0x0008C32C
	private void PassiveHorrorEffectDisappear()
	{
		this.PassiveHorrorEffect.GetComponent<ParticleSystem>().Stop();
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x0008DF3E File Offset: 0x0008C33E
	private void InvisibleEffectSoundPlay()
	{
		this.InvisibleEffectSound.Play();
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0008DF4B File Offset: 0x0008C34B
	public void BeScaned()
	{
		this.scaned = true;
		this.scanTime = this.scanTimeCount;
	}

	// Token: 0x0400128B RID: 4747
	private int playerViewiD;

	// Token: 0x0400128C RID: 4748
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x0400128D RID: 4749
	private GGNetworkCharacter mainPlayerNetworkCharacter;

	// Token: 0x0400128E RID: 4750
	private int preWeaponType;

	// Token: 0x0400128F RID: 4751
	private int preWeaponAnimationId;

	// Token: 0x04001290 RID: 4752
	private int preGearType;

	// Token: 0x04001291 RID: 4753
	public GameObject WeaponManagerForMultiplayer;

	// Token: 0x04001292 RID: 4754
	private WeaponManagerForMultiplayer WeaponManager_Multiplayer;

	// Token: 0x04001293 RID: 4755
	private Animator animator;

	// Token: 0x04001294 RID: 4756
	private GameObject bloodColor;

	// Token: 0x04001295 RID: 4757
	private GameObject rank;

	// Token: 0x04001296 RID: 4758
	private GameObject ZombieIcon;

	// Token: 0x04001297 RID: 4759
	private GameObject TimerBombIcon;

	// Token: 0x04001298 RID: 4760
	private GameObject TimerBombIcon1;

	// Token: 0x04001299 RID: 4761
	private GameObject nameColor;

	// Token: 0x0400129A RID: 4762
	public Material[] bloodMaterials;

	// Token: 0x0400129B RID: 4763
	public Material[] nameMaterials;

	// Token: 0x0400129C RID: 4764
	private int preRank;

	// Token: 0x0400129D RID: 4765
	private AudioSource footStep;

	// Token: 0x0400129E RID: 4766
	private float walkTime;

	// Token: 0x0400129F RID: 4767
	private GGCharacterWalkState preState;

	// Token: 0x040012A0 RID: 4768
	private AudioSource enemyDie;

	// Token: 0x040012A1 RID: 4769
	private bool isGetName;

	// Token: 0x040012A2 RID: 4770
	private GameObject bloodbar;

	// Token: 0x040012A3 RID: 4771
	private AudioSource zombieDie;

	// Token: 0x040012A4 RID: 4772
	private AudioSource zombieMutation;

	// Token: 0x040012A5 RID: 4773
	private AudioSource zombieSound;

	// Token: 0x040012A6 RID: 4774
	public AudioClip[] zombieSoundClip;

	// Token: 0x040012A7 RID: 4775
	private bool pre_isTakeTimerBomb;

	// Token: 0x040012A8 RID: 4776
	public GameObject[] GearManager;

	// Token: 0x040012A9 RID: 4777
	private AudioSource installBombSound;

	// Token: 0x040012AA RID: 4778
	private int preZombieLv;

	// Token: 0x040012AB RID: 4779
	private int preZombieType;

	// Token: 0x040012AC RID: 4780
	private float zombieSoundTimer;

	// Token: 0x040012AD RID: 4781
	private float zombieSoundTimerCount;

	// Token: 0x040012AE RID: 4782
	private int preCommonProp_HpRecover;

	// Token: 0x040012AF RID: 4783
	private int preCommonProp_AttackEnhance;

	// Token: 0x040012B0 RID: 4784
	private int preCommonProp_ArmorEnhance;

	// Token: 0x040012B1 RID: 4785
	private int preCommonProp_SpeedEnhance;

	// Token: 0x040012B2 RID: 4786
	private int preCommonProp_JumpEnhance;

	// Token: 0x040012B3 RID: 4787
	private int preMutationProp_BurstBullet;

	// Token: 0x040012B4 RID: 4788
	private int preMutationProp_DamageImmune;

	// Token: 0x040012B5 RID: 4789
	private int preMutationProp_Antivenom;

	// Token: 0x040012B6 RID: 4790
	private int preMutationProp_SpeedTrap;

	// Token: 0x040012B7 RID: 4791
	private int preMutationProp_InvisiblePotion;

	// Token: 0x040012B8 RID: 4792
	private int PreDeco_SelfInvisible;

	// Token: 0x040012B9 RID: 4793
	private bool PreRespawn_DamageImmune;

	// Token: 0x040012BA RID: 4794
	public GameObject propBufferLogo;

	// Token: 0x040012BB RID: 4795
	public GameObject propBufferLogoParent;

	// Token: 0x040012BC RID: 4796
	public Material[] propBufferLogoMaterials;

	// Token: 0x040012BD RID: 4797
	public List<GameObject> propBufferLogoList = new List<GameObject>();

	// Token: 0x040012BE RID: 4798
	private int propBufferLogoNum;

	// Token: 0x040012BF RID: 4799
	public GameObject[] BloodBarObj;

	// Token: 0x040012C0 RID: 4800
	public GameObject BufferLogoObj;

	// Token: 0x040012C1 RID: 4801
	public GameObject LeftHandWeaponObj;

	// Token: 0x040012C2 RID: 4802
	public GameObject HatObj;

	// Token: 0x040012C3 RID: 4803
	public GameObject CapeObj;

	// Token: 0x040012C4 RID: 4804
	public GameObject LeftShoeObj;

	// Token: 0x040012C5 RID: 4805
	public GameObject RightShoeObj;

	// Token: 0x040012C6 RID: 4806
	public GameObject SkinObj;

	// Token: 0x040012C7 RID: 4807
	public GameObject HPGenerateEffect;

	// Token: 0x040012C8 RID: 4808
	public GameObject SelfExplosionEffect;

	// Token: 0x040012C9 RID: 4809
	public GameObject BlindEffect;

	// Token: 0x040012CA RID: 4810
	public GameObject ActiveHorrorEffect;

	// Token: 0x040012CB RID: 4811
	public GameObject PassiveHorrorEffect;

	// Token: 0x040012CC RID: 4812
	private int preZombieSkill_SelfExplosion;

	// Token: 0x040012CD RID: 4813
	private int preZombieSkill_Blind;

	// Token: 0x040012CE RID: 4814
	private int preZombieSkill_ActiveHorror;

	// Token: 0x040012CF RID: 4815
	private int preZombieSkill_PassiveHorror;

	// Token: 0x040012D0 RID: 4816
	public GameObject DamageImmuneEffect;

	// Token: 0x040012D1 RID: 4817
	public AudioSource InvisibleEffectSound;

	// Token: 0x040012D2 RID: 4818
	public AudioSource BlindEffectSound;

	// Token: 0x040012D3 RID: 4819
	public AudioSource ActiveHorrorEffectSound;

	// Token: 0x040012D4 RID: 4820
	public bool scaned;

	// Token: 0x040012D5 RID: 4821
	private float scanTime;

	// Token: 0x040012D6 RID: 4822
	private float scanTimeCount = 0.1f;

	// Token: 0x040012D7 RID: 4823
	private GameObject ScanObj;

	// Token: 0x040012D8 RID: 4824
	private GameObject TeamIcon;

	// Token: 0x040012D9 RID: 4825
	public Material[] teamMaterials;
}
