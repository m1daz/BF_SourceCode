using System;
using UnityEngine;

// Token: 0x020005E2 RID: 1506
[ExecuteInEditMode]
[RequireComponent(typeof(UIWidget))]
public class AnimatedColor : MonoBehaviour
{
	// Token: 0x06002AFD RID: 11005 RVA: 0x0013EA78 File Offset: 0x0013CE78
	private void OnEnable()
	{
		this.mWidget = base.GetComponent<UIWidget>();
		this.LateUpdate();
	}

	// Token: 0x06002AFE RID: 11006 RVA: 0x0013EA8C File Offset: 0x0013CE8C
	private void LateUpdate()
	{
		this.mWidget.color = this.color;
	}

	// Token: 0x04002AAF RID: 10927
	public Color color = Color.white;

	// Token: 0x04002AB0 RID: 10928
	private UIWidget mWidget;
}
