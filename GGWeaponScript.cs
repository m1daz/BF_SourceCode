using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000229 RID: 553
public class GGWeaponScript : MonoBehaviour
{
	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0007D599 File Offset: 0x0007B999
	// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x0007D5A1 File Offset: 0x0007B9A1
	public int _ShotGunbulletsLeft
	{
		get
		{
			return this.ShotGunbulletsLeft;
		}
		set
		{
			GameProtecter.mInstance.SetEncryptVariable(ref this.ShotGunbulletsLeft, ref this.StrEncryptShotGunbulletsLeft, value);
		}
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0007D5BA File Offset: 0x0007B9BA
	// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x0007D5C2 File Offset: 0x0007B9C2
	public int _ShotGunclips
	{
		get
		{
			return this.ShotGunclips;
		}
		set
		{
			GameProtecter.mInstance.SetEncryptVariable(ref this.ShotGunclips, ref this.StrEncryptShotGunclips, value);
		}
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0007D5DB File Offset: 0x0007B9DB
	// (set) Token: 0x06000EEB RID: 3819 RVA: 0x0007D5E3 File Offset: 0x0007B9E3
	public int _GrenadeLauncherammoCount
	{
		get
		{
			return this.GrenadeLauncherammoCount;
		}
		set
		{
			GameProtecter.mInstance.SetEncryptVariable(ref this.GrenadeLauncherammoCount, ref this.StrEncryptGrenadeLauncherammoCount, value);
		}
	}

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0007D5FC File Offset: 0x0007B9FC
	// (set) Token: 0x06000EED RID: 3821 RVA: 0x0007D604 File Offset: 0x0007BA04
	public int _MachineGunclips
	{
		get
		{
			return this.MachineGunclips;
		}
		set
		{
			GameProtecter.mInstance.SetEncryptVariable(ref this.MachineGunclips, ref this.StrEncryptMachineGunclips, value);
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0007D61D File Offset: 0x0007BA1D
	// (set) Token: 0x06000EEF RID: 3823 RVA: 0x0007D625 File Offset: 0x0007BA25
	public int _MachineGunbulletsLeft
	{
		get
		{
			return this.MachineGunbulletsLeft;
		}
		set
		{
			GameProtecter.mInstance.SetEncryptVariable(ref this.MachineGunbulletsLeft, ref this.StrEncryptMachineGunbulletsLeft, value);
		}
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x0007D63E File Offset: 0x0007BA3E
	private void Awake()
	{
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x0007D640 File Offset: 0x0007BA40
	private void Start()
	{
		this.mNetworkCharacter = base.transform.root.GetComponent<GGNetworkCharacter>();
		this.mainCam = base.transform.root.Find("LookObject/Main Camera").GetComponent<Camera>();
		this.mCameraRecoil = base.transform.root.Find("LookObject/Main Camera").GetComponent<GGCameraRecoil>();
		this.mAudioSource = base.GetComponent<AudioSource>();
		this.managerObject = base.transform.parent.parent.gameObject;
		this.reloadFlag = true;
		if (base.transform.root.tag == "Player")
		{
			this.walkSway = this.managerObject.GetComponent<GGWalkSway>();
			this.Sliderotate = base.transform.root.GetComponent<GGSliderotate>();
			this.defaultBobbingAmount = this.walkSway.bobbingAmount;
			PlayerPrefs.SetInt("OnAim", 0);
		}
		this.weaponManager = this.managerObject.GetComponent<GGWeaponManager>();
		this.camDefaultRotation = this.mainCam.transform.localRotation;
		this.defaultFov = this.mainCam.fieldOfView;
		this.defaultPosition = base.transform.localPosition;
		if (this.weaponName.Equals("M249"))
		{
			this.weaponPart = base.transform.Find("Hands+MP5KA4/fps_hand_MP5KA4/handright/M249/M249_2").gameObject;
		}
		if (this.weaponName == "M67")
		{
			this.weaponPart = base.transform.Find("Hands+M67/fps_hand_M67/handright/M67").gameObject;
		}
		else if (this.weaponName == "RPG")
		{
			this.weaponPart = base.transform.Find("Hands+RPG/fps_hand_RPG/handright/RPG/RPG_Missle").gameObject;
		}
		else if (this.weaponName == "MilkBomb")
		{
			this.weaponPart = base.transform.Find("Hands+M67/fps_hand_M67/handright/MilkBomb").gameObject;
		}
		else if (this.weaponName == "GingerbreadBomb")
		{
			this.weaponPart = base.transform.Find("Hands+M67/fps_hand_M67/handright/GingerbreadBomb").gameObject;
		}
		else if (this.weaponName.Equals("M134"))
		{
			this.weaponPart = base.transform.Find("Hands+M134/fps_hand_M134/handright/M134/M134_2").gameObject;
		}
		else if (this.weaponName.Equals("SmokeBomb"))
		{
			this.weaponPart = base.transform.Find("Hands+M67/fps_hand_M67/handright/SmokeBomb").gameObject;
		}
		else if (this.weaponName.Equals("FlashBomb"))
		{
			this.weaponPart = base.transform.Find("Hands+M67/fps_hand_M67/handright/FlashBomb").gameObject;
		}
		else if (this.weaponName.Equals("SM134"))
		{
			this.weaponPart = base.transform.Find("Hands+M134/fps_hand_M134/handright/M134/M134_2").gameObject;
		}
		else if (this.weaponName == "SnowmanBomb")
		{
			this.weaponPart = base.transform.Find("Hands+M67/fps_hand_M67/handright/SnowmanBomb").gameObject;
		}
		else if (this.weaponName.Equals("HonorM134"))
		{
			this.weaponPart = base.transform.Find("Hands+M134/fps_hand_M134/handright/M134/M134_2").gameObject;
		}
		if (this.GunType == gunType.Pistol || this.GunType == gunType.Rifle || this.GunType == gunType.SubMachineGun || this.GunType == gunType.MachineGun || this.GunType == gunType.SniperRifle || this.GunType == gunType.PlasmarGun)
		{
			this.MachineGunAwake();
		}
		if (this.GunType == gunType.Bazooka || this.GunType == gunType.Thrown)
		{
			this.grenadeLauncherAwake();
		}
		if (this.GunType == gunType.ShotGun)
		{
			this.shotGunAwake();
		}
		if (this.GunType == gunType.Knife)
		{
			this.knifeAwake();
		}
		string text = this.weaponName;
		switch (text)
		{
		case "DesertEagle":
			this.FireAnimationTime = 0.2f;
			break;
		case "M4":
			this.FireAnimationTime = 0.1f;
			break;
		case "AK47":
			this.FireAnimationTime = 0.1f;
			break;
		case "AWP":
			this.FireAnimationTime = 0.1f;
			break;
		case "GLOCK21":
			this.FireAnimationTime = 0.2f;
			break;
		case "G36K":
			this.FireAnimationTime = 0.1f;
			break;
		case "MP5KA5":
			this.FireAnimationTime = 0.082f;
			break;
		case "UZI":
			this.FireAnimationTime = 0.079f;
			break;
		case "M249":
			this.FireAnimationTime = 0.1f;
			break;
		case "M87T":
			this.FireAnimationTime = 0.1f;
			break;
		case "ChristmasSniper":
			this.FireAnimationTime = 0.1f;
			break;
		case "CandyRifle":
			this.FireAnimationTime = 0.1f;
			break;
		case "SantaGun":
			this.FireAnimationTime = 0.079f;
			break;
		case "BallisticKnife":
			this.FireAnimationTime = 0.3f;
			break;
		case "GingerbreadKnife":
			this.FireAnimationTime = 0.24f;
			break;
		case "AUG":
			this.FireAnimationTime = 0.1f;
			break;
		case "M3":
			this.FireAnimationTime = 0.4f;
			break;
		case "M134":
			this.FireAnimationTime = 0.1f;
			break;
		case "StenMarkV":
			this.FireAnimationTime = 0.1f;
			break;
		case "LaserR7":
			this.FireAnimationTime = 0.1f;
			break;
		case "Shovel":
			this.FireAnimationTime = 0.3f;
			break;
		case "ZombieHand":
			this.FireAnimationTime = 0.8f;
			break;
		case "Firelock":
			this.FireAnimationTime = 0.2f;
			break;
		case "HalloweenGun":
			this.FireAnimationTime = 0.1f;
			break;
		case "SM134":
			this.FireAnimationTime = 0.1f;
			break;
		case "SM4":
			this.FireAnimationTime = 0.1f;
			break;
		case "SG36K":
			this.FireAnimationTime = 0.1f;
			break;
		case "SAUG":
			this.FireAnimationTime = 0.1f;
			break;
		case "SAK47":
			this.FireAnimationTime = 0.1f;
			break;
		case "SDesertEagle":
			this.FireAnimationTime = 0.2f;
			break;
		case "SantaGun2014":
			this.FireAnimationTime = 0.1f;
			break;
		case "ThunderX6":
			this.FireAnimationTime = 0.1f;
			break;
		case "MK5":
			this.FireAnimationTime = 0.079f;
			break;
		case "BurstRG2":
			this.FireAnimationTime = 0.1f;
			break;
		case "CandyHammer":
			this.FireAnimationTime = 0.35f;
			break;
		case "HonorKnife":
			this.FireAnimationTime = 0.3f;
			break;
		case "HonorAK47":
			this.FireAnimationTime = 0.1f;
			break;
		case "HonorM4":
			this.FireAnimationTime = 0.1f;
			break;
		case "TeslaP1":
			this.FireAnimationTime = 0.1f;
			break;
		case "M1":
			this.FireAnimationTime = 0.1f;
			break;
		case "M29":
			this.FireAnimationTime = 0.1f;
			break;
		case "DualPistol":
			this.FireAnimationTime = 0.25f;
			break;
		case "FreddyGun":
			this.FireAnimationTime = 0.1f;
			break;
		case "ImpulseGun":
			this.FireAnimationTime = 0.1f;
			break;
		case "Assault":
			this.FireAnimationTime = 0.1f;
			break;
		case "HonorAWP":
			this.FireAnimationTime = 0.1f;
			break;
		case "HonorM134":
			this.FireAnimationTime = 0.1f;
			break;
		case "ShadowSnake":
			this.FireAnimationTime = 0.3f;
			break;
		case "Digger":
			this.FireAnimationTime = 0.1f;
			break;
		case "Nightmare":
			this.FireAnimationTime = 0.1f;
			break;
		case "SweetMemory":
			this.FireAnimationTime = 0.3f;
			break;
		case "Shark":
			this.FireAnimationTime = 0.25f;
			break;
		case "DeathHunter":
			this.FireAnimationTime = 0.1f;
			break;
		case "Flower":
			this.FireAnimationTime = 0.3f;
			break;
		case "Flamethrower":
			this.FireAnimationTime = 10f;
			break;
		}
		if (this.upgradeLv > 0)
		{
			Forcefield[] componentsInChildren = base.transform.GetComponentsInChildren<Forcefield>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (this.weaponName != "Nightmare")
				{
					componentsInChildren[i].gameObject.GetComponent<Renderer>().material = (Resources.Load("Original Resources/Weapons/Materials/WeaponUpgradeLv_" + this.upgradeLv.ToString()) as Material);
					componentsInChildren[i].gameObject.GetComponent<Renderer>().enabled = true;
					componentsInChildren[i].enabled = true;
				}
				else
				{
					componentsInChildren[i].gameObject.GetComponent<Renderer>().material = (Resources.Load("Original Resources/Weapons/Materials/WeaponUpgradeLv_" + this.upgradeLv.ToString() + "_Nightmare") as Material);
					componentsInChildren[i].gameObject.GetComponent<Renderer>().enabled = true;
					componentsInChildren[i].enabled = true;
				}
			}
		}
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x0007E2A0 File Offset: 0x0007C6A0
	private void Update()
	{
		if (Time.timeScale < 0.01f)
		{
			return;
		}
		if (this.isAiming && this.canAim && this.GunType == gunType.SniperRifle && base.transform.root.tag == "Player")
		{
			this.isAiming = false;
			if (!this.quickSniper)
			{
				this.aimed = !this.aimed;
			}
			else if (!this.aimed)
			{
				this.aimed = true;
			}
		}
		if (base.transform.root.tag == "Player")
		{
			this.Aiming();
		}
		if (this.Recoil && base.transform.root.tag == "Player")
		{
			this.cameraRecoilDo();
		}
		if (this.GunType == gunType.Pistol || this.GunType == gunType.Rifle || this.GunType == gunType.SubMachineGun || this.GunType == gunType.MachineGun || this.GunType == gunType.SniperRifle || this.GunType == gunType.PlasmarGun)
		{
			if (this.GunType == gunType.Pistol && this._MachineGunclips == 0)
			{
				this._MachineGunclips += 60;
			}
			if (this._MachineGunbulletsLeft == 0 && this._MachineGunclips > 0 && this.reloadFlag)
			{
				this.reloadFlag = false;
				this.MachineGunReload();
			}
			if (this.GunType == gunType.PlasmarGun && this.weaponName == "TeslaP1")
			{
				if (Time.time - 0.05f > this.TeslaP1CoolDownTime)
				{
					this.TeslaP1CoolDownTime = Time.time - Time.deltaTime;
				}
				while (this.TeslaP1CoolDownTime < Time.time)
				{
					if (this._MachineGunbulletsLeft <= (int)((float)this._MachineGunclips * 0.6f))
					{
						this._MachineGunbulletsLeft -= 5;
					}
					else
					{
						this._MachineGunbulletsLeft -= 3;
					}
					this._MachineGunbulletsLeft = Mathf.Max(0, this._MachineGunbulletsLeft);
					this.TeslaP1CoolDownTime += 0.05f;
				}
			}
			this.MachineGunFixedUpdate();
		}
		if (this.GunType == gunType.Bazooka || this.GunType == gunType.Thrown)
		{
			this.grenadeLauncherFixedUpdate();
		}
		if (this.GunType == gunType.ShotGun)
		{
			if (this._ShotGunbulletsLeft == 0 && this._ShotGunclips > 0 && this.reloadFlag)
			{
				this.reloadFlag = false;
				this.shotGunReload();
			}
			this.shotGunFixedUpdate();
		}
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x0007E54C File Offset: 0x0007C94C
	private void FireInSingleMode()
	{
		this.fire = true;
		if (this.GunType == gunType.Pistol || this.GunType == gunType.Rifle || this.GunType == gunType.SubMachineGun || this.GunType == gunType.MachineGun || this.GunType == gunType.SniperRifle || this.GunType == gunType.PlasmarGun)
		{
			if (this.canFire && !this.isReload)
			{
				this.MachineGunFire();
			}
			else
			{
				this.MachineGunStopFire();
			}
		}
		if (this.GunType == gunType.ShotGun && this.canFire && !this.isReload && this.singleFire)
		{
			this.shotGunFire();
		}
		if ((this.GunType == gunType.Bazooka || this.GunType == gunType.Thrown) && this.canFire && !this.isReload && this.singleFire)
		{
			this.grenadeLauncherFIre();
		}
		if (this.GunType == gunType.Knife && this.canFire && !this.isReload && this.singleFire)
		{
			this.knifeOneShot();
		}
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x0007E674 File Offset: 0x0007CA74
	private void StopFireInSingleMode()
	{
		this.fire = false;
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x0007E680 File Offset: 0x0007CA80
	private void LateUpdate()
	{
		if (Time.timeScale < 0.01f)
		{
			return;
		}
		if (this.mNetworkCharacter.mCharacterFireState == GGCharacterFireState.Fire)
		{
			if (this.GunType == gunType.Pistol || this.GunType == gunType.Rifle || this.GunType == gunType.SubMachineGun || this.GunType == gunType.MachineGun || this.GunType == gunType.SniperRifle || this.GunType == gunType.PlasmarGun)
			{
				if (this.canFire && !this.isReload)
				{
					this.MachineGunFire();
				}
				else
				{
					this.MachineGunStopFire();
				}
			}
			if (this.GunType == gunType.ShotGun && this.canFire && !this.isReload && this.singleFire)
			{
				this.shotGunFire();
			}
			if ((this.GunType == gunType.Bazooka || this.GunType == gunType.Thrown) && this.canFire && !this.isReload && this.singleFire)
			{
				this.grenadeLauncherFIre();
			}
			if (this.GunType == gunType.Knife && this.canFire && !this.isReload)
			{
				this.knifeOneShot();
			}
		}
		else if (this.GunType == gunType.Pistol || this.GunType == gunType.Rifle || this.GunType == gunType.SubMachineGun || this.GunType == gunType.MachineGun || this.GunType == gunType.SniperRifle || this.GunType == gunType.PlasmarGun)
		{
			this.MachineGunStopFire();
			if (this.MachineGunmuzzleFlash)
			{
				this.MachineGunmuzzleFlash.active = false;
			}
			if (this.dualGun)
			{
				if (this.MachineGunmuzzleFlash_R)
				{
					this.MachineGunmuzzleFlash_R.active = false;
				}
				if (this.MachineGunmuzzleFlash_L)
				{
					this.MachineGunmuzzleFlash_L.active = false;
				}
			}
		}
		if (this.mNetworkCharacter.mCharacterFireState == GGCharacterFireState.Reload)
		{
			if ((this.GunType == gunType.Pistol || this.GunType == gunType.Rifle || this.GunType == gunType.SubMachineGun || this.GunType == gunType.MachineGun || this.GunType == gunType.SniperRifle || this.GunType == gunType.PlasmarGun) && this.MachineGunbulletsPerClip - this._MachineGunbulletsLeft > 0 && this._MachineGunclips > 0 && !this.isReload)
			{
				this.mAudioSource.clip = this.MachineGunreloadSound;
				this.mAudioSource.Play();
				base.StartCoroutine(this.MachineGunReload());
			}
			if (this.GunType == gunType.ShotGun && this.ShotGunbulletsPerClip - this._ShotGunbulletsLeft > 0 && this._ShotGunclips > 0 && !this.isReload)
			{
				this.mAudioSource.clip = this.ShotGunreloadSound;
				this.mAudioSource.Play();
				base.StartCoroutine(this.shotGunReload());
			}
		}
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x0007E974 File Offset: 0x0007CD74
	private void firePointSetup()
	{
		if (base.transform.root.tag == "Player")
		{
			Vector3 position = this.mainCam.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, this.mainCam.nearClipPlane));
			this.firePoint.position = position;
		}
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x0007E9E0 File Offset: 0x0007CDE0
	private void MachineGunAwake()
	{
		this._MachineGunbulletsLeft = this.MachineGunbulletsPerClip;
		if (this.MachineGunmuzzleFlash)
		{
			this.MachineGunmuzzleFlash.active = false;
		}
		if (this.dualGun)
		{
			if (this.MachineGunmuzzleFlash_R)
			{
				this.MachineGunmuzzleFlash_R.active = false;
			}
			if (this.MachineGunmuzzleFlash_L)
			{
				this.MachineGunmuzzleFlash_L.active = false;
			}
		}
		if (this.GunType == gunType.SniperRifle)
		{
			this.canAim = true;
		}
		else
		{
			this.canAim = false;
		}
		this.canFire = true;
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x0007EA7E File Offset: 0x0007CE7E
	private void MachineGunFixedUpdate()
	{
		if (this.isReload)
		{
			this.canAim = false;
		}
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x0007EA94 File Offset: 0x0007CE94
	private void MachineGunFire()
	{
		if (Time.time - this.MachineGunfireRate > this.nextFireTime)
		{
			this.nextFireTime = Time.time - Time.deltaTime;
		}
		if (this.weaponName != "TeslaP1")
		{
			while (this.nextFireTime < Time.time && this._MachineGunbulletsLeft != 0)
			{
				this.MachineGunOneShot();
				this.nextFireTime += this.MachineGunfireRate;
			}
		}
		else
		{
			while (this.nextFireTime < Time.time && this._MachineGunbulletsLeft != this._MachineGunclips)
			{
				this.MachineGunOneShot();
				this.nextFireTime += this.MachineGunfireRate;
			}
		}
		if (this.weaponName == "M134" || this.weaponName == "SM134" || this.weaponName == "HonorM134")
		{
			this.weaponPart.transform.localEulerAngles += new Vector3(-720f * Time.deltaTime, 0f, 0f);
		}
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x0007EBD0 File Offset: 0x0007CFD0
	private void MachineGunStopFire()
	{
		if (this.weaponName == "M134" || this.weaponName == "SM134" || this.weaponName == "HonorM134")
		{
			this.weaponPart.transform.localRotation = Quaternion.Lerp(this.weaponPart.transform.localRotation, new Quaternion(0f, 0f, 0f, 1f), Time.deltaTime * 3f);
		}
		if (this.Recoil)
		{
		}
		if (this.beam)
		{
			if (this.weaponName == "TeslaP1")
			{
				IEnumerator enumerator = this.lightBeam.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						transform.GetComponent<GGLightningBeamLaserEffect>().IsLaser = false;
						transform.GetComponent<LineRenderer>().SetPosition(0, transform.position);
						transform.GetComponent<LineRenderer>().SetPosition(1, transform.position);
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
				this.mAudioSource.Stop();
			}
			else if (this.weaponName == "ImpulseGun")
			{
				this.lightBeam.GetComponent<GGLightningBeamLaserEffect_ImpluseGun>().IsLaser = false;
				this.lightBeam.GetComponent<LineRenderer>().SetPosition(0, this.lightBeam.position);
				this.lightBeam.GetComponent<LineRenderer>().SetPosition(1, this.lightBeam.position);
				this.mAudioSource.Stop();
			}
			else if (this.weaponName == "Flamethrower")
			{
				this.lightBeam.gameObject.SetActive(false);
				this.mAudioSource.Stop();
			}
		}
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x0007EDC0 File Offset: 0x0007D1C0
	private void RecoilRecovery()
	{
		this.MachineGunShootCount = 0f;
		this.MachineGunShootCountForRecoil = 0f;
		this.Sliderotate.RecoilRecover(this.DefaultLookObjectAngle);
		this.DefaultLookObjectAngle = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x0007EE10 File Offset: 0x0007D210
	private void RecoilPower()
	{
		this.MachineGunShootCountForRecoil += 1f;
		this.DefaultLookObjectAngle += new Vector3(this.recoilPowerMultiplier.Evaluate(this.MachineGunShootCountForRecoil), 0f, 0f);
		this.MachineGunShootCountForRecoil = Mathf.Min(this.MachineGunShootCountForRecoil, 8f);
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x0007EE78 File Offset: 0x0007D278
	private void MachineGunOneShot()
	{
		this.MachineGunShootCount += 1f;
		this.MachineGunShootCount = Mathf.Min(this.MachineGunShootCount, 5f);
		if (this.Recoil)
		{
		}
		if (!this.aimed)
		{
			this.firePointSetup();
		}
		if (this.dualGun)
		{
			this.gunHand++;
		}
		Quaternion rotation = this.firePoint.rotation;
		GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(this.weaponName);
		if (!this.aimed)
		{
			if (this.GunType == gunType.SniperRifle)
			{
				this.firePoint.rotation = Quaternion.Euler(UnityEngine.Random.insideUnitSphere * this.MachineGunNoAimErrorAngle) * base.transform.rotation;
			}
			else if (this.GunType != gunType.PlasmarGun)
			{
				this.firePoint.rotation = Quaternion.Euler(UnityEngine.Random.insideUnitSphere * this.errorAngleMultiplier.Evaluate(this.MachineGunShootCount) * (1f - GrowthManagerKit.EProperty().allDic[EnchantmentType.AccuracyPlus].additionValue - weaponItemInfoByName.GetPropertyAdditionValue("Accuracy"))) * base.transform.rotation;
			}
			else
			{
				this.firePoint.rotation = Quaternion.Euler(UnityEngine.Random.insideUnitSphere * this.errorAngleMultiplier.Evaluate(this.MachineGunShootCount)) * base.transform.rotation;
			}
		}
		else
		{
			this.firePoint.rotation = Quaternion.Euler(UnityEngine.Random.insideUnitSphere * this.MachineGunAimErrorAngle) * base.transform.rotation;
		}
		GGBullet component;
		if (!this.aimed)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.MachineGunbullet, this.firePoint.position, this.firePoint.rotation);
			component = transform.GetComponent<GGBullet>();
			if (this.GunType == gunType.Pistol || this.GunType == gunType.Rifle || this.GunType == gunType.SubMachineGun || this.GunType == gunType.PlasmarGun)
			{
				this.SetBulletProperty(component, weaponItemInfoByName.mMinDamage, weaponItemInfoByName.mMaxDamage, true, weaponItemInfoByName.GetPropertyAdditionValue("Power"));
			}
			else
			{
				this.SetBulletProperty(component, weaponItemInfoByName.mMinDamage, weaponItemInfoByName.mMaxDamage, false, 0f);
				if (this.weaponName == "M249")
				{
					this.weaponPart.transform.rotation *= Quaternion.EulerAngles(-0.1f, 0f, 0f);
				}
			}
			if (this.GunType == gunType.PlasmarGun)
			{
				component.impactHoles = false;
				if (this.weaponName == "Flamethrower")
				{
					component.speed = 10;
					component.life = 0.067f;
				}
			}
		}
		else
		{
			Vector3 position = this.mainCam.ScreenToWorldPoint(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), this.mainCam.nearClipPlane));
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.MachineGunbullet, position, this.firePoint.rotation);
			component = transform.GetComponent<GGBullet>();
			this.SetBulletProperty(component, weaponItemInfoByName.mMinDamage, weaponItemInfoByName.mMaxDamage, false, 0f);
			if (!this.quickSniper)
			{
				this.aimed = false;
			}
		}
		if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.BurstBullet == 1 && UnityEngine.Random.Range(0, 100) < 5)
		{
			component.bulletDamage = 400f;
			component.isBurstBulletTrigger = true;
		}
		if (this.weaponName == "Nightmare" && UnityEngine.Random.Range(0, 100) < 5)
		{
			component.isNightmareTrigger = true;
		}
		component.shooter = "player";
		this.firePoint.rotation = rotation;
		this.lastShot = Time.time;
		if (base.transform.root.tag == "Player")
		{
			if (this.weaponName != "TeslaP1")
			{
				this._MachineGunbulletsLeft--;
				if (this._MachineGunbulletsLeft == 0)
				{
					if (this.quickSniper)
					{
						this.aimed = false;
					}
					this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
				}
			}
			else
			{
				this._MachineGunbulletsLeft += 20;
				this._MachineGunbulletsLeft = Mathf.Min(this._MachineGunbulletsLeft, this._MachineGunclips);
				if (this._MachineGunbulletsLeft == this._MachineGunclips)
				{
					this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
				}
			}
		}
		if (this.GunType == gunType.PlasmarGun)
		{
			if (!this.mAudioSource.isPlaying)
			{
				this.mAudioSource.clip = this.MachineGunfireSound;
				this.mAudioSource.Play();
			}
		}
		else
		{
			this.mAudioSource.clip = this.MachineGunfireSound;
			this.mAudioSource.Play();
		}
		if (!this.aimed)
		{
			if (!this.dualGun)
			{
				base.StartCoroutine(this.MachineGunMuzzleFlash());
			}
			else if (this.gunHand % 2 == 1)
			{
				base.StartCoroutine(this.MachineGunMuzzleFlash_R());
			}
			else
			{
				base.StartCoroutine(this.MachineGunMuzzleFlash_L());
			}
		}
		if (this.shellCase)
		{
			if (!this.dualGun)
			{
				this.EjectShells();
			}
			else if (this.gunHand % 2 == 1)
			{
				this.EjectShells_R();
			}
			else
			{
				this.EjectShells_L();
			}
		}
		if (this.beam)
		{
			if (this.weaponName == "TeslaP1")
			{
				IEnumerator enumerator = this.lightBeam.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform2 = (Transform)obj;
						transform2.GetComponent<GGLightningBeamLaserEffect>().IsLaser = true;
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
				this.lightBeam.GetComponent<GGLightningBeamLaserControl>().MuzzleAndImpact();
			}
			else if (this.weaponName == "ImpulseGun")
			{
				this.lightBeam.GetComponent<GGLightningBeamLaserEffect_ImpluseGun>().IsLaser = true;
				this.lightBeam.GetComponent<GGLightningBeamLaserEffect_ImpluseGun>().Impact();
			}
			else if (this.weaponName == "Flamethrower")
			{
				this.lightBeam.gameObject.SetActive(true);
			}
		}
		if (!this.aimed || this.AimplayAnimation)
		{
		}
		if (!this.aimed)
		{
			if (!this.dualGun)
			{
				this.mWeaponAnimation.Fire(this.FireAnimationTime);
			}
			else if (this.gunHand % 2 == 1)
			{
				this.mWeaponAnimation.Fire_R(this.FireAnimationTime);
			}
			else
			{
				this.mWeaponAnimation.Fire_L(this.FireAnimationTime);
			}
		}
		if (this.Recoil)
		{
			base.StartCoroutine(this.MachineGunCameraRecoil());
		}
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x0007F580 File Offset: 0x0007D980
	private IEnumerator MachineGunMuzzleFlash()
	{
		if (this.MachineGunmuzzleFlash)
		{
			if (this.weaponName != "ImpulseGun")
			{
				this.MachineGunmuzzleFlash.transform.localRotation = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 359), Vector3.left);
			}
			this.MachineGunmuzzleFlash.active = true;
		}
		if (this.MachineGunpointLight)
		{
			this.MachineGunpointLight.enabled = true;
		}
		yield return new WaitForSeconds(0.04f);
		if (this.MachineGunmuzzleFlash)
		{
			this.MachineGunmuzzleFlash.active = false;
		}
		if (this.MachineGunpointLight)
		{
			this.MachineGunpointLight.enabled = false;
		}
		yield break;
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x0007F59C File Offset: 0x0007D99C
	private IEnumerator MachineGunMuzzleFlash_R()
	{
		if (this.MachineGunmuzzleFlash_R)
		{
			this.MachineGunmuzzleFlash_R.transform.localRotation = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 359), Vector3.left);
			this.MachineGunmuzzleFlash_R.active = true;
		}
		if (this.MachineGunpointLight)
		{
			this.MachineGunpointLight.enabled = true;
		}
		yield return new WaitForSeconds(0.04f);
		if (this.MachineGunmuzzleFlash_R)
		{
			this.MachineGunmuzzleFlash_R.active = false;
		}
		if (this.MachineGunpointLight)
		{
			this.MachineGunpointLight.enabled = false;
		}
		yield break;
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x0007F5B8 File Offset: 0x0007D9B8
	private IEnumerator MachineGunMuzzleFlash_L()
	{
		if (this.MachineGunmuzzleFlash_L)
		{
			this.MachineGunmuzzleFlash_L.transform.localRotation = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 359), Vector3.left);
			this.MachineGunmuzzleFlash_L.active = true;
		}
		if (this.MachineGunpointLight)
		{
			this.MachineGunpointLight.enabled = true;
		}
		yield return new WaitForSeconds(0.04f);
		if (this.MachineGunmuzzleFlash_L)
		{
			this.MachineGunmuzzleFlash_L.active = false;
		}
		if (this.MachineGunpointLight)
		{
			this.MachineGunpointLight.enabled = false;
		}
		yield break;
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x0007F5D4 File Offset: 0x0007D9D4
	private IEnumerator MachineGunReload()
	{
		this.isReload = true;
		this.aimed = false;
		this.canAim = false;
		this.mWeaponAnimation.Reloading(this.MachineGunreloadTime);
		yield return new WaitForSeconds(this.MachineGunreloadTime);
		if (this._MachineGunclips > 0)
		{
			int num = this.MachineGunbulletsPerClip - this._MachineGunbulletsLeft;
			if (this._MachineGunclips > num)
			{
				this._MachineGunclips -= num;
				this._MachineGunbulletsLeft += num;
			}
			else
			{
				this._MachineGunbulletsLeft += this._MachineGunclips;
				this._MachineGunclips = 0;
			}
			this.noBullets = false;
			this.isReload = false;
			this.canAim = true;
			this.reloadFlag = true;
		}
		this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
		yield break;
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x0007F5F0 File Offset: 0x0007D9F0
	private IEnumerator MachineGunCameraRecoil()
	{
		this.camPos = Quaternion.Euler(UnityEngine.Random.Range(0f, -this.CameraRecoilshakeAmount), UnityEngine.Random.Range(-this.CameraRecoilshakeAmount, this.CameraRecoilshakeAmount), 0f);
		yield return new WaitForSeconds(0.05f);
		this.camPos = this.camDefaultRotation;
		yield break;
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x0007F60B File Offset: 0x0007DA0B
	private void grenadeLauncherAwake()
	{
		this.canAim = false;
		this.canFire = true;
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x0007F61B File Offset: 0x0007DA1B
	private void grenadeLauncherFixedUpdate()
	{
		if (this.fire && !this.isReload)
		{
			this.grenadeLauncherFIre();
		}
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x0007F640 File Offset: 0x0007DA40
	private void grenadeLauncherFIre()
	{
		if (this._GrenadeLauncherammoCount == 0 || !this.canFire)
		{
			return;
		}
		if (Time.time - this.GrenadeLauncherreloadTime > this.nextFireTime)
		{
			this.nextFireTime = Time.time - Time.deltaTime;
		}
		while (this.nextFireTime < Time.time && this._GrenadeLauncherammoCount > 0)
		{
			this.grenadeLauncherOneShot();
			this.nextFireTime += this.GrenadeLauncherreloadTime;
		}
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x0007F6C8 File Offset: 0x0007DAC8
	private void grenadeLauncherOneShot()
	{
		Rigidbody rigidbody = UnityEngine.Object.Instantiate<Rigidbody>(this.GrenadeLauncherprojectile, this.firePoint.position, this.firePoint.rotation);
		GGProjectile component = rigidbody.GetComponent<GGProjectile>();
		if (base.transform.root.tag == "Player" && this.weaponPart != null)
		{
			this.weaponPart.GetComponent<Renderer>().enabled = false;
		}
		if (this.weaponName != "RPG" && this.weaponName != "NuclearEmitter")
		{
			rigidbody.velocity = base.transform.TransformDirection(new Vector3(0f, 8f, this.GrenadeLauncherinitialSpeed));
		}
		else
		{
			rigidbody.velocity = base.transform.TransformDirection(new Vector3(0f, 0f, this.GrenadeLauncherinitialSpeed));
		}
		component.shooter = "player";
		component.mutiplayerId = this.mNetworkCharacter.mPlayerProperties.id;
		component.weapontype = this.mNetworkCharacter.mWeaponType;
		component.name = this.mNetworkCharacter.mPlayerProperties.name;
		component.team = this.mNetworkCharacter.mPlayerProperties.team;
		component.shooterPositionX = this.mNetworkCharacter.transform.root.position.x;
		component.shooterPositionY = this.mNetworkCharacter.transform.root.position.y;
		component.shooterPositionZ = this.mNetworkCharacter.transform.root.position.z;
		component.isPlused = this.isPlused;
		if (this.isShockWeapon)
		{
			component.isShockWeapon = this.isShockWeapon;
		}
		component.RangeUpgradeValue = GrowthManagerKit.GetWeaponItemInfoByName(this.weaponName).GetPropertyAdditionValue("Range");
		this.lastShot = Time.time;
		if (this.weaponName == "RPG" || this.weaponName == "MiniCannon" || this.weaponName == "NuclearEmitter")
		{
			this._GrenadeLauncherammoCount--;
			if (this._GrenadeLauncherammoCount == 0)
			{
				this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
			}
		}
		this.mAudioSource.clip = this.GrenadeLauncherfireSound;
		this.mAudioSource.Play();
		if (this.GrenadeLaunchershotDelay == 0f)
		{
			if (!this.aimed || this.AimplayAnimation)
			{
			}
			if (!this.aimed)
			{
			}
			if (this.Recoil)
			{
				base.StartCoroutine(this.grenadeLauncherCameraRecoil());
			}
			base.StartCoroutine(this.grenadeLauncherReload());
		}
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x0007F9A4 File Offset: 0x0007DDA4
	private IEnumerator grenadeLauncherReload()
	{
		this.isReload = true;
		this.aimed = false;
		this.mAudioSource.clip = this.GrenadeLauncherreloadSound;
		this.mAudioSource.Play();
		this.mWeaponAnimation.Reloading(this.GrenadeLauncherreloadTime);
		yield return new WaitForSeconds(this.GrenadeLauncherreloadTime);
		this.isReload = false;
		if (base.transform.root.tag == "Player")
		{
			if (this.GunType == gunType.Thrown)
			{
				if (this.weaponPart != null)
				{
					this.weaponPart.GetComponent<Renderer>().enabled = true;
				}
				base.gameObject.SetActive(false);
				this.weaponManager.ChangeToPreweapon();
			}
			else if (this.weaponName == "RPG")
			{
				this.weaponPart.GetComponent<Renderer>().enabled = true;
			}
		}
		yield break;
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x0007F9C0 File Offset: 0x0007DDC0
	private IEnumerator grenadeLauncherCameraRecoil()
	{
		this.camPos = Quaternion.Euler(UnityEngine.Random.Range(-this.CameraRecoilshakeAmount * 1.5f, -this.CameraRecoilshakeAmount), UnityEngine.Random.Range(this.CameraRecoilshakeAmount / 3f, this.CameraRecoilshakeAmount / 2f), 0f);
		yield return new WaitForSeconds(0.1f);
		this.camPos = this.camDefaultRotation;
		yield break;
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x0007F9DB File Offset: 0x0007DDDB
	private void shotGunAwake()
	{
		this._ShotGunbulletsLeft = this.ShotGunbulletsPerClip;
		if (this.ShotGunsmoke)
		{
		}
		this.canAim = false;
		this.canFire = true;
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x0007FA07 File Offset: 0x0007DE07
	private void shotGunFixedUpdate()
	{
		if (this.fire && !this.isReload)
		{
			this.shotGunFire();
		}
		else
		{
			this.shotGunStopFire();
		}
		if (this.isReload)
		{
			this.canAim = false;
		}
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x0007FA44 File Offset: 0x0007DE44
	private void shotGunFire()
	{
		if (Time.time - this.ShotGunfireRate > this.nextFireTime)
		{
			this.nextFireTime = Time.time - Time.deltaTime;
		}
		while (this.nextFireTime < Time.time && this._ShotGunbulletsLeft != 0)
		{
			this.shotGunOneShot();
			this.nextFireTime += this.ShotGunfireRate;
		}
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x0007FAB2 File Offset: 0x0007DEB2
	private void shotGunStopFire()
	{
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x0007FAB4 File Offset: 0x0007DEB4
	private void shotGunOneShot()
	{
		this.firePointSetup();
		Quaternion rotation = this.firePoint.rotation;
		for (int i = 0; i < this.ShotGunfractions; i++)
		{
			this.firePoint.rotation = Quaternion.Euler(UnityEngine.Random.insideUnitSphere * this.ShotGunerrorAngle) * base.transform.rotation;
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.ShotGunbullet, this.firePoint.position, this.firePoint.rotation);
			GGBullet component = transform.GetComponent<GGBullet>();
			component.shooter = "player";
			GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(this.weaponName);
			this.SetBulletProperty(component, weaponItemInfoByName.mMinDamage, weaponItemInfoByName.mMaxDamage, true, weaponItemInfoByName.GetPropertyAdditionValue("Power"));
			if (this.mNetworkCharacter.mPlayerProperties.MutationPropTrigger.BurstBullet == 1 && UnityEngine.Random.Range(0, 100) < 5)
			{
				component.bulletDamage = 400f;
				component.isBurstBulletTrigger = true;
			}
		}
		this.firePoint.rotation = rotation;
		this.lastShot = Time.time;
		this.mAudioSource.clip = this.ShotGunfireSound;
		this.mAudioSource.Play();
		if (base.transform.root.tag == "Player")
		{
			this._ShotGunbulletsLeft--;
			if (this._ShotGunbulletsLeft == 0)
			{
				this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
			}
		}
		if (!this.aimed || this.AimplayAnimation)
		{
		}
		if (!this.aimed)
		{
			this.mWeaponAnimation.Fire(this.FireAnimationTime);
		}
		this.shotGunSmokeEffect();
		if (this.shellCase)
		{
			this.EjectShells();
		}
		if (this.Recoil)
		{
			base.StartCoroutine(this.shotGunCameraRecoil());
		}
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x0007FC90 File Offset: 0x0007E090
	private IEnumerator shotGunReload()
	{
		this.isReload = true;
		this.aimed = false;
		this.mWeaponAnimation.Reloading(this.ShotGunreloadTime);
		yield return new WaitForSeconds(this.ShotGunreloadTime);
		if (this._ShotGunclips > 0)
		{
			int num = this.ShotGunbulletsPerClip - this._ShotGunbulletsLeft;
			if (this._ShotGunclips > num)
			{
				this._ShotGunclips -= num;
				this._ShotGunbulletsLeft += num;
			}
			else
			{
				this._ShotGunbulletsLeft += this._ShotGunclips;
				this._ShotGunclips = 0;
			}
			this.noBullets = false;
			this.isReload = false;
			this.canAim = true;
			this.reloadFlag = true;
		}
		this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Idle;
		yield break;
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x0007FCAB File Offset: 0x0007E0AB
	private void shotGunSmokeEffect()
	{
		if (!this.ShotGunsmoke)
		{
			return;
		}
		this.ShotGunsmoke.Play(true);
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x0007FCCC File Offset: 0x0007E0CC
	private IEnumerator shotGunCameraRecoil()
	{
		this.camPos = Quaternion.Euler(UnityEngine.Random.Range(-this.CameraRecoilshakeAmount * 1.5f, -this.CameraRecoilshakeAmount), UnityEngine.Random.Range(this.CameraRecoilshakeAmount / 3f, this.CameraRecoilshakeAmount / 2f), 0f);
		yield return new WaitForSeconds(0.1f);
		this.camPos = this.camDefaultRotation;
		yield break;
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x0007FCE7 File Offset: 0x0007E0E7
	private void knifeAwake()
	{
		this.canAim = false;
		this.canFire = true;
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x0007FCF8 File Offset: 0x0007E0F8
	private void knifeOneShot()
	{
		if (Time.time > this.knifefireRate + this.lastShot)
		{
			this.firePointSetup();
			this.mAudioSource.clip = this.knifefireSound;
			this.mAudioSource.Play();
			this.mWeaponAnimation.Fire(this.FireAnimationTime);
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.knifebullet, this.firePoint.position, this.firePoint.rotation);
			GGBullet component = transform.GetComponent<GGBullet>();
			component.shooter = "player";
			GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(this.weaponName);
			if (this.weaponName != "ZombieHand")
			{
				this.SetBulletProperty(component, weaponItemInfoByName.mMinDamage, weaponItemInfoByName.mMaxDamage, true, weaponItemInfoByName.GetPropertyAdditionValue("Power"));
			}
			else
			{
				this.SetBulletProperty(component, weaponItemInfoByName.mMinDamage, weaponItemInfoByName.mMaxDamage, false, 0f);
				if (this.mNetworkCharacter.zombieSkinIndex == 3)
				{
					component.life = 0.2f;
					if (this.DuyeEffect == null)
					{
						this.DuyeEffect = base.transform.root.Find("LookObject/CharacterDuyeEffect").gameObject;
					}
					if (!this.DuyeEffect.activeSelf)
					{
						this.DuyeEffect.SetActive(true);
					}
					this.DuyeEffect.GetComponent<ParticleSystem>().Play();
				}
				if (this.mNetworkCharacter.zombieSkinIndex == 1)
				{
					int num = UnityEngine.Random.Range(0, 100);
					if (num < 8)
					{
						this.mNetworkCharacter.mPlayerProperties.MutationSkill.ActiveHorror = 1;
						Collider[] array = Physics.OverlapSphere(base.transform.root.position, 8f);
						foreach (Collider collider in array)
						{
							if (collider)
							{
								if (collider.GetComponent<Collider>().gameObject.tag == "EnemyHeadTag")
								{
									GGMessage ggmessage = new GGMessage();
									ggmessage.messageType = GGMessageType.MessageNotifyMutationHorror;
									int ownerId = collider.GetComponent<Collider>().transform.root.GetComponent<PhotonView>().ownerId;
									GGNetworkKit.mInstance.SendMessage(ggmessage, ownerId);
								}
							}
						}
						base.StartCoroutine(this.ActiveHorrorReturn(2f));
					}
				}
			}
			this.lastShot = Time.time;
		}
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x0007FF5C File Offset: 0x0007E35C
	private void Aiming()
	{
		if (this.aimed)
		{
			this.currentPosition = this.AimaimPosition;
			this.currentFov = this.AimtoFov;
			this.walkSway.bobbingAmount = this.AimaimBobbingAmount;
		}
		else
		{
			this.currentPosition = this.defaultPosition;
			this.currentFov = this.defaultFov;
			this.walkSway.bobbingAmount = this.defaultBobbingAmount;
		}
		if (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Dead)
		{
			return;
		}
		this.mainCam.fieldOfView = Mathf.Lerp(this.mainCam.fieldOfView, this.currentFov, Time.deltaTime / this.AimsmoothTime);
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x0008000A File Offset: 0x0007E40A
	private void cameraRecoilDo()
	{
		this.mainCam.transform.localRotation = Quaternion.Slerp(this.mainCam.transform.localRotation, this.camPos, Time.deltaTime * this.CameraRecoilsmooth);
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x00080044 File Offset: 0x0007E444
	private void RotationRealism()
	{
		float axis = Input.GetAxis("Mouse X");
		float axis2 = Input.GetAxis("Mouse Y");
		float y = 0f;
		float x = 0f;
		if (Mathf.Abs(axis) > 0.1f)
		{
			if (axis < 0.1f)
			{
				y = -this.RotRealismRotationAmplitude * Mathf.Abs(axis);
			}
			else if (axis > 0.1f)
			{
				y = this.RotRealismRotationAmplitude * Mathf.Abs(axis);
			}
		}
		else
		{
			y = 0f;
		}
		if (Mathf.Abs(axis2) > 0.1f)
		{
			if (axis2 < 0.1f)
			{
				x = this.RotRealismRotationAmplitude * Mathf.Abs(axis2);
			}
			else if (axis2 > 0.1f)
			{
				x = -this.RotRealismRotationAmplitude * Mathf.Abs(axis2);
			}
		}
		else
		{
			x = 0f;
		}
		Quaternion b = Quaternion.Euler(x, y, 0f);
		base.transform.localRotation = Quaternion.Slerp(base.transform.localRotation, b, Time.deltaTime * this.RotRealismsmooth);
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x00080150 File Offset: 0x0007E550
	private void SmoothMove()
	{
		float y = this.controller.velocity.y;
		float num = 0f;
		float num2 = -Input.GetAxis("Vertical");
		if (y > this.SmoothMovementmaxAmount + 1f)
		{
			num = -this.SmoothMovementmaxAmount;
		}
		if (y < -this.SmoothMovementmaxAmount - 1f)
		{
			num = this.SmoothMovementmaxAmount;
		}
		if (num2 > this.SmoothMovementmaxAmount)
		{
			num2 = this.SmoothMovementmaxAmount;
		}
		if (num2 < -this.SmoothMovementmaxAmount)
		{
			num2 = -this.SmoothMovementmaxAmount;
		}
		Vector3 b = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y + num, base.transform.localPosition.z + num2);
		base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, b, Time.deltaTime * this.SmoothMovementSmooth);
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x00080250 File Offset: 0x0007E650
	public void selectWeapon()
	{
		this.canFire = true;
		if (this.GunType != gunType.Knife)
		{
			this.canAim = true;
		}
		this.aimed = false;
		if (base.transform.root.tag == "Player")
		{
			base.StartCoroutine(this.mWeaponAnimation.takeIn());
		}
	}

	// Token: 0x06000F18 RID: 3864 RVA: 0x000802AE File Offset: 0x0007E6AE
	public void deselectWeapon()
	{
		this.aimed = false;
		this.isReload = false;
		this.canFire = false;
		this.canAim = false;
	}

	// Token: 0x06000F19 RID: 3865 RVA: 0x000802CC File Offset: 0x0007E6CC
	public void buyBullet()
	{
		if (this.GunType == gunType.Pistol || this.GunType == gunType.Rifle || this.GunType == gunType.SubMachineGun || this.GunType == gunType.MachineGun || this.GunType == gunType.SniperRifle || this.GunType == gunType.PlasmarGun)
		{
			if (this.weaponName != "TeslaP1" && this.weaponName != "Flamethrower")
			{
				this._MachineGunclips += this.MachineGunbulletsPerClip;
				if (this._MachineGunbulletsLeft == 0 && !this.isReload)
				{
					base.StartCoroutine(this.MachineGunReload());
				}
			}
			else if (this.weaponName == "Flamethrower")
			{
				this._MachineGunbulletsLeft = this.MachineGunbulletsPerClip;
				if (!this.isReload)
				{
					this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Reload;
					base.StartCoroutine(this.MachineGunReload());
				}
			}
		}
		else if (this.GunType == gunType.ShotGun)
		{
			this._ShotGunclips += this.ShotGunbulletsPerClip;
			if (this._ShotGunbulletsLeft == 0 && !this.isReload)
			{
				base.StartCoroutine(this.shotGunReload());
			}
		}
		else if (this.GunType == gunType.Bazooka || this.GunType == gunType.Thrown)
		{
			if (this.weaponName == "RPG" || this.weaponName == "MiniCannon")
			{
				this._GrenadeLauncherammoCount += 5;
			}
			else
			{
				this._GrenadeLauncherammoCount += 5;
			}
			if (this._GrenadeLauncherammoCount == 0 && !this.isReload)
			{
				base.StartCoroutine(this.grenadeLauncherReload());
			}
		}
	}

	// Token: 0x06000F1A RID: 3866 RVA: 0x000804A0 File Offset: 0x0007E8A0
	public void PickUpBullet()
	{
		if (this.GunType == gunType.Pistol || this.GunType == gunType.Rifle || this.GunType == gunType.SubMachineGun || this.GunType == gunType.MachineGun || this.GunType == gunType.SniperRifle || this.GunType == gunType.PlasmarGun)
		{
			if (this.weaponName != "TeslaP1" && this.weaponName != "Flamethrower")
			{
				this._MachineGunclips += this.MachineGunbulletsPerClip * 4;
				if (this._MachineGunbulletsLeft == 0 && !this.isReload)
				{
					base.StartCoroutine(this.MachineGunReload());
				}
			}
			else if (this.weaponName == "Flamethrower")
			{
				this._MachineGunbulletsLeft = this.MachineGunbulletsPerClip;
				if (!this.isReload)
				{
					this.mNetworkCharacter.mCharacterFireState = GGCharacterFireState.Reload;
					base.StartCoroutine(this.MachineGunReload());
				}
			}
		}
		else if (this.GunType == gunType.ShotGun)
		{
			this._ShotGunclips += this.ShotGunbulletsPerClip * 4;
			if (this._ShotGunbulletsLeft == 0 && !this.isReload)
			{
				base.StartCoroutine(this.shotGunReload());
			}
		}
		else if (this.GunType == gunType.Bazooka || this.GunType == gunType.Thrown)
		{
			if (this.weaponName == "RPG" || this.weaponName == "MiniCannon")
			{
				this._GrenadeLauncherammoCount += 10;
			}
			else
			{
				this._GrenadeLauncherammoCount += 10;
			}
			if (this._GrenadeLauncherammoCount == 0 && !this.isReload)
			{
				base.StartCoroutine(this.grenadeLauncherReload());
			}
		}
	}

	// Token: 0x06000F1B RID: 3867 RVA: 0x00080678 File Offset: 0x0007EA78
	private IEnumerator ActiveHorrorReturn(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.mNetworkCharacter.mPlayerProperties.MutationSkill.ActiveHorror = 0;
		yield break;
	}

	// Token: 0x06000F1C RID: 3868 RVA: 0x0008069C File Offset: 0x0007EA9C
	private void EjectShells()
	{
		Rigidbody rigidbody = UnityEngine.Object.Instantiate<Rigidbody>(this.ShellCase, this.ejectPoint.position, base.transform.rotation);
		if (this.GunType == gunType.SniperRifle)
		{
			rigidbody.velocity = this.ejectPoint.transform.forward * this.ejectForce * this.ejectForce * 2f;
		}
		else
		{
			rigidbody.velocity = this.ejectPoint.transform.forward * this.ejectForce * this.ejectForce * 5f;
		}
	}

	// Token: 0x06000F1D RID: 3869 RVA: 0x00080748 File Offset: 0x0007EB48
	private void EjectShells_R()
	{
		Rigidbody rigidbody = UnityEngine.Object.Instantiate<Rigidbody>(this.ShellCase, this.ejectPoint_R.position, base.transform.rotation);
		if (this.GunType == gunType.SniperRifle)
		{
			rigidbody.velocity = this.ejectPoint_R.transform.forward * this.ejectForce * this.ejectForce * 2f;
		}
		else
		{
			rigidbody.velocity = this.ejectPoint_R.transform.forward * this.ejectForce * this.ejectForce * 5f;
		}
	}

	// Token: 0x06000F1E RID: 3870 RVA: 0x000807F4 File Offset: 0x0007EBF4
	private void EjectShells_L()
	{
		Rigidbody rigidbody = UnityEngine.Object.Instantiate<Rigidbody>(this.ShellCase, this.ejectPoint_L.position, base.transform.rotation);
		if (this.GunType == gunType.SniperRifle)
		{
			rigidbody.velocity = this.ejectPoint_L.transform.forward * this.ejectForce * this.ejectForce * 2f;
		}
		else
		{
			rigidbody.velocity = this.ejectPoint_L.transform.forward * this.ejectForce * this.ejectForce * 5f;
		}
	}

	// Token: 0x06000F1F RID: 3871 RVA: 0x000808A0 File Offset: 0x0007ECA0
	private void SetBulletProperty(GGBullet B, float minDamage, float maxDamage, bool isPowerup, float value)
	{
		B.bulletDamage = UnityEngine.Random.Range(minDamage, maxDamage) * ((!isPowerup) ? 1f : (1f + value));
		B.mutiplayerId = this.mNetworkCharacter.mPlayerProperties.id;
		B.weapontype = this.mNetworkCharacter.mWeaponType;
		B.name = this.mNetworkCharacter.mPlayerProperties.name;
		B.team = this.mNetworkCharacter.mPlayerProperties.team;
		B.shooterPositionX = this.mNetworkCharacter.transform.root.position.x;
		B.shooterPositionY = this.mNetworkCharacter.transform.root.position.y;
		B.shooterPositionZ = this.mNetworkCharacter.transform.root.position.z;
	}

	// Token: 0x06000F20 RID: 3872 RVA: 0x00080990 File Offset: 0x0007ED90
	public void SetUpgradeProperty(GWeaponItemInfo wInfo)
	{
		if (this.GunType == gunType.ShotGun)
		{
			this._ShotGunclips = (int)((float)this._ShotGunclips * (1f + wInfo.GetPropertyAdditionValue("Clip")));
			this.ShotGunbulletsPerClip = (int)((float)this.ShotGunbulletsPerClip * (1f + wInfo.GetPropertyAdditionValue("Clip")));
			this._ShotGunbulletsLeft = (int)((float)this._ShotGunbulletsLeft * (1f + wInfo.GetPropertyAdditionValue("Clip")));
		}
		else if (this.GunType == gunType.SniperRifle)
		{
			this.AimtoFov *= 1f - wInfo.GetPropertyAdditionValue("Aim");
		}
		else if (this.GunType == gunType.Rifle || this.GunType == gunType.MachineGun || this.GunType == gunType.SubMachineGun || this.GunType == gunType.Pistol)
		{
			this._MachineGunclips = (int)((float)this._MachineGunclips * (1f + wInfo.GetPropertyAdditionValue("Clip")));
			this.MachineGunbulletsPerClip = (int)((float)this.MachineGunbulletsPerClip * (1f + wInfo.GetPropertyAdditionValue("Clip")));
			this._MachineGunbulletsLeft = (int)((float)this._MachineGunbulletsLeft * (1f + wInfo.GetPropertyAdditionValue("Clip")));
		}
		else if (this.GunType == gunType.PlasmarGun)
		{
			if (this.weaponName == "TeslaP1" || this.weaponName == "Flamethrower")
			{
				this._MachineGunclips = (int)((float)this._MachineGunclips * (1f + wInfo.GetPropertyAdditionValue("Energy")));
				this.MachineGunbulletsPerClip = (int)((float)this.MachineGunbulletsPerClip * (1f + wInfo.GetPropertyAdditionValue("Energy")));
				this._MachineGunbulletsLeft = (int)((float)this._MachineGunbulletsLeft * (1f + wInfo.GetPropertyAdditionValue("Energy")));
			}
			else
			{
				this._MachineGunclips = (int)((float)this._MachineGunclips * (1f + wInfo.GetPropertyAdditionValue("Clip")));
				this.MachineGunbulletsPerClip = (int)((float)this.MachineGunbulletsPerClip * (1f + wInfo.GetPropertyAdditionValue("Clip")));
				this._MachineGunbulletsLeft = (int)((float)this._MachineGunbulletsLeft * (1f + wInfo.GetPropertyAdditionValue("Clip")));
			}
		}
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x00080BCD File Offset: 0x0007EFCD
	public void SetWeaponPosition(Vector3 p, Vector3 e, Vector3 s)
	{
		base.transform.localPosition = p;
		base.transform.localEulerAngles = e;
		base.transform.localScale = s;
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x00080BF3 File Offset: 0x0007EFF3
	public void InitAnimation()
	{
		this.mWeaponAnimation = base.gameObject.GetComponentInChildren<GGWeaponAnimation>();
	}

	// Token: 0x04001067 RID: 4199
	public bool aimed;

	// Token: 0x04001068 RID: 4200
	public bool fire;

	// Token: 0x04001069 RID: 4201
	public bool canAim;

	// Token: 0x0400106A RID: 4202
	public bool isReload;

	// Token: 0x0400106B RID: 4203
	public bool noBullets;

	// Token: 0x0400106C RID: 4204
	public bool Recoil;

	// Token: 0x0400106D RID: 4205
	public bool canFire;

	// Token: 0x0400106E RID: 4206
	public bool singleFire;

	// Token: 0x0400106F RID: 4207
	public bool shellCase;

	// Token: 0x04001070 RID: 4208
	public bool beam;

	// Token: 0x04001071 RID: 4209
	public bool dualGun;

	// Token: 0x04001072 RID: 4210
	public bool quickSniper;

	// Token: 0x04001073 RID: 4211
	public Camera cam;

	// Token: 0x04001074 RID: 4212
	public Camera mainCam;

	// Token: 0x04001075 RID: 4213
	public bool reloadFlag = true;

	// Token: 0x04001076 RID: 4214
	public bool isAiming;

	// Token: 0x04001077 RID: 4215
	public bool addBullet;

	// Token: 0x04001078 RID: 4216
	private GameObject player;

	// Token: 0x04001079 RID: 4217
	private CharacterController controller;

	// Token: 0x0400107A RID: 4218
	private GGWalkSway walkSway;

	// Token: 0x0400107B RID: 4219
	private GGSliderotate Sliderotate;

	// Token: 0x0400107C RID: 4220
	private GGWeaponManager weaponManager;

	// Token: 0x0400107D RID: 4221
	private float defaultBobbingAmount;

	// Token: 0x0400107E RID: 4222
	private GameObject managerObject;

	// Token: 0x0400107F RID: 4223
	public gunType GunType;

	// Token: 0x04001080 RID: 4224
	public bool bPlayer;

	// Token: 0x04001081 RID: 4225
	public bool FlashLight;

	// Token: 0x04001082 RID: 4226
	public string weaponName = string.Empty;

	// Token: 0x04001083 RID: 4227
	private GameObject Send;

	// Token: 0x04001084 RID: 4228
	private GameObject gameObjectIsPause;

	// Token: 0x04001085 RID: 4229
	private GameObject gameObjectIsDied;

	// Token: 0x04001086 RID: 4230
	public Vector3 AimaimPosition = Vector3.zero;

	// Token: 0x04001087 RID: 4231
	public float AimsmoothTime = 5f;

	// Token: 0x04001088 RID: 4232
	public float AimtoFov = 45f;

	// Token: 0x04001089 RID: 4233
	public float AimaimBobbingAmount;

	// Token: 0x0400108A RID: 4234
	public bool AimplayAnimation;

	// Token: 0x0400108B RID: 4235
	private float defaultFov;

	// Token: 0x0400108C RID: 4236
	private Vector3 defaultPosition;

	// Token: 0x0400108D RID: 4237
	private float currentFov;

	// Token: 0x0400108E RID: 4238
	private Vector3 currentPosition;

	// Token: 0x0400108F RID: 4239
	private Transform laserFirepoint;

	// Token: 0x04001090 RID: 4240
	public bool bLastDied;

	// Token: 0x04001091 RID: 4241
	public int dieTimeLimit;

	// Token: 0x04001092 RID: 4242
	public Transform firePoint;

	// Token: 0x04001093 RID: 4243
	public Transform ShotGunbullet;

	// Token: 0x04001094 RID: 4244
	public int ShotGunfractions = 5;

	// Token: 0x04001095 RID: 4245
	public float ShotGunerrorAngle = 1f;

	// Token: 0x04001096 RID: 4246
	public float ShotGunfireRate = 1f;

	// Token: 0x04001097 RID: 4247
	public float ShotGunreloadTime;

	// Token: 0x04001098 RID: 4248
	public AudioClip ShotGunfireSound;

	// Token: 0x04001099 RID: 4249
	public AudioClip ShotGunreloadSound;

	// Token: 0x0400109A RID: 4250
	public int ShotGunbulletsPerClip = 40;

	// Token: 0x0400109B RID: 4251
	public int ShotGunbulletsLeft;

	// Token: 0x0400109C RID: 4252
	public string StrEncryptShotGunbulletsLeft = string.Empty;

	// Token: 0x0400109D RID: 4253
	public string StrEncryptShotGunclips = string.Empty;

	// Token: 0x0400109E RID: 4254
	public int ShotGunclips = 15;

	// Token: 0x0400109F RID: 4255
	public ParticleSystem ShotGunsmoke;

	// Token: 0x040010A0 RID: 4256
	public Rigidbody GrenadeLauncherprojectile;

	// Token: 0x040010A1 RID: 4257
	public AudioClip GrenadeLauncherfireSound;

	// Token: 0x040010A2 RID: 4258
	public AudioClip GrenadeLauncherreloadSound;

	// Token: 0x040010A3 RID: 4259
	public float GrenadeLauncherinitialSpeed = 20f;

	// Token: 0x040010A4 RID: 4260
	public float GrenadeLaunchershotDelay;

	// Token: 0x040010A5 RID: 4261
	public float GrenadeLauncherwaitBeforeReload = 0.5f;

	// Token: 0x040010A6 RID: 4262
	public float GrenadeLauncherreloadTime = 0.5f;

	// Token: 0x040010A7 RID: 4263
	public bool isShockWeapon;

	// Token: 0x040010A8 RID: 4264
	public int GrenadeLauncherammoCount = 20;

	// Token: 0x040010A9 RID: 4265
	public string StrEncryptGrenadeLauncherammoCount = string.Empty;

	// Token: 0x040010AA RID: 4266
	public Transform MachineGunbullet;

	// Token: 0x040010AB RID: 4267
	public AudioClip MachineGunfireSound;

	// Token: 0x040010AC RID: 4268
	public AudioClip MachineGunreloadSound;

	// Token: 0x040010AD RID: 4269
	public GameObject MachineGunmuzzleFlash;

	// Token: 0x040010AE RID: 4270
	public GameObject MachineGunmuzzleFlash_R;

	// Token: 0x040010AF RID: 4271
	public GameObject MachineGunmuzzleFlash_L;

	// Token: 0x040010B0 RID: 4272
	public Light MachineGunpointLight;

	// Token: 0x040010B1 RID: 4273
	public float MachineGunfireRate;

	// Token: 0x040010B2 RID: 4274
	public int MachineGunbulletsPerClip;

	// Token: 0x040010B3 RID: 4275
	public int MachineGunclips;

	// Token: 0x040010B4 RID: 4276
	public string StrEncryptMachineGunclips = string.Empty;

	// Token: 0x040010B5 RID: 4277
	public int MachineGunbulletsLeft;

	// Token: 0x040010B6 RID: 4278
	public string StrEncryptMachineGunbulletsLeft = string.Empty;

	// Token: 0x040010B7 RID: 4279
	public float MachineGunreloadTime;

	// Token: 0x040010B8 RID: 4280
	public float MachineGunNoAimErrorAngle;

	// Token: 0x040010B9 RID: 4281
	public float MachineGunAimErrorAngle;

	// Token: 0x040010BA RID: 4282
	private float errorAngle;

	// Token: 0x040010BB RID: 4283
	public AnimationCurve errorAngleMultiplier = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(1f, 0f),
		new Keyframe(2f, 1f),
		new Keyframe(3f, 2f),
		new Keyframe(4f, 3f),
		new Keyframe(5f, 4f)
	});

	// Token: 0x040010BC RID: 4284
	private float MachineGunShootCount;

	// Token: 0x040010BD RID: 4285
	private float TeslaP1CoolDownTime;

	// Token: 0x040010BE RID: 4286
	private int gunHand;

	// Token: 0x040010BF RID: 4287
	public Transform knifebullet;

	// Token: 0x040010C0 RID: 4288
	public AudioClip knifefireSound;

	// Token: 0x040010C1 RID: 4289
	public float knifefireRate;

	// Token: 0x040010C2 RID: 4290
	public float knifedelayTime;

	// Token: 0x040010C3 RID: 4291
	public Rigidbody ShellCase;

	// Token: 0x040010C4 RID: 4292
	public Transform ejectPoint;

	// Token: 0x040010C5 RID: 4293
	public Transform ejectPoint_R;

	// Token: 0x040010C6 RID: 4294
	public Transform ejectPoint_L;

	// Token: 0x040010C7 RID: 4295
	public float ejectForce;

	// Token: 0x040010C8 RID: 4296
	private float lastShot = -10f;

	// Token: 0x040010C9 RID: 4297
	private float nextFireTime;

	// Token: 0x040010CA RID: 4298
	public Transform lightBeam;

	// Token: 0x040010CB RID: 4299
	public Transform lightBeamEjectPoint;

	// Token: 0x040010CC RID: 4300
	public float RotRealismRotationAmplitude = 3f;

	// Token: 0x040010CD RID: 4301
	public float RotRealismsmooth = 7f;

	// Token: 0x040010CE RID: 4302
	private float currentAnglex;

	// Token: 0x040010CF RID: 4303
	private float currentAngley;

	// Token: 0x040010D0 RID: 4304
	public float SmoothMovementSmooth = 1f;

	// Token: 0x040010D1 RID: 4305
	public float SmoothMovementmaxAmount = 0.1f;

	// Token: 0x040010D2 RID: 4306
	private Vector3 DefaultPos;

	// Token: 0x040010D3 RID: 4307
	public float CameraRecoilrecoilPower = 0.7f;

	// Token: 0x040010D4 RID: 4308
	public AnimationCurve recoilPowerMultiplier = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(1f, 0f),
		new Keyframe(2f, 0.4f),
		new Keyframe(3f, 0.6f),
		new Keyframe(4f, 0.6f),
		new Keyframe(5f, 0.5f),
		new Keyframe(6f, 0.3f),
		new Keyframe(7f, 0f),
		new Keyframe(8f, 0f)
	});

	// Token: 0x040010D5 RID: 4309
	private Vector3 DefaultLookObjectAngle = new Vector3(0f, 0f, 0f);

	// Token: 0x040010D6 RID: 4310
	private float MachineGunShootCountForRecoil;

	// Token: 0x040010D7 RID: 4311
	public float CameraRecoilshakeAmount = 0.4f;

	// Token: 0x040010D8 RID: 4312
	public float CameraRecoilsmooth = 13f;

	// Token: 0x040010D9 RID: 4313
	private Quaternion camDefaultRotation;

	// Token: 0x040010DA RID: 4314
	private Quaternion camPos;

	// Token: 0x040010DB RID: 4315
	private GameObject UIMenuDirectorExt;

	// Token: 0x040010DC RID: 4316
	private float FireAnimationTime = 0.1f;

	// Token: 0x040010DD RID: 4317
	private GameObject buyBulletPriceGO;

	// Token: 0x040010DE RID: 4318
	private GameObject weaponPart;

	// Token: 0x040010DF RID: 4319
	private GGWeaponAnimation mWeaponAnimation;

	// Token: 0x040010E0 RID: 4320
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x040010E1 RID: 4321
	private GGCameraRecoil mCameraRecoil;

	// Token: 0x040010E2 RID: 4322
	public bool isPlused;

	// Token: 0x040010E3 RID: 4323
	public int upgradeLv;

	// Token: 0x040010E4 RID: 4324
	public GameObject weaponBody;

	// Token: 0x040010E5 RID: 4325
	public GameObject DuyeEffect;

	// Token: 0x040010E6 RID: 4326
	private AudioSource mAudioSource;
}
