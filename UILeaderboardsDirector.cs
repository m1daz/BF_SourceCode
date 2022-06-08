using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CF RID: 719
public class UILeaderboardsDirector : MonoBehaviour
{
	// Token: 0x06001551 RID: 5457 RVA: 0x000B6EB0 File Offset: 0x000B52B0
	private void Awake()
	{
		if (UILeaderboardsDirector.mInstance == null)
		{
			UILeaderboardsDirector.mInstance = this;
		}
	}

	// Token: 0x06001552 RID: 5458 RVA: 0x000B6EC8 File Offset: 0x000B52C8
	private void OnDestroy()
	{
		if (UILeaderboardsDirector.mInstance != null)
		{
			UILeaderboardsDirector.mInstance = null;
		}
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x000B6EE0 File Offset: 0x000B52E0
	private void Start()
	{
		this.selfRoleName = UIUserDataController.GetDefaultRoleName();
	}

	// Token: 0x06001554 RID: 5460 RVA: 0x000B6EF0 File Offset: 0x000B52F0
	private void Update()
	{
		if (this.needRefresh)
		{
			this.readTime += Time.deltaTime;
			if (this.readTime > this.readCycle)
			{
				this.readTime = 0f;
				this.rankLeaderboardInfoList = GGCloudServiceKit.mInstance.mExpLeaderBoardInfoList;
				if (this.rankLeaderboardInfoList.Count > 0)
				{
					this.needRefresh = false;
					UITipController.mInstance.HideTip(UITipController.TipType.LoadingTip);
					this.InitRankScrollView();
				}
			}
		}
		if (this.needRefreshSeasonScore)
		{
			this.readTimeSeasonScore += Time.deltaTime;
			if (this.readTimeSeasonScore > this.readCycle)
			{
				this.readTimeSeasonScore = 0f;
				this.seasonScoreLeaderboardInfoList = GGCloudServiceKit.mInstance.mSeasonScoreBoardInfoList;
				if (this.seasonScoreLeaderboardInfoList.Count > 0)
				{
					this.needRefreshSeasonScore = false;
					this.SeasonToggleValueChanged();
				}
			}
		}
		this.rankLeaderboardInfoList = GGCloudServiceKit.mInstance.mExpLeaderBoardInfoList;
		this.totalKillLeaderboardInfoList = GGCloudServiceKit.mInstance.mTotalKillNumLeaderBoardInfoList;
		this.seasonScoreLeaderboardInfoList = GGCloudServiceKit.mInstance.mSeasonScoreBoardInfoList;
	}

	// Token: 0x06001555 RID: 5461 RVA: 0x000B7008 File Offset: 0x000B5408
	private void InitRankScrollView()
	{
		this.rankLeaderboardInfoList = GGCloudServiceKit.mInstance.mExpLeaderBoardInfoList;
		this.totalKillLeaderboardInfoList = GGCloudServiceKit.mInstance.mTotalKillNumLeaderBoardInfoList;
		this.seasonScoreLeaderboardInfoList = GGCloudServiceKit.mInstance.mSeasonScoreBoardInfoList;
		int count = this.rankLeaderboardInfoList.Count;
		if (count > 0)
		{
			this.needRefresh = false;
			for (int i = 0; i < count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.leaderboardPrefab);
				gameObject.transform.parent = this.rankListGrid.transform;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.transform.localPosition = new Vector3(0f, (float)(-64 * i), 1f);
				gameObject.GetComponent<UILeaderboardItemPrefab>().RefreshRankList(this.rankLeaderboardInfoList[i], i);
				if (this.rankLeaderboardInfoList[i].RoleName == this.selfRoleName)
				{
					this.selfRank = i + 1;
				}
				this.newRankObjList.Add(gameObject);
			}
			this.RefreshSelfInfo(this.selfRank);
		}
	}

	// Token: 0x06001556 RID: 5462 RVA: 0x000B712C File Offset: 0x000B552C
	private void RefreshToRankList()
	{
		if (this.newRankObjList.Count > 0)
		{
			int count = this.rankLeaderboardInfoList.Count;
			for (int i = 0; i < count; i++)
			{
				this.newRankObjList[i].GetComponent<UILeaderboardItemPrefab>().RefreshRankList(this.rankLeaderboardInfoList[i], i);
				if (this.rankLeaderboardInfoList[i].RoleName == this.selfRoleName)
				{
					this.selfRank = i + 1;
				}
			}
			this.RefreshSelfInfo(this.selfRank);
		}
	}

