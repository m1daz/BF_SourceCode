using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005A9 RID: 1449
[Serializable]
public class BMFont
{
	// Token: 0x17000229 RID: 553
	// (get) Token: 0x0600286C RID: 10348 RVA: 0x0012A30E File Offset: 0x0012870E
	public bool isValid
	{
		get
		{
			return this.mSaved.Count > 0;
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x0600286D RID: 10349 RVA: 0x0012A31E File Offset: 0x0012871E
	// (set) Token: 0x0600286E RID: 10350 RVA: 0x0012A326 File Offset: 0x00128726
	public int charSize
	{
		get
		{
			return this.mSize;
		}
		set
		{
			this.mSize = value;
		}
	}

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x0600286F RID: 10351 RVA: 0x0012A32F File Offset: 0x0012872F
	// (set) Token: 0x06002870 RID: 10352 RVA: 0x0012A337 File Offset: 0x00128737
	public int baseOffset
	{
		get
		{
			return this.mBase;
		}
		set
		{
			this.mBase = value;
		}
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06002871 RID: 10353 RVA: 0x0012A340 File Offset: 0x00128740
	// (set) Token: 0x06002872 RID: 10354 RVA: 0x0012A348 File Offset: 0x00128748
	public int texWidth
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			this.mWidth = value;
		}
	}

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06002873 RID: 10355 RVA: 0x0012A351 File Offset: 0x00128751
	// (set) Token: 0x06002874 RID: 10356 RVA: 0x0012A359 File Offset: 0x00128759
	public int texHeight
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			this.mHeight = value;
		}
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06002875 RID: 10357 RVA: 0x0012A362 File Offset: 0x00128762
	public int glyphCount
	{
		get
		{
			return (!this.isValid) ? 0 : this.mSaved.Count;
		}
	}

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06002876 RID: 10358 RVA: 0x0012A380 File Offset: 0x00128780
	// (set) Token: 0x06002877 RID: 10359 RVA: 0x0012A388 File Offset: 0x00128788
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			this.mSpriteName = value;
		}
	}

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06002878 RID: 10360 RVA: 0x0012A391 File Offset: 0x00128791
	public List<BMGlyph> glyphs
	{
		get
		{
			return this.mSaved;
		}
	}

	// Token: 0x06002879 RID: 10361 RVA: 0x0012A39C File Offset: 0x0012879C
	public BMGlyph GetGlyph(int index, bool createIfMissing)
	{
		BMGlyph bmglyph = null;
		if (this.mDict.Count == 0)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph2 = this.mSaved[i];
				this.mDict.Add(bmglyph2.index, bmglyph2);
				i++;
			}
		}
		if (!this.mDict.TryGetValue(index, out bmglyph) && createIfMissing)
		{
			bmglyph = new BMGlyph();
			bmglyph.index = index;
			this.mSaved.Add(bmglyph);
			this.mDict.Add(index, bmglyph);
		}
		return bmglyph;
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x0012A438 File Offset: 0x00128838
	public BMGlyph GetGlyph(int index)
	{
		return this.GetGlyph(index, false);
	}

	// Token: 0x0600287B RID: 10363 RVA: 0x0012A442 File Offset: 0x00128842
	public void Clear()
	{
		this.mDict.Clear();
		this.mSaved.Clear();
	}

	// Token: 0x0600287C RID: 10364 RVA: 0x0012A45C File Offset: 0x0012885C
	public void Trim(int xMin, int yMin, int xMax, int yMax)
	{
		if (this.isValid)
		{
			int i = 0;
			int count = this.mSaved.Count;
			while (i < count)
			{
				BMGlyph bmglyph = this.mSaved[i];
				if (bmglyph != null)
				{
					bmglyph.Trim(xMin, yMin, xMax, yMax);
				}
				i++;
			}
		}
	}

	// Token: 0x04002946 RID: 10566
	[HideInInspector]
	[SerializeField]
	private int mSize = 16;

	// Token: 0x04002947 RID: 10567
	[HideInInspector]
	[SerializeField]
	private int mBase;

	// Token: 0x04002948 RID: 10568
	[HideInInspector]
	[SerializeField]
	private int mWidth;

	// Token: 0x04002949 RID: 10569
	[HideInInspector]
	[SerializeField]
	private int mHeight;

	// Token: 0x0400294A RID: 10570
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x0400294B RID: 10571
	[HideInInspector]
	[SerializeField]
	private List<BMGlyph> mSaved = new List<BMGlyph>();

	// Token: 0x0400294C RID: 10572
	private Dictionary<int, BMGlyph> mDict = new Dictionary<int, BMGlyph>();
}
