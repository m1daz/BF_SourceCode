using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002C9 RID: 713
public class UICasinoDirector : MonoBehaviour
{
	// Token: 0x060014D7 RID: 5335 RVA: 0x000B28D4 File Offset: 0x000B0CD4
	private void Awake()
	{
		if (UICasinoDirector.mInstance == null)
		{
			UICasinoDirector.mInstance = this;
		}
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x000B28EC File Offset: 0x000B0CEC
	private void OnDestroy()
	{
		if (UICasinoDirector.mInstance != null)
		{
			UICasinoDirector.mInstance = null;
		}
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x000B2904 File Offset: 0x000B0D04
	private void Start()
	{
		this.defaultLogo = (Resources.Load("UI/Images/SlotLogo/Default_SlotLogo") as Texture);
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x000B291B File Offset: 0x000B0D1B
	private void Update()
	{
		if (UIHomeDirector.mInstance.casinoNode.activeSelf)
		{
			this.SlotCursorTimer();
		}
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x000B2938 File Offset: 0x000B0D38
	public void BackBtnPressed()
	{
		this.restTimes = 0;
		this.isSloting = false;
		this.casinoCanRefreshCoinbar = true;
		this.curCursorIndex = 0;
		this.cursorDeltaTime = 0f;
		this.cursorLevelDeltaTime = 0f;
		this.realCycleNums = 0;
		this.curCycleIndex = 1;
		for (int i = 0; i < UICasinoDirector.giftBoxSlotItemNum; i++)
		{
			this.giftBoxSlotItemLogo[i].mainTexture = this.defaultLogo;
			this.giftBoxSlotItemNumLabel[i].text = string.Empty;
			this.giftBoxSlotCursor[i].gameObject.SetActive(false);
			this.cursorLighted[i] = false;
		}
		this.resultLogoSprite.gameObject.SetActive(false);
		this.resultNumLabel.gameObject.SetActive(false);
		this.spinOnceBtn.isEnabled = true;
		this.spinTenBtn.isEnabled = true;
		this.luckyListGrid.SetActive(false);
		if (UIUserDataController.GetMusic() == 1)
		{
			this.casinoBgMusic.Stop();
			this.mainMenuBgMusic.Play();
		}
		UIHomeDirector.mInstance.BackToRootNode(UIHomeDirector.mInstance.casinoNode);
		UIHomeDirector.mInstance.UpdateGiftTips();
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x000B2A61 File Offset: 0x000B0E61
	public void GetLuckyListData()
	{
		GGCloudServiceKit.mInstance.GetSlotTopPrize(20);
		this.loadingSprite.SetActive(true);
		if (UIUserDataController.GetMusic() == 1)
		{
			this.mainMenuBgMusic.Stop();
			this.casinoBgMusic.Play();
		}
	}

	// Token: 0x060014DD RID: 5341 RVA: 0x000B2A9C File Offset: 0x000B0E9C
	public void ShowLuckyList(List<CSSlotTopPrizeInfo> luckyInfoList)
	{
		if (this.luckyListItemList == null || this.luckyListItemList.Count == 0)
		{
			if (luckyInfoList != null)
			{
				for (int i = 0; i < luckyInfoList.Count; i++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.luckyListItemPrefab);
					gameObject.transform.parent = this.luckyListGrid.transform;
					gameObject.transform.position = new Vector3(396f, 0f, 0f);
					gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
					gameObject.GetComponent<UILuckyListItemPrefab>().ReadData(luckyInfoList[i]);
					this.luckyListItemList.Add(gameObject);
				}
				this.loadingSprite.SetActive(false);
				this.luckyListGrid.SetActive(true);
			}
		}
		else if (luckyInfoList != null)
		{
			for (int j = 0; j < luckyInfoList.Count; j++)
			{
				this.luckyListItemList[j].SetActive(true);
				this.luckyListItemList[j].GetComponent<UILuckyListItemPrefab>().ReadData(luckyInfoList[j]);
			}
			this.loadingSprite.SetActive(false);
			this.luckyListGrid.SetActive(true);
		}
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x000B2BE0 File Offset: 0x000B0FE0
	public void SpinOnceBtnPressed()
	{
		if (GrowthManagerKit.GetCurGiftBoxTotal() > 0)
		{
			this.restTimes = 1;
			this.PrepareForStart();
		}
		else
		{
			EventDelegate btnEventName = new EventDelegate(this, "TipOkBtnPressed");
			UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "You need get enough giftbox!", Color.white, "OK", string.Empty, btnEventName, null, null);
		}
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x000B2C38 File Offset: 0x000B1038
	public void SpinTenBtnPressed()
	{
		if (GrowthManagerKit.GetCurGiftBoxTotal() > 9)
		{
			this.restTimes = 10;
			this.PrepareForStart();
		}
		else
		{
			EventDelegate btnEventName = new EventDelegate(this, "TipOkBtnPressed");
			UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "You need get enough giftbox!", Color.white, "OK", string.Empty, btnEventName, null, null);
		}
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x000B2C94 File Offset: 0x000B1094
	public void PurchaseGiftboxThree()
	{
		if (GrowthManagerKit.GetGems() >= 15)
		{
			GrowthManagerKit.SubGems(15);
			GrowthManagerKit.AddGiftBox(3);
		}
		else
		{
			EventDelegate btnEventName = new EventDelegate(this, "TipOkBtnPressed");
			UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "You need get enough gems!", Color.white, "OK", string.Empty, btnEventName, null, null);
		}
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x000B2CF0 File Offset: 0x000B10F0
	public void PurchaseGiftboxTen()
	{
		if (GrowthManagerKit.GetGems() >= 45)
		{
			GrowthManagerKit.SubGems(45);
			GrowthManagerKit.AddGiftBox(10);
		}
		else
		{
			EventDelegate btnEventName = new EventDelegate(this, "TipOkBtnPressed");
			UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, "You need get enough gems!", Color.white, "OK", string.Empty, btnEventName, null, null);
		}
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x000B2D4C File Offset: 0x000B114C
	private void PrepareForStart()
	{
		this.curCursorIndex = 0;
		this.spinOnceBtn.isEnabled = false;
		this.spinTenBtn.isEnabled = false;
		this.giftBoxSlotCursor[this.giftBoxResultIndex].gameObject.SetActive(false);
		this.resultNumLabel.gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		this.resultLogoSprite.gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		this.resultNumLabel.gameObject.GetComponent<TweenScale>().from = new Vector3(0.01f, 0.01f, 0.01f);
		this.resultLogoSprite.gameObject.GetComponent<TweenScale>().from = new Vector3(0.01f, 0.01f, 0.01f);
		this.resultNumLabel.gameObject.GetComponent<TweenScale>().to = new Vector3(1f, 1f, 1f);
		this.resultLogoSprite.gameObject.GetComponent<TweenScale>().to = new Vector3(1f, 1f, 1f);
		this.resultNumLabel.gameObject.SetActive(false);
		this.resultLogoSprite.gameObject.SetActive(false);
		this.casinoCanRefreshCoinbar = false;
		this.giftBoxSlotsTableInfo = GrowthManagerKit.GetGiftBoxSlotsResultInfo(UICasinoDirector.giftBoxSlotItemNum);
		this.giftBoxResultIndex = this.giftBoxSlotsTableInfo.resultIndex;
		int num = 3;
		if (this.giftBoxResultIndex < 8)
		{
			this.realCycleNums = num - 1;
		}
		else
		{
			this.realCycleNums = num;
		}
		if (this.giftBoxSlotsTableInfo != null)
		{
			List<GiftBoxSlotsItemInfo> itemList = this.giftBoxSlotsTableInfo.itemList;
			for (int i = 0; i < itemList.Count; i++)
			{
				this.cursorLighted[i] = false;
				this.giftBoxSlotItemLogo[i].mainTexture = (Resources.Load("UI/Images/SlotLogo/" + itemList[i].spriteName) as Texture);
				this.giftBoxSlotItemNumLabel[i].text = itemList[i].Num.ToString();
				this.cursorLighted[i] = false;
				this.giftBoxSlotCursor[i].gameObject.SetActive(false);
			}
		}
		this.giftboxLabel.text = GrowthManagerKit.GetCurGiftBoxTotal().ToString();
		base.StartCoroutine(this.AfterCoverSlotItemFg(0.4f));
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x000B2FC7 File Offset: 0x000B13C7
	private void TipOkBtnPressed()
	{
		UITipController.mInstance.HideCurTip();
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x000B2FD4 File Offset: 0x000B13D4
	private IEnumerator AfterCoverSlotItemFg(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		this.isSloting = true;
		yield break;
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x000B2FF8 File Offset: 0x000B13F8
	private void SlotItemBgChangeColorTimer()
	{
		this.giftBoxSlotItemBgChangeTime += Time.deltaTime;
		if (this.giftBoxSlotItemBgChangeTime > 1f)
		{
			this.giftBoxSlotItemBgChangeTime = 0f;
			this.colorIndex++;
			if (this.colorIndex >= this.colorArray.Length)
			{
				this.colorIndex = 0;
			}
			for (int i = 0; i < UICasinoDirector.giftBoxSlotItemNum; i++)
			{
				this.giftBoxSlotItemBg[i].color = this.colorArray[this.colorIndex];
			}
		}
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x000B3094 File Offset: 0x000B1494
	private void SlotCursorTimer()
	{
		if (this.restTimes > 0 && this.isSloting)
		{
			this.cursorDeltaTime += Time.deltaTime;
			if (this.curCycleIndex == 1)
			{
				if (this.curCursorIndex < 10)
				{
					this.cursorLevelDeltaTime = 0.46f - (float)this.curCursorIndex * 0.04f;
				}
				else
				{
					this.cursorLevelDeltaTime = 0.06f;
				}
				if (!this.cursorLighted[this.curCursorIndex] && this.cursorDeltaTime >= this.cursorLevelDeltaTime)
				{
					this.cursorDeltaTime = 0f;
					this.cursorLighted[this.curCursorIndex] = true;
					this.giftBoxSlotCursor[this.curCursorIndex].gameObject.SetActive(true);
					if (this.curCursorIndex == 0)
					{
						this.cursorLighted[UICasinoDirector.giftBoxSlotItemNum - 1] = false;
						this.giftBoxSlotCursor[UICasinoDirector.giftBoxSlotItemNum - 1].gameObject.SetActive(false);
					}
					else
					{
						this.cursorLighted[this.curCursorIndex - 1] = false;
						this.giftBoxSlotCursor[this.curCursorIndex - 1].gameObject.SetActive(false);
					}
					this.curCursorIndex++;
					if (this.curCursorIndex == UICasinoDirector.giftBoxSlotItemNum)
					{
						this.curCursorIndex = 0;
						this.curCycleIndex++;
					}
				}
			}
			else if (this.curCycleIndex == this.realCycleNums)
			{
				if (this.giftBoxResultIndex >= 9)
				{
					if (this.curCursorIndex >= this.giftBoxResultIndex - 9)
					{
						this.cursorLevelDeltaTime = 0.46f - (float)(this.giftBoxResultIndex - this.curCursorIndex) * 0.04f;
					}
					else
					{
						this.cursorLevelDeltaTime = 0.05f;
					}
					if (!this.cursorLighted[this.curCursorIndex] && this.cursorDeltaTime >= this.cursorLevelDeltaTime)
					{
						this.cursorDeltaTime = 0f;
						this.cursorLighted[this.curCursorIndex] = true;
						this.giftBoxSlotCursor[this.curCursorIndex].gameObject.SetActive(true);
						if (this.curCursorIndex == 0)
						{
							this.cursorLighted[UICasinoDirector.giftBoxSlotItemNum - 1] = false;
							this.giftBoxSlotCursor[UICasinoDirector.giftBoxSlotItemNum - 1].gameObject.SetActive(false);
						}
						else
						{
							this.cursorLighted[this.curCursorIndex - 1] = false;
							this.giftBoxSlotCursor[this.curCursorIndex - 1].gameObject.SetActive(false);
						}
						this.curCursorIndex++;
						if (this.curCursorIndex > this.giftBoxResultIndex)
						{
							this.curCursorIndex = 0;
							this.isSloting = false;
							this.casinoCanRefreshCoinbar = true;
							this.curCycleIndex = 1;
							this.cursorLighted[this.giftBoxResultIndex] = false;
							this.resultNumLabel.gameObject.SetActive(true);
							this.resultNumLabel.text = this.giftBoxSlotsTableInfo.itemList[this.giftBoxResultIndex].shareName.ToUpper();
							this.resultLogoSprite.gameObject.SetActive(true);
							this.resultLogoSprite.mainTexture = (Resources.Load("UI/Images/SlotLogo/" + this.giftBoxSlotsTableInfo.itemList[this.giftBoxResultIndex].spriteName) as Texture);
							TweenScale.Begin(this.resultNumLabel.gameObject, 1f, new Vector3(1f, 1f, 1f));
							TweenScale.Begin(this.resultLogoSprite.gameObject, 1f, new Vector3(1f, 1f, 1f));
							this.mSequenceFrameAnimation.enabled = true;
							this.mSequenceFrameAnimation.open = true;
							base.Invoke("StopSequenceFrameAnimation", 1.5f);
							if (this.giftBoxSlotsTableInfo.itemList[this.giftBoxResultIndex].isSpecialAward)
							{
								base.GetComponent<AudioSource>().clip = this.clips[3];
								base.GetComponent<AudioSource>().Play();
							}
							else
							{
								base.GetComponent<AudioSource>().clip = this.clips[2];
								base.GetComponent<AudioSource>().Play();
							}
							this.restTimes--;
							if (this.restTimes > 0)
							{
								base.Invoke("PrepareForStart", 2f);
							}
							else
							{
								this.restTimes = 0;
							}
						}
					}
				}
				else
				{
					int num = UICasinoDirector.giftBoxSlotItemNum + this.giftBoxResultIndex;
					if (this.curCursorIndex <= UICasinoDirector.giftBoxSlotItemNum - 1 - (10 - this.giftBoxResultIndex))
					{
						this.cursorLevelDeltaTime = 0.06f;
						if (!this.cursorLighted[this.curCursorIndex] && this.cursorDeltaTime >= this.cursorLevelDeltaTime)
						{
							this.cursorDeltaTime = 0f;
							this.cursorLighted[this.curCursorIndex] = true;
							this.giftBoxSlotCursor[this.curCursorIndex].gameObject.SetActive(true);
							if (this.curCursorIndex == 0)
							{
								this.cursorLighted[UICasinoDirector.giftBoxSlotItemNum - 1] = false;
								this.giftBoxSlotCursor[UICasinoDirector.giftBoxSlotItemNum - 1].gameObject.SetActive(false);
							}
							else
							{
								this.cursorLighted[this.curCursorIndex - 1] = false;
								this.giftBoxSlotCursor[this.curCursorIndex - 1].gameObject.SetActive(false);
							}
							this.curCursorIndex++;
						}
					}
					else
					{
						int num2 = this.curCursorIndex % UICasinoDirector.giftBoxSlotItemNum;
						this.cursorLevelDeltaTime = 0.04f + (float)(this.curCursorIndex - (num - 9)) * 0.04f;
						if (!this.cursorLighted[num2] && this.cursorDeltaTime >= this.cursorLevelDeltaTime)
						{
							this.cursorDeltaTime = 0f;
							this.cursorLighted[num2] = true;
							this.giftBoxSlotCursor[num2].gameObject.SetActive(true);
							if (num2 == 0)
							{
								this.cursorLighted[UICasinoDirector.giftBoxSlotItemNum - 1] = false;
								this.giftBoxSlotCursor[UICasinoDirector.giftBoxSlotItemNum - 1].gameObject.SetActive(false);
							}
							else
							{
								this.cursorLighted[num2 - 1] = false;
								this.giftBoxSlotCursor[num2 - 1].gameObject.SetActive(false);
							}
							this.curCursorIndex++;
							if (this.curCursorIndex > num)
							{
								this.curCursorIndex = 0;
								this.isSloting = false;
								this.casinoCanRefreshCoinbar = true;
								this.curCycleIndex = 1;
								this.cursorLighted[this.giftBoxResultIndex] = false;
								this.resultNumLabel.gameObject.SetActive(true);
								this.resultNumLabel.text = this.giftBoxSlotsTableInfo.itemList[this.giftBoxResultIndex].shareName.ToUpper();
								this.resultLogoSprite.gameObject.SetActive(true);
								this.resultLogoSprite.mainTexture = (Resources.Load("UI/Images/SlotLogo/" + this.giftBoxSlotsTableInfo.itemList[this.giftBoxResultIndex].spriteName) as Texture);
								TweenScale.Begin(this.resultNumLabel.gameObject, 1f, new Vector3(1f, 1f, 1f));
								TweenScale.Begin(this.resultLogoSprite.gameObject, 1f, new Vector3(1f, 1f, 1f));
								this.mSequenceFrameAnimation.enabled = true;
								this.mSequenceFrameAnimation.open = true;
								base.Invoke("StopSequenceFrameAnimation", 1.5f);
								if (this.giftBoxSlotsTableInfo.itemList[this.giftBoxResultIndex].isSpecialAward)
								{
									base.GetComponent<AudioSource>().clip = this.clips[3];
									base.GetComponent<AudioSource>().Play();
								}
								else
								{
									base.GetComponent<AudioSource>().clip = this.clips[2];
									base.GetComponent<AudioSource>().Play();
								}
								this.restTimes--;
								if (this.restTimes > 0)
								{
									base.Invoke("PrepareForStart", 2f);
								}
								else
								{
									this.restTimes = 0;
								}
							}
						}
					}
				}
			}
			else
			{
				this.cursorLevelDeltaTime = 0.06f;
				if (!this.cursorLighted[this.curCursorIndex] && this.cursorDeltaTime >= this.cursorLevelDeltaTime)
				{
					this.cursorDeltaTime = 0f;
					this.cursorLighted[this.curCursorIndex] = true;
					this.giftBoxSlotCursor[this.curCursorIndex].gameObject.SetActive(true);
					if (this.curCursorIndex == 0)
					{
						this.cursorLighted[UICasinoDirector.giftBoxSlotItemNum - 1] = false;
						this.giftBoxSlotCursor[UICasinoDirector.giftBoxSlotItemNum - 1].gameObject.SetActive(false);
					}
					else
					{
						this.cursorLighted[this.curCursorIndex - 1] = false;
						this.giftBoxSlotCursor[this.curCursorIndex - 1].gameObject.SetActive(false);
					}
					this.curCursorIndex++;
					if (this.curCursorIndex == UICasinoDirector.giftBoxSlotItemNum)
					{
						this.curCursorIndex = 0;
						this.curCycleIndex++;
					}
				}
			}
		}
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x000B3994 File Offset: 0x000B1D94
	private void StopSequenceFrameAnimation()
	{
		this.mSequenceFrameAnimation.T.mainTexture = null;
		this.mSequenceFrameAnimation.open = false;
		this.mSequenceFrameAnimation.enabled = false;
		if (this.restTimes == 0)
		{
			this.spinOnceBtn.isEnabled = true;
			this.spinTenBtn.isEnabled = true;
		}
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x000B39ED File Offset: 0x000B1DED
	public void ResultNumLabelTweenFinish()
	{
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x000B39EF File Offset: 0x000B1DEF
	public void ResultLogoSpriteTweenFinish()
	{
	}

	// Token: 0x04001781 RID: 6017
	public static UICasinoDirector mInstance;

	// Token: 0x04001782 RID: 6018
	public AudioClip[] clips;

	// Token: 0x04001783 RID: 6019
	private GiftBoxSlotsTableInfo giftBoxSlotsTableInfo = new GiftBoxSlotsTableInfo();

	// Token: 0x04001784 RID: 6020
	private int giftBoxResultIndex;

	// Token: 0x04001785 RID: 6021
	private static int giftBoxSlotItemNum = 14;

	// Token: 0x04001786 RID: 6022
	public UITexture[] giftBoxSlotItemLogo = new UITexture[UICasinoDirector.giftBoxSlotItemNum];

	// Token: 0x04001787 RID: 6023
	public UISprite[] giftBoxSlotCursor = new UISprite[UICasinoDirector.giftBoxSlotItemNum];

	// Token: 0x04001788 RID: 6024
	public UISprite[] giftBoxSlotItemBg = new UISprite[UICasinoDirector.giftBoxSlotItemNum];

	// Token: 0x04001789 RID: 6025
	public UILabel[] giftBoxSlotItemNumLabel = new UILabel[UICasinoDirector.giftBoxSlotItemNum];

	// Token: 0x0400178A RID: 6026
	private bool isSloting;

	// Token: 0x0400178B RID: 6027
	public bool casinoCanRefreshCoinbar = true;

	// Token: 0x0400178C RID: 6028
	private float giftBoxSlotItemBgChangeTime;

	// Token: 0x0400178D RID: 6029
	private Color[] colorArray = new Color[]
	{
		Color.magenta,
		Color.green,
		Color.red,
		Color.yellow,
		Color.white
	};

	// Token: 0x0400178E RID: 6030
	private int colorIndex;

	// Token: 0x0400178F RID: 6031
	public UIButton spinOnceBtn;

	// Token: 0x04001790 RID: 6032
	public UIButton spinTenBtn;

	// Token: 0x04001791 RID: 6033
	private int restTimes;

	// Token: 0x04001792 RID: 6034
	public UILabel giftboxLabel;

	// Token: 0x04001793 RID: 6035
	public UITexture resultLogoSprite;

	// Token: 0x04001794 RID: 6036
	public UILabel resultNumLabel;

	// Token: 0x04001795 RID: 6037
	private Texture defaultLogo;

	// Token: 0x04001796 RID: 6038
	public AudioSource mainMenuBgMusic;

	// Token: 0x04001797 RID: 6039
	public AudioSource casinoBgMusic;

	// Token: 0x04001798 RID: 6040
	public GameObject loadingSprite;

	// Token: 0x04001799 RID: 6041
	public GameObject luckyListItemPrefab;

	// Token: 0x0400179A RID: 6042
	public GameObject luckyListGrid;

	// Token: 0x0400179B RID: 6043
	private List<GameObject> luckyListItemList = new List<GameObject>();

	// Token: 0x0400179C RID: 6044
	private int curCursorIndex;

	// Token: 0x0400179D RID: 6045
	private bool[] cursorLighted = new bool[UICasinoDirector.giftBoxSlotItemNum];

	// Token: 0x0400179E RID: 6046
	private float cursorDeltaTime;

	// Token: 0x0400179F RID: 6047
	private float cursorLevelDeltaTime;

	// Token: 0x040017A0 RID: 6048
	private int realCycleNums;

	// Token: 0x040017A1 RID: 6049
	private int curCycleIndex = 1;

	// Token: 0x040017A2 RID: 6050
	public GGSequenceFrameAnimation mSequenceFrameAnimation;
}
