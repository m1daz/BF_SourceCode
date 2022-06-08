using System;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class UIFriendInfoItemPrefab : MonoBehaviour
{
	// Token: 0x0600126B RID: 4715 RVA: 0x000A4D87 File Offset: 0x000A3187
	private void Start()
	{
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x000A4D89 File Offset: 0x000A3189
	private void Update()
	{
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x000A4D8C File Offset: 0x000A318C
	public void ReadData(CSFriendInfo info, int zIndex)
	{
		this.index = zIndex;
		if (GGCloudServiceKit.mInstance.mUserNameRoleNameDic.ContainsKey(info.Name))
		{
			this.nameLabel.text = GGCloudServiceKit.mInstance.mUserNameRoleNameDic[info.Name];
		}
		else
		{
			this.nameLabel.text = string.Empty;
		}
		if (info.IsHaveNewMessage)
		{
			this.msgLogoSprite.gameObject.SetActive(true);
		}
		else
		{
			this.msgLogoSprite.gameObject.SetActive(false);
		}
		if (info.IsOnline)
		{
			this.toggle.GetComponent<UIButton>().normalSprite = "BtnBg_1";
			this.rankLevelLabel.text = "LV." + info.rank;
			this.rankSprite.spriteName = "Rank_" + info.rank;
			if (info.IsInRoom)
			{
				this.playingSprite.gameObject.SetActive(true);
			}
			else
			{
				this.playingSprite.gameObject.SetActive(false);
			}
		}
		else
		{
			this.toggle.GetComponent<UIButton>().normalSprite = "SensitivityBg";
			this.rankLevelLabel.text = "LV.?";
			this.rankSprite.spriteName = "Rank_0";
			this.playingSprite.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x000A4EF4 File Offset: 0x000A32F4
	public void ToggleSelected()
	{
		if (this.toggle.value)
		{
			UIFriendSystemDirector.mInstance.FriendSelected(this.index);
		}
	}

	// Token: 0x04001520 RID: 5408
	public UILabel nameLabel;

	// Token: 0x04001521 RID: 5409
	public UISprite msgLogoSprite;

	// Token: 0x04001522 RID: 5410
	public UISprite rankSprite;

	// Token: 0x04001523 RID: 5411
	public UILabel rankLevelLabel;

	// Token: 0x04001524 RID: 5412
	public UISprite playingSprite;

	// Token: 0x04001525 RID: 5413
	public UIToggle toggle;

	// Token: 0x04001526 RID: 5414
	private int index;
}
