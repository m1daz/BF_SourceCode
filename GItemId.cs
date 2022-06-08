using System;

// Token: 0x02000179 RID: 377
[Serializable]
public class GItemId
{
	// Token: 0x06000AAB RID: 2731 RVA: 0x0004D6C1 File Offset: 0x0004BAC1
	public GItemId(int i_1, int i_2, int i_3, int i_x)
	{
		this.mId_1 = i_1;
		this.mId_2 = i_2;
		this.mId_3 = i_3;
		this.mId_x = i_x;
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0004D6E8 File Offset: 0x0004BAE8
	public string ParseToString()
	{
		return string.Concat(new object[]
		{
			'(',
			this.mId_1.ToString(),
			',',
			this.mId_2.ToString(),
			',',
			this.mId_3.ToString(),
			')'
		});
	}

	// Token: 0x040009DB RID: 2523
	public int mId_1;

	// Token: 0x040009DC RID: 2524
	public int mId_2;

	// Token: 0x040009DD RID: 2525
	public int mId_3;

	// Token: 0x040009DE RID: 2526
	public int mId_x;
}
