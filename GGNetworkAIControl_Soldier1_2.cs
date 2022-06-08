using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000236 RID: 566
public class GGNetworkAIControl_Soldier1_2 : MonoBehaviour
{
	// Token: 0x06001012 RID: 4114 RVA: 0x00089DF0 File Offset: 0x000881F0
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

	// Token: 0x06001013 RID: 4115 RVA: 0x00089E78 File Offset: 0x00088278
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
		this.wheel.localEulerAngles += new Vector3(0f, 15f, 0f);
	}

	// Token: 0x06001014 RID: 4116 RVA: 0x00089FD8 File Offset: 0x000883D8
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

	// Token: 0x06001015 RID: 4117 RVA: 0x0008A100 File Offset: 0x00088500
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

	// Token: 0x06001016 RID: 4118 RVA: 0x0008A150 File Offset: 0x00088550
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

	// Token: 0x06001017 RID: 4119 RVA: 0x0008A1F8 File Offset: 0x000885F8
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

	// Token: 0x06001018 RID: 4120 RVA: 0x0008A25C File Offset: 0x0008865C
	private void Attack()
	{
		for (int i = 0; i < this.QiangFirepoint.Length; i++)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.Bullet, this.QiangFirepoint[i].position, this.QiangFirepoint[i].rotation);
		}
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x0008A2A8 File Offset: 0x000886A8
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

	// Token: 0x0600101A RID: 4122 RVA: 0x0008A32C File Offset: 0x0008872C
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

	// Token: 0x0600101B RID: 4123 RVA: 0x0008A3B0 File Offset: 0x000887B0
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

	// Token: 0x0600101C RID: 4124 RVA: 0x0008A448 File Offset: 0x00088848
	public void AIDamaged(GGDamageEventArgs mdamageEventArgs)
	{
		mdamageEventArgs.id = this.mPhotonView.viewID;
		GGNetworkKit.mInstance.DamageToAI(mdamageEventArgs, this.mPhotonView);
	}

	// Token: 0x0600101D RID: 4125 RVA: 0x0008A46C File Offset: 0x0008886C
	public void Event_Damage_AI(GGDamageEventArgs damageEventArgs)
	{
		if (damageEventArgs.id == this.mPhotonView.viewID)
		{
			int damage = (int)damageEventArgs.damage;
			this.decreaseBlood(damage);
		}
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x0008A4A0 File Offset: 0x000888A0
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

	// Token: 0x0600101F RID: 4127 RVA: 0x0008A51D File Offset: 0x0008891D
	public void playEnemyDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x0008A52A File Offset: 0x0008892A
	public void playDieEffect()
	{
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x0008A52C File Offset: 0x0008892C
	public void playBulletHitAudio()
	{
	}

	// Token: 0x06001022 RID: 4130 RVA: 0x0008A530 File Offset: 0x00088930
	private IEnumerator waitForSecondsToDestory(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x06001023 RID: 4131 RVA: 0x0008A552 File Offset: 0x00088952
	private void onDestroy()
	{
		GGNetworkKit.mInstance.AIReceiveDamage -= this.Event_Damage_AI;
	}

	// Token: 0x06001024 RID: 4132 RVA: 0x0008A56C File Offset: 0x0008896C
	private IEnumerator DestroySlef()
	{
		yield return new WaitForSeconds(50f);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x04001209 RID: 4617
	private NavMeshAgent mNavMeshAgent;

	// Token: 0x0400120A RID: 4618
	private GGNetWorkAISeeker mGGNetWorkAISeeker;

	// Token: 0x0400120B RID: 4619
	private GGNetWorkAIProperty mGGNetWorkAIProperty;

	// Token: 0x0400120C RID: 4620
	private GGNetworkCharacter targetNetworkCharacter;

	// Token: 0x0400120D RID: 4621
	private PhotonView mPhotonView;

	// Token: 0x0400120E RID: 4622
	public bool bDead;

	// Token: 0x0400120F RID: 4623
	public bool preState;

	// Token: 0x04001210 RID: 4624
	private AudioSource audioEnemyDie;

	// Token: 0x04001211 RID: 4625
	public int mPreSkillIndex;

	// Token: 0x04001212 RID: 4626
	private float logicTime;

	// Token: 0x04001213 RID: 4627
	public Dictionary<int, GameObject> Players;

	// Token: 0x04001214 RID: 4628
	public List<GameObject> PlayersList = new List<GameObject>();

	// Token: 0x04001215 RID: 4629
	private float checkCountTime;

	// Token: 0x04001216 RID: 4630
	private int QiangFireCount = 3;

	// Token: 0x04001217 RID: 4631
	public Transform[] QiangFirepoint;

	// Token: 0x04001218 RID: 4632
	public Transform Bullet;

	// Token: 0x04001219 RID: 4633
	public AudioClip[] clips;

	// Token: 0x0400121A RID: 4634
	private bool SkillProcess;

	// Token: 0x0400121B RID: 4635
	private bool NeedRotateTo = true;

	// Token: 0x0400121C RID: 4636
	public float AttackCD = 2f;

	// Token: 0x0400121D RID: 4637
	public float AttackTime;

	// Token: 0x0400121E RID: 4638
	private float CheckTargetTime;

	// Token: 0x0400121F RID: 4639
	public Transform wheel;
}
