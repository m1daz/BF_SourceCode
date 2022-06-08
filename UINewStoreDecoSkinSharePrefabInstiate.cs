using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002F3 RID: 755
public class UINewStoreDecoSkinSharePrefabInstiate : MonoBehaviour
{
	// Token: 0x06001747 RID: 5959 RVA: 0x000C56B8 File Offset: 0x000C3AB8
	private void Start()
	{
		this.SkinShareDownloadLabel.SetActive(false);
		this.InitShareSkinProperty();
		this.InitShareSkinDownload();
		SkinManager.OnSharedSkinsDownloaded += this.OnSharedSkinsDownloaded;
	}

	// Token: 0x06001748 RID: 5960 RVA: 0x000C56E3 File Offset: 0x000C3AE3
	private void OnDestroy()
	{
		SkinManager.OnSharedSkinsDownloaded -= this.OnSharedSkinsDownloaded;
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x000C56F8 File Offset: 0x000C3AF8
	private void OnSharedSkinsDownloaded()
	{
		this.InstantiateShareSkin();
		UITipController.mInstance.HideCurTip();
		this.SkinShareRefreshButton.SetActive(true);
		if (SkinManager.mInstance.sharedSkinEntityList.Count != 0)
		{
			base.GetComponent<UIScrollView>().transform.localPosition = new Vector3(0f, 0f, 0f);
			base.GetComponent<UIPanel>().clipOffset = new Vector2(0f, 0f);
			this.curSelectedSkinShareIndex = 0;
			this.RefreshShareSkinProperty();
			this.RefreshShareSkinDownload();
			this.ShareSkinShowForCharacter();
		}
		else
		{
			UINewStoreBasicWindowDirector.mInstance.TipDownloadShareSkinFail();
		}
	}

	// Token: 0x0600174A RID: 5962 RVA: 0x000C579C File Offset: 0x000C3B9C
	private void Update()
	{
		if (!this.isSkinShareInstantiated)
		{
			return;
		}
		if (this.curSelectedSkinShareIndex != this.preSelectedSkinShareIndex && this.isSkinShareInstantiated)
		{
			this.preSelectedSkinShareIndex = this.curSelectedSkinShareIndex;
			this.RefreshShareSkinProperty();
			this.RefreshShareSkinDownload();
			this.ShareSkinShowForCharacter();
		}
	}

	// Token: 0x0600174B RID: 5963 RVA: 0x000C57F0 File Offset: 0x000C3BF0
	public void RefreshShareSkin()
	{
		while (this.NewStoreSkinShareItem.Count > 0)
		{
			for (int i = 0; i < this.NewStoreSkinShareItem.Count; i++)
			{
				UnityEngine.Object.DestroyImmediate(this.NewStoreSkinShareItem[i]);
			}
			this.NewStoreSkinShareItem.Clear();
		}
		UINewStoreBasicWindowDirector.mInstance.TipDownloadShareSkin();
		SkinManager.mInstance.RequestSharedSkins();
		this.InitShareSkinProperty();
		this.InitShareSkinDownload();
		this.SkinShareRefreshButton.SetActive(false);
		this.isSkinShareInstantiated = false;
	}

	// Token: 0x0600174C RID: 5964 RVA: 0x000C587E File Offset: 0x000C3C7E
	public void DisplayShareSkinButton()
	{
		this.SkinShareRefreshButton.SetActive(true);
	}

	// Token: 0x0600174D RID: 5965 RVA: 0x000C588C File Offset: 0x000C3C8C
	public void HideShareSkinButton()
	{
		this.SkinShareRefreshButton.SetActive(false);
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x000C589C File Offset: 0x000C3C9C
	public void InstantiateShareSkin()
	{
		if (!this.isSkinShareInstantiated)
		{
			this.skinShareEntityList = SkinManager.mInstance.sharedSkinEntityList;
			for (int i = 0; i < this.skinShareEntityList.Count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.SkinSharePrefab, this.SkinSharePrefab.transform.position, this.SkinSharePrefab.transform.rotation);
				gameObject.transform.parent = this.SkinShareObjParent.transform;
				gameObject.transform.localScale = this.SkinSharePrefab.transform.localScale;
				gameObject.transform.localPosition = new Vector3((float)(i - 1) * 200f, 0f, 0f);
				gameObject.transform.rotation = this.SkinSharePrefab.transform.rotation;
				gameObject.transform.Find("skinShareModel/player_model").GetComponent<Renderer>().material.mainTexture = this.skinShareEntityList[i].tex;
				gameObject.GetComponent<UINewStoreDecoSkinSharePrefab>().index = i;
				this.NewStoreSkinShareItem.Add(gameObject);
			}
			this.isSkinShareInstantiated = true;
		}
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x000C59CC File Offset: 0x000C3DCC
	public void ShareSkinDownload()
	{
		if (SkinManager.mInstance.myCustomSkinEntityList.Count >= 30)
		{
			UINewStoreBasicWindowDirector.mInstance.TipReachCustomSkinLimit();
		}
		else
		{
			this.mUINewStoreDecoSkinPrefabInstiate.isSkinInstantiated = false;
			SkinManager.mInstance.DownloadSkinFromSharedList(this.skinShareEntityList[this.curSelectedSkinShareIndex]);
			this.SkinShareDownloadButton.SetActive(false);
		}
	}

	// Token: 0x06001750 RID: 5968 RVA: 0x000C5A31 File Offset: 0x000C3E31
	public void InitShareSkinProperty()
	{
		this.skinNameLabel.text = string.Empty;
		this.descriptionLabel.text = string.Empty;
		this.enchantmentPropetyLabel.text = string.Empty;
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x000C5A64 File Offset: 0x000C3E64
	public void RefreshShareSkinProperty()
	{
		if (SkinManager.mInstance.sharedSkinEntityList.Count > this.curSelectedSkinShareIndex)
		{
			this.curSelectedShareSkinItemInfo = this.skinShareEntityList[this.curSelectedSkinShareIndex];
			this.skinNameLabel.text = this.curSelectedShareSkinItemInfo.info.mNameDisplay;
			this.descriptionLabel.text = this.curSelectedShareSkinItemInfo.info.mDescription;
			this.enchantmentPropetyLabel.text = string.Empty;
		}
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x000C5AE8 File Offset: 0x000C3EE8
	public void InitShareSkinDownload()
	{
		this.SkinShareDownloadButton.SetActive(false);
	}

	// Token: 0x06001753 RID: 5971 RVA: 0x000C5AF8 File Offset: 0x000C3EF8
	public void RefreshShareSkinDownload()
	{
		if (SkinManager.mInstance.sharedSkinEntityList.Count > this.curSelectedSkinShareIndex)
		{
			this.curSelectedShareSkinItemInfo = this.skinShareEntityList[this.curSelectedSkinShareIndex];
			if (SkinManager.mInstance.CanDownload(this.curSelectedShareSkinItemInfo))
			{
				this.SkinShareDownloadButton.SetActive(true);
			}
			else
			{
				this.SkinShareDownloadButton.SetActive(false);
			}
		}
	}

	// Token: 0x06001754 RID: 5972 RVA: 0x000C5B68 File Offset: 0x000C3F68
	public void ShareSkinShowForCharacter()
	{
		if (SkinManager.mInstance.sharedSkinEntityList.Count > this.curSelectedSkinShareIndex)
		{
			UINewStoreEquipForCharacter.mInstance.EquipSkin(this.skinShareEntityList[this.curSelectedSkinShareIndex].tex);
		}
	}

	// Token: 0x04001A42 RID: 6722
	private List<SkinEntity> skinShareEntityList;

	// Token: 0x04001A43 RID: 6723
	public UINewStoreDecoSkinPrefabInstiate mUINewStoreDecoSkinPrefabInstiate;

	// Token: 0x04001A44 RID: 6724
	public int curSelectedSkinShareIndex;

	// Token: 0x04001A45 RID: 6725
	private int preSelectedSkinShareIndex = -1;

	// Token: 0x04001A46 RID: 6726
	public GameObject SkinSharePrefab;

	// Token: 0x04001A47 RID: 6727
	public GameObject SkinShareObjParent;

	// Token: 0x04001A48 RID: 6728
	public List<GameObject> NewStoreSkinShareItem;

	// Token: 0x04001A49 RID: 6729
	public UILabel skinNameLabel;

	// Token: 0x04001A4A RID: 6730
	public UILabel descriptionLabel;

	// Token: 0x04001A4B RID: 6731
	public UILabel enchantmentPropetyLabel;

	// Token: 0x04001A4C RID: 6732
	private SkinEntity curSelectedShareSkinItemInfo;

	// Token: 0x04001A4D RID: 6733
	public GameObject SkinShareDownloadLabel;

	// Token: 0x04001A4E RID: 6734
	public GameObject SkinShareDownloadButton;

	// Token: 0x04001A4F RID: 6735
	public GameObject SkinShareRefreshButton;

	// Token: 0x04001A50 RID: 6736
	private bool isSkinShareInstantiated;

	// Token: 0x04001A51 RID: 6737
	public int curSettedSkinIndex;
}
