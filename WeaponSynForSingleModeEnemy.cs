using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200027C RID: 636
public class WeaponSynForSingleModeEnemy : MonoBehaviour
{
	// Token: 0x06001212 RID: 4626 RVA: 0x000A3800 File Offset: 0x000A1C00
	private void Awake()
	{
		if (this.muzzleFlash)
		{
			this.muzzleFlash.GetComponent<Renderer>().enabled = false;
		}
		base.GetComponent<AudioSource>().playOnAwake = false;
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x000A382F File Offset: 0x000A1C2F
	private void Start()
	{
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x000A3834 File Offset: 0x000A1C34
	private void Update()
	{
		if (Time.timeScale < 0.01f)
		{
			return;
		}
		if (this.gunType == WeaponSynForSingleModeEnemy.GunType.MachineGun)
		{
			this.MachineGunFixedUpdate();
		}
		else if (this.gunType == WeaponSynForSingleModeEnemy.GunType.ShotGun)
		{
			this.ShotGunFixedUpdate();
		}
		else if (this.gunType == WeaponSynForSingleModeEnemy.GunType.GrenadeLauncher)
		{
			this.GrenadeLauncherFixedUpdate();
		}
		else if (this.gunType == WeaponSynForSingleModeEnemy.GunType.Knife)
		{
			this.KnifeFixedUpdate();
		}
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x000A38A7 File Offset: 0x000A1CA7
	private void MachineGunFixedUpdate()
	{
		if (this.fire)
		{
			this.MachineGunFire();
		}
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x000A38BC File Offset: 0x000A1CBC
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

	// Token: 0x06001217 RID: 4631 RVA: 0x000A3920 File Offset: 0x000A1D20
	private void MachineGunShot()
	{
		Quaternion rotation = this.firePoint.rotation;
		this.firePoint.rotation = Quaternion.Euler(UnityEngine.Random.insideUnitSphere * this.errorAngle) * this.firePoint.rotation;
		UnityEngine.Object.Instantiate<GameObject>(this.bullet, this.firePoint.position, this.firePoint.rotation);
		this.firePoint.rotation = rotation;
		base.GetComponent<AudioSource>().clip = this.fireAudio;
		base.GetComponent<AudioSource>().Play();
		base.StartCoroutine(this.MuzzleFlash(0.04f));
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x000A39C5 File Offset: 0x000A1DC5
	private void ShotGunFixedUpdate()
	{
		if (this.fire)
		{
			this.ShotGunFire();
		}
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x000A39D8 File Offset: 0x000A1DD8
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

	// Token: 0x0600121A RID: 4634 RVA: 0x000A3A3C File Offset: 0x000A1E3C
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

	// Token: 0x0600121B RID: 4635 RVA: 0x000A3AF7 File Offset: 0x000A1EF7
	private void GrenadeLauncherFixedUpdate()
	{
		if (this.fire)
		{
			this.GrenadeLauncherFire();
		}
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x000A3B0C File Offset: 0x000A1F0C
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

	// Token: 0x0600121D RID: 4637 RVA: 0x000A3B70 File Offset: 0x000A1F70
	private void GrenadeLauncherOneShot()
	{
		Rigidbody rigidbody = UnityEngine.Object.Instantiate<Rigidbody>(this.projectile, this.firePoint.position, this.firePoint.rotation);
		rigidbody.AddForce(rigidbody.transform.forward * this.initialSpeed);
		base.GetComponent<AudioSource>().clip = this.fireAudio;
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x000A3BD7 File Offset: 0x000A1FD7
	private void KnifeFixedUpdate()
	{
		if (this.fire)
		{
			this.KnifeFire();
		}
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x000A3BEC File Offset: 0x000A1FEC
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

	// Token: 0x06001220 RID: 4640 RVA: 0x000A3C4F File Offset: 0x000A204F
	private void KnifeOneShot()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.bullet, this.firePoint.position, this.firePoint.rotation);
		base.GetComponent<AudioSource>().clip = this.fireAudio;
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x000A3C90 File Offset: 0x000A2090
	private IEnumerator MuzzleFlash(float delay)
	{
		if (this.muzzleFlash)
		{
			this.muzzleFlash.GetComponent<Renderer>().enabled = true;
			yield return new WaitForSeconds(delay);
			this.muzzleFlash.GetComponent<Renderer>().enabled = false;
		}
		yield break;
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x000A3CB2 File Offset: 0x000A20B2
	private void FireInSingleMode()
	{
		this.fire = true;
	}

	// Token: 0x06001223 RID: 4643 RVA: 0x000A3CBB File Offset: 0x000A20BB
	private void StopFireInSingleMode()
	{
		this.fire = false;
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x000A3CC4 File Offset: 0x000A20C4
	private void ChangeWeaponSpeed(int AttackSpeed)
	{
		this.fireRate *= (float)AttackSpeed;
	}

	// Token: 0x040014ED RID: 5357
	public Transform firePoint;

	// Token: 0x040014EE RID: 5358
	public GameObject bullet;

	// Token: 0x040014EF RID: 5359
	public Renderer muzzleFlash;

	// Token: 0x040014F0 RID: 5360
	public Rigidbody projectile;

	// Token: 0x040014F1 RID: 5361
	public AudioClip fireAudio;

	// Token: 0x040014F2 RID: 5362
	public float fireRate;

	// Token: 0x040014F3 RID: 5363
	private float nextFireTime;

	// Token: 0x040014F4 RID: 5364
	private bool fire;

	// Token: 0x040014F5 RID: 5365
	public WeaponSynForSingleModeEnemy.GunType gunType;

	// Token: 0x040014F6 RID: 5366
	public int fractions;

	// Token: 0x040014F7 RID: 5367
	public float errorAngle;

	// Token: 0x040014F8 RID: 5368
	public float initialSpeed;

	// Token: 0x0200027D RID: 637
	public enum GunType
	{
		// Token: 0x040014FA RID: 5370
		MachineGun,
		// Token: 0x040014FB RID: 5371
		ShotGun,
		// Token: 0x040014FC RID: 5372
		GrenadeLauncher,
		// Token: 0x040014FD RID: 5373
		Knife
	}
}
