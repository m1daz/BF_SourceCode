using System;
using UnityEngine;

// Token: 0x02000546 RID: 1350
[RequireComponent(typeof(UIInput))]
[AddComponentMenu("NGUI/Examples/Chat Input")]
public class ChatInput : MonoBehaviour
{
	// Token: 0x0600260A RID: 9738 RVA: 0x0011A428 File Offset: 0x00118828
	private void Start()
	{
		this.mInput = base.GetComponent<UIInput>();
		this.mInput.label.maxLineCount = 1;
		if (this.fillWithDummyData && this.textList != null)
		{
			for (int i = 0; i < 30; i++)
			{
				this.textList.Add(string.Concat(new object[]
				{
					(i % 2 != 0) ? "[AAAAAA]" : "[FFFFFF]",
					"This is an example paragraph for the text list, testing line ",
					i,
					"[-]"
				}));
			}
		}
	}

	// Token: 0x0600260B RID: 9739 RVA: 0x0011A4CC File Offset: 0x001188CC
	public void OnSubmit()
	{
		if (this.textList != null)
		{
			string text = NGUIText.StripSymbols(this.mInput.value);
			if (!string.IsNullOrEmpty(text))
			{
				this.textList.Add(text);
				this.mInput.value = string.Empty;
				this.mInput.isSelected = false;
			}
		}
	}

	// Token: 0x040026C5 RID: 9925
	public UITextList textList;

	// Token: 0x040026C6 RID: 9926
	public bool fillWithDummyData;

	// Token: 0x040026C7 RID: 9927
	private UIInput mInput;
}
