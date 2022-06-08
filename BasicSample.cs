using System;

// Token: 0x0200043E RID: 1086
public class BasicSample : SampleBase
{
	// Token: 0x06001F8B RID: 8075 RVA: 0x000EFE95 File Offset: 0x000EE295
	protected override string GetHelpText()
	{
		return this.helpText;
	}

	// Token: 0x06001F8C RID: 8076 RVA: 0x000EFE9D File Offset: 0x000EE29D
	protected override void Start()
	{
		base.Start();
		base.UI.StatusText = this.statusText;
	}

	// Token: 0x04002099 RID: 8345
	public string helpText = "Help text here";

	// Token: 0x0400209A RID: 8346
	public string statusText = string.Empty;
}
