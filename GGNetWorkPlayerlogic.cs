using System;
using System.Collections;
using System.Collections.Generic;
using GrowthSystem;
using UnityEngine;

// Token: 0x0200024B RID: 587
public class GGNetWorkPlayerlogic : MonoBehaviour
{
	// Token: 0x17000153 RID: 339
	// (get) Token: 0x060010AA RID: 4266 RVA: 0x0008F589 File Offset: 0x0008D989
	// (set) Token: 0x060010AB RID: 4267 RVA: 0x0008F591 File Offset: 0x0008D991
	public int HuntingModeMaxDeadCount
	{
		get
		{
			return this._HuntingModeMaxDeadCount;
		}
		set
		{
			GameProtecter.mInstance.SetEncryptVariable(ref this._HuntingModeMaxDeadCount, ref this.StrEncryptHuntingModeMaxDeadCount, value);
		}
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x0008F5AA File Offset: 0x0008D9AA
	public void IgnoreHeadshotTimeCacheChk(float deltaTime)
	{
		if (this.ignoreHeadshotTimeCache > 0f)
		{
			this.ignoreHeadshotTimeCache -= deltaTime;
		}
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x0008F5CC File Offset: 0x0008D9CC
	private void Awake()
	{
		this.mNetworkCharacter = base.GetComponent<GGNetworkCharacter>();
		this.mCharacterMotorCS = base.GetComponent<CharacterMotorCS>();
		this.mNetworkCharacter.mBlood = 100;
		this.killedNum = 0;
		this.killNumNoDie = 0;
		this.lastDateTime = DateTime.Now;
		this.audioEnemyDie = base.transform.Find("Audio/EnemyDieAudio").GetComponent<AudioSource>();
		this.audioBulletHit = base.transform.Find("Audio/BulletHitAudio").GetComponent<AudioSource>();
		this.headShotKill = base.transform.Find("Audio/headshotKill").GetComponent<AudioSource>();
		this.timerKill = base.transform.Find("Audio/TimerKill").GetComponent<AudioSource>();
		this.zombieDie = base.transform.Find("Audio/zombieDie").GetComponent<AudioSource>();
		this.zombieMutation = base.transform.Find("Audio/zombieMutation").GetComponent<AudioSource>();
		this.installBombSound = base.transform.Find("Audio/InstallBombSound").GetComponent<AudioSource>();
		this.uninstallBombSound = base.transform.Find("Audio/UninstallBombSound").GetComponent<AudioSource>();
		this.RedTeamWin = base.transform.Find("Audio/RedTeamWin").GetComponent<AudioSource>();
		this.BlueTeamWin = base.transform.Find("Audio/BlueTeamWin").GetComponent<AudioSource>();
		this.EarthShakeEffectSound = base.transform.Find("Audio/EarthShakeEffectSound").GetComponent<AudioSource>();
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0008F740 File Offset: 0x0008DB40
	private void OnDestroy()
	{
		base.StopAllCoroutines();
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x0008F748 File Offset: 0x0008DB48
	private void Start()
	{
		if (GGNetworkKit.mInstance.GetPlayMode() == GGPlayModeType.Entertainment)
		{
			GrowthManagerKit.InitEProperty();
		}
		else
		{
			GrowthManagerKit.ClearAllEProperty();
		}
		this.InitEvent();
		this.mainCamera = base.transform.Find("LookObject/Main Camera").transform;
		this.weaponCamera = base.transform.Find("LookObject/Main Camera/Weapon Camera").transform;
		this.PhotonGame = GameObject.Find("PhotonGame");
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		if (base.transform.tag == "Player")
		{
			this.CrosshairSprite = UIPlayDirector.mInstance.crosshairObj.GetComponent<UISprite>();
			this.PlayerBloodShow = UIPlayDirector.mInstance.bloodEffectObj;
			this.PlayerBloodShow.SetActive(false);
			this.FlashBombShow = UIPlayDirector.mInstance.flashBombEffectObj;
			if (this.FlashBombShow.activeSelf)
			{
				this.FlashBombShow.SetActive(false);
			}
			this.BlindEffectShow = UIPlayDirector.mInstance.blindEffectObj;
			if (this.BlindEffectShow.activeSelf)
			{
				this.BlindEffectShow.SetActive(false);
			}
			this.NightmareEffectShow = UIPlayDirector.mInstance.NightmareEffectObj;
			if (this.NightmareEffectShow.activeSelf)
			{
				this.NightmareEffectShow.SetActive(false);
			}
			if (Application.loadedLevelName == "MGameScene_14")
			{
				this.IsSnowFallEffectShow = true;
			}
			if (this.IsSnowFallEffectShow)
			{
				this.SnowFallEffect.SetActive(true);
			}
			this.mWeaponManager = base.transform.Find("LookObject/Main Camera/Weapon Camera/WeaponManager").GetComponent<GGWeaponManager>();
			this.ScheduleSprite = UIPlayDirector.mInstance.explosionProgressBarObj.GetComponent<UISprite>();
			this.InstallBombButtonObj = UIPlayDirector.mInstance.installBombBtn;
			this.UninstallBombButtonObj = UIPlayDirector.mInstance.unInstallBombBtn;
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			base.StartCoroutine(this.MutationModePlayerJoinAfterStart());
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Explosion)
		{
			this.mNetworkCharacter.mPlayerProperties.isObserver = true;
			if (this.ObserverCamera != null)
			{
				this.ObserverCamera.SetActive(false);
			}
			UIModeDirector.mInstance.HideCarryBombLogo();
			this.HideInstallBombButton();
			this.showInstallSchedule = false;
			this.HideUninstallBombButton();
			this.showRemoveSchedule = false;
			this.ScheduleSprite.fillAmount = 0f;
			UIModeDirector.mInstance.HideObserverNode();
			UIModeDirector.mInstance.HideExplosionCurRoundLabel();
			UIModeDirector.mInstance.HideTeamWinSpriteTip();
			UIModeDirector.mInstance.RefreshObserverNode(this.mNetworkCharacter);
			this.mNetworkCharacter.isNeedSyn = true;
		}
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0008F9F0 File Offset: 0x0008DDF0
	private void Update()
	{
		if (this.killTimer > 0f)
		{
			this.killTimer -= Time.deltaTime;
		}
		else
		{
			this.TimerkillNum = 0;
		}
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead && Physics.Raycast(this.mainCamera.position, this.mainCamera.forward, out this.hit))
		{
			if (this.hit.collider.gameObject.tag == "EnemyBodyTag" || this.hit.collider.gameObject.tag == "EnemyHeadTag" || this.hit.collider.gameObject.tag == "EnemyFootTag" || this.hit.collider.gameObject.tag == "EnemyHeadAroundTag")
			{
				if (this.CrosshairSprite.color != Color.red)
				{
					if (this.mNetworkCharacter.mPlayerProperties.team != this.hit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team)
					{
						this.CrosshairSprite.color = Color.red;
					}
				}
				else if (this.mNetworkCharacter.mPlayerProperties.team == this.hit.transform.root.GetComponent<GGNetworkCharacter>().mPlayerProperties.team)
				{
					this.CrosshairSprite.color = Color.white;
				}
				this.hit.transform.root.GetComponent<GGNetWorkMillorPlayerLogic>().BeScaned();
			}
			else if (this.CrosshairSprite.color != Color.white)
			{
				this.CrosshairSprite.color = Color.white;
			}
		}
		this.IgnoreHeadshotTimeCacheChk(Time.deltaTime);
		this.curBloodIncreseVar = GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue;
		if (this.preBloodIncreseVar != this.curBloodIncreseVar)
		{
			this.mNetworkCharacter.mBlood = (int)((float)this.mNetworkCharacter.mBlood * (1f + this.curBloodIncreseVar) / (1f + this.preBloodIncreseVar));
			this.preBloodIncreseVar = this.curBloodIncreseVar;
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
		{
			if (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Idle)
			{
				this.MutationModeRecoveryBloodTime += Time.deltaTime;
				if (this.MutationModeRecoveryBloodTime >= 1f)
				{
					this.mNetworkCharacter.mBlood += GGMutationModeControl.ZombieProperty.GetbloodRecoverSpeedWithLv(GGMutationModeControl.mInstance.zombielv);
					this.mNetworkCharacter.mBlood = Math.Min(this.mNetworkCharacter.mBlood, GGMutationModeControl.ZombieProperty.GetMaxBloodWithLv(GGMutationModeControl.mInstance.zombielv));
					this.MutationModeRecoveryBloodTime = 0f;
				}
			}
			if ((this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Idle || this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Walk) && this.mNetworkCharacter.zombieSkinIndex == 2)
			{
				this.MutationModeRecoveryBloodTimeForHPRecoverSkill += Time.deltaTime;
				if (this.MutationModeRecoveryBloodTimeForHPRecoverSkill >= 1f)
				{
					this.mNetworkCharacter.mBlood += GGMutationModeControl.ZombieProperty.GetbloodRecoverSpeedWithLv(GGMutationModeControl.mInstance.zombielv) * 2;
					this.mNetworkCharacter.mBlood = Math.Min(this.mNetworkCharacter.mBlood, GGMutationModeControl.ZombieProperty.GetMaxBloodWithLv(GGMutationModeControl.mInstance.zombielv));
					this.MutationModeRecoveryBloodTimeForHPRecoverSkill = 0f;
				}
			}
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue && this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.MutationModeHumanRecoveryBloodTime += Time.deltaTime;
			if (this.MutationModeHumanRecoveryBloodTime >= 1f)
			{
				this.mNetworkCharacter.mBlood++;
				this.mNetworkCharacter.mBlood = Math.Min(this.mNetworkCharacter.mBlood, (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue)));
				this.MutationModeHumanRecoveryBloodTime = 0f;
			}
		}
		if (this.SpeedRecoverTime > 0f)
		{
			this.SpeedRecoverTime -= Time.deltaTime;
		}
		else if (this.SpeedRecoverTime < 0f)
		{
			this.SpeedRecoverTime = 0f;
			if (!this.IsSpeedTrapTrigger)
			{
				this.mWeaponManager.ChangeMoveSpeed(7f);
			}
		}
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead && GrowthManagerKit.EProperty().allDic[EnchantmentType.HpRecovery].isEnabled)
		{
			this.RecoveryBloodTime += Time.deltaTime;
			if (this.RecoveryBloodTime >= 1f)
			{
				float num = 100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue);
				this.mNetworkCharacter.mBlood += (int)(num * GrowthManagerKit.EProperty().allDic[EnchantmentType.HpRecovery].additionValue / GrowthManagerKit.EProperty().allDic[EnchantmentType.HpRecovery].triggerInterval);
				this.mNetworkCharacter.mBlood = Math.Min(this.mNetworkCharacter.mBlood, (int)num);
				this.RecoveryBloodTime = 0f;
			}
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting && this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead && this.isAcidRainEffect)
		{
			this.HuntingModeAcidRainTime += Time.deltaTime;
			if (this.HuntingModeAcidRainTime >= 2f)
			{
				if (this.mNetworkCharacter.mBlood > 0)
				{
					this.Event_Damage(new GGDamageEventArgs
					{
						damage = 4
					});
				}
				this.HuntingModeAcidRainTime = 0f;
			}
		}
		if (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Dead)
		{
			if (UIPauseDirector.mInstance != null)
			{
				UIPauseDirector.mInstance.isDead = true;
			}
		}
		else if (UIPauseDirector.mInstance != null)
		{
			UIPauseDirector.mInstance.isDead = false;
		}
		if (this.startDisappear)
		{
			if (this.FlashBombShow.GetComponent<UITexture>().alpha > 0.05f)
			{
				this.FlashBombShow.GetComponent<UITexture>().alpha -= Time.deltaTime * 0.5f;
			}
			else
			{
				this.startDisappear = false;
				this.FlashBombShow.SetActive(false);
			}
		}
		if (this.blindStartDisappear)
		{
			if (this.BlindEffectShow.GetComponent<UITexture>().alpha > 0.05f)
			{
				this.BlindEffectShow.GetComponent<UITexture>().alpha -= Time.deltaTime * 0.5f;
			}
			else
			{
				this.blindStartDisappear = false;
				this.BlindEffectShow.SetActive(false);
			}
		}
		if (this.nightmareStartDisappear)
		{
			if (this.NightmareEffectShow.GetComponent<UITexture>().alpha > 0.05f)
			{
				this.NightmareEffectShow.GetComponent<UITexture>().alpha -= Time.deltaTime * 1.5f;
			}
			else
			{
				this.nightmareStartDisappear = false;
				this.NightmareEffectShow.SetActive(false);
			}
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Explosion)
		{
			if (this.showInstallSchedule)
			{
				if (!this.mGlobalInfo.modeInfo.singleRoundResultCalc)
				{
					this.ScheduleSprite.fillAmount += Time.deltaTime * 0.33f;
				}
				if (this.ScheduleSprite.fillAmount >= 1f)
				{
					this.showInstallSchedule = false;
					this.ScheduleSprite.fillAmount = 0f;
					this.SendTimerBombInstallMessage(this.mNetworkCharacter.mPlayerProperties.id, this.timerBombPositionIndex);
					GGNetworkPlayerProperties mPlayerProperties = this.mNetworkCharacter.mPlayerProperties;
					mPlayerProperties.installbombNum += 1;
					this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb = false;
					UIModeDirector.mInstance.HideCarryBombLogo();
					this.HideInstallBombButton();
					this.OnInstallBtnReleased();
				}
			}
			if (this.showRemoveSchedule)
			{
				if (!this.mGlobalInfo.modeInfo.singleRoundResultCalc)
				{
					this.ScheduleSprite.fillAmount += Time.deltaTime * 0.2f;
				}
				if (this.ScheduleSprite.fillAmount >= 1f)
				{
					this.showRemoveSchedule = false;
					this.ScheduleSprite.fillAmount = 0f;
					this.SendTimerBombRemoveMessage(this.mNetworkCharacter.mPlayerProperties.id);
					GGNetworkPlayerProperties mPlayerProperties2 = this.mNetworkCharacter.mPlayerProperties;
					mPlayerProperties2.unInstallbombNum += 1;
					this.HideUninstallBombButton();
					this.OnUninstallBtnReleased();
				}
			}
			if (this.explosionModeHasStart && this.mNetworkCharacter.mPlayerProperties.isObserver && this.ObserverCamera == null)
			{
				base.StartCoroutine(this.ExplosionModeChangeToObserver(0f));
			}
			this.RecordTimerBombPostion();
		}
		if (this.isAutoMove)
		{
			this.mCharacterMotorCS.controller.Move(this.AutoMovement * Time.deltaTime);
			this.AutoMoveTimer += Time.deltaTime;
			if (this.AutoMoveTimer > 0.5f)
			{
				this.isAutoMove = false;
				this.mCharacterMotorCS.canMove = true;
				if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting)
				{
					GrowthManagerKit.AddCustomEProperty(SceneEnchantmentProps.HCustomSpeedDown, -0.88f, 4f);
				}
				else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
				{
					GrowthManagerKit.AddCustomEProperty(SceneEnchantmentProps.HCustomSpeedDown, -0.7f, 2f);
				}
				else
				{
					GrowthManagerKit.AddCustomEProperty(SceneEnchantmentProps.HCustomSpeedDown, -0.5f, 1f);
				}
				this.AutoMoveTimer = 0f;
				this.AutoMovement = Vector3.zero;
			}
		}
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x0009041C File Offset: 0x0008E81C
	private void RecordTimerBombPostion()
	{
		this.TimerBombSynPositionTimeCount += Time.deltaTime;
		if (this.TimerBombSynPositionTimeCount > 1f)
		{
			if (this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb)
			{
				this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
				if (this.mGlobalInfo.modeInfo.IsStartExplosion && !this.mGlobalInfo.modeInfo.activeTimerBomb)
				{
					this.SendTimerBombSynPositionMessage();
				}
			}
			this.TimerBombSynPositionTimeCount = 0f;
		}
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x000904AB File Offset: 0x0008E8AB
	public void playPlayerDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x000904B8 File Offset: 0x0008E8B8
	public void playBulletHitAudio()
	{
		this.audioBulletHit.Play();
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x000904C8 File Offset: 0x0008E8C8
	private IEnumerator waitForGeneratePlayer(int seconds)
	{
		for (int ss = seconds; ss > 0; ss--)
		{
			yield return new WaitForSeconds(1f);
			if (this.quickRespawnTag)
			{
				this.quickRespawnTag = false;
				break;
			}
		}
		this.SetGrayscaleEffectDisappear();
		this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
		this.mainCamera.rotation = new Quaternion(0f, 0f, 0f, 0f);
		this.mainCamera.GetComponent<Camera>().fieldOfView = 47f;
		this.weaponCamera.GetComponent<Camera>().cullingMask = 256;
		this.mWeaponManager.SetLaserR7_LaserScope(true);
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			this.MutationModeRandomPosition();
			if (this.IsDeathTriggerInMutationMode)
			{
				this.DeathTriggerCount++;
				if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
				{
					GGMutationModeControl.mInstance.zombielv = Mathf.Max(GGMutationModeControl.mInstance.zombielv - 1, 1);
					this.Mutation(GGMutationModeControl.mInstance.zombielv);
				}
				else
				{
					int num = UnityEngine.Random.Range(0, 10);
					if (this.DeathTriggerCount < 6 && num < this.DeathTriggerCount)
					{
						this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
					}
					else
					{
						GGMutationModeControl.mInstance.zombielv = 5;
						this.Mutation(GGMutationModeControl.mInstance.zombielv);
					}
				}
				this.IsDeathTriggerInMutationMode = false;
			}
			else if (this.IsKilledInMutationMode)
			{
				if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
				{
					GGMutationModeControl.mInstance.zombielv = Mathf.Max(GGMutationModeControl.mInstance.zombielv - 1, 1);
					this.Mutation(GGMutationModeControl.mInstance.zombielv);
				}
				else if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.Antivenom == 1)
				{
					this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
				}
				else
				{
					GGMutationModeControl.mInstance.zombielv = 5;
					this.Mutation(GGMutationModeControl.mInstance.zombielv);
				}
				this.IsKilledInMutationMode = false;
			}
			else if (this.IsKilledByBombInMutaionMode)
			{
				this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
				this.IsKilledByBombInMutaionMode = false;
			}
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.StrongHold)
		{
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
			this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
			if (this.StrongholdInArea1)
			{
				if (this.mGlobalInfo.modeInfo.mStronghold1State == GGStrondholdState.BlueOccupation)
				{
					if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
					{
						this.BlueRandomPositionInStongholdArea(1);
					}
					else
					{
						this.RandomPosition();
					}
				}
				else if (this.mGlobalInfo.modeInfo.mStronghold1State == GGStrondholdState.RedOccupation)
				{
					if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
					{
						this.RedRandomPositionInStongholdArea(1);
					}
					else
					{
						this.RandomPosition();
					}
				}
				else
				{
					this.RandomPosition();
				}
			}
			else if (this.StrongholdInArea2)
			{
				if (this.mGlobalInfo.modeInfo.mStronghold2State == GGStrondholdState.BlueOccupation)
				{
					if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
					{
						this.BlueRandomPositionInStongholdArea(2);
					}
					else
					{
						this.RandomPosition();
					}
				}
				else if (this.mGlobalInfo.modeInfo.mStronghold2State == GGStrondholdState.RedOccupation)
				{
					if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
					{
						this.RedRandomPositionInStongholdArea(2);
					}
					else
					{
						this.RandomPosition();
					}
				}
				else
				{
					this.RandomPosition();
				}
			}
			else if (this.StrongholdInArea3)
			{
				if (this.mGlobalInfo.modeInfo.mStronghold3State == GGStrondholdState.BlueOccupation)
				{
					if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
					{
						this.BlueRandomPositionInStongholdArea(3);
					}
					else
					{
						this.RandomPosition();
					}
				}
				else if (this.mGlobalInfo.modeInfo.mStronghold3State == GGStrondholdState.RedOccupation)
				{
					if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
					{
						this.RedRandomPositionInStongholdArea(3);
					}
					else
					{
						this.RandomPosition();
					}
				}
				else
				{
					this.RandomPosition();
				}
			}
			else
			{
				this.RandomPosition();
			}
			this.StrongholdInArea1 = false;
			this.StrongholdInArea2 = false;
			this.StrongholdInArea3 = false;
			base.StartCoroutine(this.DamageImmuneWhenRespawn());
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting)
		{
			this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
			this.HuntingModeRandomPosition();
			base.StartCoroutine(this.DamageImmuneWhenRespawn());
		}
		else
		{
			this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
			this.RandomPosition();
			base.StartCoroutine(this.DamageImmuneWhenRespawn());
		}
		this.ClearAllBufferOfPropAfterDie();
		yield break;
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x000904EC File Offset: 0x0008E8EC
	private IEnumerator HuntingModeWaitForRespawn(int seconds)
	{
		this.HuntingModeDeadCount++;
		yield return new WaitForSeconds((float)seconds);
		this.HuntingModeRandomPositionInWaitingRoom();
		int respawnTimer = 5;
		int respawnPrice = 5;
		if (this.HuntingModeDeadCount == 1)
		{
			respawnTimer = 5;
			respawnPrice = 3;
		}
		else if (this.HuntingModeDeadCount == 2)
		{
			respawnTimer = 10;
			respawnPrice = 5;
		}
		else if (this.HuntingModeDeadCount == 3)
		{
			respawnTimer = 20;
			respawnPrice = 10;
		}
		else if (this.HuntingModeDeadCount == 4)
		{
			respawnTimer = 30;
			respawnPrice = 20;
		}
		else if (this.HuntingModeDeadCount == 5)
		{
			respawnTimer = 30;
			respawnPrice = 30;
		}
		else if (this.HuntingModeDeadCount >= 6)
		{
			respawnTimer = 30;
			respawnPrice = 50;
		}
		if (this.HuntingModeDeadCount <= this.HuntingModeMaxDeadCount)
		{
			UIHuntingModeDirector.mInstance.ShowRespawnNode(respawnTimer, respawnPrice);
			base.StartCoroutine(this.waitForGeneratePlayer(respawnTimer));
		}
		else if (GGNetWorkAIDifficultyControl.mInstance.maxPlayerSet == 1)
		{
			this.mGlobalInfo.modeInfo.HuntingTimer = 0;
		}
		yield break;
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x00090510 File Offset: 0x0008E910
	public void RandomPosition()
	{
		int index = UnityEngine.Random.Range(0, 8);
		Vector3 b = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0f, UnityEngine.Random.Range(-0.2f, 0.2f));
		if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
		{
			base.transform.position = GGPlayerGeneratePositionControl.mInstance.BlueGeneratePositions[index].position + b;
		}
		else
		{
			base.transform.position = GGPlayerGeneratePositionControl.mInstance.RedGeneratePositions[index].position + b;
		}
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x000905B8 File Offset: 0x0008E9B8
	public void BlueRandomPositionInStongholdArea(int index)
	{
		int index2 = UnityEngine.Random.Range(0, 4);
		Vector3 b = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0f, UnityEngine.Random.Range(-0.2f, 0.2f));
		if (index == 1)
		{
			base.transform.position = GGPlayerGeneratePositionControl.mInstance.StrongholdModeBlueGeneratePositions_Area1[index2].position + b;
		}
		else if (index == 2)
		{
			base.transform.position = GGPlayerGeneratePositionControl.mInstance.StrongholdModeBlueGeneratePositions_Area2[index2].position + b;
		}
		else if (index == 3)
		{
			base.transform.position = GGPlayerGeneratePositionControl.mInstance.StrongholdModeBlueGeneratePositions_Area3[index2].position + b;
		}
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x00090688 File Offset: 0x0008EA88
	public void RedRandomPositionInStongholdArea(int index)
	{
		int index2 = UnityEngine.Random.Range(0, 4);
		Vector3 b = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0f, UnityEngine.Random.Range(-0.2f, 0.2f));
		if (index == 1)
		{
			base.transform.position = GGPlayerGeneratePositionControl.mInstance.StrongholdModeRedGeneratePositions_Area1[index2].position + b;
		}
		else if (index == 2)
		{
			base.transform.position = GGPlayerGeneratePositionControl.mInstance.StrongholdModeRedGeneratePositions_Area2[index2].position + b;
		}
		else if (index == 3)
		{
			base.transform.position = GGPlayerGeneratePositionControl.mInstance.StrongholdModeRedGeneratePositions_Area3[index2].position + b;
		}
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x00090758 File Offset: 0x0008EB58
	public void MutationModeRandomPositionInWaitingRoom()
	{
		int index = UnityEngine.Random.Range(0, 15);
		Vector3 b = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0f, UnityEngine.Random.Range(-0.2f, 0.2f));
		base.transform.position = GGPlayerGeneratePositionControl.mInstance.MutationModeGeneratePositionsInWaitingRoom[index].position + b;
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x000907C0 File Offset: 0x0008EBC0
	public void MutationModeRandomPosition()
	{
		int index = UnityEngine.Random.Range(0, 30);
		Vector3 b = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0f, UnityEngine.Random.Range(-0.2f, 0.2f));
		base.transform.position = GGPlayerGeneratePositionControl.mInstance.MutationModeGeneratePositions[index].position + b;
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x00090828 File Offset: 0x0008EC28
	public void HuntingModeRandomPositionInWaitingRoom()
	{
		int index = UnityEngine.Random.Range(0, 4);
		Vector3 b = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0f, UnityEngine.Random.Range(-0.2f, 0.2f));
		base.transform.position = GGPlayerGeneratePositionControl.mInstance.RedGeneratePositions[index].position + b;
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x00090890 File Offset: 0x0008EC90
	public void HuntingModeRandomPosition()
	{
		int index = UnityEngine.Random.Range(0, 4);
		Vector3 b = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0f, UnityEngine.Random.Range(-0.2f, 0.2f));
		base.transform.position = GGPlayerGeneratePositionControl.mInstance.BlueGeneratePositions[index].position + b;
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x000908F8 File Offset: 0x0008ECF8
	private void InitEvent()
	{
		GGNetworkKit.mInstance.ReceiveDamage += this.Event_Damage;
		UIPlayDirector.OnMediKit += this.OnMediKit;
		GGNetworkKit.mInstance.MessageOk += this.Event_MessageOK;
		UIPlayDirector.OnArmor += this.OnArmor;
		UIPauseDirector.OnPauseStart += this.OnPauseStart;
		GrowthManger.OnScenePropsInvalid += this.OnScenePropsInvalid;
		UIPlayDirector.OnInstallBtnPressed += this.OnInstallBtnPressed;
		UIPlayDirector.OnInstallBtnReleased += this.OnInstallBtnReleased;
		UIPlayDirector.OnUninstallBtnPressed += this.OnUninstallBtnPressed;
		UIPlayDirector.OnUninstallBtnReleased += this.OnUninstallBtnReleased;
		UIPlayDirector.OnObserverLeft += this.OnObserverLeft;
		UIPlayDirector.OnObserverRight += this.OnObserverRight;
		UIHuntingModeDirector.OnUIRespawn += this.OnUIRespawn;
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x000909EC File Offset: 0x0008EDEC
	private void Event_Damage(GGDamageEventArgs damageEventArgs)
	{
		int damage = (int)damageEventArgs.damage;
		int id = damageEventArgs.id;
		int weaponType = (int)damageEventArgs.weaponType;
		string name = damageEventArgs.name;
		GGTeamType team = damageEventArgs.team;
		float shooterPositionX = damageEventArgs.shooterPositionX;
		float shooterPositionY = damageEventArgs.shooterPositionY;
		float shooterPositionZ = damageEventArgs.shooterPositionZ;
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead && this.mNetworkCharacter.mBlood > 0)
		{
			if (this.mNetworkCharacter.myArmorInfo.mDurabilityInGame > 0 && this.mNetworkCharacter.myArmorInfo.mDurabilityInGame <= 100)
			{
				this.headRate = Mathf.Lerp(this.mNetworkCharacter.myArmorInfo.mHeadshotIgnoreRate[0], this.mNetworkCharacter.myArmorInfo.mHeadshotIgnoreRate[1], (float)this.mNetworkCharacter.myArmorInfo.mDurabilityInGame / 100f);
				this.bodyRate = Mathf.Lerp(this.mNetworkCharacter.myArmorInfo.mBodyDamageDefendRate[0], this.mNetworkCharacter.myArmorInfo.mBodyDamageDefendRate[1], (float)this.mNetworkCharacter.myArmorInfo.mDurabilityInGame / 100f);
			}
			else
			{
				this.headRate = 0f;
				this.bodyRate = 0f;
			}
			this.headRate += GrowthManagerKit.EProperty().allDic[EnchantmentType.HeadshotProtectRate].additionValue;
			if (weaponType == 7 || weaponType == 8 || weaponType == 14 || weaponType == 17 || weaponType == 41 || weaponType == 48)
			{
				this.bodyRate += GrowthManagerKit.EProperty().allDic[EnchantmentType.DamageReducation].additionValue + GrowthManagerKit.EProperty().allDic[EnchantmentType.ExplosionDamageReducation].additionValue;
				this.mainCamera.GetComponent<Animation>().Play("CameraShakeForBomb");
			}
			else
			{
				this.bodyRate += GrowthManagerKit.EProperty().allDic[EnchantmentType.DamageReducation].additionValue;
			}
			this.bodyRate += 0.1f;
			this.bodyRate = Mathf.Min(this.bodyRate, 0.9f);
			if (damage > 500)
			{
				if (!this.mNetworkCharacter.mPlayerProperties.DamageImmuneWhenRespawn)
				{
					float num = UnityEngine.Random.Range(0f, 1f);
					if (this.ignoreHeadshotTimeCache > 0f)
					{
						this.mNetworkCharacter.mBlood -= 15;
						GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.Killing, 1, "Headshot Protected!");
					}
					else if (num < this.headRate)
					{
						this.mNetworkCharacter.mBlood -= 15;
						GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.Killing, 1, "Headshot Protected!");
						this.ignoreHeadshotTimeCache = 0.5f;
					}
					else
					{
						this.mNetworkCharacter.mBlood -= damage;
					}
				}
			}
			else
			{
				if (!this.mNetworkCharacter.mPlayerProperties.DamageImmuneWhenRespawn && this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.DamageImmune == 0)
				{
					if (this.isInAttackZone_HuntingMode && !this.isInDefendZone_HuntingMode)
					{
						this.mNetworkCharacter.mBlood -= Mathf.Max((int)(2f * (float)damage * Mathf.Max(0f, 1f - this.bodyRate)), 1);
						this.mNetworkCharacter.mBlood = Math.Max(this.mNetworkCharacter.mBlood, 0);
					}
					else if (this.isInDefendZone_HuntingMode && !this.isInAttackZone_HuntingMode)
					{
						this.mNetworkCharacter.mBlood -= Mathf.Max((int)(0.5f * (float)damage * Mathf.Max(0f, 1f - this.bodyRate)), 1);
						this.mNetworkCharacter.mBlood = Math.Max(this.mNetworkCharacter.mBlood, 0);
					}
					else if (!this.isInDefendZone_HuntingMode && !this.isInAttackZone_HuntingMode)
					{
						this.mNetworkCharacter.mBlood -= Mathf.Max((int)((float)damage * Mathf.Max(0f, 1f - this.bodyRate)), 1);
						this.mNetworkCharacter.mBlood = Math.Max(this.mNetworkCharacter.mBlood, 0);
					}
				}
				if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red && !this.IsSpeedTrapTrigger)
				{
					this.mWeaponManager.ChangeMoveSpeed(4f);
					this.SpeedRecoverTime = 1f;
				}
			}
			if (!this.mNetworkCharacter.myArmorInfo.mIsInAutoSupplyStatus)
			{
				if (damage <= 20 && damage > 0)
				{
					this.mNetworkCharacter.myArmorInfo.mDurabilityInGame = Math.Max(this.mNetworkCharacter.myArmorInfo.mDurabilityInGame - 2, 0);
				}
				else if (damage <= 50)
				{
					this.mNetworkCharacter.myArmorInfo.mDurabilityInGame = Math.Max(this.mNetworkCharacter.myArmorInfo.mDurabilityInGame - 5, 0);
				}
				else if (damage > 50)
				{
					this.mNetworkCharacter.myArmorInfo.mDurabilityInGame = Math.Max(this.mNetworkCharacter.myArmorInfo.mDurabilityInGame - 8, 0);
				}
			}
			if (this.mNetworkCharacter.mBlood <= 0 && this.mNetworkCharacter.mBlood + damage >= 0)
			{
				this.mNetworkCharacter.mBlood = 0;
				GGNetworkPlayerProperties mPlayerProperties = this.mNetworkCharacter.mPlayerProperties;
				mPlayerProperties.deadNum += 1;
				this.killNumNoDie = 0;
				this.TimerkillNum = 0;
				this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Dead;
				this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
				this.weaponCamera.GetComponent<Camera>().cullingMask = 0;
				GGNetworkPlayerProperties mPlayerProperties2 = this.mNetworkCharacter.mPlayerProperties;
				mPlayerProperties2.bedamageNum += (short)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
				this.mWeaponManager.SetLaserR7_LaserScope(false);
				this.DeathRotation = true;
				this.SetGrayscaleEffectShow();
				if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
				{
					if ((weaponType != 7 && weaponType != 8 && weaponType != 14 && weaponType != 17 && weaponType != 41 && weaponType != 48 && weaponType != 62) || this.mNetworkCharacter.mPlayerProperties.team != GGTeamType.blue)
					{
						this.IsKilledInMutationMode = true;
						GGMessage ggmessage = new GGMessage();
						ggmessage.messageType = GGMessageType.MessageMutationModeKillOneEnemy;
						ggmessage.messageContent = new GGMessageContent();
						ggmessage.messageContent.ZombieLv = GGMutationModeControl.mInstance.zombielv;
						if (id != this.mNetworkCharacter.mPlayerProperties.id)
						{
							GGNetworkKit.mInstance.SendMessage(ggmessage, id);
						}
					}
					else
					{
						this.IsKilledByBombInMutaionMode = true;
						GGMutationModeControl mInstance = GGMutationModeControl.mInstance;
						mInstance.score -= 40;
						GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.ScoreInMutation, -40, string.Empty);
						this.mNetworkCharacter.mPlayerProperties.MutationModeScore = GGMutationModeControl.mInstance.score;
					}
					if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
					{
						this.zombieDie.Play();
						if (this.mNetworkCharacter.zombieSkinIndex == 5)
						{
							Collider[] array = Physics.OverlapSphere(base.transform.position, 5f);
							foreach (Collider collider in array)
							{
								if (collider)
								{
									if (collider.GetComponent<Collider>().gameObject.tag == "EnemyHeadTag")
									{
										Vector3 a = collider.GetComponent<Collider>().ClosestPointOnBounds(base.transform.position);
										float num2 = Vector3.Distance(a, base.transform.position);
										int num3 = (int)((1f - Mathf.Clamp01(num2 / 8f)) * 250f);
										GGDamageEventArgs ggdamageEventArgs = new GGDamageEventArgs();
										ggdamageEventArgs.damage = (short)num3;
										ggdamageEventArgs.id = this.mNetworkCharacter.mPlayerProperties.id;
										ggdamageEventArgs.weaponType = (short)this.mNetworkCharacter.mWeaponType;
										ggdamageEventArgs.name = this.mNetworkCharacter.mPlayerProperties.name;
										ggdamageEventArgs.team = this.mNetworkCharacter.mPlayerProperties.team;
										ggdamageEventArgs.shooterPositionX = this.mNetworkCharacter.transform.root.position.x;
										ggdamageEventArgs.shooterPositionY = this.mNetworkCharacter.transform.root.position.y;
										ggdamageEventArgs.shooterPositionZ = this.mNetworkCharacter.transform.root.position.z;
										collider.transform.SendMessageUpwards("PlayerDamaged", ggdamageEventArgs, SendMessageOptions.DontRequireReceiver);
										this.mNetworkCharacter.mPlayerProperties.MutationSkill.SelfExplosion = 1;
										base.StartCoroutine(this.SelfExplosionReturn(2f));
									}
								}
							}
						}
					}
					else
					{
						this.playPlayerDieAudio();
					}
				}
				else
				{
					this.playPlayerDieAudio();
				}
				if (GGNetworkKit.mInstance.GetGameMode() != GGModeType.Hunting)
				{
					GGMessage ggmessage2 = new GGMessage();
					ggmessage2.messageType = GGMessageType.MessageKillOneEnemy;
					ggmessage2.messageContent = new GGMessageContent();
					ggmessage2.messageContent.Damage = damage;
					if (id != this.mNetworkCharacter.mPlayerProperties.id)
					{
						GGNetworkKit.mInstance.SendMessage(ggmessage2, id);
					}
					GGNetworkKillMessage ggnetworkKillMessage = new GGNetworkKillMessage();
					ggnetworkKillMessage.killer = name;
					ggnetworkKillMessage.killerTeam = team;
					ggnetworkKillMessage.gun = weaponType;
					ggnetworkKillMessage.theDead = this.mNetworkCharacter.mPlayerProperties.name;
					ggnetworkKillMessage.theDeadTeam = this.mNetworkCharacter.mPlayerProperties.team;
					if (damage > 500)
					{
						ggnetworkKillMessage.headShot = true;
					}
					else
					{
						ggnetworkKillMessage.headShot = false;
					}
					GGNetworkChat.mInstance.WhoKillWhoMessage(ggnetworkKillMessage);
				}
				if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Explosion)
				{
					if (this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb)
					{
						this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb = false;
						UIModeDirector.mInstance.HideCarryBombLogo();
						this.SendTimerBombDropMessage();
						this.SendTimerBombActiveInSceneMessage(true);
					}
					this.HideInstallBombButton();
					this.HideUninstallBombButton();
					for (int j = 0; j < this.GearManager.Length; j++)
					{
						this.GearManager[j].SetActive(false);
					}
					this.mNetworkCharacter.mGearType = 0;
					this.showInstallSchedule = false;
					this.showRemoveSchedule = false;
					this.ScheduleSprite.fillAmount = 0f;
					base.StartCoroutine(this.ExplosionModeChangeToObserver(4f));
				}
				else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting)
				{
					base.StartCoroutine(this.HuntingModeWaitForRespawn(2));
				}
				else
				{
					base.StartCoroutine(this.waitForGeneratePlayer(5));
				}
				this.lastDateTime = DateTime.Now;
			}
			this.playBulletHitAudio();
			base.StartCoroutine(this.DisplayPlayerBlood());
		}
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x0009151C File Offset: 0x0008F91C
	private void Tower_Damage(GGDamageEventArgs damageEventArgs)
	{
		int damage = (int)damageEventArgs.damage;
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead && this.mNetworkCharacter.mBlood > 0)
		{
			if (this.mNetworkCharacter.myArmorInfo.mDurabilityInGame > 0)
			{
				this.headRate = Mathf.Lerp(this.mNetworkCharacter.myArmorInfo.mHeadshotIgnoreRate[0], this.mNetworkCharacter.myArmorInfo.mHeadshotIgnoreRate[1], (float)this.mNetworkCharacter.myArmorInfo.mDurabilityInGame / 100f);
				this.bodyRate = Mathf.Lerp(this.mNetworkCharacter.myArmorInfo.mBodyDamageDefendRate[0], this.mNetworkCharacter.myArmorInfo.mBodyDamageDefendRate[1], (float)this.mNetworkCharacter.myArmorInfo.mDurabilityInGame / 100f);
			}
			else
			{
				this.headRate = 0f;
				this.bodyRate = 0f;
			}
			this.headRate += GrowthManagerKit.EProperty().allDic[EnchantmentType.HeadshotProtectRate].additionValue;
			this.bodyRate += GrowthManagerKit.EProperty().allDic[EnchantmentType.DamageReducation].additionValue + GrowthManagerKit.EProperty().allDic[EnchantmentType.ExplosionDamageReducation].additionValue;
			this.mNetworkCharacter.mBlood -= (int)((float)damage * Mathf.Max(0f, 1f - this.bodyRate));
			if (!this.mNetworkCharacter.myArmorInfo.mIsInAutoSupplyStatus)
			{
				if (damage <= 20 && damage > 0)
				{
					this.mNetworkCharacter.myArmorInfo.mDurabilityInGame = Math.Max(this.mNetworkCharacter.myArmorInfo.mDurabilityInGame - 2, 0);
				}
				else if (damage <= 50)
				{
					this.mNetworkCharacter.myArmorInfo.mDurabilityInGame = Math.Max(this.mNetworkCharacter.myArmorInfo.mDurabilityInGame - 5, 0);
				}
				else if (damage > 50)
				{
					this.mNetworkCharacter.myArmorInfo.mDurabilityInGame = Math.Max(this.mNetworkCharacter.myArmorInfo.mDurabilityInGame - 8, 0);
				}
			}
			if (this.mNetworkCharacter.mBlood <= 0 && this.mNetworkCharacter.mBlood + damage >= 0)
			{
				this.mNetworkCharacter.mBlood = 0;
				GGNetworkPlayerProperties mPlayerProperties = this.mNetworkCharacter.mPlayerProperties;
				mPlayerProperties.deadNum += 1;
				this.killNumNoDie = 0;
				this.TimerkillNum = 0;
				this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Dead;
				this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
				this.weaponCamera.GetComponent<Camera>().cullingMask = 0;
				this.mWeaponManager.SetLaserR7_LaserScope(false);
				this.DeathRotation = true;
				this.SetGrayscaleEffectShow();
				this.playPlayerDieAudio();
				base.StartCoroutine(this.waitForGeneratePlayer(5));
				this.lastDateTime = DateTime.Now;
			}
			base.StartCoroutine(this.DisplayPlayerBlood());
		}
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x00091814 File Offset: 0x0008FC14
	private void Event_MessageOK(GGMessage message)
	{
		if (message.messageType == GGMessageType.MessageKillOneEnemy)
		{
			this.AddOneKill(message.messageContent.Damage);
		}
		if (message.messageType == GGMessageType.MessageMutationModeKillOneEnemy)
		{
			if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
			{
				int zombieLv = message.messageContent.ZombieLv;
				GGMutationModeControl mInstance = GGMutationModeControl.mInstance;
				mInstance.score += (short)(20 * zombieLv);
				GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.ScoreInMutation, 20 * zombieLv, string.Empty);
				this.mNetworkCharacter.mPlayerProperties.MutationModeScore = GGMutationModeControl.mInstance.score;
			}
			else
			{
				if (GGMutationModeControl.mInstance.zombielv == 6)
				{
					GGMutationModeControl mInstance2 = GGMutationModeControl.mInstance;
					mInstance2.score += 100;
					GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.ScoreInMutation, 100, string.Empty);
				}
				else
				{
					GGMutationModeControl mInstance3 = GGMutationModeControl.mInstance;
					mInstance3.score += 200;
					GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.ScoreInMutation, 200, string.Empty);
				}
				this.mNetworkCharacter.mPlayerProperties.MutationModeScore = GGMutationModeControl.mInstance.score;
			}
		}
		if (message.messageType == GGMessageType.MessagePlayerStartMutation)
		{
			this.ChangeMatrixZombie();
		}
		if (message.messageType == GGMessageType.MessageMutationModePlayerCanAttack)
		{
			UIModeDirector.mInstance.EnableButtonInMutationMode();
		}
		if (message.messageType == GGMessageType.MessageMutationModePlayerTranslate)
		{
			this.MutationModeChangePositionToGameScene();
		}
		if (message.messageType == GGMessageType.MessageHuntingModePlayerMoveOut)
		{
			this.HuntingModeChangePositionToGameScene();
		}
		if (message.messageType == GGMessageType.MessageStrondholdOccupy)
		{
			GGNetworkPlayerProperties playerPropertyByID = GGNetworkKit.mInstance.GetManagePlayerProperties().GetPlayerPropertyByID(message.messageContent.ID);
			string name = playerPropertyByID.name;
			GGTeamType team = playerPropertyByID.team;
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
			if (message.messageContent.strongholdID == 1)
			{
				if ((this.mGlobalInfo.modeInfo.mStronghold1State == GGStrondholdState.BlueOccupation || this.mGlobalInfo.modeInfo.mStronghold1State == GGStrondholdState.activate) && team == GGTeamType.red)
				{
					this.mGlobalInfo.modeInfo.mStronghold1State = GGStrondholdState.RedOccupation;
					this.mGlobalInfo.modeInfo.mStronghold1CD = true;
				}
				if ((this.mGlobalInfo.modeInfo.mStronghold1State == GGStrondholdState.RedOccupation || this.mGlobalInfo.modeInfo.mStronghold1State == GGStrondholdState.activate) && team == GGTeamType.blue)
				{
					this.mGlobalInfo.modeInfo.mStronghold1State = GGStrondholdState.BlueOccupation;
					this.mGlobalInfo.modeInfo.mStronghold1CD = true;
				}
			}
			if (message.messageContent.strongholdID == 2)
			{
				if ((this.mGlobalInfo.modeInfo.mStronghold2State == GGStrondholdState.BlueOccupation || this.mGlobalInfo.modeInfo.mStronghold2State == GGStrondholdState.activate) && team == GGTeamType.red)
				{
					this.mGlobalInfo.modeInfo.mStronghold2State = GGStrondholdState.RedOccupation;
					this.mGlobalInfo.modeInfo.mStronghold2CD = true;
				}
				if ((this.mGlobalInfo.modeInfo.mStronghold2State == GGStrondholdState.RedOccupation || this.mGlobalInfo.modeInfo.mStronghold2State == GGStrondholdState.activate) && team == GGTeamType.blue)
				{
					this.mGlobalInfo.modeInfo.mStronghold2State = GGStrondholdState.BlueOccupation;
					this.mGlobalInfo.modeInfo.mStronghold2CD = true;
				}
			}
			if (message.messageContent.strongholdID == 3)
			{
				if ((this.mGlobalInfo.modeInfo.mStronghold3State == GGStrondholdState.BlueOccupation || this.mGlobalInfo.modeInfo.mStronghold3State == GGStrondholdState.activate) && team == GGTeamType.red)
				{
					this.mGlobalInfo.modeInfo.mStronghold3State = GGStrondholdState.RedOccupation;
					this.mGlobalInfo.modeInfo.mStronghold3CD = true;
				}
				if ((this.mGlobalInfo.modeInfo.mStronghold3State == GGStrondholdState.RedOccupation || this.mGlobalInfo.modeInfo.mStronghold3State == GGStrondholdState.activate) && team == GGTeamType.blue)
				{
					this.mGlobalInfo.modeInfo.mStronghold3State = GGStrondholdState.BlueOccupation;
					this.mGlobalInfo.modeInfo.mStronghold3CD = true;
				}
			}
			GGNetworkSystemMessage ggnetworkSystemMessage = new GGNetworkSystemMessage();
			string str = (message.messageContent.strongholdID != 1) ? ((message.messageContent.strongholdID != 2) ? " A " : " B ") : " C ";
			ggnetworkSystemMessage.content = name + " occupy the  stronghold " + str + " ! ";
			if (team == GGTeamType.blue)
			{
				ggnetworkSystemMessage.color = GGColor.Blue;
			}
			else if (team == GGTeamType.red)
			{
				ggnetworkSystemMessage.color = GGColor.Red;
			}
			GGNetworkChat.mInstance.SystemMessage(ggnetworkSystemMessage);
		}
		if (message.messageType == GGMessageType.MessagePlayerActiveTimerBomb)
		{
			this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb = true;
			UIModeDirector.mInstance.ShowCarryBombLogo();
			this.SendTimerBombTakeMessage();
		}
		if (message.messageType == GGMessageType.MessageTimerBombTake)
		{
			GGNetworkPlayerProperties playerPropertyByID2 = GGNetworkKit.mInstance.GetManagePlayerProperties().GetPlayerPropertyByID(message.messageContent.ID);
			string name2 = playerPropertyByID2.name;
			GGNetworkSystemMessage ggnetworkSystemMessage2 = new GGNetworkSystemMessage();
			ggnetworkSystemMessage2.content = name2 + " take the time bomb !";
			ggnetworkSystemMessage2.color = GGColor.Red;
			GGNetworkChat.mInstance.SystemMessage(ggnetworkSystemMessage2);
		}
		if (message.messageType == GGMessageType.MessageTimerBombActiveInScene)
		{
			this.mGlobalInfo.modeInfo.activeTimerBomb = message.messageContent.ActiveTimerBomb;
		}
		if (message.messageType == GGMessageType.SendTimerBombSynPositionMessage)
		{
			this.mGlobalInfo.modeInfo.TimerBombPositionX = message.messageContent.X;
			this.mGlobalInfo.modeInfo.TimerBombPositionY = message.messageContent.Y;
			this.mGlobalInfo.modeInfo.TimerBombPositionZ = message.messageContent.Z;
		}
		if (message.messageType == GGMessageType.MessageTimerBombDrop)
		{
			GGNetworkKit.mInstance.CreateSeceneObject("ExplosionModeTimerBombDroped", new Vector3(message.messageContent.X, message.messageContent.Y, message.messageContent.Z), Quaternion.identity);
		}
		if (message.messageType == GGMessageType.MessageTimerBombInstall)
		{
			GGNetworkPlayerProperties playerPropertyByID3 = GGNetworkKit.mInstance.GetManagePlayerProperties().GetPlayerPropertyByID(message.messageContent.ID);
			string name3 = playerPropertyByID3.name;
			GGTeamType team2 = playerPropertyByID3.team;
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
			this.mGlobalInfo.modeInfo.IsTimerBombInstalled = true;
			this.mGlobalInfo.modeInfo.bombPositionId = message.messageContent.TimerBombPositionID;
			GGNetworkKit.mInstance.CreateSeceneObject("ExplosionModeTimerBombInstalled", GGPlayerGeneratePositionControl.mInstance.ExplosionModeTimerBombPosition[message.messageContent.TimerBombPositionID - 1].position, Quaternion.identity);
			this.mGlobalInfo.modeInfo.activeTimerBomb = true;
			GGNetworkSystemMessage ggnetworkSystemMessage3 = new GGNetworkSystemMessage();
			ggnetworkSystemMessage3.content = name3 + " install the time bomb !";
			if (team2 == GGTeamType.blue)
			{
				ggnetworkSystemMessage3.color = GGColor.Blue;
			}
			else if (team2 == GGTeamType.red)
			{
				ggnetworkSystemMessage3.color = GGColor.Red;
			}
			GGNetworkChat.mInstance.SystemMessage(ggnetworkSystemMessage3);
		}
		if (message.messageType == GGMessageType.MessageTimerBombRemove)
		{
			GGNetworkPlayerProperties playerPropertyByID4 = GGNetworkKit.mInstance.GetManagePlayerProperties().GetPlayerPropertyByID(message.messageContent.ID);
			string name4 = playerPropertyByID4.name;
			GGTeamType team3 = playerPropertyByID4.team;
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
			this.mGlobalInfo.modeInfo.activeTimerBomb = true;
			if (this.mGlobalInfo.modeInfo.explosionTimer > 1)
			{
				this.mGlobalInfo.modeInfo.IsTimerBombInstalled = false;
				this.mGlobalInfo.modeInfo.IsTimerBombUninstall = true;
				GameObject gameObject = GameObject.FindGameObjectWithTag("ExplosionModeTimerBomb");
				if (gameObject != null)
				{
					GGNetworkKit.mInstance.DestroySceneObjectRPC(gameObject);
				}
				GGNetworkSystemMessage ggnetworkSystemMessage4 = new GGNetworkSystemMessage();
				ggnetworkSystemMessage4.content = name4 + " uninstall the time bomb !";
				if (team3 == GGTeamType.blue)
				{
					ggnetworkSystemMessage4.color = GGColor.Blue;
				}
				else if (team3 == GGTeamType.red)
				{
					ggnetworkSystemMessage4.color = GGColor.Red;
				}
				GGNetworkChat.mInstance.SystemMessage(ggnetworkSystemMessage4);
			}
		}
		if (message.messageType == GGMessageType.MessageExplosionModeSingleRoundWin)
		{
			string winnerName = (message.messageContent.ID != 1) ? "ExplosionWinLogo_Red" : "ExplosionWinLogo_Blue";
			UIModeDirector.mInstance.ShowTeamWinSpriteTip(winnerName);
			if (message.messageContent.ID == 1)
			{
				this.BlueTeamWin.Play();
			}
			else
			{
				this.RedTeamWin.Play();
			}
			base.StartCoroutine(this.HideTeamWinTipLabel(2f));
		}
		if (message.messageType == GGMessageType.MessageExplosionModeNextRoundStart)
		{
			this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb = false;
			UIModeDirector.mInstance.HideCarryBombLogo();
			this.HideInstallBombButton();
			this.showInstallSchedule = false;
			this.HideUninstallBombButton();
			this.showRemoveSchedule = false;
			this.ScheduleSprite.fillAmount = 0f;
			UIModeDirector.mInstance.HideExplosionCurRoundLabel();
			UIModeDirector.mInstance.HideTeamWinSpriteTip();
			if (this.mNetworkCharacter.mPlayerProperties.isObserver)
			{
				this.ExplosionModeChangeToGamer();
			}
			else
			{
				base.StartCoroutine(this.waitForGeneratePlayer(0));
			}
			GGExplosionModeTimerBombLogic.mInstance.cannotControlJoystick = true;
			UIModeDirector.mInstance.ShowJoystickNode(false);
		}
		if (message.messageType == GGMessageType.MessageGodLike)
		{
			GGNetworkPlayerProperties playerPropertyByID5 = GGNetworkKit.mInstance.GetManagePlayerProperties().GetPlayerPropertyByID(message.messageContent.ID);
			string name5 = playerPropertyByID5.name;
			GGTeamType team4 = playerPropertyByID5.team;
			GGNetworkSystemMessage ggnetworkSystemMessage5 = new GGNetworkSystemMessage();
			ggnetworkSystemMessage5.content = string.Concat(new string[]
			{
				UIToolFunctionController.GetColorCode((team4 != GGTeamType.blue) ? GGColor.Red : GGColor.Blue),
				name5,
				"[-]",
				UIToolFunctionController.GetColorCode(GGColor.Yellow),
				" God Like ![-]"
			});
			GGNetworkChat.mInstance.SystemMessage(ggnetworkSystemMessage5);
		}
		if (message.messageType == GGMessageType.MessagePlayerAutoMove)
		{
			this.AutoMove(new Vector3(message.messageContent.X, message.messageContent.Y, message.messageContent.Z));
		}
		if (message.messageType == GGMessageType.MessagePlayerSpeedSlow)
		{
			GrowthManagerKit.AddCustomEProperty(SceneEnchantmentProps.HCustomSpeedDown, -0.6f, 16f);
		}
		if (message.messageType == GGMessageType.MessagePlayerAcidRain)
		{
			this.RainFallEffect.SetActive(true);
			this.isAcidRainEffect = true;
		}
		if (message.messageType == GGMessageType.MessageHuntingModeEarthShake && this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.EarthShakeEffectSound.Play();
			this.mainCamera.GetComponent<Animation>().Play("CameraShakeForEarthShake");
			this.EarthshakeDamage();
		}
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x00092268 File Offset: 0x00090668
	private void EarthshakeCheck()
	{
		this.EarthshakeDamage();
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x00092270 File Offset: 0x00090670
	private void EarthshakeDamage()
	{
		if (this.mNetworkCharacter.mBlood > 0)
		{
			this.Event_Damage(new GGDamageEventArgs
			{
				damage = 16
			});
		}
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x000922A3 File Offset: 0x000906A3
	public void DisableAcidRainEffect()
	{
		this.RainFallEffect.SetActive(false);
		this.isAcidRainEffect = false;
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x000922B8 File Offset: 0x000906B8
	public void AutoMove(Vector3 Movement)
	{
		this.mCharacterMotorCS.canMove = false;
		this.isAutoMove = true;
		this.AutoMovement = Movement;
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x000922D4 File Offset: 0x000906D4
	public void AddOneKill(int damage)
	{
		GGNetworkPlayerProperties mPlayerProperties = this.mNetworkCharacter.mPlayerProperties;
		mPlayerProperties.killNum += 1;
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageStrongholdTeamKillerIncrease;
		ggmessage.messageContent = new GGMessageContent();
		ggmessage.messageContent.Team = this.mNetworkCharacter.mPlayerProperties.team;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
		GGMessage ggmessage2 = new GGMessage();
		ggmessage2.messageType = GGMessageType.MessageKillingCompetitionTeamKillerIncrease;
		ggmessage2.messageContent = new GGMessageContent();
		ggmessage2.messageContent.Team = this.mNetworkCharacter.mPlayerProperties.team;
		GGNetworkKit.mInstance.SendMessage(ggmessage2, GGTarget.MasterClient);
		GGMessage ggmessage3 = new GGMessage();
		ggmessage3.messageType = GGMessageType.MessageKnifeCompetitionTeamKillerIncrease;
		ggmessage3.messageContent = new GGMessageContent();
		ggmessage3.messageContent.Team = this.mNetworkCharacter.mPlayerProperties.team;
		GGNetworkKit.mInstance.SendMessage(ggmessage3, GGTarget.MasterClient);
		if (GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingExpDouble].isEnabled)
		{
			GrowthManagerKit.AddCharacterExp((int)(3f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingExpDouble].additionValue)));
		}
		else if (GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingExpDoubleTrigger].isTrigger())
		{
			GrowthManagerKit.AddCharacterExp((int)(3f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingExpDoubleTrigger].additionValue)));
		}
		else
		{
			GrowthManagerKit.AddCharacterExp(3);
		}
		if (GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingCoinDouble].isEnabled)
		{
			GrowthManagerKit.AddCoins((int)(3f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingCoinDouble].additionValue)));
		}
		else if (GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingCoinDoubleTrigger].isTrigger())
		{
			GrowthManagerKit.AddCoins((int)(3f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingCoinDoubleTrigger].additionValue)));
		}
		else
		{
			GrowthManagerKit.AddCoins(3);
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue && GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingHpFullRecovery].isTrigger())
			{
				this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
			}
		}
		else if (GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingHpFullRecovery].isTrigger())
		{
			this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
		}
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue && GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingInvisible].isTrigger())
			{
				base.StartCoroutine(this.InvisibleTriggerAfterKilling(GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingInvisible].validTimeAfterTrigger));
			}
		}
		else if (GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingInvisible].isTrigger())
		{
			base.StartCoroutine(this.InvisibleTriggerAfterKilling(GrowthManagerKit.EProperty().allDic[EnchantmentType.KillingInvisible].validTimeAfterTrigger));
		}
		GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInWorldwideMultiplayer, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInWorldwideMultiplayer));
		GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInWorldwideMultiplayerSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInWorldwideMultiplayerSeason));
		this.killNumNoDie++;
		if ((int)this.mNetworkCharacter.mPlayerProperties.maxKillNum < this.killNumNoDie)
		{
			this.mNetworkCharacter.mPlayerProperties.maxKillNum = (short)this.killNumNoDie;
		}
		this.TimerkillNum++;
		this.killTimer += 4f;
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.TeamDeathMatch)
		{
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tDailyKillInDeathMatchMode, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tDailyKillInDeathMatchMode));
		}
		switch (this.killNumNoDie)
		{
		case 2:
			GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.Killing, 1, "KillingPopLogo_Dominating");
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalTwoKill, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalTwoKill));
			goto IL_552;
		case 4:
			GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.Killing, 1, "KillingPopLogo_Unstoppable");
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalFourKill, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalFourKill));
			goto IL_552;
		case 6:
			GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.Killing, 1, "KillingPopLogo_Rampage");
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalSixKill, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalSixKill));
			goto IL_552;
		case 8:
			GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.Killing, 1, "KillingPopLogo_MonsterKill");
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalEightKill, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalEightKill));
			goto IL_552;
		case 10:
		{
			GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.Killing, 1, "KillingPopLogo_GodLike");
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKill, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKill));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKillSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKillSeason));
			GGMessage ggmessage4 = new GGMessage();
			ggmessage4.messageType = GGMessageType.MessageGodLike;
			ggmessage4.messageContent = new GGMessageContent();
			ggmessage4.messageContent.ID = this.mNetworkCharacter.mPlayerProperties.id;
			GGNetworkKit.mInstance.SendMessage(ggmessage4, GGTarget.MasterClient);
			goto IL_552;
		}
		}
		GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.Killing, 1, "KillingPopLogo_NormalKilling");
		IL_552:
		if (this.killNumNoDie > 10)
		{
			if (this.killNumNoDie % 2 == 0)
			{
				GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.Killing, 1, "KillingPopLogo_GodLike");
				GGMessage ggmessage5 = new GGMessage();
				ggmessage5.messageType = GGMessageType.MessageGodLike;
				ggmessage5.messageContent = new GGMessageContent();
				ggmessage5.messageContent.ID = this.mNetworkCharacter.mPlayerProperties.id;
				GGNetworkKit.mInstance.SendMessage(ggmessage5, GGTarget.MasterClient);
			}
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKill, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKill));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKillSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalGoldLikeKillSeason));
		}
		if (this.killTimer > 0f)
		{
			if (this.TimerkillNum == 2)
			{
				this.timerKill.clip = this.timerKillclip[0];
				this.timerKill.Play();
			}
			else if (this.TimerkillNum == 3)
			{
				this.timerKill.clip = this.timerKillclip[1];
				this.timerKill.Play();
			}
			else if (this.TimerkillNum == 4)
			{
				this.timerKill.clip = this.timerKillclip[2];
				this.timerKill.Play();
			}
			else if (this.TimerkillNum == 5)
			{
				this.timerKill.clip = this.timerKillclip[3];
				this.timerKill.Play();
			}
			else if (this.TimerkillNum > 5)
			{
				this.timerKill.clip = this.timerKillclip[4];
				this.timerKill.Play();
			}
		}
		if (damage >= 500)
		{
			GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.Killing, 1, "KillingPopLogo_Headshot");
			this.headShotKill.Play();
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalHeadshotKill, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalHeadshotKill));
			GrowthManagerKit.SetFightingStatisticsValue(FightingStatisticsTag.tTotalHeadshotKillSeason, 1 + GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalHeadshotKillSeason));
			GGNetworkPlayerProperties mPlayerProperties2 = this.mNetworkCharacter.mPlayerProperties;
			mPlayerProperties2.headshotNum += 1;
		}
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x00092A1C File Offset: 0x00090E1C
	private IEnumerator DisplayPlayerBlood()
	{
		if (!this.PlayerBloodShow.activeSelf)
		{
			this.PlayerBloodShow.SetActive(true);
			this.PlayerBloodShow.GetComponent<UITexture>().color = new Color(1f, 0f, 0f, 1f);
			TweenAlpha.Begin(this.PlayerBloodShow, 1f, 0f, 0f);
			yield return new WaitForSeconds(1f);
			this.PlayerBloodShow.SetActive(false);
		}
		yield break;
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x00092A37 File Offset: 0x00090E37
	public void addKilledNum(int i)
	{
		this.killedNum++;
		GrowthManagerKit.AddCharacterExp(i);
		GrowthManagerKit.AddCoins(i);
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x00092A53 File Offset: 0x00090E53
	public void OnGameOver()
	{
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x00092A55 File Offset: 0x00090E55
	public void SetGrayscaleEffectShow()
	{
		this.grayscaleEffectAnimation.enabled = true;
		this.grayscaleEffect.enabled = true;
		this.grayscaleEffect.rampOffset = 0.4f;
		this.grayscaleEffectAnimation.isGrayscaleEffect = true;
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x00092A8B File Offset: 0x00090E8B
	public void SetGrayscaleEffectDisappear()
	{
		if (this.grayscaleEffectAnimation)
		{
			this.grayscaleEffectAnimation.isGrayscaleEffect = false;
		}
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x00092AAC File Offset: 0x00090EAC
	private void OnDisable()
	{
		GGNetworkKit.mInstance.ReceiveDamage -= this.Event_Damage;
		UIPlayDirector.OnMediKit -= this.OnMediKit;
		UIPlayDirector.OnArmor -= this.OnArmor;
		UIPauseDirector.OnPauseStart -= this.OnPauseStart;
		GGNetworkKit.mInstance.MessageOk -= this.Event_MessageOK;
		GrowthManger.OnScenePropsInvalid -= this.OnScenePropsInvalid;
		UIPlayDirector.OnInstallBtnPressed -= this.OnInstallBtnPressed;
		UIPlayDirector.OnInstallBtnReleased -= this.OnInstallBtnReleased;
		UIPlayDirector.OnUninstallBtnPressed -= this.OnUninstallBtnPressed;
		UIPlayDirector.OnUninstallBtnReleased -= this.OnUninstallBtnReleased;
		UIPlayDirector.OnObserverLeft -= this.OnObserverLeft;
		UIPlayDirector.OnObserverRight -= this.OnObserverRight;
		UIHuntingModeDirector.OnUIRespawn -= this.OnUIRespawn;
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x00092BA0 File Offset: 0x00090FA0
	private void OnPauseStart()
	{
		this.isOnPauseStart = true;
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
			if (!this.mGlobalInfo.modeInfo.isGotoGameScene)
			{
				this.MutationModeRandomPositionInWaitingRoom();
			}
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting)
		{
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
			if (!this.mGlobalInfo.modeInfo.IsStartHunting)
			{
				this.HuntingModeRandomPositionInWaitingRoom();
			}
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Explosion)
		{
			this.explosionModeHasStart = true;
			this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
			if (180 - this.mGlobalInfo.modeInfo.totalTimer <= this.mGlobalInfo.modeInfo.ExplosionModeNewPlayerJoinGameTimer)
			{
				if (this.mGlobalInfo.modeInfo.RoundNum == 1 && !this.mGlobalInfo.modeInfo.IsStartExplosion)
				{
					UIModeDirector.mInstance.StartWaitTipNode();
				}
				this.mNetworkCharacter.mPlayerProperties.isObserver = false;
				this.RandomPosition();
			}
			else
			{
				this.mNetworkCharacter.mPlayerProperties.isObserver = true;
				base.StartCoroutine(this.ExplosionModeChangeToObserver(0f));
			}
		}
		else
		{
			this.RandomPosition();
		}
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x00092D08 File Offset: 0x00091108
	private IEnumerator ExplosionModeChangeToObserver(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
		if (!this.mGlobalInfo.modeInfo.singleRoundResultCalc)
		{
			this.BluePlayerObserverCamera = GGNetworkKit.mInstance.GetBlueLivePlayerObserverCameraList();
			this.RedPlayerObserverCamera = GGNetworkKit.mInstance.GetRedLivePlayerObserverCameraList();
			if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
			{
				if (this.BluePlayerObserverCamera.Count != 0)
				{
					this.mainCamera.GetComponent<Camera>().enabled = false;
					this.mainCamera.root.transform.Find("AudioListenerControl").GetComponent<AudioListener>().enabled = false;
					this.weaponCamera.GetComponent<Camera>().cullingMask = 0;
					this.BluePlayerObserverCamera[0].SetActive(true);
					this.ObserverCamera = this.BluePlayerObserverCamera[0];
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(false);
				}
				else if (this.RedPlayerObserverCamera.Count != 0)
				{
					this.mainCamera.GetComponent<Camera>().enabled = false;
					this.mainCamera.root.transform.Find("AudioListenerControl").GetComponent<AudioListener>().enabled = false;
					this.weaponCamera.GetComponent<Camera>().cullingMask = 0;
					this.RedPlayerObserverCamera[0].SetActive(true);
					this.ObserverCamera = this.RedPlayerObserverCamera[0];
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(false);
				}
			}
			else if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
			{
				if (this.RedPlayerObserverCamera.Count != 0)
				{
					this.mainCamera.GetComponent<Camera>().enabled = false;
					this.mainCamera.root.transform.Find("AudioListenerControl").GetComponent<AudioListener>().enabled = false;
					this.weaponCamera.GetComponent<Camera>().cullingMask = 0;
					this.RedPlayerObserverCamera[0].SetActive(true);
					this.ObserverCamera = this.RedPlayerObserverCamera[0];
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(false);
				}
				else if (this.BluePlayerObserverCamera.Count != 0)
				{
					this.mainCamera.GetComponent<Camera>().enabled = false;
					this.mainCamera.root.transform.Find("AudioListenerControl").GetComponent<AudioListener>().enabled = false;
					this.weaponCamera.GetComponent<Camera>().cullingMask = 0;
					this.BluePlayerObserverCamera[0].SetActive(true);
					this.ObserverCamera = this.BluePlayerObserverCamera[0];
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(false);
				}
			}
			if (this.BluePlayerObserverCamera.Count != 0 || this.RedPlayerObserverCamera.Count != 0)
			{
				UIModeDirector.mInstance.ShowObserverNode();
				this.ObserverCameraIndex = 0;
				this.mNetworkCharacter.mPlayerProperties.isObserver = true;
				if (this.ObserverCamera != null)
				{
					UIModeDirector.mInstance.RefreshObserverNode(this.ObserverCamera.transform.root.GetComponent<GGNetworkCharacter>());
				}
				this.mNetworkCharacter.isNeedSyn = false;
				base.transform.root.position -= new Vector3(0f, 2500f, 0f);
			}
		}
		yield break;
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x00092D2C File Offset: 0x0009112C
	private void OnObserverRight()
	{
		this.BluePlayerObserverCamera = GGNetworkKit.mInstance.GetBlueLivePlayerObserverCameraList();
		this.RedPlayerObserverCamera = GGNetworkKit.mInstance.GetRedLivePlayerObserverCameraList();
		if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
		{
			if (this.BluePlayerObserverCamera.Count > 0)
			{
				if (this.ObserverCameraIndex < this.BluePlayerObserverCamera.Count - 1)
				{
					this.ObserverCameraIndex++;
				}
				else
				{
					this.ObserverCameraIndex = 0;
				}
				if (this.ObserverCamera != null)
				{
					this.ObserverCamera.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(true);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(true);
					this.BluePlayerObserverCamera[this.ObserverCameraIndex].SetActive(true);
					this.ObserverCamera = this.BluePlayerObserverCamera[this.ObserverCameraIndex];
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(false);
				}
			}
		}
		else if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
		{
			if (this.RedPlayerObserverCamera.Count > 0)
			{
				if (this.ObserverCameraIndex < this.RedPlayerObserverCamera.Count - 1)
				{
					this.ObserverCameraIndex++;
				}
				else
				{
					this.ObserverCameraIndex = 0;
				}
				if (this.ObserverCamera != null)
				{
					this.ObserverCamera.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(true);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(true);
					this.RedPlayerObserverCamera[this.ObserverCameraIndex].SetActive(true);
					this.ObserverCamera = this.RedPlayerObserverCamera[this.ObserverCameraIndex];
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(false);
				}
			}
			else if (this.BluePlayerObserverCamera.Count > 0)
			{
				if (this.ObserverCameraIndex < this.BluePlayerObserverCamera.Count - 1)
				{
					this.ObserverCameraIndex++;
				}
				else
				{
					this.ObserverCameraIndex = 0;
				}
				if (this.ObserverCamera != null)
				{
					this.ObserverCamera.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(true);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(true);
					this.BluePlayerObserverCamera[this.ObserverCameraIndex].SetActive(true);
					this.ObserverCamera = this.BluePlayerObserverCamera[this.ObserverCameraIndex];
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(false);
				}
			}
		}
		if (this.ObserverCamera != null)
		{
			UIModeDirector.mInstance.RefreshObserverNode(this.ObserverCamera.transform.root.GetComponent<GGNetworkCharacter>());
		}
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x00093124 File Offset: 0x00091524
	private void OnObserverLeft()
	{
		this.BluePlayerObserverCamera = GGNetworkKit.mInstance.GetBlueLivePlayerObserverCameraList();
		this.RedPlayerObserverCamera = GGNetworkKit.mInstance.GetRedLivePlayerObserverCameraList();
		if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
		{
			if (this.BluePlayerObserverCamera.Count > 0)
			{
				if (this.ObserverCameraIndex > 0)
				{
					this.ObserverCameraIndex--;
				}
				else
				{
					this.ObserverCameraIndex = this.BluePlayerObserverCamera.Count - 1;
				}
				if (this.ObserverCamera != null)
				{
					this.ObserverCamera.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(true);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(true);
					this.BluePlayerObserverCamera[this.ObserverCameraIndex].SetActive(true);
					this.ObserverCamera = this.BluePlayerObserverCamera[this.ObserverCameraIndex];
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(false);
				}
			}
		}
		else if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
		{
			if (this.RedPlayerObserverCamera.Count > 0)
			{
				if (this.ObserverCameraIndex > 0)
				{
					this.ObserverCameraIndex--;
				}
				else
				{
					this.ObserverCameraIndex = this.RedPlayerObserverCamera.Count - 1;
				}
				if (this.ObserverCamera != null)
				{
					this.ObserverCamera.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(true);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(true);
					this.RedPlayerObserverCamera[this.ObserverCameraIndex].SetActive(true);
					this.ObserverCamera = this.RedPlayerObserverCamera[this.ObserverCameraIndex];
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(false);
				}
			}
			else if (this.RedPlayerObserverCamera.Count == 0 && this.BluePlayerObserverCamera.Count > 0)
			{
				if (this.ObserverCameraIndex > 0)
				{
					this.ObserverCameraIndex--;
				}
				else
				{
					this.ObserverCameraIndex = this.BluePlayerObserverCamera.Count - 1;
				}
				if (this.ObserverCamera != null)
				{
					this.ObserverCamera.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(true);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(true);
					this.BluePlayerObserverCamera[this.ObserverCameraIndex].SetActive(true);
					this.ObserverCamera = this.BluePlayerObserverCamera[this.ObserverCameraIndex];
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(false);
					this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(false);
				}
			}
		}
		if (this.ObserverCamera != null)
		{
			UIModeDirector.mInstance.RefreshObserverNode(this.ObserverCamera.transform.root.GetComponent<GGNetworkCharacter>());
		}
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0009352C File Offset: 0x0009192C
	private void ExplosionModeChangeToGamer()
	{
		this.mNetworkCharacter.mPlayerProperties.isObserver = false;
		if (this.ObserverCamera != null)
		{
			this.ObserverCamera.SetActive(false);
			this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/BloodBar").gameObject.SetActive(true);
			this.ObserverCamera.transform.root.Find("Player_1_sinkmesh/TeamIcon").gameObject.SetActive(true);
		}
		this.mainCamera.GetComponent<Camera>().enabled = true;
		this.weaponCamera.GetComponent<Camera>().cullingMask = 256;
		this.mainCamera.root.transform.Find("AudioListenerControl").GetComponent<AudioListener>().enabled = true;
		UIModeDirector.mInstance.HideObserverNode();
		UIModeDirector.mInstance.RefreshObserverNode(this.mNetworkCharacter);
		this.mNetworkCharacter.isNeedSyn = true;
		this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
		this.mainCamera.rotation = new Quaternion(0f, 0f, 0f, 0f);
		this.mainCamera.GetComponent<Camera>().fieldOfView = 47f;
		this.mWeaponManager.SetLaserR7_LaserScope(true);
		this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
		this.RandomPosition();
		base.StartCoroutine(this.DamageImmuneWhenRespawn());
		this.SetGrayscaleEffectDisappear();
		this.ClearAllBufferOfPropAfterDie();
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x000936C0 File Offset: 0x00091AC0
	private IEnumerator MutationModePlayerJoinAfterStart()
	{
		yield return new WaitForSeconds(3f);
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
		if (this.mGlobalInfo.modeInfo.isGotoGameScene)
		{
			UIModeDirector.mInstance.EnableButtonInMutationMode();
			this.MutationModeRandomPosition();
			if (this.mNetworkCharacter != null)
			{
				this.mNetworkCharacter.mPlayerProperties.MutationModeScore = GGMutationModeControl.mInstance.score;
			}
		}
		yield break;
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x000936DC File Offset: 0x00091ADC
	private void OnMediKit()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
		}
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x00093728 File Offset: 0x00091B28
	private void OnArmor()
	{
		this.mNetworkCharacter.myArmorInfo.mDurabilityInGame = 100;
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x0009373C File Offset: 0x00091B3C
	public void SendStrongholdGetMessage(int strongholdId, int playerID)
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageStrondholdOccupy;
		ggmessage.messageContent = new GGMessageContent();
		ggmessage.messageContent.strongholdID = strongholdId;
		ggmessage.messageContent.ID = playerID;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x00093788 File Offset: 0x00091B88
	public void SendTimerBombTakeMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageTimerBombTake;
		ggmessage.messageContent = new GGMessageContent();
		ggmessage.messageContent.ID = this.mNetworkCharacter.mPlayerProperties.id;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x000937D8 File Offset: 0x00091BD8
	public void SendTimerBombActiveInSceneMessage(bool activeTimerBomb)
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageTimerBombActiveInScene;
		ggmessage.messageContent = new GGMessageContent();
		ggmessage.messageContent.ActiveTimerBomb = activeTimerBomb;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x00093818 File Offset: 0x00091C18
	public void SendTimerBombSynPositionMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.SendTimerBombSynPositionMessage;
		ggmessage.messageContent = new GGMessageContent();
		ggmessage.messageContent.X = base.transform.position.x;
		ggmessage.messageContent.Y = base.transform.position.y;
		ggmessage.messageContent.Z = base.transform.position.z;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x000938A8 File Offset: 0x00091CA8
	public void SendTimerBombInstallMessage(int playerID, int timerBombPositionIndex)
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageTimerBombInstall;
		ggmessage.messageContent = new GGMessageContent();
		ggmessage.messageContent.ID = playerID;
		ggmessage.messageContent.TimerBombPositionID = timerBombPositionIndex;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x000938F4 File Offset: 0x00091CF4
	public void SendTimerBombRemoveMessage(int playerID)
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageTimerBombRemove;
		ggmessage.messageContent = new GGMessageContent();
		ggmessage.messageContent.ID = playerID;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x00093934 File Offset: 0x00091D34
	public void SendTimerBombDropMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageTimerBombDrop;
		ggmessage.messageContent = new GGMessageContent();
		ggmessage.messageContent.X = base.transform.position.x;
		ggmessage.messageContent.Y = base.transform.position.y;
		ggmessage.messageContent.Z = base.transform.position.z;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x000939C4 File Offset: 0x00091DC4
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "DeathTrigger" && this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
			{
				this.IsDeathTriggerInMutationMode = true;
				if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
				{
					this.zombieDie.Play();
				}
				else
				{
					GGMutationModeControl mInstance = GGMutationModeControl.mInstance;
					mInstance.score -= 40;
					GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.ScoreInMutation, -40, string.Empty);
					this.mNetworkCharacter.mPlayerProperties.MutationModeScore = GGMutationModeControl.mInstance.score;
					this.playPlayerDieAudio();
				}
			}
			else
			{
				this.playPlayerDieAudio();
			}
			this.mNetworkCharacter.mBlood = 0;
			GGNetworkPlayerProperties mPlayerProperties = this.mNetworkCharacter.mPlayerProperties;
			mPlayerProperties.deadNum += 1;
			this.killNumNoDie = 0;
			this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Dead;
			this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
			this.weaponCamera.GetComponent<Camera>().cullingMask = 0;
			this.mWeaponManager.SetLaserR7_LaserScope(false);
			this.SetGrayscaleEffectShow();
			base.StartCoroutine(this.waitForGeneratePlayer(5));
			this.lastDateTime = DateTime.Now;
			base.StartCoroutine(this.DisplayPlayerBlood());
		}
		if (other.gameObject.name == "SpeedTrap(Clone)" && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red && this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.IsSpeedTrapTrigger = true;
			base.StartCoroutine(this.SpeedTrapReturn(20f));
			this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.SpeedTrap = 1;
			this.mWeaponManager.ChangeMoveSpeed(2f);
		}
		if (other.gameObject.tag == "Props")
		{
			string name = other.gameObject.name;
			switch (name)
			{
			case "GGPropHpRecover(Clone)":
				GrowthManagerKit.AddEProperty(SceneEnchantmentProps.HpAdd100);
				this.mNetworkCharacter.mBlood += 100;
				if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
				{
					if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
					{
						this.mNetworkCharacter.mBlood = Math.Min(this.mNetworkCharacter.mBlood, (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue)));
					}
					else
					{
						this.mNetworkCharacter.mBlood = Math.Min(this.mNetworkCharacter.mBlood, GGMutationModeControl.ZombieProperty.GetMaxBloodWithLv(GGMutationModeControl.mInstance.zombielv));
					}
				}
				else
				{
					this.mNetworkCharacter.mBlood = Math.Min(this.mNetworkCharacter.mBlood, (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue)));
				}
				other.gameObject.GetComponent<BoxCollider>().enabled = false;
				other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
				GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
				this.PropTriggerSound.Play();
				break;
			case "GGPropAttackEnhance(Clone)":
				GrowthManagerKit.AddEProperty(SceneEnchantmentProps.DamagePlus50);
				this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.AttackEnhance = 1;
				other.gameObject.GetComponent<BoxCollider>().enabled = false;
				other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
				GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
				this.PropTriggerSound.Play();
				break;
			case "GGPropArmorEnhance(Clone)":
				GrowthManagerKit.AddEProperty(SceneEnchantmentProps.DamageReducation50);
				this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.ArmorEnhance = 1;
				other.gameObject.GetComponent<BoxCollider>().enabled = false;
				other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
				GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
				this.PropTriggerSound.Play();
				break;
			case "GGPropSpeedEnhance(Clone)":
				GrowthManagerKit.AddEProperty(SceneEnchantmentProps.TopSpeed);
				this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.SpeedEnhance = 1;
				other.gameObject.GetComponent<BoxCollider>().enabled = false;
				other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
				GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
				this.PropTriggerSound.Play();
				break;
			case "GGPropJumpEnhance(Clone)":
				GrowthManagerKit.AddEProperty(SceneEnchantmentProps.JumpPlus50);
				this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.JumpEnhance = 1;
				other.gameObject.GetComponent<BoxCollider>().enabled = false;
				other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
				GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
				this.PropTriggerSound.Play();
				break;
			case "GGPropBurstBullet(Clone)":
				if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
				{
					GrowthManagerKit.AddEProperty(SceneEnchantmentProps.MBurstBullet30S);
					this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.BurstBullet = 1;
					other.gameObject.GetComponent<BoxCollider>().enabled = false;
					other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
					GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
					this.PropTriggerSound.Play();
				}
				break;
			case "GGPropDamageImmune(Clone)":
				if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
				{
					GrowthManagerKit.AddEProperty(SceneEnchantmentProps.MIgnoreDamage10S);
					this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.DamageImmune = 1;
					other.gameObject.GetComponent<BoxCollider>().enabled = false;
					other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
					GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
					this.PropTriggerSound.Play();
				}
				break;
			case "GGPropAntivenom(Clone)":
				if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
				{
					GrowthManagerKit.AddEProperty(SceneEnchantmentProps.MAntiVirusUntilDead);
					this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.Antivenom = 1;
					other.gameObject.GetComponent<BoxCollider>().enabled = false;
					other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
					GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
					this.PropTriggerSound.Play();
				}
				break;
			case "GGPropSpeedTrap(Clone)":
				if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
				{
					GrowthManagerKit.AddEProperty(SceneEnchantmentProps.MSpeedTrap20S);
					GGMessage ggmessage = new GGMessage();
					ggmessage.messageType = GGMessageType.MessageNotifyMutationTrap;
					ggmessage.messageContent = new GGMessageContent();
					ggmessage.messageContent.X = other.GetComponent<Collider>().gameObject.transform.position.x;
					ggmessage.messageContent.Y = other.GetComponent<Collider>().gameObject.transform.position.y + 0.3f;
					ggmessage.messageContent.Z = other.GetComponent<Collider>().gameObject.transform.position.z;
					GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
					other.gameObject.GetComponent<BoxCollider>().enabled = false;
					other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
					GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
					this.PropTriggerSound.Play();
				}
				break;
			case "GGPropInvisiblePotion(Clone)":
				if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
				{
					GrowthManagerKit.AddEProperty(SceneEnchantmentProps.MInvisibleBuff20S);
					this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.InvisiblePotion = 1;
					other.gameObject.GetComponent<BoxCollider>().enabled = false;
					other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
					GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
					this.CharacterInvisibleEffectShow();
					this.PropTriggerSound.Play();
				}
				break;
			case "GGPropHpRecover_HuntingMode(Clone)":
				this.mNetworkCharacter.mBlood += 30;
				this.mNetworkCharacter.mBlood = Math.Min(this.mNetworkCharacter.mBlood, (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue)));
				other.gameObject.GetComponent<BoxCollider>().enabled = false;
				other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
				GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
				this.PropTriggerSound.Play();
				break;
			case "GGPropBullet_HuntingMode(Clone)":
				this.mWeaponManager.PickUpBullet();
				other.gameObject.GetComponent<BoxCollider>().enabled = false;
				other.gameObject.transform.Find("propRotate/propmesh").GetComponent<Renderer>().enabled = false;
				GGNetworkKit.mInstance.DestroySceneObjectRPC(other.gameObject);
				this.PropTriggerSound.Play();
				break;
			}
		}
		if (this.IsSnowFallEffectShow && other.gameObject.name == "SnowFallEffectTrigger")
		{
			this.SnowFallEffect.SetActive(false);
		}
		if (other.gameObject.name == "StrongholdArea1")
		{
			this.StrongholdInArea1 = true;
			this.StrongholdInArea2 = false;
			this.StrongholdInArea3 = false;
		}
		else if (other.gameObject.name == "StrongholdArea2")
		{
			this.StrongholdInArea1 = false;
			this.StrongholdInArea2 = true;
			this.StrongholdInArea3 = false;
		}
		else if (other.gameObject.name == "StrongholdArea3")
		{
			this.StrongholdInArea1 = false;
			this.StrongholdInArea2 = false;
			this.StrongholdInArea3 = true;
		}
		if (other.gameObject.name == "ExplosionModeBombInstallPosition1")
		{
			if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
			{
				if (this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb)
				{
					this.ShowInstallBombButton();
					this.timerBombPositionIndex = 1;
				}
			}
			else if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
			{
				this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
				if (this.mGlobalInfo.modeInfo.IsTimerBombInstalled && this.mGlobalInfo.modeInfo.bombPositionId == 1)
				{
					this.ShowUninstallBombButton();
				}
			}
		}
		if (other.gameObject.name == "ExplosionModeBombInstallPosition2")
		{
			if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
			{
				if (this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb)
				{
					this.ShowInstallBombButton();
					this.timerBombPositionIndex = 2;
				}
			}
			else if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
			{
				this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
				if (this.mGlobalInfo.modeInfo.IsTimerBombInstalled && this.mGlobalInfo.modeInfo.bombPositionId == 2)
				{
					this.ShowUninstallBombButton();
				}
			}
		}
		if (other.gameObject.name == "TimerBomb" && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red && this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb = true;
			UIModeDirector.mInstance.ShowCarryBombLogo();
			this.SendTimerBombActiveInSceneMessage(false);
			other.gameObject.GetComponent<SphereCollider>().enabled = false;
			other.gameObject.transform.GetComponent<Renderer>().enabled = false;
			GGNetworkKit.mInstance.DestroySceneObjectRPC(other.transform.root.gameObject);
			this.PropTriggerSound.Play();
			this.SendTimerBombTakeMessage();
		}
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x000946B0 File Offset: 0x00092AB0
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "SpeedTrap(Clone)" && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
		{
			this.IsSpeedTrapTrigger = false;
			this.mWeaponManager.ChangeMoveSpeed(7f);
			this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.SpeedTrap = 0;
		}
		if (this.IsSnowFallEffectShow && other.gameObject.name == "SnowFallEffectTrigger")
		{
			this.SnowFallEffect.SetActive(true);
		}
		if (other.gameObject.name == "StrongholdArea1")
		{
			this.StrongholdInArea1 = false;
		}
		else if (other.gameObject.name == "StrongholdArea2")
		{
			this.StrongholdInArea2 = false;
		}
		else if (other.gameObject.name == "StrongholdArea3")
		{
			this.StrongholdInArea3 = false;
		}
		if (other.gameObject.name == "ExplosionModeBombInstallPosition1")
		{
			if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
			{
				if (this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb)
				{
					this.HideInstallBombButton();
					this.showInstallSchedule = false;
					this.ScheduleSprite.fillAmount = 0f;
				}
			}
			else if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
			{
				this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
				if (this.mGlobalInfo.modeInfo.IsTimerBombInstalled && this.mGlobalInfo.modeInfo.bombPositionId == 1)
				{
					this.HideUninstallBombButton();
					this.showRemoveSchedule = false;
					this.ScheduleSprite.fillAmount = 0f;
				}
			}
		}
		if (other.gameObject.name == "ExplosionModeBombInstallPosition2")
		{
			if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
			{
				if (this.mNetworkCharacter.mPlayerProperties.isTakeTimerBomb)
				{
					this.HideInstallBombButton();
					this.showInstallSchedule = false;
					this.ScheduleSprite.fillAmount = 0f;
				}
			}
			else if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
			{
				this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
				if (this.mGlobalInfo.modeInfo.IsTimerBombInstalled && this.mGlobalInfo.modeInfo.bombPositionId == 2)
				{
					this.HideUninstallBombButton();
					this.showRemoveSchedule = false;
					this.ScheduleSprite.fillAmount = 0f;
				}
			}
		}
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x00094960 File Offset: 0x00092D60
	public void ShowInstallBombButton()
	{
		this.InstallBombButtonObj.SetActive(true);
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x0009496E File Offset: 0x00092D6E
	public void HideInstallBombButton()
	{
		this.installBombSound.Stop();
		this.InstallBombButtonObj.GetComponent<UITouchMonitor>().isPressing = true;
		this.InstallBombButtonObj.SetActive(false);
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x00094998 File Offset: 0x00092D98
	public void ShowUninstallBombButton()
	{
		this.UninstallBombButtonObj.SetActive(true);
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.GetGlobalInfo();
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x000949B6 File Offset: 0x00092DB6
	public void HideUninstallBombButton()
	{
		this.uninstallBombSound.Stop();
		this.UninstallBombButtonObj.GetComponent<UITouchMonitor>().isPressing = true;
		this.UninstallBombButtonObj.SetActive(false);
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x000949E0 File Offset: 0x00092DE0
	public void OnInstallBtnPressed()
	{
		this.showInstallSchedule = true;
		this.installBombSound.Play();
		this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
		this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
		this.mNetworkCharacter.mGearType = 1;
		this.weaponCamera.GetComponent<Camera>().cullingMask = 512;
		this.GearManager[0].SetActive(true);
		this.GearManager[0].BroadcastMessage("GearUse", SendMessageOptions.DontRequireReceiver);
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x00094A5C File Offset: 0x00092E5C
	public void OnInstallBtnReleased()
	{
		this.showInstallSchedule = false;
		this.ScheduleSprite.fillAmount = 0f;
		this.installBombSound.Stop();
		this.mNetworkCharacter.mGearType = 0;
		this.weaponCamera.GetComponent<Camera>().cullingMask = 256;
		this.GearManager[0].SetActive(false);
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x00094ABA File Offset: 0x00092EBA
	public void OnUninstallBtnPressed()
	{
		this.showRemoveSchedule = true;
		this.uninstallBombSound.Play();
		this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
		this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x00094AE6 File Offset: 0x00092EE6
	public void OnUninstallBtnReleased()
	{
		this.showRemoveSchedule = false;
		this.ScheduleSprite.fillAmount = 0f;
		this.uninstallBombSound.Stop();
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x00094B0C File Offset: 0x00092F0C
	private IEnumerator HideTeamWinTipLabel(float delay)
	{
		yield return new WaitForSeconds(delay);
		UIModeDirector.mInstance.HideTeamWinSpriteTip();
		yield break;
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x00094B28 File Offset: 0x00092F28
	private IEnumerator SpeedTrapReturn(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (this.IsSpeedTrapTrigger)
		{
			this.IsSpeedTrapTrigger = false;
			this.mWeaponManager.ChangeMoveSpeed(7f);
		}
		this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.SpeedTrap = 0;
		yield break;
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x00094B4C File Offset: 0x00092F4C
	public void FlashBombRender(float distance)
	{
		this.FlashBombShow.SetActive(true);
		float num = 1f - distance * 0.01f;
		this.FlashBombShow.GetComponent<UITexture>().alpha = 1f;
		float durationTime = (1f - distance * 0.01f) * 4f;
		base.StartCoroutine(this.FlashBombEffectDisappear(durationTime));
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x00094BAC File Offset: 0x00092FAC
	private IEnumerator FlashBombEffectDisappear(float durationTime)
	{
		yield return new WaitForSeconds(durationTime);
		this.startDisappear = true;
		yield break;
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x00094BD0 File Offset: 0x00092FD0
	public void BlindRender()
	{
		this.BlindEffectShow.SetActive(true);
		this.BlindEffectShow.GetComponent<UITexture>().alpha = 1f;
		this.mNetworkCharacter.mPlayerProperties.MutationSkill.Blind = 1;
		base.StartCoroutine(this.BlindEffectDisappear());
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x00094C24 File Offset: 0x00093024
	private IEnumerator BlindEffectDisappear()
	{
		yield return new WaitForSeconds(2f);
		this.mNetworkCharacter.mPlayerProperties.MutationSkill.Blind = 0;
		this.blindStartDisappear = true;
		yield break;
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x00094C3F File Offset: 0x0009303F
	public void Nightmare()
	{
		this.NightmareEffectShow.SetActive(true);
		this.NightmareEffectShow.GetComponent<UITexture>().alpha = 0.75f;
		base.StartCoroutine(this.NightmareEffectDisappear());
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x00094C70 File Offset: 0x00093070
	private IEnumerator NightmareEffectDisappear()
	{
		yield return new WaitForSeconds(0.5f);
		this.nightmareStartDisappear = true;
		yield break;
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x00094C8C File Offset: 0x0009308C
	private IEnumerator SelfExplosionReturn(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.mNetworkCharacter.mPlayerProperties.MutationSkill.SelfExplosion = 0;
		yield break;
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x00094CB0 File Offset: 0x000930B0
	private IEnumerator DamageImmuneWhenRespawn()
	{
		this.mNetworkCharacter.mPlayerProperties.DamageImmuneWhenRespawn = true;
		yield return new WaitForSeconds(3f);
		this.mNetworkCharacter.mPlayerProperties.DamageImmuneWhenRespawn = false;
		yield break;
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x00094CCC File Offset: 0x000930CC
	public void Mutation(int zombieLv)
	{
		if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
		{
			this.ClearAllBufferWhenMutation();
			this.mNetworkCharacter.mPlayerProperties.team = GGTeamType.red;
			this.mNetworkCharacter.mBlood = GGMutationModeControl.ZombieProperty.GetMaxBloodWithLv(zombieLv);
			this.mNetworkCharacter.myArmorInfo.mDurabilityInGame = 0;
			this.mWeaponManager.SwitchWeaponToZombieHand();
			if (zombieLv == 6)
			{
				this.mNetworkCharacter.zombieSkinIndex = 1;
				this.mWeaponManager.SetZombiehandMaterial(this.mNetworkCharacter.zombieSkinIndex);
				UIModeDirector.mInstance.RefreshToZombieUI("Frighten");
			}
			else if (zombieLv == 5)
			{
				this.mNetworkCharacter.zombieSkinIndex = UnityEngine.Random.Range(2, 6);
				this.mWeaponManager.SetZombiehandMaterial(this.mNetworkCharacter.zombieSkinIndex);
				string zombieSkillLogoName = string.Empty;
				switch (this.mNetworkCharacter.zombieSkinIndex)
				{
				case 2:
					zombieSkillLogoName = "HPGenerate";
					break;
				case 3:
					zombieSkillLogoName = "VenomBlow";
					break;
				case 4:
					zombieSkillLogoName = "Blind";
					break;
				case 5:
					zombieSkillLogoName = "SelfExplosion";
					break;
				}
				UIModeDirector.mInstance.RefreshToZombieUI(zombieSkillLogoName);
			}
			int num = (zombieLv != 6) ? -500 : 200;
			GGMutationModeControl mInstance = GGMutationModeControl.mInstance;
			mInstance.score += (short)num;
			GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.ScoreInMutation, num, string.Empty);
			this.mNetworkCharacter.mPlayerProperties.MutationModeScore = GGMutationModeControl.mInstance.score;
			if (zombieLv == 6)
			{
				GGNetworkSystemMessage ggnetworkSystemMessage = new GGNetworkSystemMessage();
				ggnetworkSystemMessage.content = this.mNetworkCharacter.mPlayerProperties.name + " mutation to zombie!";
				ggnetworkSystemMessage.color = GGColor.Red;
				GGNetworkChat.mInstance.SystemMessage(ggnetworkSystemMessage);
			}
			if (zombieLv == 5)
			{
				GGNetworkSystemMessage ggnetworkSystemMessage2 = new GGNetworkSystemMessage();
				ggnetworkSystemMessage2.content = this.mNetworkCharacter.mPlayerProperties.name + " was infected!";
				ggnetworkSystemMessage2.color = GGColor.Red;
				GGNetworkChat.mInstance.SystemMessage(ggnetworkSystemMessage2);
			}
			this.mNetworkCharacter.mZombieLv = zombieLv;
			if (zombieLv == 6)
			{
				this.zombieMutation.Play();
			}
		}
		else
		{
			this.mNetworkCharacter.mBlood = GGMutationModeControl.ZombieProperty.GetMaxBloodWithLv(zombieLv);
			if (zombieLv == 5)
			{
				this.mNetworkCharacter.zombieSkinIndex = UnityEngine.Random.Range(2, 6);
				this.mWeaponManager.SetZombiehandMaterial(this.mNetworkCharacter.zombieSkinIndex);
				string zombieSkillLogoName2 = string.Empty;
				switch (this.mNetworkCharacter.zombieSkinIndex)
				{
				case 2:
					zombieSkillLogoName2 = "HPGenerate";
					break;
				case 3:
					zombieSkillLogoName2 = "VenomBlow";
					break;
				case 4:
					zombieSkillLogoName2 = "Blind";
					break;
				case 5:
					zombieSkillLogoName2 = "SelfExplosion";
					break;
				}
				UIModeDirector.mInstance.RefreshToZombieUI(zombieSkillLogoName2);
			}
			GGMutationModeControl mInstance2 = GGMutationModeControl.mInstance;
			mInstance2.score -= (short)((zombieLv + 1) * 20);
			GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.ScoreInMutation, -(zombieLv + 1) * 20, string.Empty);
			this.mNetworkCharacter.mPlayerProperties.MutationModeScore = GGMutationModeControl.mInstance.score;
			this.mNetworkCharacter.mZombieLv = zombieLv;
		}
		this.mWeaponManager.ChangeMoveSpeed(7f);
		this.IsSpeedTrapTrigger = false;
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x0009501C File Offset: 0x0009341C
	private void ChangeMatrixZombie()
	{
		GGMutationModeControl.mInstance.zombielv = 6;
		this.Mutation(6);
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x00095030 File Offset: 0x00093430
	public void MutationModeChangePositionToGameScene()
	{
		this.mNetworkCharacter.mPlayerProperties.MutationModeScore = GGMutationModeControl.mInstance.score;
		this.MutationModeRandomPosition();
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x00095054 File Offset: 0x00093454
	public void HuntingModeChangePositionToGameScene()
	{
		this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
		this.mainCamera.rotation = new Quaternion(0f, 0f, 0f, 0f);
		this.mainCamera.GetComponent<Camera>().fieldOfView = 47f;
		this.weaponCamera.GetComponent<Camera>().cullingMask = 256;
		this.mWeaponManager.SetLaserR7_LaserScope(true);
		this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
		this.HuntingModeRandomPosition();
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x000950FB File Offset: 0x000934FB
	public void HuntingModePlayerEnterAttackZone()
	{
		GrowthManagerKit.AddCustomEProperty(SceneEnchantmentProps.HCustomDamagePlus, 1f);
		this.isInAttackZone_HuntingMode = true;
		this.isInDefendZone_HuntingMode = false;
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x00095117 File Offset: 0x00093517
	public void HuntingModePlayerLeaveAttackZone()
	{
		GrowthManagerKit.RemoveEProperty(SceneEnchantmentProps.HCustomDamagePlus);
		this.isInAttackZone_HuntingMode = false;
		this.isInDefendZone_HuntingMode = false;
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x0009512E File Offset: 0x0009352E
	public void HuntingModePlayerEnterDefendZone()
	{
		GrowthManagerKit.AddCustomEProperty(SceneEnchantmentProps.HCustomDamagePlus, -0.5f);
		this.isInDefendZone_HuntingMode = true;
		this.isInAttackZone_HuntingMode = false;
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x0009514A File Offset: 0x0009354A
	public void HuntingModePlayerLeaveDefendZone()
	{
		GrowthManagerKit.RemoveEProperty(SceneEnchantmentProps.HCustomDamagePlus);
		this.isInDefendZone_HuntingMode = false;
		this.isInAttackZone_HuntingMode = false;
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x00095164 File Offset: 0x00093564
	public void OnUIRespawn()
	{
		this.SetGrayscaleEffectDisappear();
		this.quickRespawnTag = true;
		this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
		this.mainCamera.rotation = new Quaternion(0f, 0f, 0f, 0f);
		this.mainCamera.GetComponent<Camera>().fieldOfView = 47f;
		this.weaponCamera.GetComponent<Camera>().cullingMask = 256;
		this.mWeaponManager.SetLaserR7_LaserScope(true);
		this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
		this.HuntingModeRandomPosition();
		base.StartCoroutine(this.DamageImmuneWhenRespawn());
		this.ClearAllBufferOfPropAfterDie();
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x0009522C File Offset: 0x0009362C
	private void OnScenePropsInvalid(SceneEnchantmentProps type)
	{
		switch (type)
		{
		case SceneEnchantmentProps.TopSpeed:
			this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.SpeedEnhance = 0;
			break;
		case SceneEnchantmentProps.JumpPlus50:
			this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.JumpEnhance = 0;
			break;
		case SceneEnchantmentProps.DamagePlus50:
			this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.AttackEnhance = 0;
			break;
		case SceneEnchantmentProps.DamageReducation50:
			this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.ArmorEnhance = 0;
			break;
		default:
			switch (type)
			{
			case SceneEnchantmentProps.MBurstBullet30S:
				this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.BurstBullet = 0;
				break;
			case SceneEnchantmentProps.MIgnoreDamage10S:
				this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.DamageImmune = 0;
				break;
			case SceneEnchantmentProps.MAntiVirusUntilDead:
				this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.Antivenom = 0;
				break;
			case SceneEnchantmentProps.MInvisibleBuff20S:
				this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.InvisiblePotion = 0;
				this.CharacterInvisibleEffectDisappear();
				break;
			}
			break;
		}
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x00095358 File Offset: 0x00093758
	public void ClearAllBufferOfPropAfterDie()
	{
		GrowthManagerKit.ClearScenePropsEProperty();
		this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.AttackEnhance = 0;
		this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.ArmorEnhance = 0;
		this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.SpeedEnhance = 0;
		this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.JumpEnhance = 0;
		this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.BurstBullet = 0;
		this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.DamageImmune = 0;
		this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.Antivenom = 0;
		this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.SpeedTrap = 0;
		this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.InvisiblePotion = 0;
		this.CharacterInvisibleEffectDisappear();
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x00095438 File Offset: 0x00093838
	public void ClearAllBufferWhenMutation()
	{
		GrowthManagerKit.ClearAllEProperty();
		this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.AttackEnhance = 0;
		this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.ArmorEnhance = 0;
		this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.SpeedEnhance = 0;
		this.mNetworkCharacter.mPlayerProperties.CommonPropTrigger.JumpEnhance = 0;
		this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.BurstBullet = 0;
		this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.DamageImmune = 0;
		this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.Antivenom = 0;
		this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.SpeedTrap = 0;
		this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.InvisiblePotion = 0;
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x00095510 File Offset: 0x00093910
	public IEnumerator InvisibleTriggerAfterKilling(float delaytime)
	{
		this.mNetworkCharacter.mPlayerProperties.DecorationSkill.SelfInvisible = 1;
		this.CharacterInvisibleEffectShow();
		yield return new WaitForSeconds(delaytime);
		this.mNetworkCharacter.mPlayerProperties.DecorationSkill.SelfInvisible = 0;
		if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.InvisiblePotion == 0 && this.mNetworkCharacter.mPlayerProperties.DecorationSkill.SelfInvisible == 0)
		{
			this.CharacterInvisibleEffectDisappear();
		}
		yield break;
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x00095532 File Offset: 0x00093932
	private void CharacterInvisibleEffectShow()
	{
		this.CharacterHandMaterial.shader = Shader.Find("Particles/Additive");
		this.CharacterHandMaterial.color = new Color(1f, 1f, 1f, 0.5f);
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x0009556D File Offset: 0x0009396D
	private void CharacterInvisibleEffectDisappear()
	{
		this.CharacterHandMaterial.shader = Shader.Find("Diffuse");
		this.CharacterHandMaterial.color = new Color(1f, 1f, 1f, 0f);
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x000955A8 File Offset: 0x000939A8
	public void BeJingxiaEffect()
	{
		if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
		{
			base.GetComponent<CharacterMotorCS>().canMove = false;
			this.mNetworkCharacter.mPlayerProperties.MutationSkill.PassiveHorror = 1;
			base.StartCoroutine(this.BeJingxiaEffectReturn(2f));
		}
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x00095600 File Offset: 0x00093A00
	private IEnumerator BeJingxiaEffectReturn(float delay)
	{
		yield return new WaitForSeconds(delay);
		base.GetComponent<CharacterMotorCS>().canMove = true;
		this.mNetworkCharacter.mPlayerProperties.MutationSkill.PassiveHorror = 0;
		yield break;
	}

	// Token: 0x040012FA RID: 4858
	public DateTime lastDateTime;

	// Token: 0x040012FB RID: 4859
	public int killedNum;

	// Token: 0x040012FC RID: 4860
	public int deadNum;

	// Token: 0x040012FD RID: 4861
	private bool isTouchDeathTrigger;

	// Token: 0x040012FE RID: 4862
	private AudioSource audioEnemyDie;

	// Token: 0x040012FF RID: 4863
	private AudioSource audioBulletHit;

	// Token: 0x04001300 RID: 4864
	private AudioSource twoKill;

	// Token: 0x04001301 RID: 4865
	private AudioSource fourKill;

	// Token: 0x04001302 RID: 4866
	private AudioSource sixKill;

	// Token: 0x04001303 RID: 4867
	private AudioSource eightKill;

	// Token: 0x04001304 RID: 4868
	private AudioSource tenKill;

	// Token: 0x04001305 RID: 4869
	private AudioSource headShotKill;

	// Token: 0x04001306 RID: 4870
	private int killNumNoDie;

	// Token: 0x04001307 RID: 4871
	private AudioSource timerKill;

	// Token: 0x04001308 RID: 4872
	public AudioClip[] timerKillclip;

	// Token: 0x04001309 RID: 4873
	private int TimerkillNum;

	// Token: 0x0400130A RID: 4874
	private float killTimer;

	// Token: 0x0400130B RID: 4875
	private AudioSource zombieDie;

	// Token: 0x0400130C RID: 4876
	private AudioSource zombieMutation;

	// Token: 0x0400130D RID: 4877
	private float headRate;

	// Token: 0x0400130E RID: 4878
	private float bodyRate;

	// Token: 0x0400130F RID: 4879
	private GameObject UIGameSceneDirectorForArmAndBlood;

	// Token: 0x04001310 RID: 4880
	private float ignoreHeadshotTimeCache;

	// Token: 0x04001311 RID: 4881
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x04001312 RID: 4882
	private CharacterMotorCS mCharacterMotorCS;

	// Token: 0x04001313 RID: 4883
	public Material CharacterHandMaterial;

	// Token: 0x04001314 RID: 4884
	private GameObject PlayerBloodShow;

	// Token: 0x04001315 RID: 4885
	private GameObject FlashBombShow;

	// Token: 0x04001316 RID: 4886
	private GameObject BlindEffectShow;

	// Token: 0x04001317 RID: 4887
	private GameObject NightmareEffectShow;

	// Token: 0x04001318 RID: 4888
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x04001319 RID: 4889
	private RaycastHit hit;

	// Token: 0x0400131A RID: 4890
	private Transform mainCamera;

	// Token: 0x0400131B RID: 4891
	private Transform weaponCamera;

	// Token: 0x0400131C RID: 4892
	private float preBloodIncreseVar;

	// Token: 0x0400131D RID: 4893
	private float curBloodIncreseVar;

	// Token: 0x0400131E RID: 4894
	private float RecoveryBloodTime;

	// Token: 0x0400131F RID: 4895
	private GameObject PhotonGame;

	// Token: 0x04001320 RID: 4896
	private bool DeathRotation;

	// Token: 0x04001321 RID: 4897
	public GrayscaleEffect grayscaleEffect;

	// Token: 0x04001322 RID: 4898
	public GrayscaleEffectAnimation grayscaleEffectAnimation;

	// Token: 0x04001323 RID: 4899
	private bool isGrayscaleEffect;

	// Token: 0x04001324 RID: 4900
	private GGWeaponManager mWeaponManager;

	// Token: 0x04001325 RID: 4901
	private bool IsDeathTriggerInMutationMode;

	// Token: 0x04001326 RID: 4902
	private int DeathTriggerCount;

	// Token: 0x04001327 RID: 4903
	private bool IsKilledInMutationMode;

	// Token: 0x04001328 RID: 4904
	private bool IsKilledByBombInMutaionMode;

	// Token: 0x04001329 RID: 4905
	private int KillerIdInMutationMode;

	// Token: 0x0400132A RID: 4906
	private float MutationModeRecoveryBloodTime;

	// Token: 0x0400132B RID: 4907
	private float MutationModeHumanRecoveryBloodTime;

	// Token: 0x0400132C RID: 4908
	private float MutationModeRecoveryBloodTimeForHPRecoverSkill;

	// Token: 0x0400132D RID: 4909
	private float SpeedRecoverTime;

	// Token: 0x0400132E RID: 4910
	private bool IsSpeedTrapTrigger;

	// Token: 0x0400132F RID: 4911
	private bool StrongholdInArea1;

	// Token: 0x04001330 RID: 4912
	private bool StrongholdInArea2;

	// Token: 0x04001331 RID: 4913
	private bool StrongholdInArea3;

	// Token: 0x04001332 RID: 4914
	private List<GameObject> BluePlayerObserverCamera = new List<GameObject>();

	// Token: 0x04001333 RID: 4915
	private List<GameObject> RedPlayerObserverCamera = new List<GameObject>();

	// Token: 0x04001334 RID: 4916
	public GameObject ObserverCamera;

	// Token: 0x04001335 RID: 4917
	private int ObserverCameraIndex;

	// Token: 0x04001336 RID: 4918
	public bool showInstallSchedule;

	// Token: 0x04001337 RID: 4919
	public bool showRemoveSchedule;

	// Token: 0x04001338 RID: 4920
	private UISprite ScheduleSprite;

	// Token: 0x04001339 RID: 4921
	private int timerBombPositionIndex;

	// Token: 0x0400133A RID: 4922
	private float CheckObserverTargetTimeCount;

	// Token: 0x0400133B RID: 4923
	private float TimerBombSynPositionTimeCount;

	// Token: 0x0400133C RID: 4924
	public GameObject[] GearManager;

	// Token: 0x0400133D RID: 4925
	private GameObject InstallBombButtonObj;

	// Token: 0x0400133E RID: 4926
	private GameObject UninstallBombButtonObj;

	// Token: 0x0400133F RID: 4927
	private bool explosionModeHasStart;

	// Token: 0x04001340 RID: 4928
	private AudioSource installBombSound;

	// Token: 0x04001341 RID: 4929
	private AudioSource uninstallBombSound;

	// Token: 0x04001342 RID: 4930
	private AudioSource BlueTeamWin;

	// Token: 0x04001343 RID: 4931
	private AudioSource RedTeamWin;

	// Token: 0x04001344 RID: 4932
	private AudioSource EarthShakeEffectSound;

	// Token: 0x04001345 RID: 4933
	private UISprite CrosshairSprite;

	// Token: 0x04001346 RID: 4934
	public GameObject SnowFallEffect;

	// Token: 0x04001347 RID: 4935
	private bool IsSnowFallEffectShow;

	// Token: 0x04001348 RID: 4936
	public GameObject RainFallEffect;

	// Token: 0x04001349 RID: 4937
	public AudioSource PropTriggerSound;

	// Token: 0x0400134A RID: 4938
	public bool isOnPauseStart;

	// Token: 0x0400134B RID: 4939
	private bool isAutoMove;

	// Token: 0x0400134C RID: 4940
	private float AutoMoveTimer;

	// Token: 0x0400134D RID: 4941
	private Vector3 AutoMovement = Vector3.zero;

	// Token: 0x0400134E RID: 4942
	private bool isAcidRainEffect;

	// Token: 0x0400134F RID: 4943
	private float HuntingModeAcidRainTime;

	// Token: 0x04001350 RID: 4944
	public int HuntingModeDeadCount;

	// Token: 0x04001351 RID: 4945
	private bool isInAttackZone_HuntingMode;

	// Token: 0x04001352 RID: 4946
	private bool isInDefendZone_HuntingMode;

	// Token: 0x04001353 RID: 4947
	private int EarthShakeCount = 4;

	// Token: 0x04001354 RID: 4948
	public int _HuntingModeMaxDeadCount = 6;

	// Token: 0x04001355 RID: 4949
	public string StrEncryptHuntingModeMaxDeadCount = string.Empty;

	// Token: 0x04001356 RID: 4950
	private bool quickRespawnTag;

	// Token: 0x04001357 RID: 4951
	private bool startDisappear;

	// Token: 0x04001358 RID: 4952
	private bool blindStartDisappear;

	// Token: 0x04001359 RID: 4953
	private bool nightmareStartDisappear;
}
