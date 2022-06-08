using System;
using UnityEngine;

// Token: 0x020002B2 RID: 690
public class UIPlayerInfoListPrefab : MonoBehaviour
{
	// Token: 0x06001434 RID: 5172 RVA: 0x000B0788 File Offset: 0x000AEB88
	private void Start()
	{
	}

	// Token: 0x06001435 RID: 5173 RVA: 0x000B078A File Offset: 0x000AEB8A
	private void Update()
	{
	}

	// Token: 0x06001436 RID: 5174 RVA: 0x000B078C File Offset: 0x000AEB8C
	public void AddFriendBtnPressed()
	{
		if (this.addFriendBtn.gameObject.activeSelf)
		{
			this.addFriendBtn.gameObject.SetActive(false);
			UIPauseDirector.mInstance.SendAddRequest(this.roleName);
		}
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x000B07C4 File Offset: 0x000AEBC4
	public void OnClick()
	{
		UIOtherPlayerInfoDirector.mInstance.ShowOtherPlayerProfileNode(this.playerInfoData, this.isFriendValue);
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x000B07DC File Offset: 0x000AEBDC
	public void SetInfoValue(int rankIndex, Color color, GGNetworkPlayerProperties playerInfo, bool isFriend)
	{
		this.isFriendValue = isFriend;
		this.playerInfoData = playerInfo;
		if (playerInfo.name == UIUserDataController.GetDefaultRoleName())
		{
			this.bgSprite.color = new Color(0f, 0.4392157f, 0.15686275f, 1f);
		}
		else if (playerInfo.isObserver)
		{
			this.bgSprite.color = UIPauseDirector.mInstance.myGrayColor;
		}
		else
		{
			this.bgSprite.color = color;
		}
		this.rankLabel.text = "- " + playerInfo.rank.ToString() + " -";
		this.rankSprite.spriteName = "Rank_" + playerInfo.rank.ToString();
		this.nickNameLabel.text = playerInfo.name;
		this.infoItem1Label.text = playerInfo.ping.ToString();
		if (UIModeDirector.mInstance.modeType == GGModeType.Mutation)
		{
			this.infoItem2Label.text = playerInfo.MutationModeScore.ToString();
			rankIndex = -1;
		}
		else
		{
			this.infoItem2Label.text = playerInfo.killNum.ToString() + "/" + playerInfo.deadNum.ToString();
		}
		if (UIModeDirector.mInstance.modeType == GGModeType.Explosion)
		{
			if (playerInfo.isTakeTimerBomb)
			{
				this.bombSprite.gameObject.SetActive(true);
			}
			else
			{
				this.bombSprite.gameObject.SetActive(false);
			}
		}
		if (!isFriend && playerInfo.name != UIUserDataController.GetDefaultRoleName())
		{
			this.roleName = playerInfo.name;
		}
		else
		{
			this.addFriendBtn.gameObject.SetActive(false);
		}
		switch (rankIndex)
		{
		case 0:
			this.crownSprite.gameObject.SetActive(true);
			this.crownSprite.spriteName = "KillRank_1";
			break;
		case 1:
			this.crownSprite.gameObject.SetActive(true);
			this.crownSprite.spriteName = "KillRank_2";
			break;
		case 2:
			this.crownSprite.gameObject.SetActive(true);
			this.crownSprite.spriteName = "KillRank_3";
			break;
		default:
			this.crownSprite.gameObject.SetActive(false);
			break;
		}
	}

	// Token: 0x0400170C RID: 5900
	public UILabel nickNameLabel;

	// Token: 0x0400170D RID: 5901
	public UILabel rankLabel;

	// Token: 0x0400170E RID: 5902
	public UILabel infoItem1Label;

	// Token: 0x0400170F RID: 5903
	public UILabel infoItem2Label;

	// Token: 0x04001710 RID: 5904
	public UISprite rankSprite;

	// Token: 0x04001711 RID: 5905
	public UISprite crownSprite;

	// Token: 0x04001712 RID: 5906
	public UISprite bombSprite;

	// Token: 0x04001713 RID: 5907
	public UISprite bgSprite;

	// Token: 0x04001714 RID: 5908
	public UIButton addFriendBtn;

	// Token: 0x04001715 RID: 5909
	private string roleName = string.Empty;

	// Token: 0x04001716 RID: 5910
	private GGNetworkPlayerProperties playerInfoData;

	// Token: 0x04001717 RID: 5911
	private bool isFriendValue;
}
