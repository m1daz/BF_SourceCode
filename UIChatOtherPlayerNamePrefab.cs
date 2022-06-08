using System;
using UnityEngine;

// Token: 0x0200028D RID: 653
public class UIChatOtherPlayerNamePrefab : MonoBehaviour
{
	// Token: 0x06001293 RID: 4755 RVA: 0x000A63DE File Offset: 0x000A47DE
	private void Start()
	{
	}

	// Token: 0x06001294 RID: 4756 RVA: 0x000A63E0 File Offset: 0x000A47E0
	private void Update()
	{
	}

	// Token: 0x06001295 RID: 4757 RVA: 0x000A63E2 File Offset: 0x000A47E2
	public void ToggleValueChanged()
	{
		if (this.toggle.value)
		{
			UIChatSystemDirector.mInstance.curPrivateChatIndex = this.index;
		}
	}

	// Token: 0x0400154B RID: 5451
	public int index;

	// Token: 0x0400154C RID: 5452
	public UIToggle toggle;

	// Token: 0x0400154D RID: 5453
	public UILabel label;
}
