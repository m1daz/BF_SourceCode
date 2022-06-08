using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200022F RID: 559
public class GGNetWorkAIControl_Boss1_3 : MonoBehaviour
{
	// Token: 0x06000F77 RID: 3959 RVA: 0x00084ABC File Offset: 0x00082EBC
	private void Start()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		this.mNavMeshAgent = base.GetComponent<NavMeshAgent>();
		this.mGGNetWorkAISeeker = base.GetComponent<GGNetWorkAISeeker>();
		this.mGGNetWorkAIProperty = base.GetComponent<GGNetWorkAIProperty>();
		this.mPhotonView = base.GetComponent<PhotonView>();
		this.audioEnemyDie = base.GetComponent<AudioSource>();
		GGNetworkKit.mInstance.AIReceiveDamage += this.Event_Damage_AI;
		this.DifficultyInit();
	}

	// Token: 0x06000F78 RID: 3960 RVA: 0x00084B34 File Offset: 0x00082F34
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
			this.CheckTarget();
		}
		this.PassiveSkillCheck();
		this.PlayerHatredJudge();
		this.PlayerBloodJudge();
	}

	// Token: 0x06000F79 RID: 3961 RVA: 0x00084C0C File Offset: 0x0008300C
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
				true,
				false,
				true
			};
			this.mGGNetWorkAIProperty.mBlood = (int)(0.7f * (float)this.mGGNetWorkAIProperty.mBlood);
			this.skillCD = new float[]
			{
				47f,
				7f,
				31f,
				50f,
				120f,
				80f,
				0.6f
			};
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
				true,
				true
			};
			this.mGGNetWorkAIProperty.mBlood = (int)(1f * (float)this.mGGNetWorkAIProperty.mBlood);
			this.skillCD = new float[]
			{
				47f,
				7f,
				31f,
				43f,
				120f,
				80f,
				0.6f
			};
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
		this.skillCheckTime = new float[7];
		this.skillReleaseRate = new int[]
		{
			100,
			100,
			100,
			100,
			100,
			100,
			100
		};
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x00084DC4 File Offset: 0x000831C4
	private void StateChange(int SkillIndex)
	{
		switch (SkillIndex)
		{
		case 1:
			this.GravityFieldActive();
			break;
		case 2:
			this.EyeFireCount = 16;
			base.StartCoroutine(this.EyeFireCheck());
			break;
		case 3:
			this.SkillProcess = true;
			this.BloodAbsorbShield();
			break;
		}
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x00084E2D File Offset: 0x0008322D
	private void AILogic()
	{
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x00084E30 File Offset: 0x00083230
	private void IsCanSkill()
	{
		if (this.skillEnable[0])
		{
			this.skillCheckTime[0] += Time.deltaTime;
			if (this.skillCheckTime[0] > this.skillCD[0])
			{
				if (this.SkillReleaseRate(0))
				{
					this.mGGNetWorkAIProperty.mSkillIndex = 1;
					this.GravityField();
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
		if (this.skillEnable[2])
		{
			this.skillCheckTime[2] += Time.deltaTime;
			if (this.skillCheckTime[2] > this.skillCD[2])
			{
				if (this.SkillReleaseRate(2))
				{
					this.mGGNetWorkAIProperty.mSkillIndex = 3;
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
					this.SummonRecoverSnail();
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
					this.AcidRain();
				}
				this.skillCheckTime[4] = 0f;
			}
		}
		if (this.skillEnable[5])
		{
			this.skillCheckTime[5] += Time.deltaTime;
			if (this.skillCheckTime[5] > this.skillCD[5])
			{
				if (this.SkillReleaseRate(5))
				{
					this.SummonPoisonousSnail();
				}
				this.skillCheckTime[5] = 0f;
			}
		}
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x0008505C File Offset: 0x0008345C
	private bool SkillReleaseRate(int index)
	{
		int num = UnityEngine.Random.Range(0, 100);
		return num < this.skillReleaseRate[index];
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x0008508C File Offset: 0x0008348C
	private void GravityField()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessagePlayerSpeedSlow;
		ggmessage.messageContent = new GGMessageContent();
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x000850C1 File Offset: 0x000834C1
	private void GravityFieldActive()
	{
		this.gravityField.SetActive(true);
		base.StartCoroutine(this.GravityFieldDeactive());
	}

	// Token: 0x06000F80 RID: 3968 RVA: 0x000850DC File Offset: 0x000834DC
	private IEnumerator GravityFieldDeactive()
	{
		yield return new WaitForSeconds(16f);
		this.gravityField.SetActive(false);
		this.mGGNetWorkAIProperty.mSkillIndex = 0;
		yield break;
	}

	// Token: 0x06000F81 RID: 3969 RVA: 0x000850F8 File Offset: 0x000834F8
	private IEnumerator EyeFireCheck()
	{
		yield return new WaitForSeconds(0.1f);
		if (this.EyeFireCount > 0)
		{
			this.EyeFireCount--;
			this.EyeFire();
			base.StartCoroutine(this.EyeFireCheck());
		}
		else
		{
			this.SkillProcess = false;
			this.mGGNetWorkAIProperty.mSkillIndex = 0;
		}
		yield break;
	}

	// Token: 0x06000F82 RID: 3970 RVA: 0x00085114 File Offset: 0x00083514
	private void EyeFire()
	{
		for (int i = 0; i < this.EyeFirepoint.Length; i++)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.Bullet, this.EyeFirepoint[i].position, this.EyeFirepoint[i].rotation);
			base.GetComponent<AudioSource>().clip = this.clips[0];
			base.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x06000F83 RID: 3971 RVA: 0x0008517E File Offset: 0x0008357E
	private void BloodAbsorbShield()
	{
		this.isBloodAbsorbShield = true;
		this.BloodShield.SetActive(true);
		this.SnailHPGenerate.Play();
		base.StartCoroutine(this.BloodAbsorbShieldDisappear());
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x000851AC File Offset: 0x000835AC
	private IEnumerator BloodAbsorbShieldDisappear()
	{
		yield return new WaitForSeconds(5f);
		this.BloodShield.SetActive(false);
		this.isBloodAbsorbShield = false;
		this.mGGNetWorkAIProperty.mSkillIndex = 0;
		yield break;
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x000851C8 File Offset: 0x000835C8
	private void SummonRecoverSnail()
	{
		base.GetComponent<AudioSource>().clip = this.clips[2];
		base.GetComponent<AudioSource>().Play();
		if (UnityEngine.Random.Range(0, 2) == 0)
		{
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingMode_RecoverSnail", new Vector3(24f, 0.9f, 24f), Quaternion.identity);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingMode_RecoverSnail", new Vector3(24f, 0.9f, -24f), Quaternion.identity);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingMode_RecoverSnail", new Vector3(-24f, 0.9f, 24f), Quaternion.identity);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingMode_RecoverSnail", new Vector3(-24f, 0.9f, -24f), Quaternion.identity);
		}
		else
		{
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingMode_RecoverSnail", new Vector3(24f, 0.9f, 0f), Quaternion.identity);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingMode_RecoverSnail", new Vector3(0f, 0.9f, 24f), Quaternion.identity);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingMode_RecoverSnail", new Vector3(-24f, 0.9f, 0f), Quaternion.identity);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingMode_RecoverSnail", new Vector3(0f, 0.9f, -24f), Quaternion.identity);
		}
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x00085348 File Offset: 0x00083748
	private void AcidRain()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessagePlayerAcidRain;
		ggmessage.messageContent = new GGMessageContent();
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.All);
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x00085380 File Offset: 0x00083780
	private void SummonPoisonousSnail()
	{
		base.GetComponent<AudioSource>().clip = this.clips[2];
		base.GetComponent<AudioSource>().Play();
		GGNetworkKit.mInstance.CreateSeceneObject("HuntingMode_PoisonousSnail", new Vector3(UnityEngine.Random.Range(-24f, 24f), 0.9f, UnityEngine.Random.Range(-24f, 24f)), Quaternion.identity);
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x000853E8 File Offset: 0x000837E8
	private void PassiveSkillCheck()
	{
		if (this.skillEnable[6])
		{
			this.skillCheckTime[6] += Time.deltaTime;
			if (this.skillCheckTime[6] > this.skillCD[6])
			{
				if (this.SkillReleaseRate(6))
				{
					this.PassiveSkill();
				}
				this.skillCheckTime[6] = 0f;
			}
		}
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x0008544C File Offset: 0x0008384C
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
					collider.transform.SendMessageUpwards("AutoMove", normalized * 5f + new Vector3(0f, 3f, 0f), SendMessageOptions.DontRequireReceiver);
					base.GetComponent<AudioSource>().clip = this.clips[1];
					base.GetComponent<AudioSource>().Play();
				}
			}
		}
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x00085598 File Offset: 0x00083998
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

	// Token: 0x06000F8B RID: 3979 RVA: 0x0008561C File Offset: 0x00083A1C
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

	// Token: 0x06000F8C RID: 3980 RVA: 0x000856A0 File Offset: 0x00083AA0
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

	// Token: 0x06000F8D RID: 3981 RVA: 0x00085738 File Offset: 0x00083B38
	public void AIDamaged(GGDamageEventArgs mdamageEventArgs)
	{
		mdamageEventArgs.id = this.mPhotonView.viewID;
		GGNetworkKit.mInstance.DamageToAI(mdamageEventArgs, this.mPhotonView);
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x0008575C File Offset: 0x00083B5C
	public void Event_Damage_AI(GGDamageEventArgs damageEventArgs)
	{
		if (damageEventArgs.id == this.mPhotonView.viewID)
		{
			int damage = (int)damageEventArgs.damage;
			if (!this.isBloodAbsorbShield)
			{
				this.DecreaseBlood(damage);
			}
			else
			{
				this.IncreaseBlood(damage);
				GGNetworkKit.mInstance.CreateSeceneObject("HuntingMode_SnailHPGenerate", base.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
			}
		}
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x000857DC File Offset: 0x00083BDC
	public void DecreaseBlood(int bulletDamage)
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

	// Token: 0x06000F90 RID: 3984 RVA: 0x0008587C File Offset: 0x00083C7C
	public void IncreaseBlood(int bulletDamage)
	{
		if (!this.bDead && this.mGGNetWorkAIProperty.mBlood < this.maxBlood)
		{
			this.mGGNetWorkAIProperty.mBlood += bulletDamage;
			this.mGGNetWorkAIProperty.mBlood = Mathf.Min(this.mGGNetWorkAIProperty.mBlood, this.maxBlood);
		}
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x000858DE File Offset: 0x00083CDE
	public void playEnemyDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x06000F92 RID: 3986 RVA: 0x000858EB File Offset: 0x00083CEB
	public void playDieEffect()
	{
		GGNetworkKit.mInstance.CreateSeceneObject("boss_dead", base.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x00085925 File Offset: 0x00083D25
	public void playBulletHitAudio()
	{
	}

	// Token: 0x06000F94 RID: 3988 RVA: 0x00085928 File Offset: 0x00083D28
	private IEnumerator waitForSecondsToDestory(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x0008594C File Offset: 0x00083D4C
	private void PlayerHatredJudge()
	{
		this.CheckHatredTime += Time.deltaTime;
		if (this.CheckHatredTime > 16f)
		{
			if (GGNetworkKit.mInstance.IsMasterClient())
			{
				this.Player_HighHatred.Clear();
				this.RefreshTarget();
				foreach (GameObject gameObject in this.PlayersList)
				{
					GGNetworkCharacter component = gameObject.GetComponent<GGNetworkCharacter>();
					int damageNum = (int)component.mPlayerProperties.damageNum;
					if (damageNum > (int)((float)this.mGGNetWorkAIProperty.mBlood * 0.02f))
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

	// Token: 0x06000F96 RID: 3990 RVA: 0x00085A94 File Offset: 0x00083E94
	private void PlayerBloodJudge()
	{
		this.CheckBloodTime += Time.deltaTime;
		if (this.CheckBloodTime > 25f)
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

	// Token: 0x06000F97 RID: 3991 RVA: 0x00085BBC File Offset: 0x00083FBC
	private void onDestroy()
	{
		GGNetworkKit.mInstance.AIReceiveDamage -= this.Event_Damage_AI;
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x00085BD4 File Offset: 0x00083FD4
	public void SendBossKilledMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageHuntingModeBossKilled;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x0400114A RID: 4426
	private NavMeshAgent mNavMeshAgent;

	// Token: 0x0400114B RID: 4427
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x0400114C RID: 4428
	private GGNetWorkAISeeker mGGNetWorkAISeeker;

	// Token: 0x0400114D RID: 4429
	private GGNetWorkAIProperty mGGNetWorkAIProperty;

	// Token: 0x0400114E RID: 4430
	private GGNetworkCharacter targetNetworkCharacter;

	// Token: 0x0400114F RID: 4431
	private PhotonView mPhotonView;

	// Token: 0x04001150 RID: 4432
	public bool bDead;

	// Token: 0x04001151 RID: 4433
	public bool preState;

	// Token: 0x04001152 RID: 4434
	private AudioSource audioEnemyDie;

	// Token: 0x04001153 RID: 4435
	public int mPreSkillIndex;

	// Token: 0x04001154 RID: 4436
	private float logicTime;

	// Token: 0x04001155 RID: 4437
	public Dictionary<int, GameObject> Players;

	// Token: 0x04001156 RID: 4438
	public List<GameObject> PlayersList = new List<GameObject>();

	// Token: 0x04001157 RID: 4439
	private List<GameObject> Player_HighHatred = new List<GameObject>();

	// Token: 0x04001158 RID: 4440
	private List<GameObject> Player_LowBlood = new List<GameObject>();

	// Token: 0x04001159 RID: 4441
	private float checkCountTime;

	// Token: 0x0400115A RID: 4442
	private int EyeFireCount = 16;

	// Token: 0x0400115B RID: 4443
	public Transform[] EyeFirepoint;

	// Token: 0x0400115C RID: 4444
	public Transform Bullet;

	// Token: 0x0400115D RID: 4445
	public AudioClip[] clips;

	// Token: 0x0400115E RID: 4446
	private bool SkillProcess;

	// Token: 0x0400115F RID: 4447
	private float CheckTargetTime;

	// Token: 0x04001160 RID: 4448
	private float CheckHatredTime;

	// Token: 0x04001161 RID: 4449
	private float CheckBloodTime;

	// Token: 0x04001162 RID: 4450
	public bool[] skillEnable = new bool[7];

	// Token: 0x04001163 RID: 4451
	public float[] skillCD = new float[7];

	// Token: 0x04001164 RID: 4452
	public float[] skillCheckTime = new float[7];

	// Token: 0x04001165 RID: 4453
	public int[] skillReleaseRate = new int[7];

	// Token: 0x04001166 RID: 4454
	public int maxBlood = 20000;

	// Token: 0x04001167 RID: 4455
	private bool isBloodAbsorbShield;

	// Token: 0x04001168 RID: 4456
	public GameObject BloodShield;

	// Token: 0x04001169 RID: 4457
	public ParticleSystem SnailHPGenerate;

	// Token: 0x0400116A RID: 4458
	public Transform BossDeadEffect;

	// Token: 0x0400116B RID: 4459
	public GameObject gravityField;
}
