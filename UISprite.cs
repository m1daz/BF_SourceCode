using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000633 RID: 1587
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Sprite")]
public class UISprite : UIBasicSprite
{
	// Token: 0x17000356 RID: 854
	// (get) Token: 0x06002DEE RID: 11758 RVA: 0x0015067C File Offset: 0x0014EA7C
	// (set) Token: 0x06002DEF RID: 11759 RVA: 0x001506C4 File Offset: 0x0014EAC4
	public override Texture mainTexture
	{
		get
		{
			Material material = (!(this.mAtlas != null)) ? null : this.mAtlas.spriteMaterial;
			return (!(material != null)) ? null : material.mainTexture;
		}
		set
		{
			base.mainTexture = value;
		}
	}

	// Token: 0x17000357 RID: 855
	// (get) Token: 0x06002DF0 RID: 11760 RVA: 0x001506D0 File Offset: 0x0014EAD0
	// (set) Token: 0x06002DF1 RID: 11761 RVA: 0x00150714 File Offset: 0x0014EB14
	public override Material material
	{
		get
		{
			Material material = base.material;
			if (material != null)
			{
				return material;
			}
			return (!(this.mAtlas != null)) ? null : this.mAtlas.spriteMaterial;
		}
		set
		{
			base.material = value;
		}
	}

	// Token: 0x17000358 RID: 856
	// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x0015071D File Offset: 0x0014EB1D
	// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x00150728 File Offset: 0x0014EB28
	public UIAtlas atlas
	{
		get
		{
			return this.mAtlas;
		}
		set
		{
			if (this.mAtlas != value)
			{
				base.RemoveFromPanel();
				this.mAtlas = value;
				this.mSpriteSet = false;
				this.mSprite = null;
				if (string.IsNullOrEmpty(this.mSpriteName) && this.mAtlas != null && this.mAtlas.spriteList.Count > 0)
				{
					this.SetAtlasSprite(this.mAtlas.spriteList[0]);
					this.mSpriteName = this.mSprite.name;
				}
				if (!string.IsNullOrEmpty(this.mSpriteName))
				{
					string spriteName = this.mSpriteName;
					this.mSpriteName = string.Empty;
					this.spriteName = spriteName;
					this.MarkAsChanged();
				}
			}
		}
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x001507EF File Offset: 0x0014EBEF
	// (set) Token: 0x06002DF5 RID: 11765 RVA: 0x001507F8 File Offset: 0x0014EBF8
	public string spriteName
	{
		get
		{
			return this.mSpriteName;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				if (string.IsNullOrEmpty(this.mSpriteName))
				{
					return;
				}
				this.mSpriteName = string.Empty;
				this.mSprite = null;
				this.mChanged = true;
				this.mSpriteSet = false;
			}
			else if (this.mSpriteName != value)
			{
				this.mSpriteName = value;
				this.mSprite = null;
				this.mChanged = true;
				this.mSpriteSet = false;
			}
		}
	}

	// Token: 0x1700035A RID: 858
	// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x00150873 File Offset: 0x0014EC73
	public bool isValid
	{
		get
		{
			return this.GetAtlasSprite() != null;
		}
	}

