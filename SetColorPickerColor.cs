using System;
using UnityEngine;

// Token: 0x02000552 RID: 1362
[RequireComponent(typeof(UIWidget))]
public class SetColorPickerColor : MonoBehaviour
{
	// Token: 0x06002631 RID: 9777 RVA: 0x0011B848 File Offset: 0x00119C48
	public void SetToCurrent()
	{
		if (this.mWidget == null)
		{
			this.mWidget = base.GetComponent<UIWidget>();
		}
		if (UIColorPicker.current != null)
		{
			this.mWidget.color = UIColorPicker.current.value;
		}
	}

	// Token: 0x040026EE RID: 9966
	[NonSerialized]
	private UIWidget mWidget;
}
