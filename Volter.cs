using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006C9 RID: 1737
public class Volter : MonoBehaviour
{
	// Token: 0x06003308 RID: 13064 RVA: 0x00166A0B File Offset: 0x00164E0B
	private void Start()
	{
		this.line = base.GetComponentInChildren<LineRenderer>();
		this.line.enabled = false;
		this.hitRender.enabled = false;
		this.hitSmoke.emit = false;
		this.hitDebris.emit = false;
	}

	// Token: 0x06003309 RID: 13065 RVA: 0x00166A4C File Offset: 0x00164E4C
	private void ReadyWeapon()
	{
		GameObject gameObject = GameObject.Find("WeaponControl");
		base.transform.parent = gameObject.transform;
		GameObject gameObject2 = GameObject.Find("Muzzle");
		if (gameObject2 != null)
		{
			this.muzzlePosition = gameObject2.transform.position;
			this.muzzleflash = GameObject.Find("Flash");
		}
		if (this.muzzleflash != null)
		{
			this.flash = this.muzzleflash.GetComponent<Renderer>();
			this.flash.enabled = false;
		}
		this.audioPlayer = base.GetComponent<AudioSource>();
	}

	// Token: 0x0600330A RID: 13066 RVA: 0x00166AE8 File Offset: 0x00164EE8
	private void Fire()
	{
		base.StartCoroutine("COFlash");
		if (this.audioPlayer != null && !base.GetComponent<AudioSource>().isPlaying)
		{
			base.GetComponent<AudioSource>().loop = true;
			base.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x0600330B RID: 13067 RVA: 0x00166B3C File Offset: 0x00164F3C
	private void Lightning()
	{
		Ray ray = new Ray(base.transform.position, -base.transform.up);
		this.line.SetPosition(0, this.muzzlePosition);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit, 1000f))
		{
			this.line.enabled = true;
			this.line.SetPosition(1, raycastHit.point);
			this.hitRender.enabled = true;
			this.hitPoint.transform.position = raycastHit.point;
			Quaternion rotation = Quaternion.FromToRotation(Vector3.up, raycastHit.normal);
			this.hitEffect.transform.rotation = rotation;
			this.hitEffect.transform.rotation = Quaternion.Euler((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360));
			this.hitEffect.transform.localScale = new Vector3(UnityEngine.Random.Range(this.hitEffectScaleMin, this.hitEffectScaleMax), UnityEngine.Random.Range(this.hitEffectScaleMin, this.hitEffectScaleMax), UnityEngine.Random.Range(this.hitEffectScaleMin, this.hitEffectScaleMax));
			if (raycastHit.rigidbody)
			{
				raycastHit.rigidbody.AddForceAtPosition(base.transform.forward * 10f, raycastHit.point + new Vector3(0f, 0f, 1f));
			}
		}
		else
		{
			this.line.enabled = false;
			this.hitRender.enabled = false;
		}
	}

	// Token: 0x0600330C RID: 13068 RVA: 0x00166CE4 File Offset: 0x001650E4
	private void StopLightning()
	{
		this.line.enabled = false;
		this.hitRender.enabled = false;
	}

	// Token: 0x0600330D RID: 13069 RVA: 0x00166D00 File Offset: 0x00165100
	public IEnumerator COFlash()
	{
		if (this.muzzleflash != null)
		{
			this.flash.enabled = true;
			int rotX = rotX = UnityEngine.Random.Range(0, 360);
			float flashScale = UnityEngine.Random.Range(0.6f, 1.4f);
			this.muzzleflash.transform.Rotate(Vector3.forward, (float)rotX);
			this.muzzleflash.transform.localScale = new Vector3(flashScale, flashScale, flashScale);
			yield return new WaitForSeconds(0.08f);
			this.flash.enabled = false;
			if (Input.GetButton("Fire1"))
			{
				base.StartCoroutine("COFlash");
			}
		}
		yield break;
	}

	// Token: 0x0600330E RID: 13070 RVA: 0x00166D1C File Offset: 0x0016511C
	private void Update()
	{
		if (Input.GetButton("Fire1"))
		{
			this.ReadyWeapon();
			this.Fire();
			this.Lightning();
			this.blueGlow.transform.localScale = new Vector3(UnityEngine.Random.Range(0.1f, 0.4f), UnityEngine.Random.Range(0.1f, 0.4f), UnityEngine.Random.Range(0.1f, 0.4f));
			this.hitSmoke.emit = true;
			this.hitDebris.emit = true;
		}
		else
		{
			base.GetComponent<AudioSource>().Stop();
			this.StopLightning();
			this.blueGlow.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
			this.hitSmoke.emit = false;
			this.hitDebris.emit = false;
		}
	}

	// Token: 0x04002F62 RID: 12130
	private Vector3 muzzlePosition;

	// Token: 0x04002F63 RID: 12131
	private Renderer flash;

	// Token: 0x04002F64 RID: 12132
	private AudioSource audioPlayer;

	// Token: 0x04002F65 RID: 12133
	public AudioClip sound;

	// Token: 0x04002F66 RID: 12134
	private GameObject muzzleflash;

	// Token: 0x04002F67 RID: 12135
	public GameObject hitEffect;

	// Token: 0x04002F68 RID: 12136
	public Renderer hitRender;

	// Token: 0x04002F69 RID: 12137
	public LineRenderer line;

	// Token: 0x04002F6A RID: 12138
	public Transform hitPoint;

	// Token: 0x04002F6B RID: 12139
	public Transform blueGlow;

	// Token: 0x04002F6C RID: 12140
	public ParticleEmitter hitSmoke;

	// Token: 0x04002F6D RID: 12141
	public ParticleEmitter hitDebris;

	// Token: 0x04002F6E RID: 12142
	public float hitEffectScaleMin;

	// Token: 0x04002F6F RID: 12143
	public float hitEffectScaleMax;
}
