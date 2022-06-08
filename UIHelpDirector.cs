using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020002BA RID: 698
public class UIHelpDirector : MonoBehaviour
{
	// Token: 0x1400001E RID: 30
	// (add) Token: 0x06001456 RID: 5206 RVA: 0x000B1408 File Offset: 0x000AF808
	// (remove) Token: 0x06001457 RID: 5207 RVA: 0x000B143C File Offset: 0x000AF83C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpJumpEventHandler OnHelpJump;

	// Token: 0x06001458 RID: 5208 RVA: 0x000B1470 File Offset: 0x000AF870
	public void GenJumpEvent()
	{
		if (UIHelpDirector.OnHelpJump != null)
		{
			UIHelpDirector.OnHelpJump();
		}
	}

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x06001459 RID: 5209 RVA: 0x000B1488 File Offset: 0x000AF888
	// (remove) Token: 0x0600145A RID: 5210 RVA: 0x000B14BC File Offset: 0x000AF8BC
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpSniperCancelEventHandler OnHelpSniperCancel;

	// Token: 0x0600145B RID: 5211 RVA: 0x000B14F0 File Offset: 0x000AF8F0
	public void GenSniperCancelEvent()
	{
		if (UIHelpDirector.OnHelpSniperCancel != null)
		{
			UIHelpDirector.OnHelpSniperCancel();
		}
	}

	// Token: 0x14000020 RID: 32
	// (add) Token: 0x0600145C RID: 5212 RVA: 0x000B1508 File Offset: 0x000AF908
	// (remove) Token: 0x0600145D RID: 5213 RVA: 0x000B153C File Offset: 0x000AF93C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpReloadEventHandler OnHelpReload;

