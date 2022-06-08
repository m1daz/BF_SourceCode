using System;
using UnityEngine;

// Token: 0x020002B5 RID: 693
public class UITipController : MonoBehaviour
{
	// Token: 0x06001444 RID: 5188 RVA: 0x000B0F20 File Offset: 0x000AF320
	private void Awake()
	{
		if (UITipController.mInstance == null)
		{
			UITipController.mInstance = this;
		}
	}

	// Token: 0x06001445 RID: 5189 RVA: 0x000B0F38 File Offset: 0x000AF338
	private void OnDestroy()
	{
		if (UITipController.mInstance != null)
		{
			UITipController.mInstance = null;
		}
	}

	// Token: 0x06001446 RID: 5190 RVA: 0x000B0F50 File Offset: 0x000AF350
	private void Start()
	{
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x000B0F54 File Offset: 0x000AF354
	private void Update()
	{
		if (this.tipNode[2].activeSelf)
		{
			this.LoadingGif(0);
		}
		else if (this.tipNode[3].activeSelf)
		{
			this.LoadingGif(1);
		}
		else if (this.tipNode[4].activeSelf)
		{
			this.LoadingGif(2);
		}
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x000B0FB8 File Offset: 0x000AF3B8
	public void SetTipData(UITipController.TipType tipType, string tipContent, Color tipContentColor, string btnName1, string btnName2, EventDelegate btnEventName1, EventDelegate btnEventName2, EventDelegate inputSubmitEvent)
	{
		int num = this.TypeToIndex(tipType);
		if (num >= 0 && num < 11)
		{
			if (this.tipNode[this.curTipIndex].activeSelf)
			{
				this.tipNode[this.curTipIndex].SetActive(false);
			}
			this.curTipIndex = num;
			this.tipNode[num].SetActive(true);
			this.tipNode[num].GetComponent<UITipNode>().contentLabel.text = tipContent;
			this.tipNode[num].GetComponent<UITipNode>().contentLabel.color = tipContentColor;
			UIButton btn = this.tipNode[num].GetComponent<UITipNode>().btn1;
			UIButton btn2 = this.tipNode[num].GetComponent<UITipNode>().btn2;
			if (btn != null)
			{
				this.tipNode[num].GetComponent<UITipNode>().btnLabel1.text = btnName1;
				if (btnEventName1 != null)
				{
					EventDelegate.Add(btn.onClick, btnEventName1);
				}
			}
			if (btn2 != null)
			{
				this.tipNode[num].GetComponent<UITipNode>().btnLabel2.text = btnName2;
				if (btnEventName2 != null)
				{
					EventDelegate.Add(btn2.onClick, btnEventName2);
				}
			}
			if (this.passwordInput != null && tipType == UITipController.TipType.InputPasswordTip && inputSubmitEvent != null)
			{
				EventDelegate.Add(this.passwordInput.onSubmit, inputSubmitEvent);
			}
			if (this.input != null && tipType == UITipController.TipType.InputTip && inputSubmitEvent != null)
			{
				EventDelegate.Add(this.input.onSubmit, inputSubmitEvent);
			}
		}
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x000B1148 File Offset: 0x000AF548
	private int TypeToIndex(UITipController.TipType type)
	{
		int result = 0;
		switch (type)
		{
		case UITipController.TipType.OneButtonTip:
			result = 0;
			break;
		case UITipController.TipType.TwoButtonTip:
			result = 1;
			break;
		case UITipController.TipType.LoadingTip:
			result = 2;
			break;
		case UITipController.TipType.LoadingNoBg:
			result = 3;
			break;
		case UITipController.TipType.LobbyLoadingOneButton:
			result = 4;
			break;
		case UITipController.TipType.InputPasswordTip:
			result = 5;
			break;
		case UITipController.TipType.TwoButtonBlackBgTip:
			result = 6;
			break;
		case UITipController.TipType.InputTip:
			result = 7;
			break;
		case UITipController.TipType.TwoButtonPlusTip:
			result = 8;
			break;
		case UITipController.TipType.NoButtonTitleTip:
			result = 9;
			break;
		case UITipController.TipType.OneButtonCloseTip:
			result = 10;
			break;
		}
		return result;
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x000B11E5 File Offset: 0x000AF5E5
	public void HideCurTip()
	{
		if (this.tipNode[this.curTipIndex].activeSelf)
		{
			this.tipNode[this.curTipIndex].SetActive(false);
		}
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x000B1214 File Offset: 0x000AF614
	public void HideTip(UITipController.TipType type)
	{
		int num = this.TypeToIndex(type);
		if (this.tipNode[num].activeSelf)
		{
			this.tipNode[num].SetActive(false);
		}
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x000B124C File Offset: 0x000AF64C
	public bool TipActiveSelf(UITipController.TipType type)
	{
		int num = this.TypeToIndex(type);
		return num >= 0 && num < 11 && this.tipNode[num].activeSelf;
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x000B1280 File Offset: 0x000AF680
	private void LoadingGif(int index)
	{
		this.gifDeltaTime += Time.deltaTime;
		if (this.gifDeltaTime > 0.2f)
		{
			this.gifDeltaTime = 0f;
			this.loadingBarSprite[index].spriteName = ((!(this.loadingBarSprite[index].spriteName == "LoadingBar1")) ? "LoadingBar1" : "LoadingBar2");
		}
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x000B12F2 File Offset: 0x000AF6F2
	public void CloseBtnPressed()
	{
		this.HideCurTip();
	}

	// Token: 0x04001722 RID: 5922
	public static UITipController mInstance;

	// Token: 0x04001723 RID: 5923
	public UIInput input;

	// Token: 0x04001724 RID: 5924
	public UIInput passwordInput;

	// Token: 0x04001725 RID: 5925
	public GameObject oneButtonCloseTipCloseBtnObj;

	// Token: 0x04001726 RID: 5926
	private int curTipIndex;

	// Token: 0x04001727 RID: 5927
	private const int typeNum = 11;

	// Token: 0x04001728 RID: 5928
	public GameObject[] tipNode = new GameObject[11];

	// Token: 0x04001729 RID: 5929
	private float gifDeltaTime;

	// Token: 0x0400172A RID: 5930
	public UISprite[] loadingBarSprite = new UISprite[3];

	// Token: 0x020002B6 RID: 694
	public enum TipType
	{
		// Token: 0x0400172C RID: 5932
		Nil,
		// Token: 0x0400172D RID: 5933
		OneButtonTip,
		// Token: 0x0400172E RID: 5934
		TwoButtonTip,
		// Token: 0x0400172F RID: 5935
		LoadingTip,
		// Token: 0x04001730 RID: 5936
		LoadingNoBg,
		// Token: 0x04001731 RID: 5937
		LobbyLoadingOneButton,
		// Token: 0x04001732 RID: 5938
		InputPasswordTip,
		// Token: 0x04001733 RID: 5939
		TwoButtonBlackBgTip,
		// Token: 0x04001734 RID: 5940
		InputTip,
		// Token: 0x04001735 RID: 5941
		TwoButtonPlusTip,
		// Token: 0x04001736 RID: 5942
		NoButtonTitleTip,
		// Token: 0x04001737 RID: 5943
		OneButtonCloseTip
	}
}
