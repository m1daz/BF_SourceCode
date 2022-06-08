using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000234 RID: 564
public class GGNetWorkAIControl_RecoverSnail : MonoBehaviour
{
	// Token: 0x06000FEE RID: 4078 RVA: 0x00088E4C File Offset: 0x0008724C
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
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x00088EB8 File Offset: 0x000872B8
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
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			this.AILogic();
		}
		this.PassiveSkillCheck();
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x00088F1C File Offset: 0x0008731C
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
			this.recoverBlood = 500;
		}
		else if (count == 2)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(0.5f * (float)this.mGGNetWorkAIProperty.mBlood);
			this.recoverBlood = 700;
		}
		else if (count == 3)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(0.8f * (float)this.mGGNetWorkAIProperty.mBlood);
			this.recoverBlood = 1000;
		}
		else if (count == 4)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(1f * (float)this.mGGNetWorkAIProperty.mBlood);
			this.recoverBlood = 1500;
		}
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x00089070 File Offset: 0x00087470
	private void AILogic()
	{
		this.logicTime += Time.deltaTime;
		if (this.logicTime > 1f)
		{
			if (this.mGGNetWorkAISeeker.target != null && Vector3.Distance(base.transform.position, this.mGGNetWorkAISeeker.target.transform.position) < 3f)
			{
				this.mGGNetWorkAISeeker.target.GetComponent<GGNetWorkAIProperty>().mBlood += this.recoverBlood;
				GGNetworkKit.mInstance.CreateSeceneObject("HuntingMode_SnailHPGenerate", this.mGGNetWorkAISeeker.target.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
				GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
			}
			this.logicTime = 0f;
		}
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x00089163 File Offset: 0x00087563
	private void PassiveSkillCheck()
	{
		this.PassiveSkillCheckTime += Time.deltaTime;
		if (this.PassiveSkillCheckTime > 1f)
		{
			this.PassiveSkill();
			this.PassiveSkillCheckTime = 0f;
		}
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x00089198 File Offset: 0x00087598
	private void PassiveSkill()
	{
		Collider[] array = Physics.OverlapSphere(base.transform.position, 2f);
		if (array != null)
		{
			foreach (Collider collider in array)
			{
				if (collider.transform.tag == "Player")
				{
					GGDamageEventArgs ggdamageEventArgs = new GGDamageEventArgs();
					ggdamageEventArgs.damage = 120;
					collider.transform.SendMessageUpwards("Event_Damage", ggdamageEventArgs, SendMessageOptions.DontRequireReceiver);
					Vector3 vector = new Vector3(collider.transform.position.x - base.transform.position.x, 0f, collider.transform.position.z - base.transform.position.z);
					Vector3 normalized = vector.normalized;
					collider.transform.SendMessageUpwards("AutoMove", normalized * 5f + new Vector3(0f, 3f, 0f), SendMessageOptions.DontRequireReceiver);
					base.GetComponent<AudioSource>().clip = this.clip[0];
					base.GetComponent<AudioSource>().Play();
				}
			}
		}
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x000892D8 File Offset: 0x000876D8
	private void SetTarget()
	{
		this.mGGNetWorkAISeeker.target = GameObject.FindGameObjectWithTag("HuntingModeBoss");
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x000892EF File Offset: 0x000876EF
	public void AIDamaged(GGDamageEventArgs mdamageEventArgs)
	{
		mdamageEventArgs.id = this.mPhotonView.viewID;
		GGNetworkKit.mInstance.DamageToAI(mdamageEventArgs, this.mPhotonView);
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x00089314 File Offset: 0x00087714
	public void Event_Damage_AI(GGDamageEventArgs damageEventArgs)
	{
		if (damageEventArgs.id == this.mPhotonView.viewID)
		{
			int damage = (int)damageEventArgs.damage;
			this.decreaseBlood(damage);
		}
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x00089348 File Offset: 0x00087748
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

	// Token: 0x06000FF8 RID: 4088 RVA: 0x000893C5 File Offset: 0x000877C5
	public void playEnemyDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x000893D2 File Offset: 0x000877D2
	public void playDieEffect()
	{
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x000893D4 File Offset: 0x000877D4
	public void playBulletHitAudio()
	{
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x000893D8 File Offset: 0x000877D8
	private IEnumerator waitForSecondsToDestory(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x000893FA File Offset: 0x000877FA
	private void onDestroy()
	{
		GGNetworkKit.mInstance.AIReceiveDamage -= this.Event_Damage_AI;
	}

	// Token: 0x040011E2 RID: 4578
	private NavMeshAgent mNavMeshAgent;

	// Token: 0x040011E3 RID: 4579
	private GGNetWorkAISeeker mGGNetWorkAISeeker;

	// Token: 0x040011E4 RID: 4580
	private GGNetWorkAIProperty mGGNetWorkAIProperty;

	// Token: 0x040011E5 RID: 4581
	private GGNetworkCharacter targetNetworkCharacter;

	// Token: 0x040011E6 RID: 4582
	private PhotonView mPhotonView;

	// Token: 0x040011E7 RID: 4583
	public bool bDead;

	// Token: 0x040011E8 RID: 4584
	public bool preState;

	// Token: 0x040011E9 RID: 4585
	private AudioSource audioEnemyDie;

	// Token: 0x040011EA RID: 4586
	public int mPreSkillIndex;

	// Token: 0x040011EB RID: 4587
	private float logicTime;

	// Token: 0x040011EC RID: 4588
	public Dictionary<int, GameObject> Players;

	// Token: 0x040011ED RID: 4589
	public List<GameObject> PlayersList = new List<GameObject>();

	// Token: 0x040011EE RID: 4590
	private float PassiveSkillCheckTime;

	// Token: 0x040011EF RID: 4591
	public AudioClip[] clip;

	// Token: 0x040011F0 RID: 4592
	private float CheckTargetTime;

	// Token: 0x040011F1 RID: 4593
	private int recoverBlood = 3000;
}
