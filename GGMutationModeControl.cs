using System;
using UnityEngine;

// Token: 0x02000244 RID: 580
public class GGMutationModeControl : MonoBehaviour
{
	// Token: 0x0600105B RID: 4187 RVA: 0x0008BE8C File Offset: 0x0008A28C
	private void Start()
	{
		GGMutationModeControl.mInstance = this;
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			this.isMutationMode = true;
		}
		this.mGlobalInfo = GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo();
		this.StartCountDownMutation = UIPlayDirector.mInstance.startCountDownMutationObj;
		this.InitMyNetworkCharacter();
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x0008BEE4 File Offset: 0x0008A2E4
	private void InitMyNetworkCharacter()
	{
		if (this.mNetworkCharacter == null)
		{
			GameObject gameObject = GameObject.FindWithTag("Player");
			if (gameObject != null)
			{
				this.mNetworkCharacter = gameObject.GetComponent<GGNetworkCharacter>();
			}
		}
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x0008BF28 File Offset: 0x0008A328
	private void Update()
	{
		if (this.isMutationMode)
		{
			this.InitMyNetworkCharacter();
			if (Time.frameCount % 5 == 0)
			{
				int num = this.totalDamage / 1000;
				if (this.preDamage < 1000 * num && this.totalDamage >= 1000 * num)
				{
					this.score += 20;
					GrowthManagerKit.GenGrowthPromptEvent(GrowthPrometType.ScoreInMutation, 20, string.Empty);
					if (this.mNetworkCharacter != null)
					{
						this.mNetworkCharacter.mPlayerProperties.MutationModeScore = GGMutationModeControl.mInstance.score;
					}
				}
				this.preDamage = this.totalDamage;
			}
			this.mGlobalInfo = GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo();
			if (this.mGlobalInfo == null)
			{
				return;
			}
			this.survivalTimer = this.mGlobalInfo.modeInfo.survivalTimer;
			this.humanNum = this.mGlobalInfo.modeInfo.humanNum;
			this.zombieNum = this.mGlobalInfo.modeInfo.zombieNum;
			this.isGotoGameScene = this.mGlobalInfo.modeInfo.isGotoGameScene;
			if (this.isShow5sMutationTimer)
			{
				this.Show5sMutationTimer();
			}
			if (this.mGlobalInfo.modeInfo.isStartMutationTimer)
			{
				this.isShow5sMutationTimer = true;
			}
			else if (this.isShow5sMutationTimer)
			{
				this.isShow5sMutationTimer = false;
			}
		}
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x0008C09B File Offset: 0x0008A49B
	private void OnDestroy()
	{
		GGMutationModeControl.mInstance = null;
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x0008C0A4 File Offset: 0x0008A4A4
	public void reset()
	{
		this.zombielv = 0;
		this.score = 1000;
		this.totalDamage = 0;
		this.survivalTimer = 600;
		this.humanNum = 0;
		this.zombieNum = 0;
		this.preDamage = 0;
		this.isShow5sMutationTimer = false;
		this.InitMyNetworkCharacter();
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x0008C0F8 File Offset: 0x0008A4F8
	private void Show5sMutationTimer()
	{
		if (this.mGlobalInfo.modeInfo.MutationTimer == 5)
		{
			this.StartCountDownMutation.SetActive(true);
			this.StartCountDownMutation.GetComponent<UILabel>().text = "5";
		}
		if (this.mGlobalInfo.modeInfo.MutationTimer == 4)
		{
			this.StartCountDownMutation.GetComponent<UILabel>().text = "4";
		}
		if (this.mGlobalInfo.modeInfo.MutationTimer == 3)
		{
			this.StartCountDownMutation.GetComponent<UILabel>().text = "3";
		}
		if (this.mGlobalInfo.modeInfo.MutationTimer == 2)
		{
			this.StartCountDownMutation.GetComponent<UILabel>().text = "2";
		}
		if (this.mGlobalInfo.modeInfo.MutationTimer == 1)
		{
			this.StartCountDownMutation.GetComponent<UILabel>().text = "1";
		}
		if (this.mGlobalInfo.modeInfo.MutationTimer == 0)
		{
			this.StartCountDownMutation.GetComponent<UILabel>().text = " ";
			this.StartCountDownMutation.SetActive(false);
			this.isShow5sMutationTimer = false;
		}
	}

	// Token: 0x0400127B RID: 4731
	public static GGMutationModeControl mInstance;

	// Token: 0x0400127C RID: 4732
	private GGNetworkGlobalInfo mGlobalInfo;

	// Token: 0x0400127D RID: 4733
	public int zombielv;

	// Token: 0x0400127E RID: 4734
	public short score = 1000;

	// Token: 0x0400127F RID: 4735
	public int totalDamage;

	// Token: 0x04001280 RID: 4736
	public int survivalTimer = 600;

	// Token: 0x04001281 RID: 4737
	public int humanNum;

	// Token: 0x04001282 RID: 4738
	public int zombieNum;

	// Token: 0x04001283 RID: 4739
	private int preDamage;

	// Token: 0x04001284 RID: 4740
	private bool isMutationMode;

	// Token: 0x04001285 RID: 4741
	private GameObject StartCountDownMutation;

	// Token: 0x04001286 RID: 4742
	private bool isShow5sMutationTimer;

	// Token: 0x04001287 RID: 4743
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x04001288 RID: 4744
	public bool isGotoGameScene;

	// Token: 0x02000245 RID: 581
	public class ZombieProperty
	{
		// Token: 0x06001062 RID: 4194 RVA: 0x0008C22D File Offset: 0x0008A62D
		public static int GetMaxBloodWithLv(int lv)
		{
			return (lv < 1 || lv > 6) ? 0 : GGMutationModeControl.ZombieProperty.maxBlood[lv - 1];
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0008C24C File Offset: 0x0008A64C
		public static int GetbloodRecoverSpeedWithLv(int lv)
		{
			return (lv < 1 || lv > 6) ? 0 : GGMutationModeControl.ZombieProperty.bloodRecoverSpeed[lv - 1];
		}

		// Token: 0x04001289 RID: 4745
		public static readonly int[] maxBlood = new int[]
		{
			100,
			200,
			300,
			400,
			500,
			4000
		};

		// Token: 0x0400128A RID: 4746
		public static readonly int[] bloodRecoverSpeed = new int[]
		{
			5,
			5,
			5,
			5,
			10,
			20
		};
	}
}
