using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000258 RID: 600
[RequireComponent(typeof(AudioSource))]
public class WeaponSynForMultiplayer : MonoBehaviour
{
	// Token: 0x06001140 RID: 4416 RVA: 0x00099A14 File Offset: 0x00097E14
	private void Awake()
	{
		if (this.muzzleFlash)
		{
			this.muzzleFlash.GetComponent<Renderer>().enabled = false;
		}
		if (this.dualGun)
		{
			if (this.muzzleFlash_R)
			{
				this.muzzleFlash_R.enabled = false;
			}
			if (this.muzzleFlash_L)
			{
				this.muzzleFlash_L.enabled = false;
			}
		}
		base.GetComponent<AudioSource>().playOnAwake = false;
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x00099A94 File Offset: 0x00097E94
	private void Start()
	{
		this.mNetworkCharacter = base.transform.root.GetComponent<GGNetworkCharacter>();
		if (base.gameObject.name == "M134(Clone)")
		{
			this.weaponPart = base.transform.Find("M134/M134_2");
		}
		if (base.gameObject.name == "SM134(Clone)")
		{
			this.weaponPart = base.transform.Find("M134/M134_2");
		}
		if (base.gameObject.name == "HonorM134(Clone)")
		{
			this.weaponPart = base.transform.Find("M134/M134_2");
		}
		this.SetWeaponUpgrade(this.upgradeLv, this.weaponName);
		this.SetBulletRemote(this.upgradeLv);
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x00099B68 File Offset: 0x00097F68
	private void Update()
	{
		if (this.mNetworkCharacter.mCharacterFireState == GGCharacterFireState.Fire)
		{
			this.fire = true;
		}
		else
		{
			this.fire = false;
		}
		if (this.gunType == WeaponSynForMultiplayer.GunType.MachineGun)
		{
			this.MachineGunFixedUpdate();
		}
		else if (this.gunType == WeaponSynForMultiplayer.GunType.ShotGun)
		{
			this.ShotGunFixedUpdate();
		}
		else if (this.gunType == WeaponSynForMultiplayer.GunType.GrenadeLauncher)
		{
			this.GrenadeLauncherFixedUpdate();
		}
		else if (this.gunType == WeaponSynForMultiplayer.GunType.Knife)
		{
			this.KnifeFixedUpdate();
		}
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x00099BF0 File Offset: 0x00097FF0
	private void MachineGunFixedUpdate()
	{
		if (this.fire)
		{
			this.MachineGunFire();
			if (base.gameObject.name == "M134(Clone)" || base.gameObject.name == "SM134(Clone)" || base.gameObject.name == "HonorM134(Clone)")
			{
				this.weaponPart.localEulerAngles += new Vector3(-720f * Time.deltaTime, 0f, 0f);
			}
		}
		else
		{
			if (base.gameObject.name == "M134(Clone)" || base.gameObject.name == "SM134(Clone)" || base.gameObject.name == "HonorM134(Clone)")
			{
				this.weaponPart.localRotation = Quaternion.Lerp(this.weaponPart.localRotation, new Quaternion(0f, 0f, 0f, 1f), Time.deltaTime * 3f);
			}
			if (this.beam && this.lightBeamFire != this.fire && this.lightBeamFire)
			{
				if (base.gameObject.name == "TeslaP1(Clone)")
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
					this.lightBeamFire = this.fire;
					base.GetComponent<AudioSource>().Stop();
				}
				else if (base.gameObject.name == "ImpulseGun(Clone)")
				{
					this.lightBeam.GetComponent<GGLightningBeamLaserEffect_ImpluseGun>().IsLaser = false;
					this.lightBeam.GetComponent<LineRenderer>().SetPosition(0, this.lightBeam.position);
					this.lightBeam.GetComponent<LineRenderer>().SetPosition(1, this.lightBeam.position);
					this.lightBeamFire = this.fire;
					base.GetComponent<AudioSource>().Stop();
				}
				else if (base.gameObject.name == "Flamethrower(Clone)")
				{
					this.lightBeam.gameObject.SetActive(false);
					this.lightBeamFire = this.fire;
					base.GetComponent<AudioSource>().Stop();
				}
			}
		}
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x00099EC0 File Offset: 0x000982C0
	private void MachineGunFire()
	{
		if (Time.time - this.fireRate > this.nextFireTime)
		{
			this.nextFireTime = Time.time - Time.deltaTime;
		}
		while (this.nextFireTime < Time.time)
		{
			this.MachineGunShot();
			this.nextFireTime += this.fireRate;
		}
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x00099F24 File Offset: 0x00098324
	private void MachineGunShot()
	{
		Quaternion rotation = this.firePoint.rotation;
		this.firePoint.rotation = Quaternion.Euler(UnityEngine.Random.insideUnitSphere * this.errorAngle) * this.firePoint.rotation;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.bullet, this.firePoint.position, this.firePoint.rotation);
		if (this.beam)
		{
			if (base.gameObject.name == "TeslaP1(Clone)")
			{
				IEnumerator enumerator = this.lightBeam.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						transform.GetComponent<GGLightningBeamLaserEffect>().IsLaser = true;
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
				gameObject.GetComponent<TrailRenderer>().enabled = false;
				this.lightBeamFire = true;
			}
			else if (base.gameObject.name == "ImpulseGun(Clone)")
			{
				this.lightBeam.GetComponent<GGLightningBeamLaserEffect_ImpluseGun>().IsLaser = true;
				this.lightBeam.GetComponent<GGLightningBeamLaserEffect_ImpluseGun>().Impact();
				gameObject.GetComponent<TrailRenderer>().enabled = false;
				this.lightBeamFire = true;
			}
			else if (base.gameObject.name == "Flamethrower(Clone)")
			{
				this.lightBeam.gameObject.SetActive(true);
				gameObject.GetComponent<TrailRenderer>().enabled = false;
				this.lightBeamFire = true;
			}
		}
		this.firePoint.rotation = rotation;
		if (base.gameObject.name != "TeslaP1(Clone)" && base.gameObject.name != "ImpulseGun(Clone)" && base.gameObject.name != "Flamethrower(Clone)")
		{
			base.GetComponent<AudioSource>().clip = this.fireAudio;
			base.GetComponent<AudioSource>().Play();
		}
		else if ((base.gameObject.name == "TeslaP1(Clone)" || base.gameObject.name == "ImpulseGun(Clone)" || base.gameObject.name == "Flamethrower(Clone)") && !base.GetComponent<AudioSource>().isPlaying)
		{
			base.GetComponent<AudioSource>().clip = this.fireAudio;
			base.GetComponent<AudioSource>().Play();
		}
		if (!this.dualGun)
		{
			base.StartCoroutine(this.MuzzleFlash(0.04f));
		}
		else
		{
			if (this.mGGNetWorkPlayerAnimationControl == null)
			{
				this.mGGNetWorkPlayerAnimationControl = base.transform.root.GetComponentInChildren<GGNetWorkPlayerAnimationControl>();
			}
			this.mGGNetWorkPlayerAnimationControl.dualGunHandIndex++;
			if (this.mGGNetWorkPlayerAnimationControl.dualGunHandIndex % 2 == 1)
			{
				base.StartCoroutine(this.MuzzleFlash_R(0.04f));
			}
			else
			{
				base.StartCoroutine(this.MuzzleFlash_L(0.04f));
			}
		}
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x0009A25C File Offset: 0x0009865C
	private void ShotGunFixedUpdate()
	{
		if (this.fire)
		{
			this.ShotGunFire();
		}
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x0009A270 File Offset: 0x00098670
	private void ShotGunFire()
	{
		if (Time.time - this.fireRate > this.nextFireTime)
		{
			this.nextFireTime = Time.time - Time.deltaTime;
		}
		while (this.nextFireTime < Time.time)
		{
			this.ShotGunOneShot();
			this.nextFireTime += this.fireRate;
		}
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x0009A2D4 File Offset: 0x000986D4
	private void ShotGunOneShot()
	{
		for (int i = 0; i < this.fractions; i++)
		{
			Quaternion rotation = this.firePoint.rotation;
			this.firePoint.rotation = Quaternion.Euler(UnityEngine.Random.insideUnitSphere * 3f) * this.firePoint.rotation;
			UnityEngine.Object.Instantiate<GameObject>(this.bullet, this.firePoint.position, this.firePoint.rotation);
			this.firePoint.rotation = rotation;
		}
		base.GetComponent<AudioSource>().clip = this.fireAudio;
		base.GetComponent<AudioSource>().Play();
		base.StartCoroutine(this.MuzzleFlash(0.04f));
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x0009A38F File Offset: 0x0009878F
	private void GrenadeLauncherFixedUpdate()
	{
		if (this.fire)
		{
			this.GrenadeLauncherFire();
		}
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x0009A3A4 File Offset: 0x000987A4
	private void GrenadeLauncherFire()
	{
		if (Time.time - this.fireRate > this.nextFireTime)
		{
			this.nextFireTime = Time.time - Time.deltaTime;
		}
		while (this.nextFireTime < Time.time)
		{
			this.GrenadeLauncherOneShot();
			this.nextFireTime += this.fireRate;
		}
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x0009A408 File Offset: 0x00098808
	private void GrenadeLauncherOneShot()
	{
		Vector3 eulerAngles = new Vector3(this.mNetworkCharacter.cameraRotationX, base.transform.root.eulerAngles.y, 0f);
		this.firePoint.eulerAngles = eulerAngles;
		Rigidbody rigidbody = UnityEngine.Object.Instantiate<Rigidbody>(this.projectile, this.firePoint.position, this.firePoint.rotation);
		if (base.gameObject.name != "RPG(Clone)" && base.gameObject.name != "NuclearEmitter(Clone)")
		{
			rigidbody.velocity = this.firePoint.TransformDirection(new Vector3(0f, 8f, this.initialSpeed));
		}
		else
		{
			rigidbody.velocity = this.firePoint.TransformDirection(new Vector3(0f, 0f, this.initialSpeed));
		}
		base.GetComponent<AudioSource>().clip = this.fireAudio;
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x0009A513 File Offset: 0x00098913
	private void KnifeFixedUpdate()
	{
		if (this.fire)
		{
			this.KnifeFire();
		}
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x0009A528 File Offset: 0x00098928
	private void KnifeFire()
	{
		if (Time.time - this.fireRate > this.nextFireTime)
		{
			this.nextFireTime = Time.time - Time.deltaTime;
		}
		while (this.nextFireTime < Time.time)
		{
			this.KnifeOneShot();
			this.nextFireTime += this.fireRate;
		}
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x0009A58C File Offset: 0x0009898C
	private void KnifeOneShot()
	{
		if (base.gameObject.name == "ZombieHand(Clone)" && this.mNetworkCharacter.zombieSkinIndex == 3)
		{
			if (this.DuyeEffect == null)
			{
				this.DuyeEffect = base.transform.root.Find("Player_1_sinkmesh/H/S/C/N001/Head/DuyeEffect").gameObject;
			}
			if (!this.DuyeEffect.activeSelf)
			{
				this.DuyeEffect.SetActive(true);
			}
			this.DuyeEffect.GetComponent<ParticleSystem>().Play();
		}
		base.GetComponent<AudioSource>().clip = this.fireAudio;
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x0009A640 File Offset: 0x00098A40
	private IEnumerator MuzzleFlash(float delay)
	{
		if (this.muzzleFlash)
		{
			this.muzzleFlash.enabled = true;
			yield return new WaitForSeconds(delay);
			this.muzzleFlash.enabled = false;
		}
		yield break;
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x0009A664 File Offset: 0x00098A64
	private IEnumerator MuzzleFlash_R(float delay)
	{
		if (this.muzzleFlash_R)
		{
			this.muzzleFlash_R.enabled = true;
			yield return new WaitForSeconds(delay);
			this.muzzleFlash_R.enabled = false;
		}
		yield break;
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0009A688 File Offset: 0x00098A88
	private IEnumerator MuzzleFlash_L(float delay)
	{
		if (this.muzzleFlash_L == null)
		{
			this.muzzleFlash_L = base.transform.root.Find("Player_1_sinkmesh/H/S/C/L0/L1/L2/DualPistol_L/MuzzleFlash_L").gameObject.GetComponent<Renderer>();
		}
		if (this.muzzleFlash_L)
		{
			this.muzzleFlash_L.enabled = true;
			yield return new WaitForSeconds(delay);
			this.muzzleFlash_L.enabled = false;
		}
		yield break;
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x0009A6AA File Offset: 0x00098AAA
	public void SetWeaponTransform(Vector3 p, Vector3 e, Vector3 s)
	{
		base.transform.localPosition = p;
		base.transform.localEulerAngles = e;
		base.transform.localScale = s;
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x0009A6D0 File Offset: 0x00098AD0
	public void SetWeaponUpgrade(int upgradeLv, string name)
	{
		if (upgradeLv > 0)
		{
			Forcefield[] componentsInChildren = base.transform.GetComponentsInChildren<Forcefield>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (name != "Nightmare")
				{
					componentsInChildren[i].gameObject.GetComponent<Renderer>().material = (Resources.Load("Original Resources/Weapons/Materials/WeaponUpgradeLv_" + upgradeLv.ToString()) as Material);
					componentsInChildren[i].gameObject.GetComponent<Renderer>().enabled = true;
					componentsInChildren[i].enabled = true;
				}
				else
				{
					componentsInChildren[i].gameObject.GetComponent<Renderer>().material = (Resources.Load("Original Resources/Weapons/Materials/WeaponUpgradeLv_" + upgradeLv.ToString() + "_Nightmare") as Material);
					componentsInChildren[i].gameObject.GetComponent<Renderer>().enabled = true;
					componentsInChildren[i].enabled = true;
				}
			}
			if (name == "DualPistol")
			{
				if (this.DualPistol_L == null)
				{
					this.DualPistol_L = base.transform.root.Find("Player_1_sinkmesh/H/S/C/L0/L1/L2/DualPistol_L");
				}
				Forcefield componentInChildren = this.DualPistol_L.GetComponentInChildren<Forcefield>();
				componentInChildren.gameObject.GetComponent<Renderer>().material = (Resources.Load("Original Resources/Weapons/Materials/WeaponUpgradeLv_" + upgradeLv.ToString()) as Material);
				componentInChildren.gameObject.GetComponent<Renderer>().enabled = true;
				componentInChildren.enabled = true;
			}
		}
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x0009A84D File Offset: 0x00098C4D
	private void SetBulletRemote(int upgradeLv)
	{
		this.bullet = (Resources.Load("Prefabs/Mode_Multiplayer/Projectiles/RemoteBullet_UpgradeLv" + upgradeLv.ToString()) as GameObject);
	}

	// Token: 0x040013E4 RID: 5092
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x040013E5 RID: 5093
	public Transform firePoint;

	// Token: 0x040013E6 RID: 5094
	public GameObject bullet;

	// Token: 0x040013E7 RID: 5095
	public GameObject bulletPlused;

	// Token: 0x040013E8 RID: 5096
	public bool isPulsed;

	// Token: 0x040013E9 RID: 5097
	public bool isUpgrade;

	// Token: 0x040013EA RID: 5098
	public Renderer muzzleFlash;

	// Token: 0x040013EB RID: 5099
	public Renderer muzzleFlash_R;

	// Token: 0x040013EC RID: 5100
	public Renderer muzzleFlash_L;

	// Token: 0x040013ED RID: 5101
	public Rigidbody projectile;

	// Token: 0x040013EE RID: 5102
	public AudioClip fireAudio;

	// Token: 0x040013EF RID: 5103
	public float fireRate;

	// Token: 0x040013F0 RID: 5104
	private float nextFireTime;

	// Token: 0x040013F1 RID: 5105
	private bool fire;

	// Token: 0x040013F2 RID: 5106
	public GameObject DuyeEffect;

	// Token: 0x040013F3 RID: 5107
	public bool beam;

	// Token: 0x040013F4 RID: 5108
	public Transform lightBeam;

	// Token: 0x040013F5 RID: 5109
	private bool lightBeamFire;

	// Token: 0x040013F6 RID: 5110
	public bool dualGun;

	// Token: 0x040013F7 RID: 5111
	private Transform DualPistol_L;

	// Token: 0x040013F8 RID: 5112
	public GGNetWorkPlayerAnimationControl mGGNetWorkPlayerAnimationControl;

	// Token: 0x040013F9 RID: 5113
	public int upgradeLv;

	// Token: 0x040013FA RID: 5114
	public string weaponName = string.Empty;

	// Token: 0x040013FB RID: 5115
	public WeaponSynForMultiplayer.GunType gunType;

	// Token: 0x040013FC RID: 5116
	public int fractions;

	// Token: 0x040013FD RID: 5117
	public float errorAngle;

	// Token: 0x040013FE RID: 5118
	public float initialSpeed;

	// Token: 0x040013FF RID: 5119
	private Transform weaponPart;

	// Token: 0x02000259 RID: 601
	public enum GunType
	{
		// Token: 0x04001401 RID: 5121
		MachineGun,
		// Token: 0x04001402 RID: 5122
		ShotGun,
		// Token: 0x04001403 RID: 5123
		GrenadeLauncher,
		// Token: 0x04001404 RID: 5124
		Knife
	}
}
