using System;

// Token: 0x02000635 RID: 1589
[Serializable]
public class UISpriteData
{
	// Token: 0x1700036B RID: 875
	// (get) Token: 0x06002E1C RID: 11804 RVA: 0x0015154E File Offset: 0x0014F94E
	public bool hasBorder
	{
		get
		{
			return (this.borderLeft | this.borderRight | this.borderTop | this.borderBottom) != 0;
		}
	}

	// Token: 0x1700036C RID: 876
	// (get) Token: 0x06002E1D RID: 11805 RVA: 0x00151571 File Offset: 0x0014F971
	public bool hasPadding
	{
		get
		{
			return (this.paddingLeft | this.paddingRight | this.paddingTop | this.paddingBottom) != 0;
		}
	}

	// Token: 0x06002E1E RID: 11806 RVA: 0x00151594 File Offset: 0x0014F994
	public void SetRect(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	// Token: 0x06002E1F RID: 11807 RVA: 0x001515B3 File Offset: 0x0014F9B3
	public void SetPadding(int left, int bottom, int right, int top)
	{
		this.paddingLeft = left;
		this.paddingBottom = bottom;
		this.paddingRight = right;
		this.paddingTop = top;
	}

	// Token: 0x06002E20 RID: 11808 RVA: 0x001515D2 File Offset: 0x0014F9D2
	public void SetBorder(int left, int bottom, int right, int top)
	{
		this.borderLeft = left;
		this.borderBottom = bottom;
		this.borderRight = right;
		this.borderTop = top;
	}

	// Token: 0x06002E21 RID: 11809 RVA: 0x001515F4 File Offset: 0x0014F9F4
	public void CopyFrom(UISpriteData sd)
	{
		this.name = sd.name;
		this.x = sd.x;
		this.y = sd.y;
		this.width = sd.width;
		this.height = sd.height;
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
		this.paddingLeft = sd.paddingLeft;
		this.paddingRight = sd.paddingRight;
		this.paddingTop = sd.paddingTop;
		this.paddingBottom = sd.paddingBottom;
	}

	// Token: 0x06002E22 RID: 11810 RVA: 0x0015169D File Offset: 0x0014FA9D
	public void CopyBorderFrom(UISpriteData sd)
	{
		this.borderLeft = sd.borderLeft;
		this.borderRight = sd.borderRight;
		this.borderTop = sd.borderTop;
		this.borderBottom = sd.borderBottom;
	}

	// Token: 0x04002D00 RID: 11520
	public string name = "Sprite";

	// Token: 0x04002D01 RID: 11521
	public int x;

	// Token: 0x04002D02 RID: 11522
	public int y;

	// Token: 0x04002D03 RID: 11523
	public int width;

	// Token: 0x04002D04 RID: 11524
	public int height;

	// Token: 0x04002D05 RID: 11525
	public int borderLeft;

	// Token: 0x04002D06 RID: 11526
	public int borderRight;

	// Token: 0x04002D07 RID: 11527
	public int borderTop;

	// Token: 0x04002D08 RID: 11528
	public int borderBottom;

	// Token: 0x04002D09 RID: 11529
	public int paddingLeft;

	// Token: 0x04002D0A RID: 11530
	public int paddingRight;

	// Token: 0x04002D0B RID: 11531
	public int paddingTop;

	// Token: 0x04002D0C RID: 11532
	public int paddingBottom;
}
