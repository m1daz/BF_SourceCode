using System;

// Token: 0x020000FB RID: 251
public class RaiseEventOptions
{
	// Token: 0x040006D0 RID: 1744
	public static readonly RaiseEventOptions Default = new RaiseEventOptions();

	// Token: 0x040006D1 RID: 1745
	public EventCaching CachingOption;

	// Token: 0x040006D2 RID: 1746
	public byte InterestGroup;

	// Token: 0x040006D3 RID: 1747
	public int[] TargetActors;

	// Token: 0x040006D4 RID: 1748
	public ReceiverGroup Receivers;

	// Token: 0x040006D5 RID: 1749
	public byte SequenceChannel;

	// Token: 0x040006D6 RID: 1750
	public bool ForwardToWebhook;

	// Token: 0x040006D7 RID: 1751
	public bool Encrypt;
}
