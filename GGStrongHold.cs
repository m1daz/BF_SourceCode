using System;
using UnityEngine;

// Token: 0x02000251 RID: 593
public class GGStrongHold : MonoBehaviour
{
	// Token: 0x0600111D RID: 4381 RVA: 0x00097DAC File Offset: 0x000961AC
	private void Start()
	{
		this.mNetworkCharacter = GameObject.FindWithTag("Player").GetComponent<GGNetworkCharacter>();
		this.mNetWorkPlayerlogic = GameObject.FindWithTag("Player").GetComponent<GGNetWorkPlayerlogic>();
		this.ScheduleSprite = UIPlayDirector.mInstance.strongholdProgressBarObj.GetComponent<UISprite>();
		this.ScheduleSprite.fillAmount = 0f;
		this.curType = this.mNetworkCharacter.mPlayerProperties.team;
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x00097E1E File Offset: 0x0009621E
	private void OnDisable()
	{
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x00097E20 File Offset: 0x00096220
	private void Update()
	{
		if (this.mNetworkCharacter == null)
		{
			if (Time.frameCount % 32 != 0)
			{
				return;
			}
			if (GameObject.FindWithTag("Player") != null)
			{
				this.mNetworkCharacter = GameObject.FindWithTag("Player").GetComponent<GGNetworkCharacter>();
				this.mNetWorkPlayerlogic = GameObject.FindWithTag("Player").GetComponent<GGNetWorkPlayerlogic>();
				this.curType = this.mNetworkCharacter.mPlayerProperties.team;
			}
		}
		if (this.mNetworkCharacter.mCharacterWalkState == GGCharacterWalkState.Dead)
		{
			this.showSchedule1 = false;
			this.showSchedule2 = false;
			this.showSchedule3 = false;
			this.ScheduleSprite.fillAmount = 0f;
		}
		if (this.curType != this.mNetworkCharacter.mPlayerProperties.team)
		{
			this.curType = this.mNetworkCharacter.mPlayerProperties.team;
			this.showSchedule1 = false;
			this.showSchedule2 = false;
			this.showSchedule3 = false;
			this.ScheduleSprite.fillAmount = 0f;
		}
		if (this.preHoldState != this.holdState)
		{
			if (this.holdState == GGStrondholdState.activate)
			{
				this.StrongholdActiveEffect.Play();
				this.StrongholdGetEffect.SetActive(false);
				this.StrongholdGetEffectTopObj.GetComponent<ParticleSystem>().Stop();
			}
			else if (this.holdState == GGStrondholdState.unactivate)
			{
				this.showSchedule1 = false;
				this.showSchedule2 = false;
				this.showSchedule3 = false;
				this.ScheduleSprite.fillAmount = 0f;
				this.StrongholdActiveEffect.Stop();
				this.StrongholdGetEffect.SetActive(false);
				this.StrongholdGetEffectTopObj.GetComponent<ParticleSystem>().Stop();
			}
			else if (this.holdState == GGStrondholdState.BlueOccupation || this.holdState == GGStrondholdState.RedOccupation)
			{
				this.StrongholdGetEffect.SetActive(true);
				this.StrongholdGetEffectTopObj.GetComponent<ParticleSystem>().Play();
				base.GetComponent<AudioSource>().clip = this.StrongholdGetClip;
				base.GetComponent<AudioSource>().Play();
			}
			if (this.holdStateCD)
			{
				this.showSchedule1 = false;
				this.showSchedule2 = false;
				this.showSchedule3 = false;
				this.ScheduleSprite.fillAmount = 0f;
			}
			this.preHoldState = this.holdState;
		}
		if (this.showSchedule1)
		{
			this.ScheduleSprite.fillAmount += Time.deltaTime * 0.33f;
			if (this.ScheduleSprite.fillAmount >= 1f)
			{
				this.showSchedule1 = false;
				this.ScheduleSprite.fillAmount = 0f;
				if ((this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red && (this.holdState == GGStrondholdState.BlueOccupation || this.holdState == GGStrondholdState.activate)) || (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue && (this.holdState == GGStrondholdState.RedOccupation || this.holdState == GGStrondholdState.activate)))
				{
					this.mNetWorkPlayerlogic.SendStrongholdGetMessage(1, this.mNetworkCharacter.mPlayerProperties.id);
					GGNetworkPlayerProperties mPlayerProperties = this.mNetworkCharacter.mPlayerProperties;
					mPlayerProperties.strongholdGetNum += 1;
				}
				this.timeLimit = 30;
			}
		}
		if (this.showSchedule2)
		{
			this.ScheduleSprite.fillAmount += Time.deltaTime * 0.33f;
			if (this.ScheduleSprite.fillAmount >= 1f)
			{
				this.showSchedule2 = false;
				this.ScheduleSprite.fillAmount = 0f;
				if ((this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red && (this.holdState == GGStrondholdState.BlueOccupation || this.holdState == GGStrondholdState.activate)) || (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue && (this.holdState == GGStrondholdState.RedOccupation || this.holdState == GGStrondholdState.activate)))
				{
					this.mNetWorkPlayerlogic.SendStrongholdGetMessage(2, this.mNetworkCharacter.mPlayerProperties.id);
					GGNetworkPlayerProperties mPlayerProperties2 = this.mNetworkCharacter.mPlayerProperties;
					mPlayerProperties2.strongholdGetNum += 1;
				}
				this.timeLimit = 30;
			}
		}
		if (this.showSchedule3)
		{
			this.ScheduleSprite.fillAmount += Time.deltaTime * 0.33f;
			if (this.ScheduleSprite.fillAmount >= 1f)
			{
				this.showSchedule3 = false;
				this.ScheduleSprite.fillAmount = 0f;
				if ((this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red && (this.holdState == GGStrondholdState.BlueOccupation || this.holdState == GGStrondholdState.activate)) || (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue && (this.holdState == GGStrondholdState.RedOccupation || this.holdState == GGStrondholdState.activate)))
				{
					this.mNetWorkPlayerlogic.SendStrongholdGetMessage(3, this.mNetworkCharacter.mPlayerProperties.id);
					GGNetworkPlayerProperties mPlayerProperties3 = this.mNetworkCharacter.mPlayerProperties;
					mPlayerProperties3.strongholdGetNum += 1;
				}
				this.timeLimit = 30;
			}
		}
		if (this.timeLimit > 0)
		{
			this.timeLimit--;
		}
		this.ChangeStrongholdColor();
		if (this.preHoldStateCD != this.holdStateCD)
		{
			if (this.holdStateCD)
			{
				this.StrongholdCDEffect.Play();
			}
			this.preHoldStateCD = this.holdStateCD;
		}
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x0009837C File Offset: 0x0009677C
	private void OnTriggerEnter(Collider other)
	{
		if (this.holdState == GGStrondholdState.unactivate || this.holdStateCD)
		{
			return;
		}
		if (this.timeLimit == 0 && other.gameObject.tag == "Player")
		{
			if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
			{
				if (this.holdState == GGStrondholdState.RedOccupation || this.holdState == GGStrondholdState.activate)
				{
					if (this.Id == 1)
					{
						this.showSchedule1 = true;
					}
					else if (this.Id == 2)
					{
						this.showSchedule2 = true;
					}
					else if (this.Id == 3)
					{
						this.showSchedule3 = true;
					}
					base.GetComponent<AudioSource>().clip = this.ProceedingClip;
					base.GetComponent<AudioSource>().Play();
				}
			}
			else if (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red && (this.holdState == GGStrondholdState.BlueOccupation || this.holdState == GGStrondholdState.activate))
			{
				if (this.Id == 1)
				{
					this.showSchedule1 = true;
				}
				else if (this.Id == 2)
				{
					this.showSchedule2 = true;
				}
				else if (this.Id == 3)
				{
					this.showSchedule3 = true;
				}
				base.GetComponent<AudioSource>().clip = this.ProceedingClip;
				base.GetComponent<AudioSource>().Play();
			}
		}
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x000984E4 File Offset: 0x000968E4
	private void OnTriggerExit(Collider other)
	{
		if (this.Id == 1)
		{
			if (other.gameObject.tag == "Player" && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
			{
				this.ScheduleSprite.fillAmount = 0f;
				this.showSchedule1 = false;
			}
			if (other.gameObject.tag == "Player" && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
			{
				this.ScheduleSprite.fillAmount = 0f;
				this.showSchedule1 = false;
			}
		}
		else if (this.Id == 2)
		{
			if (other.gameObject.tag == "Player" && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
			{
				this.ScheduleSprite.fillAmount = 0f;
				this.showSchedule2 = false;
			}
			if (other.gameObject.tag == "Player" && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
			{
				this.ScheduleSprite.fillAmount = 0f;
				this.showSchedule2 = false;
			}
		}
		else if (this.Id == 3)
		{
			if (other.gameObject.tag == "Player" && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue)
			{
				this.ScheduleSprite.fillAmount = 0f;
				this.showSchedule3 = false;
			}
			if (other.gameObject.tag == "Player" && this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red)
			{
				this.ScheduleSprite.fillAmount = 0f;
				this.showSchedule3 = false;
			}
		}
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x000986C8 File Offset: 0x00096AC8
	private void ChangeStrongholdColor()
	{
		if ((this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue && this.holdState == GGStrondholdState.BlueOccupation) || (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red && this.holdState == GGStrondholdState.RedOccupation))
		{
			if (!this.StrongholdGetEffectButtomObj.GetComponent<Renderer>().material.name.Contains(this.strongholdMaterials_Green[0].name))
			{
				this.StrongholdGetEffectButtomObj.GetComponent<Renderer>().material = this.strongholdMaterials_Green[0];
			}
			if (!this.StrongholdGetEffectTopObj.GetComponent<Renderer>().material.name.Contains(this.strongholdMaterials_Green[1].name))
			{
				this.StrongholdGetEffectTopObj.GetComponent<Renderer>().material = this.strongholdMaterials_Green[1];
			}
			if (!this.StrongholdFlag.GetComponent<Renderer>().material.name.Contains(this.strongholdMaterials_Green[2].name))
			{
				this.StrongholdFlag.GetComponent<Renderer>().material = this.strongholdMaterials_Green[2];
			}
		}
		else if ((this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.blue && this.holdState == GGStrondholdState.RedOccupation) || (this.mNetworkCharacter.mPlayerProperties.team == GGTeamType.red && this.holdState == GGStrondholdState.BlueOccupation))
		{
			if (!this.StrongholdGetEffectButtomObj.GetComponent<Renderer>().material.name.Contains(this.strongholdMaterials_Red[0].name))
			{
				this.StrongholdGetEffectButtomObj.GetComponent<Renderer>().material = this.strongholdMaterials_Red[0];
			}
			if (!this.StrongholdGetEffectTopObj.GetComponent<Renderer>().material.name.Contains(this.strongholdMaterials_Red[1].name))
			{
				this.StrongholdGetEffectTopObj.GetComponent<Renderer>().material = this.strongholdMaterials_Red[1];
			}
			if (!this.StrongholdFlag.GetComponent<Renderer>().material.name.Contains(this.strongholdMaterials_Red[2].name))
			{
				this.StrongholdFlag.GetComponent<Renderer>().material = this.strongholdMaterials_Red[2];
			}
		}
		else if ((this.holdState == GGStrondholdState.activate || this.holdState == GGStrondholdState.unactivate) && !this.StrongholdFlag.GetComponent<Renderer>().material.name.Contains(this.strongholdMaterials_Grey[0].name))
		{
			this.StrongholdFlag.GetComponent<Renderer>().material = this.strongholdMaterials_Grey[0];
		}
	}

	// Token: 0x0400138D RID: 5005
	public int Id;

	// Token: 0x0400138E RID: 5006
	public GGStrondholdState holdState = GGStrondholdState.unactivate;

	// Token: 0x0400138F RID: 5007
	public GGStrondholdState preHoldState = GGStrondholdState.unactivate;

	// Token: 0x04001390 RID: 5008
	public bool holdStateCD;

	// Token: 0x04001391 RID: 5009
	public bool preHoldStateCD;

	// Token: 0x04001392 RID: 5010
	private int timeLimit;

	// Token: 0x04001393 RID: 5011
	private bool showSchedule1;

	// Token: 0x04001394 RID: 5012
	private bool showSchedule2;

	// Token: 0x04001395 RID: 5013
	private bool showSchedule3;

	// Token: 0x04001396 RID: 5014
	private UISprite ScheduleSprite;

	// Token: 0x04001397 RID: 5015
	public Material[] strongholdMaterials_Green;

	// Token: 0x04001398 RID: 5016
	public Material[] strongholdMaterials_Red;

	// Token: 0x04001399 RID: 5017
	public Material[] strongholdMaterials_Grey;

	// Token: 0x0400139A RID: 5018
	public GameObject StrongholdGetEffectButtomObj;

	// Token: 0x0400139B RID: 5019
	public GameObject StrongholdGetEffectTopObj;

	// Token: 0x0400139C RID: 5020
	public GameObject StrongholdFlag;

	// Token: 0x0400139D RID: 5021
	private GGTeamType curType;

	// Token: 0x0400139E RID: 5022
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x0400139F RID: 5023
	private GGNetWorkPlayerlogic mNetWorkPlayerlogic;

	// Token: 0x040013A0 RID: 5024
	public ParticleSystem StrongholdCDEffect;

	// Token: 0x040013A1 RID: 5025
	public ParticleSystem StrongholdActiveEffect;

	// Token: 0x040013A2 RID: 5026
	public GameObject StrongholdGetEffect;

	// Token: 0x040013A3 RID: 5027
	public AudioClip StrongholdGetClip;

	// Token: 0x040013A4 RID: 5028
	public AudioClip TowerRotateClip;

	// Token: 0x040013A5 RID: 5029
	public AudioClip ProceedingClip;

	// Token: 0x040013A6 RID: 5030
	private float TowerRotateTime;

	// Token: 0x040013A7 RID: 5031
	private float TowerRotateTime1;

	// Token: 0x040013A8 RID: 5032
	private bool TowerRotate;

	// Token: 0x040013A9 RID: 5033
	public GameObject TowerRotateObj;
}
