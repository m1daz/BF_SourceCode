using System;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class UINewStoreEquipForCharacter : MonoBehaviour
{
	// Token: 0x06001764 RID: 5988 RVA: 0x000C5FE6 File Offset: 0x000C43E6
	private void Awake()
	{
		if (UINewStoreEquipForCharacter.mInstance == null)
		{
			UINewStoreEquipForCharacter.mInstance = this;
		}
	}

	// Token: 0x06001765 RID: 5989 RVA: 0x000C5FFE File Offset: 0x000C43FE
	private void OnDestroy()
	{
		if (UINewStoreEquipForCharacter.mInstance != null)
		{
			UINewStoreEquipForCharacter.mInstance = null;
		}
	}

	// Token: 0x06001766 RID: 5990 RVA: 0x000C6016 File Offset: 0x000C4416
	private void Start()
	{
	}

	// Token: 0x06001767 RID: 5991 RVA: 0x000C6018 File Offset: 0x000C4418
	private void Update()
	{
	}

	// Token: 0x06001768 RID: 5992 RVA: 0x000C601A File Offset: 0x000C441A
	public void EquipSkin(Texture skinTexture)
	{
		this.playerSkin.GetComponent<Renderer>().material.mainTexture = skinTexture;
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x000C6034 File Offset: 0x000C4434
	public void EquipHat(int hatIndex)
	{
		for (int i = 0; i < this.hats.Length; i++)
		{
			if (i != hatIndex - 1)
			{
				if (this.hats[i].activeSelf)
				{
					this.hats[i].SetActive(false);
				}
			}
			else
			{
				this.hats[i].SetActive(true);
			}
		}
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x000C6098 File Offset: 0x000C4498
	public void EquipCape(int capeIndex)
	{
		for (int i = 0; i < this.capes.Length; i++)
		{
			if (i != capeIndex - 1)
			{
				if (this.capes[i].activeSelf)
				{
					this.capes[i].SetActive(false);
				}
			}
			else
			{
				this.capes[i].SetActive(true);
			}
		}
	}

	// Token: 0x0600176B RID: 5995 RVA: 0x000C60FC File Offset: 0x000C44FC
	public void EquipBoot(int bootIndex)
	{
		for (int i = 0; i < this.leftshoes.Length; i++)
		{
			if (i != bootIndex - 1)
			{
				if (this.leftshoes[i].activeSelf)
				{
					this.leftshoes[i].SetActive(false);
				}
			}
			else
			{
				this.leftshoes[i].SetActive(true);
			}
		}
		for (int j = 0; j < this.rightshoes.Length; j++)
		{
			if (j != bootIndex - 1)
			{
				if (this.rightshoes[j].activeSelf)
				{
					this.rightshoes[j].SetActive(false);
				}
			}
			else
			{
				this.rightshoes[j].SetActive(true);
			}
		}
	}

	// Token: 0x04001A6A RID: 6762
	public static UINewStoreEquipForCharacter mInstance;

	// Token: 0x04001A6B RID: 6763
	public GameObject playerSkin;

	// Token: 0x04001A6C RID: 6764
	public Texture[] skins;

	// Token: 0x04001A6D RID: 6765
	public GameObject[] hats;

	// Token: 0x04001A6E RID: 6766
	public GameObject[] capes;

	// Token: 0x04001A6F RID: 6767
	public GameObject[] leftshoes;

	// Token: 0x04001A70 RID: 6768
	public GameObject[] rightshoes;
}
