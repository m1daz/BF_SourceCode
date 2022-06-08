using System;

// Token: 0x02000543 RID: 1347
[Serializable]
public class InvStat
{
	// Token: 0x06002605 RID: 9733 RVA: 0x0011A1F3 File Offset: 0x001185F3
	public static string GetName(InvStat.Identifier i)
	{
		return i.ToString();
	}

	// Token: 0x06002606 RID: 9734 RVA: 0x0011A204 File Offset: 0x00118604
	public static string GetDescription(InvStat.Identifier i)
	{
		switch (i)
		{
		case InvStat.Identifier.Strength:
			return "Strength increases melee damage";
		case InvStat.Identifier.Constitution:
			return "Constitution increases health";
		case InvStat.Identifier.Agility:
			return "Agility increases armor";
		case InvStat.Identifier.Intelligence:
			return "Intelligence increases mana";
		case InvStat.Identifier.Damage:
			return "Damage adds to the amount of damage done in combat";
		case InvStat.Identifier.Crit:
			return "Crit increases the chance of landing a critical strike";
		case InvStat.Identifier.Armor:
			return "Armor protects from damage";
		case InvStat.Identifier.Health:
			return "Health prolongs life";
		case InvStat.Identifier.Mana:
			return "Mana increases the number of spells that can be cast";
		default:
			return null;
		}
	}

	// Token: 0x06002607 RID: 9735 RVA: 0x0011A278 File Offset: 0x00118678
	public static int CompareArmor(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Armor)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Damage)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06002608 RID: 9736 RVA: 0x0011A34C File Offset: 0x0011874C
	public static int CompareWeapon(InvStat a, InvStat b)
	{
		int num = (int)a.id;
		int num2 = (int)b.id;
		if (a.id == InvStat.Identifier.Damage)
		{
			num -= 10000;
		}
		else if (a.id == InvStat.Identifier.Armor)
		{
			num -= 5000;
		}
		if (b.id == InvStat.Identifier.Damage)
		{
			num2 -= 10000;
		}
		else if (b.id == InvStat.Identifier.Armor)
		{
			num2 -= 5000;
		}
		if (a.amount < 0)
		{
			num += 1000;
		}
		if (b.amount < 0)
		{
			num2 += 1000;
		}
		if (a.modifier == InvStat.Modifier.Percent)
		{
			num += 100;
		}
		if (b.modifier == InvStat.Modifier.Percent)
		{
			num2 += 100;
		}
		if (num < num2)
		{
			return -1;
		}
		if (num > num2)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x040026B4 RID: 9908
	public InvStat.Identifier id;

	// Token: 0x040026B5 RID: 9909
	public InvStat.Modifier modifier;

	// Token: 0x040026B6 RID: 9910
	public int amount;

	// Token: 0x02000544 RID: 1348
	public enum Identifier
	{
		// Token: 0x040026B8 RID: 9912
		Strength,
		// Token: 0x040026B9 RID: 9913
		Constitution,
		// Token: 0x040026BA RID: 9914
		Agility,
		// Token: 0x040026BB RID: 9915
		Intelligence,
		// Token: 0x040026BC RID: 9916
		Damage,
		// Token: 0x040026BD RID: 9917
		Crit,
		// Token: 0x040026BE RID: 9918
		Armor,
		// Token: 0x040026BF RID: 9919
		Health,
		// Token: 0x040026C0 RID: 9920
		Mana,
		// Token: 0x040026C1 RID: 9921
		Other
	}

	// Token: 0x02000545 RID: 1349
	public enum Modifier
	{
		// Token: 0x040026C3 RID: 9923
		Added,
		// Token: 0x040026C4 RID: 9924
		Percent
	}
}
