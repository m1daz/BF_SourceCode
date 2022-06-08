using System;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public class UILuckyListItemPrefab : MonoBehaviour
{
	// Token: 0x060015A4 RID: 5540 RVA: 0x000B8FDF File Offset: 0x000B73DF
	private void Start()
	{
	}

	// Token: 0x060015A5 RID: 5541 RVA: 0x000B8FE4 File Offset: 0x000B73E4
	public void ReadData(CSSlotTopPrizeInfo info)
	{
		this.roleNameLabel.text = "[FFCC00]" + info.RoleName + "[-][00CC00] Get[-]";
		this.numLabel.text = info.PrizeInfo;
		this.itemLogo.mainTexture = (Resources.Load("UI/Images/SlotLogo/" + info.PrizeName) as Texture);
	}

	// Token: 0x04001876 RID: 6262
	public UILabel roleNameLabel;

	// Token: 0x04001877 RID: 6263
	public UITexture itemLogo;

	// Token: 0x04001878 RID: 6264
	public UILabel numLabel;
}
