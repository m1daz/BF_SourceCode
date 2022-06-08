using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000232 RID: 562
public class GGNetworkAIControl_DragonBaby : MonoBehaviour
{
	// Token: 0x06000FD2 RID: 4050 RVA: 0x000881D8 File Offset: 0x000865D8
	private void Start()
	{
		this.mNavMeshAgent = base.GetComponent<NavMeshAgent>();
		this.mGGNetWorkAISeeker = base.GetComponent<GGNetWorkAISeeker>();
		this.mGGNetWorkAIProperty = base.GetComponent<GGNetWorkAIProperty>();
		this.mPhotonView = base.GetComponent<PhotonView>();
		this.audioEnemyDie = base.GetComponent<AudioSource>();
		this.SetTarget();
		GGNetworkKit.mInstance.AIReceiveDamage += this.Event_Damage_AI;
		this.DifficultyInit();
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
		}
		base.GetComponent<AudioSource>().clip = this.clips[0];
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x06000FD3 RID: 4051 RVA: 0x00088270 File Offset: 0x00086670
	private void Update()
	{
		if (this.preState != this.bDead)
		{
			this.preState = this.bDead;
			if (this.bDead)
			{
				this.playDieEffect();
			}
		}
		if (this.bDead)
		{
			return;
		}
		if (this.mGGNetWorkAIProperty.mSkillIndex != this.mPreSkillIndex)
		{
			this.mPreSkillIndex = this.mGGNetWorkAIProperty.mSkillIndex;
			this.StateChange(this.mGGNetWorkAIProperty.mSkillIndex);
		}
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			this.AILogic();
			this.CheckTarget();
			if (this.mNavMeshAgent.speed == 0f && this.mGGNetWorkAISeeker.target != null)
			{
				Quaternion b = Quaternion.LookRotation(this.mGGNetWorkAISeeker.target.transform.position - base.transform.position, Vector3.up);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, 4f * Time.deltaTime);
			}
		}
		if (this.mGGNetWorkAIProperty.mSkillIndex == 1)
		{
			this.IsCanSkill();
		}
	}

	// Token: 0x06000FD4 RID: 4052 RVA: 0x000883A4 File Offset: 0x000867A4
	public void DifficultyInit()
	{
		int difficultySet = GGNetWorkAIDifficultyControl.mInstance.difficultySet;
		int maxPlayerSet = GGNetWorkAIDifficultyControl.mInstance.maxPlayerSet;
		if (difficultySet == 1)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(0.7f * (float)this.mGGNetWorkAIProperty.mBlood);
		}
		else if (difficultySet == 2)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(1f * (float)this.mGGNetWorkAIProperty.mBlood);
		}
		int count = GGNetworkKit.mInstance.GetPlayerGameObjectList().Count;
		if (count == 1)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(0.3f * (float)this.mGGNetWorkAIProperty.mBlood);
		}
		else if (count == 2)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(0.5f * (float)this.mGGNetWorkAIProperty.mBlood);
		}
		else if (count == 3)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(0.8f * (float)this.mGGNetWorkAIProperty.mBlood);
		}
		else if (count == 4)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(1f * (float)this.mGGNetWorkAIProperty.mBlood);
		}
	}

	// Token: 0x06000FD5 RID: 4053 RVA: 0x000884CC File Offset: 0x000868CC
	private void StateChange(int SkillIndex)
	{
		if (SkillIndex != 0)
		{
			if (SkillIndex == 1)
			{
				this.mNavMeshAgent.speed = 0f;
			}
		}
		else
		{
			this.mNavMeshAgent.speed = 3.5f;
		}
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x0008851C File Offset: 0x0008691C
	private void AILogic()
	{
		if (!this.SkillProcess)
		{
			this.logicTime += Time.deltaTime;
			if (this.logicTime > 1f)
			{
				if (this.mGGNetWorkAISeeker.target != null)
				{
					if (Vector3.Distance(base.transform.position, this.mGGNetWorkAISeeker.target.transform.position) > 5f)
					{
						this.mGGNetWorkAIProperty.mSkillIndex = 0;
					}
					else
					{
						this.mGGNetWorkAIProperty.mSkillIndex = 1;
					}
				}
				this.logicTime = 0f;
			}
		}
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x000885C4 File Offset: 0x000869C4
	private void IsCanSkill()
	{
		if (Time.time - this.AttackCD > this.AttackTime)
		{
			this.AttackTime = Time.time - Time.deltaTime;
		}
		while (this.AttackTime < Time.time)
		{
			this.Attack();
			this.AttackTime += this.AttackCD;
		}
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x00088628 File Offset: 0x00086A28
	private void Attack()
	{
		for (int i = 0; i < this.QiangFirepoint.Length; i++)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.Bullet, this.QiangFirepoint[i].position, this.QiangFirepoint[i].rotation);
			base.GetComponent<AudioSource>().clip = this.clips[1];
			base.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x00088694 File Offset: 0x00086A94
	private void SetTarget()
	{
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			this.RefreshTarget();
			if (this.PlayersList.Count > 0)
			{
				this.mGGNetWorkAISeeker.target = this.PlayersList[UnityEngine.Random.Range(0, this.PlayersList.Count)];
				this.targetNetworkCharacter = this.mGGNetWorkAISeeker.target.GetComponent<GGNetworkCharacter>();
			}
			else
			{
				this.mGGNetWorkAISeeker.target = null;
			}
		}
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x00088718 File Offset: 0x00086B18
	private void CheckTarget()
	{
		this.CheckTargetTime += Time.deltaTime;
		if (this.CheckTargetTime > 0.5f)
		{
			if (GGNetworkKit.mInstance.IsMasterClient())
			{
				if (this.mGGNetWorkAISeeker.target == null)
				{
					this.SetTarget();
				}
				else if (this.targetNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Dead)
				{
					this.SetTarget();
				}
			}
			this.CheckTargetTime = 0f;
		}
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x0008879C File Offset: 0x00086B9C
	public void RefreshTarget()
	{
		this.PlayersList.Clear();
		this.Players = GGNetworkKit.mInstance.GetPlayerGameObjectList();
		foreach (KeyValuePair<int, GameObject> keyValuePair in this.Players)
		{
			GameObject value = keyValuePair.Value;
			GGNetworkCharacter component = value.GetComponent<GGNetworkCharacter>();
			if (component.mCharacterWalkState != GGCharacterWalkState.Dead)
			{
				this.PlayersList.Add(value);
			}
		}
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x00088834 File Offset: 0x00086C34
	public void AIDamaged(GGDamageEventArgs mdamageEventArgs)
	{
		mdamageEventArgs.id = this.mPhotonView.viewID;
		GGNetworkKit.mInstance.DamageToAI(mdamageEventArgs, this.mPhotonView);
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x00088858 File Offset: 0x00086C58
	public void Event_Damage_AI(GGDamageEventArgs damageEventArgs)
	{
		if (damageEventArgs.id == this.mPhotonView.viewID)
		{
			int damage = (int)damageEventArgs.damage;
			this.decreaseBlood(damage);
		}
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x0008888C File Offset: 0x00086C8C
	public void decreaseBlood(int bulletDamage)
	{
		if (this.mGGNetWorkAIProperty.mBlood > 0)
		{
			this.mGGNetWorkAIProperty.mBlood -= bulletDamage;
			this.mGGNetWorkAIProperty.mBlood = Mathf.Max(this.mGGNetWorkAIProperty.mBlood, 0);
			if (this.mGGNetWorkAIProperty.mBlood <= 0)
			{
				this.bDead = true;
				this.playEnemyDieAudio();
				base.StartCoroutine(this.waitForSecondsToDestory(0.5f));
			}
		}
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x00088909 File Offset: 0x00086D09
	public void playEnemyDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x00088916 File Offset: 0x00086D16
	public void playDieEffect()
	{
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x00088918 File Offset: 0x00086D18
	public void playBulletHitAudio()
	{
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x0008891C File Offset: 0x00086D1C
	private IEnumerator waitForSecondsToDestory(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x0008893E File Offset: 0x00086D3E
	private void onDestroy()
	{
		GGNetworkKit.mInstance.AIReceiveDamage -= this.Event_Damage_AI;
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00088958 File Offset: 0x00086D58
	private IEnumerator DestroySlef()
	{
		yield return new WaitForSeconds(35f);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x040011BB RID: 4539
	private NavMeshAgent mNavMeshAgent;

	// Token: 0x040011BC RID: 4540
	private GGNetWorkAISeeker mGGNetWorkAISeeker;

	// Token: 0x040011BD RID: 4541
	private GGNetWorkAIProperty mGGNetWorkAIProperty;

	// Token: 0x040011BE RID: 4542
	private GGNetworkCharacter targetNetworkCharacter;

	// Token: 0x040011BF RID: 4543
	private PhotonView mPhotonView;

	// Token: 0x040011C0 RID: 4544
	public bool bDead;

	// Token: 0x040011C1 RID: 4545
	public bool preState;

	// Token: 0x040011C2 RID: 4546
	private AudioSource audioEnemyDie;

	// Token: 0x040011C3 RID: 4547
	public int mPreSkillIndex;

	// Token: 0x040011C4 RID: 4548
	private float logicTime;

	// Token: 0x040011C5 RID: 4549
	public Dictionary<int, GameObject> Players;

	// Token: 0x040011C6 RID: 4550
	public List<GameObject> PlayersList = new List<GameObject>();

	// Token: 0x040011C7 RID: 4551
	private float checkCountTime;

	// Token: 0x040011C8 RID: 4552
	private int QiangFireCount = 3;

	// Token: 0x040011C9 RID: 4553
	public Transform[] QiangFirepoint;

	// Token: 0x040011CA RID: 4554
	public Transform Bullet;

	// Token: 0x040011CB RID: 4555
	public AudioClip[] clips;

	// Token: 0x040011CC RID: 4556
	private bool SkillProcess;

	// Token: 0x040011CD RID: 4557
	private bool NeedRotateTo = true;

	// Token: 0x040011CE RID: 4558
	public float AttackCD = 2f;

	// Token: 0x040011CF RID: 4559
	public float AttackTime;

	// Token: 0x040011D0 RID: 4560
	private float CheckTargetTime;

	// Token: 0x040011D1 RID: 4561
	public Animator mAnimator;
}
