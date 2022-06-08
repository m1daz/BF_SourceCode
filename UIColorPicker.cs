using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200061A RID: 1562
[RequireComponent(typeof(UITexture))]
public class UIColorPicker : MonoBehaviour
{
	// Token: 0x06002C8F RID: 11407 RVA: 0x00147204 File Offset: 0x00145604
	private void Start()
	{
		this.mTrans = base.transform;
		this.mUITex = base.GetComponent<UITexture>();
		this.mCam = UICamera.FindCameraForLayer(base.gameObject.layer);
		this.mWidth = this.mUITex.width;
		this.mHeight = this.mUITex.height;
		Color[] array = new Color[this.mWidth * this.mHeight];
		for (int i = 0; i < this.mHeight; i++)
		{
			float y = ((float)i - 1f) / (float)this.mHeight;
			for (int j = 0; j < this.mWidth; j++)
			{
				float x = ((float)j - 1f) / (float)this.mWidth;
				int num = j + i * this.mWidth;
				array[num] = UIColorPicker.Sample(x, y);
			}
		}
		this.mTex = new Texture2D(this.mWidth, this.mHeight, TextureFormat.RGB24, false);
		this.mTex.SetPixels(array);
		this.mTex.filterMode = FilterMode.Trilinear;
		this.mTex.wrapMode = TextureWrapMode.Clamp;
		this.mTex.Apply();
		this.mUITex.mainTexture = this.mTex;
		this.Select(this.value);
	}

