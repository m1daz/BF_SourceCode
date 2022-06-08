using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000235 RID: 565
public class GGNetworkAIControl_Soldier1_1 : MonoBehaviour
{
	// Token: 0x06000FFE RID: 4094 RVA: 0x000894E4 File Offset: 0x000878E4
	private void Start()
	{
		this.mNavMeshAgent = base.GetComponent<NavMeshAgent>();
		this.mGGNetWorkAISeeker = base.GetComponent<GGNetWorkAISeeker>();
		this.mGGNetWorkAIProperty = base.GetComponent<GGNetWorkAIProperty>();
		this.mPhotonView = base.GetComponent<PhotonView>();
		this.audioEnemyDie = base.GetComponent<AudioSource>();
		this.SetTarget();
		GGNetworkKit.mInstance.AIReceiveDamage += this.Event_Damage_AI;
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			base.StartCoroutine(this.DestroySlef());
		}
		this.DifficultyInit();
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x0008956C File Offset: 0x0008796C
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
		this.wheel.localRotation *= Quaternion.Euler(new Vector3(15f, 0f, 0f));
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x000896D0 File Offset: 0x00087AD0
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

	// Token: 0x06001001 RID: 4097 RVA: 0x000897F8 File Offset: 0x00087BF8
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

	// Token: 0x06001002 RID: 4098 RVA: 0x00089848 File Offset: 0x00087C48
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

	// Token: 0x06001003 RID: 4099 RVA: 0x000898F0 File Offset: 0x00087CF0
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

	// Token: 0x06001004 RID: 4100 RVA: 0x00089954 File Offset: 0x00087D54
	private void Attack()
	{
		for (int i = 0; i < this.QiangFirepoint.Length; i++)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.Bullet, this.QiangFirepoint[i].position, this.QiangFirepoint[i].rotation);
		}
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x000899A0 File Offset: 0x00087DA0
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

	// Token: 0x06001006 RID: 4102 RVA: 0x00089A24 File Offset: 0x00087E24
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

	// Token: 0x06001007 RID: 4103 RVA: 0x00089AA8 File Offset: 0x00087EA8
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

	// Token: 0x06001008 RID: 4104 RVA: 0x00089B40 File Offset: 0x00087F40
	public void AIDamaged(GGDamageEventArgs mdamageEventArgs)
	{
		mdamageEventArgs.id = this.mPhotonView.viewID;
		GGNetworkKit.mInstance.DamageToAI(mdamageEventArgs, this.mPhotonView);
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x00089B64 File Offset: 0x00087F64
	public void Event_Damage_AI(GGDamageEventArgs damageEventArgs)
	{
		if (damageEventArgs.id == this.mPhotonView.viewID)
		{
			int damage = (int)damageEventArgs.damage;
			this.decreaseBlood(damage);
		}
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x00089B98 File Offset: 0x00087F98
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

	// Token: 0x0600100B RID: 4107 RVA: 0x00089C15 File Offset: 0x00088015
	public void playEnemyDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x00089C22 File Offset: 0x00088022
	public void playDieEffect()
	{
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x00089C24 File Offset: 0x00088024
	public void playBulletHitAudio()
	{
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x00089C28 File Offset: 0x00088028
	private IEnumerator waitForSecondsToDestory(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x00089C4A File Offset: 0x0008804A
	private void onDestroy()
	{
		GGNetworkKit.mInstance.AIReceiveDamage -= this.Event_Damage_AI;
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x00089C64 File Offset: 0x00088064
	private IEnumerator DestroySlef()
	{
		yield return new WaitForSeconds(50f);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x040011F2 RID: 4594
	private NavMeshAgent mNavMeshAgent;

	// Token: 0x040011F3 RID: 4595
	private GGNetWorkAISeeker mGGNetWorkAISeeker;

	// Token: 0x040011F4 RID: 4596
	private GGNetWorkAIProperty mGGNetWorkAIProperty;

	// Token: 0x040011F5 RID: 4597
	private GGNetworkCharacter targetNetworkCharacter;

	// Token: 0x040011F6 RID: 4598
	private PhotonView mPhotonView;

	// Token: 0x040011F7 RID: 4599
	public bool bDead;

	// Token: 0x040011F8 RID: 4600
	public bool preState;

	// Token: 0x040011F9 RID: 4601
	private AudioSource audioEnemyDie;

	// Token: 0x040011FA RID: 4602
	public int mPreSkillIndex;

	// Token: 0x040011FB RID: 4603
	private float logicTime;

	// Token: 0x040011FC RID: 4604
	public Dictionary<int, GameObject> Players;

	// Token: 0x040011FD RID: 4605
	public List<GameObject> PlayersList = new List<GameObject>();

	// Token: 0x040011FE RID: 4606
	private float checkCountTime;

	// Token: 0x040011FF RID: 4607
	private int QiangFireCount = 3;

	// Token: 0x04001200 RID: 4608
	public Transform[] QiangFirepoint;

	// Token: 0x04001201 RID: 4609
	public Transform Bullet;

	// Token: 0x04001202 RID: 4610
	public AudioClip[] clips;

	// Token: 0x04001203 RID: 4611
	private bool SkillProcess;

	// Token: 0x04001204 RID: 4612
	private bool NeedRotateTo = true;

	// Token: 0x04001205 RID: 4613
	public float AttackCD = 2f;

	// Token: 0x04001206 RID: 4614
	public float AttackTime;

	// Token: 0x04001207 RID: 4615
	private float CheckTargetTime;

	// Token: 0x04001208 RID: 4616
	public Transform wheel;
}
