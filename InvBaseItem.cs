using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200053D RID: 1341
[Serializable]
public class InvBaseItem
{
	// Token: 0x04002685 RID: 9861
	public int id16;

	// Token: 0x04002686 RID: 9862
	public string name;

	// Token: 0x04002687 RID: 9863
	public string description;

	// Token: 0x04002688 RID: 9864
	public InvBaseItem.Slot slot;

	// Token: 0x04002689 RID: 9865
	public int minItemLevel = 1;

	// Token: 0x0400268A RID: 9866
	public int maxItemLevel = 50;

	// Token: 0x0400268B RID: 9867
	public List<InvStat> stats = new List<InvStat>();

	// Token: 0x0400268C RID: 9868
	public GameObject attachment;

	// Token: 0x0400268D RID: 9869
	public Color color = Color.white;

	// Token: 0x0400268E RID: 9870
	public UIAtlas iconAtlas;

	// Token: 0x0400268F RID: 9871
	public string iconName = string.Empty;

	// Token: 0x0200053E RID: 1342
	public enum Slot
	{
		// Token: 0x04002691 RID: 9873
		None,
		// Token: 0x04002692 RID: 9874
		Weapon,
		// Token: 0x04002693 RID: 9875
		Shield,
		// Token: 0x04002694 RID: 9876
		Body,
		// Token: 0x04002695 RID: 9877
		Shoulders,
		// Token: 0x04002696 RID: 9878
		Bracers,
		// Token: 0x04002697 RID: 9879
		Boots,
		// Token: 0x04002698 RID: 9880
		Trinket,
		// Token: 0x04002699 RID: 9881
		_LastDoNotUse
	}
}
