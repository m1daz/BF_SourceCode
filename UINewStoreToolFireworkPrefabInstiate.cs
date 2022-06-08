using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FF RID: 767
public class UINewStoreToolFireworkPrefabInstiate : MonoBehaviour
{
	// Token: 0x06001799 RID: 6041 RVA: 0x000C7240 File Offset: 0x000C5640
	private void Start()
	{
	}

	// Token: 0x0600179A RID: 6042 RVA: 0x000C7242 File Offset: 0x000C5642
	private void Update()
	{
		if (!this.isFireworkInstantiated)
		{
			return;
		}
		if (this.curSelectedFireworkIndex != this.preSelectedFireworkIndex)
		{
			this.preSelectedFireworkIndex = this.curSelectedFireworkIndex;
			this.RefreshFireworkProperty();
			this.RefreshFireworkPurchase();
		}
	}

	// Token: 0x0600179B RID: 6043 RVA: 0x000C727C File Offset: 0x000C567C
	public void InstantiateFirework()
	{
		if (!this.isFireworkInstantiated)
		{
			for (int i = 0; i < 4; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.fireworkItemPrefab, this.fireworkItemPrefab.transform.position, this.fireworkItemPrefab.transform.rotation);
				gameObject.transform.parent = this.fireworkObjParent.transform;
				gameObject.transform.localScale = this.fireworkItemPrefab.transform.localScale;
				gameObject.transform.localPosition = new Vector3((float)(i - 1) * 200f, 0f, 0f);
				gameObject.transform.rotation = this.fireworkItemPrefab.transform.rotation;
				gameObject.transform.Find("fireworkModel/fireworkTexture").GetComponent<UITexture>().mainTexture = this.FireworkTexture[i];
				gameObject.GetComponent<UINewStoreToolFireworkPrefab>().index = i;
			}
			this.isFireworkInstantiated = true;
		}
	}

	// Token: 0x0600179C RID: 6044 RVA: 0x000C7378 File Offset: 0x000C5778
	public void RefreshFireworkProperty()
	{
		switch (this.curSelectedFireworkIndex)
		{
		case 0:
			this.fireworkNameLabel.text = "GENERAL FIREWORK";
			this.fireworkDesLabel.text = "COMMON FIREWORK, HOLODAY MUST-HAVE.";
			this.fireworkRestNumLabel.text = PlayerPrefs.GetInt("Firework1", 0).ToString();
			break;
		case 1:
			this.fireworkNameLabel.text = "UMBRELLA FIREWORK";
			this.fireworkDesLabel.text = "SPREAD AROUND LIKE UMBRELLA, BRING STRONG FESTIVAL ATMOSPHERE.";
			this.fireworkRestNumLabel.text = PlayerPrefs.GetInt("Firework2", 0).ToString();
			break;
		case 2:
			this.fireworkNameLabel.text = "FIREFLIES FIREWORK";
			this.fireworkDesLabel.text = "INTERMITTENT ERUPTION, LIKE FIREFLIES FLYING IN THE AIR.";
			this.fireworkRestNumLabel.text = PlayerPrefs.GetInt("Firework3", 0).ToString();
			break;
		case 3:
			this.fireworkNameLabel.text = "COLORFUL FIREWORK";
			this.fireworkDesLabel.text = "TWO PHASE ERUPTION, WIDE RANGE, GORGEOUS AND BEAUTIFUL.";
			this.fireworkRestNumLabel.text = PlayerPrefs.GetInt("Firework4", 0).ToString();
			break;
		}
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x000C74D1 File Offset: 0x000C58D1
	public void RefreshFireworkPurchase()
	{
		this.fireworkPriceTypeSprite.spriteName = "Coin";
		this.fireworkPriceNumLabel.text = "100";
		this.fireworkNumLabel.text = "X5";
	}

	// Token: 0x0600179E RID: 6046 RVA: 0x000C7504 File Offset: 0x000C5904
	public void FireworkPurchase()
	{
		if (GrowthManagerKit.GetCoins() >= 100)
		{
			GrowthManagerKit.SubCoins(100);
			PlayerPrefs.SetInt("Firework1", PlayerPrefs.GetInt("Firework1", 0) + 5);
			this.RefreshFireworkProperty();
		}
		else
		{
			UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
		}
	}

	// Token: 0x04001A99 RID: 6809
	public int curSelectedFireworkIndex;

	// Token: 0x04001A9A RID: 6810
	private int preSelectedFireworkIndex = -1;

	// Token: 0x04001A9B RID: 6811
	private string[] fireworkNameList;

	// Token: 0x04001A9C RID: 6812
	public GameObject fireworkItemPrefab;

	// Token: 0x04001A9D RID: 6813
	public GameObject fireworkObjParent;

	// Token: 0x04001A9E RID: 6814
	private List<GameObject> fireworkItemObjList = new List<GameObject>();

	// Token: 0x04001A9F RID: 6815
	public Texture[] FireworkTexture;

	// Token: 0x04001AA0 RID: 6816
	public UILabel fireworkNameLabel;

	// Token: 0x04001AA1 RID: 6817
	public UILabel fireworkDesLabel;

	// Token: 0x04001AA2 RID: 6818
	public UILabel fireworkRestNumLabel;

	// Token: 0x04001AA3 RID: 6819
	public GameObject fireworkBuyBtn;

	// Token: 0x04001AA4 RID: 6820
	public UISprite fireworkPriceTypeSprite;

	// Token: 0x04001AA5 RID: 6821
	public UILabel fireworkPriceNumLabel;

	// Token: 0x04001AA6 RID: 6822
	public UILabel fireworkOffRateLabel;

	// Token: 0x04001AA7 RID: 6823
	public UILabel fireworkNumLabel;

	// Token: 0x04001AA8 RID: 6824
	private bool isFireworkInstantiated;
}
