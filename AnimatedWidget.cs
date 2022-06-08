using System;
using UnityEngine;

// Token: 0x020005E3 RID: 1507
[ExecuteInEditMode]
public class AnimatedWidget : MonoBehaviour
{
	// Token: 0x06002B00 RID: 11008 RVA: 0x0013EABD File Offset: 0x0013CEBD
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.LateUpdate();
	}

	// Token: 0x06002B01 RID: 11009 RVA: 0x0013EAD1 File Offset: 0x0013CED1
	private void LateUpdate()
	{
		if (this.mWidget != null)
		{
			this.mWidget.width = Mathf.RoundToInt(this.width);
			this.mWidget.height = Mathf.RoundToInt(this.height);
		}
	}

	// Token: 0x04002AB1 RID: 10929
	public float width = 1f;

	// Token: 0x04002AB2 RID: 10930
	public float height = 1f;

	// Token: 0x04002AB3 RID: 10931
	private UIWidget mWidget;
}
