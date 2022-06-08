using System;
using ProtoBuf;

// Token: 0x0200051D RID: 1309
[ProtoContract]
public class GGNetworkCape
{
	// Token: 0x04002583 RID: 9603
	[ProtoMember(1, IsRequired = true)]
	public bool needTargetCape;

	// Token: 0x04002584 RID: 9604
	[ProtoMember(2, IsRequired = true)]
	public int ownerID;

	// Token: 0x04002585 RID: 9605
	[ProtoMember(3, IsRequired = true)]
	public byte[] data;
}
