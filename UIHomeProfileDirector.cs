using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002CD RID: 717
public class UIHomeProfileDirector : MonoBehaviour
{
	// Token: 0x06001542 RID: 5442 RVA: 0x000B6492 File Offset: 0x000B4892
	private void Awake()
	{
		if (UIHomeProfileDirector.mInstance == null)
		{
			UIHomeProfileDirector.mInstance = this;
		}
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x000B64AA File Offset: 0x000B48AA
	private void OnDestroy()
	{
		if (UIHomeProfileDirector.mInstance != null)
		{
			UIHomeProfileDirector.mInstance = null;
		}
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x000B64C4 File Offset: 0x000B48C4
	private void Start()
	{
		this.careerNode.transform.localPosition = new Vector3(-1000f, 0f, 0f);
		this.seasonNode.transform.localPosition = new Vector3(1440f, 0f, 0f);
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x000B6519 File Offset: 0x000B4919
	private void Update()
	{
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x000B651C File Offset: 0x000B491C
	public void RefreshData()
	{
		CareerStatValuesAll careerStatValuesOfAll = GrowthManagerKit.GetCareerStatValuesOfAll();
		this.totalKillingLabel.text = careerStatValuesOfAll.totalKilling.ToString();
		this.headshotLabel.text = careerStatValuesOfAll.totalHeadshotKilling.ToString();
		this.godLikeLabel.text = careerStatValuesOfAll.totalGodLikeKilling.ToString();
		this.killingVictoryJoinLabel.text = careerStatValuesOfAll.totalKillingCompetitionModeVictoryCount.ToString() + "/" + careerStatValuesOfAll.totalKillingCompetitionModeJoinCount.ToString();
		this.strongholdVictoryJoinLabel.text = careerStatValuesOfAll.totalStrongholdModeVictoryCount.ToString() + "/" + careerStatValuesOfAll.totalStrongholdModeJoinCount.ToString();
		this.explosionVictoryJoinLabel.text = careerStatValuesOfAll.totalExplosionModeVictoryCount.ToString() + "/" + careerStatValuesOfAll.totalExplosionModeJoinCount.ToString();
		this.killingVictoryRateLabel.text = careerStatValuesOfAll.killingCompetitionModeVictoryRate.ToString() + "%";
		this.strongholdVictoryRateLabel.text = careerStatValuesOfAll.strongholdModeVictoryRate.ToString() + "%";
		this.explosionVictoryRateLabel.text = careerStatValuesOfAll.explosionModeVictoryRate.ToString() + "%";
		this.killingMVPLabel.text = careerStatValuesOfAll.totalKillingCompetitionModeMvpCount.ToString();
		this.strongholdMVPLabel.text = careerStatValuesOfAll.totalStrongholdModeMvpCount.ToString();
		this.explosionMVPLabel.text = careerStatValuesOfAll.totalExplosionModeMvpCount.ToString();
		CareerStatValuesSeason careerStatValuesOfSeason = GrowthManagerKit.GetCareerStatValuesOfSeason();
		this.totalKillingSeasonLabel.text = careerStatValuesOfSeason.totalKilling.ToString();
		this.headshotSeasonLabel.text = careerStatValuesOfSeason.totalHeadshotKilling.ToString();
		this.godLikeSeasonLabel.text = careerStatValuesOfSeason.totalGodLikeKilling.ToString();
		this.seasonScoreLabel.text = careerStatValuesOfSeason.seasonScore.ToString();
		this.seasonRankLabel.text = ((careerStatValuesOfSeason.seasonRank <= 0) ? "- -" : careerStatValuesOfSeason.seasonRank.ToString());
		this.killingVictoryJoinSeasonLabel.text = careerStatValuesOfSeason.totalKillingCompetitionModeVictoryCount.ToString() + "/" + careerStatValuesOfAll.totalKillingCompetitionModeJoinCount.ToString();
		this.strongholdVictoryJoinSeasonLabel.text = careerStatValuesOfSeason.totalStrongholdModeVictoryCount.ToString() + "/" + careerStatValuesOfAll.totalStrongholdModeJoinCount.ToString();
		this.explosionVictoryJoinSeasonLabel.text = careerStatValuesOfSeason.totalExplosionModeVictoryCount.ToString() + "/" + careerStatValuesOfAll.totalExplosionModeJoinCount.ToString();
		this.killingVictoryRateSeasonLabel.text = careerStatValuesOfSeason.killingCompetitionModeVictoryRate.ToString() + "%";
		this.strongholdVictoryRateSeasonLabel.text = careerStatValuesOfSeason.strongholdModeVictoryRate.ToString() + "%";
		this.explosionVictoryRateSeasonLabel.text = careerStatValuesOfSeason.explosionModeVictoryRate.ToString() + "%";
		this.killingMVPSeasonLabel.text = careerStatValuesOfSeason.totalKillingCompetitionModeMvpCount.ToString();
		this.strongholdMVPSeasonLabel.text = careerStatValuesOfSeason.totalStrongholdModeMvpCount.ToString();
		this.explosionMVPSeasonLabel.text = careerStatValuesOfSeason.totalExplosionModeMvpCount.ToString();
		switch (careerStatValuesOfSeason.seasonRank)
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

	// Token: 0x06001547 RID: 5447 RVA: 0x000B69AC File Offset: 0x000B4DAC
	public void BackBtnPressed()
	{
		TweenPosition.Begin(this.careerNode, 0.1f, new Vector3(-1000f, 0f, 0f));
		TweenPosition.Begin(this.seasonNode, 0.1f, new Vector3(1440f, 0f, 0f));
		base.StartCoroutine(this.HideProfileNode(0.1f));
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x000B6A18 File Offset: 0x000B4E18
	private IEnumerator HideProfileNode(float time)
	{
		yield return new WaitForSeconds(time);
		UIHomeDirector.mInstance.BackToRootNode(UIHomeDirector.mInstance.profileNode);
		yield break;
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x000B6A34 File Offset: 0x000B4E34
	public void ShowProfileNodeAnimaiton()
	{
		TweenPosition.Begin(this.careerNode, 0.1f, new Vector3(0f, 0f, 0f));
		TweenPosition.Begin(this.seasonNode, 0.1f, new Vector3(440f, 0f, 0f));
	}

	// Token: 0x040017F4 RID: 6132
	public static UIHomeProfileDirector mInstance;

	// Token: 0x040017F5 RID: 6133
	public GameObject careerNode;

	// Token: 0x040017F6 RID: 6134
	public GameObject seasonNode;

	// Token: 0x040017F7 RID: 6135
	public UILabel totalKillingLabel;

	// Token: 0x040017F8 RID: 6136
	public UILabel headshotLabel;

	// Token: 0x040017F9 RID: 6137
	public UILabel godLikeLabel;

	// Token: 0x040017FA RID: 6138
	public UILabel killingVictoryJoinLabel;

	// Token: 0x040017FB RID: 6139
	public UILabel strongholdVictoryJoinLabel;

	// Token: 0x040017FC RID: 6140
	public UILabel explosionVictoryJoinLabel;

	// Token: 0x040017FD RID: 6141
	public UILabel killingVictoryRateLabel;

	// Token: 0x040017FE RID: 6142
	public UILabel strongholdVictoryRateLabel;

	// Token: 0x040017FF RID: 6143
	public UILabel explosionVictoryRateLabel;

	// Token: 0x04001800 RID: 6144
	public UILabel killingMVPLabel;

	// Token: 0x04001801 RID: 6145
	public UILabel strongholdMVPLabel;

	// Token: 0x04001802 RID: 6146
	public UILabel explosionMVPLabel;

	// Token: 0x04001803 RID: 6147
	public UILabel totalKillingSeasonLabel;

	// Token: 0x04001804 RID: 6148
	public UILabel headshotSeasonLabel;

	// Token: 0x04001805 RID: 6149
	public UILabel godLikeSeasonLabel;

	// Token: 0x04001806 RID: 6150
	public UILabel seasonScoreLabel;

	// Token: 0x04001807 RID: 6151
	public UILabel seasonRankLabel;

	// Token: 0x04001808 RID: 6152
	public UISprite crownSprite;

	// Token: 0x04001809 RID: 6153
	public UILabel killingVictoryJoinSeasonLabel;

	// Token: 0x0400180A RID: 6154
	public UILabel strongholdVictoryJoinSeasonLabel;

	// Token: 0x0400180B RID: 6155
	public UILabel explosionVictoryJoinSeasonLabel;

	// Token: 0x0400180C RID: 6156
	public UILabel killingVictoryRateSeasonLabel;

	// Token: 0x0400180D RID: 6157
	public UILabel strongholdVictoryRateSeasonLabel;

	// Token: 0x0400180E RID: 6158
	public UILabel explosionVictoryRateSeasonLabel;

	// Token: 0x0400180F RID: 6159
	public UILabel killingMVPSeasonLabel;

	// Token: 0x04001810 RID: 6160
	public UILabel strongholdMVPSeasonLabel;

	// Token: 0x04001811 RID: 6161
	public UILabel explosionMVPSeasonLabel;
}
