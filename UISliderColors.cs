using System;
using UnityEngine;

// Token: 0x02000556 RID: 1366
[AddComponentMenu("NGUI/Examples/Slider Colors")]
public class UISliderColors : MonoBehaviour
{
	// Token: 0x0600263D RID: 9789 RVA: 0x0011BB12 File Offset: 0x00119F12
	private void Start()
	{
		this.mBar = base.GetComponent<UIProgressBar>();
		this.mSprite = base.GetComponent<UIBasicSprite>();
		this.Update();
	}

	// Token: 0x0600263E RID: 9790 RVA: 0x0011BB34 File Offset: 0x00119F34
	private void Update()
	{
		if (this.sprite == null || this.colors.Length == 0)
		{
			return;
		}
		float num = (!(this.mBar != null)) ? this.mSprite.fillAmount : this.mBar.value;
		num *= (float)(this.colors.Length - 1);
		int num2 = Mathf.FloorToInt(num);
		Color color = this.colors[0];
		if (num2 >= 0)
		{
			if (num2 + 1 < this.colors.Length)
			{
				float t = num - (float)num2;
				color = Color.Lerp(this.colors[num2], this.colors[num2 + 1], t);
			}
			else if (num2 < this.colors.Length)
			{
				color = this.colors[num2];
			}
			else
			{
				color = this.colors[this.colors.Length - 1];
			}
		}
		color.a = this.sprite.color.a;
		this.sprite.color = color;
	}

	// Token: 0x040026F6 RID: 9974
	public UISprite sprite;

	// Token: 0x040026F7 RID: 9975
	public Color[] colors = new Color[]
	{
		Color.red,
		Color.yellow,
		Color.green
	};

	// Token: 0x040026F8 RID: 9976
	private UIProgressBar mBar;

	// Token: 0x040026F9 RID: 9977
	private UIBasicSprite mSprite;
}
