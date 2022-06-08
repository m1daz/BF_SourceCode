using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkinEditor
{
	// Token: 0x0200034F RID: 847
	public class ColorPicker : MonoBehaviour
	{
		// Token: 0x06001AB4 RID: 6836 RVA: 0x000D6C8C File Offset: 0x000D508C
		public Vector2 GetTouchPointInTexture(Vector3 hitPoint)
		{
			float x = (hitPoint.x - this.cpCalcFactor.originPoint.x) / this.cpCalcFactor.f_DeltaX;
			float y = (hitPoint.y - this.cpCalcFactor.originPoint.y) / this.cpCalcFactor.f_DeltaY;
			return new Vector2(x, y);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000D6CEC File Offset: 0x000D50EC
		public void RGBValueChanged()
		{
			float value = this.RGBSliders[0].value;
			float value2 = this.RGBSliders[1].value;
			float value3 = this.RGBSliders[2].value;
			this.curSelectedColor = new Color(value, value2, value3, 1f);
			this.RGBValue[0].text = ((int)(value * 255f)).ToString();
			this.RGBValue[1].text = ((int)(value2 * 255f)).ToString();
			this.RGBValue[2].text = ((int)(value3 * 255f)).ToString();
			base.gameObject.SendMessageUpwards("SiwtchPaintColor", this.curSelectedColor, SendMessageOptions.DontRequireReceiver);
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x000D6DC0 File Offset: 0x000D51C0
		public void UserColorSelectEventChk()
		{
			if (Input.touchCount > 0)
			{
				Ray ray = SkinEditorDirector.mInstance.m_CurActivedCamera.ScreenPointToRay(Input.GetTouch(0).position);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, 100f) && raycastHit.transform == base.transform)
				{
					Vector2 touchPointInTexture = this.GetTouchPointInTexture(raycastHit.point);
					int x = (int)touchPointInTexture.x;
					int y = (int)touchPointInTexture.y;
					if (this.curSelectedColor == this.m_ColorTableTex.GetPixel(x, y))
					{
						return;
					}
					this.curSelectedColor = this.m_ColorTableTex.GetPixel(x, y);
					base.gameObject.SendMessageUpwards("SiwtchPaintColor", this.curSelectedColor, SendMessageOptions.DontRequireReceiver);
					if (SkinEditorDirector.mInstance.m_ColorHistroy.Count < 10)
					{
						SkinEditorDirector.mInstance.m_ColorHistroy.Add(this.curSelectedColor);
						this.m_ColorHistroy.Add(this.curSelectedColor);
					}
					else
					{
						SkinEditorDirector.mInstance.m_ColorHistroy.RemoveAt(0);
						SkinEditorDirector.mInstance.m_ColorHistroy.Add(this.curSelectedColor);
						this.m_ColorHistroy.RemoveAt(0);
						this.m_ColorHistroy.Add(this.curSelectedColor);
					}
				}
			}
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000D6F18 File Offset: 0x000D5318
		private void Awake()
		{
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000D6F1C File Offset: 0x000D531C
		private void Start()
		{
			this.cpCalcFactor = new ColorPickerCalcFactor();
			this.cpCalcFactor.f_Width = base.transform.localScale.x;
			this.cpCalcFactor.f_Height = base.transform.localScale.y;
			this.cpCalcFactor.p_Width = (float)this.m_ColorTableTex.width;
			this.cpCalcFactor.p_Height = (float)this.m_ColorTableTex.height;
			this.cpCalcFactor.f_DeltaX = this.cpCalcFactor.f_Width / this.cpCalcFactor.p_Width;
			this.cpCalcFactor.f_DeltaY = this.cpCalcFactor.f_Height / this.cpCalcFactor.p_Height;
			this.cpCalcFactor.originPoint = new Vector3(base.transform.position.x - base.transform.localScale.x / 2f, base.transform.position.y - base.transform.localScale.y / 2f);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000D704E File Offset: 0x000D544E
		private void Update()
		{
			this.UserColorSelectEventChk();
		}

		// Token: 0x04001CE2 RID: 7394
		private ColorPickerCalcFactor cpCalcFactor;

		// Token: 0x04001CE3 RID: 7395
		public Texture2D m_ColorTableTex;

		// Token: 0x04001CE4 RID: 7396
		public Color curSelectedColor;

		// Token: 0x04001CE5 RID: 7397
		public List<Color> m_ColorHistroy = new List<Color>();

		// Token: 0x04001CE6 RID: 7398
		public UISlider[] RGBSliders;

		// Token: 0x04001CE7 RID: 7399
		public UILabel[] RGBValue;
	}
}
