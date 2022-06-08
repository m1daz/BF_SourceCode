using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000305 RID: 773
public class UINewStoreToolStaffPrefabInstiate : MonoBehaviour
{
	// Token: 0x060017B9 RID: 6073 RVA: 0x000C82F8 File Offset: 0x000C66F8
	private void Start()
	{
	}

	// Token: 0x060017BA RID: 6074 RVA: 0x000C82FA File Offset: 0x000C66FA
	private void Update()
	{
		if (!this.isStaffInstantiated)
		{
			return;
		}
		if (this.curSelectedStaffIndex != this.preSelectedStaffIndex)
		{
			this.preSelectedStaffIndex = this.curSelectedStaffIndex;
			this.RefreshStaffProperty();
			this.RefreshStaffPurchase();
		}
	}

	// Token: 0x060017BB RID: 6075 RVA: 0x000C8334 File Offset: 0x000C6734
	public void InstantiateStaff()
	{
		if (!this.isStaffInstantiated)
		{
			this.StaffNameList = GrowthManagerKit.GetAllUpEnabledWeaponPropertyNameList();
			this.StaffItemInfo = GrowthManagerKit.GetAllWeaponPropertyCardItemInfo();
			int num = this.StaffItemInfo.Length;
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.StaffItemPrefab, this.StaffItemPrefab.transform.position, this.StaffItemPrefab.transform.rotation);
				gameObject.transform.parent = this.StaffObjParent.transform;
				gameObject.transform.localScale = this.StaffItemPrefab.transform.localScale;
				gameObject.transform.localPosition = new Vector3((float)(i - 1) * 200f, 0f, 0f);
				gameObject.transform.rotation = this.StaffItemPrefab.transform.rotation;
				gameObject.transform.Find("StaffModel/StaffTexture").GetComponent<UITexture>().mainTexture = this.StaffTexture[i];
				gameObject.GetComponent<UINewStoreToolStaffPrefab>().index = i;
			}
			this.isStaffInstantiated = true;
		}
	}

	// Token: 0x060017BC RID: 6076 RVA: 0x000C8450 File Offset: 0x000C6850
	public void RefreshStaffProperty()
	{
		this.curStaffItemInfo = GrowthManagerKit.GetAllWeaponPropertyCardItemInfo()[this.curSelectedStaffIndex];
		this.StaffNameLabel.text = this.curStaffItemInfo.mNameDisplay;
		this.StaffDesLabel.text = this.curStaffItemInfo.mDescription;
		this.StaffRestNumLabel.text = this.curStaffItemInfo.mExistNum.ToString();
	}

	// Token: 0x060017BD RID: 6077 RVA: 0x000C84BC File Offset: 0x000C68BC
	public void RefreshStaffPurchase()
	{
		this.curStaffItemInfo = GrowthManagerKit.GetAllWeaponPropertyCardItemInfo()[this.curSelectedStaffIndex];
		this.StaffPriceTypeSprite.spriteName = ((this.curStaffItemInfo.mPurchasedType != GItemPurchaseType.CoinsPurchase) ? ((this.curStaffItemInfo.mPurchasedType != GItemPurchaseType.GemPurchase) ? "Null" : "Gem") : "Coin");
		this.StaffPriceNumLabel.text = this.curStaffItemInfo.mSinglePrice.ToString();
		if (this.curStaffItemInfo.mSellable)
		{
			this.StaffBuyBtn.SetActive(true);
		}
		else
		{
			this.StaffBuyBtn.SetActive(false);
		}
	}

	// Token: 0x060017BE RID: 6078 RVA: 0x000C8570 File Offset: 0x000C6970
	public void StaffPurchase()
	{
		if (this.StaffItemInfo[this.curSelectedStaffIndex].mPurchasedType == GItemPurchaseType.CoinsPurchase)
		{
			if (GrowthManagerKit.GetCoins() >= this.StaffItemInfo[this.curSelectedStaffIndex].mSinglePrice)
			{
				GrowthManagerKit.SubCoins(this.StaffItemInfo[this.curSelectedStaffIndex].mSinglePrice);
				this.StaffItemInfo[this.curSelectedStaffIndex].AddCardNum(1);
				this.RefreshStaffProperty();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
		else if (this.StaffItemInfo[this.curSelectedStaffIndex].mPurchasedType == GItemPurchaseType.GemPurchase)
		{
			if (GrowthManagerKit.GetGems() >= this.StaffItemInfo[this.curSelectedStaffIndex].mSinglePrice)
			{
				GrowthManagerKit.SubGems(this.StaffItemInfo[this.curSelectedStaffIndex].mSinglePrice);
				this.StaffItemInfo[this.curSelectedStaffIndex].AddCardNum(1);
				this.RefreshStaffProperty();
			}
			else
			{
				UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
			}
		}
	}

	// Token: 0x04001AE2 RID: 6882
	public int curSelectedStaffIndex;

	// Token: 0x04001AE3 RID: 6883
	private int preSelectedStaffIndex = -1;

	// Token: 0x04001AE4 RID: 6884
	private string[] StaffNameList;

	// Token: 0x04001AE5 RID: 6885
	private GWeaponPropertyCardItemInfo[] StaffItemInfo;

	// Token: 0x04001AE6 RID: 6886
	public GameObject StaffItemPrefab;

	// Token: 0x04001AE7 RID: 6887
	public GameObject StaffObjParent;

	// Token: 0x04001AE8 RID: 6888
	private List<GameObject> StaffItemObjList = new List<GameObject>();

	// Token: 0x04001AE9 RID: 6889
	public Texture[] StaffTexture;

	// Token: 0x04001AEA RID: 6890
	private GWeaponPropertyCardItemInfo curStaffItemInfo;

	// Token: 0x04001AEB RID: 6891
	public UILabel StaffNameLabel;

	// Token: 0x04001AEC RID: 6892
	public UILabel StaffDesLabel;

	// Token: 0x04001AED RID: 6893
	public UILabel StaffRestNumLabel;

	// Token: 0x04001AEE RID: 6894
	public GameObject StaffBuyBtn;

	// Token: 0x04001AEF RID: 6895
	public UISprite StaffPriceTypeSprite;

	// Token: 0x04001AF0 RID: 6896
	public UILabel StaffPriceNumLabel;

	// Token: 0x04001AF1 RID: 6897
	public UILabel StaffOffRateLabel;

	// Token: 0x04001AF2 RID: 6898
	private bool isStaffInstantiated;
}
