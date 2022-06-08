using System;
using UnityEngine;

// Token: 0x02000295 RID: 661
public class UIModeDirector : MonoBehaviour
{
	// Token: 0x060012F0 RID: 4848 RVA: 0x000A9A85 File Offset: 0x000A7E85
	private void Awake()
	{
		UIModeDirector.mInstance = this;
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x000A9A8D File Offset: 0x000A7E8D
	private void OnDestroy()
	{
		MobileFPSInputController.isPause = false;
		if (UIModeDirector.mInstance != null)
		{
			UIModeDirector.mInstance = null;
		}
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x000A9AAB File Offset: 0x000A7EAB
	private void Start()
	{
		this.InitUI();
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x000A9AB4 File Offset: 0x000A7EB4
	private void Update()
	{
		if (!this.isDeathMatchMode)
		{
			this.updateTime += Time.deltaTime;
			if (this.updateTime >= this.updateTimerCycle)
			{
				this.updateTime = 0f;
				if (this.modeType == GGModeType.StrongHold)
				{
					this.UpdateFlagBarStrongHold();
				}
				else if (this.modeType == GGModeType.KillingCompetition)
				{
					this.UpdateFlagBarKillingCompetition();
				}
				else if (this.modeType == GGModeType.Explosion)
				{
					this.UpdateExplosionCountDown();
				}
				else if (this.modeType == GGModeType.Mutation)
				{
					this.UpdateMutationCountDown();
				}
				else if (this.modeType == GGModeType.KnifeCompetition)
				{
					this.UpdateFlagBarKnifeCompetition();
				}
				else if (this.modeType == GGModeType.Hunting)
				{
				}
			}
		}
		this.RefreshAllModeInfoTimer();
	}

	// Token: 0x060012F4 RID: 4852 RVA: 0x000A9B80 File Offset: 0x000A7F80
	public void InitUI()
	{
		this.observerNode.SetActive(false);
		this.modeType = GGNetworkKit.mInstance.GetGameMode();
		this.flagNode.SetActive(false);
		this.bombNode.SetActive(false);
		if (this.modeType == GGModeType.TeamDeathMatch)
		{
			this.isDeathMatchMode = true;
			this.modeInfoNode.SetActive(false);
		}
		else
		{
			this.isDeathMatchMode = false;
			this.modeInfoNode.SetActive(true);
			switch (this.modeType)
			{
			case GGModeType.StrongHold:
				this.flagNode.SetActive(true);
				this.modeLogoSprite.spriteName = "LobbyModeLogoStronghold";
				break;
			case GGModeType.KillingCompetition:
				this.modeLogoSprite.spriteName = "LobbyModeLogoKilling";
				this.modeInfoTimeLabel.gameObject.SetActive(false);
				break;
			case GGModeType.Explosion:
				this.modeLogoSprite.spriteName = "LobbyModeLogoExplosion";
				this.bombCountDownLabel.color = Color.white;
				this.bombNode.SetActive(false);
				this.explosionProgressBar.fillAmount = 0f;
				this.installBombLabel.gameObject.SetActive(true);
				this.uninstallBombLabel.gameObject.SetActive(true);
				TweenAlpha.Begin(this.teamWinTipSprite.gameObject, 0.05f, 0f, 0f);
				this.blueModeInfoBarLabel.text = "0/5";
				this.redModeInfoBarLabel.text = "0/5";
				break;
			case GGModeType.Mutation:
				this.modeLogoSprite.spriteName = "LobbyModeLogoMutation";
				this.blueModeInfoBarLabel.text = "HUMAN";
				this.redModeInfoBarLabel.text = "ZOMBIE";
				this.blueModeInfoPlayerNumLabel.text = "0";
				this.redModeInfoPlayerNumLabel.text = "0";
				break;
			case GGModeType.KnifeCompetition:
				this.modeLogoSprite.spriteName = "LobbyModeLogoKnifeCompetition";
				this.modeInfoTimeLabel.gameObject.SetActive(false);
				break;
			case GGModeType.Hunting:
				this.modeInfoNode.SetActive(false);
				this.huntingModeInfoNode.SetActive(true);
				this.enchantmentBarNode.transform.localPosition = new Vector3(0f, -1000f, 0f);
				break;
			}
		}
		if (GGNetworkKit.mInstance.GetGamePlayModeType() == GGPlayModeType.Sport)
		{
			this.resultTitleLabel.gameObject.SetActive(false);
			this.resultSportTopNode.SetActive(true);
		}
		else
		{
			this.resultTitleLabel.gameObject.SetActive(true);
			this.resultSportTopNode.SetActive(false);
		}
		this.InitMutationMode();
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x000A9E22 File Offset: 0x000A8222
	public void ResetInitUI()
	{
		this.InitUI();
		this.RefreshToHumanUI();
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x000A9E30 File Offset: 0x000A8230
	private void UpdateFlagBarStrongHold()
	{
		this.modeInfo = GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().modeInfo;
		this.resourceTotalNum = this.modeInfo.mMaxResources;
		this.redResourceNum = this.modeInfo.mRedResources;
		this.blueResourceNum = this.modeInfo.mBlueResources;
		this.redResFillAmount = (float)(this.redResourceNum * 100 / this.resourceTotalNum);
		this.blueResFillAmount = (float)(this.blueResourceNum * 100 / this.resourceTotalNum);
		this.redModeInfoBarSprite.fillAmount = this.redResFillAmount / 100f;
		this.blueModeInfoBarSprite.fillAmount = this.blueResFillAmount / 100f;
		this.blueModeInfoBarLabel.text = this.blueResourceNum.ToString() + "/" + this.resourceTotalNum.ToString();
		this.redModeInfoBarLabel.text = this.redResourceNum.ToString() + "/" + this.resourceTotalNum.ToString();
		if (this.modeInfo.mStronghold3State != this.strongholdStateA)
		{
			if (this.modeInfo.mStronghold3State == GGStrondholdState.BlueOccupation)
			{
				if (GGNetworkKit.mInstance.GetManagePlayerProperties().GetMainPlayerProperty().team == GGTeamType.blue)
				{
					if (this.flagASprite.color != Color.green)
					{
						this.flagASprite.color = Color.green;
					}
				}
				else if (this.flagASprite.color != Color.red)
				{
					this.flagASprite.color = Color.red;
				}
			}
			else if (this.modeInfo.mStronghold3State == GGStrondholdState.RedOccupation)
			{
				if (GGNetworkKit.mInstance.GetManagePlayerProperties().GetMainPlayerProperty().team == GGTeamType.red)
				{
					if (this.flagASprite.color != Color.green)
					{
						this.flagASprite.color = Color.green;
					}
				}
				else if (this.flagASprite.color != Color.red)
				{
					this.flagASprite.color = Color.red;
				}
			}
			else if (this.flagASprite.color != Color.white)
			{
				this.flagASprite.color = Color.white;
			}
			this.strongholdStateA = this.modeInfo.mStronghold3State;
		}
		if (this.modeInfo.mStronghold2State != this.strongholdStateB)
		{
			if (this.modeInfo.mStronghold2State == GGStrondholdState.BlueOccupation)
			{
				if (GGNetworkKit.mInstance.GetManagePlayerProperties().GetMainPlayerProperty().team == GGTeamType.blue)
				{
					if (this.flagBSprite.color != Color.green)
					{
						this.flagBSprite.color = Color.green;
					}
				}
				else if (this.flagBSprite.color != Color.red)
				{
					this.flagBSprite.color = Color.red;
				}
			}
			else if (this.modeInfo.mStronghold2State == GGStrondholdState.RedOccupation)
			{
				if (GGNetworkKit.mInstance.GetManagePlayerProperties().GetMainPlayerProperty().team == GGTeamType.red)
				{
					if (this.flagBSprite.color != Color.green)
					{
						this.flagBSprite.color = Color.green;
					}
				}
				else if (this.flagBSprite.color != Color.red)
				{
					this.flagBSprite.color = Color.red;
				}
			}
			else if (this.flagBSprite.color != Color.white)
			{
				this.flagBSprite.color = Color.white;
			}
			this.strongholdStateB = this.modeInfo.mStronghold2State;
		}
		if (this.modeInfo.mStronghold1State != this.strongholdStateC)
		{
			if (this.modeInfo.mStronghold1State == GGStrondholdState.BlueOccupation)
			{
				if (GGNetworkKit.mInstance.GetManagePlayerProperties().GetMainPlayerProperty().team == GGTeamType.blue)
				{
					if (this.flagCSprite.color != Color.green)
					{
						this.flagCSprite.color = Color.green;
					}
				}
				else if (this.flagCSprite.color != Color.red)
				{
					this.flagCSprite.color = Color.red;
				}
			}
			else if (this.modeInfo.mStronghold1State == GGStrondholdState.RedOccupation)
			{
				if (GGNetworkKit.mInstance.GetManagePlayerProperties().GetMainPlayerProperty().team == GGTeamType.red)
				{
					if (this.flagCSprite.color != Color.green)
					{
						this.flagCSprite.color = Color.green;
					}
				}
				else if (this.flagCSprite.color != Color.red)
				{
					this.flagCSprite.color = Color.red;
				}
			}
			else if (this.flagCSprite.color != Color.white)
			{
				this.flagCSprite.color = Color.white;
			}
			this.strongholdStateC = this.modeInfo.mStronghold1State;
		}
		if (this.modeInfo.mStrongholdTimer < this.strongHoldCountdownStep1)
		{
			if (this.modeInfoTimeLabel.text != (this.strongHoldCountdownStep1 - this.modeInfo.mStrongholdTimer).ToString())
			{
				this.modeInfoTimeLabel.GetComponent<TweenScale>().ResetToBeginning();
				this.modeInfoTimeLabel.GetComponent<TweenScale>().Play();
				this.modeInfoTimeLabel.text = (this.strongHoldCountdownStep1 - this.modeInfo.mStrongholdTimer).ToString();
				if (this.modeInfoTimeLabel.color != Color.red)
				{
					this.modeInfoTimeLabel.color = Color.red;
				}
			}
		}
		else
		{
			int num = 120 - this.modeInfo.mStrongholdTimer;
			if (this.modeInfoTimeLabel.text != num.ToString())
			{
				if (num == 120 - this.strongHoldCountdownStep1)
				{
					this.modeInfoTimeLabel.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				}
				this.modeInfoTimeLabel.text = num.ToString();
				if (this.modeInfoTimeLabel.color != Color.white)
				{
					this.modeInfoTimeLabel.color = Color.white;
				}
			}
		}
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x000AA4E8 File Offset: 0x000A88E8
	private void UpdateFlagBarKillingCompetition()
	{
		this.modeInfo = GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().modeInfo;
		this.killTotalNum = this.modeInfo.MAXKilling;
		this.redKillNum = this.modeInfo.redKilling;
		this.blueKillNum = this.modeInfo.blueKilling;
		this.redKillFillAmount = (float)(this.redKillNum * 100 / this.killTotalNum);
		this.blueKillFillAmount = (float)(this.blueKillNum * 100 / this.killTotalNum);
		this.redModeInfoBarSprite.fillAmount = this.redKillFillAmount / 100f;
		this.blueModeInfoBarSprite.fillAmount = this.blueKillFillAmount / 100f;
		this.blueModeInfoBarLabel.text = this.blueKillNum.ToString() + "/" + this.killTotalNum.ToString();
		this.redModeInfoBarLabel.text = this.redKillNum.ToString() + "/" + this.killTotalNum.ToString();
	}

	// Token: 0x060012F8 RID: 4856 RVA: 0x000AA60C File Offset: 0x000A8A0C
	private void UpdateFlagBarKnifeCompetition()
	{
		this.modeInfo = GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().modeInfo;
		this.killTotalNum = this.modeInfo.MAXKilling;
		this.redKillNum = this.modeInfo.redKilling;
		this.blueKillNum = this.modeInfo.blueKilling;
		this.redKillFillAmount = (float)(this.redKillNum * 100 / this.killTotalNum);
		this.blueKillFillAmount = (float)(this.blueKillNum * 100 / this.killTotalNum);
		this.redModeInfoBarSprite.fillAmount = this.redKillFillAmount / 100f;
		this.blueModeInfoBarSprite.fillAmount = this.blueKillFillAmount / 100f;
		this.blueModeInfoBarLabel.text = this.blueKillNum.ToString() + "/" + this.killTotalNum.ToString();
		this.redModeInfoBarLabel.text = this.redKillNum.ToString() + "/" + this.killTotalNum.ToString();
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x000AA730 File Offset: 0x000A8B30
	private void UpdateFlagBarHunting()
	{
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x000AA734 File Offset: 0x000A8B34
	private void UpdateExplosionCountDown()
	{
		this.modeInfo = GGNetworkKit.mInstance.GetManageGlobalInfo().GetGlobalInfo().modeInfo;
		this.modeInfoTimeLabel.text = (this.modeInfo.totalTimer / 60).ToString() + " : " + ((this.modeInfo.totalTimer % 60 >= 10) ? (this.modeInfo.totalTimer % 60).ToString() : ("0" + (this.modeInfo.totalTimer % 60).ToString()));
		if (this.blueModeInfoBarLabel.text != this.modeInfo.BlueTeamWinNum + "/5")
		{
			this.blueModeInfoBarLabel.text = this.modeInfo.BlueTeamWinNum.ToString() + "/5";
		}
		if (this.redModeInfoBarLabel.text != this.modeInfo.RedTeamWinNum + "/5")
		{
			this.redModeInfoBarLabel.text = this.modeInfo.RedTeamWinNum.ToString() + "/5";
		}
		if (this.modeInfo.IsTimerBombInstalled)
		{
			if (!this.bombNode.activeSelf)
			{
				this.bombNode.SetActive(true);
			}
			if (this.modeInfo.explosionTimer != int.Parse(this.bombCountDownLabel.text))
			{
				if (this.modeInfo.explosionTimer <= 10)
				{
					this.bombCountDownLabel.color = Color.red;
					this.bombCountDownLabel.GetComponent<TweenScale>().ResetToBeginning();
					this.bombCountDownLabel.GetComponent<TweenScale>().Play();
				}
				this.bombCountDownLabel.text = this.modeInfo.explosionTimer.ToString();
			}
		}
		else if (this.bombNode.activeSelf)
		{
			this.bombNode.SetActive(false);
			this.bombCountDownLabel.color = Color.white;
		}
	}

	// Token: 0x060012FB RID: 4859 RVA: 0x000AA97C File Offset: 0x000A8D7C
	public void ShowCarryBombLogo()
	{
		if (!this.carryBombLogo.activeSelf)
		{
			this.carryBombLogo.SetActive(true);
		}
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x000AA99A File Offset: 0x000A8D9A
	public void HideCarryBombLogo()
	{
		if (this.carryBombLogo.activeSelf)
		{
			this.carryBombLogo.SetActive(false);
		}
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x000AA9B8 File Offset: 0x000A8DB8
	public void ShowObserverNode()
	{
		if (!this.observerNode.activeSelf)
		{
			this.observerNode.SetActive(true);
		}
		MobileFPSInputController.isPause = true;
		this.nodeForObserver.SetActive(false);
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x000AA9E8 File Offset: 0x000A8DE8
	public void HideObserverNode()
	{
		if (this.observerNode.activeSelf)
		{
			UIPlayDirector.mInstance.fireBtn.GetComponent<UITouchMonitor>().isFirePressing = true;
			this.observerNode.SetActive(false);
		}
		MobileFPSInputController.isPause = false;
		this.nodeForObserver.SetActive(true);
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x000AAA38 File Offset: 0x000A8E38
	public void RefreshObserverNode(GGNetworkCharacter observerCharacter)
	{
		if (observerCharacter != null)
		{
			UIPlayDirector.mInstance.mNetworkCharacter = observerCharacter;
			this.observerRankLabel.text = "-" + observerCharacter.mPlayerProperties.rank.ToString() + "-";
			this.observerRankLogo.spriteName = "Rank_" + observerCharacter.mPlayerProperties.rank.ToString();
			this.observerNameLabel.text = observerCharacter.mPlayerProperties.name.ToString();
		}
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x000AAAD8 File Offset: 0x000A8ED8
	public void ShowTeamWinSpriteTip(string winnerName)
	{
		this.teamWinTipSprite.spriteName = winnerName;
		TweenPosition.Begin(this.teamWinTipSprite.gameObject, 0.2f, new Vector3(0f, 250f, 0f));
		TweenAlpha.Begin(this.teamWinTipSprite.gameObject, 0.4f, 1f, 0f);
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x000AAB3C File Offset: 0x000A8F3C
	public void HideTeamWinSpriteTip()
	{
		TweenAlpha.Begin(this.teamWinTipSprite.gameObject, 0.01f, 0f, 0f);
		TweenPosition.Begin(this.teamWinTipSprite.gameObject, 0.02f, new Vector3(0f, 500f, 0f));
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x000AAB94 File Offset: 0x000A8F94
	public void ShowExplosionCurRoundLabel(string roundContent)
	{
		this.explosionCurRoundLabel.text = roundContent;
		this.explosionCurRoundLabel.gameObject.SetActive(true);
		TweenScale.Begin(this.explosionCurRoundLabel.gameObject, 0.12f, new Vector3(1f, 1f, 1f));
	}

	// Token: 0x06001303 RID: 4867 RVA: 0x000AABE8 File Offset: 0x000A8FE8
	public void HideExplosionCurRoundLabel()
	{
		this.explosionCurRoundLabel.gameObject.SetActive(false);
		this.explosionCurRoundLabel.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
	}

	// Token: 0x06001304 RID: 4868 RVA: 0x000AAC24 File Offset: 0x000A9024
	private void InitMutationMode()
	{
		if (this.modeType == GGModeType.Mutation)
		{
			this.DisableButtonInMutationMode();
			this.blueLobbyLabel.text = "Human";
			this.redLobbyLabel.text = "Zombie";
			this.blueItem1Label.text = "Ping";
			this.blueItem2Label.text = "Score";
			this.blueItem3Label.text = string.Empty;
			this.redItem1Label.text = "Ping";
			this.redItem2Label.text = "Score";
			this.redItem3Label.text = string.Empty;
			this.blueTeamBtn.gameObject.SetActive(false);
			this.redTeamBtn.gameObject.SetActive(false);
			this.blueResultLabel.text = "Human";
			this.redResultLabel.text = "Zombie";
			this.blueResultItem1Label.text = "Rate";
			this.blueResultItem2Label.text = "Exp.";
			this.blueResultItem3Label.text = "Coin";
			this.blueResultItem4Label.text = "Score";
			this.redResultItem1Label.text = "Rate";
			this.redResultItem2Label.text = "Exp.";
			this.redResultItem3Label.text = "Coin";
			this.redResultItem4Label.text = "Score";
		}
		else if (this.modeType == GGModeType.Hunting)
		{
			this.EnableButtonInMutationMode();
			this.thrownBtn.gameObject.SetActive(false);
			this.addBulletBtn.gameObject.SetActive(false);
			this.blueItem1Label.text = "Ping";
			this.blueItem2Label.text = "Dead";
			this.blueItem3Label.text = string.Empty;
			this.redItem1Label.text = "Ping";
			this.redItem2Label.text = "Dead";
			this.redItem3Label.text = string.Empty;
			this.blueTeamBtn.gameObject.SetActive(false);
			this.redTeamBtn.gameObject.SetActive(false);
		}
		else
		{
			this.EnableButtonInMutationMode();
			if (this.modeType == GGModeType.TeamDeathMatch)
			{
				this.blueItem1Label.text = "Ping";
				this.blueItem2Label.text = "Kill/Dead";
				this.blueItem3Label.text = string.Empty;
				this.redItem1Label.text = "Ping";
				this.redItem2Label.text = "Kill/Dead";
				this.redItem3Label.text = string.Empty;
				this.blueResultItem1Label.text = "Rate";
				this.blueResultItem2Label.text = "Exp.";
				this.blueResultItem3Label.text = "Coin";
				this.blueResultItem4Label.text = "Kill/Dead";
				this.redResultItem1Label.text = "Rate";
				this.redResultItem2Label.text = "Exp.";
				this.redResultItem3Label.text = "Coin";
				this.redResultItem4Label.text = "Kill/Dead";
			}
			else
			{
				this.blueItem1Label.text = "Ping";
				this.blueItem2Label.text = "Kill/Dead";
				this.blueItem3Label.text = string.Empty;
				this.redItem1Label.text = "Ping";
				this.redItem2Label.text = "Kill/Dead";
				this.redItem3Label.text = string.Empty;
				this.blueResultItem1Label.text = "Rate";
				this.blueResultItem2Label.text = "Score";
				this.blueResultItem3Label.text = string.Empty;
				this.blueResultItem4Label.text = string.Empty;
				this.redResultItem1Label.text = "Rate";
				this.redResultItem2Label.text = "Score";
				this.redResultItem3Label.text = string.Empty;
				this.redResultItem4Label.text = string.Empty;
				if (this.modeType == GGModeType.StrongHold)
				{
					this.modeUniqueDataTitleLabel.text = "Occupy";
				}
				else if (this.modeType == GGModeType.Explosion)
				{
					this.modeUniqueDataTitleLabel.text = "Install/Uninstall";
				}
				else
				{
					this.modeUniqueDataTitleLabel.text = string.Empty;
				}
			}
		}
		this.startBtn.gameObject.SetActive(false);
	}

	// Token: 0x06001305 RID: 4869 RVA: 0x000AB080 File Offset: 0x000A9480
	public void RefreshToZombieUI(string zombieSkillLogoName)
	{
		this.buffNode.SetActive(false);
		this.buffToggle.gameObject.SetActive(false);
		this.weaponSprite.gameObject.SetActive(false);
		this.addBloodBtn.gameObject.SetActive(false);
		this.addBloodFgSprite.gameObject.SetActive(false);
		this.addArmorBtn.gameObject.SetActive(false);
		this.weaponLeftBtn.gameObject.SetActive(false);
		this.weaponRightBtn.gameObject.SetActive(false);
		this.reloadBtn.gameObject.SetActive(false);
		this.thrownBtn.gameObject.SetActive(false);
		if (this.aimBtn.gameObject != null)
		{
			this.aimBtn.gameObject.SetActive(false);
		}
		this.addBulletBtn.gameObject.SetActive(false);
		this.zombieSkillLogo.gameObject.SetActive(true);
		string path = "UI/Images/ZombieSkillLogo/ZS_" + zombieSkillLogoName + "_Logo";
		this.zombieSkillLogo.mainTexture = (Resources.Load(path) as Texture);
	}

	// Token: 0x06001306 RID: 4870 RVA: 0x000AB1A4 File Offset: 0x000A95A4
	public void RefreshToHumanUI()
	{
		if (this.modeType != GGModeType.Mutation)
		{
			return;
		}
		this.buffNode.SetActive(false);
		this.buffToggle.gameObject.SetActive(true);
		this.weaponSprite.gameObject.SetActive(true);
		this.addBloodBtn.gameObject.SetActive(true);
		this.addArmorBtn.gameObject.SetActive(true);
		this.weaponLeftBtn.gameObject.SetActive(true);
		this.weaponRightBtn.gameObject.SetActive(true);
		this.reloadBtn.gameObject.SetActive(true);
		if (UIUserDataController.GetQuickBarItemIndex() == 6)
		{
			this.thrownBtn.gameObject.SetActive(false);
		}
		else
		{
			this.thrownBtn.gameObject.SetActive(true);
		}
		this.addBulletBtn.gameObject.SetActive(true);
		this.DisableButtonInMutationMode();
		if (this.zombieSkillLogo.gameObject.activeSelf)
		{
			this.zombieSkillLogo.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001307 RID: 4871 RVA: 0x000AB2B0 File Offset: 0x000A96B0
	public void EnableButtonInMutationMode()
	{
		this.fireFrontSprite.gameObject.SetActive(false);
		this.thrownFrontSprite.gameObject.SetActive(false);
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x000AB2D4 File Offset: 0x000A96D4
	public void DisableButtonInMutationMode()
	{
		this.fireFrontSprite.gameObject.SetActive(true);
		if (UIUserDataController.GetQuickBarItemIndex() == 6)
		{
			this.thrownFrontSprite.gameObject.SetActive(false);
		}
		else
		{
			this.thrownFrontSprite.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x000AB324 File Offset: 0x000A9724
	public void ShowStartBtn()
	{
		this.startBtn.isEnabled = false;
		this.startBtn.gameObject.SetActive(true);
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x000AB343 File Offset: 0x000A9743
	public void StartBtnPressed()
	{
		this.startBtn.gameObject.SetActive(false);
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x000AB356 File Offset: 0x000A9756
	public void EnableStartBtn()
	{
		if (!this.startBtn.gameObject.activeSelf)
		{
			this.startBtn.gameObject.SetActive(true);
		}
		this.startBtn.isEnabled = true;
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x000AB38C File Offset: 0x000A978C
	private void UpdateMutationCountDown()
	{
		if (this.mutationModeTime != GGMutationModeControl.mInstance.survivalTimer)
		{
			this.mutationModeTime = GGMutationModeControl.mInstance.survivalTimer;
			this.modeInfoTimeLabel.text = (this.mutationModeTime / 60).ToString() + " : " + ((this.mutationModeTime % 60 >= 10) ? (this.mutationModeTime % 60).ToString() : ("0" + (this.mutationModeTime % 60).ToString()));
		}
		if (this.mutationHumanNum != GGMutationModeControl.mInstance.humanNum)
		{
			this.mutationHumanNum = GGMutationModeControl.mInstance.humanNum;
			this.blueModeInfoPlayerNumLabel.text = this.mutationHumanNum.ToString();
		}
		if (this.mutationZombieNum != GGMutationModeControl.mInstance.zombieNum)
		{
			this.mutationZombieNum = GGMutationModeControl.mInstance.zombieNum;
			this.redModeInfoPlayerNumLabel.text = this.mutationZombieNum.ToString();
		}
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x000AB4BC File Offset: 0x000A98BC
	private void RefreshAllModeInfoTimer()
	{
		if (this.modeType != GGModeType.TeamDeathMatch && this.modeType != GGModeType.Mutation && this.modeType != GGModeType.Hunting)
		{
			this.updateTime2 += Time.deltaTime;
			if (this.updateTime2 > this.updateTimerCycle2)
			{
				this.updateTime2 = 0f;
				string text = this.modeInfo.AllModeBlueTeamPlayerSurvivalNum.ToString() + "/" + this.modeInfo.AllModeBlueTeamPlayerTotalNum.ToString();
				if (this.blueModeInfoPlayerNumLabel.text != text)
				{
					this.blueModeInfoPlayerNumLabel.text = text;
				}
				string text2 = this.modeInfo.AllModeRedTeamPlayerSurvivalNum.ToString() + "/" + this.modeInfo.AllModeRedTeamPlayerTotalNum.ToString();
				if (this.redModeInfoPlayerNumLabel.text != text2)
				{
					this.redModeInfoPlayerNumLabel.text = text2;
				}
			}
		}
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x000AB5CC File Offset: 0x000A99CC
	public void StartWaitTipNode()
	{
		this.modewaitTipLabel.gameObject.SetActive(true);
		MobileFPSInputController.isPause = true;
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x000AB5E5 File Offset: 0x000A99E5
	public void EndWaitTipNode()
	{
		this.modewaitTipLabel.gameObject.SetActive(false);
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x000AB5F8 File Offset: 0x000A99F8
	public void ShowJoystickNode(bool isVisible)
	{
		MobileFPSInputController.isPause = !isVisible;
	}

	// Token: 0x040015CC RID: 5580
	public static UIModeDirector mInstance;

	// Token: 0x040015CD RID: 5581
	public GameObject modeInfoNode;

	// Token: 0x040015CE RID: 5582
	public GameObject flagNode;

	// Token: 0x040015CF RID: 5583
	public GameObject bombNode;

	// Token: 0x040015D0 RID: 5584
	public UISprite modeLogoSprite;

	// Token: 0x040015D1 RID: 5585
	public UISprite blueModeInfoBarSprite;

	// Token: 0x040015D2 RID: 5586
	public UISprite redModeInfoBarSprite;

	// Token: 0x040015D3 RID: 5587
	public UILabel blueModeInfoBarLabel;

	// Token: 0x040015D4 RID: 5588
	public UILabel redModeInfoBarLabel;

	// Token: 0x040015D5 RID: 5589
	public UILabel blueModeInfoPlayerNumLabel;

	// Token: 0x040015D6 RID: 5590
	public UILabel redModeInfoPlayerNumLabel;

	// Token: 0x040015D7 RID: 5591
	public GameObject modePlayerNumNode;

	// Token: 0x040015D8 RID: 5592
	public UILabel modeInfoTimeLabel;

	// Token: 0x040015D9 RID: 5593
	public UISprite flagASprite;

	// Token: 0x040015DA RID: 5594
	public UISprite flagBSprite;

	// Token: 0x040015DB RID: 5595
	public UISprite flagCSprite;

	// Token: 0x040015DC RID: 5596
	private int resourceTotalNum;

	// Token: 0x040015DD RID: 5597
	private int redResourceNum;

	// Token: 0x040015DE RID: 5598
	private int blueResourceNum;

	// Token: 0x040015DF RID: 5599
	private float redResFillAmount;

	// Token: 0x040015E0 RID: 5600
	private float blueResFillAmount;

	// Token: 0x040015E1 RID: 5601
	public UISprite strongHoldProgressBar;

	// Token: 0x040015E2 RID: 5602
	private int strongHoldCountdownStep1 = 20;

	// Token: 0x040015E3 RID: 5603
	private GGStrondholdState strongholdStateA = GGStrondholdState.unactivate;

	// Token: 0x040015E4 RID: 5604
	private GGStrondholdState strongholdStateB = GGStrondholdState.unactivate;

	// Token: 0x040015E5 RID: 5605
	private GGStrondholdState strongholdStateC = GGStrondholdState.unactivate;

	// Token: 0x040015E6 RID: 5606
	private int killTotalNum;

	// Token: 0x040015E7 RID: 5607
	private int redKillNum;

	// Token: 0x040015E8 RID: 5608
	private int blueKillNum;

	// Token: 0x040015E9 RID: 5609
	private float redKillFillAmount;

	// Token: 0x040015EA RID: 5610
	private float blueKillFillAmount;

	// Token: 0x040015EB RID: 5611
	public GGModeType modeType;

	// Token: 0x040015EC RID: 5612
	private GGModeInfo modeInfo;

	// Token: 0x040015ED RID: 5613
	public GameObject huntingModeInfoNode;

	// Token: 0x040015EE RID: 5614
	private int frameCount;

	// Token: 0x040015EF RID: 5615
	private float updateTimerCycle = 0.5f;

	// Token: 0x040015F0 RID: 5616
	private float updateTime;

	// Token: 0x040015F1 RID: 5617
	private bool isDeathMatchMode = true;

	// Token: 0x040015F2 RID: 5618
	private float updateTimerCycle2 = 1f;

	// Token: 0x040015F3 RID: 5619
	private float updateTime2;

	// Token: 0x040015F4 RID: 5620
	public UILabel bombCountDownLabel;

	// Token: 0x040015F5 RID: 5621
	public UISprite explosionProgressBar;

	// Token: 0x040015F6 RID: 5622
	public UILabel installBombLabel;

	// Token: 0x040015F7 RID: 5623
	public UILabel uninstallBombLabel;

	// Token: 0x040015F8 RID: 5624
	public GameObject carryBombLogo;

	// Token: 0x040015F9 RID: 5625
	public UILabel explosionCurRoundLabel;

	// Token: 0x040015FA RID: 5626
	public UISprite teamWinTipSprite;

	// Token: 0x040015FB RID: 5627
	public GameObject observerNode;

	// Token: 0x040015FC RID: 5628
	public UISprite observerRankLogo;

	// Token: 0x040015FD RID: 5629
	public UILabel observerRankLabel;

	// Token: 0x040015FE RID: 5630
	public UILabel observerNameLabel;

	// Token: 0x040015FF RID: 5631
	public GameObject nodeForObserver;

	// Token: 0x04001600 RID: 5632
	public GameObject buffNode;

	// Token: 0x04001601 RID: 5633
	public UIToggle buffToggle;

	// Token: 0x04001602 RID: 5634
	public UISprite weaponSprite;

	// Token: 0x04001603 RID: 5635
	public UIButton addBloodBtn;

	// Token: 0x04001604 RID: 5636
	public UISprite addBloodFgSprite;

	// Token: 0x04001605 RID: 5637
	public UIButton addArmorBtn;

	// Token: 0x04001606 RID: 5638
	public UIButton weaponLeftBtn;

	// Token: 0x04001607 RID: 5639
	public UIButton weaponRightBtn;

	// Token: 0x04001608 RID: 5640
	public UIButton reloadBtn;

	// Token: 0x04001609 RID: 5641
	public UIButton thrownBtn;

	// Token: 0x0400160A RID: 5642
	public UIButton aimBtn;

	// Token: 0x0400160B RID: 5643
	public UIButton addBulletBtn;

	// Token: 0x0400160C RID: 5644
	public UISprite fireFrontSprite;

	// Token: 0x0400160D RID: 5645
	public UISprite thrownFrontSprite;

	// Token: 0x0400160E RID: 5646
	public UIButton startBtn;

	// Token: 0x0400160F RID: 5647
	public GameObject enchantmentBarNode;

	// Token: 0x04001610 RID: 5648
	public UITexture zombieSkillLogo;

	// Token: 0x04001611 RID: 5649
	public UILabel blueLobbyLabel;

	// Token: 0x04001612 RID: 5650
	public UILabel redLobbyLabel;

	// Token: 0x04001613 RID: 5651
	public UILabel blueItem1Label;

	// Token: 0x04001614 RID: 5652
	public UILabel blueItem2Label;

	// Token: 0x04001615 RID: 5653
	public UILabel blueItem3Label;

	// Token: 0x04001616 RID: 5654
	public UILabel redItem1Label;

	// Token: 0x04001617 RID: 5655
	public UILabel redItem2Label;

	// Token: 0x04001618 RID: 5656
	public UILabel redItem3Label;

	// Token: 0x04001619 RID: 5657
	public UIButton blueTeamBtn;

	// Token: 0x0400161A RID: 5658
	public UIButton redTeamBtn;

	// Token: 0x0400161B RID: 5659
	public UILabel blueResultLabel;

	// Token: 0x0400161C RID: 5660
	public UILabel redResultLabel;

	// Token: 0x0400161D RID: 5661
	public UILabel blueResultItem1Label;

	// Token: 0x0400161E RID: 5662
	public UILabel blueResultItem2Label;

	// Token: 0x0400161F RID: 5663
	public UILabel blueResultItem3Label;

	// Token: 0x04001620 RID: 5664
	public UILabel blueResultItem4Label;

	// Token: 0x04001621 RID: 5665
	public UILabel redResultItem1Label;

	// Token: 0x04001622 RID: 5666
	public UILabel redResultItem2Label;

	// Token: 0x04001623 RID: 5667
	public UILabel redResultItem3Label;

	// Token: 0x04001624 RID: 5668
	public UILabel redResultItem4Label;

	// Token: 0x04001625 RID: 5669
	public UILabel resultTitleLabel;

	// Token: 0x04001626 RID: 5670
	public GameObject resultSportTopNode;

	// Token: 0x04001627 RID: 5671
	public UILabel modeUniqueDataTitleLabel;

	// Token: 0x04001628 RID: 5672
	private int mutationModeTime;

	// Token: 0x04001629 RID: 5673
	private int mutationHumanNum;

	// Token: 0x0400162A RID: 5674
	private int mutationZombieNum;

	// Token: 0x0400162B RID: 5675
	public UILabel modewaitTipLabel;

	// Token: 0x0400162C RID: 5676
	public GameObject countdownBgObj;
}
