using System;
using UnityEngine;

// Token: 0x02000296 RID: 662
public class UIOtherPlayerInfoDirector : MonoBehaviour
{
	// Token: 0x06001312 RID: 4882 RVA: 0x000AB60B File Offset: 0x000A9A0B
	private void Awake()
	{
		if (UIOtherPlayerInfoDirector.mInstance == null)
		{
			UIOtherPlayerInfoDirector.mInstance = this;
		}
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x000AB623 File Offset: 0x000A9A23
	private void OnDestroy()
	{
		if (UIOtherPlayerInfoDirector.mInstance != null)
		{
			UIOtherPlayerInfoDirector.mInstance = null;
		}
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x000AB63B File Offset: 0x000A9A3B
	private void Start()
	{
		this.otherPlayerProfileNode.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x000AB661 File Offset: 0x000A9A61
	private void Update()
	{
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x000AB664 File Offset: 0x000A9A64
	public void CloseBtnPressed()
	{
		this.crownSprite.gameObject.SetActive(false);
		this.otherPlayerProfileNode.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
		this.otherPlayerProfileNode.SetActive(false);
		GGPlayerSeasonInfo seasonInfo = new GGPlayerSeasonInfo();
		this.RefreshOtherPlayerProfileUI(seasonInfo);
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x000AB6C0 File Offset: 0x000A9AC0
	public void ChatBtnPressed()
	{
		this.CloseBtnPressed();
		UIPauseDirector.mInstance.pauseNode.SetActive(false);
		UIChatSystemDirector.mInstance.chatNode.SetActive(true);
		UIChatSystemDirector.mInstance.otherPlayerPropertiesList = GGNetworkKit.mInstance.GetOtherPlayerPropertiesList();
		int count = UIChatSystemDirector.mInstance.otherPlayerPropertiesList.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.playerInfo.name == UIChatSystemDirector.mInstance.otherPlayerPropertiesList[i].name)
			{
				UIChatSystemDirector.mInstance.curPrivateChatIndex = i;
			}
		}
		UIChatSystemDirector.mInstance.chatToPrivateToggle.value = true;
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x000AB76E File Offset: 0x000A9B6E
	public void AddFriendBtnPressed()
	{
		this.addFriendBtn.gameObject.SetActive(false);
		UIPauseDirector.mInstance.SendAddRequest(this.playerInfo.name);
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x000AB798 File Offset: 0x000A9B98
	public void ShowOtherPlayerProfileNode(GGNetworkPlayerProperties playerInfoValue, bool isFriendValue)
	{
		this.isFriend = isFriendValue;
		this.playerInfo = playerInfoValue;
		this.otherPlayerProfileNode.SetActive(true);
		TweenScale.Begin(this.otherPlayerProfileNode, 0.1f, new Vector3(1f, 1f, 1f));
		if (playerInfoValue.name == UIUserDataController.GetDefaultRoleName())
		{
			this.addFriendBtn.gameObject.SetActive(false);
			this.chatBtn.gameObject.SetActive(false);
		}
		else
		{
			if (!this.addFriendBtn.gameObject.activeSelf)
			{
				this.addFriendBtn.gameObject.SetActive(true);
			}
			if (!this.chatBtn.gameObject.activeSelf)
			{
				this.chatBtn.gameObject.SetActive(true);
			}
			if (isFriendValue)
			{
				this.addFriendBtn.gameObject.SetActive(false);
			}
		}
		if (GGNetworkKit.mInstance.GetGamePlayModeType() == GGPlayModeType.Sport)
		{
			GGNetworkKit.mInstance.RequestPlayerSeasonInfo(playerInfoValue.id);
			this.seasonStatNode.SetActive(true);
		}
		else
		{
			this.seasonStatNode.SetActive(false);
		}
		this.rankLabel.text = "- " + this.playerInfo.rank.ToString() + " -";
		this.rankSprite.spriteName = "Rank_" + this.playerInfo.rank.ToString();
		this.nickNameLabel.text = this.playerInfo.name;
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x000AB938 File Offset: 0x000A9D38
	public void RefreshOtherPlayerProfileUI(GGPlayerSeasonInfo seasonInfo)
	{
		this.seasonScoreLabel.text = seasonInfo.Score.ToString();
		this.seasonRankLabel.text = ((seasonInfo.Rank <= 0) ? "- -" : seasonInfo.Rank.ToString());
		this.totalKillingLabel.text = seasonInfo.Killing.ToString();
		this.headshorLabel.text = seasonInfo.HeadShot.ToString();
		this.godLikeLabel.text = seasonInfo.GodLike.ToString();
		this.killingVictoryJoinLabel.text = seasonInfo.KillingVictory.ToString() + "/" + seasonInfo.KillingJoin.ToString();
		this.strongholdVictoryJoinLabel.text = seasonInfo.StrongholdVictory.ToString() + "/" + seasonInfo.StrongholdJoin.ToString();
		this.explosionVictoryJoinLabel.text = seasonInfo.ExplositionVictory.ToString() + "/" + seasonInfo.ExplositionJoin.ToString();
		this.killingVictoryRateLabel.text = seasonInfo.KillingVictoryRate.ToString() + "%";
		this.strongholdVictoryRateLabel.text = seasonInfo.StrongholdVictoryRate.ToString() + "%";
		this.explosionVictoryRateLabel.text = seasonInfo.ExplositionVictoryRate.ToString() + "%";
		this.killingMvpLabel.text = seasonInfo.KillingMVP.ToString();
		this.strongholdMvpLabel.text = seasonInfo.StrongholdMVP.ToString();
		this.explosionMvpLabel.text = seasonInfo.ExplositionMVP.ToString();
		switch (seasonInfo.Rank)
		{
		case 1:
			this.crownSprite.gameObject.SetActive(true);
			this.crownSprite.spriteName = "KillRank_1";
			break;
		case 2:
			this.crownSprite.gameObject.SetActive(true);
			this.crownSprite.spriteName = "KillRank_2";
			break;
		case 3:
			this.crownSprite.gameObject.SetActive(true);
			this.crownSprite.spriteName = "KillRank_3";
			break;
		default:
			this.crownSprite.gameObject.SetActive(false);
			break;
		}
	}

	// Token: 0x0400162D RID: 5677
	public static UIOtherPlayerInfoDirector mInstance;

	// Token: 0x0400162E RID: 5678
	public UISprite rankSprite;

	// Token: 0x0400162F RID: 5679
	public UILabel rankLabel;

	// Token: 0x04001630 RID: 5680
	public UILabel nickNameLabel;

	// Token: 0x04001631 RID: 5681
	public UILabel seasonScoreLabel;

	// Token: 0x04001632 RID: 5682
	public UILabel seasonRankLabel;

	// Token: 0x04001633 RID: 5683
	public UISprite crownSprite;

	// Token: 0x04001634 RID: 5684
	public UILabel totalKillingLabel;

	// Token: 0x04001635 RID: 5685
	public UILabel headshorLabel;

	// Token: 0x04001636 RID: 5686
	public UILabel godLikeLabel;

	// Token: 0x04001637 RID: 5687
	public UILabel killingVictoryJoinLabel;

	// Token: 0x04001638 RID: 5688
	public UILabel strongholdVictoryJoinLabel;

	// Token: 0x04001639 RID: 5689
	public UILabel explosionVictoryJoinLabel;

	// Token: 0x0400163A RID: 5690
	public UILabel killingVictoryRateLabel;

	// Token: 0x0400163B RID: 5691
	public UILabel strongholdVictoryRateLabel;

	// Token: 0x0400163C RID: 5692
	public UILabel explosionVictoryRateLabel;

	// Token: 0x0400163D RID: 5693
	public UILabel killingMvpLabel;

	// Token: 0x0400163E RID: 5694
	public UILabel strongholdMvpLabel;

	// Token: 0x0400163F RID: 5695
	public UILabel explosionMvpLabel;

	// Token: 0x04001640 RID: 5696
	public UIButton addFriendBtn;

	// Token: 0x04001641 RID: 5697
	public UIButton chatBtn;

	// Token: 0x04001642 RID: 5698
	public GameObject seasonStatNode;

	// Token: 0x04001643 RID: 5699
	public GameObject otherPlayerProfileNode;

	// Token: 0x04001644 RID: 5700
	private GGNetworkPlayerProperties playerInfo;

	// Token: 0x04001645 RID: 5701
	private bool isFriend;
}
