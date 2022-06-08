using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GrowthSystem;
using UnityEngine;

// Token: 0x02000299 RID: 665
public class UIPlayDirector : MonoBehaviour
{
	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06001349 RID: 4937 RVA: 0x000ADF24 File Offset: 0x000AC324
	// (remove) Token: 0x0600134A RID: 4938 RVA: 0x000ADF58 File Offset: 0x000AC358
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.JumpEventHandler OnJump;

	// Token: 0x0600134B RID: 4939 RVA: 0x000ADF8C File Offset: 0x000AC38C
	public void GenJumpEvent()
	{
		if (UIPlayDirector.OnJump != null)
		{
			UIPlayDirector.OnJump();
		}
	}

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x0600134C RID: 4940 RVA: 0x000ADFA4 File Offset: 0x000AC3A4
	// (remove) Token: 0x0600134D RID: 4941 RVA: 0x000ADFD8 File Offset: 0x000AC3D8
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.AimEventHandler OnAim;

	// Token: 0x0600134E RID: 4942 RVA: 0x000AE00C File Offset: 0x000AC40C
	public void GenAimEvent()
	{
		if (UIPlayDirector.OnAim != null)
		{
			UIPlayDirector.OnAim();
		}
	}

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x0600134F RID: 4943 RVA: 0x000AE024 File Offset: 0x000AC424
	// (remove) Token: 0x06001350 RID: 4944 RVA: 0x000AE058 File Offset: 0x000AC458
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.SniperCancelEventHandler OnSniperCancel;

	// Token: 0x06001351 RID: 4945 RVA: 0x000AE08C File Offset: 0x000AC48C
	public void GenSniperCancelEvent()
	{
		if (UIPlayDirector.OnSniperCancel != null)
		{
			UIPlayDirector.OnSniperCancel();
		}
	}

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06001352 RID: 4946 RVA: 0x000AE0A4 File Offset: 0x000AC4A4
	// (remove) Token: 0x06001353 RID: 4947 RVA: 0x000AE0D8 File Offset: 0x000AC4D8
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.ReloadEventHandler OnReload;

	// Token: 0x06001354 RID: 4948 RVA: 0x000AE10C File Offset: 0x000AC50C
	public void GenReloadEvent()
	{
		if (UIPlayDirector.OnReload != null)
		{
			UIPlayDirector.OnReload();
		}
	}

