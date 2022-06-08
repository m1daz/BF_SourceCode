using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000230 RID: 560
public class GGNetWorkAIControl_Boss1_4_L : MonoBehaviour
{
	// Token: 0x06000F9A RID: 3994 RVA: 0x00085F94 File Offset: 0x00084394
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
		UnityEngine.Object.Instantiate<Transform>(this.bufferZone, new Vector3(2f, 0.39f, -2f), Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
		GameObject[] array = GameObject.FindGameObjectsWithTag("HuntingModeBoss");
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].name != base.gameObject.name)
			{
				this.ThunderChainTarget = array[i];
				this.TargetGGNetWorkAIControl_Boss1_4_R = this.ThunderChainTarget.GetComponent<GGNetWorkAIControl_Boss1_4_R>();
			}
		}
		this.ThunderChainInstantiate();
		GameObject gameObject = new GameObject();
		gameObject.transform.position = new Vector3(-24.6f, 0.9f, -26.6f);
		this.MoveTarget.Add(gameObject);
		GameObject gameObject2 = new GameObject();
		gameObject2.transform.position = new Vector3(28.3f, 0.9f, -26.6f);
		this.MoveTarget.Add(gameObject2);
		this.DifficultyInit();
		if (GGNetworkKit.mInstance.IsMasterClient())
		{
			this.FlameWall();
		}
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x0008611C File Offset: 0x0008451C
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
			this.mGlobalInfo.modeInfo.huntingprocess1 = 0.5f * (float)this.mGGNetWorkAIProperty.mBlood / (float)this.maxBlood;
			this.AILogic();
			this.IsCanSkill();
			this.CheckTarget();
		}
		this.PassiveSkillCheck();
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x000861EC File Offset: 0x000845EC
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
			5f,
			21f,
			500f,
			0.5f
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

	// Token: 0x06000F9D RID: 3997 RVA: 0x0008638A File Offset: 0x0008478A
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

	// Token: 0x06000F9E RID: 3998 RVA: 0x000863C4 File Offset: 0x000847C4
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

	// Token: 0x06000F9F RID: 3999 RVA: 0x0008653C File Offset: 0x0008493C
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

	// Token: 0x06000FA0 RID: 4000 RVA: 0x0008660C File Offset: 0x00084A0C
	private bool SkillReleaseRate(int index)
	{
		int num = UnityEngine.Random.Range(0, 100);
		return num < this.skillReleaseRate[index];
	}

	// Token: 0x06000FA1 RID: 4001 RVA: 0x0008663C File Offset: 0x00084A3C
	private void ThunderChainInstantiate()
	{
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.laser, base.transform.position, Quaternion.identity);
		transform.parent = base.transform;
		transform.GetComponent<GGThunderChainForBoss1_4>().startTransform = base.transform;
		transform.GetComponent<GGThunderChainForBoss1_4>().endTransform = this.ThunderChainTarget.transform;
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x00086698 File Offset: 0x00084A98
	private void FlameShoot()
	{
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.FlameShoot_L, this.FlameShoot_L_firePoint.position, this.FlameShoot_L_firePoint.rotation);
		transform.parent = base.transform;
		base.GetComponent<AudioSource>().clip = this.clips[0];
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x000866F4 File Offset: 0x00084AF4
	private void EarthShakeAndDragonBaby()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageHuntingModeEarthShake;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
		base.StartCoroutine(this.SummonDragonBaby(4f));
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x00086730 File Offset: 0x00084B30
	private IEnumerator SummonDragonBaby(float delay)
	{
		base.GetComponent<AudioSource>().clip = this.clips[1];
		base.GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(delay);
		if (this.Players.Count == 1)
		{
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeDragonBaby", new Vector3(20f, 0.9f, 0f), base.transform.rotation);
		}
		else if (this.Players.Count > 1)
		{
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeDragonBaby", new Vector3(20f, 0.9f, 0f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeDragonBaby", new Vector3(10f, 0.9f, 0f), base.transform.rotation);
		}
		yield break;
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x00086752 File Offset: 0x00084B52
	private void FlameWall()
	{
		if (this.skillEnable[2])
		{
			GGNetworkKit.mInstance.CreateSeceneObject("FlameWall_HuntingMode", new Vector3(0f, 1f, 0f), Quaternion.identity);
		}
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x0008678C File Offset: 0x00084B8C
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

	// Token: 0x06000FA7 RID: 4007 RVA: 0x000867F0 File Offset: 0x00084BF0
	private void PassiveSkill()
	{
		Collider[] array = Physics.OverlapSphere(base.transform.position, 3f);
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

	// Token: 0x06000FA8 RID: 4008 RVA: 0x00086920 File Offset: 0x00084D20
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

	// Token: 0x06000FA9 RID: 4009 RVA: 0x000869A4 File Offset: 0x00084DA4
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

	// Token: 0x06000FAA RID: 4010 RVA: 0x000869F4 File Offset: 0x00084DF4
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

	// Token: 0x06000FAB RID: 4011 RVA: 0x00086A8C File Offset: 0x00084E8C
	public void AIDamaged(GGDamageEventArgs mdamageEventArgs)
	{
		mdamageEventArgs.id = this.mPhotonView.viewID;
		GGNetworkKit.mInstance.DamageToAI(mdamageEventArgs, this.mPhotonView);
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x00086AB0 File Offset: 0x00084EB0
	public void Event_Damage_AI(GGDamageEventArgs damageEventArgs)
	{
		if (damageEventArgs.id == this.mPhotonView.viewID)
		{
			int damage = (int)damageEventArgs.damage;
			this.decreaseBlood(damage);
		}
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x00086AE4 File Offset: 0x00084EE4
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
				if (this.TargetGGNetWorkAIControl_Boss1_4_R.bDead)
				{
					this.SendBossKilledMessage();
				}
			}
		}
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x00086B80 File Offset: 0x00084F80
	public void playEnemyDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x00086B8D File Offset: 0x00084F8D
	public void playDieEffect()
	{
		GGNetworkKit.mInstance.CreateSeceneObject("boss_dead", base.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x00086BC7 File Offset: 0x00084FC7
	public void playBulletHitAudio()
	{
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x00086BCC File Offset: 0x00084FCC
	private IEnumerator waitForSecondsToDestory(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x00086BF0 File Offset: 0x00084FF0
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

	// Token: 0x06000FB3 RID: 4019 RVA: 0x00086D38 File Offset: 0x00085138
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

	// Token: 0x06000FB4 RID: 4020 RVA: 0x00086E60 File Offset: 0x00085260
	private void onDestroy()
	{
		GGNetworkKit.mInstance.AIReceiveDamage -= this.Event_Damage_AI;
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x00086E78 File Offset: 0x00085278
	public void SendBossKilledMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageHuntingModeBossKilled;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x0400116C RID: 4460
	private NavMeshAgent mNavMeshAgent;

	// Token: 0x0400116D RID: 4461
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x0400116E RID: 4462
	private GGNetWorkAISeeker mGGNetWorkAISeeker;

	// Token: 0x0400116F RID: 4463
	private GGNetWorkAIProperty mGGNetWorkAIProperty;

	// Token: 0x04001170 RID: 4464
	private GGNetworkCharacter targetNetworkCharacter;

	// Token: 0x04001171 RID: 4465
	private PhotonView mPhotonView;

	// Token: 0x04001172 RID: 4466
	public bool bDead;

	// Token: 0x04001173 RID: 4467
	public bool preState;

	// Token: 0x04001174 RID: 4468
	private AudioSource audioEnemyDie;

	// Token: 0x04001175 RID: 4469
	public int mPreSkillIndex;

	// Token: 0x04001176 RID: 4470
	private float logicTime;

	// Token: 0x04001177 RID: 4471
	public Dictionary<int, GameObject> Players;

	// Token: 0x04001178 RID: 4472
	public List<GameObject> PlayersList = new List<GameObject>();

	// Token: 0x04001179 RID: 4473
	private List<GameObject> Player_HighHatred = new List<GameObject>();

	// Token: 0x0400117A RID: 4474
	private List<GameObject> Player_LowBlood = new List<GameObject>();

	// Token: 0x0400117B RID: 4475
	private float checkCountTime;

	// Token: 0x0400117C RID: 4476
	private int FlameTrailCount = 10;

	// Token: 0x0400117D RID: 4477
	public AudioClip[] clips;

	// Token: 0x0400117E RID: 4478
	private bool SkillProcess;

	// Token: 0x0400117F RID: 4479
	private bool NeedRotateTo = true;

	// Token: 0x04001180 RID: 4480
	private float CheckTargetTime;

	// Token: 0x04001181 RID: 4481
	private float CheckHatredTime;

	// Token: 0x04001182 RID: 4482
	private float CheckBloodTime;

	// Token: 0x04001183 RID: 4483
	public bool[] skillEnable = new bool[4];

	// Token: 0x04001184 RID: 4484
	public float[] skillCD = new float[6];

	// Token: 0x04001185 RID: 4485
	public float[] skillCheckTime = new float[6];

	// Token: 0x04001186 RID: 4486
	public int[] skillReleaseRate = new int[6];

	// Token: 0x04001187 RID: 4487
	public List<GameObject> MoveTarget = new List<GameObject>();

	// Token: 0x04001188 RID: 4488
	private float speedChangetime;

	// Token: 0x04001189 RID: 4489
	private bool IsMoveDown = true;

	// Token: 0x0400118A RID: 4490
	private bool IsMoveUp;

	// Token: 0x0400118B RID: 4491
	public GameObject ThunderChainTarget;

	// Token: 0x0400118C RID: 4492
	private GGNetWorkAIControl_Boss1_4_R TargetGGNetWorkAIControl_Boss1_4_R;

	// Token: 0x0400118D RID: 4493
	public Transform laser;

	// Token: 0x0400118E RID: 4494
	public Transform FlameShoot_L;

	// Token: 0x0400118F RID: 4495
	public Transform FlameShoot_L_firePoint;

	// Token: 0x04001190 RID: 4496
	public Transform bufferZone;

	// Token: 0x04001191 RID: 4497
	public Transform[] DragonBabyTranform;

	// Token: 0x04001192 RID: 4498
	public Transform BossDeadEffect;

	// Token: 0x04001193 RID: 4499
	public int maxBlood = 20000;
}
