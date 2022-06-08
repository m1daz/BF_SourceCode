using System;
using SkinEditor;
using UnityEngine;

// Token: 0x020002E0 RID: 736
public class UISkinEditDirector : MonoBehaviour
{
	// Token: 0x06001685 RID: 5765 RVA: 0x000C0937 File Offset: 0x000BED37
	private void Awake()
	{
		if (UISkinEditDirector.mInstance == null)
		{
			UISkinEditDirector.mInstance = this;
		}
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x000C094F File Offset: 0x000BED4F
	private void OnDestroy()
	{
		if (UISkinEditDirector.mInstance != null)
		{
			UISkinEditDirector.mInstance = null;
		}
	}

	// Token: 0x06001687 RID: 5767 RVA: 0x000C0967 File Offset: 0x000BED67
	private void Start()
	{
		this.nameLabel.text = SkinEditorDirector.mInstance.curSelectedTitle.ToUpper();
		this.InitPalette();
		this.InitToolBarPanel();
	}

	// Token: 0x06001688 RID: 5768 RVA: 0x000C0990 File Offset: 0x000BED90
	private void Update()
	{
		if (SkinEditorDirector.mInstance.isNeedUIRefresh)
		{
			this.RefreshUI();
			SkinEditorDirector.mInstance.isNeedUIRefresh = false;
		}
		if (SkinEditorDirector.mInstance.m_SkinDrawer.isColorPickerOpen)
		{
			this.curColorSprite.color = SkinEditorDirector.mInstance.m_SkinDrawer.m_CurPaintColor;
		}
		if (this.isNeedRefreshUndoRedoBtnStatus)
		{
			this.RefreshUndoRedoBtnStatus();
		}
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x000C09FC File Offset: 0x000BEDFC
	public void RefreshUI()
	{
		this.nameLabel.text = SkinEditorDirector.mInstance.curSelectedTitle.ToUpper();
		switch (SkinEditorDirector.mInstance.m_CurViewLevel)
		{
		case ViewLevelType.Level_1_Overview:
			this.saveBtn.gameObject.SetActive(true);
			this.equipCostTipLabel.gameObject.SetActive(true);
			this.midBgSprite.gameObject.SetActive(true);
			this.toolNode.SetActive(false);
			break;
		case ViewLevelType.Level_2_Module:
			this.toolNode.SetActive(false);
			this.saveColorBtn.gameObject.SetActive(true);
			SkinEditorDirector.mInstance.m_SkinDrawer.CloseColorPicker();
			this.saveBtn.gameObject.SetActive(false);
			this.equipCostTipLabel.gameObject.SetActive(false);
			this.midBgSprite.gameObject.SetActive(true);
			break;
		case ViewLevelType.Level_3_Panel:
			this.toolNode.SetActive(true);
			this.isNeedRefreshUndoRedoBtnStatus = true;
			this.saveColorBtn.gameObject.SetActive(false);
			this.saveBtn.gameObject.SetActive(false);
			this.equipCostTipLabel.gameObject.SetActive(false);
			this.midBgSprite.gameObject.SetActive(true);
			this.colorAreaNode.SetActive(true);
			break;
		}
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x000C0B5C File Offset: 0x000BEF5C
	public void BackBtnPressed()
	{
		switch (SkinEditorDirector.mInstance.m_CurViewLevel)
		{
		case ViewLevelType.Level_1_Overview:
			if (!SkinEditorDirector.mInstance.isSaved)
			{
				string tipContent = "The new skin has not be saved.";
				EventDelegate btnEventName = new EventDelegate(this, "SaveAndQuitBtnPressed");
				EventDelegate btnEventName2 = new EventDelegate(this, "QuitBtnPressed");
				UITipController.mInstance.SetTipData(UITipController.TipType.TwoButtonTip, tipContent, Color.white, "Save & Quit", "Quit", btnEventName, btnEventName2, null);
				this.OverviewObj.SetActive(false);
			}
			else
			{
				this.QuitBtnPressed();
			}
			break;
		case ViewLevelType.Level_2_Module:
			SkinEditorDirector.mInstance.Back();
			this.saveBtn.gameObject.SetActive(true);
			this.equipCostTipLabel.gameObject.SetActive(true);
			this.midBgSprite.gameObject.SetActive(false);
			this.colorAreaNode.SetActive(false);
			break;
		case ViewLevelType.Level_3_Panel:
			if (SkinEditorDirector.mInstance.m_SkinDrawer.isColorPickerOpen)
			{
				SkinEditorDirector.mInstance.m_SkinDrawer.CloseColorPicker();
				this.toolNode.SetActive(true);
				this.isNeedRefreshUndoRedoBtnStatus = true;
				this.saveColorBtn.gameObject.SetActive(false);
			}
			else
			{
				SkinEditorDirector.mInstance.Back();
				this.toolNode.SetActive(false);
				this.colorAreaNode.SetActive(false);
			}
			break;
		}
	}

	// Token: 0x0600168B RID: 5771 RVA: 0x000C0CBA File Offset: 0x000BF0BA
	public void SaveBtnPressed()
	{
		if (!SkinEditorDirector.mInstance.isSaved)
		{
			SkinEditorDirector.mInstance.Save();
		}
	}

	// Token: 0x0600168C RID: 5772 RVA: 0x000C0CD5 File Offset: 0x000BF0D5
	public void PaletteBtnPressed()
	{
		SkinEditorDirector.mInstance.m_SkinDrawer.OpenColorPicker();
		this.toolNode.SetActive(false);
		this.saveColorBtn.gameObject.SetActive(true);
	}

	// Token: 0x0600168D RID: 5773 RVA: 0x000C0D03 File Offset: 0x000BF103
	public void SaveAndQuitBtnPressed()
	{
		this.SaveBtnPressed();
		this.QuitBtnPressed();
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x000C0D11 File Offset: 0x000BF111
	public void QuitBtnPressed()
	{
		UIUserDataController.SetCurPaletteColor(this.curColorSprite.color);
		SkinEditorDirector.mInstance.Back();
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x000C0D34 File Offset: 0x000BF134
	private void InitPalette()
	{
		this.InitPaletteButton();
		this.curColorSprite.color = UIUserDataController.GetCurPaletteColor();
		this.InitSelectPalette(1);
		this.saveColorBtn.gameObject.SetActive(false);
		this.colorAreaNode.SetActive(false);
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x000C0D80 File Offset: 0x000BF180
	private void InitPaletteButton()
	{
		int unlockPaletteIndex = GrowthManagerKit.GetUnlockPaletteIndex();
		for (int i = 1; i <= 5; i++)
		{
			if (i <= unlockPaletteIndex)
			{
				this.paletteButton[i - 1].gameObject.SetActive(true);
				this.paletteButton[i - 1].GetComponentInChildren<UILabel>().text = string.Empty + i + string.Empty;
			}
			else if (i == unlockPaletteIndex + 1)
			{
				this.paletteButton[i - 1].gameObject.SetActive(true);
			}
			else
			{
				this.paletteButton[i - 1].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06001691 RID: 5777 RVA: 0x000C0E28 File Offset: 0x000BF228
	private void InitSelectPalette(int paletteIndexArg)
	{
		for (int i = 0; i < 15; i++)
		{
			if (i == this.curSelectSquareIndex && this.curSelectPaletteIndex == this.curSelectSquareInPaletteIndex)
			{
				this.colorToggleCheckmark[i].gameObject.SetActive(true);
			}
			else
			{
				this.colorToggleCheckmark[i].gameObject.SetActive(false);
			}
			this.colorToggleBg[i].color = UIUserDataController.GetSavedColor(i + (paletteIndexArg - 1) * 15);
		}
		int unlockPaletteIndex = GrowthManagerKit.GetUnlockPaletteIndex();
		for (int j = 0; j < 5; j++)
		{
			if (j == paletteIndexArg - 1)
			{
				this.paletteButton[j].Set(true, true);
			}
		}
		this.InitPaletteButton();
	}

	// Token: 0x06001692 RID: 5778 RVA: 0x000C0EE5 File Offset: 0x000BF2E5
	public void PaletteUnlockReset()
	{
		GrowthManagerKit.SetUnlockPaletteIndex(2);
	}

	// Token: 0x06001693 RID: 5779 RVA: 0x000C0EED File Offset: 0x000BF2ED
	public void PaletteSelect(int indexArg)
	{
		this.curSelectPaletteIndex = indexArg;
		this.InitSelectPalette(indexArg);
	}

	// Token: 0x06001694 RID: 5780 RVA: 0x000C0F00 File Offset: 0x000BF300
	public void PaletteSelectChecked(int indexArg)
	{
		int unlockPaletteIndex = GrowthManagerKit.GetUnlockPaletteIndex();
		if (this.curSelectPaletteIndex != indexArg)
		{
			if (indexArg == unlockPaletteIndex + 1)
			{
				this.selectLockedPaletteIndexArg = indexArg;
				int paletteUnlockPrice = GrowthManagerKit.GetPaletteUnlockPrice(indexArg);
				if (GrowthManagerKit.GetCoins() >= paletteUnlockPrice)
				{
					string text = "Cost " + paletteUnlockPrice + " coins to unlock this palette?";
					this.UnlockButtonNode.SetActive(true);
					this.UnlockLabel.text = text;
				}
				else
				{
					string tipContent = "No Enough Coin!\nYou need " + paletteUnlockPrice + " coins to unlock this palette!";
					EventDelegate btnEventName = new EventDelegate(this, "cancelBtnPressed");
					UITipController.mInstance.SetTipData(UITipController.TipType.OneButtonTip, tipContent, Color.white, "OK", string.Empty, btnEventName, null, null);
				}
				this.OverviewCanvasObj.SetActive(false);
			}
			else if (indexArg <= unlockPaletteIndex)
			{
				this.PaletteSelect(indexArg);
			}
		}
	}

	// Token: 0x06001695 RID: 5781 RVA: 0x000C0FD8 File Offset: 0x000BF3D8
	public void buyBtnPressed()
	{
		this.UnlockButtonNode.SetActive(false);
		int paletteUnlockPrice = GrowthManagerKit.GetPaletteUnlockPrice(this.selectLockedPaletteIndexArg);
		if (GrowthManagerKit.SubCoins(paletteUnlockPrice))
		{
			int unlockPaletteIndex = GrowthManagerKit.GetUnlockPaletteIndex();
			if (unlockPaletteIndex < 5)
			{
				GrowthManagerKit.SetUnlockPaletteIndex(unlockPaletteIndex + 1);
			}
			this.OverviewCanvasObj.SetActive(true);
			for (int i = 1; i <= 5; i++)
			{
				if (i <= this.selectLockedPaletteIndexArg)
				{
					this.paletteButton[i - 1].gameObject.SetActive(true);
					this.paletteButton[i - 1].GetComponentInChildren<UILabel>().text = string.Empty + i + string.Empty;
				}
				else if (i == this.selectLockedPaletteIndexArg + 1)
				{
					this.paletteButton[i - 1].gameObject.SetActive(true);
				}
				else
				{
					this.paletteButton[i - 1].gameObject.SetActive(false);
				}
			}
			this.PaletteSelect(this.selectLockedPaletteIndexArg);
			this.selectLockedPaletteIndexArg = 0;
			return;
		}
	}

	// Token: 0x06001696 RID: 5782 RVA: 0x000C10E0 File Offset: 0x000BF4E0
	public void cancelBtnPressed()
	{
		this.OverviewCanvasObj.SetActive(true);
		this.UnlockButtonNode.SetActive(false);
		this.paletteButton[this.curSelectPaletteIndex - 1].Set(true, true);
	}

	// Token: 0x06001697 RID: 5783 RVA: 0x000C1110 File Offset: 0x000BF510
	public void ColorSelectChecked(int indexArg)
	{
		if (this.curColorSprite.color != this.colorToggleBg[indexArg - 1].color || this.curSelectSquareIndex != indexArg - 1)
		{
			this.colorToggleCheckmark[this.curSelectSquareIndex].gameObject.SetActive(false);
			this.curSelectSquareIndex = indexArg - 1;
			this.colorToggleCheckmark[this.curSelectSquareIndex].gameObject.SetActive(true);
			this.curColorSprite.color = this.colorToggleBg[this.curSelectSquareIndex].color;
			this.curSelectSquareInPaletteIndex = this.curSelectPaletteIndex;
			SkinEditorDirector.mInstance.m_SkinDrawer.m_CurPaintColor = this.curColorSprite.color;
		}
	}

	// Token: 0x06001698 RID: 5784 RVA: 0x000C11CC File Offset: 0x000BF5CC
	public void SaveColorBtnPressed()
	{
		this.colorToggleBg[this.curSelectSquareIndex].color = this.curColorSprite.color;
		UIUserDataController.SetSavedColor(this.curColorSprite.color, this.curSelectSquareIndex + (this.curSelectPaletteIndex - 1) * 15);
	}

	// Token: 0x06001699 RID: 5785 RVA: 0x000C1220 File Offset: 0x000BF620
	private void InitToolBarPanel()
	{
		this.toolNode.SetActive(false);
		this.noiseSlider.value = (float)this.preNoiseValue * 0.1f;
		this.noiseValueLabel.text = "Intensity : " + this.preNoiseValue.ToString();
	}

	// Token: 0x0600169A RID: 5786 RVA: 0x000C1277 File Offset: 0x000BF677
	public void UndoBtnPressed()
	{
		SkinEditorDirector.mInstance.m_SkinDrawer.UnDo();
	}

	// Token: 0x0600169B RID: 5787 RVA: 0x000C1288 File Offset: 0x000BF688
	public void RedoBtnPressed()
	{
		SkinEditorDirector.mInstance.m_SkinDrawer.ReDo();
	}

	// Token: 0x0600169C RID: 5788 RVA: 0x000C1299 File Offset: 0x000BF699
	public void SuckerToggleValueChanged()
	{
		if (this.suckerToggle.value)
		{
			SkinEditorDirector.mInstance.m_SkinDrawer.SwitchBrushToSucker();
		}
	}

	// Token: 0x0600169D RID: 5789 RVA: 0x000C12BA File Offset: 0x000BF6BA
	public void PencilToggleValueChanged()
	{
		if (this.pencilToggle.value)
		{
			SkinEditorDirector.mInstance.m_SkinDrawer.SwitchBrushToPencil();
		}
	}

	// Token: 0x0600169E RID: 5790 RVA: 0x000C12DB File Offset: 0x000BF6DB
	public void PaintbucketToggleValueChanged()
	{
		if (this.paintbucketToggle.value)
		{
			SkinEditorDirector.mInstance.m_SkinDrawer.SwitchBrushToPaintBucket();
		}
	}

	// Token: 0x0600169F RID: 5791 RVA: 0x000C12FC File Offset: 0x000BF6FC
	private void RefreshUndoRedoBtnStatus()
	{
		if (SkinEditorDirector.mInstance.m_SkinDrawer.CanUnDo())
		{
			this.undoBtn.isEnabled = true;
		}
		else
		{
			this.undoBtn.isEnabled = false;
		}
		if (SkinEditorDirector.mInstance.m_SkinDrawer.CanReDo())
		{
			this.redoBtn.isEnabled = true;
		}
		else
		{
			this.redoBtn.isEnabled = false;
		}
	}

	// Token: 0x060016A0 RID: 5792 RVA: 0x000C136C File Offset: 0x000BF76C
	public void NoiseValueChanged()
	{
		int num = (int)(this.noiseSlider.value * 10f);
		if (this.preNoiseValue != num)
		{
			this.noiseValueLabel.text = "Intensity : " + num.ToString();
			SkinEditorDirector.mInstance.m_SkinDrawer.SetNoiseFactor(num);
			this.preNoiseValue = num;
		}
	}

	// Token: 0x060016A1 RID: 5793 RVA: 0x000C13D1 File Offset: 0x000BF7D1
	public void AddNoiseBtnPressed()
	{
		SkinEditorDirector.mInstance.m_SkinDrawer.AddNoise();
	}

	// Token: 0x04001959 RID: 6489
	public static UISkinEditDirector mInstance;

	// Token: 0x0400195A RID: 6490
	public UIButton saveBtn;

	// Token: 0x0400195B RID: 6491
	public UIButton paletteBtn;

	// Token: 0x0400195C RID: 6492
	public UILabel nameLabel;

	// Token: 0x0400195D RID: 6493
	public GameObject OverviewObj;

	// Token: 0x0400195E RID: 6494
	public GameObject OverviewCanvasObj;

	// Token: 0x0400195F RID: 6495
	public UISprite midBgSprite;

	// Token: 0x04001960 RID: 6496
	public UILabel equipCostTipLabel;

	// Token: 0x04001961 RID: 6497
	public GameObject UnlockButtonNode;

	// Token: 0x04001962 RID: 6498
	public UILabel UnlockLabel;

	// Token: 0x04001963 RID: 6499
	public GameObject colorAreaNode;

	// Token: 0x04001964 RID: 6500
	public UISprite curColorSprite;

	// Token: 0x04001965 RID: 6501
	public UIButton saveColorBtn;

	// Token: 0x04001966 RID: 6502
	public UIButton[] colorButton = new UIButton[15];

	// Token: 0x04001967 RID: 6503
	public UIToggle[] paletteButton = new UIToggle[5];

	// Token: 0x04001968 RID: 6504
	public UISprite[] colorToggleCheckmark = new UISprite[15];

	// Token: 0x04001969 RID: 6505
	public UISprite[] colorToggleBg = new UISprite[15];

	// Token: 0x0400196A RID: 6506
	private int curSelectSquareIndex;

	// Token: 0x0400196B RID: 6507
	private int curSelectSquareInPaletteIndex = 1;

	// Token: 0x0400196C RID: 6508
	private int curSelectPaletteIndex = 1;

	// Token: 0x0400196D RID: 6509
	private bool isNeedRefreshUndoRedoBtnStatus;

	// Token: 0x0400196E RID: 6510
	public int selectLockedPaletteIndexArg;

	// Token: 0x0400196F RID: 6511
	public UIButton undoBtn;

	// Token: 0x04001970 RID: 6512
	public UIButton redoBtn;

	// Token: 0x04001971 RID: 6513
	public UIToggle suckerToggle;

	// Token: 0x04001972 RID: 6514
	public UIToggle pencilToggle;

	// Token: 0x04001973 RID: 6515
	public UIToggle paintbucketToggle;

	// Token: 0x04001974 RID: 6516
	public GameObject toolNode;

	// Token: 0x04001975 RID: 6517
	public UISlider noiseSlider;

	// Token: 0x04001976 RID: 6518
	public UILabel noiseValueLabel;

	// Token: 0x04001977 RID: 6519
	private int preNoiseValue = 5;
}