	// Token: 0x06001557 RID: 5463 RVA: 0x000B71C4 File Offset: 0x000B55C4
	private void RefreshToTotalKillList()
	{
		int count = this.totalKillLeaderboardInfoList.Count;
		for (int i = 0; i < count; i++)
		{
			this.newRankObjList[i].GetComponent<UILeaderboardItemPrefab>().RefreshTotalKillList(this.totalKillLeaderboardInfoList[i], i);
			if (this.totalKillLeaderboardInfoList[i].RoleName == this.selfRoleName)
			{
				this.selfTotalKillRank = i + 1;
			}
		}
		this.RefreshSelfInfo(this.selfTotalKillRank);
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x000B7248 File Offset: 0x000B5648
	private void RefreshToSeasonScoreList()
	{
		int count = this.seasonScoreLeaderboardInfoList.Count;
		for (int i = 0; i < count; i++)
		{
			this.newSeasonScoreObjList[i].GetComponent<UILeaderboardItemPrefab>().RefreshSeasonScoreList(this.seasonScoreLeaderboardInfoList[i], i);
			if (this.seasonScoreLeaderboardInfoList[i].RoleName == this.selfRoleName)
			{
				this.selfSeasonScoreRank = i + 1;
			}
		}
		if (this.selfSeasonScoreRank <= 0)
		{
			this.selfSeasonScoreRank = GGCloudServiceKit.mInstance.MySeasonScoreRank;
		}
		this.RefreshSelfInfo(this.selfSeasonScoreRank);
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x000B72E8 File Offset: 0x000B56E8
	private void RefreshSelfInfo(int selfRankValue)
	{
		this.nickNameLabel.text = this.selfRoleName;
		int characterLevel = GrowthManagerKit.GetCharacterLevel();
		this.rankSprite.spriteName = "Rank_" + characterLevel;
		if (selfRankValue == 0)
		{
			this.indexLabel.text = "- -";
		}
		else
		{
			this.indexLabel.text = selfRankValue.ToString() + ".";
		}
		switch (selfRankValue)
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
		switch (this.listType)
		{
		case UILeaderboardsDirector.LeaderboardListType.Rank:
			this.valueLabel.text = characterLevel.ToString();
			break;
		case UILeaderboardsDirector.LeaderboardListType.TotalKill:
			this.valueLabel.text = GrowthManagerKit.GetFightingStatisticsValue(FightingStatisticsTag.tTotalKillInWorldwideMultiplayer).ToString();
			break;
		case UILeaderboardsDirector.LeaderboardListType.SeasonScore:
			this.valueLabel.text = ((this.selfSeasonScoreRank <= 0 || this.selfSeasonScoreRank > this.seasonScoreLeaderboardInfoList.Count) ? GrowthManagerKit.GetSeasonScore().ToString() : this.seasonScoreLeaderboardInfoList[this.selfSeasonScoreRank - 1].Score.ToString());
			this.indexLabel.text = ((this.selfSeasonScoreRank <= 0) ? "- -" : (this.selfSeasonScoreRank.ToString() + "."));
			break;
		default:
			this.valueLabel.text = characterLevel.ToString();
			this.indexLabel.text = "- -";
			break;
		}
	}

	// Token: 0x0600155A RID: 5466 RVA: 0x000B7534 File Offset: 0x000B5934
	public void BackBtnPressed()
	{
		UIHomeDirector.mInstance.BackToRootNode(UIHomeDirector.mInstance.leaderboardNode);
	}

	// Token: 0x0600155B RID: 5467 RVA: 0x000B754C File Offset: 0x000B594C
	public void RankToggleValueChanged()
	{
		if (this.rankToggle.value)
		{
			this.rankListScrollView.gameObject.SetActive(true);
			this.seasonScoreListScrollView.gameObject.SetActive(false);
			this.rankListScrollView.ResetPosition();
			this.listType = UILeaderboardsDirector.LeaderboardListType.Rank;
			this.RefreshToRankList();
			this.needRefreshSeasonScore = false;
		}
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x000B75AC File Offset: 0x000B59AC
	public void TotalKillToggleValueChanged()
	{
		if (this.totalKillToggle.value)
		{
			this.rankListScrollView.gameObject.SetActive(true);
			this.seasonScoreListScrollView.gameObject.SetActive(false);
			this.rankListScrollView.ResetPosition();
			this.listType = UILeaderboardsDirector.LeaderboardListType.TotalKill;
			this.RefreshToTotalKillList();
			this.needRefreshSeasonScore = false;
		}
	}

	// Token: 0x0600155D RID: 5469 RVA: 0x000B760C File Offset: 0x000B5A0C
	public void SeasonToggleValueChanged()
	{
		if (this.seasonScoreToggle.value)
		{
			this.rankListScrollView.gameObject.SetActive(false);
			this.seasonScoreListScrollView.gameObject.SetActive(true);
			this.listType = UILeaderboardsDirector.LeaderboardListType.SeasonScore;
			if (this.seasonScoreTipLabel.text == string.Empty)
			{
				this.seasonScoreTipLabel.text = GGCloudServiceKit.mInstance.mSeasonExtraInfo.SeasonTip;
			}
			if (this.newSeasonScoreObjList.Count > 0)
			{
				this.seasonScoreListScrollView.ResetPosition();
				this.RefreshToSeasonScoreList();
			}
			else
			{
				this.seasonScoreLeaderboardInfoList = GGCloudServiceKit.mInstance.mSeasonScoreBoardInfoList;
				int count = this.seasonScoreLeaderboardInfoList.Count;
				if (count > 0)
				{
					this.needRefreshSeasonScore = false;
					this.newSeasonScoreObjList.Clear();
					for (int i = 0; i < count; i++)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.leaderboardPrefab);
						gameObject.transform.parent = this.seasonScoreListGrid.transform;
						gameObject.transform.localPosition = new Vector3(0f, -64f - (float)(64 * i), 1f);
						gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
						gameObject.GetComponent<UILeaderboardItemPrefab>().RefreshSeasonScoreList(this.seasonScoreLeaderboardInfoList[i], i);
						if (this.seasonScoreLeaderboardInfoList[i].RoleName == this.selfRoleName)
						{
							this.selfSeasonScoreRank = i + 1;
						}
						this.newSeasonScoreObjList.Add(gameObject);
					}
					if (this.selfSeasonScoreRank <= 0)
					{
						this.selfSeasonScoreRank = GGCloudServiceKit.mInstance.MySeasonScoreRank;
					}
					this.RefreshSelfInfo(this.selfSeasonScoreRank);
				}
				else
				{
					this.needRefreshSeasonScore = true;
				}
			}
		}
	}

	// Token: 0x04001817 RID: 6167
	public static UILeaderboardsDirector mInstance;

	// Token: 0x04001818 RID: 6168
	public UIScrollView rankListScrollView;

	// Token: 0x04001819 RID: 6169
	public GameObject rankListGrid;

	// Token: 0x0400181A RID: 6170
	public UIScrollView seasonScoreListScrollView;

	// Token: 0x0400181B RID: 6171
	public GameObject seasonScoreListGrid;

	// Token: 0x0400181C RID: 6172
	public UILabel nickNameLabel;

	// Token: 0x0400181D RID: 6173
	public UILabel valueLabel;

	// Token: 0x0400181E RID: 6174
	public UILabel indexLabel;

	// Token: 0x0400181F RID: 6175
	public UISprite rankSprite;

	// Token: 0x04001820 RID: 6176
	public UISprite crownSprite;

	// Token: 0x04001821 RID: 6177
	public UILabel seasonScoreTipLabel;

	// Token: 0x04001822 RID: 6178
	private UILeaderboardsDirector.LeaderboardListType listType;

	// Token: 0x04001823 RID: 6179
	private List<CSLeaderBoardInfo> rankLeaderboardInfoList = new List<CSLeaderBoardInfo>();

	// Token: 0x04001824 RID: 6180
	private List<CSLeaderBoardInfo> totalKillLeaderboardInfoList = new List<CSLeaderBoardInfo>();

	// Token: 0x04001825 RID: 6181
	private List<CSSeasonScoreBoardInfo> seasonScoreLeaderboardInfoList = new List<CSSeasonScoreBoardInfo>();

	// Token: 0x04001826 RID: 6182
	public GameObject leaderboardPrefab;

	// Token: 0x04001827 RID: 6183
	private List<GameObject> newRankObjList = new List<GameObject>();

	// Token: 0x04001828 RID: 6184
	private List<GameObject> newSeasonScoreObjList = new List<GameObject>();

	// Token: 0x04001829 RID: 6185
	private int selfRank;

	// Token: 0x0400182A RID: 6186
	private int selfTotalKillRank;

	// Token: 0x0400182B RID: 6187
	private int selfSeasonScoreRank;

	// Token: 0x0400182C RID: 6188
	private string selfRoleName;

	// Token: 0x0400182D RID: 6189
	private float readCycle = 1f;

	// Token: 0x0400182E RID: 6190
	private float readTime;

	// Token: 0x0400182F RID: 6191
	public bool needRefresh = true;

	// Token: 0x04001830 RID: 6192
	private float readTimeSeasonScore;

	// Token: 0x04001831 RID: 6193
	public bool needRefreshSeasonScore;

	// Token: 0x04001832 RID: 6194
	public UIToggle rankToggle;

	// Token: 0x04001833 RID: 6195
	public UIToggle totalKillToggle;

	// Token: 0x04001834 RID: 6196
	public UIToggle seasonScoreToggle;

	// Token: 0x020002D0 RID: 720
	public enum LeaderboardListType
	{
		// Token: 0x04001836 RID: 6198
		Rank,
		// Token: 0x04001837 RID: 6199
		TotalKill,
		// Token: 0x04001838 RID: 6200
		SeasonScore
	}
}
