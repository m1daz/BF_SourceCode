using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000233 RID: 563
public class GGNetWorkAIControl_PoisonousSnail : MonoBehaviour
{
	// Token: 0x06000FE6 RID: 4070 RVA: 0x00088AD8 File Offset: 0x00086ED8
	private void Start()
	{
		this.mNavMeshAgent = base.GetComponent<NavMeshAgent>();
		this.mGGNetWorkAISeeker = base.GetComponent<GGNetWorkAISeeker>();
		this.mGGNetWorkAIProperty = base.GetComponent<GGNetWorkAIProperty>();
		this.mPhotonView = base.GetComponent<PhotonView>();
		this.audioEnemyDie = base.GetComponent<AudioSource>();
		for (int i = 0; i < 6; i++)
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-29.5f, 29.5f), 0.9f, UnityEngine.Random.Range(-29.5f, 29.5f));
			this.TargetList.Add(gameObject);
		}
		this.SetTarget();
		this.DifficultyInit();
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x00088B84 File Offset: 0x00086F84
	private void Update()
	{
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			this.CheckTarget();
		}
		this.PassiveSkillCheck();
	}

	// Token: 0x06000FE8 RID: 4072 RVA: 0x00088BA4 File Offset: 0x00086FA4
	public void DifficultyInit()
	{
		int difficultySet = GGNetWorkAIDifficultyControl.mInstance.difficultySet;
		int maxPlayerSet = GGNetWorkAIDifficultyControl.mInstance.maxPlayerSet;
		if (difficultySet == 1)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(0.6f * (float)this.mGGNetWorkAIProperty.mBlood);
		}
		else if (difficultySet == 2)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(1f * (float)this.mGGNetWorkAIProperty.mBlood);
		}
		if (maxPlayerSet == 1)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(0.3f * (float)this.mGGNetWorkAIProperty.mBlood);
		}
	}

	// Token: 0x06000FE9 RID: 4073 RVA: 0x00088C3B File Offset: 0x0008703B
	private void PassiveSkillCheck()
	{
		this.PassiveSkillCheckTime += Time.deltaTime;
		if (this.PassiveSkillCheckTime > 1f)
		{
			this.PassiveSkill();
			this.PassiveSkillCheckTime = 0f;
		}
	}

	// Token: 0x06000FEA RID: 4074 RVA: 0x00088C70 File Offset: 0x00087070
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

	// Token: 0x06000FEB RID: 4075 RVA: 0x00088DB0 File Offset: 0x000871B0
	private void SetTarget()
	{
		this.mGGNetWorkAISeeker.target = this.TargetList[UnityEngine.Random.Range(0, this.TargetList.Count)];
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x00088DDC File Offset: 0x000871DC
	private void CheckTarget()
	{
		this.CheckTargetTime += Time.deltaTime;
		if (this.CheckTargetTime > 8f)
		{
			if (GGNetworkKit.mInstance.IsMasterClient())
			{
				this.SetTarget();
			}
			this.CheckTargetTime = 0f;
		}
	}

	// Token: 0x040011D2 RID: 4562
	private NavMeshAgent mNavMeshAgent;

	// Token: 0x040011D3 RID: 4563
	private GGNetWorkAISeeker mGGNetWorkAISeeker;

	// Token: 0x040011D4 RID: 4564
	private GGNetWorkAIProperty mGGNetWorkAIProperty;

	// Token: 0x040011D5 RID: 4565
	private GGNetworkCharacter targetNetworkCharacter;

	// Token: 0x040011D6 RID: 4566
	private PhotonView mPhotonView;

	// Token: 0x040011D7 RID: 4567
	public bool bDead;

	// Token: 0x040011D8 RID: 4568
	public bool preState;

	// Token: 0x040011D9 RID: 4569
	private AudioSource audioEnemyDie;

	// Token: 0x040011DA RID: 4570
	public int mPreSkillIndex;

	// Token: 0x040011DB RID: 4571
	private float logicTime;

	// Token: 0x040011DC RID: 4572
	public Dictionary<int, GameObject> Players;

	// Token: 0x040011DD RID: 4573
	public List<GameObject> PlayersList = new List<GameObject>();

	// Token: 0x040011DE RID: 4574
	private float PassiveSkillCheckTime;

	// Token: 0x040011DF RID: 4575
	public AudioClip[] clip;

	// Token: 0x040011E0 RID: 4576
	private float CheckTargetTime;

	// Token: 0x040011E1 RID: 4577
	public List<GameObject> TargetList = new List<GameObject>();
}
