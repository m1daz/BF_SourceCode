using System;
using UnityEngine;

// Token: 0x02000537 RID: 1335
[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/Examples/UI Cursor")]
public class UICursor : MonoBehaviour
{
	// Token: 0x060025CC RID: 9676 RVA: 0x00118EA5 File Offset: 0x001172A5
	private void Awake()
	{
		UICursor.instance = this;
	}

	// Token: 0x060025CD RID: 9677 RVA: 0x00118EAD File Offset: 0x001172AD
	private void OnDestroy()
	{
		UICursor.instance = null;
	}

	// Token: 0x060025CE RID: 9678 RVA: 0x00118EB8 File Offset: 0x001172B8
	private void Start()
	{
		this.mTrans = base.transform;
		this.mSprite = base.GetComponentInChildren<UISprite>();
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		if (this.mSprite != null)
		{
			this.mAtlas = this.mSprite.atlas;
			this.mSpriteName = this.mSprite.spriteName;
			if (this.mSprite.depth < 100)
			{
				this.mSprite.depth = 100;
			}
		}
	}

	// Token: 0x060025CF RID: 9679 RVA: 0x00118F58 File Offset: 0x00117358
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		if (this.uiCamera != null)
		{
			mousePosition.x = Mathf.Clamp01(mousePosition.x / (float)Screen.width);
			mousePosition.y = Mathf.Clamp01(mousePosition.y / (float)Screen.height);
			this.mTrans.position = this.uiCamera.ViewportToWorldPoint(mousePosition);
			if (this.uiCamera.orthographic)
			{
				Vector3 localPosition = this.mTrans.localPosition;
				localPosition.x = Mathf.Round(localPosition.x);
				localPosition.y = Mathf.Round(localPosition.y);
				this.mTrans.localPosition = localPosition;
			}
		}
		else
		{
			mousePosition.x -= (float)Screen.width * 0.5f;
			mousePosition.y -= (float)Screen.height * 0.5f;
			mousePosition.x = Mathf.Round(mousePosition.x);
			mousePosition.y = Mathf.Round(mousePosition.y);
			this.mTrans.localPosition = mousePosition;
		}
	}

	// Token: 0x060025D0 RID: 9680 RVA: 0x00119080 File Offset: 0x00117480
	public static void Clear()
	{
		if (UICursor.instance != null && UICursor.instance.mSprite != null)
		{
			UICursor.Set(UICursor.instance.mAtlas, UICursor.instance.mSpriteName);
		}
	}

	// Token: 0x060025D1 RID: 9681 RVA: 0x001190C0 File Offset: 0x001174C0
	public static void Set(UIAtlas atlas, string sprite)
	{
		if (UICursor.instance != null && UICursor.instance.mSprite)
		{
			UICursor.instance.mSprite.atlas = atlas;
			UICursor.instance.mSprite.spriteName = sprite;
			UICursor.instance.mSprite.MakePixelPerfect();
			UICursor.instance.Update();
		}
	}

	// Token: 0x04002667 RID: 9831
	public static UICursor instance;

	// Token: 0x04002668 RID: 9832
	public Camera uiCamera;

	// Token: 0x04002669 RID: 9833
	private Transform mTrans;

	// Token: 0x0400266A RID: 9834
	private UISprite mSprite;

	// Token: 0x0400266B RID: 9835
	private UIAtlas mAtlas;

	// Token: 0x0400266C RID: 9836
	private string mSpriteName;
}
