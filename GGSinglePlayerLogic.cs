using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200027A RID: 634
public class GGSinglePlayerLogic : MonoBehaviour
{
	// Token: 0x060011FB RID: 4603 RVA: 0x000A3158 File Offset: 0x000A1558
	private void Awake()
	{
		this.mNetworkCharacter = base.GetComponent<GGNetworkCharacter>();
		this.GeneratePositions = GameObject.Find("SpawnPosition");
		this.mNetworkCharacter.mBlood = 100;
		this.audioEnemyDie = GameObject.Find("EnemyDieAudio").GetComponent<AudioSource>();
		this.audioBulletHit = GameObject.Find("BulletHitAudio").GetComponent<AudioSource>();
		this.PlayerBloodShow = GameObject.Find("PlayerBloodEffect");
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x000A31C8 File Offset: 0x000A15C8
	private void OnDestroy()
	{
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x000A31CC File Offset: 0x000A15CC
	private void Start()
	{
		this.Respawn();
		if (PlayerPrefs.GetInt("IsSoundEffectEnabled", 1) == 0)
		{
			base.transform.Find("AudioListenerControl").localPosition = new Vector3(0f, 10000f, 0f);
		}
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x000A3218 File Offset: 0x000A1618
	private void Update()
	{
		this.PlayerDamageWhenInFire();
		this.PlayerDamageWhenInTieding();
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x000A3226 File Offset: 0x000A1626
	public void playPlayerDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x000A3233 File Offset: 0x000A1633
	public void playBulletHitAudio()
	{
		this.audioBulletHit.Play();
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x000A3240 File Offset: 0x000A1640
	private IEnumerator waitForGeneratePlayer(int seconds)
	{
		yield return new WaitForSeconds((float)seconds);
		this.mNetworkCharacter.mBlood = 100;
		this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Idle;
		base.GetComponent<CharacterController>().enabled = true;
		base.transform.Find("LookObject/Main Camera/Weapon Camera").GetComponent<Camera>().cullingMask = 256;
		this.Respawn();
		yield break;
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x000A3262 File Offset: 0x000A1662
	public void Respawn()
	{
		base.transform.position = this.GeneratePositions.transform.position + new Vector3(0f, 1f, 0f);
	}

	// Token: 0x06001203 RID: 4611 RVA: 0x000A3298 File Offset: 0x000A1698
	public void SinglePlayerDamage(int damage)
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			if (GGSingleEnemyManager.mInstance != null)
			{
				float num = 1f;
				if (GGSingleEnemyManager.mInstance.Difficulty == 1)
				{
					num = 1f;
				}
				else if (GGSingleEnemyManager.mInstance.Difficulty == 2)
				{
					num = 1.3f;
				}
				else if (GGSingleEnemyManager.mInstance.Difficulty == 3)
				{
					num = 1.6f;
				}
				this.mNetworkCharacter.mBlood -= (int)((float)damage * 0.1f * num);
			}
			if (this.mNetworkCharacter.mBlood <= 0 && this.mNetworkCharacter.mBlood + damage > 0)
			{
				this.playPlayerDieAudio();
				this.mNetworkCharacter.mBlood = 0;
				this.mNetworkCharacter.mCharacterWalkState = GGCharacterWalkState.Dead;
				base.GetComponent<CharacterController>().enabled = false;
				base.transform.Find("LookObject/Main Camera/Weapon Camera").GetComponent<Camera>().cullingMask = 0;
			}
			this.playBulletHitAudio();
			base.StartCoroutine(this.DisplayPlayerBlood());
		}
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x000A33B4 File Offset: 0x000A17B4
	private IEnumerator DisplayPlayerBlood()
	{
		this.PlayerBloodShow.SetActive(true);
		yield return new WaitForSeconds(0.12f);
		this.PlayerBloodShow.SetActive(false);
		yield break;
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x000A33CF File Offset: 0x000A17CF
	public void addKilledNum(int i)
	{
		GrowthManagerKit.AddCharacterExp(i);
		GrowthManagerKit.AddCoins(i);
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x000A33DD File Offset: 0x000A17DD
	public void OnGameOver()
	{
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x000A33DF File Offset: 0x000A17DF
	private void OnDisable()
	{
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x000A33E1 File Offset: 0x000A17E1
	private void OnMediKit()
	{
		if (this.mNetworkCharacter.mCharacterWalkState != GGCharacterWalkState.Dead)
		{
			this.mNetworkCharacter.mBlood = 100;
		}
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x000A3404 File Offset: 0x000A1804
	private void OnSoundSetting()
	{
		if (PlayerPrefs.GetInt("IsSoundEffectEnabled", 1) == 0)
		{
			base.transform.Find("AudioListenerControl").localPosition = new Vector3(0f, 10000f, 0f);
		}
		else if (PlayerPrefs.GetInt("IsSoundEffectEnabled", 1) == 1)
		{
			base.transform.Find("AudioListenerControl").localPosition = new Vector3(0f, 2f, 0f);
		}
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x000A348C File Offset: 0x000A188C
	private void PlayerDamageWhenInFire()
	{
		if (this.PlayerInFire)
		{
			this.InFireTime += Time.deltaTime;
			if ((double)this.InFireTime > 0.5)
			{
				this.mNetworkCharacter.mBlood -= 8;
				this.InFireTime = 0f;
			}
		}
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x000A34EC File Offset: 0x000A18EC
	private void PlayerDamageWhenInTieding()
	{
		if (this.PlayerInTieding)
		{
			this.InTiedingTime += Time.deltaTime;
			if ((double)this.InFireTime > 0.2)
			{
				this.mNetworkCharacter.mBlood--;
				this.InTiedingTime = 0f;
			}
		}
	}

	// Token: 0x040014E0 RID: 5344
	public bool PlayerInFire;

	// Token: 0x040014E1 RID: 5345
	public float InFireTime;

	// Token: 0x040014E2 RID: 5346
	public bool PlayerInTieding;

	// Token: 0x040014E3 RID: 5347
	public float InTiedingTime;

	// Token: 0x040014E4 RID: 5348
	private AudioSource audioEnemyDie;

	// Token: 0x040014E5 RID: 5349
	private AudioSource audioBulletHit;

	// Token: 0x040014E6 RID: 5350
	private GameObject GeneratePositions;

	// Token: 0x040014E7 RID: 5351
	private GameObject PlayerBloodShow;

	// Token: 0x040014E8 RID: 5352
	private GGNetworkCharacter mNetworkCharacter;
}
