using System;
using UnityEngine;

// Token: 0x020002B3 RID: 691
public class UIPlayerResultInfoListPrefab : MonoBehaviour
{
	// Token: 0x0600143A RID: 5178 RVA: 0x000B0A8B File Offset: 0x000AEE8B
	private void Start()
	{
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x000B0A8D File Offset: 0x000AEE8D
	private void Update()
	{
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x000B0A90 File Offset: 0x000AEE90
	public void SetInfoValue(int index, Color color, GGNetworkPlayerProperties playerInfo)
	{
		if (playerInfo.name == UIUserDataController.GetDefaultRoleName())
		{
			this.bgSprite.color = new Color(0f, 0.4392157f, 0.15686275f, 1f);
		}
		else
		{
			this.bgSprite.color = color;
		}
		this.rankLabel.text = "- " + playerInfo.rank.ToString() + " -";
		this.rankSprite.spriteName = "Rank_" + playerInfo.rank.ToString();
		this.nickNameLabel.text = playerInfo.name;
		switch (UIModeDirector.mInstance.modeType)
		{
		case GGModeType.TeamDeathMatch:
			this.infoItem4Label.text = playerInfo.killNum.ToString() + " / " + playerInfo.deadNum.ToString();
			this.infoItem3Label.text = "+" + playerInfo.coinAdd.ToString();
			this.infoItem2Label.text = "+" + playerInfo.expAdd.ToString();
			this.infoItem1Label.text = this.RateToString(playerInfo.rating);
			break;
		case GGModeType.StrongHold:
			this.infoItem4Label.text = string.Empty;
			this.infoItem3Label.text = string.Empty;
			this.infoItem2Label.text = playerInfo.StrongholdScore.ToString();
			this.infoItem1Label.text = this.RateToString(playerInfo.rating);
			break;
		case GGModeType.KillingCompetition:
			this.infoItem4Label.text = string.Empty;
			this.infoItem3Label.text = string.Empty;
			this.infoItem2Label.text = playerInfo.KillingCompetitionScore.ToString();
			this.infoItem1Label.text = this.RateToString(playerInfo.rating);
			break;
		case GGModeType.Explosion:
			this.infoItem4Label.text = string.Empty;
			this.infoItem3Label.text = string.Empty;
			this.infoItem2Label.text = playerInfo.ExplosionScore.ToString();
			this.infoItem1Label.text = this.RateToString(playerInfo.rating);
			break;
		case GGModeType.Mutation:
			this.infoItem4Label.text = playerInfo.MutationModeScore.ToString();
			this.infoItem3Label.text = "+" + playerInfo.coinAdd.ToString();
			this.infoItem2Label.text = "+" + playerInfo.expAdd.ToString();
			this.infoItem1Label.text = this.RateToString(playerInfo.rating);
			break;
		case GGModeType.KnifeCompetition:
			this.infoItem4Label.text = string.Empty;
			this.infoItem3Label.text = string.Empty;
			this.infoItem2Label.text = playerInfo.KnifeCompetitionScore.ToString();
			this.infoItem1Label.text = this.RateToString(playerInfo.rating);
			break;
		}
	}

	// Token: 0x0600143D RID: 5181 RVA: 0x000B0E30 File Offset: 0x000AF230
	private string RateToString(GrowthGameRatingTag ratingTag)
	{
		string result = string.Empty;
		switch (ratingTag)
		{
		case GrowthGameRatingTag.RatingTag_F:
			result = "F";
			break;
		case GrowthGameRatingTag.RatingTag_E:
			result = "E";
			break;
		case GrowthGameRatingTag.RatingTag_D:
			result = "D";
			break;
		case GrowthGameRatingTag.RatingTag_C:
			result = "C";
			break;
		case GrowthGameRatingTag.RatingTag_B:
			result = "B";
			break;
		case GrowthGameRatingTag.RatingTag_A:
			result = "A";
			break;
		case GrowthGameRatingTag.RatingTag_S:
			result = "S";
			break;
		}
		return result;
	}

	// Token: 0x04001718 RID: 5912
	public UILabel nickNameLabel;

	// Token: 0x04001719 RID: 5913
	public UILabel rankLabel;

	// Token: 0x0400171A RID: 5914
	public UILabel infoItem1Label;

	// Token: 0x0400171B RID: 5915
	public UILabel infoItem2Label;

	// Token: 0x0400171C RID: 5916
	public UILabel infoItem3Label;

	// Token: 0x0400171D RID: 5917
	public UILabel infoItem4Label;

	// Token: 0x0400171E RID: 5918
	public UISprite rankSprite;

	// Token: 0x0400171F RID: 5919
	public UISprite bgSprite;
}
