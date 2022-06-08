using System;
using UnityEngine;

// Token: 0x02000308 RID: 776
public class UINewStoreToolWindowDirector : MonoBehaviour
{
	// Token: 0x060017CB RID: 6091 RVA: 0x000C883A File Offset: 0x000C6C3A
	private void Awake()
	{
		if (UINewStoreToolWindowDirector.mInstance == null)
		{
			UINewStoreToolWindowDirector.mInstance = this;
		}
	}

	// Token: 0x060017CC RID: 6092 RVA: 0x000C8852 File Offset: 0x000C6C52
	private void OnDestroy()
	{
		if (UINewStoreToolWindowDirector.mInstance != null)
		{
			UINewStoreToolWindowDirector.mInstance = null;
		}
	}

	// Token: 0x060017CD RID: 6093 RVA: 0x000C886A File Offset: 0x000C6C6A
	private void Start()
	{
	}

	// Token: 0x060017CE RID: 6094 RVA: 0x000C886C File Offset: 0x000C6C6C
	private void Update()
	{
	}

	// Token: 0x060017CF RID: 6095 RVA: 0x000C8870 File Offset: 0x000C6C70
	public void PotionBtnPressed()
	{
		this.potionScrollView.SetActive(true);
		this.keyScrollView.SetActive(false);
		this.FireworkScrollView.SetActive(false);
		this.StaffScrollView.SetActive(false);
		this.TicketScrollView.SetActive(false);
		this.potionProperty.SetActive(true);
		this.keyProperty.SetActive(false);
		this.FireworkProperty.SetActive(false);
		this.StaffProperty.SetActive(false);
		this.TicketProperty.SetActive(false);
		this.potionBuy.SetActive(true);
		this.keyBuy.SetActive(false);
		this.FireworkBuy.SetActive(false);
		this.StaffBuy.SetActive(false);
		this.TicketBuy.SetActive(false);
		this.potionScrollViewInstiate.InstantiatePotion();
	}

	// Token: 0x060017D0 RID: 6096 RVA: 0x000C893C File Offset: 0x000C6D3C
	public void KeyBtnPressed()
	{
		this.potionScrollView.SetActive(false);
		this.keyScrollView.SetActive(true);
		this.FireworkScrollView.SetActive(false);
		this.StaffScrollView.SetActive(false);
		this.TicketScrollView.SetActive(false);
		this.potionProperty.SetActive(false);
		this.keyProperty.SetActive(true);
		this.FireworkProperty.SetActive(false);
		this.StaffProperty.SetActive(false);
		this.TicketProperty.SetActive(false);
		this.potionBuy.SetActive(false);
		this.keyBuy.SetActive(true);
		this.FireworkBuy.SetActive(false);
		this.StaffBuy.SetActive(false);
		this.TicketBuy.SetActive(false);
		this.keyScrollViewInstiate.InstantiateKey();
	}

	// Token: 0x060017D1 RID: 6097 RVA: 0x000C8A08 File Offset: 0x000C6E08
	public void FireworkBtnPressed()
	{
		this.potionScrollView.SetActive(false);
		this.keyScrollView.SetActive(false);
		this.FireworkScrollView.SetActive(true);
		this.StaffScrollView.SetActive(false);
		this.TicketScrollView.SetActive(false);
		this.potionProperty.SetActive(false);
		this.keyProperty.SetActive(false);
		this.FireworkProperty.SetActive(true);
		this.StaffProperty.SetActive(false);
		this.TicketProperty.SetActive(false);
		this.potionBuy.SetActive(false);
		this.keyBuy.SetActive(false);
		this.FireworkBuy.SetActive(true);
		this.StaffBuy.SetActive(false);
		this.TicketBuy.SetActive(false);
		this.FireworkScrollViewInstiate.InstantiateFirework();
	}

