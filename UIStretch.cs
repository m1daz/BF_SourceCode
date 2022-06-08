using System;
using UnityEngine;

// Token: 0x02000636 RID: 1590
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Stretch")]
public class UIStretch : MonoBehaviour
{
	// Token: 0x06002E24 RID: 11812 RVA: 0x00151700 File Offset: 0x0014FB00
	private void Awake()
	{
		this.mAnim = base.GetComponent<Animation>();
		this.mRect = default(Rect);
		this.mTrans = base.transform;
		this.mWidget = base.GetComponent<UIWidget>();
		this.mSprite = base.GetComponent<UISprite>();
		this.mPanel = base.GetComponent<UIPanel>();
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Combine(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x06002E25 RID: 11813 RVA: 0x00151778 File Offset: 0x0014FB78
	private void OnDestroy()
	{
		UICamera.onScreenResize = (UICamera.OnScreenResize)Delegate.Remove(UICamera.onScreenResize, new UICamera.OnScreenResize(this.ScreenSizeChanged));
	}

	// Token: 0x06002E26 RID: 11814 RVA: 0x0015179A File Offset: 0x0014FB9A
	private void ScreenSizeChanged()
	{
		if (this.mStarted && this.runOnlyOnce)
		{
			this.Update();
		}
	}

	// Token: 0x06002E27 RID: 11815 RVA: 0x001517B8 File Offset: 0x0014FBB8
	private void Start()
	{
		if (this.container == null && this.widgetContainer != null)
		{
			this.container = this.widgetContainer.gameObject;
			this.widgetContainer = null;
		}
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		this.Update();
		this.mStarted = true;
	}

	// Token: 0x06002E28 RID: 11816 RVA: 0x00151844 File Offset: 0x0014FC44
	private void Update()
	{
		if (this.mAnim != null && this.mAnim.isPlaying)
		{
			return;
		}
		if (this.style != UIStretch.Style.None)
		{
			UIWidget uiwidget = (!(this.container == null)) ? this.container.GetComponent<UIWidget>() : null;
			UIPanel uipanel = (!(this.container == null) || !(uiwidget == null)) ? this.container.GetComponent<UIPanel>() : null;
			float num = 1f;
			if (uiwidget != null)
			{
				Bounds bounds = uiwidget.CalculateBounds(base.transform.parent);
				this.mRect.x = bounds.min.x;
				this.mRect.y = bounds.min.y;
				this.mRect.width = bounds.size.x;
				this.mRect.height = bounds.size.y;
			}
			else if (uipanel != null)
			{
				if (uipanel.clipping == UIDrawCall.Clipping.None)
				{
					float num2 = (!(this.mRoot != null)) ? 0.5f : ((float)this.mRoot.activeHeight / (float)Screen.height * 0.5f);
					this.mRect.xMin = (float)(-(float)Screen.width) * num2;
					this.mRect.yMin = (float)(-(float)Screen.height) * num2;
					this.mRect.xMax = -this.mRect.xMin;
					this.mRect.yMax = -this.mRect.yMin;
				}
				else
				{
					Vector4 finalClipRegion = uipanel.finalClipRegion;
					this.mRect.x = finalClipRegion.x - finalClipRegion.z * 0.5f;
					this.mRect.y = finalClipRegion.y - finalClipRegion.w * 0.5f;
					this.mRect.width = finalClipRegion.z;
					this.mRect.height = finalClipRegion.w;
				}
			}
			else if (this.container != null)
			{
				Transform parent = base.transform.parent;
				Bounds bounds2 = (!(parent != null)) ? NGUIMath.CalculateRelativeWidgetBounds(this.container.transform) : NGUIMath.CalculateRelativeWidgetBounds(parent, this.container.transform);
				this.mRect.x = bounds2.min.x;
				this.mRect.y = bounds2.min.y;
				this.mRect.width = bounds2.size.x;
				this.mRect.height = bounds2.size.y;
			}
			else
			{
				if (!(this.uiCamera != null))
				{
					return;
				}
				this.mRect = this.uiCamera.pixelRect;
				if (this.mRoot != null)
				{
					num = this.mRoot.pixelSizeAdjustment;
				}
			}
			float num3 = this.mRect.width;
			float num4 = this.mRect.height;
			if (num != 1f && num4 > 1f)
			{
				float num5 = (float)this.mRoot.activeHeight / num4;
				num3 *= num5;
				num4 *= num5;
			}
			Vector3 vector = (!(this.mWidget != null)) ? this.mTrans.localScale : new Vector3((float)this.mWidget.width, (float)this.mWidget.height);
			if (this.style == UIStretch.Style.BasedOnHeight)
			{
				vector.x = this.relativeSize.x * num4;
				vector.y = this.relativeSize.y * num4;
			}
			else if (this.style == UIStretch.Style.FillKeepingRatio)
			{
				float num6 = num3 / num4;
				float num7 = this.initialSize.x / this.initialSize.y;
				if (num7 < num6)
				{
					float num8 = num3 / this.initialSize.x;
					vector.x = num3;
					vector.y = this.initialSize.y * num8;
				}
				else
				{
					float num9 = num4 / this.initialSize.y;
					vector.x = this.initialSize.x * num9;
					vector.y = num4;
				}
			}
			else if (this.style == UIStretch.Style.FitInternalKeepingRatio)
			{
				float num10 = num3 / num4;
				float num11 = this.initialSize.x / this.initialSize.y;
				if (num11 > num10)
				{
					float num12 = num3 / this.initialSize.x;
					vector.x = num3;
					vector.y = this.initialSize.y * num12;
				}
				else
				{
					float num13 = num4 / this.initialSize.y;
					vector.x = this.initialSize.x * num13;
					vector.y = num4;
				}
			}
			else
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					vector.x = this.relativeSize.x * num3;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					vector.y = this.relativeSize.y * num4;
				}
			}
			if (this.mSprite != null)
			{
				float num14 = (!(this.mSprite.atlas != null)) ? 1f : this.mSprite.atlas.pixelSize;
				vector.x -= this.borderPadding.x * num14;
				vector.y -= this.borderPadding.y * num14;
				if (this.style != UIStretch.Style.Vertical)
				{
					this.mSprite.width = Mathf.RoundToInt(vector.x);
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					this.mSprite.height = Mathf.RoundToInt(vector.y);
				}
				vector = Vector3.one;
			}
			else if (this.mWidget != null)
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					this.mWidget.width = Mathf.RoundToInt(vector.x - this.borderPadding.x);
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					this.mWidget.height = Mathf.RoundToInt(vector.y - this.borderPadding.y);
				}
				vector = Vector3.one;
			}
			else if (this.mPanel != null)
			{
				Vector4 baseClipRegion = this.mPanel.baseClipRegion;
				if (this.style != UIStretch.Style.Vertical)
				{
					baseClipRegion.z = vector.x - this.borderPadding.x;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					baseClipRegion.w = vector.y - this.borderPadding.y;
				}
				this.mPanel.baseClipRegion = baseClipRegion;
				vector = Vector3.one;
			}
			else
			{
				if (this.style != UIStretch.Style.Vertical)
				{
					vector.x -= this.borderPadding.x;
				}
				if (this.style != UIStretch.Style.Horizontal)
				{
					vector.y -= this.borderPadding.y;
				}
			}
			if (this.mTrans.localScale != vector)
			{
				this.mTrans.localScale = vector;
			}
			if (this.runOnlyOnce && Application.isPlaying)
			{
				base.enabled = false;
			}
		}
	}

	// Token: 0x04002D0D RID: 11533
	public Camera uiCamera;

	// Token: 0x04002D0E RID: 11534
	public GameObject container;

	// Token: 0x04002D0F RID: 11535
	public UIStretch.Style style;

	// Token: 0x04002D10 RID: 11536
	public bool runOnlyOnce = true;

	// Token: 0x04002D11 RID: 11537
	public Vector2 relativeSize = Vector2.one;

	// Token: 0x04002D12 RID: 11538
	public Vector2 initialSize = Vector2.one;

	// Token: 0x04002D13 RID: 11539
	public Vector2 borderPadding = Vector2.zero;

	// Token: 0x04002D14 RID: 11540
	[HideInInspector]
	[SerializeField]
	private UIWidget widgetContainer;

	// Token: 0x04002D15 RID: 11541
	private Transform mTrans;

	// Token: 0x04002D16 RID: 11542
	private UIWidget mWidget;

	// Token: 0x04002D17 RID: 11543
	private UISprite mSprite;

	// Token: 0x04002D18 RID: 11544
	private UIPanel mPanel;

	// Token: 0x04002D19 RID: 11545
	private UIRoot mRoot;

	// Token: 0x04002D1A RID: 11546
	private Animation mAnim;

	// Token: 0x04002D1B RID: 11547
	private Rect mRect;

	// Token: 0x04002D1C RID: 11548
	private bool mStarted;

	// Token: 0x02000637 RID: 1591
	[DoNotObfuscateNGUI]
	public enum Style
	{
		// Token: 0x04002D1E RID: 11550
		None,
		// Token: 0x04002D1F RID: 11551
		Horizontal,
		// Token: 0x04002D20 RID: 11552
		Vertical,
		// Token: 0x04002D21 RID: 11553
		Both,
		// Token: 0x04002D22 RID: 11554
		BasedOnHeight,
		// Token: 0x04002D23 RID: 11555
		FillKeepingRatio,
		// Token: 0x04002D24 RID: 11556
		FitInternalKeepingRatio
	}
}
