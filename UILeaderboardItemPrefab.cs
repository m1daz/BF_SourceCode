using System;
using UnityEngine;

// Token: 0x020002CE RID: 718
public class UILeaderboardItemPrefab : MonoBehaviour
{
	// Token: 0x0600154B RID: 5451 RVA: 0x000B6B34 File Offset: 0x000B4F34
	private void Start()
	{
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x000B6B36 File Offset: 0x000B4F36
	private void Update()
	{
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x000B6B38 File Offset: 0x000B4F38
	public void RefreshRankList(CSLeaderBoardInfo info, int index)
	{
		this.nickNameLabel.text = info.RoleName;
		this.indexLabel.text = info.rank + ".";
		this.rankSprite.spriteName = "Rank_" + info.Level;
		this.valueLabel.text = info.Level;
		switch (index)
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

	// Token: 0x0600154E RID: 5454 RVA: 0x000B6C3C File Offset: 0x000B503C
	public void RefreshTotalKillList(CSLeaderBoardInfo info, int index)
	{
		this.nickNameLabel.text = info.RoleName;
		this.indexLabel.text = info.rank + ".";
		this.rankSprite.spriteName = "Rank_" + info.Level;
		this.valueLabel.text = info.TotalKillNum.ToString();
		switch (index)
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

	// Token: 0x0600154F RID: 5455 RVA: 0x000B6D48 File Offset: 0x000B5148
	public void RefreshSeasonScoreList(CSSeasonScoreBoardInfo info, int index)
	{
		this.nickNameLabel.text = info.RoleName;
		this.indexLabel.text = info.ScoreRank + ".";
		this.rankSprite.spriteName = "Rank_" + info.Level;
		this.valueLabel.text = info.Score.ToString();
		switch (index)
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

	// Token: 0x04001812 RID: 6162
	public UILabel nickNameLabel;

	// Token: 0x04001813 RID: 6163
	public UILabel valueLabel;

	// Token: 0x04001814 RID: 6164
	public UILabel indexLabel;

	// Token: 0x04001815 RID: 6165
	public UISprite rankSprite;

	// Token: 0x04001816 RID: 6166
	public UISprite crownSprite;
}
