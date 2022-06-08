using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003ED RID: 1005
public class FPSPlayerControl : MonoBehaviour
{
	// Token: 0x06001E2F RID: 7727 RVA: 0x000E78FB File Offset: 0x000E5CFB
	private void Awake()
	{
		this.anim = base.GetComponentInChildren<Animator>();
		this.audioSource = base.GetComponent<AudioSource>();
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x000E7918 File Offset: 0x000E5D18
	private void Update()
	{
		if (ETCInput.GetButton("Fire") && !this.inFire && this.armoCount > 0 && !this.inReload)
		{
			this.inFire = true;
			this.anim.SetBool("Shoot", true);
			base.InvokeRepeating("GunFire", 0.12f, 0.12f);
			this.GunFire();
		}
		if (ETCInput.GetButtonDown("Fire") && this.armoCount == 0 && !this.inReload)
		{
			this.audioSource.PlayOneShot(this.needReload, 1f);
		}
		if (ETCInput.GetButtonUp("Fire"))
		{
			this.anim.SetBool("Shoot", false);
			this.inFire = false;
			base.CancelInvoke();
		}
		if (ETCInput.GetButtonDown("Reload"))
		{
			this.inReload = true;
			this.audioSource.PlayOneShot(this.reload, 1f);
			this.anim.SetBool("Reload", true);
			base.StartCoroutine(this.Reload());
		}
		if (ETCInput.GetButtonDown("Back"))
		{
			base.transform.Rotate(Vector3.up * 180f);
		}
		this.armoText.text = this.armoCount.ToString();
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x000E7A80 File Offset: 0x000E5E80
	public void MoveStart()
	{
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x000E7A82 File Offset: 0x000E5E82
	public void MoveStop()
	{
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x000E7A84 File Offset: 0x000E5E84
	public void GunFire()
	{
		if (this.armoCount > 0)
		{
			this.muzzleEffect.transform.Rotate(Vector3.forward * UnityEngine.Random.Range(0f, 360f));
			this.muzzleEffect.transform.localScale = new Vector3(UnityEngine.Random.Range(0.1f, 0.2f), UnityEngine.Random.Range(0.1f, 0.2f), 1f);
			this.muzzleEffect.SetActive(true);
			base.StartCoroutine(this.Flash());
			this.audioSource.PlayOneShot(this.gunSound, 1f);
			Vector3 vector = new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f);
			vector += new Vector3((float)UnityEngine.Random.Range(-10, 10), (float)UnityEngine.Random.Range(-10, 10), 0f);
			Ray ray = Camera.main.ScreenPointToRay(vector);
			RaycastHit[] array = Physics.RaycastAll(ray);
			if (array.Length > 0)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.impactEffect, array[0].point - array[0].normal * -0.2f, Quaternion.identity);
			}
		}
		else
		{
			this.anim.SetBool("Shoot", false);
			this.muzzleEffect.SetActive(false);
			this.inFire = false;
		}
		this.armoCount--;
		if (this.armoCount < 0)
		{
			this.armoCount = 0;
		}
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x000E7C0C File Offset: 0x000E600C
	public void TouchPadSwipe(bool value)
	{
		ETCInput.SetControlSwipeIn("FreeLookTouchPad", value);
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x000E7C1C File Offset: 0x000E601C
	private IEnumerator Flash()
	{
		yield return new WaitForSeconds(0.08f);
		this.muzzleEffect.SetActive(false);
		yield break;
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x000E7C38 File Offset: 0x000E6038
	private IEnumerator Reload()
	{
		yield return new WaitForSeconds(0.5f);
		this.armoCount = 30;
		this.inReload = false;
		this.anim.SetBool("Reload", false);
		yield break;
	}

	// Token: 0x04001F50 RID: 8016
	public AudioClip gunSound;

	// Token: 0x04001F51 RID: 8017
	public AudioClip reload;

	// Token: 0x04001F52 RID: 8018
	public AudioClip needReload;

	// Token: 0x04001F53 RID: 8019
	public ParticleSystem shellParticle;

	// Token: 0x04001F54 RID: 8020
	public GameObject muzzleEffect;

	// Token: 0x04001F55 RID: 8021
	public GameObject impactEffect;

	// Token: 0x04001F56 RID: 8022
	public Text armoText;

	// Token: 0x04001F57 RID: 8023
	private bool inFire;

	// Token: 0x04001F58 RID: 8024
	private bool inReload;

	// Token: 0x04001F59 RID: 8025
	private Animator anim;

	// Token: 0x04001F5A RID: 8026
	private int armoCount = 30;

	// Token: 0x04001F5B RID: 8027
	private AudioSource audioSource;
}
