using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000225 RID: 549
public class GGWeaponManager : MonoBehaviour
{
	// Token: 0x06000EB4 RID: 3764 RVA: 0x0007AD36 File Offset: 0x00079136
	private void SetMyTag(string id)
	{
		this.onlinePlayerTag = id;
	}

	// Token: 0x06000EB5 RID: 3765 RVA: 0x0007AD3F File Offset: 0x0007913F
	private void SetGunLv(string param)
	{
	}

	// Token: 0x06000EB6 RID: 3766 RVA: 0x0007AD44 File Offset: 0x00079144
	private void Awake()
	{
		if (Application.loadedLevelName == "UIHelp")
		{
			this.isUIHelp = true;
		}
		this.mNetworkCharacter = base.transform.root.GetComponent<GGNetworkCharacter>();
		this.mMotorCS = base.transform.root.GetComponent<CharacterMotorCS>();
		this.mNetworkPlayerLogic = base.transform.root.GetComponent<GGNetWorkPlayerlogic>();
		this.preState = this.mNetworkCharacter.mCharacterWalkState;
		UIPlayDirector.OnReload += this.OnReload;
		UIPlayDirector.OnFireStart += this.OnFireStart;
		UIPlayDirector.OnFireEnd += this.OnFireEnd;
		UIPlayDirector.OnSwitchLeft += this.OnSwitchLeft;
		UIPlayDirector.OnSwitchRight += this.OnSwitchRight;
		UIPlayDirector.OnAim += this.OnAim;
		UIPlayDirector.OnSniperCancel += this.OnSniperCancel;
		UIPlayDirector.OnAddBullet += this.OnAddBullet;
		UIPlayDirector.OnThrowWeaponFire += this.OnThrowWeaponFire;
		UIHelpDirector.OnHelpReload += this.OnHelpReload;
		UIHelpDirector.OnHelpFireStart += this.OnHelpFireStart;
		UIHelpDirector.OnHelpFireEnd += this.OnHelpFireEnd;
		UIHelpDirector.OnHelpSwitchLeft += this.OnHelpSwitchLeft;
		UIHelpDirector.OnHelpSwitchRight += this.OnHelpSwitchRight;
		UIHelpDirector.OnHelpAddBullet += this.OnHelpAddBullet;
		UIHelpDirector.OnHelpThrowWeaponFire += this.OnHelpThrowWeaponFire;
		UIHelpDirector.OnHelpArmor += this.OnHelpArmor;
		UIHelpDirector.OnHelpMediKit += this.OnHelpMediKit;
		this.Init();
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x0007AEF8 File Offset: 0x000792F8
	private void Init()
	{
		if (!this.isUIHelp)
		{
			GWeaponItemInfo[] curEquippedWeaponItemInfoList = GrowthManagerKit.GetCurEquippedWeaponItemInfoList();
			if (GGNetworkKit.mInstance.GetGameMode() != GGModeType.KnifeCompetition)
			{
				if (curEquippedWeaponItemInfoList != null)
				{
					for (int i = 0; i < curEquippedWeaponItemInfoList.Length; i++)
					{
						this.allWeapons.Add(curEquippedWeaponItemInfoList[i]);
					}
				}
				else
				{
					GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName("GLOCK21");
					this.allWeapons.Add(weaponItemInfoByName);
				}
				GWeaponItemInfo[] userAllEnabledWeaponItemInfo = GrowthManagerKit.GetUserAllEnabledWeaponItemInfo();
				for (int j = 0; j < userAllEnabledWeaponItemInfo.Length; j++)
				{
					if (userAllEnabledWeaponItemInfo[j].mGunType == "Thrown")
					{
						this.allThrown.Add(userAllEnabledWeaponItemInfo[j]);
					}
				}
				GWeaponItemInfo weaponItemInfoByName2 = GrowthManagerKit.GetWeaponItemInfoByName("ZombieHand");
				this.allZombiehand.Add(weaponItemInfoByName2);
			}
			else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.KnifeCompetition)
			{
				bool flag = false;
				if (curEquippedWeaponItemInfoList != null)
				{
					for (int k = 0; k < curEquippedWeaponItemInfoList.Length; k++)
					{
						if (curEquippedWeaponItemInfoList[k].mGunType == "Knife")
						{
							this.allWeapons.Add(curEquippedWeaponItemInfoList[k]);
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					GWeaponItemInfo weaponItemInfoByName3 = GrowthManagerKit.GetWeaponItemInfoByName("BallisticKnife");
					this.allWeapons.Add(weaponItemInfoByName3);
				}
			}
			this.LoadEquipedWeapons();
			this.TakeFirstWeapon();
		}
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x0007B05C File Offset: 0x0007945C
	private void InitUIHelp()
	{
		if (this.isUIHelp)
		{
			GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName("MP5KA5");
			this.allWeapons.Add(weaponItemInfoByName);
			GWeaponItemInfo weaponItemInfoByName2 = GrowthManagerKit.GetWeaponItemInfoByName("AK47");
			this.allWeapons.Add(weaponItemInfoByName2);
			GWeaponItemInfo weaponItemInfoByName3 = GrowthManagerKit.GetWeaponItemInfoByName("M67");
			this.allThrown.Add(weaponItemInfoByName3);
			this.mNetworkCharacter.mBlood = 50;
			this.mNetworkCharacter.myArmorInfo.mDurabilityInGame = 50;
			this.LoadEquipedWeapons();
			this.TakeFirstWeapon();
		}
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x0007B0E4 File Offset: 0x000794E4
	private void Start()
	{
		this.InitUIHelp();
		if (base.transform.root.tag == "Player" && !this.isUIHelp)
		{
			GGWeaponManager.mInstance = this;
			this.CrosshairSprite = UIPlayDirector.mInstance.crosshairObj;
			this.AimButton = UIPlayDirector.mInstance.sniperBtnObj;
			this.SniperCancelButton = UIPlayDirector.mInstance.sniperCancelBtnObj;
			this.scopeSprite = UIPlayDirector.mInstance.sniperEffectObj;
			this.scopeSprite.SetActive(false);
			this.preSniperMode = UIUserDataController.GetSniperMode();
			if (UIUserDataController.GetSniperMode() == 0)
			{
				if (this.SelectedWeapon.GunType == gunType.SniperRifle)
				{
					this.AimButton.SetActive(true);
				}
				else
				{
					this.AimButton.SetActive(false);
				}
			}
			if (this.SelectedWeapon.weaponName == "TeslaP1" || this.SelectedWeapon.weaponName == "Flamethrower")
			{
				if (UIPlayDirector.mInstance != null)
				{
					UIPlayDirector.mInstance.ShowEnergyBarNode();
				}
			}
			else if (UIPlayDirector.mInstance != null)
			{
				UIPlayDirector.mInstance.HideEnergyBarNode();
			}
		}
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x0007B224 File Offset: 0x00079624
	private void Update()
	{
		if ((double)Time.timeScale < 0.01)
		{
			return;
		}
		if (!this.isUIHelp)
		{
			if (this.SelectedWeapon.aimed)
			{
				if (this.CrosshairSprite != null && this.CrosshairSprite.activeSelf)
				{
					this.CrosshairSprite.SetActive(false);
				}
				if (this.scopeSprite != null && !this.scopeSprite.activeSelf)
				{
					this.scopeSprite.SetActive(true);
				}
				if (UIUserDataController.GetSniperMode() == 1)
				{
					if (this.SniperCancelButton != null && !this.SniperCancelButton.activeSelf)
					{
						this.SniperCancelButton.SetActive(true);
					}
				}
			}
			else
			{
				if (this.CrosshairSprite != null && !this.CrosshairSprite.activeSelf)
				{
					this.CrosshairSprite.SetActive(true);
				}
				if (this.scopeSprite != null && this.scopeSprite.activeSelf)
				{
					this.scopeSprite.SetActive(false);
				}
				if (UIUserDataController.GetSniperMode() == 1)
				{
					if (this.SniperCancelButton != null && this.SniperCancelButton.activeSelf)
					{
						this.SniperCancelButton.SetActive(false);
					}
				}
			}
			if (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Dead && this.SelectedWeapon.aimed)
			{
				this.SelectedWeapon.aimed = false;
			}
			if (this.SelectedWeapon.GunType == gunType.SniperRifle && this.preSniperMode != UIUserDataController.GetSniperMode())
			{
				if (UIUserDataController.GetSniperMode() == 0)
				{
					this.AimButton.SetActive(true);
				}
				else if (UIUserDataController.GetSniperMode() == 1)
				{
					this.AimButton.SetActive(false);
				}
				this.preSniperMode = UIUserDataController.GetSniperMode();
			}
			if (Time.frameCount % 15 == 0)
			{
				this.speedBuffValue_New = GrowthManagerKit.EProperty().allDic[EnchantmentType.SpeedPlus].additionValue;
				if (this.speedBuffValue_New != this.speedBuffValue_Old)
				{
					this.ChangeWeaponNameToIndex(this.SelectedWeapon.weaponName);
				}
				this.speedBuffValue_Old = this.speedBuffValue_New;
			}
			if (this.crosshairDynamic)
			{
				if (this.mNetworkCharacter.mCharacterCurFireState == GGCharacterFireState.Fire)
				{
					if (this.SelectedWeapon.singleFire)
					{
						this.curCrosshairScale += new Vector3(0.3f, 0.3f, 0f);
						this.curCrosshairScale = Vector3.Min(new Vector3(1.8f, 1.8f, 1f), this.curCrosshairScale);
						this.crosshairSmooth = 6f;
					}
					else
					{
						this.curCrosshairScale = new Vector3(2f, 2f, 0f);
						this.crosshairSmooth = 12f;
					}
				}
				else if (this.mNetworkCharacter.mCharacterCurFireState == GGCharacterFireState.Reload)
				{
					this.curCrosshairScale = new Vector3(1f, 1f, 0f);
					this.crosshairSmooth = 3f;
				}
				else
				{
					this.curCrosshairScale = new Vector3(1f, 1f, 0f);
					this.crosshairSmooth = 3f;
				}
				this.CrosshairSprite.transform.localScale = Vector3.Lerp(this.CrosshairSprite.transform.localScale, this.curCrosshairScale, Time.deltaTime * this.crosshairSmooth);
			}
		}
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x0007B5C0 File Offset: 0x000799C0
	public void SwitchWeaponRight()
	{
		if (this.allWeapons.Count < 2)
		{
			return;
		}
		int selectedWeaponIndex = this.SelectedWeaponIndex;
		this.SelectedWeaponIndex = ((this.SelectedWeaponIndex + 1 < this.allWeapons.Count) ? (this.SelectedWeaponIndex + 1) : 0);
		int selectedWeaponIndex2 = this.SelectedWeaponIndex;
		this.SwitchWeapons(selectedWeaponIndex, selectedWeaponIndex2);
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x0007B624 File Offset: 0x00079A24
	public void SwitchWeaponLeft()
	{
		if (this.allWeapons.Count < 2)
		{
			return;
		}
		int selectedWeaponIndex = this.SelectedWeaponIndex;
		this.SelectedWeaponIndex = ((this.SelectedWeaponIndex - 1 >= 0) ? (this.SelectedWeaponIndex - 1) : (this.allWeapons.Count - 1));
		int selectedWeaponIndex2 = this.SelectedWeaponIndex;
		this.SwitchWeapons(selectedWeaponIndex, selectedWeaponIndex2);
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x0007B688 File Offset: 0x00079A88
	private void LoadEquipedWeapons()
	{
		for (int i = 0; i < this.allWeapons.Count; i++)
		{
			GameObject gameObject = Resources.Load("Prefabs/Weapons_Local/" + this.allWeapons[i].mName) as GameObject;
			GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Weapons_Local/" + this.allWeapons[i].mName), Vector3.zero, Quaternion.identity) as GameObject;
			gameObject2.transform.parent = this.equipWeaponManager.transform;
			GGWeaponScript component = gameObject2.GetComponent<GGWeaponScript>();
			component.upgradeLv = this.allWeapons[i].mModelLv;
			component.SetUpgradeProperty(this.allWeapons[i]);
			component.SetWeaponPosition(gameObject.transform.localPosition, gameObject.transform.localEulerAngles, gameObject.transform.localScale);
			this.equipWeapons.Add(gameObject2);
			GGNetworkWeaponProperties ggnetworkWeaponProperties = new GGNetworkWeaponProperties();
			ggnetworkWeaponProperties.weaponType = this.allWeapons[i].mWeaponId;
			ggnetworkWeaponProperties.upgradeLv = this.allWeapons[i].mModelLv;
			this.plusedWeaponIndex.Add(ggnetworkWeaponProperties);
		}
		for (int j = 0; j < this.allThrown.Count; j++)
		{
			GameObject gameObject3 = Resources.Load("Prefabs/Weapons_Local/" + this.allThrown[j].mName) as GameObject;
			GameObject gameObject4 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Weapons_Local/" + this.allThrown[j].mName), Vector3.zero, Quaternion.identity) as GameObject;
			gameObject4.transform.parent = this.thrownManager.transform;
			GGWeaponScript component2 = gameObject4.GetComponent<GGWeaponScript>();
			component2.SetWeaponPosition(gameObject3.transform.localPosition, gameObject3.transform.localEulerAngles, gameObject3.transform.localScale);
			this.throwns.Add(gameObject4);
			GGNetworkWeaponProperties ggnetworkWeaponProperties2 = new GGNetworkWeaponProperties();
			ggnetworkWeaponProperties2.weaponType = this.allThrown[j].mWeaponId;
			ggnetworkWeaponProperties2.upgradeLv = this.allThrown[j].mModelLv;
			this.plusedWeaponIndex.Add(ggnetworkWeaponProperties2);
		}
		for (int k = 0; k < this.allZombiehand.Count; k++)
		{
			GameObject gameObject5 = Resources.Load("Prefabs/Weapons_Local/" + this.allZombiehand[k].mName) as GameObject;
			GameObject gameObject6 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Weapons_Local/" + this.allZombiehand[k].mName), Vector3.zero, Quaternion.identity) as GameObject;
			gameObject6.transform.parent = this.zombiehandManager.transform;
			GGWeaponScript component3 = gameObject6.GetComponent<GGWeaponScript>();
			component3.SetWeaponPosition(gameObject5.transform.localPosition, gameObject5.transform.localEulerAngles, gameObject5.transform.localScale);
			this.Zombiehands.Add(gameObject6);
			GGNetworkWeaponProperties ggnetworkWeaponProperties3 = new GGNetworkWeaponProperties();
			ggnetworkWeaponProperties3.weaponType = this.allZombiehand[k].mWeaponId;
			ggnetworkWeaponProperties3.upgradeLv = 0;
			this.plusedWeaponIndex.Add(ggnetworkWeaponProperties3);
		}
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x0007B9F4 File Offset: 0x00079DF4
	private void TakeFirstWeapon()
	{
		for (int i = 0; i < this.equipWeapons.Count; i++)
		{
			this.equipWeapons[i].SetActive(false);
		}
		this.equipWeapons[this.SelectedWeaponIndex].SetActive(true);
		this.SelectedWeapon = this.equipWeapons[this.SelectedWeaponIndex].GetComponent<GGWeaponScript>();
		this.SelectedWeapon.InitAnimation();
		this.SelectedWeapon.selectWeapon();
		this.ChangeWeaponNameToIndex(this.SelectedWeapon.weaponName);
		this.canSwitch = true;
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x0007BA90 File Offset: 0x00079E90
	public void SwitchWeapons(int curIndex, int nextIndex)
	{
		base.GetComponent<AudioSource>().clip = this.takeInAudio;
		base.GetComponent<AudioSource>().Play();
		this.canSwitch = true;
		this.SelectedWeapon.deselectWeapon();
		this.equipWeapons[curIndex].SetActive(false);
		this.equipWeapons[nextIndex].SetActive(true);
		this.SelectedWeapon = this.equipWeapons[this.SelectedWeaponIndex].GetComponent<GGWeaponScript>();
		this.SelectedWeapon.InitAnimation();
		this.SelectedWeapon.selectWeapon();
		this.ChangeWeaponNameToIndex(this.SelectedWeapon.weaponName);
		if (UIUserDataController.GetSniperMode() == 0)
		{
			if (this.SelectedWeapon.GunType == gunType.SniperRifle)
			{
				if (this.AimButton != null && !this.AimButton.activeSelf)
				{
					this.AimButton.SetActive(true);
				}
			}
			else if (this.AimButton != null && this.AimButton.activeSelf)
			{
				this.AimButton.SetActive(false);
			}
		}
		if (this.SelectedWeapon.weaponName == "TeslaP1" || this.SelectedWeapon.weaponName == "Flamethrower")
		{
			if (UIPlayDirector.mInstance != null)
			{
				UIPlayDirector.mInstance.ShowEnergyBarNode();
			}
		}
		else if (UIPlayDirector.mInstance != null)
		{
			UIPlayDirector.mInstance.HideEnergyBarNode();
		}
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x0007BC1C File Offset: 0x0007A01C
	private void ChangeToBomb(int bombIndex)
	{
		this.isTakedGrenade = true;
		this.canSwitch = false;
		base.GetComponent<AudioSource>().clip = this.takeInAudio;
		base.GetComponent<AudioSource>().Play();
		this.SelectedWeapon.deselectWeapon();
		this.equipWeaponManager.SetActive(false);
		this.thrownManager.SetActive(true);
		this.throwns[bombIndex].SetActive(true);
		this.SelectedWeapon = this.throwns[bombIndex].GetComponent<GGWeaponScript>();
		this.SelectedWeapon.InitAnimation();
		this.SelectedWeapon.selectWeapon();
		this.ChangeWeaponNameToIndex(this.SelectedWeapon.weaponName);
		this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x0007BCD4 File Offset: 0x0007A0D4
	public void ChangeToPreweapon()
	{
		base.GetComponent<AudioSource>().clip = this.takeInAudio;
		base.GetComponent<AudioSource>().Play();
		this.SelectedWeapon.deselectWeapon();
		this.thrownManager.SetActive(false);
		this.equipWeaponManager.SetActive(true);
		this.SelectedWeapon = this.equipWeapons[this.SelectedWeaponIndex].GetComponent<GGWeaponScript>();
		this.SelectedWeapon.InitAnimation();
		this.SelectedWeapon.selectWeapon();
		this.ChangeWeaponNameToIndex(this.SelectedWeapon.weaponName);
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
		}
		this.canSwitch = true;
		this.isTakedGrenade = false;
	}

	// Token: 0x06000EC2 RID: 3778 RVA: 0x0007BD90 File Offset: 0x0007A190
	public void SwitchWeaponToZombieHand()
	{
		base.GetComponent<AudioSource>().clip = this.takeInAudio;
		base.GetComponent<AudioSource>().Play();
		this.canSwitch = false;
		this.SelectedWeapon.deselectWeapon();
		this.equipWeaponManager.SetActive(false);
		this.zombiehandManager.SetActive(true);
		this.Zombiehands[0].SetActive(true);
		this.SelectedWeapon = this.Zombiehands[0].GetComponent<GGWeaponScript>();
		this.SelectedWeapon.InitAnimation();
		this.SelectedWeapon.selectWeapon();
		this.ChangeWeaponNameToIndex(this.SelectedWeapon.weaponName);
	}

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0007BE34 File Offset: 0x0007A234
	public void SetZombiehandMaterial(int index)
	{
		MeshRenderer[] componentsInChildren = this.Zombiehands[0].GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].material = (Resources.Load("Original Resources/Character/Materials/zombieHand" + index.ToString()) as Material);
		}
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x0007BE90 File Offset: 0x0007A290
	private void ChangeWeaponNameToIndex(string weaponName)
	{
		GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(weaponName);
		this.mNetworkCharacter.mWeaponType = weaponItemInfoByName.mWeaponId;
		if (weaponItemInfoByName.mGunType == "MachineGun")
		{
			this.ChangeMoveSpeed(weaponItemInfoByName.mMoveSpeed * (1f + weaponItemInfoByName.GetPropertyAdditionValue("Move")));
		}
		else
		{
			this.ChangeMoveSpeed(weaponItemInfoByName.mMoveSpeed);
		}
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x0007BEFC File Offset: 0x0007A2FC
	private void OnThrowWeaponFire(int bombIndex)
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (this.mNetworkPlayerLogic.showInstallSchedule)
			{
				this.mNetworkPlayerLogic.OnInstallBtnReleased();
				this.mNetworkPlayerLogic.HideInstallBombButton();
			}
			else if (this.mNetworkPlayerLogic.showRemoveSchedule)
			{
				this.mNetworkPlayerLogic.OnUninstallBtnReleased();
				this.mNetworkPlayerLogic.HideUninstallBombButton();
			}
			if (!this.SelectedWeapon.isReload && !this.SelectedWeapon.aimed && this.mNetworkCharacter.mCharacterFireState == GGCharacterFireState.Idle)
			{
				if (UIPlayDirector.mInstance != null)
				{
					UIPlayDirector.mInstance.ThrownWeaponSubCoins(bombIndex - 1);
					UIPlayDirector.mInstance.ThrowWeaponStart();
				}
				this.ChangeToBomb(bombIndex - 1);
			}
		}
	}

	// Token: 0x06000EC6 RID: 3782 RVA: 0x0007BFCC File Offset: 0x0007A3CC
	private void OnFireStart()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (this.mNetworkPlayerLogic.showInstallSchedule)
			{
				this.mNetworkPlayerLogic.OnInstallBtnReleased();
				this.mNetworkPlayerLogic.HideInstallBombButton();
			}
			else if (this.mNetworkPlayerLogic.showRemoveSchedule)
			{
				this.mNetworkPlayerLogic.OnUninstallBtnReleased();
				this.mNetworkPlayerLogic.HideUninstallBombButton();
			}
			if (this.SelectedWeapon.GunType == gunType.Pistol || this.SelectedWeapon.GunType == gunType.Rifle || this.SelectedWeapon.GunType == gunType.SubMachineGun || this.SelectedWeapon.GunType == gunType.MachineGun || this.SelectedWeapon.GunType == gunType.SniperRifle || this.SelectedWeapon.GunType == gunType.PlasmarGun)
			{
				if (this.SelectedWeapon.weaponName != "TeslaP1")
				{
					if (this.SelectedWeapon._MachineGunbulletsLeft != 0)
					{
						if (this.SelectedWeapon.GunType == gunType.SniperRifle)
						{
							if (UIUserDataController.GetSniperMode() == 1)
							{
								if (this.snipeCanAim)
								{
									this.SelectedWeapon.isAiming = true;
								}
							}
							else
							{
								this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
							}
						}
						else
						{
							this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
						}
					}
					else if (UIPlayDirector.mInstance != null)
					{
						UIPlayDirector.mInstance.NoBulletTip();
					}
				}
				else if (this.SelectedWeapon._MachineGunbulletsLeft <= (int)((float)this.SelectedWeapon._MachineGunclips * 0.8f))
				{
					this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
				}
			}
			else if (this.SelectedWeapon.GunType == gunType.ShotGun)
			{
				if (this.SelectedWeapon._ShotGunbulletsLeft != 0)
				{
					this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
				}
				else if (UIPlayDirector.mInstance != null)
				{
					UIPlayDirector.mInstance.NoBulletTip();
				}
			}
			else if (this.SelectedWeapon.GunType == gunType.Bazooka || this.SelectedWeapon.GunType == gunType.Thrown)
			{
				if (this.SelectedWeapon._GrenadeLauncherammoCount != 0)
				{
					this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
				}
				else if (UIPlayDirector.mInstance != null)
				{
					UIPlayDirector.mInstance.NoBulletTip();
				}
			}
			else if (this.SelectedWeapon.GunType == gunType.Knife)
			{
				this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
			}
		}
	}

	// Token: 0x06000EC7 RID: 3783 RVA: 0x0007C248 File Offset: 0x0007A648
	private void OnFireEnd()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (this.SelectedWeapon.GunType == gunType.Pistol || this.SelectedWeapon.GunType == gunType.Rifle || this.SelectedWeapon.GunType == gunType.SubMachineGun || this.SelectedWeapon.GunType == gunType.MachineGun || this.SelectedWeapon.GunType == gunType.SniperRifle || this.SelectedWeapon.GunType == gunType.PlasmarGun)
			{
				if (this.SelectedWeapon.GunType == gunType.SniperRifle)
				{
					if (UIUserDataController.GetSniperMode() == 1)
					{
						if (this.SelectedWeapon.aimed)
						{
							this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
							base.StartCoroutine(this.StopFire(Time.deltaTime));
							this.snipeCanAim = false;
							base.StartCoroutine(this.SnipeRefreshAim(this.SelectedWeapon.MachineGunfireRate));
						}
					}
					else if (!this.isTakedGrenade)
					{
						this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
					}
				}
				else if (!this.isTakedGrenade)
				{
					this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
				}
			}
			else if (this.SelectedWeapon.GunType == gunType.ShotGun)
			{
				base.StartCoroutine(this.StopFire(0.25f));
			}
			else if (this.SelectedWeapon.GunType == gunType.Bazooka || this.SelectedWeapon.GunType == gunType.Thrown)
			{
				base.StartCoroutine(this.StopFire(0.25f));
			}
			else if (this.SelectedWeapon.GunType == gunType.Knife && !this.isTakedGrenade)
			{
				this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
			}
		}
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x0007C3FC File Offset: 0x0007A7FC
	private IEnumerator StopFire(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		if (!this.isTakedGrenade)
		{
			this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
		}
		yield break;
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x0007C420 File Offset: 0x0007A820
	private IEnumerator SnipeRefreshAim(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		this.snipeCanAim = true;
		yield break;
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0007C444 File Offset: 0x0007A844
	private void OnReload()
	{
		if (this.SelectedWeapon.weaponName == "TeslaP1" || this.SelectedWeapon.weaponName == "Flamethrower")
		{
			return;
		}
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead && this.mNetworkCharacter.mCharacterFireState != GGCharacterFireState.Fire)
		{
			if (this.mNetworkPlayerLogic.showInstallSchedule)
			{
				this.mNetworkPlayerLogic.OnInstallBtnReleased();
				this.mNetworkPlayerLogic.HideInstallBombButton();
			}
			else if (this.mNetworkPlayerLogic.showRemoveSchedule)
			{
				this.mNetworkPlayerLogic.OnUninstallBtnReleased();
				this.mNetworkPlayerLogic.HideUninstallBombButton();
			}
			this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Reload;
		}
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0007C508 File Offset: 0x0007A908
	private void OnSwitchLeft()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (this.mNetworkPlayerLogic.showInstallSchedule || this.mNetworkPlayerLogic.showRemoveSchedule)
			{
				return;
			}
			if (!this.SelectedWeapon.isReload && !this.SelectedWeapon.aimed && this.canSwitch)
			{
				this.SwitchWeaponLeft();
			}
		}
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0007C578 File Offset: 0x0007A978
	private void OnSwitchRight()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (this.mNetworkPlayerLogic.showInstallSchedule || this.mNetworkPlayerLogic.showRemoveSchedule)
			{
				return;
			}
			if (!this.SelectedWeapon.isReload && !this.SelectedWeapon.aimed && this.canSwitch)
			{
				this.SwitchWeaponRight();
			}
		}
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x0007C5E8 File Offset: 0x0007A9E8
	private void OnAim()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (this.mNetworkPlayerLogic.showInstallSchedule)
			{
				this.mNetworkPlayerLogic.OnInstallBtnReleased();
				this.mNetworkPlayerLogic.HideInstallBombButton();
			}
			else if (this.mNetworkPlayerLogic.showRemoveSchedule)
			{
				this.mNetworkPlayerLogic.OnUninstallBtnReleased();
				this.mNetworkPlayerLogic.HideUninstallBombButton();
			}
			this.SelectedWeapon.isAiming = true;
		}
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x0007C663 File Offset: 0x0007AA63
	private void OnSniperCancel()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.SelectedWeapon.isAiming = true;
			this.snipeCanAim = true;
		}
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x0007C68C File Offset: 0x0007AA8C
	private void OnAddBullet()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (!this.isUIHelp && (this.mNetworkPlayerLogic.showInstallSchedule || this.mNetworkPlayerLogic.showRemoveSchedule))
			{
				return;
			}
			this.SelectedWeapon.buyBullet();
		}
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x0007C6E1 File Offset: 0x0007AAE1
	public void PickUpBullet()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.SelectedWeapon.PickUpBullet();
		}
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x0007C700 File Offset: 0x0007AB00
	private void OnHelpThrowWeaponFire(int bombIndex)
	{
		if (!this.SelectedWeapon.isReload && !this.SelectedWeapon.aimed)
		{
			if (UIPlayDirector.mInstance != null)
			{
				UIPlayDirector.mInstance.ThrowWeaponStart();
			}
			this.ChangeToBomb(0);
		}
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x0007C750 File Offset: 0x0007AB50
	private void OnHelpFireStart()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (this.SelectedWeapon.GunType == gunType.Pistol || this.SelectedWeapon.GunType == gunType.Rifle || this.SelectedWeapon.GunType == gunType.SubMachineGun || this.SelectedWeapon.GunType == gunType.MachineGun || this.SelectedWeapon.GunType == gunType.SniperRifle || this.SelectedWeapon.GunType == gunType.PlasmarGun)
			{
				if (this.SelectedWeapon._MachineGunbulletsLeft != 0)
				{
					if (this.SelectedWeapon.GunType == gunType.SniperRifle)
					{
						if (UIUserDataController.GetSniperMode() == 1)
						{
							if (this.snipeCanAim)
							{
								this.SelectedWeapon.isAiming = true;
							}
						}
						else
						{
							this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
						}
					}
					else
					{
						this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
					}
				}
				else if (UIPlayDirector.mInstance != null)
				{
					UIPlayDirector.mInstance.NoBulletTip();
				}
			}
			else if (this.SelectedWeapon.GunType == gunType.ShotGun)
			{
				if (this.SelectedWeapon._ShotGunbulletsLeft != 0)
				{
					this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
				}
				else if (UIPlayDirector.mInstance != null)
				{
					UIPlayDirector.mInstance.NoBulletTip();
				}
			}
			else if (this.SelectedWeapon.GunType == gunType.Bazooka || this.SelectedWeapon.GunType == gunType.Thrown)
			{
				if (this.SelectedWeapon._GrenadeLauncherammoCount != 0)
				{
					this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
				}
				else if (UIPlayDirector.mInstance != null)
				{
					UIPlayDirector.mInstance.NoBulletTip();
				}
			}
			else if (this.SelectedWeapon.GunType == gunType.Knife)
			{
				this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
			}
		}
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x0007C930 File Offset: 0x0007AD30
	private void OnHelpFireEnd()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (this.SelectedWeapon.GunType == gunType.Pistol || this.SelectedWeapon.GunType == gunType.Rifle || this.SelectedWeapon.GunType == gunType.SubMachineGun || this.SelectedWeapon.GunType == gunType.MachineGun || this.SelectedWeapon.GunType == gunType.SniperRifle || this.SelectedWeapon.GunType == gunType.PlasmarGun)
			{
				if (this.SelectedWeapon.GunType == gunType.SniperRifle)
				{
					if (UIUserDataController.GetSniperMode() == 1)
					{
						if (this.SelectedWeapon.aimed)
						{
							this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Fire;
							base.StartCoroutine(this.StopFire(Time.deltaTime));
							this.snipeCanAim = false;
							base.StartCoroutine(this.SnipeRefreshAim(this.SelectedWeapon.MachineGunfireRate));
						}
					}
					else
					{
						this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
					}
				}
				else
				{
					this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
				}
			}
			else if (this.SelectedWeapon.GunType == gunType.ShotGun)
			{
				base.StartCoroutine(this.StopFire(0.25f));
			}
			else if (this.SelectedWeapon.GunType == gunType.Bazooka || this.SelectedWeapon.GunType == gunType.Thrown)
			{
				base.StartCoroutine(this.StopFire(0.25f));
			}
			else if (this.SelectedWeapon.GunType == gunType.Knife)
			{
				this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
			}
		}
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x0007CAC2 File Offset: 0x0007AEC2
	private void OnHelpReload()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead && this.mNetworkCharacter.mCharacterFireState != GGCharacterFireState.Fire)
		{
			this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Reload;
		}
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0007CAF4 File Offset: 0x0007AEF4
	private void OnHelpSwitchLeft()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead && !this.SelectedWeapon.isReload && !this.SelectedWeapon.aimed && this.canSwitch)
		{
			this.SwitchWeaponLeft();
		}
	}

	// Token: 0x06000ED6 RID: 3798 RVA: 0x0007CB44 File Offset: 0x0007AF44
	private void OnHelpSwitchRight()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead && !this.SelectedWeapon.isReload && !this.SelectedWeapon.aimed && this.canSwitch)
		{
			this.SwitchWeaponRight();
		}
	}

	// Token: 0x06000ED7 RID: 3799 RVA: 0x0007CB93 File Offset: 0x0007AF93
	private void OnHelpAddBullet()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.SelectedWeapon.buyBullet();
		}
	}

	// Token: 0x06000ED8 RID: 3800 RVA: 0x0007CBB4 File Offset: 0x0007AFB4
	private void OnHelpMediKit()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.mNetworkCharacter.mBlood = (int)(100f * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.HpPlus].additionValue));
		}
	}

	// Token: 0x06000ED9 RID: 3801 RVA: 0x0007CC00 File Offset: 0x0007B000
	private void OnHelpArmor()
	{
		this.mNetworkCharacter.myArmorInfo.mDurabilityInGame = 100;
	}

	// Token: 0x06000EDA RID: 3802 RVA: 0x0007CC14 File Offset: 0x0007B014
	private void OnDisable()
	{
		UIPlayDirector.OnReload -= this.OnReload;
		UIPlayDirector.OnFireStart -= this.OnFireStart;
		UIPlayDirector.OnFireEnd -= this.OnFireEnd;
		UIPlayDirector.OnSwitchLeft -= this.OnSwitchLeft;
		UIPlayDirector.OnSwitchRight -= this.OnSwitchRight;
		UIPlayDirector.OnAim -= this.OnAim;
		UIPlayDirector.OnSniperCancel -= this.OnSniperCancel;
		UIPlayDirector.OnAddBullet -= this.OnAddBullet;
		UIPlayDirector.OnThrowWeaponFire -= this.OnThrowWeaponFire;
		UIHelpDirector.OnHelpReload -= this.OnHelpReload;
		UIHelpDirector.OnHelpFireStart -= this.OnHelpFireStart;
		UIHelpDirector.OnHelpFireEnd -= this.OnHelpFireEnd;
		UIHelpDirector.OnHelpSwitchLeft -= this.OnHelpSwitchLeft;
		UIHelpDirector.OnHelpSwitchRight -= this.OnHelpSwitchRight;
		UIHelpDirector.OnHelpAddBullet -= this.OnHelpAddBullet;
		UIHelpDirector.OnHelpThrowWeaponFire -= this.OnHelpThrowWeaponFire;
		UIHelpDirector.OnHelpArmor -= this.OnHelpArmor;
		UIHelpDirector.OnHelpMediKit -= this.OnHelpMediKit;
	}

	// Token: 0x06000EDB RID: 3803 RVA: 0x0007CD53 File Offset: 0x0007B153
	private void OnDestroy()
	{
		if (GGWeaponManager.mInstance != null)
		{
			GGWeaponManager.mInstance = null;
		}
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x0007CD6C File Offset: 0x0007B16C
	public string GetAmmoStr()
	{
		if (this.SelectedWeapon.GunType == gunType.Pistol || this.SelectedWeapon.GunType == gunType.Rifle || this.SelectedWeapon.GunType == gunType.SubMachineGun || this.SelectedWeapon.GunType == gunType.MachineGun || this.SelectedWeapon.GunType == gunType.SniperRifle || this.SelectedWeapon.GunType == gunType.PlasmarGun)
		{
			this.bulletsLeft = this.SelectedWeapon._MachineGunbulletsLeft;
			this.clips = this.SelectedWeapon._MachineGunclips;
		}
		if (this.SelectedWeapon.GunType == gunType.ShotGun)
		{
			this.bulletsLeft = this.SelectedWeapon._ShotGunbulletsLeft;
			this.clips = this.SelectedWeapon._ShotGunclips;
		}
		if (this.SelectedWeapon.GunType == gunType.Bazooka || this.SelectedWeapon.GunType == gunType.Thrown)
		{
			this.clips = this.SelectedWeapon._GrenadeLauncherammoCount;
		}
		if (this.SelectedWeapon)
		{
			if (this.SelectedWeapon.GunType != gunType.Knife)
			{
				if (this.SelectedWeapon.GunType == gunType.Bazooka || this.SelectedWeapon.GunType == gunType.Thrown)
				{
					this.rtn = this.clips.ToString();
				}
				else if (this.SelectedWeapon.GunType == gunType.Pistol)
				{
					this.rtn = this.bulletsLeft.ToString() + " | NA";
				}
				else if (this.SelectedWeapon.weaponName == "TeslaP1" || this.SelectedWeapon.weaponName == "Flamethrower")
				{
					this.rtn = ((float)this.bulletsLeft / (float)this.clips).ToString();
				}
				else
				{
					this.rtn = this.bulletsLeft.ToString() + " | " + this.clips.ToString();
				}
			}
			else
			{
				this.rtn = "NA";
			}
		}
		return this.rtn;
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x0007CFA4 File Offset: 0x0007B3A4
	public void ChangeMoveSpeed(float speed)
	{
		this.mMotorCS.movement.maxBackwardsSpeed = Mathf.Min(speed * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.SpeedPlus].additionValue), 7.5f);
		this.mMotorCS.movement.maxForwardSpeed = Mathf.Min(speed * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.SpeedPlus].additionValue), 7.5f);
		this.mMotorCS.movement.maxSidewaysSpeed = Mathf.Min(speed * (1f + GrowthManagerKit.EProperty().allDic[EnchantmentType.SpeedPlus].additionValue), 7.5f);
		this.mMotorCS.footStepFreq = 0.45f * (5f / this.mMotorCS.movement.maxBackwardsSpeed);
	}

	// Token: 0x06000EDE RID: 3806 RVA: 0x0007D080 File Offset: 0x0007B480
	public List<GGNetworkWeaponProperties> GetWeaponProperties()
	{
		return this.plusedWeaponIndex;
	}

	// Token: 0x06000EDF RID: 3807 RVA: 0x0007D088 File Offset: 0x0007B488
	public void SetLaserR7_LaserScope(bool e)
	{
		if (this.SelectedWeapon.weaponName == "LaserR7")
		{
			Laserscope componentInChildren = base.transform.GetComponentInChildren<Laserscope>();
			componentInChildren.gameObject.GetComponent<LineRenderer>().enabled = e;
		}
	}

	// Token: 0x04001015 RID: 4117
	public static GGWeaponManager mInstance;

	// Token: 0x04001016 RID: 4118
	public List<GWeaponItemInfo> allWeapons = new List<GWeaponItemInfo>();

	// Token: 0x04001017 RID: 4119
	public List<GameObject> equipWeapons = new List<GameObject>();

	// Token: 0x04001018 RID: 4120
	public List<GWeaponItemInfo> allThrown = new List<GWeaponItemInfo>();

	// Token: 0x04001019 RID: 4121
	public List<GameObject> throwns = new List<GameObject>();

	// Token: 0x0400101A RID: 4122
	public List<GWeaponItemInfo> allZombiehand = new List<GWeaponItemInfo>();

	// Token: 0x0400101B RID: 4123
	public List<GameObject> Zombiehands = new List<GameObject>();

	// Token: 0x0400101C RID: 4124
	public GameObject equipWeaponManager;

	// Token: 0x0400101D RID: 4125
	public GameObject thrownManager;

	// Token: 0x0400101E RID: 4126
	public GameObject zombiehandManager;

	// Token: 0x0400101F RID: 4127
	public List<GGNetworkWeaponProperties> plusedWeaponIndex = new List<GGNetworkWeaponProperties>();

	// Token: 0x04001020 RID: 4128
	private int[] GrenadeList = new int[]
	{
		7,
		13,
		16,
		23,
		24,
		40
	};

	// Token: 0x04001021 RID: 4129
	private float SwitchTime = 0.5f;

	// Token: 0x04001022 RID: 4130
	public bool bPlayer;

	// Token: 0x04001023 RID: 4131
	public GGWeaponScript SelectedWeapon;

	// Token: 0x04001024 RID: 4132
	public int SelectedWeaponIndex;

	// Token: 0x04001025 RID: 4133
	public int index;

	// Token: 0x04001026 RID: 4134
	public AudioClip takeInAudio;

	// Token: 0x04001027 RID: 4135
	private GameObject defaultPrimaryWeap;

	// Token: 0x04001028 RID: 4136
	private GameObject defaultSecondaryWeap;

	// Token: 0x04001029 RID: 4137
	private bool canSwitch;

	// Token: 0x0400102A RID: 4138
	private bool isTakedGrenade;

	// Token: 0x0400102B RID: 4139
	public string onlinePlayerTag = "null";

	// Token: 0x0400102C RID: 4140
	private GameObject UIMenuDirectorExt;

	// Token: 0x0400102D RID: 4141
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x0400102E RID: 4142
	private CharacterMotorCS mMotorCS;

	// Token: 0x0400102F RID: 4143
	private GGNetWorkPlayerlogic mNetworkPlayerLogic;

	// Token: 0x04001030 RID: 4144
	private int bulletsLeft;

	// Token: 0x04001031 RID: 4145
	private int clips;

	// Token: 0x04001032 RID: 4146
	private string rtn = string.Empty;

	// Token: 0x04001033 RID: 4147
	private GGCharacterWalkState preState;

	// Token: 0x04001034 RID: 4148
	private GameObject CrosshairSprite;

	// Token: 0x04001035 RID: 4149
	private GameObject AimButton;

	// Token: 0x04001036 RID: 4150
	private float nextAimTime;

	// Token: 0x04001037 RID: 4151
	private bool snipeCanAim = true;

	// Token: 0x04001038 RID: 4152
	private GameObject SniperCancelButton;

	// Token: 0x04001039 RID: 4153
	private GameObject scopeSprite;

	// Token: 0x0400103A RID: 4154
	private int preSniperMode;

	// Token: 0x0400103B RID: 4155
	private int preWeaponIndex;

	// Token: 0x0400103C RID: 4156
	private int TempbombIndex;

	// Token: 0x0400103D RID: 4157
	private float speedBuffValue_Old;

	// Token: 0x0400103E RID: 4158
	private float speedBuffValue_New;

	// Token: 0x0400103F RID: 4159
	private int bombNum = 6;

	// Token: 0x04001040 RID: 4160
	private bool isUIHelp;

	// Token: 0x04001041 RID: 4161
	private bool crosshairDynamic = true;

	// Token: 0x04001042 RID: 4162
	private Vector3 curCrosshairScale = new Vector3(1f, 1f, 0f);

	// Token: 0x04001043 RID: 4163
	private float crosshairSmooth = 3f;
}
