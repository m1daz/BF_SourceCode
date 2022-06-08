using System;
using UnityEngine;

// Token: 0x020005E1 RID: 1505
[ExecuteInEditMode]
public class AnimatedAlpha : MonoBehaviour
{
	// Token: 0x06002AFA RID: 11002 RVA: 0x0013E9F4 File Offset: 0x0013CDF4
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.mPanel = base.GetComponent<UIPanel>();
		this.LateUpdate();
	}

	// Token: 0x06002AFB RID: 11003 RVA: 0x0013EA14 File Offset: 0x0013CE14
	private void LateUpdate()
	{
		if (this.mWidget != null)
		{
			this.mWidget.alpha = this.alpha;
		}
		if (this.mPanel != null)
		{
			this.mPanel.alpha = this.alpha;
		}
	}

	// Token: 0x04002AAC RID: 10924
	[Range(0f, 1f)]
	public float alpha = 1f;

	// Token: 0x04002AAD RID: 10925
	private UIWidget mWidget;

	// Token: 0x04002AAE RID: 10926
	private UIPanel mPanel;
}
