using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000301 RID: 769
public class UINewStoreToolKeyPrefabInstiate : MonoBehaviour
{
	// Token: 0x060017A3 RID: 6051 RVA: 0x000C77BD File Offset: 0x000C5BBD
	private void Start()
	{
		this.keyInit();
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x000C77C5 File Offset: 0x000C5BC5
	private void Update()
	{
		if (!this.isKeyInstantiated)
		{
			return;
		}
		if (this.curSelectedKeyIndex != this.preSelectedKeyIndex)
		{
			this.preSelectedKeyIndex = this.curSelectedKeyIndex;
			this.RefreshKeyProperty();
			this.RefreshKeyPurchase();
		}
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x000C77FC File Offset: 0x000C5BFC
	public void UnlockEquipWindowBtnPressed()
	{
		GrowthManagerKit.UpgradeWeaponEquipLimitedNum();
	}

	// Token: 0x060017A6 RID: 6054 RVA: 0x000C7804 File Offset: 0x000C5C04
	public void InstantiateKey()
	{
		if (!this.isKeyInstantiated)
		{
			for (int i = 0; i < 4; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.KeyItemPrefab, this.KeyItemPrefab.transform.position, this.KeyItemPrefab.transform.rotation);
				gameObject.transform.parent = this.KeyObjParent.transform;
				gameObject.transform.localScale = this.KeyItemPrefab.transform.localScale;
				gameObject.transform.localPosition = new Vector3((float)(i - 1) * 200f, 0f, 0f);
				gameObject.transform.rotation = this.KeyItemPrefab.transform.rotation;
				gameObject.transform.Find("keyModel/KeySprite").GetComponent<UISprite>().spriteName = this.KeyItemSpriteName[i];
				gameObject.GetComponent<UINewStoreToolKeyPrefab>().index = i;
			}
			this.isKeyInstantiated = true;
		}
	}

	// Token: 0x060017A7 RID: 6055 RVA: 0x000C7900 File Offset: 0x000C5D00
	public void RefreshKeyProperty()
	{
		switch (this.curSelectedKeyIndex)
		{
		case 0:
			this.KeyNameLabel.text = "COPPER KEY";
			this.KeyDesLabel.text = "COPPER KEY CAN UNLOCK THE FIRST WEAPON SLOT.";
			this.KeyUnlockItemLabel.text = "UNLOCK WEAPON SLOT : NO 5";
			break;
		case 1:
			this.KeyNameLabel.text = "SILVER KEY";
			this.KeyDesLabel.text = "SILVER KEY CAN UNLOCK THE SECOND WEAPON SLOT.";
			this.KeyUnlockItemLabel.text = "UNLOCK WEAPON SLOT : NO 6";
			break;
		case 2:
			this.KeyNameLabel.text = "GOLD KEY";
			this.KeyDesLabel.text = "GOLD KEY CAN UNLOCK THE THIRD WEAPON SLOT.";
			this.KeyUnlockItemLabel.text = "UNLOCK WEAPON SLOT : NO 7";
			break;
		case 3:
			this.KeyNameLabel.text = "DIAMOND KEY";
			this.KeyDesLabel.text = "DIAMOND KEY CAN UNLOCK THE ULTIMATE WEAPON SLOT.";
			this.KeyUnlockItemLabel.text = "UNLOCK WEAPON SLOT : NO 8";
			break;
		}
	}

	// Token: 0x060017A8 RID: 6056 RVA: 0x000C7A08 File Offset: 0x000C5E08
	public void RefreshKeyPurchase()
	{
		int curWeaponEquipLimitedNum = GrowthManagerKit.GetCurWeaponEquipLimitedNum();
		if (curWeaponEquipLimitedNum - 4 > this.curSelectedKeyIndex)
		{
			this.KeyBuyBtn.SetActive(false);
			this.KeyUnlockedLabel.SetActive(true);
		}
		else
		{
			this.KeyBuyBtn.SetActive(true);
			this.KeyUnlockedLabel.SetActive(false);
		}
		this.KeyPriceTypeSprite.spriteName = "Gem";
		this.KeyPriceNumLabel.text = "100";
	}

	// Token: 0x060017A9 RID: 6057 RVA: 0x000C7A7E File Offset: 0x000C5E7E
	public void KeyPurchase()
	{
		if (GrowthManagerKit.GetGems() >= 100)
		{
			GrowthManagerKit.UpgradeWeaponEquipLimitedNum();
			this.RefreshKeyPurchase();
			this.keyInit();
		}
		else
		{
			UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
		}
	}

	// Token: 0x060017AA RID: 6058 RVA: 0x000C7AB0 File Offset: 0x000C5EB0
	public void keyInit()
	{
		int curWeaponEquipLimitedNum = GrowthManagerKit.GetCurWeaponEquipLimitedNum();
		for (int i = 0; i < curWeaponEquipLimitedNum - 4; i++)
		{
			if (this.keySprite[i] != null)
			{
				this.keySprite[i].SetActive(false);
			}
		}
	}

	// Token: 0x04001AAE RID: 6830
	public int curSelectedKeyIndex;

	// Token: 0x04001AAF RID: 6831
	private int preSelectedKeyIndex = -1;

	// Token: 0x04001AB0 RID: 6832
	private string[] KeyNameList;

	// Token: 0x04001AB1 RID: 6833
	private GMultiplayerBuffItemInfo[] KeyItemInfo;

	// Token: 0x04001AB2 RID: 6834
	public GameObject KeyItemPrefab;

	// Token: 0x04001AB3 RID: 6835
	public GameObject KeyObjParent;

	// Token: 0x04001AB4 RID: 6836
	private List<GameObject> KeyItemObjList = new List<GameObject>();

	// Token: 0x04001AB5 RID: 6837
	public GameObject NewStoreNullItemPrefabEnd;

	// Token: 0x04001AB6 RID: 6838
	public GameObject NewStoreNullItemPrefabStart;

	// Token: 0x04001AB7 RID: 6839
	private GMultiplayerBuffItemInfo curKeyItemInfo;

	// Token: 0x04001AB8 RID: 6840
	public UILabel KeyNameLabel;

	// Token: 0x04001AB9 RID: 6841
	public UILabel KeyDesLabel;

	// Token: 0x04001ABA RID: 6842
	public UILabel KeyUnlockItemLabel;

	// Token: 0x04001ABB RID: 6843
	public GameObject KeyBuyBtn;

	// Token: 0x04001ABC RID: 6844
	public UISprite KeyPriceTypeSprite;

	// Token: 0x04001ABD RID: 6845
	public UILabel KeyPriceNumLabel;

	// Token: 0x04001ABE RID: 6846
	public UILabel KeyOffRateLabel;

	// Token: 0x04001ABF RID: 6847
	public GameObject KeyUnlockedLabel;

	// Token: 0x04001AC0 RID: 6848
	private bool isKeyInstantiated;

	// Token: 0x04001AC1 RID: 6849
	public GameObject[] keySprite;

	// Token: 0x04001AC2 RID: 6850
	private string[] KeyItemSpriteName = new string[]
	{
		"copperKey",
		"silverKey",
		"goldKey",
		"diamondKey"
	};
}
