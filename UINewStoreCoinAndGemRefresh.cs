using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public class UINewStoreCoinAndGemRefresh : MonoBehaviour
{
	// Token: 0x060016FD RID: 5885 RVA: 0x000C2C36 File Offset: 0x000C1036
	private void Start()
	{
		this.CoinAndGemInit();
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x000C2C3E File Offset: 0x000C103E
	private void Update()
	{
		this.UpdateCoinBar();
	}

	// Token: 0x060016FF RID: 5887 RVA: 0x000C2C48 File Offset: 0x000C1048
	public void CoinAndGemInit()
	{
		this.coinNum = GrowthManagerKit.GetCoins();
		this.gemNum = GrowthManagerKit.GetGems();
		this.giftboxNum = GrowthManagerKit.GetCurGiftBoxTotal();
		this.honorpointNum = GrowthManagerKit.GetHonorPoint();
		this.coinLabel.text = this.coinNum.ToString();
		this.gemLabel.text = this.gemNum.ToString();
		this.giftboxLabel.text = this.giftboxNum.ToString();
		this.honorpointLabel.text = this.honorpointNum.ToString();
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x000C2CF4 File Offset: 0x000C10F4
	private void UpdateCoinBar()
	{
		if (GrowthManagerKit.NeedRefreshDataDisplay())
		{
			if (this.coinNum != GrowthManagerKit.GetCoins())
			{
				TweenScale.Begin(this.coinLabel.gameObject, 0.15f, new Vector3(1.5f, 1.5f, 1f));
				if (this.coinNum < GrowthManagerKit.GetCoins())
				{
					TweenColor.Begin(this.coinLabel.gameObject, 0.15f, Color.green);
				}
				else if (this.coinNum > GrowthManagerKit.GetCoins())
				{
					TweenColor.Begin(this.coinLabel.gameObject, 0.15f, Color.red);
				}
				base.StartCoroutine(this.TweenFinish(this.coinLabel));
				this.coinNum = GrowthManagerKit.GetCoins();
				this.coinLabel.text = this.coinNum.ToString();
			}
			if (this.gemNum != GrowthManagerKit.GetGems())
			{
				TweenScale.Begin(this.gemLabel.gameObject, 0.15f, new Vector3(1.5f, 1.5f, 1f));
				if (this.gemNum < GrowthManagerKit.GetGems())
				{
					TweenColor.Begin(this.gemLabel.gameObject, 0.15f, Color.green);
				}
				else if (this.gemNum > GrowthManagerKit.GetGems())
				{
					TweenColor.Begin(this.gemLabel.gameObject, 0.15f, Color.red);
				}
				base.StartCoroutine(this.TweenFinish(this.gemLabel));
				this.gemNum = GrowthManagerKit.GetGems();
				this.gemLabel.text = this.gemNum.ToString();
			}
			if (this.giftboxNum != GrowthManagerKit.GetCurGiftBoxTotal())
			{
				TweenScale.Begin(this.giftboxLabel.gameObject, 0.15f, new Vector3(1.5f, 1.5f, 1f));
				if (this.giftboxNum < GrowthManagerKit.GetCurGiftBoxTotal())
				{
					TweenColor.Begin(this.giftboxLabel.gameObject, 0.15f, Color.green);
				}
				else if (this.giftboxNum > GrowthManagerKit.GetCurGiftBoxTotal())
				{
					TweenColor.Begin(this.giftboxLabel.gameObject, 0.15f, Color.red);
				}
				base.StartCoroutine(this.TweenFinish(this.giftboxLabel));
				this.giftboxNum = GrowthManagerKit.GetCurGiftBoxTotal();
				this.giftboxLabel.text = this.giftboxNum.ToString();
			}
			if (this.honorpointNum != GrowthManagerKit.GetHonorPoint())
			{
				TweenScale.Begin(this.honorpointLabel.gameObject, 0.15f, new Vector3(1.5f, 1.5f, 1f));
				if (this.honorpointNum < GrowthManagerKit.GetHonorPoint())
				{
					TweenColor.Begin(this.honorpointLabel.gameObject, 0.15f, Color.green);
				}
				else if (this.honorpointNum > GrowthManagerKit.GetHonorPoint())
				{
					TweenColor.Begin(this.honorpointLabel.gameObject, 0.15f, Color.red);
				}
				base.StartCoroutine(this.TweenFinish(this.honorpointLabel));
				this.honorpointNum = GrowthManagerKit.GetHonorPoint();
				this.honorpointLabel.text = this.honorpointNum.ToString();
			}
			GrowthManagerKit.SetDataDisplayRefreshFlag(false);
		}
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x000C3050 File Offset: 0x000C1450
	private IEnumerator TweenFinish(UILabel label)
	{
		yield return new WaitForSeconds(0.2f);
		label.color = Color.white;
		label.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		yield break;
	}

	// Token: 0x040019C7 RID: 6599
	private int coinNum;

	// Token: 0x040019C8 RID: 6600
	private int gemNum;

	// Token: 0x040019C9 RID: 6601
	private int giftboxNum;

	// Token: 0x040019CA RID: 6602
	private int honorpointNum;

	// Token: 0x040019CB RID: 6603
	public UILabel coinLabel;

	// Token: 0x040019CC RID: 6604
	public UILabel gemLabel;

	// Token: 0x040019CD RID: 6605
	public UILabel giftboxLabel;

	// Token: 0x040019CE RID: 6606
	public UILabel honorpointLabel;
}
