using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000541 RID: 1345
[Serializable]
public class InvGameItem
{
	// Token: 0x060025FC RID: 9724 RVA: 0x00119DD2 File Offset: 0x001181D2
	public InvGameItem(int id)
	{
		this.mBaseItemID = id;
	}

	// Token: 0x060025FD RID: 9725 RVA: 0x00119DEF File Offset: 0x001181EF
	public InvGameItem(int id, InvBaseItem bi)
	{
		this.mBaseItemID = id;
		this.mBaseItem = bi;
	}

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x060025FE RID: 9726 RVA: 0x00119E13 File Offset: 0x00118213
	public int baseItemID
	{
		get
		{
			return this.mBaseItemID;
		}
	}

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x060025FF RID: 9727 RVA: 0x00119E1B File Offset: 0x0011821B
	public InvBaseItem baseItem
	{
		get
		{
			if (this.mBaseItem == null)
			{
				this.mBaseItem = InvDatabase.FindByID(this.baseItemID);
			}
			return this.mBaseItem;
		}
	}

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06002600 RID: 9728 RVA: 0x00119E3F File Offset: 0x0011823F
	public string name
	{
		get
		{
			if (this.baseItem == null)
			{
				return null;
			}
			return this.quality.ToString() + " " + this.baseItem.name;
		}
	}

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06002601 RID: 9729 RVA: 0x00119E74 File Offset: 0x00118274
	public float statMultiplier
	{
		get
		{
			float num = 0f;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				num = 0f;
				break;
			case InvGameItem.Quality.Cursed:
				num = -1f;
				break;
			case InvGameItem.Quality.Damaged:
				num = 0.25f;
				break;
			case InvGameItem.Quality.Worn:
				num = 0.9f;
				break;
			case InvGameItem.Quality.Sturdy:
				num = 1f;
				break;
			case InvGameItem.Quality.Polished:
				num = 1.1f;
				break;
			case InvGameItem.Quality.Improved:
				num = 1.25f;
				break;
			case InvGameItem.Quality.Crafted:
				num = 1.5f;
				break;
			case InvGameItem.Quality.Superior:
				num = 1.75f;
				break;
			case InvGameItem.Quality.Enchanted:
				num = 2f;
				break;
			case InvGameItem.Quality.Epic:
				num = 2.5f;
				break;
			case InvGameItem.Quality.Legendary:
				num = 3f;
				break;
			}
			float num2 = (float)this.itemLevel / 50f;
			return num * Mathf.Lerp(num2, num2 * num2, 0.5f);
		}
	}

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06002602 RID: 9730 RVA: 0x00119F70 File Offset: 0x00118370
	public Color color
	{
		get
		{
			Color result = Color.white;
			switch (this.quality)
			{
			case InvGameItem.Quality.Broken:
				result = new Color(0.4f, 0.2f, 0.2f);
				break;
			case InvGameItem.Quality.Cursed:
				result = Color.red;
				break;
			case InvGameItem.Quality.Damaged:
				result = new Color(0.4f, 0.4f, 0.4f);
				break;
			case InvGameItem.Quality.Worn:
				result = new Color(0.7f, 0.7f, 0.7f);
				break;
			case InvGameItem.Quality.Sturdy:
				result = new Color(1f, 1f, 1f);
				break;
			case InvGameItem.Quality.Polished:
				result = NGUIMath.HexToColor(3774856959U);
				break;
			case InvGameItem.Quality.Improved:
				result = NGUIMath.HexToColor(2480359935U);
				break;
			case InvGameItem.Quality.Crafted:
				result = NGUIMath.HexToColor(1325334783U);
				break;
			case InvGameItem.Quality.Superior:
				result = NGUIMath.HexToColor(12255231U);
				break;
			case InvGameItem.Quality.Enchanted:
				result = NGUIMath.HexToColor(1937178111U);
				break;
			case InvGameItem.Quality.Epic:
				result = NGUIMath.HexToColor(2516647935U);
				break;
			case InvGameItem.Quality.Legendary:
				result = NGUIMath.HexToColor(4287627519U);
				break;
			}
			return result;
		}
	}

	// Token: 0x06002603 RID: 9731 RVA: 0x0011A0B0 File Offset: 0x001184B0
	public List<InvStat> CalculateStats()
	{
		List<InvStat> list = new List<InvStat>();
		if (this.baseItem != null)
		{
			float statMultiplier = this.statMultiplier;
			List<InvStat> stats = this.baseItem.stats;
			int i = 0;
			int count = stats.Count;
			while (i < count)
			{
				InvStat invStat = stats[i];
				int num = Mathf.RoundToInt(statMultiplier * (float)invStat.amount);
				if (num != 0)
				{
					bool flag = false;
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						InvStat invStat2 = list[j];
						if (invStat2.id == invStat.id && invStat2.modifier == invStat.modifier)
						{
							invStat2.amount += num;
							flag = true;
							break;
						}
						j++;
					}
					if (!flag)
					{
						list.Add(new InvStat
						{
							id = invStat.id,
							amount = num,
							modifier = invStat.modifier
						});
					}
				}
				i++;
			}
			List<InvStat> list2 = list;
			if (InvGameItem.<>f__mg$cache0 == null)
			{
				InvGameItem.<>f__mg$cache0 = new Comparison<InvStat>(InvStat.CompareArmor);
			}
			list2.Sort(InvGameItem.<>f__mg$cache0);
		}
		return list;
	}

	// Token: 0x040026A1 RID: 9889
	[SerializeField]
	private int mBaseItemID;

	// Token: 0x040026A2 RID: 9890
	public InvGameItem.Quality quality = InvGameItem.Quality.Sturdy;

	// Token: 0x040026A3 RID: 9891
	public int itemLevel = 1;

	// Token: 0x040026A4 RID: 9892
	private InvBaseItem mBaseItem;

	// Token: 0x040026A5 RID: 9893
	[CompilerGenerated]
	private static Comparison<InvStat> <>f__mg$cache0;

	// Token: 0x02000542 RID: 1346
	public enum Quality
	{
		// Token: 0x040026A7 RID: 9895
		Broken,
		// Token: 0x040026A8 RID: 9896
		Cursed,
		// Token: 0x040026A9 RID: 9897
		Damaged,
		// Token: 0x040026AA RID: 9898
		Worn,
		// Token: 0x040026AB RID: 9899
		Sturdy,
		// Token: 0x040026AC RID: 9900
		Polished,
		// Token: 0x040026AD RID: 9901
		Improved,
		// Token: 0x040026AE RID: 9902
		Crafted,
		// Token: 0x040026AF RID: 9903
		Superior,
		// Token: 0x040026B0 RID: 9904
		Enchanted,
		// Token: 0x040026B1 RID: 9905
		Epic,
		// Token: 0x040026B2 RID: 9906
		Legendary,
		// Token: 0x040026B3 RID: 9907
		_LastDoNotUse
	}
}