	// Token: 0x060017D2 RID: 6098 RVA: 0x000C8AD4 File Offset: 0x000C6ED4
	public void StaffBtnPressed()
	{
		this.potionScrollView.SetActive(false);
		this.keyScrollView.SetActive(false);
		this.FireworkScrollView.SetActive(false);
		this.StaffScrollView.SetActive(true);
		this.TicketScrollView.SetActive(false);
		this.potionProperty.SetActive(false);
		this.keyProperty.SetActive(false);
		this.FireworkProperty.SetActive(false);
		this.StaffProperty.SetActive(true);
		this.TicketProperty.SetActive(false);
		this.potionBuy.SetActive(false);
		this.keyBuy.SetActive(false);
		this.FireworkBuy.SetActive(false);
		this.StaffBuy.SetActive(true);
		this.TicketBuy.SetActive(false);
		this.StaffScrollViewInstiate.InstantiateStaff();
	}

	// Token: 0x060017D3 RID: 6099 RVA: 0x000C8BA0 File Offset: 0x000C6FA0
	public void TicketBtnPressed()
	{
		this.potionScrollView.SetActive(false);
		this.keyScrollView.SetActive(false);
		this.FireworkScrollView.SetActive(false);
		this.StaffScrollView.SetActive(false);
		this.TicketScrollView.SetActive(true);
		this.potionProperty.SetActive(false);
		this.keyProperty.SetActive(false);
		this.FireworkProperty.SetActive(false);
		this.StaffProperty.SetActive(false);
		this.TicketProperty.SetActive(true);
		this.potionBuy.SetActive(false);
		this.keyBuy.SetActive(false);
		this.FireworkBuy.SetActive(false);
		this.StaffBuy.SetActive(false);
		this.TicketBuy.SetActive(true);
		this.TicketScrollViewInstiate.InstantiateTicket();
		this.TicketScrollViewInstiate.BackToTicketUIToggle();
	}

	// Token: 0x04001B01 RID: 6913
	public static UINewStoreToolWindowDirector mInstance;

	// Token: 0x04001B02 RID: 6914
	public GameObject potionScrollView;

	// Token: 0x04001B03 RID: 6915
	public GameObject keyScrollView;

	// Token: 0x04001B04 RID: 6916
	public GameObject FireworkScrollView;

	// Token: 0x04001B05 RID: 6917
	public GameObject StaffScrollView;

	// Token: 0x04001B06 RID: 6918
	public GameObject TicketScrollView;

	// Token: 0x04001B07 RID: 6919
	public UINewStoreToolPotionPrefabInstiate potionScrollViewInstiate;

	// Token: 0x04001B08 RID: 6920
	public UINewStoreToolKeyPrefabInstiate keyScrollViewInstiate;

	// Token: 0x04001B09 RID: 6921
	public UINewStoreToolFireworkPrefabInstiate FireworkScrollViewInstiate;

	// Token: 0x04001B0A RID: 6922
	public UINewStoreToolStaffPrefabInstiate StaffScrollViewInstiate;

	// Token: 0x04001B0B RID: 6923
	public UINewStoreToolTicketPrefabInstiate TicketScrollViewInstiate;

	// Token: 0x04001B0C RID: 6924
	public GameObject potionProperty;

	// Token: 0x04001B0D RID: 6925
	public GameObject keyProperty;

	// Token: 0x04001B0E RID: 6926
	public GameObject FireworkProperty;

	// Token: 0x04001B0F RID: 6927
	public GameObject StaffProperty;

	// Token: 0x04001B10 RID: 6928
	public GameObject TicketProperty;

	// Token: 0x04001B11 RID: 6929
	public GameObject potionBuy;

	// Token: 0x04001B12 RID: 6930
	public GameObject keyBuy;

	// Token: 0x04001B13 RID: 6931
	public GameObject FireworkBuy;

	// Token: 0x04001B14 RID: 6932
	public GameObject StaffBuy;

	// Token: 0x04001B15 RID: 6933
	public GameObject TicketBuy;
}
