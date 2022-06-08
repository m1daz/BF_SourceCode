using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020005DB RID: 1499
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/NGUI Widget")]
public class UIWidget : UIRect
{
	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x00137FE3 File Offset: 0x001363E3
	// (set) Token: 0x06002AA4 RID: 10916 RVA: 0x00137FEC File Offset: 0x001363EC
	public UIDrawCall.OnRenderCallback onRender
	{
		get
		{
			return this.mOnRender;
		}
		set
		{
			if (this.mOnRender != value)
			{
				if (this.drawCall != null && this.drawCall.onRender != null && this.mOnRender != null)
				{
					UIDrawCall uidrawCall = this.drawCall;
					uidrawCall.onRender = (UIDrawCall.OnRenderCallback)Delegate.Remove(uidrawCall.onRender, this.mOnRender);
				}
				this.mOnRender = value;
				if (this.drawCall != null)
				{
					UIDrawCall uidrawCall2 = this.drawCall;
					uidrawCall2.onRender = (UIDrawCall.OnRenderCallback)Delegate.Combine(uidrawCall2.onRender, value);
				}
			}
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x0013808B File Offset: 0x0013648B
	// (set) Token: 0x06002AA6 RID: 10918 RVA: 0x00138093 File Offset: 0x00136493
	public Vector4 drawRegion
	{
		get
		{
			return this.mDrawRegion;
		}
		set
		{
			if (this.mDrawRegion != value)
			{
				this.mDrawRegion = value;
				if (this.autoResizeBoxCollider)
				{
					this.ResizeCollider();
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06002AA7 RID: 10919 RVA: 0x001380C4 File Offset: 0x001364C4
	public Vector2 pivotOffset
	{
		get
		{
			return NGUIMath.GetPivotOffset(this.pivot);
		}
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x001380D1 File Offset: 0x001364D1
	// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x001380DC File Offset: 0x001364DC
	public int width
	{
		get
		{
			return this.mWidth;
		}
		set
		{
			int minWidth = this.minWidth;
			if (value < minWidth)
			{
				value = minWidth;
			}
			if (this.mWidth != value && this.keepAspectRatio != UIWidget.AspectRatioSource.BasedOnHeight)
			{
				if (this.isAnchoredHorizontally)
				{
					if (this.leftAnchor.target != null && this.rightAnchor.target != null)
					{
						if (this.mPivot == UIWidget.Pivot.BottomLeft || this.mPivot == UIWidget.Pivot.Left || this.mPivot == UIWidget.Pivot.TopLeft)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, (float)(value - this.mWidth), 0f);
						}
						else if (this.mPivot == UIWidget.Pivot.BottomRight || this.mPivot == UIWidget.Pivot.Right || this.mPivot == UIWidget.Pivot.TopRight)
						{
							NGUIMath.AdjustWidget(this, (float)(this.mWidth - value), 0f, 0f, 0f);
						}
						else
						{
							int num = value - this.mWidth;
							num -= (num & 1);
							if (num != 0)
							{
								NGUIMath.AdjustWidget(this, (float)(-(float)num) * 0.5f, 0f, (float)num * 0.5f, 0f);
							}
						}
					}
					else if (this.leftAnchor.target != null)
					{
						NGUIMath.AdjustWidget(this, 0f, 0f, (float)(value - this.mWidth), 0f);
					}
					else
					{
						NGUIMath.AdjustWidget(this, (float)(this.mWidth - value), 0f, 0f, 0f);
					}
				}
				else
				{
					this.SetDimensions(value, this.mHeight);
				}
			}
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x06002AAA RID: 10922 RVA: 0x0013827A File Offset: 0x0013667A
	// (set) Token: 0x06002AAB RID: 10923 RVA: 0x00138284 File Offset: 0x00136684
	public int height
	{
		get
		{
			return this.mHeight;
		}
		set
		{
			int minHeight = this.minHeight;
			if (value < minHeight)
			{
				value = minHeight;
			}
			if (this.mHeight != value && this.keepAspectRatio != UIWidget.AspectRatioSource.BasedOnWidth)
			{
				if (this.isAnchoredVertically)
				{
					if (this.bottomAnchor.target != null && this.topAnchor.target != null)
					{
						if (this.mPivot == UIWidget.Pivot.BottomLeft || this.mPivot == UIWidget.Pivot.Bottom || this.mPivot == UIWidget.Pivot.BottomRight)
						{
							NGUIMath.AdjustWidget(this, 0f, 0f, 0f, (float)(value - this.mHeight));
						}
						else if (this.mPivot == UIWidget.Pivot.TopLeft || this.mPivot == UIWidget.Pivot.Top || this.mPivot == UIWidget.Pivot.TopRight)
						{
							NGUIMath.AdjustWidget(this, 0f, (float)(this.mHeight - value), 0f, 0f);
						}
						else
						{
							int num = value - this.mHeight;
							num -= (num & 1);
							if (num != 0)
							{
								NGUIMath.AdjustWidget(this, 0f, (float)(-(float)num) * 0.5f, 0f, (float)num * 0.5f);
							}
						}
					}
					else if (this.bottomAnchor.target != null)
					{
						NGUIMath.AdjustWidget(this, 0f, 0f, 0f, (float)(value - this.mHeight));
					}
					else
					{
						NGUIMath.AdjustWidget(this, 0f, (float)(this.mHeight - value), 0f, 0f);
					}
				}
				else
				{
					this.SetDimensions(this.mWidth, value);
				}
			}
		}
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06002AAC RID: 10924 RVA: 0x00138422 File Offset: 0x00136822
	// (set) Token: 0x06002AAD RID: 10925 RVA: 0x0013842C File Offset: 0x0013682C
	public Color color
	{
		get
		{
			return this.mColor;
		}
		set
		{
			if (this.mColor != value)
			{
				bool includeChildren = this.mColor.a != value.a;
				this.mColor = value;
				this.Invalidate(includeChildren);
			}
		}
	}

	// Token: 0x06002AAE RID: 10926 RVA: 0x00138470 File Offset: 0x00136870
	public void SetColorNoAlpha(Color c)
	{
		if (this.mColor.r != c.r || this.mColor.g != c.g || this.mColor.b != c.b)
		{
			this.mColor.r = c.r;
			this.mColor.g = c.g;
			this.mColor.b = c.b;
			this.Invalidate(false);
		}
	}

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06002AAF RID: 10927 RVA: 0x001384FF File Offset: 0x001368FF
	// (set) Token: 0x06002AB0 RID: 10928 RVA: 0x0013850C File Offset: 0x0013690C
	public override float alpha
	{
		get
		{
			return this.mColor.a;
		}
		set
		{
			if (this.mColor.a != value)
			{
				this.mColor.a = value;
				this.Invalidate(true);
			}
		}
	}

	// Token: 0x17000283 RID: 643
	// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x00138532 File Offset: 0x00136932
	public bool isVisible
	{
		get
		{
			return this.mIsVisibleByPanel && this.mIsVisibleByAlpha && this.mIsInFront && this.finalAlpha > 0.001f && NGUITools.GetActive(this);
		}
	}

	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x0013856E File Offset: 0x0013696E
	public bool hasVertices
	{
		get
		{
			return this.geometry != null && this.geometry.hasVertices;
		}
	}

	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x00138589 File Offset: 0x00136989
	// (set) Token: 0x06002AB4 RID: 10932 RVA: 0x00138591 File Offset: 0x00136991
	public UIWidget.Pivot rawPivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				this.mPivot = value;
				if (this.autoResizeBoxCollider)
				{
					this.ResizeCollider();
				}
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x001385BD File Offset: 0x001369BD
	// (set) Token: 0x06002AB6 RID: 10934 RVA: 0x001385C8 File Offset: 0x001369C8
	public UIWidget.Pivot pivot
	{
		get
		{
			return this.mPivot;
		}
		set
		{
			if (this.mPivot != value)
			{
				Vector3 vector = this.worldCorners[0];
				this.mPivot = value;
				this.mChanged = true;
				Vector3 vector2 = this.worldCorners[0];
				Transform cachedTransform = base.cachedTransform;
				Vector3 vector3 = cachedTransform.position;
				float z = cachedTransform.localPosition.z;
				vector3.x += vector.x - vector2.x;
				vector3.y += vector.y - vector2.y;
				base.cachedTransform.position = vector3;
				vector3 = base.cachedTransform.localPosition;
				vector3.x = Mathf.Round(vector3.x);
				vector3.y = Mathf.Round(vector3.y);
				vector3.z = z;
				base.cachedTransform.localPosition = vector3;
			}
		}
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x001386BF File Offset: 0x00136ABF
	// (set) Token: 0x06002AB8 RID: 10936 RVA: 0x001386C8 File Offset: 0x00136AC8
	public int depth
	{
		get
		{
			return this.mDepth;
		}
		set
		{
			if (this.mDepth != value)
			{
				if (this.panel != null)
				{
					this.panel.RemoveWidget(this);
				}
				this.mDepth = value;
				if (this.panel != null)
				{
					this.panel.AddWidget(this);
					if (!Application.isPlaying)
					{
						this.panel.SortWidgets();
						this.panel.RebuildAllDrawCalls();
					}
				}
			}
		}
	}

	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x00138744 File Offset: 0x00136B44
	public int raycastDepth
	{
		get
		{
			if (this.panel == null)
			{
				this.CreatePanel();
			}
			return (!(this.panel != null)) ? this.mDepth : (this.mDepth + this.panel.depth * 1000);
		}
	}

	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06002ABA RID: 10938 RVA: 0x001387A0 File Offset: 0x00136BA0
	public override Vector3[] localCorners
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float x = num + (float)this.mWidth;
			float y = num2 + (float)this.mHeight;
			this.mCorners[0] = new Vector3(num, num2);
			this.mCorners[1] = new Vector3(num, y);
			this.mCorners[2] = new Vector3(x, y);
			this.mCorners[3] = new Vector3(x, num2);
			return this.mCorners;
		}
	}

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x06002ABB RID: 10939 RVA: 0x00138854 File Offset: 0x00136C54
	public virtual Vector2 localSize
	{
		get
		{
			Vector3[] localCorners = this.localCorners;
			return localCorners[2] - localCorners[0];
		}
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x06002ABC RID: 10940 RVA: 0x0013888C File Offset: 0x00136C8C
	public Vector3 localCenter
	{
		get
		{
			Vector3[] localCorners = this.localCorners;
			return Vector3.Lerp(localCorners[0], localCorners[2], 0.5f);
		}
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x06002ABD RID: 10941 RVA: 0x001388C4 File Offset: 0x00136CC4
	public override Vector3[] worldCorners
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float x = num + (float)this.mWidth;
			float y = num2 + (float)this.mHeight;
			Transform cachedTransform = base.cachedTransform;
			this.mCorners[0] = cachedTransform.TransformPoint(num, num2, 0f);
			this.mCorners[1] = cachedTransform.TransformPoint(num, y, 0f);
			this.mCorners[2] = cachedTransform.TransformPoint(x, y, 0f);
			this.mCorners[3] = cachedTransform.TransformPoint(x, num2, 0f);
			return this.mCorners;
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x06002ABE RID: 10942 RVA: 0x0013899B File Offset: 0x00136D9B
	public Vector3 worldCenter
	{
		get
		{
			return base.cachedTransform.TransformPoint(this.localCenter);
		}
	}

	// Token: 0x1700028E RID: 654
	// (get) Token: 0x06002ABF RID: 10943 RVA: 0x001389B0 File Offset: 0x00136DB0
	public virtual Vector4 drawingDimensions
	{
		get
		{
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float num3 = num + (float)this.mWidth;
			float num4 = num2 + (float)this.mHeight;
			return new Vector4((this.mDrawRegion.x != 0f) ? Mathf.Lerp(num, num3, this.mDrawRegion.x) : num, (this.mDrawRegion.y != 0f) ? Mathf.Lerp(num2, num4, this.mDrawRegion.y) : num2, (this.mDrawRegion.z != 1f) ? Mathf.Lerp(num, num3, this.mDrawRegion.z) : num3, (this.mDrawRegion.w != 1f) ? Mathf.Lerp(num2, num4, this.mDrawRegion.w) : num4);
		}
	}

	// Token: 0x1700028F RID: 655
	// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x00138AB7 File Offset: 0x00136EB7
	// (set) Token: 0x06002AC1 RID: 10945 RVA: 0x00138ABF File Offset: 0x00136EBF
	public virtual Material material
	{
		get
		{
			return this.mMat;
		}
		set
		{
			if (this.mMat != value)
			{
				this.RemoveFromPanel();
				this.mMat = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000290 RID: 656
	// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x00138AE8 File Offset: 0x00136EE8
	// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x00138B14 File Offset: 0x00136F14
	public virtual Texture mainTexture
	{
		get
		{
			Material material = this.material;
			return (!(material != null)) ? null : material.mainTexture;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no mainTexture setter");
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x00138B2C File Offset: 0x00136F2C
	// (set) Token: 0x06002AC5 RID: 10949 RVA: 0x00138B58 File Offset: 0x00136F58
	public virtual Shader shader
	{
		get
		{
			Material material = this.material;
			return (!(material != null)) ? null : material.shader;
		}
		set
		{
			throw new NotImplementedException(base.GetType() + " has no shader setter");
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x00138B6F File Offset: 0x00136F6F
	[Obsolete("There is no relative scale anymore. Widgets now have width and height instead")]
	public Vector2 relativeSize
	{
		get
		{
			return Vector2.one;
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06002AC7 RID: 10951 RVA: 0x00138B78 File Offset: 0x00136F78
	public bool hasBoxCollider
	{
		get
		{
			BoxCollider x = base.GetComponent<Collider>() as BoxCollider;
			return x != null || base.GetComponent<BoxCollider2D>() != null;
		}
	}

	// Token: 0x06002AC8 RID: 10952 RVA: 0x00138BAC File Offset: 0x00136FAC
	public void SetDimensions(int w, int h)
	{
		if (this.mWidth != w || this.mHeight != h)
		{
			this.mWidth = w;
			this.mHeight = h;
			if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnWidth)
			{
				this.mHeight = Mathf.RoundToInt((float)this.mWidth / this.aspectRatio);
			}
			else if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
			{
				this.mWidth = Mathf.RoundToInt((float)this.mHeight * this.aspectRatio);
			}
			else if (this.keepAspectRatio == UIWidget.AspectRatioSource.Free)
			{
				this.aspectRatio = (float)this.mWidth / (float)this.mHeight;
			}
			this.mMoved = true;
			if (this.autoResizeBoxCollider)
			{
				this.ResizeCollider();
			}
			this.MarkAsChanged();
		}
	}

	// Token: 0x06002AC9 RID: 10953 RVA: 0x00138C74 File Offset: 0x00137074
	public override Vector3[] GetSides(Transform relativeTo)
	{
		Vector2 pivotOffset = this.pivotOffset;
		float num = -pivotOffset.x * (float)this.mWidth;
		float num2 = -pivotOffset.y * (float)this.mHeight;
		float num3 = num + (float)this.mWidth;
		float num4 = num2 + (float)this.mHeight;
		float x = (num + num3) * 0.5f;
		float y = (num2 + num4) * 0.5f;
		Transform cachedTransform = base.cachedTransform;
		this.mCorners[0] = cachedTransform.TransformPoint(num, y, 0f);
		this.mCorners[1] = cachedTransform.TransformPoint(x, num4, 0f);
		this.mCorners[2] = cachedTransform.TransformPoint(num3, y, 0f);
		this.mCorners[3] = cachedTransform.TransformPoint(x, num2, 0f);
		if (relativeTo != null)
		{
			for (int i = 0; i < 4; i++)
			{
				this.mCorners[i] = relativeTo.InverseTransformPoint(this.mCorners[i]);
			}
		}
		return this.mCorners;
	}

	// Token: 0x06002ACA RID: 10954 RVA: 0x00138DB1 File Offset: 0x001371B1
	public override float CalculateFinalAlpha(int frameID)
	{
		if (this.mAlphaFrameID != frameID)
		{
			this.mAlphaFrameID = frameID;
			this.UpdateFinalAlpha(frameID);
		}
		return this.finalAlpha;
	}

	// Token: 0x06002ACB RID: 10955 RVA: 0x00138DD4 File Offset: 0x001371D4
	protected void UpdateFinalAlpha(int frameID)
	{
		if (!this.mIsVisibleByAlpha || !this.mIsInFront)
		{
			this.finalAlpha = 0f;
		}
		else
		{
			UIRect parent = base.parent;
			this.finalAlpha = ((!(parent != null)) ? this.mColor.a : (parent.CalculateFinalAlpha(frameID) * this.mColor.a));
		}
	}

	// Token: 0x06002ACC RID: 10956 RVA: 0x00138E44 File Offset: 0x00137244
	public override void Invalidate(bool includeChildren)
	{
		this.mChanged = true;
		this.mAlphaFrameID = -1;
		if (this.panel != null)
		{
			bool visibleByPanel = (!this.hideIfOffScreen && !this.panel.hasCumulativeClipping) || this.panel.IsVisible(this);
			this.UpdateVisibility(this.CalculateCumulativeAlpha(Time.frameCount) > 0.001f, visibleByPanel);
			this.UpdateFinalAlpha(Time.frameCount);
			if (includeChildren)
			{
				base.Invalidate(true);
			}
		}
	}

	// Token: 0x06002ACD RID: 10957 RVA: 0x00138ED0 File Offset: 0x001372D0
	public float CalculateCumulativeAlpha(int frameID)
	{
		UIRect parent = base.parent;
		return (!(parent != null)) ? this.mColor.a : (parent.CalculateFinalAlpha(frameID) * this.mColor.a);
	}

	// Token: 0x06002ACE RID: 10958 RVA: 0x00138F14 File Offset: 0x00137314
	public override void SetRect(float x, float y, float width, float height)
	{
		Vector2 pivotOffset = this.pivotOffset;
		float num = Mathf.Lerp(x, x + width, pivotOffset.x);
		float num2 = Mathf.Lerp(y, y + height, pivotOffset.y);
		int num3 = Mathf.FloorToInt(width + 0.5f);
		int num4 = Mathf.FloorToInt(height + 0.5f);
		if (pivotOffset.x == 0.5f)
		{
			num3 = num3 >> 1 << 1;
		}
		if (pivotOffset.y == 0.5f)
		{
			num4 = num4 >> 1 << 1;
		}
		Transform transform = base.cachedTransform;
		Vector3 localPosition = transform.localPosition;
		localPosition.x = Mathf.Floor(num + 0.5f);
		localPosition.y = Mathf.Floor(num2 + 0.5f);
		if (num3 < this.minWidth)
		{
			num3 = this.minWidth;
		}
		if (num4 < this.minHeight)
		{
			num4 = this.minHeight;
		}
		transform.localPosition = localPosition;
		this.width = num3;
		this.height = num4;
		if (base.isAnchored)
		{
			transform = transform.parent;
			if (this.leftAnchor.target)
			{
				this.leftAnchor.SetHorizontal(transform, x);
			}
			if (this.rightAnchor.target)
			{
				this.rightAnchor.SetHorizontal(transform, x + width);
			}
			if (this.bottomAnchor.target)
			{
				this.bottomAnchor.SetVertical(transform, y);
			}
			if (this.topAnchor.target)
			{
				this.topAnchor.SetVertical(transform, y + height);
			}
		}
	}

	// Token: 0x06002ACF RID: 10959 RVA: 0x001390B4 File Offset: 0x001374B4
	public void ResizeCollider()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		if (component != null)
		{
			NGUITools.UpdateWidgetCollider(this, component);
		}
		else
		{
			NGUITools.UpdateWidgetCollider(this, base.GetComponent<BoxCollider2D>());
		}
	}

	// Token: 0x06002AD0 RID: 10960 RVA: 0x001390EC File Offset: 0x001374EC
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int FullCompareFunc(UIWidget left, UIWidget right)
	{
		int num = UIPanel.CompareFunc(left.panel, right.panel);
		return (num != 0) ? num : UIWidget.PanelCompareFunc(left, right);
	}

	// Token: 0x06002AD1 RID: 10961 RVA: 0x00139120 File Offset: 0x00137520
	[DebuggerHidden]
	[DebuggerStepThrough]
	public static int PanelCompareFunc(UIWidget left, UIWidget right)
	{
		if (left.mDepth < right.mDepth)
		{
			return -1;
		}
		if (left.mDepth > right.mDepth)
		{
			return 1;
		}
		Material material = left.material;
		Material material2 = right.material;
		if (material == material2)
		{
			return 0;
		}
		if (material == null)
		{
			return 1;
		}
		if (material2 == null)
		{
			return -1;
		}
		return (material.GetInstanceID() >= material2.GetInstanceID()) ? 1 : -1;
	}

	// Token: 0x06002AD2 RID: 10962 RVA: 0x001391A3 File Offset: 0x001375A3
	public Bounds CalculateBounds()
	{
		return this.CalculateBounds(null);
	}

	// Token: 0x06002AD3 RID: 10963 RVA: 0x001391AC File Offset: 0x001375AC
	public Bounds CalculateBounds(Transform relativeParent)
	{
		if (relativeParent == null)
		{
			Vector3[] localCorners = this.localCorners;
			Bounds result = new Bounds(localCorners[0], Vector3.zero);
			for (int i = 1; i < 4; i++)
			{
				result.Encapsulate(localCorners[i]);
			}
			return result;
		}
		Matrix4x4 worldToLocalMatrix = relativeParent.worldToLocalMatrix;
		Vector3[] worldCorners = this.worldCorners;
		Bounds result2 = new Bounds(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[0]), Vector3.zero);
		for (int j = 1; j < 4; j++)
		{
			result2.Encapsulate(worldToLocalMatrix.MultiplyPoint3x4(worldCorners[j]));
		}
		return result2;
	}

	// Token: 0x06002AD4 RID: 10964 RVA: 0x00139270 File Offset: 0x00137670
	public void SetDirty()
	{
		if (this.drawCall != null)
		{
			this.drawCall.isDirty = true;
		}
		else if (this.isVisible && this.hasVertices)
		{
			this.CreatePanel();
		}
	}

	// Token: 0x06002AD5 RID: 10965 RVA: 0x001392BC File Offset: 0x001376BC
	public void RemoveFromPanel()
	{
		if (this.panel != null)
		{
			this.panel.RemoveWidget(this);
			this.panel = null;
		}
		this.drawCall = null;
	}

	// Token: 0x06002AD6 RID: 10966 RVA: 0x001392EC File Offset: 0x001376EC
	public virtual void MarkAsChanged()
	{
		if (NGUITools.GetActive(this))
		{
			this.mChanged = true;
			if (this.panel != null && base.enabled && NGUITools.GetActive(base.gameObject) && !this.mPlayMode)
			{
				this.SetDirty();
				this.CheckLayer();
			}
		}
	}

	// Token: 0x06002AD7 RID: 10967 RVA: 0x00139350 File Offset: 0x00137750
	public UIPanel CreatePanel()
	{
		if (this.mStarted && this.panel == null && base.enabled && NGUITools.GetActive(base.gameObject))
		{
			this.panel = UIPanel.Find(base.cachedTransform, true, base.cachedGameObject.layer);
			if (this.panel != null)
			{
				this.mParentFound = false;
				this.panel.AddWidget(this);
				this.CheckLayer();
				this.Invalidate(true);
			}
		}
		return this.panel;
	}

	// Token: 0x06002AD8 RID: 10968 RVA: 0x001393E8 File Offset: 0x001377E8
	public void CheckLayer()
	{
		if (this.panel != null && this.panel.gameObject.layer != base.gameObject.layer)
		{
			UnityEngine.Debug.LogWarning("You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
			base.gameObject.layer = this.panel.gameObject.layer;
		}
	}

	// Token: 0x06002AD9 RID: 10969 RVA: 0x0013944C File Offset: 0x0013784C
	public override void ParentHasChanged()
	{
		base.ParentHasChanged();
		if (this.panel != null)
		{
			UIPanel y = UIPanel.Find(base.cachedTransform, true, base.cachedGameObject.layer);
			if (this.panel != y)
			{
				this.RemoveFromPanel();
				this.CreatePanel();
			}
		}
	}

	// Token: 0x06002ADA RID: 10970 RVA: 0x001394A6 File Offset: 0x001378A6
	protected override void Awake()
	{
		base.Awake();
		this.mPlayMode = Application.isPlaying;
	}

	// Token: 0x06002ADB RID: 10971 RVA: 0x001394B9 File Offset: 0x001378B9
	protected override void OnInit()
	{
		base.OnInit();
		this.RemoveFromPanel();
		this.mMoved = true;
		base.Update();
	}

	// Token: 0x06002ADC RID: 10972 RVA: 0x001394D4 File Offset: 0x001378D4
	protected virtual void UpgradeFrom265()
	{
		Vector3 localScale = base.cachedTransform.localScale;
		this.mWidth = Mathf.Abs(Mathf.RoundToInt(localScale.x));
		this.mHeight = Mathf.Abs(Mathf.RoundToInt(localScale.y));
		NGUITools.UpdateWidgetCollider(base.gameObject, true);
	}

	// Token: 0x06002ADD RID: 10973 RVA: 0x00139527 File Offset: 0x00137927
	protected override void OnStart()
	{
		this.CreatePanel();
	}

	// Token: 0x06002ADE RID: 10974 RVA: 0x00139530 File Offset: 0x00137930
	protected override void OnAnchor()
	{
		Transform cachedTransform = base.cachedTransform;
		Transform parent = cachedTransform.parent;
		Vector3 localPosition = cachedTransform.localPosition;
		Vector2 pivotOffset = this.pivotOffset;
		float num;
		float num2;
		float num3;
		float num4;
		if (this.leftAnchor.target == this.bottomAnchor.target && this.leftAnchor.target == this.rightAnchor.target && this.leftAnchor.target == this.topAnchor.target)
		{
			Vector3[] sides = this.leftAnchor.GetSides(parent);
			if (sides != null)
			{
				num = NGUIMath.Lerp(sides[0].x, sides[2].x, this.leftAnchor.relative) + (float)this.leftAnchor.absolute;
				num2 = NGUIMath.Lerp(sides[0].x, sides[2].x, this.rightAnchor.relative) + (float)this.rightAnchor.absolute;
				num3 = NGUIMath.Lerp(sides[3].y, sides[1].y, this.bottomAnchor.relative) + (float)this.bottomAnchor.absolute;
				num4 = NGUIMath.Lerp(sides[3].y, sides[1].y, this.topAnchor.relative) + (float)this.topAnchor.absolute;
				this.mIsInFront = true;
			}
			else
			{
				Vector3 localPos = base.GetLocalPos(this.leftAnchor, parent);
				num = localPos.x + (float)this.leftAnchor.absolute;
				num3 = localPos.y + (float)this.bottomAnchor.absolute;
				num2 = localPos.x + (float)this.rightAnchor.absolute;
				num4 = localPos.y + (float)this.topAnchor.absolute;
				this.mIsInFront = (!this.hideIfOffScreen || localPos.z >= 0f);
			}
		}
		else
		{
			this.mIsInFront = true;
			if (this.leftAnchor.target)
			{
				Vector3[] sides2 = this.leftAnchor.GetSides(parent);
				if (sides2 != null)
				{
					num = NGUIMath.Lerp(sides2[0].x, sides2[2].x, this.leftAnchor.relative) + (float)this.leftAnchor.absolute;
				}
				else
				{
					num = base.GetLocalPos(this.leftAnchor, parent).x + (float)this.leftAnchor.absolute;
				}
			}
			else
			{
				num = localPosition.x - pivotOffset.x * (float)this.mWidth;
			}
			if (this.rightAnchor.target)
			{
				Vector3[] sides3 = this.rightAnchor.GetSides(parent);
				if (sides3 != null)
				{
					num2 = NGUIMath.Lerp(sides3[0].x, sides3[2].x, this.rightAnchor.relative) + (float)this.rightAnchor.absolute;
				}
				else
				{
					num2 = base.GetLocalPos(this.rightAnchor, parent).x + (float)this.rightAnchor.absolute;
				}
			}
			else
			{
				num2 = localPosition.x - pivotOffset.x * (float)this.mWidth + (float)this.mWidth;
			}
			if (this.bottomAnchor.target)
			{
				Vector3[] sides4 = this.bottomAnchor.GetSides(parent);
				if (sides4 != null)
				{
					num3 = NGUIMath.Lerp(sides4[3].y, sides4[1].y, this.bottomAnchor.relative) + (float)this.bottomAnchor.absolute;
				}
				else
				{
					num3 = base.GetLocalPos(this.bottomAnchor, parent).y + (float)this.bottomAnchor.absolute;
				}
			}
			else
			{
				num3 = localPosition.y - pivotOffset.y * (float)this.mHeight;
			}
			if (this.topAnchor.target)
			{
				Vector3[] sides5 = this.topAnchor.GetSides(parent);
				if (sides5 != null)
				{
					num4 = NGUIMath.Lerp(sides5[3].y, sides5[1].y, this.topAnchor.relative) + (float)this.topAnchor.absolute;
				}
				else
				{
					num4 = base.GetLocalPos(this.topAnchor, parent).y + (float)this.topAnchor.absolute;
				}
			}
			else
			{
				num4 = localPosition.y - pivotOffset.y * (float)this.mHeight + (float)this.mHeight;
			}
		}
		Vector3 vector = new Vector3(Mathf.Lerp(num, num2, pivotOffset.x), Mathf.Lerp(num3, num4, pivotOffset.y), localPosition.z);
		vector.x = Mathf.Round(vector.x);
		vector.y = Mathf.Round(vector.y);
		int num5 = Mathf.FloorToInt(num2 - num + 0.5f);
		int num6 = Mathf.FloorToInt(num4 - num3 + 0.5f);
		if (this.keepAspectRatio != UIWidget.AspectRatioSource.Free && this.aspectRatio != 0f)
		{
			if (this.keepAspectRatio == UIWidget.AspectRatioSource.BasedOnHeight)
			{
				num5 = Mathf.RoundToInt((float)num6 * this.aspectRatio);
			}
			else
			{
				num6 = Mathf.RoundToInt((float)num5 / this.aspectRatio);
			}
		}
		if (num5 < this.minWidth)
		{
			num5 = this.minWidth;
		}
		if (num6 < this.minHeight)
		{
			num6 = this.minHeight;
		}
		if (Vector3.SqrMagnitude(localPosition - vector) > 0.001f)
		{
			base.cachedTransform.localPosition = vector;
			if (this.mIsInFront)
			{
				this.mChanged = true;
			}
		}
		if (this.mWidth != num5 || this.mHeight != num6)
		{
			this.mWidth = num5;
			this.mHeight = num6;
			if (this.mIsInFront)
			{
				this.mChanged = true;
			}
			if (this.autoResizeBoxCollider)
			{
				this.ResizeCollider();
			}
		}
	}

	// Token: 0x06002ADF RID: 10975 RVA: 0x00139B7A File Offset: 0x00137F7A
	protected override void OnUpdate()
	{
		if (this.panel == null)
		{
			this.CreatePanel();
		}
	}

	// Token: 0x06002AE0 RID: 10976 RVA: 0x00139B94 File Offset: 0x00137F94
	private void OnApplicationPause(bool paused)
	{
		if (!paused)
		{
			this.MarkAsChanged();
		}
	}

	// Token: 0x06002AE1 RID: 10977 RVA: 0x00139BA2 File Offset: 0x00137FA2
	protected override void OnDisable()
	{
		this.RemoveFromPanel();
		base.OnDisable();
	}

	// Token: 0x06002AE2 RID: 10978 RVA: 0x00139BB0 File Offset: 0x00137FB0
	private void OnDestroy()
	{
		this.RemoveFromPanel();
	}

	// Token: 0x06002AE3 RID: 10979 RVA: 0x00139BB8 File Offset: 0x00137FB8
	public bool UpdateVisibility(bool visibleByAlpha, bool visibleByPanel)
	{
		if (this.mIsVisibleByAlpha != visibleByAlpha || this.mIsVisibleByPanel != visibleByPanel)
		{
			this.mChanged = true;
			this.mIsVisibleByAlpha = visibleByAlpha;
			this.mIsVisibleByPanel = visibleByPanel;
			return true;
		}
		return false;
	}

	// Token: 0x06002AE4 RID: 10980 RVA: 0x00139BEC File Offset: 0x00137FEC
	public bool UpdateTransform(int frame)
	{
		Transform cachedTransform = base.cachedTransform;
		this.mPlayMode = Application.isPlaying;
		if (this.mMoved)
		{
			this.mMoved = true;
			this.mMatrixFrame = -1;
			cachedTransform.hasChanged = false;
			Vector2 pivotOffset = this.pivotOffset;
			float num = -pivotOffset.x * (float)this.mWidth;
			float num2 = -pivotOffset.y * (float)this.mHeight;
			float x = num + (float)this.mWidth;
			float y = num2 + (float)this.mHeight;
			this.mOldV0 = this.panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(num, num2, 0f));
			this.mOldV1 = this.panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(x, y, 0f));
		}
		else if (!this.panel.widgetsAreStatic && cachedTransform.hasChanged)
		{
			this.mMatrixFrame = -1;
			cachedTransform.hasChanged = false;
			Vector2 pivotOffset2 = this.pivotOffset;
			float num3 = -pivotOffset2.x * (float)this.mWidth;
			float num4 = -pivotOffset2.y * (float)this.mHeight;
			float x2 = num3 + (float)this.mWidth;
			float y2 = num4 + (float)this.mHeight;
			Vector3 b = this.panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(num3, num4, 0f));
			Vector3 b2 = this.panel.worldToLocal.MultiplyPoint3x4(cachedTransform.TransformPoint(x2, y2, 0f));
			if (Vector3.SqrMagnitude(this.mOldV0 - b) > 1E-06f || Vector3.SqrMagnitude(this.mOldV1 - b2) > 1E-06f)
			{
				this.mMoved = true;
				this.mOldV0 = b;
				this.mOldV1 = b2;
			}
		}
		if (this.mMoved && this.onChange != null)
		{
			this.onChange();
		}
		return this.mMoved || this.mChanged;
	}

	// Token: 0x06002AE5 RID: 10981 RVA: 0x00139DEC File Offset: 0x001381EC
	public bool UpdateGeometry(int frame)
	{
		float num = this.CalculateFinalAlpha(frame);
		if (this.mIsVisibleByAlpha && this.mLastAlpha != num)
		{
			this.mChanged = true;
		}
		this.mLastAlpha = num;
		if (this.mChanged)
		{
			if (this.mIsVisibleByAlpha && num > 0.001f && this.shader != null)
			{
				bool hasVertices = this.geometry.hasVertices;
				if (this.fillGeometry)
				{
					this.geometry.Clear();
					this.OnFill(this.geometry.verts, this.geometry.uvs, this.geometry.cols);
				}
				if (this.geometry.hasVertices)
				{
					if (this.mMatrixFrame != frame)
					{
						this.mLocalToPanel = this.panel.worldToLocal * base.cachedTransform.localToWorldMatrix;
						this.mMatrixFrame = frame;
					}
					this.geometry.ApplyTransform(this.mLocalToPanel, this.panel.generateNormals);
					this.mMoved = false;
					this.mChanged = false;
					return true;
				}
				this.mChanged = false;
				return hasVertices;
			}
			else if (this.geometry.hasVertices)
			{
				if (this.fillGeometry)
				{
					this.geometry.Clear();
				}
				this.mMoved = false;
				this.mChanged = false;
				return true;
			}
		}
		else if (this.mMoved && this.geometry.hasVertices)
		{
			if (this.mMatrixFrame != frame)
			{
				this.mLocalToPanel = this.panel.worldToLocal * base.cachedTransform.localToWorldMatrix;
				this.mMatrixFrame = frame;
			}
			this.geometry.ApplyTransform(this.mLocalToPanel, this.panel.generateNormals);
			this.mMoved = false;
			this.mChanged = false;
			return true;
		}
		this.mMoved = false;
		this.mChanged = false;
		return false;
	}

	// Token: 0x06002AE6 RID: 10982 RVA: 0x00139FDF File Offset: 0x001383DF
	public void WriteToBuffers(List<Vector3> v, List<Vector2> u, List<Color> c, List<Vector3> n, List<Vector4> t, List<Vector4> u2)
	{
		this.geometry.WriteToBuffers(v, u, c, n, t, u2);
	}

	// Token: 0x06002AE7 RID: 10983 RVA: 0x00139FF8 File Offset: 0x001383F8
	public virtual void MakePixelPerfect()
	{
		Vector3 localPosition = base.cachedTransform.localPosition;
		localPosition.z = Mathf.Round(localPosition.z);
		localPosition.x = Mathf.Round(localPosition.x);
		localPosition.y = Mathf.Round(localPosition.y);
		base.cachedTransform.localPosition = localPosition;
		Vector3 localScale = base.cachedTransform.localScale;
		base.cachedTransform.localScale = new Vector3(Mathf.Sign(localScale.x), Mathf.Sign(localScale.y), 1f);
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x0013A08F File Offset: 0x0013848F
	public virtual int minWidth
	{
		get
		{
			return 2;
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x0013A092 File Offset: 0x00138492
	public virtual int minHeight
	{
		get
		{
			return 2;
		}
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06002AEA RID: 10986 RVA: 0x0013A095 File Offset: 0x00138495
	// (set) Token: 0x06002AEB RID: 10987 RVA: 0x0013A09C File Offset: 0x0013849C
	public virtual Vector4 border
	{
		get
		{
			return Vector4.zero;
		}
		set
		{
		}
	}

	// Token: 0x06002AEC RID: 10988 RVA: 0x0013A09E File Offset: 0x0013849E
	public virtual void OnFill(List<Vector3> verts, List<Vector2> uvs, List<Color> cols)
	{
	}

	// Token: 0x04002A7F RID: 10879
	[HideInInspector]
	[SerializeField]
	protected Color mColor = Color.white;

	// Token: 0x04002A80 RID: 10880
	[HideInInspector]
	[SerializeField]
	protected UIWidget.Pivot mPivot = UIWidget.Pivot.Center;

	// Token: 0x04002A81 RID: 10881
	[HideInInspector]
	[SerializeField]
	protected int mWidth = 100;

	// Token: 0x04002A82 RID: 10882
	[HideInInspector]
	[SerializeField]
	protected int mHeight = 100;

	// Token: 0x04002A83 RID: 10883
	[HideInInspector]
	[SerializeField]
	protected int mDepth;

	// Token: 0x04002A84 RID: 10884
	[Tooltip("Custom material, if desired")]
	[HideInInspector]
	[SerializeField]
	protected Material mMat;

	// Token: 0x04002A85 RID: 10885
	public UIWidget.OnDimensionsChanged onChange;

	// Token: 0x04002A86 RID: 10886
	public UIWidget.OnPostFillCallback onPostFill;

	// Token: 0x04002A87 RID: 10887
	public UIDrawCall.OnRenderCallback mOnRender;

	// Token: 0x04002A88 RID: 10888
	public bool autoResizeBoxCollider;

	// Token: 0x04002A89 RID: 10889
	public bool hideIfOffScreen;

	// Token: 0x04002A8A RID: 10890
	public UIWidget.AspectRatioSource keepAspectRatio;

	// Token: 0x04002A8B RID: 10891
	public float aspectRatio = 1f;

	// Token: 0x04002A8C RID: 10892
	public UIWidget.HitCheck hitCheck;

	// Token: 0x04002A8D RID: 10893
	[NonSerialized]
	public UIPanel panel;

	// Token: 0x04002A8E RID: 10894
	[NonSerialized]
	public UIGeometry geometry = new UIGeometry();

	// Token: 0x04002A8F RID: 10895
	[NonSerialized]
	public bool fillGeometry = true;

	// Token: 0x04002A90 RID: 10896
	[NonSerialized]
	protected bool mPlayMode = true;

	// Token: 0x04002A91 RID: 10897
	[NonSerialized]
	protected Vector4 mDrawRegion = new Vector4(0f, 0f, 1f, 1f);

	// Token: 0x04002A92 RID: 10898
	[NonSerialized]
	private Matrix4x4 mLocalToPanel;

	// Token: 0x04002A93 RID: 10899
	[NonSerialized]
	private bool mIsVisibleByAlpha = true;

	// Token: 0x04002A94 RID: 10900
	[NonSerialized]
	private bool mIsVisibleByPanel = true;

	// Token: 0x04002A95 RID: 10901
	[NonSerialized]
	private bool mIsInFront = true;

	// Token: 0x04002A96 RID: 10902
	[NonSerialized]
	private float mLastAlpha;

	// Token: 0x04002A97 RID: 10903
	[NonSerialized]
	private bool mMoved;

	// Token: 0x04002A98 RID: 10904
	[NonSerialized]
	public UIDrawCall drawCall;

	// Token: 0x04002A99 RID: 10905
	[NonSerialized]
	protected Vector3[] mCorners = new Vector3[4];

	// Token: 0x04002A9A RID: 10906
	[NonSerialized]
	private int mAlphaFrameID = -1;

	// Token: 0x04002A9B RID: 10907
	private int mMatrixFrame = -1;

	// Token: 0x04002A9C RID: 10908
	private Vector3 mOldV0;

	// Token: 0x04002A9D RID: 10909
	private Vector3 mOldV1;

	// Token: 0x020005DC RID: 1500
	[DoNotObfuscateNGUI]
	public enum Pivot
	{
		// Token: 0x04002A9F RID: 10911
		TopLeft,
		// Token: 0x04002AA0 RID: 10912
		Top,
		// Token: 0x04002AA1 RID: 10913
		TopRight,
		// Token: 0x04002AA2 RID: 10914
		Left,
		// Token: 0x04002AA3 RID: 10915
		Center,
		// Token: 0x04002AA4 RID: 10916
		Right,
		// Token: 0x04002AA5 RID: 10917
		BottomLeft,
		// Token: 0x04002AA6 RID: 10918
		Bottom,
		// Token: 0x04002AA7 RID: 10919
		BottomRight
	}

	// Token: 0x020005DD RID: 1501
	// (Invoke) Token: 0x06002AEE RID: 10990
	public delegate void OnDimensionsChanged();

	// Token: 0x020005DE RID: 1502
	// (Invoke) Token: 0x06002AF2 RID: 10994
	public delegate void OnPostFillCallback(UIWidget widget, int bufferOffset, List<Vector3> verts, List<Vector2> uvs, List<Color> cols);

	// Token: 0x020005DF RID: 1503
	[DoNotObfuscateNGUI]
	public enum AspectRatioSource
	{
		// Token: 0x04002AA9 RID: 10921
		Free,
		// Token: 0x04002AAA RID: 10922
		BasedOnWidth,
		// Token: 0x04002AAB RID: 10923
		BasedOnHeight
	}

	// Token: 0x020005E0 RID: 1504
	// (Invoke) Token: 0x06002AF6 RID: 10998
	public delegate bool HitCheck(Vector3 worldPos);
}
