using System;
using UnityEngine;

// Token: 0x020002D5 RID: 725
public class UIRewardItemPrefab : MonoBehaviour
{
	// Token: 0x060015AF RID: 5551 RVA: 0x000B921F File Offset: 0x000B761F
	private void Start()
	{
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x000B9224 File Offset: 0x000B7624
	public void RefreshRewardInfo(RewardUnitInfo info, FightingStatisticsTag tag)
	{
		this.infoTag = tag;
		this.nameAndDescriptionLabel.text = info.rewardHonorName.ToUpper() + " (Lv." + info.rewardLvStr + ")";
		this.progressNumLabel.text = string.Concat(new object[]
		{
			"(",
			info.progressCurValue,
			"/",
			info.progressTargetValue,
			")"
		});
		this.progressFgSprite.fillAmount = (float)info.progressRate * 0.01f;
		this.rewardItemLogoTexture.mainTexture = (Resources.Load("UI/Images/SlotLogo/" + info.spriteName) as Texture);
		this.rewardItemNumLabel.text = info.sNum;
		this.rewardDescriptionLabel.text = info.rewardDescription.ToUpper();
		if (info.canGotReward)
		{
			this.rewardGetBtn.isEnabled = true;
		}
		else
		{
			this.rewardGetBtn.isEnabled = false;
		}
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x000B933C File Offset: 0x000B773C
	public void RewardGetBtnPressed()
	{
		GrowthManagerKit.ReceiveReward(this.infoTag);
		RewardUnitInfo rewardUnitInfo = GrowthManagerKit.GetRewardUnitInfo(this.infoTag);
		this.RefreshRewardInfo(rewardUnitInfo, this.infoTag);
	}

	// Token: 0x04001881 RID: 6273
	public UILabel nameAndDescriptionLabel;

	// Token: 0x04001882 RID: 6274
	public UILabel progressNumLabel;

	// Token: 0x04001883 RID: 6275
	public UISprite progressFgSprite;

	// Token: 0x04001884 RID: 6276
	public UIButton rewardGetBtn;

	// Token: 0x04001885 RID: 6277
	public UITexture rewardItemLogoTexture;

	// Token: 0x04001886 RID: 6278
	public UILabel rewardItemNumLabel;

	// Token: 0x04001887 RID: 6279
	public UILabel rewardDescriptionLabel;

	// Token: 0x04001888 RID: 6280
	private FightingStatisticsTag infoTag;
}
