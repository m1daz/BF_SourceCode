using System;
using UnityEngine;

// Token: 0x0200063C RID: 1596
[AddComponentMenu("NGUI/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
	// Token: 0x1700037B RID: 891
	// (get) Token: 0x06002E50 RID: 11856 RVA: 0x00152EC3 File Offset: 0x001512C3
	public static bool isVisible
	{
		get
		{
			return UITooltip.mInstance != null && UITooltip.mInstance.mTarget == 1f;
		}
	}

	// Token: 0x06002E51 RID: 11857 RVA: 0x00152EE9 File Offset: 0x001512E9
	private void Awake()
	{
		UITooltip.mInstance = this;
	}

	// Token: 0x06002E52 RID: 11858 RVA: 0x00152EF1 File Offset: 0x001512F1
	private void OnDestroy()
	{
		UITooltip.mInstance = null;
	}

	// Token: 0x06002E53 RID: 11859 RVA: 0x00152EFC File Offset: 0x001512FC
	protected virtual void Start()
	{
		this.mTrans = base.transform;
		this.mWidgets = base.GetComponentsInChildren<UIWidget>();
		this.mPos = this.mTrans.localPosition;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.SetAlpha(0f);
	}

	// Token: 0x06002E54 RID: 11860 RVA: 0x00152F64 File Offset: 0x00151364
	protected virtual void Update()
	{
		if (this.mTooltip != UICamera.tooltipObject)
		{
			this.mTooltip = null;
			this.mTarget = 0f;
		}
		if (this.mCurrent != this.mTarget)
		{
			this.mCurrent = Mathf.Lerp(this.mCurrent, this.mTarget, RealTime.deltaTime * this.appearSpeed);
			if (Mathf.Abs(this.mCurrent - this.mTarget) < 0.001f)
			{
				this.mCurrent = this.mTarget;
			}
			this.SetAlpha(this.mCurrent * this.mCurrent);
			if (this.scalingTransitions)
			{
				Vector3 b = this.mSize * 0.25f;
				b.y = -b.y;
				Vector3 localScale = Vector3.one * (1.5f - this.mCurrent * 0.5f);
				Vector3 localPosition = Vector3.Lerp(this.mPos - b, this.mPos, this.mCurrent);
				this.mTrans.localPosition = localPosition;
				this.mTrans.localScale = localScale;
			}
		}
	}

	// Token: 0x06002E55 RID: 11861 RVA: 0x00153088 File Offset: 0x00151488
	protected virtual void SetAlpha(float val)
	{
		int i = 0;
		int num = this.mWidgets.Length;
		while (i < num)
		{
			UIWidget uiwidget = this.mWidgets[i];
			Color color = uiwidget.color;
			color.a = val;
			uiwidget.color = color;
			i++;
		}
	}

	// Token: 0x06002E56 RID: 11862 RVA: 0x001530D0 File Offset: 0x001514D0
	protected virtual void SetText(string tooltipText)
	{
		if (this.text != null && !string.IsNullOrEmpty(tooltipText))
		{
			this.mTarget = 1f;
			this.mTooltip = UICamera.tooltipObject;
			this.text.text = tooltipText;
			this.mPos = UICamera.lastEventPosition;
			Transform transform = this.text.transform;
			Vector3 localPosition = transform.localPosition;
			Vector3 localScale = transform.localScale;
			this.mSize = this.text.printedSize;
			this.mSize.x = this.mSize.x * localScale.x;
			this.mSize.y = this.mSize.y * localScale.y;
			if (this.background != null)
			{
				Vector4 border = this.background.border;
				this.mSize.x = this.mSize.x + (border.x + border.z + (localPosition.x - border.x) * 2f);
				this.mSize.y = this.mSize.y + (border.y + border.w + (-localPosition.y - border.y) * 2f);
				this.background.width = Mathf.RoundToInt(this.mSize.x);
				this.background.height = Mathf.RoundToInt(this.mSize.y);
			}
			if (this.uiCamera != null)
			{
				this.mPos.x = Mathf.Clamp01(this.mPos.x / (float)Screen.width);
				this.mPos.y = Mathf.Clamp01(this.mPos.y / (float)Screen.height);
				float num = this.uiCamera.orthographicSize / this.mTrans.parent.lossyScale.y;
				float num2 = (float)Screen.height * 0.5f / num;
				Vector2 vector = new Vector2(num2 * this.mSize.x / (float)Screen.width, num2 * this.mSize.y / (float)Screen.height);
				this.mPos.x = Mathf.Min(this.mPos.x, 1f - vector.x);
				this.mPos.y = Mathf.Max(this.mPos.y, vector.y);
				this.mTrans.position = this.uiCamera.ViewportToWorldPoint(this.mPos);
				this.mPos = this.mTrans.localPosition;
				this.mPos.x = Mathf.Round(this.mPos.x);
				this.mPos.y = Mathf.Round(this.mPos.y);
			}
			else
			{
				if (this.mPos.x + this.mSize.x > (float)Screen.width)
				{
					this.mPos.x = (float)Screen.width - this.mSize.x;
				}
				if (this.mPos.y - this.mSize.y < 0f)
				{
					this.mPos.y = this.mSize.y;
				}
				this.mPos.x = this.mPos.x - (float)Screen.width * 0.5f;
				this.mPos.y = this.mPos.y - (float)Screen.height * 0.5f;
			}
			this.mTrans.localPosition = this.mPos;
			if (this.tooltipRoot != null)
			{
				this.tooltipRoot.BroadcastMessage("UpdateAnchors");
			}
			else
			{
				this.text.BroadcastMessage("UpdateAnchors");
			}
		}
		else
		{
			this.mTooltip = null;
			this.mTarget = 0f;
		}
	}

	// Token: 0x06002E57 RID: 11863 RVA: 0x001534CF File Offset: 0x001518CF
	[Obsolete("Use UITooltip.Show instead")]
	public static void ShowText(string text)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(text);
		}
	}

	// Token: 0x06002E58 RID: 11864 RVA: 0x001534EC File Offset: 0x001518EC
	public static void Show(string text)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(text);
		}
	}

	// Token: 0x06002E59 RID: 11865 RVA: 0x00153509 File Offset: 0x00151909
	public static void Hide()
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.mTooltip = null;
			UITooltip.mInstance.mTarget = 0f;
		}
	}

	// Token: 0x04002D3B RID: 11579
	protected static UITooltip mInstance;

	// Token: 0x04002D3C RID: 11580
	public Camera uiCamera;

	// Token: 0x04002D3D RID: 11581
	public UILabel text;

	// Token: 0x04002D3E RID: 11582
	public GameObject tooltipRoot;

	// Token: 0x04002D3F RID: 11583
	public UISprite background;

	// Token: 0x04002D40 RID: 11584
	public float appearSpeed = 10f;

	// Token: 0x04002D41 RID: 11585
	public bool scalingTransitions = true;

	// Token: 0x04002D42 RID: 11586
	protected GameObject mTooltip;

	// Token: 0x04002D43 RID: 11587
	protected Transform mTrans;

	// Token: 0x04002D44 RID: 11588
	protected float mTarget;

	// Token: 0x04002D45 RID: 11589
	protected float mCurrent;

	// Token: 0x04002D46 RID: 11590
	protected Vector3 mPos;

	// Token: 0x04002D47 RID: 11591
	protected Vector3 mSize = Vector3.zero;

	// Token: 0x04002D48 RID: 11592
	protected UIWidget[] mWidgets;
}
