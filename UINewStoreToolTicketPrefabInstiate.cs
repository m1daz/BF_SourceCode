using System;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class UINewStoreToolTicketPrefabInstiate : MonoBehaviour
{
	// Token: 0x060017C3 RID: 6083 RVA: 0x000C868F File Offset: 0x000C6A8F
	private void Start()
	{
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x000C8691 File Offset: 0x000C6A91
	private void Update()
	{
		if (!this.isTicketInstantiated)
		{
			return;
		}
		if (this.curSelectedTicketIndex != this.preSelectedTicketIndex)
		{
			this.preSelectedTicketIndex = this.curSelectedTicketIndex;
			this.RefreshTicketProperty();
			this.RefreshTicketPurchase();
		}
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x000C86C8 File Offset: 0x000C6AC8
	public void InstantiateTicket()
	{
		if (!this.isTicketInstantiated)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TicketItemPrefab, this.TicketItemPrefab.transform.position, this.TicketItemPrefab.transform.rotation);
			gameObject.transform.parent = this.TicketObjParent.transform;
			gameObject.transform.localScale = this.TicketItemPrefab.transform.localScale;
			gameObject.transform.localPosition = new Vector3(-200f, 0f, 0f);
			gameObject.transform.rotation = this.TicketItemPrefab.transform.rotation;
			this.isTicketInstantiated = true;
		}
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x000C8780 File Offset: 0x000C6B80
	public void RefreshTicketProperty()
	{
		this.TicketNameLabel.text = "TICKET";
		this.TicketDesLabel.text = "EVERY TICKET GIVE YOU A CHANCE TO CHALLENGE HUNTING!";
		this.TicketRestNumLabel.text = GrowthManagerKit.GetHuntingTickets().ToString();
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x000C87CB File Offset: 0x000C6BCB
	public void RefreshTicketPurchase()
	{
		this.TicketPriceTypeSprite.spriteName = "Coin";
		this.TicketPriceNumLabel.text = "300";
	}

	// Token: 0x060017C8 RID: 6088 RVA: 0x000C87ED File Offset: 0x000C6BED
	public void TicketPurchase()
	{
		if (GrowthManagerKit.GetCoins() >= 300)
		{
			GrowthManagerKit.SubCoins(300);
			GrowthManagerKit.AddHuntingTickets(1);
			this.RefreshTicketProperty();
		}
		else
		{
			UINewStoreBasicWindowDirector.mInstance.TipGoToShop();
		}
	}

	// Token: 0x060017C9 RID: 6089 RVA: 0x000C8824 File Offset: 0x000C6C24
	public void BackToTicketUIToggle()
	{
		this.TicketUIToggle.value = true;
	}

	// Token: 0x04001AF3 RID: 6899
	public int curSelectedTicketIndex;

	// Token: 0x04001AF4 RID: 6900
	private int preSelectedTicketIndex = -1;

	// Token: 0x04001AF5 RID: 6901
	public GameObject TicketItemPrefab;

	// Token: 0x04001AF6 RID: 6902
	public GameObject TicketObjParent;

	// Token: 0x04001AF7 RID: 6903
	public UIToggle TicketUIToggle;

	// Token: 0x04001AF8 RID: 6904
	public UILabel TicketNameLabel;

	// Token: 0x04001AF9 RID: 6905
	public UILabel TicketDesLabel;

	// Token: 0x04001AFA RID: 6906
	public UILabel TicketRestNumLabel;

	// Token: 0x04001AFB RID: 6907
	public GameObject TicketBuyBtn;

	// Token: 0x04001AFC RID: 6908
	public UISprite TicketPriceTypeSprite;

	// Token: 0x04001AFD RID: 6909
	public UILabel TicketPriceNumLabel;

	// Token: 0x04001AFE RID: 6910
	public UILabel TicketOffRateLabel;

	// Token: 0x04001AFF RID: 6911
	private bool isTicketInstantiated;

	// Token: 0x04001B00 RID: 6912
	private string TicketItemSpriteName = "Ticket";
}
