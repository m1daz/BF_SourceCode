using System;
using UnityEngine;

// Token: 0x020005AB RID: 1451
[Serializable]
public class BMSymbol
{
	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06002882 RID: 10370 RVA: 0x0012A673 File Offset: 0x00128A73
	public int length
	{
		get
		{
			if (this.mLength == 0)
			{
				this.mLength = this.sequence.Length;
			}
			return this.mLength;
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06002883 RID: 10371 RVA: 0x0012A697 File Offset: 0x00128A97
	public int offsetX
	{
		get
		{
			return this.mOffsetX;
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06002884 RID: 10372 RVA: 0x0012A69F File Offset: 0x00128A9F
	public int offsetY
	{
		get
		{
			return this.mOffsetY;
		}
	}

	// Token: 0x17000234 RID: 564
	// (get) Token: 0x06002885 RID: 10373 RVA: 0x0012A6A7 File Offset: 0x00128AA7
	public int width
	{
		get
		{
			return this.mWidth;
		}
	}

	// Token: 0x17000235 RID: 565
	// (get) Token: 0x06002886 RID: 10374 RVA: 0x0012A6AF File Offset: 0x00128AAF
	public int height
	{
		get
		{
			return this.mHeight;
		}
	}

	// Token: 0x17000236 RID: 566
	// (get) Token: 0x06002887 RID: 10375 RVA: 0x0012A6B7 File Offset: 0x00128AB7
	public int advance
	{
		get
		{
			return this.mAdvance;
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06002888 RID: 10376 RVA: 0x0012A6BF File Offset: 0x00128ABF
	public Rect uvRect
	{
		get
		{
			return this.mUV;
		}
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x0012A6C7 File Offset: 0x00128AC7
	public void MarkAsChanged()
	{
		this.mIsValid = false;
	}

	// Token: 0x0600288A RID: 10378 RVA: 0x0012A6D0 File Offset: 0x00128AD0
	public bool Validate(UIAtlas atlas)
	{
		if (atlas == null)
		{
			return false;
		}
		if (!this.mIsValid)
		{
			if (string.IsNullOrEmpty(this.spriteName))
			{
				return false;
			}
			this.mSprite = ((!(atlas != null)) ? null : atlas.GetSprite(this.spriteName));
			if (this.mSprite != null)
			{
				Texture texture = atlas.texture;
				if (texture == null)
				{
					this.mSprite = null;
				}
				else
				{
					this.mUV = new Rect((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
					this.mUV = NGUIMath.ConvertToTexCoords(this.mUV, texture.width, texture.height);
					this.mOffsetX = this.mSprite.paddingLeft;
					this.mOffsetY = this.mSprite.paddingTop;
					this.mWidth = this.mSprite.width;
					this.mHeight = this.mSprite.height;
					this.mAdvance = this.mSprite.width + (this.mSprite.paddingLeft + this.mSprite.paddingRight);
					this.mIsValid = true;
				}
			}
		}
		return this.mSprite != null;
	}

	// Token: 0x04002957 RID: 10583
	public string sequence;

	// Token: 0x04002958 RID: 10584
	public string spriteName;

	// Token: 0x04002959 RID: 10585
	private UISpriteData mSprite;

	// Token: 0x0400295A RID: 10586
	private bool mIsValid;

	// Token: 0x0400295B RID: 10587
	private int mLength;

	// Token: 0x0400295C RID: 10588
	private int mOffsetX;

	// Token: 0x0400295D RID: 10589
	private int mOffsetY;

	// Token: 0x0400295E RID: 10590
	private int mWidth;

	// Token: 0x0400295F RID: 10591
	private int mHeight;

	// Token: 0x04002960 RID: 10592
	private int mAdvance;

	// Token: 0x04002961 RID: 10593
	private Rect mUV;
}