	// Token: 0x06001355 RID: 4949 RVA: 0x000AE122 File Offset: 0x000AC522
	public void FireOnPress()
	{
		if (UIUserDataController.GetSniperMode() == 1)
		{
			this.cancelFire = false;
		}
		this.GenFireStartEvent();
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x000AE13C File Offset: 0x000AC53C
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

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x06001357 RID: 4951 RVA: 0x000AE168 File Offset: 0x000AC568
	// (remove) Token: 0x06001358 RID: 4952 RVA: 0x000AE19C File Offset: 0x000AC59C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.FireStartEventHandler OnFireStart;

	// Token: 0x06001359 RID: 4953 RVA: 0x000AE1D0 File Offset: 0x000AC5D0
	public void GenFireStartEvent()
	{
		if (UIPlayDirector.OnFireStart != null)
		{
			UIPlayDirector.OnFireStart();
		}
	}

	// Token: 0x1400000B RID: 11
	// (add) Token: 0x0600135A RID: 4954 RVA: 0x000AE1E8 File Offset: 0x000AC5E8
	// (remove) Token: 0x0600135B RID: 4955 RVA: 0x000AE21C File Offset: 0x000AC61C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.FireEndEventHandler OnFireEnd;

	// Token: 0x0600135C RID: 4956 RVA: 0x000AE250 File Offset: 0x000AC650
	public void GenFireEndEvent()
	{
		if (UIPlayDirector.OnFireEnd != null)
		{
			UIPlayDirector.OnFireEnd();
		}
	}

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x0600135D RID: 4957 RVA: 0x000AE268 File Offset: 0x000AC668
	// (remove) Token: 0x0600135E RID: 4958 RVA: 0x000AE29C File Offset: 0x000AC69C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.JumpStartEventHandler OnJumpStart;

	// Token: 0x0600135F RID: 4959 RVA: 0x000AE2D0 File Offset: 0x000AC6D0
	public void GenJumpStartEvent()
	{
		if (UIPlayDirector.OnJumpStart != null)
		{
			UIPlayDirector.OnJumpStart();
		}
	}

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06001360 RID: 4960 RVA: 0x000AE2E8 File Offset: 0x000AC6E8
	// (remove) Token: 0x06001361 RID: 4961 RVA: 0x000AE31C File Offset: 0x000AC71C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.JumpEndEventHandler OnJumpEnd;

	// Token: 0x06001362 RID: 4962 RVA: 0x000AE350 File Offset: 0x000AC750
	public void GenJumpEndEvent()
	{
		if (UIPlayDirector.OnJumpEnd != null)
		{
			UIPlayDirector.OnJumpEnd();
		}
	}

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x06001363 RID: 4963 RVA: 0x000AE368 File Offset: 0x000AC768
	// (remove) Token: 0x06001364 RID: 4964 RVA: 0x000AE39C File Offset: 0x000AC79C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.SwitchLeftEventHandler OnSwitchLeft;

	// Token: 0x06001365 RID: 4965 RVA: 0x000AE3D0 File Offset: 0x000AC7D0
	public void GenSwitchLeftEvent()
	{
		if (UIPlayDirector.OnSwitchLeft != null)
		{
			UIPlayDirector.OnSwitchLeft();
		}
	}

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06001366 RID: 4966 RVA: 0x000AE3E8 File Offset: 0x000AC7E8
	// (remove) Token: 0x06001367 RID: 4967 RVA: 0x000AE41C File Offset: 0x000AC81C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.SwitchRightEventHandler OnSwitchRight;

	// Token: 0x06001368 RID: 4968 RVA: 0x000AE450 File Offset: 0x000AC850
	public void GenSwitchRightEvent()
	{
		if (UIPlayDirector.OnSwitchRight != null)
		{
			UIPlayDirector.OnSwitchRight();
		}
	}

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x06001369 RID: 4969 RVA: 0x000AE468 File Offset: 0x000AC868
	// (remove) Token: 0x0600136A RID: 4970 RVA: 0x000AE49C File Offset: 0x000AC89C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.AddBulletEventHandler OnAddBullet;

	// Token: 0x0600136B RID: 4971 RVA: 0x000AE4D0 File Offset: 0x000AC8D0
	public void GenAddBulletEvent()
	{
		if (UIPlayDirector.OnAddBullet != null)
		{
			UIPlayDirector.OnAddBullet();
		}
	}

	// Token: 0x14000011 RID: 17
	// (add) Token: 0x0600136C RID: 4972 RVA: 0x000AE4E8 File Offset: 0x000AC8E8
	// (remove) Token: 0x0600136D RID: 4973 RVA: 0x000AE51C File Offset: 0x000AC91C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.ArmorEventHandler OnArmor;

	// Token: 0x0600136E RID: 4974 RVA: 0x000AE550 File Offset: 0x000AC950
	public void GenArmorEvent()
	{
		if (UIPlayDirector.OnArmor != null)
		{
			UIPlayDirector.OnArmor();
		}
	}

	// Token: 0x14000012 RID: 18
	// (add) Token: 0x0600136F RID: 4975 RVA: 0x000AE568 File Offset: 0x000AC968
	// (remove) Token: 0x06001370 RID: 4976 RVA: 0x000AE59C File Offset: 0x000AC99C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.MediKitEventHandler OnMediKit;

	// Token: 0x06001371 RID: 4977 RVA: 0x000AE5D0 File Offset: 0x000AC9D0
	public void GenMediKitEvent()
	{
		if (UIPlayDirector.OnMediKit != null)
		{
			UIPlayDirector.OnMediKit();
		}
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x000AE5E6 File Offset: 0x000AC9E6
	private void Awake()
	{
		UIPlayDirector.mInstance = this;
		GrowthManger.OnGrowthPrompt += this.OnGrowthPrompt;
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x000AE5FF File Offset: 0x000AC9FF
	private void OnDisable()
	{
		GrowthManger.OnGrowthPrompt -= this.OnGrowthPrompt;
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x000AE612 File Offset: 0x000ACA12
	private void OnDestroy()
	{
		if (UIPlayDirector.mInstance != null)
		{
			UIPlayDirector.mInstance = null;
		}
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x000AE62A File Offset: 0x000ACA2A
	private void Start()
	{
		this.ggPlayModeType = GGNetworkKit.mInstance.GetPlayMode();
		this.Init();
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x000AE644 File Offset: 0x000ACA44
	private void Update()
	{
		if (this.mNetworkCharacter != null)
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
		else if (Time.frameCount % 16 == 0 && GameObject.FindWithTag("Player") != null)
		{
			this.mNetworkCharacter = GameObject.FindWithTag("Player").GetComponent<GGNetworkCharacter>();
		}
		if (Time.frameCount % 4 == 0)
		{
			if (this.energyBarNode.activeSelf)
			{
				if (this.mWeaponManager.GetAmmoStr() != this.energyBarSprite.fillAmount.ToString())
				{
					this.energyBarSprite.fillAmount = float.Parse(this.mWeaponManager.GetAmmoStr());
				}
			}
			else if (this.bulletLabel.text != this.mWeaponManager.GetAmmoStr())
			{
				this.bulletLabel.text = this.mWeaponManager.GetAmmoStr();
			}
		}
		this.fTime += Time.deltaTime;
		if (this.fTime > 1.2f && this.mPopMessageList.Count > 0)
		{
			this.PopMessageEvent();
			this.fTime = 0f;
		}
		if (!this.canUseMediKit)
		{
			this.fMediTime += Time.deltaTime;
			this.mediKitBackgroudF.fillAmount = 1f - this.fMediTime / 180f;
			if (this.fMediTime >= 180f)
			{
				this.fMediTime = 0f;
				this.canUseMediKit = true;
				this.mediKitBackgroudF.gameObject.SetActive(false);
			}
		}
		this.UpdateThrowWeaponSpriteFgFillAmount();
		this.UpdateBuffBar();
		if (this.ggPlayModeType == GGPlayModeType.Entertainment)
		{
			this.EnchantmentLogoRefreshTimer();
		}
		this.FireworkCDTimer();
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x000AE8A4 File Offset: 0x000ACCA4
	private void InitAsReset()
	{
		this.exampleKillPopPrefab.SetActive(false);
		this.mediKitBackgroudF.gameObject.SetActive(false);
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

	// Token: 0x06001378 RID: 4984 RVA: 0x000AE93C File Offset: 0x000ACD3C
	public void Reset()
	{
		this.exampleKillPopPrefab.SetActive(false);
		this.mediKitBackgroudF.gameObject.SetActive(false);
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
		UIModeDirector.mInstance.ResetInitUI();
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x000AE9DC File Offset: 0x000ACDDC
	public void Init()
	{
		this.InitAsReset();
		this.throwWeaponFgSprite.gameObject.SetActive(false);
		this.RefreshThrowWeapon();
		this.coinTipLabel.gameObject.SetActive(false);
		this.expTipLabel.gameObject.SetActive(false);
		this.armorLogoSprite.spriteName = GrowthManagerKit.GetCurSettedArmorInfo().mInGameLogoSpriteName;
		this.InitBuffBar();
		if (!VideoRecordDirector.IsEnabled())
		{
			this.videoRecordToggle.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x000AEA60 File Offset: 0x000ACE60
	private string GetCurWeaponLogoName()
	{
		string name = GrowthManagerKit.GetAllWeaponNameList()[this.mNetworkCharacter.mWeaponType - 1];
		GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(name);
		return weaponItemInfoByName.mLogoSpriteName;
	}

	// Token: 0x0600137B RID: 4987 RVA: 0x000AEA90 File Offset: 0x000ACE90
	private int GetCurWeaponClipPrice()
	{
		string name = GrowthManagerKit.GetAllWeaponNameList()[this.mNetworkCharacter.mWeaponType - 1];
		GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(name);
		return weaponItemInfoByName.mClipPrice;
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x000AEAC0 File Offset: 0x000ACEC0
	private int GetArmorPrice()
	{
		string name = GrowthManagerKit.GetAllWeaponNameList()[this.mNetworkCharacter.mWeaponType - 1];
		GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(name);
		return weaponItemInfoByName.mClipPrice;
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x000AEAF0 File Offset: 0x000ACEF0
	private int GetMediKitPrice()
	{
		string name = GrowthManagerKit.GetAllWeaponNameList()[this.mNetworkCharacter.mWeaponType - 1];
		GWeaponItemInfo weaponItemInfoByName = GrowthManagerKit.GetWeaponItemInfoByName(name);
		return weaponItemInfoByName.mClipPrice;
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x000AEB1E File Offset: 0x000ACF1E
	public void PauseBtnPressed()
	{
		this.pauseNode.SetActive(true);
		UIPauseDirector.mInstance.ShowLobbyNode();
	}

	// Token: 0x0600137F RID: 4991 RVA: 0x000AEB38 File Offset: 0x000ACF38
	public void StoreBtnPressed()
	{
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting && GGNetworkKit.mInstance.IsMasterClient())
		{
			GGNetworkKit.mInstance.SwitchMasterClient();
		}
		this.storeNode.SetActive(true);
		UIPauseDirector.mInstance.isCutControl = true;
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x000AEB88 File Offset: 0x000ACF88
	public void ChatBtnPressed()
	{
		this.chatNode.SetActive(true);
		UIPauseDirector.mInstance.isCutControl = true;
		if (UIChatSystemDirector.mInstance.isFirstOpen)
		{
			UIChatSystemDirector.mInstance.isFirstOpen = false;
			UIChatSystemDirector.mInstance.chatToAllToggle.value = true;
		}
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x000AEBD8 File Offset: 0x000ACFD8
	public void MedikitBtnPressed()
	{
		if (this.bloodNum < 100 && this.canUseMediKit)
		{
			if (GrowthManagerKit.GetCoins() >= 30)
			{
				base.GetComponent<AudioSource>().PlayOneShot(this.clips[1]);
				GrowthManagerKit.SubCoins(30);
				this.canUseMediKit = false;
				this.mediKitBackgroudF.gameObject.SetActive(true);
				this.coinTipLabel.text = "- " + 30.ToString();
				this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
				this.GenMediKitEvent();
			}
			else
			{
				this.coinTipLabel.text = "- Lack";
				this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
			}
		}
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x000AECAC File Offset: 0x000AD0AC
	public void ArmorBtnPressed()
	{
		if (this.armorNum < 100)
		{
			int mCoinsPriceInGame = this.mNetworkCharacter.myArmorInfo.mCoinsPriceInGame;
			if (GrowthManagerKit.GetCoins() >= mCoinsPriceInGame)
			{
				this.coinTipLabel.text = "- " + mCoinsPriceInGame.ToString();
				this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
				base.GetComponent<AudioSource>().PlayOneShot(this.clips[2]);
				GrowthManagerKit.SubCoins(mCoinsPriceInGame);
				this.GenArmorEvent();
			}
			else
			{
				this.coinTipLabel.text = "- Lack";
				this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
			}
		}
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x000AED68 File Offset: 0x000AD168
	public void AddClipBtnPressed()
	{
		int curWeaponClipPrice = this.GetCurWeaponClipPrice();
		if (curWeaponClipPrice > 0)
		{
			if (GrowthManagerKit.GetCoins() >= curWeaponClipPrice)
			{
				this.coinTipLabel.text = "- " + curWeaponClipPrice.ToString();
				this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
				GrowthManagerKit.SubCoins(curWeaponClipPrice);
				base.GetComponent<AudioSource>().PlayOneShot(this.clips[3]);
				this.GenAddBulletEvent();
			}
			else
			{
				this.coinTipLabel.text = "- Lack";
				this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
			}
		}
		else if (curWeaponClipPrice == 0)
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.clips[3]);
			this.expTipLabel.text = "Free(LV < 5)";
			this.ShowSubCoinLabel(this.expTipLabel.gameObject, null, 1f);
			this.GenAddBulletEvent();
		}
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x000AEE60 File Offset: 0x000AD260
	public void LastWeaponBtnPressed()
	{
		this.GenSwitchLeftEvent();
		if (this.GetCurWeaponLogoName() == "GItemLogo_1_9_1_0" || this.GetCurWeaponLogoName() == "GItemLogo_1_9_2_0" || this.GetCurWeaponLogoName() == "GItemLogo_1_9_3_0" || this.GetCurWeaponLogoName() == "GItemLogo_1_9_4_0" || this.GetCurWeaponLogoName() == "GItemLogo_1_9_5_0")
		{
			return;
		}
		this.weaponSprite.spriteName = this.GetCurWeaponLogoName();
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x000AEEF0 File Offset: 0x000AD2F0
	public void NextWeaponBtnPressed()
	{
		this.GenSwitchRightEvent();
		if (this.GetCurWeaponLogoName() == "GItemLogo_1_9_1_0" || this.GetCurWeaponLogoName() == "GItemLogo_1_9_2_0" || this.GetCurWeaponLogoName() == "GItemLogo_1_9_3_0" || this.GetCurWeaponLogoName() == "GItemLogo_1_9_4_0" || this.GetCurWeaponLogoName() == "GItemLogo_1_9_5_0")
		{
			return;
		}
		this.weaponSprite.spriteName = this.GetCurWeaponLogoName();
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x000AEF7E File Offset: 0x000AD37E
	public void AimBtnPressed()
	{
		this.GenAimEvent();
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x000AEF86 File Offset: 0x000AD386
	public void SniperCancelBtnPressed()
	{
		this.cancelFire = true;
		this.GenSniperCancelEvent();
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x000AEF95 File Offset: 0x000AD395
	public void JumpBtnPressed()
	{
		this.GenJumpStartEvent();
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x000AEF9D File Offset: 0x000AD39D
	public void ReloadBtnPressed()
	{
		this.GenReloadEvent();
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x000AEFA5 File Offset: 0x000AD3A5
	private void ShowSubCoinLabel(GameObject coinLabel, GameObject hiddenObject, float time)
	{
		if (hiddenObject != null)
		{
			hiddenObject.SetActive(false);
		}
		coinLabel.SetActive(true);
		base.StartCoroutine(this.RecoverShowState(coinLabel, hiddenObject, time));
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x000AEFD4 File Offset: 0x000AD3D4
	private IEnumerator RecoverShowState(GameObject coinLabel, GameObject hiddenObject, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		if (hiddenObject != null)
		{
			hiddenObject.SetActive(true);
		}
		coinLabel.SetActive(false);
		yield break;
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x000AEFFD File Offset: 0x000AD3FD
	public void NoBulletTip()
	{
		this.bulletLabel.GetComponent<TweenColor>().Play();
		this.bulletLabel.GetComponent<TweenScale>().Play();
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x000AF020 File Offset: 0x000AD420
	public void TweenFinished()
	{
		this.bulletLabel.GetComponent<TweenColor>().ResetToBeginning();
		this.bulletLabel.GetComponent<TweenScale>().ResetToBeginning();
		this.bulletLabel.color = this.tempColor;
		this.bulletLabel.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x000AF084 File Offset: 0x000AD484
	public void ShowEnergyBarNode()
	{
		if (this.bulletLabel.gameObject.activeSelf)
		{
			this.bulletLabel.gameObject.SetActive(false);
		}
		if (!this.energyBarNode.activeSelf)
		{
			this.energyBarNode.SetActive(true);
		}
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x000AF0D4 File Offset: 0x000AD4D4
	public void HideEnergyBarNode()
	{
		if (!this.bulletLabel.gameObject.activeSelf)
		{
			this.bulletLabel.gameObject.SetActive(true);
		}
		if (this.energyBarNode.activeSelf)
		{
			this.energyBarNode.SetActive(false);
		}
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x000AF124 File Offset: 0x000AD524
	private void CreatKillPopObject(string spriteName, Transform parentTransform)
	{
		this.newKillPopObject = UnityEngine.Object.Instantiate<GameObject>(this.killPopPrefab, this.killPopPrefab.transform.position, this.killPopPrefab.transform.rotation);
		this.newKillPopObject.transform.parent = parentTransform;
		this.newKillPopObject.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.newKillPopObject.transform.localScale = new Vector3(1f, 1f, 1f);
		this.newKillPopObject.GetComponent<UIKillSpritePopPrefab>().SetKillSprite(spriteName);
		UnityEngine.Object.Destroy(this.newKillPopObject, 2f);
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x000AF1DC File Offset: 0x000AD5DC
	public void PopMessageEvent()
	{
		GrowthPrometType[] array = new GrowthPrometType[2];
		int[] array2 = new int[2];
		array[0] = (array[1] = GrowthPrometType.Nil);
		string spriteName = string.Empty;
		if (this.mPopMessageList.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < this.mPopMessageTypeList.Count; i++)
		{
			if (this.mPopMessageTypeList[i] == GrowthPrometType.CoinsAdd || this.mPopMessageTypeList[i] == GrowthPrometType.ExpAdd || this.mPopMessageTypeList[i] == GrowthPrometType.ScoreInMutation)
			{
				array[0] = this.mPopMessageTypeList[i];
				array2[0] = i;
				break;
			}
		}
		for (int j = 0; j < this.mPopMessageTypeList.Count; j++)
		{
			if (this.mPopMessageTypeList[j] == GrowthPrometType.Killing)
			{
				array[1] = this.mPopMessageTypeList[j];
				array2[1] = j;
				break;
			}
		}
		if (array[0] == GrowthPrometType.CoinsAdd)
		{
			this.coinTipLabel.text = this.mPopMessageList[array2[0]].ToUpper();
			this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
		}
		else if (array[0] == GrowthPrometType.ExpAdd)
		{
			this.expTipLabel.text = this.mPopMessageList[array2[0]];
			this.ShowSubCoinLabel(this.expTipLabel.gameObject, null, 1f);
		}
		else if (array[0] == GrowthPrometType.ScoreInMutation)
		{
			this.expTipLabel.text = this.mPopMessageList[array2[0]];
			this.ShowSubCoinLabel(this.expTipLabel.gameObject, null, 1f);
		}
		if (array[1] == GrowthPrometType.Killing)
		{
			spriteName = this.mPopMessageList[array2[1]];
			this.CreatKillPopObject(spriteName, this.controlMiddleNode.transform);
		}
		if (array[0] != GrowthPrometType.Nil && array[1] != GrowthPrometType.Nil)
		{
			if (array2[0] < array2[1])
			{
				this.mPopMessageList.RemoveAt(array2[1]);
				this.mPopMessageTypeList.RemoveAt(array2[1]);
				this.mPopMessageList.RemoveAt(array2[0]);
				this.mPopMessageTypeList.RemoveAt(array2[0]);
			}
			else
			{
				this.mPopMessageList.RemoveAt(array2[0]);
				this.mPopMessageTypeList.RemoveAt(array2[0]);
				this.mPopMessageList.RemoveAt(array2[1]);
				this.mPopMessageTypeList.RemoveAt(array2[1]);
			}
		}
		else
		{
			if (array[0] != GrowthPrometType.Nil)
			{
				this.mPopMessageList.RemoveAt(array2[0]);
				this.mPopMessageTypeList.RemoveAt(array2[0]);
			}
			if (array[1] != GrowthPrometType.Nil)
			{
				this.mPopMessageList.RemoveAt(array2[1]);
				this.mPopMessageTypeList.RemoveAt(array2[1]);
			}
		}
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x000AF4A0 File Offset: 0x000AD8A0
	public void PopMessageEvent(GrowthPrometType type)
	{
		string spriteName = string.Empty;
		if (this.mPopMessageList.Count <= 0)
		{
			return;
		}
		if (type != GrowthPrometType.CoinsAdd)
		{
			if (type != GrowthPrometType.ExpAdd)
			{
				if (type != GrowthPrometType.Killing)
				{
					if (type == GrowthPrometType.ScoreInMutation)
					{
						this.expTipLabel.text = this.mPopMessageList[0];
						this.ShowSubCoinLabel(this.expTipLabel.gameObject, null, 1f);
					}
				}
				else
				{
					spriteName = this.mPopMessageList[0];
					this.CreatKillPopObject(spriteName, this.controlMiddleNode.transform);
				}
			}
			else
			{
				this.expTipLabel.text = this.mPopMessageList[0];
				this.ShowSubCoinLabel(this.expTipLabel.gameObject, null, 1f);
			}
		}
		else
		{
			this.coinTipLabel.text = this.mPopMessageList[0].ToUpper();
			this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
		}
		this.mPopMessageList.RemoveAt(0);
		this.mPopMessageTypeList.RemoveAt(0);
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x000AF5C8 File Offset: 0x000AD9C8
	private void OnGrowthPrompt(GrowthPrometType type, int num, string description)
	{
		if (type != GrowthPrometType.CoinsAdd)
		{
			if (type != GrowthPrometType.ExpAdd)
			{
				if (type != GrowthPrometType.Killing)
				{
					if (type != GrowthPrometType.ScoreInMutation)
					{
						if (type != GrowthPrometType.ArmorBody)
						{
							if (type != GrowthPrometType.ArmorHead)
							{
							}
						}
					}
					else
					{
						this.mPopMessageTypeList.Add(type);
						if (num >= 0)
						{
							this.mPopMessageList.Add("Score + " + num.ToString());
						}
						else
						{
							this.mPopMessageList.Add("Score - " + Mathf.Abs(num).ToString());
						}
					}
				}
				else
				{
					this.mPopMessageTypeList.Insert(0, type);
					this.mPopMessageList.Insert(0, description);
				}
			}
			else
			{
				this.mPopMessageTypeList.Add(type);
				this.mPopMessageList.Add("Exp + " + num);
			}
		}
		else
		{
			this.mPopMessageTypeList.Add(type);
			this.mPopMessageList.Add("+ " + num.ToString());
		}
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x000AF700 File Offset: 0x000ADB00
	public void RefreshThrowWeapon()
	{
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.KnifeCompetition || GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting)
		{
			this.throwWeaponBtn.gameObject.SetActive(false);
			this.throwWeaponFgSprite.gameObject.SetActive(false);
			this.throwWeaponFgSprite2.gameObject.SetActive(false);
		}
		else
		{
			this.throwWeaponIndex = UIUserDataController.GetQuickBarItemIndex();
			if (this.throwWeaponIndex == UIUserDataController.allThrowWeapon.Length - 1)
			{
				this.throwWeaponBtn.gameObject.SetActive(false);
				this.throwWeaponFgSprite.gameObject.SetActive(false);
				this.throwWeaponFgSprite2.gameObject.SetActive(false);
			}
			else
			{
				GameObject mainPlayer = GGNetworkKit.mInstance.GetMainPlayer();
				if (GGMutationModeControl.mInstance != null)
				{
					if (mainPlayer.GetComponent<GGNetworkCharacter>().mPlayerProperties.team == GGTeamType.blue && !this.throwWeaponBtn.gameObject.activeSelf)
					{
						this.throwWeaponBtn.gameObject.SetActive(true);
					}
					if (!GGMutationModeControl.mInstance.isGotoGameScene)
					{
						this.throwWeaponFgSprite2.gameObject.SetActive(true);
					}
				}
				else if (!this.throwWeaponBtn.gameObject.activeSelf)
				{
					this.throwWeaponBtn.gameObject.SetActive(true);
				}
			}
			if (this.throwWeaponIndex == 3)
			{
				this.throwWeaponUseCycleTime = 30f;
			}
			else if (this.throwWeaponIndex == 4)
			{
				this.throwWeaponUseCycleTime = 16f;
			}
			else
			{
				this.throwWeaponUseCycleTime = 8f;
			}
		}
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x000AF8A0 File Offset: 0x000ADCA0
	public void ThrowWeaponBtnPressed()
	{
		if (!UIPauseDirector.mInstance.isDead)
		{
			this.throwWeaponIndex = UIUserDataController.GetQuickBarItemIndex();
			if (GrowthManagerKit.GetCoins() >= UIUserDataController.allThrowWeaponPrice[this.throwWeaponIndex])
			{
				this.GenThrowWeaponFireEvent(this.throwWeaponIndex + 1);
			}
			else
			{
				this.coinTipLabel.text = "- Lack";
				this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
			}
		}
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x000AF918 File Offset: 0x000ADD18
	public void ThrownWeaponSubCoins(int throwWeaponIndex)
	{
		GrowthManagerKit.SubCoins(UIUserDataController.allThrowWeaponPrice[throwWeaponIndex]);
		this.coinTipLabel.text = "- " + UIUserDataController.allThrowWeaponPrice[throwWeaponIndex].ToString();
		this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x000AF974 File Offset: 0x000ADD74
	public void ThrowWeaponStart()
	{
		UIUserDataController.SubThrowWeaponNum(UIUserDataController.allThrowWeapon[this.throwWeaponIndex], 1);
		this.throwWeaponBtn.isEnabled = false;
		this.canUseThrowWeapon = false;
		this.throwWeaponFgSprite.gameObject.SetActive(true);
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x000AF9AC File Offset: 0x000ADDAC
	private void UpdateThrowWeaponSpriteFgFillAmount()
	{
		if (!this.canUseThrowWeapon)
		{
			this.fThrowWeaponTime += Time.deltaTime;
			this.throwWeaponFgSprite.fillAmount = (this.throwWeaponUseCycleTime - this.fThrowWeaponTime) / this.throwWeaponUseCycleTime;
			if (this.fThrowWeaponTime >= this.throwWeaponUseCycleTime)
			{
				this.fThrowWeaponTime = 0f;
				this.canUseThrowWeapon = true;
				this.throwWeaponFgSprite.gameObject.SetActive(false);
				this.throwWeaponBtn.isEnabled = true;
			}
		}
	}

	// Token: 0x14000013 RID: 19
	// (add) Token: 0x06001399 RID: 5017 RVA: 0x000AFA38 File Offset: 0x000ADE38
	// (remove) Token: 0x0600139A RID: 5018 RVA: 0x000AFA6C File Offset: 0x000ADE6C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.ThrowWeaponFireEventHandler OnThrowWeaponFire;

	// Token: 0x0600139B RID: 5019 RVA: 0x000AFAA0 File Offset: 0x000ADEA0
	public void GenThrowWeaponFireEvent(int index)
	{
		if (UIPlayDirector.OnThrowWeaponFire != null)
		{
			UIPlayDirector.OnThrowWeaponFire(index);
		}
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x000AFAB8 File Offset: 0x000ADEB8
	private void InitBuffBar()
	{
		this.buffBarNode.SetActive(false);
		for (int i = 0; i < 5; i++)
		{
			this.isUsingBuff[i] = false;
			this.buffTime[i] = 0f;
		}
		this.buffItemInfo = GrowthManagerKit.GetAllMultiplayerBuffItemInfo();
		int num = this.buffItemInfo.Length;
		if (num > 0)
		{
			for (int j = 0; j < num; j++)
			{
				this.buffItemBtn[j].normalSprite = this.buffItemInfo[j].mLogoSpriteName;
				this.buffItemNumLabel[j].text = this.buffItemInfo[j].mExistNum.ToString();
				this.buffItemFgSprite[j].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x000AFB78 File Offset: 0x000ADF78
	private void UpdateBuffBar()
	{
		this.BuffTimer();
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x000AFB80 File Offset: 0x000ADF80
	public void BuffToggleValueChanged()
	{
		if (this.buffToggle.value)
		{
			this.buffOpen = true;
			this.OpenBuffBarBtnPressed();
		}
		else
		{
			this.buffOpen = false;
			this.CloseBuffBarBtnPressed();
		}
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x000AFBB1 File Offset: 0x000ADFB1
	public void OpenBuffBarBtnPressed()
	{
		this.buffBarNode.SetActive(true);
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x000AFBBF File Offset: 0x000ADFBF
	public void CloseBuffBarBtnPressed()
	{
		this.buffBarNode.SetActive(false);
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x000AFBCD File Offset: 0x000ADFCD
	public void BuffBarMoveFinished()
	{
		if (!this.isOpenBuffBar)
		{
			this.buffBarNode.SetActive(false);
		}
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x000AFBE8 File Offset: 0x000ADFE8
	public void BuffBtnPressed(int index)
	{
		if (this.buffItemInfo[index - 1].mExistNum > 0)
		{
			this.isUsingBuff[index - 1] = true;
			this.buffItemBtn[index - 1].isEnabled = false;
			this.buffItemFgSprite[index - 1].gameObject.SetActive(true);
			this.buffItemInfo[index - 1].UseBuff();
			this.buffItemInfo = GrowthManagerKit.GetAllMultiplayerBuffItemInfo();
			this.buffItemNumLabel[index - 1].text = this.buffItemInfo[index - 1].mExistNum.ToString();
			base.GetComponent<AudioSource>().PlayOneShot(this.clips[0]);
		}
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x000AFC94 File Offset: 0x000AE094
	private void BuffTimer()
	{
		for (int i = 0; i < 5; i++)
		{
			if (this.isUsingBuff[i])
			{
				this.buffTime[i] += Time.deltaTime;
				if (this.buffOpen)
				{
					this.buffItemFgSprite[i].fillAmount = 1f - this.buffTime[i] / this.buffItemInfo[i].mMaxEffectTime_S;
				}
				if (this.buffTime[i] >= this.buffItemInfo[i].mMaxEffectTime_S)
				{
					this.buffTime[i] = 0f;
					this.isUsingBuff[i] = false;
					this.buffItemBtn[i].isEnabled = true;
					this.buffItemFgSprite[i].gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x000AFD5C File Offset: 0x000AE15C
	public void RefreshBuffNum()
	{
		this.buffItemInfo = GrowthManagerKit.GetAllMultiplayerBuffItemInfo();
		int num = this.buffItemInfo.Length;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				this.buffItemNumLabel[i].text = this.buffItemInfo[i].mExistNum.ToString();
			}
		}
	}

	// Token: 0x14000014 RID: 20
	// (add) Token: 0x060013A5 RID: 5029 RVA: 0x000AFDBC File Offset: 0x000AE1BC
	// (remove) Token: 0x060013A6 RID: 5030 RVA: 0x000AFDF0 File Offset: 0x000AE1F0
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.JumpEventHandler OnStartMutationMode;

	// Token: 0x060013A7 RID: 5031 RVA: 0x000AFE24 File Offset: 0x000AE224
	public void GenStartMutationModeEvent()
	{
		if (UIPlayDirector.OnStartMutationMode != null)
		{
			UIPlayDirector.OnStartMutationMode();
		}
	}

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x060013A8 RID: 5032 RVA: 0x000AFE3C File Offset: 0x000AE23C
	// (remove) Token: 0x060013A9 RID: 5033 RVA: 0x000AFE70 File Offset: 0x000AE270
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.JumpEventHandler OnStartStrongholdMode;

	// Token: 0x060013AA RID: 5034 RVA: 0x000AFEA4 File Offset: 0x000AE2A4
	public void GenStartStrongholdModeEvent()
	{
		if (UIPlayDirector.OnStartStrongholdMode != null)
		{
			UIPlayDirector.OnStartStrongholdMode();
		}
	}

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x060013AB RID: 5035 RVA: 0x000AFEBC File Offset: 0x000AE2BC
	// (remove) Token: 0x060013AC RID: 5036 RVA: 0x000AFEF0 File Offset: 0x000AE2F0
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.JumpEventHandler OnStartHuntingMode;

	// Token: 0x060013AD RID: 5037 RVA: 0x000AFF24 File Offset: 0x000AE324
	public void GenStartHuntingModeEvent()
	{
		if (UIPlayDirector.OnStartHuntingMode != null)
		{
			UIPlayDirector.OnStartHuntingMode();
		}
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x000AFF3C File Offset: 0x000AE33C
	public void ModeStartBtnPressed()
	{
		if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Mutation)
		{
			this.GenStartMutationModeEvent();
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.StrongHold)
		{
			this.GenStartStrongholdModeEvent();
		}
		else if (GGNetworkKit.mInstance.GetGameMode() == GGModeType.Hunting)
		{
			this.GenStartHuntingModeEvent();
		}
		UIModeDirector.mInstance.StartBtnPressed();
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x000AFFA0 File Offset: 0x000AE3A0
	private void EnchantmentLogoRefreshTimer()
	{
		this.enchantmentLogoRefreshTime += Time.deltaTime;
		if (this.enchantmentLogoRefreshTime > this.enchantmentLogoRefreshTimerCycle)
		{
			this.enchantmentLogoRefreshTime = 0f;
			int num = 0;
			List<EnchantmentDetails> list = GrowthManagerKit.EnabledTickedEPropertyList();
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].originType == EnchantmentOriginType.ScenePropsAddition)
				{
					if (num >= 6)
					{
						break;
					}
					this.enchantmentLogo[num].gameObject.SetActive(true);
					this.enchantmentLogo[num].mainTexture = (Resources.Load("UI/Images/EnchantmentLogo/" + list[i].logoSpriteName) as Texture);
					num++;
				}
			}
			for (int j = num; j < 6; j++)
			{
				if (this.enchantmentLogo[j].gameObject.activeSelf)
				{
					this.enchantmentLogo[j].gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x060013B0 RID: 5040 RVA: 0x000B00A4 File Offset: 0x000AE4A4
	// (remove) Token: 0x060013B1 RID: 5041 RVA: 0x000B00D8 File Offset: 0x000AE4D8
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.FireworkEventHandler OnFirework;

	// Token: 0x060013B2 RID: 5042 RVA: 0x000B010C File Offset: 0x000AE50C
	public void GenFireworkEvent(int index)
	{
		if (UIPlayDirector.OnFirework != null)
		{
			UIPlayDirector.OnFirework(index);
		}
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x000B0123 File Offset: 0x000AE523
	private void InitFireworkNode()
	{
	}

	// Token: 0x060013B4 RID: 5044 RVA: 0x000B0125 File Offset: 0x000AE525
	public void FireworkBarMoveFinished()
	{
		this.fireworkBarNode.SetActive(false);
	}

	// Token: 0x060013B5 RID: 5045 RVA: 0x000B0133 File Offset: 0x000AE533
	public void FireworkToggleValueChanged()
	{
		if (this.fireworkToggle.value)
		{
			this.OpenFireworkBarBtnPressed();
		}
		else
		{
			this.CloseFireworkBarBtnPressed();
		}
	}

	// Token: 0x060013B6 RID: 5046 RVA: 0x000B0156 File Offset: 0x000AE556
	public void OpenFireworkBarBtnPressed()
	{
		this.fireworkBarNode.SetActive(true);
	}

	// Token: 0x060013B7 RID: 5047 RVA: 0x000B0164 File Offset: 0x000AE564
	public void CloseFireworkBarBtnPressed()
	{
		this.fireworkBarNode.SetActive(false);
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x000B0174 File Offset: 0x000AE574
	public void FireworkBtnPressed(int index)
	{
		if (GrowthManagerKit.GetCoins() >= this.fireworkPrice[index - 1])
		{
			GrowthManagerKit.SubCoins(this.fireworkPrice[index - 1]);
			this.coinTipLabel.text = "- " + this.fireworkPrice[index - 1].ToString();
			this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
			this.GenFireworkEvent(index);
			this.cding = true;
			for (int i = 0; i < this.fireworkBtn.Length; i++)
			{
				this.fireworkBtn[i].isEnabled = false;
			}
			for (int j = 0; j < this.fireworkLogoFg.Length; j++)
			{
				this.fireworkLogoFg[j].gameObject.SetActive(true);
			}
		}
		else
		{
			this.coinTipLabel.text = "- Lack";
			this.ShowSubCoinLabel(this.coinTipLabel.gameObject, null, 1f);
		}
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x000B0278 File Offset: 0x000AE678
	private void FireworkCDTimer()
	{
		if (this.cding)
		{
			this.cdTime += Time.deltaTime;
			if (this.cdTime > this.cdCycle)
			{
				this.cdTime = 0f;
				for (int i = 0; i < this.fireworkBtn.Length; i++)
				{
					this.fireworkBtn[i].isEnabled = true;
				}
				for (int j = 0; j < this.fireworkLogoFg.Length; j++)
				{
					this.fireworkLogoFg[j].gameObject.SetActive(false);
				}
				this.cding = false;
			}
			else
			{
				float fillAmount = 1f - this.cdTime / this.cdCycle;
				for (int k = 0; k < this.fireworkLogoFg.Length; k++)
				{
					this.fireworkLogoFg[k].fillAmount = fillAmount;
				}
			}
		}
	}

	// Token: 0x14000018 RID: 24
	// (add) Token: 0x060013BA RID: 5050 RVA: 0x000B0358 File Offset: 0x000AE758
	// (remove) Token: 0x060013BB RID: 5051 RVA: 0x000B038C File Offset: 0x000AE78C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.InstallBtnPressedEventHandler OnInstallBtnPressed;

	// Token: 0x060013BC RID: 5052 RVA: 0x000B03C0 File Offset: 0x000AE7C0
	public void GenInstallBtnPressedEvent()
	{
		if (UIPlayDirector.OnInstallBtnPressed != null)
		{
			UIPlayDirector.OnInstallBtnPressed();
		}
	}

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x060013BD RID: 5053 RVA: 0x000B03D8 File Offset: 0x000AE7D8
	// (remove) Token: 0x060013BE RID: 5054 RVA: 0x000B040C File Offset: 0x000AE80C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.InstallBtnReleasedEventHandler OnInstallBtnReleased;

	// Token: 0x060013BF RID: 5055 RVA: 0x000B0440 File Offset: 0x000AE840
	public void GenInstallBtnReleasedEvent()
	{
		if (UIPlayDirector.OnInstallBtnReleased != null)
		{
			UIPlayDirector.OnInstallBtnReleased();
		}
	}

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x060013C0 RID: 5056 RVA: 0x000B0458 File Offset: 0x000AE858
	// (remove) Token: 0x060013C1 RID: 5057 RVA: 0x000B048C File Offset: 0x000AE88C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.UninstallBtnPressedEventHandler OnUninstallBtnPressed;

	// Token: 0x060013C2 RID: 5058 RVA: 0x000B04C0 File Offset: 0x000AE8C0
	public void GenUninstallBtnPressedEvent()
	{
		if (UIPlayDirector.OnUninstallBtnPressed != null)
		{
			UIPlayDirector.OnUninstallBtnPressed();
		}
	}

	// Token: 0x1400001B RID: 27
	// (add) Token: 0x060013C3 RID: 5059 RVA: 0x000B04D8 File Offset: 0x000AE8D8
	// (remove) Token: 0x060013C4 RID: 5060 RVA: 0x000B050C File Offset: 0x000AE90C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.UninstallBtnReleasedEventHandler OnUninstallBtnReleased;

	// Token: 0x060013C5 RID: 5061 RVA: 0x000B0540 File Offset: 0x000AE940
	public void GenUninstallBtnReleasedEvent()
	{
		if (UIPlayDirector.OnUninstallBtnReleased != null)
		{
			UIPlayDirector.OnUninstallBtnReleased();
		}
	}

	// Token: 0x1400001C RID: 28
	// (add) Token: 0x060013C6 RID: 5062 RVA: 0x000B0558 File Offset: 0x000AE958
	// (remove) Token: 0x060013C7 RID: 5063 RVA: 0x000B058C File Offset: 0x000AE98C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.ObserverLeftEventHandler OnObserverLeft;

	// Token: 0x060013C8 RID: 5064 RVA: 0x000B05C0 File Offset: 0x000AE9C0
	public void GenObserverLeftEvent()
	{
		if (UIPlayDirector.OnObserverLeft != null)
		{
			UIPlayDirector.OnObserverLeft();
		}
	}

	// Token: 0x1400001D RID: 29
	// (add) Token: 0x060013C9 RID: 5065 RVA: 0x000B05D8 File Offset: 0x000AE9D8
	// (remove) Token: 0x060013CA RID: 5066 RVA: 0x000B060C File Offset: 0x000AEA0C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event UIPlayDirector.ObserverRightEventHandler OnObserverRight;

	// Token: 0x060013CB RID: 5067 RVA: 0x000B0640 File Offset: 0x000AEA40
	public void GenObserverRightEvent()
	{
		if (UIPlayDirector.OnObserverRight != null)
		{
			UIPlayDirector.OnObserverRight();
		}
	}

	// Token: 0x060013CC RID: 5068 RVA: 0x000B0656 File Offset: 0x000AEA56
	public void InstallBtnPressed()
	{
		this.GenInstallBtnPressedEvent();
	}

	// Token: 0x060013CD RID: 5069 RVA: 0x000B065E File Offset: 0x000AEA5E
	public void InstallBtnReleased()
	{
		this.GenInstallBtnReleasedEvent();
	}

	// Token: 0x060013CE RID: 5070 RVA: 0x000B0666 File Offset: 0x000AEA66
	public void UninstallBtnPressed()
	{
		this.GenUninstallBtnPressedEvent();
	}

	// Token: 0x060013CF RID: 5071 RVA: 0x000B066E File Offset: 0x000AEA6E
	public void UninstallBtnReleased()
	{
		this.GenUninstallBtnReleasedEvent();
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x000B0676 File Offset: 0x000AEA76
	public void ObserverLeftBtnPressed()
	{
		this.GenObserverLeftEvent();
	}

	// Token: 0x060013D1 RID: 5073 RVA: 0x000B067E File Offset: 0x000AEA7E
	public void ObserverRightBtnPressed()
	{
		this.GenObserverRightEvent();
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x000B0686 File Offset: 0x000AEA86
	public void VideoRecordToggleValueChanged()
	{
		if (this.videoRecordToggle.value)
		{
			VideoRecordDirector.StartRecording();
			this.videoRecordHighlight.SetActive(true);
		}
		else
		{
			VideoRecordDirector.StopRecording();
			this.videoRecordHighlight.SetActive(false);
		}
	}

	// Token: 0x0400169D RID: 5789
	public static UIPlayDirector mInstance;

	// Token: 0x0400169E RID: 5790
	public AudioClip[] clips;

	// Token: 0x040016A3 RID: 5795
	private bool cancelFire;

	// Token: 0x040016AD RID: 5805
	public GGNetworkCharacter mNetworkCharacter;

	// Token: 0x040016AE RID: 5806
	private GGWeaponManager mWeaponManager;

	// Token: 0x040016AF RID: 5807
	private int bloodNum;

	// Token: 0x040016B0 RID: 5808
	private int armorNum;

	// Token: 0x040016B1 RID: 5809
	public GameObject controlMiddleNode;

	// Token: 0x040016B2 RID: 5810
	public GameObject explosionProgressBarObj;

	// Token: 0x040016B3 RID: 5811
	public GameObject strongholdProgressBarObj;

	// Token: 0x040016B4 RID: 5812
	public GameObject flashBombEffectObj;

	// Token: 0x040016B5 RID: 5813
	public GameObject blindEffectObj;

	// Token: 0x040016B6 RID: 5814
	public GameObject NightmareEffectObj;

	// Token: 0x040016B7 RID: 5815
	public GameObject bloodEffectObj;

	// Token: 0x040016B8 RID: 5816
	public GameObject sniperEffectObj;

	// Token: 0x040016B9 RID: 5817
	public GameObject crosshairObj;

	// Token: 0x040016BA RID: 5818
	public GameObject sniperBtnObj;

	// Token: 0x040016BB RID: 5819
	public GameObject sniperCancelBtnObj;

	// Token: 0x040016BC RID: 5820
	public GameObject startCountDownMutationObj;

	// Token: 0x040016BD RID: 5821
	public GameObject installBombBtn;

	// Token: 0x040016BE RID: 5822
	public GameObject unInstallBombBtn;

	// Token: 0x040016BF RID: 5823
	public UIButton fireBtn;

	// Token: 0x040016C0 RID: 5824
	public GameObject energyBarNode;

	// Token: 0x040016C1 RID: 5825
	public UISprite energyBarSprite;

	// Token: 0x040016C2 RID: 5826
	public UILabel bloodLabel;

	// Token: 0x040016C3 RID: 5827
	public UILabel armorLabel;

	// Token: 0x040016C4 RID: 5828
	public UILabel bulletLabel;

	// Token: 0x040016C5 RID: 5829
	public UISprite weaponSprite;

	// Token: 0x040016C6 RID: 5830
	public UILabel coinTipLabel;

	// Token: 0x040016C7 RID: 5831
	public UILabel expTipLabel;

	// Token: 0x040016C8 RID: 5832
	public UILabel socksTipLabel;

	// Token: 0x040016C9 RID: 5833
	public UISprite bloodBg;

	// Token: 0x040016CA RID: 5834
	public UISprite armorBg;

	// Token: 0x040016CB RID: 5835
	public GameObject killPopPrefab;

	// Token: 0x040016CC RID: 5836
	private GameObject newKillPopObject;

	// Token: 0x040016CD RID: 5837
	public GameObject exampleKillPopPrefab;

	// Token: 0x040016CE RID: 5838
	private List<string> mPopMessageList = new List<string>();

	// Token: 0x040016CF RID: 5839
	private List<GrowthPrometType> mPopMessageTypeList = new List<GrowthPrometType>();

	// Token: 0x040016D0 RID: 5840
	private float fTime;

	// Token: 0x040016D1 RID: 5841
	private const int mediKitPrice = 30;

	// Token: 0x040016D2 RID: 5842
	private const float mediKitUseCycleTime = 180f;

	// Token: 0x040016D3 RID: 5843
	private bool canUseMediKit = true;

	// Token: 0x040016D4 RID: 5844
	private float fMediTime;

	// Token: 0x040016D5 RID: 5845
	public UISprite mediKitBackgroudF;

	// Token: 0x040016D6 RID: 5846
	public UIButton addBloodBtn;

	// Token: 0x040016D7 RID: 5847
	public UISprite armorLogoSprite;

	// Token: 0x040016D8 RID: 5848
	public UIButton addArmorBtn;

	// Token: 0x040016D9 RID: 5849
	private GGPlayModeType ggPlayModeType = GGPlayModeType.Other;

	// Token: 0x040016DA RID: 5850
	public GameObject pauseNode;

	// Token: 0x040016DB RID: 5851
	public GameObject storeNode;

	// Token: 0x040016DC RID: 5852
	public GameObject chatNode;

	// Token: 0x040016DD RID: 5853
	private Color tempColor = new Color(255f, 206f, 0f);

	// Token: 0x040016DE RID: 5854
	public UIButton throwWeaponBtn;

	// Token: 0x040016DF RID: 5855
	public UISprite throwWeaponFgSprite;

	// Token: 0x040016E0 RID: 5856
	public UISprite throwWeaponFgSprite2;

	// Token: 0x040016E1 RID: 5857
	private int throwWeaponIndex = 6;

	// Token: 0x040016E2 RID: 5858
	private float throwWeaponUseCycleTime = 8f;

	// Token: 0x040016E3 RID: 5859
	private bool canUseThrowWeapon = true;

	// Token: 0x040016E4 RID: 5860
	private float fThrowWeaponTime;

	// Token: 0x040016E6 RID: 5862
	public GameObject buffBarNode;

	// Token: 0x040016E7 RID: 5863
	public UIButton[] buffItemBtn = new UIButton[5];

	// Token: 0x040016E8 RID: 5864
	public UILabel[] buffItemNumLabel = new UILabel[5];

	// Token: 0x040016E9 RID: 5865
	public UISprite[] buffItemFgSprite = new UISprite[5];

	// Token: 0x040016EA RID: 5866
	public UISprite[] buffItemBgSprite = new UISprite[5];

	// Token: 0x040016EB RID: 5867
	public UIToggle buffToggle;

	// Token: 0x040016EC RID: 5868
	public UISprite buffNoselectBg;

	// Token: 0x040016ED RID: 5869
	private const float buffPanlTweenCycle = 0.15f;

	// Token: 0x040016EE RID: 5870
	private bool isOpenBuffBar;

	// Token: 0x040016EF RID: 5871
	private bool buffOpen;

	// Token: 0x040016F0 RID: 5872
	private string[] buffNameList;

	// Token: 0x040016F1 RID: 5873
	private GMultiplayerBuffItemInfo[] buffItemInfo;

	// Token: 0x040016F2 RID: 5874
	private bool[] isUsingBuff = new bool[5];

	// Token: 0x040016F3 RID: 5875
	private float[] buffTime = new float[5];

	// Token: 0x040016F7 RID: 5879
	private const int enchantmentListCount = 6;

	// Token: 0x040016F8 RID: 5880
	public UITexture[] enchantmentLogo = new UITexture[6];

	// Token: 0x040016F9 RID: 5881
	private float enchantmentLogoRefreshTimerCycle = 1f;

	// Token: 0x040016FA RID: 5882
	private float enchantmentLogoRefreshTime = 1f;

	// Token: 0x040016FB RID: 5883
	public UIToggle fireworkToggle;

	// Token: 0x040016FC RID: 5884
	public GameObject fireworkBarNode;

	// Token: 0x040016FD RID: 5885
	public UIButton[] fireworkBtn;

	// Token: 0x040016FE RID: 5886
	public UISprite[] fireworkLogoFg;

	// Token: 0x040016FF RID: 5887
	private int[] fireworkPrice = new int[]
	{
		5,
		5,
		5,
		5
	};

	// Token: 0x04001700 RID: 5888
	private float cdCycle = 5f;

	// Token: 0x04001701 RID: 5889
	private float cdTime;

	// Token: 0x04001702 RID: 5890
	private bool cding;

	// Token: 0x0400170A RID: 5898
	public UIToggle videoRecordToggle;

	// Token: 0x0400170B RID: 5899
	public GameObject videoRecordHighlight;

	// Token: 0x0200029A RID: 666
	// (Invoke) Token: 0x060013D4 RID: 5076
	public delegate void JumpEventHandler();

	// Token: 0x0200029B RID: 667
	// (Invoke) Token: 0x060013D8 RID: 5080
	public delegate void AimEventHandler();

	// Token: 0x0200029C RID: 668
	// (Invoke) Token: 0x060013DC RID: 5084
	public delegate void SniperCancelEventHandler();

	// Token: 0x0200029D RID: 669
	// (Invoke) Token: 0x060013E0 RID: 5088
	public delegate void ReloadEventHandler();

	// Token: 0x0200029E RID: 670
	// (Invoke) Token: 0x060013E4 RID: 5092
	public delegate void FireStartEventHandler();

	// Token: 0x0200029F RID: 671
	// (Invoke) Token: 0x060013E8 RID: 5096
	public delegate void FireEndEventHandler();

	// Token: 0x020002A0 RID: 672
	// (Invoke) Token: 0x060013EC RID: 5100
	public delegate void JumpStartEventHandler();

	// Token: 0x020002A1 RID: 673
	// (Invoke) Token: 0x060013F0 RID: 5104
	public delegate void JumpEndEventHandler();

	// Token: 0x020002A2 RID: 674
	// (Invoke) Token: 0x060013F4 RID: 5108
	public delegate void SwitchLeftEventHandler();

	// Token: 0x020002A3 RID: 675
	// (Invoke) Token: 0x060013F8 RID: 5112
	public delegate void SwitchRightEventHandler();

	// Token: 0x020002A4 RID: 676
	// (Invoke) Token: 0x060013FC RID: 5116
	public delegate void AddBulletEventHandler();

	// Token: 0x020002A5 RID: 677
	// (Invoke) Token: 0x06001400 RID: 5120
	public delegate void ArmorEventHandler();

	// Token: 0x020002A6 RID: 678
	// (Invoke) Token: 0x06001404 RID: 5124
	public delegate void MediKitEventHandler();

	// Token: 0x020002A7 RID: 679
	// (Invoke) Token: 0x06001408 RID: 5128
	public delegate void ThrowWeaponFireEventHandler(int index);

	// Token: 0x020002A8 RID: 680
	// (Invoke) Token: 0x0600140C RID: 5132
	public delegate void StartMutationModeEventHandler();

	// Token: 0x020002A9 RID: 681
	// (Invoke) Token: 0x06001410 RID: 5136
	public delegate void StartStrongholdModeEventHandler();

	// Token: 0x020002AA RID: 682
	// (Invoke) Token: 0x06001414 RID: 5140
	public delegate void StartHuntingModeEventHandler();

	// Token: 0x020002AB RID: 683
	// (Invoke) Token: 0x06001418 RID: 5144
	public delegate void FireworkEventHandler(int index);

	// Token: 0x020002AC RID: 684
	// (Invoke) Token: 0x0600141C RID: 5148
	public delegate void InstallBtnPressedEventHandler();

	// Token: 0x020002AD RID: 685
	// (Invoke) Token: 0x06001420 RID: 5152
	public delegate void InstallBtnReleasedEventHandler();

	// Token: 0x020002AE RID: 686
	// (Invoke) Token: 0x06001424 RID: 5156
	public delegate void UninstallBtnPressedEventHandler();

	// Token: 0x020002AF RID: 687
	// (Invoke) Token: 0x06001428 RID: 5160
	public delegate void UninstallBtnReleasedEventHandler();

	// Token: 0x020002B0 RID: 688
	// (Invoke) Token: 0x0600142C RID: 5164
	public delegate void ObserverLeftEventHandler();

	// Token: 0x020002B1 RID: 689
	// (Invoke) Token: 0x06001430 RID: 5168
	public delegate void ObserverRightEventHandler();
}
