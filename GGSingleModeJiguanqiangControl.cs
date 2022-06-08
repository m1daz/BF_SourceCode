using System;
using UnityEngine;

// Token: 0x02000271 RID: 625
public class GGSingleModeJiguanqiangControl : MonoBehaviour
{
	// Token: 0x060011C2 RID: 4546 RVA: 0x000A20B4 File Offset: 0x000A04B4
	private void Start()
	{
		switch (PlayerPrefs.GetInt("SingleModeChapterOneDifficulty", 1))
		{
		case 1:
			this.jiguanqiangDamage = 22f;
			this.fireRate = 0.15f;
			this.attackTime = UnityEngine.Random.Range(3f, 4f);
			this.attackTimeDelay = UnityEngine.Random.Range(1f, 2f);
			break;
		case 2:
			this.jiguanqiangDamage = 20f;
			this.fireRate = 0.11f;
			this.attackTime = UnityEngine.Random.Range(4.5f, 6f);
			this.attackTimeDelay = UnityEngine.Random.Range(0.5f, 1f);
			break;
		case 3:
			this.jiguanqiangDamage = 18f;
			this.fireRate = 0.09f;
			this.attackTime = UnityEngine.Random.Range(5.5f, 6f);
			this.attackTimeDelay = UnityEngine.Random.Range(0f, 0.5f);
			break;
		}
		base.transform.localEulerAngles += new Vector3(0f, UnityEngine.Random.Range(-8f, 20f), UnityEngine.Random.Range(-28f, 28f));
		this.muzzleFlash.active = false;
		this.pointLight.enabled = false;
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x000A2210 File Offset: 0x000A0610
	private void Update()
	{
		if (!this.jiguanqiangStart)
		{
			return;
		}
		this.attackStartTime += Time.deltaTime;
		if (this.attackStartTime > this.attackTimeDelay && this.attackStartTime <= this.attackTimeDelay + this.attackTime)
		{
			this.fire = true;
		}
		if (this.attackStartTime > this.attackTimeDelay + this.attackTime)
		{
			this.fire = false;
			this.attackStartTime = 0f;
		}
		if (this.fire)
		{
			this.Jiguanqiangfire();
		}
		if (base.transform.localEulerAngles.z > 135f)
		{
			this.turnLeft = true;
			this.turnRight = false;
		}
		if (base.transform.localEulerAngles.z < 55f)
		{
			this.turnLeft = false;
			this.turnRight = true;
		}
		if (base.transform.localEulerAngles.y > 30f)
		{
			this.turnDown = true;
			this.turnUp = false;
		}
		if (base.transform.localEulerAngles.y < 1f || base.transform.localEulerAngles.y > 355f)
		{
			this.turnDown = false;
			this.turnUp = true;
		}
		if (this.turnLeft)
		{
			base.transform.localEulerAngles -= new Vector3(0f, 0f, 0.2f);
		}
		if (this.turnRight)
		{
			base.transform.localEulerAngles += new Vector3(0f, 0f, 0.2f);
		}
		if (this.turnDown)
		{
			base.transform.localEulerAngles -= new Vector3(0f, 0.3f, 0f);
		}
		if (this.turnUp)
		{
			base.transform.localEulerAngles += new Vector3(0f, 0.3f, 0f);
		}
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x000A2444 File Offset: 0x000A0844
	private void Jiguanqiangfire()
	{
		if (Time.time - this.fireRate > this.nextFireTime)
		{
			this.nextFireTime = Time.time - Time.deltaTime;
		}
		while (this.nextFireTime < Time.time)
		{
			this.JiguanqiangOneShot();
			this.nextFireTime += this.fireRate;
		}
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x000A24A8 File Offset: 0x000A08A8
	private void JiguanqiangOneShot()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.jiguanqiangbullet, this.firePoint.position, this.firePoint.rotation);
		gameObject.GetComponent<GGBullet>().bulletDamage = this.jiguanqiangDamage;
		this.jiguanqiangMuzzleFlash();
		base.GetComponent<AudioSource>().clip = this.fireSound;
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x000A250C File Offset: 0x000A090C
	private void jiguanqiangMuzzleFlash()
	{
		if (this.muzzleFlash)
		{
			this.muzzleFlash.active = true;
		}
		if (this.pointLight)
		{
			this.pointLight.enabled = true;
		}
		if (this.muzzleFlash)
		{
			this.muzzleFlash.active = false;
		}
		if (this.pointLight)
		{
			this.pointLight.enabled = false;
		}
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x000A2589 File Offset: 0x000A0989
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			this.jiguanqiangStart = true;
		}
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x000A25AC File Offset: 0x000A09AC
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			this.jiguanqiangStart = false;
		}
	}

	// Token: 0x040014A1 RID: 5281
	public Transform firePoint;

	// Token: 0x040014A2 RID: 5282
	public GameObject jiguanqiangbullet;

	// Token: 0x040014A3 RID: 5283
	public GameObject muzzleFlash;

	// Token: 0x040014A4 RID: 5284
	public Light pointLight;

	// Token: 0x040014A5 RID: 5285
	public AudioClip fireSound;

	// Token: 0x040014A6 RID: 5286
	private float fireRate;

	// Token: 0x040014A7 RID: 5287
	private float jiguanqiangDamage;

	// Token: 0x040014A8 RID: 5288
	private float attackTime;

	// Token: 0x040014A9 RID: 5289
	private float attackTimeDelay;

	// Token: 0x040014AA RID: 5290
	private float attackStartTime;

	// Token: 0x040014AB RID: 5291
	private bool fire;

	// Token: 0x040014AC RID: 5292
	private float nextFireTime = -1f;

	// Token: 0x040014AD RID: 5293
	private bool turnLeft = true;

	// Token: 0x040014AE RID: 5294
	private bool turnRight;

	// Token: 0x040014AF RID: 5295
	private bool turnDown = true;

	// Token: 0x040014B0 RID: 5296
	private bool turnUp;

	// Token: 0x040014B1 RID: 5297
	private bool jiguanqiangStart;
}
