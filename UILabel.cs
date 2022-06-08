using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000623 RID: 1571
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Label")]
public class UILabel : UIWidget
{
	// Token: 0x17000306 RID: 774
	// (get) Token: 0x06002CFD RID: 11517 RVA: 0x0014AC11 File Offset: 0x00149011
	public int finalFontSize
	{
		get
		{
			if (this.trueTypeFont)
			{
				return Mathf.RoundToInt(this.mScale * (float)this.mFinalFontSize);
			}
			return Mathf.RoundToInt((float)this.mFontSize * this.mScale);
		}
	}

	// Token: 0x17000307 RID: 775
	// (get) Token: 0x06002CFE RID: 11518 RVA: 0x0014AC4A File Offset: 0x0014904A
	// (set) Token: 0x06002CFF RID: 11519 RVA: 0x0014AC52 File Offset: 0x00149052
	private bool shouldBeProcessed
	{
		get
		{
			return this.mShouldBeProcessed;
		}
		set
		{
			if (value)
			{
				this.mChanged = true;
				this.mShouldBeProcessed = true;
			}
			else
			{
				this.mShouldBeProcessed = false;
			}
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x06002D00 RID: 11520 RVA: 0x0014AC74 File Offset: 0x00149074
	public override bool isAnchoredHorizontally
	{
		get
		{
			return base.isAnchoredHorizontally || this.mOverflow == UILabel.Overflow.ResizeFreely;
		}
	}

	// Token: 0x17000309 RID: 777
	// (get) Token: 0x06002D01 RID: 11521 RVA: 0x0014AC8D File Offset: 0x0014908D
	public override bool isAnchoredVertically
	{
		get
		{
			return base.isAnchoredVertically || this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight;
		}
	}

	// Token: 0x1700030A RID: 778
	// (get) Token: 0x06002D02 RID: 11522 RVA: 0x0014ACB4 File Offset: 0x001490B4
	// (set) Token: 0x06002D03 RID: 11523 RVA: 0x0014AD14 File Offset: 0x00149114
	public override Material material
	{
		get
		{
			if (this.mMat != null)
			{
				return this.mMat;
			}
			if (this.mFont != null)
			{
				return this.mFont.material;
			}
			if (this.mTrueTypeFont != null)
			{
				return this.mTrueTypeFont.material;
			}
			return null;
		}
		set
		{
			base.material = value;
		}
	}

	// Token: 0x1700030B RID: 779
	// (get) Token: 0x06002D04 RID: 11524 RVA: 0x0014AD20 File Offset: 0x00149120
	// (set) Token: 0x06002D05 RID: 11525 RVA: 0x0014AD7B File Offset: 0x0014917B
	public override Texture mainTexture
	{
		get
		{
			if (this.mFont != null)
			{
				return this.mFont.texture;
			}
			if (this.mTrueTypeFont != null)
			{
				Material material = this.mTrueTypeFont.material;
				if (material != null)
				{
					return material.mainTexture;
				}
			}
			return null;
		}
		set
		{
			base.mainTexture = value;
		}
	}

	// Token: 0x1700030C RID: 780
	// (get) Token: 0x06002D06 RID: 11526 RVA: 0x0014AD84 File Offset: 0x00149184
	// (set) Token: 0x06002D07 RID: 11527 RVA: 0x0014AD8C File Offset: 0x0014918C
	[Obsolete("Use UILabel.bitmapFont instead")]
	public UIFont font
	{
		get
		{
			return this.bitmapFont;
		}
		set
		{
			this.bitmapFont = value;
		}
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06002D08 RID: 11528 RVA: 0x0014AD95 File Offset: 0x00149195
	// (set) Token: 0x06002D09 RID: 11529 RVA: 0x0014AD9D File Offset: 0x0014919D
	public UIFont bitmapFont
	{
		get
		{
			return this.mFont;
		}
		set
		{
			if (this.mFont != value)
			{
				base.RemoveFromPanel();
				this.mFont = value;
				this.mTrueTypeFont = null;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06002D0A RID: 11530 RVA: 0x0014ADCA File Offset: 0x001491CA
	// (set) Token: 0x06002D0B RID: 11531 RVA: 0x0014AE08 File Offset: 0x00149208
	public Font trueTypeFont
	{
		get
		{
			if (this.mTrueTypeFont != null)
			{
				return this.mTrueTypeFont;
			}
			return (!(this.mFont != null)) ? null : this.mFont.dynamicFont;
		}
		set
		{
			if (this.mTrueTypeFont != value)
			{
				this.SetActiveFont(null);
				base.RemoveFromPanel();
				this.mTrueTypeFont = value;
				this.shouldBeProcessed = true;
				this.mFont = null;
				this.SetActiveFont(value);
				this.ProcessAndRequest();
				if (this.mActiveTTF != null)
				{
					base.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x06002D0C RID: 11532 RVA: 0x0014AE6C File Offset: 0x0014926C
	// (set) Token: 0x06002D0D RID: 11533 RVA: 0x0014AE84 File Offset: 0x00149284
	public UnityEngine.Object ambigiousFont
	{
		get
		{
			return this.mFont ?? this.mTrueTypeFont;
		}
		set
		{
			UIFont uifont = value as UIFont;
			if (uifont != null)
			{
				this.bitmapFont = uifont;
			}
			else
			{
				this.trueTypeFont = (value as Font);
			}
		}
	}

	// Token: 0x17000310 RID: 784
	// (get) Token: 0x06002D0E RID: 11534 RVA: 0x0014AEBC File Offset: 0x001492BC
	// (set) Token: 0x06002D0F RID: 11535 RVA: 0x0014AEC4 File Offset: 0x001492C4
	public string text
	{
		get
		{
			return this.mText;
		}
		set
		{
			if (this.mText == value)
			{
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				if (!string.IsNullOrEmpty(this.mText))
				{
					this.mText = string.Empty;
					this.MarkAsChanged();
					this.ProcessAndRequest();
					if (this.autoResizeBoxCollider)
					{
						base.ResizeCollider();
					}
				}
			}
			else if (this.mText != value)
			{
				this.mText = value;
				this.MarkAsChanged();
				this.ProcessAndRequest();
				if (this.autoResizeBoxCollider)
				{
					base.ResizeCollider();
				}
			}
		}
	}

	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06002D10 RID: 11536 RVA: 0x0014AF60 File Offset: 0x00149360
	public int defaultFontSize
	{
		get
		{
			return (!(this.trueTypeFont != null)) ? ((!(this.mFont != null)) ? 16 : this.mFont.defaultSize) : this.mFontSize;
		}
	}

	// Token: 0x17000312 RID: 786
	// (get) Token: 0x06002D11 RID: 11537 RVA: 0x0014AFAC File Offset: 0x001493AC
	// (set) Token: 0x06002D12 RID: 11538 RVA: 0x0014AFB4 File Offset: 0x001493B4
	public int fontSize
	{
		get
		{
			return this.mFontSize;
		}
		set
		{
			value = Mathf.Clamp(value, 0, 256);
			if (this.mFontSize != value)
			{
				this.mFontSize = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000313 RID: 787
	// (get) Token: 0x06002D13 RID: 11539 RVA: 0x0014AFE4 File Offset: 0x001493E4
	// (set) Token: 0x06002D14 RID: 11540 RVA: 0x0014AFEC File Offset: 0x001493EC
	public FontStyle fontStyle
	{
		get
		{
			return this.mFontStyle;
		}
		set
		{
			if (this.mFontStyle != value)
			{
				this.mFontStyle = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000314 RID: 788
	// (get) Token: 0x06002D15 RID: 11541 RVA: 0x0014B00E File Offset: 0x0014940E
	// (set) Token: 0x06002D16 RID: 11542 RVA: 0x0014B016 File Offset: 0x00149416
	public NGUIText.Alignment alignment
	{
		get
		{
			return this.mAlignment;
		}
		set
		{
			if (this.mAlignment != value)
			{
				this.mAlignment = value;
				this.shouldBeProcessed = true;
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x17000315 RID: 789
	// (get) Token: 0x06002D17 RID: 11543 RVA: 0x0014B038 File Offset: 0x00149438
	// (set) Token: 0x06002D18 RID: 11544 RVA: 0x0014B040 File Offset: 0x00149440
	public bool applyGradient
	{
		get
		{
			return this.mApplyGradient;
		}
		set
		{
			if (this.mApplyGradient != value)
			{
				this.mApplyGradient = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000316 RID: 790
	// (get) Token: 0x06002D19 RID: 11545 RVA: 0x0014B05B File Offset: 0x0014945B
	// (set) Token: 0x06002D1A RID: 11546 RVA: 0x0014B063 File Offset: 0x00149463
	public Color gradientTop
	{
		get
		{
			return this.mGradientTop;
		}
		set
		{
			if (this.mGradientTop != value)
			{
				this.mGradientTop = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000317 RID: 791
	// (get) Token: 0x06002D1B RID: 11547 RVA: 0x0014B08E File Offset: 0x0014948E
	// (set) Token: 0x06002D1C RID: 11548 RVA: 0x0014B096 File Offset: 0x00149496
	public Color gradientBottom
	{
		get
		{
			return this.mGradientBottom;
		}
		set
		{
			if (this.mGradientBottom != value)
			{
				this.mGradientBottom = value;
				if (this.mApplyGradient)
				{
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x06002D1D RID: 11549 RVA: 0x0014B0C1 File Offset: 0x001494C1
	// (set) Token: 0x06002D1E RID: 11550 RVA: 0x0014B0C9 File Offset: 0x001494C9
	public int spacingX
	{
		get
		{
			return this.mSpacingX;
		}
		set
		{
			if (this.mSpacingX != value)
			{
				this.mSpacingX = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06002D1F RID: 11551 RVA: 0x0014B0E4 File Offset: 0x001494E4
	// (set) Token: 0x06002D20 RID: 11552 RVA: 0x0014B0EC File Offset: 0x001494EC
	public int spacingY
	{
		get
		{
			return this.mSpacingY;
		}
		set
		{
			if (this.mSpacingY != value)
			{
				this.mSpacingY = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06002D21 RID: 11553 RVA: 0x0014B107 File Offset: 0x00149507
	// (set) Token: 0x06002D22 RID: 11554 RVA: 0x0014B10F File Offset: 0x0014950F
	public bool useFloatSpacing
	{
		get
		{
			return this.mUseFloatSpacing;
		}
		set
		{
			if (this.mUseFloatSpacing != value)
			{
				this.mUseFloatSpacing = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x1700031B RID: 795
	// (get) Token: 0x06002D23 RID: 11555 RVA: 0x0014B12B File Offset: 0x0014952B
	// (set) Token: 0x06002D24 RID: 11556 RVA: 0x0014B133 File Offset: 0x00149533
	public float floatSpacingX
	{
		get
		{
			return this.mFloatSpacingX;
		}
		set
		{
			if (!Mathf.Approximately(this.mFloatSpacingX, value))
			{
				this.mFloatSpacingX = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06002D25 RID: 11557 RVA: 0x0014B153 File Offset: 0x00149553
	// (set) Token: 0x06002D26 RID: 11558 RVA: 0x0014B15B File Offset: 0x0014955B
	public float floatSpacingY
	{
		get
		{
			return this.mFloatSpacingY;
		}
		set
		{
			if (!Mathf.Approximately(this.mFloatSpacingY, value))
			{
				this.mFloatSpacingY = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06002D27 RID: 11559 RVA: 0x0014B17B File Offset: 0x0014957B
	public float effectiveSpacingY
	{
		get
		{
			return (!this.mUseFloatSpacing) ? ((float)this.mSpacingY) : this.mFloatSpacingY;
		}
	}

	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06002D28 RID: 11560 RVA: 0x0014B19A File Offset: 0x0014959A
	public float effectiveSpacingX
	{
		get
		{
			return (!this.mUseFloatSpacing) ? ((float)this.mSpacingX) : this.mFloatSpacingX;
		}
	}

	// Token: 0x1700031F RID: 799
	// (get) Token: 0x06002D29 RID: 11561 RVA: 0x0014B1B9 File Offset: 0x001495B9
	// (set) Token: 0x06002D2A RID: 11562 RVA: 0x0014B1C1 File Offset: 0x001495C1
	public bool overflowEllipsis
	{
		get
		{
			return this.mOverflowEllipsis;
		}
		set
		{
			if (this.mOverflowEllipsis != value)
			{
				this.mOverflowEllipsis = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06002D2B RID: 11563 RVA: 0x0014B1DC File Offset: 0x001495DC
	// (set) Token: 0x06002D2C RID: 11564 RVA: 0x0014B1E4 File Offset: 0x001495E4
	public int overflowWidth
	{
		get
		{
			return this.mOverflowWidth;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (this.mOverflowWidth != value)
			{
				this.mOverflowWidth = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000321 RID: 801
	// (get) Token: 0x06002D2D RID: 11565 RVA: 0x0014B209 File Offset: 0x00149609
	// (set) Token: 0x06002D2E RID: 11566 RVA: 0x0014B211 File Offset: 0x00149611
	public int overflowHeight
	{
		get
		{
			return this.mOverflowHeight;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			if (this.mOverflowHeight != value)
			{
				this.mOverflowHeight = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06002D2F RID: 11567 RVA: 0x0014B236 File Offset: 0x00149636
	private bool keepCrisp
	{
		get
		{
			return this.trueTypeFont != null && this.keepCrispWhenShrunk != UILabel.Crispness.Never && this.keepCrispWhenShrunk == UILabel.Crispness.Always;
		}
	}

	// Token: 0x17000323 RID: 803
	// (get) Token: 0x06002D30 RID: 11568 RVA: 0x0014B25F File Offset: 0x0014965F
	// (set) Token: 0x06002D31 RID: 11569 RVA: 0x0014B267 File Offset: 0x00149667
	public bool supportEncoding
	{
		get
		{
			return this.mEncoding;
		}
		set
		{
			if (this.mEncoding != value)
			{
				this.mEncoding = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000324 RID: 804
	// (get) Token: 0x06002D32 RID: 11570 RVA: 0x0014B283 File Offset: 0x00149683
	// (set) Token: 0x06002D33 RID: 11571 RVA: 0x0014B28B File Offset: 0x0014968B
	public NGUIText.SymbolStyle symbolStyle
	{
		get
		{
			return this.mSymbols;
		}
		set
		{
			if (this.mSymbols != value)
			{
				this.mSymbols = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000325 RID: 805
	// (get) Token: 0x06002D34 RID: 11572 RVA: 0x0014B2A7 File Offset: 0x001496A7
	// (set) Token: 0x06002D35 RID: 11573 RVA: 0x0014B2AF File Offset: 0x001496AF
	public UILabel.Overflow overflowMethod
	{
		get
		{
			return this.mOverflow;
		}
		set
		{
			if (this.mOverflow != value)
			{
				this.mOverflow = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000326 RID: 806
	// (get) Token: 0x06002D36 RID: 11574 RVA: 0x0014B2CB File Offset: 0x001496CB
	// (set) Token: 0x06002D37 RID: 11575 RVA: 0x0014B2D3 File Offset: 0x001496D3
	[Obsolete("Use 'width' instead")]
	public int lineWidth
	{
		get
		{
			return base.width;
		}
		set
		{
			base.width = value;
		}
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x06002D38 RID: 11576 RVA: 0x0014B2DC File Offset: 0x001496DC
	// (set) Token: 0x06002D39 RID: 11577 RVA: 0x0014B2E4 File Offset: 0x001496E4
	[Obsolete("Use 'height' instead")]
	public int lineHeight
	{
		get
		{
			return base.height;
		}
		set
		{
			base.height = value;
		}
	}

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x06002D3A RID: 11578 RVA: 0x0014B2ED File Offset: 0x001496ED
	// (set) Token: 0x06002D3B RID: 11579 RVA: 0x0014B2FB File Offset: 0x001496FB
	public bool multiLine
	{
		get
		{
			return this.mMaxLineCount != 1;
		}
		set
		{
			if (this.mMaxLineCount != 1 != value)
			{
				this.mMaxLineCount = ((!value) ? 1 : 0);
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x06002D3C RID: 11580 RVA: 0x0014B329 File Offset: 0x00149729
	public override Vector3[] localCorners
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.localCorners;
		}
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x06002D3D RID: 11581 RVA: 0x0014B344 File Offset: 0x00149744
	public override Vector3[] worldCorners
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.worldCorners;
		}
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x06002D3E RID: 11582 RVA: 0x0014B35F File Offset: 0x0014975F
	public override Vector4 drawingDimensions
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.drawingDimensions;
		}
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x06002D3F RID: 11583 RVA: 0x0014B37A File Offset: 0x0014977A
	// (set) Token: 0x06002D40 RID: 11584 RVA: 0x0014B382 File Offset: 0x00149782
	public int maxLineCount
	{
		get
		{
			return this.mMaxLineCount;
		}
		set
		{
			if (this.mMaxLineCount != value)
			{
				this.mMaxLineCount = Mathf.Max(value, 0);
				this.shouldBeProcessed = true;
				if (this.overflowMethod == UILabel.Overflow.ShrinkContent)
				{
					this.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06002D41 RID: 11585 RVA: 0x0014B3B5 File Offset: 0x001497B5
	// (set) Token: 0x06002D42 RID: 11586 RVA: 0x0014B3BD File Offset: 0x001497BD
	public UILabel.Effect effectStyle
	{
		get
		{
			return this.mEffectStyle;
		}
		set
		{
			if (this.mEffectStyle != value)
			{
				this.mEffectStyle = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x06002D43 RID: 11587 RVA: 0x0014B3D9 File Offset: 0x001497D9
	// (set) Token: 0x06002D44 RID: 11588 RVA: 0x0014B3E1 File Offset: 0x001497E1
	public Color effectColor
	{
		get
		{
			return this.mEffectColor;
		}
		set
		{
			if (this.mEffectColor != value)
			{
				this.mEffectColor = value;
				if (this.mEffectStyle != UILabel.Effect.None)
				{
					this.shouldBeProcessed = true;
				}
			}
		}
	}

	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06002D45 RID: 11589 RVA: 0x0014B40D File Offset: 0x0014980D
	// (set) Token: 0x06002D46 RID: 11590 RVA: 0x0014B415 File Offset: 0x00149815
	public Vector2 effectDistance
	{
		get
		{
			return this.mEffectDistance;
		}
		set
		{
			if (this.mEffectDistance != value)
			{
				this.mEffectDistance = value;
				this.shouldBeProcessed = true;
			}
		}
	}

	// Token: 0x17000330 RID: 816
	// (get) Token: 0x06002D47 RID: 11591 RVA: 0x0014B436 File Offset: 0x00149836
	public int quadsPerCharacter
	{
		get
		{
			if (this.mEffectStyle == UILabel.Effect.Shadow)
			{
				return 2;
			}
			if (this.mEffectStyle == UILabel.Effect.Outline)
			{
				return 5;
			}
			if (this.mEffectStyle == UILabel.Effect.Outline8)
			{
				return 9;
			}
			return 1;
		}
	}

	// Token: 0x17000331 RID: 817
	// (get) Token: 0x06002D48 RID: 11592 RVA: 0x0014B464 File Offset: 0x00149864
	// (set) Token: 0x06002D49 RID: 11593 RVA: 0x0014B46F File Offset: 0x0014986F
	[Obsolete("Use 'overflowMethod == UILabel.Overflow.ShrinkContent' instead")]
	public bool shrinkToFit
	{
		get
		{
			return this.mOverflow == UILabel.Overflow.ShrinkContent;
		}
		set
		{
			if (value)
			{
				this.overflowMethod = UILabel.Overflow.ShrinkContent;
			}
		}
	}

	// Token: 0x17000332 RID: 818
	// (get) Token: 0x06002D4A RID: 11594 RVA: 0x0014B480 File Offset: 0x00149880
	public string processedText
	{
		get
		{
			if (this.mLastWidth != this.mWidth || this.mLastHeight != this.mHeight)
			{
				this.mLastWidth = this.mWidth;
				this.mLastHeight = this.mHeight;
				this.mShouldBeProcessed = true;
			}
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return this.mProcessedText;
		}
	}

	// Token: 0x17000333 RID: 819
	// (get) Token: 0x06002D4B RID: 11595 RVA: 0x0014B4E7 File Offset: 0x001498E7
	public Vector2 printedSize
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return this.mCalculatedSize;
		}
	}

	// Token: 0x17000334 RID: 820
	// (get) Token: 0x06002D4C RID: 11596 RVA: 0x0014B502 File Offset: 0x00149902
	public override Vector2 localSize
	{
		get
		{
			if (this.shouldBeProcessed)
			{
				this.ProcessText(false, true);
			}
			return base.localSize;
		}
	}

	// Token: 0x17000335 RID: 821
	// (get) Token: 0x06002D4D RID: 11597 RVA: 0x0014B51D File Offset: 0x0014991D
	private bool isValid
	{
		get
		{
			return this.mFont != null || this.mTrueTypeFont != null;
		}
	}

	// Token: 0x17000336 RID: 822
	// (get) Token: 0x06002D4E RID: 11598 RVA: 0x0014B53F File Offset: 0x0014993F
	// (set) Token: 0x06002D4F RID: 11599 RVA: 0x0014B547 File Offset: 0x00149947
	public UILabel.Modifier modifier
	{
		get
		{
			return this.mModifier;
		}
		set
		{
			if (this.mModifier != value)
			{
				this.mModifier = value;
				this.MarkAsChanged();
				this.ProcessAndRequest();
			}
		}
	}

	// Token: 0x06002D50 RID: 11600 RVA: 0x0014B568 File Offset: 0x00149968
	protected override void OnInit()
	{
		base.OnInit();
		UILabel.mList.Add(this);
		this.SetActiveFont(this.trueTypeFont);
	}

	// Token: 0x06002D51 RID: 11601 RVA: 0x0014B587 File Offset: 0x00149987
	protected override void OnDisable()
	{
		this.SetActiveFont(null);
		UILabel.mList.Remove(this);
		base.OnDisable();
	}

	// Token: 0x06002D52 RID: 11602 RVA: 0x0014B5A4 File Offset: 0x001499A4
	protected void SetActiveFont(Font fnt)
	{
		if (this.mActiveTTF != fnt)
		{
			Font font = this.mActiveTTF;
			int num;
			if (font != null && UILabel.mFontUsage.TryGetValue(font, out num))
			{
				num = Mathf.Max(0, --num);
				if (num == 0)
				{
					UILabel.mFontUsage.Remove(font);
				}
				else
				{
					UILabel.mFontUsage[font] = num;
				}
			}
			this.mActiveTTF = fnt;
			if (fnt != null)
			{
				int num2 = 0;
				UILabel.mFontUsage[fnt] = num2 + 1;
			}
		}
	}

	// Token: 0x17000337 RID: 823
	// (get) Token: 0x06002D53 RID: 11603 RVA: 0x0014B640 File Offset: 0x00149A40
	public string printedText
	{
		get
		{
			if (!string.IsNullOrEmpty(this.mText))
			{
				if (this.mModifier == UILabel.Modifier.None)
				{
					return this.mText;
				}
				if (this.mModifier == UILabel.Modifier.ToLowercase)
				{
					return this.mText.ToLower();
				}
				if (this.mModifier == UILabel.Modifier.ToUppercase)
				{
					return this.mText.ToUpper();
				}
				if (this.mModifier == UILabel.Modifier.Custom && this.customModifier != null)
				{
					return this.customModifier(this.mText);
				}
			}
			return this.mText;
		}
	}

	// Token: 0x06002D54 RID: 11604 RVA: 0x0014B6D4 File Offset: 0x00149AD4
	private static void OnFontChanged(Font font)
	{
		for (int i = 0; i < UILabel.mList.size; i++)
		{
			UILabel uilabel = UILabel.mList[i];
			if (uilabel != null)
			{
				Font trueTypeFont = uilabel.trueTypeFont;
				if (trueTypeFont == font)
				{
					trueTypeFont.RequestCharactersInTexture(uilabel.mText, uilabel.mFinalFontSize, uilabel.mFontStyle);
					uilabel.MarkAsChanged();
					if (uilabel.panel == null)
					{
						uilabel.CreatePanel();
					}
					if (UILabel.mTempDrawcalls == null)
					{
						UILabel.mTempDrawcalls = new BetterList<UIDrawCall>();
					}
					if (uilabel.drawCall != null && !UILabel.mTempDrawcalls.Contains(uilabel.drawCall))
					{
						UILabel.mTempDrawcalls.Add(uilabel.drawCall);
					}
				}
			}
		}
		if (UILabel.mTempDrawcalls != null)
		{
			int j = 0;
			int size = UILabel.mTempDrawcalls.size;
			while (j < size)
			{
				UIDrawCall uidrawCall = UILabel.mTempDrawcalls[j];
				if (uidrawCall.panel != null)
				{
					uidrawCall.panel.FillDrawCall(uidrawCall);
				}
				j++;
			}
			UILabel.mTempDrawcalls.Clear();
		}
	}

	// Token: 0x06002D55 RID: 11605 RVA: 0x0014B808 File Offset: 0x00149C08
	public override Vector3[] GetSides(Transform relativeTo)
	{
		if (this.shouldBeProcessed)
		{
			this.ProcessText(false, true);
		}
		return base.GetSides(relativeTo);
	}

	// Token: 0x06002D56 RID: 11606 RVA: 0x0014B824 File Offset: 0x00149C24
	protected override void UpgradeFrom265()
	{
		this.ProcessText(true, true);
		if (this.mShrinkToFit)
		{
			this.overflowMethod = UILabel.Overflow.ShrinkContent;
			this.mMaxLineCount = 0;
		}
		if (this.mMaxLineWidth != 0)
		{
			base.width = this.mMaxLineWidth;
			this.overflowMethod = ((this.mMaxLineCount <= 0) ? UILabel.Overflow.ShrinkContent : UILabel.Overflow.ResizeHeight);
		}
		else
		{
			this.overflowMethod = UILabel.Overflow.ResizeFreely;
		}
		if (this.mMaxLineHeight != 0)
		{
			base.height = this.mMaxLineHeight;
		}
		if (this.mFont != null)
		{
			int defaultSize = this.mFont.defaultSize;
			if (base.height < defaultSize)
			{
				base.height = defaultSize;
			}
			this.fontSize = defaultSize;
		}
		this.mMaxLineWidth = 0;
		this.mMaxLineHeight = 0;
		this.mShrinkToFit = false;
		NGUITools.UpdateWidgetCollider(base.gameObject, true);
	}

	// Token: 0x06002D57 RID: 11607 RVA: 0x0014B900 File Offset: 0x00149D00
	protected override void OnAnchor()
	{
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			if (base.isFullyAnchored)
			{
				this.mOverflow = UILabel.Overflow.ShrinkContent;
			}
		}
		else if (this.mOverflow == UILabel.Overflow.ResizeHeight && this.topAnchor.target != null && this.bottomAnchor.target != null)
		{
			this.mOverflow = UILabel.Overflow.ShrinkContent;
		}
		base.OnAnchor();
	}

	// Token: 0x06002D58 RID: 11608 RVA: 0x0014B975 File Offset: 0x00149D75
	private void ProcessAndRequest()
	{
		if (this.ambigiousFont != null)
		{
			this.ProcessText(false, true);
		}
	}

	// Token: 0x06002D59 RID: 11609 RVA: 0x0014B990 File Offset: 0x00149D90
	protected override void OnEnable()
	{
		base.OnEnable();
		if (!UILabel.mTexRebuildAdded)
		{
			UILabel.mTexRebuildAdded = true;
			if (UILabel.<>f__mg$cache0 == null)
			{
				UILabel.<>f__mg$cache0 = new Action<Font>(UILabel.OnFontChanged);
			}
			Font.textureRebuilt += UILabel.<>f__mg$cache0;
		}
	}

	// Token: 0x06002D5A RID: 11610 RVA: 0x0014B9CC File Offset: 0x00149DCC
	protected override void OnStart()
	{
		base.OnStart();
		if (this.mLineWidth > 0f)
		{
			this.mMaxLineWidth = Mathf.RoundToInt(this.mLineWidth);
			this.mLineWidth = 0f;
		}
		if (!this.mMultiline)
		{
			this.mMaxLineCount = 1;
			this.mMultiline = true;
		}
		this.mPremultiply = (this.material != null && this.material.shader != null && this.material.shader.name.Contains("Premultiplied"));
		this.ProcessAndRequest();
	}

	// Token: 0x06002D5B RID: 11611 RVA: 0x0014BA74 File Offset: 0x00149E74
	public override void MarkAsChanged()
	{
		this.shouldBeProcessed = true;
		base.MarkAsChanged();
	}

	// Token: 0x06002D5C RID: 11612 RVA: 0x0014BA84 File Offset: 0x00149E84
	public void ProcessText(bool legacyMode = false, bool full = true)
	{
		if (!this.isValid)
		{
			return;
		}
		this.mChanged = true;
		this.shouldBeProcessed = false;
		float num = this.mDrawRegion.z - this.mDrawRegion.x;
		float num2 = this.mDrawRegion.w - this.mDrawRegion.y;
		NGUIText.rectWidth = ((!legacyMode) ? base.width : ((this.mMaxLineWidth == 0) ? 1000000 : this.mMaxLineWidth));
		NGUIText.rectHeight = ((!legacyMode) ? base.height : ((this.mMaxLineHeight == 0) ? 1000000 : this.mMaxLineHeight));
		NGUIText.regionWidth = ((num == 1f) ? NGUIText.rectWidth : Mathf.RoundToInt((float)NGUIText.rectWidth * num));
		NGUIText.regionHeight = ((num2 == 1f) ? NGUIText.rectHeight : Mathf.RoundToInt((float)NGUIText.rectHeight * num2));
		this.mFinalFontSize = Mathf.Abs((!legacyMode) ? this.defaultFontSize : Mathf.RoundToInt(base.cachedTransform.localScale.x));
		this.mScale = 1f;
		if (NGUIText.regionWidth < 1 || NGUIText.regionHeight < 0)
		{
			this.mProcessedText = string.Empty;
			return;
		}
		bool flag = this.trueTypeFont != null;
		if (flag && this.keepCrisp)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				this.mDensity = ((!(root != null)) ? 1f : root.pixelSizeAdjustment);
			}
		}
		else
		{
			this.mDensity = 1f;
		}
		if (full)
		{
			this.UpdateNGUIText();
		}
		if (this.mOverflow == UILabel.Overflow.ResizeFreely)
		{
			if (this.mOverflowWidth > 0)
			{
				NGUIText.rectWidth = this.mOverflowWidth;
				NGUIText.regionWidth = this.mOverflowWidth;
			}
			else
			{
				NGUIText.rectWidth = 1000000;
				NGUIText.regionWidth = 1000000;
			}
			if (this.mOverflowHeight > 0)
			{
				NGUIText.rectHeight = this.mOverflowHeight;
				NGUIText.regionHeight = this.mOverflowHeight;
			}
			else
			{
				NGUIText.rectHeight = 1000000;
				NGUIText.regionHeight = 1000000;
			}
		}
		else if (this.mOverflow == UILabel.Overflow.ResizeFreely || this.mOverflow == UILabel.Overflow.ResizeHeight)
		{
			NGUIText.rectHeight = 1000000;
			NGUIText.regionHeight = 1000000;
		}
		if (this.mFinalFontSize > 0)
		{
			bool keepCrisp = this.keepCrisp;
			for (int i = this.mFinalFontSize; i > 0; i--)
			{
				if (keepCrisp)
				{
					this.mFinalFontSize = i;
					NGUIText.fontSize = this.mFinalFontSize;
				}
				else
				{
					this.mScale = (float)i / (float)this.mFinalFontSize;
					NGUIText.fontScale = ((!flag) ? ((float)this.mFontSize / (float)this.mFont.defaultSize * this.mScale) : this.mScale);
				}
				NGUIText.Update(false);
				bool flag2 = NGUIText.WrapText(this.printedText, out this.mProcessedText, false, false, this.mOverflow == UILabel.Overflow.ClampContent && this.mOverflowEllipsis);
				if (this.mOverflow != UILabel.Overflow.ShrinkContent || flag2)
				{
					if (this.mOverflow == UILabel.Overflow.ResizeFreely)
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
						if (!flag2 && this.mOverflowWidth > 0)
						{
							if (--i > 1)
							{
								goto IL_559;
							}
							break;
						}
						else
						{
							int num3 = Mathf.Max(this.minWidth, Mathf.RoundToInt(this.mCalculatedSize.x));
							if (num != 1f)
							{
								num3 = Mathf.RoundToInt((float)num3 / num);
							}
							int num4 = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
							if (num2 != 1f)
							{
								num4 = Mathf.RoundToInt((float)num4 / num2);
							}
							if ((num3 & 1) == 1)
							{
								num3++;
							}
							if ((num4 & 1) == 1)
							{
								num4++;
							}
							if (this.mWidth != num3 || this.mHeight != num4)
							{
								this.mWidth = num3;
								this.mHeight = num4;
								if (this.onChange != null)
								{
									this.onChange();
								}
							}
						}
					}
					else if (this.mOverflow == UILabel.Overflow.ResizeHeight)
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
						int num5 = Mathf.Max(this.minHeight, Mathf.RoundToInt(this.mCalculatedSize.y));
						if (num2 != 1f)
						{
							num5 = Mathf.RoundToInt((float)num5 / num2);
						}
						if ((num5 & 1) == 1)
						{
							num5++;
						}
						if (this.mHeight != num5)
						{
							this.mHeight = num5;
							if (this.onChange != null)
							{
								this.onChange();
							}
						}
					}
					else
					{
						this.mCalculatedSize = NGUIText.CalculatePrintedSize(this.mProcessedText);
					}
					if (legacyMode)
					{
						base.width = Mathf.RoundToInt(this.mCalculatedSize.x);
						base.height = Mathf.RoundToInt(this.mCalculatedSize.y);
						base.cachedTransform.localScale = Vector3.one;
					}
					break;
				}
				if (--i <= 1)
				{
					break;
				}
				IL_559:;
			}
		}
		else
		{
			base.cachedTransform.localScale = Vector3.one;
			this.mProcessedText = string.Empty;
			this.mScale = 1f;
		}
		if (full)
		{
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
	}

	// Token: 0x06002D5D RID: 11613 RVA: 0x0014C038 File Offset: 0x0014A438
	public override void MakePixelPerfect()
	{
		if (this.ambigiousFont != null)
		{
			Vector3 localPosition = base.cachedTransform.localPosition;
			localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
			localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
			localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
			base.cachedTransform.localPosition = localPosition;
			base.cachedTransform.localScale = Vector3.one;
			if (this.mOverflow == UILabel.Overflow.ResizeFreely)
			{
				this.AssumeNaturalSize();
			}
			else
			{
				int width = base.width;
				int height = base.height;
				UILabel.Overflow overflow = this.mOverflow;
				if (overflow != UILabel.Overflow.ResizeHeight)
				{
					this.mWidth = 100000;
				}
				this.mHeight = 100000;
				this.mOverflow = UILabel.Overflow.ShrinkContent;
				this.ProcessText(false, true);
				this.mOverflow = overflow;
				int num = Mathf.RoundToInt(this.mCalculatedSize.x);
				int num2 = Mathf.RoundToInt(this.mCalculatedSize.y);
				num = Mathf.Max(num, base.minWidth);
				num2 = Mathf.Max(num2, base.minHeight);
				if ((num & 1) == 1)
				{
					num++;
				}
				if ((num2 & 1) == 1)
				{
					num2++;
				}
				this.mWidth = Mathf.Max(width, num);
				this.mHeight = Mathf.Max(height, num2);
				this.MarkAsChanged();
			}
		}
		else
		{
			base.MakePixelPerfect();
		}
	}

	// Token: 0x06002D5E RID: 11614 RVA: 0x0014C1A8 File Offset: 0x0014A5A8
	public void AssumeNaturalSize()
	{
		if (this.ambigiousFont != null)
		{
			this.mWidth = 100000;
			this.mHeight = 100000;
			this.ProcessText(false, true);
			this.mWidth = Mathf.RoundToInt(this.mCalculatedSize.x);
			this.mHeight = Mathf.RoundToInt(this.mCalculatedSize.y);
			if ((this.mWidth & 1) == 1)
			{
				this.mWidth++;
			}
			if ((this.mHeight & 1) == 1)
			{
				this.mHeight++;
			}
			this.MarkAsChanged();
		}
	}

	// Token: 0x06002D5F RID: 11615 RVA: 0x0014C24E File Offset: 0x0014A64E
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector3 worldPos)
	{
		return this.GetCharacterIndexAtPosition(worldPos, false);
	}

	// Token: 0x06002D60 RID: 11616 RVA: 0x0014C258 File Offset: 0x0014A658
	[Obsolete("Use UILabel.GetCharacterAtPosition instead")]
	public int GetCharacterIndex(Vector2 localPos)
	{
		return this.GetCharacterIndexAtPosition(localPos, false);
	}

	// Token: 0x06002D61 RID: 11617 RVA: 0x0014C264 File Offset: 0x0014A664
	public int GetCharacterIndexAtPosition(Vector3 worldPos, bool precise)
	{
		Vector2 localPos = base.cachedTransform.InverseTransformPoint(worldPos);
		return this.GetCharacterIndexAtPosition(localPos, precise);
	}

	// Token: 0x06002D62 RID: 11618 RVA: 0x0014C28C File Offset: 0x0014A68C
	public int GetCharacterIndexAtPosition(Vector2 localPos, bool precise)
	{
		if (this.isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			this.UpdateNGUIText();
			if (precise)
			{
				NGUIText.PrintExactCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			}
			else
			{
				NGUIText.PrintApproximateCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			}
			if (UILabel.mTempVerts.Count > 0)
			{
				this.ApplyOffset(UILabel.mTempVerts, 0);
				int result = (!precise) ? NGUIText.GetApproximateCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, localPos) : NGUIText.GetExactCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, localPos);
				UILabel.mTempVerts.Clear();
				UILabel.mTempIndices.Clear();
				NGUIText.bitmapFont = null;
				NGUIText.dynamicFont = null;
				return result;
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
		}
		return 0;
	}

	// Token: 0x06002D63 RID: 11619 RVA: 0x0014C364 File Offset: 0x0014A764
	public string GetWordAtPosition(Vector3 worldPos)
	{
		int characterIndexAtPosition = this.GetCharacterIndexAtPosition(worldPos, true);
		return this.GetWordAtCharacterIndex(characterIndexAtPosition);
	}

	// Token: 0x06002D64 RID: 11620 RVA: 0x0014C384 File Offset: 0x0014A784
	public string GetWordAtPosition(Vector2 localPos)
	{
		int characterIndexAtPosition = this.GetCharacterIndexAtPosition(localPos, true);
		return this.GetWordAtCharacterIndex(characterIndexAtPosition);
	}

	// Token: 0x06002D65 RID: 11621 RVA: 0x0014C3A4 File Offset: 0x0014A7A4
	public string GetWordAtCharacterIndex(int characterIndex)
	{
		string printedText = this.printedText;
		if (characterIndex != -1 && characterIndex < printedText.Length)
		{
			int num = printedText.LastIndexOfAny(new char[]
			{
				' ',
				'\n'
			}, characterIndex) + 1;
			int num2 = printedText.IndexOfAny(new char[]
			{
				' ',
				'\n',
				',',
				'.'
			}, characterIndex);
			if (num2 == -1)
			{
				num2 = printedText.Length;
			}
			if (num != num2)
			{
				int num3 = num2 - num;
				if (num3 > 0)
				{
					string text = printedText.Substring(num, num3);
					return NGUIText.StripSymbols(text);
				}
			}
		}
		return null;
	}

	// Token: 0x06002D66 RID: 11622 RVA: 0x0014C431 File Offset: 0x0014A831
	public string GetUrlAtPosition(Vector3 worldPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(worldPos, true));
	}

	// Token: 0x06002D67 RID: 11623 RVA: 0x0014C441 File Offset: 0x0014A841
	public string GetUrlAtPosition(Vector2 localPos)
	{
		return this.GetUrlAtCharacterIndex(this.GetCharacterIndexAtPosition(localPos, true));
	}

	// Token: 0x06002D68 RID: 11624 RVA: 0x0014C454 File Offset: 0x0014A854
	public string GetUrlAtCharacterIndex(int characterIndex)
	{
		string printedText = this.printedText;
		if (characterIndex != -1 && characterIndex < printedText.Length - 6)
		{
			int num;
			if (printedText[characterIndex] == '[' && printedText[characterIndex + 1] == 'u' && printedText[characterIndex + 2] == 'r' && printedText[characterIndex + 3] == 'l' && printedText[characterIndex + 4] == '=')
			{
				num = characterIndex;
			}
			else
			{
				num = printedText.LastIndexOf("[url=", characterIndex);
			}
			if (num == -1)
			{
				return null;
			}
			num += 5;
			int num2 = printedText.IndexOf("]", num);
			if (num2 == -1)
			{
				return null;
			}
			int num3 = printedText.IndexOf("[/url]", num2);
			if (num3 == -1 || characterIndex <= num3)
			{
				return printedText.Substring(num, num2 - num);
			}
		}
		return null;
	}

	// Token: 0x06002D69 RID: 11625 RVA: 0x0014C52C File Offset: 0x0014A92C
	public int GetCharacterIndex(int currentIndex, KeyCode key)
	{
		if (this.isValid)
		{
			string processedText = this.processedText;
			if (string.IsNullOrEmpty(processedText))
			{
				return 0;
			}
			int defaultFontSize = this.defaultFontSize;
			this.UpdateNGUIText();
			NGUIText.PrintApproximateCharacterPositions(processedText, UILabel.mTempVerts, UILabel.mTempIndices);
			if (UILabel.mTempVerts.Count > 0)
			{
				this.ApplyOffset(UILabel.mTempVerts, 0);
				int i = 0;
				int count = UILabel.mTempIndices.Count;
				while (i < count)
				{
					if (UILabel.mTempIndices[i] == currentIndex)
					{
						Vector2 pos = UILabel.mTempVerts[i];
						if (key == KeyCode.UpArrow)
						{
							pos.y += (float)defaultFontSize + this.effectiveSpacingY;
						}
						else if (key == KeyCode.DownArrow)
						{
							pos.y -= (float)defaultFontSize + this.effectiveSpacingY;
						}
						else if (key == KeyCode.Home)
						{
							pos.x -= 1000f;
						}
						else if (key == KeyCode.End)
						{
							pos.x += 1000f;
						}
						int approximateCharacterIndex = NGUIText.GetApproximateCharacterIndex(UILabel.mTempVerts, UILabel.mTempIndices, pos);
						if (approximateCharacterIndex == currentIndex)
						{
							break;
						}
						UILabel.mTempVerts.Clear();
						UILabel.mTempIndices.Clear();
						return approximateCharacterIndex;
					}
					else
					{
						i++;
					}
				}
				UILabel.mTempVerts.Clear();
				UILabel.mTempIndices.Clear();
			}
			NGUIText.bitmapFont = null;
			NGUIText.dynamicFont = null;
			if (key == KeyCode.UpArrow || key == KeyCode.Home)
			{
				return 0;
			}
			if (key == KeyCode.DownArrow || key == KeyCode.End)
			{
				return processedText.Length;
			}
		}
		return currentIndex;
	}

	// Token: 0x06002D6A RID: 11626 RVA: 0x0014C6F0 File Offset: 0x0014AAF0
	public void PrintOverlay(int start, int end, UIGeometry caret, UIGeometry highlight, Color caretColor, Color highlightColor)
	{
		if (caret != null)
		{
			caret.Clear();
		}
		if (highlight != null)
		{
			highlight.Clear();
		}
		if (!this.isValid)
		{
			return;
		}
		string processedText = this.processedText;
		this.UpdateNGUIText();
		int count = caret.verts.Count;
		Vector2 item = new Vector2(0.5f, 0.5f);
		float finalAlpha = this.finalAlpha;
		if (highlight != null && start != end)
		{
			int count2 = highlight.verts.Count;
			NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, highlight.verts);
			if (highlight.verts.Count > count2)
			{
				this.ApplyOffset(highlight.verts, count2);
				Color item2 = new Color(highlightColor.r, highlightColor.g, highlightColor.b, highlightColor.a * finalAlpha);
				int i = count2;
				int count3 = highlight.verts.Count;
				while (i < count3)
				{
					highlight.uvs.Add(item);
					highlight.cols.Add(item2);
					i++;
				}
			}
		}
		else
		{
			NGUIText.PrintCaretAndSelection(processedText, start, end, caret.verts, null);
		}
		this.ApplyOffset(caret.verts, count);
		Color item3 = new Color(caretColor.r, caretColor.g, caretColor.b, caretColor.a * finalAlpha);
		int j = count;
		int count4 = caret.verts.Count;
		while (j < count4)
		{
			caret.uvs.Add(item);
			caret.cols.Add(item3);
			j++;
		}
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
	}

	// Token: 0x06002D6B RID: 11627 RVA: 0x0014C8A0 File Offset: 0x0014ACA0
	public override void OnFill(List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
		if (!this.isValid)
		{
			return;
		}
		int num = verts.Count;
		Color color = base.color;
		color.a = this.finalAlpha;
		if (this.mFont != null && this.mFont.premultipliedAlphaShader)
		{
			color = NGUITools.ApplyPMA(color);
		}
		string processedText = this.processedText;
		int count = verts.Count;
		this.UpdateNGUIText();
		NGUIText.tint = color;
		NGUIText.Print(processedText, verts, uvs, cols);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		Vector2 vector = this.ApplyOffset(verts, count);
		if (this.mFont != null && this.mFont.packedFontShader)
		{
			return;
		}
		if (this.effectStyle != UILabel.Effect.None)
		{
			int count2 = verts.Count;
			vector.x = this.mEffectDistance.x;
			vector.y = this.mEffectDistance.y;
			this.ApplyShadow(verts, uvs, cols, num, count2, vector.x, -vector.y);
			if (this.effectStyle == UILabel.Effect.Outline || this.effectStyle == UILabel.Effect.Outline8)
			{
				num = count2;
				count2 = verts.Count;
				this.ApplyShadow(verts, uvs, cols, num, count2, -vector.x, vector.y);
				num = count2;
				count2 = verts.Count;
				this.ApplyShadow(verts, uvs, cols, num, count2, vector.x, vector.y);
				num = count2;
				count2 = verts.Count;
				this.ApplyShadow(verts, uvs, cols, num, count2, -vector.x, -vector.y);
				if (this.effectStyle == UILabel.Effect.Outline8)
				{
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, -vector.x, 0f);
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, vector.x, 0f);
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, 0f, vector.y);
					num = count2;
					count2 = verts.Count;
					this.ApplyShadow(verts, uvs, cols, num, count2, 0f, -vector.y);
				}
			}
		}
		if (NGUIText.symbolStyle == NGUIText.SymbolStyle.NoOutline)
		{
			int i = 0;
			int count3 = cols.Count;
			while (i < count3)
			{
				if (cols[i].r == -1f)
				{
					cols[i] = Color.white;
				}
				i++;
			}
		}
		if (this.onPostFill != null)
		{
			this.onPostFill(this, num, verts, uvs, cols);
		}
	}

	// Token: 0x06002D6C RID: 11628 RVA: 0x0014CB3C File Offset: 0x0014AF3C
	public Vector2 ApplyOffset(List<Vector3> verts, int start)
	{
		Vector2 pivotOffset = base.pivotOffset;
		float num = Mathf.Lerp(0f, (float)(-(float)this.mWidth), pivotOffset.x);
		float num2 = Mathf.Lerp((float)this.mHeight, 0f, pivotOffset.y) + Mathf.Lerp(this.mCalculatedSize.y - (float)this.mHeight, 0f, pivotOffset.y);
		num = Mathf.Round(num);
		num2 = Mathf.Round(num2);
		int i = start;
		int count = verts.Count;
		while (i < count)
		{
			Vector3 value = verts[i];
			value.x += num;
			value.y += num2;
			verts[i] = value;
			i++;
		}
		return new Vector2(num, num2);
	}

	// Token: 0x06002D6D RID: 11629 RVA: 0x0014CC0C File Offset: 0x0014B00C
	public void ApplyShadow(List<Vector3> verts, List<Vector2> uvs, List<Color> cols, int start, int end, float x, float y)
	{
		Color color = this.mEffectColor;
		color.a *= this.finalAlpha;
		if (this.bitmapFont != null && this.bitmapFont.premultipliedAlphaShader)
		{
			color = NGUITools.ApplyPMA(color);
		}
		Color value = color;
		for (int i = start; i < end; i++)
		{
			verts.Add(verts[i]);
			uvs.Add(uvs[i]);
			cols.Add(cols[i]);
			Vector3 value2 = verts[i];
			value2.x += x;
			value2.y += y;
			verts[i] = value2;
			Color color2 = cols[i];
			if (color2.a == 1f)
			{
				cols[i] = value;
			}
			else
			{
				Color value3 = color;
				value3.a = color2.a * color.a;
				cols[i] = value3;
			}
		}
	}

	// Token: 0x06002D6E RID: 11630 RVA: 0x0014CD14 File Offset: 0x0014B114
	public int CalculateOffsetToFit(string text)
	{
		this.UpdateNGUIText();
		NGUIText.encoding = false;
		NGUIText.symbolStyle = NGUIText.SymbolStyle.None;
		int result = NGUIText.CalculateOffsetToFit(text);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	// Token: 0x06002D6F RID: 11631 RVA: 0x0014CD48 File Offset: 0x0014B148
	public void SetCurrentProgress()
	{
		if (UIProgressBar.current != null)
		{
			this.text = UIProgressBar.current.value.ToString("F");
		}
	}

	// Token: 0x06002D70 RID: 11632 RVA: 0x0014CD82 File Offset: 0x0014B182
	public void SetCurrentPercent()
	{
		if (UIProgressBar.current != null)
		{
			this.text = Mathf.RoundToInt(UIProgressBar.current.value * 100f) + "%";
		}
	}

	// Token: 0x06002D71 RID: 11633 RVA: 0x0014CDC0 File Offset: 0x0014B1C0
	public void SetCurrentSelection()
	{
		if (UIPopupList.current != null)
		{
			this.text = ((!UIPopupList.current.isLocalized) ? UIPopupList.current.value : Localization.Get(UIPopupList.current.value, true));
		}
	}

	// Token: 0x06002D72 RID: 11634 RVA: 0x0014CE11 File Offset: 0x0014B211
	public bool Wrap(string text, out string final)
	{
		return this.Wrap(text, out final, 1000000);
	}

	// Token: 0x06002D73 RID: 11635 RVA: 0x0014CE20 File Offset: 0x0014B220
	public bool Wrap(string text, out string final, int height)
	{
		this.UpdateNGUIText();
		NGUIText.rectHeight = height;
		NGUIText.regionHeight = height;
		bool result = NGUIText.WrapText(text, out final, false);
		NGUIText.bitmapFont = null;
		NGUIText.dynamicFont = null;
		return result;
	}

	// Token: 0x06002D74 RID: 11636 RVA: 0x0014CE58 File Offset: 0x0014B258
	public void UpdateNGUIText()
	{
		Font trueTypeFont = this.trueTypeFont;
		bool flag = trueTypeFont != null;
		NGUIText.fontSize = this.mFinalFontSize;
		NGUIText.fontStyle = this.mFontStyle;
		NGUIText.rectWidth = this.mWidth;
		NGUIText.rectHeight = this.mHeight;
		NGUIText.regionWidth = Mathf.RoundToInt((float)this.mWidth * (this.mDrawRegion.z - this.mDrawRegion.x));
		NGUIText.regionHeight = Mathf.RoundToInt((float)this.mHeight * (this.mDrawRegion.w - this.mDrawRegion.y));
		NGUIText.gradient = (this.mApplyGradient && (this.mFont == null || !this.mFont.packedFontShader));
		NGUIText.gradientTop = this.mGradientTop;
		NGUIText.gradientBottom = this.mGradientBottom;
		NGUIText.encoding = this.mEncoding;
		NGUIText.premultiply = this.mPremultiply;
		NGUIText.symbolStyle = this.mSymbols;
		NGUIText.maxLines = this.mMaxLineCount;
		NGUIText.spacingX = this.effectiveSpacingX;
		NGUIText.spacingY = this.effectiveSpacingY;
		NGUIText.fontScale = ((!flag) ? ((float)this.mFontSize / (float)this.mFont.defaultSize * this.mScale) : this.mScale);
		if (this.mFont != null)
		{
			NGUIText.bitmapFont = this.mFont;
			for (;;)
			{
				UIFont replacement = NGUIText.bitmapFont.replacement;
				if (replacement == null)
				{
					break;
				}
				NGUIText.bitmapFont = replacement;
			}
			if (NGUIText.bitmapFont.isDynamic)
			{
				NGUIText.dynamicFont = NGUIText.bitmapFont.dynamicFont;
				NGUIText.bitmapFont = null;
			}
			else
			{
				NGUIText.dynamicFont = null;
			}
		}
		else
		{
			NGUIText.dynamicFont = trueTypeFont;
			NGUIText.bitmapFont = null;
		}
		if (flag && this.keepCrisp)
		{
			UIRoot root = base.root;
			if (root != null)
			{
				NGUIText.pixelDensity = ((!(root != null)) ? 1f : root.pixelSizeAdjustment);
			}
		}
		else
		{
			NGUIText.pixelDensity = 1f;
		}
		if (this.mDensity != NGUIText.pixelDensity)
		{
			this.ProcessText(false, false);
			NGUIText.rectWidth = this.mWidth;
			NGUIText.rectHeight = this.mHeight;
			NGUIText.regionWidth = Mathf.RoundToInt((float)this.mWidth * (this.mDrawRegion.z - this.mDrawRegion.x));
			NGUIText.regionHeight = Mathf.RoundToInt((float)this.mHeight * (this.mDrawRegion.w - this.mDrawRegion.y));
		}
		if (this.alignment == NGUIText.Alignment.Automatic)
		{
			UIWidget.Pivot pivot = base.pivot;
			if (pivot == UIWidget.Pivot.Left || pivot == UIWidget.Pivot.TopLeft || pivot == UIWidget.Pivot.BottomLeft)
			{
				NGUIText.alignment = NGUIText.Alignment.Left;
			}
			else if (pivot == UIWidget.Pivot.Right || pivot == UIWidget.Pivot.TopRight || pivot == UIWidget.Pivot.BottomRight)
			{
				NGUIText.alignment = NGUIText.Alignment.Right;
			}
			else
			{
				NGUIText.alignment = NGUIText.Alignment.Center;
			}
		}
		else
		{
			NGUIText.alignment = this.alignment;
		}
		NGUIText.Update();
	}

	// Token: 0x06002D75 RID: 11637 RVA: 0x0014D17F File Offset: 0x0014B57F
	private void OnApplicationPause(bool paused)
	{
		if (!paused && this.mTrueTypeFont != null)
		{
			this.Invalidate(false);
		}
	}

	// Token: 0x04002C61 RID: 11361
	public UILabel.Crispness keepCrispWhenShrunk = UILabel.Crispness.OnDesktop;

	// Token: 0x04002C62 RID: 11362
	[HideInInspector]
	[SerializeField]
	private Font mTrueTypeFont;

	// Token: 0x04002C63 RID: 11363
	[HideInInspector]
	[SerializeField]
	private UIFont mFont;

	// Token: 0x04002C64 RID: 11364
	[Multiline(6)]
	[HideInInspector]
	[SerializeField]
	private string mText = string.Empty;

	// Token: 0x04002C65 RID: 11365
	[HideInInspector]
	[SerializeField]
	private int mFontSize = 16;

	// Token: 0x04002C66 RID: 11366
	[HideInInspector]
	[SerializeField]
	private FontStyle mFontStyle;

	// Token: 0x04002C67 RID: 11367
	[HideInInspector]
	[SerializeField]
	private NGUIText.Alignment mAlignment;

	// Token: 0x04002C68 RID: 11368
	[HideInInspector]
	[SerializeField]
	private bool mEncoding = true;

	// Token: 0x04002C69 RID: 11369
	[HideInInspector]
	[SerializeField]
	private int mMaxLineCount;

	// Token: 0x04002C6A RID: 11370
	[HideInInspector]
	[SerializeField]
	private UILabel.Effect mEffectStyle;

	// Token: 0x04002C6B RID: 11371
	[HideInInspector]
	[SerializeField]
	private Color mEffectColor = Color.black;

	// Token: 0x04002C6C RID: 11372
	[HideInInspector]
	[SerializeField]
	private NGUIText.SymbolStyle mSymbols = NGUIText.SymbolStyle.Normal;

	// Token: 0x04002C6D RID: 11373
	[HideInInspector]
	[SerializeField]
	private Vector2 mEffectDistance = Vector2.one;

	// Token: 0x04002C6E RID: 11374
	[HideInInspector]
	[SerializeField]
	private UILabel.Overflow mOverflow;

	// Token: 0x04002C6F RID: 11375
	[HideInInspector]
	[SerializeField]
	private bool mApplyGradient;

	// Token: 0x04002C70 RID: 11376
	[HideInInspector]
	[SerializeField]
	private Color mGradientTop = Color.white;

	// Token: 0x04002C71 RID: 11377
	[HideInInspector]
	[SerializeField]
	private Color mGradientBottom = new Color(0.7f, 0.7f, 0.7f);

	// Token: 0x04002C72 RID: 11378
	[HideInInspector]
	[SerializeField]
	private int mSpacingX;

	// Token: 0x04002C73 RID: 11379
	[HideInInspector]
	[SerializeField]
	private int mSpacingY;

	// Token: 0x04002C74 RID: 11380
	[HideInInspector]
	[SerializeField]
	private bool mUseFloatSpacing;

	// Token: 0x04002C75 RID: 11381
	[HideInInspector]
	[SerializeField]
	private float mFloatSpacingX;

	// Token: 0x04002C76 RID: 11382
	[HideInInspector]
	[SerializeField]
	private float mFloatSpacingY;

	// Token: 0x04002C77 RID: 11383
	[HideInInspector]
	[SerializeField]
	private bool mOverflowEllipsis;

	// Token: 0x04002C78 RID: 11384
	[HideInInspector]
	[SerializeField]
	private int mOverflowWidth;

	// Token: 0x04002C79 RID: 11385
	[HideInInspector]
	[SerializeField]
	private int mOverflowHeight;

	// Token: 0x04002C7A RID: 11386
	[HideInInspector]
	[SerializeField]
	private UILabel.Modifier mModifier;

	// Token: 0x04002C7B RID: 11387
	[HideInInspector]
	[SerializeField]
	private bool mShrinkToFit;

	// Token: 0x04002C7C RID: 11388
	[HideInInspector]
	[SerializeField]
	private int mMaxLineWidth;

	// Token: 0x04002C7D RID: 11389
	[HideInInspector]
	[SerializeField]
	private int mMaxLineHeight;

	// Token: 0x04002C7E RID: 11390
	[HideInInspector]
	[SerializeField]
	private float mLineWidth;

	// Token: 0x04002C7F RID: 11391
	[HideInInspector]
	[SerializeField]
	private bool mMultiline = true;

	// Token: 0x04002C80 RID: 11392
	[NonSerialized]
	private Font mActiveTTF;

	// Token: 0x04002C81 RID: 11393
	[NonSerialized]
	private float mDensity = 1f;

	// Token: 0x04002C82 RID: 11394
	[NonSerialized]
	private bool mShouldBeProcessed = true;

	// Token: 0x04002C83 RID: 11395
	[NonSerialized]
	private string mProcessedText;

	// Token: 0x04002C84 RID: 11396
	[NonSerialized]
	private bool mPremultiply;

	// Token: 0x04002C85 RID: 11397
	[NonSerialized]
	private Vector2 mCalculatedSize = Vector2.zero;

	// Token: 0x04002C86 RID: 11398
	[NonSerialized]
	private float mScale = 1f;

	// Token: 0x04002C87 RID: 11399
	[NonSerialized]
	private int mFinalFontSize;

	// Token: 0x04002C88 RID: 11400
	[NonSerialized]
	private int mLastWidth;

	// Token: 0x04002C89 RID: 11401
	[NonSerialized]
	private int mLastHeight;

	// Token: 0x04002C8A RID: 11402
	public UILabel.ModifierFunc customModifier;

	// Token: 0x04002C8B RID: 11403
	private static BetterList<UILabel> mList = new BetterList<UILabel>();

	// Token: 0x04002C8C RID: 11404
	private static Dictionary<Font, int> mFontUsage = new Dictionary<Font, int>();

	// Token: 0x04002C8D RID: 11405
	[NonSerialized]
	private static BetterList<UIDrawCall> mTempDrawcalls;

	// Token: 0x04002C8E RID: 11406
	private static bool mTexRebuildAdded = false;

	// Token: 0x04002C8F RID: 11407
	private static List<Vector3> mTempVerts = new List<Vector3>();

	// Token: 0x04002C90 RID: 11408
	private static List<int> mTempIndices = new List<int>();

	// Token: 0x04002C91 RID: 11409
	[CompilerGenerated]
	private static Action<Font> <>f__mg$cache0;

	// Token: 0x02000624 RID: 1572
	[DoNotObfuscateNGUI]
	public enum Effect
	{
		// Token: 0x04002C93 RID: 11411
		None,
		// Token: 0x04002C94 RID: 11412
		Shadow,
		// Token: 0x04002C95 RID: 11413
		Outline,
		// Token: 0x04002C96 RID: 11414
		Outline8
	}

	// Token: 0x02000625 RID: 1573
	[DoNotObfuscateNGUI]
	public enum Overflow
	{
		// Token: 0x04002C98 RID: 11416
		ShrinkContent,
		// Token: 0x04002C99 RID: 11417
		ClampContent,
		// Token: 0x04002C9A RID: 11418
		ResizeFreely,
		// Token: 0x04002C9B RID: 11419
		ResizeHeight
	}

	// Token: 0x02000626 RID: 1574
	[DoNotObfuscateNGUI]
	public enum Crispness
	{
		// Token: 0x04002C9D RID: 11421
		Never,
		// Token: 0x04002C9E RID: 11422
		OnDesktop,
		// Token: 0x04002C9F RID: 11423
		Always
	}

	// Token: 0x02000627 RID: 1575
	[DoNotObfuscateNGUI]
	public enum Modifier
	{
		// Token: 0x04002CA1 RID: 11425
		None,
		// Token: 0x04002CA2 RID: 11426
		ToUppercase,
		// Token: 0x04002CA3 RID: 11427
		ToLowercase,
		// Token: 0x04002CA4 RID: 11428
		Custom = 255
	}

	// Token: 0x02000628 RID: 1576
	// (Invoke) Token: 0x06002D78 RID: 11640
	public delegate string ModifierFunc(string s);
}
