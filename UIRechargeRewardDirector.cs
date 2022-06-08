using System;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class UIRechargeRewardDirector : MonoBehaviour
{
	// Token: 0x060015A7 RID: 5543 RVA: 0x000B9067 File Offset: 0x000B7467
	private void Awake()
	{
		UIRechargeRewardDirector.mInstance = this;
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x000B906F File Offset: 0x000B746F
	private void OnDestroy()
	{
		if (UIRechargeRewardDirector.mInstance != null)
		{
			UIRechargeRewardDirector.mInstance = null;
		}
	}

	// Token: 0x060015A9 RID: 5545 RVA: 0x000B9087 File Offset: 0x000B7487
	private void Start()
	{
		this.RefreshUI();
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x000B908F File Offset: 0x000B748F
	private void Update()
	{
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x000B9094 File Offset: 0x000B7494
	public void RefreshUI()
	{
		int num = 0;
		int holidayRechargeRecord = GrowthManagerKit.GetHolidayRechargeRecord();
		this.targetValueArray = GrowthManagerKit.HolidayRechargeRewardTarget();
		for (int i = 0; i < this.targetValueArray.Length; i++)
		{
			if (holidayRechargeRecord < this.targetValueArray[i])
			{
				num = this.targetValueArray[i];
				break;
			}
			if (i == this.targetValueArray.Length - 1)
			{
				num = this.targetValueArray[i];
			}
		}
		this.rechargeProgressLabel.text = holidayRechargeRecord.ToString() + "/" + num.ToString();
		this.rechargeProgressSprite.fillAmount = ((holidayRechargeRecord >= num) ? 1f : ((float)holidayRechargeRecord / (float)num));
		for (int j = 0; j < this.targetValueArray.Length; j++)
		{
			if (GrowthManagerKit.CanGetHolidayRechargeReward(j + 1) && !GrowthManagerKit.HasEverGotHolidayRechargeReward(j + 1))
			{
				this.rewardGetBtn[j].isEnabled = true;
			}
			else
			{
				this.rewardGetBtn[j].isEnabled = false;
			}
		}
		this.rewardGetTipSprite.SetActive(false);
		for (int k = 0; k < this.rewardGetBtn.Length; k++)
		{
			if (this.rewardGetBtn[k].isEnabled)
			{
				this.rewardGetTipSprite.SetActive(true);
				break;
			}
		}
	}

	// Token: 0x060015AC RID: 5548 RVA: 0x000B91F3 File Offset: 0x000B75F3
	public void RewardGetBtnPressed(int index)
	{
		GrowthManagerKit.ReceiveHolidayRechargeReward(index);
		this.RefreshUI();
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x000B9201 File Offset: 0x000B7601
	public void BackBtnPressed()
	{
		UIHomeDirector.mInstance.BackToRootNode(UIHomeDirector.mInstance.rechargeRewardNode);
	}

	// Token: 0x04001879 RID: 6265
	public static UIRechargeRewardDirector mInstance;

	// Token: 0x0400187A RID: 6266
	public GameObject giftGetNode;

	// Token: 0x0400187B RID: 6267
	public UISprite rechargeProgressSprite;

	// Token: 0x0400187C RID: 6268
	public UILabel rechargeProgressLabel;

	// Token: 0x0400187D RID: 6269
	public UILabel tipLabel;

	// Token: 0x0400187E RID: 6270
	public UIButton[] rewardGetBtn = new UIButton[6];

	// Token: 0x0400187F RID: 6271
	private int[] targetValueArray = new int[6];

	// Token: 0x04001880 RID: 6272
	public GameObject rewardGetTipSprite;
}