	// Token: 0x1700035B RID: 859
	// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x00150881 File Offset: 0x0014EC81
	// (set) Token: 0x06002DF8 RID: 11768 RVA: 0x0015088F File Offset: 0x0014EC8F
	[Obsolete("Use 'centerType' instead")]
	public bool fillCenter
	{
		get
		{
			return this.centerType != UIBasicSprite.AdvancedType.Invisible;
		}
		set
		{
			if (value != (this.centerType != UIBasicSprite.AdvancedType.Invisible))
			{
				this.centerType = ((!value) ? UIBasicSprite.AdvancedType.Invisible : UIBasicSprite.AdvancedType.Sliced);
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700035C RID: 860
	// (get) Token: 0x06002DF9 RID: 11769 RVA: 0x001508BC File Offset: 0x0014ECBC
	// (set) Token: 0x06002DFA RID: 11770 RVA: 0x001508C4 File Offset: 0x0014ECC4
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

	// Token: 0x1700035D RID: 861
	// (get) Token: 0x06002DFB RID: 11771 RVA: 0x001508DF File Offset: 0x0014ECDF
	// (set) Token: 0x06002DFC RID: 11772 RVA: 0x001508E7 File Offset: 0x0014ECE7
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

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x06002DFD RID: 11773 RVA: 0x00150912 File Offset: 0x0014ED12
	// (set) Token: 0x06002DFE RID: 11774 RVA: 0x0015091A File Offset: 0x0014ED1A
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

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x06002DFF RID: 11775 RVA: 0x00150948 File Offset: 0x0014ED48
	public override Vector4 border
	{
		get
		{
			UISpriteData atlasSprite = this.GetAtlasSprite();
			if (atlasSprite == null)
			{
				return base.border;
			}
			return new Vector4((float)atlasSprite.borderLeft, (float)atlasSprite.borderBottom, (float)atlasSprite.borderRight, (float)atlasSprite.borderTop);
		}
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x06002E00 RID: 11776 RVA: 0x0015098C File Offset: 0x0014ED8C
	protected override Vector4 padding
	{
		get
		{
			UISpriteData atlasSprite = this.GetAtlasSprite();
			Vector4 result = new Vector4(0f, 0f, 0f, 0f);
			if (atlasSprite != null)
			{
				result.x = (float)atlasSprite.paddingLeft;
				result.y = (float)atlasSprite.paddingBottom;
				result.z = (float)atlasSprite.paddingRight;
				result.w = (float)atlasSprite.paddingTop;
			}
			return result;
		}
	}

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x06002E01 RID: 11777 RVA: 0x001509FA File Offset: 0x0014EDFA
	public override float pixelSize
	{
		get
		{
			return (!(this.mAtlas != null)) ? 1f : this.mAtlas.pixelSize;
		}
	}

	// Token: 0x17000362 RID: 866
	// (get) Token: 0x06002E02 RID: 11778 RVA: 0x00150A24 File Offset: 0x0014EE24
	public override int minWidth
	{
		get
		{
			if (this.type == UIBasicSprite.Type.Sliced || this.type == UIBasicSprite.Type.Advanced)
			{
				float pixelSize = this.pixelSize;
				Vector4 vector = this.border * this.pixelSize;
				int num = Mathf.RoundToInt(vector.x + vector.z);
				UISpriteData atlasSprite = this.GetAtlasSprite();
				if (atlasSprite != null)
				{
					num += Mathf.RoundToInt(pixelSize * (float)(atlasSprite.paddingLeft + atlasSprite.paddingRight));
				}
				return Mathf.Max(base.minWidth, ((num & 1) != 1) ? num : (num + 1));
			}
			return base.minWidth;
		}
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x06002E03 RID: 11779 RVA: 0x00150AC0 File Offset: 0x0014EEC0
	public override int minHeight
	{
		get
		{
			if (this.type == UIBasicSprite.Type.Sliced || this.type == UIBasicSprite.Type.Advanced)
			{
				float pixelSize = this.pixelSize;
				Vector4 vector = this.border * this.pixelSize;
				int num = Mathf.RoundToInt(vector.y + vector.w);
				UISpriteData atlasSprite = this.GetAtlasSprite();
				if (atlasSprite != null)
				{
					num += Mathf.RoundToInt(pixelSize * (float)(atlasSprite.paddingTop + atlasSprite.paddingBottom));
				}
				return Mathf.Max(base.minHeight, ((num & 1) != 1) ? num : (num + 1));
			}
			return base.minHeight;
		}
	}

	// Token: 0x17000364 RID: 868
	// (get) Token: 0x06002E04 RID: 11780 RVA: 0x00150B5C File Offset: 0x0014EF5C
	public override Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = base.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			if (this.GetAtlasSprite() != null && this.mType != UIBasicSprite.Type.Tiled)
			{
				int num5 = this.mSprite.paddingLeft;
				int num6 = this.mSprite.paddingBottom;
				int num7 = this.mSprite.paddingRight;
				int num8 = this.mSprite.paddingTop;
				if (this.mType != UIBasicSprite.Type.Simple)
				{
					float pixelSize = this.pixelSize;
					if (pixelSize != 1f)
					{
						num5 = Mathf.RoundToInt(pixelSize * (float)num5);
						num6 = Mathf.RoundToInt(pixelSize * (float)num6);
						num7 = Mathf.RoundToInt(pixelSize * (float)num7);
						num8 = Mathf.RoundToInt(pixelSize * (float)num8);
					}
				}
				int num9 = this.mSprite.width + num5 + num7;
				int num10 = this.mSprite.height + num6 + num8;
				float num11 = 1f;
				float num12 = 1f;
				if (num9 > 0 && num10 > 0 && (this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled))
				{
					if ((num9 & 1) != 0)
					{
						num7++;
					}
					if ((num10 & 1) != 0)
					{
						num8++;
					}
					num11 = 1f / (float)num9 * (float)this.mWidth;
					num12 = 1f / (float)num10 * (float)this.mHeight;
				}
				if (this.mFlip == UIBasicSprite.Flip.Horizontally || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num += (float)num7 * num11;
					num3 -= (float)num5 * num11;
				}
				else
				{
					num += (float)num5 * num11;
					num3 -= (float)num7 * num11;
				}
				if (this.mFlip == UIBasicSprite.Flip.Vertically || this.mFlip == UIBasicSprite.Flip.Both)
				{
					num2 += (float)num8 * num12;
					num4 -= (float)num6 * num12;
				}
				else
				{
					num2 += (float)num6 * num12;
					num4 -= (float)num8 * num12;
				}
			}
			Vector4 vector = (!(this.mAtlas != null)) ? Vector4.zero : (this.border * this.pixelSize);
			float num13 = vector.x + vector.z;
			float num14 = vector.y + vector.w;
			float x = Mathf.Lerp(num, num3 - num13, this.mDrawRegion.x);
			float y = Mathf.Lerp(num2, num4 - num14, this.mDrawRegion.y);
			float z = Mathf.Lerp(num + num13, num3, this.mDrawRegion.z);
			float w = Mathf.Lerp(num2 + num14, num4, this.mDrawRegion.w);
			return new Vector4(x, y, z, w);
		}
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x06002E05 RID: 11781 RVA: 0x00150E25 File Offset: 0x0014F225
	public override bool premultipliedAlpha
	{
		get
		{
			return this.mAtlas != null && this.mAtlas.premultipliedAlpha;
		}
	}

	// Token: 0x06002E06 RID: 11782 RVA: 0x00150E48 File Offset: 0x0014F248
	public UISpriteData GetAtlasSprite()
	{
		if (!this.mSpriteSet)
		{
			this.mSprite = null;
		}
		if (this.mSprite == null && this.mAtlas != null)
		{
			if (!string.IsNullOrEmpty(this.mSpriteName))
			{
				UISpriteData sprite = this.mAtlas.GetSprite(this.mSpriteName);
				if (sprite == null)
				{
					return null;
				}
				this.SetAtlasSprite(sprite);
			}
			if (this.mSprite == null && this.mAtlas.spriteList.Count > 0)
			{
				UISpriteData uispriteData = this.mAtlas.spriteList[0];
				if (uispriteData == null)
				{
					return null;
				}
				this.SetAtlasSprite(uispriteData);
				if (this.mSprite == null)
				{
					Debug.LogError(this.mAtlas.name + " seems to have a null sprite!");
					return null;
				}
				this.mSpriteName = this.mSprite.name;
			}
		}
		return this.mSprite;
	}

	// Token: 0x06002E07 RID: 11783 RVA: 0x00150F34 File Offset: 0x0014F334
	protected void SetAtlasSprite(UISpriteData sp)
	{
		this.mChanged = true;
		this.mSpriteSet = true;
		if (sp != null)
		{
			this.mSprite = sp;
			this.mSpriteName = this.mSprite.name;
		}
		else
		{
			this.mSpriteName = ((this.mSprite == null) ? string.Empty : this.mSprite.name);
			this.mSprite = sp;
		}
	}

	// Token: 0x06002E08 RID: 11784 RVA: 0x00150FA0 File Offset: 0x0014F3A0
	public override void MakePixelPerfect()
	{
		if (!this.isValid)
		{
			return;
		}
		base.MakePixelPerfect();
		if (this.mType == UIBasicSprite.Type.Tiled)
		{
			return;
		}
		UISpriteData atlasSprite = this.GetAtlasSprite();
		if (atlasSprite == null)
		{
			return;
		}
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		if ((this.mType == UIBasicSprite.Type.Simple || this.mType == UIBasicSprite.Type.Filled || !atlasSprite.hasBorder) && mainTexture != null)
		{
			int num = Mathf.RoundToInt(this.pixelSize * (float)(atlasSprite.width + atlasSprite.paddingLeft + atlasSprite.paddingRight));
			int num2 = Mathf.RoundToInt(this.pixelSize * (float)(atlasSprite.height + atlasSprite.paddingTop + atlasSprite.paddingBottom));
			if ((num & 1) == 1)
			{
				num++;
			}
			if ((num2 & 1) == 1)
			{
				num2++;
			}
			base.width = num;
			base.height = num2;
		}
	}

	// Token: 0x06002E09 RID: 11785 RVA: 0x00151088 File Offset: 0x0014F488
	protected override void OnInit()
	{
		if (!this.mFillCenter)
		{
			this.mFillCenter = true;
			this.centerType = UIBasicSprite.AdvancedType.Invisible;
		}
		base.OnInit();
	}

	// Token: 0x06002E0A RID: 11786 RVA: 0x001510A9 File Offset: 0x0014F4A9
	protected override void OnUpdate()
	{
		base.OnUpdate();
		if (this.mChanged || !this.mSpriteSet)
		{
			this.mSpriteSet = true;
			this.mSprite = null;
			this.mChanged = true;
		}
	}

	// Token: 0x06002E0B RID: 11787 RVA: 0x001510DC File Offset: 0x0014F4DC
	public override void OnFill(List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
		Texture mainTexture = this.mainTexture;
		if (mainTexture == null)
		{
			return;
		}
		if (this.mSprite == null)
		{
			this.mSprite = this.atlas.GetSprite(this.spriteName);
		}
		if (this.mSprite == null)
		{
			return;
		}
		Rect rect = new Rect((float)this.mSprite.x, (float)this.mSprite.y, (float)this.mSprite.width, (float)this.mSprite.height);
		Rect rect2 = new Rect((float)(this.mSprite.x + this.mSprite.borderLeft), (float)(this.mSprite.y + this.mSprite.borderTop), (float)(this.mSprite.width - this.mSprite.borderLeft - this.mSprite.borderRight), (float)(this.mSprite.height - this.mSprite.borderBottom - this.mSprite.borderTop));
		rect = NGUIMath.ConvertToTexCoords(rect, mainTexture.width, mainTexture.height);
		rect2 = NGUIMath.ConvertToTexCoords(rect2, mainTexture.width, mainTexture.height);
		int count = verts.Count;
		base.Fill(verts, uvs, cols, rect, rect2);
		if (this.onPostFill != null)
		{
			this.onPostFill(this, count, verts, uvs, cols);
		}
	}

	// Token: 0x04002CF2 RID: 11506
	[HideInInspector]
	[SerializeField]
	private UIAtlas mAtlas;

	// Token: 0x04002CF3 RID: 11507
	[HideInInspector]
	[SerializeField]
	private string mSpriteName;

	// Token: 0x04002CF4 RID: 11508
	[HideInInspector]
	[SerializeField]
	private bool mFillCenter = true;

	// Token: 0x04002CF5 RID: 11509
	[NonSerialized]
	protected UISpriteData mSprite;

	// Token: 0x04002CF6 RID: 11510
	[NonSerialized]
	private bool mSpriteSet;
}