	// Token: 0x06002C90 RID: 11408 RVA: 0x0014734C File Offset: 0x0014574C
	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(this.mTex);
		this.mTex = null;
	}

	// Token: 0x06002C91 RID: 11409 RVA: 0x00147360 File Offset: 0x00145760
	private void OnPress(bool pressed)
	{
		if (base.enabled && pressed && UICamera.currentScheme != UICamera.ControlScheme.Controller)
		{
			this.Sample();
		}
	}

	// Token: 0x06002C92 RID: 11410 RVA: 0x00147384 File Offset: 0x00145784
	private void OnDrag(Vector2 delta)
	{
		if (base.enabled)
		{
			this.Sample();
		}
	}

	// Token: 0x06002C93 RID: 11411 RVA: 0x00147398 File Offset: 0x00145798
	private void OnPan(Vector2 delta)
	{
		if (base.enabled)
		{
			this.mPos.x = Mathf.Clamp01(this.mPos.x + delta.x);
			this.mPos.y = Mathf.Clamp01(this.mPos.y + delta.y);
			this.Select(this.mPos);
		}
	}

	// Token: 0x06002C94 RID: 11412 RVA: 0x00147404 File Offset: 0x00145804
	private void Sample()
	{
		Vector3 vector = UICamera.lastEventPosition;
		vector = this.mCam.cachedCamera.ScreenToWorldPoint(vector);
		vector = this.mTrans.InverseTransformPoint(vector);
		Vector3[] localCorners = this.mUITex.localCorners;
		this.mPos.x = Mathf.Clamp01((vector.x - localCorners[0].x) / (localCorners[2].x - localCorners[0].x));
		this.mPos.y = Mathf.Clamp01((vector.y - localCorners[0].y) / (localCorners[2].y - localCorners[0].y));
		if (this.selectionWidget != null)
		{
			vector.x = Mathf.Lerp(localCorners[0].x, localCorners[2].x, this.mPos.x);
			vector.y = Mathf.Lerp(localCorners[0].y, localCorners[2].y, this.mPos.y);
			vector = this.mTrans.TransformPoint(vector);
			this.selectionWidget.transform.OverlayPosition(vector, this.mCam.cachedCamera);
		}
		this.value = UIColorPicker.Sample(this.mPos.x, this.mPos.y);
		UIColorPicker.current = this;
		EventDelegate.Execute(this.onChange);
		UIColorPicker.current = null;
	}

	// Token: 0x06002C95 RID: 11413 RVA: 0x00147594 File Offset: 0x00145994
	public void Select(Vector2 v)
	{
		v.x = Mathf.Clamp01(v.x);
		v.y = Mathf.Clamp01(v.y);
		this.mPos = v;
		if (this.selectionWidget != null)
		{
			Vector3[] localCorners = this.mUITex.localCorners;
			v.x = Mathf.Lerp(localCorners[0].x, localCorners[2].x, this.mPos.x);
			v.y = Mathf.Lerp(localCorners[0].y, localCorners[2].y, this.mPos.y);
			v = this.mTrans.TransformPoint(v);
			this.selectionWidget.transform.OverlayPosition(v, this.mCam.cachedCamera);
		}
		this.value = UIColorPicker.Sample(this.mPos.x, this.mPos.y);
		UIColorPicker.current = this;
		EventDelegate.Execute(this.onChange);
		UIColorPicker.current = null;
	}

	// Token: 0x06002C96 RID: 11414 RVA: 0x001476BC File Offset: 0x00145ABC
	public Vector2 Select(Color c)
	{
		if (this.mUITex == null)
		{
			this.value = c;
			return this.mPos;
		}
		float num = float.MaxValue;
		for (int i = 0; i < this.mHeight; i++)
		{
			float y = ((float)i - 1f) / (float)this.mHeight;
			for (int j = 0; j < this.mWidth; j++)
			{
				float x = ((float)j - 1f) / (float)this.mWidth;
				Color color = UIColorPicker.Sample(x, y);
				Color color2 = color;
				color2.r -= c.r;
				color2.g -= c.g;
				color2.b -= c.b;
				float num2 = color2.r * color2.r + color2.g * color2.g + color2.b * color2.b;
				if (num2 < num)
				{
					num = num2;
					this.mPos.x = x;
					this.mPos.y = y;
				}
			}
		}
		if (this.selectionWidget != null)
		{
			Vector3[] localCorners = this.mUITex.localCorners;
			Vector3 vector;
			vector.x = Mathf.Lerp(localCorners[0].x, localCorners[2].x, this.mPos.x);
			vector.y = Mathf.Lerp(localCorners[0].y, localCorners[2].y, this.mPos.y);
			vector.z = 0f;
			vector = this.mTrans.TransformPoint(vector);
			this.selectionWidget.transform.OverlayPosition(vector, this.mCam.cachedCamera);
		}
		this.value = c;
		UIColorPicker.current = this;
		EventDelegate.Execute(this.onChange);
		UIColorPicker.current = null;
		return this.mPos;
	}

	// Token: 0x06002C97 RID: 11415 RVA: 0x001478C0 File Offset: 0x00145CC0
	public static Color Sample(float x, float y)
	{
		if (UIColorPicker.mRed == null)
		{
			UIColorPicker.mRed = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 1f),
				new Keyframe(0.14285715f, 1f),
				new Keyframe(0.2857143f, 0f),
				new Keyframe(0.42857143f, 0f),
				new Keyframe(0.5714286f, 0f),
				new Keyframe(0.71428573f, 1f),
				new Keyframe(0.85714287f, 1f),
				new Keyframe(1f, 0.5f)
			});
			UIColorPicker.mGreen = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f),
				new Keyframe(0.14285715f, 1f),
				new Keyframe(0.2857143f, 1f),
				new Keyframe(0.42857143f, 1f),
				new Keyframe(0.5714286f, 0f),
				new Keyframe(0.71428573f, 0f),
				new Keyframe(0.85714287f, 0f),
				new Keyframe(1f, 0.5f)
			});
			UIColorPicker.mBlue = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f),
				new Keyframe(0.14285715f, 0f),
				new Keyframe(0.2857143f, 0f),
				new Keyframe(0.42857143f, 1f),
				new Keyframe(0.5714286f, 1f),
				new Keyframe(0.71428573f, 1f),
				new Keyframe(0.85714287f, 0f),
				new Keyframe(1f, 0.5f)
			});
		}
		Vector3 a = new Vector3(UIColorPicker.mRed.Evaluate(x), UIColorPicker.mGreen.Evaluate(x), UIColorPicker.mBlue.Evaluate(x));
		if (y < 0.5f)
		{
			y *= 2f;
			a.x *= y;
			a.y *= y;
			a.z *= y;
		}
		else
		{
			a = Vector3.Lerp(a, Vector3.one, y * 2f - 1f);
		}
		return new Color(a.x, a.y, a.z, 1f);
	}

	// Token: 0x04002C00 RID: 11264
	public static UIColorPicker current;

	// Token: 0x04002C01 RID: 11265
	public Color value = Color.white;

	// Token: 0x04002C02 RID: 11266
	public UIWidget selectionWidget;

	// Token: 0x04002C03 RID: 11267
	public List<EventDelegate> onChange = new List<EventDelegate>();

	// Token: 0x04002C04 RID: 11268
	[NonSerialized]
	private Transform mTrans;

	// Token: 0x04002C05 RID: 11269
	[NonSerialized]
	private UITexture mUITex;

	// Token: 0x04002C06 RID: 11270
	[NonSerialized]
	private Texture2D mTex;

	// Token: 0x04002C07 RID: 11271
	[NonSerialized]
	private UICamera mCam;

	// Token: 0x04002C08 RID: 11272
	[NonSerialized]
	private Vector2 mPos;

	// Token: 0x04002C09 RID: 11273
	[NonSerialized]
	private int mWidth;

	// Token: 0x04002C0A RID: 11274
	[NonSerialized]
	private int mHeight;

	// Token: 0x04002C0B RID: 11275
	private static AnimationCurve mRed;

	// Token: 0x04002C0C RID: 11276
	private static AnimationCurve mGreen;

	// Token: 0x04002C0D RID: 11277
	private static AnimationCurve mBlue;
}
