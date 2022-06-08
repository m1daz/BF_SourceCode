using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkinEditor
{
	// Token: 0x02000357 RID: 855
	public class SkinDrawingPanel : MonoBehaviour
	{
		// Token: 0x06001ABF RID: 6847 RVA: 0x000D7292 File Offset: 0x000D5692
		public void SiwtchPaintColor(Color cc)
		{
			this.m_CurPaintColor = cc;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x000D729C File Offset: 0x000D569C
		public Vector2 GetTouchPointInTexture(Vector3 hitPoint)
		{
			float x = (hitPoint.x - this.sCalcFactor.originPoint.x) / this.sCalcFactor.f_DeltaX;
			float y = (hitPoint.y - this.sCalcFactor.originPoint.y) / this.sCalcFactor.f_DeltaY;
			return new Vector2(x, y);
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x000D72FC File Offset: 0x000D56FC
		public void UserDrawingEventChk()
		{
			if (Input.touchCount > 0)
			{
				Ray ray = SkinEditorDirector.mInstance.m_CurActivedCamera.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, 100f) && raycastHit.transform == this.m_Canvas)
				{
					Vector2 touchPointInTexture = this.GetTouchPointInTexture(raycastHit.point);
					int num = Math.Min((int)touchPointInTexture.x, (int)this.curSideRect.width - 1);
					int num2 = Math.Min((int)touchPointInTexture.y, (int)this.curSideRect.height - 1);
					if (this.m_BrushType == BrushType.Sucker && this.colorListNew[num2 + (int)this.curSideRect.height * num] == this.m_CurPaintColor)
					{
						return;
					}
					if (this.m_BrushType == BrushType.Pencil && this.colorListNew[num2 + (int)this.curSideRect.height * num] == this.m_CurPaintColor)
					{
						return;
					}
					if (this.m_BrushType == BrushType.PaintBucket && this.m_BrushContinuousOpCount >= 1 && this.colorListNew[num2 + (int)this.curSideRect.height * num] == this.m_CurPaintColor)
					{
						return;
					}
					if (this.m_BrushType == BrushType.Pencil)
					{
						this.colorListNew[num2 + (int)this.curSideRect.height * num] = this.m_CurPaintColor;
						this.m_EditingTex.SetPixel((int)this.curSideRect.x + num, (int)this.curSideRect.y + num2, this.m_CurPaintColor);
						this.m_EditingTex.Apply();
						this.m_BrushContinuousOpCount = 0;
					}
					if (this.m_BrushType == BrushType.Sucker)
					{
						this.m_CurPaintColor = this.m_EditingTex.GetPixel((int)this.curSideRect.x + num, (int)this.curSideRect.y + num2);
						UISkinEditDirector.mInstance.curColorSprite.color = this.m_CurPaintColor;
						UISkinEditDirector.mInstance.SaveBtnPressed();
						SkinEditorDirector.mInstance.isSaved = false;
						return;
					}
					if (this.m_BrushType == BrushType.PaintBucket)
					{
						int num3 = 0;
						while ((float)num3 < this.curSideRect.width)
						{
							int num4 = 0;
							while ((float)num4 < this.curSideRect.height)
							{
								this.colorListNew[num4 + (int)this.curSideRect.height * num3] = this.m_CurPaintColor;
								this.m_EditingTex.SetPixel((int)this.curSideRect.x + num3, (int)this.curSideRect.y + num4, this.colorListNew[num4 + (int)this.curSideRect.height * num3]);
								num4++;
							}
							num3++;
						}
						this.m_EditingTex.Apply();
						this.m_BrushContinuousOpCount++;
					}
					SkinEditorDirector.mInstance.isSaved = false;
					if (this.indexInHistroy != this.colorListHistroy.Count - 1)
					{
						this.colorListHistroy.RemoveRange(this.indexInHistroy + 1, this.colorListHistroy.Count - this.indexInHistroy - 1);
					}
					Color[] array = new Color[this.colorListNew.Length];
					this.colorListNew.CopyTo(array, 0);
					this.colorListHistroy.Add(array);
					this.indexInHistroy = this.colorListHistroy.Count - 1;
				}
			}
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x000D76A8 File Offset: 0x000D5AA8
		private void Awake()
		{
			SkinEditorDirector.mInstance = GameObject.Find("SkinEditorDirector").GetComponent<SkinEditorDirector>();
			this.m_EditingTex = SkinEditorDirector.mInstance.m_EditingTex;
			this.m_CanvasMt = this.m_Canvas.GetComponent<MeshRenderer>().material;
			this.m_Canvas.position = new Vector3(0.5f, -1f, 0f);
			this.m_CurPaintColor = UIUserDataController.GetCurPaletteColor();
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000D7720 File Offset: 0x000D5B20
		private void OnEnable()
		{
			this.m_EditingTex = SkinEditorDirector.mInstance.m_EditingTex;
			this.m_CanvasMt.mainTexture = this.m_EditingTex;
			this.curSideRect = SkinDefine.GetSkinSideRect(SkinEditorDirector.mInstance.m_CurModule, SkinEditorDirector.mInstance.m_CurModuleSide);
			this.m_Canvas.localScale = new Vector3(this.curSideRect.width * 0.4f, this.curSideRect.height * 0.4f, 0.1f);
			this.m_CanvasMt.mainTextureOffset = new Vector2(this.curSideRect.x / (float)this.m_CanvasMt.mainTexture.width, this.curSideRect.y / (float)this.m_CanvasMt.mainTexture.height);
			this.m_CanvasMt.mainTextureScale = new Vector2(this.curSideRect.width / (float)this.m_CanvasMt.mainTexture.width, this.curSideRect.height / (float)this.m_CanvasMt.mainTexture.height);
			this.colorListBackup = new Color[(int)(this.curSideRect.width * this.curSideRect.height)];
			this.colorListNew = new Color[(int)(this.curSideRect.width * this.curSideRect.height)];
			int num = 0;
			while ((float)num < this.curSideRect.width)
			{
				int num2 = 0;
				while ((float)num2 < this.curSideRect.height)
				{
					this.colorListBackup[num2 + (int)this.curSideRect.height * num] = this.m_EditingTex.GetPixel((int)this.curSideRect.x + num, (int)this.curSideRect.y + num2);
					this.colorListNew[num2 + (int)this.curSideRect.height * num] = this.m_EditingTex.GetPixel((int)this.curSideRect.x + num, (int)this.curSideRect.y + num2);
					num2++;
				}
				num++;
			}
			this.indexInHistroy = 0;
			this.colorListHistroy.Clear();
			Color[] array = new Color[this.colorListNew.Length];
			this.colorListNew.CopyTo(array, 0);
			this.colorListHistroy.Add(array);
			this.sCalcFactor = new SkinCanvasCalcFactor();
			this.sCalcFactor.f_Width = this.m_Canvas.localScale.x;
			this.sCalcFactor.f_Height = this.m_Canvas.localScale.y;
			this.sCalcFactor.p_Width = this.curSideRect.width;
			this.sCalcFactor.p_Height = this.curSideRect.height;
			this.sCalcFactor.f_DeltaX = this.sCalcFactor.f_Width / this.sCalcFactor.p_Width;
			this.sCalcFactor.f_DeltaY = this.sCalcFactor.f_Height / this.sCalcFactor.p_Height;
			this.sCalcFactor.originPoint = new Vector3(this.m_Canvas.position.x - this.m_Canvas.localScale.x / 2f, this.m_Canvas.position.y - this.m_Canvas.localScale.y / 2f);
			this.m_Canvas.position = new Vector3(0.5f, -1f, 0f);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000D7AC4 File Offset: 0x000D5EC4
		private void OnDisable()
		{
			this.m_BrushContinuousOpCount = 0;
			this.indexInHistroy = 0;
			this.colorListHistroy.Clear();
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000D7ADF File Offset: 0x000D5EDF
		private void Start()
		{
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000D7AE1 File Offset: 0x000D5EE1
		private void Update()
		{
			this.UserDrawingEventChk();
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000D7AEC File Offset: 0x000D5EEC
		public void SwitchColorPicker()
		{
			this.isColorPickerOpen = !this.isColorPickerOpen;
			if (this.isColorPickerOpen)
			{
				this.m_ColorPickerObj.SetActive(true);
				this.m_RGBColorPickerObj.SetActive(true);
				this.m_Canvas.gameObject.SetActive(false);
			}
			else
			{
				this.m_ColorPickerObj.SetActive(false);
				this.m_RGBColorPickerObj.SetActive(false);
				this.m_Canvas.gameObject.SetActive(true);
			}
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000D7B6C File Offset: 0x000D5F6C
		public void OpenColorPicker()
		{
			this.isColorPickerOpen = true;
			if (this.isColorPickerOpen)
			{
				this.m_ColorPickerObj.SetActive(true);
				this.m_RGBColorPickerObj.SetActive(true);
				this.m_Canvas.gameObject.SetActive(false);
			}
			else
			{
				this.m_ColorPickerObj.SetActive(false);
				this.m_RGBColorPickerObj.SetActive(false);
				this.m_Canvas.gameObject.SetActive(true);
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000D7BE4 File Offset: 0x000D5FE4
		public void CloseColorPicker()
		{
			this.isColorPickerOpen = false;
			if (this.isColorPickerOpen)
			{
				this.m_ColorPickerObj.SetActive(true);
				this.m_RGBColorPickerObj.SetActive(true);
				this.m_Canvas.gameObject.SetActive(false);
			}
			else
			{
				this.m_ColorPickerObj.SetActive(false);
				this.m_RGBColorPickerObj.SetActive(false);
				this.m_Canvas.gameObject.SetActive(true);
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000D7C5A File Offset: 0x000D605A
		public bool CanUnDo()
		{
			return this.indexInHistroy > 0;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000D7C6C File Offset: 0x000D606C
		public void UnDo()
		{
			if (this.indexInHistroy <= 0)
			{
				return;
			}
			this.indexInHistroy--;
			this.colorListHistroy[this.indexInHistroy].CopyTo(this.colorListNew, 0);
			int num = 0;
			while ((float)num < this.curSideRect.width)
			{
				int num2 = 0;
				while ((float)num2 < this.curSideRect.height)
				{
					this.m_EditingTex.SetPixel((int)this.curSideRect.x + num, (int)this.curSideRect.y + num2, this.colorListNew[num2 + (int)this.curSideRect.height * num]);
					num2++;
				}
				num++;
			}
			this.m_EditingTex.Apply();
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000D7D3D File Offset: 0x000D613D
		public bool CanReDo()
		{
			return this.indexInHistroy < this.colorListHistroy.Count - 1;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000D7D5C File Offset: 0x000D615C
		public void ReDo()
		{
			if (this.indexInHistroy >= this.colorListHistroy.Count - 1)
			{
				return;
			}
			this.indexInHistroy++;
			this.colorListHistroy[this.indexInHistroy].CopyTo(this.colorListNew, 0);
			int num = 0;
			while ((float)num < this.curSideRect.width)
			{
				int num2 = 0;
				while ((float)num2 < this.curSideRect.height)
				{
					this.m_EditingTex.SetPixel((int)this.curSideRect.x + num, (int)this.curSideRect.y + num2, this.colorListNew[num2 + (int)this.curSideRect.height * num]);
					num2++;
				}
				num++;
			}
			this.m_EditingTex.Apply();
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000D7E39 File Offset: 0x000D6239
		public BrushType GetCurrentBrushType()
		{
			return this.m_BrushType;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000D7E41 File Offset: 0x000D6241
		public void SwitchBrushToSucker()
		{
			this.m_BrushType = BrushType.Sucker;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000D7E4A File Offset: 0x000D624A
		public void SwitchBrushToPencil()
		{
			this.m_BrushType = BrushType.Pencil;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000D7E53 File Offset: 0x000D6253
		public void SwitchBrushToPaintBucket()
		{
			this.m_BrushType = BrushType.PaintBucket;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000D7E5C File Offset: 0x000D625C
		private Color NoisePixel(Color cc, float factor)
		{
			Color black = Color.black;
			float num = UnityEngine.Random.Range(0f, factor);
			int num2 = UnityEngine.Random.Range(0, 2);
			float num3 = (num2 != 0) ? (-num) : num;
			float num4 = (num2 != 0) ? (-num) : num;
			float num5 = (num2 != 0) ? (-num) : num;
			black.r = Mathf.Min(1f, Mathf.Max(0f, cc.r + num3));
			black.g = Mathf.Min(1f, Mathf.Max(0f, cc.g + num4));
			black.b = Mathf.Min(1f, Mathf.Max(0f, cc.b + num5));
			black.a = cc.a;
			return black;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x000D7F2F File Offset: 0x000D632F
		public void SetNoiseFactor(int iFactor)
		{
			this.m_NoiseFactor = (float)iFactor / 100f;
			this.m_NoiseFactor = Mathf.Min(Mathf.Max(0.01f, this.m_NoiseFactor), 0.1f);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x000D7F60 File Offset: 0x000D6360
		public void AddNoise()
		{
			int num = 0;
			while ((float)num < this.curSideRect.width)
			{
				int num2 = 0;
				while ((float)num2 < this.curSideRect.height)
				{
					this.colorListNew[num2 + (int)this.curSideRect.height * num] = this.NoisePixel(this.colorListNew[num2 + (int)this.curSideRect.height * num], this.m_NoiseFactor);
					this.m_EditingTex.SetPixel((int)this.curSideRect.x + num, (int)this.curSideRect.y + num2, this.colorListNew[num2 + (int)this.curSideRect.height * num]);
					num2++;
				}
				num++;
			}
			this.m_EditingTex.Apply();
			SkinEditorDirector.mInstance.isSaved = false;
			if (this.indexInHistroy != this.colorListHistroy.Count - 1)
			{
				this.colorListHistroy.RemoveRange(this.indexInHistroy + 1, this.colorListHistroy.Count - this.indexInHistroy - 1);
			}
			Color[] array = new Color[this.colorListNew.Length];
			this.colorListNew.CopyTo(array, 0);
			this.colorListHistroy.Add(array);
			this.indexInHistroy = this.colorListHistroy.Count - 1;
		}

		// Token: 0x04001D12 RID: 7442
		private Rect curSideRect;

		// Token: 0x04001D13 RID: 7443
		public Transform m_Canvas;

		// Token: 0x04001D14 RID: 7444
		private Material m_CanvasMt;

		// Token: 0x04001D15 RID: 7445
		public Texture2D m_EditingTex;

		// Token: 0x04001D16 RID: 7446
		public Color[] colorListBackup;

		// Token: 0x04001D17 RID: 7447
		public Color[] colorListNew;

		// Token: 0x04001D18 RID: 7448
		public List<Color[]> colorListHistroy = new List<Color[]>();

		// Token: 0x04001D19 RID: 7449
		public int indexInHistroy;

		// Token: 0x04001D1A RID: 7450
		private SkinCanvasCalcFactor sCalcFactor;

		// Token: 0x04001D1B RID: 7451
		public bool isColorPickerOpen;

		// Token: 0x04001D1C RID: 7452
		public GameObject m_ColorPickerObj;

		// Token: 0x04001D1D RID: 7453
		public GameObject m_RGBColorPickerObj;

		// Token: 0x04001D1E RID: 7454
		public Color m_CurPaintColor = Color.blue;

		// Token: 0x04001D1F RID: 7455
		public BrushType m_BrushType = BrushType.Pencil;

		// Token: 0x04001D20 RID: 7456
		public int m_BrushContinuousOpCount;

		// Token: 0x04001D21 RID: 7457
		public float m_NoiseFactor = 0.05f;
	}
}
