using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000231 RID: 561
public class GGNetWorkAIControl_Boss1_4_R : MonoBehaviour
{
	// Token: 0x06000FB7 RID: 4023 RVA: 0x0008715C File Offset: 0x0008555C
	private void Start()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		this.mNavMeshAgent = base.GetComponent<NavMeshAgent>();
		this.mGGNetWorkAISeeker = base.GetComponent<GGNetWorkAISeeker>();
		this.mGGNetWorkAIProperty = base.GetComponent<GGNetWorkAIProperty>();
		this.mPhotonView = base.GetComponent<PhotonView>();
		this.audioEnemyDie = base.GetComponent<AudioSource>();
		GGNetworkKit.mInstance.AIReceiveDamage += this.Event_Damage_AI;
		this.RefreshTarget();
		GameObject[] array = GameObject.FindGameObjectsWithTag("HuntingModeBoss");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].name != base.gameObject.name)
			{
				this.ThunderChainTarget = array[i];
				this.TargetGGNetWorkAIControl_Boss1_4_L = this.ThunderChainTarget.GetComponent<GGNetWorkAIControl_Boss1_4_L>();
			}
		}
		GameObject gameObject = new GameObject();
		gameObject.transform.position = new Vector3(-24.6f, 0.9f, 23f);
		this.MoveTarget.Add(gameObject);
		GameObject gameObject2 = new GameObject();
		gameObject2.transform.position = new Vector3(28.3f, 0.9f, 23f);
		this.MoveTarget.Add(gameObject2);
		this.DifficultyInit();
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x00087290 File Offset: 0x00085690
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
			this.mGlobalInfo.modeInfo.huntingprocess2 = 0.5f * (float)this.mGGNetWorkAIProperty.mBlood / (float)this.maxBlood;
			this.AILogic();
			this.IsCanSkill();
			this.CheckTarget();
		}
		this.PassiveSkillCheck();
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x00087360 File Offset: 0x00085760
	public void DifficultyInit()
	{
		int difficultySet = GGNetWorkAIDifficultyControl.mInstance.difficultySet;
		int maxPlayerSet = GGNetWorkAIDifficultyControl.mInstance.maxPlayerSet;
		int count = GGNetworkKit.mInstance.GetPlayerGameObjectList().Count;
		if (difficultySet == 1)
		{
			this.skillEnable = new bool[]
			{
				true,
				true,
				false,
				true
			};
			this.mGGNetWorkAIProperty.mBlood = (int)(0.7f * (float)this.mGGNetWorkAIProperty.mBlood);
		}
		else if (difficultySet == 2)
		{
			this.skillEnable = new bool[]
			{
				true,
				true,
				true,
				true
			};
			this.mGGNetWorkAIProperty.mBlood = (int)(1f * (float)this.mGGNetWorkAIProperty.mBlood);
		}
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
		this.maxBlood = this.mGGNetWorkAIProperty.mBlood;
		this.skillCD = new float[]
		{
			4.5f,
			51f,
			500f,
			2f
		};
		this.skillCheckTime = new float[4];
		this.skillReleaseRate = new int[]
		{
			100,
			100,
			100,
			100
		};
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x000874FE File Offset: 0x000858FE
	private void StateChange(int SkillIndex)
	{
		switch (SkillIndex)
		{
		case 1:
			this.FlameShoot();
			break;
		case 2:
			this.EarthShakeAndDragonBaby();
			break;
		}
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x00087538 File Offset: 0x00085938
	private void AILogic()
	{
		this.speedChangetime += Time.deltaTime;
		if (this.speedChangetime > 5f)
		{
			this.mNavMeshAgent.speed = UnityEngine.Random.Range(2f, 7f);
			this.speedChangetime = 0f;
		}
		if (this.IsMoveUp)
		{
			if (base.transform.position.x > this.MoveTarget[0].transform.position.x)
			{
				base.transform.position -= new Vector3(this.mNavMeshAgent.speed, 0f, 0f) * Time.deltaTime;
			}
			else
			{
				this.IsMoveDown = true;
				this.IsMoveUp = false;
			}
		}
		else if (this.IsMoveDown)
		{
			if (base.transform.position.x < this.MoveTarget[1].transform.position.x)
			{
				base.transform.position += new Vector3(this.mNavMeshAgent.speed, 0f, 0f) * Time.deltaTime;
			}
			else
			{
				this.IsMoveDown = false;
				this.IsMoveUp = true;
			}
		}
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x000876B0 File Offset: 0x00085AB0
	private void IsCanSkill()
	{
		if (!this.SkillProcess && this.skillEnable[0])
		{
			this.skillCheckTime[0] += Time.deltaTime;
			if (this.skillCheckTime[0] > this.skillCD[0])
			{
				if (this.SkillReleaseRate(0))
				{
					this.mGGNetWorkAIProperty.mSkillIndex = 1;
				}
				this.skillCheckTime[0] = 0f;
			}
		}
		if (this.skillEnable[1])
		{
			this.skillCheckTime[1] += Time.deltaTime;
			if (this.skillCheckTime[1] > this.skillCD[1])
			{
				if (this.SkillReleaseRate(1))
				{
					this.mGGNetWorkAIProperty.mSkillIndex = 2;
				}
				this.skillCheckTime[1] = 0f;
			}
		}
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x00087780 File Offset: 0x00085B80
	private bool SkillReleaseRate(int index)
	{
		int num = UnityEngine.Random.Range(0, 100);
		return num < this.skillReleaseRate[index];
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x000877B0 File Offset: 0x00085BB0
	private void FlameShoot()
	{
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.FlameShoot_L, this.FlameShoot_L_firePoint.position, this.FlameShoot_L_firePoint.rotation);
		transform.parent = base.transform;
		base.GetComponent<AudioSource>().clip = this.clips[0];
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x0008780C File Offset: 0x00085C0C
	private void EarthShakeAndDragonBaby()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageHuntingModeEarthShake;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
		base.StartCoroutine(this.SummonDragonBaby(4f));
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x00087848 File Offset: 0x00085C48
	private IEnumerator SummonDragonBaby(float delay)
	{
		base.GetComponent<AudioSource>().clip = this.clips[1];
		base.GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(delay);
		if (this.Players.Count == 1)
		{
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeDragonBaby", new Vector3(-20f, 0.9f, 0f), base.transform.rotation);
		}
		else if (this.Players.Count > 1)
		{
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeDragonBaby", new Vector3(-20f, 0.9f, 0f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeDragonBaby", new Vector3(-10f, 0.9f, 0f), base.transform.rotation);
		}
		yield break;
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x0008786C File Offset: 0x00085C6C
	private void PassiveSkillCheck()
	{
		if (this.skillEnable[3])
		{
			this.skillCheckTime[3] += Time.deltaTime;
			if (this.skillCheckTime[3] > this.skillCD[3])
			{
				if (this.SkillReleaseRate(3))
				{
					this.PassiveSkill();
				}
				this.skillCheckTime[3] = 0f;
			}
		}
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x000878D0 File Offset: 0x00085CD0
	private void PassiveSkill()
	{
		Collider[] array = Physics.OverlapSphere(base.transform.position, 4f);
		if (array != null)
		{
			foreach (Collider collider in array)
			{
				if (collider.transform.tag == "Player")
				{
					GGDamageEventArgs ggdamageEventArgs = new GGDamageEventArgs();
					ggdamageEventArgs.damage = (short)UnityEngine.Random.Range(90, 140);
					collider.transform.SendMessageUpwards("Event_Damage", ggdamageEventArgs, SendMessageOptions.DontRequireReceiver);
					Vector3 vector = new Vector3(collider.transform.position.x - base.transform.position.x, 0f, collider.transform.position.z - base.transform.position.z);
					Vector3 normalized = vector.normalized;
					collider.transform.SendMessageUpwards("AutoMove", normalized * 5.5f + new Vector3(0f, 3.5f, 0f), SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x00087A00 File Offset: 0x00085E00
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

	// Token: 0x06000FC4 RID: 4036 RVA: 0x00087A84 File Offset: 0x00085E84
	private void CheckTarget()
	{
		this.CheckTargetTime += Time.deltaTime;
		if (this.CheckTargetTime > 2f)
		{
			if (GGNetworkKit.mInstance.IsMasterClient())
			{
				this.RefreshTarget();
			}
			this.CheckTargetTime = 0f;
		}
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x00087AD4 File Offset: 0x00085ED4
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

	// Token: 0x06000FC6 RID: 4038 RVA: 0x00087B6C File Offset: 0x00085F6C
	public void AIDamaged(GGDamageEventArgs mdamageEventArgs)
	{
		mdamageEventArgs.id = this.mPhotonView.viewID;
		GGNetworkKit.mInstance.DamageToAI(mdamageEventArgs, this.mPhotonView);
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x00087B90 File Offset: 0x00085F90
	public void Event_Damage_AI(GGDamageEventArgs damageEventArgs)
	{
		if (damageEventArgs.id == this.mPhotonView.viewID)
		{
			int damage = (int)damageEventArgs.damage;
			this.decreaseBlood(damage);
		}
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x00087BC4 File Offset: 0x00085FC4
	public void decreaseBlood(int bulletDamage)
	{
		if (this.mGGNetWorkAIProperty.mBlood > 0)
		{
			this.mGGNetWorkAIProperty.mBlood -= bulletDamage;
			this.mGGNetWorkAIProperty.mBlood = Mathf.Max(this.mGGNetWorkAIProperty.mBlood, 0);
			if (this.mGGNetWorkAIProperty.mBlood <= 0)
			{
				this.bDead = true;
				this.mNavMeshAgent.velocity = Vector3.zero;
				this.mNavMeshAgent.Stop();
				this.playEnemyDieAudio();
				if (this.TargetGGNetWorkAIControl_Boss1_4_L.bDead)
				{
					this.SendBossKilledMessage();
				}
			}
		}
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x00087C60 File Offset: 0x00086060
	public void playEnemyDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x00087C6D File Offset: 0x0008606D
	public void playDieEffect()
	{
		GGNetworkKit.mInstance.CreateSeceneObject("boss_dead", base.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x00087CA7 File Offset: 0x000860A7
	public void playBulletHitAudio()
	{
	}

	// Token: 0x06000FCC RID: 4044 RVA: 0x00087CAC File Offset: 0x000860AC
	private IEnumerator waitForSecondsToDestory(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x00087CD0 File Offset: 0x000860D0
	private void PlayerHatredJudge()
	{
		this.CheckHatredTime += Time.deltaTime;
		if (this.CheckHatredTime > 24f)
		{
			if (GGNetworkKit.mInstance.IsMasterClient())
			{
				this.Player_HighHatred.Clear();
				this.RefreshTarget();
				foreach (GameObject gameObject in this.PlayersList)
				{
					GGNetworkCharacter component = gameObject.GetComponent<GGNetworkCharacter>();
					int damageNum = (int)component.mPlayerProperties.damageNum;
					if (damageNum > (int)((float)this.mGGNetWorkAIProperty.mBlood * 0.03f))
					{
						this.Player_HighHatred.Add(gameObject);
					}
					component.mPlayerProperties.damageNum = 0;
				}
				if (this.Player_HighHatred.Count > 0)
				{
					int num = UnityEngine.Random.Range(0, 100);
					if (num < 85)
					{
						this.mGGNetWorkAISeeker.target = this.Player_HighHatred[UnityEngine.Random.Range(0, this.Player_HighHatred.Count)];
						this.targetNetworkCharacter = this.mGGNetWorkAISeeker.target.GetComponent<GGNetworkCharacter>();
					}
				}
			}
			this.CheckHatredTime = 0f;
		}
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x00087E18 File Offset: 0x00086218
	private void PlayerBloodJudge()
	{
		this.CheckBloodTime += Time.deltaTime;
		if (this.CheckBloodTime > 40f)
		{
			if (GGNetworkKit.mInstance.IsMasterClient())
			{
				this.Player_LowBlood.Clear();
				this.RefreshTarget();
				foreach (GameObject gameObject in this.PlayersList)
				{
					GGNetworkCharacter component = gameObject.GetComponent<GGNetworkCharacter>();
					int mBlood = component.mBlood;
					if (mBlood < 40)
					{
						this.Player_LowBlood.Add(gameObject);
					}
				}
				if (this.Player_LowBlood.Count > 0)
				{
					int num = UnityEngine.Random.Range(0, 100);
					if (num < 70)
					{
						this.mGGNetWorkAISeeker.target = this.Player_LowBlood[UnityEngine.Random.Range(0, this.Player_LowBlood.Count)];
						this.targetNetworkCharacter = this.mGGNetWorkAISeeker.target.GetComponent<GGNetworkCharacter>();
					}
				}
			}
			this.CheckBloodTime = 0f;
		}
	}

	// Token: 0x06000FCF RID: 4047 RVA: 0x00087F40 File Offset: 0x00086340
	private void onDestroy()
	{
		GGNetworkKit.mInstance.AIReceiveDamage -= this.Event_Damage_AI;
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x00087F58 File Offset: 0x00086358
	public void SendBossKilledMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageHuntingModeBossKilled;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x04001194 RID: 4500
	private NavMeshAgent mNavMeshAgent;

	// Token: 0x04001195 RID: 4501
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x04001196 RID: 4502
	private GGNetWorkAISeeker mGGNetWorkAISeeker;

	// Token: 0x04001197 RID: 4503
	private GGNetWorkAIProperty mGGNetWorkAIProperty;

	// Token: 0x04001198 RID: 4504
	private GGNetworkCharacter targetNetworkCharacter;

	// Token: 0x04001199 RID: 4505
	private PhotonView mPhotonView;

	// Token: 0x0400119A RID: 4506
	public bool bDead;

	// Token: 0x0400119B RID: 4507
	public bool preState;

	// Token: 0x0400119C RID: 4508
	private AudioSource audioEnemyDie;

	// Token: 0x0400119D RID: 4509
	public int mPreSkillIndex;

	// Token: 0x0400119E RID: 4510
	private float logicTime;

	// Token: 0x0400119F RID: 4511
	public Dictionary<int, GameObject> Players;

	// Token: 0x040011A0 RID: 4512
	public List<GameObject> PlayersList = new List<GameObject>();

	// Token: 0x040011A1 RID: 4513
	private List<GameObject> Player_HighHatred = new List<GameObject>();

	// Token: 0x040011A2 RID: 4514
	private List<GameObject> Player_LowBlood = new List<GameObject>();

	// Token: 0x040011A3 RID: 4515
	private float checkCountTime;

	// Token: 0x040011A4 RID: 4516
	private int FlameTrailCount = 10;

	// Token: 0x040011A5 RID: 4517
	public AudioClip[] clips;

	// Token: 0x040011A6 RID: 4518
	private bool SkillProcess;

	// Token: 0x040011A7 RID: 4519
	private bool NeedRotateTo = true;

	// Token: 0x040011A8 RID: 4520
	private float CheckTargetTime;

	// Token: 0x040011A9 RID: 4521
	private float CheckHatredTime;

	// Token: 0x040011AA RID: 4522
	private float CheckBloodTime;

	// Token: 0x040011AB RID: 4523
	public bool[] skillEnable = new bool[4];

	// Token: 0x040011AC RID: 4524
	public float[] skillCD = new float[6];

	// Token: 0x040011AD RID: 4525
	public float[] skillCheckTime = new float[6];

	// Token: 0x040011AE RID: 4526
	public int[] skillReleaseRate = new int[6];

	// Token: 0x040011AF RID: 4527
	public List<GameObject> MoveTarget = new List<GameObject>();

	// Token: 0x040011B0 RID: 4528
	private float speedChangetime;

	// Token: 0x040011B1 RID: 4529
	private bool IsMoveDown;

	// Token: 0x040011B2 RID: 4530
	private bool IsMoveUp = true;

	// Token: 0x040011B3 RID: 4531
	public GameObject ThunderChainTarget;

	// Token: 0x040011B4 RID: 4532
	private GGNetWorkAIControl_Boss1_4_L TargetGGNetWorkAIControl_Boss1_4_L;

	// Token: 0x040011B5 RID: 4533
	public Transform laser;

	// Token: 0x040011B6 RID: 4534
	public Transform FlameShoot_L;

	// Token: 0x040011B7 RID: 4535
	public Transform FlameShoot_L_firePoint;

	// Token: 0x040011B8 RID: 4536
	public Transform[] DragonBabyTranform;

	// Token: 0x040011B9 RID: 4537
	public Transform BossDeadEffect;

	// Token: 0x040011BA RID: 4538
	public int maxBlood = 20000;
}
