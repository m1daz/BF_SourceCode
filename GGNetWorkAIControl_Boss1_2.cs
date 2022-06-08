using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200022E RID: 558
public class GGNetWorkAIControl_Boss1_2 : MonoBehaviour
{
	// Token: 0x06000F57 RID: 3927 RVA: 0x00083528 File Offset: 0x00081928
	private void Start()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		this.mNavMeshAgent = base.GetComponent<NavMeshAgent>();
		this.mGGNetWorkAISeeker = base.GetComponent<GGNetWorkAISeeker>();
		this.mGGNetWorkAIProperty = base.GetComponent<GGNetWorkAIProperty>();
		this.mPhotonView = base.GetComponent<PhotonView>();
		this.audioEnemyDie = base.GetComponent<AudioSource>();
		GGNetworkKit.mInstance.AIReceiveDamage += this.Event_Damage_AI;
		Transform transform = UnityEngine.Object.Instantiate<Transform>(this.DeadZone, Vector3.zero, Quaternion.identity);
		Transform transform2 = UnityEngine.Object.Instantiate<Transform>(this.GravitySource, Vector3.zero, Quaternion.identity);
		Transform transform3 = UnityEngine.Object.Instantiate<Transform>(this.CenterGravitySource, Vector3.zero, Quaternion.identity);
		IEnumerator enumerator = transform2.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform item = (Transform)obj;
				this.GravitySourceTargetList.Add(item);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.DifficultyInit();
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x00083640 File Offset: 0x00081A40
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
			this.mGlobalInfo.modeInfo.huntingprocess1 = (float)this.mGGNetWorkAIProperty.mBlood / (float)this.maxBlood;
			this.AILogic();
			this.IsCanSkill();
			if (!(this.mNavMeshAgent.velocity == Vector3.zero) || this.mGGNetWorkAISeeker.target != null)
			{
			}
		}
		this.PassiveSkillCheck();
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x00083734 File Offset: 0x00081B34
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
				true,
				true,
				false,
				true
			};
			this.mGGNetWorkAIProperty.mBlood = (int)(0.7f * (float)this.mGGNetWorkAIProperty.mBlood);
			this.ThunderChainMaxCount = 4;
		}
		else if (difficultySet == 2)
		{
			this.skillEnable = new bool[]
			{
				true,
				true,
				true,
				true,
				true,
				true
			};
			this.mGGNetWorkAIProperty.mBlood = (int)(1f * (float)this.mGGNetWorkAIProperty.mBlood);
			this.ThunderChainMaxCount = 6;
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
			2f,
			20f,
			15f,
			30f,
			2f,
			0.6f
		};
		this.skillCheckTime = new float[6];
		this.skillReleaseRate = new int[]
		{
			100,
			100,
			100,
			100,
			100,
			100
		};
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x000838E0 File Offset: 0x00081CE0
	private void StateChange(int SkillIndex)
	{
		if (SkillIndex != 0)
		{
			if (SkillIndex == 4)
			{
				this.SkillProcess = true;
				this.ThunderChain();
			}
		}
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x00083910 File Offset: 0x00081D10
	private void AILogic()
	{
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x00083914 File Offset: 0x00081D14
	private void IsCanSkill()
	{
		if (!this.SkillProcess && this.skillEnable[0])
		{
			this.skillCheckTime[0] += Time.deltaTime;
			if (this.skillCheckTime[0] > this.skillCD[0])
			{
				if (this.SkillReleaseRate(0))
				{
					this.MagneticAttraction();
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
					this.FlameTrailCount = 10;
					base.StartCoroutine(this.ThunderTrailCheck());
				}
				this.skillCheckTime[1] = 0f;
			}
		}
		if (this.skillEnable[2])
		{
			this.skillCheckTime[2] += Time.deltaTime;
			if (this.skillCheckTime[2] > this.skillCD[2])
			{
				if (this.SkillReleaseRate(2))
				{
					this.PutPlayerToCenter();
				}
				this.skillCheckTime[2] = 0f;
			}
		}
		if (this.skillEnable[3])
		{
			this.skillCheckTime[3] += Time.deltaTime;
			if (this.skillCheckTime[3] > this.skillCD[3])
			{
				if (this.SkillReleaseRate(3))
				{
					this.mGGNetWorkAIProperty.mSkillIndex = 4;
					this.ThunderChainCheck();
				}
				this.skillCheckTime[3] = 0f;
			}
		}
		if (this.skillEnable[4])
		{
			this.skillCheckTime[4] += Time.deltaTime;
			if (this.skillCheckTime[4] > this.skillCD[4])
			{
				if (this.SkillReleaseRate(4))
				{
				}
				this.skillCheckTime[4] = 0f;
			}
		}
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x00083AF0 File Offset: 0x00081EF0
	private bool SkillReleaseRate(int index)
	{
		int num = UnityEngine.Random.Range(0, 100);
		return num < this.skillReleaseRate[index];
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x00083B1F File Offset: 0x00081F1F
	private void MagneticAttraction()
	{
		this.mGGNetWorkAISeeker.target = this.GravitySourceTargetList[UnityEngine.Random.Range(0, this.GravitySourceTargetList.Count)].gameObject;
		this.mGGNetWorkAIProperty.mSkillIndex = 0;
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x00083B5C File Offset: 0x00081F5C
	private IEnumerator ThunderTrailCheck()
	{
		yield return new WaitForSeconds(0.5f);
		if (this.FlameTrailCount > 0)
		{
			this.FlameTrailCount--;
			this.ThunderTrail();
			base.StartCoroutine(this.ThunderTrailCheck());
		}
		else
		{
			this.SkillProcess = false;
		}
		yield break;
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x00083B78 File Offset: 0x00081F78
	private void ThunderTrail()
	{
		if (this.mNavMeshAgent.velocity != Vector3.zero)
		{
			GGNetworkKit.mInstance.CreateSeceneObject("ThunderTrail_HuntingMode", base.transform.position - new Vector3(0f, base.transform.position.y - 1.3f, 0f) + 0.6f * base.transform.TransformDirection(Vector3.back).normalized, base.transform.rotation);
		}
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x00083C18 File Offset: 0x00082018
	private void PutPlayerToCenter()
	{
		GGNetworkKit.mInstance.CreateSeceneObject("CenterGravitySource_HuntingMode", new Vector3(0f, 1.3f, 0f), Quaternion.identity);
		this.SkillProcess = true;
		this.RefreshTarget();
		if (this.PlayersList.Count > 0)
		{
			GameObject gameObject = this.PlayersList[UnityEngine.Random.Range(0, this.PlayersList.Count)];
			int id = gameObject.GetComponent<GGNetworkCharacter>().mPlayerProperties.id;
			GGMessage ggmessage = new GGMessage();
			ggmessage.messageType = GGMessageType.MessagePlayerAutoMove;
			ggmessage.messageContent = new GGMessageContent();
			ggmessage.messageContent.X = (0f - gameObject.transform.position.x) * 2f;
			ggmessage.messageContent.Y = 0f;
			ggmessage.messageContent.Z = (0f - gameObject.transform.position.z) * 2f;
			GGNetworkKit.mInstance.SendMessage(ggmessage, id);
		}
		this.mGGNetWorkAISeeker.target = null;
		base.StartCoroutine(this.RushToCenter(1f));
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x00083D44 File Offset: 0x00082144
	private IEnumerator RushToCenter(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.mGGNetWorkAISeeker.target = this.CenterGravitySource.gameObject;
		base.StartCoroutine(this.RecovertoThinkAfterRush(4f));
		yield break;
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x00083D68 File Offset: 0x00082168
	private IEnumerator RecovertoThinkAfterRush(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.SkillProcess = false;
		this.mGGNetWorkAIProperty.mSkillIndex = 0;
		yield break;
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x00083D8C File Offset: 0x0008218C
	private void ThunderChainCheck()
	{
		this.mGGNetWorkAIProperty.mSkillStruct.Clear();
		List<int> list = new List<int>
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7
		};
		List<int> list2 = new List<int>();
		for (int i = 0; i < this.ThunderChainMaxCount; i++)
		{
			int num = list[UnityEngine.Random.Range(0, list.Count)];
			list.Remove(num);
			this.mGGNetWorkAIProperty.mSkillStruct.Add((byte)num);
		}
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x00083E38 File Offset: 0x00082238
	private void ThunderChain()
	{
		for (int i = 0; i < this.mGGNetWorkAIProperty.mSkillStruct.Count - 1; i++)
		{
			for (int j = i + 1; j < this.mGGNetWorkAIProperty.mSkillStruct.Count; j++)
			{
				Vector3 vector = this.GravitySourceTargetList[(int)this.mGGNetWorkAIProperty.mSkillStruct[i]].position + new Vector3(0f, 0.3f, 0f);
				Vector3 vector2 = this.GravitySourceTargetList[(int)this.mGGNetWorkAIProperty.mSkillStruct[j]].position + new Vector3(0f, 0.3f, 0f);
				Transform transform = UnityEngine.Object.Instantiate<Transform>(this.laser, vector, Quaternion.identity);
				IEnumerator enumerator = transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform2 = (Transform)obj;
						GGThunderChainEffect component = transform2.GetComponent<GGThunderChainEffect>();
						component.startPosition = vector;
						component.endPosition = vector2;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				transform.GetComponent<BoxCollider>().center = new Vector3(0f, 0f, Vector3.Distance(vector, vector2) * 0.5f);
				transform.GetComponent<BoxCollider>().size = new Vector3(0.2f, 0.2f, Vector3.Distance(vector, vector2));
				transform.LookAt(vector2);
				transform.GetComponent<BoxCollider>().isTrigger = true;
			}
		}
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x00083FE0 File Offset: 0x000823E0
	private void PassiveSkillCheck()
	{
		if (this.skillEnable[5])
		{
			this.skillCheckTime[5] += Time.deltaTime;
			if (this.skillCheckTime[5] > this.skillCD[5])
			{
				if (this.SkillReleaseRate(5))
				{
					this.PassiveSkill();
				}
				this.skillCheckTime[5] = 0f;
			}
		}
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x00084044 File Offset: 0x00082444
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
					base.GetComponent<AudioSource>().clip = this.clips[0];
					base.GetComponent<AudioSource>().Play();
				}
			}
		}
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x00084190 File Offset: 0x00082590
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

	// Token: 0x06000F69 RID: 3945 RVA: 0x00084214 File Offset: 0x00082614
	private void CheckTarget()
	{
		this.CheckTargetTime += Time.deltaTime;
		if (this.CheckTargetTime > 2f)
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

	// Token: 0x06000F6A RID: 3946 RVA: 0x00084298 File Offset: 0x00082698
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

	// Token: 0x06000F6B RID: 3947 RVA: 0x00084330 File Offset: 0x00082730
	public void AIDamaged(GGDamageEventArgs mdamageEventArgs)
	{
		mdamageEventArgs.id = this.mPhotonView.viewID;
		GGNetworkKit.mInstance.DamageToAI(mdamageEventArgs, this.mPhotonView);
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x00084354 File Offset: 0x00082754
	public void Event_Damage_AI(GGDamageEventArgs damageEventArgs)
	{
		if (damageEventArgs.id == this.mPhotonView.viewID)
		{
			int damage = (int)damageEventArgs.damage;
			this.decreaseBlood(damage);
		}
	}

	// Token: 0x06000F6D RID: 3949 RVA: 0x00084388 File Offset: 0x00082788
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
				base.StartCoroutine(this.waitForSecondsToDestory(3f));
				this.SendBossKilledMessage();
			}
		}
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x00084426 File Offset: 0x00082826
	public void playEnemyDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x00084433 File Offset: 0x00082833
	public void playDieEffect()
	{
		GGNetworkKit.mInstance.CreateSeceneObject("boss_dead", base.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x0008446D File Offset: 0x0008286D
	public void playBulletHitAudio()
	{
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x00084470 File Offset: 0x00082870
	private IEnumerator waitForSecondsToDestory(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x06000F72 RID: 3954 RVA: 0x00084494 File Offset: 0x00082894
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

	// Token: 0x06000F73 RID: 3955 RVA: 0x000845DC File Offset: 0x000829DC
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

	// Token: 0x06000F74 RID: 3956 RVA: 0x00084704 File Offset: 0x00082B04
	private void onDestroy()
	{
		GGNetworkKit.mInstance.AIReceiveDamage -= this.Event_Damage_AI;
	}

	// Token: 0x06000F75 RID: 3957 RVA: 0x0008471C File Offset: 0x00082B1C
	public void SendBossKilledMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageHuntingModeBossKilled;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x04001127 RID: 4391
	private NavMeshAgent mNavMeshAgent;

	// Token: 0x04001128 RID: 4392
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x04001129 RID: 4393
	private GGNetWorkAISeeker mGGNetWorkAISeeker;

	// Token: 0x0400112A RID: 4394
	private GGNetWorkAIProperty mGGNetWorkAIProperty;

	// Token: 0x0400112B RID: 4395
	private GGNetworkCharacter targetNetworkCharacter;

	// Token: 0x0400112C RID: 4396
	private PhotonView mPhotonView;

	// Token: 0x0400112D RID: 4397
	public bool bDead;

	// Token: 0x0400112E RID: 4398
	public bool preState;

	// Token: 0x0400112F RID: 4399
	private AudioSource audioEnemyDie;

	// Token: 0x04001130 RID: 4400
	public int mPreSkillIndex;

	// Token: 0x04001131 RID: 4401
	private float logicTime;

	// Token: 0x04001132 RID: 4402
	public Dictionary<int, GameObject> Players;

	// Token: 0x04001133 RID: 4403
	public List<GameObject> PlayersList = new List<GameObject>();

	// Token: 0x04001134 RID: 4404
	private List<GameObject> Player_HighHatred = new List<GameObject>();

	// Token: 0x04001135 RID: 4405
	private List<GameObject> Player_LowBlood = new List<GameObject>();

	// Token: 0x04001136 RID: 4406
	private float checkCountTime;

	// Token: 0x04001137 RID: 4407
	private int FlameTrailCount = 10;

	// Token: 0x04001138 RID: 4408
	public AudioClip[] clips;

	// Token: 0x04001139 RID: 4409
	private bool SkillProcess;

	// Token: 0x0400113A RID: 4410
	private bool NeedRotateTo = true;

	// Token: 0x0400113B RID: 4411
	private float CheckTargetTime;

	// Token: 0x0400113C RID: 4412
	private float CheckHatredTime;

	// Token: 0x0400113D RID: 4413
	private float CheckBloodTime;

	// Token: 0x0400113E RID: 4414
	public bool[] skillEnable = new bool[6];

	// Token: 0x0400113F RID: 4415
	public float[] skillCD = new float[6];

	// Token: 0x04001140 RID: 4416
	public float[] skillCheckTime = new float[6];

	// Token: 0x04001141 RID: 4417
	public int[] skillReleaseRate = new int[6];

	// Token: 0x04001142 RID: 4418
	public Transform GravitySource;

	// Token: 0x04001143 RID: 4419
	public List<Transform> GravitySourceTargetList = new List<Transform>();

	// Token: 0x04001144 RID: 4420
	public Transform CenterGravitySource;

	// Token: 0x04001145 RID: 4421
	private int ThunderChainMaxCount = 4;

	// Token: 0x04001146 RID: 4422
	public Transform laser;

	// Token: 0x04001147 RID: 4423
	public Transform DeadZone;

	// Token: 0x04001148 RID: 4424
	public Transform BossDeadEffect;

	// Token: 0x04001149 RID: 4425
	public int maxBlood = 20000;
}
