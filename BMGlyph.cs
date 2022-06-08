using System;
using System.Collections.Generic;

// Token: 0x020005AA RID: 1450
[Serializable]
public class BMGlyph
{
	// Token: 0x0600287E RID: 10366 RVA: 0x0012A4B8 File Offset: 0x001288B8
	public int GetKerning(int previousChar)
	{
		if (this.kerning != null && previousChar != 0)
		{
			int i = 0;
			int count = this.kerning.Count;
			while (i < count)
			{
				if (this.kerning[i] == previousChar)
				{
					return this.kerning[i + 1];
				}
				i += 2;
			}
		}
		return 0;
	}

	// Token: 0x0600287F RID: 10367 RVA: 0x0012A518 File Offset: 0x00128918
	public void SetKerning(int previousChar, int amount)
	{
		if (this.kerning == null)
		{
			this.kerning = new List<int>();
		}
		for (int i = 0; i < this.kerning.Count; i += 2)
		{
			if (this.kerning[i] == previousChar)
			{
				this.kerning[i + 1] = amount;
				return;
			}
		}
		this.kerning.Add(previousChar);
		this.kerning.Add(amount);
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x0012A594 File Offset: 0x00128994
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		int num = this.x + this.width;
		int num2 = this.y + this.height;
		if (this.x < xMin)
		{
			int num3 = xMin - this.x;
			this.x += num3;
			this.width -= num3;
			this.offsetX += num3;
		}
		if (this.y < yMin)
		{
			int num4 = yMin - this.y;
			this.y += num4;
			this.height -= num4;
			this.offsetY += num4;
		}
		if (num > xMax)
		{
			this.width -= num - xMax;
		}
		if (num2 > yMax)
		{
			this.height -= num2 - yMax;
		}
	}

	// Token: 0x0400294D RID: 10573
	public int index;

	// Token: 0x0400294E RID: 10574
	public int x;

	// Token: 0x0400294F RID: 10575
	public int y;

	// Token: 0x04002950 RID: 10576
	public int width;

	// Token: 0x04002951 RID: 10577
	public int height;

	// Token: 0x04002952 RID: 10578
	public int offsetX;

	// Token: 0x04002953 RID: 10579
	public int offsetY;

	// Token: 0x04002954 RID: 10580
	public int advance;

	// Token: 0x04002955 RID: 10581
	public int channel;

	// Token: 0x04002956 RID: 10582
	public List<int> kerning;
}
