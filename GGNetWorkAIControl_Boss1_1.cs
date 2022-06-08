using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x0200022D RID: 557
public class GGNetWorkAIControl_Boss1_1 : MonoBehaviour
{
	// Token: 0x06000F35 RID: 3893 RVA: 0x00081CC4 File Offset: 0x000800C4
	private void Start()
	{
		this.mGlobalInfo = GGNetworkManageGlobalInfo.mInstance.mGlobalInfo;
		this.mNavMeshAgent = base.GetComponent<NavMeshAgent>();
		this.mGGNetWorkAISeeker = base.GetComponent<GGNetWorkAISeeker>();
		this.mGGNetWorkAIProperty = base.GetComponent<GGNetWorkAIProperty>();
		this.mPhotonView = base.GetComponent<PhotonView>();
		this.audioEnemyDie = base.GetComponent<AudioSource>();
		this.SetTarget();
		GGNetworkKit.mInstance.AIReceiveDamage += this.Event_Damage_AI;
		this.DifficultyInit();
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x00081D40 File Offset: 0x00080140
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
			if (this.mGGNetWorkAISeeker.target != null)
			{
				this.AILogic();
				this.IsCanSkill();
			}
			this.CheckTarget();
			if (this.mNavMeshAgent.velocity == Vector3.zero && this.mGGNetWorkAISeeker.target != null && !this.SkillProcess)
			{
				Quaternion b = Quaternion.LookRotation(this.mGGNetWorkAISeeker.target.transform.position - base.transform.position, Vector3.up);
				base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, 5f * Time.deltaTime);
			}
		}
		this.PassiveSkillCheck();
		this.PlayerHatredJudge();
		this.PlayerBloodJudge();
		if (this.isRun)
		{
			this.TK_1.mainTextureOffset += new Vector2(0f, 0.005f);
		}
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x00081EF0 File Offset: 0x000802F0
	public void DifficultyInit()
	{
		int difficultySet = GGNetWorkAIDifficultyControl.mInstance.difficultySet;
		int maxPlayerSet = GGNetWorkAIDifficultyControl.mInstance.maxPlayerSet;
		int count = GGNetworkKit.mInstance.GetPlayerGameObjectList().Count;
		if (difficultySet == 1)
		{
			this.skillCD = new float[]
			{
				3f,
				5f,
				10f,
				31f,
				2f,
				2f
			};
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
		}
		else if (difficultySet == 2)
		{
			this.skillCD = new float[]
			{
				3f,
				5f,
				10f,
				31f,
				2f,
				2f
			};
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
		}
		if (count == 1)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(0.3f * (float)this.mGGNetWorkAIProperty.mBlood);
			this.MissleCount = 5;
		}
		else if (count == 2)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(0.5f * (float)this.mGGNetWorkAIProperty.mBlood);
			this.MissleCount = 6;
		}
		else if (count == 3)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(0.8f * (float)this.mGGNetWorkAIProperty.mBlood);
			this.MissleCount = 6;
		}
		else if (count == 4)
		{
			this.mGGNetWorkAIProperty.mBlood = (int)(1f * (float)this.mGGNetWorkAIProperty.mBlood);
			this.MissleCount = 6;
		}
		this.maxBlood = this.mGGNetWorkAIProperty.mBlood;
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

	// Token: 0x06000F38 RID: 3896 RVA: 0x000820C4 File Offset: 0x000804C4
	private void StateChange(int SkillIndex)
	{
		switch (SkillIndex)
		{
		case 0:
			this.mNavMeshAgent.speed = 3.5f;
			this.mAnimator.SetBool("Move", true);
			this.isRun = true;
			break;
		case 1:
			this.mNavMeshAgent.speed = 0f;
			this.mAnimator.SetBool("Move", false);
			this.isRun = false;
			this.SkillProcess = true;
			this.PaoFireCount = 1;
			base.StartCoroutine(this.BatteryFireCheck());
			break;
		case 2:
			this.mNavMeshAgent.speed = 0f;
			this.mAnimator.SetBool("Move", false);
			this.mAnimator.SetBool("Fire_Gun", true);
			this.isRun = false;
			this.SkillProcess = true;
			this.QiangFireCount = 15;
			base.StartCoroutine(this.GunFireCheck());
			break;
		}
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x000821BC File Offset: 0x000805BC
	private void AILogic()
	{
		if (!this.SkillProcess)
		{
			this.logicTime += Time.deltaTime;
			if (this.logicTime > 1f)
			{
				if (this.mGGNetWorkAISeeker.target != null && Vector3.Distance(base.transform.position, this.mGGNetWorkAISeeker.target.transform.position) > 18f)
				{
					this.mGGNetWorkAIProperty.mSkillIndex = 0;
				}
				this.logicTime = 0f;
			}
		}
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x00082254 File Offset: 0x00080654
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
					this.MissleFireCheck();
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
					this.SummonSoldier();
				}
				this.skillCheckTime[3] = 0f;
			}
		}
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x000823C8 File Offset: 0x000807C8
	private bool SkillReleaseRate(int index)
	{
		int num = UnityEngine.Random.Range(0, 100);
		return num < this.skillReleaseRate[index];
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x000823F8 File Offset: 0x000807F8
	private IEnumerator BatteryFireCheck()
	{
		this.BatteryPluse();
		this.SetGroundFlag_Pao();
		yield return new WaitForSeconds(1f);
		if (this.PaoFireCount > 0)
		{
			this.PaoFireCount--;
			this.BatteryFire();
			base.StartCoroutine(this.BatteryFireCheck());
		}
		else
		{
			this.SkillProcess = false;
		}
		yield break;
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x00082413 File Offset: 0x00080813
	private void BatteryPluse()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.PaoPluse, this.PaoFirepoint.position, Quaternion.identity);
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x00082434 File Offset: 0x00080834
	private void SetGroundFlag_Pao()
	{
		Vector3 position = this.PaoFirepoint.position + this.PaoFirepoint.TransformDirection(Vector3.forward).normalized * 15.5f - new Vector3(0f, this.PaoFirepoint.position.y - 0.4f, 0f);
		UnityEngine.Object.Instantiate<GameObject>(this.GroundFlag_Pao, position, Quaternion.identity);
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x000824B4 File Offset: 0x000808B4
	private void BatteryFire()
	{
		Rigidbody rigidbody = UnityEngine.Object.Instantiate<Rigidbody>(this.Pao, this.PaoFirepoint.position, Quaternion.identity);
		rigidbody.velocity = base.transform.TransformDirection(new Vector3(0f, 4f, 15f));
		base.GetComponent<AudioSource>().clip = this.clips[0];
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x00082520 File Offset: 0x00080920
	private IEnumerator GunFireCheck()
	{
		yield return new WaitForSeconds(0.1f);
		if (this.QiangFireCount > 0)
		{
			this.QiangFireCount--;
			this.GunFire();
			base.StartCoroutine(this.GunFireCheck());
		}
		else
		{
			this.SkillProcess = false;
			this.mAnimator.SetBool("Fire_Gun", false);
		}
		yield break;
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x0008253C File Offset: 0x0008093C
	private void GunFire()
	{
		for (int i = 0; i < this.QiangFirepoint.Length; i++)
		{
			Transform transform = UnityEngine.Object.Instantiate<Transform>(this.Bullet, this.QiangFirepoint[i].position, this.QiangFirepoint[i].rotation);
			base.GetComponent<AudioSource>().clip = this.clips[1];
			base.GetComponent<AudioSource>().Play();
		}
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x000825A6 File Offset: 0x000809A6
	private void MissleFireCheck()
	{
		this.MissleFire();
		this.SkillProcess = false;
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x000825B8 File Offset: 0x000809B8
	private void MissleFire()
	{
		for (int i = 0; i < this.MissleCount; i++)
		{
			Vector3 vector = new Vector3(UnityEngine.Random.Range(-this.maxWidth, this.maxWidth), 15f, UnityEngine.Random.Range(-this.maxLength, this.maxLength));
			GGNetworkKit.mInstance.CreateSeceneObject("Missile_AI", vector + new Vector3(0f, 15f, 0f), Quaternion.Euler(Vector3.down));
			GGNetworkKit.mInstance.CreateSeceneObject("Missle_Effect", vector, Quaternion.identity);
			GGNetworkKit.mInstance.CreateSeceneObject("Flag_missle", new Vector3(vector.x, 0.42f, vector.z), Quaternion.identity);
			if (this.skillEnable[4])
			{
				base.StartCoroutine(this.FlameTrail(vector));
			}
		}
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x0008269C File Offset: 0x00080A9C
	private void SummonSoldier()
	{
		base.GetComponent<AudioSource>().clip = this.clips[2];
		base.GetComponent<AudioSource>().Play();
		switch (this.Players.Count)
		{
		case 1:
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_1", base.transform.position + new Vector3(-2f, -0.37f, -1f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_2", base.transform.position + new Vector3(-2f, -0.37f, 1f), base.transform.rotation);
			break;
		case 2:
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_1", base.transform.position + new Vector3(-2f, -0.37f, -1f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_1", base.transform.position + new Vector3(-2f, -0.37f, 1f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_2", base.transform.position + new Vector3(2f, -0.37f, 1f), base.transform.rotation);
			break;
		case 3:
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_1", base.transform.position + new Vector3(-2f, -0.37f, -1f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_1", base.transform.position + new Vector3(-2f, -0.37f, 1f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_2", base.transform.position + new Vector3(2f, -0.37f, -1f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_2", base.transform.position + new Vector3(2f, -0.37f, 1f), base.transform.rotation);
			break;
		case 4:
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_1", base.transform.position + new Vector3(-2f, -0.37f, -1f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_1", base.transform.position + new Vector3(-2f, -0.37f, 1f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_1", base.transform.position + new Vector3(2f, -0.37f, -1f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_2", base.transform.position + new Vector3(2f, -0.37f, 1f), base.transform.rotation);
			GGNetworkKit.mInstance.CreateSeceneObject("HuntingModeSoldier1_2", base.transform.position + new Vector3(2f, -0.37f, 2f), base.transform.rotation);
			break;
		}
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x00082A70 File Offset: 0x00080E70
	private IEnumerator FlameTrail(Vector3 position)
	{
		yield return new WaitForSeconds(3f);
		GGNetworkKit.mInstance.CreateSeceneObject("FlameTrail_HuntingMode", position - new Vector3(0f, 14f, 0f), base.transform.rotation);
		yield break;
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x00082A94 File Offset: 0x00080E94
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

	// Token: 0x06000F47 RID: 3911 RVA: 0x00082AF8 File Offset: 0x00080EF8
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
					ggdamageEventArgs.damage = (short)UnityEngine.Random.Range(50, 80);
					collider.transform.SendMessageUpwards("Event_Damage", ggdamageEventArgs, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x00082B80 File Offset: 0x00080F80
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

	// Token: 0x06000F49 RID: 3913 RVA: 0x00082C04 File Offset: 0x00081004
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

	// Token: 0x06000F4A RID: 3914 RVA: 0x00082C88 File Offset: 0x00081088
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

	// Token: 0x06000F4B RID: 3915 RVA: 0x00082D20 File Offset: 0x00081120
	public void AIDamaged(GGDamageEventArgs mdamageEventArgs)
	{
		mdamageEventArgs.id = this.mPhotonView.viewID;
		GGNetworkKit.mInstance.DamageToAI(mdamageEventArgs, this.mPhotonView);
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x00082D44 File Offset: 0x00081144
	public void Event_Damage_AI(GGDamageEventArgs damageEventArgs)
	{
		if (damageEventArgs.id == this.mPhotonView.viewID)
		{
			int damage = (int)damageEventArgs.damage;
			this.decreaseBlood(damage);
		}
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x00082D78 File Offset: 0x00081178
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

	// Token: 0x06000F4E RID: 3918 RVA: 0x00082E16 File Offset: 0x00081216
	public void playEnemyDieAudio()
	{
		this.audioEnemyDie.Play();
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x00082E23 File Offset: 0x00081223
	public void playDieEffect()
	{
		GGNetworkKit.mInstance.CreateSeceneObject("boss_dead", base.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x00082E5D File Offset: 0x0008125D
	public void playBulletHitAudio()
	{
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x00082E60 File Offset: 0x00081260
	private IEnumerator waitForSecondsToDestory(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		GGNetworkKit.mInstance.DestorySceneObject(base.gameObject);
		yield break;
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x00082E84 File Offset: 0x00081284
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

	// Token: 0x06000F53 RID: 3923 RVA: 0x00082FCC File Offset: 0x000813CC
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

	// Token: 0x06000F54 RID: 3924 RVA: 0x000830F4 File Offset: 0x000814F4
	private void onDestroy()
	{
		GGNetworkKit.mInstance.AIReceiveDamage -= this.Event_Damage_AI;
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x0008310C File Offset: 0x0008150C
	public void SendBossKilledMessage()
	{
		GGMessage ggmessage = new GGMessage();
		ggmessage.messageType = GGMessageType.MessageHuntingModeBossKilled;
		GGNetworkKit.mInstance.SendMessage(ggmessage, GGTarget.MasterClient);
	}

	// Token: 0x040010F9 RID: 4345
	private NavMeshAgent mNavMeshAgent;

	// Token: 0x040010FA RID: 4346
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x040010FB RID: 4347
	private GGNetWorkAISeeker mGGNetWorkAISeeker;

	// Token: 0x040010FC RID: 4348
	private GGNetWorkAIProperty mGGNetWorkAIProperty;

	// Token: 0x040010FD RID: 4349
	private GGNetworkCharacter targetNetworkCharacter;

	// Token: 0x040010FE RID: 4350
	private PhotonView mPhotonView;

	// Token: 0x040010FF RID: 4351
	public bool bDead;

	// Token: 0x04001100 RID: 4352
	public bool preState;

	// Token: 0x04001101 RID: 4353
	private AudioSource audioEnemyDie;

	// Token: 0x04001102 RID: 4354
	public int mPreSkillIndex;

	// Token: 0x04001103 RID: 4355
	private float logicTime;

	// Token: 0x04001104 RID: 4356
	public Dictionary<int, GameObject> Players;

	// Token: 0x04001105 RID: 4357
	public List<GameObject> PlayersList = new List<GameObject>();

	// Token: 0x04001106 RID: 4358
	private List<GameObject> Player_HighHatred = new List<GameObject>();

	// Token: 0x04001107 RID: 4359
	private List<GameObject> Player_LowBlood = new List<GameObject>();

	// Token: 0x04001108 RID: 4360
	private float checkCountTime;

	// Token: 0x04001109 RID: 4361
	private int PaoFireCount = 1;

	// Token: 0x0400110A RID: 4362
	private int QiangFireCount = 15;

	// Token: 0x0400110B RID: 4363
	public Transform PaoFirepoint;

	// Token: 0x0400110C RID: 4364
	public Rigidbody Pao;

	// Token: 0x0400110D RID: 4365
	public GameObject PaoPluse;

	// Token: 0x0400110E RID: 4366
	public Transform[] QiangFirepoint;

	// Token: 0x0400110F RID: 4367
	public Transform Bullet;

	// Token: 0x04001110 RID: 4368
	public Transform[] MissleFirepoint;

	// Token: 0x04001111 RID: 4369
	public Rigidbody Missle;

	// Token: 0x04001112 RID: 4370
	public GameObject MissleEffect;

	// Token: 0x04001113 RID: 4371
	private int MissleCount = 8;

	// Token: 0x04001114 RID: 4372
	public AudioClip[] clips;

	// Token: 0x04001115 RID: 4373
	private bool SkillProcess;

	// Token: 0x04001116 RID: 4374
	private bool NeedRotateTo = true;

	// Token: 0x04001117 RID: 4375
	private float CheckTargetTime;

	// Token: 0x04001118 RID: 4376
	private float CheckHatredTime;

	// Token: 0x04001119 RID: 4377
	private float CheckBloodTime;

	// Token: 0x0400111A RID: 4378
	public bool[] skillEnable = new bool[6];

	// Token: 0x0400111B RID: 4379
	public float[] skillCD = new float[6];

	// Token: 0x0400111C RID: 4380
	public float[] skillCheckTime = new float[6];

	// Token: 0x0400111D RID: 4381
	public int[] skillReleaseRate = new int[6];

	// Token: 0x0400111E RID: 4382
	public Animator mAnimator;

	// Token: 0x0400111F RID: 4383
	public Material TK_1;

	// Token: 0x04001120 RID: 4384
	private bool isRun = true;

	// Token: 0x04001121 RID: 4385
	public GameObject GroundFlag_Pao;

	// Token: 0x04001122 RID: 4386
	public GameObject GroundFlag_Missle;

	// Token: 0x04001123 RID: 4387
	private float maxLength = 27f;

	// Token: 0x04001124 RID: 4388
	private float maxWidth = 27f;

	// Token: 0x04001125 RID: 4389
	public Transform BossDeadEffect;

	// Token: 0x04001126 RID: 4390
	public int maxBlood = 20000;
}
