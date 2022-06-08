using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class UIRewardNewDirector : MonoBehaviour
{
	// Token: 0x060015B3 RID: 5555 RVA: 0x000B93C5 File Offset: 0x000B77C5
	private void Awake()
	{
		if (UIRewardNewDirector.mInstance == null)
		{
			UIRewardNewDirector.mInstance = this;
		}
	}

	// Token: 0x060015B4 RID: 5556 RVA: 0x000B93DD File Offset: 0x000B77DD
	private void OnDestroy()
	{
		if (UIRewardNewDirector.mInstance != null)
		{
			UIRewardNewDirector.mInstance = null;
		}
	}

	// Token: 0x060015B5 RID: 5557 RVA: 0x000B93F5 File Offset: 0x000B77F5
	private void Start()
	{
		this.InitRewardNodeData();
	}

	// Token: 0x060015B6 RID: 5558 RVA: 0x000B93FD File Offset: 0x000B77FD
	private void Update()
	{
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x000B9400 File Offset: 0x000B7800
	private void InitRewardNodeData()
	{
		this.dailyRewardTypeList = GrowthManagerKit.GetDailyRewardTypeList();
		if (this.dailyRewardTypeList.Count > 0)
		{
			for (int i = 0; i < this.dailyRewardTypeList.Count; i++)
			{
				this.dailyRewardInfoList.Add(GrowthManagerKit.GetRewardUnitInfo(this.dailyRewardTypeList[i]));
			}
		}
		this.generalRewardTypeList = GrowthManagerKit.GetGeneralRewardTypeList();
		if (this.generalRewardTypeList.Count > 0)
		{
			for (int j = 0; j < this.generalRewardTypeList.Count; j++)
			{
				this.generalRewardInfoList.Add(GrowthManagerKit.GetRewardUnitInfo(this.generalRewardTypeList[j]));
			}
		}
		this.InitRewardScrollView();
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x000B94BC File Offset: 0x000B78BC
	private void InitRewardScrollView()
	{
		int count = this.dailyRewardInfoList.Count;
		int count2 = this.generalRewardInfoList.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.rewardObjPrefab);
				gameObject.transform.parent = this.dailyRewardGrid.transform;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.GetComponent<UIRewardItemPrefab>().RefreshRewardInfo(this.dailyRewardInfoList[i], this.dailyRewardTypeList[i]);
				this.newDailyRewardList.Add(gameObject);
			}
		}
		if (count2 > 0)
		{
			for (int j = 0; j < count2; j++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.rewardObjPrefab);
				gameObject2.transform.parent = this.generalRewardGrid.transform;
				gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject2.GetComponent<UIRewardItemPrefab>().RefreshRewardInfo(this.generalRewardInfoList[j], this.generalRewardTypeList[j]);
				this.newGeneralRewardList.Add(gameObject2);
			}
		}
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x000B95FE File Offset: 0x000B79FE
	public void BackBtnPressed()
	{
		UIHomeDirector.mInstance.BackToRootNode(UIHomeDirector.mInstance.rewardNode);
		UIHomeDirector.mInstance.RefreshNewRewardSprite();
	}

	// Token: 0x060015BA RID: 5562 RVA: 0x000B961E File Offset: 0x000B7A1E
	public void RateUsPressed()
	{
		UIUserDataController.VerifyAppstoreRating();
	}

	// Token: 0x060015BB RID: 5563 RVA: 0x000B9625 File Offset: 0x000B7A25
	public void DailyToggleValueChanged()
	{
		if (this.dailyToggle.value)
		{
			this.dailyRewardScrollView.gameObject.SetActive(true);
			this.generalRewardScrollView.gameObject.SetActive(false);
		}
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x000B9659 File Offset: 0x000B7A59
	public void GeneralToggleValueChanged()
	{
		if (this.generalToggle.value)
		{
			this.dailyRewardScrollView.gameObject.SetActive(false);
			this.generalRewardScrollView.gameObject.SetActive(true);
		}
	}

	// Token: 0x060015BD RID: 5565 RVA: 0x000B9690 File Offset: 0x000B7A90
	public void RefershDailyRewardScrollVeiw()
	{
		this.dailyRewardInfoList.Clear();
		this.dailyRewardTypeList = GrowthManagerKit.GetDailyRewardTypeList();
		if (this.dailyRewardTypeList.Count > 0)
		{
			for (int i = 0; i < this.dailyRewardTypeList.Count; i++)
			{
				this.dailyRewardInfoList.Add(GrowthManagerKit.GetRewardUnitInfo(this.dailyRewardTypeList[i]));
			}
			int count = this.dailyRewardInfoList.Count;
			if (count > 0)
			{
				for (int j = 0; j < this.newDailyRewardList.Count; j++)
				{
					this.newDailyRewardList[j].GetComponent<UIRewardItemPrefab>().RefreshRewardInfo(this.dailyRewardInfoList[j], this.dailyRewardTypeList[j]);
				}
			}
		}
	}

	// Token: 0x04001889 RID: 6281
	public static UIRewardNewDirector mInstance;

	// Token: 0x0400188A RID: 6282
	private List<FightingStatisticsTag> dailyRewardTypeList = new List<FightingStatisticsTag>();

	// Token: 0x0400188B RID: 6283
	private List<FightingStatisticsTag> generalRewardTypeList = new List<FightingStatisticsTag>();

	// Token: 0x0400188C RID: 6284
	private List<RewardUnitInfo> dailyRewardInfoList = new List<RewardUnitInfo>();

	// Token: 0x0400188D RID: 6285
	private List<RewardUnitInfo> generalRewardInfoList = new List<RewardUnitInfo>();

	// Token: 0x0400188E RID: 6286
	private List<GameObject> newDailyRewardList = new List<GameObject>();

	// Token: 0x0400188F RID: 6287
	private List<GameObject> newGeneralRewardList = new List<GameObject>();

	// Token: 0x04001890 RID: 6288
	public GameObject rewardObjPrefab;

	// Token: 0x04001891 RID: 6289
	public UIScrollView dailyRewardScrollView;

	// Token: 0x04001892 RID: 6290
	public UIScrollView generalRewardScrollView;

	// Token: 0x04001893 RID: 6291
	public GameObject dailyRewardGrid;

	// Token: 0x04001894 RID: 6292
	public GameObject generalRewardGrid;

	// Token: 0x04001895 RID: 6293
	public UIToggle dailyToggle;

	// Token: 0x04001896 RID: 6294
	public UIToggle generalToggle;
}