	// Token: 0x0600145E RID: 5214 RVA: 0x000B1570 File Offset: 0x000AF970
	public void GenReloadEvent()
	{
		if (UIHelpDirector.OnHelpReload != null)
		{
			UIHelpDirector.OnHelpReload();
		}
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x000B1586 File Offset: 0x000AF986
	public void FireOnPress()
	{
		if (UIUserDataController.GetSniperMode() == 1)
		{
			this.cancelFire = false;
		}
		this.GenFireStartEvent();
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x000B15A0 File Offset: 0x000AF9A0
	public void FireOnRelease()
	{
		if (UIUserDataController.GetSniperMode() == 1)
		{
			if (!this.cancelFire)
			{
				this.GenFireEndEvent();
			}
		}
		else
		{
			this.GenFireEndEvent();
		}
	}

	// Token: 0x14000021 RID: 33
	// (add) Token: 0x06001461 RID: 5217 RVA: 0x000B15CC File Offset: 0x000AF9CC
	// (remove) Token: 0x06001462 RID: 5218 RVA: 0x000B1600 File Offset: 0x000AFA00
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpFireStartEventHandler OnHelpFireStart;

	// Token: 0x06001463 RID: 5219 RVA: 0x000B1634 File Offset: 0x000AFA34
	public void GenFireStartEvent()
	{
		if (UIHelpDirector.OnHelpFireStart != null)
		{
			UIHelpDirector.OnHelpFireStart();
		}
	}

	// Token: 0x14000022 RID: 34
	// (add) Token: 0x06001464 RID: 5220 RVA: 0x000B164C File Offset: 0x000AFA4C
	// (remove) Token: 0x06001465 RID: 5221 RVA: 0x000B1680 File Offset: 0x000AFA80
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpFireEndEventHandler OnHelpFireEnd;

	// Token: 0x06001466 RID: 5222 RVA: 0x000B16B4 File Offset: 0x000AFAB4
	public void GenFireEndEvent()
	{
		if (UIHelpDirector.OnHelpFireEnd != null)
		{
			UIHelpDirector.OnHelpFireEnd();
		}
	}

	// Token: 0x14000023 RID: 35
	// (add) Token: 0x06001467 RID: 5223 RVA: 0x000B16CC File Offset: 0x000AFACC
	// (remove) Token: 0x06001468 RID: 5224 RVA: 0x000B1700 File Offset: 0x000AFB00
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpJumpStartEventHandler OnHelpJumpStart;

	// Token: 0x06001469 RID: 5225 RVA: 0x000B1734 File Offset: 0x000AFB34
	public void GenJumpStartEvent()
	{
		if (UIHelpDirector.OnHelpJumpStart != null)
		{
			UIHelpDirector.OnHelpJumpStart();
		}
	}

	// Token: 0x14000024 RID: 36
	// (add) Token: 0x0600146A RID: 5226 RVA: 0x000B174C File Offset: 0x000AFB4C
	// (remove) Token: 0x0600146B RID: 5227 RVA: 0x000B1780 File Offset: 0x000AFB80
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpJumpEndEventHandler OnHelpJumpEnd;

	// Token: 0x0600146C RID: 5228 RVA: 0x000B17B4 File Offset: 0x000AFBB4
	public void GenJumpEndEvent()
	{
		if (UIHelpDirector.OnHelpJumpEnd != null)
		{
			UIHelpDirector.OnHelpJumpEnd();
		}
	}

	// Token: 0x14000025 RID: 37
	// (add) Token: 0x0600146D RID: 5229 RVA: 0x000B17CC File Offset: 0x000AFBCC
	// (remove) Token: 0x0600146E RID: 5230 RVA: 0x000B1800 File Offset: 0x000AFC00
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpSwitchLeftEventHandler OnHelpSwitchLeft;

	// Token: 0x0600146F RID: 5231 RVA: 0x000B1834 File Offset: 0x000AFC34
	public void GenSwitchLeftEvent()
	{
		if (UIHelpDirector.OnHelpSwitchLeft != null)
		{
			UIHelpDirector.OnHelpSwitchLeft();
		}
	}

	// Token: 0x14000026 RID: 38
	// (add) Token: 0x06001470 RID: 5232 RVA: 0x000B184C File Offset: 0x000AFC4C
	// (remove) Token: 0x06001471 RID: 5233 RVA: 0x000B1880 File Offset: 0x000AFC80
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpSwitchRightEventHandler OnHelpSwitchRight;

	// Token: 0x06001472 RID: 5234 RVA: 0x000B18B4 File Offset: 0x000AFCB4
	public void GenSwitchRightEvent()
	{
		if (UIHelpDirector.OnHelpSwitchRight != null)
		{
			UIHelpDirector.OnHelpSwitchRight();
		}
	}

	// Token: 0x14000027 RID: 39
	// (add) Token: 0x06001473 RID: 5235 RVA: 0x000B18CC File Offset: 0x000AFCCC
	// (remove) Token: 0x06001474 RID: 5236 RVA: 0x000B1900 File Offset: 0x000AFD00
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpAddBulletEventHandler OnHelpAddBullet;

	// Token: 0x06001475 RID: 5237 RVA: 0x000B1934 File Offset: 0x000AFD34
	public void GenAddBulletEvent()
	{
		if (UIHelpDirector.OnHelpAddBullet != null)
		{
			UIHelpDirector.OnHelpAddBullet();
		}
	}

	// Token: 0x14000028 RID: 40
	// (add) Token: 0x06001476 RID: 5238 RVA: 0x000B194C File Offset: 0x000AFD4C
	// (remove) Token: 0x06001477 RID: 5239 RVA: 0x000B1980 File Offset: 0x000AFD80
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpArmorEventHandler OnHelpArmor;

	// Token: 0x06001478 RID: 5240 RVA: 0x000B19B4 File Offset: 0x000AFDB4
	public void GenArmorEvent()
	{
		if (UIHelpDirector.OnHelpArmor != null)
		{
			UIHelpDirector.OnHelpArmor();
		}
	}

	// Token: 0x14000029 RID: 41
	// (add) Token: 0x06001479 RID: 5241 RVA: 0x000B19CC File Offset: 0x000AFDCC
	// (remove) Token: 0x0600147A RID: 5242 RVA: 0x000B1A00 File Offset: 0x000AFE00
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpMediKitEventHandler OnHelpMediKit;

	// Token: 0x0600147B RID: 5243 RVA: 0x000B1A34 File Offset: 0x000AFE34
	public void GenMediKitEvent()
	{
		if (UIHelpDirector.OnHelpMediKit != null)
		{
			UIHelpDirector.OnHelpMediKit();
		}
	}

	// Token: 0x1400002A RID: 42
	// (add) Token: 0x0600147C RID: 5244 RVA: 0x000B1A4C File Offset: 0x000AFE4C
	// (remove) Token: 0x0600147D RID: 5245 RVA: 0x000B1A80 File Offset: 0x000AFE80
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIHelpDirector.HelpThrowWeaponFireEventHandler OnHelpThrowWeaponFire;

	// Token: 0x0600147E RID: 5246 RVA: 0x000B1AB4 File Offset: 0x000AFEB4
	public void GenThrowWeaponFireEvent(int index)
	{
		if (UIHelpDirector.OnHelpThrowWeaponFire != null)
		{
			UIHelpDirector.OnHelpThrowWeaponFire(index);
		}
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x000B1ACB File Offset: 0x000AFECB
	private void Awake()
	{
		UIHelpDirector.mInstance = this;
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x000B1AD3 File Offset: 0x000AFED3
	private void OnDestroy()
	{
		if (UIHelpDirector.mInstance != null)
		{
			UIHelpDirector.mInstance = null;
		}
	}

	// Token: 0x06001481 RID: 5249 RVA: 0x000B1AEC File Offset: 0x000AFEEC
	private void Start()
	{
		if (UIUserDataController.GetFirstPlay() == 0)
		{
			UnityAnalyticsIntegration.mInstance.NewUserLoadedHelp();
		}
		this.Init();
		if (GameObject.FindWithTag("Player") != null)
		{
			this.mNetworkCharacter = GameObject.FindWithTag("Player").GetComponent<GGNetworkCharacter>();
		}
		else
		{
			this.mNetworkCharacter = null;
		}
		this.mWeaponManager = GameObject.FindWithTag("Player").transform.Find("LookObject/Main Camera/Weapon Camera/WeaponManager").GetComponent<GGWeaponManager>();
		this.weaponSprite.spriteName = this.GetCurWeaponLogoName();
	}

	// Token: 0x06001482 RID: 5250 RVA: 0x000B1B80 File Offset: 0x000AFF80
	private void Update()
	{
		if (this.mNetworkCharacter != null)
		{
			if (Time.frameCount % 5 == 0)
			{
				if (this.bloodNum != this.mNetworkCharacter.mBlood)
				{
					this.bloodNum = this.mNetworkCharacter.mBlood;
					this.bloodLabel.text = this.bloodNum.ToString();
				}
				if (this.armorNum != this.mNetworkCharacter.myArmorInfo.mDurabilityInGame)
				{
					this.armorNum = this.mNetworkCharacter.myArmorInfo.mDurabilityInGame;
					this.armorLabel.text = this.armorNum.ToString();
				}
			}
		}
		else if (Time.frameCount % 16 == 0 && GameObject.FindWithTag("Player") != null)
		{
			this.mNetworkCharacter = GameObject.FindWithTag("Player").GetComponent<GGNetworkCharacter>();
		}
		if (Time.frameCount % 5 == 0 && this.clipLabel.text != this.mWeaponManager.GetAmmoStr())
		{
			this.clipLabel.text = this.mWeaponManager.GetAmmoStr();
		}
	}

	// Token: 0x06001483 RID: 5251 RVA: 0x000B1CB8 File Offset: 0x000B00B8
	private void Init()
	{
		this.fireBtn.gameObject.SetActive(false);
		this.reloadBtn.gameObject.SetActive(false);
		this.jumpBtn.gameObject.SetActive(false);
		this.throwGrenadeBtn.gameObject.SetActive(false);
		this.addClipBtn.gameObject.SetActive(false);
		this.addBloodBtn.gameObject.SetActive(false);
		this.addArmorBtn.gameObject.SetActive(false);
		this.weaponLogoObject.gameObject.SetActive(true);
		this.welcomeLabel.text = "HI, " + UIUserDataController.GetDefaultRoleName() + ". WELCOME TO \nTHE TRAINING CENTER!";
		base.StartCoroutine(this.ShowObjectDelayTime(this.welcomeLabel.gameObject, null, null, 4f));
	}

	// Token: 0x06001484 RID: 5252 RVA: 0x000B1D8B File Offset: 0x000B018B
	public void JoystickDragEvent()
	{
		base.StartCoroutine(this.ShowObjectDelayTime(this.joystickTip, this.rotateTip, null, 2f));
	}

	// Token: 0x06001485 RID: 5253 RVA: 0x000B1DAC File Offset: 0x000B01AC
	private IEnumerator ShowObjectDelayTime(GameObject hideObj, GameObject obj1, GameObject obj2, float time)
	{
		yield return new WaitForSeconds(time);
		if (obj1 != null)
		{
			obj1.SetActive(true);
		}
		if (obj2 != null)
		{
			obj2.SetActive(true);
		}
		if (hideObj != null)
		{
			hideObj.SetActive(false);
		}
		yield break;
	}

	// Token: 0x06001486 RID: 5254 RVA: 0x000B1DDD File Offset: 0x000B01DD
	public void RotateDragEvent()
	{
		base.StartCoroutine(this.ShowObjectDelayTime(this.rotateTip, this.fireBtn.gameObject, this.fireTip, 2f));
	}

	// Token: 0x06001487 RID: 5255 RVA: 0x000B1E08 File Offset: 0x000B0208
	public void FireBtnPressed()
	{
	}

	// Token: 0x06001488 RID: 5256 RVA: 0x000B1E0C File Offset: 0x000B020C
	public void ThrowGrenadeBtnPressed()
	{
		if (!this.hasThrowGrenade)
		{
			this.hasThrowGrenade = true;
			base.StartCoroutine(this.ShowObjectDelayTime(this.throwGrenadeTip, this.jumpBtn.gameObject, this.jumpTip, 2f));
		}
		this.GenThrowWeaponFireEvent(0);
	}

	// Token: 0x06001489 RID: 5257 RVA: 0x000B1E5C File Offset: 0x000B025C
	public void AddBloodBtnPressed()
	{
		if (!this.hasAddBlood)
		{
			this.hasAddBlood = true;
			base.StartCoroutine(this.ShowObjectDelayTime(this.addBloodTip, this.addArmorBtn.gameObject, this.addArmorTip, 2f));
		}
		this.GenMediKitEvent();
	}

	// Token: 0x0600148A RID: 5258 RVA: 0x000B1EAC File Offset: 0x000B02AC
	public void AddArmorBtnPressed()
	{
		if (!this.hasAddArmor)
		{
			this.hasAddArmor = true;
			base.StartCoroutine(this.ShowObjectDelayTime(this.addArmorTip, this.killEnemyTip, null, 2f));
			base.StartCoroutine(this.ShowTargetEffect(2f));
		}
		this.GenArmorEvent();
	}

	// Token: 0x0600148B RID: 5259 RVA: 0x000B1F04 File Offset: 0x000B0304
	public void LastWeaponBtnPressed()
	{
		if (!this.hasSwitchWeapon)
		{
			this.hasSwitchWeapon = true;
			base.StartCoroutine(this.ShowObjectDelayTime(this.weaponSwitchTip, this.addBloodBtn.gameObject, this.addBloodTip, 3f));
		}
		this.GenSwitchLeftEvent();
		this.weaponSprite.spriteName = this.GetCurWeaponLogoName();
	}

	// Token: 0x0600148C RID: 5260 RVA: 0x000B1F64 File Offset: 0x000B0364
	public void NextWeaponBtnPressed()
	{
		if (!this.hasSwitchWeapon)
		{
			this.hasSwitchWeapon = true;
			base.StartCoroutine(this.ShowObjectDelayTime(this.weaponSwitchTip, this.addBloodBtn.gameObject, this.addBloodTip, 3f));
		}
		this.GenSwitchRightEvent();
		this.weaponSprite.spriteName = this.GetCurWeaponLogoName();
	}

	// Token: 0x0600148D RID: 5261 RVA: 0x000B1FC3 File Offset: 0x000B03C3
	public void AimBtnPressed()
	{
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x000B1FC5 File Offset: 0x000B03C5
	public void SniperCancelBtnPressed()
	{
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x000B1FC8 File Offset: 0x000B03C8
	public void JumpBtnPressed()
	{
		if (!this.hasJumped)
		{
			this.hasJumped = true;
			base.StartCoroutine(this.ShowObjectDelayTime(this.jumpTip, this.reloadBtn.gameObject, this.reloadTip, 2f));
		}
		this.GenJumpEvent();
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x000B2018 File Offset: 0x000B0418
	public void ReloadBtnPressed()
	{
		if (!this.hasReloaded)
		{
			this.hasReloaded = true;
			base.StartCoroutine(this.ShowObjectDelayTime(this.reloadTip, this.addClipBtn.gameObject, this.addClipTip, 2f));
		}
		this.GenReloadEvent();
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x000B2068 File Offset: 0x000B0468
	public void AddClipBtnPressed()
	{
		if (!this.hasAddClip)
		{
			this.hasSwitchWeapon = false;
			this.hasAddClip = true;
			base.StartCoroutine(this.ShowObjectDelayTime(this.addClipTip, this.weaponLogoObject, this.weaponSwitchTip, 2f));
		}
		this.GenAddBulletEvent();
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x000B20B8 File Offset: 0x000B04B8
	public void PauseBtnPressed()
	{
		EventDelegate btnEventName = new EventDelegate(this, "MainMenuBtnPressed");
		EventDelegate btnEventName2 = new EventDelegate(this, "ResumeBtnPressed");
		UITipController.mInstance.SetTipData(UITipController.TipType.TwoButtonTip, "Do you want to end the training?", Color.white, "YES", "NO", btnEventName, btnEventName2, null);
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x000B20FF File Offset: 0x000B04FF
	public void MainMenuBtnPressed()
	{
		UISceneManager.mInstance.LoadLevel("MainMenu");
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x000B2110 File Offset: 0x000B0510
	public void ResumeBtnPressed()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x000B211C File Offset: 0x000B051C
	public void FireStart()
	{
		this.GenFireStartEvent();
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x000B2124 File Offset: 0x000B0524
	public void FireEnd()
	{
		this.GenFireEndEvent();
		if (!this.hasFired)
		{
			this.hasFired = true;
			base.StartCoroutine(this.ShowObjectDelayTime(this.fireTip, this.throwGrenadeBtn.gameObject, this.throwGrenadeTip, 2f));
		}
	}

	// Token: 0x06001497 RID: 5271 RVA: 0x000B2174 File Offset: 0x000B0574
	private IEnumerator CompleteTrainingEvent(float time)
	{
		yield return new WaitForSeconds(time);
		EventDelegate ev = new EventDelegate(this, "MainMenuBtnPressed");
		EventDelegate ev2 = new EventDelegate(this, "ResumeBtnPressed");
		UITipController.mInstance.SetTipData(UITipController.TipType.TwoButtonTip, "Well done! Go to fight now?", Color.white, "YES", "NO", ev, ev2, null);
		yield break;
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x000B2198 File Offset: 0x000B0598
	public void ShotOneBullEye()
	{
		if (this.hitNum < 4)
		{
			if (this.hitNum == 0)
			{
				this.killEnemyTipLabel.gameObject.SetActive(false);
			}
			this.hitNum++;
			this.hitNumLabel.text = this.hitNum.ToString() + "/5";
		}
		else if (!this.hasComplete)
		{
			this.hasComplete = true;
			this.hitNum++;
			this.hitNumLabel.text = this.hitNum.ToString() + "/5";
			base.StartCoroutine(this.CompleteTrainingEvent(1.5f));
		}
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x000B2260 File Offset: 0x000B0660
	private IEnumerator ShowTargetEffect(float time)
	{
		yield return new WaitForSeconds(time);
		for (int i = 0; i < 5; i++)
		{
			this.targetEffect[i].transform.parent.GetComponent<Collider>().enabled = true;
			this.targetEffect[i].Play();
		}
		yield break;
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x000B2284 File Offset: 0x000B0684
	private string GetCurWeaponLogoName()
	{
		string name = GrowthManagerKit.GetAllWeaponNameList()[this.mNetworkCharacter.mWeaponType - 1];
		GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(name);
		return weaponItemInfoByName.mLogoSpriteName;
	}

	// Token: 0x04001745 RID: 5957
	public static UIHelpDirector mInstance;

	// Token: 0x04001746 RID: 5958
	public GameObject fireTip;

	// Token: 0x04001747 RID: 5959
	public GameObject jumpTip;

	// Token: 0x04001748 RID: 5960
	public GameObject throwGrenadeTip;

	// Token: 0x04001749 RID: 5961
	public GameObject rotateTip;

	// Token: 0x0400174A RID: 5962
	public GameObject joystickTip;

	// Token: 0x0400174B RID: 5963
	public GameObject reloadTip;

	// Token: 0x0400174C RID: 5964
	public GameObject addClipTip;

	// Token: 0x0400174D RID: 5965
	public GameObject addBloodTip;

	// Token: 0x0400174E RID: 5966
	public GameObject addArmorTip;

	// Token: 0x0400174F RID: 5967
	public GameObject weaponSwitchTip;

	// Token: 0x04001750 RID: 5968
	public GameObject killEnemyTip;

	// Token: 0x04001751 RID: 5969
	public UILabel killEnemyTipLabel;

	// Token: 0x04001752 RID: 5970
	public UILabel welcomeLabel;

	// Token: 0x04001753 RID: 5971
	public ParticleSystem[] targetEffect;

	// Token: 0x04001754 RID: 5972
	public UIButton fireBtn;

	// Token: 0x04001755 RID: 5973
	public UIButton reloadBtn;

	// Token: 0x04001756 RID: 5974
	public UIButton jumpBtn;

	// Token: 0x04001757 RID: 5975
	public UIButton throwGrenadeBtn;

	// Token: 0x04001758 RID: 5976
	public UIButton addClipBtn;

	// Token: 0x04001759 RID: 5977
	public UIButton addBloodBtn;

	// Token: 0x0400175A RID: 5978
	public UIButton addArmorBtn;

	// Token: 0x0400175B RID: 5979
	public GameObject weaponLogoObject;

	// Token: 0x0400175C RID: 5980
	public GameObject joystickObject;

	// Token: 0x0400175D RID: 5981
	public UILabel clipLabel;

	// Token: 0x0400175E RID: 5982
	public UILabel armorLabel;

	// Token: 0x0400175F RID: 5983
	public UILabel bloodLabel;

	// Token: 0x04001760 RID: 5984
	public UISprite weaponSprite;

	// Token: 0x04001761 RID: 5985
	public GameObject pauseNode;

	// Token: 0x04001762 RID: 5986
	private bool hasFired;

	// Token: 0x04001763 RID: 5987
	private bool hasThrowGrenade;

	// Token: 0x04001764 RID: 5988
	private bool hasJumped;

	// Token: 0x04001765 RID: 5989
	private bool hasReloaded;

	// Token: 0x04001766 RID: 5990
	private bool hasAddBlood;

	// Token: 0x04001767 RID: 5991
	private bool hasAddClip;

	// Token: 0x04001768 RID: 5992
	private bool hasAddArmor;

	// Token: 0x04001769 RID: 5993
	private bool hasSwitchWeapon = true;

	// Token: 0x0400176A RID: 5994
	private GGNetworkCharacter mNetworkCharacter;

	// Token: 0x0400176B RID: 5995
	private GGWeaponManager mWeaponManager;

	// Token: 0x0400176C RID: 5996
	private int bloodNum;

	// Token: 0x0400176D RID: 5997
	private int armorNum;

	// Token: 0x04001771 RID: 6001
	private bool cancelFire;

	// Token: 0x0400177C RID: 6012
	private int hitNum;

	// Token: 0x0400177D RID: 6013
	private bool hasComplete;

	// Token: 0x0400177E RID: 6014
	public UILabel hitNumLabel;

	// Token: 0x020002BB RID: 699
	// (Invoke) Token: 0x0600149C RID: 5276
	public delegate void HelpJumpEventHandler();

	// Token: 0x020002BC RID: 700
	// (Invoke) Token: 0x060014A0 RID: 5280
	public delegate void HelpSniperCancelEventHandler();

	// Token: 0x020002BD RID: 701
	// (Invoke) Token: 0x060014A4 RID: 5284
	public delegate void HelpReloadEventHandler();

	// Token: 0x020002BE RID: 702
	// (Invoke) Token: 0x060014A8 RID: 5288
	public delegate void HelpFireStartEventHandler();

	// Token: 0x020002BF RID: 703
	// (Invoke) Token: 0x060014AC RID: 5292
	public delegate void HelpFireEndEventHandler();

	// Token: 0x020002C0 RID: 704
	// (Invoke) Token: 0x060014B0 RID: 5296
	public delegate void HelpJumpStartEventHandler();

	// Token: 0x020002C1 RID: 705
	// (Invoke) Token: 0x060014B4 RID: 5300
	public delegate void HelpJumpEndEventHandler();

	// Token: 0x020002C2 RID: 706
	// (Invoke) Token: 0x060014B8 RID: 5304
	public delegate void HelpSwitchLeftEventHandler();

	// Token: 0x020002C3 RID: 707
	// (Invoke) Token: 0x060014BC RID: 5308
	public delegate void HelpSwitchRightEventHandler();

	// Token: 0x020002C4 RID: 708
	// (Invoke) Token: 0x060014C0 RID: 5312
	public delegate void HelpAddBulletEventHandler();

	// Token: 0x020002C5 RID: 709
	// (Invoke) Token: 0x060014C4 RID: 5316
	public delegate void HelpArmorEventHandler();

	// Token: 0x020002C6 RID: 710
	// (Invoke) Token: 0x060014C8 RID: 5320
	public delegate void HelpMediKitEventHandler();

	// Token: 0x020002C7 RID: 711
	// (Invoke) Token: 0x060014CC RID: 5324
	public delegate void HelpThrowWeaponFireEventHandler(int index);
}
