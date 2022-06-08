using System;
using UnityEngine;

// Token: 0x02000629 RID: 1577
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
[AddComponentMenu("NGUI/UI/Localize")]
public class UILocalize : MonoBehaviour
{
	// Token: 0x17000338 RID: 824
	// (set) Token: 0x06002D7C RID: 11644 RVA: 0x0014D1D8 File Offset: 0x0014B5D8
	public string value
	{
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				UIWidget component = base.GetComponent<UIWidget>();
				UILabel uilabel = component as UILabel;
				UISprite uisprite = component as UISprite;
				if (uilabel != null)
				{
					UIInput uiinput = NGUITools.FindInParents<UIInput>(uilabel.gameObject);
					if (uiinput != null && uiinput.label == uilabel)
					{
						uiinput.defaultText = value;
					}
					else
					{
						uilabel.text = value;
					}
				}
				else if (uisprite != null)
				{
					UIButton uibutton = NGUITools.FindInParents<UIButton>(uisprite.gameObject);
					if (uibutton != null && uibutton.tweenTarget == uisprite.gameObject)
					{
						uibutton.normalSprite = value;
					}
					uisprite.spriteName = value;
					uisprite.MakePixelPerfect();
				}
			}
		}
	}

	// Token: 0x06002D7D RID: 11645 RVA: 0x0014D2A4 File Offset: 0x0014B6A4
	private void OnEnable()
	{
		if (this.mStarted)
		{
			this.OnLocalize();
		}
	}

	// Token: 0x06002D7E RID: 11646 RVA: 0x0014D2B7 File Offset: 0x0014B6B7
	private void Start()
	{
		this.mStarted = true;
		this.OnLocalize();
	}

	// Token: 0x06002D7F RID: 11647 RVA: 0x0014D2C8 File Offset: 0x0014B6C8
	private void OnLocalize()
	{
		if (string.IsNullOrEmpty(this.key))
		{
			UILabel component = base.GetComponent<UILabel>();
			if (component != null)
			{
				this.key = component.text;
			}
		}
		if (!string.IsNullOrEmpty(this.key))
		{
			this.value = Localization.Get(this.key, true);
		}
	}

	// Token: 0x04002CA5 RID: 11429
	public string key;

	// Token: 0x04002CA6 RID: 11430
	private bool mStarted;
}
