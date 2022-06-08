using System;
using UnityEngine;

// Token: 0x0200044C RID: 1100
public class ToolboxFingerEventsSample : SampleBase
{
	// Token: 0x06001FDD RID: 8157 RVA: 0x000F116F File Offset: 0x000EF56F
	private void ToggleLight1()
	{
		this.light1.enabled = !this.light1.enabled;
	}

	// Token: 0x06001FDE RID: 8158 RVA: 0x000F118A File Offset: 0x000EF58A
	private void ToggleLight2()
	{
		this.light2.enabled = !this.light2.enabled;
	}

	// Token: 0x06001FDF RID: 8159 RVA: 0x000F11A5 File Offset: 0x000EF5A5
	protected override string GetHelpText()
	{
		return "This sample demonstrates the use of the toolbox scripts TBFingerDown and TBFingerUp. It also shows how you can use the message target property to turn the light on & off.";
	}

	// Token: 0x040020E5 RID: 8421
	public Light light1;

	// Token: 0x040020E6 RID: 8422
	public Light light2;
}
