using System;
using UnityEngine;

// Token: 0x020002F4 RID: 756
public class UINewStoreDecoWindowDirector : MonoBehaviour
{
	// Token: 0x06001756 RID: 5974 RVA: 0x000C5BAC File Offset: 0x000C3FAC
	private void Awake()
	{
		if (UINewStoreDecoWindowDirector.mInstance == null)
		{
			UINewStoreDecoWindowDirector.mInstance = this;
		}
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x000C5BC4 File Offset: 0x000C3FC4
	private void OnDestroy()
	{
		if (UINewStoreDecoWindowDirector.mInstance != null)
		{
			UINewStoreDecoWindowDirector.mInstance = null;
		}
	}

	// Token: 0x06001758 RID: 5976 RVA: 0x000C5BDC File Offset: 0x000C3FDC
	private void Start()
	{
	}

	// Token: 0x06001759 RID: 5977 RVA: 0x000C5BDE File Offset: 0x000C3FDE
	private void Update()
	{
	}

	// Token: 0x0600175A RID: 5978 RVA: 0x000C5BE0 File Offset: 0x000C3FE0
	public void SkinBtnPressed()
	{
		this.skinScrollView.SetActive(true);
		this.hatScrollView.SetActive(false);
		this.capeScrollView.SetActive(false);
		this.shoeScrollView.SetActive(false);
		this.skinShareScrollView.SetActive(false);
		this.skinBuy.SetActive(true);
		this.hatBuy.SetActive(false);
		this.capeBuy.SetActive(false);
		this.shoeBuy.SetActive(false);
		this.skinShareDownload.SetActive(false);
		this.skinScrollViewInstiate.InstantiateSkin();
		this.skinScrollViewInstiate.RefreshSkinProperty();
		this.skinScrollViewInstiate.BackToSkinUIToggle();
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x000C5C88 File Offset: 0x000C4088
	public void HatBtnPressed()
	{
		this.skinScrollView.SetActive(false);
		this.hatScrollView.SetActive(true);
		this.capeScrollView.SetActive(false);
		this.shoeScrollView.SetActive(false);
		this.skinShareScrollView.SetActive(false);
		this.skinBuy.SetActive(false);
		this.hatBuy.SetActive(true);
		this.capeBuy.SetActive(false);
		this.shoeBuy.SetActive(false);
		this.skinShareDownload.SetActive(false);
		this.hatScrollViewInstiate.InstantiateHat();
		this.hatScrollViewInstiate.RefreshHatProperty();
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x000C5D24 File Offset: 0x000C4124
	public void CapeBtnPressed()
	{
		this.skinScrollView.SetActive(false);
		this.hatScrollView.SetActive(false);
		this.capeScrollView.SetActive(true);
		this.shoeScrollView.SetActive(false);
		this.skinShareScrollView.SetActive(false);
		this.skinBuy.SetActive(false);
		this.hatBuy.SetActive(false);
		this.capeBuy.SetActive(true);
		this.shoeBuy.SetActive(false);
		this.skinShareDownload.SetActive(false);
		this.capeScrollViewInstiate.InstantiateCape();
		this.capeScrollViewInstiate.RefreshCapeProperty();
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x000C5DC0 File Offset: 0x000C41C0
	public void ShoeBtnPressed()
	{
		this.skinScrollView.SetActive(false);
		this.hatScrollView.SetActive(false);
		this.capeScrollView.SetActive(false);
		this.shoeScrollView.SetActive(true);
		this.skinShareScrollView.SetActive(false);
		this.skinBuy.SetActive(false);
		this.hatBuy.SetActive(false);
		this.capeBuy.SetActive(false);
		this.shoeBuy.SetActive(true);
		this.skinShareDownload.SetActive(false);
		this.shoeScrollViewInstiate.InstantiateBoot();
		this.shoeScrollViewInstiate.RefreshBootProperty();
	}

	// Token: 0x0600175E RID: 5982 RVA: 0x000C5E5C File Offset: 0x000C425C
	public void SkinShareBtnPressed()
	{
		this.skinScrollView.SetActive(false);
		this.hatScrollView.SetActive(false);
		this.capeScrollView.SetActive(false);
		this.shoeScrollView.SetActive(false);
		this.skinShareScrollView.SetActive(true);
		this.skinBuy.SetActive(false);
		this.hatBuy.SetActive(false);
		this.capeBuy.SetActive(false);
		this.shoeBuy.SetActive(false);
		this.skinShareDownload.SetActive(true);
		if (SkinManager.mInstance.sharedSkinEntityList.Count == 0)
		{
			UINewStoreBasicWindowDirector.mInstance.TipDownloadShareSkin();
			SkinManager.mInstance.RequestSharedSkins();
			this.skinShareScrollViewInstiate.HideShareSkinButton();
		}
		else
		{
			this.skinShareScrollViewInstiate.RefreshShareSkinProperty();
			this.skinShareScrollViewInstiate.RefreshShareSkinDownload();
			this.skinShareScrollViewInstiate.DisplayShareSkinButton();
		}
	}

	// Token: 0x04001A52 RID: 6738
	public static UINewStoreDecoWindowDirector mInstance;

	// Token: 0x04001A53 RID: 6739
	public GameObject skinScrollView;

	// Token: 0x04001A54 RID: 6740
	public GameObject hatScrollView;

	// Token: 0x04001A55 RID: 6741
	public GameObject capeScrollView;

	// Token: 0x04001A56 RID: 6742
	public GameObject shoeScrollView;

	// Token: 0x04001A57 RID: 6743
	public GameObject skinShareScrollView;

	// Token: 0x04001A58 RID: 6744
	public UINewStoreDecoSkinPrefabInstiate skinScrollViewInstiate;

	// Token: 0x04001A59 RID: 6745
	public UINewStoreDecoHatPrefabInstiate hatScrollViewInstiate;

	// Token: 0x04001A5A RID: 6746
	public UINewStoreDecoCapePrefabInstiate capeScrollViewInstiate;

	// Token: 0x04001A5B RID: 6747
	public UINewStoreDecoShoePrefabInstiate shoeScrollViewInstiate;

	// Token: 0x04001A5C RID: 6748
	public UINewStoreDecoSkinSharePrefabInstiate skinShareScrollViewInstiate;

	// Token: 0x04001A5D RID: 6749
	public GameObject skinBuy;

	// Token: 0x04001A5E RID: 6750
	public GameObject hatBuy;

	// Token: 0x04001A5F RID: 6751
	public GameObject capeBuy;

	// Token: 0x04001A60 RID: 6752
	public GameObject shoeBuy;

	// Token: 0x04001A61 RID: 6753
	public GameObject skinShareDownload;
}
