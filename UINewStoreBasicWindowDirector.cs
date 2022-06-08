using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E6 RID: 742
public class UINewStoreBasicWindowDirector : MonoBehaviour
{
	// Token: 0x060016E4 RID: 5860 RVA: 0x000C2606 File Offset: 0x000C0A06
	private void Awake()
	{
		if (UINewStoreBasicWindowDirector.mInstance == null)
		{
			UINewStoreBasicWindowDirector.mInstance = this;
		}
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x000C261E File Offset: 0x000C0A1E
	private void OnDestroy()
	{
		if (UINewStoreBasicWindowDirector.mInstance != null)
		{
			UINewStoreBasicWindowDirector.mInstance = null;
		}
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x000C2638 File Offset: 0x000C0A38
	private void Start()
	{
		UINewStoreWeaponWindowDirector.mInstance.MeeleBtnPressed();
		this.mUINewStoreArmorWindowDirector.ArmorInit();
		this.mUINewStoreDecoSkinPrefabInstiate.SkinInit();
		this.mUINewStoreDecoHatPrefabInstiate.HatInit();
		this.mUINewStoreDecoCapePrefabInstiate.CapeInit();
		this.mUINewStoreDecoShoePrefabInstiate.BootInit();
		this.mUINewStoreToolKeyPrefabInstiate.keyInit();
		if (UIUserDataController.GetOffsaleTimeSpan().TotalSeconds > 0.0 && UIUserDataController.GetOffsaleFactor() > 1f)
		{
			this.offsaleTip.gameObject.SetActive(true);
		}
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x000C26CC File Offset: 0x000C0ACC
	private void Update()
	{
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x000C26D0 File Offset: 0x000C0AD0
	public void WeaponBtnPressed()
	{
		for (int i = 0; i < this.Windows.Count - 1; i++)
		{
			if (i == 0)
			{
				this.Windows[i].SetActive(true);
			}
			else
			{
				this.Windows[i].SetActive(false);
			}
		}
		if (!this.Windows[5].activeSelf)
		{
			this.Windows[5].SetActive(true);
		}
		UINewStoreWeaponWindowDirector.mInstance.MeeleBtnPressed();
		this.IsInShopWindow = false;
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x000C2764 File Offset: 0x000C0B64
	public void ArmorBtnPressed()
	{
		for (int i = 0; i < this.Windows.Count - 1; i++)
		{
			if (i == 1)
			{
				this.Windows[i].SetActive(true);
			}
			else
			{
				this.Windows[i].SetActive(false);
			}
		}
		if (!this.Windows[5].activeSelf)
		{
			this.Windows[5].SetActive(true);
		}
		this.IsInShopWindow = false;
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x000C27F0 File Offset: 0x000C0BF0
	public void DecoBtnPressed()
	{
		for (int i = 0; i < this.Windows.Count - 1; i++)
		{
			if (i == 2)
			{
				this.Windows[i].SetActive(true);
			}
			else
			{
				this.Windows[i].SetActive(false);
			}
		}
		if (!this.Windows[5].activeSelf)
		{
			this.Windows[5].SetActive(true);
		}
		UINewStoreDecoWindowDirector.mInstance.SkinBtnPressed();
		this.IsInShopWindow = false;
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x000C2884 File Offset: 0x000C0C84
	public void ToolBtnPressed()
	{
		for (int i = 0; i < this.Windows.Count - 1; i++)
		{
			if (i == 3)
			{
				this.Windows[i].SetActive(true);
			}
			else
			{
				this.Windows[i].SetActive(false);
			}
		}
		if (!this.Windows[5].activeSelf)
		{
			this.Windows[5].SetActive(true);
		}
		UINewStoreToolWindowDirector.mInstance.TicketBtnPressed();
		this.IsInShopWindow = false;
	}

	// Token: 0x060016EC RID: 5868 RVA: 0x000C2918 File Offset: 0x000C0D18
	public void CoinBtnPressed()
	{
		for (int i = 0; i < this.Windows.Count; i++)
		{
			if (i == 4)
			{
				this.Windows[i].SetActive(true);
			}
			else
			{
				this.Windows[i].SetActive(false);
			}
		}
		UINewStoreGemAndCoinPurchaseWindowDirector.mInstance.PurchaseBtnPressed();
		this.IsInShopWindow = true;
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x000C2982 File Offset: 0x000C0D82
	public void CharacterDefalutEquip()
	{
		this.mUINewStoreDecoSkinPrefabInstiate.SkinDefaultEquipSet();
		this.mUINewStoreDecoHatPrefabInstiate.HatDefaultEquipSet();
		this.mUINewStoreDecoCapePrefabInstiate.CapeDefaultEquipSet();
		this.mUINewStoreDecoShoePrefabInstiate.BootDefaultEquipSet();
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x000C29B0 File Offset: 0x000C0DB0
	public void BackToMainMenu()
	{
		if (!this.IsInShopWindow)
		{
			Application.LoadLevel("MainMenu");
		}
		else
		{
			this.WeaponBtnPressed();
			this.WeaponUIToggle.value = true;
		}
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x000C29DE File Offset: 0x000C0DDE
	public void GoToShop()
	{
		this.CoinBtnPressed();
		this.CoinAndGemUIToggle.value = true;
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x000C29FC File Offset: 0x000C0DFC
	public void ReturnToCurPanel()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x000C2A08 File Offset: 0x000C0E08
	public void TipGoToShop()
	{
		EventDelegate btnEventName = new EventDelegate(this, "GoToShop");
		EventDelegate btnEventName2 = new EventDelegate(this, "ReturnToCurPanel");
		UITipController.mInstance.SetTipData(UITipController.TipType.TwoButtonTip, "No Enough Coin Or Gem!\nGo to Shop Now?", Color.green, "OK", "CANCEL", btnEventName, btnEventName2, null);
	}

	// Token: 0x060016F2 RID: 5874 RVA: 0x000C2A50 File Offset: 0x000C0E50
	public void TipHonorPointLack()
	{
		EventDelegate btnEventName = new EventDelegate(this, "ReturnToCurPanel");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "Honor Point Lack!", Color.red, "CANCEL", string.Empty, btnEventName, null, null);
	}

	// Token: 0x060016F3 RID: 5875 RVA: 0x000C2A8C File Offset: 0x000C0E8C
	public void TipReachCustomSkinLimit()
	{
		EventDelegate btnEventName = new EventDelegate(this, "ReturnToCurPanel");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "Reach Custom Limit!", Color.red, "CANCEL", string.Empty, btnEventName, null, null);
	}

	// Token: 0x060016F4 RID: 5876 RVA: 0x000C2AC7 File Offset: 0x000C0EC7
	public void DeleteCustomSkin()
	{
		this.mUINewStoreDecoSkinPrefabInstiate.DeleteSkin();
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060016F5 RID: 5877 RVA: 0x000C2AE0 File Offset: 0x000C0EE0
	public void TipDeleteCustomSkin()
	{
		EventDelegate btnEventName = new EventDelegate(this, "DeleteCustomSkin");
		EventDelegate btnEventName2 = new EventDelegate(this, "ReturnToCurPanel");
		UITipController.mInstance.SetTipData(UITipController.TipType.TwoButtonTip, "Delete Your Custom Skin?", Color.red, "OK", "CANCEL", btnEventName, btnEventName2, null);
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x000C2B28 File Offset: 0x000C0F28
	public void TipDownloadShareSkin()
	{
		UITipController.mInstance.SetTipData(UITipController.TipType.LoadingTip, "Downloading...", Color.white, null, null, null, null, null);
	}

	// Token: 0x060016F7 RID: 5879 RVA: 0x000C2B50 File Offset: 0x000C0F50
	public void TipDownloadShareSkinFail()
	{
		EventDelegate btnEventName = new EventDelegate(this, "ReturnToCurPanel");
		UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "Download  Failed!", Color.red, "OK", string.Empty, btnEventName, null, null);
	}

	// Token: 0x040019B2 RID: 6578
	public static UINewStoreBasicWindowDirector mInstance;

	// Token: 0x040019B3 RID: 6579
	public List<GameObject> Windows;

	// Token: 0x040019B4 RID: 6580
	public UINewStoreArmorWindowDirector mUINewStoreArmorWindowDirector;

	// Token: 0x040019B5 RID: 6581
	public UINewStoreDecoSkinPrefabInstiate mUINewStoreDecoSkinPrefabInstiate;

	// Token: 0x040019B6 RID: 6582
	public UINewStoreDecoHatPrefabInstiate mUINewStoreDecoHatPrefabInstiate;

	// Token: 0x040019B7 RID: 6583
	public UINewStoreDecoCapePrefabInstiate mUINewStoreDecoCapePrefabInstiate;

	// Token: 0x040019B8 RID: 6584
	public UINewStoreDecoShoePrefabInstiate mUINewStoreDecoShoePrefabInstiate;

	// Token: 0x040019B9 RID: 6585
	public UINewStoreToolKeyPrefabInstiate mUINewStoreToolKeyPrefabInstiate;

	// Token: 0x040019BA RID: 6586
	public UINewStoreGemAndCoinPurchaseWindowDirector mUINewStoreGemAndCoinPurchaseWindowDirector;

	// Token: 0x040019BB RID: 6587
	public UIToggle CoinAndGemUIToggle;

	// Token: 0x040019BC RID: 6588
	public UIToggle WeaponUIToggle;

	// Token: 0x040019BD RID: 6589
	private bool IsInShopWindow;

	// Token: 0x040019BE RID: 6590
	public UISprite offsaleTip;
}
