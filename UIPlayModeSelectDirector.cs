using System;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class UIPlayModeSelectDirector : MonoBehaviour
{
	// Token: 0x06001656 RID: 5718 RVA: 0x000BFE7E File Offset: 0x000BE27E
	private void Awake()
	{
		UIPlayModeSelectDirector.mInstance = this;
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x000BFE86 File Offset: 0x000BE286
	private void Start()
	{
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x000BFE88 File Offset: 0x000BE288
	private void InitPlayModeButton()
	{
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x000BFE8A File Offset: 0x000BE28A
	private void Update()
	{
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x000BFE8C File Offset: 0x000BE28C
	public void SportMode()
	{
		UIPlayModeSelectDirector.mPlayModeType = GGPlayModeType.Sport;
		this.mPlayModeSelectGO.SetActive(false);
		this.mEntertainmentGO.SetActive(false);
		this.mSportGO.SetActive(true);
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x000BFEB8 File Offset: 0x000BE2B8
	public void EntertainmentMode()
	{
		UIPlayModeSelectDirector.mPlayModeType = GGPlayModeType.Entertainment;
		this.mPlayModeSelectGO.SetActive(false);
		this.mSportGO.SetActive(false);
		this.mEntertainmentGO.SetActive(true);
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x000BFEE4 File Offset: 0x000BE2E4
	public void BackToMainMenu()
	{
		UISceneManager.mInstance.LoadLevel("MainMenu");
	}

	// Token: 0x0600165D RID: 5725 RVA: 0x000BFEF5 File Offset: 0x000BE2F5
	public void BackToPlayModeSelect()
	{
		this.mSportGO.SetActive(false);
		this.mEntertainmentGO.SetActive(false);
		this.mPlayModeSelectGO.SetActive(true);
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x000BFF1B File Offset: 0x000BE31B
	public void PotionInstruction()
	{
		this.mPotionInstructionGO.SetActive(true);
		this.mPlayModeSelectGO.SetActive(false);
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x000BFF35 File Offset: 0x000BE335
	public void BackToPlayMode()
	{
		this.mPotionInstructionGO.SetActive(false);
		this.mPlayModeSelectGO.SetActive(true);
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x000BFF4F File Offset: 0x000BE34F
	private void OnDestroy()
	{
		if (UIPlayModeSelectDirector.mInstance != null)
		{
			UIPlayModeSelectDirector.mInstance = null;
		}
	}

	// Token: 0x04001932 RID: 6450
	public static UIPlayModeSelectDirector mInstance;

	// Token: 0x04001933 RID: 6451
	public GameObject mPlayModeSelectGO;

	// Token: 0x04001934 RID: 6452
	public GameObject mSportGO;

	// Token: 0x04001935 RID: 6453
	public GameObject mEntertainmentGO;

	// Token: 0x04001936 RID: 6454
	public GameObject mPotionInstructionGO;

	// Token: 0x04001937 RID: 6455
	public static GGPlayModeType mPlayModeType;
}
